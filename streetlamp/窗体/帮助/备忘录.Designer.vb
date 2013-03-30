<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 备忘录
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(备忘录))
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.record_title = New System.Windows.Forms.TextBox
        Me.recorder = New System.Windows.Forms.TextBox
        Me.record_content = New System.Windows.Forms.RichTextBox
        Me.input = New System.Windows.Forms.Button
        Me.DataGridView_recordlist = New System.Windows.Forms.DataGridView
        Me.RecordlistBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Record_list = New streetlamp.record_list
        Me.Record_listTableAdapter = New streetlamp.record_listTableAdapters.record_listTableAdapter
        Me.IdDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.RecordtitleDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.RecordernameDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        CType(Me.DataGridView_recordlist, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RecordlistBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Record_list, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label1.Location = New System.Drawing.Point(371, 40)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(82, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "记录人员："
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label2.Location = New System.Drawing.Point(50, 40)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 14)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "主题："
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label3.Location = New System.Drawing.Point(20, 88)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(82, 14)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "主要内容："
        '
        'record_title
        '
        Me.record_title.Location = New System.Drawing.Point(113, 37)
        Me.record_title.Name = "record_title"
        Me.record_title.Size = New System.Drawing.Size(223, 21)
        Me.record_title.TabIndex = 3
        '
        'recorder
        '
        Me.recorder.Location = New System.Drawing.Point(459, 39)
        Me.recorder.Name = "recorder"
        Me.recorder.Size = New System.Drawing.Size(154, 21)
        Me.recorder.TabIndex = 4
        '
        'record_content
        '
        Me.record_content.Location = New System.Drawing.Point(113, 77)
        Me.record_content.Name = "record_content"
        Me.record_content.Size = New System.Drawing.Size(500, 96)
        Me.record_content.TabIndex = 5
        Me.record_content.Text = ""
        '
        'input
        '
        Me.input.Location = New System.Drawing.Point(283, 191)
        Me.input.Name = "input"
        Me.input.Size = New System.Drawing.Size(75, 23)
        Me.input.TabIndex = 6
        Me.input.Text = "记录"
        Me.input.UseVisualStyleBackColor = True
        '
        'DataGridView_recordlist
        '
        Me.DataGridView_recordlist.AllowUserToAddRows = False
        Me.DataGridView_recordlist.AutoGenerateColumns = False
        Me.DataGridView_recordlist.BackgroundColor = System.Drawing.Color.WhiteSmoke
        Me.DataGridView_recordlist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView_recordlist.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IdDataGridViewTextBoxColumn, Me.RecordtitleDataGridViewTextBoxColumn, Me.RecordernameDataGridViewTextBoxColumn, Me.DataGridViewTextBoxColumn1})
        Me.DataGridView_recordlist.DataSource = Me.RecordlistBindingSource
        Me.DataGridView_recordlist.Location = New System.Drawing.Point(23, 236)
        Me.DataGridView_recordlist.Name = "DataGridView_recordlist"
        Me.DataGridView_recordlist.RowTemplate.Height = 23
        Me.DataGridView_recordlist.Size = New System.Drawing.Size(616, 315)
        Me.DataGridView_recordlist.TabIndex = 7
        '
        'RecordlistBindingSource
        '
        Me.RecordlistBindingSource.DataMember = "record_list"
        Me.RecordlistBindingSource.DataSource = Me.Record_list
        '
        'Record_list
        '
        Me.Record_list.DataSetName = "record_list"
        Me.Record_list.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'Record_listTableAdapter
        '
        Me.Record_listTableAdapter.ClearBeforeFill = True
        '
        'IdDataGridViewTextBoxColumn
        '
        Me.IdDataGridViewTextBoxColumn.DataPropertyName = "id"
        Me.IdDataGridViewTextBoxColumn.HeaderText = "编号"
        Me.IdDataGridViewTextBoxColumn.Name = "IdDataGridViewTextBoxColumn"
        Me.IdDataGridViewTextBoxColumn.ReadOnly = True
        '
        'RecordtitleDataGridViewTextBoxColumn
        '
        Me.RecordtitleDataGridViewTextBoxColumn.DataPropertyName = "record_title"
        Me.RecordtitleDataGridViewTextBoxColumn.HeaderText = "主题"
        Me.RecordtitleDataGridViewTextBoxColumn.Name = "RecordtitleDataGridViewTextBoxColumn"
        '
        'RecordernameDataGridViewTextBoxColumn
        '
        Me.RecordernameDataGridViewTextBoxColumn.DataPropertyName = "recorder_name"
        Me.RecordernameDataGridViewTextBoxColumn.HeaderText = "记录人员"
        Me.RecordernameDataGridViewTextBoxColumn.Name = "RecordernameDataGridViewTextBoxColumn"
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "record_content"
        Me.DataGridViewTextBoxColumn1.HeaderText = "主要内容"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        '
        '备忘录
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.BackgroundImage = Global.streetlamp.My.Resources.Resources.bg11
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(652, 575)
        Me.Controls.Add(Me.DataGridView_recordlist)
        Me.Controls.Add(Me.input)
        Me.Controls.Add(Me.record_content)
        Me.Controls.Add(Me.recorder)
        Me.Controls.Add(Me.record_title)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "备忘录"
        Me.Text = "备忘录"
        CType(Me.DataGridView_recordlist, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RecordlistBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Record_list, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents record_title As System.Windows.Forms.TextBox
    Friend WithEvents recorder As System.Windows.Forms.TextBox
    Friend WithEvents record_content As System.Windows.Forms.RichTextBox
    Friend WithEvents input As System.Windows.Forms.Button
    Friend WithEvents DataGridView_recordlist As System.Windows.Forms.DataGridView
    Friend WithEvents Record_list As streetlamp.record_list
    Friend WithEvents RecordlistBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents Record_listTableAdapter As streetlamp.record_listTableAdapters.record_listTableAdapter
    Friend WithEvents RecordercontentDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IdDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RecordtitleDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RecordernameDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
