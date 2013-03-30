''' <summary>
''' 统计一个时间区间内主控箱的运行状态
''' </summary>
''' <remarks></remarks>
Public Class 电控箱数据统计

    Private m_timestart, m_timeend As System.DateTime  '查询时间段的开始与结束时间
    Private m_controlboxnamelist As New ArrayList   '查询的电控箱名称
    Private m_statetag As Integer  '0表示暗，1表示亮
    Private m_exceltable As Integer  '0表示查询，1表示导出excel表
    Private m_xlApp As Microsoft.Office.Interop.Excel.Application

    Private m_xlBook As Microsoft.Office.Interop.Excel.Workbook
    Private m_xlSheet As Microsoft.Office.Interop.Excel.Worksheet
    Private m_row As Integer  'excel表的行数
    Private m_stringtag As Integer   '提示信息只出现一次
    Private m_id As Integer   '编号
    Private m_statestring As String  '查询的条件：正常等
    Private m_checktype As Integer '记录查询的类型，1表示查询三遥数据，2表示查询通信状态，3表示查询供电情况，4表示查询开关量
    Private m_small_sanyao As Boolean = False   '记录是否为小三遥

    '定义的委托，用于设置文本或者是获取文本内容
    Delegate Sub SetTextControlCallBack(ByVal text As String, ByVal control As Control)

    ''' <summary>
    ''' 委托，在线程中使用文本框
    ''' </summary>
    ''' <param name="text"></param>
    ''' <param name="control"></param>
    ''' <remarks></remarks>
    Public Sub SetTextDelegate(ByVal text As String, ByVal control As Control)
        '如果调用方位于创建该控件线程以外的线程时，需要获取是否要调用Invoke方法
        If (control.InvokeRequired) Then
            Dim SetText As SetTextControlCallBack = New SetTextControlCallBack(AddressOf SetTextDelegate)
            Me.Invoke(SetText, New Object() {text, control})

        Else '如果不是线程外的调用时：设置文本框的值
            If m_statetag = 1 Then
                rtb_doingnow_text.SelectionStart = rtb_doingnow_text.TextLength
                rtb_doingnow_text.SelectionColor = Color.Black    ' 亮的
            End If
            If m_statetag = 2 Then
                rtb_doingnow_text.SelectionStart = rtb_doingnow_text.TextLength
                rtb_doingnow_text.SelectionColor = Color.Orange      '暗的
            End If
            If m_statetag = 3 Then
                rtb_doingnow_text.SelectionStart = rtb_doingnow_text.TextLength
                rtb_doingnow_text.SelectionColor = Color.Green       '无返回值的
            End If
            If m_statetag = 4 Then
                rtb_doingnow_text.SelectionStart = rtb_doingnow_text.TextLength
                rtb_doingnow_text.SelectionColor = Color.Red   '故障

            End If

            Me.rtb_doingnow_text.AppendText(text)
            control.Refresh()

        End If

    End Sub

    ''' <summary>
    ''' 选择区域名称
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_control_box_name_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_control_box_name.DropDown
        Com_inf.Select_box_name_level(cb_city_name, cb_area_name, cb_street_name, cb_control_box_name)

    End Sub

    ''' <summary>
    ''' 查询路灯的亮暗记录
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_find_record_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_find_record.Click
        If Me.rb_sanyao_statecheck.Checked = False And Me.rb_sanyao_datacheck.Checked = False And Me.rb_communicationcheck.Checked = False And Me.rb_kaiguancheck.Checked = False And Me.rb_powercheck.Checked = False And rb_total_state.Checked = False Then
            MsgBox("请选择查询类型", , PROJECT_TITLE_STRING)
            Me.rb_sanyao_datacheck.Focus()
            Exit Sub
        End If
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
        If Trim(dtp_date_start.Text) = Trim(dtp_date_end.Text) Then  '开始时间不应该和结束时间相同
            MsgBox("统计的开始时间与结束时间相同，请选择一个时间区间", , PROJECT_TITLE_STRING)
            dtp_date_start.Focus()
            Exit Sub
        End If

        If dtp_date_start.Value > dtp_date_end.Value Then
            MsgBox("开始时间大于结束时间，请重新选择一个时间区间", , PROJECT_TITLE_STRING)
            dtp_date_start.Focus()
            Exit Sub

        End If
        If Me.rb_kaiguancheck.Checked = False And rb_sanyao_datacheck.Checked = False And rb_total_state.Checked = False Then
            If cb_state_list.Text = "" Then
                MsgBox("请选择数据状态", , PROJECT_TITLE_STRING)
                cb_state_list.Focus()
                Exit Sub
            End If
        End If

        m_exceltable = 0
        ProgressBar.Visible = True
        m_timestart = dtp_date_start.Value  '开始日期
        m_timeend = dtp_date_end.Value  '结束日期
        m_stringtag = 0  '首次查询标志

        ' m_controlboxname = Trim(cb_control_box_name.Text)  '主控箱的名称
        m_statestring = Trim(cb_state_list.Text) '选择状态
        If rb_city_control.Checked = True Then
            If Trim(cb_city_name.Text = "") Then
                MsgBox("请选择城市名称", , PROJECT_TITLE_STRING)
                Exit Sub
            End If
            get_controlboxname(1, Trim(cb_city_name.Text))
        End If

        If rb_area_control.Checked = True Then
            If Trim(cb_area_name.Text = "") Then
                MsgBox("请选择区域名称", , PROJECT_TITLE_STRING)
                Exit Sub
            End If
            get_controlboxname(2, Trim(cb_area_name.Text))
        End If

        If rb_street_control.Checked = True Then
            If Trim(cb_street_name.Text = "") Then
                MsgBox("请选择街道名称", , PROJECT_TITLE_STRING)
                Exit Sub
            End If
            get_controlboxname(3, Trim(cb_street_name.Text))
        End If

        If rb_control_box_name_control.Checked = True Then
            If Trim(cb_control_box_name.Text = "") Then
                MsgBox("请选择主控箱名称", , PROJECT_TITLE_STRING)
                Exit Sub
            End If
            get_controlboxname(4, Trim(cb_control_box_name.Text))
        End If

        '如果没有主控箱，进行提示
        If m_controlboxnamelist.Count = 0 Then
            MsgBox("请选择主控箱", , PROJECT_TITLE_STRING)
            Exit Sub
        End If

        If Me.BackgroundWorker_on_off.IsBusy = False Then
            Me.BackgroundWorker_on_off.RunWorkerAsync()
        Else
            MsgBox("线程正忙，请稍后重试", , PROJECT_TITLE_STRING)
        End If

    End Sub

    ''' <summary>
    ''' 获取所选择的主控箱的名称
    ''' </summary>
    ''' <param name="choose_tag" >1表示按城市，2表示按区域，3表示按街道，4表示按主控箱</param>
    ''' <remarks></remarks>
    Private Sub get_controlboxname(ByVal choose_tag As Integer, ByVal name As String)
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String

        msg = ""
        sql = ""
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If

        If choose_tag = 1 Then  '城市
            sql = "SELECT city.city, area.area, street.street, control_box.control_box_name FROM  city INNER JOIN " _
            & "area ON city.ID = area.city_id INNER JOIN street ON area.id = street.area_id INNER JOIN " _
             & "control_box ON street.street_id = control_box.street_id where city='" & name & "'"

        End If
        If choose_tag = 2 Then '区域
            sql = "SELECT city.city, area.area, street.street, control_box.control_box_name FROM  city INNER JOIN " _
            & "area ON city.ID = area.city_id INNER JOIN street ON area.id = street.area_id INNER JOIN " _
             & "control_box ON street.street_id = control_box.street_id where area='" & name & "'"

        End If
        If choose_tag = 3 Then  '街道
            sql = "SELECT city.city, area.area, street.street, control_box.control_box_name FROM  city INNER JOIN " _
            & "area ON city.ID = area.city_id INNER JOIN street ON area.id = street.area_id INNER JOIN " _
             & "control_box ON street.street_id = control_box.street_id where street='" & name & "'"

        End If
        If choose_tag = 4 Then  '电控箱
            sql = "SELECT city.city, area.area, street.street, control_box.control_box_name FROM  city INNER JOIN " _
            & "area ON city.ID = area.city_id INNER JOIN street ON area.id = street.area_id INNER JOIN " _
             & "control_box ON street.street_id = control_box.street_id where control_box_name='" & name & "'"

        End If

        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If m_controlboxnamelist.Count > 0 Then
            m_controlboxnamelist.Clear()
        End If
        While rs.EOF = False
            m_controlboxnamelist.Add(Trim(rs.Fields("control_box_name").Value))
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
    ''' 清空文本框
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_clear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_clear.Click
        rtb_doingnow_text.Text = ""
    End Sub

    ''' <summary>
    ''' 查询状态线程的主函数
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BackgroundWorker_on_off_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker_on_off.DoWork
        Dim controlboxname As String
        Dim i As Integer = 0

      
        If m_checktype = 1 Then
            '查询三遥数据 
            m_statestring = "全部"
            If m_exceltable = 0 Then
                While i < m_controlboxnamelist.Count
                    controlboxname = m_controlboxnamelist(i)
                    sanyao_state(m_statestring, controlboxname)  '查询
                    i += 1
                End While
               
            Else
                If m_exceltable = 1 Then
                    sanyao_date_excel()   'excel表
                End If

            End If
        End If

        If m_checktype = 2 Then
            '查询通信状态
            If m_exceltable = 0 Then
                While i < m_controlboxnamelist.Count
                    controlboxname = m_controlboxnamelist(i)
                    communication_state(m_statestring, controlboxname)  '查询
                    i += 1
                End While

            Else
                communication_excel()
            End If
        End If

        If m_checktype = 3 Then
            '查询供电情况
            If m_exceltable = 0 Then
                While i < m_controlboxnamelist.Count
                    controlboxname = m_controlboxnamelist(i)
                    power_type_state(m_statestring, controlboxname)  '查询
                    i += 1
                End While

            Else
                power_type_excel()
            End If
        End If

        If m_checktype = 4 Then
            '查询开关量报警情况
            If m_exceltable = 0 Then
                While i < m_controlboxnamelist.Count
                    controlboxname = m_controlboxnamelist(i)
                    kaiguanliang_state(controlboxname)  '查询
                    i += 1
                End While

            Else
                kaiguan_excel()
            End If
        End If

        If m_checktype = 5 Then
            '查询三遥运行状态，包括电压，电流，电功率
            If m_exceltable = 0 Then

                While i < m_controlboxnamelist.Count
                    controlboxname = m_controlboxnamelist(i)
                    sanyao_state_state(m_statestring, controlboxname)  '查询
                    i += 1
                End While

            Else
                sanyao_state_excel()
            End If
        End If


        If m_checktype = 6 Then
            '查询所有报警状态
            If m_exceltable = 0 Then

                While i < m_controlboxnamelist.Count
                    controlboxname = m_controlboxnamelist(i)
                    sanyao_total_state(controlboxname)  '查询
                    i += 1
                End While

            Else
                sanyao_total_state_excel()
            End If

        End If


    End Sub

    ''' <summary>
    ''' 查询主控箱的综合数据
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub sanyao_total_state(ByVal controlboxname As String)
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim statetype As String = "数据"
        Dim statestring1 As String = "交流电"
        Dim statestring2 As String = "正常"
        Dim statestring3 As String = "通信正常"
        Dim num As Integer = 0
        Dim progress_value As Integer = 1
        Dim state As String
        Dim starttime, endtime As String '状态的开始和结束时间


        msg = ""
        DBOperation.OpenConn(conn)
        sql = "select * from control_box_state where (createtime>='" & dtp_date_start.Value & "' and " _
         & "createtime<='" & dtp_date_end.Value & "' or (createtime<'" & dtp_date_start.Value & "' and " _
         & "(StatusContent2 is NULL or convert(Datetime,StatusContent2) >'" & dtp_date_start.Value & "')))" _
         & "and control_box_name='" & controlboxname & "' and kaiguan_string<>'" & statetype & "'  and " _
         & "createtime<>convert(Datetime,StatusContent2) and statuscontent<>'" & statestring1 & "' and statuscontent<>'" & statestring2 & "' and statuscontent<>'" & statestring3 & "' order by ID"


        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "sanyao_total_state", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        While rs.EOF = False
            If Me.BackgroundWorker_on_off.CancellationPending = True Then
                GoTo finish
            End If
            '显示进度
            num = 100 * progress_value / rs.RecordCount
            If num > 100 Then
                num = 100
            End If
            Me.BackgroundWorker_on_off.ReportProgress(num)
            progress_value += 1
            If rs.Fields("StatusContent").Value Is System.DBNull.Value Then
                state = ""
            Else
                state = Trim(rs.Fields("StatusContent").Value)  '取主控箱的工作状态
                If state = "电池" Then
                    state = "失压"
                End If
                If state = "正常" Then  '交流电
                    m_statetag = 1
                Else  '电池供电
                    m_statetag = 4
                End If
            End If
            starttime = rs.Fields("Createtime").Value.ToString
            '状态的结束时间
            If rs.Fields("StatusContent2").Value Is System.DBNull.Value Then
                If rs.Fields("state").Value = "1" Then
                    rs.MoveNext()
                    Continue While

                End If
                endtime = dtp_date_end.Value.ToString
            Else
                endtime = Trim(rs.Fields("StatusContent2").Value)
            End If

            If m_exceltable = 0 Then

                Me.SetTextDelegate("主控箱名称：" & controlboxname & "  时间：" & starttime & "至" & endtime & vbCrLf, Me.rtb_doingnow_text)
                Me.SetTextDelegate("运行情况：" & state & vbCrLf, Me.rtb_doingnow_text)
                Me.SetTextDelegate("" & vbCrLf, Me.rtb_doingnow_text)
            Else  '导出报表
                m_xlApp.Cells(m_row, 1) = "'" & m_id
                m_xlApp.Cells(m_row, 2) = controlboxname
                m_xlApp.Cells(m_row, 3) = starttime
                m_xlApp.Cells(m_row, 4) = endtime
                m_xlApp.Cells(m_row, 5) = "'" & state

                m_row += 1
                m_id += 1

            End If

            rs.MoveNext()
        End While

