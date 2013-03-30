''' <summary>
''' 增加区域（电控箱）的名称，编号，IMEI，备注等信息
''' </summary>
''' <remarks></remarks>

Public Class 增加主控箱

    ''' <summary>
    ''' 增加区域名称的点击按钮响应函数
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_add_control_box_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_add_control_box.Click

        If cb_city_box_add.Text = "" Then
            MsgBox("城市名称不可以为空", , PROJECT_TITLE_STRING)
            cb_city_box_add.Focus()
            Exit Sub
        End If
        If cb_area_box_add.Text = "" Then
            MsgBox("区域名称不可以为空", , PROJECT_TITLE_STRING)
            cb_area_box_add.Focus()
            Exit Sub
        End If
        If cb_street_box_add.Text = "" Then
            MsgBox("街道名称不可以为空", , PROJECT_TITLE_STRING)
            cb_street_box_add.Focus()
            Exit Sub
        End If
        If tb_box_name.Text = "" Then
            MsgBox("主控箱名称不可以为空", , PROJECT_TITLE_STRING)
            tb_box_name.Focus()
            Exit Sub
        End If
        If tb_box_name.TextLength > 10 Then
            MsgBox("主控箱名称长度大于10，请重新输入", , PROJECT_TITLE_STRING)
            tb_box_name.Focus()
            Exit Sub
        End If
        If tb_control_box_id.TextLength <> 4 Then '电控箱编号长度一定为4
            MsgBox("主控箱编号长度为4，请重新输入！", , PROJECT_TITLE_STRING)
            tb_control_box_id.Focus() '光标定位在电控箱编号文本框
            Exit Sub
        End If
        If IsNumeric(Trim(tb_control_box_id.Text)) = False Then
            '电控箱编号必须为数字
            MsgBox("主控箱编号必须为数字，请重新输入！", , PROJECT_TITLE_STRING)
            tb_control_box_id.Focus()  '光标定位在电控箱编号文本框
            Exit Sub
        End If

        'If imei.TextLength <> 16 Then  'IMEI长度为16
        '    MsgBox("GPRS编号长度为16，请重新输入！", , PROJECT_TITLE_STRING)
        '    imei.Focus() '光标定位在IMEI文本框
        '    Exit Sub
        'End If

        If Trim(tb_start_pos_x.Text) = "" Or Trim(tb_start_pos_y.Text) = "" Then  '灯的起始位置
            MsgBox("请在地图上双击生成位置坐标", , PROJECT_TITLE_STRING)
            Exit Sub
        End If
        If Trim(tb_huilu_num.Text) = "" Then
            MsgBox("请输入模拟量路数", , PROJECT_TITLE_STRING)
            tb_huilu_num.Focus()
            Exit Sub
        End If
        If IsNumeric(Trim(tb_huilu_num.Text)) = False Then
            '模拟量路数必须为数字
            MsgBox("模拟量路数必须为数字，请重新输入！", , PROJECT_TITLE_STRING)
            tb_huilu_num.Focus()  '光标定位在电控箱编号文本框
            Exit Sub
        End If
        If Trim(tb_kaiguan_num.Text) = "" Then
            MsgBox("请输入开关量路数", , PROJECT_TITLE_STRING)
            tb_kaiguan_num.Focus()
            Exit Sub
        End If
        If IsNumeric(Trim(tb_kaiguan_num.Text)) = False Then
            '开关量路数必须为数字
            MsgBox("开关量路数必须为数字，请重新输入！", , PROJECT_TITLE_STRING)
            tb_kaiguan_num.Focus()  '光标定位在电控箱编号文本框
            Exit Sub
        End If
        If Trim(cb_testboard_num.Text) = "" Then
            MsgBox("请输入测量板个数", , PROJECT_TITLE_STRING)
            cb_testboard_num.Focus()
            Exit Sub
        End If
        If IsNumeric(Trim(cb_testboard_num.Text)) = False Then
            '测量板个数必须为数字
            MsgBox("测量板个数必须为数字，请重新输入！", , PROJECT_TITLE_STRING)
            cb_testboard_num.Focus()  '光标定位在测量板个数
            Exit Sub
        End If
        If cb_control_box_type.Text = "" Then
            MsgBox("请选择主控箱版本", , PROJECT_TITLE_STRING)
            cb_control_box_type.Focus()
            Exit Sub
        End If

        If tb_information.TextLength > 100 Then
            MsgBox("备注信息长度大于100", , PROJECT_TITLE_STRING)
            tb_information.Focus()
            Exit Sub
        End If

        Dim rs As ADODB.Recordset
        Dim rs_control_box As ADODB.Recordset
        Dim sql As String
        Dim msg As String
        Dim conn As New ADODB.Connection
        Dim street_id As String
        Dim pos_add As System.Drawing.Point  '新增一串路灯的起点和终点坐标
        Dim box_id As String  '电控箱编号！
        Dim imei_string, box_name_string As String
        Dim huilunum As Integer  '模拟量
        Dim kaiguannum As Integer  '开关量
        Dim testboardnum As Integer '测量板个数
        Dim box_type As Integer = 1  '默认版本为1

        '*********************'全角转换成半角*****************************
        box_id = StrConv(Trim(tb_control_box_id.Text), VbStrConv.Narrow)
        imei_string = StrConv(Trim(tb_imei.Text), VbStrConv.Narrow)
        box_name_string = StrConv(Trim(tb_box_name.Text), VbStrConv.Narrow)
        box_name_string = box_name_string.Replace(" ", "") '将字符串中的空格去掉
        huilunum = Val(StrConv(Trim(tb_huilu_num.Text), VbStrConv.Narrow))
        kaiguannum = Val(StrConv(Trim(tb_kaiguan_num.Text), VbStrConv.Narrow))
        testboardnum = Val(StrConv(Trim(cb_testboard_num.Text), VbStrConv.Narrow))
        box_type = Val(Trim(cb_control_box_type.Text))
        '********************************************************************

        msg = ""
        DBOperation.OpenConn(conn)

        '检测当前的地图和所增加的区域是否匹配
        sql = "select * from map_list where area='" & Trim(cb_area_box_add.Text) & "' and id='" & g_choosemapid & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "add_control_box_Click", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs.RecordCount <= 0 Then
            MsgBox("请匹配地图与所增加主控箱的区域", , PROJECT_TITLE_STRING)
            rs.Close()
            rs = Nothing
            conn.Close()
            conn = Nothing
            Exit Sub
        End If

        '屏幕中的路灯起点坐标转换城地图中的相对坐标
        pos_add.X = Val(tb_start_pos_x.Text - g_welcomewinobj.GroupBox1.Location.X - g_welcomewinobj.DesktopLocation.X - (Me.Width - Me.ClientSize.Width) - g_welcomewinobj.pb_map.Location.X - g_welcomewinobj.SplitContainer3.Panel1.Width)
        pos_add.Y = Val(tb_start_pos_y.Text - g_welcomewinobj.GroupBox1.Location.Y - g_welcomewinobj.DesktopLocation.Y - (Me.Height - Me.ClientSize.Height) - g_welcomewinobj.pb_map.Location.Y)


        sql = "select * from control_box where control_box_id='" & box_id & "' or control_box_name='" & Trim(tb_box_name.Text) & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs.RecordCount > 0 Then
            MsgBox("主控箱名称或编号已存在", , PROJECT_TITLE_STRING)
            Me.tb_box_name.Focus()
            rs.Close()
            rs = Nothing
            conn.Close()
            conn = Nothing
            Exit Sub
        End If

        sql = "select * from RoadIDAndIMEI where IMEI='" & imei_string & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs.RecordCount > 0 Then
            If MsgBox("该IMEI编号信息已经存在，是否继续添加区域信息？", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.No Then
                rs.Close()
                rs = Nothing
                conn.Close()
                conn = Nothing
                Exit Sub
            End If
        End If
        sql = "select * from RoadIDAndIMEI where RoadID='" & Val(box_id) & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs.RecordCount > 0 Then
            MsgBox("该主控箱信息已经存在", , PROJECT_TITLE_STRING)
            rs.Close()
            rs = Nothing
            conn.Close()
            conn = Nothing
            Exit Sub
        Else
            'add_lamp_type()  '增加景观灯类型
            '2011年1月18日将变比参数与电控箱绑定information参数存储变比
            '2011年5月11日版本将information参数回复到备注信息
            sql = "insert into RoadIDAndIMEI (RoadID,IMEI,Createtime,information) " & _
            "values('" & Val(box_id) & "', '" & imei_string & "','" & Now & "','" & Trim(tb_information.Text) & "' )"
            DBOperation.ExecuteSQL(conn, sql, msg)

            sql = "select street_id from street where street='" & Trim(cb_street_box_add.Text) & "'"
            rs_control_box = DBOperation.SelectSQL(conn, sql, msg)
            If rs_control_box.RecordCount > 0 Then
                street_id = Trim(rs_control_box.Fields("street_id").Value)
            Else
                GoTo finish
            End If

            sql = "select * from control_box where control_box_id='" & box_id & "'"


            rs_control_box = DBOperation.SelectSQL(conn, sql, msg)
            If rs_control_box.RecordCount = 0 Then

                sql = "insert into control_box(id,control_box_id,street_id,control_box_name,pos_x,pos_y,elec_state,huilu_num, kaiguanliang_num, board_num, control_box_type,createtime,StatusContent,StatusContent2,StatusContent3)" & _
                "values('" & Val(box_id) & "','" & box_id & "','" & street_id & "','" & box_name_string & "','" & pos_add.X & "','" & pos_add.Y & "','断电','" & huilunum & "','" & kaiguannum & "','" & testboardnum & "','" & box_type & "','" & Now & "','','','')"
                DBOperation.ExecuteSQL(conn, sql, msg)

            End If
            '增加该电控箱下的路灯类型
            sql = "insert into box_lamptype(control_box_id,lamp_type_id) values('" & box_id & "','0')"
            DBOperation.ExecuteSQL(conn, sql, msg)

            '增加该电控箱下的交流接触器类型
            sql = "insert into box_lamptype(control_box_id,lamp_type_id) values('" & box_id & "','31')"
            DBOperation.ExecuteSQL(conn, sql, msg)

            Me.Box_IMEITableAdapter.Fill(Me.Box_inf.Box_IMEI)

            MsgBox("信息添加成功！", , PROJECT_TITLE_STRING)

            '增加操作记录
            Com_inf.Insert_Operation("增加主控箱：" & box_name_string)
        End If



        '刷新主控界面左边的电控箱信息
        g_welcomewinobj.find_box_state() '电控箱列表

        '刷新主控箱列表
        g_welcomewinobj.m_controlboxobj.set_controlbox_list(g_welcomewinobj.tv_box_inf_list)



        '2011年12月12日增加抄表功能配置，抄表配置记录增加到一个新的表中，controlbox_power
        If Me.ck_getmeterdata.Checked = True Then
            If Me.cb_control_box_type.Text = "" Then
                MsgBox("请选择电表规约", , PROJECT_TITLE_STRING)
                GoTo finish
            End If
            If tb_powermeter_bianbi.Text = "" Then
                MsgBox("请输入电表变比", , PROJECT_TITLE_STRING)
                GoTo finish
            End If
            If IsNumeric(Trim(tb_powermeter_bianbi.Text)) = False Then
                MsgBox("变比必须为数字", , PROJECT_TITLE_STRING)
                GoTo finish
            End If
            '电表编号可以为空，不知道的情况下为空，以后获取后补上
            sql = "insert into controlbox_power(control_box_name,control_box_id, powermeter_type," _
            & "powermeter_id,powermeter_bianbi,imei) values('" & box_name_string & "','" & box_id _
            & "','" & Trim(Me.cb_metertype.Text) & "','" & Trim(Me.tb_powermeterid.Text) & "'," & _
            Val(Trim(tb_powermeter_bianbi.Text)) & ",'" & imei_string & "')"
            DBOperation.ExecuteSQL(conn, sql, msg)


        End If

finish:
        rs_control_box.Close()
        rs_control_box = Nothing
        rs.Close()
        rs = Nothing
        conn.Close()
        conn = Nothing
        'Me.Close()
    End Sub

    ''' <summary>
    ''' 窗体载入函数
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 增加电控箱_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: 这行代码将数据加载到表“Box_inf.Box_IMEI”中。您可以根据需要移动或移除它。
        Me.Box_IMEITableAdapter.Fill(Me.Box_inf.Box_IMEI)
        '设置各个窗体的图标
        Me.Icon = New Icon("图片\favicon.ico", 32, 32)
        'add_control_box_inf()
        '自动生成电控箱编号
        Com_inf.Select_city_name(cb_city_box_add)
        Com_inf.Select_area_name(cb_city_box_add, cb_area_box_add)
        Com_inf.Select_street_name(cb_city_box_add, cb_area_box_add, cb_street_box_add)


        g_addboxtag = 1  '设置取地图坐标

        Dim rs As ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim id_num As Integer
        Dim conn As New ADODB.Connection
        DBOperation.OpenConn(conn)


        msg = ""
        sql = "select * from control_box order by control_box_id desc"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "增加电控箱_Load", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        '自动更新区域编号，在原有最大的基础上加1
        If rs.RecordCount > 0 Then
            id_num = Val(Trim(rs.Fields("control_box_id").Value)) + 1
            If id_num <= 0 Then
                MsgBox("主控箱编号输入出错", , PROJECT_TITLE_STRING)
                conn.Close()
                conn = Nothing
                Exit Sub
            End If
            If id_num > 0 And id_num < 10 Then
                tb_control_box_id.Text = "000" & id_num.ToString
            End If
            If id_num >= 10 And id_num < 100 Then
                tb_control_box_id.Text = "00" & id_num.ToString
            End If

            If id_num >= 100 And id_num < 1000 Then
                tb_control_box_id.Text = "0" & id_num.ToString
            End If
            If id_num >= 1000 And id_num < 10000 Then
                tb_control_box_id.Text = id_num.ToString
            End If
            If id_num >= 10000 Then
                MsgBox("主控箱编号输入出错", , PROJECT_TITLE_STRING)
                conn.Close()
                conn = Nothing
                Exit Sub
            End If

        End If

        '初始化测量板个数
        cb_testboard_num.SelectedIndex = 0
        '初始化主控箱版本
        cb_control_box_type.SelectedIndex = 0

        rs.Close()
        rs = Nothing
        conn.Close()
        conn = Nothing


    End Sub

    ''' <summary>
    ''' 增加城市名称
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_city_box_add_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_city_box_add.DropDown
        Com_inf.Select_city_name(cb_city_box_add)
    End Sub

    ''' <summary>
    ''' 城市名称改变，其他信息改变
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_city_box_add_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_city_box_add.SelectedIndexChanged
        Com_inf.Select_area_name(cb_city_box_add, cb_area_box_add)
        Com_inf.Select_street_name(cb_city_box_add, cb_area_box_add, cb_street_box_add)

    End Sub

    ''' <summary>
    ''' 增加区域名称
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_area_box_add_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_area_box_add.DropDown
        If cb_city_box_add.Text <> "" Then
            Com_inf.Select_area_name(cb_city_box_add, cb_area_box_add)
        End If
    End Sub

    ''' <summary>
    ''' 区域名称改变，其他信息改变
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_area_box_add_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_area_box_add.SelectedIndexChanged
        Com_inf.Select_street_name(cb_city_box_add, cb_area_box_add, cb_street_box_add)
    End Sub

    ''' <summary>
    ''' 增加街道名称
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_street_box_add_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_street_box_add.DropDown
        Com_inf.Select_street_name(cb_city_box_add, cb_area_box_add, cb_street_box_add)
    End Sub

    ''' <summary>
    ''' 关闭窗口
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 增加主控箱_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        g_addboxtag = 0  '设置取地图坐标

    End Sub

    ''' <summary>
    ''' 改变测量板个数
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_testboard_num_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_testboard_num.SelectedIndexChanged
        tb_huilu_num.Text = (Val(cb_testboard_num.Text) * 12).ToString
    End Sub

    Private Sub ck_getmeterdata_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ck_getmeterdata.Click
        If ck_getmeterdata.Checked = False Then
            cb_metertype.Enabled = False
            tb_powermeterid.Enabled = False
        Else
            cb_metertype.Enabled = True
            tb_powermeterid.Enabled = True
        End If
    End Sub

    Private Sub cb_control_box_type_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_control_box_type.SelectedIndexChanged
        If cb_control_box_type.Text = 3 Then
            '小三遥
            cb_testboard_num.Text = 1
            tb_huilu_num.Text = 6
        Else
            '大三遥
            cb_testboard_num.Text = 1
            tb_huilu_num.Text = 12
        End If
    End Sub
End Class