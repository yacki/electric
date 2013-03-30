<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 抄表
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.tv_all_controlbox = New System.Windows.Forms.TreeView
        Me.dgv_datalist = New System.Windows.Forms.DataGridView
        Me.control_box_name = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.power_readdata = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.power_data = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.get_time = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.rb_zonggong = New System.Windows.Forms.RadioButton
        Me.rb_Axiang = New System.Windows.Forms.RadioButton
        Me.rb_Bxiang = New System.Windows.Forms.RadioButton
        Me.rb_Cxiang = New System.Windows.Forms.RadioButton
        Me.bt_startgetdata = New System.Windows.Forms.Button
        Me.bt_stopgetdata = New System.Windows.Forms.Button
        Me.BackgroundWorkergetdata = New System.ComponentModel.BackgroundWorker
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.powermeter_id = New System.Windows.Forms.Label
        Me.bt_updateid = New System.Windows.Forms.Button
        Me.cb_selectall = New System.Windows.Forms.CheckBox
        Me.bt_readmeterid = New System.Windows.Forms.Button
        Me.dgv_configlist = New System.Windows.Forms.DataGridView
        Me.check_id = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.config_controlboxid = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.config_controlboxname = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.powermeter_type = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.powermeter_id_setvalue = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.powermeter_id_getvalue = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.imei = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.config_id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ControlboxpowerBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.StreetlampDataSetpowermeter = New streetlamp.streetlampDataSetpowermeter
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.powermeter_string = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.TabPage3 = New System.Windows.Forms.TabPage
        Me.dgv_datarecord = New System.Windows.Forms.DataGridView
        Me.id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.read_data = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.readdata_cha = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.powerdata = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.powerdata_cha = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.data_time = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.bt_excel = New System.Windows.Forms.Button
        Me.bt_getrecord = New System.Windows.Forms.Button
        Me.dtp_date_start = New System.Windows.Forms.DateTimePicker
        Me.Date_start_string = New System.Windows.Forms.Label
        Me.dtp_date_end = New System.Windows.Forms.DateTimePicker
        Me.Date_end_string = New System.Windows.Forms.Label
        Me.tv_box_inf_list = New System.Windows.Forms.TreeView
        Me.Controlbox_powerTableAdapter = New streetlamp.streetlampDataSetpowermeterTableAdapters.controlbox_powerTableAdapter
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.progress = New System.Windows.Forms.ToolStripProgressBar
        CType(Me.dgv_datalist, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.dgv_configlist, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ControlboxpowerBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StreetlampDataSetpowermeter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        CType(Me.dgv_datarecord, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'tv_all_controlbox
        '
        Me.tv_all_controlbox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.tv_all_controlbox.CheckBoxes = True
        Me.tv_all_controlbox.Location = New System.Drawing.Point(5, 5)
        Me.tv_all_controlbox.Name = "tv_all_controlbox"
        Me.tv_all_controlbox.Size = New System.Drawing.Size(197, 407)
        Me.tv_all_controlbox.TabIndex = 175
        '
        'dgv_datalist
        '
        Me.dgv_datalist.AllowUserToAddRows = False
        Me.dgv_datalist.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv_datalist.BackgroundColor = System.Drawing.Color.WhiteSmoke
        Me.dgv_datalist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_datalist.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.control_box_name, Me.power_readdata, Me.power_data, Me.get_time})
        Me.dgv_datalist.Location = New System.Drawing.Point(207, 54)
        Me.dgv_datalist.Name = "dgv_datalist"
        Me.dgv_datalist.RowHeadersVisible = False
        Me.dgv_datalist.RowTemplate.Height = 23
        Me.dgv_datalist.Size = New System.Drawing.Size(540, 357)
        Me.dgv_datalist.TabIndex = 176
        '
        'control_box_name
        '
        Me.control_box_name.HeaderText = "主控箱名称"
        Me.control_box_name.Name = "control_box_name"
        Me.control_box_name.Width = 150
        '
        'power_readdata
        '
        Me.power_readdata.HeaderText = "电表读数(KWH)"
        Me.power_readdata.Name = "power_readdata"
        Me.power_readdata.Width = 150
        '
        'power_data
        '
        Me.power_data.HeaderText = "电量"
        Me.power_data.Name = "power_data"
        '
        'get_time
        '
        Me.get_time.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.get_time.HeaderText = "采集时间"
        Me.get_time.Name = "get_time"
        '
        'rb_zonggong
        '
        Me.rb_zonggong.AutoSize = True
        Me.rb_zonggong.Checked = True
        Me.rb_zonggong.Location = New System.Drawing.Point(5, 19)
        Me.rb_zonggong.Name = "rb_zonggong"
        Me.rb_zonggong.Size = New System.Drawing.Size(47, 16)
        Me.rb_zonggong.TabIndex = 177
        Me.rb_zonggong.TabStop = True
        Me.rb_zonggong.Text = "总功"
        Me.rb_zonggong.UseVisualStyleBackColor = True
        '
        'rb_Axiang
        '
        Me.rb_Axiang.AutoSize = True
        Me.rb_Axiang.Location = New System.Drawing.Point(84, 19)
        Me.rb_Axiang.Name = "rb_Axiang"
        Me.rb_Axiang.Size = New System.Drawing.Size(41, 16)
        Me.rb_Axiang.TabIndex = 178
        Me.rb_Axiang.TabStop = True
        Me.rb_Axiang.Text = "A相"
        Me.rb_Axiang.UseVisualStyleBackColor = True
        '
        'rb_Bxiang
        '
        Me.rb_Bxiang.AutoSize = True
        Me.rb_Bxiang.Location = New System.Drawing.Point(163, 19)
        Me.rb_Bxiang.Name = "rb_Bxiang"
        Me.rb_Bxiang.Size = New System.Drawing.Size(41, 16)
        Me.rb_Bxiang.TabIndex = 179
        Me.rb_Bxiang.TabStop = True
        Me.rb_Bxiang.Text = "B相"
        Me.rb_Bxiang.UseVisualStyleBackColor = True
        '
        'rb_Cxiang
        '
        Me.rb_Cxiang.AutoSize = True
        Me.rb_Cxiang.Location = New System.Drawing.Point(257, 19)
        Me.rb_Cxiang.Name = "rb_Cxiang"
        Me.rb_Cxiang.Size = New System.Drawing.Size(41, 16)
        Me.rb_Cxiang.TabIndex = 180
        Me.rb_Cxiang.TabStop = True
        Me.rb_Cxiang.Text = "C相"
        Me.rb_Cxiang.UseVisualStyleBackColor = True
        '
        'bt_startgetdata
        '
        Me.bt_startgetdata.Location = New System.Drawing.Point(207, 13)
        Me.bt_startgetdata.Name = "bt_startgetdata"
        Me.bt_startgetdata.Size = New System.Drawing.Size(75, 23)
        Me.bt_startgetdata.TabIndex = 181
        Me.bt_startgetdata.Text = "抄表"
        Me.bt_startgetdata.UseVisualStyleBackColor = True
        '
        'bt_stopgetdata
        '
        Me.bt_stopgetdata.Location = New System.Drawing.Point(308, 13)
        Me.bt_stopgetdata.Name = "bt_stopgetdata"
        Me.bt_stopgetdata.Size = New System.Drawing.Size(75, 23)
        Me.bt_stopgetdata.TabIndex = 182
        Me.bt_stopgetdata.Text = "停止"
        Me.bt_stopgetdata.UseVisualStyleBackColor = True
        '
        'BackgroundWorkergetdata
        '
        Me.BackgroundWorkergetdata.WorkerReportsProgress = True
        Me.BackgroundWorkergetdata.WorkerSupportsCancellation = True
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Location = New System.Drawing.Point(7, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(762, 429)
        Me.TabControl1.TabIndex = 183
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.powermeter_id)
        Me.TabPage1.Controls.Add(Me.bt_updateid)
        Me.TabPage1.Controls.Add(Me.cb_selectall)
        Me.TabPage1.Controls.Add(Me.bt_readmeterid)
        Me.TabPage1.Controls.Add(Me.dgv_configlist)
        Me.TabPage1.Location = New System.Drawing.Point(4, 21)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(754, 404)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "电表配置"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'powermeter_id
        '
        Me.powermeter_id.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.powermeter_id.AutoSize = True
        Me.powermeter_id.Location = New System.Drawing.Point(86, 380)
        Me.powermeter_id.Name = "powermeter_id"
        Me.powermeter_id.Size = New System.Drawing.Size(101, 12)
        Me.powermeter_id.TabIndex = 185
        Me.powermeter_id.Text = "正在读编号......"
        Me.powermeter_id.Visible = False
        '
        'bt_updateid
        '
        Me.bt_updateid.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.bt_updateid.Location = New System.Drawing.Point(375, 375)
        Me.bt_updateid.Name = "bt_updateid"
        Me.bt_updateid.Size = New System.Drawing.Size(64, 20)
        Me.bt_updateid.TabIndex = 180
        Me.bt_updateid.Text = "更新编号"
        Me.bt_updateid.UseVisualStyleBackColor = True
        '
        'cb_selectall
        '
        Me.cb_selectall.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cb_selectall.AutoSize = True
        Me.cb_selectall.Location = New System.Drawing.Point(5, 380)
        Me.cb_selectall.Name = "cb_selectall"
        Me.cb_selectall.Size = New System.Drawing.Size(48, 16)
        Me.cb_selectall.TabIndex = 179
        Me.cb_selectall.Text = "全选"
        Me.cb_selectall.UseVisualStyleBackColor = True
        '
        'bt_readmeterid
        '
        Me.bt_readmeterid.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.bt_readmeterid.Location = New System.Drawing.Point(230, 377)
        Me.bt_readmeterid.Name = "bt_readmeterid"
        Me.bt_readmeterid.Size = New System.Drawing.Size(64, 20)
        Me.bt_readmeterid.TabIndex = 178
        Me.bt_readmeterid.Text = "读取编号"
        Me.bt_readmeterid.UseVisualStyleBackColor = True
        '
        'dgv_configlist
        '
        Me.dgv_configlist.AllowUserToAddRows = False
        Me.dgv_configlist.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv_configlist.AutoGenerateColumns = False
        Me.dgv_configlist.BackgroundColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_configlist.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv_configlist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_configlist.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.check_id, Me.config_controlboxid, Me.config_controlboxname, Me.powermeter_type, Me.powermeter_id_setvalue, Me.powermeter_id_getvalue, Me.imei, Me.config_id})
        Me.dgv_configlist.DataSource = Me.ControlboxpowerBindingSource
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv_configlist.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgv_configlist.Location = New System.Drawing.Point(5, 5)
        Me.dgv_configlist.Name = "dgv_configlist"
        Me.dgv_configlist.RowHeadersVisible = False
        Me.dgv_configlist.RowTemplate.Height = 23
        Me.dgv_configlist.Size = New System.Drawing.Size(741, 359)
        Me.dgv_configlist.TabIndex = 177
        '
        'check_id
        '
        Me.check_id.FalseValue = "0"
        Me.check_id.HeaderText = ""
        Me.check_id.Name = "check_id"
        Me.check_id.TrueValue = "1"
        Me.check_id.Width = 20
        '
        'config_controlboxid
        '
        Me.config_controlboxid.DataPropertyName = "control_box_id"
        Me.config_controlboxid.HeaderText = "主控箱编号"
        Me.config_controlboxid.Name = "config_controlboxid"
        '
        'config_controlboxname
        '
        Me.config_controlboxname.DataPropertyName = "control_box_name"
        Me.config_controlboxname.HeaderText = "主控箱名称"
        Me.config_controlboxname.Name = "config_controlboxname"
        '
        'powermeter_type
        '
        Me.powermeter_type.DataPropertyName = "powermeter_type"
        Me.powermeter_type.HeaderText = "电表类型"
        Me.powermeter_type.Name = "powermeter_type"
        '
        'powermeter_id_setvalue
        '
        Me.powermeter_id_setvalue.DataPropertyName = "powermeter_id"
        Me.powermeter_id_setvalue.HeaderText = "电表编号（设置值）"
        Me.powermeter_id_setvalue.Name = "powermeter_id_setvalue"
        Me.powermeter_id_setvalue.Width = 220
        '
        'powermeter_id_getvalue
        '
        Me.powermeter_id_getvalue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.powermeter_id_getvalue.HeaderText = "电表编号（读取值）"
        Me.powermeter_id_getvalue.Name = "powermeter_id_getvalue"
        '
        'imei
        '
        Me.imei.DataPropertyName = "imei"
        Me.imei.HeaderText = "imei"
        Me.imei.Name = "imei"
        Me.imei.Visible = False
        '
        'config_id
        '
        Me.config_id.DataPropertyName = "id"
        Me.config_id.HeaderText = "id"
        Me.config_id.Name = "config_id"
        Me.config_id.ReadOnly = True
        Me.config_id.Visible = False
        '
        'ControlboxpowerBindingSource
        '
        Me.ControlboxpowerBindingSource.DataMember = "controlbox_power"
        Me.ControlboxpowerBindingSource.DataSource = Me.StreetlampDataSetpowermeter
        '
        'StreetlampDataSetpowermeter
        '
        Me.StreetlampDataSetpowermeter.DataSetName = "streetlampDataSetpowermeter"
        Me.StreetlampDataSetpowermeter.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.powermeter_string)
        Me.TabPage2.Controls.Add(Me.GroupBox1)
        Me.TabPage2.Controls.Add(Me.tv_all_controlbox)
        Me.TabPage2.Controls.Add(Me.bt_stopgetdata)
        Me.TabPage2.Controls.Add(Me.bt_startgetdata)
        Me.TabPage2.Controls.Add(Me.dgv_datalist)
        Me.TabPage2.Location = New System.Drawing.Point(4, 21)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(754, 404)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "手控抄表"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'powermeter_string
        '
        Me.powermeter_string.AutoSize = True
        Me.powermeter_string.Location = New System.Drawing.Point(430, 18)
        Me.powermeter_string.Name = "powermeter_string"
        Me.powermeter_string.Size = New System.Drawing.Size(89, 12)
        Me.powermeter_string.TabIndex = 184
        Me.powermeter_string.Text = "正在抄表......"
        Me.powermeter_string.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rb_Cxiang)
        Me.GroupBox1.Controls.Add(Me.rb_Bxiang)
        Me.GroupBox1.Controls.Add(Me.rb_Axiang)
        Me.GroupBox1.Controls.Add(Me.rb_zonggong)
        Me.GroupBox1.Location = New System.Drawing.Point(602, 11)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(53, 38)
        Me.GroupBox1.TabIndex = 183
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Visible = False
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.dgv_datarecord)
        Me.TabPage3.Controls.Add(Me.bt_excel)
        Me.TabPage3.Controls.Add(Me.bt_getrecord)
        Me.TabPage3.Controls.Add(Me.dtp_date_start)
        Me.TabPage3.Controls.Add(Me.Date_start_string)
        Me.TabPage3.Controls.Add(Me.dtp_date_end)
        Me.TabPage3.Controls.Add(Me.Date_end_string)
        Me.TabPage3.Controls.Add(Me.tv_box_inf_list)
        Me.TabPage3.Location = New System.Drawing.Point(4, 21)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(754, 404)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "抄表查询"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'dgv_datarecord
        '
        Me.dgv_datarecord.AllowUserToAddRows = False
        Me.dgv_datarecord.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv_datarecord.BackgroundColor = System.Drawing.Color.WhiteSmoke
        Me.dgv_datarecord.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_datarecord.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.id, Me.read_data, Me.readdata_cha, Me.powerdata, Me.powerdata_cha, Me.data_time})
        Me.dgv_datarecord.Location = New System.Drawing.Point(206, 65)
        Me.dgv_datarecord.Name = "dgv_datarecord"
        Me.dgv_datarecord.RowHeadersVisible = False
        Me.dgv_datarecord.RowTemplate.Height = 23
        Me.dgv_datarecord.Size = New System.Drawing.Size(544, 338)
        Me.dgv_datarecord.TabIndex = 153
        '
        'id
        '
        Me.id.HeaderText = "编号"
        Me.id.Name = "id"
        Me.id.Width = 60
        '
        'read_data
        '
        DataGridViewCellStyle3.Format = "N2"
        DataGridViewCellStyle3.NullValue = Nothing
        Me.read_data.DefaultCellStyle = DataGridViewCellStyle3
        Me.read_data.HeaderText = "总抄码"
        Me.read_data.Name = "read_data"
        Me.read_data.Width = 110
        '
        'readdata_cha
        '
        DataGridViewCellStyle4.Format = "N2"
        DataGridViewCellStyle4.NullValue = Nothing
        Me.readdata_cha.DefaultCellStyle = DataGridViewCellStyle4
        Me.readdata_cha.HeaderText = "抄码差额"
        Me.readdata_cha.Name = "readdata_cha"
        '
        'powerdata
        '
        DataGridViewCellStyle5.Format = "N2"
        DataGridViewCellStyle5.NullValue = Nothing
        Me.powerdata.DefaultCellStyle = DataGridViewCellStyle5
        Me.powerdata.HeaderText = "总电量"
        Me.powerdata.Name = "powerdata"
        '
        'powerdata_cha
        '
        DataGridViewCellStyle6.Format = "N2"
        DataGridViewCellStyle6.NullValue = Nothing
        Me.powerdata_cha.DefaultCellStyle = DataGridViewCellStyle6
        Me.powerdata_cha.HeaderText = "电量差额"
        Me.powerdata_cha.Name = "powerdata_cha"
        Me.powerdata_cha.Width = 110
        '
        'data_time
        '
        Me.data_time.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.data_time.HeaderText = "读表时间"
        Me.data_time.Name = "data_time"
        '
        'bt_excel
        '
        Me.bt_excel.Location = New System.Drawing.Point(510, 40)
        Me.bt_excel.Name = "bt_excel"
        Me.bt_excel.Size = New System.Drawing.Size(64, 20)
        Me.bt_excel.TabIndex = 152
        Me.bt_excel.Text = "EXCEL表"
        Me.bt_excel.UseVisualStyleBackColor = True
        '
        'bt_getrecord
        '
        Me.bt_getrecord.Location = New System.Drawing.Point(510, 6)
        Me.bt_getrecord.Name = "bt_getrecord"
        Me.bt_getrecord.Size = New System.Drawing.Size(64, 20)
        Me.bt_getrecord.TabIndex = 151
        Me.bt_getrecord.Text = "查询"
        Me.bt_getrecord.UseVisualStyleBackColor = True
        '
        'dtp_date_start
        '
        Me.dtp_date_start.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_date_start.Location = New System.Drawing.Point(270, 5)
        Me.dtp_date_start.Name = "dtp_date_start"
        Me.dtp_date_start.Size = New System.Drawing.Size(141, 21)
        Me.dtp_date_start.TabIndex = 149
        '
        'Date_start_string
        '
        Me.Date_start_string.AutoSize = True
        Me.Date_start_string.BackColor = System.Drawing.Color.Transparent
        Me.Date_start_string.Location = New System.Drawing.Point(200, 9)
        Me.Date_start_string.Name = "Date_start_string"
        Me.Date_start_string.Size = New System.Drawing.Size(65, 12)
        Me.Date_start_string.TabIndex = 147
        Me.Date_start_string.Text = "开始日期："
        '
        'dtp_date_end
        '
        Me.dtp_date_end.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_date_end.Location = New System.Drawing.Point(270, 40)
        Me.dtp_date_end.Name = "dtp_date_end"
        Me.dtp_date_end.Size = New System.Drawing.Size(141, 21)
        Me.dtp_date_end.TabIndex = 150
        '
        'Date_end_string
        '
        Me.Date_end_string.AutoSize = True
        Me.Date_end_string.BackColor = System.Drawing.Color.Transparent
        Me.Date_end_string.Location = New System.Drawing.Point(199, 45)
        Me.Date_end_string.Name = "Date_end_string"
        Me.Date_end_string.Size = New System.Drawing.Size(65, 12)
        Me.Date_end_string.TabIndex = 148
        Me.Date_end_string.Text = "结束日期："
        '
        'tv_box_inf_list
        '
        Me.tv_box_inf_list.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.tv_box_inf_list.Location = New System.Drawing.Point(5, 5)
        Me.tv_box_inf_list.Name = "tv_box_inf_list"
        Me.tv_box_inf_list.Size = New System.Drawing.Size(193, 482)
        Me.tv_box_inf_list.TabIndex = 2
        '
        'Controlbox_powerTableAdapter
        '
        Me.Controlbox_powerTableAdapter.ClearBeforeFill = True
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.progress})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 429)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Padding = New System.Windows.Forms.Padding(1, 0, 12, 0)
        Me.StatusStrip1.Size = New System.Drawing.Size(779, 22)
        Me.StatusStrip1.TabIndex = 184
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'progress
        '
        Me.progress.Name = "progress"
        Me.progress.Size = New System.Drawing.Size(86, 16)
        Me.progress.Visible = False
        '
        '抄表
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(779, 451)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.TabControl1)
        Me.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Name = "抄表"
        Me.Text = "抄表"
        CType(Me.dgv_datalist, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        CType(Me.dgv_configlist, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ControlboxpowerBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StreetlampDataSetpowermeter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        CType(Me.dgv_datarecord, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tv_all_controlbox As System.Windows.Forms.TreeView
    Friend WithEvents dgv_datalist As System.Windows.Forms.DataGridView
    Friend WithEvents rb_zonggong As System.Windows.Forms.RadioButton
    Friend WithEvents rb_Axiang As System.Windows.Forms.RadioButton
    Friend WithEvents rb_Bxiang As System.Windows.Forms.RadioButton
    Friend WithEvents rb_Cxiang As System.Windows.Forms.RadioButton
    Friend WithEvents bt_startgetdata As System.Windows.Forms.Button
    Friend WithEvents bt_stopgetdata As System.Windows.Forms.Button
    Friend WithEvents BackgroundWorkergetdata As System.ComponentModel.BackgroundWorker
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents dgv_configlist As System.Windows.Forms.DataGridView
    Friend WithEvents StreetlampDataSetpowermeter As streetlamp.streetlampDataSetpowermeter
    Friend WithEvents ControlboxpowerBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents Controlbox_powerTableAdapter As streetlamp.streetlampDataSetpowermeterTableAdapters.controlbox_powerTableAdapter
    Friend WithEvents bt_readmeterid As System.Windows.Forms.Button
    Friend WithEvents check_id As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents config_controlboxid As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents config_controlboxname As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents powermeter_type As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents powermeter_id_setvalue As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents powermeter_id_getvalue As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents imei As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents config_id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cb_selectall As System.Windows.Forms.CheckBox
    Friend WithEvents bt_updateid As System.Windows.Forms.Button
    Friend WithEvents control_box_name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents power_readdata As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents power_data As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents get_time As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents tv_box_inf_list As System.Windows.Forms.TreeView
    Friend WithEvents dtp_date_start As System.Windows.Forms.DateTimePicker
    Friend WithEvents Date_start_string As System.Windows.Forms.Label
    Friend WithEvents dtp_date_end As System.Windows.Forms.DateTimePicker
    Friend WithEvents Date_end_string As System.Windows.Forms.Label
    Friend WithEvents bt_getrecord As System.Windows.Forms.Button
    Friend WithEvents bt_excel As System.Windows.Forms.Button
    Friend WithEvents dgv_datarecord As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents read_data As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents readdata_cha As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents powerdata As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents powerdata_cha As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents data_time As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents powermeter_string As System.Windows.Forms.Label
    Friend WithEvents powermeter_id As System.Windows.Forms.Label
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents progress As System.Windows.Forms.ToolStripProgressBar
End Class
