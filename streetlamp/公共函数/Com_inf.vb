''' <summary>
''' 公共函数
''' </summary>
''' <remarks></remarks>
Module Com_inf
    Public Const INVALID_HANDLE_VALUE = -1
    Public Const INFINITE = &HFFFFFFFF            ' Infinite timeout
    Public Structure handorder
        Dim row_id As Integer
        Dim order_string As String
        Dim order_type As String
    End Structure

    ''' <summary>
    ''' 获取工程的标题
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetProjectTitle()
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String

        msg = ""
        sql = "select * from sysconfig where type='项目标题'"
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "GetProjectTitle" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs.RecordCount > 0 Then
            PROJECT_TITLE_STRING = Trim(rs.Fields("name").Value)
        Else
            PROJECT_TITLE_STRING = "苏州电网故障报警系统"
        End If

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    ''' 设置地图的配置
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GetMapSize()
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String

        msg = ""
        sql = "select * from sysconfig where type='地图最大尺寸'"
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "GetMapSize" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs.RecordCount > 0 Then
            MAP_MAX_SIZE = Val(Trim(rs.Fields("name").Value))
        Else
            MAP_MAX_SIZE = 10
        End If

        sql = "select * from sysconfig where type='地图默认尺寸'"

        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "GetMapSize" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs.RecordCount > 0 Then
            MAP_MID_SIZE = Val(Trim(rs.Fields("name").Value))
        Else
            MAP_MID_SIZE = 5
        End If

        MAP_SIZE_BASE = 1 - (MAP_MID_SIZE / MAP_MAX_SIZE)
        MAP_SIZE_CHANGE = 1 / MAP_MAX_SIZE
        g_changemapvalue = MAP_MID_SIZE
        'g_mapsizevalue = map.Size '地图的尺寸
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub


    ''' <summary>
    ''' 初始化各类需要设置的系统参数
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitSystemConfig()
        GetProjectTitle()  '获取工程的标题
        'GetMapSize()  '获取地图的尺寸
        'MAP_MAX_SIZE = 20   '地图的滑动条的最大值
        'MAP_MID_SIZE = 5    '地图的滑动条的常值
        'MAP_SIZE_BASE = 0.6   '缩放的大小基数
        'MAP_SIZE_CHANGE = 0.1    '缩放的比例

    End Sub
    ''' <summary>
    '''   读取路灯状态颜色文件
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Read_file()
        Try
            Dim MyRead As New System.IO.StreamReader("lamp_color.txt", System.Text.Encoding.UTF8)
            g_fullcolor = Color.FromArgb(Val(MyRead.ReadLine))  '开灯颜色
            g_partcolor = Color.FromArgb(Val(MyRead.ReadLine))   '半功率灯的颜色
            g_noreturncolor = Color.FromArgb(Val(MyRead.ReadLine))   '无返回值颜色
            g_closecolor = Color.FromArgb(Val(MyRead.ReadLine))   '关闭灯颜色
            g_problemcolor = Color.FromArgb(Val(MyRead.ReadLine))   '故障灯的颜色
            MyRead.Close()  '关闭文件

        Catch ex As Exception
            MsgBox(ex.Message, , PROJECT_TITLE_STRING)
        End Try
    End Sub

    ''' <summary>
    '''   读取地图文件
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Read_map()
        Try
            Dim MyRead As New System.IO.StreamReader("map_name.txt", System.Text.Encoding.UTF8)
            g_mapname = MyRead.ReadLine
            MyRead.Close()  '关闭文件

        Catch ex As Exception
            MsgBox(ex.Message, , PROJECT_TITLE_STRING)
        End Try
    End Sub

    ''' <summary>
    ''' '将十进制的数转换成二进制的字符串，长度为len 
    ''' </summary>
    ''' <param name="num">数值</param>
    ''' <param name="len">长度</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Dec_to_Bin(ByVal num As Integer, ByVal len As Integer) As String
        Dim str1 As String
        Dim ox_string As String
        Dim string_len As Integer
        str1 = ""
        ox_string = Convert.ToString(num, 2).ToString.ToUpper   '十进制转换城2进制数
        string_len = ox_string.Length
        While string_len < len  '如果长度比len小，则在数字前补充0
            ox_string = "0" & ox_string
            string_len += 1
        End While
        Dec_to_Bin = ox_string   '返回值

    End Function

    ''' <summary>
    '''  '十进制的字符串转换成16进制的字符串，长度为len
    ''' </summary>
    ''' <param name="str">数值</param>
    ''' <param name="len">长度</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Dec_to_Hex(ByVal str As String, ByVal len As Integer) As String
        Dim str1 As String
        Dim num As Integer
        Dim hex_string As String
        Dim string_len As Integer  '字符串长度
        str1 = ""
        num = Val(str)
        hex_string = Convert.ToString(num, 16).ToString.ToUpper   '十进制转换城16进制数

        string_len = hex_string.Length
        While string_len < len   '如果长度比len小，则在数字前补充0
            hex_string = "0" & hex_string
            string_len += 1
        End While
        Dec_to_Hex = hex_string   '返回值
    End Function

    ''' <summary>
    ''' '二进制转换成十六进制
    ''' </summary>
    ''' <param name="Bin"></param>
    ''' <returns>十六进制</returns>
    ''' <remarks></remarks>
    Public Function BIN_to_HEX(ByVal Bin As String) As String
        Dim i As Long
        Dim H As String
        H = ""
        i = 1
        Bin = UCase(Bin)
        While i < Len(Bin)
            Select Case Mid(Bin, i, 4)
                Case "0000" : H = H & "0"
                Case "0001" : H = H & "1"
                Case "0010" : H = H & "2"
                Case "0011" : H = H & "3"
                Case "0100" : H = H & "4"
                Case "0101" : H = H & "5"
                Case "0110" : H = H & "6"
                Case "0111" : H = H & "7"
                Case "1000" : H = H & "8"
                Case "1001" : H = H & "9"
                Case "1010" : H = H & "A"
                Case "1011" : H = H & "B"
                Case "1100" : H = H & "C"
                Case "1101" : H = H & "D"
                Case "1110" : H = H & "E"
                Case "1111" : H = H & "F"
            End Select
            i += 4
        End While

        BIN_to_HEX = H
    End Function

    ''' <summary>
    ''' '十六进制转换成二进制
    ''' </summary>
    ''' <param name="Hex"></param>
    ''' <returns>十六进制</returns>
    ''' <remarks></remarks>
    Public Function HEX_to_BIN(ByVal Hex As String) As String
        Dim i As Long
        Dim B As String
        B = ""
        i = 1
        Hex = UCase(Hex)
        For i = 1 To Len(Hex)
            Select Case Mid(Hex, i, 1)
                Case "0" : B = B & "0000"
                Case "1" : B = B & "0001"
                Case "2" : B = B & "0010"
                Case "3" : B = B & "0011"
                Case "4" : B = B & "0100"
                Case "5" : B = B & "0101"
                Case "6" : B = B & "0110"
                Case "7" : B = B & "0111"
                Case "8" : B = B & "1000"
                Case "9" : B = B & "1001"
                Case "A" : B = B & "1010"
                Case "B" : B = B & "1011"
                Case "C" : B = B & "1100"
                Case "D" : B = B & "1101"
                Case "E" : B = B & "1110"
                Case "F" : B = B & "1111"
            End Select
        Next i

        HEX_to_BIN = B
    End Function

    ''' <summary>
    ''' 将二进制字符串转换成十进制的（目的是用于将五位类型和11位的灯的编号转换）
    ''' </summary>
    ''' <param name="Number"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Value_Bin(ByVal Number As Long) As Long
        Dim Temp_value As Long
        Dim Cyc As Long
        For Cyc = 0 To Len(Str(Number))
            If Cyc = Len(Str(Number)) Then
                Value_Bin = Temp_value + Val(Mid(Str(Number), Cyc, 1))
                Exit For
            End If
            If Cyc > 0 Then Temp_value = (Temp_value + Val(Mid(Str(Number), Cyc, 1))) * 2
        Next Cyc
    End Function

    ''' <summary>
    ''' '控制命令中的十六进制转换成十进制的两位的灯类型和三位的路灯编号
    ''' </summary>
    ''' <param name="str">控制命令字符串</param>
    ''' <returns>返回五位长度的十进制数，两位灯的类型和三位路灯编号</returns>
    ''' <remarks></remarks>
    Public Function Control_Hex_to_Dec(ByVal str As String) As String
        Dim str_bin, str_id As String
        Dim lamp_type_num As Integer
        Dim lamp_id As Integer
        Dim id_str As String
        '********************2011年3月7日将灯的编号扩展为5位时修改*******************
        'str_bin = HEX_to_BIN(Mid(str, 1, 2)) '第1,2两位十六进制转换成八位二进制
        'str_bin &= HEX_to_BIN(Mid(str, 4, 2))  '第4,5两位十六进制转换成八位二进制
        'lamp_type_num = Value_Bin(Mid(str_bin, 1, 5)).ToString  '类型编号
        'lamp_id = Value_Bin(Mid(str_bin, 6, 11)).ToString  '灯的编号


        'If lamp_type_num.ToString.Length = 1 Then  '两位的十进制路灯编号
        '    str_id = "0" & lamp_type_num.ToString
        'Else
        '    str_id = lamp_type_num.ToString
        'End If

        'id_str = lamp_id.ToString
        'While id_str.Length < LAMP_ID_LEN
        '    id_str = "0" & id_str
        'End While
        'str_id &= id_str

        'Control_Hex_to_Dec = str_id '返回七位长度的十进制数，两位灯的类型和五位路灯编号
        '********************2011年3月7日将灯的编号扩展为5位时修改*******************
        str_bin = HEX_to_BIN(str)  '将四位的十六进制灯的编号转换成16位二进制

        str_id = System.Convert.ToInt32(str_bin, 2)  '将二进制转换成16进制
        If str_id > LAMP_ID_MAX Then
            lamp_type_num = Value_Bin(Mid(str_bin, 1, 5)).ToString  '类型编号
            lamp_id = Value_Bin(Mid(str_bin, 6, 11)).ToString  '灯的编号
        Else
            lamp_type_num = 0 '0类型
            lamp_id = str_id  '灯的编号

        End If


        If lamp_type_num.ToString.Length = 1 Then  '两位的十进制路灯编号
            str_id = "0" & lamp_type_num.ToString
        Else
            str_id = lamp_type_num.ToString
        End If

        id_str = lamp_id.ToString
        While id_str.Length < LAMP_ID_LEN
            id_str = "0" & id_str
        End While
        str_id &= id_str

        Control_Hex_to_Dec = str_id '返回2+LAMP_ID_LEN位长度的十进制数，两位灯的类型和LAMP_ID_LEN位路灯编号


    End Function

    ''' <summary>
    ''' 将状态的十六进制转换成十进制值,解析各个字段的含义,2012年5月24日单灯的节点内容为6个字节，12位电压
    ''' 10位电流，14位功率，4位故障类型
    ''' </summary>
    ''' <param name="inf_list">状态段数组</param>
    ''' <remarks></remarks>
    Public Sub Explain_State_String_AD6(ByVal inf_list() As String, ByVal state_id As Integer)
        Dim state_string As String
        Dim presurevalue As String
        Dim currentvalue As String
        Dim powervalue As String
        Dim information As String
        Dim yinshuvalue As Double

        state_string = inf_list(3 + state_id * 6) & inf_list(4 + state_id * 6) & inf_list(5 + state_id * 6) & inf_list(6 + state_id * 6) & inf_list(7 + state_id * 6)  '五个字节的状态
        If state_string = "FFFFFFFFFF" Then
            g_presurevalue = 0
            g_currentvalue = 0
            g_powervalue = 0
            g_information = 2
            g_yinshuvalue = 0
            Exit Sub
        End If
        '将十六进制的数据转换成二进制字符串
        state_string = Com_inf.HEX_to_BIN(state_string)
        presurevalue = Mid(state_string, 1, 12)  '12位的电压
        currentvalue = Mid(state_string, 13, 10)  ''10位的电流
        powervalue = Mid(state_string, 23, 14)  '14位的功率
        information = Mid(state_string, 37, 4) '4位的状态

        g_presurevalue = Format(System.Convert.ToInt32(presurevalue, 2) / 10, "0.00")
        g_currentvalue = Format(System.Convert.ToInt32(currentvalue, 2) / 100, "0.00")
        g_powervalue = Format(System.Convert.ToInt32(powervalue, 2) / 10, "0.00")
        g_information = Format(System.Convert.ToInt32(information, 2), "0.00")
        If (g_information And &H4) = 4 Then
            g_information = 12
        End If
        If g_presurevalue * g_currentvalue = 0 Then
            yinshuvalue = 0
        Else
            yinshuvalue = g_powervalue / (g_presurevalue * g_currentvalue)
        End If

        If yinshuvalue >= 0.991 Then
            yinshuvalue = 0.99
        End If
        g_yinshuvalue = Format(yinshuvalue, "0.00")  '功率因数

        ' Dim i As Integer = System.Convert.ToInt32(Com_inf.BIN_to_HEX(currentvalue), 16)

        'g_dianzuad = System.Convert.ToInt32(inf_list(3), 16).ToString   '第4个字节作为判断开关的标志
        'g_currentad = System.Convert.ToInt32(inf_list(3) & inf_list(4), 16).ToString  '第4，5个字节转换为2位电流
        'g_information = System.Convert.ToInt32(inf_list(5), 16).ToString  '第6个字节转换为2位的预留
    End Sub

    ''' <summary>
    ''' 将状态的十六进制转换成十进制值,解析各个字段的含义,2011年5月19日增加电流AD为2个字节，取消电阻AD值
    ''' </summary>
    ''' <param name="inf_list">状态段数组</param>
    ''' <remarks></remarks>
    Public Sub Explain_State_String_AD2(ByVal inf_list() As String)
        'Dim short_str As String
        'Dim inf_list() As String
        'inf_list = str.Split(" ")
        'short_str = Val("&H" & (inf_list(6) & inf_list(0))).ToString   '第7个字节并上第1个字节转换为四位的十进制电控箱编号
        'While short_str.Length < 4
        '    short_str = "0" & short_str '电控箱编号不足4位用0补充
        'End While
        'g_controlboxid = short_str
        'g_lampidstring = g_controlboxid & Control_Hex_to_Dec(inf_list(1) & inf_list(2))   '第2和3个字节转换成2+LAMP_ID_LEN长度的路灯编号
        g_dianzuad = System.Convert.ToInt32(inf_list(3), 16).ToString   '第4个字节作为判断开关的标志
        g_currentad = System.Convert.ToInt32(inf_list(3) & inf_list(4), 16).ToString  '第4，5个字节转换为2位电流
        g_information = System.Convert.ToInt32(inf_list(5), 16).ToString  '第6个字节转换为2位的预留
    End Sub
    ''' <summary>
    ''' 将状态的十六进制转换成十进制值,解析各个字段的含义
    ''' </summary>
    ''' <param name="inf_list">状态段数组</param>
    ''' <remarks></remarks>
    Public Sub Explain_State_String(ByVal inf_list() As String)
        'Dim short_str As String
        'Dim inf_list() As String
        'inf_list = str.Split(" ")
        'short_str = Val("&H" & (inf_list(6) & inf_list(0))).ToString   '第7个字节并上第1个字节转换为四位的十进制电控箱编号
        'While short_str.Length < 4
        '    short_str = "0" & short_str '电控箱编号不足4位用0补充
        'End While
        'g_controlboxid = short_str
        'g_lampidstring = g_controlboxid & Control_Hex_to_Dec(inf_list(1) & inf_list(2))   '第2和3个字节转换成2+LAMP_ID_LEN长度的路灯编号
        g_dianzuad = System.Convert.ToInt32(inf_list(3), 16).ToString   '第4个字节转换为2位电阻
        g_currentad = System.Convert.ToInt32(inf_list(4), 16).ToString  '第5个字节转换为2位电流
        g_information = System.Convert.ToInt32(inf_list(5), 16).ToString  '第6个字节转换为2位的预留
    End Sub

    Public Function getstate_lampid(ByVal inf_list() As String) As String
        Dim short_str As String
        Dim lamp_kind As String
        Dim lamp_id As String
        If inf_list.Length = 7 Then
            short_str = Val("&H" & (inf_list(6) & inf_list(0))).ToString   '第7个字节并上第1个字节转换为四位的十进制电控箱编号
            While short_str.Length < 4
                short_str = "0" & short_str '电控箱编号不足4位用0补充
            End While
            g_controlboxid = short_str
            short_str = HEX_to_BIN(inf_list(1) & inf_list(2))
            lamp_kind = System.Convert.ToInt32(Mid(short_str, 1, 5), 2)
            While lamp_kind.Length < 2
                lamp_kind = "0" & lamp_kind
            End While
            lamp_id = System.Convert.ToInt32(Mid(short_str, 6, 11), 2)
            While lamp_id.Length < LAMP_ID_LEN
                lamp_id = "0" & lamp_id

            End While
            g_lampidstring = g_controlboxid & lamp_kind & lamp_id  '第2和3个字节转换成2+LAMP_ID_LEN长度的路灯编号

        Else
            g_controlboxid = System.Convert.ToInt32(inf_list(9) & inf_list(0), 16)
            While g_controlboxid.Length < 4
                g_controlboxid = "0" & g_controlboxid
            End While
            short_str = HEX_to_BIN(inf_list(1) & inf_list(2))
            lamp_kind = System.Convert.ToInt32(Mid(short_str, 1, 5), 2)
            While lamp_kind.Length < 2
                lamp_kind = "0" & lamp_kind
            End While
            lamp_id = System.Convert.ToInt32(Mid(short_str, 6, 11), 2)
            While lamp_id.Length < LAMP_ID_LEN
                lamp_id = "0" & lamp_id

            End While
            g_lampidstring = g_controlboxid & lamp_kind & lamp_id  '第2和3个字节转换成2+LAMP_ID_LEN长度的路灯编号

        End If

        getstate_lampid = g_lampidstring
    End Function

    ''' <summary>
    '''  '下拉框中增加城市名称
    ''' </summary>
    ''' <param name="city_combox">城市下拉框</param>
    ''' <remarks></remarks>
    Public Sub Select_city_name(ByVal city_combox As System.Windows.Forms.ComboBox)
        Dim rs As New ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim conn As New ADODB.Connection
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        msg = ""
        city_combox.Items.Clear()  '城市下拉框清空
        sql = "select * from city "
        rs = DBOperation.SelectSQL(conn, sql, msg)
        While rs.EOF = False
            city_combox.Items.Add(Trim(rs.Fields("city").Value))  '增加城市名称
            rs.MoveNext()

        End While
        If city_combox.Items.Count > 0 Then
            city_combox.SelectedIndex = 0
        Else
            city_combox.Text = ""
        End If

        rs.Close()
        rs = Nothing
        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    '''  '下拉框中增加区域名称
    ''' </summary>
    ''' <param name="city_combox">城市名称下拉框</param>
    ''' <param name="area_combox">区下拉框</param>
    ''' <remarks></remarks>
    Public Sub Select_area_name(ByVal city_combox As System.Windows.Forms.ComboBox, ByVal area_combox As System.Windows.Forms.ComboBox)
        Dim rs As New ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim city_name As String
        Dim conn As New ADODB.Connection
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If

        city_name = Trim(city_combox.Text)
        msg = ""
        sql = "select * from city_area_view where city='" & city_name & "'"
        area_combox.Items.Clear()  '清空区域下拉框
        rs = DBOperation.SelectSQL(conn, sql, msg)
        While rs.EOF = False
            area_combox.Items.Add(rs.Fields("area").Value) '增加区域下拉框
            rs.MoveNext()

        End While
        If area_combox.Items.Count > 0 Then
            area_combox.SelectedIndex = 0
        Else
            area_combox.Text = ""
        End If
        rs.Close()
        rs = Nothing
        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    ''' 增加街道名称
    ''' </summary>
    ''' <param name="city_combox">城市名称下拉框</param>
    ''' <param name="area_combox">区名称下拉框</param>
    ''' <param name="street_combox">街道名称下拉框</param>
    ''' <remarks></remarks>
    Public Sub Select_street_name(ByVal city_combox As System.Windows.Forms.ComboBox, ByVal area_combox As System.Windows.Forms.ComboBox, ByVal street_combox As System.Windows.Forms.ComboBox)
        street_combox.Items.Clear()
        Dim rs As New ADODB.Recordset
        Dim msg As String
        Dim sql As String

        Dim city_name, area_name As String
        Dim conn As New ADODB.Connection
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If

        city_name = Trim(city_combox.Text)
        area_name = Trim(area_combox.Text)
        msg = ""
        sql = "select * from area_street where area='" & area_name & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs.RecordCount > 0 Then
            While rs.EOF = False
                street_combox.Items.Add(Trim(rs.Fields("street").Value))  '增加街道下拉框的内容
                rs.MoveNext()
            End While
        End If

        If street_combox.Items.Count > 0 Then
            street_combox.SelectedIndex = 0
        Else
            street_combox.Text = ""
        End If
        rs.Close()
        rs = Nothing
        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    ''' 选择类型编号
    ''' </summary>
    ''' <param name="lamp_type"></param>
    ''' <param name="lamp_type_id"></param>
    ''' <remarks></remarks>
    Public Sub Select_type_id(ByVal lamp_type As String, ByVal lamp_type_id As System.Windows.Forms.Label)
        Dim rs As New ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim conn As New ADODB.Connection

        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        msg = ""
        sql = "select * from lamp_type where type_string='" & lamp_type & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Select_type_id" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            Exit Sub
        End If

        If rs.RecordCount > 0 Then
            lamp_type_id.Text = rs.Fields("type_id").Value

        Else
            lamp_type_id.Text = ""
        End If
        rs.Close()
        rs = Nothing
        conn.Close()
        conn = Nothing


    End Sub

    ''' <summary>
    ''' 景观灯的类型名称
    ''' </summary>
    ''' <param name="control_box_combox"></param>
    ''' <param name="type_combox"></param>
    ''' <param name="type_id"></param>
    ''' <remarks></remarks>
    Public Sub Select_type_name(ByVal control_box_combox As System.Windows.Forms.ComboBox, ByVal type_combox As System.Windows.Forms.ComboBox, ByVal type_id As System.Windows.Forms.Label)
        Dim rs As New ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim conn As New ADODB.Connection
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If

        msg = ""
        sql = "select * from box_lamptype_view where control_box_name='" & Trim(control_box_combox.Text) & "'"

        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Select_type_name" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            Exit Sub
        End If

        type_combox.Items.Clear()
        If rs.RecordCount > 0 Then '选择第一个灯的类型编号
            type_id.Text = rs.Fields("type_id").Value

            While rs.EOF = False
                type_combox.Items.Add(Trim(rs.Fields("type_string").Value))
                rs.MoveNext()

            End While
        End If

        If type_combox.Items.Count > 0 Then
            type_combox.SelectedIndex = 0
        Else
            type_combox.Text = ""
        End If
        rs.Close()
        rs = Nothing
        conn.Close()
        conn = Nothing

    End Sub

    ''' <summary>
    ''' 选择区域名称（即电控箱名称）
    ''' </summary>
    ''' <param name="box_id_combox"></param>
    ''' <remarks></remarks>
    Public Sub Select_box_name(ByVal box_id_combox As System.Windows.Forms.ComboBox)
        Dim rs As New ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim conn As New ADODB.Connection
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        msg = ""
        box_id_combox.Items.Clear()  '清空电控箱下拉框内容


        sql = "select * from control_box"
        rs = DBOperation.SelectSQL(conn, sql, msg)

        While rs.EOF = False
            box_id_combox.Items.Add(Trim(rs.Fields("control_box_name").Value))  '增加电控箱下拉框的内容
            rs.MoveNext()
        End While

        If box_id_combox.Items.Count > 0 Then
            box_id_combox.SelectedIndex = 0
        Else
            box_id_combox.Text = ""
        End If


        rs.Close()
        rs = Nothing
        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    ''' 电控箱名称
    ''' </summary>
    ''' <param name="city_combox">城市名称</param>
    ''' <param name="area_combox">区名称</param>
    ''' <param name="street_combox">街道名称</param>
    ''' <param name="box_name_combox">区域名称（电控箱）</param>
    ''' <remarks></remarks>
    Public Sub Select_box_name_level(ByVal city_combox As System.Windows.Forms.ComboBox, ByVal area_combox As System.Windows.Forms.ComboBox, ByVal street_combox As System.Windows.Forms.ComboBox, ByVal box_name_combox As System.Windows.Forms.ComboBox)
        Dim rs As New ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim conn As New ADODB.Connection
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        msg = ""
        Dim city_name, area_name, street_name As String
        city_name = Trim(city_combox.Text)
        area_name = Trim(area_combox.Text)
        street_name = Trim(street_combox.Text)
        box_name_combox.Items.Clear()  '清空电控箱下拉框内容
        sql = "select distinct(control_box_name),control_box_id from control_inf where street='" & street_name & "'and area='" & area_name & "' and city='" & city_name & "' order by control_box_id"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        While rs.EOF = False
            box_name_combox.Items.Add(Trim(rs.Fields("control_box_name").Value))  '增加电控箱下拉框的内容
            rs.MoveNext()
        End While
        If box_name_combox.Items.Count > 0 Then
            box_name_combox.SelectedIndex = 0
        Else
            box_name_combox.Text = ""
        End If
        rs.Close()
        rs = Nothing
        conn.Close()
        conn = Nothing
    End Sub


    ''' <summary>
    ''' 增加类型级别后，选择灯的编号
    ''' </summary>
    ''' <param name="box_id_combox">区域名称</param>
    ''' <param name="lamp_type_combox">类型</param>
    ''' <param name="lamp_id_combox">灯的独立编号</param>
    ''' <param name="lamp_id_start">灯的编号的起始五位</param>
    ''' <remarks></remarks>
    Public Sub Select_lamp_id_type(ByVal box_id_combox As System.Windows.Forms.ComboBox, ByVal lamp_type_combox As System.Windows.Forms.ComboBox, ByVal lamp_id_combox As System.Windows.Forms.ComboBox, ByVal lamp_id_start As System.Windows.Forms.Label)
        Dim rs As New ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim conn As New ADODB.Connection
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        msg = ""
        lamp_id_combox.Items.Clear()  '将路灯编号下拉框内容清空
        sql = "select * from lamp_street where control_box_name='" & Trim(box_id_combox.Text) & "' and type_string='" & Trim(lamp_type_combox.Text) & "' order by lamp_id"

        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Select_lamp_id_type" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If

        If rs.RecordCount > 0 Then
            lamp_id_start.Text = Mid(Trim(rs.Fields("lamp_id").Value), 1, 6)
            While rs.EOF = False
                lamp_id_combox.Items.Add(Mid(Trim(rs.Fields("lamp_id").Value), 7, LAMP_ID_LEN))  '增加路灯编号下拉框中的内容
                rs.MoveNext()
            End While
        End If

        If lamp_id_combox.Items.Count > 0 Then
            lamp_id_combox.SelectedIndex = 0
        End If

        rs.Close()
        rs = Nothing
        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    ''' 选择灯的编号（原始版本，目前不用）
    ''' </summary>
    ''' <param name="lamp_all"></param>
    ''' <param name="city_combox"></param>
    ''' <param name="area_combox"></param>
    ''' <param name="street_combox"></param>
    ''' <param name="box_id_combox"></param>
    ''' <param name="lamp_id_combox"></param>
    ''' <remarks></remarks>
    Public Sub Select_lamp_id(ByVal lamp_all As Integer, ByVal city_combox As System.Windows.Forms.ComboBox, ByVal area_combox As System.Windows.Forms.ComboBox, ByVal street_combox As System.Windows.Forms.ComboBox, ByVal box_id_combox As System.Windows.Forms.ComboBox, ByVal lamp_id_combox As System.Windows.Forms.ComboBox)
        Dim rs As New ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim conn As New ADODB.Connection
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If


        Dim city_name, area_name, street_name As String

        city_name = Trim(city_combox.Text)
        area_name = Trim(area_combox.Text)
        street_name = Trim(street_combox.Text)


        msg = ""
        lamp_id_combox.Items.Clear()  '将路灯编号下拉框内容清空

        If lamp_all = 0 Then  '如果选择的路灯有范围控制
            sql = "select * from lamp_inf where control_box_id='" & Trim(box_id_combox.Text) & "'"

        Else  '路灯编号无范围控制，选择所有
            sql = "select * from lamp_inf where control_box_id='" & Trim(box_id_combox.Text) & "'"

        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)

        If rs.RecordCount > 0 Then
            While rs.EOF = False
                lamp_id_combox.Items.Add(Trim(rs.Fields("lamp_id").Value))  '增加路灯编号下拉框中的内容
                rs.MoveNext()
            End While
        End If

        rs.Close()
        rs = Nothing
        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    ''' 选择电控箱名称
    ''' </summary>
    ''' <param name="control_box_name_combobox">电控箱名称下拉框</param>
    ''' <remarks></remarks>
    Public Sub Select_control_box_all(ByVal control_box_name_combobox As System.Windows.Forms.ComboBox)
        '电控箱名称下拉框
        Dim rs As New ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim conn As New ADODB.Connection
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If

        control_box_name_combobox.Items.Clear()
        msg = ""
        sql = "select * from control_box order by control_box_id"
        rs = DBOperation.SelectSQL(conn, sql, msg)

        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Select_control_box_all" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            Exit Sub
        End If

        While rs.EOF = False
            control_box_name_combobox.Items.Add(Trim(rs.Fields("control_box_name").Value))
            rs.MoveNext()
        End While



        rs.Close()
        rs = Nothing
        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    ''' 将电控箱编号，景观灯种类和灯的独立编号组合成一个完整的路灯编号
    ''' </summary>
    ''' <param name="control_box_name">电控箱名称</param>
    ''' <param name="lamp_type">类型</param>
    ''' <param name="lamp_id">灯的独立编号</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Com_lamp_id_all(ByVal control_box_name As String, ByVal lamp_type As String, ByVal lamp_id As String) As String
        Dim rs As New ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim conn As New ADODB.Connection
        If DBOperation.OpenConn(conn) = False Then
            Com_lamp_id_all = ""
            Exit Function
        End If
        lamp_id = StrConv(lamp_id, VbStrConv.Narrow)

        msg = ""
        sql = "select * from box_lamptype_view where control_box_name='" & control_box_name & "' and type_string='" & lamp_type & "'"

        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Com_lamp_id_all" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            Com_lamp_id_all = ""
            conn.Close()
            conn = Nothing
            Exit Function
        End If

        If rs.RecordCount > 0 Then
            If rs.Fields("type_id").Value < 10 Then
                Com_lamp_id_all = Trim(rs.Fields("control_box_id").Value) & "0" & rs.Fields("type_id").Value.ToString & lamp_id
            Else
                If rs.Fields("type_id").Value >= 10 And rs.Fields("type_id").Value < 100 Then
                    Com_lamp_id_all = Trim(rs.Fields("control_box_id").Value) & rs.Fields("type_id").Value.ToString & lamp_id

                Else
                    MsgBox("灯的编号超出最大值", , PROJECT_TITLE_STRING)
                    rs.Close()
                    rs = Nothing
                    Com_lamp_id_all = ""
                    conn.Close()
                    conn = Nothing
                    Exit Function
                End If

            End If

        Else
            Com_lamp_id_all = ""
        End If
        rs.Close()
        rs = Nothing
        conn.Close()
        conn = Nothing
    End Function

    ''' <summary>
    ''' 将原始的路灯编号，转换成电控箱名称+景观灯类型+景观灯编号
    ''' </summary>
    ''' <param name="lamp_id">灯的编号</param>
    ''' <remarks></remarks>
    Public Sub lamp_id_to_detail(ByVal lamp_id As String)
        Dim rs As New ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim conn As New ADODB.Connection
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        msg = ""
        sql = "select * from lamp_street where lamp_id='" & lamp_id & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "lamp_id_to_detail" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If

        If rs.RecordCount > 0 Then
            g_controlboxname = Trim(rs.Fields("control_box_name").Value) '电控箱名称
            g_lamptype = Trim(rs.Fields("type_string").Value) '景观灯类型
            g_shortlampid = Mid(lamp_id, 7, LAMP_ID_LEN) '景观灯编号

        End If
        rs.Close()
        rs = Nothing
        conn.Close()
        conn = Nothing

    End Sub
    ''' <summary>
    ''' 将电流的AD值转换成实际的数值（单灯）,2011年5月19日增加电流AD为2个字节
    ''' </summary>
    ''' <param name="current_AD"></param>
    ''' <remarks></remarks>
    Public Sub Get_current_and_presure_AD2(ByVal current_AD As Integer)
        '******************新版三回路状态分析协议*************************
        g_currentvalue = (current_AD) / &HFFFF / 0.6 * 5
        g_presurevalue = 0
        g_currentvalue = Format(g_currentvalue, "0.00")
        g_presurevalue = Format(g_presurevalue, "0.00")


    End Sub

    ''' <summary>
    ''' 将电流和电压的AD值转换成实际的数值（单灯）
    ''' </summary>
    ''' <param name="current_AD"></param>
    ''' <param name="presure_AD"></param>
    ''' <remarks></remarks>
    Public Sub Get_current_and_presure(ByVal current_AD As Integer, ByVal presure_AD As Integer)
        '淄博的项目中电阻值为求功率因素
        Dim PI As Double  'PI值

        PI = 3.1415926
        If current_AD = 0 Then
            g_presurevalue = 0
        Else
            g_presurevalue = System.Math.Abs(System.Math.Cos(presure_AD * 0.5 Mod 5 * PI / 2))

        End If

finish1:
        '电流值转换

        If current_AD >= 93 Then
            g_currentvalue = 1.53 + (current_AD - 93) * (1.81 - 1.53) / (111 - 93)
            GoTo finish2
        End If
        If current_AD >= 63 And current_AD < 93 Then
            g_currentvalue = 1.05 + (current_AD - 63) * (1.53 - 1.05) / (93 - 63)
            GoTo finish2
        End If
        If current_AD >= 45 And current_AD < 63 Then
            g_currentvalue = 0.76 + (current_AD - 45) * (1.05 - 0.76) / (63 - 45)
            GoTo finish2
        End If
        If current_AD >= 27 And current_AD < 45 Then
            g_currentvalue = 0.47 + (current_AD - 27) * (0.76 - 0.47) / (45 - 27)
            GoTo finish2
        End If
        If current_AD >= 9 And current_AD < 27 Then
            g_currentvalue = 0.18 + (current_AD - 9) * (0.47 - 0.18) / (27 - 9)
            GoTo finish2
        End If
        If current_AD >= 0 And current_AD < 9 Then
            g_currentvalue = (current_AD) * (0.18) / 9
            GoTo finish2
        End If

finish2:
        g_currentvalue = Format(g_currentvalue, "0.00")
        g_presurevalue = Format(g_presurevalue, "0.00")

    End Sub

    '''' <summary>
    '''' 关闭EXCEL进程
    '''' </summary>
    '''' <remarks></remarks>
    'Public Sub ProcessKill()
    '    'Dim p As System.Diagnostics.Process
    '    'p = New System.Diagnostics.Process
    '    'For Each p In System.Diagnostics.Process.GetProcesses()
    '    '    If p.ProcessName.ToUpper() = "EXCEL" Then
    '    '        p.Kill()
    '    '    End If
    '    'Next
    'End Sub

    Public Function ProcessKill(ByVal xlapp As Microsoft.Office.Interop.Excel.Application, ByVal xlbook As Microsoft.Office.Interop.Excel.Workbook, ByVal xlsheet As Microsoft.Office.Interop.Excel.Worksheet)
        If xlapp Is Nothing Then
            Return True
        End If
        If xlsheet Is Nothing Then
            Return True
        End If
        Try
            Dim PreocessExcelId As Integer = 0
            BringWindowToTop(xlapp.Hwnd)
            If xlbook IsNot Nothing Then
                Try
                    xlbook.Close()
                Catch ex As Exception
                    Throw ex
                End Try
            End If
            xlapp.Quit()
            GetWindowThreadProcessId(xlapp.Hwnd, PreocessExcelId)
            If PreocessExcelId > 0 Then
                Dim pExcel = Process.GetProcessById(PreocessExcelId)
                If pExcel IsNot Nothing Then
                    pExcel.Kill()
                    Return True
                End If
            End If
        Catch ex As Exception
            '_xlBook.Close()    
            '_xlApp.Quit()    
        Finally
            xlsheet = Nothing
            xlbook = Nothing
            xlapp = Nothing
        End Try
        Return True
    End Function

    Private Function GetWindowThreadProcessId(ByVal hWnd As IntPtr, ByRef OutPresId As Integer) As Integer
    End Function
    'BringWindowToTop    

    Private Function BringWindowToTop(ByVal hWnd As IntPtr) As Boolean
    End Function


    ''' <summary>
    ''' 判断数据库连接是否
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DB_Connection() As System.Boolean
        Dim conn As New ADODB.Connection
        DB_Connection = DBOperation.OpenConn(conn)
    End Function

    ''' <summary>
    ''' 由灯的编号获取灯杆号
    ''' </summary>
    ''' <param name="lamp_id">路灯编号</param>
    ''' <remarks></remarks>
    Public Sub Get_DengGan(ByVal lamp_id As String)
        g_dengzhuid = (System.Convert.ToInt32(Mid(lamp_id, 7, LAMP_ID_LEN)) - 1) \ g_lampnum + 1
        g_dengzhulampid = System.Convert.ToInt32(Mid(lamp_id, 7, LAMP_ID_LEN)) Mod g_lampnum
        If g_dengzhulampid = 0 Then
            g_dengzhulampid = g_lampnum
        End If
    End Sub

    ''' <summary>
    ''' 将电压逆向置换成两个字节的数值
    ''' </summary>
    ''' <param name="Presure"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Get_EXEPresure(ByVal Presure As Integer, ByVal control_box_id As String) As Integer
        'V = (iVoltage[0]*256+iVoltage[1]) / 0x7fff * 176.78 / 150 * 300(V)
        'Get_Presure = Presure / &H7FFF * 176.78 / 150 * 300
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim box_type As Integer

        box_type = 1
        msg = ""
        sql = "select control_box_type from control_box where control_box_id='" & control_box_id & "'"
        If DBOperation.OpenConn(conn) = False Then
            Exit Function
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Get_Presure" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            Get_EXEPresure = 0
            Exit Function
        End If
        If rs.RecordCount > 0 Then
            box_type = rs.Fields("control_box_type").Value

        End If

        If box_type = 1 Then
            Get_EXEPresure = Presure * &H7FFF / 176.78 * 150 / 300
        Else
            '2011-4-21号更改公式
            Get_EXEPresure = Presure / 500 * &HFFFF

        End If



        If Get_EXEPresure < 10 Then
            Get_EXEPresure = 0
        End If

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Function

    ''' <summary>
    ''' 将电流逆向置换成两个字节的数值
    ''' </summary>
    ''' <param name="current"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Get_EXECurrent(ByVal current As Integer, ByVal control_box_id As String, ByVal bianbi As Integer) As Integer
        'V = (iVoltage[0]*256+iVoltage[1]) / 0x7fff * 176.78 / 150 * 300(V)
        'Get_Presure = Presure / &H7FFF * 176.78 / 150 * 300
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim box_type As Integer

        box_type = 1
        msg = ""
        sql = "select control_box_type from control_box where control_box_id='" & control_box_id & "'"
        If DBOperation.OpenConn(conn) = False Then
            Exit Function
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Get_Presure" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            Get_EXECurrent = 0
            Exit Function
        End If


        If rs.RecordCount > 0 Then
            box_type = rs.Fields("control_box_type").Value
        End If

        If box_type = 1 Then
            Get_EXECurrent = current * &H7FFF / 176.78 * 150 / 5
        Else
            '2011-4-21号更改公式
            Get_EXECurrent = current * &HFFFF / 8.3 / bianbi

        End If


        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Function

    ''' <summary>
    ''' 转换成电压的公式(主控箱)
    ''' </summary>
    ''' <param name="Presure"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Get_Presure(ByVal Presure As Double, ByVal control_box_id As String) As Double
        'V = (iVoltage[0]*256+iVoltage[1]) / 0x7fff * 176.78 / 150 * 300(V)
        'Get_Presure = Presure / &H7FFF * 176.78 / 150 * 300
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim box_type As Integer

        box_type = 1
        msg = ""
        sql = "select control_box_type from control_box where control_box_id='" & control_box_id & "'"
        If DBOperation.OpenConn(conn) = False Then
            Exit Function
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Get_Presure" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            Get_Presure = 0
            Exit Function
        End If
        If rs.RecordCount > 0 Then
            box_type = rs.Fields("control_box_type").Value

        End If

        If box_type = 1 Then
            Get_Presure = Presure / &H7FFF * 176.78 / 150 * 300
        Else
            '2011-4-21号更改公式
            Get_Presure = Presure * 500 / &HFFFF

        End If

        '2012年1月11日电压小于50伏则置为0
        If Get_Presure < 50 Then
            Get_Presure = 0
        End If


        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Function


    ''' <summary>
    ''' 连着四次发现故障则发送报警短信
    ''' </summary>
    ''' <param name="control_box_id"></param>
    ''' <param name="lamp_id"></param>
    ''' <remarks></remarks>
    Public Sub Send_Msg(ByVal control_box_id As String, ByVal lamp_id As String, ByVal alarm_string As String)
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim IMEIstring As String  'IMEI
        Dim Lampidstring As String  '灯的编号
        Dim phonenum As String  '电话号码
        Dim control_box_name As String = "" '主控箱名称
        Dim type As String = "alarm"

        '短信最长150个字符
        If alarm_string.Length > 150 Then
            alarm_string = Mid(alarm_string, 1, 150)
        End If

        IMEIstring = ""
        Lampidstring = ""
        msg = ""
        sql = "select control_box_name,IMEI,Information from Box_IMEI where control_box_id='" & control_box_id & "'"
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Send_Msg" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs.RecordCount > 0 Then
            IMEIstring = rs.Fields("IMEI").Value
            control_box_name = Trim(rs.Fields("control_box_name").Value)
        End If
        If lamp_id <> "" Then
            '单灯故障
            Lampidstring = Val(Mid(lamp_id, 1, 4)).ToString & " . " & Val(Mid(lamp_id, 5, 2)).ToString & " . " & Val(Mid(lamp_id, 7, LAMP_ID_LEN)).ToString & "  " & alarm_string
        Else
            '主控箱故障
            Lampidstring = control_box_name & " " & alarm_string
        End If

        sql = "select phonenum from contact where control_box_name='" & type & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Send_Msg" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        While rs.EOF = False
            phonenum = Trim(rs.Fields("phonenum").Value)
            '短信猫
            sql = "insert into SendGSM_Modem (RoadIMEI, PhoneNumber, SendContent, HandlerFlag, CreateTime)" _
                       & "values('" & IMEIstring & "' , '" & phonenum & "' , '" & Lampidstring & "', 0 , '" & Now & "')"

            'GPRS直接发送
            'sql = "insert into SendGSM (RoadIMEI, PhoneNumber, SendContent, HandlerFlag, CreateTime)" _
            '             & "values('" & IMEIstring & "' , '" & phonenum & "' , '" & Lampidstring & "', 0 , '" & Now & "')"

            DBOperation.ExecuteSQL(conn, sql, msg)
            rs.MoveNext()
        End While

        rs.Close()
        rs = Nothing
        conn.Close()
        conn = Nothing
    End Sub


    ''' <summary>
    ''' 根据类型编号取类型名称
    ''' </summary>
    ''' <param name="type_id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Get_Type_String(ByVal type_id As Integer) As String
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String
        msg = ""
        sql = "select * from lamp_type where type_id='" & type_id & "'"
        If DBOperation.OpenConn(conn) = False Then
            Get_Type_String = ""
            Exit Function
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        Get_Type_String = ""
        If rs.RecordCount > 0 Then
            Get_Type_String = Trim(rs.Fields("type_string").Value)
        End If

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Function

    Public Function Get_Type_id(ByVal type_string As String) As Integer
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String
        msg = ""
        sql = "select * from lamp_type where type_string='" & type_string & "'"
        If DBOperation.OpenConn(conn) = False Then
            Exit Function
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        Get_Type_id = 0
        If rs.RecordCount > 0 Then
            Get_Type_id = Trim(rs.Fields("type_id").Value)
        End If

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Function

    ''' <summary>
    ''' 将控制主控节点的方法转换叫法，如单灯开，或打开叫闭合，单灯闭或关闭叫断开
    ''' </summary>
    ''' <param name="control_string"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Change_Controlboxcontrol(ByVal control_string As String) As String
        Change_Controlboxcontrol = ""
        If control_string = "单灯闭" Then
            Change_Controlboxcontrol = "断开"
        End If
        If control_string = "单灯开" Then
            Change_Controlboxcontrol = "吸合"
        End If
        If control_string = "关闭" Then
            Change_Controlboxcontrol = "断开"
        End If
        If control_string = "打开" Then
            Change_Controlboxcontrol = "吸合"
        End If
        If control_string = "暗" Then
            Change_Controlboxcontrol = "断开"
        End If
        If control_string = "亮" Then
            Change_Controlboxcontrol = "吸合"
        End If
    End Function

    ''' <summary>
    ''' 将当前时间下放 2011年2月16日增加年、月、日
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub sendTime()
        Dim time As System.DateTime
        Dim hour As String
        Dim min As String
        Dim second As String
        'Dim year As String
        'Dim month As String
        'Dim day As String
        Dim sendString As String
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim rs As New ADODB.Recordset

        time = Now
        hour = Com_inf.Dec_to_Hex(time.Hour, 2)
        min = Com_inf.Dec_to_Hex(time.Minute, 2)
        second = Com_inf.Dec_to_Hex(time.Second, 2)
        'year = Com_inf.Dec_to_ox(time.Year, 4)
        'month = Com_inf.Dec_to_ox(time.Month, 2)
        'day = Com_inf.Dec_to_ox(time.Day, 2)

        sendString = hour & " " & min & " " & second
        msg = ""
        sql = "select distinct(IMEI) from RoadIDAndIMEI"
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "sendTime" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        While rs.EOF = False
            sql = "insert into TimeControl(RoadIMEI, CMDType, CMDContent, HandlerFlag, Createtime) values('" & Trim(rs.Fields("IMEI").Value) & "', 2, '" & sendString & "', 0, '" & Now & "')"
            DBOperation.ExecuteSQL(conn, sql, msg)
            rs.MoveNext()
        End While


        If rs.State Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    '''  根据灯的类型将灯的十进制编号转换成2进制，基本思想是：0类型的灯编号可以编码最多20000个灯号
    ''' 1-10类型的灯号停用，11-31类型的灯号最大可使用2000个编号
    ''' </summary>
    ''' <param name="type_string">类型的编号</param>
    ''' <param name="lamp_id">灯的编号</param>
    ''' <remarks></remarks>
    Public Function Get_lampid_bin(ByVal type_string As String, ByVal lamp_id As Integer) As String
        Dim type_id As Integer
        type_id = type_string

        If type_id = 0 Then
            If lamp_id > LAMP_ID_MAX Then
                MsgBox("0类型节点编号不可以大于" & LAMP_ID_MAX, , PROJECT_TITLE_STRING)
                Get_lampid_bin = ""
                Exit Function
            End If
            Get_lampid_bin = Com_inf.Dec_to_Bin(lamp_id, 16)

        Else
            'If type_id >= 1 And type_id <= 10 Then
            '    MsgBox("1-10类型节点编号不可以使用", , PROJECT_TITLE_STRING)
            '    Get_lampid_bin = ""
            '    Exit Function
            'End If
            If lamp_id > 2000 Then
                MsgBox("11-31最大节点编号超过2000", , PROJECT_TITLE_STRING)
                Get_lampid_bin = ""
                Exit Function
            End If
            Get_lampid_bin = Com_inf.Dec_to_Bin(type_id, 5) & Com_inf.Dec_to_Bin(lamp_id, 11)
        End If


    End Function

    ''' <summary>
    ''' 将当前的操作字符串增加到记录中以便查询
    ''' </summary>
    ''' <param name="operation_string"></param>
    ''' <remarks></remarks>
    Public Sub Insert_Operation(ByVal operation_string As String)
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String
        msg = ""
        sql = "insert into operation_record (user_name,operation,createtime) values('" & g_username & "','" & operation_string & "','" & Now & "')"
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        DBOperation.ExecuteSQL(conn, sql, msg)

        conn.Close()
        conn = Nothing

    End Sub

    ''' <summary>
    ''' 根据电控箱灯编号，获取电控箱名称
    ''' </summary>
    ''' <param name="control_box_id"></param>
    ''' <remarks></remarks>
    Public Function Get_box_name(ByVal control_box_id As String) As String
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String

        msg = ""
        sql = "select control_box_name from control_box where control_box_id='" & control_box_id & "'"
        If DBOperation.OpenConn(conn) = False Then
            Get_box_name = ""
            Exit Function
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Get_box_name" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            Get_box_name = ""
            Exit Function
        End If
        If rs.RecordCount > 0 Then
            Get_box_name = Trim(rs.Fields("control_box_name").Value)
        Else
            Get_box_name = ""
        End If

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Function


    ''' <summary>
    ''' 根据电控箱名称，获取电控箱灯编号
    ''' </summary>
    ''' <param name="control_box_name"></param>
    ''' <remarks></remarks>
    Public Function get_box_id(ByVal control_box_name As String) As String
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String

        msg = ""
        sql = "select control_box_id from control_box where control_box_name='" & control_box_name & "'"
        If DBOperation.OpenConn(conn) = False Then
            get_box_id = ""
            Exit Function
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Get_box_id" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            get_box_id = ""
            Exit Function
        End If
        If rs.RecordCount > 0 Then
            get_box_id = Trim(rs.Fields("control_box_id").Value)
        Else
            get_box_id = ""
        End If

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Function
    ''' <summary>
    ''' 根据类型编号，获取类型名称
    ''' </summary>
    ''' <param name="type_id"></param>
    ''' <remarks></remarks>
    Public Function Get_lamp_type(ByVal type_id As String) As String
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String

        msg = ""
        sql = "select type_string from lamp_type where type_id='" & type_id & "'"
        If DBOperation.OpenConn(conn) = False Then
            Get_lamp_type = ""
            Exit Function
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Get_lamp_type" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            Get_lamp_type = ""
            Exit Function
        End If
        If rs.RecordCount > 0 Then
            Get_lamp_type = Trim(rs.Fields("type_string").Value)
        Else
            Get_lamp_type = ""
        End If

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Function

    ''' <summary>
    ''' 记录通信状态和供电是否正常，正常一个时间段，不正常一个时间段
    ''' </summary>
    ''' <param name="control_box_name">主控箱名称</param>
    ''' <param name="state">记录的状态</param>
    ''' <param name="inf" >记录的种类：通信，供电，状态，数据</param>
    ''' <remarks></remarks>
    Public Sub Setcontrolbox_Record(ByVal control_box_name As String, ByVal state As String, ByVal inf As String)
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String
        Dim rs As New ADODB.Recordset
        Dim id As Integer


        sql = ""
        msg = ""
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        sql = "select * from control_box_state where control_box_name='" & control_box_name & "' and state='0' and kaiguan_string='" & inf & "' order by id"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Communication_Record" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs.RecordCount > 1 Then
            '偶然插入两条相同记录
            id = rs.Fields("ID").Value
            sql = "delete from control_box_state where control_box_name='" & control_box_name & "' and state='0' and kaiguan_string='" & inf & "' and ID<>'" & id & "'"
            DBOperation.ExecuteSQL(conn, sql, msg)
        End If

        If rs.RecordCount > 0 Then
            If Trim(state) = "" Then
                '通信不正常的时候
                sql = "update control_box_state set state='1', StatusContent2='" & Now.ToString & "' where control_box_name='" & control_box_name & "' and state='0' and kaiguan_string='" & inf & "'"
                DBOperation.ExecuteSQL(conn, sql, msg)
            Else
                If Trim(state) = "未连接" Then
                    '通信不正常的时候
                    sql = "update control_box_state set state='1', StatusContent2='" & Now.ToString & "' where control_box_name='" & control_box_name & "' and state='0' and kaiguan_string<>'" & inf & "'"
                    DBOperation.ExecuteSQL(conn, sql, msg)
                End If

                '如果目前通信正常，原来的状态是通信不正常的，则要重新招测一下
                'If Trim(state) = "通信正常" Then
                '    communication_string = Trim(rs.Fields("StatusContent").Value)
                '    If Trim(state) <> communication_string Then

                '    End If

                'End If


                If Trim(rs.Fields("StatusContent").Value) <> Trim(state) Then

                    '找到未结束的状态数据，但状态不一致，则修改状态标志位及新建一条状态数据
                    'rs.Fields("state").Value = 1
                    'Dim s As String = Trim(rs.Fields("StatusContent").Value)
                    'rs.Fields("StatusContent2").Value = Now.ToString 'StatusContent2记录结束时间
                    'rs.Update()
                    sql = "update control_box_state set state='1', StatusContent2='" & Now.ToString & "' where control_box_name='" & control_box_name & "' and state='0' and kaiguan_string='" & inf & "'"
                    DBOperation.ExecuteSQL(conn, sql, msg)

                    '新增一条记录
                    sql = "insert into control_box_state(control_box_name,StatusContent,Createtime,state,kaiguan_string) values('" & control_box_name & "','" & state & "'" _
                    & ", '" & Format(Now, "yyyy-MM-dd HH:mm:ss") & "','0','" & inf & "')"
                    DBOperation.ExecuteSQL(conn, sql, msg)

                    'rs.AddNew()
                    'rs.Fields("control_box_name").Value = control_box_name
                    'rs.Fields("StatusContent").Value = state
                    'rs.Fields("Createtime").Value = Format(Now, "yyyy-MM-dd HH:mm:ss")
                    'rs.Fields("state").Value = "0"
                    'rs.Fields("kaiguan_string").Value = inf
                    'rs.Update()



                End If

            End If
        Else
            '没有相关状态的数据
            '新增一条记录
            'rs.AddNew()
            'rs.Fields("control_box_name").Value = control_box_name
            'rs.Fields("StatusContent").Value = state
            'rs.Fields("Createtime").Value = Now
            'rs.Fields("state").Value = "0"
            'rs.Fields("kaiguan_string").Value = inf
            'rs.Update()

            sql = "insert into control_box_state(control_box_name,StatusContent,Createtime,state,kaiguan_string) values('" & control_box_name & "','" & state & "'" _
               & ", '" & Format(Now, "yyyy-MM-dd HH:mm:ss") & "','0','" & inf & "')"
            DBOperation.ExecuteSQL(conn, sql, msg)

        End If

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing

    End Sub

    Public Sub Select_box_id(ByVal box_combox As System.Windows.Forms.ComboBox)
        Dim rs As New ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim conn As New ADODB.Connection

        msg = ""
        sql = "select control_box_id from control_box order by control_box_id"
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        box_combox.Items.Clear()
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Select_box_id" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        While rs.EOF = False
            box_combox.Items.Add(Trim(rs.Fields("control_box_id").Value))
            rs.MoveNext()
        End While

        If box_combox.Items.Count > 0 Then
            box_combox.SelectedIndex = 0
        End If
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing

    End Sub

    ''' <summary>
    ''' 从一个字符串中获取其中的数字
    ''' </summary>
    ''' <param name="input_string"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Get_Number(ByVal input_string As String) As String
        Dim temp_num As String
        Dim ch As Char
        temp_num = ""
        Dim i As Integer = 0
        While i < input_string.Length
            ch = input_string(i)
            If ch >= "0" And ch <= "9" Then
                temp_num &= ch
            End If
            i += 1
        End While
        Get_Number = temp_num
    End Function

    ''' <summary>
    ''' 选择主控箱的名称，保持在选择第三层上
    ''' </summary>
    ''' <param name="tnParent"></param>
    ''' <param name="checklist"></param>
    ''' <remarks></remarks>
    Public Sub FindNode(ByVal tnParent As TreeNode, ByVal checklist As ArrayList)
        If tnParent Is Nothing Then
            Exit Sub
        End If

        If tnParent.Level = 3 And tnParent.Checked = True Then
            checklist.Add(Trim(tnParent.Text).Split(" ")(0))
        End If

        Dim tempNode As New TreeNode
        For Each tn As TreeNode In tnParent.Nodes
            FindNode(tn, checklist)

            'If tempNode IsNot Nothing Then
            '    Exit For
            'End If
        Next
    End Sub

    ''' <summary>
    ''' 设置节点的选中标志
    ''' </summary>
    ''' <param name="tnParent"></param>
    ''' <remarks></remarks>
    Public Sub SetControlNode(ByVal tnParent As TreeNode, ByVal checktag As Boolean)
        If tnParent Is Nothing Then
            Exit Sub
        End If

        If tnParent.Checked = True Then
            tnParent.Checked = False

        End If

        For Each tn As TreeNode In tnParent.Nodes
            SetControlNode(tn, checktag)

            'If tempNode IsNot Nothing Then
            '    Exit For
            'End If
        Next
    End Sub

    ''' <summary>
    ''' 选择选中的对象名称，增加到array_list中，并记录级别
    ''' </summary>
    ''' <param name="tnParent"></param>
    ''' <param name="checklist"></param>
    ''' <remarks></remarks>
    Public Sub FindControlNode(ByVal tnParent As TreeNode, ByVal checklist As ArrayList)
        If tnParent Is Nothing Then
            Exit Sub
        End If

        If tnParent.Checked = True Then
            Dim data(1) As String
            If tnParent.Level = 4 Then
                data(0) = tnParent.Parent.Text.Split(" ")(0) & " " & tnParent.Text.Split(" ")(0)
            Else
                If tnParent.Level = 5 Then
                    data(0) = tnParent.Text.Split(" ")(1)
                Else
                    data(0) = tnParent.Text.Split(" ")(0)
                End If
            End If

            data(1) = tnParent.Level
            checklist.Add(data)
        End If

        Dim tempNode As New TreeNode
        For Each tn As TreeNode In tnParent.Nodes
            FindControlNode(tn, checklist)

            'If tempNode IsNot Nothing Then
            '    Exit For
            'End If
        Next
    End Sub

    ''' <summary>
    ''' 将主控箱名称记录到列表中
    ''' </summary>
    ''' <param name="controlboxname"></param>
    ''' <param name="dgv_box"></param>
    ''' <param name="rowname"></param>
    ''' <remarks></remarks>
    Public Sub Addcontrolbox_to_Datagridview(ByVal controlboxname As ArrayList, ByVal dgv_box As System.Windows.Forms.DataGridView, ByVal rowname As String)
        Dim array_boxname(controlboxname.Count - 1) As String
        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim row As Integer = 0
        controlboxname.CopyTo(array_boxname)
        dgv_box.Rows.Clear()
        While i < array_boxname.Length
            dgv_box.Rows.Add()
            dgv_box.Rows(i).Cells(rowname).Value = array_boxname(i)
            i += 1
        End While

    End Sub

    ''' <summary>
    ''' 将控制对象的名称记录到列表中
    ''' </summary>
    ''' <param name="controlobj"></param>
    ''' <param name="dgv_box"></param>
    ''' <param name="rowname"></param>
    ''' <remarks></remarks>
    Public Sub Addcontrolobj_to_Datagridview(ByVal controlobj As ArrayList, ByVal dgv_box As System.Windows.Forms.DataGridView, ByVal rowname As String, ByVal control_level As String)
        Dim array_objname(1) As String
        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim row As Integer = 0
        dgv_box.Rows.Clear()

        While i < controlobj.Count
            array_objname = controlobj(i)

            j = 0
            While j < dgv_box.RowCount
                If array_objname(0) = dgv_box.Rows(j).Cells(rowname).Value Then
                    Exit While
                End If
                j += 1
            End While
            If j >= dgv_box.RowCount Then
                '增加控制对象的名称
                row = dgv_box.RowCount
                dgv_box.Rows.Add()
                dgv_box.Rows(i).Cells(rowname).Value = array_objname(0)
                dgv_box.Rows(i).Cells(control_level).Value = array_objname(1)
            End If
            i += 1
        End While

    End Sub

    ''' <summary>
    ''' 查询节日模式表，看当天是否有主控箱处于节假日模式，有则标志为holiday,没有则标志为normal
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Set_holiday_mod()
        Dim conn As New ADODB.Connection
        Dim rs As New ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim time As DateTime
        Dim control_box_id As String
        Dim mod_tag As String = "正常"

        msg = ""


        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If


        '将所有主控箱的节假日标志位复位为正常
        sql = "update control_box set elec_state='" & mod_tag & "'"
        DBOperation.ExecuteSQL(conn, sql, msg)

        sql = "SELECT Special_div_time.Time, Special_road_level.control_box_id FROM  Special_div_time INNER JOIN Special_road_level ON Special_div_time.name = Special_road_level.div_time_level"

        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Get_lamp_type" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        While rs.EOF = False
            time = rs.Fields("Time").Value
            control_box_id = Trim(rs.Fields("control_box_id").Value)
            mod_tag = "正常"
            If time.Year = Now.Year And time.Month = Now.Month And time.Day = Now.Day Then
                mod_tag = "节日"
                sql = "update control_box set elec_state='" & mod_tag & "' where control_box_id='" & control_box_id & "'"
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
    ''' 根据电控箱灯编号，获取电控箱是否处于节假日控制模式
    ''' </summary>
    ''' <param name="control_box_id"></param>
    ''' <remarks></remarks>
    Public Function Get_box_holidaymod(ByVal control_box_id As String) As Boolean
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String
        Dim holiday_tag As String = "节日"

        msg = ""
        Get_box_holidaymod = False
        sql = "select elec_state from control_box where control_box_id='" & control_box_id & "' and elec_state='" & holiday_tag & "'"
        If DBOperation.OpenConn(conn) = False Then
            Get_box_holidaymod = False
            Exit Function
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Get_box_name" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            Get_box_holidaymod = False
            Exit Function
        End If
        If rs.RecordCount > 0 Then
            Get_box_holidaymod = True
        End If

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Function

    ''' <summary>
    ''' 通过电控箱名称和回路编号查询对应的接触器编号
    ''' </summary>
    ''' <param name="huilu_id"></param>
    ''' <param name="control_box_name"></param>
    ''' <remarks></remarks>
    Public Function get_jiechuqi_id(ByVal huilu_id As Integer, ByVal control_box_name As String) As String
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String

        msg = ""
        sql = "select jiechuqi_id, open_close from huilu_inf where huilu_id=" & huilu_id & " and control_box_name='" & control_box_name & "'"
        If DBOperation.OpenConn(conn) = False Then
            get_jiechuqi_id = "0"
            Exit Function
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Get_box_name" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            get_jiechuqi_id = "0"
            Exit Function
        End If

        If rs.RecordCount > 0 Then
            get_jiechuqi_id = Trim(rs.Fields("jiechuqi_id").Value) & " " & Trim(rs.Fields("open_close").Value)
        Else
            get_jiechuqi_id = "0"
        End If

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing


    End Function

    Public Function get_jiechuqi_state(ByVal control_box_name As String)
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim jiechuqi_id As String
        Dim state As String

        msg = ""
        sql = "select * from lamp_street where type_id=31 and control_box_name='" & control_box_name & "' order by lamp_id"
        If DBOperation.OpenConn(conn) = False Then
            get_jiechuqi_state = ""
            Exit Function
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Get_box_name" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)
            get_jiechuqi_state = ""
            conn.Close()
            conn = Nothing
            Exit Function
        End If

        get_jiechuqi_state = ""
        While rs.EOF = False
            jiechuqi_id = Val(Mid(Trim(rs.Fields("lamp_id").Value), 7, LAMP_ID_LEN)).ToString
            state = "断开"
            If rs.Fields("state").Value = 0 Or rs.Fields("state").Value = 3 Then
                '表示断开
                state = "断开"
                'sql = "update huilu_inf set open_close='" & state & "' where control_box_name='" & Trim(rs.Fields("control_box_name").Value) & "' and jiechuqi_id='" & jiechuqi_id & "'"
                'DBOperation.ExecuteSQL(conn, sql, msg)

            End If
            If rs.Fields("state").Value = 1 Or rs.Fields("state").Value = 4 Then
                '表示断开
                state = "吸合"
                'sql = "update huilu_inf set open_close='" & state & "' where control_box_name='" & Trim(rs.Fields("control_box_name").Value) & "' and jiechuqi_id='" & jiechuqi_id & "'"
                'DBOperation.ExecuteSQL(conn, sql, msg)

            End If
            jiechuqi_id = "K" & jiechuqi_id
            get_jiechuqi_state &= jiechuqi_id & "-" & state & " "
            rs.MoveNext()
        End While

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing

    End Function

    ''' <summary>
    ''' 取当前主控箱的接触器状态，配置到huilu_inf表中
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub get_jiechuqi_state()
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim jiechuqi_id As String
        Dim state As String

        msg = ""
        sql = "select * from lamp_street where type_id=31 order by control_box_id"
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Get_box_name" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        While rs.EOF = False
            jiechuqi_id = Val(Mid(Trim(rs.Fields("lamp_id").Value), 7, LAMP_ID_LEN)).ToString
            If rs.Fields("state").Value = 0 Or rs.Fields("state").Value = 3 Then
                '表示断开
                state = "断开"
                sql = "update huilu_inf set open_close='" & state & "' where control_box_name='" & Trim(rs.Fields("control_box_name").Value) & "' and jiechuqi_id='" & jiechuqi_id & "'"
                DBOperation.ExecuteSQL(conn, sql, msg)

            End If
            If rs.Fields("state").Value = 1 Or rs.Fields("state").Value = 4 Then
                '表示断开
                state = "吸合"
                sql = "update huilu_inf set open_close='" & state & "' where control_box_name='" & Trim(rs.Fields("control_box_name").Value) & "' and jiechuqi_id='" & jiechuqi_id & "'"
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
    ''' 获取特殊控制模式的控制名称
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub get_divtime_name(ByVal control_obj As System.Windows.Forms.ComboBox, ByVal special_control As Integer)
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String

        msg = ""
        DBOperation.OpenConn(conn)
        If special_control = 0 Then
            '半夜模式
            sql = "select distinct div_level from div_time where hour_end=0"
        Else
            '特殊模式
            sql = "select distinct div_level from div_time where hour_end is null"
        End If

        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "set_mod_list_DropDown", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If

        control_obj.Items.Clear()
        While rs.EOF = False
            control_obj.Items.Add(Trim(rs.Fields("div_level").Value))
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
    ''' 根据组号及主控箱编号，获取三遥各类数据
    ''' </summary>
    ''' <param name="group_id"></param>
    ''' <param name="control_box_id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function get_controlboxstate(ByVal group_id As Integer, ByVal control_box_id As String) As String
        Dim conn As New ADODB.Connection
        Dim rs As New ADODB.Recordset
        Dim sql As String
        Dim msg As String
        Dim statestring As String = ""
        Dim boardnum As Integer = 0

        msg = ""
        sql = "select * from control_box where control_box_id='" & control_box_id & "'"
        If DBOperation.OpenConn(conn) = False Then
            get_controlboxstate = ""
            Exit Function
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING, , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            get_controlboxstate = ""
            Exit Function
        End If
        If rs.RecordCount > 0 Then
            boardnum = rs.Fields("board_num").Value

            If group_id = 0 Then
                statestring = Trim(rs.Fields("state").Value) & "+" & Trim(rs.Fields("StatusContent").Value)
            Else
                If group_id = 1 Then
                    statestring = Trim(rs.Fields("state").Value) & "+" & Trim(rs.Fields("StatusContent2").Value)
                Else
                    statestring = Trim(rs.Fields("state").Value) & "+" & Trim(rs.Fields("StatusContent3").Value)
                End If
            End If

        End If
        get_controlboxstate = statestring

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing

    End Function

    ''' <summary>
    ''' 将RoadLightFlag表中之前遗留的未被置为的状态及开关量数据置位
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub setstatueflag()

        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String

        sql = "update RoadLightStatus set HandlerFlag=1 where (PackType='" & HG_TYPE.HG_GET_YAOCE & "' or PackType='" & HG_TYPE.HG_GET_KAIGUAN & "') and HandlerFlag=3"
        msg = ""
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        DBOperation.ExecuteSQL(conn, sql, msg)


        conn.Close()
        conn = Nothing

    End Sub

    ''' <summary>
    ''' 根据lamp_id获取单灯的协议类型，电流1字节，2字节及6字节的
    ''' </summary>
    ''' <param name="lamp_id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getlampprotocletype(ByVal lamp_id) As String
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String

        msg = ""
        If DBOperation.OpenConn(conn) = False Then
            getlampprotocletype = "1"
            Exit Function
        End If
        sql = "select jiechuqi_id from lamp_inf where lamp_id='" & lamp_id & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            conn.Close()
            conn = Nothing
            getlampprotocletype = "1"
            Exit Function
        End If
        If rs.RecordCount > 0 Then
            If rs.Fields("jiechuqi_id").Value Is System.DBNull.Value Then
                getlampprotocletype = "1"
            Else
                getlampprotocletype = Trim(rs.Fields("jiechuqi_id").Value)

            End If
        Else
            getlampprotocletype = "-1"  '表示没这个灯
        End If

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Function

    ''' <summary>
    ''' 有控制命令后，将状态表中未处理的开关量和三遥数据置为已处理状态，
    '''防止有控制之前的数据对故障判断造成干扰
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub clearstatus(ByVal order As String)
        Dim sql As String
        Dim msg As String
        Dim conn As New ADODB.Connection
        Dim boxid As String
        Dim orderstring() As String
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If

        orderstring = order.Split(" ")
        boxid = orderstring(orderstring.Length - 1) & " " & orderstring(0)

        msg = ""
        If order = "" Then
            sql = "update RoadLightStatus set HandlerFlag=1 where (Packtype='" & _
                        HG_TYPE.HG_HOST_SANYAO_AUTO & "' or Packtype='" & HG_TYPE.HG_HOST_SW_AUTO & "' or Packtype='" & HG_TYPE.HG_HOST_NEWSAYAO_AUTO & "' or Packtype='" & HG_TYPE.HG_GET_KAIGUAN & "')" _
                        & "and (HandlerFlag=0 or HandlerFlag=3)"
        Else
            sql = "update RoadLightStatus set HandlerFlag=1 where  StatusContent like '" & boxid & "%' and (Packtype='" & _
            HG_TYPE.HG_HOST_SANYAO_AUTO & "' or Packtype='" & HG_TYPE.HG_HOST_SW_AUTO & "' or Packtype='" & HG_TYPE.HG_HOST_NEWSAYAO_AUTO & "' or Packtype='" & HG_TYPE.HG_GET_KAIGUAN & "')" _
            & "and (HandlerFlag=0 or HandlerFlag=3)"
        End If
      
        DBOperation.ExecuteSQL(conn, sql, msg)

        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    ''' 将时段控制字符串写入到control_box_state表中
    ''' </summary>
    ''' <param name="divtime_state"></param>
    ''' <remarks></remarks>
    Public Sub insert_state(ByVal divtime_state As String, ByVal control_box_name As String)
        Dim sql As String
        Dim msg As String
        Dim conn As New ADODB.Connection
        Dim rs As New ADODB.Recordset
        Dim state_string As String = "时段"
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If

        msg = ""
        sql = "select * from control_box_state where kaiguan_string='" & state_string & "' and control_box_name='" & control_box_name & "'"
        DBOperation.ExecuteSQL(conn, sql, msg)

        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING, , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If

        If rs.RecordCount > 0 Then
            rs.Fields("StatusContent").Value = divtime_state
            rs.Fields("Createtime").Value = Now
            rs.Update()
        Else
            rs.AddNew()
            rs.Fields("control_box_name").Value = control_box_name
            rs.Fields("StatusContent").Value = divtime_state
            rs.Fields("Createtime").Value = Now
            rs.Fields("kaiguan_string").Value = "时段"
            rs.Update()
        End If

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If

        conn.Close()
        conn = Nothing
    End Sub

    Public Function get_boxstate(ByVal control_box_name As String)
        Dim sql As String
        Dim msg As String
        Dim conn As New ADODB.Connection
        Dim rs As New ADODB.Recordset
        Dim state_string As String = "时段"

        get_boxstate = ""
        If DBOperation.OpenConn(conn) = False Then
            Exit Function
        End If

        msg = ""
        sql = "select * from control_box_state where kaiguan_string='" & state_string & "' and control_box_name='" & control_box_name & "'"
        DBOperation.ExecuteSQL(conn, sql, msg)

        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING, , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Function
        End If

        If rs.RecordCount > 0 Then
            get_boxstate = Trim(rs.Fields("StatusContent").Value)

        End If

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If

        conn.Close()
        conn = Nothing
    End Function

    ''' <summary>
    ''' 将控制命令转换成文字叙述
    ''' </summary>
    ''' <param name="order_string"></param>
    ''' <remarks></remarks>
    Public Function controlorder_string(ByVal order_string As String, ByVal order_num As Integer) As String
        Dim data() As String
        Dim i As Integer = 0
        Dim time_string As String = ""
        Dim lampid_string As String = ""
        Dim control_string As String = ""
        Dim boxid, short_str As String

        data = order_string.Split(" ")
        controlorder_string = ""
        While i < order_num
            time_string = System.Convert.ToInt32(data(i * 11), 16) & ":" & System.Convert.ToInt32(data(i * 11 + 1), 16) & ":" & System.Convert.ToInt32(data(i * 11 + 2), 16)
            boxid = Val("&H" & (data(i * 11 + 3) & data(i * 11 + 4))).ToString   '第4个字节并上第5个字节转换为四位的十进制电控箱编号
            short_str = Control_Hex_to_Dec(data(i * 11 + 5) & data(i * 11 + 6))
            lampid_string = boxid & "-" & Val(Mid(short_str, 1, 2)).ToString & "-" & Val(Mid(short_str, 3, LAMP_ID_LEN)).ToString

            If data(i * 11 + 7) = "1B" Then
                '回路开
                control_string = "回路开"
            End If
            If data(i * 11 + 7) = "1C" Then
                '回路开
                control_string = "回路关"
            End If
            If data(i * 11 + 7) = "41" Then
                '回路开
                control_string = "类型全开"
            End If
            If data(i * 11 + 7) = "42" Then
                '回路开
                control_string = "类型全闭"
            End If
            If data(i * 11 + 7) = "43" Then
                '回路开
                control_string = "类型奇开"
            End If
            If data(i * 11 + 7) = "45" Then
                '回路开
                control_string = "类型偶开"
            End If

            controlorder_string &= time_string & " " & lampid_string & " " & control_string & vbCrLf
            i += 1
        End While

    End Function

    ''' <summary>
    ''' 获取设置的经度的值
    ''' </summary>
    ''' <remarks></remarks>
    Public Function get_jingduvalue() As Double
        Dim conn As New ADODB.Connection
        Dim sql, msg As String
        Dim rs As New ADODB.Recordset
        Dim type As String = "经度"
        get_jingduvalue = 118.03  '默认情况为淄博经度

        sql = ""
        msg = ""
        sql = "select * from sysconfig where type='" & type & "'"
        If DBOperation.OpenConn(conn) = False Then
            Exit Function
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING, , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Function
        End If
        If rs.RecordCount > 0 Then
            get_jingduvalue = System.Convert.ToDouble(Trim(rs.Fields("name").Value))
        End If

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing

    End Function

    ''' <summary>
    ''' 获取设置的纬度的值
    ''' </summary>
    ''' <remarks></remarks>
    Public Function get_weiduvalue() As Double
        Dim conn As New ADODB.Connection
        Dim sql, msg As String
        Dim rs As New ADODB.Recordset
        Dim type As String = "纬度"
        get_weiduvalue = 36.48  '默认情况为淄博纬度

        sql = ""
        msg = ""
        sql = "select * from sysconfig where type='" & type & "'"
        If DBOperation.OpenConn(conn) = False Then
            Exit Function
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING, , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Function
        End If
        If rs.RecordCount > 0 Then
            get_weiduvalue = System.Convert.ToDouble(Trim(rs.Fields("name").Value))
        End If

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing

    End Function

    ''' <summary>
    ''' 设置每年的日出日落表
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub set_suntime()
        Dim bLeap As Boolean '是否为闰年
        Dim suntimeobj As New SunTime
        Dim month() As Integer  '每月的天数
        Dim month_num As Integer '月份
        Dim row As Integer  '列表的行和列
        Dim risetime, downtime As DateTime  '日出及日落时间
        Dim i As Integer
        Dim sql As String
        Dim msg As String
        Dim conn As New ADODB.Connection
        Dim nowtime As DateTime = Now  '当前的时间
        Dim jingdu, weidu As Double '经纬度
        Dim now_year As Integer
        now_year = nowtime.Year
        Dim mothList() As Integer = {31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31}  '非闰年
        Dim leapList() As Integer = {31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31}  '闰年

        jingdu = get_jingduvalue()
        weidu = get_weiduvalue()

        DBOperation.OpenConn(conn)
        msg = ""

        bLeap = suntimeobj.IsLeapYear(nowtime.Year)
        If bLeap = True Then  '闰年
            month = leapList
        Else   '非闰年
            month = mothList
        End If
        '删除原先的日出日落表
        sql = "delete from suntime"
        DBOperation.ExecuteSQL(conn, sql, msg)
        month_num = 0 '初始化从1月开始

        i = 0

        While month_num < 12
            row = 0
            i = 0
            While row < month(month_num)

                risetime = suntimeobj.GetSunrise(weidu, jingdu, now_year, month_num + 1, row + 1)
                downtime = suntimeobj.GetSunset(weidu, jingdu, now_year, month_num + 1, row + 1)
                i += 1
                row += 1

                '将当前查询的日出日落时间记录到数据库中，作为时控的基本时间
                sql = "insert into suntime(time,mod) values ('" & risetime & "', '关')"  '记录关灯时间
                DBOperation.ExecuteSQL(conn, sql, msg)

                sql = "insert into suntime(time,mod) values ('" & downtime & "', '开')"  '记录开灯时间
                DBOperation.ExecuteSQL(conn, sql, msg)
            End While
            month_num += 1
        End While

        '将修改经纬度的操作记录记录到数据库中
        Com_inf.Insert_Operation("修改日出日落表，时间：" & now_year & ", 经度：" & jingdu.ToString & " ,纬度：" & weidu.ToString)


        conn.Close()
        conn = Nothing
    End Sub

    Public Function get_alarmdelaytime() As String
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String
        Dim setvalue As String = "报警延时"

        msg = ""
        sql = "select * from sysconfig where type='" & setvalue & "'"
        If DBOperation.OpenConn(conn) = False Then
            get_alarmdelaytime = 1
            Exit Function
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING, , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            get_alarmdelaytime = 1
            Exit Function
        End If
        If rs.RecordCount > 0 Then
            get_alarmdelaytime = rs.Fields("name").Value

        Else
            get_alarmdelaytime = 1
        End If



        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Function

    Public Sub Select_all_lamptype(ByVal lamptype As System.Windows.Forms.ComboBox)

        Dim rs As ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim conn As New ADODB.Connection  '数据库连接
        DBOperation.OpenConn(conn)

        msg = ""
        sql = "select * from lamp_type order by type_id"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Select_all_lamptype" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        lamptype.Items.Clear()
        While rs.EOF = False
            lamptype.Items.Add(Trim(rs.Fields("type_string").Value))
            rs.MoveNext()
        End While
        If lamptype.Items.Count > 0 Then
            lamptype.SelectedIndex = 0

        End If
        rs.Close()
        rs = Nothing
        conn.Close()
        conn = Nothing
    End Sub


    ''' <summary>
    ''' 判断是否是有控制权限的电话号码
    ''' </summary>
    ''' <param name="phone"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getphonenum(ByVal phone As String) As Boolean
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String
        Dim setvalue As String = "control"

        msg = ""
        getphonenum = False
        Dim len As Integer = Trim(phone).Length
        If len < 11 Or len > 15 Then
            getphonenum = False
            Exit Function
        End If
        sql = "select * from contact where control_box_name='" & setvalue & "' and Phonenum='" & phone & "'"
        If DBOperation.OpenConn(conn) = False Then
            getphonenum = False
            Exit Function
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            ' MsgBox(MSG_ERROR_STRING, , PROJECT_TITLE_STRING)

            conn.Close()
            conn = Nothing
            getphonenum = False
            Exit Function
        End If
        If rs.RecordCount > 0 Then
            getphonenum = True

        Else
            getphonenum = False
        End If



        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Function

    ''' <summary>
    ''' 获取抄表的设置
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub get_chaobiaoconfig()
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String = ""
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If

        '设置电表的设置是否为自动抄表
        sql = "select * from sysconfig where type='自动抄表'"

        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            conn.Close()
            conn = Nothing
            g_chaobiaotag = False
            Exit Sub
        End If
        If rs.RecordCount > 0 Then
            If rs.Fields("name").Value = 0 Then
                g_chaobiaotag = False
            Else
                g_chaobiaotag = True
            End If
        Else
            g_chaobiaotag = False
        End If


        '按月进行抄表
        sql = "select * from sysconfig where type='抄表月'"
       
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            conn.Close()
            conn = Nothing
            g_chaobiaodate = -1
            g_chaobiaotime = -1
            Exit Sub
        End If
        If rs.RecordCount > 0 Then
            g_chaobiaodate = Val(rs.Fields("name").Value)
        Else
            g_chaobiaodate = -1
        End If

        '按每天进行抄表
        sql = "select * from sysconfig where type='抄表日'"

        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            conn.Close()
            conn = Nothing
            g_chaobiaodate = -1
            g_chaobiaotime = -1
            Exit Sub
        End If
        If rs.RecordCount > 0 Then
            g_chaobiaotime = Val(rs.Fields("name").Value)
        Else
            g_chaobiaotime = -1
        End If


        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing

    End Sub

    ''' <summary>
    ''' 模式清空时，删除所有的模式设置
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Del_all_inf(ByVal box_name As String)

        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim i As Integer = 0
        Dim control_box_id As String  '主控箱编号
        Dim mod_string As String = ""  '模式名称

        msg = ""
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If

        sql = "select control_box_id from control_box where control_box_name='" & box_name & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "Del_all_inf", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs.RecordCount > 0 Then
            control_box_id = Trim(rs.Fields("control_box_id").Value)
        Else
            Exit Sub

        End If

        '按回路设置进行清空
        '经纬度清空
        sql = "delete from pianyi where lamp_id like '" & control_box_id & "%'"
        DBOperation.ExecuteSQL(conn, sql, msg)

        '将该主控箱的所有特殊控制模式删除
        sql = "delete from road_level where control_box_id='" & control_box_id & "'"
        DBOperation.ExecuteSQL(conn, sql, msg)

        '将该主控箱的所有节假日控制模式删除
        sql = "delete from Special_road_level where control_box_id='" & control_box_id & "'"
        DBOperation.ExecuteSQL(conn, sql, msg)


        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    ''' 获取电控箱的状态，是否受时段控制
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function get_controlbox_state(ByVal controlboxid As String) As Boolean
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String

        msg = ""
        If DBOperation.OpenConn(conn) = False Then
            Exit Function
        End If
        sql = "select Information from RoadIDAndIMEI where RoadID='" & Val(controlboxid).ToString & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "get_controlbox_state", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            get_controlbox_state = False
            Exit Function
        End If
        If rs.RecordCount > 0 Then
            If rs.Fields("Information").Value Is System.DBNull.Value Then
                get_controlbox_state = True
            Else
                If Trim(rs.Fields("Information").Value) = "关闭" Then
                    get_controlbox_state = False
                Else
                    get_controlbox_state = True
                End If
            End If

        Else
            get_controlbox_state = False
        End If


        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Function

    Public Function g_textbox_time_value() As String
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        g_textbox_time_value = ""
        msg = ""
        sql = "select * from sysconfig where type='区间时间'"
        DBOperation.OpenConn(conn)
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs.RecordCount > 0 Then
            g_textbox_time_value = System.Convert.ToDouble(Trim(rs.Fields("name").Value))
        End If
        conn.Close()
        conn = Nothing
    End Function

End Module
