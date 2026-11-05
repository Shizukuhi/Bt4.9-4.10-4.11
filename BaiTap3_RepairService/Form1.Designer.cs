namespace RepairServiceTracker;

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
        lblCount = new ToolStripStatusLabel { Text = "Số đơn: 0" };
        lblTotal = new ToolStripStatusLabel { Text = "Tổng chi phí dự kiến (chưa hoàn tất): 0" };
        statusStrip1 = new StatusStrip { Dock = DockStyle.Bottom };
        statusStrip1.Items.AddRange(new ToolStripItem[] { lblCount, lblTotal });
        errorProvider1 = new ErrorProvider(components) { BlinkStyle = ErrorBlinkStyle.NeverBlink };
        openFileDialog1 = new OpenFileDialog { Filter = "Ảnh (*.jpg;*.jpeg;*.png;*.bmp;*.gif)|*.jpg;*.jpeg;*.png;*.bmp;*.gif", Title = "Chọn ảnh thiết bị" };

        // Khu vực nhập thông tin đơn
        lblId = new Label { Text = "Mã đơn:", AutoSize = true, Anchor = AnchorStyles.Left };
        lblCustomer = new Label { Text = "Tên khách hàng:", AutoSize = true, Anchor = AnchorStyles.Left };
        lblPhone = new Label { Text = "Số điện thoại:", AutoSize = true, Anchor = AnchorStyles.Left };
        lblDevice = new Label { Text = "Loại thiết bị:", AutoSize = true, Anchor = AnchorStyles.Left };
        lblReceived = new Label { Text = "Ngày tiếp nhận:", AutoSize = true, Anchor = AnchorStyles.Left };
        lblCost = new Label { Text = "Chi phí dự kiến:", AutoSize = true, Anchor = AnchorStyles.Left };
        lblStatus = new Label { Text = "Trạng thái:", AutoSize = true, Anchor = AnchorStyles.Left };
        lblDesc = new Label { Text = "Mô tả lỗi:", AutoSize = true, Anchor = AnchorStyles.Left };
        txtId = new TextBox { Anchor = AnchorStyles.Left | AnchorStyles.Right };
        txtCustomer = new TextBox { Anchor = AnchorStyles.Left | AnchorStyles.Right };
        txtPhone = new TextBox { Anchor = AnchorStyles.Left | AnchorStyles.Right };
        txtCost = new TextBox { Anchor = AnchorStyles.Left | AnchorStyles.Right, Text = "0" };
        txtDesc = new TextBox { Anchor = AnchorStyles.Left | AnchorStyles.Right, Multiline = true, Height = 50, ScrollBars = ScrollBars.Vertical };
        cboDevice = new ComboBox { Anchor = AnchorStyles.Left | AnchorStyles.Right, DropDownStyle = ComboBoxStyle.DropDownList };
        cboStatus = new ComboBox { Anchor = AnchorStyles.Left | AnchorStyles.Right, DropDownStyle = ComboBoxStyle.DropDownList };
        dtpReceived = new DateTimePicker { Anchor = AnchorStyles.Left | AnchorStyles.Right, Format = DateTimePickerFormat.Short };

        btnAdd = new Button { Text = "Thêm", AutoSize = true }; btnAdd.Click += btnAdd_Click;
        btnUpdate = new Button { Text = "Cập nhật", AutoSize = true }; btnUpdate.Click += btnUpdate_Click;
        btnDelete = new Button { Text = "Xóa", AutoSize = true }; btnDelete.Click += btnDelete_Click;
        btnClear = new Button { Text = "Làm mới", AutoSize = true }; btnClear.Click += btnClear_Click;
        flowButtons = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoSize = true };
        flowButtons.Controls.AddRange(new Control[] { btnAdd, btnUpdate, btnDelete, btnClear });

        tableInput = new TableLayoutPanel { Dock = DockStyle.Fill, AutoSize = true, ColumnCount = 4, RowCount = 5 };
        tableInput.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110));
        tableInput.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        tableInput.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110));
        tableInput.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        tableInput.Controls.Add(lblId, 0, 0); tableInput.Controls.Add(txtId, 1, 0);
        tableInput.Controls.Add(lblCustomer, 2, 0); tableInput.Controls.Add(txtCustomer, 3, 0);
        tableInput.Controls.Add(lblPhone, 0, 1); tableInput.Controls.Add(txtPhone, 1, 1);
        tableInput.Controls.Add(lblDevice, 2, 1); tableInput.Controls.Add(cboDevice, 3, 1);
        tableInput.Controls.Add(lblReceived, 0, 2); tableInput.Controls.Add(dtpReceived, 1, 2);
        tableInput.Controls.Add(lblCost, 2, 2); tableInput.Controls.Add(txtCost, 3, 2);
        tableInput.Controls.Add(lblStatus, 0, 3); tableInput.Controls.Add(cboStatus, 1, 3);
        tableInput.Controls.Add(lblDesc, 2, 3); tableInput.Controls.Add(txtDesc, 3, 3);
        tableInput.Controls.Add(flowButtons, 0, 4); tableInput.SetColumnSpan(flowButtons, 4);

        grpInfo = new GroupBox { Text = "Thông tin đơn", Dock = DockStyle.Fill, AutoSize = true };
        grpInfo.Controls.Add(tableInput);

        // Khu vực ảnh thiết bị
        picDevice = new PictureBox { Dock = DockStyle.Fill, SizeMode = PictureBoxSizeMode.Zoom, BorderStyle = BorderStyle.FixedSingle };
        btnBrowse = new Button { Text = "Chọn ảnh...", Dock = DockStyle.Bottom }; btnBrowse.Click += btnBrowse_Click;
        grpImage = new GroupBox { Text = "Ảnh thiết bị", Dock = DockStyle.Fill, MinimumSize = new Size(230, 190) };
        grpImage.Controls.Add(picDevice);
        grpImage.Controls.Add(btnBrowse);

        tableTop = new TableLayoutPanel { Dock = DockStyle.Fill, AutoSize = true, ColumnCount = 2, RowCount = 1 };
        tableTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        tableTop.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 240));
        tableTop.Controls.Add(grpInfo, 0, 0);
        tableTop.Controls.Add(grpImage, 1, 0);

        // Tìm kiếm / lọc
        lblSearch = new Label { Text = "Tìm (mã đơn / tên / SĐT):", AutoSize = true, Margin = new Padding(3, 6, 3, 0) };
        txtSearch = new TextBox { Width = 220 }; txtSearch.TextChanged += Filter_Changed;
        lblFStatus = new Label { Text = "Trạng thái:", AutoSize = true, Margin = new Padding(15, 6, 3, 0) };
        cboFilterStatus = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 130 }; cboFilterStatus.SelectedIndexChanged += Filter_Changed;
        flowFilter = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoSize = true };
        flowFilter.Controls.AddRange(new Control[] { lblSearch, txtSearch, lblFStatus, cboFilterStatus });

        // DataGridView
        dgv = new DataGridView
        {
            Dock = DockStyle.Fill, AutoGenerateColumns = false, AllowUserToAddRows = false, AllowUserToDeleteRows = false,
            ReadOnly = true, MultiSelect = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, RowHeadersVisible = false
        };
        dgv.Columns.AddRange(new DataGridViewColumn[]
        {
            new DataGridViewTextBoxColumn { Name = "colId", DataPropertyName = "Id", HeaderText = "Mã đơn", FillWeight = 60 },
            new DataGridViewTextBoxColumn { Name = "colCustomer", DataPropertyName = "CustomerName", HeaderText = "Khách hàng", FillWeight = 110 },
            new DataGridViewTextBoxColumn { Name = "colPhone", DataPropertyName = "Phone", HeaderText = "SĐT", FillWeight = 80 },
            new DataGridViewTextBoxColumn { Name = "colDevice", DataPropertyName = "DeviceType", HeaderText = "Thiết bị", FillWeight = 80 },
            new DataGridViewTextBoxColumn { Name = "colDesc", DataPropertyName = "Description", HeaderText = "Mô tả lỗi", FillWeight = 140 },
            new DataGridViewTextBoxColumn { Name = "colReceived", DataPropertyName = "ReceivedDate", HeaderText = "Ngày nhận", FillWeight = 80, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" } },
            new DataGridViewTextBoxColumn { Name = "colCost", DataPropertyName = "EstimatedCost", HeaderText = "Chi phí dự kiến", FillWeight = 90, DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight } },
            new DataGridViewTextBoxColumn { Name = "colStatus", DataPropertyName = "Status", HeaderText = "Trạng thái", FillWeight = 80 }
        });
        dgv.SelectionChanged += dgv_SelectionChanged;

        tableMain = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 3 };
        tableMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        tableMain.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        tableMain.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        tableMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        tableMain.Controls.Add(tableTop, 0, 0);
        tableMain.Controls.Add(flowFilter, 0, 1);
        tableMain.Controls.Add(dgv, 0, 2);

        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1100, 700);
        MinimumSize = new Size(850, 580);
        Text = "Repair Service Tracker";
        StartPosition = FormStartPosition.CenterScreen;
        Controls.Add(tableMain);
        Controls.Add(statusStrip1);
        Controls.Add(menuStrip1);
        MainMenuStrip = menuStrip1;
        ResumeLayout(false); PerformLayout();
    }

    private MenuStrip menuStrip1; private ToolStripMenuItem mnuFile, mnuExport, mnuExit;
    private StatusStrip statusStrip1; private ToolStripStatusLabel lblCount, lblTotal;
    private ErrorProvider errorProvider1; private OpenFileDialog openFileDialog1;
    private TableLayoutPanel tableMain, tableTop, tableInput; private GroupBox grpInfo, grpImage;
    private FlowLayoutPanel flowButtons, flowFilter;
    private Label lblId, lblCustomer, lblPhone, lblDevice, lblReceived, lblCost, lblStatus, lblDesc, lblSearch, lblFStatus;
    private TextBox txtId, txtCustomer, txtPhone, txtCost, txtDesc, txtSearch;
    private ComboBox cboDevice, cboStatus, cboFilterStatus;
    private DateTimePicker dtpReceived;
    private PictureBox picDevice;
    private Button btnAdd, btnUpdate, btnDelete, btnClear, btnBrowse;
    private DataGridView dgv;
}
