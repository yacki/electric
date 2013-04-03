Partial Public Class welcome_win
    '主控窗体的菜单部分
#Region "系统"

    ''' <summary>
    ''' 城区街设置
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 城区街设置ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 城区街设置ToolStripMenuItem.Click
        'If GetInstanceState("城区街设置") Then
        '    Exit Sub
        'End If
        '城区街设置.MdiParent = Me
        Dim city_obj As New 城区街设置
        city_obj.ShowDialog()
    End Sub

    ''' <summary>
    ''' 打开增加节日模式窗口
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 增加ToolStripMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 增加ToolStripMenuItem4.Click
        '用户可以自定义一个节日的工作模式
        'If GetInstanceState("增加节日模式") Then
        '    Exit Sub
        'End If
        '增加节日模式.MdiParent = Me
        Dim holiday_mod As New 增加节日模式
        holiday_mod.ShowDialog()

    End Sub

    ''' <summary>
    ''' 打开删除节日模式窗口
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 删除ToolStripMenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 删除ToolStripMenuItem5.Click
        'If GetInstanceState("删除节日模式") Then
        '    Exit Sub
        'End If
        '删除节日模式.MdiParent = Me
        Dim del_holiday_mod As New 删除节日模式
        del_holiday_mod.ShowDialog()
    End Sub

    ''' <summary>
    ''' 增加定位区域的函数
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 增加ToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 增加ToolStripMenuItem.Click
        'If GetInstanceState("增加查询街道") Then
        '    Exit Sub
        'End If
        '增加查询街道.MdiParent = Me
        m_addstreetobj = New 增加查询街道
        m_addstreetobj.ShowDialog()
    End Sub

    ''' <summary>
    ''' 删除定位的区域
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 删除ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 删除ToolStripMenuItem.Click
        'If GetInstanceState("删除查询区域") Then
        '    Exit Sub
        'End If
        '删除查询区域.MdiParent = Me
        Dim delete_area_obj As New 删除查询区域
        delete_area_obj.ShowDialog()
    End Sub

    '''' <summary>
    '''' 主控箱参数设置
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    '''' <remarks></remarks>
    'Private Sub ToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem2.Click
    '    Dim box_config_obj As New 主控箱参数设置
    '    box_config_obj.Show()
    'End Sub

    '''' <summary>
    '''' 设置控制终端亮暗的参数，即用电流判断还是电阻判断，及判断值
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    '''' <remarks></remarks>
    'Private Sub 终端ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 终端ToolStripMenuItem.Click
    '    Dim control_obj As New 终端开关控制参数
    '    control_obj.Show()
    'End Sub

    ''' <summary>
    ''' 配置系统的电流、电压、标题、缩放比例等相关配置参数
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 系统参数设置ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 系统参数设置ToolStripMenuItem.Click
        'If GetInstanceState("系统参数设置") Then
        '    Exit Sub
        'End If
        '系统参数设置.MdiParent = Me
        Dim control_obj As New 系统参数设置
        control_obj.ShowDialog()
    End Sub

    ''' <summary>
    ''' 清空数据库
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 清空数据库ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 清空数据库ToolStripMenuItem.Click
        '选择数据库表进行按日期对表的数据进行删除
        'If GetInstanceState("清空数据库") Then
        '    Exit Sub
        'End If
        '清空数据库.MdiParent = Me
        Dim del_db As New 清空数据库
        del_db.ShowDialog()
    End Sub


    ''' <summary>
    ''' 退出系统
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 退出系统ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 退出系统ToolStripMenuItem1.Click
        If MsgBox("是否退出系统？", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.Yes Then
            Me.Close()
        End If

    End Sub

    ''' <summary>
    ''' 增加上传地图
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 增加地图ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 增加地图ToolStripMenuItem.Click
        'If GetInstanceState("地图上传") Then
        '    Exit Sub
        'End If
        '地图上传.MdiParent = Me
        Dim map_update As New 地图上传
        map_update.ShowDialog()
    End Sub

    ''' <summary>
    ''' 删除上传的地图
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 删除地图ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 删除地图ToolStripMenuItem.Click
        'If GetInstanceState("删除地图") Then
        '    Exit Sub
        'End If
        '删除地图.MdiParent = Me
        Dim map_delete As New 删除地图
        map_delete.ShowDialog()

    End Sub


#End Region

#Region "配置"
    ''' <summary>
    ''' 增加,编辑，删除主控箱
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>

    Private Sub 主控箱设备ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 主控箱设备ToolStripMenuItem.Click
        'If GetInstanceState("主控箱") Then
        '    Exit Sub
        'End If
        '主控箱.MdiParent = Me
        m_addboxobj = New 主控箱
        m_addboxobj.ShowDialog()
    End Sub

    ''' <summary>
    ''' 增加
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 编辑终端ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 编辑终端ToolStripMenuItem.Click
        'If GetInstanceState("增加终端") Then
        '    Exit Sub
        'End If
        '增加终端.MdiParent = Me
        m_addlampobj = New 增加终端
        m_addlampobj.Show()
    End Sub

    ''' <summary>
    ''' 编辑终端颜色
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 编辑终端颜色ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 编辑终端颜色ToolStripMenuItem1.Click
        'If GetInstanceState("终端颜色编辑窗口") Then
        '    Exit Sub
        'End If
        '终端颜色编辑窗口.MdiParent = Me
        Dim edit_lamp_color As New 终端颜色编辑窗口
        edit_lamp_color.ShowDialog()
    End Sub

    '''' <summary>
    '''' 增加某一级别时段划分
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    '''' <remarks></remarks>
    'Private Sub 添加ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 添加ToolStripMenuItem1.Click
    '    Dim div_time_obj As New 增加控制模式
    '    div_time_obj.Show()
    'End Sub

    '''' <summary>
    '''' 删除某一级别的时段划分
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    '''' <remarks></remarks>
    'Private Sub 删除ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 删除ToolStripMenuItem1.Click
    '    Dim div_time_obj As New 删除控制模式
    '    div_time_obj.Show()
    'End Sub

    '''' <summary>
    '''' 增加交流接触器的时段设置
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    '''' <remarks></remarks>
    'Private Sub 增加ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 增加ToolStripMenuItem1.Click
    '    Dim jiaoliu_obj As New 交流接触器时段设置
    '    jiaoliu_obj.Show()
    'End Sub

    '''' <summary>
    '''' 删除交流接触器的时段设置
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    '''' <remarks></remarks>
    'Private Sub 删除ToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 删除ToolStripMenuItem2.Click
    '    Dim del_jiaoliu_obj As New 删除交流接触器时段设置
    '    del_jiaoliu_obj.Show()
    'End Sub

    ''' <summary>
    ''' 时钟下放
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 时钟下放ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 时钟下放ToolStripMenuItem.Click
        sendTime()
        MsgBox("时钟下放成功", , PROJECT_TITLE_STRING)
    End Sub

    ''' <summary>
    ''' 打开手工控制面板
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 手工控制面板ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 手工控制面板ToolStripMenuItem.Click
        'If GetInstanceState("设备控制面板") Then
        '    Exit Sub
        'End If
        '设备控制面板.MdiParent = Me
        Dim hand_control_obj As New 设备控制面板
        hand_control_obj.ShowDialog()
    End Sub

    '''' <summary>
    '''' 添加特殊的时段控制模式
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    '''' <remarks></remarks>
    'Private Sub 添加特殊ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 添加特殊ToolStripMenuItem.Click
    '    Dim special_div_obj As New 增加特殊时段控制
    '    special_div_obj.Show()
    'End Sub

    '''' <summary>
    '''' 删除特殊控制模式
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    '''' <remarks></remarks>
    'Private Sub 删除特殊控制模式ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 删除特殊控制模式ToolStripMenuItem.Click
    '    Dim set_div_obj As New 删除特殊时段控制
    '    set_div_obj.Show()
    'End Sub

    Private Sub 控制模式ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 控制模式ToolStripMenuItem.Click
        'If GetInstanceState("经纬度") Then
        '    Exit Sub
        'End If
        '经纬度.MdiParent = Me
        Dim div_obj As New 经纬度
        div_obj.ShowDialog()
    End Sub

    Private Sub 打开ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 打开ToolStripMenuItem.Click
        If m_divtimestart = 0 Then
            Me.SetTextLabelDelegate("目前系统处于时段控制", Tool, "work_string")
            m_divtimestart = 1
        Else
            Me.SetTextLabelDelegate("目前系统处于时段控制", Tool, "work_string")
        End If
    End Sub

    Private Sub 关闭ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 关闭ToolStripMenuItem.Click
        If m_divtimestart = 1 Then
            Me.SetTextLabelDelegate(PROJECT_TITLE_STRING, Tool, "work_string")
            m_divtimestart = 0
        Else
            Me.SetTextLabelDelegate(PROJECT_TITLE_STRING, Tool, "work_string")
        End If
    End Sub
#End Region

#Region "报警"
    ''' <summary>
    '''  报警历史
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 报警历史ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 报警历史ToolStripMenuItem.Click
        'If GetInstanceState("故障列表") Then
        '    Exit Sub
        'End If
        '故障列表.MdiParent = Me
        Dim problem_list As New 故障列表
        problem_list.ShowDialog()
    End Sub

    ''' <summary>
    ''' 编辑联系方式
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 编辑联系方式ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 编辑联系方式ToolStripMenuItem.Click
        'If GetInstanceState("编辑联系人") Then
        '    Exit Sub
        'End If
        '编辑联系人.MdiParent = Me
        Dim edit_contact As New 编辑联系人
        edit_contact.ShowDialog()
    End Sub

    ''' <summary>
    ''' 声音报警
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 声音报警ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 声音报警ToolStripMenuItem.Click
        If m_alarmopenorclose = False Then
            m_alarmopenorclose = True
            声音报警ToolStripMenuItem.Image = System.Drawing.Image.FromFile("图片\dis.png")
            ToolStripButton_alarm.Image = System.Drawing.Image.FromFile("图片\响音.jpg")  '载入地图
            ToolStripButton_alarm.Text = "报警"
            '增加操作记录
            Com_inf.Insert_Operation("打开声音报警")
        Else
            PlaySound(vbNullString, 0, 0)
            声音报警ToolStripMenuItem.Image = System.Drawing.Image.FromFile("图片\cha.png")
            m_alarmopenorclose = False
            If Me.BackgroundWorkerAlarm.IsBusy = True Then
                Me.BackgroundWorkerAlarm.CancelAsync()
            End If
            ToolStripButton_alarm.Image = System.Drawing.Image.FromFile("图片\静音.jpg")  '载入地图
            ToolStripButton_alarm.Text = "静音"
            '增加操作记录
            Com_inf.Insert_Operation("关闭声音报警")
        End If
    End Sub
    Private Sub 故障报警窗口ToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles 故障报警窗口ToolStripMenuItem.Click
        If g_lampalarm_show = False Then
            g_lampalarm_show = True
            故障报警窗口ToolStripMenuItem.Image = System.Drawing.Image.FromFile("图片\dis.png")
        Else
            故障报警窗口ToolStripMenuItem.Image = System.Drawing.Image.FromFile("图片\静音.jpg")
            g_lampalarm_show = False
        End If
    End Sub
    ''' <summary>
    ''' 报警类型的选择
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 报警类型选择ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 报警类型选择ToolStripMenuItem.Click
        'If GetInstanceState("SelectAlarmType") Then
        '    Exit Sub
        'End If
        'SelectAlarmType.MdiParent = Me
        Dim SelectAlarmTypeobj As New SelectAlarmType
        SelectAlarmTypeobj.ShowDialog()
    End Sub
#End Region

#Region "故障处理"
    '''' <summary>
    '''' 打开维修统计报表窗口(未使用)
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    '''' <remarks></remarks>
    'Private Sub 维修统计报表ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 维修统计报表ToolStripMenuItem.Click
    '    Dim check_problem_ok As New 维修统计报表
    '    check_problem_ok.Show()
    'End Sub
#End Region

#Region "数据"
    ''' <summary>
    ''' 打开单灯状态查询窗口
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem1.Click
        'If GetInstanceState("终端状态查询窗口") Then
        '    Exit Sub
        'End If
        '终端状态查询窗口.MdiParent = Me
        Dim lamp_window_obj As New 终端状态查询窗口
        lamp_window_obj.ShowDialog()
    End Sub

    ''' <summary>
    ''' 打开统计终端运行状态的窗口
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 终端亮暗信息统计ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 终端亮暗信息统计ToolStripMenuItem.Click
        '用户可以查询特定范围内终端的亮暗信息
        'If GetInstanceState("终端亮暗信息统计") Then
        '    Exit Sub
        'End If
        '终端亮暗信息统计.MdiParent = Me
        Dim lamp_on_off As New 终端亮暗信息统计
        lamp_on_off.ShowDialog()
    End Sub

    ''' <summary>
    ''' 停止轮询线程
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 停止ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 停止ToolStripMenuItem.Click
        If Me.BackgroundWorker_find_state.IsBusy = True Then
            Me.BackgroundWorker_find_state.CancelAsync()
        Else
            MsgBox("查询状态操作已停止，无需重复操作", , PROJECT_TITLE_STRING)
        End If
    End Sub

    ''' <summary>
    ''' 启动轮询线程
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 开始ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 开始ToolStripMenuItem.Click

        If Me.BackgroundWorker_find_state.IsBusy = False Then
            Me.BackgroundWorker_find_state.RunWorkerAsync()
        Else
            MsgBox("查询状态操作已开始，无需重复操作", , PROJECT_TITLE_STRING)
        End If
    End Sub

    ''' <summary>
    ''' 主控箱状态查询
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 主控箱状态查询ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 主控箱状态查询ToolStripMenuItem1.Click
        'If GetInstanceState("主控箱状态查询") Then
        '    Exit Sub
        'End If
        '主控箱状态查询.MdiParent = Me
        Dim box_state As New 主控箱状态查询
        box_state.ShowDialog()
    End Sub

    ''' <summary>
    ''' 主控箱状态统计
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 主控箱状态统计ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 主控箱状态统计ToolStripMenuItem.Click
        'If GetInstanceState("主控箱数据") Then
        '    Exit Sub
        'End If
        '主控箱数据.MdiParent = Me
        Dim box_inf_obj As New 主控箱数据
        box_inf_obj.ShowDialog()
    End Sub


    Private Sub 自动抄表ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 自动抄表ToolStripMenuItem.Click
        '获取抄表的设置
        ' Com_inf.get_chaobiaoconfig()
        Dim conn As New ADODB.Connection
        Dim rs As New ADODB.Recordset
        Dim sql As String
        Dim msg As String = ""

        If g_chaobiaodate = -1 And g_chaobiaotime = -1 Then
            '表示没有设置抄表模式
            MsgBox("请在系统设置里先设置好抄表模式", , PROJECT_TITLE_STRING)
            Exit Sub
        End If
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        sql = "select * from sysconfig where type='自动抄表'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If g_chaobiaotag = False Then
            g_chaobiaotag = True
            自动抄表ToolStripMenuItem.Text = "关闭自动抄表"
            ToolStripLabel1.Text = "抄表模式：自动"
        Else
            g_chaobiaotag = False
            自动抄表ToolStripMenuItem.Text = "打开自动抄表"
            ToolStripLabel1.Text = "抄表模式：手动"
        End If

        If rs.RecordCount > 0 Then
            If g_chaobiaotag = True Then
                rs.Fields("name").Value = 1
            Else
                rs.Fields("name").Value = 0
            End If
        Else
            rs.AddNew()
            rs.Fields("type").Value = "自动抄表"
            If g_chaobiaotag = True Then
                rs.Fields("name").Value = 1
            Else
                rs.Fields("name").Value = 0
            End If
        End If
        rs.Update()

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub
#End Region

#Region "日志"
    ''' <summary>
    ''' 打开手工控制记录
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 系统日志ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 系统日志ToolStripMenuItem.Click
        'If GetInstanceState("手工控制记录") Then
        '    Exit Sub
        'End If
        '手工控制记录.MdiParent = Me
        Dim hand_control_record_obj As New 手工控制记录
        hand_control_record_obj.ShowDialog()
    End Sub

    '''' <summary>
    '''' 故障日志
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    '''' <remarks></remarks>
    'Private Sub 故障日志ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 故障日志ToolStripMenuItem.Click
    '    Dim check_problem As New 故障统计报表
    '    check_problem.Show()
    'End Sub


    ''' <summary>
    ''' 流量日志
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 流量日志ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 流量日志ToolStripMenuItem.Click
        'If GetInstanceState("流量日志") Then
        '    Exit Sub
        'End If
        '流量日志.MdiParent = Me
        Dim gprs_obj As New 流量日志
        gprs_obj.ShowDialog()

    End Sub

    ''' <summary>
    ''' 设备日志
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 设备日志ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 设备日志ToolStripMenuItem.Click
        '用户可以查询特定范围内终端的亮暗信息
        'If GetInstanceState("终端亮暗信息统计") Then
        '    Exit Sub
        'End If
        '终端亮暗信息统计.MdiParent = Me
        Dim lamp_on_off As New 终端亮暗信息统计
        lamp_on_off.ShowDialog()
    End Sub
#End Region

#Region "权限"
    ''' <summary>
    ''' 打开修改用户密码的窗口
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 修改密码ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 修改密码ToolStripMenuItem1.Click
        'If GetInstanceState("edit_password") Then
        '    Exit Sub
        'End If
        'edit_password.MdiParent = Me
        Dim edit_password_obj As New edit_password
        edit_password_obj.ShowDialog()
    End Sub

    ''' <summary>
    ''' 打开用户管理的窗口
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 用户管理ToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 用户管理ToolStripMenuItem2.Click
        'If GetInstanceState("m_manager") Then
        '    Exit Sub
        'End If
        'm_manager.MdiParent = Me
        Dim manage_obj As New m_manager
        manage_obj.ShowDialog()
    End Sub

    ''' <summary>
    ''' 编辑岗位
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 编辑岗位ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 编辑岗位ToolStripMenuItem.Click
        'If GetInstanceState("岗位编辑") Then
        '    Exit Sub
        'End If
        '岗位编辑.MdiParent = Me
        Dim edit_gangwei As New 岗位编辑
        edit_gangwei.ShowDialog()
    End Sub

    ''' <summary>
    ''' 权限分配
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 权限分配ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 权限分配ToolStripMenuItem.Click
        'If GetInstanceState("用户岗位分配") Then
        '    Exit Sub
        'End If
        '用户岗位分配.MdiParent = Me
        Dim edit_rights As New 用户岗位分配
        edit_rights.ShowDialog()
    End Sub

#End Region

#Region "地图"
    ''' <summary>
    ''' 关于地图的操作，包括：删除地图，增加地图背景1，增加地图背景2
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 地图ToolStripMenuItem_DropDownItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles 地图ToolStripMenuItem.DropDownItemClicked
        Dim map_name_string As String
        map_name_string = e.ClickedItem.Text
        Dim rs As New ADODB.Recordset
        Dim map_id As String
        Dim msg As String
        Dim sql As String
        Dim map_path, smallmap_path As String  '大图和小图的路径
        Dim conn As New ADODB.Connection
        DBOperation.OpenConn(conn)
        msg = ""
        ' map_change_tag = 1
        If e.ClickedItem.Text = "删除地图背景" Then
            pb_map.BackgroundImage = Nothing
            pb_small_map.BackgroundImage = Nothing
            g_choosemapid = 0
            ' work_string.Text = "正在删除地图..."
            Exit Sub
        End If
        sql = "select * from map_list where map_name='" & map_name_string & "'"  '根据选择的地图名称，查询数据库
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then '数据库查询出错
            MsgBox(MSG_ERROR_STRING & "地图ToolStripMenuItem_DropDownItemClicked", , PROJECT_TITLE_STRING)
            Exit Sub
        End If
        If rs.RecordCount > 0 Then  '数据库中查询到选择的地图名称
            map_id = rs.Fields("id").Value
            g_mapname = Trim(rs.Fields("map_name").Value)
            map_path = "map\" & g_mapname & ".jpg"  '地图路径
            smallmap_path = "map\s" & g_mapname & ".jpg"  '地图路径
        Else
            MsgBox("没有地图信息，地图被删除", , PROJECT_TITLE_STRING)
            rs.Close()
            rs = Nothing
            Exit Sub
        End If
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
        '将地图的地址写入文件
        Try
            Dim write As New System.IO.StreamWriter("map_name.txt")  '将设置的系统默认颜色写入文件中
            write.WriteLine(map_path)
            write.Close()
        Catch ex As Exception
            MsgBox(ex.Message, , PROJECT_TITLE_STRING)
        End Try
    End Sub
    ''' <summary>
    ''' 为菜单中增加三个菜单项:删除地图背景；增加地图背景1；增加地图背景2
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 地图ToolStripMenuItem_DropDownOpening(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 地图ToolStripMenuItem.DropDownOpening

        '菜单中的的地图按钮加载
        Dim rs_map As ADODB.Recordset
        Dim sql As String
        Dim msg As String
        Dim conn As New ADODB.Connection
        DBOperation.OpenConn(conn)
        sql = "select * from map_list"
        msg = ""
        rs_map = DBOperation.SelectSQL(conn, sql, msg) '查询数据库中的地图表
        If rs_map Is Nothing Then '查询出错
            MsgBox(MSG_ERROR_STRING & "地图ToolStripMenuItem_DropDownOpening", , PROJECT_TITLE_STRING)
            Exit Sub
        End If
        地图ToolStripMenuItem.DropDownItems.Clear() '清空菜单中的内容
        Me.地图ToolStripMenuItem.DropDownItems.Add("删除地图背景")  '菜单中增加一项删除地图背景的选项
        While rs_map.EOF = False
            地图ToolStripMenuItem.DropDownItems.Add(Trim(rs_map.Fields("map_name").Value)) '增加菜单中地图名称
            rs_map.MoveNext()
        End While
        rs_map.Close()
        rs_map = Nothing
    End Sub
#End Region

#Region "节日模式"
    ''' <summary>
    ''' 为节日模式添加菜单内容
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 节日模式ToolStripMenuItem_DropDownOpening(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 节日模式ToolStripMenuItem.DropDownOpening
        '为节日模式添加菜单内容
        Dim rs As ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim conn As New ADODB.Connection
        DBOperation.OpenConn(conn)
        msg = ""
        sql = "select distinct(mod_title) from holiday_mod"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "节日模式ToolStripMenuItem_DropDownOpening", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        Me.节日模式ToolStripMenuItem.DropDownItems.Clear()
        While rs.EOF = False
            Me.节日模式ToolStripMenuItem.DropDownItems.Add(Trim(rs.Fields("mod_title").Value))
            rs.MoveNext()
        End While
        rs.Close()
        rs = Nothing
        conn.Close()
        conn = Nothing
    End Sub
    ''' <summary>
    ''' 点击菜单，选择某一类型的节日模式
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 节日模式ToolStripMenuItem_DropDownItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles 节日模式ToolStripMenuItem.DropDownItemClicked
        m_holidaytitle = e.ClickedItem.Text  '节日模式标题
        If Me.BackgroundWorker_holiday_mod.IsBusy = False Then
            MsgBox("用户自定义模式" & m_holidaytitle & "开始执行", , PROJECT_TITLE_STRING)
            Me.BackgroundWorker_holiday_mod.RunWorkerAsync()
        Else
            MsgBox("用户自定义模式" & m_holidaytitle & "正在执行，请稍后再选择其他模式", , PROJECT_TITLE_STRING)
            'Me.Doing_now_text.AppendText("节日模式" & holiday_title & "正在执行，请稍后再选择节日模式" & vbCrLf)
            Exit Sub
        End If
    End Sub
#End Region

#Region "窗体"

    ''' <summary>
    ''' 监控面板（打开，关闭）
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 监控面板ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 监控面板ToolStripMenuItem.Click
        If m_jiankongopenorclose = True Then
            Me.监控面板ToolStripMenuItem.Image = Nothing
            SplitContainer1.Panel2Collapsed = True
            m_jiankongopenorclose = False
        Else
            Me.监控面板ToolStripMenuItem.Image = System.Drawing.Image.FromFile("图片\dis.png")
            SplitContainer1.Panel2Collapsed = False
            Me.Tab_show_box.TabPages.Remove(Me.TabPage2)
            m_jiankongopenorclose = True
        End If
    End Sub

    ''' <summary>
    ''' 实时数据统计
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 实时数据统计ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 实时数据统计ToolStripMenuItem.Click
        If m_tongjiopenorclose = True Then
            Me.实时数据统计ToolStripMenuItem.Image = Nothing
            Me.shujutongji.Visible = False
            m_tongjiopenorclose = False
        Else
            Me.实时数据统计ToolStripMenuItem.Image = System.Drawing.Image.FromFile("图片\dis.png")
            Me.shujutongji.Visible = True
            m_tongjiopenorclose = True
        End If
    End Sub

    ''' <summary>
    ''' 功能提示
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 功能提示ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 功能提示ToolStripMenuItem.Click
        If m_gongnengopenorclose = True Then
            Me.功能提示ToolStripMenuItem.Image = Nothing
            Me.gongnengtishi.Visible = False
            m_gongnengopenorclose = False
        Else
            Me.功能提示ToolStripMenuItem.Image = System.Drawing.Image.FromFile("图片\dis.png")
            Me.gongnengtishi.Visible = True
            m_gongnengopenorclose = True
        End If
    End Sub

#End Region

#Region "帮助"
    ''' <summary>
    ''' 监控软件使用说明书
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 监控软件使用说明书ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 监控软件使用说明书ToolStripMenuItem.Click
        If GetInstanceState("监控软件使用说明书") Then
            Exit Sub
        End If
        监控软件使用说明书.MdiParent = Me
        Dim readme_book As New 监控软件使用说明书
        readme_book.Show()

    End Sub

    ''' <summary>
    ''' 关于
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 关于ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 关于ToolStripMenuItem1.Click
        If GetInstanceState("关于") Then
            Exit Sub
        End If
        关于.MdiParent = Me
        关于.Show()
    End Sub


    ''' <summary>
    ''' 打开备忘录窗口
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 备注ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 备注ToolStripMenuItem.Click
        '为用户提供一个窗口，用户可以随时记录下软件使用中出现的问题或需要改进的地方
        'If GetInstanceState("备忘录") Then
        '    Exit Sub
        'End If
        '备忘录.MdiParent = Me
        Dim record_obj As New 备忘录
        record_obj.ShowDialog()
    End Sub
#End Region

#Region "工具栏"
    ''' <summary>
    '''  系统参数设置的窗口快捷键
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_systemset.Click
        'If GetInstanceState("系统参数设置") Then
        '    Exit Sub
        'End If
        '系统参数设置.MdiParent = Me
        Dim control_obj As New 系统参数设置
        control_obj.ShowDialog()
    End Sub

    ''' <summary>
    ''' 增加主控箱快捷键
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_boxset.Click
        If GetInstanceState("主控箱") Then
            Exit Sub
        End If
        主控箱.MdiParent = Me
        m_addboxobj = New 主控箱
        m_addboxobj.Show()
    End Sub

    '''' <summary>
    '''' 编辑主控箱
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    '''' <remarks></remarks>
    'Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton3.Click
    '    'map_size.Value = 8
    '    'g_changemapvalue = MAP_MID_SIZE
    '    'tb_map_size.Value = g_changemapvalue
    '    'map_size_id.Text = "地图尺寸：100 %"

    '    '2012年3月21增加编辑主控箱的快捷键
    '    m_editboxobj = New 编辑电控箱
    '    m_editboxobj.Show()
    'End Sub

    ''' <summary>
    ''' 增加终端设备
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ToolStripButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_setlamp.Click
        If GetInstanceState("增加终端") Then
            Exit Sub
        End If
        增加终端.MdiParent = Me
        m_addlampobj = New 增加终端
        m_addlampobj.Show()
    End Sub

    ''' <summary>
    ''' 设备控制面板的快捷键
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ToolStripButtonup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_control.Click
        'If GetInstanceState("设备控制面板") Then
        '    Exit Sub
        'End If
        '设备控制面板.MdiParent = Me
        Dim hand_control_obj As New 设备控制面板
        hand_control_obj.ShowDialog()
    End Sub

    ''' <summary>
    '''  遥测窗口
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ToolStripButtondown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_zhaoce.Click
        'If GetInstanceState("遥测窗口") Then
        '    Exit Sub
        'End If
        '遥测窗口.MdiParent = Me
        Dim zhaoceobj As New 遥测窗口
        Dim controlboxobj As New control_box
        controlboxobj.set_controlbox_list(zhaoceobj.tv_yaoce_controlbox) '主控箱信息列表
        zhaoceobj.ShowDialog()
    End Sub

    '''' <summary>
    ''''  向左移动地图
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    '''' <remarks></remarks>
    'Private Sub ToolStripButtonleft_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonleft.Click
    '    '用户可以查询特定范围内终端的亮暗信息
    '    Dim lamp_on_off As New 终端亮暗信息统计
    '    lamp_on_off.Show()
    'End Sub

    ''' <summary>
    ''' 主控箱数据统计快捷键
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ToolStripButtonright_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_boxinf.Click
        'If GetInstanceState("主控箱数据") Then
        '    Exit Sub
        'End If
        '主控箱数据.MdiParent = Me
        Dim box_inf_obj As New 主控箱数据
        Dim controlboxobj As New control_box
        'controlboxobj.set_controlbox_list(box_inf_obj.tv_yaoce_controlbox) '主控箱信息列表
        controlboxobj.set_controlbox_list(box_inf_obj.tv_yaoce_controlbox) '主控箱信息列表
        controlboxobj.set_controlbox_list(box_inf_obj.tv_boxlist_state) '状态查询中的主控箱列表
        controlboxobj.set_controlbox_list(box_inf_obj.tv_boxlist_kaiguan) '开关查询中的主控箱列表
        controlboxobj.Dispose()
        box_inf_obj.ShowDialog()
    End Sub

    ''' <summary>
    ''' 刷新，重启所有线程
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ToolStripButtonReF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_light.Click
        If g_openvalue = True Then
            g_openvalue = False
            If Me.BackgroundWorker_check_lightvalue.IsBusy = True Then
                Me.BackgroundWorker_check_lightvalue.CancelAsync()
            End If
            ToolStripButton_light.Text = "开启光照采集"
            Lightvalue.Text = "当前光照度："
        Else
            g_openvalue = True
            If Me.BackgroundWorker_check_lightvalue.IsBusy = False Then
                Me.BackgroundWorker_check_lightvalue.RunWorkerAsync()
            End If
            ToolStripButton_light.Text = "停止光照采集"
        End If
    End Sub

    ''' <summary>
    ''' 报警类型选择
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Logout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton_choosealarm.Click
        'If GetInstanceState("SelectAlarmType") Then
        '    Exit Sub
        'End If
        'SelectAlarmType.MdiParent = Me
        Dim SelectAlarmTypeobj As New SelectAlarmType
        SelectAlarmTypeobj.ShowDialog()
    End Sub
    ''' <summary>
    ''' 控制模式设置的快捷方式
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ToolStripButtonControlSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonControlSet.Click
        'If GetInstanceState("经纬度") Then
        '    Exit Sub
        'End If
        '经纬度.MdiParent = Me
        Dim zhaoceobj As New 经纬度
        Dim controlboxobj As New control_box
        m_controlboxobj.set_controlbox_list(zhaoceobj.tv_box_inf_list) '主控箱信息列表
        m_controlboxobj.set_controlbox_list(zhaoceobj.tv_all_controlbox) '主控箱信息列表
        m_controlboxobj.set_controlbox_list(zhaoceobj.tv_divtime_controlbox) '自动控制模式中的主控箱信息列表
        m_controlboxobj.set_controlbox_list(zhaoceobj.tv_banye_controlbox)  '半夜灯控制
        m_controlboxobj.set_controlbox_list(zhaoceobj.tv_holidaydivtime_box) '节假日控制
        zhaoceobj.ShowDialog()
    End Sub

#End Region


#Region "判断是否已经在页面打开此菜单"
    Public Function GetInstanceState(ByVal name As String) As Boolean
        Dim i As Integer = Me.MdiChildren.Length
        '循环判断是否有名为name的子窗体实例
        If g_windowclose = 0 Then
            For i = 0 To Me.MdiChildren.Length - 1
                If Me.MdiChildren(i).Name = name Then
                    '存在名为name的子窗体，是子窗体获得焦点并返回True
                    Me.MdiChildren(i).Focus()
                    Return True
                End If
            Next
            '不存在名为Name的子窗体False
            Return False
        Else
            g_windowclose = 0
            Return False
        End If
    End Function
#End Region

End Class
