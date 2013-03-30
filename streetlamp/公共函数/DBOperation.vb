''' <summary>
''' 数据库函数
''' </summary>
''' <remarks></remarks>
''' 
Public Class DBOperation
    Shared conn_err As Integer = 1
    ''' <summary>
    '''  '得到数据库连接字符串，用户可以在此设置连接字符串
    ''' </summary>
    ''' <returns>连接字符串</returns>
    ''' <remarks></remarks>
    Shared Function GetConnStr() As String
        'User ID是数据库用户ID，Password是登录密码
        'Initial Catalog是数据库名，Data Source是服务器名称
        'GetConnStr = "Provider=SQLOLEDB.1;Persist Security Info=True;" & "User ID='" + Com_inf.server_user + "';Password='" + Com_inf.server_password + "';Initial Catalog=streetlamp;Data Source='" + Com_inf.server + "'"
        'GetConnStr = System.Configuration.ConfigurationManager.ConnectionStrings("streetlamp.Logion").ConnectionString

        Dim config As Configuration.Configuration
        config = System.Configuration.ConfigurationManager.OpenExeConfiguration(Configuration.ConfigurationUserLevel.None)

        GetConnStr = config.ConnectionStrings.ConnectionStrings("streetlamp.Login").ConnectionString

    End Function
    ''' <summary>
    '''   '打开数据库连接，
    ''' </summary>
    ''' <param name="Conn"></param>
    ''' <returns>连接成功返回true,出错返回false</returns>
    ''' <remarks></remarks>
    Shared Function OpenConn(ByRef Conn As ADODB.Connection) As Boolean

        '  Conn = New ADODB.Connection
        On Error GoTo ErrorHandle '出错处理
        Conn.Open(GetConnStr) '打开数据库连接
        OpenConn = True
        Exit Function
ErrorHandle:
        'MsgBox("连接数据库失败！请重新登录！", , PROJECT_TITLE_STRING)
        g_welcomewinobj.SetTextDelegate("连接数据库失败" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

        OpenConn = False
        ' End
        'Exit Function
    End Function
    ''' <summary>
    '''  '执行SQL语句，包括UPDATA，DELETE和INSERT语句
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
        On Error GoTo ErrorHandle '出错处理
        '判断SQL语句是不是UPDATE，SELECT和INSERT语句
        sTokens = Split(SQL) '调用Split 函数拆分SQL语句
        If InStr("DELETE", UCase(sTokens(0))) Then
            '打开数据库
            If conn.State = ConnectionState.Open Then '如果打开成功,则执行SQL
                conn.Execute(SQL)
                msg = sTokens(0) & "执行成功！"
            Else
                msg = sTokens(0) & "执行失败！"
            End If
            msg = msg & SQL
        End If
        If InStr("INSERT", UCase(sTokens(0))) Then
            '打开数据库
            If conn.State = ConnectionState.Open Then '如果打开成功,则执行SQL
                conn.Execute(SQL)
                msg = sTokens(0) & "执行成功！"
            Else
                msg = sTokens(0) & "执行失败！"
            End If
            msg = msg & SQL
        End If
        If InStr("UPDATE", UCase(sTokens(0))) Then
            '打开数据库
            If conn.State = ConnectionState.Open Then '如果打开成功,则执行SQL
                conn.Execute(SQL)
                msg = sTokens(0) & "执行成功！"
            Else
                msg = sTokens(0) & "执行失败！"
            End If
            msg = msg & SQL
        End If

Finally_Exit:  '程序结束的时候进行对象的销毁工作
        ' rst = Nothing
        'Conn.Close()
        'Conn = Nothing
        ' ExecuteSQL = Nothing
        Exit Sub
ErrorHandle:  '如果SQL语句执行出错，则提示出错信息并转到Finally_Exit
        msg = "执行错误：" & Err.Description
        Resume Finally_Exit

    End Sub

    ''' <summary>
    ''' '执行SQL语句，
    ''' </summary>
    ''' <param name="conn"></param>
    ''' <param name="SQL"></param>
    ''' <param name="msg"></param>
    ''' <returns>返回ADODB.Recordset</returns>
    ''' <remarks></remarks>
    Shared Function SelectSQL(ByVal conn As ADODB.Connection, ByVal SQL As String, ByRef msg As String) As ADODB.Recordset
        Dim MsgString As Object

        ' Dim Conn As ADODB.Connection
        Dim rst As ADODB.Recordset
        Dim sTokens() As String
        ' Conn = New ADODB.Connection
        ' Conn = System.DBNull.Value
        On Error GoTo ErrorHandle '出错处理
        '判断SQL语句
        sTokens = Split(SQL) '调用Split 函数拆分SQL语句
        If InStr("SELECT", UCase(sTokens(0))) Then '如果是SELECT语句
            If conn.State = ConnectionState.Open Then
                rst = New ADODB.Recordset
                rst.CursorLocation = ADODB.CursorLocationEnum.adUseClient
                '执行查询操作
                rst.Open(Trim(SQL), conn, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                SelectSQL = rst
                msg = "查询到" & rst.RecordCount & "条记录！"
            Else
                msg = "数据连接出错"
                SelectSQL = Nothing

            End If

        Else
            msg = "SQL语句有误：" & SQL
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
        MsgString = "查询有误：" & Err.Description
        Resume Finally_Exit
    End Function

    '    Shared Function SelectSQL(ByVal SQL As String, ByRef msg As String) As ADODB.Recordset
    '        Dim MsgString As Object
    '        '执行SQL语句，返回ADODB.Recordset
    '        Dim Conn As ADODB.Connection
    '        Dim rst As ADODB.Recordset
    '        Dim sTokens() As String
    '        Conn = New ADODB.Connection
    '        ' Conn = System.DBNull.Value
    '        On Error GoTo ErrorHandle '出错处理
    '        '判断SQL语句
    '        sTokens = Split(SQL) '调用Split 函数拆分SQL语句
    '        If InStr("SELECT", UCase(sTokens(0))) Then '如果是SELECT语句
    '            If OpenConn(Conn) Then
    '                rst = New ADODB.Recordset
    '                rst.CursorLocation = ADODB.CursorLocationEnum.adUseClient
    '                '执行查询操作
    '                rst.Open(Trim(SQL), Conn, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
    '                SelectSQL = rst
    '                msg = "查询到" & rst.RecordCount & "条记录！"

    '            Else
    '                msg = "数据查询出错"
    '                SelectSQL = Nothing

    '            End If
    '        Else
    '            msg = "SQL语句有误：" & SQL
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
    '        MsgString = "查询有误：" & Err.Description
    '        Resume Finally_Exit
    '    End Function

End Class
