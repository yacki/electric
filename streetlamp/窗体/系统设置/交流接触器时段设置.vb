Public Class 交流接触器时段设置
    Dim m_lampid() As String '选中的交流接触器编号

    ''' <summary>
    ''' 载入窗体，初始化将类型号位31的编号全罗列出来
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 交流接触器时段设置_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '设置各个窗体的图标
        Me.Icon = New Icon("图片\favicon.ico", 32, 32)
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String

        msg = ""
        sql = "select * from lamp_inf where lamp_type_id=31"

        DBOperation.OpenConn(conn)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "交流接触器时段设置_Load", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If

        jiechuqi_list.Items.Clear()  '清除所有的选项
        rs = DBOperation.SelectSQL(conn, sql, msg)
        While rs.EOF = False
            jiechuqi_list.Items.Add(Val(Mid(Trim(rs.Fields("lamp_id").Value), 1, 4)).ToString & " - " & Val(Mid(Trim(rs.Fields("lamp_id").Value), 5, 2)).ToString & " - " & Val(Mid(Trim(rs.Fields("lamp_id").Value), 7, LAMP_ID_LEN)).ToString)
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
    ''' 将选中的交流接触器编号，增加到右边的列表中
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Add_jiaoliu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Add_jiaoliu.Click
        Dim i As Integer
        Dim list_string As String
        Dim id(4) As String   '存放单个灯的编号

        i = 0
        list_string = ""
        ReDim m_lampid(Me.jiechuqi_list.CheckedItems.Count)
        While i < Me.jiechuqi_list.CheckedItems.Count
            list_string &= Me.jiechuqi_list.CheckedItems.Item(i) & "  "
            If i Mod 4 = 0 And i > 4 Then
                list_string &= vbCrLf
            End If
            '将灯的编号恢复成原来的格式
            id = Me.jiechuqi_list.CheckedItems.Item(i).ToString.Split(" ")

            While id(0).Length < 4  '四位的主控箱
                id(0) = "0" & id(0)
            End While

            While id(2).Length < 2  '两位的类型
                id(2) = "0" & id(2)
            End While

            While id(4).Length < 3 '三位的灯的编号
                id(4) = "0" & id(4)
            End While

            m_lampid(i) = id(0) & id(2) & id(4)

            i += 1
        End While


        jiechuqi_collection.Text = list_string
    End Sub

    ''' <summary>
    ''' 选取各种控制模式名称
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mod_name_list_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mod_name_list.DropDown
        '选择模式级别
        mod_name_list.Items.Clear()
        Dim i As Integer = 0

        While i < g_divname.Length
            If g_divname(i) Is Nothing Then
                i += 1
            Else
                mod_name_list.Items.Add(g_divname(i))
                i += 1
            End If

        End While


        If mod_name_list.Items.Count > 0 Then
            mod_name_list.SelectedIndex = 0
        End If

    End Sub

    ''' <summary>
    ''' 增加选中的交流接触器的编号的时段模式
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub input_jiaoliu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles input_jiaoliu.Click
        If jiechuqi_collection.Text = "" Then
            MsgBox("请先添加主控箱节点编号", , PROJECT_TITLE_STRING)
            Me.Add_jiaoliu.Focus()
            Exit Sub
        End If

        If mod_name_list.Text = "" Then
            MsgBox("请选择控制模式名称", , PROJECT_TITLE_STRING)
            mod_name_list.Focus()
            Exit Sub
        End If

        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim i As Integer
        Dim rs As New ADODB.Recordset

        msg = ""
        sql = ""
        i = 0
        DBOperation.OpenConn(conn)

        '********************增加判断用户所选择的模式中是否有类型控制命令*******************
        sql = "select mod from div_time where div_level='" & Trim(mod_name_list.Text) & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "input_jiaoliu_Click", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        While rs.EOF = False
            If Mid(Trim(rs.Fields("mod").Value), 1, 2) = "类型" Then
                MsgBox("请选择只有回路开关控制的模式名称", , PROJECT_TITLE_STRING)
                rs.Close()
                rs = Nothing
                conn.Close()
                conn = Nothing
                Exit Sub
            End If
            rs.MoveNext()
        End While

        While i < m_lampid.Length - 1
            sql = "select * from road_level where lamp_id='" & m_lampid(i) & "'"
            rs = DBOperation.SelectSQL(conn, sql, msg)
            If rs Is Nothing Then
                MsgBox(MSG_ERROR_STRING & "input_jiaoliu_Click", , PROJECT_TITLE_STRING)
                conn.Close()
                conn = Nothing
                Exit Sub
            End If

            If rs.RecordCount > 0 Then
                If MsgBox("该节点时段设置已存在:" & Val(Mid(m_lampid(i), 1, 4)) & "-" & Val(Mid(m_lampid(i), 5, 2)) & "-" & Val(Mid(m_lampid(i), 7, LAMP_ID_LEN)) & "，是否覆盖?", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.Yes Then
                    rs.Fields("div_time_level").Value = Trim(mod_name_list.Text)
                    rs.Update()

                End If
            Else
                sql = "insert into road_level(control_box_id, div_time_level, type_id, lamp_id ) values('" & Mid(m_lampid(i), 1, 4) & "', '" & Trim(Me.mod_name_list.Text) & "', 31 , '" & m_lampid(i) & "')"
                DBOperation.ExecuteSQL(conn, sql, msg)
            End If


            i += 1
        End While

        MsgBox("主控箱节点时段设定成功", , PROJECT_TITLE_STRING)


        '刷新时段设置列表
        Dim div_time_obj As New div_time_class
        div_time_obj.Div_time_show()

        conn.Close()
        conn = Nothing

    End Sub

End Class