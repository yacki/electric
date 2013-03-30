'��������Ĳ�������
Public Class control_box

    Public m_currenttopvalue, m_currentbottomvalue, m_presuretopvalue, m_presurebottomvalue, m_bianbivalue As Integer  '������ѹ����ֵ
    Private m_controlproblemnum As Integer = 0   '������Ĺ��ϴ���
    Public m_huilu() As String = {"A1", "A2", "A3", "A4", "B1", "B2", "B3", "B4", "C1", "C2", "C3", "C4"}  '���ڻ�·����ת��
    Public m_huilu2() As String = {"A5", "A6", "A7", "A8", "B5", "B6", "B7", "B8", "C5", "C6", "C7", "C8"}  '���ڻ�·����ת��
    Public m_huilu3() As String = {"A9", "A10", "A11", "A12", "B9", "B10", "B11", "B12", "C9", "C10", "C11", "C12"}  '���ڻ�·����ת��

    Public m_huilu_small() As String = {"A1", "A2", "B1", "B2", "C1", "C2"}  '���ڻ�·����ת��
    Public m_huilu2_small() As String = {"A3", "A4", "B3", "B4", "C3", "C4"}  '���ڻ�·����ת��
    Public m_huilu3_small() As String = {"A5", "A6", "B5", "B6", "C5", "C6"}  '���ڻ�·����ת��



    Private Structure m_currentalarm  '�·Ÿ�������ĵ�����ѹ������������ı�����ֵ
        Dim use_mask() As Integer  '���룬���û�·Ϊ1�����û�·Ϊ0
        Dim current_top() As Integer  '������������
        Dim current_bottom() As Integer  '����������

    End Structure

   

    ''' <summary>
    ''' ���õ�����ѹ��������ֵ
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Get_TopBottom_value()
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String

        msg = ""
        '��ȡ��ѹ����
        sql = "select * from sysconfig where type='��ѹ����'"
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Get_TopBottom_value" & " ʱ�䣺" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs.RecordCount > 0 Then
            m_presuretopvalue = Val(Trim(rs.Fields("name").Value))
        Else
            m_presuretopvalue = 220
        End If

        '���õ�ѹ����
        sql = "select * from sysconfig where type='��ѹ����'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Get_TopBottom_value" & " ʱ�䣺" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

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
    '    ''' ����������������е���Ϣ
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
    '            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "set_controlbox_list" & " ʱ�䣺" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

    '            GoTo finish
    '        End If
    '        While rs_city.EOF = False
    '            city_string = Trim(rs_city.Fields("city_name").Value)
    '            box_list.Nodes.Add(city_string & " (����" & rs_city.Fields("box_num").Value.ToString & "��������)")

    '            '��������
    '            sql = "select distinct(area) as area_name,count(distinct(control_box_id)) as box_num from control_inf where city='" & city_string & "' group by area"
    '            rs_area = DBOperation.SelectSQL(conn, sql, msg)
    '            If rs_area Is Nothing Then
    '                g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "set_controlbox_list" & " ʱ�䣺" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

    '                GoTo finish
    '            End If
    '            While rs_area.EOF = False
    '                area_string = Trim(rs_area.Fields("area_name").Value)
    '                box_list.Nodes(i1).Nodes.Add(area_string & " (����" & rs_area.Fields("box_num").Value.ToString & "��������)")
    '                '�ֵ�����
    '                sql = "select distinct(street) as street_name,street_id,count(distinct(control_box_id)) as box_num from control_inf where city='" & city_string & "' and area='" & area_string & "' group by street, street_id order by street_id"
    '                rs_street = DBOperation.SelectSQL(conn, sql, msg)
    '                If rs_street Is Nothing Then
    '                    g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "set_controlbox_list" & " ʱ�䣺" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

    '                    GoTo finish
    '                End If
    '                While rs_street.EOF = False
    '                    street_string = Trim(rs_street.Fields("street_name").Value)
    '                    box_list.Nodes(i1).Nodes(i2).Nodes.Add(street_string & " (����" & rs_street.Fields("box_num").Value.ToString & "��������)")
    '                    '���������
    '                    sql = "select distinct(control_box_name) as box_name,control_box_id from control_inf where city='" & city_string & "' and area='" & area_string & "' and street='" & street_string & "' order by control_box_id"
    '                    rs_box = DBOperation.SelectSQL(conn, sql, msg)
    '                    If rs_box Is Nothing Then
    '                        g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "set_controlbox_list" & " ʱ�䣺" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

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
    ''' ����������������е���Ϣ(������������)
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
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "set_controlbox_list" & " ʱ�䣺" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            GoTo finish
        End If
        While rs_city.EOF = False
            city_string = Trim(rs_city.Fields("city_name").Value)
            box_list.Nodes.Add(city_string & " (����" & rs_city.Fields("box_num").Value.ToString & "������)")

            '��������
            sql = "select distinct(area) as area_name,count(distinct(control_box_id)) as box_num from control_inf where city='" & city_string & "' group by area"
            rs_area = DBOperation.SelectSQL(conn, sql, msg)
            If rs_area Is Nothing Then
                g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "set_controlbox_list" & " ʱ�䣺" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

                GoTo finish
            End If
            While rs_area.EOF = False
                area_string = Trim(rs_area.Fields("area_name").Value)
                box_list.Nodes(i1).Nodes.Add(area_string & " (����" & rs_area.Fields("box_num").Value.ToString & "������!)")
                '�ֵ�����
                sql = "select distinct(street) as street_name,street_id,count(distinct(control_box_id)) as box_num from control_inf where city='" & city_string & "' and area='" & area_string & "' group by street, street_id order by street_id"
                rs_street = DBOperation.SelectSQL(conn, sql, msg)
                If rs_street Is Nothing Then
                    g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "set_controlbox_list" & " ʱ�䣺" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

                    GoTo finish
                End If
                While rs_street.EOF = False
                    street_string = Trim(rs_street.Fields("street_name").Value)
                    box_list.Nodes(i1).Nodes(i2).Nodes.Add(street_string & " (����" & rs_street.Fields("box_num").Value.ToString & "������)")
                    '���������
                    sql = "select distinct(control_box_name) as box_name,control_box_id from control_inf where city='" & city_string & "' and area='" & area_string & "' and street='" & street_string & "' order by control_box_id"
                    rs_box = DBOperation.SelectSQL(conn, sql, msg)
                    If rs_box Is Nothing Then
                        g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "set_controlbox_list" & " ʱ�䣺" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

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
    ''' ��ȡ�����Ĳɼ�����ABCABCABCABC
    ''' </summary>
    ''' <param name="box_tag"></param>
    ''' <param name="board_num" >���������</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Get_controlbox_tipABC(ByVal box_tag As String, ByVal board_num As Integer) As String
        Dim rs As New ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim lamp_tip As String
        Dim control_lamp_obj As New control_lamp
        Dim conn As New ADODB.Connection
        Dim state() As String   '�������״̬
        Dim i As Integer
        Dim j As Integer = 1
        Dim huilu_num As Integer  '��·������
        Dim board_id As Integer = 0 '��������
        Dim state_string As String '�����������״̬
        Dim box_type As Integer '�����������
        Dim problem_string As String '����
        Dim control_box_name As String  '���������

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
            If problem_string <> "����" Then
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
            lamp_tip = "�����䣺" & control_box_name & " (" & problem_string & ")"
            lamp_tip &= "  ʱ�䣺" & rs.Fields("Createtime").Value & vbCrLf
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

                lamp_tip &= "A���ѹ��" & state(0) & _
                           vbCrLf & "B���ѹ��" & state(1) & _
                           vbCrLf & "C���ѹ��" & state(2) & vbCrLf
                i = 3
                j = 0
                huilu_num = (state.Length - 3) / 3  '��·��Ŀ

                ''ԭ����state˳����AAAABBBBCCCC��2012��5��3�ո�ΪABCABCABCABC,
                'change_state(state)
                Dim presure_type As Integer
                While j < huilu_num
                    'lamp_tip &= "��" & j & "��·  ����(A)��" & state(i) & "    "
                    'ԭ������Ĭ�ϵ�AABBCC˳���������������2012��5��3�ո���Ϊ�����û����趨����������
                    '���������Ĭ�ϵ�ABCABC()
                    presure_type = get_presuretype(j + 1 + 12 * board_id, control_box_name)

                    If presure_type = 0 Then
                        lamp_tip &= String.Format("��{0,-3}��·(A) ����(A)��{1,-10} ", j + 1 + 12 * board_id, state(i))
                    Else
                        If presure_type = 1 Then
                            lamp_tip &= String.Format("��{0,-3}��·(B) ����(A)��{1,-10} ", j + 1 + 12 * board_id, state(i))
                        Else
                            lamp_tip &= String.Format("��{0,-3}��·(C) ����(A)��{1,-10} ", j + 1 + 12 * board_id, state(i))

                        End If
                    End If


                    i += 1
                    If box_type = 1 Then
                        lamp_tip &= String.Format("����(KW)��{0,-8}", Format(state(i) / 1000, "0.000"))
                    Else
                        lamp_tip &= String.Format("����(KW)��{0,-8}", state(i))
                    End If

                    i += 1
                    lamp_tip &= "����������" & state(i) & vbCrLf
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
    ''' ��ȡ�����Ĳɼ�����AAAABBBBCCCC
    ''' </summary>
    ''' <param name="box_tag"></param>
    ''' <param name="board_num" >���������</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Get_controlbox_tip(ByVal box_tag As String, ByVal board_num As Integer) As String
        Dim rs As New ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim lamp_tip As String
        Dim control_lamp_obj As New control_lamp
        Dim conn As New ADODB.Connection
        Dim state() As String   '�������״̬
        Dim i As Integer
        Dim j As Integer = 1
        Dim huilu_num As Integer  '��·������
        Dim board_id As Integer = 0 '��������
        Dim state_string As String '�����������״̬
        Dim box_type As Integer '�����������
        Dim problem_string As String '����

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
            If problem_string <> "����" Then
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
            lamp_tip = "�����䣺" & Trim(rs.Fields("control_box_name").Value) & " (" & problem_string & ")"
            lamp_tip &= "  ʱ�䣺" & rs.Fields("Createtime").Value & vbCrLf
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

                lamp_tip &= "A���ѹ��" & state(0) & _
                           vbCrLf & "B���ѹ��" & state(1) & _
                           vbCrLf & "C���ѹ��" & state(2) & vbCrLf
                i = 3
                j = 0
                huilu_num = (state.Length - 3) / 3  '��·��Ŀ

                While j < huilu_num
                    'lamp_tip &= "��" & j & "��·  ����(A)��" & state(i) & "    "
                    If huilu_num = 12 Then  '12·���ݰ�
                        If board_id = 0 Then
                            lamp_tip &= String.Format("��{0,-3}��· ����(A)��{1,-10} ", m_huilu(j), state(i))
                        Else
                            If board_id = 1 Then
                                lamp_tip &= String.Format("��{0,-3}��· ����(A)��{1,-10} ", m_huilu2(j), state(i))
                            Else
                                lamp_tip &= String.Format("��{0,-3}��· ����(A)��{1,-10} ", m_huilu3(j), state(i))


                            End If
                        End If
                    Else
                        If board_id = 0 Then
                            lamp_tip &= String.Format("��{0,-3}��· ����(A)��{1,-8} ", m_huilu_small(j), state(i))
                        Else
                            If board_id = 1 Then
                                lamp_tip &= String.Format("��{0,-3}��· ����(A)��{1,-8} ", m_huilu2_small(j), state(i))
                            Else
                                lamp_tip &= String.Format("��{0,-3}��· ����(A)��{1,-8} ", m_huilu3_small(j), state(i))


                            End If
                        End If
                    End If

                    i += 1
                    If box_type = 1 Then
                        lamp_tip &= String.Format("����(KW)��{0,-8}", Format(state(i) / 1000, "0.000"))
                    Else
                        lamp_tip &= String.Format("����(KW)��{0,-8}", state(i))
                    End If

                    i += 1
                    lamp_tip &= "����������" & state(i) & vbCrLf
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
    ''' ���Ƶ����
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
        rs = DBOperation.SelectSQL(conn, sql, msg)  '�õ�ͼ�ϵ�������
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "get_box_information" & " ʱ�䣺" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        While rs.EOF = False
            problem_string = ""
            board_num = rs.Fields("board_num").Value   '���������
            problem_string = Trim(Trim(rs.Fields("state").Value))
          
           
            If problem_string = "����" Then   '����

                '��ң�����������ٴ��жϿ������Ƿ�����
                kaiguan_alarm = get_kaiguanalarm(Trim(rs.Fields("control_box_name").Value))
                If kaiguan_alarm = True Then  '����
                    g_lampmap.DrawRectangle(myPen, rs.Fields("pos_x").Value, rs.Fields("pos_y").Value, 20, 20)
                    g_lampmap.FillRectangle(myBrush2, rs.Fields("pos_x").Value, rs.Fields("pos_y").Value, 20, 20)
                Else
                    g_lampmap.DrawRectangle(myPen, rs.Fields("pos_x").Value, rs.Fields("pos_y").Value, 20, 20)
                    g_lampmap.FillRectangle(myBrush, rs.Fields("pos_x").Value, rs.Fields("pos_y").Value, 20, 20)
                End If


            Else  '����
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
    ''' ����������ı�Ų�ѯ�����������
    ''' </summary>
    ''' <param name="control_box_id"></param>
    ''' <remarks></remarks>
    Public Function get_controltype(ByVal control_box_id As String) As Integer
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String
        get_controltype = 2  'Ĭ���Ǵ���ң
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
    ''' Ѱ���Ƿ��е���������������Ϣ
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub find_auto_alarm()
        Dim rs_find_state As New ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim conn As New ADODB.Connection
        Dim control_box_id As String '�������
        Dim recData As String '���յ�����
        'Dim rs As New ADODB.Recordset
        Dim status_list(80) As String '���״̬
        Dim i As Integer = 0
        Dim problem_tag As String   '��־��·��״̬
        Dim rs_box As New ADODB.Recordset
        Dim group_id As Integer = 0
        Dim boxtype As Integer '���ִ�С��ң������ң����2��С��ң����3

        Dim datainf As Boolean = True '��ң���ݵĺϷ���
        Dim stateflag As Integer '�ϴ�״̬��handlerflag���������ִ�С��ң

        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If

        msg = ""

        '��ѯ10����֮���Ƿ���û��������������,�����ϴ�������HandlerFlag��־Ϊ10
        sql = "select * from RoadLightStatus nolock where Createtime > DateAdd(n,-10,'" & Now() & "') and (PackType='" & HG_TYPE.HG_HOST_ALARMAUTO & "' or PackType='" & HG_TYPE.HG_HOST_SMALL_ALARMAUTO & "') and HandlerFlag=3 order by ID"  'û�з���״̬

        rs_find_state = DBOperation.SelectSQL(conn, sql, msg)
        If rs_find_state Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "find_box_state_fun1" & " ʱ�䣺" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs_find_state.RecordCount <= 0 Then  'û�������򷵻�
            GoTo finish
        End If

        '����ȡ�����ݰ���������Ϣ���з���
        While rs_find_state.EOF = False

            problem_tag = ""
            recData = Trim(rs_find_state.Fields("statusContent").Value)
            status_list = recData.Split(" ")
            stateflag = rs_find_state.Fields("PackType").Value   '�ϴ�״̬�Ĵ�С��ң

            '�ж������Ƿ�Ϊ��ʱ���ݣ��������Ϊ��ʱ����������ж�����������
            datainf = CheckData(status_list, rs_find_state.Fields("ID").Value)
            If datainf = False Then
                '֤���ϴ�������Ϊ��ʱ���ݣ���������
                GoTo next1
            End If
            control_box_id = System.Convert.ToInt32(status_list(0) & status_list(1), 16)  'ǰ�����ֽ���Ϊ�������
            While control_box_id.Length < 4
                control_box_id = "0" & control_box_id
            End While

            '�����������Ų�ѯ����������ͣ����ִ�С��ң
            boxtype = get_controltype(control_box_id)

            If ((boxtype = 3 Or boxtype = 5) And stateflag <> 38) Or ((boxtype = 2 Or boxtype = 4) And stateflag <> 36) Then
                '�ϴ�����ң���������õ�������״̬��һ��
                g_welcomewinobj.SetTextDelegate(control_box_id & "��������İ汾���ϴ�״̬��ƥ�� ʱ�䣺" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, True, g_welcomewinobj.rtb_info_list)
                GoTo next1
            End If

            '3�����ݣ���ң����ģʽ���в������ģʽһ�£�������������������������һ������2011��9��22�գ�
            Dim recstring As String
            Dim data() As String
            'Dim controlboxobj As New control_box
            Dim group_num As Integer = 0  '���
            Dim j As Integer = 0
            Dim sanyao_data(56) As String

            recstring = Trim(rs_find_state.Fields("StatusContent").Value)
            data = recstring.Split(" ")

            If boxtype = 3 Then
                '����ȡ������ΪС��ң���ݣ�,������ʾAABBCC���������е������ֽڵ�������1��Ϊ��ţ�����ͨ�õ���ң���ݷ���
                data(2) = (Val(data(2).ToString) - 1).ToString
                If data(2) = -1 Then
                    '��ʾ����ʧ��,����״̬��Ϊ1
                    rs_find_state.Fields("handlerflag").Value = 1
                    rs_find_state.Update()

                    GoTo next1
                End If

                Get_Huilu_inf_small(Val(data(2)), data, problem_tag, control_box_id, rs_find_state.Fields("ID").Value)
            End If

            If boxtype = 2 Then
                '��ͳ����ң����,������ʾAAAABBBBCCCC
                group_num = Val(data(2))
                If group_num = 0 Then
                    '��ʾ����ʧ��,����״̬��Ϊ1
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


                '��ȡ������Ϣ
                get_alarminf(data, 3 + group_num * 54, rs_find_state.Fields("ID").Value, control_box_id, group_num)

            End If

            If boxtype = 5 Then
                '����ȡ������ΪС��ң���ݣ�,������ʾABCABC���������е������ֽڵ�������1��Ϊ��ţ�����ͨ�õ���ң���ݷ���
                data(2) = (Val(data(2).ToString) - 1).ToString
                If data(2) = -1 Then
                    '��ʾ����ʧ��,����״̬��Ϊ1
                    rs_find_state.Fields("handlerflag").Value = 1
                    rs_find_state.Update()

                    GoTo next1
                End If

                Get_Huilu_inf_smallABC(Val(data(2)), data, problem_tag, control_box_id, rs_find_state.Fields("ID").Value)
            End If

            If boxtype = 4 Then
                '��ͳ����ң����,������ʾABCABCABCABC
                group_num = Val(data(2))
                If group_num = 0 Then
                    '��ʾ����ʧ��,����״̬��Ϊ1
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


                '��ȡ������Ϣ
                get_alarminf(data, 3 + group_num * 54, rs_find_state.Fields("ID").Value, control_box_id, group_num)

            End If
            '��¼��������
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
        '��handlerflag��Ϊ1
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If

        '���������ֽڵĵ�ѹ������Ϣ
        Dim power As String
        Dim i As Integer = 0

        power = System.Convert.ToInt32(data(start_id), 16).ToString

        If power <> 0 And power <> 4 Then
            problem_tag &= "A�ࣺ" & get_alarminf("power_" & power, 0, control_box_id) & " "

        End If
        If power = 4 And chaoshi_tag = 0 Then
            problem_tag &= "���������ݲɼ���ʱ"
            chaoshi_tag += 1
        End If
        power = System.Convert.ToInt32(data(start_id + 1), 16)
        If power <> 0 And power <> 4 Then
            problem_tag &= "B�ࣺ" & get_alarminf("power_" & power, 0, control_box_id) & " "

        End If
        If power = 4 And chaoshi_tag = 0 Then
            problem_tag &= "���������ݲɼ���ʱ"
            chaoshi_tag += 1
        End If
        power = System.Convert.ToInt32(data(start_id + 2), 16)
        If power <> 0 And power <> 4 Then
            problem_tag &= "C�ࣺ" & get_alarminf("power_" & power, 0, control_box_id) & " "

        End If
        If power = 4 And chaoshi_tag = 0 Then
            problem_tag &= "���������ݲɼ���ʱ"
            chaoshi_tag += 1
        End If


        If data.Length > 7 + group_num * 54 Then
            '�жϵ���������Ϣ
            Dim huilu_num As Integer '��·������
            Dim huilu_id As Integer '��·�ı��
            Dim alarm_tag As Integer '������־
            huilu_num = System.Convert.ToInt32(data(start_id + 3), 16)
            i = 0
            While i < huilu_num * 2
                huilu_id = System.Convert.ToInt32(data(start_id + 4 + i), 16)
                alarm_tag = System.Convert.ToInt32(data(start_id + 5 + i), 16)
                If alarm_tag <> 0 And alarm_tag <> 9 Then
                    problem_tag &= get_alarminf("current_" & alarm_tag, huilu_id, control_box_id) & "(��·" & huilu_id & ") "
                End If
                If alarm_tag = 9 And chaoshi_tag = 0 Then
                    chaoshi_tag += 1
                    problem_tag &= get_alarminf("current_" & alarm_tag, huilu_id, control_box_id)

                End If
                i += 2
            End While

        End If

        If Trim(problem_tag) = "" Then
            problem_tag = "����"
        End If

        '��״̬��1
        sql = "update RoadLightStatus set HandlerFlag=1 where ID ='" & ID & "'"
        DBOperation.ExecuteSQL(conn, sql, msg)


        '��ȡ��ǰ�����Ĺ��ϴ���
        sql = "select * from control_box where control_box_id='" & control_box_id & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "find_box_state_fun3" & " ʱ�䣺" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

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

            '����ǰ�������������ݴ����ӵ�control_box����
            If problem_tag <> "����" Then
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
        '����control_box_state�е�״̬��¼
        Dim state_type As String = "״̬"
        state_string = ""
        sql = "select state, StatusContent,StatusContent2,StatusContent3 from control_box where control_box_id='" & control_box_id & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "find_box_state_fun2" & " ʱ�䣺" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            Exit Sub

        End If
        If rs.RecordCount > 0 Then
            state_string = Trim(rs.Fields("state").Value)
            '����control_box_state�е�״̬��¼
            If state_string <> "" Then
                Setcontrolbox_Record(control_box_name, state_string, "״̬")
            End If

            'Setcontrolbox_Record(control_box_name, problem_tag, "״̬")

            If problem_tag <> "����" And m_controlproblemnum = 1 Then  'ȷ���Ĵι��Ϻ��Ͷ���
                '���Ͷ���
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
    ''' ����·��������ѹ���繦��
    ''' </summary>
    ''' <remarks></remarks>
    ''' <param name="load_state_tag" >load_state_tag=true��¼һ������</param>
    Public Function find_box_state_fun(ByVal load_state_tag As Boolean) As Boolean
        Dim rs_find_state As New ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim conn As New ADODB.Connection
        Dim control_box_id As String '�������
        Dim recData As String '���յ�����
        'Dim rs As New ADODB.Recordset
        Dim status_list(80) As String '���״̬
        Dim i As Integer = 0
        Dim problem_tag As String   '��־��·��״̬
        Dim rs_box As New ADODB.Recordset
        Dim group_id As Integer = 0

        Dim datainf As Boolean = True '��ң���ݵĺϷ���

        If DBOperation.OpenConn(conn) = False Then
            Exit Function
        End If

        msg = ""

        '��ѯ10����֮���Ƿ���û��������������,�����ϴ�������HandlerFlag��־Ϊ0
        ' sql = "select * from RoadLightStatus where Createtime > DateAdd(n,-10,'" & Now() & "') and HandlerFlag=" & 0 & " order by ID"  'û�з���״̬
        sql = "select * from RoadLightStatus nolock where Createtime > DateAdd(n,-10,'" & Now() & "') and HandlerFlag=" & 0 & " order by ID"  'û�з���״̬

        rs_find_state = DBOperation.SelectSQL(conn, sql, msg)
        If rs_find_state Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "find_box_state_fun1" & " ʱ�䣺" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            find_box_state_fun = False '��ʾû������
            Exit Function
        End If
        If rs_find_state.RecordCount <= 0 Then  'û�������򷵻�
            find_box_state_fun = False '��ʾû������
            GoTo finish
        End If
        '*************************������*********************
        '2011��4��11�գ�����ң���ݷ������ֿ��ǣ�һ���ϰ�ģ�û��������ģ�ֻ��·�κź�12·��Ϣ
        '�����°�ģ���������33�����ֽ�·�κ�+1�ֽ���ţ�00��,01,02��+12·��·��Ϣ������57
        '2011��9��5�գ�����С��ң���ݷ�������������33�����ֽ�·�κ�+1�ֽ���ţ�00��,01,02��+6·��·��Ϣ������33

        '����ȡ�����ݰ���������Ϣ���з���
        While rs_find_state.EOF = False
            '2011��11��22�գ�ÿ��ȡ״̬��ʱ�����ж�һ���Ƿ���Ҫ�ȴ��ٲ���Ϣ
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

            problem_tag = "����"
            recData = Trim(rs_find_state.Fields("statusContent").Value)
            status_list = recData.Split(" ")

            '�ж������Ƿ�Ϸ���������ݺϷ�������ж�����������
            datainf = CheckData(status_list, rs_find_state.Fields("ID").Value)
            If datainf = False Then
                '֤���ϴ�������Ϊ��ʱ���ݣ���������
                GoTo next1
            End If
            control_box_id = System.Convert.ToInt32(status_list(0) & status_list(1), 16)  'ǰ�����ֽ���Ϊ�������
            While control_box_id.Length < 4
                control_box_id = "0" & control_box_id
            End While
            If rs_find_state.Fields("PackType").Value Is System.DBNull.Value Then
                '1������
                Me.Get_Huilu_inf(-1, 1, status_list, problem_tag, control_box_id)
            Else
                ''2������
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

                '3�����ݣ���ң����ģʽ���в������ģʽһ�£�������������������������һ������2011��9��22�գ�
                If rs_find_state.Fields("PackType").Value = HG_TYPE.HG_HOST_NEWSAYAO_AUTO Then
                    Dim recstring As String
                    Dim data() As String
                    Dim controlboxobj As New control_box
                    Dim group_num As Integer = 0  '���
                    Dim j As Integer = 0
                    Dim sanyao_data(56) As String

                    recstring = Trim(rs_find_state.Fields("StatusContent").Value)
                    data = recstring.Split(" ")

                    If data.Length = SMALL_DATALENGHT Then
                        '����ȡ������ΪС��ң���ݣ��������е������ֽڵ�������1��Ϊ��ţ�����ͨ�õ���ң���ݷ���
                        data(2) = (Val(data(2).ToString) - 1).ToString

                        controlboxobj.Get_Huilu_inf_small(Val(data(2)), data, problem_tag, control_box_id, rs_find_state.Fields("ID").Value)
                    End If

                    If data.Length = 3 + data(2) * 54 Then
                        '��ͳ��ң����
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
        find_box_state_fun = True  '��ʾ������
       
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
        Dim state1, state2, state3 As String  '����������״̬
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String = ""
        Dim board_num As Integer = 1
        Dim state_type As String = "����"
        state1 = ""
        state2 = ""
        state3 = ""

        '���ϴ��Ļ�·��Ϣ���浽control_box_state����
        If control_box_id = "" Then
            sql = "select * from control_box order by control_box_id"

        Else
            sql = "select * from control_box where control_box_id='" & control_box_id & "'"

        End If
        DBOperation.OpenConn(conn)
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "find_box_state_fun2" & " ʱ�䣺" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

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

            '��¼����
            If board_num = 1 Then
                If rs.Fields("StatusContent").Value IsNot System.DBNull.Value Then

                    If Trim(rs.Fields("StatusContent").Value) <> "" Then
                        state1 = Trim(rs.Fields("StatusContent").Value)
                    Else
                        If Trim(rs.Fields("StatusContent").Value) = "" Then
                            '��ʾ�˿�ͨ�Ų�����
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
                        '�������������ݶ�Ϊ��
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
                        '�������������ݶ�Ϊ�գ��򲻴���
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
    ''' �жϻ�õ���ң�����Ƿ�Ϊ��ʱ���ݣ����ȫ��FF��֤���ǳ�ʱ���ݣ�ֱ�Ӷ���
    ''' </summary>
    ''' <remarks></remarks>
    Public Function CheckData(ByVal datainf() As String, ByVal ID As Integer) As Boolean

        Dim group_num As Integer
        group_num = (datainf.Length - 3) / 54  '���������
        Dim i As Integer = 0
        Dim noinf_num As Integer = 0 '��ʱ���������
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
    ''' ��ȡ�����
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
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Get_boxtype" & " ʱ�䣺" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

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
    ''' ��ȡ�����
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
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Get_boxtype" & " ʱ�䣺" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

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
    ''' ��������Ļ�·��Ϣ��������(2011��9��5�����ӵ�С��ң����)AABBCC
    ''' </summary>
    ''' <param name="group_id">-1��ʾ�ϰ����ݣ�û����ţ�0Ϊ��һ�飻1Ϊ�ڶ��飻2λ������</param>
    ''' <param name="status_list">6·��·״̬</param>
    ''' <param name="problem_tag" >״̬</param>
    ''' <param name="control_box_id" >��������</param>
    ''' <param name="ID" >��¼���</param>
    ''' <remarks></remarks>
    Public Sub Get_Huilu_inf_small(ByVal group_id As Integer, ByVal status_list() As String, ByVal problem_tag As String, ByVal control_box_id As String, ByVal ID As Integer)

        Dim VA_value, VB_value, VC_value As Double  'ʮ���Ƶĵ�ѹֵ
        Dim Current_value(11) As Double  '����ֵ
        Dim Power_value(11) As Double  '�繦��
        Dim Power_yinshu(11) As Double   '��������
        Dim sendData As String = "" '������Ѿ�������������
        Dim start_id As Integer '���ݵ���ʼ���
        Dim currentalarm_string As String = "" '��·������բ����
        Dim box_type As Integer '����������
        Dim i As Integer
        If group_id = -1 Then
            start_id = 2
        Else
            start_id = 3
        End If
        Get_TopBottom_value()
        If status_list(start_id) = "FF" And status_list(start_id + 1) = "FF" And status_list(start_id + 2) = "FF" And status_list(start_id + 3) = "FF" And status_list(start_id + 4) = "FF" _
        And status_list(start_id + 5) = "FF" Then
            '��ʱ���ݣ���������
            sendData = ""
            GoTo next1

        End If

        box_type = Get_boxtype(control_box_id)  '����������
        VA_value = System.Convert.ToInt64(status_list(start_id) & status_list(start_id + 1), 16)
        VA_value = Format(Com_inf.Get_Presure(VA_value, control_box_id), "0.00")
        VB_value = System.Convert.ToInt64(status_list(start_id + 2) & status_list(start_id + 3), 16)
        VB_value = Format(Com_inf.Get_Presure(VB_value, control_box_id), "0.00")
        VC_value = System.Convert.ToInt64(status_list(start_id + 4) & status_list(start_id + 5), 16)
        VC_value = Format(Com_inf.Get_Presure(VC_value, control_box_id), "0.00")
        sendData = VA_value.ToString & " " & VB_value.ToString & " " & VC_value.ToString  '�ַ���

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
            Current_value(i) = Format(Get_Current(Current_value(i), control_box_id, i + 1, box_type), "0.00")  '����

            If Current_value(i) = 0 Then
                Power_value(i) = Format(0, "0.00")
            Else

                Power_value(i) = Format(Get_Power1(Power_value(i), control_box_id, i + 1, box_type), "0.00") '����
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

        'ͨ������Ĺ�����Ϣ�����������ϱ���
        '���������ֽڵĵ�ѹ������Ϣ
        Dim power As String

        Dim chaoshi_tag As Integer = 0

        power = System.Convert.ToInt32(status_list(start_id + 30), 16).ToString

        If power <> 0 And power <> 4 Then
            problem_tag &= "A�ࣺ" & get_alarminf("power_" & power, 0, control_box_id) & " "

        End If
        If power = 4 And chaoshi_tag = 0 Then
            problem_tag &= "���������ݲɼ���ʱ"
            chaoshi_tag += 1
        End If
        power = System.Convert.ToInt32(status_list(start_id + 31), 16)
        If power <> 0 And power <> 4 Then
            problem_tag &= "B�ࣺ" & get_alarminf("power_" & power, 0, control_box_id) & " "

        End If
        If power = 4 And chaoshi_tag = 0 Then
            problem_tag &= "���������ݲɼ���ʱ"
            chaoshi_tag += 1
        End If
        power = System.Convert.ToInt32(status_list(start_id + 32), 16)
        If power <> 0 And power <> 4 Then
            problem_tag &= "C�ࣺ" & get_alarminf("power_" & power, 0, control_box_id) & " "

        End If
        If power = 4 And chaoshi_tag = 0 Then
            problem_tag &= "���������ݲɼ���ʱ"
            chaoshi_tag += 1
        End If


        '�ж�״̬�����ǲ��������ı�����Ϣ
        If status_list.Length > start_id + 34 Then
            '�жϵ���������Ϣ
            Dim huilu_num As Integer '��·������
            Dim huilu_id As Integer '��·�ı��
            Dim alarm_tag As Integer '������־
            huilu_num = System.Convert.ToInt32(status_list(start_id + 33), 16)
            i = 0
            While i < huilu_num * 2
                huilu_id = System.Convert.ToInt32(status_list(start_id + 34 + i), 16)
                alarm_tag = System.Convert.ToInt32(status_list(start_id + 35 + i), 16)
                If alarm_tag <> 0 And alarm_tag <> 9 Then
                    problem_tag &= get_alarminf("current_" & alarm_tag, huilu_id, control_box_id) & "(��·" & huilu_id & ") "
                End If
                If alarm_tag = 9 And chaoshi_tag = 0 Then
                    chaoshi_tag += 1
                    problem_tag &= get_alarminf("current_" & alarm_tag, huilu_id, control_box_id)

                End If
                i += 2
            End While
        End If

    
        If Trim(problem_tag) = "" Then
            problem_tag = "����"
        End If

        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String
        Dim control_box_name As String = ""
        Dim state_string As String
        Dim board_num As Integer
        msg = ""
        '��handlerflag��Ϊ1
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        sql = "update RoadLightStatus set HandlerFlag=1 where ID ='" & ID & "'"
        DBOperation.ExecuteSQL(conn, sql, msg)

        '��ȡ��ǰ�����Ĺ��ϴ���
        sql = "select * from control_box where control_box_id='" & control_box_id & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "find_box_state_fun5" & " ʱ�䣺" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

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

            '����ǰ�������������ݴ����ӵ�control_box����
            If problem_tag <> "����" Then
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
        '����control_box_state�е�״̬��¼
        Dim state_type As String = "״̬"
        state_string = ""
        sql = "select state, StatusContent from control_box where control_box_id='" & control_box_id & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "find_box_state_fun4" & " ʱ�䣺" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            Exit Sub

        End If
        If rs.RecordCount > 0 Then
            state_string = Trim(rs.Fields("state").Value)

            '����control_box_state�е�״̬��¼
            If state_string <> "" Then
                Setcontrolbox_Record(control_box_name, state_string, "״̬")
            End If

            'Setcontrolbox_Record(control_box_name, problem_tag, "״̬")

            If problem_tag <> "����" And m_controlproblemnum = 1 Then  'ȷ���Ĵι��Ϻ��Ͷ���
                '���Ͷ���
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
    ''' ��������Ļ�·��Ϣ��������(ABCABCABCABC)
    ''' </summary>
    ''' <param name="group_id">-1��ʾ�ϰ����ݣ�û����ţ�0Ϊ��һ�飻1Ϊ�ڶ��飻2λ������</param>
    ''' <param name="status_list">12·��·״̬</param>
    ''' <param name="problem_tag" >״̬</param>
    ''' <param name="control_box_id" >��������</param>
    ''' <remarks></remarks>
    Public Sub Get_Huilu_infABC(ByVal group_id As Integer, ByVal group_num As Integer, ByVal status_list() As String, ByVal problem_tag As String, ByVal control_box_id As String)
        Dim VA_value, VB_value, VC_value As Double  'ʮ���Ƶĵ�ѹֵ
        Dim Current_value(11) As Double  '����ֵ
        Dim Power_value(11) As Double  '�繦��
        Dim Power_yinshu(11) As Double   '��������
        Dim sendData As String '������Ѿ�������������
        Dim start_id As Integer '���ݵ���ʼ���
        Dim currentalarm_string As String = "" '��·������բ����
        Dim box_type As Integer '����������
        Dim i As Integer
        Dim presure_type As Integer '��ѹ��λ
        Dim control_box_name As String = Com_inf.Get_box_name(control_box_id)
        If group_id = -1 Then
            start_id = 2
        Else
            start_id = 3
        End If
        Get_TopBottom_value()
        box_type = Get_boxtype(control_box_id)  '����������
        If status_list(start_id) = "FF" And status_list(start_id + 1) = "FF" And status_list(start_id + 2) = "FF" And status_list(start_id + 3) = "FF" And status_list(start_id + 4) = "FF" _
        And status_list(start_id + 5) = "FF" Then
            '��������Ϊ��ʱ���ݲ�����
            Exit Sub
        End If

        VA_value = System.Convert.ToInt64(status_list(start_id) & status_list(start_id + 1), 16)
        VA_value = Format(Com_inf.Get_Presure(VA_value, control_box_id), "0.00")
        VB_value = System.Convert.ToInt64(status_list(start_id + 2) & status_list(start_id + 3), 16)
        VB_value = Format(Com_inf.Get_Presure(VB_value, control_box_id), "0.00")
        VC_value = System.Convert.ToInt64(status_list(start_id + 4) & status_list(start_id + 5), 16)
        VC_value = Format(Com_inf.Get_Presure(VC_value, control_box_id), "0.00")

        sendData = VA_value.ToString & " " & VB_value.ToString & " " & VC_value.ToString  '�ַ���

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
            Current_value(i) = Format(Get_Current(Current_value(i), control_box_id, i + 1 + group_id * 12, box_type), "0.00")  '����


            If Current_value(i) = 0 Then
                Power_value(i) = Format(0, "0.00")
            Else

                Power_value(i) = Format(Get_Power1(Power_value(i), control_box_id, i + 1 + group_id * 12, box_type), "0.00") '����
            End If

            'ԭ������Ĭ�ϵ�AAAABBBBCCCC˳���������������2012��5��3�ո���Ϊ�����û����趨����������
            '���������Ĭ�ϵ�ABCABCABCABC
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
        '��handlerflag��Ϊ1
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If

        '���µ�������ң����
        sql = "select * from control_box where control_box_id='" & control_box_id & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "find_box_state_fun3" & " ʱ�䣺" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

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
    ''' ��������Ļ�·��Ϣ��������(2011��9��5�����ӵ�С��ң����)(ABCABC)
    ''' </summary>
    ''' <param name="group_id">-1��ʾ�ϰ����ݣ�û����ţ�0Ϊ��һ�飻1Ϊ�ڶ��飻2λ������</param>
    ''' <param name="status_list">6·��·״̬</param>
    ''' <param name="problem_tag" >״̬</param>
    ''' <param name="control_box_id" >��������</param>
    ''' <param name="ID" >��¼���</param>
    ''' <remarks></remarks>
    Public Sub Get_Huilu_inf_smallABC(ByVal group_id As Integer, ByVal status_list() As String, ByVal problem_tag As String, ByVal control_box_id As String, ByVal ID As Integer)

        Dim VA_value, VB_value, VC_value As Double  'ʮ���Ƶĵ�ѹֵ
        Dim Current_value(11) As Double  '����ֵ
        Dim Power_value(11) As Double  '�繦��
        Dim Power_yinshu(11) As Double   '��������
        Dim sendData As String = "" '������Ѿ�������������
        Dim start_id As Integer '���ݵ���ʼ���
        Dim currentalarm_string As String = "" '��·������բ����
        Dim box_type As Integer '����������
        Dim i As Integer
        Dim presure_type As Integer '��λ 0�� 1��2
        Dim control_box_name As String = Com_inf.Get_box_name(control_box_id)
        If group_id = -1 Then
            start_id = 2
        Else
            start_id = 3
        End If
        Get_TopBottom_value()
        If status_list(start_id) = "FF" And status_list(start_id + 1) = "FF" And status_list(start_id + 2) = "FF" And status_list(start_id + 3) = "FF" And status_list(start_id + 4) = "FF" _
        And status_list(start_id + 5) = "FF" Then
            '��ʱ���ݣ���������
            sendData = ""
            GoTo next1

        End If

        box_type = Get_boxtype(control_box_id)  '����������
        VA_value = System.Convert.ToInt64(status_list(start_id) & status_list(start_id + 1), 16)
        VA_value = Format(Com_inf.Get_Presure(VA_value, control_box_id), "0.00")
        VB_value = System.Convert.ToInt64(status_list(start_id + 2) & status_list(start_id + 3), 16)
        VB_value = Format(Com_inf.Get_Presure(VB_value, control_box_id), "0.00")
        VC_value = System.Convert.ToInt64(status_list(start_id + 4) & status_list(start_id + 5), 16)
        VC_value = Format(Com_inf.Get_Presure(VC_value, control_box_id), "0.00")
        sendData = VA_value.ToString & " " & VB_value.ToString & " " & VC_value.ToString  '�ַ���

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
            Current_value(i) = Format(Get_Current(Current_value(i), control_box_id, i + 1, box_type), "0.00")  '����

            If Current_value(i) = 0 Then
                Power_value(i) = Format(0, "0.00")
            Else

                Power_value(i) = Format(Get_Power1(Power_value(i), control_box_id, i + 1, box_type), "0.00") '����
            End If

            'ԭ������Ĭ�ϵ�AABBCC˳���������������2012��5��3�ո���Ϊ�����û����趨����������
            '���������Ĭ�ϵ�ABCABC()
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

        'ͨ������Ĺ�����Ϣ�����������ϱ���
        '���������ֽڵĵ�ѹ������Ϣ
        Dim power As String

        Dim chaoshi_tag As Integer = 0

        power = System.Convert.ToInt32(status_list(start_id + 30), 16).ToString

        If power <> 0 And power <> 4 Then
            problem_tag &= "A�ࣺ" & get_alarminf("power_" & power, 0, control_box_id) & " "

        End If
        If power = 4 And chaoshi_tag = 0 Then
            problem_tag &= "���������ݲɼ���ʱ"
            chaoshi_tag += 1
        End If
        power = System.Convert.ToInt32(status_list(start_id + 31), 16)
        If power <> 0 And power <> 4 Then
            problem_tag &= "B�ࣺ" & get_alarminf("power_" & power, 0, control_box_id) & " "

        End If
        If power = 4 And chaoshi_tag = 0 Then
            problem_tag &= "���������ݲɼ���ʱ"
            chaoshi_tag += 1
        End If
        power = System.Convert.ToInt32(status_list(start_id + 32), 16)
        If power <> 0 And power <> 4 Then
            problem_tag &= "C�ࣺ" & get_alarminf("power_" & power, 0, control_box_id) & " "

        End If
        If power = 4 And chaoshi_tag = 0 Then
            problem_tag &= "���������ݲɼ���ʱ"
            chaoshi_tag += 1
        End If
        '2012��6��11����������ѹ��ȱ��ı�ʧѹ����
        If problem_tag = "A�ࣺȱ�� B�ࣺȱ�� C�ࣺȱ�� " Then
            problem_tag = "ʧѹ "
        End If

        '�ж�״̬�����ǲ��������ı�����Ϣ
        If status_list.Length > start_id + 34 Then
            '�жϵ���������Ϣ
            Dim huilu_num As Integer '��·������
            Dim huilu_id As Integer '��·�ı��
            Dim alarm_tag As Integer '������־
            huilu_num = System.Convert.ToInt32(status_list(start_id + 33), 16)
            i = 0
            While i < huilu_num * 2
                huilu_id = System.Convert.ToInt32(status_list(start_id + 34 + i), 16)
                alarm_tag = System.Convert.ToInt32(status_list(start_id + 35 + i), 16)
                '2012��6��11��ȥ�������������
                If alarm_tag <> 0 And alarm_tag <> 9 And alarm_tag <> 5 Then
                    problem_tag &= get_alarminf("current_" & alarm_tag, huilu_id, control_box_id) & "(��·" & huilu_id & ") "
                End If
                If alarm_tag = 9 And chaoshi_tag = 0 Then
                    chaoshi_tag += 1
                    problem_tag &= get_alarminf("current_" & alarm_tag, huilu_id, control_box_id)

                End If
                i += 2
            End While
        End If


        If Trim(problem_tag) = "" Then
            problem_tag = "����"
        End If

        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String
        'Dim control_box_name As String = ""
        Dim state_string As String
        Dim board_num As Integer
        msg = ""
        '��handlerflag��Ϊ1
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        sql = "update RoadLightStatus set HandlerFlag=1 where ID ='" & ID & "'"
        DBOperation.ExecuteSQL(conn, sql, msg)

        '��ȡ��ǰ�����Ĺ��ϴ���
        sql = "select * from control_box where control_box_id='" & control_box_id & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "find_box_state_fun5" & " ʱ�䣺" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

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

            '����ǰ�������������ݴ����ӵ�control_box����
            If problem_tag <> "����" Then
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
        '����control_box_state�е�״̬��¼
        Dim state_type As String = "״̬"
        state_string = ""
        sql = "select state, StatusContent from control_box where control_box_id='" & control_box_id & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "find_box_state_fun4" & " ʱ�䣺" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            Exit Sub

        End If
        If rs.RecordCount > 0 Then
            state_string = Trim(rs.Fields("state").Value)

            '����control_box_state�е�״̬��¼
            If state_string <> "" Then
                Setcontrolbox_Record(control_box_name, state_string, "״̬")
            End If

            'Setcontrolbox_Record(control_box_name, problem_tag, "״̬")

            If problem_tag <> "����" And m_controlproblemnum = 4 Then  'ȷ���Ĵι��Ϻ��Ͷ���
                '���Ͷ���
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
    ''' ���ݻ�·�����û�ȡ��ѹ��λ
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
            get_presuretype = 0  'Ĭ�ϵ������A��
            Exit Function
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs.RecordCount > 0 Then
            '�û�·�Ѿ������˵�ѹ��λ
            get_presuretype = rs.Fields("presure_type").Value
        Else
            get_presuretype = (huilu_id - 1) Mod 3  'Ĭ��ΪABC
        End If

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing

    End Function


    ''' <summary>
    ''' ͨ���ϴ���Ϣ��ȡ������Ϣ
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
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "get_alarminf" & " ʱ�䣺" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing

            Exit Function
        End If
        If rs.RecordCount > 0 Then
            get_alarminf = Trim(rs.Fields("type").Value)
            control_box_name = Com_inf.Get_box_name(control_box_id)

            If tag = "current_1" Or tag = "current_2" Or tag = "current_4" Or tag = "current_5" Then
                '�Ӵ�������
                sql = "select jiechuqi_id from huilu_inf where huilu_id='" & huilu_id & "' and control_box_name='" & control_box_name & "'"
                rs_jiechuqi = DBOperation.SelectSQL(conn, sql, msg)

                If rs_jiechuqi Is Nothing Then
                    g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "get_alarminf" & " ʱ�䣺" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)
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
    ''' ��������Ļ�·��Ϣ��������
    ''' </summary>
    ''' <param name="group_id">-1��ʾ�ϰ����ݣ�û����ţ�0Ϊ��һ�飻1Ϊ�ڶ��飻2λ������</param>
    ''' <param name="status_list">12·��·״̬</param>
    ''' <param name="problem_tag" >״̬</param>
    ''' <param name="control_box_id" >��������</param>
    ''' <remarks></remarks>
    Public Sub Get_Huilu_inf(ByVal group_id As Integer, ByVal group_num As Integer, ByVal status_list() As String, ByVal problem_tag As String, ByVal control_box_id As String)
        Dim VA_value, VB_value, VC_value As Double  'ʮ���Ƶĵ�ѹֵ
        Dim Current_value(11) As Double  '����ֵ
        Dim Power_value(11) As Double  '�繦��
        Dim Power_yinshu(11) As Double   '��������
        Dim sendData As String '������Ѿ�������������
        Dim start_id As Integer '���ݵ���ʼ���
        Dim currentalarm_string As String = "" '��·������բ����
        Dim box_type As Integer '����������
        Dim i As Integer
        If group_id = -1 Then
            start_id = 2
        Else
            start_id = 3
        End If
        Get_TopBottom_value()
        box_type = Get_boxtype(control_box_id)  '����������
        If status_list(start_id) = "FF" And status_list(start_id + 1) = "FF" And status_list(start_id + 2) = "FF" And status_list(start_id + 3) = "FF" And status_list(start_id + 4) = "FF" _
        And status_list(start_id + 5) = "FF" Then
            '��������Ϊ��ʱ���ݲ�����
            Exit Sub
        End If

        VA_value = System.Convert.ToInt64(status_list(start_id) & status_list(start_id + 1), 16)
        VA_value = Format(Com_inf.Get_Presure(VA_value, control_box_id), "0.00")
        VB_value = System.Convert.ToInt64(status_list(start_id + 2) & status_list(start_id + 3), 16)
        VB_value = Format(Com_inf.Get_Presure(VB_value, control_box_id), "0.00")
        VC_value = System.Convert.ToInt64(status_list(start_id + 4) & status_list(start_id + 5), 16)
        VC_value = Format(Com_inf.Get_Presure(VC_value, control_box_id), "0.00")

        sendData = VA_value.ToString & " " & VB_value.ToString & " " & VC_value.ToString  '�ַ���

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
            Current_value(i) = Format(Get_Current(Current_value(i), control_box_id, i + 1, box_type), "0.00")  '����


            If Current_value(i) = 0 Then
                Power_value(i) = Format(0, "0.00")
            Else

                Power_value(i) = Format(Get_Power1(Power_value(i), control_box_id, i + 1, box_type), "0.00") '����
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
        '��handlerflag��Ϊ1
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If

        '���µ�������ң����
        sql = "select * from control_box where control_box_id='" & control_box_id & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "find_box_state_fun3" & " ʱ�䣺" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

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
    ''' ���ݵ���ֵ��������բ�����ж�
    ''' </summary>
    ''' <param name="current"></param>
    ''' <param name="control_box_id" >�������</param>
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

            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Current_Alarm" & " ʱ�䣺" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            Current_Alarm = ""
            Exit Function
        End If
        If rs.RecordCount > 0 Then
            '�жϽӴ����ıպϻ�Ͽ�
            jiechuqi_id = Trim(rs.Fields("jiechuqi_id").Value)
            While jiechuqi_id.Length < LAMP_ID_LEN
                jiechuqi_id = "0" & jiechuqi_id
            End While
            jiechuqi_id = control_box_id & "31" & jiechuqi_id
            sql = "select state, result from lamp_inf where lamp_id='" & jiechuqi_id & "'"
            rs_jiechuqi = DBOperation.SelectSQL(conn, sql, msg)
            If rs_jiechuqi Is Nothing Then
                g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Current_Alarm" & " ʱ�䣺" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

                GoTo finish
            End If
            If rs_jiechuqi.Fields("state").Value = 1 Or rs_jiechuqi.Fields("state").Value = 4 Then
                '�Ӵ���ʱ�պϵ�
                If current < 0.5 Then
                    Current_Alarm = "��·������բ"
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
    ''' ת���ɵ����Ĺ�ʽ�������䣩
    ''' </summary>
    ''' <param name="Current"></param>
    ''' <param name="huilu_id" >��·ID</param>
    ''' <param name="box_id" >��������</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Get_Current(ByVal Current As Double, ByVal box_id As String, ByVal huilu_id As Integer, ByVal box_type As Integer) As Double
        ''ԭ��û�б�ȵ�
        'Get_Current = Current / &H7FFF * 176.78 / 150 * 100
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim bianbi As Integer
        ' Dim box_type As Integer '��������
        m_currenttopvalue = 50
        m_currentbottomvalue = 0
        If DBOperation.OpenConn(conn) = False Then
            Exit Function
        End If
        msg = ""
        ' box_type = 1
        '���ݵ�����źͻ�·��Ų������Ӧ�ı��ֵ��Ĭ�ϵ����Ϊ1
        bianbi = 1
        sql = "SELECT control_box_type,huilu_inf.bianbi , control_box.control_box_id,huilu_inf.current_alarmtop, huilu_inf.current_alarmbot FROM  control_box INNER JOIN huilu_inf ON control_box.control_box_name = huilu_inf.control_box_name where control_box_id='" & box_id & "' and huilu_id='" & huilu_id & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Get_Current" & " ʱ�䣺" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

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
        '2011-4-21���Ĺ�ʽ
        If box_type = 1 Then
            Get_Current = Current / &H7FFF * 176.78 / 150 * 5
        Else
            Get_Current = Current * 8.3 / &HFFFF
        End If
        '2012��1��11�գ��������⴦��������0.2������0
        If Get_Current <= 0.2 Then
            Get_Current = 0

        End If
        Get_Current = Get_Current * bianbi

        '2011��3��14��ɾ��һ��������һ�����ֵ�����÷���
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
        '    '���ӱ��
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
    ''' ����ת���ɵ����Ĺ�ʽ�������䣩
    ''' </summary>
    ''' <param name="Current"></param>
    ''' <param name="huilu_id" >��·ID</param>
    ''' <param name="box_id" >��������</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Get_EXECurrent(ByVal Current As Double, ByVal box_id As String, ByVal huilu_id As Integer, ByVal box_type As Integer) As Integer
        ''ԭ��û�б�ȵ�
        'Get_Current = Current / &H7FFF * 176.78 / 150 * 100
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim bianbi As Integer
        ' Dim box_type As Integer '��������
      
        If DBOperation.OpenConn(conn) = False Then
            Exit Function
        End If
        msg = ""
        ' box_type = 1
        '���ݵ�����źͻ�·��Ų������Ӧ�ı��ֵ��Ĭ�ϵ����Ϊ1
        bianbi = 1
        sql = "SELECT control_box_type,huilu_inf.bianbi , control_box.control_box_id,huilu_inf.current_alarmtop, huilu_inf.current_alarmbot FROM  control_box INNER JOIN huilu_inf ON control_box.control_box_name = huilu_inf.control_box_name where control_box_id='" & box_id & "' and huilu_id='" & huilu_id & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Get_Current" & " ʱ�䣺" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

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

        '2011-4-21���Ĺ�ʽ
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
    ''' ת���ɵ繦�ʵĹ�ʽ�������䣩
    ''' </summary>
    ''' <param name="Power1"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Get_Power1(ByVal Power1 As Double, ByVal box_id As String, ByVal huilu_id As Integer, ByVal box_type As Integer) As Double
        ''ԭ��û�б�ȵ�
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
        '���ݵ�����źͻ�·��Ų������Ӧ�ı��ֵ��Ĭ�ϵ����Ϊ1
        bianbi = 1
        sql = "SELECT control_box_type,huilu_inf.bianbi FROM  control_box INNER JOIN huilu_inf ON control_box.control_box_name = huilu_inf.control_box_name where control_box_id='" & box_id & "' and huilu_id='" & huilu_id & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Get_Power1" & " ʱ�䣺" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

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
        '2011-4-21���Ĺ�ʽ
        If box_type = 1 Then
            Get_Power1 = Power1 / &H7FFF * 300 * 5 * bianbi
        Else
            Get_Power1 = Power1 * 4.05 / &HFFFF * bianbi

        End If

        ''2011��3��14��ɾ��һ��������һ����ȵ�����
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
        '    '���ӱ��
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

    '�����ӽڵ�״̬
    Public Function setChild(ByVal node As TreeNode) As Boolean
        '    Dim child As New TreeNode
        For Each child As TreeNode In node.Nodes
            child.Checked = node.Checked
        Next
        setChild = True

    End Function

    '���ø��ڵ�״̬
    Public Sub setparent(ByVal node As TreeNode)
        ' Dim brother As New TreeNode
        If node.Parent IsNot Nothing Then
            '�����ǰ�ڵ�״̬Ϊ��ѡ������Ҫ�����ֵܽڵ㶼��ѡ���ܹ�ѡ���ڵ�
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
    ''' ���ϴ��Ŀ��������ݷ�Ϊ�����źͽ�2���ֽڵĿ������ֽ�Ϊ16λ��0��1��ʾ
    ''' </summary>
    ''' <param name="alarm_list" >������Ŀ���������</param>
    ''' <param name="kaiguan_string" >�ϴ��Ŀ������ַ���,2�ֽڵĵ�����ţ�2�ֽڵ�״̬�ϴ�����</param>
    ''' <remarks></remarks>
    Public Sub get_kaiguanString(ByVal kaiguan_string As String, ByVal alarm_list() As String)
        Dim str() As String
        Dim control_box_id As String  '�������
        Dim kaiguan As String  '16λ�Ŀ���������
        Dim power_type As String '��Դ����

        str = kaiguan_string.Split(" ")
        control_box_id = System.Convert.ToInt32(str(0) & str(1), 16)  'ǰ�����ֽ���Ϊ�������
        While control_box_id.Length < 4
            control_box_id = "0" & control_box_id
        End While
        '2011��5��26���޸ģ�������2���ֽ���ǰһ���ֽ��ǵͰ�λ����һ���ֽ��Ǹ߰�λ
        kaiguan = Com_inf.HEX_to_BIN(str(3) & str(2))
        power_type = Val(str(4)).ToString
        alarm_list(0) = control_box_id
        alarm_list(1) = kaiguan
        alarm_list(2) = power_type

    End Sub

    ''' <summary>
    ''' �����ϴ��Ŀ�������ǰ����������ֵ��Ĭ�ϸ��Ӵ����ģ��жϽӴ�����ʵ��״̬
    ''' </summary>
    ''' <param name="kaiguan_tag" >������1-16</param>
    ''' <param name="value" >�ϴ��Ŀ�������ֵ0 or 1</param>
    ''' <param name="control_box_id" >��������</param>
    ''' <remarks></remarks>
    Public Sub set_huilu_actualstate(ByVal kaiguan_tag As Integer, ByVal value As Integer, ByVal control_box_id As String, ByVal control_box_name As String)
      
        If value = 0 Then
            set_huiluinf(control_box_name, kaiguan_tag, "����")
        Else
            set_huiluinf(control_box_name, kaiguan_tag, "�Ͽ�")

        End If
    End Sub

    ''' <summary>
    ''' �����ϴ��Ŀ������ж��Ƿ񱨾�
    ''' </summary>
    ''' <param name="kaiguan_tag" >������1-16</param>
    ''' <param name="value" >�ϴ��Ŀ�������ֵ0 or 1</param>
    ''' <param name="control_box_name" >����������</param>
    ''' <returns>""Ϊ����������Ϊ""�򱨾������ص��Ǳ�������</returns>
    ''' <remarks></remarks>
    Public Function alarm_yes_no(ByVal kaiguan_tag As Integer, ByVal value As Integer, ByVal control_box_name As String) As String
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim alarmstring As String '���ϵ�����
        Dim lampid As String '�Ӵ������

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
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Alarm_YorN" & " ʱ�䣺" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            alarm_yes_no = ""
            Exit Function
        End If


        If rs.RecordCount > 0 Then
            While rs.EOF = False
                '����жϵ��ǽӴ�������������Ҫ���ϴ������ݺ�ʵ�ʵĽӴ���״̬���жԱȣ��Ա�һ�²���������һ���򱨾�
                '���ö�û�Ͽ����ñպ�û�պϳɹ�
                alarmstring = Trim(rs.Fields("alarm_type").Value)
                If alarmstring.Substring(0, 1) = "K" Then
                    lampid = Com_inf.Get_Number(alarmstring)
                    While lampid.Length < LAMP_ID_LEN
                        lampid = "0" & lampid
                    End While

                    lampid = "31" & lampid  'ͨ��ID��ȡ�Ӵ����ڵ���
                    If Check_jiechuqi_alarm(control_box_name, value, lampid) = True Then
                        alarm_yes_no &= Trim(rs.Fields("alarm_type").Value) & " "
                        '��ĳ��������µĽӴ����й��ϣ����huilu_inf���е�open_close��Ϊ��Ӧ���ϴ�״̬
                        'set_huiluinf(control_box_name, Val(Mid(lampid, 3, LAMP_ID_LEN)), alarmstring.Split("_")(1))
                        If value = 0 Then
                            set_huiluinf(control_box_name, Val(Mid(lampid, 3, LAMP_ID_LEN)), "����")
                        Else
                            set_huiluinf(control_box_name, Val(Mid(lampid, 3, LAMP_ID_LEN)), "�Ͽ�")

                        End If
                    Else
                        alarm_yes_no &= ""
                        If value = 0 Then
                            set_huiluinf(control_box_name, Val(Mid(lampid, 3, LAMP_ID_LEN)), "����")
                        Else
                            set_huiluinf(control_box_name, Val(Mid(lampid, 3, LAMP_ID_LEN)), "�Ͽ�")

                        End If

                    End If
                Else
                    '�ǽӴ����������㣬�������õĽ��б����ж�
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
    ''' ���ݻ�·id ��ȡ����Ӧ�ĽӴ���id
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
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Get_jiechuqi_id" & " ʱ�䣺" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

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
    ''' �ԱȽӴ����͸�������Ĺ�ϵ���ж��Ƿ���Ҫ����
    ''' </summary>
    ''' <param name="control_box_name"></param>
    ''' <param name="state"></param>
    ''' <param name="lamp_id"></param>
    ''' <returns>true������false������</returns>
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
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Check_jiechuqi_alarm" & " ʱ�䣺" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
        End If

        If rs.RecordCount > 0 Then
            If rs.Fields("state").Value = state Or rs.Fields("state").Value = state + 3 Then
                If state = 0 Then
                    'Kδ�Ͽ�
                    sql = "update lamp_inf set result=2 where lamp_id='" & rs.Fields("lamp_id").Value & "'"
                    DBOperation.SelectSQL(conn, sql, msg)

                Else
                    'Kδ�պ�
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
        Dim alarm_list(2) As String '��ſ�����������3λ���ȵ��ַ���
        Dim control_box_id As String '�������
        Dim control_box_name As String '���������
        Dim i As Integer
        Dim alarmstring As String '�����ַ���
        Dim alarm_tag As Boolean = False
        Dim alarminf() As String '�����б�
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
            control_box_id = alarm_list(0)  '�����ֽڵĵ������

            '2011��4��20�����ӵ������õ����ͣ���ػ�����
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
                    If alarmstring <> "" Then  '����

                        '�б���
                        alarminf = Trim(alarmstring).Split(" ")
                        j = 0
                        While j < alarminf.Length
                            sql = "select * from kaiguan_alarm_list where control_box_name='" & control_box_name & "' and alarm_string='" & alarminf(j) & "' and (alarm_tag=0 or alarm_tag=2)"
                            rs_record = DBOperation.SelectSQL(conn, sql, msg)
                            If rs_record Is Nothing Then
                                g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Check_Kaiguan_Alarm" & " ʱ�䣺" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

                                GoTo finish
                            End If
                            If rs_record.RecordCount <= 0 Then
                                alarm_tag = True
                                'ԭ��û�б�������
                                sql = "insert into kaiguan_alarm_list(alarm_string,createtime,alarm_tag," _
                              & "control_box_name,kaiguan_tag) values('" & alarminf(j) & "','" & Now & "'," _
                              & "0,'" & control_box_name & "' ,'" & i + 1 & "')"
                                DBOperation.ExecuteSQL(conn, sql, msg)
                            Else
                                '��ԭ���б�����Ϣ����ԭ���ı�����Ϣ��Ϊ2����ʾ����ȷ�ϵ�
                                alarm_tag = True
                                sql = "update kaiguan_alarm_list set alarm_tag=2 where id='" & rs_record.Fields("id").Value & "'"
                                DBOperation.ExecuteSQL(conn, sql, msg)

                            End If
                            j += 1

                        End While


                    Else  '״̬�������ģ����ѯ��ǰ�ı������б�����Ϣ�򽫱�����Ϣ��1
                        sql = "select * from kaiguan_alarm_list where control_box_name='" & control_box_name & "' and kaiguan_tag='" & i + 1 & "' and (alarm_tag=0 or alarm_tag=2)"
                        rs_record = DBOperation.SelectSQL(conn, sql, msg)
                        If rs_record Is Nothing Then
                            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Check_Kaiguan_Alarm" & " ʱ�䣺" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

                            GoTo finish
                        End If
                        While rs_record.EOF = False
                            alarm_tag = True
                            'ԭ���б������ݣ��򽫱�����־��Ϊ2����ʾ��������
                            'rs_record.Fields("alarm_tag").Value = 1
                            'rs_record.Fields("endtime").Value = Now
                            'rs_record.Update()
                            sql = "update kaiguan_alarm_list set alarm_tag=1,endtime='" & Now & "' where id='" & rs_record.Fields("id").Value & "'"
                            DBOperation.ExecuteSQL(conn, sql, msg)

                            rs_record.MoveNext()
                        End While


                    End If
                    i += 1  'ͬһ���������е�16λ��������������
                End While

                '����������Ĺ�������
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



                '��¼�������
                If alarm_list(2) = 1 Then
                    Setcontrolbox_Record(control_box_name, POWERTYPE_BUTTERY, "����")
                Else
                    Setcontrolbox_Record(control_box_name, POWERTYPE_CURRENT, "����")
                End If
                'ʧѹ����ˢ��
                g_welcomewinobj.BackgroundWorker_find_state.ReportProgress(2)
            End If




            '��������¼��Ϊ1
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
    ''' ���ҵ�����ͨ���Ƿ�����
    ''' </summary>
    ''' <param name="control_box_name"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function get_communication(ByVal control_box_name As String) As Boolean
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim state_name As String = "ͨ��"
        Dim state As String = "δ����"

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
    ''' ����·�Ƶĵ��������������·��ַ���
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
        Dim huilu_id As Integer '��·�ı��
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
                    huilu_id = rs.Fields("huilu_id").Value - 1   '��·���
                    current_bottom = rs.Fields("current_alarmbot").Value

                    current_top = rs.Fields("current_alarmtop").Value


                    If i + (board_id * 12) = huilu_id Then
                        '�����˸û�·��������ֵ��Ϊ1
                        config_state(board_id).use_mask(i) = 1
                        config_state(board_id).current_bottom(i) = current_bottom
                        config_state(board_id).current_top(i) = current_top

                        i += 1
                        rs.MoveNext()

                    Else
                        'û�����øû�·������·��������Ϊ0 ����������ֵĬ��Ϊ0
                        config_state(board_id).use_mask(i) = 0
                        config_state(board_id).current_bottom(i) = 0
                        config_state(board_id).current_top(i) = 0
                        i += 1
                    End If

                Else
                    'û�����øû�·������·��������Ϊ0 ����������ֵĬ��Ϊ0
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
            '��һ���ֽ�ǰ��λ��ǰ��·
            While i < 6
                mask_str = config_state(board_id).use_mask(i) & mask_str
                i += 1
            End While
            While mask_str.Length < 8
                mask_str = "0" & mask_str
            End While
            '�ڶ����ֽ�ǰ��λ�ܺ���·
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
            '12λ����
            i = 0
            While i < 12
                top_str = config_state(board_id).current_top(i)
                '����ʽת��
                top_str = Me.Get_EXECurrent(top_str, control_box_id, board_id * 12 + i + 1, box_type)
                '��������
                top_str = Com_inf.Dec_to_Hex(top_str, 4)
                top_str = Mid(top_str, 1, 2) & " " & Mid(top_str, 3, 2)

                config_string &= top_str & " "

                i += 1
            End While
            '12λ����
            i = 0
            While i < 12
                bottom_str = config_state(board_id).current_bottom(i)
                '����ʽת��
                bottom_str = Me.Get_EXECurrent(bottom_str, control_box_id, board_id * 12 + i + 1, box_type)

                '��������
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
