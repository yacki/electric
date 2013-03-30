<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 删除节日模式
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(删除节日模式))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.tv__holiday_mod = New System.Windows.Forms.TreeView()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.rtb_holiday_mod = New System.Windows.Forms.RichTextBox()
        Me.bt_del_holiday_mod = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.tv__holiday_mod)
        Me.GroupBox1.Location = New System.Drawing.Point(10, 11)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(254, 407)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "用户自定义模式列表"
        '
        'tv__holiday_mod
        '
        Me.tv__holiday_mod.Location = New System.Drawing.Point(14, 20)
        Me.tv__holiday_mod.Name = "tv__holiday_mod"
        Me.tv__holiday_mod.Size = New System.Drawing.Size(227, 379)
        Me.tv__holiday_mod.TabIndex = 0
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.Controls.Add(Me.rtb_holiday_mod)
        Me.GroupBox2.Location = New System.Drawing.Point(279, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(358, 406)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "用户自定义模式"
        '
        'rtb_holiday_mod
        '
        Me.rtb_holiday_mod.Location = New System.Drawing.Point(11, 20)
        Me.rtb_holiday_mod.Name = "rtb_holiday_mod"
        Me.rtb_holiday_mod.Size = New System.Drawing.Size(336, 369)
        Me.rtb_holiday_mod.TabIndex = 0
        Me.rtb_holiday_mod.Text = ""
        '
        'bt_del_holiday_mod
        '
        Me.bt_del_holiday_mod.Location = New System.Drawing.Point(286, 424)
        Me.bt_del_holiday_mod.Name = "bt_del_holiday_mod"
        Me.bt_del_holiday_mod.Size = New System.Drawing.Size(75, 23)
        Me.bt_del_holiday_mod.TabIndex = 2
        Me.bt_del_holiday_mod.Text = "删除"
        Me.bt_del_holiday_mod.UseVisualStyleBackColor = True
        '
        '删除节日模式
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(647, 459)
        Me.Controls.Add(Me.bt_del_holiday_mod)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "删除节日模式"
        Me.Text = "删除用户自定义模式"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents tv__holiday_mod As System.Windows.Forms.TreeView
    Friend WithEvents rtb_holiday_mod As System.Windows.Forms.RichTextBox
    Friend WithEvents bt_del_holiday_mod As System.Windows.Forms.Button
End Class
