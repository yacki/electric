<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 流量日志
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(流量日志))
        Me.GPRS_list = New System.Windows.Forms.DataGridView()
        Me.IDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ControlboxidDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GPRSDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TimeDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BoxGPRSBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.GPRS = New streetlamp.GPRS()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.bt_find_record = New System.Windows.Forms.Button()
        Me.control_box_string = New System.Windows.Forms.Label()
        Me.cb_control_box_name = New System.Windows.Forms.ComboBox()
        Me.rb_date_condition_find = New System.Windows.Forms.RadioButton()
        Me.dtp_date_end = New System.Windows.Forms.DateTimePicker()
        Me.rb_no_date_condition_find = New System.Windows.Forms.RadioButton()
        Me.Date_end_string = New System.Windows.Forms.Label()
        Me.dtp_date_start = New System.Windows.Forms.DateTimePicker()
        Me.Date_start_string = New System.Windows.Forms.Label()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.GPRS_total = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Box_GPRSTableAdapter = New streetlamp.GPRSTableAdapters.Box_GPRSTableAdapter()
        CType(Me.GPRS_list, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BoxGPRSBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GPRS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GPRS_list
        '
        Me.GPRS_list.AllowUserToAddRows = False
        Me.GPRS_list.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GPRS_list.AutoGenerateColumns = False
        Me.GPRS_list.BackgroundColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GPRS_list.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.GPRS_list.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GPRS_list.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IDDataGridViewTextBoxColumn, Me.ControlboxidDataGridViewTextBoxColumn, Me.GPRSDataGridViewTextBoxColumn, Me.TimeDataGridViewTextBoxColumn})
        Me.GPRS_list.DataSource = Me.BoxGPRSBindingSource
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GPRS_list.DefaultCellStyle = DataGridViewCellStyle2
        Me.GPRS_list.Location = New System.Drawing.Point(264, 10)
        Me.GPRS_list.Name = "GPRS_list"
        Me.GPRS_list.RowTemplate.Height = 23
        Me.GPRS_list.Size = New System.Drawing.Size(504, 423)
        Me.GPRS_list.TabIndex = 0
        '
        'IDDataGridViewTextBoxColumn
        '
        Me.IDDataGridViewTextBoxColumn.DataPropertyName = "ID"
        Me.IDDataGridViewTextBoxColumn.HeaderText = "ID"
        Me.IDDataGridViewTextBoxColumn.Name = "IDDataGridViewTextBoxColumn"
        Me.IDDataGridViewTextBoxColumn.ReadOnly = True
        '
        'ControlboxidDataGridViewTextBoxColumn
        '
        Me.ControlboxidDataGridViewTextBoxColumn.DataPropertyName = "control_box_id"
        Me.ControlboxidDataGridViewTextBoxColumn.HeaderText = "区域编号"
        Me.ControlboxidDataGridViewTextBoxColumn.Name = "ControlboxidDataGridViewTextBoxColumn"
        '
        'GPRSDataGridViewTextBoxColumn
        '
        Me.GPRSDataGridViewTextBoxColumn.DataPropertyName = "GPRS"
        Me.GPRSDataGridViewTextBoxColumn.HeaderText = "流量（单位：字节）"
        Me.GPRSDataGridViewTextBoxColumn.Name = "GPRSDataGridViewTextBoxColumn"
        Me.GPRSDataGridViewTextBoxColumn.Width = 200
        '
        'TimeDataGridViewTextBoxColumn
        '
        Me.TimeDataGridViewTextBoxColumn.DataPropertyName = "Time"
        Me.TimeDataGridViewTextBoxColumn.HeaderText = "时间"
        Me.TimeDataGridViewTextBoxColumn.Name = "TimeDataGridViewTextBoxColumn"
        Me.TimeDataGridViewTextBoxColumn.Width = 150
        '
        'BoxGPRSBindingSource
        '
        Me.BoxGPRSBindingSource.DataMember = "Box_GPRS"
        Me.BoxGPRSBindingSource.DataSource = Me.GPRS
        '
        'GPRS
        '
        Me.GPRS.DataSetName = "GPRS"
        Me.GPRS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.bt_find_record)
        Me.GroupBox1.Controls.Add(Me.control_box_string)
        Me.GroupBox1.Controls.Add(Me.cb_control_box_name)
        Me.GroupBox1.Controls.Add(Me.rb_date_condition_find)
        Me.GroupBox1.Controls.Add(Me.dtp_date_end)
        Me.GroupBox1.Controls.Add(Me.rb_no_date_condition_find)
        Me.GroupBox1.Controls.Add(Me.Date_end_string)
        Me.GroupBox1.Controls.Add(Me.dtp_date_start)
        Me.GroupBox1.Controls.Add(Me.Date_start_string)
        Me.GroupBox1.Location = New System.Drawing.Point(10, 10)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(241, 206)
        Me.GroupBox1.TabIndex = 113
        Me.GroupBox1.TabStop = False
        '
        'bt_find_record
        '
        Me.bt_find_record.Location = New System.Drawing.Point(79, 165)
        Me.bt_find_record.Name = "bt_find_record"
        Me.bt_find_record.Size = New System.Drawing.Size(75, 23)
        Me.bt_find_record.TabIndex = 115
        Me.bt_find_record.Text = "查询"
        Me.bt_find_record.UseVisualStyleBackColor = True
        '
        'control_box_string
        '
        Me.control_box_string.AutoSize = True
        Me.control_box_string.BackColor = System.Drawing.Color.Transparent
        Me.control_box_string.Location = New System.Drawing.Point(17, 128)
        Me.control_box_string.Name = "control_box_string"
        Me.control_box_string.Size = New System.Drawing.Size(65, 12)
        Me.control_box_string.TabIndex = 112
        Me.control_box_string.Text = "区域名称："
        '
        'cb_control_box_name
        '
        Me.cb_control_box_name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_control_box_name.FormattingEnabled = True
        Me.cb_control_box_name.Location = New System.Drawing.Point(88, 125)
        Me.cb_control_box_name.Name = "cb_control_box_name"
        Me.cb_control_box_name.Size = New System.Drawing.Size(136, 20)
        Me.cb_control_box_name.TabIndex = 113
        '
        'rb_date_condition_find
        '
        Me.rb_date_condition_find.AutoSize = True
        Me.rb_date_condition_find.BackColor = System.Drawing.Color.Transparent
        Me.rb_date_condition_find.Checked = True
        Me.rb_date_condition_find.Location = New System.Drawing.Point(20, 20)
        Me.rb_date_condition_find.Name = "rb_date_condition_find"
        Me.rb_date_condition_find.Size = New System.Drawing.Size(83, 16)
        Me.rb_date_condition_find.TabIndex = 110
        Me.rb_date_condition_find.TabStop = True
        Me.rb_date_condition_find.Text = "按日期查询"
        Me.rb_date_condition_find.UseVisualStyleBackColor = False
        '
        'dtp_date_end
        '
        Me.dtp_date_end.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_date_end.Location = New System.Drawing.Point(88, 92)
        Me.dtp_date_end.Name = "dtp_date_end"
        Me.dtp_date_end.Size = New System.Drawing.Size(136, 21)
        Me.dtp_date_end.TabIndex = 109
        '
        'rb_no_date_condition_find
        '
        Me.rb_no_date_condition_find.AutoSize = True
        Me.rb_no_date_condition_find.BackColor = System.Drawing.Color.Transparent
        Me.rb_no_date_condition_find.Location = New System.Drawing.Point(124, 20)
        Me.rb_no_date_condition_find.Name = "rb_no_date_condition_find"
        Me.rb_no_date_condition_find.Size = New System.Drawing.Size(83, 16)
        Me.rb_no_date_condition_find.TabIndex = 111
        Me.rb_no_date_condition_find.Text = "无日期查询"
        Me.rb_no_date_condition_find.UseVisualStyleBackColor = False
        '
        'Date_end_string
        '
        Me.Date_end_string.AutoSize = True
        Me.Date_end_string.BackColor = System.Drawing.Color.Transparent
        Me.Date_end_string.Location = New System.Drawing.Point(17, 95)
        Me.Date_end_string.Name = "Date_end_string"
        Me.Date_end_string.Size = New System.Drawing.Size(65, 12)
        Me.Date_end_string.TabIndex = 107
        Me.Date_end_string.Text = "结束日期："
        '
        'dtp_date_start
        '
        Me.dtp_date_start.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_date_start.Location = New System.Drawing.Point(88, 58)
        Me.dtp_date_start.Name = "dtp_date_start"
        Me.dtp_date_start.Size = New System.Drawing.Size(136, 21)
        Me.dtp_date_start.TabIndex = 108
        '
        'Date_start_string
        '
        Me.Date_start_string.AutoSize = True
        Me.Date_start_string.BackColor = System.Drawing.Color.Transparent
        Me.Date_start_string.Location = New System.Drawing.Point(17, 62)
        Me.Date_start_string.Name = "Date_start_string"
        Me.Date_start_string.Size = New System.Drawing.Size(65, 12)
        Me.Date_start_string.TabIndex = 106
        Me.Date_start_string.Text = "开始日期："
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.GPRS_total})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 433)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Padding = New System.Windows.Forms.Padding(1, 0, 12, 0)
        Me.StatusStrip1.Size = New System.Drawing.Size(778, 22)
        Me.StatusStrip1.TabIndex = 115
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'GPRS_total
        '
        Me.GPRS_total.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.GPRS_total.Name = "GPRS_total"
        Me.GPRS_total.Size = New System.Drawing.Size(154, 17)
        Me.GPRS_total.Text = "ToolStripStatusLabel1"
        '
        'Box_GPRSTableAdapter
        '
        Me.Box_GPRSTableAdapter.ClearBeforeFill = True
        '
        '流量日志
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(778, 455)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GPRS_list)
        Me.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "流量日志"
        Me.Text = "流量日志"
        CType(Me.GPRS_list, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BoxGPRSBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GPRS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GPRS_list As System.Windows.Forms.DataGridView
    Friend WithEvents GPRS As streetlamp.GPRS
    Friend WithEvents BoxGPRSBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents Box_GPRSTableAdapter As streetlamp.GPRSTableAdapters.Box_GPRSTableAdapter
    Friend WithEvents IDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ControlboxidDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GPRSDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TimeDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rb_date_condition_find As System.Windows.Forms.RadioButton
    Friend WithEvents dtp_date_end As System.Windows.Forms.DateTimePicker
    Friend WithEvents rb_no_date_condition_find As System.Windows.Forms.RadioButton
    Friend WithEvents Date_end_string As System.Windows.Forms.Label
    Friend WithEvents dtp_date_start As System.Windows.Forms.DateTimePicker
    Friend WithEvents Date_start_string As System.Windows.Forms.Label
    Friend WithEvents bt_find_record As System.Windows.Forms.Button
    Friend WithEvents control_box_string As System.Windows.Forms.Label
    Friend WithEvents cb_control_box_name As System.Windows.Forms.ComboBox
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents GPRS_total As System.Windows.Forms.ToolStripStatusLabel
End Class
