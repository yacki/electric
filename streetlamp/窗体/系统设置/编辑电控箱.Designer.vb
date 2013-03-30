<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 编辑电控箱
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(编辑电控箱))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.dgv_control_inf = New System.Windows.Forms.DataGridView
        Me.control_box_id_col = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.box_name_col = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.control_box_type = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.IMEI = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.pos_x = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.pos_y = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.huilu_num = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.kaiguanliang_num = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.board_num = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Information = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ControlinfBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Control_box = New streetlamp.control_box
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.record_num = New System.Windows.Forms.ToolStripStatusLabel
        Me.lb_huilunum = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.lb_control_box_name = New System.Windows.Forms.TextBox
        Me.control_gprs_string = New System.Windows.Forms.Label
        Me.gprs_id_string = New System.Windows.Forms.Label
        Me.lb_imei = New System.Windows.Forms.TextBox
        Me.bt_edit_control_box = New System.Windows.Forms.Button
        Me.bt_delete_control_box = New System.Windows.Forms.Button
        Me.lb_start_pos_y = New System.Windows.Forms.TextBox
        Me.lb_start_pos_x = New System.Windows.Forms.TextBox
        Me.start_pos_string = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.lb_testboard_num = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Control_infTableAdapter = New streetlamp.control_boxTableAdapters.control_infTableAdapter
        Me.lb_box_type = New System.Windows.Forms.ComboBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.lb_box_information = New System.Windows.Forms.TextBox
        Me.ck_getmeter = New System.Windows.Forms.CheckBox
        Me.tb_powermeterid = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.tb_powermeter_type = New System.Windows.Forms.TextBox
        Me.tb_powermeter_bianbi = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgv_control_inf, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ControlinfBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Control_box, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.dgv_control_inf)
        Me.GroupBox1.Location = New System.Drawing.Point(17, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(788, 312)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'dgv_control_inf
        '
        Me.dgv_control_inf.AllowUserToAddRows = False
        Me.dgv_control_inf.AutoGenerateColumns = False
        Me.dgv_control_inf.BackgroundColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_control_inf.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv_control_inf.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_control_inf.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.control_box_id_col, Me.box_name_col, Me.control_box_type, Me.IMEI, Me.pos_x, Me.pos_y, Me.huilu_num, Me.kaiguanliang_num, Me.board_num, Me.Information})
        Me.dgv_control_inf.DataSource = Me.ControlinfBindingSource
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv_control_inf.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgv_control_inf.Location = New System.Drawing.Point(9, 20)
        Me.dgv_control_inf.Name = "dgv_control_inf"
        Me.dgv_control_inf.RowTemplate.Height = 23
        Me.dgv_control_inf.Size = New System.Drawing.Size(768, 284)
        Me.dgv_control_inf.TabIndex = 0
        '
        'control_box_id_col
        '
        Me.control_box_id_col.DataPropertyName = "control_box_id"
        Me.control_box_id_col.HeaderText = "编号"
        Me.control_box_id_col.Name = "control_box_id_col"
        Me.control_box_id_col.Width = 60
        '
        'box_name_col
        '
        Me.box_name_col.DataPropertyName = "control_box_name"
        Me.box_name_col.HeaderText = "名称"
        Me.box_name_col.Name = "box_name_col"
        Me.box_name_col.Width = 120
        '
        'control_box_type
        '
        Me.control_box_type.DataPropertyName = "control_box_type"
        Me.control_box_type.HeaderText = "版本"
        Me.control_box_type.Name = "control_box_type"
        Me.control_box_type.Width = 60
        '
        'IMEI
        '
        Me.IMEI.DataPropertyName = "IMEI"
        Me.IMEI.HeaderText = "IMEI"
        Me.IMEI.Name = "IMEI"
        Me.IMEI.Width = 120
        '
        'pos_x
        '
        Me.pos_x.DataPropertyName = "pos_x"
        Me.pos_x.HeaderText = "X坐标"
        Me.pos_x.Name = "pos_x"
        Me.pos_x.Width = 70
        '
        'pos_y
        '
        Me.pos_y.DataPropertyName = "pos_y"
        Me.pos_y.HeaderText = "Y坐标"
        Me.pos_y.Name = "pos_y"
        Me.pos_y.Width = 70
        '
        'huilu_num
        '
        Me.huilu_num.DataPropertyName = "huilu_num"
        Me.huilu_num.HeaderText = "模拟量路数"
        Me.huilu_num.Name = "huilu_num"
        '
        'kaiguanliang_num
        '
        Me.kaiguanliang_num.DataPropertyName = "kaiguanliang_num"
        Me.kaiguanliang_num.HeaderText = "开关量路数"
        Me.kaiguanliang_num.Name = "kaiguanliang_num"
        '
        'board_num
        '
        Me.board_num.DataPropertyName = "board_num"
        Me.board_num.HeaderText = "测量板个数"
        Me.board_num.Name = "board_num"
        '
        'Information
        '
        Me.Information.DataPropertyName = "Information"
        Me.Information.HeaderText = "备注"
        Me.Information.Name = "Information"
        '
        'ControlinfBindingSource
        '
        Me.ControlinfBindingSource.DataMember = "control_inf"
        Me.ControlinfBindingSource.DataSource = Me.Control_box
        '
        'Control_box
        '
        Me.Control_box.DataSetName = "control_box"
        Me.Control_box.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.record_num})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 487)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(821, 22)
        Me.StatusStrip1.TabIndex = 1
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'record_num
        '
        Me.record_num.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.record_num.Name = "record_num"
        Me.record_num.Size = New System.Drawing.Size(154, 17)
        Me.record_num.Text = "ToolStripStatusLabel1"
        '
        'lb_huilunum
        '
        Me.lb_huilunum.Location = New System.Drawing.Point(103, 387)
        Me.lb_huilunum.Name = "lb_huilunum"
        Me.lb_huilunum.Size = New System.Drawing.Size(121, 21)
        Me.lb_huilunum.TabIndex = 21
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(20, 390)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(77, 12)
        Me.Label3.TabIndex = 20
        Me.Label3.Text = "模拟量路数："
        '
        'lb_control_box_name
        '
        Me.lb_control_box_name.Location = New System.Drawing.Point(104, 350)
        Me.lb_control_box_name.Name = "lb_control_box_name"
        Me.lb_control_box_name.Size = New System.Drawing.Size(121, 21)
        Me.lb_control_box_name.TabIndex = 15
        '
        'control_gprs_string
        '
        Me.control_gprs_string.AutoSize = True
        Me.control_gprs_string.BackColor = System.Drawing.Color.Transparent
        Me.control_gprs_string.Location = New System.Drawing.Point(21, 353)
        Me.control_gprs_string.Name = "control_gprs_string"
        Me.control_gprs_string.Size = New System.Drawing.Size(77, 12)
        Me.control_gprs_string.TabIndex = 14
        Me.control_gprs_string.Text = "主控箱名称："
        '
        'gprs_id_string
        '
        Me.gprs_id_string.AutoSize = True
        Me.gprs_id_string.BackColor = System.Drawing.Color.Transparent
        Me.gprs_id_string.Location = New System.Drawing.Point(257, 353)
        Me.gprs_id_string.Name = "gprs_id_string"
        Me.gprs_id_string.Size = New System.Drawing.Size(41, 12)
        Me.gprs_id_string.TabIndex = 13
        Me.gprs_id_string.Text = "IMEI："
        '
        'lb_imei
        '
        Me.lb_imei.Location = New System.Drawing.Point(316, 350)
        Me.lb_imei.Name = "lb_imei"
        Me.lb_imei.Size = New System.Drawing.Size(121, 21)
        Me.lb_imei.TabIndex = 22
        '
        'bt_edit_control_box
        '
        Me.bt_edit_control_box.Location = New System.Drawing.Point(247, 461)
        Me.bt_edit_control_box.Name = "bt_edit_control_box"
        Me.bt_edit_control_box.Size = New System.Drawing.Size(75, 23)
        Me.bt_edit_control_box.TabIndex = 23
        Me.bt_edit_control_box.Text = "修改"
        Me.bt_edit_control_box.UseVisualStyleBackColor = True
        '
        'bt_delete_control_box
        '
        Me.bt_delete_control_box.Location = New System.Drawing.Point(490, 461)
        Me.bt_delete_control_box.Name = "bt_delete_control_box"
        Me.bt_delete_control_box.Size = New System.Drawing.Size(75, 23)
        Me.bt_delete_control_box.TabIndex = 24
        Me.bt_delete_control_box.Text = "删除"
        Me.bt_delete_control_box.UseVisualStyleBackColor = True
        '
        'lb_start_pos_y
        '
        Me.lb_start_pos_y.Location = New System.Drawing.Point(583, 345)
        Me.lb_start_pos_y.Name = "lb_start_pos_y"
        Me.lb_start_pos_y.ReadOnly = True
        Me.lb_start_pos_y.Size = New System.Drawing.Size(33, 21)
        Me.lb_start_pos_y.TabIndex = 152
        '
        'lb_start_pos_x
        '
        Me.lb_start_pos_x.Location = New System.Drawing.Point(548, 345)
        Me.lb_start_pos_x.Name = "lb_start_pos_x"
        Me.lb_start_pos_x.ReadOnly = True
        Me.lb_start_pos_x.Size = New System.Drawing.Size(33, 21)
        Me.lb_start_pos_x.TabIndex = 151
        '
        'start_pos_string
        '
        Me.start_pos_string.AutoSize = True
        Me.start_pos_string.BackColor = System.Drawing.Color.Transparent
        Me.start_pos_string.Location = New System.Drawing.Point(447, 351)
        Me.start_pos_string.Name = "start_pos_string"
        Me.start_pos_string.Size = New System.Drawing.Size(95, 12)
        Me.start_pos_string.TabIndex = 150
        Me.start_pos_string.Text = "位置坐标(X,Y)："
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(233, 390)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(77, 12)
        Me.Label1.TabIndex = 153
        Me.Label1.Text = "主控箱版本："
        '
        'lb_testboard_num
        '
        Me.lb_testboard_num.Location = New System.Drawing.Point(738, 345)
        Me.lb_testboard_num.Name = "lb_testboard_num"
        Me.lb_testboard_num.Size = New System.Drawing.Size(68, 21)
        Me.lb_testboard_num.TabIndex = 156
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(655, 348)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(77, 12)
        Me.Label2.TabIndex = 155
        Me.Label2.Text = "测量板个数："
        '
        'Control_infTableAdapter
        '
        Me.Control_infTableAdapter.ClearBeforeFill = True
        '
        'lb_box_type
        '
        Me.lb_box_type.FormattingEnabled = True
        Me.lb_box_type.Items.AddRange(New Object() {"1", "2", "3"})
        Me.lb_box_type.Location = New System.Drawing.Point(316, 387)
        Me.lb_box_type.Name = "lb_box_type"
        Me.lb_box_type.Size = New System.Drawing.Size(121, 20)
        Me.lb_box_type.TabIndex = 157
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(501, 389)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(41, 12)
        Me.Label4.TabIndex = 158
        Me.Label4.Text = "备注："
        '
        'lb_box_information
        '
        Me.lb_box_information.Location = New System.Drawing.Point(548, 387)
        Me.lb_box_information.Name = "lb_box_information"
        Me.lb_box_information.Size = New System.Drawing.Size(259, 21)
        Me.lb_box_information.TabIndex = 159
        '
        'ck_getmeter
        '
        Me.ck_getmeter.AutoSize = True
        Me.ck_getmeter.Location = New System.Drawing.Point(27, 430)
        Me.ck_getmeter.Name = "ck_getmeter"
        Me.ck_getmeter.Size = New System.Drawing.Size(48, 16)
        Me.ck_getmeter.TabIndex = 160
        Me.ck_getmeter.Text = "抄表"
        Me.ck_getmeter.UseVisualStyleBackColor = True
        '
        'tb_powermeterid
        '
        Me.tb_powermeterid.Location = New System.Drawing.Point(316, 423)
        Me.tb_powermeterid.Name = "tb_powermeterid"
        Me.tb_powermeterid.Size = New System.Drawing.Size(121, 21)
        Me.tb_powermeterid.TabIndex = 166
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(245, 431)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(65, 12)
        Me.Label10.TabIndex = 165
        Me.Label10.Text = "电表编号："
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(99, 431)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(53, 12)
        Me.Label9.TabIndex = 163
        Me.Label9.Text = "电表规约"
        '
        'tb_powermeter_type
        '
        Me.tb_powermeter_type.Location = New System.Drawing.Point(159, 426)
        Me.tb_powermeter_type.Name = "tb_powermeter_type"
        Me.tb_powermeter_type.Size = New System.Drawing.Size(66, 21)
        Me.tb_powermeter_type.TabIndex = 167
        '
        'tb_powermeter_bianbi
        '
        Me.tb_powermeter_bianbi.Location = New System.Drawing.Point(548, 423)
        Me.tb_powermeter_bianbi.Name = "tb_powermeter_bianbi"
        Me.tb_powermeter_bianbi.Size = New System.Drawing.Size(121, 21)
        Me.tb_powermeter_bianbi.TabIndex = 169
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(477, 429)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(65, 12)
        Me.Label5.TabIndex = 168
        Me.Label5.Text = "电表变比："
        '
        '编辑电控箱
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(821, 509)
        Me.Controls.Add(Me.tb_powermeter_bianbi)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.tb_powermeter_type)
        Me.Controls.Add(Me.tb_powermeterid)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.ck_getmeter)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.lb_box_information)
        Me.Controls.Add(Me.lb_box_type)
        Me.Controls.Add(Me.lb_testboard_num)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lb_start_pos_y)
        Me.Controls.Add(Me.lb_start_pos_x)
        Me.Controls.Add(Me.start_pos_string)
        Me.Controls.Add(Me.bt_delete_control_box)
        Me.Controls.Add(Me.bt_edit_control_box)
        Me.Controls.Add(Me.lb_imei)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lb_control_box_name)
        Me.Controls.Add(Me.control_gprs_string)
        Me.Controls.Add(Me.gprs_id_string)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.lb_huilunum)
        Me.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "编辑电控箱"
        Me.Text = "编辑主控箱信息(双击数据行进行相关操作！)"
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.dgv_control_inf, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ControlinfBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Control_box, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents dgv_control_inf As System.Windows.Forms.DataGridView
    Friend WithEvents Control_box As streetlamp.control_box
    Friend WithEvents ControlinfBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents Control_infTableAdapter As streetlamp.control_boxTableAdapters.control_infTableAdapter
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents record_num As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents control_box_id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents control_box_name_col As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lb_huilunum As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lb_control_box_name As System.Windows.Forms.TextBox
    Friend WithEvents control_gprs_string As System.Windows.Forms.Label
    Friend WithEvents gprs_id_string As System.Windows.Forms.Label
    Friend WithEvents lb_imei As System.Windows.Forms.TextBox
    Friend WithEvents bt_edit_control_box As System.Windows.Forms.Button
    Friend WithEvents bt_delete_control_box As System.Windows.Forms.Button
    Friend WithEvents lb_start_pos_y As System.Windows.Forms.TextBox
    Friend WithEvents lb_start_pos_x As System.Windows.Forms.TextBox
    Friend WithEvents start_pos_string As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lb_testboard_num As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lb_box_type As System.Windows.Forms.ComboBox
    Friend WithEvents control_box_id_col As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents box_name_col As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents control_box_type As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IMEI As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents pos_x As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents pos_y As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents huilu_num As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents kaiguanliang_num As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents board_num As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Information As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lb_box_information As System.Windows.Forms.TextBox
    Friend WithEvents ck_getmeter As System.Windows.Forms.CheckBox
    Friend WithEvents tb_powermeterid As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents tb_powermeter_type As System.Windows.Forms.TextBox
    Friend WithEvents tb_powermeter_bianbi As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
End Class
