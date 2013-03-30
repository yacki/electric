Public Class 编辑联系人

    ''' <summary>
    ''' 载入窗口
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 编辑联系人_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: 这行代码将数据加载到表“Control_list.Contact”中。您可以根据需要移动或移除它。
        Me.ContactTableAdapter1.Fill(Me.Control_list.Contact)
        'TODO: 这行代码将数据加载到表“Contact_list.Contact”中。您可以根据需要移动或移除它。
        '设置各个窗体的图标
        Me.Icon = New Icon("图片\favicon.ico", 32, 32)
        Me.ContactTableAdapter.Fill(Me.Contact_list.Contact)

    End Sub

    ''' <summary>
    ''' 增加联系人的联系方式
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_add.Click
        If lb_phonenum.Text = "" Then
            MsgBox("联系电话号码为空", , PROJECT_TITLE_STRING)
            lb_phonenum.Focus()
            Exit Sub
        End If

        If lb_contactname.Text = "" Then
            MsgBox("联系人姓名为空", , PROJECT_TITLE_STRING)
            lb_contactname.Focus()
            Exit Sub
        End If

        If lb_contactname.TextLength > 10 Then
            MsgBox("联系姓名长度大于10", , PROJECT_TITLE_STRING)
            lb_contactname.Focus()
            Exit Sub
        End If

        If lb_phonenum.TextLength > 20 Then
            MsgBox("联系电话号码过长", , PROJECT_TITLE_STRING)
            lb_phonenum.Focus()
            Exit Sub
        End If

        Dim sql As String
        Dim msg As String
        Dim conn As New ADODB.Connection
        Dim type As String = "alarm"

        DBOperation.OpenConn(conn)
        sql = "insert into Contact(Contact, phonenum,control_box_name) values('" & Trim(lb_contactname.Text) & "','" & Trim(lb_phonenum.Text) & "','" & type & "')"
        msg = ""
        DBOperation.ExecuteSQL(conn, sql, msg)

        MsgBox("联系方式添加成功", , PROJECT_TITLE_STRING)
        '增加操作记录
        Com_inf.Insert_Operation("添加报警短信联系人方式：" & Trim(lb_contactname.Text) & " " & Trim(lb_phonenum.Text))

        'TODO: 这行代码将数据加载到表“Contact_list.Contact”中。您可以根据需要移动或移除它。
        Me.ContactTableAdapter.Fill(Me.Contact_list.Contact)

        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    ''' 双击联系人条目进行删除
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub DataGridView1_CellMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgv_contact_list.CellMouseDoubleClick
        If MsgBox("是否删除" & Trim(Me.dgv_contact_list.CurrentRow.Cells("ContactDataGridViewTextBoxColumn").Value) & "信息？", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.Yes Then
            Dim conn As New ADODB.Connection
            Dim sql As String
            Dim msg As String
            msg = ""
            DBOperation.OpenConn(conn)
            sql = "delete from Contact where id='" & Me.dgv_contact_list.CurrentRow.Cells("IDDataGridViewTextBoxColumn").Value & "'"
            DBOperation.ExecuteSQL(conn, sql, msg)

            MsgBox("删除成功", , PROJECT_TITLE_STRING)

            '增加操作记录
            Com_inf.Insert_Operation("删除报警短信联系人方式：" & Me.dgv_contact_list.CurrentRow.Cells("ContactDataGridViewTextBoxColumn").Value)

            'TODO: 这行代码将数据加载到表“Contact_list.Contact”中。您可以根据需要移动或移除它。
            Me.ContactTableAdapter.Fill(Me.Contact_list.Contact)

            conn.Close()
            conn = Nothing
        End If
    End Sub

   
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If tb_controlphonenum.Text = "" Then
            MsgBox("联系电话号码为空", , PROJECT_TITLE_STRING)
            tb_controlphonenum.Focus()
            Exit Sub
        End If

        If tb_controlname.Text = "" Then
            MsgBox("联系人姓名为空", , PROJECT_TITLE_STRING)
            tb_controlname.Focus()
            Exit Sub
        End If

        If tb_controlname.TextLength > 10 Then
            MsgBox("联系姓名长度大于10", , PROJECT_TITLE_STRING)
            tb_controlname.Focus()
            Exit Sub
        End If

        If tb_controlphonenum.TextLength > 20 Then
            MsgBox("联系电话号码过长", , PROJECT_TITLE_STRING)
            tb_controlphonenum.Focus()
            Exit Sub
        End If

        Dim sql As String
        Dim msg As String
        Dim conn As New ADODB.Connection
        Dim type As String = "control"

        DBOperation.OpenConn(conn)
        sql = "insert into Contact(Contact, phonenum,control_box_name) values('" & Trim(tb_controlname.Text) & "','" & Trim(tb_controlphonenum.Text) & "','" & type & "')"
        msg = ""
        DBOperation.ExecuteSQL(conn, sql, msg)

        MsgBox("联系方式添加成功", , PROJECT_TITLE_STRING)
        '增加操作记录
        Com_inf.Insert_Operation("添加控制短信联系人方式：" & Trim(lb_contactname.Text) & " " & Trim(lb_phonenum.Text))

        Me.ContactTableAdapter1.Fill(Me.Control_list.Contact)


        conn.Close()
        conn = Nothing
    End Sub

    Private Sub FillByToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Me.ContactTableAdapter.FillBy(Me.Contact_list.Contact)
        Catch ex As System.Exception
            System.Windows.Forms.MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub dgv_control_list_CellMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgv_control_list.CellMouseDoubleClick
        If MsgBox("是否删除" & Trim(Me.dgv_control_list.CurrentRow.Cells("control_contact").Value) & "信息？", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.Yes Then
            Dim conn As New ADODB.Connection
            Dim sql As String
            Dim msg As String
            msg = ""
            DBOperation.OpenConn(conn)
            sql = "delete from Contact where id='" & Me.dgv_control_list.CurrentRow.Cells("ID").Value & "'"
            DBOperation.ExecuteSQL(conn, sql, msg)

            MsgBox("删除成功", , PROJECT_TITLE_STRING)

            '增加操作记录
            Com_inf.Insert_Operation("删除控制短信联系人方式：" & Me.dgv_control_list.CurrentRow.Cells("control_contact").Value)

            'TODO: 这行代码将数据加载到表“Contact_list.Contact”中。您可以根据需要移动或移除它。
            Me.ContactTableAdapter1.Fill(Me.Control_list.Contact)


            conn.Close()
            conn = Nothing
        End If
    End Sub

    Private Sub 编辑联系人_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        g_windowclose = 1
    End Sub
End Class