Public Class 用户岗位分配
    ''' <summary>
    ''' 载入窗体
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 用户岗位分配_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: 这行代码将数据加载到表“Rights._Rights”中。您可以根据需要移动或移除它。
        Me.RightsTableAdapter.Fill(Me.Rights._Rights)
        '设置各个窗体的图标
        Me.Icon = New Icon("图片\favicon.ico", 32, 32)
    End Sub

    ''' <summary>
    ''' 不保存当前设置退出窗口
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_close_rights_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_close_rights.Click
        Me.Close()
    End Sub

    ''' <summary>
    ''' 增加岗位名称
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_gangweilist_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_gangweilist.DropDown
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        msg = ""
        DBOperation.OpenConn(conn)
        sql = "select gangwei from gangwei"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "gangweilist_DropDown", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        cb_gangweilist.Items.Clear()
        While rs.EOF = False
            cb_gangweilist.Items.Add(Trim(rs.Fields("gangwei").Value))  '添加岗位名称
            rs.MoveNext()
        End While

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    ''' 将左侧选中的权限名称添加到右侧列表中
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_add_rights_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_add_rights.Click

        'Me.Addrightslist.Items.Clear()  '清空左侧列表
        Dim i As Integer
        i = 0

        While i < Me.dgv_rights_list.Rows.Count
            If Me.dgv_rights_list.Rows(i).Cells("checkid").Value = 1 Then
                '选中的权限
                Me.clb_addrights_list.Items.Add(Trim(Me.dgv_rights_list.Rows(i).Cells("RightsDataGridViewTextBoxColumn").Value))
            End If
            i += 1
        End While
    End Sub

    ''' <summary>
    ''' 删除右侧的列表中被勾选的权限
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_delete_rights_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_delete_rights.Click

        Dim i As Integer = Me.clb_addrights_list.CheckedItems.Count - 1
        While i >= 0
            Me.clb_addrights_list.Items.Remove(Me.clb_addrights_list.CheckedItems(i))
            i -= 1
        End While
    End Sub

    ''' <summary>
    ''' 将左侧列表中的所有项添加到该岗位中,保存
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_save_right_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_save_right.Click
        If cb_gangweilist.Text = "" Then
            MsgBox("岗位不可以为空", , PROJECT_TITLE_STRING)
            cb_gangweilist.Focus()
            Exit Sub
        End If

        If clb_addrights_list.Items.Count = 0 Then
            MsgBox("权限不可为空", , PROJECT_TITLE_STRING)
            Exit Sub
        End If

        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String
        Dim i As Integer
        Dim rs As New ADODB.Recordset
        Dim add_string As String = ""


        DBOperation.OpenConn(conn) '连接数据库


        msg = ""
        '查询该岗位是否已经被分配了权限，如果已经分配，则询问是否更新
        sql = "select gangwei from gangwei_rights where gangwei='" & Trim(cb_gangweilist.Text) & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "save_right_Click", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If MsgBox("请确认修改岗位" & Trim(cb_gangweilist.Text) & "的权限", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.Yes Then
            If rs.RecordCount > 0 Then
                If MsgBox("该岗位已被分配权限，是否重新分配？", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.Yes Then
                    sql = "delete from gangwei_rights where gangwei='" & Trim(cb_gangweilist.Text) & "'"
                    DBOperation.ExecuteSQL(conn, sql, msg)
                Else
                    rs.Close()
                    rs = Nothing
                    conn.Close()
                    conn = Nothing
                    Exit Sub
                End If
            End If

            i = 0
            While i < Me.clb_addrights_list.Items.Count

                sql = "insert into gangwei_rights (gangwei,rights) values('" & Trim(cb_gangweilist.Text) & "','" & Me.clb_addrights_list.Items(i) & "')"  '增加岗位的权限
                DBOperation.ExecuteSQL(conn, sql, msg)

                add_string &= Trim(cb_gangweilist.Text) & " "
                i += 1
            End While
            MsgBox("权限分配成功", , PROJECT_TITLE_STRING)

            '增加操作记录
            If add_string.Length > 80 Then
                add_string = Mid(add_string, 1, 80) & "..."

            End If

            Com_inf.Insert_Operation("岗位" & Trim(cb_gangweilist.Text) & "分配权限：" & add_string)

        End If


        conn.Close()
        conn = Nothing

    End Sub

    Private Sub cb_gangweilist_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_gangweilist.SelectedIndexChanged
        '当岗位名称改变后
        'Dim rights_string As String
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String

        msg = ""
        sql = "select * from gangwei_rights where gangwei='" & Trim(cb_gangweilist.Text) & "'"
        DBOperation.OpenConn(conn)
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "gangweilist_SelectedIndexChanged", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
        End If
        clb_addrights_list.Items.Clear()  '清除岗位项目
        While rs.EOF = False
            clb_addrights_list.Items.Add(Trim(rs.Fields("rights").Value))
            rs.MoveNext()

        End While

        rs.Close()
        rs = Nothing
        conn.Close()
        conn = Nothing
    End Sub

    Private Sub 用户岗位分配_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        g_windowclose = 1
    End Sub
End Class