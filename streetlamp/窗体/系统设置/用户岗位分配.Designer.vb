<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 用户岗位分配
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(用户岗位分配))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cb_gangweilist = New System.Windows.Forms.ComboBox()
        Me.bt_add_rights = New System.Windows.Forms.Button()
        Me.bt_delete_rights = New System.Windows.Forms.Button()
        Me.bt_close_rights = New System.Windows.Forms.Button()
        Me.dgv_rights_list = New System.Windows.Forms.DataGridView()
        Me.checkid = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.IDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RightsDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RightsdescDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RightsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Rights = New streetlamp.Rights()
        Me.RightsTableAdapter = New streetlamp.RightsTableAdapters.RightsTableAdapter()
        Me.bt_save_right = New System.Windows.Forms.Button()
        Me.clb_addrights_list = New System.Windows.Forms.CheckedListBox()
        CType(Me.dgv_rights_list, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RightsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Rights, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(21, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(29, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "岗位"
        '
        'cb_gangweilist
        '
        Me.cb_gangweilist.FormattingEnabled = True
        Me.cb_gangweilist.Location = New System.Drawing.Point(73, 10)
        Me.cb_gangweilist.Name = "cb_gangweilist"
        Me.cb_gangweilist.Size = New System.Drawing.Size(104, 20)
        Me.cb_gangweilist.TabIndex = 1
        '
        'bt_add_rights
        '
        Me.bt_add_rights.Location = New System.Drawing.Point(267, 79)
        Me.bt_add_rights.Name = "bt_add_rights"
        Me.bt_add_rights.Size = New System.Drawing.Size(64, 20)
        Me.bt_add_rights.TabIndex = 3
        Me.bt_add_rights.Text = "<-增加"
        Me.bt_add_rights.UseVisualStyleBackColor = True
        '
        'bt_delete_rights
        '
        Me.bt_delete_rights.Location = New System.Drawing.Point(267, 139)
        Me.bt_delete_rights.Name = "bt_delete_rights"
        Me.bt_delete_rights.Size = New System.Drawing.Size(64, 20)
        Me.bt_delete_rights.TabIndex = 4
        Me.bt_delete_rights.Text = "删除->"
        Me.bt_delete_rights.UseVisualStyleBackColor = True
        '
        'bt_close_rights
        '
        Me.bt_close_rights.Location = New System.Drawing.Point(267, 259)
        Me.bt_close_rights.Name = "bt_close_rights"
        Me.bt_close_rights.Size = New System.Drawing.Size(64, 20)
        Me.bt_close_rights.TabIndex = 5
        Me.bt_close_rights.Text = "关闭"
        Me.bt_close_rights.UseVisualStyleBackColor = True
        '
        'dgv_rights_list
        '
        Me.dgv_rights_list.AllowUserToAddRows = False
        Me.dgv_rights_list.AutoGenerateColumns = False
        Me.dgv_rights_list.BackgroundColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_rights_list.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv_rights_list.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_rights_list.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.checkid, Me.IDDataGridViewTextBoxColumn, Me.RightsDataGridViewTextBoxColumn, Me.RightsdescDataGridViewTextBoxColumn})
        Me.dgv_rights_list.DataSource = Me.RightsBindingSource
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv_rights_list.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgv_rights_list.Location = New System.Drawing.Point(351, 46)
        Me.dgv_rights_list.Name = "dgv_rights_list"
        Me.dgv_rights_list.RowHeadersVisible = False
        Me.dgv_rights_list.RowTemplate.Height = 23
        Me.dgv_rights_list.Size = New System.Drawing.Size(362, 266)
        Me.dgv_rights_list.TabIndex = 6
        '
        'checkid
        '
        Me.checkid.FalseValue = "0"
        Me.checkid.HeaderText = ""
        Me.checkid.Name = "checkid"
        Me.checkid.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.checkid.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.checkid.TrueValue = "1"
        Me.checkid.Width = 20
        '
        'IDDataGridViewTextBoxColumn
        '
        Me.IDDataGridViewTextBoxColumn.DataPropertyName = "ID"
        Me.IDDataGridViewTextBoxColumn.HeaderText = "ID"
        Me.IDDataGridViewTextBoxColumn.Name = "IDDataGridViewTextBoxColumn"
        Me.IDDataGridViewTextBoxColumn.ReadOnly = True
        Me.IDDataGridViewTextBoxColumn.Visible = False
        '
        'RightsDataGridViewTextBoxColumn
        '
        Me.RightsDataGridViewTextBoxColumn.DataPropertyName = "Rights"
        Me.RightsDataGridViewTextBoxColumn.HeaderText = "权限名称"
        Me.RightsDataGridViewTextBoxColumn.Name = "RightsDataGridViewTextBoxColumn"
        '
        'RightsdescDataGridViewTextBoxColumn
        '
        Me.RightsdescDataGridViewTextBoxColumn.DataPropertyName = "Rights_desc"
        Me.RightsdescDataGridViewTextBoxColumn.HeaderText = "描述"
        Me.RightsdescDataGridViewTextBoxColumn.Name = "RightsdescDataGridViewTextBoxColumn"
        Me.RightsdescDataGridViewTextBoxColumn.Width = 250
        '
        'RightsBindingSource
        '
        Me.RightsBindingSource.DataMember = "Rights"
        Me.RightsBindingSource.DataSource = Me.Rights
        '
        'Rights
        '
        Me.Rights.DataSetName = "Rights"
        Me.Rights.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'RightsTableAdapter
        '
        Me.RightsTableAdapter.ClearBeforeFill = True
        '
        'bt_save_right
        '
        Me.bt_save_right.Location = New System.Drawing.Point(267, 199)
        Me.bt_save_right.Name = "bt_save_right"
        Me.bt_save_right.Size = New System.Drawing.Size(64, 20)
        Me.bt_save_right.TabIndex = 7
        Me.bt_save_right.Text = "保存"
        Me.bt_save_right.UseVisualStyleBackColor = True
        '
        'clb_addrights_list
        '
        Me.clb_addrights_list.FormattingEnabled = True
        Me.clb_addrights_list.Location = New System.Drawing.Point(23, 46)
        Me.clb_addrights_list.Name = "clb_addrights_list"
        Me.clb_addrights_list.Size = New System.Drawing.Size(223, 260)
        Me.clb_addrights_list.TabIndex = 8
        '
        '用户岗位分配
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(728, 336)
        Me.Controls.Add(Me.clb_addrights_list)
        Me.Controls.Add(Me.bt_save_right)
        Me.Controls.Add(Me.dgv_rights_list)
        Me.Controls.Add(Me.bt_close_rights)
        Me.Controls.Add(Me.bt_delete_rights)
        Me.Controls.Add(Me.bt_add_rights)
        Me.Controls.Add(Me.cb_gangweilist)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "用户岗位分配"
        Me.Text = "用户岗位分配"
        CType(Me.dgv_rights_list, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RightsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Rights, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cb_gangweilist As System.Windows.Forms.ComboBox
    Friend WithEvents bt_add_rights As System.Windows.Forms.Button
    Friend WithEvents bt_delete_rights As System.Windows.Forms.Button
    Friend WithEvents bt_close_rights As System.Windows.Forms.Button
    Friend WithEvents dgv_rights_list As System.Windows.Forms.DataGridView
    Friend WithEvents Rights As streetlamp.Rights
    Friend WithEvents RightsBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents RightsTableAdapter As streetlamp.RightsTableAdapters.RightsTableAdapter
    Friend WithEvents bt_save_right As System.Windows.Forms.Button
    Friend WithEvents clb_addrights_list As System.Windows.Forms.CheckedListBox
    Friend WithEvents checkid As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents IDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RightsDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RightsdescDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
