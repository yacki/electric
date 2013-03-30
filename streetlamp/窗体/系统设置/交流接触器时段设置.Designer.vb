<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 交流接触器时段设置
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(交流接触器时段设置))
        Me.jiechuqi_list = New System.Windows.Forms.CheckedListBox
        Me.Add_jiaoliu = New System.Windows.Forms.Button
        Me.jiechuqi_collection = New System.Windows.Forms.RichTextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.mod_name_list = New System.Windows.Forms.ComboBox
        Me.input_jiaoliu = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.时段设置 = New System.Windows.Forms.GroupBox
        Me.时段设置.SuspendLayout()
        Me.SuspendLayout()
        '
        'jiechuqi_list
        '
        Me.jiechuqi_list.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.jiechuqi_list.FormattingEnabled = True
        Me.jiechuqi_list.Location = New System.Drawing.Point(15, 15)
        Me.jiechuqi_list.Name = "jiechuqi_list"
        Me.jiechuqi_list.Size = New System.Drawing.Size(135, 228)
        Me.jiechuqi_list.TabIndex = 0
        '
        'Add_jiaoliu
        '
        Me.Add_jiaoliu.Location = New System.Drawing.Point(154, 116)
        Me.Add_jiaoliu.Name = "Add_jiaoliu"
        Me.Add_jiaoliu.Size = New System.Drawing.Size(64, 20)
        Me.Add_jiaoliu.TabIndex = 1
        Me.Add_jiaoliu.Text = "增加>>>>"
        Me.Add_jiaoliu.UseVisualStyleBackColor = True
        '
        'jiechuqi_collection
        '
        Me.jiechuqi_collection.Location = New System.Drawing.Point(231, 40)
        Me.jiechuqi_collection.Name = "jiechuqi_collection"
        Me.jiechuqi_collection.Size = New System.Drawing.Size(365, 165)
        Me.jiechuqi_collection.TabIndex = 2
        Me.jiechuqi_collection.Text = ""
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(236, 223)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 12)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "模式名称："
        '
        'mod_name_list
        '
        Me.mod_name_list.FormattingEnabled = True
        Me.mod_name_list.Location = New System.Drawing.Point(334, 220)
        Me.mod_name_list.Name = "mod_name_list"
        Me.mod_name_list.Size = New System.Drawing.Size(104, 20)
        Me.mod_name_list.TabIndex = 4
        '
        'input_jiaoliu
        '
        Me.input_jiaoliu.Location = New System.Drawing.Point(495, 219)
        Me.input_jiaoliu.Name = "input_jiaoliu"
        Me.input_jiaoliu.Size = New System.Drawing.Size(64, 20)
        Me.input_jiaoliu.TabIndex = 5
        Me.input_jiaoliu.Text = "输入模式"
        Me.input_jiaoliu.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label2.Location = New System.Drawing.Point(236, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(239, 12)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "被选中进行时段设定的主控箱节点编号："
        '
        '时段设置
        '
        Me.时段设置.Controls.Add(Me.input_jiaoliu)
        Me.时段设置.Controls.Add(Me.Label2)
        Me.时段设置.Controls.Add(Me.mod_name_list)
        Me.时段设置.Controls.Add(Me.Add_jiaoliu)
        Me.时段设置.Controls.Add(Me.jiechuqi_collection)
        Me.时段设置.Controls.Add(Me.Label1)
        Me.时段设置.Controls.Add(Me.jiechuqi_list)
        Me.时段设置.Location = New System.Drawing.Point(10, 10)
        Me.时段设置.Name = "时段设置"
        Me.时段设置.Size = New System.Drawing.Size(623, 260)
        Me.时段设置.TabIndex = 7
        Me.时段设置.TabStop = False
        Me.时段设置.Text = "时段设置"
        '
        '交流接触器时段设置
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(644, 283)
        Me.Controls.Add(Me.时段设置)
        Me.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "交流接触器时段设置"
        Me.Text = "增加主控箱时段设置"
        Me.时段设置.ResumeLayout(False)
        Me.时段设置.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents jiechuqi_list As System.Windows.Forms.CheckedListBox
    Friend WithEvents Add_jiaoliu As System.Windows.Forms.Button
    Friend WithEvents jiechuqi_collection As System.Windows.Forms.RichTextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents mod_name_list As System.Windows.Forms.ComboBox
    Friend WithEvents input_jiaoliu As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents 时段设置 As System.Windows.Forms.GroupBox
End Class
