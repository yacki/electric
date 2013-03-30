
''' <summary>
''' 时段处理的相关函数
''' </summary>
''' <remarks></remarks>
Public Class div_time_class
    '时段控制类
    '  Private street_l As String

    Private div_time_tag As Integer   '时段开始标志
    Private city_string, area_string, street_string, level_string As String
    Private box_string, type_string As String   '电控箱名称，景观灯类型
    Private box_id, type_id As String  '电控箱编号，景观灯编号
#Region "属性方法"

    Public Property Property_div_time_tag() As Integer
        Get
            Return div_time_tag
        End Get
        Set(ByVal value As Integer)
            div_time_tag = value
        End Set
    End Property
#End Region

    'Public Function Get_city_string() As String
    '    Get_city_string = city_string
    'End Function
    'Public Function Get_area_string() As String
    '    Get_area_string = area_string
    'End Function
    'Public Function Get_street_string() As String
    '    Get_street_string = street_string
    'End Function

    'Public Function Get_level_string() As String
    '    Get_level_string = level_string
    'End Function
    'Public Function Get_box_string() As String   '获取电控箱名称
    '    Get_box_string = box_string
    'End Function

    'Public Function Get_type_string() As String  '获取景观灯类型
    '    Get_type_string = type_string
    'End Function
    ''' <summary>
    '''  '按城市、区域、街道、电控箱级别进行时段设定
    ''' </summary>
    ''' <param name="control_level">控制级别</param>
    ''' <param name="control_name">控制名称</param>
    ''' <param name="mod_time">模式级别</param>
    ''' <remarks></remarks>
    Public Sub Div_control_function(ByVal control_level As String, ByVal control_name As String, ByVal mod_time As String, ByVal special_tag As Boolean)

        Dim rs, rs_insert As New ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim conn As New ADODB.Connection
        Dim type_id As String  '类型ID
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        msg = ""

        '********************增加判断用户所选择的模式中是否有类型控制命令*******************
        sql = "select mod from div_time where div_level='" & mod_time & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Div_control_function" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        While rs.EOF = False
            If Mid(Trim(rs.Fields("mod").Value), 1, 2) = "回路" Then
                MsgBox("请选择只有类型开关控制的模式名称", , PROJECT_TITLE_STRING)
                rs.Close()
                rs = Nothing
                conn.Close()
                conn = Nothing
                Exit Sub
            End If
            rs.MoveNext()
        End While

        Select Case control_level
            Case "city"
                '按城市控制
                sql = "select distinct(type_string),control_box_id,control_box_name from lamp_street where city='" & control_name & "'"

            Case "area"
                '按区域控制
                sql = "select distinct(type_string) ,control_box_id,control_box_name from lamp_street where area='" & control_name & "'"

            Case "street"
                '按街道控制
                sql = "select distinct(type_string) ,control_box_id ,control_box_name from lamp_street where street='" & control_name & "'"

            Case "box"
                '按电控箱控制
                sql = "select distinct(type_string) ,control_box_id,control_box_name from lamp_street where control_box_name='" & control_name & "'"
            Case Else
                conn.Close()
                conn = Nothing
                Exit Sub
        End Select

        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Div_control_function" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            Exit Sub
        End If

        If rs.RecordCount <= 0 Then
            MsgBox("不存在区域信息，请重新选择", , PROJECT_TITLE_STRING)
            rs.Close()
            rs = Nothing
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        While rs.EOF = False
            '将控制范围内的类型控制写入road_level表中
            sql = "select type_id from lamp_type where type_string='" & Trim(rs.Fields("type_string").Value) & "'"
            rs_insert = DBOperation.SelectSQL(conn, sql, msg)
            If rs_insert Is Nothing Then
                g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Div_control_function" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

                conn.Close()
                conn = Nothing
                Exit Sub
            End If
            type_id = Trim(rs_insert.Fields("type_id").Value)
            If special_tag = False Then
                sql = "select * from road_level where control_box_id='" & Trim(rs.Fields("control_box_id").Value) & "' and type_id='" & type_id & "' and lamp_id is NULL"
            Else
                sql = "select * from special_road_level where control_box_id='" & Trim(rs.Fields("control_box_id").Value) & "' and type_id='" & type_id & "' and lamp_id is NULL"

            End If
            rs_insert = DBOperation.SelectSQL(conn, sql, msg)
            If rs_insert Is Nothing Then
                g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Div_control_function" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

                conn.Close()
                conn = Nothing
                Exit Sub
            End If
            If rs_insert.RecordCount > 0 Then

                If MsgBox(Trim(rs.Fields("control_box_name").Value) & Trim(rs.Fields("type_string").Value) & "时段控制方式已经存在，是否替换？", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.Yes Then
                    rs_insert.Fields("div_time_level").Value = mod_time
                    rs_insert.Update()
                End If
                rs.MoveNext()

                Continue While
            Else
                '增加时段控制方式
                rs_insert.AddNew()
                rs_insert.Fields("control_box_id").Value = Trim(rs.Fields("control_box_id").Value)
                rs_insert.Fields("div_time_level").Value = mod_time
                rs_insert.Fields("type_id").Value = type_id

                rs_insert.Update()
            End If
            rs.MoveNext()

        End While

        MsgBox("模式控制添加成功", , PROJECT_TITLE_STRING)
        If rs_insert.State = 1 Then
            rs_insert.Close()
            rs_insert = Nothing
        End If

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If

        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    '''  '按区域级别进行时段设定
    ''' </summary>
    ''' <param name="box_string">区域名称</param>
    ''' <param name="mod_time">模式级别</param>
    ''' <remarks></remarks>
    Public Sub box_control_function(ByVal box_string As String, ByVal mod_time As String)

        Dim rs_box, rs_box_insert As New ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim conn As New ADODB.Connection
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If

        msg = ""
        sql = "select * from box_lamptype_view where control_box_name='" & box_string & "'"
        rs_box = DBOperation.SelectSQL(conn, sql, msg)
        If rs_box Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "box_control_function" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            Exit Sub
        End If

        If rs_box.RecordCount <= 0 Then
            MsgBox("不存在区域信息，请重新选择", , PROJECT_TITLE_STRING)
            rs_box.Close()
            rs_box = Nothing
            rs_box_insert.Close()
            rs_box_insert = Nothing
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        While rs_box.EOF = False
            '将电控箱及类型控制写入road_level表中

            sql = "select * from road_level where control_box_id='" & Trim(rs_box.Fields("control_box_id").Value) & "' and type_id='" & rs_box.Fields("type_id").Value & "' and lamp_id is NULL"
            rs_box_insert = DBOperation.SelectSQL(conn, sql, msg)
            If rs_box_insert Is Nothing Then
                g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "box_control_function" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

                conn.Close()
                conn = Nothing
                Exit Sub
            End If
            If rs_box_insert.RecordCount > 0 Then

                If MsgBox(Trim(rs_box.Fields("control_box_name").Value) & Trim(rs_box.Fields("type_string").Value) & "时段控制方式已经存在，是否替换？", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.Yes Then
                    rs_box_insert.Fields("div_time_level").Value = mod_time
                    rs_box_insert.Update()
                End If
                rs_box.MoveNext()

                Continue While
            Else
                '增加时段控制方式
                rs_box_insert.AddNew()
                rs_box_insert.Fields("control_box_id").Value = Trim(rs_box.Fields("control_box_id").Value)
                rs_box_insert.Fields("div_time_level").Value = mod_time
                rs_box_insert.Fields("type_id").Value = rs_box.Fields("type_id")

                rs_box_insert.Update()
            End If
            rs_box.MoveNext()

        End While
        rs_box_insert.Close()
        rs_box_insert = Nothing

        rs_box.Close()
        rs_box = Nothing
        conn.Close()
        conn = Nothing
    End Sub
    ''' <summary>
    ''' '按类型进行时段设定
    ''' </summary>
    ''' <param name="box_string">电控箱名称</param>
    ''' <param name="type_string">类型</param>
    ''' <param name="mod_time">模式级别</param>
    ''' <remarks></remarks>
    Public Sub type_control_function(ByVal box_string As String, ByVal type_string As String, ByVal mod_time As String, ByVal special_tag As Boolean)

        Dim rs_box, rs_box_insert As New ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim conn As New ADODB.Connection
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If


        msg = ""


        '********************增加判断用户所选择的模式中是否有类型控制命令*******************
        sql = "select mod from div_time where div_level='" & mod_time & "'"
        rs_box = DBOperation.SelectSQL(conn, sql, msg)
        If rs_box Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "type_control_function" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        While rs_box.EOF = False
            If Mid(Trim(rs_box.Fields("mod").Value), 1, 2) = "回路" Then
                MsgBox("请选择只有类型开关控制的模式名称", , PROJECT_TITLE_STRING)
                rs_box.Close()
                rs_box = Nothing
                conn.Close()
                conn = Nothing
                Exit Sub
            End If
            rs_box.MoveNext()
        End While


        sql = "select * from box_lamptype_view where control_box_name='" & box_string & "'and type_string='" & type_string & "'"
        rs_box = DBOperation.SelectSQL(conn, sql, msg)
        If rs_box Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "type_control_function" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            Exit Sub
        End If

        If rs_box.RecordCount <= 0 Then
            MsgBox("不存在景观灯类型信息，请重新选择", , PROJECT_TITLE_STRING)
            rs_box.Close()
            rs_box = Nothing
            conn.Close()
            conn = Nothing
            Exit Sub

        End If

        While rs_box.EOF = False
            '将电控箱及类型控制写入road_level表中
            If special_tag = False Then
                sql = "select * from road_level where control_box_id='" & Trim(rs_box.Fields("control_box_id").Value) & "' and type_id='" & rs_box.Fields("type_id").Value & "' and lamp_id is  NULL"
            Else
                sql = "select * from special_road_level where control_box_id='" & Trim(rs_box.Fields("control_box_id").Value) & "' and type_id='" & rs_box.Fields("type_id").Value & "' and lamp_id is  NULL"

            End If
            rs_box_insert = DBOperation.SelectSQL(conn, sql, msg)
            If rs_box_insert Is Nothing Then
                g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "type_control_function" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

                conn.Close()
                conn = Nothing
                Exit Sub
            End If
            If rs_box_insert.RecordCount > 0 Then
                If MsgBox(Trim(rs_box.Fields("control_box_name").Value) & Trim(rs_box.Fields("type_string").Value) & "时段控制方式已经存在,是否替换？", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) Then
                    rs_box_insert.Fields("div_time_level").Value = mod_time
                    rs_box_insert.Update()
                End If
                rs_box.MoveNext()

                Continue While
            Else
                '增加时段控制方式
                rs_box_insert.AddNew()
                rs_box_insert.Fields("control_box_id").Value = Trim(rs_box.Fields("control_box_id").Value)
                rs_box_insert.Fields("div_time_level").Value = mod_time
                rs_box_insert.Fields("type_id").Value = rs_box.Fields("type_id")

                rs_box_insert.Update()
            End If
            rs_box.MoveNext()

        End While
        MsgBox("模式控制添加成功", , PROJECT_TITLE_STRING)
        rs_box_insert.Close()
        rs_box_insert = Nothing
        rs_box.Close()
        rs_box = Nothing
        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    '''   '根据各个街道设置的级别，进行特殊时段控制
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub special_clock_time(ByVal single_tag As System.Boolean)

        Dim clock_string As String
        Dim control_lamp_obj As New control_lamp
        Dim rs_level As New ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim mod_string As String
        Dim lv_string As String
        Dim control_string As String  '命令字节
        Dim ox_str As String '四位的景观灯编号
        Dim lamp_id_string As String  '景观灯编号
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        'Dim i As Integer
        Dim box_id_ox As String
        Dim state_tag As Integer
        Dim communication_time As Integer  '判断通信是否正常的次数
        Dim diangan_id, diangan As String
        Dim condition As String '控制对象
        Dim lamp_id As String = ""
        Dim time As System.DateTime
        Dim hand_type As String = "类型"
        'Dim box_string As String
        Dim i As Integer
        Dim lamp_id_tag As String = ""
        Dim control_box_name As String = "" '电控箱名称
        Dim lamptype_string As String = "" '灯的类型
        Dim order_type As String = 1 '标志命令协议的类型，1表示7字节，2表示8字节

        'Dim control_lamp_obj As New control_lamp

        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If

        clock_string = Format(Now(), "yyyy-MM-dd HH:mm:ss").ToString   '当前时间
        ''刷新路灯状况
        control_string = ""
        Dim time_s1 As DateTime  '记录当前时间
        state_tag = 0
        msg = ""
        mod_string = ""
        lv_string = ""
        communication_time = 50  '最长重复发查询命令50次
        ' rs = New ADODB.Recordset
        time_s1 = Format(Now, "yyyy-MM-dd HH:mm:ss") '当前时间


        sql = "select * from special_div_time"

        rs_level = DBOperation.SelectSQL(conn, sql, msg)
        If rs_level.RecordCount <= 0 Then  '如果没有时控的街道，则返回
            rs_level.Close()
            rs_level = Nothing
            conn.Close()
            conn = Nothing

            Exit Sub
        End If

        '时控操作时按每个街道
        While rs_level.EOF = False

            diangan = Trim(rs_level.Fields("diangan").Value)
            'time_s2 = Trim(rs_level.Fields("hour_beg").Value) & ":" & Trim(rs_level.Fields("min_beg").Value) & ":" & Trim(rs_level.Fields("second_beg").Value)  '时段控制的边界时间

            time = Format(rs_level.Fields("time").Value, "yyyy-MM-dd HH:mm:ss")

            'If time.ToString = Now.ToString Or time.AddMinutes(2).ToString = Now.ToString Or time.AddMinutes(5).ToString = Now.ToString Or time.AddMinutes(10).ToString = Now.ToString Or time.AddMinutes(15).ToString = Now.ToString Then
            If time_s1.Year = time.Year And time_s1.Month = time.Month And time_s1.Day = time.Day And time_s1.TimeOfDay.Hours = time.TimeOfDay.Hours And time_s1.TimeOfDay.Minutes = time.TimeOfDay.Minutes And (time_s1.TimeOfDay.Seconds < 50) Then


                If single_tag = False Then
                    '整体控制
                    sql = "select * from Special_road_level where div_time_level='" & Trim(rs_level.Fields("name").Value) & "' and lamp_id is NULL"

                Else
                    sql = "select * from Special_road_level where div_time_level='" & Trim(rs_level.Fields("name").Value) & "' and lamp_id is not NULL"

                End If
                rs = DBOperation.SelectSQL(conn, sql, msg)  '获取各个时段的详细信息


                mod_string = ""
                While rs.EOF = False

                    box_id = Trim(rs.Fields("control_box_id").Value) '电控箱编号
                    '2012年9月7日，对每个命令要判断其主控箱是否停运，如果停运则不执行
                    If Com_inf.get_controlbox_state(box_id) = False Then
                        rs.MoveNext()
                        Continue While
                    End If
                    control_box_name = Com_inf.Get_box_name(box_id)
                    type_id = Trim(rs.Fields("type_id").Value) '景观灯编号
                    lamptype_string = Com_inf.Get_lamp_type(type_id)
                    'box_string = Trim(rs.Fields("control_box_name").Value)  '电控箱名称
                    'type_string = Trim(rs.Fields("type_string").Value) '景观灯类型
                    level_string = Trim(rs.Fields("div_time_level").Value) '时段级别
                    mod_string = Trim(rs_level.Fields("mod").Value)  '控制模式
                    box_id_ox = Com_inf.Dec_to_Hex(Trim(rs.Fields("control_box_id").Value), 4) '电控箱编号转换
                    control_string = ""



                    If single_tag = False Then  '整体
                        lamp_id_string = box_id & type_id
                        While lamp_id_string.Length < LAMP_ID_LEN + 5
                            lamp_id_string &= "0"
                        End While
                        lamp_id_string &= "1"
                        ox_str = Com_inf.Get_lampid_bin(Val(type_id), Val(Mid(lamp_id_string, 7, LAMP_ID_LEN)))
                        'ox_str = Com_inf.Dec_to_Bin(Mid(lamp_id_string, 5, 2), 5) & Com_inf.Dec_to_Bin(Mid(lamp_id_string, 7, LAMP_ID_LEN), 11)
                        ox_str = Com_inf.BIN_to_HEX(ox_str)
                        ox_str = Mid(ox_str, 1, 2) & " " & Mid(ox_str, 3, 2)

                        Select Case mod_string
                            Case "类型全闭"
                                order_type = 1
                                control_string &= Mid(box_id_ox, 3, 2) & " " & ox_str & " 42 "  '全闭控制命令字符串
                                sql = "update lamp_inf set div_time_id=0, state=3, result=4 ,current_l=0,presure_l=0,power=0,presure_end=0,date='" & Now & "' where control_box_id='" & box_id & "' and lamp_type_id='" & type_id & "'"
                                DBOperation.ExecuteSQL(conn, sql, msg)

                                '如果控制的是回路，则将回路中的路灯全部设置为关
                                If Mid(lamp_id_string, 5, 2) = "31" Then
                                    i = 1
                                    While i <= g_lampnum
                                        control_lamp_obj.open_close_huilulamp(0, i, box_id)
                                        i += 1

                                    End While
                                End If
                                '模拟时钟.full_close(lv_string, Trim(rs_road.Fields("control_box_id").Value))
                            Case "类型全开"
                                order_type = 1
                                control_string &= Mid(box_id_ox, 3, 2) & " " & ox_str & " 41 "    '全开控制命令字符串

                                sql = "update lamp_inf set div_time_id=1, state=4, result=4 , date='" & Now & "' where control_box_id='" & box_id & "' and lamp_type_id='" & type_id & "'"
                                DBOperation.ExecuteSQL(conn, sql, msg)


                                '如果控制的是回路，则将回路中的路灯全部设置为关
                                If Mid(lamp_id_string, 5, 2) = "31" Then
                                    i = 1
                                    While i <= g_lampnum
                                        control_lamp_obj.open_close_huilulamp(1, i, box_id)
                                        i += 1

                                    End While
                                End If

                            Case "类型奇开"
                                order_type = 1
                                control_string &= Mid(box_id_ox, 3, 2) & " " & ox_str & " 43 "  '奇开控制命令字符串

                                '打开奇数的编号
                                sql = "update lamp_inf set div_time_id=1, state=4, result=4 where CAST(SUBSTRING(lamp_id, 7, " & LAMP_ID_LEN & ") AS int) % 2 =1 and control_box_id='" & box_id & "' and lamp_type_id='" & type_id & "'"
                                DBOperation.ExecuteSQL(conn, sql, msg)
                                '关闭偶数的编号
                                sql = "update lamp_inf set div_time_id=0, state=3, result=4,current_l=0,presure_l=0,power=0,presure_end=0 where CAST(SUBSTRING(lamp_id, 7, " & LAMP_ID_LEN & ") AS int) % 2 =0 and control_box_id='" & box_id & "' and lamp_type_id='" & type_id & "'"
                                DBOperation.ExecuteSQL(conn, sql, msg)


                                '如果控制的是回路，则将回路中的路灯全部设置为关
                                If Mid(lamp_id_string, 5, 2) = "31" Then
                                    i = 1
                                    While i <= g_lampnum
                                        If i Mod 2 = 0 Then
                                            control_lamp_obj.open_close_huilulamp(0, i, box_id)
                                        Else
                                            control_lamp_obj.open_close_huilulamp(1, i, box_id)

                                        End If
                                        i += 1

                                    End While
                                End If
                            Case "类型奇闭"
                                order_type = 1
                                control_string &= Mid(box_id_ox, 3, 2) & " " & ox_str & " 44 " '奇闭控制命令字符串
                                '关闭奇数的编号
                                sql = "update lamp_inf set div_time_id=0, state=3, result=4,current_l=0,presure_l=0,power=0,presure_end=0 where CAST(SUBSTRING(lamp_id, 7, " & LAMP_ID_LEN & ") AS int) % 2 =1 and control_box_id='" & box_id & "' and lamp_type_id='" & type_id & "'"
                                DBOperation.ExecuteSQL(conn, sql, msg)

                                '如果控制的是回路，则将回路中的路灯全部设置为关
                                If Mid(lamp_id_string, 5, 2) = "31" Then
                                    i = 1
                                    While i <= g_lampnum
                                        If i Mod 2 = 1 Then
                                            control_lamp_obj.open_close_huilulamp(0, i, box_id)

                                        End If
                                        i += 1

                                    End While
                                End If
                            Case "类型偶开"
                                order_type = 1
                                control_string &= Mid(box_id_ox, 3, 2) & " " & ox_str & " 45 "  '偶开控制命令字符串
                                '关闭奇数的编号
                                sql = "update lamp_inf set div_time_id=0, state=3, result=4,current_l=0,presure_l=0,power=0,presure_end=0 where CAST(SUBSTRING(lamp_id, 7, " & LAMP_ID_LEN & ") AS int) % 2 =1 and control_box_id='" & box_id & "' and lamp_type_id='" & type_id & "'"
                                DBOperation.ExecuteSQL(conn, sql, msg)
                                '打开偶数的编号
                                sql = "update lamp_inf set div_time_id=1, state=4, result=4 where CAST(SUBSTRING(lamp_id, 7, " & LAMP_ID_LEN & ") AS int) % 2 =0 and control_box_id='" & box_id & "' and lamp_type_id='" & type_id & "'"
                                DBOperation.ExecuteSQL(conn, sql, msg)

                                '如果控制的是回路，则将回路中的路灯全部设置为关
                                If Mid(lamp_id_string, 5, 2) = "31" Then
                                    i = 1
                                    While i <= g_lampnum
                                        If i Mod 2 = 1 Then
                                            control_lamp_obj.open_close_huilulamp(0, i, box_id)
                                        Else
                                            control_lamp_obj.open_close_huilulamp(1, i, box_id)

                                        End If
                                        i += 1

                                    End While
                                End If
                            Case "类型偶闭"
                                order_type = 1
                                control_string &= Mid(box_id_ox, 3, 2) & " " & ox_str & " 46 "  '偶闭控制命令字符串

                                '关闭偶数的编号
                                sql = "update lamp_inf set div_time_id=0, state=3, result=4,current_l=0,presure_l=0,power=0,presure_end=0 where CAST(SUBSTRING(lamp_id, 7, " & LAMP_ID_LEN & ") AS int) % 2 =0 and control_box_id='" & box_id & "' and lamp_type_id='" & type_id & "'"
                                DBOperation.ExecuteSQL(conn, sql, msg)

                                '如果控制的是回路，则将回路中的路灯全部设置为关
                                If Mid(lamp_id_string, 5, 2) = "31" Then
                                    i = 1
                                    While i <= g_lampnum
                                        If i Mod 2 = 0 Then
                                            control_lamp_obj.open_close_huilulamp(0, i, box_id)

                                        End If
                                        i += 1

                                    End While
                                End If
                            Case Else
                                '不是传统的控制命令，则为三回路组合控制模式
                                order_type = 2
                                control_string &= Mid(box_id_ox, 1, 2) & " " & Mid(box_id_ox, 3, 2) & " " & ox_str   '三回路控制命令字符串
                                control_string = get_grouporder_string(box_id, control_string, mod_string)
                                control_string = Mid(control_string, 4, 17) & " 64 " & Mid(control_string, 1, 2)

                                If control_string <> "" Then
                                    GoTo huilu_next
                                End If
                        End Select



                    Else
                        '交流接触器
                        lamp_id = Trim(rs.Fields("lamp_id").Value)
                        '登录窗口.Property_welcome_win_obj.Property_hand_control_tag = 1
                        ox_str = Com_inf.Get_lampid_bin(Val(Mid(lamp_id, 5, 2)), Val(Mid(lamp_id, 7, LAMP_ID_LEN)))
                        ox_str = Com_inf.BIN_to_HEX(ox_str)
                        ox_str = Mid(ox_str, 1, 2) & " " & Mid(ox_str, 3, 2)
                        If mod_string = "回路开" Then
                            order_type = 1
                            control_string &= Mid(box_id_ox, 3, 2) & " " & ox_str & " 1B "  '单灯开控制命令字符串
                            sql = "update lamp_inf set div_time_id=1, state=4, result=4 , date='" & Now & "' where lamp_id='" & lamp_id & "'"
                            DBOperation.ExecuteSQL(conn, sql, msg)
                            '将回路下控制的灯全部打开
                            control_lamp_obj.open_close_huilulamp(1, Val(Mid(lamp_id, 7, 3)), Mid(lamp_id, 1, 4))

                            '模拟时钟.full_close(lv_string, Trim(rs_road.Fields("control_box_id").Value))
                        Else
                            If mod_string = "回路关" Then
                                order_type = 1
                                control_string &= Mid(box_id_ox, 3, 2) & " " & ox_str & " 1C "    '回路闭控制命令字符串

                                sql = "update lamp_inf set div_time_id=0, state=3, result=4 ,current_l=0,presure_l=0,power=0,presure_end=0,date='" & Now & "' where lamp_id='" & lamp_id & "'"
                                DBOperation.ExecuteSQL(conn, sql, msg)
                                '将回路下控制的灯全部关闭
                                control_lamp_obj.open_close_huilulamp(0, Val(Mid(lamp_id, 7, 3)), Mid(lamp_id, 1, 4))

                            Else
                                rs.MoveNext()
                                Continue While
                            End If

                        End If


                    End If

                    'rs_lamp.Close()
                    'rs_lamp = Nothing
                    If control_string <> "" Then
                        diangan_id = control_lamp_obj.Find_diangan_id(diangan)     '电感型路灯标志
                        control_string &= diangan_id
                        control_string &= " " & "64 " & Mid(box_id_ox, 1, 2)
huilu_next:

                        If get_samll_order(control_string) = False Then
                            g_sethuilutag = True '设置回路标志

                            control_lamp_obj.Input_db_control(control_string, box_id, "", order_type, -1)  '将控制命令发送到数据库中
                            condition = "时段类型"
                            '记录手控信息到数据库
                            '记录手控信息到数据库
                            If single_tag = False Then
                                condition = "时段类型"
                                sql = "insert into hand_control_record(control_content,content_name,control_method,diangan,power,control_time,user_name) values('" & condition & "','" & control_box_name & lamptype_string & "','" & mod_string & "','" & diangan & "','" & 100 & "','" & Now() & "','" & g_username & "')"
                            Else
                                condition = "时段节点"
                                lamp_id_tag = Val(Mid(lamp_id, 1, 4)).ToString & "-" & Val(Mid(lamp_id, 5, 2)).ToString & "-" & Val(Mid(lamp_id, 7, LAMP_ID_LEN)).ToString
                                sql = "insert into hand_control_record(control_content,content_name,control_method,diangan,power,control_time,user_name) values('" & condition & "','" & lamp_id_tag & "','" & mod_string & "','" & diangan & "','" & 100 & "','" & Now() & "','" & g_username & "')"

                            End If
                            DBOperation.ExecuteSQL(conn, sql, msg)
                        End If



                    End If
                    '关闭需要暗的灯
                    'Com_inf.Turn_off_lamp(box_string, type_string, "")

                    'If single_tag = True Then
                    '    If mod_string = "回路开" Then
                    '        '开一条回路，后将该电控箱下面所有的路灯都打开

                    '        control_lamp_obj.Input_control_inf("(0)路灯", "类型", box_string, "类型全开", 1, "全功率", "100", hand_type)


                    '    End If
                    'Else

                    '    '对于针对整体的回路控制，需要将路灯类型的数据都置为开的状态
                    '    If type_id = 31 Then

                    '        control_lamp_obj.Input_control_inf("(0)路灯", "类型", box_string, "类型全开", 1, "全功率", "100", hand_type)

                    '    End If
                    'End If
                    rs.MoveNext()
                End While

            End If

            rs_level.MoveNext()

        End While

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        If rs_level.State = 1 Then
            rs_level.Close()
            rs_level = Nothing
        End If


        conn.Close()
        conn = Nothing


    End Sub



    ''' <summary>
    '''   '根据各个街道设置的级别，进行时段控制
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub clock_time(ByVal single_tag As System.Boolean)

        Dim clock_string As String
        Dim control_lamp_obj As New control_lamp
        Dim rs_level As New ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim mod_string As String
        Dim lv_string As String
        Dim control_string As String  '命令字节
        Dim ox_str As String '四位的景观灯编号
        Dim lamp_id_string As String  '景观灯编号
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection

        'Dim i As Integer
        Dim box_id_ox As String
        Dim state_tag As Integer
        Dim communication_time As Integer  '判断通信是否正常的次数
        Dim diangan_id, diangan As String
        Dim condition As String '控制对象
        Dim lamp_id As String = ""
        Dim time As System.DateTime

        'Dim control_lamp_obj As New control_lamp
        Dim now_time As System.DateTime  '记录当前时间
        Dim i As Integer
        Dim lamp_id_tag As String = ""
        Dim order_type As Integer = 1  '命令的类型，1表示7字节，2表示8字节

        i = 0

        now_time = Format(Now, "HH:mm:ss")

        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If

        clock_string = Format(Now(), "yyyy-MM-dd HH:mm:ss").ToString   '当前时间
        ''刷新路灯状况
        control_string = ""
        Dim time_s1, time_s2 As String
        state_tag = 0
        msg = ""
        mod_string = ""
        lv_string = ""
        communication_time = 50  '最长重复发查询命令50次
        ' rs = New ADODB.Recordset
        time_s1 = (Format(Now().Hour, "00") & Format(Now().Minute, "00") & Format(Now().Second, "00"))  '当前时间


        sql = "select * from div_time"

        rs_level = DBOperation.SelectSQL(conn, sql, msg)
        If rs_level.RecordCount <= 0 Then  '如果没有时控的街道，则返回
            rs_level.Close()
            rs_level = Nothing
            conn.Close()
            conn = Nothing

            Exit Sub
        End If

        '时控操作时按每个街道
        While rs_level.EOF = False
            diangan = Trim(rs_level.Fields("diangan").Value)
            time_s2 = Trim(rs_level.Fields("hour_beg").Value) & ":" & Trim(rs_level.Fields("min_beg").Value) & ":" & Trim(rs_level.Fields("second_beg").Value)  '时段控制的边界时间

            time = Format(System.Convert.ToDateTime(time_s2), "HH:mm:ss")

            'If time.ToString = Now.ToString Or time.AddMinutes(2).ToString = Now.ToString Or time.AddMinutes(5).ToString = Now.ToString Or time.AddMinutes(10).ToString = Now.ToString Or time.AddMinutes(15).ToString = Now.ToString Then
            ' If time.ToString = Now.ToString Then
            If now_time.TimeOfDay.Hours = time.TimeOfDay.Hours And now_time.TimeOfDay.Minutes = time.TimeOfDay.Minutes And (now_time.TimeOfDay.Seconds < 50) Then


                If single_tag = False Then
                    '整体控制
                    sql = "select * from control_level_view where div_time_level='" & Trim(rs_level.Fields("div_level").Value) & "' and lamp_id is NULL and week_id='" & Now.DayOfWeek & "'"

                Else
                    sql = "select * from control_level_view where div_time_level='" & Trim(rs_level.Fields("div_level").Value) & "' and lamp_id is not NULL and week_id='" & Now.DayOfWeek & "'"

                End If
                rs = DBOperation.SelectSQL(conn, sql, msg)  '获取各个时段的详细信息


                mod_string = ""
                While rs.EOF = False

                    box_id = Trim(rs.Fields("control_box_id").Value) '电控箱编号
                    '2012年9月7日，对每个命令要判断其主控箱是否停运，如果停运则不执行
                    If Com_inf.get_controlbox_state(box_id) = False Then
                        rs.MoveNext()
                        Continue While
                    End If
                    '判断是否有节假日控制
                    If Get_box_holidaymod(box_id) = True Then
                        '有节假日控制，则不处理特殊控制模式
                        rs.MoveNext()
                        Continue While
                    End If

                    type_id = Trim(rs.Fields("type_id").Value) '景观灯编号
                    box_string = Trim(rs.Fields("control_box_name").Value)  '电控箱名称
                    type_string = Trim(rs.Fields("type_string").Value) '景观灯类型
                    level_string = Trim(rs.Fields("div_time_level").Value) '时段级别
                    mod_string = Trim(rs_level.Fields("mod").Value)  '控制模式
                    box_id_ox = Com_inf.Dec_to_Hex(Trim(rs.Fields("control_box_id").Value), 4) '电控箱编号转换
                    control_string = ""

                    If single_tag = False Then  '整体
                        lamp_id_string = box_id & type_id
                        While lamp_id_string.Length < 6 + LAMP_ID_LEN - 1
                            lamp_id_string &= "0"
                        End While
                        lamp_id_string &= "1"
                        ox_str = Com_inf.Get_lampid_bin(type_id, Mid(lamp_id_string, 7, LAMP_ID_LEN))
                        'lamp_id_string = box_id & type_id & "001"
                        'ox_str = Com_inf.Dec_to_Bin(Mid(lamp_id_string, 5, 2), 5) & Com_inf.Dec_to_Bin(Mid(lamp_id_string, 7, LAMP_ID_LEN), 11)
                        ox_str = Com_inf.BIN_to_HEX(ox_str)
                        ox_str = Mid(ox_str, 1, 2) & " " & Mid(ox_str, 3, 2)

                        Select Case mod_string
                            Case "类型全闭"
                                order_type = 1
                                control_string &= Mid(box_id_ox, 3, 2) & " " & ox_str & " 42 "  '全闭控制命令字符串
                                sql = "update lamp_inf set div_time_id=0, state=3, result=4 , date='" & Now & "' where control_box_id='" & box_id & "' and lamp_type_id='" & type_id & "'"
                                DBOperation.ExecuteSQL(conn, sql, msg)

                                '如果控制的是回路，则将回路中的路灯全部设置为关
                                If Mid(lamp_id_string, 5, 2) = "31" Then
                                    i = 1
                                    While i <= g_lampnum
                                        control_lamp_obj.open_close_huilulamp(0, i, box_id)
                                        i += 1

                                    End While
                                End If
                                '模拟时钟.full_close(lv_string, Trim(rs_road.Fields("control_box_id").Value))
                            Case "类型全开"
                                order_type = 1
                                control_string &= Mid(box_id_ox, 3, 2) & " " & ox_str & " 41 "    '全开控制命令字符串

                                sql = "update lamp_inf set div_time_id=1, state=4, result=4 , date='" & Now & "' where control_box_id='" & box_id & "' and lamp_type_id='" & type_id & "'"
                                DBOperation.ExecuteSQL(conn, sql, msg)


                                '如果控制的是回路，则将回路中的路灯全部设置为关
                                If Mid(lamp_id_string, 5, 2) = "31" Then
                                    i = 1
                                    While i <= g_lampnum
                                        control_lamp_obj.open_close_huilulamp(1, i, box_id)
                                        i += 1

                                    End While
                                End If

                            Case "类型奇开"
                                order_type = 1
                                control_string &= Mid(box_id_ox, 3, 2) & " " & ox_str & " 43 "  '奇开控制命令字符串

                                '打开奇数的编号
                                sql = "update lamp_inf set div_time_id=1, state=4, result=4 where CAST(SUBSTRING(lamp_id, 7, " & LAMP_ID_LEN & ") AS int) % 2 =1 and control_box_id='" & box_id & "' and lamp_type_id='" & type_id & "'"
                                DBOperation.ExecuteSQL(conn, sql, msg)
                                '关闭偶数的编号
                                sql = "update lamp_inf set div_time_id=0, state=3, result=4 where CAST(SUBSTRING(lamp_id, 7, " & LAMP_ID_LEN & ") AS int) % 2 =0 and control_box_id='" & box_id & "' and lamp_type_id='" & type_id & "'"
                                DBOperation.ExecuteSQL(conn, sql, msg)


                                '如果控制的是回路，则将回路中的路灯全部设置为关
                                If Mid(lamp_id_string, 5, 2) = "31" Then
                                    i = 1
                                    While i <= g_lampnum
                                        If i Mod 2 = 0 Then
                                            control_lamp_obj.open_close_huilulamp(0, i, box_id)
                                        Else
                                            control_lamp_obj.open_close_huilulamp(1, i, box_id)

                                        End If
                                        i += 1

                                    End While
                                End If
                            Case "类型奇闭"
                                order_type = 1
                                control_string &= Mid(box_id_ox, 3, 2) & " " & ox_str & " 44 " '奇闭控制命令字符串
                                '关闭奇数的编号
                                sql = "update lamp_inf set div_time_id=0, state=3, result=4 where CAST(SUBSTRING(lamp_id, 7, " & LAMP_ID_LEN & ") AS int) % 2 =1 and control_box_id='" & box_id & "' and lamp_type_id='" & type_id & "'"
                                DBOperation.ExecuteSQL(conn, sql, msg)

                                '如果控制的是回路，则将回路中的路灯全部设置为关
                                If Mid(lamp_id_string, 5, 2) = "31" Then
                                    i = 1
                                    While i <= g_lampnum
                                        If i Mod 2 = 1 Then
                                            control_lamp_obj.open_close_huilulamp(0, i, box_id)

                                        End If
                                        i += 1

                                    End While
                                End If
                            Case "类型偶开"
                                order_type = 1
                                control_string &= Mid(box_id_ox, 3, 2) & " " & ox_str & " 45 "  '偶开控制命令字符串
                                '关闭奇数的编号
                                sql = "update lamp_inf set div_time_id=0, state=3, result=4 where CAST(SUBSTRING(lamp_id, 7, " & LAMP_ID_LEN & ") AS int) % 2 =1 and control_box_id='" & box_id & "' and lamp_type_id='" & type_id & "'"
                                DBOperation.ExecuteSQL(conn, sql, msg)
                                '打开偶数的编号
                                sql = "update lamp_inf set div_time_id=1, state=4, result=4 where CAST(SUBSTRING(lamp_id, 7, " & LAMP_ID_LEN & ") AS int) % 2 =0 and control_box_id='" & box_id & "' and lamp_type_id='" & type_id & "'"
                                DBOperation.ExecuteSQL(conn, sql, msg)

                                '如果控制的是回路，则将回路中的路灯全部设置为关
                                If Mid(lamp_id_string, 5, 2) = "31" Then
                                    i = 1
                                    While i <= g_lampnum
                                        If i Mod 2 = 1 Then
                                            control_lamp_obj.open_close_huilulamp(0, i, box_id)
                                        Else
                                            control_lamp_obj.open_close_huilulamp(1, i, box_id)

                                        End If
                                        i += 1

                                    End While
                                End If

                            Case "类型偶闭"
                                order_type = 1
                                control_string &= Mid(box_id_ox, 3, 2) & " " & ox_str & " 46 "  '偶闭控制命令字符串

                                '关闭偶数的编号
                                sql = "update lamp_inf set div_time_id=0, state=3, result=4 where CAST(SUBSTRING(lamp_id, 7, " & LAMP_ID_LEN & ") AS int) % 2 =0 and control_box_id='" & box_id & "' and lamp_type_id='" & type_id & "'"
                                DBOperation.ExecuteSQL(conn, sql, msg)

                                '如果控制的是回路，则将回路中的路灯全部设置为关
                                If Mid(lamp_id_string, 5, 2) = "31" Then
                                    i = 1
                                    While i <= g_lampnum
                                        If i Mod 2 = 0 Then
                                            control_lamp_obj.open_close_huilulamp(0, i, box_id)

                                        End If
                                        i += 1

                                    End While
                                End If
                            Case Else
                                '不是传统的控制命令，则为三回路组合控制模式
                                order_type = 2
                                control_string &= Mid(box_id_ox, 1, 2) & " " & Mid(box_id_ox, 3, 2) & " " & ox_str   '三回路控制命令字符串
                                control_string = get_grouporder_string(box_id, control_string, mod_string)
                                control_string = Mid(control_string, 4, 17) & " 64 " & Mid(control_string, 1, 2)

                                If control_string <> "" Then
                                    GoTo huilu_next
                                End If
                        End Select


                    Else
                        '交流接触器
                        lamp_id = Trim(rs.Fields("lamp_id").Value)
                        ox_str = Com_inf.Get_lampid_bin(type_id, Mid(lamp_id, 7, LAMP_ID_LEN))

                        '登录窗口.Property_welcome_win_obj.Property_hand_control_tag = 1
                        'ox_str = Com_inf.Dec_to_Bin(Mid(lamp_id, 5, 2), 5) & Com_inf.Dec_to_Bin(Mid(lamp_id, 7, LAMP_ID_LEN), 11)
                        ox_str = Com_inf.BIN_to_HEX(ox_str)
                        ox_str = Mid(ox_str, 1, 2) & " " & Mid(ox_str, 3, 2)

                        If mod_string = "回路开" Then
                            order_type = 1
                            control_string &= Mid(box_id_ox, 3, 2) & " " & ox_str & " 1B "  '单灯开控制命令字符串
                            sql = "update lamp_inf set div_time_id=1, state=4, result=4 , date='" & Now & "' where lamp_id='" & lamp_id & "'"
                            DBOperation.ExecuteSQL(conn, sql, msg)
                            '将回路下控制的灯全部打开
                            control_lamp_obj.open_close_huilulamp(1, Val(Mid(lamp_id, 7, LAMP_ID_LEN)), Mid(lamp_id, 1, 4))

                            '模拟时钟.full_close(lv_string, Trim(rs_road.Fields("control_box_id").Value))
                        Else
                            If mod_string = "回路关" Then
                                order_type = 1
                                control_string &= Mid(box_id_ox, 3, 2) & " " & ox_str & " 1C "    '回路闭控制命令字符串

                                sql = "update lamp_inf set div_time_id=0, state=3, result=4 , date='" & Now & "' where lamp_id='" & lamp_id & "'"
                                DBOperation.ExecuteSQL(conn, sql, msg)
                                '将回路下控制的灯全部关闭
                                control_lamp_obj.open_close_huilulamp(0, Val(Mid(lamp_id, 7, LAMP_ID_LEN)), Mid(lamp_id, 1, 4))

                            Else
                                rs.MoveNext()
                                Continue While
                            End If

                        End If


                    End If

                    'rs_lamp.Close()
                    'rs_lamp = Nothing
                    If control_string <> "" Then
                        diangan_id = control_lamp_obj.Find_diangan_id(diangan)     '电感型路灯标志
                        control_string &= diangan_id
                        control_string &= " " & "64 " & Mid(box_id_ox, 1, 2)
huilu_next:
                        If Me.get_samll_order(control_string) = False Then
                            g_sethuilutag = True '设置回路标志

                            control_lamp_obj.Input_db_control(control_string, box_id, "", order_type, -1)  '将控制命令发送到数据库中

                            '记录手控信息到数据库
                            If single_tag = False Then
                                condition = "时段类型"
                                sql = "insert into hand_control_record(control_content,content_name,control_method,diangan,power,control_time,user_name) values('" & condition & "','" & box_string & type_string & "','" & mod_string & "','" & diangan & "','" & 100 & "','" & Now() & "','" & g_username & "')"
                            Else
                                condition = "时段节点"
                                lamp_id_tag = Val(Mid(lamp_id, 1, 4)).ToString & "-" & Val(Mid(lamp_id, 5, 2)).ToString & "-" & Val(Mid(lamp_id, 7, LAMP_ID_LEN)).ToString
                                sql = "insert into hand_control_record(control_content,content_name,control_method,diangan,power,control_time,user_name) values('" & condition & "','" & lamp_id_tag & "','" & mod_string & "','" & diangan & "','" & 100 & "','" & Now() & "','" & g_username & "')"

                            End If


                            'sql = "insert into hand_control_record(control_content,content_name,control_method,diangan,power,control_time) values('" & condition & "','" & box_string & type_string & "','" & mod_string & "','" & diangan & "', 100 ,'" & Now() & "')"
                            DBOperation.ExecuteSQL(conn, sql, msg)

                        End If
                      
                    End If

                    rs.MoveNext()
                End While

            End If

            rs_level.MoveNext()

        End While

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        If rs_level.State = 1 Then
            rs_level.Close()
            rs_level = Nothing
        End If


        conn.Close()
        conn = Nothing


    End Sub


    ''' <summary>
    ''' 通过三回路控制名称，组合成新的控制命令字符串，将lamp_inf表中的状态改变
    ''' </summary>
    ''' <param name="control_string">路段号和节点号组成的字符串</param>
    '''<param name="control_box_id" >电控箱编号</param>
    ''' <param name="mod_name">三回路模式名称</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function get_grouporder_string(ByVal control_box_id As String, ByVal control_string As String, ByVal mod_name As String) As String
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim method_string As String
        Dim control_lamp_obj As New control_lamp

        msg = ""
        sql = "select * from group_order where grouporder_name='" & mod_name & "'"
        If DBOperation.OpenConn(conn) = False Then
            get_grouporder_string = ""
            Exit Function
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "get_grouporder_string" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            get_grouporder_string = ""
            Exit Function
        End If

        If rs.RecordCount > 0 Then
            get_grouporder_string = control_string & " " & Trim(rs.Fields("grouporder").Value)
            method_string = Trim(rs.Fields("grouporder_string").Value)  '控制01字符串
            control_lamp_obj.Setgrouporder_lamp_state(control_box_id, method_string)
        Else
            get_grouporder_string = ""
        End If

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing

    End Function

#Region "时段控制面板初始化"
    Public Sub Div_time_show()

        time_div()  '主控界面中时段信息
        get_treelist_inf() '添加树形等级列表，显示每个时段下控制的对象

    End Sub
    ''' <summary>
    ''' 主控界面中时段划分的ABCD四个等级划分，原来是按照四个面板显示，后来改成树形列表形式
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub time_div()
        '2010年10月4日树形列表形式
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String

        Dim i As Integer = 0
        Dim level1_num As Integer  '第一层节点的个数
        Dim level2_num As Integer  '第一层节点的个数

        Dim level_content As String
        Dim node1, node2 As TreeNode
        msg = ""
      
        DBOperation.OpenConn(conn)
        i = 0
        level1_num = g_divname.Length   '平时控制模式的个数
        level2_num = g_specialdivname.Length  '特殊控制模式的个数
        g_welcomewinobj.Div_infList.Nodes.Clear()   '清除节点
        node1 = g_welcomewinobj.Div_infList.Nodes.Add("平时控制模式")  '增加平时控制模式的根节点
        node2 = g_welcomewinobj.Div_infList.Nodes.Add("特殊控制模式")  '增加特殊控制模式的跟节点
        While i < level1_num
            node1.Nodes.Add(g_divname(i))  '控制模式的名称（平时）
            sql = "SELECT * FROM div_time where div_level='" & g_divname(i) & "' order by id"
            rs = DBOperation.SelectSQL(conn, sql, msg)
            If rs Is Nothing Then
                MsgBox(MSG_ERROR_STRING & "time_div", , PROJECT_TITLE_STRING)
                conn.Close()
                conn = Nothing
                Exit Sub
            End If

            While rs.EOF = False
                level_content = Trim(rs.Fields("id").Value.ToString) & " " & Trim(rs.Fields("hour_beg").Value) & "时 " & Trim(rs.Fields("min_beg").Value) _
                & "分 " & Trim(rs.Fields("second_beg").Value) & "秒  " & Trim(rs.Fields("mod").Value)

                node1.Nodes(i).Nodes.Add(level_content)   '控制模式的内容（平时）

                rs.MoveNext()
            End While
            i += 1

        End While

        i = 0

        While i < level2_num
            node2.Nodes.Add(g_specialdivname(i))   '控制模式的名称（特殊）
            sql = "SELECT * FROM special_div_time where name='" & g_specialdivname(i) & "' order by id"
            rs = DBOperation.SelectSQL(conn, sql, msg)
            If rs Is Nothing Then
                MsgBox(MSG_ERROR_STRING & "time_div", , PROJECT_TITLE_STRING)
                conn.Close()
                conn = Nothing
                Exit Sub
            End If

            While rs.EOF = False
                level_content = rs.Fields("Time").Value & " " & Trim(rs.Fields("mod").Value)
                node2.Nodes(i).Nodes.Add(level_content)  '控制模式的内容（特殊）

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
    ''' 添加树形等级列表
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub get_treelist_inf()
        Dim rs, rs_lamp As New ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim conn As New ADODB.Connection
        Dim node1, node2 As TreeNode
        Dim node11() As TreeNode
        Dim node21() As TreeNode
        DBOperation.OpenConn(conn)


        g_welcomewinobj.level_street_list.Nodes.Clear()
        '树形列表的第一级标题
        Dim i As Integer = 0
        node1 = g_welcomewinobj.level_street_list.Nodes.Add("平时控制模式")
        node2 = g_welcomewinobj.level_street_list.Nodes.Add("特殊控制模式")
        ReDim node11(g_divname.Length)
        ReDim node21(g_specialdivname.Length)

        While i < g_divname.Length
            node11(i) = node1.Nodes.Add(g_divname(i))  '平时控制模式名称
            i += 1
        End While
        i = 0
        While i < g_specialdivname.Length
            node21(i) = node2.Nodes.Add(g_specialdivname(i))  '特殊控制模式名称
            i += 1
        End While

        msg = ""
        sql = "select * from control_level_view"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        i = 0
        While i < g_divname.Length
            node11(i).Nodes.Clear()
            sql = "select * from control_level_view where div_time_level='" & g_divname(i) & "'"
            rs = DBOperation.SelectSQL(conn, sql, msg)
            If rs Is Nothing Then
                MsgBox(MSG_ERROR_STRING & "get_treelist_inf", , PROJECT_TITLE_STRING)
                conn.Close()
                conn = Nothing
                Exit Sub
            End If

            While rs.EOF = False
                If rs.Fields("lamp_id").Value Is System.DBNull.Value Then  '控制的级别是电控箱下面的某一类型
                    node11(i).Nodes.Add(Trim(rs.Fields("control_box_name").Value) & " " & Trim(rs.Fields("type_string").Value))

                Else  '控制级别是回路的
                    node11(i).Nodes.Add(Trim(rs.Fields("control_box_name").Value) & " " & Trim(rs.Fields("type_string").Value) _
                     & Val(Mid(Trim(rs.Fields("lamp_id").Value), 7, 3)).ToString & " (" & Val(Mid(Trim(rs.Fields("lamp_id").Value), 1, 4)).ToString _
                     & "-" & Val(Mid(Trim(rs.Fields("lamp_id").Value), 5, 2)).ToString & "-" & Val(Mid(Trim(rs.Fields("lamp_id").Value), 7, 3)).ToString & ")")


                End If
                rs.MoveNext()


            End While
            i += 1
        End While


        '特殊的时段控制

        i = 0
        While i < g_specialdivname.Length
            node21(i).Nodes.Clear()
            sql = "select * from Special_road_level where div_time_level='" & g_specialdivname(i) & "'"
            rs = DBOperation.SelectSQL(conn, sql, msg)
            If rs Is Nothing Then
                MsgBox(MSG_ERROR_STRING & "get_treelist_inf", , PROJECT_TITLE_STRING)
                conn.Close()
                conn = Nothing
                Exit Sub
            End If

            While rs.EOF = False
                sql = "select control_box_name, type_string,lamp_id from lamp_street where control_box_id='" & Trim(rs.Fields("control_box_id").Value) & "' and type_id='" & Trim(rs.Fields("type_id").Value) & "'"
                rs_lamp = DBOperation.SelectSQL(conn, sql, msg)
                If rs_lamp.RecordCount > 0 Then
                    If rs.Fields("lamp_id").Value Is System.DBNull.Value Then '控制的级别是电控箱下面的某一类型
                        node21(i).Nodes.Add(Trim(rs_lamp.Fields("control_box_name").Value) & " " & Trim(rs_lamp.Fields("type_string").Value))

                    Else  '控制级别是回路的
                        node21(i).Nodes.Add(Trim(rs_lamp.Fields("control_box_name").Value) & " " & Trim(rs_lamp.Fields("type_string").Value) _
                         & Val(Mid(Trim(rs_lamp.Fields("lamp_id").Value), 7, 3)).ToString & " (" & Val(Mid(Trim(rs_lamp.Fields("lamp_id").Value), 1, 4)).ToString _
                         & "-" & Val(Mid(Trim(rs_lamp.Fields("lamp_id").Value), 5, 2)).ToString & "-" & Val(Mid(Trim(rs_lamp.Fields("lamp_id").Value), 7, 3)).ToString & ")")


                    End If
                End If

                rs.MoveNext()

            End While
            i += 1
        End While


        If rs_lamp.State = 1 Then
            rs_lamp.Close()
            rs_lamp = Nothing
        End If


        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If

        conn.Close()
        conn = Nothing
    End Sub

#End Region

    ''' <summary>
    '''   '对单灯进行时段控制，单灯的时段控制优先级最高
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub clock_time_lamp_id()

        Dim rs, rs_lamp As New ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim conn As New ADODB.Connection
        Dim state As String '灯的状态
        Dim time_start, time_end As String
        Dim lamp_id As String  '灯的编号
        Dim lamp_id_bin As String   '灯的16位二进制编码
        Dim ox_str, control_string, lamp_id_hex As String
        Dim control_lamp_obj As New control_lamp
        Dim time As String  '当前时间
        time = (Format(Now().Hour, "00") & Format(Now().Minute, "00") & Format(Now().Second, "00"))  '当前时间

        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        msg = ""
        sql = "select * from lamp_level"

        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "clock_time_lamp_id" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            Exit Sub
        End If

        While rs.EOF = False
            '对每盏要进行时段控制的单灯进行状态检查，如果时段控制与目前该灯的状态不一致，则继续
            '维持时段控制状态
            time_start = Mid(rs.Fields("time_start").Value, 1, 2) & Mid(rs.Fields("time_start").Value, 4, 2) & Mid(rs.Fields("time_start").Value, 7, 2) '单灯的开始时间
            time_end = Mid(rs.Fields("time_end").Value, 1, 2) & Mid(rs.Fields("time_end").Value, 4, 2) & Mid(rs.Fields("time_end").Value, 7, 2) '单灯的结束时间
            lamp_id = Trim(rs.Fields("lamp_id").Value)

            If time_start <= time And time_end >= time Then
                '目前时间在单灯的控制范围内
                state = Trim(rs.Fields("open_close").Value)  '目前单灯该处于的状态
                sql = "select state from lamp_inf where lamp_id='" & lamp_id & "'"

                rs_lamp = DBOperation.SelectSQL(conn, sql, msg)
                If rs_lamp Is Nothing Then
                    rs.Close()
                    rs = Nothing
                    conn.Close()
                    conn = Nothing
                    Exit Sub
                End If
                If rs_lamp.RecordCount > 0 Then
                    If state = "关" And (rs_lamp.Fields("state").Value = 1 Or rs_lamp.Fields("state").Value = 4) Then
                        '时段该关，但目前灯被打开，则必须关闭该灯
                        lamp_id_bin = Com_inf.Dec_to_Bin(Mid(lamp_id, 5, 2), 5) & Com_inf.Dec_to_Bin(Mid(lamp_id, 7, 3), 11) '十六位长度的路灯编号二进制
                        lamp_id_hex = Com_inf.BIN_to_HEX(lamp_id_bin)
                        '  ox_str = Com_inf.Dec_to_ox(Mid(lamp_id, 6, 4), 4)  '将路灯编号转变城4位16进制数

                        control_string = Mid(lamp_id_hex, 1, 2) & " " & Mid(lamp_id_hex, 3, 2) & " 1C 13 FF"  '控制命令字符串
                        If SYSTEM_VISION = 1 Then
                            ox_str = Com_inf.Dec_to_Hex(Mid(lamp_id, 1, 4), 2)  '将电控箱编号转变成2位16进制数
                            control_string = ox_str & " " & control_string
                        Else
                            ox_str = Com_inf.Dec_to_Hex(Mid(lamp_id, 1, 4), 4)  '将电控箱编号转变成2位16进制数
                            control_string = Mid(ox_str, 3, 2) & " " & control_string & " " & Mid(ox_str, 1, 2)
                        End If
                        control_lamp_obj.Input_db_control(control_string, Mid(lamp_id, 1, 4), "", 1, -1) '将命令加入到数据库中
                        sql = "update lamp_inf set state=3 where lamp_id='" & lamp_id & "'"
                        DBOperation.ExecuteSQL(conn, sql, msg)
                    End If

                    If state = "开" And (rs_lamp.Fields("state").Value = 0 Or rs_lamp.Fields("state").Value = 3) Then
                        '时段该开，但目前灯被关闭，则必须打开该灯
                        lamp_id_bin = Com_inf.Dec_to_Bin(Mid(lamp_id, 5, 2), 5) & Com_inf.Dec_to_Bin(Mid(lamp_id, 7, 3), 11) '十六位长度的路灯编号二进制

                        lamp_id_hex = Com_inf.BIN_to_HEX(lamp_id_bin)
                        '  ox_str = Com_inf.Dec_to_ox(Mid(lamp_id, 6, 4), 4)  '将路灯编号转变城4位16进制数

                        control_string = Mid(lamp_id_hex, 1, 2) & " " & Mid(lamp_id_hex, 3, 2) & " 1B 11 FF"  '控制命令字符串
                        If SYSTEM_VISION = 1 Then
                            ox_str = Com_inf.Dec_to_Hex(Mid(lamp_id, 1, 4), 2)  '将电控箱编号转变成2位16进制数
                            control_string = ox_str & " " & control_string
                        Else
                            ox_str = Com_inf.Dec_to_Hex(Mid(lamp_id, 1, 4), 4)  '将电控箱编号转变成2位16进制数
                            control_string = Mid(ox_str, 3, 2) & " " & control_string & " " & Mid(ox_str, 1, 2)
                        End If

                        control_lamp_obj.Input_db_control(control_string, Mid(lamp_id, 1, 4), "", 1, -1) '将命令加入到数据库中
                        sql = "update lamp_inf set state=4 where lamp_id='" & lamp_id & "'"
                        DBOperation.ExecuteSQL(conn, sql, msg)
                    End If

                End If
            End If
            rs.MoveNext()

        End While
        If rs_lamp.State = 1 Then
            rs_lamp.Close()
            rs_lamp = Nothing
        End If
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

  

    ''' <summary>
    ''' 根据日出日落时间来进行开关灯控制
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub suntime_divoperation()
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

        msg = ""
        ' sql = "select * from pianyi order by id "
        sql = "SELECT pianyi.lamp_id, pianyi.open_pianyi, pianyi.close_pianyi, pianyi.today_opentime, pianyi.today_closetime FROM lamp_inf INNER JOIN pianyi ON lamp_inf.lamp_id = pianyi.lamp_id"

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
            '2012年9月7日，对每个命令要判断其主控箱是否停运，如果停运则不执行
            If Com_inf.get_controlbox_state(Mid(lamp_id, 1, 4)) = False Then
                rs.MoveNext()
                Continue While
            End If
            opentime = System.Convert.ToDateTime(rs.Fields("today_opentime").Value)
            '精确到秒
            ' If opentime.TimeOfDay >= nowtime.TimeOfDay And opentime.TimeOfDay < nowtime.AddSeconds(3).TimeOfDay Then
            '2011年11月23日精确到分钟
            If opentime.TimeOfDay.Hours = nowtime.TimeOfDay.Hours And opentime.TimeOfDay.Minutes = nowtime.TimeOfDay.Minutes And (nowtime.TimeOfDay.Seconds < 50) Then



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
                sql = "update lamp_inf set div_time_id=1, state=4, result=4 where lamp_id='" & lamp_id & "'"
                DBOperation.ExecuteSQL(conn, sql, msg)

                condition = "时段节点"
                mod_string = "回路开"
                ' control_box_name = control_box_name & "主控箱节点"

            End If

            closetime = System.Convert.ToDateTime(rs.Fields("today_closetime").Value)
            ' If closetime.TimeOfDay >= nowtime.TimeOfDay And closetime.TimeOfDay <= nowtime.AddSeconds(3).TimeOfDay Then
            '2011年11月23日精确到分钟
            If closetime.TimeOfDay.Hours = nowtime.TimeOfDay.Hours And closetime.TimeOfDay.Minutes = nowtime.TimeOfDay.Minutes And (nowtime.TimeOfDay.Seconds < 50) Then


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
                sql = "update lamp_inf set div_time_id=0, state=3, result=4,current_l=0,presure_l=0,power=0 where lamp_id='" & lamp_id & "'"
                DBOperation.ExecuteSQL(conn, sql, msg)

                condition = "时段节点"
                mod_string = "回路关"
                'control_box_name = control_box_name & "主控箱节点"

            End If

            If order_string <> "" Then
                If get_samll_order(order_string) = False Then
                    g_sethuilutag = True '有当前的时段控制

                    control_lamp_obj.Input_db_control(order_string, Mid(lamp_id, 1, 4), "", 1, -1) '将命令加入到数据库中

                    lamp_id_tag = Val(Mid(lamp_id, 1, 4)).ToString & "-" & Val(Mid(lamp_id, 5, 2)).ToString & "-" & Val(Mid(lamp_id, 7, LAMP_ID_LEN)).ToString

                    '记录手控信息到数据库
                    sql = "insert into hand_control_record(control_content,content_name,control_method,diangan,power,control_time,user_name) values('" & condition & "','" & lamp_id_tag & "','" & mod_string & "','" & diangan & "','" & 100 & "','" & Now() & "','" & g_username & "')"

                    'sql = "insert into hand_control_record(control_content,content_name,control_method,diangan,power,control_time) values('" & condition & "','" & box_string & type_string & "','" & mod_string & "','" & diangan & "', 100 ,'" & Now() & "')"
                    DBOperation.ExecuteSQL(conn, sql, msg)
                End If
               
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


    Private Function get_samll_order(ByVal order_string As String) As Boolean
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        get_samll_order = False
        msg = ""
        '配置召测等待时间
        sql = "select * from RoadLightControl where Createtime > DateAdd(n,-1,'" & Now() & "') and Createtime<='" & Now & "' and ControlContent ='" & order_string & "'"

        DBOperation.OpenConn(conn)
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "get_samll_order", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            get_samll_order = False
            Exit Function
        End If
        If rs.RecordCount > 0 Then
            get_samll_order = True
        Else
            get_samll_order = False
        End If



        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing

    End Function
   
End Class
