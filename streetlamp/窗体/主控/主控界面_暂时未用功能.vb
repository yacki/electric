Partial Public Class welcome_win
    'Private Sub 运行报表ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 运行报表ToolStripMenuItem.Click
    '    '选择时间区间在当前时间?个小时之间的数据

    '    '故障统计报表
    '    Dim xlApp As Microsoft.Office.Interop.Excel.Application

    '    Dim xlBook As Microsoft.Office.Interop.Excel.Workbook
    '    Dim xlSheet As Microsoft.Office.Interop.Excel.Worksheet
    '    Dim row As Integer


    '    'open_file = "excel"
    '    xlApp = (New Microsoft.Office.Interop.Excel.Application)

    '    xlBook = xlApp.Workbooks().Add
    '    xlSheet = xlBook.Worksheets("sheet1")

    '    'On Error GoTo createErr

    '    '  Dim Table As New DataTable
    '    xlApp.Cells(1, 1) = "路灯统计报表"
    '    xlApp.Rows(1).RowHeight = 50
    '    xlApp.Rows(1).Font.Size = 18
    '    xlApp.Rows(1).Font.Bold = True

    '    xlApp.Cells(2, 1) = "编号"
    '    xlApp.Cells(2, 2) = "路灯编号"
    '    xlApp.Cells(2, 3) = "电流"
    '    xlApp.Cells(2, 4) = "时间"
    '    xlApp.Cells(2, 5) = "备注"

    '    'xlApp.Rows(2).Font.Bold = True
    '    'xlApp.Rows(2).font.size = 12
    '    'xlApp.Rows(2).RowHeight = 30


    '    row = 3

    '    Dim rs As New ADODB.Recordset
    '    Dim conn As New ADODB.Connection
    '    Dim sql As String
    '    Dim msg As String
    '    Dim id As Integer
    '    Dim lamp_id As String
    '    id = 1
    '    DBOperation.OpenConn(conn)

    '    sql = ""
    '    msg = ""
    '    sql = "select * from RoadLightStatus where Createtime>'2010-5-25 18:45:00' and Createtime<'2010-5-26 05:00:00' and substring(StatusContent,7,2)<>'00' order by substring(StatusContent,1,8),createtime"
    '    rs = DBOperation.SelectSQL(conn, sql, msg)
    '    If rs Is Nothing Then
    '        MsgBox(MSG_ERROR_STRING & "运行报表ToolStripMenuItem_Click", , PROJECT_TITLE_STRING)
    '        conn.Close()
    '        conn = Nothing
    '        Me.Close()
    '    End If
    '    While rs.EOF = False
    '        Com_inf.ExplainStateString(Trim(rs.Fields("StatusContent").Value))

    '        lamp_id = g_lampidstring
    '        xlApp.Cells(row, 1) = "'" & id

    '        xlApp.Cells(row, 2) = "'" & Val(Mid(Trim(lamp_id), 1, 4)) & "-" & Val(Mid(Trim(lamp_id), 5, 2)) & "-" & Val(Mid(Trim(lamp_id), 7, LAMP_ID_LEN))
    '        If g_currentad = 255 And g_dianzuad = 255 Then
    '            xlApp.Cells(row, 3) = "无返回"
    '        Else
    '            xlApp.Cells(row, 3) = g_currentad

    '        End If
    '        xlApp.Cells(row, 4) = rs.Fields("createtime").Value
    '        row += 1
    '        id += 1
    '        rs.MoveNext()
    '    End While


    '    With xlSheet
    '        .Range(.Cells(1, 1), .Cells(1, 5)).Merge()



    '        .Range(.Cells(3, 1), .Cells(3, 1)).ColumnWidth = 5
    '        .Range(.Cells(3, 2), .Cells(3, 2)).ColumnWidth = 10
    '        .Range(.Cells(3, 3), .Cells(3, 3)).ColumnWidth = 10
    '        .Range(.Cells(3, 4), .Cells(3, 4)).ColumnWidth = 20
    '        .Range(.Cells(3, 5), .Cells(3, 5)).ColumnWidth = 10


    '        .Range(.Cells(2, 1), .Cells(row - 1, 5)).RowHeight = 20

    '        .Range(.Cells(1, 1), .Cells(1, 5)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter

    '        .Range(.Cells(2, 1), .Cells(row - 1, 5)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter


    '        .Range(.Cells(2, 1), .Cells(3, 5)).Font.Name = "宋体"
    '        '设标题为宋体字
    '        .Range(.Cells(2, 1), .Cells(3, 5)).Font.Bold = "True"
    '        '标题字体加粗
    '        .Range(.Cells(1, 1), .Cells(2, 5)).Borders.LineStyle = 0
    '        .Range(.Cells(2, 1), .Cells(row - 1, 5)).Borders.LineStyle = 1
    '        '设表格边框样式
    '        .Range(.Cells(2, 1), .Cells(row - 1, 5)).Font.Size = 12

    '        '表中数据的字号

    '        .PageSetup.CenterFooter = "第 &P 页/共 &N 页"

    '    End With


    '    id = 1

    '    xlApp.Visible = True
    '    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    '    xlBook.Close()

    '    xlApp.Quit()
    '    xlSheet = Nothing

    '    xlBook = Nothing
    '    xlApp = Nothing



    'End Sub

    '    Private Sub BackgroundWorker_on_off_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker_on_off.DoWork
    '        Dim conn As New ADODB.Connection
    '        Dim rs, rs_lamp As New ADODB.Recordset
    '        Dim msg As String
    '        Dim sql As String
    '        Dim control_string As String  '发送的控制命令
    '        Dim control_box_num As Integer  '电控箱的个数
    '        Dim i As Integer
    '        Dim control_box_list() As m_control_box_on_off
    '        Dim j As Integer = 0
    '        Dim get_tag As Integer = 0
    '        Dim time As Date '发送查询的时间
    '        Dim str As String
    '        i = 0
    '        control_box_num = 0
    '        DBOperation.OpenConn(conn)
    '        msg = ""
    '        sql = ""
    '        While Me.BackgroundWorker_on_off.CancellationPending = False
    '            time = Now
    '            get_tag = 0
    '            sql = "select control_box_id from control_box where elec_state='断电'"
    '            rs = DBOperation.SelectSQL(conn, sql, msg)
    '            If rs Is Nothing Then
    '                MsgBox(MSG_ERROR_STRING & "BackgroundWorker_on_off_DoWork", , PROJECT_TITLE_STRING)
    '                GoTo finish
    '            End If

    '            ReDim control_box_list(rs.RecordCount)
    '            control_box_num = 0
    '            While rs.EOF = False
    '                ' 对每个电控箱的开灯状态进行判断
    '                str = Com_inf.Dec_to_ox(Val(Trim(rs.Fields("control_box_id").Value)), 2)  '电控箱的两位十六制编号

    '                control_string = str & " 00 01 20 11 FF"  '
    '                control_box_list(control_box_num).control_box_string1 = control_string
    '                '   control_lamp_obj.Input_db_control(control_string) '将查询命令放入数据库中
    '                ' System.Threading.Thread.Sleep(2000)
    '                control_string = str & " 00 02 20 11 FF"  '
    '                control_box_list(control_box_num).control_box_string2 = control_string
    '                control_box_list(control_box_num).tag = 0  '初始状态没上电
    '                rs.MoveNext()
    '                control_box_num += 1

    '            End While

    '            i = 0
    '            While i < control_box_num  '发送查询第一盏灯的查询命令
    '                m_controllampobj.Input_db_control(control_box_list(i).control_box_string1) '将查询命令放入数据库中
    '                i += 1
    '            End While

    '            i = 0
    '            While i < m_waittime
    '                While j < control_box_num
    '                    sql = "select * from RoadLightStatus where HandlerFlag<>1 and substring(statusContent,1,8)=substring('" & control_box_list(j).control_box_string1 & "',1,8) and createtime>'" & time & "'"
    '                    rs = DBOperation.SelectSQL(conn, sql, msg)
    '                    If rs Is Nothing Then
    '                        MsgBox(MSG_ERROR_STRING & "BackgroundWorker_on_off_DoWork", , PROJECT_TITLE_STRING)
    '                        GoTo finish
    '                    End If
    '                    If rs.RecordCount > 0 Then
    '                        Com_inf.ExplainStateString(Trim(rs.Fields("statusContent").Value))
    '                        If g_currentad <> 255 Or g_dianzuad <> 255 Then
    '                            control_box_list(j).tag = 1
    '                            sql = "update lamp_inf set result=4 where control_box_id='" & g_lampidstring & "'"
    '                            DBOperation.ExecuteSQL(conn, sql, msg)
    '                            sql = "update RoadLightStatus set HandlerFlag=1 where ID='" & rs.Fields("ID").Value & "'"
    '                            DBOperation.ExecuteSQL(conn, sql, msg)
    '                            sql = "Update control_box set elec_state='上电' where control_box_id='" & g_lampidstring & "'"
    '                            DBOperation.ExecuteSQL(conn, sql, msg)
    '                            get_tag += 1
    '                        End If
    '                    End If
    '                    j += 1
    '                End While
    '                j = 0
    '                If get_tag < control_box_num Then
    '                    System.Threading.Thread.Sleep(1000)
    '                    i += 1
    '                Else
    '                    GoTo finish2
    '                End If

    '            End While


    '            '如果第一盏灯没有收到状态收第二盏灯
    '            i = 0
    '            While i < control_box_num  '发送查询第二盏灯的查询命令
    '                If control_box_list(i).tag = 0 Then
    '                    m_controllampobj.Input_db_control(control_box_list(i).control_box_string2) '将查询命令放入数据库中

    '                End If
    '                i += 1
    '            End While

    '            i = 0
    '            While i < m_waittime
    '                While j < control_box_num - 1
    '                    If control_box_list(j).tag = 0 Then
    '                        sql = "select * from RoadLightStatus where HandlerFlag<>1 and substring(statusContent,1,8)=substring('" & control_box_list(j).control_box_string2 & "',1,8) and createtime>'" & time & "'"
    '                        rs = DBOperation.SelectSQL(conn, sql, msg)
    '                        If rs Is Nothing Then
    '                            MsgBox(MSG_ERROR_STRING & "BackgroundWorker_on_off_DoWork", , PROJECT_TITLE_STRING)
    '                            GoTo finish
    '                        End If
    '                        If rs.RecordCount > 0 Then
    '                            Com_inf.ExplainStateString(Trim(rs.Fields("statusContent").Value))
    '                            If g_currentad <> 255 Or g_dianzuad <> 255 Then
    '                                control_box_list(j).tag = 1
    '                                sql = "update lamp_inf set result=4 where control_box_id='" & g_lampidstring & "'"
    '                                DBOperation.ExecuteSQL(conn, sql, msg)
    '                                sql = "update RoadLightStatus set HandlerFlag=1 where ID='" & rs.Fields("ID").Value & "'"
    '                                DBOperation.ExecuteSQL(conn, sql, msg)
    '                                sql = "Update control_box set elec_state='上电' where control_box_id='" & g_lampidstring & "'"
    '                                DBOperation.ExecuteSQL(conn, sql, msg)
    '                                get_tag += 1
    '                            End If
    '                        End If

    '                    End If
    '                    j += 1
    '                End While
    '                j = 0
    '                If get_tag < control_box_num Then
    '                    System.Threading.Thread.Sleep(1000)
    '                    i += 1
    '                Else
    '                    GoTo finish2
    '                End If

    '            End While
    '            If get_tag < control_box_num Then
    '                GoTo finish
    '            Else
    '                GoTo finish2
    '            End If


    'finish:
    '            System.Threading.Thread.Sleep(20000)
    '        End While
    'finish2:
    '        ' Me.SetTextLabelDelegate("道路全部上电", Tool, "elec_on_off")  '状态栏显示当前坐标

    '        If rs.State = 1 Then
    '            rs.Close()
    '            rs = Nothing
    '        End If
    '        If rs_lamp.State = 1 Then
    '            rs_lamp.Close()
    '            rs_lamp = Nothing

    '        End If
    '        conn.Close()
    '        conn = Nothing
    '    End Sub



    '    ''' <summary>
    '    ''' 发现故障后，进行二次确认，该亮非亮发开命令，取状态确认；该暗非暗发关命令，取状态确认
    '    ''' </summary>
    '    ''' <param name="sender"></param>
    '    ''' <param name="e"></param>
    '    ''' <remarks></remarks>
    '    '''
    '    Private Sub BackgroundWorker_check_problem_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker_check_problem.DoWork
    '        '发现故障发二次确认故障,故障确认只确认路灯一块，三遥部分另处理
    '        Dim rs, rs_lamp As New ADODB.Recordset
    '        Dim conn As New ADODB.Connection
    '        Dim sql As String
    '        Dim msg As String
    '        Dim ox_str As String
    '        Dim lamp_id_string As String
    '        Dim control_string As String
    '        Dim lamp_id_hex As String
    '        Dim state_string As String
    '        Dim find_time As Integer
    '        Dim end_tag As String
    '        Dim result As Integer
    '        Dim open_close_string As String '开关标志
    '        Dim power As Integer  '功率
    '        Dim current As Double  '电流安培值
    '        Dim rs_lampnow As New ADODB.Recordset   '灯的实时状态

    '        DBOperation.OpenConn(conn)

    '        msg = ""
    '        power = 0  '功率初始化为0
    '        find_time = 0
    '        While Me.BackgroundWorker_check_problem.CancellationPending = False
    '            sql = "select * from lamp_state_list where time_end>DateAdd(n,-60,'" & Now() & "') and time_end<'" & Now & "' and (state='" & LAMP_STATE_PROBLEM_ON & "' or state='" & LAMP_STATE_PROBLEM_OFF & "') and (end_tag=" & 0 & "or end_tag=" & 1 & ") and substring(lamp_id,5,2)='00' order by lamp_id"
    '            rs = DBOperation.SelectSQL(conn, sql, msg)
    '            If rs Is Nothing Then
    '                MsgBox(MSG_ERROR_STRING & "BackgroundWorker_check_problem_DoWork", , PROJECT_TITLE_STRING)
    '                conn.Close()
    '                conn = Nothing
    '                Exit Sub
    '            End If
    '            control_string = ""
    '            If rs.RecordCount <= 0 Then
    '                Me.SetTextLabelDelegate("故障确认", Tool, "problem_comfirm_string")

    '            Else
    '                lamp_id_string = Trim(rs.Fields("lamp_id").Value) '灯的编号

    '                '***************检查数据库中lamp_inf此时的状态，是否与发生故障的时候改变**************
    '                sql = "select state from lamp_inf where lamp_id='" & Trim(rs.Fields("lamp_id").Value) & "'"
    '                rs_lampnow = DBOperation.SelectSQL(conn, sql, msg)
    '                If rs_lampnow Is Nothing Then
    '                    MsgBox(MSG_ERROR_STRING & "BackgroundWorker_check_problem_DoWork", , PROJECT_TITLE_STRING)
    '                    conn.Close()
    '                    conn = Nothing
    '                    Exit Sub
    '                End If
    '                If rs_lampnow.RecordCount > 0 Then
    '                    If (rs_lampnow.Fields("state").Value = 1 Or rs_lampnow.Fields("state").Value = 4) And Trim(rs.Fields("state").Value) = LAMP_STATE_PROBLEM_OFF Then
    '                        '该暗非暗的故障，然后路灯被发了开的命令，则此故障不处理
    '                        sql = "update lamp_state_list set end_tag=5 where ID='" & rs.Fields("ID").Value & "'"
    '                        DBOperation.ExecuteSQL(conn, sql, msg)
    '                        GoTo finish2
    '                    End If
    '                    If (rs_lampnow.Fields("state").Value = 0 Or rs_lampnow.Fields("state").Value = 3) And Trim(rs.Fields("state").Value) = LAMP_STATE_PROBLEM_ON Then
    '                        '该亮非亮的故障，然后路灯被发了关的命令，则此故障不处理
    '                        sql = "update lamp_state_list set end_tag=5 where ID='" & rs.Fields("ID").Value & "'"
    '                        DBOperation.ExecuteSQL(conn, sql, msg)
    '                        GoTo finish2
    '                    End If
    '                End If
    '                '**************************************************************************************

    '                Me.SetTextLabelDelegate("正在确认故障：" & lamp_id_string, Tool, "problem_comfirm_string")
    '                ox_str = Com_inf.Dec_to_ox(Mid(lamp_id_string, 1, 4), 2)  '将电控箱编号转变成2位16进制数
    '                control_string = ox_str
    '                ox_str = Com_inf.Dec_to_Bin(Val(Mid(lamp_id_string, 5, 2)), 5) & Com_inf.Dec_to_Bin(Val(Mid(lamp_id_string, 7, LAMP_ID_LEN)), 11)
    '                lamp_id_hex = Com_inf.BIN_to_HEX(ox_str)
    '                control_string &= " " & Mid(lamp_id_hex, 1, 2) & " " & Mid(lamp_id_hex, 3, 2)
    '                Dim state As String
    '                state = Trim(rs.Fields("state").Value)
    '                If state = LAMP_STATE_PROBLEM_ON Then
    '                    '重新发送一次打开的命令
    '                    open_close_string = "1B"
    '                    control_string &= " 1B 11 FF"  '开控制命令字符串
    '                Else
    '                    open_close_string = "1C"
    '                    control_string &= " 1C 11 FF"  '关控制命令字符串

    '                End If
    '                '置状态标志位
    '                sql = "select * from lamp_inf where lamp_id='" & lamp_id_string & "'"
    '                rs_lamp = DBOperation.SelectSQL(conn, sql, msg)
    '                If rs_lamp.RecordCount > 0 Then
    '                    If open_close_string = "1B" Then
    '                        'rs_lamp.Fields("state").Value = 4
    '                        sql = "update lamp_inf set state=4 where lamp_id='" & lamp_id_string & "'"
    '                    Else
    '                        ' rs_lamp.Fields("state").Value = 3
    '                        sql = "update lamp_inf set state=3 where lamp_id='" & lamp_id_string & "'"
    '                    End If
    '                    ' rs_lamp.Update()
    '                    DBOperation.ExecuteSQL(conn, sql, msg)
    '                End If


    '                m_controllampobj.Input_db_control(control_string)  '发送控制命令
    '                Dim time As Date
    '                System.Threading.Thread.Sleep(15000)
    '                control_string = Mid(control_string, 1, 8) & " 20 11 FF"
    '                m_controllampobj.Input_db_control(control_string)  '发送取状态命令
    '                time = Now()
    '                state_string = ""

    '                '将控制命令和目前的lamp_inf中的state，是否和控制命令相同，如相同则表示此次确认没有被手工打断，如不相同则表示此次
    '                '确认被手工打断，此次确认不算
    '                sql = "select * from lamp_inf where lamp_id='" & lamp_id_string & "'"
    '                rs_lamp = DBOperation.SelectSQL(conn, sql, msg)
    '                If rs_lamp.RecordCount > 0 Then
    '                    If (rs_lamp.Fields("state").Value = 1 Or rs_lamp.Fields("state").Value = 4) And open_close_string <> "1B" Then
    '                        'rs_lamp.Close()
    '                        GoTo finish2
    '                    End If
    '                    If (rs_lamp.Fields("state").Value = 0 Or rs_lamp.Fields("state").Value = 3) And open_close_string <> "1C" Then
    '                        'rs_lamp.Close()
    '                        GoTo finish2

    '                    End If

    '                End If


    '                find_time = 0
    '                While find_time < 10
    '                    state_string = m_controllampobj.Get_actual_state(lamp_id_string, 2, time)  '获取路灯的运行状态
    '                    If state_string <> "" Then
    '                        Exit While
    '                    End If
    '                    System.Threading.Thread.Sleep(1000)
    '                    find_time += 1
    '                End While

    '                Dim current_value As Double '电流值
    '                Dim presure_value As Double  '电压值
    '                Com_inf.Get_current_and_presure(g_currentad, g_dianzuad)

    '                current_value = g_currentvalue  '电流值
    '                presure_value = g_presurevalue  '电压值


    '                If state_string = "C" And state = LAMP_STATE_PROBLEM_ON Then

    '                    '确认故障为该亮非亮
    '                    end_tag = rs.Fields("end_tag").Value
    '                    If end_tag = 0 Then
    '                        'rs.Fields("end_tag").Value = 2
    '                        sql = "update lamp_state_list set end_tag=2 where ID='" & rs.Fields("ID").Value & "'"
    '                    Else
    '                        'rs.Fields("end_tag").Value = 3
    '                        sql = "update lamp_state_list set end_tag=3 where ID='" & rs.Fields("ID").Value & "'"
    '                    End If

    '                    'rs.Update()

    '                    DBOperation.ExecuteSQL(conn, sql, msg)

    '                    ''故障确认后，将lamp_inf中的total_num位置为1
    '                    'sql = "update lamp_inf set total_num=1, date='" & Now() & "' where lamp_id='" & lamp_id_string & "'"
    '                    'DBOperation.ExecuteSQL(conn, sql, msg)

    '                Else
    '                    If (state_string = "D_part" Or state_string = "D_all") And state = LAMP_STATE_PROBLEM_OFF Then
    '                        '确认故障为该暗非暗
    '                        end_tag = rs.Fields("end_tag").Value
    '                        'sql = "update lamp_state_list set end_tag=" & end_tag_string + 2
    '                        'DBOperation.ExecuteSQL(conn, sql, msg)
    '                        If end_tag = 0 Then
    '                            sql = "update lamp_state_list set end_tag=2 where ID='" & rs.Fields("ID").Value & "'"
    '                        Else
    '                            sql = "update lamp_state_list set end_tag=3 where ID='" & rs.Fields("ID").Value & "'"
    '                        End If
    '                        DBOperation.ExecuteSQL(conn, sql, msg)

    '                        ''故障确认后，将lamp_inf中的total_num位置为1
    '                        'sql = "update lamp_inf set total_num=1 where lamp_id='" & lamp_id_string & "'"
    '                        'DBOperation.ExecuteSQL(conn, sql, msg)

    '                    Else
    '                        Select Case (state_string)
    '                            Case ""
    '                                state = LAMP_STATE_NORETURN
    '                                result = 3
    '                                power = 0
    '                                current = 0
    '                            Case "F"
    '                                state = LAMP_STATE_NORETURN
    '                                result = 3
    '                                power = 0
    '                                current = 0
    '                            Case "A"
    '                                state = LAMP_STATE_OFF
    '                                result = 0
    '                                power = 0
    '                                current = 0
    '                            Case "B_part"
    '                                state = LAMP_STATE_ON
    '                                result = 0
    '                                power = 50
    '                                current = current_value
    '                            Case "B_all"
    '                                state = LAMP_STATE_ON
    '                                result = 0
    '                                power = 100
    '                                current = current_value
    '                            Case "C"
    '                                state = LAMP_STATE_PROBLEM_ON
    '                                result = 1
    '                                power = 0
    '                                current = 0
    '                            Case "D_part"
    '                                state = LAMP_STATE_PROBLEM_OFF
    '                                result = 2
    '                                power = 50
    '                                current = current_value
    '                            Case "D_all"
    '                                state = LAMP_STATE_PROBLEM_OFF
    '                                result = 2
    '                                power = 100
    '                                current = current_value
    '                        End Select

    '                        sql = "insert into lamp_state_list(lamp_id,state,time_start,time_end,end_tag) values('" & lamp_id_string & "','" & state & "','" & Now & "','" & Now & "'," & 0 & ")"
    '                        DBOperation.ExecuteSQL(conn, sql, msg)

    '                        'rs.Fields("end_tag").Value = 5  '标志为状态改变的故障
    '                        'rs.Update()
    '                        sql = "update lamp_state_list set end_tag=5 where ID='" & rs.Fields("ID").Value & "'"
    '                        DBOperation.ExecuteSQL(conn, sql, msg)

    '                        If result = 0 And state = LAMP_STATE_ON Then '亮
    '                            sql = "update lamp_inf set result='" & result & "' , state=" & 1 & " , current_l=" & current & ", power='" & power & "' where lamp_id='" & lamp_id_string & "'"
    '                        Else
    '                            If result = 0 And state = LAMP_STATE_OFF Then  '暗
    '                                sql = "update lamp_inf set result='" & result & "' , state=" & 0 & ", current_l=" & current & ", power='" & power & "' where lamp_id='" & lamp_id_string & "'"
    '                            Else
    '                                If result = 3 Then
    '                                    sql = "update lamp_inf set result='" & result & "' , current_l=" & current & ", power='" & power & "' , total_num=0 where lamp_id='" & lamp_id_string & "'"
    '                                Else
    '                                    sql = "update lamp_inf set result='" & result & "' , current_l=" & current & ", power='" & power & "' , total_num=1 where lamp_id='" & lamp_id_string & "'"

    '                                End If

    '                            End If
    '                        End If
    '                        DBOperation.ExecuteSQL(conn, sql, msg)

    '                    End If

    '                End If

    '            End If

    'finish2:
    '            System.Threading.Thread.Sleep(5000)
    '        End While

    'finish1:
    '        If rs_lampnow.State = 1 Then
    '            rs_lampnow.Close()
    '            rs_lampnow = Nothing
    '        End If
    '        If rs_lamp.State = 1 Then
    '            rs_lamp.Close()
    '            rs_lamp = Nothing
    '        End If
    '        rs.Close()
    '        rs = Nothing
    '        conn.Close()
    '        conn = Nothing

    '    End Sub

    '''' <summary>
    '''' 判断是否断电（标准是某一电控箱下所有的灯都无状态返回）
    '''' </summary>
    '''' <remarks></remarks>
    'Private Sub close_state()
    '    Dim rs_box, rs_lamp As New ADODB.Recordset
    '    Dim conn As New ADODB.Connection
    '    Dim sql As String
    '    Dim msg As String
    '    Dim lamp_num, lamp_i As Integer
    '    Dim box_num As Integer

    '    box_num = 0

    '    DBOperation.OpenConn(conn)
    '    msg = ""
    '    sql = "select control_box_id from control_box"

    '    rs_box = DBOperation.SelectSQL(conn, sql, msg)
    '    If rs_box Is Nothing Then
    '        MsgBox(MSG_ERROR_STRING & "close_state", , PROJECT_TITLE_STRING)
    '        conn.Close()
    '        conn = Nothing
    '    End If
    '    While rs_box.EOF = False
    '        sql = "select * from lamp_inf where control_box_id='" & Trim(rs_box.Fields("control_box_id").Value) & "'"
    '        rs_lamp = DBOperation.SelectSQL(conn, sql, msg)
    '        If rs_lamp Is Nothing Then
    '            MsgBox(MSG_ERROR_STRING & "close_state", , PROJECT_TITLE_STRING)
    '            If rs_box.State = 1 Then
    '                rs_box.Close()
    '                rs_box = Nothing
    '            End If
    '            conn.Close()
    '            conn = Nothing
    '        End If
    '        lamp_i = 0
    '        lamp_num = rs_lamp.RecordCount
    '        While rs_lamp.EOF = False
    '            If rs_lamp.Fields("result").Value = 3 Then
    '                lamp_i += 1
    '            End If
    '            rs_lamp.MoveNext()
    '        End While
    '        If lamp_i = lamp_num Then
    '            '所有灯都无返回值，则表示该路断电，启动判断上电的线程
    '            box_num += 1
    '            'Me.SetTextLabelDelegate(box_num & "条道路断电", Tool, "elec_on_off")  '状态栏显示当前坐标
    '            '更新电控箱的上电情况
    '            sql = "update control_box set elec_state='断电' where control_box_id='" & Trim(rs_box.Fields("control_box_id").Value) & "'"
    '            DBOperation.ExecuteSQL(conn, sql, msg)

    '            If Me.BackgroundWorker_on_off.IsBusy = False Then
    '                Me.BackgroundWorker_on_off.RunWorkerAsync()
    '            End If
    '        End If
    '        rs_box.MoveNext()

    '    End While
    '    'If box_num = 0 Then
    '    '    '表示所有道路都上电
    '    '    Me.SetTextLabelDelegate("所有道路上电", Tool, "elec_on_off")  '状态栏显示当前坐标

    '    'End If

    '    If rs_box.State = 1 Then
    '        rs_box.Close()
    '        rs_box = Nothing
    '    End If
    '    conn.Close()
    '    conn = Nothing

    'End Sub
    '#Region "监测面板"
    '    ''' <summary>
    '    ''' 定位到某一区域
    '    ''' </summary>
    '    ''' <param name="sender"></param>
    '    ''' <param name="e"></param>
    '    ''' <remarks></remarks>
    '    Private Sub find_street_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '        '景观灯中将街道定位改为电控箱定位

    '        Dim left_ori As Integer
    '        Dim top_ori As Integer
    '        Dim left_moveto As Integer
    '        Dim top_moveto As Integer

    '        Dim rs As ADODB.Recordset
    '        Dim msg As String
    '        Dim sql As String


    '        left_moveto = 0
    '        top_moveto = 0
    '        If map_size.Value <> 10 Then  '定位时恢复正常尺寸
    '            'MsgBox("请在正常尺寸下进行地图的定位")
    '            'Exit Sub

    '            map_size.Value = 10
    '        End If
    '        If control_box_name.Text = "" Then
    '            MsgBox("请选择区域", , msg_box_title)
    '            control_box_name.Focus()
    '            Exit Sub
    '        End If
    '        Dim conn As New ADODB.Connection
    '        DBOperation.OpenConn(conn)
    '        msg = ""
    '        sql = "select * from street_position where control_box_name='" & Trim(control_box_name.Text) & "'"
    '        rs = DBOperation.SelectSQL(conn, sql, msg)
    '        If rs.RecordCount > 0 Then
    '            left_moveto = rs.Fields("pos_x").Value  '定位的坐标X信息
    '            top_moveto = rs.Fields("pos_y").Value  '定位的坐标Y信息
    '        Else
    '            MsgBox("没有该区域的信息，请选择正确的区域！", , msg_box_title)
    '            conn.Close()
    '            conn = Nothing
    '            Exit Sub
    '        End If

    '        rs.Close()
    '        rs = Nothing

    '        left_ori = map.Left  '地图的原始X坐标
    '        top_ori = map.Top   '地图的原始Y坐标

    '        map.Left = left_moveto   '地图的新X坐标
    '        map.Top = top_moveto   '地图的新Y坐标

    '        map.Refresh()
    '        conn.Close()
    '        conn = Nothing
    '    End Sub

    '#End Region

    '''' <summary>
    '''' 打开所有的路灯，按区域、类型和单灯三个级别
    '''' </summary>
    '''' <remarks></remarks>

    'Private Sub open_all_lamp()   '打开所有路灯
    '    If box_control.Checked = False And type_control.Checked = False And lamp_id_control.Checked = False Then
    '        MsgBox("请选择控制类型！", , msg_box_title)  '如果没有选择控制类型，提示选择控制类型后再进行下一步操作
    '        Exit Sub
    '    End If
    '    Dim power_string As String

    '    power_string = "100"  '转换功率的值

    '    '按电控箱查询
    '    If box_control.Checked = True Then
    '        If box_all.Text = "" Then   '如果电控箱文本框为空
    '            MsgBox("请选择区域编号！", , msg_box_title)
    '            box_all.Focus()  '光标定位在电控箱文本框
    '            Exit Sub
    '        End If
    '        '判断是否输入控制命令
    '        If MsgBox("通信正常，是否打开区域: " & Trim(box_all.Text) & "的所有景观灯?", MsgBoxStyle.YesNo, msg_box_title) = MsgBoxResult.No Then
    '            Exit Sub
    '        End If


    '        '输入电控箱控制命令：
    '        control_lamp_obj.Input_control_inf("", "区域", Trim(box_all.Text), "全开", 1, Trim(diangan.Text), power_string)
    '        '关闭需要暗的灯
    '        Com_inf.Turn_off_lamp(Trim(box_all.Text), "", "")
    '    End If

    '    If type_control.Checked = True Then  '按类型查询
    '        If box_all.Text = "" Then
    '            MsgBox("请选择区域编号！", , msg_box_title)
    '            box_all.Focus()  '光标定位在电控箱文本框
    '            Exit Sub
    '        End If
    '        If lamp_type_all.Text = "" Then
    '            MsgBox("请选择景观灯类型！", , msg_box_title)
    '            lamp_type_all.Focus()
    '            Exit Sub
    '        End If
    '        If MsgBox("通信正常，是否打开区域: " & Trim(box_all.Text) & "中所有" & Trim(lamp_type_all.Text) & "?", MsgBoxStyle.YesNo, msg_box_title) = MsgBoxResult.No Then
    '            Exit Sub
    '        End If
    '        '输入按电控箱下某一类型控制的命令
    '        control_lamp_obj.Input_control_inf(Trim(lamp_type_all.Text), "类型", Trim(box_all.Text), "类型全开", 1, Trim(diangan.Text), power_string)
    '        '关闭需要暗的灯
    '        Com_inf.Turn_off_lamp(Trim(box_all.Text), Trim(lamp_type_all.Text), "")
    '    End If

    '    If lamp_id_control.Checked = True Then  '按景观灯编号查询
    '        If MsgBox("通信正常，是否打开区域: " & Trim(box_all.Text) & "中第" & Trim(lamp_id_all.Text) & "号" & Trim(lamp_type_all.Text), MsgBoxStyle.YesNo, msg_box_title) = MsgBoxResult.No Then
    '            Exit Sub
    '        End If
    '        open_close_single_lamp(Trim(box_all.Text), Trim(lamp_type_all.Text), Trim(lamp_id_all_start.Text) & Trim(lamp_id_all.Text), 1)
    '        '关闭需要暗的灯
    '        'Com_inf.Turn_off_lamp(Trim(box_all.Text), Trim(lamp_type_all.Text), Trim(lamp_id_all_start.Text) & Trim(lamp_id_all.Text))
    '    End If

    '    If box_control.Checked = True Then  '电控箱控制
    '        MsgBox("区域：" & Trim(box_all.Text) & " 打开！", , msg_box_title)
    '        Doing_now_text.AppendText("区域：" & Trim(box_all.Text) & " 打开！" & Format(Now(), "yyyy-MM-dd HH:mm:ss") & vbCrLf)
    '        Exit Sub
    '    End If
    '    If type_control.Checked = True Then  '按类型控制
    '        MsgBox("区域：" & Trim(box_all.Text) & " " & Trim(lamp_type_all.Text) & " 打开！", , msg_box_title)
    '        Doing_now_text.AppendText("区域：" & Trim(box_all.Text) & " " & Trim(lamp_type_all.Text) & " 打开！" & Format(Now(), "yyyy-MM-dd HH:mm:ss") & vbCrLf)
    '        Exit Sub
    '    End If

    '    If lamp_id_control.Checked = True Then '按景观灯编号控制
    '        MsgBox("区域：" & Trim(box_all.Text) & " " & Trim(lamp_type_all.Text) & " " & Trim(lamp_id_all.Text) & " 打开！", , msg_box_title)
    '        Doing_now_text.AppendText("区域：" & Trim(box_all.Text) & " " & Trim(lamp_type_all.Text) & " " & Trim(lamp_id_all.Text) & " 全部打开！" & Format(Now(), "yyyy-MM-dd HH:mm:ss") & vbCrLf)
    '        Exit Sub
    '    End If

    '    MsgBox("打开灯出错！", , msg_box_title)
    '    Doing_now_text.AppendText("打开灯出错" & Format(Now(), "yyyy-MM-dd HH:mm:ss") & vbCrLf)

    'End Sub

    '''' <summary>
    '''' 隔两盏的方式打开路灯，分为区域、类型、单灯三个级别
    '''' </summary>
    '''' <remarks></remarks>
    'Private Sub open_1_3_control()
    '    '打开1/3路灯
    '    ' Dim control_lamp_obj As New control_lamp
    '    Dim power_string As String


    '    If box_control.Checked = False Then
    '        MsgBox("请选择控制类型！", , msg_box_title)  '如果没有选择控制类型，提示选择控制类型后再进行下一步操作
    '        Exit Sub
    '    End If
    '    power_string = "100"  '转换功率的值
    '    '按电控箱查询
    '    If box_control.Checked = True Then
    '        If box_all.Text = "" Then   '如果电控箱文本框为空
    '            MsgBox("请选择区域编号！", , msg_box_title)
    '            box_all.Focus()  '光标定位在电控箱文本框
    '            Exit Sub
    '        End If
    '        '判断是否输入控制命令
    '        If MsgBox("通信正常，是否打开区域: " & Trim(box_all.Text) & "的1/3编号景观灯?", MsgBoxStyle.YesNo, msg_box_title) = MsgBoxResult.No Then
    '            Exit Sub
    '        End If
    '        '输入电控箱控制命令：
    '        control_lamp_obj.Input_control_inf("", "区域", Trim(box_all.Text), "1/3开", 1, Trim(diangan.Text), power_string)
    '        '关闭需要暗的灯
    '        Com_inf.Turn_off_lamp(Trim(box_all.Text), "", "")
    '    End If

    '    If box_control.Checked = True Then  '按电控箱控制
    '        MsgBox(Trim(box_all.Text) & " 号区域1/3编号景观灯打开", , msg_box_title)
    '        ' Doing_now_text.AppendText(area_title & Trim(box_all.Text) & " 号电控箱单号灯" & open_close & Format(Now(), "yyyy-MM-dd HH:mm:ss") & vbCrLf)
    '        circle_string.Text = Trim(box_all.Text) & " 号区域1/3编号景观灯打开打开"

    '    End If


    'End Sub
    '''' <summary>
    '''' 按单号开还是双号开关
    '''' </summary>
    '''' <param name="open">open :0,关，1 开</param>
    '''' <param name="single_double">single_double 1，奇  0,偶</param>
    '''' <remarks></remarks>
    'Private Sub single_open_control(ByVal open As Integer, ByVal single_double As Integer)  'open :0,关，1 开；single_double 1，奇  0,偶
    '    ' Dim control_lamp_obj As New control_lamp
    '    If box_control.Checked = False And type_control.Checked = False Then
    '        MsgBox("请选择控制类型！", , msg_box_title)  '如果没有选择控制类型，提示选择控制类型后再进行下一步操作
    '        Exit Sub
    '    End If

    '    If open = 1 Then  'open=1表示单/双号路灯开
    '        If power.Text = "" Then  '如果没输入功率，则提示输入功率后再进行下一步操作
    '            MsgBox("请输入功率！", , msg_box_title)
    '            power.Focus()  '光标定位在功率文本框
    '            Exit Sub
    '        Else
    '            If IsNumeric(Trim(power.Text)) = False Or Val(Trim(power.Text)) <= 0 Or Val(Trim(power.Text)) > 100 Then  '功率的值不在1%-100%之间
    '                MsgBox("功率值必须为1%-100%之间的数值，请重新输入!", , msg_box_title)
    '                power.Focus() '光标定位在功率文本框
    '                Exit Sub
    '            End If
    '        End If
    '        If diangan.Text = "" Then
    '            MsgBox("请选择电感控制方法", , msg_box_title)
    '            diangan.Focus()
    '            Exit Sub
    '        End If

    '    End If

    '    If city_all.Text = "" Then  '如果城市文本框为空
    '        MsgBox("请选择城市名称！", , msg_box_title)
    '        city_all.Focus()  '光标定位在城市文本框
    '        Exit Sub
    '    End If


    '    If type_control.Checked = True Then  '按电控箱控制

    '        If box_all.Text = "" Then  '如果电控箱文本框为空
    '            MsgBox("请选择区域编号！", , msg_box_title)
    '            box_all.Focus()  '光标定位在电控箱文本框
    '            Exit Sub
    '        End If
    '        If lamp_type_all.Text = "" Then
    '            MsgBox("请选择景观灯类型!", , msg_box_title)
    '            lamp_type_all.Focus()
    '            Exit Sub
    '        End If

    '        If open = 1 And single_double = 1 Then '单号路灯开
    '            If MsgBox("通信正常，是否打开区域: " & Trim(box_all.Text) & "中所有单号" & Trim(lamp_type_all.Text) & "?", MsgBoxStyle.YesNo, msg_box_title) = MsgBoxResult.No Then
    '                Exit Sub
    '            End If
    '            control_lamp_obj.Input_control_inf(Trim(lamp_type_all.Text), "类型", Trim(box_all.Text), "类型奇开", 1, Trim(diangan.Text), Trim(power.Text))

    '        Else
    '            If open = 0 And single_double = 1 Then  '单号路灯关
    '                If MsgBox("通信正常，是否关闭区域: " & Trim(box_all.Text) & "中所有单号" & Trim(lamp_type_all.Text) & "?", MsgBoxStyle.YesNo, msg_box_title) = MsgBoxResult.No Then
    '                    Exit Sub
    '                End If
    '                control_lamp_obj.Input_control_inf(Trim(lamp_type_all.Text), "类型", Trim(box_all.Text), "类型奇闭", 0, "关闭灯", "0")


    '                ' control_lamp_obj.Input_control_inf(lamp_start, lamp_end, "电控箱", Trim(box_all.Text), "奇闭", 0, Trim(diangan.Text), Trim(power.Text))
    '            Else
    '                If open = 1 And single_double = 0 Then  '双号路灯开
    '                    If MsgBox("通信正常，是否打开区域: " & Trim(box_all.Text) & "中所有双号" & Trim(lamp_type_all.Text) & "?", MsgBoxStyle.YesNo, msg_box_title) = MsgBoxResult.No Then
    '                        Exit Sub
    '                    End If
    '                    control_lamp_obj.Input_control_inf(Trim(lamp_type_all.Text), "类型", Trim(box_all.Text), "类型偶开", 1, Trim(diangan.Text), Trim(power.Text))

    '                Else

    '                    If open = 0 And single_double = 0 Then  '双号路灯关
    '                        If MsgBox("通信正常，是否关闭区域: " & Trim(box_all.Text) & "中所有双号" & Trim(lamp_type_all.Text) & "?", MsgBoxStyle.YesNo, msg_box_title) = MsgBoxResult.No Then
    '                            Exit Sub
    '                        End If
    '                        control_lamp_obj.Input_control_inf(Trim(lamp_type_all.Text), "类型", Trim(box_all.Text), "类型偶闭", 0, "关闭灯", "0")


    '                        'control_lamp_obj.Input_control_inf(lamp_start, lamp_end, "电控箱", Trim(box_all.Text), "偶闭", 0, Trim(diangan.Text), Trim(power.Text))
    '                    Else
    '                        MsgBox("奇偶控制出错！", , msg_box_title)
    '                    End If

    '                End If
    '            End If

    '        End If
    '    End If
    '    '关闭需要暗的灯
    '    Com_inf.Turn_off_lamp(Trim(box_all.Text), Trim(lamp_type_all.Text), "")

    '    Dim open_close As String
    '    If open = 1 Then
    '        open_close = "打开"
    '    Else
    '        open_close = "关闭"
    '    End If

    '    If box_control.Checked = True Then  '按电控箱控制
    '        If single_double = 1 Then  '显示单号路灯开关信息
    '            MsgBox(Trim(box_all.Text) & " 号区域单号灯" & open_close, , msg_box_title)
    '            ' Doing_now_text.AppendText(area_title & Trim(box_all.Text) & " 号电控箱单号灯" & open_close & Format(Now(), "yyyy-MM-dd HH:mm:ss") & vbCrLf)
    '            circle_string.Text = Trim(box_all.Text) & " 号区域单号灯" & open_close
    '        Else  '显示双号路灯开关信息
    '            MsgBox(Trim(box_all.Text) & " 号区域双号灯" & open_close, , msg_box_title)
    '            'Doing_now_text.AppendText(area_title & Trim(box_all.Text) & " 号电控箱双号灯" & open_close & Format(Now(), "yyyy-MM-dd HH:mm:ss") & vbCrLf)
    '            circle_string.Text = Trim(box_all.Text) & " 号区域双号灯" & open_close
    '        End If
    '    End If

    '    If type_control.Checked = True Then  '按类型控制
    '        If single_double = 1 Then
    '            '显示单号灯开关信息
    '            MsgBox(Trim(box_all.Text) & " 号区域 " & Trim(lamp_type_all.Text) & " 单号灯" & open_close, , msg_box_title)
    '            'Doing_now_text.AppendText(area_title & Trim(box_all.Text) & " 号电控箱 " & Trim(lamp_type_all.Text) & " 单号灯" & open_close & Format(Now(), "yyyy-MM-dd HH:mm:ss") & vbCrLf)
    '            circle_string.Text = Trim(box_all.Text) & " 号区域 " & Trim(lamp_type_all.Text) & " 单号灯" & open_close
    '        Else
    '            '显示双号灯开关信息
    '            MsgBox(Trim(box_all.Text) & " 号区域 " & Trim(lamp_type_all.Text) & " 双号灯" & open_close, , msg_box_title)
    '            ' Doing_now_text.AppendText(area_title & Trim(box_all.Text) & " 号电控箱 " & Trim(lamp_type_all.Text) & " 双号灯" & open_close & Format(Now(), "yyyy-MM-dd HH:mm:ss") & vbCrLf)
    '            circle_string.Text = Trim(box_all.Text) & " 号区域 " & Trim(lamp_type_all.Text) & " 双号灯" & open_close
    '        End If

    '    End If


    'End Sub

    '''' <summary>
    '''' 关闭所有的路灯,按区域、类型和单灯范围进行控制
    '''' </summary>
    '''' <remarks></remarks>
    '''' 
    'Private Sub close_all_lamp()
    '    If box_control.Checked = False And type_control.Checked = False And lamp_id_control.Checked = False Then
    '        MsgBox("请选择控制类型！", , msg_box_title)  '如果没有选择控制类型，提示选择控制类型后再进行下一步操作
    '        Exit Sub
    '    End If
    '    If box_control.Checked = True Then  '按区域范围进行控制
    '        If box_all.Text = "" Then  '如果区域文本框为空
    '            MsgBox("请选择区域！", , msg_box_title)
    '            box_all.Focus()  '光标定位在区域文本框
    '            Exit Sub
    '        End If
    '        '进行关闭确认
    '        If MsgBox("通信正常，是否关闭区域: " & Trim(box_all.Text) & "的所有景观灯?", MsgBoxStyle.YesNo, msg_box_title) = MsgBoxResult.No Then
    '            Exit Sub

    '        End If
    '        '按区域进行控制，向数据库中输入控制命令
    '        control_lamp_obj.Input_control_inf("", "区域", Trim(box_all.Text), "全闭", 0, "关闭灯", "0")
    '    End If


    '    If type_control.Checked = True Then  '按类型进行控制
    '        If box_all.Text = "" Then  '如果区域文本框为空
    '            MsgBox("请选择区域！", , msg_box_title)
    '            box_all.Focus()  '光标定位在区域文本框
    '            Exit Sub
    '        End If
    '        If lamp_type_all.Text = "" Then '如果类型文本框为空
    '            MsgBox("请选择景观灯类型", , msg_box_title)
    '            Exit Sub
    '        End If
    '        '进行关闭确认
    '        If MsgBox("通信正常，是否关闭区域: " & Trim(box_all.Text) & "中所有" & Trim(lamp_type_all.Text) & "?", MsgBoxStyle.YesNo, msg_box_title) = MsgBoxResult.No Then
    '            Exit Sub
    '        End If
    '        '按类型进行控制，向数据库发送控制命令
    '        control_lamp_obj.Input_control_inf(Trim(lamp_type_all.Text), "类型", Trim(box_all.Text), "类型全闭", 0, "关闭灯", "0")


    '    End If

    '    If lamp_id_control.Checked = True Then  '按单灯控制
    '        '进行关闭确认
    '        If MsgBox("通信正常，是否关闭区域: " & Trim(box_all.Text) & "中第" & Trim(lamp_id_all.Text) & "号" & Trim(lamp_type_all.Text), MsgBoxStyle.YesNo, msg_box_title) = MsgBoxResult.No Then
    '            Exit Sub
    '        End If
    '        '按单灯进行控制，向数据库发送控制命令
    '        Me.open_close_single_lamp(Trim(box_all.Text), Trim(lamp_type_all.Text), Trim(lamp_id_all_start.Text) & Trim(lamp_id_all.Text), 0)

    '    End If

    '    If box_control.Checked = True Then

    '        '按电控箱，提示关闭信息
    '        MsgBox("区域：" & Trim(box_all.Text) & " 关闭！", , msg_box_title)
    '        Doing_now_text.AppendText("区域：" & Trim(box_all.Text) & " 关闭！" & Format(Now(), "yyyy-MM-dd HH:mm:ss") & vbCrLf)
    '        Exit Sub
    '    End If

    '    If type_control.Checked = True Then  '按类型，提示关闭信息


    '        MsgBox("区域：" & Trim(box_all.Text) & " " & Trim(lamp_type_all.Text) & " 关闭！", , msg_box_title)
    '        Doing_now_text.AppendText("区域：" & Trim(box_all.Text) & " " & Trim(lamp_type_all.Text) & " 关闭！" & Format(Now(), "yyyy-MM-dd HH:mm:ss") & vbCrLf)
    '        Exit Sub
    '    End If

    '    If lamp_id_control.Checked = True Then  '按单灯，提示关闭信息

    '        MsgBox("区域：" & Trim(box_all.Text) & " " & Trim(lamp_type_all.Text) & " " & Trim(lamp_id_all.Text) & " 关闭！", , msg_box_title)
    '        Doing_now_text.AppendText("区域：" & Trim(box_all.Text) & " " & Trim(lamp_type_all.Text) & " 关闭！" & Trim(lamp_id_all.Text) & Format(Now(), "yyyy-MM-dd HH:mm:ss") & vbCrLf)
    '        Exit Sub
    '    End If

    '    MsgBox("关闭灯出错！", , msg_box_title)
    '    Doing_now_text.AppendText("关闭灯出错" & Format(Now(), "yyyy-MM-dd HH:mm:ss") & vbCrLf)


    'End Sub
    '''' <summary>
    '''' 时段控制的设置按钮相应函数
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    '''' <remarks></remarks>

    'Private Sub div_set_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Dim div_time_obj As New div_time_class
    '    ' work_string.Text = "设置时段控制"  '时段设置的信息提示
    '    If box_time_control.Checked = False And type_time_control.Checked = False Then
    '        MsgBox("请选择控制类型！", , msg_box_title)  '如果没有选择控制类型，则提示选择控制类型后，进行下一步操作
    '        Exit Sub
    '    Else

    '        If box_time_control.Checked = True Then  '设置区域控制下的景观灯
    '            If box_time.Text = "" Then
    '                MsgBox("请选择区域", , msg_box_title)
    '                box_time.Focus()
    '                Exit Sub
    '            End If
    '            '按区域范围进行设置
    '            div_time_obj.box_control_function(Trim(box_time.Text), Trim(mod_time.Text))
    '        End If
    '        If type_time_control.Checked = True Then  '设置某一类型的景观灯
    '            If box_time.Text = "" Then
    '                MsgBox("请选择区域", , msg_box_title)
    '                box_time.Focus()
    '                Exit Sub
    '            End If
    '            If type_time.Text = "" Then
    '                MsgBox("请选择景观灯类型", , msg_box_title)
    '                type_time.Focus()
    '                Exit Sub
    '            End If
    '            '按类型范围进行设置
    '            div_time_obj.type_control_function(Trim(box_time.Text), Trim(type_time.Text), Trim(mod_time.Text))
    '        End If

    '    End If
    '    div_time_obj.get_treelist_inf()  '刷新时段的树形列表
    'End Sub

    '''' <summary>
    '''' 设置批量查询的间隔时间
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    '''' <remarks></remarks>
    'Private Sub set_wait_time_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If set_wait_time.Text = "" Then  '如果没有选择超时时间
    '        MsgBox("请选择超时时间值", , msg_box_title)
    '        ComboBox_wait_time.Focus()
    '        Exit Sub
    '    End If

    '    If Val(Trim(ComboBox_wait_time.Text)) < 1 Or Val(Trim(ComboBox_wait_time.Text)) > 20 Then  '如果超时时间不在1-20之间
    '        MsgBox("请选择1-20之间的时间数值", , msg_box_title)
    '        ComboBox_wait_time.Focus()
    '        Exit Sub
    '    End If

    '    'circle_wait_time1 = Val(Trim(ComboBox_wait_time.Text))  '设置轮询的等待时间
    '    'circle_wait_time2 = Val(Trim(ComboBox_wait_time.Text))  '设置轮询的等待时间
    '    'circle_wait_time3 = Val(Trim(ComboBox_wait_time.Text))  '设置轮询的等待时间
    '    get_state_wait_time = Val(Trim(ComboBox_wait_time.Text))  '批量查询状态的间隔时间

    '    ' Doing_now_text.AppendText("轮询超时时间设置为" & Trim(ComboBox_wait_time.Text) & "秒" & vbCrLf)  '提示框中提示设置等待时间
    '    circle_string.Text = "轮询超时时间设置为" & Trim(ComboBox_wait_time.Text & "秒")  '提示栏中提示设置时间


    'End Sub




    '''' <summary>
    '''' 删除
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    '''' <remarks></remarks>
    'Private Sub 删除ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 删除ToolStripMenuItem.Click
    '    Dim delete_street_find_obj As New 删除查询区域
    '    delete_street_find_obj.Show()
    'End Sub

    'Private Sub 路灯状态统计ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    '用户可以查询特定范围内路灯的统计信息
    '    Dim static_lamp As New 路灯运行状态统计
    '    static_lamp.Show()
    'End Sub

    'Private Sub Treeview_menu_list_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
    '    If e.KeyCode = Keys.F1 Then
    '        Doing_now_text.Visible = False  '隐藏信息栏
    '    End If
    '    If e.KeyCode = Keys.F2 Then
    '        Doing_now_text.Visible = True  '显示信息栏
    '    End If
    'End Sub



    'Private Sub all_time_control_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    '控制所有景观灯
    '    box_time.Enabled = False
    '    type_time.Enabled = False
    'End Sub
    '''' <summary>
    '''' 时段控制按区域进行设置
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    '''' <remarks></remarks>
    'Private Sub box_time_control_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    '控制电控箱
    '    box_time.Enabled = True
    '    type_time.Enabled = False
    'End Sub

    'Private Sub lamp_time_control_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    '控制类型
    '    box_time.Enabled = True
    '    type_time.Enabled = True
    'End Sub

    'Private Sub box_time_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles box_time.DropDown
    '    '电控箱编号
    '    Com_inf.Select_box_name(city_all, area_all, street_all, box_time)
    'End Sub

    'Private Sub type_time_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles type_time.DropDown
    '    '按景观灯类型
    '    Dim rs As ADODB.Recordset
    '    Dim msg As String
    '    Dim sql As String

    '    msg = ""
    '    sql = "select * from box_lamptype_view where control_box_name='" & Trim(box_time.Text) & "'"
    '    rs = DBOperation.SelectSQL(sql, msg)

    '    If rs Is Nothing Then
    '        MsgBox("数据库查询出错")
    '        Exit Sub
    '    End If
    '    type_time.Items.Clear()

    '    While rs.EOF = False
    '        type_time.Items.Add(Trim(rs.Fields("type_string").Value))
    '        rs.MoveNext()
    '    End While

    '    rs.Close()
    '    rs = Nothing
    'End Sub
    '''' <summary>
    '''' 时段控制按类型进行设置
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    '''' <remarks></remarks>
    'Private Sub type_time_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Com_inf.Select_type_name(box_time, type_time, type_id)
    'End Sub

    'Private Sub control_box_name_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles control_box_name.DropDown
    '    '电控箱定位，下拉时添加电控箱名称
    '    Dim rs As ADODB.Recordset
    '    Dim msg As String
    '    Dim sql As String
    '    Dim conn As New ADODB.Connection
    '    DBOperation.OpenConn(conn)

    '    msg = ""
    '    sql = "select * from street_position "
    '    control_box_name.Items.Clear()
    '    rs = DBOperation.SelectSQL(conn, sql, msg)
    '    While rs.EOF = False
    '        control_box_name.Items.Add(Trim(rs.Fields("control_box_name").Value))  '添加电控箱名称

    '        rs.MoveNext()
    '    End While
    '    If control_box_name.Items.Count > 0 Then
    '        control_box_name.SelectedIndex = 0
    '    End If
    '    rs.Close()
    '    rs = Nothing
    '    conn.Close()
    '    conn = Nothing
    'End Sub

    '#Region "实时显示故障信息"
    '    Private Sub Alarm_list()
    '        Dim rs_alarm As New ADODB.Recordset
    '        Dim msg As String
    '        Dim sql As String
    '        Dim conn As New ADODB.Connection

    '        DBOperation.OpenConn(conn)

    '        msg = ""
    '        sql = "select * from alarm_view where total_num=1 and (result=1 or result=2) order by date desc"
    '        rs_alarm = DBOperation.SelectSQL(conn, sql, msg)  '
    '        If rs_alarm Is Nothing Then
    '            MsgBox(MSG_ERROR_STRING & " Alarm_list", , PROJECT_TITLE_STRING)
    '            conn.Close()
    '            conn = Nothing
    '            Exit Sub
    '        Else
    '            If rs_alarm.RecordCount > 0 Then

    '                '有故障显示报警提示栏
    '                While rs_alarm.EOF = False
    '                    'If rs_alarm.Fields("result").Value = 1 Or rs_alarm.Fields("result").Value = 2 Then
    '                    '    alarm_string &= "区域：" & Trim(rs_alarm.Fields("control_box_name").Value) & _
    '                    '    " 第" & Trim(rs_alarm.Fields("short_lampid").Value) & "盏灯 " & Trim(rs_alarm.Fields("problem_inf").Value) & _
    '                    '    " " & rs_alarm.Fields("date").Value & "   "
    '                    '    '故障字符串的信息
    '                    'End If
    '                    '将确认的故障写入到lamp_inf_record 表中
    '                    m_controllampobj.Input_problem(rs_alarm.Fields("result").Value, rs_alarm.Fields("lamp_id").Value)  '写入故障列表

    '                    rs_alarm.MoveNext()
    '                End While


    '            End If

    '            rs_alarm.Close()
    '            rs_alarm.ActiveConnection = Nothing
    '        End If
    '    End Sub

    '#End Region

    '''' <summary>
    '''' 描述景观灯的最新状态
    '''' </summary>
    '''' <param name="control_box_name">电控箱名称</param>
    '''' <remarks></remarks>
    'Public Sub lamp_state_show(ByVal control_box_name As String)
    '    Dim rs As ADODB.Recordset
    '    Dim msg As String
    '    Dim sql As String
    '    Dim type_string As String '景观灯的类型
    '    ' Dim node, node_type, node_inf As New TreeNode
    '    Dim type_index As Integer  '标志取景观灯类型
    '    Dim sum As Integer  '统计灯的总的个数
    '    Dim lv_light_on_num, lv_light_off_num, lv_light_problem_num, lv_no_return_num As Integer  '路灯数量
    '    Dim type_index_num As Integer  '景观灯类型的个数
    '    Dim result_tag, state_tag, power_tag As Integer   '灯的状态和结果,电功率
    '    Dim lamp_id_tag As String  '灯的编号
    '    Dim lamp_state_time As Date  '灯的状态的时间
    '    Dim conn As New ADODB.Connection
    '    ' Dim level2_num, level3_num As Integer
    '    DBOperation.OpenConn(conn)

    '    sum = 0
    '    msg = ""
    '    type_index = 0
    '    type_index_num = 0
    '    lamp_id_tag = ""

    '    lv_light_on_num = 0 '亮灯数量清零
    '    lv_light_off_num = 0  '暗灯数量清零
    '    lv_light_problem_num = 0  '故障灯数量清零
    '    lv_no_return_num = 0  '无返回值数量清零
    '    m_lvlighton = 0.0  '亮灯率清零
    '    m_lvlightoff = 0.0  '暗灯率清零
    '    m_lvlightproblem = 0.0  '故障率清零
    '    m_lvnoreturn = 0.0  '无返回值率清零

    '    sql = "select * from lamp_street where control_box_name='" & control_box_name & "' order by lamp_id"
    '    rs = DBOperation.SelectSQL(conn, sql, msg)
    '    If rs Is Nothing Then
    '        MsgBox(MSG_ERROR_STRING & "lamp_state_show", , PROJECT_TITLE_STRING)
    '        conn.Close()
    '        conn = Nothing
    '        Exit Sub
    '    Else
    '        If rs.RecordCount <= 0 Then
    '            MsgBox("该区域还没有增加灯信息", , PROJECT_TITLE_STRING)
    '            rs.Close()
    '            rs = Nothing
    '            conn.Close()
    '            conn = Nothing
    '            Exit Sub
    '        End If
    '        type_string = Trim(rs.Fields("type_string").Value)  '灯的类型
    '        result_tag = rs.Fields("result").Value
    '        state_tag = rs.Fields("state").Value
    '        power_tag = rs.Fields("power").Value
    '        lamp_id_tag = Val(Mid(Trim(rs.Fields("lamp_id").Value), 1, 4)) & "-" & Val(Mid(Trim(rs.Fields("lamp_id").Value), 5, 2)) & "-" & Val(Mid(Trim(rs.Fields("lamp_id").Value), 7, 3))
    '        lamp_state_time = rs.Fields("date").Value
    '        While rs.EOF = False
    '            'If type_index = 0 Then
    '            '    type_index_num += 1
    '            '    'node.Nodes.Add(type_string) '增加一个灯的类型的节点
    '            '    level2_num += 1  '第2级别的节点数加1
    '            '    level3_num = -1
    '            '    '  Me.AddTreeViewDelegate(type_string, Me.TreeView_lampinf, 1, 0, 1, level2_num, level3_num)
    '            '    type_index = 1
    '            'End If

    '            If type_string = Trim(rs.Fields("type_string").Value) Then
    '                '表示同一种类型，在此类型下增加每一盏灯的详细信息，信息包括：灯的编号，电流和电阻的AD值，灯的状态
    '                sum += 1
    '                If result_tag = 1 Or result_tag = 2 Then
    '                    '故障状态,排除了无返回信息的情况
    '                    'node.Nodes(type_index_num - 1).Nodes.Add(lamp_id_tag & " 故障")
    '                    ' level3_num += 1  '第3级别的节点数加1
    '                    '   Me.AddTreeViewDelegate(lamp_id_tag & " 故障", Me.TreeView_lampinf, 2, type_index_num - 1, 4, level2_num, level3_num)
    '                    lv_light_problem_num += 1

    '                End If

    '                If (result_tag = 0 Or result_tag = 4) And state_tag = 3 Then
    '                    '控制后，关闭状态
    '                    ' level3_num += 1  '第3级别的节点数加1
    '                    lv_light_off_num += 1
    '                    ' Me.AddTreeViewDelegate(lamp_id_tag & " 关闭", Me.TreeView_lampinf, 2, type_index_num - 1, 6, level2_num, level3_num)
    '                End If

    '                If (result_tag = 0 Or result_tag = 4) And state_tag = 4 Then
    '                    '控制后，打开状态
    '                    '  level3_num += 1  '第3级别的节点数加1
    '                    lv_light_on_num += 1
    '                    'Me.AddTreeViewDelegate(lamp_id_tag & " 打开", Me.TreeView_lampinf, 2, type_index_num - 1, 6, level2_num, level3_num)
    '                End If

    '                If result_tag = 3 Then  '无返回状态，只统计不在路灯的颜色上标志出来
    '                    'node.Nodes(type_index_num - 1).Nodes.Add(lamp_id_tag & " 无返回值")
    '                    ' level3_num += 1  '第3级别的节点数加1
    '                    ' Me.AddTreeViewDelegate(lamp_id_tag & " 无返回值", Me.TreeView_lampinf, 2, type_index_num - 1, 5, level2_num, level3_num)
    '                    lv_no_return_num += 1
    '                End If

    '                If state_tag = 0 And result_tag = 0 Then
    '                    '   BackgroundWorker_state_inf.ReportProgress(3)
    '                    'node.Nodes(type_index_num - 1).Nodes.Add(lamp_id_tag & " 关闭")
    '                    '  level3_num += 1  '第3级别的节点数加1
    '                    'Me.AddTreeViewDelegate(lamp_id_tag & " 关闭", Me.TreeView_lampinf, 2, type_index_num - 1, 3, level2_num, level3_num)

    '                    lv_light_off_num += 1
    '                End If

    '                If state_tag = 1 And result_tag = 0 Then
    '                    '   BackgroundWorker_state_inf.ReportProgress(3)
    '                    'node.Nodes(type_index_num - 1).Nodes.Add(lamp_id_tag & " 关闭")
    '                    '  level3_num += 1  '第3级别的节点数加1
    '                    ' Me.AddTreeViewDelegate(lamp_id_tag & " 打开", Me.TreeView_lampinf, 2, type_index_num - 1, 2, level2_num, level3_num)

    '                    lv_light_on_num += 1
    '                End If


    '                'End If  '故障灯
    '                rs.MoveNext()
    '                If rs.EOF = False Then
    '                    result_tag = rs.Fields("result").Value
    '                    state_tag = rs.Fields("state").Value
    '                    power_tag = rs.Fields("power").Value
    '                    lamp_id_tag = Val(Mid(Trim(rs.Fields("lamp_id").Value), 1, 4)) & "-" & Val(Mid(Trim(rs.Fields("lamp_id").Value), 5, 2)) & "-" & Val(Mid(Trim(rs.Fields("lamp_id").Value), 7, 3))
    '                    lamp_state_time = rs.Fields("date").Value
    '                End If

    '            Else
    '                '更换了另一种类型
    '                type_string = Trim(rs.Fields("type_string").Value)
    '                type_index = 0
    '                result_tag = rs.Fields("result").Value
    '                state_tag = rs.Fields("state").Value
    '                power_tag = rs.Fields("power").Value
    '                lamp_id_tag = Val(Mid(Trim(rs.Fields("lamp_id").Value), 1, 4)) & "-" & Val(Mid(Trim(rs.Fields("lamp_id").Value), 5, 2)) & "-" & Val(Mid(Trim(rs.Fields("lamp_id").Value), 7, 3))
    '                lamp_state_time = rs.Fields("date").Value
    '                '  rs.MoveNext()

    '            End If

    '        End While

    '    End If

    '    ''
    '    If sum <> 0 Then
    '        m_lvlighton = Format(lv_light_on_num / sum * 100, "00.00")
    '        m_lvlightoff = Format(lv_light_off_num / sum * 100, "00.00")
    '        m_lvlightproblem = Format(lv_light_problem_num / sum * 100, "00.00")
    '        m_lvnoreturn = 100 - m_lvlighton - m_lvlightoff - m_lvlightproblem
    '    Else
    '        m_lvlighton = 0
    '        m_lvlightoff = 0
    '        m_lvlightproblem = 0
    '        m_lvnoreturn = 0
    '    End If
    '    'Me.SetTextDelegate("", False, Me.RichTextBox_static_inf)
    '    'Me.SetTextDelegate("区域名称：" & control_box_name & vbCrLf & vbCrLf, True, Me.RichTextBox_static_inf)
    '    'Me.SetTextDelegate("亮灯率：" & Format(lv_light_on, "0.00") & "%" & vbCrLf & vbCrLf, True, Me.RichTextBox_static_inf)
    '    'Me.SetTextDelegate("熄灯率：" & Format(lv_light_off, "0.00") & "%" & vbCrLf & vbCrLf, True, Me.RichTextBox_static_inf)
    '    'Me.SetTextDelegate("故障率：" & Format(lv_light_problem, "0.00") & "%" & vbCrLf & vbCrLf, True, Me.RichTextBox_static_inf)
    '    'Me.SetTextDelegate("无返回信息率：" & Format(lv_no_return, "0.00") & "%" & vbCrLf & vbCrLf, True, Me.RichTextBox_static_inf)
    '    'Me.SetTextDelegate("时间：" & Now() & vbCrLf, True, Me.RichTextBox_static_inf)


    '    rs.Close()
    '    rs = Nothing
    '    conn.Close()
    '    conn = Nothing

    'End Sub
    '''' <summary>
    ''''  打开所有灯，关闭必须为暗的灯,只修改数据库中的值，不发送命令
    '''' </summary>
    '''' <remarks></remarks>
    'Private Sub Open_and_Close()

    '    Dim rs, rs_lamp As New ADODB.Recordset
    '    Dim conn As New ADODB.Connection
    '    Dim msg As String
    '    Dim sql As String
    '    '  Dim ox_str As String

    '    DBOperation.OpenConn(conn)

    '    msg = ""
    '    sql = "select * from control_box order by control_box_id"
    '    rs = DBOperation.SelectSQL(conn, sql, msg)
    '    While rs.EOF = False
    '        'ox_str = Com_inf.Dec_to_ox(Trim(rs.Fields("control_box_id").Value), 2)  '将电控箱编号转换成两位的十六进制数
    '        'ox_str = ox_str & " 00 01 11 11 64 "
    '        'control_lamp_obj.Input_db_control(ox_str)   '将命令写入到控制命令数据库中
    '        '控制命令提示栏

    '        ' 修改lamp_inf表中的手工控制参数
    '        m_controllampobj.Input_hand_control_table(Trim(rs.Fields("control_box_id").Value), -1, "全开", "全夜灯", 100)

    '        rs.MoveNext()
    '    End While

    '    '关闭所有必须为暗的景观灯
    '    sql = "select * from lamp_off_list"
    '    rs = DBOperation.SelectSQL(conn, sql, msg)
    '    While rs.EOF = False
    '        ' control_lamp_obj.Input_db_control(Trim(rs.Fields("lampid_off_string").Value))   '将命令写入到控制命令数据库中
    '        sql = "select * from lamp_inf where lamp_id='" & Trim(rs.Fields("lamp_id").Value) & "'"
    '        rs_lamp = DBOperation.SelectSQL(conn, sql, msg)
    '        If rs_lamp.RecordCount > 0 Then
    '            sql = "update lamp_inf set state=3 , result=4 where lamp_id='" & Trim(rs.Fields("lamp_id").Value) & "'"
    '            DBOperation.ExecuteSQL(conn, sql, msg)
    '            'rs_lamp.Fields("state").Value = 3
    '            'rs_lamp.Fields("result").Value = 4
    '            'rs_lamp.Update()
    '        End If
    '        rs.MoveNext()
    '        rs_lamp.Close()
    '    End While


    '    rs_lamp = Nothing
    '    rs.Close()
    '    rs = Nothing
    '    conn.Close()
    '    conn = Nothing
    'End Sub

    '主控界面中的路灯信息线程
    'Private lamp_road_string As String   '路灯状态提示栏的字符串
    'Private alarm_string As String  '报警信息提示栏的字符串

    ''轮询线程变量
    'Private time_wait1, time_wait2, time_wait3 As Integer    '等待计时
    'Private set_tag As Integer '标志路灯起始参数
    'Private StatusStrip_boxname1, StatusStrip_typestring1, StatusStrip_control_box1, StatusStrip_lamp_id1 As String  '状态栏上的提示信息
    'Private StatusStrip_boxname2, StatusStrip_typestring2, StatusStrip_control_box2, StatusStrip_lamp_id2 As String  '状态栏上的提示信息
    'Private StatusStrip_boxname3, StatusStrip_typestring3, StatusStrip_control_box3, StatusStrip_lamp_id3 As String  '状态栏上的提示信息
    'Private state1_string, state2_string, state3_string As String   '灯的状态
    'Private result1, result2, result3 As Integer  '对比结果
    'Private state1, state2, state3 As Integer '状态
    'Private lamp_pos As System.Drawing.Point   '轮询的坐标
    'Private circle_wait_time1, circle_wait_time2, circle_wait_time3  '轮询的超时时间
    'Private find_tag1, find_tag2, find_tag3 As Integer  '查询标志

    '地图编号
    'Private map_id As Integer
    'Private map_change_tag As Integer


    'circle_wait_time1 = 12 '轮询等待时间
    'circle_wait_time2 = 12 '轮询等待时间
    'circle_wait_time3 = 12 '轮询等待时间


    '#Region "移动文字"
    '    Private Sub mov_tex()

    '        Me.MovtextDelegate(lamp_road_inf, GroupBox_street_inf)  '灯的统计信息
    '        Me.MovtextDelegate(alarm_string_show, GroupBox_alarm)  '故障灯的信息

    '    End Sub
    '#End Region

    'Public Sub MovtextDelegate(ByVal Label As Windows.Forms.Label, ByVal Groupbox As Windows.Forms.GroupBox)
    '    If Label.InvokeRequired Then
    '        Dim Labelobj As Movtext = New Movtext(AddressOf MovtextDelegate)
    '        Me.Invoke(Labelobj, New Object() {Label, Groupbox})
    '    Else
    '        If Label.Name = "lamp_road_inf" Then
    '            Label.Left = Label.Left - 5
    '            If Label.Left + Label.Width <= Groupbox.Left Then
    '                Label.Left = Groupbox.Left + Groupbox.Width
    '            End If

    '        Else
    '            Label.Left = Label.Left - 5

    '            If Label.Left + Label.Width <= 0 Then
    '                Label.Left = Groupbox.Width

    '            End If
    '        End If

    '    End If
    'End Sub

    '#Region "报警提示栏"
    '    Public Sub alarm_show()
    '        Dim rs_alarm As ADODB.Recordset
    '        Dim msg As String
    '        Dim sql As String
    '        Dim conn As New ADODB.Connection
    '        DBOperation.OpenConn(conn)
    '        msg = ""

    '        sql = "select * from alarm_view order by date desc"
    '        rs_alarm = DBOperation.SelectSQL(conn, sql, msg)  '报警提示
    '        If rs_alarm Is Nothing Then  '数据库查询出错
    '            MsgBox("数据库查询出错")
    '            conn.Close()
    '            conn = Nothing
    '            Exit Sub
    '        Else
    '            ' alarm_string = ""   '清空报警提示字符串
    '            If rs_alarm.RecordCount > 0 Then

    '                '有故障显示报警提示栏
    '                While rs_alarm.EOF = False

    '                    If rs_alarm.Fields("result").Value = 1 Or rs_alarm.Fields("result").Value = 2 Then
    '                        ' alarm_string &= rs_alarm.Fields("lamp_id").Value & " " & rs_alarm.Fields("date").Value & " " & rs_alarm.Fields("problem_inf").Value
    '                        '故障字符串的信息
    '                    End If

    '                    rs_alarm.MoveNext()
    '                End While

    '            End If

    '            rs_alarm.Close()
    '            rs_alarm = Nothing
    '        End If
    '        conn.Close()
    '        conn = Nothing
    '    End Sub
    '#End Region


    '    Private Sub find_control_box_inf(ByVal control_box_name As String)  '标签显示电控箱信息
    '        '鼠标移到上面后显示
    '        Dim rs As New ADODB.Recordset
    '        Dim msg As String
    '        Dim sql As String
    '        Dim control_box_tip As String
    '        Dim control_lamp_obj As New control_lamp
    '        Dim type_num As Integer  '记录某一种类型的灯的个数
    '        Dim type_string As String  '记录电控箱控制下的类型
    '        Dim conn As New ADODB.Connection
    '        DBOperation.OpenConn(conn)
    '        type_num = 0
    '        msg = ""
    '        control_box_tip = "" & vbCrLf
    '        sql = "select * from lamp_street where control_box_name='" & control_box_name & "' order by type_string"
    '        rs = DBOperation.SelectSQL(conn, sql, msg)
    '        If rs Is Nothing Then
    '            MsgBox("数据库查询出错", , msg_box_title)
    '            conn.Close()
    '            conn = Nothing
    '            rs.MoveNext()
    '        Else
    '            control_box_tip &= "区域：" & control_box_name & vbCrLf
    '            If rs.RecordCount > 0 Then
    '                type_string = Trim(rs.Fields("type_string").Value)

    '                While rs.EOF = False
    '                    If type_string = Trim(rs.Fields("type_string").Value) Then '同一种类型的个数加一
    '                        type_num += 1
    '                    Else
    '                        control_box_tip &= type_string & " " & type_num & "盏" & vbCrLf  '统计一种类型的灯的盏数
    '                        type_string = Trim(rs.Fields("type_string").Value)  '取新的类型
    '                        type_num = 1
    '                    End If

    '                    rs.MoveNext()
    '                End While
    '                control_box_tip &= type_string & " " & type_num & "盏" & vbCrLf
    '            End If
    '        End If

    '        ToolTip_lamp.Show(control_box_tip, Me.map)

    '        'lamp_inf.Text = lamp_tip
    '        ToolTip_lamp.UseFading = True
    '        rs.Close()
    '        rs = Nothing
    '        conn.Close()
    '        conn = Nothing
    '    End Sub

    '#Region "轮询路灯的当前状态"

    '    Private Sub state_read_fun(ByVal id As Integer)
    '        '轮询景观灯的当前状态
    '        Dim i1, i2, i3 As Integer
    '        Dim rs As ADODB.Recordset
    '        Dim msg As String
    '        Dim sql As String
    '        Dim lamp_id_tag As String  '当前灯的编号标识
    '        'Dim control_string As String  '控制字符串
    '        Dim conn As New ADODB.Connection
    '        DBOperation.OpenConn(conn)

    '        msg = ""
    '        sql = ""

    '        If id = 1 Then
    '            time_wait1 = 0  '轮询的等待时间

    '            sql = "select * from lamp_street where lamp_id >='" & lamp_id_start & "' and control_box_id='0001' or control_box_id='0002' order by lamp_id"

    '        End If
    '        If id = 2 Then
    '            time_wait2 = 0  '轮询的等待时间

    '            sql = "select * from lamp_street where lamp_id >='" & lamp_id_start & "' and control_box_id='0003' or control_box_id='0005' order by lamp_id"

    '        End If
    '        If id = 3 Then
    '            time_wait3 = 0  '轮询的等待时间

    '            sql = "select * from lamp_street where lamp_id >='" & lamp_id_start & "' and control_box_id='0004' or control_box_id='0006' order by lamp_id"

    '        End If

    '        rs = DBOperation.SelectSQL(conn, sql, msg)

    '        If rs Is Nothing Then  '数据库查询出错
    '            MsgBox("数据库查询出错", , msg_box_title)
    '            conn.Close()
    '            conn = Nothing
    '            Exit Sub
    '        End If
    '        Dim control_string As String
    '        While rs.EOF = False
    '            If set_tag = 1 Then  '手工控制开始,线程重新从手工控制的灯号开始轮询
    '                rs.Close()
    '                rs = Nothing
    '                set_tag = 0  '将手工控制标准置零
    '                conn.Close()
    '                conn = Nothing
    '                Exit Sub
    '            End If
    '            lamp_id_tag = Trim(rs.Fields("lamp_id").Value)  '标记需要查询的路灯编号
    '            control_string = control_lamp_obj.Set_control_inf(lamp_id_tag)  '设置控制命令
    '            control_lamp_obj.Input_db_control(control_string)   '发送控制命令

    '            '获取当前的路灯信息
    '            If id = 1 Then
    '                StatusStrip_boxname1 = Trim(rs.Fields("control_box_name").Value) '电控箱名称
    '                StatusStrip_typestring1 = Trim(rs.Fields("type_string").Value) '景观灯类型
    '                StatusStrip_lamp_id1 = Trim(rs.Fields("lamp_id").Value) '灯的编号


    '                '提示正在获取的灯的信息
    '                Me.SetTextLabelDelegate("正在获取" & StatusStrip_boxname1 & " " & StatusStrip_typestring1 & " " & Val(Mid(StatusStrip_lamp_id1, 7, 4)) & "号灯信息", Tool, "circle_string")

    '            End If

    '            If id = 2 Then
    '                StatusStrip_boxname2 = Trim(rs.Fields("control_box_name").Value) '电控箱名称
    '                StatusStrip_typestring2 = Trim(rs.Fields("type_string").Value) '景观灯类型
    '                StatusStrip_lamp_id2 = Trim(rs.Fields("lamp_id").Value) '灯的编号


    '                '提示正在获取的灯的信息
    '                Me.SetTextLabelDelegate("正在获取" & StatusStrip_boxname2 & " " & StatusStrip_typestring2 & " " & Val(Mid(StatusStrip_lamp_id2, 7, 4)) & "号灯信息", Tool, "circle_string")

    '            End If

    '            If id = 3 Then
    '                StatusStrip_boxname3 = Trim(rs.Fields("control_box_name").Value) '电控箱名称
    '                StatusStrip_typestring3 = Trim(rs.Fields("type_string").Value) '景观灯类型
    '                StatusStrip_lamp_id3 = Trim(rs.Fields("lamp_id").Value) '灯的编号


    '                '提示正在获取的灯的信息
    '                Me.SetTextLabelDelegate("正在获取" & StatusStrip_boxname3 & " " & StatusStrip_typestring3 & " " & Val(Mid(StatusStrip_lamp_id3, 7, 4)) & "号灯信息", Tool, "circle_string")

    '            End If

    '            i1 = 0



    '            Dim control_time As Date
    '            control_time = Now  '记录下发送控制命令时间，返回状态的时间必须在此之后，才是该命令查询的状态
    '            Dim find_num As Integer = 1

    '            '等待回复信息
    '            If id = 1 Then
    '                While i1 < circle_wait_time1
    '                    If Me.change_map_size_tag = 1 Then
    '                        '地图大小发生变化
    '                        Me.change_map_size_tag = 0
    '                        Exit While
    '                    End If

    '                    find_state(lamp_id_tag, control_time, id)  '查找标志路灯的状态返回
    '                    System.Threading.Thread.Sleep(1000)  '线程休眠1秒
    '                    i1 += 1
    '                    If find_tag1 = 1 Then    '如果找到了当前路灯的返回值
    '                        'System.Threading.Thread.Sleep(1000)
    '                        '将编号在其后的10盏赋同样的状态

    '                        Exit While
    '                    Else
    '                        'If i1 = circle_wait_time1 - 2 And find_num = 1 Then  '第一次没有接收到信息，则发第二遍
    '                        '    control_lamp_obj.Input_db_control(control_string)   '发送控制命令
    '                        '    i1 = 0
    '                        '    time_wait1 = 0
    '                        '    find_num += 1

    '                        'End If
    '                    End If
    '                    If BackgroundWorker_circle_state.CancellationPending = True Then  '线程被挂起
    '                        lamp_id_start = lamp_id_tag  '轮询的第一盏灯的编号
    '                        rs.Close()
    '                        rs = Nothing
    '                        conn.Close()
    '                        conn = Nothing
    '                        Exit Sub
    '                    End If
    '                End While
    '                time_wait1 = 0 '将计时器置0
    '                find_tag1 = 0  '找寻标志清0
    '            End If

    '            '等待回复信息
    '            i2 = 0
    '            If id = 2 Then
    '                While i2 < circle_wait_time2
    '                    If Me.change_map_size_tag = 1 Then
    '                        '地图大小发生变化
    '                        Me.change_map_size_tag = 0
    '                        Exit While
    '                    End If

    '                    find_state(lamp_id_tag, control_time, id)  '查找标志路灯的状态返回
    '                    System.Threading.Thread.Sleep(1000)  '线程休眠1秒
    '                    i2 += 1
    '                    If find_tag2 = 1 Then    '如果找到了当前路灯的返回值
    '                        'System.Threading.Thread.Sleep(1000)
    '                        Exit While
    '                    Else
    '                        'If i2 = circle_wait_time2 - 2 And find_num = 1 Then  '第一次没有接收到信息，则发第二遍
    '                        '    control_lamp_obj.Input_db_control(control_string)   '发送控制命令
    '                        '    i2 = 0
    '                        '    time_wait2 = 0
    '                        '    find_num += 1

    '                        'End If
    '                    End If
    '                    If BackgroundWorker_circle2.CancellationPending = True Then  '线程被挂起
    '                        lamp_id_start = lamp_id_tag  '轮询的第一盏灯的编号
    '                        rs.Close()
    '                        rs = Nothing
    '                        conn.Close()
    '                        conn = Nothing
    '                        Exit Sub
    '                    End If
    '                End While
    '                time_wait2 = 0 '将计时器置0
    '                find_tag2 = 0  '找寻标志清0
    '            End If

    '            '等待回复信息
    '            i3 = 0
    '            If id = 3 Then

    '                While i3 < circle_wait_time3
    '                    If Me.change_map_size_tag = 1 Then
    '                        '地图大小发生变化
    '                        Me.change_map_size_tag = 0
    '                        Exit While
    '                    End If

    '                    find_state(lamp_id_tag, control_time, id)  '查找标志路灯的状态返回
    '                    System.Threading.Thread.Sleep(1000)  '线程休眠1秒
    '                    i3 += 1
    '                    If find_tag3 = 1 Then    '如果找到了当前路灯的返回值
    '                        'System.Threading.Thread.Sleep(1000)
    '                        Exit While
    '                    Else
    '                        'If i3 = circle_wait_time3 - 2 And find_num = 1 Then  '第一次没有接收到信息，则发第二遍
    '                        '    control_lamp_obj.Input_db_control(control_string)   '发送控制命令
    '                        '    i3 = 0
    '                        '    time_wait3 = 0
    '                        '    find_num += 1

    '                        'End If
    '                    End If
    '                    If BackgroundWorker_circle3.CancellationPending = True Then  '线程被挂起
    '                        lamp_id_start = lamp_id_tag  '轮询的第一盏灯的编号
    '                        rs.Close()
    '                        rs = Nothing
    '                        conn.Close()
    '                        conn = Nothing
    '                        Exit Sub
    '                    End If
    '                End While
    '                time_wait3 = 0 '将计时器置0
    '                find_tag3 = 0  '找寻标志清0
    '            End If


    '            '灯的信息找到后将其后
    '            rs.MoveNext()
    '        End While

    '        rs.Close()
    '        rs = Nothing
    '        conn.Close()
    '        conn = Nothing
    '        lamp_id_start = Me.Get_first_lamp_id


    '    End Sub

    '#End Region

    '#Region "单灯查询状态"

    '    Private Sub find_state(ByVal lamp_id_tag As String, ByVal control_time As Date, ByVal id As Integer)


    '        Dim msg As String
    '        Dim sql As String
    '        'Dim pos As System.Drawing.Point
    '        Dim state_string As String  ' A,暗；B,亮；C，该亮非亮；D该暗非暗
    '        Dim conn As New ADODB.Connection
    '        Dim lamp_state As Integer   '灯的状态
    '        DBOperation.OpenConn(conn)
    '        If id = 1 Then
    '            Me.StatusStrip_lamp_id1 = lamp_id_tag
    '        End If
    '        If id = 2 Then
    '            Me.StatusStrip_lamp_id2 = lamp_id_tag
    '        End If
    '        If id = 3 Then
    '            Me.StatusStrip_lamp_id3 = lamp_id_tag
    '        End If

    '        Me.show_time = control_time

    '        If BackgroundWorker_circle_state.CancellationPending = True Then
    '            Exit Sub
    '        End If

    '        If BackgroundWorker_circle2.CancellationPending = True Then
    '            Exit Sub
    '        End If


    '        If BackgroundWorker_circle3.CancellationPending = True Then
    '            Exit Sub
    '        End If

    '        If id = 1 Then
    '            If time_wait1 >= circle_wait_time1 - 1 Then  '判断时间，超时
    '                '没有有返回信息
    '                Dim rs_lamp As New ADODB.Recordset
    '                msg = ""
    '                sql = "select * from lamp_inf where lamp_id='" & lamp_id_tag & "'"

    '                rs_lamp = DBOperation.SelectSQL(conn, sql, msg)  '在路灯的表中找到当前需要查询的路灯的信息

    '                If rs_lamp.RecordCount > 0 Then  '如果存在该路灯
    '                    lamp_state = rs_lamp.Fields("state").Value   '记录灯的状态
    '                    rs_lamp.Fields("result").Value = 3    '无状态返回，将路灯的信息表中标志故障
    '                    rs_lamp.Update()

    '                End If
    '                Me.SetTextDelegate(Me.StatusStrip_boxname1 & " " & Me.StatusStrip_typestring1 & " " & Mid(Me.StatusStrip_lamp_id1, 7, 3) & "号灯信息返回超时！" & Now & vbCrLf & vbCrLf, True, Me.Doing_now_text)


    '                rs_lamp.Close()
    '                rs_lamp = Nothing

    '                '将超时信息输入到故障表中
    '                '判断故障列表中是否存在该路灯的该故障

    '                control_lamp_obj.Input_problem(3, lamp_id_tag, lamp_state)  '写入故障列表
    '                time_wait1 = 0  '轮询的等待时间置为0
    '                conn.Close()
    '                conn = Nothing
    '                Exit Sub
    '            End If
    '        End If

    '        If id = 2 Then
    '            If time_wait2 >= circle_wait_time2 - 1 Then  '判断时间，超时
    '                '没有有返回信息
    '                Dim rs_lamp As New ADODB.Recordset
    '                msg = ""
    '                sql = "select * from lamp_inf where lamp_id='" & lamp_id_tag & "'"

    '                rs_lamp = DBOperation.SelectSQL(conn, sql, msg)  '在路灯的表中找到当前需要查询的路灯的信息

    '                If rs_lamp.RecordCount > 0 Then  '如果存在该路灯
    '                    lamp_state = rs_lamp.Fields("state").Value   '记录灯的状态
    '                    rs_lamp.Fields("result").Value = 3    '无状态返回，将路灯的信息表中标志故障
    '                    rs_lamp.Update()

    '                End If

    '                Me.SetTextDelegate(Me.StatusStrip_boxname2 & " " & Me.StatusStrip_typestring2 & " " & Mid(Me.StatusStrip_lamp_id2, 7, 3) & "号灯信息返回超时！" & Now & vbCrLf & vbCrLf, True, Me.Doing_now_text)

    '                rs_lamp.Close()
    '                rs_lamp = Nothing

    '                '将超时信息输入到故障表中
    '                '判断故障列表中是否存在该路灯的该故障

    '                control_lamp_obj.Input_problem(3, lamp_id_tag, lamp_state)  '写入故障列表
    '                time_wait2 = 0  '轮询的等待时间置为0
    '                conn.Close()
    '                conn = Nothing
    '                Exit Sub
    '            End If
    '        End If

    '        If id = 3 Then
    '            If time_wait3 >= circle_wait_time3 - 1 Then  '判断时间，超时
    '                '没有有返回信息
    '                Dim rs_lamp As New ADODB.Recordset
    '                msg = ""
    '                sql = "select * from lamp_inf where lamp_id='" & lamp_id_tag & "'"

    '                rs_lamp = DBOperation.SelectSQL(conn, sql, msg)  '在路灯的表中找到当前需要查询的路灯的信息

    '                If rs_lamp.RecordCount > 0 Then  '如果存在该路灯
    '                    lamp_state = rs_lamp.Fields("state").Value   '记录灯的状态
    '                    rs_lamp.Fields("result").Value = 3    '无状态返回，将路灯的信息表中标志故障
    '                    rs_lamp.Update()

    '                End If

    '                Me.SetTextDelegate(Me.StatusStrip_boxname3 & " " & Me.StatusStrip_typestring3 & " " & Mid(Me.StatusStrip_lamp_id3, 7, 3) & "号灯信息返回超时！" & Now & vbCrLf & vbCrLf, True, Me.Doing_now_text)

    '                rs_lamp.Close()
    '                rs_lamp = Nothing

    '                '将超时信息输入到故障表中
    '                '判断故障列表中是否存在该路灯的该故障

    '                control_lamp_obj.Input_problem(3, lamp_id_tag, lamp_state)  '写入故障列表
    '                time_wait3 = 0  '轮询的等待时间置为0
    '                conn.Close()
    '                conn = Nothing
    '                Exit Sub
    '            End If
    '        End If


    '        msg = ""
    '        '对比路灯状态返回值与操作值 

    '        state_string = control_lamp_obj.Get_actual_state(lamp_id_tag, control_time, hand_control_tag)  '获取路灯的运行状态
    '        If state_string = "" Then
    '            'MsgBox("查询出错！")
    '            If id = 1 Then
    '                If find_tag1 = 0 Then
    '                    time_wait1 += 1
    '                    If time_wait1 = 1 Then
    '                        Me.SetTextDelegate(">>>>正在接收" & Me.StatusStrip_boxname1 & " " & Me.StatusStrip_typestring1 & " " & Mid(Me.StatusStrip_lamp_id1, 7, 3) & "号灯的信息请稍候..." & "  " & Now & vbCrLf, True, Me.Doing_now_text)
    '                    End If
    '                End If
    '            End If

    '            If id = 2 Then
    '                If find_tag2 = 0 Then
    '                    time_wait2 += 1
    '                    If time_wait2 = 1 Then
    '                        Me.SetTextDelegate(">>>>正在接收" & Me.StatusStrip_boxname2 & " " & Me.StatusStrip_typestring2 & " " & Mid(Me.StatusStrip_lamp_id2, 7, 3) & "号灯的信息请稍候..." & "  " & Now & vbCrLf, True, Me.Doing_now_text)
    '                    End If
    '                End If
    '            End If

    '            If id = 3 Then

    '                If find_tag3 = 0 Then
    '                    time_wait3 += 1
    '                    If time_wait3 = 1 Then
    '                        Me.SetTextDelegate(">>>>正在接收" & Me.StatusStrip_boxname3 & " " & Me.StatusStrip_typestring3 & " " & Mid(Me.StatusStrip_lamp_id3, 7, 3) & "号灯的信息请稍候..." & "  " & Now & vbCrLf, True, Me.Doing_now_text)
    '                    End If
    '                End If
    '            End If
    '            conn.Close()
    '            conn = Nothing
    '            Exit Sub
    '        Else
    '            Dim rs As ADODB.Recordset
    '            sql = "select * from lamp_inf where lamp_id='" & lamp_id_tag & "'"
    '            rs = DBOperation.SelectSQL(conn, sql, msg)
    '            If rs Is Nothing Then
    '                MsgBox("查询数据库出错", , msg_box_title)
    '                conn.Close()
    '                conn = Nothing
    '                Exit Sub
    '            Else

    '                If rs.RecordCount > 0 Then
    '                    If id = 1 Then
    '                        find_tag1 = 1 '找到数据
    '                        lamp_state = rs.Fields("state").Value   '灯的状态
    '                        ' Me.StatusStrip_control_box = Trim(rs.Fields("control_box_id").Value)   '提示栏中的控制箱编号
    '                        Me.StatusStrip_control_box1 = Mid(lamp_id_tag, 1, 4)   '提示栏中的控制箱编号
    '                        Me.StatusStrip_lamp_id1 = lamp_id_tag   '提示栏中的路灯编号
    '                        rs.Fields("date").Value = Now    '更新路灯信息表中的时间信息
    '                        Me.SetTextDelegate("区域：" & Me.StatusStrip_boxname1 & vbCrLf, True, Me.Doing_now_text)
    '                        Me.SetTextDelegate("景观灯类型：" & Me.StatusStrip_typestring1 & vbCrLf, True, Me.Doing_now_text)
    '                        Me.SetTextDelegate("景观灯编号：" & Mid(Me.StatusStrip_lamp_id1, 7, 3) & vbCrLf, True, Me.Doing_now_text)
    '                        Me.SetTextDelegate("电阻AD值：" & control_lamp_obj.Get_dianzu_ad & vbCrLf, True, Me.Doing_now_text)
    '                        Me.SetTextDelegate("电流AD值：" & control_lamp_obj.Get_current_ad & vbCrLf, True, Me.Doing_now_text)

    '                    End If
    '                    If id = 2 Then
    '                        find_tag2 = 1
    '                        lamp_state = rs.Fields("state").Value   '灯的状态
    '                        ' Me.StatusStrip_control_box = Trim(rs.Fields("control_box_id").Value)   '提示栏中的控制箱编号
    '                        Me.StatusStrip_control_box2 = Mid(lamp_id_tag, 1, 4)   '提示栏中的控制箱编号
    '                        Me.StatusStrip_lamp_id2 = lamp_id_tag   '提示栏中的路灯编号
    '                        rs.Fields("date").Value = Now    '更新路灯信息表中的时间信息
    '                        Me.SetTextDelegate("区域：" & Me.StatusStrip_boxname2 & vbCrLf, True, Me.Doing_now_text)
    '                        Me.SetTextDelegate("景观灯类型：" & Me.StatusStrip_typestring2 & vbCrLf, True, Me.Doing_now_text)
    '                        Me.SetTextDelegate("景观灯编号：" & Mid(Me.StatusStrip_lamp_id2, 7, 3) & vbCrLf, True, Me.Doing_now_text)
    '                        Me.SetTextDelegate("电阻AD值：" & control_lamp_obj.Get_dianzu_ad & vbCrLf, True, Me.Doing_now_text)
    '                        Me.SetTextDelegate("电流AD值：" & control_lamp_obj.Get_current_ad & vbCrLf, True, Me.Doing_now_text)

    '                    End If

    '                    If id = 3 Then
    '                        find_tag3 = 1
    '                        lamp_state = rs.Fields("state").Value   '灯的状态
    '                        ' Me.StatusStrip_control_box = Trim(rs.Fields("control_box_id").Value)   '提示栏中的控制箱编号
    '                        Me.StatusStrip_control_box3 = Mid(lamp_id_tag, 1, 4)   '提示栏中的控制箱编号
    '                        Me.StatusStrip_lamp_id3 = lamp_id_tag   '提示栏中的路灯编号
    '                        rs.Fields("date").Value = Now    '更新路灯信息表中的时间信息
    '                        Me.SetTextDelegate("区域：" & Me.StatusStrip_boxname3 & vbCrLf, True, Me.Doing_now_text)
    '                        Me.SetTextDelegate("景观灯类型：" & Me.StatusStrip_typestring3 & vbCrLf, True, Me.Doing_now_text)
    '                        Me.SetTextDelegate("景观灯编号：" & Mid(Me.StatusStrip_lamp_id3, 7, 3) & vbCrLf, True, Me.Doing_now_text)
    '                        Me.SetTextDelegate("电阻AD值：" & control_lamp_obj.Get_dianzu_ad & vbCrLf, True, Me.Doing_now_text)
    '                        Me.SetTextDelegate("电流AD值：" & control_lamp_obj.Get_current_ad & vbCrLf, True, Me.Doing_now_text)

    '                    End If

    '                    Select Case state_string
    '                        Case "A"
    '                            '如果数据库中的状态是故障状态，而目前是成功的，则表示故障维修好，写入维修工单表中
    '                            If rs.Fields("result").Value <> 0 Then
    '                                Dim rs_work_end As ADODB.Recordset
    '                                sql = "select * from work_table where lamp_id='" & lamp_id_tag & "'and time_end is null"
    '                                rs_work_end = DBOperation.SelectSQL(conn, sql, msg)   '维修工单中找到只有开始时间没有结束时间的该路灯条目
    '                                If rs_work_end.RecordCount > 0 Then
    '                                    '分配了工单,记录工单中的故障维修完成时间
    '                                    rs_work_end.Fields("time_end").Value = Now
    '                                    rs_work_end.Update()

    '                                    sql = "select * from lamp_inf_record  where lamp_id='" & lamp_id_tag & "' and state_inf=" & 4  '将路灯状态中标志到维修清单中的路灯标志为维修好
    '                                    rs_work_end = DBOperation.SelectSQL(conn, sql, msg) '标志路灯维修好
    '                                    If rs_work_end.RecordCount > 0 Then
    '                                        rs_work_end.Fields("server_state").Value = 2  '标志为维修好的
    '                                        rs_work_end.Update()
    '                                    End If

    '                                End If

    '                                rs_work_end.Close()
    '                                rs_work_end = Nothing
    '                            End If

    '                            rs.Fields("result").Value = 0  '将路灯表中的路灯结果该为成功
    '                            rs.Fields("state").Value = 0   '路灯状态为暗
    '                            rs.Update()
    '                            'Me.SetTextDelegate("状态：" & control_lamp_obj.Get_actual_state(Me.StatusStrip_lamp_id, Me.show_time) & " 暗" & Now & vbCrLf, True, Me.Doing_now_text)
    '                            Me.SetTextDelegate("状态： 暗  " & Now & vbCrLf, True, Me.Doing_now_text)
    '                            If id = 1 Then
    '                                result1 = 0
    '                                state1 = 0
    '                                state1_string = "状态： 暗  " & Now & vbCrLf
    '                            End If
    '                            If id = 2 Then
    '                                result2 = 0
    '                                state2 = 0
    '                                state2_string = "状态： 暗  " & Now & vbCrLf
    '                            End If
    '                            If id = 3 Then
    '                                result3 = 0
    '                                state3 = 0
    '                                state3_string = "状态： 暗  " & Now & vbCrLf
    '                            End If

    '                        Case "B"
    '                            '如果数据库中的状态是故障状态，而目前是成功的，则表示故障维修好，写入维修工单表中
    '                            If rs.Fields("result").Value <> 0 Then
    '                                Dim rs_work_end As ADODB.Recordset
    '                                sql = "select * from work_table where lamp_id='" & lamp_id_tag & "'and time_end is null"
    '                                rs_work_end = DBOperation.SelectSQL(conn, sql, msg)
    '                                If rs_work_end.RecordCount > 0 Then
    '                                    '分配了工单
    '                                    rs_work_end.Fields("time_end").Value = Now '维修好的时间
    '                                    rs_work_end.Update()

    '                                    sql = "select * from lamp_inf_record  where lamp_id='" & lamp_id_tag & "'and state_inf=" & 4
    '                                    rs_work_end = DBOperation.SelectSQL(conn, sql, msg) '标志路灯维修好

    '                                    If rs_work_end.RecordCount > 0 Then
    '                                        rs_work_end.Fields("server_state").Value = 2  '标志为维修好的
    '                                        rs_work_end.Update()
    '                                    End If

    '                                End If

    '                                rs_work_end.Close()
    '                                rs_work_end = Nothing

    '                            End If
    '                            rs.Fields("result").Value = 0  '路灯表中的结果该为成功
    '                            rs.Fields("state").Value = 1   '路灯表中的状态改为亮
    '                            rs.Update()
    '                            ' Me.SetTextDelegate("状态：" & control_lamp_obj.Get_actual_state(Me.StatusStrip_lamp_id, Me.show_time) & " 亮" & Now & vbCrLf, True, Me.Doing_now_text)
    '                            Me.SetTextDelegate("状态： 亮  " & Now & vbCrLf, True, Me.Doing_now_text)
    '                            If id = 1 Then
    '                                result1 = 0
    '                                state1 = 1
    '                                state1_string = "状态： 亮  " & Now & vbCrLf
    '                            End If
    '                            If id = 2 Then
    '                                result2 = 0
    '                                state2 = 1
    '                                state2_string = "状态： 亮  " & Now & vbCrLf
    '                            End If
    '                            If id = 3 Then
    '                                result3 = 0
    '                                state3 = 1
    '                                state3_string = "状态： 亮  " & Now & vbCrLf
    '                            End If
    '                        Case "C"
    '                            '判断故障列表中是否存在该路灯的该故障
    '                            control_lamp_obj.Input_problem(1, lamp_id_tag, lamp_state)  '写入故障列表
    '                            '  rs.Fields("state").Value = 0
    '                            rs.Fields("result").Value = 1  '路灯列表中的结果为该亮非亮
    '                            rs.Update()
    '                            Me.SetTextDelegate("状态：该亮非亮  " & Now & vbCrLf, True, Me.Doing_now_text)
    '                            If id = 1 Then
    '                                result1 = 1
    '                                state1 = 0
    '                                state1_string = "状态： 该亮非亮  " & Now & vbCrLf
    '                            End If
    '                            If id = 2 Then
    '                                result2 = 1
    '                                state2 = 0
    '                                state2_string = "状态： 该亮非亮  " & Now & vbCrLf
    '                            End If
    '                            If id = 3 Then
    '                                result3 = 1
    '                                state3 = 0
    '                                state3_string = "状态： 该亮非亮  " & Now & vbCrLf
    '                            End If
    '                        Case "D"
    '                            ''判断故障列表中是否存在该路灯的该故障
    '                            control_lamp_obj.Input_problem(2, Trim(rs.Fields("lamp_id").Value), lamp_state)  '如果没有写入故障列表
    '                            '  rs.Fields("state").Value = 1
    '                            rs.Fields("result").Value = 2  '路灯列表中的结果为该暗非暗
    '                            rs.Update()
    '                            Me.SetTextDelegate("状态：该暗非暗  " & Now & vbCrLf, True, Me.Doing_now_text)
    '                            If id = 1 Then
    '                                result1 = 2
    '                                state1 = 1
    '                                state1_string = "状态： 该暗非暗  " & Now & vbCrLf
    '                            End If
    '                            If id = 2 Then
    '                                result2 = 2
    '                                state2 = 1
    '                                state2_string = "状态： 该暗非暗  " & Now & vbCrLf
    '                            End If
    '                            If id = 3 Then
    '                                result3 = 2
    '                                state3 = 1
    '                                state3_string = "状态： 该暗非暗  " & Now & vbCrLf
    '                            End If
    '                        Case Else
    '                            rs.Close()
    '                            rs = Nothing
    '                            MsgBox("路灯状态对比出错！", , msg_box_title)
    '                            conn.Close()
    '                            conn = Nothing
    '                            Exit Sub

    '                    End Select

    '                End If
    '                rs.Close()
    '                rs = Nothing

    '            End If '有返回状态

    '        End If  '查询超时


    '        conn.Close()
    '        conn = Nothing

    '    End Sub
    '#End Region

    '#Region "轮询线程"

    '    Private Sub BackgroundWorker_circle_state_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs)
    '        '轮询的线程
    '        While Me.BackgroundWorker_circle_state.CancellationPending = False
    '            'Me.control_center_inf()  '主控制界面上的实时统计信息
    '            Me.state_read_fun(1)  '轮询灯的实时状态

    '            System.Threading.Thread.Sleep(2000)

    '        End While

    '    End Sub



    '    'Private Sub BackgroundWorker_circle_state_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker_circle_state.ProgressChanged
    ' Dim div_time_obj As New div_time_class
    '  Dim control_lamp_obj As New control_lamp

    'If e.ProgressPercentage = 1 Then
    '    '主控界面显示
    '    Doing_now_text.AppendText(div_time_obj.Get_box_string & div_time_obj.Get_type_string & "时段控制开始......" & vbCrLf)
    '    work_string.Text = div_time_obj.Get_box_string & " " & div_time_obj.Get_type_string & div_time_obj.Get_level_string & "时段控制开始......"

    'End If

    'If e.ProgressPercentage = 2 Then

    '    If map_size.Value = 0 Then

    '        pos.X = pos.X * 0.7 + Me.map.Left - 1  '相对于groupbox1的坐标
    '        pos.Y = pos.Y * 0.7 + Me.map.Top - 2


    '    End If

    '    If map_size.Value = 1 Then
    '        pos.X = pos.X + Me.map.Left - 1  '相对于groupbox1的坐标
    '        pos.Y = pos.Y + Me.map.Top - 2

    '    End If

    '    If map_size.Value = 2 Then

    '        pos.X = pos.X * 1.5 + Me.map.Left - 2   '相对于groupbox1的坐标
    '        pos.Y = pos.Y * 1.5 + Me.map.Top - 3


    '    End If
    'End If

    'If e.ProgressPercentage = 3 Then

    '    map.Location = hand_pos_end
    'End If

    'If e.ProgressPercentage = 4 Then
    '    map.Location = dis_pos
    'End If

    'If e.ProgressPercentage = 5 Then
    '    lamp_pos = pos

    '    ' Me.find_state_lamp.Location = lamp_pos

    'End If

    'If e.ProgressPercentage = 6 Then
    '    Doing_now_text.AppendText(Me.StatusStrip_lamp_id & "信息返回超时！" & Now & vbCrLf & vbCrLf)

    'End If

    'If e.ProgressPercentage = 7 Then
    '    Doing_now_text.AppendText(">>>>还没有接收到" & Me.StatusStrip_boxname & " " & Me.StatusStrip_typestring & " " & Mid(Me.StatusStrip_lamp_id, 7, 3) & "号景观灯的信息请稍候..." & "  " & Now & vbCrLf)
    'End If

    'If e.ProgressPercentage = 8 Then

    '    'Doing_now_text.AppendText("电控箱编号：" & Me.StatusStrip_control_box & vbCrLf)
    '    Doing_now_text.AppendText("电控箱名称：" & Me.StatusStrip_boxname & vbCrLf)
    '    Doing_now_text.AppendText("景观灯类型：" & Me.StatusStrip_typestring & vbCrLf)
    '    Doing_now_text.AppendText("景观灯编号：" & Mid(Me.StatusStrip_lamp_id, 7, 3) & vbCrLf)
    '    Doing_now_text.AppendText("电阻AD值：" & control_lamp_obj.Get_dianzu_ad & vbCrLf)
    '    Doing_now_text.AppendText("电流AD值：" & control_lamp_obj.Get_current_ad & vbCrLf)
    'End If

    'If e.ProgressPercentage = 9 Then
    '    Doing_now_text.AppendText("状态：" & control_lamp_obj.Get_actual_state(Me.StatusStrip_lamp_id, Me.show_time) & " 暗" & Now & vbCrLf)

    'End If

    'If e.ProgressPercentage = 10 Then
    '    Doing_now_text.AppendText("状态：" & control_lamp_obj.Get_actual_state(Me.StatusStrip_lamp_id, Me.show_time) & " 亮" & Now & vbCrLf)

    'End If

    'If e.ProgressPercentage = 11 Then
    '    Doing_now_text.AppendText("状态：该亮非亮" & Now & vbCrLf)
    'End If

    'If e.ProgressPercentage = 12 Then
    '    Doing_now_text.AppendText("状态：该暗非暗" & Now & vbCrLf)
    'End If

    'End Sub

    '#End Region



End Class
