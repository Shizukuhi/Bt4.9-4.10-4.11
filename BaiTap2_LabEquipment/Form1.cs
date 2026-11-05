using System.ComponentModel;
using System.Text;

namespace LabEquipmentCheckout;

public class Checkout
{
    public string Id { get; set; } = "";
    public string StudentId { get; set; } = "";
    public string StudentName { get; set; } = "";
    public string Equipment { get; set; } = "";
    public int Quantity { get; set; } = 1;
    public DateTime BorrowDate { get; set; } = DateTime.Today;
    public DateTime DueDate { get; set; } = DateTime.Today.AddDays(7);
    public DateTime? ReturnDate { get; set; }

    // Tự xác định: đã có ngày trả -> Đã trả; chưa trả và hạn trả < hôm nay -> Quá hạn
    public string Status => ReturnDate.HasValue ? "Đã trả"
                          : DueDate.Date < DateTime.Today ? "Quá hạn" : "Đang mượn";
}

public partial class Form1 : Form
{
    private readonly List<Checkout> _all = new();
    private readonly BindingSource _bs = new();
    private Checkout _selected;
    private bool _binding;

    public Form1()
    {
        InitializeComponent();
        dgv.DataSource = _bs;
        cboEquipment.Items.AddRange(new object[] { "Oscilloscope", "Đồng hồ vạn năng", "Kit Arduino", "Raspberry Pi", "Máy hàn", "Laptop thực hành" });
        cboFilter.Items.AddRange(new object[] { "Tất cả", "Đang mượn", "Quá hạn", "Đã trả" });

        _all.Add(new Checkout { Id = "PM001", StudentId = "SV001", StudentName = "Nguyễn Văn A", Equipment = "Kit Arduino", Quantity = 1, BorrowDate = DateTime.Today.AddDays(-10), DueDate = DateTime.Today.AddDays(-3) });
        _all.Add(new Checkout { Id = "PM002", StudentId = "SV002", StudentName = "Trần Thị B", Equipment = "Oscilloscope", Quantity = 1, BorrowDate = DateTime.Today, DueDate = DateTime.Today.AddDays(5) });

        cboFilter.SelectedIndex = 0;   // kích hoạt ApplyFilter
        ClearForm();
    }

    private void ApplyFilter()
    {
        string kw = txtSearch.Text.Trim().ToLower();
        var q = _all.Where(c =>
            (kw == "" || c.StudentId.ToLower().Contains(kw) || c.Equipment.ToLower().Contains(kw)) &&
            (cboFilter.SelectedIndex <= 0 || c.Status == cboFilter.Text));
        _binding = true;
        _bs.DataSource = new BindingList<Checkout>(q.ToList());
        foreach (DataGridViewRow r in dgv.Rows)
            r.DefaultCellStyle.ForeColor = ((Checkout)r.DataBoundItem).Status == "Quá hạn" ? Color.Red : Color.Black;
        dgv.ClearSelection();
        _binding = false;
        lblCount.Text = $"Tổng số phiếu: {_all.Count}   |   Quá hạn: {_all.Count(c => c.Status == "Quá hạn")}   |   Đang hiển thị: {_bs.Count}";
    }

    private void Filter_Changed(object sender, EventArgs e) => ApplyFilter();

    private void dgv_SelectionChanged(object sender, EventArgs e)
    {
        if (_binding || dgv.SelectedRows.Count == 0) return;
        if (dgv.SelectedRows[0].DataBoundItem is Checkout c) FillForm(c);
    }

    private void FillForm(Checkout c)
    {
        _selected = c;
        errorProvider1.Clear();
        txtId.Text = c.Id; txtStudentId.Text = c.StudentId; txtStudentName.Text = c.StudentName;
        cboEquipment.SelectedItem = c.Equipment;
        numQty.Value = Math.Max(numQty.Minimum, Math.Min(numQty.Maximum, c.Quantity));
        dtpBorrow.Value = c.BorrowDate; dtpDue.Value = c.DueDate;
        if (c.ReturnDate.HasValue) { dtpReturn.Value = c.ReturnDate.Value; dtpReturn.Checked = true; }
        else dtpReturn.Checked = false;
        txtStatus.Text = c.Status;
    }

    private void SelectRow(Checkout item)
    {
        foreach (DataGridViewRow r in dgv.Rows)
            if (r.DataBoundItem == item) { r.Selected = true; return; }
        FillForm(item);
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
        if (!ValidateForm(null)) return;
        if (!Confirm("Thêm phiếu mượn mới?")) return;
        var c = new Checkout();
        ReadForm(c);
        _all.Add(c);
        ApplyFilter();
        ClearForm();
    }

