<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 单灯召测
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(单灯召测))
        Me.BackgroundWorker_yaoce = New System.ComponentModel.BackgroundWorker()
        Me.clear_text = New System.Windows.Forms.Button()
        Me.rtb_inf = New System.Windows.Forms.RichTextBox()
        Me.tv_yaoce_controlbox = New System.Windows.Forms.TreeView()
        Me.bt_yaoce = New System.Windows.Forms.Button()
        Me.dgv_yaoce_list = New System.Windows.Forms.DataGridView()
        Me.checkid = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.yaoce_boxname = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.bt_del_boxname = New System.Windows.Forms.Button()
        Me.bt_add_boxname = New System.Windows.Forms.Button()
        Me.lamp_id_part = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lamp_id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lampkind = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.open_close = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.power_lamp = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.yinshu_lamp = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.presure_lamp = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.presure_end = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.power1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.presure_l = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.current_lamp = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.state_content = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.denggan_id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.type_string = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.control_box_name = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.div_time_id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgv_lamp_state_list = New System.Windows.Forms.DataGridView()
        CType(Me.dgv_yaoce_list, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgv_lamp_state_list, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BackgroundWorker_yaoce
        '
        Me.BackgroundWorker_yaoce.WorkerReportsProgress = True
        Me.BackgroundWorker_yaoce.WorkerSupportsCancellation = True
        '
        'clear_text
        '
        Me.clear_text.Location = New System.Drawing.Point(238, 186)
        Me.clear_text.Name = "clear_text"
        Me.clear_text.Size = New System.Drawing.Size(64, 20)
        Me.clear_text.TabIndex = 196
        Me.clear_text.Text = "清空"
        Me.clear_text.UseVisualStyleBackColor = True
        '
        'rtb_inf
        '
        Me.rtb_inf.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rtb_inf.Location = New System.Drawing.Point(476, 2)
        Me.rtb_inf.Name = "rtb_inf"
        Me.rtb_inf.Size = New System.Drawing.Size(241, 242)
        Me.rtb_inf.TabIndex = 195
        Me.rtb_inf.Text = ""
        '
        'tv_yaoce_controlbox
        '
        Me.tv_yaoce_controlbox.CheckBoxes = True
        Me.tv_yaoce_controlbox.Location = New System.Drawing.Point(2, 2)
        Me.tv_yaoce_controlbox.Name = "tv_yaoce_controlbox"
        Me.tv_yaoce_controlbox.Size = New System.Drawing.Size(221, 242)
        Me.tv_yaoce_controlbox.TabIndex = 194
        '
        'bt_yaoce
        '
        Me.bt_yaoce.Location = New System.Drawing.Point(238, 128)
        Me.bt_yaoce.Name = "bt_yaoce"
        Me.bt_yaoce.Size = New System.Drawing.Size(64, 20)
        Me.bt_yaoce.TabIndex = 192
        Me.bt_yaoce.Text = "查询"
        Me.bt_yaoce.UseVisualStyleBackColor = True
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
        Me.dgv_yaoce_list.Location = New System.Drawing.Point(321, 2)
        Me.dgv_yaoce_list.Name = "dgv_yaoce_list"
        Me.dgv_yaoce_list.RowHeadersVisible = False
        Me.dgv_yaoce_list.RowHeadersWidth = 5
        Me.dgv_yaoce_list.RowTemplate.Height = 23
        Me.dgv_yaoce_list.Size = New System.Drawing.Size(149, 242)
        Me.dgv_yaoce_list.TabIndex = 191
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
        Me.yaoce_boxname.Width = 120
        '
        'bt_del_boxname
        '
        Me.bt_del_boxname.Location = New System.Drawing.Point(238, 73)
        Me.bt_del_boxname.Name = "bt_del_boxname"
        Me.bt_del_boxname.Size = New System.Drawing.Size(64, 20)
        Me.bt_del_boxname.TabIndex = 190
        Me.bt_del_boxname.Text = "删除<<"
        Me.bt_del_boxname.UseVisualStyleBackColor = True
        '
        'bt_add_boxname
        '
        Me.bt_add_boxname.Location = New System.Drawing.Point(238, 17)
        Me.bt_add_boxname.Name = "bt_add_boxname"
        Me.bt_add_boxname.Size = New System.Drawing.Size(64, 20)
        Me.bt_add_boxname.TabIndex = 189
        Me.bt_add_boxname.Text = "选择>>"
        Me.bt_add_boxname.UseVisualStyleBackColor = True
        '
        'lamp_id_part
        '
        Me.lamp_id_part.DataPropertyName = "lamp_id_part"
        Me.lamp_id_part.HeaderText = "终端编号"
        Me.lamp_id_part.Name = "lamp_id_part"
        Me.lamp_id_part.ReadOnly = True
        Me.lamp_id_part.Visible = False
        '
        'lamp_id
        '
        Me.lamp_id.DataPropertyName = "lamp_id"
        Me.lamp_id.HeaderText = "lamp_id"
        Me.lamp_id.Name = "lamp_id"
        Me.lamp_id.Visible = False
        '
        'lampkind
        '
        Me.lampkind.DataPropertyName = "jiechuqi_id"
        Me.lampkind.HeaderText = "jiechuqi_id"
        Me.lampkind.Name = "lampkind"
        Me.lampkind.Visible = False
        '
        'open_close
        '
        Me.open_close.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.open_close.HeaderText = "开关状态"
        Me.open_close.Name = "open_close"
        Me.open_close.Visible = False
        '
        'power_lamp
        '
        Me.power_lamp.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.power_lamp.HeaderText = "功率（W）"
        Me.power_lamp.Name = "power_lamp"
        Me.power_lamp.Width = 84
        '
        'yinshu_lamp
        '
        Me.yinshu_lamp.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.yinshu_lamp.HeaderText = "功率因数"
        Me.yinshu_lamp.Name = "yinshu_lamp"
        Me.yinshu_lamp.Width = 78
        '
        'presure_lamp
        '
        Me.presure_lamp.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.presure_lamp.HeaderText = "电压（V）"
        Me.presure_lamp.Name = "presure_lamp"
        Me.presure_lamp.Width = 84
        '
        'presure_end
        '
        Me.presure_end.DataPropertyName = "presure_end"
        Me.presure_end.HeaderText = "presure_end"
        Me.presure_end.Name = "presure_end"
        Me.presure_end.Visible = False
        '
        'power1
        '
        Me.power1.DataPropertyName = "power1"
        Me.power1.HeaderText = "power1"
        Me.power1.Name = "power1"
        Me.power1.Visible = False
        '
        'presure_l
        '
        Me.presure_l.DataPropertyName = "presure_l"
        Me.presure_l.HeaderText = "presure_l"
        Me.presure_l.Name = "presure_l"
        Me.presure_l.Visible = False
        '
        'current_lamp
        '
        Me.current_lamp.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.current_lamp.DataPropertyName = "current_l"
        Me.current_lamp.HeaderText = "电流（A）"
        Me.current_lamp.Name = "current_lamp"
        Me.current_lamp.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.current_lamp.Width = 65
        '
        'state_content
        '
        Me.state_content.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.state_content.HeaderText = "状态"
        Me.state_content.Name = "state_content"
        Me.state_content.Width = 80
        '
        'denggan_id
        '
        Me.denggan_id.HeaderText = "灯杆号"
        Me.denggan_id.Name = "denggan_id"
        '
        'type_string
        '
        Me.type_string.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.type_string.HeaderText = "类型"
        Me.type_string.Name = "type_string"
        '
        'control_box_name
        '
        Me.control_box_name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.control_box_name.DataPropertyName = "control_box_name"
        Me.control_box_name.HeaderText = "主控箱"
        Me.control_box_name.Name = "control_box_name"
        Me.control_box_name.Width = 80
        '
        'div_time_id
        '
        Me.div_time_id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.div_time_id.DataPropertyName = "div_time_id"
        Me.div_time_id.HeaderText = "div_time_id"
        Me.div_time_id.Name = "div_time_id"
        Me.div_time_id.Visible = False
        '
        'dgv_lamp_state_list
        '
        Me.dgv_lamp_state_list.AllowUserToAddRows = False
        Me.dgv_lamp_state_list.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv_lamp_state_list.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgv_lamp_state_list.BackgroundColor = System.Drawing.Color.WhiteSmoke
        Me.dgv_lamp_state_list.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle3.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.dgv_lamp_state_list.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dgv_lamp_state_list.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_lamp_state_list.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.div_time_id, Me.control_box_name, Me.type_string, Me.denggan_id, Me.state_content, Me.current_lamp, Me.presure_l, Me.power1, Me.presure_end, Me.presure_lamp, Me.yinshu_lamp, Me.power_lamp, Me.open_close, Me.lampkind, Me.lamp_id, Me.lamp_id_part})
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle4.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv_lamp_state_list.DefaultCellStyle = DataGridViewCellStyle4
        Me.dgv_lamp_state_list.Location = New System.Drawing.Point(1, 250)
        Me.dgv_lamp_state_list.MultiSelect = False
        Me.dgv_lamp_state_list.Name = "dgv_lamp_state_list"
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle5.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.dgv_lamp_state_list.RowHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.dgv_lamp_state_list.RowHeadersVisible = False
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window
        Me.dgv_lamp_state_list.RowsDefaultCellStyle = DataGridViewCellStyle6
        Me.dgv_lamp_state_list.RowTemplate.Height = 23
        Me.dgv_lamp_state_list.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv_lamp_state_list.Size = New System.Drawing.Size(717, 295)
        Me.dgv_lamp_state_list.TabIndex = 188
        '
        '单灯召测
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(719, 546)
        Me.Controls.Add(Me.clear_text)
        Me.Controls.Add(Me.rtb_inf)
        Me.Controls.Add(Me.tv_yaoce_controlbox)
        Me.Controls.Add(Me.bt_yaoce)
        Me.Controls.Add(Me.dgv_yaoce_list)
        Me.Controls.Add(Me.bt_del_boxname)
        Me.Controls.Add(Me.bt_add_boxname)
        Me.Controls.Add(Me.dgv_lamp_state_list)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "单灯召测"
        Me.Text = "单灯召测"
        CType(Me.dgv_yaoce_list, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgv_lamp_state_list, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BackgroundWorker_yaoce As System.ComponentModel.BackgroundWorker
    Friend WithEvents clear_text As System.Windows.Forms.Button
    Friend WithEvents rtb_inf As System.Windows.Forms.RichTextBox
    Friend WithEvents tv_yaoce_controlbox As System.Windows.Forms.TreeView
    Friend WithEvents bt_yaoce As System.Windows.Forms.Button
    Friend WithEvents dgv_yaoce_list As System.Windows.Forms.DataGridView
    Friend WithEvents bt_del_boxname As System.Windows.Forms.Button
    Friend WithEvents bt_add_boxname As System.Windows.Forms.Button
    Friend WithEvents checkid As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents yaoce_boxname As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lamp_id_part As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lamp_id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lampkind As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents open_close As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents power_lamp As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents yinshu_lamp As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents presure_lamp As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents presure_end As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents power1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents presure_l As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents current_lamp As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents state_content As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents denggan_id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents type_string As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents control_box_name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents div_time_id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgv_lamp_state_list As System.Windows.Forms.DataGridView
End Class
