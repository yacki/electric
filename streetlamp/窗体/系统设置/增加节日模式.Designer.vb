<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 增加节日模式
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(增加节日模式))
        Me.control_type_groupbox = New System.Windows.Forms.GroupBox()
        Me.rb_type_control = New System.Windows.Forms.RadioButton()
        Me.rb_street_control = New System.Windows.Forms.RadioButton()
        Me.rb_area_control = New System.Windows.Forms.RadioButton()
        Me.rb_city_control = New System.Windows.Forms.RadioButton()
        Me.rb_lamp_id_control = New System.Windows.Forms.RadioButton()
        Me.rb_box_control = New System.Windows.Forms.RadioButton()
        Me.cb_lamp_type_all = New System.Windows.Forms.ComboBox()
        Me.cb_box_all = New System.Windows.Forms.ComboBox()
        Me.box_all_string = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cb_city_all = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cb_area_all = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cb_street_all = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lb_lamp_type_id = New System.Windows.Forms.Label()
        Me.lb_lamp_id_start = New System.Windows.Forms.Label()
        Me.cb_lamp_id = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.dgv_control_list = New System.Windows.Forms.DataGridView()
        Me.control_type = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.control_obj = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.control_method = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.time = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.bt_add_method = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tb_mod_title = New System.Windows.Forms.TextBox()
        Me.bt_add_mod = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.rb_type_double_open = New System.Windows.Forms.RadioButton()
        Me.rb_type_single_open = New System.Windows.Forms.RadioButton()
        Me.rb_all_close = New System.Windows.Forms.RadioButton()
        Me.rb_all_open = New System.Windows.Forms.RadioButton()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cb_next_time = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.control_type_groupbox.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        CType(Me.dgv_control_list, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'control_type_groupbox
        '
        Me.control_type_groupbox.BackColor = System.Drawing.Color.Transparent
        Me.control_type_groupbox.Controls.Add(Me.rb_type_control)
        Me.control_type_groupbox.Controls.Add(Me.rb_street_control)
        Me.control_type_groupbox.Controls.Add(Me.rb_area_control)
        Me.control_type_groupbox.Controls.Add(Me.rb_city_control)
        Me.control_type_groupbox.Controls.Add(Me.rb_lamp_id_control)
        Me.control_type_groupbox.Controls.Add(Me.rb_box_control)
        Me.control_type_groupbox.Location = New System.Drawing.Point(12, 12)
        Me.control_type_groupbox.Name = "control_type_groupbox"
        Me.control_type_groupbox.Size = New System.Drawing.Size(315, 78)
        Me.control_type_groupbox.TabIndex = 103
        Me.control_type_groupbox.TabStop = False
        Me.control_type_groupbox.Text = "控制类型"
        '
        'rb_type_control
        '
        Me.rb_type_control.AutoSize = True
        Me.rb_type_control.Location = New System.Drawing.Point(116, 57)
        Me.rb_type_control.Name = "rb_type_control"
        Me.rb_type_control.Size = New System.Drawing.Size(71, 16)
        Me.rb_type_control.TabIndex = 23
        Me.rb_type_control.Text = "类型名称"
        Me.rb_type_control.UseVisualStyleBackColor = True
        '
        'rb_street_control
        '
        Me.rb_street_control.AutoSize = True
        Me.rb_street_control.Location = New System.Drawing.Point(207, 19)
        Me.rb_street_control.Name = "rb_street_control"
        Me.rb_street_control.Size = New System.Drawing.Size(71, 16)
        Me.rb_street_control.TabIndex = 22
        Me.rb_street_control.Text = "街道名称"
        Me.rb_street_control.UseVisualStyleBackColor = True
        '
        'rb_area_control
        '
        Me.rb_area_control.AutoSize = True
        Me.rb_area_control.Location = New System.Drawing.Point(116, 19)
        Me.rb_area_control.Name = "rb_area_control"
        Me.rb_area_control.Size = New System.Drawing.Size(71, 16)
        Me.rb_area_control.TabIndex = 21
        Me.rb_area_control.Text = "区域名称"
        Me.rb_area_control.UseVisualStyleBackColor = True
        '
        'rb_city_control
        '
        Me.rb_city_control.AutoSize = True
        Me.rb_city_control.Location = New System.Drawing.Point(13, 19)
        Me.rb_city_control.Name = "rb_city_control"
        Me.rb_city_control.Size = New System.Drawing.Size(71, 16)
        Me.rb_city_control.TabIndex = 20
        Me.rb_city_control.Text = "城市名称"
        Me.rb_city_control.UseVisualStyleBackColor = True
        '
        'rb_lamp_id_control
        '
        Me.rb_lamp_id_control.AutoSize = True
        Me.rb_lamp_id_control.Checked = True
        Me.rb_lamp_id_control.Location = New System.Drawing.Point(207, 57)
        Me.rb_lamp_id_control.Name = "rb_lamp_id_control"
        Me.rb_lamp_id_control.Size = New System.Drawing.Size(71, 16)
        Me.rb_lamp_id_control.TabIndex = 19
        Me.rb_lamp_id_control.TabStop = True
        Me.rb_lamp_id_control.Text = "终端编号"
        Me.rb_lamp_id_control.UseVisualStyleBackColor = True
        '
        'rb_box_control
        '
        Me.rb_box_control.AutoSize = True
        Me.rb_box_control.Location = New System.Drawing.Point(13, 57)
        Me.rb_box_control.Name = "rb_box_control"
        Me.rb_box_control.Size = New System.Drawing.Size(83, 16)
        Me.rb_box_control.TabIndex = 16
        Me.rb_box_control.Text = "主控箱名称"
        Me.rb_box_control.UseVisualStyleBackColor = True
        '
        'cb_lamp_type_all
        '
        Me.cb_lamp_type_all.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_lamp_type_all.FormattingEnabled = True
        Me.cb_lamp_type_all.Location = New System.Drawing.Point(87, 117)
        Me.cb_lamp_type_all.Name = "cb_lamp_type_all"
        Me.cb_lamp_type_all.Size = New System.Drawing.Size(128, 20)
        Me.cb_lamp_type_all.TabIndex = 111
        '
        'cb_box_all
        '
        Me.cb_box_all.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_box_all.FormattingEnabled = True
        Me.cb_box_all.Location = New System.Drawing.Point(87, 93)
        Me.cb_box_all.Name = "cb_box_all"
        Me.cb_box_all.Size = New System.Drawing.Size(127, 20)
        Me.cb_box_all.TabIndex = 109
        '
        'box_all_string
        '
        Me.box_all_string.AutoSize = True
        Me.box_all_string.BackColor = System.Drawing.Color.Transparent
        Me.box_all_string.Location = New System.Drawing.Point(3, 100)
        Me.box_all_string.Name = "box_all_string"
        Me.box_all_string.Size = New System.Drawing.Size(77, 12)
        Me.box_all_string.TabIndex = 110
        Me.box_all_string.Text = "主控箱名称："
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.cb_city_all)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.cb_area_all)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.cb_street_all)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.lb_lamp_type_id)
        Me.GroupBox1.Controls.Add(Me.lb_lamp_id_start)
        Me.GroupBox1.Controls.Add(Me.cb_lamp_id)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.cb_box_all)
        Me.GroupBox1.Controls.Add(Me.cb_lamp_type_all)
        Me.GroupBox1.Controls.Add(Me.box_all_string)
        Me.GroupBox1.Location = New System.Drawing.Point(13, 93)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(315, 169)
        Me.GroupBox1.TabIndex = 113
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "控制对象"
        '
        'cb_city_all
        '
        Me.cb_city_all.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_city_all.FormattingEnabled = True
        Me.cb_city_all.Location = New System.Drawing.Point(87, 19)
        Me.cb_city_all.Name = "cb_city_all"
        Me.cb_city_all.Size = New System.Drawing.Size(127, 20)
        Me.cb_city_all.TabIndex = 140
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Location = New System.Drawing.Point(15, 21)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(65, 12)
        Me.Label8.TabIndex = 141
        Me.Label8.Text = "城市名称："
        '
        'cb_area_all
        '
        Me.cb_area_all.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_area_all.FormattingEnabled = True
        Me.cb_area_all.Location = New System.Drawing.Point(87, 45)
        Me.cb_area_all.Name = "cb_area_all"
        Me.cb_area_all.Size = New System.Drawing.Size(127, 20)
        Me.cb_area_all.TabIndex = 138
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Location = New System.Drawing.Point(15, 48)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(65, 12)
        Me.Label7.TabIndex = 139
        Me.Label7.Text = "区域名称："
        '
        'cb_street_all
        '
        Me.cb_street_all.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_street_all.FormattingEnabled = True
        Me.cb_street_all.Location = New System.Drawing.Point(87, 69)
        Me.cb_street_all.Name = "cb_street_all"
        Me.cb_street_all.Size = New System.Drawing.Size(127, 20)
        Me.cb_street_all.TabIndex = 136
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Location = New System.Drawing.Point(15, 72)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(65, 12)
        Me.Label6.TabIndex = 137
        Me.Label6.Text = "街道名称："
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(15, 120)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(65, 12)
        Me.Label4.TabIndex = 135
        Me.Label4.Text = "类型名称："
        '
        'lb_lamp_type_id
        '
        Me.lb_lamp_type_id.AutoSize = True
        Me.lb_lamp_type_id.Location = New System.Drawing.Point(243, 17)
        Me.lb_lamp_type_id.Name = "lb_lamp_type_id"
        Me.lb_lamp_type_id.Size = New System.Drawing.Size(53, 12)
        Me.lb_lamp_type_id.TabIndex = 134
        Me.lb_lamp_type_id.Text = "灯的类型"
        '
        'lb_lamp_id_start
        '
        Me.lb_lamp_id_start.AutoSize = True
        Me.lb_lamp_id_start.Location = New System.Drawing.Point(219, 144)
        Me.lb_lamp_id_start.Name = "lb_lamp_id_start"
        Me.lb_lamp_id_start.Size = New System.Drawing.Size(113, 12)
        Me.lb_lamp_id_start.TabIndex = 115
        Me.lb_lamp_id_start.Text = "终端编号的前半部分"
        '
        'cb_lamp_id
        '
        Me.cb_lamp_id.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_lamp_id.FormattingEnabled = True
        Me.cb_lamp_id.Location = New System.Drawing.Point(87, 141)
        Me.cb_lamp_id.Name = "cb_lamp_id"
        Me.cb_lamp_id.Size = New System.Drawing.Size(128, 20)
        Me.cb_lamp_id.TabIndex = 113
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(15, 145)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 12)
        Me.Label2.TabIndex = 114
        Me.Label2.Text = "终端编号："
        '
        'GroupBox4
        '
        Me.GroupBox4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox4.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox4.Controls.Add(Me.dgv_control_list)
        Me.GroupBox4.Location = New System.Drawing.Point(8, 259)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(539, 199)
        Me.GroupBox4.TabIndex = 119
        Me.GroupBox4.TabStop = False
        '
        'dgv_control_list
        '
        Me.dgv_control_list.AllowUserToAddRows = False
        Me.dgv_control_list.BackgroundColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_control_list.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv_control_list.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_control_list.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.control_type, Me.control_obj, Me.control_method, Me.time})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv_control_list.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgv_control_list.Location = New System.Drawing.Point(6, 20)
        Me.dgv_control_list.Name = "dgv_control_list"
        Me.dgv_control_list.RowTemplate.Height = 23
        Me.dgv_control_list.Size = New System.Drawing.Size(526, 174)
        Me.dgv_control_list.TabIndex = 121
        '
        'control_type
        '
        Me.control_type.HeaderText = "控制类型"
        Me.control_type.Name = "control_type"
        Me.control_type.Width = 120
        '
        'control_obj
        '
        Me.control_obj.HeaderText = "控制对象"
        Me.control_obj.Name = "control_obj"
        Me.control_obj.Width = 120
        '
        'control_method
        '
        Me.control_method.HeaderText = "控制方法"
        Me.control_method.Name = "control_method"
        Me.control_method.Width = 150
        '
        'time
        '
        Me.time.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.time.HeaderText = "间隔时间"
        Me.time.Name = "time"
        '
        'bt_add_method
        '
        Me.bt_add_method.Location = New System.Drawing.Point(405, 231)
        Me.bt_add_method.Name = "bt_add_method"
        Me.bt_add_method.Size = New System.Drawing.Size(75, 23)
        Me.bt_add_method.TabIndex = 118
        Me.bt_add_method.Text = "添加"
        Me.bt_add_method.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(10, 469)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 12)
        Me.Label1.TabIndex = 120
        Me.Label1.Text = "模式名称："
        '
        'tb_mod_title
        '
        Me.tb_mod_title.Location = New System.Drawing.Point(109, 464)
        Me.tb_mod_title.Name = "tb_mod_title"
        Me.tb_mod_title.Size = New System.Drawing.Size(100, 21)
        Me.tb_mod_title.TabIndex = 121
        '
        'bt_add_mod
        '
        Me.bt_add_mod.Location = New System.Drawing.Point(285, 464)
        Me.bt_add_mod.Name = "bt_add_mod"
        Me.bt_add_mod.Size = New System.Drawing.Size(75, 23)
        Me.bt_add_mod.TabIndex = 122
        Me.bt_add_mod.Text = "增加"
        Me.bt_add_mod.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.Controls.Add(Me.rb_type_double_open)
        Me.GroupBox2.Controls.Add(Me.rb_type_single_open)
        Me.GroupBox2.Controls.Add(Me.rb_all_close)
        Me.GroupBox2.Controls.Add(Me.rb_all_open)
        Me.GroupBox2.Location = New System.Drawing.Point(333, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(207, 123)
        Me.GroupBox2.TabIndex = 123
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "控制方法"
        '
        'rb_type_double_open
        '
        Me.rb_type_double_open.AutoSize = True
        Me.rb_type_double_open.Location = New System.Drawing.Point(128, 81)
        Me.rb_type_double_open.Name = "rb_type_double_open"
        Me.rb_type_double_open.Size = New System.Drawing.Size(59, 16)
        Me.rb_type_double_open.TabIndex = 11
        Me.rb_type_double_open.Text = "双号开"
        Me.rb_type_double_open.UseVisualStyleBackColor = True
        '
        'rb_type_single_open
        '
        Me.rb_type_single_open.AutoSize = True
        Me.rb_type_single_open.Location = New System.Drawing.Point(20, 81)
        Me.rb_type_single_open.Name = "rb_type_single_open"
        Me.rb_type_single_open.Size = New System.Drawing.Size(59, 16)
        Me.rb_type_single_open.TabIndex = 10
        Me.rb_type_single_open.Text = "单号开"
        Me.rb_type_single_open.UseVisualStyleBackColor = True
        '
        'rb_all_close
        '
        Me.rb_all_close.AutoSize = True
        Me.rb_all_close.Location = New System.Drawing.Point(128, 30)
        Me.rb_all_close.Name = "rb_all_close"
        Me.rb_all_close.Size = New System.Drawing.Size(47, 16)
        Me.rb_all_close.TabIndex = 9
        Me.rb_all_close.Text = "全闭"
        Me.rb_all_close.UseVisualStyleBackColor = True
        '
        'rb_all_open
        '
        Me.rb_all_open.AutoSize = True
        Me.rb_all_open.Checked = True
        Me.rb_all_open.Location = New System.Drawing.Point(20, 30)
        Me.rb_all_open.Name = "rb_all_open"
        Me.rb_all_open.Size = New System.Drawing.Size(47, 16)
        Me.rb_all_open.TabIndex = 8
        Me.rb_all_open.TabStop = True
        Me.rb_all_open.Text = "全开"
        Me.rb_all_open.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Controls.Add(Me.cb_next_time)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Location = New System.Drawing.Point(333, 141)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(207, 53)
        Me.GroupBox3.TabIndex = 124
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "间隔时间"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(180, 26)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(17, 12)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "秒"
        '
        'cb_next_time
        '
        Me.cb_next_time.FormattingEnabled = True
        Me.cb_next_time.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5", "10", "15", "20", "25", "30", "60", "120"})
        Me.cb_next_time.Location = New System.Drawing.Point(81, 23)
        Me.cb_next_time.Name = "cb_next_time"
        Me.cb_next_time.Size = New System.Drawing.Size(93, 20)
        Me.cb_next_time.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(10, 26)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 12)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "间隔时间："
        '
        '增加节日模式
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(559, 499)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.bt_add_mod)
        Me.Controls.Add(Me.bt_add_method)
        Me.Controls.Add(Me.tb_mod_title)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.control_type_groupbox)
        Me.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "增加节日模式"
        Me.Text = "增加用户自定义模式"
        Me.control_type_groupbox.ResumeLayout(False)
        Me.control_type_groupbox.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        CType(Me.dgv_control_list, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents control_type_groupbox As System.Windows.Forms.GroupBox
    Friend WithEvents rb_box_control As System.Windows.Forms.RadioButton
    Friend WithEvents cb_lamp_type_all As System.Windows.Forms.ComboBox
    Friend WithEvents cb_box_all As System.Windows.Forms.ComboBox
    Friend WithEvents box_all_string As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents bt_add_method As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tb_mod_title As System.Windows.Forms.TextBox
    Friend WithEvents bt_add_mod As System.Windows.Forms.Button
    Friend WithEvents dgv_control_list As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents rb_lamp_id_control As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents cb_lamp_id As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lb_lamp_id_start As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cb_next_time As System.Windows.Forms.ComboBox
    Friend WithEvents lb_lamp_type_id As System.Windows.Forms.Label
    Friend WithEvents cb_city_all As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cb_area_all As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cb_street_all As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents rb_area_control As System.Windows.Forms.RadioButton
    Friend WithEvents rb_city_control As System.Windows.Forms.RadioButton
    Friend WithEvents rb_street_control As System.Windows.Forms.RadioButton
    Friend WithEvents rb_type_control As System.Windows.Forms.RadioButton
    Friend WithEvents rb_type_double_open As System.Windows.Forms.RadioButton
    Friend WithEvents rb_type_single_open As System.Windows.Forms.RadioButton
    Friend WithEvents rb_all_close As System.Windows.Forms.RadioButton
    Friend WithEvents rb_all_open As System.Windows.Forms.RadioButton
    Friend WithEvents control_type As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents control_obj As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents control_method As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents time As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
