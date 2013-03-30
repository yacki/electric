
''' <summary>
''' 编辑区域（电控箱）的信息，删除区域（电控箱）的信息
''' </summary>
''' <remarks></remarks>
Public Class 编辑电控箱

    Private m_IMEI As String
    Private m_controlboxname As String  '电控箱名称
    Private m_boxid As String  '电控箱编号
  
    ''' <summary>
    ''' 载入窗体
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 编辑电控箱_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: 这行代码将数据加载到表“Control_box.control_inf”中。您可以根据需要移动或移除它。
        '设置各个窗体的图标
        Me.Icon = New Icon("图片\favicon.ico", 32, 32)
        Me.Control_infTableAdapter.Fill(Me.Control_box.control_inf) '电控箱列表
        record_num.Text = "共有" & dgv_control_inf.RowCount.ToString & "条记录"

    End Sub

    ''' <summary>
    ''' 双击区域名称条目，给文本框中的内容赋值
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgv_control_inf_CellMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgv_control_inf.CellMouseDoubleClick
        ' g_addboxtag = 3  '编辑电控箱坐标

        m_controlboxname = Trim(dgv_control_inf.CurrentRow.Cells("box_name_col").Value) '电控箱名称
        m_IMEI = Trim(dgv_control_inf.CurrentRow.Cells("IMEI").Value) 'IMEI
        m_boxid = Trim(dgv_control_inf.CurrentRow.Cells("control_box_id_col").Value)  '电控箱编号

        lb_control_box_name.Text = m_controlboxname  '电控箱名称
        lb_imei.Text = m_IMEI  'IMEI
        lb_start_pos_x.Text = Trim(dgv_control_inf.CurrentRow.Cells("pos_x").Value)
        lb_start_pos_y.Text = Trim(dgv_control_inf.CurrentRow.Cells("pos_y").Value)
        If dgv_control_inf.CurrentRow.Cells("huilu_num").Value Is DBNull.Value Then
            lb_huilunum.Text = ""
        Else
            lb_huilunum.Text = Trim(dgv_control_inf.CurrentRow.Cells("huilu_num").Value)

        End If
        If dgv_control_inf.CurrentRow.Cells("control_box_type").Value Is DBNull.Value Then
            lb_box_type.Text = ""
        Else
            lb_box_type.Text = Trim(dgv_control_inf.CurrentRow.Cells("control_box_type").Value)

        End If
        If dgv_control_inf.CurrentRow.Cells("board_num").Value Is DBNull.Value Then
            lb_testboard_num.Text = ""
        Else
            lb_testboard_num.Text = Trim(dgv_control_inf.CurrentRow.Cells("board_num").Value)

        End If

        If dgv_control_inf.CurrentRow.Cells("information").Value Is DBNull.Value Then
            lb_box_information.Text = ""
        Else
            lb_box_information.Text = Trim(dgv_control_inf.CurrentRow.Cells("information").Value)
        End If

        '2011年12月12日，双击后增加电表的配置
        get_powermeter_set(m_boxid)

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
            tb_powermeter_type.Text = Trim(rs.Fields("powermeter_type").Value)
            tb_powermeterid.Text = Trim(rs.Fields("powermeter_id").Value)
            If rs.Fields("powermeter_bianbi").Value Is System.DBNull.Value Then
                tb_powermeter_bianbi.Text = ""
            Else
                tb_powermeter_bianbi.Text = Trim(rs.Fields("powermeter_bianbi").Value)

            End If
        Else
            ck_getmeter.Checked = False
            tb_powermeter_type.Text = ""
            tb_powermeterid.Text = ""
            tb_powermeter_bianbi.Text = ""
        End If

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing

    End Sub

    ''' <summary>
    ''' 修改区域的信息,2011年9月28日，编辑主控箱信息后当前主控箱的报警被置1，不显示，等待新的报警上传
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_edit_control_box_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_edit_control_box.Click

        If lb_control_box_name.Text = "" Then
            MsgBox("主控箱名称不可以为空", , PROJECT_TITLE_STRING)
            lb_control_box_name.Focus()
            Exit Sub
        End If
        If lb_control_box_name.TextLength > 10 Then
            MsgBox("主控箱名称长度不可以大于10", , PROJECT_TITLE_STRING)
            lb_control_box_name.Focus()
            Exit Sub
        End If
        'If gprs.TextLength <> 16 Then  '输入的IMEI的长度必须是16
        '    MsgBox("IMEI长度必须为16，请重新输入！", , PROJECT_TITLE_STRING)
        '    gprs.Focus()  '光标定位在IMEI文本框中
        '    Exit Sub
        'End If
        If lb_start_pos_x.Text = "" Then '在地图上双击生成电控箱坐标
            MsgBox("请在地图上双击生成位置坐标", , PROJECT_TITLE_STRING)
            Exit Sub
        End If

        If lb_huilunum.Text = "" Then  '请输入
            MsgBox("请输入模拟量路数", , PROJECT_TITLE_STRING)
            Exit Sub

        End If
        'If kaiguan_num.Text = "" Then
        '    MsgBox("请输入开关量路数", , PROJECT_TITLE_STRING)
        '    Exit Sub
        'End If

        If lb_testboard_num.Text = "" Then
            MsgBox("请输入测量板个数", , PROJECT_TITLE_STRING)
            Exit Sub
        End If

        'If huilunum_text.TextLength > 100 Then  '备注信息
        '    MsgBox("备注内容长度超过100", , PROJECT_TITLE_STRING)
        '    huilunum_text.Focus()
        '    Exit Sub
        'End If

        If lb_box_information.TextLength > 100 Then
            MsgBox("备注信息长度大于100", , PROJECT_TITLE_STRING)
            lb_box_information.Focus()
            Exit Sub
        End If
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
        imei_string = StrConv(Trim(lb_imei.Text), VbStrConv.Narrow)
        box_name_string = StrConv(Trim(lb_control_box_name.Text), VbStrConv.Narrow)
        box_name_string = box_name_string.Replace(" ", "") '将字符串中的空格去掉
        huilunum_string = StrConv(Trim(lb_huilunum.Text), VbStrConv.Narrow)
        '  kaiguannum_string = StrConv(Trim(kaiguan_num.Text), VbStrConv.Narrow)
        testboardnum_string = StrConv(Trim(lb_testboard_num.Text), VbStrConv.Narrow)
        '********************************************************************

        boxtype = Val(Trim(lb_box_type.Text))

        sql = "SELECT map_list.id, control_box.control_box_id, control_box_name , control_box.state, " _
               & "control_box.pos_y, control_box.pos_x FROM control_box INNER JOIN street ON control_box.street_id =" _
               & " street.street_id INNER JOIN area ON street.area_id = area.id INNER JOIN map_list ON area.area = map_list.area where map_list.id='" & g_choosemapid & "' and control_box.control_box_id='" & m_boxid & "'"
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
            '屏幕中的路灯起点坐标转换城地图中的相对坐标
            pos_add.X = Val(lb_start_pos_x.Text - g_welcomewinobj.GroupBox1.Location.X - g_welcomewinobj.DesktopLocation.X - (Me.Width - Me.ClientSize.Width) - g_welcomewinobj.pb_map.Location.X - g_welcomewinobj.SplitContainer3.Panel1.Width)
            pos_add.Y = Val(lb_start_pos_y.Text - g_welcomewinobj.GroupBox1.Location.Y - g_welcomewinobj.DesktopLocation.Y - (Me.Height - Me.ClientSize.Height) - g_welcomewinobj.pb_map.Location.Y)

        Else
            '屏幕中的路灯起点坐标转换城地图中的相对坐标
            pos_add.X = Val(lb_start_pos_x.Text)
            pos_add.Y = Val(lb_start_pos_y.Text)

        End If
        sql = "select * from RoadIDAndIMEI where IMEI='" & m_IMEI & "' and RoadID='" & Val(m_boxid) & "'"  '更新RoadIDAndIMEI 表
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs.RecordCount > 0 Then

            rs.Fields("IMEI").Value = imei_string  '更新IMEI值
            rs.Fields("information").Value = Trim(lb_box_information.Text) '更新备注
            rs.Update()
            Me.Control_infTableAdapter.Fill(Me.Control_box.control_inf)

        Else
            MsgBox("编辑出错，请在列表中双击选择需编辑的主控箱条目！", , PROJECT_TITLE_STRING)
            rs.Close()
            rs = Nothing
            conn.Close()
            conn = Nothing
            Exit Sub

        End If

        '更改电控箱control_box表
        sql = "select * from control_box where control_box_name='" & m_controlboxname & "'"
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
            MsgBox("编辑出错，请在列表中双击选择需编辑的主控箱条目！", , PROJECT_TITLE_STRING)
            rs.Close()
            rs = Nothing
            conn.Close()
            conn = Nothing
            Exit Sub
        End If


        '2011年12月12日增加抄表功能配置，抄表配置记录增加到一个新的表中，controlbox_power
        If ck_getmeter.Checked = True Then
            If tb_powermeter_type.Text = "" Then
                MsgBox("请输入电表规约", , PROJECT_TITLE_STRING)

            Else
                '电表编号可以为空，不知道的情况下为空，以后获取后补上
                sql = "select * from controlbox_power where control_box_id='" & m_boxid & "'"
                rs = DBOperation.SelectSQL(conn, sql, msg)
                If rs Is Nothing Then
                    GoTo finish
                End If
                If rs.RecordCount > 0 Then
                    '存在电表的配置，进行更新
                    rs.Fields("powermeter_type").Value = Trim(Me.tb_powermeter_type.Text)
                    rs.Fields("powermeter_id").Value = Trim(Me.tb_powermeterid.Text)
                    rs.Fields("powermeter_bianbi").Value = Val(Trim(Me.tb_powermeter_bianbi.Text))
                    rs.Fields("imei").Value = imei_string
                    rs.Update()
                Else
                    '原来不存在电表的配置，新增了电表的配置
                    rs.AddNew()
                    rs.Fields("control_box_id").Value = m_boxid
                    rs.Fields("control_box_name").Value = m_controlboxname
                    rs.Fields("powermeter_type").Value = Trim(Me.tb_powermeter_type.Text)
                    rs.Fields("powermeter_id").Value = Trim(Me.tb_powermeterid.Text)
                    rs.Fields("powermeter_bianbi").Value = Val(Trim(Me.tb_powermeter_bianbi.Text))
                    rs.Fields("imei").Value = imei_string
                    rs.Update()
                End If


            End If

        Else
            '删除抄表功能
            sql = "delete from controlbox_power where control_box_id='" & m_boxid & "'"
            DBOperation.ExecuteSQL(conn, sql, msg)

        End If


        rs.Close()
        rs = Nothing

