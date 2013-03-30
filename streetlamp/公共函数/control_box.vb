'对主控箱的操作函数
Public Class control_box

    Public m_currenttopvalue, m_currentbottomvalue, m_presuretopvalue, m_presurebottomvalue, m_bianbivalue As Integer  '过流过压的阈值
    Private m_controlproblemnum As Integer = 0   '主控箱的故障次数
    Public m_huilu() As String = {"A1", "A2", "A3", "A4", "B1", "B2", "B3", "B4", "C1", "C2", "C3", "C4"}  '用于回路名称转换
    Public m_huilu2() As String = {"A5", "A6", "A7", "A8", "B5", "B6", "B7", "B8", "C5", "C6", "C7", "C8"}  '用于回路名称转换
    Public m_huilu3() As String = {"A9", "A10", "A11", "A12", "B9", "B10", "B11", "B12", "C9", "C10", "C11", "C12"}  '用于回路名称转换

    Public m_huilu_small() As String = {"A1", "A2", "B1", "B2", "C1", "C2"}  '用于回路名称转换
    Public m_huilu2_small() As String = {"A3", "A4", "B3", "B4", "C3", "C4"}  '用于回路名称转换
    Public m_huilu3_small() As String = {"A5", "A6", "B5", "B6", "C5", "C6"}  '用于回路名称转换



    Private Structure m_currentalarm  '下放给主控箱的电流电压及测量板个数的报警阈值
        Dim use_mask() As Integer  '掩码，可用回路为1，不用回路为0
        Dim current_top() As Integer  '电流的上限制
        Dim current_bottom() As Integer  '电流的下限

    End Structure

   

    ''' <summary>
    ''' 设置电流电压的上下限值
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Get_TopBottom_value()
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String

        msg = ""
        '获取电压上限
        sql = "select * from sysconfig where type='电压上限'"
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Get_TopBottom_value" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs.RecordCount > 0 Then
            m_presuretopvalue = Val(Trim(rs.Fields("name").Value))
        Else
            m_presuretopvalue = 220
        End If

        '配置电压下限
        sql = "select * from sysconfig where type='电压下限'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Get_TopBottom_value" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs.RecordCount > 0 Then
            m_presurebottomvalue = Val(Trim(rs.Fields("name").Value))
        Else
            m_presurebottomvalue = 0
        End If

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    '    ''' <summary>
    '    ''' 设置主控箱监测面板中的信息
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    Public Sub set_controlbox_list(ByVal box_list As System.Windows.Forms.TreeView)
    '        Dim conn As New ADODB.Connection
    '        Dim sql As String
    '        Dim msg As String
    '        Dim rs_city, rs_area, rs_street, rs_box As New ADODB.Recordset
    '        Dim city_string, area_string, street_string, box_string As String
    '        Dim i1, i2, i3, i4 As Integer

    '        If DBOperation.OpenConn(conn) = False Then
    '            Exit Sub
    '        End If
    '        msg = ""
    '        i1 = 0
    '        i2 = 0
    '        i3 = 0
    '        i4 = 0
    '        box_list.Nodes.Clear()
    '        sql = "select distinct(city) as city_name,count(distinct(control_box_id)) as box_num from control_inf group by city"
    '        rs_city = DBOperation.SelectSQL(conn, sql, msg)
    '        If rs_city Is Nothing Then
    '            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "set_controlbox_list" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

    '            GoTo finish
    '        End If
    '        While rs_city.EOF = False
    '            city_string = Trim(rs_city.Fields("city_name").Value)
    '            box_list.Nodes.Add(city_string & " (共计" & rs_city.Fields("box_num").Value.ToString & "个主控箱)")

    '            '区域名称
    '            sql = "select distinct(area) as area_name,count(distinct(control_box_id)) as box_num from control_inf where city='" & city_string & "' group by area"
    '            rs_area = DBOperation.SelectSQL(conn, sql, msg)
    '            If rs_area Is Nothing Then
    '                g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "set_controlbox_list" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

    '                GoTo finish
    '            End If
    '            While rs_area.EOF = False
    '                area_string = Trim(rs_area.Fields("area_name").Value)
    '                box_list.Nodes(i1).Nodes.Add(area_string & " (共计" & rs_area.Fields("box_num").Value.ToString & "个主控箱)")
    '                '街道名称
    '                sql = "select distinct(street) as street_name,street_id,count(distinct(control_box_id)) as box_num from control_inf where city='" & city_string & "' and area='" & area_string & "' group by street, street_id order by street_id"
    '                rs_street = DBOperation.SelectSQL(conn, sql, msg)
    '                If rs_street Is Nothing Then
    '                    g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "set_controlbox_list" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

    '                    GoTo finish
    '                End If
    '                While rs_street.EOF = False
    '                    street_string = Trim(rs_street.Fields("street_name").Value)
    '                    box_list.Nodes(i1).Nodes(i2).Nodes.Add(street_string & " (共计" & rs_street.Fields("box_num").Value.ToString & "个主控箱)")
    '                    '电控箱名称
    '                    sql = "select distinct(control_box_name) as box_name,control_box_id from control_inf where city='" & city_string & "' and area='" & area_string & "' and street='" & street_string & "' order by control_box_id"
    '                    rs_box = DBOperation.SelectSQL(conn, sql, msg)
    '                    If rs_box Is Nothing Then
    '                        g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "set_controlbox_list" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

    '                        GoTo finish
    '                    End If
    '                    While rs_box.EOF = False
    '                        box_string = Trim(rs_box.Fields("box_name").Value)
    '                        box_list.Nodes(i1).Nodes(i2).Nodes(i3).Nodes.Add(box_string)

    '                        i4 += 1
    '                        rs_box.MoveNext()

    '                    End While

    '                    i3 += 1
    '                    i4 = 0
    '                    rs_street.MoveNext()

    '                End While

    '                i2 += 1
    '                i3 = 0
    '                rs_area.MoveNext()
    '            End While

    '            i1 += 1
    '            i2 = 0
    '            rs_city.MoveNext()
    '        End While

    '        If rs_city.State = 1 Then
    '            rs_city.Close()
    '            rs_city = Nothing
    '        End If
    '        If rs_area.State = 1 Then
    '            rs_area.Close()
    '            rs_area = Nothing
    '        End If
    '        If rs_street.State = 1 Then
    '            rs_street.Close()
    '            rs_street = Nothing
    '        End If
    '        If rs_box.State = 1 Then
    '            rs_box.Close()
    '            rs_box = Nothing
    '        End If


    'finish:
    '        conn.Close()
    '        conn = Nothing

    '    End Sub

    ''' <summary>
    ''' 设置主控箱监测面板中的信息(增加主控箱编号)
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub set_controlbox_list(ByVal box_list As System.Windows.Forms.TreeView)
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String
        Dim rs_city, rs_area, rs_street, rs_box As New ADODB.Recordset
        Dim city_string, area_string, street_string, box_string As String
        Dim i1, i2, i3, i4 As Integer

        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        msg = ""
        i1 = 0
        i2 = 0
        i3 = 0
        i4 = 0
        box_list.Nodes.Clear()
        sql = "select distinct(city) as city_name,count(distinct(control_box_id)) as box_num from control_inf group by city"
        rs_city = DBOperation.SelectSQL(conn, sql, msg)
        If rs_city Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "set_controlbox_list" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            GoTo finish
        End If
        While rs_city.EOF = False
            city_string = Trim(rs_city.Fields("city_name").Value)
            box_list.Nodes.Add(city_string & " (共计" & rs_city.Fields("box_num").Value.ToString & "个网关)")

            '区域名称
            sql = "select distinct(area) as area_name,count(distinct(control_box_id)) as box_num from control_inf where city='" & city_string & "' group by area"
            rs_area = DBOperation.SelectSQL(conn, sql, msg)
            If rs_area Is Nothing Then
                g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "set_controlbox_list" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

                GoTo finish
            End If
            While rs_area.EOF = False
                area_string = Trim(rs_area.Fields("area_name").Value)
                box_list.Nodes(i1).Nodes.Add(area_string & " (共计" & rs_area.Fields("box_num").Value.ToString & "个网关!)")
                '街道名称
                sql = "select distinct(street) as street_name,street_id,count(distinct(control_box_id)) as box_num from control_inf where city='" & city_string & "' and area='" & area_string & "' group by street, street_id order by street_id"
                rs_street = DBOperation.SelectSQL(conn, sql, msg)
                If rs_street Is Nothing Then
                    g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "set_controlbox_list" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

                    GoTo finish
                End If
                While rs_street.EOF = False
                    street_string = Trim(rs_street.Fields("street_name").Value)
                    box_list.Nodes(i1).Nodes(i2).Nodes.Add(street_string & " (共计" & rs_street.Fields("box_num").Value.ToString & "个网关)")
                    '电控箱名称
                    sql = "select distinct(control_box_name) as box_name,control_box_id from control_inf where city='" & city_string & "' and area='" & area_string & "' and street='" & street_string & "' order by control_box_id"
                    rs_box = DBOperation.SelectSQL(conn, sql, msg)
                    If rs_box Is Nothing Then
                        g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "set_controlbox_list" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

                        GoTo finish
                    End If
                    While rs_box.EOF = False
                        box_string = Trim(rs_box.Fields("box_name").Value) & " (" & Trim(rs_box.Fields("control_box_id").Value) & ")"
                        box_list.Nodes(i1).Nodes(i2).Nodes(i3).Nodes.Add(box_string)

                        i4 += 1
                        rs_box.MoveNext()

                    End While

                    i3 += 1
                    i4 = 0
                    rs_street.MoveNext()

                End While

                i2 += 1
                i3 = 0
                rs_area.MoveNext()
            End While

            i1 += 1
            i2 = 0
            rs_city.MoveNext()
        End While

        If rs_city.State = 1 Then
            rs_city.Close()
            rs_city = Nothing
        End If
        If rs_area.State = 1 Then
            rs_area.Close()
            rs_area = Nothing
        End If
        If rs_street.State = 1 Then
            rs_street.Close()
            rs_street = Nothing
        End If
        If rs_box.State = 1 Then
            rs_box.Close()
            rs_box = Nothing
        End If


