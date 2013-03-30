<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 主控箱参数设置
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.set_current = New System.Windows.Forms.Button
        Me.current_bottom = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.current_top = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.groupbox = New System.Windows.Forms.GroupBox
        Me.set_presure = New System.Windows.Forms.Button
        Me.presure_bottom = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.presure_top = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.set_bianbi = New System.Windows.Forms.Button
        Me.bianbip = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.GroupBox1.SuspendLayout()
        Me.groupbox.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.set_current)
        Me.GroupBox1.Controls.Add(Me.current_bottom)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.current_top)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(10, 10)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(224, 173)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "电流阈值"
        '
        'set_current
        '
        Me.set_current.Location = New System.Drawing.Point(75, 129)
        Me.set_current.Name = "set_current"
        Me.set_current.Size = New System.Drawing.Size(64, 20)
        Me.set_current.TabIndex = 4
        Me.set_current.Text = "设置"
        Me.set_current.UseVisualStyleBackColor = True
        '
        'current_bottom
        '
        Me.current_bottom.Location = New System.Drawing.Point(76, 76)
        Me.current_bottom.Name = "current_bottom"
        Me.current_bottom.Size = New System.Drawing.Size(86, 21)
        Me.current_bottom.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(5, 79)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 12)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "电流下限："
        '
        'current_top
        '
        Me.current_top.Location = New System.Drawing.Point(76, 33)
        Me.current_top.Name = "current_top"
        Me.current_top.Size = New System.Drawing.Size(86, 21)
        Me.current_top.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(5, 36)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "电流上限："
        '
        'groupbox
        '
        Me.groupbox.Controls.Add(Me.set_presure)
        Me.groupbox.Controls.Add(Me.presure_bottom)
        Me.groupbox.Controls.Add(Me.Label3)
        Me.groupbox.Controls.Add(Me.presure_top)
        Me.groupbox.Controls.Add(Me.Label4)
        Me.groupbox.Location = New System.Drawing.Point(239, 10)
        Me.groupbox.Name = "groupbox"
        Me.groupbox.Size = New System.Drawing.Size(238, 173)
        Me.groupbox.TabIndex = 1
        Me.groupbox.TabStop = False
        Me.groupbox.Text = "电压阈值"
        '
        'set_presure
        '
        Me.set_presure.Location = New System.Drawing.Point(93, 124)
        Me.set_presure.Name = "set_presure"
        Me.set_presure.Size = New System.Drawing.Size(64, 20)
        Me.set_presure.TabIndex = 9
        Me.set_presure.Text = "设置"
        Me.set_presure.UseVisualStyleBackColor = True
        '
        'presure_bottom
        '
        Me.presure_bottom.Location = New System.Drawing.Point(95, 71)
        Me.presure_bottom.Name = "presure_bottom"
        Me.presure_bottom.Size = New System.Drawing.Size(86, 21)
        Me.presure_bottom.TabIndex = 8
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(24, 74)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 12)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "电压下限："
        '
        'presure_top
        '
        Me.presure_top.Location = New System.Drawing.Point(95, 28)
        Me.presure_top.Name = "presure_top"
        Me.presure_top.Size = New System.Drawing.Size(86, 21)
        Me.presure_top.TabIndex = 6
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(24, 31)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(65, 12)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "电压上限："
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.set_bianbi)
        Me.GroupBox2.Controls.Add(Me.bianbip)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Location = New System.Drawing.Point(10, 189)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(467, 90)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "互感器变比参数"
        '
        'set_bianbi
        '
        Me.set_bianbi.Location = New System.Drawing.Point(229, 42)
        Me.set_bianbi.Name = "set_bianbi"
        Me.set_bianbi.Size = New System.Drawing.Size(64, 20)
        Me.set_bianbi.TabIndex = 10
        Me.set_bianbi.Text = "设置"
        Me.set_bianbi.UseVisualStyleBackColor = True
        '
        'bianbip
        '
        Me.bianbip.Location = New System.Drawing.Point(97, 42)
        Me.bianbip.Name = "bianbip"
        Me.bianbip.Size = New System.Drawing.Size(86, 21)
        Me.bianbip.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(14, 45)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(77, 12)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "互感器变比："
        '
        '主控箱参数设置
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(486, 285)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.groupbox)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Name = "主控箱参数设置"
        Me.Text = "主控箱参数设置"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.groupbox.ResumeLayout(False)
        Me.groupbox.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents groupbox As System.Windows.Forms.GroupBox
    Friend WithEvents set_current As System.Windows.Forms.Button
    Friend WithEvents current_bottom As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents current_top As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents set_presure As System.Windows.Forms.Button
    Friend WithEvents presure_bottom As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents presure_top As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents bianbip As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents set_bianbi As System.Windows.Forms.Button
End Class
