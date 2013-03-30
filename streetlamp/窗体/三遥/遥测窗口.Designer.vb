<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 遥测窗口
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(遥测窗口))
        Me.bt_add_boxname = New System.Windows.Forms.Button()
        Me.bt_del_boxname = New System.Windows.Forms.Button()
        Me.dgv_yaoce_list = New System.Windows.Forms.DataGridView()
        Me.checkid = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.yaoce_boxname = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.bt_yaoce = New System.Windows.Forms.Button()
        Me.bt_stop = New System.Windows.Forms.Button()
        Me.tv_yaoce_controlbox = New System.Windows.Forms.TreeView()
        Me.BackgroundWorker_yaoce = New System.ComponentModel.BackgroundWorker()
        Me.rtb_inf = New System.Windows.Forms.RichTextBox()
        Me.clear_text = New System.Windows.Forms.Button()
        Me.dgv_boxinf = New System.Windows.Forms.DataGridView()
        Me.id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.board_id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.box_id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.box_name = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.xiangwei = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.presure = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.current = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.power = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.power_yinshu = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.state = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.yaoce_state = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.dgv_yaoce_list, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgv_boxinf, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'bt_add_boxname
        '
        Me.bt_add_boxname.ForeColor = System.Drawing.SystemColors.WindowText
        Me.bt_add_boxname.Location = New System.Drawing.Point(273, 18)
        Me.bt_add_boxname.Name = "bt_add_boxname"
        Me.bt_add_boxname.Size = New System.Drawing.Size(64, 20)
        Me.bt_add_boxname.TabIndex = 78
        Me.bt_add_boxname.Text = "选择>>"
        Me.bt_add_boxname.UseVisualStyleBackColor = True
        '
        'bt_del_boxname
        '
        Me.bt_del_boxname.ForeColor = System.Drawing.SystemColors.WindowText
        Me.bt_del_boxname.Location = New System.Drawing.Point(273, 74)
        Me.bt_del_boxname.Name = "bt_del_boxname"
        Me.bt_del_boxname.Size = New System.Drawing.Size(64, 20)
        Me.bt_del_boxname.TabIndex = 79
        Me.bt_del_boxname.Text = "删除<<"
        Me.bt_del_boxname.UseVisualStyleBackColor = True
        '
        'dgv_yaoce_list
        '
        Me.dgv_yaoce_list.AllowUserToAddRows = False
        Me.dgv_yaoce_list.BackgroundColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_yaoce_list.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv_yaoce_list.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_yaoce_list.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.checkid, Me.yaoce_boxname})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv_yaoce_list.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgv_yaoce_list.Location = New System.Drawing.Point(372, 3)
        Me.dgv_yaoce_list.Name = "dgv_yaoce_list"
        Me.dgv_yaoce_list.RowHeadersVisible = False
        Me.dgv_yaoce_list.RowHeadersWidth = 5
        Me.dgv_yaoce_list.RowTemplate.Height = 23
        Me.dgv_yaoce_list.Size = New System.Drawing.Size(185, 297)
        Me.dgv_yaoce_list.TabIndex = 80
        '
        'checkid
        '
        Me.checkid.FalseValue = "0"
        Me.checkid.HeaderText = ""
        Me.checkid.Name = "checkid"
        Me.checkid.TrueValue = "1"
        Me.checkid.Width = 25
        '
        'yaoce_boxname
        '
        Me.yaoce_boxname.HeaderText = "主控箱名称"
        Me.yaoce_boxname.Name = "yaoce_boxname"
        Me.yaoce_boxname.Width = 150
        '
        'bt_yaoce
        '
        Me.bt_yaoce.ForeColor = System.Drawing.SystemColors.WindowText
        Me.bt_yaoce.Location = New System.Drawing.Point(273, 129)
        Me.bt_yaoce.Name = "bt_yaoce"
        Me.bt_yaoce.Size = New System.Drawing.Size(64, 20)
        Me.bt_yaoce.TabIndex = 81
        Me.bt_yaoce.Text = "召测"
        Me.bt_yaoce.UseVisualStyleBackColor = True
        '
        'bt_stop
        '
        Me.bt_stop.ForeColor = System.Drawing.SystemColors.WindowText
        Me.bt_stop.Location = New System.Drawing.Point(273, 185)
        Me.bt_stop.Name = "bt_stop"
        Me.bt_stop.Size = New System.Drawing.Size(64, 20)
        Me.bt_stop.TabIndex = 82
        Me.bt_stop.Text = "停止"
        Me.bt_stop.UseVisualStyleBackColor = True
        '
        'tv_yaoce_controlbox
        '
        Me.tv_yaoce_controlbox.BackColor = System.Drawing.SystemColors.Window
        Me.tv_yaoce_controlbox.CheckBoxes = True
        Me.tv_yaoce_controlbox.Location = New System.Drawing.Point(4, 3)
        Me.tv_yaoce_controlbox.Name = "tv_yaoce_controlbox"
        Me.tv_yaoce_controlbox.Size = New System.Drawing.Size(221, 297)
        Me.tv_yaoce_controlbox.TabIndex = 176
        '
        'BackgroundWorker_yaoce
        '
        Me.BackgroundWorker_yaoce.WorkerReportsProgress = True
        Me.BackgroundWorker_yaoce.WorkerSupportsCancellation = True
        '
        'rtb_inf
        '
        Me.rtb_inf.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rtb_inf.Location = New System.Drawing.Point(563, 3)
        Me.rtb_inf.Name = "rtb_inf"
        Me.rtb_inf.Size = New System.Drawing.Size(291, 297)
        Me.rtb_inf.TabIndex = 177
        Me.rtb_inf.Text = ""
        '
        'clear_text
        '
        Me.clear_text.ForeColor = System.Drawing.SystemColors.WindowText
        Me.clear_text.Location = New System.Drawing.Point(273, 241)
        Me.clear_text.Name = "clear_text"
        Me.clear_text.Size = New System.Drawing.Size(64, 20)
        Me.clear_text.TabIndex = 178
        Me.clear_text.Text = "清空"
        Me.clear_text.UseVisualStyleBackColor = True
        '
        'dgv_boxinf
        '
        Me.dgv_boxinf.AllowUserToAddRows = False
        Me.dgv_boxinf.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv_boxinf.BackgroundColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_boxinf.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dgv_boxinf.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_boxinf.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.id, Me.board_id, Me.box_id, Me.box_name, Me.xiangwei, Me.presure, Me.current, Me.power, Me.power_yinshu, Me.state, Me.yaoce_state})
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle4.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_boxinf.DefaultCellStyle = DataGridViewCellStyle4
        Me.dgv_boxinf.Location = New System.Drawing.Point(5, 302)
        Me.dgv_boxinf.Name = "dgv_boxinf"
        Me.dgv_boxinf.ReadOnly = True
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle5.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.dgv_boxinf.RowHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.dgv_boxinf.RowHeadersVisible = False
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_boxinf.RowsDefaultCellStyle = DataGridViewCellStyle6
        Me.dgv_boxinf.RowTemplate.Height = 23
        Me.dgv_boxinf.Size = New System.Drawing.Size(850, 279)
        Me.dgv_boxinf.TabIndex = 179
        '
        'id
        '
        Me.id.HeaderText = "编号"
        Me.id.Name = "id"
        Me.id.ReadOnly = True
        Me.id.Width = 60
        '
        'board_id
        '
        Me.board_id.HeaderText = "测量板编号"
        Me.board_id.Name = "board_id"
        Me.board_id.ReadOnly = True
        Me.board_id.Width = 88
        '
        'box_id
        '
        Me.box_id.HeaderText = "主控箱编号"
        Me.box_id.Name = "box_id"
        Me.box_id.ReadOnly = True
        Me.box_id.Visible = False
        '
        'box_name
        '
        Me.box_name.HeaderText = "主控箱名称"
        Me.box_name.Name = "box_name"
        Me.box_name.ReadOnly = True
        Me.box_name.Width = 90
        '
        'xiangwei
        '
        Me.xiangwei.HeaderText = "相位"
        Me.xiangwei.Name = "xiangwei"
        Me.xiangwei.ReadOnly = True
        Me.xiangwei.Width = 80
        '
        'presure
        '
        Me.presure.HeaderText = "电压(V)"
        Me.presure.Name = "presure"
        Me.presure.ReadOnly = True
        Me.presure.Width = 80
        '
        'current
        '
        Me.current.HeaderText = "电流(A)"
        Me.current.Name = "current"
        Me.current.ReadOnly = True
        Me.current.Width = 80
        '
        'power
        '
        Me.power.HeaderText = "功率(KW)"
        Me.power.Name = "power"
        Me.power.ReadOnly = True
        Me.power.Width = 80
        '
        'power_yinshu
        '
        Me.power_yinshu.HeaderText = "功率因数"
        Me.power_yinshu.Name = "power_yinshu"
        Me.power_yinshu.ReadOnly = True
        Me.power_yinshu.Width = 80
        '
        'state
        '
        Me.state.HeaderText = "数据状态"
        Me.state.Name = "state"
        Me.state.ReadOnly = True
        Me.state.Width = 95
        '
        'yaoce_state
        '
        Me.yaoce_state.HeaderText = "回复状态"
        Me.yaoce_state.Name = "yaoce_state"
        Me.yaoce_state.ReadOnly = True
        Me.yaoce_state.Width = 94
        '
        '遥测窗口
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(860, 586)
        Me.Controls.Add(Me.dgv_boxinf)
        Me.Controls.Add(Me.clear_text)
        Me.Controls.Add(Me.rtb_inf)
        Me.Controls.Add(Me.tv_yaoce_controlbox)
        Me.Controls.Add(Me.bt_stop)
        Me.Controls.Add(Me.bt_yaoce)
        Me.Controls.Add(Me.dgv_yaoce_list)
        Me.Controls.Add(Me.bt_del_boxname)
        Me.Controls.Add(Me.bt_add_boxname)
        Me.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "遥测窗口"
        Me.Text = "召测窗口"
        Me.TopMost = True
        Me.TransparencyKey = System.Drawing.Color.Azure
        CType(Me.dgv_yaoce_list, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgv_boxinf, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents bt_add_boxname As System.Windows.Forms.Button
    Friend WithEvents bt_del_boxname As System.Windows.Forms.Button
    Friend WithEvents dgv_yaoce_list As System.Windows.Forms.DataGridView
    Friend WithEvents bt_yaoce As System.Windows.Forms.Button
    Friend WithEvents bt_stop As System.Windows.Forms.Button
    Friend WithEvents tv_yaoce_controlbox As System.Windows.Forms.TreeView
    Friend WithEvents BackgroundWorker_yaoce As System.ComponentModel.BackgroundWorker
    Friend WithEvents rtb_inf As System.Windows.Forms.RichTextBox
    Friend WithEvents checkid As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents yaoce_boxname As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents clear_text As System.Windows.Forms.Button
    Friend WithEvents dgv_boxinf As System.Windows.Forms.DataGridView
    Friend WithEvents id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents board_id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents box_id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents box_name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents xiangwei As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents presure As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents current As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents power As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents power_yinshu As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents state As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents yaoce_state As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
