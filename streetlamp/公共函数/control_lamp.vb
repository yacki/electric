
''' <summary>
''' 手工控制灯的相关操作函数
''' </summary>
''' <remarks></remarks>
Public Class control_lamp

    Private m_operationstring As String  '记录当前操作的位置
    Private m_dianzuad, m_currentad As Integer
    Private m_typeid As Integer  '景观灯编号
    Public m_lampwide, m_lampheight As Integer   '路灯的宽度，高度
    Public m_alarmlinetag As Integer  '进行故障灯的报警,故障灯大小闪烁变化的标志
    Public m_handcontrollist As New ArrayList  '记录一次手工控制的各条信息

   

    Sub New()  '构造函数
        m_lampwide = LAMP_WIDE '绘制路灯高度
        m_lampheight = LAMP_HEIGHT '绘制路灯宽度
        m_alarmlinetag = 0  '故障报警的标志

    End Sub

    Public Function Get_string() As String
        Get_string = m_operationstring   '获取当前操作字符串
        m_operationstring = ""
    End Function

    Public Function Get_dianzu_ad() As Integer
        Get_dianzu_ad = m_dianzuad
    End Function
    Public Function Get_current_ad() As Integer
        Get_current_ad = m_currentad
    End Function

    'Public Function Get_first_lamp() As String
    '    Get_first_lamp = lamp_hand_start

    'End Function

    ''' <summary>
    ''' 将控制命令写入数据库中的控制命令表
    ''' </summary>
    ''' <param name="control_string">控制命令</param>
    ''' <param name="order_descreption" >命令描述</param>
    ''' <param name="control_box_id" >主控箱编号</param>
    ''' <param name="order_type" >1表示7字节的旧协议，2表示8字节的三回路协议</param>
    ''' <remarks></remarks>
    Public Function Input_db_control(ByVal control_string As String, ByVal control_box_id As String, ByVal order_descreption As String, ByVal order_type As Integer, ByVal row_id As Integer) As Boolean
        '写入到控制数据库中
        Dim sql As String
        Dim msg As String
        Dim time As Date
        Dim rs As New ADODB.Recordset
        Dim flag As Integer = 1 '命令标志位
      '  Dim feed_back_tag As Integer '控制命令返回值0表示失败，1表示成功，2表示超时

        Dim conn As New ADODB.Connection
        If DBOperation.OpenConn(conn) = False Then
            Input_db_control = False
            Exit Function
        End If

        time = Format(Now(), "yyyy-MM-dd HH:mm:ss")
        msg = ""

        '2012年3月27日在每次发送命令之前先判断整点招测的线程是否在工作，如果工作则停止该线程
        If g_welcomewinobj.BackgroundWorker_getboxdata.IsBusy = True Then
            g_welcomewinobj.BackgroundWorker_getboxdata.CancelAsync()
        End If


        '先判断电控箱的类型，根据类型置标志位
        sql = "select control_box_type from control_box where control_box_id='" & control_box_id & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            ' g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Input_db_control" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)
            Input_db_control = False
            conn.Close()
            conn = Nothing
            Exit Function
        End If

        If rs.RecordCount > 0 Then
            If rs.Fields("control_box_type").Value Is System.DBNull.Value Then
                flag = 1
            Else
                flag = Val(Trim(rs.Fields("control_box_type").Value))
            End If
        Else
            flag = 1
        End If
        If flag = 1 Then
            sql = "insert into RoadLightControl (ControlContent, HandlerFlag, Information, Createtime) values('" & control_string.ToUpper & "','" & CONTROL_BOX_TYPE1_FLAG & "','','" & time & "')"
        Else
            sql = "insert into RoadLightControl (ControlContent, HandlerFlag, Information, Createtime) values('" & control_string.ToUpper & "','" & CONTROL_BOX_TYPE2_FLAG & "','','" & time & "')"

        End If
        DBOperation.ExecuteSQL(conn, sql, msg)

        '发送了控制命令后等待1秒钟后，2类型电控箱下的控制命令，等待状态回复
        'If flag = 1 Then
        '    '1类型的直接显示命令发送成功
        '    If order_descreption <> "" Then
        '        'System.Threading.Thread.Sleep(1000)

        '        'g_welcomewinobj.SetTextDelegate(order_descreption & "命令执行成功 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, True, g_welcomewinobj.rtb_info_list)
        '        Input_db_control = True
        '    End If

        'Else
        '    '2类型的根据监听的回复状态，判断命令是否执行成功
        '    If order_descreption <> "" Then
        '        'System.Threading.Thread.Sleep(1000)
        '        'feed_back_tag = Get_Control_FeedBack(control_string.ToUpper, time, order_type)
        '        'If feed_back_tag = 0 Then  '失败
        '        '    Input_db_control = False
        '        '    'g_welcomewinobj.SetTextDelegate(order_descreption & "命令执行失败 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)
        '        'Else
        '        '    If feed_back_tag = 1 Then  '成功
        '        '        Input_db_control = True
        '        '        'g_welcomewinobj.SetTextDelegate(order_descreption & "命令执行成功 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, True, g_welcomewinobj.rtb_info_list)

        '        '    Else  '超时
        '        '        Input_db_control = False
        '        '        'g_welcomewinobj.SetTextDelegate(order_descreption & "命令执行超时 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

        '        '    End If
        '        'End If


        '    End If
        'End If
        '2011年10月19日将发送手工命令由串行改为并行,先将命令保存到列表中
        If row_id >= 0 Then '表示是手工控制的命令
            Dim order As New handorder
            order.row_id = row_id
            order.order_string = control_string.ToUpper
            order.order_type = order_type
            m_handcontrollist.Add(order)

        End If

        '有控制命令，将主动召测命令等待标志时间置位
        g_autozhaoce = g_ycjgtime * 60  '召测等待时间
        g_zhaocetag = True

        ''控制命令发出后，如果有正在进行的召测命令，则不停止
        'If g_welcomewinobj.BackgroundWorker_getboxdata.IsBusy = True Then
        '    g_welcomewinobj.BackgroundWorker_getboxdata.CancelAsync()
        'End If

        '有控制命令后，将状态表中未处理的开关量和三遥数据置为已处理状态，
        '防止有控制之前的数据对故障判断造成干扰
        clearstatus(control_string)

        '有了控制命令之后，将目前RoadLightStatus中未处理的单灯命令置为1，防止误报警

        clearlampstate()

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing

    End Function

    Public Sub clearlampstate()
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String = ""

        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If

        sql = "update RoadLightStatus set HandlerFlag=1 where HandlerFlag=2"
        DBOperation.ExecuteSQL(conn, sql, msg)

        conn.Close()
        conn = Nothing
    End Sub
    
    ''' <summary>
    ''' 获取单灯控制命令的回复,0表示失败，1表示成功，2表示无返回
    ''' </summary>
    ''' <param name="createtime" >命令发送时间</param>
    ''' <param name="order_string" >控制命令</param>
    ''' <param name="order_type" >1表示7字节的旧协议，2表示8字节的新协议</param>
    ''' <remarks></remarks>
    Public Function Get_Control_FeedBack(ByVal order_string As String, ByVal createtime As DateTime, ByVal order_type As Integer) As Integer
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String
        Dim wait_time As Integer = CONTROL_WAIT_TIME
        Dim lamp_id_string As String = "" '节点的字符串


        msg = ""
        If DBOperation.OpenConn(conn) = False Then
            Exit Function
        End If
        sql = "select * from RoadLightStatus where createtime>'" & createtime & "' and PackType='" & HG_TYPE.HG_GET_CONTROL_OPEN_CLOSE & "' and HandlerFlag=3"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Get_Control_FeedBack" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)
            conn.Close()
            conn = Nothing
            Get_Control_FeedBack = 0
            Exit Function
        End If
        If rs.RecordCount > 0 Then
            While rs.EOF = False
                If order_type = 1 Then
                    lamp_id_string = Mid(order_string, 19, 2) & " " & Mid(order_string, 1, 8)
                Else
                    lamp_id_string = Mid(order_string, 22, 2) & " " & Mid(order_string, 1, 8)
                End If
                If lamp_id_string = Mid(Trim(rs.Fields("StatusContent").Value), 1, 11) Then
                    If Mid(Trim(rs.Fields("StatusContent").Value), 13, 2) = "00" Then
                        Get_Control_FeedBack = 0
                    Else
                        Get_Control_FeedBack = 1
                    End If
                    rs.Fields("HandlerFlag").Value = 1
                    rs.Update()
                    GoTo finish
                Else
                    Get_Control_FeedBack = 2
                End If
                rs.MoveNext()
            End While
        Else
            Get_Control_FeedBack = 2
        End If
finish:
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing


    End Function

    ''' <summary>
    ''' 将故障信息写入到数据库中的故障表
    ''' </summary>
    ''' <param name="inf_id">故障信息</param>
    ''' <param name="lamp_id_tag">灯的编号</param>
    ''' <remarks></remarks>
    Public Sub Input_problem(ByVal inf_id As Integer, ByVal lamp_id_tag As String)
        '输入故障信息进故障表  ,2010年3月27日，剔除无返回状态的故障信息，增加故障的结束时间,将原来记录故障点改为记录故障区间
        Dim sql As String
        Dim msg As String
        Dim time As Date
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If

        msg = ""
        time = Format(Now(), "yyyy-MM-dd HH:mm:ss")
        'sql = "select * from lamp_inf_record where lamp_id='" & lamp_id_tag & "' and server_state=" & 0 & " and state_inf='" & inf_id & "'"
        '2011年5月12日修改查询语句，将两种相互矛盾的故障同时出现的情况也排除在外
        sql = "select * from lamp_inf_record where lamp_id='" & lamp_id_tag & "' and server_state=" & 0 & " "

        rs = DBOperation.SelectSQL(conn, sql, msg)

        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Input_problem" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs.RecordCount > 0 Then
            If Trim(rs.Fields("state_inf").Value) = inf_id Then
                '表示该故障还未维修好
                rs.Fields("date_end").Value = time
                rs.Update()
            Else
                '表示原来的故障和现在出现的故障矛盾，将原来的故障取消掉
                rs.Fields("server_state").Value = 1
                rs.Fields("date_end").Value = time
                rs.Update()

                '增添新的故障信息
                sql = "insert into lamp_inf_record(lamp_id,state_inf,date,date_end,server_state) values('" & lamp_id_tag & "','" & inf_id & "','" & time & "','" & time & "'," & 0 & ")"
                DBOperation.ExecuteSQL(conn, sql, msg)
            End If



        Else
            '表示没有未维修的故障，则新添记录

            sql = "insert into lamp_inf_record(lamp_id,state_inf,date,date_end,server_state) values('" & lamp_id_tag & "','" & inf_id & "','" & time & "','" & time & "'," & 0 & ")"
            DBOperation.ExecuteSQL(conn, sql, msg)


        End If
        rs.Close()
        rs = Nothing
        conn.Close()
        conn = Nothing

    End Sub

    ''' <summary>
    ''' "输入控制方法，返回控制编号
    ''' </summary>
    ''' <param name="method_string">控制方法，如全开</param>
    ''' <returns>返回控制编号，如11</returns>
    ''' <remarks></remarks>
    Public Function Find_method_id(ByVal method_string As String) As String '返回控制方法编号 11全亮
        Dim rs_method As New ADODB.Recordset
        Dim sql As String
        Dim msg As String
        Dim conn As New ADODB.Connection
        If DBOperation.OpenConn(conn) = False Then
            Find_method_id = ""
            Exit Function
        End If

        msg = ""
        sql = "select * from control_method where control_inf='" & method_string & "'"   '查询控制方法名
        rs_method = DBOperation.SelectSQL(conn, sql, msg)
        If rs_method.RecordCount <= 0 Then
            MsgBox("不存在对应的控制编码，请确认控制命令是否被修改！", , PROJECT_TITLE_STRING)
            rs_method.Close()
            rs_method = Nothing
            Find_method_id = ""
            conn.Close()
            conn = Nothing
            Exit Function
        Else
            Find_method_id = Trim(rs_method.Fields("control_id").Value)  '控制方法编号
        End If
        rs_method.Close()
        rs_method = Nothing
        conn.Close()
        conn = Nothing
    End Function

    ''' <summary>
    ''' 输入电感型路灯的控制方法，返回控制编号
    ''' </summary>
    ''' <param name="diangan_string">电感控制方法，如全夜灯</param>
    ''' <returns>控制编号，如11</returns>
    ''' <remarks></remarks>
    Public Function Find_diangan_id(ByVal diangan_string As String) As String   '返回电感型路灯编号
        Dim rs As New ADODB.Recordset
        Dim sql As String
        Dim msg As String
        Dim conn As New ADODB.Connection
        If DBOperation.OpenConn(conn) = False Then
            Find_diangan_id = ""
            Exit Function
        End If

        msg = ""
        sql = "select * from inductance where control_inf='" & diangan_string & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)    '电感型路灯的控制方法

        If rs.RecordCount > 0 Then
            Find_diangan_id = Trim(rs.Fields("control_id").Value)
        Else
            Find_diangan_id = "11"   '无控制值,赋定值
        End If


        rs.Close()
        rs = Nothing
        conn.Close()
        conn = Nothing
    End Function

    ''' <summary>
    ''' 修改lamp_inf表中的路灯状态参数, 将lamp_inf 表中的状态修改为当前手工控制状态
    ''' </summary>
    ''' <param name="control_box_id">区域编号</param>
    ''' <param name="lamp_type_id">类型编号</param>
    ''' <param name="control_method">控制方法</param>
    ''' <param name="diangan">电感型</param>
    ''' <param name="power">功率</param>
    ''' <remarks></remarks>
    Public Sub Input_hand_control_table(ByVal control_box_id As String, ByVal lamp_type_id As Integer, ByVal control_method As String, ByVal diangan As String, ByVal power As String)

        Dim rs_lamp As New ADODB.Recordset
        Dim sql As String
        Dim msg As String
        Dim conn As New ADODB.Connection
        Dim sate_tag As Integer  '状态标志符号
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If

        msg = ""
        sate_tag = 0


        If lamp_type_id = -1 Then
            sql = "select * from lamp_inf where control_box_id='" & control_box_id & "' and lamp_type_id<>31"  '按电控箱级别查询
        Else
            ''回路控制则要控制下面的灯

            sql = "select * from lamp_inf where control_box_id='" & control_box_id & "' and lamp_type_id='" & Val(lamp_type_id) & "'"   '按类型级别查询
        End If

        rs_lamp = DBOperation.SelectSQL(conn, sql, msg)
        If rs_lamp Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Input_hand_control_table" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs_lamp.RecordCount > 0 Then
            '对于整体控制而言，如果控制的是回路，则将该电控箱下所有的灯的状态修改为开的状态
            If lamp_type_id = 31 Then
                sql = "update lamp_inf set state=4,result=4 where control_box_id='" & Trim(rs_lamp.Fields("control_box_id").Value) & "'"
                DBOperation.ExecuteSQL(conn, sql, msg)

            End If

        End If


        While rs_lamp.EOF = False

            If control_method = "全开" Then
                sate_tag = 4  '控制状态开
            End If
            If control_method = "全闭" Then
                sate_tag = 3   '控制状态关
            End If

            If control_method = "奇开" Then
                If Val(Mid(Trim(rs_lamp.Fields("lamp_id").Value), 7, LAMP_ID_LEN)) Mod 2 <> 0 Then
                    sate_tag = 4  '奇数的路灯设为控制状态开状态
                Else
                    sate_tag = 3  '偶数的路灯设为控制状态闭状态
                End If

            End If

            If control_method = "奇闭" Then
                If Val(Mid(Trim(rs_lamp.Fields("lamp_id").Value), 7, LAMP_ID_LEN)) Mod 2 <> 0 Then
                    sate_tag = 3  '奇数的路灯设为控制状态闭状态
                End If

            End If
            If control_method = "偶开" Then
                If Val(Mid(Trim(rs_lamp.Fields("lamp_id").Value), 7, LAMP_ID_LEN)) Mod 2 = 0 Then
                    sate_tag = 4  '偶数的路灯设为控制状态开状态
                Else
                    sate_tag = 3  '奇数的路灯设为控制状态闭状态
                End If

            End If

            If control_method = "偶闭" Then
                If Val(Mid(Trim(rs_lamp.Fields("lamp_id").Value), 7, LAMP_ID_LEN)) Mod 2 = 0 Then
                    sate_tag = 3   '偶数路灯设为控制状态闭状态
                End If

            End If

            If control_method = "类型全开" Then
                sate_tag = 4  '控制状态开

            End If
            If control_method = "类型全闭" Then
                sate_tag = 3   '控制状态关
                'If lamp_type_id = 31 Then
                '    sql = "update lamp_inf set state=3,result=4 where jiechuqi_id='" & Trim(rs_lamp.Fields("lamp_id").Value) & "'"
                '    DBOperation.ExecuteSQL(conn, sql, msg)

                'End If
            End If

            If control_method = "类型奇开" Then
                If Val(Mid(Trim(rs_lamp.Fields("lamp_id").Value), 7, LAMP_ID_LEN)) Mod 2 <> 0 Then
                    sate_tag = 4  '奇数的路灯设为控制状态开状态
                    'If lamp_type_id = 31 Then
                    '    sql = "update lamp_inf set state=4,result=4 where jiechuqi_id='" & Trim(rs_lamp.Fields("lamp_id").Value) & "'"
                    '    DBOperation.ExecuteSQL(conn, sql, msg)

                    'End If
                Else
                    sate_tag = 3  '偶数的路灯设为控制状态闭状态
                    'If lamp_type_id = 31 Then
                    '    sql = "update lamp_inf set state=3,result=4 where jiechuqi_id='" & Trim(rs_lamp.Fields("lamp_id").Value) & "'"
                    '    DBOperation.ExecuteSQL(conn, sql, msg)

                    'End If
                End If

            End If

            If control_method = "类型奇闭" Then
                If Val(Mid(Trim(rs_lamp.Fields("lamp_id").Value), 7, LAMP_ID_LEN)) Mod 2 <> 0 Then
                    sate_tag = 3  '奇数的路灯设为控制状态闭状态
                    'If lamp_type_id = 31 Then
                    '    sql = "update lamp_inf set state=3,result=4 where jiechuqi_id='" & Trim(rs_lamp.Fields("lamp_id").Value) & "'"
                    '    DBOperation.ExecuteSQL(conn, sql, msg)

                    'End If
                End If

            End If
            If control_method = "类型偶开" Then
                If Val(Mid(Trim(rs_lamp.Fields("lamp_id").Value), 7, LAMP_ID_LEN)) Mod 2 = 0 Then
                    sate_tag = 4  '偶数的路灯设为控制状态开状态
                    'If lamp_type_id = 31 Then
                    '    sql = "update lamp_inf set state=4,result=4 where jiechuqi_id='" & Trim(rs_lamp.Fields("lamp_id").Value) & "'"
                    '    DBOperation.ExecuteSQL(conn, sql, msg)

                    'End If
                Else
                    sate_tag = 3  '奇数的路灯设为控制状态闭状态
                    'If lamp_type_id = 31 Then
                    '    sql = "update lamp_inf set state=3,result=4 where jiechuqi_id='" & Trim(rs_lamp.Fields("lamp_id").Value) & "'"
                    '    DBOperation.ExecuteSQL(conn, sql, msg)

                    'End If
                End If

            End If

            If control_method = "类型偶闭" Then
                If Val(Mid(Trim(rs_lamp.Fields("lamp_id").Value), 7, LAMP_ID_LEN)) Mod 2 = 0 Then
                    sate_tag = 3   '偶数路灯设为控制状态闭状态
                    'If lamp_type_id = 31 Then
                    '    sql = "update lamp_inf set state=3,result=4 where jiechuqi_id='" & Trim(rs_lamp.Fields("lamp_id").Value) & "'"
                    '    DBOperation.ExecuteSQL(conn, sql, msg)

                    'End If
                End If

            End If

            If control_method = "1/3开" Then
                If (Val(Mid(Trim(rs_lamp.Fields("lamp_id").Value), 7, LAMP_ID_LEN)) - g_startid) Mod 3 = 0 Then

                    'If Trim(rs_lamp.Fields("lamp_type_id").Value) = 31 Then
                    '    sql = "update lamp_inf set state=4,result=4 where jiechuqi_id='" & Trim(rs_lamp.Fields("lamp_id").Value) & "'"
                    '    DBOperation.ExecuteSQL(conn, sql, msg)
                    '    GoTo next1
                    'End If
                    sate_tag = 4   '1/3处路灯设为控制状态开状态
                Else

                    'If Trim(rs_lamp.Fields("lamp_type_id").Value) = 31 Then
                    '    sql = "update lamp_inf set state=3,result=4 where jiechuqi_id='" & Trim(rs_lamp.Fields("lamp_id").Value) & "'"
                    '    DBOperation.ExecuteSQL(conn, sql, msg)
                    '    GoTo next1
                    'End If
                    sate_tag = 3   '非1/3处路灯设为控制状态闭状态
                End If

            End If

            'rs_lamp.Fields("date").Value = Now   '手控时间
            'rs_lamp.Fields("result").Value = 4 '标志等待返回信息状态

            'rs_lamp.Update()
            'rs_lamp.Resync(ADODB.AffectEnum.adAffectCurrent)

            '2011年11月15日，如果是关闭的话则将电流，电压功率值设为0
            If sate_tag = 3 Then
                sql = "update lamp_inf set state='" & sate_tag & "',current_l=0,presure_l=0,power=0, presure_end=0,div_time_id=0,date='" & Now() & "', result=4 where lamp_id='" & Trim(rs_lamp.Fields("lamp_id").Value) & "'"
            Else
                sql = "update lamp_inf set state='" & sate_tag & "', date='" & Now() & "', result=4,div_time_id=1 where lamp_id='" & Trim(rs_lamp.Fields("lamp_id").Value) & "'"

            End If

            DBOperation.ExecuteSQL(conn, sql, msg)
next1:
            rs_lamp.MoveNext()
        End While
        rs_lamp.Close()
        rs_lamp = Nothing
        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    ''' 获取景观灯的类型编号的五位二进制表示
    ''' </summary>
    ''' <param name="type_string">类型</param>
    ''' <returns>类型编号的五位二进制表示</returns>
    ''' <remarks></remarks>
    Private Function Get_lamp_type_id(ByVal type_string As String) As String  '景观灯的类型
        '求得景观灯的类型编号

        Dim rs As New ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim conn As New ADODB.Connection
        ' Dim type_id As String
        If DBOperation.OpenConn(conn) = False Then
            Get_lamp_type_id = ""
            Exit Function
        End If
        msg = ""
        sql = "select * from lamp_type where type_string='" & type_string & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Get_lamp_type_id" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            Get_lamp_type_id = ""
            conn.Close()
            conn = Nothing
            Exit Function
        End If
        If rs.RecordCount <= 0 Then
            MsgBox("没有该类型的景观灯，请重新选择", , PROJECT_TITLE_STRING)
            Get_lamp_type_id = ""

        Else
            m_typeid = rs.Fields("type_id").Value  '景观灯类型的编号
            '将十进制的景观灯编号转换成五位的二进制
            Get_lamp_type_id = Com_inf.Dec_to_Bin(m_typeid, 5)
        End If
        rs.Close()
        rs = Nothing
        conn.Close()
        conn = Nothing
    End Function


    ''' <summary>
    ''' 执行控制命令
    ''' </summary>
    ''' <param name="lamp_type">类型</param>
    ''' <param name="condition">控制条件：电控箱，类型，路灯</param>
    ''' <param name="condition_name">相应的区域名称</param>
    ''' <param name="control_method">控制方法：全开，全闭，奇开，奇闭，1/3开，1/3闭，1/4开，1/4闭</param>
    ''' <param name="open">0关，1开</param>
    ''' <param name="diangan">电感型路灯</param>
    ''' <param name="power">功率</param>
    ''' <param name="hand_type" >实际的控制范围</param>
    ''' <param name="row_id" >大于0是表示手工控制需要返回，-1表示不是手工控制不需要返回</param>
    ''' <remarks></remarks>
    Public Function Input_control_inf(ByVal lamp_type As String, ByVal condition As String, ByVal condition_name As String, ByVal control_method As String, ByVal open As Integer, ByVal diangan As String, ByVal power As String, ByVal hand_type As String, ByVal row_id As Integer) As Boolean

        Dim sql As String
        Dim msg As String
        Dim control_method_id As String
        Dim diangan_id As String

        Dim rs_first As New ADODB.Recordset
        Dim rs_type As New ADODB.Recordset

        Dim control_string As String  '控制字符串
        Dim conn As New ADODB.Connection
        Dim find_tag As Integer
        '获取路灯的编号包括类型
        Dim lamp_id_bin As String  '路灯编号的十六位二进制
        Dim lamp_id_hex As String  '路灯编号的十六进制四位
        Dim ox_str As String  '字符串
        Dim box_id_hex As String  '电控箱的16进制编号
        Dim order_descreption As String = "" '对控制命令的描述
        find_tag = 0
        If DBOperation.OpenConn(conn) = False Then
            Input_control_inf = False
            Exit Function
        End If


        msg = ""
        sql = ""
        control_method_id = ""
        control_string = ""
        diangan_id = ""
        If power = "" Then
            power = "0"
        End If

        '******************************按电控箱级别控制*********************
        If condition = "主控箱" Then

            sql = "select * from box_lamptype_view where control_box_name='" & condition_name & "'"
            rs_type = DBOperation.SelectSQL(conn, sql, msg)
            If rs_type Is Nothing Then
                g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Input_control_inf" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

                conn.Close()
                conn = Nothing
                Exit Function
            End If
            If rs_type.RecordCount <= 0 Then
                MsgBox("控制范围内无景观灯类型", , PROJECT_TITLE_STRING)
                rs_type.Close()
                rs_type = Nothing
                conn.Close()
                conn = Nothing
                Exit Function
            Else
                While rs_type.EOF = False
                    '将电控箱下的每个类型的景观灯的控制命令加入到数据库中
                    control_string = ""
                    order_descreption = condition_name & " " & control_method  '控制命令的描述
                    control_method_id = Find_method_id(control_method)  '查找控制方法编号
                    If control_method = "1/3开" Then
                        lamp_id_bin = Com_inf.Get_lampid_bin(rs_type.Fields("type_id").Value, g_startidvalue)
                        'lamp_id_bin = Com_inf.Dec_to_Bin(rs_type.Fields("type_id").Value, 5) & Com_inf.Dec_to_Bin(g_startidvalue, 11)
                    Else
                        'lamp_id_bin = Com_inf.Dec_to_Bin(rs_type.Fields("type_id").Value, 5) & "00000000001"
                        lamp_id_bin = Com_inf.Get_lampid_bin(rs_type.Fields("type_id").Value, 1)

                    End If
                    If lamp_id_bin = "" Then
                        '数据转换出错,退出程序
                        GoTo finish
                    End If
                    lamp_id_hex = Com_inf.BIN_to_HEX(lamp_id_bin)  '灯的二进制编码
                    If control_method_id = "" Then
                        MsgBox("不存在对应的控制编码，请确认控制命令是否被修改！", , PROJECT_TITLE_STRING)
                        conn.Close()
                        conn = Nothing
                        Exit Function
                    End If
                    diangan_id = Find_diangan_id(diangan)
                    '电感型路灯标志

                    If diangan_id = "" Then
                        MsgBox("没有电感控制方法", , PROJECT_TITLE_STRING)
                        conn.Close()
                        conn = Nothing
                        Exit Function
                    End If

                    '开关
                    If open = 1 Then
                        ox_str = Com_inf.Dec_to_Hex(Trim(power), 2)  '将功率转换为16进制

                    Else
                        ox_str = "00"   '关闭时功率为0
                    End If
                    '根据版本来判断电控箱是一个字节还是双字节的
                    If SYSTEM_VISION = 1 Then
                        box_id_hex = Com_inf.Dec_to_Hex(Trim(rs_type.Fields("control_box_id").Value), 2)  '将电控箱编号转换成两位的十六进制数
                        control_string = box_id_hex & " " & Mid(lamp_id_hex, 1, 2) & " " & Mid(lamp_id_hex, 3, 2) & " " & control_method_id & " " & diangan_id & " " & ox_str '控制命令中的两位电控箱号，四位路灯编号（起始为00 01），控制方法编号

                    Else
                        box_id_hex = Com_inf.Dec_to_Hex(Trim(rs_type.Fields("control_box_id").Value), 4)  '将电控箱编号转换成四位的十六进制数
                        control_string = Mid(box_id_hex, 3, 2) & " " & Mid(lamp_id_hex, 1, 2) & " " & Mid(lamp_id_hex, 3, 2) & " " & control_method_id & " " & diangan_id & " " & ox_str & " " & Mid(box_id_hex, 1, 2) '控制命令中的两位电控箱号，四位路灯编号（起始为00 01），控制方法编号

                    End If
                    Input_control_inf = Input_db_control(control_string, Trim(rs_type.Fields("control_box_id").Value), order_descreption, 1, row_id)   '将命令写入到控制命令数据库中
                    '控制命令提示栏

                    m_operationstring &= control_string & "  " & Now() & vbCrLf

                    ' 修改lamp_inf表中的手工控制参数
                    Input_hand_control_table(Trim(rs_type.Fields("control_box_id").Value), -1, control_method, diangan, power)

                    rs_type.MoveNext()
                End While  '电控箱下的每种类型的景观灯
                '  rs_control_box.MoveNext()
            End If
        End If

        If condition = "类型" Then
            '******************************按类型级别控制*********************
            control_string = ""
            Dim control_box_id As String
            sql = "select * from control_box where control_box_name='" & condition_name & "'"
            rs_type = DBOperation.SelectSQL(conn, sql, msg)
            If rs_type Is Nothing Then
                g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Input_control_inf" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

                conn.Close()
                conn = Nothing
                Exit Function
            End If
            If rs_type.RecordCount <= 0 Then
                MsgBox("控制范围内无景观灯类型", , PROJECT_TITLE_STRING)
                rs_type.Close()
                rs_type = Nothing
                conn.Close()
                conn = Nothing
                'rs_control_box.MoveNext()
                Exit Function
            Else
                control_box_id = Trim(rs_type.Fields("control_box_id").Value)
            End If
            order_descreption = condition_name & " " & lamp_type & " " & control_method  '控制命令的描述
            control_method_id = Find_method_id(control_method)  '查找控制方法编号
            If control_method_id = "" Then
                MsgBox("不存在对应的控制编码，请确认控制命令是否被修改！", , PROJECT_TITLE_STRING)
                conn.Close()
                conn = Nothing
                Exit Function
            End If
            '按景观灯的类型进行控制
            'lamp_id_bin = Get_lamp_type_id(lamp_type) & "00000000001"  'lamp_id的二进制，并且获取了lamp_type_id


            lamp_id_bin = Com_inf.Get_lampid_bin(Com_inf.Get_Type_id(lamp_type), 1)  'lamp_id的二进制，并且获取了lamp_type_id

            'lamp_id_bin = Mid(lamp_id_bin, 1, 5)
            lamp_id_hex = Com_inf.BIN_to_HEX(lamp_id_bin)

            diangan_id = Find_diangan_id(diangan)     '电感型路灯标志

            If diangan_id = "" Then
                MsgBox("没有电感控制方法", , PROJECT_TITLE_STRING)
                conn.Close()
                conn = Nothing
                Exit Function

            End If

            '开关
            If open = 1 Then
                ox_str = Com_inf.Dec_to_Hex(Trim(power), 2)  '将功率转换为16进制
            Else
                ox_str = "00"   '关闭时功率为0
            End If

            '根据版本来判断电控箱是一个字节还是双字节的
            If SYSTEM_VISION = 1 Then
                box_id_hex = Com_inf.Dec_to_Hex(Trim(rs_type.Fields("control_box_id").Value), 2)  '将电控箱编号转换成两位的十六进制数
                control_string = box_id_hex & " " & Mid(lamp_id_hex, 1, 2) & " " & Mid(lamp_id_hex, 3, 2) & " " & control_method_id & " " & diangan_id & " " & ox_str '控制命令中的两位电控箱号，四位路灯编号（起始为00 01），控制方法编号

            Else
                box_id_hex = Com_inf.Dec_to_Hex(Trim(rs_type.Fields("control_box_id").Value), 4)  '将电控箱编号转换成四位的十六进制数
                control_string = Mid(box_id_hex, 3, 2) & " " & Mid(lamp_id_hex, 1, 2) & " " & Mid(lamp_id_hex, 3, 2) & " " & control_method_id & " " & diangan_id & " " & ox_str & " " & Mid(box_id_hex, 1, 2) '控制命令中的两位电控箱号，四位路灯编号（起始为00 01），控制方法编号

            End If


            Input_control_inf = Input_db_control(control_string, Trim(rs_type.Fields("control_box_id").Value), order_descreption, 1, row_id)   '将命令写入到控制命令数据库中
            '控制命令提示栏

            m_operationstring &= control_string & "  " & Now() & vbCrLf
            m_typeid = Com_inf.Get_Type_id(lamp_type)
            ' 修改lamp_inf表中的手工控制参数
            Input_hand_control_table(control_box_id, m_typeid, control_method, diangan, power)  '-1表示按电控箱级别控制

        End If
finish:
        If rs_type.State = 1 Then
            rs_type.Close()
            rs_type = Nothing
        End If

        conn.Close()
        conn = Nothing

    End Function

    ''' <summary>
    ''' 单灯关闭
    ''' </summary>
    ''' <param name="control_box_id">区域编号</param>
    ''' <param name="lamp_id_bin">灯的编号的二进制</param>
    ''' <param name="lamp_id">灯的编号</param>
    ''' <param name="diangan">电感</param>
    ''' <param name="power">电功率</param>
    ''' <remarks></remarks>
    Public Function close_light_single(ByVal control_box_id As String, ByVal lamp_id_bin As String, ByVal lamp_id As String, ByVal diangan As String, ByVal power As String, ByVal control_time As Integer, ByVal row_id As Integer) As Boolean
        '单灯关闭
        Dim control_string As String
        Dim sql, msg As String
        Dim lamp_id_hex As String '景观灯编号的十六进制
        Dim method_string As String
        '  Dim rs As New ADODB.Recordset
        Dim rs_method As New ADODB.Recordset
        Dim rs_hand As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim box_id_hex As String '电控箱编号的十六进制
        Dim order_descreption As String = "" '命令的描述
        If DBOperation.OpenConn(conn) = False Then
            close_light_single = False
            Exit Function
        End If


        method_string = "单灯闭"
        msg = ""

        If control_time = 0 Then
            '记录手控信息到数据库

            ' sql = "select * from hand_control_record"
            Dim lamp_id_tag As String

            lamp_id_tag = Val(Mid(lamp_id, 1, 4)) & "-" & Val(Mid(lamp_id, 5, 2)) & "-" & Val(Mid(lamp_id, 7, LAMP_ID_LEN))
            'sql = "insert into hand_control_record(control_content,content_name,control_method,diangan,power,control_time,user_name) values('单灯','" & lamp_id_tag & "','" & method_string & "','" & diangan & "','" & power & "','" & Now() & "','" & g_username & "')"
            order_descreption = lamp_id_tag & " " & method_string

            If Mid(lamp_id, 5, 2) = "31" Then

                sql = "insert into hand_control_record(control_content,content_name,control_method,diangan,power,control_time,user_name) values('主控节点','" & lamp_id_tag & "','" & Com_inf.Change_Controlboxcontrol(method_string) & "','" & diangan & "','" & power & "','" & Now() & "','" & g_username & "')"
            Else
                sql = "insert into hand_control_record(control_content,content_name,control_method,diangan,power,control_time,user_name) values('单灯','" & lamp_id_tag & "','" & method_string & "','" & diangan & "','" & power & "','" & Now() & "','" & g_username & "')"

            End If
            DBOperation.ExecuteSQL(conn, sql, msg)

        End If




        lamp_id_hex = Com_inf.BIN_to_HEX(lamp_id_bin)

        control_string = Mid(lamp_id_hex, 1, 2) & " " & Mid(lamp_id_hex, 3, 2) & " "  '控制命令字符串


        sql = "select * from control_method where control_inf='" & method_string & "'"
        rs_method = DBOperation.SelectSQL(conn, sql, msg)  '数据库查询单灯闭的编号
        control_string &= Trim(rs_method.Fields("control_id").Value) & " "  '控制命令

        rs_method.Close()
        rs_method = Nothing

        control_string &= "13" & " 00"  '控制命令

        '将命令加入到数据库中
        If SYSTEM_VISION = 1 Then
            box_id_hex = Com_inf.Dec_to_Hex(control_box_id, 2)  '将电控箱编号转换成2位的16进制
            control_string = box_id_hex & " " & control_string  '控制命令
        Else
            box_id_hex = Com_inf.Dec_to_Hex(control_box_id, 4)
            control_string = Mid(box_id_hex, 3, 2) & " " & control_string & " " & Mid(box_id_hex, 1, 2)
        End If

        close_light_single = Input_db_control(control_string, control_box_id, order_descreption, 1, row_id)  '将控制命令写入到数据库中

        '控制命令提示栏
        m_operationstring &= control_string & "  " & Now() & vbCrLf

        '记录手工控制
        sql = "select * from lamp_inf where lamp_id='" & lamp_id & "'"

        rs_hand = DBOperation.SelectSQL(conn, sql, msg)

        If rs_hand.RecordCount > 0 Then  '修改手工控制参数
            If rs_hand.Fields("lamp_kind").Value = 0 Then  '电感型路灯
                If diangan = "全功率" Then  '全夜灯功率为100%
                    rs_hand.Fields("power").Value = 0
                Else
                    If diangan = "半功率" Then  '半夜灯功率为50%
                        rs_hand.Fields("power").Value = 0
                    Else  '关闭灯功率为0%
                        rs_hand.Fields("power").Value = 0
                    End If
                End If
            Else
                If power <> "" Then  '电子型路灯功率
                    rs_hand.Fields("power").Value = 0
                Else
                    rs_hand.Fields("power").Value = 0
                End If

            End If
            rs_hand.Fields("current_l").Value = 0
            rs_hand.Fields("presure_l").Value = 0
            rs_hand.Fields("presure_end").Value = 0
            rs_hand.Fields("div_time_id").Value = 0  '表示为关的状态
            rs_hand.Fields("state").Value = 3
            rs_hand.Fields("result").Value = 4
            rs_hand.Fields("date").Value = Now  '手控的时间
            rs_hand.Update()
        End If
        'If Mid(Trim(lamp_id), 5, 2) = 31 Then
        '    '控制的为回路
        '    sql = "update lamp_inf set state=3,result=4,date='" & Now & "' where jiechuqi_id='" & lamp_id & "'"
        '    DBOperation.ExecuteSQL(conn, sql, msg)
        'End If
        rs_hand.Close()
        rs_hand = Nothing
        conn.Close()
        conn = Nothing

    End Function

    ''' <summary>
    ''' 单灯开
    ''' </summary>
    ''' <param name="control_box_id">区域编号</param>
    ''' <param name="lamp_id_bin">灯的编号的二进制</param>
    ''' <param name="lamp_id">灯的编号</param>
    ''' <param name="diangan">电感</param>
    ''' <param name="power">电功率</param>
    ''' <remarks></remarks>
    Public Function open_light_single(ByVal control_box_id As String, ByVal lamp_id_bin As String, ByVal lamp_id As String, ByVal diangan As String, ByVal power As String, ByVal control_time As Integer, ByVal row_id As Integer) As Boolean
        '单灯开
        Dim control_string As String
        Dim msg As String
        Dim sql As String
        msg = ""
        Dim ox_str As String
        Dim lamp_id_hex As String
        Dim method_string As String
        Dim rs As New ADODB.Recordset
        Dim rs_method As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim rs_hand As New ADODB.Recordset
        Dim box_id_hex As String '电控箱编号的十六进制
        Dim order_descreption As String = "" '命令描述
        If DBOperation.OpenConn(conn) = False Then
            open_light_single = False
            Exit Function
        End If

        method_string = "单灯开"


        If control_time = 0 Then
            '记录手控信息到数据库
            Dim lamp_id_tag As String
            '修改交流接触器控制的称呼

            lamp_id_tag = Val(Mid(lamp_id, 1, 4)).ToString & "-" & Val(Mid(lamp_id, 5, 2)).ToString & "-" & Val(Mid(lamp_id, 7, LAMP_ID_LEN)).ToString
            order_descreption = lamp_id_tag & " " & method_string
            If Mid(lamp_id, 5, 2) = "31" Then


                sql = "insert into hand_control_record(control_content,content_name,control_method,diangan,power,control_time,user_name) values('主控节点','" & lamp_id_tag & "','" & Com_inf.Change_Controlboxcontrol(method_string) & "','" & diangan & "','" & power & "','" & Now() & "','" & g_username & "')"
            Else

                sql = "insert into hand_control_record(control_content,content_name,control_method,diangan,power,control_time,user_name) values('单灯','" & lamp_id_tag & "','" & method_string & "','" & diangan & "','" & power & "','" & Now() & "','" & g_username & "')"

            End If
            DBOperation.ExecuteSQL(conn, sql, msg)

        End If


        lamp_id_hex = Com_inf.BIN_to_HEX(lamp_id_bin)
        '  ox_str = Com_inf.Dec_to_ox(Mid(lamp_id, 6, 4), 4)  '将路灯编号转变城4位16进制数

        control_string = Mid(lamp_id_hex, 1, 2) & " " & Mid(lamp_id_hex, 3, 2) & " "  '控制命令字符串

        sql = "select * from control_method where control_inf='" & method_string & "'"
        rs_method = DBOperation.SelectSQL(conn, sql, msg)
        control_string &= Trim(rs_method.Fields("control_id").Value) & " "  '控制命令字符串
        '电感型

        sql = "select * from inductance where control_inf='" & diangan & "'"
        rs_method = DBOperation.SelectSQL(conn, sql, msg)
        If rs_method.RecordCount > 0 Then
            control_string &= Trim(rs_method.Fields("control_id").Value) & " "  '控制命令字符串
        Else
            MsgBox("没有电感型路灯的控制方法，请重新选择！", , PROJECT_TITLE_STRING)
            rs_method.Close()
            rs_method = Nothing
            conn.Close()
            conn = Nothing
            open_light_single = False
            'diangan_dan.Focus()
            Exit Function
        End If

        rs_method.Close()
        rs_method = Nothing

        ox_str = Com_inf.Dec_to_Hex(power, 2)  '将功率转换为16进制
        control_string &= ox_str  '控制命令字符串

        If SYSTEM_VISION = 1 Then

            box_id_hex = Com_inf.Dec_to_Hex(control_box_id, 2)  '将电控箱编号转变成2位16进制数
            control_string = box_id_hex & " " & control_string
        Else
            box_id_hex = Com_inf.Dec_to_Hex(control_box_id, 4)
            control_string = Mid(box_id_hex, 3, 2) & " " & control_string & " " & Mid(box_id_hex, 1, 2)
        End If
        open_light_single = Input_db_control(control_string, control_box_id, order_descreption, 1, row_id) '将命令加入到数据库中
        m_operationstring &= control_string & "  " & Now() & vbCrLf '控制命令提示栏

        '记录手工控制的开始时间
        sql = "select * from lamp_inf where lamp_id='" & lamp_id & "'"
        rs_hand = DBOperation.SelectSQL(conn, sql, msg)

        If rs_hand.RecordCount > 0 Then  '修改手工控制参数
            ' rs_hand.Fields("state").Value = 1
            If rs_hand.Fields("lamp_kind").Value = 0 Then   '如果是电感型路灯
                If diangan = "全功率" Then  '全夜灯，功率100%
                    rs_hand.Fields("power").Value = 0
                Else
                    If diangan = "半功率" Then  '半夜灯，功率50%
                        rs_hand.Fields("power").Value = 0
                    Else  '关闭灯，功率0%
                        rs_hand.Fields("power").Value = 0
                    End If
                End If
            Else  '电子型路灯
                If power <> "" Then
                    rs_hand.Fields("power").Value = 0
                Else
                    rs_hand.Fields("power").Value = 0
                End If

            End If
            rs_hand.Fields("state").Value = 4
            rs_hand.Fields("result").Value = 4
            rs_hand.Fields("div_time_id").Value = 1  '表示为开的状态
            rs_hand.Fields("date").Value = Now  '手控的时间
            rs_hand.Update()
        End If

        'If Mid(Trim(lamp_id), 5, 2) = 31 Then
        '    '控制的为回路，开一条回路，则发送整个电控箱的路灯类型的全开命令，这样，接触器断开的回路不受影响，影响的只是有电的回路上，之前设置的开灯关灯的操作
        '    sql = "update lamp_inf set state=4,result=4,date='" & Now & "' where jiechuqi_id='" & lamp_id & "'"
        '    DBOperation.ExecuteSQL(conn, sql, msg)
        'End If
        rs_hand.Close()
        rs_hand = Nothing
        conn.Close()
        conn = Nothing

    End Function

    '''' <summary>
    '''' 根据故障编号，查找故障状态
    '''' </summary>
    '''' <param name="problem_id">故障编号</param>
    '''' <returns>故障状态</returns>
    '''' <remarks></remarks>
    'Public Function Find_problem_inf(ByVal problem_id As Integer) As String
    '    '根据故障编号，查找故障状态  
    '    Dim rs As New ADODB.Recordset
    '    Dim msg As String
    '    Dim sql As String
    '    Dim conn As New ADODB.Connection
    '    DBOperation.OpenConn(conn)


    '    msg = ""
    '    sql = "select * from problem_list where problem_id='" & problem_id & "'"

    '    rs = DBOperation.SelectSQL(conn, sql, msg)
    '    If rs Is Nothing Then
    '        MsgBox(MSG_ERROR_STRING & "Find_problem_inf", , PROJECT_TITLE_STRING)
    '        Find_problem_inf = ""
    '        Exit Function
    '    Else
    '        If rs.RecordCount > 0 Then
    '            Find_problem_inf = Trim(rs.Fields("problem_inf").Value)  '根据状态编号，查找状态信息
    '        Else
    '            Find_problem_inf = "无状态信息"

    '        End If
    '        rs.Close()
    '        rs = Nothing
    '    End If
    '    conn.Close()
    '    conn = Nothing
    'End Function

    ''' <summary>
    ''' 读取控制条件和控制值
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Get_condition()
        Try
            Dim rs As New ADODB.Recordset
            Dim conn As New ADODB.Connection
            Dim msg As String
            Dim sql As String

            msg = ""
            If DBOperation.OpenConn(conn) = False Then
                Exit Sub
            End If
            sql = "select * from sysconfig where type='半功率控制'"

            rs = DBOperation.SelectSQL(conn, sql, msg)
            If rs Is Nothing Then
                g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Get_condition" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

                conn.Close()
                conn = Nothing
                Exit Sub
            End If
            '获取半功率的控制数值
            If rs.RecordCount > 0 Then
                g_controlvaluepart = Val(Trim(rs.Fields("name").Value))
            Else
                g_controlvaluepart = 0
            End If


            sql = "select * from sysconfig where type='全功率控制'"

            rs = DBOperation.SelectSQL(conn, sql, msg)
            If rs Is Nothing Then
                g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Get_condition" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

                conn.Close()
                conn = Nothing
                Exit Sub
            End If
            '获取全功率的控制数值
            If rs.RecordCount > 0 Then
                g_controlvalueall = Val(Trim(rs.Fields("name").Value))
            Else
                g_controlvalueall = 0
            End If


            sql = "select * from sysconfig where type='灯头个数'"

            rs = DBOperation.SelectSQL(conn, sql, msg)
            If rs Is Nothing Then
                g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Get_condition" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

                conn.Close()
                conn = Nothing
                Exit Sub
            End If
            '获取灯杆对应的灯头的个数
            If rs.RecordCount > 0 Then
                g_lampnum = Val(Trim(rs.Fields("name").Value))
            Else
                g_lampnum = 1
            End If


        Catch ex As Exception
            MsgBox(ex.Message, , PROJECT_TITLE_STRING)
        End Try

    End Sub

    ''' <summary>
    ''' 对五字节状态进行分析
    ''' </summary>
    ''' <param name="state_string">状态表中的状态字符串</param>
    ''' <param name="state_id">状态表中的id</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCompareState_AD6(ByVal state_string As String, ByVal state_id As Integer) As String
        Dim rs_lamp As New ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim conn As New ADODB.Connection
        Dim pro_num As Integer   '统计故障次数
        Dim controlstate As Integer '
        Dim state_inf As String '分析的状态
        Dim orig_state, orig_result As Integer '原来的状态
        If DBOperation.OpenConn(conn) = False Then
            GetCompareState_AD6 = ""
            Exit Function
        End If
        msg = ""
        state_inf = ""
        GetCompareState_AD6 = "E"
        '有返回的路灯状态数据,将 其与路灯状态表中的状态对比，A,暗；B,亮；C，该亮非亮；D该暗非暗
        sql = "select state,result,total_num,control_box_id,lamp_id,div_time_id from lamp_inf where lamp_id='" & g_lampidstring & "'"
        rs_lamp = DBOperation.SelectSQL(conn, sql, msg)
        If rs_lamp.RecordCount > 0 Then
            orig_state = rs_lamp.Fields("state").Value
            orig_result = rs_lamp.Fields("result").Value
            controlstate = rs_lamp.Fields("div_time_id").Value
            '状态只有0与8是正常的，其他为故障类型
            If g_information = 0 Then  '表示暗的
                'If controlstate = 1 Then
                '    '控制状态为亮，作为故障
                '    GetCompareState_AD6 = "C"  '表示故障
                '    g_information = 21
                'Else
                '    GetCompareState_AD6 = "A"
                'End If
                GetCompareState_AD6 = "A"
            Else
                If g_information = 8 Then
                    'If controlstate = 0 Then  '表示故障
                    '    GetCompareState_AD6 = "C"  '表示故障
                    '    g_information = 20
                    'Else
                    '    GetCompareState_AD6 = "B"  '表示亮
                    'End If
                    GetCompareState_AD6 = "B"  '表示亮
                Else
                    If g_information = 2 Then
                        GetCompareState_AD6 = "F"
                    Else
                        GetCompareState_AD6 = "C"  '表示故障
                    End If

                End If
            End If
            '更改路灯的实时状态时间
            'sql = "update lamp_inf set date='" & Now & "' where lamp_id='" & g_lampidstring & "'"
            'DBOperation.ExecuteSQL(conn, sql, msg)
            '如果获取的状态时正常的，并且之前的状态为故障，则表示故障维修完毕，标志lamp_inf表中的total_num位为0
            '      If (GetCompareState_AD2 = "A" Or GetCompareState_AD2 = "B_part" Or GetCompareState_AD2 = "B_all") And (rs_lamp.Fields("result").Value = 1 Or rs_lamp.Fields("result").Value = 2) Then
            If (GetCompareState_AD6 = "A" Or GetCompareState_AD6 = "B") And (rs_lamp.Fields("result").Value <> 0) Then

                '路灯的实时状态表
                sql = "update lamp_inf set total_num=0 where lamp_id='" & g_lampidstring & "'"
                DBOperation.ExecuteSQL(conn, sql, msg)
                '路灯的故障表
                sql = "update lamp_inf_record set server_state=1, date_end='" & Now & "' where lamp_id='" & g_lampidstring & "' and server_state=0"
                DBOperation.ExecuteSQL(conn, sql, msg)
            End If
next1:

            sql = "update RoadLightStatus set HandlerFlag=1 where ID='" & state_id & "'"
            DBOperation.ExecuteSQL(conn, sql, msg)
            Com_inf.lamp_id_to_detail(g_lampidstring)   '通过灯的编号获取电控箱名称，类型和灯的独立编号
            ' Com_inf.Get_current_and_presure_AD2(g_currentad)  '将电流和电压的AD值转换成电流电压实际值
            Select Case GetCompareState_AD6
                Case "A"   '关闭状态
                    'current = 0.0
                    If g_information <> orig_state Then
                        sql = "update lamp_inf set div_time_id=0, result=" & 0 & " , presure_l=" & g_presurevalue & " , current_l=" & g_currentvalue & ", state=" & g_information & " ,power=" & g_powervalue & ", presure_end=" & g_yinshuvalue & ", total_num=0,date='" & Now & "' where lamp_id='" & g_lampidstring & "'"
                    Else
                        sql = "update lamp_inf set div_time_id=0, result=" & 0 & " , presure_l=" & g_presurevalue & " , current_l=" & g_currentvalue & ", state=" & g_information & " ,power=" & g_powervalue & ", presure_end=" & g_yinshuvalue & ", total_num=0 where lamp_id='" & g_lampidstring & "'"

                    End If
                    'sql = "update lamp_inf set result=" & 0 & " , presure_l=" & g_presurevalue & " , current_l=" & g_currentvalue & ", state=" & g_information & " ,power=" & g_powervalue & ", presure_end=" & g_yinshuvalue & ", total_num=0 where lamp_id='" & g_lampidstring & "'"
                    DBOperation.ExecuteSQL(conn, sql, msg)
                    state_inf = get_probleminf(g_information)
                    Add_state_record(g_lampidstring, state_inf, g_presurevalue, g_currentvalue, g_powervalue, g_yinshuvalue)
                Case "B"  '打开
                    If g_information <> orig_state Then
                        sql = "update lamp_inf set div_time_id=1, result=" & 0 & " , presure_l=" & g_presurevalue & " , current_l=" & g_currentvalue & ", state=" & g_information & " ,power=" & g_powervalue & ", presure_end=" & g_yinshuvalue & ", total_num=0,date='" & Now & "'  where lamp_id='" & g_lampidstring & "'"
                    Else
                        sql = "update lamp_inf set div_time_id=1, result=" & 0 & " , presure_l=" & g_presurevalue & " , current_l=" & g_currentvalue & ", state=" & g_information & " ,power=" & g_powervalue & ", presure_end=" & g_yinshuvalue & ", total_num=0  where lamp_id='" & g_lampidstring & "'"


                    End If
                    ' sql = "update lamp_inf set result=" & 0 & " , presure_l=" & g_presurevalue & " , current_l=" & g_currentvalue & ", state=" & g_information & " ,power=" & g_powervalue & ", presure_end=" & g_yinshuvalue & ", total_num=0 where lamp_id='" & g_lampidstring & "'"
                    DBOperation.ExecuteSQL(conn, sql, msg)
                    state_inf = get_probleminf(g_information)
                    Add_state_record(g_lampidstring, state_inf, g_presurevalue, g_currentvalue, g_powervalue, g_yinshuvalue)
                Case "C"
                    If rs_lamp.Fields("total_num").Value = 2 Then '表示累计2次故障则发过短信
                        '发送短信
                        Send_Msg(rs_lamp.Fields("control_box_id").Value, rs_lamp.Fields("lamp_id").Value, LAMP_STATE_PROBLEM_OFF)
                    End If
                    pro_num = rs_lamp.Fields("total_num").Value + 1
                    If pro_num > 10000 Then
                        pro_num = 20
                    End If
                    '故障，区分不同的故障类型
                    If g_information <> orig_state Then
                        If orig_state = 22 Then
                            sql = "update lamp_inf set result=" & 1 & " , presure_l=" & g_presurevalue & " , current_l=" & g_currentvalue & ", state=" & g_information & " ,power=" & g_powervalue & ", presure_end=" & g_yinshuvalue & ",  total_num='" & pro_num & "'  where lamp_id='" & g_lampidstring & "'"
                        Else
                            sql = "update lamp_inf set result=" & 1 & " , presure_l=" & g_presurevalue & " , current_l=" & g_currentvalue & ", state=" & g_information & " ,power=" & g_powervalue & ", presure_end=" & g_yinshuvalue & ",  total_num='" & pro_num & "' ,date='" & Now & "' where lamp_id='" & g_lampidstring & "'"

                        End If
                    Else
                        sql = "update lamp_inf set result=" & 1 & " , presure_l=" & g_presurevalue & " , current_l=" & g_currentvalue & ", state=" & g_information & " ,power=" & g_powervalue & ", presure_end=" & g_yinshuvalue & ",  total_num='" & pro_num & "'  where lamp_id='" & g_lampidstring & "'"

                    End If
                    'If g_information <= 8 Then
                    '    sql = "update lamp_inf set result=" & 1 & " , presure_l=" & g_presurevalue & " , current_l=" & g_currentvalue & ", state=" & g_information & " ,power=" & g_powervalue & ", presure_end=" & g_yinshuvalue & ",  total_num='" & pro_num & "' where lamp_id='" & g_lampidstring & "'"
                    'Else
                    '    sql = "update lamp_inf set result=" & 1 & " , presure_l=" & g_presurevalue & " , current_l=" & g_currentvalue & ", state=" & g_information & " ,power=" & g_powervalue & ", presure_end=" & g_yinshuvalue & ",  total_num='" & pro_num & "' where lamp_id='" & g_lampidstring & "'"
                    'End If
                    DBOperation.ExecuteSQL(conn, sql, msg)
                    state_inf = get_probleminf(g_information)
                    Add_state_record(g_lampidstring, state_inf, g_presurevalue, g_currentvalue, g_powervalue, g_yinshuvalue)
                Case "F"
                    '无信息返回
                    If g_information <> orig_state Then
                        If orig_state = 22 Then
                            sql = "update lamp_inf set result=3, presure_l=0, current_l=0, power=0, presure_end=0, state=" & g_information & ", total_num=0 where lamp_id='" & g_lampidstring & "'"
                        Else
                            sql = "update lamp_inf set result=3, presure_l=0, current_l=0, power=0, presure_end=0, state=" & g_information & ", total_num=0,date='" & Now & "' where lamp_id='" & g_lampidstring & "'"

                        End If
                    Else
                        sql = "update lamp_inf set result=3, presure_l=0, current_l=0, power=0, presure_end=0, state=" & g_information & ", total_num=0 where lamp_id='" & g_lampidstring & "'"

                    End If
                    ' sql = "update lamp_inf set result=3, presure_l=0, current_l=0, power=0, presure_end=0, state=" & g_information & ", total_num=0 where lamp_id='" & g_lampidstring & "'"
                    DBOperation.ExecuteSQL(conn, sql, msg)
                    state_inf = get_probleminf(g_information)
                    Add_state_record(g_lampidstring, state_inf, 0, 0, 0, 0)
            End Select
            'Add_state_record(g_lampidstring, state_inf, g_presurevalue, g_currentvalue)
        End If
        If rs_lamp.State = 1 Then
            rs_lamp.Close()
            rs_lamp = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Function


    ''' <summary>
    ''' 根据故障类型，给出故障描述
    ''' </summary>
    ''' <param name="problem"></param>
    ''' <remarks></remarks>
    Public Function get_probleminf(ByVal problem As String) As String
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String
        Dim problemstring As String = "lamp_" & problem.ToString
        get_probleminf = ""
        msg = ""
        If DBOperation.OpenConn(conn) = False Then
            Exit Function
        End If
        sql = "select * from sysconfig where name='" & problemstring & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            conn.Close()
            conn = Nothing
            Exit Function
        End If
        If rs.RecordCount > 0 Then
            get_probleminf = rs.Fields("type").Value
        End If
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Function

    ''' <summary>
    ''' 获取单灯状态与数据库中的操作状态对比，得到路灯的实际状态，是否故障,2011年5月19日，增加电流AD为2个字节
    ''' </summary>
    ''' <param name="state_string">状态表中的状态字符串</param>
    ''' <param name="state_id">状态表中的id</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCompareState_AD2(ByVal state_string As String, ByVal state_id As Integer) As String
        Dim rs_lamp As New ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim control_condition_ad As Integer  '控制条件：电阻or电流
        Dim conn As New ADODB.Connection
        Dim state_inf As String '分析的状态
        Dim pro_num As Integer   '统计故障次数
        If DBOperation.OpenConn(conn) = False Then
            GetCompareState_AD2 = ""
            Exit Function
        End If
        Dim orig_state, orig_result As Integer '原来的状态
        msg = ""
        state_inf = ""
       
        '有返回的路灯状态数据,将 其与路灯状态表中的状态对比，A,暗；B,亮；C，该亮非亮；D该暗非暗
        sql = "select state,result,total_num,control_box_id,lamp_id from lamp_inf where lamp_id='" & g_lampidstring & "'"
        rs_lamp = DBOperation.SelectSQL(conn, sql, msg)
        GetCompareState_AD2 = "E"  '标志状态默认值
        If rs_lamp.RecordCount > 0 Then
            'Get_condition()   '获取设置的全功率半功率的AD阈值
            '以电流为判断标志
            If Mid(state_string, 10, 5) = "FF FF" Then  '无返回值
                GetCompareState_AD2 = "F"
                ' orig_state = rs_lamp.Fields("state").Value
                ' orig_result = 3
                m_dianzuad = 0  '电阻AD值
                m_currentad = 0   '电流AD值
                GoTo next1
            End If
            control_condition_ad = g_dianzuad   '判断开关的标志2011年5月19日改为电流的高8位，即原来的电阻AD值
            If control_condition_ad >= g_controlvaluepart And control_condition_ad < g_controlvalueall And (rs_lamp.Fields("state").Value = 4 Or rs_lamp.Fields("state").Value = 1) Then
                '比对成功，灯是亮的,半功率
                GetCompareState_AD2 = "B_part"
            End If
            If control_condition_ad >= g_controlvalueall And (rs_lamp.Fields("state").Value = 4 Or rs_lamp.Fields("state").Value = 1) Then
                '比对成功，灯是亮的,全功率
                GetCompareState_AD2 = "B_all"
            End If
            If control_condition_ad < g_controlvaluepart And (rs_lamp.Fields("state").Value = 4 Or rs_lamp.Fields("state").Value = 1) Then
                '比对失败，灯是该亮非亮
                GetCompareState_AD2 = "C"
            End If
            If control_condition_ad >= g_controlvaluepart And control_condition_ad < g_controlvalueall And (rs_lamp.Fields("state").Value = 3 Or rs_lamp.Fields("state").Value = 0) Then
                '比对失败，灯是该暗非暗
                GetCompareState_AD2 = "D_part"
            End If
            If control_condition_ad >= g_controlvaluepart And (rs_lamp.Fields("state").Value = 3 Or rs_lamp.Fields("state").Value = 0) Then
                '比对失败，灯是该暗非暗
                GetCompareState_AD2 = "D_all"
            End If
            If control_condition_ad < g_controlvaluepart And (rs_lamp.Fields("state").Value = 3 Or rs_lamp.Fields("state").Value = 0) Then
                '比对成功，灯是暗的
                GetCompareState_AD2 = "A"
            End If
            '更改路灯的实时状态时间
            'sql = "update lamp_inf set date='" & Now & "' where lamp_id='" & g_lampidstring & "'"
            'DBOperation.ExecuteSQL(conn, sql, msg)
            '如果获取的状态时正常的，并且之前的状态为故障，则表示故障维修完毕，标志lamp_inf表中的total_num位为0
            '      If (GetCompareState_AD2 = "A" Or GetCompareState_AD2 = "B_part" Or GetCompareState_AD2 = "B_all") And (rs_lamp.Fields("result").Value = 1 Or rs_lamp.Fields("result").Value = 2) Then
            If (GetCompareState_AD2 = "A" Or GetCompareState_AD2 = "B_part" Or GetCompareState_AD2 = "B_all") And (rs_lamp.Fields("result").Value <> 0) Then

                '路灯的实时状态表
                sql = "update lamp_inf set total_num=0 where lamp_id='" & g_lampidstring & "'"
                DBOperation.ExecuteSQL(conn, sql, msg)
                '路灯的故障表
                sql = "update lamp_inf_record set server_state=1, date_end='" & Now & "' where lamp_id='" & g_lampidstring & "' and server_state=0"
                DBOperation.ExecuteSQL(conn, sql, msg)
            End If

next1:
            sql = "update RoadLightStatus set HandlerFlag=1 where ID='" & state_id & "'"
            DBOperation.ExecuteSQL(conn, sql, msg)
            Com_inf.lamp_id_to_detail(g_lampidstring)   '通过灯的编号获取电控箱名称，类型和灯的独立编号
            Com_inf.Get_current_and_presure_AD2(g_currentad)  '将电流和电压的AD值转换成电流电压实际值
            orig_state = rs_lamp.Fields("state").Value
            orig_result = rs_lamp.Fields("result").Value
            Select Case GetCompareState_AD2
                Case "F" '无返回值
                    ' current = 0.0
                    If orig_result <> 3 Then
                        sql = "update lamp_inf set date='" & Now & "' , result=" & 3 & ", presure_l=0 , current_l=0, power=" & 0 & ",total_num=0 where lamp_id='" & g_lampidstring & "'"
                    Else
                        sql = "update lamp_inf set result=" & 3 & ", presure_l=0 , current_l=0, power=" & 0 & ",total_num=0 where lamp_id='" & g_lampidstring & "'"
                    End If
                    DBOperation.ExecuteSQL(conn, sql, msg)
                    state_inf = LAMP_STATE_NORETURN
                    Add_state_record(g_lampidstring, state_inf, 0, 0, 0, 0)
                Case "A"   '关闭状态
                    'current = 0.0
                    If (orig_state = 0 Or orig_state = 3) And (orig_result = 0 Or orig_result = 4) Then
                        sql = "update lamp_inf set result=" & 0 & " , presure_l=" & g_presurevalue & " , current_l=" & g_currentvalue & ", state=" & 0 & " ,power=" & 0 & ", total_num=0 where lamp_id='" & g_lampidstring & "'"
                    Else
                        sql = "update lamp_inf set date='" & Now & "' , result=" & 0 & " , presure_l=" & g_presurevalue & " , current_l=" & g_currentvalue & ", state=" & 0 & " ,power=" & 0 & ", total_num=0 where lamp_id='" & g_lampidstring & "'"
                    End If
                    '  sql = "update lamp_inf set result=" & 0 & " , presure_l=" & g_presurevalue & " , current_l=" & g_currentvalue & ", state=" & 0 & " ,power=" & 0 & ", total_num=0 where lamp_id='" & g_lampidstring & "'"
                    DBOperation.ExecuteSQL(conn, sql, msg)
                    state_inf = LAMP_STATE_OFF
                    Add_state_record(g_lampidstring, state_inf, g_presurevalue, g_currentvalue, 0, 0)
                Case "B_part"  '半功率
                    If (orig_state = 1 Or orig_state = 4) And (orig_result = 0 Or orig_result = 4) Then
                        sql = "update lamp_inf set result=" & 0 & " , presure_l=" & g_presurevalue & " , current_l=" & g_currentvalue & ", state=" & 1 & " ,power=" & 50 & ", total_num=0 where lamp_id='" & g_lampidstring & "'"
                    Else
                        sql = "update lamp_inf set date='" & Now & "' ,result=" & 0 & " , presure_l=" & g_presurevalue & " , current_l=" & g_currentvalue & ", state=" & 1 & " ,power=" & 50 & ", total_num=0 where lamp_id='" & g_lampidstring & "'"
                    End If
                    '  sql = "update lamp_inf set result=" & 0 & " , presure_l=" & g_presurevalue & " , current_l=" & g_currentvalue & ", state=" & 1 & " ,power=" & 50 & ", total_num=0 where lamp_id='" & g_lampidstring & "'"
                    DBOperation.ExecuteSQL(conn, sql, msg)
                    state_inf = LAMP_STATE_ON
                    Add_state_record(g_lampidstring, state_inf, g_presurevalue, g_currentvalue, 0, 0)
                Case "B_all"  '全功率
                    If (orig_state = 1 Or orig_state = 4) And (orig_result = 0 Or orig_result = 4) Then
                        sql = "update lamp_inf set result=" & 0 & " , presure_l=" & g_presurevalue & " , current_l=" & g_currentvalue & ", state=" & 1 & " ,power=" & 100 & ", total_num=0 where lamp_id='" & g_lampidstring & "'"
                    Else
                        sql = "update lamp_inf set date='" & Now & "' ,result=" & 0 & " , presure_l=" & g_presurevalue & " , current_l=" & g_currentvalue & ", state=" & 1 & " ,power=" & 100 & ", total_num=0 where lamp_id='" & g_lampidstring & "'"
                    End If
                    'sql = "update lamp_inf set result=" & 0 & " , presure_l=" & g_presurevalue & " , current_l=" & g_currentvalue & ", state=" & 1 & " ,power=" & 100 & ", total_num=0  where lamp_id='" & g_lampidstring & "'"
                    DBOperation.ExecuteSQL(conn, sql, msg)
                    state_inf = LAMP_STATE_ON
                    Add_state_record(g_lampidstring, state_inf, g_presurevalue, g_currentvalue, 0, 0)
                Case "C"
                    '判断故障列表中是否存在该路灯的该故障，该亮非亮
                    If rs_lamp.Fields("total_num").Value = 2 Then '表示累计2次故障则发过短信
                        '发送短信
                        Send_Msg(rs_lamp.Fields("control_box_id").Value, rs_lamp.Fields("lamp_id").Value, LAMP_STATE_PROBLEM_ON)
                    End If
                    pro_num = rs_lamp.Fields("total_num").Value + 1
                    If pro_num > 10000 Then
                        pro_num = 20
                    End If
                    state_inf = LAMP_STATE_PROBLEM_ON
                    If rs_lamp.Fields("total_num").Value >= 1 Then  '有故障的，确认2次后才记录
                        Add_state_record(g_lampidstring, state_inf, g_presurevalue, g_currentvalue, 0, 0)
                        Input_problem(1, g_lampidstring)  '写入故障列表
                        If orig_result <> 1 Then
                            sql = "update lamp_inf set date='" & Now & "' ,result=" & 1 & " , presure_l=" & g_presurevalue & " , current_l=" & g_currentvalue & ", power=" & 0 & " , total_num=" & pro_num & " where lamp_id='" & g_lampidstring & "'"
                        Else
                            sql = "update lamp_inf set result=" & 1 & " , presure_l=" & g_presurevalue & " , current_l=" & g_currentvalue & ", power=" & 0 & " , total_num=" & pro_num & " where lamp_id='" & g_lampidstring & "'"
                        End If
                        ' sql = "update lamp_inf set result=" & 1 & " , presure_l=" & g_presurevalue & " , current_l=" & g_currentvalue & ", power=" & 0 & " , total_num=" & pro_num & " where lamp_id='" & g_lampidstring & "'"
                        DBOperation.ExecuteSQL(conn, sql, msg)
                    Else
                        sql = "update lamp_inf set total_num=" & pro_num & " where lamp_id='" & g_lampidstring & "'"
                        DBOperation.ExecuteSQL(conn, sql, msg)
                    End If
                Case "D_part"
                    ''判断故障列表中是否存在该路灯的该故障，该暗非暗
                    If rs_lamp.Fields("total_num").Value = 2 Then '表示累计2次故障则发过短信
                        '发送短信
                        Send_Msg(rs_lamp.Fields("control_box_id").Value, rs_lamp.Fields("lamp_id").Value, LAMP_STATE_PROBLEM_OFF)
                        System.Threading.Thread.Sleep(2000)

                    End If
                    pro_num = rs_lamp.Fields("total_num").Value + 1
                    If pro_num > 10000 Then
                        pro_num = 20
                    End If
                    state_inf = LAMP_STATE_PROBLEM_OFF
                    If rs_lamp.Fields("total_num").Value >= 1 Then  '有故障的，确认2次后才记录
                        Add_state_record(g_lampidstring, state_inf, g_presurevalue, g_currentvalue, 0, 0)
                        Input_problem(2, g_lampidstring)  '写入故障列表
                        If orig_result <> 2 Then
                            sql = "update lamp_inf set date='" & Now & "' ,result=" & 2 & ", presure_l=" & g_presurevalue & " , current_l=" & g_currentvalue & ", power=" & 50 & " , total_num=" & pro_num & " where lamp_id='" & g_lampidstring & "'"
                        Else
                            sql = "update lamp_inf set result=" & 2 & ", presure_l=" & g_presurevalue & " , current_l=" & g_currentvalue & ", power=" & 50 & " , total_num=" & pro_num & " where lamp_id='" & g_lampidstring & "'"
                        End If

                        DBOperation.ExecuteSQL(conn, sql, msg)
                    Else
                        sql = "update lamp_inf set total_num=" & pro_num & " where lamp_id='" & g_lampidstring & "'"
                        DBOperation.ExecuteSQL(conn, sql, msg)
                    End If
                Case "D_all"
                    ''判断故障列表中是否存在该路灯的该故障，该暗非暗
                    If rs_lamp.Fields("total_num").Value = 2 Then '表示累计2次故障则发过短信
                        '发送短信
                        Send_Msg(rs_lamp.Fields("control_box_id").Value, rs_lamp.Fields("lamp_id").Value, LAMP_STATE_PROBLEM_OFF)

                    End If
                    pro_num = rs_lamp.Fields("total_num").Value + 1
                    If pro_num > 10000 Then
                        pro_num = 20
                    End If
                    state_inf = LAMP_STATE_PROBLEM_OFF
                    If rs_lamp.Fields("total_num").Value >= 1 Then  '有故障的，确认2次后才记录
                        Add_state_record(g_lampidstring, state_inf, g_presurevalue, g_currentvalue, 0, 0)
                        Input_problem(2, rs_lamp.Fields("lamp_id").Value)  '写入故障列表
                        If orig_result <> 2 Then
                            sql = "update lamp_inf set date='" & Now & "' ,result=" & 2 & ", presure_l=" & g_presurevalue & " , current_l=" & g_currentvalue & ", power=" & 100 & " , total_num=" & pro_num & " where lamp_id='" & g_lampidstring & "'"
                        Else
                            sql = "update lamp_inf set result=" & 2 & ", presure_l=" & g_presurevalue & " , current_l=" & g_currentvalue & ", power=" & 100 & " , total_num=" & pro_num & " where lamp_id='" & g_lampidstring & "'"
                        End If

                        ' sql = "update lamp_inf set result=" & 2 & ", presure_l=" & g_presurevalue & " , current_l=" & g_currentvalue & ", power=" & 100 & " , total_num=" & pro_num & " where lamp_id='" & g_lampidstring & "'"
                        DBOperation.ExecuteSQL(conn, sql, msg)
                    Else
                        sql = "update lamp_inf set total_num=" & pro_num & " where lamp_id='" & g_lampidstring & "'"
                        DBOperation.ExecuteSQL(conn, sql, msg)
                    End If
            End Select
            'Add_state_record(g_lampidstring, state_inf, g_presurevalue, g_currentvalue)
        End If
        If rs_lamp.State = 1 Then
            rs_lamp.Close()
            rs_lamp = Nothing
        End If

        conn.Close()
        conn = Nothing
    End Function


    ''' <summary>
    ''' 获取单灯状态与数据库中的操作状态对比，得到路灯的实际状态，是否故障
    ''' </summary>
    ''' <param name="state_string">状态表中的状态字符串</param>
    ''' <param name="state_id">状态表中的id</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCompareState(ByVal state_string As String, ByVal state_id As Integer) As String

        Dim rs_lamp As New ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim control_condition_ad As Integer  '控制条件：电阻or电流
        Dim conn As New ADODB.Connection
        Dim state_inf As String '分析的状态
        Dim pro_num As Integer   '统计故障次数
        If DBOperation.OpenConn(conn) = False Then
            GetCompareState = ""
            Exit Function
        End If
        msg = ""
        state_inf = ""
        If Mid(state_string, 10, 5) = "FF FF" Then  '无返回值
            GetCompareState = "F"
            m_dianzuad = 0  '电阻AD值
            m_currentad = 0   '电流AD值
            GoTo next1
        End If
        '有返回的路灯状态数据,将 其与路灯状态表中的状态对比，A,暗；B,亮；C，该亮非亮；D该暗非暗

        sql = "select state,result,total_num,control_box_id,lamp_id from lamp_inf where lamp_id='" & g_lampidstring & "'"
        rs_lamp = DBOperation.SelectSQL(conn, sql, msg)
        GetCompareState = "E"  '标志状态默认值
        If rs_lamp Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "GetCompareState" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)
            conn.Close()
            conn = Nothing
            Exit Function
        End If

        If rs_lamp.RecordCount > 0 Then
            'Get_condition()   '获取设置的全功率半功率的AD阈值
            '以电流为判断标志
            control_condition_ad = g_currentad

            If control_condition_ad >= g_controlvaluepart And control_condition_ad < g_controlvalueall And (rs_lamp.Fields("state").Value = 4 Or rs_lamp.Fields("state").Value = 1) Then
                '比对成功，灯是亮的,半功率
                GetCompareState = "B_part"

            End If
            If control_condition_ad >= g_controlvalueall And (rs_lamp.Fields("state").Value = 4 Or rs_lamp.Fields("state").Value = 1) Then
                '比对成功，灯是亮的,全功率
                GetCompareState = "B_all"

            End If
            If control_condition_ad < g_controlvaluepart And (rs_lamp.Fields("state").Value = 4 Or rs_lamp.Fields("state").Value = 1) Then
                '比对失败，灯是该亮非亮
                GetCompareState = "C"

            End If
            If control_condition_ad >= g_controlvaluepart And control_condition_ad < g_controlvalueall And (rs_lamp.Fields("state").Value = 3 Or rs_lamp.Fields("state").Value = 0) Then
                '比对失败，灯是该暗非暗
                GetCompareState = "D_part"

            End If
            If control_condition_ad >= g_controlvaluepart And (rs_lamp.Fields("state").Value = 3 Or rs_lamp.Fields("state").Value = 0) Then
                '比对失败，灯是该暗非暗
                GetCompareState = "D_all"

            End If
            If control_condition_ad < g_controlvaluepart And (rs_lamp.Fields("state").Value = 3 Or rs_lamp.Fields("state").Value = 0) Then
                '比对成功，灯是暗的
                GetCompareState = "A"

            End If

            '更改路灯的实时状态时间
            sql = "update lamp_inf set date='" & Now & "' where lamp_id='" & g_lampidstring & "'"
            DBOperation.ExecuteSQL(conn, sql, msg)

            '如果获取的状态时正常的，并且之前的状态为故障，则表示故障维修完毕，标志lamp_inf表中的total_num位为0
            ' If (GetCompareState = "A" Or GetCompareState = "B_part" Or GetCompareState = "B_all") And (rs_lamp.Fields("result").Value = 1 Or rs_lamp.Fields("result").Value = 2) Then
            If (GetCompareState = "A" Or GetCompareState = "B_part" Or GetCompareState = "B_all") And (rs_lamp.Fields("result").Value <> 0) Then

                '路灯的实时状态表
                sql = "update lamp_inf set total_num=0 where lamp_id='" & g_lampidstring & "'"
                DBOperation.ExecuteSQL(conn, sql, msg)
                '路灯的故障表
                sql = "update lamp_inf_record set server_state=1, date_end='" & Now & "' where lamp_id='" & g_lampidstring & "' and server_state=0"
                DBOperation.ExecuteSQL(conn, sql, msg)

            End If

