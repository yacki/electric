''' <summary>
''' 增加某一区域(电控箱)的灯的类型，灯的编号，删除某一盏或多盏灯
''' </summary>
''' <remarks></remarks>

Public Class 增加终端
    Public m_addtag As Integer  '增加终端的标志
    Private m_lampidline() As String '终端编号行数组
    Dim m_control_box_name As String  '主控箱的名称
    Dim m_control_box_id As String '主控箱的编号
    Dim m_jiechuqi_id As String = "K1"  '接触器编号
    Dim m_6huilu() = {"A1", "A2", "B1", "B2", "C1", "C2"}
    Dim m_12huilu() = {"A1", "A2", "A3", "A4", "B1", "B2", "B3", "B4", "C1", "C2", "C3", "C4"}
    Dim m_24huilu() = {"A1", "A2", "A3", "A4", "B1", "B2", "B3", "B4", "C1", "C2", "C3", "C4", _
                    "A5", "A6", "A7", "A8", "B5", "B6", "B7", "B8", "C5", "C6", "C7", "C8"}
    Dim m_36huilu() = {"A1", "A2", "A3", "A4", "B1", "B2", "B3", "B4", "C1", "C2", "C3", "C4", _
                    "A5", "A6", "A7", "A8", "B5", "B6", "B7", "B8", "C5", "C6", "C7", "C8", _
                     "A9", "A10", "A11", "A12", "B9", "B10", "B11", "B12", "C9", "C10", "C11", "C12"}

    ''' <summary>
    ''' 根据电控箱名称和类型获取景观灯编号
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Get_lamp_id_inf()
        Dim rs As ADODB.Recordset
        Dim sql As String
        Dim msg As String
        Dim i As Integer
        i = 0
        Dim conn As New ADODB.Connection
        DBOperation.OpenConn(conn)


        msg = ""
        tb_lamp_id.Text = ""
        sql = "select * from lamp_street where control_box_name='" & Trim(cb_box_add.Text) & "'and type_string='" & Trim(cb_lamp_type.Text) & "'order by total_num desc"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs.RecordCount > 0 Then
            Dim id As String
            id = (Val(Mid(Trim(rs.Fields("lamp_id").Value), 7, LAMP_ID_LEN)) + 1).ToString  '提示编号为目前终端编号中最大的加一
            tb_lamp_id.Text = id
            i = 3 - id.Length
            While i > 0  '如果不足3位则用0补足
                tb_lamp_id.Text = "0" & tb_lamp_id.Text
                i -= 1
            End While
        Else
            tb_lamp_id.Text = "001" '如果是第一盏终端，则使用001

        End If

        rs.Close()
        rs = Nothing
        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    '''增加灯
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub add_street()
        If cb_city_add.Text = "" Then
            MsgBox("请选择城市名称", , PROJECT_TITLE_STRING)
            cb_city_add.Focus() '光标定位在城市名称文本框
            Exit Sub
        End If
        If cb_area_add.Text = "" Then
            MsgBox("请选择区域名称", , PROJECT_TITLE_STRING)
            cb_area_add.Focus() '光标定位在城市名称文本框
            Exit Sub
        End If
        If cb_street_all.Text = "" Then
            MsgBox("请选择街道名称", , PROJECT_TITLE_STRING)
            cb_street_all.Focus() '光标定位在城市名称文本框
            Exit Sub
        End If
        If cb_box_add.Text = "" Then  '如果电控箱名称为空
            MsgBox("请选择主控箱名称", , PROJECT_TITLE_STRING)
            cb_box_add.Focus() '光标定位在电控箱名称文本框
            Exit Sub
        End If
        If cb_lamp_type.Text = "" Then  '如果景观灯类型为空
            MsgBox("请选择类型名称", , PROJECT_TITLE_STRING)
            cb_lamp_type.Focus()  '光标定位在景观灯类型
            Exit Sub
        End If

        If tb_lamp_num.Text = "" Then '如果一段路中增加的景观灯个数为空
            MsgBox("请输入该范围内的终端数目", , PROJECT_TITLE_STRING)
            tb_lamp_num.Focus() '光标定位在景观灯个数文本框
            Exit Sub
        End If

        If IsNumeric(tb_lamp_num.Text) = False Then   '如果输入的终端个数不是数字
            MsgBox("终端数目必须是数字，请重新输入", , PROJECT_TITLE_STRING)
            tb_lamp_num.Focus()
            Exit Sub   '光标定位在终端个数的文本框
        End If

        If System.Convert.ToInt32(tb_lamp_num.Text) + System.Convert.ToInt32(tb_lamp_id.Text) > LAMP_ID_MAX Then    '终端的数目不可大于20000
            MsgBox("终端编号大于" & LAMP_ID_MAX & "，请重新输入", , PROJECT_TITLE_STRING)
            tb_lamp_num.Focus()
            Exit Sub
        End If

        If Trim(tb_start_pos_x.Text) = "" Or Trim(tb_start_pos_y.Text) = "" Then  '灯的起始位置
            MsgBox("请在地图上双击生成起点坐标", , PROJECT_TITLE_STRING)
            Exit Sub
        End If

        If Val(Trim(tb_lamp_num.Text)) > 1 Then
            If Trim(tb_end_pos_x.Text) = "" Or Trim(tb_end_pos_y.Text) = "" Then  '灯的终点位置
                MsgBox("请在地图上双击生成终点坐标", , PROJECT_TITLE_STRING)
                Exit Sub
            End If
        End If
        If Trim(cb_lampvision.Text = "") Then
            MsgBox("请选择节点版本号", , PROJECT_TITLE_STRING)
            cb_lampvision.Focus()
            Exit Sub
        End If

        If IsNumeric(Trim(cb_lampvision.Text)) = False Then
            MsgBox("请选择节点版本号", , PROJECT_TITLE_STRING)
            cb_lampvision.Focus()
            Exit Sub
        End If

        Dim lamp_add_num As Integer  '增加终端的起始编号
        Dim rs As ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim lamp_id_add_start As String  '新增终端编号
        Dim lamp_id_num As Integer  '终端编号
        Dim control_box_id As String  '电控箱编号
        Dim lamp_type_id As String  '灯的类型编号
        Dim pos_add_start, pos_add_end As System.Drawing.Point  '新增一串终端的起点和终点坐标
        Dim stp_x, stp_y As Double    'x 轴方向和y轴方向的步长
        Dim i As Integer
        Dim conn As New ADODB.Connection
        Dim addNum As Integer  '每次加灯的个数d
        Dim map_id As Integer  '地图的编号
        Dim lamp_id_beg As String  '记录起始的终端编号


        DBOperation.OpenConn(conn)

        i = 0

        msg = ""
        If Me.rb_addnext_num.Checked = True Then
            '选中按顺序编号
            addNum = 1
        Else
            If Me.rb_addnexttwo_num.Checked = True Then
                '选中奇偶编号
                addNum = 2
            Else
                '选中连加三个编号的特殊加灯方式
                addNum = 4
            End If

        End If

        lamp_add_num = Val(StrConv(Trim(tb_lamp_num.Text), VbStrConv.Narrow)) '景观灯个数

        '屏幕中的终端起点坐标转换城地图中的相对坐标
        pos_add_start.X = Val(tb_start_pos_x.Text - g_welcomewinobj.GroupBox1.Location.X - g_welcomewinobj.DesktopLocation.X - (Me.Width - Me.ClientSize.Width) - g_welcomewinobj.pb_map.Location.X - g_welcomewinobj.SplitContainer3.Panel1.Width)
        pos_add_start.Y = Val(tb_start_pos_y.Text - g_welcomewinobj.GroupBox1.Location.Y - g_welcomewinobj.DesktopLocation.Y - (Me.Height - Me.ClientSize.Height) - g_welcomewinobj.pb_map.Location.Y)
        '屏幕中的终端终点坐标转换成地图中的相对坐标


        '根据增加的终端个数，计算出每两盏终端之间距离
        If Val(Trim(tb_lamp_num.Text) > 1) Then
            pos_add_end.X = Val(tb_end_pos_x.Text - g_welcomewinobj.GroupBox1.Location.X - g_welcomewinobj.DesktopLocation.X - (Me.Width - Me.ClientSize.Width) - g_welcomewinobj.pb_map.Location.X - g_welcomewinobj.SplitContainer3.Panel1.Width)
            pos_add_end.Y = Val(tb_end_pos_y.Text - g_welcomewinobj.GroupBox1.Location.Y - g_welcomewinobj.DesktopLocation.Y - (Me.Height - Me.ClientSize.Height) - g_welcomewinobj.pb_map.Location.Y)

            stp_x = (pos_add_end.X - pos_add_start.X) / (lamp_add_num \ g_lampnum)  'X方向
            stp_y = (pos_add_end.Y - pos_add_start.Y) / (lamp_add_num \ g_lampnum)  'Y方向
        Else
            stp_x = 0
            stp_y = 0
        End If
        '检测当前的地图和所增加的区域是否匹配
        sql = "select * from map_list where area='" & Trim(cb_area_add.Text) & "' and id='" & g_choosemapid & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "add_street", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs.RecordCount <= 0 Then
            MsgBox("请匹配地图与所增加终端的区域", , PROJECT_TITLE_STRING)
            rs.Close()
            rs = Nothing
            conn.Close()
            conn = Nothing
            Exit Sub
        End If

        sql = "select * from box_lamptype_view where control_box_name='" & Trim(cb_box_add.Text) & "' and type_string='" & Trim(cb_lamp_type.Text) & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "add_street", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If

        If rs.RecordCount > 0 Then
            control_box_id = Trim(rs.Fields("control_box_id").Value)
            lamp_type_id = Trim(rs.Fields("type_id").Value)
            '灯的类型出错
            If lamp_type_id > 0 And lamp_type_id < TYPE_ID_MIN Then
                MsgBox("终端节点的类型编号预留" & TYPE_ID_MIN & "-31", , PROJECT_TITLE_STRING)
                GoTo finish
            End If
            '0类型的灯的编号<20000，11-31类型的灯的编号<2000
            If lamp_type_id > TYPE_ID_MIN And lamp_type_id <= 31 And Val(Trim(tb_lamp_id.Text)) + Val(Trim(tb_lamp_num.Text)) > 2000 Then
                MsgBox("终端节点的类型编号预留" & TYPE_ID_MIN & "-31, 终端节点的编号大于2000", , PROJECT_TITLE_STRING)
                tb_lamp_num.Focus()
                GoTo finish
            End If
        Else
            MsgBox("该主控箱还未添加终端类型", , PROJECT_TITLE_STRING)
            rs.Close()
            rs = Nothing
            conn.Close()
            conn = Nothing
            Exit Sub
        End If

        '查询出区域所对应的地图编号
        sql = "select id from map_list where area='" & Trim(cb_area_add.Text) & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs.RecordCount > 0 Then
            map_id = rs.Fields("id").Value
        Else
            MsgBox("该区域还未增加地图，请先增加该区域地图", , PROJECT_TITLE_STRING)
            rs.Close()
            rs = Nothing
            conn.Close()
            conn = Nothing
            Exit Sub
        End If


        lamp_id_add_start = Com_inf.Com_lamp_id_all(Trim(cb_box_add.Text), Trim(cb_lamp_type.Text), StrConv(Trim(tb_lamp_id.Text), VbStrConv.Narrow))
        lamp_id_beg = lamp_id_add_start
        Dim j As Integer = 0 '计数器，记录连着三个编号
        Dim lamp_id_string As String

        While i < lamp_add_num
            If i = 0 Then
                lamp_id_num = Val(Mid(lamp_id_add_start, 7, LAMP_ID_LEN))   '新终端编号为当前终端编号
                If addNum = 4 Then
                    j += 1
                End If
            Else
                If addNum = 1 Or addNum = 2 Then
                    lamp_id_num = Val(Mid(lamp_id_add_start, 7, LAMP_ID_LEN)) + addNum  '新终端编号为当前终端编号

                Else
                    If j < 3 Then
                        lamp_id_num = Val(Mid(lamp_id_add_start, 7, LAMP_ID_LEN)) + 1  '三个号码连号
                        j += 1
                    Else
                        lamp_id_num = Val(Mid(lamp_id_add_start, 7, LAMP_ID_LEN)) + addNum  '第四个号码加四
                        j = 1
                    End If
                End If
            End If

            lamp_id_string = lamp_id_num.ToString
            While lamp_id_string.Length < LAMP_ID_LEN
                lamp_id_string = "0" & lamp_id_string
            End While
            lamp_id_add_start = Mid(lamp_id_add_start, 1, 6) & lamp_id_string
            'If lamp_id_num < 10 Then
            '    lamp_id_add_start = Mid(lamp_id_add_start, 1, 6) & "00" & lamp_id_num.ToString
            'Else
            '    If lamp_id_num < 100 Then
            '        lamp_id_add_start = Mid(lamp_id_add_start, 1, 6) & "0" & lamp_id_num.ToString
            '    Else
            '        lamp_id_add_start = Mid(lamp_id_add_start, 1, 6) & lamp_id_num.ToString

            '    End If
            'End If
            sql = "select * from lamp_inf where lamp_id='" & lamp_id_add_start & "'"
            rs = DBOperation.SelectSQL(conn, sql, msg)
            If rs Is Nothing Then
                MsgBox(MSG_ERROR_STRING & "add_street", , PROJECT_TITLE_STRING)
                conn.Close()
                conn = Nothing
                Exit Sub
            End If
            If rs.RecordCount > 0 Then
                '   If MsgBox("终端的编号" & lamp_id_add_start & "已存在,是否修改其信息？", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.Yes Then
                If Me.rb_yadd.Checked = True Then
                    sql = "update lamp_inf set pos_x='" & Convert.ToInt16(pos_add_start.X + stp_x * (i \ 3)) & "' , pos_y='" & Convert.ToInt16(pos_add_start.Y + stp_y * (i \ 3) - (i Mod 3) * 8) & "' where lamp_id='" & lamp_id_add_start & "'"
                Else
                    sql = "update lamp_inf set pos_x='" & Convert.ToInt16(pos_add_start.X + stp_x * (i \ 3) - (i Mod 3) * 8) & "' , pos_y='" & Convert.ToInt16(pos_add_start.Y + stp_y * (i \ 3)) & "' where lamp_id='" & lamp_id_add_start & "'"

                End If


                DBOperation.ExecuteSQL(conn, sql, msg)

                '  End If


                i += 1

                Continue While
            End If
            '增加终端
            If Me.rb_yadd.Checked = True Then
                sql = "insert into lamp_inf(lamp_id,control_box_id,pos_x,pos_y,lamp_kind,state,result,area_street_id" & _
                          ",total_num,div_time_id,map_id,date,lamp_type_id,current_l,presure_l,power,presure_end,jiechuqi_id,lamp_pointinfor) values(" & _
                    "'" & lamp_id_add_start & "' , '" & control_box_id & "' ,'" & Convert.ToInt16(pos_add_start.X + stp_x * (i \ g_lampnum)) & "'" & _
                    ", '" & Convert.ToInt16(pos_add_start.Y + stp_y * (i \ g_lampnum) - (i Mod g_lampnum) * 8) & "' , '0','0','3','01001','0','4','" & map_id & "','" & Now() & "','" & lamp_type_id & "'" & _
                    ", '0' ,'0','0','0','" & Trim(cb_lampvision.Text) & "','" & Trim(LampPointInfor.Text) & "')"
            Else
                sql = "insert into lamp_inf(lamp_id,control_box_id,pos_x,pos_y,lamp_kind,state,result,area_street_id" & _
          ",total_num,div_time_id,map_id,date,lamp_type_id,current_l,presure_l,power,presure_end,jiechuqi_id,lamp_pointinfor) values(" & _
    "'" & lamp_id_add_start & "' , '" & control_box_id & "' ,'" & Convert.ToInt16(pos_add_start.X + stp_x * (i \ g_lampnum) - (i Mod g_lampnum) * 8) & "'" & _
    ", '" & Convert.ToInt16(pos_add_start.Y + stp_y * (i \ g_lampnum)) & "' , '0','0','3','01001','0','4','" & map_id & "','" & Now() & "','" & lamp_type_id & "'" & _
    ", '0' ,'0','0','0','" & Trim(cb_lampvision.Text) & "','" & Trim(LampPointInfor.Text) & "')"
            End If



            DBOperation.ExecuteSQL(conn, sql, msg)
            i += 1

        End While

        MsgBox("终端节点增加操作完成", , PROJECT_TITLE_STRING)

        '增加操作记录
        Com_inf.Insert_Operation("增加终端节点：起始编号" & lamp_id_beg & "，个数：" & lamp_add_num)

        Me.Lamp_streetTableAdapter.FillBy(Me.Street_add.lamp_street) '终端列表
        g_lampdrawtag = True  '同步lamp_map
        g_lampmap.Clear(Color.Empty)  '重绘灯
        g_lampdrawtag = False '同步lamp_map
        ' LoginForm1.Property_welcome_win_obj.Property_lamp_end = lamp_id_add_start  '将终端范围的终点扩展到目前增加的终端最终编号

finish:
        rs.Close()
        rs = Nothing
        conn.Close()
        conn = Nothing

        '刷新主控界面
        '  LoginForm1.Property_welcome_win_obj.control_center_inf()
    End Sub

    ''' <summary>
    ''' 点击“新增”按钮的处理函数
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_add_new_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_add_new.Click
        If g_welcomewinobj.tb_map_size.Value <> MAP_MID_SIZE Then
            g_welcomewinobj.tb_map_size.Value = MAP_MID_SIZE
        End If
        If rb_add_lampid.Checked = True Then
            add_street()  '增加灯的编号

            m_addtag = 2

            '刷新右侧列表
            g_welcomewinobj.SetControlBoxListDelegate(g_welcomewinobj.dgv_lamp_state_list, 0)

        End If

        If rb_add_lamptype.Checked = True Then
            add_lamp_type() '增加类型
        End If
    End Sub

    ''' <summary>
    ''' 增加类型函数
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub add_lamp_type()
        Dim rs_control_box As ADODB.Recordset
        Dim type_id As Integer
        Dim sql As String
        Dim msg As String
        Dim control_box_id As String  '灯的编号
        Dim conn As New ADODB.Connection
        DBOperation.OpenConn(conn)
        msg = ""

        '增加电控箱下面的景观灯类型
        sql = "select * from lamp_type where type_string='" & Trim(cb_lamp_type.Text) & "'"
        rs_control_box = DBOperation.SelectSQL(conn, sql, msg)
        If rs_control_box Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "add_lamp_type", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs_control_box.RecordCount > 0 Then
            type_id = rs_control_box.Fields("type_id").Value  '类型编号
        Else
            rs_control_box.Close()
            rs_control_box = Nothing
            conn.Close()
            conn = Nothing
            Exit Sub
        End If

        sql = "select * from control_box where control_box_name='" & Trim(cb_box_add.Text) & "'"
        rs_control_box = DBOperation.SelectSQL(conn, sql, msg)
        If rs_control_box Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "add_lamp_type", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs_control_box.RecordCount > 0 Then
            control_box_id = rs_control_box.Fields("control_box_id").Value  '电控箱编号
        Else
            rs_control_box.Close()
            rs_control_box = Nothing
            conn.Close()
            conn = Nothing
            Exit Sub
        End If


        sql = "select * from box_lamptype where lamp_type_id='" & type_id & "' and control_box_id='" & control_box_id & "'"
        rs_control_box = DBOperation.SelectSQL(conn, sql, msg)
        If rs_control_box Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "add_lamp_type", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs_control_box.RecordCount > 0 Then
            MsgBox("主控箱：" & Trim(cb_box_add.Text) & "下的" & Trim(cb_lamp_type.Text) & "信息已存在", , PROJECT_TITLE_STRING)
        Else
            rs_control_box.AddNew()
            rs_control_box.Fields("control_box_id").Value = control_box_id
            rs_control_box.Fields("lamp_type_id").Value = type_id
            rs_control_box.Update()
            MsgBox("类型名称添加成功", , PROJECT_TITLE_STRING)

        End If


        rs_control_box.Close()
        rs_control_box = Nothing
        conn.Close()
        conn = Nothing


    End Sub

    ''' <summary>
    ''' 窗体载入函数
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 增加终端_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: 这行代码将数据加载到表“KaiguanDataSet.kaiguan_list”中。您可以根据需要移动或移除它。
        '  Me.Kaiguan_listTableAdapter.Fill(Me.KaiguanDataSet.kaiguan_list)
        'TODO: 这行代码将数据加载到表“EditDataSet.huilu_inf”中。您可以根据需要移动或移除它。
        ' Me.Huilu_infTableAdapter.Fill(Me.EditDataSet.huilu_inf)
        'TODO: 这行代码将数据加载到表“Street_add.lamp_street”中。您可以根据需要移动或移除它。
        Me.Lamp_streetTableAdapter.FillBy(Me.Street_add.lamp_street) '终端列表
        '设置各个窗体的图标
        Me.Icon = New Icon("图片\favicon.ico", 32, 32)
        m_addtag = 2

        Com_inf.Select_city_name(cb_city_add)

        Com_inf.Select_city_name(cb_delete_city)

        ' Me.Get_lamp_id_inf()

        lb_lamp_id_start.Visible = False   '景观灯的编号前半部分不可见
        lb_delete_lamp_id_start_start.Visible = False
        lb_delete_lamp_id_end_start.Visible = False
        lb_lamp_type_id_add.Visible = False
        lb_lamp_type_id_del.Visible = False

        '底端工具栏中提示当前终端个数
        lamp_inf_text.Text = "当前共有：" & Me.dgv_old_lamp_list.RowCount & "个终端设备"

        '增加各个主控箱的名称，点击时显示其开关灯时间安排
        Dim m_controlboxobj As New control_box
        m_controlboxobj.set_controlbox_list(tv_box_inf_list) '主控箱信息列表
        tv_box_inf_list.ExpandAll()

        m_control_box_name = "" '初始化电控箱名称
        TabControl1.TabPages.Remove(Me.TabPage2)  '短信猫功能暂不打开
    End Sub
    Public Sub zhaoceobj_city_area_street(p1 As Object, p2 As Object, p3 As Object, control_box_name As String)
        'cb_delete_city.Text = p1
        'cb_delete_area.Text = p2
        'cb_delete_street.Text = p3
        'cb_delete_control_box_name.Text = control_box_name
        rb_box_name_control.Checked = True
    End Sub
    ''' <summary>
    ''' 窗体关闭时，将将add_tag变量清零
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 增加终端_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        m_addtag = 0  '将add_tag变量清零
        g_windowclose = 1
    End Sub

    ''' <summary>
    ''' 选择区域名称
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_box_add_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_box_add.DropDown
        Com_inf.Select_box_name_level(cb_city_add, cb_area_add, cb_street_all, cb_box_add)
    End Sub

    ''' <summary>
    ''' 查询时，按电控箱级别查询
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rb_box_name_control_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_box_name_control.Click
        If rb_box_name_control.Checked = True Then
            cb_delete_city.Enabled = True
            cb_delete_area.Enabled = True
            cb_delete_street.Enabled = True

            cb_delete_control_box_name.Enabled = True
            cb_delete_lamp_type.Enabled = False
            cb_delete_lamp_id_start.Enabled = False
            cb_delete_lamp_id_end.Enabled = False

        End If
    End Sub


    ''' <summary>
    '''  '按景观灯的编号区间段进行选择
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rb_lamp_id_control_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_lamp_id_control.Click

        If rb_lamp_id_control.Checked = True Then
            cb_delete_city.Enabled = True
            cb_delete_area.Enabled = True
            cb_delete_street.Enabled = True

            cb_delete_control_box_name.Enabled = True
            cb_delete_lamp_type.Enabled = True
            cb_delete_lamp_id_start.Enabled = True
            cb_delete_lamp_id_end.Enabled = True

        End If
    End Sub

    ''' <summary>
    ''' 选区域名称（删除操作）
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_delete_control_box_name_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_delete_control_box_name.DropDown
        Com_inf.Select_box_name_level(cb_delete_city, cb_delete_area, cb_delete_street, cb_delete_control_box_name)
    End Sub

    ''' <summary>
    ''' '选择灯的起始编号
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_delete_lamp_id_start_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_delete_lamp_id_start.DropDown
        Com_inf.Select_lamp_id_type(cb_delete_control_box_name, cb_delete_lamp_type, cb_delete_lamp_id_start, lb_delete_lamp_id_start_start)
    End Sub

    ''' <summary>
    ''' 起始编号改变，改变结束编号的值
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_delete_lamp_id_start_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_delete_lamp_id_start.SelectedIndexChanged
        lamp_id_end_add() '增加景观灯结束编号下拉框内容

    End Sub

    ''' <summary>
    '''  根据起始编号的值改变景观灯结束编号下拉框内容
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub lamp_id_end_add()
        Dim rs As ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim lamp_id As String
        Dim conn As New ADODB.Connection
        DBOperation.OpenConn(conn)
        cb_delete_lamp_id_end.Items.Clear()

        lamp_id = lb_delete_lamp_id_start_start.Text & Trim(cb_delete_lamp_id_start.Text)

        msg = ""
        sql = "select * from lamp_street where control_box_name='" & Trim(cb_delete_control_box_name.Text) & "' and type_string='" & Trim(cb_delete_lamp_type.Text) & "' and lamp_id>='" & lamp_id & "'"

        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "lamp_id_end_add", , PROJECT_TITLE_STRING)
            Exit Sub
        End If
        cb_delete_lamp_id_end.Items.Clear()
        If rs.RecordCount > 0 Then
            lb_delete_lamp_id_end_start.Text = Mid(Trim(rs.Fields("lamp_id").Value), 1, 6)
            While rs.EOF = False
                cb_delete_lamp_id_end.Items.Add(Mid(Trim(rs.Fields("lamp_id").Value), 7, LAMP_ID_LEN))
                rs.MoveNext()

            End While
        End If
        If cb_delete_lamp_id_end.Items.Count > 0 Then
            cb_delete_lamp_id_end.SelectedIndex = 0

        End If

        rs.Close()
        rs = Nothing
        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    ''' 删除按钮的响应函数，将删除内容显示在文本框中，确认后再删除
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_delete_lamp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_delete_lamp.Click
        Dim msg As String
        Dim i As Integer

        msg = ""
        i = 0

        While i < dgv_old_lamp_list.RowCount
            If dgv_old_lamp_list.Rows(i).Cells(0).Value = 1 Then  '如果终端被勾选

                rtb_delete_detail.AppendText(dgv_old_lamp_list.Rows(i).Cells("lamp_id_value").Value & "  ")
                rtb_delete_detail.AppendText(dgv_old_lamp_list.Rows(i).Cells("control_box_name").Value & "  ")
                rtb_delete_detail.AppendText(dgv_old_lamp_list.Rows(i).Cells("type_string").Value & "  ")
                rtb_delete_detail.AppendText(dgv_old_lamp_list.Rows(i).Cells("lamp_id_part").Value & " *" & vbCrLf)
            End If
            i += 1
        End While

        MsgBox("请确认是否删除文本框中显示的所有灯,如删除请点击 <<确定>> 按钮", , PROJECT_TITLE_STRING)

    End Sub

    ''' <summary>
    ''' '显示查询的灯的编号
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_choose_lamp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_choose_lamp.Click

        If rb_city_control.Checked = True Then
            '选择城市
            If cb_delete_city.Text = "" Then
                MsgBox("请选择城市名称", , PROJECT_TITLE_STRING)
                cb_delete_city.Focus()
                Exit Sub
            End If
            '更新列表内容
            Me.Lamp_streetTableAdapter.FillBy_city(Me.Street_add.lamp_street, Trim(cb_delete_city.Text)) '终端列表

            '底端工具栏中提示当前终端个数
            lamp_inf_text.Text = "城市:" & Trim(cb_delete_city.Text) & " " & "当前共有：" & Me.dgv_old_lamp_list.RowCount & "盏灯"

        End If
        If rb_area_control.Checked = True Then
            '区域选择
            If cb_delete_city.Text = "" Then
                MsgBox("请选择城市名称", , PROJECT_TITLE_STRING)
                cb_delete_city.Focus()
                Exit Sub
            End If
            If cb_delete_area.Text = "" Then
                MsgBox("请选择区域名称", , PROJECT_TITLE_STRING)
                cb_delete_area.Focus()
                Exit Sub
            End If
            '更新列表内容
            Me.Lamp_streetTableAdapter.FillBy_area(Me.Street_add.lamp_street, Trim(cb_delete_area.Text)) '终端列表

            '底端工具栏中提示当前终端个数
            lamp_inf_text.Text = "区域:" & Trim(cb_delete_area.Text) & " " & "当前共有：" & Me.dgv_old_lamp_list.RowCount & "盏灯"

        End If

        If rb_street_control.Checked = True Then
            '街道选择
            If cb_delete_city.Text = "" Then
                MsgBox("请选择城市名称", , PROJECT_TITLE_STRING)
                cb_delete_city.Focus()
                Exit Sub
            End If
            If cb_delete_area.Text = "" Then
                MsgBox("请选择区域名称", , PROJECT_TITLE_STRING)
                cb_delete_area.Focus()
                Exit Sub
            End If
            If cb_delete_street.Text = "" Then
                MsgBox("请选择街道名称", , PROJECT_TITLE_STRING)
                cb_delete_street.Focus()
                Exit Sub
            End If
            '更新列表内容
            Me.Lamp_streetTableAdapter.FillBy_street(Me.Street_add.lamp_street, Trim(cb_delete_street.Text)) '终端列表

            '底端工具栏中提示当前终端个数
            lamp_inf_text.Text = "街道:" & Trim(cb_delete_street.Text) & " " & "当前共有：" & Me.dgv_old_lamp_list.RowCount & "盏灯"

        End If
        If rb_box_name_control.Checked = True Then
            If cb_delete_city.Text = "" Then
                MsgBox("请选择城市名称", , PROJECT_TITLE_STRING)
                cb_delete_city.Focus()
                Exit Sub
            End If
            If cb_delete_area.Text = "" Then
                MsgBox("请选择区域名称", , PROJECT_TITLE_STRING)
                cb_delete_area.Focus()
                Exit Sub
            End If
            If cb_delete_street.Text = "" Then
                MsgBox("请选择街道名称", , PROJECT_TITLE_STRING)
                cb_delete_street.Focus()
                Exit Sub
            End If
            If cb_delete_control_box_name.Text = "" Then
                MsgBox("请选择主控箱名称", , PROJECT_TITLE_STRING)
                cb_delete_control_box_name.Focus()
                Exit Sub
            End If
            '更新列表内容
            Me.Lamp_streetTableAdapter.FillBy_box_name(Me.Street_add.lamp_street, Trim(cb_delete_control_box_name.Text)) '终端列表

            '底端工具栏中提示当前终端个数
            lamp_inf_text.Text = "主控箱:" & Trim(cb_delete_control_box_name.Text) & " " & "当前共有：" & Me.dgv_old_lamp_list.RowCount & "盏灯"

        End If

        If rb_lamp_type_control.Checked = True Then
            If cb_delete_city.Text = "" Then
                MsgBox("请选择城市名称", , PROJECT_TITLE_STRING)
                cb_delete_city.Focus()
                Exit Sub
            End If
            If cb_delete_area.Text = "" Then
                MsgBox("请选择区域名称", , PROJECT_TITLE_STRING)
                cb_delete_area.Focus()
                Exit Sub
            End If
            If cb_delete_street.Text = "" Then
                MsgBox("请选择街道名称", , PROJECT_TITLE_STRING)
                cb_delete_street.Focus()
                Exit Sub
            End If
            If cb_delete_control_box_name.Text = "" Then
                MsgBox("请选择主控箱", , PROJECT_TITLE_STRING)
                cb_delete_control_box_name.Focus()
                Exit Sub
            End If
            If cb_delete_lamp_type.Text = "" Then
                MsgBox("请选择类型名称", , PROJECT_TITLE_STRING)
                cb_delete_lamp_type.Focus()
                Exit Sub
            End If
            '更新列表内容
            Me.Lamp_streetTableAdapter.FillBy_type_string(Me.Street_add.lamp_street, Trim(cb_delete_control_box_name.Text), Trim(cb_delete_lamp_type.Text)) '终端列表

            '底端工具栏中提示当前终端个数
            lamp_inf_text.Text = "主控箱:" & Trim(cb_delete_control_box_name.Text) & " " & Trim(cb_delete_lamp_type.Text) & "当前共有：" & Me.dgv_old_lamp_list.RowCount & "盏"

        End If


        If rb_lamp_id_control.Checked = True Then
            If cb_delete_city.Text = "" Then
                MsgBox("请选择城市名称", , PROJECT_TITLE_STRING)
                cb_delete_city.Focus()
                Exit Sub
            End If
            If cb_delete_area.Text = "" Then
                MsgBox("请选择区域名称", , PROJECT_TITLE_STRING)
                cb_delete_area.Focus()
                Exit Sub
            End If
            If cb_delete_street.Text = "" Then
                MsgBox("请选择街道名称", , PROJECT_TITLE_STRING)
                cb_delete_street.Focus()
                Exit Sub
            End If
            If cb_delete_control_box_name.Text = "" Then
                MsgBox("请选择主控箱", , PROJECT_TITLE_STRING)
                cb_delete_control_box_name.Focus()
                Exit Sub
            End If
            If cb_delete_lamp_type.Text = "" Then
                MsgBox("请选择类型名称", , PROJECT_TITLE_STRING)
                cb_delete_lamp_type.Focus()
                Exit Sub
            End If
            If cb_delete_lamp_id_start.Text = "" Then
                MsgBox("请选择终端的编号", , PROJECT_TITLE_STRING)
                cb_delete_lamp_id_start.Focus()
                Exit Sub
            End If
            '更新列表内容
            If Trim(cb_delete_lamp_id_end.Text) <> "" Then
                Me.Lamp_streetTableAdapter.FillBy_lamp_id(Me.Street_add.lamp_street, Trim(cb_delete_control_box_name.Text), Trim(cb_delete_lamp_type.Text), Trim(lb_delete_lamp_id_start_start.Text) & Trim(cb_delete_lamp_id_start.Text), Trim(lb_delete_lamp_id_end_start.Text) & Trim(cb_delete_lamp_id_end.Text)) '终端列表

            Else
                Me.Lamp_streetTableAdapter.FillBy_lamp_single(Me.Street_add.lamp_street, Trim(lb_delete_lamp_id_start_start.Text) & Trim(cb_delete_lamp_id_start.Text)) '终端列表

            End If

            '底端工具栏中提示当前终端个数
            lamp_inf_text.Text = "主控箱:" & Trim(cb_delete_control_box_name.Text) & " " & Trim(cb_delete_lamp_type.Text) & " " & Trim(cb_delete_lamp_id_start.Text) & "至" & Trim(cb_delete_lamp_id_end.Text) & "共有：" & Me.dgv_old_lamp_list.RowCount & "盏"

        End If

        ''全部勾选
        'Dim row As Integer
        'row = 0
        'While row < old_lamp_list.Rows.Count
        '    old_lamp_list.Rows(row).Cells("check").Value = 1  '勾选该区域的终端
        '    row += 1
        'End While
        '根据选择的查询对象范围进行勾选
        Dim row As Integer
        row = 0
        While row < dgv_old_lamp_list.Rows.Count
            If rb_check_all.Checked = True Then   '选择所有三个灯头
                dgv_old_lamp_list.Rows(row).Cells("check").Value = 1  '勾选该区域的终端
            End If

            If rb_check_1.Checked = True Then    '选择删除灯杆上的1号灯头
                If Val(dgv_old_lamp_list.Rows(row).Cells("lamp_id_part").Value) Mod 3 = 1 Then
                    dgv_old_lamp_list.Rows(row).Cells("check").Value = 1
                Else
                    dgv_old_lamp_list.Rows(row).Cells("check").Value = 0
                End If

            End If

            If rb_check_2.Checked = True Then '选择删除灯杆上的2号灯头
                If Val(dgv_old_lamp_list.Rows(row).Cells("lamp_id_part").Value) Mod 3 = 2 Then
                    dgv_old_lamp_list.Rows(row).Cells("check").Value = 1
                Else
                    dgv_old_lamp_list.Rows(row).Cells("check").Value = 0
                End If
            End If

            If rb_check_3.Checked = True Then  '选择删除灯杆上的3号灯头
                If Val(dgv_old_lamp_list.Rows(row).Cells("lamp_id_part").Value) Mod 3 = 0 Then
                    dgv_old_lamp_list.Rows(row).Cells("check").Value = 1
                Else
                    dgv_old_lamp_list.Rows(row).Cells("check").Value = 0
                End If
            End If

            row += 1

        End While

    End Sub

    ''' <summary>
    '''  '选择全部
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rb_all_lamp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_city_control.Click

        If rb_city_control.Checked = True Then
            cb_delete_city.Enabled = True
            cb_delete_area.Enabled = False
            cb_delete_street.Enabled = False
            cb_delete_control_box_name.Enabled = False
            cb_delete_lamp_type.Enabled = False
            cb_delete_lamp_id_start.Enabled = False
            cb_delete_lamp_id_end.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' 确认删除
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_delete_confirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_delete_confirm.Click
        Dim i As Integer
        Dim sql As String
        Dim msg As String
        Dim lamp_id_string As String
        Dim conn As New ADODB.Connection
        Dim del_lampid As String = ""
        DBOperation.OpenConn(conn)
        msg = ""
        lamp_id_string = ""
        m_lampidline = rtb_delete_detail.Text.Split("*")
        If MsgBox("请确认是否删除文本框中显示的所有终端节点", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.Yes Then
            i = 0

            While i < m_lampidline.Length - 1
                If i = 0 Then
                    lamp_id_string = Trim(Mid(m_lampidline(i), 1, LAMP_ID_LEN + 6))

                Else
                    lamp_id_string = Trim(Mid(m_lampidline(i), 2, LAMP_ID_LEN + 6))
                End If

                sql = "delete from lamp_inf where lamp_id='" & lamp_id_string & "'"
                DBOperation.ExecuteSQL(conn, sql, msg)  '删除终端

                '如有时段设置，一并删除
                '1、经纬度
                sql = "delete from pianyi where lamp_id='" & lamp_id_string & "'"
                DBOperation.ExecuteSQL(conn, sql, msg)
                '2、特殊
                sql = "delete from road_level where lamp_id='" & lamp_id_string & "'"
                DBOperation.ExecuteSQL(conn, sql, msg)

                '3 节假日
                sql = "delete from Special_road_level where lamp_id='" & lamp_id_string & "'"
                DBOperation.ExecuteSQL(conn, sql, msg)


                del_lampid &= lamp_id_string & " "
                i += 1

            End While
            '增加操作记录
            If del_lampid.Length > 80 Then
                del_lampid = Mid(del_lampid, 1, 80) & "..."
            End If
            Com_inf.Insert_Operation("删除终端节点：" & del_lampid)

        End If

        Me.Lamp_streetTableAdapter.FillBy(Me.Street_add.lamp_street)  '载入删除后的终端列表
        rtb_delete_detail.Clear()
        '刷新右侧列表
        g_welcomewinobj.SetControlBoxListDelegate(g_welcomewinobj.dgv_lamp_state_list, 0)
        '底端工具栏中提示当前终端个数
        lamp_inf_text.Text = "当前共有：" & Me.dgv_old_lamp_list.RowCount & "盏灯"


        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    ''' 主控箱改变后其他值随之改变，增加
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_box_add_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_box_add.SelectedIndexChanged
        Com_inf.Select_type_name(cb_box_add, cb_lamp_type, lb_lamp_type_id_add)
    End Sub

    ''' <summary>
    ''' 主控箱的名称改变，其他值随之改变，删除
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub delete_control_box_name_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_delete_control_box_name.SelectedIndexChanged
        Com_inf.Select_type_name(cb_delete_control_box_name, cb_delete_lamp_type, lb_lamp_type_id_del)
        ' lamp_id_end_add() '增加景观灯结束编号下拉框内容
    End Sub

    Private Sub cb_delete_lamp_id_end_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_delete_lamp_id_end.DropDown
        lamp_id_end_add() '增加景观灯结束编号下拉框内容
    End Sub

    ''' <summary>
    ''' 增加类型名称
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_lamp_type_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_lamp_type.DropDown
        If rb_add_lampid.Checked = True Then
            Com_inf.Select_type_name(cb_box_add, cb_lamp_type, lb_lamp_type_id_add) '灯类型下拉框

        End If

        If rb_add_lamptype.Checked = True Then
            Select_all_lamptype(cb_lamp_type)
        End If

    End Sub

    Public Sub Select_all_lamptype(ByVal lamptype As System.Windows.Forms.ComboBox)

        Dim rs As ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim conn As New ADODB.Connection  '数据库连接
        DBOperation.OpenConn(conn)

        msg = ""
        sql = "select * from lamp_type order by type_id"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Select_all_lamptype" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        lamptype.Items.Clear()
        While rs.EOF = False
            lamptype.Items.Add(Trim(rs.Fields("type_string").Value))
            rs.MoveNext()
        End While
        If lamptype.Items.Count > 0 Then
            lamptype.SelectedIndex = 0

        End If
        rs.Close()
        rs = Nothing
        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    ''' 类型改变，其他值随之改变
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_lamp_type_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_lamp_type.SelectedIndexChanged
        Dim rs As New ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim i As Integer
        Dim id As String
        Dim conn As New ADODB.Connection
        Dim str As String '存放灯的编号
        DBOperation.OpenConn(conn)

        i = 0
        msg = ""
        sql = "select * from lamp_street where control_box_name='" & Trim(cb_box_add.Text) & "' and type_string='" & Trim(cb_lamp_type.Text) & "' order by lamp_id desc"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs.RecordCount > 0 Then

            id = (Val(Mid(Trim(rs.Fields("lamp_id").Value), 7, LAMP_ID_LEN)) + 1).ToString  '提示编号为目前终端编号中最大的加一
            ' id = (Val(Mid(Trim(rs.Fields("lamp_id").Value), 7, 3)) + 1).ToString  '提示编号为目前终端编号中最大的加一
        Else
            id = "1"
            'lamp_id.Text = "0001" '如果是第一盏终端，则使用001
        End If
        str = id

        While str.Length < LAMP_ID_LEN   '如果不足4位则用0补足
            str = "0" & str
        End While

        tb_lamp_id.Text = str

        rs.Close()
        rs = Nothing
        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    ''' 按灯的类型控制
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rb_lamp_type_control_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_lamp_type_control.Click
        If rb_lamp_type_control.Checked = True Then
            cb_delete_city.Enabled = True
            cb_delete_area.Enabled = True
            cb_delete_street.Enabled = True

            cb_delete_control_box_name.Enabled = True
            cb_delete_lamp_type.Enabled = True
            cb_delete_lamp_id_start.Enabled = False
            cb_delete_lamp_id_end.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' 增加类型名称
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_delete_lamp_type_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_delete_lamp_type.DropDown
        If cb_delete_control_box_name.Items.Count > 0 Then
            Com_inf.Select_type_name(cb_delete_control_box_name, cb_delete_lamp_type, lb_lamp_type_id_del) '灯类型下拉框
        End If
    End Sub

    ''' <summary>
    ''' 增加城市名称
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_city_add_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_city_add.DropDown
        Com_inf.Select_city_name(cb_city_add)
    End Sub

    ''' <summary>
    ''' 城市名称改变，其他值随之改变
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_city_add_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_city_add.SelectedIndexChanged
        Com_inf.Select_area_name(cb_city_add, cb_area_add)
        Com_inf.Select_street_name(cb_city_add, cb_area_add, cb_street_all)
        Com_inf.Select_box_name_level(cb_city_add, cb_area_add, cb_street_all, cb_box_add)
        Com_inf.Select_type_name(cb_box_add, cb_lamp_type, lb_lamp_type_id_add)
    End Sub

    ''' <summary>
    ''' 增加区域名称
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_area_add_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_area_add.DropDown
        Com_inf.Select_area_name(cb_city_add, cb_area_add)
    End Sub

    ''' <summary>
    ''' 区域名称改变，其他值随之改变
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_area_add_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_area_add.SelectedIndexChanged
        Com_inf.Select_street_name(cb_city_add, cb_area_add, cb_street_all)
        Com_inf.Select_box_name_level(cb_city_add, cb_area_add, cb_street_all, cb_box_add)
        Com_inf.Select_type_name(cb_box_add, cb_lamp_type, lb_lamp_type_id_add)
    End Sub

    ''' <summary>
    ''' 增加街道名称
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_street_all_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_street_all.DropDown
        Com_inf.Select_street_name(cb_city_add, cb_area_add, cb_street_all)
    End Sub

    ''' <summary>
    ''' 街道名称改变，其他值随之改变
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_street_all_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_street_all.SelectedIndexChanged
        Com_inf.Select_box_name_level(cb_city_add, cb_area_add, cb_street_all, cb_box_add)
        'Com_inf.Select_type_name(box_add, lamp_type, lamp_type_id_add)
    End Sub

    ''' <summary>
    ''' 以区域控制
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rb_area_control_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_area_control.Click
        If rb_area_control.Checked = True Then
            cb_delete_city.Enabled = True
            cb_delete_area.Enabled = True
            cb_delete_street.Enabled = False
            cb_delete_control_box_name.Enabled = False
            cb_delete_lamp_type.Enabled = False
            cb_delete_lamp_id_start.Enabled = False
            cb_delete_lamp_id_end.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' 以街道进行控制
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rb_street_control_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_street_control.Click
        If rb_street_control.Checked = True Then
            cb_delete_city.Enabled = True
            cb_delete_area.Enabled = True
            cb_delete_street.Enabled = True
            cb_delete_control_box_name.Enabled = False
            cb_delete_lamp_type.Enabled = False
            cb_delete_lamp_id_start.Enabled = False
            cb_delete_lamp_id_end.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' 增加城市名称,删除
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_delete_city_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_delete_city.DropDown
        Com_inf.Select_city_name(cb_delete_city)
    End Sub

    ''' <summary>
    ''' 城市名称改变，其他值随之改变
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_delete_city_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_delete_city.SelectedIndexChanged
        Com_inf.Select_area_name(cb_delete_city, cb_delete_area)
        'Com_inf.Select_street_name(delete_city, delete_area, delete_street)
        'Com_inf.Select_box_name_level(delete_city, delete_area, delete_street, delete_control_box_name)
        'Com_inf.Select_type_name(delete_control_box_name, delete_lamp_type, lamp_type_id_del)
        'lamp_id_end_add() '增加景观灯结束编号下拉框内容
    End Sub

    ''' <summary>
    ''' 增加区域名称，删除
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_delete_area_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_delete_area.DropDown
        Com_inf.Select_area_name(cb_delete_city, cb_delete_area)
    End Sub

    ''' <summary>
    ''' 区域名称改变，其他值随之改变
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_delete_area_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_delete_area.SelectedIndexChanged
        Com_inf.Select_street_name(cb_delete_city, cb_delete_area, cb_delete_street)
        'Com_inf.Select_box_name_level(delete_city, delete_area, delete_street, delete_control_box_name)
        'Com_inf.Select_type_name(delete_control_box_name, delete_lamp_type, lamp_type_id_del)
        'lamp_id_end_add() '增加景观灯结束编号下拉框内容
    End Sub

    ''' <summary>
    ''' 增加街道名称
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_delete_street_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_delete_street.DropDown
        Com_inf.Select_street_name(cb_delete_city, cb_delete_area, cb_delete_street)
    End Sub

    ''' <summary>
    ''' 街道名称改变，其他值随之改变
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_delete_street_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_delete_street.SelectedIndexChanged
        Com_inf.Select_box_name_level(cb_delete_city, cb_delete_area, cb_delete_street, cb_delete_control_box_name)
        'Com_inf.Select_type_name(delete_control_box_name, delete_lamp_type, lamp_type_id_del)
        'lamp_id_end_add() '增加景观灯结束编号下拉框内容
    End Sub

    ''' <summary>
    ''' 灯的类型改变，其他值随之改变
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_delete_lamp_type_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_delete_lamp_type.SelectedIndexChanged
        ' lamp_id_end_add() '增加景观灯结束编号下拉框内容
        Com_inf.Select_lamp_id_type(cb_delete_control_box_name, cb_delete_lamp_type, cb_delete_lamp_id_start, lb_delete_lamp_id_start_start)

    End Sub

    Private Sub box_inf_list_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tv_box_inf_list.DoubleClick
        If tv_box_inf_list.SelectedNode Is Nothing Then
            Exit Sub
        End If
        If tv_box_inf_list.SelectedNode.Level = 3 Then
            Dim rs As New ADODB.Recordset
            Dim conn As New ADODB.Connection
            Dim msg As String
            Dim sql As String
            Dim KList() As System.Windows.Forms.RadioButton = {rb_k1_list, rb_k2_list, rb_k3_list, rb_k4_list, rb_k5_list, rb_k6_list}

            Dim i As Integer = 1  '回路的起始编号
            Dim huilunum As Integer = 0  '记录该主控箱下的总回路数
            m_control_box_name = tv_box_inf_list.SelectedNode.Text
            Me.GroupBoxhuilu.Text = m_control_box_name & "回路编号"


            msg = ""
            sql = "select huilu_num, control_box_id,control_box_name from control_box where control_box_name='" & m_control_box_name & "'"
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
                    m_control_box_id = ""
                Else
                    m_control_box_id = Trim(rs.Fields("control_box_id").Value)
                End If
                If rs.Fields("control_box_name").Value Is System.DBNull.Value Then
                    m_control_box_name = Trim(rs.Fields("control_box_name").Value)
                Else
                    m_control_box_name = Trim(rs.Fields("control_box_name").Value)

                End If

            End If
            '根据电控箱编号找到该主控箱下面可以使用的接触器编号
            sql = "select lamp_id from lamp_inf where control_box_id='" & m_control_box_id & "' and lamp_type_id=31"
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
                    Else
                        KList(i).Enabled = False
                    End If
                    rs.MoveNext()
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
                    clb_huilu_idlist.Items.Add(i.ToString & " 号回路" & "(" & m_6huilu(i - 1) & ")")
                    i += 1
                End While
            Else
                If huilunum = 12 Then
                    While i <= huilunum
                        clb_huilu_idlist.Items.Add(i.ToString & " 号回路" & "(" & m_12huilu(i - 1) & ")")
                        i += 1
                    End While
                Else
                    If huilunum = 24 Then
                        While i <= huilunum
                            clb_huilu_idlist.Items.Add(i.ToString & " 号回路" & "(" & m_24huilu(i - 1) & ")")
                            i += 1
                        End While
                    Else
                        While i <= huilunum
                            clb_huilu_idlist.Items.Add(i.ToString & " 号回路" & "(" & m_36huilu(i - 1) & ")")
                            i += 1
                        End While
                    End If
                End If

            End If


            '默认选择第一个接触器下的回路
            select_huiluid(1)


            '根据选择的主控箱名称选择模拟量配置列表
            Me.Huilu_infTableAdapter.FillBy_box(Me.EditDataSet.huilu_inf, Trim(m_control_box_name))

            '根据选择的主控箱名称选择开关量配置列表
            Me.Kaiguan_listTableAdapter.FillBy_box(Me.KaiguanDataSet.kaiguan_list, m_control_box_name)

            '模拟量列表名称
            GroupBoxmoniliang.Text = "编辑主控箱：" & m_control_box_name & "模拟量"
            GroupBoxkaiguanliang.Text = "编辑主控箱：" & m_control_box_name & "开关量"
            If rs.State = 1 Then
                rs.Close()
                rs = Nothing
            End If
            conn.Close()
            conn = Nothing
        End If

    End Sub
    ''' <summary>
    ''' 将回路与交流接触器编号及变比相匹配
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_set_huilu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_set_huilu.Click
        If m_control_box_name = "" Then
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

            sql = "select * from huilu_inf where control_box_name='" & m_control_box_name & "' and huilu_id='" & huiluid & "'"
            rs = DBOperation.SelectSQL(conn, sql, msg)
            If rs Is Nothing Then
                MsgBox(MSG_ERROR_STRING & "set_huilu_Click", , PROJECT_TITLE_STRING)

                conn.Close()
                conn = Nothing
                Exit Sub
            End If

            If rs.RecordCount > 0 Then
                If MsgBox("主控箱" & m_control_box_name & "下的回路: " & huiluid & "已配置，是否重新配置", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.Yes Then
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
                rs.Fields("control_box_name").Value = m_control_box_name
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
        sql = "insert into kaiguan_list(kaiguan_name,kaiguan_tag,alarm,control_box_name) values('" & kaiguan_name & "', 0,0,'" & m_control_box_name & "')"
        DBOperation.ExecuteSQL(conn, sql, msg)


        MsgBox("回路配置成功", , PROJECT_TITLE_STRING)

        '增加操作记录
        Com_inf.Insert_Operation("配置" & m_control_box_name & "接触器" & jiechuqi_id_string & "下回路: " & huilu_string)

        '刷新删除的回路列表
        Me.Huilu_infTableAdapter.FillBy_box(Me.EditDataSet.huilu_inf, m_control_box_name)

        '根据选择的主控箱名称选择开关量配置列表
        Me.Kaiguan_listTableAdapter.FillBy_box(Me.KaiguanDataSet.kaiguan_list, m_control_box_name)

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
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
            Com_inf.Insert_Operation("删除" & m_control_box_name & "回路：" & del_string & "的模拟量设置")

            '
            '刷新删除的回路列表
            Me.Huilu_infTableAdapter.FillBy_box(Me.EditDataSet.huilu_inf, m_control_box_name)


            conn.Close()
            conn = Nothing
        End If

    End Sub

    ''' <summary>
    ''' 根据解除器的编号，选择其所控制的回路编号
    ''' </summary>
    ''' <param name="jiechuqi_id"></param>
    ''' <remarks></remarks>
    Private Sub select_huiluid(ByVal jiechuqi_id As Integer)
        '选择第个接触器编号
        If Me.m_control_box_name = "" Then
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

        sql = "select huilu_id from huilu_inf where control_box_name='" & m_control_box_name & "' and jiechuqi_id='" & jiechuqi_id & "' order by id "
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

                    If Mid(dgv_huilu_list.Rows(i).Cells("jiechuqi").Value, 1, 1) <> "K" Or IsNumeric(jiechuqi_id) Then
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
                    Com_inf.Insert_Operation("编辑" & m_control_box_name & "模拟量")

                End If
                i += 1
            End While
            MsgBox("保存成功", , PROJECT_TITLE_STRING)

            'TODO: 这行代码将数据加载到表“EditDataSet.huilu_inf”中。您可以根据需要移动或移除它。
            Me.Huilu_infTableAdapter.FillBy_box(Me.EditDataSet.huilu_inf, m_control_box_name)
finish:
            conn.Close()
            conn = Nothing
        End If

    End Sub

    ''' <summary>
    ''' 保存开关量的编辑
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_save_kaiguan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_save_kaiguan.Click
        If Me.m_control_box_name = "" Then
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
                        sql = "insert into kaiguan_list(kaiguan_name,kaiguan_tag,alarm,control_box_name) values('" & kaiguanname & "', '" & kaiguantag & "' ,'" & alarm & "','" & m_control_box_name & "')"
                        DBOperation.ExecuteSQL(conn, sql, msg)

                    Else
                        '主控箱不为空表示编辑
                        sql = "update kaiguan_list set kaiguan_name='" & kaiguanname & "', kaiguan_tag='" & kaiguantag & "' , alarm='" & alarm & "', control_box_name='" & m_control_box_name & "' where id ='" & dgv_kaiguanliang_list.Rows(i).Cells("kgid").Value & "'"
                        DBOperation.ExecuteSQL(conn, sql, msg)

                    End If


                    '检查已设的报警，看是否有该开关量名称的
                    sql = "select * from alarm_typelist where control_box_name='" & m_control_box_name & "' and alarm_type like'" & kaiguanname & "%'"
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
            Com_inf.Insert_Operation("编辑" & m_control_box_name & "开关量: " & add_string)

            Me.Kaiguan_listTableAdapter.FillBy_box(Me.KaiguanDataSet.kaiguan_list, m_control_box_name)
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
        If Me.m_control_box_name = "" Then
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
            Com_inf.Insert_Operation("删除" & m_control_box_name & "开关量:" & del_string)

            Me.Kaiguan_listTableAdapter.FillBy_box(Me.KaiguanDataSet.kaiguan_list, m_control_box_name)
            chb_checkall.Checked = False
finish:
            conn.Close()
            conn = Nothing

        End If

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
        Me.Kaiguan_listTableAdapter.FillBy_box(Me.KaiguanDataSet.kaiguan_list, m_control_box_name)

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
    ''' 选择主控箱名称
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_controlbox_name_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_controlbox_name.DropDown
        Com_inf.Select_box_name(cb_controlbox_name)
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

    Private Sub rb_add_lamptype_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_add_lamptype.Click
        If rb_add_lamptype.Checked = True Then
            tb_lamp_id.Enabled = False
            tb_lamp_num.Enabled = False
            gb_drawpos.Enabled = False
            gb_draw_method.Enabled = False


        End If
    End Sub

    Private Sub rb_add_lampid_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_add_lampid.Click
        If rb_add_lampid.Checked = True Then
            tb_lamp_id.Enabled = True
            tb_lamp_num.Enabled = True
            gb_drawpos.Enabled = True
            gb_draw_method.Enabled = True
        End If
    End Sub

    Private Sub cb_lampvision_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_lampvision.MouseHover
        Me.ToolTip_lamp.SetToolTip(cb_lampvision, "版本1为1字节;版本2为2字节;版本3为6字节")
    End Sub

    Private Sub lamp_pointEdit(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv_old_lamp_list.CellEndEdit
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String
        Dim lamp_pointinfor As String = ""
        msg = ""
        Dim i As Integer = 0
        If Me.dgv_old_lamp_list.EndEdit = True Then
            i = Me.dgv_old_lamp_list.CurrentRow.Index
            If IsDBNull(Me.dgv_old_lamp_list.Rows(i).Cells("lamp_id_value").Value) = False Then
                If IsDBNull(Me.dgv_old_lamp_list.Rows(i).Cells("lamp_pointinfor").Value) = False Then
                    lamp_pointinfor = Me.dgv_old_lamp_list.Rows(i).Cells("lamp_pointinfor").Value
                    If Trim(lamp_pointinfor) <> "" Then
                        sql = "UPDATE lamp_inf SET lamp_pointinfor='" & lamp_pointinfor & "' WHERE lamp_id=" & Me.dgv_old_lamp_list.Rows(i).Cells("lamp_id_value").Value & ""
                    Else
                        sql = "UPDATE lamp_inf SET lamp_pointinfor=null WHERE lamp_id=" & Me.dgv_old_lamp_list.Rows(i).Cells("lamp_id_value").Value & ""
                    End If
                Else
                    sql = "UPDATE lamp_inf SET lamp_pointinfor=null WHERE lamp_id=" & Me.dgv_old_lamp_list.Rows(i).Cells("lamp_id_value").Value & ""
                End If
                DBOperation.OpenConn(conn)
                DBOperation.ExecuteSQL(conn, sql, msg)
                conn.Close()
                conn = Nothing
            End If
        End If
    End Sub
End Class