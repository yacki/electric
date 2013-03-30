''' <summary>
''' ���ݿ⺯��
''' </summary>
''' <remarks></remarks>
''' 
Public Class DBOperation
    Shared conn_err As Integer = 1
    ''' <summary>
    '''  '�õ����ݿ������ַ������û������ڴ����������ַ���
    ''' </summary>
    ''' <returns>�����ַ���</returns>
    ''' <remarks></remarks>
    Shared Function GetConnStr() As String
        'User ID�����ݿ��û�ID��Password�ǵ�¼����
        'Initial Catalog�����ݿ�����Data Source�Ƿ���������
        'GetConnStr = "Provider=SQLOLEDB.1;Persist Security Info=True;" & "User ID='" + Com_inf.server_user + "';Password='" + Com_inf.server_password + "';Initial Catalog=streetlamp;Data Source='" + Com_inf.server + "'"
        'GetConnStr = System.Configuration.ConfigurationManager.ConnectionStrings("streetlamp.Logion").ConnectionString

        Dim config As Configuration.Configuration
        config = System.Configuration.ConfigurationManager.OpenExeConfiguration(Configuration.ConfigurationUserLevel.None)

        GetConnStr = config.ConnectionStrings.ConnectionStrings("streetlamp.Login").ConnectionString

    End Function
    ''' <summary>
    '''   '�����ݿ����ӣ�
    ''' </summary>
    ''' <param name="Conn"></param>
    ''' <returns>���ӳɹ�����true,������false</returns>
    ''' <remarks></remarks>
    Shared Function OpenConn(ByRef Conn As ADODB.Connection) As Boolean

        '  Conn = New ADODB.Connection
        On Error GoTo ErrorHandle '������
        Conn.Open(GetConnStr) '�����ݿ�����
        OpenConn = True
        Exit Function