finish:
        conn.Close()
        conn = Nothing

    End Sub

    ''' <summary>
    ''' 获取电控箱的采集数据ABCABCABCABC
    ''' </summary>
    ''' <param name="box_tag"></param>
    ''' <param name="board_num" >测量板个数</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Get_controlbox_tipABC(ByVal box_tag As String, ByVal board_num As Integer) As String
        Dim rs As New ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim lamp_tip As String
        Dim control_lamp_obj As New control_lamp
        Dim conn As New ADODB.Connection
        Dim state() As String   '主控箱的状态
        Dim i As Integer
        Dim j As Integer = 1
        Dim huilu_num As Integer  '回路的数量
        Dim board_id As Integer = 0 '测量板编号
        Dim state_string As String '三个测量板的状态
        Dim box_type As Integer '主控箱的类型
        Dim problem_string As String '故障
        Dim control_box_name As String  '电控箱名称

        i = 1
        If DBOperation.OpenConn(conn) = False Then
            Get_controlbox_tipABC = ""
            Exit Function
        End If

        msg = ""
        lamp_tip = ""
        sql = "select * from control_box where control_box_id ='" & box_tag & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)


        If rs.RecordCount > 0 Then
            box_type = rs.Fields("control_box_type").Value
            problem_string = Trim(rs.Fields("state").Value)
            If problem_string <> "正常" Then
                problem_string &= " " & get_kaiguanalarmstring(Trim(rs.Fields("control_box_name").Value))
            Else
                problem_string = get_kaiguanalarmstring(Trim(rs.Fields("control_box_name").Value))
            End If
            control_box_name = Trim(rs.Fields("control_box_name").Value)
            'If board_num = 1 Then
            '    problem_string = Trim(rs.Fields("StatusContent").Value).Split("+")(0)
            'Else
            '    If board_num = 2 Then
            '        problem_string = "[1] " & Trim(rs.Fields("StatusContent").Value).Split("+")(0) & vbCrLf & _
            '        "[2] " & Trim(rs.Fields("StatusContent2").Value).Split("+")(0)
            '    Else
            '        problem_string = "[1] " & Trim(rs.Fields("StatusContent").Value).Split("+")(0) & _
            '        vbCrLf & "[2] " & Trim(rs.Fields("StatusContent2").Value).Split("+")(0) & _
            '        vbCrLf & "[3] " & Trim(rs.Fields("StatusContent3").Value).Split("+")(0)

            '    End If
            'End If
            lamp_tip = "主控箱：" & control_box_name & " (" & problem_string & ")"
            lamp_tip &= "  时间：" & rs.Fields("Createtime").Value & vbCrLf
            While board_id < board_num
                If board_id = 0 Then
                    If rs.Fields("StatusContent").Value IsNot System.DBNull.Value Then
                        If Trim(rs.Fields("StatusContent").Value) <> "" Then
                            state_string = (Trim(rs.Fields("StatusContent").Value))
                            state = state_string.Split(" ")
                        Else
                            state_string = ""
                            state = state_string.Split("")
                        End If
                    Else
                        state_string = ""
                        state = state_string.Split("")
                    End If
                Else
                    If board_id = 1 Then
                        If rs.Fields("StatusContent2").Value IsNot System.DBNull.Value Then
                            If Trim(rs.Fields("StatusContent2").Value) <> "" Then
                                state_string = (Trim(rs.Fields("StatusContent2").Value))
                                state = state_string.Split(" ")
                            Else
                                state_string = ""
                                state = state_string.Split("")
                            End If
                        Else
                            state_string = ""
                            state = state_string.Split("")
                        End If

                    Else
                        If rs.Fields("StatusContent3").Value IsNot System.DBNull.Value Then
                            If Trim(rs.Fields("StatusContent3").Value) <> "" Then
                                state_string = (Trim(rs.Fields("StatusContent3").Value))
                                state = state_string.Split(" ")
                            Else
                                state_string = ""
                                state = state_string.Split("")
                            End If
                        Else
                            state_string = ""
                            state = state_string.Split("")
                        End If


                    End If
                End If


                If state_string = "" Then
                    board_id += 1
                    Continue While
                End If

                lamp_tip &= "A相电压：" & state(0) & _
                           vbCrLf & "B相电压：" & state(1) & _
                           vbCrLf & "C相电压：" & state(2) & vbCrLf
                i = 3
                j = 0
                huilu_num = (state.Length - 3) / 3  '回路数目

                ''原来的state顺序是AAAABBBBCCCC，2012年5月3日改为ABCABCABCABC,
                'change_state(state)
                Dim presure_type As Integer
                While j < huilu_num
                    'lamp_tip &= "第" & j & "回路  电流(A)：" & state(i) & "    "
                    '原来根据默认的AABBCC顺序进行求功率因数，2012年5月3日更改为按照用户的设定求功率因数，
                    '如果不设则按默认的ABCABC()
                    presure_type = get_presuretype(j + 1 + 12 * board_id, control_box_name)

                    If presure_type = 0 Then
                        lamp_tip &= String.Format("第{0,-3}回路(A) 电流(A)：{1,-10} ", j + 1 + 12 * board_id, state(i))
                    Else
                        If presure_type = 1 Then
                            lamp_tip &= String.Format("第{0,-3}回路(B) 电流(A)：{1,-10} ", j + 1 + 12 * board_id, state(i))
                        Else
                            lamp_tip &= String.Format("第{0,-3}回路(C) 电流(A)：{1,-10} ", j + 1 + 12 * board_id, state(i))

                        End If
                    End If


                    i += 1
                    If box_type = 1 Then
                        lamp_tip &= String.Format("功率(KW)：{0,-8}", Format(state(i) / 1000, "0.000"))
                    Else
                        lamp_tip &= String.Format("功率(KW)：{0,-8}", state(i))
                    End If

                    i += 1
                    lamp_tip &= "功率因数：" & state(i) & vbCrLf
                    j += 1
                    i += 1
                End While


                board_id += 1
            End While

        End If
        Get_controlbox_tipABC = lamp_tip
finish:
        rs.Close()
        rs = Nothing
        conn.Close()
        conn = Nothing
    End Function

    ''' <summary>
    ''' 获取电控箱的采集数据AAAABBBBCCCC
    ''' </summary>
    ''' <param name="box_tag"></param>
    ''' <param name="board_num" >测量板个数</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Get_controlbox_tip(ByVal box_tag As String, ByVal board_num As Integer) As String
        Dim rs As New ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim lamp_tip As String
        Dim control_lamp_obj As New control_lamp
        Dim conn As New ADODB.Connection
        Dim state() As String   '主控箱的状态
        Dim i As Integer
        Dim j As Integer = 1
        Dim huilu_num As Integer  '回路的数量
        Dim board_id As Integer = 0 '测量板编号
        Dim state_string As String '三个测量板的状态
        Dim box_type As Integer '主控箱的类型
        Dim problem_string As String '故障

        i = 1
        If DBOperation.OpenConn(conn) = False Then
            Get_controlbox_tip = ""
            Exit Function
        End If

        msg = ""
        lamp_tip = ""
        sql = "select * from control_box where control_box_id ='" & box_tag & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
       

        If rs.RecordCount > 0 Then
            box_type = rs.Fields("control_box_type").Value
            problem_string = Trim(rs.Fields("state").Value)
            If problem_string <> "正常" Then
                problem_string &= " " & get_kaiguanalarmstring(Trim(rs.Fields("control_box_name").Value))
            Else
                problem_string = get_kaiguanalarmstring(Trim(rs.Fields("control_box_name").Value))
            End If
            'If board_num = 1 Then
            '    problem_string = Trim(rs.Fields("StatusContent").Value).Split("+")(0)
            'Else
            '    If board_num = 2 Then
            '        problem_string = "[1] " & Trim(rs.Fields("StatusContent").Value).Split("+")(0) & vbCrLf & _
            '        "[2] " & Trim(rs.Fields("StatusContent2").Value).Split("+")(0)
            '    Else
            '        problem_string = "[1] " & Trim(rs.Fields("StatusContent").Value).Split("+")(0) & _
            '        vbCrLf & "[2] " & Trim(rs.Fields("StatusContent2").Value).Split("+")(0) & _
            '        vbCrLf & "[3] " & Trim(rs.Fields("StatusContent3").Value).Split("+")(0)

            '    End If
            'End If
            lamp_tip = "主控箱：" & Trim(rs.Fields("control_box_name").Value) & " (" & problem_string & ")"
            lamp_tip &= "  时间：" & rs.Fields("Createtime").Value & vbCrLf
            While board_id < board_num
                If board_id = 0 Then
                    If rs.Fields("StatusContent").Value IsNot System.DBNull.Value Then
                        If Trim(rs.Fields("StatusContent").Value) <> "" Then
                            state_string = (Trim(rs.Fields("StatusContent").Value))
                            state = state_string.Split(" ")
                        Else
                            state_string = ""
                            state = state_string.Split("")
                        End If
                    Else
                        state_string = ""
                        state = state_string.Split("")
                    End If
                Else
                    If board_id = 1 Then
                        If rs.Fields("StatusContent2").Value IsNot System.DBNull.Value Then
                            If Trim(rs.Fields("StatusContent2").Value) <> "" Then
                                state_string = (Trim(rs.Fields("StatusContent2").Value))
                                state = state_string.Split(" ")
                            Else
                                state_string = ""
                                state = state_string.Split("")
                            End If
                        Else
                            state_string = ""
                            state = state_string.Split("")
                        End If

                    Else
                        If rs.Fields("StatusContent3").Value IsNot System.DBNull.Value Then
                            If Trim(rs.Fields("StatusContent3").Value) <> "" Then
                                state_string = (Trim(rs.Fields("StatusContent3").Value))
                                state = state_string.Split(" ")
                            Else
                                state_string = ""
                                state = state_string.Split("")
                            End If
                        Else
                            state_string = ""
                            state = state_string.Split("")
                        End If


                    End If
                End If


                If state_string = "" Then
                    board_id += 1
                    Continue While
                End If

                lamp_tip &= "A相电压：" & state(0) & _
                           vbCrLf & "B相电压：" & state(1) & _
                           vbCrLf & "C相电压：" & state(2) & vbCrLf
                i = 3
                j = 0
                huilu_num = (state.Length - 3) / 3  '回路数目

                While j < huilu_num
                    'lamp_tip &= "第" & j & "回路  电流(A)：" & state(i) & "    "
                    If huilu_num = 12 Then  '12路数据板
                        If board_id = 0 Then
                            lamp_tip &= String.Format("第{0,-3}回路 电流(A)：{1,-10} ", m_huilu(j), state(i))
                        Else
                            If board_id = 1 Then
                                lamp_tip &= String.Format("第{0,-3}回路 电流(A)：{1,-10} ", m_huilu2(j), state(i))
                            Else
                                lamp_tip &= String.Format("第{0,-3}回路 电流(A)：{1,-10} ", m_huilu3(j), state(i))


                            End If
                        End If
                    Else
                        If board_id = 0 Then
                            lamp_tip &= String.Format("第{0,-3}回路 电流(A)：{1,-8} ", m_huilu_small(j), state(i))
                        Else
                            If board_id = 1 Then
                                lamp_tip &= String.Format("第{0,-3}回路 电流(A)：{1,-8} ", m_huilu2_small(j), state(i))
                            Else
                                lamp_tip &= String.Format("第{0,-3}回路 电流(A)：{1,-8} ", m_huilu3_small(j), state(i))


                            End If
                        End If
                    End If

                    i += 1
                    If box_type = 1 Then
                        lamp_tip &= String.Format("功率(KW)：{0,-8}", Format(state(i) / 1000, "0.000"))
                    Else
                        lamp_tip &= String.Format("功率(KW)：{0,-8}", state(i))
                    End If

                    i += 1
                    lamp_tip &= "功率因数：" & state(i) & vbCrLf
                    j += 1
                    i += 1
                End While


                board_id += 1
            End While

        End If
        Get_controlbox_tip = lamp_tip
