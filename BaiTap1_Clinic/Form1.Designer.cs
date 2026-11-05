namespace ClinicAppointmentManager;

partial class Form1
{
    private System.ComponentModel.IContainer components = null;
    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        SuspendLayout();

        // MenuStrip
        mnuExport = new ToolStripMenuItem { Text = "Xuất CSV..." }; mnuExport.Click += mnuExport_Click;
        mnuExit = new ToolStripMenuItem { Text = "Thoát" }; mnuExit.Click += mnuExit_Click;
        mnuFile = new ToolStripMenuItem { Text = "&Tệp" };
        mnuFile.DropDownItems.AddRange(new ToolStripItem[] { mnuExport, mnuExit });
        menuStrip1 = new MenuStrip { Dock = DockStyle.Top };
        menuStrip1.Items.Add(mnuFile);

        // StatusStrip
        lblCount = new ToolStripStatusLabel { Text = "Số lịch hẹn: 0" };
        statusStrip1 = new StatusStrip { Dock = DockStyle.Bottom };
        statusStrip1.Items.Add(lblCount);

        errorProvider1 = new ErrorProvider(components) { BlinkStyle = ErrorBlinkStyle.NeverBlink };

        // Khu vực nhập liệu
        lblId = new Label { Text = "Mã lịch hẹn:", AutoSize = true, Anchor = AnchorStyles.Left };
        lblName = new Label { Text = "Họ tên bệnh nhân:", AutoSize = true, Anchor = AnchorStyles.Left };
        lblPhone = new Label { Text = "Số điện thoại:", AutoSize = true, Anchor = AnchorStyles.Left };
        lblTime = new Label { Text = "Ngày giờ hẹn:", AutoSize = true, Anchor = AnchorStyles.Left };
        lblDoctor = new Label { Text = "Bác sĩ:", AutoSize = true, Anchor = AnchorStyles.Left };
        lblStatus = new Label { Text = "Trạng thái:", AutoSize = true, Anchor = AnchorStyles.Left };
        lblReason = new Label { Text = "Lý do khám:", AutoSize = true, Anchor = AnchorStyles.Left };
        txtId = new TextBox { Anchor = AnchorStyles.Left | AnchorStyles.Right };
        txtName = new TextBox { Anchor = AnchorStyles.Left | AnchorStyles.Right };
        txtPhone = new TextBox { Anchor = AnchorStyles.Left | AnchorStyles.Right };
        txtReason = new TextBox { Anchor = AnchorStyles.Left | AnchorStyles.Right };
        dtpTime = new DateTimePicker { Anchor = AnchorStyles.Left | AnchorStyles.Right, Format = DateTimePickerFormat.Custom, CustomFormat = "dd/MM/yyyy HH:mm" };
        cboDoctor = new ComboBox { Anchor = AnchorStyles.Left | AnchorStyles.Right, DropDownStyle = ComboBoxStyle.DropDownList };
        cboStatus = new ComboBox { Anchor = AnchorStyles.Left | AnchorStyles.Right, DropDownStyle = ComboBoxStyle.DropDownList };

