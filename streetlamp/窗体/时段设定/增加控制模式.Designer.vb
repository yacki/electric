<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 增加控制模式
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(增加控制模式))
        Me.div_time_groupbox1 = New System.Windows.Forms.GroupBox
        Me.Add_divlevel = New System.Windows.Forms.Button
        Me.time_start_string1 = New System.Windows.Forms.Label
        Me.second_beg = New System.Windows.Forms.ComboBox
        Me.min_beg = New System.Windows.Forms.ComboBox
        Me.hour_beg = New System.Windows.Forms.ComboBox
        Me.mod_value = New System.Windows.Forms.ComboBox
        Me.mod1_string = New System.Windows.Forms.Label
        Me.hour1_beg_string = New System.Windows.Forms.Label
        Me.min1_beg_string = New System.Windows.Forms.Label
        Me.second1_beg_string = New System.Windows.Forms.Label
        Me.Add_modlist = New System.Windows.Forms.DataGridView
        Me.id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.start_hour = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.start_min = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.start_second = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.moduel = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Road_level = New streetlamp.road_level
        Me.DivtimeBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Div_timeTableAdapter = New streetlamp.road_levelTableAdapters.div_timeTableAdapter
        Me.input_hms = New System.Windows.Forms.Button
        Me.gonglv1 = New System.Windows.Forms.ComboBox
        Me.mod_level_choose_string = New System.Windows.Forms.Label
        Me.mod_level_choose = New System.Windows.Forms.TextBox
        Me.div_time_groupbox1.SuspendLayout()
        CType(Me.Add_modlist, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Road_level, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DivtimeBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'div_time_groupbox1
        '
        Me.div_time_groupbox1.Controls.Add(Me.Add_divlevel)
        Me.div_time_groupbox1.Controls.Add(Me.time_start_string1)
        Me.div_time_groupbox1.Controls.Add(Me.second_beg)
        Me.div_time_groupbox1.Controls.Add(Me.min_beg)
        Me.div_time_groupbox1.Controls.Add(Me.hour_beg)
        Me.div_time_groupbox1.Controls.Add(Me.mod_value)
        Me.div_time_groupbox1.Controls.Add(Me.mod1_string)
        Me.div_time_groupbox1.Controls.Add(Me.hour1_beg_string)
        Me.div_time_groupbox1.Controls.Add(Me.min1_beg_string)
        Me.div_time_groupbox1.Controls.Add(Me.second1_beg_string)
        Me.div_time_groupbox1.Controls.Add(Me.Add_modlist)
        Me.div_time_groupbox1.Location = New System.Drawing.Point(12, 12)
        Me.div_time_groupbox1.Name = "div_time_groupbox1"
        Me.div_time_groupbox1.Size = New System.Drawing.Size(695, 333)
        Me.div_time_groupbox1.TabIndex = 58
        Me.div_time_groupbox1.TabStop = False
        '
        'Add_divlevel
        '
        Me.Add_divlevel.Location = New System.Drawing.Point(592, 33)
        Me.Add_divlevel.Name = "Add_divlevel"
        Me.Add_divlevel.Size = New System.Drawing.Size(75, 23)
        Me.Add_divlevel.TabIndex = 87
        Me.Add_divlevel.Text = "增加时段"
        Me.Add_divlevel.UseVisualStyleBackColor = True
        '
        'time_start_string1
        '
        Me.time_start_string1.AutoSize = True
        Me.time_start_string1.Location = New System.Drawing.Point(13, 38)
        Me.time_start_string1.Name = "time_start_string1"
        Me.time_start_string1.Size = New System.Drawing.Size(77, 14)
        Me.time_start_string1.TabIndex = 86
        Me.time_start_string1.Text = "开始时间："
        '
        'second_beg
        '
        Me.second_beg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.second_beg.FormattingEnabled = True
        Me.second_beg.Items.AddRange(New Object() {"00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50", "51", "52", "53", "54", "55", "56", "57", "58", "59"})
        Me.second_beg.Location = New System.Drawing.Point(286, 33)
        Me.second_beg.Name = "second_beg"
        Me.second_beg.Size = New System.Drawing.Size(60, 22)
        Me.second_beg.TabIndex = 85
        '
        'min_beg
        '
        Me.min_beg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.min_beg.FormattingEnabled = True
        Me.min_beg.Items.AddRange(New Object() {"00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50", "51", "52", "53", "54", "55", "56", "57", "58", "59"})
        Me.min_beg.Location = New System.Drawing.Point(192, 33)
        Me.min_beg.Name = "min_beg"
        Me.min_beg.Size = New System.Drawing.Size(60, 22)
        Me.min_beg.TabIndex = 84
        '
        'hour_beg
        '
        Me.hour_beg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.hour_beg.FormattingEnabled = True
        Me.hour_beg.Items.AddRange(New Object() {"00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23"})
        Me.hour_beg.Location = New System.Drawing.Point(95, 33)
        Me.hour_beg.Name = "hour_beg"
        Me.hour_beg.Size = New System.Drawing.Size(61, 22)
        Me.hour_beg.TabIndex = 83
        '
        'mod_value
        '
        Me.mod_value.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.mod_value.FormattingEnabled = True
        Me.mod_value.Items.AddRange(New Object() {"类型全开", "类型全闭", "类型奇开", "类型偶开", "回路开", "回路关"})
        Me.mod_value.Location = New System.Drawing.Point(457, 33)
        Me.mod_value.Name = "mod_value"
        Me.mod_value.Size = New System.Drawing.Size(114, 22)
        Me.mod_value.TabIndex = 78
        '
        'mod1_string
        '
        Me.mod1_string.AutoSize = True
        Me.mod1_string.Location = New System.Drawing.Point(402, 38)
        Me.mod1_string.Name = "mod1_string"
        Me.mod1_string.Size = New System.Drawing.Size(49, 14)
        Me.mod1_string.TabIndex = 79
        Me.mod1_string.Text = "模式："
        '
        'hour1_beg_string
        '
        Me.hour1_beg_string.AutoSize = True
        Me.hour1_beg_string.Location = New System.Drawing.Point(164, 36)
        Me.hour1_beg_string.Name = "hour1_beg_string"
        Me.hour1_beg_string.Size = New System.Drawing.Size(21, 14)
        Me.hour1_beg_string.TabIndex = 80
        Me.hour1_beg_string.Text = "时"
        '
        'min1_beg_string
        '
        Me.min1_beg_string.AutoSize = True
        Me.min1_beg_string.Location = New System.Drawing.Point(259, 36)
        Me.min1_beg_string.Name = "min1_beg_string"
        Me.min1_beg_string.Size = New System.Drawing.Size(21, 14)
        Me.min1_beg_string.TabIndex = 81
        Me.min1_beg_string.Text = "分"
        '
        'second1_beg_string
        '
        Me.second1_beg_string.AutoSize = True
        Me.second1_beg_string.Location = New System.Drawing.Point(353, 38)
        Me.second1_beg_string.Name = "second1_beg_string"
        Me.second1_beg_string.Size = New System.Drawing.Size(21, 14)
        Me.second1_beg_string.TabIndex = 82
        Me.second1_beg_string.Text = "秒"
        '
        'Add_modlist
        '
        Me.Add_modlist.AllowUserToAddRows = False
        Me.Add_modlist.BackgroundColor = System.Drawing.Color.WhiteSmoke
        Me.Add_modlist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Add_modlist.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.id, Me.start_hour, Me.start_min, Me.start_second, Me.moduel})
        Me.Add_modlist.Location = New System.Drawing.Point(6, 76)
        Me.Add_modlist.Name = "Add_modlist"
        Me.Add_modlist.RowTemplate.Height = 23
        Me.Add_modlist.Size = New System.Drawing.Size(681, 251)
        Me.Add_modlist.TabIndex = 67
        '
        'id
        '
        Me.id.HeaderText = "编号"
        Me.id.Name = "id"
        '
        'start_hour
        '
        Me.start_hour.HeaderText = "小时"
        Me.start_hour.Name = "start_hour"
        '
        'start_min
        '
        Me.start_min.HeaderText = "分钟"
        Me.start_min.Name = "start_min"
        '
        'start_second
        '
        Me.start_second.HeaderText = "秒"
        Me.start_second.Name = "start_second"
        '
        'moduel
        '
        Me.moduel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.moduel.HeaderText = "模式"
        Me.moduel.Name = "moduel"
        '
        'Road_level
        '
        Me.Road_level.DataSetName = "road_level"
        Me.Road_level.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'DivtimeBindingSource
        '
        Me.DivtimeBindingSource.DataMember = "div_time"
        Me.DivtimeBindingSource.DataSource = Me.Road_level
        '
        'Div_timeTableAdapter
        '
        Me.Div_timeTableAdapter.ClearBeforeFill = True
        '
        'input_hms
        '
        Me.input_hms.Location = New System.Drawing.Point(333, 361)
        Me.input_hms.Name = "input_hms"
        Me.input_hms.Size = New System.Drawing.Size(102, 27)
        Me.input_hms.TabIndex = 70
        Me.input_hms.Text = "输入控制模式"
        Me.input_hms.UseVisualStyleBackColor = True
        '
        'gonglv1
        '
        Me.gonglv1.FormattingEnabled = True
        Me.gonglv1.Items.AddRange(New Object() {"全功率", "半功率"})
        Me.gonglv1.Location = New System.Drawing.Point(604, 361)
        Me.gonglv1.Name = "gonglv1"
        Me.gonglv1.Size = New System.Drawing.Size(57, 22)
        Me.gonglv1.TabIndex = 67
        Me.gonglv1.Text = "全功率"
        Me.gonglv1.Visible = False
        '
        'mod_level_choose_string
        '
        Me.mod_level_choose_string.AutoSize = True
        Me.mod_level_choose_string.Location = New System.Drawing.Point(24, 367)
        Me.mod_level_choose_string.Name = "mod_level_choose_string"
        Me.mod_level_choose_string.Size = New System.Drawing.Size(77, 14)
        Me.mod_level_choose_string.TabIndex = 71
        Me.mod_level_choose_string.Text = "模式名称："
        '
        'mod_level_choose
        '
        Me.mod_level_choose.Location = New System.Drawing.Point(107, 364)
        Me.mod_level_choose.Name = "mod_level_choose"
        Me.mod_level_choose.Size = New System.Drawing.Size(100, 23)
        Me.mod_level_choose.TabIndex = 73
        '
        '增加控制模式
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(716, 429)
        Me.Controls.Add(Me.mod_level_choose)
        Me.Controls.Add(Me.mod_level_choose_string)
        Me.Controls.Add(Me.input_hms)
        Me.Controls.Add(Me.gonglv1)
        Me.Controls.Add(Me.div_time_groupbox1)
        Me.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "增加控制模式"
        Me.Text = "编辑控制模式"
        Me.div_time_groupbox1.ResumeLayout(False)
        Me.div_time_groupbox1.PerformLayout()
        CType(Me.Add_modlist, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Road_level, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DivtimeBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents div_time_groupbox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Road_level As streetlamp.road_level
    Friend WithEvents DivtimeBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents Div_timeTableAdapter As streetlamp.road_levelTableAdapters.div_timeTableAdapter
    Friend WithEvents Add_modlist As System.Windows.Forms.DataGridView
    Friend WithEvents id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents start_hour As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents start_min As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents start_second As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents moduel As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents input_hms As System.Windows.Forms.Button
    Friend WithEvents gonglv1 As System.Windows.Forms.ComboBox
    Friend WithEvents mod_level_choose_string As System.Windows.Forms.Label
    Friend WithEvents Add_divlevel As System.Windows.Forms.Button
    Friend WithEvents time_start_string1 As System.Windows.Forms.Label
    Friend WithEvents second_beg As System.Windows.Forms.ComboBox
    Friend WithEvents min_beg As System.Windows.Forms.ComboBox
    Friend WithEvents hour_beg As System.Windows.Forms.ComboBox
    Friend WithEvents mod_value As System.Windows.Forms.ComboBox
    Friend WithEvents mod1_string As System.Windows.Forms.Label
    Friend WithEvents hour1_beg_string As System.Windows.Forms.Label
    Friend WithEvents min1_beg_string As System.Windows.Forms.Label
    Friend WithEvents second1_beg_string As System.Windows.Forms.Label
    Friend WithEvents mod_level_choose As System.Windows.Forms.TextBox
End Class
