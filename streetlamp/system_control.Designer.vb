<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class system_control
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(system_control))
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.control_set = New System.Windows.Forms.Button
        Me.system_show = New System.Windows.Forms.Button
        Me.alarm = New System.Windows.Forms.Button
        Me.hand_control = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.time_mod = New System.Windows.Forms.Button
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = CType(resources.GetObject("PictureBox1.BackgroundImage"), System.Drawing.Image)
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox1.Location = New System.Drawing.Point(6, 6)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(200, 543)
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.GroupBox3)
        Me.GroupBox1.Controls.Add(Me.time_mod)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.hand_control)
        Me.GroupBox1.Controls.Add(Me.alarm)
        Me.GroupBox1.Controls.Add(Me.system_show)
        Me.GroupBox1.Controls.Add(Me.control_set)
        Me.GroupBox1.Location = New System.Drawing.Point(213, 6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(632, 543)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        '
        'control_set
        '
        Me.control_set.Location = New System.Drawing.Point(228, 488)
        Me.control_set.Name = "control_set"
        Me.control_set.Size = New System.Drawing.Size(75, 23)
        Me.control_set.TabIndex = 0
        Me.control_set.Text = "控制设定"
        Me.control_set.UseVisualStyleBackColor = True
        '
        'system_show
        '
        Me.system_show.Location = New System.Drawing.Point(333, 488)
        Me.system_show.Name = "system_show"
        Me.system_show.Size = New System.Drawing.Size(75, 23)
        Me.system_show.TabIndex = 1
        Me.system_show.Text = "界面演示"
        Me.system_show.UseVisualStyleBackColor = True
        '
        'alarm
        '
        Me.alarm.Location = New System.Drawing.Point(426, 488)
        Me.alarm.Name = "alarm"
        Me.alarm.Size = New System.Drawing.Size(75, 23)
        Me.alarm.TabIndex = 2
        Me.alarm.Text = "报警模拟"
        Me.alarm.UseVisualStyleBackColor = True
        '
        'hand_control
        '
        Me.hand_control.Location = New System.Drawing.Point(119, 499)
        Me.hand_control.Name = "hand_control"
        Me.hand_control.Size = New System.Drawing.Size(75, 23)
        Me.hand_control.TabIndex = 3
        Me.hand_control.Text = "手工控制"
        Me.hand_control.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(6, 20)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(620, 132)
        Me.GroupBox2.TabIndex = 4
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "增加区域定位"
        '
        'time_mod
        '
        Me.time_mod.Location = New System.Drawing.Point(20, 499)
        Me.time_mod.Name = "time_mod"
        Me.time_mod.Size = New System.Drawing.Size(75, 23)
        Me.time_mod.TabIndex = 5
        Me.time_mod.Text = "时钟模拟"
        Me.time_mod.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Location = New System.Drawing.Point(12, 167)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(613, 149)
        Me.GroupBox3.TabIndex = 6
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "时段设置"
        '
        'system_control
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(857, 561)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Name = "system_control"
        Me.Text = "系统控制台"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents control_set As System.Windows.Forms.Button
    Friend WithEvents system_show As System.Windows.Forms.Button
    Friend WithEvents alarm As System.Windows.Forms.Button
    Friend WithEvents hand_control As System.Windows.Forms.Button
    Friend WithEvents time_mod As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
End Class
