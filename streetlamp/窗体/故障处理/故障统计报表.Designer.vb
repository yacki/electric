<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 故障统计报表
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(故障统计报表))
        Me.check_time = New System.Windows.Forms.DateTimePicker
        Me.Label1 = New System.Windows.Forms.Label
        Me.find_problem = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.control_box_name = New System.Windows.Forms.ComboBox
        Me.BackgroundWorker_check_problem = New System.ComponentModel.BackgroundWorker
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.record_num = New System.Windows.Forms.ToolStripStatusLabel
        Me.ProgressBar = New System.Windows.Forms.ToolStripProgressBar
        Me.control_area_name = New System.Windows.Forms.ComboBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.control_street_name = New System.Windows.Forms.ComboBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.control_city_name = New System.Windows.Forms.ComboBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'check_time
        '
        Me.check_time.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.check_time.Location = New System.Drawing.Point(185, 176)
        Me.check_time.Name = "check_time"
        Me.check_time.Size = New System.Drawing.Size(149, 23)
        Me.check_time.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label1.Location = New System.Drawing.Point(70, 143)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(97, 14)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "主控箱名称："
        '
        'find_problem
        '
        Me.find_problem.Location = New System.Drawing.Point(146, 244)
        Me.find_problem.Name = "find_problem"
        Me.find_problem.Size = New System.Drawing.Size(87, 27)
        Me.find_problem.TabIndex = 2
        Me.find_problem.Text = "查询"
        Me.find_problem.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label2.Location = New System.Drawing.Point(85, 185)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(82, 14)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "统计日期："
        '
        'control_box_name
        '
        Me.control_box_name.FormattingEnabled = True
        Me.control_box_name.Location = New System.Drawing.Point(185, 135)
        Me.control_box_name.Name = "control_box_name"
        Me.control_box_name.Size = New System.Drawing.Size(149, 22)
        Me.control_box_name.TabIndex = 5
        '
        'BackgroundWorker_check_problem
        '
        Me.BackgroundWorker_check_problem.WorkerReportsProgress = True
        Me.BackgroundWorker_check_problem.WorkerSupportsCancellation = True
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.record_num, Me.ProgressBar})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 299)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Padding = New System.Windows.Forms.Padding(1, 0, 16, 0)
        Me.StatusStrip1.Size = New System.Drawing.Size(413, 22)
        Me.StatusStrip1.TabIndex = 6
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'record_num
        '
        Me.record_num.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.record_num.Name = "record_num"
        Me.record_num.Size = New System.Drawing.Size(63, 17)
        Me.record_num.Text = "故障日志"
        '
        'ProgressBar
        '
        Me.ProgressBar.Name = "ProgressBar"
        Me.ProgressBar.Size = New System.Drawing.Size(117, 19)
        Me.ProgressBar.Visible = False
        '
        'control_area_name
        '
        Me.control_area_name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.control_area_name.FormattingEnabled = True
        Me.control_area_name.Location = New System.Drawing.Point(185, 69)
        Me.control_area_name.Name = "control_area_name"
        Me.control_area_name.Size = New System.Drawing.Size(149, 22)
        Me.control_area_name.TabIndex = 152
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label12.Location = New System.Drawing.Point(85, 77)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(82, 14)
        Me.Label12.TabIndex = 151
        Me.Label12.Text = "区域名称："
        '
        'control_street_name
        '
        Me.control_street_name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.control_street_name.FormattingEnabled = True
        Me.control_street_name.Location = New System.Drawing.Point(185, 99)
        Me.control_street_name.Name = "control_street_name"
        Me.control_street_name.Size = New System.Drawing.Size(149, 22)
        Me.control_street_name.TabIndex = 150
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label11.Location = New System.Drawing.Point(85, 107)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(82, 14)
        Me.Label11.TabIndex = 149
        Me.Label11.Text = "街道名称："
        '
        'control_city_name
        '
        Me.control_city_name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.control_city_name.FormattingEnabled = True
        Me.control_city_name.Location = New System.Drawing.Point(185, 36)
        Me.control_city_name.Name = "control_city_name"
        Me.control_city_name.Size = New System.Drawing.Size(149, 22)
        Me.control_city_name.TabIndex = 148
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label10.Location = New System.Drawing.Point(85, 44)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(82, 14)
        Me.Label10.TabIndex = 147
        Me.Label10.Text = "城市名称："
        '
        '故障统计报表
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(413, 321)
        Me.Controls.Add(Me.control_area_name)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.control_street_name)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.control_city_name)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.control_box_name)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.find_problem)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.check_time)
        Me.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "故障统计报表"
        Me.Text = "故障日志"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents check_time As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents find_problem As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents control_box_name As System.Windows.Forms.ComboBox
    Friend WithEvents BackgroundWorker_check_problem As System.ComponentModel.BackgroundWorker
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents record_num As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ProgressBar As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents control_area_name As System.Windows.Forms.ComboBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents control_street_name As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents control_city_name As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
End Class
