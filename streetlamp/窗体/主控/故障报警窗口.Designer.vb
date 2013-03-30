<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 故障报警窗口
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
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.dgv_lamp_alarm = New System.Windows.Forms.DataGridView()
        Me.alarm_controlboxname = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.alarm_information = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.alarm_lampid = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.alarm_string = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.time = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgv_alarmlist = New System.Windows.Forms.DataGridView()
        Me.alarm_control_box_name = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.alarm_inf = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.alarm_time = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.dgv_lamp_alarm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgv_alarmlist, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(-1, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(772, 498)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.dgv_lamp_alarm)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(764, 472)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "单灯故障报警窗口"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.dgv_alarmlist)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(764, 472)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "三遥故障报警窗口"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'dgv_lamp_alarm
        '
        Me.dgv_lamp_alarm.AllowUserToAddRows = False
        Me.dgv_lamp_alarm.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgv_lamp_alarm.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.dgv_lamp_alarm.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv_lamp_alarm.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_lamp_alarm.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.alarm_controlboxname, Me.alarm_information, Me.alarm_lampid, Me.alarm_string, Me.time})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.Red
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv_lamp_alarm.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgv_lamp_alarm.Location = New System.Drawing.Point(0, 0)
        Me.dgv_lamp_alarm.Name = "dgv_lamp_alarm"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_lamp_alarm.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dgv_lamp_alarm.RowHeadersVisible = False
        Me.dgv_lamp_alarm.RowTemplate.Height = 23
        Me.dgv_lamp_alarm.Size = New System.Drawing.Size(761, 476)
        Me.dgv_lamp_alarm.TabIndex = 2
        '
        'alarm_controlboxname
        '
        Me.alarm_controlboxname.HeaderText = "主控箱名称"
        Me.alarm_controlboxname.Name = "alarm_controlboxname"
        '
        'alarm_information
        '
        Me.alarm_information.HeaderText = "节点备注"
        Me.alarm_information.Name = "alarm_information"
        Me.alarm_information.Visible = False
        '
        'alarm_lampid
        '
        Me.alarm_lampid.HeaderText = "节点编号"
        Me.alarm_lampid.Name = "alarm_lampid"
        '
        'alarm_string
        '
        Me.alarm_string.HeaderText = "报警信息"
        Me.alarm_string.Name = "alarm_string"
        '
        'time
        '
        Me.time.HeaderText = "开始时间"
        Me.time.Name = "time"
        '
        'dgv_alarmlist
        '
        Me.dgv_alarmlist.AllowUserToAddRows = False
        Me.dgv_alarmlist.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv_alarmlist.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgv_alarmlist.BackgroundColor = System.Drawing.Color.WhiteSmoke
        Me.dgv_alarmlist.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgv_alarmlist.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.dgv_alarmlist.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.dgv_alarmlist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_alarmlist.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.alarm_control_box_name, Me.alarm_inf, Me.alarm_time})
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.Red
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv_alarmlist.DefaultCellStyle = DataGridViewCellStyle5
        Me.dgv_alarmlist.Location = New System.Drawing.Point(0, 0)
        Me.dgv_alarmlist.Name = "dgv_alarmlist"
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle6.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.Padding = New System.Windows.Forms.Padding(41)
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_alarmlist.RowHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.dgv_alarmlist.RowHeadersVisible = False
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_alarmlist.RowsDefaultCellStyle = DataGridViewCellStyle7
        Me.dgv_alarmlist.RowTemplate.Height = 23
        Me.dgv_alarmlist.Size = New System.Drawing.Size(761, 472)
        Me.dgv_alarmlist.TabIndex = 1
        '
        'alarm_control_box_name
        '
        Me.alarm_control_box_name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.alarm_control_box_name.HeaderText = "主控箱名称"
        Me.alarm_control_box_name.Name = "alarm_control_box_name"
        Me.alarm_control_box_name.Width = 90
        '
        'alarm_inf
        '
        Me.alarm_inf.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.alarm_inf.HeaderText = "故障信息"
        Me.alarm_inf.Name = "alarm_inf"
        Me.alarm_inf.Width = 90
        '
        'alarm_time
        '
        Me.alarm_time.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.alarm_time.HeaderText = "故障时间"
        Me.alarm_time.Name = "alarm_time"
        Me.alarm_time.Width = 150
        '
        '故障报警窗口
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(766, 498)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "故障报警窗口"
        Me.Text = "故障报警窗口"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        CType(Me.dgv_lamp_alarm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgv_alarmlist, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents dgv_lamp_alarm As System.Windows.Forms.DataGridView
    Friend WithEvents alarm_controlboxname As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents alarm_information As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents alarm_lampid As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents alarm_string As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents time As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgv_alarmlist As System.Windows.Forms.DataGridView
    Friend WithEvents alarm_control_box_name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents alarm_inf As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents alarm_time As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
