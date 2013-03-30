Public Class 删除控制模式
    ''' <summary>
    ''' 载入控制模式到树形列表
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub load_list_inf()
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String

        Dim i As Integer = 0
        Dim level1_num As Integer  '第一层节点的个数
        Dim level_content As String
        msg = ""

        DBOperation.OpenConn(conn)
        i = 0
        level1_num = g_divname.Length
        Div_infList.Nodes.Clear()   '清除节点
        While i < level1_num  '多个节点名称
            Div_infList.Nodes.Add(g_divname(i))
            sql = "select * from div_time where div_level='" & g_divname(i) & "' order by id"
            rs = DBOperation.SelectSQL(conn, sql, msg)
            If rs Is Nothing Then
                MsgBox(MSG_ERROR_STRING & "load_list_inf", , PROJECT_TITLE_STRING)
                conn.Close()
                conn = Nothing
                Exit Sub
            End If

            While rs.EOF = False  '节点信息
                level_content = Trim(rs.Fields("id").Value.ToString) & " " & Trim(rs.Fields("hour_beg").Value) & "时 " & Trim(rs.Fields("min_beg").Value) _
                & "分 " & Trim(rs.Fields("second_beg").Value) & "秒  " & Trim(rs.Fields("mod").Value)

                Div_infList.Nodes(i).Nodes.Add(level_content)

                rs.MoveNext()
            End While
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
    ''' 载入删除控制模式窗体
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 删除控制模式_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '设置各个窗体的图标
        Me.Icon = New Icon("图片\favicon.ico", 32, 32)
        Dim i As Integer
        load_list_inf()  '载入控制模式名称

        mod_level_choose.Items.Clear()
        i = 0

        While i < g_divname.Length
            If g_divname(i) Is Nothing Then
                i += 1
            Else
                mod_level_choose.Items.Add(g_divname(i))  '将控制模式添加到树形列表
                i += 1
            End If

        End While
        If mod_level_choose.Items.Count > 0 Then
            mod_level_choose.SelectedIndex = 0
        End If
    End Sub

    ''' <summary>
    ''' 删除控制模式
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub del_div_name_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles del_div_name.Click
        If mod_level_choose.Text = "" Then
            MsgBox("控制模式名称不可以为空", , PROJECT_TITLE_STRING)
            mod_level_choose.Focus()
            Exit Sub
        End If

        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim div_time_obj As New div_time_class
        msg = ""
        DBOperation.OpenConn(conn)
        sql = "delete from div_time where div_level='" & Trim(mod_level_choose.Text) & "'"
        If MsgBox("是否删除" & Trim(mod_level_choose.Text) & "的设置", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.Yes Then
            DBOperation.ExecuteSQL(conn, sql, msg)
            sql = "delete from road_level where div_time_level='" & Trim(mod_level_choose.Text) & "'"
            DBOperation.ExecuteSQL(conn, sql, msg)

            g_welcomewinobj.init_div_list()
            g_welcomewinobj.init_special_div_list()
            div_time_obj.Div_time_show() '删除后刷新主界面上的时段面板
            load_list_inf()
        End If


        conn.Close()
        conn = Nothing

    End Sub

    ''' <summary>
    ''' 选择控制模式名称
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mod_level_choose_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mod_level_choose.DropDown
        Dim i As Integer
        mod_level_choose.Items.Clear()
        i = 0

        While i < g_divname.Length
            If g_divname(i) Is Nothing Then
                i += 1
            Else
                mod_level_choose.Items.Add(g_divname(i))  '增加控制模式名称
                i += 1
            End If

        End While


        If mod_level_choose.Items.Count > 0 Then
            mod_level_choose.SelectedIndex = 0
        End If
    End Sub
End Class