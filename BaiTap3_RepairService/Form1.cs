using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace RepairServiceTracker;

public class RepairOrder
{
    public string Id { get; set; } = "";
    public string CustomerName { get; set; } = "";
    public string Phone { get; set; } = "";
    public string DeviceType { get; set; } = "";
    public string Description { get; set; } = "";
    public DateTime ReceivedDate { get; set; } = DateTime.Today;
    public decimal EstimatedCost { get; set; }
    public string Status { get; set; } = "Mới tiếp nhận";
    public string ImagePath { get; set; } = "";
}

public partial class Form1 : Form
{
    private static readonly string[] Statuses = { "Mới tiếp nhận", "Đang sửa", "Chờ linh kiện", "Hoàn tất", "Đã hủy" };

    private readonly List<RepairOrder> _all = new();
    private readonly BindingSource _bs = new();
    private RepairOrder _selected;
    private string _imagePath = "";
    private bool _binding;

    public Form1()
    {
        InitializeComponent();
        dgv.DataSource = _bs;
        cboDevice.Items.AddRange(new object[] { "Điện thoại", "Laptop", "Máy tính bảng", "Tivi", "Máy in", "Khác" });
        cboStatus.Items.AddRange(Statuses);
        cboFilterStatus.Items.Add("Tất cả");
        cboFilterStatus.Items.AddRange(Statuses);

        _all.Add(new RepairOrder { Id = "SC001", CustomerName = "Lê Văn C", Phone = "0987654321", DeviceType = "Laptop", Description = "Không lên nguồn", EstimatedCost = 850000, Status = "Đang sửa" });
        _all.Add(new RepairOrder { Id = "SC002", CustomerName = "Phạm Thị D", Phone = "0977111222", DeviceType = "Điện thoại", Description = "Vỡ màn hình", EstimatedCost = 1200000, Status = "Hoàn tất" });

        cboFilterStatus.SelectedIndex = 0;   // kích hoạt ApplyFilter
        ClearForm();
    }

    private void ApplyFilter()
    {
        string kw = txtSearch.Text.Trim().ToLower();
        var q = _all.Where(o =>
            (kw == "" || o.Id.ToLower().Contains(kw) || o.CustomerName.ToLower().Contains(kw) || o.Phone.Contains(kw)) &&
            (cboFilterStatus.SelectedIndex <= 0 || o.Status == cboFilterStatus.Text));
        _binding = true;
        _bs.DataSource = new BindingList<RepairOrder>(q.ToList());
        dgv.ClearSelection();
        _binding = false;
        UpdateStatusBar();
    }

    // Tổng chi phí dự kiến của các đơn chưa hoàn tất (không tính đơn Hoàn tất và Đã hủy)
    private void UpdateStatusBar()
    {
        decimal total = _all.Where(o => o.Status != "Hoàn tất" && o.Status != "Đã hủy").Sum(o => o.EstimatedCost);
        lblCount.Text = $"Số đơn: {_bs.Count}";
        lblTotal.Text = $"Tổng chi phí dự kiến (chưa hoàn tất): {total:N0}";
    }

    private void Filter_Changed(object sender, EventArgs e) => ApplyFilter();

    private void dgv_SelectionChanged(object sender, EventArgs e)
    {
        if (_binding || dgv.SelectedRows.Count == 0) return;
        if (dgv.SelectedRows[0].DataBoundItem is RepairOrder o) FillForm(o);
    }

    private void FillForm(RepairOrder o)
    {
        _selected = o;
        errorProvider1.Clear();
        txtId.Text = o.Id; txtCustomer.Text = o.CustomerName; txtPhone.Text = o.Phone; txtDesc.Text = o.Description;
        txtCost.Text = o.EstimatedCost.ToString("N0");
        dtpReceived.Value = o.ReceivedDate;
        cboDevice.SelectedItem = o.DeviceType; cboStatus.SelectedItem = o.Status;
        ShowImage(o.ImagePath);
    }

    private void SelectRow(RepairOrder item)
    {
        foreach (DataGridViewRow r in dgv.Rows)
            if (r.DataBoundItem == item) { r.Selected = true; return; }
        FillForm(item);
    }

    // ---------- Ảnh thiết bị ----------
    private void btnBrowse_Click(object sender, EventArgs e)
    {
        if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
        ShowImage(openFileDialog1.FileName);
    }

    private void ShowImage(string path)
    {
        picDevice.Image?.Dispose();
        picDevice.Image = null;
        _imagePath = "";
        if (string.IsNullOrEmpty(path) || !File.Exists(path)) return;
        try
        {
            using var tmp = Image.FromFile(path);
            picDevice.Image = new Bitmap(tmp);   // copy để không khóa file; SizeMode = Zoom nên không méo
            _imagePath = path;
        }
        catch { MessageBox.Show("Không thể mở file ảnh này."); }
    }

