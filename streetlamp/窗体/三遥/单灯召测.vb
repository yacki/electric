Public Class 单灯召测
    Public m_checklist As New ArrayList  '存放选中的主控箱名称
    Private m_check As Boolean = False '设置标志，防止死循环
    Private m_rowid As Integer
    Private m_controlboxname As String
    Delegate Sub SetControlBoxList(ByVal Lamp_State As Windows.Forms.DataGridView, ByVal lampid As String)

    Private Sub 单灯召测_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim controlboxobj As New control_box
        'controlboxobj.set_controlbox_list(tv_yaoce_controlbox) '主控箱信息列表
        controlboxobj.Dispose()
        Me.Icon = New Icon("图片\favicon.ico", 50, 50)
    End Sub

    ''' <summary>
    ''' 将选定的主控箱名称增加到右边的列表中
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_add_boxname_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_add_boxname.Click
        m_checklist.Clear() '清除所有选中的项目
        Dim tnRet As New TreeNode
        For Each treenode As TreeNode In tv_yaoce_controlbox.Nodes
            Com_inf.FindNode(treenode, m_checklist)
        Next
        If m_checklist.Count = 0 Then
            MsgBox("请选择需设置的主控箱名称", , PROJECT_TITLE_STRING)
            Exit Sub
        End If

        '将arraylist中的主控箱名称，显示到右边的列表中
        Com_inf.Addcontrolbox_to_Datagridview(m_checklist, dgv_yaoce_list, "yaoce_boxname")

    End Sub
    ''' <summary>
    ''' 删除被选中的主控箱
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_del_boxname_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_del_boxname.Click
        Dim i As Integer = 0
        Dim boxlist As New ArrayList

        While i < dgv_yaoce_list.RowCount
            If dgv_yaoce_list.Rows(i).Cells("checkid").Value = 0 Then
                boxlist.Add(Trim(dgv_yaoce_list.Rows(i).Cells("yaoce_boxname").Value))

            End If
            i += 1
        End While
        dgv_yaoce_list.Rows.Clear()
        i = 0
        While i < boxlist.Count
            dgv_yaoce_list.Rows.Add()
            dgv_yaoce_list.Rows(i).Cells("yaoce_boxname").Value = boxlist(i)
            i += 1
        End While

        '重新载入m_checklist
        i = 0
        m_checklist.Clear()
        While i < Me.dgv_yaoce_list.Rows.Count
            m_checklist.Add(Trim(Me.dgv_yaoce_list.Rows(i).Cells("yaoce_boxname").Value))
            i += 1
        End While


    End Sub


    Private Sub bt_yaoce_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_yaoce.Click
        dgv_lamp_state_list.Rows.Clear()

        If Me.BackgroundWorker_yaoce.IsBusy = False Then
            Me.BackgroundWorker_yaoce.RunWorkerAsync()

            '招测的时候有些按钮变灰
            bt_add_boxname.Enabled = False
            bt_del_boxname.Enabled = False
            bt_yaoce.Enabled = False
        Else
            MsgBox("召测正在进行,请稍后重试！", , PROJECT_TITLE_STRING)

        End If
    End Sub


    Private Sub clear_text_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles clear_text.Click
        rtb_inf.Text = ""
    End Sub


    Private Sub tv_yaoce_controlbox_AfterCheck(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tv_yaoce_controlbox.AfterCheck
        Dim controlboxobj As New control_box
        If m_check = False Then
            m_check = controlboxobj.setChild(e.Node)
        End If

        controlboxobj.setparent(e.Node)
        m_check = False
    End Sub


    Private Sub BackgroundWorker_yaoce_DoWork(sender As System.Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker_yaoce.DoWork
        get_lampinf()

    End Sub



    Public Sub get_lampinf()    '插入整点区间命令
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim sql_return As String
        Dim rs_return As New ADODB.Recordset
        Dim msg As String
        Dim rs As New ADODB.Recordset
        Dim m As Integer = 0
        Dim imei As String
        Dim inter_count As Integer = 0
        Dim inter_command As String = ""
        Dim time As DateTime = Now
        Dim box_id As String
        Dim controllampobj As New control_lamp
        Dim state() As String
        Dim lamp_protocle_type As String '单灯的协议类型1，2,6
        Dim qujiannum As Integer
        Dim lampid As String
        Dim controlboxid, controlboxname As String
        Dim controlboxobj As New control_box
        Dim i As Integer = 0
        msg = ""
        m_rowid = 0 '将列表中的编号初始化为0
        If m_checklist.Count = 0 Then
            MsgBox("请选择遥测主控箱", , PROJECT_TITLE_STRING)
            Exit Sub
        End If
        Dim boxname(m_checklist.Count - 1) As String
        m_checklist.CopyTo(boxname)
        DBOperation.OpenConn(conn)
        While i < m_checklist.Count
            If Me.BackgroundWorker_yaoce.CancellationPending = False Then
                sql = "SELECT B.control_box_id AS control_box_id,B.inter_count AS inter_count,A.IMEI AS IMEI,A.control_box_name AS control_box_name FROM control_box AS B LEFT JOIN Box_IMEI AS A ON B.control_box_id=A.control_box_id where A.control_box_name='" & boxname(i) & "'"
                rs = DBOperation.SelectSQL(conn, sql, msg)
                If rs Is Nothing Then
                    MsgBox(MSG_ERROR_STRING, , PROJECT_TITLE_STRING)
                    conn.Close()
                    conn = Nothing
                    Exit Sub
                End If
                If rs.RecordCount > 0 Then
                    controlboxid = Trim(rs.Fields("control_box_id").Value)
                    controlboxname = Trim(rs.Fields("control_box_name").Value)
                    m_controlboxname = controlboxname
                    If IsDBNull(rs.Fields("inter_count").Value) = False Then
                        If controlboxobj.get_communication(controlboxname) = False Then
                            Me.BackgroundWorker_yaoce.ReportProgress(33)
                            System.Threading.Thread.Sleep(1000)
                            'GoTo finish
                        Else
                            sql = "SELECT B.control_box_id AS control_box_id,B.inter_count AS inter_count,A.IMEI AS IMEI FROM control_box AS B LEFT JOIN Box_IMEI AS A ON B.control_box_id=A.control_box_id WHERE B.control_box_id=" & controlboxid & ""
                            rs = DBOperation.SelectSQL(conn, sql, msg)
                            If rs Is Nothing Then
                                MsgBox(MSG_ERROR_STRING, , PROJECT_TITLE_STRING)
                                conn.Close()
                                conn = Nothing
                            End If
                            If rs.RecordCount > 0 Then
                                '有单灯数据
                                While rs.EOF = False
                                    box_id = Trim(rs.Fields("control_box_id").Value)
                                    box_id = Com_inf.Dec_to_Hex(box_id, 4)
                                    box_id = Mid(box_id, 1, 2) & " " & Mid(box_id, 3, 2)
                                    imei = Trim(rs.Fields("IMEI").Value)
                                    If IsDBNull(rs.Fields("inter_count").Value) = False Then
                                        inter_count = Trim(rs.Fields("inter_count").Value)
                                        Dim j As Integer = 1
                                        While (j <= inter_count)
                                            inter_command = box_id + " " + Com_inf.Dec_to_Hex(j, 2)
                                            qujiannum = controllampobj.get_qujian(Trim(rs.Fields("control_box_id").Value), j)
                                            sql = "INSERT INTO TimeControl(RoadIMEI,CMDType,CMDContent,CreateTime,HandlerFlag) values('" & imei & "', '158','" & inter_command & "','" & Now & "','" & CONTROL_BOX_TYPE2_FLAG & "')"
                                            DBOperation.ExecuteSQL(conn, sql, msg)
                                            m = 0
                                            While m < 20
                                                sql_return = "SELECT * FROM RoadLightStatus where PackType=25 AND HandlerFlag=2 and Left(StatusContent,2)= '" & Mid(box_id, 4, 2) & "' and Right(StatusContent,2)='" & Mid(box_id, 1, 2) & "' and Createtime>'" & time & "'"
                                                rs_return = DBOperation.SelectSQL(conn, sql_return, msg)
                                                If rs_return Is Nothing Then
                                                    MsgBox(MSG_ERROR_STRING, , PROJECT_TITLE_STRING)
                                                    conn.Close()
                                                    conn = Nothing
                                                End If
                                                If rs_return.RecordCount > 0 Then
                                                    '  g_welcomewinobj.SetTextLabelDelegate("获取路灯数据.....", rtb_inf, "circle_string")

                                                    '查找当前数据库的所有灯的记录，按上传数据进行数据分析

                                                    While rs_return.EOF = False
                                                        If g_welcomewinobj.BackgroundWorkergetlampdata.CancellationPending = True Then
                                                            GoTo finish
                                                        End If
                                                        '2011年10月17日将单灯分为不同的协议类型
                                                        state = Trim(rs_return.Fields("StatusContent").Value).Split(" ")
                                                        lampid = Com_inf.getstate_lampid(state)  '获取单灯编号
                                                        Dim t As Integer = 0
                                                        'Dim short_id As String
                                                        lamp_protocle_type = Com_inf.getlampprotocletype(g_lampidstring)
                                                        If lamp_protocle_type = "-1" Then
                                                            '表示软件中未添加该灯号
                                                            '去除改状态
                                                            sql = "update RoadLightStatus set HandlerFlag=1 where id=" & rs_return.Fields("id").Value
                                                            DBOperation.ExecuteSQL(conn, sql, msg)
                                                            GoTo next1

                                                        End If
                                                        If lamp_protocle_type = "1" Then

                                                            If state.Length <> 7 Then
                                                                '上传的状态长度与单灯的类型不符合
                                                                g_welcomewinobj.SetTextDelegate(g_lampidstring & "节点版本设置为" & lamp_protocle_type & "与上传状态不符 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, True, rtb_inf)

                                                                GoTo finish
                                                            End If
                                                            Com_inf.Explain_State_String(state)  '解析状态字符串的各个含义
                                                            controllampobj.GetCompareState(Trim(rs_return.Fields("StatusContent").Value), rs_return.Fields("id").Value)  '获取路灯的运行状态

                                                        Else
                                                            If lamp_protocle_type = "2" Then
                                                                If state.Length <> 7 Then
                                                                    '上传的状态长度与单灯的类型不符合
                                                                    g_welcomewinobj.SetTextDelegate(g_lampidstring & "节点版本设置为" & lamp_protocle_type & "与上传状态不符 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, True, rtb_inf)

                                                                    GoTo finish
                                                                End If
                                                                Com_inf.Explain_State_String_AD2(state)  '解析状态字符串的各个含义
                                                                controllampobj.GetCompareState_AD2(Trim(rs_return.Fields("StatusContent").Value), rs_return.Fields("id").Value)  '获取路灯的运行状态
                                                            Else
                                                                If state.Length <> 10 Then
                                                                    '上传的状态长度与单灯的类型不符合
                                                                    g_welcomewinobj.SetTextDelegate(g_lampidstring & "节点版本设置为" & lamp_protocle_type & "与上传状态不符 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, True, rtb_inf)

                                                                    GoTo finish
                                                                End If
                                                                While t < 1

                                                                    '2012年5月24日增加五字节的单灯协议,单灯的格式为两字节路段号，两字节节点号，六个字节的单灯状态
                                                                    Com_inf.Explain_State_String_AD6(state, t) '解析状态字符串的各个含义
                                                                    controllampobj.GetCompareState_AD6(Trim(rs_return.Fields("StatusContent").Value), rs_return.Fields("id").Value)  '获取路灯的运行状态
                                                                    t += 1
                                                                    'short_id = Mid(g_lampidstring, 7, LAMP_ID_LEN) + 1

                                                                    'While short_id.Length < LAMP_ID_LEN
                                                                    '    short_id = "0" & short_id
                                                                    'End While
                                                                    'g_lampidstring = Mid(g_lampidstring, 1, 6) & short_id
                                                                    SetControlBoxListDelegate(dgv_lamp_state_list, lampid)
                                                                End While
                                                            End If
                                                        End If
                                                        qujiannum -= 1
                                                        If qujiannum = 0 Then
                                                            rs_return.MoveNext()
                                                            If rs_return.EOF = True Then
                                                                g_welcomewinobj.SetTextDelegate("主控箱：" & Trim(rs.Fields("control_box_id").Value) & "区间" & j & " 数据采集完毕" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, True, rtb_inf)
                                                                GoTo next1
                                                            End If
                                                        Else
                                                            rs_return.MoveNext()
                                                        End If
                                                    End While
                                                End If
                                                m += 1
                                                g_welcomewinobj.SetTextDelegate("主控箱：" & Trim(rs.Fields("control_box_id").Value) & "区间" & j & " 数据采集" & m & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, True, rtb_inf)

                                                System.Threading.Thread.Sleep(1000)
                                            End While
next1:
                                            If m > 20 Then
                                                g_welcomewinobj.SetTextDelegate("主控箱：" & Trim(rs.Fields("control_box_id").Value) & "区间" & j & " 数据采集超时 " & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, True, rtb_inf)

                                            End If
                                            j += 1
                                        End While
                                    End If

                                    rs.MoveNext()
                                End While
                            End If
                            g_welcomewinobj.SetTextDelegate("单灯数据采集完毕！ " & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, True, rtb_inf)

                        End If
                    End If

                End If
            End If
            i += 1
        End While

finish:

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        If rs_return.State = 1 Then
            rs_return.Close()
            rs_return = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub


    '    ''' <summary>
    '    ''' 分析底层上传灯的状态数据处理函数，将当前的状态插入到lamp_state_list表中
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    Public Sub find_total_state_fun(time)
    '        Dim rs_find_state As New ADODB.Recordset
    '        Dim msg As String
    '        Dim sql As String
    '        Dim conn As New ADODB.Connection
    '        Dim state_id As Integer
    '        Dim sql_state As String  '将灯的状态记录到lamp_state_record 表中的sql语句
    '        Dim lamp_protocle_type As String '单灯的协议类型1，2,6
    '        Dim state() As String
    '        Dim controlboxobj As New control_lamp

    '        If DBOperation.OpenConn(conn) = False Then
    '            Exit Sub
    '        End If
    '        '连接数据库
    '        msg = ""
    '        sql_state = ""
    '        state_id = 1
    '        '查询30分钟之间是否有没被分析过的数据,批量上传的数据HandlerFlag标志为2
    '        ' sql = "select * from RoadLightStatus where Createtime > DateAdd(n,-30,'" & Now() & "') and HandlerFlag=" & 2 & " order by ID"  '没有返回状态
    '        sql = "select * from RoadLightStatus nolock where Createtime > '" & time & "' and HandlerFlag=" & 2 & " AND PackType=25  order by ID"  '没有返回状态

    '        rs_find_state = DBOperation.SelectSQL(conn, sql, msg)
    '        If rs_find_state Is Nothing Then  '数据库连接失败
    '            welcome_win.SetTextDelegate(MSG_ERROR_STRING & "find_total_state_fun" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_inf)
    '            conn.Close()
    '            conn = Nothing
    '            Exit Sub
    '        End If
    '        If rs_find_state.RecordCount <= 0 Then  '没有数据则返回
    '            GoTo finish
    '        End If
    '        While rs_find_state.EOF = False
    '            If Me.BackgroundWorker_yaoce.CancellationPending = True Then
    '                GoTo finish
    '            End If
    '            '2011年10月17日将单灯分为不同的协议类型
    '            state = Trim(rs_find_state.Fields("StatusContent").Value).Split(" ")
    '            Com_inf.getstate_lampid(state)  '获取单灯编号
    '            Dim i As Integer = 0
    '            Dim short_id As String
    '            lamp_protocle_type = Com_inf.getlampprotocletype(g_lampidstring)
    '            If lamp_protocle_type = "-1" Then
    '                '表示软件中未添加该灯号
    '                '去除改状态
    '                sql = "update RoadLightStatus set HandlerFlag=1 where id=" & rs_find_state.Fields("id").Value
    '                DBOperation.ExecuteSQL(conn, sql, msg)
    '                GoTo next1

    '            End If
    '            If lamp_protocle_type = "1" Then

    '                If state.Length <> 7 Then
    '                    '上传的状态长度与单灯的类型不符合
    '                    welcome_win.SetTextDelegate(g_lampidstring & "节点版本设置为" & lamp_protocle_type & "与上传状态不符 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, True, rtb_inf)

    '                    GoTo finish
    '                End If
    '                Com_inf.Explain_State_String(state)  '解析状态字符串的各个含义
    '                controlboxobj.GetCompareState(Trim(rs_find_state.Fields("StatusContent").Value), rs_find_state.Fields("id").Value)  '获取路灯的运行状态

    '            Else
    '                If lamp_protocle_type = "2" Then
    '                    If state.Length <> 7 Then
    '                        '上传的状态长度与单灯的类型不符合
    '                        welcome_win.SetTextDelegate(g_lampidstring & "节点版本设置为" & lamp_protocle_type & "与上传状态不符 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, True, rtb_inf)

    '                        GoTo finish
    '                    End If
    '                    Com_inf.Explain_State_String_AD2(state)  '解析状态字符串的各个含义
    '                    controlboxobj.GetCompareState_AD2(Trim(rs_find_state.Fields("StatusContent").Value), rs_find_state.Fields("id").Value)  '获取路灯的运行状态
    '                Else
    '                    If state.Length <> 10 Then
    '                        '上传的状态长度与单灯的类型不符合
    '                        welcome_win.SetTextDelegate(g_lampidstring & "节点版本设置为" & lamp_protocle_type & "与上传状态不符 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, True, rtb_inf)

    '                        GoTo finish
    '                    End If
    '                    While i < 1

    '                        '2012年5月24日增加五字节的单灯协议,单灯的格式为两字节路段号，两字节节点号，六个字节的单灯状态
    '                        Com_inf.Explain_State_String_AD6(state, i) '解析状态字符串的各个含义
    '                        controlboxobj.GetCompareState_AD6(Trim(rs_find_state.Fields("StatusContent").Value), rs_find_state.Fields("id").Value)  '获取路灯的运行状态



    '                        SetControlBoxListDelegate(dgv_lamp_state_list, lampid)
    '                        i += 1
    '                        short_id = Mid(g_lampidstring, 7, LAMP_ID_LEN) + 1

    '                        While short_id.Length < LAMP_ID_LEN
    '                            short_id = "0" & short_id
    '                        End While
    '                        g_lampidstring = Mid(g_lampidstring, 1, 6) & short_id

    '                    End While


    '                End If

    '            End If

    'next1:
    '            rs_find_state.MoveNext()
    '        End While

    'finish:
    '        'Me.BackgroundWorker_find_state.ReportProgress(1) '刷新故障报警
    '        'area_content_list_all(m_checkcondition) '刷新右侧列表


    '        If rs_find_state.State = 1 Then
    '            rs_find_state.Close()
    '            rs_find_state = Nothing
    '        End If
    '        conn.Close()
    '        conn = Nothing

    '    End Sub



    Public Sub SetControlBoxListDelegate(ByVal Lamp_State As Windows.Forms.DataGridView, ByVal lampid As String)
        If Lamp_State.InvokeRequired Then
            Dim stateobj As SetControlBoxList = New SetControlBoxList(AddressOf SetControlBoxListDelegate)
            Me.Invoke(stateobj, New Object() {Lamp_State, lampid})
        Else
            '右边的路灯统计信息
            Dim probleminf As String = ""
            Dim lampkind As String = ""
            Dim lampstate As String = ""  '灯的开关情况，div_time_id列>=8为开，<8的为关
            Dim rs As New ADODB.Recordset
            Dim conn As New ADODB.Connection
            Dim msg As String
            Dim sql As String
            Dim controllamp As New control_lamp
            msg = ""
            sql = "SELECT B.control_box_name AS control_box_name,A.* FROM lamp_inf A JOIN control_box B ON A.control_box_id=B.control_box_id WHERE A.lamp_id=" & g_lampidstring & ""
            DBOperation.OpenConn(conn)
            rs = DBOperation.SelectSQL(conn, sql, msg)
            Dim row As Integer
            row = dgv_lamp_state_list.RowCount
            dgv_lamp_state_list.Rows.Add()
            While row < Me.dgv_lamp_state_list.RowCount
                If rs.Fields("jiechuqi_id").Value IsNot System.DBNull.Value Then
                    lampkind = Trim(rs.Fields("jiechuqi_id").Value)
                Else
                    lampkind = 1
                End If
                If lampkind < 3 Then
                    '表示两字节的，则把状态显示中的功率，功率因数，电压值置为“-”
                    Me.dgv_lamp_state_list.Rows(row).Cells("presure_lamp").Value = "-"
                    Me.dgv_lamp_state_list.Rows(row).Cells("yinshu_lamp").Value = "-"
                    Me.dgv_lamp_state_list.Rows(row).Cells("power_lamp").Value = "-"
                    Me.dgv_lamp_state_list.Rows(row).Cells("current_lamp").Value = "-"
                    Me.dgv_lamp_state_list.Rows(row).Cells("control_box_name").Value = "-"
                Else
                    '六字节的存在
                    Me.dgv_lamp_state_list.Rows(row).Cells("presure_lamp").Value = rs.Fields("presure_l").Value
                    Me.dgv_lamp_state_list.Rows(row).Cells("current_lamp").Value = rs.Fields("current_l").Value
                    Me.dgv_lamp_state_list.Rows(row).Cells("yinshu_lamp").Value = rs.Fields("presure_end").Value
                    Me.dgv_lamp_state_list.Rows(row).Cells("power_lamp").Value = rs.Fields("power").Value
                    Me.dgv_lamp_state_list.Rows(row).Cells("control_box_name").Value = rs.Fields("control_box_name").Value
                    Me.dgv_lamp_state_list.Rows(row).Cells("type_string").Value = Com_inf.Get_Type_String(Val(rs.Fields("lamp_type_id").Value))
                End If
                Com_inf.Get_DengGan(Trim(rs.Fields("lamp_id").Value))
                Me.dgv_lamp_state_list.Rows(row).Cells("denggan_id").Value = g_dengzhuid & "号灯杆 第" & g_dengzhulampid & "盏灯"
                If lampkind = "3" Then
                    probleminf = controllamp.get_probleminf(rs.Fields("state").Value)
                    If rs.Fields("div_time_id").Value = "0" Then
                        '表示关闭
                        Me.dgv_lamp_state_list.Rows(row).Cells("open_close").Value = "关闭"
                    Else
                        If rs.Fields("div_time_id").Value = "1" Then
                            '表示打开
                            Me.dgv_lamp_state_list.Rows(row).Cells("open_close").Value = "打开"
                        Else
                            '表示初始状态
                            Me.dgv_lamp_state_list.Rows(row).Cells("open_close").Value = "-"
                        End If
                    End If
                    Me.dgv_lamp_state_list.Rows(row).Cells("state_content").Value = probleminf
                Else
                    If rs.Fields("div_time_id").Value = 0 Then
                        '表示关闭
                        Me.dgv_lamp_state_list.Rows(row).Cells("open_close").Value = "关闭"
                    Else
                        If rs.Fields("div_time_id").Value = 1 Then
                            '表示打开
                            Me.dgv_lamp_state_list.Rows(row).Cells("open_close").Value = "打开"
                        Else
                            '表示初始状态
                            Me.dgv_lamp_state_list.Rows(row).Cells("open_close").Value = "-"
                        End If
                    End If
                    If rs.Fields("result").Value = 0 Then
                        If rs.Fields("state").Value = 1 Or rs.Fields("state").Value = 4 Then
                            Me.dgv_lamp_state_list.Rows(row).Cells("state_content").Value = LAMP_STATE_ON
                        Else
                            Me.dgv_lamp_state_list.Rows(row).Cells("state_content").Value = LAMP_STATE_OFF
                        End If
                    Else
                        If rs.Fields("result").Value = 1 Then
                            Me.dgv_lamp_state_list.Rows(row).Cells("state_content").Value = LAMP_STATE_PROBLEM_ON

                        Else
                            If rs.Fields("result").Value = 2 Then
                                Me.dgv_lamp_state_list.Rows(row).Cells("state_content").Value = LAMP_STATE_PROBLEM_OFF

                            Else
                                If rs.Fields("result").Value = 3 Then
                                    Me.dgv_lamp_state_list.Rows(row).Cells("state_content").Value = LAMP_STATE_NORETURN
                                Else
                                    Me.dgv_lamp_state_list.Rows(row).Cells("state_content").Value = LAMP_STATE_CONTROL
                                End If
                            End If

                        End If
                    End If
                End If

                row += 1

            End While
            If rs.State = 1 Then
                rs.Close()
                rs = Nothing
            End If
            conn.Close()
            conn = Nothing
        End If
    End Sub

    Private Sub BackgroundWorker_yaoce_ProgressChanged(sender As System.Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker_yaoce.ProgressChanged
        If e.ProgressPercentage = 30 Then
            rtb_inf.Refresh()
            rtb_inf.AppendText("主控箱：" & m_controlboxname & " 开始巡查数据......" & vbCrLf)

        End If
        If e.ProgressPercentage = 31 Then
            rtb_inf.AppendText("主控箱：" & m_controlboxname & " 巡查数据结束......" & vbCrLf)
        End If

        If e.ProgressPercentage = 33 Then
            rtb_inf.AppendText("主控箱：" & m_controlboxname & " 未连接无法召测" & vbCrLf)
        End If
        If e.ProgressPercentage = 34 Then
            rtb_inf.AppendText("主控箱：" & m_controlboxname & " 巡查数据超时......" & vbCrLf)
        End If
        rtb_inf.Select(rtb_inf.Text.Length, 0)
        rtb_inf.ScrollToCaret()
    End Sub

    Private Sub BackgroundWorker_yaoce_RunWorkerCompleted(sender As System.Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker_yaoce.RunWorkerCompleted
        bt_add_boxname.Enabled = True
        bt_del_boxname.Enabled = True
        bt_yaoce.Enabled = True
    End Sub

    Private Sub 单灯召测_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        g_windowclose = 1
        welcome_win.Enabled = True
    End Sub
End Class