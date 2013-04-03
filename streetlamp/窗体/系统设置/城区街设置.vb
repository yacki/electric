Public Class 城区街设置

    ''' <summary>
    ''' 选择删除区域
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rb_delete_area_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_delete_area_control.Click
        If rb_delete_area_control.Checked = True Then
            delete_area_string.Visible = True  '区域名可见
            cb_area_delete.Visible = True  '区域名下拉框可见
            delete_street_string.Visible = False  '街道名不可见
            cb_street_delete.Visible = False  '街道名下拉框不可见
        End If
    End Sub

    ''' <summary>
    ''' 选择删除城市
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rb_delete_city_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_delete_city_control.Click
        If rb_delete_city_control.Checked = True Then
            delete_area_string.Visible = False  '区域名不可见
            cb_area_delete.Visible = False  '区域名下拉框不可见
            delete_street_string.Visible = False  '街道名不可见
            cb_street_delete.Visible = False  '街道名下拉框不可见
        End If
    End Sub

    ''' <summary>
    ''' 选择删除街道
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rb_delete_street_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_delete_street_control.Click
        If rb_delete_street_control.Checked = True Then
            delete_area_string.Visible = True  '区域名可见
            cb_area_delete.Visible = True  '区域名下拉框可见
            delete_street_string.Visible = True  '街道名可见
            cb_street_delete.Visible = True  '街道名下拉框可见
        End If
    End Sub

    ''' <summary>
    ''' 增加城市名称
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_city_delete_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_city_delete.DropDown
        Com_inf.Select_city_name(cb_city_delete)  '为城市名下拉框增加内容
    End Sub

    ''' <summary>
    ''' 城市名称改变后，其他名称也相应改变
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_city_delete_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_city_delete.SelectedIndexChanged
        Com_inf.Select_area_name(cb_city_delete, cb_area_delete)
        If cb_area_delete.Items.Count > 0 Then
            cb_area_delete.SelectedIndex = 0
            Com_inf.Select_street_name(cb_city_delete, cb_area_delete, cb_street_delete)
            If cb_street_delete.Items.Count > 0 Then
                cb_street_delete.SelectedIndex = 0
            Else
                cb_street_delete.Items.Clear()
            End If
        Else
            cb_area_delete.Items.Clear()
            cb_street_delete.Items.Clear()
        End If

    End Sub

    ''' <summary>
    ''' 增加区域名称
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_area_delete_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_area_delete.DropDown
        Com_inf.Select_area_name(cb_city_delete, cb_area_delete)  '为区域名下拉框增加内容
    End Sub

    ''' <summary>
    ''' 区域名称该改后，其他名称也相应改变
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_area_delete_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_area_delete.SelectedIndexChanged
        'street_delete.Text = ""  '街道名称清空
        Com_inf.Select_street_name(cb_city_delete, cb_area_delete, cb_street_delete)
        If cb_street_delete.Items.Count > 0 Then
            cb_street_delete.SelectedIndex = 0
        Else
            cb_street_delete.Items.Clear()
        End If
    End Sub

    ''' <summary>
    ''' 增加街道名称
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_street_delete_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_street_delete.DropDown
        Com_inf.Select_street_name(cb_city_delete, cb_area_delete, cb_street_delete)  '为街道下拉框增加内容

    End Sub

    ''' <summary>
    ''' 删除信息
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_delete_inf_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_delete_inf.Click
        Dim city_id As String
        Dim area_id_list As New System.Collections.ArrayList
        Dim street_id_list As New System.Collections.ArrayList
        Dim control_box_id_list As New System.Collections.ArrayList
        Dim area_id_num As Integer
        Dim street_id_num As Integer
        Dim control_box_id_num As Integer

        Dim rs_city As New ADODB.Recordset
        Dim sql As String
        Dim msg As String
        Dim rs_area As New ADODB.Recordset
        Dim rs_street As New ADODB.Recordset
        Dim rs_control_box_id As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim i As Integer = 0
        DBOperation.OpenConn(conn)


        area_id_num = 0
        street_id_num = 0
        control_box_id_num = 0
        city_id = ""

        msg = ""
        '**********************************删除城市**************************************
        If rb_delete_city_control.Checked = True Then  '删除城市
            If cb_city_delete.Text = "" Then   '如果城市名称为空
                MsgBox("请选择城市名称", , PROJECT_TITLE_STRING)
                cb_city_delete.Focus()  '光标定位在城市名称下拉框
                Exit Sub
            End If
            area_id_list.Clear()   '区域编号列表清空
            street_id_list.Clear()  '街道编号列表清空
            control_box_id_list.Clear()  '电控箱编号列表清空
            area_id_num = 0 '区域编号数目清零
            street_id_num = 0  '街道编号数目清零
            control_box_id_num = 0  '电控箱编号数目清零

            sql = "select * from city where city='" & Trim(cb_city_delete.Text) & "'"
            rs_city = DBOperation.SelectSQL(conn, sql, msg)  '在city表中找到需要删除的城市名称
            If rs_city Is Nothing Then
                MsgBox(MSG_ERROR_STRING & "delete_inf_Click", , PROJECT_TITLE_STRING)
            Else
                If rs_city.RecordCount <= 0 Then  '如果没有该城市的名称，提示查找出错
                    MsgBox("不存在该城市信息", , PROJECT_TITLE_STRING)
                Else
                    If MsgBox("是否删除该城市的信息？", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.Yes Then  '询问是否删除数据库中城市的名称
                        '第一步删除city表中的数据
                        city_id = rs_city.Fields("ID").Value
                        sql = "delete from city where city='" & Trim(cb_city_delete.Text) & "'"
                        DBOperation.ExecuteSQL(conn, sql, msg)
                    Else
                        Exit Sub
                    End If
                End If
            End If

            If city_id <> "" Then
                '第二步删除area表中的该城市的数据
                sql = "select * from area where city_id='" & city_id & "'"

                rs_area = DBOperation.SelectSQL(conn, sql, msg)
                If rs_area Is Nothing Then
                    MsgBox(MSG_ERROR_STRING & "delete_inf_Click", , PROJECT_TITLE_STRING)
                Else
                    While rs_area.EOF = False
                        area_id_list.Add(Trim(rs_area.Fields("id").Value))  '在区域编号列表中增加该城市的区域编号
                        '第八步删除map_list表中这个城市的地图
                        sql = "delete from map_list where area='" & Trim(rs_area.Fields("area").Value) & "'"
                        DBOperation.ExecuteSQL(conn, sql, msg)

                        area_id_num += 1  '区域编号数目加1
                        rs_area.MoveNext()




                    End While
                    sql = "delete from area where city_id='" & city_id & "'"
                    DBOperation.ExecuteSQL(conn, sql, msg)  '删除area表中的该城市的区域

                End If
            End If

            i = 0
            While i < area_id_num  '第三步删除street表中的关于区域列表中的区域编号的街道
                sql = "select * from street where area_id='" & area_id_list(i) & "'"
                rs_street = DBOperation.SelectSQL(conn, sql, msg)
                If rs_street Is Nothing Then
                    MsgBox(MSG_ERROR_STRING & "delete_inf_Click", , PROJECT_TITLE_STRING)

                Else
                    If rs_street.RecordCount > 0 Then
                        While rs_street.EOF = False
                            street_id_list.Add(Trim(rs_street.Fields("street_id").Value)) '将想街道列表增加街道编号
                            street_id_num += 1  '街道数目增加1
                            rs_street.MoveNext()
                        End While
                        sql = "delete from street where area_id='" & area_id_list(i) & "'"
                        DBOperation.ExecuteSQL(conn, sql, msg)  '删除street表中区域属于区域列表中的所有街道信息


                    End If
                End If
                i += 1

            End While

            i = 0
            '第四步删除control_box表中的数据
            While i < street_id_num
                sql = "select * from control_box where street_id='" & street_id_list(i) & "'"
                rs_control_box_id = DBOperation.SelectSQL(conn, sql, msg) '电控箱表中所有属于街道列表中电控箱
                If rs_control_box_id Is Nothing Then
                    MsgBox(MSG_ERROR_STRING & "delete_inf_Click", , PROJECT_TITLE_STRING)
                Else
                    If rs_control_box_id.RecordCount > 0 Then
                        While rs_control_box_id.EOF = False
                            '根据电控箱名称，删除定位信息
                            sql = "delete from street_position where control_box_name= '" & Trim(rs_control_box_id.Fields("control_box_name").Value) & "'"
                            DBOperation.ExecuteSQL(conn, sql, msg)
                            control_box_id_list.Add(Trim(rs_control_box_id.Fields("control_box_id").Value))  '将电控箱编号增加到电控箱列表中
                            control_box_id_num += 1  '电控箱编号数目加1
                            rs_control_box_id.MoveNext()
                        End While
                        sql = "delete from control_box where street_id='" & street_id_list(i) & "'"
                        DBOperation.ExecuteSQL(conn, sql, msg) '删除电控箱列表中的电控箱信息
                    End If
                End If
                i += 1
            End While

            '第五步删除RoadIDAndIMEI表中的内容
            i = 0
            While i < control_box_id_num
                sql = "delete from RoadIDAndIMEI where RoadID='" & Val(control_box_id_list(i)) & "'"
                DBOperation.ExecuteSQL(conn, sql, msg)  '删除RoadIDAndIMEI中的终端信息
                i += 1
            End While

            '第六步删除box_lamptype中的类型数据
            i = 0
            While i < control_box_id_num
                sql = "delete from box_lamptype where control_box_id='" & control_box_id_list(i) & "'"
                DBOperation.ExecuteSQL(conn, sql, msg)  '删除RoadIDAndIMEI中的终端信息
                i += 1
            End While
            '第七步删除lamp_inf表中的数据

            i = 0
            While i < control_box_id_num
                sql = "delete from lamp_inf where control_box_id='" & control_box_id_list(i) & "'"
                DBOperation.ExecuteSQL(conn, sql, msg)  '删除lamp_inf表中的终端信息
                i += 1
            End While
            cb_city_delete.Text = ""

            MsgBox("删除成功", , PROJECT_TITLE_STRING)

            '增加操作记录
            Com_inf.Insert_Operation("删除城市名称：" & Trim(cb_city_delete.Text))

        End If

        '**********************************删除区域**************************************
        If rb_delete_area_control.Checked = True Then  '删除区域
            If cb_city_delete.Text = "" Then  '如果城市名称为空
                MsgBox("请选择城市名称", , PROJECT_TITLE_STRING)
                cb_city_delete.Focus()  '光标定位在城市名称下拉框
                Exit Sub
            End If
            If cb_area_delete.Text = "" Then  '如果区域名称为空
                MsgBox("请选择区域名称", , PROJECT_TITLE_STRING)
                cb_area_delete.Focus()  '光标定位在区域名称下拉框
                Exit Sub
            End If

            area_id_list.Clear() '清空区域列表
            street_id_list.Clear()  '清空街道列表
            control_box_id_list.Clear()  '清空电控箱列表
            area_id_num = 0  '区域编号数目清零
            street_id_num = 0  '街道编号数目清零
            control_box_id_num = 0  '电控箱编号数目清零

            If MsgBox("是否删除该区域的信息？", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.Yes Then
                '第一步删除area表中的该城市的数据
                sql = "select * from area where area='" & Trim(cb_area_delete.Text) & "'"
                rs_area = DBOperation.SelectSQL(conn, sql, msg)  '在区域表中查询
                If rs_area Is Nothing Then
                    MsgBox(MSG_ERROR_STRING & "delete_inf_Click", , PROJECT_TITLE_STRING)
                Else
                    While rs_area.EOF = False
                        area_id_list.Add(Trim(rs_area.Fields("id").Value))  '将要删除的区域名称增加到区域列表中
                        area_id_num += 1  '区域编号数目
                        rs_area.MoveNext()
                    End While
                    sql = "delete from area where area='" & Trim(cb_area_delete.Text) & "'"
                    DBOperation.ExecuteSQL(conn, sql, msg)  '删除区域
                    '第八步删除map_list表中这个城市的地图
                    sql = "delete from map_list where area='" & Trim(cb_area_delete.Text) & "'"
                    DBOperation.ExecuteSQL(conn, sql, msg)

                End If
            Else
                Exit Sub
            End If


            While i < area_id_num  '第二步删除street表中的数据
                sql = "select * from street where area_id='" & area_id_list(i) & "'"
                rs_street = DBOperation.SelectSQL(conn, sql, msg) '在street表中查找属于删除区域中的街道
                If rs_street Is Nothing Then
                    MsgBox(MSG_ERROR_STRING & "delete_inf_Click", , PROJECT_TITLE_STRING)
                Else
                    If rs_street.RecordCount > 0 Then
                        While rs_street.EOF = False
                            street_id_list.Add(Trim(rs_street.Fields("street_id").Value))  '将街道名增加到街道列表中
                            street_id_num += 1 '街道数目增加1
                            rs_street.MoveNext()
                        End While
                        sql = "delete from street where area_id='" & area_id_list(i) & "'"
                        DBOperation.ExecuteSQL(conn, sql, msg)  '删除街道



                    End If
                End If
                i += 1

            End While

            i = 0
            '第三步删除control_box表中的数据
            While i < street_id_num
                sql = "select * from control_box where street_id='" & street_id_list(i) & "'"
                rs_control_box_id = DBOperation.SelectSQL(conn, sql, msg) '在control_box表中查找属于街道中的电控箱
                If rs_control_box_id Is Nothing Then
                    MsgBox(MSG_ERROR_STRING & "delete_inf_Click", , PROJECT_TITLE_STRING)

                Else
                    If rs_control_box_id.RecordCount > 0 Then
                        While rs_control_box_id.EOF = False
                            '根据电控箱名称，删除定位信息
                            sql = "delete from street_position where control_box_name= '" & Trim(rs_control_box_id.Fields("control_box_name").Value) & "'"
                            DBOperation.ExecuteSQL(conn, sql, msg)

                            control_box_id_list.Add(Trim(rs_control_box_id.Fields("control_box_id").Value))  '将电控箱的编号增加到电控箱编号列表中
                            control_box_id_num += 1 '电控箱编号数目增加1
                            rs_control_box_id.MoveNext()
                        End While
                        sql = "delete from control_box where street_id='" & street_id_list(i) & "'"
                        DBOperation.ExecuteSQL(conn, sql, msg)  '’删除电控箱

                    End If
                End If
                i += 1
            End While

            '第四步删除RoadIDAndIMEI表中的内容
            i = 0
            While i < control_box_id_num
                sql = "delete from RoadIDAndIMEI where RoadID='" & Val(control_box_id_list(i)) & "'"
                DBOperation.ExecuteSQL(conn, sql, msg)  '删除RoadIDAndIMEI中的终端信息
                i += 1
            End While

            '第五步删除box_lamptype中的类型数据
            i = 0
            While i < control_box_id_num
                sql = "delete from box_lamptype where control_box_id='" & control_box_id_list(i) & "'"
                DBOperation.ExecuteSQL(conn, sql, msg)  '删除RoadIDAndIMEI中的终端信息
                i += 1
            End While
            '第六步删除lamp_inf表中的数据

            i = 0
            While i < control_box_id_num
                sql = "delete from lamp_inf where control_box_id='" & control_box_id_list(i) & "'"
                DBOperation.ExecuteSQL(conn, sql, msg)  '删除lamp_inf中的终端信息
                i += 1
            End While
            cb_area_delete.Text = ""
            MsgBox("删除成功", , PROJECT_TITLE_STRING)

            '增加操作记录
            Com_inf.Insert_Operation("删除区域名称：" & Trim(cb_area_delete.Text))
        End If

        '**********************************删除街道**************************************
        If rb_delete_street_control.Checked = True Then  '删除街道
            If cb_city_delete.Text = "" Then  '如果城市名称为空
                MsgBox("请选择城市名称", , PROJECT_TITLE_STRING)
                cb_city_delete.Focus()  '光标定位在城市下拉框中
                Exit Sub
            End If
            If cb_area_delete.Text = "" Then  '如果区域名称为空
                MsgBox("请选择区域名称", , PROJECT_TITLE_STRING)
                cb_area_delete.Focus() '光标定位在区域下拉框中
                Exit Sub
            End If
            If cb_street_delete.Text = "" Then  '如果街道名称为空
                MsgBox("请选择街道名称", , PROJECT_TITLE_STRING)
                cb_street_delete.Focus()  '光标定位在街道下拉框中
                Exit Sub
            End If

            area_id_list.Clear()  '区域列表清空
            street_id_list.Clear()  '街道列表清空
            control_box_id_list.Clear() '电控箱列表清空
            area_id_num = 0  '区域编号数目加1
            street_id_num = 0 '街道编号数目加1
            control_box_id_num = 0 '电控箱编号数目加1

            If MsgBox("是否删除该街道的信息？", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.Yes Then
                '第一步删除street表中的数据
                sql = "select * from street where street='" & Trim(cb_street_delete.Text) & "'"
                rs_street = DBOperation.SelectSQL(conn, sql, msg) '查询street表中需删除街道的信息
                If rs_street Is Nothing Then
                    MsgBox(MSG_ERROR_STRING & "delete_inf_Click", , PROJECT_TITLE_STRING)
                Else
                    If rs_street.RecordCount > 0 Then
                        While rs_street.EOF = False
                            street_id_list.Add(Trim(rs_street.Fields("street_id").Value))  '将街道信息增加到街道编号列表中
                            street_id_num += 1
                            rs_street.MoveNext()
                        End While
                        sql = "delete from street where street ='" & Trim(cb_street_delete.Text) & "'"
                        DBOperation.ExecuteSQL(conn, sql, msg)  '删除街道信息

                    End If
                End If
            Else
                Exit Sub

            End If


            i = 0
            '第二步删除control_box表中的数据
            While i < street_id_num
                sql = "select * from control_box where street_id='" & street_id_list(i) & "'"
                rs_control_box_id = DBOperation.SelectSQL(conn, sql, msg) '在control_box表中找到输入街道列表中街道的电控箱信息
                If rs_control_box_id Is Nothing Then
                    MsgBox(MSG_ERROR_STRING & "delete_inf_Click", , PROJECT_TITLE_STRING)
                Else
                    If rs_control_box_id.RecordCount > 0 Then
                        While rs_control_box_id.EOF = False
                            '根据电控箱名称，删除定位信息
                            sql = "delete from street_position where control_box_name= '" & Trim(rs_control_box_id.Fields("control_box_name").Value) & "'"
                            DBOperation.ExecuteSQL(conn, sql, msg)

                            control_box_id_list.Add(Trim(rs_control_box_id.Fields("control_box_id").Value))  '将电控箱的信息增加到电控箱编号列表中
                            control_box_id_num += 1
                            rs_control_box_id.MoveNext()
                        End While
                        sql = "delete from control_box where street_id='" & street_id_list(i) & "'"
                        DBOperation.ExecuteSQL(conn, sql, msg)  '删除control_box表中电控箱信息

                    End If
                End If
                i += 1
            End While

            '第三步删除RoadIDAndIMEI表中的内容
            i = 0
            While i < control_box_id_num
                sql = "delete from RoadIDAndIMEI where RoadID='" & Val(control_box_id_list(i)) & "'"
                DBOperation.ExecuteSQL(conn, sql, msg)  '删除RoadIDAndIMEI中的终端信息
                i += 1
            End While

            '第四步删除box_lamptype中的类型数据
            i = 0
            While i < control_box_id_num
                sql = "delete from box_lamptype where control_box_id='" & control_box_id_list(i) & "'"
                DBOperation.ExecuteSQL(conn, sql, msg)  '删除RoadIDAndIMEI中的终端信息
                i += 1
            End While
            '第五步删除lamp_inf表中的数据
            i = 0
            While i < control_box_id_num
                sql = "delete from lamp_inf where control_box_id='" & control_box_id_list(i) & "'"
                DBOperation.ExecuteSQL(conn, sql, msg) '删除lamp_inf表中的终端信息
                i += 1
            End While
            cb_street_delete.Text = ""
            MsgBox("删除成功", , PROJECT_TITLE_STRING)


            '增加操作记录
            Com_inf.Insert_Operation("删除街道名称：" & Trim(cb_street_delete.Text))
        End If

        '根据电控箱列表将时段，街道定位以及电控箱通信状态表更新
        i = 0
        While i < control_box_id_num
            '删除时段控制
            '1、经纬度
            sql = "delete from pianyi where lamp_id like'" & control_box_id_list(i) & "%'"
            DBOperation.ExecuteSQL(conn, sql, msg)
            '2、特殊
            sql = "delete from road_level where control_box_id='" & control_box_id_list(i) & "'"
            DBOperation.ExecuteSQL(conn, sql, msg)

            '3 节假日
            sql = "delete from Special_road_level where control_box_id='" & control_box_id_list(i) & "'"
            DBOperation.ExecuteSQL(conn, sql, msg)


            '删除报警信息
            sql = "delete from lamp_inf_record where substring(lamp_id,1,4)='" & control_box_id_list(i) & "' "
            DBOperation.ExecuteSQL(conn, sql, msg)

            i += 1

        End While
        '主控界面上刷新下拉框
        'sql = "select * from street_position "
        'g_welcomewinobj.cb_goto_area.Items.Clear()
        'rs_control_box_id = DBOperation.SelectSQL(conn, sql, msg)

        'While rs_control_box_id.EOF = False
        '    g_welcomewinobj.cb_goto_area.Items.Add(Trim(rs_control_box_id.Fields("control_box_name").Value))  '添加街道名称
        '    rs_control_box_id.MoveNext()
        'End While
        'If g_welcomewinobj.cb_goto_area.Items.Count > 0 Then
        '    g_welcomewinobj.cb_goto_area.SelectedIndex = 0    '选取第一个定位街道值
        'Else
        '    g_welcomewinobj.cb_goto_area.Text = ""    '选取第一个定位街道值

        'End If

        g_lampmap.Clear(Color.Empty)  '清空终端图片
        '刷新右侧列表
        g_welcomewinobj.SetControlBoxListDelegate(g_welcomewinobj.dgv_lamp_state_list, 0)

        '刷新主控界面左边的电控箱信息
        g_welcomewinobj.find_box_state() '电控箱列表

        '刷新时段控制
        Dim div_time_obj As New div_time_class
        div_time_obj.Div_time_show()

        ''首页的报警信息
        g_welcomewinobj.get_boxprobleminf() '获取故障信息

        '电控箱故障
        g_welcomewinobj.Control_boxTableAdapter.Fill(g_welcomewinobj.Control_box_state.control_box)

        '主控箱列表
        g_welcomewinobj.m_controlboxobj.set_controlbox_list(g_welcomewinobj.tv_box_inf_list)

        If rs_city.State = 1 Then
            rs_city.Close()
            rs_city = Nothing
        End If
        If rs_area.State = 1 Then
            rs_area.Close()
            rs_area = Nothing
        End If
        If rs_street.State = 1 Then
            rs_street.Close()
            rs_street = Nothing
        End If
        If rs_control_box_id.State = 1 Then
            rs_control_box_id.Close()
            rs_control_box_id = Nothing
        End If
        conn.Close()
        conn = Nothing

    End Sub

    ''' <summary>
    ''' 增加城区街的设置
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub add_inf_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_add_inf.Click
        Dim rs As New ADODB.Recordset
        Dim sql As String
        Dim msg As String
        Dim conn As New ADODB.Connection

        DBOperation.OpenConn(conn)
        msg = ""


        If rb_city_add.Checked = True Then '增加城市
            If lb_city_name.TextLength > 10 Then   '城市名称的长度非法
                MsgBox("城市名称的长度大于10，请重新输入", , PROJECT_TITLE_STRING)
                lb_city_name.Focus()
                Exit Sub
            End If
            If lb_city_name.Text = "" Then   '城市名称不可以为空
                MsgBox("城市名称不可以为空", , PROJECT_TITLE_STRING)
                lb_city_name.Focus()
                Exit Sub
            End If


            sql = "select * from city where city='" & Trim(lb_city_name.Text) & "'"
            rs = DBOperation.SelectSQL(conn, sql, msg)
            If rs Is Nothing Then
                MsgBox(MSG_ERROR_STRING & "add_inf_Click_1", , PROJECT_TITLE_STRING)
                Exit Sub
            Else
                If rs.RecordCount > 0 Then  '城市名已存在
                    MsgBox("该城市信息已经存在，不需重复输入", , PROJECT_TITLE_STRING)
                    Exit Sub
                Else
                    '增加城市信息
                    rs.AddNew()
                    rs.Fields("city").Value = Trim(lb_city_name.Text)
                    rs.Update()

                End If
                MsgBox("城市名称添加成功", , PROJECT_TITLE_STRING)
                rs.Close()
                rs = Nothing

            End If

            '增加操作记录
            Com_inf.Insert_Operation("添加城市名称：" & Trim(lb_city_name.Text))

        End If
        If rb_area_add.Checked = True Then   '增加区域名称
            If cb_city_name2.Text = "" Then
                MsgBox("城市名称不可以为空", , PROJECT_TITLE_STRING)
                cb_city_name2.Focus()
                Exit Sub
            End If
            If lb_area_name.TextLength > 10 Then  '区域名称长度非法
                MsgBox("区域名称的长度大于10，请重新输入", , PROJECT_TITLE_STRING)
                lb_area_name.Focus()
                Exit Sub
            End If

            sql = "select * from area where area='" & Trim(lb_area_name.Text) & "'"
            rs = DBOperation.SelectSQL(conn, sql, msg)
            Dim rs_city As ADODB.Recordset
            If rs Is Nothing Then
                MsgBox(MSG_ERROR_STRING & "add_inf_Click_1", , PROJECT_TITLE_STRING)
                Exit Sub
            Else
                If rs.RecordCount > 0 Then   '已存在该区域名称
                    MsgBox("该区域信息已经存在，不需重复输入", , PROJECT_TITLE_STRING)
                    Exit Sub
                Else
                    Dim rs_id As New ADODB.Recordset
                    Dim id As String   '区域编号
                    sql = "select * from area order by id desc"
                    rs_id = DBOperation.SelectSQL(conn, sql, msg)
                    If rs_id.RecordCount > 0 Then
                        id = Format(Trim(rs_id.Fields("id").Value) + 1, "00")  '如果已存在区域名则编号加一
                    Else
                        id = "01"  '编号初始为“01”
                    End If
                    rs_id.Close()
                    rs_id = Nothing

                    sql = "select * from city where city='" & Trim(cb_city_name2.Text) & "'"
                    rs_city = DBOperation.SelectSQL(conn, sql, msg)
                    If rs_city.RecordCount > 0 Then
                        rs.AddNew()
                        rs.Fields("city_id").Value = rs_city.Fields("ID").Value  '增加区域所属的城市编号

                    Else
                        MsgBox("没有城市信息，请先添加城市信息！", , PROJECT_TITLE_STRING)
                        rb_city_add.Focus()  '城市名输入出错，则光标定位到城市名文本框并退出增加程序
                        rs.Close()
                        rs = Nothing
                        rs_city.Close()
                        rs_city = Nothing
                        Exit Sub
                    End If

                    rs.Fields("area").Value = Trim(lb_area_name.Text)  '区域名称
                    rs.Fields("id").Value = id  '区域编号
                    rs.Update()
                    MsgBox("区域名称添加成功！", , PROJECT_TITLE_STRING)
                    rs_city.Close()
                    rs_city = Nothing

                End If
            End If

            '增加操作记录
            Com_inf.Insert_Operation("添加区域名称：" & Trim(lb_area_name.Text))

            rs.Close()
            rs = Nothing
        End If

        If rb_street_add.Checked = True Then   '增加街道名称
            If cb_city_name2.Text = "" Then
                MsgBox("城市名称不可以为空", , PROJECT_TITLE_STRING)
                cb_city_name2.Focus()
                Exit Sub
            End If
            If cb_area_name2.Text = "" Then
                MsgBox("区域名称不可以为空", , PROJECT_TITLE_STRING)
                cb_area_name2.Focus()
                Exit Sub
            End If

            If lb_street_name.TextLength > 10 Then
                MsgBox("街道名称的长度大于10，请重新输入", , PROJECT_TITLE_STRING)
                lb_street_name.Focus()
                Exit Sub
            End If


            Dim rs_ca As ADODB.Recordset
            Dim city_id As Integer
            sql = "select * from city where city='" & Trim(cb_city_name2.Text) & "'"
            rs_ca = DBOperation.SelectSQL(conn, sql, msg)
            If rs_ca.RecordCount <= 0 Then  '判断是否存在输入的城市名称
                MsgBox("无城市信息，请先添加城市信息", , PROJECT_TITLE_STRING)
                rb_city_add.Focus()  '城市名输入出错，则光标定位在城市名文本框，退出增加街道函数
                rs_ca.Close()
                rs_ca = Nothing
                Exit Sub
            Else
                city_id = rs_ca.Fields("ID").Value
                sql = "select * from area where area='" & Trim(cb_area_name2.Text) & "'and city_id='" & city_id & "'"
                rs_ca = DBOperation.SelectSQL(conn, sql, msg)
                If rs_ca.RecordCount <= 0 Then  '判断是否存在输入的区域名称
                    MsgBox("无区域信息，请先添加区域信息", , PROJECT_TITLE_STRING)
                    rb_area_add.Focus() '如果城市名及区域名输入出错，则光标定位在城市名文本框，并退出增加街道函数
                    rs_ca.Close()
                    rs_ca = Nothing
                    Exit Sub
                Else
                    Dim area_id As String
                    area_id = Trim(rs_ca.Fields("id").Value) '区域编号
                    sql = "select * from street where street='" & Trim(lb_street_name.Text) & "'"
                    rs_ca = DBOperation.SelectSQL(conn, sql, msg)

                    If rs_ca.RecordCount > 0 Then  '判断输入的街道信息是否已存在
                        MsgBox("街道信息已存在，不需重复输入", , PROJECT_TITLE_STRING)
                        rs_ca.Close()
                        rs_ca = Nothing
                        Exit Sub
                    Else
                        Dim street_id As String
                        sql = "select * from street where area_id='" & area_id & "' order by street_id desc"
                        rs_ca = DBOperation.SelectSQL(conn, sql, msg)
                        If rs_ca.RecordCount > 0 Then
                            street_id = Format(Mid(Trim(rs_ca.Fields("street_id").Value), 3, 3) + 1, "000")  '该区域已存在街道，街道编号增一
                            street_id = area_id & street_id
                        Else
                            street_id = area_id & "001"  '该区域内无街道则初始编号为区域名+“001”
                        End If
                        rs_ca.AddNew()
                        rs_ca.Fields("street_id").Value = street_id  '街道编号
                        rs_ca.Fields("area_id").Value = area_id  '区域编号
                        rs_ca.Fields("street").Value = Trim(lb_street_name.Text)  '街道名称
                        rs_ca.Update()
                        MsgBox("街道名称添加成功！", , PROJECT_TITLE_STRING)
                    End If
                End If
            End If

            '增加操作记录
            Com_inf.Insert_Operation("添加街道名称：" & Trim(lb_street_name.Text))

            rs_ca.Close()
            rs_ca = Nothing

        End If
        ''刷新主控界面左边的电控箱信息
        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    ''' 增加城市名称设置
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rb_city_add_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_city_add.Click
        If rb_city_add.Checked = True Then
            cb_city_name2.Visible = False  '城市名下拉框不可见
            lb_city_name.Visible = True  '城市名文本框可见
            area_name_string.Visible = False  '区域名不可见
            lb_area_name.Visible = False  '区域文本框不可见
            cb_area_name2.Visible = False
            lb_street_name.Visible = False  '街道名不可见
            street_name_string.Visible = False  '街道文本框不可见
        End If
    End Sub

    ''' <summary>
    ''' 增加区域名称设置
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rb_area_add_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_area_add.Click
        If rb_area_add.Checked = True Then
            lb_city_name.Visible = False
            cb_city_name2.Visible = True
            area_name_string.Visible = True '区域名可见
            lb_area_name.Visible = True  '区域名文本框可见
            cb_area_name2.Visible = False
            street_name_string.Visible = False  '街道名不可见
            lb_street_name.Visible = False  '街道名文本框不可见
        End If
    End Sub

    ''' <summary>
    ''' 增加街道名称设置
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rb_street_add_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_street_add.Click
        If rb_street_add.Checked = True Then
            lb_city_name.Visible = False  '城市名文本框不可见
            cb_city_name2.Visible = True  '城市名下拉框可见

            area_name_string.Visible = True  '区域名可见
            lb_area_name.Visible = False  '区域名文本框不可见
            cb_area_name2.Visible = True   '区域下拉框可见
            street_name_string.Visible = True '街道名可见
            lb_street_name.Visible = True  '街道名文本框可见
        End If
    End Sub

    ''' <summary>
    ''' 增加城市名称
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_city_name2_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_city_name2.DropDown
        Com_inf.Select_city_name(cb_city_name2)  '为城市名下拉框增加内容
    End Sub

    ''' <summary>
    ''' 增加街道名称
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_area_name2_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_area_name2.DropDown
        Com_inf.Select_area_name(cb_city_name2, cb_area_name2)  '为区域名下拉框增加内容
    End Sub

    ''' <summary>
    ''' 载入窗体
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 城区街设置_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = New Icon("图片\favicon.ico", 32, 32)
        Com_inf.Select_city_name(cb_city_delete)  '为城市名下拉框增加内容
        If cb_city_delete.Items.Count > 0 Then
            cb_city_delete.SelectedIndex = 0
            Com_inf.Select_area_name(cb_city_delete, cb_area_delete)  '为区域名下拉框增加内容
            If cb_area_delete.Items.Count > 0 Then
                cb_area_delete.SelectedIndex = 0
                Com_inf.Select_street_name(cb_city_delete, cb_area_delete, cb_street_delete)  '为街道下拉框增加内容
                If cb_street_delete.Items.Count > 0 Then
                    cb_street_delete.SelectedIndex = 0
                End If
            End If
        End If

        Com_inf.Select_city_name(cb_city_name2)
        If cb_city_name2.Items.Count > 0 Then
            cb_city_name2.SelectedIndex = 0
            Com_inf.Select_area_name(cb_city_name2, cb_area_name2)
            If cb_area_name2.Items.Count > 0 Then
                cb_area_name2.SelectedIndex = 0
            Else
                cb_area_name2.Items.Clear()
            End If
        Else
            cb_city_name2.Items.Clear()
            cb_area_name2.Items.Clear()
        End If
    End Sub

    ''' <summary>
    ''' 城市名称改变，其他信息改变
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_city_name2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_city_name2.SelectedIndexChanged
        Com_inf.Select_area_name(cb_city_name2, cb_area_name2)
        If cb_area_name2.Items.Count > 0 Then
            cb_area_name2.SelectedIndex = 0
        Else
            cb_area_name2.Items.Clear()
        End If
    End Sub

    Private Sub 城区街设置_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        g_windowclose = 1
    End Sub
End Class