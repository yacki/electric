''' <summary>
''' 统计一个时间范围内的故障维修情况，即比较相邻两天的故障情况，前一天出现而后一天没有的即为维修好
''' </summary>
''' <remarks></remarks>

Public Class 维修统计报表
    Private xlApp As Microsoft.Office.Interop.Excel.Application

    Private xlBook As Microsoft.Office.Interop.Excel.Workbook
    Private xlSheet As Microsoft.Office.Interop.Excel.Worksheet
    Private danwei As String
    Private control_box_name_string As String
    Private row As Integer
    Private id As Integer   '编号
    Private lamp_id_check As String   '查询的路灯编号
    Private lamp_type_check As String  '查询的灯的类型
    Private control_box_name_check As String  '查询的电控箱编号
    Private check_times As Integer '查询进度参数
    Private string_tag As Integer   '提示信息只出现一次
    Private lamp_id_num As Integer  '灯的数目
    'Private state_problem_on As String  '该亮非亮
    'Private state_problem_off As String  '该暗非暗
    'Private state_on As String
    'Private state_off As String

    ''' <summary>
    ''' 维修统计报表的load函数，初始化日期和区域名称
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 维修统计报表_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '设置各个窗体的图标
        Me.Icon = New Icon("图片\favicon.ico", 32, 32)
        danwei = ""
        '初始化下拉框
        Com_inf.Select_box_name(control_box_name)
        control_box_name_string = Trim(control_box_name.Text)
        id = 1
        'state_problem_on = LoginForm.Property_welcome_win_obj.Property_state_problem_on
        'state_problem_off = LoginForm.Property_welcome_win_obj.Property_state_problem_off

        'state_on = LoginForm.Property_welcome_win_obj.Property_state_on
        'state_off = LoginForm.Property_welcome_win_obj.Property_state_off
    End Sub

    ''' <summary>
    ''' 查询的按钮处理函数
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub find_problem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles find_problem.Click
        Dim time As Date
        If control_box_name.Text = "" Then
            MsgBox("请选择区域名称")
            Exit Sub
        End If
        ProgressBar.Visible = True

        time = check_time.Value  '时间
        control_box_name_string = Trim(control_box_name.Text)  '区域名称
        check_times = 1  '查询进度参数
        string_tag = 0  '首次查询标志

        If Me.BackgroundWorker_check_problem.IsBusy = False Then
            Me.BackgroundWorker_check_problem.RunWorkerAsync()
        Else
            MsgBox("线程正忙，请稍后重试", , PROJECT_TITLE_STRING)
        End If
    End Sub

    ''' <summary>
    ''' 生成维修统计报表的主处理函数
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Get_problem_table()
        'open_file = "excel"
        xlApp = (New Microsoft.Office.Interop.Excel.Application)

        xlBook = xlApp.Workbooks().Add
        xlSheet = xlBook.Worksheets("sheet1")



        'On Error GoTo createErr

        '  Dim Table As New DataTable
        xlApp.Cells(1, 1) = "路灯维修统计报表"
        xlApp.Rows(1).RowHeight = 50
        xlApp.Rows(1).Font.Size = 18
        xlApp.Rows(1).Font.Bold = True
        xlApp.Cells(2, 1) = "单位：" & danwei
        xlApp.Cells(2, 2) = ""
        xlApp.Cells(2, 3) = "区域：" & control_box_name_string

        xlApp.Cells(2, 4) = "时间：" & check_time.Value.Date


        xlApp.Rows(2).RowHeight = 30
        xlApp.Rows(2).Font.Size = 12
        ' xlApp.Rows(2).Font.Bold = True


        ' Dim dt As New DataTable
        ' dt.Columns.Add("市", GetType(String))

        '  xlApp.Cells(3, 1) = "区域"
        xlApp.Cells(3, 1) = "编号"
        xlApp.Cells(3, 2) = "路灯编号"
        xlApp.Cells(3, 3) = "故障原因"
        xlApp.Cells(3, 4) = "备注"

        xlApp.Rows(3).Font.Bold = True
        xlApp.Rows(3).font.size = 12
        xlApp.Rows(3).RowHeight = 30


        row = 4

        lamp_id_state()

        With xlSheet
            .Range(.Cells(1, 1), .Cells(1, 4)).Merge()
            .Range(.Cells(2, 1), .Cells(2, 2)).Merge()
            '.Range(.Cells(2, 3), .Cells(2, 4)).Merge()


            .Range(.Cells(3, 1), .Cells(3, 1)).ColumnWidth = 5
            .Range(.Cells(3, 2), .Cells(3, 2)).ColumnWidth = 13
            .Range(.Cells(3, 4), .Cells(3, 3)).ColumnWidth = 25
            .Range(.Cells(3, 5), .Cells(3, 4)).ColumnWidth = 20

            .Range(.Cells(3, 1), .Cells(row - 1, 4)).RowHeight = 20

            .Range(.Cells(1, 1), .Cells(1, 4)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter
            .Range(.Cells(2, 1), .Cells(2, 3)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft
            .Range(.Cells(2, 4), .Cells(2, 4)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight

            .Range(.Cells(3, 1), .Cells(row - 1, 4)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter


            .Range(.Cells(3, 1), .Cells(3, 4)).Font.Name = "宋体"
            '设标题为宋体字
            .Range(.Cells(3, 1), .Cells(3, 4)).Font.Bold = "True"
            '标题字体加粗
            .Range(.Cells(1, 1), .Cells(2, 4)).Borders.LineStyle = 0
            .Range(.Cells(3, 1), .Cells(row - 1, 4)).Borders.LineStyle = 1
            '设表格边框样式
            .Range(.Cells(2, 1), .Cells(row - 1, 4)).Font.Size = 12

            '表中数据的字号

            .PageSetup.CenterFooter = "第 &P 页/共 &N 页"

        End With


    End Sub
    ''' <summary>
    ''' 选择区域名称
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub control_box_name_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles control_box_name.DropDown
        Com_inf.Select_box_name(control_box_name)  '为电控箱编号增加内容
    End Sub
    ''' <summary>
    '''   按路灯编号进行路灯状态查询
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub lamp_id_state()

        '    Me.BackgroundWorker_on_off.ReportProgress(find_tag)
        Dim rs As ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim rs_on_off As New ADODB.Recordset
        Dim lamp_state As Integer  '1表示运行正常，0表示在时间段内有故障

        Dim time_start, time_end As Date  '当前的时间
        Dim last_time_start, last_time_end As Date  '前一天的时间

        lamp_state = 1  '路灯初始化为正常
        'page_id = 1
        'page_num_total = 1

        DBOperation.OpenConn(conn)

        sql = "select * from lamp_street where control_box_name='" & control_box_name_string & "' order by lamp_id"

        msg = ""
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "lamp_id_state", , PROJECT_TITLE_STRING)
            conn.Close()

            Exit Sub
        End If
        If rs.RecordCount <= 0 Then
            MsgBox("该范围内没有灯的信息", , PROJECT_TITLE_STRING)
            rs.Close()
            rs = Nothing
            conn.Close()
            conn = Nothing
            Exit Sub
        Else
            '在各个需要查询的时间点上对路灯的运行状态进行进行统计
            time_start = DateAdd(DateInterval.Day, -1, check_time.Value.Date) & " 22:00:00"
            time_end = check_time.Value.Date & " 05:00:00"
            last_time_start = DateAdd(DateInterval.Day, -1, time_start.Date) & " 22:00:00"
            last_time_end = DateAdd(DateInterval.Day, -1, time_end.Date) & " 05:00:00"


            lamp_id_num = rs.RecordCount  '记录查询路灯的个数，为查询的进度做参数
            'page_num_total = (lamp_id_num + 3) / 30
            While rs.EOF = False

                '判断每一盏路灯在此时间区间内容是否工作正常
                control_box_name_check = Trim(rs.Fields("control_box_name").Value)
                lamp_type_check = Trim(rs.Fields("type_string").Value)
                lamp_id_check = Trim(rs.Fields("lamp_id").Value)

                Get_on_off_state(lamp_id_check, time_start, time_end, last_time_start, last_time_end)

                rs.MoveNext()

            End While


        End If

        conn.Close()
        conn = Nothing

    End Sub

    ''' <summary>
    ''' 将前一天的故障和后一天的故障对比，前一天有的故障在后一天没出现，则表示维修好了
    ''' </summary>
    ''' <param name="lamp_id_tag">灯的编号</param>
    ''' <param name="check_time_start">查询的开始时间</param>
    ''' <param name="check_time_end">查询的结束时间</param>
    ''' <param name="last_check_time_start">前一天的查询开始时间</param>
    ''' <param name="last_check_time_end">前一天的查询结束时间</param>
    ''' <remarks></remarks>
    Public Sub Get_on_off_state(ByVal lamp_id_tag As String, ByVal check_time_start As Date, ByVal check_time_end As Date, ByVal last_check_time_start As Date, ByVal last_check_time_end As Date)
        Dim conn As New ADODB.Connection
        Dim rs As New ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim time_start As Date  '开始时间
        Dim time_end As Date  '结束时间
        Dim state As String  '灯的状态
        'Dim inf_string As String  '提示信息
        Dim progress_percentage As Integer
        Dim tag As String

        DBOperation.OpenConn(conn)

        sql = ""

        msg = ""

        'sql = "select * from lamp_state_record where lamp_id='" & lamp_id_tag & "' and time>='" & check_time_start & "' and time<='" & check_time_end & "' order by time"
        sql = "select * from lamp_state_list where lamp_id='" & lamp_id_tag & "' and time_end>='" & last_check_time_start & "' and time_start<='" & last_check_time_end & "' order by time_start"

        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "Get_on_off_state", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing

            Exit Sub
        End If
        If rs.RecordCount > 0 Then
            If rs.RecordCount = 1 Then
                '只有一条记录，并且是无返回值的，怎表示通信不正常
                If rs.Fields("state").Value = LAMP_STATE_NORETURN Then

                    If Me.check_problem_ok_or_not(lamp_id_check, LAMP_STATE_NORETURN, check_time_start, check_time_end) = 0 Then
                        '已维修好
                        xlApp.Cells(row, 1) = "'" & id

                        ' xlApp.Cells(row, 2) = "'" & lamp_type_check
                        xlApp.Cells(row, 2) = "'" & Val(Mid(Trim(lamp_id_check), 1, 4)) & "-" & Val(Mid(Trim(lamp_id_check), 5, 2)) & "-" & Val(Mid(Trim(lamp_id_check), 7, LAMP_ID_LEN))
                        xlApp.Cells(row, 3) = "没上电"

                        row += 1
                        id += 1
                        GoTo Finish

                    End If
                End If
            End If

            'time_start = rs.Fields("time_start").Value
            'time_end = rs.Fields("time_start").Value
            time_start = check_time_start
            time_end = check_time_end
            state = ""
            tag = 0
            Dim no_return_num, record_num As Integer
            no_return_num = 0
            record_num = rs.RecordCount
            While rs.EOF = False
                check_times += 1
                state = Trim(rs.Fields("state").Value)
                progress_percentage = check_times * (100 / rs.RecordCount) / lamp_id_num
                If progress_percentage > 100 Then
                    progress_percentage = 100
                End If
                Me.BackgroundWorker_check_problem.ReportProgress(progress_percentage)


                '状态变化
                time_end = rs.Fields("time_start").Value
                '如果是该亮非亮或该暗非暗的，则标志故障，记录一条后跳出，查询下一盏灯

                tag = rs.Fields("end_tag").Value
                If (state = LAMP_STATE_PROBLEM_ON Or state = LAMP_STATE_PROBLEM_OFF) And (tag = 2 Or tag = 3) Then
                    If Me.check_problem_ok_or_not(lamp_id_check, state, check_time_start, check_time_end) = 0 Then
                        xlApp.Cells(row, 1) = "'" & id
                        ' xlApp.Cells(row, 2) = "'" & lamp_type_check
                        xlApp.Cells(row, 2) = "'" & Val(Mid(Trim(lamp_id_check), 1, 4)) & "-" & Val(Mid(Trim(lamp_id_check), 5, 2)) & "-" & Val(Mid(Trim(lamp_id_check), 7, LAMP_ID_LEN))
                        xlApp.Cells(row, 3) = state

                        row += 1
                        id += 1
                        GoTo Finish
                    End If

                End If
                If state = LAMP_STATE_NORETURN Then
                    no_return_num += 1
                End If

                time_start = time_end
                state = Trim(rs.Fields("state").Value)
                tag = Trim(rs.Fields("end_tag").Value)
                rs.MoveNext()


            End While
            If no_return_num = record_num Then
                '全是无返值
                If Me.check_problem_ok_or_not(lamp_id_check, LAMP_STATE_NORETURN, check_time_start, check_time_end) = 0 Then
                    '已维修好
                    xlApp.Cells(row, 1) = "'" & id

                    'xlApp.Cells(row, 2) = "'" & lamp_type_check
                    xlApp.Cells(row, 2) = "'" & Val(Mid(Trim(lamp_id_check), 1, 4)) & "-" & Val(Mid(Trim(lamp_id_check), 5, 2)) & "-" & Val(Mid(Trim(lamp_id_check), 7, LAMP_ID_LEN))
                    xlApp.Cells(row, 3) = "没上电"

                    row += 1
                    id += 1
                    GoTo Finish

                End If
            End If

            If (state = LAMP_STATE_PROBLEM_ON Or state = LAMP_STATE_PROBLEM_OFF) And (tag = 2 Or tag = 3) Then
                ' xlApp.Cells(row, 1) = "'" & control_box_name_check
                If Me.check_problem_ok_or_not(lamp_id_check, state, check_time_start, check_time_end) = 0 Then
                    xlApp.Cells(row, 1) = "'" & id
                    ' xlApp.Cells(row, 2) = "'" & lamp_type_check
                    xlApp.Cells(row, 2) = "'" & Val(Mid(Trim(lamp_id_check), 1, 4)) & "-" & Val(Mid(Trim(lamp_id_check), 5, 2)) & "-" & Val(Mid(Trim(lamp_id_check), 7, LAMP_ID_LEN))
                    xlApp.Cells(row, 3) = state
                    row += 1
                    GoTo Finish

                End If

            End If


        End If

Finish:

        rs.Close()
        rs = Nothing
        conn.Close()
        conn = Nothing

    End Sub

    ''' <summary>
    ''' 检查后一天是否有这条故障记录
    ''' </summary>
    ''' <param name="lamp_id">灯的编号</param>
    ''' <param name="state">状态</param>
    ''' <param name="check_time_start">查询的开始时间</param>
    ''' <param name="check_time_end">查询结束时间</param>
    ''' <returns>返回0为第二天已没有该记录，返回1为仍然存在该故障</returns>
    ''' <remarks></remarks>
    Public Function check_problem_ok_or_not(ByVal lamp_id As String, ByVal state As String, ByVal check_time_start As Date, ByVal check_time_end As Date) As Integer
        '
        Dim rs As New ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim conn As New ADODB.Connection

        DBOperation.OpenConn(conn)
        msg = ""
        If state = LAMP_STATE_PROBLEM_ON Or state = LAMP_STATE_PROBLEM_OFF Then
            sql = "select * from lamp_state_list where (state<>'亮' or state<>'暗') and lamp_id='" & lamp_id & "' and time_end>='" & check_time_start & "' and time_start<='" & check_time_end & "' order by time_start"

            'sql = "select * from lamp_state_list where (end_tag=" & 3 & " or end_tag=" & 2 & ") and state='" & state & "' and lamp_id='" & lamp_id & "' and time_end>='" & check_time_start & "' and time_start<='" & check_time_end & "' order by time_start"
        Else
            sql = "select * from lamp_state_list where (end_tag=" & 0 & " or end_tag=" & 1 & ") and  lamp_id='" & lamp_id & "' and time_end>='" & check_time_start & "' and time_start<='" & check_time_end & "' order by time_start"

        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs.RecordCount > 0 Then
            While rs.EOF = False
                If (rs.Fields("state").Value = LAMP_STATE_ON Or rs.Fields("state").Value = LAMP_STATE_OFF) And state = LAMP_STATE_NORETURN Then
                    check_problem_ok_or_not = 0
                    GoTo finish
                End If
                rs.MoveNext()
            End While
            check_problem_ok_or_not = 1
        Else
            check_problem_ok_or_not = 0
        End If
finish:

        rs.Close()
        rs = Nothing
        conn.Close()
        conn = Nothing

    End Function

    ''' <summary>
    ''' 维修统计线程的主函数
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BackgroundWorker_check_problem_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker_check_problem.DoWork
        Get_problem_table()
    End Sub
    ''' <summary>
    ''' 统计的进度条显示
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BackgroundWorker_check_problem_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker_check_problem.ProgressChanged
        Me.ProgressBar.Value = e.ProgressPercentage
        g_welcomewinobj.circle_string.Text = "导出路灯维修统计报表"
        record_num.Text = "导出路灯维修统计报表"
        string_tag = 1
    End Sub
    ''' <summary>
    ''' 统计完成，生成EXCEL表
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BackgroundWorker_check_problem_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker_check_problem.RunWorkerCompleted
        ProgressBar.Value = 0
        id = 1
        ProgressBar.Visible = False
        record_num.Text = "维修报表统计"
        xlApp.Visible = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        xlBook.Close()

        xlApp.Quit()
        xlSheet = Nothing

        xlBook = Nothing
        xlApp = Nothing

    End Sub
End Class