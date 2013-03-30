<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 设备控制面板
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(设备控制面板))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.rb_tiaoguang = New System.Windows.Forms.RadioButton()
        Me.rb_normal = New System.Windows.Forms.RadioButton()
        Me.gb_tiaoguang = New System.Windows.Forms.GroupBox()
        Me.lb_string2 = New System.Windows.Forms.Label()
        Me.rb_diangan = New System.Windows.Forms.RadioButton()
        Me.rb_power = New System.Windows.Forms.RadioButton()
        Me.lb_string1 = New System.Windows.Forms.Label()
        Me.lb_power = New System.Windows.Forms.TextBox()
        Me.cb_diangan = New System.Windows.Forms.ComboBox()
        Me.bt_reoperation = New System.Windows.Forms.Button()
        Me.cb_auto_close = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cb_auto_waittime = New System.Windows.Forms.ComboBox()
        Me.bt_selectall = New System.Windows.Forms.Button()
        Me.dgv_hand_controllist = New System.Windows.Forms.DataGridView()
        Me.checkid = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.control_obj = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.result = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.level_num = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.type_id_string = New System.Windows.Forms.Label()
        Me.bt_operation = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.cb_grouporder_list = New System.Windows.Forms.ComboBox()
        Me.rb_order_group3 = New System.Windows.Forms.RadioButton()
        Me.cb_waittime = New System.Windows.Forms.ComboBox()
        Me.waittime_string = New System.Windows.Forms.Label()
        Me.cb_startid_6 = New System.Windows.Forms.ComboBox()
        Me.rb_denggan1_1 = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lb_start_id = New System.Windows.Forms.TextBox()
        Me.rb_close_all = New System.Windows.Forms.RadioButton()
        Me.rb_open1_2 = New System.Windows.Forms.RadioButton()
        Me.rb_open_all = New System.Windows.Forms.RadioButton()
        Me.rb_double_open = New System.Windows.Forms.RadioButton()
        Me.rb_single_open = New System.Windows.Forms.RadioButton()
        Me.bt_clear = New System.Windows.Forms.Button()
        Me.bt_add_controlinf = New System.Windows.Forms.Button()
        Me.tv_control_list = New System.Windows.Forms.TreeView()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.BackgroundWorkergroupcontrol = New System.ComponentModel.BackgroundWorker()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.gb_tiaoguang.SuspendLayout()
        CType(Me.dgv_hand_controllist, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.GroupBox4)
        Me.GroupBox1.Controls.Add(Me.gb_tiaoguang)
        Me.GroupBox1.Controls.Add(Me.bt_reoperation)
        Me.GroupBox1.Controls.Add(Me.cb_auto_close)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.cb_auto_waittime)
        Me.GroupBox1.Controls.Add(Me.bt_selectall)
        Me.GroupBox1.Controls.Add(Me.dgv_hand_controllist)
        Me.GroupBox1.Controls.Add(Me.type_id_string)
        Me.GroupBox1.Controls.Add(Me.bt_operation)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.bt_clear)
        Me.GroupBox1.Controls.Add(Me.bt_add_controlinf)
        Me.GroupBox1.Controls.Add(Me.tv_control_list)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(821, 540)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "控制节点信息"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.rb_tiaoguang)
        Me.GroupBox4.Controls.Add(Me.rb_normal)
        Me.GroupBox4.Location = New System.Drawing.Point(422, 371)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(139, 82)
        Me.GroupBox4.TabIndex = 146
        Me.GroupBox4.TabStop = False
        '
        'rb_tiaoguang
        '
        Me.rb_tiaoguang.AutoSize = True
        Me.rb_tiaoguang.Location = New System.Drawing.Point(6, 55)
        Me.rb_tiaoguang.Name = "rb_tiaoguang"
        Me.rb_tiaoguang.Size = New System.Drawing.Size(59, 16)
        Me.rb_tiaoguang.TabIndex = 132
        Me.rb_tiaoguang.Text = "调光型"
        Me.rb_tiaoguang.UseVisualStyleBackColor = True
        '
        'rb_normal
        '
        Me.rb_normal.AutoSize = True
        Me.rb_normal.Checked = True
        Me.rb_normal.Location = New System.Drawing.Point(6, 15)
        Me.rb_normal.Name = "rb_normal"
        Me.rb_normal.Size = New System.Drawing.Size(71, 16)
        Me.rb_normal.TabIndex = 131
        Me.rb_normal.TabStop = True
        Me.rb_normal.Text = "非调光型"
        Me.rb_normal.UseVisualStyleBackColor = True
        '
        'gb_tiaoguang
        '
        Me.gb_tiaoguang.Controls.Add(Me.lb_string2)
        Me.gb_tiaoguang.Controls.Add(Me.rb_diangan)
        Me.gb_tiaoguang.Controls.Add(Me.rb_power)
        Me.gb_tiaoguang.Controls.Add(Me.lb_string1)
        Me.gb_tiaoguang.Controls.Add(Me.lb_power)
        Me.gb_tiaoguang.Controls.Add(Me.cb_diangan)
        Me.gb_tiaoguang.Enabled = False
        Me.gb_tiaoguang.Location = New System.Drawing.Point(567, 371)
        Me.gb_tiaoguang.Name = "gb_tiaoguang"
        Me.gb_tiaoguang.Size = New System.Drawing.Size(247, 82)
        Me.gb_tiaoguang.TabIndex = 145
        Me.gb_tiaoguang.TabStop = False
        '
        'lb_string2
        '
        Me.lb_string2.AutoSize = True
        Me.lb_string2.Location = New System.Drawing.Point(120, 62)
        Me.lb_string2.Name = "lb_string2"
        Me.lb_string2.Size = New System.Drawing.Size(41, 12)
        Me.lb_string2.TabIndex = 131
        Me.lb_string2.Text = "功率："
        Me.lb_string2.Visible = False
        '
        'rb_diangan
        '
        Me.rb_diangan.AutoSize = True
        Me.rb_diangan.Checked = True
        Me.rb_diangan.Location = New System.Drawing.Point(20, 17)
        Me.rb_diangan.Name = "rb_diangan"
        Me.rb_diangan.Size = New System.Drawing.Size(83, 16)
        Me.rb_diangan.TabIndex = 130
        Me.rb_diangan.TabStop = True
        Me.rb_diangan.Text = "电感调光型"
        Me.rb_diangan.UseVisualStyleBackColor = True
        '
        'rb_power
        '
        Me.rb_power.AutoSize = True
        Me.rb_power.Location = New System.Drawing.Point(20, 60)
        Me.rb_power.Name = "rb_power"
        Me.rb_power.Size = New System.Drawing.Size(83, 16)
        Me.rb_power.TabIndex = 0
        Me.rb_power.Text = "电子调光型"
        Me.rb_power.UseVisualStyleBackColor = True
        '
        'lb_string1
        '
        Me.lb_string1.AutoSize = True
        Me.lb_string1.Location = New System.Drawing.Point(120, 19)
        Me.lb_string1.Name = "lb_string1"
        Me.lb_string1.Size = New System.Drawing.Size(41, 12)
        Me.lb_string1.TabIndex = 129
        Me.lb_string1.Text = "功率："
        '
        'lb_power
        '
        Me.lb_power.Location = New System.Drawing.Point(165, 55)
        Me.lb_power.Name = "lb_power"
        Me.lb_power.Size = New System.Drawing.Size(58, 21)
        Me.lb_power.TabIndex = 128
        Me.lb_power.Text = "100"
        Me.lb_power.Visible = False
        '
        'cb_diangan
        '
        Me.cb_diangan.FormattingEnabled = True
        Me.cb_diangan.Items.AddRange(New Object() {"全功率", "半功率"})
        Me.cb_diangan.Location = New System.Drawing.Point(167, 16)
        Me.cb_diangan.Name = "cb_diangan"
        Me.cb_diangan.Size = New System.Drawing.Size(56, 20)
        Me.cb_diangan.TabIndex = 127
        Me.cb_diangan.Text = "全功率"
        '
        'bt_reoperation
        '
        Me.bt_reoperation.Enabled = False
        Me.bt_reoperation.Location = New System.Drawing.Point(616, 514)
        Me.bt_reoperation.Name = "bt_reoperation"
        Me.bt_reoperation.Size = New System.Drawing.Size(64, 20)
        Me.bt_reoperation.TabIndex = 144
        Me.bt_reoperation.Text = "失败重发"
        Me.bt_reoperation.UseVisualStyleBackColor = True
        '
        'cb_auto_close
        '
        Me.cb_auto_close.AutoSize = True
        Me.cb_auto_close.Location = New System.Drawing.Point(422, 471)
        Me.cb_auto_close.Name = "cb_auto_close"
        Me.cb_auto_close.Size = New System.Drawing.Size(96, 16)
        Me.cb_auto_close.TabIndex = 143
        Me.cb_auto_close.Text = "延时自动关灯"
        Me.cb_auto_close.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(614, 472)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(41, 12)
        Me.Label2.TabIndex = 142
        Me.Label2.Text = "（分）"
        '
        'cb_auto_waittime
        '
        Me.cb_auto_waittime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_auto_waittime.Enabled = False
        Me.cb_auto_waittime.FormattingEnabled = True
        Me.cb_auto_waittime.Items.AddRange(New Object() {"10", "20", "30", "40", "50", "60"})
        Me.cb_auto_waittime.Location = New System.Drawing.Point(537, 467)
        Me.cb_auto_waittime.Name = "cb_auto_waittime"
        Me.cb_auto_waittime.Size = New System.Drawing.Size(66, 20)
        Me.cb_auto_waittime.TabIndex = 141
        '
        'bt_selectall
        '
        Me.bt_selectall.Location = New System.Drawing.Point(329, 98)
        Me.bt_selectall.Name = "bt_selectall"
        Me.bt_selectall.Size = New System.Drawing.Size(64, 20)
        Me.bt_selectall.TabIndex = 139
        Me.bt_selectall.Text = "全选"
        Me.bt_selectall.UseVisualStyleBackColor = True
        '
        'dgv_hand_controllist
        '
        Me.dgv_hand_controllist.AllowUserToAddRows = False
        Me.dgv_hand_controllist.BackgroundColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_hand_controllist.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv_hand_controllist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_hand_controllist.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.checkid, Me.control_obj, Me.result, Me.level_num})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv_hand_controllist.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgv_hand_controllist.Location = New System.Drawing.Point(415, 19)
        Me.dgv_hand_controllist.Name = "dgv_hand_controllist"
        Me.dgv_hand_controllist.RowHeadersVisible = False
        Me.dgv_hand_controllist.RowTemplate.Height = 23
        Me.dgv_hand_controllist.Size = New System.Drawing.Size(397, 218)
        Me.dgv_hand_controllist.TabIndex = 138
        '
        'checkid
        '
        Me.checkid.HeaderText = ""
        Me.checkid.Name = "checkid"
        Me.checkid.Width = 20
        '
        'control_obj
        '
        Me.control_obj.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.control_obj.HeaderText = "控制对象"
        Me.control_obj.Name = "control_obj"
        '
        'result
        '
        Me.result.HeaderText = "控制结果"
        Me.result.Name = "result"
        Me.result.Width = 200
        '
        'level_num
        '
        Me.level_num.HeaderText = "级别"
        Me.level_num.Name = "level_num"
        Me.level_num.Visible = False
        '
        'type_id_string
        '
        Me.type_id_string.AutoSize = True
        Me.type_id_string.Location = New System.Drawing.Point(735, 518)
        Me.type_id_string.Name = "type_id_string"
        Me.type_id_string.Size = New System.Drawing.Size(53, 12)
        Me.type_id_string.TabIndex = 137
        Me.type_id_string.Text = "类型编号"
        Me.type_id_string.Visible = False
        '
        'bt_operation
        '
        Me.bt_operation.Location = New System.Drawing.Point(496, 514)
        Me.bt_operation.Name = "bt_operation"
        Me.bt_operation.Size = New System.Drawing.Size(64, 20)
        Me.bt_operation.TabIndex = 11
        Me.bt_operation.Text = "操作"
        Me.bt_operation.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cb_grouporder_list)
        Me.GroupBox2.Controls.Add(Me.rb_order_group3)
        Me.GroupBox2.Controls.Add(Me.cb_waittime)
        Me.GroupBox2.Controls.Add(Me.waittime_string)
        Me.GroupBox2.Controls.Add(Me.cb_startid_6)
        Me.GroupBox2.Controls.Add(Me.rb_denggan1_1)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.lb_start_id)
        Me.GroupBox2.Controls.Add(Me.rb_close_all)
        Me.GroupBox2.Controls.Add(Me.rb_open1_2)
        Me.GroupBox2.Controls.Add(Me.rb_open_all)
        Me.GroupBox2.Controls.Add(Me.rb_double_open)
        Me.GroupBox2.Controls.Add(Me.rb_single_open)
        Me.GroupBox2.Location = New System.Drawing.Point(422, 243)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(393, 122)
        Me.GroupBox2.TabIndex = 10
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "控制方式"
        '
        'cb_grouporder_list
        '
        Me.cb_grouporder_list.FormattingEnabled = True
        Me.cb_grouporder_list.Location = New System.Drawing.Point(115, 93)
        Me.cb_grouporder_list.Name = "cb_grouporder_list"
        Me.cb_grouporder_list.Size = New System.Drawing.Size(156, 20)
        Me.cb_grouporder_list.TabIndex = 22
        Me.cb_grouporder_list.Visible = False
        '
        'rb_order_group3
        '
        Me.rb_order_group3.AutoSize = True
        Me.rb_order_group3.Location = New System.Drawing.Point(5, 97)
        Me.rb_order_group3.Name = "rb_order_group3"
        Me.rb_order_group3.Size = New System.Drawing.Size(71, 16)
        Me.rb_order_group3.TabIndex = 21
        Me.rb_order_group3.TabStop = True
        Me.rb_order_group3.Text = "组合控制"
        Me.rb_order_group3.UseVisualStyleBackColor = True
        '
        'cb_waittime
        '
        Me.cb_waittime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_waittime.FormattingEnabled = True
        Me.cb_waittime.Items.AddRange(New Object() {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10"})
        Me.cb_waittime.Location = New System.Drawing.Point(385, 93)
        Me.cb_waittime.Name = "cb_waittime"
        Me.cb_waittime.Size = New System.Drawing.Size(48, 20)
        Me.cb_waittime.TabIndex = 20
        Me.cb_waittime.Visible = False
        '
        'waittime_string
        '
        Me.waittime_string.AutoSize = True
        Me.waittime_string.Location = New System.Drawing.Point(266, 78)
        Me.waittime_string.Name = "waittime_string"
        Me.waittime_string.Size = New System.Drawing.Size(131, 12)
        Me.waittime_string.TabIndex = 19
        Me.waittime_string.Text = "延缓时间(单位：0.5秒)"
        Me.waittime_string.Visible = False
        '
        'cb_startid_6
        '
        Me.cb_startid_6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_startid_6.FormattingEnabled = True
        Me.cb_startid_6.Items.AddRange(New Object() {"0", "1"})
        Me.cb_startid_6.Location = New System.Drawing.Point(316, 89)
        Me.cb_startid_6.Name = "cb_startid_6"
        Me.cb_startid_6.Size = New System.Drawing.Size(40, 20)
        Me.cb_startid_6.TabIndex = 18
        Me.cb_startid_6.Visible = False
        '
        'rb_denggan1_1
        '
        Me.rb_denggan1_1.AutoSize = True
        Me.rb_denggan1_1.Location = New System.Drawing.Point(328, 69)
        Me.rb_denggan1_1.Name = "rb_denggan1_1"
        Me.rb_denggan1_1.Size = New System.Drawing.Size(83, 16)
        Me.rb_denggan1_1.TabIndex = 17
        Me.rb_denggan1_1.TabStop = True
        Me.rb_denggan1_1.Text = "灯杆隔盏开"
        Me.rb_denggan1_1.UseVisualStyleBackColor = True
        Me.rb_denggan1_1.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(305, 37)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 12)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "起始编号："
        Me.Label1.Visible = False
        '
        'lb_start_id
        '
        Me.lb_start_id.Enabled = False
        Me.lb_start_id.Location = New System.Drawing.Point(385, 34)
        Me.lb_start_id.Name = "lb_start_id"
        Me.lb_start_id.Size = New System.Drawing.Size(33, 21)
        Me.lb_start_id.TabIndex = 10
        Me.lb_start_id.Visible = False
        '
        'rb_close_all
        '
        Me.rb_close_all.AutoSize = True
        Me.rb_close_all.Location = New System.Drawing.Point(201, 19)
        Me.rb_close_all.Name = "rb_close_all"
        Me.rb_close_all.Size = New System.Drawing.Size(71, 16)
        Me.rb_close_all.TabIndex = 6
        Me.rb_close_all.Text = "关    灯"
        Me.rb_close_all.UseVisualStyleBackColor = True
        '
        'rb_open1_2
        '
        Me.rb_open1_2.AutoSize = True
        Me.rb_open1_2.Location = New System.Drawing.Point(328, 19)
        Me.rb_open1_2.Name = "rb_open1_2"
        Me.rb_open1_2.Size = New System.Drawing.Size(71, 16)
        Me.rb_open1_2.TabIndex = 9
        Me.rb_open1_2.Text = "隔两盏开"
        Me.rb_open1_2.UseVisualStyleBackColor = True
        Me.rb_open1_2.Visible = False
        '
        'rb_open_all
        '
        Me.rb_open_all.AutoSize = True
        Me.rb_open_all.Checked = True
        Me.rb_open_all.Location = New System.Drawing.Point(5, 19)
        Me.rb_open_all.Name = "rb_open_all"
        Me.rb_open_all.Size = New System.Drawing.Size(71, 16)
        Me.rb_open_all.TabIndex = 5
        Me.rb_open_all.TabStop = True
        Me.rb_open_all.Text = "开    灯"
        Me.rb_open_all.UseVisualStyleBackColor = True
        '
        'rb_double_open
        '
        Me.rb_double_open.AutoSize = True
        Me.rb_double_open.Location = New System.Drawing.Point(201, 59)
        Me.rb_double_open.Name = "rb_double_open"
        Me.rb_double_open.Size = New System.Drawing.Size(71, 16)
        Me.rb_double_open.TabIndex = 8
        Me.rb_double_open.Text = "双 号 开"
        Me.rb_double_open.UseVisualStyleBackColor = True
        '
        'rb_single_open
        '
        Me.rb_single_open.AutoSize = True
        Me.rb_single_open.Location = New System.Drawing.Point(7, 59)
        Me.rb_single_open.Name = "rb_single_open"
        Me.rb_single_open.Size = New System.Drawing.Size(71, 16)
        Me.rb_single_open.TabIndex = 7
        Me.rb_single_open.Text = "单 号 开"
        Me.rb_single_open.UseVisualStyleBackColor = True
        '
        'bt_clear
        '
        Me.bt_clear.Location = New System.Drawing.Point(329, 153)
        Me.bt_clear.Name = "bt_clear"
        Me.bt_clear.Size = New System.Drawing.Size(64, 20)
        Me.bt_clear.TabIndex = 4
        Me.bt_clear.Text = "移除"
        Me.bt_clear.UseVisualStyleBackColor = True
        '
        'bt_add_controlinf
        '
        Me.bt_add_controlinf.Location = New System.Drawing.Point(329, 46)
        Me.bt_add_controlinf.Name = "bt_add_controlinf"
        Me.bt_add_controlinf.Size = New System.Drawing.Size(64, 20)
        Me.bt_add_controlinf.TabIndex = 3
        Me.bt_add_controlinf.Text = "增加>>>>"
        Me.bt_add_controlinf.UseVisualStyleBackColor = True
        '
        'tv_control_list
        '
        Me.tv_control_list.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.tv_control_list.CheckBoxes = True
        Me.tv_control_list.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.tv_control_list.ImageIndex = 0
        Me.tv_control_list.ImageList = Me.ImageList1
        Me.tv_control_list.Location = New System.Drawing.Point(6, 19)
        Me.tv_control_list.Name = "tv_control_list"
        Me.tv_control_list.SelectedImageIndex = 0
        Me.tv_control_list.Size = New System.Drawing.Size(301, 516)
        Me.tv_control_list.TabIndex = 1
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "desk6副本.png")
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 549)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Padding = New System.Windows.Forms.Padding(1, 0, 12, 0)
        Me.StatusStrip1.Size = New System.Drawing.Size(845, 22)
        Me.StatusStrip1.TabIndex = 1
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'BackgroundWorkergroupcontrol
        '
        Me.BackgroundWorkergroupcontrol.WorkerReportsProgress = True
        Me.BackgroundWorkergroupcontrol.WorkerSupportsCancellation = True
        '
        '设备控制面板
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(845, 571)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "设备控制面板"
        Me.Text = "设备控制面板"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.gb_tiaoguang.ResumeLayout(False)
        Me.gb_tiaoguang.PerformLayout()
        CType(Me.dgv_hand_controllist, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents tv_control_list As System.Windows.Forms.TreeView
    Friend WithEvents rb_open_all As System.Windows.Forms.RadioButton
    Friend WithEvents bt_clear As System.Windows.Forms.Button
    Friend WithEvents bt_add_controlinf As System.Windows.Forms.Button
    Friend WithEvents rb_single_open As System.Windows.Forms.RadioButton
    Friend WithEvents rb_close_all As System.Windows.Forms.RadioButton
    Friend WithEvents rb_open1_2 As System.Windows.Forms.RadioButton
    Friend WithEvents rb_double_open As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents bt_operation As System.Windows.Forms.Button
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents cb_diangan As System.Windows.Forms.ComboBox
    Friend WithEvents lb_power As System.Windows.Forms.TextBox
    Friend WithEvents type_id_string As System.Windows.Forms.Label
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lb_start_id As System.Windows.Forms.TextBox
    Friend WithEvents cb_waittime As System.Windows.Forms.ComboBox
    Friend WithEvents waittime_string As System.Windows.Forms.Label
    Friend WithEvents cb_startid_6 As System.Windows.Forms.ComboBox
    Friend WithEvents rb_denggan1_1 As System.Windows.Forms.RadioButton
    Friend WithEvents BackgroundWorkergroupcontrol As System.ComponentModel.BackgroundWorker
    Friend WithEvents rb_order_group3 As System.Windows.Forms.RadioButton
    Friend WithEvents cb_grouporder_list As System.Windows.Forms.ComboBox
    Friend WithEvents dgv_hand_controllist As System.Windows.Forms.DataGridView
    Friend WithEvents bt_selectall As System.Windows.Forms.Button
    Friend WithEvents checkid As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents control_obj As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents result As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents level_num As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cb_auto_waittime As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cb_auto_close As System.Windows.Forms.CheckBox
    Friend WithEvents bt_reoperation As System.Windows.Forms.Button
    Friend WithEvents lb_string1 As System.Windows.Forms.Label
    Friend WithEvents gb_tiaoguang As System.Windows.Forms.GroupBox
    Friend WithEvents rb_power As System.Windows.Forms.RadioButton
    Friend WithEvents rb_diangan As System.Windows.Forms.RadioButton
    Friend WithEvents rb_normal As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents rb_tiaoguang As System.Windows.Forms.RadioButton
    Friend WithEvents lb_string2 As System.Windows.Forms.Label
End Class
