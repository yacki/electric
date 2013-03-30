<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class m_manager
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(m_manager))
        Me.Manager = New streetlamp.manager()
        Me.manage = New System.Windows.Forms.BindingSource(Me.components)
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.tv_user_tree = New System.Windows.Forms.TreeView()
        Me.user_list = New System.Windows.Forms.ImageList(Me.components)
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.cb_right_user = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lb_password = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lb_user_name = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.bt_add = New System.Windows.Forms.Button()
        Me.bt_modif = New System.Windows.Forms.Button()
        Me.bt_delete = New System.Windows.Forms.Button()
        CType(Me.Manager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.manage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Manager
        '
        Me.Manager.DataSetName = "manager"
        Me.Manager.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'manage
        '
        Me.manage.DataSource = Me.Manager
        Me.manage.Position = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.tv_user_tree)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 21)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(144, 224)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'tv_user_tree
        '
        Me.tv_user_tree.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.tv_user_tree.Location = New System.Drawing.Point(6, 19)
        Me.tv_user_tree.Name = "tv_user_tree"
        Me.tv_user_tree.Size = New System.Drawing.Size(128, 199)
        Me.tv_user_tree.StateImageList = Me.user_list
        Me.tv_user_tree.TabIndex = 0
        '
        'user_list
        '
        Me.user_list.ImageStream = CType(resources.GetObject("user_list.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.user_list.TransparentColor = System.Drawing.Color.Transparent
        Me.user_list.Images.SetKeyName(0, "touxiang.jpeg")
        Me.user_list.Images.SetKeyName(1, "party-new.png")
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.Controls.Add(Me.PictureBox1)
        Me.GroupBox2.Controls.Add(Me.cb_right_user)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.lb_password)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.lb_user_name)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Location = New System.Drawing.Point(164, 21)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(329, 224)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(6, 19)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(124, 133)
        Me.PictureBox1.TabIndex = 6
        Me.PictureBox1.TabStop = False
        '
        'cb_right_user
        '
        Me.cb_right_user.FormattingEnabled = True
        Me.cb_right_user.Items.AddRange(New Object() {"管理员", "普通用户"})
        Me.cb_right_user.Location = New System.Drawing.Point(218, 168)
        Me.cb_right_user.Name = "cb_right_user"
        Me.cb_right_user.Size = New System.Drawing.Size(100, 20)
        Me.cb_right_user.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(160, 171)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(35, 12)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "岗位:"
        '
        'lb_password
        '
        Me.lb_password.Location = New System.Drawing.Point(218, 121)
        Me.lb_password.Name = "lb_password"
        Me.lb_password.Size = New System.Drawing.Size(100, 21)
        Me.lb_password.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(160, 130)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(35, 12)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "密码:"
        '
        'lb_user_name
        '
        Me.lb_user_name.Location = New System.Drawing.Point(218, 77)
        Me.lb_user_name.Name = "lb_user_name"
        Me.lb_user_name.Size = New System.Drawing.Size(100, 21)
        Me.lb_user_name.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(148, 86)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(47, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "用户名:"
        '
        'bt_add
        '
        Me.bt_add.Location = New System.Drawing.Point(50, 275)
        Me.bt_add.Name = "bt_add"
        Me.bt_add.Size = New System.Drawing.Size(75, 23)
        Me.bt_add.TabIndex = 8
        Me.bt_add.Text = "添加用户"
        Me.bt_add.UseVisualStyleBackColor = True
        '
        'bt_modif
        '
        Me.bt_modif.Location = New System.Drawing.Point(219, 275)
        Me.bt_modif.Name = "bt_modif"
        Me.bt_modif.Size = New System.Drawing.Size(75, 23)
        Me.bt_modif.TabIndex = 9
        Me.bt_modif.Text = "保存修改"
        Me.bt_modif.UseVisualStyleBackColor = True
        '
        'bt_delete
        '
        Me.bt_delete.Location = New System.Drawing.Point(382, 275)
        Me.bt_delete.Name = "bt_delete"
        Me.bt_delete.Size = New System.Drawing.Size(75, 23)
        Me.bt_delete.TabIndex = 10
        Me.bt_delete.Text = "删除用户"
        Me.bt_delete.UseVisualStyleBackColor = True
        '
        'm_manager
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(505, 316)
        Me.Controls.Add(Me.bt_delete)
        Me.Controls.Add(Me.bt_modif)
        Me.Controls.Add(Me.bt_add)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "m_manager"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "用户信息管理"
        Me.TopMost = True
        CType(Me.Manager, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.manage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Manager As streetlamp.manager
    Friend WithEvents manage As System.Windows.Forms.BindingSource
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents lb_password As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lb_user_name As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents tv_user_tree As System.Windows.Forms.TreeView
    Friend WithEvents user_list As System.Windows.Forms.ImageList
    Friend WithEvents cb_right_user As System.Windows.Forms.ComboBox
    Friend WithEvents bt_add As System.Windows.Forms.Button
    Friend WithEvents bt_modif As System.Windows.Forms.Button
    Friend WithEvents bt_delete As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
End Class
