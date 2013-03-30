<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 电控箱数据统计
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(电控箱数据统计))
        Me.bt_static_excel = New System.Windows.Forms.Button
        Me.bt_clear = New System.Windows.Forms.Button
        Me.rtb_doingnow_text = New System.Windows.Forms.RichTextBox
        Me.dtp_date_end = New System.Windows.Forms.DateTimePicker
        Me.dtp_date_start = New System.Windows.Forms.DateTimePicker
        Me.Date_end_string = New System.Windows.Forms.Label
        Me.Date_start_string = New System.Windows.Forms.Label
        Me.bt_find_record = New System.Windows.Forms.Button
        Me.cb_control_box_name = New System.Windows.Forms.ComboBox
        Me.BackgroundWorker_on_off = New System.ComponentModel.BackgroundWorker
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.record_num = New System.Windows.Forms.ToolStripStatusLabel
        Me.ProgressBar = New System.Windows.Forms.ToolStripProgressBar
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.bt_stopcheck = New System.Windows.Forms.Button
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.rb_city_control = New System.Windows.Forms.RadioButton
        Me.rb_area_control = New System.Windows.Forms.RadioButton
        Me.rb_street_control = New System.Windows.Forms.RadioButton
        Me.cb_state_list = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.cb_city_name = New System.Windows.Forms.ComboBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.cb_street_name = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.cb_area_name = New System.Windows.Forms.ComboBox
        Me.rb_control_box_name_control = New System.Windows.Forms.RadioButton
        Me.pro_box_string = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.rb_total_state = New System.Windows.Forms.RadioButton
        Me.rb_communicationcheck = New System.Windows.Forms.RadioButton
        Me.rb_sanyao_statecheck = New System.Windows.Forms.RadioButton
        Me.rb_kaiguancheck = New System.Windows.Forms.RadioButton
        Me.rb_sanyao_datacheck = New System.Windows.Forms.RadioButton
        Me.rb_powercheck = New System.Windows.Forms.RadioButton
        Me.lb_lamptype_id = New System.Windows.Forms.Label
        Me.lb_lamp_id_start = New System.Windows.Forms.Label
        Me.StatusStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'bt_static_excel
        '
        Me.bt_static_excel.Location = New System.Drawing.Point(154, 500)
        Me.bt_static_excel.Name = "bt_static_excel"
        Me.bt_static_excel.Size = New System.Drawing.Size(90, 23)
        Me.bt_static_excel.TabIndex = 149
        Me.bt_static_excel.Text = "运行统计报表"
        Me.bt_static_excel.UseVisualStyleBackColor = True
        '
        'bt_clear
        '
        Me.bt_clear.Location = New System.Drawing.Point(11, 500)
        Me.bt_clear.Name = "bt_clear"
        Me.bt_clear.Size = New System.Drawing.Size(90, 23)
        Me.bt_clear.TabIndex = 148
        Me.bt_clear.Text = "清空"
        Me.bt_clear.UseVisualStyleBackColor = True
        '
        'rtb_doingnow_text
        '
        Me.rtb_doingnow_text.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rtb_doingnow_text.HideSelection = False
        Me.rtb_doingnow_text.Location = New System.Drawing.Point(283, 10)
        Me.rtb_doingnow_text.Name = "rtb_doingnow_text"
        Me.rtb_doingnow_text.Size = New System.Drawing.Size(592, 541)
        Me.rtb_doingnow_text.TabIndex = 147
        Me.rtb_doingnow_text.Text = ""
        '
        'dtp_date_end
        '
        Me.dtp_date_end.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_date_end.Location = New System.Drawing.Point(122, 246)
        Me.dtp_date_end.Name = "dtp_date_end"
        Me.dtp_date_end.Size = New System.Drawing.Size(106, 21)
        Me.dtp_date_end.TabIndex = 146
        '
        'dtp_date_start
        '
        Me.dtp_date_start.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_date_start.Location = New System.Drawing.Point(122, 219)
        Me.dtp_date_start.Name = "dtp_date_start"
        Me.dtp_date_start.Size = New System.Drawing.Size(106, 21)
        Me.dtp_date_start.TabIndex = 145
        '
        'Date_end_string
        '
        Me.Date_end_string.AutoSize = True
        Me.Date_end_string.BackColor = System.Drawing.Color.Transparent
        Me.Date_end_string.Location = New System.Drawing.Point(29, 250)
        Me.Date_end_string.Name = "Date_end_string"
        Me.Date_end_string.Size = New System.Drawing.Size(65, 12)
        Me.Date_end_string.TabIndex = 144
        Me.Date_end_string.Text = "结束日期："
        '
        'Date_start_string
        '
        Me.Date_start_string.AutoSize = True
        Me.Date_start_string.BackColor = System.Drawing.Color.Transparent
        Me.Date_start_string.Location = New System.Drawing.Point(31, 219)
        Me.Date_start_string.Name = "Date_start_string"
        Me.Date_start_string.Size = New System.Drawing.Size(65, 12)
        Me.Date_start_string.TabIndex = 143
        Me.Date_start_string.Text = "开始日期："
        '
        'bt_find_record
        '
        Me.bt_find_record.Location = New System.Drawing.Point(11, 471)
        Me.bt_find_record.Name = "bt_find_record"
        Me.bt_find_record.Size = New System.Drawing.Size(90, 23)
        Me.bt_find_record.TabIndex = 142
        Me.bt_find_record.Text = "查询"
        Me.bt_find_record.UseVisualStyleBackColor = True
        '
        'cb_control_box_name
        '
        Me.cb_control_box_name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_control_box_name.FormattingEnabled = True
        Me.cb_control_box_name.Location = New System.Drawing.Point(123, 178)
        Me.cb_control_box_name.Name = "cb_control_box_name"
        Me.cb_control_box_name.Size = New System.Drawing.Size(102, 20)
        Me.cb_control_box_name.TabIndex = 139
        '
        'BackgroundWorker_on_off
        '
        Me.BackgroundWorker_on_off.WorkerReportsProgress = True
        Me.BackgroundWorker_on_off.WorkerSupportsCancellation = True
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.record_num, Me.ProgressBar})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 565)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(885, 22)
        Me.StatusStrip1.TabIndex = 150
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'record_num
        '
        Me.record_num.Name = "record_num"
        Me.record_num.Size = New System.Drawing.Size(89, 17)
        Me.record_num.Text = "主控箱数据统计"
        '
        'ProgressBar
        '
        Me.ProgressBar.Name = "ProgressBar"
        Me.ProgressBar.Size = New System.Drawing.Size(200, 16)
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.bt_stopcheck)
        Me.GroupBox1.Controls.Add(Me.GroupBox3)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.lb_lamptype_id)
        Me.GroupBox1.Controls.Add(Me.lb_lamp_id_start)
        Me.GroupBox1.Controls.Add(Me.bt_clear)
        Me.GroupBox1.Controls.Add(Me.bt_static_excel)
        Me.GroupBox1.Controls.Add(Me.bt_find_record)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 10)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(255, 541)
        Me.GroupBox1.TabIndex = 151
        Me.GroupBox1.TabStop = False
        '
        'bt_stopcheck
        '
        Me.bt_stopcheck.Location = New System.Drawing.Point(154, 471)
        Me.bt_stopcheck.Name = "bt_stopcheck"
        Me.bt_stopcheck.Size = New System.Drawing.Size(90, 23)
        Me.bt_stopcheck.TabIndex = 177
        Me.bt_stopcheck.Text = "停止查询"
        Me.bt_stopcheck.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.rb_city_control)
        Me.GroupBox3.Controls.Add(Me.rb_area_control)
        Me.GroupBox3.Controls.Add(Me.rb_street_control)
        Me.GroupBox3.Controls.Add(Me.cb_state_list)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.cb_control_box_name)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Controls.Add(Me.cb_city_name)
        Me.GroupBox3.Controls.Add(Me.dtp_date_start)
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Controls.Add(Me.dtp_date_end)
        Me.GroupBox3.Controls.Add(Me.Date_end_string)
        Me.GroupBox3.Controls.Add(Me.Date_start_string)
        Me.GroupBox3.Controls.Add(Me.cb_street_name)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Controls.Add(Me.cb_area_name)
        Me.GroupBox3.Controls.Add(Me.rb_control_box_name_control)
        Me.GroupBox3.Controls.Add(Me.pro_box_string)
        Me.GroupBox3.Location = New System.Drawing.Point(2, 113)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(247, 327)
        Me.GroupBox3.TabIndex = 176
        Me.GroupBox3.TabStop = False
        '
        'rb_city_control
        '
        Me.rb_city_control.AutoSize = True
        Me.rb_city_control.BackColor = System.Drawing.Color.Transparent
        Me.rb_city_control.Location = New System.Drawing.Point(14, 20)
        Me.rb_city_control.Name = "rb_city_control"
        Me.rb_city_control.Size = New System.Drawing.Size(71, 16)
        Me.rb_city_control.TabIndex = 145
        Me.rb_city_control.Text = "城市名称"
        Me.rb_city_control.UseVisualStyleBackColor = False
        '
        'rb_area_control
        '
        Me.rb_area_control.AutoSize = True
        Me.rb_area_control.BackColor = System.Drawing.Color.Transparent
        Me.rb_area_control.Location = New System.Drawing.Point(122, 20)
        Me.rb_area_control.Name = "rb_area_control"
        Me.rb_area_control.Size = New System.Drawing.Size(71, 16)
        Me.rb_area_control.TabIndex = 144
        Me.rb_area_control.Text = "区域名称"
        Me.rb_area_control.UseVisualStyleBackColor = False
        '
        'rb_street_control
        '
        Me.rb_street_control.AutoSize = True
        Me.rb_street_control.BackColor = System.Drawing.Color.Transparent
        Me.rb_street_control.Location = New System.Drawing.Point(15, 48)
        Me.rb_street_control.Name = "rb_street_control"
        Me.rb_street_control.Size = New System.Drawing.Size(71, 16)
        Me.rb_street_control.TabIndex = 143
        Me.rb_street_control.Text = "街道名称"
        Me.rb_street_control.UseVisualStyleBackColor = False
        '
        'cb_state_list
        '
        Me.cb_state_list.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_state_list.Enabled = False
        Me.cb_state_list.FormattingEnabled = True
        Me.cb_state_list.Items.AddRange(New Object() {"故障", "正常", "全部"})
        Me.cb_state_list.Location = New System.Drawing.Point(122, 273)
        Me.cb_state_list.Name = "cb_state_list"
        Me.cb_state_list.Size = New System.Drawing.Size(106, 20)
        Me.cb_state_list.TabIndex = 151
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(29, 276)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 12)
        Me.Label1.TabIndex = 150
        Me.Label1.Text = "数据状态："
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Location = New System.Drawing.Point(29, 94)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(65, 12)
        Me.Label5.TabIndex = 140
        Me.Label5.Text = "城市名称："
        '
        'cb_city_name
        '
        Me.cb_city_name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_city_name.FormattingEnabled = True
        Me.cb_city_name.Location = New System.Drawing.Point(123, 91)
        Me.cb_city_name.Name = "cb_city_name"
        Me.cb_city_name.Size = New System.Drawing.Size(102, 20)
        Me.cb_city_name.TabIndex = 141
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(29, 157)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(65, 12)
        Me.Label4.TabIndex = 138
        Me.Label4.Text = "街道名称："
        '
        'cb_street_name
        '
        Me.cb_street_name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_street_name.FormattingEnabled = True
        Me.cb_street_name.Location = New System.Drawing.Point(123, 150)
        Me.cb_street_name.Name = "cb_street_name"
        Me.cb_street_name.Size = New System.Drawing.Size(102, 20)
        Me.cb_street_name.TabIndex = 139
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(29, 123)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 12)
        Me.Label3.TabIndex = 136
        Me.Label3.Text = "区域名称："
        '
        'cb_area_name
        '
        Me.cb_area_name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_area_name.FormattingEnabled = True
        Me.cb_area_name.Location = New System.Drawing.Point(122, 121)
        Me.cb_area_name.Name = "cb_area_name"
        Me.cb_area_name.Size = New System.Drawing.Size(102, 20)
        Me.cb_area_name.TabIndex = 137
        '
        'rb_control_box_name_control
        '
        Me.rb_control_box_name_control.AutoSize = True
        Me.rb_control_box_name_control.BackColor = System.Drawing.Color.Transparent
        Me.rb_control_box_name_control.Checked = True
        Me.rb_control_box_name_control.Location = New System.Drawing.Point(123, 48)
        Me.rb_control_box_name_control.Name = "rb_control_box_name_control"
        Me.rb_control_box_name_control.Size = New System.Drawing.Size(83, 16)
        Me.rb_control_box_name_control.TabIndex = 134
        Me.rb_control_box_name_control.TabStop = True
        Me.rb_control_box_name_control.Text = "主控箱名称"
        Me.rb_control_box_name_control.UseVisualStyleBackColor = False
        '
        'pro_box_string
        '
        Me.pro_box_string.AutoSize = True
        Me.pro_box_string.BackColor = System.Drawing.Color.Transparent
        Me.pro_box_string.Location = New System.Drawing.Point(17, 186)
        Me.pro_box_string.Name = "pro_box_string"
        Me.pro_box_string.Size = New System.Drawing.Size(77, 12)
        Me.pro_box_string.TabIndex = 132
        Me.pro_box_string.Text = "主控箱名称："
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.rb_total_state)
        Me.GroupBox2.Controls.Add(Me.rb_communicationcheck)
        Me.GroupBox2.Controls.Add(Me.rb_sanyao_statecheck)
        Me.GroupBox2.Controls.Add(Me.rb_kaiguancheck)
        Me.GroupBox2.Controls.Add(Me.rb_sanyao_datacheck)
        Me.GroupBox2.Controls.Add(Me.rb_powercheck)
        Me.GroupBox2.Location = New System.Drawing.Point(2, 2)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(249, 105)
        Me.GroupBox2.TabIndex = 175
        Me.GroupBox2.TabStop = False
        '
        'rb_total_state
        '
        Me.rb_total_state.AutoSize = True
        Me.rb_total_state.Location = New System.Drawing.Point(123, 81)
        Me.rb_total_state.Name = "rb_total_state"
        Me.rb_total_state.Size = New System.Drawing.Size(119, 16)
        Me.rb_total_state.TabIndex = 175
        Me.rb_total_state.Text = "综合报警数据查询"
        Me.rb_total_state.UseVisualStyleBackColor = True
        Me.rb_total_state.Visible = False
        '
        'rb_communicationcheck
        '
        Me.rb_communicationcheck.AutoSize = True
        Me.rb_communicationcheck.Location = New System.Drawing.Point(14, 81)
        Me.rb_communicationcheck.Name = "rb_communicationcheck"
        Me.rb_communicationcheck.Size = New System.Drawing.Size(95, 16)
        Me.rb_communicationcheck.TabIndex = 171
        Me.rb_communicationcheck.Text = "通信状态查询"
        Me.rb_communicationcheck.UseVisualStyleBackColor = True
        '
        'rb_sanyao_statecheck
        '
        Me.rb_sanyao_statecheck.AutoSize = True
        Me.rb_sanyao_statecheck.Location = New System.Drawing.Point(122, 18)
        Me.rb_sanyao_statecheck.Name = "rb_sanyao_statecheck"
        Me.rb_sanyao_statecheck.Size = New System.Drawing.Size(95, 16)
        Me.rb_sanyao_statecheck.TabIndex = 174
        Me.rb_sanyao_statecheck.Text = "三遥状态查询"
        Me.rb_sanyao_statecheck.UseVisualStyleBackColor = True
        '
        'rb_kaiguancheck
        '
        Me.rb_kaiguancheck.AutoSize = True
        Me.rb_kaiguancheck.Location = New System.Drawing.Point(122, 51)
        Me.rb_kaiguancheck.Name = "rb_kaiguancheck"
        Me.rb_kaiguancheck.Size = New System.Drawing.Size(107, 16)
        Me.rb_kaiguancheck.TabIndex = 173
        Me.rb_kaiguancheck.Text = "开关量状态查询"
        Me.rb_kaiguancheck.UseVisualStyleBackColor = True
        '
        'rb_sanyao_datacheck
        '
        Me.rb_sanyao_datacheck.AutoSize = True
        Me.rb_sanyao_datacheck.Checked = True
        Me.rb_sanyao_datacheck.Location = New System.Drawing.Point(15, 18)
        Me.rb_sanyao_datacheck.Name = "rb_sanyao_datacheck"
        Me.rb_sanyao_datacheck.Size = New System.Drawing.Size(95, 16)
        Me.rb_sanyao_datacheck.TabIndex = 170
        Me.rb_sanyao_datacheck.TabStop = True
        Me.rb_sanyao_datacheck.Text = "三遥数据查询"
        Me.rb_sanyao_datacheck.UseVisualStyleBackColor = True
        '
        'rb_powercheck
        '
        Me.rb_powercheck.AutoSize = True
        Me.rb_powercheck.Location = New System.Drawing.Point(14, 51)
        Me.rb_powercheck.Name = "rb_powercheck"
        Me.rb_powercheck.Size = New System.Drawing.Size(95, 16)
        Me.rb_powercheck.TabIndex = 172
        Me.rb_powercheck.Text = "供电状态查询"
        Me.rb_powercheck.UseVisualStyleBackColor = True
        '
        'lb_lamptype_id
        '
        Me.lb_lamptype_id.AutoSize = True
        Me.lb_lamptype_id.Location = New System.Drawing.Point(172, 526)
        Me.lb_lamptype_id.Name = "lb_lamptype_id"
        Me.lb_lamptype_id.Size = New System.Drawing.Size(53, 12)
        Me.lb_lamptype_id.TabIndex = 169
        Me.lb_lamptype_id.Text = "灯的类型"
        Me.lb_lamptype_id.Visible = False
        '
        'lb_lamp_id_start
        '
        Me.lb_lamp_id_start.AutoSize = True
        Me.lb_lamp_id_start.Location = New System.Drawing.Point(9, 526)
        Me.lb_lamp_id_start.Name = "lb_lamp_id_start"
        Me.lb_lamp_id_start.Size = New System.Drawing.Size(113, 12)
        Me.lb_lamp_id_start.TabIndex = 155
        Me.lb_lamp_id_start.Text = "景观灯编号前半部分"
        Me.lb_lamp_id_start.Visible = False
        '
        '电控箱数据统计
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.streetlamp.My.Resources.Resources.bg11
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(885, 587)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.rtb_doingnow_text)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "电控箱数据统计"
        Me.Text = "主控箱状态统计"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents bt_static_excel As System.Windows.Forms.Button
    Friend WithEvents bt_clear As System.Windows.Forms.Button
    Friend WithEvents rtb_doingnow_text As System.Windows.Forms.RichTextBox
    Friend WithEvents dtp_date_end As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtp_date_start As System.Windows.Forms.DateTimePicker
    Friend WithEvents Date_end_string As System.Windows.Forms.Label
    Friend WithEvents Date_start_string As System.Windows.Forms.Label
    Friend WithEvents bt_find_record As System.Windows.Forms.Button
    Friend WithEvents cb_control_box_name As System.Windows.Forms.ComboBox
    Friend WithEvents BackgroundWorker_on_off As System.ComponentModel.BackgroundWorker
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents record_num As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ProgressBar As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lb_lamp_id_start As System.Windows.Forms.Label
    Friend WithEvents lb_lamptype_id As System.Windows.Forms.Label
    Friend WithEvents cb_state_list As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents rb_powercheck As System.Windows.Forms.RadioButton
    Friend WithEvents rb_communicationcheck As System.Windows.Forms.RadioButton
    Friend WithEvents rb_sanyao_datacheck As System.Windows.Forms.RadioButton
    Friend WithEvents rb_kaiguancheck As System.Windows.Forms.RadioButton
    Friend WithEvents rb_sanyao_statecheck As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents rb_city_control As System.Windows.Forms.RadioButton
    Friend WithEvents rb_area_control As System.Windows.Forms.RadioButton
    Friend WithEvents rb_street_control As System.Windows.Forms.RadioButton
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cb_city_name As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cb_street_name As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cb_area_name As System.Windows.Forms.ComboBox
    Friend WithEvents rb_control_box_name_control As System.Windows.Forms.RadioButton
    Friend WithEvents pro_box_string As System.Windows.Forms.Label
    Friend WithEvents rb_total_state As System.Windows.Forms.RadioButton
    Friend WithEvents bt_stopcheck As System.Windows.Forms.Button
End Class