next1:

            sql = "update RoadLightStatus set HandlerFlag=1 where ID='" & state_id & "'"
            DBOperation.ExecuteSQL(conn, sql, msg)


            Com_inf.lamp_id_to_detail(g_lampidstring)   '通过灯的编号获取电控箱名称，类型和灯的独立编号

            Com_inf.Get_current_and_presure(g_currentad, g_dianzuad)  '将电流和电压的AD值转换成电流电压实际值

            Select Case GetCompareState

                Case "F" '无返回值
                    ' current = 0.0
                    sql = "update lamp_inf set date='" & Now & "' , result=" & 3 & ", presure_l=0 , current_l=0, power=" & 0 & ",total_num=0 where lamp_id='" & g_lampidstring & "'"
                    DBOperation.ExecuteSQL(conn, sql, msg)
                    state_inf = LAMP_STATE_NORETURN
                    Add_state_record(g_lampidstring, state_inf, g_presurevalue, g_currentvalue, 0, 0)

                Case "A"   '关闭状态
                    'current = 0.0
                    sql = "update lamp_inf set result=" & 0 & " , presure_l=" & g_presurevalue & " , current_l=" & g_currentvalue & ", state=" & 0 & " ,power=" & 0 & ", total_num=0 where lamp_id='" & g_lampidstring & "'"
                    DBOperation.ExecuteSQL(conn, sql, msg)
                    state_inf = LAMP_STATE_OFF
                    Add_state_record(g_lampidstring, state_inf, g_presurevalue, g_currentvalue, 0, 0)

                Case "B_part"  '半功率
                    sql = "update lamp_inf set result=" & 0 & " , presure_l=" & g_presurevalue & " , current_l=" & g_currentvalue & ", state=" & 1 & " ,power=" & 50 & ", total_num=0 where lamp_id='" & g_lampidstring & "'"
                    DBOperation.ExecuteSQL(conn, sql, msg)
                    state_inf = LAMP_STATE_ON
                    Add_state_record(g_lampidstring, state_inf, g_presurevalue, g_currentvalue, 0, 0)

                Case "B_all"  '全功率
                    sql = "update lamp_inf set result=" & 0 & " , presure_l=" & g_presurevalue & " , current_l=" & g_currentvalue & ", state=" & 1 & " ,power=" & 100 & ", total_num=0  where lamp_id='" & g_lampidstring & "'"
                    DBOperation.ExecuteSQL(conn, sql, msg)
                    state_inf = LAMP_STATE_ON
                    Add_state_record(g_lampidstring, state_inf, g_presurevalue, g_currentvalue, 0, 0)

                Case "C"
                    '判断故障列表中是否存在该路灯的该故障，该亮非亮


                    If rs_lamp.Fields("total_num").Value = 4 Then '表示累计4次故障则发过短信
                        '发送短信
                        Send_Msg(rs_lamp.Fields("control_box_id").Value, rs_lamp.Fields("lamp_id").Value, LAMP_STATE_PROBLEM_ON)

                    End If
                    pro_num = rs_lamp.Fields("total_num").Value + 1
                    If pro_num > 10000 Then
                        pro_num = 20
                    End If
                    state_inf = LAMP_STATE_PROBLEM_ON
                    If rs_lamp.Fields("total_num").Value >= 1 Then  '有故障的，确认2次后才记录
                        Add_state_record(g_lampidstring, state_inf, g_presurevalue, g_currentvalue, 0, 0)
                        Input_problem(1, g_lampidstring)  '写入故障列表
                        sql = "update lamp_inf set result=" & 1 & " , presure_l=" & g_presurevalue & " , current_l=" & g_currentvalue & ", power=" & 0 & " , total_num=" & pro_num & " where lamp_id='" & g_lampidstring & "'"
                        DBOperation.ExecuteSQL(conn, sql, msg)
                    Else
                        sql = "update lamp_inf set total_num=" & pro_num & " where lamp_id='" & g_lampidstring & "'"
                        DBOperation.ExecuteSQL(conn, sql, msg)
                    End If


                Case "D_part"
                    ''判断故障列表中是否存在该路灯的该故障，该暗非暗



                    If rs_lamp.Fields("total_num").Value = 4 Then '表示累计4次故障则发过短信
                        '发送短信
                        Send_Msg(rs_lamp.Fields("control_box_id").Value, rs_lamp.Fields("lamp_id").Value, LAMP_STATE_PROBLEM_OFF)
                        System.Threading.Thread.Sleep(2000)

                    End If
                    pro_num = rs_lamp.Fields("total_num").Value + 1
                    If pro_num > 10000 Then
                        pro_num = 20
                    End If
                    state_inf = LAMP_STATE_PROBLEM_OFF
                    If rs_lamp.Fields("total_num").Value >= 1 Then  '有故障的，确认2次后才记录
                        Add_state_record(g_lampidstring, state_inf, g_presurevalue, g_currentvalue, 0, 0)
                        Input_problem(2, g_lampidstring)  '写入故障列表
                        sql = "update lamp_inf set result=" & 2 & ", presure_l=" & g_presurevalue & " , current_l=" & g_currentvalue & ", power=" & 50 & " , total_num=" & pro_num & " where lamp_id='" & g_lampidstring & "'"
                        DBOperation.ExecuteSQL(conn, sql, msg)

                    Else
                        sql = "update lamp_inf set total_num=" & pro_num & " where lamp_id='" & g_lampidstring & "'"
                        DBOperation.ExecuteSQL(conn, sql, msg)
                    End If



                Case "D_all"
                    ''判断故障列表中是否存在该路灯的该故障，该暗非暗


                    If rs_lamp.Fields("total_num").Value = 4 Then '表示累计4次故障则发过短信
                        '发送短信
                        Send_Msg(rs_lamp.Fields("control_box_id").Value, rs_lamp.Fields("lamp_id").Value, LAMP_STATE_PROBLEM_OFF)

                    End If
                    pro_num = rs_lamp.Fields("total_num").Value + 1
                    If pro_num > 10000 Then
                        pro_num = 20
                    End If
                    state_inf = LAMP_STATE_PROBLEM_OFF
                    If rs_lamp.Fields("total_num").Value >= 1 Then  '有故障的，确认2次后才记录
                        Add_state_record(g_lampidstring, state_inf, g_presurevalue, g_currentvalue, 0, 0)
                        Input_problem(2, rs_lamp.Fields("lamp_id").Value)  '写入故障列表
                        sql = "update lamp_inf set result=" & 2 & ", presure_l=" & g_presurevalue & " , current_l=" & g_currentvalue & ", power=" & 100 & " , total_num=" & pro_num & " where lamp_id='" & g_lampidstring & "'"
                        DBOperation.ExecuteSQL(conn, sql, msg)
                    Else
                        sql = "update lamp_inf set total_num=" & pro_num & " where lamp_id='" & g_lampidstring & "'"
                        DBOperation.ExecuteSQL(conn, sql, msg)
                    End If


            End Select

        End If


        If rs_lamp.State = 1 Then
            rs_lamp.Close()
            rs_lamp = Nothing
        End If

        conn.Close()
        conn = Nothing
    End Function


    ''' <summary>
    ''' 将当前分析出的状态数据插入到lamp_state_list表中
    ''' </summary>
    ''' <param name="lamp_id">灯的编号</param>
    ''' <param name="state_inf">状态信息</param>
    ''' <param name="presure">电压值</param>
    ''' <param name="current">电流值</param>
    ''' <remarks></remarks>
    Private Sub Add_state_record(ByVal lamp_id As String, ByVal state_inf As String, ByVal presure As Double, ByVal current As Double, ByVal power As Double, ByVal yinshu As Double)
        '将当前的状态插入到lamp_state_list
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim time As Date
        time = Now()
        state_inf = Trim(state_inf)
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        msg = ""
        sql = "select * from lamp_state_list where lamp_id='" & lamp_id & "' and (end_tag=" & 0 & " or end_tag=" & 2 & ") order by id"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Add_state_record" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs.RecordCount <= 0 Then
            '如果没有记录，则增加一条新的记录
            sql = "insert into lamp_state_list(lamp_id,state,time_start,time_end,end_tag,presure_value,current_value,power_value,yinshu_value) values('" & lamp_id & "','" & state_inf & "','" & time & "','" & time & "',0," & presure & "," & current & "," & power & "," & yinshu & ")"
            DBOperation.ExecuteSQL(conn, sql, msg)
        Else
            If Trim(rs.Fields("state").Value) = state_inf Then   '如果灯的状态连续，则修改状态的结束时间
                sql = "update lamp_state_list set time_end='" & time & "' , presure_value=" & presure & ",current_value=" & current & ", power_value=" & power & ", yinshu_value=" & yinshu & " where lamp_id='" & lamp_id & "' and (end_tag=" & 0 & " or end_tag=" & 2 & ") "
                DBOperation.ExecuteSQL(conn, sql, msg)
            Else
                sql = "update lamp_state_list set end_tag='" & rs.Fields("end_tag").Value + 1 & "' where lamp_id='" & lamp_id & "' and (end_tag=" & 0 & " or end_tag=" & 2 & ") "
                DBOperation.ExecuteSQL(conn, sql, msg)
                sql = "insert into lamp_state_list(lamp_id,state,time_start,time_end,end_tag,presure_value,current_value,power_value,yinshu_value) values('" & lamp_id & "','" & state_inf & "','" & time & "','" & time & "',0," & presure & "," & current & "," & power & "," & yinshu & ")"
                DBOperation.ExecuteSQL(conn, sql, msg)
            End If
        End If
        '2013年1月3日增加每条数据都记录在数据库lamp_state_record表中
        sql = "insert into lamp_alarm_record(lamp_id,state,time_start,time_end,presure_value,current_value,power_value,yinshu_value) values('" & lamp_id & "','" & state_inf & "','" & time & "','" & time & "'," & presure & "," & current & "," & power & "," & yinshu & ")"
        DBOperation.ExecuteSQL(conn, sql, msg)
        rs.Close()
        rs = Nothing
        conn.Close()
        conn = Nothing
        Exit Sub
    End Sub


    ''' <summary>
    ''' 将当前分析出的状态数据插入到lamp_alarm_list表中
    ''' </summary>
    ''' <param name="lamp_id">灯的编号</param>
    ''' <param name="state_inf">状态信息</param>
    ''' <param name="presure">电压值</param>
    ''' <param name="current">电流值</param>
    ''' <remarks></remarks>
    Private Sub Add_alarm_record(ByVal lamp_id As String, ByVal state_inf As String, ByVal presure As Double, ByVal current As Double, ByVal power As Double, ByVal yinshu As Double)
        '将当前的状态插入到lamp_alarm_list
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim time As Date
        time = Now()
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        msg = ""
        sql = "insert into lamp_alarm_record(lamp_id,state,time_start,time_end,end_tag,presure_value,current_value,power_value,yinshu_value) values('" & lamp_id & "','" & state_inf & "','" & time & "','" & time & "',0," & presure & "," & current & "," & power & "," & yinshu & ")"
        DBOperation.ExecuteSQL(conn, sql, msg)
        conn.Close()
        conn = Nothing
        Exit Sub
    End Sub
    ''' <summary>
    ''' 获取单灯状态与数据库中的操作状态对比，得到路灯的实际状态，是否故障
    ''' </summary>
    ''' <param name="lamp_id_tag">灯的编号</param>
    ''' <param name="find_tag">1，批量查询，2单灯查询</param>
    ''' <param name="time">查询时间</param>
    ''' <returns>灯的状态</returns>
    ''' <remarks></remarks>
    Public Function Get_actual_state(ByVal lamp_id_tag As String, ByVal find_tag As Integer, ByVal time As Date) As String

        Dim rs As New ADODB.Recordset
        Dim rs_lamp As New ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim control_box_ox As String  '电控箱编号的十六进制表示
        Dim lamp_id_ox As String  '灯的编号的十六进制表示
        Dim control_condition_ad As Integer  '控制条件：电阻or电流
        Dim control_value_ad_part, control_value_ad_all As Integer  '半功率，全功率控制条件的界限值
        Dim ox_str As String  '十六进制字符串
        Dim conn As New ADODB.Connection
        Dim no_return_num As Integer '无返回值的次数
        no_return_num = 0

        If DBOperation.OpenConn(conn) = False Then
            Get_actual_state = ""
            Exit Function
        End If


        msg = ""
        '将灯的两位类型编号转换成5位长度的二进制，三位长度的类型下灯的编号转换成11位长度二进制
        ox_str = Com_inf.Dec_to_Bin(Mid(lamp_id_tag, 5, 2), 5) & Com_inf.Dec_to_Bin(Mid(lamp_id_tag, 7, LAMP_ID_LEN), 11)
        ox_str = Com_inf.BIN_to_HEX(ox_str)   '控制命令中的路灯编号16位二进制转换成四位十六进制
        If SYSTEM_VISION = 1 Then
            control_box_ox = Com_inf.Dec_to_Hex(Mid(lamp_id_tag, 1, 4), 2) '将四位十进制电控箱编号转换成2位的十六进制
            lamp_id_ox = control_box_ox & " " & Mid(ox_str, 1, 2) & " " & Mid(ox_str, 3, 2) '路灯编号的16进制位数 4位

        Else
            control_box_ox = Com_inf.Dec_to_Hex(Mid(lamp_id_tag, 1, 4), 4) '将四位十进制电控箱编号转换成2位的十六进制
            lamp_id_ox = Mid(control_box_ox, 3, 2) & " " & Mid(ox_str, 1, 2) & " " & Mid(ox_str, 3, 2) & " " & Mid(control_box_ox, 1, 2) '路灯编号的16进制位数 4位

        End If

        If find_tag = 1 Then
            '批量查询
            sql = "select * from RoadLightStatus where StatusContent like '" & lamp_id_ox & "%' and HandlerFlag=" & 2 & " and Createtime > DateAdd(n,-30,'" & Now() & "') order by Createtime "

            'sql = "select * from RoadLightStatus where StatusContent like '" & lamp_id_ox & "%' and HandlerFlag=" & 2 & " order by Createtime "
        Else
            sql = "select * from RoadLightStatus where StatusContent like '" & lamp_id_ox & "%' and HandlerFlag=" & 0 & " and createtime >'" & time & "' order by Createtime "

        End If
        ' sql = "select * from RoadLightStatus where StatusContent like '" & lamp_id_ox & "%' AND Createtime>'2010-3-26 06:00:10' and Createtime <'2010-3-26 06:30:10' order by Createtime"

        rs = DBOperation.SelectSQL(conn, sql, msg)   '查询状态表中为被标志的及时间在发送命令之后的状态
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Get_actual_state" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            Get_actual_state = ""
            conn.Close()
            conn = Nothing
            Exit Function
        End If

        Get_actual_state = ""
        While rs.EOF = False
            If Mid(Trim(rs.Fields("StatusContent").Value), 10, 5) = "FF FF" Then
                If no_return_num = rs.RecordCount - 1 Then
                    '表示这段时间内收到的信号都是无返回值的
                    Get_actual_state = "F"
                    m_dianzuad = -1  '电阻AD值
                    m_currentad = -1   '电流AD值
                Else
                    no_return_num += 1
                End If

                '恢复标志位
                rs.Fields("HandlerFlag").Value = 1  '将处理标志位置为1

                rs.Update()
                rs.MoveNext()
                Continue While
                'Exit While
            End If
            '有返回的路灯状态数据,将 其与路灯状态表中的状态对比，A,暗；B,亮；C，该亮非亮；D该暗非暗

            sql = "select * from lamp_inf where lamp_id='" & lamp_id_tag & "'"
            rs_lamp = DBOperation.SelectSQL(conn, sql, msg)

            If rs_lamp.RecordCount > 0 Then
                '获取当前点坐标

                '进行亮暗 的比对
                '    Explain_State_String(Trim(rs.Fields("StatusContent").Value))

                Get_condition()



                control_value_ad_part = g_controlvaluepart  '判断路灯亮暗的控制值，即电阻值或电流值
                control_value_ad_all = g_controlvalueall '全功率的判断值              

                ''***********************************************


                m_dianzuad = g_dianzuad  '电阻AD值
                m_currentad = g_currentad '电流AD值 ,修改为标准安培值
                '以电流为判断标志
                control_condition_ad = m_currentad
                Get_actual_state = "E"  '标志状态默认值
                If control_condition_ad >= control_value_ad_part And control_condition_ad < control_value_ad_all And (rs_lamp.Fields("state").Value = 4 Or rs_lamp.Fields("state").Value = 1) Then
                    '比对成功，灯是亮的,半功率
                    Get_actual_state = "B_part"

                End If
                If control_condition_ad >= control_value_ad_all And (rs_lamp.Fields("state").Value = 4 Or rs_lamp.Fields("state").Value = 1) Then
                    '比对成功，灯是亮的,全功率
                    Get_actual_state = "B_all"

                End If
                If control_condition_ad < control_value_ad_part And (rs_lamp.Fields("state").Value = 4 Or rs_lamp.Fields("state").Value = 1) Then
                    '比对失败，灯是该亮非亮
                    Get_actual_state = "C"

                End If
                If control_condition_ad >= control_value_ad_part And control_condition_ad < control_value_ad_all And (rs_lamp.Fields("state").Value = 3 Or rs_lamp.Fields("state").Value = 0) Then
                    '比对失败，灯是该暗非暗
                    Get_actual_state = "D_part"

                End If
                If control_condition_ad >= control_value_ad_all And (rs_lamp.Fields("state").Value = 3 Or rs_lamp.Fields("state").Value = 0) Then
                    '比对失败，灯是该暗非暗
                    Get_actual_state = "D_all"

                End If
                If control_condition_ad < control_value_ad_part And (rs_lamp.Fields("state").Value = 3 Or rs_lamp.Fields("state").Value = 0) Then
                    '比对成功，灯是暗的
                    Get_actual_state = "A"

                End If
            End If
            'Exit While

            '恢复标志位
            'If find_tag = 1 Then
            '    '批量查询
            '    sql = "update RoadLightStatus set HandlerFlag=1 where StatusContent like '" & lamp_id_ox & "%' and HandlerFlag=" & 2
            'Else
            '    sql = "update RoadLightStatus set HandlerFlag=1 where StatusContent like '" & lamp_id_ox & "%' and HandlerFlag=" & 0

            'End If
            'DBOperation.ExecuteSQL(conn, sql, msg)
            'sql = "update RoadLightStatus(HandlerFlag) values(1) where "
            'rs.Resync(ADODB.AffectEnum.adAffectCurrent)
            'rs.Fields("HandlerFlag").Value = 1  '将处理标志位置为1
            sql = "update RoadLightStatus set HandlerFlag=1 where ID='" & rs.Fields("ID").Value & "'"
            'rs.Update()
            DBOperation.ExecuteSQL(conn, sql, msg)

            rs.MoveNext()
        End While
        If rs_lamp.State = 1 Then
            rs_lamp.Close()
            rs_lamp = Nothing
        End If
        ' rs_lamp = Nothing
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If

        conn.Close()
        conn = Nothing
    End Function

    ''' <summary>
    ''' '将lamp_inf 表中的状态修改为当前手工控制状态
    ''' </summary>
    ''' <param name="control_box_id">区域编号</param>
    ''' <param name="control_method">控制方法</param>
    ''' <param name="diangan">电感</param>
    ''' <param name="power">电功率</param>
    ''' <remarks></remarks>
    Public Sub Input_hand_control_table_demo(ByVal control_box_id As String, ByVal control_method As String, ByVal diangan As String, ByVal power As String)

        Dim rs_lamp As New ADODB.Recordset
        Dim sql As String
        Dim msg As String
        Dim conn As New ADODB.Connection
        Dim state_tag As Integer
        Dim power_tag As Integer
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If

        msg = ""
        sql = "select * from lamp_inf where control_box_id='" & control_box_id & "'"
        rs_lamp = DBOperation.SelectSQL(conn, sql, msg)

        While rs_lamp.EOF = False
            If rs_lamp.Fields("lamp_kind").Value = 0 Then
                '电感型 功率
                If Trim(diangan) = "全功率" Then
                    power_tag = 100
                Else
                    If Trim(diangan) = "半功率" Then
                        power_tag = 50
                    Else
                        power_tag = 0
                    End If
                End If
            Else
                '电子型  功率
                If power <> "" Then
                    power_tag = Val(power)
                Else
                    power_tag = 0
                End If

            End If

            If control_method = "全开" Then
                state_tag = 1  '控制状态开
            End If
            If control_method = "全闭" Then
                state_tag = 0   '控制状态关
            End If

            If control_method = "奇开" Then
                If Val(Mid(Trim(rs_lamp.Fields("lamp_id").Value), 6, 4)) Mod 2 <> 0 Then
                    state_tag = 1
                Else
                    state_tag = 0
                End If

            End If

            If control_method = "奇闭" Then
                If Val(Mid(Trim(rs_lamp.Fields("lamp_id").Value), 6, 4)) Mod 2 <> 0 Then
                    state_tag = 0
                End If

            End If
            If control_method = "偶开" Then
                If Val(Mid(Trim(rs_lamp.Fields("lamp_id").Value), 6, 4)) Mod 2 = 0 Then
                    state_tag = 1
                Else
                    state_tag = 0
                End If

            End If

            If control_method = "偶闭" Then
                If Val(Mid(Trim(rs_lamp.Fields("lamp_id").Value), 6, 4)) Mod 2 = 0 Then
                    state_tag = 0
                End If

            End If

            If control_method = "1/3开" Then
                If (Val(Mid(Trim(rs_lamp.Fields("lamp_id").Value), 6, 4)) - 1) Mod 3 = 0 Then
                    state_tag = 1
                Else
                    state_tag = 0
                End If

            End If

            If control_method = "1/3闭" Then
                If (Val(Mid(Trim(rs_lamp.Fields("lamp_id").Value), 6, 4)) - 1) Mod 3 = 0 Then
                    state_tag = 0
                End If

            End If

            If control_method = "1/4开" Then
                If (Val(Mid(Trim(rs_lamp.Fields("lamp_id").Value), 6, 4)) - 1) Mod 4 = 0 Then
                    state_tag = 1
                Else
                    state_tag = 0
                End If

            End If

            If control_method = "1/4闭" Then
                If (Val(Mid(Trim(rs_lamp.Fields("lamp_id").Value), 6, 4)) - 1) Mod 4 = 0 Then
                    state_tag = 0
                End If

            End If
            'rs_lamp.Fields("date").Value = Now
            'rs_lamp.Fields("result").Value = 0 '标志等待返回信息状态
            'rs_lamp.Update()
            sql = "update lamp_inf set power='" & power_tag & "' , state='" & state_tag & "', date='" & Now & "', result=0 where lamp_id='" & Trim(rs_lamp.Fields("lamp_id").Value) & "'"
            DBOperation.ExecuteSQL(conn, sql, msg)
            rs_lamp.MoveNext()
        End While
        rs_lamp.Close()
        rs_lamp = Nothing
        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    ''' 设置单灯查询控制命令
    ''' </summary>
    ''' <param name="lamp_id">灯的编号</param>
    ''' <returns>查询命令字符串</returns>
    ''' <remarks></remarks>
    Public Function Set_control_inf(ByVal lamp_id As String) As String
        Dim rs As New ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim control_box_ox As String  '十六进制的电控箱编号
        Dim lamp_id_ox As String  '十六进制的路灯编号
        Dim ox_str As String  '十六进制字符串
        Dim bin_str As String '十六位长度的二进制，包括5位的类型和11位的灯的独立编号
        Dim conn As New ADODB.Connection
        If DBOperation.OpenConn(conn) = False Then
            Set_control_inf = ""
            Exit Function
        End If

        msg = ""
        sql = "select * from lamp_inf where lamp_id='" & lamp_id & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Set_control_inf" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            Set_control_inf = ""
            conn.Close()
            conn = Nothing
            Exit Function
        End If


        If rs.RecordCount > 0 Then
            bin_str = Com_inf.Dec_to_Bin(Val(Mid(Trim(rs.Fields("lamp_id").Value), 5, 2)), 5) & Com_inf.Dec_to_Bin(Val(Mid(Trim(rs.Fields("lamp_id").Value), 7, LAMP_ID_LEN)), 11)
            ox_str = Com_inf.BIN_to_HEX(bin_str) '16位的二进制转变成四位的十六进制
            lamp_id_ox = Mid(ox_str, 1, 2) & " " & Mid(ox_str, 3, 2)  '路灯编号的16进制位数 4位
            If SYSTEM_VISION = 1 Then
                control_box_ox = Com_inf.Dec_to_Hex(Trim(rs.Fields("control_box_id").Value), 2)  '十进制数转换成2位的十六进制的数
                Set_control_inf = control_box_ox & " " & lamp_id_ox & " 20 13 00"   '轮询的控制命令设置

            Else
                control_box_ox = Com_inf.Dec_to_Hex(Trim(rs.Fields("control_box_id").Value), 4)  '十进制数转换成2位的十六进制的数
                Set_control_inf = Mid(control_box_ox, 3, 2) & " " & lamp_id_ox & " 20 13 00 " & Mid(control_box_ox, 1, 2)   '轮询的控制命令设置

            End If
        Else
            MsgBox("路灯" & lamp_id & "信息不存在！", , PROJECT_TITLE_STRING)
            Set_control_inf = ""

        End If
        rs.Close()
        rs = Nothing

        conn.Close()
        conn = Nothing
    End Function

    ''' <summary>
    ''' 获取路灯或回路的信息，与电控箱区分
    ''' </summary>
    ''' <param name="lamp_tag">路灯编号</param>
    ''' <remarks></remarks>
    Public Function get_lampinf_tip(ByVal lamp_tag As String) As String
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim lamp_tip As String
        Dim lamp_id_string As String
        Dim control_lamp_obj As New control_lamp
        Dim state_inf_tag As Integer
        Dim lamp_num As Integer
        Dim state_inf As String
        Dim lamp_type As String  '灯的类型
        Dim lamp_pointinfor As String = ""

        If DBOperation.OpenConn(conn) = False Then
            get_lampinf_tip = ""
            Exit Function
        End If
        msg = ""
        lamp_tip = ""
        sql = "select * from lamp_street where lamp_id ='" & lamp_tag & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)

        If rs.RecordCount > 0 Then
            If Mid(lamp_tag, 5, 2) = "31" Then  '交流接触器
                state_inf_tag = rs.Fields("state").Value  '根据故障编号，查找路灯的故障信息
                lamp_num = Val(Mid(Trim(rs.Fields("lamp_id").Value), 7, LAMP_ID_LEN))
                lamp_id_string = Val(Mid(Trim(rs.Fields("lamp_id").Value), 1, 4)) & "-" & Val(Mid(Trim(rs.Fields("lamp_id").Value), 5, 2)) & "-" & Val(Mid(Trim(rs.Fields("lamp_id").Value), 7, LAMP_ID_LEN))
                Dim control_string As String

                If state_inf_tag = 0 Or state_inf_tag = 3 Then
                    control_string = Change_Controlboxcontrol(LAMP_STATE_OFF)
                    lamp_tip = Trim("主控箱： " & Trim(rs.Fields("control_box_name").Value) & "第" & lamp_num & "号交流接触器，状态：" & control_string & " 时间：" & rs.Fields("date").Value)
                Else
                    control_string = Change_Controlboxcontrol(LAMP_STATE_ON)
                    lamp_tip = Trim("主控箱： " & Trim(rs.Fields("control_box_name").Value) & "第" & lamp_num & "号交流接触器，状态：" & control_string & " 时间：" & rs.Fields("date").Value)
                End If

            Else '灯的编号
                state_inf_tag = rs.Fields("result").Value  '根据故障编号，查找路灯的故障信息
                If IsDBNull(rs.Fields("lamp_pointinfor").Value) = False Then
                    lamp_pointinfor = rs.Fields("lamp_pointinfor").Value
                    If Trim(lamp_pointinfor) <> "" Then
                        lamp_pointinfor = "  位置:" & lamp_pointinfor
                    Else
                        lamp_pointinfor = ""
                    End If
                Else
                    lamp_pointinfor = ""
                End If
                lamp_num = Val(Mid(Trim(rs.Fields("lamp_id").Value), 7, LAMP_ID_LEN))
                lamp_type = Trim(rs.Fields("jiechuqi_id").Value)  '单灯的类型
                lamp_id_string = Val(Mid(Trim(rs.Fields("lamp_id").Value), 1, 4)) & "-" & Val(Mid(Trim(rs.Fields("lamp_id").Value), 5, 2)) & "-" & Val(Mid(Trim(rs.Fields("lamp_id").Value), 7, LAMP_ID_LEN))
                Com_inf.Get_DengGan(Trim(rs.Fields("lamp_id").Value)) '获取灯杆号及每个灯杆上灯的编号
                If state_inf_tag = 0 Then   '表示灯的状态是正常的
                    If lamp_type = 2 Then
                        If rs.Fields("state").Value = 1 Then
                            lamp_tip = Trim(rs.Fields("street").Value) & " 第" & g_dengzhuid & "号灯杆" & " 第" & g_dengzhulampid & "盏灯" & "(编号：" & lamp_id_string & lamp_pointinfor & ")  状态：" & LAMP_STATE_ON & " 时间：" & rs.Fields("date").Value
                        Else
                            If rs.Fields("state").Value = 0 Then
                                lamp_tip = Trim(rs.Fields("street").Value) & " 第" & g_dengzhuid & "号灯杆" & " 第" & g_dengzhulampid & "盏灯" & "(编号：" & lamp_id_string & lamp_pointinfor & ")  状态：" & LAMP_STATE_OFF & " 时间：" & rs.Fields("date").Value

                            Else
                                If rs.Fields("state").Value = 3 Then
                                    lamp_tip = Trim(rs.Fields("street").Value) & " 第" & g_dengzhuid & "号灯杆" & " 第" & g_dengzhulampid & "盏灯" & "(编号：" & lamp_id_string & lamp_pointinfor & ")  状态：" & LAMP_STATE_OFF & " 时间：" & rs.Fields("date").Value

                                End If
                                If rs.Fields("state").Value = 4 Then
                                    lamp_tip = Trim(rs.Fields("street").Value) & " 第" & g_dengzhuid & "号灯杆" & " 第" & g_dengzhulampid & "盏灯" & "(编号：" & lamp_id_string & lamp_pointinfor & ")  状态：" & LAMP_STATE_ON & " 时间：" & rs.Fields("date").Value

                                End If

                            End If
                        End If
                    Else
                        state_inf = get_probleminf(rs.Fields("state").Value)
                        lamp_tip = Trim(rs.Fields("street").Value) & " 第" & g_dengzhuid & "号灯杆" & " 第" & g_dengzhulampid & "盏灯" & "(编号：" & lamp_id_string & lamp_pointinfor & ")  状态：" & state_inf & " 时间：" & rs.Fields("date").Value

                    End If


                Else
                    If state_inf_tag = 4 Then  '控制状态
                        If rs.Fields("state").Value = 0 Or rs.Fields("state").Value = 3 Then
                            lamp_tip = Trim(rs.Fields("street").Value) & " 第" & g_dengzhuid & "号灯杆" & " 第" & g_dengzhulampid & "盏灯" & "(编号：" & lamp_id_string & lamp_pointinfor & ")  控制状态：" & LAMP_STATE_OFF & " 时间：" & rs.Fields("date").Value

                        End If
                        If rs.Fields("state").Value = 1 Or rs.Fields("state").Value = 4 Then
                            lamp_tip = Trim(rs.Fields("street").Value) & " 第" & g_dengzhuid & "号灯杆" & " 第" & g_dengzhulampid & "盏灯" & "(编号：" & lamp_id_string & lamp_pointinfor & ")  控制状态：" & LAMP_STATE_ON & " 时间：" & rs.Fields("date").Value

                        End If

                    Else  '故障
                        If state_inf_tag = 3 Then
                            '无信息返回状态
                            lamp_tip = Trim(rs.Fields("street").Value) & " 第" & g_dengzhuid & "号灯杆" & " 第" & g_dengzhulampid & "盏灯" & "(编号：" & lamp_id_string & lamp_pointinfor & ")  控制状态：" & LAMP_STATE_NORETURN & " 时间：" & rs.Fields("date").Value
                        Else
                            If lamp_type = 2 Then
                                If state_inf_tag = 1 Then  '该亮非亮
                                    lamp_tip = Trim(rs.Fields("street").Value) & " 第" & g_dengzhuid & "号灯杆" & " 第" & g_dengzhulampid & "盏灯" & "(编号：" & lamp_id_string & lamp_pointinfor & ")  状态：" & LAMP_STATE_PROBLEM_ON & " 时间：" & rs.Fields("date").Value

                                Else
                                    If state_inf_tag = 2 Then  '该暗非暗
                                        lamp_tip = Trim(rs.Fields("street").Value) & " 第" & g_dengzhuid & "号灯杆" & " 第" & g_dengzhulampid & "盏灯" & "(编号：" & lamp_id_string & lamp_pointinfor & ")  状态：" & LAMP_STATE_PROBLEM_OFF & " 时间：" & rs.Fields("date").Value
                                    Else   '无返回值
                                        lamp_tip = Trim(rs.Fields("street").Value) & " 第" & g_dengzhuid & "号灯杆" & " 第" & g_dengzhulampid & "盏灯" & "(编号：" & lamp_id_string & lamp_pointinfor & ")  状态：" & LAMP_STATE_NORETURN & " 时间：" & rs.Fields("date").Value

                                    End If '无返回值与该暗非暗
                                End If '该亮非亮
                            Else
                                state_inf = get_probleminf(rs.Fields("state").Value)
                                lamp_tip = Trim(rs.Fields("street").Value) & " 第" & g_dengzhuid & "号灯杆" & " 第" & g_dengzhulampid & "盏灯" & "(编号：" & lamp_id_string & lamp_pointinfor & ")  状态：" & state_inf & " 时间：" & rs.Fields("date").Value

                            End If

                        End If


                    End If '故障
                End If  '控制状态
                End If '正常


        End If
        get_lampinf_tip = lamp_tip  '标签显示
        rs.Close()
        rs = Nothing
        conn.Close()
        conn = Nothing
    End Function

    ''' <summary>
    ''' 获取路灯的实时状态，用不同的颜色标识不同的状态，并且统计故障率，亮灯率的信息
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub get_lamp_information()
        '获取路灯的状况信息
        Dim rs_lamp As New ADODB.Recordset
        Dim lamp_on, lamp_on_total As Integer   '每条路亮的路灯数量，及总的亮灯数量
        Dim lamp_off, lamp_off_total As Integer  '每条路暗的路灯数量
        Dim lamp_problem, lamp_problem_total As Integer '每条路的故障路灯数量，及总的故障数量
        Dim lamp_no_return_value, lamp_no_return_value_total As Integer  '每条路的无返回状态路灯数量，总的无返回状态灯数量
        Dim lamp_total_num, lamp_total_num_total As Integer   '每条路的路灯总数，及所有路灯的总数
        Dim lamp_on_lv, lamp_on_lv_total As Double  '每条路的亮的灯率,总的亮灯率
        Dim lamp_off_lv, lamp_off_lv_total As Double  '暗的灯率,总的暗灯率
        Dim lamp_problem_lv, lamp_problem_lv_total As Double  '故障率,总的故障率
        Dim lamp_no_return_lv, lamp_no_return_lv_total As Double  '无返回状态率，总的无返回状态率
        Dim control_box_string As String  '电控箱名称
        Dim type_string As String  '景观灯类型的名称
        Dim sql As String   'sql语句
        Dim msg As String  '提示sql语句的执行结果
        Dim control_box_problem_lv As Double   '一个电控箱下的故障率
        Dim pos_lamp As New System.Drawing.Point  '绘制路灯
        Dim lamp_kind As Integer  '单灯的类型 3类型表示新的五个字节状态：电压，电流，功率
        'Dim row As Integer   '区域状态的行数

        Dim conn As New ADODB.Connection

        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If

        control_box_problem_lv = 0.0 '故障率清零
        lamp_on = 0   '将亮灯的数量清零
        lamp_on_total = 0  '总的亮灯率
        lamp_off = 0  '将暗灯的数量清零
        lamp_off_total = 0 '将总的暗灯率清零
        lamp_problem = 0  '将故障的数量清零
        lamp_problem_total = 0  '总的故障率
        lamp_total_num = 0  '将一条路总体的数量清零
        lamp_total_num_total = 0 '总体灯的数量清零
        lamp_no_return_value = 0  '将无返回值的数量清零
        lamp_no_return_value_total = 0  '将无返回值的总体数量清零
        lamp_off_lv = 0  '关灯率
        lamp_off_lv_total = 0 '总的关灯率

        '  row = 0
        'm_fullcolor = g_color(0) '全功率灯，如果没有设置，则使用默认颜色
        'm_noreturncolor = g_color(1) '无返回值灯，如果没有设置，则使用默认颜色
        'm_closecolor = g_color(2) '暗灯，如果没有设置，则使用默认颜色
        'm_problemcolor = g_color(3) '故障灯，如果没有设置，则使用默认颜色
        'm_partcolor = g_color(4) '半功率灯，如果没有设置，则使用默认颜色


        g_lampmap.Clear(Color.Empty)  '清空灯的图

        sql = "select * from lamp_street where map_id='" & g_choosemapid & "' order by lamp_id"
        msg = ""
        rs_lamp = DBOperation.SelectSQL(conn, sql, msg)
        If rs_lamp Is Nothing Then   '数据库连接失败
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "get_lamp_information" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs_lamp.RecordCount <= 0 Then  '该地图所控制的范围内没有路灯信息
            rs_lamp.Close()
            rs_lamp = Nothing
            conn.Close()
            conn = Nothing
            Exit Sub
        End If

        control_box_string = Trim(rs_lamp.Fields("control_box_name").Value)  '电控箱名称
        type_string = Trim(rs_lamp.Fields("type_string").Value)  '景观灯类型
        While rs_lamp.EOF = False
            If rs_lamp.Fields("jiechuqi_id").Value Is System.DBNull.Value Then
                lamp_kind = 2   '默认为两个字节的
            Else
                lamp_kind = rs_lamp.Fields("jiechuqi_id").Value   '单灯的类型
            End If

            If control_box_string = Trim(rs_lamp.Fields("control_box_name").Value) And type_string = Trim(rs_lamp.Fields("type_string").Value) Then
                '表示同一个电控箱下的同一种类型的景观灯

                pos_lamp.X = rs_lamp.Fields("pos_x").Value  '路灯的X坐标
                pos_lamp.Y = rs_lamp.Fields("pos_y").Value  '路灯的Y坐标

                If lamp_kind = 3 Then
                    '六个字节的状态
                    If rs_lamp.Fields("result").Value = 0 And rs_lamp.Fields("state").Value = 0 Then
                        '表示状态正常,暗
                        lamp_off += 1   '暗的路灯数量加1
                        lamp_total_num += 1   '路灯总量加1
                        DrawLamp(g_closecolor, m_lampwide, m_lampheight, pos_lamp.X, pos_lamp.Y, Trim(rs_lamp.Fields("lamp_id").Value), False)
                        GoTo nextstp

                    End If

                    If rs_lamp.Fields("result").Value = 0 And (rs_lamp.Fields("state").Value = 8 Or rs_lamp.Fields("state").Value = 12) Then
                        '表示状态正常，亮
                        lamp_on += 1   '亮的路灯数量加1，全功率
                        lamp_total_num += 1   '路灯总量加1
                        DrawLamp(g_fullcolor, m_lampwide, m_lampheight, pos_lamp.X, pos_lamp.Y, Trim(rs_lamp.Fields("lamp_id").Value), False)
                        GoTo nextstp
                    End If

                    If rs_lamp.Fields("result").Value = 1 Then
                        '表示故障
                        lamp_problem += 1   '故障路灯
                        lamp_total_num += 1   '路灯总量加1
                        DrawLamp(g_problemcolor, m_lampwide, m_lampheight, pos_lamp.X, pos_lamp.Y, Trim(rs_lamp.Fields("lamp_id").Value), True)
                        GoTo nextstp
                    End If
                    If rs_lamp.Fields("result").Value = 3 Then
                        lamp_no_return_value += 1  '无返回状态的数量加1
                        lamp_total_num += 1   '路灯总量加1
                        DrawLamp(g_noreturncolor, m_lampwide, m_lampheight, pos_lamp.X, pos_lamp.Y, Trim(rs_lamp.Fields("lamp_id").Value), False)
                        GoTo nextstp
                    End If
                    If rs_lamp.Fields("result").Value = 4 And rs_lamp.Fields("state").Value = 4 Then
                        '控制状态等待状态返回
                        lamp_on += 1   '亮的路灯数量加1
                        lamp_total_num += 1   '路灯总量加1
                        DrawLamp(g_fullcolor, m_lampwide, m_lampheight, pos_lamp.X, pos_lamp.Y, Trim(rs_lamp.Fields("lamp_id").Value), False)
                        GoTo nextstp
                    End If
                    If rs_lamp.Fields("result").Value = 4 And rs_lamp.Fields("state").Value = 3 Then
                        '控制状态等待状态返回
                        lamp_off += 1   '暗的路灯数量加1
                        lamp_total_num += 1   '路灯总量加1
                        DrawLamp(g_closecolor, m_lampwide, m_lampheight, pos_lamp.X, pos_lamp.Y, Trim(rs_lamp.Fields("lamp_id").Value), False)
                        GoTo nextstp
                    End If

                Else

                    If rs_lamp.Fields("state").Value = 1 And rs_lamp.Fields("result").Value = 0 Then
                        lamp_on += 1   '亮的路灯数量加1，全功率
                        lamp_total_num += 1   '路灯总量加1
                        DrawLamp(g_fullcolor, m_lampwide, m_lampheight, pos_lamp.X, pos_lamp.Y, Trim(rs_lamp.Fields("lamp_id").Value), False)
                        GoTo nextstp

                    End If
                    'If rs_lamp.Fields("state").Value = 1 And rs_lamp.Fields("result").Value = 0 And rs_lamp.Fields("power").Value = 50 Then
                    '    lamp_on += 1   '亮的路灯数量加1,半功率
                    '    lamp_total_num += 1   '路灯总量加1
                    '    DrawLamp(g_partcolor, m_lampwide, m_lampheight, pos_lamp.X, pos_lamp.Y, Trim(rs_lamp.Fields("lamp_id").Value), False)
                    '    GoTo nextstp
                    'End If
                    If rs_lamp.Fields("state").Value = 0 And rs_lamp.Fields("result").Value = 0 Then
                        lamp_off += 1   '暗的路灯数量加1
                        lamp_total_num += 1   '路灯总量加1
                        DrawLamp(g_closecolor, m_lampwide, m_lampheight, pos_lamp.X, pos_lamp.Y, Trim(rs_lamp.Fields("lamp_id").Value), False)
                        GoTo nextstp
                    End If
                    If (rs_lamp.Fields("result").Value = 1 Or rs_lamp.Fields("result").Value = 2) And rs_lamp.Fields("total_num").Value > 1 Then
                        lamp_problem += 1   '故障路灯:该亮非亮或该暗非暗
                        lamp_total_num += 1   '路灯总量加1
                        DrawLamp(g_problemcolor, m_lampwide, m_lampheight, pos_lamp.X, pos_lamp.Y, Trim(rs_lamp.Fields("lamp_id").Value), True)
                        GoTo nextstp
                    End If

                    If rs_lamp.Fields("result").Value = 3 Then
                        lamp_no_return_value += 1  '无返回状态的数量加1
                        lamp_total_num += 1   '路灯总量加1
                        DrawLamp(g_noreturncolor, m_lampwide, m_lampheight, pos_lamp.X, pos_lamp.Y, Trim(rs_lamp.Fields("lamp_id").Value), False)
                        GoTo nextstp
                    End If
                    If rs_lamp.Fields("result").Value = 4 And rs_lamp.Fields("state").Value = 3 Then
                        '控制状态等待状态返回
                        lamp_off += 1   '暗的路灯数量加1
                        lamp_total_num += 1   '路灯总量加1
                        DrawLamp(g_closecolor, m_lampwide, m_lampheight, pos_lamp.X, pos_lamp.Y, Trim(rs_lamp.Fields("lamp_id").Value), False)
                        GoTo nextstp
                    End If
                    If rs_lamp.Fields("result").Value = 4 And rs_lamp.Fields("state").Value = 4 Then
                        '控制状态等待状态返回
                        lamp_on += 1   '亮的路灯数量加1
                        lamp_total_num += 1   '路灯总量加1
                        DrawLamp(g_fullcolor, m_lampwide, m_lampheight, pos_lamp.X, pos_lamp.Y, Trim(rs_lamp.Fields("lamp_id").Value), False)
                        GoTo nextstp
                    End If

                    '其他情况,灰色表示
                    DrawLamp(Color.Gray, m_lampwide, m_lampheight, pos_lamp.X, pos_lamp.Y, Trim(rs_lamp.Fields("lamp_id").Value), False)

                End If


