''' <summary>
''' �û����Ĺ���
''' </summary>
''' <remarks></remarks>
Public Class m_manager

    Dim Node_num As Integer
    ''' <summary>
    ''' �������뺯��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub m_manager_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '���ø��������ͼ��
        Me.Icon = New Icon("ͼƬ\favicon.ico", 32, 32)
        Dim msg_user As String
        Dim sql_user As String
        Dim rs_user As New ADODB.Recordset
        Dim Node As TreeNode
        Dim conn As New ADODB.Connection
        DBOperation.OpenConn(conn)


        msg_user = ""
        sql_user = "select * from manage"
        rs_user = DBOperation.SelectSQL(conn, sql_user, msg_user)
        Node_num = rs_user.RecordCount


        While rs_user.EOF = False
            Node = tv_user_tree.Nodes.Add(Trim(rs_user.Fields("name").Value))

            Node.StateImageKey = "touxiang.jpeg"


            rs_user.MoveNext()
        End While
        rs_user.Close()
        rs_user = Nothing
        conn.Close()
        conn = Nothing


    End Sub

    ''' <summary>
    ''' ѡ�������б��е��û���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub user_tree_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tv_user_tree.AfterSelect

        Dim msg_user As String
        Dim sql_user As String
        Dim rs_user As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        DBOperation.OpenConn(conn)


        msg_user = ""

        Dim i As Integer
        i = 0

        While i < Node_num
            If tv_user_tree.Nodes(i).IsSelected Then
                sql_user = "select * from manage where name='" + Trim(tv_user_tree.Nodes(i).Text) + "'"
                rs_user = DBOperation.SelectSQL(conn, sql_user, msg_user)
                lb_user_name.Text = Trim(rs_user.Fields("name").Value)
                lb_password.Text = Trim(rs_user.Fields("password").Value)
                cb_right_user.Text = Trim(rs_user.Fields("rights").Value)
                Me.GroupBox2.Refresh()



            End If
            i = i + 1
        End While
        rs_user.Close()
        rs_user = Nothing
        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    ''' �����û�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_add.Click
        Dim rs_add As New ADODB.Recordset
        Dim msg_add As String
        Dim sql_add As String
        Dim Node As TreeNode

        'If Com_inf.Property_right_manage <> "����Ա" Then
        '    MsgBox("��δ����Ȩ�˲�����", , LoginForm.Property_welcome_win_obj.Property_msg_box_title)
        '    Me.Close()
        '    Exit Sub
        'End If
        If Trim(lb_user_name.Text) = "" Then
            MsgBox("�û���������Ϊ�գ�", , PROJECT_TITLE_STRING)
            lb_user_name.Focus()
            Exit Sub
        End If
        If Trim(lb_user_name.Text).Length > 20 Then
            MsgBox("�û���̫�������ȳ���20��", , PROJECT_TITLE_STRING)
            lb_user_name.Focus()
            Exit Sub
        End If
        If Trim(lb_password.Text) = "" Then
            MsgBox("���벻����Ϊ�գ�", , PROJECT_TITLE_STRING)
            lb_password.Focus()
            Exit Sub
        End If
        If Trim(lb_password.Text).Length > 20 Then
            MsgBox("����̫�������ȳ���20��", , PROJECT_TITLE_STRING)
            lb_password.Focus()
            Exit Sub
        End If
        If Trim(cb_right_user.Text) = "" Then
            MsgBox("��λ������Ϊ�գ���ѡ��", , PROJECT_TITLE_STRING)
            cb_right_user.Focus()
            Exit Sub
        End If
        'If Trim(right_user.Text) <> "����Ա" And Trim(right_user.Text) <> "��ͨ�û�" Then
        '    MsgBox("Ȩ�����������ѡ��", , LoginForm.Property_welcome_win_obj.Property_msg_box_title)
        '    right_user.Focus()
        '    Exit Sub
        'End If
        Dim conn As New ADODB.Connection
        DBOperation.OpenConn(conn)
        msg_add = ""
        sql_add = "select * from manage where name='" + Trim(lb_user_name.Text) + "'"
        rs_add = DBOperation.SelectSQL(conn, sql_add, msg_add)
        If rs_add.RecordCount > 0 Then
            MsgBox("���û��Ѿ�����,������û���!", , PROJECT_TITLE_STRING)
            lb_user_name.Focus()
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        rs_add.AddNew()
        rs_add.Fields("name").Value = Trim(lb_user_name.Text)
        rs_add.Fields("password").Value = Trim(lb_password.Text)
        rs_add.Fields("rights").Value = Trim(cb_right_user.Text)

        rs_add.Update()
        rs_add.Close()
        rs_add = Nothing

        MsgBox("�û���ӳɹ���", , PROJECT_TITLE_STRING)
        '���Ӳ�����¼
        Com_inf.Insert_Operation("�����û���" & Trim(lb_user_name.Text) & " ," & Trim(cb_right_user.Text))

        sql_add = "select * from manage"
        tv_user_tree.Nodes.Clear()
        rs_add = DBOperation.SelectSQL(conn, sql_add, msg_add)
        Node_num = rs_add.RecordCount

        While rs_add.EOF = False
            Node = tv_user_tree.Nodes.Add(Trim(rs_add.Fields("name").Value))
            Node.StateImageKey = "touxiang.jpeg"
            rs_add.MoveNext()
        End While
        rs_add.Close()
        rs_add = Nothing
        conn.Close()
        conn = Nothing

    End Sub
    ''' <summary>
    ''' �޸��û���Ϣ
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_modif_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_modif.Click

        Dim rs_modif As New ADODB.Recordset
        Dim msg_modif As String
        Dim sql_modif As String
        Dim i As Integer
        i = 0

        msg_modif = ""
        sql_modif = "select * from manage where name='" + Trim(lb_user_name.Text) + "'"
        'If Com_inf.Property_right_manage <> "����Ա" Then
        '    MsgBox("��δ����Ȩ�˲�����", , LoginForm.Property_welcome_win_obj.Property_msg_box_title)
        '    Me.Close()
        '    Exit Sub
        'End If
        If Trim(lb_user_name.Text) = "" Then
            MsgBox("�û���������Ϊ�գ�", , PROJECT_TITLE_STRING)
            lb_user_name.Focus()
            Exit Sub
        End If
        Dim conn As New ADODB.Connection
        DBOperation.OpenConn(conn)

        rs_modif = DBOperation.SelectSQL(conn, sql_modif, msg_modif)
        If rs_modif.RecordCount <= 0 Then
            MsgBox("û�и��û�����Ϣ,��ȷ���û����������޸�!", MsgBoxStyle.Exclamation, PROJECT_TITLE_STRING)
            lb_user_name.Focus()
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If Trim(lb_user_name.Text) = g_username Then
            If MsgBox("��������¼���û���,�޸ĸ���Ϣ���׶�����һ����������Ӱ��,�Ƿ������", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.Yes Then

                g_rightmanage = Trim(cb_right_user.Text)


            Else
                rs_modif.Close()
                rs_modif = Nothing
                conn.Close()
                conn = Nothing
                Exit Sub
            End If
        End If


        If Trim(lb_password.Text) = "" Then
            MsgBox("���벻����Ϊ�գ�", , PROJECT_TITLE_STRING)
            lb_password.Focus()
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If Trim(lb_password.Text).Length > 20 Then
            MsgBox("����̫�������ȳ���20", , PROJECT_TITLE_STRING)
            lb_password.Focus()
            conn.Close()
            conn = Nothing
            Exit Sub
        End If

        If Trim(cb_right_user.Text) = "" Then
            MsgBox("��λ������Ϊ�գ���ѡ��", , PROJECT_TITLE_STRING)
            cb_right_user.Focus()
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        'If Trim(right_user.Text) <> "����Ա" And Trim(right_user.Text) <> "��ͨ�û�" Then
        '    MsgBox("Ȩ�����������ѡ��", , LoginForm.Property_welcome_win_obj.Property_msg_box_title)
        '    right_user.Focus()
        '    conn.Close()
        '    conn = Nothing
        '    Exit Sub
        'End If

        rs_modif.Fields("password").Value = Trim(lb_password.Text)
        rs_modif.Fields("rights").Value = Trim(cb_right_user.Text)
        rs_modif.Update()

        MsgBox("�û�" + Trim(lb_user_name.Text) + "����Ϣ�޸����!", , PROJECT_TITLE_STRING)
        '���Ӳ�����¼
        Com_inf.Insert_Operation("�޸��û�" + Trim(lb_user_name.Text) + "����Ϣ")

        'If Com_inf.Property_right_manage <> "����Ա" Then
        '    MsgBox("�����ǹ���Ա�����ڼ����رգ�", , LoginForm.Property_welcome_win_obj.Property_msg_box_title)
        '    conn.Close()
        '    conn = Nothing
        '    Me.Close()
        '    'Exit Sub
        'End If

        While i < tv_user_tree.Nodes.Count
            If tv_user_tree.Nodes(i).Text = Trim(lb_user_name.Text) Then
                tv_user_tree.Nodes(i).StateImageKey = "touxiang.jpeg"
            End If
            i = i + 1
        End While

        tv_user_tree.Refresh()
        rs_modif.Close()
        rs_modif = Nothing
        conn.Close()
        conn = Nothing

    End Sub
    ''' <summary>
    ''' ɾ���û���Ϣ
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_delete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_delete.Click
        'If Com_inf.Property_right_manage <> "����Ա" Then
        '    MsgBox("��δ����Ȩ�˲�����", , LoginForm.Property_welcome_win_obj.Property_msg_box_title)
        '    Me.Close()
        '    Exit Sub
        'End If
        If MsgBox("�Ƿ�ɾ���û�" + Trim(lb_user_name.Text) + "", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.Yes Then
            Dim sql_del As String
            Dim msg_del As String
            Dim conn As New ADODB.Connection
            Dim rs_del As New ADODB.Recordset
            Dim Node As TreeNode
            DBOperation.OpenConn(conn)


            msg_del = ""
            sql_del = "delete from manage where name='" + Trim(lb_user_name.Text) + "'"
            DBOperation.ExecuteSQL(conn, sql_del, msg_del)

            '���Ӳ�����¼
            Com_inf.Insert_Operation("ɾ���û�" + Trim(lb_user_name.Text) + "����Ϣ")


            sql_del = "select * from manage"
            tv_user_tree.Nodes.Clear()
            rs_del = DBOperation.SelectSQL(conn, sql_del, msg_del)
            Node_num = rs_del.RecordCount

            While rs_del.EOF = False
                Node = tv_user_tree.Nodes.Add(Trim(rs_del.Fields("name").Value))
                Node.StateImageKey = "touxiang.jpeg"
                rs_del.MoveNext()
            End While
            rs_del.Close()
            rs_del = Nothing
            conn.Close()
            conn = Nothing
            lb_user_name.Text = ""
            lb_password.Text = ""
            cb_right_user.Text = ""
        End If

    End Sub

    ''' <summary>
    ''' Ϊ�µ��û���Ӹ�λ
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_right_user_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_right_user.DropDown
        'Ϊ�µ��û���Ӹ�λ
        Dim rs As New ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim conn As New ADODB.Connection

        DBOperation.OpenConn(conn)
        msg = ""
        sql = "select gangwei from gangwei"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "right_user_DropDown", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        Me.cb_right_user.Items.Clear()   '��ո�λ�б�
        While rs.EOF = False
            Me.cb_right_user.Items.Add(Trim(rs.Fields("gangwei").Value))
            rs.MoveNext()
        End While
        If Me.cb_right_user.Items.Count > 0 Then
            Me.cb_right_user.SelectedIndex = 0
        End If

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    Private Sub m_manager_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        g_windowclose = 1
    End Sub
End Class