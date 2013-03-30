<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 删除地图
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(删除地图))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cb_delmaplist = New System.Windows.Forms.ComboBox()
        Me.delmapname = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label1.Location = New System.Drawing.Point(32, 41)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(83, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "地图名称列表"
        '
        'cb_delmaplist
        '
        Me.cb_delmaplist.FormattingEnabled = True
        Me.cb_delmaplist.Location = New System.Drawing.Point(140, 38)
        Me.cb_delmaplist.Name = "cb_delmaplist"
        Me.cb_delmaplist.Size = New System.Drawing.Size(121, 20)
        Me.cb_delmaplist.TabIndex = 1
        '
        'delmapname
        '
        Me.delmapname.Location = New System.Drawing.Point(144, 110)
        Me.delmapname.Name = "delmapname"
        Me.delmapname.Size = New System.Drawing.Size(75, 23)
        Me.delmapname.TabIndex = 2
        Me.delmapname.Text = "删除"
        Me.delmapname.UseVisualStyleBackColor = True
        '
        '删除地图
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(362, 171)
        Me.Controls.Add(Me.delmapname)
        Me.Controls.Add(Me.cb_delmaplist)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "删除地图"
        Me.Text = "删除地图"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cb_delmaplist As System.Windows.Forms.ComboBox
    Friend WithEvents delmapname As System.Windows.Forms.Button
End Class
