<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 增加终端
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
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(增加终端))
        Me.cb_box_add = New System.Windows.Forms.ComboBox()
        Me.box_add_string = New System.Windows.Forms.Label()
        Me.lamp_id_string = New System.Windows.Forms.Label()
        Me.tb_lamp_id = New System.Windows.Forms.TextBox()
        Me.bt_add_new = New System.Windows.Forms.Button()
        Me.dgv_old_lamp_list = New System.Windows.Forms.DataGridView()
        Me.LampstreetBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Street_add = New streetlamp.street_add()
        Me.tb_lamp_num = New System.Windows.Forms.TextBox()
        Me.lamp_num_string = New System.Windows.Forms.Label()
        Me.lb_lamp_id_start = New System.Windows.Forms.Label()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.lamp_inf_text = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lamp_type_new = New System.Windows.Forms.GroupBox()
        Me.LampPointInfor = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.rb_add_lampid = New System.Windows.Forms.RadioButton()
        Me.rb_add_lamptype = New System.Windows.Forms.RadioButton()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.cb_lampvision = New System.Windows.Forms.ComboBox()
        Me.gb_draw_method = New System.Windows.Forms.GroupBox()
        Me.rb_addnext_num = New System.Windows.Forms.RadioButton()
        Me.rb_addnexttwo_num = New System.Windows.Forms.RadioButton()
        Me.rb_addnextthree_num = New System.Windows.Forms.RadioButton()
        Me.gb_drawpos = New System.Windows.Forms.GroupBox()
        Me.rb_xadd = New System.Windows.Forms.RadioButton()
        Me.rb_yadd = New System.Windows.Forms.RadioButton()
        Me.lb_jiechuqi_full_id = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cb_street_all = New System.Windows.Forms.ComboBox()
        Me.cb_area_add = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cb_city_add = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cb_lamp_type = New System.Windows.Forms.ComboBox()
        Me.lb_lamp_type_id_add = New System.Windows.Forms.Label()
        Me.tb_end_pos_y = New System.Windows.Forms.TextBox()
        Me.tb_end_pos_x = New System.Windows.Forms.TextBox()
        Me.tb_start_pos_y = New System.Windows.Forms.TextBox()
        Me.end_pos_string = New System.Windows.Forms.Label()
        Me.tb_start_pos_x = New System.Windows.Forms.TextBox()
        Me.start_pos_string = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.rb_check_3 = New System.Windows.Forms.RadioButton()
        Me.rb_check_2 = New System.Windows.Forms.RadioButton()
        Me.rb_check_1 = New System.Windows.Forms.RadioButton()
        Me.rb_check_all = New System.Windows.Forms.RadioButton()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cb_delete_city = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cb_delete_area = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cb_delete_street = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lb_lamp_type_id_del = New System.Windows.Forms.Label()
        Me.bt_delete_confirm = New System.Windows.Forms.Button()
        Me.rtb_delete_detail = New System.Windows.Forms.RichTextBox()
        Me.bt_delete_lamp = New System.Windows.Forms.Button()
        Me.cb_delete_lamp_id_start = New System.Windows.Forms.ComboBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.rb_area_control = New System.Windows.Forms.RadioButton()
        Me.rb_street_control = New System.Windows.Forms.RadioButton()
        Me.rb_lamp_type_control = New System.Windows.Forms.RadioButton()
        Me.rb_city_control = New System.Windows.Forms.RadioButton()
        Me.rb_lamp_id_control = New System.Windows.Forms.RadioButton()
        Me.rb_box_name_control = New System.Windows.Forms.RadioButton()
        Me.lb_delete_lamp_id_end_start = New System.Windows.Forms.Label()
        Me.delete_control_box_name_string = New System.Windows.Forms.Label()
        Me.lb_delete_lamp_id_start_start = New System.Windows.Forms.Label()
        Me.cb_delete_lamp_id_end = New System.Windows.Forms.ComboBox()
        Me.cb_delete_control_box_name = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cb_delete_lamp_type = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.bt_choose_lamp = New System.Windows.Forms.Button()
        Me.Lamp_streetTableAdapter = New streetlamp.street_addTableAdapters.lamp_streetTableAdapter()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.bt_sendboardnum = New System.Windows.Forms.Button()
        Me.cb_controlbox_name = New System.Windows.Forms.ComboBox()
        Me.rb_checkall = New System.Windows.Forms.RadioButton()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.tb_bianbi_value = New System.Windows.Forms.TextBox()
        Me.bt_set_huilu = New System.Windows.Forms.Button()
        Me.GroupBoxkaiguanliang = New System.Windows.Forms.GroupBox()
        Me.chb_checkall = New System.Windows.Forms.CheckBox()
        Me.GroupBoxAlarm = New System.Windows.Forms.GroupBox()
        Me.bt_set_alarmvalue = New System.Windows.Forms.Button()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.cb_shiliang_list = New System.Windows.Forms.ComboBox()
        Me.cb_tiaobian_list = New System.Windows.Forms.ComboBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.chb_set_alarm = New System.Windows.Forms.CheckBox()
        Me.bt_save_kaiguan = New System.Windows.Forms.Button()
        Me.bt_del_kaiguan = New System.Windows.Forms.Button()
        Me.dgv_kaiguanliang_list = New System.Windows.Forms.DataGridView()
        Me.kgcheckid = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.kaiguan_name = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.kaiguan_tag = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.alarm = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.kgcontrol_box_name = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.kgid = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.KaiguanlistBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.KaiguanDataSet = New streetlamp.kaiguanDataSet()
        Me.GroupBoxmoniliang = New System.Windows.Forms.GroupBox()
        Me.chb_allhuilu = New System.Windows.Forms.CheckBox()
        Me.bt_save_huilu = New System.Windows.Forms.Button()
        Me.dgv_huilu_list = New System.Windows.Forms.DataGridView()
        Me.checkid = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.huilu_id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.jiechuqi = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.bianbi = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.current_alarmtop = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.current_alarmbot = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.presure_type = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.information = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HuiluinfBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.EditDataSet = New streetlamp.EditDataSet()
        Me.bt_del_huilu = New System.Windows.Forms.Button()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.GroupBoxhuilu = New System.Windows.Forms.GroupBox()
        Me.clb_huilu_idlist = New System.Windows.Forms.CheckedListBox()
        Me.tv_box_inf_list = New System.Windows.Forms.TreeView()
        Me.huilu_group_list = New System.Windows.Forms.GroupBox()
        Me.rb_k6_list = New System.Windows.Forms.RadioButton()
        Me.rb_k5_list = New System.Windows.Forms.RadioButton()
        Me.rb_k4_list = New System.Windows.Forms.RadioButton()
        Me.rb_k3_list = New System.Windows.Forms.RadioButton()
        Me.rb_k2_list = New System.Windows.Forms.RadioButton()
        Me.rb_k1_list = New System.Windows.Forms.RadioButton()
        Me.bt_send_alarmdata = New System.Windows.Forms.Button()
        Me.rb_box_check = New System.Windows.Forms.RadioButton()
        Me.bt_send_kaiguan = New System.Windows.Forms.Button()
        Me.bt_send_huiluinf = New System.Windows.Forms.Button()
        Me.Huilu_infTableAdapter = New streetlamp.EditDataSetTableAdapters.huilu_infTableAdapter()
        Me.Kaiguan_listTableAdapter = New streetlamp.kaiguanDataSetTableAdapters.kaiguan_listTableAdapter()
        Me.ToolTip_lamp = New System.Windows.Forms.ToolTip(Me.components)
        Me.check = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.control_box_name = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.type_string = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lamp_id_part = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lamp_id_value = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lamp_pointinfor = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.dgv_old_lamp_list, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LampstreetBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Street_add, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip1.SuspendLayout()
        Me.lamp_type_new.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.gb_draw_method.SuspendLayout()
        Me.gb_drawpos.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBoxkaiguanliang.SuspendLayout()
        Me.GroupBoxAlarm.SuspendLayout()
        CType(Me.dgv_kaiguanliang_list, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.KaiguanlistBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.KaiguanDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBoxmoniliang.SuspendLayout()
        CType(Me.dgv_huilu_list, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.HuiluinfBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EditDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBoxhuilu.SuspendLayout()
        Me.huilu_group_list.SuspendLayout()
        Me.SuspendLayout()
        '
        'cb_box_add
        '
        Me.cb_box_add.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_box_add.DropDownWidth = 120
        Me.cb_box_add.FormattingEnabled = True
        Me.cb_box_add.Location = New System.Drawing.Point(504, 107)
        Me.cb_box_add.Name = "cb_box_add"
        Me.cb_box_add.Size = New System.Drawing.Size(64, 20)
        Me.cb_box_add.TabIndex = 101
        '
        'box_add_string
        '
        Me.box_add_string.AutoSize = True
        Me.box_add_string.BackColor = System.Drawing.Color.Transparent
        Me.box_add_string.Location = New System.Drawing.Point(433, 110)
        Me.box_add_string.Name = "box_add_string"
        Me.box_add_string.Size = New System.Drawing.Size(65, 12)
        Me.box_add_string.TabIndex = 100
        Me.box_add_string.Text = "网关编号："
        '
        'lamp_id_string
        '
        Me.lamp_id_string.AutoSize = True
        Me.lamp_id_string.BackColor = System.Drawing.Color.Transparent
        Me.lamp_id_string.Location = New System.Drawing.Point(139, 158)
        Me.lamp_id_string.Name = "lamp_id_string"
        Me.lamp_id_string.Size = New System.Drawing.Size(65, 12)
        Me.lamp_id_string.TabIndex = 102
        Me.lamp_id_string.Text = "终端编号："
        '
        'tb_lamp_id
        '
        Me.tb_lamp_id.Location = New System.Drawing.Point(204, 155)
        Me.tb_lamp_id.Name = "tb_lamp_id"
        Me.tb_lamp_id.Size = New System.Drawing.Size(64, 21)
        Me.tb_lamp_id.TabIndex = 104
        '
        'bt_add_new
        '
        Me.bt_add_new.Location = New System.Drawing.Point(204, 241)
        Me.bt_add_new.Name = "bt_add_new"
        Me.bt_add_new.Size = New System.Drawing.Size(75, 23)
        Me.bt_add_new.TabIndex = 105
        Me.bt_add_new.Text = "新增"
        Me.bt_add_new.UseVisualStyleBackColor = True
        '
        'dgv_old_lamp_list
        '
        Me.dgv_old_lamp_list.AllowUserToAddRows = False
        Me.dgv_old_lamp_list.AutoGenerateColumns = False
        Me.dgv_old_lamp_list.BackgroundColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_old_lamp_list.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv_old_lamp_list.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_old_lamp_list.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.check, Me.control_box_name, Me.type_string, Me.lamp_id_part, Me.lamp_id_value, Me.lamp_pointinfor})
        Me.dgv_old_lamp_list.DataSource = Me.LampstreetBindingSource
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv_old_lamp_list.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgv_old_lamp_list.Location = New System.Drawing.Point(592, 10)
        Me.dgv_old_lamp_list.Name = "dgv_old_lamp_list"
        Me.dgv_old_lamp_list.RowHeadersVisible = False
        Me.dgv_old_lamp_list.RowTemplate.Height = 23
        Me.dgv_old_lamp_list.Size = New System.Drawing.Size(349, 584)
        Me.dgv_old_lamp_list.TabIndex = 114
        '
        'LampstreetBindingSource
        '
        Me.LampstreetBindingSource.DataMember = "lamp_street"
        Me.LampstreetBindingSource.DataSource = Me.Street_add
        '
        'Street_add
        '
        Me.Street_add.DataSetName = "street_add"
        Me.Street_add.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'tb_lamp_num
        '
        Me.tb_lamp_num.Location = New System.Drawing.Point(344, 155)
        Me.tb_lamp_num.Name = "tb_lamp_num"
        Me.tb_lamp_num.Size = New System.Drawing.Size(64, 21)
        Me.tb_lamp_num.TabIndex = 122
        '
        'lamp_num_string
        '
        Me.lamp_num_string.AutoSize = True
        Me.lamp_num_string.BackColor = System.Drawing.Color.Transparent
        Me.lamp_num_string.Location = New System.Drawing.Point(273, 158)
        Me.lamp_num_string.Name = "lamp_num_string"
        Me.lamp_num_string.Size = New System.Drawing.Size(65, 12)
        Me.lamp_num_string.TabIndex = 121
        Me.lamp_num_string.Text = "终端个数："
        '
        'lb_lamp_id_start
        '
        Me.lb_lamp_id_start.AutoSize = True
        Me.lb_lamp_id_start.BackColor = System.Drawing.Color.Transparent
        Me.lb_lamp_id_start.Location = New System.Drawing.Point(319, 246)
        Me.lb_lamp_id_start.Name = "lb_lamp_id_start"
        Me.lb_lamp_id_start.Size = New System.Drawing.Size(113, 12)
        Me.lb_lamp_id_start.TabIndex = 125
        Me.lb_lamp_id_start.Text = "终端编号的前半部分"
        Me.lb_lamp_id_start.Visible = False
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lamp_inf_text})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 635)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(954, 22)
        Me.StatusStrip1.TabIndex = 126
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'lamp_inf_text
        '
        Me.lamp_inf_text.Name = "lamp_inf_text"
        Me.lamp_inf_text.Size = New System.Drawing.Size(131, 17)
        Me.lamp_inf_text.Text = "ToolStripStatusLabel1"
        '
        'lamp_type_new
        '
        Me.lamp_type_new.BackColor = System.Drawing.Color.Transparent
        Me.lamp_type_new.Controls.Add(Me.LampPointInfor)
        Me.lamp_type_new.Controls.Add(Me.Label16)
        Me.lamp_type_new.Controls.Add(Me.GroupBox7)
        Me.lamp_type_new.Controls.Add(Me.Label15)
        Me.lamp_type_new.Controls.Add(Me.cb_lampvision)
        Me.lamp_type_new.Controls.Add(Me.gb_draw_method)
        Me.lamp_type_new.Controls.Add(Me.gb_drawpos)
        Me.lamp_type_new.Controls.Add(Me.lb_jiechuqi_full_id)
        Me.lamp_type_new.Controls.Add(Me.Label6)
        Me.lamp_type_new.Controls.Add(Me.cb_street_all)
        Me.lamp_type_new.Controls.Add(Me.cb_area_add)
        Me.lamp_type_new.Controls.Add(Me.Label5)
        Me.lamp_type_new.Controls.Add(Me.cb_city_add)
        Me.lamp_type_new.Controls.Add(Me.Label10)
        Me.lamp_type_new.Controls.Add(Me.Label1)
        Me.lamp_type_new.Controls.Add(Me.cb_lamp_type)
        Me.lamp_type_new.Controls.Add(Me.lb_lamp_type_id_add)
        Me.lamp_type_new.Controls.Add(Me.tb_end_pos_y)
        Me.lamp_type_new.Controls.Add(Me.tb_end_pos_x)
        Me.lamp_type_new.Controls.Add(Me.tb_start_pos_y)
        Me.lamp_type_new.Controls.Add(Me.end_pos_string)
        Me.lamp_type_new.Controls.Add(Me.tb_start_pos_x)
        Me.lamp_type_new.Controls.Add(Me.start_pos_string)
        Me.lamp_type_new.Controls.Add(Me.box_add_string)
        Me.lamp_type_new.Controls.Add(Me.lb_lamp_id_start)
        Me.lamp_type_new.Controls.Add(Me.cb_box_add)
        Me.lamp_type_new.Controls.Add(Me.tb_lamp_num)
        Me.lamp_type_new.Controls.Add(Me.bt_add_new)
        Me.lamp_type_new.Controls.Add(Me.lamp_num_string)
        Me.lamp_type_new.Controls.Add(Me.lamp_id_string)
        Me.lamp_type_new.Controls.Add(Me.tb_lamp_id)
        Me.lamp_type_new.Location = New System.Drawing.Point(12, 5)
        Me.lamp_type_new.Name = "lamp_type_new"
        Me.lamp_type_new.Size = New System.Drawing.Size(574, 270)
        Me.lamp_type_new.TabIndex = 127
        Me.lamp_type_new.TabStop = False
        Me.lamp_type_new.Text = "增加终端编号"
        '
        'LampPointInfor
        '
        Me.LampPointInfor.Location = New System.Drawing.Point(434, 195)
        Me.LampPointInfor.Name = "LampPointInfor"
        Me.LampPointInfor.Size = New System.Drawing.Size(134, 21)
        Me.LampPointInfor.TabIndex = 176
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Location = New System.Drawing.Point(363, 198)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(65, 12)
        Me.Label16.TabIndex = 175
        Me.Label16.Text = "联系电话："
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.rb_add_lampid)
        Me.GroupBox7.Controls.Add(Me.rb_add_lamptype)
        Me.GroupBox7.Location = New System.Drawing.Point(7, 20)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(127, 77)
        Me.GroupBox7.TabIndex = 174
        Me.GroupBox7.TabStop = False
        '
        'rb_add_lampid
        '
        Me.rb_add_lampid.AutoSize = True
        Me.rb_add_lampid.Checked = True
        Me.rb_add_lampid.Location = New System.Drawing.Point(14, 53)
        Me.rb_add_lampid.Name = "rb_add_lampid"
        Me.rb_add_lampid.Size = New System.Drawing.Size(71, 16)
        Me.rb_add_lampid.TabIndex = 128
        Me.rb_add_lampid.TabStop = True
        Me.rb_add_lampid.Text = "终端编号"
        Me.rb_add_lampid.UseVisualStyleBackColor = True
        '
        'rb_add_lamptype
        '
        Me.rb_add_lamptype.AutoSize = True
        Me.rb_add_lamptype.Location = New System.Drawing.Point(13, 16)
        Me.rb_add_lamptype.Name = "rb_add_lamptype"
        Me.rb_add_lamptype.Size = New System.Drawing.Size(71, 16)
        Me.rb_add_lamptype.TabIndex = 127
        Me.rb_add_lamptype.Text = "终端类型"
        Me.rb_add_lamptype.UseVisualStyleBackColor = True
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Location = New System.Drawing.Point(421, 158)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(77, 12)
        Me.Label15.TabIndex = 173
        Me.Label15.Text = "节点版本号："
        '
        'cb_lampvision
        '
        Me.cb_lampvision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_lampvision.DropDownWidth = 120
        Me.cb_lampvision.FormattingEnabled = True
        Me.cb_lampvision.Items.AddRange(New Object() {"1", "2", "3"})
        Me.cb_lampvision.Location = New System.Drawing.Point(504, 156)
        Me.cb_lampvision.Name = "cb_lampvision"
        Me.cb_lampvision.Size = New System.Drawing.Size(64, 20)
        Me.cb_lampvision.TabIndex = 172
        Me.ToolTip_lamp.SetToolTip(Me.cb_lampvision, "版本1为1字节" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "版本2为2字节" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "版本3为6字节")
        '
        'gb_draw_method
        '
        Me.gb_draw_method.Controls.Add(Me.rb_addnext_num)
        Me.gb_draw_method.Controls.Add(Me.rb_addnexttwo_num)
        Me.gb_draw_method.Controls.Add(Me.rb_addnextthree_num)
        Me.gb_draw_method.Location = New System.Drawing.Point(303, 20)
        Me.gb_draw_method.Name = "gb_draw_method"
        Me.gb_draw_method.Size = New System.Drawing.Size(238, 75)
        Me.gb_draw_method.TabIndex = 171
        Me.gb_draw_method.TabStop = False
        '
        'rb_addnext_num
        '
        Me.rb_addnext_num.AutoSize = True
        Me.rb_addnext_num.Checked = True
        Me.rb_addnext_num.Location = New System.Drawing.Point(5, 14)
        Me.rb_addnext_num.Name = "rb_addnext_num"
        Me.rb_addnext_num.Size = New System.Drawing.Size(71, 16)
        Me.rb_addnext_num.TabIndex = 157
        Me.rb_addnext_num.TabStop = True
        Me.rb_addnext_num.Text = "顺序编号"
        Me.rb_addnext_num.UseVisualStyleBackColor = True
        '
        'rb_addnexttwo_num
        '
        Me.rb_addnexttwo_num.AutoSize = True
        Me.rb_addnexttwo_num.Location = New System.Drawing.Point(105, 14)
        Me.rb_addnexttwo_num.Name = "rb_addnexttwo_num"
        Me.rb_addnexttwo_num.Size = New System.Drawing.Size(71, 16)
        Me.rb_addnexttwo_num.TabIndex = 158
        Me.rb_addnexttwo_num.Text = "隔盏编号"
        Me.rb_addnexttwo_num.UseVisualStyleBackColor = True
        '
        'rb_addnextthree_num
        '
        Me.rb_addnextthree_num.AutoSize = True
        Me.rb_addnextthree_num.Location = New System.Drawing.Point(5, 53)
        Me.rb_addnextthree_num.Name = "rb_addnextthree_num"
        Me.rb_addnextthree_num.Size = New System.Drawing.Size(95, 16)
        Me.rb_addnextthree_num.TabIndex = 159
        Me.rb_addnextthree_num.Text = "三相组合编号"
        Me.rb_addnextthree_num.UseVisualStyleBackColor = True
        '
        'gb_drawpos
        '
        Me.gb_drawpos.Controls.Add(Me.rb_xadd)
        Me.gb_drawpos.Controls.Add(Me.rb_yadd)
        Me.gb_drawpos.Location = New System.Drawing.Point(141, 20)
        Me.gb_drawpos.Name = "gb_drawpos"
        Me.gb_drawpos.Size = New System.Drawing.Size(139, 75)
        Me.gb_drawpos.TabIndex = 170
        Me.gb_drawpos.TabStop = False
        '
        'rb_xadd
        '
        Me.rb_xadd.AutoSize = True
        Me.rb_xadd.Location = New System.Drawing.Point(5, 53)
        Me.rb_xadd.Name = "rb_xadd"
        Me.rb_xadd.Size = New System.Drawing.Size(71, 16)
        Me.rb_xadd.TabIndex = 1
        Me.rb_xadd.Text = "横向偏移"
        Me.rb_xadd.UseVisualStyleBackColor = True
        '
        'rb_yadd
        '
        Me.rb_yadd.AutoSize = True
        Me.rb_yadd.Checked = True
        Me.rb_yadd.Location = New System.Drawing.Point(5, 16)
        Me.rb_yadd.Name = "rb_yadd"
        Me.rb_yadd.Size = New System.Drawing.Size(71, 16)
        Me.rb_yadd.TabIndex = 0
        Me.rb_yadd.TabStop = True
        Me.rb_yadd.Text = "纵向偏移"
        Me.rb_yadd.UseVisualStyleBackColor = True
        '
        'lb_jiechuqi_full_id
        '
        Me.lb_jiechuqi_full_id.AutoSize = True
        Me.lb_jiechuqi_full_id.Location = New System.Drawing.Point(9, 246)
        Me.lb_jiechuqi_full_id.Name = "lb_jiechuqi_full_id"
        Me.lb_jiechuqi_full_id.Size = New System.Drawing.Size(65, 12)
        Me.lb_jiechuqi_full_id.TabIndex = 169
        Me.lb_jiechuqi_full_id.Text = "主控箱节点"
        Me.lb_jiechuqi_full_id.Visible = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label6.Location = New System.Drawing.Point(274, 110)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(65, 12)
        Me.Label6.TabIndex = 165
        Me.Label6.Text = "乡镇名称："
        '
        'cb_street_all
        '
        Me.cb_street_all.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_street_all.DropDownWidth = 120
        Me.cb_street_all.FormattingEnabled = True
        Me.cb_street_all.Location = New System.Drawing.Point(344, 107)
        Me.cb_street_all.Name = "cb_street_all"
        Me.cb_street_all.Size = New System.Drawing.Size(64, 20)
        Me.cb_street_all.TabIndex = 166
        '
        'cb_area_add
        '
        Me.cb_area_add.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_area_add.DropDownWidth = 120
        Me.cb_area_add.FormattingEnabled = True
        Me.cb_area_add.Location = New System.Drawing.Point(204, 108)
        Me.cb_area_add.Name = "cb_area_add"
        Me.cb_area_add.Size = New System.Drawing.Size(64, 20)
        Me.cb_area_add.TabIndex = 164
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label5.Location = New System.Drawing.Point(139, 110)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(65, 12)
        Me.Label5.TabIndex = 163
        Me.Label5.Text = "区域名称："
        '
        'cb_city_add
        '
        Me.cb_city_add.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_city_add.DropDownWidth = 120
        Me.cb_city_add.FormattingEnabled = True
        Me.cb_city_add.Location = New System.Drawing.Point(70, 108)
        Me.cb_city_add.Name = "cb_city_add"
        Me.cb_city_add.Size = New System.Drawing.Size(64, 20)
        Me.cb_city_add.TabIndex = 162
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label10.Location = New System.Drawing.Point(8, 110)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(65, 12)
        Me.Label10.TabIndex = 161
        Me.Label10.Text = "城市名称："
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(3, 158)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 12)
        Me.Label1.TabIndex = 160
        Me.Label1.Text = "类型名称："
        '
        'cb_lamp_type
        '
        Me.cb_lamp_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_lamp_type.DropDownWidth = 120
        Me.cb_lamp_type.FormattingEnabled = True
        Me.cb_lamp_type.Location = New System.Drawing.Point(70, 155)
        Me.cb_lamp_type.Name = "cb_lamp_type"
        Me.cb_lamp_type.Size = New System.Drawing.Size(64, 20)
        Me.cb_lamp_type.TabIndex = 156
        '
        'lb_lamp_type_id_add
        '
        Me.lb_lamp_type_id_add.AutoSize = True
        Me.lb_lamp_type_id_add.Location = New System.Drawing.Point(456, 246)
        Me.lb_lamp_type_id_add.Name = "lb_lamp_type_id_add"
        Me.lb_lamp_type_id_add.Size = New System.Drawing.Size(53, 12)
        Me.lb_lamp_type_id_add.TabIndex = 136
        Me.lb_lamp_type_id_add.Text = "灯的类型"
        Me.lb_lamp_type_id_add.Visible = False
        '
        'tb_end_pos_y
        '
        Me.tb_end_pos_y.Location = New System.Drawing.Point(325, 195)
        Me.tb_end_pos_y.Name = "tb_end_pos_y"
        Me.tb_end_pos_y.ReadOnly = True
        Me.tb_end_pos_y.Size = New System.Drawing.Size(33, 21)
        Me.tb_end_pos_y.TabIndex = 135
        '
        'tb_end_pos_x
        '
        Me.tb_end_pos_x.Location = New System.Drawing.Point(276, 195)
        Me.tb_end_pos_x.Name = "tb_end_pos_x"
        Me.tb_end_pos_x.ReadOnly = True
        Me.tb_end_pos_x.Size = New System.Drawing.Size(33, 21)
        Me.tb_end_pos_x.TabIndex = 134
        '
        'tb_start_pos_y
        '
        Me.tb_start_pos_y.Location = New System.Drawing.Point(138, 195)
        Me.tb_start_pos_y.Name = "tb_start_pos_y"
        Me.tb_start_pos_y.ReadOnly = True
        Me.tb_start_pos_y.Size = New System.Drawing.Size(33, 21)
        Me.tb_start_pos_y.TabIndex = 133
        '
        'end_pos_string
        '
        Me.end_pos_string.AutoSize = True
        Me.end_pos_string.BackColor = System.Drawing.Color.Transparent
        Me.end_pos_string.Location = New System.Drawing.Point(177, 201)
        Me.end_pos_string.Name = "end_pos_string"
        Me.end_pos_string.Size = New System.Drawing.Size(95, 12)
        Me.end_pos_string.TabIndex = 132
        Me.end_pos_string.Text = "终点坐标(X,Y)："
        '
        'tb_start_pos_x
        '
        Me.tb_start_pos_x.Location = New System.Drawing.Point(99, 195)
        Me.tb_start_pos_x.Name = "tb_start_pos_x"
        Me.tb_start_pos_x.ReadOnly = True
        Me.tb_start_pos_x.Size = New System.Drawing.Size(33, 21)
        Me.tb_start_pos_x.TabIndex = 131
        '
        'start_pos_string
        '
        Me.start_pos_string.AutoSize = True
        Me.start_pos_string.BackColor = System.Drawing.Color.Transparent
        Me.start_pos_string.Location = New System.Drawing.Point(2, 198)
        Me.start_pos_string.Name = "start_pos_string"
        Me.start_pos_string.Size = New System.Drawing.Size(95, 12)
        Me.start_pos_string.TabIndex = 130
        Me.start_pos_string.Text = "起点坐标(X,Y)："
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.Controls.Add(Me.GroupBox6)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.cb_delete_city)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.cb_delete_area)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.cb_delete_street)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.lb_lamp_type_id_del)
        Me.GroupBox2.Controls.Add(Me.bt_delete_confirm)
        Me.GroupBox2.Controls.Add(Me.rtb_delete_detail)
        Me.GroupBox2.Controls.Add(Me.bt_delete_lamp)
        Me.GroupBox2.Controls.Add(Me.cb_delete_lamp_id_start)
        Me.GroupBox2.Controls.Add(Me.GroupBox3)
        Me.GroupBox2.Controls.Add(Me.lb_delete_lamp_id_end_start)
        Me.GroupBox2.Controls.Add(Me.delete_control_box_name_string)
        Me.GroupBox2.Controls.Add(Me.lb_delete_lamp_id_start_start)
        Me.GroupBox2.Controls.Add(Me.cb_delete_lamp_id_end)
        Me.GroupBox2.Controls.Add(Me.cb_delete_control_box_name)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.cb_delete_lamp_type)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.bt_choose_lamp)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 281)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(574, 313)
        Me.GroupBox2.TabIndex = 128
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "查询/删除终端"
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.rb_check_3)
        Me.GroupBox6.Controls.Add(Me.rb_check_2)
        Me.GroupBox6.Controls.Add(Me.rb_check_1)
        Me.GroupBox6.Controls.Add(Me.rb_check_all)
        Me.GroupBox6.Location = New System.Drawing.Point(11, 158)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(121, 113)
        Me.GroupBox6.TabIndex = 176
        Me.GroupBox6.TabStop = False
        '
        'rb_check_3
        '
        Me.rb_check_3.AutoSize = True
        Me.rb_check_3.Location = New System.Drawing.Point(21, 83)
        Me.rb_check_3.Name = "rb_check_3"
        Me.rb_check_3.Size = New System.Drawing.Size(41, 16)
        Me.rb_check_3.TabIndex = 3
        Me.rb_check_3.Text = "C相"
        Me.rb_check_3.UseVisualStyleBackColor = True
        '
        'rb_check_2
        '
        Me.rb_check_2.AutoSize = True
        Me.rb_check_2.Location = New System.Drawing.Point(21, 59)
        Me.rb_check_2.Name = "rb_check_2"
        Me.rb_check_2.Size = New System.Drawing.Size(41, 16)
        Me.rb_check_2.TabIndex = 2
        Me.rb_check_2.Text = "B相"
        Me.rb_check_2.UseVisualStyleBackColor = True
        '
        'rb_check_1
        '
        Me.rb_check_1.AutoSize = True
        Me.rb_check_1.Location = New System.Drawing.Point(21, 36)
        Me.rb_check_1.Name = "rb_check_1"
        Me.rb_check_1.Size = New System.Drawing.Size(41, 16)
        Me.rb_check_1.TabIndex = 1
        Me.rb_check_1.Text = "A相"
        Me.rb_check_1.UseVisualStyleBackColor = True
        '
        'rb_check_all
        '
        Me.rb_check_all.AutoSize = True
        Me.rb_check_all.Checked = True
        Me.rb_check_all.Location = New System.Drawing.Point(21, 11)
        Me.rb_check_all.Name = "rb_check_all"
        Me.rb_check_all.Size = New System.Drawing.Size(47, 16)
        Me.rb_check_all.TabIndex = 0
        Me.rb_check_all.TabStop = True
        Me.rb_check_all.Text = "全相"
        Me.rb_check_all.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Location = New System.Drawing.Point(159, 16)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(65, 12)
        Me.Label9.TabIndex = 173
        Me.Label9.Text = "城市名称："
        '
        'cb_delete_city
        '
        Me.cb_delete_city.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_delete_city.FormattingEnabled = True
        Me.cb_delete_city.Location = New System.Drawing.Point(230, 14)
        Me.cb_delete_city.Name = "cb_delete_city"
        Me.cb_delete_city.Size = New System.Drawing.Size(86, 20)
        Me.cb_delete_city.TabIndex = 174
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Location = New System.Drawing.Point(342, 16)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(65, 12)
        Me.Label8.TabIndex = 171
        Me.Label8.Text = "区域名称："
        '
        'cb_delete_area
        '
        Me.cb_delete_area.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_delete_area.FormattingEnabled = True
        Me.cb_delete_area.Location = New System.Drawing.Point(413, 14)
        Me.cb_delete_area.Name = "cb_delete_area"
        Me.cb_delete_area.Size = New System.Drawing.Size(91, 20)
        Me.cb_delete_area.TabIndex = 172
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Location = New System.Drawing.Point(159, 50)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(65, 12)
        Me.Label7.TabIndex = 169
        Me.Label7.Text = "乡镇名称："
        '
        'cb_delete_street
        '
        Me.cb_delete_street.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_delete_street.FormattingEnabled = True
        Me.cb_delete_street.Location = New System.Drawing.Point(230, 47)
        Me.cb_delete_street.Name = "cb_delete_street"
        Me.cb_delete_street.Size = New System.Drawing.Size(87, 20)
        Me.cb_delete_street.TabIndex = 170
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(159, 82)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(65, 12)
        Me.Label4.TabIndex = 168
        Me.Label4.Text = "类型名称："
        '
        'lb_lamp_type_id_del
        '
        Me.lb_lamp_type_id_del.AutoSize = True
        Me.lb_lamp_type_id_del.Location = New System.Drawing.Point(395, 132)
        Me.lb_lamp_type_id_del.Name = "lb_lamp_type_id_del"
        Me.lb_lamp_type_id_del.Size = New System.Drawing.Size(53, 12)
        Me.lb_lamp_type_id_del.TabIndex = 167
        Me.lb_lamp_type_id_del.Text = "灯的类型"
        Me.lb_lamp_type_id_del.Visible = False
        '
        'bt_delete_confirm
        '
        Me.bt_delete_confirm.Location = New System.Drawing.Point(279, 277)
        Me.bt_delete_confirm.Name = "bt_delete_confirm"
        Me.bt_delete_confirm.Size = New System.Drawing.Size(75, 23)
        Me.bt_delete_confirm.TabIndex = 166
        Me.bt_delete_confirm.Text = "确定"
        Me.bt_delete_confirm.UseVisualStyleBackColor = True
        '
        'rtb_delete_detail
        '
        Me.rtb_delete_detail.Location = New System.Drawing.Point(161, 190)
        Me.rtb_delete_detail.Name = "rtb_delete_detail"
        Me.rtb_delete_detail.Size = New System.Drawing.Size(345, 81)
        Me.rtb_delete_detail.TabIndex = 165
        Me.rtb_delete_detail.Text = ""
        '
        'bt_delete_lamp
        '
        Me.bt_delete_lamp.Location = New System.Drawing.Point(327, 153)
        Me.bt_delete_lamp.Name = "bt_delete_lamp"
        Me.bt_delete_lamp.Size = New System.Drawing.Size(75, 23)
        Me.bt_delete_lamp.TabIndex = 164
        Me.bt_delete_lamp.Text = "删除"
        Me.bt_delete_lamp.UseVisualStyleBackColor = True
        '
        'cb_delete_lamp_id_start
        '
        Me.cb_delete_lamp_id_start.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_delete_lamp_id_start.FormattingEnabled = True
        Me.cb_delete_lamp_id_start.Location = New System.Drawing.Point(413, 79)
        Me.cb_delete_lamp_id_start.Name = "cb_delete_lamp_id_start"
        Me.cb_delete_lamp_id_start.Size = New System.Drawing.Size(91, 20)
        Me.cb_delete_lamp_id_start.TabIndex = 163
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox3.Controls.Add(Me.rb_area_control)
        Me.GroupBox3.Controls.Add(Me.rb_street_control)
        Me.GroupBox3.Controls.Add(Me.rb_lamp_type_control)
        Me.GroupBox3.Controls.Add(Me.rb_city_control)
        Me.GroupBox3.Controls.Add(Me.rb_lamp_id_control)
        Me.GroupBox3.Controls.Add(Me.rb_box_name_control)
        Me.GroupBox3.Location = New System.Drawing.Point(13, 19)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(121, 134)
        Me.GroupBox3.TabIndex = 157
        Me.GroupBox3.TabStop = False
        '
        'rb_area_control
        '
        Me.rb_area_control.AutoSize = True
        Me.rb_area_control.BackColor = System.Drawing.Color.Transparent
        Me.rb_area_control.Location = New System.Drawing.Point(21, 31)
        Me.rb_area_control.Name = "rb_area_control"
        Me.rb_area_control.Size = New System.Drawing.Size(71, 16)
        Me.rb_area_control.TabIndex = 148
        Me.rb_area_control.Text = "区域名称"
        Me.rb_area_control.UseVisualStyleBackColor = False
        '
        'rb_street_control
        '
        Me.rb_street_control.AutoSize = True
        Me.rb_street_control.BackColor = System.Drawing.Color.Transparent
        Me.rb_street_control.Location = New System.Drawing.Point(21, 51)
        Me.rb_street_control.Name = "rb_street_control"
        Me.rb_street_control.Size = New System.Drawing.Size(71, 16)
        Me.rb_street_control.TabIndex = 147
        Me.rb_street_control.Text = "乡镇名称"
        Me.rb_street_control.UseVisualStyleBackColor = False
        '
        'rb_lamp_type_control
        '
        Me.rb_lamp_type_control.AutoSize = True
        Me.rb_lamp_type_control.BackColor = System.Drawing.Color.Transparent
        Me.rb_lamp_type_control.Location = New System.Drawing.Point(21, 93)
        Me.rb_lamp_type_control.Name = "rb_lamp_type_control"
        Me.rb_lamp_type_control.Size = New System.Drawing.Size(71, 16)
        Me.rb_lamp_type_control.TabIndex = 146
        Me.rb_lamp_type_control.Text = "类型名称"
        Me.rb_lamp_type_control.UseVisualStyleBackColor = False
        '
        'rb_city_control
        '
        Me.rb_city_control.AutoSize = True
        Me.rb_city_control.BackColor = System.Drawing.Color.Transparent
        Me.rb_city_control.Location = New System.Drawing.Point(21, 11)
        Me.rb_city_control.Name = "rb_city_control"
        Me.rb_city_control.Size = New System.Drawing.Size(71, 16)
        Me.rb_city_control.TabIndex = 145
        Me.rb_city_control.Text = "城市名称"
        Me.rb_city_control.UseVisualStyleBackColor = False
        '
        'rb_lamp_id_control
        '
        Me.rb_lamp_id_control.AutoSize = True
        Me.rb_lamp_id_control.BackColor = System.Drawing.Color.Transparent
        Me.rb_lamp_id_control.Checked = True
        Me.rb_lamp_id_control.Location = New System.Drawing.Point(21, 113)
        Me.rb_lamp_id_control.Name = "rb_lamp_id_control"
        Me.rb_lamp_id_control.Size = New System.Drawing.Size(71, 16)
        Me.rb_lamp_id_control.TabIndex = 144
        Me.rb_lamp_id_control.TabStop = True
        Me.rb_lamp_id_control.Text = "终端编号"
        Me.rb_lamp_id_control.UseVisualStyleBackColor = False
        '
        'rb_box_name_control
        '
        Me.rb_box_name_control.AutoSize = True
        Me.rb_box_name_control.BackColor = System.Drawing.Color.Transparent
        Me.rb_box_name_control.Location = New System.Drawing.Point(21, 72)
        Me.rb_box_name_control.Name = "rb_box_name_control"
        Me.rb_box_name_control.Size = New System.Drawing.Size(71, 16)
        Me.rb_box_name_control.TabIndex = 142
        Me.rb_box_name_control.Text = "网关编号"
        Me.rb_box_name_control.UseVisualStyleBackColor = False
        '
        'lb_delete_lamp_id_end_start
        '
        Me.lb_delete_lamp_id_end_start.AutoSize = True
        Me.lb_delete_lamp_id_end_start.BackColor = System.Drawing.Color.Transparent
        Me.lb_delete_lamp_id_end_start.Location = New System.Drawing.Point(273, 138)
        Me.lb_delete_lamp_id_end_start.Name = "lb_delete_lamp_id_end_start"
        Me.lb_delete_lamp_id_end_start.Size = New System.Drawing.Size(101, 12)
        Me.lb_delete_lamp_id_end_start.TabIndex = 162
        Me.lb_delete_lamp_id_end_start.Text = "结束编号前半部分"
        Me.lb_delete_lamp_id_end_start.Visible = False
        '
        'delete_control_box_name_string
        '
        Me.delete_control_box_name_string.AutoSize = True
        Me.delete_control_box_name_string.BackColor = System.Drawing.Color.Transparent
        Me.delete_control_box_name_string.Location = New System.Drawing.Point(342, 47)
        Me.delete_control_box_name_string.Name = "delete_control_box_name_string"
        Me.delete_control_box_name_string.Size = New System.Drawing.Size(65, 12)
        Me.delete_control_box_name_string.TabIndex = 152
        Me.delete_control_box_name_string.Text = "网关编号："
        '
        'lb_delete_lamp_id_start_start
        '
        Me.lb_delete_lamp_id_start_start.AutoSize = True
        Me.lb_delete_lamp_id_start_start.BackColor = System.Drawing.Color.Transparent
        Me.lb_delete_lamp_id_start_start.Location = New System.Drawing.Point(159, 138)
        Me.lb_delete_lamp_id_start_start.Name = "lb_delete_lamp_id_start_start"
        Me.lb_delete_lamp_id_start_start.Size = New System.Drawing.Size(101, 12)
        Me.lb_delete_lamp_id_start_start.TabIndex = 161
        Me.lb_delete_lamp_id_start_start.Text = "起始编号前半部分"
        Me.lb_delete_lamp_id_start_start.Visible = False
        '
        'cb_delete_lamp_id_end
        '
        Me.cb_delete_lamp_id_end.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_delete_lamp_id_end.FormattingEnabled = True
        Me.cb_delete_lamp_id_end.Location = New System.Drawing.Point(230, 114)
        Me.cb_delete_lamp_id_end.Name = "cb_delete_lamp_id_end"
        Me.cb_delete_lamp_id_end.Size = New System.Drawing.Size(86, 20)
        Me.cb_delete_lamp_id_end.TabIndex = 160
        '
        'cb_delete_control_box_name
        '
        Me.cb_delete_control_box_name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_delete_control_box_name.FormattingEnabled = True
        Me.cb_delete_control_box_name.Location = New System.Drawing.Point(413, 46)
        Me.cb_delete_control_box_name.Name = "cb_delete_control_box_name"
        Me.cb_delete_control_box_name.Size = New System.Drawing.Size(91, 20)
        Me.cb_delete_control_box_name.TabIndex = 154
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(159, 117)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 12)
        Me.Label2.TabIndex = 159
        Me.Label2.Text = "结束编号："
        '
        'cb_delete_lamp_type
        '
        Me.cb_delete_lamp_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_delete_lamp_type.FormattingEnabled = True
        Me.cb_delete_lamp_type.Location = New System.Drawing.Point(230, 81)
        Me.cb_delete_lamp_type.Name = "cb_delete_lamp_type"
        Me.cb_delete_lamp_type.Size = New System.Drawing.Size(87, 20)
        Me.cb_delete_lamp_type.TabIndex = 155
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(342, 83)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 12)
        Me.Label3.TabIndex = 158
        Me.Label3.Text = "起始编号："
        '
        'bt_choose_lamp
        '
        Me.bt_choose_lamp.Location = New System.Drawing.Point(198, 153)
        Me.bt_choose_lamp.Name = "bt_choose_lamp"
        Me.bt_choose_lamp.Size = New System.Drawing.Size(75, 23)
        Me.bt_choose_lamp.TabIndex = 156
        Me.bt_choose_lamp.Text = "查询"
        Me.bt_choose_lamp.UseVisualStyleBackColor = True
        '
        'Lamp_streetTableAdapter
        '
        Me.Lamp_streetTableAdapter.ClearBeforeFill = True
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(4, 7)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(949, 625)
        Me.TabControl1.TabIndex = 129
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.lamp_type_new)
        Me.TabPage1.Controls.Add(Me.GroupBox2)
        Me.TabPage1.Controls.Add(Me.dgv_old_lamp_list)
        Me.TabPage1.Location = New System.Drawing.Point(4, 21)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(941, 600)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "终端节点/交流接触器"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.bt_sendboardnum)
        Me.TabPage2.Controls.Add(Me.cb_controlbox_name)
        Me.TabPage2.Controls.Add(Me.rb_checkall)
        Me.TabPage2.Controls.Add(Me.GroupBox5)
        Me.TabPage2.Controls.Add(Me.bt_send_alarmdata)
        Me.TabPage2.Controls.Add(Me.rb_box_check)
        Me.TabPage2.Controls.Add(Me.bt_send_kaiguan)
        Me.TabPage2.Controls.Add(Me.bt_send_huiluinf)
        Me.TabPage2.Location = New System.Drawing.Point(4, 21)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(941, 600)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "模拟量/开关量设置"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'bt_sendboardnum
        '
        Me.bt_sendboardnum.Location = New System.Drawing.Point(624, 12)
        Me.bt_sendboardnum.Name = "bt_sendboardnum"
        Me.bt_sendboardnum.Size = New System.Drawing.Size(106, 25)
        Me.bt_sendboardnum.TabIndex = 166
        Me.bt_sendboardnum.Text = "发送测量板个数"
        Me.bt_sendboardnum.UseVisualStyleBackColor = True
        '
        'cb_controlbox_name
        '
        Me.cb_controlbox_name.Enabled = False
        Me.cb_controlbox_name.FormattingEnabled = True
        Me.cb_controlbox_name.Location = New System.Drawing.Point(217, 16)
        Me.cb_controlbox_name.Name = "cb_controlbox_name"
        Me.cb_controlbox_name.Size = New System.Drawing.Size(104, 20)
        Me.cb_controlbox_name.TabIndex = 165
        '
        'rb_checkall
        '
        Me.rb_checkall.AutoSize = True
        Me.rb_checkall.Checked = True
        Me.rb_checkall.Location = New System.Drawing.Point(11, 17)
        Me.rb_checkall.Name = "rb_checkall"
        Me.rb_checkall.Size = New System.Drawing.Size(71, 16)
        Me.rb_checkall.TabIndex = 164
        Me.rb_checkall.TabStop = True
        Me.rb_checkall.Text = "全部信息"
        Me.rb_checkall.UseVisualStyleBackColor = True
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.Label11)
        Me.GroupBox5.Controls.Add(Me.tb_bianbi_value)
        Me.GroupBox5.Controls.Add(Me.bt_set_huilu)
        Me.GroupBox5.Controls.Add(Me.GroupBoxkaiguanliang)
        Me.GroupBox5.Controls.Add(Me.GroupBoxmoniliang)
        Me.GroupBox5.Controls.Add(Me.Label12)
        Me.GroupBox5.Controls.Add(Me.GroupBoxhuilu)
        Me.GroupBox5.Controls.Add(Me.tv_box_inf_list)
        Me.GroupBox5.Controls.Add(Me.huilu_group_list)
        Me.GroupBox5.Location = New System.Drawing.Point(3, 45)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(880, 491)
        Me.GroupBox5.TabIndex = 158
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "增加\编辑回路编号（双击主控箱名称进行选择）"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(195, 399)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(77, 12)
        Me.Label11.TabIndex = 168
        Me.Label11.Text = "互感器变比："
        '
        'tb_bianbi_value
        '
        Me.tb_bianbi_value.Location = New System.Drawing.Point(278, 397)
        Me.tb_bianbi_value.Name = "tb_bianbi_value"
        Me.tb_bianbi_value.Size = New System.Drawing.Size(41, 21)
        Me.tb_bianbi_value.TabIndex = 167
        Me.tb_bianbi_value.Text = "10"
        '
        'bt_set_huilu
        '
        Me.bt_set_huilu.Location = New System.Drawing.Point(263, 435)
        Me.bt_set_huilu.Name = "bt_set_huilu"
        Me.bt_set_huilu.Size = New System.Drawing.Size(86, 25)
        Me.bt_set_huilu.TabIndex = 162
        Me.bt_set_huilu.Text = "新增"
        Me.bt_set_huilu.UseVisualStyleBackColor = True
        '
        'GroupBoxkaiguanliang
        '
        Me.GroupBoxkaiguanliang.Controls.Add(Me.chb_checkall)
        Me.GroupBoxkaiguanliang.Controls.Add(Me.GroupBoxAlarm)
        Me.GroupBoxkaiguanliang.Controls.Add(Me.chb_set_alarm)
        Me.GroupBoxkaiguanliang.Controls.Add(Me.bt_save_kaiguan)
        Me.GroupBoxkaiguanliang.Controls.Add(Me.bt_del_kaiguan)
        Me.GroupBoxkaiguanliang.Controls.Add(Me.dgv_kaiguanliang_list)
        Me.GroupBoxkaiguanliang.Location = New System.Drawing.Point(405, 264)
        Me.GroupBoxkaiguanliang.Name = "GroupBoxkaiguanliang"
        Me.GroupBoxkaiguanliang.Size = New System.Drawing.Size(465, 222)
        Me.GroupBoxkaiguanliang.TabIndex = 164
        Me.GroupBoxkaiguanliang.TabStop = False
        Me.GroupBoxkaiguanliang.Text = "增加/编辑开关量"
        '
        'chb_checkall
        '
        Me.chb_checkall.AutoSize = True
        Me.chb_checkall.Location = New System.Drawing.Point(9, 171)
        Me.chb_checkall.Name = "chb_checkall"
        Me.chb_checkall.Size = New System.Drawing.Size(48, 16)
        Me.chb_checkall.TabIndex = 12
        Me.chb_checkall.Text = "全选"
        Me.chb_checkall.UseVisualStyleBackColor = True
        '
        'GroupBoxAlarm
        '
        Me.GroupBoxAlarm.Controls.Add(Me.bt_set_alarmvalue)
        Me.GroupBoxAlarm.Controls.Add(Me.Label13)
        Me.GroupBoxAlarm.Controls.Add(Me.cb_shiliang_list)
        Me.GroupBoxAlarm.Controls.Add(Me.cb_tiaobian_list)
        Me.GroupBoxAlarm.Controls.Add(Me.Label14)
        Me.GroupBoxAlarm.Enabled = False
        Me.GroupBoxAlarm.Location = New System.Drawing.Point(343, 39)
        Me.GroupBoxAlarm.Name = "GroupBoxAlarm"
        Me.GroupBoxAlarm.Size = New System.Drawing.Size(93, 164)
        Me.GroupBoxAlarm.TabIndex = 11
        Me.GroupBoxAlarm.TabStop = False
        '
        'bt_set_alarmvalue
        '
        Me.bt_set_alarmvalue.Location = New System.Drawing.Point(9, 128)
        Me.bt_set_alarmvalue.Name = "bt_set_alarmvalue"
        Me.bt_set_alarmvalue.Size = New System.Drawing.Size(64, 20)
        Me.bt_set_alarmvalue.TabIndex = 10
        Me.bt_set_alarmvalue.Text = "设置"
        Me.bt_set_alarmvalue.UseVisualStyleBackColor = True
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(5, 16)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(53, 12)
        Me.Label13.TabIndex = 6
        Me.Label13.Text = "物理矢量"
        '
        'cb_shiliang_list
        '
        Me.cb_shiliang_list.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_shiliang_list.FormattingEnabled = True
        Me.cb_shiliang_list.Items.AddRange(New Object() {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16"})
        Me.cb_shiliang_list.Location = New System.Drawing.Point(8, 39)
        Me.cb_shiliang_list.Name = "cb_shiliang_list"
        Me.cb_shiliang_list.Size = New System.Drawing.Size(66, 20)
        Me.cb_shiliang_list.TabIndex = 7
        '
        'cb_tiaobian_list
        '
        Me.cb_tiaobian_list.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_tiaobian_list.FormattingEnabled = True
        Me.cb_tiaobian_list.Items.AddRange(New Object() {"0", "1"})
        Me.cb_tiaobian_list.Location = New System.Drawing.Point(8, 99)
        Me.cb_tiaobian_list.Name = "cb_tiaobian_list"
        Me.cb_tiaobian_list.Size = New System.Drawing.Size(66, 20)
        Me.cb_tiaobian_list.TabIndex = 9
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(5, 69)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(53, 12)
        Me.Label14.TabIndex = 8
        Me.Label14.Text = "跳变报警"
        '
        'chb_set_alarm
        '
        Me.chb_set_alarm.AutoSize = True
        Me.chb_set_alarm.Location = New System.Drawing.Point(343, 19)
        Me.chb_set_alarm.Name = "chb_set_alarm"
        Me.chb_set_alarm.Size = New System.Drawing.Size(96, 16)
        Me.chb_set_alarm.TabIndex = 10
        Me.chb_set_alarm.Text = "设置跳变报警"
        Me.chb_set_alarm.UseVisualStyleBackColor = True
        '
        'bt_save_kaiguan
        '
        Me.bt_save_kaiguan.Location = New System.Drawing.Point(88, 192)
        Me.bt_save_kaiguan.Name = "bt_save_kaiguan"
        Me.bt_save_kaiguan.Size = New System.Drawing.Size(86, 25)
        Me.bt_save_kaiguan.TabIndex = 5
        Me.bt_save_kaiguan.Text = "保存"
        Me.bt_save_kaiguan.UseVisualStyleBackColor = True
        '
        'bt_del_kaiguan
        '
        Me.bt_del_kaiguan.Location = New System.Drawing.Point(223, 192)
        Me.bt_del_kaiguan.Name = "bt_del_kaiguan"
        Me.bt_del_kaiguan.Size = New System.Drawing.Size(86, 25)
        Me.bt_del_kaiguan.TabIndex = 4
        Me.bt_del_kaiguan.Text = "删除"
        Me.bt_del_kaiguan.UseVisualStyleBackColor = True
        '
        'dgv_kaiguanliang_list
        '
        Me.dgv_kaiguanliang_list.AutoGenerateColumns = False
        Me.dgv_kaiguanliang_list.BackgroundColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_kaiguanliang_list.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dgv_kaiguanliang_list.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_kaiguanliang_list.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.kgcheckid, Me.kaiguan_name, Me.kaiguan_tag, Me.alarm, Me.kgcontrol_box_name, Me.kgid})
        Me.dgv_kaiguanliang_list.DataSource = Me.KaiguanlistBindingSource
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv_kaiguanliang_list.DefaultCellStyle = DataGridViewCellStyle5
        Me.dgv_kaiguanliang_list.Location = New System.Drawing.Point(9, 19)
        Me.dgv_kaiguanliang_list.Name = "dgv_kaiguanliang_list"
        Me.dgv_kaiguanliang_list.RowHeadersVisible = False
        Me.dgv_kaiguanliang_list.RowTemplate.Height = 23
        Me.dgv_kaiguanliang_list.Size = New System.Drawing.Size(315, 147)
        Me.dgv_kaiguanliang_list.TabIndex = 0
        '
        'kgcheckid
        '
        Me.kgcheckid.FalseValue = "0"
        Me.kgcheckid.HeaderText = ""
        Me.kgcheckid.Name = "kgcheckid"
        Me.kgcheckid.TrueValue = "1"
        Me.kgcheckid.Width = 25
        '
        'kaiguan_name
        '
        Me.kaiguan_name.DataPropertyName = "kaiguan_name"
        Me.kaiguan_name.HeaderText = "开关量名称"
        Me.kaiguan_name.Name = "kaiguan_name"
        Me.kaiguan_name.Width = 120
        '
        'kaiguan_tag
        '
        Me.kaiguan_tag.DataPropertyName = "kaiguan_tag"
        Me.kaiguan_tag.HeaderText = "物理矢量"
        Me.kaiguan_tag.Name = "kaiguan_tag"
        Me.kaiguan_tag.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.kaiguan_tag.ToolTipText = "共计1-16个物理矢量编号"
        '
        'alarm
        '
        Me.alarm.DataPropertyName = "alarm"
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.alarm.DefaultCellStyle = DataGridViewCellStyle4
        Me.alarm.HeaderText = "跳变报警"
        Me.alarm.Name = "alarm"
        Me.alarm.ReadOnly = True
        Me.alarm.ToolTipText = "0表示跳变不报警，1表示跳变报警"
        Me.alarm.Width = 120
        '
        'kgcontrol_box_name
        '
        Me.kgcontrol_box_name.DataPropertyName = "control_box_name"
        Me.kgcontrol_box_name.HeaderText = "control_box_name"
        Me.kgcontrol_box_name.Name = "kgcontrol_box_name"
        Me.kgcontrol_box_name.Visible = False
        '
        'kgid
        '
        Me.kgid.DataPropertyName = "id"
        Me.kgid.HeaderText = "id"
        Me.kgid.Name = "kgid"
        Me.kgid.ReadOnly = True
        Me.kgid.Visible = False
        '
        'KaiguanlistBindingSource
        '
        Me.KaiguanlistBindingSource.DataMember = "kaiguan_list"
        Me.KaiguanlistBindingSource.DataSource = Me.KaiguanDataSet
        '
        'KaiguanDataSet
        '
        Me.KaiguanDataSet.DataSetName = "kaiguanDataSet"
        Me.KaiguanDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'GroupBoxmoniliang
        '
        Me.GroupBoxmoniliang.Controls.Add(Me.chb_allhuilu)
        Me.GroupBoxmoniliang.Controls.Add(Me.bt_save_huilu)
        Me.GroupBoxmoniliang.Controls.Add(Me.dgv_huilu_list)
        Me.GroupBoxmoniliang.Controls.Add(Me.bt_del_huilu)
        Me.GroupBoxmoniliang.Location = New System.Drawing.Point(405, 15)
        Me.GroupBoxmoniliang.Name = "GroupBoxmoniliang"
        Me.GroupBoxmoniliang.Size = New System.Drawing.Size(471, 244)
        Me.GroupBoxmoniliang.TabIndex = 163
        Me.GroupBoxmoniliang.TabStop = False
        Me.GroupBoxmoniliang.Text = "编辑模拟量"
        '
        'chb_allhuilu
        '
        Me.chb_allhuilu.AutoSize = True
        Me.chb_allhuilu.Location = New System.Drawing.Point(9, 195)
        Me.chb_allhuilu.Name = "chb_allhuilu"
        Me.chb_allhuilu.Size = New System.Drawing.Size(48, 16)
        Me.chb_allhuilu.TabIndex = 13
        Me.chb_allhuilu.Text = "全选"
        Me.chb_allhuilu.UseVisualStyleBackColor = True
        '
        'bt_save_huilu
        '
        Me.bt_save_huilu.Location = New System.Drawing.Point(88, 214)
        Me.bt_save_huilu.Name = "bt_save_huilu"
        Me.bt_save_huilu.Size = New System.Drawing.Size(86, 25)
        Me.bt_save_huilu.TabIndex = 2
        Me.bt_save_huilu.Text = "保存"
        Me.bt_save_huilu.UseVisualStyleBackColor = True
        '
        'dgv_huilu_list
        '
        Me.dgv_huilu_list.AllowUserToAddRows = False
        Me.dgv_huilu_list.AutoGenerateColumns = False
        Me.dgv_huilu_list.BackgroundColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle6.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_huilu_list.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.dgv_huilu_list.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_huilu_list.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.checkid, Me.huilu_id, Me.jiechuqi, Me.bianbi, Me.current_alarmtop, Me.current_alarmbot, Me.presure_type, Me.information, Me.id})
        Me.dgv_huilu_list.DataSource = Me.HuiluinfBindingSource
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle7.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv_huilu_list.DefaultCellStyle = DataGridViewCellStyle7
        Me.dgv_huilu_list.Location = New System.Drawing.Point(9, 25)
        Me.dgv_huilu_list.Name = "dgv_huilu_list"
        Me.dgv_huilu_list.RowHeadersVisible = False
        Me.dgv_huilu_list.RowTemplate.Height = 23
        Me.dgv_huilu_list.Size = New System.Drawing.Size(451, 165)
        Me.dgv_huilu_list.TabIndex = 0
        '
        'checkid
        '
        Me.checkid.FalseValue = "0"
        Me.checkid.HeaderText = ""
        Me.checkid.Name = "checkid"
        Me.checkid.TrueValue = "1"
        Me.checkid.Width = 25
        '
        'huilu_id
        '
        Me.huilu_id.DataPropertyName = "huilu_id"
        Me.huilu_id.HeaderText = "回路"
        Me.huilu_id.Name = "huilu_id"
        Me.huilu_id.Width = 60
        '
        'jiechuqi
        '
        Me.jiechuqi.DataPropertyName = "jiechuqi"
        Me.jiechuqi.HeaderText = "接触器"
        Me.jiechuqi.Name = "jiechuqi"
        Me.jiechuqi.ReadOnly = True
        Me.jiechuqi.Width = 75
        '
        'bianbi
        '
        Me.bianbi.DataPropertyName = "bianbi"
        Me.bianbi.HeaderText = "变比"
        Me.bianbi.Name = "bianbi"
        Me.bianbi.Width = 60
        '
        'current_alarmtop
        '
        Me.current_alarmtop.DataPropertyName = "current_alarmtop"
        Me.current_alarmtop.HeaderText = "报警上限"
        Me.current_alarmtop.Name = "current_alarmtop"
        Me.current_alarmtop.ToolTipText = "电流报警上限"
        '
        'current_alarmbot
        '
        Me.current_alarmbot.DataPropertyName = "current_alarmbot"
        Me.current_alarmbot.HeaderText = "报警下限"
        Me.current_alarmbot.Name = "current_alarmbot"
        Me.current_alarmbot.ToolTipText = "电流报警上限"
        '
        'presure_type
        '
        Me.presure_type.DataPropertyName = "presure_type"
        Me.presure_type.HeaderText = "回路电压"
        Me.presure_type.Name = "presure_type"
        Me.presure_type.ToolTipText = "0表示A相，1表示B相，2表示C相"
        '
        'information
        '
        Me.information.DataPropertyName = "information"
        Me.information.HeaderText = "备注"
        Me.information.Name = "information"
        Me.information.Visible = False
        '
        'id
        '
        Me.id.DataPropertyName = "id"
        Me.id.HeaderText = "id"
        Me.id.Name = "id"
        Me.id.ReadOnly = True
        Me.id.Visible = False
        '
        'HuiluinfBindingSource
        '
        Me.HuiluinfBindingSource.DataMember = "huilu_inf"
        Me.HuiluinfBindingSource.DataSource = Me.EditDataSet
        '
        'EditDataSet
        '
        Me.EditDataSet.DataSetName = "EditDataSet"
        Me.EditDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'bt_del_huilu
        '
        Me.bt_del_huilu.Location = New System.Drawing.Point(223, 214)
        Me.bt_del_huilu.Name = "bt_del_huilu"
        Me.bt_del_huilu.Size = New System.Drawing.Size(86, 25)
        Me.bt_del_huilu.TabIndex = 1
        Me.bt_del_huilu.Text = "删除"
        Me.bt_del_huilu.UseVisualStyleBackColor = True
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(351, 79)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(0, 12)
        Me.Label12.TabIndex = 159
        '
        'GroupBoxhuilu
        '
        Me.GroupBoxhuilu.Controls.Add(Me.clb_huilu_idlist)
        Me.GroupBoxhuilu.Location = New System.Drawing.Point(192, 79)
        Me.GroupBoxhuilu.Name = "GroupBoxhuilu"
        Me.GroupBoxhuilu.Size = New System.Drawing.Size(207, 305)
        Me.GroupBoxhuilu.TabIndex = 156
        Me.GroupBoxhuilu.TabStop = False
        Me.GroupBoxhuilu.Text = "回路"
        '
        'clb_huilu_idlist
        '
        Me.clb_huilu_idlist.FormattingEnabled = True
        Me.clb_huilu_idlist.Location = New System.Drawing.Point(9, 19)
        Me.clb_huilu_idlist.Name = "clb_huilu_idlist"
        Me.clb_huilu_idlist.Size = New System.Drawing.Size(193, 260)
        Me.clb_huilu_idlist.TabIndex = 155
        '
        'tv_box_inf_list
        '
        Me.tv_box_inf_list.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.tv_box_inf_list.Location = New System.Drawing.Point(5, 19)
        Me.tv_box_inf_list.Name = "tv_box_inf_list"
        Me.tv_box_inf_list.Size = New System.Drawing.Size(178, 470)
        Me.tv_box_inf_list.TabIndex = 2
        '
        'huilu_group_list
        '
        Me.huilu_group_list.Controls.Add(Me.rb_k6_list)
        Me.huilu_group_list.Controls.Add(Me.rb_k5_list)
        Me.huilu_group_list.Controls.Add(Me.rb_k4_list)
        Me.huilu_group_list.Controls.Add(Me.rb_k3_list)
        Me.huilu_group_list.Controls.Add(Me.rb_k2_list)
        Me.huilu_group_list.Controls.Add(Me.rb_k1_list)
        Me.huilu_group_list.Location = New System.Drawing.Point(192, 15)
        Me.huilu_group_list.Name = "huilu_group_list"
        Me.huilu_group_list.Size = New System.Drawing.Size(207, 64)
        Me.huilu_group_list.TabIndex = 154
        Me.huilu_group_list.TabStop = False
        Me.huilu_group_list.Text = "接触器"
        '
        'rb_k6_list
        '
        Me.rb_k6_list.AutoSize = True
        Me.rb_k6_list.Location = New System.Drawing.Point(145, 40)
        Me.rb_k6_list.Name = "rb_k6_list"
        Me.rb_k6_list.Size = New System.Drawing.Size(35, 16)
        Me.rb_k6_list.TabIndex = 93
        Me.rb_k6_list.Text = "K6"
        Me.rb_k6_list.UseVisualStyleBackColor = True
        '
        'rb_k5_list
        '
        Me.rb_k5_list.AutoSize = True
        Me.rb_k5_list.Location = New System.Drawing.Point(71, 39)
        Me.rb_k5_list.Name = "rb_k5_list"
        Me.rb_k5_list.Size = New System.Drawing.Size(35, 16)
        Me.rb_k5_list.TabIndex = 92
        Me.rb_k5_list.Text = "K5"
        Me.rb_k5_list.UseVisualStyleBackColor = True
        '
        'rb_k4_list
        '
        Me.rb_k4_list.AutoSize = True
        Me.rb_k4_list.Location = New System.Drawing.Point(5, 42)
        Me.rb_k4_list.Name = "rb_k4_list"
        Me.rb_k4_list.Size = New System.Drawing.Size(35, 16)
        Me.rb_k4_list.TabIndex = 91
        Me.rb_k4_list.Text = "K4"
        Me.rb_k4_list.UseVisualStyleBackColor = True
        '
        'rb_k3_list
        '
        Me.rb_k3_list.AutoSize = True
        Me.rb_k3_list.Location = New System.Drawing.Point(145, 20)
        Me.rb_k3_list.Name = "rb_k3_list"
        Me.rb_k3_list.Size = New System.Drawing.Size(35, 16)
        Me.rb_k3_list.TabIndex = 90
        Me.rb_k3_list.Text = "K3"
        Me.rb_k3_list.UseVisualStyleBackColor = True
        '
        'rb_k2_list
        '
        Me.rb_k2_list.AutoSize = True
        Me.rb_k2_list.Location = New System.Drawing.Point(71, 19)
        Me.rb_k2_list.Name = "rb_k2_list"
        Me.rb_k2_list.Size = New System.Drawing.Size(35, 16)
        Me.rb_k2_list.TabIndex = 89
        Me.rb_k2_list.Text = "K2"
        Me.rb_k2_list.UseVisualStyleBackColor = True
        '
        'rb_k1_list
        '
        Me.rb_k1_list.AutoSize = True
        Me.rb_k1_list.Checked = True
        Me.rb_k1_list.Location = New System.Drawing.Point(5, 19)
        Me.rb_k1_list.Name = "rb_k1_list"
        Me.rb_k1_list.Size = New System.Drawing.Size(35, 16)
        Me.rb_k1_list.TabIndex = 88
        Me.rb_k1_list.TabStop = True
        Me.rb_k1_list.Text = "K1"
        Me.rb_k1_list.UseVisualStyleBackColor = True
        '
        'bt_send_alarmdata
        '
        Me.bt_send_alarmdata.Location = New System.Drawing.Point(753, 13)
        Me.bt_send_alarmdata.Name = "bt_send_alarmdata"
        Me.bt_send_alarmdata.Size = New System.Drawing.Size(114, 25)
        Me.bt_send_alarmdata.TabIndex = 160
        Me.bt_send_alarmdata.Text = "发送报警阈值配置"
        Me.bt_send_alarmdata.UseVisualStyleBackColor = True
        '
        'rb_box_check
        '
        Me.rb_box_check.AutoSize = True
        Me.rb_box_check.Location = New System.Drawing.Point(110, 17)
        Me.rb_box_check.Name = "rb_box_check"
        Me.rb_box_check.Size = New System.Drawing.Size(83, 16)
        Me.rb_box_check.TabIndex = 163
        Me.rb_box_check.Text = "主控箱名称"
        Me.rb_box_check.UseVisualStyleBackColor = True
        '
        'bt_send_kaiguan
        '
        Me.bt_send_kaiguan.Location = New System.Drawing.Point(489, 12)
        Me.bt_send_kaiguan.Name = "bt_send_kaiguan"
        Me.bt_send_kaiguan.Size = New System.Drawing.Size(106, 25)
        Me.bt_send_kaiguan.TabIndex = 162
        Me.bt_send_kaiguan.Text = "发送开关量数据"
        Me.bt_send_kaiguan.UseVisualStyleBackColor = True
        '
        'bt_send_huiluinf
        '
        Me.bt_send_huiluinf.Location = New System.Drawing.Point(357, 12)
        Me.bt_send_huiluinf.Name = "bt_send_huiluinf"
        Me.bt_send_huiluinf.Size = New System.Drawing.Size(106, 25)
        Me.bt_send_huiluinf.TabIndex = 159
        Me.bt_send_huiluinf.Text = "发送模拟量配置"
        Me.bt_send_huiluinf.UseVisualStyleBackColor = True
        '
        'Huilu_infTableAdapter
        '
        Me.Huilu_infTableAdapter.ClearBeforeFill = True
        '
        'Kaiguan_listTableAdapter
        '
        Me.Kaiguan_listTableAdapter.ClearBeforeFill = True
        '
        'ToolTip_lamp
        '
        Me.ToolTip_lamp.ToolTipTitle = "节点版本备注"
        '
        'check
        '
        Me.check.FalseValue = "0"
        Me.check.HeaderText = ""
        Me.check.Name = "check"
        Me.check.TrueValue = "1"
        Me.check.Width = 20
        '
        'control_box_name
        '
        Me.control_box_name.DataPropertyName = "control_box_name"
        Me.control_box_name.HeaderText = "网关编号"
        Me.control_box_name.Name = "control_box_name"
        Me.control_box_name.Width = 70
        '
        'type_string
        '
        Me.type_string.DataPropertyName = "type_string"
        Me.type_string.HeaderText = "类型名称"
        Me.type_string.Name = "type_string"
        Me.type_string.Width = 70
        '
        'lamp_id_part
        '
        Me.lamp_id_part.DataPropertyName = "lamp_id_part"
        Me.lamp_id_part.HeaderText = "编号"
        Me.lamp_id_part.Name = "lamp_id_part"
        Me.lamp_id_part.ReadOnly = True
        Me.lamp_id_part.Width = 70
        '
        'lamp_id_value
        '
        Me.lamp_id_value.DataPropertyName = "lamp_id"
        Me.lamp_id_value.HeaderText = "lamp_id"
        Me.lamp_id_value.Name = "lamp_id_value"
        Me.lamp_id_value.Visible = False
        Me.lamp_id_value.Width = 70
        '
        'lamp_pointinfor
        '
        Me.lamp_pointinfor.DataPropertyName = "lamp_pointinfor"
        Me.lamp_pointinfor.HeaderText = "联系电话"
        Me.lamp_pointinfor.Name = "lamp_pointinfor"
        '
        '增加终端
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(954, 657)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "增加终端"
        Me.Text = "编辑终端设备"
        CType(Me.dgv_old_lamp_list, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LampstreetBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Street_add, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.lamp_type_new.ResumeLayout(False)
        Me.lamp_type_new.PerformLayout()
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.gb_draw_method.ResumeLayout(False)
        Me.gb_draw_method.PerformLayout()
        Me.gb_drawpos.ResumeLayout(False)
        Me.gb_drawpos.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBoxkaiguanliang.ResumeLayout(False)
        Me.GroupBoxkaiguanliang.PerformLayout()
        Me.GroupBoxAlarm.ResumeLayout(False)
        Me.GroupBoxAlarm.PerformLayout()
        CType(Me.dgv_kaiguanliang_list, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.KaiguanlistBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.KaiguanDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBoxmoniliang.ResumeLayout(False)
        Me.GroupBoxmoniliang.PerformLayout()
        CType(Me.dgv_huilu_list, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.HuiluinfBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EditDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBoxhuilu.ResumeLayout(False)
        Me.huilu_group_list.ResumeLayout(False)
        Me.huilu_group_list.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cb_box_add As System.Windows.Forms.ComboBox
    Friend WithEvents box_add_string As System.Windows.Forms.Label
    Friend WithEvents lamp_id_string As System.Windows.Forms.Label
    Friend WithEvents tb_lamp_id As System.Windows.Forms.TextBox
    Friend WithEvents bt_add_new As System.Windows.Forms.Button
    Friend WithEvents dgv_old_lamp_list As System.Windows.Forms.DataGridView
    Friend WithEvents Street_add As streetlamp.street_add
    Friend WithEvents LampstreetBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents Lamp_streetTableAdapter As streetlamp.street_addTableAdapters.lamp_streetTableAdapter
    Friend WithEvents AreaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents StreetDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents tb_lamp_num As System.Windows.Forms.TextBox
    Friend WithEvents lamp_num_string As System.Windows.Forms.Label
    Friend WithEvents lb_lamp_id_start As System.Windows.Forms.Label
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents lamp_inf_text As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lamp_id_full As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lamp_type_new As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents cb_delete_lamp_id_start As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents rb_lamp_id_control As System.Windows.Forms.RadioButton
    Friend WithEvents rb_box_name_control As System.Windows.Forms.RadioButton
    Friend WithEvents lb_delete_lamp_id_end_start As System.Windows.Forms.Label
    Friend WithEvents delete_control_box_name_string As System.Windows.Forms.Label
    Friend WithEvents lb_delete_lamp_id_start_start As System.Windows.Forms.Label
    Friend WithEvents cb_delete_lamp_id_end As System.Windows.Forms.ComboBox
    Friend WithEvents cb_delete_control_box_name As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cb_delete_lamp_type As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents bt_choose_lamp As System.Windows.Forms.Button
    Friend WithEvents bt_delete_lamp As System.Windows.Forms.Button
    Friend WithEvents rb_city_control As System.Windows.Forms.RadioButton
    Friend WithEvents rtb_delete_detail As System.Windows.Forms.RichTextBox
    Friend WithEvents bt_delete_confirm As System.Windows.Forms.Button
    Friend WithEvents tb_end_pos_y As System.Windows.Forms.TextBox
    Friend WithEvents tb_end_pos_x As System.Windows.Forms.TextBox
    Friend WithEvents tb_start_pos_y As System.Windows.Forms.TextBox
    Friend WithEvents end_pos_string As System.Windows.Forms.Label
    Friend WithEvents tb_start_pos_x As System.Windows.Forms.TextBox
    Friend WithEvents start_pos_string As System.Windows.Forms.Label
    Friend WithEvents lb_lamp_type_id_add As System.Windows.Forms.Label
    Friend WithEvents lb_lamp_type_id_del As System.Windows.Forms.Label
    Friend WithEvents cb_lamp_type As System.Windows.Forms.ComboBox
    Friend WithEvents rb_addnexttwo_num As System.Windows.Forms.RadioButton
    Friend WithEvents rb_addnext_num As System.Windows.Forms.RadioButton
    Friend WithEvents rb_addnextthree_num As System.Windows.Forms.RadioButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents rb_lamp_type_control As System.Windows.Forms.RadioButton
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cb_city_add As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cb_street_all As System.Windows.Forms.ComboBox
    Friend WithEvents cb_area_add As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents rb_area_control As System.Windows.Forms.RadioButton
    Friend WithEvents rb_street_control As System.Windows.Forms.RadioButton
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cb_delete_street As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cb_delete_city As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents lb_jiechuqi_full_id As System.Windows.Forms.Label
    Friend WithEvents gb_drawpos As System.Windows.Forms.GroupBox
    Friend WithEvents rb_xadd As System.Windows.Forms.RadioButton
    Friend WithEvents rb_yadd As System.Windows.Forms.RadioButton
    Friend WithEvents gb_draw_method As System.Windows.Forms.GroupBox
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents tv_box_inf_list As System.Windows.Forms.TreeView
    Friend WithEvents GroupBoxhuilu As System.Windows.Forms.GroupBox
    Friend WithEvents clb_huilu_idlist As System.Windows.Forms.CheckedListBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents bt_set_huilu As System.Windows.Forms.Button
    Friend WithEvents huilu_group_list As System.Windows.Forms.GroupBox
    Friend WithEvents rb_k6_list As System.Windows.Forms.RadioButton
    Friend WithEvents rb_k5_list As System.Windows.Forms.RadioButton
    Friend WithEvents rb_k4_list As System.Windows.Forms.RadioButton
    Friend WithEvents rb_k3_list As System.Windows.Forms.RadioButton
    Friend WithEvents rb_k2_list As System.Windows.Forms.RadioButton
    Friend WithEvents rb_k1_list As System.Windows.Forms.RadioButton
    Friend WithEvents dgv_huilu_list As System.Windows.Forms.DataGridView
    Friend WithEvents EditDataSet As streetlamp.EditDataSet
    Friend WithEvents HuiluinfBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents Huilu_infTableAdapter As streetlamp.EditDataSetTableAdapters.huilu_infTableAdapter
    Friend WithEvents bt_del_huilu As System.Windows.Forms.Button
    Friend WithEvents jiechuqi_id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GroupBoxmoniliang As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBoxkaiguanliang As System.Windows.Forms.GroupBox
    Friend WithEvents bt_save_huilu As System.Windows.Forms.Button
    Friend WithEvents dgv_kaiguanliang_list As System.Windows.Forms.DataGridView
    Friend WithEvents KaiguanDataSet As streetlamp.kaiguanDataSet
    Friend WithEvents KaiguanlistBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents Kaiguan_listTableAdapter As streetlamp.kaiguanDataSetTableAdapters.kaiguan_listTableAdapter
    Friend WithEvents bt_save_kaiguan As System.Windows.Forms.Button
    Friend WithEvents bt_del_kaiguan As System.Windows.Forms.Button
    Friend WithEvents bt_send_alarmdata As System.Windows.Forms.Button
    Friend WithEvents bt_send_huiluinf As System.Windows.Forms.Button
    Friend WithEvents bt_send_kaiguan As System.Windows.Forms.Button
    Friend WithEvents cb_shiliang_list As System.Windows.Forms.ComboBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents GroupBoxAlarm As System.Windows.Forms.GroupBox
    Friend WithEvents bt_set_alarmvalue As System.Windows.Forms.Button
    Friend WithEvents cb_tiaobian_list As System.Windows.Forms.ComboBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents chb_set_alarm As System.Windows.Forms.CheckBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents tb_bianbi_value As System.Windows.Forms.TextBox
    Friend WithEvents kgcheckid As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents kaiguan_name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents kaiguan_tag As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents alarm As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents kgcontrol_box_name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents kgid As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents checkid As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents huilu_id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents jiechuqi As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents bianbi As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents current_alarmtop As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents current_alarmbot As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents presure_type As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents information As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chb_checkall As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents rb_check_3 As System.Windows.Forms.RadioButton
    Friend WithEvents rb_check_2 As System.Windows.Forms.RadioButton
    Friend WithEvents rb_check_1 As System.Windows.Forms.RadioButton
    Friend WithEvents rb_check_all As System.Windows.Forms.RadioButton
    Friend WithEvents chb_allhuilu As System.Windows.Forms.CheckBox
    Friend WithEvents cb_controlbox_name As System.Windows.Forms.ComboBox
    Friend WithEvents rb_checkall As System.Windows.Forms.RadioButton
    Friend WithEvents rb_box_check As System.Windows.Forms.RadioButton
    Friend WithEvents bt_sendboardnum As System.Windows.Forms.Button
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents cb_lampvision As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents rb_add_lampid As System.Windows.Forms.RadioButton
    Friend WithEvents rb_add_lamptype As System.Windows.Forms.RadioButton
    Friend WithEvents ToolTip_lamp As System.Windows.Forms.ToolTip
    Friend WithEvents LampPointInfor As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents cb_delete_area As System.Windows.Forms.ComboBox
    Friend WithEvents check As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents control_box_name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents type_string As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lamp_id_part As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lamp_id_value As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lamp_pointinfor As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
