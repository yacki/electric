''' <summary>
''' ��������
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
    ''' ��ȡ���̵ı���
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetProjectTitle()
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String

        msg = ""
        sql = "select * from sysconfig where type='��Ŀ����'"
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "GetProjectTitle" & " ʱ�䣺" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs.RecordCount > 0 Then
            PROJECT_TITLE_STRING = Trim(rs.Fields("name").Value)
        Else
            PROJECT_TITLE_STRING = "���ݵ������ϱ���ϵͳ"
        End If

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    ''' ���õ�ͼ������
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GetMapSize()
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String

        msg = ""
        sql = "select * from sysconfig where type='��ͼ���ߴ�'"
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "GetMapSize" & " ʱ�䣺" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs.RecordCount > 0 Then
            MAP_MAX_SIZE = Val(Trim(rs.Fields("name").Value))
        Else
            MAP_MAX_SIZE = 10
        End If

        sql = "select * from sysconfig where type='��ͼĬ�ϳߴ�'"

        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "GetMapSize" & " ʱ�䣺" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

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
        'g_mapsizevalue = map.Size '��ͼ�ĳߴ�
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub


    ''' <summary>
    ''' ��ʼ��������Ҫ���õ�ϵͳ����
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitSystemConfig()
        GetProjectTitle()  '��ȡ���̵ı���
        'GetMapSize()  '��ȡ��ͼ�ĳߴ�
        'MAP_MAX_SIZE = 20   '��ͼ�Ļ����������ֵ
        'MAP_MID_SIZE = 5    '��ͼ�Ļ������ĳ�ֵ
        'MAP_SIZE_BASE = 0.6   '���ŵĴ�С����
        'MAP_SIZE_CHANGE = 0.1    '���ŵı���

    End Sub
    ''' <summary>
    '''   ��ȡ·��״̬��ɫ�ļ�
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Read_file()
        Try
            Dim MyRead As New System.IO.StreamReader("lamp_color.txt", System.Text.Encoding.UTF8)
            g_fullcolor = Color.FromArgb(Val(MyRead.ReadLine))  '������ɫ
            g_partcolor = Color.FromArgb(Val(MyRead.ReadLine))   '�빦�ʵƵ���ɫ
            g_noreturncolor = Color.FromArgb(Val(MyRead.ReadLine))   '�޷���ֵ��ɫ
            g_closecolor = Color.FromArgb(Val(MyRead.ReadLine))   '�رյ���ɫ
            g_problemcolor = Color.FromArgb(Val(MyRead.ReadLine))   '���ϵƵ���ɫ
            MyRead.Close()  '�ر��ļ�

        Catch ex As Exception
            MsgBox(ex.Message, , PROJECT_TITLE_STRING)
        End Try
    End Sub

    ''' <summary>
    '''   ��ȡ��ͼ�ļ�
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Read_map()
        Try
            Dim MyRead As New System.IO.StreamReader("map_name.txt", System.Text.Encoding.UTF8)
            g_mapname = MyRead.ReadLine
            MyRead.Close()  '�ر��ļ�

        Catch ex As Exception
            MsgBox(ex.Message, , PROJECT_TITLE_STRING)
        End Try
    End Sub

    ''' <summary>
    ''' '��ʮ���Ƶ���ת���ɶ����Ƶ��ַ���������Ϊlen 
    ''' </summary>
    ''' <param name="num">��ֵ</param>
    ''' <param name="len">����</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Dec_to_Bin(ByVal num As Integer, ByVal len As Integer) As String
        Dim str1 As String
        Dim ox_string As String
        Dim string_len As Integer
        str1 = ""
        ox_string = Convert.ToString(num, 2).ToString.ToUpper   'ʮ����ת����2������
        string_len = ox_string.Length
        While string_len < len  '������ȱ�lenС����������ǰ����0
            ox_string = "0" & ox_string
            string_len += 1
        End While
        Dec_to_Bin = ox_string   '����ֵ

    End Function

    ''' <summary>
    '''  'ʮ���Ƶ��ַ���ת����16���Ƶ��ַ���������Ϊlen
    ''' </summary>
    ''' <param name="str">��ֵ</param>
    ''' <param name="len">����</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Dec_to_Hex(ByVal str As String, ByVal len As Integer) As String
        Dim str1 As String
        Dim num As Integer
        Dim hex_string As String
        Dim string_len As Integer  '�ַ�������
        str1 = ""
        num = Val(str)
        hex_string = Convert.ToString(num, 16).ToString.ToUpper   'ʮ����ת����16������

        string_len = hex_string.Length
        While string_len < len   '������ȱ�lenС����������ǰ����0
            hex_string = "0" & hex_string
            string_len += 1
        End While
        Dec_to_Hex = hex_string   '����ֵ
    End Function

    ''' <summary>
    ''' '������ת����ʮ������
    ''' </summary>
    ''' <param name="Bin"></param>
    ''' <returns>ʮ������</returns>
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
    ''' 'ʮ������ת���ɶ�����
    ''' </summary>
    ''' <param name="Hex"></param>
    ''' <returns>ʮ������</returns>
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
    ''' ���������ַ���ת����ʮ���Ƶģ�Ŀ�������ڽ���λ���ͺ�11λ�ĵƵı��ת����
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
    ''' '���������е�ʮ������ת����ʮ���Ƶ���λ�ĵ����ͺ���λ��·�Ʊ��
    ''' </summary>
    ''' <param name="str">���������ַ���</param>
    ''' <returns>������λ���ȵ�ʮ����������λ�Ƶ����ͺ���λ·�Ʊ��</returns>
    ''' <remarks></remarks>
    Public Function Control_Hex_to_Dec(ByVal str As String) As String
        Dim str_bin, str_id As String
        Dim lamp_type_num As Integer
        Dim lamp_id As Integer
        Dim id_str As String
        '********************2011��3��7�ս��Ƶı����չΪ5λʱ�޸�*******************
        'str_bin = HEX_to_BIN(Mid(str, 1, 2)) '��1,2��λʮ������ת���ɰ�λ������
        'str_bin &= HEX_to_BIN(Mid(str, 4, 2))  '��4,5��λʮ������ת���ɰ�λ������
        'lamp_type_num = Value_Bin(Mid(str_bin, 1, 5)).ToString  '���ͱ��
        'lamp_id = Value_Bin(Mid(str_bin, 6, 11)).ToString  '�Ƶı��


        'If lamp_type_num.ToString.Length = 1 Then  '��λ��ʮ����·�Ʊ��
        '    str_id = "0" & lamp_type_num.ToString
        'Else
        '    str_id = lamp_type_num.ToString
        'End If

        'id_str = lamp_id.ToString
        'While id_str.Length < LAMP_ID_LEN
        '    id_str = "0" & id_str
        'End While
        'str_id &= id_str

        'Control_Hex_to_Dec = str_id '������λ���ȵ�ʮ����������λ�Ƶ����ͺ���λ·�Ʊ��
        '********************2011��3��7�ս��Ƶı����չΪ5λʱ�޸�*******************
        str_bin = HEX_to_BIN(str)  '����λ��ʮ�����ƵƵı��ת����16λ������

        str_id = System.Convert.ToInt32(str_bin, 2)  '��������ת����16����
        If str_id > LAMP_ID_MAX Then
            lamp_type_num = Value_Bin(Mid(str_bin, 1, 5)).ToString  '���ͱ��
            lamp_id = Value_Bin(Mid(str_bin, 6, 11)).ToString  '�Ƶı��
        Else
            lamp_type_num = 0 '0����
            lamp_id = str_id  '�Ƶı��

        End If


        If lamp_type_num.ToString.Length = 1 Then  '��λ��ʮ����·�Ʊ��
            str_id = "0" & lamp_type_num.ToString
        Else
            str_id = lamp_type_num.ToString
        End If

        id_str = lamp_id.ToString
        While id_str.Length < LAMP_ID_LEN
            id_str = "0" & id_str
        End While
        str_id &= id_str

        Control_Hex_to_Dec = str_id '����2+LAMP_ID_LENλ���ȵ�ʮ����������λ�Ƶ����ͺ�LAMP_ID_LENλ·�Ʊ��


    End Function

    ''' <summary>
    ''' ��״̬��ʮ������ת����ʮ����ֵ,���������ֶεĺ���,2012��5��24�յ��ƵĽڵ�����Ϊ6���ֽڣ�12λ��ѹ
    ''' 10λ������14λ���ʣ�4λ��������
    ''' </summary>
    ''' <param name="inf_list">״̬������</param>
    ''' <remarks></remarks>
    Public Sub Explain_State_String_AD6(ByVal inf_list() As String, ByVal state_id As Integer)
        Dim state_string As String
        Dim presurevalue As String
        Dim currentvalue As String
        Dim powervalue As String
        Dim information As String
        Dim yinshuvalue As Double

        state_string = inf_list(3 + state_id * 6) & inf_list(4 + state_id * 6) & inf_list(5 + state_id * 6) & inf_list(6 + state_id * 6) & inf_list(7 + state_id * 6)  '����ֽڵ�״̬
        If state_string = "FFFFFFFFFF" Then
            g_presurevalue = 0
            g_currentvalue = 0
            g_powervalue = 0
            g_information = 2
            g_yinshuvalue = 0
            Exit Sub
        End If
        '��ʮ�����Ƶ�����ת���ɶ������ַ���
        state_string = Com_inf.HEX_to_BIN(state_string)
        presurevalue = Mid(state_string, 1, 12)  '12λ�ĵ�ѹ
        currentvalue = Mid(state_string, 13, 10)  ''10λ�ĵ���
        powervalue = Mid(state_string, 23, 14)  '14λ�Ĺ���
        information = Mid(state_string, 37, 4) '4λ��״̬

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
        g_yinshuvalue = Format(yinshuvalue, "0.00")  '��������

        ' Dim i As Integer = System.Convert.ToInt32(Com_inf.BIN_to_HEX(currentvalue), 16)

        'g_dianzuad = System.Convert.ToInt32(inf_list(3), 16).ToString   '��4���ֽ���Ϊ�жϿ��صı�־
        'g_currentad = System.Convert.ToInt32(inf_list(3) & inf_list(4), 16).ToString  '��4��5���ֽ�ת��Ϊ2λ����
        'g_information = System.Convert.ToInt32(inf_list(5), 16).ToString  '��6���ֽ�ת��Ϊ2λ��Ԥ��
    End Sub

    ''' <summary>
    ''' ��״̬��ʮ������ת����ʮ����ֵ,���������ֶεĺ���,2011��5��19�����ӵ���ADΪ2���ֽڣ�ȡ������ADֵ
    ''' </summary>
    ''' <param name="inf_list">״̬������</param>
    ''' <remarks></remarks>
    Public Sub Explain_State_String_AD2(ByVal inf_list() As String)
        'Dim short_str As String
        'Dim inf_list() As String
        'inf_list = str.Split(" ")
        'short_str = Val("&H" & (inf_list(6) & inf_list(0))).ToString   '��7���ֽڲ��ϵ�1���ֽ�ת��Ϊ��λ��ʮ���Ƶ������
        'While short_str.Length < 4
        '    short_str = "0" & short_str '������Ų���4λ��0����
        'End While
        'g_controlboxid = short_str
        'g_lampidstring = g_controlboxid & Control_Hex_to_Dec(inf_list(1) & inf_list(2))   '��2��3���ֽ�ת����2+LAMP_ID_LEN���ȵ�·�Ʊ��
        g_dianzuad = System.Convert.ToInt32(inf_list(3), 16).ToString   '��4���ֽ���Ϊ�жϿ��صı�־
        g_currentad = System.Convert.ToInt32(inf_list(3) & inf_list(4), 16).ToString  '��4��5���ֽ�ת��Ϊ2λ����
        g_information = System.Convert.ToInt32(inf_list(5), 16).ToString  '��6���ֽ�ת��Ϊ2λ��Ԥ��
    End Sub
    ''' <summary>
    ''' ��״̬��ʮ������ת����ʮ����ֵ,���������ֶεĺ���
    ''' </summary>
    ''' <param name="inf_list">״̬������</param>
    ''' <remarks></remarks>
    Public Sub Explain_State_String(ByVal inf_list() As String)
        'Dim short_str As String
        'Dim inf_list() As String
        'inf_list = str.Split(" ")
        'short_str = Val("&H" & (inf_list(6) & inf_list(0))).ToString   '��7���ֽڲ��ϵ�1���ֽ�ת��Ϊ��λ��ʮ���Ƶ������
        'While short_str.Length < 4
        '    short_str = "0" & short_str '������Ų���4λ��0����
        'End While
        'g_controlboxid = short_str
        'g_lampidstring = g_controlboxid & Control_Hex_to_Dec(inf_list(1) & inf_list(2))   '��2��3���ֽ�ת����2+LAMP_ID_LEN���ȵ�·�Ʊ��
        g_dianzuad = System.Convert.ToInt32(inf_list(3), 16).ToString   '��4���ֽ�ת��Ϊ2λ����
        g_currentad = System.Convert.ToInt32(inf_list(4), 16).ToString  '��5���ֽ�ת��Ϊ2λ����
        g_information = System.Convert.ToInt32(inf_list(5), 16).ToString  '��6���ֽ�ת��Ϊ2λ��Ԥ��
    End Sub

    Public Function getstate_lampid(ByVal inf_list() As String) As String
        Dim short_str As String
        Dim lamp_kind As String
        Dim lamp_id As String
        If inf_list.Length = 7 Then
            short_str = Val("&H" & (inf_list(6) & inf_list(0))).ToString   '��7���ֽڲ��ϵ�1���ֽ�ת��Ϊ��λ��ʮ���Ƶ������
            While short_str.Length < 4
                short_str = "0" & short_str '������Ų���4λ��0����
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
            g_lampidstring = g_controlboxid & lamp_kind & lamp_id  '��2��3���ֽ�ת����2+LAMP_ID_LEN���ȵ�·�Ʊ��

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
            g_lampidstring = g_controlboxid & lamp_kind & lamp_id  '��2��3���ֽ�ת����2+LAMP_ID_LEN���ȵ�·�Ʊ��

        End If

        getstate_lampid = g_lampidstring
    End Function

    ''' <summary>
    '''  '�����������ӳ�������
    ''' </summary>
    ''' <param name="city_combox">����������</param>
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
        city_combox.Items.Clear()  '�������������
        sql = "select * from city "
        rs = DBOperation.SelectSQL(conn, sql, msg)
        While rs.EOF = False
            city_combox.Items.Add(Trim(rs.Fields("city").Value))  '���ӳ�������
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
    '''  '��������������������
    ''' </summary>
    ''' <param name="city_combox">��������������</param>
    ''' <param name="area_combox">��������</param>
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
        area_combox.Items.Clear()  '�������������
        rs = DBOperation.SelectSQL(conn, sql, msg)
        While rs.EOF = False
            area_combox.Items.Add(rs.Fields("area").Value) '��������������
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
    ''' ���ӽֵ�����
    ''' </summary>
    ''' <param name="city_combox">��������������</param>
    ''' <param name="area_combox">������������</param>
    ''' <param name="street_combox">�ֵ�����������</param>
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
                street_combox.Items.Add(Trim(rs.Fields("street").Value))  '���ӽֵ������������
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
    ''' ѡ�����ͱ��
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
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Select_type_id" & " ʱ�䣺" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

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
    ''' ���۵Ƶ���������
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
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Select_type_name" & " ʱ�䣺" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            Exit Sub
        End If

        type_combox.Items.Clear()
        If rs.RecordCount > 0 Then 'ѡ���һ���Ƶ����ͱ��
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
    ''' ѡ���������ƣ�����������ƣ�
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
        box_id_combox.Items.Clear()  '��յ��������������


        sql = "select * from control_box"
        rs = DBOperation.SelectSQL(conn, sql, msg)

        While rs.EOF = False
            box_id_combox.Items.Add(Trim(rs.Fields("control_box_name").Value))  '���ӵ���������������
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
    ''' ���������
    ''' </summary>
    ''' <param name="city_combox">��������</param>
    ''' <param name="area_combox">������</param>
    ''' <param name="street_combox">�ֵ�����</param>
    ''' <param name="box_name_combox">�������ƣ�����䣩</param>
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
        box_name_combox.Items.Clear()  '��յ��������������
        sql = "select distinct(control_box_name),control_box_id from control_inf where street='" & street_name & "'and area='" & area_name & "' and city='" & city_name & "' order by control_box_id"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        While rs.EOF = False
            box_name_combox.Items.Add(Trim(rs.Fields("control_box_name").Value))  '���ӵ���������������
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
    ''' �������ͼ����ѡ��Ƶı��
    ''' </summary>
    ''' <param name="box_id_combox">��������</param>
    ''' <param name="lamp_type_combox">����</param>
    ''' <param name="lamp_id_combox">�ƵĶ������</param>
    ''' <param name="lamp_id_start">�Ƶı�ŵ���ʼ��λ</param>
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
        lamp_id_combox.Items.Clear()  '��·�Ʊ���������������
        sql = "select * from lamp_street where control_box_name='" & Trim(box_id_combox.Text) & "' and type_string='" & Trim(lamp_type_combox.Text) & "' order by lamp_id"

        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Select_lamp_id_type" & " ʱ�䣺" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If

        If rs.RecordCount > 0 Then
            lamp_id_start.Text = Mid(Trim(rs.Fields("lamp_id").Value), 1, 6)
            While rs.EOF = False
                lamp_id_combox.Items.Add(Mid(Trim(rs.Fields("lamp_id").Value), 7, LAMP_ID_LEN))  '����·�Ʊ���������е�����
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
    ''' ѡ��Ƶı�ţ�ԭʼ�汾��Ŀǰ���ã�
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
        lamp_id_combox.Items.Clear()  '��·�Ʊ���������������

        If lamp_all = 0 Then  '���ѡ���·���з�Χ����
            sql = "select * from lamp_inf where control_box_id='" & Trim(box_id_combox.Text) & "'"

        Else  '·�Ʊ���޷�Χ���ƣ�ѡ������
            sql = "select * from lamp_inf where control_box_id='" & Trim(box_id_combox.Text) & "'"

        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)

        If rs.RecordCount > 0 Then
            While rs.EOF = False
                lamp_id_combox.Items.Add(Trim(rs.Fields("lamp_id").Value))  '����·�Ʊ���������е�����
                rs.MoveNext()
            End While
        End If

        rs.Close()
        rs = Nothing
        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    ''' ѡ����������
    ''' </summary>
    ''' <param name="control_box_name_combobox">���������������</param>
    ''' <remarks></remarks>
    Public Sub Select_control_box_all(ByVal control_box_name_combobox As System.Windows.Forms.ComboBox)
        '���������������
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
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Select_control_box_all" & " ʱ�䣺" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

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
    ''' ��������ţ����۵�����͵ƵĶ��������ϳ�һ��������·�Ʊ��
    ''' </summary>
    ''' <param name="control_box_name">���������</param>
    ''' <param name="lamp_type">����</param>
    ''' <param name="lamp_id">�ƵĶ������</param>
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
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Com_lamp_id_all" & " ʱ�䣺" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

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
                    MsgBox("�Ƶı�ų������ֵ", , PROJECT_TITLE_STRING)
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
    ''' ��ԭʼ��·�Ʊ�ţ�ת���ɵ��������+���۵�����+���۵Ʊ��
    ''' </summary>
    ''' <param name="lamp_id">�Ƶı��</param>
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
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "lamp_id_to_detail" & " ʱ�䣺" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If

        If rs.RecordCount > 0 Then
            g_controlboxname = Trim(rs.Fields("control_box_name").Value) '���������
            g_lamptype = Trim(rs.Fields("type_string").Value) '���۵�����
            g_shortlampid = Mid(lamp_id, 7, LAMP_ID_LEN) '���۵Ʊ��

        End If
        rs.Close()
        rs = Nothing
        conn.Close()
        conn = Nothing

    End Sub
    ''' <summary>
    ''' ��������ADֵת����ʵ�ʵ���ֵ�����ƣ�,2011��5��19�����ӵ���ADΪ2���ֽ�
    ''' </summary>
    ''' <param name="current_AD"></param>
    ''' <remarks></remarks>
    Public Sub Get_current_and_presure_AD2(ByVal current_AD As Integer)
        '******************�°�����·״̬����Э��*************************
        g_currentvalue = (current_AD) / &HFFFF / 0.6 * 5
        g_presurevalue = 0
        g_currentvalue = Format(g_currentvalue, "0.00")
        g_presurevalue = Format(g_presurevalue, "0.00")


    End Sub

    ''' <summary>
    ''' �������͵�ѹ��ADֵת����ʵ�ʵ���ֵ�����ƣ�
    ''' </summary>
    ''' <param name="current_AD"></param>
    ''' <param name="presure_AD"></param>
    ''' <remarks></remarks>
    Public Sub Get_current_and_presure(ByVal current_AD As Integer, ByVal presure_AD As Integer)
        '�Ͳ�����Ŀ�е���ֵΪ��������
        Dim PI As Double  'PIֵ

        PI = 3.1415926
        If current_AD = 0 Then
            g_presurevalue = 0
        Else
            g_presurevalue = System.Math.Abs(System.Math.Cos(presure_AD * 0.5 Mod 5 * PI / 2))

        End If

finish1:
        '����ֵת��

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
    '''' �ر�EXCEL����
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
    ''' �ж����ݿ������Ƿ�
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DB_Connection() As System.Boolean
        Dim conn As New ADODB.Connection
        DB_Connection = DBOperation.OpenConn(conn)
    End Function

    ''' <summary>
    ''' �ɵƵı�Ż�ȡ�Ƹ˺�
    ''' </summary>
    ''' <param name="lamp_id">·�Ʊ��</param>
    ''' <remarks></remarks>
    Public Sub Get_DengGan(ByVal lamp_id As String)
        g_dengzhuid = (System.Convert.ToInt32(Mid(lamp_id, 7, LAMP_ID_LEN)) - 1) \ g_lampnum + 1
        g_dengzhulampid = System.Convert.ToInt32(Mid(lamp_id, 7, LAMP_ID_LEN)) Mod g_lampnum
        If g_dengzhulampid = 0 Then
            g_dengzhulampid = g_lampnum
        End If
    End Sub

    ''' <summary>
    ''' ����ѹ�����û��������ֽڵ���ֵ
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
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Get_Presure" & " ʱ�䣺" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

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
            '2011-4-21�Ÿ��Ĺ�ʽ
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
    ''' �����������û��������ֽڵ���ֵ
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
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Get_Presure" & " ʱ�䣺" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

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
            '2011-4-21�Ÿ��Ĺ�ʽ
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
    ''' ת���ɵ�ѹ�Ĺ�ʽ(������)
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
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Get_Presure" & " ʱ�䣺" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

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
            '2011-4-21�Ÿ��Ĺ�ʽ
            Get_Presure = Presure * 500 / &HFFFF

        End If

        '2012��1��11�յ�ѹС��50������Ϊ0
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
    ''' �����Ĵη��ֹ������ͱ�������
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
        Dim Lampidstring As String  '�Ƶı��
        Dim phonenum As String  '�绰����
        Dim control_box_name As String = "" '����������
        Dim type As String = "alarm"

        '�����150���ַ�
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
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Send_Msg" & " ʱ�䣺" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs.RecordCount > 0 Then
            IMEIstring = rs.Fields("IMEI").Value
            control_box_name = Trim(rs.Fields("control_box_name").Value)
        End If
        If lamp_id <> "" Then
            '���ƹ���
            Lampidstring = Val(Mid(lamp_id, 1, 4)).ToString & " . " & Val(Mid(lamp_id, 5, 2)).ToString & " . " & Val(Mid(lamp_id, 7, LAMP_ID_LEN)).ToString & "  " & alarm_string
        Else
            '���������
            Lampidstring = control_box_name & " " & alarm_string
        End If

        sql = "select phonenum from contact where control_box_name='" & type & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Send_Msg" & " ʱ�䣺" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        While rs.EOF = False
            phonenum = Trim(rs.Fields("phonenum").Value)
            '����è
            sql = "insert into SendGSM_Modem (RoadIMEI, PhoneNumber, SendContent, HandlerFlag, CreateTime)" _
                       & "values('" & IMEIstring & "' , '" & phonenum & "' , '" & Lampidstring & "', 0 , '" & Now & "')"

            'GPRSֱ�ӷ���
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
    ''' �������ͱ��ȡ��������
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
    ''' ���������ؽڵ�ķ���ת���з����絥�ƿ�����򿪽бպϣ����Ʊջ�رսжϿ�
    ''' </summary>
    ''' <param name="control_string"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Change_Controlboxcontrol(ByVal control_string As String) As String
        Change_Controlboxcontrol = ""
        If control_string = "���Ʊ�" Then
            Change_Controlboxcontrol = "�Ͽ�"
        End If
        If control_string = "���ƿ�" Then
            Change_Controlboxcontrol = "����"
        End If
        If control_string = "�ر�" Then
            Change_Controlboxcontrol = "�Ͽ�"
        End If
        If control_string = "��" Then
            Change_Controlboxcontrol = "����"
        End If
        If control_string = "��" Then
            Change_Controlboxcontrol = "�Ͽ�"
        End If
        If control_string = "��" Then
            Change_Controlboxcontrol = "����"
        End If
    End Function

    ''' <summary>
    ''' ����ǰʱ���·� 2011��2��16�������ꡢ�¡���
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
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "sendTime" & " ʱ�䣺" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

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
    '''  ���ݵƵ����ͽ��Ƶ�ʮ���Ʊ��ת����2���ƣ�����˼���ǣ�0���͵ĵƱ�ſ��Ա������20000���ƺ�
    ''' 1-10���͵ĵƺ�ͣ�ã�11-31���͵ĵƺ�����ʹ��2000�����
    ''' </summary>
    ''' <param name="type_string">���͵ı��</param>
    ''' <param name="lamp_id">�Ƶı��</param>
    ''' <remarks></remarks>
    Public Function Get_lampid_bin(ByVal type_string As String, ByVal lamp_id As Integer) As String
        Dim type_id As Integer
        type_id = type_string

        If type_id = 0 Then
            If lamp_id > LAMP_ID_MAX Then
                MsgBox("0���ͽڵ��Ų����Դ���" & LAMP_ID_MAX, , PROJECT_TITLE_STRING)
                Get_lampid_bin = ""
                Exit Function
            End If
            Get_lampid_bin = Com_inf.Dec_to_Bin(lamp_id, 16)

        Else
            'If type_id >= 1 And type_id <= 10 Then
            '    MsgBox("1-10���ͽڵ��Ų�����ʹ��", , PROJECT_TITLE_STRING)
            '    Get_lampid_bin = ""
            '    Exit Function
            'End If
            If lamp_id > 2000 Then
                MsgBox("11-31���ڵ��ų���2000", , PROJECT_TITLE_STRING)
                Get_lampid_bin = ""
                Exit Function
            End If
            Get_lampid_bin = Com_inf.Dec_to_Bin(type_id, 5) & Com_inf.Dec_to_Bin(lamp_id, 11)
        End If


    End Function

    ''' <summary>
    ''' ����ǰ�Ĳ����ַ������ӵ���¼���Ա��ѯ
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
    ''' ���ݵ����Ʊ�ţ���ȡ���������
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
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Get_box_name" & " ʱ�䣺" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

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
    ''' ���ݵ�������ƣ���ȡ�����Ʊ��
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
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Get_box_id" & " ʱ�䣺" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

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
    ''' �������ͱ�ţ���ȡ��������
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
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Get_lamp_type" & " ʱ�䣺" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

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
    ''' ��¼ͨ��״̬�͹����Ƿ�����������һ��ʱ��Σ�������һ��ʱ���
    ''' </summary>
    ''' <param name="control_box_name">����������</param>
    ''' <param name="state">��¼��״̬</param>
    ''' <param name="inf" >��¼�����ࣺͨ�ţ����磬״̬������</param>
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
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Communication_Record" & " ʱ�䣺" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs.RecordCount > 1 Then
            'żȻ����������ͬ��¼
            id = rs.Fields("ID").Value
            sql = "delete from control_box_state where control_box_name='" & control_box_name & "' and state='0' and kaiguan_string='" & inf & "' and ID<>'" & id & "'"
            DBOperation.ExecuteSQL(conn, sql, msg)
        End If

        If rs.RecordCount > 0 Then
            If Trim(state) = "" Then
                'ͨ�Ų�������ʱ��
                sql = "update control_box_state set state='1', StatusContent2='" & Now.ToString & "' where control_box_name='" & control_box_name & "' and state='0' and kaiguan_string='" & inf & "'"
                DBOperation.ExecuteSQL(conn, sql, msg)
            Else
                If Trim(state) = "δ����" Then
                    'ͨ�Ų�������ʱ��
                    sql = "update control_box_state set state='1', StatusContent2='" & Now.ToString & "' where control_box_name='" & control_box_name & "' and state='0' and kaiguan_string<>'" & inf & "'"
                    DBOperation.ExecuteSQL(conn, sql, msg)
                End If

                '���Ŀǰͨ��������ԭ����״̬��ͨ�Ų������ģ���Ҫ�����в�һ��
                'If Trim(state) = "ͨ������" Then
                '    communication_string = Trim(rs.Fields("StatusContent").Value)
                '    If Trim(state) <> communication_string Then

                '    End If

                'End If


                If Trim(rs.Fields("StatusContent").Value) <> Trim(state) Then

                    '�ҵ�δ������״̬���ݣ���״̬��һ�£����޸�״̬��־λ���½�һ��״̬����
                    'rs.Fields("state").Value = 1
                    'Dim s As String = Trim(rs.Fields("StatusContent").Value)
                    'rs.Fields("StatusContent2").Value = Now.ToString 'StatusContent2��¼����ʱ��
                    'rs.Update()
                    sql = "update control_box_state set state='1', StatusContent2='" & Now.ToString & "' where control_box_name='" & control_box_name & "' and state='0' and kaiguan_string='" & inf & "'"
                    DBOperation.ExecuteSQL(conn, sql, msg)

                    '����һ����¼
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
            'û�����״̬������
            '����һ����¼
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
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Select_box_id" & " ʱ�䣺" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

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
    ''' ��һ���ַ����л�ȡ���е�����
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
    ''' ѡ������������ƣ�������ѡ���������
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
    ''' ���ýڵ��ѡ�б�־
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
    ''' ѡ��ѡ�еĶ������ƣ����ӵ�array_list�У�����¼����
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
    ''' �����������Ƽ�¼���б���
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
    ''' �����ƶ�������Ƽ�¼���б���
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
                '���ӿ��ƶ��������
                row = dgv_box.RowCount
                dgv_box.Rows.Add()
                dgv_box.Rows(i).Cells(rowname).Value = array_objname(0)
                dgv_box.Rows(i).Cells(control_level).Value = array_objname(1)
            End If
            i += 1
        End While

    End Sub

    ''' <summary>
    ''' ��ѯ����ģʽ���������Ƿ��������䴦�ڽڼ���ģʽ�������־Ϊholiday,û�����־Ϊnormal
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Set_holiday_mod()
        Dim conn As New ADODB.Connection
        Dim rs As New ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim time As DateTime
        Dim control_box_id As String
        Dim mod_tag As String = "����"

        msg = ""


        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If


        '������������Ľڼ��ձ�־λ��λΪ����
        sql = "update control_box set elec_state='" & mod_tag & "'"
        DBOperation.ExecuteSQL(conn, sql, msg)

        sql = "SELECT Special_div_time.Time, Special_road_level.control_box_id FROM  Special_div_time INNER JOIN Special_road_level ON Special_div_time.name = Special_road_level.div_time_level"

        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Get_lamp_type" & " ʱ�䣺" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        While rs.EOF = False
            time = rs.Fields("Time").Value
            control_box_id = Trim(rs.Fields("control_box_id").Value)
            mod_tag = "����"
            If time.Year = Now.Year And time.Month = Now.Month And time.Day = Now.Day Then
                mod_tag = "����"
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
    ''' ���ݵ����Ʊ�ţ���ȡ������Ƿ��ڽڼ��տ���ģʽ
    ''' </summary>
    ''' <param name="control_box_id"></param>
    ''' <remarks></remarks>
    Public Function Get_box_holidaymod(ByVal control_box_id As String) As Boolean
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String
        Dim holiday_tag As String = "����"

        msg = ""
        Get_box_holidaymod = False
        sql = "select elec_state from control_box where control_box_id='" & control_box_id & "' and elec_state='" & holiday_tag & "'"
        If DBOperation.OpenConn(conn) = False Then
            Get_box_holidaymod = False
            Exit Function
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Get_box_name" & " ʱ�䣺" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

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
    ''' ͨ����������ƺͻ�·��Ų�ѯ��Ӧ�ĽӴ������
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
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Get_box_name" & " ʱ�䣺" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

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
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Get_box_name" & " ʱ�䣺" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)
            get_jiechuqi_state = ""
            conn.Close()
            conn = Nothing
            Exit Function
        End If

        get_jiechuqi_state = ""
        While rs.EOF = False
            jiechuqi_id = Val(Mid(Trim(rs.Fields("lamp_id").Value), 7, LAMP_ID_LEN)).ToString
            state = "�Ͽ�"
            If rs.Fields("state").Value = 0 Or rs.Fields("state").Value = 3 Then
                '��ʾ�Ͽ�
                state = "�Ͽ�"
                'sql = "update huilu_inf set open_close='" & state & "' where control_box_name='" & Trim(rs.Fields("control_box_name").Value) & "' and jiechuqi_id='" & jiechuqi_id & "'"
                'DBOperation.ExecuteSQL(conn, sql, msg)

            End If
            If rs.Fields("state").Value = 1 Or rs.Fields("state").Value = 4 Then
                '��ʾ�Ͽ�
                state = "����"
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
    ''' ȡ��ǰ������ĽӴ���״̬�����õ�huilu_inf����
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
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Get_box_name" & " ʱ�䣺" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        While rs.EOF = False
            jiechuqi_id = Val(Mid(Trim(rs.Fields("lamp_id").Value), 7, LAMP_ID_LEN)).ToString
            If rs.Fields("state").Value = 0 Or rs.Fields("state").Value = 3 Then
                '��ʾ�Ͽ�
                state = "�Ͽ�"
                sql = "update huilu_inf set open_close='" & state & "' where control_box_name='" & Trim(rs.Fields("control_box_name").Value) & "' and jiechuqi_id='" & jiechuqi_id & "'"
                DBOperation.ExecuteSQL(conn, sql, msg)

            End If
            If rs.Fields("state").Value = 1 Or rs.Fields("state").Value = 4 Then
                '��ʾ�Ͽ�
                state = "����"
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
    ''' ��ȡ�������ģʽ�Ŀ�������
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
            '��ҹģʽ
            sql = "select distinct div_level from div_time where hour_end=0"
        Else
            '����ģʽ
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
    ''' ������ż��������ţ���ȡ��ң��������
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
    ''' ��RoadLightFlag����֮ǰ������δ����Ϊ��״̬��������������λ
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
    ''' ����lamp_id��ȡ���Ƶ�Э�����ͣ�����1�ֽڣ�2�ֽڼ�6�ֽڵ�
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
            getlampprotocletype = "-1"  '��ʾû�����
        End If

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Function

    ''' <summary>
    ''' �п�������󣬽�״̬����δ����Ŀ���������ң������Ϊ�Ѵ���״̬��
    '''��ֹ�п���֮ǰ�����ݶԹ����ж���ɸ���
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
    ''' ��ʱ�ο����ַ���д�뵽control_box_state����
    ''' </summary>
    ''' <param name="divtime_state"></param>
    ''' <remarks></remarks>
    Public Sub insert_state(ByVal divtime_state As String, ByVal control_box_name As String)
        Dim sql As String
        Dim msg As String
        Dim conn As New ADODB.Connection
        Dim rs As New ADODB.Recordset
        Dim state_string As String = "ʱ��"
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
            rs.Fields("kaiguan_string").Value = "ʱ��"
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
        Dim state_string As String = "ʱ��"

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
    ''' ����������ת������������
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
            boxid = Val("&H" & (data(i * 11 + 3) & data(i * 11 + 4))).ToString   '��4���ֽڲ��ϵ�5���ֽ�ת��Ϊ��λ��ʮ���Ƶ������
            short_str = Control_Hex_to_Dec(data(i * 11 + 5) & data(i * 11 + 6))
            lampid_string = boxid & "-" & Val(Mid(short_str, 1, 2)).ToString & "-" & Val(Mid(short_str, 3, LAMP_ID_LEN)).ToString

            If data(i * 11 + 7) = "1B" Then
                '��·��
                control_string = "��·��"
            End If
            If data(i * 11 + 7) = "1C" Then
                '��·��
                control_string = "��·��"
            End If
            If data(i * 11 + 7) = "41" Then
                '��·��
                control_string = "����ȫ��"
            End If
            If data(i * 11 + 7) = "42" Then
                '��·��
                control_string = "����ȫ��"
            End If
            If data(i * 11 + 7) = "43" Then
                '��·��
                control_string = "�����濪"
            End If
            If data(i * 11 + 7) = "45" Then
                '��·��
                control_string = "����ż��"
            End If

            controlorder_string &= time_string & " " & lampid_string & " " & control_string & vbCrLf
            i += 1
        End While

    End Function

    ''' <summary>
    ''' ��ȡ���õľ��ȵ�ֵ
    ''' </summary>
    ''' <remarks></remarks>
    Public Function get_jingduvalue() As Double
        Dim conn As New ADODB.Connection
        Dim sql, msg As String
        Dim rs As New ADODB.Recordset
        Dim type As String = "����"
        get_jingduvalue = 118.03  'Ĭ�����Ϊ�Ͳ�����

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
    ''' ��ȡ���õ�γ�ȵ�ֵ
    ''' </summary>
    ''' <remarks></remarks>
    Public Function get_weiduvalue() As Double
        Dim conn As New ADODB.Connection
        Dim sql, msg As String
        Dim rs As New ADODB.Recordset
        Dim type As String = "γ��"
        get_weiduvalue = 36.48  'Ĭ�����Ϊ�Ͳ�γ��

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
    ''' ����ÿ����ճ������
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub set_suntime()
        Dim bLeap As Boolean '�Ƿ�Ϊ����
        Dim suntimeobj As New SunTime
        Dim month() As Integer  'ÿ�µ�����
        Dim month_num As Integer '�·�
        Dim row As Integer  '�б���к���
        Dim risetime, downtime As DateTime  '�ճ�������ʱ��
        Dim i As Integer
        Dim sql As String
        Dim msg As String
        Dim conn As New ADODB.Connection
        Dim nowtime As DateTime = Now  '��ǰ��ʱ��
        Dim jingdu, weidu As Double '��γ��
        Dim now_year As Integer
        now_year = nowtime.Year
        Dim mothList() As Integer = {31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31}  '������
        Dim leapList() As Integer = {31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31}  '����

        jingdu = get_jingduvalue()
        weidu = get_weiduvalue()

        DBOperation.OpenConn(conn)
        msg = ""

        bLeap = suntimeobj.IsLeapYear(nowtime.Year)
        If bLeap = True Then  '����
            month = leapList
        Else   '������
            month = mothList
        End If
        'ɾ��ԭ�ȵ��ճ������
        sql = "delete from suntime"
        DBOperation.ExecuteSQL(conn, sql, msg)
        month_num = 0 '��ʼ����1�¿�ʼ

        i = 0

        While month_num < 12
            row = 0
            i = 0
            While row < month(month_num)

                risetime = suntimeobj.GetSunrise(weidu, jingdu, now_year, month_num + 1, row + 1)
                downtime = suntimeobj.GetSunset(weidu, jingdu, now_year, month_num + 1, row + 1)
                i += 1
                row += 1

                '����ǰ��ѯ���ճ�����ʱ���¼�����ݿ��У���Ϊʱ�صĻ���ʱ��
                sql = "insert into suntime(time,mod) values ('" & risetime & "', '��')"  '��¼�ص�ʱ��
                DBOperation.ExecuteSQL(conn, sql, msg)

                sql = "insert into suntime(time,mod) values ('" & downtime & "', '��')"  '��¼����ʱ��
                DBOperation.ExecuteSQL(conn, sql, msg)
            End While
            month_num += 1
        End While

        '���޸ľ�γ�ȵĲ�����¼��¼�����ݿ���
        Com_inf.Insert_Operation("�޸��ճ������ʱ�䣺" & now_year & ", ���ȣ�" & jingdu.ToString & " ,γ�ȣ�" & weidu.ToString)


        conn.Close()
        conn = Nothing
    End Sub

    Public Function get_alarmdelaytime() As String
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String
        Dim setvalue As String = "������ʱ"

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
        Dim conn As New ADODB.Connection  '���ݿ�����
        DBOperation.OpenConn(conn)

        msg = ""
        sql = "select * from lamp_type order by type_id"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Select_all_lamptype" & " ʱ�䣺" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

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
    ''' �ж��Ƿ����п���Ȩ�޵ĵ绰����
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
    ''' ��ȡ���������
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

        '���õ��������Ƿ�Ϊ�Զ�����
        sql = "select * from sysconfig where type='�Զ�����'"

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


        '���½��г���
        sql = "select * from sysconfig where type='������'"
       
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

        '��ÿ����г���
        sql = "select * from sysconfig where type='������'"

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
    ''' ģʽ���ʱ��ɾ�����е�ģʽ����
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Del_all_inf(ByVal box_name As String)

        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim i As Integer = 0
        Dim control_box_id As String  '��������
        Dim mod_string As String = ""  'ģʽ����

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

        '����·���ý������
        '��γ�����
        sql = "delete from pianyi where lamp_id like '" & control_box_id & "%'"
        DBOperation.ExecuteSQL(conn, sql, msg)

        '����������������������ģʽɾ��
        sql = "delete from road_level where control_box_id='" & control_box_id & "'"
        DBOperation.ExecuteSQL(conn, sql, msg)

        '��������������нڼ��տ���ģʽɾ��
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
    ''' ��ȡ������״̬���Ƿ���ʱ�ο���
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
                If Trim(rs.Fields("Information").Value) = "�ر�" Then
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
        sql = "select * from sysconfig where type='����ʱ��'"
        DBOperation.OpenConn(conn)
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs.RecordCount > 0 Then
            g_textbox_time_value = System.Convert.ToDouble(Trim(rs.Fields("name").Value))
        End If
        conn.Close()
        conn = Nothing
    End Function

End Module