finish:
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    ''' 查询主控箱电压，电流，功率等状态是否正常的历史数据
    ''' </summary>
    ''' <param name="check_state">三种状态，交流电，电池，全部</param>
    ''' <remarks></remarks>
    Private Sub sanyao_state_state(ByVal check_state As String, ByVal controlboxname As String)
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim statetype As String = "状态"
        Dim num As Integer = 0
        Dim progress_value As Integer = 1
        Dim state As String
        Dim starttime, endtime As String '状态的开始和结束时间


        msg = ""
        DBOperation.OpenConn(conn)
        If check_state = "全部" Then
            ' sql = "select * from control_box_state where (createtime>='" & dtp_date_start.Value & "' and " _
            '& "createtime<='" & dtp_date_end.Value & "' or (createtime<'" & dtp_date_start.Value & "' and " _
            '& "(StatusContent2 is NULL or convert(Datetime,StatusContent2) >'" & dtp_date_start.Value & "')))" _
            '& "and control_box_name='" & controlboxname & "' and kaiguan_string='" & statetype & "'  and createtime<>convert(Datetime,StatusContent2) order by createtime"
            sql = "select * from control_box_state where (createtime>='" & dtp_date_start.Value & "' and " _
           & "createtime<='" & dtp_date_end.Value & "' or (createtime<'" & dtp_date_start.Value & "' and " _
           & "(StatusContent2 is NULL or convert(Datetime,StatusContent2) >'" & dtp_date_start.Value & "')))" _
           & "and control_box_name='" & controlboxname & "' and kaiguan_string='" & statetype & "' order by createtime"


        Else
            If check_state = "正常" Then
                '                sql = "select * from control_box_state where (createtime>='" & dtp_date_start.Value & "' and " _
                '& "createtime<='" & dtp_date_end.Value & "' or (createtime<'" & dtp_date_start.Value & "' and " _
                '& "(StatusContent2 is NULL or convert(Datetime,StatusContent2) >'" & dtp_date_start.Value & "')))" _
                '& "and control_box_name='" & controlboxname & "' and kaiguan_string='" & statetype & "' and StatusContent='" & check_state & "' and createtime<>convert(Datetime,StatusContent2) order by createtime"
                sql = "select * from control_box_state where (createtime>='" & dtp_date_start.Value & "' and " _
               & "createtime<='" & dtp_date_end.Value & "' or (createtime<'" & dtp_date_start.Value & "' and " _
               & "(StatusContent2 is NULL or convert(Datetime,StatusContent2) >'" & dtp_date_start.Value & "')))" _
               & "and control_box_name='" & controlboxname & "' and kaiguan_string='" & statetype & "' and StatusContent='" & check_state & "' order by createtime"


                ' sql = "select * from control_box_state where createtime>='" & Date_start.Value & "' and createtime<='" & Date_end.Value & "' and control_box_name='" & m_controlboxname & "' and StatusContent='" & check_state & "' and kaiguan_string='" & statetype & "' order by createtime"
            Else
                '   sql = "select * from control_box_state where (createtime>='" & dtp_date_start.Value & "' and " _
                '& "createtime<='" & dtp_date_end.Value & "' or (createtime<'" & dtp_date_start.Value & "' and " _
                '& "(StatusContent2 is NULL or convert(Datetime,StatusContent2) >'" & dtp_date_start.Value & "')))" _
                '& "and control_box_name='" & controlboxname & "' and kaiguan_string='" & statetype & "' and StatusContent<>'正常' and createtime<>convert(Datetime,StatusContent2) order by createtime"
                sql = "select * from control_box_state where (createtime>='" & dtp_date_start.Value & "' and " _
               & "createtime<='" & dtp_date_end.Value & "' or (createtime<'" & dtp_date_start.Value & "' and " _
               & "(StatusContent2 is NULL or convert(Datetime,StatusContent2) >'" & dtp_date_start.Value & "')))" _
               & "and control_box_name='" & controlboxname & "' and kaiguan_string='" & statetype & "' and StatusContent<>'正常' order by createtime"


                '   sql = "select * from control_box_state where createtime>='" & Date_start.Value & "' and createtime<='" & Date_end.Value & "' and control_box_name='" & m_controlboxname & "' and StatusContent<>'正常' and kaiguan_string='" & statetype & "' order by createtime"

            End If
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "SanYaostate_state", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        While rs.EOF = False
            If Me.BackgroundWorker_on_off.CancellationPending = True Then
                GoTo finish
            End If
            '显示进度
            num = 100 * progress_value / rs.RecordCount
            If num > 100 Then
                num = 100
            End If
            Me.BackgroundWorker_on_off.ReportProgress(num)
            progress_value += 1
            If rs.Fields("StatusContent").Value Is System.DBNull.Value Then
                state = ""
            Else
                state = Trim(rs.Fields("StatusContent").Value)  '取主控箱的工作状态
                If state = "正常" Then  '交流电
                    m_statetag = 1
                Else  '电池供电
                    m_statetag = 4
                End If
            End If
            starttime = rs.Fields("Createtime").Value.ToString
            '如果状态是连续的，将状态的开始时间定为查找的时间
            If starttime < m_timestart Then
                starttime = m_timestart
            End If

            If rs.Fields("StatusContent2").Value Is System.DBNull.Value Then
                If rs.Fields("state").Value = "1" Then
                    rs.MoveNext()
                    Continue While

                End If
                endtime = dtp_date_end.Value.ToString
            Else
                endtime = Trim(rs.Fields("StatusContent2").Value)
            End If

            If m_exceltable = 0 Then

                Me.SetTextDelegate("主控箱名称：" & controlboxname & "  时间：" & starttime & "至" & endtime & vbCrLf, Me.rtb_doingnow_text)
                Me.SetTextDelegate("运行情况：" & state & vbCrLf, Me.rtb_doingnow_text)
                Me.SetTextDelegate("" & vbCrLf, Me.rtb_doingnow_text)
            Else  '导出报表
                m_xlApp.Cells(m_row, 1) = "'" & m_id
                m_xlApp.Cells(m_row, 2) = controlboxname
                m_xlApp.Cells(m_row, 3) = starttime
                m_xlApp.Cells(m_row, 4) = endtime
                m_xlApp.Cells(m_row, 5) = "'" & state

                m_row += 1
                m_id += 1

            End If

            rs.MoveNext()
        End While
