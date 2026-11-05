using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;

namespace ClinicAppointmentManager;

public class Appointment
{
    public string Id { get; set; } = "";
    public string PatientName { get; set; } = "";
    public string Phone { get; set; } = "";
    public DateTime Time { get; set; }
    public string Doctor { get; set; } = "";
    public string Reason { get; set; } = "";
    public string Status { get; set; } = "Chờ khám";
}

public partial class Form1 : Form
{
    private static readonly Regex PhoneRx = new(@"^(0|\+84)\d{9,10}$");
    private static readonly string[] Statuses = { "Chờ khám", "Đang khám", "Hoàn tất", "Đã hủy" };

    private readonly List<Appointment> _all = new();   // dữ liệu gốc
    private readonly BindingSource _bs = new();        // nguồn dữ liệu của lưới (BindingList<Appointment> đã lọc)
    private Appointment _selected;
    private bool _binding;

    public Form1()
    {
        InitializeComponent();
        dgv.DataSource = _bs;
        cboDoctor.Items.AddRange(new object[] { "BS. Nguyễn Văn An", "BS. Trần Thị Bình", "BS. Lê Minh Châu" });
        cboStatus.Items.AddRange(Statuses);
        cboFilterStatus.Items.Add("Tất cả");
        cboFilterStatus.Items.AddRange(Statuses);

        _all.Add(new Appointment { Id = "LH001", PatientName = "Nguyễn Thị Lan", Phone = "0901234567", Time = DateTime.Today.AddDays(1).AddHours(9), Doctor = "BS. Nguyễn Văn An", Reason = "Khám tổng quát", Status = "Chờ khám" });
        _all.Add(new Appointment { Id = "LH002", PatientName = "Trần Văn Hùng", Phone = "0912345678", Time = DateTime.Today.AddDays(1).AddHours(10), Doctor = "BS. Trần Thị Bình", Reason = "Đau răng", Status = "Chờ khám" });

        cboFilterStatus.SelectedIndex = 0;   // kích hoạt ApplyFilter
        ClearForm();
    }

    // ---------- Lọc / tìm kiếm ----------
    private void ApplyFilter()
    {
        string kw = txtSearch.Text.Trim().ToLower();
        var q = _all.Where(a =>
            (kw == "" || a.PatientName.ToLower().Contains(kw) || a.Phone.Contains(kw)) &&
            (!chkDate.Checked || a.Time.Date == dtpFilter.Value.Date) &&
            (cboFilterStatus.SelectedIndex <= 0 || a.Status == cboFilterStatus.Text));
        _binding = true;
        _bs.DataSource = new BindingList<Appointment>(q.ToList());
        dgv.ClearSelection();
        _binding = false;
        lblCount.Text = $"Số lịch hẹn: {_bs.Count}";
    }

    private void Filter_Changed(object sender, EventArgs e) => ApplyFilter();

    // ---------- Chọn dòng -> nạp lên form ----------
    private void dgv_SelectionChanged(object sender, EventArgs e)
    {
        if (_binding || dgv.SelectedRows.Count == 0) return;
        if (dgv.SelectedRows[0].DataBoundItem is not Appointment a) return;
        _selected = a;
        errorProvider1.Clear();
        txtId.Text = a.Id; txtName.Text = a.PatientName; txtPhone.Text = a.Phone; txtReason.Text = a.Reason;
        dtpTime.Value = a.Time;
        cboDoctor.SelectedItem = a.Doctor; cboStatus.SelectedItem = a.Status;
    }

    private void SelectRow(Appointment item)
    {
        foreach (DataGridViewRow r in dgv.Rows)
            if (r.DataBoundItem == item) { r.Selected = true; break; }
    }

    // ---------- Thêm / Sửa / Xóa ----------
    private void btnAdd_Click(object sender, EventArgs e)
    {
        if (!ValidateForm(null)) return;
        if (!Confirm("Thêm lịch hẹn mới?")) return;
        var a = new Appointment();
        ReadForm(a);
        _all.Add(a);
        ApplyFilter();
        ClearForm();
    }

