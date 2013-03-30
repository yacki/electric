<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 单灯时段设定
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(单灯时段设定))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lamp_time_list = New System.Windows.Forms.DataGridView()
        Me.del_id = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.short_lamp_id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Openclose = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Timestart = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Timeend = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lamp_id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LamplevelBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Lamp_level = New streetlamp.lamp_level()
        Me.lamp_id_list = New System.Windows.Forms.ComboBox()
        Me.state = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.hour_start = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.min_start = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.second_start = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.second_end = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.min_end = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.hour_end = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.time_set = New System.Windows.Forms.Button()
        Me.time_del = New System.Windows.Forms.Button()
        Me.Lamp_levelTableAdapter = New streetlamp.lamp_levelTableAdapters.lamp_levelTableAdapter()
        Me.full_lamp_id = New System.Windows.Forms.Label()
        CType(Me.lamp_time_list, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LamplevelBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Lamp_level, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(14, 33)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(63, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "灯的编号"
        '
        'lamp_time_list
        '
        Me.lamp_time_list.AllowUserToAddRows = False
        Me.lamp_time_list.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lamp_time_list.AutoGenerateColumns = False
        Me.lamp_time_list.BackgroundColor = System.Drawing.Color.WhiteSmoke
        Me.lamp_time_list.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.lamp_time_list.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.del_id, Me.short_lamp_id, Me.Openclose, Me.Timestart, Me.Timeend, Me.id, Me.lamp_id})
        Me.lamp_time_list.DataSource = Me.LamplevelBindingSource
        Me.lamp_time_list.Location = New System.Drawing.Point(283, 3)
        Me.lamp_time_list.Name = "lamp_time_list"
        Me.lamp_time_list.RowTemplate.Height = 23
        Me.lamp_time_list.Size = New System.Drawing.Size(436, 426)
        Me.lamp_time_list.TabIndex = 1
        '
        'del_id
        '
        Me.del_id.FalseValue = "0"
        Me.del_id.HeaderText = ""
        Me.del_id.Name = "del_id"
        Me.del_id.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.del_id.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.del_id.TrueValue = "1"
        Me.del_id.Width = 20
        '
        'short_lamp_id
        '
        Me.short_lamp_id.HeaderText = "灯的编号"
        Me.short_lamp_id.Name = "short_lamp_id"
        '
        'Openclose
        '
        Me.Openclose.DataPropertyName = "open_close"
        Me.Openclose.HeaderText = "时控状态"
        Me.Openclose.Name = "Openclose"
        '
        'Timestart
        '
        Me.Timestart.DataPropertyName = "time_start"
        Me.Timestart.HeaderText = "开始时间"
        Me.Timestart.Name = "Timestart"
        '
        'Timeend
        '
        Me.Timeend.DataPropertyName = "time_end"
        Me.Timeend.HeaderText = "结束时间"
        Me.Timeend.Name = "Timeend"
        '
        'id
        '
        Me.id.DataPropertyName = "id"
        Me.id.HeaderText = "id"
        Me.id.Name = "id"
        Me.id.ReadOnly = True
        Me.id.Visible = False
        '
        'lamp_id
        '
        Me.lamp_id.DataPropertyName = "lamp_id"
        Me.lamp_id.HeaderText = "灯的编号"
        Me.lamp_id.Name = "lamp_id"
        Me.lamp_id.Visible = False
        '
        'LamplevelBindingSource
        '
        Me.LamplevelBindingSource.DataMember = "lamp_level"
        Me.LamplevelBindingSource.DataSource = Me.Lamp_level
        '
        'Lamp_level
        '
        Me.Lamp_level.DataSetName = "lamp_level"
        Me.Lamp_level.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'lamp_id_list
        '
        Me.lamp_id_list.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.lamp_id_list.FormattingEnabled = True
        Me.lamp_id_list.Location = New System.Drawing.Point(107, 30)
        Me.lamp_id_list.Name = "lamp_id_list"
        Me.lamp_id_list.Size = New System.Drawing.Size(111, 22)
        Me.lamp_id_list.TabIndex = 2
        '
        'state
        '
        Me.state.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.state.FormattingEnabled = True
        Me.state.Items.AddRange(New Object() {"开", "关"})
        Me.state.Location = New System.Drawing.Point(107, 104)
        Me.state.Name = "state"
        Me.state.Size = New System.Drawing.Size(111, 22)
        Me.state.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(14, 107)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(63, 14)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "控制方式"
        '
        'hour_start
        '
        Me.hour_start.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.hour_start.FormattingEnabled = True
        Me.hour_start.Items.AddRange(New Object() {"00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23"})
        Me.hour_start.Location = New System.Drawing.Point(107, 153)
        Me.hour_start.Name = "hour_start"
        Me.hour_start.Size = New System.Drawing.Size(111, 22)
        Me.hour_start.TabIndex = 6
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(14, 157)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(63, 14)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "开始时间"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(229, 157)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(21, 14)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "时"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Location = New System.Drawing.Point(229, 198)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(21, 14)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "分"
        '
        'min_start
        '
        Me.min_start.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.min_start.FormattingEnabled = True
        Me.min_start.Items.AddRange(New Object() {"00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50", "51", "52", "53", "54", "55", "56", "57", "58", "59"})
        Me.min_start.Location = New System.Drawing.Point(107, 194)
        Me.min_start.Name = "min_start"
        Me.min_start.Size = New System.Drawing.Size(111, 22)
        Me.min_start.TabIndex = 8
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Location = New System.Drawing.Point(229, 240)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(21, 14)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "秒"
        '
        'second_start
        '
        Me.second_start.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.second_start.FormattingEnabled = True
        Me.second_start.Items.AddRange(New Object() {"00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50", "51", "52", "53", "54", "55", "56", "57", "58", "59"})
        Me.second_start.Location = New System.Drawing.Point(107, 236)
        Me.second_start.Name = "second_start"
        Me.second_start.Size = New System.Drawing.Size(111, 22)
        Me.second_start.TabIndex = 10
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Location = New System.Drawing.Point(229, 355)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(21, 14)
        Me.Label7.TabIndex = 18
        Me.Label7.Text = "秒"
        '
        'second_end
        '
        Me.second_end.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.second_end.FormattingEnabled = True
        Me.second_end.Items.AddRange(New Object() {"00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50", "51", "52", "53", "54", "55", "56", "57", "58", "59"})
        Me.second_end.Location = New System.Drawing.Point(107, 351)
        Me.second_end.Name = "second_end"
        Me.second_end.Size = New System.Drawing.Size(111, 22)
        Me.second_end.TabIndex = 17
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Location = New System.Drawing.Point(229, 319)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(21, 14)
        Me.Label8.TabIndex = 16
        Me.Label8.Text = "分"
        '
        'min_end
        '
        Me.min_end.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.min_end.FormattingEnabled = True
        Me.min_end.Items.AddRange(New Object() {"00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50", "51", "52", "53", "54", "55", "56", "57", "58", "59"})
        Me.min_end.Location = New System.Drawing.Point(107, 315)
        Me.min_end.Name = "min_end"
        Me.min_end.Size = New System.Drawing.Size(111, 22)
        Me.min_end.TabIndex = 15
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Location = New System.Drawing.Point(229, 283)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(21, 14)
        Me.Label9.TabIndex = 14
        Me.Label9.Text = "时"
        '
        'hour_end
        '
        Me.hour_end.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.hour_end.FormattingEnabled = True
        Me.hour_end.Items.AddRange(New Object() {"00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23"})
        Me.hour_end.Location = New System.Drawing.Point(107, 279)
        Me.hour_end.Name = "hour_end"
        Me.hour_end.Size = New System.Drawing.Size(111, 22)
        Me.hour_end.TabIndex = 13
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Location = New System.Drawing.Point(14, 283)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(63, 14)
        Me.Label10.TabIndex = 12
        Me.Label10.Text = "终止时间"
        '
        'time_set
        '
        Me.time_set.Location = New System.Drawing.Point(76, 402)
        Me.time_set.Name = "time_set"
        Me.time_set.Size = New System.Drawing.Size(126, 27)
        Me.time_set.TabIndex = 19
        Me.time_set.Text = "设定单灯时段"
        Me.time_set.UseVisualStyleBackColor = True
        '
        'time_del
        '
        Me.time_del.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.time_del.Location = New System.Drawing.Point(432, 448)
        Me.time_del.Name = "time_del"
        Me.time_del.Size = New System.Drawing.Size(92, 27)
        Me.time_del.TabIndex = 20
        Me.time_del.Text = "删除时段控制"
        Me.time_del.UseVisualStyleBackColor = True
        '
        'Lamp_levelTableAdapter
        '
        Me.Lamp_levelTableAdapter.ClearBeforeFill = True
        '
        'full_lamp_id
        '
        Me.full_lamp_id.AutoSize = True
        Me.full_lamp_id.BackColor = System.Drawing.Color.Transparent
        Me.full_lamp_id.Location = New System.Drawing.Point(194, 9)
        Me.full_lamp_id.Name = "full_lamp_id"
        Me.full_lamp_id.Size = New System.Drawing.Size(56, 14)
        Me.full_lamp_id.TabIndex = 21
        Me.full_lamp_id.Text = "Label11"
        Me.full_lamp_id.Visible = False
        '
        '单灯时段设定
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.streetlamp.My.Resources.Resources.bg11
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(731, 489)
        Me.Controls.Add(Me.full_lamp_id)
        Me.Controls.Add(Me.time_del)
        Me.Controls.Add(Me.time_set)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.second_end)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.min_end)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.hour_end)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.second_start)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.min_start)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.hour_start)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.state)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lamp_id_list)
        Me.Controls.Add(Me.lamp_time_list)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "单灯时段设定"
        Me.Text = "单灯时段编辑"
        CType(Me.lamp_time_list, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LamplevelBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Lamp_level, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lamp_time_list As System.Windows.Forms.DataGridView
    Friend WithEvents Lamp_level As streetlamp.lamp_level
    Friend WithEvents LamplevelBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents Lamp_levelTableAdapter As streetlamp.lamp_levelTableAdapters.lamp_levelTableAdapter
    Friend WithEvents lamp_id_list As System.Windows.Forms.ComboBox
    Friend WithEvents state As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents hour_start As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents min_start As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents second_start As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents second_end As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents min_end As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents hour_end As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents time_set As System.Windows.Forms.Button
    Friend WithEvents time_del As System.Windows.Forms.Button
    Friend WithEvents del_id As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents short_lamp_id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Openclose As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Timestart As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Timeend As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lamp_id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents full_lamp_id As System.Windows.Forms.Label
End Class
