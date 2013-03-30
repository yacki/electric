<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 路灯颜色编辑窗口
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(路灯颜色编辑窗口))
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog()
        Me.full_color_string = New System.Windows.Forms.Label()
        Me.pb_full_color = New System.Windows.Forms.PictureBox()
        Me.pb_problem_color = New System.Windows.Forms.PictureBox()
        Me.problem_color_string = New System.Windows.Forms.Label()
        Me.bt_edit_color = New System.Windows.Forms.Button()
        Me.ToolTip_color = New System.Windows.Forms.ToolTip(Me.components)
        Me.pb_close_color = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pb_part_color = New System.Windows.Forms.PictureBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.pb_par_color = New System.Windows.Forms.PictureBox()
        Me.Label3 = New System.Windows.Forms.Label()
        CType(Me.pb_full_color, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pb_problem_color, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pb_close_color, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pb_part_color, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pb_par_color, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'full_color_string
        '
        Me.full_color_string.AutoSize = True
        Me.full_color_string.BackColor = System.Drawing.Color.Transparent
        Me.full_color_string.Location = New System.Drawing.Point(79, 30)
        Me.full_color_string.Name = "full_color_string"
        Me.full_color_string.Size = New System.Drawing.Size(65, 12)
        Me.full_color_string.TabIndex = 0
        Me.full_color_string.Text = "亮灯标记："
        '
        'pb_full_color
        '
        Me.pb_full_color.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pb_full_color.Location = New System.Drawing.Point(150, 29)
        Me.pb_full_color.Name = "pb_full_color"
        Me.pb_full_color.Size = New System.Drawing.Size(20, 20)
        Me.pb_full_color.TabIndex = 1
        Me.pb_full_color.TabStop = False
        '
        'pb_problem_color
        '
        Me.pb_problem_color.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pb_problem_color.Location = New System.Drawing.Point(309, 29)
        Me.pb_problem_color.Name = "pb_problem_color"
        Me.pb_problem_color.Size = New System.Drawing.Size(20, 20)
        Me.pb_problem_color.TabIndex = 7
        Me.pb_problem_color.TabStop = False
        '
        'problem_color_string
        '
        Me.problem_color_string.AutoSize = True
        Me.problem_color_string.BackColor = System.Drawing.Color.Transparent
        Me.problem_color_string.Location = New System.Drawing.Point(237, 30)
        Me.problem_color_string.Name = "problem_color_string"
        Me.problem_color_string.Size = New System.Drawing.Size(65, 12)
        Me.problem_color_string.TabIndex = 6
        Me.problem_color_string.Text = "故障标记："
        '
        'bt_edit_color
        '
        Me.bt_edit_color.Location = New System.Drawing.Point(150, 128)
        Me.bt_edit_color.Name = "bt_edit_color"
        Me.bt_edit_color.Size = New System.Drawing.Size(87, 25)
        Me.bt_edit_color.TabIndex = 8
        Me.bt_edit_color.Text = "编辑"
        Me.bt_edit_color.UseVisualStyleBackColor = True
        '
        'pb_close_color
        '
        Me.pb_close_color.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pb_close_color.Location = New System.Drawing.Point(309, 69)
        Me.pb_close_color.Name = "pb_close_color"
        Me.pb_close_color.Size = New System.Drawing.Size(20, 20)
        Me.pb_close_color.TabIndex = 10
        Me.pb_close_color.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(238, 77)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 12)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "关灯标记："
        '
        'pb_part_color
        '
        Me.pb_part_color.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pb_part_color.Location = New System.Drawing.Point(150, 69)
        Me.pb_part_color.Name = "pb_part_color"
        Me.pb_part_color.Size = New System.Drawing.Size(20, 20)
        Me.pb_part_color.TabIndex = 12
        Me.pb_part_color.TabStop = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(55, 77)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(89, 12)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "无返回值标记："
        '
        'pb_par_color
        '
        Me.pb_par_color.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pb_par_color.Location = New System.Drawing.Point(276, 133)
        Me.pb_par_color.Name = "pb_par_color"
        Me.pb_par_color.Size = New System.Drawing.Size(20, 20)
        Me.pb_par_color.TabIndex = 14
        Me.pb_par_color.TabStop = False
        Me.pb_par_color.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(10, 133)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(113, 12)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "亮灯标记(半功率)："
        Me.Label3.Visible = False
        '
        '路灯颜色编辑窗口
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(384, 171)
        Me.Controls.Add(Me.pb_par_color)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.pb_part_color)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.pb_close_color)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.bt_edit_color)
        Me.Controls.Add(Me.pb_problem_color)
        Me.Controls.Add(Me.problem_color_string)
        Me.Controls.Add(Me.pb_full_color)
        Me.Controls.Add(Me.full_color_string)
        Me.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "路灯颜色编辑窗口"
        Me.Text = "路灯状态颜色编辑窗口"
        CType(Me.pb_full_color, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pb_problem_color, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pb_close_color, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pb_part_color, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pb_par_color, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ColorDialog1 As System.Windows.Forms.ColorDialog
    Friend WithEvents full_color_string As System.Windows.Forms.Label
    Friend WithEvents pb_full_color As System.Windows.Forms.PictureBox
    Friend WithEvents pb_problem_color As System.Windows.Forms.PictureBox
    Friend WithEvents problem_color_string As System.Windows.Forms.Label
    Friend WithEvents bt_edit_color As System.Windows.Forms.Button
    Friend WithEvents ToolTip_color As System.Windows.Forms.ToolTip
    Friend WithEvents pb_close_color As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents pb_part_color As System.Windows.Forms.PictureBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents pb_par_color As System.Windows.Forms.PictureBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
