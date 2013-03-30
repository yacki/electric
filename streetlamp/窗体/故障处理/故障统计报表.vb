''' <summary>
''' 统计一个时间区间内的故障信息，包括：该亮非亮，该暗非暗，通信不正常/没上电
''' </summary>
''' <remarks></remarks>

Public Class 故障统计报表
    Private m_xlApp As Microsoft.Office.Interop.Excel.Application
    Private m_xlBook As Microsoft.Office.Interop.Excel.Workbook
    Private m_xlSheet As Microsoft.Office.Interop.Excel.Worksheet
    Private m_controlbox_namestring As String  '主控箱名称
    Private m_row As Integer  'EXCEL表的行数
    Private m_id As Integer   '编号
    Private m_checktimes As Integer '查询进度参数
    Private m_lampidnum As Integer  '灯的数目

    ''' <summary>
    ''' 故障统计窗体，初始化日期和区域名称
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' 
    Private Sub 故障统计报表_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '设置各个窗体的图标
        Me.Icon = New Icon("图片\favicon.ico", 32, 32)
        Com_inf.Select_city_name(control_city_name)
        m_controlbox_namestring = Trim(control_box_name.Text)
        m_id = 1

    End Sub

    ''' <summary>
    ''' 查询报表按钮处理函数
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub find_problem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles find_problem.Click
        Dim time As Date
        If control_box_name.Text = "" Then
            MsgBox("请选择主控箱名称", , PROJECT_TITLE_STRING)
            Exit Sub
        End If
        ProgressBar.Visible = True

        time = check_time.Value '日期
        m_controlbox_namestring = Trim(control_box_name.Text) '区域名称
        m_checktimes = 1  '查询进度参数

        If Me.BackgroundWorker_check_problem.IsBusy = False Then
            Me.BackgroundWorker_check_problem.RunWorkerAsync()
        Else
            MsgBox("线程正忙，请稍后重试", , PROJECT_TITLE_STRING)
        End If
    End Sub

    ''' <summary>
    ''' 生成统计报表主处理函数
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Get_problem_table()
        m_xlApp = (New Microsoft.Office.Interop.Excel.Application)
        m_xlBook = m_xlApp.Workbooks().Add
        m_xlSheet = m_xlBook.Worksheets("sheet1")

        m_xlApp.Cells(1, 1) = "路灯故障统计报表"
        m_xlApp.Rows(1).RowHeight = 50
        m_xlApp.Rows(1).Font.Size = 18
        m_xlApp.Rows(1).Font.Bold = True
        m_xlApp.Cells(2, 1) = "单位：" & COMPANY_NAME
        m_xlApp.Cells(2, 2) = ""
        m_xlApp.Cells(2, 3) = "主控箱名称：" & m_controlbox_namestring

        m_xlApp.Cells(2, 4) = "时间：" & check_time.Value.Date


        m_xlApp.Rows(2).RowHeight = 30
        m_xlApp.Rows(2).Font.Size = 12

        m_xlApp.Cells(3, 1) = "编号"
        m_xlApp.Cells(3, 2) = "路灯编号"
        m_xlApp.Cells(3, 3) = "故障原因"
        m_xlApp.Cells(3, 4) = "备注"

        m_xlApp.Rows(3).Font.Bold = True
        m_xlApp.Rows(3).font.size = 12
        m_xlApp.Rows(3).RowHeight = 30


        m_row = 4

        lamp_id_state()

        With m_xlSheet
            .Range(.Cells(1, 1), .Cells(1, 4)).Merge()
            .Range(.Cells(2, 1), .Cells(2, 2)).Merge()
            .Range(.Cells(3, 1), .Cells(3, 1)).ColumnWidth = 5
            .Range(.Cells(3, 2), .Cells(3, 2)).ColumnWidth = 13
            .Range(.Cells(3, 3), .Cells(3, 3)).ColumnWidth = 25
            .Range(.Cells(3, 4), .Cells(3, 4)).ColumnWidth = 20
            .Range(.Cells(3, 1), .Cells(m_row - 1, 4)).RowHeight = 20
            .Range(.Cells(1, 1), .Cells(1, 4)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter
            .Range(.Cells(2, 1), .Cells(2, 3)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft
            .Range(.Cells(2, 4), .Cells(2, 4)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight
            .Range(.Cells(3, 1), .Cells(m_row - 1, 4)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter
            .Range(.Cells(3, 1), .Cells(3, 4)).Font.Name = "宋体"
            '设标题为宋体字
            .Range(.Cells(3, 1), .Cells(3, 4)).Font.Bold = "True"
            '标题字体加粗
            .Range(.Cells(1, 1), .Cells(2, 4)).Borders.LineStyle = 0
            .Range(.Cells(3, 1), .Cells(m_row - 1, 4)).Borders.LineStyle = 1
            '设表格边框样式
            .Range(.Cells(2, 1), .Cells(m_row - 1, 4)).Font.Size = 12

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
        Com_inf.Select_box_name_level(control_city_name, control_area_name, control_street_name, control_box_name)
    End Sub

    ''' <summary>
    ''' 按路灯编号进行路灯状态查询(统计每天晚上8点到早上5点之间的故障信息，作为常规的故障统计)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub lamp_id_state()
        Dim rs As ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim rs_on_off As New ADODB.Recordset
        Dim lamp_state As Integer  '1表示运行正常，0表示在时间段内有故障
        Dim lamp_id As String '查询的路灯编号
        Dim time_start, time_end As Date '开始时间与结束时间

        lamp_state = 1  '路灯初始化为正常

        DBOperation.OpenConn(conn)
        sql = "select * from lamp_street where control_box_name='" & m_controlbox_namestring & "' order by lamp_id"

        msg = ""
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "lamp_id_state", , PROJECT_TITLE_STRING)
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
            time_start = DateAdd(DateInterval.Day, -1, check_time.Value.Date) & " 20:00:00"
            time_end = check_time.Value.Date & " 05:00:00"

            m_lampidnum = rs.RecordCount  '记录查询路灯的个数，为查询的进度做参数
            While rs.EOF = False
                lamp_id = Trim(rs.Fields("lamp_id").Value)
                get_on_off_state(lamp_id, time_start, time_end) '判断每一盏路灯在此时间区间内容是否工作正常

                rs.MoveNext()

            End While
        End If

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing

    End Sub

    ''' <summary>
    ''' 判断某一盏灯的工作是否正常
    ''' </summary>
    ''' <param name="lamp_id_tag">灯的编号</param>
    ''' <param name="check_time_start">查询的开始时间</param>
    ''' <param name="check_time_end">查询的结束时间</param>
    ''' <remarks></remarks>
    Public Sub get_on_off_state(ByVal lamp_id_tag As String, ByVal check_time_start As Date, ByVal check_time_end As Date)
        Dim conn As New ADODB.Connection
        Dim rs As New ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim time_start As Date  '开始时间
        Dim time_end As Date  '结束时间
        Dim state As String  '灯的状态
        Dim progress_percentage As Integer
        Dim tag As String
        Dim no_return_num As Integer
        Dim record_num As Integer
        Dim lamp_time_start, lamp_time_end As DateTime '记录一个路灯开始和结束时间
        no_return_num = 0
        DBOperation.OpenConn(conn)
        msg = ""

        sql = "select * from lamp_state_list where lamp_id='" & lamp_id_tag & "' and time_end>='" & check_time_start & "' and time_start<='" & check_time_end & "' order by time_start"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "Get_on_off_state", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        record_num = rs.RecordCount
        If rs.RecordCount > 0 Then
            If rs.RecordCount = 1 Then    '只有一条记录，并且是无返回值的，表示通信不正常
                If rs.Fields("state").Value = LAMP_STATE_NORETURN Then
                    m_xlApp.Cells(m_row, 1) = "'" & m_id
                    m_xlApp.Cells(m_row, 2) = "'" & Val(Mid(lamp_id_tag, 1, 4)) & "-" & Val(Mid(lamp_id_tag, 5, 2)) & "-" & Val(Mid(lamp_id_tag, 7, LAMP_ID_LEN))
                    m_xlApp.Cells(m_row, 3) = LAMP_STATE_NORETURN

                    m_row += 1
                    m_id += 1
                    GoTo Finish
                End If
            End If
            time_start = check_time_start
            time_end = check_time_end
            state = ""
            tag = 0
            While rs.EOF = False
                m_checktimes += 1
                lamp_time_start = rs.Fields("time_start").Value
                lamp_time_end = rs.Fields("time_end").Value

                state = Trim(rs.Fields("state").Value)
                progress_percentage = m_checktimes * (100 / rs.RecordCount) / m_lampidnum
                If progress_percentage > 100 Then
                    progress_percentage = 100
                End If
                Me.BackgroundWorker_check_problem.ReportProgress(progress_percentage)
                '状态变化
                time_end = rs.Fields("time_start").Value
                '如果是该亮非亮或该暗非暗的，则标志故障，记录一条后跳出，查询下一盏灯
                tag = rs.Fields("end_tag").Value
                If (state = LAMP_STATE_PROBLEM_ON Or state = LAMP_STATE_PROBLEM_OFF) And (lamp_time_end > lamp_time_start) Then
                    m_xlApp.Cells(m_row, 1) = "'" & m_id
                    m_xlApp.Cells(m_row, 2) = "'" & Val(Mid(Trim(lamp_id_tag), 1, 4)) & "-" & Val(Mid(lamp_id_tag, 5, 2)) & "-" & Val(Mid(lamp_id_tag, 7, LAMP_ID_LEN))
                    m_xlApp.Cells(m_row, 3) = state

                    m_row += 1
                    m_id += 1
                    GoTo Finish
                End If

                If state = LAMP_STATE_NORETURN Then  '如果是无返回值的，则记录次数
                    no_return_num += 1
                End If

                time_start = time_end
                state = Trim(rs.Fields("state").Value)
                tag = Trim(rs.Fields("end_tag").Value)
                rs.MoveNext()
            End While
            If no_return_num = record_num Then  '全是无返值
                m_xlApp.Cells(m_row, 1) = "'" & m_id
                m_xlApp.Cells(m_row, 2) = "'" & Val(Mid(Trim(lamp_id_tag), 1, 4)) & "-" & Val(Mid(lamp_id_tag, 5, 2)) & "-" & Val(Mid(lamp_id_tag, 7, LAMP_ID_LEN))
                m_xlApp.Cells(m_row, 3) = LAMP_STATE_NORETURN

                m_row += 1
                m_id += 1
                GoTo Finish
            End If
            If (state = LAMP_STATE_PROBLEM_ON Or state = LAMP_STATE_PROBLEM_OFF) And (lamp_time_end > lamp_time_start) Then
                m_xlApp.Cells(m_row, 1) = "'" & m_id
                m_xlApp.Cells(m_row, 2) = "'" & Val(Mid(Trim(lamp_id_tag), 1, 4)) & "-" & Val(Mid(lamp_id_tag, 5, 2)) & "-" & Val(Mid(lamp_id_tag, 7, LAMP_ID_LEN))
                m_xlApp.Cells(m_row, 3) = state
                m_row += 1
                GoTo Finish

            End If
        End If

Finish:

        rs.Close()
        rs = Nothing
        conn.Close()
        conn = Nothing

    End Sub

    ''' <summary>
    ''' 故障统计线程的主函数
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BackgroundWorker_check_problem_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker_check_problem.DoWork
        Get_problem_table()
    End Sub

    ''' <summary>
    ''' 显示处理进度条
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BackgroundWorker_check_problem_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker_check_problem.ProgressChanged
        Me.ProgressBar.Value = e.ProgressPercentage
        g_welcomewinobj.circle_string.Text = "导出路灯故障统计报表"
        record_num.Text = "导出路灯故障统计报表"
    End Sub

    ''' <summary>
    ''' 统计完毕后，生成excel表并显示
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BackgroundWorker_check_problem_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker_check_problem.RunWorkerCompleted
        ProgressBar.Value = 0
        m_id = 1
        ProgressBar.Visible = False
        record_num.Text = "故障报表统计"
        m_xlApp.Visible = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default


    End Sub

    ''' <summary>
    ''' 故障统计报表
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 故障统计报表_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If Me.BackgroundWorker_check_problem.IsBusy = True Then
            Me.BackgroundWorker_check_problem.CancelAsync()
        End If
        ProcessKill(m_xlApp, m_xlBook, m_xlSheet)

    End Sub

    ''' <summary>
    ''' 取城市名称
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub control_city_name_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles control_city_name.DropDown
        Com_inf.Select_city_name(control_city_name)
    End Sub

    ''' <summary>
    ''' 城市名称改变
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub control_city_name_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles control_city_name.SelectedIndexChanged
        Com_inf.Select_area_name(control_city_name, control_area_name)
        Com_inf.Select_street_name(control_city_name, control_area_name, control_street_name)
        Com_inf.Select_box_name_level(control_city_name, control_area_name, control_street_name, control_box_name)

    End Sub

    ''' <summary>
    ''' 取区域名称
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub control_area_name_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles control_area_name.DropDown
        Com_inf.Select_area_name(control_city_name, control_area_name)
    End Sub

    ''' <summary>
    ''' 区域名称改变
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub control_area_name_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles control_area_name.SelectedIndexChanged
        Com_inf.Select_street_name(control_city_name, control_area_name, control_street_name)
        Com_inf.Select_box_name_level(control_city_name, control_area_name, control_street_name, control_box_name)

    End Sub

    ''' <summary>
    ''' 街道名称
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub control_street_name_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles control_street_name.DropDown
        Com_inf.Select_street_name(control_city_name, control_area_name, control_street_name)
    End Sub

    ''' <summary>
    ''' 街道名称改变
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub control_street_name_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles control_street_name.SelectedIndexChanged
        Com_inf.Select_box_name_level(control_city_name, control_area_name, control_street_name, control_box_name)

    End Sub
End Class