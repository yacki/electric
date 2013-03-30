Public Class light_control
    Private m_open_tag As Boolean = False

    '���տ���
    Public Sub control_light()
        If g_lightvalue < g_lightvalueset Then
            '��ǰ�Ĺ���ֵ�������õ���ֵ
            suntime_divoperation(True)
        End If

        If g_lightvalue >= g_lightvalueset Then
            '��ǰ�Ĺ���ֵ�����趨����ֵ
            suntime_divoperation(False)
        End If


    End Sub

    ''' <summary>
    ''' ������������ĵ�
    '''ͨ���жϹ�ص���ֵ���������������Ƿ��(�򿪵��������Ϊ�����˾�γ�ȵ�������Ӵ�������Чʱ��Ҳ���ھ�γ�ȿ��ص�ǰ��һ��Сʱ)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub open_alllamp()
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String = ""
        Dim control_lamp_obj As New control_lamp
        Dim result As Boolean
        Dim huilu, lamp As String  '���Ƶ���������
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
            result = control_lamp_obj.Input_control_inf(huilu, "����", Trim(rs.Fields("control_box_name").Value), "����ȫ��", 1, "ȫ����", 100, "", -1)

            result = control_lamp_obj.Input_control_inf("", "������", Trim(rs.Fields("control_box_name").Value), "ȫ��", 1, "ȫ����", 100, "", -1)

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
    ''' ���ݾ�γ�ȵ�ʱ����ȷ����ص�ʱ�䣬�ھ�γ�ȿ��Ƶ�ǰһ��Сʱ����Ч
    ''' </summary>
    ''' <param name="open_close" >true��ʾ����false��ʾ��</param>
    ''' <remarks></remarks>
    Public Sub suntime_divoperation(ByVal open_close As Boolean)
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String
        Dim nowtime As DateTime
        Dim lamp_id As String
        Dim order_string As String '�����ַ���
        Dim box_ox, type_ox As String
        Dim control_lamp_obj As New control_lamp
        Dim condition As String = ""  '��������
        nowtime = Now
        Dim mod_string As String = "" '��������
        Dim diangan As String = "ȫ����"
        Dim lamp_id_tag As String = ""
        Dim opentime, closetime As DateTime '���صƵ�ʱ��
        Dim openstate As Integer

        msg = ""
        ' sql = "select * from pianyi order by id "
        sql = "SELECT pianyi.lamp_id, pianyi.open_pianyi, pianyi.close_pianyi, pianyi.today_opentime, pianyi.today_closetime,lamp_inf.state FROM lamp_inf INNER JOIN pianyi ON lamp_inf.lamp_id = pianyi.lamp_id"

        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then

            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "suntime_divoperation" & " ʱ�䣺" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            Exit Sub
        End If

        While rs.EOF = False
            order_string = ""
            lamp_id = Trim(rs.Fields("lamp_id").Value)
            openstate = rs.Fields("state").Value
            opentime = System.Convert.ToDateTime(rs.Fields("today_opentime").Value).AddHours(-1)  '��γ�����ÿ���ǰһ��Сʱ���տ���Ч
            '��ȷ����
            ' If opentime.TimeOfDay >= nowtime.TimeOfDay And opentime.TimeOfDay < nowtime.AddSeconds(3).TimeOfDay Then
            '2011��11��23�վ�ȷ������
            If nowtime >= opentime And open_close = True And (openstate = 0 Or openstate = 3) Then

                g_sethuilutag = True '�е�ǰ��ʱ�ο���

                '   control_box_name = Get_box_name(lamp_id)  '��ȡ���������
                box_ox = Com_inf.Dec_to_Hex(Mid(lamp_id, 1, 4), 4)
                type_ox = Com_inf.Get_lampid_bin(Val(Mid(lamp_id, 5, 2)), Val(Mid(lamp_id, 7, LAMP_ID_LEN)))
                type_ox = Com_inf.BIN_to_HEX(type_ox)
                ' lamp_ox = Com_inf.Dec_to_ox(Mid(lamp_id, 7, 3), 2)

                order_string = Mid(box_ox, 3, 2) & " " & Mid(type_ox, 1, 2) & " " & Mid(type_ox, 3, 2) & " 1B 11 64 " & Mid(box_ox, 1, 2)

                '����򿪵��ǻ�·������Ҫ����·�����µ����е��ƽ�����ز���
                If Mid(lamp_id, 5, 2) = "31" Then
                    control_lamp_obj.open_close_huilulamp(1, Val(Mid(lamp_id, 7, LAMP_ID_LEN)), Mid(lamp_id, 1, 4))

                End If

                '�ı�lamp_inf�е�״̬��Ϣ
                sql = "update lamp_inf set state=4, result=4 where lamp_id='" & lamp_id & "'"
                DBOperation.ExecuteSQL(conn, sql, msg)

                condition = "��ؽڵ�"
                mod_string = "��·��"
                ' control_box_name = control_box_name & "������ڵ�"

            End If

            closetime = System.Convert.ToDateTime(rs.Fields("today_closetime").Value).AddHours(-1)
            ' If closetime.TimeOfDay >= nowtime.TimeOfDay And closetime.TimeOfDay <= nowtime.AddSeconds(3).TimeOfDay Then
            '2011��11��23�վ�ȷ������
            If nowtime >= closetime And open_close = False And (openstate = 1 Or openstate = 4) Then

                g_sethuilutag = True '�е�ǰ��ʱ�ο���

                'lamp_id = Trim(rs.Fields("lamp_id").Value)
                'control_box_name = Get_box_name(lamp_id)  '��ȡ���������

                box_ox = Com_inf.Dec_to_Hex(Mid(lamp_id, 1, 4), 4)
                type_ox = Com_inf.Get_lampid_bin(Val(Mid(lamp_id, 5, 2)), Val(Mid(lamp_id, 7, LAMP_ID_LEN)))
                type_ox = Com_inf.BIN_to_HEX(type_ox)
                ' type_ox = Com_inf.Dec_to_ox(Mid(lamp_id, 5, 2), 2)
                ' lamp_ox = Com_inf.Dec_to_ox(Mid(lamp_id, 7, 3), 2)

                order_string = Mid(box_ox, 3, 2) & " " & Mid(type_ox, 1, 2) & " " & Mid(type_ox, 3, 2) & " 1C 13 00 " & Mid(box_ox, 1, 2)

                '����رյ��ǻ�·������Ҫ����·�����µ����е��ƽ�����ز���
                If Mid(lamp_id, 5, 2) = "31" Then
                    control_lamp_obj.open_close_huilulamp(0, Val(Mid(lamp_id, 7, LAMP_ID_LEN)), Mid(lamp_id, 1, 4))

                End If

                '�ı�lamp_inf�е�״̬��Ϣ
                '2011��11��15�չرյƵ�ʱ���Զ�����������ѹ���繦�ʵ�ֵ��Ϊ0
                sql = "update lamp_inf set state=3, result=4,current_l=0,presure_l=0,power=0 where lamp_id='" & lamp_id & "'"
                DBOperation.ExecuteSQL(conn, sql, msg)

                condition = "��ؽڵ�"
                mod_string = "��·��"
                'control_box_name = control_box_name & "������ڵ�"

            End If

            If order_string <> "" Then
                control_lamp_obj.Input_db_control(order_string, Mid(lamp_id, 1, 4), "", 1, -1) '��������뵽���ݿ���

                lamp_id_tag = Val(Mid(lamp_id, 1, 4)).ToString & "-" & Val(Mid(lamp_id, 5, 2)).ToString & "-" & Val(Mid(lamp_id, 7, LAMP_ID_LEN)).ToString

                '��¼�ֿ���Ϣ�����ݿ�
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
    ''' �ر�����������ĵ�
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub close_alllamp()
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String = ""
        Dim control_lamp_obj As New control_lamp
        Dim result As Boolean
        Dim huilu, lamp As String  '���Ƶ���������
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
            result = control_lamp_obj.Input_control_inf(huilu, "����", Trim(rs.Fields("control_box_name").Value), "����ȫ��", 1, "ȫ����", 100, "", -1)

            result = control_lamp_obj.Input_control_inf("", "������", Trim(rs.Fields("control_box_name").Value), "ȫ��", 1, "ȫ����", 100, "", -1)

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
