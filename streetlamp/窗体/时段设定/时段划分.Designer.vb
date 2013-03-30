<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 时段划分
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(时段划分))
        Me.input_hms = New System.Windows.Forms.Button
        Me.div_time_groupbox1 = New System.Windows.Forms.GroupBox
        Me.time_start_string1 = New System.Windows.Forms.Label
        Me.second1_beg = New System.Windows.Forms.ComboBox
        Me.min1_beg = New System.Windows.Forms.ComboBox
        Me.hour1_beg = New System.Windows.Forms.ComboBox
        Me.mod1 = New System.Windows.Forms.ComboBox
        Me.mod1_string = New System.Windows.Forms.Label
        Me.hour1_beg_string = New System.Windows.Forms.Label
        Me.min1_beg_string = New System.Windows.Forms.Label
        Me.second1_beg_string = New System.Windows.Forms.Label
        Me.gonglv1 = New System.Windows.Forms.ComboBox
        Me.div_time_groupbox3 = New System.Windows.Forms.GroupBox
        Me.time_start_string3 = New System.Windows.Forms.Label
        Me.second3_beg = New System.Windows.Forms.ComboBox
        Me.min3_beg = New System.Windows.Forms.ComboBox
        Me.hour3_beg = New System.Windows.Forms.ComboBox
        Me.mod3 = New System.Windows.Forms.ComboBox
        Me.mod3_string = New System.Windows.Forms.Label
        Me.hour3_beg_string = New System.Windows.Forms.Label
        Me.min3_beg_string = New System.Windows.Forms.Label
        Me.second3_beg_string = New System.Windows.Forms.Label
        Me.gonglv3 = New System.Windows.Forms.ComboBox
        Me.div_time_groupbox2 = New System.Windows.Forms.GroupBox
        Me.time_start_string2 = New System.Windows.Forms.Label
        Me.second2_beg = New System.Windows.Forms.ComboBox
        Me.min2_beg = New System.Windows.Forms.ComboBox
        Me.hour2_beg = New System.Windows.Forms.ComboBox
        Me.mod2 = New System.Windows.Forms.ComboBox
        Me.mod2_string = New System.Windows.Forms.Label
        Me.second2_beg_string = New System.Windows.Forms.Label
        Me.min2_beg_string = New System.Windows.Forms.Label
        Me.hour2_beg_string = New System.Windows.Forms.Label
        Me.gonglv2 = New System.Windows.Forms.ComboBox
        Me.div_groupbox = New System.Windows.Forms.GroupBox
        Me.mod_level_choose = New System.Windows.Forms.ComboBox
        Me.mod_level_choose_string = New System.Windows.Forms.Label
        Me.div_time_groupbox1.SuspendLayout()
        Me.div_time_groupbox3.SuspendLayout()
        Me.div_time_groupbox2.SuspendLayout()
        Me.div_groupbox.SuspendLayout()
        Me.SuspendLayout()
        '
        'input_hms
        '
        Me.input_hms.Location = New System.Drawing.Point(290, 440)
        Me.input_hms.Name = "input_hms"
        Me.input_hms.Size = New System.Drawing.Size(87, 27)
        Me.input_hms.TabIndex = 21
        Me.input_hms.Text = "输入"
        Me.input_hms.UseVisualStyleBackColor = True
        '
        'div_time_groupbox1
        '
        Me.div_time_groupbox1.Controls.Add(Me.time_start_string1)
        Me.div_time_groupbox1.Controls.Add(Me.second1_beg)
        Me.div_time_groupbox1.Controls.Add(Me.min1_beg)
        Me.div_time_groupbox1.Controls.Add(Me.hour1_beg)
        Me.div_time_groupbox1.Controls.Add(Me.mod1)
        Me.div_time_groupbox1.Controls.Add(Me.mod1_string)
        Me.div_time_groupbox1.Controls.Add(Me.hour1_beg_string)
        Me.div_time_groupbox1.Controls.Add(Me.min1_beg_string)
        Me.div_time_groupbox1.Controls.Add(Me.second1_beg_string)
        Me.div_time_groupbox1.Location = New System.Drawing.Point(15, 23)
        Me.div_time_groupbox1.Name = "div_time_groupbox1"
        Me.div_time_groupbox1.Size = New System.Drawing.Size(661, 84)
        Me.div_time_groupbox1.TabIndex = 57
        Me.div_time_groupbox1.TabStop = False
        Me.div_time_groupbox1.Text = "时段1"
        '
        'time_start_string1
        '
        Me.time_start_string1.AutoSize = True
        Me.time_start_string1.Location = New System.Drawing.Point(54, 26)
        Me.time_start_string1.Name = "time_start_string1"
        Me.time_start_string1.Size = New System.Drawing.Size(77, 14)
        Me.time_start_string1.TabIndex = 44
        Me.time_start_string1.Text = "开始时间："
        '
        'second1_beg
        '
        Me.second1_beg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.second1_beg.FormattingEnabled = True
        Me.second1_beg.Items.AddRange(New Object() {"00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50", "51", "52", "53", "54", "55", "56", "57", "58", "59"})
        Me.second1_beg.Location = New System.Drawing.Point(327, 21)
        Me.second1_beg.Name = "second1_beg"
        Me.second1_beg.Size = New System.Drawing.Size(60, 22)
        Me.second1_beg.TabIndex = 37
        '
        'min1_beg
        '
        Me.min1_beg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.min1_beg.FormattingEnabled = True
        Me.min1_beg.Items.AddRange(New Object() {"00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50", "51", "52", "53", "54", "55", "56", "57", "58", "59"})
        Me.min1_beg.Location = New System.Drawing.Point(233, 21)
        Me.min1_beg.Name = "min1_beg"
        Me.min1_beg.Size = New System.Drawing.Size(60, 22)
        Me.min1_beg.TabIndex = 36
        '
        'hour1_beg
        '
        Me.hour1_beg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.hour1_beg.FormattingEnabled = True
        Me.hour1_beg.Items.AddRange(New Object() {"00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23"})
        Me.hour1_beg.Location = New System.Drawing.Point(136, 21)
        Me.hour1_beg.Name = "hour1_beg"
        Me.hour1_beg.Size = New System.Drawing.Size(61, 22)
        Me.hour1_beg.TabIndex = 35
        '
        'mod1
        '
        Me.mod1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.mod1.FormattingEnabled = True
        Me.mod1.Items.AddRange(New Object() {"全开", "全闭", "奇开", "奇闭", "偶开", "偶闭"})
        Me.mod1.Location = New System.Drawing.Point(503, 21)
        Me.mod1.Name = "mod1"
        Me.mod1.Size = New System.Drawing.Size(138, 22)
        Me.mod1.TabIndex = 25
        '
        'mod1_string
        '
        Me.mod1_string.AutoSize = True
        Me.mod1_string.Location = New System.Drawing.Point(446, 24)
        Me.mod1_string.Name = "mod1_string"
        Me.mod1_string.Size = New System.Drawing.Size(49, 14)
        Me.mod1_string.TabIndex = 26
        Me.mod1_string.Text = "模式："
        '
        'hour1_beg_string
        '
        Me.hour1_beg_string.AutoSize = True
        Me.hour1_beg_string.Location = New System.Drawing.Point(205, 24)
        Me.hour1_beg_string.Name = "hour1_beg_string"
        Me.hour1_beg_string.Size = New System.Drawing.Size(21, 14)
        Me.hour1_beg_string.TabIndex = 30
        Me.hour1_beg_string.Text = "时"
        '
        'min1_beg_string
        '
        Me.min1_beg_string.AutoSize = True
        Me.min1_beg_string.Location = New System.Drawing.Point(300, 24)
        Me.min1_beg_string.Name = "min1_beg_string"
        Me.min1_beg_string.Size = New System.Drawing.Size(21, 14)
        Me.min1_beg_string.TabIndex = 32
        Me.min1_beg_string.Text = "分"
        '
        'second1_beg_string
        '
        Me.second1_beg_string.AutoSize = True
        Me.second1_beg_string.Location = New System.Drawing.Point(394, 26)
        Me.second1_beg_string.Name = "second1_beg_string"
        Me.second1_beg_string.Size = New System.Drawing.Size(21, 14)
        Me.second1_beg_string.TabIndex = 34
        Me.second1_beg_string.Text = "秒"
        '
        'gonglv1
        '
        Me.gonglv1.FormattingEnabled = True
        Me.gonglv1.Items.AddRange(New Object() {"全功率", "半功率"})
        Me.gonglv1.Location = New System.Drawing.Point(273, 388)
        Me.gonglv1.Name = "gonglv1"
        Me.gonglv1.Size = New System.Drawing.Size(86, 22)
        Me.gonglv1.TabIndex = 45
        Me.gonglv1.Text = "全功率"
        Me.gonglv1.Visible = False
        '
        'div_time_groupbox3
        '
        Me.div_time_groupbox3.Controls.Add(Me.time_start_string3)
        Me.div_time_groupbox3.Controls.Add(Me.second3_beg)
        Me.div_time_groupbox3.Controls.Add(Me.min3_beg)
        Me.div_time_groupbox3.Controls.Add(Me.hour3_beg)
        Me.div_time_groupbox3.Controls.Add(Me.mod3)
        Me.div_time_groupbox3.Controls.Add(Me.mod3_string)
        Me.div_time_groupbox3.Controls.Add(Me.hour3_beg_string)
        Me.div_time_groupbox3.Controls.Add(Me.min3_beg_string)
        Me.div_time_groupbox3.Controls.Add(Me.second3_beg_string)
        Me.div_time_groupbox3.Location = New System.Drawing.Point(15, 266)
        Me.div_time_groupbox3.Name = "div_time_groupbox3"
        Me.div_time_groupbox3.Size = New System.Drawing.Size(661, 84)
        Me.div_time_groupbox3.TabIndex = 58
        Me.div_time_groupbox3.TabStop = False
        Me.div_time_groupbox3.Text = "时段3"
        '
        'time_start_string3
        '
        Me.time_start_string3.AutoSize = True
        Me.time_start_string3.Location = New System.Drawing.Point(51, 26)
        Me.time_start_string3.Name = "time_start_string3"
        Me.time_start_string3.Size = New System.Drawing.Size(77, 14)
        Me.time_start_string3.TabIndex = 61
        Me.time_start_string3.Text = "开始时间："
        '
        'second3_beg
        '
        Me.second3_beg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.second3_beg.FormattingEnabled = True
        Me.second3_beg.Items.AddRange(New Object() {"00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50", "51", "52", "53", "54", "55", "56", "57", "58", "59"})
        Me.second3_beg.Location = New System.Drawing.Point(332, 22)
        Me.second3_beg.Name = "second3_beg"
        Me.second3_beg.Size = New System.Drawing.Size(54, 22)
        Me.second3_beg.TabIndex = 48
        '
        'min3_beg
        '
        Me.min3_beg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.min3_beg.FormattingEnabled = True
        Me.min3_beg.Items.AddRange(New Object() {"00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50", "51", "52", "53", "54", "55", "56", "57", "58", "59"})
        Me.min3_beg.Location = New System.Drawing.Point(230, 22)
        Me.min3_beg.Name = "min3_beg"
        Me.min3_beg.Size = New System.Drawing.Size(59, 22)
        Me.min3_beg.TabIndex = 47
        '
        'hour3_beg
        '
        Me.hour3_beg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.hour3_beg.FormattingEnabled = True
        Me.hour3_beg.Items.AddRange(New Object() {"00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23"})
        Me.hour3_beg.Location = New System.Drawing.Point(136, 22)
        Me.hour3_beg.Name = "hour3_beg"
        Me.hour3_beg.Size = New System.Drawing.Size(61, 22)
        Me.hour3_beg.TabIndex = 46
        '
        'mod3
        '
        Me.mod3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.mod3.FormattingEnabled = True
        Me.mod3.Items.AddRange(New Object() {"全开", "全闭", "奇开", "奇闭", "偶开", "偶闭"})
        Me.mod3.Location = New System.Drawing.Point(503, 22)
        Me.mod3.Name = "mod3"
        Me.mod3.Size = New System.Drawing.Size(138, 22)
        Me.mod3.TabIndex = 36
        '
        'mod3_string
        '
        Me.mod3_string.AutoSize = True
        Me.mod3_string.Location = New System.Drawing.Point(448, 26)
        Me.mod3_string.Name = "mod3_string"
        Me.mod3_string.Size = New System.Drawing.Size(49, 14)
        Me.mod3_string.TabIndex = 37
        Me.mod3_string.Text = "模式："
        '
        'hour3_beg_string
        '
        Me.hour3_beg_string.AutoSize = True
        Me.hour3_beg_string.Location = New System.Drawing.Point(203, 31)
        Me.hour3_beg_string.Name = "hour3_beg_string"
        Me.hour3_beg_string.Size = New System.Drawing.Size(21, 14)
        Me.hour3_beg_string.TabIndex = 41
        Me.hour3_beg_string.Text = "时"
        '
        'min3_beg_string
        '
        Me.min3_beg_string.AutoSize = True
        Me.min3_beg_string.Location = New System.Drawing.Point(300, 31)
        Me.min3_beg_string.Name = "min3_beg_string"
        Me.min3_beg_string.Size = New System.Drawing.Size(21, 14)
        Me.min3_beg_string.TabIndex = 43
        Me.min3_beg_string.Text = "分"
        '
        'second3_beg_string
        '
        Me.second3_beg_string.AutoSize = True
        Me.second3_beg_string.Location = New System.Drawing.Point(394, 26)
        Me.second3_beg_string.Name = "second3_beg_string"
        Me.second3_beg_string.Size = New System.Drawing.Size(21, 14)
        Me.second3_beg_string.TabIndex = 45
        Me.second3_beg_string.Text = "秒"
        '
        'gonglv3
        '
        Me.gonglv3.FormattingEnabled = True
        Me.gonglv3.Items.AddRange(New Object() {"全功率", "半功率"})
        Me.gonglv3.Location = New System.Drawing.Point(491, 388)
        Me.gonglv3.Name = "gonglv3"
        Me.gonglv3.Size = New System.Drawing.Size(86, 22)
        Me.gonglv3.TabIndex = 62
        Me.gonglv3.Text = "全功率"
        Me.gonglv3.Visible = False
        '
        'div_time_groupbox2
        '
        Me.div_time_groupbox2.Controls.Add(Me.time_start_string2)
        Me.div_time_groupbox2.Controls.Add(Me.second2_beg)
        Me.div_time_groupbox2.Controls.Add(Me.min2_beg)
        Me.div_time_groupbox2.Controls.Add(Me.hour2_beg)
        Me.div_time_groupbox2.Controls.Add(Me.mod2)
        Me.div_time_groupbox2.Controls.Add(Me.mod2_string)
        Me.div_time_groupbox2.Controls.Add(Me.second2_beg_string)
        Me.div_time_groupbox2.Controls.Add(Me.min2_beg_string)
        Me.div_time_groupbox2.Controls.Add(Me.hour2_beg_string)
        Me.div_time_groupbox2.Location = New System.Drawing.Point(15, 145)
        Me.div_time_groupbox2.Name = "div_time_groupbox2"
        Me.div_time_groupbox2.Size = New System.Drawing.Size(661, 84)
        Me.div_time_groupbox2.TabIndex = 59
        Me.div_time_groupbox2.TabStop = False
        Me.div_time_groupbox2.Text = "时段2"
        '
        'time_start_string2
        '
        Me.time_start_string2.AutoSize = True
        Me.time_start_string2.Location = New System.Drawing.Point(51, 36)
        Me.time_start_string2.Name = "time_start_string2"
        Me.time_start_string2.Size = New System.Drawing.Size(77, 14)
        Me.time_start_string2.TabIndex = 60
        Me.time_start_string2.Text = "开始时间："
        '
        'second2_beg
        '
        Me.second2_beg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.second2_beg.FormattingEnabled = True
        Me.second2_beg.Items.AddRange(New Object() {"00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50", "51", "52", "53", "54", "55", "56", "57", "58", "59"})
        Me.second2_beg.Location = New System.Drawing.Point(332, 33)
        Me.second2_beg.Name = "second2_beg"
        Me.second2_beg.Size = New System.Drawing.Size(54, 22)
        Me.second2_beg.TabIndex = 59
        '
        'min2_beg
        '
        Me.min2_beg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.min2_beg.FormattingEnabled = True
        Me.min2_beg.Items.AddRange(New Object() {"00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50", "51", "52", "53", "54", "55", "56", "57", "58", "59"})
        Me.min2_beg.Location = New System.Drawing.Point(232, 33)
        Me.min2_beg.Name = "min2_beg"
        Me.min2_beg.Size = New System.Drawing.Size(56, 22)
        Me.min2_beg.TabIndex = 58
        '
        'hour2_beg
        '
        Me.hour2_beg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.hour2_beg.FormattingEnabled = True
        Me.hour2_beg.Items.AddRange(New Object() {"00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23"})
        Me.hour2_beg.Location = New System.Drawing.Point(136, 33)
        Me.hour2_beg.Name = "hour2_beg"
        Me.hour2_beg.Size = New System.Drawing.Size(61, 22)
        Me.hour2_beg.TabIndex = 57
        '
        'mod2
        '
        Me.mod2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.mod2.FormattingEnabled = True
        Me.mod2.Items.AddRange(New Object() {"全开", "全闭", "奇开", "奇闭", "偶开", "偶闭"})
        Me.mod2.Location = New System.Drawing.Point(503, 33)
        Me.mod2.Name = "mod2"
        Me.mod2.Size = New System.Drawing.Size(138, 22)
        Me.mod2.TabIndex = 47
        '
        'mod2_string
        '
        Me.mod2_string.AutoSize = True
        Me.mod2_string.Location = New System.Drawing.Point(446, 36)
        Me.mod2_string.Name = "mod2_string"
        Me.mod2_string.Size = New System.Drawing.Size(49, 14)
        Me.mod2_string.TabIndex = 48
        Me.mod2_string.Text = "模式："
        '
        'second2_beg_string
        '
        Me.second2_beg_string.AutoSize = True
        Me.second2_beg_string.Location = New System.Drawing.Point(394, 36)
        Me.second2_beg_string.Name = "second2_beg_string"
        Me.second2_beg_string.Size = New System.Drawing.Size(21, 14)
        Me.second2_beg_string.TabIndex = 56
        Me.second2_beg_string.Text = "秒"
        '
        'min2_beg_string
        '
        Me.min2_beg_string.AutoSize = True
        Me.min2_beg_string.Location = New System.Drawing.Point(300, 36)
        Me.min2_beg_string.Name = "min2_beg_string"
        Me.min2_beg_string.Size = New System.Drawing.Size(21, 14)
        Me.min2_beg_string.TabIndex = 54
        Me.min2_beg_string.Text = "分"
        '
        'hour2_beg_string
        '
        Me.hour2_beg_string.AutoSize = True
        Me.hour2_beg_string.Location = New System.Drawing.Point(205, 36)
        Me.hour2_beg_string.Name = "hour2_beg_string"
        Me.hour2_beg_string.Size = New System.Drawing.Size(21, 14)
        Me.hour2_beg_string.TabIndex = 52
        Me.hour2_beg_string.Text = "时"
        '
        'gonglv2
        '
        Me.gonglv2.FormattingEnabled = True
        Me.gonglv2.Items.AddRange(New Object() {"全功率", "半功率"})
        Me.gonglv2.Location = New System.Drawing.Point(373, 388)
        Me.gonglv2.Name = "gonglv2"
        Me.gonglv2.Size = New System.Drawing.Size(86, 22)
        Me.gonglv2.TabIndex = 61
        Me.gonglv2.Text = "全功率"
        Me.gonglv2.Visible = False
        '
        'div_groupbox
        '
        Me.div_groupbox.BackColor = System.Drawing.Color.Transparent
        Me.div_groupbox.Controls.Add(Me.gonglv3)
        Me.div_groupbox.Controls.Add(Me.gonglv2)
        Me.div_groupbox.Controls.Add(Me.gonglv1)
        Me.div_groupbox.Controls.Add(Me.mod_level_choose)
        Me.div_groupbox.Controls.Add(Me.mod_level_choose_string)
        Me.div_groupbox.Controls.Add(Me.div_time_groupbox2)
        Me.div_groupbox.Controls.Add(Me.div_time_groupbox3)
        Me.div_groupbox.Controls.Add(Me.div_time_groupbox1)
        Me.div_groupbox.Controls.Add(Me.input_hms)
        Me.div_groupbox.Location = New System.Drawing.Point(26, 14)
        Me.div_groupbox.Name = "div_groupbox"
        Me.div_groupbox.Size = New System.Drawing.Size(691, 497)
        Me.div_groupbox.TabIndex = 1
        Me.div_groupbox.TabStop = False
        '
        'mod_level_choose
        '
        Me.mod_level_choose.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.mod_level_choose.FormattingEnabled = True
        Me.mod_level_choose.Items.AddRange(New Object() {"平时模式", "节日模式", "半夜模式", "全关模式"})
        Me.mod_level_choose.Location = New System.Drawing.Point(96, 395)
        Me.mod_level_choose.Name = "mod_level_choose"
        Me.mod_level_choose.Size = New System.Drawing.Size(114, 22)
        Me.mod_level_choose.TabIndex = 61
        '
        'mod_level_choose_string
        '
        Me.mod_level_choose_string.AutoSize = True
        Me.mod_level_choose_string.Location = New System.Drawing.Point(13, 398)
        Me.mod_level_choose_string.Name = "mod_level_choose_string"
        Me.mod_level_choose_string.Size = New System.Drawing.Size(77, 14)
        Me.mod_level_choose_string.TabIndex = 60
        Me.mod_level_choose_string.Text = "模式名称："
        '
        '时段划分
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(724, 517)
        Me.Controls.Add(Me.div_groupbox)
        Me.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "时段划分"
        Me.Text = "控制模式编辑窗口"
        Me.div_time_groupbox1.ResumeLayout(False)
        Me.div_time_groupbox1.PerformLayout()
        Me.div_time_groupbox3.ResumeLayout(False)
        Me.div_time_groupbox3.PerformLayout()
        Me.div_time_groupbox2.ResumeLayout(False)
        Me.div_time_groupbox2.PerformLayout()
        Me.div_groupbox.ResumeLayout(False)
        Me.div_groupbox.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents input_hms As System.Windows.Forms.Button
    Friend WithEvents div_time_groupbox1 As System.Windows.Forms.GroupBox
    Friend WithEvents second1_beg As System.Windows.Forms.ComboBox
    Friend WithEvents min1_beg As System.Windows.Forms.ComboBox
    Friend WithEvents hour1_beg As System.Windows.Forms.ComboBox
    Friend WithEvents mod1 As System.Windows.Forms.ComboBox
    Friend WithEvents mod1_string As System.Windows.Forms.Label
    Friend WithEvents hour1_beg_string As System.Windows.Forms.Label
    Friend WithEvents min1_beg_string As System.Windows.Forms.Label
    Friend WithEvents second1_beg_string As System.Windows.Forms.Label
    Friend WithEvents div_time_groupbox3 As System.Windows.Forms.GroupBox
    Friend WithEvents second3_beg As System.Windows.Forms.ComboBox
    Friend WithEvents min3_beg As System.Windows.Forms.ComboBox
    Friend WithEvents hour3_beg As System.Windows.Forms.ComboBox
    Friend WithEvents mod3 As System.Windows.Forms.ComboBox
    Friend WithEvents mod3_string As System.Windows.Forms.Label
    Friend WithEvents hour3_beg_string As System.Windows.Forms.Label
    Friend WithEvents min3_beg_string As System.Windows.Forms.Label
    Friend WithEvents second3_beg_string As System.Windows.Forms.Label
    Friend WithEvents div_time_groupbox2 As System.Windows.Forms.GroupBox
    Friend WithEvents second2_beg As System.Windows.Forms.ComboBox
    Friend WithEvents min2_beg As System.Windows.Forms.ComboBox
    Friend WithEvents hour2_beg As System.Windows.Forms.ComboBox
    Friend WithEvents mod2 As System.Windows.Forms.ComboBox
    Friend WithEvents mod2_string As System.Windows.Forms.Label
    Friend WithEvents second2_beg_string As System.Windows.Forms.Label
    Friend WithEvents min2_beg_string As System.Windows.Forms.Label
    Friend WithEvents hour2_beg_string As System.Windows.Forms.Label
    Friend WithEvents div_groupbox As System.Windows.Forms.GroupBox
    Friend WithEvents time_start_string1 As System.Windows.Forms.Label
    Friend WithEvents time_start_string3 As System.Windows.Forms.Label
    Friend WithEvents time_start_string2 As System.Windows.Forms.Label
    Friend WithEvents mod_level_choose_string As System.Windows.Forms.Label
    Friend WithEvents mod_level_choose As System.Windows.Forms.ComboBox
    Friend WithEvents gonglv1 As System.Windows.Forms.ComboBox
    Friend WithEvents gonglv3 As System.Windows.Forms.ComboBox
    Friend WithEvents gonglv2 As System.Windows.Forms.ComboBox
End Class
