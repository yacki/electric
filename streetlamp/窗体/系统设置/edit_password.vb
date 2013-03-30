''' <summary>
''' 修改密码
''' </summary>
''' <remarks></remarks>
Public Class edit_password
    ''' <summary>
    ''' 保存修改后的密码
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_save.Click
        Dim rs_p As New ADODB.Recordset
        Dim sql_p As String
        Dim msg_p As String


        msg_p = ""
        sql_p = "select * from manage where name='" + Trim(g_username) + "'"
        If Trim(lb_origin_password.Text) = "" Then
            MsgBox("原密码不可为空,请输入原密码！", , PROJECT_TITLE_STRING)
            lb_origin_password.Focus()
            Exit Sub
        End If
        If Trim(lb_password.Text) = "" Then
            MsgBox("密码不可以为空,请输入密码!", , PROJECT_TITLE_STRING)
            lb_password.Focus()
            Exit Sub
        End If
        If Trim(lb_password.Text).Length > 20 Then
            MsgBox("密码太长，长度超过20！", , PROJECT_TITLE_STRING)
            lb_password.Focus()
            Exit Sub
        End If
        If Trim(lb_password_again.Text) = "" Then
            MsgBox("请再次输入密码，以保证其正确性！", , PROJECT_TITLE_STRING)
            lb_password_again.Focus()
            Exit Sub
        End If
        If Trim(lb_origin_password.Text) <> g_password Then
            MsgBox("原密码输入不正确！", , PROJECT_TITLE_STRING)
            lb_origin_password.Focus()
            Exit Sub
        End If
        If Trim(lb_password.Text) <> Trim(lb_password_again.Text) Then
            MsgBox("两次密码输入不一致，请确认！", , PROJECT_TITLE_STRING)
            lb_password.Focus()
            Exit Sub
        End If
        Dim conn As New ADODB.Connection
        DBOperation.OpenConn(conn)


        rs_p = DBOperation.SelectSQL(conn, sql_p, msg_p)
        rs_p.Fields("password").Value = Trim(lb_password.Text)

        rs_p.Update()
        rs_p.Close()
        rs_p = Nothing
        g_password = Trim(lb_password.Text)
        MsgBox("密码修改完毕！", , PROJECT_TITLE_STRING)

        '增加操作记录
        Com_inf.Insert_Operation("用户" + Trim(g_username) + "修改密码")


        conn.Close()
        conn = Nothing
        Me.Close()

    End Sub


    Private Sub edit_password_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '设置各个窗体的图标
        Me.Icon = New Icon("图片\favicon.ico", 32, 32)
    End Sub

    Private Sub edit_password_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        g_windowclose = 1
    End Sub
End Class