nextstp:

                rs_lamp.MoveNext()
            Else
                '总的灯的信息统计
                lamp_total_num_total += lamp_total_num
                lamp_on_total += lamp_on
                lamp_off_total += lamp_off
                lamp_problem_total += lamp_problem
                lamp_no_return_value_total += lamp_no_return_value

                If lamp_total_num <> 0 Then  '如果路灯总数不为0
                    lamp_on_lv = Format(lamp_on / lamp_total_num * 100, "00.00")  '亮灯率
                    lamp_off_lv = Format(lamp_off / lamp_total_num * 100, "00.00") '暗灯率
                    lamp_problem_lv = Format(lamp_problem / lamp_total_num * 100, "00.00")  '故障率
                    lamp_no_return_lv = 100 - lamp_on_lv - lamp_off_lv - lamp_problem_lv
                Else
                    lamp_on_lv = 0   '路灯总数为0，亮灯率为0
                    lamp_off_lv = 0 '灯的总数为0 ，暗灯率为0
                    lamp_problem_lv = 0  '路灯总数为0，故障率为0
                    lamp_no_return_lv = 0  '路灯总数为0，无返回状态率为0

                End If

                lamp_on = 0
                lamp_off = 0
                lamp_problem = 0
                lamp_no_return_value = 0
                lamp_total_num = 0

                pos_lamp.X = rs_lamp.Fields("pos_x").Value  '路灯的X坐标
                pos_lamp.Y = rs_lamp.Fields("pos_y").Value  '路灯的Y坐标

                If lamp_kind = 3 Then

                    '六个字节的状态
                    If rs_lamp.Fields("result").Value = 0 And rs_lamp.Fields("state").Value = 0 Then
                        '表示状态正常,暗
                        lamp_off += 1   '暗的路灯数量加1
                        lamp_total_num += 1   '路灯总量加1
                        DrawLamp(g_closecolor, m_lampwide, m_lampheight, pos_lamp.X, pos_lamp.Y, Trim(rs_lamp.Fields("lamp_id").Value), False)
                        GoTo nextstp2

                    End If

                    If rs_lamp.Fields("result").Value = 0 And rs_lamp.Fields("state").Value = 8 Then
                        '表示状态正常，亮
                        lamp_on += 1   '亮的路灯数量加1，全功率
                        lamp_total_num += 1   '路灯总量加1
                        DrawLamp(g_fullcolor, m_lampwide, m_lampheight, pos_lamp.X, pos_lamp.Y, Trim(rs_lamp.Fields("lamp_id").Value), False)
                        GoTo nextstp2
                    End If

                    If rs_lamp.Fields("result").Value = 1 Then
                        '表示故障
                        lamp_problem += 1   '故障路灯
                        lamp_total_num += 1   '路灯总量加1
                        DrawLamp(g_problemcolor, m_lampwide, m_lampheight, pos_lamp.X, pos_lamp.Y, Trim(rs_lamp.Fields("lamp_id").Value), True)
                        GoTo nextstp2
                    End If
                    If rs_lamp.Fields("result").Value = 3 Then
                        lamp_no_return_value += 1  '无返回状态的数量加1
                        lamp_total_num += 1   '路灯总量加1
                        DrawLamp(g_noreturncolor, m_lampwide, m_lampheight, pos_lamp.X, pos_lamp.Y, Trim(rs_lamp.Fields("lamp_id").Value), False)
                        GoTo nextstp2
                    End If
                    If rs_lamp.Fields("result").Value = 4 And rs_lamp.Fields("state").Value = 4 Then
                        '控制状态等待状态返回
                        lamp_on += 1   '亮的路灯数量加1
                        lamp_total_num += 1   '路灯总量加1
                        DrawLamp(g_fullcolor, m_lampwide, m_lampheight, pos_lamp.X, pos_lamp.Y, Trim(rs_lamp.Fields("lamp_id").Value), False)
                        GoTo nextstp2
                    End If
                    If rs_lamp.Fields("result").Value = 4 And rs_lamp.Fields("state").Value = 3 Then
                        '控制状态等待状态返回
                        lamp_off += 1   '暗的路灯数量加1
                        lamp_total_num += 1   '路灯总量加1
                        DrawLamp(g_closecolor, m_lampwide, m_lampheight, pos_lamp.X, pos_lamp.Y, Trim(rs_lamp.Fields("lamp_id").Value), False)
                        GoTo nextstp2
                    End If

                Else

                    If rs_lamp.Fields("state").Value = 1 And rs_lamp.Fields("result").Value = 0 Then
                        lamp_on += 1   '亮的路灯数量加1，全功率
                        lamp_total_num += 1   '路灯总量加1
                        DrawLamp(g_fullcolor, m_lampwide, m_lampheight, pos_lamp.X, pos_lamp.Y, Trim(rs_lamp.Fields("lamp_id").Value), False)
                        GoTo nextstp2

                    End If
                    'If rs_lamp.Fields("state").Value = 1 And rs_lamp.Fields("result").Value = 0 And rs_lamp.Fields("power").Value = 50 Then
                    '    lamp_on += 1   '亮的路灯数量加1,半功率
                    '    lamp_total_num += 1   '路灯总量加1
                    '    DrawLamp(g_partcolor, m_lampwide, m_lampheight, pos_lamp.X, pos_lamp.Y, Trim(rs_lamp.Fields("lamp_id").Value), False)
                    '    GoTo nextstp
                    'End If
                    If rs_lamp.Fields("state").Value = 0 And rs_lamp.Fields("result").Value = 0 Then
                        lamp_off += 1   '暗的路灯数量加1
                        lamp_total_num += 1   '路灯总量加1
                        DrawLamp(g_closecolor, m_lampwide, m_lampheight, pos_lamp.X, pos_lamp.Y, Trim(rs_lamp.Fields("lamp_id").Value), False)
                        GoTo nextstp2
                    End If
                    If (rs_lamp.Fields("result").Value = 1 Or rs_lamp.Fields("result").Value = 2) And rs_lamp.Fields("total_num").Value > 1 Then
                        lamp_problem += 1   '故障路灯:该亮非亮或该暗非暗
                        lamp_total_num += 1   '路灯总量加1
                        DrawLamp(g_problemcolor, m_lampwide, m_lampheight, pos_lamp.X, pos_lamp.Y, Trim(rs_lamp.Fields("lamp_id").Value), True)
                        GoTo nextstp2
                    End If

                    If rs_lamp.Fields("result").Value = 3 Then
                        lamp_no_return_value += 1  '无返回状态的数量加1
                        lamp_total_num += 1   '路灯总量加1
                        DrawLamp(g_noreturncolor, m_lampwide, m_lampheight, pos_lamp.X, pos_lamp.Y, Trim(rs_lamp.Fields("lamp_id").Value), False)
                        GoTo nextstp2
                    End If
                    If rs_lamp.Fields("result").Value = 4 And rs_lamp.Fields("state").Value = 3 Then
                        '控制状态等待状态返回
                        lamp_off += 1   '暗的路灯数量加1
                        lamp_total_num += 1   '路灯总量加1
                        DrawLamp(g_closecolor, m_lampwide, m_lampheight, pos_lamp.X, pos_lamp.Y, Trim(rs_lamp.Fields("lamp_id").Value), False)
                        GoTo nextstp2
                    End If
                    If rs_lamp.Fields("result").Value = 4 And rs_lamp.Fields("state").Value = 4 Then
                        '控制状态等待状态返回
                        lamp_on += 1   '亮的路灯数量加1
                        lamp_total_num += 1   '路灯总量加1
                        DrawLamp(g_fullcolor, m_lampwide, m_lampheight, pos_lamp.X, pos_lamp.Y, Trim(rs_lamp.Fields("lamp_id").Value), False)
                        GoTo nextstp2
                    End If

                    '其他情况,灰色表示
                    DrawLamp(Color.Gray, m_lampwide, m_lampheight, pos_lamp.X, pos_lamp.Y, Trim(rs_lamp.Fields("lamp_id").Value), False)
                End If
