''' <summary>
''' 修改控制条件和控制值，即按电流还是电阻判断，判断值是多少，如电流AD,判断值20
''' </summary>
''' <remarks></remarks>

Public Class 系统参数设置

    Private m_phonenum As String '短信中心号码
    Private m_controlall As String '控制条件
    Private m_controlpart As String '半功率控制条件
    Private m_lampnum As String  '灯杆对应灯头的个数
    Private m_configstring As String '区间配置命令字符串
    Private m_inter_config As String = "1" '区间配置命令字符串
    Private m_inter_count As String '区间区间数量
    Private m_from_to As String '区间区间数量
    Private m_boxid As String '区间配置的主控箱编号
    Private m_boxname As String '区间配置的主控箱名称
    Private m_configtitle As String '配置区间还是配置路段号
    Private m_string As String '配置信息
    Private m_configtag As Integer
    Private m_autotime As String '自动上传的配置时间
    Private m_check As Boolean = False '设置标志，防止死循环
    Private m_checklist As New ArrayList  '存放选中的主控箱名称
    Private m_jiechuqiid As String '接触器编号
    Private m_controlboxid As New ArrayList'主控箱编号


    ''' <summary>
    ''' 修改控制条件和控制值，即按电流还是电阻判断，判断值是多少
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub input_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_input.Click
        If cb_control_condition.Text = "" Then  '如果控制条件下拉框为空
            MsgBox("请选择控制条件", , PROJECT_TITLE_STRING)
            cb_control_condition.Focus() '光标定位在控制条件下拉框
            Exit Sub
        End If
        If tb_control_value_part.Text = "" Then  '如果控制值为空
            MsgBox("请选择半功率控制值", , PROJECT_TITLE_STRING)
            tb_control_value_part.Focus() '光标定位在控制值文本框
            Exit Sub
        End If
        If tb_control_value_all.Text = "" Then  '如果控制值为空
            MsgBox("请选择全功率控制值", , PROJECT_TITLE_STRING)
            tb_control_value_all.Focus() '光标定位在控制值文本框
            Exit Sub
        End If
        If IsNumeric(Trim(tb_control_value_part.Text)) = False Or Val(Trim(tb_control_value_part.Text)) < 0 Or Val(Trim(tb_control_value_part.Text)) > 256 Then  '控制值限定在0-256
            MsgBox("请输入0-255之间的数值", , PROJECT_TITLE_STRING)
            tb_control_value_part.Focus()  '光标定位在控制值文本框
            Exit Sub
        End If
        If IsNumeric(Trim(tb_control_value_all.Text)) = False Or Val(Trim(tb_control_value_all.Text)) < 0 Or Val(Trim(tb_control_value_all.Text)) > 256 Then  '控制值限定在0-256
            MsgBox("请输入0-255之间的数值", , PROJECT_TITLE_STRING)
            tb_control_value_all.Focus()  '光标定位在控制值文本框
            Exit Sub
        End If

        Try
            Dim rs As New ADODB.Recordset
            Dim conn As New ADODB.Connection
            Dim msg As String
            Dim sql As String

            msg = ""
            '配置半功率控制值，
            sql = "select * from sysconfig where type='半功率控制'"
            DBOperation.OpenConn(conn)
            rs = DBOperation.SelectSQL(conn, sql, msg)
            If rs Is Nothing Then
                MsgBox(MSG_ERROR_STRING & "input_Click", , PROJECT_TITLE_STRING)
                conn.Close()
                conn = Nothing
                Exit Sub
            End If
            If rs.RecordCount > 0 Then
                rs.Fields("name").Value = Trim(tb_control_value_part.Text)
                rs.Update()
            Else
                rs.AddNew()
                rs.Fields("type").Value = "半功率控制"
                rs.Fields("name").Value = Trim(tb_control_value_part.Text)
                rs.Update()
            End If

            '配置全功率控制
            sql = "select * from sysconfig where type='全功率控制'"
            rs = DBOperation.SelectSQL(conn, sql, msg)
            If rs Is Nothing Then
                MsgBox(MSG_ERROR_STRING & "input_Click", , PROJECT_TITLE_STRING)
                conn.Close()
                conn = Nothing
                Exit Sub
            End If
            If rs.RecordCount > 0 Then
                rs.Fields("name").Value = Trim(tb_control_value_all.Text)
                rs.Update()
            Else
                rs.AddNew()
                rs.Fields("type").Value = "全功率控制"
                rs.Fields("name").Value = Trim(tb_control_value_all.Text)
                rs.Update()
            End If

            MsgBox("路灯开关参数设置成功", , PROJECT_TITLE_STRING)

            '增加操作记录
            Com_inf.Insert_Operation("编辑开关控制参数：" & Trim(tb_control_value_part.Text) & ", " & Trim(tb_control_value_all.Text))


            m_controlall = Trim(tb_control_value_all.Text)
            m_controlpart = Trim(tb_control_value_part.Text)
            '将主控界面中的control_condition和control_value两个变量赋值
            g_controlcondition = Trim(cb_control_condition.Text)
            g_controlvaluepart = Val(Trim(tb_control_value_part.Text))
            g_controlvalueall = Val(Trim(tb_control_value_all.Text))

            If rs.State = 1 Then
                rs.Close()
                rs = Nothing
            End If
            conn.Close()
            conn = Nothing
        Catch ex As Exception
            MsgBox(ex.Message, , PROJECT_TITLE_STRING)
        End Try


    End Sub

    ''' <summary>
    ''' 窗体载入函数
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 路灯开关控制参数_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '目前的控制参数
        '设置各个窗体的图标
        Me.Icon = New Icon("图片\favicon.ico", 32, 32)
        Try
            ' Dim MyRead1 As New System.IO.StreamReader("control_condition.txt", System.Text.Encoding.UTF8)
            tb_control_value_part.Text = g_controlvalueall
            m_controlpart = tb_control_value_part.Text   '半功率控制条件
            tb_control_value_all.Text = g_controlvaluepart
            m_controlall = tb_control_value_all.Text '全功率控制条件
            'TCenterNO.Text = MyRead1.ReadLine   '中心号码
            m_phonenum = lb_center_num.Text   '中心号码
            tb_denggan_lampnum.Text = g_lampnum '灯杆对应灯头的个数
            m_lampnum = tb_denggan_lampnum.Text '灯杆对应灯头的个数
            tb_projecttitle.Text = PROJECT_TITLE_STRING '软件标题
            tb_map_maxsize.Text = MAP_MAX_SIZE  '地图的缩放最大值
            tb_map_defsize.Text = MAP_MID_SIZE  '地图的默认值
            tb_presure_top.Text = g_welcomewinobj.m_controlboxobj.m_presuretopvalue  '电压上限值
            tb_presure_bottom.Text = g_welcomewinobj.m_controlboxobj.m_presurebottomvalue  '电压下限值
            tb_getlamp_time.Text = g_getlamp_time   '获取招测单灯等待时间
            'sysconfig.TabPages.Remove(Me.TabPageMail)  '短信猫功能暂不打开
            tb_zc_waittime.Text = g_ycwaittime  '招测等待时间
            tb_zc_jgtime.Text = g_ycjgtime '召测间隔时间
            m_configtitle = "区间"
            Dim controlboxobj As New control_box
            controlboxobj.set_controlbox_list(tv_boxlist) '主控箱信息列表

            controlboxobj.set_controlbox_list(tv_yearconfig) '主控箱信息列表

            '设置经纬度
            tb_jingdu.Text = Com_inf.get_jingduvalue().ToString
            tb_weidu.Text = Com_inf.get_weiduvalue().ToString
            tb_delaytime.Text = Com_inf.get_alarmdelaytime()

            '光照
            tb_light_value.Text = g_lightvalueset
            tb_getlamp_time.Text = Com_inf.g_textbox_time_value()
            ' MyRead1.Close()

            '定时抄表设置

            If g_chaobiaodate = -1 And g_chaobiaotime > -1 Then
                '按日召测
                rb_checktime.Checked = True
                cb_time.Text = g_chaobiaotime
            End If

            If g_chaobiaodate > -1 And g_chaobiaotime > -1 Then
                '按月抄表
                rb_checkmonth.Checked = True
                cb_date.Enabled = True
                cb_date.Text = g_chaobiaodate
                cb_time.Text = g_chaobiaotime
            End If

        Catch ex As Exception
            MsgBox(ex.Message, , PROJECT_TITLE_STRING)
        End Try
    End Sub

    '''' <summary>
    '''' 连接短信猫
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    '''' <remarks></remarks>
    'Private Sub LinkModem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LinkModem.Click
    '    Dim ret As Integer
    '    Try
    '        ret = g_welcomewinobj.AxModem1.ConModem(comName.Text, CInt(comRate.Text))
    '        If ret Then
    '            readCenterNO.Enabled = True
    '            disLinkModem.Enabled = True
    '            LinkModem.Enabled = False

    '            g_welcomewinobj.ModemState.Text = "短信猫连接"

    '            '打开发送短信的线程
    '            If g_welcomewinobj.BackgroundWorkerSendMsg.IsBusy = False Then
    '                g_welcomewinobj.BackgroundWorkerSendMsg.RunWorkerAsync()
    '            End If

    '        Else
    '            MsgBox("连接MODEM失败", , PROJECT_TITLE_STRING)
    '            g_welcomewinobj.ModemState.Text = "短信猫断开"
    '        End If
    '    Catch ex As Exception
    '        MsgBox("短信猫配置出错", , PROJECT_TITLE_STRING)
    '    End Try

    'End Sub

    '''' <summary>
    '''' 断开短信猫
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    '''' <remarks></remarks>
    'Private Sub disLinkModem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles disLinkModem.Click
    '    Try
    '        g_welcomewinobj.AxModem1.DisconModem()
    '        readCenterNO.Enabled = False
    '        disLinkModem.Enabled = False
    '        LinkModem.Enabled = True
    '        g_welcomewinobj.ModemState.Text = "短信猫断开"

    '        '关闭发送短信的线程
    '        If g_welcomewinobj.BackgroundWorkerSendMsg.IsBusy = True Then
    '            g_welcomewinobj.BackgroundWorkerSendMsg.CancelAsync()
    '        End If
    '    Catch ex As Exception
    '        MsgBox("短信猫配置出错", , PROJECT_TITLE_STRING)
    '    End Try


    'End Sub

    ''' <summary>
    ''' 读取短信中心的号码
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub readCenterNO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles readCenterNO.Click
        Try
            Dim MyRead1 As New System.IO.StreamReader("control_condition.txt", System.Text.Encoding.UTF8)
            MyRead1.ReadLine()
            MyRead1.ReadLine()
            lb_center_num.Text = MyRead1.ReadLine()
            MsgBox(Trim(lb_center_num.Text), , "短信中心号")

            MyRead1.Close()

        Catch ex As Exception
            MsgBox(ex.Message, , PROJECT_TITLE_STRING)
        End Try
        Try

        Catch ex As Exception
            MsgBox("短信猫配置出错", , PROJECT_TITLE_STRING)
        End Try

    End Sub

    '''' <summary>
    '''' 配置短信猫
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    '''' <remarks></remarks>
    'Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
    '    Try
    '        g_welcomewinobj.AxModem1.About()
    '    Catch ex As Exception
    '        MsgBox("短信猫配置出错", , PROJECT_TITLE_STRING)
    '    End Try

    'End Sub

    ''' <summary>
    ''' 编辑短信中心的号码
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_edit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_edit.Click
        If Me.tb_edit_centernum.Text = "" Then
            MsgBox("短信中心的号码不可以为空")
            Me.tb_edit_centernum.Focus()
            Exit Sub
        End If
        If IsNumeric(Trim(Me.tb_edit_centernum.Text)) = False Then
            MsgBox("短信中心的号码必须全为数字")
            Me.tb_edit_centernum.Focus()
            Exit Sub
        End If
        If Me.tb_edit_centernum.TextLength > 20 Then
            MsgBox("短信中心的号码长度大于20")
            Me.tb_edit_centernum.Focus()
            Exit Sub
        End If

        Try
            Dim write As New System.IO.StreamWriter("control_condition.txt")  '打开control_condition文本框
            write.WriteLine(m_controlpart) '写入控制值
            write.WriteLine(m_controlall) '写入控制值
            write.WriteLine(Me.tb_edit_centernum.Text)  '写入中心号码
            write.Close()

            MsgBox("短信中心号码编辑成功！", , PROJECT_TITLE_STRING)

            m_phonenum = Me.tb_edit_centernum.Text   '中心号码全局变量
            Me.lb_center_num.Text = m_phonenum

        Catch ex As Exception
            MsgBox(ex.Message, , PROJECT_TITLE_STRING)
        End Try


    End Sub

    ''' <summary>
    ''' 设置每个灯杆上灯头的个数
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_set_lamp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_set_lamp.Click
        If IsNumeric(Trim(tb_denggan_lampnum.Text)) = False Then  '控制值必须为数字
            MsgBox("请输入数值", , PROJECT_TITLE_STRING)
            tb_denggan_lampnum.Focus()  '光标定位在灯杆数值上
            Exit Sub
        End If

        Try
            Dim rs As New ADODB.Recordset
            Dim conn As New ADODB.Connection
            Dim msg As String
            Dim sql As String

            msg = ""
            '配置电压上限
            sql = "select * from sysconfig where type='灯头个数'"
            DBOperation.OpenConn(conn)
            rs = DBOperation.SelectSQL(conn, sql, msg)
            If rs Is Nothing Then
                MsgBox(MSG_ERROR_STRING & "set_lamp_Click", , PROJECT_TITLE_STRING)
                conn.Close()
                conn = Nothing
                Exit Sub
            End If
            If rs.RecordCount > 0 Then
                rs.Fields("name").Value = Trim(tb_denggan_lampnum.Text)
                rs.Update()
            Else
                rs.AddNew()
                rs.Fields("type").Value = "灯头个数"
                rs.Fields("name").Value = Trim(tb_denggan_lampnum.Text)
                rs.Update()
            End If


            MsgBox("控制参数编辑成功！", , PROJECT_TITLE_STRING)
            '增加操作记录
            Com_inf.Insert_Operation("配置灯杆对应灯头个数：" & Trim(tb_denggan_lampnum.Text))

            m_lampnum = Trim(tb_denggan_lampnum.Text)
            '将主控界面中的control_condition和control_value两个变量赋值
            g_lampnum = m_lampnum

            If rs.State = 1 Then
                rs.Close()
                rs = Nothing
            End If
            conn.Close()
            conn = Nothing
        Catch ex As Exception
            MsgBox(ex.Message, , PROJECT_TITLE_STRING)
        End Try

    End Sub

    ''' <summary>
    ''' 设置软件的标题
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_set_project_title_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_set_project_title.Click
        If Trim(tb_projecttitle.Text) = "" Then
            MsgBox("软件的标题不可以为空", , PROJECT_TITLE_STRING)
            tb_projecttitle.Focus()
            Exit Sub
        End If
        If Trim(tb_projecttitle.Text).Length > 30 Then
            MsgBox("软件的标题长度不可以超过30", , PROJECT_TITLE_STRING)
            tb_projecttitle.Focus()
            Exit Sub
        End If

        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim rs As New ADODB.Recordset

        msg = ""
        DBOperation.OpenConn(conn)
        sql = "select * from sysconfig where type='项目标题'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "setProjectTitle_Click", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs.RecordCount > 0 Then
            rs.Fields("name").Value = Trim(tb_projecttitle.Text)
            rs.Update()
        Else
            rs.AddNew()
            rs.Fields("type").Value = "项目标题"
            rs.Fields("name").Value = Trim(tb_projecttitle.Text)
            rs.Update()
        End If

        PROJECT_TITLE_STRING = Trim(tb_projecttitle.Text)
        g_welcomewinobj.Text = PROJECT_TITLE_STRING
        MsgBox("软件标题设置成功", , PROJECT_TITLE_STRING)

        '增加操作记录
        Com_inf.Insert_Operation("配置软件标题：" & PROJECT_TITLE_STRING)

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing

    End Sub

    ''' <summary>
    ''' 设置地图的缩放尺度
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_set_map_size_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_set_map_size.Click
        If Trim(tb_map_maxsize.Text) = "" Then
            MsgBox("地图缩放最大级别不可为空", , PROJECT_TITLE_STRING)
            tb_map_maxsize.Focus()
            Exit Sub
        End If
        If Trim(tb_map_defsize.Text) = "" Then
            MsgBox("地图缩放默认级别不可为空", , PROJECT_TITLE_STRING)
            tb_map_defsize.Focus()
            Exit Sub
        End If
        If IsNumeric(Trim(tb_map_maxsize.Text)) = False Then
            MsgBox("地图缩放最大级别必须为数字", , PROJECT_TITLE_STRING)
            tb_map_maxsize.Focus()
            Exit Sub
        End If
        If IsNumeric(Trim(tb_map_defsize.Text)) = False Then
            MsgBox("地图缩放默认级别必须为数字", , PROJECT_TITLE_STRING)
            tb_map_defsize.Focus()
            Exit Sub
        End If
        If Val(Trim(tb_map_maxsize.Text)) > 30 Or Val(Trim(tb_map_maxsize.Text)) <= 0 Then
            MsgBox("地图缩放最大级别范围为1-30", , PROJECT_TITLE_STRING)
            tb_map_maxsize.Focus()
            Exit Sub
        End If
        If Val(Trim(tb_map_defsize.Text)) > 30 Or Val(Trim(tb_map_defsize.Text)) <= 0 Then
            MsgBox("地图缩放默认级别范围为1-30", , PROJECT_TITLE_STRING)
            tb_map_defsize.Focus()
            Exit Sub
        End If
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim rs As New ADODB.Recordset

        msg = ""
        DBOperation.OpenConn(conn)
        '设置地图最大尺寸
        sql = "select * from sysconfig where type='地图最大尺寸'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "setMapSize_Click", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs.RecordCount > 0 Then
            rs.Fields("name").Value = Trim(tb_map_maxsize.Text)
            rs.Update()
        Else
            rs.AddNew()
            rs.Fields("type").Value = "地图最大尺寸"
            rs.Fields("name").Value = Trim(tb_map_maxsize.Text)
            rs.Update()
        End If

        '设置地图默认尺寸
        sql = "select * from sysconfig where type='地图默认尺寸'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "setMapSize_Click", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs.RecordCount > 0 Then
            rs.Fields("name").Value = Trim(tb_map_defsize.Text)
            rs.Update()
        Else
            rs.AddNew()
            rs.Fields("type").Value = "地图默认尺寸"
            rs.Fields("name").Value = Trim(tb_map_defsize.Text)
            rs.Update()
        End If

        MAP_MAX_SIZE = Val(Trim(tb_map_maxsize.Text))

        MAP_MID_SIZE = Val(Trim(tb_map_defsize.Text))
        g_changemapvalue = MAP_MID_SIZE
        g_welcomewinobj.tb_map_size.Maximum = MAP_MAX_SIZE
        g_welcomewinobj.tb_map_size.Value = MAP_MID_SIZE

        MAP_SIZE_BASE = 1 - (MAP_MID_SIZE / MAP_MAX_SIZE)
        MAP_SIZE_CHANGE = 1 / MAP_MAX_SIZE

        MsgBox("地图尺寸设置成功", , PROJECT_TITLE_STRING)

        '增加操作记录
        Com_inf.Insert_Operation("配置地图尺寸：" & Trim(tb_map_maxsize.Text) & ", " & Trim(tb_map_defsize.Text))
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing

    End Sub

    ''' <summary>
    ''' 设置电压上下限
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_set_presure_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_set_presure.Click
        If tb_presure_top.Text = "" Then
            MsgBox("电压上限不可以为空", , PROJECT_TITLE_STRING)
            tb_presure_top.Focus()
            Exit Sub
        End If
        If tb_presure_bottom.Text = "" Then
            MsgBox("电压下限不可以为空", , PROJECT_TITLE_STRING)
            tb_presure_bottom.Focus()
            Exit Sub
        End If

        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String

        msg = ""
        '配置电压上限
        sql = "select * from sysconfig where type='电压上限'"
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
            rs.Fields("name").Value = Trim(tb_presure_top.Text)
            rs.Update()
        Else
            rs.AddNew()
            rs.Fields("type").Value = "电压上限"
            rs.Fields("name").Value = Trim(tb_presure_top.Text)
            rs.Update()
        End If

        '配置电压下限
        sql = "select * from sysconfig where type='电压下限'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "set_presure_Click", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs.RecordCount > 0 Then
            rs.Fields("name").Value = Trim(tb_presure_bottom.Text)
            rs.Update()
        Else
            rs.AddNew()
            rs.Fields("type").Value = "电压下限"
            rs.Fields("name").Value = Trim(tb_presure_bottom.Text)
            rs.Update()
        End If

        MsgBox("电压上下限配置成功！", , PROJECT_TITLE_STRING)

        '增加操作记录
        Com_inf.Insert_Operation("配置电压上下限：" & Trim(tb_presure_top.Text) & ", " & Trim(tb_presure_bottom.Text))

        '重新设置全局变量
        g_welcomewinobj.m_controlboxobj.Get_TopBottom_value()

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub


    ''' <summary>
    ''' 增加设置区间
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_add_config_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_add_config.Click
        If tb_control_box_id.Text = "" Then
            MsgBox("请选择主控箱", , PROJECT_TITLE_STRING)
            tb_control_box_id.Focus()
            Exit Sub
        End If
        If IsNumeric(Trim(tb_control_box_id.Text)) = False Or Trim(tb_control_box_id.Text).Length > 4 Then
            MsgBox("请输入正确格式的主控箱编号", , PROJECT_TITLE_STRING)
            tb_control_box_id.Focus()
            Exit Sub
        End If

        If cb_type_id.Text = "" Then
            MsgBox("请选择类型编号", , PROJECT_TITLE_STRING)
            cb_type_id.Focus()
            Exit Sub
        End If
        If tb_config_startid.Text = "" Then
            MsgBox("请输入起始节点编号", , PROJECT_TITLE_STRING)
            tb_config_startid.Focus()
            Exit Sub
        End If
        If IsNumeric(Trim(tb_config_startid.Text)) = False Then
            MsgBox("节点编号必须为数字", , PROJECT_TITLE_STRING)
            tb_config_startid.Focus()
            Exit Sub
        End If
        If tb_config_startid.TextLength > 5 Then
            MsgBox("节点编号长度大于5", , PROJECT_TITLE_STRING)
            tb_config_startid.Focus()
            Exit Sub
        End If

        If tb_config_len.Text = "" Then
            MsgBox("请输入区间长度", , PROJECT_TITLE_STRING)
            tb_config_len.Focus()
            Exit Sub
        End If
        If IsNumeric(Trim(tb_config_len.Text)) = False Then
            MsgBox("区间长度必须为数字", , PROJECT_TITLE_STRING)
            tb_config_len.Focus()
            Exit Sub
        End If
        If tb_config_len.TextLength > 3 Then
            MsgBox("区间长度大于1000", , PROJECT_TITLE_STRING)
            tb_config_len.Focus()
            Exit Sub
        End If

        Dim list_length As Integer = dgv_config_list.RowCount
        dgv_config_list.Rows.Add()
        dgv_config_list.Rows(list_length).Cells("id").Value = list_length + 1
        dgv_config_list.Rows(list_length).Cells("startid").Value = Val(Trim(tb_control_box_id.Text)).ToString & "-" & Val(cb_type_id.Text) & "-" & Trim(tb_config_startid.Text)
        dgv_config_list.Rows(list_length).Cells("length").Value = Trim(tb_config_len.Text)

        '此时只允许配置一个主控箱的信息
        cb_control_box_name.Enabled = False
        tb_control_box_id.Enabled = False
    End Sub

    ''' <summary>
    ''' 配置区间
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_set_config_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_set_config.Click
        Dim box_id As String
        Dim config_num As String
        Dim i As Integer = 0
        Dim J As Integer = 0
        Dim start_id As String
        Dim length As String
        Dim type_id As String
        Dim conn As New ADODB.Connection
        m_from_to = ""
        ' msg = ""
        '配置区间
        If rb_setbox_area.Checked = True Then
            If dgv_config_list.Rows.Count = 0 Then
                MsgBox("请增加区间信息", , PROJECT_TITLE_STRING)
                Exit Sub
            End If


            m_boxid = Trim(tb_control_box_id.Text)
            box_id = Com_inf.Dec_to_Hex(Trim(tb_control_box_id.Text), 4)

            box_id = Mid(box_id, 1, 2) & " " & Mid(box_id, 3, 2)

            config_num = dgv_config_list.RowCount
            If config_num > 20 Then
                MsgBox("区间长度超过20，请重新设置", , PROJECT_TITLE_STRING)
                Exit Sub
            End If
            config_num = Com_inf.Dec_to_Hex(config_num.ToString, 4)
            config_num = Mid(config_num, 1, 2) & " " & Mid(config_num, 3, 2)
            m_configstring = config_num & " " & box_id

            While i < dgv_config_list.RowCount
                start_id = Trim(dgv_config_list.Rows(i).Cells("startid").Value).Split("-")(2)
                length = Trim(dgv_config_list.Rows(i).Cells("length").Value)

                m_from_to &= (start_id + "_" + length) + ","

                type_id = Trim(dgv_config_list.Rows(i).Cells("startid").Value).Split("-")(1) '
                start_id = Com_inf.Dec_to_Bin(Val(type_id), 5) & Com_inf.Dec_to_Bin(Val(start_id), 11)
                start_id = Com_inf.BIN_to_HEX(start_id)
                length = Com_inf.Dec_to_Hex(length, 4)
                m_configstring &= " " & Mid(start_id, 1, 2) & " " & Mid(start_id, 3, 2) & " " & Mid(length, 1, 2) & " " & Mid(length, 3, 2)

                i += 1
            End While
            m_inter_count = dgv_config_list.RowCount
        End If
        '配置路段号
        If rb_setbox_id.Checked = True Then
            m_boxid = Trim(tb_control_box_id.Text)
            box_id = Com_inf.Dec_to_Hex(Trim(tb_control_box_id.Text), 4)

            box_id = Mid(box_id, 1, 2) & " " & Mid(box_id, 3, 2)
            m_configstring = "00 00 " & box_id
        End If

        If Me.BackgroundWorkerconfigarea.IsBusy = False Then
            m_configtag = 1  '
            Me.BackgroundWorkerconfigarea.RunWorkerAsync()
        Else
            MsgBox("配置线程正在运行请稍后重试", , PROJECT_TITLE_STRING)

        End If

        '恢复允许配置其他主控箱的信息
        cb_control_box_name.Enabled = True
        tb_control_box_id.Enabled = True
    End Sub

    ''' <summary>
    ''' 发送区间配置命令，等待回复，判断是否配置成功
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BackgroundWorkerconfigarea_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorkerconfigarea.DoWork
        If m_configtag = 1 Then  '配置区间
            Dim sendconfig As Boolean
            Dim time As DateTime
            sendconfig = insert_config_string()
            time = Now
            If sendconfig = False Then
                '命令发送失败，退出
                Exit Sub
            End If
            Me.BackgroundWorkerconfigarea.ReportProgress(1)
            '命令发送后，等待1秒钟后查询是否有回复
            m_string = m_boxname & "配置区间......"
            Me.BackgroundWorkerconfigarea.ReportProgress(2)

            System.Threading.Thread.Sleep(1000)
            get_back_inf(time)
        End If

        If m_configtag = 2 Then  '下放自动上传的时间间隔
            insert_config_time()
        End If

        If m_configtag = 3 Then  '年时刻表配置

            insert_config_year()
        End If

        If m_configtag = 4 Then  '清除时刻表配置
            clear_config_year()

        End If

    End Sub

    ''' <summary>
    ''' 清除年配置
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub clear_config_year()
        '读出所有的需要配置的主控箱
        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim jiechuqi_num As String  '接触器个数
        Dim order_string As String = "" '命令字符串
        Dim sql, msg As String
        Dim conn As New ADODB.Connection
        Dim month, day As String  '
        Dim controlboxobj As New control_box
        Dim boxid_hex As String
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        Dim nowtime As DateTime = Now
        msg = ""

        If m_checklist.Count = 0 Then
            MsgBox("请选择需要清除年控制表的主控箱", , PROJECT_TITLE_STRING)
            Exit Sub
        End If
        While i < m_checklist.Count

            If controlboxobj.get_communication(m_checklist(i)) = False Then
                m_boxname = m_checklist(i)
                m_checklist.RemoveAt(i)
                '通信不正常则不测

                Me.BackgroundWorkerconfigarea.ReportProgress(5)

                System.Threading.Thread.Sleep(1000)
                Continue While
            End If


            '第一步获取接触器个数    最前面增加两个字节的月，日
            jiechuqi_num = "00 00"

            month = "0" & System.Convert.ToString(Now.Month, 16).ToUpper
            day = System.Convert.ToString(Now.Day, 16).ToUpper
            If day.Length = 1 Then
                day = "0" & day
            End If

            order_string = month & " " & day & " " & jiechuqi_num & " "

            '获取该主控箱的IMEI
            Dim imei_string As String
            imei_string = Me.get_imei(m_checklist(i))

            boxid_hex = Com_inf.Dec_to_Hex(m_controlboxid(i), 4)
            boxid_hex = Mid(boxid_hex, 1, 2) & " " & Mid(boxid_hex, 3, 2)
            order_string = boxid_hex & order_string

            '设置好接触器整年的控制命令后，将命令写入到数据库中
            sql = "insert into TimeControl(RoadIMEI, CMDType, CMDContent,CreateTime,HandlerFlag) values('" & imei_string & "','" & HG_TYPE.HG_SET_YEARCONFIG & "','" & Trim(order_string) & "','" & nowtime & "','" & CONTROL_BOX_TYPE2_FLAG & "')"
            DBOperation.ExecuteSQL(conn, sql, msg)

next1:
            i += 1
        End While

        conn.Close()
        conn = Nothing

        '发送完所有命令后，等待回复
        get_configreturn(nowtime)
    End Sub

    Private Sub insert_config_year()
        '读出所有的需要配置的主控箱
        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim jiechuqi_num As String  '接触器个数
        Dim pianyi_string As String  '开关偏移量字符串
        Dim jiechuqi() As String '接触器编号的数组
        Dim order_string As String = "" '命令字符串
        Dim id As String '接触器编号
        Dim divtime_string As String '设置半夜灯模式
        Dim sql, msg As String
        Dim conn As New ADODB.Connection
        Dim month, day As String  '
        Dim controlboxobj As New control_box
        '  Dim boxid_hex As String '主控箱的编号
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        Dim nowtime As DateTime = Now
        msg = ""

        If m_checklist.Count = 0 Then
            MsgBox("请选择需要配置年控制表的主控箱", , PROJECT_TITLE_STRING)
            Exit Sub
        End If
        While i < m_checklist.Count

            If controlboxobj.get_communication(m_checklist(i)) = False Then
                m_boxname = m_checklist(i)

                m_checklist.RemoveAt(i)
                '通信不正常则不测

                Me.BackgroundWorkerconfigarea.ReportProgress(5)

                System.Threading.Thread.Sleep(1000)
                Continue While
            End If



            '第一步获取接触器个数    最前面增加两个字节的月，日
            jiechuqi_num = "00 " & Me.get_jiechuqi_num(m_checklist(i))
            If jiechuqi_num = "" Then
                '如果没有设置接触器，则退出该主控箱的设置
                GoTo next1
            End If
            month = "0" & System.Convert.ToString(Now.Month, 16).ToUpper
            day = System.Convert.ToString(Now.Day, 16).ToUpper
            If day.Length = 1 Then
                day = "0" & day
            End If



            order_string = month & " " & day & " " & jiechuqi_num & " "

            '第二步获取偏移量
            pianyi_string = Me.get_offset(m_checklist(i))
            If pianyi_string = "" Then
                '表示没有设置经纬度控制
                order_string &= "00 00 "
            Else
                order_string &= pianyi_string & " "
            End If

            '根据每个接触器的编号进行相关的设置
            j = 0
            jiechuqi = Trim(m_jiechuqiid).Split(" ")
            While j < jiechuqi.Length

                id = Com_inf.Dec_to_Hex(Mid(jiechuqi(j), 7, LAMP_ID_LEN), 2)  '接触器编号


                '第三步设置半夜灯控制
                divtime_string = Me.get_divtimestring(jiechuqi(j))
                If divtime_string <> "" Then
                    order_string &= id & " 02 " & divtime_string & " 00 00 "

                Else
                    '第四步没有半夜灯设置经纬度
                    If pianyi_string <> "" Then
                        order_string &= id & " 01 00 00 00 00 "
                    End If

                End If

                j += 1
            End While

            '获取该主控箱的IMEI
            Dim imei_string As String
            imei_string = Me.get_imei(m_checklist(i))

            'boxid_hex = Com_inf.Dec_to_Hex(m_controlboxid(i), 4)
            'boxid_hex = Mid(boxid_hex, 1, 2) & " " & Mid(boxid_hex, 3, 2)
            'order_string = boxid_hex & " " & order_string

            '设置好接触器整年的控制命令后，将命令写入到数据库中
            sql = "insert into TimeControl(RoadIMEI, CMDType, CMDContent,CreateTime,HandlerFlag) values('" & imei_string & "','" & HG_TYPE.HG_SET_YEARCONFIG & "','" & Trim(order_string) & "','" & nowtime & "','" & CONTROL_BOX_TYPE2_FLAG & "')"
            DBOperation.ExecuteSQL(conn, sql, msg)

next1:
            i += 1
        End While

        conn.Close()
        conn = Nothing

        '发送完所有命令后，等待回复
        get_configreturn(nowtime)

    End Sub

    Private Sub get_configreturn(ByVal gettime As DateTime)
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim i As Integer = 0
        Dim state As String '返回的状态
        Dim j As Integer = 0
        Dim boxid_hex As String '主控箱的16进制标号

        msg = ""
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If

        While i < CONTROL_WAIT_TIME
            If Me.BackgroundWorkerconfigarea.CancellationPending = False Then
                j = 0
                If m_checklist.Count = 0 Then
                    Exit While
                End If
                m_string = "正在配置一年时刻表......" & i + 1 & vbCrLf
                Me.BackgroundWorkerconfigarea.ReportProgress(4)
                While j < m_checklist.Count

                    boxid_hex = Com_inf.Dec_to_Hex(m_controlboxid(j), 4)
                    boxid_hex = Mid(boxid_hex, 1, 2) & " " & Mid(boxid_hex, 3, 2)

                    sql = "select * from RoadLightStatus where PackType='" & HG_TYPE.HG_GET_YEARCONFIG & "'" _
                                     & " and Createtime>'" & gettime & "' and HandlerFlag=3 and StatusContent like'" & boxid_hex & "%'"

                    rs = DBOperation.SelectSQL(conn, sql, msg)
                    If rs Is Nothing Then
                        MsgBox(MSG_ERROR_STRING & "get_back_configtimeinf", , PROJECT_TITLE_STRING)
                        conn.Close()
                        conn = Nothing
                        Exit Sub
                    End If
                    If rs.RecordCount > 0 Then

                        state = Mid(Trim(rs.Fields("StatusContent").Value), 7, 2)
                        If state = "01" Then
                            m_string = m_checklist(j) & "配置一年时刻表成功 时间：" & Now & vbCrLf & vbCrLf
                            Me.BackgroundWorkerconfigarea.ReportProgress(4)
                            '将配置信息保存
                            ' MsgBox(m_boxname & "配置" & " " & m_configtitle & "成功", , PROJECT_TITLE_STRING)
                            g_welcomewinobj.Insert_configinf(m_checklist(j) & "配置一年时刻表成功", 1, m_controlboxid(j), m_checklist(j))
                        Else
                            m_string = m_checklist(j) & "配置一年时刻表失败 时间：" & Now & vbCrLf & vbCrLf
                            Me.BackgroundWorkerconfigarea.ReportProgress(4)
                            'g_welcomewinobj.SetTextDelegate(m_boxname & "配置" & " " & m_configtitle & "失败 时间：" & Now & vbCrLf, False, config_string)
                            '将配置信息保存
                            'MsgBox(m_boxname & "配置" & " " & m_configtitle & "失败", , PROJECT_TITLE_STRING)
                            g_welcomewinobj.Insert_configinf(m_checklist(j) & "配置一年时刻表失败", 1, m_controlboxid(j), m_checklist(j))

                        End If

                        '将查找到的数据置为1
                        rs.Fields("HandlerFlag").Value = 1
                        rs.Update()


                        m_checklist.RemoveAt(j)
                        m_controlboxid.RemoveAt(j)

                    Else
                        j += 1
                    End If
                End While

                System.Threading.Thread.Sleep(1000)

            Else
                m_string = "配置一年时刻表终止 时间：" & Now & vbCrLf & vbCrLf
                Me.BackgroundWorkerconfigarea.ReportProgress(4)
                Exit While
            End If
            i += 1

        End While
        If i >= CONTROL_WAIT_TIME Then
            j = 0
            While j < m_checklist.Count
                m_string = m_checklist(j) & "配置一年时刻表超时 时间：" & Now & vbCrLf & vbCrLf
                Me.BackgroundWorkerconfigarea.ReportProgress(4)
                'g_welcomewinobj.SetTextDelegate(m_boxname & "配置" & " " & m_configtitle & "超时 时间：" & Now & vbCrLf, False, config_string)
                '将配置信息保存
                'MsgBox(m_boxname & "配置" & " " & m_configtitle & "超时", , PROJECT_TITLE_STRING)
                g_welcomewinobj.Insert_configinf(m_checklist(j) & "配置一年时刻表超时", 0, m_controlboxid(j), m_checklist(j))

                j += 1
            End While

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
    ''' 将设置自动上传的时间插入数据库
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub insert_config_time()
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String
        Dim rs As New ADODB.Recordset
        Dim i As Integer = 0
        Dim imei As String
        Dim id_hex As String
        Dim time As DateTime = Now
        Dim box_id As String
        Dim controlboxobj As New control_box
        m_autotime = Com_inf.Dec_to_Hex(m_autotime, 4)
        m_autotime = Mid(m_autotime, 1, 2) & " " & Mid(m_autotime, 3, 2)

        msg = ""
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        If m_checklist.Count = 0 Then
            MsgBox("请选择遥测主控箱", , PROJECT_TITLE_STRING)
            Exit Sub
        End If
        Dim boxname(m_checklist.Count - 1) As String
        m_checklist.CopyTo(boxname)

        While i < m_checklist.Count
            If Me.BackgroundWorkerconfigarea.CancellationPending = False Then
                sql = "select imei,control_box_id,control_box_name from Box_IMEI where control_box_name='" & boxname(i) & "'"
                rs = DBOperation.SelectSQL(conn, sql, msg)
                If rs Is Nothing Then
                    MsgBox(MSG_ERROR_STRING, , PROJECT_TITLE_STRING)
                    conn.Close()
                    conn = Nothing
                    Exit Sub
                End If
                If rs.RecordCount > 0 Then
                    m_boxname = Trim(rs.Fields("control_box_name").Value)
                    id_hex = Com_inf.Dec_to_Hex(Trim(tb_autowaittime.Text), 4)
                    id_hex = Mid(id_hex, 1, 2) & " " & Mid(id_hex, 3, 2)

                    box_id = Trim(rs.Fields("control_box_id").Value)
                    box_id = Com_inf.Dec_to_Hex(box_id, 4)
                    box_id = Mid(box_id, 1, 2) & " " & Mid(box_id, 3, 2)
                    id_hex = box_id & " " & id_hex
                    '发送时间配置
                    imei = Trim(rs.Fields("imei").Value)
                    sql = "insert into TimeControl(RoadIMEI, CMDType,CMDContent, CreateTime,HandlerFlag) values('" & imei & "', '" & HG_TYPE.HG_SET_WAITTIME & "','" & id_hex & "','" & Now & "','" & CONTROL_BOX_TYPE2_FLAG & "')"
                    DBOperation.ExecuteSQL(conn, sql, msg)
                    System.Threading.Thread.Sleep(1000)

                    get_back_configtimeinf(box_id, time)

                End If
            Else
                GoTo finish
            End If

            i += 1
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
    ''' 查询是否有回复的信息，有回复信息则配置成功，否则显示超时
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub get_back_configtimeinf(ByVal id_hex As String, ByVal time As DateTime)
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim i As Integer = 0
        Dim state As String '返回的状态

        msg = ""
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        m_string = "正在配置" & m_boxname & "自动上传时间......" & vbCrLf
        Me.BackgroundWorkerconfigarea.ReportProgress(3)
        While i < CONTROL_WAIT_TIME
            sql = "select * from RoadLightStatus where PackType='" & HG_TYPE.HG_GET_WAITIME & "'" _
                   & " and Createtime>'" & time & "' and HandlerFlag=3 and StatusContent like'" & id_hex & "%'"

            rs = DBOperation.SelectSQL(conn, sql, msg)
            If rs Is Nothing Then
                MsgBox(MSG_ERROR_STRING & "get_back_configtimeinf", , PROJECT_TITLE_STRING)
                conn.Close()
                conn = Nothing
                Exit Sub
            End If
            If rs.RecordCount > 0 Then

                state = Mid(Trim(rs.Fields("StatusContent").Value), 7, 2)
                If state = "01" Then
                    m_string = m_boxname & "配置自动上传时间成功 时间：" & Now & vbCrLf & vbCrLf
                    Me.BackgroundWorkerconfigarea.ReportProgress(3)
                    '将配置信息保存
                    ' MsgBox(m_boxname & "配置" & " " & m_configtitle & "成功", , PROJECT_TITLE_STRING)
                    g_welcomewinobj.Insert_configinf(m_boxname & "配置自动上传时间成功", 1, m_boxid, m_boxname)
                Else
                    m_string = m_boxname & "配置自动上传时间失败 时间：" & Now & vbCrLf & vbCrLf
                    Me.BackgroundWorkerconfigarea.ReportProgress(3)
                    'g_welcomewinobj.SetTextDelegate(m_boxname & "配置" & " " & m_configtitle & "失败 时间：" & Now & vbCrLf, False, config_string)
                    '将配置信息保存
                    'MsgBox(m_boxname & "配置" & " " & m_configtitle & "失败", , PROJECT_TITLE_STRING)
                    g_welcomewinobj.Insert_configinf(m_boxname & "配置自动上传时间失败", 1, m_boxid, m_boxname)

                End If

                '将查找到的数据置为1
                rs.Fields("HandlerFlag").Value = 1
                rs.Update()
                GoTo finish
            End If
            i += 1
            System.Threading.Thread.Sleep(1000)
        End While
        If i >= CONTROL_WAIT_TIME Then
            m_string = m_boxname & "配置自动上传时间超时 时间：" & Now & vbCrLf & vbCrLf
            Me.BackgroundWorkerconfigarea.ReportProgress(3)
            'g_welcomewinobj.SetTextDelegate(m_boxname & "配置" & " " & m_configtitle & "超时 时间：" & Now & vbCrLf, False, config_string)
            '将配置信息保存
            'MsgBox(m_boxname & "配置" & " " & m_configtitle & "超时", , PROJECT_TITLE_STRING)
            g_welcomewinobj.Insert_configinf(m_boxname & "配置自动上传时间超时", 0, m_boxid, m_boxname)

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
    ''' 查询是否有回复的信息，有回复信息则配置成功，否则显示超时
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub get_back_inf(ByVal time As DateTime)
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim i As Integer = 0
        ' Dim box_id As String
        Dim state As String '返回的状态

        msg = ""
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If

        While i < CONTROL_WAIT_TIME
            sql = "select * from RoadLightStatus where PackType='" & HG_TYPE.HG_GET_CONTROL_BOX_ID & "'" _
                   & " and Createtime>'" & time & "' and HandlerFlag=3"

            rs = DBOperation.SelectSQL(conn, sql, msg)
            If rs Is Nothing Then
                MsgBox(MSG_ERROR_STRING & "Get_Backinf", , PROJECT_TITLE_STRING)
                conn.Close()
                conn = Nothing
                Exit Sub
            End If
            If rs.RecordCount > 0 Then
                '2011年10月21日修改为收到回复不判断主控箱编号，直接判断成功与否的标志位
                'box_id = Com_inf.Dec_to_Hex(m_boxid, 4)
                ' box_id = Mid(box_id, 1, 2) & " " & Mid(box_id, 3, 2)
                'If box_id = Mid(Trim(rs.Fields("StatusContent").Value), 1, 5) Then
                state = Mid(Trim(rs.Fields("StatusContent").Value), 7, 2)
                If state = "01" Then
                    m_string = m_boxname & "配置" & " " & m_configtitle & "成功 时间：" & Now
                    Me.BackgroundWorkerconfigarea.ReportProgress(2)
                    '将配置信息保存
                    ' MsgBox(m_boxname & "配置" & " " & m_configtitle & "成功", , PROJECT_TITLE_STRING)
                    g_welcomewinobj.Insert_configinf(m_boxname & " " & m_configstring & "配置" & m_configtitle & "成功", 1, m_boxid, m_boxname)
                    If m_inter_config = "1" Then
                        Update_configinf()
                    End If
                Else
                    m_string = m_boxname & "配置" & " " & m_configtitle & "失败 时间：" & Now
                    Me.BackgroundWorkerconfigarea.ReportProgress(2)
                    'g_welcomewinobj.SetTextDelegate(m_boxname & "配置" & " " & m_configtitle & "失败 时间：" & Now & vbCrLf, False, config_string)
                    '将配置信息保存
                    'MsgBox(m_boxname & "配置" & " " & m_configtitle & "失败", , PROJECT_TITLE_STRING)
                    g_welcomewinobj.Insert_configinf(m_boxname & " " & m_configstring & "配置" & m_configtitle & "失败", 1, m_boxid, m_boxname)

                End If
                GoTo finish
                'End If
            End If
            i += 1
            System.Threading.Thread.Sleep(1000)
        End While
        If i >= CONTROL_WAIT_TIME Then
            m_string = m_boxname & "配置" & " " & m_configtitle & "超时 时间：" & Now
            Me.BackgroundWorkerconfigarea.ReportProgress(2)
            'g_welcomewinobj.SetTextDelegate(m_boxname & "配置" & " " & m_configtitle & "超时 时间：" & Now & vbCrLf, False, config_string)
            '将配置信息保存
            'MsgBox(m_boxname & "配置" & " " & m_configtitle & "超时", , PROJECT_TITLE_STRING)
            g_welcomewinobj.Insert_configinf(m_boxname & " " & m_configstring & "配置" & m_configtitle & "超时", 0, m_boxid, m_boxname)

        End If

