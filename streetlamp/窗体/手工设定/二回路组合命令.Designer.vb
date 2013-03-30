<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 二回路组合命令
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.gpb_denggan2_group = New System.Windows.Forms.GroupBox()
        Me.chb_lamp_2_2 = New System.Windows.Forms.CheckBox()
        Me.chb_lamp_2_1 = New System.Windows.Forms.CheckBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.chb_lamp_1_2 = New System.Windows.Forms.CheckBox()
        Me.chb_lamp_1_1 = New System.Windows.Forms.CheckBox()
        Me.gpb_denggan1_group = New System.Windows.Forms.GroupBox()
        Me.GroupBox12 = New System.Windows.Forms.GroupBox()
        Me.rb_del_order = New System.Windows.Forms.RadioButton()
        Me.rb_add_order = New System.Windows.Forms.RadioButton()
        Me.chb_lamp_4_2 = New System.Windows.Forms.CheckBox()
        Me.chb_lamp_4_1 = New System.Windows.Forms.CheckBox()
        Me.gpb_denggan3_group = New System.Windows.Forms.GroupBox()
        Me.chb_lamp_3_2 = New System.Windows.Forms.CheckBox()
        Me.chb_lamp_3_1 = New System.Windows.Forms.CheckBox()
        Me.gpb_denggan4_group = New System.Windows.Forms.GroupBox()
        Me.chb_denggan_4 = New System.Windows.Forms.CheckBox()
        Me.GBMethod = New System.Windows.Forms.GroupBox()
        Me.chb_denggan_3 = New System.Windows.Forms.CheckBox()
        Me.chb_denggan_2 = New System.Windows.Forms.CheckBox()
        Me.chb_denggan_1 = New System.Windows.Forms.CheckBox()
        Me.cb_order_list = New System.Windows.Forms.ComboBox()
        Me.lb_grouporder_name = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.bt_send_order = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.pb_lamp1 = New System.Windows.Forms.PictureBox()
        Me.pb_lamp2 = New System.Windows.Forms.PictureBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.pb_lamp5 = New System.Windows.Forms.PictureBox()
        Me.pb_lamp6 = New System.Windows.Forms.PictureBox()
        Me.pb_lamp4 = New System.Windows.Forms.PictureBox()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.pb_lamp3 = New System.Windows.Forms.PictureBox()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.pb_lamp7 = New System.Windows.Forms.PictureBox()
        Me.pb_lamp8 = New System.Windows.Forms.PictureBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.gpb_denggan2_group.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.gpb_denggan1_group.SuspendLayout()
        Me.GroupBox12.SuspendLayout()
        Me.gpb_denggan3_group.SuspendLayout()
        Me.gpb_denggan4_group.SuspendLayout()
        Me.GBMethod.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.pb_lamp1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pb_lamp2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox5.SuspendLayout()
        CType(Me.pb_lamp5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pb_lamp6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pb_lamp4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox6.SuspendLayout()
        CType(Me.pb_lamp3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox7.SuspendLayout()
        CType(Me.pb_lamp7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pb_lamp8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label1.Location = New System.Drawing.Point(5, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(330, 120)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "二回路组合命令由两部分组成：灯杆控制方式和灯头控制" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "方式。" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "灯杆控制方式每四个灯杆为一个控制单元，用户可以在这" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "个控制单元中任意组合需要开灯的灯杆号。" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "灯头控制方式每二个灯头为一个控制单云，用户可以在这" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "个控制单元中任意组合需要亮灯的灯头号。" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "两种控制方式组合成一条控制命令发送。" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'gpb_denggan2_group
        '
        Me.gpb_denggan2_group.Controls.Add(Me.chb_lamp_2_2)
        Me.gpb_denggan2_group.Controls.Add(Me.chb_lamp_2_1)
        Me.gpb_denggan2_group.Enabled = False
        Me.gpb_denggan2_group.Location = New System.Drawing.Point(385, 71)
        Me.gpb_denggan2_group.Name = "gpb_denggan2_group"
        Me.gpb_denggan2_group.Size = New System.Drawing.Size(202, 45)
        Me.gpb_denggan2_group.TabIndex = 17
        Me.gpb_denggan2_group.TabStop = False
        Me.gpb_denggan2_group.Text = "2号灯杆灯头控制方式"
        '
        'chb_lamp_2_2
        '
        Me.chb_lamp_2_2.AutoSize = True
        Me.chb_lamp_2_2.Location = New System.Drawing.Point(88, 24)
        Me.chb_lamp_2_2.Name = "chb_lamp_2_2"
        Me.chb_lamp_2_2.Size = New System.Drawing.Size(66, 16)
        Me.chb_lamp_2_2.TabIndex = 1
        Me.chb_lamp_2_2.Text = "2号灯头"
        Me.chb_lamp_2_2.UseVisualStyleBackColor = True
        '
        'chb_lamp_2_1
        '
        Me.chb_lamp_2_1.AutoSize = True
        Me.chb_lamp_2_1.Location = New System.Drawing.Point(13, 24)
        Me.chb_lamp_2_1.Name = "chb_lamp_2_1"
        Me.chb_lamp_2_1.Size = New System.Drawing.Size(66, 16)
        Me.chb_lamp_2_1.TabIndex = 0
        Me.chb_lamp_2_1.Text = "1号灯头"
        Me.chb_lamp_2_1.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Label1)
        Me.GroupBox4.Location = New System.Drawing.Point(2, 7)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(366, 168)
        Me.GroupBox4.TabIndex = 15
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "功能描述"
        '
        'chb_lamp_1_2
        '
        Me.chb_lamp_1_2.AutoSize = True
        Me.chb_lamp_1_2.Location = New System.Drawing.Point(88, 21)
        Me.chb_lamp_1_2.Name = "chb_lamp_1_2"
        Me.chb_lamp_1_2.Size = New System.Drawing.Size(66, 16)
        Me.chb_lamp_1_2.TabIndex = 1
        Me.chb_lamp_1_2.Text = "2号灯头"
        Me.chb_lamp_1_2.UseVisualStyleBackColor = True
        '
        'chb_lamp_1_1
        '
        Me.chb_lamp_1_1.AutoSize = True
        Me.chb_lamp_1_1.Location = New System.Drawing.Point(13, 21)
        Me.chb_lamp_1_1.Name = "chb_lamp_1_1"
        Me.chb_lamp_1_1.Size = New System.Drawing.Size(66, 16)
        Me.chb_lamp_1_1.TabIndex = 0
        Me.chb_lamp_1_1.Text = "1号灯头"
        Me.chb_lamp_1_1.UseVisualStyleBackColor = True
        '
        'gpb_denggan1_group
        '
        Me.gpb_denggan1_group.Controls.Add(Me.chb_lamp_1_2)
        Me.gpb_denggan1_group.Controls.Add(Me.chb_lamp_1_1)
        Me.gpb_denggan1_group.Enabled = False
        Me.gpb_denggan1_group.Location = New System.Drawing.Point(385, 23)
        Me.gpb_denggan1_group.Name = "gpb_denggan1_group"
        Me.gpb_denggan1_group.Size = New System.Drawing.Size(202, 46)
        Me.gpb_denggan1_group.TabIndex = 14
        Me.gpb_denggan1_group.TabStop = False
        Me.gpb_denggan1_group.Text = "1号灯杆灯头控制方式"
        '
        'GroupBox12
        '
        Me.GroupBox12.Controls.Add(Me.rb_del_order)
        Me.GroupBox12.Controls.Add(Me.rb_add_order)
        Me.GroupBox12.Location = New System.Drawing.Point(2, 181)
        Me.GroupBox12.Name = "GroupBox12"
        Me.GroupBox12.Size = New System.Drawing.Size(141, 99)
        Me.GroupBox12.TabIndex = 22
        Me.GroupBox12.TabStop = False
        '
        'rb_del_order
        '
        Me.rb_del_order.AutoSize = True
        Me.rb_del_order.Location = New System.Drawing.Point(21, 69)
        Me.rb_del_order.Name = "rb_del_order"
        Me.rb_del_order.Size = New System.Drawing.Size(47, 16)
        Me.rb_del_order.TabIndex = 1
        Me.rb_del_order.Text = "删除"
        Me.rb_del_order.UseVisualStyleBackColor = True
        '
        'rb_add_order
        '
        Me.rb_add_order.AutoSize = True
        Me.rb_add_order.Checked = True
        Me.rb_add_order.Location = New System.Drawing.Point(21, 31)
        Me.rb_add_order.Name = "rb_add_order"
        Me.rb_add_order.Size = New System.Drawing.Size(47, 16)
        Me.rb_add_order.TabIndex = 0
        Me.rb_add_order.TabStop = True
        Me.rb_add_order.Text = "增加"
        Me.rb_add_order.UseVisualStyleBackColor = True
        '
        'chb_lamp_4_2
        '
        Me.chb_lamp_4_2.AutoSize = True
        Me.chb_lamp_4_2.Location = New System.Drawing.Point(88, 25)
        Me.chb_lamp_4_2.Name = "chb_lamp_4_2"
        Me.chb_lamp_4_2.Size = New System.Drawing.Size(66, 16)
        Me.chb_lamp_4_2.TabIndex = 1
        Me.chb_lamp_4_2.Text = "2号灯头"
        Me.chb_lamp_4_2.UseVisualStyleBackColor = True
        '
        'chb_lamp_4_1
        '
        Me.chb_lamp_4_1.AutoSize = True
        Me.chb_lamp_4_1.Location = New System.Drawing.Point(13, 25)
        Me.chb_lamp_4_1.Name = "chb_lamp_4_1"
        Me.chb_lamp_4_1.Size = New System.Drawing.Size(66, 16)
        Me.chb_lamp_4_1.TabIndex = 0
        Me.chb_lamp_4_1.Text = "1号灯头"
        Me.chb_lamp_4_1.UseVisualStyleBackColor = True
        '
        'gpb_denggan3_group
        '
        Me.gpb_denggan3_group.Controls.Add(Me.chb_lamp_3_2)
        Me.gpb_denggan3_group.Controls.Add(Me.chb_lamp_3_1)
        Me.gpb_denggan3_group.Enabled = False
        Me.gpb_denggan3_group.Location = New System.Drawing.Point(385, 120)
        Me.gpb_denggan3_group.Name = "gpb_denggan3_group"
        Me.gpb_denggan3_group.Size = New System.Drawing.Size(202, 47)
        Me.gpb_denggan3_group.TabIndex = 18
        Me.gpb_denggan3_group.TabStop = False
        Me.gpb_denggan3_group.Text = "3号灯杆灯头控制方式"
        '
        'chb_lamp_3_2
        '
        Me.chb_lamp_3_2.AutoSize = True
        Me.chb_lamp_3_2.Location = New System.Drawing.Point(88, 25)
        Me.chb_lamp_3_2.Name = "chb_lamp_3_2"
        Me.chb_lamp_3_2.Size = New System.Drawing.Size(66, 16)
        Me.chb_lamp_3_2.TabIndex = 1
        Me.chb_lamp_3_2.Text = "2号灯头"
        Me.chb_lamp_3_2.UseVisualStyleBackColor = True
        '
        'chb_lamp_3_1
        '
        Me.chb_lamp_3_1.AutoSize = True
        Me.chb_lamp_3_1.Location = New System.Drawing.Point(13, 25)
        Me.chb_lamp_3_1.Name = "chb_lamp_3_1"
        Me.chb_lamp_3_1.Size = New System.Drawing.Size(66, 16)
        Me.chb_lamp_3_1.TabIndex = 0
        Me.chb_lamp_3_1.Text = "1号灯头"
        Me.chb_lamp_3_1.UseVisualStyleBackColor = True
        '
        'gpb_denggan4_group
        '
        Me.gpb_denggan4_group.Controls.Add(Me.chb_lamp_4_2)
        Me.gpb_denggan4_group.Controls.Add(Me.chb_lamp_4_1)
        Me.gpb_denggan4_group.Enabled = False
        Me.gpb_denggan4_group.Location = New System.Drawing.Point(385, 173)
        Me.gpb_denggan4_group.Name = "gpb_denggan4_group"
        Me.gpb_denggan4_group.Size = New System.Drawing.Size(202, 50)
        Me.gpb_denggan4_group.TabIndex = 19
        Me.gpb_denggan4_group.TabStop = False
        Me.gpb_denggan4_group.Text = "4号灯杆灯头控制方式"
        '
        'chb_denggan_4
        '
        Me.chb_denggan_4.AutoSize = True
        Me.chb_denggan_4.Location = New System.Drawing.Point(76, 65)
        Me.chb_denggan_4.Name = "chb_denggan_4"
        Me.chb_denggan_4.Size = New System.Drawing.Size(66, 16)
        Me.chb_denggan_4.TabIndex = 2
        Me.chb_denggan_4.Text = "4号灯杆"
        Me.chb_denggan_4.UseVisualStyleBackColor = True
        '
        'GBMethod
        '
        Me.GBMethod.Controls.Add(Me.chb_denggan_3)
        Me.GBMethod.Controls.Add(Me.chb_denggan_4)
        Me.GBMethod.Controls.Add(Me.chb_denggan_2)
        Me.GBMethod.Controls.Add(Me.chb_denggan_1)
        Me.GBMethod.Location = New System.Drawing.Point(148, 180)
        Me.GBMethod.Name = "GBMethod"
        Me.GBMethod.Size = New System.Drawing.Size(220, 93)
        Me.GBMethod.TabIndex = 12
        Me.GBMethod.TabStop = False
        Me.GBMethod.Text = "灯杆控制方式"
        '
        'chb_denggan_3
        '
        Me.chb_denggan_3.AutoSize = True
        Me.chb_denggan_3.Location = New System.Drawing.Point(76, 32)
        Me.chb_denggan_3.Name = "chb_denggan_3"
        Me.chb_denggan_3.Size = New System.Drawing.Size(66, 16)
        Me.chb_denggan_3.TabIndex = 4
        Me.chb_denggan_3.Text = "3号灯杆"
        Me.chb_denggan_3.UseVisualStyleBackColor = True
        '
        'chb_denggan_2
        '
        Me.chb_denggan_2.AutoSize = True
        Me.chb_denggan_2.Location = New System.Drawing.Point(5, 65)
        Me.chb_denggan_2.Name = "chb_denggan_2"
        Me.chb_denggan_2.Size = New System.Drawing.Size(66, 16)
        Me.chb_denggan_2.TabIndex = 1
        Me.chb_denggan_2.Text = "2号灯杆"
        Me.chb_denggan_2.UseVisualStyleBackColor = True
        '
        'chb_denggan_1
        '
        Me.chb_denggan_1.AutoSize = True
        Me.chb_denggan_1.Location = New System.Drawing.Point(5, 33)
        Me.chb_denggan_1.Name = "chb_denggan_1"
        Me.chb_denggan_1.Size = New System.Drawing.Size(66, 16)
        Me.chb_denggan_1.TabIndex = 0
        Me.chb_denggan_1.Text = "1号灯杆"
        Me.chb_denggan_1.UseVisualStyleBackColor = True
        '
        'cb_order_list
        '
        Me.cb_order_list.FormattingEnabled = True
        Me.cb_order_list.Location = New System.Drawing.Point(84, 392)
        Me.cb_order_list.Name = "cb_order_list"
        Me.cb_order_list.Size = New System.Drawing.Size(237, 20)
        Me.cb_order_list.TabIndex = 26
        Me.cb_order_list.Visible = False
        '
        'lb_grouporder_name
        '
        Me.lb_grouporder_name.Location = New System.Drawing.Point(84, 393)
        Me.lb_grouporder_name.Name = "lb_grouporder_name"
        Me.lb_grouporder_name.Size = New System.Drawing.Size(252, 21)
        Me.lb_grouporder_name.TabIndex = 25
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(5, 395)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 12)
        Me.Label2.TabIndex = 24
        Me.Label2.Text = "命令名称："
        '
        'bt_send_order
        '
        Me.bt_send_order.Location = New System.Drawing.Point(351, 392)
        Me.bt_send_order.Name = "bt_send_order"
        Me.bt_send_order.Size = New System.Drawing.Size(64, 20)
        Me.bt_send_order.TabIndex = 23
        Me.bt_send_order.Text = "保存命令"
        Me.bt_send_order.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.pb_lamp1)
        Me.GroupBox1.Controls.Add(Me.pb_lamp2)
        Me.GroupBox1.Location = New System.Drawing.Point(13, 11)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(55, 39)
        Me.GroupBox1.TabIndex = 24
        Me.GroupBox1.TabStop = False
        '
        'pb_lamp1
        '
        Me.pb_lamp1.Location = New System.Drawing.Point(5, 16)
        Me.pb_lamp1.Name = "pb_lamp1"
        Me.pb_lamp1.Size = New System.Drawing.Size(17, 17)
        Me.pb_lamp1.TabIndex = 0
        Me.pb_lamp1.TabStop = False
        '
        'pb_lamp2
        '
        Me.pb_lamp2.Location = New System.Drawing.Point(27, 16)
        Me.pb_lamp2.Name = "pb_lamp2"
        Me.pb_lamp2.Size = New System.Drawing.Size(17, 17)
        Me.pb_lamp2.TabIndex = 1
        Me.pb_lamp2.TabStop = False
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.pb_lamp5)
        Me.GroupBox5.Controls.Add(Me.pb_lamp6)
        Me.GroupBox5.Location = New System.Drawing.Point(146, 18)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(55, 34)
        Me.GroupBox5.TabIndex = 25
        Me.GroupBox5.TabStop = False
        '
        'pb_lamp5
        '
        Me.pb_lamp5.Location = New System.Drawing.Point(5, 12)
        Me.pb_lamp5.Name = "pb_lamp5"
        Me.pb_lamp5.Size = New System.Drawing.Size(17, 17)
        Me.pb_lamp5.TabIndex = 4
        Me.pb_lamp5.TabStop = False
        '
        'pb_lamp6
        '
        Me.pb_lamp6.Location = New System.Drawing.Point(27, 12)
        Me.pb_lamp6.Name = "pb_lamp6"
        Me.pb_lamp6.Size = New System.Drawing.Size(17, 17)
        Me.pb_lamp6.TabIndex = 7
        Me.pb_lamp6.TabStop = False
        '
        'pb_lamp4
        '
        Me.pb_lamp4.Location = New System.Drawing.Point(28, 13)
        Me.pb_lamp4.Name = "pb_lamp4"
        Me.pb_lamp4.Size = New System.Drawing.Size(17, 17)
        Me.pb_lamp4.TabIndex = 3
        Me.pb_lamp4.TabStop = False
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.pb_lamp4)
        Me.GroupBox6.Controls.Add(Me.pb_lamp3)
        Me.GroupBox6.Location = New System.Drawing.Point(11, 56)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(57, 36)
        Me.GroupBox6.TabIndex = 26
        Me.GroupBox6.TabStop = False
        '
        'pb_lamp3
        '
        Me.pb_lamp3.Location = New System.Drawing.Point(5, 14)
        Me.pb_lamp3.Name = "pb_lamp3"
        Me.pb_lamp3.Size = New System.Drawing.Size(17, 17)
        Me.pb_lamp3.TabIndex = 6
        Me.pb_lamp3.TabStop = False
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.pb_lamp7)
        Me.GroupBox7.Controls.Add(Me.pb_lamp8)
        Me.GroupBox7.Location = New System.Drawing.Point(146, 56)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(57, 36)
        Me.GroupBox7.TabIndex = 27
        Me.GroupBox7.TabStop = False
        '
        'pb_lamp7
        '
        Me.pb_lamp7.Location = New System.Drawing.Point(5, 14)
        Me.pb_lamp7.Name = "pb_lamp7"
        Me.pb_lamp7.Size = New System.Drawing.Size(17, 17)
        Me.pb_lamp7.TabIndex = 9
        Me.pb_lamp7.TabStop = False
        '
        'pb_lamp8
        '
        Me.pb_lamp8.Location = New System.Drawing.Point(27, 14)
        Me.pb_lamp8.Name = "pb_lamp8"
        Me.pb_lamp8.Size = New System.Drawing.Size(17, 17)
        Me.pb_lamp8.TabIndex = 10
        Me.pb_lamp8.TabStop = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.GroupBox7)
        Me.GroupBox2.Controls.Add(Me.GroupBox6)
        Me.GroupBox2.Controls.Add(Me.GroupBox5)
        Me.GroupBox2.Controls.Add(Me.GroupBox1)
        Me.GroupBox2.Location = New System.Drawing.Point(2, 267)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(372, 96)
        Me.GroupBox2.TabIndex = 13
        Me.GroupBox2.TabStop = False
        '
        '二回路组合命令
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(599, 441)
        Me.Controls.Add(Me.cb_order_list)
        Me.Controls.Add(Me.lb_grouporder_name)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.bt_send_order)
        Me.Controls.Add(Me.gpb_denggan2_group)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.gpb_denggan1_group)
        Me.Controls.Add(Me.GroupBox12)
        Me.Controls.Add(Me.gpb_denggan3_group)
        Me.Controls.Add(Me.gpb_denggan4_group)
        Me.Controls.Add(Me.GBMethod)
        Me.Name = "二回路组合命令"
        Me.Text = "二回路组合命令"
        Me.gpb_denggan2_group.ResumeLayout(False)
        Me.gpb_denggan2_group.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.gpb_denggan1_group.ResumeLayout(False)
        Me.gpb_denggan1_group.PerformLayout()
        Me.GroupBox12.ResumeLayout(False)
        Me.GroupBox12.PerformLayout()
        Me.gpb_denggan3_group.ResumeLayout(False)
        Me.gpb_denggan3_group.PerformLayout()
        Me.gpb_denggan4_group.ResumeLayout(False)
        Me.gpb_denggan4_group.PerformLayout()
        Me.GBMethod.ResumeLayout(False)
        Me.GBMethod.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.pb_lamp1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pb_lamp2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox5.ResumeLayout(False)
        CType(Me.pb_lamp5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pb_lamp6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pb_lamp4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox6.ResumeLayout(False)
        CType(Me.pb_lamp3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox7.ResumeLayout(False)
        CType(Me.pb_lamp7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pb_lamp8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents gpb_denggan2_group As System.Windows.Forms.GroupBox
    Friend WithEvents chb_lamp_2_2 As System.Windows.Forms.CheckBox
    Friend WithEvents chb_lamp_2_1 As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents chb_lamp_1_2 As System.Windows.Forms.CheckBox
    Friend WithEvents chb_lamp_1_1 As System.Windows.Forms.CheckBox
    Friend WithEvents gpb_denggan1_group As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox12 As System.Windows.Forms.GroupBox
    Friend WithEvents rb_del_order As System.Windows.Forms.RadioButton
    Friend WithEvents rb_add_order As System.Windows.Forms.RadioButton
    Friend WithEvents chb_lamp_4_2 As System.Windows.Forms.CheckBox
    Friend WithEvents chb_lamp_4_1 As System.Windows.Forms.CheckBox
    Friend WithEvents gpb_denggan3_group As System.Windows.Forms.GroupBox
    Friend WithEvents chb_lamp_3_2 As System.Windows.Forms.CheckBox
    Friend WithEvents chb_lamp_3_1 As System.Windows.Forms.CheckBox
    Friend WithEvents gpb_denggan4_group As System.Windows.Forms.GroupBox
    Friend WithEvents chb_denggan_4 As System.Windows.Forms.CheckBox
    Friend WithEvents GBMethod As System.Windows.Forms.GroupBox
    Friend WithEvents chb_denggan_2 As System.Windows.Forms.CheckBox
    Friend WithEvents chb_denggan_1 As System.Windows.Forms.CheckBox
    Friend WithEvents cb_order_list As System.Windows.Forms.ComboBox
    Friend WithEvents lb_grouporder_name As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents bt_send_order As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents pb_lamp1 As System.Windows.Forms.PictureBox
    Friend WithEvents pb_lamp2 As System.Windows.Forms.PictureBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents pb_lamp4 As System.Windows.Forms.PictureBox
    Friend WithEvents pb_lamp5 As System.Windows.Forms.PictureBox
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents pb_lamp3 As System.Windows.Forms.PictureBox
    Friend WithEvents pb_lamp6 As System.Windows.Forms.PictureBox
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents pb_lamp7 As System.Windows.Forms.PictureBox
    Friend WithEvents pb_lamp8 As System.Windows.Forms.PictureBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents chb_denggan_3 As System.Windows.Forms.CheckBox
End Class
