<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 清空数据库
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(清空数据库))
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.bt_clear = New System.Windows.Forms.Button()
        Me.cb_database = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.dtp_date_end = New System.Windows.Forms.DateTimePicker()
        Me.Date_end_string = New System.Windows.Forms.Label()
        Me.dtp_date_start = New System.Windows.Forms.DateTimePicker()
        Me.Date_start_string = New System.Windows.Forms.Label()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox4
        '
        Me.GroupBox4.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox4.Controls.Add(Me.bt_clear)
        Me.GroupBox4.Controls.Add(Me.cb_database)
        Me.GroupBox4.Controls.Add(Me.Label8)
        Me.GroupBox4.Controls.Add(Me.dtp_date_end)
        Me.GroupBox4.Controls.Add(Me.Date_end_string)
        Me.GroupBox4.Controls.Add(Me.dtp_date_start)
        Me.GroupBox4.Controls.Add(Me.Date_start_string)
        Me.GroupBox4.Location = New System.Drawing.Point(9, 10)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(283, 241)
        Me.GroupBox4.TabIndex = 15
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "清除数据表"
        '
        'bt_clear
        '
        Me.bt_clear.Location = New System.Drawing.Point(96, 182)
        Me.bt_clear.Name = "bt_clear"
        Me.bt_clear.Size = New System.Drawing.Size(87, 27)
        Me.bt_clear.TabIndex = 116
        Me.bt_clear.Text = "清除"
        Me.bt_clear.UseVisualStyleBackColor = True
        '
        'cb_database
        '
        Me.cb_database.FormattingEnabled = True
        Me.cb_database.Items.AddRange(New Object() {"系统日志表", "设备日志表", "报警历史表", "数据状态表", "系统控制表"})
        Me.cb_database.Location = New System.Drawing.Point(107, 123)
        Me.cb_database.Name = "cb_database"
        Me.cb_database.Size = New System.Drawing.Size(137, 20)
        Me.cb_database.TabIndex = 115
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(24, 126)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(65, 12)
        Me.Label8.TabIndex = 114
        Me.Label8.Text = "数据名称："
        '
        'dtp_date_end
        '
        Me.dtp_date_end.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_date_end.Location = New System.Drawing.Point(107, 77)
        Me.dtp_date_end.Name = "dtp_date_end"
        Me.dtp_date_end.Size = New System.Drawing.Size(137, 21)
        Me.dtp_date_end.TabIndex = 113
        '
        'Date_end_string
        '
        Me.Date_end_string.AutoSize = True
        Me.Date_end_string.BackColor = System.Drawing.Color.Transparent
        Me.Date_end_string.Location = New System.Drawing.Point(24, 82)
        Me.Date_end_string.Name = "Date_end_string"
        Me.Date_end_string.Size = New System.Drawing.Size(65, 12)
        Me.Date_end_string.TabIndex = 111
        Me.Date_end_string.Text = "结束日期："
        '
        'dtp_date_start
        '
        Me.dtp_date_start.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_date_start.Location = New System.Drawing.Point(107, 38)
        Me.dtp_date_start.Name = "dtp_date_start"
        Me.dtp_date_start.Size = New System.Drawing.Size(137, 21)
        Me.dtp_date_start.TabIndex = 112
        '
        'Date_start_string
        '
        Me.Date_start_string.AutoSize = True
        Me.Date_start_string.BackColor = System.Drawing.Color.Transparent
        Me.Date_start_string.Location = New System.Drawing.Point(24, 42)
        Me.Date_start_string.Name = "Date_start_string"
        Me.Date_start_string.Size = New System.Drawing.Size(65, 12)
        Me.Date_start_string.TabIndex = 110
        Me.Date_start_string.Text = "开始日期："
        '
        '清空数据库
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(303, 262)
        Me.Controls.Add(Me.GroupBox4)
        Me.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "清空数据库"
        Me.Text = "清空数据库"
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents bt_clear As System.Windows.Forms.Button
    Friend WithEvents cb_database As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents dtp_date_end As System.Windows.Forms.DateTimePicker
    Friend WithEvents Date_end_string As System.Windows.Forms.Label
    Friend WithEvents dtp_date_start As System.Windows.Forms.DateTimePicker
    Friend WithEvents Date_start_string As System.Windows.Forms.Label
End Class
