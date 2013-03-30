''' <summary>
''' 统计一个时间区间内灯的运行状态
''' </summary>
''' <remarks></remarks>
Public Class 路灯亮暗信息统计

    Private m_timestart, m_timeend As System.DateTime  '查询时间段的开始与结束时间
    Private m_statictag As Integer  '查询的级别城市，区域，街道等
    Private m_cityname As String  '查询的城市名称
    Private m_areaname As String   '查询的区域名称
    Private m_streetname As String  '查询的街道名称
    Private m_controlboxname As String  '查询的主控箱名称
    Private m_alarm_record As String  '查询的自动巡查的数据记录
    Private m_lamptype As String  '查询的节点类型
    Private m_lampid As String    '查询的节点编号
    Private m_statetag As Integer  '0表示暗，1表示亮
    Private m_exceltable As Integer  '0表示查询，1表示导出excel表
    Private m_xlApp As Microsoft.Office.Interop.Excel.Application
    Private m_xlBook As Microsoft.Office.Interop.Excel.Workbook
    Private m_xlSheet As Microsoft.Office.Interop.Excel.Worksheet
    Private m_row As Integer  '行数
    Private m_stringtag As Integer   '提示信息只出现一次
    Private m_lampidnum As Integer  '记录的查询路灯的个数
    Private m_checktime As Integer '查询进度参数
    Private m_id As Integer   '编号
    Private m_exceltag As Integer  '标志做哪些操作 m_exceltag=0查询设备状态日志， =1查询设备配置日志
    Private m_checkstate As String '查询的状态
    Private Structure configstate_list
        Dim control_box_name As String  '主控箱名称
        Dim control_box_id As String  '主控箱编号
        Dim config_state As String '配置状态
        Dim createtime As String  '时间
        Dim id As Integer  '编号
    End Structure
    Private m_statelist As New List(Of configstate_list)

    '定义的委托，用于设置文本或者是获取文本内容
    Delegate Sub SetTextControlCallBack(ByVal lamp_id As String, ByVal presure As String, ByVal current As String, ByVal power As String, ByVal yinshu As String, ByVal state As String, ByVal time_start As DateTime, ByVal time_end As DateTime, ByVal datalist As DataGridView, ByVal lampvision As Integer, ByVal lamp_pointinfor As String)



    ''' <summary>
    ''' 委托，在线程中使用文本框
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetTextControl(ByVal lamp_id As String, ByVal presure As String, ByVal current As String, ByVal power As String, ByVal yinshu As String, ByVal state As String, ByVal time_start As DateTime, ByVal time_end As DateTime, ByVal datalist As DataGridView, ByVal lampvision As Integer, ByVal lamp_pointinfor As String)

        '如果调用方位于创建该控件线程以外的线程时，需要获取是否要调用Invoke方法
        If (datalist.InvokeRequired) Then

            Dim SetText As SetTextControlCallBack = New SetTextControlCallBack(AddressOf SetTextControl)
            Me.Invoke(SetText, New Object() {lamp_id, presure, current, power, yinshu, state, time_start, time_end, datalist, lampvision, lamp_pointinfor})
        Else '如果不是线程外的调用时：设置文本框的值

            Dim row As Integer = datalist.Rows.Count
            datalist.Rows.Add()
            datalist.Rows(row).Cells("id_lamp").Value = row + 1
            If lamp_pointinfor = "" Then
                datalist.Rows(row).Cells("lampid").Value = lamp_id
            Else
                datalist.Rows(row).Cells("lampid").Value = lamp_id & lamp_pointinfor
            End If
            If lampvision < 3 Then
                datalist.Rows(row).Cells("presure").Value = "-"
                datalist.Rows(row).Cells("current").Value = current
                datalist.Rows(row).Cells("power").Value = "-"
                datalist.Rows(row).Cells("yinshu").Value = "-"
            Else
                datalist.Rows(row).Cells("presure").Value = presure
                datalist.Rows(row).Cells("current").Value = current
                datalist.Rows(row).Cells("power").Value = power
                datalist.Rows(row).Cells("yinshu").Value = yinshu
            End If
            datalist.Rows(row).Cells("state").Value = state
            datalist.Rows(row).Cells("start_time").Value = time_start
            datalist.Rows(row).Cells("end_time").Value = time_end
            End If
    End Sub
    ''' <summary>
    ''' 选择区域名称
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub static_control_box_name_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles static_control_box_name.DropDown
        Com_inf.Select_box_name_level(Me.static_city, Me.static_area, Me.static_street, Me.static_control_box_name)
        
    End Sub

    ''' <summary>
    ''' 区域名称改变，类型和编号跟着改变
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub static_control_box_name_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles static_control_box_name.SelectedIndexChanged
        Com_inf.Select_type_name(Me.static_control_box_name, Me.static_lamp_type, Me.lamp_type_id)
        Com_inf.Select_lamp_id_type(Me.static_control_box_name, Me.static_lamp_type, Me.static_lamp_id, Me.static_lamp_id_start)

    End Sub
    ''' <summary>
    ''' 选择灯的编号
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub static_lamp_id_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles static_lamp_id.DropDown

        Com_inf.Select_lamp_id_type(static_control_box_name, static_lamp_type, static_lamp_id, static_lamp_id_start)
    End Sub


    ''' <summary>
    ''' 查询路灯的亮暗记录
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub find_record_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles find_record.Click

        If Date_start.Text = "" Then  '开始日期为空
            MsgBox("请选择开始时间", , PROJECT_TITLE_STRING)
            Date_start.Focus()  '光标定位在开始日期
            Exit Sub
        End If
        If Date_start.Text = "" Then  '结束日期为空
            MsgBox("请选择结束时间", , PROJECT_TITLE_STRING)
            Date_end.Focus()  '光标定位在结束日期
            Exit Sub
        End If
        If Trim(Date_start.Text) = Trim(Date_end.Text) Then  '开始时间不应该和结束时间相同
            MsgBox("统计的开始时间与结束时间相同，请选择一个时间区间", , PROJECT_TITLE_STRING)
            Date_start.Focus()
            Exit Sub
        End If

        If Date_start.Value > Date_end.Value Then
            MsgBox("开始时间大于结束时间，请重新选择一个时间区间", , PROJECT_TITLE_STRING)
            Date_start.Focus()
            Exit Sub

        End If

        If Trim(Me.static_lampstate.Text) = "" Then
            MsgBox("请选择查询的条件", , PROJECT_TITLE_STRING)
            static_lampstate.Focus()
            Exit Sub
        End If

        If Trim(Me.ComboBox_Record.SelectedItem) = "数据记录" Then
            m_alarm_record = "lamp_alarm_record"
            end_time.Visible = False
        Else
            m_alarm_record = "lamp_state_list"
            end_time.Visible = True
        End If

        m_exceltable = 0
        ProgressBar.Visible = True
        m_timestart = Date_start.Value  '开始日期
        m_timeend = Date_end.Value  '结束日期
        m_checktime = 1  '查询进度参数
        m_stringtag = 0  '首次查询标志
        m_checkstate = Trim(static_lampstate.Text) '查询的条件：正常，故障，全部

        m_controlboxname = Trim(static_control_box_name.Text)
        m_lamptype = Trim(static_lamp_type.Text)

        m_lampid = Trim(static_lamp_id_start.Text) & Trim(static_lamp_id.Text)
        dgv_statelist.Rows.Clear()  '清除列表
        If city_control.Checked = True Then
            '按日期及城市范围进行查询
            If static_city.Text = "" Then
                MsgBox("请选择城市", , PROJECT_TITLE_STRING)
                Me.static_city.Focus()
                Exit Sub
            End If
            m_statictag = 0
            m_cityname = Trim(Me.static_city.Text)
        End If

        If area_control.Checked = True Then
            '按日期及区域范围进行查询
            If static_city.Text = "" Then
                MsgBox("请选择城市", , PROJECT_TITLE_STRING)
                Me.static_city.Focus()
                Exit Sub
            End If
            If static_area.Text = "" Then
                MsgBox("请选择区域", , PROJECT_TITLE_STRING)
                Me.static_area.Focus()
                Exit Sub
            End If
            m_statictag = 1
            m_cityname = Trim(Me.static_city.Text)
            m_areaname = Trim(Me.static_area.Text)
        End If

        If street_control.Checked = True Then
            '按日期及街道范围进行查询
            If static_city.Text = "" Then
                MsgBox("请选择城市", , PROJECT_TITLE_STRING)
                Me.static_city.Focus()
                Exit Sub
            End If
            If static_area.Text = "" Then
                MsgBox("请选择区域", , PROJECT_TITLE_STRING)
                Me.static_area.Focus()
                Exit Sub
            End If
            If static_street.Text = "" Then
                MsgBox("请选择街道", , PROJECT_TITLE_STRING)
                Me.static_street.Focus()
                Exit Sub
            End If
            m_statictag = 2
            m_cityname = Trim(Me.static_city.Text)
            m_areaname = Trim(Me.static_area.Text)
            m_streetname = Trim(Me.static_street.Text)
        End If

        If control_box_control.Checked = True Then
            '按日期及电控箱范围进行查询
            If static_city.Text = "" Then
                MsgBox("请选择城市", , PROJECT_TITLE_STRING)
                Me.static_city.Focus()
                Exit Sub
            End If
            If static_area.Text = "" Then
                MsgBox("请选择区域", , PROJECT_TITLE_STRING)
                Me.static_area.Focus()
                Exit Sub
            End If
            If static_street.Text = "" Then
                MsgBox("请选择街道", , PROJECT_TITLE_STRING)
                Me.static_street.Focus()
                Exit Sub
            End If
            If static_control_box_name.Text = "" Then
                MsgBox("请选择主控箱", , PROJECT_TITLE_STRING)
                Me.static_control_box_name.Focus()
                Exit Sub
            End If
            m_statictag = 3
            m_cityname = Trim(Me.static_city.Text)
            m_areaname = Trim(Me.static_area.Text)
            m_streetname = Trim(Me.static_street.Text)
            m_controlboxname = Trim(Me.static_control_box_name.Text)
        End If

        If lamp_type_control.Checked = True Then
            '按日期及类型范围进行查询

            If static_city.Text = "" Then
                MsgBox("请选择城市", , PROJECT_TITLE_STRING)
                Me.static_city.Focus()
                Exit Sub
            End If
            If static_area.Text = "" Then
                MsgBox("请选择区域", , PROJECT_TITLE_STRING)
                Me.static_area.Focus()
                Exit Sub
            End If
            If static_street.Text = "" Then
                MsgBox("请选择街道", , PROJECT_TITLE_STRING)
                Me.static_street.Focus()
                Exit Sub
            End If
            If static_control_box_name.Text = "" Then
                MsgBox("请选择主控箱", , PROJECT_TITLE_STRING)
                Me.static_control_box_name.Focus()
                Exit Sub
            End If
            If static_lamp_type.Text = "" Then
                MsgBox("请选择类型", , PROJECT_TITLE_STRING)
                Me.static_lamp_type.Focus()
                Exit Sub
            End If
            m_statictag = 4
            m_cityname = Trim(Me.static_city.Text)
            m_areaname = Trim(Me.static_area.Text)
            m_streetname = Trim(Me.static_street.Text)
            m_controlboxname = Trim(Me.static_control_box_name.Text)
            m_lamptype = Trim(Me.static_lamp_type.Text)
        End If

        If lamp_id_control.Checked = True Then
            '按日期及灯的编号范围进行查询

            If static_city.Text = "" Then
                MsgBox("请选择城市", , PROJECT_TITLE_STRING)
                Me.static_city.Focus()
                Exit Sub
            End If
            If static_area.Text = "" Then
                MsgBox("请选择区域", , PROJECT_TITLE_STRING)
                Me.static_area.Focus()
                Exit Sub
            End If
            If static_street.Text = "" Then
                MsgBox("请选择街道", , PROJECT_TITLE_STRING)
                Me.static_street.Focus()
                Exit Sub
            End If
            If static_control_box_name.Text = "" Then
                MsgBox("请选择主控箱", , PROJECT_TITLE_STRING)
                Me.static_control_box_name.Focus()
                Exit Sub
            End If
            If static_lamp_type.Text = "" Then
                MsgBox("请选择类型", , PROJECT_TITLE_STRING)
                Me.static_lamp_type.Focus()
                Exit Sub
            End If
            If static_lamp_id.Text = "" Then
                MsgBox("请选择灯的编号", , PROJECT_TITLE_STRING)
                Me.static_lamp_id.Focus()
                Exit Sub
            End If
            m_statictag = 5
            m_cityname = Trim(Me.static_city.Text)
            m_areaname = Trim(Me.static_area.Text)
            m_streetname = Trim(Me.static_street.Text)
            m_controlboxname = Trim(Me.static_control_box_name.Text)
            m_lamptype = Trim(Me.static_lamp_type.Text)
            m_lampid = Trim(static_lamp_id_start.Text) & Trim(Me.static_lamp_id.Text)

        End If

        If Me.BackgroundWorker_on_off.IsBusy = False Then
            Me.BackgroundWorker_on_off.RunWorkerAsync()
        Else
            MsgBox("线程正忙，请稍后重试", , PROJECT_TITLE_STRING)
        End If

    End Sub

    ''' <summary>
    ''' 清空文本框
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub clear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles clear.Click
        dgv_statelist.Rows.Clear()
    End Sub

    ''' <summary>
    ''' 查询状态线程的主函数
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BackgroundWorker_on_off_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker_on_off.DoWork
        '*****首先将数据库中所有无返回值的持续时间小于30分钟的条目置为5***************
        If m_exceltag = 0 Then
            Dim conn As New ADODB.Connection
            Dim msg As String
            Dim sql As String
            msg = ""
            sql = "update lamp_state_list set end_tag=5 where state='" & LAMP_STATE_NORETURN & "' and end_tag=1 and DateDiff(n,time_start,time_end)<30"
            DBOperation.OpenConn(conn)
            DBOperation.ExecuteSQL(conn, sql, msg)

            conn.Close()
            conn = Nothing
            '**************************************************************
            If m_exceltable = 0 Then
                lamp_id_state(m_statictag)
            Else
                If m_exceltable = 1 Then
                    excel()   'excel表
                End If

            End If

        Else
            excel() '导出excel 表
        End If

    End Sub

    ''' <summary>
    ''' 按路灯编号进行路灯状态查询
    ''' </summary>
    ''' <param name="find_tag"></param>
    ''' <remarks></remarks>
    Private Sub lamp_id_state(ByVal find_tag As Integer)
        Dim rs As ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim rs_on_off As New ADODB.Recordset
        Dim lamp_state As Integer  '1表示运行正常，0表示在时间段内有故障
        Dim lamp_id_check As String '查询的编号
        Dim control_box_name As String  '电控箱名称
        Dim lamp_type As String  '查询的灯的类型
        Dim lampvision As Integer '灯的型号

        lamp_state = 1  '路灯初始化为正常

        DBOperation.OpenConn(conn)
        Select Case find_tag
            Case 0
                '城市级别查询
                sql = "select control_box_name,type_string,lamp_id,jiechuqi_id from lamp_street where city='" & m_cityname & "' order by lamp_id"
            Case 1
                '区域级别查询
                sql = "select control_box_name,type_string,lamp_id,jiechuqi_id from lamp_street where city='" & m_cityname & "' and area='" & m_areaname & "' order by lamp_id"

            Case 2
                '街道级别查询
                sql = "select control_box_name,type_string,lamp_id,jiechuqi_id from lamp_street where city='" & m_cityname & "' and area='" & m_areaname & "' and street='" & m_streetname & "' order by lamp_id"
            Case 3
                '主控箱
                sql = "select control_box_name,type_string,lamp_id,jiechuqi_id from lamp_street where city='" & m_cityname & "' and area='" & m_areaname & "' and street='" & m_streetname & "' and control_box_name='" & m_controlboxname & "' order by lamp_id"
            Case 4
                '类型
                sql = "select control_box_name,type_string,lamp_id,jiechuqi_id from lamp_street where control_box_name='" & m_controlboxname & "' and type_string='" & m_lamptype & "' order by lamp_id"
            Case 5
                '灯的编号
                sql = "select control_box_name,type_string,lamp_id,jiechuqi_id from lamp_street where lamp_id='" & m_lampid & "' order by lamp_id"
            Case Else
                MsgBox(MSG_ERROR_STRING & "lamp_id_state", , PROJECT_TITLE_STRING)
                If rs_on_off IsNot Nothing Then
                    rs_on_off.Close()
                    rs_on_off = Nothing
                End If
                Exit Sub
        End Select

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
            m_lampidnum = rs.RecordCount  '记录查询路灯的个数，为查询的进度做参数
            While rs.EOF = False
                If Me.BackgroundWorker_on_off.CancellationPending = True Then
                    rs.Close()
                    rs = Nothing
                    If rs_on_off.State = 1 Then
                        rs_on_off.Close()
                        rs_on_off = Nothing

                    End If
                    conn.Close()
                    conn = Nothing
                    Exit Sub
                End If

                '判断每一盏路灯在此时间区间内容是否工作正常

                control_box_name = Trim(rs.Fields("control_box_name").Value)
                lamp_type = Trim(rs.Fields("type_string").Value)
                lamp_id_check = Trim(rs.Fields("lamp_id").Value)
                lampvision = rs.Fields("jiechuqi_id").Value

                Get_on_off_state(control_box_name, lamp_type, lamp_id_check, m_timestart, m_timeend, lampvision)

                rs.MoveNext()

            End While


        End If
        If rs_on_off.State = 1 Then
            rs_on_off.Close()
            rs_on_off = Nothing
        End If
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If

        conn.Close()
        conn = Nothing

    End Sub

    ''' <summary>
    ''' 查询路灯的亮暗状态
    ''' </summary>
    ''' <param name="lamp_id_tag"></param>
    ''' <param name="check_time_start"></param>
    ''' <param name="check_time_end"></param>
    ''' <remarks></remarks>
    Public Sub Get_on_off_state(ByVal control_box_name As String, ByVal lamp_type As String, ByVal lamp_id_tag As String, ByVal check_time_start As Date, ByVal check_time_end As Date, ByVal lampvision As Integer)
        Dim conn As New ADODB.Connection
        Dim rs As New ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim time_start As Date  '开始时间
        Dim time_end As Date  '结束时间
        Dim state As String  '灯的状态
        Dim current As Double  '电流值
        Dim lamp_pointinfor As String = "" '位置信息
        Dim presure As Double  '电压
        Dim power, yinshu As Double '功率，功率因数
        Dim electime_start As Date '计算耗电量的开始时间
        Dim electime_end As Date '计算耗电量的结束时间
        Dim lampid As String '单灯节点编号
        Dim checktag As Integer = 0 '查询条件，1为无返回信息状态
        lampid = Val(Mid(lamp_id_tag, 1, 4)).ToString & "-" & Val(Mid(lamp_id_tag, 5, 2)).ToString & "-" & Val(Mid(lamp_id_tag, 7, LAMP_ID_LEN)).ToString
        Dim progress_percentage As Integer
        DBOperation.OpenConn(conn)
        msg = ""
        state = ""
        If m_alarm_record = "lamp_state_list" Then

            If m_checkstate = "全部状态" Then
                sql = "SELECT A.*,B.lamp_pointinfor FROM " & m_alarm_record & " A LEFT JOIN lamp_inf B ON A.lamp_id=B.lamp_id where A.lamp_id='" & lamp_id_tag & "' and " & _
                     "((A.time_start<='" & check_time_start & "' and A.time_end>='" & check_time_end & "') or " & _
                     "(A.time_start>='" & check_time_start & "' and A.time_end<='" & check_time_end & "') or " & _
                      "(A.time_start>='" & check_time_start & "' and A.time_start<='" & check_time_end & "' and A.time_end>='" & check_time_start & "') or " & _
                      "(A.time_start<='" & check_time_start & "' and A.time_end>='" & check_time_start & "' and A.time_end<='" & check_time_end & "')) and A.end_tag<>" & 5 & "order by A.id"
            Else
                If m_checkstate = "正常状态" Then
                    sql = "SELECT A.*,B.lamp_pointinfor FROM " & m_alarm_record & " A LEFT JOIN lamp_inf B ON A.lamp_id=B.lamp_id where A.lamp_id='" & lamp_id_tag & "' and " & _
                         "((A.time_start<='" & check_time_start & "' and A.time_end>='" & check_time_end & "') or " & _
                         "(A.time_start>='" & check_time_start & "' and A.time_end<='" & check_time_end & "') or " & _
                          "(A.time_start>='" & check_time_start & "' and A.time_start<='" & check_time_end & "' and A.time_end>='" & check_time_start & "') or " & _
                          "(A.time_start<='" & check_time_start & "' and A.time_end>='" & check_time_start & "' and A.time_end<='" & check_time_end & "')) and A.end_tag<>" & 5 & " and (A.state='亮' or A.state='暗') order by A.id"
                Else
                    If m_checkstate = "故障状态" Then
                        sql = "SELECT A.*,B.lamp_pointinfor FROM " & m_alarm_record & " A LEFT JOIN lamp_inf B ON A.lamp_id=B.lamp_id where A.lamp_id='" & lamp_id_tag & "' and " & _
                                           "((A.time_start<='" & check_time_start & "' and A.time_end>='" & check_time_end & "') or " & _
                                           "(A.time_start>='" & check_time_start & "' and A.time_end<='" & check_time_end & "') or " & _
                                            "(A.time_start>='" & check_time_start & "' and A.time_start<='" & check_time_end & "' and A.time_end>='" & check_time_start & "') or " & _
                                            "(A.time_start<='" & check_time_start & "' and A.time_end>='" & check_time_start & "' and A.time_end<='" & check_time_end & "')) and A.end_tag<>" & 5 & " and (A.state<>'亮' and A.state<>'暗' and A.state<>'" & LAMP_STATE_NORETURN & "') order by A.id"
                    Else
                        sql = "SELECT A.*,B.lamp_pointinfor FROM " & m_alarm_record & " A LEFT JOIN lamp_inf B ON A.lamp_id=B.lamp_id where A.lamp_id='" & lamp_id_tag & "' and " & _
                                           "((A.time_start<='" & check_time_start & "' and A.time_end>='" & check_time_end & "') or " & _
                                           "(A.time_start>='" & check_time_start & "' and A.time_end<='" & check_time_end & "') or " & _
                                            "(A.time_start>='" & check_time_start & "' and A.time_start<='" & check_time_end & "' and A.time_end>='" & check_time_start & "') or " & _
                                            "(A.time_start<='" & check_time_start & "' and A.time_end>='" & check_time_start & "' and A.time_end<='" & check_time_end & "')) and A.end_tag<>" & 5 & " and (A.state='" & LAMP_STATE_NORETURN & "') order by A.id"
                        checktag = 1
                    End If

                End If
            End If

        Else
            '  end_time.Visible = False
            If m_checkstate = "全部状态" Then
                sql = "SELECT A.*,B.lamp_pointinfor FROM " & m_alarm_record & " A LEFT JOIN lamp_inf B ON A.lamp_id=B.lamp_id where A.lamp_id='" & lamp_id_tag & "' and " & _
                     "(A.time_start>='" & check_time_start & "' and A.time_start<='" & check_time_end & "') order by A.id"
            Else
                If m_checkstate = "正常状态" Then
                    sql = "SELECT A.*,B.lamp_pointinfor FROM " & m_alarm_record & " A LEFT JOIN lamp_inf B ON A.lamp_id=B.lamp_id where A.lamp_id='" & lamp_id_tag & "' and " & _
                         "(A.time_start>='" & check_time_start & "' and A.time_start<='" & check_time_end & "') and (A.state='亮' or A.state='暗') order by A.id"
                Else
                    If m_checkstate = "故障状态" Then
                        sql = "SELECT A.*,B.lamp_pointinfor FROM " & m_alarm_record & " A LEFT JOIN lamp_inf B ON A.lamp_id=B.lamp_id where A.lamp_id='" & lamp_id_tag & "' and " & _
                                           "(A.time_start>='" & check_time_start & "' and A.time_start<='" & check_time_end & "') and (A.state<>'亮' and A.state<>'暗' and A.state<>'" & LAMP_STATE_NORETURN & "') order by A.id"
                    Else
                        sql = "SELECT A.*,B.lamp_pointinfor FROM " & m_alarm_record & " A LEFT JOIN lamp_inf B ON A.lamp_id=B.lamp_id where A.lamp_id='" & lamp_id_tag & "' and " & _
                                           "(A.time_start>='" & check_time_start & "' and A.time_start<='" & check_time_end & "') and (A.state='" & LAMP_STATE_NORETURN & "') order by A.id"
                        checktag = 1
                    End If

                End If
            End If
        End If

        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "Get_on_off_state", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing

            Exit Sub
        End If
        If rs.RecordCount > 0 Then

            time_start = check_time_start
            time_end = check_time_end
            state = ""
            If rs.EOF = False Then

                state = Trim(rs.Fields("state").Value)
            End If
            While rs.EOF = False
                If Me.BackgroundWorker_on_off.CancellationPending = True Then
                    GoTo Finish

                End If
                m_checktime += 1
                time_start = Trim(rs.Fields("time_start").Value)

                progress_percentage = m_checktime * (100 / rs.RecordCount) / m_lampidnum
                If progress_percentage > 100 Then
                    progress_percentage = 100
                End If
                Me.BackgroundWorker_on_off.ReportProgress(progress_percentage)

                electime_start = rs.Fields("time_start").Value  '开始时间
                electime_end = rs.Fields("time_end").Value '结束时间

                '位置信息
                If IsDBNull(rs.Fields("lamp_pointinfor").Value) = False Then
                    lamp_pointinfor = rs.Fields("lamp_pointinfor").Value
                    If Trim(lamp_pointinfor) <> "" Then
                        lamp_pointinfor = "  位置:" & lamp_pointinfor
                    Else
                        lamp_pointinfor = ""
                    End If
                Else
                    lamp_pointinfor = ""
                End If
                If checktag = 0 Then
                    '电流
                    If rs.Fields("current_value").Value Is System.DBNull.Value Then
                        current = 0
                    Else
                        If rs.Fields("current_value").Value = -1 Then
                            current = 0
                        Else
                            current = Format(rs.Fields("current_value").Value, "0.00")
                        End If

                    End If
                    '电压
                    If rs.Fields("presure_value").Value Is System.DBNull.Value Then
                        presure = 0
                    Else
                        If rs.Fields("presure_value").Value = -1 Then
                            presure = 0
                        Else
                            presure = Format(rs.Fields("presure_value").Value, "0.00")
                        End If

                    End If

                    '功率
                    If rs.Fields("power_value").Value Is System.DBNull.Value Then
                        power = 0
                    Else
                        If rs.Fields("power_value").Value = -1 Then
                            power = 0
                        Else
                            power = Format(rs.Fields("power_value").Value, "0.00")
                        End If

                    End If

                    '功率因数
                    If rs.Fields("yinshu_value").Value Is System.DBNull.Value Then
                        yinshu = 0
                    Else
                        If rs.Fields("yinshu_value").Value = -1 Then
                            yinshu = 0
                        Else
                            yinshu = Format(rs.Fields("yinshu_value").Value, "0.00")
                        End If

                    End If
                Else
                    current = 0
                    presure = 0
                    power = 0
                    yinshu = 0
                End If

                '状态变化
                If rs.Fields("time_end").Value > check_time_end Then
                    time_end = check_time_end
                Else
                    time_end = rs.Fields("time_end").Value
                End If
                rs.MoveNext()
                'If rs.EOF = False Then

                '    If state <> Trim(rs.Fields("state").Value) Then

                '        GoTo finish2
                '    Else
                '        While state = Trim(rs.Fields("state").Value)
                '            If rs.Fields("time_end").Value > check_time_end Then
                '                time_end = check_time_end
                '            Else
                '                time_end = rs.Fields("time_end").Value

                '            End If

                '            rs.MoveNext()

                '            If rs.EOF = True Then
                '                GoTo Finish2
                '                '电流
                '                If rs.Fields("current_value").Value Is System.DBNull.Value Then
                '                    current = 0
                '                Else
                '                    If rs.Fields("current_value").Value = -1 Then
                '                        current = 0
                '                    Else
                '                        current = Format(rs.Fields("current_value").Value, "0.00")
                '                    End If

                '                End If

                '                '电压
                '                If rs.Fields("presure_value").Value Is System.DBNull.Value Then
                '                    presure = 0
                '                Else
                '                    If rs.Fields("presure_value").Value = -1 Then
                '                        presure = 0
                '                    Else
                '                        presure = Format(rs.Fields("presure_value").Value, "0.00")
                '                    End If

                '                End If

                '                '功率
                '                If rs.Fields("power_value").Value Is System.DBNull.Value Then
                '                    power = 0
                '                Else
                '                    If rs.Fields("power_value").Value = -1 Then
                '                        power = 0
                '                    Else
                '                        power = Format(rs.Fields("power_value").Value, "0.00")
                '                    End If

                '                End If

                '                '功率因数
                '                If rs.Fields("yinshu_value").Value Is System.DBNull.Value Then
                '                    yinshu = 0
                '                Else
                '                    If rs.Fields("yinshu_value").Value = -1 Then
                '                        yinshu = 0
                '                    Else
                '                        yinshu = Format(rs.Fields("yinshu_value").Value, "0.00")
                '                    End If

                '                End If

                '            End If
                '        End While

                '    End If


                'End If
