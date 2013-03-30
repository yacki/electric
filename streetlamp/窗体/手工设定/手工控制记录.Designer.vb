<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 手工控制记录
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(手工控制记录))
        Me.cb_control_box_name = New System.Windows.Forms.ComboBox()
        Me.control_box_string = New System.Windows.Forms.Label()
        Me.cb_lamp_id = New System.Windows.Forms.ComboBox()
        Me.lamp_id_string = New System.Windows.Forms.Label()
        Me.bt_find_record = New System.Windows.Forms.Button()
        Me.record_num = New System.Windows.Forms.ToolStripStatusLabel()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.progress = New System.Windows.Forms.ToolStripProgressBar()
        Me.dgv_hand_control_list = New System.Windows.Forms.DataGridView()
        Me.user_name = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Controlcontent = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Contentname = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Controlmethod = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Diangan = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.power = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Controltime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HandcontrolrecordBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Hand_record = New streetlamp.hand_record()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.rb_no_date_condition_find = New System.Windows.Forms.RadioButton()
        Me.rb_date_condition_find = New System.Windows.Forms.RadioButton()
        Me.dtp_date_end = New System.Windows.Forms.DateTimePicker()
        Me.dtp_date_start = New System.Windows.Forms.DateTimePicker()
        Me.Date_end_string = New System.Windows.Forms.Label()
        Me.Date_start_string = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.rb_area_control = New System.Windows.Forms.RadioButton()
        Me.rb_street_control = New System.Windows.Forms.RadioButton()
        Me.rb_city_control = New System.Windows.Forms.RadioButton()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cb_city_name = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cb_area_name = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cb_street_name = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lamp_type_id = New System.Windows.Forms.Label()
        Me.lamp_id_start = New System.Windows.Forms.Label()
        Me.rb_lamp_type_control = New System.Windows.Forms.RadioButton()
        Me.cb_lamp_type = New System.Windows.Forms.ComboBox()
        Me.rb_control_box_name_control = New System.Windows.Forms.RadioButton()
        Me.rb_lamp_id_control = New System.Windows.Forms.RadioButton()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.bt_excel_table = New System.Windows.Forms.Button()
        Me.bt_getall = New System.Windows.Forms.Button()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.rb_divtime_check = New System.Windows.Forms.RadioButton()
        Me.rb_hand_check = New System.Windows.Forms.RadioButton()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.bt_operation_excel = New System.Windows.Forms.Button()
        Me.bt_operation_all = New System.Windows.Forms.Button()
        Me.bt_operation_check = New System.Windows.Forms.Button()
        Me.rb_check_nousername = New System.Windows.Forms.RadioButton()
        Me.rb_check_username = New System.Windows.Forms.RadioButton()
        Me.cb_usernamelist = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.dgv_operation_record = New System.Windows.Forms.DataGridView()
        Me.operation_username = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.operation_string = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.operation_time = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OperationrecordBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Operation_list = New streetlamp.operation_list()
        Me.dtp_operation_endtime = New System.Windows.Forms.DateTimePicker()
        Me.dtp_operation_starttime = New System.Windows.Forms.DateTimePicker()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Operation_recordTableAdapter = New streetlamp.operation_listTableAdapters.operation_recordTableAdapter()
        Me.Hand_control_recordTableAdapter = New streetlamp.hand_recordTableAdapters.hand_control_recordTableAdapter()
        Me.StatusStrip1.SuspendLayout()
        CType(Me.dgv_hand_control_list, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.HandcontrolrecordBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Hand_record, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.dgv_operation_record, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OperationrecordBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Operation_list, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cb_control_box_name
        '
        Me.cb_control_box_name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_control_box_name.FormattingEnabled = True
        Me.cb_control_box_name.Location = New System.Drawing.Point(91, 175)
        Me.cb_control_box_name.Name = "cb_control_box_name"
        Me.cb_control_box_name.Size = New System.Drawing.Size(118, 20)
        Me.cb_control_box_name.TabIndex = 84
        '
        'control_box_string
        '
        Me.control_box_string.AutoSize = True
        Me.control_box_string.BackColor = System.Drawing.Color.Transparent
        Me.control_box_string.Location = New System.Drawing.Point(17, 178)
        Me.control_box_string.Name = "control_box_string"
        Me.control_box_string.Size = New System.Drawing.Size(65, 12)
        Me.control_box_string.TabIndex = 83
        Me.control_box_string.Text = "网关名称："
        '
        'cb_lamp_id
        '
        Me.cb_lamp_id.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_lamp_id.FormattingEnabled = True
        Me.cb_lamp_id.Location = New System.Drawing.Point(91, 232)
        Me.cb_lamp_id.Name = "cb_lamp_id"
        Me.cb_lamp_id.Size = New System.Drawing.Size(118, 20)
        Me.cb_lamp_id.TabIndex = 86
        '
        'lamp_id_string
        '
        Me.lamp_id_string.AutoSize = True
        Me.lamp_id_string.BackColor = System.Drawing.Color.Transparent
        Me.lamp_id_string.Location = New System.Drawing.Point(20, 235)
        Me.lamp_id_string.Name = "lamp_id_string"
        Me.lamp_id_string.Size = New System.Drawing.Size(65, 12)
        Me.lamp_id_string.TabIndex = 85
        Me.lamp_id_string.Text = "终端代码："
        '
        'bt_find_record
        '
        Me.bt_find_record.Location = New System.Drawing.Point(98, 439)
        Me.bt_find_record.Name = "bt_find_record"
        Me.bt_find_record.Size = New System.Drawing.Size(64, 20)
        Me.bt_find_record.TabIndex = 87
        Me.bt_find_record.Text = "查询"
        Me.bt_find_record.UseVisualStyleBackColor = True
        '
        'record_num
        '
        Me.record_num.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.record_num.Name = "record_num"
        Me.record_num.Size = New System.Drawing.Size(63, 17)
        Me.record_num.Text = "系统日志"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.record_num, Me.progress})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 491)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(787, 22)
        Me.StatusStrip1.TabIndex = 2
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'progress
        '
        Me.progress.Name = "progress"
        Me.progress.Size = New System.Drawing.Size(200, 16)
        '
        'dgv_hand_control_list
        '
        Me.dgv_hand_control_list.AllowUserToAddRows = False
        Me.dgv_hand_control_list.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv_hand_control_list.AutoGenerateColumns = False
        Me.dgv_hand_control_list.BackgroundColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_hand_control_list.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv_hand_control_list.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_hand_control_list.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.user_name, Me.ID, Me.Controlcontent, Me.Contentname, Me.Controlmethod, Me.Diangan, Me.power, Me.Controltime})
        Me.dgv_hand_control_list.DataSource = Me.HandcontrolrecordBindingSource
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv_hand_control_list.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgv_hand_control_list.Location = New System.Drawing.Point(12, 20)
        Me.dgv_hand_control_list.Name = "dgv_hand_control_list"
        Me.dgv_hand_control_list.RowTemplate.Height = 23
        Me.dgv_hand_control_list.Size = New System.Drawing.Size(494, 422)
        Me.dgv_hand_control_list.TabIndex = 90
        '
        'user_name
        '
        Me.user_name.DataPropertyName = "user_name"
        Me.user_name.HeaderText = "用户名"
        Me.user_name.Name = "user_name"
        '
        'ID
        '
        Me.ID.DataPropertyName = "id"
        Me.ID.FillWeight = 100.166!
        Me.ID.HeaderText = "编号"
        Me.ID.Name = "ID"
        Me.ID.ReadOnly = True
        Me.ID.Visible = False
        '
        'Controlcontent
        '
        Me.Controlcontent.DataPropertyName = "control_content"
        Me.Controlcontent.FillWeight = 100.0628!
        Me.Controlcontent.HeaderText = "控制范围"
        Me.Controlcontent.Name = "Controlcontent"
        '
        'Contentname
        '
        Me.Contentname.DataPropertyName = "content_name"
        Me.Contentname.FillWeight = 99.97436!
        Me.Contentname.HeaderText = "范围名称"
        Me.Contentname.Name = "Contentname"
        Me.Contentname.Width = 120
        '
        'Controlmethod
        '
        Me.Controlmethod.DataPropertyName = "control_method"
        Me.Controlmethod.FillWeight = 99.89859!
        Me.Controlmethod.HeaderText = "控制方法"
        Me.Controlmethod.Name = "Controlmethod"
        Me.Controlmethod.Width = 120
        '
        'Diangan
        '
        Me.Diangan.DataPropertyName = "diangan"
        Me.Diangan.FillWeight = 99.83366!
        Me.Diangan.HeaderText = "电感调光"
        Me.Diangan.Name = "Diangan"
        Me.Diangan.Width = 99
        '
        'power
        '
        Me.power.DataPropertyName = "power"
        Me.power.FillWeight = 100.2865!
        Me.power.HeaderText = "电子调光"
        Me.power.Name = "power"
        '
        'Controltime
        '
        Me.Controltime.DataPropertyName = "control_time"
        Me.Controltime.FillWeight = 99.77804!
        Me.Controltime.HeaderText = "操作时间"
        Me.Controltime.Name = "Controltime"
        Me.Controltime.Width = 99
        '
        'HandcontrolrecordBindingSource
        '
        Me.HandcontrolrecordBindingSource.DataMember = "hand_control_record"
        Me.HandcontrolrecordBindingSource.DataSource = Me.Hand_record
        '
        'Hand_record
        '
        Me.Hand_record.DataSetName = "hand_record"
        Me.Hand_record.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'BackgroundWorker1
        '
        Me.BackgroundWorker1.WorkerReportsProgress = True
        Me.BackgroundWorker1.WorkerSupportsCancellation = True
        '
        'rb_no_date_condition_find
        '
        Me.rb_no_date_condition_find.AutoSize = True
        Me.rb_no_date_condition_find.BackColor = System.Drawing.Color.Transparent
        Me.rb_no_date_condition_find.Location = New System.Drawing.Point(124, 20)
        Me.rb_no_date_condition_find.Name = "rb_no_date_condition_find"
        Me.rb_no_date_condition_find.Size = New System.Drawing.Size(83, 16)
        Me.rb_no_date_condition_find.TabIndex = 111
        Me.rb_no_date_condition_find.Text = "无日期查询"
        Me.rb_no_date_condition_find.UseVisualStyleBackColor = False
        '
        'rb_date_condition_find
        '
        Me.rb_date_condition_find.AutoSize = True
        Me.rb_date_condition_find.BackColor = System.Drawing.Color.Transparent
        Me.rb_date_condition_find.Checked = True
        Me.rb_date_condition_find.Location = New System.Drawing.Point(20, 20)
        Me.rb_date_condition_find.Name = "rb_date_condition_find"
        Me.rb_date_condition_find.Size = New System.Drawing.Size(83, 16)
        Me.rb_date_condition_find.TabIndex = 110
        Me.rb_date_condition_find.TabStop = True
        Me.rb_date_condition_find.Text = "按日期查询"
        Me.rb_date_condition_find.UseVisualStyleBackColor = False
        '
        'dtp_date_end
        '
        Me.dtp_date_end.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_date_end.Location = New System.Drawing.Point(106, 80)
        Me.dtp_date_end.Name = "dtp_date_end"
        Me.dtp_date_end.Size = New System.Drawing.Size(118, 21)
        Me.dtp_date_end.TabIndex = 109
        '
        'dtp_date_start
        '
        Me.dtp_date_start.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_date_start.Location = New System.Drawing.Point(106, 46)
        Me.dtp_date_start.Name = "dtp_date_start"
        Me.dtp_date_start.Size = New System.Drawing.Size(118, 21)
        Me.dtp_date_start.TabIndex = 108
        '
        'Date_end_string
        '
        Me.Date_end_string.AutoSize = True
        Me.Date_end_string.BackColor = System.Drawing.Color.Transparent
        Me.Date_end_string.Location = New System.Drawing.Point(35, 84)
        Me.Date_end_string.Name = "Date_end_string"
        Me.Date_end_string.Size = New System.Drawing.Size(65, 12)
        Me.Date_end_string.TabIndex = 107
        Me.Date_end_string.Text = "结束日期："
        '
        'Date_start_string
        '
        Me.Date_start_string.AutoSize = True
        Me.Date_start_string.BackColor = System.Drawing.Color.Transparent
        Me.Date_start_string.Location = New System.Drawing.Point(35, 50)
        Me.Date_start_string.Name = "Date_start_string"
        Me.Date_start_string.Size = New System.Drawing.Size(65, 12)
        Me.Date_start_string.TabIndex = 106
        Me.Date_start_string.Text = "开始日期："
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.rb_date_condition_find)
        Me.GroupBox1.Controls.Add(Me.dtp_date_end)
        Me.GroupBox1.Controls.Add(Me.rb_no_date_condition_find)
        Me.GroupBox1.Controls.Add(Me.Date_end_string)
        Me.GroupBox1.Controls.Add(Me.dtp_date_start)
        Me.GroupBox1.Controls.Add(Me.Date_start_string)
        Me.GroupBox1.Location = New System.Drawing.Point(7, 55)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(241, 111)
        Me.GroupBox1.TabIndex = 112
        Me.GroupBox1.TabStop = False
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.Controls.Add(Me.rb_area_control)
        Me.GroupBox2.Controls.Add(Me.rb_street_control)
        Me.GroupBox2.Controls.Add(Me.rb_city_control)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.cb_city_name)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.cb_area_name)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.cb_street_name)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.lamp_type_id)
        Me.GroupBox2.Controls.Add(Me.lamp_id_start)
        Me.GroupBox2.Controls.Add(Me.rb_lamp_type_control)
        Me.GroupBox2.Controls.Add(Me.cb_lamp_type)
        Me.GroupBox2.Controls.Add(Me.rb_control_box_name_control)
        Me.GroupBox2.Controls.Add(Me.rb_lamp_id_control)
        Me.GroupBox2.Controls.Add(Me.lamp_id_string)
        Me.GroupBox2.Controls.Add(Me.cb_lamp_id)
        Me.GroupBox2.Controls.Add(Me.control_box_string)
        Me.GroupBox2.Controls.Add(Me.cb_control_box_name)
        Me.GroupBox2.Location = New System.Drawing.Point(7, 165)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(241, 269)
        Me.GroupBox2.TabIndex = 113
        Me.GroupBox2.TabStop = False
        '
        'rb_area_control
        '
        Me.rb_area_control.AutoSize = True
        Me.rb_area_control.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.rb_area_control.Location = New System.Drawing.Point(136, 13)
        Me.rb_area_control.Name = "rb_area_control"
        Me.rb_area_control.Size = New System.Drawing.Size(71, 16)
        Me.rb_area_control.TabIndex = 178
        Me.rb_area_control.Text = "区域名称"
        Me.rb_area_control.UseVisualStyleBackColor = True
        '
        'rb_street_control
        '
        Me.rb_street_control.AutoSize = True
        Me.rb_street_control.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.rb_street_control.Location = New System.Drawing.Point(22, 32)
        Me.rb_street_control.Name = "rb_street_control"
        Me.rb_street_control.Size = New System.Drawing.Size(71, 16)
        Me.rb_street_control.TabIndex = 177
        Me.rb_street_control.Text = "乡镇名称"
        Me.rb_street_control.UseVisualStyleBackColor = True
        '
        'rb_city_control
        '
        Me.rb_city_control.AutoSize = True
        Me.rb_city_control.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.rb_city_control.Location = New System.Drawing.Point(22, 11)
        Me.rb_city_control.Name = "rb_city_control"
        Me.rb_city_control.Size = New System.Drawing.Size(71, 16)
        Me.rb_city_control.TabIndex = 176
        Me.rb_city_control.Text = "城市名称"
        Me.rb_city_control.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(17, 79)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(65, 12)
        Me.Label4.TabIndex = 174
        Me.Label4.Text = "城市名称："
        '
        'cb_city_name
        '
        Me.cb_city_name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_city_name.FormattingEnabled = True
        Me.cb_city_name.Location = New System.Drawing.Point(91, 76)
        Me.cb_city_name.Name = "cb_city_name"
        Me.cb_city_name.Size = New System.Drawing.Size(118, 20)
        Me.cb_city_name.TabIndex = 175
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(17, 112)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 12)
        Me.Label3.TabIndex = 172
        Me.Label3.Text = "区域名称："
        '
        'cb_area_name
        '
        Me.cb_area_name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_area_name.FormattingEnabled = True
        Me.cb_area_name.Location = New System.Drawing.Point(91, 110)
        Me.cb_area_name.Name = "cb_area_name"
        Me.cb_area_name.Size = New System.Drawing.Size(118, 20)
        Me.cb_area_name.TabIndex = 173
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(17, 144)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 12)
        Me.Label2.TabIndex = 170
        Me.Label2.Text = "乡镇名称："
        '
        'cb_street_name
        '
        Me.cb_street_name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_street_name.FormattingEnabled = True
        Me.cb_street_name.Location = New System.Drawing.Point(91, 141)
        Me.cb_street_name.Name = "cb_street_name"
        Me.cb_street_name.Size = New System.Drawing.Size(118, 20)
        Me.cb_street_name.TabIndex = 171
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(17, 210)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 12)
        Me.Label1.TabIndex = 169
        Me.Label1.Text = "类型名称："
        '
        'lamp_type_id
        '
        Me.lamp_type_id.AutoSize = True
        Me.lamp_type_id.Location = New System.Drawing.Point(20, 254)
        Me.lamp_type_id.Name = "lamp_type_id"
        Me.lamp_type_id.Size = New System.Drawing.Size(53, 12)
        Me.lamp_type_id.TabIndex = 168
        Me.lamp_type_id.Text = "灯的类型"
        '
        'lamp_id_start
        '
        Me.lamp_id_start.AutoSize = True
        Me.lamp_id_start.Location = New System.Drawing.Point(110, 254)
        Me.lamp_id_start.Name = "lamp_id_start"
        Me.lamp_id_start.Size = New System.Drawing.Size(113, 12)
        Me.lamp_id_start.TabIndex = 117
        Me.lamp_id_start.Text = "景观灯编号前半部分"
        '
        'rb_lamp_type_control
        '
        Me.rb_lamp_type_control.AutoSize = True
        Me.rb_lamp_type_control.BackColor = System.Drawing.Color.Transparent
        Me.rb_lamp_type_control.Location = New System.Drawing.Point(22, 54)
        Me.rb_lamp_type_control.Name = "rb_lamp_type_control"
        Me.rb_lamp_type_control.Size = New System.Drawing.Size(71, 16)
        Me.rb_lamp_type_control.TabIndex = 116
        Me.rb_lamp_type_control.Text = "类型名称"
        Me.rb_lamp_type_control.UseVisualStyleBackColor = False
        '
        'cb_lamp_type
        '
        Me.cb_lamp_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_lamp_type.FormattingEnabled = True
        Me.cb_lamp_type.Location = New System.Drawing.Point(91, 203)
        Me.cb_lamp_type.Name = "cb_lamp_type"
        Me.cb_lamp_type.Size = New System.Drawing.Size(118, 20)
        Me.cb_lamp_type.TabIndex = 115
        '
        'rb_control_box_name_control
        '
        Me.rb_control_box_name_control.AutoSize = True
        Me.rb_control_box_name_control.BackColor = System.Drawing.Color.Transparent
        Me.rb_control_box_name_control.Location = New System.Drawing.Point(136, 33)
        Me.rb_control_box_name_control.Name = "rb_control_box_name_control"
        Me.rb_control_box_name_control.Size = New System.Drawing.Size(71, 16)
        Me.rb_control_box_name_control.TabIndex = 112
        Me.rb_control_box_name_control.Text = "网关名称"
        Me.rb_control_box_name_control.UseVisualStyleBackColor = False
        '
        'rb_lamp_id_control
        '
        Me.rb_lamp_id_control.AutoSize = True
        Me.rb_lamp_id_control.BackColor = System.Drawing.Color.Transparent
        Me.rb_lamp_id_control.Checked = True
        Me.rb_lamp_id_control.Location = New System.Drawing.Point(136, 54)
        Me.rb_lamp_id_control.Name = "rb_lamp_id_control"
        Me.rb_lamp_id_control.Size = New System.Drawing.Size(71, 16)
        Me.rb_lamp_id_control.TabIndex = 113
        Me.rb_lamp_id_control.TabStop = True
        Me.rb_lamp_id_control.Text = "终端代码"
        Me.rb_lamp_id_control.UseVisualStyleBackColor = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox3.Controls.Add(Me.dgv_hand_control_list)
        Me.GroupBox3.Location = New System.Drawing.Point(253, 0)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(512, 447)
        Me.GroupBox3.TabIndex = 114
        Me.GroupBox3.TabStop = False
        '
        'bt_excel_table
        '
        Me.bt_excel_table.Location = New System.Drawing.Point(183, 439)
        Me.bt_excel_table.Name = "bt_excel_table"
        Me.bt_excel_table.Size = New System.Drawing.Size(64, 20)
        Me.bt_excel_table.TabIndex = 115
        Me.bt_excel_table.Text = "导出报表"
        Me.bt_excel_table.UseVisualStyleBackColor = True
        '
        'bt_getall
        '
        Me.bt_getall.Location = New System.Drawing.Point(7, 439)
        Me.bt_getall.Name = "bt_getall"
        Me.bt_getall.Size = New System.Drawing.Size(64, 20)
        Me.bt_getall.TabIndex = 116
        Me.bt_getall.Text = "刷新"
        Me.bt_getall.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(0, 2)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(777, 487)
        Me.TabControl1.TabIndex = 117
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.GroupBox4)
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.Controls.Add(Me.bt_excel_table)
        Me.TabPage1.Controls.Add(Me.bt_getall)
        Me.TabPage1.Controls.Add(Me.bt_find_record)
        Me.TabPage1.Controls.Add(Me.GroupBox3)
        Me.TabPage1.Controls.Add(Me.GroupBox2)
        Me.TabPage1.Location = New System.Drawing.Point(4, 21)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(769, 462)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "系统控制日志"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.rb_divtime_check)
        Me.GroupBox4.Controls.Add(Me.rb_hand_check)
        Me.GroupBox4.Location = New System.Drawing.Point(7, 3)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(241, 51)
        Me.GroupBox4.TabIndex = 117
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "控制类型"
        '
        'rb_divtime_check
        '
        Me.rb_divtime_check.AutoSize = True
        Me.rb_divtime_check.Location = New System.Drawing.Point(130, 19)
        Me.rb_divtime_check.Name = "rb_divtime_check"
        Me.rb_divtime_check.Size = New System.Drawing.Size(71, 16)
        Me.rb_divtime_check.TabIndex = 1
        Me.rb_divtime_check.Text = "时段控制"
        Me.rb_divtime_check.UseVisualStyleBackColor = True
        '
        'rb_hand_check
        '
        Me.rb_hand_check.AutoSize = True
        Me.rb_hand_check.Checked = True
        Me.rb_hand_check.Location = New System.Drawing.Point(20, 19)
        Me.rb_hand_check.Name = "rb_hand_check"
        Me.rb_hand_check.Size = New System.Drawing.Size(71, 16)
        Me.rb_hand_check.TabIndex = 0
        Me.rb_hand_check.TabStop = True
        Me.rb_hand_check.Text = "手工控制"
        Me.rb_hand_check.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.bt_operation_excel)
        Me.TabPage2.Controls.Add(Me.bt_operation_all)
        Me.TabPage2.Controls.Add(Me.bt_operation_check)
        Me.TabPage2.Controls.Add(Me.rb_check_nousername)
        Me.TabPage2.Controls.Add(Me.rb_check_username)
        Me.TabPage2.Controls.Add(Me.cb_usernamelist)
        Me.TabPage2.Controls.Add(Me.Label7)
        Me.TabPage2.Controls.Add(Me.dgv_operation_record)
        Me.TabPage2.Controls.Add(Me.dtp_operation_endtime)
        Me.TabPage2.Controls.Add(Me.dtp_operation_starttime)
        Me.TabPage2.Controls.Add(Me.Label6)
        Me.TabPage2.Controls.Add(Me.Label5)
        Me.TabPage2.Location = New System.Drawing.Point(4, 21)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(769, 462)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "系统操作日志"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'bt_operation_excel
        '
        Me.bt_operation_excel.Location = New System.Drawing.Point(141, 201)
        Me.bt_operation_excel.Name = "bt_operation_excel"
        Me.bt_operation_excel.Size = New System.Drawing.Size(64, 20)
        Me.bt_operation_excel.TabIndex = 118
        Me.bt_operation_excel.Text = "导出报表"
        Me.bt_operation_excel.UseVisualStyleBackColor = True
        '
        'bt_operation_all
        '
        Me.bt_operation_all.Location = New System.Drawing.Point(3, 201)
        Me.bt_operation_all.Name = "bt_operation_all"
        Me.bt_operation_all.Size = New System.Drawing.Size(64, 20)
        Me.bt_operation_all.TabIndex = 117
        Me.bt_operation_all.Text = "刷新"
        Me.bt_operation_all.UseVisualStyleBackColor = True
        '
        'bt_operation_check
        '
        Me.bt_operation_check.Location = New System.Drawing.Point(72, 201)
        Me.bt_operation_check.Name = "bt_operation_check"
        Me.bt_operation_check.Size = New System.Drawing.Size(64, 20)
        Me.bt_operation_check.TabIndex = 115
        Me.bt_operation_check.Text = "查询"
        Me.bt_operation_check.UseVisualStyleBackColor = True
        '
        'rb_check_nousername
        '
        Me.rb_check_nousername.AutoSize = True
        Me.rb_check_nousername.Location = New System.Drawing.Point(108, 26)
        Me.rb_check_nousername.Name = "rb_check_nousername"
        Me.rb_check_nousername.Size = New System.Drawing.Size(95, 16)
        Me.rb_check_nousername.TabIndex = 114
        Me.rb_check_nousername.Text = "无用户名查询"
        Me.rb_check_nousername.UseVisualStyleBackColor = True
        '
        'rb_check_username
        '
        Me.rb_check_username.AutoSize = True
        Me.rb_check_username.Checked = True
        Me.rb_check_username.Location = New System.Drawing.Point(7, 26)
        Me.rb_check_username.Name = "rb_check_username"
        Me.rb_check_username.Size = New System.Drawing.Size(95, 16)
        Me.rb_check_username.TabIndex = 113
        Me.rb_check_username.TabStop = True
        Me.rb_check_username.Text = "按用户名查询"
        Me.rb_check_username.UseVisualStyleBackColor = True
        '
        'cb_usernamelist
        '
        Me.cb_usernamelist.FormattingEnabled = True
        Me.cb_usernamelist.Location = New System.Drawing.Point(81, 69)
        Me.cb_usernamelist.Name = "cb_usernamelist"
        Me.cb_usernamelist.Size = New System.Drawing.Size(120, 20)
        Me.cb_usernamelist.TabIndex = 112
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(20, 72)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(47, 12)
        Me.Label7.TabIndex = 111
        Me.Label7.Text = "用户名:"
        '
        'dgv_operation_record
        '
        Me.dgv_operation_record.AllowUserToAddRows = False
        Me.dgv_operation_record.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv_operation_record.AutoGenerateColumns = False
        Me.dgv_operation_record.BackgroundColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_operation_record.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dgv_operation_record.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_operation_record.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.operation_username, Me.operation_string, Me.operation_time})
        Me.dgv_operation_record.DataSource = Me.OperationrecordBindingSource
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle4.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv_operation_record.DefaultCellStyle = DataGridViewCellStyle4
        Me.dgv_operation_record.Location = New System.Drawing.Point(211, 5)
        Me.dgv_operation_record.Name = "dgv_operation_record"
        Me.dgv_operation_record.RowTemplate.Height = 23
        Me.dgv_operation_record.Size = New System.Drawing.Size(554, 453)
        Me.dgv_operation_record.TabIndex = 110
        '
        'operation_username
        '
        Me.operation_username.DataPropertyName = "user_name"
        Me.operation_username.HeaderText = "用户名"
        Me.operation_username.Name = "operation_username"
        '
        'operation_string
        '
        Me.operation_string.DataPropertyName = "operation"
        Me.operation_string.HeaderText = "操作"
        Me.operation_string.Name = "operation_string"
        Me.operation_string.Width = 350
        '
        'operation_time
        '
        Me.operation_time.DataPropertyName = "createtime"
        Me.operation_time.HeaderText = "时间"
        Me.operation_time.Name = "operation_time"
        Me.operation_time.Width = 200
        '
        'OperationrecordBindingSource
        '
        Me.OperationrecordBindingSource.DataMember = "operation_record"
        Me.OperationrecordBindingSource.DataSource = Me.Operation_list
        '
        'Operation_list
        '
        Me.Operation_list.DataSetName = "operation_list"
        Me.Operation_list.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'dtp_operation_endtime
        '
        Me.dtp_operation_endtime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_operation_endtime.Location = New System.Drawing.Point(81, 138)
        Me.dtp_operation_endtime.Name = "dtp_operation_endtime"
        Me.dtp_operation_endtime.Size = New System.Drawing.Size(118, 21)
        Me.dtp_operation_endtime.TabIndex = 109
        '
        'dtp_operation_starttime
        '
        Me.dtp_operation_starttime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_operation_starttime.Location = New System.Drawing.Point(81, 105)
        Me.dtp_operation_starttime.Name = "dtp_operation_starttime"
        Me.dtp_operation_starttime.Size = New System.Drawing.Size(118, 21)
        Me.dtp_operation_starttime.TabIndex = 108
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Location = New System.Drawing.Point(9, 108)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(65, 12)
        Me.Label6.TabIndex = 106
        Me.Label6.Text = "开始日期："
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Location = New System.Drawing.Point(9, 142)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(65, 12)
        Me.Label5.TabIndex = 107
        Me.Label5.Text = "结束日期："
        '
        'Operation_recordTableAdapter
        '
        Me.Operation_recordTableAdapter.ClearBeforeFill = True
        '
        'Hand_control_recordTableAdapter
        '
        Me.Hand_control_recordTableAdapter.ClearBeforeFill = True
        '
        '手工控制记录
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(787, 513)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "手工控制记录"
        Me.Text = "系统日志"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        CType(Me.dgv_hand_control_list, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.HandcontrolrecordBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Hand_record, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        CType(Me.dgv_operation_record, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OperationrecordBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Operation_list, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cb_control_box_name As System.Windows.Forms.ComboBox
    Friend WithEvents control_box_string As System.Windows.Forms.Label
    Friend WithEvents cb_lamp_id As System.Windows.Forms.ComboBox
    Friend WithEvents lamp_id_string As System.Windows.Forms.Label
    Friend WithEvents bt_find_record As System.Windows.Forms.Button
    Friend WithEvents record_num As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents dgv_hand_control_list As System.Windows.Forms.DataGridView
    Friend WithEvents Hand_record As streetlamp.hand_record
    Friend WithEvents HandcontrolrecordBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents Hand_control_recordTableAdapter As streetlamp.hand_recordTableAdapters.hand_control_recordTableAdapter
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents rb_no_date_condition_find As System.Windows.Forms.RadioButton
    Friend WithEvents rb_date_condition_find As System.Windows.Forms.RadioButton
    Friend WithEvents dtp_date_end As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtp_date_start As System.Windows.Forms.DateTimePicker
    Friend WithEvents Date_end_string As System.Windows.Forms.Label
    Friend WithEvents Date_start_string As System.Windows.Forms.Label
    Friend WithEvents progress As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents rb_control_box_name_control As System.Windows.Forms.RadioButton
    Friend WithEvents rb_lamp_id_control As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents rb_lamp_type_control As System.Windows.Forms.RadioButton
    Friend WithEvents cb_lamp_type As System.Windows.Forms.ComboBox
    Friend WithEvents lamp_id_start As System.Windows.Forms.Label
    Friend WithEvents lamp_type_id As System.Windows.Forms.Label
    Friend WithEvents bt_excel_table As System.Windows.Forms.Button
    Friend WithEvents bt_getall As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cb_city_name As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cb_area_name As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cb_street_name As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents rb_area_control As System.Windows.Forms.RadioButton
    Friend WithEvents rb_street_control As System.Windows.Forms.RadioButton
    Friend WithEvents rb_city_control As System.Windows.Forms.RadioButton
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents dgv_operation_record As System.Windows.Forms.DataGridView
    Friend WithEvents dtp_operation_endtime As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtp_operation_starttime As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Operation_list As streetlamp.operation_list
    Friend WithEvents OperationrecordBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents Operation_recordTableAdapter As streetlamp.operation_listTableAdapters.operation_recordTableAdapter
    Friend WithEvents rb_check_username As System.Windows.Forms.RadioButton
    Friend WithEvents cb_usernamelist As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents rb_check_nousername As System.Windows.Forms.RadioButton
    Friend WithEvents bt_operation_check As System.Windows.Forms.Button
    Friend WithEvents bt_operation_all As System.Windows.Forms.Button
    Friend WithEvents bt_operation_excel As System.Windows.Forms.Button
    Friend WithEvents operation_username As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents operation_string As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents operation_time As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents rb_divtime_check As System.Windows.Forms.RadioButton
    Friend WithEvents rb_hand_check As System.Windows.Forms.RadioButton
    Friend WithEvents user_name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Controlcontent As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Contentname As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Controlmethod As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Diangan As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents power As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Controltime As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
