<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 主控箱状态查询
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
        Me.rtb_control_box_state = New System.Windows.Forms.RichTextBox()
        Me.bt_getnew_state = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'rtb_control_box_state
        '
        Me.rtb_control_box_state.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rtb_control_box_state.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.rtb_control_box_state.Location = New System.Drawing.Point(8, 11)
        Me.rtb_control_box_state.Name = "rtb_control_box_state"
        Me.rtb_control_box_state.Size = New System.Drawing.Size(642, 378)
        Me.rtb_control_box_state.TabIndex = 0
        Me.rtb_control_box_state.Text = ""
        '
        'bt_getnew_state
        '
        Me.bt_getnew_state.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.bt_getnew_state.Location = New System.Drawing.Point(283, 412)
        Me.bt_getnew_state.Name = "bt_getnew_state"
        Me.bt_getnew_state.Size = New System.Drawing.Size(75, 23)
        Me.bt_getnew_state.TabIndex = 1
        Me.bt_getnew_state.Text = "刷新"
        Me.bt_getnew_state.UseVisualStyleBackColor = True
        '
        '主控箱状态查询
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(662, 445)
        Me.Controls.Add(Me.bt_getnew_state)
        Me.Controls.Add(Me.rtb_control_box_state)
        Me.Name = "主控箱状态查询"
        Me.Text = "网关状态查询"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents rtb_control_box_state As System.Windows.Forms.RichTextBox
    Friend WithEvents bt_getnew_state As System.Windows.Forms.Button
End Class