    // ---------- Thêm / Sửa / Xóa ----------
    private void btnAdd_Click(object sender, EventArgs e)
    {
        if (!ValidateForm(null, out decimal cost)) return;
        if (!Confirm("Thêm đơn mới?")) return;
        var o = new RepairOrder();
        ReadForm(o, cost);
        _all.Add(o);
        ApplyFilter();
        ClearForm();
    }

    private void btnUpdate_Click(object sender, EventArgs e)
    {
        if (_selected == null || !_all.Contains(_selected)) { MessageBox.Show("Hãy chọn một đơn trong bảng."); return; }
        if (!ValidateForm(_selected, out decimal cost)) return;
        if (!Confirm("Cập nhật đơn này?")) return;
        ReadForm(_selected, cost);
        ApplyFilter();
        SelectRow(_selected);
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
        if (_selected == null || !_all.Contains(_selected)) { MessageBox.Show("Hãy chọn một đơn trong bảng."); return; }
        if (!Confirm("Xóa đơn này?")) return;
        _all.Remove(_selected);
        ApplyFilter();
        ClearForm();
    }

    private void btnClear_Click(object sender, EventArgs e) => ClearForm();

    private static bool Confirm(string msg) =>
        MessageBox.Show(msg, "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;

    private void ReadForm(RepairOrder o, decimal cost)
    {
        o.Id = txtId.Text.Trim(); o.CustomerName = txtCustomer.Text.Trim(); o.Phone = txtPhone.Text.Trim();
        o.DeviceType = cboDevice.Text; o.Description = txtDesc.Text.Trim();
        o.ReceivedDate = dtpReceived.Value.Date; o.EstimatedCost = cost;
        o.Status = cboStatus.Text; o.ImagePath = _imagePath;
    }

    private void ClearForm()
    {
        _selected = null;
        errorProvider1.Clear();
        txtId.Clear(); txtCustomer.Clear(); txtPhone.Clear(); txtDesc.Clear(); txtCost.Text = "0";
        dtpReceived.Value = DateTime.Today;
        cboDevice.SelectedIndex = 0; cboStatus.SelectedIndex = 0;
        ShowImage(null);
        dgv.ClearSelection();
        txtId.Focus();
    }

    private bool ValidateForm(RepairOrder editing, out decimal cost)
    {
        errorProvider1.Clear();
        bool ok = true;
        decimal parsed = 0;
        void Err(Control c, string m) { errorProvider1.SetError(c, m); ok = false; }

        string id = txtId.Text.Trim();
        if (id == "") Err(txtId, "Mã đơn không được để trống");
        else if (_all.Any(o => o != editing && o.Id.Equals(id, StringComparison.OrdinalIgnoreCase))) Err(txtId, "Mã đơn đã tồn tại");
        if (txtCustomer.Text.Trim() == "") Err(txtCustomer, "Tên khách hàng không được để trống");
        if (txtPhone.Text.Trim() == "") Err(txtPhone, "Số điện thoại không được để trống");
        if (txtDesc.Text.Trim() == "") Err(txtDesc, "Vui lòng nhập mô tả lỗi");
        if (cboDevice.SelectedIndex < 0) Err(cboDevice, "Chọn loại thiết bị");
        if (cboStatus.SelectedIndex < 0) Err(cboStatus, "Chọn trạng thái");
        if (!decimal.TryParse(txtCost.Text.Trim(), NumberStyles.Number, CultureInfo.CurrentCulture, out parsed) || parsed < 0)
            Err(txtCost, "Chi phí phải là số không âm");

        cost = parsed;
        return ok;
    }

    // ---------- Menu ----------
    private void mnuExport_Click(object sender, EventArgs e)
    {
        using var sfd = new SaveFileDialog { Filter = "CSV (*.csv)|*.csv", FileName = "don_sua_chua.csv" };
        if (sfd.ShowDialog() != DialogResult.OK) return;
        var sb = new StringBuilder("Mã đơn,Khách hàng,SĐT,Thiết bị,Mô tả lỗi,Ngày tiếp nhận,Chi phí dự kiến,Trạng thái\r\n");
        foreach (RepairOrder o in (BindingList<RepairOrder>)_bs.DataSource)
            sb.AppendLine(string.Join(",", new[] { o.Id, o.CustomerName, o.Phone, o.DeviceType, o.Description,
                o.ReceivedDate.ToString("dd/MM/yyyy"), o.EstimatedCost.ToString(CultureInfo.InvariantCulture), o.Status }.Select(Csv)));
        File.WriteAllText(sfd.FileName, sb.ToString(), new UTF8Encoding(true));
        MessageBox.Show("Đã xuất file thành công.");
    }

    private static string Csv(string s) => "\"" + s.Replace("\"", "\"\"") + "\"";
    private void mnuExit_Click(object sender, EventArgs e) => Close();
}
