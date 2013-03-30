<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 增加主控箱
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
        Me.components = New System.ComponentModel.Container
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(增加主控箱))
        Me.IMEI_string = New System.Windows.Forms.Label
        Me.tb_imei = New System.Windows.Forms.TextBox
        Me.bt_add_control_box = New System.Windows.Forms.Button
        Me.information_string = New System.Windows.Forms.Label
        Me.tb_huilu_num = New System.Windows.Forms.TextBox
        Me.dgv_control_box_list = New System.Windows.Forms.DataGridView
        Me.ControlboxidDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ControlboxnameDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.IMEIDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.kaiguanliang_num = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.board_num = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.BoxIMEIBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Box_inf = New streetlamp.Box_inf
        Me.tb_control_box_id = New System.Windows.Forms.TextBox
        Me.control_box_string = New System.Windows.Forms.Label
        Me.tb_box_name = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.cb_city_box_add = New System.Windows.Forms.ComboBox
        Me.cb_area_box_add = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.cb_street_box_add = New System.Windows.Forms.ComboBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.tb_start_pos_y = New System.Windows.Forms.TextBox
        Me.tb_start_pos_x = New System.Windows.Forms.TextBox
        Me.start_pos_string = New System.Windows.Forms.Label
        Me.tb_kaiguan_num = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.cb_testboard_num = New System.Windows.Forms.ComboBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.cb_control_box_type = New System.Windows.Forms.ComboBox
        Me.Box_IMEITableAdapter = New streetlamp.Box_infTableAdapters.Box_IMEITableAdapter
        Me.Label8 = New System.Windows.Forms.Label
        Me.tb_information = New System.Windows.Forms.TextBox
        Me.ck_getmeterdata = New System.Windows.Forms.CheckBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.cb_metertype = New System.Windows.Forms.ComboBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.tb_powermeterid = New System.Windows.Forms.TextBox
        Me.tb_powermeter_bianbi = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        CType(Me.dgv_control_box_list, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BoxIMEIBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Box_inf, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'IMEI_string
        '
        Me.IMEI_string.AutoSize = True
        Me.IMEI_string.BackColor = System.Drawing.Color.Transparent
        Me.IMEI_string.Location = New System.Drawing.Point(486, 96)
        Me.IMEI_string.Name = "IMEI_string"
        Me.IMEI_string.Size = New System.Drawing.Size(41, 12)
        Me.IMEI_string.TabIndex = 88
        Me.IMEI_string.Text = "IMEI："
        '
        'tb_imei
        '
        Me.tb_imei.Location = New System.Drawing.Point(533, 93)
        Me.tb_imei.Name = "tb_imei"
        Me.tb_imei.Size = New System.Drawing.Size(122, 21)
        Me.tb_imei.TabIndex = 89
        '
        'bt_add_control_box
        '
        Me.bt_add_control_box.Location = New System.Drawing.Point(333, 204)
        Me.bt_add_control_box.Name = "bt_add_control_box"
        Me.bt_add_control_box.Size = New System.Drawing.Size(75, 23)
        Me.bt_add_control_box.TabIndex = 93
        Me.bt_add_control_box.Text = "增加"
        Me.bt_add_control_box.UseVisualStyleBackColor = True
        '
        'information_string
        '
        Me.information_string.AutoSize = True
        Me.information_string.BackColor = System.Drawing.Color.Transparent
        Me.information_string.Location = New System.Drawing.Point(237, 96)
        Me.information_string.Name = "information_string"
        Me.information_string.Size = New System.Drawing.Size(77, 12)
        Me.information_string.TabIndex = 91
        Me.information_string.Text = "模拟量路数："
        '
        'tb_huilu_num
        '
        Me.tb_huilu_num.BackColor = System.Drawing.SystemColors.Window
        Me.tb_huilu_num.Location = New System.Drawing.Point(320, 93)
        Me.tb_huilu_num.Name = "tb_huilu_num"
        Me.tb_huilu_num.ReadOnly = True
        Me.tb_huilu_num.Size = New System.Drawing.Size(88, 21)
        Me.tb_huilu_num.TabIndex = 92
        Me.tb_huilu_num.Text = "12"
        '
        'dgv_control_box_list
        '
        Me.dgv_control_box_list.AllowUserToAddRows = False
        Me.dgv_control_box_list.AutoGenerateColumns = False
        Me.dgv_control_box_list.BackgroundColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_control_box_list.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv_control_box_list.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_control_box_list.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ControlboxidDataGridViewTextBoxColumn, Me.ControlboxnameDataGridViewTextBoxColumn, Me.DataGridViewTextBoxColumn1, Me.IMEIDataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn2, Me.kaiguanliang_num, Me.board_num, Me.DataGridViewTextBoxColumn3})
        Me.dgv_control_box_list.DataSource = Me.BoxIMEIBindingSource
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv_control_box_list.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgv_control_box_list.Location = New System.Drawing.Point(14, 232)
        Me.dgv_control_box_list.Name = "dgv_control_box_list"
        Me.dgv_control_box_list.RowTemplate.Height = 23
        Me.dgv_control_box_list.Size = New System.Drawing.Size(715, 217)
        Me.dgv_control_box_list.TabIndex = 92
        '
        'ControlboxidDataGridViewTextBoxColumn
        '
        Me.ControlboxidDataGridViewTextBoxColumn.DataPropertyName = "control_box_id"
        Me.ControlboxidDataGridViewTextBoxColumn.HeaderText = "编号"
        Me.ControlboxidDataGridViewTextBoxColumn.Name = "ControlboxidDataGridViewTextBoxColumn"
        Me.ControlboxidDataGridViewTextBoxColumn.Width = 60
        '
        'ControlboxnameDataGridViewTextBoxColumn
        '
        Me.ControlboxnameDataGridViewTextBoxColumn.DataPropertyName = "control_box_name"
        Me.ControlboxnameDataGridViewTextBoxColumn.HeaderText = "名称"
        Me.ControlboxnameDataGridViewTextBoxColumn.Name = "ControlboxnameDataGridViewTextBoxColumn"
        Me.ControlboxnameDataGridViewTextBoxColumn.Width = 120
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "control_box_type"
        Me.DataGridViewTextBoxColumn1.HeaderText = "版本"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.Width = 60
        '
        'IMEIDataGridViewTextBoxColumn1
        '
        Me.IMEIDataGridViewTextBoxColumn1.DataPropertyName = "IMEI"
        Me.IMEIDataGridViewTextBoxColumn1.HeaderText = "IMEI"
        Me.IMEIDataGridViewTextBoxColumn1.Name = "IMEIDataGridViewTextBoxColumn1"
        Me.IMEIDataGridViewTextBoxColumn1.Width = 150
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "huilu_num"
        Me.DataGridViewTextBoxColumn2.HeaderText = "模拟量路数"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.Width = 120
        '
        'kaiguanliang_num
        '
        Me.kaiguanliang_num.DataPropertyName = "kaiguanliang_num"
        Me.kaiguanliang_num.HeaderText = "开关量路数"
        Me.kaiguanliang_num.Name = "kaiguanliang_num"
        Me.kaiguanliang_num.Width = 120
        '
        'board_num
        '
        Me.board_num.DataPropertyName = "board_num"
        Me.board_num.HeaderText = "测量板个数"
        Me.board_num.Name = "board_num"
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "Information"
        Me.DataGridViewTextBoxColumn3.HeaderText = "备注"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        '
        'BoxIMEIBindingSource
        '
        Me.BoxIMEIBindingSource.DataMember = "Box_IMEI"
        Me.BoxIMEIBindingSource.DataSource = Me.Box_inf
        '
        'Box_inf
        '
        Me.Box_inf.DataSetName = "Box_inf"
        Me.Box_inf.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'tb_control_box_id
        '
        Me.tb_control_box_id.Location = New System.Drawing.Point(320, 58)
        Me.tb_control_box_id.Name = "tb_control_box_id"
        Me.tb_control_box_id.Size = New System.Drawing.Size(88, 21)
        Me.tb_control_box_id.TabIndex = 86
        '
        'control_box_string
        '
        Me.control_box_string.AutoSize = True
        Me.control_box_string.BackColor = System.Drawing.Color.Transparent
        Me.control_box_string.Location = New System.Drawing.Point(234, 61)
        Me.control_box_string.Name = "control_box_string"
        Me.control_box_string.Size = New System.Drawing.Size(77, 12)
        Me.control_box_string.TabIndex = 85
        Me.control_box_string.Text = "主控箱编号："
        '
        'tb_box_name
        '
        Me.tb_box_name.Location = New System.Drawing.Point(106, 58)
        Me.tb_box_name.Name = "tb_box_name"
        Me.tb_box_name.Size = New System.Drawing.Size(88, 21)
        Me.tb_box_name.TabIndex = 97
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(23, 61)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(77, 12)
        Me.Label1.TabIndex = 96
        Me.Label1.Text = "主控箱名称："
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(33, 33)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(59, 12)
        Me.Label2.TabIndex = 98
        Me.Label2.Text = "城市名称:"
        '
        'cb_city_box_add
        '
        Me.cb_city_box_add.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_city_box_add.FormattingEnabled = True
        Me.cb_city_box_add.Location = New System.Drawing.Point(106, 26)
        Me.cb_city_box_add.Name = "cb_city_box_add"
        Me.cb_city_box_add.Size = New System.Drawing.Size(88, 20)
        Me.cb_city_box_add.TabIndex = 142
        '
        'cb_area_box_add
        '
        Me.cb_area_box_add.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_area_box_add.FormattingEnabled = True
        Me.cb_area_box_add.Location = New System.Drawing.Point(320, 26)
        Me.cb_area_box_add.Name = "cb_area_box_add"
        Me.cb_area_box_add.Size = New System.Drawing.Size(88, 20)
        Me.cb_area_box_add.TabIndex = 144
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(246, 28)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(59, 12)
        Me.Label3.TabIndex = 143
        Me.Label3.Text = "区域名称:"
        '
        'cb_street_box_add
        '
        Me.cb_street_box_add.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_street_box_add.FormattingEnabled = True
        Me.cb_street_box_add.Location = New System.Drawing.Point(534, 26)
        Me.cb_street_box_add.Name = "cb_street_box_add"
        Me.cb_street_box_add.Size = New System.Drawing.Size(122, 20)
        Me.cb_street_box_add.TabIndex = 146
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(460, 27)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(59, 12)
        Me.Label4.TabIndex = 145
        Me.Label4.Text = "街道名称:"
        '
        'tb_start_pos_y
        '
        Me.tb_start_pos_y.Location = New System.Drawing.Point(375, 130)
        Me.tb_start_pos_y.Name = "tb_start_pos_y"
        Me.tb_start_pos_y.ReadOnly = True
        Me.tb_start_pos_y.Size = New System.Drawing.Size(33, 21)
        Me.tb_start_pos_y.TabIndex = 149
        '
        'tb_start_pos_x
        '
        Me.tb_start_pos_x.Location = New System.Drawing.Point(320, 130)
        Me.tb_start_pos_x.Name = "tb_start_pos_x"
        Me.tb_start_pos_x.ReadOnly = True
        Me.tb_start_pos_x.Size = New System.Drawing.Size(33, 21)
        Me.tb_start_pos_x.TabIndex = 148
        '
        'start_pos_string
        '
        Me.start_pos_string.AutoSize = True
        Me.start_pos_string.BackColor = System.Drawing.Color.Transparent
        Me.start_pos_string.Location = New System.Drawing.Point(219, 133)
        Me.start_pos_string.Name = "start_pos_string"
        Me.start_pos_string.Size = New System.Drawing.Size(95, 12)
        Me.start_pos_string.TabIndex = 147
        Me.start_pos_string.Text = "位置坐标(X,Y)："
        '
        'tb_kaiguan_num
        '
        Me.tb_kaiguan_num.BackColor = System.Drawing.SystemColors.Window
        Me.tb_kaiguan_num.Location = New System.Drawing.Point(106, 127)
        Me.tb_kaiguan_num.Name = "tb_kaiguan_num"
        Me.tb_kaiguan_num.ReadOnly = True
        Me.tb_kaiguan_num.Size = New System.Drawing.Size(88, 21)
        Me.tb_kaiguan_num.TabIndex = 151
        Me.tb_kaiguan_num.Text = "16"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Location = New System.Drawing.Point(23, 132)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(77, 12)
        Me.Label5.TabIndex = 150
        Me.Label5.Text = "开关量路数："
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Location = New System.Drawing.Point(23, 99)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(77, 12)
        Me.Label6.TabIndex = 152
        Me.Label6.Text = "测量板个数："
        '
        'cb_testboard_num
        '
        Me.cb_testboard_num.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_testboard_num.FormattingEnabled = True
        Me.cb_testboard_num.Items.AddRange(New Object() {"1", "2", "3"})
        Me.cb_testboard_num.Location = New System.Drawing.Point(106, 94)
        Me.cb_testboard_num.Name = "cb_testboard_num"
        Me.cb_testboard_num.Size = New System.Drawing.Size(88, 20)
        Me.cb_testboard_num.TabIndex = 153
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(451, 62)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(77, 12)
        Me.Label7.TabIndex = 154
        Me.Label7.Text = "主控箱版本："
        '
        'cb_control_box_type
        '
        Me.cb_control_box_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_control_box_type.FormattingEnabled = True
        Me.cb_control_box_type.Items.AddRange(New Object() {"1", "2", "3"})
        Me.cb_control_box_type.Location = New System.Drawing.Point(534, 59)
        Me.cb_control_box_type.Name = "cb_control_box_type"
        Me.cb_control_box_type.Size = New System.Drawing.Size(121, 20)
        Me.cb_control_box_type.TabIndex = 155
        '
        'Box_IMEITableAdapter
        '
        Me.Box_IMEITableAdapter.ClearBeforeFill = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(454, 166)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(65, 12)
        Me.Label8.TabIndex = 156
        Me.Label8.Text = "备注信息："
        '
        'tb_information
        '
        Me.tb_information.Location = New System.Drawing.Point(534, 164)
        Me.tb_information.Name = "tb_information"
        Me.tb_information.Size = New System.Drawing.Size(122, 21)
        Me.tb_information.TabIndex = 157
        '
        'ck_getmeterdata
        '
        Me.ck_getmeterdata.AutoSize = True
        Me.ck_getmeterdata.Location = New System.Drawing.Point(453, 129)
        Me.ck_getmeterdata.Name = "ck_getmeterdata"
        Me.ck_getmeterdata.Size = New System.Drawing.Size(48, 16)
        Me.ck_getmeterdata.TabIndex = 158
        Me.ck_getmeterdata.Text = "抄表"
        Me.ck_getmeterdata.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(531, 130)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(53, 12)
        Me.Label9.TabIndex = 159
        Me.Label9.Text = "电表规约"
        '
        'cb_metertype
        '
        Me.cb_metertype.Enabled = False
        Me.cb_metertype.FormattingEnabled = True
        Me.cb_metertype.Items.AddRange(New Object() {"97", "07"})
        Me.cb_metertype.Location = New System.Drawing.Point(591, 126)
        Me.cb_metertype.Name = "cb_metertype"
        Me.cb_metertype.Size = New System.Drawing.Size(66, 20)
        Me.cb_metertype.TabIndex = 160
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(33, 166)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(65, 12)
        Me.Label10.TabIndex = 161
        Me.Label10.Text = "电表编号："
        '
        'tb_powermeterid
        '
        Me.tb_powermeterid.Enabled = False
        Me.tb_powermeterid.Location = New System.Drawing.Point(106, 164)
        Me.tb_powermeterid.Name = "tb_powermeterid"
        Me.tb_powermeterid.Size = New System.Drawing.Size(88, 21)
        Me.tb_powermeterid.TabIndex = 162
        '
        'tb_powermeter_bianbi
        '
        Me.tb_powermeter_bianbi.Location = New System.Drawing.Point(320, 166)
        Me.tb_powermeter_bianbi.Name = "tb_powermeter_bianbi"
        Me.tb_powermeter_bianbi.Size = New System.Drawing.Size(88, 21)
        Me.tb_powermeter_bianbi.TabIndex = 164
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(246, 169)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(65, 12)
        Me.Label11.TabIndex = 163
        Me.Label11.Text = "电表变比："
        '
        '增加主控箱
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(739, 459)
        Me.Controls.Add(Me.tb_powermeter_bianbi)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.tb_powermeterid)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.cb_metertype)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.ck_getmeterdata)
        Me.Controls.Add(Me.tb_information)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.cb_control_box_type)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.cb_testboard_num)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.tb_kaiguan_num)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.tb_start_pos_y)
        Me.Controls.Add(Me.tb_start_pos_x)
        Me.Controls.Add(Me.start_pos_string)
        Me.Controls.Add(Me.cb_street_box_add)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cb_area_box_add)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cb_city_box_add)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.tb_box_name)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dgv_control_box_list)
        Me.Controls.Add(Me.tb_huilu_num)
        Me.Controls.Add(Me.information_string)
        Me.Controls.Add(Me.bt_add_control_box)
        Me.Controls.Add(Me.tb_imei)
        Me.Controls.Add(Me.IMEI_string)
        Me.Controls.Add(Me.tb_control_box_id)
        Me.Controls.Add(Me.control_box_string)
        Me.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "增加主控箱"
        Me.Text = "增加主控箱"
        CType(Me.dgv_control_box_list, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BoxIMEIBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Box_inf, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents IMEI_string As System.Windows.Forms.Label
    Friend WithEvents tb_imei As System.Windows.Forms.TextBox
    Friend WithEvents bt_add_control_box As System.Windows.Forms.Button
    Friend WithEvents information_string As System.Windows.Forms.Label
    Friend WithEvents tb_huilu_num As System.Windows.Forms.TextBox
    Friend WithEvents dgv_control_box_list As System.Windows.Forms.DataGridView
    Friend WithEvents tb_control_box_id As System.Windows.Forms.TextBox
    Friend WithEvents control_box_string As System.Windows.Forms.Label
    Friend WithEvents tb_box_name As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Box_inf As streetlamp.Box_inf
    Friend WithEvents BoxIMEIBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents Box_IMEITableAdapter As streetlamp.Box_infTableAdapters.Box_IMEITableAdapter
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cb_city_box_add As System.Windows.Forms.ComboBox
    Friend WithEvents cb_area_box_add As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cb_street_box_add As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents tb_start_pos_y As System.Windows.Forms.TextBox
    Friend WithEvents tb_start_pos_x As System.Windows.Forms.TextBox
    Friend WithEvents start_pos_string As System.Windows.Forms.Label
    Friend WithEvents tb_kaiguan_num As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cb_testboard_num As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cb_control_box_type As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents tb_information As System.Windows.Forms.TextBox
    Friend WithEvents ControlboxidDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ControlboxnameDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IMEIDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents kaiguanliang_num As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents board_num As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ck_getmeterdata As System.Windows.Forms.CheckBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cb_metertype As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents tb_powermeterid As System.Windows.Forms.TextBox
    Friend WithEvents tb_powermeter_bianbi As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
End Class