finish:
        rs.Close()
        rs = Nothing
        conn.Close()
        conn = Nothing
    End Function


    ''' <summary>
    ''' 绘制电控箱
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub get_box_information()
        Dim conn As New ADODB.Connection
        Dim rs As New ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim myBrush As New SolidBrush(Drawing.Color.Blue)
        Dim myPen As New Pen(Drawing.Color.Gold, 4)
        Dim myBrush2 As New SolidBrush(Drawing.Color.Red)
        Dim problem_string As String
        Dim board_num As Integer
        Dim kaiguan_alarm As Boolean
        msg = ""
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        sql = "SELECT control_box.state, map_list.id, control_box.board_num,control_box.control_box_id, control_box_name , control_box.StatusContent,control_box.StatusContent2,control_box.StatusContent3, " _
        & "control_box.pos_y, control_box.pos_x FROM control_box INNER JOIN street ON control_box.street_id =" _
        & " street.street_id INNER JOIN area ON street.area_id = area.id INNER JOIN map_list ON area.area = map_list.area where map_list.id='" & g_choosemapid & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)  '该地图上的主控箱
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "get_box_information" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        While rs.EOF = False
            problem_string = ""
            board_num = rs.Fields("board_num").Value   '测量板个数
            problem_string = Trim(Trim(rs.Fields("state").Value))
          
           
            If problem_string = "正常" Then   '正常

                '三遥数据正常后，再次判断开关量是否正常
                kaiguan_alarm = get_kaiguanalarm(Trim(rs.Fields("control_box_name").Value))
                If kaiguan_alarm = True Then  '故障
                    g_lampmap.DrawRectangle(myPen, rs.Fields("pos_x").Value, rs.Fields("pos_y").Value, 20, 20)
                    g_lampmap.FillRectangle(myBrush2, rs.Fields("pos_x").Value, rs.Fields("pos_y").Value, 20, 20)
                Else
                    g_lampmap.DrawRectangle(myPen, rs.Fields("pos_x").Value, rs.Fields("pos_y").Value, 20, 20)
                    g_lampmap.FillRectangle(myBrush, rs.Fields("pos_x").Value, rs.Fields("pos_y").Value, 20, 20)
                End If


            Else  '故障
                g_lampmap.DrawRectangle(myPen, rs.Fields("pos_x").Value, rs.Fields("pos_y").Value, 20, 20)
                g_lampmap.FillRectangle(myBrush2, rs.Fields("pos_x").Value, rs.Fields("pos_y").Value, 20, 20)

            End If
            rs.MoveNext()
        End While

        myBrush.Dispose()
        myBrush2.Dispose()
        myPen.Dispose()
        myPen = Nothing
        myBrush = Nothing
        myBrush2 = Nothing
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If

        conn.Close()
        conn = Nothing
    End Sub

    Public Function get_kaiguanalarm(ByVal control_box_name As String) As Boolean
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String
        get_kaiguanalarm = False
        msg = ""
        If DBOperation.OpenConn(conn) = False Then
            Exit Function
        End If
        sql = "select alarm_tag from kaiguan_alarm_list where control_box_name='" & control_box_name & "' and (alarm_tag=0 or alarm_tag=2)"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            conn.Close()
            conn = Nothing
            Exit Function
        End If
        If rs.RecordCount > 0 Then
            get_kaiguanalarm = True
        Else
            get_kaiguanalarm = False
        End If
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Function

    Public Function get_kaiguanalarmstring(ByVal control_box_name As String) As String
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String
        get_kaiguanalarmstring = ""
        msg = ""
        If DBOperation.OpenConn(conn) = False Then
            Exit Function
        End If
        sql = "select alarm_string from kaiguan_alarm_list where control_box_name='" & control_box_name & "' and (alarm_tag=0 or alarm_tag=2)"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            conn.Close()
            conn = Nothing
            Exit Function
        End If
        If rs.RecordCount > 0 Then
            While rs.EOF = False
                get_kaiguanalarmstring &= Trim(rs.Fields("alarm_string").Value) & " "
                rs.MoveNext()

            End While

        Else
            get_kaiguanalarmstring = ""
        End If
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Function


    ''' <summary>
    ''' 根据主控箱的编号查询主控箱的类型
    ''' </summary>
    ''' <param name="control_box_id"></param>
    ''' <remarks></remarks>
    Public Function get_controltype(ByVal control_box_id As String) As Integer
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String
        get_controltype = 2  '默认是大三遥
        msg = ""
        If DBOperation.OpenConn(conn) = False Then
            Exit Function
        End If
        sql = "select control_box_type from control_box where control_box_id ='" & control_box_id & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            conn.Close()
            conn = Nothing
            Exit Function
        End If
        If rs.RecordCount > 0 Then
            get_controltype = rs.Fields("control_box_type").Value

        End If

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing

    End Function

    ''' <summary>
    ''' 寻找是否有电控箱的主动报警信息
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub find_auto_alarm()
        Dim rs_find_state As New ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim conn As New ADODB.Connection
        Dim control_box_id As String '电控箱编号
        Dim recData As String '接收的数据
        'Dim rs As New ADODB.Recordset
        Dim status_list(80) As String '存放状态
        Dim i As Integer = 0
        Dim problem_tag As String   '标志回路的状态
        Dim rs_box As New ADODB.Recordset
        Dim group_id As Integer = 0
        Dim boxtype As Integer '区分大小三遥，大三遥类型2，小三遥类型3

        Dim datainf As Boolean = True '三遥数据的合法性
        Dim stateflag As Integer '上传状态的handlerflag，用来区分大小三遥

        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If

        msg = ""

        '查询10分钟之间是否有没被分析过的数据,批量上传的数据HandlerFlag标志为10
        sql = "select * from RoadLightStatus nolock where Createtime > DateAdd(n,-10,'" & Now() & "') and (PackType='" & HG_TYPE.HG_HOST_ALARMAUTO & "' or PackType='" & HG_TYPE.HG_HOST_SMALL_ALARMAUTO & "') and HandlerFlag=3 order by ID"  '没有返回状态

        rs_find_state = DBOperation.SelectSQL(conn, sql, msg)
        If rs_find_state Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "find_box_state_fun1" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs_find_state.RecordCount <= 0 Then  '没有数据则返回
            GoTo finish
        End If

        '将读取的数据按电控箱的信息进行分析
        While rs_find_state.EOF = False

            problem_tag = ""
            recData = Trim(rs_find_state.Fields("statusContent").Value)
            status_list = recData.Split(" ")
            stateflag = rs_find_state.Fields("PackType").Value   '上传状态的大小三遥

            '判断数据是否为超时数据，如果数据为超时数据则继续判读否则丢弃不判
            datainf = CheckData(status_list, rs_find_state.Fields("ID").Value)
            If datainf = False Then
                '证明上传的数据为超时数据，丢弃不管
                GoTo next1
            End If
            control_box_id = System.Convert.ToInt32(status_list(0) & status_list(1), 16)  '前两个字节作为电控箱编号
            While control_box_id.Length < 4
                control_box_id = "0" & control_box_id
            End While

            '根据主控箱编号查询主控箱的类型，区分大小三遥
            boxtype = get_controltype(control_box_id)

            If ((boxtype = 3 Or boxtype = 5) And stateflag <> 38) Or ((boxtype = 2 Or boxtype = 4) And stateflag <> 36) Then
                '上传的三遥数据与设置的主控箱状态不一致
                g_welcomewinobj.SetTextDelegate(control_box_id & "号主控箱的版本与上传状态不匹配 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, True, g_welcomewinobj.rtb_info_list)
                GoTo next1
            End If

            '3版数据（三遥数据模式和招测回来的模式一致，如三个测量板则三个测量板一条数据2011年9月22日）
            Dim recstring As String
            Dim data() As String
            'Dim controlboxobj As New control_box
            Dim group_num As Integer = 0  '组号
            Dim j As Integer = 0
            Dim sanyao_data(56) As String

            recstring = Trim(rs_find_state.Fields("StatusContent").Value)
            data = recstring.Split(" ")

            If boxtype = 3 Then
                '所获取的数据为小三遥数据，,数据显示AABBCC，将数据中第三个字节的组数减1变为组号，调用通用的三遥数据分析
                data(2) = (Val(data(2).ToString) - 1).ToString
                If data(2) = -1 Then
                    '表示数据失败,将该状态置为1
                    rs_find_state.Fields("handlerflag").Value = 1
                    rs_find_state.Update()

                    GoTo next1
                End If

                Get_Huilu_inf_small(Val(data(2)), data, problem_tag, control_box_id, rs_find_state.Fields("ID").Value)
            End If

            If boxtype = 2 Then
                '传统大三遥数据,数据显示AAAABBBBCCCC
                group_num = Val(data(2))
                If group_num = 0 Then
                    '表示数据失败,将该状态置为1
                    rs_find_state.Fields("handlerflag").Value = 1
                    rs_find_state.Update()
                    GoTo next1
                End If

                j = 0
                While j < group_num

                    sanyao_data(0) = data(0)
                    sanyao_data(1) = data(1)
                    sanyao_data(2) = j
                    i = 3
                    While i < 57
                        sanyao_data(i) = data(i + (j) * 54)
                        i += 1
                    End While
                    Get_Huilu_inf(j, group_num, sanyao_data, problem_tag, control_box_id)
                    j += 1

                End While


                '读取故障信息
                get_alarminf(data, 3 + group_num * 54, rs_find_state.Fields("ID").Value, control_box_id, group_num)

            End If

            If boxtype = 5 Then
                '所获取的数据为小三遥数据，,数据显示ABCABC，将数据中第三个字节的组数减1变为组号，调用通用的三遥数据分析
                data(2) = (Val(data(2).ToString) - 1).ToString
                If data(2) = -1 Then
                    '表示数据失败,将该状态置为1
                    rs_find_state.Fields("handlerflag").Value = 1
                    rs_find_state.Update()

                    GoTo next1
                End If

                Get_Huilu_inf_smallABC(Val(data(2)), data, problem_tag, control_box_id, rs_find_state.Fields("ID").Value)
            End If

            If boxtype = 4 Then
                '传统大三遥数据,数据显示ABCABCABCABC
                group_num = Val(data(2))
                If group_num = 0 Then
                    '表示数据失败,将该状态置为1
                    rs_find_state.Fields("handlerflag").Value = 1
                    rs_find_state.Update()
                    GoTo next1
                End If

                j = 0
                While j < group_num

                    sanyao_data(0) = data(0)
                    sanyao_data(1) = data(1)
                    sanyao_data(2) = j
                    i = 3
                    While i < 57
                        sanyao_data(i) = data(i + (j) * 54)
                        i += 1
                    End While
                    Get_Huilu_infABC(j, group_num, sanyao_data, problem_tag, control_box_id)
                    j += 1

                End While


                '读取故障信息
                get_alarminf(data, 3 + group_num * 54, rs_find_state.Fields("ID").Value, control_box_id, group_num)

            End If
            '记录故障数据
            Saveboxdata(control_box_id)



next1:
            rs_find_state.MoveNext()
        End While

        g_welcomewinobj.BackgroundWorker_find_state.ReportProgress(2)

finish:

        

        If rs_box.State = 1 Then
            rs_box.Close()
            rs_box = Nothing
        End If
        If rs_find_state.State = 1 Then
            rs_find_state.Close()
            rs_find_state = Nothing

        End If
        conn.Close()
        conn = Nothing

    End Sub



    Public Sub get_alarminf(ByVal data() As String, ByVal start_id As Integer, ByVal ID As Integer, ByVal control_box_id As String, ByVal group_num As Integer)
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String
        Dim control_box_name As String = ""
        Dim state_string As String
        Dim board_num As Integer
        Dim problem_tag As String = ""
        Dim chaoshi_tag As Integer = 0
        msg = ""
        '将handlerflag至为1
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If

        '首先三个字节的电压报警信息
        Dim power As String
        Dim i As Integer = 0

        power = System.Convert.ToInt32(data(start_id), 16).ToString

        If power <> 0 And power <> 4 Then
            problem_tag &= "A相：" & get_alarminf("power_" & power, 0, control_box_id) & " "

        End If
        If power = 4 And chaoshi_tag = 0 Then
            problem_tag &= "测量板数据采集超时"
            chaoshi_tag += 1
        End If
        power = System.Convert.ToInt32(data(start_id + 1), 16)
        If power <> 0 And power <> 4 Then
            problem_tag &= "B相：" & get_alarminf("power_" & power, 0, control_box_id) & " "

        End If
        If power = 4 And chaoshi_tag = 0 Then
            problem_tag &= "测量板数据采集超时"
            chaoshi_tag += 1
        End If
        power = System.Convert.ToInt32(data(start_id + 2), 16)
        If power <> 0 And power <> 4 Then
            problem_tag &= "C相：" & get_alarminf("power_" & power, 0, control_box_id) & " "

        End If
        If power = 4 And chaoshi_tag = 0 Then
            problem_tag &= "测量板数据采集超时"
            chaoshi_tag += 1
        End If


        If data.Length > 7 + group_num * 54 Then
            '判断电流报警信息
            Dim huilu_num As Integer '回路的数量
            Dim huilu_id As Integer '回路的编号
            Dim alarm_tag As Integer '报警标志
            huilu_num = System.Convert.ToInt32(data(start_id + 3), 16)
            i = 0
            While i < huilu_num * 2
                huilu_id = System.Convert.ToInt32(data(start_id + 4 + i), 16)
                alarm_tag = System.Convert.ToInt32(data(start_id + 5 + i), 16)
                If alarm_tag <> 0 And alarm_tag <> 9 Then
                    problem_tag &= get_alarminf("current_" & alarm_tag, huilu_id, control_box_id) & "(回路" & huilu_id & ") "
                End If
                If alarm_tag = 9 And chaoshi_tag = 0 Then
                    chaoshi_tag += 1
                    problem_tag &= get_alarminf("current_" & alarm_tag, huilu_id, control_box_id)

                End If
                i += 2
            End While

        End If

        If Trim(problem_tag) = "" Then
            problem_tag = "正常"
        End If

        '将状态置1
        sql = "update RoadLightStatus set HandlerFlag=1 where ID ='" & ID & "'"
        DBOperation.ExecuteSQL(conn, sql, msg)


        '获取当前电控箱的故障次数
        sql = "select * from control_box where control_box_id='" & control_box_id & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "find_box_state_fun3" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            Exit Sub

        End If
        If rs.RecordCount > 0 Then
            If rs.Fields("problem_num").Value Is System.DBNull.Value Then
                m_controlproblemnum = 0
            Else
                m_controlproblemnum = rs.Fields("problem_num").Value
            End If
            control_box_name = Trim(rs.Fields("control_box_name").Value)
            ' state_string = Trim(rs.Fields("state").Value)
            board_num = rs.Fields("board_num").Value

            '将当前解析出来的数据串增加到control_box表中
            If problem_tag <> "正常" Then
                m_controlproblemnum += 1
                If m_controlproblemnum = 10000 Then
                    m_controlproblemnum = 20
                End If
            Else
                m_controlproblemnum = 0
            End If

            sql = "update control_box set state='" & problem_tag & "' , problem_num='" & m_controlproblemnum & "' where control_box_id='" & control_box_id & "'"

            DBOperation.ExecuteSQL(conn, sql, msg)

        End If
        '更新control_box_state中的状态记录
        Dim state_type As String = "状态"
        state_string = ""
        sql = "select state, StatusContent,StatusContent2,StatusContent3 from control_box where control_box_id='" & control_box_id & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "find_box_state_fun2" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            Exit Sub

        End If
        If rs.RecordCount > 0 Then
            state_string = Trim(rs.Fields("state").Value)
            '更新control_box_state中的状态记录
            If state_string <> "" Then
                Setcontrolbox_Record(control_box_name, state_string, "状态")
            End If

            'Setcontrolbox_Record(control_box_name, problem_tag, "状态")

            If problem_tag <> "正常" And m_controlproblemnum = 1 Then  '确认四次故障后发送短信
                '发送短信
                Com_inf.Send_Msg(control_box_id, "", state_string)
                System.Threading.Thread.Sleep(2000)
            End If
        End If
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub


    ''' <summary>
    ''' 检测回路电流，电压，电功率
    ''' </summary>
    ''' <remarks></remarks>
    ''' <param name="load_state_tag" >load_state_tag=true记录一次数据</param>
    Public Function find_box_state_fun(ByVal load_state_tag As Boolean) As Boolean
        Dim rs_find_state As New ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim conn As New ADODB.Connection
        Dim control_box_id As String '电控箱编号
        Dim recData As String '接收的数据
        'Dim rs As New ADODB.Recordset
        Dim status_list(80) As String '存放状态
        Dim i As Integer = 0
        Dim problem_tag As String   '标志回路的状态
        Dim rs_box As New ADODB.Recordset
        Dim group_id As Integer = 0

        Dim datainf As Boolean = True '三遥数据的合法性

        If DBOperation.OpenConn(conn) = False Then
            Exit Function
        End If

        msg = ""

        '查询10分钟之间是否有没被分析过的数据,批量上传的数据HandlerFlag标志为0
        ' sql = "select * from RoadLightStatus where Createtime > DateAdd(n,-10,'" & Now() & "') and HandlerFlag=" & 0 & " order by ID"  '没有返回状态
        sql = "select * from RoadLightStatus nolock where Createtime > DateAdd(n,-10,'" & Now() & "') and HandlerFlag=" & 0 & " order by ID"  '没有返回状态

        rs_find_state = DBOperation.SelectSQL(conn, sql, msg)
        If rs_find_state Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "find_box_state_fun1" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            find_box_state_fun = False '表示没有数据
            Exit Function
        End If
        If rs_find_state.RecordCount <= 0 Then  '没有数据则返回
            find_box_state_fun = False '表示没有数据
            GoTo finish
        End If
        '*************************有数据*********************
        '2011年4月11日，有三遥数据分两部分考虑：一、老版的，没有类型码的，只有路段号和12路信息
        '二、新版的，有类型码33，两字节路段号+1字节组号（00，,01,02）+12路回路信息，长度57
        '2011年9月5日，增加小三遥数据分析，有类型码33，两字节路段号+1字节组号（00，,01,02）+6路回路信息，长度33

        '将读取的数据按电控箱的信息进行分析
        While rs_find_state.EOF = False
            '2011年11月22日，每次取状态的时候先判断一下是否需要等待召测信息
            If g_zhaocetag = True Then
                Com_inf.clearstatus("")
                If rs_find_state.State = 1 Then
                    rs_find_state.Close()
                    rs_find_state = Nothing

                End If
                conn.Close()
                conn = Nothing
                Exit Function

            End If

            problem_tag = "正常"
            recData = Trim(rs_find_state.Fields("statusContent").Value)
            status_list = recData.Split(" ")

            '判断数据是否合法，如果数据合法则继续判读否则丢弃不判
            datainf = CheckData(status_list, rs_find_state.Fields("ID").Value)
            If datainf = False Then
                '证明上传的数据为超时数据，丢弃不管
                GoTo next1
            End If
            control_box_id = System.Convert.ToInt32(status_list(0) & status_list(1), 16)  '前两个字节作为电控箱编号
            While control_box_id.Length < 4
                control_box_id = "0" & control_box_id
            End While
            If rs_find_state.Fields("PackType").Value Is System.DBNull.Value Then
                '1版数据
                Me.Get_Huilu_inf(-1, 1, status_list, problem_tag, control_box_id)
            Else
                ''2版数据
                'If rs_find_state.Fields("PackType").Value = HG_TYPE.HG_HOST_SANYAO_AUTO Then
                '    group_id = Val(status_list(2))
                '    If status_list.Length = OLD_DATALENGHT Then
                '        Me.Get_Huilu_inf(group_id, status_list, problem_tag, control_box_id, rs_find_state.Fields("ID").Value)
                '    Else
                '        If status_list.Length = SMALL_DATALENGHT Then
                '            Me.Get_Huilu_inf_small(group_id, status_list, problem_tag, control_box_id, rs_find_state.Fields("ID").Value)

                '        End If

                '    End If
                'End If

                '3版数据（三遥数据模式和招测回来的模式一致，如三个测量板则三个测量板一条数据2011年9月22日）
                If rs_find_state.Fields("PackType").Value = HG_TYPE.HG_HOST_NEWSAYAO_AUTO Then
                    Dim recstring As String
                    Dim data() As String
                    Dim controlboxobj As New control_box
                    Dim group_num As Integer = 0  '组号
                    Dim j As Integer = 0
                    Dim sanyao_data(56) As String

                    recstring = Trim(rs_find_state.Fields("StatusContent").Value)
                    data = recstring.Split(" ")

                    If data.Length = SMALL_DATALENGHT Then
                        '所获取的数据为小三遥数据，将数据中第三个字节的组数减1变为组号，调用通用的三遥数据分析
                        data(2) = (Val(data(2).ToString) - 1).ToString

                        controlboxobj.Get_Huilu_inf_small(Val(data(2)), data, problem_tag, control_box_id, rs_find_state.Fields("ID").Value)
                    End If

                    If data.Length = 3 + data(2) * 54 Then
                        '传统三遥数据
                        group_num = Val(data(2))
                        j = 0
                        While j < group_num

                            sanyao_data(0) = data(0)
                            sanyao_data(1) = data(1)
                            sanyao_data(2) = j
                            i = 3
                            While i < 57
                                sanyao_data(i) = data(i + (j) * 54)
                                i += 1
                            End While
                            controlboxobj.Get_Huilu_inf(j, group_num, sanyao_data, problem_tag, control_box_id)

                            j += 1

                        End While


                    End If
                End If
            End If
next1:
            rs_find_state.MoveNext()
        End While
        find_box_state_fun = True  '表示有数据
       
        g_welcomewinobj.BackgroundWorker_find_state.ReportProgress(2)

