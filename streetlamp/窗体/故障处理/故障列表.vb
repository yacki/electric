''' <summary>
''' 报警信息统计窗口
''' </summary>
''' <remarks></remarks>
Public Class 故障列表
    Private m_xlApp As Microsoft.Office.Interop.Excel.Application
    Private m_xlBook As Microsoft.Office.Interop.Excel.Workbook
    Private m_xlSheet As Microsoft.Office.Interop.Excel.Worksheet
    Private m_worktabletag As Integer
    Private m_row As Integer  '行数
    Private m_stringtag As Integer

    ''' <summary>
    ''' 标志报警是否完成
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub choose_state()
        Dim i As Integer = 0
        While i < Me.dgv_problem_list.RowCount
            If Me.dgv_problem_list.Rows(i).Cells("server_state").Value = 1 Then  '1为完成
                Me.dgv_problem_list.Rows(i).Cells("alarm_ok").Value = "是"
            Else  '0为未完成
                Me.dgv_problem_list.Rows(i).Cells("alarm_ok").Value = "否"
            End If
            i += 1
        End While
    End Sub

    ''' <summary>
    ''' 初始化窗体
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 故障列表_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: 这行代码将数据加载到表“KaiguanlistDataSet.kaiguan_alarm_list”中。您可以根据需要移动或移除它。
        '   Me.Kaiguan_alarm_listTableAdapter.Fill(Me.KaiguanlistDataSet.kaiguan_alarm_list)
        '设置各个窗体的图标
        Me.Icon = New Icon("图片\favicon.ico", 32, 32)
        m_worktabletag = 0 '1表示工单，0表示其他列表 
        'Me.Alarm_viewTableAdapter.Fill(Me.StreetlampDataSet.alarm_view)   '故障列表
        'choose_state() '根据标志为标志故障是否结束
        '  Me.dgv_problem_list.Columns("data").DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss"  '列表中设置日期的格式
        '   Me.dgv_problem_list.Columns("data_end").DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss"  '列表中设置日期的格式
        get_all_probleminf()  '获取所有报警信息
        record_num.Text = "共有" & dgv_problem_list.RowCount.ToString & "条记录"  '列表中故障的条数
        dtp_date_start.CustomFormat = "yyyy-MM-dd HH:mm:ss"  '查询条件中开始日期的格式
        dtp_date_end.CustomFormat = "yyyy-MM-dd HH:mm:ss" '查询条件中结束日期的格式

        '初始化下拉框
        Com_inf.Select_city_name(cb_city_name)



        '隐藏市区街的下拉框
        'pro_city.Visible = False
        'pro_area.Visible = False
        'pro_street.Visible = False
        lb_lamp_id_start.Visible = False

        lb_lamp_type_id.Visible = False

        '开始日期默认为当前日期的前一天
        dtp_date_start.Value = DateAdd(DateInterval.Day, -1, Now)

    End Sub

    Private Sub get_all_probleminf()
        Dim conn As New ADODB.Connection
        Dim rs As New ADODB.Recordset
        Dim msg As String
        Dim sql As String

        msg = ""
        sql = "select * from Record_view order by id"
        DBOperation.OpenConn(conn)
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING, , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        Dim i As Integer = 0
        dgv_problem_list.Rows.Clear()
        While rs.EOF = False
            dgv_problem_list.Rows.Add()
            dgv_problem_list.Rows(i).Cells("alarm_id").Value = rs.Fields("id").Value
            dgv_problem_list.Rows(i).Cells("control_box_name").Value = rs.Fields("control_box_name").Value
            dgv_problem_list.Rows(i).Cells("lamp_id_short").Value = Mid(Trim(rs.Fields("lamp_id").Value), 7, LAMP_ID_LEN)
            dgv_problem_list.Rows(i).Cells("problem_inf").Value = Trim(rs.Fields("problem_inf").Value)
            dgv_problem_list.Rows(i).Cells("date_start").Value = rs.Fields("date").Value
            dgv_problem_list.Rows(i).Cells("Date_end").Value = rs.Fields("date_end").Value
            dgv_problem_list.Rows(i).Cells("server_state").Value = rs.Fields("server_state").Value
            dgv_problem_list.Rows(i).Cells("type_string").Value = Trim(rs.Fields("type_string").Value)

            i += 1
            rs.MoveNext()
        End While

        choose_state() '对路灯的编号进行重新赋值

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub


    ''' <summary>
    ''' EXCEL表
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub excel()
        Dim rowIndex, colIndex As Integer

        m_xlApp = (New Microsoft.Office.Interop.Excel.Application)

        rowIndex = 1
        colIndex = 0

        m_xlBook = m_xlApp.Workbooks().Add
        m_xlSheet = m_xlBook.Worksheets("sheet1")

        m_xlApp.Cells(1, 1) = "报警信息表"
        m_xlApp.Rows(1).RowHeight = 50
        m_xlApp.Rows(1).Font.Size = 18
        m_xlApp.Rows(1).Font.Bold = True

        m_xlApp.Cells(2, 1) = "编号"
        m_xlApp.Cells(2, 2) = "主控箱名称"
        m_xlApp.Cells(2, 3) = "路灯编号"
        m_xlApp.Cells(2, 4) = "报警原因"
        m_xlApp.Cells(2, 5) = "报警是否解除"
        m_xlApp.Cells(2, 6) = "开始时间"
        m_xlApp.Cells(2, 7) = "结束时间"
        m_xlApp.Rows(2).Font.Bold = True
        m_xlApp.Rows(2).font.size = 9
        m_xlApp.Rows(2).RowHeight = 30

        m_row = 3
        Dim i As Integer = 0

        While i < dgv_problem_list.RowCount
            Me.BackgroundWorker1.ReportProgress(i * 100 / dgv_problem_list.RowCount)
            m_xlApp.Cells(m_row, 1) = Trim(dgv_problem_list.Rows(i).Cells("alarm_id").Value)  '报警
            m_xlApp.Cells(m_row, 2) = Trim(dgv_problem_list.Rows(i).Cells("control_box_name").Value) '主控箱名称
            m_xlApp.Cells(m_row, 3) = "'" & Trim(dgv_problem_list.Rows(i).Cells("lamp_id_short").Value)  '灯的短编号
            m_xlApp.Cells(m_row, 4) = Trim(dgv_problem_list.Rows(i).Cells("problem_inf").Value)  '故障信息
            m_xlApp.Cells(m_row, 5) = "'" & Trim(dgv_problem_list.Rows(i).Cells("alarm_ok").Value.ToString) '故障是否完成
            m_xlApp.Cells(m_row, 6) = "'" & Trim(dgv_problem_list.Rows(i).Cells("date_start").Value.ToString) '开始时间
            m_xlApp.Cells(m_row, 7) = "'" & Trim(dgv_problem_list.Rows(i).Cells("Date_end").Value.ToString) '结束时间

            m_row += 1
            i += 1

        End While


        With m_xlSheet
            .Range(.Cells(1, 1), .Cells(1, 7)).Merge()

            .Range(.Cells(2, 1), .Cells(2, 1)).ColumnWidth = 7
            .Range(.Cells(2, 2), .Cells(2, 2)).ColumnWidth = 7
            .Range(.Cells(2, 3), .Cells(2, 3)).ColumnWidth = 9
            .Range(.Cells(2, 4), .Cells(2, 4)).ColumnWidth = 10
            .Range(.Cells(2, 5), .Cells(2, 5)).ColumnWidth = 20
            .Range(.Cells(2, 6), .Cells(2, 6)).ColumnWidth = 20
            .Range(.Cells(2, 7), .Cells(2, 6)).ColumnWidth = 20
            .Range(.Cells(1, 1), .Cells(m_row - 1, 7)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter


            .Range(.Cells(2, 1), .Cells(1, 7)).Font.Name = "宋体"
            '设标题为宋体字
            .Range(.Cells(2, 1), .Cells(1, 7)).Font.Bold = "True"
            '标题字体加粗
            .Range(.Cells(1, 1), .Cells(m_row - 1, 7)).Borders.LineStyle = 1
            '设表格边框样式
            .Range(.Cells(3, 1), .Cells(m_row - 1, 7)).Font.Size = 9
            '表中数据的字号

        End With
    End Sub

    ''' <summary>
    ''' 电控箱编号
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_control_box_id_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_control_box_name.DropDown
        'Com_inf.Select_box_name(pro_control_box_name)
        Com_inf.Select_box_name_level(cb_city_name, cb_area_name, cb_street_name, cb_control_box_name)

    End Sub

    ''' <summary>
    ''' 路灯编号
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_lamp_id_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_lamp_id.DropDown
        Com_inf.Select_lamp_id_type(cb_control_box_name, cb_lamp_type, cb_lamp_id, lb_lamp_id_start)
    End Sub

    ''' <summary>
    ''' 电控箱编号改变，影响到其他的数据
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_control_box_id_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_control_box_name.SelectedIndexChanged
        'Com_inf.Select_type_name(pro_control_box_name, pro_lamp_type, lamp_type_id)
        'Com_inf.Select_lamp_id_type(pro_control_box_name, pro_lamp_type, pro_lamp_id, pro_lamp_id_start)
        Com_inf.Select_type_name(cb_control_box_name, cb_lamp_type, lb_lamp_type_id)

    End Sub

    ''' <summary>
    ''' 查询数据
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_find_record_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_find_record.Click

        Dim time_start, time_end As System.DateTime
        Dim lamp_id_string As String  '记录路灯的完整编号
        Dim rs As New ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim conn As New ADODB.Connection

        DBOperation.OpenConn(conn)
        msg = ""
        sql = "select * from box_lamptype_view where control_box_name='" & Trim(cb_control_box_name.Text) & "' and type_string='" & Trim(cb_lamp_type.Text) & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "find_record_Click", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs.RecordCount > 0 Then
            If rs.Fields("type_id").Value < 10 Then
                lamp_id_string = Trim(rs.Fields("control_box_id").Value) & "0" & rs.Fields("type_id").Value.ToString & Trim(cb_lamp_id.Text)
            Else
                If rs.Fields("type_id").Value >= 10 And rs.Fields("type_id").Value < 100 Then
                    lamp_id_string = Trim(rs.Fields("control_box_id").Value) & rs.Fields("type_id").Value.ToString & Trim(cb_lamp_id.Text)

                Else
                    MsgBox("灯的种类超出最大值", , PROJECT_TITLE_STRING)
                    rs.Close()
                    rs = Nothing
                    conn.Close()
                    conn = Nothing
                    Exit Sub
                End If

            End If

        Else
            MsgBox("没有该类型景观灯的编号", , PROJECT_TITLE_STRING)
            rs.Close()
            rs = Nothing
            conn.Close()
            conn = Nothing
            Exit Sub

        End If

        If rb_date_condition_find.Checked = True Then '如果选择按日期查询
            If dtp_date_start.Text = "" Then  '开始日期为空
                MsgBox("请选择开始时间", , PROJECT_TITLE_STRING)
                dtp_date_start.Focus()  '光标定位在开始日期
                Exit Sub
            End If
            If dtp_date_start.Text = "" Then  '结束日期为空
                MsgBox("请选择结束时间", , PROJECT_TITLE_STRING)
                dtp_date_end.Focus()  '光标定位在结束日期
                Exit Sub
            End If
            If alarm_okornot.Text = "" Then
                MsgBox("请选择报警是否解除", , PROJECT_TITLE_STRING)
                alarm_okornot.Focus()
                Exit Sub
            End If

            time_start = dtp_date_start.Value  '开始日期
            time_end = dtp_date_end.Value  '结束日期
            dtp_date_start.Text = Format(time_start, "yyyy-MM-dd HH:mm:ss")  '开始日期格式化
            '按日期及城市进行查询
            If rb_city_control.Checked = True And alarm_okornot.Text = "全部" Then
                'Alarm_viewTableAdapter.FillBy_city_date(StreetlampDataSet.alarm_view, time_start, time_end, Trim(cb_city_name.Text))
                sql = "select * from Record_view where city='" & Trim(cb_city_name.Text) & "' and ((date<'" & time_start & "' and date_end>'" & time_start & "') or (date<'" & time_end & "' and date>'" & time_start & "')) order by date"

            End If
            If rb_city_control.Checked = True And alarm_okornot.Text = "是" Then
                ' Alarm_viewTableAdapter.FillBy_city_date_alarmok(StreetlampDataSet.alarm_view, time_start, time_end, Trim(cb_city_name.Text), 1, 1)
                sql = "select * from Record_view where city='" & Trim(cb_city_name.Text) & "' and ((date<'" & time_start & "' and date_end>'" & time_start & "') or (date<'" & time_end & "' and date>'" & time_start & "'))  and (server_state=1 or server_state=1) order by date"

            End If
            If rb_city_control.Checked = True And alarm_okornot.Text = "否" Then
                'Alarm_viewTableAdapter.FillBy_city_date_alarmok(StreetlampDataSet.alarm_view, time_start, time_end, Trim(cb_city_name.Text), 0, 2)
                sql = "select * from Record_view where city='" & Trim(cb_city_name.Text) & "' and ((date<'" & time_start & "' and date_end>'" & time_start & "') or (date<'" & time_end & "' and date>'" & time_start & "'))  and (server_state=0 or server_state=2) order by date"
            End If

            '按日期及区域进行查询
            If rb_area_control.Checked = True And alarm_okornot.Text = "全部" Then
                'Alarm_viewTableAdapter.FillBy_area_date(StreetlampDataSet.alarm_view, time_start, time_end, Trim(cb_area_name.Text))
                sql = "select * from Record_view where city='" & Trim(cb_city_name.Text) & "' and area='" & Trim(cb_area_name.Text) & "' and ((date<'" & time_start & "' and date_end>'" & time_start & "') or (date<'" & time_end & "' and date>'" & time_start & "')) order by date"
            End If
            If rb_area_control.Checked = True And alarm_okornot.Text = "是" Then
                'Alarm_viewTableAdapter.FillBy_area_date_alarmok(StreetlampDataSet.alarm_view, time_start, time_end, Trim(cb_area_name.Text), 1, 1)
                sql = "select * from Record_view where city='" & Trim(cb_city_name.Text) & "' and area='" & Trim(cb_area_name.Text) & "'  and ((date<'" & time_start & "' and date_end>'" & time_start & "') or (date<'" & time_end & "' and date>'" & time_start & "'))  and (server_state=1 or server_state=1) order by date"
            End If
            If rb_area_control.Checked = True And alarm_okornot.Text = "否" Then
                'Alarm_viewTableAdapter.FillBy_area_date_alarmok(StreetlampDataSet.alarm_view, time_start, time_end, Trim(cb_area_name.Text), 0, 2)
                sql = "select * from Record_view where city='" & Trim(cb_city_name.Text) & "' and area='" & Trim(cb_area_name.Text) & "'  and ((date<'" & time_start & "' and date_end>'" & time_start & "') or (date<'" & time_end & "' and date>'" & time_start & "'))  and (server_state=0 or server_state=2) order by date"

            End If

            '按日期及街道进行查询
            If rb_street_control.Checked = True And alarm_okornot.Text = "全部" Then
                'Alarm_viewTableAdapter.FillBy_street_date(StreetlampDataSet.alarm_view, time_start, time_end, Trim(cb_street_name.Text))
                sql = "select * from Record_view where city='" & Trim(cb_city_name.Text) & "' and area='" & Trim(cb_area_name.Text) & "' and street='" & Trim(cb_street_name.Text) & "' and ((date<'" & time_start & "' and date_end>'" & time_start & "') or (date<'" & time_end & "' and date>'" & time_start & "')) order by date"

            End If
            If rb_street_control.Checked = True And alarm_okornot.Text = "是" Then
                'Alarm_viewTableAdapter.FillBy_street_date_alarmok(StreetlampDataSet.alarm_view, time_start, time_end, Trim(cb_street_name.Text), 1, 1)
                sql = "select * from Record_view where city='" & Trim(cb_city_name.Text) & "' and area='" & Trim(cb_area_name.Text) & "' and street='" & Trim(cb_street_name.Text) & "' and ((date<'" & time_start & "' and date_end>'" & time_start & "') or (date<'" & time_end & "' and date>'" & time_start & "'))   and (server_state=1 or server_state=1) order by date"

            End If
            If rb_street_control.Checked = True And alarm_okornot.Text = "否" Then
                'Alarm_viewTableAdapter.FillBy_street_date_alarmok(StreetlampDataSet.alarm_view, time_start, time_end, Trim(cb_street_name.Text), 0, 2)
                sql = "select * from Record_view where city='" & Trim(cb_city_name.Text) & "' and area='" & Trim(cb_area_name.Text) & "' and street='" & Trim(cb_street_name.Text) & "' and ((date<'" & time_start & "' and date_end>'" & time_start & "') or (date<'" & time_end & "' and date>'" & time_start & "'))   and (server_state=0 or server_state=2) order by date"

            End If


            '按日期及主控箱编号进行查询
            If rb_control_box_name_control.Checked = True And alarm_okornot.Text = "全部" Then
                'Alarm_viewTableAdapter.FillBy_control_box_date(StreetlampDataSet.alarm_view, time_start, time_end, Trim(cb_control_box_name.Text))
                sql = "select * from Record_view where control_box_name='" & Trim(cb_control_box_name.Text) & "' and ((date<'" & time_start & "' and date_end>'" & time_start & "') or (date<'" & time_end & "' and date>'" & time_start & "')) order by date"

            End If
            If rb_control_box_name_control.Checked = True And alarm_okornot.Text = "是" Then
                'Alarm_viewTableAdapter.FillBy_control_box_date_alarmok(StreetlampDataSet.alarm_view, time_start, time_end, Trim(cb_control_box_name.Text), 1, 1)
                sql = "select * from Record_view where control_box_name='" & Trim(cb_control_box_name.Text) & "' and ((date<'" & time_start & "' and date_end>'" & time_start & "') or (date<'" & time_end & "' and date>'" & time_start & "'))   and (server_state=1 or server_state=1) order by date"

            End If
            If rb_control_box_name_control.Checked = True And alarm_okornot.Text = "否" Then
                'Alarm_viewTableAdapter.FillBy_control_box_date_alarmok(StreetlampDataSet.alarm_view, time_start, time_end, Trim(cb_control_box_name.Text), 0, 2)
                sql = "select * from Record_view where control_box_name='" & Trim(cb_control_box_name.Text) & "' and ((date<'" & time_start & "' and date_end>'" & time_start & "') or (date<'" & time_end & "' and date>'" & time_start & "'))   and (server_state=0 or server_state=2) order by date"

            End If


            '按日期及类型进行查询
            If rb_lamp_type_control.Checked = True And alarm_okornot.Text = "全部" Then
                'Alarm_viewTableAdapter.FillBy_type_date(StreetlampDataSet.alarm_view, time_start, time_end, Trim(cb_control_box_name.Text), Trim(cb_lamp_type.Text))
                sql = "select * from Record_view where control_box_name='" & Trim(cb_control_box_name.Text) & "' and type_string='" & Trim(cb_lamp_type.Text) & "' and ((date<'" & time_start & "' and date_end>'" & time_start & "') or (date<'" & time_end & "' and date>'" & time_start & "')) order by date "

            End If
            If rb_lamp_type_control.Checked = True And alarm_okornot.Text = "是" Then
                'Alarm_viewTableAdapter.FillBy_type_date_alarmok(StreetlampDataSet.alarm_view, time_start, time_end, Trim(cb_control_box_name.Text), Trim(cb_lamp_type.Text), 1, 1)
                sql = "select * from Record_view where control_box_name='" & Trim(cb_control_box_name.Text) & "' and type_string='" & Trim(cb_lamp_type.Text) & "' and ((date<'" & time_start & "' and date_end>'" & time_start & "') or (date<'" & time_end & "' and date>'" & time_start & "'))   and (server_state=1 or server_state=1) order by date"

            End If
            If rb_lamp_type_control.Checked = True And alarm_okornot.Text = "否" Then
                'Alarm_viewTableAdapter.FillBy_type_date_alarmok(StreetlampDataSet.alarm_view, time_start, time_end, Trim(cb_control_box_name.Text), Trim(cb_lamp_type.Text), 0, 2)
                sql = "select * from Record_view where control_box_name='" & Trim(cb_control_box_name.Text) & "' and type_string='" & Trim(cb_lamp_type.Text) & "' and ((date<'" & time_start & "' and date_end>'" & time_start & "') or (date<'" & time_end & "' and date>'" & time_start & "'))   and (server_state=0 or server_state=2) order by date"

            End If

            '按日期及单灯进行查询
            If rb_lamp_id_control.Checked = True And alarm_okornot.Text = "全部" Then
                sql = "select * from Record_view where lamp_id='" & lamp_id_string & "' and ((date<'" & time_start & "' and date_end>'" & time_start & "') or (date<'" & time_end & "' and date>'" & time_start & "')) order by date "

                'Alarm_viewTableAdapter.FillBy_lampid_date(StreetlampDataSet.alarm_view, Trim(cb_control_box_name.Text), Trim(cb_lamp_type.Text), lamp_id_string, time_start, time_end)
            End If
            If rb_lamp_id_control.Checked = True And alarm_okornot.Text = "是" Then
                sql = "select * from Record_view where lamp_id='" & lamp_id_string & "' and ((date<'" & time_start & "' and date_end>'" & time_start & "') or (date<'" & time_end & "' and date>'" & time_start & "')) and (server_state=1 or server_state=1) order by date"

                ' Alarm_viewTableAdapter.FillBy_lampid_date_alarmok(StreetlampDataSet.alarm_view, lamp_id_string, time_start, time_start, 1, 1, time_end, time_start)
            End If
            If rb_lamp_id_control.Checked = True And alarm_okornot.Text = "否" Then
                'Alarm_viewTableAdapter.FillBy_lampid_date_alarmok(StreetlampDataSet.alarm_view, lamp_id_string, time_start, time_start, 0, 2, time_end, time_start)
                sql = "select * from Record_view where lamp_id='" & lamp_id_string & "' and ((date<'" & time_start & "' and date_end>'" & time_start & "') or (date<'" & time_end & "' and date>'" & time_start & "')) and (server_state=0 or server_state=2) order by date"

            End If


        End If

        If rb_no_date_condition_find.Checked = True Then  '不按日期进行查询
            If alarm_okornot.Text = "" Then
                MsgBox("请选择报警是否解除", , PROJECT_TITLE_STRING)
                alarm_okornot.Focus()
                Exit Sub
            End If
            '按城市进行查询
            If rb_city_control.Checked = True And alarm_okornot.Text = "全部" Then
                ' Alarm_viewTableAdapter.FillBy_city(StreetlampDataSet.alarm_view, Trim(cb_city_name.Text))
                sql = "select * from Record_view where city='" & Trim(cb_city_name.Text) & "' order by date"

            End If
            If rb_city_control.Checked = True And alarm_okornot.Text = "是" Then
                'Alarm_viewTableAdapter.FillBy_city_alarmok(StreetlampDataSet.alarm_view, Trim(cb_city_name.Text), 1, 1)
                sql = "select * from Record_view where city='" & Trim(cb_city_name.Text) & "'  and (server_state=1 or server_state=1) order by date"

            End If
            If rb_city_control.Checked = True And alarm_okornot.Text = "否" Then
                'Alarm_viewTableAdapter.FillBy_city_alarmok(StreetlampDataSet.alarm_view, Trim(cb_city_name.Text), 0, 2)
                sql = "select * from Record_view where city='" & Trim(cb_city_name.Text) & "'  and (server_state=0 or server_state=2) order by date"

            End If

            '按区域进行查询
            If rb_area_control.Checked = True And alarm_okornot.Text = "全部" Then
                'Alarm_viewTableAdapter.FillBy_area(StreetlampDataSet.alarm_view, Trim(cb_area_name.Text))
                sql = "select * from Record_view where city='" & Trim(cb_city_name.Text) & "' and area='" & Trim(cb_area_name.Text) & "' order by date"

            End If
            If rb_area_control.Checked = True And alarm_okornot.Text = "是" Then
                ' Alarm_viewTableAdapter.FillBy_area_alarmok(StreetlampDataSet.alarm_view, Trim(cb_area_name.Text), 1, 1)
                sql = "select * from Record_view where city='" & Trim(cb_city_name.Text) & "' and area='" & Trim(cb_area_name.Text) & "' and (server_state=1 or server_state=1) order by date"

            End If
            If rb_area_control.Checked = True And alarm_okornot.Text = "否" Then
                ' Alarm_viewTableAdapter.FillBy_area_alarmok(StreetlampDataSet.alarm_view, Trim(cb_area_name.Text), 0, 2)
                sql = "select * from Record_view where city='" & Trim(cb_city_name.Text) & "' and area='" & Trim(cb_area_name.Text) & "' and (server_state=0 or server_state=2) order by date"

            End If

            '按街道进行查询
            If rb_street_control.Checked = True And alarm_okornot.Text = "全部" Then
                ' Alarm_viewTableAdapter.FillBy_street(StreetlampDataSet.alarm_view, Trim(cb_street_name.Text))
                sql = "select * from Record_view where city='" & Trim(cb_city_name.Text) & "' and area='" & Trim(cb_area_name.Text) & "' and street='" & Trim(cb_street_name.Text) & "' order by date "

            End If
            If rb_street_control.Checked = True And alarm_okornot.Text = "是" Then
                '  Alarm_viewTableAdapter.FillBy_street_alarmok(StreetlampDataSet.alarm_view, Trim(cb_street_name.Text), 1, 1)
                sql = "select * from Record_view where city='" & Trim(cb_city_name.Text) & "' and area='" & Trim(cb_area_name.Text) & "' and street='" & Trim(cb_street_name.Text) & "' and (server_state=1 or server_state=1) order by date"

            End If
            If rb_street_control.Checked = True And alarm_okornot.Text = "否" Then
                ' Alarm_viewTableAdapter.FillBy_street_alarmok(StreetlampDataSet.alarm_view, Trim(cb_street_name.Text), 0, 2)
                sql = "select * from Record_view where city='" & Trim(cb_city_name.Text) & "' and area='" & Trim(cb_area_name.Text) & "' and street='" & Trim(cb_street_name.Text) & "' and (server_state=0 or server_state=2) order by date"

            End If


            '按主控箱编号进行查询
            If rb_control_box_name_control.Checked = True And alarm_okornot.Text = "全部" Then
                ' Alarm_viewTableAdapter.FillBy_control_box(StreetlampDataSet.alarm_view, Trim(cb_control_box_name.Text))
                sql = "select * from Record_view where control_box_name='" & Trim(cb_control_box_name.Text) & "' order by date"

            End If
            If rb_control_box_name_control.Checked = True And alarm_okornot.Text = "是" Then
                ' Alarm_viewTableAdapter.FillBy_control_box_alarmok(StreetlampDataSet.alarm_view, Trim(cb_control_box_name.Text), 1, 1)
                sql = "select * from Record_view where control_box_name='" & Trim(cb_control_box_name.Text) & "' and (server_state=1 or server_state=1) order by date"

            End If
            If rb_control_box_name_control.Checked = True And alarm_okornot.Text = "否" Then
                ' Alarm_viewTableAdapter.FillBy_control_box_alarmok(StreetlampDataSet.alarm_view, Trim(cb_control_box_name.Text), 0, 2)
                sql = "select * from Record_view where control_box_name='" & Trim(cb_control_box_name.Text) & "' and (server_state=0 or server_state=2) order by date"

            End If


            '按类型进行查询
            If rb_lamp_type_control.Checked = True And alarm_okornot.Text = "全部" Then
                ' Alarm_viewTableAdapter.FillBy_type(StreetlampDataSet.alarm_view, Trim(cb_control_box_name.Text), Trim(cb_lamp_type.Text))
                sql = "select * from Record_view where control_box_name='" & Trim(cb_control_box_name.Text) & "' and type_string='" & Trim(cb_lamp_type.Text) & "' order by date"

            End If
            If rb_lamp_type_control.Checked = True And alarm_okornot.Text = "是" Then
                'Alarm_viewTableAdapter.FillBy_type_alarmok(StreetlampDataSet.alarm_view, Trim(cb_control_box_name.Text), Trim(cb_lamp_type.Text), 1, 1)
                sql = "select * from Record_view where control_box_name='" & Trim(cb_control_box_name.Text) & "' and type_string='" & Trim(cb_lamp_type.Text) & "' and (server_state=1 or server_state=1) order by date"

            End If
            If rb_lamp_type_control.Checked = True And alarm_okornot.Text = "否" Then
                'Alarm_viewTableAdapter.FillBy_type_alarmok(StreetlampDataSet.alarm_view, Trim(cb_control_box_name.Text), Trim(cb_lamp_type.Text), 0, 2)
                sql = "select * from Record_view where control_box_name='" & Trim(cb_control_box_name.Text) & "' and type_string='" & Trim(cb_lamp_type.Text) & "' and (server_state=0 or server_state=2) order by date"

            End If

            '按单灯进行查询
            If rb_lamp_id_control.Checked = True And alarm_okornot.Text = "全部" Then
                'Alarm_viewTableAdapter.FillBy_lampid(StreetlampDataSet.alarm_view, Trim(cb_control_box_name.Text), Trim(cb_lamp_type.Text), lamp_id_string)
                sql = "select * from Record_view where lamp_id='" & lamp_id_string & "' order by date"

            End If
            If rb_lamp_id_control.Checked = True And alarm_okornot.Text = "是" Then
                ' Alarm_viewTableAdapter.FillBy_lampid_alarmok(StreetlampDataSet.alarm_view, Trim(cb_control_box_name.Text), Trim(cb_lamp_type.Text), lamp_id_string, 1, 1)
                sql = "select * from Record_view where lamp_id='" & lamp_id_string & "' and (server_state=1 or server_state=1) order by date"

            End If
            If rb_lamp_id_control.Checked = True And alarm_okornot.Text = "否" Then
                ' Alarm_viewTableAdapter.FillBy_lampid_alarmok(StreetlampDataSet.alarm_view, Trim(cb_control_box_name.Text), Trim(cb_lamp_type.Text), lamp_id_string, 0, 2)
                sql = "select * from Record_view where lamp_id='" & lamp_id_string & "' and (server_state=0 or server_state=2) order by date"

            End If
        End If

        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING, , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        Dim i As Integer = 0
        dgv_problem_list.Rows.Clear()
        While rs.EOF = False
            dgv_problem_list.Rows.Add()
            dgv_problem_list.Rows(i).Cells("alarm_id").Value = rs.Fields("id").Value
            dgv_problem_list.Rows(i).Cells("control_box_name").Value = rs.Fields("control_box_name").Value
            dgv_problem_list.Rows(i).Cells("lamp_id_short").Value = Mid(Trim(rs.Fields("lamp_id").Value), 7, LAMP_ID_LEN)
            dgv_problem_list.Rows(i).Cells("problem_inf").Value = Trim(rs.Fields("problem_inf").Value)
            If time_start > rs.Fields("date").Value Then
                dgv_problem_list.Rows(i).Cells("Date_start").Value = time_start
            Else
                dgv_problem_list.Rows(i).Cells("Date_start").Value = rs.Fields("date").Value

            End If
            dgv_problem_list.Rows(i).Cells("Date_end").Value = rs.Fields("date_end").Value
            dgv_problem_list.Rows(i).Cells("server_state").Value = rs.Fields("server_state").Value
            dgv_problem_list.Rows(i).Cells("type_string").Value = Trim(rs.Fields("type_string").Value)
            i += 1
            rs.MoveNext()
        End While

        choose_state() '对路灯的编号进行重新赋值
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If

        conn.Close()
        conn = Nothing
        record_num.Text = "共有" & dgv_problem_list.RowCount.ToString & "条记录"  '载入的故障条数
    End Sub


    ''' <summary>
    ''' 查询生成报表
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork

        excel()

    End Sub

    ''' <summary>
    ''' 标志工作的提示文字
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BackgroundWorker1_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker1.ProgressChanged
        If m_worktabletag = 1 And m_stringtag = 0 Then
            record_num.Text = "正在导出工单，请稍候......"
            m_stringtag = 1
        Else
            If m_stringtag = 0 Then
                record_num.Text = "导出报警信息表"
                m_stringtag = 1
            End If

        End If
        progress.Value = e.ProgressPercentage
    End Sub

    ''' <summary>
    ''' 报表生成结束
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BackgroundWorker1_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        progress.Visible = False
        m_xlApp.Visible = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default


        If m_worktabletag = 1 Then
            'TODO: 这行代码将数据加载到表“StreetlampDataSet.alarm_view”中。您可以根据需要移动或移除它。
            Me.Alarm_viewTableAdapter.Fill(Me.StreetlampDataSet.alarm_view)
            Me.dgv_problem_list.Columns(6).DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss"

            record_num.Text = "共有" & dgv_problem_list.RowCount.ToString & "条记录"

        End If


    End Sub

    Private Sub rb_date_condition_find_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_date_condition_find.Click
        If rb_date_condition_find.Checked = True Then
            dtp_date_start.Enabled = True
            dtp_date_end.Enabled = True
        End If
    End Sub

    Private Sub rb_no_date_condition_find_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_no_date_condition_find.Click
        If rb_no_date_condition_find.Checked = True Then
            dtp_date_start.Enabled = False
            dtp_date_end.Enabled = False
        End If
    End Sub

    Private Sub cb_lamp_type_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_lamp_type.DropDown
        Com_inf.Select_type_name(cb_control_box_name, cb_lamp_type, lb_lamp_type_id)
    End Sub

    Private Sub cb_lamp_type_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_lamp_type.SelectedIndexChanged
        Com_inf.Select_lamp_id_type(cb_control_box_name, cb_lamp_type, cb_lamp_id, lb_lamp_id_start)

    End Sub

    Private Sub rb_control_box_name_control_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_control_box_name_control.Click
        If rb_control_box_name_control.Checked = True Then
            Me.cb_city_name.Enabled = True
            Me.cb_area_name.Enabled = True
            Me.cb_street_name.Enabled = True
            Me.cb_control_box_name.Enabled = True
            Me.cb_lamp_type.Enabled = False
            Me.cb_lamp_id.Enabled = False
        End If
    End Sub

    Private Sub rb_lamp_id_control_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_lamp_id_control.Click
        If rb_lamp_id_control.Checked = True Then
            Me.cb_city_name.Enabled = True
            Me.cb_area_name.Enabled = True
            Me.cb_street_name.Enabled = True
            Me.cb_control_box_name.Enabled = True
            Me.cb_lamp_type.Enabled = True
            Me.cb_lamp_id.Enabled = True
        End If
    End Sub

    Private Sub dgv_problem_list_ColumnHeaderMouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgv_problem_list.ColumnHeaderMouseClick
        choose_state() '标识是否警报解除
    End Sub

    Private Sub bt_excel_data_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_excel_data.Click
        record_num.Text = "正在导出EXCEL表，请稍候......"
        m_worktabletag = 0
        progress.Visible = True
        m_stringtag = 0

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        BackgroundWorker1.RunWorkerAsync()

    End Sub

    Private Sub bt_reflash_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_reflash.Click
        'TODO: 这行代码将数据加载到表“StreetlampDataSet.alarm_view”中。您可以根据需要移动或移除它。
        Me.Alarm_viewTableAdapter.Fill(Me.StreetlampDataSet.alarm_view)
        'Me.problem_list.Columns(6).DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss"  '日期的格式化
        get_all_probleminf()  '获取所有报警信息
        choose_state()
        record_num.Text = "共有" & dgv_problem_list.RowCount.ToString & "条记录"  '刷新故障条数

    End Sub

    ''' <summary>
    ''' 窗口在线程运行的过程中被关闭则关闭线程，关闭EXCEL进程
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 故障列表_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If Me.BackgroundWorker1.IsBusy = True Then
            Me.BackgroundWorker1.CancelAsync()
        End If
       ProcessKill(m_xlApp, m_xlBook, m_xlSheet)
        g_windowclose = 1
    End Sub

    Private Sub cb_city_name_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_city_name.DropDown
        Com_inf.Select_city_name(cb_city_name)
    End Sub

    Private Sub cb_city_name_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_city_name.SelectedIndexChanged
        Com_inf.Select_area_name(cb_city_name, cb_area_name)
        'Com_inf.Select_street_name(cb_city_name, pro_area_name, pro_street_name)
        'Com_inf.Select_box_name_level(cb_city_name, pro_area_name, pro_street_name, pro_control_box_name)
        'Com_inf.Select_type_name(pro_control_box_name, pro_lamp_type, lamp_type_id)
        'Com_inf.Select_lamp_id_type(pro_control_box_name, pro_lamp_type, pro_lamp_id, pro_lamp_id_start)

    End Sub

    Private Sub cb_area_name_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_area_name.DropDown
        Com_inf.Select_area_name(cb_city_name, cb_area_name)
    End Sub

    Private Sub cb_area_name_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_area_name.SelectedIndexChanged
        Com_inf.Select_street_name(cb_city_name, cb_area_name, cb_street_name)
        'Com_inf.Select_box_name_level(cb_city_name, cb_area_name, pro_street_name, pro_control_box_name)
        'Com_inf.Select_type_name(pro_control_box_name, pro_lamp_type, lamp_type_id)
        'Com_inf.Select_lamp_id_type(pro_control_box_name, pro_lamp_type, pro_lamp_id, pro_lamp_id_start)

    End Sub

    Private Sub cb_street_name_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_street_name.DropDown
        Com_inf.Select_street_name(cb_city_name, cb_area_name, cb_street_name)

    End Sub

    Private Sub cb_street_name_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_street_name.SelectedIndexChanged
        Com_inf.Select_box_name_level(cb_city_name, cb_area_name, cb_street_name, cb_control_box_name)
        'Com_inf.Select_type_name(pro_control_box_name, pro_lamp_type, lamp_type_id)
        'Com_inf.Select_lamp_id_type(pro_control_box_name, pro_lamp_type, pro_lamp_id, pro_lamp_id_start)

    End Sub

    Private Sub rb_city_control_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_city_control.Click
        If rb_city_control.Checked = True Then
            Me.cb_city_name.Enabled = True
            Me.cb_area_name.Enabled = False
            Me.cb_street_name.Enabled = False
            Me.cb_control_box_name.Enabled = False
            Me.cb_lamp_type.Enabled = False
            Me.cb_lamp_id.Enabled = False
        End If
    End Sub

    Private Sub rb_area_control_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_area_control.Click
        If rb_area_control.Checked = True Then
            Me.cb_city_name.Enabled = True
            Me.cb_area_name.Enabled = True
            Me.cb_street_name.Enabled = False
            Me.cb_control_box_name.Enabled = False
            Me.cb_lamp_type.Enabled = False
            Me.cb_lamp_id.Enabled = False
        End If
    End Sub

    Private Sub rb_street_control_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_street_control.Click
        If rb_street_control.Checked = True Then
            Me.cb_city_name.Enabled = True
            Me.cb_area_name.Enabled = True
            Me.cb_street_name.Enabled = True
            Me.cb_control_box_name.Enabled = False
            Me.cb_lamp_type.Enabled = False
            Me.cb_lamp_id.Enabled = False
        End If
    End Sub

    Private Sub rb_lamp_type_control_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_lamp_type_control.Click
        If rb_lamp_type_control.Checked = True Then
            Me.cb_city_name.Enabled = True
            Me.cb_area_name.Enabled = True
            Me.cb_street_name.Enabled = True
            Me.cb_control_box_name.Enabled = True
            Me.cb_lamp_type.Enabled = True
            Me.cb_lamp_id.Enabled = False
        End If
    End Sub



End Class