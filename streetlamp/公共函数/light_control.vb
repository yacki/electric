Public Class light_control
    Private m_open_tag As Boolean = False

    '光照控制
    Public Sub control_light()
        If g_lightvalue < g_lightvalueset Then
            '当前的光照值低于设置的阈值
            suntime_divoperation(True)
        End If

        If g_lightvalue >= g_lightvalueset Then
            '当前的光照值高于设定的阈值
            suntime_divoperation(False)
        End If


    End Sub

    ''' <summary>
    ''' 打开所有主控箱的灯
    '''通过判断光控的阈值，来决定主控箱是否打开(打开的主控箱仅为设置了经纬度的主控箱接触器，有效时间也仅在经纬度开关灯前后一个小时)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub open_alllamp()
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String = ""
        Dim control_lamp_obj As New control_lamp
        Dim result As Boolean
        Dim huilu, lamp As String  '控制的类型名称
        huilu = Com_inf.Get_Type_String(31)
        lamp = Com_inf.Get_Type_String(0)

        sql = "select control_box_name from control_box order by id desc"
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        While rs.EOF = False
            result = control_lamp_obj.Input_control_inf(huilu, "类型", Trim(rs.Fields("control_box_name").Value), "类型全开", 1, "全功率", 100, "", -1)

            result = control_lamp_obj.Input_control_inf("", "主控箱", Trim(rs.Fields("control_box_name").Value), "全开", 1, "全功率", 100, "", -1)

            rs.MoveNext()
        End While
        m_open_tag = True


        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If

        conn.Close()
        conn = Nothing

    End Sub

    ''' <summary>
    ''' 根据经纬度的时间来确定光控的时间，在经纬度控制的前一个小时内有效
    ''' </summary>
    ''' <param name="open_close" >true表示开，false表示关</param>
    ''' <remarks></remarks>
    Public Sub suntime_divoperation(ByVal open_close As Boolean)
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String
        Dim nowtime As DateTime
        Dim lamp_id As String
        Dim order_string As String '命令字符串
        Dim box_ox, type_ox As String
        Dim control_lamp_obj As New control_lamp
        Dim condition As String = ""  '控制内容
        nowtime = Now
        Dim mod_string As String = "" '控制类型
        Dim diangan As String = "全功率"
        Dim lamp_id_tag As String = ""
        Dim opentime, closetime As DateTime '开关灯的时间
        Dim openstate As Integer

        msg = ""
        ' sql = "select * from pianyi order by id "
        sql = "SELECT pianyi.lamp_id, pianyi.open_pianyi, pianyi.close_pianyi, pianyi.today_opentime, pianyi.today_closetime,lamp_inf.state FROM lamp_inf INNER JOIN pianyi ON lamp_inf.lamp_id = pianyi.lamp_id"

        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then

            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "suntime_divoperation" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            Exit Sub
        End If

        While rs.EOF = False
            order_string = ""
            lamp_id = Trim(rs.Fields("lamp_id").Value)
            openstate = rs.Fields("state").Value
            opentime = System.Convert.ToDateTime(rs.Fields("today_opentime").Value).AddHours(-1)  '经纬度设置开灯前一个小时光照开有效
            '精确到秒
            ' If opentime.TimeOfDay >= nowtime.TimeOfDay And opentime.TimeOfDay < nowtime.AddSeconds(3).TimeOfDay Then
            '2011年11月23日精确到分钟
            If nowtime >= opentime And open_close = True And (openstate = 0 Or openstate = 3) Then

                g_sethuilutag = True '有当前的时段控制

                '   control_box_name = Get_box_name(lamp_id)  '获取电控箱名称
                box_ox = Com_inf.Dec_to_Hex(Mid(lamp_id, 1, 4), 4)
                type_ox = Com_inf.Get_lampid_bin(Val(Mid(lamp_id, 5, 2)), Val(Mid(lamp_id, 7, LAMP_ID_LEN)))
                type_ox = Com_inf.BIN_to_HEX(type_ox)
                ' lamp_ox = Com_inf.Dec_to_ox(Mid(lamp_id, 7, 3), 2)

                order_string = Mid(box_ox, 3, 2) & " " & Mid(type_ox, 1, 2) & " " & Mid(type_ox, 3, 2) & " 1B 11 64 " & Mid(box_ox, 1, 2)

                '如果打开的是回路，则需要将回路控制下的所有单灯进行相关操作
                If Mid(lamp_id, 5, 2) = "31" Then
                    control_lamp_obj.open_close_huilulamp(1, Val(Mid(lamp_id, 7, LAMP_ID_LEN)), Mid(lamp_id, 1, 4))

                End If

                '改变lamp_inf中的状态信息
                sql = "update lamp_inf set state=4, result=4 where lamp_id='" & lamp_id & "'"
                DBOperation.ExecuteSQL(conn, sql, msg)

                condition = "光控节点"
                mod_string = "回路开"
                ' control_box_name = control_box_name & "主控箱节点"

            End If

            closetime = System.Convert.ToDateTime(rs.Fields("today_closetime").Value).AddHours(-1)
            ' If closetime.TimeOfDay >= nowtime.TimeOfDay And closetime.TimeOfDay <= nowtime.AddSeconds(3).TimeOfDay Then
            '2011年11月23日精确到分钟
            If nowtime >= closetime And open_close = False And (openstate = 1 Or openstate = 4) Then

                g_sethuilutag = True '有当前的时段控制

                'lamp_id = Trim(rs.Fields("lamp_id").Value)
                'control_box_name = Get_box_name(lamp_id)  '获取电控箱名称

                box_ox = Com_inf.Dec_to_Hex(Mid(lamp_id, 1, 4), 4)
                type_ox = Com_inf.Get_lampid_bin(Val(Mid(lamp_id, 5, 2)), Val(Mid(lamp_id, 7, LAMP_ID_LEN)))
                type_ox = Com_inf.BIN_to_HEX(type_ox)
                ' type_ox = Com_inf.Dec_to_ox(Mid(lamp_id, 5, 2), 2)
                ' lamp_ox = Com_inf.Dec_to_ox(Mid(lamp_id, 7, 3), 2)

                order_string = Mid(box_ox, 3, 2) & " " & Mid(type_ox, 1, 2) & " " & Mid(type_ox, 3, 2) & " 1C 13 00 " & Mid(box_ox, 1, 2)

                '如果关闭的是回路，则需要将回路控制下的所有单灯进行相关操作
                If Mid(lamp_id, 5, 2) = "31" Then
                    control_lamp_obj.open_close_huilulamp(0, Val(Mid(lamp_id, 7, LAMP_ID_LEN)), Mid(lamp_id, 1, 4))

                End If

                '改变lamp_inf中的状态信息
                '2011年11月15日关闭灯的时候自动将电流，电压及电功率的值置为0
                sql = "update lamp_inf set state=3, result=4,current_l=0,presure_l=0,power=0 where lamp_id='" & lamp_id & "'"
                DBOperation.ExecuteSQL(conn, sql, msg)

                condition = "光控节点"
                mod_string = "回路关"
                'control_box_name = control_box_name & "主控箱节点"

            End If

            If order_string <> "" Then
                control_lamp_obj.Input_db_control(order_string, Mid(lamp_id, 1, 4), "", 1, -1) '将命令加入到数据库中

                lamp_id_tag = Val(Mid(lamp_id, 1, 4)).ToString & "-" & Val(Mid(lamp_id, 5, 2)).ToString & "-" & Val(Mid(lamp_id, 7, LAMP_ID_LEN)).ToString

                '记录手控信息到数据库
                sql = "insert into hand_control_record(control_content,content_name,control_method,diangan,power,control_time,user_name) values('" & condition & "','" & lamp_id_tag & "','" & mod_string & "','" & diangan & "','" & 100 & "','" & Now() & "','" & g_username & "')"

                'sql = "insert into hand_control_record(control_content,content_name,control_method,diangan,power,control_time) values('" & condition & "','" & box_string & type_string & "','" & mod_string & "','" & diangan & "', 100 ,'" & Now() & "')"
                DBOperation.ExecuteSQL(conn, sql, msg)
            End If

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
    ''' 关闭所有主控箱的灯
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub close_alllamp()
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String = ""
        Dim control_lamp_obj As New control_lamp
        Dim result As Boolean
        Dim huilu, lamp As String  '控制的类型名称
        huilu = Com_inf.Get_Type_String(31)
        lamp = Com_inf.Get_Type_String(0)

        sql = "select control_box_name from control_box order by id desc"
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        While rs.EOF = False
            result = control_lamp_obj.Input_control_inf(huilu, "类型", Trim(rs.Fields("control_box_name").Value), "类型全闭", 1, "全功率", 100, "", -1)

            result = control_lamp_obj.Input_control_inf("", "主控箱", Trim(rs.Fields("control_box_name").Value), "全闭", 1, "全功率", 100, "", -1)

            rs.MoveNext()
        End While

        m_open_tag = False
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If

        conn.Close()
        conn = Nothing
    End Sub


End Class