finish:
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    ''' 查询开关量报警记录
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub kaiguanliang_state(ByVal controlboxname As String)
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim statetype As String = ""
        Dim num As Integer = 0
        Dim progress_value As Integer = 1
        Dim state As String
        Dim starttime, endtime As String '状态的开始和结束时间


        msg = ""
        DBOperation.OpenConn(conn)

        'sql = "select * from kaiguan_alarm_list where createtime>='" & Date_start.Value & "' and createtime<='" & Date_end.Value & "' and control_box_name='" & m_controlboxname & "' order by createtime"
        sql = "select * from kaiguan_alarm_list where (createtime>='" & dtp_date_start.Value & "' and " _
         & "createtime<='" & dtp_date_end.Value & "' or (createtime<'" & dtp_date_start.Value & "' and " _
         & "(endtime is NULL or endtime >'" & dtp_date_start.Value & "')))" _
         & "and control_box_name='" & controlboxname & "'  order by createtime"


        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "Kaiguan_state", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        While rs.EOF = False
            If Me.BackgroundWorker_on_off.CancellationPending = True Then
                GoTo finish
            End If
            '显示进度
            num = 100 * progress_value / rs.RecordCount
            If num > 100 Then
                num = 100
            End If
            Me.BackgroundWorker_on_off.ReportProgress(num)
            progress_value += 1
            If rs.Fields("alarm_string").Value Is System.DBNull.Value Then
                state = ""
            Else
                state = Trim(rs.Fields("alarm_string").Value)  '取主控箱的工作状态

            End If
            m_statetag = 4
            starttime = rs.Fields("createtime").Value.ToString
            '如果状态是连续的，将状态的开始时间定为查找的时间
            If starttime < m_timestart Then
                starttime = m_timestart
            End If

            If rs.Fields("endtime").Value Is System.DBNull.Value Then
                If rs.Fields("alarm_tag").Value = "1" Then
                    rs.MoveNext()
                    Continue While
                End If
                endtime = dtp_date_end.Value.ToString
            Else
                endtime = Trim(rs.Fields("endtime").Value.ToString)
            End If

            If m_exceltable = 0 Then

                Me.SetTextDelegate("主控箱名称：" & controlboxname & "  时间：" & starttime & "至" & endtime & vbCrLf, Me.rtb_doingnow_text)
                Me.SetTextDelegate("开关量报警信息：" & state & vbCrLf, Me.rtb_doingnow_text)
                Me.SetTextDelegate("" & vbCrLf, Me.rtb_doingnow_text)
            Else  '导出报表
                m_xlApp.Cells(m_row, 1) = "'" & m_id
                m_xlApp.Cells(m_row, 2) = controlboxname
                m_xlApp.Cells(m_row, 3) = starttime
                m_xlApp.Cells(m_row, 4) = endtime
                m_xlApp.Cells(m_row, 5) = "'" & state

                m_row += 1
                m_id += 1

            End If

            rs.MoveNext()
        End While
finish:
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub


    ''' <summary>
    ''' 查询主控箱的供电情况是否正常的历史数据
    ''' </summary>
    ''' <param name="check_state">三种状态，交流电，电池，全部</param>
    ''' <remarks></remarks>
    Private Sub power_type_state(ByVal check_state As String, ByVal controlboxname As String)
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim statetype As String = "供电"
        Dim num As Integer = 0
        Dim progress_value As Integer = 1
        Dim state As String
        Dim starttime, endtime As String '状态的开始和结束时间


        msg = ""
        DBOperation.OpenConn(conn)
        If check_state = "全部" Then
            sql = "select * from control_box_state where (createtime>='" & dtp_date_start.Value & "' and " _
   & "createtime<='" & dtp_date_end.Value & "' or (createtime<'" & dtp_date_start.Value & "' and " _
   & "(StatusContent2 is NULL or convert(Datetime,StatusContent2) >'" & dtp_date_start.Value & "')))" _
   & "and control_box_name='" & controlboxname & "' and kaiguan_string='" & statetype & "'  order by createtime"


            'sql = "select * from control_box_state where createtime>='" & Date_start.Value & "' and createtime<='" & Date_end.Value & "' and control_box_name='" & m_controlboxname & "' and kaiguan_string='" & statetype & "' order by createtime"
        Else
            sql = "select * from control_box_state where (createtime>='" & dtp_date_start.Value & "' and " _
   & "createtime<='" & dtp_date_end.Value & "' or (createtime<'" & dtp_date_start.Value & "' and " _
   & "(StatusContent2 is NULL or convert(Datetime,StatusContent2) >'" & dtp_date_start.Value & "')))" _
   & "and control_box_name='" & controlboxname & "' and kaiguan_string='" & statetype & "' and StatusContent='" & check_state & "' order by createtime"


            ' sql = "select * from control_box_state where createtime>='" & Date_start.Value & "' and createtime<='" & Date_end.Value & "' and control_box_name='" & m_controlboxname & "' and StatusContent='" & check_state & "' and kaiguan_string='" & statetype & "' order by createtime"
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "Powertype_state", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        While rs.EOF = False
            If Me.BackgroundWorker_on_off.CancellationPending = True Then
                GoTo finish
            End If
            '显示进度
            num = 100 * progress_value / rs.RecordCount
            If num > 100 Then
                num = 100
            End If
            Me.BackgroundWorker_on_off.ReportProgress(num)
            progress_value += 1
            If rs.Fields("StatusContent").Value Is System.DBNull.Value Then
                state = ""
            Else
                state = Trim(rs.Fields("StatusContent").Value)  '取主控箱的工作状态
                If state = POWERTYPE_CURRENT Then  '交流电
                    m_statetag = 1
                Else  '电池供电
                    m_statetag = 4
                End If
            End If
            starttime = rs.Fields("Createtime").Value.ToString
            '如果状态是连续的，将状态的开始时间定为查找的时间
            If starttime < m_timestart Then
                starttime = m_timestart
            End If

            If rs.Fields("StatusContent2").Value Is System.DBNull.Value Then
                If rs.Fields("state").Value = "1" Then
                    rs.MoveNext()
                    Continue While
                End If
                endtime = dtp_date_end.Value.ToString
            Else
                endtime = Trim(rs.Fields("StatusContent2").Value)
            End If

            If m_exceltable = 0 Then

                Me.SetTextDelegate("主控箱名称：" & controlboxname & "  时间：" & starttime & "至" & endtime & vbCrLf, Me.rtb_doingnow_text)
                Me.SetTextDelegate("供电情况：" & state & vbCrLf, Me.rtb_doingnow_text)
                Me.SetTextDelegate("" & vbCrLf, Me.rtb_doingnow_text)
            Else  '导出报表
                m_xlApp.Cells(m_row, 1) = "'" & m_id
                m_xlApp.Cells(m_row, 2) = controlboxname
                m_xlApp.Cells(m_row, 3) = starttime
                m_xlApp.Cells(m_row, 4) = endtime
                m_xlApp.Cells(m_row, 5) = "'" & state

                m_row += 1
                m_id += 1

            End If

            rs.MoveNext()
        End While
