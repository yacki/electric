<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 编辑联系人
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(编辑联系人))
        Me.dgv_contact_list = New System.Windows.Forms.DataGridView()
        Me.IDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ContactDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PhonenumDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ContactBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Contact_list = New streetlamp.contact_list()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lb_contactname = New System.Windows.Forms.TextBox()
        Me.lb_phonenum = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.bt_add = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.dgv_control_list = New System.Windows.Forms.DataGridView()
        Me.control_contact = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.control_phonenum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ContactBindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.Control_list = New streetlamp.control_list()
        Me.tb_controlname = New System.Windows.Forms.TextBox()
        Me.tb_controlphonenum = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.ContactTableAdapter = New streetlamp.contact_listTableAdapters.ContactTableAdapter()
        Me.ContactTableAdapter1 = New streetlamp.control_listTableAdapters.ContactTableAdapter()
        CType(Me.dgv_contact_list, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ContactBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Contact_list, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.dgv_control_list, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ContactBindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Control_list, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgv_contact_list
        '
        Me.dgv_contact_list.AllowUserToAddRows = False
        Me.dgv_contact_list.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv_contact_list.AutoGenerateColumns = False
        Me.dgv_contact_list.BackgroundColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_contact_list.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv_contact_list.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_contact_list.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IDDataGridViewTextBoxColumn, Me.ContactDataGridViewTextBoxColumn, Me.PhonenumDataGridViewTextBoxColumn})
        Me.dgv_contact_list.DataSource = Me.ContactBindingSource
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv_contact_list.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgv_contact_list.Location = New System.Drawing.Point(7, 53)
        Me.dgv_contact_list.Name = "dgv_contact_list"
        Me.dgv_contact_list.RowTemplate.Height = 23
        Me.dgv_contact_list.Size = New System.Drawing.Size(461, 255)
        Me.dgv_contact_list.TabIndex = 0
        '
        'IDDataGridViewTextBoxColumn
        '
        Me.IDDataGridViewTextBoxColumn.DataPropertyName = "ID"
        Me.IDDataGridViewTextBoxColumn.HeaderText = "ID"
        Me.IDDataGridViewTextBoxColumn.Name = "IDDataGridViewTextBoxColumn"
        Me.IDDataGridViewTextBoxColumn.Visible = False
        Me.IDDataGridViewTextBoxColumn.Width = 50
        '
        'ContactDataGridViewTextBoxColumn
        '
        Me.ContactDataGridViewTextBoxColumn.DataPropertyName = "Contact"
        Me.ContactDataGridViewTextBoxColumn.HeaderText = "联系人"
        Me.ContactDataGridViewTextBoxColumn.Name = "ContactDataGridViewTextBoxColumn"
        '
        'PhonenumDataGridViewTextBoxColumn
        '
        Me.PhonenumDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.PhonenumDataGridViewTextBoxColumn.DataPropertyName = "Phonenum"
        Me.PhonenumDataGridViewTextBoxColumn.HeaderText = "手机号码"
        Me.PhonenumDataGridViewTextBoxColumn.Name = "PhonenumDataGridViewTextBoxColumn"
        '
        'ContactBindingSource
        '
        Me.ContactBindingSource.DataMember = "Contact"
        Me.ContactBindingSource.DataSource = Me.Contact_list
        '
        'Contact_list
        '
        Me.Contact_list.DataSetName = "contact_list"
        Me.Contact_list.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(77, 12)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "联系人姓名："
        '
        'lb_contactname
        '
        Me.lb_contactname.Location = New System.Drawing.Point(89, 19)
        Me.lb_contactname.Name = "lb_contactname"
        Me.lb_contactname.Size = New System.Drawing.Size(90, 21)
        Me.lb_contactname.TabIndex = 2
        '
        'lb_phonenum
        '
        Me.lb_phonenum.Location = New System.Drawing.Point(270, 19)
        Me.lb_phonenum.Name = "lb_phonenum"
        Me.lb_phonenum.Size = New System.Drawing.Size(100, 21)
        Me.lb_phonenum.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(199, 22)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 12)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "手机号码："
        '
        'bt_add
        '
        Me.bt_add.Location = New System.Drawing.Point(393, 18)
        Me.bt_add.Name = "bt_add"
        Me.bt_add.Size = New System.Drawing.Size(75, 23)
        Me.bt_add.TabIndex = 5
        Me.bt_add.Text = "增加"
        Me.bt_add.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label3.Location = New System.Drawing.Point(6, 311)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(187, 12)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "（双击某一行可进行删除操作）"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(2, 5)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(483, 364)
        Me.TabControl1.TabIndex = 8
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.bt_add)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.dgv_contact_list)
        Me.TabPage1.Controls.Add(Me.lb_contactname)
        Me.TabPage1.Controls.Add(Me.lb_phonenum)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(475, 338)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "报警联系方式"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.AutoScroll = True
        Me.TabPage2.Controls.Add(Me.Button1)
        Me.TabPage2.Controls.Add(Me.Label4)
        Me.TabPage2.Controls.Add(Me.Label5)
        Me.TabPage2.Controls.Add(Me.dgv_control_list)
        Me.TabPage2.Controls.Add(Me.tb_controlname)
        Me.TabPage2.Controls.Add(Me.tb_controlphonenum)
        Me.TabPage2.Controls.Add(Me.Label6)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(475, 338)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "短信控制联系方式"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(393, 17)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 13
        Me.Button1.Text = "增加"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label4.Location = New System.Drawing.Point(6, 310)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(187, 12)
        Me.Label4.TabIndex = 14
        Me.Label4.Text = "（双击某一行可进行删除操作）"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 22)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(77, 12)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "联系人姓名："
        '
        'dgv_control_list
        '
        Me.dgv_control_list.AllowUserToAddRows = False
        Me.dgv_control_list.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv_control_list.AutoGenerateColumns = False
        Me.dgv_control_list.BackgroundColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_control_list.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dgv_control_list.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_control_list.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.control_contact, Me.control_phonenum, Me.ID})
        Me.dgv_control_list.DataSource = Me.ContactBindingSource1
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle4.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv_control_list.DefaultCellStyle = DataGridViewCellStyle4
        Me.dgv_control_list.Location = New System.Drawing.Point(7, 52)
        Me.dgv_control_list.Name = "dgv_control_list"
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle5.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv_control_list.RowHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.dgv_control_list.RowTemplate.Height = 23
        Me.dgv_control_list.Size = New System.Drawing.Size(461, 255)
        Me.dgv_control_list.TabIndex = 8
        '
        'control_contact
        '
        Me.control_contact.DataPropertyName = "Contact"
        Me.control_contact.HeaderText = "联系人"
        Me.control_contact.Name = "control_contact"
        '
        'control_phonenum
        '
        Me.control_phonenum.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.control_phonenum.DataPropertyName = "Phonenum"
        Me.control_phonenum.HeaderText = "手机号码"
        Me.control_phonenum.Name = "control_phonenum"
        '
        'ID
        '
        Me.ID.DataPropertyName = "ID"
        Me.ID.HeaderText = "ID"
        Me.ID.Name = "ID"
        Me.ID.ReadOnly = True
        Me.ID.Visible = False
        '
        'ContactBindingSource1
        '
        Me.ContactBindingSource1.DataMember = "Contact"
        Me.ContactBindingSource1.DataSource = Me.Control_list
        '
        'Control_list
        '
        Me.Control_list.DataSetName = "control_list"
        Me.Control_list.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'tb_controlname
        '
        Me.tb_controlname.Location = New System.Drawing.Point(89, 18)
        Me.tb_controlname.Name = "tb_controlname"
        Me.tb_controlname.Size = New System.Drawing.Size(90, 21)
        Me.tb_controlname.TabIndex = 10
        '
        'tb_controlphonenum
        '
        Me.tb_controlphonenum.Location = New System.Drawing.Point(270, 18)
        Me.tb_controlphonenum.Name = "tb_controlphonenum"
        Me.tb_controlphonenum.Size = New System.Drawing.Size(100, 21)
        Me.tb_controlphonenum.TabIndex = 12
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(199, 21)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(65, 12)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "手机号码："
        '
        'ContactTableAdapter
        '
        Me.ContactTableAdapter.ClearBeforeFill = True
        '
        'ContactTableAdapter1
        '
        Me.ContactTableAdapter1.ClearBeforeFill = True
        '
        '编辑联系人
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(486, 373)
        Me.Controls.Add(Me.TabControl1)
        Me.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "编辑联系人"
        Me.Text = "编辑短信报警联系方式"
        CType(Me.dgv_contact_list, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ContactBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Contact_list, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        CType(Me.dgv_control_list, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ContactBindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Control_list, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgv_contact_list As System.Windows.Forms.DataGridView
    Friend WithEvents Contact_list As streetlamp.contact_list
    Friend WithEvents ContactBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ContactTableAdapter As streetlamp.contact_listTableAdapters.ContactTableAdapter
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lb_contactname As System.Windows.Forms.TextBox
    Friend WithEvents lb_phonenum As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents bt_add As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents dgv_control_list As System.Windows.Forms.DataGridView
    Friend WithEvents tb_controlname As System.Windows.Forms.TextBox
    Friend WithEvents tb_controlphonenum As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Control_list As streetlamp.control_list
    Friend WithEvents ContactBindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents ContactTableAdapter1 As streetlamp.control_listTableAdapters.ContactTableAdapter
    Friend WithEvents IDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ContactDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PhonenumDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents control_contact As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents control_phonenum As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ID As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
