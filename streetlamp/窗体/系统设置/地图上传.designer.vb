<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 地图上传
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(地图上传))
        Me.map_path_string = New System.Windows.Forms.Label()
        Me.tb_map_path = New System.Windows.Forms.TextBox()
        Me.bt_file_path_find = New System.Windows.Forms.Button()
        Me.bt_save_map = New System.Windows.Forms.Button()
        Me.find_map = New System.Windows.Forms.OpenFileDialog()
        Me.save_file_name_string = New System.Windows.Forms.Label()
        Me.tb_save_file_name = New System.Windows.Forms.TextBox()
        Me.area_map_string = New System.Windows.Forms.Label()
        Me.cb_area_map = New System.Windows.Forms.ComboBox()
        Me.bt_file_smallpath_find = New System.Windows.Forms.Button()
        Me.tb_smallmap_path = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'map_path_string
        '
        Me.map_path_string.AutoSize = True
        Me.map_path_string.BackColor = System.Drawing.Color.Transparent
        Me.map_path_string.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.map_path_string.Location = New System.Drawing.Point(27, 33)
        Me.map_path_string.Name = "map_path_string"
        Me.map_path_string.Size = New System.Drawing.Size(96, 12)
        Me.map_path_string.TabIndex = 0
        Me.map_path_string.Text = "地图文件路径："
        '
        'tb_map_path
        '
        Me.tb_map_path.Location = New System.Drawing.Point(157, 30)
        Me.tb_map_path.Name = "tb_map_path"
        Me.tb_map_path.Size = New System.Drawing.Size(196, 21)
        Me.tb_map_path.TabIndex = 1
        '
        'bt_file_path_find
        '
        Me.bt_file_path_find.Location = New System.Drawing.Point(375, 28)
        Me.bt_file_path_find.Name = "bt_file_path_find"
        Me.bt_file_path_find.Size = New System.Drawing.Size(48, 23)
        Me.bt_file_path_find.TabIndex = 2
        Me.bt_file_path_find.Text = "浏览"
        Me.bt_file_path_find.UseVisualStyleBackColor = True
        '
        'bt_save_map
        '
        Me.bt_save_map.Location = New System.Drawing.Point(182, 216)
        Me.bt_save_map.Name = "bt_save_map"
        Me.bt_save_map.Size = New System.Drawing.Size(79, 25)
        Me.bt_save_map.TabIndex = 3
        Me.bt_save_map.Text = "上传"
        Me.bt_save_map.UseVisualStyleBackColor = True
        '
        'find_map
        '
        Me.find_map.FileName = "OpenFileDialog1"
        '
        'save_file_name_string
        '
        Me.save_file_name_string.AutoSize = True
        Me.save_file_name_string.BackColor = System.Drawing.Color.Transparent
        Me.save_file_name_string.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.save_file_name_string.Location = New System.Drawing.Point(40, 116)
        Me.save_file_name_string.Name = "save_file_name_string"
        Me.save_file_name_string.Size = New System.Drawing.Size(83, 12)
        Me.save_file_name_string.TabIndex = 4
        Me.save_file_name_string.Text = "保存文件名："
        '
        'tb_save_file_name
        '
        Me.tb_save_file_name.Location = New System.Drawing.Point(157, 115)
        Me.tb_save_file_name.Name = "tb_save_file_name"
        Me.tb_save_file_name.Size = New System.Drawing.Size(196, 21)
        Me.tb_save_file_name.TabIndex = 5
        '
        'area_map_string
        '
        Me.area_map_string.AutoSize = True
        Me.area_map_string.BackColor = System.Drawing.Color.Transparent
        Me.area_map_string.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.area_map_string.Location = New System.Drawing.Point(27, 155)
        Me.area_map_string.Name = "area_map_string"
        Me.area_map_string.Size = New System.Drawing.Size(96, 12)
        Me.area_map_string.TabIndex = 6
        Me.area_map_string.Text = "地图所属区域："
        '
        'cb_area_map
        '
        Me.cb_area_map.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_area_map.FormattingEnabled = True
        Me.cb_area_map.Location = New System.Drawing.Point(157, 155)
        Me.cb_area_map.Name = "cb_area_map"
        Me.cb_area_map.Size = New System.Drawing.Size(196, 20)
        Me.cb_area_map.TabIndex = 7
        '
        'bt_file_smallpath_find
        '
        Me.bt_file_smallpath_find.Location = New System.Drawing.Point(375, 69)
        Me.bt_file_smallpath_find.Name = "bt_file_smallpath_find"
        Me.bt_file_smallpath_find.Size = New System.Drawing.Size(48, 23)
        Me.bt_file_smallpath_find.TabIndex = 10
        Me.bt_file_smallpath_find.Text = "浏览"
        Me.bt_file_smallpath_find.UseVisualStyleBackColor = True
        '
        'tb_smallmap_path
        '
        Me.tb_smallmap_path.Location = New System.Drawing.Point(157, 71)
        Me.tb_smallmap_path.Name = "tb_smallmap_path"
        Me.tb_smallmap_path.Size = New System.Drawing.Size(196, 21)
        Me.tb_smallmap_path.TabIndex = 9
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label1.Location = New System.Drawing.Point(14, 74)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(109, 12)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "鹰眼图文件路径："
        '
        '地图上传
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(450, 253)
        Me.Controls.Add(Me.bt_file_smallpath_find)
        Me.Controls.Add(Me.tb_smallmap_path)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cb_area_map)
        Me.Controls.Add(Me.area_map_string)
        Me.Controls.Add(Me.tb_save_file_name)
        Me.Controls.Add(Me.save_file_name_string)
        Me.Controls.Add(Me.bt_save_map)
        Me.Controls.Add(Me.bt_file_path_find)
        Me.Controls.Add(Me.tb_map_path)
        Me.Controls.Add(Me.map_path_string)
        Me.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "地图上传"
        Me.Text = "地图上传"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents map_path_string As System.Windows.Forms.Label
    Friend WithEvents tb_map_path As System.Windows.Forms.TextBox
    Friend WithEvents bt_file_path_find As System.Windows.Forms.Button
    Friend WithEvents bt_save_map As System.Windows.Forms.Button
    Friend WithEvents find_map As System.Windows.Forms.OpenFileDialog
    Friend WithEvents save_file_name_string As System.Windows.Forms.Label
    Friend WithEvents tb_save_file_name As System.Windows.Forms.TextBox
    Friend WithEvents area_map_string As System.Windows.Forms.Label
    Friend WithEvents cb_area_map As System.Windows.Forms.ComboBox
    Friend WithEvents bt_file_smallpath_find As System.Windows.Forms.Button
    Friend WithEvents tb_smallmap_path As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
