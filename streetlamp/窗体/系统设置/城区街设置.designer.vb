<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 城区街设置
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(城区街设置))
        Me.增加 = New System.Windows.Forms.GroupBox()
        Me.cb_area_name2 = New System.Windows.Forms.ComboBox()
        Me.rb_city_add = New System.Windows.Forms.RadioButton()
        Me.cb_city_name2 = New System.Windows.Forms.ComboBox()
        Me.bt_add_inf = New System.Windows.Forms.Button()
        Me.lb_street_name = New System.Windows.Forms.TextBox()
        Me.street_name_string = New System.Windows.Forms.Label()
        Me.area_name_string = New System.Windows.Forms.Label()
        Me.lb_area_name = New System.Windows.Forms.TextBox()
        Me.lb_city_name = New System.Windows.Forms.TextBox()
        Me.city_name_string = New System.Windows.Forms.Label()
        Me.rb_street_add = New System.Windows.Forms.RadioButton()
        Me.rb_area_add = New System.Windows.Forms.RadioButton()
        Me.bt_delete_inf = New System.Windows.Forms.Button()
        Me.delete_street_string = New System.Windows.Forms.Label()
        Me.delete_area_string = New System.Windows.Forms.Label()
        Me.delete_city_string = New System.Windows.Forms.Label()
        Me.rb_delete_street_control = New System.Windows.Forms.RadioButton()
        Me.rb_delete_area_control = New System.Windows.Forms.RadioButton()
        Me.rb_delete_city_control = New System.Windows.Forms.RadioButton()
        Me.cb_city_delete = New System.Windows.Forms.ComboBox()
        Me.cb_area_delete = New System.Windows.Forms.ComboBox()
        Me.cb_street_delete = New System.Windows.Forms.ComboBox()
        Me.Broupbox = New System.Windows.Forms.GroupBox()
        Me.增加.SuspendLayout()
        Me.Broupbox.SuspendLayout()
        Me.SuspendLayout()
        '
        '增加
        '
        Me.增加.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.增加.BackColor = System.Drawing.Color.Transparent
        Me.增加.Controls.Add(Me.cb_area_name2)
        Me.增加.Controls.Add(Me.rb_city_add)
        Me.增加.Controls.Add(Me.cb_city_name2)
        Me.增加.Controls.Add(Me.bt_add_inf)
        Me.增加.Controls.Add(Me.lb_street_name)
        Me.增加.Controls.Add(Me.street_name_string)
        Me.增加.Controls.Add(Me.area_name_string)
        Me.增加.Controls.Add(Me.lb_area_name)
        Me.增加.Controls.Add(Me.lb_city_name)
        Me.增加.Controls.Add(Me.city_name_string)
        Me.增加.Controls.Add(Me.rb_street_add)
        Me.增加.Controls.Add(Me.rb_area_add)
        Me.增加.Location = New System.Drawing.Point(12, 2)
        Me.增加.Name = "增加"
        Me.增加.Size = New System.Drawing.Size(785, 146)
        Me.增加.TabIndex = 11
        Me.增加.TabStop = False
        Me.增加.Text = "增加城区街"
        '
        'cb_area_name2
        '
        Me.cb_area_name2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_area_name2.FormattingEnabled = True
        Me.cb_area_name2.Location = New System.Drawing.Point(298, 86)
        Me.cb_area_name2.Name = "cb_area_name2"
        Me.cb_area_name2.Size = New System.Drawing.Size(100, 20)
        Me.cb_area_name2.TabIndex = 26
        '
        'rb_city_add
        '
        Me.rb_city_add.AutoSize = True
        Me.rb_city_add.BackColor = System.Drawing.Color.Transparent
        Me.rb_city_add.Location = New System.Drawing.Point(37, 41)
        Me.rb_city_add.Name = "rb_city_add"
        Me.rb_city_add.Size = New System.Drawing.Size(71, 16)
        Me.rb_city_add.TabIndex = 20
        Me.rb_city_add.Text = "增加城市"
        Me.rb_city_add.UseVisualStyleBackColor = False
        '
        'cb_city_name2
        '
        Me.cb_city_name2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_city_name2.FormattingEnabled = True
        Me.cb_city_name2.Location = New System.Drawing.Point(94, 86)
        Me.cb_city_name2.Name = "cb_city_name2"
        Me.cb_city_name2.Size = New System.Drawing.Size(100, 20)
        Me.cb_city_name2.TabIndex = 25
        '
        'bt_add_inf
        '
        Me.bt_add_inf.Location = New System.Drawing.Point(675, 83)
        Me.bt_add_inf.Name = "bt_add_inf"
        Me.bt_add_inf.Size = New System.Drawing.Size(75, 23)
        Me.bt_add_inf.TabIndex = 19
        Me.bt_add_inf.Text = "增加"
        Me.bt_add_inf.UseVisualStyleBackColor = True
        '
        'lb_street_name
        '
        Me.lb_street_name.ImeMode = System.Windows.Forms.ImeMode.[On]
        Me.lb_street_name.Location = New System.Drawing.Point(501, 85)
        Me.lb_street_name.Name = "lb_street_name"
        Me.lb_street_name.Size = New System.Drawing.Size(100, 21)
        Me.lb_street_name.TabIndex = 18
        '
        'street_name_string
        '
        Me.street_name_string.AutoSize = True
        Me.street_name_string.BackColor = System.Drawing.Color.Transparent
        Me.street_name_string.Location = New System.Drawing.Point(430, 88)
        Me.street_name_string.Name = "street_name_string"
        Me.street_name_string.Size = New System.Drawing.Size(65, 12)
        Me.street_name_string.TabIndex = 17
        Me.street_name_string.Text = "街道名称："
        '
        'area_name_string
        '
        Me.area_name_string.AutoSize = True
        Me.area_name_string.BackColor = System.Drawing.Color.Transparent
        Me.area_name_string.Location = New System.Drawing.Point(227, 88)
        Me.area_name_string.Name = "area_name_string"
        Me.area_name_string.Size = New System.Drawing.Size(65, 12)
        Me.area_name_string.TabIndex = 16
        Me.area_name_string.Text = "区域名称："
        '
        'lb_area_name
        '
        Me.lb_area_name.ImeMode = System.Windows.Forms.ImeMode.[On]
        Me.lb_area_name.Location = New System.Drawing.Point(298, 85)
        Me.lb_area_name.Name = "lb_area_name"
        Me.lb_area_name.Size = New System.Drawing.Size(100, 21)
        Me.lb_area_name.TabIndex = 15
        '
        'lb_city_name
        '
        Me.lb_city_name.ImeMode = System.Windows.Forms.ImeMode.[On]
        Me.lb_city_name.Location = New System.Drawing.Point(94, 85)
        Me.lb_city_name.Name = "lb_city_name"
        Me.lb_city_name.Size = New System.Drawing.Size(100, 21)
        Me.lb_city_name.TabIndex = 14
        '
        'city_name_string
        '
        Me.city_name_string.AutoSize = True
        Me.city_name_string.BackColor = System.Drawing.Color.Transparent
        Me.city_name_string.Location = New System.Drawing.Point(26, 88)
        Me.city_name_string.Name = "city_name_string"
        Me.city_name_string.Size = New System.Drawing.Size(65, 12)
        Me.city_name_string.TabIndex = 13
        Me.city_name_string.Text = "城市名称："
        '
        'rb_street_add
        '
        Me.rb_street_add.AutoSize = True
        Me.rb_street_add.BackColor = System.Drawing.Color.Transparent
        Me.rb_street_add.Checked = True
        Me.rb_street_add.Location = New System.Drawing.Point(275, 41)
        Me.rb_street_add.Name = "rb_street_add"
        Me.rb_street_add.Size = New System.Drawing.Size(71, 16)
        Me.rb_street_add.TabIndex = 12
        Me.rb_street_add.TabStop = True
        Me.rb_street_add.Text = "增加街道"
        Me.rb_street_add.UseVisualStyleBackColor = False
        '
        'rb_area_add
        '
        Me.rb_area_add.AutoSize = True
        Me.rb_area_add.BackColor = System.Drawing.Color.Transparent
        Me.rb_area_add.Location = New System.Drawing.Point(152, 41)
        Me.rb_area_add.Name = "rb_area_add"
        Me.rb_area_add.Size = New System.Drawing.Size(71, 16)
        Me.rb_area_add.TabIndex = 11
        Me.rb_area_add.Text = "增加区域"
        Me.rb_area_add.UseVisualStyleBackColor = False
        '
        'bt_delete_inf
        '
        Me.bt_delete_inf.Location = New System.Drawing.Point(675, 73)
        Me.bt_delete_inf.Name = "bt_delete_inf"
        Me.bt_delete_inf.Size = New System.Drawing.Size(75, 23)
        Me.bt_delete_inf.TabIndex = 20
        Me.bt_delete_inf.Text = "删除"
        Me.bt_delete_inf.UseVisualStyleBackColor = True
        '
        'delete_street_string
        '
        Me.delete_street_string.AutoSize = True
        Me.delete_street_string.BackColor = System.Drawing.Color.Transparent
        Me.delete_street_string.Location = New System.Drawing.Point(430, 77)
        Me.delete_street_string.Name = "delete_street_string"
        Me.delete_street_string.Size = New System.Drawing.Size(65, 12)
        Me.delete_street_string.TabIndex = 18
        Me.delete_street_string.Text = "街道名称："
        '
        'delete_area_string
        '
        Me.delete_area_string.AutoSize = True
        Me.delete_area_string.BackColor = System.Drawing.Color.Transparent
        Me.delete_area_string.Location = New System.Drawing.Point(227, 78)
        Me.delete_area_string.Name = "delete_area_string"
        Me.delete_area_string.Size = New System.Drawing.Size(65, 12)
        Me.delete_area_string.TabIndex = 17
        Me.delete_area_string.Text = "区域名称："
        '
        'delete_city_string
        '
        Me.delete_city_string.AutoSize = True
        Me.delete_city_string.BackColor = System.Drawing.Color.Transparent
        Me.delete_city_string.Location = New System.Drawing.Point(14, 78)
        Me.delete_city_string.Name = "delete_city_string"
        Me.delete_city_string.Size = New System.Drawing.Size(65, 12)
        Me.delete_city_string.TabIndex = 14
        Me.delete_city_string.Text = "城市名称："
        '
        'rb_delete_street_control
        '
        Me.rb_delete_street_control.AutoSize = True
        Me.rb_delete_street_control.BackColor = System.Drawing.Color.Transparent
        Me.rb_delete_street_control.Checked = True
        Me.rb_delete_street_control.Location = New System.Drawing.Point(275, 34)
        Me.rb_delete_street_control.Name = "rb_delete_street_control"
        Me.rb_delete_street_control.Size = New System.Drawing.Size(71, 16)
        Me.rb_delete_street_control.TabIndex = 13
        Me.rb_delete_street_control.TabStop = True
        Me.rb_delete_street_control.Text = "删除街道"
        Me.rb_delete_street_control.UseVisualStyleBackColor = False
        '
        'rb_delete_area_control
        '
        Me.rb_delete_area_control.AutoSize = True
        Me.rb_delete_area_control.BackColor = System.Drawing.Color.Transparent
        Me.rb_delete_area_control.Location = New System.Drawing.Point(157, 34)
        Me.rb_delete_area_control.Name = "rb_delete_area_control"
        Me.rb_delete_area_control.Size = New System.Drawing.Size(71, 16)
        Me.rb_delete_area_control.TabIndex = 12
        Me.rb_delete_area_control.Text = "删除区域"
        Me.rb_delete_area_control.UseVisualStyleBackColor = False
        '
        'rb_delete_city_control
        '
        Me.rb_delete_city_control.AutoSize = True
        Me.rb_delete_city_control.BackColor = System.Drawing.Color.Transparent
        Me.rb_delete_city_control.Location = New System.Drawing.Point(37, 34)
        Me.rb_delete_city_control.Name = "rb_delete_city_control"
        Me.rb_delete_city_control.Size = New System.Drawing.Size(71, 16)
        Me.rb_delete_city_control.TabIndex = 21
        Me.rb_delete_city_control.Text = "删除城市"
        Me.rb_delete_city_control.UseVisualStyleBackColor = False
        '
        'cb_city_delete
        '
        Me.cb_city_delete.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_city_delete.FormattingEnabled = True
        Me.cb_city_delete.Location = New System.Drawing.Point(85, 75)
        Me.cb_city_delete.Name = "cb_city_delete"
        Me.cb_city_delete.Size = New System.Drawing.Size(100, 20)
        Me.cb_city_delete.TabIndex = 22
        '
        'cb_area_delete
        '
        Me.cb_area_delete.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_area_delete.FormattingEnabled = True
        Me.cb_area_delete.Location = New System.Drawing.Point(298, 75)
        Me.cb_area_delete.Name = "cb_area_delete"
        Me.cb_area_delete.Size = New System.Drawing.Size(100, 20)
        Me.cb_area_delete.TabIndex = 23
        '
        'cb_street_delete
        '
        Me.cb_street_delete.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_street_delete.FormattingEnabled = True
        Me.cb_street_delete.Location = New System.Drawing.Point(501, 75)
        Me.cb_street_delete.Name = "cb_street_delete"
        Me.cb_street_delete.Size = New System.Drawing.Size(100, 20)
        Me.cb_street_delete.TabIndex = 24
        '
        'Broupbox
        '
        Me.Broupbox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Broupbox.BackColor = System.Drawing.Color.Transparent
        Me.Broupbox.Controls.Add(Me.rb_delete_city_control)
        Me.Broupbox.Controls.Add(Me.cb_street_delete)
        Me.Broupbox.Controls.Add(Me.cb_area_delete)
        Me.Broupbox.Controls.Add(Me.rb_delete_area_control)
        Me.Broupbox.Controls.Add(Me.cb_city_delete)
        Me.Broupbox.Controls.Add(Me.rb_delete_street_control)
        Me.Broupbox.Controls.Add(Me.bt_delete_inf)
        Me.Broupbox.Controls.Add(Me.delete_city_string)
        Me.Broupbox.Controls.Add(Me.delete_street_string)
        Me.Broupbox.Controls.Add(Me.delete_area_string)
        Me.Broupbox.Location = New System.Drawing.Point(12, 170)
        Me.Broupbox.Name = "Broupbox"
        Me.Broupbox.Size = New System.Drawing.Size(785, 135)
        Me.Broupbox.TabIndex = 25
        Me.Broupbox.TabStop = False
        Me.Broupbox.Text = "删除城区街"
        '
        '城区街设置
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(806, 317)
        Me.Controls.Add(Me.Broupbox)
        Me.Controls.Add(Me.增加)
        Me.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.ImeMode = System.Windows.Forms.ImeMode.[On]
        Me.Name = "城区街设置"
        Me.Text = "城区街设置"
        Me.增加.ResumeLayout(False)
        Me.增加.PerformLayout()
        Me.Broupbox.ResumeLayout(False)
        Me.Broupbox.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents 增加 As System.Windows.Forms.GroupBox
    Friend WithEvents bt_delete_inf As System.Windows.Forms.Button
    Friend WithEvents delete_street_string As System.Windows.Forms.Label
    Friend WithEvents delete_area_string As System.Windows.Forms.Label
    Friend WithEvents delete_city_string As System.Windows.Forms.Label
    Friend WithEvents rb_delete_street_control As System.Windows.Forms.RadioButton
    Friend WithEvents rb_delete_area_control As System.Windows.Forms.RadioButton
    Friend WithEvents rb_delete_city_control As System.Windows.Forms.RadioButton
    Friend WithEvents cb_city_delete As System.Windows.Forms.ComboBox
    Friend WithEvents cb_area_delete As System.Windows.Forms.ComboBox
    Friend WithEvents cb_street_delete As System.Windows.Forms.ComboBox
    Friend WithEvents rb_city_add As System.Windows.Forms.RadioButton
    Friend WithEvents bt_add_inf As System.Windows.Forms.Button
    Friend WithEvents lb_street_name As System.Windows.Forms.TextBox
    Friend WithEvents street_name_string As System.Windows.Forms.Label
    Friend WithEvents area_name_string As System.Windows.Forms.Label
    Friend WithEvents lb_area_name As System.Windows.Forms.TextBox
    Friend WithEvents lb_city_name As System.Windows.Forms.TextBox
    Friend WithEvents city_name_string As System.Windows.Forms.Label
    Friend WithEvents rb_street_add As System.Windows.Forms.RadioButton
    Friend WithEvents rb_area_add As System.Windows.Forms.RadioButton
    Friend WithEvents cb_area_name2 As System.Windows.Forms.ComboBox
    Friend WithEvents cb_city_name2 As System.Windows.Forms.ComboBox
    Friend WithEvents Broupbox As System.Windows.Forms.GroupBox
End Class