finish:

        If load_state_tag = True Then
           
            Saveboxdata("")
        End If

      
        If rs_box.State = 1 Then
            rs_box.Close()
            rs_box = Nothing
        End If
        If rs_find_state.State = 1 Then
            rs_find_state.Close()
            rs_find_state = Nothing

        End If
        conn.Close()
        conn = Nothing


    End Function

    Public Sub Saveboxdata(ByVal control_box_id As String)
        ' Dim state_string As String
        Dim control_box_name As String
        Dim state1, state2, state3 As String  '三块测量板的状态
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String = ""
        Dim board_num As Integer = 1
        Dim state_type As String = "数据"
        state1 = ""
        state2 = ""
        state3 = ""

        '将上传的回路信息保存到control_box_state表中
        If control_box_id = "" Then
            sql = "select * from control_box order by control_box_id"

        Else
            sql = "select * from control_box where control_box_id='" & control_box_id & "'"

        End If
        DBOperation.OpenConn(conn)
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "find_box_state_fun2" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        While rs.EOF = False
            board_num = Val(rs.Fields("board_num").Value)
            control_box_name = Trim(rs.Fields("control_box_name").Value)

            If rs.Fields("Createtime").Value Is System.DBNull.Value Then
                rs.MoveNext()
                Continue While
            End If
            If Now.AddMinutes(-30) > rs.Fields("Createtime").Value Then
                rs.MoveNext()
                Continue While
            End If

            '记录数据
            If board_num = 1 Then
                If rs.Fields("StatusContent").Value IsNot System.DBNull.Value Then

                    If Trim(rs.Fields("StatusContent").Value) <> "" Then
                        state1 = Trim(rs.Fields("StatusContent").Value)
                    Else
                        If Trim(rs.Fields("StatusContent").Value) = "" Then
                            '表示此刻通信不正常
                            GoTo next2
                        Else
                            state1 = ""
                        End If
                    End If
                Else
                    state1 = ""
                End If
                'state_string = Trim(rs.Fields("StatusContent").Value).Split("+")(0)
                sql = "insert into control_box_state( StatusContent, control_box_name, Createtime, kaiguan_string)" _
                & " values('" & state1 & "', '" & Trim(rs.Fields("control_box_name").Value) & "','" & _
                Now & "','" & state_type & "')"
            Else
                If board_num = 2 Then
                    If rs.Fields("StatusContent").Value IsNot System.DBNull.Value Then

                        If Trim(rs.Fields("StatusContent").Value) <> "" Then
                            state1 = Trim(rs.Fields("StatusContent").Value)
                        Else
                            state1 = ""

                        End If
                    Else
                        state1 = ""
                    End If
                    If rs.Fields("StatusContent2").Value IsNot System.DBNull.Value Then

                        If Trim(rs.Fields("StatusContent2").Value) <> "" Then
                            state2 = Trim(rs.Fields("StatusContent2").Value)
                        Else
                            state2 = ""
                        End If
                    Else
                        state2 = ""
                    End If
                    If state1 = "" And state2 = "" Then
                        '两块测量板的数据都为空
                        GoTo next2
                    End If
                    'state_string = Trim(rs.Fields("StatusContent").Value).Split("+")(0) & " " & Trim(rs.Fields("StatusContent2").Value).Split("+")(0)
                    sql = "insert into control_box_state( StatusContent, StatusContent2, control_box_name, " _
                   & "Createtime, kaiguan_string) values('" & state1 & "','" & _
                    state2 & "', '" & Trim(rs.Fields("control_box_name").Value) & "','" & Now & "','" & state_type & "')"

                Else
                    If rs.Fields("StatusContent").Value IsNot System.DBNull.Value Then

                        If Trim(rs.Fields("StatusContent").Value) <> "" Then
                            state1 = Trim(rs.Fields("StatusContent").Value)
                        Else
                            state1 = ""
                        End If
                    Else
                        state1 = ""
                    End If
                    If rs.Fields("StatusContent2").Value IsNot System.DBNull.Value Then

                        If Trim(rs.Fields("StatusContent2").Value) <> "" Then
                            state2 = Trim(rs.Fields("StatusContent2").Value)
                        Else
                            state2 = ""
                        End If
                    Else
                        state2 = ""
                    End If
                    If rs.Fields("StatusContent3").Value IsNot System.DBNull.Value Then

                        If Trim(rs.Fields("StatusContent3").Value) <> "" Then
                            state3 = Trim(rs.Fields("StatusContent3").Value)
                        Else
                            state3 = ""
                        End If
                    Else
                        state3 = ""
                    End If

                    If state1 = "" And state2 = "" And state3 = "" Then
                        '三块测量板的数据都为空，则不处理
                        GoTo next2
                    End If
                    ' state_string = Trim(rs.Fields("StatusContent").Value).Split("+")(0) & " " & Trim(rs.Fields("StatusContent2").Value).Split("+")(0) & " " & Trim(rs.Fields("StatusContent3").Value).Split("+")(0)
                    sql = "insert into control_box_state( StatusContent, StatusContent2, StatusContent3, control_box_name, " _
                    & "Createtime,kaiguan_string) values('" & state1 & "','" & _
                    state2 & "','" & state3 & "', '" & Trim(rs.Fields("control_box_name").Value) & "','" & Now & "','" & state_type & "')"

                End If
            End If
            DBOperation.ExecuteSQL(conn, sql, msg)

next2:
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
    ''' 判断获得的三遥数据是否为超时数据，如果全是FF则证明是超时数据，直接丢弃
    ''' </summary>
    ''' <remarks></remarks>
    Public Function CheckData(ByVal datainf() As String, ByVal ID As Integer) As Boolean

        Dim group_num As Integer
        group_num = (datainf.Length - 3) / 54  '测量板个数
        Dim i As Integer = 0
        Dim noinf_num As Integer = 0 '超时测量板个数
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String
        CheckData = True

        msg = ""
        If DBOperation.OpenConn(conn) = False Then
            Exit Function
        End If

        While i < group_num
            If datainf(3 + 54 * i) = "FF" And datainf(4 + 54 * i) = "FF" And datainf(5 + 54 * i) = "FF" And datainf(6 + 54 * i) = "FF" And datainf(7 + 54 * i) = "FF" And datainf(8 + 54 * i) = "FF" Then
                noinf_num += 1
            End If

            i += 1
        End While

       

        If noinf_num = group_num Then

            sql = "update RoadLightStatus set HandlerFlag=1 where ID ='" & ID & "'"
            DBOperation.ExecuteSQL(conn, sql, msg)
            CheckData = False

        End If
        conn.Close()
        conn = Nothing
    End Function

    ''' <summary>
    ''' 获取电控箱
    ''' </summary>
    ''' <param name="control_box_name"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Get_boxtype_name(ByVal control_box_name As String) As Integer
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim box_type As Integer
        box_type = 1
        msg = ""
        sql = "select control_box_type from control_box where control_box_name='" & control_box_name & "'"
        If DBOperation.OpenConn(conn) = False Then
            Exit Function
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Get_boxtype" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            Get_boxtype_name = 1
            Exit Function
        End If
        If rs.RecordCount > 0 Then
            box_type = rs.Fields("control_box_type").Value

        End If

        Get_boxtype_name = box_type
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Function


    ''' <summary>
    ''' 获取电控箱
    ''' </summary>
    ''' <param name="control_box_id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Get_boxtype(ByVal control_box_id As String) As Integer
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
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Get_boxtype" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            Get_boxtype = 1
            Exit Function
        End If
        If rs.RecordCount > 0 Then
            box_type = rs.Fields("control_box_type").Value

        End If

        Get_boxtype = box_type
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Function
    ''' <summary>
    ''' 将各个组的回路信息分析出来(2011年9月5日增加的小三遥处理)AABBCC
    ''' </summary>
    ''' <param name="group_id">-1表示老版数据，没有组号；0为第一组；1为第二组；2位第三组</param>
    ''' <param name="status_list">6路回路状态</param>
    ''' <param name="problem_tag" >状态</param>
    ''' <param name="control_box_id" >主控箱编号</param>
    ''' <param name="ID" >记录编号</param>
    ''' <remarks></remarks>
    Public Sub Get_Huilu_inf_small(ByVal group_id As Integer, ByVal status_list() As String, ByVal problem_tag As String, ByVal control_box_id As String, ByVal ID As Integer)

        Dim VA_value, VB_value, VC_value As Double  '十进制的电压值
        Dim Current_value(11) As Double  '电流值
        Dim Power_value(11) As Double  '电功率
        Dim Power_yinshu(11) As Double   '功率因数
        Dim sendData As String = "" '保存的已经解析过的数据
        Dim start_id As Integer '数据的起始编号
        Dim currentalarm_string As String = "" '分路开关跳闸报警
        Dim box_type As Integer '电控箱的类型
        Dim i As Integer
        If group_id = -1 Then
            start_id = 2
        Else
            start_id = 3
        End If
        Get_TopBottom_value()
        If status_list(start_id) = "FF" And status_list(start_id + 1) = "FF" And status_list(start_id + 2) = "FF" And status_list(start_id + 3) = "FF" And status_list(start_id + 4) = "FF" _
        And status_list(start_id + 5) = "FF" Then
            '超时数据，丢弃数据
            sendData = ""
            GoTo next1

        End If

        box_type = Get_boxtype(control_box_id)  '主控箱类型
        VA_value = System.Convert.ToInt64(status_list(start_id) & status_list(start_id + 1), 16)
        VA_value = Format(Com_inf.Get_Presure(VA_value, control_box_id), "0.00")
        VB_value = System.Convert.ToInt64(status_list(start_id + 2) & status_list(start_id + 3), 16)
        VB_value = Format(Com_inf.Get_Presure(VB_value, control_box_id), "0.00")
        VC_value = System.Convert.ToInt64(status_list(start_id + 4) & status_list(start_id + 5), 16)
        VC_value = Format(Com_inf.Get_Presure(VC_value, control_box_id), "0.00")
        sendData = VA_value.ToString & " " & VB_value.ToString & " " & VC_value.ToString  '字符串

        Current_value(0) = System.Convert.ToInt64(status_list(start_id + 6) & status_list(start_id + 7), 16)
        Current_value(1) = System.Convert.ToInt64(status_list(start_id + 8) & status_list(start_id + 9), 16)
        Current_value(2) = System.Convert.ToInt64(status_list(start_id + 10) & status_list(start_id + 11), 16)
        Current_value(3) = System.Convert.ToInt64(status_list(start_id + 12) & status_list(start_id + 13), 16)
        Current_value(4) = System.Convert.ToInt64(status_list(start_id + 14) & status_list(start_id + 15), 16)
        Current_value(5) = System.Convert.ToInt64(status_list(start_id + 16) & status_list(start_id + 17), 16)
        Power_value(0) = System.Convert.ToInt64(status_list(start_id + 18) & status_list(start_id + 19), 16)
        Power_value(1) = System.Convert.ToInt64(status_list(start_id + 20) & status_list(start_id + 21), 16)
        Power_value(2) = System.Convert.ToInt64(status_list(start_id + 22) & status_list(start_id + 23), 16)
        Power_value(3) = System.Convert.ToInt64(status_list(start_id + 24) & status_list(start_id + 25), 16)
        Power_value(4) = System.Convert.ToInt64(status_list(start_id + 26) & status_list(start_id + 27), 16)
        Power_value(5) = System.Convert.ToInt64(status_list(start_id + 28) & status_list(start_id + 29), 16)


        i = 0
        While (i < 6)
            Current_value(i) = Format(Get_Current(Current_value(i), control_box_id, i + 1, box_type), "0.00")  '电流

            If Current_value(i) = 0 Then
                Power_value(i) = Format(0, "0.00")
            Else

                Power_value(i) = Format(Get_Power1(Power_value(i), control_box_id, i + 1, box_type), "0.00") '功率
            End If

            If i <= 1 Then
                If VA_value = 0.0 Or Current_value(i) = 0.0 Then
                    Power_yinshu(i) = 0.0

                Else
                    If box_type = 1 Then
                        Power_yinshu(i) = Format(Power_value(i) / (VA_value * Current_value(i)), "0.00")
                    Else
                        Power_yinshu(i) = Format(Power_value(i) * 1000 / (VA_value * Current_value(i)), "0.00")

                    End If

                End If
            Else
                If i <= 3 Then
                    If VB_value = 0.0 Or Current_value(i) = 0.0 Then
                        Power_yinshu(i) = 0.0

                    Else
                        If box_type = 1 Then
                            Power_yinshu(i) = Format(Power_value(i) / (VB_value * Current_value(i)), "0.00")
                        Else
                            Power_yinshu(i) = Format(Power_value(i) * 1000 / (VB_value * Current_value(i)), "0.00")

                        End If

                    End If
                Else
                    If VC_value = 0.0 Or Current_value(i) = 0.0 Then
                        Power_yinshu(i) = 0.0

                    Else
                        If box_type = 1 Then
                            Power_yinshu(i) = Format(Power_value(i) / (VC_value * Current_value(i)), "0.00")
                        Else
                            Power_yinshu(i) = Format(Power_value(i) * 1000 / (VC_value * Current_value(i)), "0.00")

                        End If

                    End If
                End If


            End If

            If Power_yinshu(i) >= 1 Then
                Power_yinshu(i) = 0.99
            End If
            sendData &= " " & Current_value(i) & " " & Power_value(i) & " " & Power_yinshu(i)

            i += 1
        End While

next1:

        '通过后面的故障信息，分析出故障报警
        '首先三个字节的电压报警信息
        Dim power As String

        Dim chaoshi_tag As Integer = 0

        power = System.Convert.ToInt32(status_list(start_id + 30), 16).ToString

        If power <> 0 And power <> 4 Then
            problem_tag &= "A相：" & get_alarminf("power_" & power, 0, control_box_id) & " "

        End If
        If power = 4 And chaoshi_tag = 0 Then
            problem_tag &= "测量板数据采集超时"
            chaoshi_tag += 1
        End If
        power = System.Convert.ToInt32(status_list(start_id + 31), 16)
        If power <> 0 And power <> 4 Then
            problem_tag &= "B相：" & get_alarminf("power_" & power, 0, control_box_id) & " "

        End If
        If power = 4 And chaoshi_tag = 0 Then
            problem_tag &= "测量板数据采集超时"
            chaoshi_tag += 1
        End If
        power = System.Convert.ToInt32(status_list(start_id + 32), 16)
        If power <> 0 And power <> 4 Then
            problem_tag &= "C相：" & get_alarminf("power_" & power, 0, control_box_id) & " "

        End If
        If power = 4 And chaoshi_tag = 0 Then
            problem_tag &= "测量板数据采集超时"
            chaoshi_tag += 1
        End If


        '判断状态长度是不是正常的报警信息
        If status_list.Length > start_id + 34 Then
            '判断电流报警信息
            Dim huilu_num As Integer '回路的数量
            Dim huilu_id As Integer '回路的编号
            Dim alarm_tag As Integer '报警标志
            huilu_num = System.Convert.ToInt32(status_list(start_id + 33), 16)
            i = 0
            While i < huilu_num * 2
                huilu_id = System.Convert.ToInt32(status_list(start_id + 34 + i), 16)
                alarm_tag = System.Convert.ToInt32(status_list(start_id + 35 + i), 16)
                If alarm_tag <> 0 And alarm_tag <> 9 Then
                    problem_tag &= get_alarminf("current_" & alarm_tag, huilu_id, control_box_id) & "(回路" & huilu_id & ") "
                End If
                If alarm_tag = 9 And chaoshi_tag = 0 Then
                    chaoshi_tag += 1
                    problem_tag &= get_alarminf("current_" & alarm_tag, huilu_id, control_box_id)

                End If
                i += 2
            End While
        End If

    
        If Trim(problem_tag) = "" Then
            problem_tag = "正常"
        End If

        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String
        Dim control_box_name As String = ""
        Dim state_string As String
        Dim board_num As Integer
        msg = ""
        '将handlerflag至为1
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        sql = "update RoadLightStatus set HandlerFlag=1 where ID ='" & ID & "'"
        DBOperation.ExecuteSQL(conn, sql, msg)

        '获取当前电控箱的故障次数
        sql = "select * from control_box where control_box_id='" & control_box_id & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "find_box_state_fun5" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            Exit Sub

        End If
        If rs.RecordCount > 0 Then
            If rs.Fields("problem_num").Value Is System.DBNull.Value Then
                m_controlproblemnum = 0
            Else
                m_controlproblemnum = rs.Fields("problem_num").Value
            End If
            control_box_name = Trim(rs.Fields("control_box_name").Value)
            ' state_string = Trim(rs.Fields("state").Value)
            board_num = rs.Fields("board_num").Value

            '将当前解析出来的数据串增加到control_box表中
            If problem_tag <> "正常" Then
                m_controlproblemnum += 1
                If m_controlproblemnum = 10000 Then
                    m_controlproblemnum = 20
                End If
            Else
                m_controlproblemnum = 0
            End If
            If sendData <> "" Then
                sql = "update control_box set statuscontent='" & sendData & "',state='" & problem_tag & "', Createtime='" & Now() & "', problem_num='" & m_controlproblemnum & "' where control_box_id='" & control_box_id & "'"
            Else
                sql = "update control_box set state='" & problem_tag & "', Createtime='" & Now() & "', problem_num='" & m_controlproblemnum & "' where control_box_id='" & control_box_id & "'"

            End If


            DBOperation.ExecuteSQL(conn, sql, msg)

        End If
        '更新control_box_state中的状态记录
        Dim state_type As String = "状态"
        state_string = ""
        sql = "select state, StatusContent from control_box where control_box_id='" & control_box_id & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "find_box_state_fun4" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            Exit Sub

        End If
        If rs.RecordCount > 0 Then
            state_string = Trim(rs.Fields("state").Value)

            '更新control_box_state中的状态记录
            If state_string <> "" Then
                Setcontrolbox_Record(control_box_name, state_string, "状态")
            End If

            'Setcontrolbox_Record(control_box_name, problem_tag, "状态")

            If problem_tag <> "正常" And m_controlproblemnum = 1 Then  '确认四次故障后发送短信
                '发送短信
                Com_inf.Send_Msg(control_box_id, "", state_string)
                System.Threading.Thread.Sleep(2000)
            End If
        End If

