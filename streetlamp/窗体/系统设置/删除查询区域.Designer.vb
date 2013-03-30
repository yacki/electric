<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 删除查询区域
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(删除查询区域))
        Me.dgv_street_position_list = New System.Windows.Forms.DataGridView()
        Me.control_box_name = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Posx = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Posy = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.StreetpositionviewBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Street_position = New streetlamp.street_position()
        Me.Street_position_viewTableAdapter = New streetlamp.street_positionTableAdapters.street_position_viewTableAdapter()
        CType(Me.dgv_street_position_list, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StreetpositionviewBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Street_position, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgv_street_position_list
        '
        Me.dgv_street_position_list.AllowUserToAddRows = False
        Me.dgv_street_position_list.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv_street_position_list.AutoGenerateColumns = False
        Me.dgv_street_position_list.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgv_street_position_list.BackgroundColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_street_position_list.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv_street_position_list.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_street_position_list.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.control_box_name, Me.Posx, Me.Posy})
        Me.dgv_street_position_list.DataSource = Me.StreetpositionviewBindingSource
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv_street_position_list.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgv_street_position_list.Location = New System.Drawing.Point(12, 12)
        Me.dgv_street_position_list.Name = "dgv_street_position_list"
        Me.dgv_street_position_list.RowTemplate.Height = 23
        Me.dgv_street_position_list.Size = New System.Drawing.Size(526, 373)
        Me.dgv_street_position_list.TabIndex = 0
        '
        'control_box_name
        '
        Me.control_box_name.DataPropertyName = "control_box_name"
        Me.control_box_name.HeaderText = "定位区域名称"
        Me.control_box_name.Name = "control_box_name"
        '
        'Posx
        '
        Me.Posx.DataPropertyName = "pos_x"
        Me.Posx.HeaderText = "X坐标"
        Me.Posx.Name = "Posx"
        '
        'Posy
        '
        Me.Posy.DataPropertyName = "pos_y"
        Me.Posy.HeaderText = "Y坐标"
        Me.Posy.Name = "Posy"
        '
        'StreetpositionviewBindingSource
        '
        Me.StreetpositionviewBindingSource.DataMember = "street_position_view"
        Me.StreetpositionviewBindingSource.DataSource = Me.Street_position
        '
        'Street_position
        '
        Me.Street_position.DataSetName = "street_position"
        Me.Street_position.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'Street_position_viewTableAdapter
        '
        Me.Street_position_viewTableAdapter.ClearBeforeFill = True
        '
        '删除查询区域
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(550, 397)
        Me.Controls.Add(Me.dgv_street_position_list)
        Me.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "删除查询区域"
        Me.Text = "删除查询区域(双击数据进行删除操作)"
        CType(Me.dgv_street_position_list, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StreetpositionviewBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Street_position, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgv_street_position_list As System.Windows.Forms.DataGridView
    Friend WithEvents Street_position As streetlamp.street_position
    Friend WithEvents StreetpositionviewBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents Street_position_viewTableAdapter As streetlamp.street_positionTableAdapters.street_position_viewTableAdapter
    Friend WithEvents Mapname As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents control_box_name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Posx As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Posy As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