ErrorHandle:
        'MsgBox("�������ݿ�ʧ�ܣ������µ�¼��", , PROJECT_TITLE_STRING)
        g_welcomewinobj.SetTextDelegate("�������ݿ�ʧ��" & " ʱ�䣺" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

        OpenConn = False
        ' End
        'Exit Function
    End Function
    ''' <summary>
    '''  'ִ��SQL��䣬����UPDATA��DELETE��INSERT���
    ''' </summary>
    ''' <param name="conn"></param>
    ''' <param name="SQL"></param>
    ''' <param name="msg"></param>
    ''' <remarks></remarks>
    Shared Sub ExecuteSQL(ByVal conn As ADODB.Connection, ByVal SQL As String, ByRef msg As String)
        ' Dim rst As Object

        'Dim Conn As ADODB.Connection
        Dim sTokens() As String
        ' Conn = New ADODB.Connection
        On Error GoTo ErrorHandle '������
        '�ж�SQL����ǲ���UPDATE��SELECT��INSERT���
        sTokens = Split(SQL) '����Split �������SQL���
        If InStr("DELETE", UCase(sTokens(0))) Then
            '�����ݿ�
            If conn.State = ConnectionState.Open Then '����򿪳ɹ�,��ִ��SQL
                conn.Execute(SQL)
                msg = sTokens(0) & "ִ�гɹ���"
            Else
                msg = sTokens(0) & "ִ��ʧ�ܣ�"
            End If
            msg = msg & SQL
        End If
        If InStr("INSERT", UCase(sTokens(0))) Then
            '�����ݿ�
            If conn.State = ConnectionState.Open Then '����򿪳ɹ�,��ִ��SQL
                conn.Execute(SQL)
                msg = sTokens(0) & "ִ�гɹ���"
            Else
                msg = sTokens(0) & "ִ��ʧ�ܣ�"
            End If
            msg = msg & SQL
        End If
        If InStr("UPDATE", UCase(sTokens(0))) Then
            '�����ݿ�
            If conn.State = ConnectionState.Open Then '����򿪳ɹ�,��ִ��SQL
                conn.Execute(SQL)
                msg = sTokens(0) & "ִ�гɹ���"
            Else
                msg = sTokens(0) & "ִ��ʧ�ܣ�"
            End If
            msg = msg & SQL
        End If

Finally_Exit:  '���������ʱ����ж�������ٹ���
        ' rst = Nothing
        'Conn.Close()
        'Conn = Nothing
        ' ExecuteSQL = Nothing
        Exit Sub
ErrorHandle:  '���SQL���ִ�г�������ʾ������Ϣ��ת��Finally_Exit
        msg = "ִ�д���" & Err.Description
        Resume Finally_Exit

    End Sub

    ''' <summary>
    ''' 'ִ��SQL��䣬
    ''' </summary>
    ''' <param name="conn"></param>
    ''' <param name="SQL"></param>
    ''' <param name="msg"></param>
    ''' <returns>����ADODB.Recordset</returns>
    ''' <remarks></remarks>
    Shared Function SelectSQL(ByVal conn As ADODB.Connection, ByVal SQL As String, ByRef msg As String) As ADODB.Recordset
        Dim MsgString As Object

        ' Dim Conn As ADODB.Connection
        Dim rst As ADODB.Recordset
        Dim sTokens() As String
        ' Conn = New ADODB.Connection
        ' Conn = System.DBNull.Value
        On Error GoTo ErrorHandle '������
        '�ж�SQL���
        sTokens = Split(SQL) '����Split �������SQL���
        If InStr("SELECT", UCase(sTokens(0))) Then '�����SELECT���
            If conn.State = ConnectionState.Open Then
                rst = New ADODB.Recordset
                rst.CursorLocation = ADODB.CursorLocationEnum.adUseClient
                'ִ�в�ѯ����
                rst.Open(Trim(SQL), conn, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                SelectSQL = rst
                msg = "��ѯ��" & rst.RecordCount & "����¼��"
            Else
                msg = "�������ӳ���"
                SelectSQL = Nothing

            End If

        Else
            msg = "SQL�������" & SQL
            SelectSQL = Nothing
        End If
Finally_Exit:

        'rst.Close()
        rst = Nothing
        ' Conn.Close()
        'Conn = Nothing
        ' SelectSQL = Nothing
        Exit Function
ErrorHandle:
        MsgString = "��ѯ����" & Err.Description
        Resume Finally_Exit
    End Function

    '    Shared Function SelectSQL(ByVal SQL As String, ByRef msg As String) As ADODB.Recordset
    '        Dim MsgString As Object
    '        'ִ��SQL��䣬����ADODB.Recordset
    '        Dim Conn As ADODB.Connection
    '        Dim rst As ADODB.Recordset
    '        Dim sTokens() As String
    '        Conn = New ADODB.Connection
    '        ' Conn = System.DBNull.Value
    '        On Error GoTo ErrorHandle '������
    '        '�ж�SQL���
    '        sTokens = Split(SQL) '����Split �������SQL���
    '        If InStr("SELECT", UCase(sTokens(0))) Then '�����SELECT���
    '            If OpenConn(Conn) Then
    '                rst = New ADODB.Recordset
    '                rst.CursorLocation = ADODB.CursorLocationEnum.adUseClient
    '                'ִ�в�ѯ����
    '                rst.Open(Trim(SQL), Conn, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
    '                SelectSQL = rst
    '                msg = "��ѯ��" & rst.RecordCount & "����¼��"

    '            Else
    '                msg = "���ݲ�ѯ����"
    '                SelectSQL = Nothing

    '            End If
    '        Else
    '            msg = "SQL�������" & SQL
    '            SelectSQL = Nothing
    '        End If
    'Finally_Exit:

    '        'rst.Close()
    '        rst = Nothing
    '        ' Conn.Close()
    '        Conn = Nothing
    '        ' SelectSQL = Nothing
    '        Exit Function
    'ErrorHandle:
    '        MsgString = "��ѯ����" & Err.Description
    '        Resume Finally_Exit
    '    End Function

End Class
