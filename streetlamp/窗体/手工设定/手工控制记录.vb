''' <summary>
''' 查询导出手工控制记录（包括开关灯、各种工作人员的操作记录）
''' </summary>
''' <remarks></remarks>

Public Class 手工控制记录
    Private m_xlApp As Microsoft.Office.Interop.Excel.Application
    Private m_xlBook As Microsoft.Office.Interop.Excel.Workbook
    Private m_xlSheet As Microsoft.Office.Interop.Excel.Worksheet
    Private m_cityname As String  '城市名称
    Private m_areaname As String  '区域名称
    Private m_streetname As String  '街道名称
    Private m_controlboxname As String  '主控箱名称
    Private m_lamptype As String  '节点类型
    Private m_lampid As String  '节点编号
    Private m_row As Integer   'excel表的行数
    Private m_stringtag As Integer
    Private m_excel_kind As Integer '生成EXCEL的种类 0为控制日志，1为操作日志


    ''' <summary>
    ''' 手工控制记录的load函数
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 手工控制记录_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: 这行代码将数据加载到表“Operation_list.operation_record”中。您可以根据需要移动或移除它。
        Me.Operation_recordTableAdapter.Fill(Me.Operation_list.operation_record)
        Me.Icon = New Icon("图片\favicon.ico", 32, 32)
        Me.Hand_control_recordTableAdapter.Fill(Me.Hand_record.hand_control_record)  '手工控制列表

        record_num.Text = "共有" & dgv_hand_control_list.RowCount.ToString & "条控制记录，" & dgv_operation_record.RowCount.ToString & "条操作记录"  '提示记录条数
        dtp_date_start.CustomFormat = "yyyy-MM-dd HH:mm:ss"  '开始时间格式
        dtp_date_end.CustomFormat = "yyyy-MM-dd HH:mm:ss"  '结束时间格式

        dtp_operation_starttime.CustomFormat = "yyyy-MM-dd HH:mm:ss"  '开始时间格式
        dtp_operation_endtime.CustomFormat = "yyyy-MM-dd HH:mm:ss"  '结束时间格式

        '开始日期默认为当前日期的前一天
        dtp_date_start.Value = DateAdd(DateInterval.Day, -1, Now)
        dtp_operation_starttime.Value = DateAdd(DateInterval.Day, -1, Now)
        Com_inf.Select_city_name(cb_city_name) '初始化下拉框的内容

        progress.Visible = False
        lamp_id_start.Visible = False
        lamp_type_id.Visible = False

        m_excel_kind = 0
    End Sub

    ''' <summary>
    ''' 导出手工控制的excel表
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub excel_control()
        Dim rowIndex, colIndex As Integer
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        m_xlApp = (New Microsoft.Office.Interop.Excel.Application)

        rowIndex = 1
        colIndex = 0

        m_xlBook = m_xlApp.Workbooks().Add
        m_xlSheet = m_xlBook.Worksheets("sheet1")

        m_xlApp.Cells(1, 1) = "系统日志（控制）"
        m_xlApp.Rows(1).RowHeight = 50
        m_xlApp.Rows(1).Font.Size = 18
        m_xlApp.Rows(1).Font.Bold = True

        Dim dt As New DataTable
        Dim id As Integer = 1
        m_xlApp.Cells(2, 1) = "编号"
        m_xlApp.Cells(2, 2) = "控制范围"
        m_xlApp.Cells(2, 3) = "控制名称"
        m_xlApp.Cells(2, 4) = "电感调光"
        m_xlApp.Cells(2, 5) = "电子调光"
        m_xlApp.Cells(2, 6) = "控制方式"
        m_xlApp.Cells(2, 7) = "时间"
        m_xlApp.Cells(2, 8) = "用户"
        m_xlApp.Rows(2).Font.Bold = True
        m_xlApp.Rows(2).font.size = 12
        m_xlApp.Rows(2).RowHeight = 30

        m_row = 3
        Dim i As Integer = 0


        Dim row_count As Integer
        row_count = dgv_hand_control_list.RowCount
        Dim progress_percentage As Integer

        While i < row_count
            progress_percentage = i * 100 / row_count + 1
            If progress_percentage > 100 Then
                progress_percentage = 100
            End If
            Me.BackgroundWorker1.ReportProgress(progress_percentage)

            m_xlApp.Cells(m_row, 1) = "'" & id
            m_xlApp.Cells(m_row, 2) = dgv_hand_control_list.Rows(i).Cells("Controlcontent").Value
            m_xlApp.Cells(m_row, 3) = "'" & dgv_hand_control_list.Rows(i).Cells("Contentname").Value
            m_xlApp.Cells(m_row, 4) = "'" & dgv_hand_control_list.Rows(i).Cells("diangan").Value
            m_xlApp.Cells(m_row, 5) = "'" & dgv_hand_control_list.Rows(i).Cells("power").Value
            m_xlApp.Cells(m_row, 6) = dgv_hand_control_list.Rows(i).Cells("Controlmethod").Value
            m_xlApp.Cells(m_row, 7) = Format(dgv_hand_control_list.Rows(i).Cells("Controltime").Value, "yyyy-MM-dd HH:mm:ss")
            m_xlApp.Cells(m_row, 8) = dgv_hand_control_list.Rows(i).Cells("user_name").Value

            m_row += 1
            i = i + 1
            id += 1

        End While

        With m_xlSheet
            .Range(.Cells(1, 1), .Cells(1, 6)).Merge()

            .Range(.Cells(2, 1), .Cells(2, 1)).ColumnWidth = 7
            .Range(.Cells(2, 2), .Cells(2, 2)).ColumnWidth = 7
            .Range(.Cells(2, 3), .Cells(2, 3)).ColumnWidth = 9
            .Range(.Cells(2, 4), .Cells(2, 4)).ColumnWidth = 9
            .Range(.Cells(2, 5), .Cells(2, 3)).ColumnWidth = 9
            .Range(.Cells(2, 6), .Cells(2, 4)).ColumnWidth = 9
            .Range(.Cells(2, 7), .Cells(2, 5)).ColumnWidth = 25
            .Range(.Cells(2, 8), .Cells(2, 6)).ColumnWidth = 15


            .Range(.Cells(1, 1), .Cells(m_row - 1, 8)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter


            .Range(.Cells(2, 1), .Cells(1, 8)).Font.Name = "宋体"
            '设标题为宋体字
            .Range(.Cells(2, 1), .Cells(1, 8)).Font.Bold = "True"
            '标题字体加粗
            .Range(.Cells(1, 1), .Cells(m_row - 1, 8)).Borders.LineStyle = 1
            '设表格边框样式
            .Range(.Cells(3, 1), .Cells(m_row - 1, 8)).Font.Size = 12
            '设置表格数据的字号

        End With
    End Sub

    ''' <summary>
    ''' 操作日志
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub excel_operation()
        Dim rowIndex, colIndex As Integer
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        m_xlApp = (New Microsoft.Office.Interop.Excel.Application)

        rowIndex = 1
        colIndex = 0

        m_xlBook = m_xlApp.Workbooks().Add
        m_xlSheet = m_xlBook.Worksheets("sheet1")

        m_xlApp.Cells(1, 1) = "系统日志（操作）"
        m_xlApp.Rows(1).RowHeight = 50
        m_xlApp.Rows(1).Font.Size = 18
        m_xlApp.Rows(1).Font.Bold = True

        Dim dt As New DataTable
        Dim id As Integer = 1
        m_xlApp.Cells(2, 1) = "编号"
        m_xlApp.Cells(2, 2) = "用户"
        m_xlApp.Cells(2, 3) = "操作"
        m_xlApp.Cells(2, 4) = "时间"

        m_xlApp.Rows(2).Font.Bold = True
        m_xlApp.Rows(2).font.size = 12
        m_xlApp.Rows(2).RowHeight = 30

        m_row = 3
        Dim i As Integer = 0


        Dim row_count As Integer
        row_count = dgv_operation_record.RowCount
        Dim progress_percentage As Integer

        While i < row_count
            progress_percentage = i * 100 / row_count + 1
            If progress_percentage > 100 Then
                progress_percentage = 100
            End If
            Me.BackgroundWorker1.ReportProgress(progress_percentage)
            m_xlApp.Cells(m_row, 1) = "'" & id

            m_xlApp.Cells(m_row, 2) = dgv_operation_record.Rows(i).Cells("operation_username").Value
            m_xlApp.Cells(m_row, 3) = dgv_operation_record.Rows(i).Cells("operation_string").Value
            m_xlApp.Cells(m_row, 4) = dgv_operation_record.Rows(i).Cells("operation_time").Value

            m_row += 1
            i = i + 1
            id += 1

        End While

        With m_xlSheet
            .Range(.Cells(1, 1), .Cells(1, 4)).Merge()

            .Range(.Cells(2, 1), .Cells(2, 1)).ColumnWidth = 7
            .Range(.Cells(2, 2), .Cells(2, 2)).ColumnWidth = 15
            .Range(.Cells(2, 3), .Cells(2, 3)).ColumnWidth = 50
            .Range(.Cells(2, 4), .Cells(2, 4)).ColumnWidth = 20



            .Range(.Cells(1, 1), .Cells(m_row - 1, 4)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter


            .Range(.Cells(2, 1), .Cells(1, 4)).Font.Name = "宋体"
            '设标题为宋体字
            .Range(.Cells(2, 1), .Cells(1, 4)).Font.Bold = "True"
            '标题字体加粗
            .Range(.Cells(1, 1), .Cells(m_row - 1, 4)).Borders.LineStyle = 1
            '设表格边框样式
            .Range(.Cells(3, 1), .Cells(m_row - 1, 4)).Font.Size = 12
            '设置表格数据的字号

        End With
    End Sub

    ''' <summary>
    ''' 按不同级别查询，区域，类型，级别
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_find_record_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_find_record.Click
        Dim time_start, time_end As System.DateTime
        Dim lamp_id_tag As String
        m_cityname = Trim(cb_city_name.Text)  '城市名称
        m_areaname = Trim(cb_area_name.Text) '区域名称
        m_streetname = Trim(cb_street_name.Text) '街道
        m_controlboxname = Trim(cb_control_box_name.Text)  '电控箱名称
        m_lamptype = Trim(cb_lamp_type.Text) '景观灯类型
        m_lampid = Trim(cb_lamp_id.Text)  '路灯编号


        If rb_date_condition_find.Checked = True Then  '按日期查询
            If dtp_date_start.Text = "" Then  '如果开始日期为空
                MsgBox("请选择开始时间", , PROJECT_TITLE_STRING)
                dtp_date_start.Focus() '光标定位在开始日期框
                Exit Sub
            End If
            If dtp_date_start.Text = "" Then  '如果结束日期为空
                MsgBox("请选择结束时间", , PROJECT_TITLE_STRING)
                dtp_date_end.Focus()  '光标定位在结束日期框
                Exit Sub
            End If

            time_start = dtp_date_start.Value '开始时间
            time_end = dtp_date_end.Value  '结束时间
            dtp_date_start.Text = Format(time_start, "yyyy-MM-dd HH:mm:ss")  '开始日期框中为当前日期
            If rb_hand_check.Checked = True Then
                '查询手工控制
                If rb_city_control.Checked = True Then
                    '选择城市+日期

                    Me.Hand_control_recordTableAdapter.FillBy_find_time(Me.Hand_record.hand_control_record, "城市", m_cityname, time_start, time_end)

                End If
                If rb_area_control.Checked = True Then
                    '选择区域+日期

                    Me.Hand_control_recordTableAdapter.FillBy_find_time(Me.Hand_record.hand_control_record, "区域", m_areaname, time_start, time_end)

                End If
                If rb_street_control.Checked = True Then
                    '选择街道+日期
                    Me.Hand_control_recordTableAdapter.FillBy_find_time(Me.Hand_record.hand_control_record, "街道", m_streetname, time_start, time_end)

                End If
                If rb_control_box_name_control.Checked = True Then
                    '选择电控箱+日期
                    Me.Hand_control_recordTableAdapter.FillBy_find_time(Me.Hand_record.hand_control_record, "主控箱", m_controlboxname, time_start, time_end)
                End If

                If rb_lamp_type_control.Checked = True Then
                    '选择类型+日期
                    Me.Hand_control_recordTableAdapter.FillBy_find_time(Me.Hand_record.hand_control_record, "类型", m_controlboxname & " " & m_lamptype, time_start, time_end)

                End If

                If rb_lamp_id_control.Checked = True And rb_date_condition_find.Checked = True Then
                    '选择灯的编号+日期
                    lamp_id_tag = Val(Mid(Trim(lamp_id_start.Text) & m_lampid, 1, 4)).ToString & "-" & Val(Mid(Trim(lamp_id_start.Text) & m_lampid, 5, 2)).ToString & "-" & Val(Mid(Trim(lamp_id_start.Text) & m_lampid, 7, LAMP_ID_LEN)).ToString
                    If Mid(Trim(lamp_id_start.Text) & m_lampid, 5, 2) = "31" Then  '主控节点
                        Me.Hand_control_recordTableAdapter.FillBy_find_time(Me.Hand_record.hand_control_record, "主控节点", lamp_id_tag, time_start, time_end)

                    Else
                        Me.Hand_control_recordTableAdapter.FillBy_find_time(Me.Hand_record.hand_control_record, "单灯", lamp_id_tag, time_start, time_end)

                    End If
                End If

            Else
                '时段控制
                If rb_lamp_type_control.Checked = True Then
                    '选择类型+日期
                    Me.Hand_control_recordTableAdapter.FillBy_find_time(Me.Hand_record.hand_control_record, "时段类型", m_controlboxname & m_lamptype, time_start, time_end)

                End If

                If rb_lamp_id_control.Checked = True And rb_date_condition_find.Checked = True Then
                    '选择灯的编号+日期
                    lamp_id_tag = Val(Mid(Trim(lamp_id_start.Text) & m_lampid, 1, 4)).ToString & "-" & Val(Mid(Trim(lamp_id_start.Text) & m_lampid, 5, 2)).ToString & "-" & Val(Mid(Trim(lamp_id_start.Text) & m_lampid, 7, LAMP_ID_LEN)).ToString
                    If Mid(Trim(lamp_id_start.Text) & m_lampid, 5, 2) = "31" Then  '主控节点
                        Me.Hand_control_recordTableAdapter.FillBy_find_time(Me.Hand_record.hand_control_record, "时段节点", lamp_id_tag, time_start, time_end)

                    Else
                        Me.Hand_control_recordTableAdapter.FillBy_find_time(Me.Hand_record.hand_control_record, "时段单灯", lamp_id_tag, time_start, time_end)

                    End If
                End If

            End If


        End If
        If rb_no_date_condition_find.Checked = True Then '不按日期查询
            If rb_hand_check.Checked = True Then
                If rb_city_control.Checked = True Then
                    '选择城市

                    Me.Hand_control_recordTableAdapter.FillBy_find(Me.Hand_record.hand_control_record, "城市", m_cityname)

                End If
                If rb_area_control.Checked = True Then
                    '选择区域

                    Me.Hand_control_recordTableAdapter.FillBy_find(Me.Hand_record.hand_control_record, "区域", m_areaname)

                End If
                If rb_street_control.Checked = True Then
                    '选择街道
                    Me.Hand_control_recordTableAdapter.FillBy_find(Me.Hand_record.hand_control_record, "街道", m_streetname)

                End If
                If rb_control_box_name_control.Checked = True And rb_no_date_condition_find.Checked = True Then
                    '选择电控箱
                    Me.Hand_control_recordTableAdapter.FillBy_find(Me.Hand_record.hand_control_record, "主控箱", m_controlboxname)
                End If

                If rb_lamp_type_control.Checked = True And rb_no_date_condition_find.Checked = True Then
                    '选择类型
                    Me.Hand_control_recordTableAdapter.FillBy_find(Me.Hand_record.hand_control_record, "类型", m_controlboxname & " " & m_lamptype)

                End If

                If rb_lamp_id_control.Checked = True And rb_no_date_condition_find.Checked = True Then
                    '选择灯的编号
                    lamp_id_tag = Val(Mid(Trim(lamp_id_start.Text) & m_lampid, 1, 4)).ToString & "-" & Val(Mid(Trim(lamp_id_start.Text) & m_lampid, 5, 2)).ToString & "-" & Val(Mid(Trim(lamp_id_start.Text) & m_lampid, 7, LAMP_ID_LEN)).ToString
                    If Mid(Trim(lamp_id_start.Text) & m_lampid, 5, 2) = "31" Then  '主控节点
                        Me.Hand_control_recordTableAdapter.FillBy_find(Me.Hand_record.hand_control_record, "主控节点", lamp_id_tag)
                    Else
                        Me.Hand_control_recordTableAdapter.FillBy_find(Me.Hand_record.hand_control_record, "单灯", lamp_id_tag)

                    End If
                End If

            Else
                If rb_lamp_type_control.Checked = True And rb_no_date_condition_find.Checked = True Then
                    '选择类型
                    Me.Hand_control_recordTableAdapter.FillBy_find(Me.Hand_record.hand_control_record, "时段类型", m_controlboxname & m_lamptype)

                End If

                If rb_lamp_id_control.Checked = True And rb_no_date_condition_find.Checked = True Then
                    '选择灯的编号
                    lamp_id_tag = Val(Mid(Trim(lamp_id_start.Text) & m_lampid, 1, 4)).ToString & "-" & Val(Mid(Trim(lamp_id_start.Text) & m_lampid, 5, 2)).ToString & "-" & Val(Mid(Trim(lamp_id_start.Text) & m_lampid, 7, LAMP_ID_LEN)).ToString
                    If Mid(Trim(lamp_id_start.Text) & m_lampid, 5, 2) = "31" Then  '主控节点
                        Me.Hand_control_recordTableAdapter.FillBy_find(Me.Hand_record.hand_control_record, "时段节点", lamp_id_tag)
                    Else
                        Me.Hand_control_recordTableAdapter.FillBy_find(Me.Hand_record.hand_control_record, "时段单灯", lamp_id_tag)

                    End If
                End If
            End If


        End If

        record_num.Text = "共有" & dgv_hand_control_list.RowCount.ToString & "条控制记录，" & dgv_operation_record.RowCount.ToString & "条操作记录"  '提示记录条数


    End Sub

    ''' <summary>
    ''' 导出excel线程的主函数
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Me.BackgroundWorker1.ReportProgress(1)  '提示开始excel操作
        Try
            If m_excel_kind = 0 Then
                excel_control()  '导出控制系统excel函数
            Else
                excel_operation()  '导出操作excel
            End If

        Catch ex As Exception
            ex.ToString()
        End Try

    End Sub

    ''' <summary>
    ''' 完成导出excel表
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BackgroundWorker1_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        m_xlApp.Visible = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        progress.Visible = False
        g_welcomewinobj.circle_string.Text = "完成系统日志的导出"
        record_num.Text = "系统日志"

    End Sub

    ''' <summary>
    ''' 导出进度显示
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BackgroundWorker1_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker1.ProgressChanged
        If e.ProgressPercentage = 1 And m_stringtag = 0 Then
            g_welcomewinobj.circle_string.Text = "导出系统日志"
            record_num.Text = "导出系统日志"
            m_stringtag = 1
        End If
        If e.ProgressPercentage >= 2 Then
            progress.Value = e.ProgressPercentage
        End If

    End Sub

    ''' <summary>
    ''' 关闭窗体时，停止线程
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 手工控制记录_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If Me.BackgroundWorker1.IsBusy = True Then
            Me.BackgroundWorker1.CancelAsync()
        End If
        ProcessKill(m_xlApp, m_xlBook, m_xlSheet)
        g_windowclose = 1
    End Sub

    ''' <summary>
    ''' 按日期查询，使开始和结束日期可用
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rb_date_condition_find_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_date_condition_find.Click
        If rb_date_condition_find.Checked = True Then  '日期查询
            dtp_date_start.Enabled = True '开始日期可用
            dtp_date_end.Enabled = True  '结束日期可用
        End If
    End Sub

    ''' <summary>
    ''' 不按日期查询，使开始和结束日期不可用
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rb_no_date_condition_find_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_no_date_condition_find.Click
        If rb_no_date_condition_find.Checked = True Then  '不用日期查询
            dtp_date_start.Enabled = False '开始日期不可用
            dtp_date_end.Enabled = False  '结束日期不可用
        End If
    End Sub

    ''' <summary>
    ''' 按区域名称查询，使类型和编号下拉框不可用，区域类型下拉框可用
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rb_control_box_name_control_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_control_box_name_control.Click
        If rb_control_box_name_control.Checked = True Then
            cb_city_name.Enabled = True
            cb_area_name.Enabled = True
            cb_street_name.Enabled = True
            cb_control_box_name.Enabled = True
            cb_lamp_type.Enabled = False
            cb_lamp_id.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' 按类型查询使编号下拉框不可用，区域，类型下拉框可用
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rb_lamp_type_control_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_lamp_type_control.Click
        If rb_lamp_type_control.Checked = True Then
            cb_city_name.Enabled = True
            cb_area_name.Enabled = True
            cb_street_name.Enabled = True
            cb_control_box_name.Enabled = True
            cb_lamp_type.Enabled = True
            cb_lamp_id.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' 按灯的编号查询，使区域，类型。编号可用
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rb_lamp_id_control_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_lamp_id_control.Click
        If rb_lamp_id_control.Checked = True Then
            cb_city_name.Enabled = True
            cb_area_name.Enabled = True
            cb_street_name.Enabled = True
            cb_control_box_name.Enabled = True
            cb_lamp_type.Enabled = True
            cb_lamp_id.Enabled = True
        End If
    End Sub

    ''' <summary>
    ''' 选择区域名称
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_control_box_name_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_control_box_name.DropDown
        'Com_inf.Select_box_name(control_box_name)
        Com_inf.Select_box_name_level(cb_city_name, cb_area_name, cb_street_name, cb_control_box_name)
    End Sub

    ''' <summary>
    ''' 区域名称改变，类型和编号也跟着改变
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_control_box_name_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_control_box_name.SelectedIndexChanged
        Com_inf.Select_type_name(cb_control_box_name, cb_lamp_type, lamp_type_id)
        Com_inf.Select_lamp_id_type(cb_control_box_name, cb_lamp_type, cb_lamp_id, lamp_id_start)
    End Sub

    ''' <summary>
    ''' 选择类型
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_lamp_type_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_lamp_type.DropDown
        Com_inf.Select_type_name(cb_control_box_name, cb_lamp_type, lamp_type_id)
    End Sub

    ''' <summary>
    ''' 类型改变时，编号改变
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_lamp_type_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_lamp_type.SelectedIndexChanged
        Com_inf.Select_lamp_id_type(cb_control_box_name, cb_lamp_type, cb_lamp_id, lamp_id_start)

    End Sub

    ''' <summary>
    ''' 选择编号
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_lamp_id_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_lamp_id.DropDown
        Com_inf.Select_lamp_id_type(cb_control_box_name, cb_lamp_type, cb_lamp_id, lamp_id_start)
    End Sub

    ''' <summary>
    ''' 导出EXCEL报表
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_excel_table_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_excel_table.Click
        m_stringtag = 0
        m_excel_kind = 0
        If Me.BackgroundWorker1.IsBusy Then
            MsgBox("导出线程正在操作，请稍后重试...", , PROJECT_TITLE_STRING)
            Exit Sub
        Else
            progress.Visible = True
            Me.BackgroundWorker1.RunWorkerAsync()
        End If

    End Sub

    ''' <summary>
    ''' 刷新，载入所有的数据
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_getall_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_getall.Click
        Me.Hand_control_recordTableAdapter.Fill(Me.Hand_record.hand_control_record)  '载入手工控制列表

        record_num.Text = "共有" & dgv_hand_control_list.RowCount.ToString & "条控制记录，" & dgv_operation_record.RowCount.ToString & "条操作记录"  '提示记录条数
    End Sub

    ''' <summary>
    ''' 按城市进行查询
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rb_city_control_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_city_control.Click
        If rb_city_control.Checked = True Then
            cb_city_name.Enabled = True
            cb_area_name.Enabled = False
            cb_street_name.Enabled = False
            cb_control_box_name.Enabled = False
            cb_lamp_type.Enabled = False
            cb_lamp_id.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' 按区域进行查询
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rb_area_control_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_area_control.Click
        If rb_area_control.Checked = True Then
            cb_city_name.Enabled = True
            cb_area_name.Enabled = True
            cb_street_name.Enabled = False
            cb_control_box_name.Enabled = False
            cb_lamp_type.Enabled = False
            cb_lamp_id.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' 按街道进行查询
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rb_street_control_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_street_control.Click
        If rb_street_control.Checked = True Then
            cb_city_name.Enabled = True
            cb_area_name.Enabled = True
            cb_street_name.Enabled = True
            cb_control_box_name.Enabled = False
            cb_lamp_type.Enabled = False
            cb_lamp_id.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' 增加城市名称
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_city_name_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_city_name.DropDown
        Com_inf.Select_city_name(cb_city_name)
    End Sub

    ''' <summary>
    ''' 城市名称改变
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_city_name_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_city_name.SelectedIndexChanged
        Com_inf.Select_area_name(cb_city_name, cb_area_name)
        Com_inf.Select_street_name(cb_city_name, cb_area_name, cb_street_name)
        Com_inf.Select_box_name_level(cb_city_name, cb_area_name, cb_street_name, cb_control_box_name)
        Com_inf.Select_type_name(cb_control_box_name, cb_lamp_type, lamp_type_id)
        Com_inf.Select_lamp_id_type(cb_control_box_name, cb_lamp_type, cb_lamp_id, lamp_id_start)
    End Sub

    ''' <summary>
    ''' 增加区域名称
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_area_name_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_area_name.DropDown
        Com_inf.Select_area_name(cb_city_name, cb_area_name)
    End Sub

    ''' <summary>
    ''' 区域名称改变
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_area_name_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_area_name.SelectedIndexChanged
        Com_inf.Select_street_name(cb_city_name, cb_area_name, cb_street_name)
        Com_inf.Select_box_name_level(cb_city_name, cb_area_name, cb_street_name, cb_control_box_name)
        Com_inf.Select_type_name(cb_control_box_name, cb_lamp_type, lamp_type_id)
        Com_inf.Select_lamp_id_type(cb_control_box_name, cb_lamp_type, cb_lamp_id, lamp_id_start)
    End Sub

    ''' <summary>
    ''' 增加街道名称
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_street_name_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_street_name.DropDown
        Com_inf.Select_street_name(cb_city_name, cb_area_name, cb_street_name)
    End Sub

    ''' <summary>
    ''' 街道名称改变
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_street_name_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_street_name.SelectedIndexChanged
        Com_inf.Select_box_name_level(cb_city_name, cb_area_name, cb_street_name, cb_control_box_name)
        Com_inf.Select_type_name(cb_control_box_name, cb_lamp_type, lamp_type_id)
        Com_inf.Select_lamp_id_type(cb_control_box_name, cb_lamp_type, cb_lamp_id, lamp_id_start)
    End Sub

    ''' <summary>
    ''' 选择按用户名称查询
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rb_check_username_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_check_username.Click
        If rb_check_username.Checked = True Then
            cb_usernamelist.Enabled = True
        End If
    End Sub

    ''' <summary>
    ''' 选择不按用户名称查询
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rb_check_nousername_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_check_nousername.Click
        If rb_check_nousername.Checked = True Then
            cb_usernamelist.Text = ""
            cb_usernamelist.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' 点击查询按钮，查询操作日志
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_operation_check_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_operation_check.Click
        If rb_check_username.Checked = True Then
            '按用户名称查询
            If Trim(cb_usernamelist.Text) = "" Then
                MsgBox("请选择用户名", , PROJECT_TITLE_STRING)
                cb_usernamelist.Focus()
                Exit Sub
            End If
            If dtp_operation_starttime.Value.ToString = "" Then
                MsgBox("请选择查询开始时间", , PROJECT_TITLE_STRING)
                dtp_operation_starttime.Focus()
                Exit Sub
            End If
            If dtp_operation_endtime.Value.ToString = "" Then
                MsgBox("请选择查询结束时间", , PROJECT_TITLE_STRING)
                dtp_operation_endtime.Focus()
                Exit Sub
            End If
            Me.Operation_recordTableAdapter.FillBy_time_username(Me.Operation_list.operation_record, dtp_operation_starttime.Value, dtp_operation_endtime.Value, Trim(cb_usernamelist.Text))

        Else
            '不按用户名称查询
            If dtp_operation_starttime.Value.ToString = "" Then
                MsgBox("请选择查询开始时间", , PROJECT_TITLE_STRING)
                dtp_operation_starttime.Focus()
                Exit Sub
            End If
            If dtp_operation_endtime.Value.ToString = "" Then
                MsgBox("请选择查询结束时间", , PROJECT_TITLE_STRING)
                dtp_operation_endtime.Focus()
                Exit Sub
            End If
            Me.Operation_recordTableAdapter.FillBy_time_nouser(Me.Operation_list.operation_record, dtp_operation_starttime.Value, dtp_operation_endtime.Value)

        End If
        record_num.Text = "共有" & dgv_hand_control_list.RowCount.ToString & "条控制记录，" & dgv_operation_record.RowCount.ToString & "条操作记录"  '提示记录条数


    End Sub

    ''' <summary>
    ''' 刷新按钮，查询所有操作
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_operation_all_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_operation_all.Click
        'TODO: 这行代码将数据加载到表“Operation_list.operation_record”中。您可以根据需要移动或移除它。
        Me.Operation_recordTableAdapter.Fill(Me.Operation_list.operation_record)

    End Sub

    ''' <summary>
    ''' 将查询结果导成excel表
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_operation_excel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_operation_excel.Click
        m_excel_kind = 1
        m_stringtag = 0
        If Me.BackgroundWorker1.IsBusy = False Then
            progress.Visible = True
            Me.BackgroundWorker1.RunWorkerAsync()
        Else
            MsgBox("导出线程正在操作，请稍后重试...", , PROJECT_TITLE_STRING)
        End If
    End Sub

    ''' <summary>
    ''' 选择用户名
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_usernamelist_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_usernamelist.DropDown
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String

        msg = ""
        sql = "select name from manage"
        DBOperation.OpenConn(conn)
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "usernamelist_DropDown", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        cb_usernamelist.Items.Clear()
        While rs.EOF = False
            cb_usernamelist.Items.Add(Trim(rs.Fields("name").Value))
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
    ''' 按手工控制记录查询
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rb_hand_check_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_hand_check.Click
        If rb_hand_check.Checked = True Then
            rb_city_control.Enabled = True
            rb_area_control.Enabled = True
            rb_street_control.Enabled = True
            rb_control_box_name_control.Enabled = True
            rb_lamp_type_control.Enabled = True
            rb_lamp_id_control.Enabled = True
        End If
    End Sub

    ''' <summary>
    ''' 按时控记录查询
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rb_divtime_check_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_divtime_check.Click
        If rb_divtime_check.Checked = True Then
            rb_city_control.Enabled = False
            rb_area_control.Enabled = False
            rb_street_control.Enabled = False
            rb_control_box_name_control.Enabled = False
            rb_lamp_type_control.Enabled = True
            rb_lamp_id_control.Enabled = True
            rb_lamp_type_control.Checked = True
            cb_lamp_id.Enabled = False
        End If
    End Sub
End Class