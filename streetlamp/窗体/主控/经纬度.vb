Public Class 经纬度
    Private m_mothList() As Integer = {31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31}  '非闰年
    Private m_leapList() As Integer = {31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31}  '闰年
    Private m_id As Integer  '每个时段模式中的控制时段编号
    Private m_hm_id As Integer  '节日时段模式中的控制时候编号
    Private m_control_box_id As String '主控箱编号
    Private m_xlApp As Microsoft.Office.Interop.Excel.Application
    Private m_xlBook As Microsoft.Office.Interop.Excel.Workbook
    Private m_xlSheet As Microsoft.Office.Interop.Excel.Worksheet
    Private m_row As Integer
    Private m_week() As Integer = {1, 1, 1, 1, 1, 1, 1}  '一周7天，0表示不按该模式控制，1表示按该模式控制

    Private check As Boolean = False '设置标志，防止死循环
    Private checklist As New ArrayList
    Private m_excel_type As Integer = 0 '0表示导出的是日出日落表，1表示导出的是节点的月报表
    Private m_selectboxname As String  '选中的主控箱名称

    ''' <summary>
    ''' 根据经纬度及时间查询出新的日出日落时刻表
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_getexcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_getexcel.Click
        Dim bLeap As Boolean '是否为闰年
        Dim suntimeobj As New SunTime
        Dim month() As Integer  '每月的天数
        Dim month_num As Integer '月份
        Dim row As Integer  '列表的行和列
        Dim risetime, downtime As DateTime  '日出及日落时间
        Dim i As Integer
        Dim sql As String
        Dim msg As String
        Dim conn As New ADODB.Connection

        DBOperation.OpenConn(conn)
        msg = ""
        If cb_year.Text = "" Then
            MsgBox("请输入查询年份")
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        bLeap = suntimeobj.IsLeapYear(Int(cb_year.Text))
        If bLeap = True Then  '闰年
            month = m_leapList
        Else   '非闰年
            month = m_mothList
        End If
        month_num = 0 '初始化从1月开始
        row = 1
        i = 0
        dgv_suninflist.Rows.Clear()
        If Me.chb_loadrecord.Checked = True Then
            sql = "delete from suntime"
            DBOperation.ExecuteSQL(conn, sql, msg)
        End If
        While row <= 31
            dgv_suninflist.Rows.Add()
            dgv_suninflist.Rows(i).Cells(0).Value = row.ToString & "日日出时间"
            i += 1
            dgv_suninflist.Rows.Add()
            dgv_suninflist.Rows(i).Cells(0).Value = row.ToString & "日日落时间"
            i += 1
            row += 1
        End While

        While month_num < 12
            row = 0
            i = 0
            While row < month(month_num)

                risetime = suntimeobj.GetSunrise(System.Convert.ToDouble(Trim(tb_weidu.Text)), System.Convert.ToDouble(Trim(tb_jingdu.Text)), Val(cb_year.Text), month_num + 1, row + 1)
                dgv_suninflist.Rows(i).Cells(month_num + 1).Value = risetime.TimeOfDay
                i += 1
                downtime = suntimeobj.GetSunset(System.Convert.ToDouble(Trim(tb_weidu.Text)), System.Convert.ToDouble(Trim(tb_jingdu.Text)), Val(cb_year.Text), month_num + 1, row + 1)
                dgv_suninflist.Rows(i).Cells(month_num + 1).Value = downtime.TimeOfDay
                i += 1
                row += 1

                '如果勾选了保存到数据库，则将当前查询的日出日落时间记录到数据库中，作为时控的基本时间
                If Me.chb_loadrecord.Checked = True Then

                    sql = "insert into suntime(time,mod) values ('" & risetime & "', '关')"  '记录关灯时间
                    DBOperation.ExecuteSQL(conn, sql, msg)

                    sql = "insert into suntime(time,mod) values ('" & downtime & "', '开')"  '记录开灯时间
                    DBOperation.ExecuteSQL(conn, sql, msg)


                End If
            End While
            month_num += 1
        End While

        If Me.chb_loadrecord.Checked = True Then
            '将修改经纬度的操作记录记录到数据库中
            Com_inf.Insert_Operation("修改日出日落表，时间：" & cb_year.Text & ", 经度：" & tb_jingdu.Text & " ,纬度：" & tb_weidu.Text)

        End If

        'If Me.loadRecord.Checked = True Then
        '    '如果更改了当前的日出日落时间，则重新下放时控命令
        '    Dim control_boxobj As New control_box
        '    control_boxobj.ModXiaFang()

        'End If

        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    ''' 选择了就将当前的记录保存到数据库中，不选择则不保存
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chb_loadrecord_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chb_loadrecord.Click
        If chb_loadrecord.Checked = True Then
            MsgBox("请注意：勾选此项后,点击时刻查询所的数据将被保存到数据库中作为日出日落基础时刻!!", , PROJECT_TITLE_STRING)

        End If
    End Sub

    ''' <summary>
    ''' 载入当前记录的日出日落时刻表
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_get_timetable_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_get_timetable.Click
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String
        Dim row As Integer
        Dim i As Integer
        Dim month_num As Integer
        Dim daytime As DateTime '记录时间

        msg = ""
        sql = "select * from suntime order by id"
        DBOperation.OpenConn(conn)
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "get_timetable_Click", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If

        If rs.RecordCount <= 0 Then
            '没有数据，则提示用户先查询当前年度的日出日落时间
            MsgBox("还未记录本年度的日出日落时间，请输入经纬度后记录本年度的时刻表后再查询", , PROJECT_TITLE_STRING)
            rs.Close()
            rs = Nothing
            conn.Close()
            conn = Nothing

            Exit Sub
        End If

        '查找到数据，但并不是当年的数据，则提示用户更新经纬度时刻表
        If System.Convert.ToDateTime(rs.Fields("time").Value).Year <> Now.Year Then
            '没有当年的数据，则提示用户先查询当前年度的日出日落时间
            MsgBox("还未记录本年度的日出日落时间，请输入经纬度后记录本年度的时刻表后再查询", , PROJECT_TITLE_STRING)
            rs.Close()
            rs = Nothing
            conn.Close()
            conn = Nothing

            Exit Sub
        End If

        '查询到当年的数据
        row = 1
        i = 0
        dgv_suninflist.Rows.Clear()
        While row <= 31
            dgv_suninflist.Rows.Add()
            dgv_suninflist.Rows(i).Cells(0).Value = row.ToString & "日日出时间"
            i += 1
            dgv_suninflist.Rows.Add()
            dgv_suninflist.Rows(i).Cells(0).Value = row.ToString & "日日落时间"
            i += 1
            row += 1
        End While
        month_num = 1
        While rs.EOF = False

            row = 0
            daytime = System.Convert.ToDateTime(rs.Fields("time").Value)
            While daytime.Month = month_num
                dgv_suninflist.Rows(row).Cells(month_num).Value = daytime.TimeOfDay
                row += 1
                rs.MoveNext()
                If rs.EOF = False Then
                    daytime = System.Convert.ToDateTime(rs.Fields("time").Value)
                Else
                    Exit While
                End If
            End While

            month_num += 1
        End While
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing

    End Sub

    ''' <summary>
    ''' 点击日出日落时间后，周设置的表中将以数据库中保存的日出日落时间为基准时间
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub setsuntime_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String

        msg = ""
        sql = "select * from suntime order by id"
        DBOperation.OpenConn(conn)
        rs = DBOperation.SelectSQL(conn, sql, msg)

    End Sub

    ''' <summary>
    ''' 增加开灯的偏移量,使开灯偏移量可输入
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chb_open_add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chb_open_add.Click
        If chb_open_add.Checked = True Then
            tb_open_pianyi.Enabled = True
        Else
            tb_open_pianyi.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' 增加关灯的偏移量，使关灯偏移量可输入
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chb_close_add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chb_close_add.Click
        If chb_close_add.Checked = True Then
            tb_close_pianyi.Enabled = True
        Else
            tb_close_pianyi.Enabled = False
        End If
    End Sub

    '''' <summary>
    '''' 选择城市名称
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    '''' <remarks></remarks>
    'Private Sub cb_city_name_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Com_inf.Select_city_name(cb_city_name)
    'End Sub

    '''' <summary>
    '''' 城市名称改变，下面层次的数据跟着改变
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    '''' <remarks></remarks>
    'Private Sub cb_city_name_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Com_inf.Select_area_name(cb_city_name, cb_area_name)

    'End Sub

    '''' <summary>
    '''' 选择区域名称
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    '''' <remarks></remarks>
    'Private Sub cb_area_name_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Com_inf.Select_area_name(cb_city_name, cb_area_name)
    'End Sub

    '''' <summary>
    '''' 区域名称改变，下面层次的数据跟着改变
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    '''' <remarks></remarks>
    'Private Sub cb_area_name_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Com_inf.Select_street_name(cb_city_name, cb_area_name, cb_street_name)

    'End Sub

    '''' <summary>
    '''' 选择街道名称
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    '''' <remarks></remarks>
    'Private Sub cb_street_name_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Com_inf.Select_street_name(cb_city_name, cb_area_name, cb_street_name)

    'End Sub

    '''' <summary>
    '''' 街道名称改变，下面层次的数据跟着改变
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    '''' <remarks></remarks>
    'Private Sub cb_street_name_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Com_inf.Select_box_name_level(cb_city_name, cb_area_name, cb_street_name, cb_control_box_name)

    'End Sub

    '''' <summary>
    '''' 选择主控箱名称
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    '''' <remarks></remarks>
    'Private Sub cb_control_box_name_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Com_inf.Select_box_name_level(cb_city_name, cb_area_name, cb_street_name, cb_control_box_name)

    'End Sub


    '''' <summary>
    '''' 获取所有的电控箱的名称
    '''' </summary>
    '''' <remarks></remarks>
    'Private Sub get_controlbox()
    '    Dim rs As New ADODB.Recordset
    '    Dim conn As New ADODB.Connection
    '    Dim msg As String
    '    Dim sql As String

    '    msg = ""
    '    sql = "select control_box_name from control_box order by id"
    '    DBOperation.OpenConn(conn)
    '    rs = DBOperation.SelectSQL(conn, sql, msg)
    '    If rs Is Nothing Then
    '        MsgBox(MSG_ERROR_STRING & "get_controlbox", , PROJECT_TITLE_STRING)
    '        conn.Close()
    '        conn = Nothing
    '        Exit Sub
    '    End If
    '    tv_control_box_list.Items.Clear()  '组合分组设置
    '    clb_divtime_box_list.Items.Clear()  '特殊时段模式中
    '    clb_holidaydivtime_box_list.Items.Clear() '节日时段模式中
    '    While rs.EOF = False
    '        tv_control_box_list.Items.Add(Trim(rs.Fields("control_box_name").Value))
    '        clb_divtime_box_list.Items.Add(Trim(rs.Fields("control_box_name").Value))
    '        clb_holidaydivtime_box_list.Items.Add(Trim(rs.Fields("control_box_name").Value))

    '        rs.MoveNext()
    '    End While

    '    If rs.State = 1 Then
    '        rs.Close()
    '        rs = Nothing
    '    End If
    '    conn.Close()
    '    conn = Nothing
    'End Sub

    Private Sub 经纬度_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '设置各个窗体的图标
        Me.Icon = New Icon("图片\favicon.ico", 32, 32)
        'get_controlbox()
        m_id = 1 '初始化为1
        ' dtp_start_holidaytime.CustomFormat = "yyyy-MM-dd HH:mm:ss  "  '查询条件中开始日期的格式

        Dim str1 As String
        Dim i As Integer = 0
        str1 = ""
        While i < g_modgroup.Length
            If i = g_modgroup.Length - 1 Then
                str1 &= g_modgroup(i)
            Else
                str1 &= g_modgroup(i) & " + "
            End If
            If g_modgroup(i) = "经纬度控制模式" Then
                chb_jingweicontrol.Checked = True
            End If
            If g_modgroup(i) = "特殊控制模式" Then
                chb_specialcontrol.Checked = True
            End If
            If g_modgroup(i) = "节假日控制模式" Then
                chb_holidaycontrol.Checked = True
            End If
            i += 1
        End While
        sysconfigstring.Text = "目前系统的控制级别：" & g_sysjibiecontrol

        ' cb_jibiestring.Text = g_sysjibiecontrol

        '增加各个主控箱的名称，点击时显示其开关灯时间安排
        Dim m_controlboxobj As New control_box
        'm_controlboxobj.set_controlbox_list(tv_box_inf_list) '主控箱信息列表
        'm_controlboxobj.set_controlbox_list(tv_all_controlbox) '主控箱信息列表
        'm_controlboxobj.set_controlbox_list(tv_divtime_controlbox) '自动控制模式中的主控箱信息列表
        'm_controlboxobj.set_controlbox_list(tv_banye_controlbox)  '半夜灯控制
        'm_controlboxobj.set_controlbox_list(tv_holidaydivtime_box) '节假日控制

        '经纬度
        tb_jingdu.Text = Com_inf.get_jingduvalue()
        tb_weidu.Text = Com_inf.get_weiduvalue
    End Sub

    '''' <summary>
    '''' 设置组合控制
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    '''' <remarks></remarks>
    'Private Sub chb_setweek_group_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If chb_setweek_group.Checked = True Then
    '        tv_all_controlbox.Enabled = True
    '        'rb_selectall.Checked = False
    '        'rb_selectall.Enabled = True
    '        'rb_clearall.Checked = True
    '        'rb_clearall.Enabled = True
    '    Else
    '        tv_all_controlbox.Enabled = False
    '        'rb_selectall.Checked = False
    '        'rb_clearall.Checked = True
    '        'rb_selectall.Enabled = False
    '        'rb_clearall.Enabled = False
    '    End If
    'End Sub


    'private TreeNode FindNode( TreeNode tnParent, string strValue )
    '{

    '    if( tnParent == null ) return null;

    '    if( tnParent.Text == strValue ) return tnParent;
    '    TreeNode tnRet = null;

    '    foreach( TreeNode tn in tnParent.Nodes )
    '    {
    '        tnRet = FindNode( tn, strValue );

    '        if( tnRet != null ) break;
    '    }
    '    return tnRet;
    '}



    ''' <summary>
    ''' 设置选择的节点的（如果选择的组合设置，则其他电控箱下的相同节点）的偏移量
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_set_timevalue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_set_timevalue.Click
        checklist.Clear() '清除所有选中的项目
        Dim tnRet As New TreeNode
        For Each treenode As TreeNode In tv_all_controlbox.Nodes
            Com_inf.FindNode(treenode, checklist)
        Next
        If checklist.Count = 0 Then
            MsgBox("请选择需设置的主控箱名称", , PROJECT_TITLE_STRING)
            Exit Sub
        End If
        If chb_quanye_K1.Checked = False And chb_quanye_K2.Checked = False And chb_quanye_K3.Checked = False And chb_quanye_K4.Checked = False _
        And chb_quanye_K5.Checked = False And chb_quanye_K6.Checked = False Then
            MsgBox("请选择需设置的接触器", , PROJECT_TITLE_STRING)
            Exit Sub
        End If

        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String
        Dim lamp_id() As String  '节点编号
        Dim open_pianyivalue, close_pianyivalue As Integer '开关灯偏移量
        Dim rs As New ADODB.Recordset
        Dim todayopentime, todayclosetime As DateTime  '今天的开灯与关灯时间
        Dim nowtime As DateTime '当前的时间
        Dim rs_lamp As New ADODB.Recordset
        Dim controllampobj As New control_lamp
        Dim id_string As String = "" '记录接触器号

        If chb_quanye_K1.Checked = True Then
            id_string = "1"
        End If
        If chb_quanye_K2.Checked = True Then
            id_string &= " 2"
        End If
        If chb_quanye_K3.Checked = True Then
            id_string &= " 3"
        End If
        If chb_quanye_K4.Checked = True Then
            id_string &= " 4"
        End If
        If chb_quanye_K5.Checked = True Then
            id_string &= " 5"
        End If
        If chb_quanye_K6.Checked = True Then
            id_string &= " 6"
        End If

        id_string = Trim(id_string)

        msg = ""
        sql = ""
        DBOperation.OpenConn(conn)
        nowtime = System.Convert.ToDateTime(Now.Year.ToString & "-" & Now.Month.ToString & "-" & Now.Day.ToString & " 0:0:0")
        todayopentime = nowtime
        todayclosetime = nowtime

        '查询日出日落时间表，记录当天的开关灯时间
        sql = "select * from suntime where time>= '" & nowtime & "' and time<'" & nowtime.AddDays(1) & "' order by id"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "set_timevalue_Click", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        While rs.EOF = False
            If Trim(rs.Fields("mod").Value) = "开" Then
                todayopentime = System.Convert.ToDateTime(rs.Fields("time").Value)
            Else
                todayclosetime = System.Convert.ToDateTime(rs.Fields("time").Value)
            End If
            rs.MoveNext()
        End While

        If chb_open_add.Checked = True Then
            '选择了开灯偏移量
            If tb_open_pianyi.Text = "" Then
                MsgBox("请输入开灯的偏移量", , PROJECT_TITLE_STRING)
                tb_open_pianyi.Focus()
                Exit Sub
            End If
            If IsNumeric(tb_open_pianyi.Text) = False Then
                '如果输入的不是数字，提示出错
                MsgBox("开灯偏移量必须是数字，请输入！", , PROJECT_TITLE_STRING)
                tb_open_pianyi.Focus()
                Exit Sub
            End If

            '2011年5月30日增加偏移量的长度限制，最大不超过2小时，防止出现日期不属于当天的情况发生
            If Val(tb_open_pianyi.Text) > 120 Or Val(tb_open_pianyi.Text) < 0 Then
                MsgBox("时间设置为0-120之间，请重新输入", , PROJECT_TITLE_STRING)
                tb_open_pianyi.Focus()
                Exit Sub
            End If
            If rb_open_early.Checked = True Then
                '提前
                open_pianyivalue = -Val(Trim(tb_open_pianyi.Text))
            Else
                open_pianyivalue = Val(Trim(tb_open_pianyi.Text))
            End If

        Else
            open_pianyivalue = 0

        End If

        If chb_close_add.Checked = True Then
            '选择了关灯偏移量
            If tb_close_pianyi.Text = "" Then
                MsgBox("请输入关灯的偏移量", , PROJECT_TITLE_STRING)
                tb_close_pianyi.Focus()
                Exit Sub
            End If
            If IsNumeric(tb_close_pianyi.Text) = False Then
                '如果输入的不是数字，提示出错
                MsgBox("关灯偏移量必须是数字，请输入！", , PROJECT_TITLE_STRING)
                tb_close_pianyi.Focus()
                Exit Sub
            End If

            '2011年5月30日增加偏移量的长度限制，最大不超过2小时，防止出现日期不属于当天的情况发生
            If Val(tb_close_pianyi.Text) > 120 Or Val(tb_close_pianyi.Text) < 0 Then
                MsgBox("时间设置为0-120之间，请重新输入", , PROJECT_TITLE_STRING)
                tb_close_pianyi.Focus()
                Exit Sub
            End If
            If rb_close_early.Checked = True Then
                close_pianyivalue = -Val(Trim(tb_close_pianyi.Text))
            Else
                close_pianyivalue = Val(Trim(tb_close_pianyi.Text))
            End If

        Else
            close_pianyivalue = 0

        End If
        '为当前的日出日落时间增加上偏移量
        todayopentime = todayopentime.AddMinutes(open_pianyivalue)
        todayclosetime = todayclosetime.AddMinutes(close_pianyivalue)


        Dim boxlist(checklist.Count - 1) As String
        Dim i As Integer = 0
        Dim j As Integer = 0
        'Dim other_lampid As String
        Dim control_box_name As String '电控箱名称
        checklist.CopyTo(boxlist)

        ' lamp_shortid = Mid(lamp_id(j), 5, LAMP_ID_LEN + 2)
        While i < boxlist.Length
            '按电控箱的
            control_box_name = boxlist(i)
            lamp_id = Trim(controllampobj.get_jiechuqi_id(control_box_name, id_string)).Split(" ")
            j = 0
            While j < lamp_id.Length
                sql = "select * from pianyi where lamp_id='" & lamp_id(j) & "'"
                rs = DBOperation.SelectSQL(conn, sql, msg)
                If rs Is Nothing Then
                    MsgBox(MSG_ERROR_STRING & "set_timevalue_Click", , PROJECT_TITLE_STRING)
                    conn.Close()
                    conn = Nothing
                    Exit Sub
                End If
                If rs.RecordCount > 0 Then
                    sql = "update pianyi set open_pianyi='" & open_pianyivalue & "', close_pianyi='" & close_pianyivalue & "',today_opentime='" & todayopentime & "', today_closetime='" & todayclosetime & "' where lamp_id='" & lamp_id(j) & "'"
                    DBOperation.ExecuteSQL(conn, sql, msg)
                Else
                    sql = "insert into pianyi(lamp_id,open_pianyi, close_pianyi, today_opentime, today_closetime) values('" & lamp_id(j) & "', '" & open_pianyivalue & "','" & close_pianyivalue & "','" & todayopentime & "','" & todayclosetime & "')"
                    DBOperation.ExecuteSQL(conn, sql, msg)
                End If
                j += 1
            End While


            i += 1
        End While

        '将数据库中的当天的经纬度时刻更改为新的偏移量和日出日落时间的组合
        MsgBox("全夜灯模式设置成功", , PROJECT_TITLE_STRING)

        '增加设置记录
        Com_inf.Insert_Operation("设置全夜灯模式时间,开关偏移量：" & open_pianyivalue.ToString & " ," & close_pianyivalue.ToString)
        ''经纬度开关重新设置后，将立即下放当天的时间控制
        'Dim control_boxobj As New control_box
        'control_boxobj.ModXiaFang()
finish:
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing


    End Sub

    ''' <summary>
    ''' 增加控制模式
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rb_add_timecontrol_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_add_timecontrol.Click
        If rb_add_timecontrol.Checked = True Then
            '增加控制模式
            tb_mod_name.Visible = True
            cb_mod_list.Visible = False
            GroupBoxControl.Enabled = True
            bt_input_hms.Text = "输入控制模式"
            chb_show_modcontent.Enabled = False
            chb_show_modcontent.Checked = False
            dgv_add_modlist.Rows.Clear()
        End If
    End Sub

    ''' <summary>
    ''' 编辑控制模式
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rb_edit_timecontrol_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_edit_timecontrol.Click
        If rb_edit_timecontrol.Checked = True Then
            '编辑控制模式
            tb_mod_name.Visible = False
            cb_mod_list.Visible = True
            GroupBoxControl.Enabled = True
            bt_input_hms.Text = "编辑控制模式"
            chb_show_modcontent.Enabled = True
        End If
    End Sub

    ''' <summary>
    ''' 删除控制模式
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rb_del_timecontrol_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_del_timecontrol.Click
        If rb_del_timecontrol.Checked = True Then
            '删除控制模式
            tb_mod_name.Visible = False
            cb_mod_list.Visible = True
            GroupBoxControl.Enabled = False
            bt_input_hms.Text = "删除控制模式"
            chb_show_modcontent.Enabled = True
        End If
    End Sub

    ''' <summary>
    ''' 修改的方式为每个控制模式任意增加时间段及模式
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_add_divlevel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_add_divlevel.Click
        If Me.cb_hour_beg.Text = "" Then
            MsgBox("开始时间（小时）不可以为空", , PROJECT_TITLE_STRING)
            Me.cb_hour_beg.Focus()
            Exit Sub
        End If
        If Me.cb_min_beg.Text = "" Then
            MsgBox("开始时间（分钟）不可以为空", , PROJECT_TITLE_STRING)
            Me.cb_min_beg.Focus()
            Exit Sub
        End If

        If Me.cb_second_beg.Text = "" Then
            MsgBox("开始时间（秒）不可以为空", , PROJECT_TITLE_STRING)
            Me.cb_second_beg.Focus()
            Exit Sub
        End If

        If cb_mod_value.Text = "" Then
            MsgBox("模式不可以为空", , PROJECT_TITLE_STRING)
            Me.cb_mod_value.Focus()
            Exit Sub
        End If

        Me.dgv_add_modlist.Rows.Add()
        m_id = Me.dgv_add_modlist.RowCount   '行数
        Me.dgv_add_modlist.Rows(m_id - 1).Cells("id").Value = m_id
        Me.dgv_add_modlist.Rows(m_id - 1).Cells("start_hour").Value = Trim(cb_hour_beg.Text)
        Me.dgv_add_modlist.Rows(m_id - 1).Cells("start_min").Value = Trim(cb_min_beg.Text)
        Me.dgv_add_modlist.Rows(m_id - 1).Cells("start_second").Value = Trim(cb_second_beg.Text)
        Me.dgv_add_modlist.Rows(m_id - 1).Cells("control_method").Value = Trim(cb_mod_value.Text)
        If rb_sp_tiaoguang.Checked = True Then
            If rb_sp_power.Checked = True Then
                Me.dgv_add_modlist.Rows(m_id - 1).Cells("power").Value = Trim(tb_sp_power.Text)
            End If
            If rb_sp_diangan.Checked = True Then
                Me.dgv_add_modlist.Rows(m_id - 1).Cells("diangan").Value = Trim(cb_sp_diangan.Text)
            End If
        End If


        m_id += 1  '时段编号增加1
    End Sub

    '    Private Sub input_hms_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles input_hms.Click

    '        Dim rs As New ADODB.Recordset
    '        Dim conn As New ADODB.Connection
    '        Dim sql As String
    '        Dim msg As String
    '        Dim i As Integer
    '        Dim div_time_obj As New div_time_class
    '        msg = ""
    '        i = 0
    '        DBOperation.OpenConn(conn)

    '        '增加控制模式
    '        If Add_timecontrol.Checked = True Then
    '            If Me.Add_modlist.RowCount <= 0 Then
    '                MsgBox("控制名称列表为空", , PROJECT_TITLE_STRING)
    '                Exit Sub
    '            End If

    '            If mod_name.Text = "" Then
    '                MsgBox("请输入控制模式名称", , PROJECT_TITLE_STRING)
    '                Me.mod_name.Focus()
    '                Exit Sub
    '            End If
    '            If mod_name.TextLength > 10 Then
    '                MsgBox("控制模式名称长度大于10", , PROJECT_TITLE_STRING)
    '                Me.mod_name.Focus()
    '                Exit Sub
    '            End If

    '            sql = "select * from div_time where div_level='" & Trim(Me.mod_name.Text) & "'"
    '            rs = DBOperation.SelectSQL(conn, sql, msg)
    '            If rs Is Nothing Then
    '                MsgBox(MSG_ERROR_STRING & "input_hms_Click", , PROJECT_TITLE_STRING)
    '                conn.Close()
    '                conn = Nothing
    '                Exit Sub
    '            End If

    '            If rs.RecordCount > 0 Then
    '                '如果原来的数据库中该控制模式下存在时段模式，则表示询问是否编辑
    '                If MsgBox("该控制模式的配置已经存在，是否重新输入？", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.Yes Then
    '                    sql = "delete from div_time where div_level='" & Trim(mod_name.Text) & "'"
    '                    DBOperation.ExecuteSQL(conn, sql, msg)
    '                Else
    '                    GoTo finish

    '                End If

    '            End If
    '            While i < Me.Add_modlist.RowCount
    '                rs.AddNew()
    '                rs.Fields("id").Value = Me.Add_modlist.Rows(i).Cells("id").Value
    '                rs.Fields("hour_beg").Value = Trim(Me.Add_modlist.Rows(i).Cells("start_hour").Value)
    '                rs.Fields("min_beg").Value = Trim(Me.Add_modlist.Rows(i).Cells("start_min").Value)
    '                rs.Fields("second_beg").Value = Trim(Me.Add_modlist.Rows(i).Cells("start_second").Value)
    '                rs.Fields("mod").Value = Trim(Me.Add_modlist.Rows(i).Cells("moduel").Value)
    '                rs.Fields("gonglv").Value = 100
    '                rs.Fields("diangan").Value = Trim(gonglv1.Text)  '电感
    '                rs.Fields("div_level").Value = Trim(mod_name.Text)
    '                rs.Update()
    '                i += 1
    '            End While
    '            MsgBox("控制模式添加成功", , PROJECT_TITLE_STRING)

    '        End If

    '        '编辑控制模式
    '        If Me.Edit_timecontrol.Checked = True Then
    '            If Me.Add_modlist.RowCount <= 0 Then
    '                MsgBox("控制模式列表为空", , PROJECT_TITLE_STRING)
    '                Exit Sub
    '            End If

    '            If mod_list.Text = "" Then
    '                MsgBox("请选择控制模式名称", , PROJECT_TITLE_STRING)
    '                Me.mod_name.Focus()
    '                Exit Sub
    '            End If
    '            sql = "select * from div_time where div_level='" & Trim(Me.mod_list.Text) & "'"
    '            rs = DBOperation.SelectSQL(conn, sql, msg)
    '            If rs Is Nothing Then
    '                MsgBox(MSG_ERROR_STRING & "input_hms_Click", , PROJECT_TITLE_STRING)
    '                conn.Close()
    '                conn = Nothing
    '                Exit Sub
    '            End If

    '            If rs.RecordCount > 0 Then
    '                sql = "delete from div_time where div_level='" & Trim(mod_list.Text) & "'"
    '                DBOperation.ExecuteSQL(conn, sql, msg)
    '                While i < Me.Add_modlist.RowCount
    '                    rs.AddNew()
    '                    rs.Fields("id").Value = Me.Add_modlist.Rows(i).Cells("id").Value
    '                    rs.Fields("hour_beg").Value = Trim(Me.Add_modlist.Rows(i).Cells("start_hour").Value)
    '                    rs.Fields("min_beg").Value = Trim(Me.Add_modlist.Rows(i).Cells("start_min").Value)
    '                    rs.Fields("second_beg").Value = Trim(Me.Add_modlist.Rows(i).Cells("start_second").Value)
    '                    rs.Fields("mod").Value = Trim(Me.Add_modlist.Rows(i).Cells("moduel").Value)
    '                    rs.Fields("gonglv").Value = 100
    '                    rs.Fields("diangan").Value = Trim(gonglv1.Text)  '电感
    '                    rs.Fields("div_level").Value = Trim(mod_list.Text)
    '                    rs.Update()
    '                    i += 1
    '                End While
    '            Else
    '                MsgBox("不存在该控制模式名称，请重新选择", , PROJECT_TITLE_STRING)
    '                Me.mod_list.Focus()
    '                GoTo finish
    '            End If
    '            MsgBox("控制模式编辑成功", , PROJECT_TITLE_STRING)

    '        End If

    '        '删除控制模式
    '        If Me.Del_timecontrol.Checked = True Then
    '            If mod_list.Text = "" Then
    '                MsgBox("请选择控制模式名称", , PROJECT_TITLE_STRING)
    '                Me.mod_list.Focus()
    '                Exit Sub
    '            End If
    '            sql = "select * from div_time where div_level='" & Trim(Me.mod_list.Text) & "'"
    '            rs = DBOperation.SelectSQL(conn, sql, msg)
    '            If rs Is Nothing Then
    '                MsgBox(MSG_ERROR_STRING & "input_hms_Click", , PROJECT_TITLE_STRING)
    '                conn.Close()
    '                conn = Nothing
    '                Exit Sub
    '            End If

    '            If rs.RecordCount > 0 Then
    '                sql = "delete from div_time where div_level='" & Trim(mod_list.Text) & "'"
    '                DBOperation.ExecuteSQL(conn, sql, msg)

    '            Else
    '                MsgBox("不存在该控制模式名称，请重新选择", , PROJECT_TITLE_STRING)
    '                Me.mod_list.Focus()
    '                GoTo finish
    '            End If
    '            mod_list.Items.Clear()
    '            MsgBox("控制模式删除成功", , PROJECT_TITLE_STRING)

    '        End If

    '        '刷新时段控制的名称
    '        g_welcomewinobj.init_div_list()
    '        g_welcomewinobj.init_special_div_list()
    '        div_time_obj.Div_time_show()

    'finish:

    '        If rs.State = 1 Then
    '            rs.Close()
    '            rs = Nothing
    '        End If
    '        conn.Close()
    '        conn = Nothing

    '    End Sub

    ''' <summary>
    ''' 为下拉框增加控制名称的列表
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_mod_list_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_mod_list.DropDown
        'Dim rs As New ADODB.Recordset
        'Dim conn As New ADODB.Connection
        'Dim msg As String
        'Dim sql As String

        'msg = ""
        'DBOperation.OpenConn(conn)
        'sql = "select distinct div_level from div_time"
        'rs = DBOperation.SelectSQL(conn, sql, msg)
        'If rs Is Nothing Then
        '    MsgBox(MSG_ERROR_STRING & "mod_list_DropDown", , PROJECT_TITLE_STRING)
        '    conn.Close()
        '    conn = Nothing
        '    Exit Sub
        'End If

        'cb_mod_list.Items.Clear()
        'While rs.EOF = False
        '    cb_mod_list.Items.Add(Trim(rs.Fields("div_level").Value))
        '    rs.MoveNext()
        'End While

        'If rs.State = 1 Then
        '    rs.Close()
        '    rs = Nothing
        'End If
        'conn.Close()
        'conn = Nothing
        Com_inf.get_divtime_name(cb_mod_list, 1)

    End Sub

    Private Sub show_modcontent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chb_show_modcontent.Click
        If chb_show_modcontent.Checked = False Then
            Me.dgv_add_modlist.Rows.Clear()   '清空控制模式的数据
        Else
            If cb_mod_list.Text <> "" Then
                '显示当前选择的控制模式的时段控制内容
                Me.dgv_add_modlist.Rows.Clear()   '清空控制模式的数据
                Dim i As Integer = 0
                Dim rs As New ADODB.Recordset
                Dim conn As New ADODB.Connection
                Dim msg As String
                Dim sql As String

                msg = ""
                DBOperation.OpenConn(conn)
                sql = "select * from div_time where div_level='" & Trim(cb_mod_list.Text) & "'"
                rs = DBOperation.SelectSQL(conn, sql, msg)
                If rs Is Nothing Then
                    MsgBox(MSG_ERROR_STRING & "show_modcontent_Click", , PROJECT_TITLE_STRING)
                    conn.Close()
                    conn = Nothing
                    Exit Sub
                End If
                While rs.EOF = False
                    Me.dgv_add_modlist.Rows.Add()
                    m_id = Me.dgv_add_modlist.RowCount   '行数
                    Me.dgv_add_modlist.Rows(m_id - 1).Cells("id").Value = m_id
                    Me.dgv_add_modlist.Rows(m_id - 1).Cells("start_hour").Value = Trim(rs.Fields("hour_beg").Value)
                    Me.dgv_add_modlist.Rows(m_id - 1).Cells("start_min").Value = Trim(rs.Fields("min_beg").Value)
                    Me.dgv_add_modlist.Rows(m_id - 1).Cells("start_second").Value = Trim(rs.Fields("second_beg").Value)
                    Me.dgv_add_modlist.Rows(m_id - 1).Cells("moduel").Value = Trim(rs.Fields("mod").Value)

                    rs.MoveNext()
                End While

                If rs.State = 1 Then
                    rs.Close()
                    rs = Nothing
                End If
                conn.Close()
                conn = Nothing

            End If
        End If
    End Sub
    ''' <summary>
    ''' 显示选中的节假日控制模式
    ''' </summary>
    ''' <param name="modvalue"></param>
    ''' <param name="modname"></param>
    ''' <remarks></remarks>
    Private Sub select_holiday_modvalue(ByVal modvalue As System.Windows.Forms.DataGridView, ByVal modname As String)
        '显示当前选择的控制模式的时段控制内容
        modvalue.Rows.Clear()   '清空控制模式的数据
        Dim i As Integer = 0
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String

        msg = ""
        DBOperation.OpenConn(conn)
        sql = "select * from Special_div_time where name='" & modname & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "select_holiday_modvalue", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        While rs.EOF = False
            modvalue.Rows.Add()
            m_id = modvalue.RowCount   '行数
            modvalue.Rows(m_id - 1).Cells("holiday_id").Value = m_id
            modvalue.Rows(m_id - 1).Cells("holiday_time").Value = Trim(rs.Fields("Time").Value)
            modvalue.Rows(m_id - 1).Cells("holiday_controlmethod").Value = Trim(rs.Fields("mod").Value)
            modvalue.Rows(m_id - 1).Cells("holiday_power").Value = Trim(rs.Fields("gonglv").Value)
            modvalue.Rows(m_id - 1).Cells("holiday_diangan").Value = Trim(rs.Fields("diangan").Value)
         

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
    ''' 显示选中的半夜控制及特殊控制
    ''' </summary>
    ''' <param name="modvalue"></param>
    ''' <param name="modname"></param>
    ''' <remarks></remarks>
    Private Sub select_modvalue(ByVal modvalue As System.Windows.Forms.DataGridView, ByVal modname As String, ByVal sp_tag As Integer)
        '显示当前选择的控制模式的时段控制内容
        modvalue.Rows.Clear()   '清空控制模式的数据
        Dim i As Integer = 0
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String

        msg = ""
        DBOperation.OpenConn(conn)
        If sp_tag = 1 Then
            sql = "select * from div_time where div_level='" & modname & "' and hour_end is null"
        Else
            sql = "select * from div_time where div_level='" & modname & "' and hour_end=0"

        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "mod_list_SelectedIndexChanged", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        While rs.EOF = False
            modvalue.Rows.Add()
            m_id = modvalue.RowCount   '行数
            If sp_tag = 1 Then
                modvalue.Rows(m_id - 1).Cells(0).Value = m_id
                modvalue.Rows(m_id - 1).Cells(1).Value = Trim(rs.Fields("hour_beg").Value)
                modvalue.Rows(m_id - 1).Cells(2).Value = Trim(rs.Fields("min_beg").Value)
                modvalue.Rows(m_id - 1).Cells(3).Value = Trim(rs.Fields("second_beg").Value)
                modvalue.Rows(m_id - 1).Cells(4).Value = Trim(rs.Fields("mod").Value)
                modvalue.Rows(m_id - 1).Cells(5).Value = Trim(rs.Fields("gonglv").Value)
                modvalue.Rows(m_id - 1).Cells(6).Value = Trim(rs.Fields("diangan").Value)
            Else
                modvalue.Rows(m_id - 1).Cells(0).Value = m_id
                modvalue.Rows(m_id - 1).Cells(1).Value = Trim(rs.Fields("hour_beg").Value)
                modvalue.Rows(m_id - 1).Cells(2).Value = Trim(rs.Fields("min_beg").Value)
                modvalue.Rows(m_id - 1).Cells(3).Value = Trim(rs.Fields("second_beg").Value)
                modvalue.Rows(m_id - 1).Cells(4).Value = Trim(rs.Fields("mod").Value)

            End If
           

            rs.MoveNext()
        End While

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    Private Sub cb_mod_list_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_mod_list.SelectedIndexChanged
        If chb_show_modcontent.Checked = True Then
            If cb_mod_list.Text = "" Then
                MsgBox("请选择控制模式的名称", , PROJECT_TITLE_STRING)
                cb_mod_list.Focus()
                Exit Sub
            End If
            select_modvalue(Me.dgv_add_modlist, Trim(cb_mod_list.Text), 1)

        Else
            Me.dgv_add_modlist.Rows.Clear()   '清空控制模式的数据
        End If
    End Sub

    Private Sub cb_divmod_list_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_divmod_list.DropDown
        Com_inf.get_divtime_name(cb_divmod_list, 1)

    End Sub

    Private Sub bt_divmod_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_divmod.Click
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String
        Dim control_box_id() As String  '主控箱的编号
        Dim control_box_name() As String '主控箱名称
        Dim lamp_type As Integer  '整体控制的时候控制的类型
        Dim box_num As Integer  '主控箱的个数
        Dim lamp_id(5) As Boolean '交流接触器是否被选中
        Dim i As Integer = 0
        Dim week_id As Integer = 0  '星期
        Dim mod_string As String = "" '编辑的模式字符串
        Dim week() As String = {"日", "一", "二", "三", "四", "五", "六"}
        Dim type_string As String '记录类型名称
        msg = ""
        lamp_type = 31
        DBOperation.OpenConn(conn)

        checklist.Clear() '清除所有选中的项目
        Dim tnRet As New TreeNode
        For Each treenode As TreeNode In tv_divtime_controlbox.Nodes
            FindNode(treenode, checklist)
        Next
        If checklist.Count = 0 Then
            MsgBox("请选择需设置的主控箱名称", , PROJECT_TITLE_STRING)
            Exit Sub
        End If

        box_num = checklist.Count   '选中的电控箱的个数
     
        ReDim control_box_id(box_num)
        ReDim control_box_name(box_num)
        i = 0
        While i < box_num
            sql = "select control_box_id from control_box where control_box_name='" & Trim(checklist(i)) & "'"

            rs = DBOperation.SelectSQL(conn, sql, msg)
            If rs Is Nothing Then
                MsgBox(MSG_ERROR_STRING & "set_divmod_Click", , PROJECT_TITLE_STRING)
                conn.Close()
                conn = Nothing
                Exit Sub
            End If
            If rs.RecordCount > 0 Then
                control_box_id(i) = Trim(rs.Fields("control_box_id").Value)
                control_box_name(i) = Trim(checklist(i))
            End If
            i += 1
        End While


        i = 0
        If rb_control_all.Checked = True Then
            '对所选择的主控箱进行整体控制
            If cb_lamptype.Text = "" Then
                MsgBox("请选择整体控制的对象", , PROJECT_TITLE_STRING)
                cb_lamptype.Focus()
                GoTo finish
            End If
            If cb_divmod_list.Text = "" Then
                MsgBox("请选择模式名称", , PROJECT_TITLE_STRING)
                cb_divmod_list.Focus()
                GoTo finish
            End If
            type_string = Trim(cb_lamptype.Text)
            lamp_type = Com_inf.Get_Type_id(type_string)

            week_id = 0
            While week_id < 7
                If m_week(week_id) = 1 Then  '该天需要执行此模式
                    i = 0
                    While i < box_num
                        sql = "select * from road_level where control_box_id='" & control_box_id(i) & "' and type_id='" & lamp_type & "' and week_id='" & week_id & "'and lamp_id is null "
                        rs = DBOperation.SelectSQL(conn, sql, msg)
                        If rs Is Nothing Then
                            MsgBox(MSG_ERROR_STRING & "set_divmod_Click", , PROJECT_TITLE_STRING)
                            conn.Close()
                            conn = Nothing
                            Exit Sub
                        End If
                        If rs.RecordCount > 0 Then
                            If MsgBox("主控箱" & control_box_name(i) & " " & type_string & " 星期" & week(week_id) & "的控制模式已添加，是否覆盖原有设置", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.Yes Then
                                sql = "delete from road_level where control_box_id='" & control_box_id(i) & "' and type_id='" & lamp_type & "' and week_id='" & week_id & "' and lamp_id is null "
                                DBOperation.ExecuteSQL(conn, sql, msg)

                                sql = "insert into road_level(control_box_id, div_time_level,type_id,week_id) values('" & control_box_id(i) & "','" & Trim(cb_divmod_list.Text) & "','" & lamp_type & "','" & week_id & "')"
                                DBOperation.ExecuteSQL(conn, sql, msg)
                                mod_string &= control_box_name(i) & " " & type_string & " ,模式：" & Trim(cb_divmod_list.Text) & " ,星期" & week(week_id) & "; "
                            Else
                                GoTo finish
                            End If

                        Else
                            '没有主控箱的控制模式
                            sql = "insert into road_level(control_box_id, div_time_level,type_id,week_id) values('" & control_box_id(i) & "','" & Trim(cb_divmod_list.Text) & "','" & lamp_type & "','" & week_id & "')"
                            DBOperation.ExecuteSQL(conn, sql, msg)
                            mod_string &= control_box_name(i) & " " & type_string & " ,模式：" & Trim(cb_divmod_list.Text) & " ,星期" & week(week_id) & "; "

                        End If
                        i += 1
                    End While
                End If
                week_id += 1
            End While
            '增加设置记录
            If mod_string <> "" Then
                If mod_string.Length > 80 Then
                    mod_string = Mid(mod_string, 1, 80) & "..."
                End If
                Com_inf.Insert_Operation("设置控制模式：" & mod_string)

            End If


        Else
            '对所选择的主控箱的各个交流接触器进行单独控制
            If chb_k1.Checked = False And chb_k2.Checked = False And chb_k3.Checked = False And chb_k4.Checked = False And chb_k5.Checked = False And chb_k6.Checked = False Then
                MsgBox("请选择需要设置的接触器编号", , PROJECT_TITLE_STRING)
                GoTo finish
            End If
            If cb_divmod_list.Text = "" Then
                MsgBox("请选择模式名称", , PROJECT_TITLE_STRING)
                cb_divmod_list.Focus()
                GoTo finish
            End If
            If chb_k1.Checked = True Then
                lamp_id(0) = True
            Else
                lamp_id(0) = False
            End If
            If chb_k2.Checked = True Then
                lamp_id(1) = True
            Else
                lamp_id(1) = False
            End If
            If chb_k3.Checked = True Then
                lamp_id(2) = True
            Else
                lamp_id(2) = False
            End If
            If chb_k4.Checked = True Then
                lamp_id(3) = True
            Else
                lamp_id(3) = False
            End If
            If chb_k5.Checked = True Then
                lamp_id(4) = True
            Else
                lamp_id(4) = False
            End If
            If chb_k6.Checked = True Then
                lamp_id(5) = True
            Else
                lamp_id(5) = False
            End If

            i = 0
            Dim j As Integer = 0
            Dim id_string As String '记录设置的接触器编号
            week_id = 0
            While week_id < 7
                If m_week(week_id) = 1 Then
                    i = 0
                    j = 0
                    While i < box_num
                        j = 0
                        While j <= 5
                            If lamp_id(j) = True Then
                                '表示选中了，需要进行交流接触器的设置
                                id_string = control_box_id(i) & "31"
                                While id_string.Length < LAMP_ID_LEN + 5
                                    id_string = id_string & "0"
                                End While
                                id_string = id_string & (j + 1).ToString
                                sql = "select * from road_level where control_box_id='" & control_box_id(i) & "' and lamp_id='" & id_string & "' and week_id='" & week_id & "' and street_id is null"
                                rs = DBOperation.SelectSQL(conn, sql, msg)
                                If rs Is Nothing Then
                                    MsgBox(MSG_ERROR_STRING & "set_divmod_Click", , PROJECT_TITLE_STRING)
                                    conn.Close()
                                    conn = Nothing
                                    Exit Sub
                                End If
                                If rs.RecordCount > 0 Then
                                    '  If MsgBox("主控箱" & control_box_name(i) & " K" & j + 1 & "接触器，星期" & week(week_id) & "的控制模式已添加，是否覆盖原有设置", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.Yes Then
                                    sql = "delete from road_level where control_box_id='" & control_box_id(i) & "' and lamp_id='" & id_string & "' and week_id='" & week_id & "' and street_id is null"
                                    DBOperation.ExecuteSQL(conn, sql, msg)

                                    '表示这一天需要设置该模式
                                    sql = "insert into road_level(control_box_id, div_time_level,type_id,lamp_id,week_id) values('" & control_box_id(i) & "','" & Trim(cb_divmod_list.Text) & "',31,'" & id_string & "','" & week_id & "')"
                                    DBOperation.ExecuteSQL(conn, sql, msg)
                                    mod_string &= control_box_name(i) & " 接触器：K" & Val(Mid(id_string, 7, LAMP_ID_LEN)) & " ,模式：" & Trim(cb_divmod_list.Text) & " ,星期" & week(week_id) & "; "



                                    'Else
                                    '    GoTo finish
                                    ' End If

                                Else
                                    '表示这一天需要设置该模式
                                    sql = "insert into road_level(control_box_id, div_time_level,type_id,lamp_id,week_id) values('" & control_box_id(i) & "','" & Trim(cb_divmod_list.Text) & "',31,'" & id_string & "','" & week_id & "')"
                                    DBOperation.ExecuteSQL(conn, sql, msg)
                                    mod_string &= control_box_name(i) & " 接触器：K" & Val(Mid(id_string, 7, LAMP_ID_LEN)) & " ,模式：" & Trim(cb_divmod_list.Text) & " ,星期" & week(week_id) & "; "


                                End If

                            End If
                            j += 1
                        End While
                        i += 1
                    End While
                End If
                week_id += 1
            End While
            '增加设置记录
            If mod_string <> "" Then
                If mod_string.Length > 80 Then
                    mod_string = Mid(mod_string, 1, 80) & "..."
                End If
                Com_inf.Insert_Operation("设置控制模式：" & mod_string)

            End If


        End If
        MsgBox("控制模式设置成功", , PROJECT_TITLE_STRING)



        ''经纬度开关重新设置后，将立即下放当天的时间控制
        'Dim control_boxobj As New control_box
        'control_boxobj.ModXiaFang()

finish:


        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    Private Sub rb_control_huilu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_control_huilu.Click
        If rb_control_huilu.Checked = True Then
            huilu_group.Enabled = True
            all_group.Enabled = False
        End If
    End Sub

    Private Sub rb_control_all_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_control_all.Click
        If rb_control_all.Checked = True Then
            huilu_group.Enabled = False
            all_group.Enabled = True
        End If
    End Sub

    ''' <summary>
    ''' 输入节日的控制模式
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_input_holidayhms_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_input_holidayhms.Click
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String
        Dim i As Integer
        Dim div_time_obj As New div_time_class
        Dim power_value As String
        Dim diangan_value As String

        msg = ""
        i = 0
        DBOperation.OpenConn(conn)

        '增加节日控制模式
        If rb_add_holidaycontrol.Checked = True Then
            If Me.dgv_add_holidaymodlist.RowCount <= 0 Then
                MsgBox("控制名称列表为空", , PROJECT_TITLE_STRING)
                Exit Sub
            End If

            If tb_holidaymod_name.Text = "" Then
                MsgBox("请输入控制模式名称", , PROJECT_TITLE_STRING)
                Me.tb_holidaymod_name.Focus()
                Exit Sub
            End If
            If tb_holidaymod_name.TextLength > 10 Then
                MsgBox("控制模式名称长度大于10", , PROJECT_TITLE_STRING)
                Me.tb_holidaymod_name.Focus()
                Exit Sub
            End If

            sql = "select * from Special_div_time where name='" & Trim(Me.tb_holidaymod_name.Text) & "'"
            rs = DBOperation.SelectSQL(conn, sql, msg)
            If rs Is Nothing Then
                MsgBox(MSG_ERROR_STRING & "input_hms_Click", , PROJECT_TITLE_STRING)
                conn.Close()
                conn = Nothing
                Exit Sub
            End If

            If rs.RecordCount > 0 Then
                '如果原来的数据库中该控制模式下存在时段模式，则表示询问是否编辑
                If MsgBox("该节日控制模式的配置已经存在，是否重新输入？", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.Yes Then
                    sql = "delete from Special_div_time where name='" & Trim(tb_holidaymod_name.Text) & "'"
                    DBOperation.ExecuteSQL(conn, sql, msg)
                Else
                    GoTo finish

                End If

            End If
            While i < Me.dgv_add_holidaymodlist.RowCount
                If Trim(Me.dgv_add_holidaymodlist.Rows(i).Cells("holiday_power").Value) = "" Then
                    power_value = "100"
                Else
                    power_value = Trim(Me.dgv_add_holidaymodlist.Rows(i).Cells("holiday_power").Value)
                End If

                If Trim(Me.dgv_add_holidaymodlist.Rows(i).Cells("holiday_diangan").Value) = "" Then
                    diangan_value = "全功率"
                Else
                    diangan_value = Trim(Me.dgv_add_holidaymodlist.Rows(i).Cells("holiday_diangan").Value)
                End If
                rs.AddNew()
                ' rs.Fields("ID").Value = Me.Add_holidaymodlist.Rows(i).Cells("holiday_id").Value
                rs.Fields("Time").Value = Trim(Me.dgv_add_holidaymodlist.Rows(i).Cells("holiday_time").Value)
                rs.Fields("mod").Value = Trim(Me.dgv_add_holidaymodlist.Rows(i).Cells("holiday_controlmethod").Value)
                rs.Fields("gonglv").Value = power_value
                rs.Fields("diangan").Value = diangan_value  '电感
                rs.Fields("name").Value = Trim(tb_holidaymod_name.Text)
                rs.Update()
                i += 1
            End While
            MsgBox("节日控制模式添加成功", , PROJECT_TITLE_STRING)
            '增加设置记录
            Com_inf.Insert_Operation("添加节假日控制模式：" & Trim(tb_holidaymod_name.Text))

        End If

        '编辑控制模式
        If Me.rb_edit_holidaycontrol.Checked = True Then
            If Me.dgv_add_holidaymodlist.RowCount <= 0 Then
                MsgBox("控制模式列表为空", , PROJECT_TITLE_STRING)
                Exit Sub
            End If

            If cb_holidaymod_list.Text = "" Then
                MsgBox("请选择控制模式名称", , PROJECT_TITLE_STRING)
                tb_holidaymod_name.Focus()
                Exit Sub
            End If
            sql = "select * from Special_div_time where name='" & Trim(cb_holidaymod_list.Text) & "'"
            rs = DBOperation.SelectSQL(conn, sql, msg)
            If rs Is Nothing Then
                MsgBox(MSG_ERROR_STRING & "input_holidayhms_Click", , PROJECT_TITLE_STRING)
                conn.Close()
                conn = Nothing
                Exit Sub
            End If

            If rs.RecordCount > 0 Then
                sql = "delete from Special_div_time where name='" & Trim(cb_holidaymod_list.Text) & "'"
                DBOperation.ExecuteSQL(conn, sql, msg)
                While i < dgv_add_holidaymodlist.RowCount
                    If Trim(Me.dgv_add_holidaymodlist.Rows(i).Cells("holiday_power").Value) = "" Then
                        power_value = "100"
                    Else
                        power_value = Trim(Me.dgv_add_holidaymodlist.Rows(i).Cells("holiday_power").Value)
                    End If

                    If Trim(Me.dgv_add_holidaymodlist.Rows(i).Cells("holiday_diangan").Value) = "" Then
                        diangan_value = "全功率"
                    Else
                        diangan_value = Trim(Me.dgv_add_holidaymodlist.Rows(i).Cells("holiday_diangan").Value)
                    End If
                    rs.AddNew()
                    ' rs.Fields("id").Value = Add_holidaymodlist.Rows(i).Cells("holiday_id").Value
                    rs.Fields("Time").Value = Trim(Me.dgv_add_holidaymodlist.Rows(i).Cells("holiday_time").Value)
                    rs.Fields("mod").Value = Trim(Me.dgv_add_holidaymodlist.Rows(i).Cells("holiday_controlmethod").Value)
                    rs.Fields("gonglv").Value = power_value
                    rs.Fields("diangan").Value = diangan_value  '电感
                    rs.Fields("name").Value = Trim(cb_holidaymod_list.Text)
                    rs.Update()
                    i += 1
                End While
            Else
                MsgBox("不存在该节日控制模式名称，请重新选择", , PROJECT_TITLE_STRING)
                cb_holidaymod_list.Focus()
                GoTo finish
            End If
            MsgBox("节日控制模式编辑成功", , PROJECT_TITLE_STRING)
            '增加设置记录
            Com_inf.Insert_Operation("编辑节假日控制模式：" & Trim(cb_holidaymod_list.Text))

        End If

        '删除控制模式
        If Me.rb_del_holidaycontrol.Checked = True Then
            If cb_holidaymod_list.Text = "" Then
                MsgBox("请选择节日控制模式名称", , PROJECT_TITLE_STRING)
                cb_holidaymod_list.Focus()
                Exit Sub
            End If
            sql = "select * from Special_div_time where name='" & Trim(cb_holidaymod_list.Text) & "'"
            rs = DBOperation.SelectSQL(conn, sql, msg)
            If rs Is Nothing Then
                MsgBox(MSG_ERROR_STRING & "input_holidayhms_Click", , PROJECT_TITLE_STRING)
                conn.Close()
                conn = Nothing
                Exit Sub
            End If

            If rs.RecordCount > 0 Then
                sql = "delete from Special_div_time where name='" & Trim(cb_holidaymod_list.Text) & "'"
                DBOperation.ExecuteSQL(conn, sql, msg)

                '将Special_road_level表中的数据删除
                sql = "delete from Special_road_level where div_time_level='" & Trim(cb_mod_list.Text) & "'"
                DBOperation.ExecuteSQL(conn, sql, msg)


            Else
                MsgBox("不存在该节日控制模式名称，请重新选择", , PROJECT_TITLE_STRING)
                cb_holidaymod_list.Focus()
                GoTo finish
            End If
            cb_holidaymod_list.Items.Clear()
            MsgBox("节日控制模式删除成功", , PROJECT_TITLE_STRING)
            '增加设置记录
            Com_inf.Insert_Operation("删除节假日控制模式：" & Trim(cb_holidaymod_list.Text))

        End If

        '刷新时段控制的名称
        g_welcomewinobj.init_div_list()
        g_welcomewinobj.init_special_div_list()
        div_time_obj.Div_time_show()


        ''经纬度开关重新设置后，将立即下放当天的时间控制
        'Dim control_boxobj As New control_box
        'control_boxobj.ModXiaFang()

finish:

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    Private Sub Add_holidaydivlevel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_add_holidaydivlevel.Click

        If dtp_start_holidaytime.Text = "" Then
            MsgBox("开始日期不可以为空", , PROJECT_TITLE_STRING)
            dtp_start_holidaytime.Focus()
            Exit Sub
        End If
        If dtp_end_holidaytime.Text = "" Then
            MsgBox("结束日期不可以为空", , PROJECT_TITLE_STRING)
            dtp_end_holidaytime.Focus()
            Exit Sub
        End If
        If cb_holidaymod_value.Text = "" Then
            MsgBox("模式不可以为空", , PROJECT_TITLE_STRING)
            cb_holidaymod_value.Focus()
            Exit Sub
        End If
        If cb_holiday_hour.Text = "" Or cb_holiday_min.Text = "" Or cb_holiday_second.Text = "" Then
            MsgBox("控制时间不可以为空", , PROJECT_TITLE_STRING)
            Exit Sub
        End If


        Dim i As Integer = 0
        Dim day_num As Integer
        Dim datetime As DateTime
        Dim s As String
        day_num = dtp_end_holidaytime.Value.DayOfYear - dtp_start_holidaytime.Value.DayOfYear
        While i <= day_num
            datetime = dtp_start_holidaytime.Value.AddDays(i)
            s = datetime.Year.ToString & "-" & datetime.Month.ToString & "-" & datetime.Day.ToString & " " & Trim(cb_holiday_hour.Text) & ":" & Trim(cb_holiday_min.Text) & ":" & Trim(cb_holiday_second.Text)
            datetime = System.Convert.ToDateTime(s)
            dgv_add_holidaymodlist.Rows.Add()
            m_hm_id = dgv_add_holidaymodlist.RowCount   '行数
            dgv_add_holidaymodlist.Rows(m_hm_id - 1).Cells("holiday_id").Value = m_hm_id
            dgv_add_holidaymodlist.Rows(m_hm_id - 1).Cells("holiday_time").Value = datetime
            dgv_add_holidaymodlist.Rows(m_hm_id - 1).Cells("holiday_controlmethod").Value = Trim(cb_holidaymod_value.Text)
            If rb_tiaoguang.Checked = True Then
                If rb_power.Checked = True Then
                    Me.dgv_add_holidaymodlist.Rows(m_hm_id - 1).Cells("holiday_power").Value = Trim(tb_power.Text)
                End If
                If rb_diangan.Checked = True Then
                    Me.dgv_add_holidaymodlist.Rows(m_hm_id - 1).Cells("holiday_diangan").Value = Trim(cb_diangan.Text)
                End If
            End If

            m_hm_id += 1  '时段编号增加1
            i += 1
        End While
     
    End Sub

    Private Sub bt_input_hms_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_input_hms.Click

        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String
        Dim i As Integer
        Dim div_time_obj As New div_time_class
        Dim diangan_value As String
        Dim power_value As String

     
        msg = ""
        i = 0
        DBOperation.OpenConn(conn)

        '增加控制模式
        If rb_add_timecontrol.Checked = True Then
            If Me.dgv_add_modlist.RowCount <= 0 Then
                MsgBox("控制名称列表为空", , PROJECT_TITLE_STRING)
                Exit Sub
            End If

            If tb_mod_name.Text = "" Then
                MsgBox("请输入控制模式名称", , PROJECT_TITLE_STRING)
                Me.tb_mod_name.Focus()
                Exit Sub
            End If
            If tb_mod_name.TextLength > 10 Then
                MsgBox("控制模式名称长度大于10", , PROJECT_TITLE_STRING)
                Me.tb_mod_name.Focus()
                Exit Sub
            End If

            sql = "select * from div_time where div_level='" & Trim(Me.tb_mod_name.Text) & "' and hour_end is null"
            rs = DBOperation.SelectSQL(conn, sql, msg)
            If rs Is Nothing Then
                MsgBox(MSG_ERROR_STRING & "input_hms_Click", , PROJECT_TITLE_STRING)
                conn.Close()
                conn = Nothing
                Exit Sub
            End If

            If rs.RecordCount > 0 Then
                '如果原来的数据库中该控制模式下存在时段模式，则表示询问是否编辑
                If MsgBox("该控制模式的配置已经存在，是否重新输入？", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.Yes Then
                    sql = "delete from div_time where div_level='" & Trim(tb_mod_name.Text) & "'"
                    DBOperation.ExecuteSQL(conn, sql, msg)
                Else
                    GoTo finish

                End If

            End If
            While i < Me.dgv_add_modlist.RowCount

                If Trim(Me.dgv_add_modlist.Rows(i).Cells("power").Value) = "" Then
                    power_value = "100"
                Else
                    power_value = Trim(Me.dgv_add_modlist.Rows(i).Cells("power").Value)
                End If

                If Trim(Me.dgv_add_modlist.Rows(i).Cells("diangan").Value) = "" Then
                    diangan_value = "全功率"
                Else
                    diangan_value = Trim(Me.dgv_add_modlist.Rows(i).Cells("diangan").Value)
                End If

                rs.AddNew()
                rs.Fields("id").Value = Me.dgv_add_modlist.Rows(i).Cells("id").Value
                rs.Fields("hour_beg").Value = Trim(Me.dgv_add_modlist.Rows(i).Cells("start_hour").Value)
                rs.Fields("min_beg").Value = Trim(Me.dgv_add_modlist.Rows(i).Cells("start_min").Value)
                rs.Fields("second_beg").Value = Trim(Me.dgv_add_modlist.Rows(i).Cells("start_second").Value)
                rs.Fields("mod").Value = Trim(Me.dgv_add_modlist.Rows(i).Cells("control_method").Value)
                rs.Fields("gonglv").Value = power_value
                rs.Fields("diangan").Value = diangan_value  '电感
                rs.Fields("div_level").Value = Trim(tb_mod_name.Text)
                rs.Update()
                i += 1
            End While
            MsgBox("控制模式添加成功", , PROJECT_TITLE_STRING)
            '增加设置记录
            Com_inf.Insert_Operation("添加特殊控制模式：" & Trim(tb_mod_name.Text))

        End If

        '编辑控制模式
        If Me.rb_edit_timecontrol.Checked = True Then
            If Me.dgv_add_modlist.RowCount <= 0 Then
                MsgBox("控制模式列表为空", , PROJECT_TITLE_STRING)
                Exit Sub
            End If

            If cb_mod_list.Text = "" Then
                MsgBox("请选择控制模式名称", , PROJECT_TITLE_STRING)
                Me.tb_mod_name.Focus()
                Exit Sub
            End If
            sql = "select * from div_time where div_level='" & Trim(Me.cb_mod_list.Text) & "' and hour_end is null"
            rs = DBOperation.SelectSQL(conn, sql, msg)
            If rs Is Nothing Then
                MsgBox(MSG_ERROR_STRING & "input_hms_Click", , PROJECT_TITLE_STRING)
                conn.Close()
                conn = Nothing
                Exit Sub
            End If

            If rs.RecordCount > 0 Then
                sql = "delete from div_time where div_level='" & Trim(cb_mod_list.Text) & "' and hour_end is null"
                DBOperation.ExecuteSQL(conn, sql, msg)
                While i < Me.dgv_add_modlist.RowCount

                    If Trim(Me.dgv_add_modlist.Rows(i).Cells("power").Value) = "" Then
                        power_value = "100"
                    Else
                        power_value = Trim(Me.dgv_add_modlist.Rows(i).Cells("power").Value)
                    End If

                    If Trim(Me.dgv_add_modlist.Rows(i).Cells("diangan").Value) = "" Then
                        diangan_value = "全功率"
                    Else
                        diangan_value = Trim(Me.dgv_add_modlist.Rows(i).Cells("diangan").Value)
                    End If
                    rs.AddNew()
                    rs.Fields("id").Value = Me.dgv_add_modlist.Rows(i).Cells("id").Value
                    rs.Fields("hour_beg").Value = Trim(Me.dgv_add_modlist.Rows(i).Cells("start_hour").Value)
                    rs.Fields("min_beg").Value = Trim(Me.dgv_add_modlist.Rows(i).Cells("start_min").Value)
                    rs.Fields("second_beg").Value = Trim(Me.dgv_add_modlist.Rows(i).Cells("start_second").Value)
                    rs.Fields("mod").Value = Trim(Me.dgv_add_modlist.Rows(i).Cells("control_method").Value)
                    rs.Fields("gonglv").Value = power_value
                    rs.Fields("diangan").Value = diangan_value  '电感
                    rs.Fields("div_level").Value = Trim(cb_mod_list.Text)
                    rs.Update()
                    i += 1
                End While
            Else
                MsgBox("不存在该控制模式名称，请重新选择", , PROJECT_TITLE_STRING)
                Me.cb_mod_list.Focus()
                GoTo finish
            End If
            MsgBox("控制模式编辑成功", , PROJECT_TITLE_STRING)
            '增加设置记录
            Com_inf.Insert_Operation("编辑特殊控制模式：" & Trim(cb_mod_list.Text))

        End If

        '删除控制模式
        If Me.rb_del_timecontrol.Checked = True Then
            If cb_mod_list.Text = "" Then
                MsgBox("请选择控制模式名称", , PROJECT_TITLE_STRING)
                Me.cb_mod_list.Focus()
                Exit Sub
            End If
            sql = "select * from div_time where div_level='" & Trim(Me.cb_mod_list.Text) & "'"
            rs = DBOperation.SelectSQL(conn, sql, msg)
            If rs Is Nothing Then
                MsgBox(MSG_ERROR_STRING & "input_hms_Click", , PROJECT_TITLE_STRING)
                conn.Close()
                conn = Nothing
                Exit Sub
            End If

            If rs.RecordCount > 0 Then
                sql = "delete from div_time where div_level='" & Trim(cb_mod_list.Text) & "' and hour_end is null"
                DBOperation.ExecuteSQL(conn, sql, msg)

                '将road_level表中的数据删除
                sql = "delete from road_level where div_time_level='" & Trim(cb_mod_list.Text) & "' and street_id is null"
                DBOperation.ExecuteSQL(conn, sql, msg)

            Else
                MsgBox("不存在该控制模式名称，请重新选择", , PROJECT_TITLE_STRING)
                Me.cb_mod_list.Focus()
                GoTo finish
            End If
            cb_mod_list.Items.Clear()
            MsgBox("控制模式删除成功", , PROJECT_TITLE_STRING)
            '增加设置记录
            Com_inf.Insert_Operation("删除特殊控制模式：" & Trim(cb_mod_list.Text))

        End If

        '刷新时段控制的名称
        g_welcomewinobj.init_div_list()
        g_welcomewinobj.init_special_div_list()
        div_time_obj.Div_time_show()

        ''经纬度开关重新设置后，将立即下放当天的时间控制
        'Dim control_boxobj As New control_box
        'control_boxobj.ModXiaFang()


finish:

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    Private Sub rb_add_holidaycontrol_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_add_holidaycontrol.Click
        If rb_add_holidaycontrol.Checked = True Then
            '增加节日控制模式
            tb_holidaymod_name.Visible = True
            cb_holidaymod_list.Visible = False
            holiday_old_controlmethod.Enabled = True
            bt_input_holidayhms.Text = "输入节日控制模式"
            'chb_show_holidaymodcontent.Enabled = False
            'chb_show_holidaymodcontent.Checked = False
            dgv_add_holidaymodlist.Rows.Clear()
        End If
    End Sub

    Private Sub rb_edit_holidaycontrol_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_edit_holidaycontrol.Click
        If rb_edit_holidaycontrol.Checked = True Then
            '编辑节日控制模式
            tb_holidaymod_name.Visible = False
            cb_holidaymod_list.Visible = True
            holiday_old_controlmethod.Enabled = True
            bt_input_holidayhms.Text = "编辑节日控制模式"
            ' chb_show_holidaymodcontent.Enabled = True
        End If
    End Sub

    Private Sub rb_del_holidaycontrol_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_del_holidaycontrol.Click
        If rb_del_holidaycontrol.Checked = True Then
            '删除节日控制模式
            tb_holidaymod_name.Visible = False
            cb_holidaymod_list.Visible = True
            holiday_old_controlmethod.Enabled = False
            bt_input_holidayhms.Text = "删除节日控制模式"
            'chb_show_holidaymodcontent.Enabled = True
        End If
    End Sub

    Private Sub chb_show_holidaymodcontent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chb_show_holidaymodcontent.Click
        If chb_show_holidaymodcontent.Checked = False Then
            Me.dgv_add_holidaymodlist.Rows.Clear()   '清空控制模式的数据
        Else
            If cb_holidaymod_list.Text <> "" Then
                '显示当前选择的控制模式的时段控制内容
                Me.dgv_add_holidaymodlist.Rows.Clear()   '清空控制模式的数据
                Dim i As Integer = 0
                Dim rs As New ADODB.Recordset
                Dim conn As New ADODB.Connection
                Dim msg As String
                Dim sql As String

                msg = ""
                DBOperation.OpenConn(conn)
                sql = "select * from Special_div_time where name='" & Trim(cb_holidaymod_list.Text) & "'"
                rs = DBOperation.SelectSQL(conn, sql, msg)
                If rs Is Nothing Then
                    MsgBox(MSG_ERROR_STRING & "show_holidaymodcontent_Click", , PROJECT_TITLE_STRING)
                    conn.Close()
                    conn = Nothing
                    Exit Sub
                End If
                While rs.EOF = False
                    Me.dgv_add_holidaymodlist.Rows.Add()
                    m_hm_id = Me.dgv_add_holidaymodlist.RowCount   '行数
                    Me.dgv_add_holidaymodlist.Rows(m_hm_id - 1).Cells("holiday_id").Value = m_hm_id
                    Me.dgv_add_holidaymodlist.Rows(m_hm_id - 1).Cells("holiday_time").Value = Trim(rs.Fields("Time").Value)
                    Me.dgv_add_holidaymodlist.Rows(m_hm_id - 1).Cells("holiday_moduel").Value = Trim(rs.Fields("mod").Value)

                    rs.MoveNext()
                End While

                If rs.State = 1 Then
                    rs.Close()
                    rs = Nothing
                End If
                conn.Close()
                conn = Nothing

            End If
        End If
    End Sub

    Private Sub cb_set_holidaydivmod_list_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_set_holidaydivmod_list.DropDown
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String

        msg = ""
        DBOperation.OpenConn(conn)
        sql = "select distinct name from Special_div_time"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "set_holidaydivmod_list_DropDown", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If

        cb_set_holidaydivmod_list.Items.Clear()
        While rs.EOF = False
            cb_set_holidaydivmod_list.Items.Add(Trim(rs.Fields("name").Value))
            rs.MoveNext()
        End While

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    Private Sub rb_holidaycontrol_all_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_holidaycontrol_all.Click
        If rb_holidaycontrol_all.Checked = True Then
            holidayhuilu_group.Enabled = False
            holidayall_group.Enabled = True
        End If
    End Sub

    Private Sub rb_holidaycontrol_huilu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_holidaycontrol_huilu.Click
        If rb_holidaycontrol_huilu.Checked = True Then
            holidayhuilu_group.Enabled = True
            holidayall_group.Enabled = False
        End If
    End Sub

    Private Sub bt_set_holidaydivmod_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_set_holidaydivmod.Click
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String
        Dim control_box_id() As String  '主控箱的编号
        Dim control_box_name() As String '主控箱名称
        Dim lamp_type As Integer  '整体控制的时候控制的类型
        Dim box_num As Integer  '主控箱的个数
        Dim lamp_id(5) As Boolean '交流接触器是否被选中
        Dim type_string As String '类型名称
        Dim mod_string As String = ""
        Dim i As Integer = 0
        msg = ""
        lamp_type = 31
        DBOperation.OpenConn(conn)

        checklist.Clear() '清除所有选中的项目
        Dim tnRet As New TreeNode
        For Each treenode As TreeNode In tv_holidaydivtime_box.Nodes
            FindNode(treenode, checklist)
        Next
        If checklist.Count = 0 Then
            MsgBox("请选择需设置的主控箱名称", , PROJECT_TITLE_STRING)
            Exit Sub
        End If

        box_num = checklist.Count   '选中的电控箱的个数

        ReDim control_box_id(box_num)
        ReDim control_box_name(box_num)
        i = 0
        While i < box_num
            sql = "select control_box_id from control_box where control_box_name='" & Trim(checklist(i)) & "'"

            rs = DBOperation.SelectSQL(conn, sql, msg)
            If rs Is Nothing Then
                MsgBox(MSG_ERROR_STRING & "set_holidaydivmod_Click", , PROJECT_TITLE_STRING)
                conn.Close()
                conn = Nothing
                Exit Sub
            End If
            If rs.RecordCount > 0 Then
                control_box_id(i) = Trim(rs.Fields("control_box_id").Value)
                control_box_name(i) = Trim(checklist(i))
            End If
            i += 1
        End While


        i = 0
        If rb_holidaycontrol_all.Checked = True Then
            '对所选择的主控箱进行整体控制
            If cb_holiday_lamptype.Text = "" Then
                MsgBox("请选择整体控制的对象", , PROJECT_TITLE_STRING)
                GoTo finish
            End If
            If cb_set_holidaydivmod_list.Text = "" Then
                MsgBox("请选择模式名称", , PROJECT_TITLE_STRING)
                cb_set_holidaydivmod_list.Focus()
                GoTo finish
            End If
            'If rb_holidaylamptype_31.Checked = True Then
            '    lamp_type = 31  '主控箱节点类型
            '    type_string = rb_holidaylamptype_31.Text
            'Else
            '    lamp_type = 0  '终端类型
            '    type_string = rb_holidaylamptype_0.Text
            'End If

            type_string = Trim(cb_holiday_lamptype.Text)
            lamp_type = Com_inf.Get_Type_id(type_string)

            i = 0
            While i < box_num
                sql = "select * from Special_road_level where control_box_id='" & control_box_id(i) & "' and type_id='" & lamp_type & "' and lamp_id is null"
                rs = DBOperation.SelectSQL(conn, sql, msg)
                If rs Is Nothing Then
                    MsgBox(MSG_ERROR_STRING & "set_holidaydivmod_Click", , PROJECT_TITLE_STRING)
                    conn.Close()
                    conn = Nothing
                    Exit Sub
                End If
                If rs.RecordCount > 0 Then
                    '  If MsgBox("主控箱" & control_box_name(i) & " " & type_string & "的节日控制模式已添加，是否覆盖原有设置", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.Yes Then
                    sql = "delete from Special_road_level where control_box_id='" & control_box_id(i) & "' and type_id='" & lamp_type & "' and lamp_id is null"
                    DBOperation.ExecuteSQL(conn, sql, msg)
                    sql = "insert into Special_road_level(control_box_id, div_time_level,type_id) values('" & control_box_id(i) & "','" & Trim(cb_set_holidaydivmod_list.Text) & "','" & lamp_type & "')"
                    DBOperation.ExecuteSQL(conn, sql, msg)
                    mod_string &= control_box_name(i) & " " & type_string & " ,模式：" & Trim(cb_divmod_list.Text) & "; "

                    'Else
                    '    GoTo finish
                    '  End If

                Else
                    '没有主控箱的控制模式
                    sql = "insert into Special_road_level(control_box_id, div_time_level,type_id) values('" & control_box_id(i) & "','" & Trim(cb_set_holidaydivmod_list.Text) & "','" & lamp_type & "')"
                    DBOperation.ExecuteSQL(conn, sql, msg)
                    mod_string &= control_box_name(i) & " " & type_string & " ,模式：" & Trim(cb_divmod_list.Text) & "; "

                End If
                i += 1
            End While
            If mod_string <> "" Then
                If mod_string.Length > 80 Then
                    mod_string = Mid(mod_string, 1, 80) & "..."
                End If
                Com_inf.Insert_Operation(mod_string)
            End If
        Else
            '对所选择的主控箱的各个交流接触器进行单独控制
            If chb_holidayk1.Checked = False And chb_holidayk2.Checked = False And chb_holidayk3.Checked _
            = False And chk_holidayk4.Checked = False And chk_holidayk5.Checked = False And chk_holidayk6.Checked = False Then
                MsgBox("请选择需要设置的接触器编号", , PROJECT_TITLE_STRING)
                GoTo finish
            End If
            If cb_set_holidaydivmod_list.Text = "" Then
                MsgBox("请选择模式名称", , PROJECT_TITLE_STRING)
                cb_set_holidaydivmod_list.Focus()
                GoTo finish
            End If
            If chb_holidayk1.Checked = True Then
                lamp_id(0) = True
            Else
                lamp_id(0) = False
            End If
            If chb_holidayk2.Checked = True Then
                lamp_id(1) = True
            Else
                lamp_id(1) = False
            End If
            If chb_holidayk3.Checked = True Then
                lamp_id(2) = True
            Else
                lamp_id(2) = False
            End If
            If chk_holidayk4.Checked = True Then
                lamp_id(3) = True
            Else
                lamp_id(3) = False
            End If
            If chk_holidayk5.Checked = True Then
                lamp_id(4) = True
            Else
                lamp_id(4) = False
            End If
            If chk_holidayk6.Checked = True Then
                lamp_id(5) = True
            Else
                lamp_id(5) = False
            End If

            i = 0
            Dim j As Integer = 0
            Dim id_string As String '记录设置的接触器编号
            While i < box_num
                j = 0
                While j <= 5
                    If lamp_id(j) = True Then
                        '表示选中了，需要进行交流接触器的设置
                        id_string = control_box_id(i) & "31"
                        While id_string.Length < LAMP_ID_LEN + 5
                            id_string = id_string & "0"
                        End While
                        id_string = id_string & (j + 1).ToString
                        sql = "select * from Special_road_level where control_box_id='" & control_box_id(i) & "' and lamp_id='" & id_string & "' "
                        rs = DBOperation.SelectSQL(conn, sql, msg)
                        If rs Is Nothing Then
                            MsgBox(MSG_ERROR_STRING & "set_holidaydivmod_Click", , PROJECT_TITLE_STRING)
                            conn.Close()
                            conn = Nothing
                            Exit Sub
                        End If
                        If rs.RecordCount > 0 Then
                            If MsgBox("主控箱" & control_box_id(i) & "的节日控制模式已添加，是否覆盖原有设置", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.Yes Then
                                sql = "delete from Special_road_level where control_box_id='" & control_box_id(i) & "' and lamp_id='" & id_string & "' "
                                DBOperation.ExecuteSQL(conn, sql, msg)
                                sql = "insert into Special_road_level(control_box_id, div_time_level,type_id,lamp_id) values('" & control_box_id(i) & "','" & Trim(cb_set_holidaydivmod_list.Text) & "',31,'" & id_string & "')"
                                DBOperation.ExecuteSQL(conn, sql, msg)
                                mod_string &= control_box_name(i) & " 接触器：K" & Val(Mid(id_string, 7, LAMP_ID_LEN)) & " ,模式：" & Trim(cb_divmod_list.Text) & "; "

                            Else
                                GoTo finish
                            End If

                        Else
                            '没有该主控箱下交流接触器的控制模式
                            sql = "insert into Special_road_level(control_box_id, div_time_level,type_id,lamp_id) values('" & control_box_id(i) & "','" & Trim(cb_set_holidaydivmod_list.Text) & "',31,'" & id_string & "')"
                            DBOperation.ExecuteSQL(conn, sql, msg)
                            mod_string &= control_box_name(i) & " 接触器：K" & Val(Mid(id_string, 7, LAMP_ID_LEN)) & " ,模式：" & Trim(cb_divmod_list.Text) & "; "

                        End If

                    End If
                    j += 1
                End While
                i += 1
            End While

            If mod_string <> "" Then
                If mod_string.Length > 80 Then
                    mod_string = Mid(mod_string, 1, 80) & "..."
                End If
                Com_inf.Insert_Operation(mod_string)
            End If

        End If
        MsgBox("节日控制模式设置成功", , PROJECT_TITLE_STRING)

        ''经纬度开关重新设置后，将立即下放当天的时间控制
        'Dim control_boxobj As New control_box
        'control_boxobj.ModXiaFang()

finish:


        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    Private Sub cb_holidaymod_list_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_holidaymod_list.DropDown
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String

        msg = ""
        DBOperation.OpenConn(conn)
        sql = "select distinct name from special_div_time"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "holidaymod_list_DropDown", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If

        cb_holidaymod_list.Items.Clear()
        While rs.EOF = False
            cb_holidaymod_list.Items.Add(Trim(rs.Fields("name").Value))
            rs.MoveNext()
        End While

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing

    End Sub

    Private Sub cb_holidaymod_list_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_holidaymod_list.SelectedIndexChanged
        If chb_show_holidaymodcontent.Checked = True Then
            If cb_holidaymod_list.Text = "" Then
                MsgBox("请选择控制模式的名称", , PROJECT_TITLE_STRING)
                cb_holidaymod_list.Focus()
                Exit Sub
            End If
            '显示当前选择的控制模式的时段控制内容
            Me.dgv_add_holidaymodlist.Rows.Clear()   '清空控制模式的数据
            Dim i As Integer = 0
            Dim rs As New ADODB.Recordset
            Dim conn As New ADODB.Connection
            Dim msg As String
            Dim sql As String

            msg = ""
            DBOperation.OpenConn(conn)
            sql = "select * from Special_div_time where name='" & Trim(cb_holidaymod_list.Text) & "'"
            rs = DBOperation.SelectSQL(conn, sql, msg)
            If rs Is Nothing Then
                MsgBox(MSG_ERROR_STRING & "holidaymod_list_SelectedIndexChanged", , PROJECT_TITLE_STRING)
                conn.Close()
                conn = Nothing
                Exit Sub
            End If
            While rs.EOF = False
                Me.dgv_add_holidaymodlist.Rows.Add()
                m_hm_id = Me.dgv_add_holidaymodlist.RowCount   '行数
                Me.dgv_add_holidaymodlist.Rows(m_hm_id - 1).Cells("holiday_id").Value = m_hm_id
                Me.dgv_add_holidaymodlist.Rows(m_hm_id - 1).Cells("holiday_Time").Value = Trim(rs.Fields("Time").Value)
                Me.dgv_add_holidaymodlist.Rows(m_hm_id - 1).Cells("holiday_controlmethod").Value = Trim(rs.Fields("mod").Value)
                Me.dgv_add_holidaymodlist.Rows(m_hm_id - 1).Cells("holiday_power").Value = rs.Fields("gonglv").Value
                Me.dgv_add_holidaymodlist.Rows(m_hm_id - 1).Cells("holiday_diangan").Value = rs.Fields("diangan").Value
                rs.MoveNext()
            End While

            If rs.State = 1 Then
                rs.Close()
                rs = Nothing
            End If
            conn.Close()
            conn = Nothing

        Else
            Me.dgv_add_modlist.Rows.Clear()   '清空控制模式的数据
        End If
    End Sub

    Private Sub chb_show_weekset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chb_show_weekset.Click
        '显示未来一周的时刻设置
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String
        Dim nowtime As DateTime
        Dim i As Integer = 0
        Dim open_pianyivalue, close_pianyivalue As Integer '开关灯偏移量
        Dim open_time, close_time As DateTime '开灯关灯的时间
        Dim week_num() As String = {"日", "一", "二", "三", "四", "五", "六"} '星期几

        msg = ""
        DBOperation.OpenConn(conn)
        nowtime = System.Convert.ToDateTime(Now.Year.ToString & "-" & Now.Month.ToString & "-" & _
        Now.Day.ToString & " 0:0:0")
        '  nowtime = Now
        If chb_show_weekset.Checked = True Then
            dgv_week_time.Rows.Clear()  '清空周设置时刻
            If nowtime.Month = DateAdd(DateInterval.Day, 7, nowtime).Month Then
                '一个星期在同一个月内
                sql = "select * from suntime where month(time)='" & nowtime.Month & "' and day(time)>='" & nowtime.Day & "' and day(time)<'" & DateAdd(DateInterval.Day, 7, nowtime).Day & "' order by id"
            Else
                'sql = "select * from suntime where day(time)>='" & nowtime.Day & "' and month(time)='" & nowtime.Month & "' or day(time)<'" & DateAdd(DateInterval.Day, 7, nowtime).Day & "' and month(time)='" & DateAdd(DateInterval.Day, 7, nowtime).Month & "'  order by id"
                sql = "select * from suntime where day(time)>='" & nowtime.Day & "' and month(time)='" & nowtime.Month & "'  order by id"

            End If
            rs = DBOperation.SelectSQL(conn, sql, msg)
            If rs Is Nothing Then
                MsgBox(MSG_ERROR_STRING & "show_weekset_Click", , PROJECT_TITLE_STRING)
                conn.Close()
                conn = Nothing
                Exit Sub
            End If

            If chb_open_add.Checked = True Then
                '选择了开灯偏移量
                If tb_open_pianyi.Text = "" Then
                    MsgBox("请输入开灯的偏移量", , PROJECT_TITLE_STRING)
                    tb_open_pianyi.Focus()
                    GoTo finish
                End If
                If IsNumeric(tb_open_pianyi.Text) = False Then
                    '如果输入的不是数字，提示出错
                    MsgBox("开灯偏移量必须是数字，请输入！", , PROJECT_TITLE_STRING)
                    tb_open_pianyi.Focus()
                    GoTo finish
                End If

                If Val(tb_open_pianyi.Text) > 120 Or Val(tb_open_pianyi.Text) < 0 Then
                    MsgBox("时间设置为0-120之间，请重新输入", , PROJECT_TITLE_STRING)
                    tb_open_pianyi.Focus()
                    Exit Sub
                End If
                If rb_open_early.Checked = True Then
                    '提前
                    open_pianyivalue = -Val(Trim(tb_open_pianyi.Text))
                Else
                    open_pianyivalue = Val(Trim(tb_open_pianyi.Text))
                End If

            Else
                open_pianyivalue = 0

            End If

            If chb_close_add.Checked = True Then
                '选择了关灯偏移量
                If tb_close_pianyi.Text = "" Then
                    MsgBox("请输入开灯的偏移量", , PROJECT_TITLE_STRING)
                    tb_close_pianyi.Focus()
                    GoTo finish
                End If
                If IsNumeric(tb_close_pianyi.Text) = False Then
                    '如果输入的不是数字，提示出错
                    MsgBox("开灯偏移量必须是数字，请输入！", , PROJECT_TITLE_STRING)
                    tb_close_pianyi.Focus()
                    GoTo finish
                End If
                If Val(tb_close_pianyi.Text) > 120 Or Val(tb_close_pianyi.Text) < 0 Then
                    MsgBox("时间设置为0-120之间，请重新输入", , PROJECT_TITLE_STRING)
                    tb_close_pianyi.Focus()
                    Exit Sub
                End If
                If rb_close_early.Checked = True Then
                    close_pianyivalue = -Val(Trim(tb_close_pianyi.Text))
                Else
                    close_pianyivalue = Val(Trim(tb_close_pianyi.Text))
                End If

            Else
                close_pianyivalue = 0

            End If
            i = 0
            While rs.EOF = False

                dgv_week_time.Rows.Add()
                dgv_week_time.Rows(i).Cells(0).Value = "星期" & week_num(nowtime.AddDays(i).DayOfWeek)
                ' dgv_week_time.Rows(i).Cells(0).Value = "星期" & week_num(System.Convert.ToDateTime(rs.Fields("time").Value).DayOfWeek)
                close_time = System.Convert.ToDateTime(rs.Fields("time").Value).AddMinutes(close_pianyivalue)
                Dim tt As Integer = nowtime.AddDays(i).DayOfWeek
                rs.MoveNext()
                If rs.EOF = True Then
                    If rs.State = 1 Then
                        rs.Close()
                        rs = Nothing
                    End If
                    conn.Close()
                    conn = Nothing
                    Exit Sub
                End If
                open_time = System.Convert.ToDateTime(rs.Fields("time").Value).AddMinutes(open_pianyivalue)
                rs.MoveNext()
                dgv_week_time.Rows(i).Cells(1).Value = open_time.TimeOfDay
                dgv_week_time.Rows(i).Cells(2).Value = close_time.TimeOfDay
                i += 1
            End While
            If nowtime.Month <> DateAdd(DateInterval.Day, 7, nowtime).Month Then
                sql = "select * from suntime where day(time)<'" & DateAdd(DateInterval.Day, 7, nowtime).Day & "' and month(time)='" & DateAdd(DateInterval.Day, 7, nowtime).Month & "'  order by id"
                rs = DBOperation.SelectSQL(conn, sql, msg)
                If rs Is Nothing Then
                    MsgBox(MSG_ERROR_STRING & "show_weekset_Click", , PROJECT_TITLE_STRING)
                    conn.Close()
                    conn = Nothing
                    Exit Sub
                End If
                While rs.EOF = False

                    dgv_week_time.Rows.Add()
                    dgv_week_time.Rows(i).Cells(0).Value = "星期" & week_num(nowtime.AddDays(i).DayOfWeek)
                    ' dgv_week_time.Rows(i).Cells(0).Value = "星期" & week_num(System.Convert.ToDateTime(rs.Fields("time").Value).DayOfWeek)
                    close_time = System.Convert.ToDateTime(rs.Fields("time").Value).AddMinutes(close_pianyivalue)

                    rs.MoveNext()
                    If rs.EOF = True Then
                        If rs.State = 1 Then
                            rs.Close()
                            rs = Nothing
                        End If
                        conn.Close()
                        conn = Nothing
                        Exit Sub
                    End If
                    open_time = System.Convert.ToDateTime(rs.Fields("time").Value).AddMinutes(open_pianyivalue)
                    rs.MoveNext()
                    dgv_week_time.Rows(i).Cells(1).Value = open_time.TimeOfDay
                    dgv_week_time.Rows(i).Cells(2).Value = close_time.TimeOfDay
                    i += 1
                End While
            End If


        Else
            '清除列表
            dgv_week_time.Rows.Clear()
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
    ''' 将经纬度+平时时段+节日时段三个级别分别进行下放，下放的时间长度为未来一星期
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_mod_xiaxing_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_mod_xiaxing.Click
        If rb_box_check.Checked = True Then
            If Trim(cb_controlbox_name.Text) = "" Then
                MsgBox("请选择主控箱名称", , PROJECT_TITLE_STRING)
                cb_controlbox_name.Focus()
                Exit Sub
            End If
            '按主控箱名称进行下放
            g_mod_controlboxname = Trim(cb_controlbox_name.Text)
        Else
            g_mod_controlboxname = ""
        End If

        If MsgBox("是否将时段控制模式下行到主控器？", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.No Then
            Exit Sub
        End If

        Com_inf.Insert_Operation("下放时段控制模式")
        g_modtag = 1
        If g_welcomewinobj.BackgroundWorkerModXiaFang.IsBusy = False Then
            g_welcomewinobj.BackgroundWorkerModXiaFang.RunWorkerAsync()
            MsgBox("开始配置操作，结果请查看主控界面上的操作数据面板", , PROJECT_TITLE_STRING)

        Else
            MsgBox("下放线程正在运行，请稍后重试...", , PROJECT_TITLE_STRING)
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub CheckBoxMod_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chb_mod.Click
        If Me.chb_mod.Checked = True Then
            GroupBoxGroup.Enabled = True
            GroupBoxJibie.Enabled = True
        Else
            GroupBoxGroup.Enabled = False
            GroupBoxJibie.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' 修改控制级别
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_setjibie_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_setjibie.Click

        If Trim(cb_jibiestring.Text) = "" Then
            MsgBox("控制级别为空，请选择！", , PROJECT_TITLE_STRING)
            cb_jibiestring.Focus()
            Exit Sub
        End If

        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim i As Integer
        Dim str1 As String

        msg = ""

        Try
            sql = "select * from sysconfig where type='级别'"
            DBOperation.OpenConn(conn)
            rs = DBOperation.SelectSQL(conn, sql, msg)
            If rs Is Nothing Then
                MsgBox(MSG_ERROR_STRING & "setJiBie_Click", , PROJECT_TITLE_STRING)
                conn.Close()
                conn = Nothing
                Exit Sub
            End If
            If rs.RecordCount > 0 Then
                If MsgBox("是否修改系统的控制级别？", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.Yes Then
                    rs.Fields("name").Value = Trim(cb_jibiestring.Text)
                    rs.Update()
                    MsgBox("设置成功！", , PROJECT_TITLE_STRING)
                    g_sysjibiecontrol = Trim(cb_jibiestring.Text)
                End If
            Else
                rs.AddNew()
                rs.Fields("name").Value = Trim(cb_jibiestring.Text)
                rs.Fields("type").Value = "级别"
                rs.Update()
                MsgBox("设置成功！", , PROJECT_TITLE_STRING)
                g_sysjibiecontrol = Trim(cb_jibiestring.Text)
            End If
            i = 0
            str1 = ""
            While i < g_modgroup.Length
                If i = g_modgroup.Length - 1 Then
                    str1 &= g_modgroup(i)
                Else
                    str1 &= g_modgroup(i) & " + "
                End If
                i += 1
            End While
            ' sysconfigstring.Text = "目前系统的控制级别：" & g_sysjibiecontrol & "  控制模式组合：" & str1
            sysconfigstring.Text = "目前系统的控制级别：" & g_sysjibiecontrol
        Catch ex As Exception
            ex.ToString()
        End Try
    End Sub

    ''' <summary>
    ''' 下行时间
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_timexiaxing_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_timexiaxing.Click
        'sendTime()
        'MsgBox("时钟下放成功", , PROJECT_TITLE_STRING)
        If rb_box_check.Checked = True Then
            If Trim(cb_controlbox_name.Text) = "" Then
                MsgBox("请选择主控箱名称", , PROJECT_TITLE_STRING)
                cb_controlbox_name.Focus()
                Exit Sub
            End If
            g_mod_controlboxname = Trim(cb_controlbox_name.Text)
        Else
            g_mod_controlboxname = ""
        End If
        Com_inf.Insert_Operation("下放校时命令")

        If g_welcomewinobj.BackgroundWorkerTimeXiafang.IsBusy = False Then
            g_welcomewinobj.BackgroundWorkerTimeXiafang.RunWorkerAsync()
            MsgBox("开始配置操作，结果请查看主控界面上的操作数据面板", , PROJECT_TITLE_STRING)

        Else
            MsgBox("下放线程正在运行，请稍后重试...", , PROJECT_TITLE_STRING)
        End If
    End Sub

    ''' <summary>
    ''' 修改模式组合
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_setmod_group_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_setmod_group.Click
        If chb_jingweicontrol.Checked = False And chb_specialcontrol.Checked = False And chb_holidaycontrol.Checked = False Then
            MsgBox("请选择模式组合", , PROJECT_TITLE_STRING)
            Exit Sub
        End If

        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim modgroupstring As String '模式组合字符串
        Dim num As Integer = 0 '数组的个数
        Dim str1 As String
        Dim i As Integer
        modgroupstring = ""
        If chb_jingweicontrol.Checked = True Then
            modgroupstring &= "经纬度控制模式 "
            num += 1
        End If
        If chb_specialcontrol.Checked = True Then
            modgroupstring &= "特殊控制模式 "
            num += 1
        End If
        If chb_holidaycontrol.Checked = True Then
            modgroupstring &= "节假日控制模式 "
            num += 1
        End If
        msg = ""

        Try
            sql = "select * from sysconfig where type='组合'"
            DBOperation.OpenConn(conn)
            rs = DBOperation.SelectSQL(conn, sql, msg)
            If rs Is Nothing Then
                MsgBox(MSG_ERROR_STRING & "setModGroup_Click", , PROJECT_TITLE_STRING)
                conn.Close()
                conn = Nothing
                Exit Sub
            End If
            If rs.RecordCount > 0 Then
                If MsgBox("是否修改系统的模式组合？", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.Yes Then
                    rs.Fields("name").Value = modgroupstring
                    rs.Update()
                    MsgBox("设置成功！", , PROJECT_TITLE_STRING)
                    ReDim g_modgroup(num)
                    g_modgroup = Trim(modgroupstring).Split(" ")
                End If
            Else
                rs.AddNew()
                rs.Fields("name").Value = modgroupstring
                rs.Fields("type").Value = "组合"
                rs.Update()
                MsgBox("设置成功！", , PROJECT_TITLE_STRING)
                ReDim g_modgroup(num)
                g_modgroup = Trim(modgroupstring).Split(" ")
            End If
            i = 0
            str1 = ""
            While i < num
                If i = num - 1 Then
                    str1 &= g_modgroup(i)
                Else
                    str1 &= g_modgroup(i) & " + "
                End If
                i += 1
            End While
            sysconfigstring.Text = "目前系统的控制级别：" & g_sysjibiecontrol
            '增加设置记录
            Com_inf.Insert_Operation("设置系统控制级别：" & modgroupstring)

        Catch ex As Exception
            ex.ToString()
        End Try
    End Sub


    Private Sub rb_all_control_list_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_all_control_list.Click
        If rb_all_control_list.Checked = True Then
            all_group_list.Enabled = True
            huilu_group_list.Enabled = False
        End If
    End Sub

    Private Sub rb_huilu_control_list_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_huilu_control_list.Click
        If rb_huilu_control_list.Checked = True Then
            all_group_list.Enabled = False
            huilu_group_list.Enabled = True
        End If
    End Sub

    Private Function get_lampid(ByVal control_box_name As String) As String
        Dim msg As String
        Dim sql As String
        Dim conn As New ADODB.Connection
        Dim rs As New ADODB.Recordset
        Dim control_box_id As String
        Dim lamp_id As String
        get_lampid = ""
        DBOperation.OpenConn(conn)
        msg = ""

        sql = "select control_box_id from control_box where control_box_name='" & control_box_name & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "get_lampid", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Function
        End If
        If rs.RecordCount > 0 Then
            control_box_id = Trim(rs.Fields("control_box_id").Value)
        Else
            control_box_id = ""

        End If

        If rb_k1_list.Checked = False And rb_k2_list.Checked = False And rb_k3_list.Checked = False And rb_k4_list.Checked = False _
        And rb_k5_list.Checked = False And rb_k6_list.Checked = False Then
            MsgBox("请选择接触器编号", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Function
        End If
        If control_box_id = "" Then
            MsgBox("请选择查询的主控箱", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Function
        End If
        lamp_id = control_box_id & "31"
        While lamp_id.Length < LAMP_ID_LEN + 5
            lamp_id &= "0"
        End While
        If rb_k1_list.Checked = True Then
            lamp_id = lamp_id & "1"
        End If
        If rb_k2_list.Checked = True Then
            lamp_id = lamp_id & "2"

        End If
        If rb_k3_list.Checked = True Then
            lamp_id = lamp_id & "3"

        End If
        If rb_k4_list.Checked = True Then
            lamp_id = lamp_id & "4"

        End If
        If rb_k5_list.Checked = True Then
            lamp_id = lamp_id & "5"

        End If
        If rb_k6_list.Checked = True Then
            lamp_id = lamp_id & "6"

        End If
        get_lampid = lamp_id

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing

    End Function

    Private Sub check_inf()
        Dim rs, rs_div As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim lamp_id As String '节点编号
        Dim nowtime As DateTime
        Dim i As Integer = 0
        Dim open_pianyivalue, close_pianyivalue As Integer '开关灯偏移量
        Dim open_time, close_time As DateTime '开灯关灯的时间
        Dim week_num() As String = {"日", "一", "二", "三", "四", "五", "六"} '星期几
        Dim control_box_id As String  '主控箱编号
        Dim mod_name As String  '模式名称
        Dim type_id As String '类型编号
        Dim week_id As Integer = 0
        If tv_box_inf_list.SelectedNode Is Nothing Then
            MsgBox("请选择主控箱", , PROJECT_TITLE_STRING)
            tv_box_inf_list.Focus()
            Exit Sub
        End If
        If tv_box_inf_list.SelectedNode.Level <> 3 Then
            MsgBox("请选择主控箱", , PROJECT_TITLE_STRING)
            tv_box_inf_list.Focus()
            Exit Sub
        End If
        msg = ""
        DBOperation.OpenConn(conn)

        sql = "select control_box_id from control_box where control_box_name='" & Trim(tv_box_inf_list.SelectedNode.Text).Split(" ")(0) & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "check_inf", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs.RecordCount > 0 Then
            control_box_id = Trim(rs.Fields("control_box_id").Value)
        Else
            GoTo finish

        End If
        If rb_huilu_control_list.Checked = True Then
            '如果按照回路进行查询
            dgv_divtime_methodlist.Columns("divtime_diangan").Visible = False
            dgv_divtime_methodlist.Columns("divtime_power").Visible = False
            dgv_sdivtime_methodlist.Columns("Sdivtime_diangan").Visible = False
            dgv_sdivtime_methodlist.Columns("Sdivtime_power").Visible = False

            '按回路设置进行查询

            If rb_k1_list.Checked = False And rb_k2_list.Checked = False And rb_k3_list.Checked = False And rb_k4_list.Checked = False _
            And rb_k5_list.Checked = False And rb_k6_list.Checked = False Then
                MsgBox("请选择接触器编号", , PROJECT_TITLE_STRING)
                conn.Close()
                conn = Nothing
                Exit Sub
            End If
            If control_box_id = "" Then
                MsgBox("请选择查询的主控箱", , PROJECT_TITLE_STRING)
                conn.Close()
                conn = Nothing
                Exit Sub
            End If
            lamp_id = control_box_id & "31"
            While lamp_id.Length < LAMP_ID_LEN + 5
                lamp_id &= "0"
            End While
            If rb_k1_list.Checked = True Then
                lamp_id = lamp_id & "1"
            End If
            If rb_k2_list.Checked = True Then
                lamp_id = lamp_id & "2"

            End If
            If rb_k3_list.Checked = True Then
                lamp_id = lamp_id & "3"

            End If
            If rb_k4_list.Checked = True Then
                lamp_id = lamp_id & "4"

            End If
            If rb_k5_list.Checked = True Then
                lamp_id = lamp_id & "5"

            End If
            If rb_k6_list.Checked = True Then
                lamp_id = lamp_id & "6"

            End If

            '显示未来一周的时刻设置
            GroupBoxWeektime.Text = tv_box_inf_list.SelectedNode.Text & "第" & Val(Mid(lamp_id, 7, LAMP_ID_LEN)).ToString & "号接触器" & "全夜灯模式时刻表"
            dgv_controlbox_weektime.Rows.Clear()
            sql = "select * from pianyi where lamp_id='" & lamp_id & "'"
            rs = DBOperation.SelectSQL(conn, sql, msg)
            If rs Is Nothing Then
                MsgBox(MSG_ERROR_STRING & "check_inf", , PROJECT_TITLE_STRING)
                conn.Close()
                conn = Nothing
            End If
            If rs.RecordCount > 0 Then
                open_pianyivalue = rs.Fields("open_pianyi").Value
                close_pianyivalue = rs.Fields("close_pianyi").Value
            Else
                GoTo next1 '无经纬度开关灯设置
            End If

            nowtime = System.Convert.ToDateTime(Now.Year.ToString & "-" & Now.Month.ToString & "-" & _
          Now.Day.ToString & " 0:0:0")
            dgv_controlbox_weektime.Rows.Clear()  '清空周设置时刻
            If nowtime.Month = DateAdd(DateInterval.Day, 7, nowtime).Month Then
                '一个星期在同一个月内
                sql = "select * from suntime where month(time)='" & nowtime.Month & "' and day(time)>='" & nowtime.Day & "' and day(time)<'" & DateAdd(DateInterval.Day, 7, nowtime).Day & "' order by id"
            Else
                'sql = "select * from suntime where day(time)>='" & nowtime.Day & "' and month(time)='" & nowtime.Month & "' or day(time)<'" & DateAdd(DateInterval.Day, 7, nowtime).Day & "' and month(time)='" & DateAdd(DateInterval.Day, 7, nowtime).Month & "'  order by id"
                sql = "select * from suntime where day(time)>='" & nowtime.Day & "' and month(time)='" & nowtime.Month & "'  order by id"

            End If


            'sql = "select * from suntime where time>='" & nowtime & "' and time<='" & DateAdd(DateInterval.Day, 7, nowtime) & "' order by id"
            rs = DBOperation.SelectSQL(conn, sql, msg)
            If rs Is Nothing Then
                MsgBox(MSG_ERROR_STRING & "check_inf", , PROJECT_TITLE_STRING)
                conn.Close()
                conn = Nothing
                Exit Sub
            End If

            i = 0
            While rs.EOF = False

                dgv_controlbox_weektime.Rows.Add()
                dgv_controlbox_weektime.Rows(i).Cells(0).Value = "星期" & week_num(nowtime.AddDays(i).DayOfWeek)
                close_time = System.Convert.ToDateTime(rs.Fields("time").Value).AddMinutes(close_pianyivalue)
                rs.MoveNext()
                If rs.EOF = True Then
                    If rs.State = 1 Then
                        rs.Close()
                        rs = Nothing
                    End If
                    conn.Close()
                    conn = Nothing
                    Exit Sub
                End If
                open_time = System.Convert.ToDateTime(rs.Fields("time").Value).AddMinutes(open_pianyivalue)
                rs.MoveNext()
                dgv_controlbox_weektime.Rows(i).Cells(1).Value = open_time.TimeOfDay
                dgv_controlbox_weektime.Rows(i).Cells(2).Value = close_time.TimeOfDay
                i += 1
            End While

            If nowtime.Month <> DateAdd(DateInterval.Day, 7, nowtime).Month Then
                sql = "select * from suntime where day(time)<'" & DateAdd(DateInterval.Day, 7, nowtime).Day & "' and month(time)='" & DateAdd(DateInterval.Day, 7, nowtime).Month & "'  order by id"
                rs = DBOperation.SelectSQL(conn, sql, msg)
                If rs Is Nothing Then
                    MsgBox(MSG_ERROR_STRING & "show_weekset_Click", , PROJECT_TITLE_STRING)
                    conn.Close()
                    conn = Nothing
                    Exit Sub
                End If
                While rs.EOF = False

                    dgv_controlbox_weektime.Rows.Add()
                    dgv_controlbox_weektime.Rows(i).Cells(0).Value = "星期" & week_num(nowtime.AddDays(i).DayOfWeek)
                    close_time = System.Convert.ToDateTime(rs.Fields("time").Value).AddMinutes(close_pianyivalue)
                    rs.MoveNext()
                    If rs.EOF = True Then
                        If rs.State = 1 Then
                            rs.Close()
                            rs = Nothing
                        End If
                        conn.Close()
                        conn = Nothing
                        Exit Sub
                    End If
                    open_time = System.Convert.ToDateTime(rs.Fields("time").Value).AddMinutes(open_pianyivalue)
                    rs.MoveNext()
                    dgv_controlbox_weektime.Rows(i).Cells(1).Value = open_time.TimeOfDay
                    dgv_controlbox_weektime.Rows(i).Cells(2).Value = close_time.TimeOfDay
                    i += 1
                End While
            End If
next1:
            '显示对回路的特殊时段控制
            GroupBoxSpecialtime.Text = tv_box_inf_list.SelectedNode.Text & "第" & Val(Mid(lamp_id, 7, LAMP_ID_LEN)).ToString & "号接触器" & "半夜灯及自动模式时刻表"
            dgv_divtime_methodlist.Rows.Clear()  '清除列表

            week_id = 0
            i = 0
            While week_id < 7
                sql = "select div_time_level from road_level where lamp_id='" & lamp_id & "' and week_id='" & week_id & "'"
                rs = DBOperation.SelectSQL(conn, sql, msg)
                If rs Is Nothing Then
                    MsgBox(MSG_ERROR_STRING & "check_inf", , PROJECT_TITLE_STRING)
                    conn.Close()
                    conn = Nothing
                    Exit Sub
                End If
                While rs.EOF = False
                    mod_name = Trim(rs.Fields("div_time_level").Value)
                    sql = "select * from div_time where div_level='" & mod_name & "' order by id "
                    rs_div = DBOperation.SelectSQL(conn, sql, msg)
                    If rs_div Is Nothing Then
                        MsgBox(MSG_ERROR_STRING & "check_inf", , PROJECT_TITLE_STRING)
                        conn.Close()
                        conn = Nothing
                        Exit Sub
                    End If
                    ' i = 0
                    While rs_div.EOF = False
                        dgv_divtime_methodlist.Rows.Add()
                        dgv_divtime_methodlist.Rows(i).Cells("divtime_id").Value = i + 1
                        dgv_divtime_methodlist.Rows(i).Cells("divtime_hour").Value = rs_div.Fields("hour_beg").Value
                        dgv_divtime_methodlist.Rows(i).Cells("divtime_min").Value = rs_div.Fields("min_beg").Value
                        dgv_divtime_methodlist.Rows(i).Cells("divtime_second").Value = rs_div.Fields("second_beg").Value
                        dgv_divtime_methodlist.Rows(i).Cells("divtime_method").Value = Trim(rs_div.Fields("mod").Value)
                        dgv_divtime_methodlist.Rows(i).Cells("divtime_weekid").Value = "星期" & week_num(week_id)
                        dgv_divtime_methodlist.Rows(i).Cells("divtime_diangan").Value = "-"
                        dgv_divtime_methodlist.Rows(i).Cells("divtime_power").Value = "-"
                        rs_div.MoveNext()
                        i += 1
                    End While
                    rs.MoveNext()
                End While
                week_id += 1



            End While


next2:
            '显示对回路的节日控制模式
            GroupBoxHolidaytime.Text = tv_box_inf_list.SelectedNode.Text & "第" & Val(Mid(lamp_id, 7, LAMP_ID_LEN)).ToString & "号接触器" & "节假日模式时刻表"
            dgv_sdivtime_methodlist.Rows.Clear()  '清除列表
            sql = "select div_time_level from Special_road_level where lamp_id='" & lamp_id & "'"
            rs = DBOperation.SelectSQL(conn, sql, msg)
            If rs Is Nothing Then
                MsgBox(MSG_ERROR_STRING & "check_inf", , PROJECT_TITLE_STRING)
                conn.Close()
                conn = Nothing
                Exit Sub
            End If
            If rs.RecordCount > 0 Then
                mod_name = Trim(rs.Fields("div_time_level").Value)
            Else
                GoTo finish
            End If

            sql = "select * from Special_div_time where name='" & mod_name & "' order by id "
            rs = DBOperation.SelectSQL(conn, sql, msg)
            If rs Is Nothing Then
                MsgBox(MSG_ERROR_STRING & "check_inf", , PROJECT_TITLE_STRING)
                conn.Close()
                conn = Nothing
                Exit Sub
            End If
            i = 0
            While rs.EOF = False
                dgv_sdivtime_methodlist.Rows.Add()
                dgv_sdivtime_methodlist.Rows(i).Cells("Sdivtime_id").Value = i + 1
                dgv_sdivtime_methodlist.Rows(i).Cells("Sdivtime_time").Value = rs.Fields("time").Value
                dgv_sdivtime_methodlist.Rows(i).Cells("Sdivtime_method").Value = Trim(rs.Fields("mod").Value)
                dgv_sdivtime_methodlist.Rows(i).Cells("Sdivtime_diangan").Value = "-"
                dgv_sdivtime_methodlist.Rows(i).Cells("Sdivtime_power").Value = "-"
                rs.MoveNext()
                i += 1
            End While


        End If


        '*************************************************************************
        If rb_all_control_list.Checked = True Then
            '按整体控制命令进行查询,显示功率及电感
            dgv_divtime_methodlist.Columns("divtime_diangan").Visible = True
            dgv_divtime_methodlist.Columns("divtime_power").Visible = True
            dgv_sdivtime_methodlist.Columns("Sdivtime_diangan").Visible = True
            dgv_sdivtime_methodlist.Columns("Sdivtime_power").Visible = True

            If Trim(cb_box_list.Text) = "" Then
                MsgBox("请选择整体控制对象", , PROJECT_TITLE_STRING)
                GoTo finish
            End If
            If control_box_id = "" Then
                MsgBox("请选择查询的主控箱", , PROJECT_TITLE_STRING)
                GoTo finish
            End If
            type_id = ""
            'If rb_box_list.Checked = True Then
            '    type_id = "31"
            'Else
            '    type_id = "0"
            'End If

            type_id = Com_inf.Get_Type_id(Trim(cb_box_list.Text))

            '整体命令中不显示未来一周的时刻设置
            GroupBoxWeektime.Text = tv_box_inf_list.SelectedNode.Text & "全夜灯模式时刻表"
            dgv_controlbox_weektime.Rows.Clear()

            '显示对回路的特殊时段控制
            GroupBoxSpecialtime.Text = tv_box_inf_list.SelectedNode.Text & "半夜灯及自动模式时刻表"
            dgv_divtime_methodlist.Rows.Clear()  '清除列表

            week_id = 0
            i = 0
            While week_id < 7
                sql = "select div_time_level from road_level where control_box_id='" & control_box_id & "' and type_id='" & type_id & "' and lamp_id is NULL and week_id='" & week_id & "' "
                rs = DBOperation.SelectSQL(conn, sql, msg)
                If rs Is Nothing Then
                    MsgBox(MSG_ERROR_STRING & "check_inf", , PROJECT_TITLE_STRING)
                    conn.Close()
                    conn = Nothing
                    Exit Sub
                End If
                While rs.EOF = False
                    mod_name = Trim(rs.Fields("div_time_level").Value)
                    sql = "select * from div_time where div_level='" & mod_name & "' order by id "
                    rs_div = DBOperation.SelectSQL(conn, sql, msg)
                    If rs_div Is Nothing Then
                        MsgBox(MSG_ERROR_STRING & "check_inf", , PROJECT_TITLE_STRING)
                        conn.Close()
                        conn = Nothing
                        Exit Sub
                    End If

                    While rs_div.EOF = False
                        dgv_divtime_methodlist.Rows.Add()
                        dgv_divtime_methodlist.Rows(i).Cells("divtime_id").Value = i + 1
                        dgv_divtime_methodlist.Rows(i).Cells("divtime_hour").Value = rs_div.Fields("hour_beg").Value
                        dgv_divtime_methodlist.Rows(i).Cells("divtime_min").Value = rs_div.Fields("min_beg").Value
                        dgv_divtime_methodlist.Rows(i).Cells("divtime_second").Value = rs_div.Fields("second_beg").Value
                        dgv_divtime_methodlist.Rows(i).Cells("divtime_method").Value = Trim(rs_div.Fields("mod").Value)
                        dgv_divtime_methodlist.Rows(i).Cells("divtime_weekid").Value = "星期" & week_num(week_id)
                        dgv_divtime_methodlist.Rows(i).Cells("divtime_diangan").Value = rs_div.Fields("diangan").Value
                        dgv_divtime_methodlist.Rows(i).Cells("divtime_power").Value = rs_div.Fields("gonglv").Value

                        rs_div.MoveNext()
                        i += 1
                    End While
                    rs.MoveNext()
                End While

                week_id += 1
            End While


next3:
            '显示对回路的节日控制模式
            GroupBoxHolidaytime.Text = tv_box_inf_list.SelectedNode.Text & "节假日模式时刻表"
            dgv_sdivtime_methodlist.Rows.Clear()  '清除列表
            sql = "select div_time_level from Special_road_level where control_box_id='" & control_box_id & "' and type_id='" & type_id & "' and lamp_id is NULL"
            rs = DBOperation.SelectSQL(conn, sql, msg)
            If rs Is Nothing Then
                MsgBox(MSG_ERROR_STRING & "check_inf", , PROJECT_TITLE_STRING)
                conn.Close()
                conn = Nothing
                Exit Sub
            End If
            If rs.RecordCount > 0 Then
                mod_name = Trim(rs.Fields("div_time_level").Value)
            Else
                GoTo finish
            End If

            sql = "select * from Special_div_time where name='" & mod_name & "' order by id "
            rs = DBOperation.SelectSQL(conn, sql, msg)
            If rs Is Nothing Then
                MsgBox(MSG_ERROR_STRING & "check_inf", , PROJECT_TITLE_STRING)
                conn.Close()
                conn = Nothing
                Exit Sub
            End If
            i = 0
            While rs.EOF = False
                dgv_sdivtime_methodlist.Rows.Add()
                dgv_sdivtime_methodlist.Rows(i).Cells("Sdivtime_id").Value = i + 1
                dgv_sdivtime_methodlist.Rows(i).Cells("Sdivtime_time").Value = rs.Fields("time").Value
                dgv_sdivtime_methodlist.Rows(i).Cells("Sdivtime_method").Value = Trim(rs.Fields("mod").Value)
                dgv_sdivtime_methodlist.Rows(i).Cells("Sdivtime_diangan").Value = rs.Fields("diangan").Value
                dgv_sdivtime_methodlist.Rows(i).Cells("Sdivtime_power").Value = rs.Fields("gonglv").Value

                rs.MoveNext()
                i += 1
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
    ''' 删除设置的模式
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Del_inf()
        If tv_box_inf_list.SelectedNode Is Nothing Then
            MsgBox("请选择主控箱", , PROJECT_TITLE_STRING)
            tv_box_inf_list.Focus()
            Exit Sub
        End If
        If tv_box_inf_list.SelectedNode.Level <> 3 Then
            MsgBox("请选择主控箱", , PROJECT_TITLE_STRING)
            tv_box_inf_list.Focus()
            Exit Sub
        End If
        If chb_jinweidu.Checked = False And chb_special.Checked = False And chb_holiday.Checked = False Then
            MsgBox("请选择需要删除的控制模式", , PROJECT_TITLE_STRING)
            chb_jinweidu.Focus()
            Exit Sub
        End If

        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim lamp_id As String '节点编号
        Dim i As Integer = 0
        Dim control_box_id As String  '主控箱编号
        Dim type_id As String '类型编号
        Dim type_string As String = "" '类型名称
        Dim mod_string As String = ""  '模式名称

        msg = ""
        DBOperation.OpenConn(conn)

        sql = "select control_box_id from control_box where control_box_name='" & Trim(tv_box_inf_list.SelectedNode.Text).Split(" ")(0) & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "Del_inf", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs.RecordCount > 0 Then
            control_box_id = Trim(rs.Fields("control_box_id").Value)
        Else
            GoTo finish

        End If
        If rb_huilu_control_list.Checked = True Then
            If rb_k1_list.Checked = False And rb_k2_list.Checked = False And rb_k3_list.Checked = False And rb_k4_list.Checked = False _
        And rb_k5_list.Checked = False And rb_k6_list.Checked = False Then
                MsgBox("请选择接触器编号", , PROJECT_TITLE_STRING)
                conn.Close()
                conn = Nothing
                Exit Sub
            End If
            If control_box_id = "" Then
                MsgBox("请选择删除的主控箱", , PROJECT_TITLE_STRING)
                conn.Close()
                conn = Nothing
                Exit Sub
            End If
            '按回路设置进行查询
            lamp_id = control_box_id & "31"
            While lamp_id.Length < LAMP_ID_LEN + 5
                lamp_id &= "0"
            End While
            If rb_k1_list.Checked = True Then

                lamp_id = lamp_id & "1"
            End If
            If rb_k2_list.Checked = True Then
                lamp_id = lamp_id & "2"
            End If
            If rb_k3_list.Checked = True Then
                lamp_id = lamp_id & "3"
            End If
            If rb_k4_list.Checked = True Then
                lamp_id = lamp_id & "4"
            End If
            If rb_k5_list.Checked = True Then
                lamp_id = lamp_id & "5"
            End If
            If rb_k6_list.Checked = True Then
                lamp_id = lamp_id & "6"
            End If

            '清除未来一周的时刻设置
            GroupBoxWeektime.Text = tv_box_inf_list.SelectedNode.Text & "第" & Val(Mid(lamp_id, 7, LAMP_ID_LEN)).ToString & "号接触器" & "全夜灯模式时刻表"

            If chb_jinweidu.Checked = True Then  '删除经纬度
                dgv_controlbox_weektime.Rows.Clear()
                sql = "delete from pianyi where lamp_id='" & lamp_id & "'"
                DBOperation.ExecuteSQL(conn, sql, msg)
                mod_string &= "删除" & Trim(tv_box_inf_list.SelectedNode.Text) & " 接触器K" & Val(Mid(lamp_id, 7, LAMP_ID_LEN)) & "全夜灯模式时刻表；"
            End If

            GroupBoxSpecialtime.Text = tv_box_inf_list.SelectedNode.Text & "第" & Val(Mid(lamp_id, 7, LAMP_ID_LEN)).ToString & "号接触器" & "半夜灯及自动模式时刻表"

            If chb_special.Checked = True Then
                '显示对回路的特殊时段控制
                dgv_divtime_methodlist.Rows.Clear()  '清除列表
                sql = "delete from road_level where lamp_id='" & lamp_id & "'"
                DBOperation.ExecuteSQL(conn, sql, msg)
                mod_string &= "删除" & Trim(tv_box_inf_list.SelectedNode.Text) & " 接触器K" & Val(Mid(lamp_id, 7, LAMP_ID_LEN)) & "半夜灯及自动模式时刻表；"

            End If

            '显示对回路的节日控制模式
            GroupBoxHolidaytime.Text = tv_box_inf_list.SelectedNode.Text & "第" & Val(Mid(lamp_id, 7, LAMP_ID_LEN)).ToString & "号接触器" & "节假日模式时刻表"

            If chb_holiday.Checked = True Then
                dgv_sdivtime_methodlist.Rows.Clear()  '清除列表
                sql = "delete from Special_road_level where lamp_id='" & lamp_id & "'"
                DBOperation.ExecuteSQL(conn, sql, msg)
                mod_string &= "删除" & Trim(tv_box_inf_list.SelectedNode.Text) & " 接触器K" & Val(Mid(lamp_id, 7, LAMP_ID_LEN)) & "节假日模式时刻表"


            End If

            If mod_string <> "" Then
                If mod_string.Length > 100 Then
                    mod_string = Mid(mod_string, 1, 80) & "..."

                End If
                Com_inf.Insert_Operation(mod_string)
            End If

        End If


        '*************************************************************************
        If rb_all_control_list.Checked = True Then
            '按整体控制命令进行查询


            If Trim(cb_box_list.Text) = "" Then
                MsgBox("请选择整体控制对象", , PROJECT_TITLE_STRING)
                GoTo finish
            End If
            If control_box_id = "" Then
                MsgBox("请选择查询的主控箱", , PROJECT_TITLE_STRING)
                GoTo finish
            End If
            type_id = ""
            'If rb_box_list.Checked = True Then
            '    type_id = "31"
            'Else
            '    type_id = "0"
            'End If

            type_id = Com_inf.Get_Type_id(Trim(cb_box_list.Text))

            '整体命令中不显示未来一周的时刻设置
            GroupBoxWeektime.Text = tv_box_inf_list.SelectedNode.Text & "全夜灯模式时刻表"
            dgv_controlbox_weektime.Rows.Clear()

            '显示对回路的特殊时段控制
            GroupBoxSpecialtime.Text = tv_box_inf_list.SelectedNode.Text & "半夜灯及自动模式时刻表"
            dgv_divtime_methodlist.Rows.Clear()  '清除列表
            If chb_special.Checked = True Then
                sql = "delete from road_level where control_box_id='" & control_box_id & "' and type_id='" & type_id & "' and lamp_id is NULL "
                DBOperation.ExecuteSQL(conn, sql, msg)
                mod_string &= "删除" & Trim(tv_box_inf_list.SelectedNode.Text) & " " & type_string & "半夜灯及自动模式时刻表"

            End If

            '显示对回路的节日控制模式
            GroupBoxHolidaytime.Text = tv_box_inf_list.SelectedNode.Text & "节假日模式时刻表"
            dgv_sdivtime_methodlist.Rows.Clear()  '清除列表
            If chb_holiday.Checked = True Then
                sql = "delete from Special_road_level where control_box_id='" & control_box_id & "' and type_id='" & type_id & "' and lamp_id is NULL"
                DBOperation.ExecuteSQL(conn, sql, msg)
                mod_string &= "删除" & Trim(tv_box_inf_list.SelectedNode.Text) & " " & type_string & "节假日模式时刻表"

            End If

            If mod_string <> "" Then
                If mod_string.Length > 100 Then
                    mod_string = Mid(mod_string, 1, 80) & "..."

                End If
                Com_inf.Insert_Operation(mod_string)
            End If
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
    ''' 根据所选择的条件，查询其所对应的各种类型的时刻设置
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_check_list_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_check_list.Click
        If rb_readcheck.Checked = True Then
            '查询
            check_inf()
        End If

        If rb_delcheck.Checked = True Then
            '删除
            Del_inf()
        End If
    End Sub

    Private Sub rb_readcheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_readcheck.Click
        If rb_readcheck.Checked = True Then
            GroupBoxmod.Enabled = False
        End If
    End Sub

    Private Sub rb_delcheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_delcheck.Click
        If rb_delcheck.Checked = True Then
            GroupBoxmod.Enabled = True
        End If
    End Sub


    Private Sub BackgroundWorkerexcel_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorkerexcel.DoWork
        'Me.BackgroundWorkerexcel.ReportProgress(1)  '提示开始excel操作
        Try
            If m_excel_type = 0 Then
                excel()  '导出excel函数
            Else
                month_excel()  '导出excel函数
            End If

        Catch ex As Exception
            ex.ToString()
        End Try
    End Sub

    ''' <summary>
    ''' 开启线程导出日出日落表
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_excel_time_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_excel_time.Click
        If Me.BackgroundWorkerexcel.IsBusy Then
            MsgBox("数据正在导出，请稍候......", , PROJECT_TITLE_STRING)
            Exit Sub
        Else
            progress.Visible = True
            m_excel_type = 0
            Me.BackgroundWorkerexcel.RunWorkerAsync()
        End If
    End Sub

    Private Sub BackgroundWorkerexcel_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorkerexcel.ProgressChanged

        If e.ProgressPercentage >= 0 Then
            progress.Value = e.ProgressPercentage
        End If

    End Sub

    ''' <summary>
    ''' 导出手工控制的excel表
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub month_excel()

        Dim i As Integer = 0
        Dim sql As String
        Dim msg As String
        Dim conn As New ADODB.Connection
        Dim rs, rs_pianyi As New ADODB.Recordset
        Dim open_pianyi, close_pianyi As Integer
        Dim open_time, close_time As DateTime
        Dim lamp_id As String '终端编号
        Dim progress_percentage As Integer
        Dim rowIndex, colIndex As Integer
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        m_xlApp = (New Microsoft.Office.Interop.Excel.Application)

        rowIndex = 1
        colIndex = 0

        m_xlBook = m_xlApp.Workbooks().Add
        m_xlSheet = m_xlBook.Worksheets("sheet1")

        m_xlApp.Cells(1, 1) = Now.Month.ToString & "月开关灯时刻表"
        m_xlApp.Rows(1).RowHeight = 50
        m_xlApp.Rows(1).Font.Size = 18
        m_xlApp.Rows(1).Font.Bold = True

        Dim dt As New DataTable
        m_xlApp.Cells(2, 1) = "日期"
        m_xlApp.Cells(2, 2) = "关灯时间"
        m_xlApp.Cells(2, 3) = "开灯时间"
        m_xlApp.Rows(2).Font.Bold = True
        m_xlApp.Rows(2).font.size = 9
        m_xlApp.Rows(2).RowHeight = 30

        m_row = 3
        open_pianyi = 0
        close_pianyi = 0
        lamp_id = get_lampid(m_selectboxname)

        msg = ""
        DBOperation.OpenConn(conn)
        sql = "select * from pianyi where lamp_id='" & lamp_id & "'"
        rs_pianyi = DBOperation.SelectSQL(conn, sql, msg)
        If rs_pianyi Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "month_excel", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        open_pianyi = rs_pianyi.Fields("open_pianyi").Value
        close_pianyi = rs_pianyi.Fields("close_pianyi").Value

        sql = "select * from suntime where month(time)='" & Now.Month & "' order by id"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "month_excel", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If

        While rs.EOF = False

            close_time = System.Convert.ToDateTime(rs.Fields("time").Value).AddMinutes(close_pianyi)
            rs.MoveNext()
            open_time = System.Convert.ToDateTime(rs.Fields("time").Value).AddMinutes(open_pianyi)
            progress_percentage = i * 100 / rs.RecordCount + 1
            If progress_percentage > 100 Then
                progress_percentage = 100
            End If
            '    Me.BackgroundWorkerexcel.ReportProgress(progress_percentage)
            m_xlApp.Cells(m_row, 1) = i + 1 & "号"
            m_xlApp.Cells(m_row, 2) = close_time
            m_xlApp.Cells(m_row, 3) = open_time

            m_row += 1
            i += 1
            rs.MoveNext()
        End While


        With m_xlSheet
            .Range(.Cells(1, 1), .Cells(1, 3)).Merge()
            .Range(.Cells(2, 1), .Cells(2, 1)).ColumnWidth = 10
            .Range(.Cells(2, 2), .Cells(2, 3)).ColumnWidth = 20

            .Range(.Cells(1, 1), .Cells(m_row - 1, 13)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter


            .Range(.Cells(2, 1), .Cells(1, 3)).Font.Name = "宋体"
            '设标题为宋体字
            .Range(.Cells(2, 1), .Cells(1, 3)).Font.Bold = "True"
            '标题字体加粗
            .Range(.Cells(1, 1), .Cells(m_row - 1, 3)).Borders.LineStyle = 1
            '设表格边框样式
            .Range(.Cells(3, 1), .Cells(m_row - 1, 3)).Font.Size = 9
            '设置表格数据的字号

        End With

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        If rs_pianyi.State = 1 Then
            rs_pianyi.Close()
            rs_pianyi = Nothing
        End If
        conn.Close()
        conn = Nothing

    End Sub


    ''' <summary>
    ''' 导出手工控制的excel表
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub excel()
        Dim rowIndex, colIndex As Integer
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        m_xlApp = (New Microsoft.Office.Interop.Excel.Application)

        rowIndex = 1
        colIndex = 0

        m_xlBook = m_xlApp.Workbooks().Add
        m_xlSheet = m_xlBook.Worksheets("sheet1")

        m_xlApp.Cells(1, 1) = "日出日落时刻表"
        m_xlApp.Rows(1).RowHeight = 50
        m_xlApp.Rows(1).Font.Size = 18
        m_xlApp.Rows(1).Font.Bold = True

        Dim dt As New DataTable
        m_xlApp.Cells(2, 1) = "日期"
        m_xlApp.Cells(2, 2) = "1月"
        m_xlApp.Cells(2, 3) = "2月"
        m_xlApp.Cells(2, 4) = "3月"
        m_xlApp.Cells(2, 5) = "4月"
        m_xlApp.Cells(2, 6) = "5月"
        m_xlApp.Cells(2, 7) = "6月"
        m_xlApp.Cells(2, 8) = "7月"
        m_xlApp.Cells(2, 9) = "8月"
        m_xlApp.Cells(2, 10) = "9月"
        m_xlApp.Cells(2, 11) = "10月"
        m_xlApp.Cells(2, 12) = "11月"
        m_xlApp.Cells(2, 13) = "12月"
        m_xlApp.Rows(2).Font.Bold = True
        m_xlApp.Rows(2).font.size = 9
        m_xlApp.Rows(2).RowHeight = 30

        m_row = 3
        Dim i As Integer = 0


        Dim row_count As Integer
        row_count = dgv_suninflist.RowCount
        Dim progress_percentage As Integer

        While i < row_count
            progress_percentage = i * 100 / row_count + 1
            If progress_percentage > 100 Then
                progress_percentage = 100
            End If
            'Me.BackgroundWorkerexcel.ReportProgress(progress_percentage)
            If i Mod 2 = 0 Then
                m_xlApp.Cells(m_row, 1) = (i \ 2 + 1).ToString & "日日出时刻"
            Else
                m_xlApp.Cells(m_row, 1) = (i \ 2 + 1).ToString & "日日落时刻"
            End If
            m_xlApp.Cells(m_row, 2) = System.Convert.ToString(dgv_suninflist.Rows(i).Cells("Jan").Value)
            m_xlApp.Cells(m_row, 3) = System.Convert.ToString(dgv_suninflist.Rows(i).Cells("Feb").Value)
            m_xlApp.Cells(m_row, 4) = System.Convert.ToString(dgv_suninflist.Rows(i).Cells("Mar").Value)
            m_xlApp.Cells(m_row, 5) = System.Convert.ToString(dgv_suninflist.Rows(i).Cells("Apr").Value)
            m_xlApp.Cells(m_row, 6) = System.Convert.ToString(dgv_suninflist.Rows(i).Cells("May").Value)
            m_xlApp.Cells(m_row, 7) = System.Convert.ToString(dgv_suninflist.Rows(i).Cells("Jun").Value)
            m_xlApp.Cells(m_row, 8) = System.Convert.ToString(dgv_suninflist.Rows(i).Cells("July").Value)
            m_xlApp.Cells(m_row, 9) = System.Convert.ToString(dgv_suninflist.Rows(i).Cells("Aug").Value)
            m_xlApp.Cells(m_row, 10) = System.Convert.ToString(dgv_suninflist.Rows(i).Cells("Sup").Value)
            m_xlApp.Cells(m_row, 11) = System.Convert.ToString(dgv_suninflist.Rows(i).Cells("Oct").Value)
            m_xlApp.Cells(m_row, 12) = System.Convert.ToString(dgv_suninflist.Rows(i).Cells("Nov").Value)
            m_xlApp.Cells(m_row, 13) = System.Convert.ToString(dgv_suninflist.Rows(i).Cells("Dec").Value)

            m_row += 1
            i = i + 1

        End While

        With m_xlSheet
            .Range(.Cells(1, 1), .Cells(1, 13)).Merge()
            .Range(.Cells(2, 1), .Cells(2, 1)).ColumnWidth = 20
            .Range(.Cells(2, 2), .Cells(2, 13)).ColumnWidth = 7


            .Range(.Cells(1, 1), .Cells(m_row - 1, 13)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter


            .Range(.Cells(2, 1), .Cells(1, 13)).Font.Name = "宋体"
            '设标题为宋体字
            .Range(.Cells(2, 1), .Cells(1, 13)).Font.Bold = "True"
            '标题字体加粗
            .Range(.Cells(1, 1), .Cells(m_row - 1, 13)).Borders.LineStyle = 1
            '设表格边框样式
            .Range(.Cells(3, 1), .Cells(m_row - 1, 13)).Font.Size = 9
            '设置表格数据的字号

        End With

    End Sub

    Private Sub BackgroundWorkerexcel_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorkerexcel.RunWorkerCompleted
        m_xlApp.Visible = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        progress.Visible = False
    End Sub

    Private Sub 经纬度_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If Me.BackgroundWorkerexcel.IsBusy = True Then
            Me.BackgroundWorkerexcel.CancelAsync()
        End If
        ProcessKill(m_xlApp, m_xlBook, m_xlSheet)  '关闭excel进程
        g_windowclose = 1
    End Sub

    ''' <summary>
    ''' 设置周一控制
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chb_monday_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chb_monday.Click
        If chb_monday.Checked = True Then
            m_week(1) = 1
        Else
            m_week(1) = 0
        End If
    End Sub

    ''' <summary>
    ''' 设置周二控制
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chb_tuesday_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chb_tuesday.Click
        If chb_tuesday.Checked = True Then
            m_week(2) = 1
        Else
            m_week(2) = 0
        End If
    End Sub

    ''' <summary>
    ''' 设置周三控制
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chb_wendnesday_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chb_wendnesday.Click
        If chb_wendnesday.Checked = True Then
            m_week(3) = 1
        Else
            m_week(3) = 0
        End If
    End Sub

    ''' <summary>
    ''' 设置周四控制
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chb_thursday_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chb_thursday.Click
        If chb_thursday.Checked = True Then
            m_week(4) = 1
        Else
            m_week(4) = 0
        End If
    End Sub

    ''' <summary>
    ''' 设置周五控制
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chb_friday_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chb_friday.Click
        If chb_friday.Checked = True Then
            m_week(5) = 1
        Else
            m_week(5) = 0
        End If
    End Sub

    ''' <summary>
    ''' 设置周六控制
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chb_saturday_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chb_saturday.Click
        If chb_saturday.Checked = True Then
            m_week(6) = 1
        Else
            m_week(6) = 0
        End If
    End Sub

    ''' <summary>
    ''' 设置周日控制
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Sunday_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chb_sunday.Click
        If chb_sunday.Checked = True Then
            m_week(0) = 1
        Else
            m_week(0) = 0
        End If
    End Sub

    Private Sub cb_mod_value_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_mod_value.DropDown
        If rb_old_controlmethod.Checked = True Then
            '使用传统的控制方法
            cb_mod_value.Items.Clear()
            cb_mod_value.Items.Add("类型全开")
            cb_mod_value.Items.Add("类型全闭")
            cb_mod_value.Items.Add("类型奇开")
            cb_mod_value.Items.Add("类型偶开")
            cb_mod_value.Items.Add("回路开")
            cb_mod_value.Items.Add("回路关")

        Else
            If rb_group3_controlmethod.Checked = True Then
                '使用三回路组合控制方法
                cb_mod_value.Items.Clear()
                Dim rs As New ADODB.Recordset
                Dim conn As New ADODB.Connection
                Dim msg As String
                Dim sql As String
                msg = ""
                DBOperation.OpenConn(conn)
                sql = "select grouporder_name from group_order where grouporder_type=" & g_lampnum & " order by id"
                rs = DBOperation.SelectSQL(conn, sql, msg)
                If rs Is Nothing Then
                    MsgBox(MSG_ERROR_STRING & "mod_value_DropDown", , PROJECT_TITLE_STRING)
                    conn.Close()
                    conn = Nothing
                    Exit Sub
                End If

                While rs.EOF = False
                    cb_mod_value.Items.Add(Trim(rs.Fields("grouporder_name").Value))
                    rs.MoveNext()
                End While
                If rs.State = 1 Then
                    rs.Close()
                    rs = Nothing
                End If
                conn.Close()
                conn = Nothing
            End If

        End If
    End Sub

    Private Sub cb_holidaymod_value_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_holidaymod_value.DropDown
        If rb_holidayold_controlmethod.Checked = True Then
            '使用传统的控制方法
            cb_holidaymod_value.Items.Clear()
            cb_holidaymod_value.Items.Add("类型全开")
            cb_holidaymod_value.Items.Add("类型全闭")
            cb_holidaymod_value.Items.Add("类型奇开")
            cb_holidaymod_value.Items.Add("类型偶开")
            cb_holidaymod_value.Items.Add("回路开")
            cb_holidaymod_value.Items.Add("回路关")

        Else
            If rb_holiday_group3_controlmethod.Checked = True Then
                '使用三回路组合控制方法
                cb_holidaymod_value.Items.Clear()
                Dim rs As New ADODB.Recordset
                Dim conn As New ADODB.Connection
                Dim msg As String
                Dim sql As String
                msg = ""
                DBOperation.OpenConn(conn)
                sql = "select grouporder_name from group_order where grouporder_type=" & g_lampnum & " order by id"
                rs = DBOperation.SelectSQL(conn, sql, msg)
                If rs Is Nothing Then
                    MsgBox(MSG_ERROR_STRING & "holidaymod_value_DropDown", , PROJECT_TITLE_STRING)
                    conn.Close()
                    conn = Nothing
                    Exit Sub
                End If

                While rs.EOF = False
                    cb_holidaymod_value.Items.Add(Trim(rs.Fields("grouporder_name").Value))
                    rs.MoveNext()
                End While
                If rs.State = 1 Then
                    rs.Close()
                    rs = Nothing
                End If
                conn.Close()
                conn = Nothing
            End If

        End If
    End Sub

    ''' <summary>
    ''' 选择需要进行下放的主控箱名称
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_controlbox_name_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_controlbox_name.DropDown
        Com_inf.Select_box_name(cb_controlbox_name)
    End Sub

    ''' <summary>
    ''' 选择所有的电控箱，则是主控箱名称的下拉框不可用
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rb_checkall_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_checkall.Click
        If rb_checkall.Checked = True Then
            cb_controlbox_name.Text = ""
            cb_controlbox_name.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' 选择电控箱名称，则主控箱名称的下拉框设为可用
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rb_box_check_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_box_check.Click
        If rb_box_check.Checked = True Then
            cb_controlbox_name.Enabled = True
        End If
    End Sub

    Private Sub bt_mod_clear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_mod_clear.Click
        If rb_box_check.Checked = True Then
            If Trim(cb_controlbox_name.Text) = "" Then
                MsgBox("请选择主控箱名称", , PROJECT_TITLE_STRING)
                cb_controlbox_name.Focus()
                Exit Sub
            End If
            '按主控箱名称进行下放
            g_mod_controlboxname = Trim(cb_controlbox_name.Text)
        Else
            g_mod_controlboxname = ""
        End If

        If MsgBox("是否清空主控器的时段控制模式？", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.No Then
            Exit Sub
        End If

        Com_inf.Insert_Operation("清空时段控制模式")
        g_modtag = 5  '清空时段控制模式
        g_clearmod = True
        If g_welcomewinobj.BackgroundWorkerModXiaFang.IsBusy = False Then
            g_welcomewinobj.BackgroundWorkerModXiaFang.RunWorkerAsync()
            MsgBox("开始配置操作，结果请查看主控界面上的操作数据面板", , PROJECT_TITLE_STRING)

        Else
            MsgBox("下放线程正在运行，请稍后重试...", , PROJECT_TITLE_STRING)
        End If
    End Sub

    Private Sub tv_all_controlbox_AfterCheck(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tv_all_controlbox.AfterCheck
        Dim controlboxobj As New control_box
        If check = False Then
            check = controlboxobj.setChild(e.Node)
        End If

        controlboxobj.setparent(e.Node)
        check = False

        'Dim i As Integer = 0
        'If e.Node.Level = 3 And e.Node.Checked = True Then
        '    While i < checklist.Count
        '        If checklist(i) = e.Node.Text Then
        '            Exit While
        '        End If
        '        i += 1
        '    End While
        '    If i >= checklist.Count Then
        '        checklist.Add(e.Node.Text)
        '    End If
        'End If


    End Sub


    Private Sub tv_divtime_controlbox_AfterCheck(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tv_divtime_controlbox.AfterCheck
        Dim controlboxobj As New control_box

        If check = False Then
            check = controlboxobj.setChild(e.Node)
        End If

        controlboxobj.setparent(e.Node)
        check = False

    End Sub

    ''' <summary>
    ''' 模式修改后更新列表的内容
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_divmod_list_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_divmod_list.SelectedIndexChanged
        If chb_show_modcontent.Checked = True Then
            If cb_divmod_list.Text = "" Then
                MsgBox("请选择控制模式的名称", , PROJECT_TITLE_STRING)
                cb_divmod_list.Focus()
                Exit Sub
            End If
            select_modvalue(Me.dgv_add_modlist, Trim(cb_divmod_list.Text), 1)

        Else
            Me.dgv_add_modlist.Rows.Clear()   '清空控制模式的数据
        End If
    End Sub


    Private Sub rb_banye_add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_banye_add.Click
        If rb_banye_add.Checked = True Then
            '增加控制模式
            Me.tb_banye_modname.Visible = True
            Me.cb_banye_editname.Visible = False
            '  Me.GroupBox_banye_control.Enabled = True
            GroupBoxbanyecontrol.Enabled = True
            bt_inputbanye.Text = "输入控制模式"
            dgv_banyelist.Rows.Clear()
        End If
    End Sub

    Private Sub rb_banye_edit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_banye_edit.Click
        If rb_banye_edit.Checked = True Then
            '编辑控制模式
            Me.tb_banye_modname.Visible = False
            Me.cb_banye_editname.Visible = True
            '  Me.GroupBox_banye_control.Enabled = True
            GroupBoxbanyecontrol.Enabled = True

            bt_inputbanye.Text = "编辑控制模式"
            chb_show_modcontent.Enabled = True
        End If
    End Sub

    Private Sub rb_banye_del_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_banye_del.Click
        If rb_banye_del.Checked = True Then
            '删除控制模式
            Me.tb_banye_modname.Visible = False
            Me.cb_banye_editname.Visible = True
            ' Me.GroupBox_banye_control.Enabled = True
            GroupBoxbanyecontrol.Enabled = False

            bt_inputbanye.Text = "删除控制模式"
            chb_show_modcontent.Enabled = True
        End If
    End Sub

    Private Sub bt_inputbanye_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_inputbanye.Click
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String
        Dim i As Integer
        Dim div_time_obj As New div_time_class
        msg = ""
        i = 0
        DBOperation.OpenConn(conn)

        '增加控制模式
        If Me.rb_banye_add.Checked = True Then
            If Me.dgv_banyelist.RowCount <= 0 Then
                MsgBox("控制名称列表为空", , PROJECT_TITLE_STRING)
                Exit Sub
            End If

            If Me.tb_banye_modname.Text = "" Then
                MsgBox("请输入控制模式名称", , PROJECT_TITLE_STRING)
                Me.tb_banye_modname.Focus()
                Exit Sub
            End If
            If tb_banye_modname.TextLength > 10 Then
                MsgBox("控制模式名称长度大于10", , PROJECT_TITLE_STRING)
                Me.tb_banye_modname.Focus()
                Exit Sub
            End If

            sql = "select * from div_time where div_level='" & Trim(Me.tb_banye_modname.Text) & "' and hour_end=0"
            rs = DBOperation.SelectSQL(conn, sql, msg)
            If rs Is Nothing Then
                MsgBox(MSG_ERROR_STRING & "bt_inputbanye_Click", , PROJECT_TITLE_STRING)
                conn.Close()
                conn = Nothing
                Exit Sub
            End If

            If rs.RecordCount > 0 Then
                '如果原来的数据库中该控制模式下存在时段模式，则表示询问是否编辑
                If MsgBox("该控制模式的配置已经存在，是否重新输入？", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.Yes Then
                    sql = "delete from div_time where div_level='" & Trim(tb_banye_modname.Text) & "'"
                    DBOperation.ExecuteSQL(conn, sql, msg)
                Else
                    GoTo finish

                End If

            End If
            While i < Me.dgv_banyelist.RowCount
                rs.AddNew()
                rs.Fields("id").Value = Me.dgv_banyelist.Rows(i).Cells("banye_id").Value
                rs.Fields("hour_beg").Value = Trim(Me.dgv_banyelist.Rows(i).Cells("banye_hour").Value)
                rs.Fields("min_beg").Value = Trim(Me.dgv_banyelist.Rows(i).Cells("banye_min").Value)
                rs.Fields("second_beg").Value = Trim(Me.dgv_banyelist.Rows(i).Cells("banye_second").Value)
                rs.Fields("mod").Value = Trim(Me.dgv_banyelist.Rows(i).Cells("banye_controlmethod").Value)
                rs.Fields("gonglv").Value = 100
                rs.Fields("diangan").Value = Trim(Me.cb_banye_gonglv.Text)  '电感
                rs.Fields("div_level").Value = Trim(Me.tb_banye_modname.Text)
                rs.Fields("hour_end").Value = "0" '半夜灯模式
                rs.Update()
                i += 1
            End While
            MsgBox("控制模式添加成功", , PROJECT_TITLE_STRING)
            '增加设置记录
            Com_inf.Insert_Operation("添加半夜控制模式：" & Trim(tb_mod_name.Text))

        End If

        '编辑控制模式
        If Me.rb_banye_edit.Checked = True Then
            If Me.dgv_banyelist.RowCount <= 0 Then
                MsgBox("控制模式列表为空", , PROJECT_TITLE_STRING)
                Exit Sub
            End If

            If Me.cb_banye_editname.Text = "" Then
                MsgBox("请选择控制模式名称", , PROJECT_TITLE_STRING)
                Me.cb_banye_editname.Focus()
                Exit Sub
            End If
            sql = "select * from div_time where div_level='" & Trim(Me.cb_banye_editname.Text) & "'"
            rs = DBOperation.SelectSQL(conn, sql, msg)
            If rs Is Nothing Then
                MsgBox(MSG_ERROR_STRING & "input_hms_Click", , PROJECT_TITLE_STRING)
                conn.Close()
                conn = Nothing
                Exit Sub
            End If

            If rs.RecordCount > 0 Then
                sql = "delete from div_time where div_level='" & Trim(cb_banye_editname.Text) & "' and hour_end=0"
                DBOperation.ExecuteSQL(conn, sql, msg)
                While i < Me.dgv_banyelist.RowCount
                    rs.AddNew()
                    rs.Fields("id").Value = Me.dgv_banyelist.Rows(i).Cells("banye_id").Value
                    rs.Fields("hour_beg").Value = Trim(Me.dgv_banyelist.Rows(i).Cells("banye_hour").Value)
                    rs.Fields("min_beg").Value = Trim(Me.dgv_banyelist.Rows(i).Cells("banye_min").Value)
                    rs.Fields("second_beg").Value = Trim(Me.dgv_banyelist.Rows(i).Cells("banye_second").Value)
                    rs.Fields("mod").Value = Trim(Me.dgv_banyelist.Rows(i).Cells("banye_controlmethod").Value)
                    rs.Fields("gonglv").Value = 100
                    rs.Fields("diangan").Value = Trim(cb_banye_gonglv.Text)  '电感
                    rs.Fields("div_level").Value = Trim(cb_banye_editname.Text)
                    rs.Fields("hour_end").Value = "0" '半夜灯模式
                    rs.Update()
                    i += 1
                End While
            Else
                MsgBox("不存在该控制模式名称，请重新选择", , PROJECT_TITLE_STRING)
                Me.cb_banye_editname.Focus()
                GoTo finish
            End If
            MsgBox("控制模式编辑成功", , PROJECT_TITLE_STRING)
            '增加设置记录
            Com_inf.Insert_Operation("编辑特殊控制模式：" & Trim(cb_banye_editname.Text))

        End If

        '删除控制模式
        If Me.rb_banye_del.Checked = True Then
            If cb_banye_editname.Text = "" Then
                MsgBox("请选择控制模式名称", , PROJECT_TITLE_STRING)
                Me.cb_banye_editname.Focus()
                Exit Sub
            End If
            sql = "select * from div_time where div_level='" & Trim(Me.cb_banye_editname.Text) & "'"
            rs = DBOperation.SelectSQL(conn, sql, msg)
            If rs Is Nothing Then
                MsgBox(MSG_ERROR_STRING & "input_hms_Click", , PROJECT_TITLE_STRING)
                conn.Close()
                conn = Nothing
                Exit Sub
            End If

            If rs.RecordCount > 0 Then
                sql = "delete from div_time where div_level='" & Trim(cb_banye_editname.Text) & "' and hour_end=0"
                DBOperation.ExecuteSQL(conn, sql, msg)

                '将road_level表中的数据删除
                sql = "delete from road_level where div_time_level='" & Trim(cb_banye_editname.Text) & "' and street_id=0"
                DBOperation.ExecuteSQL(conn, sql, msg)

            Else
                MsgBox("不存在该控制模式名称，请重新选择", , PROJECT_TITLE_STRING)
                Me.cb_banye_editname.Focus()
                GoTo finish
            End If
            cb_banye_editname.Items.Clear()
            MsgBox("控制模式删除成功", , PROJECT_TITLE_STRING)
            '增加设置记录
            Com_inf.Insert_Operation("删除特殊控制模式：" & Trim(cb_banye_editname.Text))

        End If

        '刷新时段控制的名称
        g_welcomewinobj.init_div_list()
        g_welcomewinobj.init_special_div_list()
        div_time_obj.Div_time_show()

        ''经纬度开关重新设置后，将立即下放当天的时间控制
        'Dim control_boxobj As New control_box
        'control_boxobj.ModXiaFang()


finish:

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub


    Private Sub cb_banye_mod_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_banye_mod.DropDown
        Com_inf.get_divtime_name(cb_banye_mod, 0)
    End Sub

    Private Sub tv_banye_controlbox_AfterCheck(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tv_banye_controlbox.AfterCheck
        Dim controlboxobj As New control_box

        If check = False Then
            check = controlboxobj.setChild(e.Node)
        End If

        controlboxobj.setparent(e.Node)
        check = False
    End Sub


    Private Sub bt_banye_divmod_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_banye_divmod.Click
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String
        Dim control_box_id() As String  '主控箱的编号
        Dim control_box_name() As String '主控箱名称
        Dim lamp_type As Integer  '整体控制的时候控制的类型
        Dim box_num As Integer  '主控箱的个数
        Dim lamp_id(5) As Boolean '交流接触器是否被选中
        Dim i As Integer = 0
        Dim week_id As Integer = 0  '星期
        Dim mod_string As String = "" '编辑的模式字符串
        Dim week() As String = {"日", "一", "二", "三", "四", "五", "六"}

        msg = ""
        lamp_type = 31
        DBOperation.OpenConn(conn)

        checklist.Clear() '清除所有选中的项目
        Dim tnRet As New TreeNode
        For Each treenode As TreeNode In tv_banye_controlbox.Nodes
            FindNode(treenode, checklist)
        Next
        If checklist.Count = 0 Then
            MsgBox("请选择需设置的主控箱名称", , PROJECT_TITLE_STRING)
            Exit Sub
        End If

        box_num = checklist.Count   '选中的电控箱的个数

        ReDim control_box_id(box_num)
        ReDim control_box_name(box_num)
        i = 0
        While i < box_num
            sql = "select control_box_id from control_box where control_box_name='" & Trim(checklist(i)) & "'"

            rs = DBOperation.SelectSQL(conn, sql, msg)
            If rs Is Nothing Then
                MsgBox(MSG_ERROR_STRING & "set_divmod_Click", , PROJECT_TITLE_STRING)
                conn.Close()
                conn = Nothing
                Exit Sub
            End If
            If rs.RecordCount > 0 Then
                control_box_id(i) = Trim(rs.Fields("control_box_id").Value)
                control_box_name(i) = Trim(checklist(i))
            End If
            i += 1
        End While


        i = 0
        

        '对所选择的主控箱的各个交流接触器进行单独控制
        If chb_banye_K1.Checked = False And chb_banye_K2.Checked = False And chb_banye_K3.Checked = False And _
        chb_banye_K4.Checked = False And chb_banye_K5.Checked = False And chb_banye_K6.Checked = False Then
            MsgBox("请选择需要设置的接触器编号", , PROJECT_TITLE_STRING)
            GoTo finish
        End If
        If cb_banye_mod.Text = "" Then
            MsgBox("请选择模式名称", , PROJECT_TITLE_STRING)
            cb_divmod_list.Focus()
            GoTo finish
        End If
        If chb_banye_K1.Checked = True Then
            lamp_id(0) = True
        Else
            lamp_id(0) = False
        End If
        If chb_banye_K2.Checked = True Then
            lamp_id(1) = True
        Else
            lamp_id(1) = False
        End If
        If chb_banye_K3.Checked = True Then
            lamp_id(2) = True
        Else
            lamp_id(2) = False
        End If
        If chb_banye_K4.Checked = True Then
            lamp_id(3) = True
        Else
            lamp_id(3) = False
        End If
        If chb_banye_K5.Checked = True Then
            lamp_id(4) = True
        Else
            lamp_id(4) = False
        End If
        If chb_banye_K6.Checked = True Then
            lamp_id(5) = True
        Else
            lamp_id(5) = False
        End If

        i = 0
        Dim j As Integer = 0
        Dim id_string As String '记录设置的接触器编号
        week_id = 0
        While week_id < 7
            If m_week(week_id) = 1 Then
                i = 0
                j = 0
                While i < box_num
                    j = 0
                    While j <= 5
                        If lamp_id(j) = True Then
                            '表示选中了，需要进行交流接触器的设置
                            id_string = control_box_id(i) & "31"
                            While id_string.Length < LAMP_ID_LEN + 5
                                id_string = id_string & "0"
                            End While
                            id_string = id_string & (j + 1).ToString
                            sql = "select * from road_level where control_box_id='" & control_box_id(i) & "' and lamp_id='" & id_string & "' and week_id='" & week_id & "' and street_id=0"
                            rs = DBOperation.SelectSQL(conn, sql, msg)
                            If rs Is Nothing Then
                                MsgBox(MSG_ERROR_STRING & "set_divmod_Click", , PROJECT_TITLE_STRING)
                                conn.Close()
                                conn = Nothing
                                Exit Sub
                            End If
                            If rs.RecordCount > 0 Then
                                '  If MsgBox("主控箱" & control_box_name(i) & " K" & j + 1 & "接触器，星期" & week(week_id) & "的控制模式已添加，是否覆盖原有设置", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.Yes Then
                                sql = "delete from road_level where control_box_id='" & control_box_id(i) & "' and lamp_id='" & id_string & "' and week_id='" & week_id & "' and street_id=0"
                                DBOperation.ExecuteSQL(conn, sql, msg)

                                '表示这一天需要设置该模式
                                sql = "insert into road_level(control_box_id, div_time_level,type_id,lamp_id,week_id,street_id) values('" & control_box_id(i) & "','" & Trim(cb_banye_mod.Text) & "',31,'" & id_string & "','" & week_id & "',0)"
                                DBOperation.ExecuteSQL(conn, sql, msg)
                                mod_string &= control_box_name(i) & " 接触器：K" & Val(Mid(id_string, 7, LAMP_ID_LEN)) & " ,模式：" & Trim(cb_banye_mod.Text) & " ,星期" & week(week_id) & "; "



                                'Else
                                '    GoTo finish
                                ' End If

                            Else
                                '表示这一天需要设置该模式
                                sql = "insert into road_level(control_box_id, div_time_level,type_id,lamp_id,week_id,street_id) values('" & control_box_id(i) & "','" & Trim(cb_banye_mod.Text) & "',31,'" & id_string & "','" & week_id & "',0)"
                                DBOperation.ExecuteSQL(conn, sql, msg)
                                mod_string &= control_box_name(i) & " 接触器：K" & Val(Mid(id_string, 7, LAMP_ID_LEN)) & " ,模式：" & Trim(cb_banye_mod.Text) & " ,星期" & week(week_id) & "; "


                            End If

                        End If
                        j += 1
                    End While
                    i += 1
                End While
            End If
            week_id += 1
        End While
        '增加设置记录
        If mod_string <> "" Then
            If mod_string.Length > 80 Then
                mod_string = Mid(mod_string, 1, 80) & "..."
            End If
            Com_inf.Insert_Operation("设置控制模式：" & mod_string)

        End If


        MsgBox("控制模式设置成功", , PROJECT_TITLE_STRING)



        ''经纬度开关重新设置后，将立即下放当天的时间控制
        'Dim control_boxobj As New control_box
        'control_boxobj.ModXiaFang()

finish:


        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    Private Sub cb_banye_mod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_banye_mod.SelectedIndexChanged
        If chb_show_modcontent.Checked = True Then
            If cb_banye_mod.Text = "" Then
                MsgBox("请选择控制模式的名称", , PROJECT_TITLE_STRING)
                cb_banye_mod.Focus()
                Exit Sub
            End If
            select_modvalue(Me.dgv_banyelist, Trim(cb_banye_mod.Text), 0)

        Else
            Me.dgv_banyelist.Rows.Clear()   '清空控制模式的数据
        End If
    End Sub

    Private Sub bt_banye_add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_banye_add.Click
        If Me.cb_banyehourbeg.Text = "" Then
            MsgBox("开始时间（小时）不可以为空", , PROJECT_TITLE_STRING)
            Me.cb_banyehourbeg.Focus()
            Exit Sub
        End If
        If Me.cb_banyeminbeg.Text = "" Then
            MsgBox("开始时间（分钟）不可以为空", , PROJECT_TITLE_STRING)
            Me.cb_banyeminbeg.Focus()
            Exit Sub
        End If

        If Me.cb_banyesecondbeg.Text = "" Then
            MsgBox("开始时间（秒）不可以为空", , PROJECT_TITLE_STRING)
            Me.cb_banyesecondbeg.Focus()
            Exit Sub
        End If

        If cb_banye_controlmethod.Text = "" Then
            MsgBox("模式不可以为空", , PROJECT_TITLE_STRING)
            Me.cb_banye_controlmethod.Focus()
            Exit Sub
        End If

        Me.dgv_banyelist.Rows.Add()
        m_id = Me.dgv_banyelist.RowCount   '行数
        Me.dgv_banyelist.Rows(m_id - 1).Cells("banye_id").Value = m_id
        Me.dgv_banyelist.Rows(m_id - 1).Cells("banye_hour").Value = Trim(cb_banyehourbeg.Text)
        Me.dgv_banyelist.Rows(m_id - 1).Cells("banye_min").Value = Trim(cb_banyeminbeg.Text)
        Me.dgv_banyelist.Rows(m_id - 1).Cells("banye_second").Value = Trim(cb_banyesecondbeg.Text)
        Me.dgv_banyelist.Rows(m_id - 1).Cells("banye_controlmethod").Value = Trim(cb_banye_controlmethod.Text)


        m_id += 1  '时段编号增加1
    End Sub

    Private Sub cb_banye_editname_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_banye_editname.DropDown
        Com_inf.get_divtime_name(cb_banye_editname, 0)
    End Sub

    Private Sub cb_banye_editname_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_banye_editname.SelectedIndexChanged
        If chb_show_modcontent.Checked = True Then
            If cb_banye_editname.Text = "" Then
                MsgBox("请选择控制模式的名称", , PROJECT_TITLE_STRING)
                cb_banye_editname.Focus()
                Exit Sub
            End If
            select_modvalue(Me.dgv_banyelist, Trim(cb_banye_editname.Text), 0)

        Else
            Me.dgv_banyelist.Rows.Clear()   '清空控制模式的数据
        End If
    End Sub

    Private Sub tv_holidaydivtime_box_AfterCheck(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tv_holidaydivtime_box.AfterCheck
        Dim controlboxobj As New control_box

        If check = False Then
            check = controlboxobj.setChild(e.Node)
        End If

        controlboxobj.setparent(e.Node)
        check = False
    End Sub

    Private Sub cb_set_holidaydivmod_list_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_set_holidaydivmod_list.SelectedIndexChanged
        If chb_show_holidaymodcontent.Checked = True Then
            If cb_set_holidaydivmod_list.Text = "" Then
                MsgBox("请选择控制模式的名称", , PROJECT_TITLE_STRING)
                cb_set_holidaydivmod_list.Focus()
                Exit Sub
            End If
            select_holiday_modvalue(Me.dgv_add_holidaymodlist, Trim(cb_set_holidaydivmod_list.Text))

        Else
            Me.dgv_add_holidaymodlist.Rows.Clear()   '清空控制模式的数据
        End If
    End Sub

    ''' <summary>
    ''' 获取查询的节点的当月的开关灯时间
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub get_monthreport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles get_monthreport.Click
        If Me.BackgroundWorkerexcel.IsBusy Then
            MsgBox("数据正在导出，请稍候......", , PROJECT_TITLE_STRING)
            Exit Sub
        Else
         
            If tv_box_inf_list.SelectedNode IsNot Nothing Then
                progress.Visible = True
                m_excel_type = 1
                m_selectboxname = tv_box_inf_list.SelectedNode.Text
                Me.BackgroundWorkerexcel.RunWorkerAsync()
            Else
                MsgBox("请选择主控箱名称", , PROJECT_TITLE_STRING)
            End If


        End If
    End Sub

    Private Sub rb_sp_normal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_sp_normal.Click
        If rb_sp_normal.Checked = True Then
            'lb_sp_string1.Visible = False
            'cb_sp_diangan.Visible = False
            'tb_sp_power.Visible = False
            gb_sp_tiaoguang.Enabled = False
        End If
    End Sub

    Private Sub rb_sp_power_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_sp_power.Click
        If rb_sp_power.Checked = True Then
            lb_sp_string1.Visible = False
            lb_sp_string2.Visible = True
            cb_sp_diangan.Visible = False
            tb_sp_power.Visible = True

        End If
    End Sub

    Private Sub rb_sp_diangan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_sp_diangan.Click
        If rb_sp_diangan.Checked = True Then
            lb_sp_string1.Visible = True
            cb_sp_diangan.Visible = True
            tb_sp_power.Visible = False
            lb_sp_string2.Visible = False

        End If
    End Sub

    Private Sub rb_normal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_normal.Click
        If rb_normal.Checked = True Then
            gb_tiaoguang.Enabled = False
        End If
    End Sub

    Private Sub rb_power_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_power.Click
        If rb_power.Checked = True Then
            lb_string1.Visible = False
            lb_string2.Visible = True
            cb_diangan.Visible = False
            tb_power.Visible = True

        End If
    End Sub

    Private Sub rb_diangan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_diangan.Click
        If rb_diangan.Checked = True Then
            lb_string1.Visible = True
            cb_diangan.Visible = True
            tb_power.Visible = False
            lb_string2.Visible = False

        End If
    End Sub

    Private Sub cb_lamptype_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_lamptype.DropDown
        Com_inf.Select_all_lamptype(cb_lamptype)

    End Sub

    Private Sub cb_holiday_lamptype_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_holiday_lamptype.DropDown
        Com_inf.Select_all_lamptype(cb_holiday_lamptype)

    End Sub

    Private Sub cb_box_list_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_box_list.DropDown
        Com_inf.Select_all_lamptype(cb_box_list)
    End Sub

    Private Sub rb_tiaoguang_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_tiaoguang.Click
        If rb_tiaoguang.Checked = True Then
            gb_tiaoguang.Enabled = True
        End If
    End Sub

    Private Sub rb_sp_tiaoguang_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_sp_tiaoguang.Click
        If rb_sp_tiaoguang.Checked = True Then
            gb_sp_tiaoguang.Enabled = True
        End If
    End Sub

    Private Sub chb_banye_monday_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chb_banye_monday.Click
        If chb_banye_monday.Checked = True Then
            m_week(1) = 1
        Else
            m_week(1) = 0
        End If
    End Sub

    Private Sub chb_banye_tuesday_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chb_banye_tuesday.Click
        If chb_banye_tuesday.Checked = True Then
            m_week(2) = 1
        Else
            m_week(2) = 0
        End If
    End Sub

    Private Sub chb_banye_wendnesday_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chb_banye_wendnesday.Click
        If chb_banye_wendnesday.Checked = True Then
            m_week(3) = 1
        Else
            m_week(3) = 0
        End If
    End Sub

    Private Sub chb_banye_thursday_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chb_banye_thursday.Click
        If chb_banye_thursday.Checked = True Then
            m_week(4) = 1
        Else
            m_week(4) = 0
        End If
    End Sub

    Private Sub chb_banye_friday_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chb_banye_friday.Click
        If chb_banye_friday.Checked = True Then
            m_week(5) = 1
        Else
            m_week(5) = 0
        End If
    End Sub

    Private Sub chb_banye_saturday_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chb_banye_saturday.Click
        If chb_banye_saturday.Checked = True Then
            m_week(6) = 1
        Else
            m_week(6) = 0
        End If
    End Sub

    Private Sub chb_banye_sunday_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chb_banye_sunday.Click
        If chb_banye_sunday.Checked = True Then
            m_week(0) = 1
        Else
            m_week(0) = 0
        End If
    End Sub
End Class