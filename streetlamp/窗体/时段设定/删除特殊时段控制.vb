Public Class 删除特殊时段控制
    ''' <summary>
    ''' 删除特殊时段控制模式窗体
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 特殊时段控制_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '设置各个窗体的图标
        Me.Icon = New Icon("图片\favicon.ico", 32, 32)
        Dim i As Integer
        time_div()  '载入特殊时段控制模式

        mod_level_choose.Items.Clear()
        i = 0

        While i < g_specialdivname.Length
            If g_specialdivname(i) Is Nothing Then
                i += 1
            Else
                mod_level_choose.Items.Add(g_specialdivname(i)) '添加到树形列表中
                i += 1
            End If

        End While
        If mod_level_choose.Items.Count > 0 Then
            mod_level_choose.SelectedIndex = 0
        End If
    End Sub

    ''' <summary>
    ''' 2010年10月4日树形列表形式
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub time_div()
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
        level1_num = g_specialdivname.Length
        Spe_div_list.Nodes.Clear()   '清除节点
        While i < level1_num
            Spe_div_list.Nodes.Add(g_specialdivname(i))
            sql = "select * from special_div_time where name='" & g_specialdivname(i) & "' order by id"
            rs = DBOperation.SelectSQL(conn, sql, msg)
            If rs Is Nothing Then
                MsgBox(MSG_ERROR_STRING & "time_div", , PROJECT_TITLE_STRING)
                conn.Close()
                conn = Nothing
                Exit Sub
            End If

            While rs.EOF = False
                level_content = rs.Fields("time").Value & " " & rs.Fields("mod").Value
                Spe_div_list.Nodes(i).Nodes.Add(level_content) '增加模式节点

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
    '''  删除控制模式
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
        sql = "delete from special_div_time where name='" & Trim(mod_level_choose.Text) & "'"
        If MsgBox("是否删除" & Trim(mod_level_choose.Text) & "的设置", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.Yes Then
            DBOperation.ExecuteSQL(conn, sql, msg)
            sql = "delete from special_road_level where div_time_level='" & Trim(mod_level_choose.Text) & "'"
            DBOperation.ExecuteSQL(conn, sql, msg)

            '时段控制面板信息()
            g_welcomewinobj.init_div_list()
            g_welcomewinobj.init_special_div_list()

            div_time_obj.Div_time_show()
            time_div()
        End If

        conn.Close()
        conn = Nothing

    End Sub

    ''' <summary>
    ''' 名称列表
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mod_level_choose_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mod_level_choose.DropDown
        Dim i As Integer
        mod_level_choose.Items.Clear()
        i = 0

        While i < g_specialdivname.Length
            If g_specialdivname(i) Is Nothing Then
                i += 1
            Else
                mod_level_choose.Items.Add(g_specialdivname(i))  '增加特殊控制模式名称
                i += 1
            End If

        End While


        If mod_level_choose.Items.Count > 0 Then
            mod_level_choose.SelectedIndex = 0
        End If
    End Sub
End Class