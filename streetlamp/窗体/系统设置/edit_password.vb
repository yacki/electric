''' <summary>
''' �޸�����
''' </summary>
''' <remarks></remarks>
Public Class edit_password
    ''' <summary>
    ''' �����޸ĺ������
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
            MsgBox("ԭ���벻��Ϊ��,������ԭ���룡", , PROJECT_TITLE_STRING)
            lb_origin_password.Focus()
            Exit Sub
        End If
        If Trim(lb_password.Text) = "" Then
            MsgBox("���벻����Ϊ��,����������!", , PROJECT_TITLE_STRING)
            lb_password.Focus()
            Exit Sub
        End If
        If Trim(lb_password.Text).Length > 20 Then
            MsgBox("����̫�������ȳ���20��", , PROJECT_TITLE_STRING)
            lb_password.Focus()
            Exit Sub
        End If
        If Trim(lb_password_again.Text) = "" Then
            MsgBox("���ٴ��������룬�Ա�֤����ȷ�ԣ�", , PROJECT_TITLE_STRING)
            lb_password_again.Focus()
            Exit Sub
        End If
        If Trim(lb_origin_password.Text) <> g_password Then
            MsgBox("ԭ�������벻��ȷ��", , PROJECT_TITLE_STRING)
            lb_origin_password.Focus()
            Exit Sub
        End If
        If Trim(lb_password.Text) <> Trim(lb_password_again.Text) Then
            MsgBox("�����������벻һ�£���ȷ�ϣ�", , PROJECT_TITLE_STRING)
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
        MsgBox("�����޸���ϣ�", , PROJECT_TITLE_STRING)

        '���Ӳ�����¼
        Com_inf.Insert_Operation("�û�" + Trim(g_username) + "�޸�����")


        conn.Close()
        conn = Nothing
        Me.Close()

    End Sub


    Private Sub edit_password_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '���ø��������ͼ��
        Me.Icon = New Icon("ͼƬ\favicon.ico", 32, 32)
    End Sub

    Private Sub edit_password_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        g_windowclose = 1
    End Sub
End Class