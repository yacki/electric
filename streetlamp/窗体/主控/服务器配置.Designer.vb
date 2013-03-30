<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 服务器配置
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
        Me.Label1 = New System.Windows.Forms.Label
        Me.rtb_text1 = New System.Windows.Forms.RichTextBox
        Me.rtb_text2 = New System.Windows.Forms.RichTextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.bt_set_server = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.tb_username = New System.Windows.Forms.TextBox
        Me.tb_password = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.bt_input = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(21, 97)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(83, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "连接字符串1："
        '
        'rtb_text1
        '
        Me.rtb_text1.Location = New System.Drawing.Point(103, 85)
        Me.rtb_text1.Name = "rtb_text1"
        Me.rtb_text1.Size = New System.Drawing.Size(406, 67)
        Me.rtb_text1.TabIndex = 1
        Me.rtb_text1.Text = ""
        '
        'rtb_text2
        '
        Me.rtb_text2.Location = New System.Drawing.Point(103, 173)
        Me.rtb_text2.Name = "rtb_text2"
        Me.rtb_text2.Size = New System.Drawing.Size(406, 67)
        Me.rtb_text2.TabIndex = 3
        Me.rtb_text2.Text = ""
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(23, 185)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(83, 12)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "连接字符串2："
        '
        'bt_set_server
        '
        Me.bt_set_server.Location = New System.Drawing.Point(232, 252)
        Me.bt_set_server.Name = "bt_set_server"
        Me.bt_set_server.Size = New System.Drawing.Size(64, 20)
        Me.bt_set_server.TabIndex = 4
        Me.bt_set_server.Text = "配置"
        Me.bt_set_server.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(44, 25)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 12)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "用户名："
        '
        'tb_username
        '
        Me.tb_username.Location = New System.Drawing.Point(103, 21)
        Me.tb_username.Name = "tb_username"
        Me.tb_username.Size = New System.Drawing.Size(86, 21)
        Me.tb_username.TabIndex = 6
        '
        'tb_password
        '
        Me.tb_password.Location = New System.Drawing.Point(279, 22)
        Me.tb_password.Name = "tb_password"
        Me.tb_password.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.tb_password.Size = New System.Drawing.Size(86, 21)
        Me.tb_password.TabIndex = 8
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(231, 25)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(41, 12)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "密码："
        '
        'bt_input
        '
        Me.bt_input.Location = New System.Drawing.Point(430, 21)
        Me.bt_input.Name = "bt_input"
        Me.bt_input.Size = New System.Drawing.Size(64, 20)
        Me.bt_input.TabIndex = 9
        Me.bt_input.Text = "输入"
        Me.bt_input.UseVisualStyleBackColor = True
        '
        '服务器配置
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(528, 282)
        Me.Controls.Add(Me.bt_input)
        Me.Controls.Add(Me.tb_password)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.tb_username)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.bt_set_server)
        Me.Controls.Add(Me.rtb_text2)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.rtb_text1)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Name = "服务器配置"
        Me.Text = "服务器配置"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents rtb_text1 As System.Windows.Forms.RichTextBox
    Friend WithEvents rtb_text2 As System.Windows.Forms.RichTextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents bt_set_server As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents tb_username As System.Windows.Forms.TextBox
    Friend WithEvents tb_password As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents bt_input As System.Windows.Forms.Button
End Class