nextstp2:

                rs_lamp.MoveNext()
                If rs_lamp.EOF = False Then
                    control_box_string = Trim(rs_lamp.Fields("control_box_name").Value)  '电控箱的名称
                    type_string = Trim(rs_lamp.Fields("type_string").Value)  '灯的类型
                End If
            End If

        End While


        '总的灯的信息统计
        lamp_total_num_total += lamp_total_num
        lamp_on_total += lamp_on
        lamp_off_total += lamp_off
        lamp_problem_total += lamp_problem
        lamp_no_return_value_total += lamp_no_return_value

        If lamp_total_num <> 0 Then  '如果路灯总数不为0
            lamp_on_lv = Format(lamp_on / lamp_total_num * 100, "00.00")  '亮灯率
            lamp_off_lv = Format(lamp_off / lamp_total_num * 100, "00.00") '暗灯率
            lamp_problem_lv = Format(lamp_problem / lamp_total_num * 100, "00.00")  '故障率
            lamp_no_return_lv = 100 - lamp_on_lv - lamp_off_lv - lamp_problem_lv

        Else
            lamp_on_lv = 0   '路灯总数为0，亮灯率为0
            lamp_off_lv = 0 '灯的总数为0 ，暗灯率为0
            lamp_problem_lv = 0  '路灯总数为0，故障率为0
            lamp_no_return_lv = 0  '路灯总数为0，无返回状态率为0

        End If
        control_box_problem_lv = lamp_problem_lv
        control_box_problem_lv = 0

        If lamp_total_num <> 0 Then  '如果路灯总数不为0
            lamp_on_lv = lamp_on / lamp_total_num * 100  '亮灯率
            lamp_off_lv = lamp_off / lamp_total_num * 100 '暗灯率
            lamp_problem_lv = lamp_problem / lamp_total_num * 100  '故障率
            lamp_no_return_lv = lamp_no_return_value / lamp_total_num * 100 '无返回状态率

        Else
            lamp_on_lv = 0  '灯总数为0，亮灯率为0
            lamp_off_lv = 0 '灯总数为0，暗灯率为0
            lamp_problem_lv = 0  '灯总数为0，故障率为0
            lamp_no_return_lv = 0 '灯总数为0，无返回状态率为0

        End If

        If lamp_total_num_total <> 0 Then

            lamp_on_lv_total = Format(lamp_on_total / lamp_total_num_total * 100, "00.00")  '总的亮灯率
            lamp_off_lv_total = Format(lamp_off_total / lamp_total_num_total * 100, "00.00") '总的关灯率
            lamp_problem_lv_total = Format(lamp_problem_total / lamp_total_num_total * 100, "00.00") '总的故障率
            lamp_no_return_lv_total = 100 - lamp_on_lv_total - lamp_off_lv_total - lamp_problem_lv_total '总的无返回状态率
        Else
            lamp_on_lv_total = 0
            lamp_off_lv_total = 0
            lamp_problem_lv_total = 0
            lamp_no_return_lv_total = 0
        End If

        '最后一个电控箱下的类型的信息统计
        g_welcomewinobj.SetTextDelegate(Format(lamp_on_lv_total, "0.00") & "%", False, g_welcomewinobj.lb_light_on_lv) '亮灯率为0
        g_welcomewinobj.SetTextDelegate(Format(lamp_off_lv_total, "0.00") & "%", False, g_welcomewinobj.lb_light_off_lv) '亮灯率为0

        g_welcomewinobj.SetTextDelegate(Format(lamp_problem_lv_total, "0.00") & "%", False, g_welcomewinobj.lb_problem_lv) '故障率
        g_welcomewinobj.SetTextDelegate(Format(lamp_no_return_lv_total, "0.00") & "%", False, g_welcomewinobj.lb_no_return_lv) '无返回值率为0

        rs_lamp.Close()
        rs_lamp = Nothing
        conn.Close()
        conn = Nothing

    End Sub

    ''' <summary>
    ''' 绘制路灯
    ''' </summary>
    ''' <param name="color">灯的颜色</param>
    ''' <param name="wide">灯的宽度</param>
    ''' <param name="height">灯的高度</param>
    ''' <param name="pos_x">X坐标</param>
    ''' <param name="pos_y">Y坐标</param>
    ''' <remarks></remarks>
    Public Sub DrawLamp(ByVal color As Color, ByVal wide As Integer, ByVal height As Integer, ByVal pos_x As Integer, ByVal pos_y As Integer, ByVal lamp_id As String, ByVal problem_tag As Boolean)
        Dim myBrush As New SolidBrush(color)
        Dim myPen As New Pen(Drawing.Color.Gold, 4)
        Dim point(1) As System.Drawing.Point
        Dim myPen2 As New Pen(Drawing.Color.Gold, 4)
        Dim myBrush2 As New SolidBrush(color)

        point(0).X = pos_x
        point(0).Y = pos_y
        While g_lampdrawtag = True

        End While
        If Mid(lamp_id, 5, 2) = "31" Then
            '表示类型为回路控制
            g_lampmap.DrawRectangle(myPen, pos_x, pos_y, wide, height)
            g_lampmap.FillRectangle(myBrush, pos_x, pos_y, wide, height)

        Else
            If problem_tag = False Then
                '正常工作的灯
                g_lampmap.DrawEllipse(myPen, pos_x, pos_y, wide, height)
                g_lampmap.FillEllipse(myBrush, pos_x, pos_y, wide, height)
            Else
                If m_alarmlinetag Mod 2 = 0 Then
                    g_lampmap.DrawEllipse(myPen2, pos_x, pos_y, wide + 5, height + 5)
                    g_lampmap.FillEllipse(myBrush2, pos_x, pos_y, wide + 5, height + 5)
                Else
                    g_lampmap.DrawEllipse(myPen, pos_x, pos_y, wide, height)
                    g_lampmap.FillEllipse(myBrush, pos_x, pos_y, wide, height)
                End If

            End If

        End If

        myBrush.Dispose()
        myPen.Dispose()
        myPen2.Dispose()
        myPen2 = Nothing
        myPen = Nothing
        myBrush = Nothing
    End Sub

    ''' <summary>
    ''' 对单灯的开关操作
    ''' </summary>
    ''' <param name="box_name">区域名称</param>
    ''' <param name="lamp_type">类型</param>
    ''' <param name="lamp_id">灯的编号</param>
    ''' <param name="open_close">0表示关，1表示开</param>
    ''' <remarks></remarks>
    Public Sub open_close_single_lamp(ByVal box_name As String, ByVal lamp_type As String, ByVal lamp_id As String, ByVal open_close As Integer, ByVal diangan As String, ByVal control_time As Integer, ByVal row_id As Integer)
        Dim power_string As String
        Dim control_lamp_obj As New control_lamp

        '将景观灯的类型和编号组合成四位的十六进制
        Dim rs As ADODB.Recordset
        Dim sql As String
        Dim msg As String
        Dim type_id As Integer   '灯类型的编号
        Dim lamp_id_bin As String  '灯的编号的十六位二进制
        Dim control_box_id As String  '电控箱编号
        Dim conn As New ADODB.Connection
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If


        power_string = "100"
        msg = ""
        sql = "select * from control_box where control_box_name='" & box_name & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "open_close_single_lamp" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs.RecordCount <= 0 Then
            MsgBox("不存在该区域编号,请重新选择", , PROJECT_TITLE_STRING)
            rs.Close()
            rs = Nothing
            conn.Close()
            conn = Nothing
            Exit Sub
        Else
            control_box_id = Trim(rs.Fields("control_box_id").Value)
        End If

        sql = "select * from lamp_type where type_string='" & lamp_type & "'"

        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "open_close_single_lamp" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            Exit Sub
        End If

        If rs.RecordCount <= 0 Then
            MsgBox("不存在该类型的灯，请重新选择", , PROJECT_TITLE_STRING)
            rs.Close()
            rs = Nothing
            conn.Close()
            conn = Nothing
            Exit Sub
        Else
            type_id = rs.Fields("type_id").Value
        End If

        rs.Close()
        rs = Nothing
        lamp_id_bin = Com_inf.Dec_to_Bin(type_id, 5) & Com_inf.Dec_to_Bin(Mid(lamp_id, 7, LAMP_ID_LEN), 11) '十六位长度的路灯编号二进制
        If open_close = 1 Then  '单灯开

            '打开路灯操作
            open_light_single(control_box_id, lamp_id_bin, lamp_id, diangan, power_string, control_time, row_id)
        End If
        If open_close = 0 Then
            '关闭路灯
            close_light_single(control_box_id, lamp_id_bin, lamp_id, diangan, "0", control_time, row_id)
        End If


        conn.Close()
        conn = Nothing

    End Sub

    ''' <summary>
    ''' 根据回路的编号将某一个电控箱下面的回路灯全部设置为开
    ''' </summary>
    ''' <param name="lamp_num">灯杆上的灯的编号1，2，3号</param>
    ''' <param name="control_box_id">电控箱编号</param>
    ''' <remarks></remarks>
    Public Sub open_close_huilulamp(ByVal open As Integer, ByVal lamp_num As Integer, ByVal control_box_id As String)
        ' Dim rs As New ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim conn As New ADODB.Connection

        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        msg = ""
        If lamp_num = g_lampnum Then
            lamp_num = 0
        End If
        If open = 1 Then  '打开
            sql = "update lamp_inf set div_time_id=1, state=4, result=4 where CAST(SUBSTRING(lamp_id, 7, " & LAMP_ID_LEN & ") AS int) % '" & g_lampnum & "' = '" & lamp_num & "' and control_box_id='" & control_box_id & "' and lamp_type_id=0"
        Else  '关闭
            '2011年11月15日，经纬度关灯后自动将电流,电压，功率状态置为0
            sql = "update lamp_inf set div_time_id=0, state=3, result=4,current_l=0,presure_l=0,power=0 where CAST(SUBSTRING(lamp_id, 7, " & LAMP_ID_LEN & ") AS int) % '" & g_lampnum & "' = '" & lamp_num & "' and control_box_id='" & control_box_id & "' and lamp_type_id=0"

        End If
        DBOperation.ExecuteSQL(conn, sql, msg)

        conn.Close()
        conn = Nothing

    End Sub

    ''' <summary>
    ''' 根据三回路的命令设置lamp_inf表中的状态
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Setgrouporder_lamp_state(ByVal control_box_id As String, ByVal method As String)

        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim id As Integer = 0
        Dim state_tag As Integer = 0 '灯的状态标志，0表示关，1表示开

        msg = ""
        sql = "select * from lamp_inf where control_box_id='" & control_box_id & "' and lamp_type_id=0"
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            ' g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Setgrouporder_lamp_state" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            Exit Sub
        End If

        While rs.EOF = False
            If g_lampnum = 3 Then
                id = (Val(Mid(Trim(rs.Fields("lamp_id").Value), 7, LAMP_ID_LEN)) - 1) Mod 12 + 1
            Else
                id = (Val(Mid(Trim(rs.Fields("lamp_id").Value), 7, LAMP_ID_LEN)) - 1) Mod 8 + 1
            End If

            state_tag = Val(Mid(method, id, 1)) + 3
            sql = "update lamp_inf set state='" & state_tag & "', result=4 where lamp_id='" & Trim(rs.Fields("lamp_id").Value) & "'"
            DBOperation.ExecuteSQL(conn, sql, msg)

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
    ''' 获取一个主控箱下所有接触器的编号,形式为00013100001 00013100002
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function get_jiechuqi_id(ByVal control_box_name As String, ByVal lampid_string As String) As String
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String
        Dim id_string As String = ""
        Dim lampid() As String
        Dim i As Integer = 0
        Dim control_box_id As String

        lampid = lampid_string.Split(" ")
        msg = ""
        DBOperation.OpenConn(conn)
        ' sql = "select lamp_id from lamp_street where control_box_name='" & control_box_name & "' and type_id=31 order by lamp_id"
        sql = "select control_box_id from control_box where control_box_name='" & control_box_name & "'"

        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Setgrouporder_lamp_state" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            get_jiechuqi_id = ""
            Exit Function
        End If
        If rs.RecordCount > 0 Then
            control_box_id = Trim(rs.Fields("control_box_id").Value)
            While i < lampid.Length
                id_string &= control_box_id & "310000" & lampid(i) & " "
                i += 1
            End While

        End If


        get_jiechuqi_id = id_string

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing

    End Function

    ''' <summary>
    ''' 自动关闭手工开放的灯
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub auto_close()
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim order_string As String '控制命令

        msg = ""
        DBOperation.OpenConn(conn)
        sql = "select * from TimeControl where CMDType='" & HG_TYPE.HG_AUTO_CLOSE_ORDER & "' and CreateTime<='" & Now & "' order by id"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Setgrouporder_lamp_state" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        While rs.EOF = False
            order_string = Trim(rs.Fields("CMDContent").Value)
            sql = "insert into RoadLightControl(ControlContent, HandlerFlag, Createtime) values('" & order_string & "',0,'" & Now & "')"
            DBOperation.ExecuteSQL(conn, sql, msg)

            '解析出需要设置关灯的状态的灯的编号
            auto_changelampstate(order_string)
          
            '删除关闭灯的命令
            sql = "delete TimeControl where ID=" & rs.Fields("ID").Value
            DBOperation.ExecuteSQL(conn, sql, msg)

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
    ''' 根据发送的关灯命令，判断需要关灯的灯的编号，并将相应的lamp_inf表中的单灯编号的状态置为3
    ''' </summary>
    ''' <param name="order_string"></param>
    ''' <remarks></remarks>
    Private Sub auto_changelampstate(ByVal order_string As String)
        Dim inf_list() As String
        Dim control_box_id As String '主控箱编号
        Dim type_id As String '类型编号
        Dim lamp_id As String '单灯编号
        Dim conn As New ADODB.Connection

        Dim sql As String
        Dim msg As String = ""


        inf_list = order_string.Split(" ")
        DBOperation.OpenConn(conn)

        control_box_id = Val("&H" & (inf_list(6) & inf_list(0))).ToString   '第7个字节并上第1个字节转换为四位的十进制电控箱编号
        While control_box_id.Length < 4
            control_box_id = "0" & control_box_id '电控箱编号不足4位用0补充
        End While

        lamp_id = control_box_id & Control_Hex_to_Dec(inf_list(1) & inf_list(2))   '第2和3个字节转换成2+LAMP_ID_LEN长度的路灯编号
        type_id = Val(Mid(lamp_id, 5, 2))

        If inf_list(3) = "1C" Then
            '单灯关
            sql = "update lamp_inf set state=3, result=4 where lamp_id='" & lamp_id & "'"
        Else
            If inf_list(3) = "42" Then
                '类型关
                sql = "update lamp_inf set state=3, result=4 where control_box_id='" & control_box_id & "' and lamp_type_id='" & type_id & "'"

            Else
                If inf_list(3) = "12" Then
                    '主控箱级别关
                    sql = "update lamp_inf set state=3, result=4 where control_box_id='" & control_box_id & "'"
                Else
                    sql = ""
                End If
            End If
        End If

        If sql <> "" Then
            DBOperation.ExecuteSQL(conn, sql, msg)

        End If

        conn.Close()
        conn = Nothing

    End Sub

    '插入整点区间命令 
    Public Sub get_lampinf()
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim sql_return As String
        Dim rs_return As New ADODB.Recordset
        Dim msg As String
        Dim rs As New ADODB.Recordset
        Dim m As Integer = 0
        Dim imei As String
        Dim inter_count As Integer = 0
        Dim inter_command As String = ""
        Dim time As DateTime = Now
        Dim box_id As String
        Dim controlboxobj As New control_box
        Dim state() As String
        Dim lamp_protocle_type As String '单灯的协议类型1，2,6
        Dim qujiannum As Integer
        Dim controlboxname As String
        msg = ""
        sql = "SELECT B.control_box_name AS control_box_name,B.control_box_id AS control_box_id,B.inter_count AS inter_count,A.IMEI AS IMEI FROM control_box AS B LEFT JOIN Box_IMEI AS A ON B.control_box_id=A.control_box_id"
        DBOperation.OpenConn(conn)
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING, , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
        End If
        If rs.RecordCount > 0 Then
            '有单灯数据
            While rs.EOF = False
                box_id = Trim(rs.Fields("control_box_id").Value)
                box_id = Com_inf.Dec_to_Hex(box_id, 4)
                box_id = Mid(box_id, 1, 2) & " " & Mid(box_id, 3, 2)
                controlboxname = Trim(rs.Fields("control_box_name").Value)
                If IsDBNull(rs.Fields("inter_count").Value) = False Then
                    If controlboxobj.get_communication(controlboxname) = False Then
                        '通信不正常则不测
                        g_welcomewinobj.SetTextDelegate("主控箱" & controlboxname & "未连接无法巡查 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, True, g_welcomewinobj.rtb_info_list)
                        System.Threading.Thread.Sleep(1000)
                    Else
                        If IsDBNull(rs.Fields("inter_count").Value) = False Then
                            inter_count = Trim(rs.Fields("inter_count").Value)
                            imei = Trim(rs.Fields("IMEI").Value)
                            Dim i As Integer = 1
                            While (i <= inter_count)
                                inter_command = box_id + " " + Com_inf.Dec_to_Hex(i, 2)
                                qujiannum = get_qujian(Trim(rs.Fields("control_box_id").Value), i)
                                sql = "INSERT INTO TimeControl(RoadIMEI,CMDType,CMDContent,CreateTime,HandlerFlag) values('" & imei & "', '158','" & inter_command & "','" & Now & "','" & CONTROL_BOX_TYPE2_FLAG & "')"
                                DBOperation.ExecuteSQL(conn, sql, msg)
                                m = 0
                                While m < 20
                                    sql_return = "SELECT * FROM RoadLightStatus where PackType=25 AND HandlerFlag=2 and Left(StatusContent,2)= '" & Mid(box_id, 4, 2) & "' and Right(StatusContent,2)='" & Mid(box_id, 1, 2) & "' and Createtime>'" & time & "'"
                                    rs_return = DBOperation.SelectSQL(conn, sql_return, msg)
                                    If rs_return Is Nothing Then
                                        MsgBox(MSG_ERROR_STRING, , PROJECT_TITLE_STRING)
                                        conn.Close()
                                        conn = Nothing
                                    End If
                                    If rs_return.RecordCount > 0 Then
                                        g_welcomewinobj.SetTextLabelDelegate("获取路灯数据.....", g_welcomewinobj.Tool, "circle_string")
                                        '查找当前数据库的所有灯的记录，按上传数据进行数据分析
                                        While rs_return.EOF = False
                                            If g_welcomewinobj.BackgroundWorkergetlampdata.CancellationPending = True Then
                                                GoTo finish
                                            End If
                                            '2011年10月17日将单灯分为不同的协议类型
                                            state = Trim(rs_return.Fields("StatusContent").Value).Split(" ")
                                            Com_inf.getstate_lampid(state)  '获取单灯编号
                                            ' i = 0
                                            Dim short_id As String
                                            lamp_protocle_type = Com_inf.getlampprotocletype(g_lampidstring)
                                            If lamp_protocle_type = "-1" Then
                                                '表示软件中未添加该灯号
                                                '去除改状态
                                                sql = "update RoadLightStatus set HandlerFlag=1 where id=" & rs_return.Fields("id").Value
                                                DBOperation.ExecuteSQL(conn, sql, msg)
                                                GoTo next1
                                            End If
                                            If lamp_protocle_type = "1" Then
                                                If state.Length <> 7 Then
                                                    '上传的状态长度与单灯的类型不符合
                                                    g_welcomewinobj.SetTextDelegate(g_lampidstring & "节点版本设置为" & lamp_protocle_type & "与上传状态不符 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, True, g_welcomewinobj.rtb_info_list)
                                                    GoTo finish
                                                End If
                                                Com_inf.Explain_State_String(state)  '解析状态字符串的各个含义
                                                GetCompareState(Trim(rs_return.Fields("StatusContent").Value), rs_return.Fields("id").Value)  '获取路灯的运行状态
                                            Else
                                                If lamp_protocle_type = "2" Then
                                                    If state.Length <> 7 Then
                                                        '上传的状态长度与单灯的类型不符合
                                                        g_welcomewinobj.SetTextDelegate(g_lampidstring & "节点版本设置为" & lamp_protocle_type & "与上传状态不符 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, True, g_welcomewinobj.rtb_info_list)

                                                        GoTo finish
                                                    End If
                                                    Com_inf.Explain_State_String_AD2(state)  '解析状态字符串的各个含义
                                                    GetCompareState_AD2(Trim(rs_return.Fields("StatusContent").Value), rs_return.Fields("id").Value)  '获取路灯的运行状态
                                                Else
                                                    If state.Length <> 10 Then
                                                        '上传的状态长度与单灯的类型不符合
                                                        g_welcomewinobj.SetTextDelegate(g_lampidstring & "节点版本设置为" & lamp_protocle_type & "与上传状态不符 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, True, g_welcomewinobj.rtb_info_list)
                                                        GoTo finish
                                                    End If
                                                    Dim j As Integer = 0
                                                    While j < 1

                                                        '2012年5月24日增加五字节的单灯协议,单灯的格式为两字节路段号，两字节节点号，六个字节的单灯状态
                                                        Com_inf.Explain_State_String_AD6(state, j) '解析状态字符串的各个含义
                                                        Alarm_GetCompareState_AD6(Trim(rs_return.Fields("StatusContent").Value), rs_return.Fields("id").Value)  '获取路灯的运行状态
                                                        j += 1
                                                        short_id = Mid(g_lampidstring, 7, LAMP_ID_LEN) + 1

                                                        While short_id.Length < LAMP_ID_LEN
                                                            short_id = "0" & short_id
                                                        End While
                                                        g_lampidstring = Mid(g_lampidstring, 1, 6) & short_id

                                                    End While
                                                End If

                                            End If
                                            m += 1
                                            'next1:
                                            qujiannum -= 1
                                            If qujiannum = 0 Then
                                                rs_return.MoveNext()
                                                If rs_return.EOF = True Then
                                                    g_welcomewinobj.SetTextDelegate("主控箱：" & Trim(rs.Fields("control_box_id").Value) & "区间" & i & " 数据采集完毕" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, True, g_welcomewinobj.rtb_info_list)

                                                    GoTo next1
                                                End If
                                            Else
                                                rs_return.MoveNext()
                                            End If
                                        End While
                                    Else
                                        m += 1
                                        g_welcomewinobj.SetTextDelegate("主控箱：" & Trim(rs.Fields("control_box_id").Value) & "区间" & i & " 数据采集 " & m & "秒 " & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, True, g_welcomewinobj.rtb_info_list)

                                        System.Threading.Thread.Sleep(1000)
                                    End If

                                    If m > 20 Then
                                        '超时
                                        g_welcomewinobj.SetTextDelegate("主控箱：" & Trim(rs.Fields("control_box_id").Value) & "区间" & i & " 数据采集失败 " & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, True, g_welcomewinobj.rtb_info_list)
                                    End If

                                End While
next1:
                                i += 1
                            End While
                        End If
                    End If
                End If
                rs.MoveNext()
            End While
        End If
        g_welcomewinobj.SetTextDelegate("单灯数据采集完毕！ " & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, True, g_welcomewinobj.rtb_info_list)

finish:

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        If rs_return.State = 1 Then
            rs_return.Close()
            rs_return = Nothing
        End If
        conn.Close()
        conn = Nothing

    End Sub

    ''' <summary>
    '''自动区间三次巡查对返回的状态处理
    ''' </summary>
    ''' <param name="state_string">状态表中的状态字符串</param>
    ''' <param name="state_id">状态表中的id</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Alarm_GetCompareState_AD6(ByVal state_string As String, ByVal state_id As Integer) As String
        Dim rs_lamp As New ADODB.Recordset
        Dim rs As New ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim conn As New ADODB.Connection
        Dim pro_num As Integer   '统计故障次数
        Dim num As Integer   '统计故障次数
        Dim controlstate As Integer '
        Dim state_inf As String '分析的状态
        Dim orig_state, orig_result As Integer '原来的状态
        If DBOperation.OpenConn(conn) = False Then
            Alarm_GetCompareState_AD6 = ""
            Exit Function
        End If
        msg = ""
        state_inf = ""
        Alarm_GetCompareState_AD6 = "E"
        '有返回的路灯状态数据,将 其与路灯状态表中的状态对比，A,暗；B,亮；C，该亮非亮；D该暗非暗
        sql = "select state,result,total_num,control_box_id,lamp_id,div_time_id from lamp_inf where lamp_id='" & g_lampidstring & "'"
        rs_lamp = DBOperation.SelectSQL(conn, sql, msg)
        If rs_lamp.RecordCount > 0 Then
            controlstate = rs_lamp.Fields("div_time_id").Value
            orig_state = rs_lamp.Fields("state").Value
            orig_result = rs_lamp.Fields("result").Value
            '状态只有0与8是正常的，其他为故障类型
            If g_information = 0 Then  '表示暗的
                Alarm_GetCompareState_AD6 = "A"
            Else
                If g_information = 8 Then
                    Alarm_GetCompareState_AD6 = "B"  '表示亮
                Else
                    If g_information = 2 Then
                        Alarm_GetCompareState_AD6 = "F" '无信息返回
                    Else
                        Alarm_GetCompareState_AD6 = "C"  '表示故障
                    End If
                End If
            End If
            '更改路灯的实时状态时间
            'sql = "update lamp_inf set date='" & Now & "' where lamp_id='" & g_lampidstring & "'"
            'DBOperation.ExecuteSQL(conn, sql, msg)
            '如果获取的状态时正常的，并且之前的状态为故障，则表示故障维修完毕，标志lamp_inf表中的total_num位为0
            'If (GetCompareState_AD2 = "A" Or GetCompareState_AD2 = "B_part" Or GetCompareState_AD2 = "B_all") And (rs_lamp.Fields("result").Value = 1 Or rs_lamp.Fields("result").Value = 2) Then
            pro_num = rs_lamp.Fields("total_num").Value
            If (Alarm_GetCompareState_AD6 = "A" Or Alarm_GetCompareState_AD6 = "B") And (rs_lamp.Fields("result").Value <> 0) Then
                '路灯的实时状态表
                sql = "update lamp_inf set total_num=0 where lamp_id='" & g_lampidstring & "'"
                DBOperation.ExecuteSQL(conn, sql, msg)
                '路灯的故障表
                sql = "update lamp_inf_record set server_state=1, date_end='" & Now & "' where lamp_id='" & g_lampidstring & "' and server_state=0"
                DBOperation.ExecuteSQL(conn, sql, msg)
                'Else
                '    '路灯的实时出现故障时
                '    sql = "update lamp_inf set total_num=" & pro_num + 1 & " where lamp_id='" & g_lampidstring & "'"
                '    DBOperation.ExecuteSQL(conn, sql, msg)
            End If
next1:
            sql = "update RoadLightStatus set HandlerFlag=1 where ID='" & state_id & "'"
            DBOperation.ExecuteSQL(conn, sql, msg)
            Com_inf.lamp_id_to_detail(g_lampidstring)   '通过灯的编号获取电控箱名称，类型和灯的独立编号
            ' Com_inf.Get_current_and_presure_AD2(g_currentad)  '将电流和电压的AD值转换成电流电压实际值
            sql = "select state,result,total_num,control_box_id,lamp_id,div_time_id from lamp_inf where lamp_id='" & g_lampidstring & "'"
            rs = DBOperation.SelectSQL(conn, sql, msg)
            'num = rs.Fields("total_num").Value
            'If num = 0 Then
            Select Case Alarm_GetCompareState_AD6
                Case "A"   '关闭状态
                    If g_information <> orig_state Then
                        sql = "update lamp_inf set div_time_id=0, result=" & 0 & " , presure_l=" & g_presurevalue & " , current_l=" & g_currentvalue & ", state=" & g_information & " ,power=" & g_powervalue & ", presure_end=" & g_yinshuvalue & ", total_num=0,date='" & Now & "' where lamp_id='" & g_lampidstring & "'"
                    Else
                        sql = "update lamp_inf set div_time_id=0, result=" & 0 & " , presure_l=" & g_presurevalue & " , current_l=" & g_currentvalue & ", state=" & g_information & " ,power=" & g_powervalue & ", presure_end=" & g_yinshuvalue & ", total_num=0 where lamp_id='" & g_lampidstring & "'"
                    End If
                    '  sql = "update lamp_inf set result=" & 0 & " , presure_l=" & g_presurevalue & " , current_l=" & g_currentvalue & ", state=" & g_information & " ,power=" & g_powervalue & ", presure_end=" & g_yinshuvalue & ", total_num=0 where lamp_id='" & g_lampidstring & "'"
                    DBOperation.ExecuteSQL(conn, sql, msg)
                    state_inf = get_probleminf(g_information)
                    Add_state_record(g_lampidstring, state_inf, g_presurevalue, g_currentvalue, g_powervalue, g_yinshuvalue)
                    ' Add_alarm_record(g_lampidstring, state_inf, g_presurevalue, g_currentvalue, g_powervalue, g_yinshuvalue)
                Case "B"  '打开
                    If g_information <> orig_state Then
                        sql = "update lamp_inf set div_time_id=1, result=" & 0 & " , presure_l=" & g_presurevalue & " , current_l=" & g_currentvalue & ", state=" & g_information & " ,power=" & g_powervalue & ", presure_end=" & g_yinshuvalue & ", total_num=0,date='" & Now & "'  where lamp_id='" & g_lampidstring & "'"
                    Else
                        sql = "update lamp_inf set div_time_id=1, result=" & 0 & " , presure_l=" & g_presurevalue & " , current_l=" & g_currentvalue & ", state=" & g_information & " ,power=" & g_powervalue & ", presure_end=" & g_yinshuvalue & ", total_num=0  where lamp_id='" & g_lampidstring & "'"


                    End If
                    ' sql = "update lamp_inf set result=" & 0 & " , presure_l=" & g_presurevalue & " , current_l=" & g_currentvalue & ", state=" & g_information & " ,power=" & g_powervalue & ", presure_end=" & g_yinshuvalue & ", total_num=0 where lamp_id='" & g_lampidstring & "'"
                    DBOperation.ExecuteSQL(conn, sql, msg)
                    state_inf = get_probleminf(g_information)
                    Add_state_record(g_lampidstring, state_inf, g_presurevalue, g_currentvalue, g_powervalue, g_yinshuvalue)
                    ' Add_alarm_record(g_lampidstring, state_inf, g_presurevalue, g_currentvalue, g_powervalue, g_yinshuvalue)
                Case "C"
                    If rs_lamp.Fields("total_num").Value = 2 Then '表示累计2次故障则发过短信
                        '发送短信
                        Send_Msg(rs_lamp.Fields("control_box_id").Value, rs_lamp.Fields("lamp_id").Value, LAMP_STATE_PROBLEM_OFF)
                    End If
                    pro_num = rs_lamp.Fields("total_num").Value + 1
                    If pro_num > 10000 Then
                        pro_num = 20
                    End If
                    '故障，区分不同的故障类型
                    'If g_information <= 8 Then
                    '    sql = "update lamp_inf set result=" & 1 & " , presure_l=" & g_presurevalue & " , current_l=" & g_currentvalue & ", state=" & g_information & " ,power=" & g_powervalue & ", presure_end=" & g_yinshuvalue & ",  total_num='" & pro_num & "' where lamp_id='" & g_lampidstring & "'"
                    'Else
                    '    sql = "update lamp_inf set result=" & 1 & " , presure_l=" & g_presurevalue & " , current_l=" & g_currentvalue & ", state=" & g_information & " ,power=" & g_powervalue & ", presure_end=" & g_yinshuvalue & ",  total_num='" & pro_num & "' where lamp_id='" & g_lampidstring & "'"
                    'End If
                    If g_information <> orig_state Then
                        If orig_state = 22 Then
                            sql = "update lamp_inf set result=" & 1 & " , presure_l=" & g_presurevalue & " , current_l=" & g_currentvalue & ", state=" & g_information & " ,power=" & g_powervalue & ", presure_end=" & g_yinshuvalue & ",  total_num=0  where lamp_id='" & g_lampidstring & "'"
                        Else
                            sql = "update lamp_inf set result=" & 1 & " , presure_l=" & g_presurevalue & " , current_l=" & g_currentvalue & ", state=" & g_information & " ,power=" & g_powervalue & ", presure_end=" & g_yinshuvalue & ",  total_num=0 ,date='" & Now & "' where lamp_id='" & g_lampidstring & "'"

                        End If
                    Else
                        sql = "update lamp_inf set result=" & 1 & " , presure_l=" & g_presurevalue & " , current_l=" & g_currentvalue & ", state=" & g_information & " ,power=" & g_powervalue & ", presure_end=" & g_yinshuvalue & ",  total_num='" & pro_num & "'  where lamp_id='" & g_lampidstring & "'"

                    End If
                    DBOperation.ExecuteSQL(conn, sql, msg)
                    state_inf = get_probleminf(g_information)
                    Add_state_record(g_lampidstring, state_inf, g_presurevalue, g_currentvalue, g_powervalue, g_yinshuvalue)

                    'Add_alarm_record(g_lampidstring, state_inf, g_presurevalue, g_currentvalue, g_powervalue, g_yinshuvalue)

                Case "F"
                    '无信息返回

                    If g_information <> orig_state And orig_result <> 5 Then

                        sql = "update lamp_inf set total_num=0,result=5 where lamp_id='" & g_lampidstring & "'"
                        DBOperation.ExecuteSQL(conn, sql, msg)
                        'state_inf = get_probleminf(g_information)
                        'Add_state_record(g_lampidstring, state_inf, 0, 0, 0, 0)
                    Else
                        If g_information <> orig_state Then
                            If pro_num = 1 And orig_result = 5 And g_autotime = 3 Then
                                sql = "update lamp_inf set result=3, presure_l=0, current_l=0, power=0, presure_end=0, state=" & g_information & ", total_num='" & pro_num + 1 & "',date='" & Now & "' where lamp_id='" & g_lampidstring & "'"
                                DBOperation.ExecuteSQL(conn, sql, msg)
                                state_inf = get_probleminf(g_information)
                                Add_state_record(g_lampidstring, state_inf, 0, 0, 0, 0)
                            Else
                                If g_autotime = 1 Then
                                    sql = "update lamp_inf set total_num=0 where lamp_id='" & g_lampidstring & "'"
                                Else
                                    sql = "update lamp_inf set total_num='" & pro_num + 1 & "' where lamp_id='" & g_lampidstring & "'"
                                End If

                                DBOperation.ExecuteSQL(conn, sql, msg)
                                'state_inf = get_probleminf(g_information)
                                'Add_state_record(g_lampidstring, state_inf, 0, 0, 0, 0)
                            End If
                        Else
                            If g_autotime = 1 Then
                                sql = "update lamp_inf set total_num=0 where lamp_id='" & g_lampidstring & "'"
                            Else
                                sql = "update lamp_inf set total_num='" & pro_num + 1 & "' where lamp_id='" & g_lampidstring & "'"
                            End If
                            DBOperation.ExecuteSQL(conn, sql, msg)
                            If pro_num = 1 And g_autotime = 3 Then
                                '第三次记录
                                state_inf = get_probleminf(g_information)
                                Add_state_record(g_lampidstring, state_inf, 0, 0, 0, 0)
                            End If
                        End If
                    End If
            End Select
        End If
        If rs_lamp.State = 1 Then
            rs_lamp.Close()
            rs_lamp = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Function




    Public Function get_qujian(ByVal control_box As String, ByVal qujian As Integer) As Integer
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        ' Dim order_string As String '控制命令
        msg = ""
        DBOperation.OpenConn(conn)
        sql = "select count(lamp_id) as quanjian_num from lamp_inf where control_box_id=" & control_box & " AND inter_count=" & qujian
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Setgrouporder_lamp_state" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)
            conn.Close()
            conn = Nothing
            Exit Function
        End If
        If rs.RecordCount <= 0 Then
            get_qujian = 0
        Else
            get_qujian = rs.Fields("quanjian_num").Value
        End If
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Function
End Class

