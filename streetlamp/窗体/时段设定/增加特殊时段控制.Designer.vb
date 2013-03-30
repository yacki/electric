<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 增加特殊时段控制
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
        Me.div_time_groupbox1 = New System.Windows.Forms.GroupBox
        Me.Add_divlevel = New System.Windows.Forms.Button
        Me.time_start_string1 = New System.Windows.Forms.Label
        Me.mod_value = New System.Windows.Forms.ComboBox
        Me.mod1_string = New System.Windows.Forms.Label
        Me.Add_modlist = New System.Windows.Forms.DataGridView
        Me.start_time = New System.Windows.Forms.DateTimePicker
        Me.id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.time = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.moduel = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.mod_level_choose = New System.Windows.Forms.TextBox
        Me.mod_level_choose_string = New System.Windows.Forms.Label
        Me.input_hms = New System.Windows.Forms.Button
        Me.gonglv1 = New System.Windows.Forms.ComboBox
        Me.div_time_groupbox1.SuspendLayout()
        CType(Me.Add_modlist, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'div_time_groupbox1
        '
        Me.div_time_groupbox1.Controls.Add(Me.start_time)
        Me.div_time_groupbox1.Controls.Add(Me.Add_divlevel)
        Me.div_time_groupbox1.Controls.Add(Me.time_start_string1)
        Me.div_time_groupbox1.Controls.Add(Me.mod_value)
        Me.div_time_groupbox1.Controls.Add(Me.mod1_string)
        Me.div_time_groupbox1.Controls.Add(Me.Add_modlist)
        Me.div_time_groupbox1.Location = New System.Drawing.Point(12, 12)
        Me.div_time_groupbox1.Name = "div_time_groupbox1"
        Me.div_time_groupbox1.Size = New System.Drawing.Size(695, 333)
        Me.div_time_groupbox1.TabIndex = 59
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
        'mod_value
        '
        Me.mod_value.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.mod_value.FormattingEnabled = True
        Me.mod_value.Items.AddRange(New Object() {"类型全开", "类型全闭", "类型奇开", "类型奇闭", "类型偶开", "类型偶闭", "回路开", "回路关"})
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
        'Add_modlist
        '
        Me.Add_modlist.AllowUserToAddRows = False
        Me.Add_modlist.BackgroundColor = System.Drawing.Color.WhiteSmoke
        Me.Add_modlist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Add_modlist.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.id, Me.time, Me.moduel})
        Me.Add_modlist.Location = New System.Drawing.Point(6, 76)
        Me.Add_modlist.Name = "Add_modlist"
        Me.Add_modlist.RowTemplate.Height = 23
        Me.Add_modlist.Size = New System.Drawing.Size(681, 251)
        Me.Add_modlist.TabIndex = 67
        '
        'start_time
        '
        Me.start_time.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.start_time.Location = New System.Drawing.Point(107, 34)
        Me.start_time.Name = "start_time"
        Me.start_time.Size = New System.Drawing.Size(200, 23)
        Me.start_time.TabIndex = 88
        '
        'id
        '
        Me.id.HeaderText = "编号"
        Me.id.Name = "id"
        '
        'time
        '
        Me.time.HeaderText = "时间"
        Me.time.Name = "time"
        Me.time.Width = 200
        '
        'moduel
        '
        Me.moduel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.moduel.HeaderText = "模式"
        Me.moduel.Name = "moduel"
        '
        'mod_level_choose
        '
        Me.mod_level_choose.Location = New System.Drawing.Point(136, 380)
        Me.mod_level_choose.Name = "mod_level_choose"
        Me.mod_level_choose.Size = New System.Drawing.Size(100, 23)
        Me.mod_level_choose.TabIndex = 77
        '
        'mod_level_choose_string
        '
        Me.mod_level_choose_string.AutoSize = True
        Me.mod_level_choose_string.Location = New System.Drawing.Point(36, 383)
        Me.mod_level_choose_string.Name = "mod_level_choose_string"
        Me.mod_level_choose_string.Size = New System.Drawing.Size(105, 14)
        Me.mod_level_choose_string.TabIndex = 76
        Me.mod_level_choose_string.Text = "特殊模式名称："
        '
        'input_hms
        '
        Me.input_hms.Location = New System.Drawing.Point(345, 377)
        Me.input_hms.Name = "input_hms"
        Me.input_hms.Size = New System.Drawing.Size(102, 27)
        Me.input_hms.TabIndex = 75
        Me.input_hms.Text = "输入控制模式"
        Me.input_hms.UseVisualStyleBackColor = True
        '
        'gonglv1
        '
        Me.gonglv1.FormattingEnabled = True
        Me.gonglv1.Items.AddRange(New Object() {"全功率", "半功率"})
        Me.gonglv1.Location = New System.Drawing.Point(616, 377)
        Me.gonglv1.Name = "gonglv1"
        Me.gonglv1.Size = New System.Drawing.Size(57, 22)
        Me.gonglv1.TabIndex = 74
        Me.gonglv1.Text = "全功率"
        Me.gonglv1.Visible = False
        '
        '增加特殊时段控制
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(719, 482)
        Me.Controls.Add(Me.mod_level_choose)
        Me.Controls.Add(Me.mod_level_choose_string)
        Me.Controls.Add(Me.input_hms)
        Me.Controls.Add(Me.gonglv1)
        Me.Controls.Add(Me.div_time_groupbox1)
        Me.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Name = "增加特殊时段控制"
        Me.Text = "增加特殊时段控制"
        Me.div_time_groupbox1.ResumeLayout(False)
        Me.div_time_groupbox1.PerformLayout()
        CType(Me.Add_modlist, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents div_time_groupbox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Add_divlevel As System.Windows.Forms.Button
    Friend WithEvents time_start_string1 As System.Windows.Forms.Label
    Friend WithEvents mod_value As System.Windows.Forms.ComboBox
    Friend WithEvents mod1_string As System.Windows.Forms.Label
    Friend WithEvents Add_modlist As System.Windows.Forms.DataGridView
    Friend WithEvents start_time As System.Windows.Forms.DateTimePicker
    Friend WithEvents id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents time As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents moduel As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents mod_level_choose As System.Windows.Forms.TextBox
    Friend WithEvents mod_level_choose_string As System.Windows.Forms.Label
    Friend WithEvents input_hms As System.Windows.Forms.Button
    Friend WithEvents gonglv1 As System.Windows.Forms.ComboBox
End Class
