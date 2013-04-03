''' <summary>
''' 监测通信正常后，发送单灯取状态命令，查询单灯状态
''' </summary>
''' <remarks></remarks>

Public Class 终端状态查询窗口
    Private Const TIME As Integer = 15  '等待最长时间15秒

    Private m_condition As String   '查询级别
    Private m_condition_inf As String  '查询的对象名称
    Delegate Sub find_status()
    Private m_waittime As Integer    '等待计时
    Private m_rownum As Integer
    Private m_controltime As Date  '发送控制命令时间
    Private m_controllampobj As New control_lamp
    Private m_controlboxname As String  '电控箱名称


    Delegate Sub SetTextControlCallBack(ByVal text As String, ByVal appendOrSet As Boolean, ByVal control As Control)
    Delegate Sub AddDataGridView(ByVal datagridview As System.Windows.Forms.DataGridView)
    Delegate Sub UpdateDataGridView(ByVal datagridview As System.Windows.Forms.DataGridView)
    Delegate Sub SetDataGridView(ByVal datagridview As System.Windows.Forms.DataGridView, ByVal col_name As String, ByVal row As Integer, ByVal text As String)
    Delegate Sub SetStatusStripText(ByVal text As String, ByVal Label_obj As StatusStrip, ByVal name As String)  '设置label的文字

#Region "委托"

    Public Sub SetTextLabelDelegate(ByVal text As String, ByVal label_obj As StatusStrip, ByVal name As String)
        If label_obj.InvokeRequired Then
            Dim setLabelText As SetStatusStripText = New SetStatusStripText(AddressOf SetTextLabelDelegate)
            Me.Invoke(setLabelText, New Object() {text, label_obj, name})
        Else
            label_obj.Items(name).Text = text   '设置状态栏中的文字

        End If
    End Sub

    Public Sub AddDataGridViewDelegate(ByVal datagridview As System.Windows.Forms.DataGridView)

        '如果调用方位于创建该控件线程以外的线程时，需要获取是否要调用Invoke方法
        If (datagridview.InvokeRequired) Then

            Dim AddText As AddDataGridView = New AddDataGridView(AddressOf AddDataGridViewDelegate)
            Me.Invoke(AddText, New Object() {datagridview})
        Else '如果不是线程外的调用时：增加datagridview的行
            datagridview.Rows.Add()
        End If
    End Sub

    Public Sub UpdateDataGridViewDelegate(ByVal datagridview As System.Windows.Forms.DataGridView)

        '如果调用方位于创建该控件线程以外的线程时，需要获取是否要调用Invoke方法
        If (datagridview.InvokeRequired) Then

            Dim UpdateText As UpdateDataGridView = New UpdateDataGridView(AddressOf UpdateDataGridViewDelegate)
            Me.Invoke(UpdateText, New Object() {datagridview})
        Else '如果不是线程外的调用时：增加datagridview的行
            datagridview.Update()
        End If
    End Sub

    Public Sub SetDataGridViewDelegate(ByVal datagridview As System.Windows.Forms.DataGridView, ByVal col_name As String, ByVal row As Integer, ByVal text As String)
        '如果调用方位于创建该控件线程以外的线程时，需要获取是否要调用Invoke方法
        If (datagridview.InvokeRequired) Then

            Dim SetText As SetDataGridView = New SetDataGridView(AddressOf SetDataGridViewDelegate)
            Me.Invoke(SetText, New Object() {datagridview, col_name, row, text})
        Else '如果不是线程外的调用时：设置datagridview的值
            datagridview.Rows(row).Cells(col_name).Value = text

        End If

    End Sub
    Public Sub SetTextDelegate(ByVal text As String, ByVal appendOrSet As Boolean, ByVal control As Control)

        '如果调用方位于创建该控件线程以外的线程时，需要获取是否要调用Invoke方法
        If (control.InvokeRequired) Then

            Dim SetText As SetTextControlCallBack = New SetTextControlCallBack(AddressOf SetTextDelegate)
            Me.Invoke(SetText, New Object() {text, appendOrSet, control})
        Else '如果不是线程外的调用时：设置文本框的值

            If (appendOrSet = True) Then

                If control.Name = Single_inf.Name Then
                    Single_inf.AppendText(text)
                Else
                    control.Text &= text
                End If

            Else
                control.Text = text
            End If

            control.Refresh()

        End If

    End Sub