    private void btnUpdate_Click(object sender, EventArgs e)
    {
        if (_selected == null || !_all.Contains(_selected)) { MessageBox.Show("Hãy chọn một phiếu trong bảng."); return; }
        if (!ValidateForm(_selected)) return;
        if (!Confirm("Cập nhật phiếu này?")) return;
        ReadForm(_selected);
        ApplyFilter();
        SelectRow(_selected);
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
        if (_selected == null || !_all.Contains(_selected)) { MessageBox.Show("Hãy chọn một phiếu trong bảng."); return; }
        if (!Confirm("Xóa phiếu này?")) return;
        _all.Remove(_selected);
        ApplyFilter();
        ClearForm();
    }

    // Đánh dấu đã trả: ghi ngày trả thực tế = hôm nay, tình trạng tự đổi sang "Đã trả"
    private void btnReturn_Click(object sender, EventArgs e)
    {
        if (_selected == null || !_all.Contains(_selected)) { MessageBox.Show("Hãy chọn một phiếu trong bảng."); return; }
        if (_selected.ReturnDate.HasValue) { MessageBox.Show("Phiếu này đã được trả."); return; }
        if (DateTime.Today < _selected.BorrowDate.Date) { MessageBox.Show("Ngày trả không được trước ngày mượn."); return; }
        if (!Confirm("Đánh dấu phiếu này là đã trả (hôm nay)?")) return;
        _selected.ReturnDate = DateTime.Today;
        ApplyFilter();
        SelectRow(_selected);
    }

    private void btnClear_Click(object sender, EventArgs e) => ClearForm();

    private static bool Confirm(string msg) =>
        MessageBox.Show(msg, "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;

    private void ReadForm(Checkout c)
    {
        c.Id = txtId.Text.Trim(); c.StudentId = txtStudentId.Text.Trim(); c.StudentName = txtStudentName.Text.Trim();
        c.Equipment = cboEquipment.Text; c.Quantity = (int)numQty.Value;
        c.BorrowDate = dtpBorrow.Value.Date; c.DueDate = dtpDue.Value.Date;
        c.ReturnDate = dtpReturn.Checked ? dtpReturn.Value.Date : null;
    }

    private void ClearForm()
    {
        _selected = null;
        errorProvider1.Clear();
        txtId.Clear(); txtStudentId.Clear(); txtStudentName.Clear(); txtStatus.Clear();
        cboEquipment.SelectedIndex = -1;
        numQty.Value = 1;
        dtpBorrow.Value = DateTime.Today; dtpDue.Value = DateTime.Today.AddDays(7);
        dtpReturn.Checked = false;
        dgv.ClearSelection();
        txtId.Focus();
    }

    private bool ValidateForm(Checkout editing)
    {
        errorProvider1.Clear();
        bool ok = true;
        void Err(Control c, string m) { errorProvider1.SetError(c, m); ok = false; }

        string id = txtId.Text.Trim();
        if (id == "") Err(txtId, "Mã phiếu không được để trống");
        else if (_all.Any(c => c != editing && c.Id.Equals(id, StringComparison.OrdinalIgnoreCase))) Err(txtId, "Mã phiếu đã tồn tại");
        if (txtStudentId.Text.Trim() == "") Err(txtStudentId, "Mã sinh viên không được để trống");
        if (cboEquipment.SelectedIndex < 0) Err(cboEquipment, "Vui lòng chọn thiết bị");
        if (numQty.Value <= 0) Err(numQty, "Số lượng phải lớn hơn 0");
        if (dtpDue.Value.Date < dtpBorrow.Value.Date) Err(dtpDue, "Hạn trả không được trước ngày mượn");
        if (dtpReturn.Checked && dtpReturn.Value.Date < dtpBorrow.Value.Date) Err(dtpReturn, "Ngày trả không được trước ngày mượn");
        return ok;
    }

    private void mnuExport_Click(object sender, EventArgs e)
    {
        using var sfd = new SaveFileDialog { Filter = "CSV (*.csv)|*.csv", FileName = "phieu_muon.csv" };
        if (sfd.ShowDialog() != DialogResult.OK) return;
        var sb = new StringBuilder("Mã phiếu,Mã SV,Họ tên,Thiết bị,Số lượng,Ngày mượn,Hạn trả,Ngày trả,Tình trạng\r\n");
        foreach (Checkout c in (BindingList<Checkout>)_bs.DataSource)
            sb.AppendLine(string.Join(",", new[] { c.Id, c.StudentId, c.StudentName, c.Equipment, c.Quantity.ToString(),
                c.BorrowDate.ToString("dd/MM/yyyy"), c.DueDate.ToString("dd/MM/yyyy"),
                c.ReturnDate?.ToString("dd/MM/yyyy") ?? "", c.Status }.Select(Csv)));
        File.WriteAllText(sfd.FileName, sb.ToString(), new UTF8Encoding(true));
        MessageBox.Show("Đã xuất file thành công.");
    }

    private static string Csv(string s) => "\"" + s.Replace("\"", "\"\"") + "\"";
    private void mnuExit_Click(object sender, EventArgs e) => Close();
}