finish:
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    ''' 查询通信状态是否正常的历史数据
    ''' </summary>
    ''' <param name="check_state">三种状态，正常，不正常，全部</param>
    ''' <remarks></remarks>
    Private Sub communication_state(ByVal check_state As String, ByVal controlboxname As String)
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim statetype As String = "通信"
        Dim num As Integer = 0
        Dim progress_value As Integer = 1
        Dim state As String
        Dim starttime, endtime As String '状态的开始和结束时间


        msg = ""
        DBOperation.OpenConn(conn)
        If check_state = "全部" Then
            sql = "select * from control_box_state where (createtime>='" & dtp_date_start.Value & "' and " _
     & "createtime<='" & dtp_date_end.Value & "' or (createtime<'" & dtp_date_start.Value & "' and " _
     & "(StatusContent2 is NULL or convert(Datetime,StatusContent2) >'" & dtp_date_start.Value & "')))" _
     & "and control_box_name='" & controlboxname & "' and kaiguan_string='" & statetype & "'  order by createtime"


            'sql = "select * from control_box_state where createtime>='" & Date_start.Value & "' and createtime<='" & Date_end.Value & "' and control_box_name='" & m_controlboxname & "' and kaiguan_string='" & statetype & "' order by createtime"
        Else
            sql = "select * from control_box_state where (createtime>='" & dtp_date_start.Value & "' and " _
        & "createtime<='" & dtp_date_end.Value & "' or (createtime<'" & dtp_date_start.Value & "' and " _
        & "(StatusContent2 is NULL or convert(Datetime,StatusContent2) >'" & dtp_date_start.Value & "')))" _
        & "and control_box_name='" & controlboxname & "' and kaiguan_string='" & statetype & "' and StatusContent='" & check_state & "' order by createtime"


            'sql = "select * from control_box_state where createtime>='" & Date_start.Value & "' and createtime<='" & Date_end.Value & "' and control_box_name='" & m_controlboxname & "' and StatusContent='" & check_state & "' and kaiguan_string='" & statetype & "' order by createtime"
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "Communication_state", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        While rs.EOF = False
            If Me.BackgroundWorker_on_off.CancellationPending = True Then
                GoTo finish
            End If
            '显示进度
            num = 100 * progress_value / rs.RecordCount
            If num > 100 Then
                num = 100
            End If
            Me.BackgroundWorker_on_off.ReportProgress(num)
            progress_value += 1
            If rs.Fields("StatusContent").Value Is System.DBNull.Value Then
                state = ""
            Else
                state = Trim(rs.Fields("StatusContent").Value)  '取主控箱的工作状态
                If state = "通信正常" Then
                    m_statetag = 1
                Else
                    m_statetag = 4
                End If
            End If
            starttime = rs.Fields("Createtime").Value.ToString
            '如果状态是连续的，将状态的开始时间定为查找的时间
            If starttime < m_timestart Then
                starttime = m_timestart
            End If

            If rs.Fields("StatusContent2").Value Is System.DBNull.Value Then
                If rs.Fields("state").Value = "1" Then
                    rs.MoveNext()
                    Continue While
                End If
                endtime = dtp_date_end.Value.ToString
            Else
                endtime = Trim(rs.Fields("StatusContent2").Value)
            End If

            If m_exceltable = 0 Then

                Me.SetTextDelegate("主控箱名称：" & controlboxname & "  时间：" & starttime & "至" & endtime & vbCrLf, Me.rtb_doingnow_text)
                Me.SetTextDelegate("状态：" & state & vbCrLf, Me.rtb_doingnow_text)
                Me.SetTextDelegate("" & vbCrLf, Me.rtb_doingnow_text)
            Else  '导出报表
                m_xlApp.Cells(m_row, 1) = "'" & m_id
                m_xlApp.Cells(m_row, 2) = controlboxname
                m_xlApp.Cells(m_row, 3) = starttime
                m_xlApp.Cells(m_row, 4) = endtime
                m_xlApp.Cells(m_row, 5) = "'" & state

                m_row += 1
                m_id += 1

            End If

            rs.MoveNext()
        End While
finish:
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub


    ''' <summary>
    '''   '按电控箱进行路灯状态查询
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub sanyao_state(ByVal check_state As String, ByVal controlboxname As String)

        Dim rs_box As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim progress_value As Integer
        Dim num As Integer
        Dim state As String
        Dim controlboxobj As New control_box
        Dim statetype As String = "数据"
       

        Dim recData(20) As String '接收的数据
        Dim box_type As Integer '电控箱类型

        Dim i As Integer = 0
        Dim j As Integer = 0

        progress_value = 1
        msg = ""

        If check_state = "全部" Then
            sql = "select * from control_box_state where createtime>='" & dtp_date_start.Value & "' and createtime<='" & dtp_date_end.Value & "' and control_box_name='" & controlboxname & "' and kaiguan_string='" & statetype & "' order by createtime"
            'sql = "SELECT control_box_state.StatusContent, control_box.control_box_type,control_box.control_box_id, control_box_state.Createtime, control_box_state.control_box_name, control_box_state.state, " _
            '& "Control_box_state.StatusContent2, Control_box_state.StatusContent3, Control_box_state.power_type, Control_box_state.kaiguan_string " _
            '& "FROM  control_box INNER JOIN Control_box_state ON control_box.control_box_name = Control_box_state.control_box_name " _
            '& "where Control_box_state.Createtime>='" & dtp_date_start.Value & "' and Control_box_state.Createtime<='" & dtp_date_end.Value & "' and Control_box_state.control_box_name='" & m_controlboxname & "' and Control_box_state.kaiguan_string='" & statetype & "' order by Control_box_state.createtime"

        Else
            If check_state = "正常" Then

                sql = "select * from control_box_state where createtime>='" & dtp_date_start.Value & "' and createtime<='" & dtp_date_end.Value & "' and control_box_name='" & controlboxname & "' and state='" & check_state & "' and kaiguan_string='" & statetype & "' order by createtime"
                'sql = "SELECT control_box_state.StatusContent,control_box.control_box_type,control_box.control_box_id, control_box_state.Createtime, control_box_state.control_box_name, control_box_state.state, " _
                '& "Control_box_state.StatusContent2, Control_box_state.StatusContent3, Control_box_state.power_type, Control_box_state.kaiguan_string " _
                '& "FROM  control_box INNER JOIN Control_box_state ON control_box.control_box_name = Control_box_state.control_box_name " _
                '& "where Control_box_state.createtime>='" & dtp_date_start.Value & "' and Control_box_state.createtime<='" & dtp_date_end.Value & "' and Control_box_state.control_box_name='" & m_controlboxname & "' and Control_box_state.state='" & check_state & "' and Control_box_state.kaiguan_string='" & statetype & "' order by Control_box_state.createtime"

            Else
                sql = "select * from control_box_state where createtime>='" & dtp_date_start.Value & "' and createtime<='" & dtp_date_end.Value & "' and control_box_name='" & controlboxname & "' and state<>'正常' and kaiguan_string='" & statetype & "' order by createtime"
                'sql = "SELECT control_box_state.StatusContent,control_box.control_box_type,control_box.control_box_id, control_box_state.Createtime, control_box_state.control_box_name, control_box_state.state, " _
                '               & "Control_box_state.StatusContent2, Control_box_state.StatusContent3, Control_box_state.power_type, Control_box_state.kaiguan_string " _
                '               & "FROM  control_box INNER JOIN Control_box_state ON control_box.control_box_name = Control_box_state.control_box_name " _
                '               & "where Control_box_state.createtime>='" & dtp_date_start.Value & "' and Control_box_state.createtime<='" & dtp_date_end.Value & "' and Control_box_state.control_box_name='" & m_controlboxname & "' and Control_box_state.state<>'正常' and Control_box_state.kaiguan_string='" & statetype & "' order by Control_box_state.createtime"
            End If

        End If
        DBOperation.OpenConn(conn)
        rs_box = DBOperation.SelectSQL(conn, sql, msg)
        If rs_box Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "control_box_state", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        While rs_box.EOF = False
            If Me.BackgroundWorker_on_off.CancellationPending = True Then
                GoTo finish
            End If
            box_type = controlboxobj.Get_boxtype_name(Trim(rs_box.Fields("control_box_name").Value))  '主控箱类型
            '显示进度
            num = 100 * progress_value / rs_box.RecordCount
            If num > 100 Then
                num = 100
            End If
            Me.BackgroundWorker_on_off.ReportProgress(num)
            progress_value += 1
            If rs_box.Fields("state").Value Is System.DBNull.Value Then
                state = ""
            Else
                state = Trim(rs_box.Fields("state").Value)  '取主控箱的工作状态
            End If
            recData = Trim(rs_box.Fields("statusContent").Value).Split(" ")  '电流电压等值

            If recData.Length = 21 Then
                '小三遥
                m_small_sanyao = True
            End If

            i = 0

            If m_exceltable = 0 Then

                Me.SetTextDelegate("主控箱名称：" & controlboxname & "  数据采集时间：" & rs_box.Fields("Createtime").Value & vbCrLf, Me.rtb_doingnow_text)
                Me.SetTextDelegate("状态：" & state & vbCrLf, Me.rtb_doingnow_text)
                Me.SetTextDelegate("A相电压(V)：" & recData(0) & vbCrLf, Me.rtb_doingnow_text)
                Me.SetTextDelegate("B相电压(V)：" & recData(1) & vbCrLf, Me.rtb_doingnow_text)
                Me.SetTextDelegate("C相电压(V)：" & recData(2) & vbCrLf, Me.rtb_doingnow_text)
                i = 1
                j = 0
                While j < (recData.Length - 3) / 3
                    If recData.Length = 21 Then
                        If box_type = 1 Then
                            Me.SetTextDelegate(String.Format("第{0,-2}回路 电流(A)：{1,-10} ", controlboxobj.m_huilu_small(j), recData(i + 2)) & "  电功率(KW)：{0,-8}" & Format(recData(i + 3) / 1000, "0.000") & "  功率因数：" & recData(i + 4) & vbCrLf, Me.rtb_doingnow_text)
                        Else
                            Me.SetTextDelegate(String.Format("第{0,-2}回路 电流(A)：{1,-10} ", controlboxobj.m_huilu_small(j), recData(i + 2)) & String.Format("  电功率(KW)：{0,-8}", recData(i + 3)) & "  功率因数：" & recData(i + 4) & vbCrLf, Me.rtb_doingnow_text)

                        End If
                    Else
                        If box_type = 1 Then
                            Me.SetTextDelegate(String.Format("第{0,-2}回路 电流(A)：{1,-10} ", controlboxobj.m_huilu(j), recData(i + 2)) & "  电功率(KW)：{0,-8}" & Format(recData(i + 3) / 1000, "0.000") & "  功率因数：" & recData(i + 4) & vbCrLf, Me.rtb_doingnow_text)
                        Else
                            Dim s As String = String.Format(System.Convert.ToDouble(recData(i + 3)), "0.000")
                            Me.SetTextDelegate(String.Format("第{0,-2}回路 电流(A)：{1,-10} ", controlboxobj.m_huilu(j), recData(i + 2)) & String.Format("  电功率(KW)：{0,-8}", recData(i + 3)) & "  功率因数：" & recData(i + 4) & vbCrLf, Me.rtb_doingnow_text)

                        End If

                    End If
                    j += 1
                    i += 3
                End While
                Me.SetTextDelegate("" & vbCrLf, Me.rtb_doingnow_text)
            Else  '导出报表
                m_xlApp.Cells(m_row, 1) = "'" & m_id
                m_xlApp.Cells(m_row, 2) = "'" & rs_box.Fields("createtime").Value
                m_xlApp.Cells(m_row, 3) = "'" & controlboxname

                i = 3
                j = 0
                While j < (recData.Length - 3) / 3
                    If recData.Length = 21 Then
                        '小三遥

                        If j < 2 Then
                            'A相
                            m_xlApp.Cells(m_row, 4) = "'" & controlboxobj.m_huilu_small(j)
                            m_xlApp.Cells(m_row, 5) = "'" & recData(0)
                            m_xlApp.Cells(m_row, 6) = "'" & recData(i)
                            m_xlApp.Cells(m_row, 7) = "'" & Format(recData(i + 1) / 1000, "0.000")
                            m_xlApp.Cells(m_row, 8) = "'" & recData(i + 2)
                        Else
                            If j < 4 Then
                                'B相
                                m_xlApp.Cells(m_row, 4) = "'" & controlboxobj.m_huilu_small(j)
                                m_xlApp.Cells(m_row, 5) = "'" & recData(1)
                                m_xlApp.Cells(m_row, 6) = "'" & recData(i)
                                m_xlApp.Cells(m_row, 7) = "'" & Format(recData(i + 1) / 1000, "0.000")
                                m_xlApp.Cells(m_row, 8) = "'" & recData(i + 2)
                            Else 'C相
                                m_xlApp.Cells(m_row, 4) = "'" & controlboxobj.m_huilu_small(j)
                                m_xlApp.Cells(m_row, 5) = "'" & recData(2)
                                m_xlApp.Cells(m_row, 6) = "'" & recData(i)
                                m_xlApp.Cells(m_row, 7) = "'" & Format(recData(i + 1) / 1000, "0.000")
                                m_xlApp.Cells(m_row, 8) = "'" & recData(i + 2)
                            End If
                        End If


                    Else
                        If j < 4 Then
                            'A相
                            m_xlApp.Cells(m_row, 4) = "'" & controlboxobj.m_huilu(j)
                            m_xlApp.Cells(m_row, 5) = "'" & recData(0)
                            m_xlApp.Cells(m_row, 6) = "'" & recData(i)
                            m_xlApp.Cells(m_row, 7) = "'" & Format(recData(i + 1) / 1000, "0.000")
                            m_xlApp.Cells(m_row, 8) = "'" & recData(i + 2)
                        Else
                            If j < 8 Then
                                'B相
                                m_xlApp.Cells(m_row, 4) = "'" & controlboxobj.m_huilu(j)
                                m_xlApp.Cells(m_row, 5) = "'" & recData(1)
                                m_xlApp.Cells(m_row, 6) = "'" & recData(i)
                                m_xlApp.Cells(m_row, 7) = "'" & Format(recData(i + 1) / 1000, "0.000")
                                m_xlApp.Cells(m_row, 8) = "'" & recData(i + 2)
                            Else 'C相
                                m_xlApp.Cells(m_row, 4) = "'" & controlboxobj.m_huilu(j)
                                m_xlApp.Cells(m_row, 5) = "'" & recData(2)
                                m_xlApp.Cells(m_row, 6) = "'" & recData(i)
                                m_xlApp.Cells(m_row, 7) = "'" & Format(recData(i + 1) / 1000, "0.000")
                                m_xlApp.Cells(m_row, 8) = "'" & recData(i + 2)
                            End If
                        End If

                    End If


                    i += 3
                    j += 1
                    m_row += 1
                End While
                m_id += 1

            End If

            rs_box.MoveNext()
        End While

