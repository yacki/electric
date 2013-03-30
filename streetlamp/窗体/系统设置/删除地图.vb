Public Class 删除地图

    ''' <summary>
    ''' 增加地图名称列表
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_delmaplist_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_delmaplist.DropDown
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String

        msg = ""
        sql = "select map_name from map_list"
        DBOperation.OpenConn(conn)
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "delmaplist_DropDown", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
        End If
        Me.cb_delmaplist.Items.Clear() '清除列表项
        While rs.EOF = False
            Me.cb_delmaplist.Items.Add(Trim(rs.Fields("map_name").Value))
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
    ''' 根据所选择的名称，删除
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub delmapname_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles delmapname.Click
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim file_name, map_name, smallmap_name As String

        msg = ""
        If Me.cb_delmaplist.Text = "" Then
            MsgBox("请选择需要删除的地图名称")
            Me.cb_delmaplist.Focus()
            Exit Sub
        End If
        file_name = Trim(cb_delmaplist.Text)  '另存的文件名
        map_name = "map\" & g_mapname & ".jpg"
        smallmap_name = "map\s" & g_mapname & ".jpg"
        '查找该目录下是否存在该文件名
        For Each file As String In System.IO.Directory.GetFiles("map\")
            If file = "map\" & file_name & ".jpg" Then

                ' Com_inf.Read_map()
                If map_name = file Then  '删除的地图为当前显示的地图
                    MsgBox("当前地图正在使用，不可以删除！", , PROJECT_TITLE_STRING)
                    Exit Sub
                Else

                    System.IO.File.Delete(file)
                End If
                Exit For
            End If
            If file = "map\s" & file_name & ".jpg" Then

                If smallmap_name = file Then  '删除的地图为当前显示的地图
                    MsgBox("当前地图正在使用，不可以删除！", , PROJECT_TITLE_STRING)
                    Exit Sub
                Else

                    System.IO.File.Delete(file)
                End If
            End If
        Next


        DBOperation.OpenConn(conn)  '打开数据库连接
        sql = "delete from map_list where map_name='" & Trim(Me.cb_delmaplist.Text) & "'"
        DBOperation.ExecuteSQL(conn, sql, msg)
        MsgBox("删除成功", , PROJECT_TITLE_STRING)

        '增加操作记录
        Com_inf.Insert_Operation("删除地图文件：" & Trim(Me.cb_delmaplist.Text))
        conn.Close()
        conn = Nothing

        Me.Close()

    End Sub

    ''' <summary>
    ''' 载入窗体
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 删除地图_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '设置各个窗体的图标
        Me.Icon = New Icon("图片\favicon.ico", 32, 32)
    End Sub

    Private Sub 删除地图_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        g_windowclose = 1
    End Sub
End Class