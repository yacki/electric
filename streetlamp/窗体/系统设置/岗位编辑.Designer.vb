<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 岗位编辑
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(岗位编辑))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tb_gangwei_title = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tb_gangwei_des = New System.Windows.Forms.TextBox()
        Me.bt_add = New System.Windows.Forms.Button()
        Me.dgv_gangweilist = New System.Windows.Forms.DataGridView()
        Me.IDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GangweiDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DescriptionDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GangweiBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Gangwei = New streetlamp.gangwei()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GangweiTableAdapter = New streetlamp.gangweiTableAdapters.gangweiTableAdapter()
        CType(Me.dgv_gangweilist, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GangweiBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Gangwei, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(10, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "岗位名称："
        '
        'tb_gangwei_title
        '
        Me.tb_gangwei_title.Location = New System.Drawing.Point(74, 23)
        Me.tb_gangwei_title.Name = "tb_gangwei_title"
        Me.tb_gangwei_title.Size = New System.Drawing.Size(86, 21)
        Me.tb_gangwei_title.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(198, 26)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 12)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "岗位描述："
        '
        'tb_gangwei_des
        '
        Me.tb_gangwei_des.Location = New System.Drawing.Point(269, 23)
        Me.tb_gangwei_des.Name = "tb_gangwei_des"
        Me.tb_gangwei_des.Size = New System.Drawing.Size(162, 21)
        Me.tb_gangwei_des.TabIndex = 3
        '
        'bt_add
        '
        Me.bt_add.Location = New System.Drawing.Point(471, 22)
        Me.bt_add.Name = "bt_add"
        Me.bt_add.Size = New System.Drawing.Size(64, 20)
        Me.bt_add.TabIndex = 4
        Me.bt_add.Text = "增加"
        Me.bt_add.UseVisualStyleBackColor = True
        '
        'dgv_gangweilist
        '
        Me.dgv_gangweilist.AllowUserToAddRows = False
        Me.dgv_gangweilist.AutoGenerateColumns = False
        Me.dgv_gangweilist.BackgroundColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_gangweilist.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv_gangweilist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_gangweilist.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IDDataGridViewTextBoxColumn, Me.GangweiDataGridViewTextBoxColumn, Me.DescriptionDataGridViewTextBoxColumn})
        Me.dgv_gangweilist.DataSource = Me.GangweiBindingSource
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv_gangweilist.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgv_gangweilist.Location = New System.Drawing.Point(13, 74)
        Me.dgv_gangweilist.Name = "dgv_gangweilist"
        Me.dgv_gangweilist.RowTemplate.Height = 23
        Me.dgv_gangweilist.Size = New System.Drawing.Size(534, 224)
        Me.dgv_gangweilist.TabIndex = 5
        '
        'IDDataGridViewTextBoxColumn
        '
        Me.IDDataGridViewTextBoxColumn.DataPropertyName = "ID"
        Me.IDDataGridViewTextBoxColumn.HeaderText = "ID"
        Me.IDDataGridViewTextBoxColumn.Name = "IDDataGridViewTextBoxColumn"
        Me.IDDataGridViewTextBoxColumn.ReadOnly = True
        '
        'GangweiDataGridViewTextBoxColumn
        '
        Me.GangweiDataGridViewTextBoxColumn.DataPropertyName = "gangwei"
        Me.GangweiDataGridViewTextBoxColumn.HeaderText = "岗位名称"
        Me.GangweiDataGridViewTextBoxColumn.Name = "GangweiDataGridViewTextBoxColumn"
        '
        'DescriptionDataGridViewTextBoxColumn
        '
        Me.DescriptionDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DescriptionDataGridViewTextBoxColumn.DataPropertyName = "description"
        Me.DescriptionDataGridViewTextBoxColumn.HeaderText = "岗位描述"
        Me.DescriptionDataGridViewTextBoxColumn.Name = "DescriptionDataGridViewTextBoxColumn"
        '
        'GangweiBindingSource
        '
        Me.GangweiBindingSource.DataMember = "gangwei"
        Me.GangweiBindingSource.DataSource = Me.Gangwei
        '
        'Gangwei
        '
        Me.Gangwei.DataSetName = "gangwei"
        Me.Gangwei.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label3.Location = New System.Drawing.Point(10, 312)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(149, 12)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "(双击数据进行删除操作)"
        '
        'GangweiTableAdapter
        '
        Me.GangweiTableAdapter.ClearBeforeFill = True
        '
        '岗位编辑
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(557, 332)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.dgv_gangweilist)
        Me.Controls.Add(Me.bt_add)
        Me.Controls.Add(Me.tb_gangwei_des)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.tb_gangwei_title)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "岗位编辑"
        Me.Text = "岗位编辑"
        CType(Me.dgv_gangweilist, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GangweiBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Gangwei, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tb_gangwei_title As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents tb_gangwei_des As System.Windows.Forms.TextBox
    Friend WithEvents bt_add As System.Windows.Forms.Button
    Friend WithEvents dgv_gangweilist As System.Windows.Forms.DataGridView
    Friend WithEvents Gangwei As streetlamp.gangwei
    Friend WithEvents GangweiBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents GangweiTableAdapter As streetlamp.gangweiTableAdapters.gangweiTableAdapter
    Friend WithEvents IDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GangweiDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DescriptionDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