finish:
        If rs_box.State = 1 Then
            rs_box.Close()
            rs_box = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    ''' 统计进度显示
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BackgroundWorker_on_off_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker_on_off.ProgressChanged
        If Me.BackgroundWorker_on_off.CancellationPending = True Then
            Exit Sub

        End If
        ProgressBar.Value = e.ProgressPercentage
        If m_exceltable = 0 And m_stringtag = 0 Then
            g_welcomewinobj.circle_string.Text = "查询主控箱数据统计信息"
            record_num.Text = "查询主控箱数据统计信息"
            m_stringtag = 1

        Else
            If m_stringtag = 0 And m_exceltable = 1 Then
                g_welcomewinobj.circle_string.Text = "导出主控箱数据统计的EXCEL表"
                record_num.Text = "导出主控箱数据统计的EXCEL表"
                m_stringtag = 1
            End If

        End If
    End Sub

    ''' <summary>
    ''' 窗体载入
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 路灯亮暗信息统计_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = New Icon("图片\favicon.ico", 32, 32)
        dtp_date_start.CustomFormat = "yyyy-MM-dd HH:mm:ss  "  '查询条件中开始日期的格式
        dtp_date_end.CustomFormat = "yyyy-MM-dd HH:mm:ss" '查询条件中结束日期的格式
        ProgressBar.Visible = False
        m_stringtag = 0

        '初始化下拉框
        Com_inf.Select_city_name(cb_city_name)

        '开始日期默认为当前日期的前一天
        dtp_date_start.Value = DateAdd(DateInterval.Day, -1, Now)

        lb_lamp_id_start.Visible = False
        lb_lamptype_id.Visible = False
        m_id = 1
        m_checktype = 1

    End Sub

    ''' <summary>
    ''' 统计完毕，导出excel表；提示统计完毕
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BackgroundWorker_on_off_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker_on_off.RunWorkerCompleted
        ProgressBar.Value = 0
        m_id = 1
        ProgressBar.Visible = False
        m_statetag = 1  '文字颜色
        If m_exceltable = 1 Then
            m_xlApp.Visible = True
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Me.SetTextDelegate("主控箱数据统计EXCEL表导出完毕" & vbCrLf, Me.rtb_doingnow_text)

        Else
            If m_exceltable = 2 Then
                m_xlApp.Visible = True
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                Me.SetTextDelegate("主控箱数据统计导出完毕" & vbCrLf, Me.rtb_doingnow_text)

            Else
                Me.SetTextDelegate("主控箱数据统计完毕" & vbCrLf, Me.rtb_doingnow_text)
            End If

        End If


        'record_num.Text = "景观灯故障统计"
    End Sub

    ''' <summary>
    ''' 点击统计，启动统计线程，查询主控箱各类状态
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_static_excel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_static_excel.Click
        If dtp_date_start.Text = "" Then  '开始日期为空
            MsgBox("请选择开始时间", , PROJECT_TITLE_STRING)
            dtp_date_start.Focus()  '光标定位在开始日期
            Exit Sub
        End If
        If dtp_date_end.Text = "" Then  '结束日期为空
            MsgBox("请选择结束时间", , PROJECT_TITLE_STRING)
            dtp_date_end.Focus()  '光标定位在结束日期
            Exit Sub
        End If
        If Trim(dtp_date_start.Text) = Trim(dtp_date_end.Text) Then  '开始时间不应该和结束时间相同
            MsgBox("统计的开始时间与结束时间相同，请选择一个时间区间", , PROJECT_TITLE_STRING)
            dtp_date_start.Focus()
            Exit Sub
        End If
        If dtp_date_start.Value > dtp_date_end.Value Then
            MsgBox("开始时间大于结束时间，请重新选择一个时间区间", , PROJECT_TITLE_STRING)
            dtp_date_start.Focus()
            Exit Sub

        End If
        m_exceltable = 1  '标志为excel表
        ProgressBar.Visible = True
        m_timestart = dtp_date_start.Value  '开始日期
        m_timeend = dtp_date_end.Value  '结束日期
        m_stringtag = 0  '首次查询标志
        ' m_controlboxname = Trim(cb_control_box_name.Text)
        m_statestring = Trim(cb_state_list.Text) '选择状态
        m_id = 1

        If rb_city_control.Checked = True Then
            If Trim(cb_city_name.Text = "") Then
                MsgBox("请选择城市名称", , PROJECT_TITLE_STRING)
                Exit Sub
            End If
            get_controlboxname(1, Trim(cb_city_name.Text))
        End If

        If rb_area_control.Checked = True Then
            If Trim(cb_area_name.Text = "") Then
                MsgBox("请选择区域名称", , PROJECT_TITLE_STRING)
                Exit Sub
            End If
            get_controlboxname(2, Trim(cb_area_name.Text))
        End If

        If rb_street_control.Checked = True Then
            If Trim(cb_street_name.Text = "") Then
                MsgBox("请选择街道名称", , PROJECT_TITLE_STRING)
                Exit Sub
            End If
            get_controlboxname(3, Trim(cb_street_name.Text))
        End If

        If rb_control_box_name_control.Checked = True Then
            If Trim(cb_control_box_name.Text = "") Then
                MsgBox("请选择主控箱名称", , PROJECT_TITLE_STRING)
                Exit Sub
            End If
            get_controlboxname(4, Trim(cb_control_box_name.Text))
        End If

        '如果没有主控箱，进行提示
        If m_controlboxnamelist.Count = 0 Then
            MsgBox("请选择主控箱", , PROJECT_TITLE_STRING)
            Exit Sub
        End If


        If Me.BackgroundWorker_on_off.IsBusy = False Then
            Me.BackgroundWorker_on_off.RunWorkerAsync()
        Else
            MsgBox("线程正忙，请稍后重试", , PROJECT_TITLE_STRING)
        End If

    End Sub
    ''' <summary>
    ''' 运行状态的excel结构
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Private Sub communication_excel()
        m_xlApp = (New Microsoft.Office.Interop.Excel.Application)

        m_xlBook = m_xlApp.Workbooks().Add
        m_xlSheet = m_xlBook.Worksheets("sheet1")
        m_xlApp.Cells(1, 1) = "主控箱通信状态统计"
        m_xlApp.Rows(1).RowHeight = 50
        m_xlApp.Rows(1).Font.Size = 18
        m_xlApp.Rows(1).Font.Bold = True
        m_xlApp.Cells(2, 1) = ""
        m_xlApp.Cells(2, 2) = ""
        m_xlApp.Cells(2, 3) = "时间：" & dtp_date_start.Value.Date & "至" & dtp_date_end.Value.Date
        m_xlApp.Cells(2, 4) = ""
        m_xlApp.Cells(2, 5) = ""


        m_xlApp.Rows(2).RowHeight = 30
        m_xlApp.Rows(2).Font.Size = 12
        m_xlApp.Cells(3, 1) = "编号"
        m_xlApp.Cells(3, 2) = "主控箱"
        m_xlApp.Cells(3, 3) = "开始时间"
        m_xlApp.Cells(3, 4) = "结束时间"
        m_xlApp.Cells(3, 5) = "状态"
        m_xlApp.Rows(3).Font.Bold = True
        m_xlApp.Rows(3).font.size = 12
        m_xlApp.Rows(3).RowHeight = 30
        m_row = 4
        Dim i As Integer = 0
        Dim controlboxname As String
        While i < m_controlboxnamelist.Count
            controlboxname = m_controlboxnamelist(i)
            communication_state(m_statestring, controlboxname)
            i += 1
        End While

        'excel表的属性设计
        With m_xlSheet
            .Range(.Cells(1, 1), .Cells(1, 5)).Merge()
            .Range(.Cells(2, 1), .Cells(2, 2)).Merge()
            .Range(.Cells(2, 3), .Cells(2, 5)).Merge()


            .Range(.Cells(3, 1), .Cells(3, 1)).ColumnWidth = 5
            .Range(.Cells(3, 2), .Cells(3, 2)).ColumnWidth = 20
            .Range(.Cells(3, 3), .Cells(3, 3)).ColumnWidth = 20
            .Range(.Cells(3, 4), .Cells(3, 4)).ColumnWidth = 15
            .Range(.Cells(3, 5), .Cells(3, 5)).ColumnWidth = 15


            .Range(.Cells(4, 1), .Cells(m_row - 1, 5)).RowHeight = 20

            .Range(.Cells(1, 1), .Cells(1, 5)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter
            .Range(.Cells(2, 3), .Cells(2, 5)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight
            .Range(.Cells(2, 1), .Cells(2, 2)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft

            .Range(.Cells(3, 1), .Cells(m_row - 1, 5)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter


            .Range(.Cells(3, 1), .Cells(3, 5)).Font.Name = "宋体"
            '设标题为宋体字
            .Range(.Cells(3, 1), .Cells(3, 5)).Font.Bold = "True"
            '标题字体加粗
            .Range(.Cells(1, 1), .Cells(2, 5)).Borders.LineStyle = 0
            .Range(.Cells(3, 1), .Cells(m_row - 1, 5)).Borders.LineStyle = 1
            '设表格边框样式
            .Range(.Cells(2, 1), .Cells(m_row - 1, 5)).Font.Size = 12

            '表中数据的字号

            .PageSetup.CenterFooter = "第 &P 页/共 &N 页"

        End With
    End Sub

    ''' <summary>
    ''' 供电情况的excel结构
    ''' </summary>
    ''' <remarks></remarks> 
    Private Sub kaiguan_excel()
        m_xlApp = (New Microsoft.Office.Interop.Excel.Application)

        m_xlBook = m_xlApp.Workbooks().Add
        m_xlSheet = m_xlBook.Worksheets("sheet1")
        m_xlApp.Cells(1, 1) = "主控箱开关量报警统计"
        m_xlApp.Rows(1).RowHeight = 50
        m_xlApp.Rows(1).Font.Size = 18
        m_xlApp.Rows(1).Font.Bold = True
        m_xlApp.Cells(2, 1) = ""
        m_xlApp.Cells(2, 2) = ""
        m_xlApp.Cells(2, 3) = "时间：" & dtp_date_start.Value.Date & "至" & dtp_date_end.Value.Date
        m_xlApp.Cells(2, 4) = ""
        m_xlApp.Cells(2, 5) = ""

        m_xlApp.Rows(2).RowHeight = 30
        m_xlApp.Rows(2).Font.Size = 12
        m_xlApp.Cells(3, 1) = "编号"
        m_xlApp.Cells(3, 2) = "主控箱"
        m_xlApp.Cells(3, 3) = "开始时间"
        m_xlApp.Cells(3, 4) = "结束时间"
        m_xlApp.Cells(3, 5) = "开关量报警"
        m_xlApp.Rows(3).Font.Bold = True
        m_xlApp.Rows(3).font.size = 12
        m_xlApp.Rows(3).RowHeight = 30
        m_row = 4

        Dim i As Integer = 0
        Dim controlboxname As String
        While i < m_controlboxnamelist.Count
            controlboxname = m_controlboxnamelist(i)
            kaiguanliang_state(controlboxname)

            i += 1
        End While


        With m_xlSheet
            .Range(.Cells(1, 1), .Cells(1, 5)).Merge()
            .Range(.Cells(2, 1), .Cells(2, 2)).Merge()
            .Range(.Cells(2, 3), .Cells(2, 5)).Merge()


            .Range(.Cells(3, 1), .Cells(3, 1)).ColumnWidth = 5
            .Range(.Cells(3, 2), .Cells(3, 2)).ColumnWidth = 20
            .Range(.Cells(3, 3), .Cells(3, 3)).ColumnWidth = 20
            .Range(.Cells(3, 4), .Cells(3, 4)).ColumnWidth = 20
            .Range(.Cells(3, 5), .Cells(3, 5)).ColumnWidth = 20


            .Range(.Cells(4, 1), .Cells(m_row - 1, 5)).RowHeight = 20

            .Range(.Cells(1, 1), .Cells(1, 5)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter
            .Range(.Cells(2, 3), .Cells(2, 5)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight
            .Range(.Cells(2, 1), .Cells(2, 2)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft

            .Range(.Cells(3, 1), .Cells(m_row - 1, 4)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter


            .Range(.Cells(3, 1), .Cells(3, 5)).Font.Name = "宋体"
            '设标题为宋体字
            .Range(.Cells(3, 1), .Cells(3, 5)).Font.Bold = "True"
            '标题字体加粗
            .Range(.Cells(1, 1), .Cells(2, 5)).Borders.LineStyle = 0
            .Range(.Cells(3, 1), .Cells(m_row - 1, 5)).Borders.LineStyle = 1
            '设表格边框样式
            .Range(.Cells(2, 1), .Cells(m_row - 1, 5)).Font.Size = 12

            '表中数据的字号

            .PageSetup.CenterFooter = "第 &P 页/共 &N 页"

        End With
    End Sub
    ''' <summary>
    ''' 三遥运行情况的excel结构
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Private Sub sanyao_state_excel()
        m_xlApp = (New Microsoft.Office.Interop.Excel.Application)

        m_xlBook = m_xlApp.Workbooks().Add
        m_xlSheet = m_xlBook.Worksheets("sheet1")
        m_xlApp.Cells(1, 1) = "主控箱运行状态统计"
        m_xlApp.Rows(1).RowHeight = 50
        m_xlApp.Rows(1).Font.Size = 18
        m_xlApp.Rows(1).Font.Bold = True
        m_xlApp.Cells(2, 1) = ""
        m_xlApp.Cells(2, 2) = ""
        m_xlApp.Cells(2, 3) = "时间：" & dtp_date_start.Value.Date & "至" & dtp_date_end.Value.Date
        m_xlApp.Cells(2, 4) = ""
        m_xlApp.Cells(2, 5) = ""


        m_xlApp.Rows(2).RowHeight = 30
        m_xlApp.Rows(2).Font.Size = 12
        m_xlApp.Cells(3, 1) = "编号"
        m_xlApp.Cells(3, 2) = "主控箱"
        m_xlApp.Cells(3, 3) = "开始时间"
        m_xlApp.Cells(3, 4) = "结束时间"
        m_xlApp.Cells(3, 5) = "运行状态"
        m_xlApp.Rows(3).Font.Bold = True
        m_xlApp.Rows(3).font.size = 12
        m_xlApp.Rows(3).RowHeight = 30
        m_row = 4

        Dim i As Integer = 0
        Dim controlboxname As String
        While i < m_controlboxnamelist.Count
            controlboxname = m_controlboxnamelist(i)
            sanyao_state_state(m_statestring, controlboxname)
            i += 1

        End While


        With m_xlSheet
            .Range(.Cells(1, 1), .Cells(1, 5)).Merge()
            .Range(.Cells(2, 1), .Cells(2, 2)).Merge()
            .Range(.Cells(2, 3), .Cells(2, 5)).Merge()


            .Range(.Cells(3, 1), .Cells(3, 1)).ColumnWidth = 5
            .Range(.Cells(3, 2), .Cells(3, 2)).ColumnWidth = 20
            .Range(.Cells(3, 3), .Cells(3, 3)).ColumnWidth = 20
            .Range(.Cells(3, 4), .Cells(3, 4)).ColumnWidth = 15
            .Range(.Cells(3, 5), .Cells(3, 5)).ColumnWidth = 15


            .Range(.Cells(4, 1), .Cells(m_row - 1, 5)).RowHeight = 20

            .Range(.Cells(1, 1), .Cells(1, 5)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter
            .Range(.Cells(2, 3), .Cells(2, 5)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight
            .Range(.Cells(2, 1), .Cells(2, 2)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft

            .Range(.Cells(3, 1), .Cells(m_row - 1, 5)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter


            .Range(.Cells(3, 1), .Cells(3, 5)).Font.Name = "宋体"
            '设标题为宋体字
            .Range(.Cells(3, 1), .Cells(3, 5)).Font.Bold = "True"
            '标题字体加粗
            .Range(.Cells(1, 1), .Cells(2, 5)).Borders.LineStyle = 0
            .Range(.Cells(3, 1), .Cells(m_row - 1, 5)).Borders.LineStyle = 1
            '设表格边框样式
            .Range(.Cells(2, 1), .Cells(m_row - 1, 5)).Font.Size = 12

            '表中数据的字号

            .PageSetup.CenterFooter = "第 &P 页/共 &N 页"

        End With
    End Sub

    ''' <summary>
    ''' 三遥运行情况的excel结构
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Private Sub sanyao_total_state_excel()
        m_xlApp = (New Microsoft.Office.Interop.Excel.Application)

        m_xlBook = m_xlApp.Workbooks().Add
        m_xlSheet = m_xlBook.Worksheets("sheet1")
        m_xlApp.Cells(1, 1) = "主控箱综合状态统计"
        m_xlApp.Rows(1).RowHeight = 50
        m_xlApp.Rows(1).Font.Size = 18
        m_xlApp.Rows(1).Font.Bold = True
        m_xlApp.Cells(2, 1) = ""
        m_xlApp.Cells(2, 2) = ""
        m_xlApp.Cells(2, 3) = "时间：" & dtp_date_start.Value.Date & "至" & dtp_date_end.Value.Date
        m_xlApp.Cells(2, 4) = ""
        m_xlApp.Cells(2, 5) = ""


        m_xlApp.Rows(2).RowHeight = 30
        m_xlApp.Rows(2).Font.Size = 12
        m_xlApp.Cells(3, 1) = "编号"
        m_xlApp.Cells(3, 2) = "主控箱"
        m_xlApp.Cells(3, 3) = "开始时间"
        m_xlApp.Cells(3, 4) = "结束时间"
        m_xlApp.Cells(3, 5) = "运行状态"
        m_xlApp.Rows(3).Font.Bold = True
        m_xlApp.Rows(3).font.size = 12
        m_xlApp.Rows(3).RowHeight = 30
        m_row = 4

        Dim i As Integer = 0
        Dim controlboxname As String
        While i < m_controlboxnamelist.Count
            controlboxname = m_controlboxnamelist(i)
            sanyao_total_state(controlboxname)
            i += 1

        End While


        With m_xlSheet
            .Range(.Cells(1, 1), .Cells(1, 5)).Merge()
            .Range(.Cells(2, 1), .Cells(2, 2)).Merge()
            .Range(.Cells(2, 3), .Cells(2, 5)).Merge()


            .Range(.Cells(3, 1), .Cells(3, 1)).ColumnWidth = 5
            .Range(.Cells(3, 2), .Cells(3, 2)).ColumnWidth = 20
            .Range(.Cells(3, 3), .Cells(3, 3)).ColumnWidth = 20
            .Range(.Cells(3, 4), .Cells(3, 4)).ColumnWidth = 15
            .Range(.Cells(3, 5), .Cells(3, 5)).ColumnWidth = 15


            .Range(.Cells(4, 1), .Cells(m_row - 1, 5)).RowHeight = 20

            .Range(.Cells(1, 1), .Cells(1, 5)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter
            .Range(.Cells(2, 3), .Cells(2, 5)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight
            .Range(.Cells(2, 1), .Cells(2, 2)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft

            .Range(.Cells(3, 1), .Cells(m_row - 1, 5)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter


            .Range(.Cells(3, 1), .Cells(3, 5)).Font.Name = "宋体"
            '设标题为宋体字
            .Range(.Cells(3, 1), .Cells(3, 5)).Font.Bold = "True"
            '标题字体加粗
            .Range(.Cells(1, 1), .Cells(2, 5)).Borders.LineStyle = 0
            .Range(.Cells(3, 1), .Cells(m_row - 1, 5)).Borders.LineStyle = 1
            '设表格边框样式
            .Range(.Cells(2, 1), .Cells(m_row - 1, 5)).Font.Size = 12

            '表中数据的字号

            .PageSetup.CenterFooter = "第 &P 页/共 &N 页"

        End With
    End Sub

    ''' <summary>
    ''' 供电情况的excel结构
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Private Sub power_type_excel()
        m_xlApp = (New Microsoft.Office.Interop.Excel.Application)

        m_xlBook = m_xlApp.Workbooks().Add
        m_xlSheet = m_xlBook.Worksheets("sheet1")
        m_xlApp.Cells(1, 1) = "主控箱供电状态统计"
        m_xlApp.Rows(1).RowHeight = 50
        m_xlApp.Rows(1).Font.Size = 18
        m_xlApp.Rows(1).Font.Bold = True
        m_xlApp.Cells(2, 1) = ""
        m_xlApp.Cells(2, 2) = ""
        m_xlApp.Cells(2, 3) = "时间：" & dtp_date_start.Value.Date & "至" & dtp_date_end.Value.Date
        m_xlApp.Cells(2, 4) = ""
        m_xlApp.Cells(2, 5) = ""



        m_xlApp.Rows(2).RowHeight = 30
        m_xlApp.Rows(2).Font.Size = 12
        m_xlApp.Cells(3, 1) = "编号"
        m_xlApp.Cells(3, 2) = "主控箱"
        m_xlApp.Cells(3, 3) = "开始时间"
        m_xlApp.Cells(3, 4) = "结束时间"
        m_xlApp.Cells(3, 5) = "供电状态"
        m_xlApp.Rows(3).Font.Bold = True
        m_xlApp.Rows(3).font.size = 12
        m_xlApp.Rows(3).RowHeight = 30
        m_row = 4

        Dim i As Integer = 0
        Dim controlboxname As String
        While i < m_controlboxnamelist.Count
            controlboxname = m_controlboxnamelist(i)
            power_type_state(m_statestring, controlboxname)
            i += 1

        End While


        With m_xlSheet
            .Range(.Cells(1, 1), .Cells(1, 5)).Merge()
            .Range(.Cells(2, 1), .Cells(2, 2)).Merge()
            .Range(.Cells(2, 3), .Cells(2, 5)).Merge()


            .Range(.Cells(3, 1), .Cells(3, 1)).ColumnWidth = 5
            .Range(.Cells(3, 2), .Cells(3, 2)).ColumnWidth = 20
            .Range(.Cells(3, 3), .Cells(3, 3)).ColumnWidth = 20
            .Range(.Cells(3, 4), .Cells(3, 4)).ColumnWidth = 15
            .Range(.Cells(3, 5), .Cells(3, 5)).ColumnWidth = 15


            .Range(.Cells(4, 1), .Cells(m_row - 1, 4)).RowHeight = 20

            .Range(.Cells(1, 1), .Cells(1, 5)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter
            .Range(.Cells(2, 3), .Cells(2, 5)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight
            .Range(.Cells(2, 1), .Cells(2, 2)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft

            .Range(.Cells(3, 1), .Cells(m_row - 1, 5)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter


            .Range(.Cells(3, 1), .Cells(3, 5)).Font.Name = "宋体"
            '设标题为宋体字
            .Range(.Cells(3, 1), .Cells(3, 5)).Font.Bold = "True"
            '标题字体加粗
            .Range(.Cells(1, 1), .Cells(2, 5)).Borders.LineStyle = 0
            .Range(.Cells(3, 1), .Cells(m_row - 1, 5)).Borders.LineStyle = 1
            '设表格边框样式
            .Range(.Cells(2, 1), .Cells(m_row - 1, 5)).Font.Size = 12

            '表中数据的字号

            .PageSetup.CenterFooter = "第 &P 页/共 &N 页"

        End With
    End Sub


    ''' <summary>
    ''' 运行状态的excel结构
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Private Sub sanyao_date_excel()
        m_xlApp = (New Microsoft.Office.Interop.Excel.Application)

        m_xlBook = m_xlApp.Workbooks().Add
        m_xlSheet = m_xlBook.Worksheets("sheet1")
        m_xlApp.Cells(1, 1) = "主控箱数据统计"
        m_xlApp.Rows(1).RowHeight = 50
        m_xlApp.Rows(1).Font.Size = 18
        m_xlApp.Rows(1).Font.Bold = True
        m_xlApp.Cells(2, 1) = "单位：" & COMPANY_NAME
        m_xlApp.Cells(2, 2) = ""
        m_xlApp.Cells(2, 3) = ""
        m_xlApp.Cells(2, 4) = ""
        m_xlApp.Cells(2, 5) = ""
        m_xlApp.Cells(2, 6) = "时间：" & dtp_date_start.Value.Date & "至" & dtp_date_end.Value.Date
        m_xlApp.Cells(2, 7) = ""
        m_xlApp.Cells(2, 8) = ""


        m_xlApp.Rows(2).RowHeight = 30
        m_xlApp.Rows(2).Font.Size = 12
        m_xlApp.Cells(3, 1) = "编号"

        m_xlApp.Cells(3, 2) = "采集时间"
        m_xlApp.Cells(3, 3) = "主控箱"
        m_xlApp.Cells(3, 4) = "回路"
        m_xlApp.Cells(3, 5) = "电压(V)"
        m_xlApp.Cells(3, 6) = "电流(A)"
        m_xlApp.Cells(3, 7) = "功率(KW)"
        m_xlApp.Cells(3, 8) = "功率因数"

        m_xlApp.Rows(3).Font.Bold = True
        m_xlApp.Rows(3).font.size = 12
        m_xlApp.Rows(3).RowHeight = 30
        m_row = 4

        Dim i As Integer = 0
        Dim controlboxname As String
        While i < m_controlboxnamelist.Count
            controlboxname = m_controlboxnamelist(i)
            sanyao_state(m_statestring, controlboxname)
            i += 1
        End While


        With m_xlSheet
            .Range(.Cells(1, 1), .Cells(1, 8)).Merge()
            .Range(.Cells(2, 1), .Cells(2, 3)).Merge()
            .Range(.Cells(2, 4), .Cells(2, 5)).Merge()
            .Range(.Cells(2, 6), .Cells(2, 8)).Merge()
            i = 4
            If m_small_sanyao = True Then  '小三遥合并6行
                While i < m_row - 1
                    .Range(.Cells(i, 1), .Cells(i + 5, 1)).Merge()
                    .Range(.Cells(i, 2), .Cells(i + 5, 2)).Merge()
                    .Range(.Cells(i, 3), .Cells(i + 5, 3)).Merge()
                    i += 6
                End While

            Else
                While i < m_row - 1
                    .Range(.Cells(i, 1), .Cells(i + 11, 1)).Merge()
                    .Range(.Cells(i, 2), .Cells(i + 11, 2)).Merge()
                    .Range(.Cells(i, 3), .Cells(i + 11, 3)).Merge()
                    i += 12
                End While
            End If
         
            .Range(.Cells(3, 1), .Cells(3, 1)).ColumnWidth = 5
            .Range(.Cells(3, 2), .Cells(3, 2)).ColumnWidth = 20
            .Range(.Cells(3, 3), .Cells(3, 5)).ColumnWidth = 10
            .Range(.Cells(3, 6), .Cells(3, 8)).ColumnWidth = 15


            .Range(.Cells(4, 1), .Cells(m_row - 1, 8)).RowHeight = 20

            .Range(.Cells(1, 1), .Cells(1, 8)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter
            .Range(.Cells(2, 1), .Cells(2, 3)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft
            .Range(.Cells(2, 6), .Cells(2, 8)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight
            .Range(.Cells(2, 11), .Cells(2, 8)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight

            .Range(.Cells(3, 1), .Cells(m_row - 1, 8)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter


            .Range(.Cells(3, 1), .Cells(3, 8)).Font.Name = "宋体"
            '设标题为宋体字
            .Range(.Cells(3, 1), .Cells(3, 8)).Font.Bold = "True"
            '标题字体加粗
            .Range(.Cells(1, 1), .Cells(2, 8)).Borders.LineStyle = 0
            .Range(.Cells(3, 1), .Cells(m_row - 1, 8)).Borders.LineStyle = 1
            '设表格边框样式
            .Range(.Cells(2, 1), .Cells(m_row - 1, 8)).Font.Size = 12

            '表中数据的字号

            .PageSetup.CenterFooter = "第 &P 页/共 &N 页"

        End With
    End Sub

    '''' <summary>
    '''' 查询路灯的亮暗记录(文本框显示)
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    '''' <remarks></remarks>
    'Private Sub static_table_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    If dtp_date_start.Text = "" Then  '开始日期为空
    '        MsgBox("请选择开始时间", , PROJECT_TITLE_STRING)
    '        dtp_date_start.Focus()  '光标定位在开始日期
    '        Exit Sub
    '    End If
    '    If dtp_date_end.Text = "" Then  '结束日期为空
    '        MsgBox("请选择结束时间", , PROJECT_TITLE_STRING)
    '        dtp_date_end.Focus()  '光标定位在结束日期
    '        Exit Sub
    '    End If
    '    If Trim(dtp_date_start.Text) = Trim(dtp_date_end.Text) Then  '开始时间不应该和结束时间相同
    '        MsgBox("统计的开始时间与结束时间相同，请选择一个时间区间", , PROJECT_TITLE_STRING)
    '        dtp_date_start.Focus()
    '        Exit Sub
    '    End If

    '    If dtp_date_start.Value > dtp_date_end.Value Then
    '        MsgBox("开始时间大于结束时间，请重新选择一个时间区间", , PROJECT_TITLE_STRING)
    '        dtp_date_start.Focus()
    '        Exit Sub

    '    End If
    '    m_exceltable = 2  '标志为统计报表
    '    ProgressBar.Visible = True
    '    m_timestart = dtp_date_start.Value  '开始日期
    '    m_timeend = dtp_date_end.Value  '结束日期
    '    'check_time = 1  '查询进度参数
    '    m_stringtag = 0  '首次查询标志

    '    m_controlboxname = Trim(cb_control_box_name.Text)


    '    '按日期及电控箱范围进行查询
    '    If cb_control_box_name.Text = "" Then
    '        MsgBox("请选择主控箱", , PROJECT_TITLE_STRING)
    '        Exit Sub
    '    End If
    '    'static_tag = 0


    '    If Me.BackgroundWorker_on_off.IsBusy = False Then
    '        Me.BackgroundWorker_on_off.RunWorkerAsync()
    '    Else
    '        MsgBox("线程正忙，请稍后重试", , PROJECT_TITLE_STRING)
    '    End If
    'End Sub

    ''' <summary>
    ''' 关闭窗口
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 路灯亮暗信息统计_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If Me.BackgroundWorker_on_off.IsBusy = True Then
            Me.BackgroundWorker_on_off.CancelAsync()
        End If
      ProcessKill(m_xlApp, m_xlBook, m_xlSheet)
        '  CloseProcess(m_xlApp, m_xlBook, m_xlSheet)
    End Sub

    ''' <summary>
    ''' 选择查询三遥数据
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rb_sanyao_datacheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_sanyao_datacheck.Click
        If rb_sanyao_datacheck.Checked = True Then
            m_checktype = 1
            cb_state_list.Enabled = False
            cb_state_list.Items.Clear()
            cb_state_list.Items.Add("正常")
            cb_state_list.Items.Add("故障")
            cb_state_list.Items.Add("全部")
        End If
    End Sub

    ''' <summary>
    ''' 选择查询通信状态
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rb_communicationcheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_communicationcheck.Click
        If rb_communicationcheck.Checked = True Then
            m_checktype = 2
            cb_state_list.Enabled = True
            cb_state_list.Items.Clear()
            cb_state_list.Items.Add("通信正常")
            cb_state_list.Items.Add("未连接")
            cb_state_list.Items.Add("全部")

        End If
    End Sub

    ''' <summary>
    ''' 选择查询供电情况
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rb_powercheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_powercheck.Click
        If rb_powercheck.Checked = True Then
            m_checktype = 3
            cb_state_list.Enabled = True
            cb_state_list.Items.Clear()
            cb_state_list.Items.Add(POWERTYPE_BUTTERY)
            cb_state_list.Items.Add(POWERTYPE_CURRENT)
            cb_state_list.Items.Add("全部")

        End If
    End Sub

    ''' <summary>
    ''' 选择查询开关量
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rb_kaiguancheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_kaiguancheck.Click
        If rb_kaiguancheck.Checked = True Then
            m_checktype = 4
            cb_state_list.Items.Clear()
            cb_state_list.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' 选择查询三遥状态
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rb_sanyao_statecheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_sanyao_statecheck.Click
        If rb_sanyao_statecheck.Checked = True Then
            m_checktype = 5
            cb_state_list.Enabled = True
            cb_state_list.Items.Clear()
            cb_state_list.Items.Add("正常")
            cb_state_list.Items.Add("故障")
            cb_state_list.Items.Add("全部")
        End If
    End Sub

    Private Sub rb_city_control_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_city_control.Click
        If rb_city_control.Checked = True Then
            Me.cb_city_name.Enabled = True
            Me.cb_area_name.Enabled = False
            Me.cb_street_name.Enabled = False
            Me.cb_control_box_name.Enabled = False

        End If
    End Sub

    Private Sub rb_area_control_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_area_control.Click
        If rb_area_control.Checked = True Then
            Me.cb_city_name.Enabled = True
            Me.cb_area_name.Enabled = True
            Me.cb_street_name.Enabled = False
            Me.cb_control_box_name.Enabled = False

        End If
    End Sub

    Private Sub rb_street_control_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_street_control.Click
        If rb_street_control.Checked = True Then
            Me.cb_city_name.Enabled = True
            Me.cb_area_name.Enabled = True
            Me.cb_street_name.Enabled = True
            Me.cb_control_box_name.Enabled = False

        End If
    End Sub

    Private Sub rb_control_box_name_control_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_control_box_name_control.Click
        If rb_control_box_name_control.Checked = True Then
            Me.cb_city_name.Enabled = True
            Me.cb_area_name.Enabled = True
            Me.cb_street_name.Enabled = True
            Me.cb_control_box_name.Enabled = True

        End If
    End Sub

    Private Sub cb_city_name_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_city_name.DropDown
        Com_inf.Select_city_name(cb_city_name)
    End Sub

    Private Sub cb_city_name_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_city_name.SelectedIndexChanged
        Com_inf.Select_area_name(cb_city_name, cb_area_name)

    End Sub

    Private Sub cb_area_name_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_area_name.DropDown
        Com_inf.Select_area_name(cb_city_name, cb_area_name)
    End Sub

    Private Sub cb_area_name_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_area_name.SelectedIndexChanged
        Com_inf.Select_street_name(cb_city_name, cb_area_name, cb_street_name)

    End Sub

    Private Sub cb_street_name_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_street_name.DropDown
        Com_inf.Select_street_name(cb_city_name, cb_area_name, cb_street_name)

    End Sub

    Private Sub cb_street_name_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_street_name.SelectedIndexChanged
        Com_inf.Select_box_name_level(cb_city_name, cb_area_name, cb_street_name, cb_control_box_name)

    End Sub

    ''' <summary>
    ''' 综合的三遥数据查询，将通信和状态组合在一起
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rb_total_state_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_total_state.Click
        If rb_total_state.Checked = True Then
            m_checktype = 6
            cb_state_list.Text = ""
            cb_state_list.Enabled = False
        End If
    End Sub

    Private Sub bt_stopcheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_stopcheck.Click
        If Me.BackgroundWorker_on_off.IsBusy = True Then
            Me.BackgroundWorker_on_off.CancelAsync()
        End If
    End Sub
End Class