finish:
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing


    End Sub


    ''' 将配置区域信息存储到数据库中
    Public Sub Update_configinf()
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim i As Integer = 0
        Dim inter_from_to() As String
        Dim inter_fromto As String
        msg = ""
        '配置区域信息
        sql = "UPDATE control_box SET inter_count = " & m_inter_count & " WHERE control_box_id=" & m_boxid & ""
        DBOperation.OpenConn(conn)
        DBOperation.ExecuteSQL(conn, sql, msg)
        sql = "UPDATE lamp_inf SET inter_count =null WHERE control_box_id=" & m_boxid & ""
        DBOperation.ExecuteSQL(conn, sql, msg)
        Dim from_to() As String
        from_to = m_from_to.Split(",")
        While i < from_to.Length - 1
            inter_fromto = from_to(i)
            inter_from_to = inter_fromto.Split("_")
            sql = "UPDATE lamp_inf SET inter_count = " & i + 1 & " WHERE control_box_id=" & m_boxid & " AND  (right(LAMP_ID,len(LAMP_ID)-4) BETWEEN " & inter_from_to(0) & " AND " & (CInt(inter_from_to(0)) + CInt(inter_from_to(1)) - 1) & ")"
            DBOperation.ExecuteSQL(conn, sql, msg)
            i += 1
        End While
        conn.Close()
        conn = Nothing
    End Sub
    ''' <summary>
    ''' 插入配置命令
    ''' </summary>
    ''' <returns>是否发送命令成功</returns>
    ''' <remarks></remarks>
    Private Function insert_config_string() As Boolean
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim rs As New ADODB.Recordset
        Dim imei As String
        Dim flag As Integer = 1 '主控箱类型的标志

        msg = ""
        sql = ""
        If DBOperation.OpenConn(conn) = False Then
            Exit Function
        End If
        sql = "select IMEI, control_box_name,control_box_type from Box_IMEI where control_box_id='" & m_boxid & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "Insert_Configstring", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            insert_config_string = False
            Exit Function
        End If
        If rs.RecordCount <= 0 Then
            MsgBox("主控箱编号不存在，请重新选择", , PROJECT_TITLE_STRING)
            rs.Close()
            rs = Nothing
            conn.Close()
            conn = Nothing
            insert_config_string = False
            Exit Function
        End If
        imei = Trim(rs.Fields("IMEI").Value)
        m_boxname = Trim(rs.Fields("control_box_name").Value)
        If rs.RecordCount > 0 Then
            If rs.Fields("control_box_type").Value Is System.DBNull.Value Then
                flag = 1
            Else
                flag = Val(Trim(rs.Fields("control_box_type").Value))
            End If
        Else
            flag = 1
        End If

        If flag = 1 Then
            sql = "insert into TimeControl(RoadIMEI,CMDType,CMDContent,HandlerFlag,Createtime) " _
            & "values('" & imei & "','" & HG_TYPE.HG_SET_CONTROL_BOX_ID & "','" & m_configstring & "','" & CONTROL_BOX_TYPE1_FLAG & "','" & Now & "')"

        Else
            sql = "insert into TimeControl(RoadIMEI,CMDType,CMDContent,HandlerFlag,Createtime) " _
            & "values('" & imei & "','" & HG_TYPE.HG_SET_CONTROL_BOX_ID & "','" & m_configstring & "','" & CONTROL_BOX_TYPE2_FLAG & "','" & Now & "')"

        End If

        DBOperation.ExecuteSQL(conn, sql, msg)
        insert_config_string = True

        ' MsgBox("配置命令执行完毕！", , PROJECT_TITLE_STRING)
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing

    End Function

    ''' <summary>
    ''' 将设置的区间配置，全部清空
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_clear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_clear.Click
        dgv_config_list.Rows.Clear()
        '恢复允许配置其他主控箱的信息
        cb_control_box_name.Enabled = True
        tb_control_box_id.Enabled = True
    End Sub

    ''' <summary>
    ''' 选择设置路段号
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rb_setbox_id_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_setbox_id.Click
        If rb_setbox_id.Checked = True Then
            tb_config_len.Text = ""
            tb_config_startid.Text = ""
            tb_config_startid.Enabled = False
            tb_config_len.Enabled = False
            bt_add_config.Enabled = False
            cb_type_id.Enabled = False
            m_configtitle = "路段号"
            dgv_config_list.Rows.Clear()
            m_inter_config = "0"
        End If
    End Sub
    ''' <summary>
    ''' 选择设置电控箱区间设置
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rb_setbox_area_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_setbox_area.Click
        If rb_setbox_area.Checked = True Then
            tb_config_startid.Enabled = True
            tb_config_len.Enabled = True
            bt_add_config.Enabled = True
            cb_type_id.Enabled = True
            m_configtitle = "区间"
            m_inter_config = "1"
            Me.bt_clean_config.Visible = True
        End If
    End Sub


    Private Sub BackgroundWorkerconfigarea_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorkerconfigarea.RunWorkerCompleted
        g_welcomewinobj.get_boxprobleminf()  '刷新故障信息
        'config_string.Visible = False
    End Sub



    ''' <summary>
    ''' 设置招测的等待时间
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_setzctime_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_setzctime.Click
        If IsNumeric(Trim(tb_zc_waittime.Text)) = False Then  '控制值必须为数字
            MsgBox("请输入数值", , PROJECT_TITLE_STRING)
            tb_zc_waittime.Focus()  '招测等待时间
            Exit Sub
        End If
        If Val(Trim(tb_zc_waittime.Text)) > 20 Then
            MsgBox("召测等待时间最大不超过20", , PROJECT_TITLE_STRING)
            tb_zc_waittime.Focus() '召测等待时间
            Exit Sub
        End If

        Try
            Dim rs As New ADODB.Recordset
            Dim conn As New ADODB.Connection
            Dim msg As String
            Dim sql As String

            msg = ""
            '配置召测等待时间
            sql = "select * from sysconfig where type='召测等待时间'"
            DBOperation.OpenConn(conn)
            rs = DBOperation.SelectSQL(conn, sql, msg)
            If rs Is Nothing Then
                MsgBox(MSG_ERROR_STRING & "set_lamp_Click", , PROJECT_TITLE_STRING)
                conn.Close()
                conn = Nothing
                Exit Sub
            End If
            If rs.RecordCount > 0 Then
                rs.Fields("name").Value = Trim(tb_zc_waittime.Text)
                rs.Update()
            Else
                rs.AddNew()
                rs.Fields("type").Value = "召测等待时间"
                rs.Fields("name").Value = Trim(tb_zc_waittime.Text)
                rs.Update()
            End If


            MsgBox("召测等待时间设置成功！", , PROJECT_TITLE_STRING)
            '增加操作记录
            Com_inf.Insert_Operation("设置召测等待时间：" & Trim(tb_zc_waittime.Text))

            g_ycwaittime = Val(Trim(tb_zc_waittime.Text))

            If rs.State = 1 Then
                rs.Close()
                rs = Nothing
            End If
            conn.Close()
            conn = Nothing
        Catch ex As Exception

            MsgBox(ex.Message, , PROJECT_TITLE_STRING)
        End Try

    End Sub

    ''' <summary>
    ''' 设置自动上传的时间
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_setautotime_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_setautotime.Click
        If Me.BackgroundWorkerconfigarea.IsBusy = False Then
            If tb_autowaittime.Text = "" Then
                MsgBox("请输入时间，单位（分）", , PROJECT_TITLE_STRING)
                tb_autowaittime.Focus()
                Exit Sub
            End If
            If IsNumeric(Trim(tb_autowaittime.Text)) = False Then
                MsgBox("请输入大于零的数字", , PROJECT_TITLE_STRING)
            End If
            m_configtag = 2
            m_autotime = Trim(tb_autowaittime.Text)
            Dim tnRet As New TreeNode
            For Each treenode As TreeNode In tv_boxlist.Nodes
                Com_inf.FindNode(treenode, m_checklist)
            Next
            Me.BackgroundWorkerconfigarea.RunWorkerAsync()  '


        Else
            MsgBox("配置正在进行，请稍后重试!", , PROJECT_TITLE_STRING)
        End If
    End Sub

    Private Sub BackgroundWorkerconfigarea_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorkerconfigarea.ProgressChanged
        If e.ProgressPercentage = 1 Then
            config_string.Visible = True
        End If
        If e.ProgressPercentage = 2 Then
            config_string.Text = m_string
        End If
        If e.ProgressPercentage = 3 Then
            config_text.AppendText(m_string)
            config_text.Select(config_text.Text.Length, 0)
            config_text.ScrollToCaret()
        End If
        If e.ProgressPercentage = 4 Then
            rtb_returnvalue.AppendText(m_string)
            rtb_returnvalue.Select(rtb_returnvalue.Text.Length, 0)
            rtb_returnvalue.ScrollToCaret()
        End If

        If e.ProgressPercentage = 5 Then
            rtb_returnvalue.AppendText("主控箱：" & m_boxname & "未连接无法配置" & vbCrLf)
        End If
    End Sub

    Private Sub tv_boxlist_AfterCheck(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tv_boxlist.AfterCheck
        Dim controlboxobj As New control_box
        If m_check = False Then
            m_check = controlboxobj.setChild(e.Node)
        End If

        controlboxobj.setparent(e.Node)
        m_check = False
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If IsNumeric(Trim(tb_zc_jgtime.Text)) = False Then  '控制值必须为数字
            MsgBox("请输入数值", , PROJECT_TITLE_STRING)
            tb_zc_jgtime.Focus()  '招测等待时间
            Exit Sub
        End If
        If Val(Trim(tb_zc_jgtime.Text)) > 20 Then
            MsgBox("召测间隔时间最大不超过20", , PROJECT_TITLE_STRING)
            tb_zc_jgtime.Focus() '召测等待时间
            Exit Sub
        End If

        Try
            Dim rs As New ADODB.Recordset
            Dim conn As New ADODB.Connection
            Dim msg As String
            Dim sql As String

            msg = ""
            '配置召测等待时间
            sql = "select * from sysconfig where type='召测间隔时间'"
            DBOperation.OpenConn(conn)
            rs = DBOperation.SelectSQL(conn, sql, msg)
            If rs Is Nothing Then
                MsgBox(MSG_ERROR_STRING & "set_lamp_Click", , PROJECT_TITLE_STRING)
                conn.Close()
                conn = Nothing
                Exit Sub
            End If
            If rs.RecordCount > 0 Then
                rs.Fields("name").Value = Trim(tb_zc_jgtime.Text)
                rs.Update()
            Else
                rs.AddNew()
                rs.Fields("type").Value = "召测间隔时间"
                rs.Fields("name").Value = Trim(tb_zc_jgtime.Text)
                rs.Update()
            End If


            MsgBox("召测间隔时间设置成功！", , PROJECT_TITLE_STRING)
            '增加操作记录
            Com_inf.Insert_Operation("设置召测间隔时间：" & Trim(tb_zc_jgtime.Text))

            g_ycjgtime = Val(Trim(tb_zc_jgtime.Text))

            If rs.State = 1 Then
                rs.Close()
                rs = Nothing
            End If
            conn.Close()
            conn = Nothing
        Catch ex As Exception

            MsgBox(ex.Message, , PROJECT_TITLE_STRING)
        End Try

    End Sub

    Private Sub cb_control_box_name_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_control_box_name.DropDown
        Com_inf.Select_box_name(cb_control_box_name)
    End Sub

    Private Sub cb_control_box_name_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_control_box_name.SelectedIndexChanged
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String

        msg = ""
        sql = "select * from control_box where control_box_name='" & Trim(cb_control_box_name.Text) & "'"
        DBOperation.OpenConn(conn)
        rs = DBOperation.SelectSQL(conn, sql, msg)

        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "cb_control_box_name_SelectedIndexChanged", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs.RecordCount > 0 Then
            tb_control_box_id.Text = Trim(rs.Fields("control_box_id").Value)

        End If

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing

    End Sub

    ''' <summary>
    ''' 将配置字符串下放下去
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_yearconfig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_yearconfig.Click
        If Me.BackgroundWorkerconfigarea.IsBusy = False Then
            m_configtag = 3
            If m_checklist.Count > 0 Then
                m_checklist.Clear()
            End If
            Dim tnRet As New TreeNode
            For Each treenode As TreeNode In tv_yearconfig.Nodes
                Com_inf.FindNode(treenode, m_checklist)
            Next

            If m_controlboxid.Count > 0 Then
                m_controlboxid.Clear()
            End If

            Me.BackgroundWorkerconfigarea.RunWorkerAsync()
        Else
            MsgBox("系统配置正在进行，请稍后重试...", , PROJECT_TITLE_STRING)

        End If

    End Sub



    ''' <summary>
    ''' 通过主控箱名称获取IMEI
    ''' </summary>
    ''' <param name="control_box_name"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function get_imei(ByVal control_box_name As String) As String
        Dim sql As String
        Dim conn As New ADODB.Connection
        Dim rs As New ADODB.Recordset
        Dim msg As String
        get_imei = ""
        msg = ""
        sql = "select * from Box_IMEI where control_box_name='" & control_box_name & "'"
        If DBOperation.OpenConn(conn) = False Then
            get_imei = ""
            Exit Function
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            conn.Close()
            conn = Nothing
        End If
        If rs.RecordCount > 0 Then
            get_imei = Trim(rs.Fields("IMEI").Value)
            Dim controlboxid As String = Trim(rs.Fields("control_box_id").Value)
            m_controlboxid.Add(controlboxid)  '主控箱编号

        End If

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing

    End Function

    ''' <summary>
    ''' 根据主控箱的名称获取接触器个数，及接触器编号(十六进制)
    ''' </summary>
    ''' <param name="control_box_name"></param>
    ''' <remarks></remarks>
    Private Function get_jiechuqi_num(ByVal control_box_name As String) As String
        Dim sql As String
        Dim conn As New ADODB.Connection
        Dim rs As New ADODB.Recordset
        Dim msg As String
        get_jiechuqi_num = ""
        m_jiechuqiid = ""
        msg = ""
        sql = "select lamp_id  from lamp_street where control_box_name='" & control_box_name & "' and type_id=31 order by lamp_id"
        If DBOperation.OpenConn(conn) = False Then
            get_jiechuqi_num = ""
            Exit Function
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            conn.Close()
            conn = Nothing
        End If
        If rs.RecordCount > 0 Then
            get_jiechuqi_num = Com_inf.Dec_to_Hex(rs.RecordCount, 2)
            While rs.EOF = False
                m_jiechuqiid &= Trim(rs.Fields("lamp_id").Value) & " "

                rs.MoveNext()
            End While

        End If

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing

    End Function

    ''' <summary>
    ''' 获取主控箱的偏移量
    ''' </summary>
    ''' <param name="control_box_name"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function get_offset(ByVal control_box_name As String) As String
        Dim sql As String
        Dim conn As New ADODB.Connection
        Dim rs As New ADODB.Recordset
        Dim msg As String
        Dim offsetopen, offsetclose As SByte
        Dim str1, str2 As String


        get_offset = ""
        msg = ""
        sql = "SELECT   pianyi.open_pianyi, pianyi.close_pianyi, control_box.control_box_id, control_box.control_box_name " _
              & "FROM   pianyi INNER JOIN lamp_inf ON pianyi.lamp_id = lamp_inf.lamp_id INNER JOIN " _
              & "control_box ON lamp_inf.control_box_id = control_box.control_box_id where control_box.control_box_name='" & control_box_name & "'"

        If DBOperation.OpenConn(conn) = False Then
            get_offset = ""
            Exit Function
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            conn.Close()
            conn = Nothing
        End If
        If rs.RecordCount > 0 Then
            offsetopen = System.Convert.ToSByte(rs.Fields("open_pianyi").Value)
            offsetclose = System.Convert.ToSByte(rs.Fields("close_pianyi").Value)

            str1 = System.Convert.ToString(offsetopen, 16).ToUpper
            str2 = System.Convert.ToString(offsetclose, 16).ToUpper
            If str1.Length < 2 Then
                str1 = "0" & str1
            End If
            If str2.Length < 2 Then
                str2 = "0" & str2
            End If
            If str1.Length = 4 Then
                str1 = Mid(str1, 3, 2)

            End If
            If str2.Length = 4 Then
                str2 = Mid(str2, 3, 2)

            End If

            get_offset = str1 & " " & str2

        End If

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing

    End Function


    ''' <summary>
    ''' 获取半夜灯的关灯时间
    ''' </summary>
    ''' <param name="lamp_id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function get_divtimestring(ByVal lamp_id As String) As String
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String
        Dim rs, rs_divtime As New ADODB.Recordset
        Dim div_time_level As String = "" '模式名称

        sql = ""
        msg = ""
        get_divtimestring = ""
        If DBOperation.OpenConn(conn) = False Then
            get_divtimestring = ""
            Exit Function
        End If
        sql = "SELECT road_level.div_time_level FROM road_level INNER JOIN " _
        & "control_box ON road_level.control_box_id = control_box.control_box_id " _
        & "where lamp_id='" & lamp_id & "' group by div_time_level"

        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            conn.Close()
            conn = Nothing
            get_divtimestring = ""
            Exit Function
        End If
        While rs.EOF = False
            div_time_level = Trim(rs.Fields("div_time_level").Value)
            '根据模式名称查询半夜灯的关灯时间

            sql = "select * from div_time where div_level='" & div_time_level & "' and hour_end=0"
            rs_divtime = DBOperation.SelectSQL(conn, sql, msg)
            If rs_divtime Is Nothing Then
                rs.Close()
                rs = Nothing
                conn.Close()
                conn = Nothing
                Exit Function
            End If

            While rs_divtime.EOF = False
                If Trim(rs_divtime.Fields("mod").Value) = "回路关" Then
                    '找到回路关的命令
                    get_divtimestring = Com_inf.Dec_to_Hex(Trim(rs_divtime.Fields("hour_beg").Value), 2) & " " & Com_inf.Dec_to_Hex(Trim(rs_divtime.Fields("min_beg").Value), 2)
                    GoTo finish
                End If
                rs_divtime.MoveNext()
            End While

            rs.MoveNext()
        End While
finish:

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        If rs_divtime.State = 1 Then
            rs_divtime.Close()
            rs_divtime = Nothing
        End If
        conn.Close()
        conn = Nothing

    End Function

    Private Sub tv_yearconfig_AfterCheck(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tv_yearconfig.AfterCheck
        Dim controlboxobj As New control_box
        If m_check = False Then
            m_check = controlboxobj.setChild(e.Node)
        End If

        controlboxobj.setparent(e.Node)
        m_check = False
    End Sub

    Private Sub 系统参数设置_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing

        If Me.BackgroundWorkerconfigarea.IsBusy = True Then
            MsgBox("线程正在运行，请稍后关闭", , PROJECT_TITLE_STRING)
            e.Cancel = True
        End If
        g_windowclose = 1
    End Sub

    Private Sub bt_stopconfig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_stopconfig.Click
        If (m_configtag = 3 Or m_configtag = 4) And Me.BackgroundWorkerconfigarea.IsBusy = True Then
            Me.BackgroundWorkerconfigarea.CancelAsync()
        End If
    End Sub

    Private Sub bt_clearconfig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_clearconfig.Click
        rtb_returnvalue.Clear()
    End Sub


    Private Sub bt_clearyearconfig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_clearyearconfig.Click
        If Me.BackgroundWorkerconfigarea.IsBusy = False Then
            m_configtag = 4
            If m_checklist.Count > 0 Then
                m_checklist.Clear()
            End If
            Dim tnRet As New TreeNode
            For Each treenode As TreeNode In tv_yearconfig.Nodes
                Com_inf.FindNode(treenode, m_checklist)
            Next

            If m_controlboxid.Count > 0 Then
                m_controlboxid.Clear()
            End If

            Me.BackgroundWorkerconfigarea.RunWorkerAsync()
        Else
            MsgBox("系统配置正在进行，请稍后重试...", , PROJECT_TITLE_STRING)

        End If
    End Sub

    ''' <summary>
    ''' 设置经纬度
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_jwset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_jwset.Click
        If Trim(tb_jingdu.Text = "") Then
            MsgBox("请设置经度", , PROJECT_TITLE_STRING)
            tb_jingdu.Focus()
            Exit Sub
        End If
        If Trim(tb_weidu.Text = "") Then
            MsgBox("请设置纬度", , PROJECT_TITLE_STRING)
            tb_weidu.Focus()
            Exit Sub
        End If


        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String
        Dim setvalue As String = "经度"

        msg = ""
        sql = "select * from sysconfig where type='" & setvalue & "'"
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
        If rs.RecordCount > 0 Then
            rs.Fields("name").Value = System.Convert.ToDouble(Trim(tb_jingdu.Text))
            rs.Update()
        Else
            rs.AddNew()
            rs.Fields("type").Value = setvalue
            rs.Fields("name").Value = System.Convert.ToDouble(Trim(tb_jingdu.Text))
            rs.Update()
        End If

        '设置纬度
        setvalue = "纬度"
        sql = "select * from sysconfig where type='" & setvalue & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING, , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs.RecordCount > 0 Then
            rs.Fields("name").Value = System.Convert.ToDouble(Trim(tb_weidu.Text))
            rs.Update()
        Else
            rs.AddNew()
            rs.Fields("type").Value = setvalue
            rs.Fields("name").Value = System.Convert.ToDouble(Trim(tb_weidu.Text))
            rs.Update()
        End If

        MsgBox("经纬度设置成功", , PROJECT_TITLE_STRING)

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    Private Sub bt_setdelaytime_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_setdelaytime.Click
        If Trim(tb_delaytime.Text = "") Then
            MsgBox("请设置自动报警延时时间", , PROJECT_TITLE_STRING)
            tb_delaytime.Focus()
            Exit Sub
        End If


        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String
        Dim setvalue As String = "报警延时"

        msg = ""
        sql = "select * from sysconfig where type='" & setvalue & "'"
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
        If rs.RecordCount > 0 Then
            rs.Fields("name").Value = System.Convert.ToInt32(Trim(tb_delaytime.Text))
            rs.Update()
        Else
            rs.AddNew()
            rs.Fields("type").Value = setvalue
            rs.Fields("name").Value = System.Convert.ToInt32(Trim(tb_delaytime.Text))
            rs.Update()
        End If


        MsgBox("自动报警延时时间设置成功", , PROJECT_TITLE_STRING)

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    Private Sub cb_type_id_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_type_id.DropDown
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String
        Dim rs As New ADODB.Recordset

        msg = ""
        sql = "select type_id from lamp_type order by type_id"
        DBOperation.OpenConn(conn)

        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "set_lamp_Click", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If

        cb_type_id.Items.Clear()
        While rs.EOF = False
            cb_type_id.Items.Add(rs.Fields("type_id").Value)
            rs.MoveNext()

        End While

        If cb_type_id.Items.Count > 0 Then
            cb_type_id.SelectedIndex = 0
        End If

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    ''' 设置路灯的光照度开关阈值
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_setlight_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_setlight.Click
        If IsNumeric(Trim(tb_light_value.Text)) = False Then  '控制值必须为数字
            MsgBox("请输入数值", , PROJECT_TITLE_STRING)
            tb_light_value.Focus()  '光标定位在光照阈值上
            Exit Sub
        End If

        Try
            Dim rs As New ADODB.Recordset
            Dim conn As New ADODB.Connection
            Dim msg As String
            Dim sql As String

            msg = ""
            '设置光照阈值
            sql = "select * from sysconfig where type='光照阈值'"
            DBOperation.OpenConn(conn)
            rs = DBOperation.SelectSQL(conn, sql, msg)
            If rs Is Nothing Then
                MsgBox(MSG_ERROR_STRING & "set_lamp_Click", , PROJECT_TITLE_STRING)
                conn.Close()
                conn = Nothing
                Exit Sub
            End If
            If rs.RecordCount > 0 Then
                rs.Fields("name").Value = Trim(tb_light_value.Text)
                rs.Update()
            Else
                rs.AddNew()
                rs.Fields("type").Value = "光照阈值"
                rs.Fields("name").Value = Trim(tb_light_value.Text)
                rs.Update()
            End If


            MsgBox("光照阈值编辑成功！", , PROJECT_TITLE_STRING)
            '增加操作记录
            Com_inf.Insert_Operation("配置光照阈值：" & Trim(tb_light_value.Text))

            g_lightvalueset = System.Convert.ToDouble(Trim(tb_light_value.Text))

            If rs.State = 1 Then
                rs.Close()
                rs = Nothing
            End If
            conn.Close()
            conn = Nothing
        Catch ex As Exception
            MsgBox(ex.Message, , PROJECT_TITLE_STRING)
        End Try
    End Sub


    Private Sub bt_link_modem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_link_modem.Click
        '******************2012年4月6日转换成用四信的短信猫（直接串口操作）**************
        '打开串口
        If g_welcomewinobj.SerialPortMSG.IsOpen = False Then
            g_welcomewinobj.SerialPortMSG.BaudRate = Trim(cb_com_rate.Text)  '扫描的波特率为15200
            g_welcomewinobj.SerialPortMSG.PortName = Trim(cb_com_name.Text)  '串口名称

            Try
                g_welcomewinobj.SerialPortMSG.Open()
                MsgBox("串口打开成功！", , PROJECT_TITLE_STRING)
                g_welcomewinobj.ModemState.Text = "短信猫连接"
                '打开发送短信的线程
                If g_welcomewinobj.BackgroundWorkerSendMsg.IsBusy = False Then
                    g_welcomewinobj.BackgroundWorkerSendMsg.RunWorkerAsync()
                End If

            Catch ex As Exception
                MsgBox("串口打开失败！", , PROJECT_TITLE_STRING)
            End Try

        End If
    End Sub

    Private Sub bt_dislink_modem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_dislink_modem.Click
        '******************2012年4月6日转换成用四信的短信猫（直接串口操作）**************
        '关闭串口
        If g_welcomewinobj.SerialPortMSG.IsOpen = True Then
            g_welcomewinobj.SerialPortMSG.Close()
            MsgBox("串口关闭成功！", , PROJECT_TITLE_STRING)
            g_welcomewinobj.ModemState.Text = "短信猫断开"
            '关闭发送短信的线程
            If g_welcomewinobj.BackgroundWorkerSendMsg.IsBusy = True Then
                g_welcomewinobj.BackgroundWorkerSendMsg.CancelAsync()
            End If
        Else
            MsgBox("串口已关闭，无需重复操作！", , PROJECT_TITLE_STRING)
        End If
    End Sub

    Private Sub rb_checkmonth_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_checkmonth.Click
        If rb_checkmonth.Checked = True Then
            cb_date.Enabled = True
        End If
    End Sub

    Private Sub rb_checkdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_checktime.Click
        If rb_checktime.Checked = True Then
            cb_date.Text = ""
            cb_date.Enabled = False
        End If
    End Sub

    Private Sub bt_setchaobiao_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_setchaobiao.Click
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String = ""
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If

        If rb_checktime.Checked = True Then
            '每月定时查询
            sql = "select * from sysconfig where type='抄表月'"

            rs = DBOperation.SelectSQL(conn, sql, msg)
            If rs Is Nothing Then
                conn.Close()
                conn = Nothing
                g_chaobiaodate = -1
                g_chaobiaotime = -1
                Exit Sub
            End If
            If rs.RecordCount > 0 Then
                g_chaobiaodate = -1
                rs.Fields("name").Value = g_chaobiaodate

            Else
                g_chaobiaodate = -1
                rs.AddNew()
                rs.Fields("type").Value = "抄表月"
                rs.Fields("name").Value = -1
            End If
            rs.Update()

            '按每天进行抄表
            sql = "select * from sysconfig where type='抄表日'"

            rs = DBOperation.SelectSQL(conn, sql, msg)
            If rs Is Nothing Then
                conn.Close()
                conn = Nothing
                g_chaobiaodate = -1
                g_chaobiaotime = -1
                Exit Sub
            End If
            If rs.RecordCount > 0 Then
                g_chaobiaotime = Val(Trim(cb_time.Text))
                rs.Fields("name").Value = g_chaobiaotime
            Else
                g_chaobiaotime = -1
                rs.AddNew()
                rs.Fields("type").Value = "抄表日"
                rs.Fields("name").Value = -1
            End If
            rs.Update()

        End If


        If rb_checkmonth.Checked = True Then
            '每天定时查询
            sql = "select * from sysconfig where type='抄表月'"

            rs = DBOperation.SelectSQL(conn, sql, msg)
            If rs Is Nothing Then
                conn.Close()
                conn = Nothing
                g_chaobiaodate = -1
                g_chaobiaotime = -1
                Exit Sub
            End If
            If rs.RecordCount > 0 Then
                g_chaobiaodate = Val(Trim(cb_date.Text))
                rs.Fields("name").Value = g_chaobiaodate

            Else
                g_chaobiaodate = -1
                rs.AddNew()
                rs.Fields("type").Value = "抄表月"
                rs.Fields("name").Value = -1
            End If
            rs.Update()

            '按每天进行抄表
            sql = "select * from sysconfig where type='抄表日'"

            rs = DBOperation.SelectSQL(conn, sql, msg)
            If rs Is Nothing Then
                conn.Close()
                conn = Nothing
                g_chaobiaodate = -1
                g_chaobiaotime = -1
                Exit Sub
            End If
            If rs.RecordCount > 0 Then
                g_chaobiaotime = Val(Trim(cb_time.Text))
                rs.Fields("name").Value = g_chaobiaotime
            Else
                g_chaobiaotime = -1
                rs.AddNew()
                rs.Fields("type").Value = "抄表日"
                rs.Fields("name").Value = -1
            End If
            rs.Update()

        End If

        MsgBox("抄表模式设置完毕", , PROJECT_TITLE_STRING)
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_getlamp_time.Click
        If IsNumeric(Trim(tb_getlamp_time.Text)) = False Then  '控制值必须为数字
            MsgBox("请输入数值", , PROJECT_TITLE_STRING)
            Exit Sub
        End If
        If Trim(tb_getlamp_time.Text) < 30 Then  '控制值必须为数字
            MsgBox("输入巡查时间需大于 30", , PROJECT_TITLE_STRING)
            Exit Sub
        End If
        Try
            Dim rs As New ADODB.Recordset
            Dim conn As New ADODB.Connection
            Dim msg As String
            Dim sql As String

            msg = ""
            '设置光照阈值
            sql = "select * from sysconfig where type='区间时间'"
            DBOperation.OpenConn(conn)
            rs = DBOperation.SelectSQL(conn, sql, msg)
            If rs Is Nothing Then
                MsgBox(MSG_ERROR_STRING & "set_lamp_Click", , PROJECT_TITLE_STRING)
                conn.Close()
                conn = Nothing
                Exit Sub
            End If
            If rs.RecordCount > 0 Then
                rs.Fields("name").Value = Val(Trim(tb_getlamp_time.Text))
                rs.Update()
            Else
                rs.AddNew()
                rs.Fields("type").Value = "区间时间"
                rs.Fields("name").Value = Trim(tb_getlamp_time.Text)
                rs.Update()
            End If
            g_getlamp_time = Val(Trim(tb_getlamp_time.Text))

            MsgBox("区间时间编辑成功！", , PROJECT_TITLE_STRING)
            '增加操作记录
            Com_inf.Insert_Operation("区间时间：" & Trim(tb_getlamp_time.Text))
            If rs.State = 1 Then
                rs.Close()
                rs = Nothing
            End If
            conn.Close()
            conn = Nothing
        Catch ex As Exception
            MsgBox(ex.Message, , PROJECT_TITLE_STRING)
        End Try
    End Sub

    Private Sub rb_setbox_id_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rb_setbox_id.CheckedChanged
        Me.bt_clean_config.Visible = False
    End Sub

    Private Sub bt_clean_config_Click(sender As System.Object, e As System.EventArgs) Handles bt_clean_config.Click
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim m_boxid As String = ""
        msg = ""
        If tb_control_box_id.Text = "" Then
            MsgBox("请选择主控箱", , PROJECT_TITLE_STRING)
            tb_control_box_id.Focus()
            Exit Sub
        Else
            m_boxid = Trim(tb_control_box_id.Text)
            '清除区域信息
            sql = "UPDATE control_box SET inter_count =null WHERE control_box_id=" & m_boxid & ""
            DBOperation.OpenConn(conn)
            DBOperation.ExecuteSQL(conn, sql, msg)
            sql = "UPDATE lamp_inf SET inter_count =null WHERE control_box_id=" & m_boxid & ""
            DBOperation.ExecuteSQL(conn, sql, msg)
            conn.Close()
            conn = Nothing
            MsgBox("主控箱区间配置信息清除成功", , PROJECT_TITLE_STRING)
        End If
    End Sub
End Class