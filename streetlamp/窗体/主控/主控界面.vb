''' <summary>
''' 软件的主控界面，实现手工控制，监测，时段控制和信息统计
''' </summary>
''' <remarks></remarks>
Public Class welcome_win
    ''' <summary>
    ''' 通过改变滚动轴的值大小，进行地图的缩放
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Overridable Sub tb_map_size_Click(ByVal sender As System.Object, ByVal e As MouseEventArgs) Handles tb_map_size.MouseWheel
        Me.tb_map_size.Focus()
        g_changemapvalue = tb_map_size.Value
        If e.Delta < 0 Then
            If g_changemapvalue > 0 Then
                g_changemapvalue -= 1   '缩小地图
            Else
                g_changemapvalue = 0
            End If
        Else
            If g_changemapvalue < MAP_MAX_SIZE Then
                g_changemapvalue += 1    '放大地图
            Else
                g_changemapvalue = MAP_MAX_SIZE
            End If
        End If
    End Sub

    ''' <summary>
    ''' 为主控界面中的各种下拉框增加初始内容
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub add_initial_value()
        '************时段控制控制模式***********************
        mod_time.Items.Clear()
        Dim i As Integer = 0
        While i < g_divname.Length   '时段控制名称
            If g_divname(i) Is Nothing Then
                i += 1
            Else
                mod_time.Items.Add(g_divname(i))
                i += 1
            End If
        End While
        If mod_time.Items.Count > 0 Then
            mod_time.SelectedIndex = 0
        End If
        Com_inf.Select_city_name(city_time)
        Com_inf.Select_area_name(city_time, area_time)
        Com_inf.Select_street_name(city_time, area_time, street_time)
        Com_inf.Select_box_name_level(city_time, area_time, street_time, box_time)
        Com_inf.Select_type_name(box_time, type_time, type_id)
        '********************************************************************************
    End Sub

    ''' <summary>
    ''' 主控界面的load函数
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub welcome_win_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: 这行代码将数据加载到表“ShiYaDataSet1.Control_box_state”中。您可以根据需要移动或移除它。
        'Me.Control_box_stateTableAdapter.Fill(Me.ShiYaDataSet1.Control_box_state)  '失压报警
        'Me.dgv_shiya_alarmlist.ForeColor = Color.Red
        ''TODO: 这行代码将数据加载到表“KaiguanalarmDataSet.kaiguan_alarm_list”中。您可以根据需要移动或移除它。
        'Me.Kaiguan_alarm_listTableAdapter.Fill(Me.KaiguanalarmDataSet.kaiguan_alarm_list)
        'dgv_kaiguan_alarmlist.ForeColor = Color.Red

        ''TODO: 这行代码将数据加载到表“ConfigDataDataSet.config_state_list”中。您可以根据需要移动或移除它。
        'Me.Config_state_listTableAdapter.Fill(Me.ConfigDataDataSet.config_state_list)
        'Me.dgv_configstate_list.ForeColor = Color.Red

        'TODO: 这行代码将数据加载到表“Configdata.config_state_list”中。您可以根据需要移动或移除它。
        ' Me.Config_state_listTableAdapter.Fill(Me.Configdata.config_state_list)
        'TODO: 这行代码将数据加载到表“Control_box_state.control_box”中。您可以根据需要移动或移除它。
    
        'Dim i As Integer = 0
        'While i < box_problem_list.RowCount
        '    If Trim(box_problem_list.Rows(i).Cells("power_type").Value) = "1" Then
        '        box_problem_list.Rows(i).Cells("powerproblem").Value = "电池供电"
        '    Else
        '        box_problem_list.Rows(i).Cells("powerproblem").Value = "交流电供电"
        '    End If
        '    i += 1
        'End While
        'box_problem_list.Refresh()
        Me.IsMdiContainer = True
        Me.Icon = New Icon("图片\favicon.ico", 32, 32)  'logo
        Me.Text = PROJECT_TITLE_STRING  '窗体标题
        Me.work_string.Text = PROJECT_TITLE_STRING '工具栏中的字幕
        Me.map_size_id.Text = "地图尺寸：100%"   '地图缩放比例提示字符串，初始为100%
        '  Me.SetTextLabelDelegate("目前系统处于时段控制", Tool, "work_string")
        load_map()  '载入地图
        Init_value() '初始化变量
        add_initial_value()  '为主控面板中的下拉框增加首选
        set_visual_false()  '隐藏相关控件
        Timer1.Start()  '定时器1启动
        Timer1.Interval = 1000
        ' area_content_list_all(0)  '右侧监控面板中灯的状态列表，初始状态为查询全部
        Dim div_time_obj As New div_time_class
        div_time_obj.Div_time_show() '时段控制面板信息()
        m_controlboxobj.set_controlbox_list(tv_box_inf_list) '主控箱信息列表
        tv_box_inf_list.ExpandAll()
        gangwei_rights() '对权限的的功能进行分配
        get_boxprobleminf() '获取故障信息

        '****************************线程操作****************************************
        BackgroundWorker_find_state.RunWorkerAsync()   '批量分析打包上传的数据
        BackgroundWorker_state_inf.RunWorkerAsync()  '统计一下当前的状态信息
        'BackgroundWorker_check_problem.RunWorkerAsync()  '故障确认
        BackgroundWorkerHeart.RunWorkerAsync()   '通信状态的确认

        'BackgroundWorkerSendMsg.RunWorkerAsync()
        'Me.BackgroundWorker_on_off.RunWorkerAsync()
        '*****************************************************************************
        Com_inf.GetMapSize()  '获取地图的尺寸
        tb_map_size.Maximum = MAP_MAX_SIZE
        tb_map_size.Value = MAP_MID_SIZE

        '*********************自动抄表设置******************************
        If g_chaobiaotag = True Then
            自动抄表ToolStripMenuItem.Text = "关闭自动抄表"
            ToolStripLabel1.Text = "抄表模式：自动"
        Else
            自动抄表ToolStripMenuItem.Text = "打开自动抄表"
            ToolStripLabel1.Text = "抄表模式：手动"
        End If
        节日模式ToolStripMenuItem.Visible = False
    End Sub

    ''' <summary>
    ''' 故障信息
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub get_boxprobleminf()
        'Dim row As Integer = 0
        'Dim colume As Integer = 0
        'If dgv_alarmlist.Rows.Count > 0 Then
        '    row = dgv_alarmlist.CurrentCell.RowIndex
        '    colume = dgv_alarmlist.CurrentCell.ColumnIndex
        'End If
        'Me.Control_boxTableAdapter.Fill(Me.Control_box_state.control_box) '刷新电控箱故障列表
        '' Me.Control_box_stateTableAdapter.Fill(Me.ShiYaDataSet1.Control_box_state)  '失压报警
        'If row > dgv_alarmlist.Rows.Count - 1 Then
        '    row = dgv_alarmlist.Rows.Count - 1
        'End If
        'If dgv_alarmlist.Rows.Count > 0 Then
        '    dgv_alarmlist.CurrentCell = dgv_alarmlist(colume, row)
        'End If

        '' Me.Control_boxTableAdapter.Fill(Me.Control_box_state.control_box)  '载入主控箱的通信状态
        'dgv_alarmlist.ForeColor = Color.Red

    End Sub
    '''' <summary>
    '''' 首页关于单灯的报警信息
    '''' </summary>
    '''' <remarks></remarks>
    'Public Sub get_alarm_list()
    '    Me.Alarm_viewTableAdapter.FillBy_notend(Me.StreetlampDataSet.alarm_view)   '故障列表
    '    Dim i As Integer = 0
    '    While i < Me.dgv_problem_list.RowCount  '对每一行数据添加灯杆的信息
    '        Com_inf.Get_DengGan(Me.dgv_problem_list.Rows(i).Cells("lamp_id_full").Value)
    '        Me.dgv_problem_list.Rows(i).Cells("denggan").Value = g_dengzhuid & "号灯杆 第" & g_dengzhulampid & "盏灯"
    '        i += 1
    '    End While
    '    Me.dgv_problem_list.ForeColor = Color.Red   '故障信息为红色显示
    '    Me.dgv_box_problem_list.ForeColor = Color.Red  '故障信息为红色显示
    'End Sub

    ''' <summary>
    ''' 根据登录的用户权限，进行软件的功能配置
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub gangwei_rights()
        '将所有的功能都设置为不可见
        Me.权限ToolStripMenuItem.Visible = False  '权限菜单
        Me.设备ToolStripMenuItem.Visible = False  '设备菜单
        Me.日志ToolStripMenuItem.Visible = False  '日志菜单
        Me.系统设置ToolStripMenuItem.Visible = False  '系统菜单
        ToolStripButton_systemset.Visible = False '工具栏中的系统设置
        Me.报警ToolStripMenuItem.Visible = False  '报警菜单
        Me.Tab_show_box.TabPages.Remove(Me.TabPage2)  '手工控制面板
        Me.Tab_show_box.TabPages.Remove(Me.TabPage3)  '时段控制面板
        Me.节日模式ToolStripMenuItem.Visible = False  '用户自定义
        Me.状态查询ToolStripMenuItem.Visible = False  '数据
        ToolStripButton_boxset.Visible = False  '主控箱
        ToolStripButton_setlamp.Visible = False '单灯控制
        ToolStripButton_control.Visible = False  '控制面板
        ToolStripButtonControlSet.Visible = False '控制模式设置
        ToolStripButton_zhaoce.Visible = False   '召测
        ToolStripButton_boxinf.Visible = False  '主控箱状态统计
        ToolStripButton_light.Visible = False '光照
        ToolStripButton_alarm.Visible = False '报警声音
        ToolStripButton_choosealarm.Visible = False '选择报警
        Me.UserNameText.Text = "登录名：" & g_username & "  岗位：" & g_rightmanage    '登录的用户名

        '查找岗位对应的权限
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        msg = ""
        sql = "select rights from gangwei_rights where gangwei='" & g_rightmanage & "'"
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            SetTextDelegate(MSG_ERROR_STRING & "GangWei_rights" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)
            conn.Close()
            conn = Nothing
            Me.Close()
        End If
        While rs.EOF = False
            '开通相关权限的功能
            If rs.Fields("rights").Value = "报警处理" Then
                'Me.报警ToolStripMenuItem.Visible = True   '报警菜单
                'ToolStripButton_choosealarm.Visible = True '报警选择
                'ToolStripButton_alarm.Visible = True '报警声音
            End If
            If rs.Fields("rights").Value = "设备开关" Then
                Me.Tab_show_box.TabPages.Insert(1, Me.TabPage2)   '原手工面板（已不用）
                'Me.Tab_show_box.TabPages.Insert(2, Me.TabPage3)  '时段控制
                'Me.节日模式ToolStripMenuItem.Visible = True '用户自定义
            End If
            If rs.Fields("rights").Value = "日志管理" Then
                Me.日志ToolStripMenuItem.Visible = True  '日志管理
            End If
            If rs.Fields("rights").Value = "权限管理" Then
                Me.权限ToolStripMenuItem.Visible = True  '权限管理
            End If
            If rs.Fields("rights").Value = "设备管理" Then
                Me.设备ToolStripMenuItem.Visible = True   '设备管理
                Me.状态查询ToolStripMenuItem.Visible = True  '设备状态查询
                ToolStripButton_boxset.Visible = True '主控箱
                ToolStripButton_setlamp.Visible = True '单灯
                ToolStripButton_control.Visible = True '控制面板
                ToolStripButtonControlSet.Visible = True '控制模式设置
                ToolStripButton_zhaoce.Visible = True '召测
                ToolStripButton_boxinf.Visible = True '主控箱状态统计
                'ToolStripButton_light.Visible = True '光照控制
            End If
            If rs.Fields("rights").Value = "系统设置" Then
                Me.系统设置ToolStripMenuItem.Visible = True  '系统设置
                ToolStripButton_systemset.Visible = True
            End If
            rs.MoveNext()
        End While
    End Sub

    ''' <summary>
    ''' 显示右边关于终端状态信息的列表
    ''' </summary>
    ''' <param name="find_condition"></param>
    ''' <remarks></remarks>
    Public Sub area_content_list_all(ByVal find_condition As Integer)
        SetControlBoxListDelegate(dgv_lamp_state_list, find_condition)
    End Sub

    ''' <summary>
    ''' 主控窗口被关闭时停止正在运行的线程
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub welcome_win_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If BackgroundWorker_state_inf.IsBusy = True Then '如果状态线程未被停止
            BackgroundWorker_state_inf.CancelAsync() '停止状态线程
        End If
        If BackgroundWorker_find_state.IsBusy = True Then
            BackgroundWorker_find_state.CancelAsync()   '停止批量查询状态
        End If
        If BackgroundWorker_check_problem.IsBusy = True Then  '故障确认\
            Me.BackgroundWorker_check_problem.CancelAsync()
        End If
        If BackgroundWorkerHeart.IsBusy = True Then   '通信状态的确认
            Me.BackgroundWorkerHeart.CancelAsync()
        End If
        If Me.BackgroundWorkerSendMsg.IsBusy = True Then
            Me.BackgroundWorkerSendMsg.CancelAsync()
        End If
        If m_lamp IsNot Nothing Then
            m_lamp.Dispose()  '释放终端图片
        End If
        Application.Exit()
    End Sub

    ''' <summary>
    ''' 统计灯的状态信息
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BackgroundWorker_state_inf_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker_state_inf.DoWork
        ' Dim con_tag As System.Boolean '判断数据库是否连接
        While Me.BackgroundWorker_state_inf.CancellationPending = False
            Try
                control_center_inf()  '主控制界面上的实时统计信息
                get_alarm_run()  '判断是否有故障，如果有故障则产生故障报警
                System.Threading.Thread.Sleep(2000)
            Catch ex As Exception
                SetTextDelegate(MSG_ERROR_STRING & "BackgroundWorker_state_inf_DoWork" & e.ToString & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)
                System.Threading.Thread.Sleep(2000)
            End Try
        End While
    End Sub

    ''' <summary>
    ''' 判断是否有故障，如果有故障则产生故障报警，直至用户关闭报警声音
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub get_alarm_run()
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim state = "正常"
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        msg = ""
        sql = "select * from lamp_inf where result=1 and total_num=1"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs.RecordCount > 0 Then
            '有单灯故障，且故障数据次数为2
            If Me.BackgroundWorkerAlarm.IsBusy = False Then
                BackgroundWorkerAlarm.RunWorkerAsync()   '报警声音
            End If
        End If
        sql = "update lamp_inf set total_num=3 where total_num=2"
        DBOperation.ExecuteSQL(conn, sql, msg)
        sql = "select * from control_box where state<>'" & state & "' and problem_num=1"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs.RecordCount > 0 Then
            '有电控箱故障，故障次数为1
            If Me.BackgroundWorkerAlarm.IsBusy = False Then
                BackgroundWorkerAlarm.RunWorkerAsync()   '报警声音
            End If
        End If
        sql = "update control_box set problem_num=2 where problem_num=1"
        DBOperation.ExecuteSQL(conn, sql, msg)

        System.Threading.Thread.Sleep(2000)
        m_controllampobj.m_alarmlinetag += 1   '报警闪烁的标志
        If m_controllampobj.m_alarmlinetag > 100 Then
            m_controllampobj.m_alarmlinetag = 0
        End If
        '开关量报警
        sql = "select id,alarm_tag from kaiguan_alarm_list where alarm_tag=0"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs.RecordCount > 0 Then
            '有开关量报警，故障次数为1
            If Me.BackgroundWorkerAlarm.IsBusy = False Then
                BackgroundWorkerAlarm.RunWorkerAsync()   '报警声音
            End If
            sql = "update kaiguan_alarm_list set alarm_tag=2 where id=" & rs.Fields("id").Value
            DBOperation.ExecuteSQL(conn, sql, msg)
        End If
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    ''' 显示电控箱的通信状态和灯的实时信息
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub control_center_inf()
        Try
            m_controllampobj.get_lamp_information()    '终端统计信息
        Catch ex As Exception
            SetTextDelegate("单灯" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)
            System.Threading.Thread.Sleep(2000)
        End Try
        Try
            m_controlboxobj.get_box_information() '绘制电控箱
        Catch ex As Exception
            SetTextDelegate("主控箱" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)
        End Try
        SetMapDelegate(pb_map, m_lamp, True)  '重绘地图中的灯
    End Sub

    ''' <summary>
    ''' 时钟定时器，显示时间，控制时段(特殊时段+平时控制模式)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Try
            Dim nowtime As DateTime '当前的时间
            Dim div_time_obj As New div_time_class
            Dim special_div_time_obj As New div_time_class
            Dim lightcontrolobj As New light_control
            nowtime = Now
            Tool.Items("time_now").Text = "现在时间：" & nowtime.ToString
            If g_sysjibiecontrol = "光照控制模式" Then
                '光照控制的情况下，利用光采集值，高于阈值关灯，低于阈值开灯
                If Me.BackgroundWorker_check_lightvalue.IsBusy = False Then
                    Me.BackgroundWorker_check_lightvalue.RunWorkerAsync()
                End If
                '通过判断光控的阈值，来决定主控箱是否打开(打开的主控箱仅为设置了经纬度的主控箱接触器，有效时间也仅在经纬度开关灯前后一个小时)
                lightcontrolobj.control_light()
            Else
                If m_divtimestart = 1 Then  '系统在时段模式下控制
                    Dim i As Integer
                    i = 0
                    While i < g_modgroup.Length
                        If g_modgroup(i) = "经纬度控制模式" Then
                            div_time_obj.suntime_divoperation()
                        End If
                        If g_modgroup(i) = "节假日控制模式" Then
                            special_div_time_obj.special_clock_time(True)  '发类型控制命令
                            special_div_time_obj.special_clock_time(False)  '发回路控制命令
                        End If
                        If g_modgroup(i) = "特殊控制模式" Then
                            div_time_obj.clock_time(True)  '发送类型控制命令
                            div_time_obj.clock_time(False)  '发回路控制命令
                        End If
                        i += 1
                    End While
                End If
            End If
next1:
            '定位单灯及主控箱的指示标识显示的时间为5秒
            If Me.zhishijiantou.Visible = True Then
                m_zhishitime += 1
                If m_zhishitime = 5 Then
                    Me.zhishijiantou.Visible = False
                End If
            End If
            If g_sethuilutag = True Then
                Com_inf.get_jiechuqi_state()
                g_sethuilutag = False
            End If
            '主控箱信息的显示时间,显示时间为1分钟
            If Me.dgv_box_inf.Visible = True Then
                Me.m_dgvviewtag += 1
                If m_dgvviewtag = 60 Then
                    pl_boxinf.Visible = False
                End If
            End If
            '每天12:15自动下放定时
            If (nowtime.Hour = 12 And nowtime.Minute = 15 And nowtime.Second = 0) Then
                'sendTime()
                g_mod_controlboxname = "" '定时校时所有主控箱的时间
                If g_welcomewinobj.BackgroundWorkerTimeXiafang.IsBusy = False Then
                    g_welcomewinobj.BackgroundWorkerTimeXiafang.RunWorkerAsync()
                End If
            End If
            '每天整点招测
            If (nowtime.Minute = 0 And nowtime.Second = 0) Then
                If g_zhaocetag = False Then
                    If Me.BackgroundWorker_getboxdata.IsBusy = False Then
                        Me.BackgroundWorker_getboxdata.RunWorkerAsync()
                    End If
                End If
            End If

            '根据设定时间写入区间命令三次中的第一次、
            If g_lampAlarmDo = "True" And g_lampStarTime > 0 And g_lampEndTime > 0 Then
                If g_lampStarTime < g_lampEndTime Then '如果配置文件时间在同一天里面并且起始时间小于结束时间、即表示不跨夜执行
                    If nowtime.Hour >= g_lampStarTime And nowtime.Hour < g_lampEndTime Then
                        If g_lampwaittime < (g_getlamp_time - g_lamptimes) * 60 Then
                            g_lampwaittime += 1
                        Else
                            If Me.BackgroundWorkergetlampdata.IsBusy = False Then
                                If g_lamptimes <> 0 Then
                                    If g_lampwaittime = (g_getlamp_time - g_lamptimes) * 60 Then
                                        g_autotime = 1
                                        Me.BackgroundWorkergetlampdata.RunWorkerAsync()
                                    ElseIf g_lampwaittime = g_getlamp_time * 60 Then
                                        g_autotime = 2
                                        Me.BackgroundWorkergetlampdata.RunWorkerAsync()
                                    ElseIf g_lampwaittime = (g_getlamp_time + g_lamptimes) * 60 Then
                                        g_autotime = 3
                                        Me.BackgroundWorkergetlampdata.RunWorkerAsync()
                                    End If
                                Else
                                    g_autotime = 1
                                    Me.BackgroundWorkergetlampdata.RunWorkerAsync()
                                End If
                            End If
                            g_lampwaittime += 1
                        End If
                    End If
                Else
                    If (nowtime.Hour >= g_lampStarTime And nowtime.Hour <= 24) Or (nowtime.Hour >= 0 And nowtime.Hour < g_lampEndTime) Then
                        If g_lampwaittime < (g_getlamp_time - g_lamptimes) * 60 Then
                            g_lampwaittime += 1
                        Else
                            If Me.BackgroundWorkergetlampdata.IsBusy = False Then
                                If g_lamptimes <> 0 Then
                                    If g_lampwaittime = (g_getlamp_time - g_lamptimes) * 60 Then
                                        g_autotime = 1
                                        Me.BackgroundWorkergetlampdata.RunWorkerAsync()
                                    ElseIf g_lampwaittime = g_getlamp_time * 60 Then
                                        g_autotime = 2
                                        Me.BackgroundWorkergetlampdata.RunWorkerAsync()
                                    ElseIf g_lampwaittime = (g_getlamp_time + g_lamptimes) * 60 Then
                                        g_autotime = 3
                                        Me.BackgroundWorkergetlampdata.RunWorkerAsync()
                                    End If
                                Else
                                    g_autotime = 1
                                    Me.BackgroundWorkergetlampdata.RunWorkerAsync()
                                End If
                            End If
                            g_lampwaittime += 1
                        End If
                    End If
                End If
            End If

            If g_chaobiaotag = True Then
                '打开自动抄表
                If g_chaobiaodate > -1 And g_chaobiaotime > -1 Then
                    '按每月固定时间查询
                    If nowtime.Day = g_chaobiaodate And nowtime.Minute = 10 And nowtime.Hour = g_chaobiaotime And (nowtime.Second < 3) Then
                        If Me.BackgroundWorker_meterdata.IsBusy = False Then
                            Me.BackgroundWorker_meterdata.RunWorkerAsync()
                        End If
                    End If
                End If
                If g_chaobiaodate = -1 And g_chaobiaotime > -1 Then
                    '按每天固定时间查询
                    If nowtime.Hour = g_chaobiaotime And nowtime.Minute = 10 And (nowtime.Second < 3) Then
                        If Me.BackgroundWorker_meterdata.IsBusy = False Then
                            Me.BackgroundWorker_meterdata.RunWorkerAsync()
                        End If
                    End If
                End If
            End If

            '每年的1月1日0:15分重新设置一下当年的日出日落表
            If nowtime.DayOfYear = 1 And nowtime.Hour = 1 And nowtime.Minute = 10 And (nowtime.Second = 0 Or nowtime.Second = 1) Then
                Com_inf.set_suntime()
            End If

            'Set_pianyi_value()
            '每天0点，1点，2点三个时段判断是否将当天的日出日落时刻记录到pianyi表中
            If (nowtime.Hour = 1 Or nowtime.Hour = 2 Or nowtime.Hour = 3) And nowtime.Minute = 20 And nowtime.Second = 0 Then
                Set_pianyi_value()
                Com_inf.Set_holiday_mod()  '设置主控箱当天是否有节假日的控制模式，标志位为control_box表中的elec_state字段
                g_modtag = 1 '对控制模式进行下放的标志
                g_mod_controlboxname = ""  '定时下放所有主控箱的模式控制
                If Me.BackgroundWorkerModXiaFang.IsBusy = False Then
                    Me.BackgroundWorkerModXiaFang.RunWorkerAsync()
                End If
            End If

            '每天中午11:30点将前两天的数据删除
            If Now.Hour = 11 And Now.Minute = 30 And Now.Second = 0 Then
                Try
                    Dim sql As String
                    Dim msg As String
                    Dim conn As New ADODB.Connection
                    Dim result As Boolean
                    msg = ""
                    result = DBOperation.OpenConn(conn)
                    If result = False Then
                        Exit Sub
                    End If
                    '删除RoadLightStatus 中两天前的数据
                    sql = "delete from RoadLightStatus where Createtime<'" & Now.AddDays(-1) & "'"
                    DBOperation.ExecuteSQL(conn, sql, msg)
                    '删除HeartPackage中两天前的数据
                    sql = "delete from HeartPackage where ReceiveTime<'" & Now.AddDays(-1) & "'"
                    DBOperation.ExecuteSQL(conn, sql, msg)
                    conn.Close()
                    conn = Nothing
                Catch ex As Exception
                    SetTextDelegate(MSG_ERROR_STRING & "数据库删除失败" & ex.ToString & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)
                End Try
            End If

            '判断是否需要招测
            If g_autozhaoce = 0 And g_zhaocetag = True Then
                If Me.BackgroundWorker_getboxdata.IsBusy = False Then
                    Me.BackgroundWorker_getboxdata.RunWorkerAsync()
                End If
            Else
                If g_autozhaoce > 0 Then
                    g_autozhaoce -= 1
                End If
            End If
        Catch ex As Exception
            SetTextDelegate(MSG_ERROR_STRING & "Timer1_Tick" & e.ToString & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, True, rtb_info_list)
        End Try
    End Sub


    ''' <summary>
    ''' 设置每天的日出日落时间
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Set_pianyi_value()
        Dim rs_pianyi, rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String
        Dim nowtime As DateTime
        Dim open_pianyi As Integer '开灯偏移
        Dim close_pianyi As Integer  '关灯偏移
        nowtime = System.Convert.ToDateTime(Now.Year.ToString & "-" & Now.Month.ToString & "-" & _
       Now.Day.ToString & " 0:0:0")
        sql = "select * from pianyi order by id"
        msg = ""
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        rs_pianyi = DBOperation.SelectSQL(conn, sql, msg)
        If rs_pianyi Is Nothing Then
            SetTextDelegate(MSG_ERROR_STRING & "Set_pianyi_value" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        While rs_pianyi.EOF = False
            If System.Convert.ToDateTime(rs_pianyi.Fields("today_closetime").Value).Date <> nowtime.Date Or System.Convert.ToDateTime(rs_pianyi.Fields("today_opentime").Value).Date <> nowtime.Date Then
                open_pianyi = rs_pianyi.Fields("open_pianyi").Value
                close_pianyi = rs_pianyi.Fields("close_pianyi").Value
                sql = "select * from suntime where time>='" & nowtime & "' and time<'" & nowtime.AddDays(1) & "'"
                rs = DBOperation.SelectSQL(conn, sql, msg)
                If rs Is Nothing Then
                    SetTextDelegate(MSG_ERROR_STRING & "Set_pianyi_value" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)
                    rs_pianyi.Close()
                    rs_pianyi = Nothing
                    conn.Close()
                    conn = Nothing
                    Exit Sub
                End If
                While rs.EOF = False
                    If Trim(rs.Fields("mod").Value) = "开" Then
                        rs_pianyi.Fields("today_opentime").Value = System.Convert.ToDateTime(rs.Fields("time").Value).AddMinutes(open_pianyi)

                    Else
                        rs_pianyi.Fields("today_closetime").Value = System.Convert.ToDateTime(rs.Fields("time").Value).AddMinutes(close_pianyi)

                    End If
                    rs.MoveNext()
                End While
                rs_pianyi.Update()
            End If
            rs_pianyi.MoveNext()
        End While
        If rs_pianyi.State = 1 Then
            rs_pianyi.Close()
            rs_pianyi = Nothing
        End If
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    ''' 分析底层上传的灯的状态信息线程主函数
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BackgroundWorker_find_state_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker_find_state.DoWork
        ''批量获取灯的状态线程
        ' Dim con_tag As System.Boolean
        While Me.BackgroundWorker_find_state.CancellationPending = False
            Try
                ''If CURRENT_ADTYPE = 1 Then
                ''    '老的电流协议，1个字节
                ''    find_total_state_fun()  '检测单灯状态
                ''Else
                ''    '新的电流协议，2个字节，2011年5月19日增加
                ''    find_total_state_fun_AD2()
                ''End If

                '2011年7月14日将单灯协议改为新旧两版并存的格式

                find_total_state_fun()

                'm_loadwaittime += 1
                'If m_loadwaittime = 30 Then

                '    m_controlboxobj.find_box_state_fun(True)  '查询回路信息,每半小时记录一次电流，电压，功率值
                '    m_loadwaittime = 0

                'Else
                '    m_controlboxobj.find_box_state_fun(False) '查询回路信息，不记录电流，电压，功率值
                'End If

                '******************检测是否有开关量的报警*******************
                check_kaiguan_alarm()

                ''检查是否有主控器的自动报警
                m_controlboxobj.find_auto_alarm()
                '****************2011年10月20日增加手工控制后自动关闭的命令（淄博特殊要求，其他地方基本用不到）***
                m_controllampobj.auto_close()
                '统计报警信息
                get_box_alarm_inf()
                Me.BackgroundWorker_find_state.ReportProgress(2)
                System.Threading.Thread.Sleep(10000)
            Catch ex As Exception
                SetTextDelegate(MSG_ERROR_STRING & "BackgroundWorker_find_state_DoWork" & e.ToString & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, True, rtb_info_list)

            End Try
        End While

    End Sub


    Private Sub get_box_alarm_inf()
        Dim rs_box As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String
        Dim i As Integer = 0

        msg = ""
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        '刷新电控箱故障列表
        If g_alarminf.Count > 0 Then
            g_alarminf.Clear() '清空列表
        End If

        '刷新主控箱的通信
        sql = "SELECT control_box_name , StatusContent, Createtime FROM control_box_state nolock" _
        & " WHERE (kaiguan_string = '通信') AND (StatusContent = '未连接') AND (state = 0 OR state = 2)"
        rs_box = DBOperation.SelectSQL(conn, Sql, msg)
        If rs_box Is Nothing Then
            conn.Close()
            conn = Nothing
        End If
        While rs_box.EOF = False
            Dim inf As New m_alarminf
            inf.control_box_name = Trim(rs_box.Fields("control_box_name").Value)
            inf.alarm_msg = Trim(rs_box.Fields("StatusContent").Value)
            inf.alarm_time = rs_box.Fields("Createtime").Value
            g_alarminf.Add(inf)
            rs_box.MoveNext()
        End While

        '刷新主控箱的状态报警
        sql = "select control_box_name, StatusContent,Createtime,kaiguan_string from control_box_state nolock where (kaiguan_string = '供电') AND (StatusContent = '电池') AND (state=0 or state=2)"
        rs_box = DBOperation.SelectSQL(conn, Sql, msg)
        If rs_box Is Nothing Then
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        While rs_box.EOF = False
            Dim inf As New m_alarminf
            inf.control_box_name = Trim(rs_box.Fields("control_box_name").Value)
            inf.alarm_msg = "失压"
            inf.alarm_time = rs_box.Fields("createtime").Value
            g_alarminf.Add(inf)
            rs_box.MoveNext()
        End While

        sql = "select control_box_name, StatusContent,Createtime,kaiguan_string from control_box_state nolock where (kaiguan_string = '状态') AND (StatusContent <> '正常') AND (state=0 or state=2)"
        rs_box = DBOperation.SelectSQL(conn, Sql, msg)
        If rs_box Is Nothing Then
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        Dim state() As String
        While rs_box.EOF = False
            Dim inf As New m_alarminf
            i = 0
            state = Trim(rs_box.Fields("StatusContent").Value).Split(" ")
            While i < state.Length
                If state(i) <> "" Then
                    inf.control_box_name = Trim(rs_box.Fields("control_box_name").Value)
                    inf.alarm_msg = state(i)
                    inf.alarm_time = rs_box.Fields("createtime").Value
                    g_alarminf.Add(inf)
                End If           
                i += 1
            End While
            rs_box.MoveNext()
        End While

        '刷新主控箱的开关量报警
        sql = "SELECT control_box_name, alarm_string, createtime FROM kaiguan_alarm_list nolock WHERE (alarm_tag = 2 or alarm_tag=0)"
        rs_box = DBOperation.SelectSQL(conn, Sql, msg)
        If rs_box Is Nothing Then
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        While rs_box.EOF = False
            Dim inf As New m_alarminf
            inf.control_box_name = Trim(rs_box.Fields("control_box_name").Value)
            inf.alarm_msg = Trim(rs_box.Fields("alarm_string").Value)
            inf.alarm_time = rs_box.Fields("createtime").Value
            g_alarminf.Add(inf)
            rs_box.MoveNext()
        End While
        If rs_box.State = 1 Then
            rs_box.Close()
            rs_box = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    ''' 检测是否有开关量报警
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub check_kaiguan_alarm()
        Dim rs, rs_box, rs_alarm, rs_record As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim alarm_list(2) As String '存放开关量报警的3位长度的字符串
        Dim control_box_id As String '电控箱编号
        Dim control_box_name As String '电控箱名称
        Dim i As Integer
        Dim alarmstring As String '报警字符串
        Dim alarm_tag As Boolean = False
        Dim alarminf() As String '故障列表
        Dim j As Integer = 0
        Dim kaiguan_value As String  '开关量的值
        Dim control_box_type As Integer '主控箱的类型，目的是区分大小三遥
        Dim huilu_tag As Integer '默认的控制路数，大三遥默认前六路，小三遥默认前三路
        msg = ""
        sql = "select * from RoadLightStatus where (PackType='" & HG_TYPE.HG_HOST_SW_AUTO & "' or PackType='" & HG_TYPE.HG_GET_KAIGUAN & "')" _
        & " and HandlerFlag=3 and createtime>'" & Now().AddMinutes(-10) & "' order by id desc"

        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            SetTextDelegate(MSG_ERROR_STRING & "Check_Kaiguan_Alarm" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If

        While rs.EOF = False
            '2011年11月22日，每次取状态的时候先判断一下是否需要等待召测信息
            'If g_zhaocetag = True Then
            '    Com_inf.clearstatus()
            '    If rs.State = 1 Then
            '        rs.Close()
            '        rs = Nothing
            '    End If
            '    conn.Close()
            '    conn = Nothing
            '    Exit Sub
            'End If
            '判断开关量的合法性
            If Trim(rs.Fields("StatusContent").Value).Split(" ").Length < 5 Then
                GoTo next1
            End If
            'alarm_list = Trim(rs.Fields("StatusContent").Value).Split(" ")
            m_controlboxobj.get_kaiguanString(Trim(rs.Fields("StatusContent").Value), alarm_list)  '将开关状态解析出主控编号，16位开关字符，及用电类型
            control_box_id = alarm_list(0)  '两个字节的电控箱编号

            '2011年4月20日增加电控箱的用电类型：电池或交流电
            sql = "select control_box_name,power_type,kaiguan_string,Createtime,control_box_id,control_box_type from control_box where control_box_id='" & control_box_id & "'"
            rs_box = DBOperation.SelectSQL(conn, sql, msg)
            If rs_box Is Nothing Then
                SetTextDelegate(MSG_ERROR_STRING & "Check_Kaiguan_Alarm" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)

                If rs.State = 1 Then
                    rs.Close()
                    rs = Nothing
                End If
                conn.Close()
                conn = Nothing
                Exit Sub
            End If
            If rs_box.RecordCount > 0 Then

                control_box_name = Trim(rs_box.Fields("control_box_name").Value)
                control_box_type = rs_box.Fields("control_box_type").Value
                If control_box_type = 3 Then  '小三遥，预留3位
                    huilu_tag = 3
                Else  '大三遥，预留6位
                    huilu_tag = 6
                End If
                i = 0
                '2012年3月5日对开关量的前6个值个开关量默认配置给6个接触器,大三遥6个接触器，小三遥3个接触器
                While i < huilu_tag
                    '根据开关量，获取实际的接触器状态
                    m_controlboxobj.set_huilu_actualstate(i + 1, Mid(alarm_list(1), 16 - i, 1), control_box_id, control_box_name)
                    i += 1
                End While

                i = 0
                '其他报警类型，大三遥从第七位开始，小三遥从第4位开始
                While i < alarm_list(1).Length
                    alarmstring = m_controlboxobj.alarm_yes_no(i + 1, Mid(alarm_list(1), 16 - i, 1), control_box_name)
                    If alarmstring <> "" Then  '报警
                        '有报警
                        alarminf = Trim(alarmstring).Split(" ")
                        j = 0
                        While j < alarminf.Length
                            kaiguan_value = alarminf(j).Split("_")(0)  '开关量的名称
                            sql = "select * from kaiguan_alarm_list where control_box_name='" & control_box_name & "' and alarm_string like'" & kaiguan_value & "%' and (alarm_tag=0 or alarm_tag=2)"
                            rs_record = DBOperation.SelectSQL(conn, sql, msg)
                            If rs_record Is Nothing Then
                                SetTextDelegate(MSG_ERROR_STRING & "Check_Kaiguan_Alarm" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)
                                GoTo finish
                            End If
                            If rs_record.RecordCount <= 0 Then
                                alarm_tag = True
                                '原来没有报警数据
                                'rs_record.AddNew()
                                'rs_record.Fields("alarm_string").Value = alarmstring
                                'rs_record.Fields("createtime").Value = Now
                                'rs_record.Fields("alarm_tag").Value = 0
                                'rs_record.Fields("control_box_name").Value = control_box_name
                                'rs_record.Fields("kaiguan_tag").Value = i + 1
                                'rs_record.Update()
                                sql = "insert into kaiguan_alarm_list(alarm_string,createtime,alarm_tag," _
                              & "control_box_name,kaiguan_tag) values('" & alarminf(j) & "','" & Now & "'," _
                              & "0,'" & control_box_name & "' ,'" & i + 1 & "')"
                                DBOperation.ExecuteSQL(conn, sql, msg)
                                Com_inf.Send_Msg(control_box_id, "", alarminf(j))
                            Else
                                '有原来有报警信息，则将原来的报警信息置为2，表示经过确认的
                                alarm_tag = True
                                'rs_record.Fields("alarm_tag").Value = 2
                                'rs_record.Update()
                                While rs_record.EOF = False
                                    If Trim(rs_record.Fields("alarm_string").Value) = alarminf(j) Then
                                        '报警信息一致
                                        sql = "update kaiguan_alarm_list set alarm_tag=2 where id='" & rs_record.Fields("id").Value & "'"
                                        DBOperation.ExecuteSQL(conn, sql, msg)
                                    Else
                                        '报警信息相反
                                        sql = "update kaiguan_alarm_list set alarm_tag=1 where id='" & rs_record.Fields("id").Value & "'"
                                        DBOperation.ExecuteSQL(conn, sql, msg)
                                        sql = "insert into kaiguan_alarm_list(alarm_string,createtime,alarm_tag," _
                             & "control_box_name,kaiguan_tag) values('" & alarminf(j) & "','" & Now & "'," _
                             & "0,'" & control_box_name & "' ,'" & i + 1 & "')"
                                        DBOperation.ExecuteSQL(conn, sql, msg)
                                        Com_inf.Send_Msg(control_box_id, "", alarminf(j))
                                    End If
                                    rs_record.MoveNext()
                                End While
                            End If
                            j += 1
                        End While
                    Else  '状态是正常的，则查询当前的报警表，有报警信息则将报警信息置1
                        sql = "select * from kaiguan_alarm_list where control_box_name='" & control_box_name & "' and kaiguan_tag='" & i + 1 & "' and (alarm_tag=0 or alarm_tag=2)"
                        rs_record = DBOperation.SelectSQL(conn, sql, msg)
                        If rs_record Is Nothing Then
                            SetTextDelegate(MSG_ERROR_STRING & "Check_Kaiguan_Alarm" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)
                            GoTo finish
                        End If
                        While rs_record.EOF = False
                            alarm_tag = True
                            '原来有报警数据，则将报警标志置为2，表示报警结束
                            'rs_record.Fields("alarm_tag").Value = 1
                            'rs_record.Fields("endtime").Value = Now
                            'rs_record.Update()
                            sql = "update kaiguan_alarm_list set alarm_tag=1,endtime='" & Now & "' where id='" & rs_record.Fields("id").Value & "'"
                            DBOperation.ExecuteSQL(conn, sql, msg)
                            rs_record.MoveNext()
                        End While
                    End If
                    i += 1  '同一个主控箱中的16位开关量报警数据
                End While
                '更新主控箱的供电类型
                If alarm_list(2) = 1 Then
                    ' rs_box.Fields("power_type").Value = POWERTYPE_BUTTERY
                    sql = "update control_box set power_type='" & POWERTYPE_BUTTERY & "',kaiguan_string='" & alarm_list(1) & "', Createtime='" & Now & "'  where control_box_name='" & control_box_name & "'"
                Else
                    '  rs_box.Fields("power_type").Value = POWERTYPE_CURRENT
                    sql = "update control_box set power_type='" & POWERTYPE_CURRENT & "',kaiguan_string='" & alarm_list(1) & "', Createtime='" & Now & "' where control_box_name='" & control_box_name & "'"
                End If
                DBOperation.ExecuteSQL(conn, sql, msg)
                'rs_box.Fields("kaiguan_string").Value = alarm_list(1)
                'rs_box.Fields("Createtime").Value = Now
                'rs_box.Update()
                '记录供电情况
                If alarm_list(2) = 1 Then
                    Setcontrolbox_Record(control_box_name, POWERTYPE_BUTTERY, "供电")
                Else
                    Setcontrolbox_Record(control_box_name, POWERTYPE_CURRENT, "供电")
                End If
                '失压报警刷新
                Me.BackgroundWorker_find_state.ReportProgress(2)
            End If
next1:
            '将本条记录置为1
            sql = "update RoadLightStatus set handlerflag=1 where id='" & rs.Fields("id").Value & "'"
            DBOperation.ExecuteSQL(conn, sql, msg)
            rs.MoveNext()
        End While
        If alarm_tag = True Then
            Me.BackgroundWorker_find_state.ReportProgress(3)
        End If
finish:
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If

        If rs_box.State = 1 Then
            rs_box.Close()
            rs_box = Nothing
        End If
        If rs_alarm.State = 1 Then
            rs_alarm.Close()
            rs_alarm = Nothing
        End If
        If rs_record.State = 1 Then
            rs_record.Close()
            rs_record = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub


 
    ''' <summary>
    ''' 计算GPRS流量
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Count_GPRS()
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        msg = ""
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        sql = "SELECT COUNT(*) AS lamp_num, SUBSTRING(StatusContent, 1, 2) AS boxid_hex FROM RoadLightStatus where HandlerFlag=2 and Createtime > DateAdd(n,-10,'" & Now() & "') GROUP BY SUBSTRING(StatusContent, 1, 2)"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Count_GPRS" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)
            conn.Close()
            conn = Nothing
        End If
        '将十六进制的电控箱编号转换成为十进制
        Dim box(rs.RecordCount - 1) As String   '电控箱编号
        Dim lamp(rs.RecordCount - 1) As String  '记录每个电控箱下有多少盏灯
        Dim rs_box As New ADODB.Recordset
        Dim time As System.DateTime
        Dim gprs As Integer  '统计一个电控箱下的流量
        Dim i As Integer = 0
        While rs.EOF = False
            box(i) = System.Convert.ToInt32(Val(Trim(rs.Fields("boxid_hex").Value)), 16)
            lamp(i) = System.Convert.ToInt32(Val(Trim(rs.Fields("lamp_num").Value)), 16)
            While box(i).Length < 4
                box(i) = "0" & box(i)
            End While
            sql = "select * from Box_GPRS where control_box_id ='" & box(i) & "' order by Time desc"
            rs_box = DBOperation.SelectSQL(conn, sql, msg)

            If rs_box Is Nothing Then
                SetTextDelegate(MSG_ERROR_STRING & "Count_GPRS" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)

                If rs.State = 1 Then
                    rs.Close()
                    rs = Nothing
                End If
                conn.Close()
                conn = Nothing
                Exit Sub
            End If
            If rs_box.RecordCount > 0 Then
                time = rs_box.Fields("Time").Value
                If time.Year = Now.Year And time.Month = Now.Month And time.Day = Now.Day Then
                    '具有当天日期的流量统计
                    gprs = 2 * lamp(i) + 20 + rs_box.Fields("gprs").Value  '一个电控箱的字节数
                    sql = "update Box_GPRS set GPRS='" & gprs & "' where id='" & rs_box.Fields("ID").Value & "'"
                Else
                    '没有当天的流量记录则新增
                    gprs = 2 * lamp(i) + 20  '一个电控箱的字节数
                    sql = "insert into Box_GPRS(control_box_id, GPRS, Time ) values('" & box(i) & "','" & gprs & "' ,'" & Now & "')"
                End If
                DBOperation.ExecuteSQL(conn, sql, msg)
            Else
                '没有当天的流量记录则新增
                gprs = 2 * lamp(i) + 20  '一个电控箱的字节数
                sql = "insert into Box_GPRS(control_box_id, GPRS, Time ) values('" & box(i) & "','" & gprs & "' ,'" & Now & "')"
                DBOperation.ExecuteSQL(conn, sql, msg)
            End If
            i += 1
            rs.MoveNext()
        End While
        '打开流量记录表，如有该电控箱当天的流量统计则累加，没有则新增
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub
    '    ''' <summary>
    '    ''' 分析底层上传灯的状态数据处理函数，将当前的状态插入到lamp_state_list表中,电流的AD值增加为2个字节
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    Private Sub find_total_state_fun_AD2()
    '        Dim rs_find_state As New ADODB.Recordset
    '        Dim msg As String
    '        Dim sql As String
    '        Dim conn As New ADODB.Connection
    '        Dim state_id As Integer
    '        Dim sql_state As String  '将灯的状态记录到lamp_state_record 表中的sql语句


    '        If DBOperation.OpenConn(conn) = False Then
    '            Exit Sub
    '        End If

    '        msg = ""
    '        sql_state = ""
    '        state_id = 1

    '        '查询30分钟之间是否有没被分析过的数据,批量上传的数据HandlerFlag标志为2
    '        ' sql = "select * from RoadLightStatus where Createtime > DateAdd(n,-30,'" & Now() & "') and HandlerFlag=" & 2 & " order by ID"  '没有返回状态
    '        sql = "select * from RoadLightStatus where Createtime > DateAdd(n,-15,'" & Now() & "') and HandlerFlag=" & 2 & "  order by ID"  '没有返回状态

    '        rs_find_state = DBOperation.SelectSQL(conn, sql, msg)
    '        If rs_find_state Is Nothing Then  '数据库连接失败
    '            SetTextDelegate(MSG_ERROR_STRING & "find_total_state_fun_AD2" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)
    '            conn.Close()
    '            conn = Nothing
    '            Exit Sub
    '        End If
    '        If rs_find_state.RecordCount <= 0 Then  '没有数据则返回
    '            GoTo finish
    '        End If
    '        SetTextLabelDelegate("获取终端数据.....", Tool, "circle_string")

    '        '*************************有数据*********************
    '        Try
    '            Count_GPRS()  '计算流量
    '        Catch ex As Exception

    '        End Try


    '        '查找当前数据库的所有灯的记录，按上传数据进行数据分析

    '        While rs_find_state.EOF = False
    '            If Me.BackgroundWorker_find_state.CancellationPending = True Then
    '                GoTo finish
    '            End If
    '            Com_inf.Explain_State_String_AD2(Trim(rs_find_state.Fields("StatusContent").Value))  '解析状态字符串的各个含义

    '            m_controllampobj.GetCompareState_AD2(Trim(rs_find_state.Fields("StatusContent").Value), rs_find_state.Fields("id").Value)  '获取终端的运行状态


    '            rs_find_state.MoveNext()
    '        End While

    'finish:
    '        Me.BackgroundWorker_find_state.ReportProgress(1) '刷新故障报警
    '        area_content_list_all(m_checkcondition) '刷新右侧列表


    '        If rs_find_state.State = 1 Then
    '            rs_find_state.Close()
    '            rs_find_state = Nothing
    '        End If
    '        conn.Close()
    '        conn = Nothing

    '    End Sub

    ''' <summary>
    ''' 分析底层上传灯的状态数据处理函数，将当前的状态插入到lamp_state_list表中
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub find_total_state_fun()
        Dim rs_find_state As New ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim conn As New ADODB.Connection
        Dim state_id As Integer
        Dim sql_state As String  '将灯的状态记录到lamp_state_record 表中的sql语句
        Dim lamp_protocle_type As String '单灯的协议类型1，2,6
        Dim state() As String
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        '连接数据库
        msg = ""
        sql_state = ""
        state_id = 1
        '查询30分钟之间是否有没被分析过的数据,批量上传的数据HandlerFlag标志为2
        sql = "select * from RoadLightStatus where Createtime > DateAdd(n,-30,'" & Now() & "') and HandlerFlag=" & 2 & "  AND PackType=32 order by ID"  '没有返回状态
        'If g_packtype = 25 Then
        '    sql = "select * from RoadLightStatus nolock where Createtime > DateAdd(n,-5,'" & Now() & "') and HandlerFlag=" & 2 & " AND (PackType=32 Or PackType=25) order by ID"  '没有返回状态
        'Else
        '    sql = "select * from RoadLightStatus nolock where Createtime > DateAdd(n,-5,'" & Now() & "') and HandlerFlag=" & 2 & " AND PackType=32 order by ID"  '没有返回状态
        'End If
        g_packtype = 0
        rs_find_state = DBOperation.SelectSQL(conn, sql, msg)
        If rs_find_state Is Nothing Then  '数据库连接失败
            SetTextDelegate(MSG_ERROR_STRING & "find_total_state_fun" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs_find_state.RecordCount <= 0 Then  '没有数据则返回
            GoTo finish
        End If
        SetTextLabelDelegate("获取终端数据.....", Tool, "circle_string")
        '*************************有数据*********************
        'Try
        '    Count_GPRS()  '计算流量
        'Catch ex As Exception

        'End Try


        '查找当前数据库的所有灯的记录，按上传数据进行数据分析

        While rs_find_state.EOF = False
            If Me.BackgroundWorker_find_state.CancellationPending = True Then
                GoTo finish
            End If

            '2011年7月14日增加单灯的解析区分方法
            'If rs_find_state.Fields("PackType").Value Is System.DBNull.Value Then  '旧协议

            '    Com_inf.Explain_State_String(Trim(rs_find_state.Fields("StatusContent").Value))  '解析状态字符串的各个含义

            '    m_controllampobj.GetCompareState(Trim(rs_find_state.Fields("StatusContent").Value), rs_find_state.Fields("id").Value)  '获取终端的运行状态
            'Else
            '    Com_inf.Explain_State_String_AD2(Trim(rs_find_state.Fields("StatusContent").Value))  '解析状态字符串的各个含义

            '    m_controllampobj.GetCompareState_AD2(Trim(rs_find_state.Fields("StatusContent").Value), rs_find_state.Fields("id").Value)  '获取终端的运行状态

            'End If
            '2011年9月13日修改为只用一个字节的单灯协议
            'Com_inf.Explain_State_String(Trim(rs_find_state.Fields("StatusContent").Value))  '解析状态字符串的各个含义

            'm_controllampobj.GetCompareState(Trim(rs_find_state.Fields("StatusContent").Value), rs_find_state.Fields("id").Value)  '获取终端的运行状态

            '2011年10月17日将单灯分为不同的协议类型
            state = Trim(rs_find_state.Fields("StatusContent").Value).Split(" ")
            Com_inf.getstate_lampid(state)  '获取单灯编号
            Dim i As Integer = 0
            Dim short_id As String
            lamp_protocle_type = Com_inf.getlampprotocletype(g_lampidstring)
            If lamp_protocle_type = "-1" Then
                '表示软件中未添加该灯号
                '去除改状态
                sql = "update RoadLightStatus set HandlerFlag=1 where id=" & rs_find_state.Fields("id").Value
                DBOperation.ExecuteSQL(conn, sql, msg)
                GoTo next1

            End If
            If lamp_protocle_type = "1" Then

                If state.Length <> 7 Then
                    '上传的状态长度与单灯的类型不符合
                    SetTextDelegate(g_lampidstring & "节点版本设置为" & lamp_protocle_type & "与上传状态不符 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, True, rtb_info_list)
                    GoTo finish
                End If
                Com_inf.Explain_State_String(state)  '解析状态字符串的各个含义
                m_controllampobj.GetCompareState(Trim(rs_find_state.Fields("StatusContent").Value), rs_find_state.Fields("id").Value)  '获取终端的运行状态
            Else
                If lamp_protocle_type = "2" Then
                    If state.Length <> 7 Then
                        '上传的状态长度与单灯的类型不符合
                        SetTextDelegate(g_lampidstring & "节点版本设置为" & lamp_protocle_type & "与上传状态不符 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, True, rtb_info_list)

                        GoTo finish
                    End If
                    Com_inf.Explain_State_String_AD2(state)  '解析状态字符串的各个含义
                    m_controllampobj.GetCompareState_AD2(Trim(rs_find_state.Fields("StatusContent").Value), rs_find_state.Fields("id").Value)  '获取终端的运行状态
                Else
                    If state.Length <> 10 Then
                        '上传的状态长度与单灯的类型不符合
                        SetTextDelegate(g_lampidstring & "节点版本设置为" & lamp_protocle_type & "与上传状态不符 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, True, rtb_info_list)

                        GoTo finish
                    End If
                    While i < 1

                        '2012年5月24日增加五字节的单灯协议,单灯的格式为两字节路段号，两字节节点号，六个字节的单灯状态
                        Com_inf.Explain_State_String_AD6(state, i) '解析状态字符串的各个含义
                        m_controllampobj.GetCompareState_AD6(Trim(rs_find_state.Fields("StatusContent").Value), rs_find_state.Fields("id").Value)  '获取终端的运行状态
                        i += 1
                        short_id = Mid(g_lampidstring, 7, LAMP_ID_LEN) + 1

                        While short_id.Length < LAMP_ID_LEN
                            short_id = "0" & short_id
                        End While
                        g_lampidstring = Mid(g_lampidstring, 1, 6) & short_id

                    End While


                End If

            End If

next1:
            rs_find_state.MoveNext()
        End While

finish:
        'Me.BackgroundWorker_find_state.ReportProgress(1) '刷新故障报警
        area_content_list_all(m_checkcondition) '刷新右侧列表
        Me.BackgroundWorker_find_state.ReportProgress(6) '刷新单灯故障报警

        If rs_find_state.State = 1 Then
            rs_find_state.Close()
            rs_find_state = Nothing
        End If
        conn.Close()
        conn = Nothing

    End Sub

    ''' <summary>
    ''' 检测当前电控箱工作是否正常，查1分钟之内是否有心跳包信息，有则正常，没有则查1分钟之内是否有状态返回
    ''' ，有则正常，没有则工作簿正常
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub find_box_state() '查询当前电控箱的工作状态
        Dim conn As New ADODB.Connection
        Dim rs, rs_state, rs_box As New ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim box_id_ox As String
        Dim ox_str As String
        Dim control_box() As m_control_box_IMEI
        Dim i, j As Integer
        Dim t As Integer
        'Dim time As System.DateTime
        Dim gprs As Integer
        Dim control_box_row As Integer
        gprs = 0

        i = 0
        j = 0
        msg = ""
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        Me.ClearDataGridviewDelegate(dgv_control_box_list) '清空列表中的所有内容
        control_box_row = 0
        '该GPRS控制几个区域
        sql = "select * from Box_IMEI order by control_box_id"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            SetTextDelegate(MSG_ERROR_STRING & "find_box_state" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)

            'heart_tag = 0
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs.RecordCount > 0 Then
            ReDim control_box(rs.RecordCount)
            While rs.EOF = False
                control_box(i).control_box_id = Trim(rs.Fields("control_box_id").Value)
                control_box(i).control_box_name = Trim(rs.Fields("control_box_name").Value)
                control_box(i).IMEI = Trim(rs.Fields("IMEI").Value)
                control_box(i).state = "未连接"
                If rs.Fields("elec_state").Value Is System.DBNull.Value Then
                    control_box(i).elec_state = ""
                Else
                    control_box(i).elec_state = Trim(rs.Fields("elec_state").Value)
                End If

                control_box(i).tag = 0 '未被标识
                i += 1
                rs.MoveNext()
            End While
        Else
            GoTo finish
        End If

        t = 0
        j = 0
        While j < i
            If control_box(j).tag = 1 Then
                j += 1
                Continue While
            End If
            sql = "select * from Heart_view where RoadIMEI='" & control_box(j).IMEI & "' and ReceiveTime>= DateAdd(n,-5,'" & Now() & "')"

            rs = DBOperation.SelectSQL(conn, sql, msg)

            If rs.RecordCount > 0 Then
                '找到心跳信息,则把相同imei的主控箱全部设置为通信正常
                control_box(j).state = "通信正常"
                t = j
                While (t < i)
                    If control_box(t).IMEI = control_box(j).IMEI And control_box(t).tag = 0 Then
                        control_box(t).tag = 1
                        control_box(t).state = "通信正常"
                    End If
                    t += 1
                End While
                j += 1

                '******************心跳包的通信流量****************************************
                'sql = "select * from Box_GPRS where control_box_id ='" & Trim(rs.Fields("control_box_id").Value) & "' order by Time desc"
                'rs_box = DBOperation.SelectSQL(conn, sql, msg)

                'If rs_box Is Nothing Then
                '    SetTextDelegate(MSG_ERROR_STRING & "find_box_state" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)

                '    If rs.State = 1 Then
                '        rs.Close()
                '        rs = Nothing
                '    End If
                '    conn.Close()
                '    conn = Nothing
                '    Exit Sub
                'End If

                'If rs_box.RecordCount > 0 Then
                '    time = rs_box.Fields("Time").Value
                '    If time.Year = Now.Year And time.Month = Now.Month And time.Day = Now.Day Then
                '        '具有当天日期的流量统计
                '        gprs = 25 + rs_box.Fields("gprs").Value  '一个电控箱的字节数
                '        sql = "update Box_GPRS set GPRS='" & gprs & "' where id='" & rs_box.Fields("ID").Value & "'"

                '    Else
                '        '没有当天的流量记录则新增
                '        gprs = 25  '一个电控箱的字节数
                '        sql = "insert into Box_GPRS(control_box_id, GPRS, Time ) values('" & Trim(rs.Fields("control_box_id").Value) & "','" & gprs & "' ,'" & Now & "')"

                '    End If
                '    DBOperation.ExecuteSQL(conn, sql, msg)
                'Else
                '    '没有当天的流量记录则新增
                '    gprs = 25  '一个电控箱的字节数
                '    sql = "insert into Box_GPRS(control_box_id, GPRS, Time ) values('" & Trim(rs.Fields("control_box_id").Value) & "','" & gprs & "' ,'" & Now & "')"

                '    DBOperation.ExecuteSQL(conn, sql, msg)
                'End If
                '********************************************************

            Else
                If SYSTEM_VISION = 1 Then  '版本1，主控箱编号为一个字节
                    ox_str = Com_inf.Dec_to_Hex(control_box(j).control_box_id, 2)
                    box_id_ox = ox_str '电控箱编号的16进制表示
                    '如果5分钟内没有心跳包信息，则查找状态表
                    sql = "select * from RoadLightStatus where StatusContent like '" & box_id_ox & "%' and Createtime>=DateAdd(n,-5,'" & Now() & "')"
                Else  '版本2，主控箱编号为两个字节
                    ox_str = Com_inf.Dec_to_Hex(control_box(j).control_box_id, 4)
                    box_id_ox = ox_str '电控箱编号的16进制表示
                    '如果5分钟内没有心跳包信息，则查找状态表
                    sql = "select * from RoadLightStatus where StatusContent like '" & Mid(box_id_ox, 3, 2) & "%' and StatusContent like '%" & Mid(box_id_ox, 1, 2) & "' and Createtime>=DateAdd(n,-5,'" & Now() & "')"

                End If
                rs_state = DBOperation.SelectSQL(conn, sql, msg)
                If rs_state.RecordCount > 0 Then
                    '找到心跳信息
                    control_box(j).state = "通信正常"
                    t = 0
                    While (t < i)
                        If control_box(t).IMEI = control_box(j).IMEI Then
                            control_box(t).tag = 1
                            control_box(t).state = "通信正常"
                        End If
                        t += 1
                    End While
                    j += 1
                Else

                    '修改记录
                    '2011年5月19 没有心跳包，查完单灯，查有该主控箱号的所有三遥数据
                    ox_str = Com_inf.Dec_to_Hex(control_box(j).control_box_id, 4)
                    box_id_ox = Mid(ox_str, 1, 2) & " " & Mid(ox_str, 3, 2) '电控箱编号的16进制表示
                    '如果两分钟内没有心跳包信息，则查找状态表
                    sql = "select * from RoadLightStatus where StatusContent like '" & box_id_ox & "%' and Createtime>=DateAdd(n,-5,'" & Now() & "') and Createtime<=DateAdd(n,5,'" & Now() & "')"


                    rs_state = DBOperation.SelectSQL(conn, sql, msg)
                    If rs_state.RecordCount > 0 Then
                        '找到心跳信息
                        control_box(j).state = "通信正常"
                        t = 0
                        While (t < i)
                            If control_box(t).IMEI = control_box(j).IMEI Then
                                control_box(t).tag = 1
                                control_box(t).state = "通信正常"
                            End If
                            t += 1
                        End While
                        'j += 1
                    End If
                    j += 1
                End If
            End If
        End While

        j = 0


        While (j < i)
            '2011年9月14日将窗体左边的通信状态去除
            'If j = i - 1 Then
            '    Me.SetDataGridviewDelegate(control_box_row, control_box(j).control_box_name, control_box(j).state, control_box(j).elec_state, dgv_control_box_list, True)
            'Else
            '    Me.SetDataGridviewDelegate(control_box_row, control_box(j).control_box_name, control_box(j).state, control_box(j).elec_state, dgv_control_box_list, False)

            'End If
            '***************************************

            '如果通信不正常，则对单灯的状态置为无状态返回，主控箱状态置为空

            If control_box(j).state = "未连接" Then
                Clear_state(control_box(j).control_box_id)
            End If

            '记录每一次的通信状态
            Setcontrolbox_Record(control_box(j).control_box_name, control_box(j).state, "通信")

            j += 1
            control_box_row += 1
        End While
finish:
        rs.Close()
        rs = Nothing
        conn.Close()
        conn = Nothing

    End Sub


    ''' <summary>
    ''' 清除主控箱的状态，单灯状态置为无状态返回，主控箱状态被清除
    ''' </summary>
    ''' <param name="control_box_id"></param>
    ''' <remarks></remarks>
    Private Sub Clear_state(ByVal control_box_id As String)

        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String

        msg = ""
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        '第一步骤，将该电控箱下单灯的状态置为无返回值,只刷新终端的，不刷新接触器的

        sql = "update lamp_inf set current_l=0, presure_l=0,power=0,presure_end=0, result=3 where control_box_id='" & control_box_id & "' and lamp_type_id=0"
        DBOperation.ExecuteSQL(conn, sql, msg)

        '第二步骤，将主控箱中的状态数据置空
        sql = "update control_box set statuscontent='' , statuscontent2='', statuscontent3='', state='', power_type='', kaiguan_string='' where control_box_id='" & control_box_id & "'"
        DBOperation.ExecuteSQL(conn, sql, msg)

        '第三步，将开关量报警列表中改主控箱的正在报警信息置为1
        sql = "update kaiguan_alarm_list set alarm_tag=1 where (control_box_name =(select control_box_name from control_box where control_box_id='" & control_box_id & "')) and alarm_tag<>1"
        DBOperation.ExecuteSQL(conn, sql, msg)

        '第四步，将主控箱状态表中正在持续的状态置为1
        sql = "update control_box_state set state=1 where control_box_name=(control_box_name =(select control_box_name from control_box where control_box_id='" & control_box_id & "')) and state=0 and kaiguan_string<>'通信'"
        DBOperation.ExecuteSQL(conn, sql, msg)
        conn.Close()
        conn = Nothing
    End Sub


    ''' <summary>
    ''' 自动抄表
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BackgroundWorker_meterdata_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker_meterdata.DoWork
        '单独检测通信是否正常
        Try
            get_meterdata()
        Catch ex As Exception
            ex.ToString()
        End Try
    End Sub


    ''' <summary>
    ''' 获取电表电量
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub get_meterdata()
        Dim i As Integer = 0
        Dim imei As String
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String
        Dim nowtime As DateTime
        Dim metertype As String
        Dim meterid As String '电表编号
        Dim cmd_string As String '获取的命令
        Dim rs As New ADODB.Recordset
        Dim chaobiaoobj As New 抄表
        Dim boxlist As New ArrayList '记录选中的IMEI


        nowtime = Now
        msg = ""
        sql = "SELECT controlbox_power.powermeter_bianbi, control_box.control_box_name,control_box.control_box_id, controlbox_power.powermeter_id, controlbox_power.powermeter_type, controlbox_power.imei FROM control_box INNER JOIN controlbox_power ON control_box.control_box_id = controlbox_power.control_box_id"
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            conn.Close()
            conn = Nothing
            Exit Sub
        End If

        SetTextDelegate("开始定时抄表......" & vbCrLf, True, rtb_info_list)

        While rs.EOF = False
            meterid = Trim(rs.Fields("powermeter_id").Value)
            imei = Trim(rs.Fields("imei").Value)
            metertype = Trim(rs.Fields("powermeter_type").Value)
            If meterid = "" Then
                GoTo next1
            End If
            If meterid.Length < 12 Then
                rs.MoveNext()
                Continue While
            End If
            meterid = chaobiaoobj.changetodcd(meterid)
            'cmd_string = "68 " & meterid & "68 11 04 33 33 34 33 "
            'cmd_string = cmd_string & chaobiaoobj.get_cstring(Trim(cmd_string)) & " 16 16"

            'If metertype = "97" Then  '97规约
            '    sql = "insert into TimeControl(RoadIMEI, CMDType, CMDContent, HandlerFlag, CreateTime)" _
            '    & " values('" & imei & "','" & HG_TYPE.HG_ASK_POWERDATA97 & "','" & cmd_string & "','" & CONTROL_BOX_TYPE2_FLAG & "','" & nowtime & "')"
            'Else '07规约
            '    sql = "insert into TimeControl(RoadIMEI, CMDType, CMDContent, HandlerFlag, CreateTime)" _
            '    & " values('" & imei & "','" & HG_TYPE.HG_ASK_POWERDATA07 & "','" & cmd_string & "','" & CONTROL_BOX_TYPE2_FLAG & "','" & nowtime & "')"

            'End If
            If metertype = "97" Then  '97规约
                cmd_string = "68 " & meterid & "68 01 02 43 C3 "
                cmd_string = cmd_string & chaobiaoobj.get_cstring(Trim(cmd_string)) & " 16 16"
                sql = "insert into TimeControl(RoadIMEI, CMDType, CMDContent, HandlerFlag, CreateTime)" _
                & " values('" & imei & "','" & HG_TYPE.HG_ASK_POWERDATA97 & "','" & cmd_string & "','" & CONTROL_BOX_TYPE2_FLAG & "','" & nowtime & "')"
            Else '07规约
                cmd_string = "68 " & meterid & "68 11 04 33 33 34 33 "
                cmd_string = cmd_string & chaobiaoobj.get_cstring(Trim(cmd_string)) & " 16 16"
                sql = "insert into TimeControl(RoadIMEI, CMDType, CMDContent, HandlerFlag, CreateTime)" _
                & " values('" & imei & "','" & HG_TYPE.HG_ASK_POWERDATA07 & "','" & cmd_string & "','" & CONTROL_BOX_TYPE2_FLAG & "','" & nowtime & "')"

            End If

            DBOperation.ExecuteSQL(conn, sql, msg)

            Dim inf As New m_boxinf
            inf.control_box_id = Trim(rs.Fields("control_box_id").Value)
            inf.control_box_name = Trim(rs.Fields("control_box_name").Value)
            inf.IMEI = imei
            inf.meterid = meterid
            inf.bianbi = rs.Fields("powermeter_bianbi").Value
            inf.metertype = metertype
            boxlist.Add(inf) '将要查询电量的主控箱的信息保存起来

next1:
            rs.MoveNext()
        End While

        get_retureDatavalue(nowtime, boxlist)

        conn.Close()
        conn = Nothing
    End Sub

    Private Sub get_retureDatavalue(ByVal createtime As Date, ByVal boxlist As ArrayList)

        Dim i As Integer = 0
        Dim waittime As Integer = 20
        Dim controlboxid As String
        Dim sql As String = ""
        Dim msg As String = ""
        Dim conn As New ADODB.Connection
        Dim rs As New ADODB.Recordset
        Dim metertype As String
        Dim data() As String
        Dim start_id As Integer = 2  '有两个字节的路段号，所以起始编号从2开始
        Dim metervalue As String '电表电能
        Dim j As Integer = 0
        Dim powerdata As Double
        Dim num As Byte
        Dim chaobiaoobj As New 抄表
        Dim inf As New m_boxinf
        Dim t As Integer = 0
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        While t < waittime

            If Me.BackgroundWorker_meterdata.CancellationPending = True Then
                Exit While
            End If
            i = 0
            While i < boxlist.Count
                inf = boxlist(i)
                metertype = inf.metertype
                controlboxid = Com_inf.Dec_to_Hex(inf.control_box_id, 4)
                controlboxid = Mid(controlboxid, 1, 2) & " " & Mid(controlboxid, 3, 2)
                If metertype = "97" Then
                    sql = "select * from RoadLightStatus where StatusContent like '" & controlboxid & "%'" _
                    & "and CreateTime>'" & createtime & "' and PackType='" & HG_TYPE.HG_ACK_POWERDATA97 & "' and HandlerFlag=3"
                Else
                    sql = "select * from RoadLightStatus where StatusContent like '" & controlboxid & "%'" _
                    & "and CreateTime>'" & createtime & "' and PackType='" & HG_TYPE.HG_ACK_POWERDATA07 & "' and HandlerFlag=3"

                End If
                rs = DBOperation.SelectSQL(conn, sql, msg)
                If rs Is Nothing Then
                    conn.Close()
                    conn = Nothing
                    Exit Sub
                End If
                If rs.RecordCount > 0 Then
                    data = Trim(rs.Fields("StatusContent").Value).Split(" ")
                    While data(start_id) <> "68"
                        start_id += 1
                    End While
                    If metertype = "97" Then
                        '验证97表号数据是否合法
                        If chaobiaoobj.check_data(start_id, data, "43", "C3", "", "") = True Then
                            metervalue = ""
                            While j < 4
                                num = System.Convert.ToByte(data(start_id + 15 - j), 16)
                                metervalue = metervalue & chaobiaoobj.getbcdcode(num - &H33)
                                j += 1
                            End While
                            '将获取的电能存入到列表中
                            powerdata = System.Convert.ToDouble(metervalue / 100)
                            '将数据记录到数据库中
                            sql = "insert into powerdata_record(control_box_name,power_readdata,powerdata,get_time) values('" & inf.control_box_name & "','" & powerdata & "','" & powerdata * inf.bianbi & "','" & Now & "')"
                            DBOperation.ExecuteSQL(conn, sql, msg)
                            '将该查找信息删除
                            boxlist.RemoveAt(i)
                            rs.Fields("HandlerFlag").Value = 1
                            rs.Update()
                            Continue While
                        End If
                    Else
                        '验证07表号数据是否合法
                        If chaobiaoobj.check_data(start_id, data, "33", "33", "34", "33") = True Then
                            metervalue = ""
                            While j < 4
                                num = System.Convert.ToByte(data(start_id + 17 - j), 16)
                                metervalue = metervalue & chaobiaoobj.getbcdcode(num - &H33)
                                j += 1
                            End While
                            '将获取的电能存入到列表中
                            powerdata = System.Convert.ToDouble(metervalue / 100)
                            '将数据记录到数据库中
                            sql = "insert into powerdata_record(control_box_name,power_readdata,powerdata,get_time) values('" & inf.control_box_name & "','" & powerdata & "','" & powerdata * inf.bianbi & "','" & Now & "')"
                            DBOperation.ExecuteSQL(conn, sql, msg)
                            '将该查找信息删除
                            boxlist.RemoveAt(i)
                            rs.Fields("HandlerFlag").Value = 1
                            rs.Update()
                            Continue While
                        End If

                    End If
                    SetTextDelegate(inf.control_box_name & "抄表完成" & vbCrLf, True, rtb_info_list)
                Else
                    SetTextDelegate("正在抄表...... " & t + 1.ToString & vbCrLf, True, rtb_info_list)
                End If
                i += 1
            End While
            t += 1
            System.Threading.Thread.Sleep(1000)
        End While
        If t < waittime Then
            i = 0
            While i < boxlist.Count
                SetTextDelegate(inf.control_box_name & "抄表失败" & vbCrLf, True, rtb_info_list)

                '将该查找信息删除
                boxlist.RemoveAt(i)
                i += 1
            End While
        Else
            SetTextDelegate("定时抄表结束" & vbCrLf, True, rtb_info_list)
        End If

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing

    End Sub



    ''' <summary>
    ''' 发送单灯状态查询命令，如有返回则通信正常，没有返回则提示通信阻塞，稍候重试
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub state_read_fun()
        '轮询景观灯的当前状态
        Dim i As Integer
        Dim rs As ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim conn As New ADODB.Connection
        Dim control_string As String
        Dim control_time As Date
        Dim find_num As Integer = 1
        Dim box_id_hex As String

        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        control_time = Now  '记录下发送控制命令时间，返回状态的时间必须在此之后，才是该命令查询的状态

        msg = ""
        sql = ""
        box_id_hex = ""
        ' time_wait = 0  '轮询的等待时间
        sql = "select * from control_box where control_box_name='" & m_controlboxnamestring & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)

        If rs Is Nothing Then  '数据库查询出错
            SetTextDelegate(MSG_ERROR_STRING & "state_read_fun" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)

            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs.RecordCount <= 0 Then
            MsgBox("不存在区域" & m_controlboxnamestring)
            rs.Close()
            rs = Nothing
            Exit Sub
        End If
        '两位的十六进制电控箱编号
        If SYSTEM_VISION = 1 Then
            control_string = Com_inf.Dec_to_Hex(Trim(rs.Fields("control_box_id").Value), 2) & " 00 00 20 11 64"  '设置控制命令
        Else
            box_id_hex = Com_inf.Dec_to_Hex(Trim(rs.Fields("control_box_id").Value), 4)
            control_string = Mid(box_id_hex, 3, 2) & " 00 00 20 11 64 " & Mid(box_id_hex, 1, 2)  '设置控制命令

        End If
        m_controllampobj.Input_db_control(control_string, Trim(rs.Fields("control_box_id").Value), "", 1, -1)   '发送控制命令


        '提示正在获取的灯的信息
        Me.SetTextLabelDelegate("正在检测通信是否正常,请稍候...", Tool, "circle_string")
        i = 0
        '等待回复信息

        While i < m_waittime
            find_state(Mid(control_string, 1, 8), control_time, i)  '查找标志终端的状态返回
            System.Threading.Thread.Sleep(1000)  '线程休眠1秒

            If m_findtag = 1 Then    '如果找到了当前终端的返回值
                Exit While

            End If
            i += 1
        End While

        If i >= m_waittime Then

            Me.SetTextLabelDelegate("通信阻塞，请稍后重试！", Tool, "circle_string")
            MsgBox("通信阻塞，请稍后重试！", , PROJECT_TITLE_STRING)

        End If

        rs.Close()
        rs = Nothing
        conn.Close()
        conn = Nothing

    End Sub

    ''' <summary>
    ''' 在数据库的状态表中查询是否有该灯的状态信息，find_tag=1表示有返回值，=0表示没有返回值
    ''' </summary>
    ''' <param name="lamp_id_tag">灯的编号</param>
    ''' <param name="control_time">控制时间</param>
    ''' <param name="progress_num">进度值</param>
    ''' <remarks></remarks>
    Private Sub find_state(ByVal lamp_id_tag As String, ByVal control_time As Date, ByVal progress_num As Integer)
        Dim msg As String
        Dim sql As String
        Dim conn As New ADODB.Connection
        ' Dim lamp_state As Integer   '灯的状态
        Dim rs As New ADODB.Recordset
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If

        msg = ""
        '对比终端状态返回值与操作值 
        sql = "select * from RoadLightStatus where StatusContent like '" & lamp_id_tag & "%' and HandlerFlag<>" & 1 & " and Createtime>'" & control_time & "' order by Createtime "

        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs.RecordCount <= 0 Then  '没有返回状态
            m_findtag = 0
            Me.BackgroundWorker_meterdata.ReportProgress(progress_num)
            rs.Close()
            rs = Nothing
            conn.Close()
            conn = Nothing
            Exit Sub
        End If

        m_findtag = 1
        sql = "update RoadLightStatus set HandlerFlag=" & 1 & "  where ID='" & rs.Fields("ID").Value & "'"
        DBOperation.ExecuteSQL(conn, sql, msg)

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing

    End Sub

    ''' <summary>
    ''' 检测通信是否正常的进度条显示
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BackgroundWorker_check_communication_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker_meterdata.ProgressChanged

        If e.ProgressPercentage = 11 Then
            Me.ToolStripProgressBar_check_communication.Visible = True
        Else
            If e.ProgressPercentage <= 10 Then
                Me.ToolStripProgressBar_check_communication.Value = e.ProgressPercentage * 10
            Else
                Me.ToolStripProgressBar_check_communication.Value = 100
            End If

        End If
    End Sub

    ''' <summary>
    ''' 检测通信正常发送控制命令，检测通信阻塞则提示稍候重试
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BackgroundWorker_check_communication_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker_meterdata.RunWorkerCompleted
        'If m_findtag = 1 Then
        '    '发现返回状态则发送控制命令
        '    If all_open.Checked = True Then
        '        open_all_lamp()   '终端全开
        '    End If
        '    If all_close.Checked = True Then

        '        close_all_lamp()   '终端全闭
        '    End If
        '    If single_all_open.Checked = True Then

        '        single_open_control(1, 1)  '终端奇开
        '    End If
        '    If single_all_close.Checked = True Then

        '        single_open_control(0, 1)   '终端奇关
        '    End If
        '    If double_all_open.Checked = True Then

        '        single_open_control(1, 0)  '终端偶开
        '    End If
        '    If double_all_close.Checked = True Then

        '        single_open_control(0, 0)   '终端偶关
        '    End If

        '    If open_1_3.Checked = True Then
        '        open_1_3_control()
        '    End If
        'End If
        'Me.ToolStripProgressBar_check_communication.Visible = False
        'Me.SetTextLabelDelegate("发送控制命令", Tool, "circle_string")
        'm_findtag = 0  '找寻标志清0
    End Sub

    '''' <summary>
    '''' 打开所有的终端，按区域、类型和单灯三个级别
    '''' </summary>
    '''' <remarks></remarks>
    'Private Sub open_all_lamp()   '打开所有终端
    '    If box_control.Checked = False And lamp_id_control.Checked = False And type_control.Checked = False Then
    '        MsgBox("请选择控制类型！", , PROJECT_TITLE_STRING)  '如果没有选择控制类型，提示选择控制类型后再进行下一步操作
    '        Exit Sub
    '    End If
    '    Dim power_string As String

    '    power_string = "100"  '转换功率的值

    '    '按电控箱查询
    '    If box_control.Checked = True Then
    '        If box_all.Text = "" Then   '如果电控箱文本框为空
    '            MsgBox("请选择区域编号！", , PROJECT_TITLE_STRING)
    '            box_all.Focus()  '光标定位在电控箱文本框
    '            Exit Sub
    '        End If
    '        '判断是否输入控制命令
    '        If MsgBox("通信正常，是否打开区域: " & Trim(box_all.Text) & "的所有终端?", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.No Then
    '            Exit Sub
    '        End If

    '        '输入电控箱控制命令：
    '        m_controllampobj.Input_control_inf("", "主控箱", Trim(box_all.Text), "全开", 1, Trim(diangan.Text), power_string, "主控箱")
    '        '关闭需要暗的灯
    '        ' Com_inf.Turn_off_lamp(Trim(box_all.Text), "", "")
    '    End If

    '    If type_control.Checked = True Then  '按类型查询
    '        If box_all.Text = "" Then
    '            MsgBox("请选择区域编号！", , PROJECT_TITLE_STRING)
    '            box_all.Focus()  '光标定位在电控箱文本框
    '            Exit Sub
    '        End If
    '        If lamp_type_all.Text = "" Then
    '            MsgBox("请选择灯类型！", , PROJECT_TITLE_STRING)
    '            lamp_type_all.Focus()
    '            Exit Sub
    '        End If
    '        If MsgBox("通信正常，是否打开区域: " & Trim(box_all.Text) & "中所有" & Trim(lamp_type_all.Text) & "?", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.No Then
    '            Exit Sub
    '        End If
    '        '输入按电控箱下某一类型控制的命令
    '        m_controllampobj.Input_control_inf(Trim(lamp_type_all.Text), "类型", Trim(box_all.Text), "类型全开", 1, Trim(diangan.Text), power_string, "类型")
    '        '关闭需要暗的灯
    '        ' Com_inf.Turn_off_lamp(Trim(box_all.Text), Trim(lamp_type_all.Text), "")
    '    End If

    '    If lamp_id_control.Checked = True Then  '按景观灯编号查询
    '        If MsgBox("通信正常，是否打开区域: " & Trim(box_all.Text) & "中第" & Trim(lamp_id_all.Text) & "号" & Trim(lamp_type_all.Text), MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.No Then
    '            Exit Sub
    '        End If
    '        m_controllampobj.open_close_single_lamp(Trim(box_all.Text), Trim(lamp_type_all.Text), Trim(lamp_id_all_start.Text) & Trim(lamp_id_all.Text), 1, Trim(diangan.Text), 0)
    '        '关闭需要暗的灯
    '        'Com_inf.Turn_off_lamp(Trim(box_all.Text), Trim(lamp_type_all.Text), Trim(lamp_id_all_start.Text) & Trim(lamp_id_all.Text))
    '    End If

    '    If box_control.Checked = True Then  '电控箱控制
    '        MsgBox("区域：" & Trim(box_all.Text) & " 打开！", , PROJECT_TITLE_STRING)
    '        Exit Sub
    '    End If
    '    If type_control.Checked = True Then  '按类型控制
    '        MsgBox("区域：" & Trim(box_all.Text) & " " & Trim(lamp_type_all.Text) & " 打开！", , PROJECT_TITLE_STRING)
    '        Exit Sub
    '    End If

    '    If lamp_id_control.Checked = True Then '按景观灯编号控制
    '        MsgBox("区域：" & Trim(box_all.Text) & " " & Trim(lamp_type_all.Text) & " " & Trim(lamp_id_all.Text) & " 打开！", , PROJECT_TITLE_STRING)
    '        Exit Sub
    '    End If

    '    MsgBox("打开灯出错！", , PROJECT_TITLE_STRING)

    'End Sub

    '''' <summary>
    '''' 隔两盏的方式打开终端，分为区域、类型、单灯三个级别
    '''' </summary>
    '''' <remarks></remarks>
    'Private Sub open_1_3_control()
    '    Dim power_string As String
    '    If box_control.Checked = False Then
    '        MsgBox("请选择控制类型！", , PROJECT_TITLE_STRING)  '如果没有选择控制类型，提示选择控制类型后再进行下一步操作
    '        Exit Sub
    '    End If
    '    power_string = "100"  '转换功率的值

    '    If box_control.Checked = True Then  '按电控箱查询
    '        If box_all.Text = "" Then   '如果电控箱文本框为空
    '            MsgBox("请选择区域编号！", , PROJECT_TITLE_STRING)
    '            box_all.Focus()  '光标定位在电控箱文本框
    '            Exit Sub
    '        End If
    '        If MsgBox("通信正常，是否打开区域: " & Trim(box_all.Text) & "的1/3编号灯?", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.No Then
    '            Exit Sub
    '        End If
    '        '输入电控箱控制命令：
    '        m_controllampobj.Input_control_inf("", "主控箱", Trim(box_all.Text), "1/3开", 1, Trim(diangan.Text), power_string, "主控箱")
    '        '关闭需要暗的灯
    '        'Com_inf.Turn_off_lamp(Trim(box_all.Text), "", "")
    '    End If

    '    If box_control.Checked = True Then  '按电控箱控制
    '        MsgBox(Trim(box_all.Text) & " 号区域1/3编号灯打开", , PROJECT_TITLE_STRING)
    '        circle_string.Text = Trim(box_all.Text) & " 号区域1/3编号灯打开"

    '    End If


    'End Sub

    '''' <summary>
    '''' 按单号开还是双号开关
    '''' </summary>
    '''' <param name="open">open :0,关，1 开</param>
    '''' <param name="single_double">single_double 1，奇  0,偶</param>
    '''' <remarks></remarks>
    'Private Sub single_open_control(ByVal open As Integer, ByVal single_double As Integer)  'open :0,关，1 开；single_double 1，奇  0,偶
    '    If type_control.Checked = False Then
    '        MsgBox("请选择控制类型！", , PROJECT_TITLE_STRING)  '如果没有选择控制类型，提示选择控制类型后再进行下一步操作
    '        Exit Sub
    '    End If

    '    If open = 1 Then  'open=1表示单/双号终端开
    '        If power.Text = "" Then  '如果没输入功率，则提示输入功率后再进行下一步操作
    '            MsgBox("请输入功率！", , PROJECT_TITLE_STRING)
    '            power.Focus()  '光标定位在功率文本框
    '            Exit Sub
    '        Else
    '            If IsNumeric(Trim(power.Text)) = False Or Val(Trim(power.Text)) <= 0 Or Val(Trim(power.Text)) > 100 Then  '功率的值不在1%-100%之间
    '                MsgBox("功率值必须为1%-100%之间的数值，请重新输入!", , PROJECT_TITLE_STRING)
    '                power.Focus() '光标定位在功率文本框
    '                Exit Sub
    '            End If
    '        End If
    '        If diangan.Text = "" Then
    '            MsgBox("请选择电感控制方法", , PROJECT_TITLE_STRING)
    '            diangan.Focus()
    '            Exit Sub
    '        End If

    '    End If

    '    If city_all.Text = "" Then  '如果城市文本框为空
    '        MsgBox("请选择城市名称！", , PROJECT_TITLE_STRING)
    '        city_all.Focus()  '光标定位在城市文本框
    '        Exit Sub
    '    End If


    '    If type_control.Checked = True Then  '按电控箱控制

    '        If box_all.Text = "" Then  '如果电控箱文本框为空
    '            MsgBox("请选择区域编号！", , PROJECT_TITLE_STRING)
    '            box_all.Focus()  '光标定位在电控箱文本框
    '            Exit Sub
    '        End If
    '        If lamp_type_all.Text = "" Then
    '            MsgBox("请选择灯类型!", , PROJECT_TITLE_STRING)
    '            lamp_type_all.Focus()
    '            Exit Sub
    '        End If

    '        If open = 1 And single_double = 1 Then '单号终端开
    '            If MsgBox("通信正常，是否打开区域: " & Trim(box_all.Text) & "中所有单号" & Trim(lamp_type_all.Text) & "?", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.No Then
    '                Exit Sub
    '            End If
    '            m_controllampobj.Input_control_inf(Trim(lamp_type_all.Text), "类型", Trim(box_all.Text), "类型奇开", 1, Trim(diangan.Text), Trim(power.Text), "类型")

    '        Else
    '            If open = 0 And single_double = 1 Then  '单号终端关
    '                If MsgBox("通信正常，是否关闭区域: " & Trim(box_all.Text) & "中所有单号" & Trim(lamp_type_all.Text) & "?", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.No Then
    '                    Exit Sub
    '                End If
    '                m_controllampobj.Input_control_inf(Trim(lamp_type_all.Text), "类型", Trim(box_all.Text), "类型奇闭", 0, "关闭灯", "0", "类型")
    '            Else
    '                If open = 1 And single_double = 0 Then  '双号终端开
    '                    If MsgBox("通信正常，是否打开区域: " & Trim(box_all.Text) & "中所有双号" & Trim(lamp_type_all.Text) & "?", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.No Then
    '                        Exit Sub
    '                    End If
    '                    m_controllampobj.Input_control_inf(Trim(lamp_type_all.Text), "类型", Trim(box_all.Text), "类型偶开", 1, Trim(diangan.Text), Trim(power.Text), "类型")

    '                Else

    '                    If open = 0 And single_double = 0 Then  '双号终端关
    '                        If MsgBox("通信正常，是否关闭区域: " & Trim(box_all.Text) & "中所有双号" & Trim(lamp_type_all.Text) & "?", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.No Then
    '                            Exit Sub
    '                        End If
    '                        m_controllampobj.Input_control_inf(Trim(lamp_type_all.Text), "类型", Trim(box_all.Text), "类型偶闭", 0, "关闭灯", "0", "类型")
    '                    Else
    '                        MsgBox("奇偶控制出错！", , PROJECT_TITLE_STRING)
    '                    End If

    '                End If
    '            End If

    '        End If
    '    End If
    '    '关闭需要暗的灯
    '    'Com_inf.Turn_off_lamp(Trim(box_all.Text), Trim(lamp_type_all.Text), "")

    '    Dim open_close As String
    '    If open = 1 Then
    '        open_close = "打开"
    '    Else
    '        open_close = "关闭"
    '    End If

    '    If box_control.Checked = True Then  '按电控箱控制
    '        If single_double = 1 Then  '显示单号终端开关信息
    '            MsgBox(Trim(box_all.Text) & " 号区域单号灯" & open_close, , PROJECT_TITLE_STRING)
    '            circle_string.Text = Trim(box_all.Text) & " 号区域单号灯" & open_close
    '        Else  '显示双号终端开关信息
    '            MsgBox(Trim(box_all.Text) & " 号区域双号灯" & open_close, , PROJECT_TITLE_STRING)
    '            circle_string.Text = Trim(box_all.Text) & " 号区域双号灯" & open_close
    '        End If
    '    End If


    'End Sub

    '''' <summary>
    '''' 关闭所有的终端,按区域、类型和单灯范围进行控制
    '''' </summary>
    '''' <remarks></remarks> 
    'Private Sub close_all_lamp()
    '    If box_control.Checked = False And lamp_id_control.Checked = False And type_control.Checked = False Then
    '        MsgBox("请选择控制类型！", , PROJECT_TITLE_STRING)  '如果没有选择控制类型，提示选择控制类型后再进行下一步操作
    '        Exit Sub
    '    End If
    '    If box_control.Checked = True Then  '按区域范围进行控制
    '        If box_all.Text = "" Then  '如果区域文本框为空
    '            MsgBox("请选择主控箱！", , PROJECT_TITLE_STRING)
    '            box_all.Focus()  '光标定位在区域文本框
    '            Exit Sub
    '        End If
    '        '进行关闭确认
    '        If MsgBox("通信正常，是否关闭主控箱: " & Trim(box_all.Text) & "的所有灯?", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.No Then
    '            Exit Sub

    '        End If
    '        '按区域进行控制，向数据库中输入控制命令
    '        m_controllampobj.Input_control_inf("", "主控箱", Trim(box_all.Text), "全闭", 0, "关闭灯", "0", "主控箱")
    '    End If


    '    If type_control.Checked = True Then  '按类型进行控制
    '        If box_all.Text = "" Then  '如果区域文本框为空
    '            MsgBox("请选择主控箱！", , PROJECT_TITLE_STRING)
    '            box_all.Focus()  '光标定位在区域文本框
    '            Exit Sub
    '        End If
    '        If lamp_type_all.Text = "" Then '如果类型文本框为空
    '            MsgBox("请选择景观灯类型", , PROJECT_TITLE_STRING)
    '            Exit Sub
    '        End If
    '        '进行关闭确认
    '        If MsgBox("通信正常，是否关闭主控箱: " & Trim(box_all.Text) & "中所有" & Trim(lamp_type_all.Text) & "?", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.No Then
    '            Exit Sub
    '        End If
    '        '按类型进行控制，向数据库发送控制命令
    '        m_controllampobj.Input_control_inf(Trim(lamp_type_all.Text), "类型", Trim(box_all.Text), "类型全闭", 0, "关闭灯", "0", "类型")


    '    End If

    '    If lamp_id_control.Checked = True Then  '按单灯控制
    '        '进行关闭确认
    '        If MsgBox("通信正常，是否关闭主控箱: " & Trim(box_all.Text) & "中第" & Trim(lamp_id_all.Text) & "号" & Trim(lamp_type_all.Text), MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.No Then
    '            Exit Sub
    '        End If
    '        '按单灯进行控制，向数据库发送控制命令
    '        m_controllampobj.open_close_single_lamp(Trim(box_all.Text), Trim(lamp_type_all.Text), Trim(lamp_id_all_start.Text) & Trim(lamp_id_all.Text), 0, "关闭灯", 0)

    '    End If

    '    If box_control.Checked = True Then

    '        '按电控箱，提示关闭信息
    '        MsgBox("主控箱：" & Trim(box_all.Text) & " 关闭！", , PROJECT_TITLE_STRING)
    '        Exit Sub
    '    End If

    '    If type_control.Checked = True Then  '按类型，提示关闭信息


    '        MsgBox("主控箱：" & Trim(box_all.Text) & " " & Trim(lamp_type_all.Text) & " 关闭！", , PROJECT_TITLE_STRING)
    '        Exit Sub
    '    End If

    '    If lamp_id_control.Checked = True Then  '按单灯，提示关闭信息

    '        MsgBox("主控箱：" & Trim(box_all.Text) & " " & Trim(lamp_type_all.Text) & " " & Trim(lamp_id_all.Text) & " 关闭！", , PROJECT_TITLE_STRING)
    '        Exit Sub
    '    End If

    '    MsgBox("关闭灯出错！", , PROJECT_TITLE_STRING)


    'End Sub

    Private Sub rb_all_lamp_check_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_all_lamp_check.Click
        If rb_all_lamp_check.Checked = True Then
            Me.cb_area_content.Visible = False
            Me.street_Lamp_Content_String.Visible = False
            m_checkcondition = 0
            m_checkvalue = ""
        End If
    End Sub

    Private Sub rb_street_lamp_check_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_box_lamp_check.Click
        If rb_box_lamp_check.Checked = True Then
            Me.cb_area_content.Visible = True
            Me.street_Lamp_Content_String.Visible = True

            m_checkvalue = Trim(Me.cb_area_content.Text)
        End If
    End Sub

    Private Sub bt_check_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_check.Click
        If Me.rb_all_lamp_check.Checked = True Then
            area_content_list_all(0)
            m_checkcondition = 0
        Else
            If Me.cb_area_content.Text = "" Then
                MsgBox("请选择主控箱名称", , PROJECT_TITLE_STRING)
                Exit Sub
            End If
            area_content_list_all(1)
            Me.m_checkcondition = 1
        End If
    End Sub

    Private Sub Area_Lamp_Content_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_area_content.SelectedIndexChanged
        m_checkvalue = Trim(Me.cb_area_content.Text)
    End Sub

    Private Sub dgv_lamp_state_list_ColumnHeaderMouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgv_lamp_state_list.ColumnHeaderMouseClick
        area_content_list_all(m_checkcondition)

    End Sub

    Private Sub box_control_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles box_control.Click
        If box_control.Checked = True Then
            lamp_type_all.Enabled = False
            lamp_id_all.Enabled = False
            single_all_open.Enabled = False
            double_all_open.Enabled = False
            open_1_3.Enabled = True
            all_open.Checked = True
        End If
    End Sub

    Private Sub lamp_id_control_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lamp_id_control.Click
        If lamp_id_control.Checked = True Then
            lamp_id_all.Enabled = True
            lamp_type_all.Enabled = True
            single_all_open.Enabled = False
            double_all_open.Enabled = False
            open_1_3.Enabled = False
            all_open.Checked = True  '选择单灯控制模式，默认情况下为开灯
        End If
    End Sub

    Private Sub box_all_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles box_all.DropDown
        Com_inf.Select_box_name(box_all)
        m_controlboxnamestring = Trim(box_all.Text)
    End Sub

    Private Sub box_all_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles box_all.SelectedIndexChanged
        If box_all.Items.Count > 0 Then
            Com_inf.Select_lamp_id_type(box_all, lamp_type_all, lamp_id_all, lamp_id_all_start)
            m_controlboxnamestring = Trim(box_all.Text)
        End If
    End Sub

    Private Sub lamp_id_all_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lamp_id_all.DropDown
        Com_inf.Select_lamp_id_type(box_all, lamp_type_all, lamp_id_all, lamp_id_all_start)

    End Sub

    ''' <summary>
    ''' 手工控制面板中的执行函数（目前不用）
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_operation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_operation.Click
        If all_open.Checked = False And all_close.Checked = False And single_all_open.Checked = False And single_all_close.Checked = False And double_all_open.Checked = False And double_all_close.Checked = False And open_1_3.Checked = False Then
            MsgBox("请选择控制方式！", , PROJECT_TITLE_STRING)  '如果没有选择控制方式，提示需选择控制方式才能下一步操作
            all_open.Focus()
            Exit Sub
        End If
        ' Com_inf.Turn_off_lamp() '关闭所有必须为暗的灯
        If Me.BackgroundWorker_meterdata.IsBusy = False Then
            Me.BackgroundWorker_meterdata.RunWorkerAsync()

        Else
            MsgBox("正在检测通信，请稍候重试")
        End If

        circle_string.Text = "手工操作"
    End Sub

    Private Sub box_time_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles box_time.DropDown
        Com_inf.Select_box_name_level(city_time, area_time, street_time, box_time)
    End Sub

    ''' <summary>
    ''' 设置时段控制模式
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub div_set_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles div_set.Click
        Dim div_time_obj As New div_time_class
        If city_time_control.Checked = False And area_time_control.Checked = False And street_time_control.Checked = False And box_time_control.Checked = False And type_time_control.Checked = False Then
            MsgBox("请选择控制类型！", , PROJECT_TITLE_STRING)  '如果没有选择控制类型，则提示选择控制类型后，进行下一步操作
            Exit Sub
        Else
            If city_time_control.Checked = True Then  '按城市进行时段分配
                If city_time.Text = "" Then
                    MsgBox("请选择城市名称", , PROJECT_TITLE_STRING)
                    city_time.Focus()
                    Exit Sub
                End If
                If Me.div_time_control.Checked = True Then  '增加特殊的控制模式
                    div_time_obj.Div_control_function("city", Trim(city_time.Text), Trim(mod_time.Text), False)
                Else   '增加平时的控制模式
                    div_time_obj.Div_control_function("city", Trim(city_time.Text), Trim(mod_time.Text), True)
                End If
            End If
            If area_time_control.Checked = True Then  '按区域进行时段分配
                If city_time.Text = "" Then
                    MsgBox("请选择城市名称", , PROJECT_TITLE_STRING)
                    city_time.Focus()
                    Exit Sub
                End If
                If area_time.Text = "" Then
                    MsgBox("请选择区域名称", , PROJECT_TITLE_STRING)
                    area_time.Focus()
                    Exit Sub
                End If
                If Me.div_time_control.Checked = True Then '增加特殊的控制模式
                    div_time_obj.Div_control_function("area", Trim(area_time.Text), Trim(mod_time.Text), False)
                Else  '增加平时的控制模式
                    div_time_obj.Div_control_function("area", Trim(area_time.Text), Trim(mod_time.Text), True)
                End If
            End If
            If street_time_control.Checked = True Then  '按街道进行时段分配
                If city_time.Text = "" Then
                    MsgBox("请选择城市名称", , PROJECT_TITLE_STRING)
                    city_time.Focus()
                    Exit Sub
                End If
                If area_time.Text = "" Then
                    MsgBox("请选择区域名称", , PROJECT_TITLE_STRING)
                    area_time.Focus()
                    Exit Sub
                End If
                If street_time.Text = "" Then
                    MsgBox("请选择街道名称", , PROJECT_TITLE_STRING)
                    street_time.Focus()
                    Exit Sub
                End If
                If Me.div_time_control.Checked = True Then  '增加特殊的控制模式
                    div_time_obj.Div_control_function("street", Trim(street_time.Text), Trim(mod_time.Text), False)
                Else '增加平时的控制模式
                    div_time_obj.Div_control_function("street", Trim(street_time.Text), Trim(mod_time.Text), True)
                End If
            End If
            If box_time_control.Checked = True Then  '设置区域控制下的景观灯
                If city_time.Text = "" Then
                    MsgBox("请选择城市名称", , PROJECT_TITLE_STRING)
                    city_time.Focus()
                    Exit Sub
                End If
                If area_time.Text = "" Then
                    MsgBox("请选择区域名称", , PROJECT_TITLE_STRING)
                    area_time.Focus()
                    Exit Sub
                End If
                If street_time.Text = "" Then
                    MsgBox("请选择街道名称", , PROJECT_TITLE_STRING)
                    street_time.Focus()
                    Exit Sub
                End If
                If box_time.Text = "" Then
                    MsgBox("请选择主控箱名称", , PROJECT_TITLE_STRING)
                    box_time.Focus()
                    Exit Sub
                End If
                '按区域范围进行设置
                If Me.div_time_control.Checked = True Then  '增加特殊的控制模式
                    div_time_obj.Div_control_function("box", Trim(box_time.Text), Trim(mod_time.Text), False)
                Else  '增加平时的控制模式
                    div_time_obj.Div_control_function("box", Trim(box_time.Text), Trim(mod_time.Text), True)
                End If
            End If
            If type_time_control.Checked = True Then  '设置某一类型的景观灯
                If city_time.Text = "" Then
                    MsgBox("请选择城市名称", , PROJECT_TITLE_STRING)
                    city_time.Focus()
                    Exit Sub
                End If
                If area_time.Text = "" Then
                    MsgBox("请选择区域名称", , PROJECT_TITLE_STRING)
                    area_time.Focus()
                    Exit Sub
                End If
                If street_time.Text = "" Then
                    MsgBox("请选择街道名称", , PROJECT_TITLE_STRING)
                    street_time.Focus()
                    Exit Sub
                End If
                If box_time.Text = "" Then
                    MsgBox("请选择主控箱名称", , PROJECT_TITLE_STRING)
                    box_time.Focus()
                    Exit Sub
                End If
                If type_time.Text = "" Then
                    MsgBox("请类型名称", , PROJECT_TITLE_STRING)
                    type_time.Focus()
                    Exit Sub
                End If
                '按类型范围进行设置
                If Me.div_time_control.Checked = True Then  '增加特殊的控制模式
                    div_time_obj.type_control_function(Trim(box_time.Text), Trim(type_time.Text), Trim(mod_time.Text), False)
                Else  '增加平时的控制模式
                    div_time_obj.type_control_function(Trim(box_time.Text), Trim(type_time.Text), Trim(mod_time.Text), True)
                End If
            End If
        End If
        div_time_obj.get_treelist_inf()  '刷新时段的树形列表
    End Sub

    ''' <summary>
    ''' 清除时段控制模式
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub div_clear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles div_clear.Click
        Dim div_time_obj As New div_time_class
        Dim rs, rs_lv As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        msg = ""
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        If city_time_control.Checked = True Then '按照城市删除
            If city_time.Text = "" Then
                MsgBox("请选择城市名称", , PROJECT_TITLE_STRING)
                city_time.Focus()
                Exit Sub
            End If
            If mod_time.Text = "" Then
                MsgBox("请选择模式级别", , PROJECT_TITLE_STRING)
                mod_time.Focus()
                Exit Sub
            End If
            sql = "select distinct(control_box_id) from lamp_street where city='" & Trim(city_time.Text) & "'"
            rs = DBOperation.SelectSQL(conn, sql, msg)
            If MsgBox("是否删除城市：" & Trim(city_time.Text) & "的模式信息", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.Yes Then
                While rs.EOF = False
                    If Me.div_time_control.Checked = True Then  '平时控制模式
                        sql = "delete from road_level where control_box_id='" & Trim(rs.Fields("control_box_id").Value) & "' and div_time_level='" & Trim(mod_time.Text) & "' and lamp_id is NULL"
                    Else  '特殊控制模式
                        sql = "delete from special_road_level where control_box_id='" & Trim(rs.Fields("control_box_id").Value) & "' and div_time_level='" & Trim(mod_time.Text) & "' and lamp_id is NULL"
                    End If
                    DBOperation.ExecuteSQL(conn, sql, msg)
                    rs.MoveNext()
                End While
                MsgBox("删除成功", , PROJECT_TITLE_STRING)
            End If
        End If
        If area_time_control.Checked = True Then   '按照区域删除
            If city_time.Text = "" Then
                MsgBox("请选择城市名称", , PROJECT_TITLE_STRING)
                city_time.Focus()
                Exit Sub
            End If
            If area_time.Text = "" Then
                MsgBox("请选择城市名称", , PROJECT_TITLE_STRING)
                area_time.Focus()
                Exit Sub
            End If
            If mod_time.Text = "" Then
                MsgBox("请选择模式级别", , PROJECT_TITLE_STRING)
                mod_time.Focus()
                Exit Sub
            End If
            sql = "select distinct(control_box_id) from lamp_street where city='" & Trim(city_time.Text) & "' and area ='" & Trim(area_time.Text) & "'"
            rs = DBOperation.SelectSQL(conn, sql, msg)
            If MsgBox("是否删除区域：" & Trim(area_time.Text) & "的模式信息", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.Yes Then
                While rs.EOF = False
                    If Me.div_time_control.Checked = True Then  '平时控制模式
                        sql = "delete from road_level where control_box_id='" & Trim(rs.Fields("control_box_id").Value) & "' and div_time_level='" & Trim(mod_time.Text) & "' and lamp_id is NULL"
                    Else  '特殊控制模式
                        sql = "delete from special_road_level where control_box_id='" & Trim(rs.Fields("control_box_id").Value) & "' and div_time_level='" & Trim(mod_time.Text) & "' and lamp_id is NULL"
                    End If
                    DBOperation.ExecuteSQL(conn, sql, msg)
                    rs.MoveNext()
                End While
                MsgBox("删除成功", , PROJECT_TITLE_STRING)
            End If
        End If
        If street_time_control.Checked = True Then     '按照街道删除
            If city_time.Text = "" Then
                MsgBox("请选择城市名称", , PROJECT_TITLE_STRING)
                city_time.Focus()
                Exit Sub
            End If
            If area_time.Text = "" Then
                MsgBox("请选择区域名称", , PROJECT_TITLE_STRING)
                area_time.Focus()
                Exit Sub
            End If
            If street_time.Text = "" Then
                MsgBox("请选择街道名称", , PROJECT_TITLE_STRING)
                street_time.Focus()
                Exit Sub
            End If
            If mod_time.Text = "" Then
                MsgBox("请选择模式级别", , PROJECT_TITLE_STRING)
                mod_time.Focus()
                Exit Sub
            End If
            sql = "select distinct(control_box_id) from lamp_street where city='" & Trim(city_time.Text) & "' and area ='" & Trim(area_time.Text) & "' and street='" & Trim(street_time.Text) & "'"
            rs = DBOperation.SelectSQL(conn, sql, msg)
            If MsgBox("是否删除街道：" & Trim(street_time.Text) & "的模式信息", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.Yes Then
                While rs.EOF = False
                    If Me.div_time_control.Checked = True Then  '平时控制模式
                        sql = "delete from road_level where control_box_id='" & Trim(rs.Fields("control_box_id").Value) & "' and div_time_level='" & Trim(mod_time.Text) & "' and lamp_id is NULL"
                    Else  '特殊控制模式
                        sql = "delete from special_road_level where control_box_id='" & Trim(rs.Fields("control_box_id").Value) & "' and div_time_level='" & Trim(mod_time.Text) & "' and lamp_id is NULL"

                    End If
                    DBOperation.ExecuteSQL(conn, sql, msg)
                    rs.MoveNext()
                End While
                MsgBox("删除成功", , PROJECT_TITLE_STRING)
            End If
        End If
        If box_time_control.Checked = True Then  '按电控箱级别删除
            If city_time.Text = "" Then
                MsgBox("请选择城市名称", , PROJECT_TITLE_STRING)
                city_time.Focus()
                Exit Sub
            End If
            If area_time.Text = "" Then
                MsgBox("请选择区域名称", , PROJECT_TITLE_STRING)
                area_time.Focus()
                Exit Sub
            End If
            If street_time.Text = "" Then
                MsgBox("请选择街道名称", , PROJECT_TITLE_STRING)
                street_time.Focus()
                Exit Sub
            End If
            If box_time.Text = "" Then
                MsgBox("请选择区域名称", , PROJECT_TITLE_STRING)
                box_time.Focus()
                Exit Sub
            End If
            If mod_time.Text = "" Then
                MsgBox("请选择模式级别", , PROJECT_TITLE_STRING)
                mod_time.Focus()
                Exit Sub
            End If
            sql = "select * from control_box where control_box_name='" & Trim(box_time.Text) & "'"
            rs = DBOperation.SelectSQL(conn, sql, msg)
            If rs.RecordCount > 0 Then
                If MsgBox("是否删除电控箱：" & Trim(box_time.Text) & "的模式信息", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.Yes Then
                    While rs.EOF = False
                        If Me.div_time_control.Checked = True Then  '平时控制模式
                            sql = "delete from road_level where control_box_id='" & Trim(rs.Fields("control_box_id").Value) & "' and div_time_level='" & Trim(mod_time.Text) & "' and lamp_id is NULL"
                        Else  '特殊控制模式
                            sql = "delete from special_road_level where control_box_id='" & Trim(rs.Fields("control_box_id").Value) & "' and div_time_level='" & Trim(mod_time.Text) & "' and lamp_id is NULL"
                        End If
                        DBOperation.ExecuteSQL(conn, sql, msg)
                        rs.MoveNext()
                    End While
                    MsgBox("删除成功", , PROJECT_TITLE_STRING)
                End If
            End If
        End If

        If type_time_control.Checked = True Then
            If city_time.Text = "" Then
                MsgBox("请选择城市名称", , PROJECT_TITLE_STRING)
                city_time.Focus()
                Exit Sub
            End If
            If area_time.Text = "" Then
                MsgBox("请选择区域名称", , PROJECT_TITLE_STRING)
                area_time.Focus()
                Exit Sub
            End If
            If street_time.Text = "" Then
                MsgBox("请选择街道名称", , PROJECT_TITLE_STRING)
                street_time.Focus()
                Exit Sub
            End If
            If box_time.Text = "" Then
                MsgBox("请选择区域名称", , PROJECT_TITLE_STRING)
                box_time.Focus()
                Exit Sub
            End If
            If type_time.Text = "" Then
                MsgBox("请选择类型名称", , PROJECT_TITLE_STRING)
                type_time.Focus()
                Exit Sub
            End If
            If mod_time.Text = "" Then
                MsgBox("请选择模式级别", , PROJECT_TITLE_STRING)
                mod_time.Focus()
                Exit Sub
            End If
            sql = "select * from box_lamptype_view where control_box_name='" & Trim(box_time.Text) & "' and type_string='" & Trim(type_time.Text) & "'"

            rs = DBOperation.SelectSQL(conn, sql, msg)
            If rs.RecordCount > 0 Then
                If Me.div_time_control.Checked = True Then  '平时控制模式
                    sql = "select * from road_level where control_box_id='" & Trim(rs.Fields("control_box_id").Value) & "' and div_time_level='" & Trim(mod_time.Text) & "' and type_id='" & Trim(rs.Fields("type_id").Value) & "' and lamp_id is NULL"
                Else  '特殊控制模式
                    sql = "select * from special_road_level where control_box_id='" & Trim(rs.Fields("control_box_id").Value) & "' and div_time_level='" & Trim(mod_time.Text) & "' and type_id='" & Trim(rs.Fields("type_id").Value) & "' and lamp_id is NULL"
                End If
                rs_lv = DBOperation.SelectSQL(conn, sql, msg)
                If rs_lv.RecordCount > 0 Then
                    If MsgBox("是否删除" & Trim(box_time.Text) & " " & Trim(type_time.Text) & "的模式信息", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.Yes Then
                        If Me.div_time_control.Checked = True Then
                            sql = "delete from road_level where control_box_id='" & Trim(rs.Fields("control_box_id").Value) & "' and div_time_level='" & Trim(mod_time.Text) & "' and type_id='" & Trim(rs.Fields("type_id").Value) & "' and lamp_id is NULL"
                        Else
                            sql = "delete from special_road_level where control_box_id='" & Trim(rs.Fields("control_box_id").Value) & "' and div_time_level='" & Trim(mod_time.Text) & "' and type_id='" & Trim(rs.Fields("type_id").Value) & "' and lamp_id is NULL"
                        End If
                        DBOperation.ExecuteSQL(conn, sql, msg)
                        MsgBox("删除成功", , PROJECT_TITLE_STRING)
                    End If
                Else
                    MsgBox("不存在该模式信息，请重新选择", , PROJECT_TITLE_STRING)
                End If
            End If
        End If
        div_time_obj.get_treelist_inf()  '刷新时段的树形列表
    End Sub

    Private Sub Area_Lamp_Content_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_area_content.DropDown
        'Com_inf.Select_box_name(Me.Area_Lamp_Content)
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        msg = ""
        sql = "select control_box_name from control_box"
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        cb_area_content.Items.Clear()
        While rs.EOF = False
            cb_area_content.Items.Add(Trim(rs.Fields("control_box_name").Value))
            rs.MoveNext()
        End While
        rs.Close()
        rs = Nothing
        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    ''' 鼠标右键，打开所选的灯
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 打开ToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 打开ToolStripMenuItem2.Click
        '单击右键后，可打开所点击的终端
        Dim rs As New ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim box_id As String = ""
        Dim imei As String = ""
        Dim conn As New ADODB.Connection
        'Dim type_id As Integer   '灯类型的编号
        'Dim lamp_id_bin As String  '灯的编号的十六位二进制
        'Dim control_box_id As String  '电控箱编号
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        Dim time As Date
        msg = ""
        'Dim box_id_hex As String '电控箱编号的十六进制
        Dim control_string As String = ""
        time = Format(Now(), "yyyy-MM-dd HH:mm:ss")
        '查询灯的状态信息
        sql = "select * from lamp_street where  pos_x<= " & (m_rightbuttonpos.X - 1) & " and pos_x >= " & (m_rightbuttonpos.X - m_controllampobj.m_lampwide - 3) & " and pos_y <= " & (m_rightbuttonpos.Y + 1) & " and pos_y >= " & (m_rightbuttonpos.Y - m_controllampobj.m_lampheight - 1)
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            SetTextDelegate(MSG_ERROR_STRING & "打开ToolStripMenuItem2_Click" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)
            conn.Close()
            conn = Nothing
            Exit Sub
        Else
            If rs.RecordCount > 0 Then
                '找到某一盏灯的信息后发送单灯开命令
                lamp_id_control.Checked = True
                all_open.Checked = True
                box_all.Text = Trim(rs.Fields("control_box_name").Value)
                lamp_id_all.Text = Mid(Trim(rs.Fields("lamp_id").Value), 7, LAMP_ID_LEN)
                lamp_id_all_start.Text = Mid(Trim(rs.Fields("lamp_id").Value), 1, 6)
                m_controlboxnamestring = Trim(rs.Fields("control_box_name").Value)
                lamp_type_all.Text = Trim(rs.Fields("type_string").Value)
                m_lampidstring = Trim(rs.Fields("lamp_id").Value)
                'sql = "SELECT B.control_box_id AS control_box_id,A.IMEI AS IMEI,A.control_box_name AS control_box_name FROM control_box AS B LEFT JOIN Box_IMEI AS A ON B.control_box_id=A.control_box_id where A.control_box_name='" & box_all.Text & "'"
                'rs = DBOperation.SelectSQL(conn, sql, msg)
                'If rs Is Nothing Then
                '    MsgBox(MSG_ERROR_STRING, , PROJECT_TITLE_STRING)
                '    conn.Close()
                '    conn = Nothing
                '    Exit Sub
                'Else
                open_close_single_lamp(m_lampidstring, 1, 1, 1)

                MsgBox("操作执行完毕", , PROJECT_TITLE_STRING)
                'End If
            Else
                '没有选择终端进行操作
                MsgBox("请选择终端后，点击右键", , PROJECT_TITLE_STRING)
            End If
        End If
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    ''' 对单灯的开关操作
    ''' </summary>
    ''' <param name="lamp_id">灯的编号</param>
    ''' <param name="open_close">0表示关，1表示开</param>
    ''' <param name="control_time">发送控制命令的次数</param>
    ''' <remarks></remarks>
    Private Function open_close_single_lamp(ByVal lamp_id As String, ByVal open_close As Integer, ByVal control_time As Integer, ByVal row_id As Integer) As Boolean
        ' Dim power_string As String
        ' Dim control_lamp_obj As New control_lamp
        '将景观灯的类型和编号组合成四位的十六进制
        Dim type_id As Integer   '灯类型的编号
        Dim lamp_id_bin As String  '灯的编号的十六位二进制
        Dim control_box_id As String  '电控箱编号
        Dim conn As New ADODB.Connection
        Dim controlcontent As String
        Dim controlname As String
        Dim controlmethod As String
        Dim sql, msg As String
        msg = ""
        DBOperation.OpenConn(conn)
        ' power_string = "100"
        type_id = Val(Mid(lamp_id, 5, 2))
        If type_id = 31 Then
            controlcontent = "主控节点"
            If open_close = 1 Then
                controlmethod = "吸合"
            Else
                controlmethod = "断开"
            End If
        Else
            controlcontent = "单灯"
            If open_close = 1 Then
                controlmethod = "单灯开"
            Else
                controlmethod = "单灯闭"
            End If
        End If
        control_box_id = Mid(lamp_id, 1, 4)
        lamp_id_bin = Com_inf.Get_lampid_bin(type_id, Val(Mid(lamp_id, 7, LAMP_ID_LEN))) '十六位长度的终端编号二进制
        If lamp_id_bin = "" Then
            GoTo finish
        End If
        controlname = Val(control_box_id).ToString & "-" & type_id.ToString & "-" & Val(Mid(lamp_id, 7, LAMP_ID_LEN))
        'lamp_id_bin = Com_inf.Dec_to_Bin(type_id, 5) & Com_inf.Dec_to_Bin(Mid(lamp_id, 7, LAMP_ID_LEN), 11) '十六位长度的终端编号二进制
        If open_close = 1 Then  '单灯开
            '打开终端操作
            open_close_single_lamp = m_controllampobj.open_light_single(control_box_id, lamp_id_bin, lamp_id, "全功率", "100", control_time, row_id)
            sql = "insert into hand_control_record(control_content,content_name,control_method,diangan,power,control_time,user_name) values('" & controlcontent & "','" & controlname & "','" & controlmethod & "','全功率','100','" & Now() & "','" & g_username & "')"
            DBOperation.ExecuteSQL(conn, sql, msg)
        End If
        If open_close = 0 Then
            '关闭终端
            open_close_single_lamp = m_controllampobj.close_light_single(control_box_id, lamp_id_bin, lamp_id, "关闭灯", "0", control_time, row_id)
            sql = "insert into hand_control_record(control_content,content_name,control_method,diangan,power,control_time,user_name) values('" & controlcontent & "','" & controlname & "','" & controlmethod & "','关闭灯','0','" & Now() & "','" & g_username & "')"
            DBOperation.ExecuteSQL(conn, sql, msg)
        End If

        If type_id = 31 Then
            '将该回路下所有的终端全部打开

            m_controllampobj.open_close_huilulamp(open_close, Val(Mid(lamp_id, 7, LAMP_ID_LEN)), control_box_id)

        End If

finish:
        conn.Close()
        conn = Nothing
    End Function

    ''' <summary>
    '''   点击右键查询灯的状态(功能隐藏，有待测试)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 查询状态ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 查询状态ToolStripMenuItem.Click
        Dim rs As ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim conn As New ADODB.Connection
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        msg = ""
        '查询灯的状态信息
        sql = "select * from lamp_inf where  pos_x<= " & (m_rightbuttonpos.X) & " and pos_x >= " & (m_rightbuttonpos.X - m_controllampobj.m_lampwide) & " and pos_y <= " & (m_rightbuttonpos.Y) & " and pos_y >= " & (m_rightbuttonpos.Y - m_controllampobj.m_lampheight)
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            SetTextDelegate(MSG_ERROR_STRING & "查询状态ToolStripMenuItem_Click" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)
            conn.Close()
            conn = Nothing
            Exit Sub
        Else
            If rs.RecordCount > 0 Then
                m_lamptip = m_controllampobj.get_lampinf_tip(Trim(rs.Fields("lamp_id").Value))   '标签显示终端的信息
            Else
                ToolTip_lamp.RemoveAll()
                MsgBox("请选择终端后，点击右键", , PROJECT_TITLE_STRING)
            End If
        End If
        rs.Close()
        rs = Nothing
        conn.Close()
        conn = Nothing

    End Sub

    ''' <summary>
    ''' 点击右键显示菜单操作
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub map_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        If e.Button = Windows.Forms.MouseButtons.Right Then
            '点击右键
            m_rightbuttonpos.X = (Me.pb_map.Location.X - Me.DesktopLocation.X - (Me.Width - Me.ClientSize.Width) - SplitContainer3.Panel1.Width) - Me.GroupBox1.Location.X   '(Me.Width - Me.ClientSize.Width) 为左边框的宽度
            m_rightbuttonpos.Y = (Me.pb_map.Location.Y - Me.DesktopLocation.Y - (Me.Height - Me.ClientSize.Height)) - Me.GroupBox1.Location.Y '(Me.Height - Me.ClientSize.Height)为上标题栏的高度

            '经过缩放后的点的坐标
            m_rightbuttonpos.X = (m_rightbuttonpos.X) / (0.5 + 0.05 * tb_map_size.Value)
            m_rightbuttonpos.Y = (m_rightbuttonpos.Y) / (0.5 + 0.05 * tb_map_size.Value)
            Me.SetTextLabelDelegate("X:" & m_rightbuttonpos.X & " ,Y:" & m_rightbuttonpos.Y, Tool, "location_string")  '状态栏显示当前坐标

        End If

    End Sub

    ''' <summary>
    ''' 单击右键后，可关闭所点击的终端
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 关闭ToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 关闭ToolStripMenuItem2.Click
        Dim rs As ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim conn As New ADODB.Connection
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        msg = ""
        '查询灯的状态信息
        sql = "select * from lamp_street where  pos_x<= " & (m_rightbuttonpos.X - 1) & " and pos_x >= " & (m_rightbuttonpos.X - m_controllampobj.m_lampwide - 3) & " and pos_y <= " & (m_rightbuttonpos.Y + 1) & " and pos_y >= " & (m_rightbuttonpos.Y - m_controllampobj.m_lampheight - 1)
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            SetTextDelegate(MSG_ERROR_STRING & "ToolStripMenuItem2_Click" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)
            conn.Close()
            conn = Nothing
            Exit Sub
        Else
            If rs.RecordCount > 0 Then '找到某一盏灯的信息后发送单灯开命令
                lamp_id_control.Checked = True
                all_close.Checked = True
                '将手工控制面板中的信息赋值，利用手工面板中的功能进行开关操作
                box_all.Text = Trim(rs.Fields("control_box_name").Value)
                lamp_id_all.Text = Mid(Trim(rs.Fields("lamp_id").Value), 7, LAMP_ID_LEN)
                lamp_id_all_start.Text = Mid(Trim(rs.Fields("lamp_id").Value), 1, 6)
                lamp_type_all.Text = Trim(rs.Fields("type_string").Value)
                m_controlboxnamestring = Trim(rs.Fields("control_box_name").Value)
                m_lampidstring = Trim(rs.Fields("lamp_id").Value)

                open_close_single_lamp(m_lampidstring, 0, 1, 1)
                MsgBox("操作执行完毕", , PROJECT_TITLE_STRING)
                'Com_inf.Turn_off_lamp() '关闭所有必须为暗的灯
                'If Me.BackgroundWorker_meterdata.IsBusy = False Then
                '    Me.BackgroundWorker_meterdata.RunWorkerAsync()

                'Else
                '    MsgBox("正在检测通信，请稍候重试", , PROJECT_TITLE_STRING)
                'End If

            Else
                '没有选择终端进行操作
                MsgBox("请选择终端后，点击右键", , PROJECT_TITLE_STRING)
            End If
        End If
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    ''' 用户自定义模式
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BackgroundWorker_holiday_mod_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker_holiday_mod.DoWork
        holiday_work()  '用户自定义模式
    End Sub

    ''' <summary>
    ''' 分析用户自定义控制字符串，按区域，类型，单灯分别进行节日模式操作
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub holiday_work()
        Dim rs, rs_box As New ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim control_method As String '选择的节日模式的控制信息
        Dim control_method_single() As String '单个控制信息数组
        Dim control_obj As String '选择的节日模式的控制对象
        Dim control_obj_single() As String  '单个控制对象
        Dim control_type As String  '控制对象的类别
        Dim i As Integer = 0
        Dim time As Integer = 1  '间隔时间
        Dim lamp_id_string As String  '景观灯编号
        Dim conn As New ADODB.Connection
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        msg = ""
        sql = "select * from holiday_mod where mod_title='" & Trim(m_holidaytitle) & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            SetTextDelegate(MSG_ERROR_STRING & "holiday_work" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs.RecordCount <= 0 Then
            MsgBox("该模式不存在", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        While rs.EOF = False
            '查询到该节日模式的控制信息
            control_method = Trim(rs.Fields("control_method").Value)  '方法
            control_obj = Trim(rs.Fields("control_obj").Value)  '对象
            control_type = Trim(rs.Fields("control_type").Value)  '类别
            control_method_single = control_method.Split(" ")
            control_obj_single = control_obj.Split(" ")
            If control_obj_single.Length = 1 Then
                If control_type = "城市名称" Then  '城市名称
                    sql = "select distinct(control_box_name) from lamp_street where city='" & control_obj_single(0) & "'"
                Else
                    If control_type = "区域名称" Then '区域名称
                        sql = "select distinct(control_box_name) from lamp_street where area='" & control_obj_single(0) & "'"
                    Else '街道名称
                        If control_type = "街道名称" Then
                            sql = "select distinct(control_box_name) from lamp_street where street='" & control_obj_single(0) & "'"
                        Else  '主控箱
                            m_controllampobj.Input_control_inf("", "主控箱", control_obj_single(0), control_method_single(0), 1, "全功率", "100", "主控箱", -1)
                            GoTo finish
                        End If
                    End If
                End If
                rs_box = DBOperation.SelectSQL(conn, sql, msg)
                If rs_box Is Nothing Then
                    If rs.State = 1 Then
                        rs.Close()
                        rs = Nothing
                    End If
                    conn.Close()
                    conn = Nothing
                    Exit Sub
                End If
                While rs_box.EOF = False
                    m_controllampobj.Input_control_inf("", "主控箱", Trim(rs_box.Fields("control_box_name").Value), control_method_single(0), 1, "全功率", "100", "主控箱", -1)
                    rs_box.MoveNext()
                End While
                GoTo finish
            End If
            If control_obj_single.Length = 2 Then  '按类型控制
                m_controllampobj.Input_control_inf(control_obj_single(1), "类型", control_obj_single(0), control_method_single(0), 1, "全功率", "100", "类型", -1)
                GoTo finish
            End If
            If control_obj_single.Length = 3 Then  '按灯的编号进行控制
                While i < control_method_single.Length '输入按电控箱下某一类型控制的命令
                    lamp_id_string = Com_inf.Com_lamp_id_all(control_obj_single(0), control_obj_single(1), control_obj_single(2))
                    If control_method_single(i) = "打开" Then
                        m_controllampobj.open_close_single_lamp(control_obj_single(0), control_obj_single(1), lamp_id_string, 1, "全功率", 0, -1)
                    End If
                    If control_method_single(i) = "关闭" Then
                        m_controllampobj.open_close_single_lamp(control_obj_single(0), control_obj_single(1), lamp_id_string, 0, "关闭灯", 0, -1)
                    End If
                    i += 1
                End While
                GoTo finish
            End If
finish:
            i = 0
            time = 1000 * rs.Fields("time").Value  '停顿多少秒
            System.Threading.Thread.Sleep(time)
            rs.MoveNext()
        End While
        conn.Close()
        conn = Nothing
        MsgBox("用户自定义模式：" & m_holidaytitle & "开启", , PROJECT_TITLE_STRING)
    End Sub

    Private Sub alarm_string_show_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs)
        '鼠标移动到报警的字幕上是，光标变成手指的摸样
        Me.Cursor = Cursors.Hand
    End Sub

    Private Sub alarm_string_show_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        '鼠标移出字幕，光标恢复默认
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub alarm_string_show_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        '点击产生故障报警列表
        If GetInstanceState("故障列表") Then
            Exit Sub
        End If
        故障列表.MdiParent = Me
        Dim alarm_list_obj As New 故障列表
        alarm_list_obj.ShowDialog()

    End Sub

    Private Sub lamp_time_set_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lamp_time_set.Click
        If GetInstanceState("单灯时段设定") Then
            Exit Sub
        End If
        单灯时段设定.MdiParent = Me
        Dim lamp_set_obj As New 单灯时段设定
        lamp_set_obj.ShowDialog()
    End Sub

    ''' <summary>
    ''' 静音或报警
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Alarm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_alarm.Click
        If m_alarmopenorclose = True Then
            PlaySound(vbNullString, 0, 0)
            m_alarmopenorclose = False
            If Me.BackgroundWorkerAlarm.IsBusy = True Then
                Me.BackgroundWorkerAlarm.CancelAsync()
            End If
            ToolStripButton_alarm.Image = System.Drawing.Image.FromFile("图片\91.png")  '载入地图
            ToolStripButton_alarm.Text = "报警静音"
        Else
            m_alarmopenorclose = True
            ToolStripButton_alarm.Image = System.Drawing.Image.FromFile("图片\9.png")  '载入地图
            ToolStripButton_alarm.Text = "报警开启"
        End If

    End Sub

    ''' <summary>
    '''  声音报警和图标定位显示
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BackgroundWorkerAlarm_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorkerAlarm.DoWork
        If m_alarmopenorclose = True Then
            Dim i As Integer = 0
            While i < 1
                PlaySound(mstrfileName, 0, SND_FILENAME)
                i += 1
                System.Threading.Thread.Sleep(200)
            End While
        End If
    End Sub

    '''' <summary>
    '''' 定位地图,区域定位的下拉框，下拉时添加区域名称
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    '''' <remarks></remarks>
    'Private Sub cb_goto_area_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Dim rs As New ADODB.Recordset
    '    Dim conn As New ADODB.Connection
    '    Dim sql As String
    '    Dim msg As String
    '    If DBOperation.OpenConn(conn) = False Then
    '        Exit Sub
    '    End If
    '    msg = ""

    '    sql = "select * from street_position where map_id='" & g_choosemapid & "'"
    '    cb_goto_area.Items.Clear()
    '    rs = DBOperation.SelectSQL(conn, sql, msg)

    '    While rs.EOF = False
    '        cb_goto_area.Items.Add(Trim(rs.Fields("control_box_name").Value))  '添加街道名称
    '        rs.MoveNext()
    '    End While
    '    If cb_goto_area.Items.Count > 0 Then
    '        cb_goto_area.SelectedIndex = 0    '选取第一个定位电控箱值

    '    End If
    '    If rs.State = 1 Then
    '        rs.Close()
    '        rs = Nothing
    '    End If
    '    conn.Close()
    '    conn = Nothing
    'End Sub

    '''' <summary>
    '''' 选择定位的区域后，按回车表示开始定位
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    '''' <remarks></remarks>
    'Private Sub cb_goto_area_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
    '    If e.KeyCode = Keys.Enter Then
    '        '景观灯中将街道定位改为电控箱定位
    '        Dim left_ori As Integer
    '        Dim top_ori As Integer
    '        Dim left_moveto As Integer
    '        Dim top_moveto As Integer

    '        Dim rs As ADODB.Recordset
    '        Dim msg As String
    '        Dim sql As String
    '        Dim map_path, smallmap_path As String  '大图和小图的路径

    '        left_moveto = 0
    '        top_moveto = 0
    '        If tb_map_size.Value <> MAP_MID_SIZE Then  '定位时将地图尺寸恢复到正常尺寸
    '            g_changemapvalue = MAP_MID_SIZE
    '            tb_map_size.Value = g_changemapvalue
    '            map_size_id.Text = "地图尺寸：100 %"
    '        End If

    '        If cb_goto_area.Text = "" Then '定位的区域名称
    '            MsgBox("请选择区域", , PROJECT_TITLE_STRING)
    '            cb_goto_area.Focus()
    '            Exit Sub
    '        End If

    '        Dim conn As New ADODB.Connection
    '        If DBOperation.OpenConn(conn) = False Then
    '            Exit Sub
    '        End If
    '        'sql = "select map_list.id, map_list.map_name, control_box.control_box_id, control_box_name, " _
    '        '                         & "control_box.pos_y, control_box.pos_x from control_box INNER JOIN street ON control_box.street_id =" _
    '        '                         & " street.street_id INNER JOIN area ON street.area_id = area.id INNER JOIN map_list ON area.area = map_list.area " _
    '        '                         & "where control_box_name='" & Trim(goto_area.Text) & "'"
    '        msg = ""
    '        sql = "SELECT map_list.map_name, map_list.id, street_position.control_box_name, street_position.pos_y, street_position.pos_x FROM map_list INNER JOIN area ON map_list.area = area.area INNER JOIN street_position ON map_list.id = street_position.map_id where control_box_name='" & Trim(cb_goto_area.Text) & "'"
    '        rs = DBOperation.SelectSQL(conn, sql, msg)
    '        If rs Is Nothing Then
    '            SetTextDelegate(MSG_ERROR_STRING & "goto_area_KeyDown" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)

    '            conn.Close()
    '            conn = Nothing
    '            Exit Sub
    '        End If
    '        If rs.RecordCount > 0 Then
    '            If rs.Fields("id").Value <> g_choosemapid Then  '如果所选择的地图编号不是当前的编号，跳转地图
    '                g_choosemapid = rs.Fields("id").Value
    '                g_mapname = Trim(rs.Fields("map_name").Value)  '地图的名称
    '                map_path = "map\" & g_mapname & ".jpg"  '地图路径
    '                smallmap_path = "map\s" & g_mapname & ".jpg"  '地图路径
    '                pb_map.BackgroundImage.Dispose()
    '                pb_small_map.BackgroundImage.Dispose()
    '                pb_map.BackgroundImage = System.Drawing.Image.FromFile(map_path) '载入地图
    '                pb_small_map.BackgroundImage = System.Drawing.Image.FromFile(smallmap_path) '载入鹰眼地图
    '                pb_map.Width = System.Drawing.Image.FromFile(map_path).Width  '设置map的宽
    '                pb_map.Height = System.Drawing.Image.FromFile(map_path).Height '设置map的高

    '                m_lamp = New Bitmap(pb_map.Width, pb_map.Height)   '绘制终端的bmp图

    '                g_lampmap.Clear(Color.Empty)
    '                g_lampmap = Graphics.FromImage(m_lamp)  '载入灯的图片
    '                g_mapsizevalue = pb_map.Size '地图的尺寸
    '                Me.map_area.Text = "地图区域：" & g_mapname

    '            End If
    '            sql = "select * from street_position where control_box_name='" & Trim(cb_goto_area.Text) & "'"
    '            rs = DBOperation.SelectSQL(conn, sql, msg)
    '            If rs.RecordCount > 0 Then
    '                left_moveto = rs.Fields("pos_x").Value  '定位的坐标X信息
    '                top_moveto = rs.Fields("pos_y").Value  '定位的坐标Y信息
    '            Else
    '                MsgBox("没有该区域的信息，请选择正确的区域！", , PROJECT_TITLE_STRING)
    '                conn.Close()
    '                conn = Nothing
    '                Exit Sub
    '            End If
    '            left_ori = pb_map.Left  '地图的原始X坐标
    '            top_ori = pb_map.Top   '地图的原始Y坐标

    '            pb_map.Left = left_moveto   '地图的新X坐标
    '            pb_map.Top = top_moveto   '地图的新Y坐标

    '        End If

    '        rs.Close()
    '        rs = Nothing
    '        conn.Close()
    '        conn = Nothing
    '    End If
    'End Sub

    ''' <summary>
    ''' 鼠标移动显示终端状态标签信息
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub show_lamp_state()
        Dim rs As ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim pos As System.Drawing.Point
        Dim conn As New ADODB.Connection
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        msg = ""
        '点在地图上的位置
        pos.X = (Control.MousePosition.X - Me.pb_map.Location.X - Me.DesktopLocation.X - (Me.Width - Me.ClientSize.Width) - SplitContainer3.Panel1.Width) - Me.GroupBox1.Location.X   '(Me.Width - Me.ClientSize.Width) 为左边框的宽度
        pos.Y = (Control.MousePosition.Y - Me.pb_map.Location.Y - Me.DesktopLocation.Y - (Me.Height - Me.ClientSize.Height)) - Me.GroupBox1.Location.Y '(Me.Height - Me.ClientSize.Height)为上标题栏的高度
        '经过缩放后的点的坐标
        pos.X = (pos.X) / (MAP_SIZE_BASE + MAP_SIZE_CHANGE * tb_map_size.Value)
        pos.Y = (pos.Y) / (MAP_SIZE_BASE + MAP_SIZE_CHANGE * tb_map_size.Value)
        '查询灯的状态信息
        sql = "select lamp_id from lamp_street where map_id='" & g_choosemapid & "' and pos_x< " & (pos.X - 1) & " and pos_x >= " & (pos.X - m_controllampobj.m_lampwide - 3) & " and pos_y < " & (pos.Y + 1) & " and pos_y >= " & (pos.Y - m_controllampobj.m_lampheight - 1)
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            SetTextDelegate(MSG_ERROR_STRING & "show_box_state" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)
            conn.Close()
            conn = Nothing
            Exit Sub
        Else
            If rs.RecordCount > 0 Then
                m_lamptip = m_controllampobj.get_lampinf_tip(Trim(rs.Fields("lamp_id").Value)) '标签显示终端的信息
            Else
                ToolTip_lamp.RemoveAll()
            End If
        End If
        Me.SetTextLabelDelegate("X:" & pos.X & " ,Y:" & pos.Y, Tool, "location_string")  '状态栏显示当前坐标
        rs.Close()
        rs = Nothing
        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    ''' 鼠标移动显示电控箱状态信息
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub show_box_state()
        Dim rs_box As New ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim pos As System.Drawing.Point
        Dim conn As New ADODB.Connection
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        Dim controlboxtype As Integer
        msg = ""
        '点在地图上的位置
        pos.X = (Control.MousePosition.X - Me.pb_map.Location.X - Me.DesktopLocation.X - (Me.Width - Me.ClientSize.Width) - SplitContainer3.Panel1.Width) - Me.GroupBox1.Location.X   '(Me.Width - Me.ClientSize.Width) 为左边框的宽度
        pos.Y = (Control.MousePosition.Y - Me.pb_map.Location.Y - Me.DesktopLocation.Y - (Me.Height - Me.ClientSize.Height)) - Me.GroupBox1.Location.Y '(Me.Height - Me.ClientSize.Height)为上标题栏的高度
        '经过缩放后的点的坐标
        pos.X = (pos.X) / (MAP_SIZE_BASE + MAP_SIZE_CHANGE * tb_map_size.Value)
        pos.Y = (pos.Y) / (MAP_SIZE_BASE + MAP_SIZE_CHANGE * tb_map_size.Value)
        '查询灯的状态信息
        sql = "SELECT control_box.control_box_type,control_box.board_num, control_box.control_box_id, control_box_name , control_box.state, " _
             & "control_box.pos_y, control_box.pos_x FROM control_box INNER JOIN street ON control_box.street_id =" _
             & " street.street_id INNER JOIN area ON street.area_id = area.id INNER JOIN map_list ON area.area = map_list.area where map_list.id='" & g_choosemapid & "'" _
             & "and  pos_x<= " & (pos.X) & " and pos_x >= " & (pos.X - 20) & " and pos_y <= " & (pos.Y) & " and pos_y >= " & (pos.Y - 20)
        rs_box = DBOperation.SelectSQL(conn, sql, msg)
        If rs_box Is Nothing Then
            SetTextDelegate(MSG_ERROR_STRING & "show_box_state" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs_box.RecordCount > 0 Then
            controlboxtype = rs_box.Fields("control_box_type").Value
            If controlboxtype < 4 Then
                m_lamptip = m_controlboxobj.Get_controlbox_tip(Trim(rs_box.Fields("control_box_id").Value), rs_box.Fields("board_num").Value)   '标签显示终端的信息
            Else
                m_lamptip = m_controlboxobj.Get_controlbox_tipABC(Trim(rs_box.Fields("control_box_id").Value), rs_box.Fields("board_num").Value)   '标签显示终端的信息
            End If
            ' m_lamptip = m_controlboxobj.Get_controlbox_tip(Trim(rs_box.Fields("control_box_id").Value), rs_box.Fields("board_num").Value)   '标签显示终端的信息
        Else
            ToolTip_box.RemoveAll()
        End If
        Me.SetTextLabelDelegate("X:" & pos.X & " ,Y:" & pos.Y, Tool, "location_string")  '状态栏显示当前坐标
        If rs_box.State = 1 Then
            rs_box.Close()
            rs_box = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    ''' 鼠标移到终端位置，工具条显示终端实时信息
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub pb_map_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pb_map.MouseMove
        m_lamptip = ""
        show_box_state()
        show_lamp_state()
        ToolTip_lamp.SetToolTip(Me.pb_map, m_lamptip)
        ToolTip_box.SetToolTip(Me.pb_map, m_lamptip)
    End Sub

    ''' <summary>
    ''' 鼠标双击地图，取得地图上的坐标，为添加终端和定位区域取坐标
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub pb_map_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pb_map.MouseDoubleClick
        g_movepictag = 0

        If m_addlampobj IsNot Nothing Then  '增加终端坐标
            If tb_map_size.Value <> MAP_MID_SIZE Then
                g_changemapvalue = MAP_MID_SIZE
                tb_map_size.Value = g_changemapvalue
                MsgBox("地图恢复正常尺寸，请重新选择坐标", , PROJECT_TITLE_STRING)
                map_size_id.Text = "地图尺寸：100 %"
                Exit Sub
            End If

            If m_addlampobj.m_addtag = 2 Then  '第一次双击地图后增加新增终端的起始终端坐标
                m_addlampobj.tb_start_pos_x.Text = Control.MousePosition.X
                m_addlampobj.tb_start_pos_y.Text = Control.MousePosition.Y
                m_addlampobj.m_addtag = 3  '标志为取新增终端的终点终端坐标
            Else
                If m_addlampobj.m_addtag = 3 Then  '第二此双击地图后增加新增终端的终点终端坐标
                    m_addlampobj.tb_end_pos_x.Text = Control.MousePosition.X
                    m_addlampobj.tb_end_pos_y.Text = Control.MousePosition.Y
                    m_addlampobj.m_addtag = 2  '标志为取新增终端的起点坐标
                End If
            End If
        End If
        If g_addstreettag = 1 Then '双击地图记录定位一条街道的坐标，取该街道位于视窗中心时的坐标
            If tb_map_size.Value <> MAP_MID_SIZE Then
                g_changemapvalue = MAP_MID_SIZE
                tb_map_size.Value = g_changemapvalue
                MsgBox("地图恢复正常尺寸，请重新选择坐标")
                map_size_id.Text = "地图尺寸：100 %"
                Exit Sub
            End If

            m_addstreetobj.lb_pos_x.Text = pb_map.Left
            m_addstreetobj.lb_pos_y.Text = pb_map.Top
            'Me.add_street_find_tag = 0
            Exit Sub
        End If

        If g_addboxtag = 1 Then
            If m_addboxobj.tc_boxinf.SelectedTab Is m_addboxobj.tp_addbox Then
                If tb_map_size.Value <> MAP_MID_SIZE Then
                    g_changemapvalue = MAP_MID_SIZE
                    tb_map_size.Value = g_changemapvalue
                    MsgBox("地图恢复正常尺寸，请重新选择坐标")
                    map_size_id.Text = "地图尺寸：100 %"
                    Exit Sub
                End If

                m_addboxobj.tb_start_pos_x.Text = Control.MousePosition.X
                m_addboxobj.tb_start_pos_y.Text = Control.MousePosition.Y
                Exit Sub
            End If
            If m_addboxobj.tc_boxinf.SelectedTab Is m_addboxobj.tp_editbox Then
                If tb_map_size.Value <> MAP_MID_SIZE Then
                    g_changemapvalue = MAP_MID_SIZE
                    tb_map_size.Value = g_changemapvalue
                    MsgBox("地图恢复正常尺寸，请重新选择坐标")
                    map_size_id.Text = "地图尺寸：100 %"
                    Exit Sub
                End If
                m_addboxobj.tb_start_x.Text = Control.MousePosition.X
                m_addboxobj.tb_start_y.Text = Control.MousePosition.Y
                'm_editboxobj.lb_start_pos_x.Text = Control.MousePosition.X
                'm_editboxobj.lb_start_pos_y.Text = Control.MousePosition.Y
                g_addboxtag = 2 '表示更新了坐标
                Exit Sub
            End If
        End If

        'If g_addboxtag = 3 Then

        'End If

    End Sub



    Private Sub dgv_lamp_state_list_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv_lamp_state_list.CellClick
        If dgv_lamp_state_list.RowCount > 0 Then
            m_index = dgv_lamp_state_list.CurrentRow.Index
        End If

    End Sub

    ''' <summary>
    ''' 鹰眼效果，单击小图，大图跳转
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub pb_small_map_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pb_small_map.Click
        Dim wide_percent As Double
        Dim height_percent As Double
        Dim pos As System.Drawing.Point   '点击的在small_map中的坐标
        Me.tb_map_size.Value = MAP_MID_SIZE '定位的时候地图跳转到100%的比例
        map_size_id.Text = "地图尺寸：100%"

        pb_map.Width = g_mapsizevalue.Width * (MAP_SIZE_BASE + MAP_SIZE_CHANGE * tb_map_size.Value)   '设置图片框的宽度
        pb_map.Height = g_mapsizevalue.Height * (MAP_SIZE_BASE + MAP_SIZE_CHANGE * tb_map_size.Value)  '设置图片框的高度


        pos.X = (Control.MousePosition.X - Me.pb_small_map.Location.X - Me.DesktopLocation.X - (Me.Width - Me.ClientSize.Width) - SplitContainer3.Panel1.Width) - Me.GroupBox1.Location.X   '(Me.Width - Me.ClientSize.Width) 为左边框的宽度
        pos.Y = (Control.MousePosition.Y - Me.pb_small_map.Location.Y - Me.DesktopLocation.Y - (Me.Height - Me.ClientSize.Height)) - Me.GroupBox1.Location.Y '(Me.Height - Me.ClientSize.Height)为上标题栏的高度


        wide_percent = pos.X / Me.pb_small_map.Width
        height_percent = pos.Y / Me.pb_small_map.Height

        '将大地图移动到相对的比例处
        pb_map.Left = -(pb_map.Width * wide_percent - Me.GroupBox1.Width / 2)
        pb_map.Top = -(pb_map.Height * height_percent - Me.GroupBox1.Height / 2)

        g_midpoint.X = pb_map.Left + pb_map.Width / 2  '中点坐标X轴
        g_midpoint.Y = pb_map.Top + pb_map.Height / 2  '中点坐标Y轴

    End Sub

    Private Sub Lamp_State_list_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        m_index = dgv_lamp_state_list.CurrentRow.Index
    End Sub

    ''' <summary>
    ''' 电控箱的通信状态
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BackgroundWorkerHeart_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorkerHeart.DoWork
        While Me.BackgroundWorkerHeart.CancellationPending = False
            Try
                find_box_state() '电控箱列表
                System.Threading.Thread.Sleep(60000) '休眠1分钟
            Catch ex As Exception
                SetTextDelegate(MSG_ERROR_STRING & "BackgroundWorkerHeart_DoWork" & e.ToString & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)
            End Try
        End While
    End Sub

    ''' <summary>
    ''' 刷新故障列表（单灯和电控箱）
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BackgroundWorker_find_state_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker_find_state.ProgressChanged
        If e.ProgressPercentage = 1 Then
            get_boxprobleminf() '获取故障信息
        End If
        If e.ProgressPercentage = 2 Then
            Dim i As Integer = 0
            Dim inf As New m_alarminf
            If dgv_alarmlist.Rows.Count > 0 Then
                Dim row As Integer = dgv_alarmlist.CurrentCell.RowIndex
                Dim colume As Integer = dgv_alarmlist.CurrentCell.ColumnIndex
                dgv_alarmlist.Rows.Clear()
                While i < g_alarminf.Count
                    inf = g_alarminf(i)
                    dgv_alarmlist.Rows.Add()
                    dgv_alarmlist.Rows(i).Cells("alarm_control_box_name").Value = inf.control_box_name
                    dgv_alarmlist.Rows(i).Cells("alarm_inf").Value = inf.alarm_msg
                    dgv_alarmlist.Rows(i).Cells("alarm_time").Value = inf.alarm_time
                    i += 1
                End While
                ' Me.Control_boxTableAdapter.Fill(Me.Control_box_state.control_box) '刷新电控箱故障列表
                If row > dgv_alarmlist.Rows.Count - 1 Then
                    row = dgv_alarmlist.Rows.Count - 1
                End If
                'dgv_alarmlist.CurrentCell = dgv_alarmlist(colume, row)
                If dgv_alarmlist.Rows.Count > 0 Then
                    dgv_alarmlist.CurrentCell = dgv_alarmlist(colume, row)
                End If
            Else
                While i < g_alarminf.Count
                    inf = g_alarminf(i)
                    dgv_alarmlist.Rows.Add()
                    dgv_alarmlist.Rows(i).Cells("alarm_control_box_name").Value = inf.control_box_name
                    dgv_alarmlist.Rows(i).Cells("alarm_inf").Value = inf.alarm_msg
                    dgv_alarmlist.Rows(i).Cells("alarm_time").Value = inf.alarm_time
                    i += 1
                End While
            End If
            sanyao_alarm_list()
        End If
        If e.ProgressPercentage = 3 Then
            '刷新开关量故障报警
            Me.Kaiguan_alarm_listTableAdapter.Fill(Me.KaiguanalarmDataSet.kaiguan_alarm_list)
        End If
        If e.ProgressPercentage = 5 Then
            '统计共有几种报警信息，然后在报警的标题栏上显示
            '  报警信息ToolStripMenuItem.Text = "报警信息栏  (目前报警信息：" & Get_alarmtype() & ")"
        End If
        If e.ProgressPercentage = 6 Then
            '单灯故障显示
            '刷新报警信息
            lamp_alarm_listshow()
        End If
        If g_lampalarm_show = True Then
            故障报警窗口.Show()
        End If

    End Sub
    ''' <summary>
    ''' 检查是否有单灯故障，有就显示报警栏，没有就不显示
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub lamp_alarm_listshow()
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String = ""
        Dim sql As String = ""
        Dim row As Integer
        Dim lamp_id As String
        Dim probleminf As String = ""
        Dim lampkind As String = ""
        Dim lamp_pointinfor As String
        Dim lampstate As Integer
        Dim row_lamp As Integer
        Dim colume_lamp As Integer
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        sql = "select state,control_box_name,lamp_id,information,date,lamp_pointinfor,jiechuqi_id,result from lamp_street where type_id<>'31'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs.RecordCount > 0 Then
            If 故障报警窗口.dgv_lamp_alarm.CurrentCell Is Nothing Then
                row_lamp = 0
                colume_lamp = 0
            Else
                row_lamp = 故障报警窗口.dgv_lamp_alarm.CurrentCell.RowIndex
                colume_lamp = 故障报警窗口.dgv_lamp_alarm.CurrentCell.ColumnIndex
            End If
            '存在单灯故障
            故障报警窗口.dgv_lamp_alarm.Rows.Clear()
            While rs.EOF = False
                lampkind = Trim(rs.Fields("jiechuqi_id").Value)
                lampstate = Trim(rs.Fields("state").Value)
                If lampkind = 3 And lampstate <> 0 And lampstate <> 8 And lampstate <> 3 And lampstate <> 4 Then
                    row = 故障报警窗口.dgv_lamp_alarm.RowCount
                    故障报警窗口.dgv_lamp_alarm.Rows.Add()
                    故障报警窗口.dgv_lamp_alarm.Rows(row).Cells("alarm_controlboxname").Value = Trim(rs.Fields("control_box_name").Value)
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
                    lamp_id = Trim(rs.Fields("lamp_id").Value)
                    lamp_id = Val(Mid(lamp_id, 1, 4)).ToString & "-" & Val(Mid(lamp_id, 5, 2)).ToString & "-" & Val(Mid(lamp_id, 7, LAMP_ID_LEN)).ToString
                    故障报警窗口.dgv_lamp_alarm.Rows(row).Cells("alarm_lampid").Value = lamp_id & lamp_pointinfor
                    probleminf = m_controllampobj.get_probleminf(rs.Fields("state").Value)
                    故障报警窗口.dgv_lamp_alarm.Rows(row).Cells("alarm_string").Value = probleminf
                    故障报警窗口.dgv_lamp_alarm.Rows(row).Cells("time").Value = rs.Fields("date").Value
                Else
                    lampstate = rs.Fields("result").Value
                    If lampkind = 2 And (lampstate = 1 Or lampstate = 2 Or lampstate = 3) Then
                        row = 故障报警窗口.dgv_lamp_alarm.RowCount
                        故障报警窗口.dgv_lamp_alarm.Rows.Add()
                        故障报警窗口.dgv_lamp_alarm.Rows(row).Cells("alarm_controlboxname").Value = Trim(rs.Fields("control_box_name").Value)
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
                        lamp_id = Trim(rs.Fields("lamp_id").Value)
                        lamp_id = Val(Mid(lamp_id, 1, 4)).ToString & "-" & Val(Mid(lamp_id, 5, 2)).ToString & "-" & Val(Mid(lamp_id, 7, LAMP_ID_LEN)).ToString
                        故障报警窗口.dgv_lamp_alarm.Rows(row).Cells("alarm_lampid").Value = lamp_id & lamp_pointinfor
                        ' probleminf = m_controllampobj.get_probleminf(rs.Fields("state").Value)
                        If lampstate = 1 Then
                            probleminf = LAMP_STATE_PROBLEM_ON
                        Else
                            If lampstate = 2 Then
                                probleminf = LAMP_STATE_PROBLEM_OFF
                            Else
                                probleminf = LAMP_STATE_NORETURN
                            End If
                        End If
                        故障报警窗口.dgv_lamp_alarm.Rows(row).Cells("alarm_string").Value = probleminf
                        故障报警窗口.dgv_lamp_alarm.Rows(row).Cells("time").Value = rs.Fields("date").Value
                    End If
                End If
                rs.MoveNext()
            End While
            If row_lamp > 故障报警窗口.dgv_lamp_alarm.Rows.Count - 1 Then
                row_lamp = 故障报警窗口.dgv_lamp_alarm.Rows.Count - 1
            End If
            If 故障报警窗口.dgv_lamp_alarm.Rows.Count > 0 Then
                故障报警窗口.dgv_lamp_alarm.CurrentCell = 故障报警窗口.dgv_lamp_alarm(colume_lamp, row_lamp)
            End If
        Else
            '不存在单灯报警信息，则将单灯报警的标志置为false
            g_lampalarm_tag = False
            故障报警窗口.Close()
        End If
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub
    Public Sub sanyao_alarm_list()
        Dim i As Integer = 0
        Dim inf As New m_alarminf
        Dim row As Integer
        Dim colume As Integer
        If 故障报警窗口.dgv_alarmlist.CurrentCell Is Nothing Then
            row = 0
            colume = 0
        Else
            row = 故障报警窗口.dgv_alarmlist.CurrentCell.RowIndex
            colume = 故障报警窗口.dgv_alarmlist.CurrentCell.ColumnIndex
        End If
        If dgv_alarmlist.Rows.Count > 0 Then

            故障报警窗口.dgv_alarmlist.Rows.Clear()
            While i < g_alarminf.Count
                inf = g_alarminf(i)
                故障报警窗口.dgv_alarmlist.Rows.Add()
                故障报警窗口.dgv_alarmlist.Rows(i).Cells("alarm_control_box_name").Value = inf.control_box_name
                故障报警窗口.dgv_alarmlist.Rows(i).Cells("alarm_inf").Value = inf.alarm_msg
                故障报警窗口.dgv_alarmlist.Rows(i).Cells("alarm_time").Value = inf.alarm_time
                i += 1
            End While
            ' Me.Control_boxTableAdapter.Fill(Me.Control_box_state.control_box) '刷新电控箱故障列表

        Else
            While i < g_alarminf.Count
                inf = g_alarminf(i)
                故障报警窗口.dgv_alarmlist.Rows.Add()
                故障报警窗口.dgv_alarmlist.Rows(i).Cells("alarm_control_box_name").Value = inf.control_box_name
                故障报警窗口.dgv_alarmlist.Rows(i).Cells("alarm_inf").Value = inf.alarm_msg
                故障报警窗口.dgv_alarmlist.Rows(i).Cells("alarm_time").Value = inf.alarm_time
                i += 1
            End While
        End If
        If row > 故障报警窗口.dgv_alarmlist.Rows.Count - 1 Then
            row = 故障报警窗口.dgv_alarmlist.Rows.Count - 1
        End If
        'dgv_alarmlist.CurrentCell = dgv_alarmlist(colume, row)
        If 故障报警窗口.dgv_alarmlist.Rows.Count > 0 Then
            故障报警窗口.dgv_alarmlist.CurrentCell = 故障报警窗口.dgv_alarmlist(colume, row)
        End If
    End Sub

    '''' <summary>
    '''' 获取目前报警的种类
    '''' </summary>
    '''' <remarks></remarks>
    'Private Function Get_alarmtype() As String
    '    Dim alarm_string As String = ""
    '    If dgv_problem_list.RowCount > 0 Then
    '        alarm_string &= "单灯报警 "
    '    End If

    '    If dgv_box_problem_list.RowCount > 0 Then
    '        alarm_string &= "主控箱报警 "
    '    End If

    '    If dgv_configstate_list.RowCount > 0 Then
    '        alarm_string &= "配置信息报警 "
    '    End If

    '    If dgv_kaiguan_alarmlist.RowCount > 0 Then
    '        alarm_string &= "开关量报警 "
    '    End If

    '    If dgv_shiya_alarmlist.RowCount > 0 Then
    '        alarm_string &= "失压报警"
    '    End If

    '    If alarm_string <> "" Then
    '        Get_alarmtype = alarm_string
    '    Else
    '        Get_alarmtype = "无"
    '    End If
    'End Function

    ''' <summary>
    ''' 双击某一盏灯后定位
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgv_lamp_state_list_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv_lamp_state_list.CellDoubleClick, dgv_lamp_state_list.CellContentClick
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim posx As Integer '点的X坐标
        Dim posy As Integer '点的Y坐标
        m_zhishitime = 0  '将指示图标显示的时间清零
        msg = ""
        sql = "select pos_x, pos_y from lamp_inf where lamp_id='" & Trim(dgv_lamp_state_list.CurrentRow.Cells("lamp_id").Value) & "' and map_id='" & g_choosemapid & "'"
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            SetTextDelegate(MSG_ERROR_STRING & "Lamp_State_list_CellDoubleClick" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)
            conn.Close()
            conn = Nothing
            Me.Close()
        End If
        If rs.RecordCount <= 0 Then
            rs.Close()
            rs = Nothing
            conn.Close()
            conn = Nothing
            Exit Sub
        End If

        '找到该灯的信息
        posx = rs.Fields("pos_x").Value
        posy = rs.Fields("pos_y").Value
        If tb_map_size.Value <> MAP_MID_SIZE Then
            g_changemapvalue = MAP_MID_SIZE
            tb_map_size.Value = g_changemapvalue
            map_size_id.Text = "地图尺寸：100 %"
        End If
        '点在地图上的位置
        Me.pb_map.Left = pb_map.Location.X + (Me.GroupBox1.Width / 2 - (pb_map.Location.X + posx))
        Me.pb_map.Top = pb_map.Location.Y + (Me.GroupBox1.Height / 2 - (pb_map.Location.Y + posy))
        '显示指示箭头
        Me.zhishijiantou.Top = Me.GroupBox1.Height / 2 - 20  '指示箭头在所选择灯的上面位置
        Me.zhishijiantou.Left = Me.GroupBox1.Width / 2
        Me.zhishijiantou.Visible = True
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    ''' 将时段模式下行
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub SendDivControl_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SendDivControl.Click
        If MsgBox("是否将时段控制模式下行到主控器？", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.No Then
            Exit Sub
        End If
        Dim rs, rs_div, rs_box, rs_mod As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim control_box_id As String  '电控箱编号
        Dim div_level As String  '时段控制级别
        Dim IMEI As String  'GPRS编号
        Dim div_string As String   '时段控制的字符串
        Dim control_id As String '控制模式代号
        Dim cmdType As String
        Dim sendString As String  '发送的字符串
        Dim roadID As String '路段号
        Dim lamp_id_bin, lamp_id_hex As String '16位长度的二进制
        Dim str, str1, str2 As String
        Dim order_num As Integer
        Dim rs_boxorder As New ADODB.Recordset  '按电控箱写字符
        Dim control_method As String '控制方法
        cmdType = "1"  ' 1--表示时段设置下放 2--表示时间定时下放
        sendString = ""
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        msg = ""
        order_num = 0
        IMEI = ""
        sql = "select distinct(control_box_id) from road_level order by control_box_id"
        rs_boxorder = DBOperation.SelectSQL(conn, sql, msg)
        If rs_boxorder Is Nothing Then
            SetTextDelegate(MSG_ERROR_STRING & "SendDivControl_Click" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        While rs_boxorder.EOF = False
            order_num = 0
            sendString = ""
            sql = "select div_time_level, control_box_id, lamp_id , type_id from control_level_view where control_box_id='" & Trim(rs_boxorder.Fields("control_box_id").Value) & "'"
            rs = DBOperation.SelectSQL(conn, sql, msg)
            If rs Is Nothing Then
                SetTextDelegate(MSG_ERROR_STRING & "SendDivControl_Click" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)

                conn.Close()
                conn = Nothing
                Exit Sub
            End If

            While rs.EOF = False

                control_box_id = Trim(rs.Fields("control_box_id").Value)
                div_level = Trim(rs.Fields("div_time_level").Value)

                sql = "select * from Box_IMEI where control_box_id='" & control_box_id & "'"
                rs_box = DBOperation.SelectSQL(conn, sql, msg)
                If rs_box Is Nothing Then
                    SetTextDelegate(MSG_ERROR_STRING & "SendDivControl_Click" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)

                    conn.Close()
                    conn = Nothing
                    Exit Sub
                End If
                While rs_box.EOF = False
                    IMEI = Trim(rs_box.Fields("IMEI").Value)
                    roadID = Com_inf.Dec_to_Hex(rs_box.Fields("RoadID").Value, 4)

                    '查询时段控制的时间
                    sql = "select * from div_time where div_level='" & div_level & "'"
                    rs_div = DBOperation.SelectSQL(conn, sql, msg)
                    If rs_div Is Nothing Then
                        SetTextDelegate(MSG_ERROR_STRING & "SendDivControl_Click" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)

                        conn.Close()
                        conn = Nothing
                        Exit Sub
                    End If
                    div_string = ""
                    order_num += rs_div.RecordCount.ToString
                    str2 = ""
                    While rs_div.EOF = False

                        str2 = Com_inf.Dec_to_Hex(Trim(rs_div.Fields("hour_beg").Value), 2) & " " & Com_inf.Dec_to_Hex(Trim(rs_div.Fields("min_beg").Value), 2) & " " & Com_inf.Dec_to_Hex(Trim(rs_div.Fields("second_beg").Value), 2)  '时间转换成两位的十六进制
                        control_method = Trim(rs_div.Fields("mod").Value)
                        If control_method = "回路开" Then
                            control_method = "单灯开"
                            '如果是单灯开，只有有单灯，该命令才起作用
                            If rs.Fields("lamp_id").Value Is System.DBNull.Value Then
                                str2 = ""
                                order_num -= 1
                                rs_div.MoveNext()
                                Continue While
                            End If
                        Else
                            If control_method = "回路关" Then
                                control_method = "单灯闭"
                                '如果是单灯开，只有有单灯，该命令才起作用
                                If rs.Fields("lamp_id").Value Is System.DBNull.Value Then
                                    str2 = ""
                                    order_num -= 1
                                    rs_div.MoveNext()
                                    Continue While
                                End If
                            End If
                        End If
                        '单灯遇到整体命令也不执行
                        If rs.Fields("lamp_id").Value IsNot System.DBNull.Value And control_method <> "单灯闭" And control_method <> "单灯开" Then
                            str2 = ""
                            order_num -= 1
                            rs_div.MoveNext()
                            Continue While
                        End If

                        sql = "select control_id from control_method where control_inf='" & control_method & "'"
                        rs_mod = DBOperation.SelectSQL(conn, sql, msg)
                        If rs_mod Is Nothing Then
                            SetTextDelegate(MSG_ERROR_STRING & "SendDivControl_Click" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)

                            conn.Close()
                            conn = Nothing
                            Exit Sub
                        End If
                        control_id = ""
                        If rs_mod.RecordCount > 0 Then
                            control_id = Trim(rs_mod.Fields("control_id").Value).ToUpper
                        End If
                        If rs.Fields("lamp_id").Value Is System.DBNull.Value Then
                            lamp_id_bin = Com_inf.Dec_to_Bin(rs.Fields("type_id").Value, 5) & Com_inf.Dec_to_Bin(1, 11)
                            lamp_id_hex = Com_inf.BIN_to_HEX(lamp_id_bin)  '灯的二进制编码

                            str2 &= " " & Mid(roadID, 1, 2) & " " & Mid(roadID, 3, 2) & " " & Mid(lamp_id_hex, 1, 2) & " " & Mid(lamp_id_hex, 3, 2) & " " & control_id & " " '共3个控制时段，每个时段的格式为：hour+min+second+mod
                        Else
                            str1 = Trim(rs.Fields("lamp_id").Value)
                            lamp_id_bin = Com_inf.Dec_to_Bin(Mid(Trim(rs.Fields("lamp_id").Value), 5, 2), 5) & Com_inf.Dec_to_Bin(Mid(Trim(rs.Fields("lamp_id").Value), 7, LAMP_ID_LEN), 11) '十六位长度的终端编号二进制
                            str = Com_inf.BIN_to_HEX(lamp_id_bin)
                            While str.Length < 4
                                str = "0" & str
                            End While
                            str2 &= " " & Mid(roadID, 1, 2) & " " & Mid(roadID, 3, 2) & " " & Mid(str, 1, 2) & " " & Mid(str, 3, 2) & " " & control_id & " " '共3个控制时段，每个时段的格式为：hour+min+second+mod


                        End If
                        str2 &= "11 64 "
                        div_string &= str2
                        rs_div.MoveNext()


                    End While
                    sendString &= div_string

                    rs_box.MoveNext()

                End While

                rs.MoveNext()
            End While
            If order_num > 20 Then
                MsgBox(Val(Trim(rs_boxorder.Fields("control_box_id").Value)) & "号主控箱下行命令超过20条，请重新设置控制模式")
            Else

                If order_num > 0 And order_num <= 20 Then  '有该电控箱的时段信息
                    sendString = Com_inf.Dec_to_Hex(order_num.ToString, 2) & " " & sendString

                    sql = "insert into TimeControl (RoadIMEI, CMDType, CMDContent, HandlerFlag, CreateTime) values ('" & IMEI & "', '" & cmdType & "', '" & Trim(sendString) & "' ,0, '" & Now & "')"
                    DBOperation.ExecuteSQL(conn, sql, msg)  '增加一条短信

                End If
            End If


            rs_boxorder.MoveNext()
        End While

        MsgBox("时段控制模式下行成功", , PROJECT_TITLE_STRING)


        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        If rs_box.State = 1 Then
            rs_box.Close()
            rs_box = Nothing
        End If
        If rs_mod.State = 1 Then
            rs_mod.Close()
            rs_mod = Nothing
        End If
        If rs_div.State = 1 Then
            rs_div.Close()
            rs_div = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    '''' <summary>
    ''''  每隔一段时间检测一下端口，如果没有连接短信猫，则提示处于断开状态，
    '''' 不做任何操作如果连接上了则检测短信表中是否有可发送的短信（暂时没用）
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    '''' <remarks></remarks>
    'Private Sub BackgroundWorkerSendMsg_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorkerSendMsg.DoWork

    '    While Me.BackgroundWorkerSendMsg.CancellationPending = False
    '        Try
    '            SendOrder()  '搜寻数据库，看是否有没有发送的短信

    '            System.Threading.Thread.Sleep(5000)
    '        Catch ex As Exception
    '            ex.ToString()
    '        End Try

    '    End While
    'End Sub

    '''' <summary>
    '''' 检测数据库中，看是否有要发送的短信(暂时没用)
    '''' </summary>
    '''' <remarks></remarks>
    'Private Sub SendOrder()
    '    Dim rs As New ADODB.Recordset
    '    Dim conn As New ADODB.Connection
    '    Dim sql As String
    '    Dim msg As String
    '    Dim MsgData() As String  '信息的内容
    '    Dim i As Integer = 0
    '    Dim ret As Boolean
    '    Dim sendstring As String
    '    Dim phonenum As String  '电话号码
    '    msg = ""
    '    sql = ""
    '    sendstring = ""
    '    DBOperation.OpenConn(conn)
    '    sql = "select * from SendGSM_Modem where HandlerFlag=0"
    '    rs = DBOperation.SelectSQL(conn, sql, msg)
    '    If rs Is Nothing Then
    '        MsgBox("数据库连接出错", , PROJECT_TITLE_STRING)
    '        conn.Close()
    '        conn = Nothing
    '        Exit Sub
    '    End If

    '    While rs.EOF = False
    '        '将数据库中未标志的短信从串口发送出去
    '        MsgData = Trim(rs.Fields("SendContent").Value).Split(" ")
    '        sendstring = Asc("A") & " "
    '        sendstring &= Asc("A") & " "
    '        i = 0
    '        While i < MsgData.Length
    '            sendstring &= Convert.ToByte(MsgData(i), 16) & " "
    '            i += 1
    '        End While
    '        sendstring &= Asc("B") & " "
    '        sendstring &= Asc("B")

    '        Try
    '            phonenum = Trim(rs.Fields("PhoneNumber").Value)
    '            ret = AxModem1.sendMsg(phonenum, sendstring)
    '            If ret Then
    '                ModemState.Text = "消息提示：发送成功"
    '            Else
    '                ModemState.Text = "消息提示：发送失败"
    '            End If
    '        Catch ex As Exception
    '            MsgBox("短信猫配置出错", , PROJECT_TITLE_STRING)
    '            ModemState.Text = "短信猫配置出错"
    '        End Try

    '        rs.Fields("HandlerFlag").Value = 1
    '        rs.Update()
    '        rs.MoveNext()

    '    End While

    'End Sub

    Private Sub dgv_control_box_list_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv_control_box_list.CellClick
        m_indexbox = dgv_control_box_list.CurrentRow.Index
    End Sub

    ''' <summary>
    '''  选择模式级别
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mod_time_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mod_time.DropDown
        mod_time.Items.Clear()
        Dim i As Integer = 0
        If Me.div_time_control.Checked = True Then  '平时控制模式
            While i < g_divname.Length
                If g_divname(i) Is Nothing Then
                    i += 1
                Else
                    mod_time.Items.Add(g_divname(i))  '平时控制模式名称
                    i += 1
                End If

            End While

        Else  '特殊控制模式
            While i < g_specialdivname.Length
                If g_specialdivname(i) Is Nothing Then
                    i += 1
                Else
                    mod_time.Items.Add(g_specialdivname(i))  '特殊控制模式名称
                    i += 1
                End If

            End While
        End If
    End Sub


    Private Sub type_control_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles type_control.Click
        If type_control.Checked = True Then
            lamp_id_all.Enabled = False
            lamp_type_all.Enabled = True
            single_all_open.Enabled = True
            double_all_open.Enabled = True
            open_1_3.Enabled = False
            all_open.Checked = True  '选择单灯控制模式，默认情况下为开灯
        End If
    End Sub

    Private Sub lamp_type_all_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lamp_type_all.DropDown
        Com_inf.Select_type_name(box_all, lamp_type_all, Me.type_id_string)

    End Sub

    Private Sub box_time_control_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles box_time_control.Click
        '区域名称
        If box_time_control.Checked = True Then
            city_time.Enabled = True
            area_time.Enabled = True
            street_time.Enabled = True
            box_time.Enabled = True
            type_time.Enabled = False
        End If
    End Sub

    Private Sub type_time_control_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles type_time_control.Click
        If type_time_control.Checked = True Then '类型
            city_time.Enabled = True
            area_time.Enabled = True
            street_time.Enabled = True
            box_time.Enabled = True
            type_time.Enabled = True
        End If
    End Sub

    Private Sub type_time_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles type_time.DropDown
        Com_inf.Select_type_name(box_time, type_time, Me.type_id)
    End Sub

    Private Sub dgv_control_box_list_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv_control_box_list.CellContentClick
        m_indexbox = dgv_control_box_list.CurrentRow.Index
    End Sub

    Private Sub city_time_control_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles city_time_control.Click
        If city_time_control.Checked = True Then
            city_time.Enabled = True
            area_time.Enabled = False
            street_time.Enabled = False
            box_time.Enabled = False
            type_time.Enabled = False

        End If
    End Sub

    Private Sub area_time_control_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles area_time_control.Click
        If area_time_control.Checked = True Then
            city_time.Enabled = True
            area_time.Enabled = True
            street_time.Enabled = False
            box_time.Enabled = False
            type_time.Enabled = False
        End If
    End Sub

    Private Sub street_time_control_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles street_time_control.Click
        If street_time_control.Checked = True Then
            city_time.Enabled = True
            area_time.Enabled = True
            street_time.Enabled = True
            box_time.Enabled = False
            type_time.Enabled = False
        End If
    End Sub

    Private Sub city_time_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles city_time.DropDown
        Com_inf.Select_city_name(city_time)
    End Sub

    Private Sub city_time_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles city_time.SelectedIndexChanged
        Com_inf.Select_area_name(city_time, area_time)
        Com_inf.Select_street_name(city_time, area_time, street_time)
        Com_inf.Select_box_name_level(city_time, area_time, street_time, box_time)
        Com_inf.Select_type_name(box_time, type_time, type_id)
    End Sub

    Private Sub area_time_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles area_time.DropDown
        If city_time.Text <> "" Then
            Com_inf.Select_area_name(city_time, area_time)

        End If
    End Sub

    Private Sub area_time_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles area_time.SelectedIndexChanged
        Com_inf.Select_street_name(city_time, area_time, street_time)
        Com_inf.Select_box_name_level(city_time, area_time, street_time, box_time)
        Com_inf.Select_type_name(box_time, type_time, type_id)
    End Sub

    Private Sub street_time_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles street_time.DropDown
        Com_inf.Select_street_name(city_time, area_time, street_time)
    End Sub

    Private Sub street_time_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles street_time.SelectedIndexChanged
        Com_inf.Select_box_name_level(city_time, area_time, street_time, box_time)
        Com_inf.Select_type_name(box_time, type_time, type_id)
    End Sub

    Private Sub box_time_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles box_time.SelectedIndexChanged
        Com_inf.Select_type_name(box_time, type_time, type_id)
    End Sub
    ''' <summary>
    ''' 定位到主控箱
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub find_box_pos(ByVal control_box_name As String)
        If tb_map_size.Value <> MAP_MID_SIZE Then
            g_changemapvalue = MAP_MID_SIZE
            tb_map_size.Value = g_changemapvalue
            map_size_id.Text = "地图尺寸：100 %"
        End If

        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim posx As Integer '点的X坐标
        Dim posy As Integer '点的Y坐标
        Dim map_path, smallmap_path, map_id As String  '大图和小图的路径
        m_zhishitime = 0 '将指示图标显示的时间清零
        msg = ""
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If

        '判断该电控箱是否在目前的地图
        sql = "select map_list.id, map_list.map_name, control_box.control_box_id, control_box_name, " _
                     & "control_box.pos_y, control_box.pos_x from control_box INNER JOIN street ON control_box.street_id =" _
                     & " street.street_id INNER JOIN area ON street.area_id = area.id INNER JOIN map_list ON area.area = map_list.area " _
                     & "where control_box_name='" & control_box_name & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            SetTextDelegate(MSG_ERROR_STRING & "box_inf_list_MouseDoubleClick" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs.RecordCount > 0 Then
            If rs.Fields("id").Value <> g_choosemapid Then  '如果不是当前的地图则进行地图跳转
                map_id = rs.Fields("id").Value
                g_choosemapid = map_id
                g_mapname = Trim(rs.Fields("map_name").Value)
                map_path = "map\" & g_mapname & ".jpg"  '地图路径
                smallmap_path = "map\s" & g_mapname & ".jpg"  '地图路径
                If pb_map.BackgroundImage IsNot Nothing Then
                    pb_map.BackgroundImage.Dispose()
                End If
                If pb_small_map.BackgroundImage IsNot Nothing Then
                    pb_small_map.BackgroundImage.Dispose()
                End If
                pb_map.BackgroundImage = System.Drawing.Image.FromFile(map_path) '载入地图
                pb_small_map.BackgroundImage = System.Drawing.Image.FromFile(smallmap_path) '载入鹰眼地图
                pb_map.Width = System.Drawing.Image.FromFile(map_path).Width  '设置map的宽
                pb_map.Height = System.Drawing.Image.FromFile(map_path).Height '设置map的高
                m_lamp = New Bitmap(pb_map.Width, pb_map.Height)   '绘制终端的bmp图
                g_lampmap.Clear(Color.Empty)
                g_lampmap = Graphics.FromImage(m_lamp)  '载入灯的图片
                g_mapsizevalue = pb_map.Size '地图的尺寸
                g_choosemapid = map_id
                Me.map_area.Text = "地图区域：" & g_mapname

            End If
            posx = rs.Fields("pos_x").Value '找到该灯的信息
            posy = rs.Fields("pos_y").Value
        Else
            rs.Close()
            rs = Nothing
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
        If tb_map_size.Value <> MAP_MID_SIZE Then
            tb_map_size.Value = MAP_MID_SIZE
        End If
        '点在地图上的位置
        Me.pb_map.Left = pb_map.Location.X + (Me.GroupBox1.Width / 2 - (pb_map.Location.X + posx))
        Me.pb_map.Top = pb_map.Location.Y + (Me.GroupBox1.Height / 2 - (pb_map.Location.Y + posy))
        '显示指示箭头
        Me.zhishijiantou.Top = Me.GroupBox1.Height / 2 - 25
        Me.zhishijiantou.Left = Me.GroupBox1.Width / 2
        Me.zhishijiantou.Visible = True
    End Sub

    ''' <summary>
    ''' 获取主控箱的用电类型
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub find_box_powerinf()
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String
        '  Dim inf_string() As String  '记录主控箱的信息字符串
        Dim kaiguan_string As String
        Dim control_box_name As String = Trim(Me.tv_box_inf_list.SelectedNode.Text)  '主控箱名称
        '  Dim kaiguan_id As Integer '开关量
        '  Dim kaiguan_state As String '开关量的状态
        Dim row As Integer = Me.dgv_box_inf.Rows.Count
        msg = ""
        sql = "select power_type, kaiguan_string from control_box where control_box_name='" & control_box_name & "'"
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            SetTextDelegate(MSG_ERROR_STRING & "find_box_powerinf" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs.RecordCount > 0 Then
            ' dgv_box_inf.Rows.Add()
            If rs.Fields("kaiguan_string").Value Is System.DBNull.Value Then
                kaiguan_string = "" '16位开关量字符串
            Else
                kaiguan_string = Trim(rs.Fields("kaiguan_string").Value) '16位开关量字符串
            End If
            If rs.Fields("power_type").Value IsNot System.DBNull.Value Then
                rtb_control_box_inf.Text &= "供电电源：" & Trim(rs.Fields("power_type").Value) & vbCrLf
                row += 1
            End If
        Else
            GoTo finish
        End If
finish:
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing

    End Sub


    ''' <summary>
    ''' 将模式下放后等待确认数据返回
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BackgroundWorkerModXiaFang_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorkerModXiaFang.DoWork
        If g_modtag = 1 Then  '发送模式配置
            ModXiaFang()
        End If

        If g_modtag = 2 Then '发送模拟量配置
            sendHuilu()
        End If

        If g_modtag = 3 Then  '发送开关量
            sendKaiGuan()
        End If

        If g_modtag = 4 Then  '发送测量板个数+报警参数
            sendAlarmdata()
        End If

        If g_modtag = 5 Then  '清空模式配置
            ModClear()
        End If

        If g_modtag = 6 Then  '发送测量板个数
            sendTestBoardNum()
        End If
    End Sub

    ''' <summary>
    ''' 发送测量板个数，2011年7月19日将旧版的主控箱剔除
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub sendTestBoardNum()
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim imei As String
        Dim boardnum As String
        Dim nowtime As DateTime = Now
        Dim control_box_name As String
        Dim control_box_id As String
        Dim box_type As Integer '主控箱版本
        Dim boxid_hex As String '主控箱编号的十六进制

        msg = ""
        If g_mod_controlboxname = "" Then
            sql = "select control_box_type,board_num,IMEI,control_box_name, control_box_id from Box_IMEI order by control_box_id"
        Else
            sql = "select control_box_type,board_num,IMEI,control_box_name, control_box_id from Box_IMEI where control_box_name='" & g_mod_controlboxname & "' order by control_box_id"

        End If
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            SetTextDelegate(MSG_ERROR_STRING & "sendTestBoardNum" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)

            conn.Close()
            conn = Nothing
            Exit Sub
        End If

        While rs.EOF = False
            imei = Trim(rs.Fields("IMEI").Value)
            control_box_name = Trim(rs.Fields("control_box_name").Value)
            control_box_id = Trim(rs.Fields("control_box_id").Value)
            boxid_hex = Com_inf.Dec_to_Hex(control_box_id, 4)
            boxid_hex = Mid(boxid_hex, 1, 2) & " " & Mid(boxid_hex, 3, 2)
            box_type = rs.Fields("control_box_type").Value
            If box_type = 1 Then
                rs.MoveNext()
                Continue While
            End If
            If rs.Fields("board_num").Value Is System.DBNull.Value Then
                boardnum = "00"
            Else
                boardnum = "0" & Trim(rs.Fields("board_num").Value)
            End If
            boardnum = boxid_hex & " " & boardnum
            Insert_db_config(control_box_id, imei, boardnum, HG_TYPE.HG_SET_TESTBOARDNUM)
            '发送数据后，等待返回状态
            SetTextDelegate(control_box_name & "配置测量板个数......" & vbCrLf, True, rtb_info_list)
            System.Threading.Thread.Sleep(500)
            Get_ConfigInf(HG_TYPE.HG_GET_TESTBOARDNUM, control_box_name, "测量板", control_box_id, nowtime)  '获取返回状态
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
    ''' 清空模式配置
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ModClear()
        Dim rs_box As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String

        Dim control_box_id As String  '电控箱编号
        Dim IMEI As String  'GPRS编号
        Dim cmdType As String
        Dim sendString As String  '发送的字符串
        Dim rs_boxorder As New ADODB.Recordset  '按电控箱写字符
        Dim nowtime, nowtime2 As DateTime
        Dim weektime(1) As DateTime   '存放未来一天的经纬度开关时间
        Dim weekoper(1) As String  '未来一天中每个日期对应的开关操作
        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim box_imei As m_box_IMEI
        Dim control_box_name As String
        Dim boxid_hex As String '主控箱编号的十六进制

        nowtime = System.Convert.ToDateTime(Now.Year.ToString & "-" & Now.Month.ToString & "-" & _
         Now.Day.ToString & " 0:0:0")


        cmdType = HG_TYPE.HG_SET_DIVMOD_CONFIG
        sendString = ""
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        msg = ""
        IMEI = ""

        j = 0

        '2011年5月25日增加了按主控箱名称进行模式下放
        If g_mod_controlboxname = "" Then
            sql = "select * from control_box order by control_box_id"
        Else
            sql = "select * from control_box where control_box_name='" & g_mod_controlboxname & "' order by control_box_id "

        End If
        rs_box = DBOperation.SelectSQL(conn, sql, msg)
        If rs_box Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "ModXiaFang", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs_box.RecordCount <= 0 Then
            '表示没有下放数据
            SetTextDelegate("没有下放数据，请重新设置" & vbCrLf, False, rtb_info_list)

        Else
            While rs_box.EOF = False  '对每个主控箱进行下放统计

                sendString = ""
                control_box_id = Trim(rs_box.Fields("control_box_id").Value)
                boxid_hex = Com_inf.Dec_to_Hex(control_box_id, 4)
                boxid_hex = Mid(boxid_hex, 1, 2) & " " & Mid(boxid_hex, 3, 2)
                control_box_name = Trim(rs_box.Fields("control_box_name").Value)
                '获取主控箱的GPRS编号
                sql = "select IMEI,control_box_id,control_box_name from Box_IMEI where control_box_id='" & control_box_id & "'"
                rs_boxorder = DBOperation.SelectSQL(conn, sql, msg)
                If rs_box Is Nothing Then
                    MsgBox(MSG_ERROR_STRING & "ModXiaFang", , PROJECT_TITLE_STRING)
                    conn.Close()
                    conn = Nothing
                    Exit Sub
                End If
                If rs_boxorder.RecordCount > 0 Then
                    IMEI = Trim(rs_boxorder.Fields("IMEI").Value)
                Else
                    Continue While
                End If
                sendString = boxid_hex & " 00"
                nowtime2 = Now
                Insert_db_config(control_box_id, IMEI, Trim(sendString), cmdType)
                box_imei.IMEI = IMEI
                box_imei.control_box_id = control_box_id
                box_imei.control_box_name = control_box_name
                'control_list.Add(box_imei)  '将IMEI号增加到列表中
                '发送数据后，等待返回状态
                SetTextDelegate(control_box_name & "模式清空下放......" & vbCrLf, True, rtb_info_list)
                System.Threading.Thread.Sleep(500)
                Get_ConfigInf(HG_TYPE.HG_GET_DIVMOD_CONFIG, control_box_name, "模式清空", control_box_id, nowtime2)  '获取返回状态
                ' GetModInf(IMEI, nowtime2, box_imei.control_box_name, control_box_id)
                rs_box.MoveNext()   '每个主控箱
            End While
        End If
        '   MsgBox("时段控制模式下行成功", , PROJECT_TITLE_STRING)



        If rs_box.State = 1 Then
            rs_box.Close()
            rs_box = Nothing
        End If
        If rs_boxorder.State = 1 Then
            rs_boxorder.Close()
            rs_boxorder = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub
    ''' <summary>
    ''' 发送模拟量配置,2011年7月19日，将1版的电控箱剔除出模拟量下放
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub sendHuilu()
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String
        Dim rs As New ADODB.Recordset
        Dim imei As String
        Dim control_box_name As String '主控箱名称
        Dim control_box_id As String '主控箱编号
        Dim sendcontent As String = ""  '发送的字符串
        Dim i As Integer = 1
        Dim group_id As Integer '发送包的组号
        Dim nowtime As DateTime = Now
        Dim box_type As Integer '主控箱版本
        '  Dim boxid_hex As String '主控箱编号的16进制 
        Dim j As Integer
        msg = ""

        If g_mod_controlboxname = "" Then
            sql = "SELECT Box_IMEI.control_box_type, Box_IMEI.IMEI, huilu_inf.presure_type, huilu_inf.huilu_id, Box_IMEI.control_box_name, Box_IMEI.control_box_id FROM " _
        & " Box_IMEI INNER JOIN huilu_inf ON Box_IMEI.control_box_name = huilu_inf.control_box_name" _
         & " ORDER BY Box_IMEI.control_box_id, huilu_inf.huilu_id"
        Else
            sql = "SELECT Box_IMEI.control_box_type, Box_IMEI.IMEI, huilu_inf.presure_type, huilu_inf.huilu_id, Box_IMEI.control_box_name, Box_IMEI.control_box_id FROM " _
        & " Box_IMEI INNER JOIN huilu_inf ON Box_IMEI.control_box_name = huilu_inf.control_box_name where Box_IMEI.control_box_name='" & g_mod_controlboxname & "'" _
         & " ORDER BY Box_IMEI.control_box_id, huilu_inf.huilu_id"
            ' sql = "select * from control_box where control_box_name='" & g_mod_controlboxname & "' order by control_box_id "

        End If
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            SetTextDelegate(MSG_ERROR_STRING & "sendHuilu" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs.RecordCount > 0 Then
            imei = Trim(rs.Fields("IMEI").Value)
            control_box_name = Trim(rs.Fields("control_box_name").Value)
            control_box_id = Trim(rs.Fields("control_box_id").Value)
            'boxid_hex = Com_inf.Dec_to_Hex(control_box_id, 4)
            'boxid_hex = Mid(boxid_hex, 1, 2) & " " & Mid(boxid_hex, 3, 2)


            While rs.EOF = False
                box_type = rs.Fields("control_box_type").Value
                If box_type = 1 Then  '第一版本的不下放
                    rs.MoveNext()
                    control_box_name = Trim(rs.Fields("control_box_name").Value)
                    control_box_id = Trim(rs.Fields("control_box_id").Value)
                    imei = Trim(rs.Fields("IMEI").Value)
                    Continue While
                End If
                If imei = Trim(rs.Fields("IMEI").Value) Then
                    j = rs.Fields("huilu_id").Value
                    If rs.Fields("huilu_id").Value = i Then
                        If rs.Fields("presure_type").Value = 0 Then
                            sendcontent &= (rs.Fields("presure_type").Value).ToString & " "
                        Else
                            sendcontent &= (4 / rs.Fields("presure_type").Value).ToString & " "
                        End If
                    Else
                        sendcontent &= "0 "
                        GoTo next1
                    End If
                    If i Mod 12 = 0 Then
                        group_id = i \ 12 - 1
                        sendcontent = group_id & " " & sendcontent  '组号及打包数据一起发送一组数据
                        'sql = "insert into TimeControl(RoadIMEI, CMDType, CMDContent, HandlerFlag, Createtime) values('" & imei & "'," & HG_TYPE.HG_SET_MN_GONFIG & ",'" & sendcontent & "',0,'" & nowtime & "')"
                        'DBOperation.ExecuteSQL(conn, sql, msg)
                        Insert_db_config(control_box_id, imei, sendcontent, HG_TYPE.HG_SET_MN_GONFIG)
                        sendcontent = ""
                    End If
                    rs.MoveNext()
next1:
                    i += 1
                Else
                    If i Mod 12 = 0 Then  '用户定义的回路数据为12,24,36 路，将数据上传
                        i = 1
                        group_id = i \ 12 - 1
                        sendcontent = group_id & " " & sendcontent  '组号及打包数据一起发送一组数据

                        ' sql = "insert into TimeControl(RoadIMEI, CMDType, CMDContent, HandlerFlag, Createtime) values('" & imei & "'," & HG_TYPE.HG_SET_MN_GONFIG & ",'" & sendcontent & "',0,'" & nowtime & "')"

                    Else  '不足12的倍数，补齐后发送数据
                        group_id = i \ 12
                        While i Mod 12 <> 0
                            sendcontent &= "0 "
                            i += 1
                        End While
                        sendcontent &= "0"

                        sendcontent = group_id & " " & sendcontent  '组号及打包数据一起发送一组数据

                        'sql = "insert into TimeControl(RoadIMEI, CMDType, CMDContent, HandlerFlag, Createtime) values('" & imei & "'," & HG_TYPE.HG_SET_MN_GONFIG & ",'" & Trim(sendcontent) & "',0,'" & nowtime & "')"
                    End If
                    Insert_db_config(control_box_id, imei, sendcontent, HG_TYPE.HG_SET_MN_GONFIG)
                    '发送数据后，等待返回状态
                    SetTextDelegate(control_box_name & "配置模拟量......" & vbCrLf, True, rtb_info_list)

                    System.Threading.Thread.Sleep(500)
                    Get_ConfigInf(HG_TYPE.HG_GET_MN_CONFIG, control_box_name, "模拟量", control_box_id, nowtime)  '获取返回状态
                    control_box_name = Trim(rs.Fields("control_box_name").Value)
                    control_box_id = Trim(rs.Fields("control_box_id").Value)
                    'boxid_hex = Com_inf.Dec_to_Hex(control_box_id, 4)
                    'boxid_hex = Mid(boxid_hex, 1, 2) & " " & Mid(boxid_hex, 3, 2)
                    imei = Trim(rs.Fields("IMEI").Value)
                    '  box_type = rs.Fields("control_box_type").Value
                    i = 1
                    sendcontent = ""
                End If
            End While
            If i Mod 12 = 1 Then
                GoTo next2
            End If
            If i Mod 12 = 0 Then  '用户定义的回路数据为12,24,36 路，将数据上传
                ' i = 1
                group_id = i \ 12 - 1
                sendcontent = group_id & " " & sendcontent & "0"  '组号及打包数据一起发送一组数据
                ' sql = "insert into TimeControl(RoadIMEI, CMDType, CMDContent, HandlerFlag, Createtime) values('" & imei & "'," & HG_TYPE.HG_SET_MN_GONFIG & ",'" & sendcontent & "',0,'" & Now & "')"

            Else  '不足12的倍数，补齐后发送数据
                group_id = i \ 12
                While i Mod 12 <> 0
                    sendcontent &= "0 "
                    i += 1
                End While
                sendcontent &= "0"
                sendcontent = group_id & " " & sendcontent  '组号及打包数据一起发送一组数据
                ' sql = "insert into TimeControl(RoadIMEI, CMDType, CMDContent, HandlerFlag, Createtime) values('" & imei & "'," & HG_TYPE.HG_SET_MN_GONFIG & ",'" & sendcontent & "',0,'" & Now & "')"
            End If
            ' DBOperation.ExecuteSQL(conn, sql, msg)
            Insert_db_config(control_box_id, imei, sendcontent, HG_TYPE.HG_SET_MN_GONFIG)
next2:
            '发送数据后，等待返回状态
            SetTextDelegate(control_box_name & "配置模拟量......" & vbCrLf, True, rtb_info_list)
            System.Threading.Thread.Sleep(500)
            Get_ConfigInf(HG_TYPE.HG_GET_MN_CONFIG, control_box_name, "模拟量", control_box_id, Now)  '获取返回状态
        End If
        ' MsgBox("发送模拟量配置成功", , PROJECT_TITLE_STRING)
        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    ''' 获取配置模拟量的返回状态
    ''' </summary>
    ''' <param name="orderType" >命令的类型</param>
    ''' <param name="control_box_name" >主控箱名称</param>
    ''' <param name="orderString" >命令名称</param>
    ''' <param name="control_box_id" >主控箱编号</param>
    ''' <remarks></remarks>
    Private Sub Get_ConfigInf(ByVal orderType As Integer, ByVal control_box_name As String, ByVal orderString As String, ByVal control_box_id As String, ByVal createtime As DateTime)
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim hex_box As String '电控箱的16进制编号
        Dim i As Integer = 0
        Dim recContent() As String '接收字符串
        hex_box = Com_inf.Dec_to_Hex(control_box_id, 4)
        hex_box = Mid(hex_box, 1, 2) & " " & Mid(hex_box, 3, 2)
        msg = ""
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        While i < CONTROL_WAIT_TIME
            sql = "select * from RoadLightStatus where StatusContent like '" & hex_box & "%' and PackType='" & orderType & "' and HandlerFlag=3 and createtime>'" & createtime & "' order by id desc"
            rs = DBOperation.SelectSQL(conn, sql, msg)
            If rs Is Nothing Then
                SetTextDelegate(MSG_ERROR_STRING & "Get_ConfigInf" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)
                conn.Close()
                conn = Nothing
                Exit Sub
            End If
            If rs.RecordCount > 0 Then
                recContent = Trim(rs.Fields("StatusContent").Value).Split(" ")
                If recContent(2) = "01" Then
                    SetTextDelegate(control_box_name & orderString & "配置成功" & "   时间：" & Now & vbCrLf & "********************" & vbCrLf & vbCrLf, True, rtb_info_list)
                    '将配置信息保存
                    Me.Insert_configinf(control_box_name & orderString & "配置成功", 1, control_box_id, control_box_name)
                    '2012年7月13日 如果下放清空模式成功，则将软件中改主控箱的设置也清空
                    If orderType = HG_TYPE.HG_GET_DIVMOD_CONFIG And orderString = "模式清空" And g_clearmod = True Then
                        Com_inf.Del_all_inf(control_box_name)
                    End If
                End If
                If recContent(2) = "00" Then
                    SetTextDelegate(control_box_name & orderString & "配置失败" & "   时间：" & Now & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)
                    '将配置信息保存
                    Me.Insert_configinf(control_box_name & orderString & "配置失败", 0, control_box_id, control_box_name)
                End If
                '将标志位置为1
                sql = "update RoadLightStatus set HandlerFlag=1 where id='" & rs.Fields("id").Value & "' "
                DBOperation.ExecuteSQL(conn, sql, msg)
                Exit While
            End If
            System.Threading.Thread.Sleep(1000)
            i += 1
        End While
        If i >= CONTROL_WAIT_TIME Then
            SetTextDelegate(control_box_name & orderString & "配置超时" & "   时间：" & Now & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)
            '将配置信息保存
            Me.Insert_configinf(control_box_name & orderString & "配置超时", 0, control_box_id, control_box_name)
        End If
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    ''' 发送开关量配置，2011年7月19日，将旧版的主控箱剔除出开关量的下放
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub sendKaiGuan()
        Dim rs, rs_box As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim imei As String
        Dim sendcontent As String
        Dim control_box_name As String '主控箱名称
        Dim control_box_id As String '主控箱编号
        Dim i As Integer = 1
        Dim nowtime As DateTime = Now
        Dim box_type As Integer '主控箱的版本
        ' Dim boxid_hex As String '主控箱编号的十六进制
        msg = ""
        sendcontent = ""
        If g_mod_controlboxname = "" Then
            'sql = "select * from control_box order by control_box_id"
            sql = "select control_box_type, control_box_name,IMEI,control_box_id from Box_IMEI order by control_box_id"
        Else
            sql = "select control_box_type, control_box_name,IMEI,control_box_id from Box_IMEI where control_box_name='" & g_mod_controlboxname & "' order by control_box_id"
            ' sql = "select * from control_box where control_box_name='" & g_mod_controlboxname & "' order by control_box_id "
        End If
        '   sql = "SELECT Box_IMEI.IMEI, kaiguan_list.kaiguan_tag, kaiguan_list.alarm FROM Box_IMEI INNER JOIN kaiguan_list ON Box_IMEI.control_box_name = kaiguan_list.control_box_name order by control_box_id "
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        rs_box = DBOperation.SelectSQL(conn, sql, msg)
        If rs_box Is Nothing Then
            SetTextDelegate(MSG_ERROR_STRING & "sendKaiGuan" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        While rs_box.EOF = False  '每个电控箱进行分别下放
            box_type = rs_box.Fields("control_box_type").Value
            If box_type = 1 Then  '第一版本的不下放
                rs_box.MoveNext()
                Continue While
            End If
            imei = Trim(rs_box.Fields("IMEI").Value)
            control_box_name = Trim(rs_box.Fields("control_box_name").Value)
            control_box_id = Trim(rs_box.Fields("control_box_id").Value)
            'boxid_hex = Com_inf.Dec_to_Hex(control_box_id, 4)
            'boxid_hex = Mid(boxid_hex, 1, 2) & " " & Mid(boxid_hex, 3, 2)


            '  sql = "select kaiguan_tag, alarm from kaiguan_list where control_box_name='" & control_box_name & "' order by kaiguan_tag"
            sql = "select * from tiaobian_alarm order by shiliang_id"
            rs = DBOperation.SelectSQL(conn, sql, msg)
            If rs Is Nothing Then
                SetTextDelegate(MSG_ERROR_STRING & "sendKaiGuan" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)
                conn.Close()
                conn = Nothing
                Exit Sub
            End If
            i = 1
            sendcontent = ""
            While i <= 16
                If rs.EOF = False Then
                    If i = rs.Fields("shiliang_id").Value Then
                        If rs.EOF = False Then
                            sendcontent = rs.Fields("alarm_id").Value.ToString & sendcontent
                            rs.MoveNext()
                        Else
                            sendcontent = "0" & sendcontent
                        End If
                    Else
                        sendcontent = "0" & sendcontent
                    End If
                Else
                    sendcontent = "0" & sendcontent
                End If

                i += 1
            End While

            sendcontent = Com_inf.BIN_to_HEX(sendcontent)
            sendcontent = Mid(sendcontent, 3, 2) & " " & Mid(sendcontent, 1, 2)
            'sql = "insert into TimeControl(RoadIMEI, CMDType, CMDContent, HandlerFlag, Createtime) values('" & imei & "'," & HG_TYPE.HG_SET_KG_CONFIG & ",'" & sendcontent & "',0,'" & nowtime & "')"
            'DBOperation.ExecuteSQL(conn, sql, msg)
            Insert_db_config(control_box_id, imei, sendcontent, HG_TYPE.HG_SET_KG_CONFIG)
            '发送数据后，等待返回状态
            SetTextDelegate(control_box_name & "配置开关量......" & vbCrLf, True, rtb_info_list)
            System.Threading.Thread.Sleep(500)
            Get_ConfigInf(HG_TYPE.HG_GET_KG_CONFIG, control_box_name, "开关量", control_box_id, nowtime)  '获取返回状态
            rs_box.MoveNext()
        End While
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        If rs_box.State = 1 Then
            rs_box.Close()
            rs_box = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub
    'Private Sub SetDivMod()
    '    Dim modcontrol_list As New List(Of m_box_IMEI)   '模式下放的电控箱编号的列表
    '    Dim i As Integer = 0
    '    Dim waittime As Integer = 30
    '    Dim sql, msg As String
    '    Dim conn As New ADODB.Connection
    '    Dim rs As New ADODB.Recordset

    '    DBOperation.OpenConn(conn)
    '    msg = ""
    '    sql = ""

    '    ModXiaFang(modcontrol_list)  '将所有的数据下放
    '    While i < modcontrol_list.Count

    '        '发送数据后，等待返回状态
    '        SetTextDelegate(modcontrol_list(i).control_box_name & "模式下放......" & vbCrLf, True, info_list)

    '        i += 1
    '    End While


    '    System.Threading.Thread.Sleep(10000) '休息5秒钟后查询返回状态

    '    While waittime > 0
    '        i = 0
    '        While i < modcontrol_list.Count
    '            sql = "select * from TimeControl where RoadIMEI='" & modcontrol_list(i).IMEI & "' and Createtime>'" & Now.AddMinutes(-10) & "' order by id"
    '            rs = DBOperation.SelectSQL(conn, sql, msg)
    '            If rs Is Nothing Then
    '                MsgBox(MSG_ERROR_STRING, , PROJECT_TITLE_STRING)
    '                conn.Close()
    '                conn = Nothing
    '                Exit Sub
    '            End If
    '            If rs.RecordCount > 0 Then
    '                If Trim(rs.Fields("HandlerFlag").Value) = "4" Then  '表示配置成功
    '                    SetTextDelegate(modcontrol_list(i).control_box_name & "模式配置成功" & vbCrLf, True, info_list)
    '                    '模式下放成功，将信息保存
    '                    Me.Insert_configinf(modcontrol_list(i).control_box_name & "模式配置成功", 1, modcontrol_list(i).control_box_id, modcontrol_list(i).control_box_name)

    '                    modcontrol_list.RemoveAt(i)

    '                End If
    '            End If
    '            i += 1
    '        End While
    '        If modcontrol_list.Count = 0 Then
    '            Exit While
    '        End If
    '        System.Threading.Thread.Sleep(1000)
    '        waittime -= 1
    '    End While
    '    If waittime = 0 Then
    '        '表示有超时的
    '        i = 0
    '        While i < modcontrol_list.Count
    '            SetTextDelegate(modcontrol_list(i).control_box_name & "模式下放超时" & vbCrLf, False, info_list)
    '            '模式下放超时将配置信息保存
    '            Me.Insert_configinf(modcontrol_list(i).control_box_name & "模式下放超时", 0, modcontrol_list(i).control_box_id, modcontrol_list(i).control_box_name)

    '            i += 1

    '        End While
    '    End If

    'End Sub

    Public Sub ModXiaFang()
        Dim rs, rs_div, rs_box, rs_mod, rs_pianyi As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim control_box_id As String  '电控箱编号
        Dim div_level As String  '时段控制级别
        Dim IMEI As String  'GPRS编号
        Dim div_string As String   '时段控制的字符串
        Dim control_id As String '控制模式代号
        Dim cmdType As String
        Dim sendString As String  '发送的字符串
        Dim lamp_id_bin, lamp_id_hex, lamp_id As String '16位长度的二进制
        Dim str, str1, str2, time As String
        Dim order_num As Integer
        Dim rs_boxorder As New ADODB.Recordset  '按电控箱写字符
        Dim control_method As String '控制方法
        Dim nowtime, nowtime2 As DateTime
        Dim weektime(1) As DateTime   '存放未来一天的经纬度开关时间
        Dim weekoper(1) As String  '未来一天中每个日期对应的开关操作
        Dim pianyitime As DateTime  '与偏移量相加减得出的时间
        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim open_pianyi, close_pianyi As Integer  '开关灯的时间偏移量
        Dim box_imei As m_box_IMEI
        Dim control_box_name As String
        Dim control_box_type As Integer  '主控箱版本类型
        Dim holiday_mod As Boolean = False '是否有节假日的控制模式
        Dim boxid_hex As String '主控器标号的十六进制代码
        Dim gonglv_value, diangan_value As String '电子功率及电感
        Dim jwtag, spetag, holitag As Boolean  '判断经纬度，特殊及节假日的控制组合
        nowtime = System.Convert.ToDateTime(Now.Year.ToString & "-" & Now.Month.ToString & "-" & _
         Now.Day.ToString & " 0:0:0")
        cmdType = HG_TYPE.HG_SET_DIVMOD_CONFIG
        sendString = ""
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        msg = ""
        order_num = 0
        IMEI = ""

        j = 0
        jwtag = False
        spetag = False
        holitag = False
        While j < g_modgroup.Length
            If g_modgroup(j) = "经纬度控制模式" Then
                jwtag = True
            End If
            If g_modgroup(j) = "特殊控制模式" Then
                spetag = True
            End If
            If g_modgroup(j) = "节假日控制模式" Then
                holitag = True
            End If
            j += 1
        End While

        '获取经纬度时间
        'sql = "select * from suntime where time>='" & nowtime & "' and time<='" & DateAdd(DateInterval.Day, 1, nowtime) & "' "
        sql = "select * from suntime where month(time)='" & nowtime.Month & "' and day(time)='" & nowtime.Day & "' "

        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            SetTextDelegate(MSG_ERROR_STRING & "ModXiaFang" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)

            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        i = 0
        ReDim weektime(rs.RecordCount)
        ReDim weekoper(rs.RecordCount)
        While rs.EOF = False   '
            weektime(i) = System.Convert.ToDateTime(rs.Fields("time").Value)  '当前的开关灯的控制时间
            weekoper(i) = Trim(rs.Fields("mod").Value)  '控制方式，开或关
            i += 1
            rs.MoveNext()
        End While

        '2011年5月25日增加了按主控箱名称进行模式下放
        If g_mod_controlboxname = "" Then
            sql = "select * from control_box order by control_box_id"
        Else
            sql = "select * from control_box where control_box_name='" & g_mod_controlboxname & "' order by control_box_id "

        End If
        rs_box = DBOperation.SelectSQL(conn, sql, msg)
        If rs_box Is Nothing Then
            SetTextDelegate(MSG_ERROR_STRING & "ModXiaFang" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)

            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs_box.RecordCount <= 0 Then
            '表示没有下放数据
            SetTextDelegate("没有下放数据，请重新设置" & vbCrLf, False, rtb_info_list)

        Else
            While rs_box.EOF = False  '对每个主控箱进行下放统计
                order_num = 0
                sendString = ""
                holiday_mod = False
                control_box_id = Trim(rs_box.Fields("control_box_id").Value)
                control_box_name = Trim(rs_box.Fields("control_box_name").Value)
                control_box_type = rs_box.Fields("control_box_type").Value  '主控箱类型
                '2012年9月7日，对每个命令要判断其主控箱是否停运，如果停运则不执行
                If Com_inf.get_controlbox_state(control_box_id) = False Then
                    rs_box.MoveNext()
                    SetTextDelegate("主控箱：" & control_box_name & "已停止控制！" & vbCrLf, False, rtb_info_list)
                    Continue While
                End If
                If Trim(rs_box.Fields("elec_state").Value) = "节日" Then
                    holiday_mod = True
                End If

                '获取主控箱的GPRS编号
                sql = "select IMEI,control_box_id,control_box_name from Box_IMEI where control_box_id='" & control_box_id & "'"
                rs_boxorder = DBOperation.SelectSQL(conn, sql, msg)
                If rs_box Is Nothing Then
                    SetTextDelegate(MSG_ERROR_STRING & "ModXiaFang" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)

                    conn.Close()
                    conn = Nothing
                    Exit Sub
                End If
                If rs_boxorder.RecordCount > 0 Then
                    IMEI = Trim(rs_boxorder.Fields("IMEI").Value)
                Else
                    Continue While
                End If
                i = 0
                While i < weektime.Length - 1   '按每天的日期
                    If jwtag = True Then '判断是否有经纬度控制的命令
                        '**********************经纬度时段设置进行下放************************************
                        sql = "select * from pianyi where substring(lamp_id,1,4)='" & Trim(rs_box.Fields("control_box_id").Value) & "'"
                        rs_pianyi = DBOperation.SelectSQL(conn, sql, msg)
                        If rs_pianyi Is Nothing Then
                            SetTextDelegate(MSG_ERROR_STRING & "ModXiaFang" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)
                            conn.Close()
                            conn = Nothing
                            Exit Sub
                        End If
                        While rs_pianyi.EOF = False
                            open_pianyi = rs_pianyi.Fields("open_pianyi").Value  '开灯偏移量
                            close_pianyi = rs_pianyi.Fields("close_pianyi").Value '关灯偏移量
                            If weekoper(i) = "开" Then
                                pianyitime = weektime(i).AddMinutes(open_pianyi)
                            Else
                                pianyitime = weektime(i).AddMinutes(close_pianyi)

                            End If
                            'year = Com_inf.Dec_to_ox(pianyitime.Year.ToString, 4)
                            'time = Mid(year, 1, 2) & " " & Mid(year, 3, 2) & " " & _
                            '                    Com_inf.Dec_to_ox(Val(pianyitime.Month.ToString), 2) & " " & _
                            '                    Com_inf.Dec_to_ox(Val(pianyitime.Day.ToString), 2) & " " & _
                            '                    Com_inf.Dec_to_ox(Val(pianyitime.Hour.ToString), 2) & " " & _
                            '                    Com_inf.Dec_to_ox(Val(pianyitime.Minute.ToString), 2) & " " & _
                            '                    Com_inf.Dec_to_ox(Val(pianyitime.Second.ToString), 2)  '日期部分
                            time = Com_inf.Dec_to_Hex(Val(pianyitime.Hour.ToString), 2) & " " & _
                                                Com_inf.Dec_to_Hex(Val(pianyitime.Minute.ToString), 2) & " " & _
                                                Com_inf.Dec_to_Hex(Val(pianyitime.Second.ToString), 2)  '日期部分

                            str1 = Com_inf.Dec_to_Hex(control_box_id, 4)
                            str1 = Mid(str1, 1, 2) & " " & Mid(str1, 3, 2)  '电控箱的的十六位编号
                            lamp_id = Trim(rs_pianyi.Fields("lamp_id").Value)  '灯的编号
                            lamp_id_bin = Com_inf.Dec_to_Bin(Mid(lamp_id, 5, 2), 5) & Com_inf.Dec_to_Bin(Mid(lamp_id, 7, LAMP_ID_LEN), 11)
                            lamp_id_hex = Com_inf.BIN_to_HEX(lamp_id_bin)
                            str1 = str1 & " " & Mid(lamp_id_hex, 1, 2) & " " & Mid(lamp_id_hex, 3, 2)  '节点编号的十六进制
                            str = time & " " & str1

                            If weekoper(i) = "开" Then
                                '开的命令
                                If control_box_type = 1 Then
                                    str = str & " 1B 11 64 "
                                Else
                                    str = str & " 1B 11 64 00 "
                                End If

                            Else '关的命令
                                If control_box_type = 1 Then
                                    str = str & " 1C 13 00 "
                                Else
                                    str = str & " 1C 13 00 00 "
                                End If

                            End If

                            sendString &= str  '下放的控制命令
                            order_num += 1  '控制命令条数
                            rs_pianyi.MoveNext()

                        End While

                    End If
                    i += 1 '每天的日期
                End While
                '*********************************************************************************************
                str1 = ""

                If spetag = True And holiday_mod = False Then  '特殊的控制模式(该主控箱今天没有节假日控制模式)
                    sql = "select div_time_level, control_box_id, lamp_id , type_id from control_level_view where control_box_id='" & control_box_id & "' and week_id='" & Now.DayOfWeek & "'"

                    rs = DBOperation.SelectSQL(conn, sql, msg)
                    If rs Is Nothing Then
                        SetTextDelegate(MSG_ERROR_STRING & "SendDivControl_Click" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)

                        conn.Close()
                        conn = Nothing
                        Exit Sub
                    End If
                    '控制方式为老的控制命令格式，需要在最后加一个字节的00，补足8个字节
                    While rs.EOF = False
                        div_level = Trim(rs.Fields("div_time_level").Value)

                        '查询时段控制的时间
                        sql = "select * from div_time where div_level='" & div_level & "'"
                        rs_div = DBOperation.SelectSQL(conn, sql, msg)
                        If rs_div Is Nothing Then
                            SetTextDelegate(MSG_ERROR_STRING & "SendDivControl_Click" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)

                            conn.Close()
                            conn = Nothing
                            Exit Sub
                        End If

                        div_string = ""
                        order_num += rs_div.RecordCount.ToString
                        str2 = ""
                        While rs_div.EOF = False
                            control_method = Trim(rs_div.Fields("mod").Value)
                            gonglv_value = Com_inf.Dec_to_Hex(Trim(rs_div.Fields("gonglv").Value), 2)
                            diangan_value = m_controllampobj.Find_diangan_id((rs_div.Fields("diangan").Value))

                            If control_method = "回路开" Then
                                control_method = "单灯开"
                                '如果是单灯开，只有有单灯，该命令才起作用
                                If rs.Fields("lamp_id").Value Is System.DBNull.Value Then
                                    str2 = ""
                                    order_num -= 1
                                    rs_div.MoveNext()
                                    Continue While
                                End If
                            Else
                                If control_method = "回路关" Then
                                    control_method = "单灯闭"
                                    '如果是单灯开，只有有单灯，该命令才起作用
                                    If rs.Fields("lamp_id").Value Is System.DBNull.Value Then
                                        str2 = ""
                                        order_num -= 1
                                        rs_div.MoveNext()
                                        Continue While
                                    End If
                                End If
                            End If
                            '单灯遇到整体命令也不执行
                            If rs.Fields("lamp_id").Value IsNot System.DBNull.Value And control_method <> "单灯闭" And control_method <> "单灯开" Then
                                str2 = ""
                                order_num -= 1
                                rs_div.MoveNext()
                                Continue While
                            End If

                            time = Com_inf.Dec_to_Hex(Val(rs_div.Fields("hour_beg").Value), 2) & " " & _
                                         Com_inf.Dec_to_Hex(Val(rs_div.Fields("min_beg").Value), 2) & " " & _
                                         Com_inf.Dec_to_Hex(Val(rs_div.Fields("second_beg").Value), 2)  '日期部分

                            str2 = time  '特殊时段控制的时间

                            sql = "select control_id from control_method where control_inf='" & control_method & "'"
                            rs_mod = DBOperation.SelectSQL(conn, sql, msg)
                            If rs_mod Is Nothing Then
                                SetTextDelegate(MSG_ERROR_STRING & "SendDivControl_Click" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)

                                conn.Close()
                                conn = Nothing
                                Exit Sub
                            End If
                            control_id = ""
                            str1 = Com_inf.Dec_to_Hex(control_box_id, 4)
                            str1 = Mid(str1, 1, 2) & " " & Mid(str1, 3, 2)  '电控箱的的十六位编号
                            If rs_mod.RecordCount > 0 Then
                                '老的控制方式，7个字节，最后一个字节补0凑8个字节
                                control_id = Trim(rs_mod.Fields("control_id").Value).ToUpper
                                If rs.Fields("lamp_id").Value Is System.DBNull.Value Then
                                    lamp_id_bin = Com_inf.Dec_to_Bin(rs.Fields("type_id").Value, 5) & Com_inf.Dec_to_Bin(1, 11)
                                    lamp_id_hex = Com_inf.BIN_to_HEX(lamp_id_bin)  '灯的二进制编码
                                    While lamp_id_hex.Length < 4
                                        lamp_id_hex = "0" & lamp_id_hex
                                    End While
                                    str2 &= " " & str1 & " " & Mid(lamp_id_hex, 1, 2) & " " & Mid(lamp_id_hex, 3, 2) & " " & control_id & " " '共3个控制时段，每个时段的格式为：hour+min+second+mod
                                Else
                                    '  str1 = Trim(rs.Fields("lamp_id").Value)
                                    lamp_id_bin = Com_inf.Dec_to_Bin(Mid(Trim(rs.Fields("lamp_id").Value), 5, 2), 5) & Com_inf.Dec_to_Bin(Mid(Trim(rs.Fields("lamp_id").Value), 7, LAMP_ID_LEN), 11) '十六位长度的终端编号二进制
                                    lamp_id_hex = Com_inf.BIN_to_HEX(lamp_id_bin)
                                    While lamp_id_hex.Length < 4
                                        lamp_id_hex = "0" & lamp_id_hex
                                    End While
                                    str2 &= " " & str1 & " " & Mid(lamp_id_hex, 1, 2) & " " & Mid(lamp_id_hex, 3, 2) & " " & control_id & " " '共3个控制时段，每个时段的格式为：hour+min+second+mod
                                End If
                                '功率按照实际功率
                                If control_box_type = 1 Then
                                    str2 &= diangan_value & " " & gonglv_value & " "
                                Else
                                    str2 &= diangan_value & " " & gonglv_value & " 00 "
                                End If
                            Else
                                '新的控制方式，8个字节
                                sql = "select grouporder from group_order where grouporder_name='" & control_method & "'"
                                rs_mod = DBOperation.SelectSQL(conn, sql, msg)
                                If rs_mod Is Nothing Then
                                    SetTextDelegate(MSG_ERROR_STRING & "SendDivControl_Click" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)
                                    conn.Close()
                                    conn = Nothing
                                    Exit Sub
                                End If
                                control_id = ""
                                If rs_mod.RecordCount > 0 Then
                                    control_id = Trim(rs_mod.Fields("grouporder").Value)
                                    str2 &= " " & str1 & " 00 01 " & control_id & " 64 " '共3个控制时段，每个时段的格式为：hour+min+second+mod
                                Else
                                    GoTo next1
                                End If
                            End If
                            div_string &= str2
next1:
                            rs_div.MoveNext()
                        End While
                        sendString &= div_string
                        rs.MoveNext()
                    End While
                End If
                '*********************************************************************************************
                If holitag = True Then  '节假日控制
                    sql = "select div_time_level, control_box_id, lamp_id , type_id from Special_road_level where control_box_id='" & control_box_id & "'"

                    rs = DBOperation.SelectSQL(conn, sql, msg)
                    If rs Is Nothing Then
                        SetTextDelegate(MSG_ERROR_STRING & "SendDivControl_Click" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)
                        conn.Close()
                        conn = Nothing
                        Exit Sub
                    End If
                    While rs.EOF = False
                        div_level = Trim(rs.Fields("div_time_level").Value)

                        '查询时段控制的时间
                        sql = "select * from Special_div_time where name='" & div_level & "'"
                        rs_div = DBOperation.SelectSQL(conn, sql, msg)
                        If rs_div Is Nothing Then
                            SetTextDelegate(MSG_ERROR_STRING & "SendDivControl_Click" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)
                            conn.Close()
                            conn = Nothing
                            Exit Sub
                        End If
                        div_string = ""
                        order_num += rs_div.RecordCount.ToString
                        str2 = ""
                        While rs_div.EOF = False
                            If System.Convert.ToDateTime(rs_div.Fields("time").Value).Date <> nowtime.Date Then
                                str2 = ""
                                order_num -= 1
                                rs_div.MoveNext()
                                Continue While
                            End If
                            control_method = Trim(rs_div.Fields("mod").Value)
                            gonglv_value = Com_inf.Dec_to_Hex(Trim(rs_div.Fields("gonglv").Value), 2)
                            diangan_value = m_controllampobj.Find_diangan_id((rs_div.Fields("diangan").Value))
                            If control_method = "回路开" Then
                                control_method = "单灯开"
                                '如果是单灯开，只有有单灯，该命令才起作用
                                If rs.Fields("lamp_id").Value Is System.DBNull.Value Then
                                    str2 = ""
                                    order_num -= 1
                                    rs_div.MoveNext()
                                    Continue While
                                End If
                            Else
                                If control_method = "回路关" Then
                                    control_method = "单灯闭"
                                    '如果是单灯开，只有有单灯，该命令才起作用
                                    If rs.Fields("lamp_id").Value Is System.DBNull.Value Then
                                        str2 = ""
                                        order_num -= 1
                                        rs_div.MoveNext()
                                        Continue While
                                    End If
                                End If
                            End If
                            '单灯遇到整体命令也不执行
                            If rs.Fields("lamp_id").Value IsNot System.DBNull.Value And control_method <> "单灯闭" And control_method <> "单灯开" Then
                                str2 = ""
                                order_num -= 1
                                rs_div.MoveNext()
                                Continue While
                            End If

                            sql = "select control_id from control_method where control_inf='" & control_method & "'"
                            rs_mod = DBOperation.SelectSQL(conn, sql, msg)
                            If rs_mod Is Nothing Then
                                SetTextDelegate(MSG_ERROR_STRING & "SendDivControl_Click" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)

                                conn.Close()
                                conn = Nothing
                                Exit Sub
                            End If
                            control_id = ""

                            pianyitime = System.Convert.ToDateTime(rs_div.Fields("time").Value) '特殊节日的控制时间
                            If pianyitime.Year = nowtime.Year And pianyitime.Month = nowtime.Month And pianyitime.Day = nowtime.Day Then
                                '具有当前的节假日控制模式
                                time = Com_inf.Dec_to_Hex(Val(pianyitime.Hour.ToString), 2) & " " & _
                                                  Com_inf.Dec_to_Hex(Val(pianyitime.Minute.ToString), 2) & " " & _
                                                  Com_inf.Dec_to_Hex(Val(pianyitime.Second.ToString), 2)  '日期部分

                                str2 = time  '节假日时段控制的时间
                                str1 = Com_inf.Dec_to_Hex(control_box_id, 4)
                                str1 = Mid(str1, 1, 2) & " " & Mid(str1, 3, 2)  '电控箱的的十六位编号
                                str2 &= " " & str1
                                If rs_mod.RecordCount > 0 Then
                                    control_id = Trim(rs_mod.Fields("control_id").Value).ToUpper

                                    If rs.Fields("lamp_id").Value Is System.DBNull.Value Then
                                        lamp_id_bin = Com_inf.Dec_to_Bin(rs.Fields("type_id").Value, 5) & Com_inf.Dec_to_Bin(1, 11)
                                        lamp_id_hex = Com_inf.BIN_to_HEX(lamp_id_bin)  '灯的二进制编码
                                        While lamp_id_hex.Length < 4
                                            lamp_id_hex = "0" & lamp_id_hex
                                        End While
                                        str2 &= " " & Mid(lamp_id_hex, 1, 2) & " " & Mid(lamp_id_hex, 3, 2) & " " & control_id & " " '共3个控制时段，每个时段的格式为：hour+min+second+mod
                                    Else

                                        lamp_id_bin = Com_inf.Dec_to_Bin(Mid(Trim(rs.Fields("lamp_id").Value), 5, 2), 5) & Com_inf.Dec_to_Bin(Mid(Trim(rs.Fields("lamp_id").Value), 7, LAMP_ID_LEN), 11) '十六位长度的终端编号二进制
                                        lamp_id_hex = Com_inf.BIN_to_HEX(lamp_id_bin)
                                        While lamp_id_hex.Length < 4
                                            lamp_id_hex = "0" & lamp_id_hex
                                        End While
                                        str2 &= " " & Mid(lamp_id_hex, 1, 2) & " " & Mid(lamp_id_hex, 3, 2) & " " & control_id & " " '共3个控制时段，每个时段的格式为：hour+min+second+mod

                                    End If
                                    '功率按照实际功率
                                    If control_box_type = 1 Then
                                        str2 &= diangan_value & " " & gonglv_value & " "
                                    Else
                                        str2 &= diangan_value & " " & gonglv_value & " 00 "
                                    End If


                                Else
                                    '新的控制方式，8个字节
                                    sql = "select grouporder from group_order where grouporder_name='" & control_method & "'"
                                    rs_mod = DBOperation.SelectSQL(conn, sql, msg)
                                    If rs_mod Is Nothing Then
                                        SetTextDelegate(MSG_ERROR_STRING & "SendDivControl_Click" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)

                                        conn.Close()
                                        conn = Nothing
                                        Exit Sub
                                    End If
                                    control_id = ""
                                    If rs_mod.RecordCount > 0 Then
                                        control_id = Trim(rs_mod.Fields("grouporder").Value)
                                        str2 &= " 00 01 " & control_id & " 64 " '共3个控制时段，每个时段的格式为：hour+min+second+mod

                                    Else
                                        GoTo next2
                                    End If

                                End If

                                div_string &= str2
                            End If
next2:
                            rs_div.MoveNext()


                        End While
                        sendString &= div_string
                        rs.MoveNext()
                    End While

                End If

                If order_num > 20 Then
                    MsgBox(Val(Trim(rs_boxorder.Fields("control_box_id").Value)) & "号主控箱下行命令超过20条，请重新设置控制模式", , PROJECT_TITLE_STRING)
                Else
                    If order_num = 0 Then
                        '如果时控配置为0，则需要更新control_box_state表中时段

                        Com_inf.insert_state("00", control_box_name)

                    End If
                    If order_num > 0 And order_num <= 20 Then  '有该电控箱的时段信息
                        boxid_hex = Com_inf.Dec_to_Hex(control_box_id, 4)
                        boxid_hex = Mid(boxid_hex, 1, 2) & " " & Mid(boxid_hex, 3, 2)
                        '模式下放成功后将当前的下放字符串记录到control_box_state表中目的是可以和从主控箱中上穿的数据匹配
                        '2011年12月26日增加
                        sendString = Com_inf.Dec_to_Hex(order_num.ToString, 2) & " " & sendString

                        Com_inf.insert_state(Trim(sendString), control_box_name)
                        sendString = boxid_hex & " " & sendString

                        nowtime2 = Now
                        'sql = "insert into TimeControl (RoadIMEI, CMDType, CMDContent, HandlerFlag, CreateTime) values ('" & IMEI & "', '" & cmdType & "', '" & Trim(sendString) & "' ,0, '" & nowtime2 & "')"
                        'DBOperation.ExecuteSQL(conn, sql, msg)  '增加一条短信
                        Insert_db_config(control_box_id, IMEI, Trim(sendString), cmdType)


                        box_imei.IMEI = IMEI
                        box_imei.control_box_id = control_box_id
                        box_imei.control_box_name = control_box_name
                        'control_list.Add(box_imei)  '将IMEI号增加到列表中

                        '发送数据后，等待返回状态
                        SetTextDelegate(control_box_name & "控制模式下放......" & vbCrLf, True, rtb_info_list)

                        System.Threading.Thread.Sleep(500)
                        Get_ConfigInf(HG_TYPE.HG_GET_DIVMOD_CONFIG, control_box_name, "控制模式", control_box_id, nowtime2)  '获取返回状态
                        ' GetModInf(IMEI, nowtime2, box_imei.control_box_name, control_box_id)
                    End If
                End If


                rs_box.MoveNext()   '每个主控箱
            End While

        End If

        '   MsgBox("时段控制模式下行成功", , PROJECT_TITLE_STRING)


        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        If rs_box.State = 1 Then
            rs_box.Close()
            rs_box = Nothing
        End If
        If rs_mod.State = 1 Then
            rs_mod.Close()
            rs_mod = Nothing
        End If
        If rs_div.State = 1 Then
            rs_div.Close()
            rs_div = Nothing
        End If
        If rs_boxorder.State = 1 Then
            rs_boxorder.Close()
            rs_boxorder = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    ''' 获取模式下放的回复命令，为兼容老版和新版监听2011年4月14日将配置成功判断统一为判断标志位
    ''' </summary>
    ''' <param name="IMEI"></param>
    ''' <param name="time" >模式下放的时间</param>
    ''' <remarks></remarks>
    Private Sub GetModInf(ByVal IMEI As String, ByVal time As DateTime, ByVal control_box_name As String, ByVal control_box_id As String)
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim wait_time As Integer = 1

        msg = ""
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If

        While wait_time < 10
            sql = "select * from TimeControl where RoadIMEI='" & IMEI & "' and CreateTime='" & time & "' and HandlerFlag=4"
            rs = DBOperation.SelectSQL(conn, sql, msg)
            If rs Is Nothing Then
                SetTextDelegate(MSG_ERROR_STRING & "GetModInf" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)

                conn.Close()
                conn = Nothing
                Exit Sub
            End If
            If rs.RecordCount > 0 Then
                SetTextDelegate(control_box_name & "控制模式下放成功" & "   时间：" & Now & vbCrLf & "********************" & vbCrLf & vbCrLf, True, rtb_info_list)
                '将控制模式下放成功记入数据库
                Insert_configinf(control_box_name & "控制模式下放成功", 1, control_box_id, control_box_name)

                Exit While
            Else
                System.Threading.Thread.Sleep(1000)
            End If

            wait_time += 1
        End While

        If wait_time >= 10 Then
            SetTextDelegate(control_box_name & "控制模式下放超时" & "   时间：" & Now & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)
            '将控制模式下放成功记入数据库
            Insert_configinf(control_box_name & "控制模式下放超时", 0, control_box_id, control_box_name)

        End If

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub


    Private Sub clear_text_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles clear_text.Click
        rtb_info_list.Text = ""
    End Sub
    ''' <summary>
    ''' 时间下放，按电控箱一次下放一个电控箱然后等待确认
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BackgroundWorkerTimeXiafang_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorkerTimeXiafang.DoWork
        Dim time As System.DateTime
        Dim hour As String
        Dim min As String
        Dim second As String
        Dim sendString As String
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim rs, rs_time As New ADODB.Recordset
        Dim waittime As Integer = CONTROL_WAIT_TIME '等待时间
        Dim IMEI As String
        Dim control_box_id As String '主控箱编号
        Dim control_box_name As String '主控箱名称
        Dim nowtime As DateTime = Now '当前的时间
        Dim boxid_hex As String '主控箱编号


        msg = ""
        '2011年5月25日增加按主控箱的时间校时
        If g_mod_controlboxname = "" Then
            sql = "select distinct(IMEI),control_box_name,control_box_id,control_box_type from Box_IMEI"
        Else
            sql = "select distinct(IMEI),control_box_name,control_box_id,control_box_type from Box_IMEI where control_box_name='" & g_mod_controlboxname & "'"
        End If

        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            SetTextDelegate(MSG_ERROR_STRING & "BackgroundWorkerTimeXiafang_DoWork" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs.RecordCount <= 0 Then
            '表示没有下放数据
            SetTextDelegate("没有下放数据，请重新设置" & vbCrLf, False, rtb_info_list)
        Else
            While rs.EOF = False
                '插入一条校时数据
                time = Now
                hour = Com_inf.Dec_to_Hex(time.Hour, 2)
                min = Com_inf.Dec_to_Hex(time.Minute, 2)
                second = Com_inf.Dec_to_Hex(time.Second, 2)
                sendString = hour & " " & min & " " & second
                IMEI = Trim(rs.Fields("IMEI").Value)
                control_box_id = Trim(rs.Fields("control_box_id").Value)
                boxid_hex = Com_inf.Dec_to_Hex(control_box_id, 4)
                boxid_hex = Mid(boxid_hex, 1, 2) & " " & Mid(boxid_hex, 3, 2)
                sendString = boxid_hex & " " & sendString
                control_box_name = Trim(rs.Fields("control_box_name").Value)
                'sql = "insert into TimeControl(RoadIMEI, CMDType, CMDContent, HandlerFlag, Createtime) values('" & IMEI & "', '" & HG_TYPE.HG_SET_TIME_CONFIG & "', '" & sendString & "', 0, '" & time & "')"
                'DBOperation.ExecuteSQL(conn, sql, msg)
                '将配置命令下放到TimeControl表中
                Insert_db_config(control_box_id, IMEI, sendString, HG_TYPE.HG_SET_TIME_CONFIG)
                SetTextDelegate(control_box_name & "校时下放： " & time.Hour & ":" & time.Minute & ":" & time.Second & vbCrLf, True, rtb_info_list)
                System.Threading.Thread.Sleep(2000) '休息2秒钟后查询返回状态
                '查询返回状态
                Get_ConfigInf(HG_TYPE.HG_GET_TIME_CONFIG, control_box_name, "校时", control_box_id, nowtime)  '获取返回状态
                '    While waittime > 0
                '        sql = "select * from TimeControl where RoadIMEI='" & IMEI & "' and Createtime='" & time & "'  and HandlerFlag=4 order by id desc"
                '        rs_time = DBOperation.SelectSQL(conn, sql, msg)
                '        If rs_time Is Nothing Then
                '            SetTextDelegate(MSG_ERROR_STRING & "BackgroundWorkerTimeXiafang_DoWork" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)

                '            If rs.State = 1 Then
                '                rs.Close()
                '                rs = Nothing
                '            End If
                '            conn.Close()
                '            conn = Nothing
                '            Exit Sub
                '        End If

                '        If rs_time.RecordCount > 0 Then
                '            SetTextDelegate(control_box_name & "校时成功" & Now.Hour & ":" & Now.Minute & ":" & Now.Second & vbCrLf & "********************" & vbCrLf & vbCrLf, True, rtb_info_list)
                '            '校时成功后将配置成功信息放置到数据库中
                '            Insert_configinf(control_box_name & "校时成功", 1, control_box_id, control_box_name)
                '            Exit While
                '        End If
                '        System.Threading.Thread.Sleep(1000)
                '        waittime -= 1
                '    End While
                '    If waittime = 0 Then
                '        SetTextDelegate(control_box_name & "校时超时" & Now.Hour & ":" & Now.Minute & ":" & Now.Second & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)
                '        '校时超时后将报警信息放置到数据库中
                '        Insert_configinf(control_box_name & "校时超时", 0, control_box_id, control_box_name)
                '    End If
                '    waittime = CONTROL_WAIT_TIME
                rs.MoveNext()
            End While
        End If
        If rs.State Then
            rs.Close()
            rs = Nothing
        End If
        If rs_time.State = 1 Then
            rs_time.Close()
            rs_time = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    ''' 将下放的命令写入到TimeControl表中
    ''' </summary>
    ''' <param name="control_box_id">主控箱编号</param>
    ''' <param name="IMEI">IMEI</param>
    ''' <param name="config_string">配置命令</param>
    ''' <param name="order_type" >命令的类型</param>
    ''' <remarks></remarks>
    Private Sub Insert_db_config(ByVal control_box_id As String, ByVal IMEI As String, ByVal config_string As String, ByVal order_type As String)
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim flag As Integer = 1 '主控箱类型的标志
        msg = ""
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        sql = "select control_box_type from control_box where control_box_id='" & control_box_id & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            SetTextDelegate(MSG_ERROR_STRING & "Insert_db_config" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs.RecordCount > 0 Then
            If rs.Fields("control_box_type").Value Is System.DBNull.Value Then
                flag = 1
            Else
                flag = Val(Trim(rs.Fields("control_box_type").Value))
            End If
        Else
            flag = 1
        End If
        If flag = 1 Then  '1类型
            sql = "insert into TimeControl(RoadIMEI, CMDType, CMDContent, HandlerFlag, Createtime) values('" & IMEI & "', '" & order_type & "', '" & Trim(config_string) & "', '" & CONTROL_BOX_TYPE1_FLAG & "', '" & Now & "')"
        Else
            sql = "insert into TimeControl(RoadIMEI, CMDType, CMDContent, HandlerFlag, Createtime) values('" & IMEI & "', '" & order_type & "', '" & Trim(config_string) & "', '" & CONTROL_BOX_TYPE2_FLAG & "', '" & Now & "')"
        End If
        DBOperation.ExecuteSQL(conn, sql, msg)
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    ''' 将配置信息存储到数据库中
    ''' </summary>
    ''' <param name="state_inf">配置的反馈信息</param>
    ''' <param name="state_flag">成功还是失败的标志，0表示失败，1表示成功</param>
    ''' <param name="control_box_id" >电控箱编号</param>
    ''' <param name="control_box_name" >电控箱名称</param>
    ''' <remarks></remarks>
    Public Sub Insert_configinf(ByVal state_inf As String, ByVal state_flag As Integer, ByVal control_box_id As String, ByVal control_box_name As String)
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        msg = ""
        '判断状态的长度，如果超过150则截取字符串
        If state_inf.Length > 150 Then
            state_inf = Mid(state_inf, 1, 150)
        End If
        sql = "insert into config_state_list (control_box_id, control_box_name, config_state,state_flag,createtime)" _
        & "values('" & control_box_id & "','" & control_box_name & "','" & state_inf & "' ,'" & state_flag & "','" & Now & "')"
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        DBOperation.ExecuteSQL(conn, sql, msg)
        conn.Close()
        conn = Nothing
    End Sub

    Private Sub BackgroundWorkerTimeXiafang_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorkerTimeXiafang.RunWorkerCompleted
        get_boxprobleminf()  '刷新故障信息
    End Sub

    Private Sub BackgroundWorkerModXiaFang_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorkerModXiaFang.RunWorkerCompleted
        g_clearmod = False
        get_boxprobleminf()  '刷新故障信息
    End Sub

    'Private Sub 解除报警ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If MsgBox("是否解除勾选的开关量报警?", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.Yes Then
    '        Dim i As Integer = 0
    '        Dim conn As New ADODB.Connection
    '        Dim sql As String
    '        Dim msg As String
    '        msg = ""
    '        If DBOperation.OpenConn(conn) = False Then
    '            Exit Sub
    '        End If
    '        While i < dgv_kaiguan_alarmlist.Rows.Count
    '            If dgv_kaiguan_alarmlist.Rows(i).Cells("checkid").Value = 1 Then
    '                sql = "update kaiguan_alarm_list set alarm_tag=1 where id='" & dgv_kaiguan_alarmlist.Rows(i).Cells("KGid").Value & "'"
    '                DBOperation.ExecuteSQL(conn, sql, msg)

    '            End If
    '            i += 1
    '        End While

    '        conn.Close()
    '        conn = Nothing
    '    End If
    '    Me.Kaiguan_alarm_listTableAdapter.Fill(Me.KaiguanalarmDataSet.kaiguan_alarm_list)

    'End Sub

    ''' <summary>
    ''' 发送测量板个数，2011年7月19日将旧版的主控箱剔除
    ''' 2011年10月24日发送测量板个数增加电压上下限和电流上下限
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub sendAlarmdata()
        Dim rs, rs_huilu As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim imei As String
        Dim boardnum As Integer
        Dim nowtime As DateTime = Now
        Dim control_box_name As String
        Dim control_box_id As String
        Dim box_type As Integer '主控箱版本
        Dim config_string As String '配置主控箱的字符串
        Dim presure_bottom, presure_top As String '电压，电流
        Dim boxid_string As String '路段号的十六进制
        Dim delaytime As String  '自动上传的延时时间
        Dim i As Integer = 0
        Dim huilu_id, jiechuqi_id, current_alarmtop, current_alarmbot, presure_type, bianbi As Integer
        Dim str, str1 As String
        delaytime = get_alarmdelaytime()
        msg = ""
        If g_mod_controlboxname = "" Then
            'sql = "select * from control_box order by control_box_id"
            sql = "select control_box_type,board_num,IMEI,control_box_name, control_box_id from Box_IMEI order by control_box_id"
        Else
            sql = "select control_box_type,board_num,IMEI,control_box_name, control_box_id from Box_IMEI where control_box_name='" & g_mod_controlboxname & "' order by control_box_id"
            ' sql = "select * from control_box where control_box_name='" & g_mod_controlboxname & "' order by control_box_id "
        End If
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            SetTextDelegate(MSG_ERROR_STRING & "sendTestBoardNum" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If

        While rs.EOF = False
            imei = Trim(rs.Fields("IMEI").Value)
            control_box_name = Trim(rs.Fields("control_box_name").Value)
            control_box_id = Trim(rs.Fields("control_box_id").Value)
            boxid_string = Com_inf.Dec_to_Hex(control_box_id, 4)
            boxid_string = Mid(boxid_string, 1, 2) & " " & Mid(boxid_string, 3, 2)

            box_type = rs.Fields("control_box_type").Value
            If box_type = 1 Then
                rs.MoveNext()
                Continue While
            End If
            If rs.Fields("board_num").Value Is System.DBNull.Value Then
                boardnum = 1
            Else
                boardnum = rs.Fields("board_num").Value
            End If
            '路段号
            config_string = boxid_string
            '电压上下限
            presure_top = Com_inf.Get_EXEPresure(Me.m_controlboxobj.m_presurebottomvalue, control_box_id)  '电压上限
            presure_bottom = Com_inf.Get_EXEPresure(Me.m_controlboxobj.m_presuretopvalue, control_box_id)  '电压下限
            presure_top = Com_inf.Dec_to_Hex(presure_top, 4)
            presure_top = Mid(presure_top, 1, 2) & " " & Mid(presure_top, 3, 2)
            presure_bottom = Com_inf.Dec_to_Hex(presure_bottom, 4)
            presure_bottom = Mid(presure_bottom, 1, 2) & " " & Mid(presure_bottom, 3, 2)
            config_string = config_string & " " & presure_bottom & " " & presure_top
            '延时时间+测量板个数
            delaytime = Com_inf.Dec_to_Hex(delaytime, 4)
            delaytime = Mid(delaytime, 1, 2) & " " & Mid(delaytime, 3, 2)
            config_string = config_string & " " & delaytime & " " & Com_inf.Dec_to_Hex(boardnum, 2)
            '通过主控箱名称获取回路配置信息
            sql = "select * from huilu_inf where control_box_name='" & control_box_name & "' order by huilu_id"
            rs_huilu = DBOperation.SelectSQL(conn, sql, msg)
            If rs_huilu Is Nothing Then
                rs.Close()
                rs = Nothing
                conn.Close()
                conn = Nothing
                Exit Sub
            End If
            If rs_huilu.RecordCount > 0 Then
                config_string = config_string & " " & Com_inf.Dec_to_Hex(rs_huilu.RecordCount.ToString, 2)
                While rs_huilu.EOF = False
                    huilu_id = rs_huilu.Fields("huilu_id").Value
                    jiechuqi_id = rs_huilu.Fields("jiechuqi_id").Value
                    presure_type = rs_huilu.Fields("presure_type").Value
                    current_alarmtop = rs_huilu.Fields("current_alarmtop").Value
                    current_alarmbot = rs_huilu.Fields("current_alarmbot").Value
                    bianbi = rs_huilu.Fields("bianbi").Value

                    current_alarmtop = Get_EXECurrent(current_alarmtop, control_box_id, bianbi)
                    current_alarmbot = Get_EXECurrent(current_alarmbot, control_box_id, bianbi)
                    str = Com_inf.Dec_to_Hex(huilu_id.ToString, 2)
                    str1 = Com_inf.BIN_to_HEX(Com_inf.Dec_to_Bin(presure_type, 2) & Com_inf.Dec_to_Bin(jiechuqi_id, 3) & Com_inf.Dec_to_Bin(jiechuqi_kaiguan(jiechuqi_id, control_box_name), 3))
                    While str1.Length < 2
                        str1 = "0" & str1
                    End While
                    str = str & " " & str1
                    str1 = Com_inf.Dec_to_Hex(current_alarmtop, 4)
                    str = str & " " & Mid(str1, 1, 2) & " " & Mid(str1, 3, 2)
                    str1 = Com_inf.Dec_to_Hex(current_alarmbot, 4)
                    str = str & " " & Mid(str1, 1, 2) & " " & Mid(str1, 3, 2)
                    config_string = config_string & " " & str
                    rs_huilu.MoveNext()
                End While
            Else
                config_string = config_string & " 00"
            End If
            'sql = "insert into TimeControl(RoadIMEI, CMDType, CMDContent, HandlerFlag, Createtime) values('" & imei & "'," & HG_TYPE.HG_SET_TESTBOARDNUM & ",'" & boardnum & "',0,'" & nowtime & "')"
            'DBOperation.ExecuteSQL(conn, sql, msg)
            Insert_db_config(control_box_id, imei, Trim(config_string), HG_TYPE.HG_SET_TESTBOARDNUM)
            '发送数据后，等待返回状态
            SetTextDelegate(control_box_name & "配置主控箱报警阈值......" & vbCrLf, True, rtb_info_list)
            System.Threading.Thread.Sleep(500)
            Get_ConfigInf(HG_TYPE.HG_GET_TESTBOARDNUM, control_box_name, "主控箱报警阈值", control_box_id, nowtime)  '获取返回状态
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
    ''' 通过接触器获取开关量
    ''' </summary>
    ''' <param name="jiechuqi"></param>
    ''' <remarks></remarks>
    Private Function jiechuqi_kaiguan(ByVal jiechuqi As String, ByVal control_box_name As String) As Integer
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String
        Dim jiechuqi_name As String = "K" & jiechuqi
        msg = ""
        sql = "select kaiguan_tag from kaiguan_list where kaiguan_name ='" & jiechuqi_name & "'"
        If DBOperation.OpenConn(conn) = False Then
            jiechuqi_kaiguan = "00"
            Exit Function
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            jiechuqi_kaiguan = "00"
            conn.Close()
            conn = Nothing
            Exit Function
        End If
        If rs.RecordCount > 0 Then
            jiechuqi_kaiguan = Com_inf.Dec_to_Hex(rs.Fields("kaiguan_tag").Value.ToString, 2)
        Else
            jiechuqi_kaiguan = "00"
        End If
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Function

    Private Sub 三回路控制命令设置ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 三回路控制命令设置ToolStripMenuItem.Click
        Dim order_windowobj As New 三回路组合命令
        order_windowobj.ShowDialog()
    End Sub

    ''' <summary>
    ''' 双击电控箱的名称进行电控箱定位
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tv_box_inf_list_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles tv_box_inf_list.MouseDoubleClick
        If Me.tv_box_inf_list.SelectedNode.Level = 3 Then  '点击的树形列表为电控箱名称，则进行定位
            m_dgvviewtag = 0  '显示
            Me.pl_boxinf.Visible = True
            Dim control_box_name As String = Trim(tv_box_inf_list.SelectedNode.Text).Split(" ")(0)
            find_box_pos(control_box_name) '定位
            get_box_state(control_box_name)  '将主控箱的所有信息整理在一个列表中
        End If
    End Sub
    ''' <summary>
    ''' 右击电控箱的名称进行电控箱定位
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tv_box_inf_list_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles tv_box_inf_list.MouseDown
        If Me.tv_box_inf_list.SelectedNode.Level = 3 Then  '点击的树形列表为电控箱名称，则进行定位
            Dim control_box_name As String = Trim(tv_box_inf_list.SelectedNode.Text).Split(" ")(0)
            Dim lamp As Boolean
            lamp = Lamp_Flag(control_box_name)
            If lamp = True Then
                单灯召测ToolStripMenuItem1.Enabled = True
                单灯数据ToolStripMenuItem.Enabled = True
                历史数据查询ToolStripMenuItem.Enabled = True
                tv_box_inf_list.ContextMenuStrip = Me.ContextMenuStrip_controlboxcontrol
            Else
                单灯召测ToolStripMenuItem1.Enabled = False
                单灯数据ToolStripMenuItem.Enabled = False
                历史数据查询ToolStripMenuItem.Enabled = False
                tv_box_inf_list.ContextMenuStrip = Me.ContextMenuStrip_controlboxcontrol
            End If
        End If
    End Sub

    Private Sub pb_map_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles pb_map.MouseDown
        Dim control_box_name As String
        Dim msg As String = ""
        Dim sql As String = ""
        Dim box_id As String = ""
        Dim imei As String = ""
        Dim conn As New ADODB.Connection
        Dim rs As New ADODB.Recordset
        Dim lamp As Boolean
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        sql = "select * from control_box where  pos_x<= " & (m_rightbuttonpos.X - 1) & " and pos_x >= " & (m_rightbuttonpos.X - m_controllampobj.m_lampwide - 3) & " and pos_y <= " & (m_rightbuttonpos.Y + 1) & " and pos_y >= " & (m_rightbuttonpos.Y - m_controllampobj.m_lampheight - 1)
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            SetTextDelegate(MSG_ERROR_STRING & "打开pb_map_MouseDown" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)
            MsgBox("请选择主控箱重新尝试", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        Else
            If rs.RecordCount > 0 Then
                '找到某一盏灯的信息后发送单灯开命令
                control_box_name = Trim(rs.Fields("control_box_name").Value)
                lamp = Lamp_Flag(control_box_name)
            End If
        End If
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
        If lamp = True Then
            单灯召测ToolStripMenuItem2.Enabled = True
            单灯数据ToolStripMenuItem18.Enabled = True
            终端历史数据ToolStripMenuItem22.Enabled = True
            Me.pb_map.ContextMenuStrip = Me.ContextMenuStrip_lampcontroler
        Else
            单灯召测ToolStripMenuItem2.Enabled = False
            单灯数据ToolStripMenuItem18.Enabled = False
            终端历史数据ToolStripMenuItem22.Enabled = False
            Me.pb_map.ContextMenuStrip = Me.ContextMenuStrip_lampcontroler
        End If

    End Sub

    Private Sub 单灯数据ToolStripMenuItem_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles 单灯数据ToolStripMenuItem.MouseDown
        Dim control_box_name As String = Trim(tv_box_inf_list.SelectedNode.Text).Split(" ")(0)
        get_lamp_dandeng(control_box_name)
    End Sub

    ''' <summary>
    ''' 检查是否有单灯故障，有就显示报警栏，没有就不显示
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub get_lamp_dandeng(ByVal control_box_name As String)
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String = ""
        Dim sql As String = ""
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        sql = "select B.* from control_box A LEFT JOIN lamp_inf B ON A.control_box_id=B.control_box_id  where A.control_box_name='" & control_box_name & "'  AND B.lamp_type_id<>'31'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs.RecordCount > 0 Then
            单灯数据.SetControlBoxListDelegate(control_box_name)
            单灯数据.ShowDialog()
        End If
    End Sub


    ''' <summary>
    ''' 检查是否有单灯故障，有就显示报警栏，没有就不显示
    ''' </summary>
    ''' <remarks></remarks>
    Public Function Lamp_Flag(ByVal control_box_name As String) As Boolean
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String = ""
        Dim sql As String = ""
        If DBOperation.OpenConn(conn) = False Then
            Exit Function
        End If
        sql = "select B.* from control_box A LEFT JOIN lamp_inf B ON A.control_box_id=B.control_box_id  where A.control_box_name='" & control_box_name & "'  AND B.lamp_type_id<>'31'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            conn.Close()
            conn = Nothing
            Exit Function
        End If
        If rs.RecordCount > 0 Then
            Lamp_Flag = True
        Else
            Lamp_Flag = False
        End If
    End Function

    ''' <summary>
    ''' 获取当前主控箱的所有信息
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub get_box_state(ByVal control_box_name As String)
        Dim rs As New ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim control_lamp_obj As New control_lamp
        Dim conn As New ADODB.Connection
        Dim state() As String   '主控箱的状态
        Dim i As Integer
        Dim j As Integer = 1
        Dim huilu_num As Integer  '回路的数量
        Dim board_id As Integer = 0 '测量板编号
        Dim state_string As String '三个测量板的状态
        Dim box_type As Integer '主控箱的类型
        Dim problem_string As String '故障
        Dim board_num As Integer
        Dim jiechuqi_id, jiechuqi_state As String '接触器编号
        Dim huilu_string As String '回路返回值
        Dim K_state '记录某个主控箱下面的接触器的控制状态
        i = 1
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        dgv_box_inf.Rows.Clear() '清空列表
        msg = ""
        sql = "select * from control_box where control_box_name ='" & control_box_name & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs.RecordCount > 0 Then
            rtb_control_box_inf.Text = "网关名称：" & control_box_name & vbCrLf
            box_type = rs.Fields("control_box_type").Value
            board_num = rs.Fields("board_num").Value
            problem_string = Trim(rs.Fields("state").Value)
            If problem_string <> "正常" Then
                problem_string = "工作状态：报警" & vbCrLf
            Else
                problem_string = "工作状态：正常" & vbCrLf
            End If
            rtb_control_box_inf.Text &= problem_string
            find_box_powerinf()
            rtb_control_box_inf.Text &= "数据采集时间：" & rs.Fields("Createtime").Value & vbCrLf & vbCrLf
            '2012年3月5号，增加6个接触器的开关状态
            K_state = Com_inf.get_jiechuqi_state(control_box_name)
            'If board_num = 1 Then
            '    problem_string = Trim(rs.Fields("StatusContent").Value).Split("+")(0) & vbCrLf
            'Else
            '    If board_num = 2 Then
            '        problem_string = "[1] " & Trim(rs.Fields("StatusContent").Value).Split("+")(0) & vbCrLf & _
            '        "[2] " & Trim(rs.Fields("StatusContent2").Value).Split("+")(0) & vbCrLf
            '    Else
            '        problem_string = "[1] " & Trim(rs.Fields("StatusContent").Value).Split("+")(0) & _
            '        vbCrLf & "[2] " & Trim(rs.Fields("StatusContent2").Value).Split("+")(0) & _
            '        vbCrLf & "[3] " & Trim(rs.Fields("StatusContent3").Value).Split("+")(0) & vbCrLf

            '    End If
            'End If
            rtb_control_box_inf.Text &= K_state
            While board_id < board_num
                If board_id = 0 Then
                    If rs.Fields("StatusContent").Value IsNot System.DBNull.Value Then
                        If Trim(rs.Fields("StatusContent").Value) <> "" Then
                            state_string = (Trim(rs.Fields("StatusContent").Value))
                            state = state_string.Split(" ")
                        Else
                            state_string = ""
                            state = state_string.Split(" ")
                        End If
                    Else
                        state_string = ""
                        state = state_string.Split(" ")
                    End If
                Else
                    If board_id = 1 Then
                        If rs.Fields("StatusContent2").Value IsNot System.DBNull.Value Then
                            If Trim(rs.Fields("StatusContent2").Value) <> "" Then
                                state_string = (Trim(rs.Fields("StatusContent2").Value))
                                state = state_string.Split(" ")
                            Else
                                state_string = ""
                                state = state_string.Split(" ")
                            End If
                        Else
                            state_string = ""
                            state = state_string.Split(" ")
                        End If
                    Else
                        If rs.Fields("StatusContent3").Value IsNot System.DBNull.Value Then
                            If Trim(rs.Fields("StatusContent3").Value) <> "" Then
                                state_string = (Trim(rs.Fields("StatusContent3").Value))
                                state = state_string.Split(" ")
                            Else
                                state_string = ""
                                state = state_string.Split(" ")
                            End If
                        Else
                            state_string = ""
                            state = state_string.Split(" ")
                        End If
                    End If
                End If
                If state_string = "" Then
                    board_id += 1
                    Continue While
                End If
                j = 0
                huilu_num = (state.Length - 3) / 3  '回路数目
                i = 3
                While j < huilu_num
                    'lamp_tip &= "第" & j & "回路  电流(A)：" & state(i) & "    "
                    dgv_box_inf.Rows.Add()
                    dgv_box_inf.Rows(j + huilu_num * board_id).Cells("data_name").Value = "回路" & j + huilu_num * board_id + 1
                    huilu_string = Com_inf.get_jiechuqi_id(j + huilu_num * board_id + 1, control_box_name)
                    If huilu_string <> "0" Then
                        jiechuqi_id = "K" & huilu_string.Split(" ")(0)
                        jiechuqi_state = huilu_string.Split(" ")(1)
                    Else
                        jiechuqi_id = "-"
                        jiechuqi_state = "-"
                    End If
                    dgv_box_inf.Rows(j + huilu_num * board_id).Cells("jiechuqi_id").Value = jiechuqi_id
                    dgv_box_inf.Rows(j + huilu_num * board_id).Cells("data_state").Value = jiechuqi_state
                    If huilu_num = 6 Then
                        If j < 2 Then
                            dgv_box_inf.Rows(j + huilu_num * board_id).Cells("presure_value").Value = state(0)
                        Else
                            If j < 4 Then
                                dgv_box_inf.Rows(j + huilu_num * board_id).Cells("presure_value").Value = state(1)
                            Else
                                dgv_box_inf.Rows(j + huilu_num * board_id).Cells("presure_value").Value = state(2)
                            End If
                        End If
                    Else
                        If j < 4 Then
                            dgv_box_inf.Rows(j + huilu_num * board_id).Cells("presure_value").Value = state(0)
                        Else
                            If j < 8 Then
                                dgv_box_inf.Rows(j + huilu_num * board_id).Cells("presure_value").Value = state(1)
                            Else
                                dgv_box_inf.Rows(j + huilu_num * board_id).Cells("presure_value").Value = state(2)
                            End If
                        End If
                    End If
                    dgv_box_inf.Rows(j + huilu_num * board_id).Cells("current_value").Value = state(i)
                    i += 1
                    dgv_box_inf.Rows(j + huilu_num * board_id).Cells("power_value").Value = state(i)
                    i += 1
                    dgv_box_inf.Rows(j + huilu_num * board_id).Cells("yinshu_value").Value = state(i)
                    i += 1
                    j += 1
                End While
                board_id += 1
            End While
        End If
finish:
        rs.Close()
        rs = Nothing
        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    ''' 召测三遥数据
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub sendsanyao_data()
        Dim imei As String
        Dim i As Integer = 0
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String = ""
        Dim msg As String = ""
        Dim tag As Boolean = False
        '   Dim id_hex As String
        Dim id_list As New ArrayList '所有主控箱编号的列表
        Dim boxname_list As New ArrayList '所有主控箱名称
        Dim controlboxobj As New control_box
        Dim waittime = 5  '开关量等待时间
        Dim controlboxid As String '主控箱编号
        Dim controlboxname As String  '主控箱名称
        Dim j As Integer = 0
        Dim now_time As DateTime = Now
        Dim boxid_hex As String '主控箱的16进制
        sql = "select imei,control_box_id,control_box_name from Box_IMEI order by control_box_id"
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING, , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        '对每个主控箱进行保存
        While rs.EOF = False
            If Me.BackgroundWorker_getboxdata.CancellationPending = False Then
                controlboxid = Trim(rs.Fields("control_box_id").Value)
                boxid_hex = Com_inf.Dec_to_Hex(controlboxid, 4)
                boxid_hex = Mid(boxid_hex, 1, 2) & " " & Mid(boxid_hex, 3, 2)

                controlboxname = Trim(rs.Fields("control_box_name").Value)
                If controlboxobj.get_communication(controlboxname) = False Then
                    m_controlboxname = controlboxname
                    '通信不正常则不测
                    Me.BackgroundWorker_getboxdata.ReportProgress(30)

                    System.Threading.Thread.Sleep(1000)
                    GoTo next1
                End If
                '发送三遥数据招测
                imei = Trim(rs.Fields("imei").Value)
                sql = "insert into TimeControl(RoadIMEI, CMDType, CMDContent, CreateTime,HandlerFlag) values('" & imei & "', '" & HG_TYPE.HG_SET_YAOCE & "','" & boxid_hex & "','" & Now & "','" & CONTROL_BOX_TYPE2_FLAG & "')"
                DBOperation.ExecuteSQL(conn, sql, msg)

                id_list.Add(controlboxid)  '将主控箱编号放入列表中
                boxname_list.Add(controlboxname) '主控箱名称
            End If
next1:
            rs.MoveNext()
        End While
        '发送完所有召测数据后，等待召测结果
        System.Threading.Thread.Sleep(1000)
        tag = Get_returnvalue(id_list, boxname_list, now_time, 0)
finish:

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub


    ''' <summary>
    ''' 获取招测的三遥数据
    ''' </summary>
    ''' <param name="boxid"></param>
    ''' <param name="time"></param>
    ''' <param name="sendtimes">招测的次数，第一次显示，第二次不显示，两次目的是使用户点击招测后有报警立刻报警</param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Private Function Get_returnvalue(ByVal boxid As ArrayList, ByVal boxname As ArrayList, ByVal time As DateTime, ByVal sendtimes As Integer) As Boolean

        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String = ""
        Dim msg As String = ""
        Dim recString As String '接收到的数据
        Dim data() As String
        Dim controlboxobj As New control_box
        Dim problem_tag As String = ""
        Dim row_count As Integer = 0
        Dim group_num As Integer = 0  '组号
        Dim sanyao_data(56) As String
        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim t As Integer = 0
        Dim datainf As Boolean = False
        Dim id_hex As String '主控箱编号的16进制表示
        Dim boxtype As String '主控箱类型区分大小三遥
        Get_returnvalue = False
        If DBOperation.OpenConn(conn) = False Then
            Exit Function
        End If
        m_waittime = g_ycwaittime
        While m_waittime > 0
            If Me.BackgroundWorker_getboxdata.CancellationPending = False Then
                Me.BackgroundWorker_getboxdata.ReportProgress(m_waittime)
                t = 0
                While t < boxid.Count
                    '根据主控箱编号查询主控箱的类型，区分大小三遥
                    boxtype = controlboxobj.get_controltype(boxid(t))

                    id_hex = Com_inf.Dec_to_Hex(boxid(t), 4)
                    id_hex = Mid(id_hex, 1, 2) & " " & Mid(id_hex, 3, 2)

                    If boxtype = 2 Then
                        sql = "select * from RoadLightStatus where PackType='" & HG_TYPE.HG_GET_YAOCE & "' and HandlerFlag=3 and Createtime>'" & time.AddMinutes(-10) & "' and StatusContent like'" & id_hex & "%' order by id desc"
                    Else
                        sql = "select * from RoadLightStatus where PackType='" & HG_TYPE.HG_GET_SMALLYAOCE & "' and HandlerFlag=3 and Createtime>'" & time.AddMinutes(-10) & "' and StatusContent like'" & id_hex & "%' order by id desc"
                    End If
                    rs = DBOperation.SelectSQL(conn, sql, msg)
                    If rs Is Nothing Then
                        MsgBox(MSG_ERROR_STRING, , PROJECT_TITLE_STRING)
                        conn.Close()
                        conn = Nothing
                        Exit Function
                    End If
                    If rs.RecordCount > 0 Then
                        recString = Trim(rs.Fields("StatusContent").Value)
                        data = recString.Split(" ")
                        If boxtype = 3 Then
                            '判断数据是否合法，如果数据合法则继续判读否则丢弃不判
                            datainf = controlboxobj.CheckData(data, rs.Fields("ID").Value)
                            If datainf = False Then
                                '证明上传的数据为超时数据，丢弃不管
                                Me.BackgroundWorker_getboxdata.ReportProgress(22)
                                GoTo finish
                            End If
                            '所获取的数据为小三遥数据，将数据中第三个字节的组数减1变为组号，调用通用的三遥数据分析
                            data(2) = (Val(data(2).ToString) - 1).ToString
                            If data(2) = -1 Then
                                '表示数据失败,将该状态置为1
                                rs.Fields("handlerflag").Value = 1
                                rs.Update()
                                GoTo next1
                            End If
                            controlboxobj.Get_Huilu_inf_small(0, data, problem_tag, boxid(t), rs.Fields("ID").Value)

                            Get_returnvalue = True
                            'Exit While
                        End If
                        If boxtype = 2 Then
                            '传统三遥数据
                            group_num = Val(data(2))
                            If group_num = 0 Then
                                '表示数据失败,将该状态置为1
                                rs.Fields("handlerflag").Value = 1
                                rs.Update()
                                GoTo next1
                            End If
                            j = 0
                            ReDim sanyao_data(56)
                            While j < group_num
                                sanyao_data(0) = data(0)
                                sanyao_data(1) = data(1)
                                sanyao_data(2) = j
                                i = 3
                                While i < 57
                                    sanyao_data(i) = data(i + (j) * 54)
                                    i += 1
                                End While
                                datainf = controlboxobj.CheckData(sanyao_data, rs.Fields("ID").Value)
                                If datainf = False Then
                                    '证明上传的数据为超时数据，丢弃不管
                                    j += 1
                                    Continue While
                                End If
                                controlboxobj.Get_Huilu_inf(j, group_num, sanyao_data, problem_tag, boxid(t))
                                Get_returnvalue = True
                                j += 1
                            End While
                            '读取故障信息
                            controlboxobj.get_alarminf(data, 3 + group_num * 54, rs.Fields("ID").Value, boxid(t), group_num)
                        End If
                        '有上传数据，数据处理完毕后将该主控箱的编号移去
next1:
                        boxid.RemoveAt(t)
                        boxname.RemoveAt(t)
                    Else
                        t += 1
                    End If
                End While
                If t = 0 Then  '表示说有的数据全部召测完毕
                    Exit While
                End If
                m_waittime -= 1
                System.Threading.Thread.Sleep(1000)
            Else
                Exit While
            End If
        End While
        '查询超时，不正常
        If m_waittime <= 0 Then
            If sendtimes = 0 Then
                t = 0
                While t < boxname.Count
                    '超时
                    m_controlboxname = boxname(t)
                    Me.BackgroundWorker_getboxdata.ReportProgress(31)
                    System.Threading.Thread.Sleep(500)
                    t += 1
                End While
            End If
        End If
finish:
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Function

    ''' <summary>
    ''' 召测开关量数据
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub sendkaiguan_data()
        Dim imei As String
        Dim i As Integer = 0
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String = ""
        Dim msg As String = ""
        Dim tag As Boolean = False
        '   Dim id_hex As String
        Dim id_list As New ArrayList '所有主控箱编号的列表
        Dim boxname_list As New ArrayList '所有主控箱名称
        Dim controlboxobj As New control_box
        Dim waittime = 5  '开关量等待时间
        Dim controlboxid As String '主控箱编号
        Dim controlboxname As String  '主控箱名称
        Dim j As Integer = 0
        Dim now_time As DateTime = Now
        Dim boxid_hex As String
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        sql = "select imei,control_box_id,control_box_name from Box_IMEI order by control_box_id"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING, , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        '对每个主控箱进行保存
        While rs.EOF = False
            If Me.BackgroundWorker_getboxdata.CancellationPending = False Then
                controlboxid = Trim(rs.Fields("control_box_id").Value)
                boxid_hex = Com_inf.Dec_to_Hex(controlboxid, 4)
                boxid_hex = Mid(boxid_hex, 1, 2) & " " & Mid(boxid_hex, 3, 2)
                controlboxname = Trim(rs.Fields("control_box_name").Value)
                If controlboxobj.get_communication(controlboxname) = False Then
                    m_controlboxname = controlboxname
                    '通信不正常则不测
                    Me.BackgroundWorker_getboxdata.ReportProgress(30)
                    System.Threading.Thread.Sleep(1000)
                    GoTo next1
                End If
                '发送开关量数据招测
                imei = Trim(rs.Fields("imei").Value)
                sql = "insert into TimeControl(RoadIMEI, CMDType, CMDContent, CreateTime,HandlerFlag) values('" & imei & "', '" & HG_TYPE.HG_SET_KAIGUAN & "','" & boxid_hex & "','" & Now & "','" & CONTROL_BOX_TYPE2_FLAG & "')"
                DBOperation.ExecuteSQL(conn, sql, msg)
                id_list.Add(controlboxid)  '将主控箱编号放入列表中
                boxname_list.Add(controlboxname) '主控箱名称
            End If
next1:
            rs.MoveNext()
        End While
        '发送完所有召测数据后，等待召测结果
        System.Threading.Thread.Sleep(1000)
        tag = Get_returnkaiguanvalue(id_list, boxname_list, now_time, 0)
finish:

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    ''' 获取招测的三遥数据
    ''' </summary>
    ''' <param name="boxid"></param>
    ''' <param name="time"></param>
    ''' <param name="sendtimes">招测的次数，第一次显示，第二次不显示，两次目的是使用户点击招测后有报警立刻报警</param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Private Function Get_returnkaiguanvalue(ByVal boxid As ArrayList, ByVal boxname As ArrayList, ByVal time As DateTime, ByVal sendtimes As Integer) As Boolean
        Dim rs, rs_box, rs_record As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String = ""
        Dim msg As String = ""
        Dim controlboxobj As New control_box
        Dim problem_tag As String = "正常"
        Dim row_count As Integer = 0
        Dim group_num As Integer = 0  '组号
        Dim sanyao_data(56) As String
        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim t As Integer = 0
        Dim datainf As Boolean = False
        Dim alarm_list(2) As String '存放开关量报警的3位长度的字符串
        'Dim alarmstring As String '报警字符串
        'Dim alarminf() As String '故障列表
        Dim alarm_tag As Boolean = False
        Dim id_hex As String '主控箱编号的十六进制

        Get_returnkaiguanvalue = False
        If DBOperation.OpenConn(conn) = False Then
            Exit Function
        End If
        m_waittime = g_ycwaittime
        While m_waittime > 0
            If Me.BackgroundWorker_getboxdata.CancellationPending = False Then
                t = 0
                While t < boxid.Count
                    id_hex = Com_inf.Dec_to_Hex(boxid(t), 4)
                    id_hex = Mid(id_hex, 1, 2) & " " & Mid(id_hex, 3, 2)

                    sql = "select * from RoadLightStatus where PackType='" & HG_TYPE.HG_GET_KAIGUAN & "' and HandlerFlag=3 and Createtime>'" & time.AddMinutes(-10) & "' and StatusContent like'" & boxid(t) & "%' order by id desc"
                    rs = DBOperation.SelectSQL(conn, sql, msg)
                    If rs Is Nothing Then
                        MsgBox(MSG_ERROR_STRING, , PROJECT_TITLE_STRING)
                        conn.Close()
                        conn = Nothing
                        Exit Function
                    End If
                    If rs.RecordCount > 0 Then
                        'controlboxobj.get_kaiguanString(Trim(rs.Fields("StatusContent").Value), alarm_list)

                        ''2011年4月20日增加电控箱的用电类型：电池或交流电
                        'sql = "select control_box_name,power_type,kaiguan_string,Createtime from control_box where control_box_id='" & boxid(t) & "'"
                        'rs_box = DBOperation.SelectSQL(conn, sql, msg)
                        'If rs_box Is Nothing Then
                        '    If rs.State = 1 Then
                        '        rs.Close()
                        '        rs = Nothing
                        '    End If
                        '    conn.Close()
                        '    conn = Nothing
                        '    Exit Function
                        'End If
                        'If rs_box.RecordCount > 0 Then

                        '    i = 0
                        '    While i < alarm_list(1).Length
                        '        alarmstring = controlboxobj.alarm_yes_no(i + 1, Mid(alarm_list(1), 16 - i, 1), boxname(t))
                        '        If alarmstring <> "" Then  '报警

                        '            '有报警
                        '            alarminf = Trim(alarmstring).Split(" ")
                        '            j = 0
                        '            While j < alarminf.Length
                        '                sql = "select * from kaiguan_alarm_list where control_box_name='" & boxname(t) & "' and alarm_string='" & alarminf(j) & "' and (alarm_tag=0 or alarm_tag=2)"
                        '                rs_record = DBOperation.SelectSQL(conn, sql, msg)
                        '                If rs_record Is Nothing Then

                        '                    GoTo finish
                        '                End If
                        '                If rs_record.RecordCount <= 0 Then
                        '                    alarm_tag = True
                        '                    '原来没有报警数据
                        '                    sql = "insert into kaiguan_alarm_list(alarm_string,createtime,alarm_tag," _
                        '                  & "control_box_name,kaiguan_tag) values('" & alarminf(j) & "','" & Now & "'," _
                        '                  & "0,'" & boxname(t) & "' ,'" & i + 1 & "')"
                        '                    DBOperation.ExecuteSQL(conn, sql, msg)
                        '                Else
                        '                    '有原来有报警信息，则将原来的报警信息置为2，表示经过确认的
                        '                    alarm_tag = True
                        '                    sql = "update kaiguan_alarm_list set alarm_tag=2 where id='" & rs_record.Fields("id").Value & "'"
                        '                    DBOperation.ExecuteSQL(conn, sql, msg)

                        '                End If
                        '                j += 1

                        '            End While


                        '        Else  '状态是正常的，则查询当前的报警表，有报警信息则将报警信息置1
                        '            sql = "select * from kaiguan_alarm_list where control_box_name='" & boxname(t) & "' and kaiguan_tag='" & i + 1 & "' and (alarm_tag=0 or alarm_tag=2)"
                        '            rs_record = DBOperation.SelectSQL(conn, sql, msg)
                        '            If rs_record Is Nothing Then

                        '                GoTo finish
                        '            End If
                        '            While rs_record.EOF = False
                        '                alarm_tag = True
                        '                '原来有报警数据，则将报警标志置为2，表示报警结束
                        '                sql = "update kaiguan_alarm_list set alarm_tag=1,endtime='" & Now & "' where id='" & rs_record.Fields("id").Value & "'"
                        '                DBOperation.ExecuteSQL(conn, sql, msg)

                        '                rs_record.MoveNext()
                        '            End While


                        '        End If
                        '        i += 1  '同一个主控箱中的16位开关量报警数据
                        '    End While

                        '    '更新主控箱的供电类型
                        '    If alarm_list(2) = 1 Then

                        '        sql = "update control_box set power_type='" & POWERTYPE_BUTTERY & "',kaiguan_string='" & alarm_list(1) & "', Createtime='" & Now & "'  where control_box_name='" & boxname(t) & "'"
                        '    Else
                        '        sql = "update control_box set power_type='" & POWERTYPE_CURRENT & "',kaiguan_string='" & alarm_list(1) & "', Createtime='" & Now & "' where control_box_name='" & boxname(t) & "'"
                        '    End If


                        '    DBOperation.ExecuteSQL(conn, sql, msg)

                        '    '记录供电情况
                        '    If alarm_list(2) = 1 Then
                        '        Setcontrolbox_Record(boxname(t), POWERTYPE_BUTTERY, "供电")
                        '    Else
                        '        Setcontrolbox_Record(boxname(t), POWERTYPE_CURRENT, "供电")
                        '    End If
                        '    '失压报警刷新
                        '    BackgroundWorker_find_state.ReportProgress(2)
                        'End If

                        ''将本条记录置为1
                        'sql = "update RoadLightStatus set handlerflag=1 where id='" & rs.Fields("id").Value & "'"
                        'DBOperation.ExecuteSQL(conn, sql, msg)
                        rs.MoveNext()

                        '有上传数据，数据处理完毕后将该主控箱的编号移去
                        boxid.RemoveAt(t)
                        boxname.RemoveAt(t)

                    Else
                        t += 1
                    End If

                End While

                If t = 0 Then  '表示说有的数据全部召测完毕
                    Exit While
                End If
                m_waittime -= 1
                System.Threading.Thread.Sleep(1000)
            Else
                Exit While
            End If

        End While

        '查询超时，不正常
        'If m_waittime <= 0 Then
        '    If sendtimes = 0 Then
        '        t = 0
        '        While t < boxname.Count
        '            m_controlboxid = boxid(t)
        '            m_controlboxname = boxname(t)
        '            '超时
        '            SetDataGridviewDelegate(boxname(t), boxid(t), m_boardid, m_rowcount, dgv_boxinf, 1)
        '            System.Threading.Thread.Sleep(500)
        '            t += 1
        '        End While
        '    End If

        'End If
finish:
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        If rs_record.State = 1 Then
            rs_record.Close()
            rs_record = Nothing
        End If
        If rs_box.State = 1 Then
            rs_box.Close()
            rs_box = Nothing
        End If
        conn.Close()
        conn = Nothing


    End Function


    ''' <summary>
    ''' 整点主动要求三遥数据和开关量
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BackgroundWorker_getboxdata_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker_getboxdata.DoWork

        BackgroundWorker_getboxdata.ReportProgress(21)

        Com_inf.setstatueflag()

        '召测三遥数据
        sendsanyao_data()
        BackgroundWorker_getboxdata.ReportProgress(22)
        '召测开关量
        sendkaiguan_data()

        BackgroundWorker_getboxdata.ReportProgress(23)

        '每次整点获取完毕后，保存一下当前的数据
        m_controlboxobj.Saveboxdata("")

    End Sub
    Private Function Get_returnvalue(ByVal box_name As String, ByVal box_id As String, ByVal boxid_hex As String, ByVal time As DateTime, ByVal row_id As Integer) As Boolean

        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String = ""
        Dim msg As String = ""
        Dim recString As String '接收到的数据
        Dim data() As String
        Dim controlboxobj As New control_box
        Dim problem_tag As String = "正常"
        Dim row_count As Integer = 0
        Dim group_num As Integer = 0  '组号
        Dim sanyao_data(56) As String
        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim datainf As Boolean = False

        Get_returnvalue = False
        If DBOperation.OpenConn(conn) = False Then
            Exit Function
        End If
        Dim waittime As Integer = g_ycwaittime
        While waittime > 0
            SetTextDelegate("正在召测主控箱" & box_name & "数据......(" & (g_ycwaittime + 1 - waittime) & "秒)" & vbCrLf, True, rtb_info_list)

            sql = "select * from RoadLightStatus where PackType='" & HG_TYPE.HG_GET_YAOCE & "' and HandlerFlag=3 and Createtime>'" & time.AddMinutes(-10) & "' and StatusContent like'" & boxid_hex & "%'"
            rs = DBOperation.SelectSQL(conn, sql, msg)
            If rs Is Nothing Then
                MsgBox(MSG_ERROR_STRING, , PROJECT_TITLE_STRING)
                conn.Close()
                conn = Nothing
                Exit Function
            End If
            If rs.RecordCount > 0 Then
                recString = Trim(rs.Fields("StatusContent").Value)
                data = recString.Split(" ")


                If data.Length = SMALL_DATALENGHT Then
                    datainf = controlboxobj.CheckData(data, rs.Fields("ID").Value)
                    If datainf = False Then
                        '证明上传的数据为超时数据，丢弃不管
                        SetTextDelegate("主控箱" & box_name & "数据召测超时......" & vbCrLf, True, rtb_info_list)
                        GoTo finish
                    End If
                    '所获取的数据为小三遥数据，将数据中第三个字节的组数减1变为组号，调用通用的三遥数据分析

                    data(2) = (Val(data(2).ToString) - 1).ToString
                    controlboxobj.Get_Huilu_inf_small(1, data, problem_tag, box_id, rs.Fields("ID").Value)
                    Get_returnvalue = True
                    SetTextDelegate("主控箱" & box_name & "数据召测成功" & vbCrLf, True, rtb_info_list)

                    Exit While

                End If

                If data.Length = 3 + data(2) * 54 Then
                    '传统三遥数据
                    group_num = Val(data(2))
                    j = 0
                    While j < group_num

                        sanyao_data(0) = data(0)
                        sanyao_data(1) = data(1)
                        sanyao_data(2) = j
                        i = 3
                        While i < 57
                            sanyao_data(i) = data(i + (j) * 27)
                            i += 1
                        End While

                        datainf = controlboxobj.CheckData(data, rs.Fields("ID").Value)
                        If datainf = False Then
                            '证明上传的数据为超时数据，丢弃不管
                            j += 1
                            Continue While
                        End If

                        controlboxobj.Get_Huilu_inf(j, group_num, sanyao_data, problem_tag, box_id)

                        '分析control_box表中的数据
                        Get_returnvalue = True

                        j += 1

                    End While
                    SetTextDelegate("主控箱" & box_name & "数据召测成功" & vbCrLf, True, rtb_info_list)

                    Exit While

                End If

            End If
            'm_waittime -= 1
            waittime -= 1
            System.Threading.Thread.Sleep(1000)
        End While

        '查询超时，不正常
        If waittime <= 0 Then
            SetTextDelegate("主控箱" & box_name & "数据召测超时" & vbCrLf, True, rtb_info_list)

        End If
finish:
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing


    End Function

    Private Sub 招测数据ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 召测数据ToolStripMenuItem.Click
        'If GetInstanceState("遥测窗口") Then
        '    Exit Sub
        'End If
        '遥测窗口.MdiParent = Me
        Dim zhaoceobj As New 遥测窗口
        Dim controlboxobj As New control_box
        controlboxobj.set_controlbox_list(zhaoceobj.tv_yaoce_controlbox) '主控箱信息列表
        zhaoceobj.ShowDialog()
    End Sub

    Private Sub bt_clear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.pl_boxinf.Visible = False
    End Sub

    Private Sub pb_visible_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pb_visible.Click
        Me.pl_boxinf.Visible = False
    End Sub

    Private Sub 刷新当日模式ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 刷新当日模式ToolStripMenuItem.Click
        '查找每个主控箱，判断其是否有节假日控制模式
        Com_inf.Set_holiday_mod()  '设置主控箱当天是否有节假日的控制模式，标志位为control_box表中的elec_state字段

    End Sub


    Private Sub Tab_show_box_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Tab_show_box.SelectedIndexChanged
        If Tab_show_box.SelectedIndex = 1 Then
            area_content_list_all(0)  '右侧监控面板中灯的状态列表，初始状态为查询全部

        End If
    End Sub

    Private Sub BackgroundWorker_getboxdata_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker_getboxdata.ProgressChanged
        '召测三遥数据
        If e.ProgressPercentage = 21 Then
            ' rtb_info_list.AppendText("开始召测三遥数据......" & vbCrLf)
            SetTextDelegate("开始召测三遥数据......" & vbCrLf, True, rtb_info_list)
        End If

        '招测开关量数据
        If e.ProgressPercentage = 22 Then
            'rtb_info_list.AppendText("开始召测开关量数据......" & vbCrLf)
            SetTextDelegate("开始召测开关量数据......" & vbCrLf, True, rtb_info_list)

        End If

        '数据召测完毕
        If e.ProgressPercentage = 23 Then
            ' rtb_info_list.AppendText("数据召测完毕" & vbCrLf)
            SetTextDelegate("数据召测完毕" & vbCrLf, True, rtb_info_list)

        End If

        If e.ProgressPercentage <= 20 Then
            ' rtb_info_list.AppendText("正在召测数据...(" & (g_ycwaittime + 1 - e.ProgressPercentage) & "秒)" & vbCrLf)
            SetTextDelegate("正在召测数据...(" & (g_ycwaittime + 1 - e.ProgressPercentage) & "秒)" & vbCrLf, True, rtb_info_list)

        End If

        If e.ProgressPercentage = 30 Then
            'rtb_info_list.AppendText("主控箱：" & m_controlboxname & "通信不正常无法召测" & vbCrLf)
            SetTextDelegate("主控箱：" & m_controlboxname & " 未连接无法召测" & vbCrLf, True, rtb_info_list)

        End If

        If e.ProgressPercentage = 31 Then
            'rtb_info_list.AppendText("主控箱：" & m_controlboxname & "数据召测超时！" & vbCrLf)
            SetTextDelegate("主控箱：" & m_controlboxname & " 数据召测超时！" & vbCrLf, True, rtb_info_list)

        End If
        'rtb_info_list.Select(rtb_info_list.Text.Length, 0)
        'rtb_info_list.ScrollToCaret()
    End Sub

    Private Sub BackgroundWorker_getboxdata_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker_getboxdata.RunWorkerCompleted
        g_zhaocetag = False '招测完毕后将控制招测标志位置为false
    End Sub


    Private Sub 抄表功能ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 抄表功能ToolStripMenuItem.Click
        'If GetInstanceState("抄表") Then
        '    Exit Sub
        'End If
        '抄表.MdiParent = Me
        Dim powermeterobj As New 抄表
        powermeterobj.ShowDialog()
    End Sub

    Private Sub 配置参数查询ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 配置参数查询ToolStripMenuItem.Click
        'If GetInstanceState("配置数据") Then
        '    Exit Sub
        'End If
        '配置数据.MdiParent = Me
        Dim readconfigobj As New 配置数据
        readconfigobj.ShowDialog()
    End Sub

    ''' <summary>
    ''' 双击右侧报警信息面板，可根据面板中的主控箱名称在地图上定位
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgv_alarmlist_CellMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgv_alarmlist.CellMouseDoubleClick
        find_box_pos(dgv_alarmlist.CurrentRow.Cells("alarm_control_box_name").Value) '定位
    End Sub

    Private Sub BackgroundWorker_check_lightvalue_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker_check_lightvalue.DoWork
        '检测光照度值
        While Me.BackgroundWorker_check_lightvalue.CancellationPending = False
            Try
                Me.BackgroundWorker_check_lightvalue.ReportProgress(1)
                'light_read_fun()

            Catch ex As Exception
                SetTextDelegate(MSG_ERROR_STRING & "BackgroundWorker_find_state_DoWork" & ex.ToString & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, True, rtb_info_list)

            End Try
            System.Threading.Thread.Sleep(6000)
        End While


    End Sub

    ''' <summary>
    ''' 发送单灯状态查询命令，如有返回则通信正常，没有返回则提示通信阻塞，稍候重试
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub light_read_fun()
        Dim InputRange As Integer
        Dim ADBuffer(0 To 8191) As UInt16 ' 分配缓冲区(存储原始数据)
        Dim hDevice As Long
        Dim DeviceLgcID As Integer
        Dim dwErrorCode As Integer
        Dim strDwError As String
        Dim strErrorMsg As String = ""
        Dim ADPara As PCI8735_PARA_AD        ' 硬件参数
        Dim nReadSizeWords As Long         ' 每次读取AD数据的长度(字)
        Dim nRetSizeWords As Long

        Dim ADData As Integer
        Dim fVolt(0 To 3) As Single

        DeviceLgcID = 0
        hDevice = PCI8735_CreateDevice(DeviceLgcID) ' 创建设备对象
        If hDevice = INVALID_HANDLE_VALUE Then
            dwErrorCode = PCI8735_GetLastErrorEx("PCI8735_CreateDevice", strErrorMsg)
            strDwError = dwErrorCode
            MsgBox("dwErrorCode = " + strDwError + strErrorMsg)
            Exit Sub
        End If

        InputRange = 3        ' 要求用户从键盘上选择输入量程,0-10V

        ' 预置硬件参数
        ADPara.FirstChannel = 0 ' 首通道
        ADPara.LastChannel = 0 ' 末通道
        ADPara.InputRange = InputRange    ' 模拟量输入量程范围
        ADPara.GroundingMode = PCI8735_GNDMODE_SE ' 选择接地方式为单端	
        ADPara.Gains = PCI8735_GAINS_1MULT ' 程控增益

        If PCI8735_InitDeviceAD(hDevice, ADPara) = False Then     ' 初始化硬件
            dwErrorCode = PCI8735_GetLastErrorEx("PCI8735_InitDeviceProAD", strErrorMsg)
            strDwError = dwErrorCode
            MsgBox("dwErrorCode = " + strDwError + strErrorMsg)
            PCI8735_ReleaseDeviceAD(hDevice)
        End If

        Dim ChannelCount As Integer
        ChannelCount = ADPara.LastChannel - ADPara.FirstChannel + 1
        nReadSizeWords = 512 - 512 Mod ChannelCount ' 将数据长度字转换为双字

        If PCI8735_ReadDeviceAD(hDevice, ADBuffer(0), nReadSizeWords, nRetSizeWords) = False Then
            dwErrorCode = PCI8735_GetLastErrorEx("PCI8735_StartDeviceProAD", strErrorMsg)
            strDwError = dwErrorCode
            MsgBox("dwErrorCode = " + strDwError + strErrorMsg)
            PCI8735_ReleaseDeviceAD(hDevice)
            PCI8735_ReleaseDevice(hDevice)
        End If

        Dim Index As Integer
        For Index = 0 To 3 Step 1
            ' 将原码转换为电压值
            ADData = ADBuffer(Index) And 65535
            Select Case InputRange
                Case PCI8735_INPUT_N10000_P10000mV     ' -10V - +10V
                    fVolt(Index) = (20000.0# / 8192) * ADData - 10000.0#
                Case PCI8735_INPUT_N5000_P5000mV     ' -5V - +5V
                    fVolt(Index) = (10000.0# / 8192) * ADData - 5000.0#
                Case PCI8735_INPUT_N2500_P2500mV     ' -2.5V - +2.5V
                    fVolt(Index) = (5000.0# / 8192) * ADData - 2500.0#
                Case PCI8735_INPUT_0_P10000mV     ' 0V - +10V
                    fVolt(Index) = (10000.0# / 8192) * ADData

                Case Else
                    MsgBox("量程选择错误....")
            End Select
        Next Index

        g_lightvalue = Format(fVolt(0) / 40, "00.00")
        Me.BackgroundWorker_check_lightvalue.ReportProgress(1)

    End Sub


    Private Sub BackgroundWorker_check_lightvalue_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker_check_lightvalue.ProgressChanged
        If e.ProgressPercentage = 1 Then
            Lightvalue.Text = "当前光照度值：" & g_lightvalue.ToString

        End If
    End Sub



    Private Sub SerialPortMSG_DataReceived(ByVal sender As System.Object, ByVal e As System.IO.Ports.SerialDataReceivedEventArgs) Handles SerialPortMSG.DataReceived
        '接收当前短信猫的信息
        recvMsg()
    End Sub

    Private Sub recvMsg()

        Dim i As Integer = 0
        Dim control_type As String = ""  '控制类型 单灯，主控箱
        Dim SciReciverNum As Integer = 20
        Dim SciReciver As New ArrayList    '接收数组
        Dim recstring As String
        Dim mailcontent() As String
        Dim control_method As String '控制方式
        Me.SerialPortMSG.Encoding = System.Text.Encoding.GetEncoding("Gb2312")
        Dim phone_num As String

        If Me.SerialPortMSG.IsOpen = False Then
            Exit Sub
        End If
        System.Threading.Thread.Sleep(1000)
        Try
            If Me.SerialPortMSG.BytesToRead > 0 Then
                recstring = Me.SerialPortMSG.ReadExisting
                phone_num = recstring.Split(",")(0)
                If Com_inf.getphonenum(phone_num) = True Then
                    SetTextDelegate("收到来自" & phone_num & "新信息" & recstring.Split(",")(1) & "  时间：" & Now.ToString & vbCrLf, True, rtb_info_list)

                Else
                    SetTextDelegate("收到非正常控制信息  时间：" & Now.ToString & vbCrLf, True, rtb_info_list)                ' sendMsg(phone_num, "您未被授权，无法操作！")

                    Exit Sub
                End If
                mailcontent = recstring.Split(",")(1).Split("*")
                If mailcontent.Length >= 2 Then
                    control_type = mailcontent(0)

                    If control_type = "单灯" Then
                        control_method = mailcontent(2)
                        control_lamp_mail(mailcontent(1), phone_num, control_method)

                    Else
                        If control_type = "主控箱" Then
                            If mailcontent.Length = 3 Then
                                '主控箱全控命令
                                control_method = mailcontent(2)
                                control_box_mail(mailcontent(1), "", phone_num, control_method)
                            Else
                                If mailcontent.Length = 4 Then
                                    '控回路的命令
                                    control_method = mailcontent(3)
                                    control_box_mail(mailcontent(1), mailcontent(2), phone_num, control_method)

                                Else
                                    If mailcontent.Length = 2 Then
                                        '所有主控箱整体控制
                                        control_method = mailcontent(1)  '全开或全闭
                                        control_box_mail("", "", phone_num, control_method)

                                    Else
                                        '命令执行完毕，返回确认短信
                                        sendMsg(phone_num, "命令格式错误！")

                                    End If
                                End If
                            End If

                        Else

                            '命令执行完毕，返回确认短信
                            sendMsg(phone_num, "命令格式错误！")
                        End If

                    End If '判断接收的数据是不是长度大于3

                Else
                    '命令执行完毕，返回确认短信
                    sendMsg(phone_num, "命令格式错误！")

                End If
            End If
        Catch ex As Exception

        End Try


    End Sub

    ''' <summary>
    ''' 发送短信
    ''' </summary>
    ''' <param name="phonenum">短信号码</param>
    ''' <param name="msg">短信内容,短信内容最长为30个字</param>
    ''' <remarks></remarks>
    Private Function sendMsg(ByVal phonenum As String, ByVal msg As String) As Boolean
        If Me.SerialPortMSG.IsOpen = False Then
            sendMsg = False
            Exit Function

        End If
        'sendData=system.
        Dim msgLen As Integer = System.Text.Encoding.GetEncoding("gb2312").GetByteCount(msg)
        Dim msgArray() As Byte = System.Text.Encoding.GetEncoding("gb2312").GetBytes(msg)
        Dim phonenumArray() As Byte = System.Text.Encoding.GetEncoding("gb2312").GetBytes(phonenum)
        Dim phonenumlen As Integer = System.Text.Encoding.GetEncoding("gb2312").GetByteCount(phonenum)
        Dim sendMSGArray(msgLen + 4 + phonenumlen) As Byte

        Dim index As Integer = 0
        sendMSGArray(index) = &H2
        index += 1
        sendMSGArray(index) = phonenumArray.Length
        index += 1
        Array.Copy(phonenumArray, 0, sendMSGArray, 2, phonenumArray.Length)
        index += phonenumArray.Length

        sendMSGArray(index) = msgLen >> 8
        index += 1
        sendMSGArray(index) = msgLen
        index += 1
        Array.Copy(msgArray, 0, sendMSGArray, index, msgArray.Length)
        index += msgArray.Length
        sendMSGArray(index) = &H3

        Me.SerialPortMSG.Write(sendMSGArray, 0, sendMSGArray.Length)

        sendMsg = True
    End Function


    ''' <summary>
    ''' 短信控制主控箱
    ''' </summary>
    ''' <param name="control_box_id">主控箱编号</param>
    ''' <param name="phone_num">电话号码</param>
    ''' <param name="control_method">控制方法</param>
    ''' <param name="jiechuqi_id">接触器编号，主控箱全控该字段则为“”</param>
    ''' <remarks></remarks>
    Private Sub control_box_mail(ByVal control_box_id As String, ByVal jiechuqi_id As String, ByVal phone_num As String, ByVal control_method As String)
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String = ""
        Dim sql As String
        Dim control_box_name As String = ""
        Dim control_lamp_obj As New control_lamp
        Dim lamp_type_string As String '类型名称
        Dim str_len As Integer
        Dim lamp_id_bin As String '灯类型的二进制
        Dim lamp_id_string As String
        Dim control_name As String
        Dim diangan As String = "全功率"
        Dim power As String = "100"
        Dim hand_type = "" '
        Dim result_string As Boolean  '短信控制结果
        lamp_type_string = Com_inf.Get_Type_String(31)

        DBOperation.OpenConn(conn)

        If control_box_id <> "" Then
            If control_box_id = "全部" Then
                '集体控制主控器下面的某一个接触器
                sql = "select control_box_name,control_box_id from control_box order by control_box_id"
            Else
                While control_box_id.Length < 4
                    control_box_id = "0" & control_box_id

                End While
                sql = "select control_box_name,control_box_id from control_box where control_box_id='" & control_box_id & "'"

            End If
        Else
            sql = "select control_box_name,control_box_id from control_box order by control_box_id"

        End If

        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "AxModem1_recvMsg", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        While rs.EOF = False
            control_box_name = Trim(rs.Fields("control_box_name").Value)  '主控箱名称
            control_box_id = Trim(rs.Fields("control_box_id").Value)
            If jiechuqi_id = "" Then  '表示按主控箱整体控制
                hand_type = "主控箱"
                control_name = control_box_name & " " & lamp_type_string
                If control_method.Trim() = "全开#" Then
                    control_lamp_obj.Input_control_inf(lamp_type_string, "类型", control_box_name, "类型全开", 1, "全功率", "100", 0, -1)

                    control_lamp_obj.Input_control_inf("", "主控箱", control_box_name, "全开", 1, "全功率", "100", 0, -1)


                    '记录手控信息到数据库

                    sql = "insert into hand_control_record(control_content,content_name,control_method,diangan,power,control_time,user_name) values('" & hand_type & "','" & control_name & "','" & control_method & "','" & diangan & "','" & power & "','" & Now() & "','" & g_username & "')"

                    'sql = "insert into hand_control_record(control_content,content_name,control_method,diangan,power,control_time) values('" & condition & "','" & condition_name & lamp_type & "','" & control_method & "','" & diangan & "','" & power & "','" & Now() & "')"
                    DBOperation.ExecuteSQL(conn, sql, msg)
                    '命令执行完毕，返回确认短信
                    'sendMsg(phone_num, "命令发送成功！")
                    result_string = True
                    GoTo next1
                End If
                If control_method.Trim() = "全闭#" Then
                    control_lamp_obj.Input_control_inf(lamp_type_string, "类型", control_box_name, "类型全闭", 0, "关闭灯", "0", 0, -1)

                    control_lamp_obj.Input_control_inf("", "主控箱", control_box_name, "全闭", 0, "关闭灯", "0", 0, -1)
                    '记录手控信息到数据库

                    sql = "insert into hand_control_record(control_content,content_name,control_method,diangan,power,control_time,user_name) values('" & hand_type & "','" & control_name & "','" & control_method & "','" & diangan & "','" & power & "','" & Now() & "','" & g_username & "')"

                    'sql = "insert into hand_control_record(control_content,content_name,control_method,diangan,power,control_time) values('" & condition & "','" & condition_name & lamp_type & "','" & control_method & "','" & diangan & "','" & power & "','" & Now() & "')"
                    DBOperation.ExecuteSQL(conn, sql, msg)

                    '命令执行完毕，返回确认短信
                    'sendMsg(phone_num, "命令发送成功！")
                    result_string = True

                    GoTo next1
                End If
                result_string = False
            Else
                str_len = jiechuqi_id.Length
                jiechuqi_id = Mid(jiechuqi_id, 2, str_len - 1)
                lamp_id_bin = Com_inf.Get_lampid_bin("31", Val(jiechuqi_id)) '十六位长度的终端编号二进制
                lamp_type_string = Com_inf.Get_Type_String(31)

                While control_box_id.Length < 4
                    control_box_id = "0" & control_box_id
                End While

                While jiechuqi_id.Length < LAMP_ID_LEN
                    jiechuqi_id = "0" & jiechuqi_id
                End While
                lamp_id_string = control_box_id & "31" & jiechuqi_id
                If control_method.Trim() = "开#" Then
                    '打开终端操作
                    control_lamp_obj.open_light_single(control_box_id, lamp_id_bin, lamp_id_string, "全功率", "100", 0, -1)
                    '打开的是回路，将该回路下所有的终端全部打开
                    control_lamp_obj.open_close_huilulamp(1, Val(jiechuqi_id), control_box_id)
                    '命令执行完毕，返回确认短信
                    'sendMsg(phone_num, "命令发送成功！")
                    result_string = True

                    GoTo next1
                End If
                If control_method.Trim() = "闭#" Then
                    '关闭终端
                    control_lamp_obj.close_light_single(control_box_id, lamp_id_bin, lamp_id_string, "关闭灯", "0", 0, -1)
                    '打开的是回路，将该回路下所有的终端全部打开
                    control_lamp_obj.open_close_huilulamp(0, Val(jiechuqi_id), control_box_id)
                    '命令执行完毕，返回确认短信
                    'sendMsg(phone_num, "命令发送成功！")
                    result_string = True

                    GoTo next1
                End If
                'sendMsg(phone_num, "命令格式错误！")
                result_string = False

            End If
next1:
            rs.MoveNext()
        End While


finish:
        If result_string = True Then
            sendMsg(phone_num, "命令发送成功！")
        Else
            sendMsg(phone_num, "命令格式错误！")
        End If
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    ''' 短信控制单灯
    ''' </summary>
    ''' <param name="lampid">灯的编号1-0-1</param>
    ''' <param name="phone_num">电话号码</param>
    ''' <param name="control_method">控制方法，单开，单闭，全开，全闭</param>
    ''' <remarks></remarks>
    Private Sub control_lamp_mail(ByVal lampid As String, ByVal phone_num As String, ByVal control_method As String)

        Dim control_box_id As String = ""  '主控箱编号
        Dim lamp_type_id As String = ""  '类型编号
        Dim lamp_short_id As String = "" '灯的独立编号
        Dim lamp_id() As String   '单灯编号
        Dim lamp_id_string As String '单灯编号

        Dim lamp_id_bin As String '灯类型的二进制
        Dim power_string As String
        Dim lamp_type_string As String '类型名称

        Dim control_lamp_obj As New control_lamp
        Dim control_box_name As String = "" '主控箱名称
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String = ""
        Dim sql As String
        Dim control_name As String
        Dim diangan As String = "全功率"
        Dim power As String = "100"
        Dim hand_type = "" '
        '表示控单灯 接下来单灯的格式为1-0-1
        lamp_id = lampid.Split("-")
        If lamp_id.Length = 3 Then
            control_box_id = lamp_id(0)
            lamp_type_id = lamp_id(1)
            lamp_short_id = lamp_id(2)

            power_string = "100"

            lamp_id_bin = Com_inf.Get_lampid_bin(lamp_type_id, Val(lamp_short_id)) '十六位长度的终端编号二进制
            lamp_type_string = Com_inf.Get_Type_String(Val(lamp_type_id))

            While control_box_id.Length < 4
                control_box_id = "0" & control_box_id
            End While
            While lamp_type_id.Length < 2
                lamp_type_id = "0" & lamp_type_id
            End While
            While lamp_short_id.Length < LAMP_ID_LEN
                lamp_short_id = "0" & lamp_short_id
            End While
            lamp_id_string = control_box_id & lamp_type_id & lamp_short_id


            If control_method.Trim() = "单开#" Then  '开
                '打开终端操作
                control_lamp_obj.open_light_single(control_box_id, lamp_id_bin, lamp_id_string, "全功率", power_string, 0, -1)
                If lamp_type_id = "31" Then
                    '如果打开的是回路，将该回路下所有的终端全部打开
                    control_lamp_obj.open_close_huilulamp(1, Val(lamp_short_id), control_box_id)
                End If
                '命令执行完毕，返回确认短信
                sendMsg(phone_num, "命令发送成功！")
                Exit Sub
            End If


            If control_method.Trim() = "单闭#" Then
                '关闭终端
                control_lamp_obj.close_light_single(control_box_id, lamp_id_bin, lamp_id_string, "关闭灯", "0", 0, -1)
                If lamp_type_id = "31" Then
                    '如果打开的是回路，将该回路下所有的终端全部打开
                    control_lamp_obj.open_close_huilulamp(1, Val(lamp_short_id), control_box_id)
                End If
                '命令执行完毕，返回确认短信
                sendMsg(phone_num, "命令发送成功！")
                Exit Sub
            End If

            DBOperation.OpenConn(conn)
            sql = "select control_box_name from control_box where control_box_id='" & control_box_id & "'"

            rs = DBOperation.SelectSQL(conn, sql, msg)
            If rs Is Nothing Then
                MsgBox(MSG_ERROR_STRING & "AxModem1_recvMsg", , PROJECT_TITLE_STRING)
                conn.Close()
                conn = Nothing
                Exit Sub
            End If
            If rs.RecordCount > 0 Then
                control_box_name = Trim(rs.Fields("control_box_name").Value)  '主控箱名称
            End If
            If control_method.Trim() = "全开#" Then
                '按类型全开
                control_lamp_obj.Input_control_inf(lamp_type_string, "类型", control_box_name, "类型全开", 1, "全功率", power_string, 0, -1)
                If lamp_type_id = "31" Then
                    '控制的是主控箱节点,则将该回路中的所有灯都打开
                    control_lamp_obj.Input_control_inf("", "主控箱", control_box_name, "全开", 1, "全功率", power_string, 0, -1)
                End If
                hand_type = "类型"
                control_name = control_box_name & " " & lamp_type_string
                '记录手控信息到数据库

                sql = "insert into hand_control_record(control_content,content_name,control_method,diangan,power,control_time,user_name) values('" & hand_type & "','" & control_name & "','" & control_method & "','" & diangan & "','" & power & "','" & Now() & "','" & g_username & "')"

                'sql = "insert into hand_control_record(control_content,content_name,control_method,diangan,power,control_time) values('" & condition & "','" & condition_name & lamp_type & "','" & control_method & "','" & diangan & "','" & power & "','" & Now() & "')"
                DBOperation.ExecuteSQL(conn, sql, msg)
                '命令执行完毕，返回确认短信
                sendMsg(phone_num, "命令发送成功！")
                GoTo finish
            End If

            If control_method.Trim() = "全闭#" Then
                '按类型全关
                control_lamp_obj.Input_control_inf(lamp_type_string, "类型", control_box_name, "类型全闭", 0, "关闭灯", power_string, 0, -1)
                If lamp_type_id = "31" Then
                    '控制的回路，则将该类型下的灯全部关闭
                    control_lamp_obj.Input_control_inf("", "主控箱", control_box_name, "全闭", 0, "关闭灯", power_string, 0, -1)

                End If

                hand_type = "类型"
                control_name = control_box_name & " " & lamp_type_string
                '记录手控信息到数据库

                sql = "insert into hand_control_record(control_content,content_name,control_method,diangan,power,control_time,user_name) values('" & hand_type & "','" & control_name & "','" & control_method & "','" & diangan & "','" & power & "','" & Now() & "','" & g_username & "')"

                'sql = "insert into hand_control_record(control_content,content_name,control_method,diangan,power,control_time) values('" & condition & "','" & condition_name & lamp_type & "','" & control_method & "','" & diangan & "','" & power & "','" & Now() & "')"
                DBOperation.ExecuteSQL(conn, sql, msg)
                '命令执行完毕，返回确认短信
                sendMsg(phone_num, "命令发送成功！")
                GoTo finish
            Else
                '命令执行完毕，返回确认短信
                sendMsg(phone_num, "命令格式错误！")
            End If

        Else
            '命令执行完毕，返回确认短信
            sendMsg(phone_num, "命令格式错误！")

        End If  '判读单灯格式是不是1-0-1


finish:
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing

    End Sub

    Private Sub BackgroundWorkerSendMsg_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorkerSendMsg.DoWork
        While Me.BackgroundWorkerSendMsg.CancellationPending = False
            Try
                ''接收当前短信猫的信息
                'RecvMsg()

                ' System.Threading.Thread.Sleep(1000)

                SendOrder()  '搜寻数据库，看是否有没有发送的短信

                System.Threading.Thread.Sleep(60000)
            Catch ex As Exception
                ex.ToString()
            End Try

        End While
    End Sub

    ''' <summary>
    ''' 检测数据库中，看是否有要发送的短信
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SendOrder()
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String
        '  Dim MsgData() As String  '信息的内容
        Dim i As Integer = 0
        Dim ret As Boolean
        Dim sendstring As String
        Dim phonenum As String  '电话号码
        Dim nowtime As DateTime = Now

        msg = ""
        sql = ""
        sendstring = ""
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        sql = "select * from SendGSM_Modem where HandlerFlag=0"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox("数据库连接出错", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If

        While rs.EOF = False
            If rs.Fields("Createtime").Value < nowtime.AddMinutes(-30) Then
                GoTo next1
            End If
            '将数据库中未标志的短信从串口发送出去
            sendstring = Trim(rs.Fields("SendContent").Value)

            Try
                phonenum = Trim(rs.Fields("PhoneNumber").Value)
                ret = sendMsg(phonenum, sendstring)

                If ret Then
                    'ModemState.Text = "消息提示：发送成功"
                    SetTextDelegate("消息提示：报警发送成功  时间：" & Now.ToString & vbCrLf, True, rtb_info_list)
                Else
                    'ModemState.Text = "消息提示：发送失败"
                    SetTextDelegate("消息提示：报警发送失败  时间：" & Now.ToString & vbCrLf, True, rtb_info_list)
                End If
            Catch ex As Exception
                ' MsgBox("短信猫配置出错", , PROJECT_TITLE_STRING)
                SetTextDelegate("短信猫配置出错" & vbCrLf, True, rtb_info_list)
            End Try
next1:
            rs.Fields("HandlerFlag").Value = 1
            rs.Update()
            rs.MoveNext()

        End While

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing

    End Sub

    Private Sub 单灯召测ToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles 单灯召测ToolStripMenuItem.Click
        'If GetInstanceState("区间巡查") Then
        '    Exit Sub
        'End If
        '区间巡查.MdiParent = Me
        Dim zhaoceobj As New 单灯召测
        Dim controlboxobj As New control_box
        controlboxobj.set_controlbox_list(zhaoceobj.tv_yaoce_controlbox) '主控箱信息列表
        zhaoceobj.ShowDialog()
    End Sub

    Private Sub 单灯召测ToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles 单灯召测ToolStripMenuItem1.Click
        Dim control_box_name As String
        Dim zhaoceobj As New 单灯召测
        Dim controlboxobj As New control_box
        control_box_name = Trim(tv_box_inf_list.SelectedNode.Text).Split(" ")(0)
        controlboxobj.set_controlbox_list(zhaoceobj.tv_yaoce_controlbox) '主控箱信息列表
        controlboxobj.Dispose()
        zhaoceobj.m_checklist.Clear()
        zhaoceobj.m_checklist.Add(control_box_name)
        Com_inf.Addcontrolbox_to_Datagridview(zhaoceobj.m_checklist, zhaoceobj.dgv_yaoce_list, "yaoce_boxname")
        zhaoceobj.BackgroundWorker_yaoce.RunWorkerAsync()
        '招测的时候有些按钮变灰
        zhaoceobj.bt_add_boxname.Enabled = False
        zhaoceobj.bt_del_boxname.Enabled = False
        zhaoceobj.bt_yaoce.Enabled = False
        zhaoceobj.tv_yaoce_controlbox.ExpandAll()
        FindNode(zhaoceobj.tv_yaoce_controlbox.Nodes(0), control_box_name)
        zhaoceobj.ShowDialog()
    End Sub

    Private Sub 三回路组合命令ToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles 三回路组合命令ToolStripMenuItem.Click
        'If GetInstanceState("三回路组合命令") Then
        '    Exit Sub
        'End If
        '三回路组合命令.MdiParent = Me
        Dim order_windowobj As New 三回路组合命令
        order_windowobj.ShowDialog()
    End Sub

    Private Sub 二回路组合命令ToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles 二回路组合命令ToolStripMenuItem.Click
        'If GetInstanceState("二回路组合命令") Then
        '    Exit Sub
        'End If
        '二回路组合命令.MdiParent = Me
        Dim order_windowobj As New 二回路组合命令
        order_windowobj.ShowDialog()
    End Sub

    ''' <summary>
    ''' 发单灯招测命令
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BackgroundWorkergetlampdata_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorkergetlampdata.DoWork
        If BackgroundWorkergetlampdata.CancellationPending = False Then
            m_controllampobj.get_lampinf()
        End If
    End Sub

    Private Sub BackgroundWorkergetlampdata_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorkergetlampdata.RunWorkerCompleted
        If g_lampwaittime > (g_getlamp_time + g_lamptimes) * 60 Then
            g_lampwaittime = 0
        End If
    End Sub

    Private Sub 开关量输出K1开灯ToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles 开关量输出K1开灯ToolStripMenuItem.Click
        Dim control_box_name As String
        Dim control_box_id As String
        Dim lamp_id As String
        control_box_name = Trim(tv_box_inf_list.SelectedNode.Text).Split(" ")(0)
        control_box_id = Com_inf.get_box_id(control_box_name)
        While control_box_id.Length < 4
            control_box_id = "0" & control_box_id
        End While
        lamp_id = control_box_id & "3100001"
        open_close_single_lamp(lamp_id, 1, 1, 1)
        MsgBox("操作执行完毕", , PROJECT_TITLE_STRING)
    End Sub

    Private Sub 开关量输出K2开灯ToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles 开关量输出K2开灯ToolStripMenuItem.Click
        Dim control_box_name As String
        Dim control_box_id As String
        Dim lamp_id As String
        control_box_name = Trim(tv_box_inf_list.SelectedNode.Text).Split(" ")(0)
        control_box_id = Com_inf.get_box_id(control_box_name)
        While control_box_id.Length < 4
            control_box_id = "0" & control_box_id
        End While
        lamp_id = control_box_id & "3100002"
        open_close_single_lamp(lamp_id, 1, 1, 1)
        MsgBox("操作执行完毕", , PROJECT_TITLE_STRING)
    End Sub

    Private Sub 开关量输出K3开灯ToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles 开关量输出K3开灯ToolStripMenuItem.Click
        Dim control_box_name As String
        Dim control_box_id As String
        Dim lamp_id As String
        control_box_name = Trim(tv_box_inf_list.SelectedNode.Text).Split(" ")(0)
        control_box_id = Com_inf.get_box_id(control_box_name)
        While control_box_id.Length < 4
            control_box_id = "0" & control_box_id
        End While
        lamp_id = control_box_id & "3100003"
        open_close_single_lamp(lamp_id, 1, 1, 1)
        MsgBox("操作执行完毕", , PROJECT_TITLE_STRING)
    End Sub

    Private Sub 开关量输出K4开灯ToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles 开关量输出K4开灯ToolStripMenuItem.Click
        Dim control_box_name As String
        Dim control_box_id As String
        Dim lamp_id As String
        control_box_name = Trim(tv_box_inf_list.SelectedNode.Text).Split(" ")(0)
        control_box_id = Com_inf.get_box_id(control_box_name)
        While control_box_id.Length < 4
            control_box_id = "0" & control_box_id
        End While
        lamp_id = control_box_id & "3100004"
        open_close_single_lamp(lamp_id, 1, 1, 1)
        MsgBox("操作执行完毕", , PROJECT_TITLE_STRING)
    End Sub

    Private Sub 开关量输出K5开灯ToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles 开关量输出K5开灯ToolStripMenuItem.Click
        Dim control_box_name As String
        Dim control_box_id As String
        Dim lamp_id As String
        control_box_name = Trim(tv_box_inf_list.SelectedNode.Text).Split(" ")(0)
        control_box_id = Com_inf.get_box_id(control_box_name)
        While control_box_id.Length < 4
            control_box_id = "0" & control_box_id
        End While
        lamp_id = control_box_id & "3100005"
        open_close_single_lamp(lamp_id, 1, 1, 1)
        MsgBox("操作执行完毕", , PROJECT_TITLE_STRING)
    End Sub

    Private Sub 开关量输出K6开灯ToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles 开关量输出K6开灯ToolStripMenuItem.Click
        Dim control_box_name As String
        Dim control_box_id As String
        Dim lamp_id As String
        control_box_name = Trim(tv_box_inf_list.SelectedNode.Text).Split(" ")(0)
        control_box_id = Com_inf.get_box_id(control_box_name)
        While control_box_id.Length < 4
            control_box_id = "0" & control_box_id
        End While
        lamp_id = control_box_id & "3100006"
        open_close_single_lamp(lamp_id, 1, 1, 1)
        MsgBox("操作执行完毕", , PROJECT_TITLE_STRING)
    End Sub

    Private Sub 开启所有回路ToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles 开启所有回路ToolStripMenuItem.Click
        Dim control_box_name As String
        Dim control_lamp_obj As New control_lamp
        Dim m_huilu, m_lamp As String  '控制的类型名称
        Dim control_method As String = "全开"
        Dim hand_type As String = "主控箱"
        Dim m_result As Boolean '发送命令结果
        m_huilu = Com_inf.Get_Type_String(31)
        m_lamp = Com_inf.Get_Type_String(0)
        control_box_name = Trim(tv_box_inf_list.SelectedNode.Text).Split(" ")(0)
        m_result = control_lamp_obj.Input_control_inf(m_huilu, "类型", control_box_name, "类型全开", 1, "全功率", 100, hand_type, 0)
        'm_result = control_lamp_obj.Input_control_inf("", "主控箱", control_box_name, "全开", 1, "全功率", 100, hand_type, 0)
        MsgBox("操作执行完毕", , PROJECT_TITLE_STRING)
    End Sub


    Private Sub 开关量输出K1关灯ToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles 开关量输出K1关灯ToolStripMenuItem.Click
        Dim control_box_name As String
        Dim control_box_id As String
        Dim lamp_id As String
        control_box_name = Trim(tv_box_inf_list.SelectedNode.Text).Split(" ")(0)
        control_box_id = Com_inf.get_box_id(control_box_name)
        While control_box_id.Length < 4
            control_box_id = "0" & control_box_id
        End While
        lamp_id = control_box_id & "3100001"
        open_close_single_lamp(lamp_id, 0, 1, 1)
        MsgBox("操作执行完毕", , PROJECT_TITLE_STRING)
    End Sub

    Private Sub 开关量输出K2关灯ToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles 开关量输出K2关灯ToolStripMenuItem.Click
        Dim control_box_name As String
        Dim control_box_id As String
        Dim lamp_id As String
        control_box_name = Trim(tv_box_inf_list.SelectedNode.Text).Split(" ")(0)
        control_box_id = Com_inf.get_box_id(control_box_name)
        While control_box_id.Length < 4
            control_box_id = "0" & control_box_id
        End While
        lamp_id = control_box_id & "3100002"
        open_close_single_lamp(lamp_id, 0, 1, 1)
        MsgBox("操作执行完毕", , PROJECT_TITLE_STRING)
    End Sub

    Private Sub 开关量输出K3关灯ToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles 开关量输出K3关灯ToolStripMenuItem.Click
        Dim control_box_name As String
        Dim control_box_id As String
        Dim lamp_id As String
        control_box_name = Trim(tv_box_inf_list.SelectedNode.Text).Split(" ")(0)
        control_box_id = Com_inf.get_box_id(control_box_name)
        While control_box_id.Length < 4
            control_box_id = "0" & control_box_id
        End While
        lamp_id = control_box_id & "3100003"
        open_close_single_lamp(lamp_id, 0, 1, 1)
        MsgBox("操作执行完毕", , PROJECT_TITLE_STRING)
    End Sub

    Private Sub 开关量输出K4关灯ToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles 开关量输出K4关灯ToolStripMenuItem.Click
        Dim control_box_name As String
        Dim control_box_id As String
        Dim lamp_id As String
        control_box_name = Trim(tv_box_inf_list.SelectedNode.Text).Split(" ")(0)
        control_box_id = Com_inf.get_box_id(control_box_name)
        While control_box_id.Length < 4
            control_box_id = "0" & control_box_id
        End While
        lamp_id = control_box_id & "3100004"
        open_close_single_lamp(lamp_id, 0, 1, 1)
        MsgBox("操作执行完毕", , PROJECT_TITLE_STRING)
    End Sub

    Private Sub 开关量输出K5关灯ToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles 开关量输出K5关灯ToolStripMenuItem.Click
        Dim control_box_name As String
        Dim control_box_id As String
        Dim lamp_id As String
        control_box_name = Trim(tv_box_inf_list.SelectedNode.Text).Split(" ")(0)
        control_box_id = Com_inf.get_box_id(control_box_name)
        While control_box_id.Length < 4
            control_box_id = "0" & control_box_id
        End While
        lamp_id = control_box_id & "3100005"
        open_close_single_lamp(lamp_id, 0, 1, 1)
        MsgBox("操作执行完毕", , PROJECT_TITLE_STRING)
    End Sub

    Private Sub 开关量输出K6关灯ToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles 开关量输出K6关灯ToolStripMenuItem.Click
        Dim control_box_name As String
        Dim control_box_id As String
        Dim lamp_id As String
        control_box_name = Trim(tv_box_inf_list.SelectedNode.Text).Split(" ")(0)
        control_box_id = Com_inf.get_box_id(control_box_name)
        While control_box_id.Length < 4
            control_box_id = "0" & control_box_id
        End While
        lamp_id = control_box_id & "3100001"
        open_close_single_lamp(lamp_id, 0, 1, 1)
        MsgBox("操作执行完毕", , PROJECT_TITLE_STRING)
    End Sub

    Private Sub 关闭所有回路ToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles 关闭所有回路ToolStripMenuItem.Click
        Dim control_box_name As String
        Dim control_lamp_obj As New control_lamp
        Dim m_huilu, m_lamp As String  '控制的类型名称
        Dim control_method As String = "全闭"
        Dim hand_type As String = "主控箱"
        Dim m_result As Boolean '发送命令结果
        m_huilu = Com_inf.Get_Type_String(31)
        m_lamp = Com_inf.Get_Type_String(0)
        control_box_name = Trim(tv_box_inf_list.SelectedNode.Text).Split(" ")(0)
        m_result = control_lamp_obj.Input_control_inf(m_huilu, "类型", control_box_name, "类型全闭", 0, "关闭灯", 100, hand_type, 0)
        'm_result = control_lamp_obj.Input_control_inf("", "主控箱", control_box_name, "全闭", 0, "关闭灯", 100, hand_type, 0)
        MsgBox("操作执行完毕", , PROJECT_TITLE_STRING)
    End Sub

    Private Sub 主控召测ToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles 主控召测ToolStripMenuItem.Click
        Dim control_box_name As String
        Dim zhaoceobj As New 遥测窗口
        Dim controlboxobj As New control_box
        control_box_name = Trim(tv_box_inf_list.SelectedNode.Text).Split(" ")(0)
        controlboxobj.set_controlbox_list(zhaoceobj.tv_yaoce_controlbox) '主控箱信息列表
        controlboxobj.Dispose()
        zhaoceobj.m_checklist.Clear()
        zhaoceobj.m_checklist.Add(control_box_name)
        Com_inf.Addcontrolbox_to_Datagridview(zhaoceobj.m_checklist, zhaoceobj.dgv_yaoce_list, "yaoce_boxname")
        zhaoceobj.BackgroundWorker_yaoce.RunWorkerAsync()
        '招测的时候有些按钮变灰
        zhaoceobj.bt_add_boxname.Enabled = False
        zhaoceobj.bt_del_boxname.Enabled = False
        zhaoceobj.bt_yaoce.Enabled = False
        zhaoceobj.tv_yaoce_controlbox.ExpandAll()
        FindNode(zhaoceobj.tv_yaoce_controlbox.Nodes(0), control_box_name)
        zhaoceobj.ShowDialog()
    End Sub

    Private Sub 终端参数设置ToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles 终端参数设置ToolStripMenuItem.Click
        Dim control_box_name As String = Trim(tv_box_inf_list.SelectedNode.Text).Split(" ")(0)
        Dim zhaoceobj As New 增加终端
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String = ""
        Dim sql As String
        sql = "select distinct area,city,street from lamp_street where control_box_name='" & control_box_name & "'"
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "set_presure_Click", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs.RecordCount > 0 Then
            'zhaoceobj.zhaoceobj_city_area_street(rs.Fields("city").Value, rs.Fields("area").Value, rs.Fields("street").Value, control_box_name)
            zhaoceobj.Show()
        End If
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing

    End Sub

    Private Sub 当前故障ToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles 当前故障ToolStripMenuItem.Click
        'Dim zhaoceobj As New 故障报警窗口
        'Dim control_box_name As String = Trim(tv_box_inf_list.SelectedNode.Text).Split(" ")(0)
        'lamp_alarm_listshow(control_box_name)
        'zhaoceobj.Show()
        If g_lampalarm_show = False Then
            g_lampalarm_show = True
            故障报警窗口ToolStripMenuItem.Image = System.Drawing.Image.FromFile("图片\dis.png")
            当前故障ToolStripMenuItem.Image = System.Drawing.Image.FromFile("图片\dis.png")
        Else
            故障报警窗口ToolStripMenuItem.Image = System.Drawing.Image.FromFile("图片\静音.jpg")
            当前故障ToolStripMenuItem.Image = System.Drawing.Image.FromFile("图片\静音.jpg")
            g_lampalarm_show = False
        End If

    End Sub

    Private Sub 历史数据查询ToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles 历史数据查询ToolStripMenuItem.Click
        Dim control_box_name As String = Trim(tv_box_inf_list.SelectedNode.Text).Split(" ")(0)
        Dim zhaoceobj As New 终端亮暗信息统计
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String = ""
        Dim sql As String
        sql = "select distinct area,city,street from lamp_street where control_box_name='" & control_box_name & "'"
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "set_presure_Click", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs.RecordCount > 0 Then
            zhaoceobj.ShowDialog()
            'zhaoceobj.zhaoceobj_city_area_street(rs.Fields("city").Value, rs.Fields("area").Value, rs.Fields("street").Value, control_box_name)
        End If
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    Private Sub 主控召测ToolStripMenuItem23_Click(sender As System.Object, e As System.EventArgs) Handles 主控召测ToolStripMenuItem23.Click
        Dim control_box_name As String
        Dim zhaoceobj As New 遥测窗口
        Dim controlboxobj As New control_box
        Dim rs As New ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim box_id As String = ""
        Dim imei As String = ""
        Dim conn As New ADODB.Connection
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        Dim time As Date
        msg = ""
        Dim control_string As String = ""
        time = Format(Now(), "yyyy-MM-dd HH:mm:ss")
        '查询灯的状态信息
        sql = "select * from control_box where  pos_x<= " & (m_rightbuttonpos.X - 1) & " and pos_x >= " & (m_rightbuttonpos.X - m_controllampobj.m_lampwide - 3) & " and pos_y <= " & (m_rightbuttonpos.Y + 1) & " and pos_y >= " & (m_rightbuttonpos.Y - m_controllampobj.m_lampheight - 1)
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            SetTextDelegate(MSG_ERROR_STRING & "打开ToolStripMenuItem23_Click" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)
            MsgBox("请选择主控箱重新尝试", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        Else
            If rs.RecordCount > 0 Then
                '找到某一盏灯的信息后发送单灯开命令
                control_box_name = Trim(rs.Fields("control_box_name").Value)
                controlboxobj.set_controlbox_list(zhaoceobj.tv_yaoce_controlbox) '主控箱信息列表
                controlboxobj.Dispose()
                zhaoceobj.m_checklist.Clear()
                zhaoceobj.m_checklist.Add(control_box_name)
                Com_inf.Addcontrolbox_to_Datagridview(zhaoceobj.m_checklist, zhaoceobj.dgv_yaoce_list, "yaoce_boxname")
                zhaoceobj.BackgroundWorker_yaoce.RunWorkerAsync()
                '招测的时候有些按钮变灰
                zhaoceobj.bt_add_boxname.Enabled = False
                zhaoceobj.bt_del_boxname.Enabled = False
                zhaoceobj.bt_yaoce.Enabled = False
                zhaoceobj.tv_yaoce_controlbox.ExpandAll()
                FindNode(zhaoceobj.tv_yaoce_controlbox.Nodes(0), control_box_name)
                zhaoceobj.ShowDialog()
            End If
        End If
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    Private Sub 开关量输出K1开灯ToolStripMenuItem3_Click(sender As System.Object, e As System.EventArgs) Handles 开关量输出K1开灯ToolStripMenuItem1.Click
        Dim control_box_name As String
        Dim control_box_id As String
        Dim lamp_id As String
        Dim msg As String = ""
        Dim sql As String = ""
        Dim box_id As String = ""
        Dim imei As String = ""
        Dim conn As New ADODB.Connection
        Dim rs As New ADODB.Recordset
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        sql = "select * from control_box where  pos_x<= " & (m_rightbuttonpos.X - 1) & " and pos_x >= " & (m_rightbuttonpos.X - m_controllampobj.m_lampwide - 3) & " and pos_y <= " & (m_rightbuttonpos.Y + 1) & " and pos_y >= " & (m_rightbuttonpos.Y - m_controllampobj.m_lampheight - 1)
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            SetTextDelegate(MSG_ERROR_STRING & "打开ToolStripMenuItem23_Click" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)
            MsgBox("请选择主控箱重新尝试", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        Else
            If rs.RecordCount > 0 Then
                '找到某一盏灯的信息后发送单灯开命令
                control_box_name = Trim(rs.Fields("control_box_name").Value)
                control_box_id = Com_inf.get_box_id(control_box_name)
                While control_box_id.Length < 4
                    control_box_id = "0" & control_box_id
                End While
                lamp_id = control_box_id & "3100001"
                open_close_single_lamp(lamp_id, 1, 1, 1)
                MsgBox("操作执行完毕", , PROJECT_TITLE_STRING)
            End If
        End If
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    Private Sub 开关量输出K2开灯ToolStripMenuItem2_Click(sender As System.Object, e As System.EventArgs) Handles 开关量输出K2开灯ToolStripMenuItem2.Click
        Dim control_box_name As String
        Dim control_box_id As String
        Dim lamp_id As String
        Dim msg As String = ""
        Dim sql As String = ""
        Dim box_id As String = ""
        Dim imei As String = ""
        Dim conn As New ADODB.Connection
        Dim rs As New ADODB.Recordset
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        sql = "select * from control_box where  pos_x<= " & (m_rightbuttonpos.X - 1) & " and pos_x >= " & (m_rightbuttonpos.X - m_controllampobj.m_lampwide - 3) & " and pos_y <= " & (m_rightbuttonpos.Y + 1) & " and pos_y >= " & (m_rightbuttonpos.Y - m_controllampobj.m_lampheight - 1)
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            SetTextDelegate(MSG_ERROR_STRING & "打开ToolStripMenuItem23_Click" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)
            MsgBox("请选择主控箱重新尝试", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        Else
            If rs.RecordCount > 0 Then
                '找到某一盏灯的信息后发送单灯开命令
                control_box_name = Trim(rs.Fields("control_box_name").Value)
                control_box_id = Com_inf.get_box_id(control_box_name)
                While control_box_id.Length < 4
                    control_box_id = "0" & control_box_id
                End While
                lamp_id = control_box_id & "3100002"
                open_close_single_lamp(lamp_id, 1, 1, 1)
                MsgBox("操作执行完毕", , PROJECT_TITLE_STRING)
            End If
        End If
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    Private Sub 开关量输出K3开灯ToolStripMenuItem3_Click(sender As System.Object, e As System.EventArgs) Handles 开关量输出K3开灯ToolStripMenuItem3.Click
        Dim control_box_name As String
        Dim control_box_id As String
        Dim lamp_id As String
        Dim msg As String = ""
        Dim sql As String = ""
        Dim box_id As String = ""
        Dim imei As String = ""
        Dim conn As New ADODB.Connection
        Dim rs As New ADODB.Recordset
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        sql = "select * from control_box where  pos_x<= " & (m_rightbuttonpos.X - 1) & " and pos_x >= " & (m_rightbuttonpos.X - m_controllampobj.m_lampwide - 3) & " and pos_y <= " & (m_rightbuttonpos.Y + 1) & " and pos_y >= " & (m_rightbuttonpos.Y - m_controllampobj.m_lampheight - 1)
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            SetTextDelegate(MSG_ERROR_STRING & "打开ToolStripMenuItem23_Click" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)
            MsgBox("请选择主控箱重新尝试", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        Else
            If rs.RecordCount > 0 Then
                '找到某一盏灯的信息后发送单灯开命令
                control_box_name = Trim(rs.Fields("control_box_name").Value)
                control_box_id = Com_inf.get_box_id(control_box_name)
                While control_box_id.Length < 4
                    control_box_id = "0" & control_box_id
                End While
                lamp_id = control_box_id & "3100003"
                open_close_single_lamp(lamp_id, 1, 1, 1)
                MsgBox("操作执行完毕", , PROJECT_TITLE_STRING)
            End If
        End If
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    Private Sub 开关量输出K4开灯ToolStripMenuItem4_Click(sender As System.Object, e As System.EventArgs) Handles 开关量输出K4开灯ToolStripMenuItem4.Click
        Dim control_box_name As String
        Dim control_box_id As String
        Dim lamp_id As String
        Dim msg As String = ""
        Dim sql As String = ""
        Dim box_id As String = ""
        Dim imei As String = ""
        Dim conn As New ADODB.Connection
        Dim rs As New ADODB.Recordset
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        sql = "select * from control_box where  pos_x<= " & (m_rightbuttonpos.X - 1) & " and pos_x >= " & (m_rightbuttonpos.X - m_controllampobj.m_lampwide - 3) & " and pos_y <= " & (m_rightbuttonpos.Y + 1) & " and pos_y >= " & (m_rightbuttonpos.Y - m_controllampobj.m_lampheight - 1)
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            SetTextDelegate(MSG_ERROR_STRING & "打开ToolStripMenuItem23_Click" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)
            MsgBox("请选择主控箱重新尝试", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        Else
            If rs.RecordCount > 0 Then
                '找到某一盏灯的信息后发送单灯开命令
                control_box_name = Trim(rs.Fields("control_box_name").Value)
                control_box_id = Com_inf.get_box_id(control_box_name)
                While control_box_id.Length < 4
                    control_box_id = "0" & control_box_id
                End While
                lamp_id = control_box_id & "3100004"
                open_close_single_lamp(lamp_id, 1, 1, 1)
                MsgBox("操作执行完毕", , PROJECT_TITLE_STRING)
            End If
        End If
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    Private Sub 开关量输出K5开灯ToolStripMenuItem5_Click(sender As System.Object, e As System.EventArgs) Handles 开关量输出K5开灯ToolStripMenuItem5.Click
        Dim control_box_name As String
        Dim control_box_id As String
        Dim lamp_id As String
        Dim msg As String = ""
        Dim sql As String = ""
        Dim box_id As String = ""
        Dim imei As String = ""
        Dim conn As New ADODB.Connection
        Dim rs As New ADODB.Recordset
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        sql = "select * from control_box where  pos_x<= " & (m_rightbuttonpos.X - 1) & " and pos_x >= " & (m_rightbuttonpos.X - m_controllampobj.m_lampwide - 3) & " and pos_y <= " & (m_rightbuttonpos.Y + 1) & " and pos_y >= " & (m_rightbuttonpos.Y - m_controllampobj.m_lampheight - 1)
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            SetTextDelegate(MSG_ERROR_STRING & "打开ToolStripMenuItem23_Click" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)
            MsgBox("请选择主控箱重新尝试", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        Else
            If rs.RecordCount > 0 Then
                '找到某一盏灯的信息后发送单灯开命令
                control_box_name = Trim(rs.Fields("control_box_name").Value)
                control_box_id = Com_inf.get_box_id(control_box_name)
                While control_box_id.Length < 4
                    control_box_id = "0" & control_box_id
                End While
                lamp_id = control_box_id & "3100005"
                open_close_single_lamp(lamp_id, 1, 1, 1)
                MsgBox("操作执行完毕", , PROJECT_TITLE_STRING)
            End If
        End If
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    Private Sub 开关量输出K6开灯ToolStripMenuItem6_Click(sender As System.Object, e As System.EventArgs) Handles 开关量输出K6开灯ToolStripMenuItem6.Click
        Dim control_box_name As String
        Dim control_box_id As String
        Dim lamp_id As String
        Dim msg As String = ""
        Dim sql As String = ""
        Dim box_id As String = ""
        Dim imei As String = ""
        Dim conn As New ADODB.Connection
        Dim rs As New ADODB.Recordset
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        sql = "select * from control_box where  pos_x<= " & (m_rightbuttonpos.X - 1) & " and pos_x >= " & (m_rightbuttonpos.X - m_controllampobj.m_lampwide - 3) & " and pos_y <= " & (m_rightbuttonpos.Y + 1) & " and pos_y >= " & (m_rightbuttonpos.Y - m_controllampobj.m_lampheight - 1)
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            SetTextDelegate(MSG_ERROR_STRING & "打开ToolStripMenuItem23_Click" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)
            MsgBox("请选择主控箱重新尝试", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        Else
            If rs.RecordCount > 0 Then
                '找到某一盏灯的信息后发送单灯开命令
                control_box_name = Trim(rs.Fields("control_box_name").Value)
                control_box_id = Com_inf.get_box_id(control_box_name)
                While control_box_id.Length < 4
                    control_box_id = "0" & control_box_id
                End While
                lamp_id = control_box_id & "3100006"
                open_close_single_lamp(lamp_id, 1, 1, 1)
                MsgBox("操作执行完毕", , PROJECT_TITLE_STRING)
            End If
        End If
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    Private Sub 开启所有回路ToolStripMenuItem9_Click(sender As System.Object, e As System.EventArgs) Handles 开启所有回路ToolStripMenuItem9.Click
        Dim control_box_name As String
        Dim control_lamp_obj As New control_lamp
        Dim m_huilu, m_lamp As String  '控制的类型名称
        Dim control_method As String = "全开"
        Dim hand_type As String = "主控箱"
        Dim m_result As Boolean '发送命令结果
        m_huilu = Com_inf.Get_Type_String(31)
        m_lamp = Com_inf.Get_Type_String(0)
        Dim msg As String = ""
        Dim sql As String = ""
        Dim box_id As String = ""
        Dim imei As String = ""
        Dim conn As New ADODB.Connection
        Dim rs As New ADODB.Recordset
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        sql = "select * from control_box where  pos_x<= " & (m_rightbuttonpos.X - 1) & " and pos_x >= " & (m_rightbuttonpos.X - m_controllampobj.m_lampwide - 3) & " and pos_y <= " & (m_rightbuttonpos.Y + 1) & " and pos_y >= " & (m_rightbuttonpos.Y - m_controllampobj.m_lampheight - 1)
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            SetTextDelegate(MSG_ERROR_STRING & "打开ToolStripMenuItem23_Click" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)
            MsgBox("请选择主控箱重新尝试", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        Else
            If rs.RecordCount > 0 Then
                '找到某一盏灯的信息后发送单灯开命令
                control_box_name = Trim(rs.Fields("control_box_name").Value)
                m_result = control_lamp_obj.Input_control_inf(m_huilu, "类型", control_box_name, "类型全开", 1, "全功率", 100, hand_type, 0)
                MsgBox("操作执行完毕", , PROJECT_TITLE_STRING)
            End If
        End If
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    Private Sub 开关量输出K1关灯ToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles 开关量输出K1关灯ToolStripMenuItem1.Click
        Dim control_box_name As String
        Dim control_box_id As String
        Dim lamp_id As String
        Dim msg As String = ""
        Dim sql As String = ""
        Dim box_id As String = ""
        Dim imei As String = ""
        Dim conn As New ADODB.Connection
        Dim rs As New ADODB.Recordset
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        sql = "select * from control_box where  pos_x<= " & (m_rightbuttonpos.X - 1) & " and pos_x >= " & (m_rightbuttonpos.X - m_controllampobj.m_lampwide - 3) & " and pos_y <= " & (m_rightbuttonpos.Y + 1) & " and pos_y >= " & (m_rightbuttonpos.Y - m_controllampobj.m_lampheight - 1)
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            SetTextDelegate(MSG_ERROR_STRING & "打开ToolStripMenuItem23_Click" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)
            MsgBox("请选择主控箱重新尝试", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        Else
            If rs.RecordCount > 0 Then
                '找到某一盏灯的信息后发送单灯开命令
                control_box_name = Trim(rs.Fields("control_box_name").Value)
                control_box_id = Com_inf.get_box_id(control_box_name)
                While control_box_id.Length < 4
                    control_box_id = "0" & control_box_id
                End While
                lamp_id = control_box_id & "3100001"
                open_close_single_lamp(lamp_id, 0, 1, 1)
                MsgBox("操作执行完毕", , PROJECT_TITLE_STRING)
            End If
        End If
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    Private Sub 开关量输出K2关灯ToolStripMenuItem2_Click(sender As System.Object, e As System.EventArgs) Handles 开关量输出K2关灯ToolStripMenuItem2.Click
        Dim control_box_name As String
        Dim control_box_id As String
        Dim lamp_id As String
        Dim msg As String = ""
        Dim sql As String = ""
        Dim box_id As String = ""
        Dim imei As String = ""
        Dim conn As New ADODB.Connection
        Dim rs As New ADODB.Recordset
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        sql = "select * from control_box where  pos_x<= " & (m_rightbuttonpos.X - 1) & " and pos_x >= " & (m_rightbuttonpos.X - m_controllampobj.m_lampwide - 3) & " and pos_y <= " & (m_rightbuttonpos.Y + 1) & " and pos_y >= " & (m_rightbuttonpos.Y - m_controllampobj.m_lampheight - 1)
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            SetTextDelegate(MSG_ERROR_STRING & "打开ToolStripMenuItem23_Click" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)
            MsgBox("请选择主控箱重新尝试", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        Else
            If rs.RecordCount > 0 Then
                '找到某一盏灯的信息后发送单灯开命令
                control_box_name = Trim(rs.Fields("control_box_name").Value)
                control_box_id = Com_inf.get_box_id(control_box_name)
                While control_box_id.Length < 4
                    control_box_id = "0" & control_box_id
                End While
                lamp_id = control_box_id & "3100002"
                open_close_single_lamp(lamp_id, 0, 1, 1)
                MsgBox("操作执行完毕", , PROJECT_TITLE_STRING)
            End If
        End If
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    Private Sub 开关量输出K3关灯ToolStripMenuItem3_Click(sender As System.Object, e As System.EventArgs) Handles 开关量输出K3关灯ToolStripMenuItem3.Click
        Dim control_box_name As String
        Dim control_box_id As String
        Dim lamp_id As String
        Dim msg As String = ""
        Dim sql As String = ""
        Dim box_id As String = ""
        Dim imei As String = ""
        Dim conn As New ADODB.Connection
        Dim rs As New ADODB.Recordset
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        sql = "select * from control_box where  pos_x<= " & (m_rightbuttonpos.X - 1) & " and pos_x >= " & (m_rightbuttonpos.X - m_controllampobj.m_lampwide - 3) & " and pos_y <= " & (m_rightbuttonpos.Y + 1) & " and pos_y >= " & (m_rightbuttonpos.Y - m_controllampobj.m_lampheight - 1)
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            SetTextDelegate(MSG_ERROR_STRING & "打开ToolStripMenuItem23_Click" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)
            MsgBox("请选择主控箱重新尝试", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        Else
            If rs.RecordCount > 0 Then
                '找到某一盏灯的信息后发送单灯开命令
                control_box_name = Trim(rs.Fields("control_box_name").Value)
                control_box_id = Com_inf.get_box_id(control_box_name)
                While control_box_id.Length < 4
                    control_box_id = "0" & control_box_id
                End While
                lamp_id = control_box_id & "3100003"
                open_close_single_lamp(lamp_id, 0, 1, 1)
                MsgBox("操作执行完毕", , PROJECT_TITLE_STRING)
            End If
        End If
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    Private Sub 开关量输出K4关灯ToolStripMenuItem4_Click(sender As System.Object, e As System.EventArgs) Handles 开关量输出K4关灯ToolStripMenuItem4.Click
        Dim control_box_name As String
        Dim control_box_id As String
        Dim lamp_id As String
        Dim msg As String = ""
        Dim sql As String = ""
        Dim box_id As String = ""
        Dim imei As String = ""
        Dim conn As New ADODB.Connection
        Dim rs As New ADODB.Recordset
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        sql = "select * from control_box where  pos_x<= " & (m_rightbuttonpos.X - 1) & " and pos_x >= " & (m_rightbuttonpos.X - m_controllampobj.m_lampwide - 3) & " and pos_y <= " & (m_rightbuttonpos.Y + 1) & " and pos_y >= " & (m_rightbuttonpos.Y - m_controllampobj.m_lampheight - 1)
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            SetTextDelegate(MSG_ERROR_STRING & "打开ToolStripMenuItem23_Click" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)
            MsgBox("请选择主控箱重新尝试", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        Else
            If rs.RecordCount > 0 Then
                '找到某一盏灯的信息后发送单灯开命令
                control_box_name = Trim(rs.Fields("control_box_name").Value)
                control_box_id = Com_inf.get_box_id(control_box_name)
                While control_box_id.Length < 4
                    control_box_id = "0" & control_box_id
                End While
                lamp_id = control_box_id & "3100004"
                open_close_single_lamp(lamp_id, 0, 1, 1)
                MsgBox("操作执行完毕", , PROJECT_TITLE_STRING)
            End If
        End If
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    Private Sub 开关量输出K5关灯ToolStripMenuItem5_Click(sender As System.Object, e As System.EventArgs) Handles 开关量输出K5关灯ToolStripMenuItem5.Click
        Dim control_box_name As String
        Dim control_box_id As String
        Dim lamp_id As String
        Dim msg As String = ""
        Dim sql As String = ""
        Dim box_id As String = ""
        Dim imei As String = ""
        Dim conn As New ADODB.Connection
        Dim rs As New ADODB.Recordset
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        sql = "select * from control_box where  pos_x<= " & (m_rightbuttonpos.X - 1) & " and pos_x >= " & (m_rightbuttonpos.X - m_controllampobj.m_lampwide - 3) & " and pos_y <= " & (m_rightbuttonpos.Y + 1) & " and pos_y >= " & (m_rightbuttonpos.Y - m_controllampobj.m_lampheight - 1)
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            SetTextDelegate(MSG_ERROR_STRING & "打开ToolStripMenuItem23_Click" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)
            MsgBox("请选择主控箱重新尝试", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        Else
            If rs.RecordCount > 0 Then
                '找到某一盏灯的信息后发送单灯开命令
                control_box_name = Trim(rs.Fields("control_box_name").Value)
                control_box_id = Com_inf.get_box_id(control_box_name)
                While control_box_id.Length < 4
                    control_box_id = "0" & control_box_id
                End While
                lamp_id = control_box_id & "3100005"
                open_close_single_lamp(lamp_id, 0, 1, 1)
                MsgBox("操作执行完毕", , PROJECT_TITLE_STRING)
            End If
        End If
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    Private Sub 开关量输出K6关灯ToolStripMenuItem6_Click(sender As System.Object, e As System.EventArgs) Handles 开关量输出K6关灯ToolStripMenuItem6.Click
        Dim control_box_name As String
        Dim control_box_id As String
        Dim lamp_id As String
        Dim msg As String = ""
        Dim sql As String = ""
        Dim box_id As String = ""
        Dim imei As String = ""
        Dim conn As New ADODB.Connection
        Dim rs As New ADODB.Recordset
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        sql = "select * from control_box where  pos_x<= " & (m_rightbuttonpos.X - 1) & " and pos_x >= " & (m_rightbuttonpos.X - m_controllampobj.m_lampwide - 3) & " and pos_y <= " & (m_rightbuttonpos.Y + 1) & " and pos_y >= " & (m_rightbuttonpos.Y - m_controllampobj.m_lampheight - 1)
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            SetTextDelegate(MSG_ERROR_STRING & "打开ToolStripMenuItem23_Click" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)
            MsgBox("请选择主控箱重新尝试", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        Else
            If rs.RecordCount > 0 Then
                '找到某一盏灯的信息后发送单灯开命令
                control_box_name = Trim(rs.Fields("control_box_name").Value)
                control_box_id = Com_inf.get_box_id(control_box_name)
                While control_box_id.Length < 4
                    control_box_id = "0" & control_box_id
                End While
                lamp_id = control_box_id & "3100006"
                open_close_single_lamp(lamp_id, 0, 1, 1)
                MsgBox("操作执行完毕", , PROJECT_TITLE_STRING)
            End If
        End If
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    Private Sub 关闭所有回路ToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles 关闭所有回路ToolStripMenuItem1.Click
        Dim control_box_name As String
        Dim control_lamp_obj As New control_lamp
        Dim m_huilu, m_lamp As String  '控制的类型名称
        Dim control_method As String = "全闭"
        Dim hand_type As String = "主控箱"
        Dim m_result As Boolean '发送命令结果
        m_huilu = Com_inf.Get_Type_String(31)
        m_lamp = Com_inf.Get_Type_String(0)
        Dim msg As String = ""
        Dim sql As String = ""
        Dim box_id As String = ""
        Dim imei As String = ""
        Dim conn As New ADODB.Connection
        Dim rs As New ADODB.Recordset
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        sql = "select * from control_box where  pos_x<= " & (m_rightbuttonpos.X - 1) & " and pos_x >= " & (m_rightbuttonpos.X - m_controllampobj.m_lampwide - 3) & " and pos_y <= " & (m_rightbuttonpos.Y + 1) & " and pos_y >= " & (m_rightbuttonpos.Y - m_controllampobj.m_lampheight - 1)
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            SetTextDelegate(MSG_ERROR_STRING & "打开ToolStripMenuItem23_Click" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)
            MsgBox("请选择主控箱重新尝试", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        Else
            If rs.RecordCount > 0 Then
                '找到某一盏灯的信息后发送单灯开命令
                control_box_name = Trim(rs.Fields("control_box_name").Value)
                m_result = control_lamp_obj.Input_control_inf(m_huilu, "类型", control_box_name, "类型全闭", 0, "关闭灯", 100, hand_type, 0)
                MsgBox("操作执行完毕", , PROJECT_TITLE_STRING)
            End If
        End If
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    Private Sub 单灯数据ToolStripMenuItem18_Click(sender As System.Object, e As System.EventArgs) Handles 单灯数据ToolStripMenuItem18.Click
        Dim control_box_name As String
        Dim msg As String = ""
        Dim sql As String = ""
        Dim box_id As String = ""
        Dim imei As String = ""
        Dim conn As New ADODB.Connection
        Dim rs As New ADODB.Recordset
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        sql = "select * from control_box where  pos_x<= " & (m_rightbuttonpos.X - 1) & " and pos_x >= " & (m_rightbuttonpos.X - m_controllampobj.m_lampwide - 3) & " and pos_y <= " & (m_rightbuttonpos.Y + 1) & " and pos_y >= " & (m_rightbuttonpos.Y - m_controllampobj.m_lampheight - 1)
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            SetTextDelegate(MSG_ERROR_STRING & "打开ToolStripMenuItem23_Click" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)
            MsgBox("请选择主控箱重新尝试", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        Else
            If rs.RecordCount > 0 Then
                '找到某一盏灯的信息后发送单灯开命令
                control_box_name = Trim(rs.Fields("control_box_name").Value)
                get_lamp_dandeng(control_box_name)
            End If
        End If
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    Private Sub 终端参数设置ToolStripMenuItem19_Click(sender As System.Object, e As System.EventArgs) Handles 终端参数设置ToolStripMenuItem19.Click
        Dim control_box_name As String
        Dim zhaoceobj As New 增加终端
        Dim msg As String = ""
        Dim sql As String = ""
        Dim box_id As String = ""
        Dim imei As String = ""
        Dim conn As New ADODB.Connection
        Dim rs As New ADODB.Recordset
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        sql = "select * from control_box where  pos_x<= " & (m_rightbuttonpos.X - 1) & " and pos_x >= " & (m_rightbuttonpos.X - m_controllampobj.m_lampwide - 3) & " and pos_y <= " & (m_rightbuttonpos.Y + 1) & " and pos_y >= " & (m_rightbuttonpos.Y - m_controllampobj.m_lampheight - 1)
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            SetTextDelegate(MSG_ERROR_STRING & "打开ToolStripMenuItem23_Click" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)
            MsgBox("请选择主控箱重新尝试", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        Else
            If rs.RecordCount > 0 Then
                '找到某一盏灯的信息后发送单灯开命令
                control_box_name = Trim(rs.Fields("control_box_name").Value)
                'zhaoceobj.zhaoceobj_city_area_street(rs.Fields("city").Value, rs.Fields("area").Value, rs.Fields("street").Value, control_box_name)
                zhaoceobj.Show()
            End If
        End If
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    Private Sub 终端当前故障ToolStripMenuItem21_Click(sender As System.Object, e As System.EventArgs) Handles 终端当前故障ToolStripMenuItem21.Click
        'Dim control_box_name As String
        'Dim zhaoceobj As New 故障报警窗口
        'Dim msg As String = ""
        'Dim sql As String = ""
        'Dim box_id As String = ""
        'Dim imei As String = ""
        'Dim conn As New ADODB.Connection
        'Dim rs As New ADODB.Recordset
        'If DBOperation.OpenConn(conn) = False Then
        '    Exit Sub
        'End If
        'sql = "select * from control_box where  pos_x<= " & (m_rightbuttonpos.X - 1) & " and pos_x >= " & (m_rightbuttonpos.X - m_controllampobj.m_lampwide - 3) & " and pos_y <= " & (m_rightbuttonpos.Y + 1) & " and pos_y >= " & (m_rightbuttonpos.Y - m_controllampobj.m_lampheight - 1)
        'rs = DBOperation.SelectSQL(conn, sql, msg)
        'If rs Is Nothing Then
        '    SetTextDelegate(MSG_ERROR_STRING & "打开ToolStripMenuItem23_Click" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)
        '    MsgBox("请选择主控箱重新尝试", , PROJECT_TITLE_STRING)
        '    conn.Close()
        '    conn = Nothing
        '    Exit Sub
        'Else
        '    If rs.RecordCount > 0 Then
        '        '找到某一盏灯的信息后发送单灯开命令
        '        control_box_name = Trim(rs.Fields("control_box_name").Value)
        '        lamp_alarm_listshow(control_box_name)
        '        zhaoceobj.Show()
        '    End If
        'End If
        'If rs.State = 1 Then
        '    rs.Close()
        '    rs = Nothing
        'End If
        'conn.Close()
        'conn = Nothing

        If g_lampalarm_show = False Then
            g_lampalarm_show = True
            故障报警窗口ToolStripMenuItem.Image = System.Drawing.Image.FromFile("图片\dis.png")
            终端当前故障ToolStripMenuItem21.Image = System.Drawing.Image.FromFile("图片\dis.png")
        Else
            故障报警窗口ToolStripMenuItem.Image = System.Drawing.Image.FromFile("图片\静音.jpg")
            终端当前故障ToolStripMenuItem21.Image = System.Drawing.Image.FromFile("图片\静音.jpg")
            g_lampalarm_show = False
        End If

    End Sub

    Private Sub 终端历史数据ToolStripMenuItem22_Click(sender As System.Object, e As System.EventArgs) Handles 终端历史数据ToolStripMenuItem22.Click
        Dim control_box_name As String
        Dim zhaoceobj As New 终端亮暗信息统计
        Dim msg As String = ""
        Dim sql As String = ""
        Dim box_id As String = ""
        Dim imei As String = ""
        Dim conn As New ADODB.Connection
        Dim rs As New ADODB.Recordset
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        sql = "select * from control_box where  pos_x<= " & (m_rightbuttonpos.X - 1) & " and pos_x >= " & (m_rightbuttonpos.X - m_controllampobj.m_lampwide - 3) & " and pos_y <= " & (m_rightbuttonpos.Y + 1) & " and pos_y >= " & (m_rightbuttonpos.Y - m_controllampobj.m_lampheight - 1)
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            SetTextDelegate(MSG_ERROR_STRING & "打开ToolStripMenuItem23_Click" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)
            MsgBox("请选择主控箱重新尝试", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        Else
            If rs.RecordCount > 0 Then
                '找到某一盏灯的信息后发送单灯开命令
                control_box_name = Trim(rs.Fields("control_box_name").Value)
                zhaoceobj.ShowDialog()
            End If
        End If
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub


    Private Sub 单灯召测ToolStripMenuItem2_Click(sender As System.Object, e As System.EventArgs) Handles 单灯召测ToolStripMenuItem2.Click
        Dim zhaoceobj As New 单灯召测
        Dim controlboxobj As New control_box
        Dim control_box_name As String
        Dim msg As String = ""
        Dim sql As String = ""
        Dim box_id As String = ""
        Dim imei As String = ""
        Dim conn As New ADODB.Connection
        Dim rs As New ADODB.Recordset
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        sql = "select * from control_box where  pos_x<= " & (m_rightbuttonpos.X - 1) & " and pos_x >= " & (m_rightbuttonpos.X - m_controllampobj.m_lampwide - 3) & " and pos_y <= " & (m_rightbuttonpos.Y + 1) & " and pos_y >= " & (m_rightbuttonpos.Y - m_controllampobj.m_lampheight - 1)
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            SetTextDelegate(MSG_ERROR_STRING & "打开ToolStripMenuItem23_Click" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)
            MsgBox("请选择主控箱重新尝试", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        Else
            If rs.RecordCount > 0 Then
                '找到某一盏灯的信息后发送单灯开命令
                control_box_name = Trim(rs.Fields("control_box_name").Value)
                controlboxobj.set_controlbox_list(zhaoceobj.tv_yaoce_controlbox) '主控箱信息列表
                controlboxobj.Dispose()
                zhaoceobj.m_checklist.Clear()
                zhaoceobj.m_checklist.Add(control_box_name)
                Com_inf.Addcontrolbox_to_Datagridview(zhaoceobj.m_checklist, zhaoceobj.dgv_yaoce_list, "yaoce_boxname")
                zhaoceobj.BackgroundWorker_yaoce.RunWorkerAsync()
                '招测的时候有些按钮变灰
                zhaoceobj.bt_add_boxname.Enabled = False
                zhaoceobj.bt_del_boxname.Enabled = False
                zhaoceobj.bt_yaoce.Enabled = False
                zhaoceobj.tv_yaoce_controlbox.ExpandAll()
                FindNode(zhaoceobj.tv_yaoce_controlbox.Nodes(0), control_box_name)
                zhaoceobj.ShowDialog()
            End If
        End If
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub


    Private Sub 时段控制设置ToolStripMenuItem20_Click(sender As System.Object, e As System.EventArgs) Handles 时段控制设置ToolStripMenuItem20.Click
        Dim zhaoceobj As New 经纬度
        Dim control_box_name As String
        Dim controlboxobj As New control_box
        Dim msg As String = ""
        Dim sql As String = ""
        Dim box_id As String = ""
        Dim imei As String = ""
        Dim conn As New ADODB.Connection
        Dim rs As New ADODB.Recordset
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        sql = "select * from control_box where  pos_x<= " & (m_rightbuttonpos.X - 1) & " and pos_x >= " & (m_rightbuttonpos.X - m_controllampobj.m_lampwide - 3) & " and pos_y <= " & (m_rightbuttonpos.Y + 1) & " and pos_y >= " & (m_rightbuttonpos.Y - m_controllampobj.m_lampheight - 1)
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            SetTextDelegate(MSG_ERROR_STRING & "打开ToolStripMenuItem23_Click" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)
            MsgBox("请选择主控箱重新尝试", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        Else
            If rs.RecordCount > 0 Then
                '找到某一盏灯的信息后发送单灯开命令
                control_box_name = Trim(rs.Fields("control_box_name").Value)
                m_controlboxobj.set_controlbox_list(zhaoceobj.tv_box_inf_list) '主控箱信息列表
                m_controlboxobj.set_controlbox_list(zhaoceobj.tv_all_controlbox) '主控箱信息列表
                m_controlboxobj.set_controlbox_list(zhaoceobj.tv_divtime_controlbox) '自动控制模式中的主控箱信息列表
                m_controlboxobj.set_controlbox_list(zhaoceobj.tv_banye_controlbox)  '半夜灯控制
                m_controlboxobj.set_controlbox_list(zhaoceobj.tv_holidaydivtime_box) '节假日控制
                controlboxobj.Dispose()
                zhaoceobj.tv_all_controlbox.ExpandAll()
                zhaoceobj.tv_box_inf_list.ExpandAll()
                zhaoceobj.tv_divtime_controlbox.ExpandAll()
                zhaoceobj.tv_banye_controlbox.ExpandAll()
                zhaoceobj.tv_holidaydivtime_box.ExpandAll()
                FindNode(zhaoceobj.tv_box_inf_list.Nodes(0), control_box_name)
                FindNode(zhaoceobj.tv_all_controlbox.Nodes(0), control_box_name)
                FindNode(zhaoceobj.tv_divtime_controlbox.Nodes(0), control_box_name)
                FindNode(zhaoceobj.tv_banye_controlbox.Nodes(0), control_box_name)
                FindNode(zhaoceobj.tv_holidaydivtime_box.Nodes(0), control_box_name)
                zhaoceobj.ShowDialog()
            End If
        End If
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    Private Sub 时段控制设置ToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles 时段控制设置ToolStripMenuItem.Click
        Dim zhaoceobj As New 经纬度
        Dim control_box_name As String
        Dim controlboxobj As New control_box
        control_box_name = Trim(tv_box_inf_list.SelectedNode.Text).Split(" ")(0)
        m_controlboxobj.set_controlbox_list(zhaoceobj.tv_box_inf_list) '主控箱信息列表
        m_controlboxobj.set_controlbox_list(zhaoceobj.tv_all_controlbox) '主控箱信息列表
        m_controlboxobj.set_controlbox_list(zhaoceobj.tv_divtime_controlbox) '自动控制模式中的主控箱信息列表
        m_controlboxobj.set_controlbox_list(zhaoceobj.tv_banye_controlbox)  '半夜灯控制
        m_controlboxobj.set_controlbox_list(zhaoceobj.tv_holidaydivtime_box) '节假日控制
        controlboxobj.Dispose()
        zhaoceobj.tv_all_controlbox.ExpandAll()
        zhaoceobj.tv_box_inf_list.ExpandAll()
        zhaoceobj.tv_divtime_controlbox.ExpandAll()
        zhaoceobj.tv_banye_controlbox.ExpandAll()
        zhaoceobj.tv_holidaydivtime_box.ExpandAll()
        FindNode(zhaoceobj.tv_box_inf_list.Nodes(0), control_box_name)
        FindNode(zhaoceobj.tv_all_controlbox.Nodes(0), control_box_name)
        FindNode(zhaoceobj.tv_divtime_controlbox.Nodes(0), control_box_name)
        FindNode(zhaoceobj.tv_banye_controlbox.Nodes(0), control_box_name)
        FindNode(zhaoceobj.tv_holidaydivtime_box.Nodes(0), control_box_name)
        zhaoceobj.ShowDialog()
    End Sub

    Private Function FindNode(ByVal tnParent As TreeNode, ByVal strValue As String) As TreeNode
        FindNode = Nothing
        If tnParent Is Nothing Then
            FindNode = Nothing
            Exit Function
        End If
        If Trim(tnParent.Text).Split(" ")(0) = strValue Then
            tnParent.Checked = True
            FindNode = tnParent
            Exit Function
        End If
        Dim tnRet As TreeNode = Nothing
        Dim tn As TreeNode
        For Each tn In tnParent.Nodes
            tnRet = FindNode(tn, strValue)
            If tnRet IsNot Nothing Then
                FindNode = tnRet
                Exit Function
            End If
        Next
    End Function


    Private Sub 主控历史数据查询ToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles 主控历史数据查询ToolStripMenuItem.Click
        Dim box_inf_obj As New 主控箱数据
        Dim control_box_name As String
        Dim controlboxobj As New control_box
        control_box_name = Trim(tv_box_inf_list.SelectedNode.Text).Split(" ")(0)
        controlboxobj.set_controlbox_list(box_inf_obj.tv_yaoce_controlbox) '主控箱信息列表
        controlboxobj.set_controlbox_list(box_inf_obj.tv_boxlist_state) '状态查询中的主控箱列表
        controlboxobj.set_controlbox_list(box_inf_obj.tv_boxlist_kaiguan) '开关查询中的主控箱列表
        controlboxobj.Dispose()
        box_inf_obj.tv_yaoce_controlbox.ExpandAll()
        box_inf_obj.tv_boxlist_state.ExpandAll()
        box_inf_obj.tv_boxlist_kaiguan.ExpandAll()
        FindNode(box_inf_obj.tv_yaoce_controlbox.Nodes(0), control_box_name)
        FindNode(box_inf_obj.tv_boxlist_state.Nodes(0), control_box_name)
        FindNode(box_inf_obj.tv_boxlist_kaiguan.Nodes(0), control_box_name)
        box_inf_obj.ShowDialog()
    End Sub

    Private Sub 主控历史数据查询ToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles 主控历史数据查询ToolStripMenuItem1.Click
        Dim box_inf_obj As New 主控箱数据
        Dim control_box_name As String
        Dim controlboxobj As New control_box
        Dim msg As String = ""
        Dim sql As String = ""
        Dim box_id As String = ""
        Dim imei As String = ""
        Dim conn As New ADODB.Connection
        Dim rs As New ADODB.Recordset
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        sql = "select * from control_box where  pos_x<= " & (m_rightbuttonpos.X - 1) & " and pos_x >= " & (m_rightbuttonpos.X - m_controllampobj.m_lampwide - 3) & " and pos_y <= " & (m_rightbuttonpos.Y + 1) & " and pos_y >= " & (m_rightbuttonpos.Y - m_controllampobj.m_lampheight - 1)
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            SetTextDelegate(MSG_ERROR_STRING & "打开ToolStripMenuItem23_Click" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, rtb_info_list)
            MsgBox("请选择主控箱重新尝试", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        Else
            If rs.RecordCount > 0 Then
                '找到某一盏灯的信息后发送单灯开命令
                control_box_name = Trim(rs.Fields("control_box_name").Value)
                control_box_name = Trim(tv_box_inf_list.SelectedNode.Text).Split(" ")(0)
                controlboxobj.set_controlbox_list(box_inf_obj.tv_yaoce_controlbox) '主控箱信息列表
                controlboxobj.set_controlbox_list(box_inf_obj.tv_boxlist_state) '状态查询中的主控箱列表
                controlboxobj.set_controlbox_list(box_inf_obj.tv_boxlist_kaiguan) '开关查询中的主控箱列表
                controlboxobj.Dispose()
                box_inf_obj.tv_yaoce_controlbox.ExpandAll()
                box_inf_obj.tv_boxlist_state.ExpandAll()
                box_inf_obj.tv_boxlist_kaiguan.ExpandAll()
                FindNode(box_inf_obj.tv_yaoce_controlbox.Nodes(0), control_box_name)
                FindNode(box_inf_obj.tv_boxlist_state.Nodes(0), control_box_name)
                FindNode(box_inf_obj.tv_boxlist_kaiguan.Nodes(0), control_box_name)
                box_inf_obj.ShowDialog()
            End If
        End If
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub
End Class