finish:
        conn.Close()
        conn = Nothing


        '更新主控面板中的电控箱信息列表
        Me.Control_infTableAdapter.Fill(Me.Control_box.control_inf) '电控箱列表

        '刷新主控界面左边的电控箱信息
        g_welcomewinobj.find_box_state() '电控箱列表

        '刷新时段控制
        div_time_obj.Div_time_show()

        '将修改信息的主控箱故障置1，等待下次新的故障上传
        clear_boxalarm_show(m_controlboxname)
        clear_boxkaiguanalarm_show(m_controlboxname)

        m_IMEI = ""
        m_boxid = ""
        MsgBox("主控箱信息修改完成", , PROJECT_TITLE_STRING)

        '增加操作记录
        Com_inf.Insert_Operation("修改主控箱：" & box_name_string)

        lb_control_box_name.Text = ""
        lb_imei.Text = ""
        lb_huilunum.Text = ""
        lb_box_type.Text = ""
        lb_start_pos_x.Text = ""
        lb_start_pos_y.Text = ""
        lb_testboard_num.Text = ""

        '刷新主控箱列表
        g_welcomewinobj.m_controlboxobj.set_controlbox_list(g_welcomewinobj.tv_box_inf_list)


       


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
    ''' 删除区域内容
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_delete_control_box_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_delete_control_box.Click
        If Trim(lb_control_box_name.Text) = "" Or Trim(lb_imei.Text) = "" Or m_IMEI = "" Or m_boxid = "" Then
            MsgBox("请双击需删除的主控箱条目", , PROJECT_TITLE_STRING)
            Exit Sub

        End If
        If MsgBox("删除区域，将删除所有与其相关灯的信息，是否删除所选主控箱：" & m_boxid, MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.Yes Then
            Dim conn As New ADODB.Connection
            Dim msg As String
            Dim sql As String
            '删除区域后需要查询定位表中是否有该区域的名称，如果有则删除
            Dim rs As New ADODB.Recordset

            DBOperation.OpenConn(conn)

            msg = ""
            sql = "select control_box_name from control_box where control_box_id='" & m_boxid & "'"
            rs = DBOperation.SelectSQL(conn, sql, msg)
            If rs Is Nothing Then
                MsgBox(MSG_ERROR_STRING & "delete_control_box_Click", , PROJECT_TITLE_STRING)
                conn.Close()
                conn = Nothing
                Exit Sub
            End If
            If rs.RecordCount > 0 Then
                '获取电控箱名称
                m_controlboxname = Trim(rs.Fields("control_box_name").Value)

            End If

            sql = "delete from RoadIDAndIMEI where IMEI='" & m_IMEI & "' and RoadID='" & Val(m_boxid) & "'"
            DBOperation.ExecuteSQL(conn, sql, msg)  '删除RoadIDAndIMEI表中关于gprs_id的内容

            '删除电控箱表中内容
            sql = "delete from control_box where control_box_id='" & m_boxid & "'"
            DBOperation.ExecuteSQL(conn, sql, msg)
            '删除类型
            sql = "delete from box_lamptype where control_box_id='" & m_boxid & "'"
            DBOperation.ExecuteSQL(conn, sql, msg)

            '删除lamp_inf该电控箱编号的路灯信息
            sql = "delete from lamp_inf where control_box_id='" & m_boxid & "'"
            DBOperation.ExecuteSQL(conn, sql, msg)

            '删除时段控制
            '1、经纬度
            sql = "delete from pianyi where lamp_id like'" & m_boxid & "%'"
            DBOperation.ExecuteSQL(conn, sql, msg)
            '2、特殊
            sql = "delete from road_level where control_box_id='" & m_boxid & "'"
            DBOperation.ExecuteSQL(conn, sql, msg)

            '3 节假日
            sql = "delete from Special_road_level where control_box_id='" & m_boxid & "'"
            DBOperation.ExecuteSQL(conn, sql, msg)

            '删除报警信息
            sql = "delete from lamp_inf_record where substring(lamp_id,1,4)='" & m_boxid & "' "

            DBOperation.ExecuteSQL(conn, sql, msg)


            '将修改信息的主控箱故障置1，等待下次新的故障上传
            clear_boxalarm_show(m_controlboxname)
            clear_boxkaiguanalarm_show(m_controlboxname)



            MsgBox("删除成功！", , PROJECT_TITLE_STRING)  '提示删除成功


            '增加操作记录
            Com_inf.Insert_Operation("删除主控箱：" & m_controlboxname)

            lb_control_box_name.Text = ""  '清空文本框
            lb_imei.Text = ""
            lb_huilunum.Text = ""
            Me.Control_infTableAdapter.Fill(Me.Control_box.control_inf)  '刷新电控箱列表
            record_num.Text = "共有" & dgv_control_inf.RowCount.ToString & "条记录"  '刷新电控箱中的记录条数

            '根据电控箱名称，删除定位信息
            sql = "delete from street_position where control_box_name='" & m_controlboxname & "'"
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
            sql = "delete from controlbox_power where control_box_id='" & m_boxid & "'"
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
        m_boxid = ""
        lb_control_box_name.Text = ""
        lb_imei.Text = ""
        lb_huilunum.Text = ""
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

    ''' <summary>
    ''' 关闭编辑窗口
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 编辑电控箱_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        g_addboxtag = 3  '编辑电控箱坐标
    End Sub

    Private Sub ck_getmeter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ck_getmeter.Click
        If ck_getmeter.Checked = False Then
            tb_powermeter_type.Text = ""
            tb_powermeterid.Text = ""
            tb_powermeter_type.Enabled = False
            tb_powermeterid.Enabled = False
        Else
            tb_powermeter_type.Enabled = True
            tb_powermeterid.Enabled = True
        End If
    End Sub
End Class