<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 路灯亮暗信息统计
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
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(路灯亮暗信息统计))
        Me.static_excel = New System.Windows.Forms.Button()
        Me.clear = New System.Windows.Forms.Button()
        Me.Date_end = New System.Windows.Forms.DateTimePicker()
        Me.Date_start = New System.Windows.Forms.DateTimePicker()
        Me.Date_end_string = New System.Windows.Forms.Label()
        Me.Date_start_string = New System.Windows.Forms.Label()
        Me.find_record = New System.Windows.Forms.Button()
        Me.static_lamp_id = New System.Windows.Forms.ComboBox()
        Me.static_lamp_id_string = New System.Windows.Forms.Label()
        Me.static_control_box_name = New System.Windows.Forms.ComboBox()
        Me.static_box_string = New System.Windows.Forms.Label()
        Me.BackgroundWorker_on_off = New System.ComponentModel.BackgroundWorker()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.record_num = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ProgressBar = New System.Windows.Forms.ToolStripProgressBar()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.ComboBox_Record = New System.Windows.Forms.ComboBox()
        Me.Label_Record = New System.Windows.Forms.Label()
        Me.static_lampstate = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lamp_type_control = New System.Windows.Forms.RadioButton()
        Me.lamp_type_id = New System.Windows.Forms.Label()
        Me.static_city = New System.Windows.Forms.ComboBox()
        Me.static_lamp_id_start = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.static_area = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.static_street = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.street_control = New System.Windows.Forms.RadioButton()
        Me.area_control = New System.Windows.Forms.RadioButton()
        Me.city_control = New System.Windows.Forms.RadioButton()
        Me.lamp_id_control = New System.Windows.Forms.RadioButton()
        Me.control_box_control = New System.Windows.Forms.RadioButton()
        Me.static_lamp_type = New System.Windows.Forms.ComboBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.dgv_statelist = New System.Windows.Forms.DataGridView()
        Me.id_lamp = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lampid = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.presure = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.current = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.power = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.yinshu = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.state = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.start_time = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.end_time = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.config_excel = New System.Windows.Forms.Button()
        Me.check = New System.Windows.Forms.Button()
        Me.config_state = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.control_box_list = New System.Windows.Forms.ComboBox()
        Me.all_inf_check = New System.Windows.Forms.RadioButton()
        Me.box_name_check = New System.Windows.Forms.RadioButton()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.config_statelist = New System.Windows.Forms.DataGridView()
        Me.ConfigstatelistBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ConfigDataSet = New streetlamp.ConfigDataSet()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.date_condition_find = New System.Windows.Forms.RadioButton()
        Me.DateTimePickerEnd = New System.Windows.Forms.DateTimePicker()
        Me.no_date_condition_find = New System.Windows.Forms.RadioButton()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.DateTimePickerStart = New System.Windows.Forms.DateTimePicker()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Config_state_listTableAdapter = New streetlamp.ConfigDataSetTableAdapters.config_state_listTableAdapter()
        Me.id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.controlboxid = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.controlboxname = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.createtime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cofigstate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.StateflagDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.StatusStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.dgv_statelist, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.config_statelist, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ConfigstatelistBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ConfigDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'static_excel
        '
        Me.static_excel.Location = New System.Drawing.Point(142, 376)
        Me.static_excel.Name = "static_excel"
        Me.static_excel.Size = New System.Drawing.Size(90, 23)
        Me.static_excel.TabIndex = 149
        Me.static_excel.Text = "运行统计报表"
        Me.static_excel.UseVisualStyleBackColor = True
        '
        'clear
        '
        Me.clear.Location = New System.Drawing.Point(12, 403)
        Me.clear.Name = "clear"
        Me.clear.Size = New System.Drawing.Size(90, 23)
        Me.clear.TabIndex = 148
        Me.clear.Text = "清空"
        Me.clear.UseVisualStyleBackColor = True
        '
        'Date_end
        '
        Me.Date_end.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Date_end.Location = New System.Drawing.Point(100, 147)
        Me.Date_end.Name = "Date_end"
        Me.Date_end.Size = New System.Drawing.Size(137, 21)
        Me.Date_end.TabIndex = 146
        '
        'Date_start
        '
        Me.Date_start.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Date_start.Location = New System.Drawing.Point(100, 111)
        Me.Date_start.Name = "Date_start"
        Me.Date_start.Size = New System.Drawing.Size(137, 21)
        Me.Date_start.TabIndex = 145
        '
        'Date_end_string
        '
        Me.Date_end_string.AutoSize = True
        Me.Date_end_string.BackColor = System.Drawing.Color.Transparent
        Me.Date_end_string.Location = New System.Drawing.Point(29, 151)
        Me.Date_end_string.Name = "Date_end_string"
        Me.Date_end_string.Size = New System.Drawing.Size(65, 12)
        Me.Date_end_string.TabIndex = 144
        Me.Date_end_string.Text = "结束日期："
        '
        'Date_start_string
        '
        Me.Date_start_string.AutoSize = True
        Me.Date_start_string.BackColor = System.Drawing.Color.Transparent
        Me.Date_start_string.Location = New System.Drawing.Point(30, 115)
        Me.Date_start_string.Name = "Date_start_string"
        Me.Date_start_string.Size = New System.Drawing.Size(65, 12)
        Me.Date_start_string.TabIndex = 143
        Me.Date_start_string.Text = "开始日期："
        '
        'find_record
        '
        Me.find_record.Location = New System.Drawing.Point(12, 374)
        Me.find_record.Name = "find_record"
        Me.find_record.Size = New System.Drawing.Size(90, 23)
        Me.find_record.TabIndex = 142
        Me.find_record.Text = "查询"
        Me.find_record.UseVisualStyleBackColor = True
        '
        'static_lamp_id
        '
        Me.static_lamp_id.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.static_lamp_id.FormattingEnabled = True
        Me.static_lamp_id.Location = New System.Drawing.Point(100, 299)
        Me.static_lamp_id.Name = "static_lamp_id"
        Me.static_lamp_id.Size = New System.Drawing.Size(137, 20)
        Me.static_lamp_id.TabIndex = 140
        '
        'static_lamp_id_string
        '
        Me.static_lamp_id_string.AutoSize = True
        Me.static_lamp_id_string.BackColor = System.Drawing.Color.Transparent
        Me.static_lamp_id_string.Location = New System.Drawing.Point(29, 302)
        Me.static_lamp_id_string.Name = "static_lamp_id_string"
        Me.static_lamp_id_string.Size = New System.Drawing.Size(65, 12)
        Me.static_lamp_id_string.TabIndex = 138
        Me.static_lamp_id_string.Text = "终端代码："
        '
        'static_control_box_name
        '
        Me.static_control_box_name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.static_control_box_name.FormattingEnabled = True
        Me.static_control_box_name.Location = New System.Drawing.Point(101, 251)
        Me.static_control_box_name.Name = "static_control_box_name"
        Me.static_control_box_name.Size = New System.Drawing.Size(136, 20)
        Me.static_control_box_name.TabIndex = 139
        '
        'static_box_string
        '
        Me.static_box_string.AutoSize = True
        Me.static_box_string.BackColor = System.Drawing.Color.Transparent
        Me.static_box_string.Location = New System.Drawing.Point(28, 254)
        Me.static_box_string.Name = "static_box_string"
        Me.static_box_string.Size = New System.Drawing.Size(65, 12)
        Me.static_box_string.TabIndex = 136
        Me.static_box_string.Text = "网关名称："
        '
        'BackgroundWorker_on_off
        '
        Me.BackgroundWorker_on_off.WorkerReportsProgress = True
        Me.BackgroundWorker_on_off.WorkerSupportsCancellation = True
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.record_num, Me.ProgressBar})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 479)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(729, 22)
        Me.StatusStrip1.TabIndex = 150
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'record_num
        '
        Me.record_num.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.record_num.Name = "record_num"
        Me.record_num.Size = New System.Drawing.Size(119, 17)
        Me.record_num.Text = "设备运行状态统计"
        '
        'ProgressBar
        '
        Me.ProgressBar.Name = "ProgressBar"
        Me.ProgressBar.Size = New System.Drawing.Size(200, 16)
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.ComboBox_Record)
        Me.GroupBox1.Controls.Add(Me.Label_Record)
        Me.GroupBox1.Controls.Add(Me.static_lampstate)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.lamp_type_control)
        Me.GroupBox1.Controls.Add(Me.lamp_type_id)
        Me.GroupBox1.Controls.Add(Me.static_city)
        Me.GroupBox1.Controls.Add(Me.static_lamp_id_start)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.static_area)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.static_street)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.street_control)
        Me.GroupBox1.Controls.Add(Me.area_control)
        Me.GroupBox1.Controls.Add(Me.city_control)
        Me.GroupBox1.Controls.Add(Me.lamp_id_control)
        Me.GroupBox1.Controls.Add(Me.control_box_control)
        Me.GroupBox1.Controls.Add(Me.static_lamp_type)
        Me.GroupBox1.Controls.Add(Me.Date_start)
        Me.GroupBox1.Controls.Add(Me.Date_start_string)
        Me.GroupBox1.Controls.Add(Me.clear)
        Me.GroupBox1.Controls.Add(Me.static_excel)
        Me.GroupBox1.Controls.Add(Me.Date_end)
        Me.GroupBox1.Controls.Add(Me.Date_end_string)
        Me.GroupBox1.Controls.Add(Me.static_control_box_name)
        Me.GroupBox1.Controls.Add(Me.static_box_string)
        Me.GroupBox1.Controls.Add(Me.find_record)
        Me.GroupBox1.Controls.Add(Me.static_lamp_id_string)
        Me.GroupBox1.Controls.Add(Me.static_lamp_id)
        Me.GroupBox1.Location = New System.Drawing.Point(5, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(264, 441)
        Me.GroupBox1.TabIndex = 151
        Me.GroupBox1.TabStop = False
        '
        'ComboBox_Record
        '
        Me.ComboBox_Record.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_Record.FormattingEnabled = True
        Me.ComboBox_Record.Items.AddRange(New Object() {"状态", "数据记录"})
        Me.ComboBox_Record.Location = New System.Drawing.Point(100, 350)
        Me.ComboBox_Record.Name = "ComboBox_Record"
        Me.ComboBox_Record.Size = New System.Drawing.Size(137, 20)
        Me.ComboBox_Record.TabIndex = 173
        '
        'Label_Record
        '
        Me.Label_Record.AutoSize = True
        Me.Label_Record.BackColor = System.Drawing.Color.Transparent
        Me.Label_Record.Location = New System.Drawing.Point(28, 352)
        Me.Label_Record.Name = "Label_Record"
        Me.Label_Record.Size = New System.Drawing.Size(65, 12)
        Me.Label_Record.TabIndex = 172
        Me.Label_Record.Text = "表达格式："
        '
        'static_lampstate
        '
        Me.static_lampstate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.static_lampstate.FormattingEnabled = True
        Me.static_lampstate.Items.AddRange(New Object() {"全部状态", "正常状态", "故障状态", "无信号状态"})
        Me.static_lampstate.Location = New System.Drawing.Point(101, 325)
        Me.static_lampstate.Name = "static_lampstate"
        Me.static_lampstate.Size = New System.Drawing.Size(137, 20)
        Me.static_lampstate.TabIndex = 171
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Location = New System.Drawing.Point(29, 327)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(65, 12)
        Me.Label9.TabIndex = 170
        Me.Label9.Text = "查询条件："
        '
        'lamp_type_control
        '
        Me.lamp_type_control.AutoSize = True
        Me.lamp_type_control.Location = New System.Drawing.Point(33, 80)
        Me.lamp_type_control.Name = "lamp_type_control"
        Me.lamp_type_control.Size = New System.Drawing.Size(95, 16)
        Me.lamp_type_control.TabIndex = 165
        Me.lamp_type_control.Text = "类型名称级别"
        Me.lamp_type_control.UseVisualStyleBackColor = True
        '
        'lamp_type_id
        '
        Me.lamp_type_id.AutoSize = True
        Me.lamp_type_id.Location = New System.Drawing.Point(183, 426)
        Me.lamp_type_id.Name = "lamp_type_id"
        Me.lamp_type_id.Size = New System.Drawing.Size(53, 12)
        Me.lamp_type_id.TabIndex = 169
        Me.lamp_type_id.Text = "灯的类型"
        Me.lamp_type_id.Visible = False
        '
        'static_city
        '
        Me.static_city.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.static_city.FormattingEnabled = True
        Me.static_city.Location = New System.Drawing.Point(100, 179)
        Me.static_city.Name = "static_city"
        Me.static_city.Size = New System.Drawing.Size(136, 20)
        Me.static_city.TabIndex = 164
        '
        'static_lamp_id_start
        '
        Me.static_lamp_id_start.AutoSize = True
        Me.static_lamp_id_start.Location = New System.Drawing.Point(10, 429)
        Me.static_lamp_id_start.Name = "static_lamp_id_start"
        Me.static_lamp_id_start.Size = New System.Drawing.Size(113, 12)
        Me.static_lamp_id_start.TabIndex = 155
        Me.static_lamp_id_start.Text = "景观灯编号前半部分"
        Me.static_lamp_id_start.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(29, 182)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(65, 12)
        Me.Label4.TabIndex = 163
        Me.Label4.Text = "城市名称："
        '
        'static_area
        '
        Me.static_area.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.static_area.FormattingEnabled = True
        Me.static_area.Location = New System.Drawing.Point(100, 203)
        Me.static_area.Name = "static_area"
        Me.static_area.Size = New System.Drawing.Size(136, 20)
        Me.static_area.TabIndex = 162
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(29, 206)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 12)
        Me.Label3.TabIndex = 161
        Me.Label3.Text = "区域名称："
        '
        'static_street
        '
        Me.static_street.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.static_street.FormattingEnabled = True
        Me.static_street.Location = New System.Drawing.Point(100, 227)
        Me.static_street.Name = "static_street"
        Me.static_street.Size = New System.Drawing.Size(136, 20)
        Me.static_street.TabIndex = 160
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(29, 230)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 12)
        Me.Label2.TabIndex = 159
        Me.Label2.Text = "乡镇名称："
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(29, 278)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 12)
        Me.Label1.TabIndex = 158
        Me.Label1.Text = "类型名称："
        '
        'street_control
        '
        Me.street_control.AutoSize = True
        Me.street_control.Location = New System.Drawing.Point(33, 50)
        Me.street_control.Name = "street_control"
        Me.street_control.Size = New System.Drawing.Size(95, 16)
        Me.street_control.TabIndex = 157
        Me.street_control.Text = "乡镇名称级别"
        Me.street_control.UseVisualStyleBackColor = True
        '
        'area_control
        '
        Me.area_control.AutoSize = True
        Me.area_control.Location = New System.Drawing.Point(142, 19)
        Me.area_control.Name = "area_control"
        Me.area_control.Size = New System.Drawing.Size(95, 16)
        Me.area_control.TabIndex = 156
        Me.area_control.Text = "区域名称级别"
        Me.area_control.UseVisualStyleBackColor = True
        '
        'city_control
        '
        Me.city_control.AutoSize = True
        Me.city_control.Location = New System.Drawing.Point(32, 19)
        Me.city_control.Name = "city_control"
        Me.city_control.Size = New System.Drawing.Size(95, 16)
        Me.city_control.TabIndex = 155
        Me.city_control.Text = "城市名称级别"
        Me.city_control.UseVisualStyleBackColor = True
        '
        'lamp_id_control
        '
        Me.lamp_id_control.AutoSize = True
        Me.lamp_id_control.Checked = True
        Me.lamp_id_control.Location = New System.Drawing.Point(142, 80)
        Me.lamp_id_control.Name = "lamp_id_control"
        Me.lamp_id_control.Size = New System.Drawing.Size(95, 16)
        Me.lamp_id_control.TabIndex = 154
        Me.lamp_id_control.TabStop = True
        Me.lamp_id_control.Text = "终端代码级别"
        Me.lamp_id_control.UseVisualStyleBackColor = True
        '
        'control_box_control
        '
        Me.control_box_control.AutoSize = True
        Me.control_box_control.Location = New System.Drawing.Point(142, 50)
        Me.control_box_control.Name = "control_box_control"
        Me.control_box_control.Size = New System.Drawing.Size(95, 16)
        Me.control_box_control.TabIndex = 152
        Me.control_box_control.Text = "网关名称级别"
        Me.control_box_control.UseVisualStyleBackColor = True
        '
        'static_lamp_type
        '
        Me.static_lamp_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.static_lamp_type.FormattingEnabled = True
        Me.static_lamp_type.Location = New System.Drawing.Point(100, 275)
        Me.static_lamp_type.Name = "static_lamp_type"
        Me.static_lamp_type.Size = New System.Drawing.Size(137, 20)
        Me.static_lamp_type.TabIndex = 151
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(0, 3)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(718, 475)
        Me.TabControl1.TabIndex = 170
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.dgv_statelist)
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 21)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(710, 450)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "设备运行状态日志"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'dgv_statelist
        '
        Me.dgv_statelist.AllowUserToAddRows = False
        Me.dgv_statelist.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv_statelist.BackgroundColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_statelist.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv_statelist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_statelist.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.id_lamp, Me.lampid, Me.presure, Me.current, Me.power, Me.yinshu, Me.state, Me.start_time, Me.end_time})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv_statelist.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgv_statelist.Location = New System.Drawing.Point(275, 6)
        Me.dgv_statelist.Name = "dgv_statelist"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_statelist.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dgv_statelist.RowHeadersVisible = False
        Me.dgv_statelist.RowTemplate.Height = 23
        Me.dgv_statelist.Size = New System.Drawing.Size(429, 438)
        Me.dgv_statelist.TabIndex = 152
        '
        'id_lamp
        '
        Me.id_lamp.HeaderText = "编号"
        Me.id_lamp.Name = "id_lamp"
        Me.id_lamp.Width = 60
        '
        'lampid
        '
        Me.lampid.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.lampid.HeaderText = "节点编号"
        Me.lampid.Name = "lampid"
        Me.lampid.Width = 78
        '
        'presure
        '
        Me.presure.HeaderText = "电压(V)"
        Me.presure.Name = "presure"
        Me.presure.Width = 80
        '
        'current
        '
        Me.current.HeaderText = "电流(A)"
        Me.current.Name = "current"
        Me.current.Width = 80
        '
        'power
        '
        Me.power.HeaderText = "功率(W)"
        Me.power.Name = "power"
        Me.power.Width = 80
        '
        'yinshu
        '
        Me.yinshu.HeaderText = "功率因数"
        Me.yinshu.Name = "yinshu"
        '
        'state
        '
        Me.state.HeaderText = "状态"
        Me.state.Name = "state"
        '
        'start_time
        '
        Me.start_time.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.start_time.HeaderText = "开始时间"
        Me.start_time.Name = "start_time"
        Me.start_time.Width = 78
        '
        'end_time
        '
        Me.end_time.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.end_time.HeaderText = "结束时间"
        Me.end_time.Name = "end_time"
        Me.end_time.Width = 78
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.GroupBox3)
        Me.TabPage2.Controls.Add(Me.config_statelist)
        Me.TabPage2.Controls.Add(Me.GroupBox2)
        Me.TabPage2.Location = New System.Drawing.Point(4, 21)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(710, 450)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "设备配置状态日志"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Controls.Add(Me.config_excel)
        Me.GroupBox3.Controls.Add(Me.check)
        Me.GroupBox3.Controls.Add(Me.config_state)
        Me.GroupBox3.Controls.Add(Me.Label8)
        Me.GroupBox3.Controls.Add(Me.control_box_list)
        Me.GroupBox3.Controls.Add(Me.all_inf_check)
        Me.GroupBox3.Controls.Add(Me.box_name_check)
        Me.GroupBox3.Controls.Add(Me.Label7)
        Me.GroupBox3.Location = New System.Drawing.Point(5, 146)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(241, 294)
        Me.GroupBox3.TabIndex = 115
        Me.GroupBox3.TabStop = False
        '
        'config_excel
        '
        Me.config_excel.Location = New System.Drawing.Point(124, 181)
        Me.config_excel.Name = "config_excel"
        Me.config_excel.Size = New System.Drawing.Size(90, 23)
        Me.config_excel.TabIndex = 150
        Me.config_excel.Text = "导出报表"
        Me.config_excel.UseVisualStyleBackColor = True
        '
        'check
        '
        Me.check.Location = New System.Drawing.Point(20, 181)
        Me.check.Name = "check"
        Me.check.Size = New System.Drawing.Size(90, 23)
        Me.check.TabIndex = 119
        Me.check.Text = "查询"
        Me.check.UseVisualStyleBackColor = True
        '
        'config_state
        '
        Me.config_state.FormattingEnabled = True
        Me.config_state.Items.AddRange(New Object() {"成功", "失败", "全部"})
        Me.config_state.Location = New System.Drawing.Point(106, 114)
        Me.config_state.Name = "config_state"
        Me.config_state.Size = New System.Drawing.Size(118, 20)
        Me.config_state.TabIndex = 118
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(35, 117)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(65, 12)
        Me.Label8.TabIndex = 117
        Me.Label8.Text = "配置状态："
        '
        'control_box_list
        '
        Me.control_box_list.FormattingEnabled = True
        Me.control_box_list.Location = New System.Drawing.Point(106, 72)
        Me.control_box_list.Name = "control_box_list"
        Me.control_box_list.Size = New System.Drawing.Size(118, 20)
        Me.control_box_list.TabIndex = 116
        '
        'all_inf_check
        '
        Me.all_inf_check.AutoSize = True
        Me.all_inf_check.Location = New System.Drawing.Point(142, 28)
        Me.all_inf_check.Name = "all_inf_check"
        Me.all_inf_check.Size = New System.Drawing.Size(95, 16)
        Me.all_inf_check.TabIndex = 115
        Me.all_inf_check.Text = "所有信息查询"
        Me.all_inf_check.UseVisualStyleBackColor = True
        '
        'box_name_check
        '
        Me.box_name_check.AutoSize = True
        Me.box_name_check.Checked = True
        Me.box_name_check.Location = New System.Drawing.Point(17, 28)
        Me.box_name_check.Name = "box_name_check"
        Me.box_name_check.Size = New System.Drawing.Size(95, 16)
        Me.box_name_check.TabIndex = 114
        Me.box_name_check.TabStop = True
        Me.box_name_check.Text = "网关名称查询"
        Me.box_name_check.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(29, 75)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(59, 12)
        Me.Label7.TabIndex = 113
        Me.Label7.Text = "网关名称:"
        '
        'config_statelist
        '
        Me.config_statelist.AllowUserToAddRows = False
        Me.config_statelist.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.config_statelist.AutoGenerateColumns = False
        Me.config_statelist.BackgroundColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.config_statelist.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.config_statelist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.config_statelist.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.id, Me.controlboxid, Me.controlboxname, Me.createtime, Me.cofigstate, Me.StateflagDataGridViewTextBoxColumn})
        Me.config_statelist.DataSource = Me.ConfigstatelistBindingSource
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.config_statelist.DefaultCellStyle = DataGridViewCellStyle5
        Me.config_statelist.Location = New System.Drawing.Point(261, 5)
        Me.config_statelist.Name = "config_statelist"
        Me.config_statelist.RowTemplate.Height = 23
        Me.config_statelist.Size = New System.Drawing.Size(445, 435)
        Me.config_statelist.TabIndex = 114
        '
        'ConfigstatelistBindingSource
        '
        Me.ConfigstatelistBindingSource.DataMember = "config_state_list"
        Me.ConfigstatelistBindingSource.DataSource = Me.ConfigDataSet
        '
        'ConfigDataSet
        '
        Me.ConfigDataSet.DataSetName = "ConfigDataSet"
        Me.ConfigDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.Controls.Add(Me.date_condition_find)
        Me.GroupBox2.Controls.Add(Me.DateTimePickerEnd)
        Me.GroupBox2.Controls.Add(Me.no_date_condition_find)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.DateTimePickerStart)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Location = New System.Drawing.Point(5, 5)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(241, 135)
        Me.GroupBox2.TabIndex = 113
        Me.GroupBox2.TabStop = False
        '
        'date_condition_find
        '
        Me.date_condition_find.AutoSize = True
        Me.date_condition_find.BackColor = System.Drawing.Color.Transparent
        Me.date_condition_find.Checked = True
        Me.date_condition_find.Location = New System.Drawing.Point(20, 20)
        Me.date_condition_find.Name = "date_condition_find"
        Me.date_condition_find.Size = New System.Drawing.Size(83, 16)
        Me.date_condition_find.TabIndex = 110
        Me.date_condition_find.TabStop = True
        Me.date_condition_find.Text = "按日期查询"
        Me.date_condition_find.UseVisualStyleBackColor = False
        '
        'DateTimePickerEnd
        '
        Me.DateTimePickerEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePickerEnd.Location = New System.Drawing.Point(106, 92)
        Me.DateTimePickerEnd.Name = "DateTimePickerEnd"
        Me.DateTimePickerEnd.Size = New System.Drawing.Size(118, 21)
        Me.DateTimePickerEnd.TabIndex = 109
        '
        'no_date_condition_find
        '
        Me.no_date_condition_find.AutoSize = True
        Me.no_date_condition_find.BackColor = System.Drawing.Color.Transparent
        Me.no_date_condition_find.Location = New System.Drawing.Point(124, 20)
        Me.no_date_condition_find.Name = "no_date_condition_find"
        Me.no_date_condition_find.Size = New System.Drawing.Size(83, 16)
        Me.no_date_condition_find.TabIndex = 111
        Me.no_date_condition_find.Text = "无日期查询"
        Me.no_date_condition_find.UseVisualStyleBackColor = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Location = New System.Drawing.Point(35, 96)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(65, 12)
        Me.Label5.TabIndex = 107
        Me.Label5.Text = "结束日期："
        '
        'DateTimePickerStart
        '
        Me.DateTimePickerStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePickerStart.Location = New System.Drawing.Point(106, 58)
        Me.DateTimePickerStart.Name = "DateTimePickerStart"
        Me.DateTimePickerStart.Size = New System.Drawing.Size(118, 21)
        Me.DateTimePickerStart.TabIndex = 108
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Location = New System.Drawing.Point(35, 62)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(65, 12)
        Me.Label6.TabIndex = 106
        Me.Label6.Text = "开始日期："
        '
        'Config_state_listTableAdapter
        '
        Me.Config_state_listTableAdapter.ClearBeforeFill = True
        '
        'id
        '
        Me.id.DataPropertyName = "id"
        Me.id.HeaderText = "编号"
        Me.id.Name = "id"
        Me.id.ReadOnly = True
        '
        'controlboxid
        '
        Me.controlboxid.DataPropertyName = "control_box_id"
        Me.controlboxid.HeaderText = "网关编号"
        Me.controlboxid.Name = "controlboxid"
        '
        'controlboxname
        '
        Me.controlboxname.DataPropertyName = "control_box_name"
        Me.controlboxname.HeaderText = "网关名称"
        Me.controlboxname.Name = "controlboxname"
        '
        'createtime
        '
        Me.createtime.DataPropertyName = "createtime"
        Me.createtime.HeaderText = "时间"
        Me.createtime.Name = "createtime"
        '
        'cofigstate
        '
        Me.cofigstate.DataPropertyName = "config_state"
        Me.cofigstate.HeaderText = "配置状态"
        Me.cofigstate.Name = "cofigstate"
        Me.cofigstate.Width = 200
        '
        'StateflagDataGridViewTextBoxColumn
        '
        Me.StateflagDataGridViewTextBoxColumn.DataPropertyName = "state_flag"
        Me.StateflagDataGridViewTextBoxColumn.HeaderText = "state_flag"
        Me.StateflagDataGridViewTextBoxColumn.Name = "StateflagDataGridViewTextBoxColumn"
        Me.StateflagDataGridViewTextBoxColumn.Visible = False
        '
        '路灯亮暗信息统计
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(729, 501)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "路灯亮暗信息统计"
        Me.Text = "设备日志"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.dgv_statelist, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.config_statelist, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ConfigstatelistBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ConfigDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents static_excel As System.Windows.Forms.Button
    Friend WithEvents clear As System.Windows.Forms.Button
    Friend WithEvents Date_end As System.Windows.Forms.DateTimePicker
    Friend WithEvents Date_start As System.Windows.Forms.DateTimePicker
    Friend WithEvents Date_end_string As System.Windows.Forms.Label
    Friend WithEvents Date_start_string As System.Windows.Forms.Label
    Friend WithEvents find_record As System.Windows.Forms.Button
    Friend WithEvents static_lamp_id As System.Windows.Forms.ComboBox
    Friend WithEvents static_lamp_id_string As System.Windows.Forms.Label
    Friend WithEvents static_control_box_name As System.Windows.Forms.ComboBox
    Friend WithEvents static_box_string As System.Windows.Forms.Label
    Friend WithEvents BackgroundWorker_on_off As System.ComponentModel.BackgroundWorker
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents record_num As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ProgressBar As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents static_lamp_type As System.Windows.Forms.ComboBox
    Friend WithEvents lamp_id_control As System.Windows.Forms.RadioButton
    Friend WithEvents control_box_control As System.Windows.Forms.RadioButton
    Friend WithEvents static_lamp_id_start As System.Windows.Forms.Label
    Friend WithEvents lamp_type_id As System.Windows.Forms.Label
    Friend WithEvents area_control As System.Windows.Forms.RadioButton
    Friend WithEvents city_control As System.Windows.Forms.RadioButton
    Friend WithEvents street_control As System.Windows.Forms.RadioButton
    Friend WithEvents static_city As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents static_area As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents static_street As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lamp_type_control As System.Windows.Forms.RadioButton
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents date_condition_find As System.Windows.Forms.RadioButton
    Friend WithEvents DateTimePickerEnd As System.Windows.Forms.DateTimePicker
    Friend WithEvents no_date_condition_find As System.Windows.Forms.RadioButton
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents DateTimePickerStart As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents config_statelist As System.Windows.Forms.DataGridView
    Friend WithEvents ConfigDataSet As streetlamp.ConfigDataSet
    Friend WithEvents ConfigstatelistBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents Config_state_listTableAdapter As streetlamp.ConfigDataSetTableAdapters.config_state_listTableAdapter
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents all_inf_check As System.Windows.Forms.RadioButton
    Friend WithEvents box_name_check As System.Windows.Forms.RadioButton
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents control_box_list As System.Windows.Forms.ComboBox
    Friend WithEvents check As System.Windows.Forms.Button
    Friend WithEvents config_state As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents config_excel As System.Windows.Forms.Button
    Friend WithEvents dgv_statelist As System.Windows.Forms.DataGridView
    Friend WithEvents static_lampstate As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents ComboBox_Record As System.Windows.Forms.ComboBox
    Friend WithEvents Label_Record As System.Windows.Forms.Label
    Friend WithEvents id_lamp As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lampid As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents presure As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents current As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents power As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents yinshu As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents state As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents start_time As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents end_time As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents controlboxid As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents controlboxname As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents createtime As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cofigstate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents StateflagDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