        btnAdd = new Button { Text = "Thêm", AutoSize = true }; btnAdd.Click += btnAdd_Click;
        btnUpdate = new Button { Text = "Cập nhật", AutoSize = true }; btnUpdate.Click += btnUpdate_Click;
        btnDelete = new Button { Text = "Xóa", AutoSize = true }; btnDelete.Click += btnDelete_Click;
        btnClear = new Button { Text = "Làm mới", AutoSize = true }; btnClear.Click += btnClear_Click;
        flowButtons = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoSize = true };
        flowButtons.Controls.AddRange(new Control[] { btnAdd, btnUpdate, btnDelete, btnClear });

        tableInput = new TableLayoutPanel { Dock = DockStyle.Fill, AutoSize = true, ColumnCount = 4, RowCount = 5 };
        tableInput.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120));
        tableInput.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        tableInput.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100));
        tableInput.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        tableInput.Controls.Add(lblId, 0, 0); tableInput.Controls.Add(txtId, 1, 0);
        tableInput.Controls.Add(lblName, 2, 0); tableInput.Controls.Add(txtName, 3, 0);
        tableInput.Controls.Add(lblPhone, 0, 1); tableInput.Controls.Add(txtPhone, 1, 1);
        tableInput.Controls.Add(lblTime, 2, 1); tableInput.Controls.Add(dtpTime, 3, 1);
        tableInput.Controls.Add(lblDoctor, 0, 2); tableInput.Controls.Add(cboDoctor, 1, 2);
        tableInput.Controls.Add(lblStatus, 2, 2); tableInput.Controls.Add(cboStatus, 3, 2);
        tableInput.Controls.Add(lblReason, 0, 3); tableInput.Controls.Add(txtReason, 1, 3);
        tableInput.SetColumnSpan(txtReason, 3);
        tableInput.Controls.Add(flowButtons, 0, 4); tableInput.SetColumnSpan(flowButtons, 4);

        grpInfo = new GroupBox { Text = "Thông tin lịch hẹn", Dock = DockStyle.Fill, AutoSize = true };
        grpInfo.Controls.Add(tableInput);

        // Thanh tìm kiếm / lọc
        lblSearch = new Label { Text = "Tìm (tên / SĐT):", AutoSize = true, Margin = new Padding(3, 6, 3, 0) };
        txtSearch = new TextBox { Width = 200 }; txtSearch.TextChanged += Filter_Changed;
        chkDate = new CheckBox { Text = "Lọc theo ngày:", AutoSize = true, Margin = new Padding(15, 4, 3, 0) }; chkDate.CheckedChanged += Filter_Changed;
        dtpFilter = new DateTimePicker { Format = DateTimePickerFormat.Short, Width = 110 }; dtpFilter.ValueChanged += Filter_Changed;
        lblFStatus = new Label { Text = "Trạng thái:", AutoSize = true, Margin = new Padding(15, 6, 3, 0) };
        cboFilterStatus = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 120 }; cboFilterStatus.SelectedIndexChanged += Filter_Changed;
        flowFilter = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoSize = true };
        flowFilter.Controls.AddRange(new Control[] { lblSearch, txtSearch, chkDate, dtpFilter, lblFStatus, cboFilterStatus });

        // DataGridView
        dgv = new DataGridView
        {
            Dock = DockStyle.Fill, AutoGenerateColumns = false, AllowUserToAddRows = false, AllowUserToDeleteRows = false,
            ReadOnly = true, MultiSelect = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, RowHeadersVisible = false
        };
        dgv.Columns.AddRange(new DataGridViewColumn[]
        {
            new DataGridViewTextBoxColumn { Name = "colId", DataPropertyName = "Id", HeaderText = "Mã LH", FillWeight = 60 },
            new DataGridViewTextBoxColumn { Name = "colName", DataPropertyName = "PatientName", HeaderText = "Bệnh nhân", FillWeight = 120 },
            new DataGridViewTextBoxColumn { Name = "colPhone", DataPropertyName = "Phone", HeaderText = "SĐT", FillWeight = 80 },
            new DataGridViewTextBoxColumn { Name = "colTime", DataPropertyName = "Time", HeaderText = "Ngày giờ hẹn", FillWeight = 100, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" } },
            new DataGridViewTextBoxColumn { Name = "colDoctor", DataPropertyName = "Doctor", HeaderText = "Bác sĩ", FillWeight = 110 },
            new DataGridViewTextBoxColumn { Name = "colReason", DataPropertyName = "Reason", HeaderText = "Lý do khám", FillWeight = 130 },
            new DataGridViewTextBoxColumn { Name = "colStatus", DataPropertyName = "Status", HeaderText = "Trạng thái", FillWeight = 70 }
        });
        dgv.SelectionChanged += dgv_SelectionChanged;

        // TableLayoutPanel chính (co giãn theo Form)
        tableMain = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 3 };
        tableMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        tableMain.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        tableMain.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        tableMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        tableMain.Controls.Add(grpInfo, 0, 0);
        tableMain.Controls.Add(flowFilter, 0, 1);
        tableMain.Controls.Add(dgv, 0, 2);

        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1000, 640);
        MinimumSize = new Size(760, 520);
        Text = "Clinic Appointment Manager";
        StartPosition = FormStartPosition.CenterScreen;
        Controls.Add(tableMain);
        Controls.Add(statusStrip1);
        Controls.Add(menuStrip1);
        MainMenuStrip = menuStrip1;
        ResumeLayout(false); PerformLayout();
    }

    private MenuStrip menuStrip1; private ToolStripMenuItem mnuFile, mnuExport, mnuExit;
    private StatusStrip statusStrip1; private ToolStripStatusLabel lblCount;
    private ErrorProvider errorProvider1;
    private TableLayoutPanel tableMain, tableInput; private GroupBox grpInfo;
    private FlowLayoutPanel flowButtons, flowFilter;
    private Label lblId, lblName, lblPhone, lblTime, lblDoctor, lblStatus, lblReason, lblSearch, lblFStatus;
    private TextBox txtId, txtName, txtPhone, txtReason, txtSearch;
    private DateTimePicker dtpTime, dtpFilter;
    private ComboBox cboDoctor, cboStatus, cboFilterStatus;
    private CheckBox chkDate;
    private Button btnAdd, btnUpdate, btnDelete, btnClear;
    private DataGridView dgv;
}
