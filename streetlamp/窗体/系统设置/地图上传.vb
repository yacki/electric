Public Class 地图上传
    Private m_filename As String

    ''' <summary>
    ''' 保存地图
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_save_map_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_save_map.Click
        If tb_map_path.Text = "" Then
            MsgBox("请选择地图的路径", , PROJECT_TITLE_STRING)
            tb_map_path.Focus()
            Exit Sub
        End If
        If tb_smallmap_path.Text = "" Then
            MsgBox("请选择鹰眼地图的路径", , PROJECT_TITLE_STRING)
            tb_smallmap_path.Focus()
            Exit Sub
        End If
        If tb_save_file_name.Text = "" Then
            MsgBox("请输入文件名", , PROJECT_TITLE_STRING)
            tb_save_file_name.Focus()
            Exit Sub
        End If

        If cb_area_map.Text = "" Then
            MsgBox("请选择地图中的所属区域", , PROJECT_TITLE_STRING)
            cb_area_map.Focus()
            Exit Sub
        End If

        m_filename = Trim(tb_save_file_name.Text)  '另存的文件名
        '查找该目录下是否存在该文件名
        For Each file As String In System.IO.Directory.GetFiles("map\")
            If file = "map\" & m_filename & ".jpg" Then
                MsgBox("该文件名已存在，请重新输入文件", , PROJECT_TITLE_STRING)
                tb_save_file_name.Focus()
                Exit Sub
            End If
        Next

        Dim rs As New ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim conn As New ADODB.Connection

        DBOperation.OpenConn(conn)
        msg = ""
        '  sql = "select * from map_list where map_name='" & m_filename & "' or area='" & Trim(area_map.Text) & "'"

        sql = "select * from map_list where map_name='" & m_filename & "' "
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "Save_map_Click", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs.RecordCount <= 0 Then
            System.IO.File.Copy(Trim(tb_map_path.Text), "map\" & m_filename & ".jpg")  '大的地图
            System.IO.File.Copy(Trim(tb_smallmap_path.Text), "map\s" & m_filename & ".jpg")  '鹰眼地图
            rs.AddNew()
            rs.Fields("map_name").Value = m_filename
            rs.Fields("area").Value = Trim(cb_area_map.Text)
            rs.Update()
            MsgBox("地图文件上传成功！", , PROJECT_TITLE_STRING)

            '增加操作记录
            Com_inf.Insert_Operation("上传地图文件：" & m_filename)

        Else
            MsgBox("保存文件名或地图所属区域已存在", , PROJECT_TITLE_STRING)

        End If


        rs.Close()
        rs = Nothing
        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    ''' 地图所属区域名称
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_area_map_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_area_map.DropDown
        '地图所属区域名称
        Dim rs As New ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim conn As New ADODB.Connection

        DBOperation.OpenConn(conn)

        msg = ""
        sql = "select * from area order by id"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        cb_area_map.Items.Clear()  '将区域名下拉框清空
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "area_map_DropDown", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If

        While rs.EOF = False
            cb_area_map.Items.Add(Trim(rs.Fields("area").Value)) '为区域的下拉框增加内容
            rs.MoveNext()
        End While

        rs.Close()
        rs = Nothing
        conn.Close()
        conn = Nothing

    End Sub

    ''' <summary>
    ''' 浏览大图
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_file_path_find_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_file_path_find.Click
        With find_map
            .Filter = "图片文件（*JPG)|*.jpg"  '筛选条件
            .Title = "上传图片"   '标题
            .RestoreDirectory = True
        End With
        find_map.FileName = ""
        find_map.ShowDialog()

        tb_map_path.Text = find_map.FileName  '取文件的路径名
    End Sub

    ''' <summary>
    ''' 窗体载入
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 地图上传_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = New Icon("图片\favicon.ico", 32, 32)
    End Sub

    ''' <summary>
    ''' 浏览小图
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub file_smallpath_find_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_file_smallpath_find.Click
        With find_map
            .Filter = "图片文件（*JPG)|*.jpg"  '筛选条件
            .Title = "上传图片"   '标题
            .RestoreDirectory = True
        End With
        find_map.FileName = ""
        find_map.ShowDialog()

        tb_smallmap_path.Text = find_map.FileName  '取文件的路径名
    End Sub

    Private Sub 地图上传_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        g_windowclose = 1
    End Sub
End Class