<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SelectAlarmType
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
        Me.clb_alarmtype_list = New System.Windows.Forms.CheckedListBox()
        Me.GBSelect = New System.Windows.Forms.GroupBox()
        Me.cb_checkall = New System.Windows.Forms.CheckBox()
        Me.bt_savealarm_type = New System.Windows.Forms.Button()
        Me.GBAdd = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.bt_delalarm_type = New System.Windows.Forms.Button()
        Me.bt_addtype = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cb_alarmlist_tag = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dgv_alarmlist = New System.Windows.Forms.DataGridView()
        Me.checkid = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Alarmtype = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Alarmtag = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.check_tag = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AlarmtypelistBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.TypeListDataSet = New streetlamp.TypeListDataSet()
        Me.tb_alarmstring = New System.Windows.Forms.TextBox()
        Me.cb_alarmtype_list = New System.Windows.Forms.ComboBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.bt_showinf = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cb_street_showtype = New System.Windows.Forms.ComboBox()
        Me.cb_area_showtype = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cb_city_showtype = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.box_add_string = New System.Windows.Forms.Label()
        Me.cb_box_showtype = New System.Windows.Forms.ComboBox()
        Me.Alarm_typelistTableAdapter = New streetlamp.TypeListDataSetTableAdapters.alarm_typelistTableAdapter()
        Me.GBSelect.SuspendLayout()
        Me.GBAdd.SuspendLayout()
        CType(Me.dgv_alarmlist, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AlarmtypelistBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TypeListDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'clb_alarmtype_list
        '
        Me.clb_alarmtype_list.FormattingEnabled = True
        Me.clb_alarmtype_list.Location = New System.Drawing.Point(6, 20)
        Me.clb_alarmtype_list.Name = "clb_alarmtype_list"
        Me.clb_alarmtype_list.Size = New System.Drawing.Size(221, 292)
        Me.clb_alarmtype_list.TabIndex = 0
        '
        'GBSelect
        '
        Me.GBSelect.Controls.Add(Me.cb_checkall)
        Me.GBSelect.Controls.Add(Me.bt_savealarm_type)
        Me.GBSelect.Controls.Add(Me.clb_alarmtype_list)
        Me.GBSelect.Enabled = False
        Me.GBSelect.Location = New System.Drawing.Point(12, 82)
        Me.GBSelect.Name = "GBSelect"
        Me.GBSelect.Size = New System.Drawing.Size(241, 389)
        Me.GBSelect.TabIndex = 1
        Me.GBSelect.TabStop = False
        Me.GBSelect.Text = "请选择报警类型"
        '
        'cb_checkall
        '
        Me.cb_checkall.AutoSize = True
        Me.cb_checkall.Location = New System.Drawing.Point(9, 332)
        Me.cb_checkall.Name = "cb_checkall"
        Me.cb_checkall.Size = New System.Drawing.Size(48, 16)
        Me.cb_checkall.TabIndex = 2
        Me.cb_checkall.Text = "全选"
        Me.cb_checkall.UseVisualStyleBackColor = True
        '
        'bt_savealarm_type
        '
        Me.bt_savealarm_type.Location = New System.Drawing.Point(91, 360)
        Me.bt_savealarm_type.Name = "bt_savealarm_type"
        Me.bt_savealarm_type.Size = New System.Drawing.Size(64, 20)
        Me.bt_savealarm_type.TabIndex = 1
        Me.bt_savealarm_type.Text = "保存"
        Me.bt_savealarm_type.UseVisualStyleBackColor = True
        '
        'GBAdd
        '
        Me.GBAdd.Controls.Add(Me.Label3)
        Me.GBAdd.Controls.Add(Me.bt_delalarm_type)
        Me.GBAdd.Controls.Add(Me.bt_addtype)
        Me.GBAdd.Controls.Add(Me.Label2)
        Me.GBAdd.Controls.Add(Me.cb_alarmlist_tag)
        Me.GBAdd.Controls.Add(Me.Label1)
        Me.GBAdd.Controls.Add(Me.dgv_alarmlist)
        Me.GBAdd.Controls.Add(Me.tb_alarmstring)
        Me.GBAdd.Controls.Add(Me.cb_alarmtype_list)
        Me.GBAdd.Enabled = False
        Me.GBAdd.Location = New System.Drawing.Point(258, 82)
        Me.GBAdd.Name = "GBAdd"
        Me.GBAdd.Size = New System.Drawing.Size(404, 389)
        Me.GBAdd.TabIndex = 2
        Me.GBAdd.TabStop = False
        Me.GBAdd.Text = "增加报警类型"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(277, 30)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 12)
        Me.Label3.TabIndex = 20
        Me.Label3.Text = "状态标志："
        '
        'bt_delalarm_type
        '
        Me.bt_delalarm_type.Location = New System.Drawing.Point(175, 360)
        Me.bt_delalarm_type.Name = "bt_delalarm_type"
        Me.bt_delalarm_type.Size = New System.Drawing.Size(64, 20)
        Me.bt_delalarm_type.TabIndex = 19
        Me.bt_delalarm_type.Text = "删除"
        Me.bt_delalarm_type.UseVisualStyleBackColor = True
        '
        'bt_addtype
        '
        Me.bt_addtype.Location = New System.Drawing.Point(175, 64)
        Me.bt_addtype.Name = "bt_addtype"
        Me.bt_addtype.Size = New System.Drawing.Size(64, 20)
        Me.bt_addtype.TabIndex = 17
        Me.bt_addtype.Text = "新增"
        Me.bt_addtype.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(159, 30)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(11, 12)
        Me.Label2.TabIndex = 16
        Me.Label2.Text = "-"
        '
        'cb_alarmlist_tag
        '
        Me.cb_alarmlist_tag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_alarmlist_tag.FormattingEnabled = True
        Me.cb_alarmlist_tag.Items.AddRange(New Object() {"0", "1"})
        Me.cb_alarmlist_tag.Location = New System.Drawing.Point(348, 27)
        Me.cb_alarmlist_tag.Name = "cb_alarmlist_tag"
        Me.cb_alarmlist_tag.Size = New System.Drawing.Size(51, 20)
        Me.cb_alarmlist_tag.TabIndex = 15
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(15, 30)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 12)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "类型："
        '
        'dgv_alarmlist
        '
        Me.dgv_alarmlist.AllowUserToAddRows = False
        Me.dgv_alarmlist.AutoGenerateColumns = False
        Me.dgv_alarmlist.BackgroundColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_alarmlist.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv_alarmlist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_alarmlist.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.checkid, Me.id, Me.Alarmtype, Me.Alarmtag, Me.check_tag})
        Me.dgv_alarmlist.DataSource = Me.AlarmtypelistBindingSource
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv_alarmlist.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgv_alarmlist.Location = New System.Drawing.Point(5, 107)
        Me.dgv_alarmlist.Name = "dgv_alarmlist"
        Me.dgv_alarmlist.RowHeadersVisible = False
        Me.dgv_alarmlist.RowTemplate.Height = 23
        Me.dgv_alarmlist.Size = New System.Drawing.Size(393, 240)
        Me.dgv_alarmlist.TabIndex = 13
        '
        'checkid
        '
        Me.checkid.FalseValue = "0"
        Me.checkid.HeaderText = ""
        Me.checkid.Name = "checkid"
        Me.checkid.TrueValue = "1"
        Me.checkid.Width = 30
        '
        'id
        '
        Me.id.DataPropertyName = "id"
        Me.id.HeaderText = "id"
        Me.id.Name = "id"
        Me.id.ReadOnly = True
        Me.id.Visible = False
        '
        'Alarmtype
        '
        Me.Alarmtype.DataPropertyName = "alarm_type"
        Me.Alarmtype.HeaderText = "自定义报警类型"
        Me.Alarmtype.Name = "Alarmtype"
        Me.Alarmtype.ReadOnly = True
        Me.Alarmtype.Width = 230
        '
        'Alarmtag
        '
        Me.Alarmtag.DataPropertyName = "alarm_tag"
        Me.Alarmtag.HeaderText = "报警标志"
        Me.Alarmtag.Name = "Alarmtag"
        '
        'check_tag
        '
        Me.check_tag.DataPropertyName = "check_tag"
        Me.check_tag.HeaderText = "是否报警"
        Me.check_tag.Name = "check_tag"
        Me.check_tag.ReadOnly = True
        '
        'AlarmtypelistBindingSource
        '
        Me.AlarmtypelistBindingSource.DataMember = "alarm_typelist"
        Me.AlarmtypelistBindingSource.DataSource = Me.TypeListDataSet
        '
        'TypeListDataSet
        '
        Me.TypeListDataSet.DataSetName = "TypeListDataSet"
        Me.TypeListDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'tb_alarmstring
        '
        Me.tb_alarmstring.Location = New System.Drawing.Point(175, 26)
        Me.tb_alarmstring.Name = "tb_alarmstring"
        Me.tb_alarmstring.Size = New System.Drawing.Size(76, 21)
        Me.tb_alarmstring.TabIndex = 12
        '
        'cb_alarmtype_list
        '
        Me.cb_alarmtype_list.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_alarmtype_list.DropDownWidth = 200
        Me.cb_alarmtype_list.FormattingEnabled = True
        Me.cb_alarmtype_list.Location = New System.Drawing.Point(62, 27)
        Me.cb_alarmtype_list.Name = "cb_alarmtype_list"
        Me.cb_alarmtype_list.Size = New System.Drawing.Size(92, 20)
        Me.cb_alarmtype_list.TabIndex = 6
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.bt_showinf)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.cb_street_showtype)
        Me.GroupBox3.Controls.Add(Me.cb_area_showtype)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Controls.Add(Me.cb_city_showtype)
        Me.GroupBox3.Controls.Add(Me.Label10)
        Me.GroupBox3.Controls.Add(Me.box_add_string)
        Me.GroupBox3.Controls.Add(Me.cb_box_showtype)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 0)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(650, 77)
        Me.GroupBox3.TabIndex = 3
        Me.GroupBox3.TabStop = False
        '
        'bt_showinf
        '
        Me.bt_showinf.Location = New System.Drawing.Point(263, 52)
        Me.bt_showinf.Name = "bt_showinf"
        Me.bt_showinf.Size = New System.Drawing.Size(64, 20)
        Me.bt_showinf.TabIndex = 175
        Me.bt_showinf.Text = "查询"
        Me.bt_showinf.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label6.Location = New System.Drawing.Point(344, 22)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(59, 12)
        Me.Label6.TabIndex = 173
        Me.Label6.Text = "街道名称:"
        '
        'cb_street_showtype
        '
        Me.cb_street_showtype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_street_showtype.DropDownWidth = 120
        Me.cb_street_showtype.FormattingEnabled = True
        Me.cb_street_showtype.Location = New System.Drawing.Point(409, 19)
        Me.cb_street_showtype.Name = "cb_street_showtype"
        Me.cb_street_showtype.Size = New System.Drawing.Size(64, 20)
        Me.cb_street_showtype.TabIndex = 174
        '
        'cb_area_showtype
        '
        Me.cb_area_showtype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_area_showtype.DropDownWidth = 120
        Me.cb_area_showtype.FormattingEnabled = True
        Me.cb_area_showtype.Location = New System.Drawing.Point(246, 19)
        Me.cb_area_showtype.Name = "cb_area_showtype"
        Me.cb_area_showtype.Size = New System.Drawing.Size(64, 20)
        Me.cb_area_showtype.TabIndex = 172
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label5.Location = New System.Drawing.Point(176, 22)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(65, 12)
        Me.Label5.TabIndex = 171
        Me.Label5.Text = "区域名称："
        '
        'cb_city_showtype
        '
        Me.cb_city_showtype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_city_showtype.DropDownWidth = 120
        Me.cb_city_showtype.FormattingEnabled = True
        Me.cb_city_showtype.Location = New System.Drawing.Point(71, 19)
        Me.cb_city_showtype.Name = "cb_city_showtype"
        Me.cb_city_showtype.Size = New System.Drawing.Size(64, 20)
        Me.cb_city_showtype.TabIndex = 170
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label10.Location = New System.Drawing.Point(6, 22)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(59, 12)
        Me.Label10.TabIndex = 169
        Me.Label10.Text = "城市名称:"
        '
        'box_add_string
        '
        Me.box_add_string.AutoSize = True
        Me.box_add_string.BackColor = System.Drawing.Color.Transparent
        Me.box_add_string.Location = New System.Drawing.Point(499, 22)
        Me.box_add_string.Name = "box_add_string"
        Me.box_add_string.Size = New System.Drawing.Size(71, 12)
        Me.box_add_string.TabIndex = 167
        Me.box_add_string.Text = "主控箱名称:"
        '
        'cb_box_showtype
        '
        Me.cb_box_showtype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_box_showtype.DropDownWidth = 120
        Me.cb_box_showtype.FormattingEnabled = True
        Me.cb_box_showtype.Location = New System.Drawing.Point(576, 19)
        Me.cb_box_showtype.Name = "cb_box_showtype"
        Me.cb_box_showtype.Size = New System.Drawing.Size(64, 20)
        Me.cb_box_showtype.TabIndex = 168
        '
        'Alarm_typelistTableAdapter
        '
        Me.Alarm_typelistTableAdapter.ClearBeforeFill = True
        '
        'SelectAlarmType
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(667, 484)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GBAdd)
        Me.Controls.Add(Me.GBSelect)
        Me.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Name = "SelectAlarmType"
        Me.Text = "选择报警类型"
        Me.GBSelect.ResumeLayout(False)
        Me.GBSelect.PerformLayout()
        Me.GBAdd.ResumeLayout(False)
        Me.GBAdd.PerformLayout()
        CType(Me.dgv_alarmlist, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AlarmtypelistBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TypeListDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents clb_alarmtype_list As System.Windows.Forms.CheckedListBox
    Friend WithEvents GBSelect As System.Windows.Forms.GroupBox
    Friend WithEvents bt_savealarm_type As System.Windows.Forms.Button
    Friend WithEvents GBAdd As System.Windows.Forms.GroupBox
    Friend WithEvents tb_alarmstring As System.Windows.Forms.TextBox
    Friend WithEvents cb_alarmtype_list As System.Windows.Forms.ComboBox
    Friend WithEvents dgv_alarmlist As System.Windows.Forms.DataGridView
    Friend WithEvents TypeListDataSet As streetlamp.TypeListDataSet
    Friend WithEvents AlarmtypelistBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents Alarm_typelistTableAdapter As streetlamp.TypeListDataSetTableAdapters.alarm_typelistTableAdapter
    Friend WithEvents cb_alarmlist_tag As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents bt_delalarm_type As System.Windows.Forms.Button
    Friend WithEvents bt_addtype As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cb_street_showtype As System.Windows.Forms.ComboBox
    Friend WithEvents cb_area_showtype As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cb_city_showtype As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents box_add_string As System.Windows.Forms.Label
    Friend WithEvents cb_box_showtype As System.Windows.Forms.ComboBox
    Friend WithEvents bt_showinf As System.Windows.Forms.Button
    Friend WithEvents checkid As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Alarmtype As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Alarmtag As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents check_tag As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cb_checkall As System.Windows.Forms.CheckBox
End Class
