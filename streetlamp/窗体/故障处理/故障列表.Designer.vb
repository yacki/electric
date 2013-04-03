<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 故障列表
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(故障列表))
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.record_num = New System.Windows.Forms.ToolStripStatusLabel()
        Me.progress = New System.Windows.Forms.ToolStripProgressBar()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.dgv_problem_list = New System.Windows.Forms.DataGridView()
        Me.alarm_id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.control_box_name = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.type_string = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lamp_id_short = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.problem_inf = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.date_start = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Date_end = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.alarm_ok = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.server_state = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AlarmviewBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.StreetlampDataSet = New streetlamp.streetlampDataSet()
        Me.bt_find_record = New System.Windows.Forms.Button()
        Me.cb_lamp_id = New System.Windows.Forms.ComboBox()
        Me.pro_lamp_id_string = New System.Windows.Forms.Label()
        Me.cb_control_box_name = New System.Windows.Forms.ComboBox()
        Me.pro_box_string = New System.Windows.Forms.Label()
        Me.Date_start_string = New System.Windows.Forms.Label()
        Me.Date_end_string = New System.Windows.Forms.Label()
        Me.dtp_date_start = New System.Windows.Forms.DateTimePicker()
        Me.dtp_date_end = New System.Windows.Forms.DateTimePicker()
        Me.rb_date_condition_find = New System.Windows.Forms.RadioButton()
        Me.rb_no_date_condition_find = New System.Windows.Forms.RadioButton()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.cb_lamp_type = New System.Windows.Forms.ComboBox()
        Me.lb_lamp_id_start = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.rb_city_control = New System.Windows.Forms.RadioButton()
        Me.rb_area_control = New System.Windows.Forms.RadioButton()
        Me.rb_street_control = New System.Windows.Forms.RadioButton()
        Me.rb_lamp_type_control = New System.Windows.Forms.RadioButton()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cb_city_name = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cb_street_name = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cb_area_name = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.alarm_okornot = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.rb_control_box_name_control = New System.Windows.Forms.RadioButton()
        Me.rb_lamp_id_control = New System.Windows.Forms.RadioButton()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.bt_excel_data = New System.Windows.Forms.Button()
        Me.bt_reflash = New System.Windows.Forms.Button()
        Me.Alarm_viewTableAdapter = New streetlamp.streetlampDataSetTableAdapters.alarm_viewTableAdapter()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.lb_lamp_type_id = New System.Windows.Forms.Label()
        Me.KaiguanalarmlistBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.KaiguanlistDataSet = New streetlamp.kaiguanlistDataSet()
        Me.Kaiguan_alarm_listTableAdapter = New streetlamp.kaiguanlistDataSetTableAdapters.kaiguan_alarm_listTableAdapter()
        Me.StatusStrip1.SuspendLayout()
        CType(Me.dgv_problem_list, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AlarmviewBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StreetlampDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.KaiguanalarmlistBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.KaiguanlistDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.record_num, Me.progress})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 548)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(866, 22)
        Me.StatusStrip1.TabIndex = 5
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'record_num
        '
        Me.record_num.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.record_num.Name = "record_num"
        Me.record_num.Size = New System.Drawing.Size(77, 17)
        Me.record_num.Text = "报警信息表"
        '
        'progress
        '
        Me.progress.Name = "progress"
        Me.progress.Size = New System.Drawing.Size(200, 16)
        Me.progress.Visible = False
        '
        'dgv_problem_list
        '
        Me.dgv_problem_list.AllowUserToAddRows = False
        Me.dgv_problem_list.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv_problem_list.BackgroundColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.dgv_problem_list.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv_problem_list.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_problem_list.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.alarm_id, Me.control_box_name, Me.type_string, Me.lamp_id_short, Me.problem_inf, Me.date_start, Me.Date_end, Me.alarm_ok, Me.server_state})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv_problem_list.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgv_problem_list.Location = New System.Drawing.Point(10, 11)
        Me.dgv_problem_list.Name = "dgv_problem_list"
        Me.dgv_problem_list.RowHeadersVisible = False
        Me.dgv_problem_list.RowTemplate.Height = 23
        Me.dgv_problem_list.Size = New System.Drawing.Size(563, 501)
        Me.dgv_problem_list.TabIndex = 7
        '
        'alarm_id
        '
        Me.alarm_id.DataPropertyName = "ID"
        Me.alarm_id.HeaderText = "编号"
        Me.alarm_id.Name = "alarm_id"
        Me.alarm_id.Width = 60
        '
        'control_box_name
        '
        Me.control_box_name.DataPropertyName = "control_box_name"
        Me.control_box_name.HeaderText = "主控箱名称"
        Me.control_box_name.Name = "control_box_name"
        '
        'type_string
        '
        Me.type_string.HeaderText = "节点类型"
        Me.type_string.Name = "type_string"
        '
        'lamp_id_short
        '
        Me.lamp_id_short.DataPropertyName = "lamp_id_short"
        Me.lamp_id_short.HeaderText = "节点编号"
        Me.lamp_id_short.Name = "lamp_id_short"
        Me.lamp_id_short.ReadOnly = True
        '
        'problem_inf
        '
        Me.problem_inf.DataPropertyName = "problem_inf"
        Me.problem_inf.HeaderText = "故障原因"
        Me.problem_inf.Name = "problem_inf"
        '
        'date_start
        '
        Me.date_start.HeaderText = "开始日期"
        Me.date_start.Name = "date_start"
        '
        'Date_end
        '
        Me.Date_end.DataPropertyName = "date_end"
        Me.Date_end.HeaderText = "结束日期"
        Me.Date_end.Name = "Date_end"
        '
        'alarm_ok
        '
        Me.alarm_ok.HeaderText = "报警是否处理"
        Me.alarm_ok.Name = "alarm_ok"
        Me.alarm_ok.Width = 120
        '
        'server_state
        '
        Me.server_state.DataPropertyName = "server_state"
        Me.server_state.HeaderText = "server_state"
        Me.server_state.Name = "server_state"
        Me.server_state.Visible = False
        '
        'AlarmviewBindingSource
        '
        Me.AlarmviewBindingSource.DataMember = "alarm_view"
        Me.AlarmviewBindingSource.DataSource = Me.StreetlampDataSet
        '
        'StreetlampDataSet
        '
        Me.StreetlampDataSet.DataSetName = "streetlampDataSet"
        Me.StreetlampDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'bt_find_record
        '
        Me.bt_find_record.Location = New System.Drawing.Point(9, 489)
        Me.bt_find_record.Name = "bt_find_record"
        Me.bt_find_record.Size = New System.Drawing.Size(75, 23)
        Me.bt_find_record.TabIndex = 96
        Me.bt_find_record.Text = "查询"
        Me.bt_find_record.UseVisualStyleBackColor = True
        '
        'cb_lamp_id
        '
        Me.cb_lamp_id.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_lamp_id.FormattingEnabled = True
        Me.cb_lamp_id.Location = New System.Drawing.Point(126, 239)
        Me.cb_lamp_id.Name = "cb_lamp_id"
        Me.cb_lamp_id.Size = New System.Drawing.Size(102, 20)
        Me.cb_lamp_id.TabIndex = 95
        '
        'pro_lamp_id_string
        '
        Me.pro_lamp_id_string.AutoSize = True
        Me.pro_lamp_id_string.BackColor = System.Drawing.Color.Transparent
        Me.pro_lamp_id_string.Location = New System.Drawing.Point(32, 246)
        Me.pro_lamp_id_string.Name = "pro_lamp_id_string"
        Me.pro_lamp_id_string.Size = New System.Drawing.Size(65, 12)
        Me.pro_lamp_id_string.TabIndex = 94
        Me.pro_lamp_id_string.Text = "终端编号："
        '
        'cb_control_box_name
        '
        Me.cb_control_box_name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_control_box_name.FormattingEnabled = True
        Me.cb_control_box_name.Location = New System.Drawing.Point(126, 176)
        Me.cb_control_box_name.Name = "cb_control_box_name"
        Me.cb_control_box_name.Size = New System.Drawing.Size(102, 20)
        Me.cb_control_box_name.TabIndex = 93
        '
        'pro_box_string
        '
        Me.pro_box_string.AutoSize = True
        Me.pro_box_string.BackColor = System.Drawing.Color.Transparent
        Me.pro_box_string.Location = New System.Drawing.Point(20, 181)
        Me.pro_box_string.Name = "pro_box_string"
        Me.pro_box_string.Size = New System.Drawing.Size(77, 12)
        Me.pro_box_string.TabIndex = 92
        Me.pro_box_string.Text = "主控箱名称："
        '
        'Date_start_string
        '
        Me.Date_start_string.AutoSize = True
        Me.Date_start_string.BackColor = System.Drawing.Color.Transparent
        Me.Date_start_string.Location = New System.Drawing.Point(12, 57)
        Me.Date_start_string.Name = "Date_start_string"
        Me.Date_start_string.Size = New System.Drawing.Size(65, 12)
        Me.Date_start_string.TabIndex = 100
        Me.Date_start_string.Text = "开始日期："
        '
        'Date_end_string
        '
        Me.Date_end_string.AutoSize = True
        Me.Date_end_string.BackColor = System.Drawing.Color.Transparent
        Me.Date_end_string.Location = New System.Drawing.Point(12, 93)
        Me.Date_end_string.Name = "Date_end_string"
        Me.Date_end_string.Size = New System.Drawing.Size(65, 12)
        Me.Date_end_string.TabIndex = 101
        Me.Date_end_string.Text = "结束日期："
        '
        'dtp_date_start
        '
        Me.dtp_date_start.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_date_start.Location = New System.Drawing.Point(83, 53)
        Me.dtp_date_start.Name = "dtp_date_start"
        Me.dtp_date_start.Size = New System.Drawing.Size(146, 21)
        Me.dtp_date_start.TabIndex = 102
        '
        'dtp_date_end
        '
        Me.dtp_date_end.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_date_end.Location = New System.Drawing.Point(83, 84)
        Me.dtp_date_end.Name = "dtp_date_end"
        Me.dtp_date_end.Size = New System.Drawing.Size(146, 21)
        Me.dtp_date_end.TabIndex = 103
        '
        'rb_date_condition_find
        '
        Me.rb_date_condition_find.AutoSize = True
        Me.rb_date_condition_find.BackColor = System.Drawing.Color.Transparent
        Me.rb_date_condition_find.Checked = True
        Me.rb_date_condition_find.Location = New System.Drawing.Point(15, 20)
        Me.rb_date_condition_find.Name = "rb_date_condition_find"
        Me.rb_date_condition_find.Size = New System.Drawing.Size(83, 16)
        Me.rb_date_condition_find.TabIndex = 104
        Me.rb_date_condition_find.TabStop = True
        Me.rb_date_condition_find.Text = "按日期查询"
        Me.rb_date_condition_find.UseVisualStyleBackColor = False
        '
        'rb_no_date_condition_find
        '
        Me.rb_no_date_condition_find.AutoSize = True
        Me.rb_no_date_condition_find.BackColor = System.Drawing.Color.Transparent
        Me.rb_no_date_condition_find.Location = New System.Drawing.Point(147, 20)
        Me.rb_no_date_condition_find.Name = "rb_no_date_condition_find"
        Me.rb_no_date_condition_find.Size = New System.Drawing.Size(83, 16)
        Me.rb_no_date_condition_find.TabIndex = 105
        Me.rb_no_date_condition_find.Text = "无日期查询"
        Me.rb_no_date_condition_find.UseVisualStyleBackColor = False
        '
        'BackgroundWorker1
        '
        Me.BackgroundWorker1.WorkerReportsProgress = True
        Me.BackgroundWorker1.WorkerSupportsCancellation = True
        '
        'cb_lamp_type
        '
        Me.cb_lamp_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_lamp_type.FormattingEnabled = True
        Me.cb_lamp_type.Location = New System.Drawing.Point(126, 207)
        Me.cb_lamp_type.Name = "cb_lamp_type"
        Me.cb_lamp_type.Size = New System.Drawing.Size(102, 20)
        Me.cb_lamp_type.TabIndex = 107
        '
        'lb_lamp_id_start
        '
        Me.lb_lamp_id_start.AutoSize = True
        Me.lb_lamp_id_start.BackColor = System.Drawing.Color.Transparent
        Me.lb_lamp_id_start.Location = New System.Drawing.Point(128, 302)
        Me.lb_lamp_id_start.Name = "lb_lamp_id_start"
        Me.lb_lamp_id_start.Size = New System.Drawing.Size(125, 12)
        Me.lb_lamp_id_start.TabIndex = 108
        Me.lb_lamp_id_start.Text = "景观灯编号的前半部分"
        Me.lb_lamp_id_start.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.rb_date_condition_find)
        Me.GroupBox1.Controls.Add(Me.rb_no_date_condition_find)
        Me.GroupBox1.Controls.Add(Me.Date_start_string)
        Me.GroupBox1.Controls.Add(Me.dtp_date_start)
        Me.GroupBox1.Controls.Add(Me.dtp_date_end)
        Me.GroupBox1.Controls.Add(Me.Date_end_string)
        Me.GroupBox1.Location = New System.Drawing.Point(5, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(255, 116)
        Me.GroupBox1.TabIndex = 109
        Me.GroupBox1.TabStop = False
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.Controls.Add(Me.rb_city_control)
        Me.GroupBox2.Controls.Add(Me.rb_area_control)
        Me.GroupBox2.Controls.Add(Me.rb_street_control)
        Me.GroupBox2.Controls.Add(Me.rb_lamp_type_control)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.cb_city_name)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.cb_street_name)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.cb_area_name)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.alarm_okornot)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.rb_control_box_name_control)
        Me.GroupBox2.Controls.Add(Me.lb_lamp_id_start)
        Me.GroupBox2.Controls.Add(Me.rb_lamp_id_control)
        Me.GroupBox2.Controls.Add(Me.cb_lamp_type)
        Me.GroupBox2.Controls.Add(Me.pro_box_string)
        Me.GroupBox2.Controls.Add(Me.cb_lamp_id)
        Me.GroupBox2.Controls.Add(Me.cb_control_box_name)
        Me.GroupBox2.Controls.Add(Me.pro_lamp_id_string)
        Me.GroupBox2.Location = New System.Drawing.Point(5, 126)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(255, 337)
        Me.GroupBox2.TabIndex = 110
        Me.GroupBox2.TabStop = False
        '
        'rb_city_control
        '
        Me.rb_city_control.AutoSize = True
        Me.rb_city_control.BackColor = System.Drawing.Color.Transparent
        Me.rb_city_control.Location = New System.Drawing.Point(28, 15)
        Me.rb_city_control.Name = "rb_city_control"
        Me.rb_city_control.Size = New System.Drawing.Size(71, 16)
        Me.rb_city_control.TabIndex = 131
        Me.rb_city_control.Text = "城市名称"
        Me.rb_city_control.UseVisualStyleBackColor = False
        '
        'rb_area_control
        '
        Me.rb_area_control.AutoSize = True
        Me.rb_area_control.BackColor = System.Drawing.Color.Transparent
        Me.rb_area_control.Location = New System.Drawing.Point(141, 15)
        Me.rb_area_control.Name = "rb_area_control"
        Me.rb_area_control.Size = New System.Drawing.Size(71, 16)
        Me.rb_area_control.TabIndex = 130
        Me.rb_area_control.Text = "区域名称"
        Me.rb_area_control.UseVisualStyleBackColor = False
        '
        'rb_street_control
        '
        Me.rb_street_control.AutoSize = True
        Me.rb_street_control.BackColor = System.Drawing.Color.Transparent
        Me.rb_street_control.Location = New System.Drawing.Point(28, 35)
        Me.rb_street_control.Name = "rb_street_control"
        Me.rb_street_control.Size = New System.Drawing.Size(71, 16)
        Me.rb_street_control.TabIndex = 129
        Me.rb_street_control.Text = "街道名称"
        Me.rb_street_control.UseVisualStyleBackColor = False
        '
        'rb_lamp_type_control
        '
        Me.rb_lamp_type_control.AutoSize = True
        Me.rb_lamp_type_control.BackColor = System.Drawing.Color.Transparent
        Me.rb_lamp_type_control.Location = New System.Drawing.Point(28, 56)
        Me.rb_lamp_type_control.Name = "rb_lamp_type_control"
        Me.rb_lamp_type_control.Size = New System.Drawing.Size(71, 16)
        Me.rb_lamp_type_control.TabIndex = 128
        Me.rb_lamp_type_control.Text = "类型名称"
        Me.rb_lamp_type_control.UseVisualStyleBackColor = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Location = New System.Drawing.Point(32, 83)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(65, 12)
        Me.Label5.TabIndex = 126
        Me.Label5.Text = "城市名称："
        '
        'cb_city_name
        '
        Me.cb_city_name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_city_name.FormattingEnabled = True
        Me.cb_city_name.Location = New System.Drawing.Point(126, 81)
        Me.cb_city_name.Name = "cb_city_name"
        Me.cb_city_name.Size = New System.Drawing.Size(102, 20)
        Me.cb_city_name.TabIndex = 127
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(32, 152)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(65, 12)
        Me.Label4.TabIndex = 124
        Me.Label4.Text = "街道名称："
        '
        'cb_street_name
        '
        Me.cb_street_name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_street_name.FormattingEnabled = True
        Me.cb_street_name.Location = New System.Drawing.Point(126, 145)
        Me.cb_street_name.Name = "cb_street_name"
        Me.cb_street_name.Size = New System.Drawing.Size(102, 20)
        Me.cb_street_name.TabIndex = 125
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(32, 118)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 12)
        Me.Label3.TabIndex = 122
        Me.Label3.Text = "区域名称："
        '
        'cb_area_name
        '
        Me.cb_area_name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_area_name.FormattingEnabled = True
        Me.cb_area_name.Location = New System.Drawing.Point(125, 116)
        Me.cb_area_name.Name = "cb_area_name"
        Me.cb_area_name.Size = New System.Drawing.Size(102, 20)
        Me.cb_area_name.TabIndex = 123
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(32, 213)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 12)
        Me.Label2.TabIndex = 121
        Me.Label2.Text = "类型名称："
        '
        'alarm_okornot
        '
        Me.alarm_okornot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.alarm_okornot.FormattingEnabled = True
        Me.alarm_okornot.Items.AddRange(New Object() {"是", "否", "全部"})
        Me.alarm_okornot.Location = New System.Drawing.Point(126, 271)
        Me.alarm_okornot.Name = "alarm_okornot"
        Me.alarm_okornot.Size = New System.Drawing.Size(102, 20)
        Me.alarm_okornot.TabIndex = 120
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(32, 279)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 12)
        Me.Label1.TabIndex = 119
        Me.Label1.Text = "是否处理："
        '
        'rb_control_box_name_control
        '
        Me.rb_control_box_name_control.AutoSize = True
        Me.rb_control_box_name_control.BackColor = System.Drawing.Color.Transparent
        Me.rb_control_box_name_control.Location = New System.Drawing.Point(141, 35)
        Me.rb_control_box_name_control.Name = "rb_control_box_name_control"
        Me.rb_control_box_name_control.Size = New System.Drawing.Size(83, 16)
        Me.rb_control_box_name_control.TabIndex = 117
        Me.rb_control_box_name_control.Text = "主控箱名称"
        Me.rb_control_box_name_control.UseVisualStyleBackColor = False
        '
        'rb_lamp_id_control
        '
        Me.rb_lamp_id_control.AutoSize = True
        Me.rb_lamp_id_control.BackColor = System.Drawing.Color.Transparent
        Me.rb_lamp_id_control.Checked = True
        Me.rb_lamp_id_control.Location = New System.Drawing.Point(141, 56)
        Me.rb_lamp_id_control.Name = "rb_lamp_id_control"
        Me.rb_lamp_id_control.Size = New System.Drawing.Size(71, 16)
        Me.rb_lamp_id_control.TabIndex = 118
        Me.rb_lamp_id_control.TabStop = True
        Me.rb_lamp_id_control.Text = "终端编号"
        Me.rb_lamp_id_control.UseVisualStyleBackColor = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox3.Controls.Add(Me.dgv_problem_list)
        Me.GroupBox3.Location = New System.Drawing.Point(266, 5)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(579, 518)
        Me.GroupBox3.TabIndex = 111
        Me.GroupBox3.TabStop = False
        '
        'bt_excel_data
        '
        Me.bt_excel_data.Location = New System.Drawing.Point(168, 489)
        Me.bt_excel_data.Name = "bt_excel_data"
        Me.bt_excel_data.Size = New System.Drawing.Size(76, 23)
        Me.bt_excel_data.TabIndex = 113
        Me.bt_excel_data.Text = "导出报表"
        Me.bt_excel_data.UseVisualStyleBackColor = True
        '
        'bt_reflash
        '
        Me.bt_reflash.Location = New System.Drawing.Point(88, 489)
        Me.bt_reflash.Name = "bt_reflash"
        Me.bt_reflash.Size = New System.Drawing.Size(75, 23)
        Me.bt_reflash.TabIndex = 114
        Me.bt_reflash.Text = "刷新"
        Me.bt_reflash.UseVisualStyleBackColor = True
        '
        'Alarm_viewTableAdapter
        '
        Me.Alarm_viewTableAdapter.ClearBeforeFill = True
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Location = New System.Drawing.Point(0, -1)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(856, 549)
        Me.TabControl1.TabIndex = 115
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.lb_lamp_type_id)
        Me.TabPage1.Controls.Add(Me.bt_reflash)
        Me.TabPage1.Controls.Add(Me.bt_excel_data)
        Me.TabPage1.Controls.Add(Me.bt_find_record)
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.Controls.Add(Me.GroupBox2)
        Me.TabPage1.Controls.Add(Me.GroupBox3)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(848, 523)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "单灯报警记录"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'lb_lamp_type_id
        '
        Me.lb_lamp_type_id.AutoSize = True
        Me.lb_lamp_type_id.BackColor = System.Drawing.Color.Transparent
        Me.lb_lamp_type_id.Location = New System.Drawing.Point(117, 475)
        Me.lb_lamp_type_id.Name = "lb_lamp_type_id"
        Me.lb_lamp_type_id.Size = New System.Drawing.Size(125, 12)
        Me.lb_lamp_type_id.TabIndex = 112
        Me.lb_lamp_type_id.Text = "景观灯编号的前半部分"
        Me.lb_lamp_type_id.Visible = False
        '
        'KaiguanalarmlistBindingSource
        '
        Me.KaiguanalarmlistBindingSource.DataMember = "kaiguan_alarm_list"
        Me.KaiguanalarmlistBindingSource.DataSource = Me.KaiguanlistDataSet
        '
        'KaiguanlistDataSet
        '
        Me.KaiguanlistDataSet.DataSetName = "kaiguanlistDataSet"
        Me.KaiguanlistDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'Kaiguan_alarm_listTableAdapter
        '
        Me.Kaiguan_alarm_listTableAdapter.ClearBeforeFill = True
        '
        '故障列表
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(866, 570)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "故障列表"
        Me.Text = "报警信息表"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        CType(Me.dgv_problem_list, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AlarmviewBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StreetlampDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        CType(Me.KaiguanalarmlistBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.KaiguanlistDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents StreetlampDataSet As streetlamp.streetlampDataSet
    Friend WithEvents AlarmviewBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents Alarm_viewTableAdapter As streetlamp.streetlampDataSetTableAdapters.alarm_viewTableAdapter
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents progress As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents record_num As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents dgv_problem_list As System.Windows.Forms.DataGridView
    Friend WithEvents bt_find_record As System.Windows.Forms.Button
    Friend WithEvents cb_lamp_id As System.Windows.Forms.ComboBox
    Friend WithEvents pro_lamp_id_string As System.Windows.Forms.Label
    Friend WithEvents cb_control_box_name As System.Windows.Forms.ComboBox
    Friend WithEvents pro_box_string As System.Windows.Forms.Label
    Friend WithEvents Date_start_string As System.Windows.Forms.Label
    Friend WithEvents Date_end_string As System.Windows.Forms.Label
    Friend WithEvents dtp_date_start As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtp_date_end As System.Windows.Forms.DateTimePicker
    Friend WithEvents rb_date_condition_find As System.Windows.Forms.RadioButton
    Friend WithEvents rb_no_date_condition_find As System.Windows.Forms.RadioButton
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents cb_lamp_type As System.Windows.Forms.ComboBox
    Friend WithEvents lb_lamp_id_start As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents rb_control_box_name_control As System.Windows.Forms.RadioButton
    Friend WithEvents rb_lamp_id_control As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents bt_excel_data As System.Windows.Forms.Button
    Friend WithEvents bt_reflash As System.Windows.Forms.Button
    Friend WithEvents alarm_okornot As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents rb_lamp_type_control As System.Windows.Forms.RadioButton
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cb_city_name As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cb_street_name As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cb_area_name As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents rb_city_control As System.Windows.Forms.RadioButton
    Friend WithEvents rb_area_control As System.Windows.Forms.RadioButton
    Friend WithEvents rb_street_control As System.Windows.Forms.RadioButton
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents lb_lamp_type_id As System.Windows.Forms.Label
    Friend WithEvents KaiguanlistDataSet As streetlamp.kaiguanlistDataSet
    Friend WithEvents KaiguanalarmlistBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents Kaiguan_alarm_listTableAdapter As streetlamp.kaiguanlistDataSetTableAdapters.kaiguan_alarm_listTableAdapter
    Friend WithEvents alarm_id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents control_box_name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents type_string As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lamp_id_short As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents problem_inf As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents date_start As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Date_end As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents alarm_ok As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents server_state As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