#End Region

    Private Sub lamp_id_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lamp_id.DropDown
        '为终端编号下拉框增加内容
        Com_inf.Select_lamp_id_type(control_box_name, lamp_type, lamp_id, lamp_id_start)
    End Sub

    Private Sub find_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles find.Click

        '首先将轮询的线程暂停

        'If LoginForm.Property_welcome_win_obj.BackgroundWorker_find_state.IsBusy = True Then
        '    LoginForm.Property_welcome_win_obj.BackgroundWorker_find_state.CancelAsync()
        'End If
        Dim lamp_id_string As String  '灯的9位长度编号

        '将查询列表清空
        state_list.Rows.Clear()
        m_rownum = 0
        Single_inf.Text = ""
        m_waittime = 0
        '设置等待信息
        finding.Text = "正在查询请稍候......"

        '********************在进行状态检测的时候将所有选择控件置为不可用
        box_control.Enabled = False
        ' lamp_type_control.Enabled = False
        lamp_id_control.Enabled = False
        control_box_name.Enabled = False
        lamp_type.Enabled = False
        lamp_id.Enabled = False


        '********************


        If box_control.Checked = True Then '按区域查询
            If control_box_name.Text = "" Then  '区域名称为空
                MsgBox("请选择区域", , PROJECT_TITLE_STRING)
                control_box_name.Focus()
                Exit Sub
            End If
            m_condition = "区域"  '查询级别为区域
            m_condition_inf = Trim(control_box_name.Text)  '区域名称

        End If
        'If lamp_type_control.Checked = True Then  '按类型查询
        '    If control_box_name.Text = "" Then  '区域名称为空
        '        MsgBox("请选择区域", , LoginForm.Property_welcome_win_obj.Property_msg_box_title)
        '        control_box_name.Focus()
        '        Exit Sub
        '    End If
        '    If lamp_type.Text = "" Then  '类型为空
        '        MsgBox("请选择景观灯类型", , LoginForm.Property_welcome_win_obj.Property_msg_box_title)
        '        lamp_type.Focus()
        '        Exit Sub
        '    End If
        '    G_condition = "类型"
        '    G_condition_inf = Trim(control_box_name.Text) '区域名称
        '    G_condition_inf2 = Trim(lamp_type.Text)  '类型名称

        'End If
        If lamp_id_control.Checked = True Then '按灯的编号查询
            If control_box_name.Text = "" Then '区域名称为空
                MsgBox("请选择区域", , PROJECT_TITLE_STRING)
                control_box_name.Focus()
                Exit Sub
            End If
            If lamp_type.Text = "" Then '区域名称为空
                MsgBox("请选择景观灯类型", , PROJECT_TITLE_STRING)
                lamp_type.Focus()
                Exit Sub
            End If
            If lamp_id.Text = "" Then  '灯的编号为空
                MsgBox("请选择景观灯编号", , PROJECT_TITLE_STRING)
                lamp_id.Focus()
                Exit Sub
            End If
            m_condition = "景观灯"
            lamp_id_string = Trim(lamp_id_start.Text) & Trim(lamp_id.Text)
            m_condition_inf = Trim(lamp_id_string)

        End If

        '查询线程
        If Me.BackgroundWorker_state.IsBusy = False Then
            Me.BackgroundWorker_state.RunWorkerAsync()
        Else
            MsgBox("查询正在进行，请稍候......", , PROJECT_TITLE_STRING)
        End If

    End Sub

    Private Sub Add_state_record(ByVal lamp_id As String, ByVal state_inf As String, ByVal dianzu_ad As Integer, ByVal current_ad As Integer)
        '将当前的状态插入到lamp_state_list

        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String

        DBOperation.OpenConn(conn)

        msg = ""
        sql = "select * from lamp_state_list where lamp_id='" & lamp_id & "' and end_tag=" & 0
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "Add_state_record", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If state_inf = "" Then
            Exit Sub
        End If
        If rs.RecordCount <= 0 Then
            '如果没有记录，则增加一条新的记录
            rs.AddNew()
            rs.Fields("lamp_id").Value = lamp_id
            rs.Fields("state").Value = state_inf
            rs.Fields("time_start").Value = Now()
            rs.Fields("time_end").Value = Now()
            rs.Fields("end_tag").Value = 0
            rs.Fields("dianzu_ad").Value = dianzu_ad
            rs.Fields("current_ad").Value = current_ad
            rs.Update()

        Else
            If rs.Fields("state").Value = state_inf Then   '如果灯的状态连续，则修改状态的结束时间
                rs.Fields("time_end").Value = Now()
                rs.Fields("dianzu_ad").Value = dianzu_ad
                rs.Fields("current_ad").Value = current_ad
                rs.Update()
            Else
                rs.Fields("end_tag").Value = 1   '如果灯的状态不连续，则将该条记录标志为结束
                rs.Update()

                rs.AddNew()   '增加新的记录
                rs.Fields("lamp_id").Value = lamp_id
                rs.Fields("state").Value = state_inf
                rs.Fields("time_start").Value = Now()
                rs.Fields("time_end").Value = Now()
                rs.Fields("end_tag").Value = 0
                rs.Fields("dianzu_ad").Value = dianzu_ad
                rs.Fields("current_ad").Value = current_ad
                rs.Update()

            End If

        End If
        rs.Close()
        rs = Nothing
        conn.Close()
        conn = Nothing
        Exit Sub


    End Sub

    ''' <summary>
    ''' 按区域、类型、单灯三个不同的范围进行状态查询
    ''' </summary>
    ''' <remarks></remarks>

    Private Sub find_state_th()
        '按条件进行状态查询

        Dim i As Integer
        Dim rs As New ADODB.Recordset
        Dim sql As String
        Dim msg As String
        Dim control_string As String
        Dim find_state_tag As Boolean  '是否找到记录的标志
        Dim conn As New ADODB.Connection
        DBOperation.OpenConn(conn)

        m_waittime = 0
        i = 1

        If m_condition = "区域" Then
            '按区域进行查询
            '发送查询命令

            msg = ""
            sql = "select * from lamp_street where control_box_name='" & Trim(control_box_name.Text) & "' order by lamp_id"
            rs = DBOperation.SelectSQL(conn, sql, msg)
            While rs.EOF = False

                control_string = m_controllampobj.Set_control_inf(Trim(rs.Fields("lamp_id").Value))  '设置查询命令
                m_controllampobj.Input_db_control(control_string, Trim(rs.Fields("control_box_id").Value), "", 1, -1)   '发送查询命令
                m_controltime = Now  '记录下发送控制命令时间，返回状态的时间必须在此之后，才是该命令查询的状态
                i = 0
                '等待回复信息
                While i < TIME
                    find_state_tag = find_single(Trim(rs.Fields("lamp_id").Value))
                    System.Threading.Thread.Sleep(1000)
                    i += 1
                    If find_state_tag = True Then  '找到返回信息
                        Exit While
                    End If
                End While

                m_waittime = 0 '将计时器置0
                rs.MoveNext()
            End While

            rs.Close()
            rs = Nothing
        Else
            If m_condition = "类型" Then
                '按类型进行查询
                '发送查询命令

                msg = ""
                sql = "select * from lamp_street where control_box_name='" & Trim(control_box_name.Text) & "' and type_string='" & Trim(lamp_type.Text) & "' order by lamp_id"
                rs = DBOperation.SelectSQL(conn, sql, msg)
                While rs.EOF = False

                    control_string = m_controllampobj.Set_control_inf(Trim(rs.Fields("lamp_id").Value))  '设置控制命令

                    m_controllampobj.Input_db_control(control_string, Trim(rs.Fields("control_box_id").Value), "", 1, -1)   '发送控制命令

                    m_controltime = Now  '记录下发送控制命令时间，返回状态的时间必须在此之后，才是该命令查询的状态
                    i = 0
                    '等待回复信息
                    While i < TIME

                        find_state_tag = find_single(Trim(rs.Fields("lamp_id").Value))
                        System.Threading.Thread.Sleep(1000)
                        i += 1
                        If find_state_tag = True Then
                            Exit While
                        End If
                    End While

                    m_waittime = 0 '将计时器置0
                    'find_tag = 0
                    rs.MoveNext()
                End While

                rs.Close()
                rs = Nothing

            Else
                If m_condition = "景观灯" Then
                    '按终端进行查询

                    control_string = m_controllampobj.Set_control_inf(m_condition_inf)  '设置控制命令
                    m_controllampobj.Input_db_control(control_string, Mid(m_condition_inf, 1, 4), "", 1, -1)   '发送控制命令
                    m_controltime = Now  '记录下发送控制命令时间，返回状态的时间必须在此之后，才是该命令查询的状态
                    i = 0
                    ' find_tag = 0
                    '等待回复信息
                    While i < TIME
                        find_state_tag = find_single(m_condition_inf)
                        System.Threading.Thread.Sleep(1000)
                        i += 1
                        If find_state_tag = True Then
                            Exit While
                        End If
                    End While
                Else
                    MsgBox("请选择相应字段进行查询！", , PROJECT_TITLE_STRING)
                    conn.Close()
                    conn = Nothing
                    Exit Sub
                End If
            End If
        End If
        '设置等待信息
        ' find_tag = 0
        SetTextLabelDelegate("查询完毕", Me.StatusStrip1, "finding")
        ' thread_find.Abort()  '终止线程
        'rs.Close()
        'rs = Nothing
        conn.Close()
        conn = Nothing
    End Sub

    Private Function find_single(ByVal lamp_id As String) As Boolean
        Dim rs As ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim control_box_ox As String
        Dim lamp_id_ox As String
        Dim dianzu_ad, current_ad As Double
        Dim control_lamp_obj As New control_lamp
        Dim control_condition_ad As Integer
        Dim control_value_ad_part, control_value_ad_all As Integer
        'Dim lamp_state As Integer  '灯的状态
        Dim rs_lamp As New ADODB.Recordset
        Dim rs_status As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        ' Dim control_condition As String   '判断亮暗的条件 电阻或电流
        Dim ox_str As String
        Dim state_inf As String  '状态
        Dim result_tag, state_tag, power_tag As Integer
        Dim date_tag As Date
        Dim current As Double  '电流安培值
        Dim current_tag As Integer  '四号路电流扩大20倍
        Dim problem As String

        If Mid(lamp_id, 1, 4) = "0004" Then
            current_tag = 20
        Else
            current_tag = 1
        End If
        DBOperation.OpenConn(conn)
        date_tag = Now
        result_tag = 0
        state_tag = 0
        power_tag = 0
        control_lamp_obj.Get_condition()  '获取判断终端状态的标准，是电阻还是电流
        Com_inf.lamp_id_to_detail(lamp_id)
        find_single = False   '初始化为没找到

        If m_waittime >= TIME - 1 Then  '时间超时
            '没有有返回信息
            ' Dim rs_lamp As ADODB.Recordset

            msg = ""
            sql = "update lamp_inf set result=" & 3 & " , power=" & 0 & " ,current_l=" & 0 & " where lamp_id='" & lamp_id & "'"

            DBOperation.ExecuteSQL(conn, sql, msg)

            'sql = "select * from lamp_inf where lamp_id='" & lamp_id & "'"
            'rs_lamp = DBOperation.SelectSQL(conn, sql, msg)
            'If rs_lamp.RecordCount > 0 Then
            '    'lamp_state = rs_lamp.Fields("state").Value
            '    rs_lamp.Fields("result").Value = 3  '标志终端的无返回状态
            '    rs_lamp.Update()

            'End If
            ' Me.BackgroundWorker_state.ReportProgress(1)

            Me.SetTextDelegate(g_controlboxname & " " & g_lamptype & " " & g_lampidstring & "信息返回超时！" & vbCrLf & vbCrLf, True, Single_inf)

            '将超时信息写入数据库
            Me.Add_state_record(lamp_id, LAMP_STATE_NORETURN, -1, -1)
            'If rs_lamp.State = 1 Then
            '    rs_lamp.Close()
            '    rs_lamp = Nothing
            'End If


            '将超时信息输入到故障表中
            'control_lamp_obj.Input_problem(3, lamp_id_tag, lamp_state)
            m_waittime = 0
            conn.Close()
            conn = Nothing
            find_single = False
            Exit Function
        End If

        msg = ""


        ox_str = Com_inf.Dec_to_Bin(Val(Mid(lamp_id, 5, 2)), 5) & Com_inf.Dec_to_Bin(Val(Mid(lamp_id, 7, LAMP_ID_LEN)), 11)  '二进制的终端16位长度的终端编号，前5位为类型号，后11位为灯的独立编号
        ox_str = Com_inf.BIN_to_HEX(ox_str)  '转换成4位长度的十六进制
        If SYSTEM_VISION = 1 Then
            control_box_ox = Dec_to_Hex(Mid(lamp_id, 1, 4), 2)
            lamp_id_ox = control_box_ox & " " & Mid(ox_str, 1, 2) & " " & Mid(ox_str, 3, 2) '终端编号的16进制位数 4位
            sql = "select * from RoadLightStatus where StatusContent like '" & lamp_id_ox & "%' and Createtime>'" & m_controltime & "' and HandlerFlag=" & 0

        Else
            control_box_ox = Dec_to_Hex(Mid(lamp_id, 1, 4), 4)
            lamp_id_ox = Mid(control_box_ox, 3, 2) & " " & Mid(ox_str, 1, 2) & " " & Mid(ox_str, 3, 2)  '终端编号的16进制位数 4位
            sql = "select * from RoadLightStatus where StatusContent like '" & lamp_id_ox & "%' and StautsContent like '%" & Mid(control_box_ox, 1, 2) & "' and Createtime>'" & m_controltime & "' and HandlerFlag=" & 0

        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        'Dim lamp_state_num As Integer
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "find_single", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            find_single = False
            Exit Function
        End If

        state_inf = ""
        While rs.EOF = False

            If Me.BackgroundWorker_state.CancellationPending = True Then
                rs.Close()
                rs = Nothing
                conn.Close()
                conn = Nothing
                Exit Function
            End If
            '有返回的终端状态数据,将其与终端状态表中的状态对比，并将对比结果输入的result字段中

            sql = "select * from lamp_inf where lamp_id='" & lamp_id & "'"
            rs_lamp = DBOperation.SelectSQL(conn, sql, msg)
            If rs_lamp.RecordCount > 0 Then

                '进行亮暗 的比对
                'lamp_state = rs_lamp.Fields("state").Value  '灯的状态

                'Com_inf.Explain_State_String(Trim(rs.Fields("StatusContent").Value))  '分析状态中的电流AD，电阻AD
                dianzu_ad = g_dianzuad  '电阻AD值
                current_ad = g_currentad  '电流AD值

                ''*******演示箱中的特殊设置，其用电阻判断，其他用电流判断
                'If Mid(lamp_id, 1, 6) = "000200" Then
                '    control_condition = control_dianzu_string
                '    control_value_ad = "144"
                '    '********************************************************
                'Else
                '    control_condition = control_lamp_obj.Property_control_condition
                '    '*****将第三类型的灯电流判定值设置为64，其他的不变
                '    If control_condition = control_current_string Then
                '        If Mid(lamp_id, 5, 2) = "03" Then
                '            control_value_ad = 64
                '        Else
                '            control_value_ad = control_lamp_obj.Property_control_value  '判断终端亮暗的控制值，即电阻值或电流值
                '        End If

                '    Else
                '        control_value_ad = control_lamp_obj.Property_control_value  '判断终端亮暗的控制值，即电阻值或电流值

                '    End If

                'End If
                If Mid(lamp_id, 1, 4) <> "0001" Then
                    control_value_ad_all = 2
                    control_value_ad_part = 1
                Else
                    control_value_ad_part = g_controlvaluepart  '判断终端亮暗的控制值，即电阻值或电流值
                    control_value_ad_all = g_controlvalueall  '判断终端亮暗的控制值，即电阻值或电流值

                End If

                control_condition_ad = current_ad
                If rs_lamp.Fields("state").Value <> 0 And rs_lamp.Fields("state").Value <> 1 And rs_lamp.Fields("state").Value <> 3 And rs_lamp.Fields("state").Value <> 4 Then
                    MsgBox("灯的状态信息出错,请检查数据库表lamp_inf", , PROJECT_TITLE_STRING)
                    rs.Close()
                    rs = Nothing
                    rs_lamp.Close()
                    rs_lamp = Nothing
                    Exit Function
                End If
                If control_condition_ad >= control_value_ad_part And control_condition_ad < control_value_ad_all And (rs_lamp.Fields("state").Value = 1 Or rs_lamp.Fields("state").Value = 4) Then
                    '比对成功，灯是亮的,半功率

                    state_inf = LAMP_STATE_ON
                    result_tag = 0
                    state_tag = 1
                    power_tag = 50
                    current = current_ad * 0.0268 * current_tag

                End If
                If control_condition_ad >= control_value_ad_all And (rs_lamp.Fields("state").Value = 1 Or rs_lamp.Fields("state").Value = 4) Then
                    '比对成功，灯是亮的，全功率

                    state_inf = LAMP_STATE_ON
                    result_tag = 0
                    state_tag = 1
                    power_tag = 100
                    current = current_ad * 0.0268 * current_tag

                End If
                If control_condition_ad < control_value_ad_part And (rs_lamp.Fields("state").Value = 1 Or rs_lamp.Fields("state").Value = 4) Then
                    '比对失败，灯是该亮非亮

                    state_inf = LAMP_STATE_PROBLEM_ON
                    result_tag = 1
                    state_tag = -1
                    power_tag = 0
                    current = 0

                End If
                If control_condition_ad >= control_value_ad_part And control_condition_ad < control_value_ad_all And (rs_lamp.Fields("state").Value = 0 Or rs_lamp.Fields("state").Value = 3) Then
                    '比对失败，灯是该暗非暗

                    state_inf = LAMP_STATE_PROBLEM_OFF
                    result_tag = 2
                    state_tag = -1
                    power_tag = 50
                    current = current_ad * 0.0268 * current_tag

                End If
                If control_condition_ad >= control_value_ad_all And (rs_lamp.Fields("state").Value = 0 Or rs_lamp.Fields("state").Value = 3) Then
                    '比对失败，灯是该暗非暗

                    state_inf = LAMP_STATE_PROBLEM_OFF
                    result_tag = 2
                    state_tag = -1
                    power_tag = 100
                    current = current_ad * 0.0268 * current_tag

                End If
                If control_condition_ad < control_value_ad_part And (rs_lamp.Fields("state").Value = 0 Or rs_lamp.Fields("state").Value = 3) Then
                    '比对成功，灯是暗的

                    state_inf = LAMP_STATE_OFF
                    result_tag = 0
                    state_tag = 0
                    power_tag = 0
                    current = current_ad * 0.0268 * current_tag

                End If

                If state_tag = -1 Then
                    sql = "update lamp_inf set result='" & result_tag & "' , date='" & date_tag & "' , current_l='" & current & "', power='" & power_tag & "' where lamp_id='" & lamp_id & "'"
                Else
                    sql = "update lamp_inf set result='" & result_tag & "' , date='" & date_tag & "' , state='" & state_tag & "' , current_l='" & current & "', power='" & power_tag & "' where lamp_id='" & lamp_id & "'"

                End If
                DBOperation.ExecuteSQL(conn, sql, msg)


                '将状态信息记录数据库中
                Me.Add_state_record(lamp_id, state_inf, dianzu_ad, current_ad)

                ' m_controlboxid = Trim(rs_lamp.Fields("control_box_id").Value)
                'lamp_id_bg = Trim(lamp_id)
                'dianzu_bg = 
                'current_bg = 
                ' Me.BackgroundWorker_state.ReportProgress(2)

                Com_inf.lamp_id_to_detail(lamp_id)
                Me.SetTextDelegate("区域：" & g_controlboxname & vbCrLf, True, Single_inf)
                Me.SetTextDelegate("景观灯类型：" & g_lamptype & vbCrLf, True, Single_inf)
                'Me.SetTextDelegate("景观灯编号：" & Com_inf.Property_lamp_id_string & vbCrLf, True, Single_inf)
                Me.SetTextDelegate("景观灯编号：" & Val(Mid(lamp_id, 1, 4)).ToString & "-" & Val(Mid(lamp_id, 5, 2)).ToString & "-" & Val(Mid(lamp_id, 7, LAMP_ID_LEN)).ToString & vbCrLf, True, Single_inf)

                'Me.SetTextDelegate("电阻AD值：" & dianzu_bg & vbCrLf, True, Single_inf)
                Me.SetTextDelegate("电流值(A)：" & current & vbCrLf, True, Single_inf)
                Me.AddDataGridViewDelegate(state_list)

                Me.SetDataGridViewDelegate(state_list, "datagridview_control_box_name", m_rownum, g_controlboxname)
                Me.SetDataGridViewDelegate(state_list, "datagridview_lamp_type", m_rownum, g_lamptype)
                Me.SetDataGridViewDelegate(state_list, "datagridview_lamp_id", m_rownum, Val(Mid(lamp_id, 1, 4)).ToString & "-" & Val(Mid(lamp_id, 5, 2)).ToString & "-" & Val(Mid(lamp_id, 7, LAMP_ID_LEN)).ToString)
                Me.SetDataGridViewDelegate(state_list, "datagridview_dianzu", m_rownum, dianzu_ad)
                Me.SetDataGridViewDelegate(state_list, "datagridview_current", m_rownum, current_ad)


                '将状态信息添加到列表中


                sql = "select * from problem_list where problem_id='" & result_tag & "'"
                rs_status = DBOperation.SelectSQL(conn, sql, msg)
                If rs_status.RecordCount > 0 Then
                    If state_tag = -1 Then
                        state_tag = Trim(rs_lamp.Fields("state").Value)
                    End If
                    If result_tag = 0 And state_tag = 1 Then
                        ' problem_bg = Trim(rs_status.Fields("problem_inf").Value)
                        ' Me.BackgroundWorker_state.ReportProgress(3)
                        Me.SetTextDelegate("状态：" & " 亮 " & vbCrLf & vbCrLf, True, Single_inf)
                        Me.SetDataGridViewDelegate(state_list, "datagridview_state", m_rownum, "亮")

                    Else
                        If result_tag = 0 And state_tag = 0 Then
                            '  problem_bg = Trim(rs_status.Fields("problem_inf").Value)
                            Me.SetTextDelegate("状态：" & " 暗 " & vbCrLf & vbCrLf, True, Single_inf)
                            Me.SetDataGridViewDelegate(state_list, "datagridview_state", m_rownum, "暗 ")
                        Else
                            problem = Trim(rs_status.Fields("problem_inf").Value)
                            Me.SetTextDelegate("状态：" & problem & vbCrLf & vbCrLf, True, Single_inf)
                            Me.SetDataGridViewDelegate(state_list, "datagridview_state", m_rownum, problem)

                        End If

                    End If
                Else
                    Me.SetTextDelegate("未找到当前状态，请确认数据库是否正确！", True, Single_inf)
                    Me.SetDataGridViewDelegate(state_list, "datagridview_state", m_rownum, "状态出错")

                End If

                Me.UpdateDataGridViewDelegate(state_list)
                m_rownum += 1

            Else
                MsgBox("未找到终端的信息，请确定数据库中的终端信息正确！", , PROJECT_TITLE_STRING)
            End If
            find_single = True   '标识设为找到


            '恢复标志位
            '恢复标志位
            sql = "update RoadLightStatus set HandlerFlag=1 where StatusContent like '" & lamp_id_ox & "%' and HandlerFlag=" & 0

            DBOperation.ExecuteSQL(conn, sql, msg)
            rs.MoveNext()
        End While



        If find_single = False Then
            m_waittime += 1
            If m_waittime = 1 Then
                Me.SetTextDelegate("正在接收" & g_controlboxname & " " & g_lamptype & " " & g_lampidstring & "的信息请稍候..." & vbCrLf, True, Single_inf)
            End If
            ' control_center.SetTextControl("还没有接收到" & lamp_id_tag & "信息请稍候..." & vbCrLf, True, Single_inf)
        End If
        If rs_lamp Is Nothing Then
            rs_lamp.Close()
            rs_lamp = Nothing
        End If

        If rs_status Is Nothing Then
            rs_status.Close()
            rs_status = Nothing
        End If

        rs.Close()
        rs = Nothing
        conn.Close()
        conn = Nothing
    End Function

    ''' <summary>
    ''' 终端状态查询的载入函数，初始化下拉框的内容
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 终端状态查询窗口_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' find_tag = 0
        m_rownum = 0
        ' control_dianzu_string = "电阻"
        m_controlboxname = "电流"
        '设置各个窗体的图标
        Me.Icon = New Icon("图片\favicon.ico", 32, 32)
        finding.Text = ""

        'wait_time = 15  '查询的等待时间

        '初始化下拉框
        Com_inf.Select_box_name(control_box_name)
        If control_box_name.Items.Count > 0 Then

            Com_inf.Select_lamp_id_type(control_box_name, lamp_type, lamp_id, lamp_id_start)
        End If
        lamp_id_start.Visible = False
        lamp_type_id.Visible = False
        'state_problem_on = LoginForm.Property_welcome_win_obj.Property_state_problem_on

        'state_problem_off = LoginForm.Property_welcome_win_obj.Property_state_problem_off
        'state_on = LoginForm.Property_welcome_win_obj.Property_state_on
        'state_off = LoginForm.Property_welcome_win_obj.Property_state_off
    End Sub

    Private Sub 终端状态查询窗口_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        If Me.BackgroundWorker_state IsNot Nothing Then
            Me.BackgroundWorker_state.Dispose()
        End If
    End Sub

    Private Sub box_control_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles box_control.Click
        If box_control.Checked = True Then '按电控箱进行查询
            control_box_name.Enabled = True '电控箱名称可用
            lamp_type.Enabled = False  '景观灯类型不可用
            lamp_id.Enabled = False  '景观灯编号不可用
        End If
    End Sub

    Private Sub lamp_id_control_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lamp_id_control.Click

        If lamp_id_control.Checked = True Then '按终端编号进行查询
            control_box_name.Enabled = True '电控箱编号可用
            lamp_type.Enabled = True  '景观灯类型可用
            lamp_id.Enabled = True '景观灯编号可用
        End If
    End Sub
    ''' <summary>
    ''' 单灯状态查询，首先检查一下通信是否正常，正常则进行状态检查，如阻塞，提示稍后重试
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BackgroundWorker_state_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker_state.DoWork
        '查询状态前先检测通信是否正常
        Dim find_state_tag As Boolean  '通信是否正常标志位

        Me.BackgroundWorker_state.ReportProgress(11)  '显示查询通信是否正常的进度条
        find_state_tag = state_read_fun()  '检测通信函数

        If find_state_tag = True Then
            Me.BackgroundWorker_state.ReportProgress(12)  '隐藏通信是否正常的进度条
            SetTextLabelDelegate("通信正常,正在查询状态请稍候...", Me.StatusStrip1, "finding")
            find_state_th()   '轮询状态
        Else
            Me.BackgroundWorker_state.ReportProgress(12) '隐藏通信是否正常的进度条
            SetTextLabelDelegate("通信阻塞,请稍后重试", Me.StatusStrip1, "finding")
        End If

    End Sub
    ''' <summary>
    ''' 检测当前通信是否正常
    ''' </summary>
    ''' <returns>返回true为正常，false为不正常</returns>
    ''' <remarks></remarks>

    Private Function state_read_fun() As Boolean
        Dim i As Integer
        Dim rs As New ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim conn As New ADODB.Connection
        Dim control_string As String  '查询状态的字符串
        Dim control_time As Date '记录下发送控制命令时间, 返回状态的时间必须在此之后, 才是该命令查询的状态
        Dim find_num As Integer
        Dim find_state_tag As Boolean   '通信是否正常的标志位
        Dim box_id_hex As String

        DBOperation.OpenConn(conn)  '打开数据库链接
        control_time = Now  '记录下发送控制命令时间，返回状态的时间必须在此之后，才是该命令查询的状态
        find_num = 1
        msg = ""
        box_id_hex = ""

        ' 检查当前路段0号节点的状态
        sql = "select * from control_box where control_box_name='" & Trim(control_box_name.Text) & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)

        If rs Is Nothing Then  '数据库查询出错
            MsgBox(MSG_ERROR_STRING & "state_read_fun", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            state_read_fun = False
            Exit Function
        End If
        If rs.RecordCount <= 0 Then
            MsgBox("不存在区域" & m_controlboxname)
            rs.Close()
            rs = Nothing
            conn.Close()
            conn = Nothing
            state_read_fun = False
            Exit Function
        End If
        '获取两位的十六进制电控箱编号
        If SYSTEM_VISION = 1 Then
            control_string = Dec_to_Hex(Trim(rs.Fields("control_box_id").Value), 2) & " 00 00 20 11 64"  '设置查询命令
        Else

            box_id_hex = Dec_to_Hex(Trim(rs.Fields("control_box_id").Value), 4)   '设置查询命令
            control_string = Mid(box_id_hex, 3, 2) & " 00 00 20 11 64 " & Mid(box_id_hex, 1, 2)
        End If
        m_controllampobj.Input_db_control(control_string, Trim(rs.Fields("control_box_id").Value), "", 1, -1)   '发送查询命令

        '提示正在获取的灯的信息
        SetTextLabelDelegate("正在检测通信是否正常,请稍候...", Me.StatusStrip1, "finding")

        i = 0

        '等待回复信息

        While i <= TIME
            find_state_tag = find_communication_state(Mid(control_string, 1, 8), control_time, i)  '查找标志终端的状态返回
            System.Threading.Thread.Sleep(1000)  '线程休眠1秒
            i += 1
            If find_state_tag = True Then    '如果找到了当前灯的返回值
                state_read_fun = True  '设置标志位
                Exit While
            End If

        End While
        If i > TIME Then  '超时
            '没有有返回信息
            SetTextLabelDelegate("通信阻塞，请稍后重试！", Me.StatusStrip1, "finding")
            MsgBox("通信阻塞，请稍后重试！", , PROJECT_TITLE_STRING)
            state_read_fun = False
        End If

        rs.Close()
        rs = Nothing
        conn.Close()
        conn = Nothing

    End Function


    ''' <summary>
    ''' 检测某一路段0号节点的状态
    ''' </summary>
    ''' <param name="lamp_id_tag">0号节点的编号</param>
    ''' <param name="control_time">发送查询的时间</param>
    ''' <param name="progress_num">查询的进度显示</param>
    ''' <returns>true表示0号无返回值，通信不正常，1表示0号有返回值，通信正常</returns>
    ''' <remarks></remarks>

    Private Function find_communication_state(ByVal lamp_id_tag As String, ByVal control_time As Date, ByVal progress_num As Integer) As Boolean


        Dim msg As String
        Dim sql As String
        Dim conn As New ADODB.Connection
        Dim rs As New ADODB.Recordset
        DBOperation.OpenConn(conn)

        'If progress_num > 9 Then  '判断时间，超时
        '    '没有有返回信息

        '    ' Me.SetTextDelegate("通信不正常，请稍后重试！" & Now & vbCrLf & vbCrLf, True, Me.Doing_now_text)
        '    SetTextLabelDelegate("通信不正常，请稍后重试！", Me.StatusStrip1, "finding")
        '    MsgBox("通信不正常，请稍后重试！", , LoginForm.Property_welcome_win_obj.Property_msg_box_title)
        '    '将超时信息输入到故障表中
        '    '判断故障列表中是否存在该终端的该故障


        '    'time_wait = 0  '轮询的等待时间置为0
        '    conn.Close()
        '    conn = Nothing
        '    find_state = False
        '    Exit Function
        'End If
        msg = ""
        '对比终端状态返回值与操作值 
        sql = "select * from RoadLightStatus where StatusContent like '" & lamp_id_tag & "%' and HandlerFlag=" & 0 & " and Createtime>'" & control_time & "' order by Createtime "

        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs.RecordCount <= 0 Then
            find_communication_state = False   '没有返回植
            Me.BackgroundWorker_state.ReportProgress(progress_num)  '通信检测进度
        Else
            find_communication_state = True  '有返回值
            '有标志为0的，则将其置1
            rs.Fields("HandlerFlag").Value = 1
            rs.Update()

        End If


        rs.Close()
        rs = Nothing
        conn.Close()
        conn = Nothing

    End Function

    ''' <summary>
    ''' 关闭窗体
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 终端状态查询窗口_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If Me.BackgroundWorker_state.IsBusy = True Then '关闭窗体时如果查询仍在进行，则停止查询进程
            Me.BackgroundWorker_state.CancelAsync()
        End If
        g_windowclose = 1
    End Sub

    'Private Sub lamp_type_control_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If lamp_type_control.Checked = True Then
    '        control_box_name.Enabled = True
    '        lamp_type.Enabled = True
    '        lamp_id.Enabled = False
    '    End If
    'End Sub

    'Private Sub control_box_name_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles control_box_name.SelectedIndexChanged
    '    Com_inf.Select_type_name(control_box_name, lamp_type, lamp_type_id)
    '    If lamp_type.Items.Count > 0 Then
    '        Com_inf.Select_lamp_id_type(control_box_name, lamp_type, lamp_id, lamp_id_start)
    '    End If
    '    If box_control.Checked = True Then
    '        G_condition_inf = Trim(control_box_name.Text)  '电控箱名称
    '    End If
    '    If lamp_type_control.Checked = True Then
    '        G_condition_inf = Trim(control_box_name.Text)  '电控箱名称
    '        G_condition_inf2 = Trim(lamp_type.Text)  '灯的类型
    '    End If

    '    If lamp_id_control.Checked = True Then
    '        G_condition_inf = Trim(lamp_id.Text)  '灯的编号
    '    End If
    '    control_box_name_string = Trim(control_box_name.Text)

    '    '  Me.lamp_id_bg
    'End Sub

    Private Sub control_box_name_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles control_box_name.DropDown
        '增加电控箱中的下拉列表内容
        Com_inf.Select_box_name(control_box_name)
        m_controlboxname = Trim(control_box_name.Text)

    End Sub

    Private Sub lamp_type_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lamp_type.DropDown
        '增加景观灯类型的下拉列表内容
        Com_inf.Select_type_name(control_box_name, lamp_type, lamp_type_id)
    End Sub

    Private Sub lamp_type_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lamp_type.SelectedIndexChanged
        If box_control.Checked = True Then
            m_condition_inf = Trim(control_box_name.Text)  '电控箱名称
        End If
        'If lamp_type_control.Checked = True Then
        '    G_condition_inf = Trim(control_box_name.Text)  '电控箱名称
        '    G_condition_inf2 = Trim(lamp_type.Text)  '灯的类型
        'End If

        If lamp_id_control.Checked = True Then
            m_condition_inf = Trim(lamp_id.Text)  '灯的编号
        End If
        If lamp_type.Items.Count > 0 Then
            Com_inf.Select_lamp_id_type(control_box_name, lamp_type, lamp_id, lamp_id_start)
        End If

    End Sub

    Private Sub lamp_id_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lamp_id.SelectedIndexChanged
        If box_control.Checked = True Then
            m_condition_inf = Trim(control_box_name.Text)  '电控箱名称
        End If
        'If lamp_type_control.Checked = True Then
        '    G_condition_inf = Trim(control_box_name.Text)  '电控箱名称
        '    G_condition_inf2 = Trim(lamp_type.Text)  '灯的类型
        'End If

        If lamp_id_control.Checked = True Then
            m_condition_inf = Trim(lamp_id.Text)  '灯的编号
        End If
    End Sub

    Private Sub BackgroundWorker_state_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker_state.ProgressChanged
        If Me.BackgroundWorker_state.CancellationPending = True Then
            Exit Sub
        End If
        If e.ProgressPercentage = 11 Then
            Me.ToolStripProgressBar_check_communication.Visible = True
        Else
            If e.ProgressPercentage = 12 Then
                Me.ToolStripProgressBar_check_communication.Visible = False
            Else
                If e.ProgressPercentage <= 10 Then
                    Me.ToolStripProgressBar_check_communication.Value = e.ProgressPercentage * 10
                Else
                    Me.ToolStripProgressBar_check_communication.Value = 100

                End If

            End If

        End If
    End Sub

    Private Sub BackgroundWorker_state_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker_state.RunWorkerCompleted
        '********************在进行状态检测完成的时候将所有选择控件置为可用
        box_control.Enabled = True
        'lamp_type_control.Enabled = True
        lamp_id_control.Enabled = True
        control_box_name.Enabled = True
        lamp_type.Enabled = True
        lamp_id.Enabled = True


        '********************


    End Sub
End Class