finish2:

                If m_exceltable = 0 Then
                    '文本框中的信息
                    If state = LAMP_STATE_ON Then
                        m_statetag = 1 '文字颜色亮
                    Else
                        If state = LAMP_STATE_OFF Then
                            m_statetag = 2 '文字颜色为暗
                        Else
                            m_statetag = 4

                        End If

                    End If

                    '对主控节点的查询只查状态，不查电流及功率因数（因为不准）
                    'If Mid(lamp_id_tag, 5, 2) = "31" Then
                    '    inf_string = "时间区间：" & time_start & "至" & time_end & " " & " 主控箱：" & control_box_name & " " & lamp_type & "，节点编号：" & Val(Mid(lamp_id_tag, 1, 4)).ToString & "-" & Val(Mid(lamp_id_tag, 5, 2)).ToString & "-" & Val(Mid(lamp_id_tag, 7, LAMP_ID_LEN)).ToString & " 状态为：" & state
                    'Else
                    '    inf_string = "时间区间：" & time_start & "至" & time_end & " " & " 主控箱：" & control_box_name & " " & lamp_type & "，灯编号：" & Val(Mid(lamp_id_tag, 1, 4)).ToString & "-" & Val(Mid(lamp_id_tag, 5, 2)).ToString & "-" & Val(Mid(lamp_id_tag, 7, LAMP_ID_LEN)).ToString & " 电流(A)：" _
                    '    & current.ToString & " 电压(V)：" & presure.ToString & " 功率(W)：" & power.ToString & " 功率因数：" & yinshu.ToString & " 状态为：" & state

                    'End If

                    Me.SetTextControl(lampid, presure, current, power, yinshu, state, time_start, time_end, dgv_statelist, lampvision, lamp_pointinfor)
                Else
                    If m_exceltable = 1 Then
                        'EXCEL表

                        m_xlApp.Cells(m_row, 1) = "'" & m_id
                        m_xlApp.Cells(m_row, 2) = "'" & lampid & lamp_pointinfor
                        m_xlApp.Cells(m_row, 3) = state
                        If Mid(lamp_id_tag, 5, 2) = "31" Then  '主控箱节点则赋值为0
                            m_xlApp.Cells(m_row, 4) = "-"
                            m_xlApp.Cells(m_row, 5) = "-"
                            m_xlApp.Cells(m_row, 6) = "-"
                            m_xlApp.Cells(m_row, 7) = "-"
                        Else
                            If lampvision < 3 Then
                                m_xlApp.Cells(m_row, 4) = "'" & current.ToString
                                m_xlApp.Cells(m_row, 5) = "-"
                                m_xlApp.Cells(m_row, 6) = "-"
                                m_xlApp.Cells(m_row, 7) = "-"
                            Else
                                m_xlApp.Cells(m_row, 4) = "'" & current.ToString
                                m_xlApp.Cells(m_row, 5) = "'" & presure.ToString
                                m_xlApp.Cells(m_row, 6) = "'" & power.ToString
                                m_xlApp.Cells(m_row, 7) = "'" & yinshu.ToString
                            End If

                        End If

                        m_xlApp.Cells(m_row, 8) = "'" & time_start
                        m_xlApp.Cells(m_row, 9) = "'" & time_end

                        m_row += 1
                        m_id += 1


                    End If


                End If
                If rs.EOF = False Then
                    state = Trim(rs.Fields("state").Value)

                End If


            End While

            'Else
            '    '无状态
            '    If state = "" Then
            '        state = LAMP_STATE_NORETURN
            '        time_start = check_time_start
            '        time_end = check_time_end
            '    End If
            '    If m_exceltable = 0 Then
            '        '文本框中的信息
            '        If state = LAMP_STATE_ON Then
            '            m_statetag = 1 '文字颜色亮
            '        Else
            '            If state = LAMP_STATE_OFF Then
            '                m_statetag = 2 '文字颜色为暗
            '            Else
            '                m_statetag = 4

            '            End If

            '        End If

            '        '对主控节点的查询只查状态，不查电流及功率因数（因为不准）
            '        'If Mid(lamp_id_tag, 5, 2) = "31" Then
            '        '    inf_string = "时间区间：" & time_start & "至" & time_end & " " & " 主控箱：" & control_box_name & " " & lamp_type & "，节点编号：" & Val(Mid(lamp_id_tag, 1, 4)).ToString & "-" & Val(Mid(lamp_id_tag, 5, 2)).ToString & "-" & Val(Mid(lamp_id_tag, 7, LAMP_ID_LEN)).ToString & " 状态为：" & state
            '        'Else
            '        '    inf_string = "时间区间：" & time_start & "至" & time_end & " " & " 主控箱：" & control_box_name & " " & lamp_type & "，灯编号：" & Val(Mid(lamp_id_tag, 1, 4)).ToString & "-" & Val(Mid(lamp_id_tag, 5, 2)).ToString & "-" & Val(Mid(lamp_id_tag, 7, LAMP_ID_LEN)).ToString & " 电流(A)：" _
            '        '    & current.ToString & " 电压(V)：" & presure.ToString & " 功率(W)：" & power.ToString & " 功率因数：" & yinshu.ToString & " 状态为：" & state

            '        'End If
            '        Me.SetTextControl(lampid, presure, current, power, yinshu, state, time_start, time_end, dgv_statelist)

            '    Else
            '        If m_exceltable = 1 Then
            '            'EXCEL表
            '            m_xlApp.Cells(m_row, 1) = "'" & m_id
            '            m_xlApp.Cells(m_row, 2) = "'" & lampid
            '            m_xlApp.Cells(m_row, 3) = state
            '            If Mid(lamp_id_tag, 5, 2) = "31" Then  '主控箱节点则赋值为0
            '                m_xlApp.Cells(m_row, 4) = "'" & "0"
            '                m_xlApp.Cells(m_row, 5) = "'" & "0"
            '                m_xlApp.Cells(m_row, 6) = "'" & "0"
            '                m_xlApp.Cells(m_row, 7) = "'" & "0"
            '            Else
            '                m_xlApp.Cells(m_row, 4) = "'" & current.ToString
            '                m_xlApp.Cells(m_row, 5) = "'" & presure.ToString
            '                m_xlApp.Cells(m_row, 6) = "'" & power.ToString
            '                m_xlApp.Cells(m_row, 7) = "'" & yinshu.ToString
            '            End If

            '            m_xlApp.Cells(m_row, 8) = "'" & time_start
            '            m_xlApp.Cells(m_row, 9) = "'" & time_end

            '            m_row += 1
            '            m_id += 1

            '        End If


            '    End If

        End If

