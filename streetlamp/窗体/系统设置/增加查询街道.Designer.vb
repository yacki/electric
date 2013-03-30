<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 增加查询街道
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(增加查询街道))
        Me.city_all_string = New System.Windows.Forms.Label
        Me.find_pos_string = New System.Windows.Forms.Label
        Me.lb_pos_x = New System.Windows.Forms.TextBox
        Me.pos_x_string = New System.Windows.Forms.Label
        Me.pos_y_string = New System.Windows.Forms.Label
        Me.lb_pos_y = New System.Windows.Forms.TextBox
        Me.bt_input_pos = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.cb_city_name = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.cb_area_name = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.tb_position = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'city_all_string
        '
        Me.city_all_string.AutoSize = True
        Me.city_all_string.BackColor = System.Drawing.Color.Transparent
        Me.city_all_string.Location = New System.Drawing.Point(22, 85)
        Me.city_all_string.Name = "city_all_string"
        Me.city_all_string.Size = New System.Drawing.Size(89, 12)
        Me.city_all_string.TabIndex = 110
        Me.city_all_string.Text = "定位区域名称："
        '
        'find_pos_string
        '
        Me.find_pos_string.AutoSize = True
        Me.find_pos_string.BackColor = System.Drawing.Color.Transparent
        Me.find_pos_string.Location = New System.Drawing.Point(237, 85)
        Me.find_pos_string.Name = "find_pos_string"
        Me.find_pos_string.Size = New System.Drawing.Size(65, 12)
        Me.find_pos_string.TabIndex = 111
        Me.find_pos_string.Text = "定位坐标："
        '
        'lb_pos_x
        '
        Me.lb_pos_x.Location = New System.Drawing.Point(338, 82)
        Me.lb_pos_x.Name = "lb_pos_x"
        Me.lb_pos_x.ReadOnly = True
        Me.lb_pos_x.Size = New System.Drawing.Size(31, 21)
        Me.lb_pos_x.TabIndex = 112
        '
        'pos_x_string
        '
        Me.pos_x_string.AutoSize = True
        Me.pos_x_string.BackColor = System.Drawing.Color.Transparent
        Me.pos_x_string.Location = New System.Drawing.Point(313, 85)
        Me.pos_x_string.Name = "pos_x_string"
        Me.pos_x_string.Size = New System.Drawing.Size(17, 12)
        Me.pos_x_string.TabIndex = 113
        Me.pos_x_string.Text = "X:"
        '
        'pos_y_string
        '
        Me.pos_y_string.AutoSize = True
        Me.pos_y_string.BackColor = System.Drawing.Color.Transparent
        Me.pos_y_string.Location = New System.Drawing.Point(374, 85)
        Me.pos_y_string.Name = "pos_y_string"
        Me.pos_y_string.Size = New System.Drawing.Size(17, 12)
        Me.pos_y_string.TabIndex = 115
        Me.pos_y_string.Text = "Y:"
        '
        'lb_pos_y
        '
        Me.lb_pos_y.Location = New System.Drawing.Point(397, 82)
        Me.lb_pos_y.Name = "lb_pos_y"
        Me.lb_pos_y.ReadOnly = True
        Me.lb_pos_y.Size = New System.Drawing.Size(30, 21)
        Me.lb_pos_y.TabIndex = 114
        '
        'bt_input_pos
        '
        Me.bt_input_pos.Location = New System.Drawing.Point(188, 131)
        Me.bt_input_pos.Name = "bt_input_pos"
        Me.bt_input_pos.Size = New System.Drawing.Size(75, 23)
        Me.bt_input_pos.TabIndex = 116
        Me.bt_input_pos.Text = "输入"
        Me.bt_input_pos.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label1.Location = New System.Drawing.Point(35, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(409, 12)
        Me.Label1.TabIndex = 117
        Me.Label1.Text = "(请在地图上将定位区域移动到中心位置后，双击地图后获得定位坐标)"
        '
        'cb_city_name
        '
        Me.cb_city_name.FormattingEnabled = True
        Me.cb_city_name.Location = New System.Drawing.Point(117, 51)
        Me.cb_city_name.Name = "cb_city_name"
        Me.cb_city_name.Size = New System.Drawing.Size(76, 20)
        Me.cb_city_name.TabIndex = 118
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(46, 53)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 12)
        Me.Label2.TabIndex = 119
        Me.Label2.Text = "城市名称："
        '
        'cb_area_name
        '
        Me.cb_area_name.FormattingEnabled = True
        Me.cb_area_name.Location = New System.Drawing.Point(309, 51)
        Me.cb_area_name.Name = "cb_area_name"
        Me.cb_area_name.Size = New System.Drawing.Size(76, 20)
        Me.cb_area_name.TabIndex = 120
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(249, 53)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 12)
        Me.Label3.TabIndex = 121
        Me.Label3.Text = "区名称："
        '
        'tb_position
        '
        Me.tb_position.Location = New System.Drawing.Point(117, 82)
        Me.tb_position.Name = "tb_position"
        Me.tb_position.Size = New System.Drawing.Size(76, 21)
        Me.tb_position.TabIndex = 124
        '
        '增加查询街道
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(470, 165)
        Me.Controls.Add(Me.tb_position)
        Me.Controls.Add(Me.cb_area_name)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cb_city_name)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.bt_input_pos)
        Me.Controls.Add(Me.pos_y_string)
        Me.Controls.Add(Me.lb_pos_y)
        Me.Controls.Add(Me.pos_x_string)
        Me.Controls.Add(Me.lb_pos_x)
        Me.Controls.Add(Me.find_pos_string)
        Me.Controls.Add(Me.city_all_string)
        Me.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "增加查询街道"
        Me.Text = "增加查询区域"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents city_all_string As System.Windows.Forms.Label
    Friend WithEvents find_pos_string As System.Windows.Forms.Label
    Friend WithEvents lb_pos_x As System.Windows.Forms.TextBox
    Friend WithEvents pos_x_string As System.Windows.Forms.Label
    Friend WithEvents pos_y_string As System.Windows.Forms.Label
    Friend WithEvents lb_pos_y As System.Windows.Forms.TextBox
    Friend WithEvents bt_input_pos As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cb_city_name As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cb_area_name As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents tb_position As System.Windows.Forms.TextBox
End Class
