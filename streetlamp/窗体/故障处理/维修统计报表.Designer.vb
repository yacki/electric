<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 维修统计报表
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(维修统计报表))
        Me.control_box_name = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.find_problem = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.check_time = New System.Windows.Forms.DateTimePicker
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.record_num = New System.Windows.Forms.ToolStripStatusLabel
        Me.ProgressBar = New System.Windows.Forms.ToolStripProgressBar
        Me.BackgroundWorker_check_problem = New System.ComponentModel.BackgroundWorker
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'control_box_name
        '
        Me.control_box_name.FormattingEnabled = True
        Me.control_box_name.Location = New System.Drawing.Point(189, 95)
        Me.control_box_name.Name = "control_box_name"
        Me.control_box_name.Size = New System.Drawing.Size(128, 20)
        Me.control_box_name.TabIndex = 10
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label2.Location = New System.Drawing.Point(68, 49)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(82, 14)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "统计日期："
        '
        'find_problem
        '
        Me.find_problem.Location = New System.Drawing.Point(156, 143)
        Me.find_problem.Name = "find_problem"
        Me.find_problem.Size = New System.Drawing.Size(75, 23)
        Me.find_problem.TabIndex = 8
        Me.find_problem.Text = "查询"
        Me.find_problem.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label1.Location = New System.Drawing.Point(68, 101)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(82, 14)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "区域名称："
        '
        'check_time
        '
        Me.check_time.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.check_time.Location = New System.Drawing.Point(189, 47)
        Me.check_time.Name = "check_time"
        Me.check_time.Size = New System.Drawing.Size(128, 21)
        Me.check_time.TabIndex = 6
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.record_num, Me.ProgressBar})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 190)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(403, 22)
        Me.StatusStrip1.TabIndex = 11
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'record_num
        '
        Me.record_num.Name = "record_num"
        Me.record_num.Size = New System.Drawing.Size(77, 17)
        Me.record_num.Text = "维修报表统计"
        '
        'ProgressBar
        '
        Me.ProgressBar.Name = "ProgressBar"
        Me.ProgressBar.Size = New System.Drawing.Size(100, 16)
        Me.ProgressBar.Visible = False
        '
        'BackgroundWorker_check_problem
        '
        Me.BackgroundWorker_check_problem.WorkerReportsProgress = True
        Me.BackgroundWorker_check_problem.WorkerSupportsCancellation = True
        '
        '维修统计报表
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.streetlamp.My.Resources.Resources.bg11
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(403, 212)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.control_box_name)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.find_problem)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.check_time)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "维修统计报表"
        Me.Text = "维修统计报表"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents control_box_name As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents find_problem As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents check_time As System.Windows.Forms.DateTimePicker
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents record_num As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ProgressBar As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents BackgroundWorker_check_problem As System.ComponentModel.BackgroundWorker
End Class