finish:

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub
    ''' <summary>
    ''' 将各个组的回路信息分析出来(ABCABCABCABC)
    ''' </summary>
    ''' <param name="group_id">-1表示老版数据，没有组号；0为第一组；1为第二组；2位第三组</param>
    ''' <param name="status_list">12路回路状态</param>
    ''' <param name="problem_tag" >状态</param>
    ''' <param name="control_box_id" >主控箱编号</param>
    ''' <remarks></remarks>
    Public Sub Get_Huilu_infABC(ByVal group_id As Integer, ByVal group_num As Integer, ByVal status_list() As String, ByVal problem_tag As String, ByVal control_box_id As String)
        Dim VA_value, VB_value, VC_value As Double  '十进制的电压值
        Dim Current_value(11) As Double  '电流值
        Dim Power_value(11) As Double  '电功率
        Dim Power_yinshu(11) As Double   '功率因数
        Dim sendData As String '保存的已经解析过的数据
        Dim start_id As Integer '数据的起始编号
        Dim currentalarm_string As String = "" '分路开关跳闸报警
        Dim box_type As Integer '电控箱的类型
        Dim i As Integer
        Dim presure_type As Integer '电压相位
        Dim control_box_name As String = Com_inf.Get_box_name(control_box_id)
        If group_id = -1 Then
            start_id = 2
        Else
            start_id = 3
        End If
        Get_TopBottom_value()
        box_type = Get_boxtype(control_box_id)  '主控箱类型
        If status_list(start_id) = "FF" And status_list(start_id + 1) = "FF" And status_list(start_id + 2) = "FF" And status_list(start_id + 3) = "FF" And status_list(start_id + 4) = "FF" _
        And status_list(start_id + 5) = "FF" Then
            '此组数据为超时数据不处理
            Exit Sub
        End If

        VA_value = System.Convert.ToInt64(status_list(start_id) & status_list(start_id + 1), 16)
        VA_value = Format(Com_inf.Get_Presure(VA_value, control_box_id), "0.00")
        VB_value = System.Convert.ToInt64(status_list(start_id + 2) & status_list(start_id + 3), 16)
        VB_value = Format(Com_inf.Get_Presure(VB_value, control_box_id), "0.00")
        VC_value = System.Convert.ToInt64(status_list(start_id + 4) & status_list(start_id + 5), 16)
        VC_value = Format(Com_inf.Get_Presure(VC_value, control_box_id), "0.00")

        sendData = VA_value.ToString & " " & VB_value.ToString & " " & VC_value.ToString  '字符串

        Current_value(0) = System.Convert.ToInt64(status_list(start_id + 6) & status_list(start_id + 7), 16)
        Current_value(1) = System.Convert.ToInt64(status_list(start_id + 8) & status_list(start_id + 9), 16)
        Current_value(2) = System.Convert.ToInt64(status_list(start_id + 10) & status_list(start_id + 11), 16)
        Current_value(3) = System.Convert.ToInt64(status_list(start_id + 12) & status_list(start_id + 13), 16)
        Current_value(4) = System.Convert.ToInt64(status_list(start_id + 14) & status_list(start_id + 15), 16)
        Current_value(5) = System.Convert.ToInt64(status_list(start_id + 16) & status_list(start_id + 17), 16)
        Current_value(6) = System.Convert.ToInt64(status_list(start_id + 18) & status_list(start_id + 19), 16)
        Current_value(7) = System.Convert.ToInt64(status_list(start_id + 20) & status_list(start_id + 21), 16)
        Current_value(8) = System.Convert.ToInt64(status_list(start_id + 22) & status_list(start_id + 23), 16)
        Current_value(9) = System.Convert.ToInt64(status_list(start_id + 24) & status_list(start_id + 25), 16)
        Current_value(10) = System.Convert.ToInt64(status_list(start_id + 26) & status_list(start_id + 27), 16)
        Current_value(11) = System.Convert.ToInt64(status_list(start_id + 28) & status_list(start_id + 29), 16)


        Power_value(0) = System.Convert.ToInt64(status_list(start_id + 30) & status_list(start_id + 31), 16)
        Power_value(1) = System.Convert.ToInt64(status_list(start_id + 32) & status_list(start_id + 33), 16)
        Power_value(2) = System.Convert.ToInt64(status_list(start_id + 34) & status_list(start_id + 35), 16)
        Power_value(3) = System.Convert.ToInt64(status_list(start_id + 36) & status_list(start_id + 37), 16)
        Power_value(4) = System.Convert.ToInt64(status_list(start_id + 38) & status_list(start_id + 39), 16)
        Power_value(5) = System.Convert.ToInt64(status_list(start_id + 40) & status_list(start_id + 41), 16)
        Power_value(6) = System.Convert.ToInt64(status_list(start_id + 42) & status_list(start_id + 43), 16)
        Power_value(7) = System.Convert.ToInt64(status_list(start_id + 44) & status_list(start_id + 45), 16)
        Power_value(8) = System.Convert.ToInt64(status_list(start_id + 46) & status_list(start_id + 47), 16)
        Power_value(9) = System.Convert.ToInt64(status_list(start_id + 48) & status_list(start_id + 49), 16)
        Power_value(10) = System.Convert.ToInt64(status_list(start_id + 50) & status_list(start_id + 51), 16)
        Power_value(11) = System.Convert.ToInt64(status_list(start_id + 52) & status_list(start_id + 53), 16)


        i = 0
        While (i < 12)
            Current_value(i) = Format(Get_Current(Current_value(i), control_box_id, i + 1 + group_id * 12, box_type), "0.00")  '电流


            If Current_value(i) = 0 Then
                Power_value(i) = Format(0, "0.00")
            Else

                Power_value(i) = Format(Get_Power1(Power_value(i), control_box_id, i + 1 + group_id * 12, box_type), "0.00") '功率
            End If

            '原来根据默认的AAAABBBBCCCC顺序进行求功率因数，2012年5月3日更改为按照用户的设定求功率因数，
            '如果不设则按默认的ABCABCABCABC
            presure_type = get_presuretype(i + 1 + group_id * 12, control_box_name)


            If presure_type = 0 Then
                If VA_value = 0.0 Or Current_value(i) = 0.0 Then
                    Power_yinshu(i) = 0.0

                Else
                    If box_type = 1 Then
                        Power_yinshu(i) = Format(Power_value(i) / (VA_value * Current_value(i)), "0.00")
                    Else
                        Power_yinshu(i) = Format(Power_value(i) * 1000 / (VA_value * Current_value(i)), "0.00")

                    End If

                End If
            Else
                If presure_type = 1 Then
                    If VB_value = 0.0 Or Current_value(i) = 0.0 Then
                        Power_yinshu(i) = 0.0

                    Else
                        If box_type = 1 Then
                            Power_yinshu(i) = Format(Power_value(i) / (VB_value * Current_value(i)), "0.00")
                        Else
                            Power_yinshu(i) = Format(Power_value(i) * 1000 / (VB_value * Current_value(i)), "0.00")

                        End If

                    End If
                Else
                    If VC_value = 0.0 Or Current_value(i) = 0.0 Then
                        Power_yinshu(i) = 0.0

                    Else
                        If box_type = 1 Then
                            Power_yinshu(i) = Format(Power_value(i) / (VC_value * Current_value(i)), "0.00")
                        Else
                            Power_yinshu(i) = Format(Power_value(i) * 1000 / (VC_value * Current_value(i)), "0.00")

                        End If

                    End If
                End If


            End If

            If Power_yinshu(i) >= 1 Then
                Power_yinshu(i) = 0.99
            End If

            sendData &= " " & Current_value(i) & " " & Power_value(i) & " " & Power_yinshu(i)
            i += 1
        End While

        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String
        ' Dim control_box_name As String = ""
        msg = ""
        '将handlerflag至为1
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If

        '更新电控箱的三遥数据
        sql = "select * from control_box where control_box_id='" & control_box_id & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "find_box_state_fun3" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            Exit Sub

        End If
        If rs.RecordCount > 0 Then

            If group_id = -1 Or group_id = 0 Then
                sql = "update control_box set statuscontent='" & sendData & "', Createtime='" & Now() & "', problem_num='" & m_controlproblemnum & "' where control_box_id='" & control_box_id & "'"
            Else
                If group_id = 1 Then

                    sql = "update control_box set statuscontent2='" & sendData & "', Createtime='" & Now() & "', problem_num='" & m_controlproblemnum & "' where control_box_id='" & control_box_id & "'"
                Else
                    sql = "update control_box set statuscontent3='" & sendData & "', Createtime='" & Now() & "', problem_num='" & m_controlproblemnum & "' where control_box_id='" & control_box_id & "'"

                End If
            End If
            DBOperation.ExecuteSQL(conn, sql, msg)

        End If



        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    ''' 将各个组的回路信息分析出来(2011年9月5日增加的小三遥处理)(ABCABC)
    ''' </summary>
    ''' <param name="group_id">-1表示老版数据，没有组号；0为第一组；1为第二组；2位第三组</param>
    ''' <param name="status_list">6路回路状态</param>
    ''' <param name="problem_tag" >状态</param>
    ''' <param name="control_box_id" >主控箱编号</param>
    ''' <param name="ID" >记录编号</param>
    ''' <remarks></remarks>
    Public Sub Get_Huilu_inf_smallABC(ByVal group_id As Integer, ByVal status_list() As String, ByVal problem_tag As String, ByVal control_box_id As String, ByVal ID As Integer)

        Dim VA_value, VB_value, VC_value As Double  '十进制的电压值
        Dim Current_value(11) As Double  '电流值
        Dim Power_value(11) As Double  '电功率
        Dim Power_yinshu(11) As Double   '功率因数
        Dim sendData As String = "" '保存的已经解析过的数据
        Dim start_id As Integer '数据的起始编号
        Dim currentalarm_string As String = "" '分路开关跳闸报警
        Dim box_type As Integer '电控箱的类型
        Dim i As Integer
        Dim presure_type As Integer '相位 0， 1，2
        Dim control_box_name As String = Com_inf.Get_box_name(control_box_id)
        If group_id = -1 Then
            start_id = 2
        Else
            start_id = 3
        End If
        Get_TopBottom_value()
        If status_list(start_id) = "FF" And status_list(start_id + 1) = "FF" And status_list(start_id + 2) = "FF" And status_list(start_id + 3) = "FF" And status_list(start_id + 4) = "FF" _
        And status_list(start_id + 5) = "FF" Then
            '超时数据，丢弃数据
            sendData = ""
            GoTo next1

        End If

        box_type = Get_boxtype(control_box_id)  '主控箱类型
        VA_value = System.Convert.ToInt64(status_list(start_id) & status_list(start_id + 1), 16)
        VA_value = Format(Com_inf.Get_Presure(VA_value, control_box_id), "0.00")
        VB_value = System.Convert.ToInt64(status_list(start_id + 2) & status_list(start_id + 3), 16)
        VB_value = Format(Com_inf.Get_Presure(VB_value, control_box_id), "0.00")
        VC_value = System.Convert.ToInt64(status_list(start_id + 4) & status_list(start_id + 5), 16)
        VC_value = Format(Com_inf.Get_Presure(VC_value, control_box_id), "0.00")
        sendData = VA_value.ToString & " " & VB_value.ToString & " " & VC_value.ToString  '字符串

        Current_value(0) = System.Convert.ToInt64(status_list(start_id + 6) & status_list(start_id + 7), 16)
        Current_value(1) = System.Convert.ToInt64(status_list(start_id + 8) & status_list(start_id + 9), 16)
        Current_value(2) = System.Convert.ToInt64(status_list(start_id + 10) & status_list(start_id + 11), 16)
        Current_value(3) = System.Convert.ToInt64(status_list(start_id + 12) & status_list(start_id + 13), 16)
        Current_value(4) = System.Convert.ToInt64(status_list(start_id + 14) & status_list(start_id + 15), 16)
        Current_value(5) = System.Convert.ToInt64(status_list(start_id + 16) & status_list(start_id + 17), 16)
        Power_value(0) = System.Convert.ToInt64(status_list(start_id + 18) & status_list(start_id + 19), 16)
        Power_value(1) = System.Convert.ToInt64(status_list(start_id + 20) & status_list(start_id + 21), 16)
        Power_value(2) = System.Convert.ToInt64(status_list(start_id + 22) & status_list(start_id + 23), 16)
        Power_value(3) = System.Convert.ToInt64(status_list(start_id + 24) & status_list(start_id + 25), 16)
        Power_value(4) = System.Convert.ToInt64(status_list(start_id + 26) & status_list(start_id + 27), 16)
        Power_value(5) = System.Convert.ToInt64(status_list(start_id + 28) & status_list(start_id + 29), 16)


        i = 0
        While (i < 6)
            Current_value(i) = Format(Get_Current(Current_value(i), control_box_id, i + 1, box_type), "0.00")  '电流

            If Current_value(i) = 0 Then
                Power_value(i) = Format(0, "0.00")
            Else

                Power_value(i) = Format(Get_Power1(Power_value(i), control_box_id, i + 1, box_type), "0.00") '功率
            End If

            '原来根据默认的AABBCC顺序进行求功率因数，2012年5月3日更改为按照用户的设定求功率因数，
            '如果不设则按默认的ABCABC()
            presure_type = get_presuretype(i + 1, control_box_name)

            If presure_type = 0 Then
                If VA_value = 0.0 Or Current_value(i) = 0.0 Then
                    Power_yinshu(i) = 0.0

                Else
                    If box_type = 1 Then
                        Power_yinshu(i) = Format(Power_value(i) / (VA_value * Current_value(i)), "0.00")
                    Else
                        Power_yinshu(i) = Format(Power_value(i) * 1000 / (VA_value * Current_value(i)), "0.00")

                    End If

                End If
            Else
                If presure_type = 1 Then
                    If VB_value = 0.0 Or Current_value(i) = 0.0 Then
                        Power_yinshu(i) = 0.0

                    Else
                        If box_type = 1 Then
                            Power_yinshu(i) = Format(Power_value(i) / (VB_value * Current_value(i)), "0.00")
                        Else
                            Power_yinshu(i) = Format(Power_value(i) * 1000 / (VB_value * Current_value(i)), "0.00")

                        End If

                    End If
                Else
                    If VC_value = 0.0 Or Current_value(i) = 0.0 Then
                        Power_yinshu(i) = 0.0

                    Else
                        If box_type = 1 Then
                            Power_yinshu(i) = Format(Power_value(i) / (VC_value * Current_value(i)), "0.00")
                        Else
                            Power_yinshu(i) = Format(Power_value(i) * 1000 / (VC_value * Current_value(i)), "0.00")

                        End If

                    End If
                End If


            End If

            If Power_yinshu(i) >= 1 Then
                Power_yinshu(i) = 0.99
            End If
            sendData &= " " & Current_value(i) & " " & Power_value(i) & " " & Power_yinshu(i)

            i += 1
        End While