Finish:

        'If m_exceltable = 0 Then
        '    Me.SetTextControl(lampid, presure, current, power, yinshu, state, time_start, time_end, dgv_statelist)

        'End If
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
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
        If Me.BackgroundWorker_on_off.CancellationPending = True Then
            ProgressBar.Value = e.ProgressPercentage

        End If
        If Me.m_exceltag = 0 Then
            If m_exceltable = 0 And m_stringtag = 0 Then
                g_welcomewinobj.circle_string.Text = "查询路灯运行状态统计信息"
                record_num.Text = "查询路灯运行状态统计信息"
                m_stringtag = 1

            Else
                If m_stringtag = 0 And m_exceltable = 1 Then
                    g_welcomewinobj.circle_string.Text = "导出路灯运行状态统计的EXCEL表"
                    record_num.Text = "导出路灯运行状态统计的EXCEL表"
                    m_stringtag = 1

                End If

            End If

        Else
            If Me.m_exceltag = 1 Then
                g_welcomewinobj.circle_string.Text = "导出设备配置状态统计的EXCEL表"
                record_num.Text = "导出设备配置状态统计的EXCEL表"
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
        'TODO: 这行代码将数据加载到表“ConfigDataSet.config_state_list”中。您可以根据需要移动或移除它。
        Me.Config_state_listTableAdapter.FillBy_nodate_all(Me.ConfigDataSet.config_state_list)
        Date_start.CustomFormat = "yyyy-MM-dd HH:mm:ss  "  '查询条件中开始日期的格式
        Date_end.CustomFormat = "yyyy-MM-dd HH:mm:ss" '查询条件中结束日期的格式
        DateTimePickerStart.CustomFormat = "yyyy-MM-dd HH:mm:ss  "
        DateTimePickerEnd.CustomFormat = "yyyy-MM-dd HH:mm:ss  "
        '设置各个窗体的图标()
        Me.Icon = New Icon("图片\favicon.ico", 32, 32)
        m_statictag = 0  '初始化查询范围标志
        ProgressBar.Visible = False
        m_stringtag = 0

        '初始化下拉框
        Com_inf.Select_city_name(static_city)

        static_lamp_id_start.Visible = False
        lamp_type_id.Visible = False
        m_id = 1
 
        '开始日期默认为当前日期的前一天
        Date_start.Value = DateAdd(DateInterval.Day, -1, Now)
        DateTimePickerStart.Value = DateAdd(DateInterval.Day, -1, Now)

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
        If m_exceltag = 0 Then   '灯的运行状态日志板块
            If m_exceltable = 1 Then
                m_xlApp.Visible = True
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                ' Me.SetTextControl("路灯运行状态统计EXCEL表导出完毕" & vbCrLf, Me.area_control_list)

            Else
                If m_exceltable = 2 Then
                    m_xlApp.Visible = True
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                    '  Me.SetTextControl("路灯故障统计导出完毕" & vbCrLf, Me.area_control_list)

                Else
                    '   Me.SetTextControl("路灯运行状态统计完毕" & vbCrLf, Me.area_control_list)
                End If

            End If

        Else
            If m_exceltag = 1 Then
                '设备的配置状态板块
                m_xlApp.Visible = True
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                ' Me.SetTextControl("设备配置状态EXCEL表导出完毕" & vbCrLf, Me.area_control_list)

            End If
        End If


    End Sub

    ''' <summary>
    ''' 点击统计，启动统计线程
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub static_excel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles static_excel.Click
        '查询路灯的亮暗记录
        m_exceltag = 0  '导出状态日志

        If Date_start.Text = "" Then  '开始日期为空
            MsgBox("请选择开始时间", , PROJECT_TITLE_STRING)
            Date_start.Focus()  '光标定位在开始日期
            Exit Sub
        End If
        If Date_end.Text = "" Then  '结束日期为空
            MsgBox("请选择结束时间", , PROJECT_TITLE_STRING)
            Date_end.Focus()  '光标定位在结束日期
            Exit Sub
        End If
        If Trim(Date_start.Text) = Trim(Date_end.Text) Then  '开始时间不应该和结束时间相同
            MsgBox("统计的开始时间与结束时间相同，请选择一个时间区间", , PROJECT_TITLE_STRING)
            Date_start.Focus()
            Exit Sub
        End If

        If Date_start.Value > Date_end.Value Then
            MsgBox("开始时间大于结束时间，请重新选择一个时间区间", , PROJECT_TITLE_STRING)
            Date_start.Focus()
            Exit Sub

        End If
        m_exceltable = 1  '标志为excel表
        ProgressBar.Visible = True
        m_timestart = Date_start.Value  '开始日期
        m_timeend = Date_end.Value  '结束日期
        m_checktime = 1  '查询进度参数
        m_stringtag = 0  '首次查询标志

        m_controlboxname = Trim(static_control_box_name.Text)
        m_lamptype = Trim(static_lamp_type.Text)

        m_lampid = Trim(static_lamp_id_start.Text) & Trim(static_lamp_id.Text)


        If city_control.Checked = True Then
            '按日期及城市范围进行查询
            If static_city.Text = "" Then
                MsgBox("请选择城市", , PROJECT_TITLE_STRING)
                Me.static_city.Focus()
                Exit Sub
            End If
            m_statictag = 0
            m_cityname = Trim(Me.static_city.Text)
        End If

        If area_control.Checked = True Then
            '按日期及区域范围进行查询
            If static_city.Text = "" Then
                MsgBox("请选择城市", , PROJECT_TITLE_STRING)
                Me.static_city.Focus()
                Exit Sub
            End If
            If static_area.Text = "" Then
                MsgBox("请选择区域", , PROJECT_TITLE_STRING)
                Me.static_area.Focus()
                Exit Sub
            End If
            m_statictag = 1
            m_cityname = Trim(Me.static_city.Text)
            m_areaname = Trim(Me.static_area.Text)
        End If

        If street_control.Checked = True Then
            '按日期及街道范围进行查询
            If static_city.Text = "" Then
                MsgBox("请选择城市", , PROJECT_TITLE_STRING)
                Me.static_city.Focus()
                Exit Sub
            End If
            If static_area.Text = "" Then
                MsgBox("请选择区域", , PROJECT_TITLE_STRING)
                Me.static_area.Focus()
                Exit Sub
            End If
            If static_street.Text = "" Then
                MsgBox("请选择街道", , PROJECT_TITLE_STRING)
                Me.static_street.Focus()
                Exit Sub
            End If
            m_statictag = 2
            m_cityname = Trim(Me.static_city.Text)
            m_areaname = Trim(Me.static_area.Text)
            m_streetname = Trim(Me.static_street.Text)
        End If

        If control_box_control.Checked = True Then
            '按日期及电控箱范围进行查询
            If static_city.Text = "" Then
                MsgBox("请选择城市", , PROJECT_TITLE_STRING)
                Me.static_city.Focus()
                Exit Sub
            End If
            If static_area.Text = "" Then
                MsgBox("请选择区域", , PROJECT_TITLE_STRING)
                Me.static_area.Focus()
                Exit Sub
            End If
            If static_street.Text = "" Then
                MsgBox("请选择街道", , PROJECT_TITLE_STRING)
                Me.static_street.Focus()
                Exit Sub
            End If
            If static_control_box_name.Text = "" Then
                MsgBox("请选择主控箱", , PROJECT_TITLE_STRING)
                Me.static_control_box_name.Focus()
                Exit Sub
            End If
            m_statictag = 3
            m_cityname = Trim(Me.static_city.Text)
            m_areaname = Trim(Me.static_area.Text)
            m_streetname = Trim(Me.static_street.Text)
            m_controlboxname = Trim(Me.static_control_box_name.Text)
        End If

        If lamp_type_control.Checked = True Then
            '按日期及类型范围进行查询

            If static_city.Text = "" Then
                MsgBox("请选择城市", , PROJECT_TITLE_STRING)
                Me.static_city.Focus()
                Exit Sub
            End If
            If static_area.Text = "" Then
                MsgBox("请选择区域", , PROJECT_TITLE_STRING)
                Me.static_area.Focus()
                Exit Sub
            End If
            If static_street.Text = "" Then
                MsgBox("请选择街道", , PROJECT_TITLE_STRING)
                Me.static_street.Focus()
                Exit Sub
            End If
            If static_control_box_name.Text = "" Then
                MsgBox("请选择主控箱", , PROJECT_TITLE_STRING)
                Me.static_control_box_name.Focus()
                Exit Sub
            End If
            If static_lamp_type.Text = "" Then
                MsgBox("请选择类型", , PROJECT_TITLE_STRING)
                Me.static_lamp_type.Focus()
                Exit Sub
            End If
            m_statictag = 4
            m_cityname = Trim(Me.static_city.Text)
            m_areaname = Trim(Me.static_area.Text)
            m_streetname = Trim(Me.static_street.Text)
            m_controlboxname = Trim(Me.static_control_box_name.Text)
            m_lamptype = Trim(Me.static_lamp_type.Text)
        End If

        If lamp_id_control.Checked = True Then
            '按日期及灯的编号范围进行查询

            If static_city.Text = "" Then
                MsgBox("请选择城市", , PROJECT_TITLE_STRING)
                Me.static_city.Focus()
                Exit Sub
            End If
            If static_area.Text = "" Then
                MsgBox("请选择区域", , PROJECT_TITLE_STRING)
                Me.static_area.Focus()
                Exit Sub
            End If
            If static_street.Text = "" Then
                MsgBox("请选择街道", , PROJECT_TITLE_STRING)
                Me.static_street.Focus()
                Exit Sub
            End If
            If static_control_box_name.Text = "" Then
                MsgBox("请选择主控箱", , PROJECT_TITLE_STRING)
                Me.static_control_box_name.Focus()
                Exit Sub
            End If
            If static_lamp_type.Text = "" Then
                MsgBox("请选择类型", , PROJECT_TITLE_STRING)
                Me.static_lamp_type.Focus()
                Exit Sub
            End If
            If static_lamp_id.Text = "" Then
                MsgBox("请选择灯的编号", , PROJECT_TITLE_STRING)
                Me.static_lamp_id.Focus()
                Exit Sub
            End If
            m_statictag = 5
            m_cityname = Trim(Me.static_city.Text)
            m_areaname = Trim(Me.static_area.Text)
            m_streetname = Trim(Me.static_street.Text)
            m_controlboxname = Trim(Me.static_control_box_name.Text)
            m_lamptype = Trim(Me.static_lamp_type.Text)
            '  m_lampid = Trim(Me.static_lamp_id.Text)
            m_lampid = Trim(static_lamp_id_start.Text) & Trim(static_lamp_id.Text)


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
    Private Sub excel()
        If m_exceltag = 0 Then  '导出状态日志
            m_xlApp = (New Microsoft.Office.Interop.Excel.Application)

            m_xlBook = m_xlApp.Workbooks().Add
            m_xlSheet = m_xlBook.Worksheets("sheet1")
            m_xlApp.Cells(1, 1) = "路灯运行状态统计"
            m_xlApp.Rows(1).RowHeight = 50
            m_xlApp.Rows(1).Font.Size = 18
            m_xlApp.Rows(1).Font.Bold = True


            ' dt.Columns.Add("市", GetType(String))
            m_xlApp.Cells(2, 1) = "单位：" & COMPANY_NAME
            m_xlApp.Cells(2, 2) = ""
            m_xlApp.Cells(2, 3) = ""
            m_xlApp.Cells(2, 4) = "区域：" & m_controlboxname
            m_xlApp.Cells(2, 5) = ""
            m_xlApp.Cells(2, 6) = ""
            m_xlApp.Cells(2, 7) = "时间：" & Date_start.Value.Date & "至" & Date_end.Value.Date
            m_xlApp.Cells(2, 8) = ""
            m_xlApp.Cells(2, 9) = ""
            m_xlApp.Rows(2).RowHeight = 30
            m_xlApp.Rows(2).Font.Size = 12

            m_xlApp.Cells(3, 1) = "编号"
            m_xlApp.Cells(3, 2) = "路灯编号"
            m_xlApp.Cells(3, 3) = "运行状态"
            m_xlApp.Cells(3, 4) = "电流(A)"
            m_xlApp.Cells(3, 5) = "电压(V)"
            m_xlApp.Cells(3, 6) = "功率(W)"
            m_xlApp.Cells(3, 7) = "功率因数"
            m_xlApp.Cells(3, 8) = "开始时间"
            m_xlApp.Cells(3, 9) = "结束时间"

            m_xlApp.Rows(3).Font.Bold = True
            m_xlApp.Rows(3).font.size = 12
            m_xlApp.Rows(3).RowHeight = 30


            m_row = 4
            lamp_id_state(m_statictag)


            With m_xlSheet
                .Range(.Cells(1, 1), .Cells(1, 9)).Merge()
                .Range(.Cells(2, 1), .Cells(2, 3)).Merge()
                .Range(.Cells(2, 4), .Cells(2, 5)).Merge()
                .Range(.Cells(2, 6), .Cells(2, 9)).Merge()


                .Range(.Cells(3, 1), .Cells(3, 1)).ColumnWidth = 5
                .Range(.Cells(3, 2), .Cells(3, 2)).ColumnWidth = 10
                .Range(.Cells(3, 3), .Cells(3, 3)).ColumnWidth = 10
                .Range(.Cells(3, 4), .Cells(3, 4)).ColumnWidth = 10
                .Range(.Cells(3, 5), .Cells(3, 5)).ColumnWidth = 10
                .Range(.Cells(3, 6), .Cells(3, 6)).ColumnWidth = 20
                .Range(.Cells(3, 7), .Cells(3, 7)).ColumnWidth = 20
                .Range(.Cells(3, 8), .Cells(3, 8)).ColumnWidth = 20
                .Range(.Cells(3, 8), .Cells(3, 9)).ColumnWidth = 20
                .Range(.Cells(3, 1), .Cells(m_row - 1, 9)).RowHeight = 20

                .Range(.Cells(1, 1), .Cells(1, 9)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter
                .Range(.Cells(2, 1), .Cells(2, 3)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft
                .Range(.Cells(2, 9), .Cells(2, 9)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight

                .Range(.Cells(3, 1), .Cells(m_row - 1, 8)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter


                .Range(.Cells(3, 1), .Cells(3, 9)).Font.Name = "宋体"
                '设标题为宋体字
                .Range(.Cells(3, 1), .Cells(3, 9)).Font.Bold = "True"
                '标题字体加粗
                .Range(.Cells(1, 1), .Cells(2, 9)).Borders.LineStyle = 0
                .Range(.Cells(3, 1), .Cells(m_row - 1, 9)).Borders.LineStyle = 1
                '设表格边框样式
                .Range(.Cells(2, 1), .Cells(m_row - 1, 9)).Font.Size = 12

                '表中数据的字号

                .PageSetup.CenterFooter = "第 &P 页/共 &N 页"

            End With

        Else
            If Me.m_exceltag = 1 Then  '导出配置状态的日志
                m_xlApp = (New Microsoft.Office.Interop.Excel.Application)

                m_xlBook = m_xlApp.Workbooks().Add
                m_xlSheet = m_xlBook.Worksheets("sheet1")
                m_xlApp.Cells(1, 1) = "设备配置状态日志"
                m_xlApp.Rows(1).RowHeight = 50
                m_xlApp.Rows(1).Font.Size = 18
                m_xlApp.Rows(1).Font.Bold = True


                ' dt.Columns.Add("市", GetType(String))
                m_xlApp.Cells(2, 1) = "单位：" & COMPANY_NAME
                m_xlApp.Cells(2, 2) = ""
                m_xlApp.Cells(2, 3) = ""
                m_xlApp.Cells(2, 4) = ""
                m_xlApp.Cells(2, 5) = ""
                m_xlApp.Rows(2).RowHeight = 30
                m_xlApp.Rows(2).Font.Size = 12

                m_xlApp.Cells(3, 1) = "编号"
                m_xlApp.Cells(3, 2) = "主控箱编号"
                m_xlApp.Cells(3, 3) = "主控箱名称"
                m_xlApp.Cells(3, 4) = "时间"
                m_xlApp.Cells(3, 5) = "配置状态"

                m_xlApp.Rows(3).Font.Bold = True
                m_xlApp.Rows(3).font.size = 12
                m_xlApp.Rows(3).RowHeight = 30


                m_row = 4
                config_state_excel()


                With m_xlSheet
                    .Range(.Cells(1, 1), .Cells(1, 5)).Merge()
                    .Range(.Cells(2, 1), .Cells(2, 5)).Merge()
                 

                    .Range(.Cells(3, 1), .Cells(3, 1)).ColumnWidth = 5
                    .Range(.Cells(3, 2), .Cells(3, 2)).ColumnWidth = 10
                    .Range(.Cells(3, 3), .Cells(3, 3)).ColumnWidth = 15
                    .Range(.Cells(3, 4), .Cells(3, 4)).ColumnWidth = 20
                    .Range(.Cells(3, 5), .Cells(3, 5)).ColumnWidth = 30
                  
                    .Range(.Cells(3, 1), .Cells(m_row - 1, 5)).RowHeight = 20

                    .Range(.Cells(1, 1), .Cells(1, 5)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter
                    .Range(.Cells(2, 1), .Cells(2, 3)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft
                    .Range(.Cells(2, 5), .Cells(2, 5)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight

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

            End If
        End If

    End Sub

    ''' <summary>
    ''' 导出配置状态的EXCEL表
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub config_state_excel()
        Dim i As Integer = 0
        Dim progress_percentage As Integer = 1
        If m_statelist.Count <= 0 Then
            Exit Sub
        End If
        While i < config_statelist.RowCount

            progress_percentage = i * (100 / config_statelist.RowCount)
            If progress_percentage > 100 Then
                progress_percentage = 100
            End If
            Me.BackgroundWorker_on_off.ReportProgress(progress_percentage)

            m_xlApp.Cells(m_row, 1) = "'" & m_statelist(i).id
            m_xlApp.Cells(m_row, 2) = "'" & m_statelist(i).control_box_id
            m_xlApp.Cells(m_row, 3) = "'" & m_statelist(i).control_box_name
            m_xlApp.Cells(m_row, 4) = "'" & m_statelist(i).createtime
            m_xlApp.Cells(m_row, 5) = "'" & m_statelist(i).config_state
            i += 1
            m_row += 1
        End While
    End Sub

    ''' <summary>
    ''' 按区域查询
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub control_box_control_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles control_box_control.Click
        If control_box_control.Checked = True Then
            Me.static_city.Enabled = True
            Me.static_area.Enabled = True
            Me.static_street.Enabled = True
            Me.static_control_box_name.Enabled = True
            Me.static_lamp_type.Enabled = False
            Me.static_lamp_id.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' 按编号查询
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub lamp_id_control_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lamp_id_control.Click
        If lamp_id_control.Checked = True Then
            Me.static_city.Enabled = True
            Me.static_area.Enabled = True
            Me.static_street.Enabled = True
            Me.static_lamp_type.Enabled = True
            Me.static_lamp_id.Enabled = True
        End If
    End Sub

    ''' <summary>
    ''' 选择类型
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub static_lamp_type_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles static_lamp_type.DropDown
        ' Com_inf.Select_type_name(static_control_box_name, static_lamp_type, lamp_type_id)
        Dim rs As New ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim conn As New ADODB.Connection
        Dim type_string As String  '类型名称
        DBOperation.OpenConn(conn)

        msg = ""
        sql = "select * from box_lamptype_view where control_box_name='" & Trim(static_control_box_name.Text) & "'"

        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "Select_type_name", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If

        static_lamp_type.Items.Clear()
        If rs.RecordCount > 0 Then '选择第一个灯的类型编号
            lamp_type_id.Text = rs.Fields("type_id").Value

            While rs.EOF = False
                type_string = rs.Fields("type_id").Value
                If type_string = "31" Then
                    '跳过查询31类型的
                    rs.MoveNext()
                    Continue While
                End If
                static_lamp_type.Items.Add(Trim(rs.Fields("type_string").Value))
                rs.MoveNext()

            End While
        End If

        If static_lamp_type.Items.Count > 0 Then
            static_lamp_type.SelectedIndex = 0
        Else
            static_lamp_type.Text = ""
        End If
        rs.Close()
        rs = Nothing
        conn.Close()
        conn = Nothing

    End Sub
   
    ''' <summary>
    '''   '查询路灯的亮暗记录(文本框显示)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub static_table_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Date_start.Text = "" Then  '开始日期为空
            MsgBox("请选择开始时间", , PROJECT_TITLE_STRING)
            Date_start.Focus()  '光标定位在开始日期
            Exit Sub
        End If
        If Date_end.Text = "" Then  '结束日期为空
            MsgBox("请选择结束时间", , PROJECT_TITLE_STRING)
            Date_end.Focus()  '光标定位在结束日期
            Exit Sub
        End If
        If Trim(Date_start.Text) = Trim(Date_end.Text) Then  '开始时间不应该和结束时间相同
            MsgBox("统计的开始时间与结束时间相同，请选择一个时间区间", , PROJECT_TITLE_STRING)
            Date_start.Focus()
            Exit Sub
        End If

        If Date_start.Value > Date_end.Value Then
            MsgBox("开始时间大于结束时间，请重新选择一个时间区间", , PROJECT_TITLE_STRING)
            Date_start.Focus()
            Exit Sub

        End If
        m_exceltable = 2  '标志为统计报表
        ProgressBar.Visible = True
        m_timestart = Date_start.Value  '开始日期
        m_timeend = Date_end.Value  '结束日期
        m_checktime = 1  '查询进度参数
        m_stringtag = 0  '首次查询标志

        m_controlboxname = Trim(static_control_box_name.Text)
        m_lamptype = Trim(static_lamp_type.Text)

        m_lampid = Trim(static_lamp_id_start.Text) & Trim(static_lamp_id.Text)


        If control_box_control.Checked = True Then
            '按日期及电控箱范围进行查询
            If static_control_box_name.Text = "" Then
                MsgBox("请选择区域", , PROJECT_TITLE_STRING)
                Exit Sub
            End If
            m_statictag = 0
        End If

        If lamp_id_control.Checked = True Then
            If static_control_box_name.Text = "" Then
                MsgBox("请选择区域", , PROJECT_TITLE_STRING)
                Exit Sub
            End If
            If static_lamp_type.Text = "" Then
                MsgBox("请选择灯的类型", , PROJECT_TITLE_STRING)
                Exit Sub
            End If

            If static_lamp_id.Text = "" Then
                MsgBox("请选择灯的编号", , PROJECT_TITLE_STRING)
                Exit Sub
            End If
            m_statictag = 2

        End If

        If Me.BackgroundWorker_on_off.IsBusy = False Then
            Me.BackgroundWorker_on_off.RunWorkerAsync()
        Else
            MsgBox("线程正忙，请稍后重试", , PROJECT_TITLE_STRING)
        End If
    End Sub

    ''' <summary>
    ''' 关闭窗体
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 路灯亮暗信息统计_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If Me.BackgroundWorker_on_off.IsBusy = True Then
            Me.BackgroundWorker_on_off.CancelAsync()
        End If
        g_windowclose = 1
        ProcessKill(m_xlApp, m_xlBook, m_xlSheet)
    End Sub

    ''' <summary>
    ''' 以城市范围查询
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub city_control_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles city_control.Click
        If city_control.Checked = True Then
            Me.static_city.Enabled = True
            Me.static_area.Enabled = False
            Me.static_street.Enabled = False
            Me.static_control_box_name.Enabled = False
            Me.static_lamp_type.Enabled = False
            Me.static_lamp_id.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' 以区域范围查询
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub area_control_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles area_control.Click
        If area_control.Checked = True Then
            Me.static_city.Enabled = True
            Me.static_area.Enabled = True
            Me.static_street.Enabled = False
            Me.static_control_box_name.Enabled = False
            Me.static_lamp_type.Enabled = False
            Me.static_lamp_id.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' 以街道范围查询
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub street_control_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles street_control.Click
        If street_control.Checked Then
            Me.static_city.Enabled = True
            Me.static_area.Enabled = True
            Me.static_street.Enabled = True
            Me.static_control_box_name.Enabled = False
            Me.static_lamp_type.Enabled = False
            Me.static_lamp_id.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' 以灯的类型查询
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub lamp_type_control_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lamp_type_control.Click
        If lamp_type_control.Checked = True Then
            Me.static_city.Enabled = True
            Me.static_area.Enabled = True
            Me.static_street.Enabled = True
            Me.static_control_box_name.Enabled = True
            Me.static_lamp_type.Enabled = True
            Me.static_lamp_id.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' 增加城市名称
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub static_city_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles static_city.DropDown
        Com_inf.Select_city_name(Me.static_city)
    End Sub

    ''' <summary>
    ''' 城市名称改变，其他值随之改变
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub static_city_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles static_city.SelectedIndexChanged
        Com_inf.Select_area_name(Me.static_city, Me.static_area)
        Com_inf.Select_street_name(Me.static_city, Me.static_area, Me.static_street)
        Com_inf.Select_box_name_level(Me.static_city, Me.static_area, Me.static_street, Me.static_control_box_name)
        Com_inf.Select_type_name(Me.static_control_box_name, Me.static_lamp_type, Me.lamp_type_id)
        Com_inf.Select_lamp_id_type(Me.static_control_box_name, Me.static_lamp_type, Me.static_lamp_id, Me.static_lamp_id_start)
    End Sub

    ''' <summary>
    ''' 增加区域名称
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub static_area_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles static_area.DropDown
        Com_inf.Select_area_name(Me.static_city, Me.static_area)
    End Sub

    ''' <summary>
    ''' 区域名称改变，其他值随之改变
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub static_area_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles static_area.SelectedIndexChanged
        Com_inf.Select_street_name(Me.static_city, Me.static_area, Me.static_street)
        Com_inf.Select_box_name_level(Me.static_city, Me.static_area, Me.static_street, Me.static_control_box_name)
        Com_inf.Select_type_name(Me.static_control_box_name, Me.static_lamp_type, Me.lamp_type_id)
        Com_inf.Select_lamp_id_type(Me.static_control_box_name, Me.static_lamp_type, Me.static_lamp_id, Me.static_lamp_id_start)

    End Sub

    ''' <summary>
    ''' 增加街道名称
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub static_street_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles static_street.DropDown
        Com_inf.Select_street_name(Me.static_city, Me.static_area, Me.static_street)

    End Sub

    ''' <summary>
    ''' 街道名称改变，其他值随之改变
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub static_street_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles static_street.SelectedIndexChanged
        Com_inf.Select_box_name_level(Me.static_city, Me.static_area, Me.static_street, Me.static_control_box_name)
        Com_inf.Select_type_name(Me.static_control_box_name, Me.static_lamp_type, Me.lamp_type_id)
        Com_inf.Select_lamp_id_type(Me.static_control_box_name, Me.static_lamp_type, Me.static_lamp_id, Me.static_lamp_id_start)

    End Sub

    ''' <summary>
    ''' 类型改变
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub static_lamp_type_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles static_lamp_type.SelectedIndexChanged
        Com_inf.Select_lamp_id_type(Me.static_control_box_name, Me.static_lamp_type, Me.static_lamp_id, Me.static_lamp_id_start)

    End Sub


    Private Sub box_name_check_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles box_name_check.Click
        If box_name_check.Checked = True Then
            control_box_list.Enabled = True
        End If
    End Sub

    Private Sub all_inf_check_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles all_inf_check.Click
        If all_inf_check.Checked = True Then
            control_box_list.Enabled = False
        End If
    End Sub

    Private Sub control_box_list_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles control_box_list.DropDown
        Com_inf.Select_control_box_all(control_box_list)
    End Sub

    Private Sub date_condition_find_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If date_condition_find.Checked = True Then  '日期查询
            Date_start.Enabled = True '开始日期可用
            Date_end.Enabled = True  '结束日期可用
        End If
    End Sub

    Private Sub no_date_condition_find_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If no_date_condition_find.Checked = True Then  '不用日期查询
            Date_start.Enabled = False '开始日期不可用
            Date_end.Enabled = False  '结束日期不可用
        End If
    End Sub

    Private Sub check_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles check.Click
        Dim CONFIG_SUCCESS As String = "成功"
        Dim CONFIG_FAIL As String = "失败"
        Dim CONFIG_ALL As String = "全部"
        If Me.date_condition_find.Checked = True And Me.box_name_check.Checked = True Then
            '按日期与电控箱名称查询
            If control_box_list.Text = "" Then
                MsgBox("请选择主控箱名称", , PROJECT_TITLE_STRING)
                control_box_list.Focus()
                Exit Sub
            End If
            If config_state.Text = "" Then
                MsgBox("请选择配置状态", , PROJECT_TITLE_STRING)
                config_state.Focus()
                Exit Sub
            End If

            If config_state.Text = CONFIG_SUCCESS Then  '成功
                Me.Config_state_listTableAdapter.FillBy_date_box_succ(Me.ConfigDataSet.config_state_list, Trim(control_box_list.Text), DateTimePickerStart.Value, DateTimePickerEnd.Value)
            Else
                If config_state.Text = CONFIG_FAIL Then '失败
                    Me.Config_state_listTableAdapter.FillBy_date_box_fail(Me.ConfigDataSet.config_state_list, Trim(control_box_list.Text), DateTimePickerStart.Value, DateTimePickerEnd.Value)
                Else '查询全部
                    Me.Config_state_listTableAdapter.Fill_date_box(Me.ConfigDataSet.config_state_list, Trim(control_box_list.Text), DateTimePickerStart.Value, DateTimePickerEnd.Value)
                End If
            End If
        End If

        If Me.date_condition_find.Checked = True And Me.all_inf_check.Checked = True Then
            '按日期查询，查询全部不包括电控箱的名称
            If config_state.Text = "" Then
                MsgBox("请选择配置状态", , PROJECT_TITLE_STRING)
                config_state.Focus()
                Exit Sub
            End If
            If config_state.Text = CONFIG_SUCCESS Then  '成功
                Me.Config_state_listTableAdapter.FillBy_date_all_succ(Me.ConfigDataSet.config_state_list, DateTimePickerStart.Value, DateTimePickerEnd.Value)
            Else
                If config_state.Text = CONFIG_FAIL Then '失败
                    Me.Config_state_listTableAdapter.FillBy_date_all_fail(Me.ConfigDataSet.config_state_list, DateTimePickerStart.Value, DateTimePickerEnd.Value)
                Else '查询全部
                    Me.Config_state_listTableAdapter.FillBy_date_all_all(Me.ConfigDataSet.config_state_list, DateTimePickerStart.Value, DateTimePickerEnd.Value)
                End If
            End If
        End If

        If Me.no_date_condition_find.Checked = True And Me.all_inf_check.Checked = True Then
            '不按日期查询，查询全部不包括电控箱的名称
            If config_state.Text = "" Then
                MsgBox("请选择配置状态", , PROJECT_TITLE_STRING)
                config_state.Focus()
                Exit Sub
            End If
            If config_state.Text = CONFIG_SUCCESS Then  '成功
                Me.Config_state_listTableAdapter.FillBy_nodate_all_succ(Me.ConfigDataSet.config_state_list)
            Else
                If config_state.Text = CONFIG_FAIL Then '失败
                    Me.Config_state_listTableAdapter.FillBy_nodate_all_fail(Me.ConfigDataSet.config_state_list)
                Else '查询全部
                    Me.Config_state_listTableAdapter.FillBy_nodate_all(Me.ConfigDataSet.config_state_list)
                End If
            End If
        End If

        If Me.no_date_condition_find.Checked = True And Me.box_name_check.Checked = True Then
            '不按日期查询，查电控箱的名称
            If control_box_list.Text = "" Then
                MsgBox("请选择主控箱名称", , PROJECT_TITLE_STRING)
                control_box_list.Focus()
                Exit Sub
            End If
            If config_state.Text = "" Then
                MsgBox("请选择配置状态", , PROJECT_TITLE_STRING)
                config_state.Focus()
                Exit Sub
            End If

            If config_state.Text = CONFIG_SUCCESS Then  '成功
                Me.Config_state_listTableAdapter.FillBy_nodate_box_succ(Me.ConfigDataSet.config_state_list, Trim(control_box_list.Text))
            Else
                If config_state.Text = CONFIG_FAIL Then '失败
                    Me.Config_state_listTableAdapter.FillBy_nodate_box_fail(Me.ConfigDataSet.config_state_list, Trim(control_box_list.Text))
                Else '查询全部
                    Me.Config_state_listTableAdapter.FillBy_nodate_box(Me.ConfigDataSet.config_state_list, Trim(control_box_list.Text))
                End If
            End If
        End If
    End Sub

    Private Sub date_condition_find_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles date_condition_find.Click
        If date_condition_find.Checked = True Then
            DateTimePickerStart.Enabled = True
            DateTimePickerEnd.Enabled = True
        End If
    End Sub

    Private Sub no_date_condition_find_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles no_date_condition_find.Click
        If no_date_condition_find.Checked = True Then
            DateTimePickerStart.Enabled = False
            DateTimePickerEnd.Enabled = False
        End If
    End Sub

    Private Sub config_excel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles config_excel.Click
        '将查询出的数据导出到EXCEL表中
        Me.m_exceltag = 1
        '将查询出的所有数据导出的填入到一个列表中
        Dim i As Integer = 0

        Dim inf As configstate_list
        m_statelist.Clear()

        While i < Me.config_statelist.RowCount

            inf.id = config_statelist.Rows(i).Cells("id").Value
            inf.control_box_id = config_statelist.Rows(i).Cells("controlboxid").Value
            inf.control_box_name = config_statelist.Rows(i).Cells("controlboxname").Value
            inf.createtime = config_statelist.Rows(i).Cells("createtime").Value
            inf.config_state = config_statelist.Rows(i).Cells("cofigstate").Value
            m_statelist.Add(inf)
            i += 1

        End While
     
        If Me.BackgroundWorker_on_off.IsBusy = False Then
            Me.BackgroundWorker_on_off.RunWorkerAsync()
        Else
            MsgBox("正在导出数据，请稍候...", , PROJECT_TITLE_STRING)
            Exit Sub
        End If


    End Sub
End Class