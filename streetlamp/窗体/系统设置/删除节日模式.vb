''' <summary>
''' 删除用户自定义的节日模式
''' </summary>
''' <remarks></remarks>
''' 
Public Class 删除节日模式
    Private m_node As Windows.Forms.TreeNode  '用户自定义模式节点

    ''' <summary>
    '''增加节日模式名称
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub del_mod()
        Dim rs As ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim conn As New ADODB.Connection
        DBOperation.OpenConn(conn)

        msg = ""
        sql = "select distinct(mod_title) from holiday_mod"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "del_mod", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        Me.tv__holiday_mod.Nodes.Clear()
        m_node = Me.tv__holiday_mod.Nodes.Add("用户自定义模式")
        While rs.EOF = False
            '控制类型+控制对象+控制方法
            m_node.Nodes.Add(Trim(rs.Fields("mod_title").Value))
            rs.MoveNext()

        End While

        rs.Close()
        rs = Nothing
        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    ''' 窗体载入函数
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 删除节日模式_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '载入所有节日模式
        '设置各个窗体的图标
        Me.Icon = New Icon("图片\favicon.ico", 32, 32)
        del_mod()
    End Sub

    ''' <summary>
    '''  '双击后显示节日模式的控制字符串
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub TreeView_holiday_mod_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tv__holiday_mod.DoubleClick

        Dim rs As New ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim i As Integer
        Dim conn As New ADODB.Connection
        DBOperation.OpenConn(conn)
        msg = ""
        rtb_holiday_mod.Text = ""  '清空
        While i < m_node.Nodes.Count
            If m_node.Nodes(i).IsSelected = True Then
                sql = "select * from holiday_mod where mod_title='" & m_node.Nodes(i).Text & "'"
                rs = DBOperation.SelectSQL(conn, sql, msg)
                If rs Is Nothing Then
                    MsgBox(MSG_ERROR_STRING & "TreeView_holiday_mod_DoubleClick", , PROJECT_TITLE_STRING)
                    conn.Close()
                    conn = Nothing
                    Exit Sub
                End If
                While rs.EOF = False
                    rtb_holiday_mod.AppendText(Trim(rs.Fields("control_type").Value) & " " & Trim(rs.Fields("control_obj").Value) & " " & Trim(rs.Fields("control_method").Value) & " 时间间隔：" & rs.Fields("time").Value & "秒" & vbCrLf)
                    rs.MoveNext()
                End While
            End If
            i += 1
        End While
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If

        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    ''' 删除选择的节日模式
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_del_holiday_mod_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_del_holiday_mod.Click

        Dim msg As String
        Dim sql As String
        Dim i As Integer = 0
        Dim conn As New ADODB.Connection
        DBOperation.OpenConn(conn)
        msg = ""

        While i < m_node.Nodes.Count
            If m_node.Nodes(i).IsSelected = True Then
                If MsgBox("是否删除用户自定义模式：" & m_node.Nodes(i).Text, MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.Yes Then
                    sql = "delete from holiday_mod where mod_title='" & m_node.Nodes(i).Text & "'"
                    DBOperation.ExecuteSQL(conn, sql, msg)
                    MsgBox("用户自定义模式删除成功！", , PROJECT_TITLE_STRING)
                    '增加操作记录
                    Com_inf.Insert_Operation("删除用户自定义模式：" & m_node.Nodes(i).Text)

                End If

            End If
            i += 1
        End While


        rtb_holiday_mod.Text = ""
        del_mod()
        conn.Close()
        conn = Nothing

    End Sub

    Private Sub 删除节日模式_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        g_windowclose = 1
    End Sub
End Class