next1:

        '通过后面的故障信息，分析出故障报警
        '首先三个字节的电压报警信息
        Dim power As String

        Dim chaoshi_tag As Integer = 0

        power = System.Convert.ToInt32(status_list(start_id + 30), 16).ToString

        If power <> 0 And power <> 4 Then
            problem_tag &= "A相：" & get_alarminf("power_" & power, 0, control_box_id) & " "

        End If
        If power = 4 And chaoshi_tag = 0 Then
            problem_tag &= "测量板数据采集超时"
            chaoshi_tag += 1
        End If
        power = System.Convert.ToInt32(status_list(start_id + 31), 16)
        If power <> 0 And power <> 4 Then
            problem_tag &= "B相：" & get_alarminf("power_" & power, 0, control_box_id) & " "

        End If
        If power = 4 And chaoshi_tag = 0 Then
            problem_tag &= "测量板数据采集超时"
            chaoshi_tag += 1
        End If
        power = System.Convert.ToInt32(status_list(start_id + 32), 16)
        If power <> 0 And power <> 4 Then
            problem_tag &= "C相：" & get_alarminf("power_" & power, 0, control_box_id) & " "

        End If
        If power = 4 And chaoshi_tag = 0 Then
            problem_tag &= "测量板数据采集超时"
            chaoshi_tag += 1
        End If
        '2012年6月11日如果三相电压均缺相的报失压报警
        If problem_tag = "A相：缺相 B相：缺相 C相：缺相 " Then
            problem_tag = "失压 "
        End If

        '判断状态长度是不是正常的报警信息
        If status_list.Length > start_id + 34 Then
            '判断电流报警信息
            Dim huilu_num As Integer '回路的数量
            Dim huilu_id As Integer '回路的编号
            Dim alarm_tag As Integer '报警标志
            huilu_num = System.Convert.ToInt32(status_list(start_id + 33), 16)
            i = 0
            While i < huilu_num * 2
                huilu_id = System.Convert.ToInt32(status_list(start_id + 34 + i), 16)
                alarm_tag = System.Convert.ToInt32(status_list(start_id + 35 + i), 16)
                '2012年6月11日去除辅助触点故障
                If alarm_tag <> 0 And alarm_tag <> 9 And alarm_tag <> 5 Then
                    problem_tag &= get_alarminf("current_" & alarm_tag, huilu_id, control_box_id) & "(回路" & huilu_id & ") "
                End If
                If alarm_tag = 9 And chaoshi_tag = 0 Then
                    chaoshi_tag += 1
                    problem_tag &= get_alarminf("current_" & alarm_tag, huilu_id, control_box_id)

                End If
                i += 2
            End While
        End If


        If Trim(problem_tag) = "" Then
            problem_tag = "正常"
        End If

        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String
        'Dim control_box_name As String = ""
        Dim state_string As String
        Dim board_num As Integer
        msg = ""
        '将handlerflag至为1
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        sql = "update RoadLightStatus set HandlerFlag=1 where ID ='" & ID & "'"
        DBOperation.ExecuteSQL(conn, sql, msg)

        '获取当前电控箱的故障次数
        sql = "select * from control_box where control_box_id='" & control_box_id & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "find_box_state_fun5" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            Exit Sub

        End If
        If rs.RecordCount > 0 Then
            If rs.Fields("problem_num").Value Is System.DBNull.Value Then
                m_controlproblemnum = 0
            Else
                m_controlproblemnum = rs.Fields("problem_num").Value
            End If
            control_box_name = Trim(rs.Fields("control_box_name").Value)
            ' state_string = Trim(rs.Fields("state").Value)
            board_num = rs.Fields("board_num").Value

            '将当前解析出来的数据串增加到control_box表中
            If problem_tag <> "正常" Then
                m_controlproblemnum += 1
                If m_controlproblemnum = 10000 Then
                    m_controlproblemnum = 20
                End If
            Else
                m_controlproblemnum = 0
            End If
            If sendData <> "" Then
                sql = "update control_box set statuscontent='" & sendData & "',state='" & problem_tag & "', Createtime='" & Now() & "', problem_num='" & m_controlproblemnum & "' where control_box_id='" & control_box_id & "'"
            Else
                sql = "update control_box set state='" & problem_tag & "', Createtime='" & Now() & "', problem_num='" & m_controlproblemnum & "' where control_box_id='" & control_box_id & "'"

            End If


            DBOperation.ExecuteSQL(conn, sql, msg)

        End If
        '更新control_box_state中的状态记录
        Dim state_type As String = "状态"
        state_string = ""
        sql = "select state, StatusContent from control_box where control_box_id='" & control_box_id & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "find_box_state_fun4" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            Exit Sub

        End If
        If rs.RecordCount > 0 Then
            state_string = Trim(rs.Fields("state").Value)

            '更新control_box_state中的状态记录
            If state_string <> "" Then
                Setcontrolbox_Record(control_box_name, state_string, "状态")
            End If

            'Setcontrolbox_Record(control_box_name, problem_tag, "状态")

            If problem_tag <> "正常" And m_controlproblemnum = 4 Then  '确认四次故障后发送短信
                '发送短信
                Com_inf.Send_Msg(control_box_id, "", state_string)
                System.Threading.Thread.Sleep(2000)
            End If
        End If

