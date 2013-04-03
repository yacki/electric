<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 单灯数据
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
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.dgv_lamp_state_list = New System.Windows.Forms.DataGridView()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.查询条件 = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.div_time_id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.control_box_name = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lamp_id_part = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lamp_pointinfor = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.type_string = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.denggan_id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.state_content = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.current_lamp = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.presure_l = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.power1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.presure_end = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.presure_lamp = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.yinshu_lamp = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.power_lamp = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.open_close = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lampkind = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lamp_id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.dgv_lamp_state_list, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.查询条件.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgv_lamp_state_list
        '
        Me.dgv_lamp_state_list.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv_lamp_state_list.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgv_lamp_state_list.BackgroundColor = System.Drawing.Color.WhiteSmoke
        Me.dgv_lamp_state_list.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle1.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.dgv_lamp_state_list.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv_lamp_state_list.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_lamp_state_list.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.div_time_id, Me.control_box_name, Me.lamp_id_part, Me.lamp_pointinfor, Me.type_string, Me.denggan_id, Me.state_content, Me.current_lamp, Me.presure_l, Me.power1, Me.presure_end, Me.presure_lamp, Me.yinshu_lamp, Me.power_lamp, Me.open_close, Me.lampkind, Me.lamp_id})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv_lamp_state_list.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgv_lamp_state_list.Location = New System.Drawing.Point(2, 79)
        Me.dgv_lamp_state_list.MultiSelect = False
        Me.dgv_lamp_state_list.Name = "dgv_lamp_state_list"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle3.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_lamp_state_list.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dgv_lamp_state_list.RowHeadersVisible = False
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window
        Me.dgv_lamp_state_list.RowsDefaultCellStyle = DataGridViewCellStyle4
        Me.dgv_lamp_state_list.RowTemplate.Height = 23
        Me.dgv_lamp_state_list.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv_lamp_state_list.Size = New System.Drawing.Size(764, 451)
        Me.dgv_lamp_state_list.TabIndex = 3
        '
        'Timer1
        '
        '
        '查询条件
        '
        Me.查询条件.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.查询条件.BackColor = System.Drawing.Color.WhiteSmoke
        Me.查询条件.Controls.Add(Me.Label1)
        Me.查询条件.Controls.Add(Me.Button1)
        Me.查询条件.Controls.Add(Me.ComboBox1)
        Me.查询条件.Location = New System.Drawing.Point(2, 2)
        Me.查询条件.Name = "查询条件"
        Me.查询条件.Size = New System.Drawing.Size(764, 71)
        Me.查询条件.TabIndex = 8
        Me.查询条件.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 30)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 12)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "网关名称："
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(226, 24)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(84, 23)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "查询"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(99, 26)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(105, 20)
        Me.ComboBox1.TabIndex = 2
        '
        'div_time_id
        '
        Me.div_time_id.DataPropertyName = "div_time_id"
        Me.div_time_id.HeaderText = "div_time_id"
        Me.div_time_id.Name = "div_time_id"
        Me.div_time_id.Visible = False
        '
        'control_box_name
        '
        Me.control_box_name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.control_box_name.DataPropertyName = "control_box_name"
        Me.control_box_name.HeaderText = "网关"
        Me.control_box_name.Name = "control_box_name"
        '
        'lamp_id_part
        '
        Me.lamp_id_part.DataPropertyName = "lamp_id_part"
        Me.lamp_id_part.HeaderText = "终端编号"
        Me.lamp_id_part.Name = "lamp_id_part"
        Me.lamp_id_part.ReadOnly = True
        '
        'lamp_pointinfor
        '
        Me.lamp_pointinfor.DataPropertyName = "lamp_pointinfor"
        Me.lamp_pointinfor.HeaderText = "联系电话"
        Me.lamp_pointinfor.Name = "lamp_pointinfor"
        '
        'type_string
        '
        Me.type_string.DataPropertyName = "type_string"
        Me.type_string.HeaderText = "类型"
        Me.type_string.Name = "type_string"
        Me.type_string.Visible = False
        '
        'denggan_id
        '
        Me.denggan_id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.denggan_id.HeaderText = "灯杆号"
        Me.denggan_id.Name = "denggan_id"
        Me.denggan_id.Visible = False
        Me.denggan_id.Width = 160
        '
        'state_content
        '
        Me.state_content.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.state_content.HeaderText = "状态"
        Me.state_content.Name = "state_content"
        '
        'current_lamp
        '
        Me.current_lamp.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.current_lamp.DataPropertyName = "current_l"
        Me.current_lamp.HeaderText = "设备组号"
        Me.current_lamp.Name = "current_lamp"
        Me.current_lamp.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.current_lamp.Width = 95
        '
        'presure_l
        '
        Me.presure_l.DataPropertyName = "presure_l"
        Me.presure_l.HeaderText = "presure_l"
        Me.presure_l.Name = "presure_l"
        Me.presure_l.Visible = False
        '
        'power1
        '
        Me.power1.DataPropertyName = "power1"
        Me.power1.HeaderText = "power1"
        Me.power1.Name = "power1"
        Me.power1.Visible = False
        '
        'presure_end
        '
        Me.presure_end.DataPropertyName = "presure_end"
        Me.presure_end.HeaderText = "presure_end"
        Me.presure_end.Name = "presure_end"
        Me.presure_end.Visible = False
        '
        'presure_lamp
        '
        Me.presure_lamp.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.presure_lamp.HeaderText = "安装地点"
        Me.presure_lamp.Name = "presure_lamp"
        Me.presure_lamp.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.presure_lamp.Width = 95
        '
        'yinshu_lamp
        '
        Me.yinshu_lamp.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.yinshu_lamp.HeaderText = "上报时间"
        Me.yinshu_lamp.Name = "yinshu_lamp"
        Me.yinshu_lamp.Width = 90
        '
        'power_lamp
        '
        Me.power_lamp.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.power_lamp.HeaderText = "手机号"
        Me.power_lamp.Name = "power_lamp"
        Me.power_lamp.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.power_lamp.Width = 95
        '
        'open_close
        '
        Me.open_close.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.open_close.HeaderText = "开关状态"
        Me.open_close.Name = "open_close"
        Me.open_close.Visible = False
        Me.open_close.Width = 95
        '
        'lampkind
        '
        Me.lampkind.DataPropertyName = "jiechuqi_id"
        Me.lampkind.HeaderText = "jiechuqi_id"
        Me.lampkind.Name = "lampkind"
        Me.lampkind.Visible = False
        '
        'lamp_id
        '
        Me.lamp_id.DataPropertyName = "lamp_id"
        Me.lamp_id.HeaderText = "lamp_id"
        Me.lamp_id.Name = "lamp_id"
        Me.lamp_id.Visible = False
        '
        '单灯数据
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(765, 531)
        Me.Controls.Add(Me.查询条件)
        Me.Controls.Add(Me.dgv_lamp_state_list)
        Me.Name = "单灯数据"
        Me.Text = "终端监测"
        CType(Me.dgv_lamp_state_list, System.ComponentModel.ISupportInitialize).EndInit()
        Me.查询条件.ResumeLayout(False)
        Me.查询条件.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgv_lamp_state_list As System.Windows.Forms.DataGridView
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents 查询条件 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents div_time_id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents control_box_name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lamp_id_part As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lamp_pointinfor As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents type_string As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents denggan_id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents state_content As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents current_lamp As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents presure_l As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents power1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents presure_end As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents presure_lamp As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents yinshu_lamp As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents power_lamp As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents open_close As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lampkind As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lamp_id As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
