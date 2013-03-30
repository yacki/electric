''' <summary>
''' 用户名的管理
''' </summary>
''' <remarks></remarks>
Public Class m_manager

    Dim Node_num As Integer
    ''' <summary>
    ''' 窗体载入函数
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub m_manager_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '设置各个窗体的图标
        Me.Icon = New Icon("图片\favicon.ico", 32, 32)
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
    ''' 选择树形列表中的用户名
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
    ''' 增加用户
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_add.Click
        Dim rs_add As New ADODB.Recordset
        Dim msg_add As String
        Dim sql_add As String
        Dim Node As TreeNode

        'If Com_inf.Property_right_manage <> "管理员" Then
        '    MsgBox("您未被授权此操作！", , LoginForm.Property_welcome_win_obj.Property_msg_box_title)
        '    Me.Close()
        '    Exit Sub
        'End If
        If Trim(lb_user_name.Text) = "" Then
            MsgBox("用户名不可以为空！", , PROJECT_TITLE_STRING)
            lb_user_name.Focus()
            Exit Sub
        End If
        If Trim(lb_user_name.Text).Length > 20 Then
            MsgBox("用户名太长，长度超过20！", , PROJECT_TITLE_STRING)
            lb_user_name.Focus()
            Exit Sub
        End If
        If Trim(lb_password.Text) = "" Then
            MsgBox("密码不可以为空！", , PROJECT_TITLE_STRING)
            lb_password.Focus()
            Exit Sub
        End If
        If Trim(lb_password.Text).Length > 20 Then
            MsgBox("密码太长，长度超过20！", , PROJECT_TITLE_STRING)
            lb_password.Focus()
            Exit Sub
        End If
        If Trim(cb_right_user.Text) = "" Then
            MsgBox("岗位不可以为空，请选择！", , PROJECT_TITLE_STRING)
            cb_right_user.Focus()
            Exit Sub
        End If
        'If Trim(right_user.Text) <> "管理员" And Trim(right_user.Text) <> "普通用户" Then
        '    MsgBox("权限输入错误，请选择！", , LoginForm.Property_welcome_win_obj.Property_msg_box_title)
        '    right_user.Focus()
        '    Exit Sub
        'End If
        Dim conn As New ADODB.Connection
        DBOperation.OpenConn(conn)
        msg_add = ""
        sql_add = "select * from manage where name='" + Trim(lb_user_name.Text) + "'"
        rs_add = DBOperation.SelectSQL(conn, sql_add, msg_add)
        If rs_add.RecordCount > 0 Then
            MsgBox("该用户已经存在,请更换用户名!", , PROJECT_TITLE_STRING)
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

        MsgBox("用户添加成功！", , PROJECT_TITLE_STRING)
        '增加操作记录
        Com_inf.Insert_Operation("增加用户：" & Trim(lb_user_name.Text) & " ," & Trim(cb_right_user.Text))

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
    ''' 修改用户信息
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
        'If Com_inf.Property_right_manage <> "管理员" Then
        '    MsgBox("您未被授权此操作！", , LoginForm.Property_welcome_win_obj.Property_msg_box_title)
        '    Me.Close()
        '    Exit Sub
        'End If
        If Trim(lb_user_name.Text) = "" Then
            MsgBox("用户名不可以为空！", , PROJECT_TITLE_STRING)
            lb_user_name.Focus()
            Exit Sub
        End If
        Dim conn As New ADODB.Connection
        DBOperation.OpenConn(conn)

        rs_modif = DBOperation.SelectSQL(conn, sql_modif, msg_modif)
        If rs_modif.RecordCount <= 0 Then
            MsgBox("没有该用户的信息,请确认用户名不可以修改!", MsgBoxStyle.Exclamation, PROJECT_TITLE_STRING)
            lb_user_name.Focus()
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If Trim(lb_user_name.Text) = g_username Then
            If MsgBox("这是您登录的用户名,修改该信息容易对您下一步操作产生影响,是否继续？", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.Yes Then

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
            MsgBox("密码不可以为空！", , PROJECT_TITLE_STRING)
            lb_password.Focus()
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If Trim(lb_password.Text).Length > 20 Then
            MsgBox("密码太长，长度超过20", , PROJECT_TITLE_STRING)
            lb_password.Focus()
            conn.Close()
            conn = Nothing
            Exit Sub
        End If

        If Trim(cb_right_user.Text) = "" Then
            MsgBox("岗位不可以为空，请选择！", , PROJECT_TITLE_STRING)
            cb_right_user.Focus()
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        'If Trim(right_user.Text) <> "管理员" And Trim(right_user.Text) <> "普通用户" Then
        '    MsgBox("权限输入错误，请选择！", , LoginForm.Property_welcome_win_obj.Property_msg_box_title)
        '    right_user.Focus()
        '    conn.Close()
        '    conn = Nothing
        '    Exit Sub
        'End If

        rs_modif.Fields("password").Value = Trim(lb_password.Text)
        rs_modif.Fields("rights").Value = Trim(cb_right_user.Text)
        rs_modif.Update()

        MsgBox("用户" + Trim(lb_user_name.Text) + "的信息修改完毕!", , PROJECT_TITLE_STRING)
        '增加操作记录
        Com_inf.Insert_Operation("修改用户" + Trim(lb_user_name.Text) + "的信息")

        'If Com_inf.Property_right_manage <> "管理员" Then
        '    MsgBox("您不是管理员，窗口即将关闭！", , LoginForm.Property_welcome_win_obj.Property_msg_box_title)
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
    ''' 删除用户信息
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_delete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_delete.Click
        'If Com_inf.Property_right_manage <> "管理员" Then
        '    MsgBox("您未被授权此操作！", , LoginForm.Property_welcome_win_obj.Property_msg_box_title)
        '    Me.Close()
        '    Exit Sub
        'End If
        If MsgBox("是否删除用户" + Trim(lb_user_name.Text) + "", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.Yes Then
            Dim sql_del As String
            Dim msg_del As String
            Dim conn As New ADODB.Connection
            Dim rs_del As New ADODB.Recordset
            Dim Node As TreeNode
            DBOperation.OpenConn(conn)


            msg_del = ""
            sql_del = "delete from manage where name='" + Trim(lb_user_name.Text) + "'"
            DBOperation.ExecuteSQL(conn, sql_del, msg_del)

            '增加操作记录
            Com_inf.Insert_Operation("删除用户" + Trim(lb_user_name.Text) + "的信息")


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
    ''' 为新的用户添加岗位
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_right_user_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_right_user.DropDown
        '为新的用户添加岗位
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
        Me.cb_right_user.Items.Clear()   '清空岗位列表
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