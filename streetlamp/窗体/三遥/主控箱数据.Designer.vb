<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 主控箱数据
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.bt_static_excel = New System.Windows.Forms.Button()
        Me.dtp_date_start = New System.Windows.Forms.DateTimePicker()
        Me.dtp_date_end = New System.Windows.Forms.DateTimePicker()
        Me.Date_end_string = New System.Windows.Forms.Label()
        Me.Date_start_string = New System.Windows.Forms.Label()
        Me.clear_text = New System.Windows.Forms.Button()
        Me.tv_yaoce_controlbox = New System.Windows.Forms.TreeView()
        Me.bt_stop = New System.Windows.Forms.Button()
        Me.bt_yaoce = New System.Windows.Forms.Button()
        Me.dgv_boxinf = New System.Windows.Forms.DataGridView()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.dgv_statelist = New System.Windows.Forms.DataGridView()
        Me.bt_excel_state = New System.Windows.Forms.Button()
        Me.bt_clear_state = New System.Windows.Forms.Button()
        Me.bt_stop_state = New System.Windows.Forms.Button()
        Me.bt_find_state = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cb_state_list = New System.Windows.Forms.ComboBox()
        Me.dtp_starttime_state = New System.Windows.Forms.DateTimePicker()
        Me.dtp_endtime_state = New System.Windows.Forms.DateTimePicker()
        Me.tv_boxlist_state = New System.Windows.Forms.TreeView()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.dgv_kaiguanlist = New System.Windows.Forms.DataGridView()
        Me.bt_excel_kaiguan = New System.Windows.Forms.Button()
        Me.bt_clear_kaiguan = New System.Windows.Forms.Button()
        Me.bt_stop_kaiguan = New System.Windows.Forms.Button()
        Me.bt_find_kaiguan = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.dtp_starttime_kaiguan = New System.Windows.Forms.DateTimePicker()
        Me.dtp_endtime_kaiguan = New System.Windows.Forms.DateTimePicker()
        Me.tv_boxlist_kaiguan = New System.Windows.Forms.TreeView()
        Me.BackgroundWorker_find = New System.ComponentModel.BackgroundWorker()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ProgressBar = New System.Windows.Forms.ToolStripProgressBar()
        Me.id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.box_name = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.huilu = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.xiangwei = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.presure = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.current = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.power = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.power_yinshu = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.createtime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.id_state = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.controlboxname_state = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.state_kind = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.state = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.time_state_start = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.time_state_end = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.id_kaiguan = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.controlboxname_kaiguan = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.state_kaiguan = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.starttime_kaiguan = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.endtime_kaiguan = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.dgv_boxinf, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.dgv_statelist, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        CType(Me.dgv_kaiguanlist, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Location = New System.Drawing.Point(-1, 3)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(900, 577)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.bt_static_excel)
        Me.TabPage1.Controls.Add(Me.dtp_date_start)
        Me.TabPage1.Controls.Add(Me.dtp_date_end)
        Me.TabPage1.Controls.Add(Me.Date_end_string)
        Me.TabPage1.Controls.Add(Me.Date_start_string)
        Me.TabPage1.Controls.Add(Me.clear_text)
        Me.TabPage1.Controls.Add(Me.tv_yaoce_controlbox)
        Me.TabPage1.Controls.Add(Me.bt_stop)
        Me.TabPage1.Controls.Add(Me.bt_yaoce)
        Me.TabPage1.Controls.Add(Me.dgv_boxinf)
        Me.TabPage1.Location = New System.Drawing.Point(4, 21)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(892, 552)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "三遥数据查询"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'bt_static_excel
        '
        Me.bt_static_excel.Location = New System.Drawing.Point(740, 46)
        Me.bt_static_excel.Name = "bt_static_excel"
        Me.bt_static_excel.Size = New System.Drawing.Size(90, 23)
        Me.bt_static_excel.TabIndex = 193
        Me.bt_static_excel.Text = "运行统计报表"
        Me.bt_static_excel.UseVisualStyleBackColor = True
        '
        'dtp_date_start
        '
        Me.dtp_date_start.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_date_start.Location = New System.Drawing.Point(300, 12)
        Me.dtp_date_start.Name = "dtp_date_start"
        Me.dtp_date_start.Size = New System.Drawing.Size(161, 21)
        Me.dtp_date_start.TabIndex = 190
        '
        'dtp_date_end
        '
        Me.dtp_date_end.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_date_end.Location = New System.Drawing.Point(301, 46)
        Me.dtp_date_end.Name = "dtp_date_end"
        Me.dtp_date_end.Size = New System.Drawing.Size(160, 21)
        Me.dtp_date_end.TabIndex = 191
        '
        'Date_end_string
        '
        Me.Date_end_string.AutoSize = True
        Me.Date_end_string.BackColor = System.Drawing.Color.Transparent
        Me.Date_end_string.Location = New System.Drawing.Point(230, 50)
        Me.Date_end_string.Name = "Date_end_string"
        Me.Date_end_string.Size = New System.Drawing.Size(65, 12)
        Me.Date_end_string.TabIndex = 189
        Me.Date_end_string.Text = "结束日期："
        '
        'Date_start_string
        '
        Me.Date_start_string.AutoSize = True
        Me.Date_start_string.BackColor = System.Drawing.Color.Transparent
        Me.Date_start_string.Location = New System.Drawing.Point(229, 17)
        Me.Date_start_string.Name = "Date_start_string"
        Me.Date_start_string.Size = New System.Drawing.Size(65, 12)
        Me.Date_start_string.TabIndex = 188
        Me.Date_start_string.Text = "开始日期："
        '
        'clear_text
        '
        Me.clear_text.Location = New System.Drawing.Point(552, 45)
        Me.clear_text.Name = "clear_text"
        Me.clear_text.Size = New System.Drawing.Size(90, 23)
        Me.clear_text.TabIndex = 187
        Me.clear_text.Text = "清空"
        Me.clear_text.UseVisualStyleBackColor = True
        '
        'tv_yaoce_controlbox
        '
        Me.tv_yaoce_controlbox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.tv_yaoce_controlbox.CheckBoxes = True
        Me.tv_yaoce_controlbox.Location = New System.Drawing.Point(3, 3)
        Me.tv_yaoce_controlbox.Name = "tv_yaoce_controlbox"
        Me.tv_yaoce_controlbox.Size = New System.Drawing.Size(221, 543)
        Me.tv_yaoce_controlbox.TabIndex = 185
        '
        'bt_stop
        '
        Me.bt_stop.Location = New System.Drawing.Point(740, 9)
        Me.bt_stop.Name = "bt_stop"
        Me.bt_stop.Size = New System.Drawing.Size(90, 23)
        Me.bt_stop.TabIndex = 184
        Me.bt_stop.Text = "停止"
        Me.bt_stop.UseVisualStyleBackColor = True
        '
        'bt_yaoce
        '
        Me.bt_yaoce.Location = New System.Drawing.Point(552, 9)
        Me.bt_yaoce.Name = "bt_yaoce"
        Me.bt_yaoce.Size = New System.Drawing.Size(90, 23)
        Me.bt_yaoce.TabIndex = 183
        Me.bt_yaoce.Text = "查询"
        Me.bt_yaoce.UseVisualStyleBackColor = True
        '
        'dgv_boxinf
        '
        Me.dgv_boxinf.AllowUserToAddRows = False
        Me.dgv_boxinf.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv_boxinf.BackgroundColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_boxinf.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv_boxinf.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_boxinf.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.id, Me.box_name, Me.huilu, Me.xiangwei, Me.presure, Me.current, Me.power, Me.power_yinshu, Me.createtime})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_boxinf.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgv_boxinf.Location = New System.Drawing.Point(231, 75)
        Me.dgv_boxinf.Name = "dgv_boxinf"
        Me.dgv_boxinf.ReadOnly = True
        Me.dgv_boxinf.RowHeadersVisible = False
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_boxinf.RowsDefaultCellStyle = DataGridViewCellStyle3
        Me.dgv_boxinf.RowTemplate.Height = 23
        Me.dgv_boxinf.Size = New System.Drawing.Size(653, 471)
        Me.dgv_boxinf.TabIndex = 179
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.dgv_statelist)
        Me.TabPage2.Controls.Add(Me.bt_excel_state)
        Me.TabPage2.Controls.Add(Me.bt_clear_state)
        Me.TabPage2.Controls.Add(Me.bt_stop_state)
        Me.TabPage2.Controls.Add(Me.bt_find_state)
        Me.TabPage2.Controls.Add(Me.Label1)
        Me.TabPage2.Controls.Add(Me.Label2)
        Me.TabPage2.Controls.Add(Me.Label3)
        Me.TabPage2.Controls.Add(Me.cb_state_list)
        Me.TabPage2.Controls.Add(Me.dtp_starttime_state)
        Me.TabPage2.Controls.Add(Me.dtp_endtime_state)
        Me.TabPage2.Controls.Add(Me.tv_boxlist_state)
        Me.TabPage2.Location = New System.Drawing.Point(4, 21)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(892, 552)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "三遥状态查询"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'dgv_statelist
        '
        Me.dgv_statelist.AllowUserToAddRows = False
        Me.dgv_statelist.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv_statelist.BackgroundColor = System.Drawing.Color.WhiteSmoke
        Me.dgv_statelist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_statelist.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.id_state, Me.controlboxname_state, Me.state_kind, Me.state, Me.time_state_start, Me.time_state_end})
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle4.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_statelist.DefaultCellStyle = DataGridViewCellStyle4
        Me.dgv_statelist.Location = New System.Drawing.Point(238, 105)
        Me.dgv_statelist.Name = "dgv_statelist"
        Me.dgv_statelist.ReadOnly = True
        Me.dgv_statelist.RowHeadersVisible = False
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_statelist.RowsDefaultCellStyle = DataGridViewCellStyle5
        Me.dgv_statelist.RowTemplate.Height = 23
        Me.dgv_statelist.Size = New System.Drawing.Size(648, 441)
        Me.dgv_statelist.TabIndex = 198
        '
        'bt_excel_state
        '
        Me.bt_excel_state.Location = New System.Drawing.Point(699, 67)
        Me.bt_excel_state.Name = "bt_excel_state"
        Me.bt_excel_state.Size = New System.Drawing.Size(90, 23)
        Me.bt_excel_state.TabIndex = 197
        Me.bt_excel_state.Text = "运行统计报表"
        Me.bt_excel_state.UseVisualStyleBackColor = True
        '
        'bt_clear_state
        '
        Me.bt_clear_state.Location = New System.Drawing.Point(541, 67)
        Me.bt_clear_state.Name = "bt_clear_state"
        Me.bt_clear_state.Size = New System.Drawing.Size(90, 23)
        Me.bt_clear_state.TabIndex = 196
        Me.bt_clear_state.Text = "清空"
        Me.bt_clear_state.UseVisualStyleBackColor = True
        '
        'bt_stop_state
        '
        Me.bt_stop_state.Location = New System.Drawing.Point(699, 14)
        Me.bt_stop_state.Name = "bt_stop_state"
        Me.bt_stop_state.Size = New System.Drawing.Size(90, 23)
        Me.bt_stop_state.TabIndex = 195
        Me.bt_stop_state.Text = "停止"
        Me.bt_stop_state.UseVisualStyleBackColor = True
        '
        'bt_find_state
        '
        Me.bt_find_state.Location = New System.Drawing.Point(541, 14)
        Me.bt_find_state.Name = "bt_find_state"
        Me.bt_find_state.Size = New System.Drawing.Size(90, 23)
        Me.bt_find_state.TabIndex = 194
        Me.bt_find_state.Text = "查询"
        Me.bt_find_state.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(236, 78)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 12)
        Me.Label1.TabIndex = 183
        Me.Label1.Text = "数据状态："
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(236, 52)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 12)
        Me.Label2.TabIndex = 182
        Me.Label2.Text = "结束日期："
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(236, 20)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 12)
        Me.Label3.TabIndex = 181
        Me.Label3.Text = "开始日期："
        '
        'cb_state_list
        '
        Me.cb_state_list.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_state_list.FormattingEnabled = True
        Me.cb_state_list.Items.AddRange(New Object() {"故障", "正常", "全部"})
        Me.cb_state_list.Location = New System.Drawing.Point(307, 70)
        Me.cb_state_list.Name = "cb_state_list"
        Me.cb_state_list.Size = New System.Drawing.Size(150, 20)
        Me.cb_state_list.TabIndex = 180
        '
        'dtp_starttime_state
        '
        Me.dtp_starttime_state.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_starttime_state.Location = New System.Drawing.Point(307, 16)
        Me.dtp_starttime_state.Name = "dtp_starttime_state"
        Me.dtp_starttime_state.Size = New System.Drawing.Size(150, 21)
        Me.dtp_starttime_state.TabIndex = 178
        '
        'dtp_endtime_state
        '
        Me.dtp_endtime_state.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_endtime_state.Location = New System.Drawing.Point(307, 43)
        Me.dtp_endtime_state.Name = "dtp_endtime_state"
        Me.dtp_endtime_state.Size = New System.Drawing.Size(150, 21)
        Me.dtp_endtime_state.TabIndex = 179
        '
        'tv_boxlist_state
        '
        Me.tv_boxlist_state.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.tv_boxlist_state.CheckBoxes = True
        Me.tv_boxlist_state.Location = New System.Drawing.Point(9, 6)
        Me.tv_boxlist_state.Name = "tv_boxlist_state"
        Me.tv_boxlist_state.Size = New System.Drawing.Size(221, 540)
        Me.tv_boxlist_state.TabIndex = 177
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.dgv_kaiguanlist)
        Me.TabPage3.Controls.Add(Me.bt_excel_kaiguan)
        Me.TabPage3.Controls.Add(Me.bt_clear_kaiguan)
        Me.TabPage3.Controls.Add(Me.bt_stop_kaiguan)
        Me.TabPage3.Controls.Add(Me.bt_find_kaiguan)
        Me.TabPage3.Controls.Add(Me.Label5)
        Me.TabPage3.Controls.Add(Me.Label6)
        Me.TabPage3.Controls.Add(Me.dtp_starttime_kaiguan)
        Me.TabPage3.Controls.Add(Me.dtp_endtime_kaiguan)
        Me.TabPage3.Controls.Add(Me.tv_boxlist_kaiguan)
        Me.TabPage3.Location = New System.Drawing.Point(4, 21)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(892, 552)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "开关量报警信息"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'dgv_kaiguanlist
        '
        Me.dgv_kaiguanlist.AllowUserToAddRows = False
        Me.dgv_kaiguanlist.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv_kaiguanlist.BackgroundColor = System.Drawing.Color.WhiteSmoke
        Me.dgv_kaiguanlist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_kaiguanlist.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.id_kaiguan, Me.controlboxname_kaiguan, Me.state_kaiguan, Me.starttime_kaiguan, Me.endtime_kaiguan})
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle6.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_kaiguanlist.DefaultCellStyle = DataGridViewCellStyle6
        Me.dgv_kaiguanlist.Location = New System.Drawing.Point(237, 105)
        Me.dgv_kaiguanlist.Name = "dgv_kaiguanlist"
        Me.dgv_kaiguanlist.ReadOnly = True
        Me.dgv_kaiguanlist.RowHeadersVisible = False
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_kaiguanlist.RowsDefaultCellStyle = DataGridViewCellStyle7
        Me.dgv_kaiguanlist.RowTemplate.Height = 23
        Me.dgv_kaiguanlist.Size = New System.Drawing.Size(648, 441)
        Me.dgv_kaiguanlist.TabIndex = 210
        '
        'bt_excel_kaiguan
        '
        Me.bt_excel_kaiguan.Location = New System.Drawing.Point(698, 67)
        Me.bt_excel_kaiguan.Name = "bt_excel_kaiguan"
        Me.bt_excel_kaiguan.Size = New System.Drawing.Size(90, 23)
        Me.bt_excel_kaiguan.TabIndex = 209
        Me.bt_excel_kaiguan.Text = "运行统计报表"
        Me.bt_excel_kaiguan.UseVisualStyleBackColor = True
        '
        'bt_clear_kaiguan
        '
        Me.bt_clear_kaiguan.Location = New System.Drawing.Point(540, 67)
        Me.bt_clear_kaiguan.Name = "bt_clear_kaiguan"
        Me.bt_clear_kaiguan.Size = New System.Drawing.Size(90, 23)
        Me.bt_clear_kaiguan.TabIndex = 208
        Me.bt_clear_kaiguan.Text = "清空"
        Me.bt_clear_kaiguan.UseVisualStyleBackColor = True
        '
        'bt_stop_kaiguan
        '
        Me.bt_stop_kaiguan.Location = New System.Drawing.Point(698, 14)
        Me.bt_stop_kaiguan.Name = "bt_stop_kaiguan"
        Me.bt_stop_kaiguan.Size = New System.Drawing.Size(90, 23)
        Me.bt_stop_kaiguan.TabIndex = 207
        Me.bt_stop_kaiguan.Text = "停止"
        Me.bt_stop_kaiguan.UseVisualStyleBackColor = True
        '
        'bt_find_kaiguan
        '
        Me.bt_find_kaiguan.Location = New System.Drawing.Point(540, 14)
        Me.bt_find_kaiguan.Name = "bt_find_kaiguan"
        Me.bt_find_kaiguan.Size = New System.Drawing.Size(90, 23)
        Me.bt_find_kaiguan.TabIndex = 206
        Me.bt_find_kaiguan.Text = "查询"
        Me.bt_find_kaiguan.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Location = New System.Drawing.Point(235, 71)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(65, 12)
        Me.Label5.TabIndex = 204
        Me.Label5.Text = "结束日期："
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Location = New System.Drawing.Point(235, 20)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(65, 12)
        Me.Label6.TabIndex = 203
        Me.Label6.Text = "开始日期："
        '
        'dtp_starttime_kaiguan
        '
        Me.dtp_starttime_kaiguan.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_starttime_kaiguan.Location = New System.Drawing.Point(306, 16)
        Me.dtp_starttime_kaiguan.Name = "dtp_starttime_kaiguan"
        Me.dtp_starttime_kaiguan.Size = New System.Drawing.Size(149, 21)
        Me.dtp_starttime_kaiguan.TabIndex = 200
        '
        'dtp_endtime_kaiguan
        '
        Me.dtp_endtime_kaiguan.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_endtime_kaiguan.Location = New System.Drawing.Point(306, 67)
        Me.dtp_endtime_kaiguan.Name = "dtp_endtime_kaiguan"
        Me.dtp_endtime_kaiguan.Size = New System.Drawing.Size(149, 21)
        Me.dtp_endtime_kaiguan.TabIndex = 201
        '
        'tv_boxlist_kaiguan
        '
        Me.tv_boxlist_kaiguan.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.tv_boxlist_kaiguan.CheckBoxes = True
        Me.tv_boxlist_kaiguan.Location = New System.Drawing.Point(8, 6)
        Me.tv_boxlist_kaiguan.Name = "tv_boxlist_kaiguan"
        Me.tv_boxlist_kaiguan.Size = New System.Drawing.Size(221, 540)
        Me.tv_boxlist_kaiguan.TabIndex = 199
        '
        'BackgroundWorker_find
        '
        Me.BackgroundWorker_find.WorkerReportsProgress = True
        Me.BackgroundWorker_find.WorkerSupportsCancellation = True
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ProgressBar})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 584)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(899, 22)
        Me.StatusStrip1.TabIndex = 1
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ProgressBar
        '
        Me.ProgressBar.Name = "ProgressBar"
        Me.ProgressBar.Size = New System.Drawing.Size(100, 16)
        Me.ProgressBar.Visible = False
        '
        'id
        '
        Me.id.FillWeight = 60.0!
        Me.id.HeaderText = "编号"
        Me.id.Name = "id"
        Me.id.ReadOnly = True
        Me.id.Width = 60
        '
        'box_name
        '
        Me.box_name.HeaderText = "网关"
        Me.box_name.Name = "box_name"
        Me.box_name.ReadOnly = True
        Me.box_name.Width = 110
        '
        'huilu
        '
        Me.huilu.HeaderText = "回路"
        Me.huilu.Name = "huilu"
        Me.huilu.ReadOnly = True
        Me.huilu.Width = 60
        '
        'xiangwei
        '
        Me.xiangwei.HeaderText = "相位"
        Me.xiangwei.Name = "xiangwei"
        Me.xiangwei.ReadOnly = True
        Me.xiangwei.Width = 60
        '
        'presure
        '
        Me.presure.HeaderText = "电压(V)"
        Me.presure.Name = "presure"
        Me.presure.ReadOnly = True
        Me.presure.Width = 80
        '
        'current
        '
        Me.current.HeaderText = "电流(A)"
        Me.current.Name = "current"
        Me.current.ReadOnly = True
        Me.current.Width = 80
        '
        'power
        '
        Me.power.HeaderText = "功率(KW)"
        Me.power.Name = "power"
        Me.power.ReadOnly = True
        Me.power.Width = 80
        '
        'power_yinshu
        '
        Me.power_yinshu.HeaderText = "功率因数"
        Me.power_yinshu.Name = "power_yinshu"
        Me.power_yinshu.ReadOnly = True
        Me.power_yinshu.Width = 95
        '
        'createtime
        '
        Me.createtime.HeaderText = "时间"
        Me.createtime.Name = "createtime"
        Me.createtime.ReadOnly = True
        Me.createtime.Width = 150
        '
        'id_state
        '
        Me.id_state.HeaderText = "编号"
        Me.id_state.Name = "id_state"
        Me.id_state.ReadOnly = True
        '
        'controlboxname_state
        '
        Me.controlboxname_state.HeaderText = "网关名称"
        Me.controlboxname_state.Name = "controlboxname_state"
        Me.controlboxname_state.ReadOnly = True
        Me.controlboxname_state.Width = 150
        '
        'state_kind
        '
        Me.state_kind.HeaderText = "状态类型"
        Me.state_kind.Name = "state_kind"
        Me.state_kind.ReadOnly = True
        '
        'state
        '
        Me.state.HeaderText = "状态"
        Me.state.Name = "state"
        Me.state.ReadOnly = True
        Me.state.Width = 250
        '
        'time_state_start
        '
        Me.time_state_start.HeaderText = "开始时间"
        Me.time_state_start.Name = "time_state_start"
        Me.time_state_start.ReadOnly = True
        Me.time_state_start.Width = 180
        '
        'time_state_end
        '
        Me.time_state_end.HeaderText = "结束时间"
        Me.time_state_end.Name = "time_state_end"
        Me.time_state_end.ReadOnly = True
        Me.time_state_end.Width = 180
        '
        'id_kaiguan
        '
        Me.id_kaiguan.HeaderText = "编号"
        Me.id_kaiguan.Name = "id_kaiguan"
        Me.id_kaiguan.ReadOnly = True
        '
        'controlboxname_kaiguan
        '
        Me.controlboxname_kaiguan.HeaderText = "网关名称"
        Me.controlboxname_kaiguan.Name = "controlboxname_kaiguan"
        Me.controlboxname_kaiguan.ReadOnly = True
        Me.controlboxname_kaiguan.Width = 150
        '
        'state_kaiguan
        '
        Me.state_kaiguan.HeaderText = "状态"
        Me.state_kaiguan.Name = "state_kaiguan"
        Me.state_kaiguan.ReadOnly = True
        Me.state_kaiguan.Width = 250
        '
        'starttime_kaiguan
        '
        Me.starttime_kaiguan.HeaderText = "开始时间"
        Me.starttime_kaiguan.Name = "starttime_kaiguan"
        Me.starttime_kaiguan.ReadOnly = True
        Me.starttime_kaiguan.Width = 180
        '
        'endtime_kaiguan
        '
        Me.endtime_kaiguan.HeaderText = "结束时间"
        Me.endtime_kaiguan.Name = "endtime_kaiguan"
        Me.endtime_kaiguan.ReadOnly = True
        Me.endtime_kaiguan.Width = 180
        '
        '主控箱数据
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(899, 606)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "主控箱数据"
        Me.Text = "网关数据"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        CType(Me.dgv_boxinf, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        CType(Me.dgv_statelist, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        CType(Me.dgv_kaiguanlist, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents clear_text As System.Windows.Forms.Button
    Friend WithEvents tv_yaoce_controlbox As System.Windows.Forms.TreeView
    Friend WithEvents bt_stop As System.Windows.Forms.Button
    Friend WithEvents bt_yaoce As System.Windows.Forms.Button
    Friend WithEvents dgv_boxinf As System.Windows.Forms.DataGridView
    Friend WithEvents dtp_date_start As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtp_date_end As System.Windows.Forms.DateTimePicker
    Friend WithEvents Date_end_string As System.Windows.Forms.Label
    Friend WithEvents Date_start_string As System.Windows.Forms.Label
    Friend WithEvents BackgroundWorker_find As System.ComponentModel.BackgroundWorker
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ProgressBar As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents bt_static_excel As System.Windows.Forms.Button
    Friend WithEvents tv_boxlist_state As System.Windows.Forms.TreeView
    Friend WithEvents cb_state_list As System.Windows.Forms.ComboBox
    Friend WithEvents dtp_starttime_state As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtp_endtime_state As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents bt_excel_state As System.Windows.Forms.Button
    Friend WithEvents bt_clear_state As System.Windows.Forms.Button
    Friend WithEvents bt_stop_state As System.Windows.Forms.Button
    Friend WithEvents bt_find_state As System.Windows.Forms.Button
    Friend WithEvents dgv_statelist As System.Windows.Forms.DataGridView
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents dgv_kaiguanlist As System.Windows.Forms.DataGridView
    Friend WithEvents bt_excel_kaiguan As System.Windows.Forms.Button
    Friend WithEvents bt_clear_kaiguan As System.Windows.Forms.Button
    Friend WithEvents bt_stop_kaiguan As System.Windows.Forms.Button
    Friend WithEvents bt_find_kaiguan As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents dtp_starttime_kaiguan As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtp_endtime_kaiguan As System.Windows.Forms.DateTimePicker
    Friend WithEvents tv_boxlist_kaiguan As System.Windows.Forms.TreeView
    Friend WithEvents id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents box_name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents huilu As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents xiangwei As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents presure As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents current As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents power As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents power_yinshu As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents createtime As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents id_state As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents controlboxname_state As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents state_kind As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents state As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents time_state_start As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents time_state_end As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents id_kaiguan As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents controlboxname_kaiguan As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents state_kaiguan As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents starttime_kaiguan As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents endtime_kaiguan As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
