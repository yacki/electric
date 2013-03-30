<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 配置数据
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
        Me.bt_clearconfig = New System.Windows.Forms.Button
        Me.bt_stopconfig = New System.Windows.Forms.Button
        Me.rtb_returnvalue = New System.Windows.Forms.RichTextBox
        Me.bt_readconfig = New System.Windows.Forms.Button
        Me.tv_configvalue = New System.Windows.Forms.TreeView
        Me.BackgroundWorkerconfigvalue = New System.ComponentModel.BackgroundWorker
        Me.groupbox = New System.Windows.Forms.GroupBox
        Me.rb_time = New System.Windows.Forms.RadioButton
        Me.rb_divtime = New System.Windows.Forms.RadioButton
        Me.rb_roadid = New System.Windows.Forms.RadioButton
        Me.groupbox.SuspendLayout()
        Me.SuspendLayout()
        '
        'bt_clearconfig
        '
        Me.bt_clearconfig.Location = New System.Drawing.Point(531, 58)
        Me.bt_clearconfig.Name = "bt_clearconfig"
        Me.bt_clearconfig.Size = New System.Drawing.Size(75, 23)
        Me.bt_clearconfig.TabIndex = 18
        Me.bt_clearconfig.Text = "清除"
        Me.bt_clearconfig.UseVisualStyleBackColor = True
        '
        'bt_stopconfig
        '
        Me.bt_stopconfig.Location = New System.Drawing.Point(411, 57)
        Me.bt_stopconfig.Name = "bt_stopconfig"
        Me.bt_stopconfig.Size = New System.Drawing.Size(75, 23)
        Me.bt_stopconfig.TabIndex = 17
        Me.bt_stopconfig.Text = "停止"
        Me.bt_stopconfig.UseVisualStyleBackColor = True
        '
        'rtb_returnvalue
        '
        Me.rtb_returnvalue.Location = New System.Drawing.Point(290, 87)
        Me.rtb_returnvalue.Name = "rtb_returnvalue"
        Me.rtb_returnvalue.Size = New System.Drawing.Size(468, 424)
        Me.rtb_returnvalue.TabIndex = 16
        Me.rtb_returnvalue.Text = ""
        '
        'bt_readconfig
        '
        Me.bt_readconfig.Location = New System.Drawing.Point(290, 58)
        Me.bt_readconfig.Name = "bt_readconfig"
        Me.bt_readconfig.Size = New System.Drawing.Size(75, 23)
        Me.bt_readconfig.TabIndex = 15
        Me.bt_readconfig.Text = "读取数据"
        Me.bt_readconfig.UseVisualStyleBackColor = True
        '
        'tv_configvalue
        '
        Me.tv_configvalue.CheckBoxes = True
        Me.tv_configvalue.Location = New System.Drawing.Point(1, 1)
        Me.tv_configvalue.Name = "tv_configvalue"
        Me.tv_configvalue.Size = New System.Drawing.Size(283, 510)
        Me.tv_configvalue.TabIndex = 14
        '
        'BackgroundWorkerconfigvalue
        '
        Me.BackgroundWorkerconfigvalue.WorkerReportsProgress = True
        Me.BackgroundWorkerconfigvalue.WorkerSupportsCancellation = True
        '
        'groupbox
        '
        Me.groupbox.Controls.Add(Me.rb_time)
        Me.groupbox.Controls.Add(Me.rb_divtime)
        Me.groupbox.Controls.Add(Me.rb_roadid)
        Me.groupbox.Location = New System.Drawing.Point(290, 1)
        Me.groupbox.Name = "groupbox"
        Me.groupbox.Size = New System.Drawing.Size(467, 51)
        Me.groupbox.TabIndex = 19
        Me.groupbox.TabStop = False
        '
        'rb_time
        '
        Me.rb_time.AutoSize = True
        Me.rb_time.Location = New System.Drawing.Point(149, 19)
        Me.rb_time.Name = "rb_time"
        Me.rb_time.Size = New System.Drawing.Size(47, 16)
        Me.rb_time.TabIndex = 2
        Me.rb_time.Text = "时间"
        Me.rb_time.UseVisualStyleBackColor = True
        '
        'rb_divtime
        '
        Me.rb_divtime.AutoSize = True
        Me.rb_divtime.Checked = True
        Me.rb_divtime.Location = New System.Drawing.Point(15, 19)
        Me.rb_divtime.Name = "rb_divtime"
        Me.rb_divtime.Size = New System.Drawing.Size(71, 16)
        Me.rb_divtime.TabIndex = 1
        Me.rb_divtime.TabStop = True
        Me.rb_divtime.Text = "时段控制"
        Me.rb_divtime.UseVisualStyleBackColor = True
        '
        'rb_roadid
        '
        Me.rb_roadid.AutoSize = True
        Me.rb_roadid.Location = New System.Drawing.Point(336, 19)
        Me.rb_roadid.Name = "rb_roadid"
        Me.rb_roadid.Size = New System.Drawing.Size(59, 16)
        Me.rb_roadid.TabIndex = 0
        Me.rb_roadid.Text = "路段号"
        Me.rb_roadid.UseVisualStyleBackColor = True
        Me.rb_roadid.Visible = False
        '
        '配置数据
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(770, 517)
        Me.Controls.Add(Me.groupbox)
        Me.Controls.Add(Me.bt_clearconfig)
        Me.Controls.Add(Me.bt_stopconfig)
        Me.Controls.Add(Me.rtb_returnvalue)
        Me.Controls.Add(Me.bt_readconfig)
        Me.Controls.Add(Me.tv_configvalue)
        Me.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Name = "配置数据"
        Me.Text = "配置数据"
        Me.groupbox.ResumeLayout(False)
        Me.groupbox.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents bt_clearconfig As System.Windows.Forms.Button
    Friend WithEvents bt_stopconfig As System.Windows.Forms.Button
    Friend WithEvents rtb_returnvalue As System.Windows.Forms.RichTextBox
    Friend WithEvents bt_readconfig As System.Windows.Forms.Button
    Friend WithEvents tv_configvalue As System.Windows.Forms.TreeView
    Friend WithEvents BackgroundWorkerconfigvalue As System.ComponentModel.BackgroundWorker
    Friend WithEvents groupbox As System.Windows.Forms.GroupBox
    Friend WithEvents rb_time As System.Windows.Forms.RadioButton
    Friend WithEvents rb_divtime As System.Windows.Forms.RadioButton
    Friend WithEvents rb_roadid As System.Windows.Forms.RadioButton
End Class
