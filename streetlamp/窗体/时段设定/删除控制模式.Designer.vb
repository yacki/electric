<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 删除控制模式
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(删除控制模式))
        Me.Div_infList = New System.Windows.Forms.TreeView
        Me.Label1 = New System.Windows.Forms.Label
        Me.mod_level_choose = New System.Windows.Forms.ComboBox
        Me.del_div_name = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Div_infList
        '
        Me.Div_infList.Location = New System.Drawing.Point(12, 12)
        Me.Div_infList.Name = "Div_infList"
        Me.Div_infList.Size = New System.Drawing.Size(424, 328)
        Me.Div_infList.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(24, 370)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(105, 14)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "控制模式名称："
        '
        'mod_level_choose
        '
        Me.mod_level_choose.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.mod_level_choose.FormattingEnabled = True
        Me.mod_level_choose.Items.AddRange(New Object() {"平时模式", "节日模式", "半夜模式", "全关模式"})
        Me.mod_level_choose.Location = New System.Drawing.Point(135, 366)
        Me.mod_level_choose.Name = "mod_level_choose"
        Me.mod_level_choose.Size = New System.Drawing.Size(138, 22)
        Me.mod_level_choose.TabIndex = 73
        '
        'del_div_name
        '
        Me.del_div_name.Location = New System.Drawing.Point(337, 366)
        Me.del_div_name.Name = "del_div_name"
        Me.del_div_name.Size = New System.Drawing.Size(75, 23)
        Me.del_div_name.TabIndex = 74
        Me.del_div_name.Text = "删除"
        Me.del_div_name.UseVisualStyleBackColor = True
        '
        '删除控制模式
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(453, 448)
        Me.Controls.Add(Me.del_div_name)
        Me.Controls.Add(Me.mod_level_choose)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Div_infList)
        Me.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "删除控制模式"
        Me.Text = "删除控制模式"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Div_infList As System.Windows.Forms.TreeView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents mod_level_choose As System.Windows.Forms.ComboBox
    Friend WithEvents del_div_name As System.Windows.Forms.Button
End Class
