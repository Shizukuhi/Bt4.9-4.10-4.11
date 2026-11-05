namespace LabEquipmentCheckout;

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

        // MenuStrip + StatusStrip
        mnuExport = new ToolStripMenuItem { Text = "Xuất CSV..." }; mnuExport.Click += mnuExport_Click;
        mnuExit = new ToolStripMenuItem { Text = "Thoát" }; mnuExit.Click += mnuExit_Click;
        mnuFile = new ToolStripMenuItem { Text = "&Tệp" };
        mnuFile.DropDownItems.AddRange(new ToolStripItem[] { mnuExport, mnuExit });
        menuStrip1 = new MenuStrip { Dock = DockStyle.Top };
        menuStrip1.Items.Add(mnuFile);
        lblCount = new ToolStripStatusLabel { Text = "Tổng số phiếu: 0" };
        statusStrip1 = new StatusStrip { Dock = DockStyle.Bottom };
        statusStrip1.Items.Add(lblCount);
        errorProvider1 = new ErrorProvider(components) { BlinkStyle = ErrorBlinkStyle.NeverBlink };

        // Khu vực nhập phiếu mượn
        lblId = new Label { Text = "Mã phiếu:", AutoSize = true, Anchor = AnchorStyles.Left };
        lblStudentId = new Label { Text = "Mã sinh viên:", AutoSize = true, Anchor = AnchorStyles.Left };
        lblStudentName = new Label { Text = "Họ tên sinh viên:", AutoSize = true, Anchor = AnchorStyles.Left };
        lblEquipment = new Label { Text = "Thiết bị:", AutoSize = true, Anchor = AnchorStyles.Left };
        lblQty = new Label { Text = "Số lượng:", AutoSize = true, Anchor = AnchorStyles.Left };
        lblStatus = new Label { Text = "Tình trạng:", AutoSize = true, Anchor = AnchorStyles.Left };
        lblBorrow = new Label { Text = "Ngày mượn:", AutoSize = true, Anchor = AnchorStyles.Left };
        lblDue = new Label { Text = "Hạn trả:", AutoSize = true, Anchor = AnchorStyles.Left };
        lblReturn = new Label { Text = "Ngày trả thực tế:", AutoSize = true, Anchor = AnchorStyles.Left };
        txtId = new TextBox { Anchor = AnchorStyles.Left | AnchorStyles.Right };
        txtStudentId = new TextBox { Anchor = AnchorStyles.Left | AnchorStyles.Right };
        txtStudentName = new TextBox { Anchor = AnchorStyles.Left | AnchorStyles.Right };
        txtStatus = new TextBox { Anchor = AnchorStyles.Left | AnchorStyles.Right, ReadOnly = true };
        cboEquipment = new ComboBox { Anchor = AnchorStyles.Left | AnchorStyles.Right, DropDownStyle = ComboBoxStyle.DropDownList };
        numQty = new NumericUpDown { Anchor = AnchorStyles.Left, Minimum = 0, Maximum = 1000, Value = 1 };
        dtpBorrow = new DateTimePicker { Anchor = AnchorStyles.Left | AnchorStyles.Right, Format = DateTimePickerFormat.Short };
        dtpDue = new DateTimePicker { Anchor = AnchorStyles.Left | AnchorStyles.Right, Format = DateTimePickerFormat.Short };
        dtpReturn = new DateTimePicker { Anchor = AnchorStyles.Left | AnchorStyles.Right, Format = DateTimePickerFormat.Short, ShowCheckBox = true, Checked = false };

        btnAdd = new Button { Text = "Thêm", AutoSize = true }; btnAdd.Click += btnAdd_Click;
        btnUpdate = new Button { Text = "Cập nhật", AutoSize = true }; btnUpdate.Click += btnUpdate_Click;
        btnDelete = new Button { Text = "Xóa", AutoSize = true }; btnDelete.Click += btnDelete_Click;
        btnReturn = new Button { Text = "Đánh dấu đã trả", AutoSize = true }; btnReturn.Click += btnReturn_Click;
        btnClear = new Button { Text = "Làm mới", AutoSize = true }; btnClear.Click += btnClear_Click;
        flowButtons = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoSize = true };
        flowButtons.Controls.AddRange(new Control[] { btnAdd, btnUpdate, btnDelete, btnReturn, btnClear });

        tableInput = new TableLayoutPanel { Dock = DockStyle.Fill, AutoSize = true, ColumnCount = 4, RowCount = 6 };
        tableInput.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120));
        tableInput.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        tableInput.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120));
        tableInput.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        tableInput.Controls.Add(lblId, 0, 0); tableInput.Controls.Add(txtId, 1, 0);
        tableInput.Controls.Add(lblStudentId, 2, 0); tableInput.Controls.Add(txtStudentId, 3, 0);
        tableInput.Controls.Add(lblStudentName, 0, 1); tableInput.Controls.Add(txtStudentName, 1, 1);
        tableInput.Controls.Add(lblEquipment, 2, 1); tableInput.Controls.Add(cboEquipment, 3, 1);
        tableInput.Controls.Add(lblQty, 0, 2); tableInput.Controls.Add(numQty, 1, 2);
        tableInput.Controls.Add(lblStatus, 2, 2); tableInput.Controls.Add(txtStatus, 3, 2);
        tableInput.Controls.Add(lblBorrow, 0, 3); tableInput.Controls.Add(dtpBorrow, 1, 3);
        tableInput.Controls.Add(lblDue, 2, 3); tableInput.Controls.Add(dtpDue, 3, 3);
        tableInput.Controls.Add(lblReturn, 0, 4); tableInput.Controls.Add(dtpReturn, 1, 4);
        tableInput.Controls.Add(flowButtons, 0, 5); tableInput.SetColumnSpan(flowButtons, 4);

        grpInfo = new GroupBox { Text = "Phiếu mượn", Dock = DockStyle.Fill, AutoSize = true };
        grpInfo.Controls.Add(tableInput);

        // Tìm kiếm / lọc
        lblSearch = new Label { Text = "Tìm (mã SV / thiết bị):", AutoSize = true, Margin = new Padding(3, 6, 3, 0) };
        txtSearch = new TextBox { Width = 200 }; txtSearch.TextChanged += Filter_Changed;
        lblFilter = new Label { Text = "Hiển thị:", AutoSize = true, Margin = new Padding(15, 6, 3, 0) };
        cboFilter = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 120 }; cboFilter.SelectedIndexChanged += Filter_Changed;
        flowFilter = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoSize = true };
        flowFilter.Controls.AddRange(new Control[] { lblSearch, txtSearch, lblFilter, cboFilter });

        // DataGridView (cột cấu hình thủ công)
        dgv = new DataGridView
        {
            Dock = DockStyle.Fill, AutoGenerateColumns = false, AllowUserToAddRows = false, AllowUserToDeleteRows = false,
            ReadOnly = true, MultiSelect = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, RowHeadersVisible = false
        };
        var date = new DataGridViewCellStyle { Format = "dd/MM/yyyy", NullValue = "" };
        dgv.Columns.AddRange(new DataGridViewColumn[]
        {
            new DataGridViewTextBoxColumn { Name = "colId", DataPropertyName = "Id", HeaderText = "Mã phiếu", FillWeight = 70 },
            new DataGridViewTextBoxColumn { Name = "colStudentId", DataPropertyName = "StudentId", HeaderText = "Mã SV", FillWeight = 70 },
            new DataGridViewTextBoxColumn { Name = "colStudentName", DataPropertyName = "StudentName", HeaderText = "Họ tên", FillWeight = 110 },
            new DataGridViewTextBoxColumn { Name = "colEquipment", DataPropertyName = "Equipment", HeaderText = "Thiết bị", FillWeight = 120 },
            new DataGridViewTextBoxColumn { Name = "colQty", DataPropertyName = "Quantity", HeaderText = "SL", FillWeight = 40 },
            new DataGridViewTextBoxColumn { Name = "colBorrow", DataPropertyName = "BorrowDate", HeaderText = "Ngày mượn", FillWeight = 80, DefaultCellStyle = date },
            new DataGridViewTextBoxColumn { Name = "colDue", DataPropertyName = "DueDate", HeaderText = "Hạn trả", FillWeight = 80, DefaultCellStyle = date },
            new DataGridViewTextBoxColumn { Name = "colReturn", DataPropertyName = "ReturnDate", HeaderText = "Ngày trả", FillWeight = 80, DefaultCellStyle = date },
            new DataGridViewTextBoxColumn { Name = "colStatus", DataPropertyName = "Status", HeaderText = "Tình trạng", FillWeight = 70 }
        });
        dgv.SelectionChanged += dgv_SelectionChanged;

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
        ClientSize = new Size(1050, 680);
        MinimumSize = new Size(800, 560);
        Text = "Lab Equipment Checkout";
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
    private Label lblId, lblStudentId, lblStudentName, lblEquipment, lblQty, lblStatus, lblBorrow, lblDue, lblReturn, lblSearch, lblFilter;
    private TextBox txtId, txtStudentId, txtStudentName, txtStatus, txtSearch;
    private ComboBox cboEquipment, cboFilter;
    private NumericUpDown numQty;
    private DateTimePicker dtpBorrow, dtpDue, dtpReturn;
    private Button btnAdd, btnUpdate, btnDelete, btnReturn, btnClear;
    private DataGridView dgv;
}
