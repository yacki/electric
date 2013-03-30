<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 删除交流接触器时段设置
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(删除交流接触器时段设置))
        Me.del_mod = New System.Windows.Forms.Button
        Me.jiaoliumod_list = New System.Windows.Forms.DataGridView
        Me.id = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.full_id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.lamp_id_short = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.control_mod = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ControllevelviewBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Lamp_level_list = New streetlamp.lamp_level_list
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.count_string = New System.Windows.Forms.ToolStripStatusLabel
        Me.Control_level_viewTableAdapter = New streetlamp.lamp_level_listTableAdapters.control_level_viewTableAdapter
        CType(Me.jiaoliumod_list, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ControllevelviewBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Lamp_level_list, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'del_mod
        '
        Me.del_mod.Location = New System.Drawing.Point(159, 387)
        Me.del_mod.Name = "del_mod"
        Me.del_mod.Size = New System.Drawing.Size(75, 23)
        Me.del_mod.TabIndex = 12
        Me.del_mod.Text = "删除"
        Me.del_mod.UseVisualStyleBackColor = True
        '
        'jiaoliumod_list
        '
        Me.jiaoliumod_list.AllowUserToAddRows = False
        Me.jiaoliumod_list.AutoGenerateColumns = False
        Me.jiaoliumod_list.BackgroundColor = System.Drawing.Color.WhiteSmoke
        Me.jiaoliumod_list.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.jiaoliumod_list.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.id, Me.full_id, Me.lamp_id_short, Me.control_mod})
        Me.jiaoliumod_list.DataSource = Me.ControllevelviewBindingSource
        Me.jiaoliumod_list.Location = New System.Drawing.Point(12, 12)
        Me.jiaoliumod_list.Name = "jiaoliumod_list"
        Me.jiaoliumod_list.RowTemplate.Height = 23
        Me.jiaoliumod_list.Size = New System.Drawing.Size(370, 355)
        Me.jiaoliumod_list.TabIndex = 13
        '
        'id
        '
        Me.id.FalseValue = "0"
        Me.id.HeaderText = ""
        Me.id.Name = "id"
        Me.id.TrueValue = "1"
        Me.id.Width = 20
        '
        'full_id
        '
        Me.full_id.DataPropertyName = "lamp_id"
        Me.full_id.HeaderText = "编号"
        Me.full_id.Name = "full_id"
        Me.full_id.Visible = False
        '
        'lamp_id_short
        '
        Me.lamp_id_short.HeaderText = "主控箱节点编号"
        Me.lamp_id_short.Name = "lamp_id_short"
        Me.lamp_id_short.Width = 150
        '
        'control_mod
        '
        Me.control_mod.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.control_mod.DataPropertyName = "div_time_level"
        Me.control_mod.HeaderText = "控制模式"
        Me.control_mod.Name = "control_mod"
        '
        'ControllevelviewBindingSource
        '
        Me.ControllevelviewBindingSource.DataMember = "control_level_view"
        Me.ControllevelviewBindingSource.DataSource = Me.Lamp_level_list
        '
        'Lamp_level_list
        '
        Me.Lamp_level_list.DataSetName = "lamp_level_list"
        Me.Lamp_level_list.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.count_string})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 415)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Padding = New System.Windows.Forms.Padding(1, 0, 12, 0)
        Me.StatusStrip1.Size = New System.Drawing.Size(393, 22)
        Me.StatusStrip1.TabIndex = 14
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'count_string
        '
        Me.count_string.Name = "count_string"
        Me.count_string.Size = New System.Drawing.Size(131, 17)
        Me.count_string.Text = "ToolStripStatusLabel1"
        '
        'Control_level_viewTableAdapter
        '
        Me.Control_level_viewTableAdapter.ClearBeforeFill = True
        '
        '删除交流接触器时段设置
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(393, 437)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.jiaoliumod_list)
        Me.Controls.Add(Me.del_mod)
        Me.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "删除交流接触器时段设置"
        Me.Text = "删除主控箱时段设置"
        CType(Me.jiaoliumod_list, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ControllevelviewBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Lamp_level_list, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents del_mod As System.Windows.Forms.Button
    Friend WithEvents jiaoliumod_list As System.Windows.Forms.DataGridView
    Friend WithEvents Lamp_level_list As streetlamp.lamp_level_list
    Friend WithEvents ControllevelviewBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents Control_level_viewTableAdapter As streetlamp.lamp_level_listTableAdapters.control_level_viewTableAdapter
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents count_string As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents id As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents full_id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lamp_id_short As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents control_mod As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