finish:

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    ''' 根据回路的设置获取电压相位
    ''' </summary>
    ''' <param name="huilu_id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function get_presuretype(ByVal huilu_id As Integer, ByVal control_box_name As String)
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String

        msg = ""
        sql = "select presure_type from huilu_inf where huilu_id='" & huilu_id & "' and control_box_name='" & control_box_name & "'"

        If DBOperation.OpenConn(conn) = False Then
            get_presuretype = 0  '默认的情况是A相
            Exit Function
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs.RecordCount > 0 Then
            '该回路已经设置了电压相位
            get_presuretype = rs.Fields("presure_type").Value
        Else
            get_presuretype = (huilu_id - 1) Mod 3  '默认为ABC
        End If

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing

    End Function


    ''' <summary>
    ''' 通过上传信息获取故障信息
    ''' </summary>
    ''' <param name="tag"></param>
    ''' <remarks></remarks>
    Private Function get_alarminf(ByVal tag As String, ByVal huilu_id As Integer, ByVal control_box_id As String) As String
        Dim rs, rs_jiechuqi As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim control_box_name As String


        msg = ""
        sql = "select type from sysconfig where name='" & tag & "'"
        get_alarminf = ""
        If DBOperation.OpenConn(conn) = False Then
            Exit Function
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "get_alarminf" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing

            Exit Function
        End If
        If rs.RecordCount > 0 Then
            get_alarminf = Trim(rs.Fields("type").Value)
            control_box_name = Com_inf.Get_box_name(control_box_id)

            If tag = "current_1" Or tag = "current_2" Or tag = "current_4" Or tag = "current_5" Then
                '接触器故障
                sql = "select jiechuqi_id from huilu_inf where huilu_id='" & huilu_id & "' and control_box_name='" & control_box_name & "'"
                rs_jiechuqi = DBOperation.SelectSQL(conn, sql, msg)

                If rs_jiechuqi Is Nothing Then
                    g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "get_alarminf" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)
                    rs.Close()
                    rs = Nothing
                    conn.Close()
                    conn = Nothing

                    Exit Function
                End If
                If rs_jiechuqi.RecordCount > 0 Then
                    get_alarminf = "(K" & rs_jiechuqi.Fields("jiechuqi_id").Value.ToString & ")" & get_alarminf
                End If
            End If

        End If

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Function
    ''' <summary>
    ''' 将各个组的回路信息分析出来
    ''' </summary>
    ''' <param name="group_id">-1表示老版数据，没有组号；0为第一组；1为第二组；2位第三组</param>
    ''' <param name="status_list">12路回路状态</param>
    ''' <param name="problem_tag" >状态</param>
    ''' <param name="control_box_id" >主控箱编号</param>
    ''' <remarks></remarks>
    Public Sub Get_Huilu_inf(ByVal group_id As Integer, ByVal group_num As Integer, ByVal status_list() As String, ByVal problem_tag As String, ByVal control_box_id As String)
        Dim VA_value, VB_value, VC_value As Double  '十进制的电压值
        Dim Current_value(11) As Double  '电流值
        Dim Power_value(11) As Double  '电功率
        Dim Power_yinshu(11) As Double   '功率因数
        Dim sendData As String '保存的已经解析过的数据
        Dim start_id As Integer '数据的起始编号
        Dim currentalarm_string As String = "" '分路开关跳闸报警
        Dim box_type As Integer '电控箱的类型
        Dim i As Integer
        If group_id = -1 Then
            start_id = 2
        Else
            start_id = 3
        End If
        Get_TopBottom_value()
        box_type = Get_boxtype(control_box_id)  '主控箱类型
        If status_list(start_id) = "FF" And status_list(start_id + 1) = "FF" And status_list(start_id + 2) = "FF" And status_list(start_id + 3) = "FF" And status_list(start_id + 4) = "FF" _
        And status_list(start_id + 5) = "FF" Then
            '此组数据为超时数据不处理
            Exit Sub
        End If

        VA_value = System.Convert.ToInt64(status_list(start_id) & status_list(start_id + 1), 16)
        VA_value = Format(Com_inf.Get_Presure(VA_value, control_box_id), "0.00")
        VB_value = System.Convert.ToInt64(status_list(start_id + 2) & status_list(start_id + 3), 16)
        VB_value = Format(Com_inf.Get_Presure(VB_value, control_box_id), "0.00")
        VC_value = System.Convert.ToInt64(status_list(start_id + 4) & status_list(start_id + 5), 16)
        VC_value = Format(Com_inf.Get_Presure(VC_value, control_box_id), "0.00")

        sendData = VA_value.ToString & " " & VB_value.ToString & " " & VC_value.ToString  '字符串

        Current_value(0) = System.Convert.ToInt64(status_list(start_id + 6) & status_list(start_id + 7), 16)
        Current_value(1) = System.Convert.ToInt64(status_list(start_id + 8) & status_list(start_id + 9), 16)
        Current_value(2) = System.Convert.ToInt64(status_list(start_id + 10) & status_list(start_id + 11), 16)
        Current_value(3) = System.Convert.ToInt64(status_list(start_id + 12) & status_list(start_id + 13), 16)
        Current_value(4) = System.Convert.ToInt64(status_list(start_id + 14) & status_list(start_id + 15), 16)
        Current_value(5) = System.Convert.ToInt64(status_list(start_id + 16) & status_list(start_id + 17), 16)
        Current_value(6) = System.Convert.ToInt64(status_list(start_id + 18) & status_list(start_id + 19), 16)
        Current_value(7) = System.Convert.ToInt64(status_list(start_id + 20) & status_list(start_id + 21), 16)
        Current_value(8) = System.Convert.ToInt64(status_list(start_id + 22) & status_list(start_id + 23), 16)
        Current_value(9) = System.Convert.ToInt64(status_list(start_id + 24) & status_list(start_id + 25), 16)
        Current_value(10) = System.Convert.ToInt64(status_list(start_id + 26) & status_list(start_id + 27), 16)
        Current_value(11) = System.Convert.ToInt64(status_list(start_id + 28) & status_list(start_id + 29), 16)


        Power_value(0) = System.Convert.ToInt64(status_list(start_id + 30) & status_list(start_id + 31), 16)
        Power_value(1) = System.Convert.ToInt64(status_list(start_id + 32) & status_list(start_id + 33), 16)
        Power_value(2) = System.Convert.ToInt64(status_list(start_id + 34) & status_list(start_id + 35), 16)
        Power_value(3) = System.Convert.ToInt64(status_list(start_id + 36) & status_list(start_id + 37), 16)
        Power_value(4) = System.Convert.ToInt64(status_list(start_id + 38) & status_list(start_id + 39), 16)
        Power_value(5) = System.Convert.ToInt64(status_list(start_id + 40) & status_list(start_id + 41), 16)
        Power_value(6) = System.Convert.ToInt64(status_list(start_id + 42) & status_list(start_id + 43), 16)
        Power_value(7) = System.Convert.ToInt64(status_list(start_id + 44) & status_list(start_id + 45), 16)
        Power_value(8) = System.Convert.ToInt64(status_list(start_id + 46) & status_list(start_id + 47), 16)
        Power_value(9) = System.Convert.ToInt64(status_list(start_id + 48) & status_list(start_id + 49), 16)
        Power_value(10) = System.Convert.ToInt64(status_list(start_id + 50) & status_list(start_id + 51), 16)
        Power_value(11) = System.Convert.ToInt64(status_list(start_id + 52) & status_list(start_id + 53), 16)


        i = 0
        While (i < 12)
            Current_value(i) = Format(Get_Current(Current_value(i), control_box_id, i + 1, box_type), "0.00")  '电流


            If Current_value(i) = 0 Then
                Power_value(i) = Format(0, "0.00")
            Else

                Power_value(i) = Format(Get_Power1(Power_value(i), control_box_id, i + 1, box_type), "0.00") '功率
            End If

            If i <= 3 Then
                If VA_value = 0.0 Or Current_value(i) = 0.0 Then
                    Power_yinshu(i) = 0.0

                Else
                    If box_type = 1 Then
                        Power_yinshu(i) = Format(Power_value(i) / (VA_value * Current_value(i)), "0.00")
                    Else
                        Power_yinshu(i) = Format(Power_value(i) * 1000 / (VA_value * Current_value(i)), "0.00")

                    End If

                End If
            Else
                If i <= 7 Then
                    If VB_value = 0.0 Or Current_value(i) = 0.0 Then
                        Power_yinshu(i) = 0.0

                    Else
                        If box_type = 1 Then
                            Power_yinshu(i) = Format(Power_value(i) / (VB_value * Current_value(i)), "0.00")
                        Else
                            Power_yinshu(i) = Format(Power_value(i) * 1000 / (VB_value * Current_value(i)), "0.00")

                        End If

                    End If
                Else
                    If VC_value = 0.0 Or Current_value(i) = 0.0 Then
                        Power_yinshu(i) = 0.0

                    Else
                        If box_type = 1 Then
                            Power_yinshu(i) = Format(Power_value(i) / (VC_value * Current_value(i)), "0.00")
                        Else
                            Power_yinshu(i) = Format(Power_value(i) * 1000 / (VC_value * Current_value(i)), "0.00")

                        End If

                    End If
                End If


            End If

            If Power_yinshu(i) >= 1 Then
                Power_yinshu(i) = 0.99
            End If

            sendData &= " " & Current_value(i) & " " & Power_value(i) & " " & Power_yinshu(i)
            i += 1
        End While

        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String
        Dim control_box_name As String = ""
        msg = ""
        '将handlerflag至为1
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If

        '更新电控箱的三遥数据
        sql = "select * from control_box where control_box_id='" & control_box_id & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "find_box_state_fun3" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            Exit Sub

        End If
        If rs.RecordCount > 0 Then

            If group_id = -1 Or group_id = 0 Then
                sql = "update control_box set statuscontent='" & sendData & "', Createtime='" & Now() & "', problem_num='" & m_controlproblemnum & "' where control_box_id='" & control_box_id & "'"
            Else
                If group_id = 1 Then

                    sql = "update control_box set statuscontent2='" & sendData & "', Createtime='" & Now() & "', problem_num='" & m_controlproblemnum & "' where control_box_id='" & control_box_id & "'"
                Else
                    sql = "update control_box set statuscontent3='" & sendData & "', Createtime='" & Now() & "', problem_num='" & m_controlproblemnum & "' where control_box_id='" & control_box_id & "'"

                End If
            End If
            DBOperation.ExecuteSQL(conn, sql, msg)

        End If



        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    ''' 根据电流值，进行跳闸报警判断
    ''' </summary>
    ''' <param name="current"></param>
    ''' <param name="control_box_id" >电控箱编号</param>
    ''' <remarks></remarks>
    Private Function Current_Alarm(ByVal current As Double, ByVal control_box_id As String, ByVal huilu_id As Integer) As String
        Dim rs, rs_jiechuqi As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String
        Dim jiechuqi_id As String
      
        msg = ""
        Current_Alarm = ""
        If DBOperation.OpenConn(conn) = False Then
            Exit Function
        End If
        sql = "SELECT huilu_id, jiechuqi_id, open_close, huilu_inf.control_box_name FROM huilu_inf INNER JOIN " _
        & "control_box ON huilu_inf.control_box_name = control_box.control_box_name where control_box.control_box_id='" & control_box_id & "' and huilu_id='" & huilu_id & "'"

        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then

            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Current_Alarm" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            Current_Alarm = ""
            Exit Function
        End If
        If rs.RecordCount > 0 Then
            '判断接触器的闭合或断开
            jiechuqi_id = Trim(rs.Fields("jiechuqi_id").Value)
            While jiechuqi_id.Length < LAMP_ID_LEN
                jiechuqi_id = "0" & jiechuqi_id
            End While
            jiechuqi_id = control_box_id & "31" & jiechuqi_id
            sql = "select state, result from lamp_inf where lamp_id='" & jiechuqi_id & "'"
            rs_jiechuqi = DBOperation.SelectSQL(conn, sql, msg)
            If rs_jiechuqi Is Nothing Then
                g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Current_Alarm" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

                GoTo finish
            End If
            If rs_jiechuqi.Fields("state").Value = 1 Or rs_jiechuqi.Fields("state").Value = 4 Then
                '接触器时闭合的
                If current < 0.5 Then
                    Current_Alarm = "分路开关跳闸"
                End If
            End If
        
        End If
        If rs_jiechuqi.State = 1 Then
            rs_jiechuqi.Close()
            rs_jiechuqi = Nothing
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
    ''' 转换成电流的公式（主控箱）
    ''' </summary>
    ''' <param name="Current"></param>
    ''' <param name="huilu_id" >回路ID</param>
    ''' <param name="box_id" >主控箱编号</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Get_Current(ByVal Current As Double, ByVal box_id As String, ByVal huilu_id As Integer, ByVal box_type As Integer) As Double
        ''原来没有变比的
        'Get_Current = Current / &H7FFF * 176.78 / 150 * 100
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim bianbi As Integer
        ' Dim box_type As Integer '主控箱编号
        m_currenttopvalue = 50
        m_currentbottomvalue = 0
        If DBOperation.OpenConn(conn) = False Then
            Exit Function
        End If
        msg = ""
        ' box_type = 1
        '根据电控箱编号和回路编号查找其对应的变比值，默认的情况为1
        bianbi = 1
        sql = "SELECT control_box_type,huilu_inf.bianbi , control_box.control_box_id,huilu_inf.current_alarmtop, huilu_inf.current_alarmbot FROM  control_box INNER JOIN huilu_inf ON control_box.control_box_name = huilu_inf.control_box_name where control_box_id='" & box_id & "' and huilu_id='" & huilu_id & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Get_Current" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            Get_Current = 0
            Exit Function
        End If
        If rs.RecordCount > 0 Then
            bianbi = rs.Fields("bianbi").Value
            m_currenttopvalue = rs.Fields("current_alarmtop").Value
            m_currentbottomvalue = rs.Fields("current_alarmbot").Value
            'box_type = rs.Fields("control_box_type").Value
        End If

        'Get_Current = Current / &H7FFF * 176.78 / 150 * 5
        '2011-4-21更改公式
        If box_type = 1 Then
            Get_Current = Current / &H7FFF * 176.78 / 150 * 5
        Else
            Get_Current = Current * 8.3 / &HFFFF
        End If
        '2012年1月11日，霸州特殊处理，电流《0.2伏则置0
        If Get_Current <= 0.2 Then
            Get_Current = 0

        End If
        Get_Current = Get_Current * bianbi

        '2011年3月14日删除一个主控箱一个变比值的设置方法
        'sql = "select information from RoadIDAndIMEI where RoadID='" & Val(box_id) & "'"
        'rs = DBOperation.SelectSQL(conn, sql, msg)
        'If rs Is Nothing Then
        '    MsgBox(MSG_ERROR_STRING, , PROJECT_TITLE_STRING)
        '    conn.Close()
        '    conn = Nothing
        '    Get_Current = 0
        '    Exit Function
        'End If
        'If rs.RecordCount > 0 Then
        '    '增加变比
        '    If rs.Fields("information").Value Is Nothing Then
        '        bianbi = 1
        '    Else
        '        bianbi = Val(Trim(rs.Fields("information").Value))

        '    End If

        '    Get_Current = Current / &H7FFF * 176.78 / 150 * 5

        '    If Get_Current <= 0.1 Then
        '        Get_Current = 0
        '    Else
        '        Get_Current = Get_Current * bianbi
        '    End If
        'Else
        '    Get_Current = 0
        'End If
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Function


    ''' <summary>
    ''' 逆向转换成电流的公式（主控箱）
    ''' </summary>
    ''' <param name="Current"></param>
    ''' <param name="huilu_id" >回路ID</param>
    ''' <param name="box_id" >主控箱编号</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Get_EXECurrent(ByVal Current As Double, ByVal box_id As String, ByVal huilu_id As Integer, ByVal box_type As Integer) As Integer
        ''原来没有变比的
        'Get_Current = Current / &H7FFF * 176.78 / 150 * 100
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim bianbi As Integer
        ' Dim box_type As Integer '主控箱编号
      
        If DBOperation.OpenConn(conn) = False Then
            Exit Function
        End If
        msg = ""
        ' box_type = 1
        '根据电控箱编号和回路编号查找其对应的变比值，默认的情况为1
        bianbi = 1
        sql = "SELECT control_box_type,huilu_inf.bianbi , control_box.control_box_id,huilu_inf.current_alarmtop, huilu_inf.current_alarmbot FROM  control_box INNER JOIN huilu_inf ON control_box.control_box_name = huilu_inf.control_box_name where control_box_id='" & box_id & "' and huilu_id='" & huilu_id & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Get_Current" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            Get_EXECurrent = 0
            Exit Function
        End If
        If rs.RecordCount > 0 Then
            bianbi = rs.Fields("bianbi").Value
        End If

        'Get_Current = Current / &H7FFF * 176.78 / 150 * 5
        Current = Current / bianbi

        '2011-4-21更改公式
        If box_type = 1 Then
            Get_EXECurrent = Current * &H7FFF / 176.78 * 150 / 5
        Else
            Get_EXECurrent = Current / 8.3 * &HFFFF
        End If


        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Function

    ''' <summary>
    ''' 转换成电功率的公式（主控箱）
    ''' </summary>
    ''' <param name="Power1"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Get_Power1(ByVal Power1 As Double, ByVal box_id As String, ByVal huilu_id As Integer, ByVal box_type As Integer) As Double
        ''原来没有变比的
        'Get_Power1 = Power1 / &H7FFF * 300 * 100 * 2.7
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim bianbi As Integer
        ' Dim box_type As Integer

        If DBOperation.OpenConn(conn) = False Then
            Exit Function
        End If
        msg = ""
        '   box_type = 1
        '根据电控箱编号和回路编号查找其对应的变比值，默认的情况为1
        bianbi = 1
        sql = "SELECT control_box_type,huilu_inf.bianbi FROM  control_box INNER JOIN huilu_inf ON control_box.control_box_name = huilu_inf.control_box_name where control_box_id='" & box_id & "' and huilu_id='" & huilu_id & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Get_Power1" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            Get_Power1 = 0
            Exit Function
        End If
        If rs.RecordCount > 0 Then
            bianbi = rs.Fields("bianbi").Value
            ' box_type = rs.Fields("control_box_type").Value
        Else
            bianbi = 1
        End If
        '        Get_Power1 = Power1 / &H7FFF * 300 * 5 * bianbi
        '2011-4-21更改公式
        If box_type = 1 Then
            Get_Power1 = Power1 / &H7FFF * 300 * 5 * bianbi
        Else
            Get_Power1 = Power1 * 4.05 / &HFFFF * bianbi

        End If

        ''2011年3月14日删除一个主控箱一个变比的设置
        'sql = "select information from RoadIDAndIMEI where RoadID='" & Val(box_id) & "'"
        'rs = DBOperation.SelectSQL(conn, sql, msg)
        'If rs Is Nothing Then
        '    MsgBox(MSG_ERROR_STRING, , PROJECT_TITLE_STRING)
        '    conn.Close()
        '    conn = Nothing
        '    Get_Power1 = 0
        '    Exit Function
        'End If
        'If rs.RecordCount > 0 Then
        '    '增加变比
        '    If rs.Fields("information").Value Is Nothing Then
        '        bianbi = 1
        '    Else
        '        bianbi = Val(Trim(rs.Fields("information").Value))

        '    End If
        '    Get_Power1 = Power1 / &H7FFF * 300 * 5 * bianbi
        'Else
        '    Get_Power1 = 0
        'End If
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing


    End Function

    '设置子节点状态
    Public Function setChild(ByVal node As TreeNode) As Boolean
        '    Dim child As New TreeNode
        For Each child As TreeNode In node.Nodes
            child.Checked = node.Checked
        Next
        setChild = True

    End Function

    '设置父节点状态
    Public Sub setparent(ByVal node As TreeNode)
        ' Dim brother As New TreeNode
        If node.Parent IsNot Nothing Then
            '如果当前节点状态为勾选，则需要所有兄弟节点都勾选才能勾选父节点
            If node.Checked Then
                For Each brother As TreeNode In node.Parent.Nodes
                    If brother.Checked = False Then
                        Exit Sub
                    End If

                Next
                node.Parent.Checked = node.Checked
            End If

        End If
    End Sub

    ''' <summary>
    ''' 将上传的开关量数据分为电控箱号和将2个字节的开关量分解为16位的0、1表示
    ''' </summary>
    ''' <param name="alarm_list" >解析后的开关量数组</param>
    ''' <param name="kaiguan_string" >上传的开关量字符串,2字节的电控箱编号，2字节的状态上传数据</param>
    ''' <remarks></remarks>
    Public Sub get_kaiguanString(ByVal kaiguan_string As String, ByVal alarm_list() As String)
        Dim str() As String
        Dim control_box_id As String  '电控箱编号
        Dim kaiguan As String  '16位的开关量数据
        Dim power_type As String '电源类型

        str = kaiguan_string.Split(" ")
        control_box_id = System.Convert.ToInt32(str(0) & str(1), 16)  '前两个字节作为电控箱编号
        While control_box_id.Length < 4
            control_box_id = "0" & control_box_id
        End While
        '2011年5月26日修改，开关量2个字节中前一个字节是低八位，后一个字节是高八位
        kaiguan = Com_inf.HEX_to_BIN(str(3) & str(2))
        power_type = Val(str(4)).ToString
        alarm_list(0) = control_box_id
        alarm_list(1) = kaiguan
        alarm_list(2) = power_type

    End Sub

    ''' <summary>
    ''' 根据上传的开关量的前六个开关量值，默认给接触器的，判断接触器的实际状态
    ''' </summary>
    ''' <param name="kaiguan_tag" >开关量1-16</param>
    ''' <param name="value" >上传的开关量的值0 or 1</param>
    ''' <param name="control_box_id" >主控箱编号</param>
    ''' <remarks></remarks>
    Public Sub set_huilu_actualstate(ByVal kaiguan_tag As Integer, ByVal value As Integer, ByVal control_box_id As String, ByVal control_box_name As String)
      
        If value = 0 Then
            set_huiluinf(control_box_name, kaiguan_tag, "吸合")
        Else
            set_huiluinf(control_box_name, kaiguan_tag, "断开")

        End If
    End Sub

    ''' <summary>
    ''' 根据上传的开关量判断是否报警
    ''' </summary>
    ''' <param name="kaiguan_tag" >开关量1-16</param>
    ''' <param name="value" >上传的开关量的值0 or 1</param>
    ''' <param name="control_box_name" >主控箱名称</param>
    ''' <returns>""为不报警，不为""则报警，返回的是报警内容</returns>
    ''' <remarks></remarks>
    Public Function alarm_yes_no(ByVal kaiguan_tag As Integer, ByVal value As Integer, ByVal control_box_name As String) As String
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim alarmstring As String '故障的内容
        Dim lampid As String '接触器编号

        msg = ""
        alarm_yes_no = ""
        sql = "select * from alarm_typelist where control_box_name='" & control_box_name & "' and kaiguan_tag='" & kaiguan_tag & "' and " _
        & " alarm_tag='" & value & "' and check_tag=1"
        If DBOperation.OpenConn(conn) = False Then
            alarm_yes_no = ""
            Exit Function
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Alarm_YorN" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            alarm_yes_no = ""
            Exit Function
        End If


        If rs.RecordCount > 0 Then
            While rs.EOF = False
                '如果判断的是接触器辅助触点则要将上传的数据和实际的接触器状态进行对比，对比一致不报警，不一致则报警
                '即该断没断开，该闭合没闭合成功
                alarmstring = Trim(rs.Fields("alarm_type").Value)
                If alarmstring.Substring(0, 1) = "K" Then
                    lampid = Com_inf.Get_Number(alarmstring)
                    While lampid.Length < LAMP_ID_LEN
                        lampid = "0" & lampid
                    End While

                    lampid = "31" & lampid  '通过ID获取接触器节点编号
                    If Check_jiechuqi_alarm(control_box_name, value, lampid) = True Then
                        alarm_yes_no &= Trim(rs.Fields("alarm_type").Value) & " "
                        '在某个电控箱下的接触器有故障，则把huilu_inf表中的open_close置为相应的上传状态
                        'set_huiluinf(control_box_name, Val(Mid(lampid, 3, LAMP_ID_LEN)), alarmstring.Split("_")(1))
                        If value = 0 Then
                            set_huiluinf(control_box_name, Val(Mid(lampid, 3, LAMP_ID_LEN)), "吸合")
                        Else
                            set_huiluinf(control_box_name, Val(Mid(lampid, 3, LAMP_ID_LEN)), "断开")

                        End If
                    Else
                        alarm_yes_no &= ""
                        If value = 0 Then
                            set_huiluinf(control_box_name, Val(Mid(lampid, 3, LAMP_ID_LEN)), "吸合")
                        Else
                            set_huiluinf(control_box_name, Val(Mid(lampid, 3, LAMP_ID_LEN)), "断开")

                        End If

                    End If
                Else
                    '非接触器辅助触点，则按照设置的进行报警判断
                    alarm_yes_no &= Trim(rs.Fields("alarm_type").Value) & " "

                End If



                rs.MoveNext()
            End While


        Else
            alarm_yes_no = ""
        End If

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Function



    Private Sub set_huiluinf(ByVal control_box_name As String, ByVal jiechuqi_id As Integer, ByVal state As String)

        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String

        msg = ""
        sql = "update huilu_inf set open_close='" & state & "' where control_box_name='" & control_box_name & "' and jiechuqi_id=" & jiechuqi_id
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        DBOperation.ExecuteSQL(conn, sql, msg)


        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    ''' 根据回路id 获取所对应的接触器id
    ''' </summary>
    ''' <param name="huilu_id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function get_jiechuqi_id(ByVal huilu_id As Integer, ByVal control_box_name As String) As String
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim jiechuqi_id As String
        msg = ""
        get_jiechuqi_id = ""
        If DBOperation.OpenConn(conn) = False Then
            Exit Function
        End If
        sql = "select jiechuqi_id from huilu_inf where huilu_id='" & huilu_id & "' and control_box_name='" & control_box_name & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Get_jiechuqi_id" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            get_jiechuqi_id = ""
            Exit Function
        End If

        If rs.RecordCount > 0 Then
            jiechuqi_id = Trim(rs.Fields("jiechuqi_id").Value)
            While jiechuqi_id.Length < LAMP_ID_LEN
                jiechuqi_id = "0" & jiechuqi_id
            End While
            jiechuqi_id = "31" & jiechuqi_id
            get_jiechuqi_id = jiechuqi_id
        End If

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Function

    ''' <summary>
    ''' 对比接触器和辅助触点的关系，判断是否需要报警
    ''' </summary>
    ''' <param name="control_box_name"></param>
    ''' <param name="state"></param>
    ''' <param name="lamp_id"></param>
    ''' <returns>true报警，false不报警</returns>
    ''' <remarks></remarks>
    Private Function Check_jiechuqi_alarm(ByVal control_box_name As String, ByVal state As Integer, ByVal lamp_id As String) As Boolean
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim id_len As Integer
        id_len = 2 + LAMP_ID_LEN

        msg = ""
        Check_jiechuqi_alarm = False
        sql = "select state,lamp_id from lamp_street where control_box_name='" & control_box_name & "' and substring(lamp_id,5," & id_len & ")='" & lamp_id & "'"
        If DBOperation.OpenConn(conn) = False Then
            Exit Function
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Check_jiechuqi_alarm" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
        End If

        If rs.RecordCount > 0 Then
            If rs.Fields("state").Value = state Or rs.Fields("state").Value = state + 3 Then
                If state = 0 Then
                    'K未断开
                    sql = "update lamp_inf set result=2 where lamp_id='" & rs.Fields("lamp_id").Value & "'"
                    DBOperation.SelectSQL(conn, sql, msg)

                Else
                    'K未闭合
                    sql = "update lamp_inf set result=1 where lamp_id='" & rs.Fields("lamp_id").Value & "'"
                    DBOperation.SelectSQL(conn, sql, msg)
                End If


                Check_jiechuqi_alarm = True
            Else

                sql = "update lamp_inf set result=0 where lamp_id='" & rs.Fields("lamp_id").Value & "'"
                DBOperation.SelectSQL(conn, sql, msg)

                Check_jiechuqi_alarm = False
            End If
        End If

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing

    End Function

    Public Function Get_kaiguanreturnvalue(ByVal boxid_hex As String, ByVal time As DateTime) As Boolean
        Dim rs, rs_box, rs_alarm, rs_record As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim alarm_list(2) As String '存放开关量报警的3位长度的字符串
        Dim control_box_id As String '电控箱编号
        Dim control_box_name As String '电控箱名称
        Dim i As Integer
        Dim alarmstring As String '报警字符串
        Dim alarm_tag As Boolean = False
        Dim alarminf() As String '故障列表
        Dim controlboxobj As New control_box
        Dim j As Integer = 0

        msg = ""
        sql = "select * from RoadLightStatus where PackType='" & HG_TYPE.HG_GET_KAIGUAN & "'" _
        & " and HandlerFlag=3 and createtime>'" & Now().AddMinutes(-10) & "' and StatusContent like'" & boxid_hex & "%'"

        If DBOperation.OpenConn(conn) = False Then
            Exit Function
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)

        If rs Is Nothing Then
            conn.Close()
            conn = Nothing
            Exit Function
        End If

        While rs.EOF = False

            'alarm_list = Trim(rs.Fields("StatusContent").Value).Split(" ")
            controlboxobj.get_kaiguanString(Trim(rs.Fields("StatusContent").Value), alarm_list)
            control_box_id = alarm_list(0)  '两个字节的电控箱编号

            '2011年4月20日增加电控箱的用电类型：电池或交流电
            sql = "select control_box_name,power_type,kaiguan_string,Createtime from control_box where control_box_id='" & control_box_id & "'"
            rs_box = DBOperation.SelectSQL(conn, sql, msg)
            If rs_box Is Nothing Then
                If rs.State = 1 Then
                    rs.Close()
                    rs = Nothing
                End If
                conn.Close()
                conn = Nothing
                Exit Function
            End If
            If rs_box.RecordCount > 0 Then

                control_box_name = Trim(rs_box.Fields("control_box_name").Value)
                i = 0
                While i < alarm_list(1).Length
                    alarmstring = controlboxobj.alarm_yes_no(i + 1, Mid(alarm_list(1), 16 - i, 1), control_box_name)
                    If alarmstring <> "" Then  '报警

                        '有报警
                        alarminf = Trim(alarmstring).Split(" ")
                        j = 0
                        While j < alarminf.Length
                            sql = "select * from kaiguan_alarm_list where control_box_name='" & control_box_name & "' and alarm_string='" & alarminf(j) & "' and (alarm_tag=0 or alarm_tag=2)"
                            rs_record = DBOperation.SelectSQL(conn, sql, msg)
                            If rs_record Is Nothing Then
                                g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Check_Kaiguan_Alarm" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

                                GoTo finish
                            End If
                            If rs_record.RecordCount <= 0 Then
                                alarm_tag = True
                                '原来没有报警数据
                                sql = "insert into kaiguan_alarm_list(alarm_string,createtime,alarm_tag," _
                              & "control_box_name,kaiguan_tag) values('" & alarminf(j) & "','" & Now & "'," _
                              & "0,'" & control_box_name & "' ,'" & i + 1 & "')"
                                DBOperation.ExecuteSQL(conn, sql, msg)
                            Else
                                '有原来有报警信息，则将原来的报警信息置为2，表示经过确认的
                                alarm_tag = True
                                sql = "update kaiguan_alarm_list set alarm_tag=2 where id='" & rs_record.Fields("id").Value & "'"
                                DBOperation.ExecuteSQL(conn, sql, msg)

                            End If
                            j += 1

                        End While


                    Else  '状态是正常的，则查询当前的报警表，有报警信息则将报警信息置1
                        sql = "select * from kaiguan_alarm_list where control_box_name='" & control_box_name & "' and kaiguan_tag='" & i + 1 & "' and (alarm_tag=0 or alarm_tag=2)"
                        rs_record = DBOperation.SelectSQL(conn, sql, msg)
                        If rs_record Is Nothing Then
                            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Check_Kaiguan_Alarm" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

                            GoTo finish
                        End If
                        While rs_record.EOF = False
                            alarm_tag = True
                            '原来有报警数据，则将报警标志置为2，表示报警结束
                            'rs_record.Fields("alarm_tag").Value = 1
                            'rs_record.Fields("endtime").Value = Now
                            'rs_record.Update()
                            sql = "update kaiguan_alarm_list set alarm_tag=1,endtime='" & Now & "' where id='" & rs_record.Fields("id").Value & "'"
                            DBOperation.ExecuteSQL(conn, sql, msg)

                            rs_record.MoveNext()
                        End While


                    End If
                    i += 1  '同一个主控箱中的16位开关量报警数据
                End While

                '更新主控箱的供电类型
                If alarm_list(2) = 1 Then
                    ' rs_box.Fields("power_type").Value = POWERTYPE_BUTTERY

                    sql = "update control_box set power_type='" & POWERTYPE_BUTTERY & "',kaiguan_string='" & alarm_list(1) & "', Createtime='" & Now & "'  where control_box_name='" & control_box_name & "'"
                Else
                    '  rs_box.Fields("power_type").Value = POWERTYPE_CURRENT
                    sql = "update control_box set power_type='" & POWERTYPE_CURRENT & "',kaiguan_string='" & alarm_list(1) & "', Createtime='" & Now & "' where control_box_name='" & control_box_name & "'"
                End If


                DBOperation.ExecuteSQL(conn, sql, msg)
                'rs_box.Fields("kaiguan_string").Value = alarm_list(1)
                'rs_box.Fields("Createtime").Value = Now
                'rs_box.Update()



                '记录供电情况
                If alarm_list(2) = 1 Then
                    Setcontrolbox_Record(control_box_name, POWERTYPE_BUTTERY, "供电")
                Else
                    Setcontrolbox_Record(control_box_name, POWERTYPE_CURRENT, "供电")
                End If
                '失压报警刷新
                g_welcomewinobj.BackgroundWorker_find_state.ReportProgress(2)
            End If




            '将本条记录置为1
            sql = "update RoadLightStatus set handlerflag=1 where id='" & rs.Fields("id").Value & "'"
            DBOperation.ExecuteSQL(conn, sql, msg)
            rs.MoveNext()

        End While

        If alarm_tag = True Then
            g_welcomewinobj.BackgroundWorker_find_state.ReportProgress(3)
        End If

