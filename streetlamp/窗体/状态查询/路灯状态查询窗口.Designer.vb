<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 路灯状态查询窗口
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(路灯状态查询窗口))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.control_box_name = New System.Windows.Forms.ComboBox
        Me.box_string = New System.Windows.Forms.Label
        Me.lamp_id = New System.Windows.Forms.ComboBox
        Me.lamp_string = New System.Windows.Forms.Label
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.finding = New System.Windows.Forms.ToolStripStatusLabel
        Me.ToolStripProgressBar_check_communication = New System.Windows.Forms.ToolStripProgressBar
        Me.find = New System.Windows.Forms.Button
        Me.state_list = New System.Windows.Forms.DataGridView
        Me.datagridview_control_box_name = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.datagridview_lamp_type = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.datagridview_lamp_id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.datagridview_dianzu = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.datagridview_current = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.datagridview_state = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Single_inf = New System.Windows.Forms.RichTextBox
        Me.box_control = New System.Windows.Forms.RadioButton
        Me.lamp_id_control = New System.Windows.Forms.RadioButton
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.BackgroundWorker_state = New System.ComponentModel.BackgroundWorker
        Me.查询对象 = New System.Windows.Forms.GroupBox
        Me.lamp_type = New System.Windows.Forms.ComboBox
        Me.lamp_id_start = New System.Windows.Forms.Label
        Me.lamp_type_id = New System.Windows.Forms.Label
        Me.StatusStrip1.SuspendLayout()
        CType(Me.state_list, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.查询对象.SuspendLayout()
        Me.SuspendLayout()
        '
        'control_box_name
        '
        Me.control_box_name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.control_box_name.FormattingEnabled = True
        Me.control_box_name.Location = New System.Drawing.Point(98, 18)
        Me.control_box_name.Name = "control_box_name"
        Me.control_box_name.Size = New System.Drawing.Size(87, 20)
        Me.control_box_name.TabIndex = 90
        '
        'box_string
        '
        Me.box_string.AutoSize = True
        Me.box_string.BackColor = System.Drawing.Color.Transparent
        Me.box_string.Location = New System.Drawing.Point(27, 23)
        Me.box_string.Name = "box_string"
        Me.box_string.Size = New System.Drawing.Size(65, 12)
        Me.box_string.TabIndex = 89
        Me.box_string.Text = "区域名称："
        '
        'lamp_id
        '
        Me.lamp_id.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.lamp_id.FormattingEnabled = True
        Me.lamp_id.Location = New System.Drawing.Point(98, 61)
        Me.lamp_id.Name = "lamp_id"
        Me.lamp_id.Size = New System.Drawing.Size(88, 20)
        Me.lamp_id.TabIndex = 92
        '
        'lamp_string
        '
        Me.lamp_string.AutoSize = True
        Me.lamp_string.BackColor = System.Drawing.Color.Transparent
        Me.lamp_string.Location = New System.Drawing.Point(27, 65)
        Me.lamp_string.Name = "lamp_string"
        Me.lamp_string.Size = New System.Drawing.Size(65, 12)
        Me.lamp_string.TabIndex = 91
        Me.lamp_string.Text = "路灯编号："
        '
        'MenuStrip1
        '
        Me.MenuStrip1.BackgroundImage = CType(resources.GetObject("MenuStrip1.BackgroundImage"), System.Drawing.Image)
        Me.MenuStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(839, 24)
        Me.MenuStrip1.TabIndex = 91
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.finding, Me.ToolStripProgressBar_check_communication})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 507)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(839, 22)
        Me.StatusStrip1.TabIndex = 92
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'finding
        '
        Me.finding.Name = "finding"
        Me.finding.Size = New System.Drawing.Size(101, 17)
        Me.finding.Text = "路灯状态查询窗口"
        '
        'ToolStripProgressBar_check_communication
        '
        Me.ToolStripProgressBar_check_communication.Name = "ToolStripProgressBar_check_communication"
        Me.ToolStripProgressBar_check_communication.Size = New System.Drawing.Size(100, 16)
        Me.ToolStripProgressBar_check_communication.Visible = False
        '
        'find
        '
        Me.find.Location = New System.Drawing.Point(63, 253)
        Me.find.Name = "find"
        Me.find.Size = New System.Drawing.Size(75, 23)
        Me.find.TabIndex = 93
        Me.find.Text = "查询"
        Me.find.UseVisualStyleBackColor = True
        '
        'state_list
        '
        Me.state_list.AllowUserToAddRows = False
        Me.state_list.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.state_list.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.state_list.BackgroundColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.state_list.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.state_list.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.state_list.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.datagridview_control_box_name, Me.datagridview_lamp_type, Me.datagridview_lamp_id, Me.datagridview_dianzu, Me.datagridview_current, Me.datagridview_state})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.state_list.DefaultCellStyle = DataGridViewCellStyle2
        Me.state_list.Location = New System.Drawing.Point(233, 27)
        Me.state_list.Name = "state_list"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.state_list.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.state_list.RowTemplate.Height = 23
        Me.state_list.Size = New System.Drawing.Size(596, 464)
        Me.state_list.TabIndex = 94
        '
        'datagridview_control_box_name
        '
        Me.datagridview_control_box_name.HeaderText = "区域名称"
        Me.datagridview_control_box_name.Name = "datagridview_control_box_name"
        '
        'datagridview_lamp_type
        '
        Me.datagridview_lamp_type.HeaderText = "路灯类型"
        Me.datagridview_lamp_type.Name = "datagridview_lamp_type"
        '
        'datagridview_lamp_id
        '
        Me.datagridview_lamp_id.HeaderText = "路灯编号"
        Me.datagridview_lamp_id.Name = "datagridview_lamp_id"
        '
        'datagridview_dianzu
        '
        Me.datagridview_dianzu.HeaderText = "电阻AD值"
        Me.datagridview_dianzu.Name = "datagridview_dianzu"
        Me.datagridview_dianzu.Visible = False
        '
        'datagridview_current
        '
        Me.datagridview_current.HeaderText = "电流AD值"
        Me.datagridview_current.Name = "datagridview_current"
        Me.datagridview_current.Visible = False
        '
        'datagridview_state
        '
        Me.datagridview_state.HeaderText = "状态"
        Me.datagridview_state.Name = "datagridview_state"
        '
        'Single_inf
        '
        Me.Single_inf.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Single_inf.HideSelection = False
        Me.Single_inf.Location = New System.Drawing.Point(10, 282)
        Me.Single_inf.Name = "Single_inf"
        Me.Single_inf.Size = New System.Drawing.Size(200, 209)
        Me.Single_inf.TabIndex = 95
        Me.Single_inf.Text = ""
        '
        'box_control
        '
        Me.box_control.AutoSize = True
        Me.box_control.BackColor = System.Drawing.Color.Transparent
        Me.box_control.Location = New System.Drawing.Point(22, 20)
        Me.box_control.Name = "box_control"
        Me.box_control.Size = New System.Drawing.Size(71, 16)
        Me.box_control.TabIndex = 99
        Me.box_control.Text = "区域名称"
        Me.box_control.UseVisualStyleBackColor = False
        '
        'lamp_id_control
        '
        Me.lamp_id_control.AutoSize = True
        Me.lamp_id_control.BackColor = System.Drawing.Color.Transparent
        Me.lamp_id_control.Checked = True
        Me.lamp_id_control.Location = New System.Drawing.Point(22, 64)
        Me.lamp_id_control.Name = "lamp_id_control"
        Me.lamp_id_control.Size = New System.Drawing.Size(71, 16)
        Me.lamp_id_control.TabIndex = 100
        Me.lamp_id_control.TabStop = True
        Me.lamp_id_control.Text = "路灯编号"
        Me.lamp_id_control.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.lamp_id_control)
        Me.GroupBox1.Controls.Add(Me.box_control)
        Me.GroupBox1.Location = New System.Drawing.Point(10, 27)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(204, 97)
        Me.GroupBox1.TabIndex = 103
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "查询级别"
        '
        'BackgroundWorker_state
        '
        Me.BackgroundWorker_state.WorkerReportsProgress = True
        Me.BackgroundWorker_state.WorkerSupportsCancellation = True
        '
        '查询对象
        '
        Me.查询对象.BackColor = System.Drawing.Color.Transparent
        Me.查询对象.Controls.Add(Me.box_string)
        Me.查询对象.Controls.Add(Me.control_box_name)
        Me.查询对象.Controls.Add(Me.lamp_string)
        Me.查询对象.Controls.Add(Me.lamp_id)
        Me.查询对象.Location = New System.Drawing.Point(10, 140)
        Me.查询对象.Name = "查询对象"
        Me.查询对象.Size = New System.Drawing.Size(204, 97)
        Me.查询对象.TabIndex = 104
        Me.查询对象.TabStop = False
        Me.查询对象.Text = "查询对象"
        '
        'lamp_type
        '
        Me.lamp_type.FormattingEnabled = True
        Me.lamp_type.Location = New System.Drawing.Point(151, 256)
        Me.lamp_type.Name = "lamp_type"
        Me.lamp_type.Size = New System.Drawing.Size(44, 20)
        Me.lamp_type.TabIndex = 92
        Me.lamp_type.Text = "(0)路灯"
        Me.lamp_type.Visible = False
        '
        'lamp_id_start
        '
        Me.lamp_id_start.AutoSize = True
        Me.lamp_id_start.BackColor = System.Drawing.Color.Transparent
        Me.lamp_id_start.Location = New System.Drawing.Point(25, 240)
        Me.lamp_id_start.Name = "lamp_id_start"
        Me.lamp_id_start.Size = New System.Drawing.Size(113, 12)
        Me.lamp_id_start.TabIndex = 105
        Me.lamp_id_start.Text = "景观灯编号前半部分"
        '
        'lamp_type_id
        '
        Me.lamp_type_id.AutoSize = True
        Me.lamp_type_id.Location = New System.Drawing.Point(144, 240)
        Me.lamp_type_id.Name = "lamp_type_id"
        Me.lamp_type_id.Size = New System.Drawing.Size(53, 12)
        Me.lamp_type_id.TabIndex = 169
        Me.lamp_type_id.Text = "灯的类型"
        '
        '路灯状态查询窗口
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(839, 529)
        Me.Controls.Add(Me.lamp_type)
        Me.Controls.Add(Me.lamp_type_id)
        Me.Controls.Add(Me.lamp_id_start)
        Me.Controls.Add(Me.查询对象)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Single_inf)
        Me.Controls.Add(Me.state_list)
        Me.Controls.Add(Me.find)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "路灯状态查询窗口"
        Me.Text = "路灯状态查询窗口"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        CType(Me.state_list, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.查询对象.ResumeLayout(False)
        Me.查询对象.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents control_box_name As System.Windows.Forms.ComboBox
    Friend WithEvents box_string As System.Windows.Forms.Label
    Friend WithEvents lamp_id As System.Windows.Forms.ComboBox
    Friend WithEvents lamp_string As System.Windows.Forms.Label
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents find As System.Windows.Forms.Button
    Friend WithEvents state_list As System.Windows.Forms.DataGridView
    Friend WithEvents finding As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Single_inf As System.Windows.Forms.RichTextBox
    Friend WithEvents box_control As System.Windows.Forms.RadioButton
    Friend WithEvents lamp_id_control As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents BackgroundWorker_state As System.ComponentModel.BackgroundWorker
    Friend WithEvents 查询对象 As System.Windows.Forms.GroupBox
    Friend WithEvents lamp_type As System.Windows.Forms.ComboBox
    Friend WithEvents lamp_id_start As System.Windows.Forms.Label
    Friend WithEvents lamp_type_id As System.Windows.Forms.Label
    Friend WithEvents ToolStripProgressBar_check_communication As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents datagridview_control_box_name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents datagridview_lamp_type As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents datagridview_lamp_id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents datagridview_dianzu As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents datagridview_current As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents datagridview_state As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