    private void btnUpdate_Click(object sender, EventArgs e)
    {
        if (_selected == null || !_all.Contains(_selected)) { MessageBox.Show("Hãy chọn một lịch hẹn trong bảng."); return; }
        if (!ValidateForm(_selected)) return;
        if (!Confirm("Cập nhật lịch hẹn này?")) return;
        ReadForm(_selected);
        ApplyFilter();
        SelectRow(_selected);
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
        if (_selected == null || !_all.Contains(_selected)) { MessageBox.Show("Hãy chọn một lịch hẹn trong bảng."); return; }
        if (!Confirm("Xóa lịch hẹn này?")) return;
        _all.Remove(_selected);
        ApplyFilter();
        ClearForm();
    }

    private void btnClear_Click(object sender, EventArgs e) => ClearForm();

    private static bool Confirm(string msg) =>
        MessageBox.Show(msg, "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;

    private void ReadForm(Appointment a)
    {
        a.Id = txtId.Text.Trim(); a.PatientName = txtName.Text.Trim(); a.Phone = txtPhone.Text.Trim();
        a.Reason = txtReason.Text.Trim(); a.Time = TrimMinute(dtpTime.Value);
        a.Doctor = cboDoctor.Text; a.Status = cboStatus.Text;
    }

    private void ClearForm()
    {
        _selected = null;
        errorProvider1.Clear();
        txtId.Clear(); txtName.Clear(); txtPhone.Clear(); txtReason.Clear();
        dtpTime.Value = DateTime.Now.AddHours(1);
        cboDoctor.SelectedIndex = 0; cboStatus.SelectedIndex = 0;
        dgv.ClearSelection();
        txtId.Focus();
    }

    private static DateTime TrimMinute(DateTime v) => new(v.Year, v.Month, v.Day, v.Hour, v.Minute, 0);

    // ---------- Kiểm tra dữ liệu + ErrorProvider ----------
    private bool ValidateForm(Appointment editing)
    {
        errorProvider1.Clear();
        bool ok = true;
        void Err(Control c, string m) { errorProvider1.SetError(c, m); ok = false; }

        string id = txtId.Text.Trim();
        if (id == "") Err(txtId, "Mã lịch hẹn không được để trống");
        else if (_all.Any(a => a != editing && a.Id.Equals(id, StringComparison.OrdinalIgnoreCase))) Err(txtId, "Mã lịch hẹn đã tồn tại");
        if (txtName.Text.Trim() == "") Err(txtName, "Vui lòng nhập họ tên bệnh nhân");
        if (!PhoneRx.IsMatch(txtPhone.Text.Trim())) Err(txtPhone, "Số điện thoại không hợp lệ (VD: 0901234567)");
        if (txtReason.Text.Trim() == "") Err(txtReason, "Vui lòng nhập lý do khám");
        if (cboDoctor.SelectedIndex < 0) Err(cboDoctor, "Chọn bác sĩ");
        if (cboStatus.SelectedIndex < 0) Err(cboStatus, "Chọn trạng thái");

        DateTime t = TrimMinute(dtpTime.Value);
        bool timeChanged = editing == null || editing.Time != t;   // sửa lịch cũ mà không đổi giờ thì không bắt lỗi
        if (timeChanged && t < DateTime.Now) Err(dtpTime, "Ngày giờ hẹn không được nằm trong quá khứ");
        return ok;
    }

    // ---------- Menu ----------
    private void mnuExport_Click(object sender, EventArgs e)
    {
        using var sfd = new SaveFileDialog { Filter = "CSV (*.csv)|*.csv", FileName = "lich_hen.csv" };
        if (sfd.ShowDialog() != DialogResult.OK) return;
        var sb = new StringBuilder("Mã lịch hẹn,Họ tên,Số điện thoại,Ngày giờ hẹn,Bác sĩ,Lý do khám,Trạng thái\r\n");
        foreach (Appointment a in (BindingList<Appointment>)_bs.DataSource)
            sb.AppendLine(string.Join(",", new[] { a.Id, a.PatientName, a.Phone, a.Time.ToString("dd/MM/yyyy HH:mm"), a.Doctor, a.Reason, a.Status }.Select(Csv)));
        File.WriteAllText(sfd.FileName, sb.ToString(), new UTF8Encoding(true));
        MessageBox.Show("Đã xuất file thành công.");
    }

    private static string Csv(string s) => "\"" + s.Replace("\"", "\"\"") + "\"";
    private void mnuExit_Click(object sender, EventArgs e) => Close();
}