finish:
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If

        If rs_box.State = 1 Then
            rs_box.Close()
            rs_box = Nothing
        End If
        If rs_alarm.State = 1 Then
            rs_alarm.Close()
            rs_alarm = Nothing
        End If
        If rs_record.State = 1 Then
            rs_record.Close()
            rs_record = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Function

    ''' <summary>
    ''' 查找电控箱的通信是否正常
    ''' </summary>
    ''' <param name="control_box_name"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function get_communication(ByVal control_box_name As String) As Boolean
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim state_name As String = "通信"
        Dim state As String = "未连接"

        msg = ""
        sql = "select * from control_box_state where control_box_name='" & control_box_name _
        & "' and kaiguan_string='" & state_name & "' and StatusContent='" & state & "' and state=0"
        If DBOperation.OpenConn(conn) = False Then
            Exit Function
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        get_communication = True
        If rs Is Nothing Then
            get_communication = False
            conn.Close()
            conn = Nothing
            Exit Function
        End If
        If rs.RecordCount > 0 Then
            get_communication = False

        End If

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Function

    ''' <summary>
    ''' 设置路灯的电流报警上下限下放字符串
    ''' </summary>
    ''' <param name="control_box_name"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function set_config_box_state(ByVal control_box_name As String, ByVal board_num As Integer, ByVal control_box_id As String, ByVal box_type As String) As String
        Dim config_state(board_num) As m_currentalarm
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim i As Integer = 0
        Dim huilu_id As Integer '回路的编号
        Dim board_id As Integer = 0
        Dim current_bottom As Integer
        Dim current_top As Integer
        Dim j As Integer = 0
        Dim mask_str, bottom_str, top_str As String
        Dim config_string As String = ""

        msg = ""
        sql = "select * from huilu_inf where control_box_name='" & control_box_name & "' order by huilu_id"
        DBOperation.OpenConn(conn)

        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            conn.Close()
            conn = Nothing
            set_config_box_state = ""
            Exit Function
        End If

        While board_id < board_num
            ReDim config_state(board_id).use_mask(11)
            ReDim config_state(board_id).current_bottom(11)
            ReDim config_state(board_id).current_top(11)
            i = 0
            While i < 12
                If rs.EOF = False Then
                    huilu_id = rs.Fields("huilu_id").Value - 1   '回路编号
                    current_bottom = rs.Fields("current_alarmbot").Value

                    current_top = rs.Fields("current_alarmtop").Value


                    If i + (board_id * 12) = huilu_id Then
                        '设置了该回路，将掩码值置为1
                        config_state(board_id).use_mask(i) = 1
                        config_state(board_id).current_bottom(i) = current_bottom
                        config_state(board_id).current_top(i) = current_top

                        i += 1
                        rs.MoveNext()

                    Else
                        '没有设置该回路，将回路的掩码置为0 ，其他报警值默认为0
                        config_state(board_id).use_mask(i) = 0
                        config_state(board_id).current_bottom(i) = 0
                        config_state(board_id).current_top(i) = 0
                        i += 1
                    End If

                Else
                    '没有设置该回路，将回路的掩码置为0 ，其他报警值默认为0
                    config_state(board_id).use_mask(i) = 0
                    config_state(board_id).current_bottom(i) = 0
                    config_state(board_id).current_top(i) = 0
                    i += 1

                End If


            End While

            board_id += 1

        End While
        i = 0
        mask_str = ""
        bottom_str = ""
        top_str = ""
        board_id = 0
        While board_id < board_num
            i = 0
            mask_str = ""
            '第一个字节前六位管前六路
            While i < 6
                mask_str = config_state(board_id).use_mask(i) & mask_str
                i += 1
            End While
            While mask_str.Length < 8
                mask_str = "0" & mask_str
            End While
            '第二个字节前六位管后六路
            While i < 12
                mask_str = config_state(board_id).use_mask(i) & mask_str
                i += 1
            End While
            While mask_str.Length < 16
                mask_str = "0" & mask_str
            End While
            mask_str = Com_inf.BIN_to_HEX(mask_str)
            mask_str = Mid(mask_str, 3, 2) & " " & Mid(mask_str, 1, 2)
            config_string &= mask_str & " "
            '12位上限
            i = 0
            While i < 12
                top_str = config_state(board_id).current_top(i)
                '逆向公式转换
                top_str = Me.Get_EXECurrent(top_str, control_box_id, board_id * 12 + i + 1, box_type)
                '电流上限
                top_str = Com_inf.Dec_to_Hex(top_str, 4)
                top_str = Mid(top_str, 1, 2) & " " & Mid(top_str, 3, 2)

                config_string &= top_str & " "

                i += 1
            End While
            '12位下限
            i = 0
            While i < 12
                bottom_str = config_state(board_id).current_bottom(i)
                '逆向公式转换
                bottom_str = Me.Get_EXECurrent(bottom_str, control_box_id, board_id * 12 + i + 1, box_type)

                '电流下限
                bottom_str = Com_inf.Dec_to_Hex(bottom_str, 4)
                bottom_str = Mid(bottom_str, 1, 2) & " " & Mid(bottom_str, 3, 2)


                config_string &= bottom_str & " "

                i += 1
            End While


            board_id += 1
        End While

        set_config_box_state = config_string

    End Function


End Class
