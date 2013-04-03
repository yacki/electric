Public Class 主控箱
    Public m_controlboxobj As New control_box  'control_box的实例化对象
    Private m_IMEI As String
    Private m_editcontrolboxname, m_configcontrolboxname As String  '电控箱名称
    Private m_editboxid, m_configboxid As String  '电控箱编号
    Dim m_jiechuqi_id As String = "K1"  '接触器编号

    Private m_xlApp As Microsoft.Office.Interop.Excel.Application

    Private m_xlBook As Microsoft.Office.Interop.Excel.Workbook
    Private m_xlSheet As Microsoft.Office.Interop.Excel.Worksheet
    Private m_excelboxname As String

    Private Sub 主控箱_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        m_controlboxobj.set_controlbox_list(tv_box_inf_list) '主控箱信息列表
        '设置各个窗体的图标
        Me.Icon = New Icon("图片\favicon.ico", 32, 32)
        'add_control_box_inf()
        '自动生成电控箱编号
        Com_inf.Select_city_name(cb_city_box_add)
        Com_inf.Select_area_name(cb_city_box_add, cb_area_box_add)
        Com_inf.Select_street_name(cb_city_box_add, cb_area_box_add, cb_street_box_add)
        m_excelboxname = ""

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

        If Trim(tb_imei.Text) = "" Then
            'IMEI   不可以为空
            MsgBox("IMEI不可以为空，请重新输入！", , PROJECT_TITLE_STRING)
            tb_imei.Focus()
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

        'If tb_information.TextLength > 100 Then
        '    MsgBox("备注信息长度大于100", , PROJECT_TITLE_STRING)
        '    tb_information.Focus()
        '    Exit Sub
        'End If

        Dim rs As ADODB.Recordset
        Dim rs_control_box As ADODB.Recordset
        Dim sql As String
        Dim msg As String
        Dim conn As New ADODB.Connection
        Dim street_id As String
        Dim pos_add As System.Drawing.Point  '新增一串终端的起点和终点坐标
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

        '屏幕中的终端起点坐标转换城地图中的相对坐标
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
            sql = "insert into RoadIDAndIMEI (RoadID,IMEI,Createtime) " & _
            "values('" & Val(box_id) & "', '" & imei_string & "','" & Now & "' )"
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
            '增加该电控箱下的终端类型
            sql = "insert into box_lamptype(control_box_id,lamp_type_id) values('" & box_id & "','0')"
            DBOperation.ExecuteSQL(conn, sql, msg)

            '增加该电控箱下的交流接触器类型
            sql = "insert into box_lamptype(control_box_id,lamp_type_id) values('" & box_id & "','31')"
            DBOperation.ExecuteSQL(conn, sql, msg)

            '2012年6月11日，增加主控箱的备注信息
            add_controlbox_inf(box_id)

            m_controlboxobj.set_controlbox_list(tv_box_inf_list) '主控箱信息列表

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
                sql = "insert into controlbox_power(control_box_id,control_box_name, powermeter_type," _
                & "powermeter_id,powermeter_bianbi,imei) values('" & box_id & "','" & box_name_string _
                & "','" & Trim(Me.cb_metertype.Text) & "','" & Trim(Me.tb_powermeterid.Text) & "'," & _
                Val(Trim(tb_powermeter_bianbi.Text)) & ",'" & imei_string & "')"
                DBOperation.ExecuteSQL(conn, sql, msg)


            End If


            MsgBox("信息添加成功！", , PROJECT_TITLE_STRING)

            '增加操作记录
            Com_inf.Insert_Operation("增加主控箱：" & box_name_string)
        End If



        '刷新主控界面左边的电控箱信息
        g_welcomewinobj.find_box_state() '电控箱列表

        '刷新主控箱列表
        g_welcomewinobj.m_controlboxobj.set_controlbox_list(g_welcomewinobj.tv_box_inf_list)




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
    ''' 增加主控的新增的备注信息
    ''' </summary>
    ''' <param name="box_id"></param>
    ''' <remarks></remarks>
    Private Sub add_controlbox_inf(ByVal box_id As String)

        If Trim(Me.tb_phonenum_add.TextLength > 20) Then
            MsgBox("电话号码长度大于20，请重新输入", , PROJECT_TITLE_STRING)
            tb_phonenum_add.Focus()
            Exit Sub
        End If
        If Trim(Me.tb_ip_add.TextLength > 50) Then
            MsgBox("前置机IP长度大于50，请重新输入", , PROJECT_TITLE_STRING)
            tb_ip_add.Focus()
            Exit Sub
        End If
        If Trim(Me.tb_com_add.TextLength > 50) Then
            MsgBox("端口长度大于50，请重新输入", , PROJECT_TITLE_STRING)
            tb_ip_add.Focus()
            Exit Sub
        End If
        If Trim(Me.tb_address_add.TextLength > 100) Then
            MsgBox("安装地址长度大于100，请重新输入", , PROJECT_TITLE_STRING)
            tb_address_add.Focus()
            Exit Sub
        End If
        If Trim(Me.tb_denggan_add.TextLength > 50) Then
            MsgBox("对应灯杆号大于50，请重新输入", , PROJECT_TITLE_STRING)
            tb_denggan_add.Focus()
            Exit Sub
        End If
        If Trim(Me.tb_dianyuan_add.TextLength > 50) Then
            MsgBox("供电电源大于50，请重新输入", , PROJECT_TITLE_STRING)
            tb_dianyuan_add.Focus()
            Exit Sub
        End If
        If Trim(Me.tb_chuchang_add.TextLength > 50) Then
            MsgBox("出厂编号大于50，请重新输入", , PROJECT_TITLE_STRING)
            tb_chuchang_add.Focus()
            Exit Sub
        End If
        If Trim(Me.tb_gaoya_add.TextLength > 50) Then
            MsgBox("高压出线大于50，请重新输入", , PROJECT_TITLE_STRING)
            tb_gaoya_add.Focus()
            Exit Sub
        End If
        If Trim(Me.tb_worker_add.TextLength > 50) Then
            MsgBox("检修负责人大于50，请重新输入", , PROJECT_TITLE_STRING)
            tb_worker_add.Focus()
            Exit Sub
        End If
        If Trim(Me.tb_contact_add.TextLength > 50) Then
            MsgBox("联系方式大于50，请重新输入", , PROJECT_TITLE_STRING)
            tb_contact_add.Focus()
            Exit Sub
        End If
        If Trim(Me.tb_dengganall_add.TextLength > 200) Then
            MsgBox("灯杆范围大于200，请重新输入", , PROJECT_TITLE_STRING)
            tb_dengganall_add.Focus()
            Exit Sub
        End If

        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String = ""
        Dim sql As String

        sql = "select * from controlbox_inf where control_box_id='" & box_id & "'"
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs.RecordCount > 0 Then
            '已经存在该主控箱的备注信息
            rs.Fields("phone_num").Value = Trim(Me.tb_phonenum_add.Text)
            rs.Fields("ip").Value = Trim(Me.tb_ip_add.Text)
            rs.Fields("com").Value = Trim(Me.tb_com_add.Text)
            rs.Fields("address").Value = Trim(Me.tb_address_add.Text)
            rs.Fields("denggan").Value = Trim(Me.tb_denggan_add.Text)
            rs.Fields("dianyuan").Value = Trim(Me.tb_dianyuan_add.Text)
            rs.Fields("chuchang_id").Value = Trim(Me.tb_chuchang_add.Text)
            rs.Fields("gaoya").Value = Trim(Me.tb_gaoya_add.Text)
            rs.Fields("jianxiu").Value = Trim(Me.tb_worker_add.Text)
            rs.Fields("jianxiu_contact").Value = Trim(Me.tb_contact_add.Text)
            rs.Fields("denggan_fanwei").Value = Trim(Me.tb_dengganall_add.Text)
            rs.Update()

        Else
            '新增主控箱的备注信息
            rs.AddNew()
            rs.Fields("control_box_id").Value = Trim(Me.tb_control_box_id.Text)
            rs.Fields("phone_num").Value = Trim(Me.tb_phonenum_add.Text)
            rs.Fields("ip").Value = Trim(Me.tb_ip_add.Text)
            rs.Fields("com").Value = Trim(Me.tb_com_add.Text)
            rs.Fields("address").Value = Trim(Me.tb_address_add.Text)
            rs.Fields("denggan").Value = Trim(Me.tb_denggan_add.Text)
            rs.Fields("dianyuan").Value = Trim(Me.tb_dianyuan_add.Text)
            rs.Fields("chuchang_id").Value = Trim(Me.tb_chuchang_add.Text)
            rs.Fields("gaoya").Value = Trim(Me.tb_gaoya_add.Text)
            rs.Fields("jianxiu").Value = Trim(Me.tb_worker_add.Text)
            rs.Fields("jianxiu_contact").Value = Trim(Me.tb_contact_add.Text)
            rs.Fields("denggan_fanwei").Value = Trim(Me.tb_dengganall_add.Text)
            rs.Update()

        End If

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing


    End Sub

    ''' <summary>
    ''' 编辑主控的备注信息
    ''' </summary>
    ''' <param name="box_id"></param>
    ''' <remarks></remarks>
    Private Sub edit_controlbox_inf(ByVal box_id As String)

        If Trim(Me.tb_phonenum.TextLength > 20) Then
            MsgBox("电话号码长度大于20，请重新输入", , PROJECT_TITLE_STRING)
            tb_phonenum.Focus()
            Exit Sub
        End If
        If Trim(Me.tb_ip.TextLength > 50) Then
            MsgBox("前置机IP长度大于50，请重新输入", , PROJECT_TITLE_STRING)
            tb_ip.Focus()
            Exit Sub
        End If
        If Trim(Me.tb_com.TextLength > 50) Then
            MsgBox("端口长度大于50，请重新输入", , PROJECT_TITLE_STRING)
            tb_com.Focus()
            Exit Sub
        End If
        If Trim(Me.tb_address.TextLength > 100) Then
            MsgBox("安装地址长度大于100，请重新输入", , PROJECT_TITLE_STRING)
            tb_address.Focus()
            Exit Sub
        End If
        If Trim(Me.tb_denggan.TextLength > 50) Then
            MsgBox("对应灯杆号大于50，请重新输入", , PROJECT_TITLE_STRING)
            tb_denggan.Focus()
            Exit Sub
        End If
        If Trim(Me.tb_dianyuan.TextLength > 50) Then
            MsgBox("供电电源大于50，请重新输入", , PROJECT_TITLE_STRING)
            tb_dianyuan.Focus()
            Exit Sub
        End If
        If Trim(Me.tb_chuchang_id.TextLength > 50) Then
            MsgBox("出厂编号大于50，请重新输入", , PROJECT_TITLE_STRING)
            tb_chuchang_id.Focus()
            Exit Sub
        End If
        If Trim(Me.tb_gaoya.TextLength > 50) Then
            MsgBox("高压出线大于50，请重新输入", , PROJECT_TITLE_STRING)
            tb_gaoya.Focus()
            Exit Sub
        End If
        If Trim(Me.tb_jianxiu.TextLength > 50) Then
            MsgBox("检修负责人大于50，请重新输入", , PROJECT_TITLE_STRING)
            tb_jianxiu.Focus()
            Exit Sub
        End If
        If Trim(Me.tb_contact.TextLength > 50) Then
            MsgBox("联系方式大于50，请重新输入", , PROJECT_TITLE_STRING)
            tb_contact.Focus()
            Exit Sub
        End If
        If Trim(Me.tb_denggan_fanwei.TextLength > 200) Then
            MsgBox("灯杆范围大于200，请重新输入", , PROJECT_TITLE_STRING)
            tb_denggan_fanwei.Focus()
            Exit Sub
        End If

        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String = ""
        Dim sql As String

        sql = "select * from controlbox_inf where control_box_id='" & box_id & "'"
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs.RecordCount > 0 Then
            '已经存在该主控箱的备注信息
            rs.Fields("phone_num").Value = Trim(Me.tb_phonenum.Text)
            rs.Fields("ip").Value = Trim(Me.tb_ip.Text)
            rs.Fields("com").Value = Trim(Me.tb_com.Text)
            rs.Fields("address").Value = Trim(Me.tb_address.Text)
            rs.Fields("denggan").Value = Trim(Me.tb_denggan.Text)
            rs.Fields("dianyuan").Value = Trim(Me.tb_dianyuan.Text)
            rs.Fields("chuchang_id").Value = Trim(Me.tb_chuchang_id.Text)
            rs.Fields("gaoya").Value = Trim(Me.tb_gaoya.Text)
            rs.Fields("jianxiu").Value = Trim(Me.tb_jianxiu.Text)
            rs.Fields("jianxiu_contact").Value = Trim(Me.tb_contact.Text)
            rs.Fields("denggan_fanwei").Value = Trim(Me.tb_denggan_fanwei.Text)
            rs.Update()

        Else
            '新增主控箱的备注信息
            rs.AddNew()
            rs.Fields("control_box_id").Value = box_id
            rs.Fields("phone_num").Value = Trim(Me.tb_phonenum.Text)
            rs.Fields("ip").Value = Trim(Me.tb_ip.Text)
            rs.Fields("com").Value = Trim(Me.tb_com.Text)
            rs.Fields("address").Value = Trim(Me.tb_address.Text)
            rs.Fields("denggan").Value = Trim(Me.tb_denggan.Text)
            rs.Fields("dianyuan").Value = Trim(Me.tb_dianyuan.Text)
            rs.Fields("chuchang_id").Value = Trim(Me.tb_chuchang_id.Text)
            rs.Fields("gaoya").Value = Trim(Me.tb_gaoya.Text)
            rs.Fields("jianxiu").Value = Trim(Me.tb_jianxiu.Text)
            rs.Fields("jianxiu_contact").Value = Trim(Me.tb_contact.Text)
            rs.Fields("denggan_fanwei").Value = Trim(Me.tb_denggan_fanwei.Text)
            rs.Update()

        End If

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing


    End Sub

    ''' <summary>
    ''' 双击获取主控箱的备注信息
    ''' </summary>
    ''' <param name="box_id"></param>
    ''' <remarks></remarks>
    Private Sub get_controlboxinf(ByVal box_id As String)

        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String = ""
        Dim sql As String

        sql = "select * from controlbox_inf where control_box_id='" & box_id & "'"
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs.RecordCount > 0 Then
            '已经存在该主控箱的备注信息
            Me.tb_phonenum.Text = Trim(rs.Fields("phone_num").Value)
            Me.tb_ip.Text = Trim(rs.Fields("ip").Value)
            Me.tb_com.Text = Trim(rs.Fields("com").Value)
            Me.tb_address.Text = Trim(rs.Fields("address").Value)
            Me.tb_denggan.Text = Trim(rs.Fields("denggan").Value)
            Me.tb_dianyuan.Text = Trim(rs.Fields("dianyuan").Value)
            Me.tb_chuchang_id.Text = Trim(rs.Fields("chuchang_id").Value)
            Me.tb_gaoya.Text = Trim(rs.Fields("gaoya").Value)
            Me.tb_jianxiu.Text = Trim(rs.Fields("jianxiu").Value)
            Me.tb_contact.Text = Trim(rs.Fields("jianxiu_contact").Value)
            Me.tb_denggan_fanwei.Text = Trim(rs.Fields("denggan_fanwei").Value)

        Else
            Me.tb_phonenum.Text = ""
            Me.tb_ip.Text = ""
            Me.tb_com.Text = ""
            Me.tb_address.Text = ""
            Me.tb_denggan.Text = ""
            Me.tb_dianyuan.Text = ""
            Me.tb_chuchang_id.Text = ""
            Me.tb_gaoya.Text = ""
            Me.tb_jianxiu.Text = ""
            Me.tb_contact.Text = ""
            Me.tb_denggan_fanwei.Text = ""

        End If

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
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
        ProcessKill(m_xlApp, m_xlBook, m_xlSheet)
        g_windowclose = 1
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

    Private Sub clear_text()
        m_editcontrolboxname = ""
        m_IMEI = ""
        m_editboxid = ""
        tb_control_box_name.Text = ""
        tb_imeistring.Text = ""  'IMEI
        tb_start_x.Text = ""
        tb_start_y.Text = ""
        tb_huilunum.Text = ""
        cb_box_type.Text = ""
        tb_testboard_num.Text = ""

        'lb_box_information.Text = ""
        'tb_editaddress.Text = ""
    End Sub

    Private Sub choose_editbox()
        '将主控箱信息中的所有文本框清空()
        clear_text()

        '根据当前选择的主控箱将编辑的文本框填充
        ' g_addboxtag = 3  '编辑电控箱坐标
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String

        msg = ""
        sql = "SELECT DISTINCT control_box_name, IMEI, Information, control_box_id,pos_x,pos_y,huilu_num, kaiguanliang_num,board_num,control_box_type FROM Box_IMEI where control_box_name='" & Trim(tv_box_inf_list.SelectedNode.Text).Split(" ")(0) & "' order by control_box_id"
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            conn.Close()
            conn = Nothing
            Exit Sub

        End If
        If rs.RecordCount > 0 Then
            '存在主控箱的相关信息
            m_editcontrolboxname = Trim(rs.Fields("control_box_name").Value)
            m_IMEI = Trim(rs.Fields("IMEI").Value)
            m_editboxid = Trim(rs.Fields("control_box_id").Value)
            tb_control_box_name.Text = m_editcontrolboxname
            tb_imeistring.Text = m_IMEI  'IMEI
            tb_start_x.Text = Trim(rs.Fields("pos_x").Value)
            tb_start_y.Text = Trim(rs.Fields("pos_y").Value)
            tb_box_id.Text = Trim(rs.Fields("control_box_id").Value)
            If rs.Fields("huilu_num").Value Is DBNull.Value Then
                tb_huilunum.Text = ""
            Else
                tb_huilunum.Text = Trim(rs.Fields("huilu_num").Value)

            End If
            If rs.Fields("control_box_type").Value Is DBNull.Value Then
                cb_box_type.Text = ""
            Else
                cb_box_type.Text = Trim(rs.Fields("control_box_type").Value)

            End If
            If rs.Fields("board_num").Value Is DBNull.Value Then
                tb_testboard_num.Text = ""
            Else
                tb_testboard_num.Text = Trim(rs.Fields("board_num").Value)

            End If
            If rs.Fields("Information").Value Is DBNull.Value Then
                cb_controlmod.SelectedIndex = 0
            Else
                If Trim(rs.Fields("Information").Value) = "关闭" Then
                    cb_controlmod.SelectedIndex = 1
                Else
                    cb_controlmod.SelectedIndex = 0
                End If


            End If

            'If rs.Fields("information").Value Is DBNull.Value Then
            '    lb_box_information.Text = ""
            'Else
            '    lb_box_information.Text = Trim(rs.Fields("information").Value)
            'End If

            '2011年12月12日，双击后增加电表的配置
            get_powermeter_set(m_editboxid)

            '2012年6月18日，双击后增加主控箱的备注信息
            get_controlboxinf(m_editboxid)

        End If

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    Private Sub tv_box_inf_list_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tv_box_inf_list.DoubleClick
        If Me.tv_box_inf_list.SelectedNode IsNot Nothing Then
            If Me.tv_box_inf_list.SelectedNode.Level = 3 Then
                '编辑或删除主控箱
                If tc_boxinf.SelectedTab Is Me.tp_editbox Then
                    choose_editbox()
                End If

                '选择主控箱的模拟量，开关量的配置
                If tc_boxinf.SelectedTab Is tp_boxconfig Then
                    choose_boxconfiginf()
                End If


            End If
        End If

    End Sub

    Private Sub choose_boxconfiginf()
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim KList() As System.Windows.Forms.RadioButton = {rb_k1_list, rb_k2_list, rb_k3_list, rb_k4_list, rb_k5_list, rb_k6_list}

        Dim i As Integer = 1  '回路的起始编号
        Dim huilunum As Integer = 0  '记录该主控箱下的总回路数
        m_configcontrolboxname = Trim(tv_box_inf_list.SelectedNode.Text).Split(" ")(0)

        msg = ""
        sql = "select huilu_num, control_box_id,control_box_name from control_box where control_box_name='" & m_configcontrolboxname & "'"
        DBOperation.OpenConn(conn)
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "box_inf_list_DoubleClick", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs.RecordCount > 0 Then
            If rs.Fields("huilu_num").Value Is System.DBNull.Value Then
                huilunum = 0
            Else
                huilunum = rs.Fields("huilu_num").Value
            End If
            If rs.Fields("control_box_id").Value Is System.DBNull.Value Then
                m_configboxid = ""
            Else
                m_configboxid = Trim(rs.Fields("control_box_id").Value)
            End If
            If rs.Fields("control_box_name").Value Is System.DBNull.Value Then
                m_configcontrolboxname = Trim(rs.Fields("control_box_name").Value)
            Else
                m_configcontrolboxname = Trim(rs.Fields("control_box_name").Value)

            End If

        End If
        '根据电控箱编号找到该主控箱下面可以使用的接触器编号
        sql = "select lamp_id from lamp_inf where control_box_id='" & m_configboxid & "' and lamp_type_id=31"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "box_inf_list_DoubleClick", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        i = 0
        While i < 6
            If rs.EOF = False Then
                If Mid(Trim(rs.Fields("lamp_id").Value), 7, LAMP_ID_LEN) = i + 1 Then
                    KList(i).Enabled = True
                    rs.MoveNext()
                Else
                    KList(i).Enabled = False
                End If

            Else
                KList(i).Enabled = False
            End If

            i += 1
        End While
        '增加该电控箱下的各个回路
        '2012年3月26日将各个回路转变为以A1,A2....为标志的
        i = 1
        clb_huilu_idlist.Items.Clear()
        If huilunum = 6 Then
            While i <= huilunum
                clb_huilu_idlist.Items.Add(i.ToString & " 号回路")
                i += 1
            End While
        Else
            If huilunum = 12 Then
                While i <= huilunum
                    clb_huilu_idlist.Items.Add(i.ToString & " 号回路")
                    i += 1
                End While
            Else
                If huilunum = 24 Then
                    While i <= huilunum
                        clb_huilu_idlist.Items.Add(i.ToString & " 号回路")
                        i += 1
                    End While
                Else
                    While i <= huilunum
                        clb_huilu_idlist.Items.Add(i.ToString & " 号回路")
                        i += 1
                    End While
                End If
            End If

        End If

        '默认选择第一个接触器下的回路
        Dim jiechuqi_id As Integer = select_firstjiechuqi()
        If jiechuqi_id > 0 Then
            KList(jiechuqi_id - 1).Checked = True
            select_huiluid(jiechuqi_id)
        End If


        '根据选择的主控箱名称选择模拟量配置列表
        Me.Huilu_infTableAdapter.FillBy_box(Me.EditDataSet.huilu_inf, Trim(m_configcontrolboxname))

        '根据选择的主控箱名称选择开关量配置列表
        Me.Kaiguan_listTableAdapter.FillBy_box(Me.KaiguanDataSet.kaiguan_list, m_configcontrolboxname)

        '模拟量列表名称
        GroupBoxmoniliang.Text = "编辑主控箱：" & m_configcontrolboxname & "模拟量"
        GroupBoxkaiguanliang.Text = "编辑主控箱：" & m_configcontrolboxname & "开关量"
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    Private Function select_firstjiechuqi() As Integer
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String = ""
        Dim sql As String

        DBOperation.OpenConn(conn)
        sql = "select lamp_id from lamp_inf where control_box_id=" & m_configboxid & " and lamp_type_id=31 order by lamp_id"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "select_firstjiechuqi", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            select_firstjiechuqi = 0

            Exit Function
        End If

        If rs.RecordCount > 0 Then
            select_firstjiechuqi = Val(Mid(Trim(rs.Fields("lamp_id").Value), 7, LAMP_ID_LEN))
        Else
            select_firstjiechuqi = 0

        End If

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing

    End Function

    ''' <summary>
    ''' 根据解除器的编号，选择其所控制的回路编号
    ''' </summary>
    ''' <param name="jiechuqi_id"></param>
    ''' <remarks></remarks>
    Private Sub select_huiluid(ByVal jiechuqi_id As Integer)
        '选择第个接触器编号
        If Me.m_configcontrolboxname = "" Then
            MsgBox("请选择主控箱的名称", , PROJECT_TITLE_STRING)
            Exit Sub
        End If
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim i As Integer = 0

        msg = ""
        '将所有的回路先设置为未选中状态
        While i < clb_huilu_idlist.Items.Count
            clb_huilu_idlist.SetItemChecked(i, False)
            i += 1
        End While

        sql = "select huilu_id from huilu_inf where control_box_name='" & m_configcontrolboxname & "' and jiechuqi_id='" & jiechuqi_id & "' order by id "
        DBOperation.OpenConn(conn)
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "select_huiluid", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If

        While rs.EOF = False
            clb_huilu_idlist.SetItemChecked(rs.Fields("huilu_id").Value - 1, True)
            rs.MoveNext()
        End While
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    Private Sub get_powermeter_set(ByVal control_box_id As String)
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String

        msg = ""
        sql = "select * from controlbox_power where control_box_id='" & control_box_id & "'"
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            conn.Close()
            conn = Nothing
            Exit Sub

        End If
        If rs.RecordCount > 0 Then
            ck_getmeter.Checked = True
            tb_metertype_edit.Text = Trim(rs.Fields("powermeter_type").Value)
            tb_meterid_edit.Text = Trim(rs.Fields("powermeter_id").Value)
            If rs.Fields("powermeter_bianbi").Value Is System.DBNull.Value Then
                tb_meterbianbi_edit.Text = ""
            Else
                tb_meterbianbi_edit.Text = Trim(rs.Fields("powermeter_bianbi").Value)

            End If
        Else
            ck_getmeter.Checked = False
            tb_metertype_edit.Text = ""
            tb_meterid_edit.Text = ""
            tb_meterbianbi_edit.Text = ""
        End If

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing

    End Sub

    Private Sub bt_edit_control_box_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_edit_control_box.Click
        If tb_control_box_name.Text = "" Then
            MsgBox("主控箱名称不可以为空", , PROJECT_TITLE_STRING)
            tb_control_box_name.Focus()
            Exit Sub
        End If
        If tb_control_box_name.TextLength > 10 Then
            MsgBox("主控箱名称长度不可以大于10", , PROJECT_TITLE_STRING)
            tb_control_box_name.Focus()
            Exit Sub
        End If
        'If gprs.TextLength <> 16 Then  '输入的IMEI的长度必须是16
        '    MsgBox("IMEI长度必须为16，请重新输入！", , PROJECT_TITLE_STRING)
        '    gprs.Focus()  '光标定位在IMEI文本框中
        '    Exit Sub
        'End If

        If Trim(tb_imeistring.Text) = "" Then
            'IMEI   不可以为空
            MsgBox("IMEI不可以为空，请重新输入！", , PROJECT_TITLE_STRING)
            tb_imei.Focus()
            Exit Sub
        End If
        If tb_start_x.Text = "" Then '在地图上双击生成电控箱坐标
            MsgBox("请在地图上双击生成位置坐标", , PROJECT_TITLE_STRING)
            Exit Sub
        End If

        If tb_huilunum.Text = "" Then  '请输入
            MsgBox("请输入模拟量路数", , PROJECT_TITLE_STRING)
            Exit Sub

        End If
        'If kaiguan_num.Text = "" Then
        '    MsgBox("请输入开关量路数", , PROJECT_TITLE_STRING)
        '    Exit Sub
        'End If

        If tb_testboard_num.Text = "" Then
            MsgBox("请输入测量板个数", , PROJECT_TITLE_STRING)
            Exit Sub
        End If

        'If huilunum_text.TextLength > 100 Then  '备注信息
        '    MsgBox("备注内容长度超过100", , PROJECT_TITLE_STRING)
        '    huilunum_text.Focus()
        '    Exit Sub
        'End If

        'If lb_box_information.TextLength > 100 Then
        '    MsgBox("备注信息长度大于100", , PROJECT_TITLE_STRING)
        '    lb_box_information.Focus()
        '    Exit Sub
        'End If
        Dim rs As New ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim pos_add As System.Drawing.Point
        Dim imei_string, box_name_string, huilunum_string, testboardnum_string As String
        Dim conn As New ADODB.Connection
        Dim div_time_obj As New div_time_class  '时段控制实例化
        Dim boxtype As Integer = 1 '主控箱的版本
        msg = ""
        DBOperation.OpenConn(conn)

        '*********************'全角转换成半角*****************************
        imei_string = StrConv(Trim(tb_imeistring.Text), VbStrConv.Narrow)
        box_name_string = StrConv(Trim(tb_control_box_name.Text), VbStrConv.Narrow)
        box_name_string = box_name_string.Replace(" ", "") '将字符串中的空格去掉
        huilunum_string = StrConv(Trim(tb_huilunum.Text), VbStrConv.Narrow)
        '  kaiguannum_string = StrConv(Trim(kaiguan_num.Text), VbStrConv.Narrow)
        testboardnum_string = StrConv(Trim(tb_testboard_num.Text), VbStrConv.Narrow)
        '********************************************************************

        boxtype = Val(Trim(cb_box_type.Text))

        sql = "SELECT map_list.id, control_box.control_box_id, control_box_name , control_box.state, " _
               & "control_box.pos_y, control_box.pos_x FROM control_box INNER JOIN street ON control_box.street_id =" _
               & " street.street_id INNER JOIN area ON street.area_id = area.id INNER JOIN map_list ON area.area = map_list.area where map_list.id='" & g_choosemapid & "' and control_box.control_box_id='" & m_editboxid & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "edit_control_box_Click", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs.RecordCount <= 0 Then
            MsgBox("请匹配主控箱和地图再进行修改操作", , PROJECT_TITLE_STRING)
            rs.Close()
            rs = Nothing
            conn.Close()
            conn = Nothing
            Exit Sub
        End If

        If g_addboxtag = 2 Then
            '表示坐标被更新过
            '屏幕中的终端起点坐标转换城地图中的相对坐标
            pos_add.X = Val(tb_start_x.Text - g_welcomewinobj.GroupBox1.Location.X - g_welcomewinobj.DesktopLocation.X - (Me.Width - Me.ClientSize.Width) - g_welcomewinobj.pb_map.Location.X - g_welcomewinobj.SplitContainer3.Panel1.Width)
            pos_add.Y = Val(tb_start_y.Text - g_welcomewinobj.GroupBox1.Location.Y - g_welcomewinobj.DesktopLocation.Y - (Me.Height - Me.ClientSize.Height) - g_welcomewinobj.pb_map.Location.Y)

        Else
            '屏幕中的终端起点坐标转换城地图中的相对坐标
            pos_add.X = Val(tb_start_x.Text)
            pos_add.Y = Val(tb_start_y.Text)

        End If
        sql = "select * from RoadIDAndIMEI where IMEI='" & m_IMEI & "' and RoadID='" & Val(m_editboxid) & "'"  '更新RoadIDAndIMEI 表
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs.RecordCount > 0 Then

            rs.Fields("IMEI").Value = imei_string  '更新IMEI值

            If Me.ck_modvalue.Checked = True Then
                If Me.cb_controlmod.Text = "打开" Then
                    rs.Fields("information").Value = "打开"
                    Com_inf.Insert_Operation("下放时段控制模式")
                    g_mod_controlboxname = box_name_string  '选择时段控制修改的主控箱
                    g_modtag = 1
                    If g_welcomewinobj.BackgroundWorkerModXiaFang.IsBusy = False Then
                        g_welcomewinobj.BackgroundWorkerModXiaFang.RunWorkerAsync()
                        ' MsgBox("开始配置操作，结果请查看主控界面上的操作数据面板", , PROJECT_TITLE_STRING)

                    Else
                        MsgBox("下放线程正在运行，请稍后重试...", , PROJECT_TITLE_STRING)
                    End If
                Else
                    rs.Fields("information").Value = "关闭"
                    If MsgBox("您选择了关闭自动控制模式，将清空主控箱中下放的时段信息，是否继续？", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.Yes Then
                        g_mod_controlboxname = box_name_string  '选择时段控制修改的主控箱
                        Com_inf.Insert_Operation("清空时段控制模式")
                        g_modtag = 5  '清空时段控制模式
                        If g_welcomewinobj.BackgroundWorkerModXiaFang.IsBusy = False Then
                            g_welcomewinobj.BackgroundWorkerModXiaFang.RunWorkerAsync()
                            'MsgBox("开始配置操作，结果请查看主控界面上的操作数据面板", , PROJECT_TITLE_STRING)
                        Else
                            MsgBox("下放线程正在运行，请稍后重试...", , PROJECT_TITLE_STRING)
                        End If
                    End If

                End If


            End If
            rs.Update()
            ' rs.Fields("information").Value = Trim(lb_box_information.Text) '更新备注

            'Me.Control_infTableAdapter.Fill(Me.Control_box.control_inf)

        Else
            MsgBox("编辑出错，请双击选择需编辑的主控箱条目！", , PROJECT_TITLE_STRING)
            rs.Close()
            rs = Nothing
            conn.Close()
            conn = Nothing
            Exit Sub

        End If


        '更改电控箱control_box表
        sql = "select * from control_box where control_box_name='" & m_editcontrolboxname & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs.RecordCount > 0 Then
            rs.Fields("control_box_name").Value = box_name_string
            rs.Fields("pos_x").Value = pos_add.X
            rs.Fields("pos_y").Value = pos_add.Y
            rs.Fields("huilu_num").Value = Val(huilunum_string)
            '  rs.Fields("kaiguanliang_num").Value = Val(kaiguannum_string)
            rs.Fields("board_num").Value = Val(testboardnum_string)
            rs.Fields("control_box_type").Value = boxtype

            rs.Update()


        Else
            MsgBox("编辑出错，请双击选择需编辑的主控箱条目！", , PROJECT_TITLE_STRING)
            rs.Close()
            rs = Nothing
            conn.Close()
            conn = Nothing
            Exit Sub
        End If

        '2012年6月11日，编辑主控箱的备注信息
        edit_controlbox_inf(m_editboxid)

        '2012年7月13日，根据回路个数，设置huilu_inf中的配置，比如原来24路修改为12路则将后12路的配置删除
        sql = "delete huilu_inf where control_box_name='" & box_name_string & "' and huilu_id>" & Val(huilunum_string)
        DBOperation.ExecuteSQL(conn, sql, msg)



        '2011年12月12日增加抄表功能配置，抄表配置记录增加到一个新的表中，controlbox_power
        If ck_getmeter.Checked = True Then
            If tb_metertype_edit.Text = "" Then
                MsgBox("请输入电表规约", , PROJECT_TITLE_STRING)

            Else
                '电表编号可以为空，不知道的情况下为空，以后获取后补上
                sql = "select * from controlbox_power where control_box_id='" & m_editboxid & "'"
                rs = DBOperation.SelectSQL(conn, sql, msg)
                If rs Is Nothing Then
                    GoTo finish
                End If
                If rs.RecordCount > 0 Then
                    '存在电表的配置，进行更新
                    rs.Fields("powermeter_type").Value = Trim(Me.tb_metertype_edit.Text)
                    rs.Fields("powermeter_id").Value = Trim(Me.tb_meterid_edit.Text)
                    rs.Fields("powermeter_bianbi").Value = Val(Trim(Me.tb_meterbianbi_edit.Text))
                    rs.Fields("imei").Value = imei_string
                    rs.Update()
                Else
                    '原来不存在电表的配置，新增了电表的配置
                    rs.AddNew()
                    rs.Fields("control_box_id").Value = m_editboxid
                    rs.Fields("control_box_name").Value = m_editcontrolboxname
                    rs.Fields("powermeter_type").Value = Trim(Me.tb_metertype_edit.Text)
                    rs.Fields("powermeter_id").Value = Trim(Me.tb_meterid_edit.Text)
                    rs.Fields("powermeter_bianbi").Value = Val(Trim(Me.tb_meterbianbi_edit.Text))
                    rs.Fields("imei").Value = imei_string
                    rs.Update()
                End If


            End If


        Else
            '删除抄表功能
            sql = "delete from controlbox_power where control_box_id='" & m_editboxid & "'"
            DBOperation.ExecuteSQL(conn, sql, msg)

        End If


        rs.Close()
        rs = Nothing

finish:
        conn.Close()
        conn = Nothing


        '更新电控箱信息列表
        m_controlboxobj.set_controlbox_list(tv_box_inf_list) '主控箱信息列表

        '刷新主控界面左边的电控箱信息
        g_welcomewinobj.find_box_state() '电控箱列表

        '刷新时段控制
        div_time_obj.Div_time_show()

        '将修改信息的主控箱故障置1，等待下次新的故障上传
        clear_boxalarm_show(m_editcontrolboxname)
        clear_boxkaiguanalarm_show(m_editcontrolboxname)

        m_IMEI = ""
        m_editboxid = ""
        MsgBox("主控箱信息修改完成", , PROJECT_TITLE_STRING)

        '增加操作记录
        Com_inf.Insert_Operation("修改主控箱：" & box_name_string)


        '删除显示
        clear_boxshow()

        '刷新主控箱列表
        g_welcomewinobj.m_controlboxobj.set_controlbox_list(g_welcomewinobj.tv_box_inf_list)

        g_addboxtag = 1
    End Sub

    Private Sub clear_boxshow()

        tb_control_box_name.Text = ""
        tb_imeistring.Text = ""
        tb_huilunum.Text = ""
        cb_box_type.Text = ""
        tb_start_x.Text = ""
        tb_start_y.Text = ""
        tb_testboard_num.Text = ""

        Me.tb_phonenum.Text = ""
        Me.tb_ip.Text = ""
        Me.tb_com.Text = ""
        Me.tb_address.Text = ""
        Me.tb_denggan.Text = ""
        Me.tb_dianyuan.Text = ""
        Me.tb_chuchang_id.Text = ""
        Me.tb_gaoya.Text = ""
        Me.tb_jianxiu.Text = ""
        Me.tb_contact.Text = ""
        Me.tb_denggan_fanwei.Text = ""
    End Sub

    ''' <summary>
    ''' 根据主控箱的名称，在主控箱信息被修改后将主控箱当前的报警信息置为1，等待新的主控箱名称产生的报警信息
    ''' </summary>
    ''' <param name="control_box_name"></param>
    ''' <remarks></remarks>
    Private Sub clear_boxalarm_show(ByVal control_box_name As String)
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String
        msg = ""
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        sql = "update control_box_state set state='1' where control_box_name='" & control_box_name & "' and state='0'"
        DBOperation.ExecuteSQL(conn, sql, msg)

        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    ''' 根据主控箱的名称，在主控箱信息被修改后将主控箱当前的报警信息置为1，等待新的主控箱名称产生的报警信息
    ''' </summary>
    ''' <param name="control_box_name"></param>
    ''' <remarks></remarks>
    Private Sub clear_boxkaiguanalarm_show(ByVal control_box_name As String)
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String
        msg = ""
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        sql = "update kaiguan_alarm_list set alarm_tag=1 where control_box_name='" & control_box_name & "' and (alarm_tag=0 or alarm_tag=2)"
        DBOperation.ExecuteSQL(conn, sql, msg)

        conn.Close()
        conn = Nothing
    End Sub


    ''' <summary>
    ''' 删除区域内容
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_delete_control_box_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_delete_control_box.Click
        If Trim(tb_control_box_name.Text) = "" Or Trim(tb_imeistring.Text) = "" Or m_IMEI = "" Or m_editboxid = "" Then
            MsgBox("请双击需删除的主控箱条目", , PROJECT_TITLE_STRING)
            Exit Sub

        End If
        If MsgBox("删除区域，将删除所有与其相关灯的信息，是否删除所选主控箱：" & m_editboxid, MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.Yes Then
            Dim conn As New ADODB.Connection
            Dim msg As String
            Dim sql As String
            '删除区域后需要查询定位表中是否有该区域的名称，如果有则删除
            Dim rs As New ADODB.Recordset

            DBOperation.OpenConn(conn)

            msg = ""
            sql = "select control_box_name from control_box where control_box_id='" & m_editboxid & "'"
            rs = DBOperation.SelectSQL(conn, sql, msg)
            If rs Is Nothing Then
                MsgBox(MSG_ERROR_STRING & "delete_control_box_Click", , PROJECT_TITLE_STRING)
                conn.Close()
                conn = Nothing
                Exit Sub
            End If
            If rs.RecordCount > 0 Then
                '获取电控箱名称
                m_editcontrolboxname = Trim(rs.Fields("control_box_name").Value)

            End If

            sql = "delete from RoadIDAndIMEI where IMEI='" & m_IMEI & "' and RoadID='" & Val(m_editboxid) & "'"
            DBOperation.ExecuteSQL(conn, sql, msg)  '删除RoadIDAndIMEI表中关于gprs_id的内容

            '删除电控箱表中内容
            sql = "delete from control_box where control_box_id='" & m_editboxid & "'"
            DBOperation.ExecuteSQL(conn, sql, msg)
            '删除类型
            sql = "delete from box_lamptype where control_box_id='" & m_editboxid & "'"
            DBOperation.ExecuteSQL(conn, sql, msg)

            '删除lamp_inf该电控箱编号的终端信息
            sql = "delete from lamp_inf where control_box_id='" & m_editboxid & "'"
            DBOperation.ExecuteSQL(conn, sql, msg)

            '删除时段控制
            '1、经纬度
            sql = "delete from pianyi where lamp_id like'" & m_editboxid & "%'"
            DBOperation.ExecuteSQL(conn, sql, msg)
            '2、特殊
            sql = "delete from road_level where control_box_id='" & m_editboxid & "'"
            DBOperation.ExecuteSQL(conn, sql, msg)

            '3 节假日
            sql = "delete from Special_road_level where control_box_id='" & m_editboxid & "'"
            DBOperation.ExecuteSQL(conn, sql, msg)

            '删除报警信息
            sql = "delete from lamp_inf_record where substring(lamp_id,1,4)='" & m_editboxid & "' "

            DBOperation.ExecuteSQL(conn, sql, msg)

            '删除主控箱的回路设置
            sql = "delete from huilu_inf where control_box_name='" & m_editcontrolboxname & "'"

            DBOperation.ExecuteSQL(conn, sql, msg)


            '将修改信息的主控箱故障置1，等待下次新的故障上传
            clear_boxalarm_show(m_editcontrolboxname)
            clear_boxkaiguanalarm_show(m_editcontrolboxname)

            '删除主控箱备注信息表中的内容
            sql = "delete from controlbox_inf where control_box_id='" & m_editboxid & "'"
            DBOperation.ExecuteSQL(conn, sql, msg)

            MsgBox("删除成功！", , PROJECT_TITLE_STRING)  '提示删除成功


            '增加操作记录
            Com_inf.Insert_Operation("删除主控箱：" & m_editcontrolboxname)

            clear_text()
            m_controlboxobj.set_controlbox_list(tv_box_inf_list) '主控箱信息列表
            'Me.Control_infTableAdapter.Fill(Me.Control_box.control_inf)  '刷新电控箱列表
            'record_num.Text = "共有" & dgv_control_inf.RowCount.ToString & "条记录"  '刷新电控箱中的记录条数

            '根据电控箱名称，删除定位信息
            sql = "delete from street_position where control_box_name='" & m_editcontrolboxname & "'"
            DBOperation.ExecuteSQL(conn, sql, msg)
            '主控界面上刷新下拉框
            'sql = "select * from street_position "
            'g_welcomewinobj.cb_goto_area.Items.Clear()
            'rs = DBOperation.SelectSQL(conn, sql, msg)

            'While rs.EOF = False
            '    g_welcomewinobj.cb_goto_area.Items.Add(Trim(rs.Fields("control_box_name").Value))  '添加街道名称
            '    rs.MoveNext()
            'End While
            'If g_welcomewinobj.cb_goto_area.Items.Count > 0 Then
            '    g_welcomewinobj.cb_goto_area.SelectedIndex = 0    '选取第一个定位街道值

            'End If

            '2011年12月12日增加删除电能表中的配置信息
            sql = "delete from controlbox_power where control_box_id='" & m_editboxid & "'"
            DBOperation.ExecuteSQL(conn, sql, msg)

            '电控箱故障
            g_welcomewinobj.Control_boxTableAdapter.Fill(g_welcomewinobj.Control_box_state.control_box)

            '主控箱列表
            g_welcomewinobj.m_controlboxobj.set_controlbox_list(g_welcomewinobj.tv_box_inf_list)

            If rs.State = 1 Then
                rs.Close()
                rs = Nothing
            End If
            conn.Close()
            conn = Nothing  '关闭连接
        End If
        g_lampmap.Clear(Color.Empty)
        m_IMEI = ""
        m_editboxid = ""
        tb_control_box_name.Text = ""
        tb_imeistring.Text = ""
        tb_huilunum.Text = ""
        clear_boxshow()
        '刷新右侧列表
        g_welcomewinobj.SetControlBoxListDelegate(g_welcomewinobj.dgv_lamp_state_list, 0)

        '刷新主控界面左边的电控箱信息
        g_welcomewinobj.find_box_state() '电控箱列表

        '刷新时段控制
        Dim div_time_obj As New div_time_class
        div_time_obj.Div_time_show()

        '刷新主控箱列表
        g_welcomewinobj.m_controlboxobj.set_controlbox_list(g_welcomewinobj.tv_box_inf_list)

        ''首页的报警信息
        g_welcomewinobj.get_boxprobleminf()

    End Sub

    Private Sub cb_controlbox_name_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_controlbox_name.DropDown
        Com_inf.Select_box_name(cb_controlbox_name)
    End Sub

    ''' <summary>
    ''' 发送模拟量的配置信息，根据用户设置的模拟量路数划分为12个一组，一组一组发送
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_send_huiluinf_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_send_huiluinf.Click

        If rb_box_check.Checked = True Then
            '按主控箱名称进行下放
            If Trim(cb_controlbox_name.Text) = "" Then
                MsgBox("请选择主控箱名称", , PROJECT_TITLE_STRING)
                cb_controlbox_name.Focus()
                Exit Sub
            End If
            g_mod_controlboxname = Trim(cb_controlbox_name.Text)
        Else
            g_mod_controlboxname = ""
        End If

        If MsgBox("是否将模拟量配置到主控器？", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.No Then
            Exit Sub
        End If
        g_modtag = 2

        If g_welcomewinobj.BackgroundWorkerModXiaFang.IsBusy = False Then
            g_welcomewinobj.BackgroundWorkerModXiaFang.RunWorkerAsync()
            MsgBox("开始配置操作，结果请查看主控界面上的操作数据面板", , PROJECT_TITLE_STRING)
            '增加操作记录
            Com_inf.Insert_Operation("发送模拟量配置")
        Else
            MsgBox("配置线程正忙，请稍后操作...", , PROJECT_TITLE_STRING)
        End If
    End Sub


    ''' <summary>
    ''' 发送开关量跳变报警数据
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_send_kaiguan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_send_kaiguan.Click
        If rb_box_check.Checked = True Then
            '按主控箱名称进行下放
            If Trim(cb_controlbox_name.Text) = "" Then
                MsgBox("请选择主控箱名称", , PROJECT_TITLE_STRING)
                cb_controlbox_name.Focus()
                Exit Sub
            End If
            g_mod_controlboxname = Trim(cb_controlbox_name.Text)
        Else
            g_mod_controlboxname = ""
        End If

        If MsgBox("是否将开关量数据配置到主控器？", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.No Then
            Exit Sub
        End If
        g_modtag = 3

        If g_welcomewinobj.BackgroundWorkerModXiaFang.IsBusy = False Then
            g_welcomewinobj.BackgroundWorkerModXiaFang.RunWorkerAsync()
            MsgBox("开始配置操作，结果请查看主控界面上的操作数据面板", , PROJECT_TITLE_STRING)

            '增加操作记录
            Com_inf.Insert_Operation("发送开关量配置")

        Else
            MsgBox("配置线程正忙，请稍后操作...", , PROJECT_TITLE_STRING)
        End If
    End Sub

    Private Sub bt_sendboardnum_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_sendboardnum.Click
        If rb_box_check.Checked = True Then
            '按主控箱名称进行下放
            If Trim(cb_controlbox_name.Text) = "" Then
                MsgBox("请选择主控箱名称", , PROJECT_TITLE_STRING)
                cb_controlbox_name.Focus()
                Exit Sub
            End If
            g_mod_controlboxname = Trim(cb_controlbox_name.Text)
        Else
            g_mod_controlboxname = ""
        End If

        If MsgBox("是否将测量板个数下放？", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.No Then
            Exit Sub
        End If
        g_modtag = 6

        If g_welcomewinobj.BackgroundWorkerModXiaFang.IsBusy = False Then
            g_welcomewinobj.BackgroundWorkerModXiaFang.RunWorkerAsync()
            MsgBox("开始配置操作，结果请查看主控界面上的操作数据面板", , PROJECT_TITLE_STRING)

            '增加操作记录
            Com_inf.Insert_Operation("配置测量板个数")

        Else
            MsgBox("配置线程正忙，请稍后操作...", , PROJECT_TITLE_STRING)
        End If
    End Sub

    ''' <summary>
    ''' 发送测量板个数
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_send_testboardnum_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_send_alarmdata.Click
        If rb_box_check.Checked = True Then
            '按主控箱名称进行下放
            If Trim(cb_controlbox_name.Text) = "" Then
                MsgBox("请选择主控箱名称", , PROJECT_TITLE_STRING)
                cb_controlbox_name.Focus()
                Exit Sub
            End If
            g_mod_controlboxname = Trim(cb_controlbox_name.Text)
        Else
            g_mod_controlboxname = ""
        End If

        If MsgBox("是否将配置报警阈值？", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.No Then
            Exit Sub
        End If
        g_modtag = 4

        If g_welcomewinobj.BackgroundWorkerModXiaFang.IsBusy = False Then
            g_welcomewinobj.BackgroundWorkerModXiaFang.RunWorkerAsync()
            MsgBox("开始配置操作，结果请查看主控界面上的操作数据面板", , PROJECT_TITLE_STRING)

            '增加操作记录
            Com_inf.Insert_Operation("配置报警阈值")

        Else
            MsgBox("配置线程正忙，请稍后操作...", , PROJECT_TITLE_STRING)
        End If
    End Sub

    ''' <summary>
    ''' 选择1号接触器
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rb_k1_list_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_k1_list.Click
        If rb_k1_list.Checked = True Then
            m_jiechuqi_id = "K1"
            select_huiluid(1)
        End If
    End Sub

    ''' <summary>
    ''' 选择2号接触器
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rb_k2_list_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_k2_list.Click
        If rb_k2_list.Checked = True Then
            '选择第2个接触器编号
            m_jiechuqi_id = "K2"
            select_huiluid(2)
        End If
    End Sub

    ''' <summary>
    ''' 选择3号接触器
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rb_k3_list_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_k3_list.Click
        If rb_k3_list.Checked = True Then
            '选择第3个接触器编号
            m_jiechuqi_id = "K3"
            select_huiluid(3)
        End If
    End Sub

    ''' <summary>
    ''' 选择4号接触器
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rb_k4_list_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_k4_list.Click
        If rb_k4_list.Checked = True Then
            '选择第4个接触器编号
            m_jiechuqi_id = "K4"
            select_huiluid(4)
        End If
    End Sub

    ''' <summary>
    ''' 选择5号接触器
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rb_k5_list_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_k5_list.Click
        If rb_k5_list.Checked = True Then
            '选择第5个接触器编号
            m_jiechuqi_id = "K5"
            select_huiluid(5)
        End If
    End Sub

    ''' <summary>
    ''' 选择6号接触器
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rb_k6_list_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_k6_list.Click
        If rb_k6_list.Checked = True Then
            '选择第6个接触器编号
            m_jiechuqi_id = "K6"
            select_huiluid(6)
        End If
    End Sub

    ''' <summary>
    ''' 将回路与交流接触器编号及变比相匹配
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_set_huilu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_set_huilu.Click
        If m_configcontrolboxname = "" Then
            MsgBox("请双击选择主控箱的名称", , PROJECT_TITLE_STRING)
            Exit Sub
        End If
        If tb_bianbi_value.Text = "" Then
            MsgBox("请设置互感器变比", , PROJECT_TITLE_STRING)
            tb_bianbi_value.Focus()
            Exit Sub
        End If
        If rb_k1_list.Enabled = False And rb_k2_list.Enabled = False And rb_k3_list.Enabled = False And rb_k4_list.Enabled = False And rb_k5_list.Enabled = False And rb_k6_list.Enabled = False Then
            MsgBox("请先增加该主控箱下的接触器编号", , PROJECT_TITLE_STRING)
            Exit Sub
        End If
        'If information_text.TextLength > 100 Then
        '    MsgBox("备注信息不超过100", , PROJECT_TITLE_STRING)
        '    information_text.Focus()
        '    Exit Sub
        'End If

        Dim huilu(clb_huilu_idlist.CheckedItems.Count) As String    '存放回路编号
        Dim i As Integer = 0
        Dim jiechuqi_id As Integer = 1   '接触器编号
        Dim jiechuqi_id_string As String  '接触器编号
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String
        Dim rs As New ADODB.Recordset
        Dim bianbi As Integer  '变比值
        Dim huiluid As Integer   '回路编号
        Dim kaiguan_name As String '开关量名称
        Dim huilu_string As String '记录配置的回路编号
        DBOperation.OpenConn(conn)
        msg = ""

        bianbi = Val(Trim(tb_bianbi_value.Text))   '记录当前需要设置的变比值

        While i < clb_huilu_idlist.CheckedItems.Count
            huilu(i) = clb_huilu_idlist.CheckedItems(i)
            i += 1
        End While
        If Me.rb_k1_list.Checked = True Then
            jiechuqi_id = 1
        End If
        If Me.rb_k2_list.Checked = True Then
            jiechuqi_id = 2
        End If
        If Me.rb_k3_list.Checked = True Then
            jiechuqi_id = 3
        End If
        If Me.rb_k4_list.Checked = True Then
            jiechuqi_id = 4

        End If
        If Me.rb_k5_list.Checked = True Then
            jiechuqi_id = 5
        End If
        If Me.rb_k6_list.Checked = True Then
            jiechuqi_id = 6
        End If
        ''i = 0
        ''jiechuqi_id_string = m_control_box_id & "31"
        ''While jiechuqi_id_string.Length < 5 + LAMP_ID_LEN
        ''    jiechuqi_id_string &= "0"
        ''End While
        'jiechuqi_id_string = jiechuqi_id_string & jiechuqi_id.ToString
        jiechuqi_id_string = jiechuqi_id.ToString
        i = 0
        huilu_string = ""
        While i < huilu.Length - 1
            huiluid = Val(huilu(i).Split(" ")(0))
            huilu_string &= huiluid & " "

            sql = "select * from huilu_inf where control_box_name='" & m_configcontrolboxname & "' and huilu_id='" & huiluid & "'"
            rs = DBOperation.SelectSQL(conn, sql, msg)
            If rs Is Nothing Then
                MsgBox(MSG_ERROR_STRING & "set_huilu_Click", , PROJECT_TITLE_STRING)

                conn.Close()
                conn = Nothing
                Exit Sub
            End If

            If rs.RecordCount > 0 Then
                If MsgBox("主控箱" & m_configcontrolboxname & "下的回路: " & huiluid & "已配置，是否重新配置", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.Yes Then
                    rs.Fields("huilu_id").Value = huiluid
                    rs.Fields("jiechuqi_id").Value = jiechuqi_id_string
                    rs.Fields("bianbi").Value = bianbi
                    rs.Fields("open_close").Value = "断开"  '默认的情况下，回路为断开
                    rs.Fields("state").Value = "正常"   '默认的情况下，回路为正常工作
                    rs.Fields("presure_type").Value = (huiluid - 1) Mod 3  '电压的类型
                    rs.Fields("current_alarmtop").Value = bianbi * 5
                    rs.Fields("current_alarmbot").Value = 0
                    ' rs.Fields("information").Value = Trim(information_text.Text)
                    rs.Update()
                End If

            Else
                '增加新的回路设置
                rs.AddNew()
                rs.Fields("control_box_name").Value = m_configcontrolboxname
                rs.Fields("huilu_id").Value = huiluid
                rs.Fields("jiechuqi_id").Value = jiechuqi_id_string
                rs.Fields("bianbi").Value = bianbi
                rs.Fields("open_close").Value = "断开"  '默认的情况下，回路为断开
                '  rs.Fields("information").Value = Trim(information_text.Text)
                rs.Fields("state").Value = "正常" '默认的情况下，回路为正常工作
                rs.Fields("presure_type").Value = (huiluid - 1) Mod 3  '电压的类型
                rs.Fields("current_alarmtop").Value = bianbi * 5
                rs.Fields("current_alarmbot").Value = 0
                rs.Update()


            End If

            i += 1
        End While
        '2011年9月19日删除默认接触器辅助触点，变为K1，K2，K3，K4，K5，K6
        ''增加默认的接触器辅助触点
        'kaiguan_name = "接触器辅助触点" & huiluid
        kaiguan_name = "K" & jiechuqi_id_string
        sql = "select * from kaiguan_list where control_box_name='" & m_configcontrolboxname & "' and kaiguan_name='" & kaiguan_name & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs.RecordCount <= 0 Then
            sql = "insert into kaiguan_list(kaiguan_name,kaiguan_tag,alarm,control_box_name) values('" & kaiguan_name & "', 0,0,'" & m_configcontrolboxname & "')"
            DBOperation.ExecuteSQL(conn, sql, msg)

        End If
      

        MsgBox("回路配置成功", , PROJECT_TITLE_STRING)

        '增加操作记录
        Com_inf.Insert_Operation("配置" & m_configcontrolboxname & "接触器" & jiechuqi_id_string & "下回路: " & huilu_string)

        '刷新删除的回路列表
        Me.Huilu_infTableAdapter.FillBy_box(Me.EditDataSet.huilu_inf, m_configcontrolboxname)

        '根据选择的主控箱名称选择开关量配置列表
        Me.Kaiguan_listTableAdapter.FillBy_box(Me.KaiguanDataSet.kaiguan_list, m_configcontrolboxname)

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    ''' 编辑模拟量全选
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chb_allhuilu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chb_allhuilu.Click
        Dim i As Integer = 0
        If chb_allhuilu.Checked = True Then
            While i <= dgv_huilu_list.RowCount - 1
                dgv_huilu_list.Rows(i).Cells("checkid").Value = 1
                i += 1
            End While

        Else
            While i <= dgv_huilu_list.RowCount - 1
                dgv_huilu_list.Rows(i).Cells("checkid").Value = 0
                i += 1
            End While
        End If
    End Sub

    ''' <summary>
    ''' 保存回路编辑
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_save_huilu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_save_huilu.Click
        '保存选中的数据行
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim i As Integer = 0
        Dim huiluid As Integer
        Dim jiechuqiid As Integer
        Dim information As String
        Dim bianbi As Integer
        Dim id As Integer
        Dim current_top As Integer
        Dim current_bot As Integer
        Dim presuretype As Integer
        msg = ""
        If MsgBox("是否保存选中的数据", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.Yes Then
            DBOperation.OpenConn(conn)

            While i < dgv_huilu_list.RowCount
                If dgv_huilu_list.Rows(i).Cells("checkid").Value = 1 Then
                    '设置回路编号
                    If dgv_huilu_list.Rows(i).Cells("huilu_id").Value Is System.DBNull.Value Then
                        MsgBox("回路编号不可以为空", , PROJECT_TITLE_STRING)
                        GoTo finish
                    End If
                    huiluid = dgv_huilu_list.Rows(i).Cells("huilu_id").Value
                    If IsNumeric(huiluid) = False Then
                        MsgBox("回路编号请输入数字", , PROJECT_TITLE_STRING)
                        GoTo finish
                    End If

                    '接触器编号
                    If dgv_huilu_list.Rows(i).Cells("jiechuqi").Value Is System.DBNull.Value Then
                        MsgBox("接触器编号不可以为空", , PROJECT_TITLE_STRING)
                        GoTo finish
                    End If
                    jiechuqiid = Mid(dgv_huilu_list.Rows(i).Cells("jiechuqi").Value, 2, 2)

                    If Mid(dgv_huilu_list.Rows(i).Cells("jiechuqi").Value, 1, 1) <> "K" Or IsNumeric(jiechuqiid) = False Then
                        MsgBox("接触器编号请按规范输入", , PROJECT_TITLE_STRING)
                        GoTo finish
                    End If

                    '变比
                    If dgv_huilu_list.Rows(i).Cells("bianbi").Value Is System.DBNull.Value Then
                        MsgBox("变比值不可以为空", , PROJECT_TITLE_STRING)
                        GoTo finish
                    End If
                    bianbi = dgv_huilu_list.Rows(i).Cells("bianbi").Value
                    If IsNumeric(bianbi) = False Then
                        MsgBox("变比值请输入数字", , PROJECT_TITLE_STRING)
                        GoTo finish
                    End If

                    '备注信息
                    If dgv_huilu_list.Rows(i).Cells("information").Value Is System.DBNull.Value Then
                        information = ""
                    Else
                        information = Trim(dgv_huilu_list.Rows(i).Cells("information").Value)

                    End If
                    id = dgv_huilu_list.Rows(i).Cells("id").Value

                    '回路电压
                    If dgv_huilu_list.Rows(i).Cells("presure_type").Value Is System.DBNull.Value Then
                        MsgBox("回路电压不可以为空", , PROJECT_TITLE_STRING)
                        GoTo finish
                    End If
                    presuretype = Val(dgv_huilu_list.Rows(i).Cells("presure_type").Value)
                    If presuretype <> 0 And presuretype <> 1 And presuretype <> 2 Then
                        MsgBox("回路电压请输入0-2的数字", , PROJECT_TITLE_STRING)
                        GoTo finish
                    End If

                    '报警上限
                    If dgv_huilu_list.Rows(i).Cells("current_alarmtop").Value Is System.DBNull.Value Then
                        MsgBox("报警上限不可以为空", , PROJECT_TITLE_STRING)
                        GoTo finish
                    End If
                    current_top = dgv_huilu_list.Rows(i).Cells("current_alarmtop").Value
                    If IsNumeric(current_top) = False Then
                        MsgBox("报警上限必须为数字", , PROJECT_TITLE_STRING)
                        GoTo finish
                    End If

                    '报警下限
                    If dgv_huilu_list.Rows(i).Cells("current_alarmbot").Value Is System.DBNull.Value Then
                        MsgBox("报警下限不可以为空", , PROJECT_TITLE_STRING)
                        GoTo finish
                    End If
                    current_bot = dgv_huilu_list.Rows(i).Cells("current_alarmbot").Value
                    If IsNumeric(current_bot) = False Then
                        MsgBox("报警下限必须为数字", , PROJECT_TITLE_STRING)
                        GoTo finish
                    End If
                    If current_bot >= current_top Then
                        MsgBox("报警下限必须小于报警上限", , PROJECT_TITLE_STRING)
                        GoTo finish
                    End If
                    '更新新的数据
                    sql = "update huilu_inf set huilu_id='" & huiluid & "' , jiechuqi_id='" & jiechuqiid _
                    & "', bianbi='" & bianbi & "' , information='" & information & "', presure_type='" & presuretype _
                    & "',current_alarmtop='" & current_top & "' , current_alarmbot='" & current_bot & "' where id='" & id & "'"
                    'sql = "insert into huilu_inf(huilu_id,jiechuqi_id,bianbi,information) values('" & huiluid _
                    '& "','" & jiechuqiid & "','" & bianbi & "','" & information & "')"
                    DBOperation.ExecuteSQL(conn, sql, msg)

                    '增加操作记录
                    Com_inf.Insert_Operation("编辑" & m_configcontrolboxname & "模拟量")

                End If
                i += 1
            End While
            MsgBox("保存成功", , PROJECT_TITLE_STRING)

            'TODO: 这行代码将数据加载到表“EditDataSet.huilu_inf”中。您可以根据需要移动或移除它。
            Me.Huilu_infTableAdapter.FillBy_box(Me.EditDataSet.huilu_inf, m_configcontrolboxname)
finish:
            conn.Close()
            conn = Nothing
        End If

    End Sub

    ''' <summary>
    ''' 删除所选中的模拟量设置
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_del_huilu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_del_huilu.Click
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim i As Integer = 0
        Dim del_string As String = ""
        msg = ""
        If MsgBox("是否删除所选中的数据", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.Yes Then
            DBOperation.OpenConn(conn)
            While i < dgv_huilu_list.RowCount
                If dgv_huilu_list.Rows(i).Cells("checkid").Value = 1 Then
                    sql = "delete from huilu_inf where id='" & dgv_huilu_list.Rows(i).Cells("id").Value & "'"
                    DBOperation.ExecuteSQL(conn, sql, msg)
                    del_string &= dgv_huilu_list.Rows(i).Cells("huilu_id").Value & " "
                End If
                i += 1
            End While

            If del_string.Length > 100 Then
                del_string = Mid(del_string, 1, 90) & "..."
            End If
            '增加操作记录
            Com_inf.Insert_Operation("删除" & m_configcontrolboxname & "回路：" & del_string & "的模拟量设置")

            '
            '刷新删除的回路列表
            Me.Huilu_infTableAdapter.FillBy_box(Me.EditDataSet.huilu_inf, m_configcontrolboxname)


            conn.Close()
            conn = Nothing
        End If

    End Sub

    ''' <summary>
    ''' 选择需要编辑的所有开关量
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chb_checkall_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chb_checkall.Click
        Dim i As Integer = 0
        If chb_checkall.Checked = True Then
            While i < dgv_kaiguanliang_list.RowCount - 1
                dgv_kaiguanliang_list.Rows(i).Cells("kgcheckid").Value = 1
                i += 1
            End While

        Else
            While i < dgv_kaiguanliang_list.RowCount - 1
                dgv_kaiguanliang_list.Rows(i).Cells("kgcheckid").Value = 0
                i += 1
            End While
        End If
    End Sub

    ''' <summary>
    ''' 保存开关量的编辑
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_save_kaiguan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_save_kaiguan.Click
        If Me.m_configcontrolboxname = "" Then
            MsgBox("请选择主控箱名称", , PROJECT_TITLE_STRING)
            Exit Sub
        End If

        If MsgBox("是否保存选中的数据", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.Yes Then
            Dim conn As New ADODB.Connection
            Dim sql As String
            Dim msg As String
            Dim i As Integer = 0
            Dim kaiguanname As String
            Dim kaiguantag As Integer
            Dim alarm As Integer
            Dim rs As New ADODB.Recordset
            Dim add_string As String = ""

            msg = ""
            sql = ""
            DBOperation.OpenConn(conn)
            While i < Me.dgv_kaiguanliang_list.Rows.Count
                If Me.dgv_kaiguanliang_list.Rows(i).Cells("kgcheckid").Value = 1 Then
                    '开关量名称
                    If dgv_kaiguanliang_list.Rows(i).Cells("kaiguan_name").Value Is System.DBNull.Value Then
                        MsgBox("开关量名称不可以为空", , PROJECT_TITLE_STRING)
                        GoTo finish
                    End If
                    If Trim(dgv_kaiguanliang_list.Rows(i).Cells("kaiguan_name").Value).Length > 10 Then
                        MsgBox("开关量名称长度不超过10", , PROJECT_TITLE_STRING)
                        GoTo finish
                    End If
                    kaiguanname = Trim(Me.dgv_kaiguanliang_list.Rows(i).Cells("kaiguan_name").Value)
                    add_string &= kaiguanname & " "

                    '物理矢量
                    If dgv_kaiguanliang_list.Rows(i).Cells("kaiguan_tag").Value Is System.DBNull.Value Then
                        MsgBox("物理矢量不可以为空", , PROJECT_TITLE_STRING)
                        GoTo finish
                    End If
                    If IsNumeric(dgv_kaiguanliang_list.Rows(i).Cells("kaiguan_tag").Value) = False Then
                        MsgBox("物理矢量必须为数字", , PROJECT_TITLE_STRING)
                        GoTo finish
                    End If
                    If Val(dgv_kaiguanliang_list.Rows(i).Cells("kaiguan_tag").Value) < 1 Or Val(dgv_kaiguanliang_list.Rows(i).Cells("kaiguan_tag").Value) > 16 Then
                        MsgBox("物理矢量必须为1-16的数字", , PROJECT_TITLE_STRING)
                        GoTo finish
                    End If
                    kaiguantag = dgv_kaiguanliang_list.Rows(i).Cells("kaiguan_tag").Value

                    ''报警参数
                    'If kaiguanliang_list.Rows(i).Cells("alarm").Value Is System.DBNull.Value Then
                    '    MsgBox("报警参数不可以为空", , PROJECT_TITLE_STRING)
                    '    GoTo finish
                    'End If
                    'If kaiguanliang_list.Rows(i).Cells("alarm").Value <> 0 And kaiguanliang_list.Rows(i).Cells("alarm").Value <> 1 Then
                    '    MsgBox("报警参数必须为0或1", , PROJECT_TITLE_STRING)
                    '    GoTo finish
                    'End If
                    sql = "select alarm_id from tiaobian_alarm where shiliang_id='" & kaiguantag & "'"
                    rs = DBOperation.SelectSQL(conn, sql, msg)
                    If rs Is Nothing Then
                        MsgBox(MSG_ERROR_STRING & "save_kaiguan_Click", , PROJECT_TITLE_STRING)
                        conn.Close()
                        conn = Nothing
                        Exit Sub
                    End If
                    If rs.RecordCount > 0 Then
                        alarm = rs.Fields("alarm_id").Value
                    Else
                        alarm = 0

                    End If

                    If dgv_kaiguanliang_list.Rows(i).Cells("kgcontrol_box_name").Value Is System.DBNull.Value Then
                        '主控箱 为空表示新增加
                        sql = "insert into kaiguan_list(kaiguan_name,kaiguan_tag,alarm,control_box_name) values('" & kaiguanname & "', '" & kaiguantag & "' ,'" & alarm & "','" & m_configcontrolboxname & "')"
                        DBOperation.ExecuteSQL(conn, sql, msg)

                    Else
                        '主控箱不为空表示编辑
                        sql = "update kaiguan_list set kaiguan_name='" & kaiguanname & "', kaiguan_tag='" & kaiguantag & "' , alarm='" & alarm & "', control_box_name='" & m_configcontrolboxname & "' where id ='" & dgv_kaiguanliang_list.Rows(i).Cells("kgid").Value & "'"
                        DBOperation.ExecuteSQL(conn, sql, msg)

                    End If


                    '检查已设的报警，看是否有该开关量名称的
                    sql = "select * from alarm_typelist where control_box_name='" & m_configcontrolboxname & "' and alarm_type like'" & kaiguanname & "%'"
                    rs = DBOperation.SelectSQL(conn, sql, msg)
                    If rs Is Nothing Then
                        MsgBox(MSG_ERROR_STRING & "save_kaiguan_Click", , PROJECT_TITLE_STRING)
                        conn.Close()
                        conn = Nothing
                        Exit Sub
                    End If

                    While rs.EOF = False
                        rs.Fields("kaiguan_tag").Value = kaiguantag
                        rs.Update()
                        rs.MoveNext()
                    End While


                End If

                i += 1
            End While

            '增加操作记录
            Com_inf.Insert_Operation("编辑" & m_configcontrolboxname & "开关量: " & add_string)

            Me.Kaiguan_listTableAdapter.FillBy_box(Me.KaiguanDataSet.kaiguan_list, m_configcontrolboxname)
            chb_checkall.Checked = False
finish:
            If rs.State = 1 Then
                rs.Close()
                rs = Nothing
            End If
            conn.Close()
            conn = Nothing

        End If

    End Sub

    Private Sub bt_del_kaiguan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_del_kaiguan.Click
        If Me.m_configcontrolboxname = "" Then
            MsgBox("请选择主控箱名称", , PROJECT_TITLE_STRING)
            Exit Sub
        End If

        If MsgBox("是否删除选中的数据", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.Yes Then
            Dim conn As New ADODB.Connection
            Dim sql As String
            Dim msg As String
            Dim i As Integer = 0
            Dim del_string As String = ""

            msg = ""
            sql = ""
            DBOperation.OpenConn(conn)
            While i < Me.dgv_kaiguanliang_list.Rows.Count
                If Me.dgv_kaiguanliang_list.Rows(i).Cells("kgcheckid").Value = 1 Then
                    sql = "delete from kaiguan_list where id= '" & Me.dgv_kaiguanliang_list.Rows(i).Cells("kgid").Value & "'"
                    DBOperation.ExecuteSQL(conn, sql, msg)
                    del_string &= ""
                End If
                del_string &= Me.dgv_kaiguanliang_list.Rows(i).Cells("kaiguan_name").Value & " "
                i += 1
            End While

            If del_string.Length > 100 Then
                del_string = Mid(del_string, 1, 90) & "..."
            End If
            '增加操作记录
            Com_inf.Insert_Operation("删除" & m_configcontrolboxname & "开关量:" & del_string)

            Me.Kaiguan_listTableAdapter.FillBy_box(Me.KaiguanDataSet.kaiguan_list, m_configcontrolboxname)
            chb_checkall.Checked = False
finish:
            conn.Close()
            conn = Nothing

        End If

    End Sub


    ''' <summary>
    ''' 选择设置跳变报警
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chb_set_alarm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chb_set_alarm.Click
        If chb_set_alarm.Checked = True Then
            Me.GroupBoxAlarm.Enabled = True
        Else
            Me.GroupBoxAlarm.Enabled = False
        End If
    End Sub
    ''' <summary>
    ''' 设置跳变报警，与物理矢量相对应
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_set_alarm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_set_alarmvalue.Click
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String

        msg = ""
        If cb_shiliang_list.Text = "" Then
            MsgBox("请选择物理矢量", , PROJECT_TITLE_STRING)
            cb_shiliang_list.Focus()
            Exit Sub
        End If

        If cb_tiaobian_list.Text = "" Then
            MsgBox("请选择跳变报警", , PROJECT_TITLE_STRING)
            cb_tiaobian_list.Focus()
            Exit Sub
        End If
        sql = "select alarm_id, shiliang_id from tiaobian_alarm where shiliang_id='" & Val(Trim(cb_shiliang_list.Text)) & "'"
        DBOperation.OpenConn(conn)
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "Set_alarm_Click", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs.RecordCount > 0 Then
            rs.Fields("alarm_id").Value = Val(Trim(cb_tiaobian_list.Text))
            rs.Update()
        Else
            rs.AddNew()
            rs.Fields("shiliang_id").Value = Val(Trim(cb_shiliang_list.Text))
            rs.Fields("alarm_id").Value = Val(Trim(cb_tiaobian_list.Text))
            rs.Update()
        End If

        sql = "update kaiguan_list set alarm='" & Val(Trim(cb_tiaobian_list.Text)) & "' where kaiguan_tag='" & Val(Trim(cb_shiliang_list.Text)) & "'"
        DBOperation.ExecuteSQL(conn, sql, msg)



        '刷新开关量配置列表
        Me.Kaiguan_listTableAdapter.FillBy_box(Me.KaiguanDataSet.kaiguan_list, m_configcontrolboxname)

        MsgBox("跳变报警参数设置成功", , PROJECT_TITLE_STRING)

        '增加操作记录
        Com_inf.Insert_Operation("设置跳变报警参数：" & Trim(cb_shiliang_list.Text))

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    ''' 将主控箱的信息导出一张表
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_boxinf_excel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_boxinf_excel.Click
        If Me.tv_box_inf_list.SelectedNode Is Nothing Then
            MsgBox("请选择信息导出的主控箱名称", , PROJECT_TITLE_STRING)
            Exit Sub
        Else
            If Me.tv_box_inf_list.SelectedNode.Level = 3 Then
                m_excelboxname = Trim(Me.tv_box_inf_list.SelectedNode.Text).Split(" ")(0)  '选择主控箱名称
            Else
                MsgBox("请选择信息导出的主控箱名称", , PROJECT_TITLE_STRING)
                Exit Sub
            End If
        End If

        If Me.BackgroundWorkerexcel.IsBusy = False Then
            Me.BackgroundWorkerexcel.RunWorkerAsync()
        End If
    End Sub

    Private Sub BackgroundWorkerexcel_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorkerexcel.DoWork
        boxinf_excel()
    End Sub

    Private Sub boxinf_excel()
        m_xlApp = (New Microsoft.Office.Interop.Excel.Application)

        m_xlBook = m_xlApp.Workbooks().Add
        m_xlSheet = m_xlBook.Worksheets("sheet1")
        m_xlApp.Cells(1, 1) = "主控箱信息表"
        m_xlApp.Rows(1).RowHeight = 50
        m_xlApp.Rows(1).Font.Size = 18
        m_xlApp.Rows(1).Font.Bold = True
        m_xlApp.Cells(2, 1) = "时间：" & Now

        m_xlApp.Rows(2).RowHeight = 30
        m_xlApp.Rows(2).Font.Size = 12

        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String = ""
        Dim controlboxid As String
        Dim huilu, board_num, controlboxtype, IMEI As String
        Dim phone_num, address, ip, com, chuchang_id, gaoya, jianxiu, jianxiu_contact, denggan_fanwei, dianyuan, denggan As String
        Dim huilu_id, jiechuqi_id, bianbi, current_top, current_bot, presure_type As String

        If DBOperation.OpenConn(conn) = False Then
            conn.Close()
            conn = Nothing
            Exit Sub
        End If

        sql = "select * from Box_IMEI where control_box_name='" & Me.m_excelboxname & "'"

        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs.RecordCount <= 0 Then
            rs.Close()
            rs = Nothing
            conn.Close()
            conn = Nothing
        End If

        controlboxid = Trim(rs.Fields("control_box_id").Value)
        huilu = Trim(rs.Fields("huilu_num").Value)
        board_num = Trim(rs.Fields("board_num").Value)
        controlboxtype = Trim(rs.Fields("control_box_type").Value)
        IMEI = Trim(rs.Fields("IMEI").Value)

        sql = "select * from  controlbox_inf where control_box_id='" & controlboxid & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs.RecordCount > 0 Then
            phone_num = Trim(rs.Fields("phone_num").Value)
            address = Trim(rs.Fields("address").Value)
            ip = Trim(rs.Fields("ip").Value)
            com = Trim(rs.Fields("com").Value)
            chuchang_id = Trim(rs.Fields("chuchang_id").Value)
            gaoya = Trim(rs.Fields("gaoya").Value)
            jianxiu = Trim(rs.Fields("jianxiu").Value)
            jianxiu_contact = Trim(rs.Fields("jianxiu_contact").Value)
            denggan_fanwei = Trim(rs.Fields("denggan_fanwei").Value)
            dianyuan = Trim(rs.Fields("dianyuan").Value)
            denggan = Trim(rs.Fields("denggan").Value)
        Else
            phone_num = ""
            address = ""
            ip = ""
            com = ""
            chuchang_id = ""
            gaoya = ""
            jianxiu = ""
            jianxiu_contact = ""
            denggan_fanwei = ""
            dianyuan = ""
            denggan = ""

        End If

       


        m_xlApp.Cells(3, 1) = "主控箱编号"
        m_xlApp.Cells(3, 2) = "'" & controlboxid
        m_xlApp.Cells(3, 3) = "主控箱名称"
        m_xlApp.Cells(3, 4) = "'" & m_excelboxname
        m_xlApp.Cells(3, 5) = "安装地点"
        m_xlApp.Cells(3, 6) = "'" & address

        m_xlApp.Cells(4, 1) = "主控箱参数"

        m_xlApp.Cells(5, 1) = "IP地址"
        m_xlApp.Cells(5, 2) = "端口"
        m_xlApp.Cells(5, 3) = "手机号码"
        m_xlApp.Cells(5, 4) = "IMEI"
        m_xlApp.Cells(5, 5) = "测量板个数"
        m_xlApp.Cells(5, 6) = "模拟量路数"
        m_xlApp.Cells(5, 7) = "主控箱版本号"


        m_xlApp.Cells(6, 1) = "'" & ip
        m_xlApp.Cells(6, 2) = "'" & com
        m_xlApp.Cells(6, 3) = "'" & phone_num
        m_xlApp.Cells(6, 4) = "'" & IMEI
        m_xlApp.Cells(6, 5) = "'" & board_num
        m_xlApp.Cells(6, 6) = "'" & huilu
        m_xlApp.Cells(6, 7) = "'" & controlboxtype

        m_xlApp.Cells(7, 1) = "供电电源"
        m_xlApp.Cells(7, 2) = "出厂编号"
        m_xlApp.Cells(7, 3) = "高压出线"
        m_xlApp.Cells(7, 4) = "负责检修人"
        m_xlApp.Cells(7, 5) = "联系方式"
        m_xlApp.Cells(7, 6) = ""
        m_xlApp.Cells(7, 7) = ""


        m_xlApp.Cells(8, 1) = "'" & dianyuan
        m_xlApp.Cells(8, 2) = "'" & chuchang_id
        m_xlApp.Cells(8, 3) = "'" & gaoya
        m_xlApp.Cells(8, 4) = "'" & jianxiu
        m_xlApp.Cells(8, 5) = "'" & jianxiu_contact
        m_xlApp.Cells(8, 6) = ""
        m_xlApp.Cells(8, 7) = ""

        m_xlApp.Cells(9, 1) = "对应灯杆号"
        m_xlApp.Cells(9, 2) = "灯杆范围"
        m_xlApp.Cells(9, 3) = ""
        m_xlApp.Cells(9, 4) = ""
        m_xlApp.Cells(9, 5) = ""
        m_xlApp.Cells(9, 6) = ""
        m_xlApp.Cells(9, 7) = ""


        m_xlApp.Cells(10, 1) = "'" & denggan
        m_xlApp.Cells(10, 2) = "'" & denggan_fanwei
        m_xlApp.Cells(10, 3) = ""
        m_xlApp.Cells(10, 4) = ""
        m_xlApp.Cells(10, 5) = ""
        m_xlApp.Cells(10, 6) = ""
        m_xlApp.Cells(10, 7) = ""

        m_xlApp.Cells(11, 1) = "遥测节点参数"

        m_xlApp.Cells(12, 1) = "遥测节点"
        m_xlApp.Cells(12, 2) = "接触器"
        m_xlApp.Cells(12, 3) = "变比"
        m_xlApp.Cells(12, 4) = "电流上限"
        m_xlApp.Cells(12, 5) = "电流下限"
        m_xlApp.Cells(12, 6) = "回路电压"
        m_xlApp.Cells(12, 7) = "备注"

        '回路信息
        sql = "select * from huilu_inf where control_box_name='" & m_excelboxname & "' order by huilu_id"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        Dim i As Integer = 1
        While rs.EOF = False
            huilu_id = Trim(rs.Fields("huilu_id").Value)
            jiechuqi_id = Trim(rs.Fields("jiechuqi_id").Value)
            bianbi = Trim(rs.Fields("bianbi").Value)
            If rs.Fields("current_alarmtop").Value IsNot System.DBNull.Value Then
                current_top = Trim(rs.Fields("current_alarmtop").Value)
            Else
                current_top = ""
            End If
            If rs.Fields("current_alarmbot").Value IsNot System.DBNull.Value Then
                current_bot = Trim(rs.Fields("current_alarmbot").Value)
            Else
                current_bot = ""
            End If

            If rs.Fields("presure_type").Value IsNot System.DBNull.Value Then

                presure_type = Trim(rs.Fields("presure_type").Value)
                If presure_type = "0" Then
                    presure_type = "A相"
                Else
                    If presure_type = "1" Then
                        presure_type = "B相"
                    Else
                        presure_type = "C相"
                    End If
                End If
            Else
                presure_type = ""
            End If
            m_xlApp.Cells(12 + i, 1) = "'" & huilu_id
            m_xlApp.Cells(12 + i, 2) = "'" & jiechuqi_id
            m_xlApp.Cells(12 + i, 3) = "'" & bianbi
            m_xlApp.Cells(12 + i, 4) = "'" & current_top
            m_xlApp.Cells(12 + i, 5) = "'" & current_bot
            m_xlApp.Cells(12 + i, 6) = "'" & presure_type
            m_xlApp.Cells(12 + i, 7) = ""

            i += 1
            rs.MoveNext()
        End While


        m_xlApp.Rows(2).RowHeight = 30
        m_xlApp.Rows(3).RowHeight = 30
        m_xlApp.Rows(4).RowHeight = 30
        m_xlApp.Rows(5).RowHeight = 15
        m_xlApp.Rows(6).RowHeight = 20
        m_xlApp.Rows(7).RowHeight = 15
        m_xlApp.Rows(8).RowHeight = 20
        m_xlApp.Rows(9).RowHeight = 15
        m_xlApp.Rows(10).RowHeight = 20
        m_xlApp.Rows(11).RowHeight = 30


        With m_xlSheet
            .Range(.Cells(1, 1), .Cells(1, 7)).Merge()
            .Range(.Cells(2, 1), .Cells(2, 7)).Merge()
            .Range(.Cells(3, 6), .Cells(3, 7)).Merge()
            .Range(.Cells(4, 1), .Cells(4, 7)).Merge()
            .Range(.Cells(7, 5), .Cells(7, 7)).Merge()
            .Range(.Cells(8, 5), .Cells(8, 7)).Merge()
            .Range(.Cells(9, 2), .Cells(9, 7)).Merge()
            .Range(.Cells(10, 2), .Cells(10, 7)).Merge()
            .Range(.Cells(11, 1), .Cells(11, 7)).Merge()



            .Range(.Cells(3, 1), .Cells(4, 7)).RowHeight = 30

            .Range(.Cells(3, 1), .Cells(3, 7)).ColumnWidth = 12


            '  .Range(.Cells(3, 1), .Cells(rows, 17)).RowHeight = 20

            .Range(.Cells(1, 1), .Cells(1, 7)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter
            .Range(.Cells(2, 1), .Cells(2, 7)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight
            .Range(.Cells(3, 1), .Cells(3, 7)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft
            .Range(.Cells(4, 1), .Cells(11 + i, 7)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter

            '   .Range(.Cells(3, 1), .Cells(rows, 17)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter


            .Range(.Cells(3, 1), .Cells(3, 7)).Font.Name = "宋体"
            '设标题为宋体字
            .Range(.Cells(3, 1), .Cells(3, 1)).Font.Bold = "True"
            .Range(.Cells(3, 3), .Cells(3, 3)).Font.Bold = "True"
            .Range(.Cells(3, 5), .Cells(3, 5)).Font.Bold = "True"
            .Range(.Cells(4, 1), .Cells(4, 1)).Font.Bold = "True"
            .Range(.Cells(5, 1), .Cells(5, 7)).Font.Bold = "True"
            .Range(.Cells(7, 1), .Cells(7, 7)).Font.Bold = "True"
            .Range(.Cells(9, 1), .Cells(9, 7)).Font.Bold = "True"
            .Range(.Cells(11, 1), .Cells(11, 7)).Font.Bold = "True"
            .Range(.Cells(12, 1), .Cells(12, 7)).Font.Bold = "True"
            '标题字体加粗
            .Range(.Cells(1, 1), .Cells(2, 7)).Borders.LineStyle = 0
            .Range(.Cells(3, 1), .Cells(11 + i, 7)).Borders.LineStyle = 1
            '设表格边框样式
            .Range(.Cells(2, 1), .Cells(11 + i, 7)).Font.Size = 10

            '表中数据的字号

            .PageSetup.CenterFooter = "第 &P 页/共 &N 页"

        End With


        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing

    End Sub

    Private Sub BackgroundWorkerexcel_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorkerexcel.RunWorkerCompleted
        m_xlApp.Visible = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub rb_checkall_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_checkall.Click
        If rb_checkall.Checked = True Then
            cb_controlbox_name.Text = ""
            cb_controlbox_name.Enabled = False
        End If
    End Sub

    Private Sub rb_box_check_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_box_check.Click
        If rb_box_check.Checked = True Then
            cb_controlbox_name.Enabled = True
        End If
    End Sub

    'Private Sub tc_boxinf_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tc_boxinf.SelectedIndexChanged
    '    If tc_boxinf.SelectedIndex = 0 Then
    '        g_addboxtag = 1
    '    End If
    '    If tc_boxinf.SelectedIndex = 1 Then
    '        g_addboxtag = 3
    '    End If
    'End Sub

    Private Sub cb_control_box_type_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_control_box_type.MouseHover
        Me.ToolTip_boxtype.SetToolTip(cb_control_box_type, "版本1目前已不用；版本2为大三遥(AAAABBBBCCCC)；版本3为小三遥(AABBCC); " & vbCrLf & "版本4为大三遥(ABCABCABCABC)；版本5为小三遥(ABCABC)")
    End Sub

    Private Sub cb_box_type_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_box_type.MouseHover
        Me.ToolTip_boxtype.SetToolTip(cb_box_type, "版本1目前已不用；版本2为大三遥(AAAABBBBCCCC)；版本3为小三遥(AABBCC); " & vbCrLf & "版本4为大三遥(ABCABCABCABC)；版本5为小三遥(ABCABC)")
    End Sub

    Private Sub tb_testboard_num_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tb_testboard_num.TextChanged

        If tb_testboard_num.Text <> "" Then
            If Trim(cb_box_type.Text) = 2 Or Trim(cb_box_type.Text) = 4 Then
                '大三遥
                tb_huilunum.Text = 12 * Val(tb_testboard_num.Text)
            Else
                tb_huilunum.Text = 6 * Val(tb_testboard_num.Text)
            End If
        End If

    End Sub

    Private Sub cb_box_type_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_box_type.SelectedIndexChanged
        If IsNumeric(Trim(cb_box_type.Text)) = False Then
            MsgBox("主控箱版本必须为数字1,2,3,4,5", , PROJECT_TITLE_STRING)
            Exit Sub
        End If
        If Val(Trim(cb_box_type.Text)) < 1 Or Val(Trim(cb_box_type.Text)) > 5 Then
            MsgBox("主控箱版本必须为数字1,2,3,4,5", , PROJECT_TITLE_STRING)
            Exit Sub
        End If

        If Trim(cb_box_type.Text) = 2 Or Trim(cb_box_type.Text) = 4 Then
            '大三遥
            tb_huilunum.Text = 12 * Val(tb_testboard_num.Text)
        Else
            tb_huilunum.Text = 6 * Val(tb_testboard_num.Text)
        End If
    End Sub

    Private Sub ck_modvalue_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub ck_modvalue_CheckedChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ck_modvalue.CheckedChanged
        If ck_modvalue.Checked = True Then
            cb_controlmod.Enabled = True
        Else
            cb_controlmod.Enabled = False
        End If
    End Sub
End Class