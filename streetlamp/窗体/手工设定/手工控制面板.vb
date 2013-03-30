''' <summary>
''' 对单灯及主控箱进行控制，包括全开，全关，单开，双开，三遥组合命令（新的命令协议）
''' </summary>
''' <remarks></remarks>
Public Class 设备控制面板
    Private Structure m_controlobj
        Dim level As String  '控制的级别如市，区，街道等
        Dim obj_name As String '控制名称
        Dim row_id As Integer '命令所在的行数
    End Structure

    'Private Structure handorder
    '    Dim row_id As Integer
    '    Dim order_string As String
    '    Dim order_type As String
    'End Structure

    Private m_controlobjlist As New ArrayList   '最多同时控制多个对象
    Private m_controlobjnum As Integer = 0  '同时控制的个数
    Private m_controllevel As String   '控制的级别
    Private m_huilu, m_lamp As String  '控制的类型名称
    Private m_startid As Integer '隔一个灯柱开的起始编号
    Private m_waittime As Integer '发送组合命令的延缓时间
    Private m_control_type As String  '控制命令的类型
    Private m_grouporder As String '记录原则的组合控制命令
    Private m_checklist As New ArrayList  '存放选中的主控箱名称
    Private m_firstchooselevel As Integer = 10 '记录第一次选择的是整体还是单个的控制命令
    Private m_rowid As Integer  '发送命令对象的行数
    Private m_result As Boolean '发送命令结果
    Delegate Sub SetDataGridview(ByVal row As Integer, ByVal text As String, ByVal datagridview _
                 As Windows.Forms.DataGridView)        '设置DataGridview中的内容
    Private m_control_lamp_obj As New control_lamp
    Private auto_waittime As Integer '自动延时的时间
    Private m_successnum As Integer '命令执行成功的个数
    Private m_powercontrol As Integer = 2 '0表示非调光，1表示电子，2表示电感
    Private m_dianganvalue As String '电感值
    Private m_powervalue As String  '电子功率值

    ''' <summary>
    ''' 设置列表中的内容
    ''' </summary>
    ''' <param name="row">列表的行数</param>
    ''' <param name="text">第一列的内容</param>
    ''' <param name="datagridview">列表名</param>
    ''' <remarks></remarks>
    ''' 
    Public Sub SetDataGridviewDelegate(ByVal row As Integer, ByVal text As String, ByVal datagridview As Windows.Forms.DataGridView)
        If datagridview.InvokeRequired Then
            Dim datagridviewobj As SetDataGridview = New SetDataGridview(AddressOf SetDataGridviewDelegate)
            Me.Invoke(datagridviewobj, New Object() {row, text, datagridview})
        Else
            datagridview.Rows(row).Cells("result").Value = text   '第一列的内容

        End If
    End Sub
    ''' <summary>
    ''' 初始化窗体
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 手工控制面板_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '设置各个窗体的图标
        Me.Icon = New Icon("图片\favicon.ico", 32, 32)

        set_handcontrol_list()   '初始化手工控制面板中的信息

        m_controllevel = "#"  '初始化控制级别
        m_huilu = Com_inf.Get_Type_String(31)
        m_lamp = Com_inf.Get_Type_String(0)



    End Sub

    ''' <summary>
    ''' 设置手工控制面板中的城、区、街道、主控箱等名称列表
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub set_handcontrol_list()
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String
        Dim lamp_pointinfor As String = ""
        Dim rs_city, rs_area, rs_street, rs_box, rs_type, rs_lamp As New ADODB.Recordset
        Dim city_string, area_string, street_string, box_string, lamp_string, type_string As String
        Dim i1, i2, i3, i4, i5, i6 As Integer
        DBOperation.OpenConn(conn)
        msg = ""
        i1 = 0
        i2 = 0
        i3 = 0
        i4 = 0
        i5 = 0
        i6 = 0


        sql = "select distinct(city) as city_name,count(lamp_id) as lamp_num, city_id from lamp_street group by city,city_id order by city_id"
        rs_city = DBOperation.SelectSQL(conn, sql, msg)
        If rs_city Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "Set_hand_control_list", , PROJECT_TITLE_STRING)
            GoTo finish
        End If
        While rs_city.EOF = False
            city_string = Trim(rs_city.Fields("city_name").Value)
            tv_control_list.Nodes.Add(city_string & " (共计" & rs_city.Fields("lamp_num").Value.ToString & "个控制节点)")

            '区域名称
            sql = "select distinct(area) as area_name,count(lamp_id) as lamp_num from lamp_street where city='" & city_string & "' group by area"
            rs_area = DBOperation.SelectSQL(conn, sql, msg)
            If rs_area Is Nothing Then
                MsgBox(MSG_ERROR_STRING & "Set_hand_control_list", , PROJECT_TITLE_STRING)
                GoTo finish
            End If
            While rs_area.EOF = False
                area_string = Trim(rs_area.Fields("area_name").Value)
                tv_control_list.Nodes(i1).Nodes.Add(area_string & " (共计" & rs_area.Fields("lamp_num").Value.ToString & "个控制节点)")
                '街道名称
                sql = "select distinct(street) as street_name,count(lamp_id) as lamp_num,street_id from lamp_street where city='" & city_string & "' and area='" & area_string & "' group by street,street_id order by street_id"
                rs_street = DBOperation.SelectSQL(conn, sql, msg)
                If rs_street Is Nothing Then
                    MsgBox(MSG_ERROR_STRING & "Set_hand_control_list", , PROJECT_TITLE_STRING)
                    GoTo finish
                End If
                While rs_street.EOF = False
                    street_string = Trim(rs_street.Fields("street_name").Value)
                    tv_control_list.Nodes(i1).Nodes(i2).Nodes.Add(street_string & " (共计" & rs_street.Fields("lamp_num").Value.ToString & "个控制节点)")
                    '电控箱名称
                    sql = "select distinct(control_box_name) as box_name,count(lamp_id) as lamp_num, control_box_id from lamp_street where city='" & city_string & "' and area='" & area_string & "' and street='" & street_string & "' group by control_box_name,control_box_id order by control_box_id"

                    rs_box = DBOperation.SelectSQL(conn, sql, msg)
                    If rs_box Is Nothing Then
                        MsgBox(MSG_ERROR_STRING & "Set_hand_control_list", , PROJECT_TITLE_STRING)
                        GoTo finish
                    End If
                    While rs_box.EOF = False
                        box_string = Trim(rs_box.Fields("box_name").Value)
                        tv_control_list.Nodes(i1).Nodes(i2).Nodes(i3).Nodes.Add(box_string & " (共计" & rs_box.Fields("lamp_num").Value.ToString & "个控制节点)")
                        '类型
                        sql = "select distinct(type_string) as type_name,count(lamp_id) as lamp_num,type_id from lamp_street where city='" & city_string & "' and area='" & area_string & "' and street='" & street_string & "' and control_box_name='" & box_string & "' group by type_string, type_id order by type_id"
                        rs_type = DBOperation.SelectSQL(conn, sql, msg)
                        If rs_type Is Nothing Then
                            MsgBox(MSG_ERROR_STRING & "Set_hand_control_list", , PROJECT_TITLE_STRING)
                            GoTo finish
                        End If
                        While rs_type.EOF = False
                            type_string = Trim(rs_type.Fields("type_name").Value)
                            tv_control_list.Nodes(i1).Nodes(i2).Nodes(i3).Nodes(i4).Nodes.Add(type_string & " (共计：" & rs_type.Fields("lamp_num").Value.ToString & "个控制节点)")

                            '路灯
                            sql = "select lamp_id,type_id,lamp_pointinfor from lamp_street where city='" & city_string & "' and area='" & area_string & "' and street='" & street_string & "' and control_box_name='" & box_string & "' and type_string='" & type_string & "' order by lamp_id"
                            rs_lamp = DBOperation.SelectSQL(conn, sql, msg)
                            If rs_lamp Is Nothing Then
                                MsgBox(MSG_ERROR_STRING & "Set_hand_control_list", , PROJECT_TITLE_STRING)
                                GoTo finish
                            End If
                            i6 = 0
                            While rs_lamp.EOF = False
                                If IsDBNull(rs_lamp.Fields("lamp_pointinfor").Value) = False Then
                                    lamp_pointinfor = rs_lamp.Fields("lamp_pointinfor").Value
                                    If Trim(lamp_pointinfor) <> "" Then
                                        lamp_pointinfor = "  位置:" & lamp_pointinfor
                                    Else
                                        lamp_pointinfor = ""
                                    End If
                                Else
                                    lamp_pointinfor = ""
                                End If
                                If rs_lamp.Fields("type_id").Value <> 31 Then
                                    '各种类型的灯
                                    Com_inf.Get_DengGan(Trim(rs_lamp.Fields("lamp_id").Value))
                                    lamp_string = Val(Mid(Trim(rs_lamp.Fields("lamp_id").Value), 1, 4)).ToString & "-" & Val(Mid(Trim(rs_lamp.Fields("lamp_id").Value), 5, 2)).ToString & "-" & Val(Mid(Trim(rs_lamp.Fields("lamp_id").Value), 7, LAMP_ID_LEN)).ToString
                                    'tv_control_list.Nodes(i1).Nodes(i2).Nodes(i3).Nodes(i4).Nodes(i5).Nodes.Add(g_dengzhuid & "号灯杆_第" & g_dengzhulampid & "盏灯_节点编号： " & lamp_string)
                                    tv_control_list.Nodes(i1).Nodes(i2).Nodes(i3).Nodes(i4).Nodes(i5).Nodes.Add("节点编号： " & lamp_string & lamp_pointinfor)

                                Else
                                    '接触器编号
                                    lamp_string = Val(Mid(Trim(rs_lamp.Fields("lamp_id").Value), 1, 4)).ToString & "-" & Val(Mid(Trim(rs_lamp.Fields("lamp_id").Value), 5, 2)).ToString & "-" & Val(Mid(Trim(rs_lamp.Fields("lamp_id").Value), 7, LAMP_ID_LEN)).ToString
                                    tv_control_list.Nodes(i1).Nodes(i2).Nodes(i3).Nodes(i4).Nodes(i5).Nodes.Add("节点编号： " & lamp_string & lamp_pointinfor)

                                End If
                                i6 += 1
                                rs_lamp.MoveNext()
                            End While

                            i5 += 1
                            rs_type.MoveNext()
                        End While
                        i4 += 1
                        i5 = 0
                        rs_box.MoveNext()

                    End While

                    i3 += 1
                    i4 = 0
                    rs_street.MoveNext()

                End While

                i2 += 1
                i3 = 0
                rs_area.MoveNext()
            End While

            i1 += 1
            i2 = 0
            rs_city.MoveNext()
        End While

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
        If rs_box.State = 1 Then
            rs_box.Close()
            rs_box = Nothing
        End If

        If rs_type.State = 1 Then
            rs_type.Close()
            rs_type = Nothing
        End If
        If rs_lamp.State = 1 Then
            rs_lamp.Close()
            rs_lamp = Nothing
        End If
finish:
        conn.Close()
        conn = Nothing

    End Sub
    ''' <summary>
    ''' 由选择的树形级别，定级别
    ''' </summary>
    ''' <param name="control_level"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function get_controllevel(ByVal control_level As String) As String
        get_controllevel = ""
        If control_level = 0 Then
            get_controllevel = "市"
        End If
        If control_level = 1 Then
            get_controllevel = "区"
        End If
        If control_level = 2 Then
            get_controllevel = "街道"
        End If
        If control_level = 3 Then
            get_controllevel = "主控箱"
        End If
        If control_level = 4 Then
            get_controllevel = "类型"
        End If
        If control_level = 5 Then
            get_controllevel = "节点编号"
        End If

    End Function

    ''' <summary>
    ''' 增加控制对象
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_add_controlinf_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_add_controlinf.Click
        m_checklist.Clear() '清除所有选中的项目
        Dim tnRet As New TreeNode
        For Each treenode As TreeNode In tv_control_list.Nodes
            Com_inf.FindControlNode(treenode, m_checklist)
        Next
        If m_checklist.Count = 0 Then
            MsgBox("请选择需设置的主控箱名称", , PROJECT_TITLE_STRING)
            Exit Sub
        End If
        If m_firstchooselevel = 5 Then
            rb_single_open.Enabled = False
            rb_double_open.Enabled = False
            rb_order_group3.Enabled = False
        Else
            rb_single_open.Enabled = True
            rb_double_open.Enabled = True
            rb_order_group3.Enabled = True
        End If



        '将arraylist中的控制对象名称，显示到右边的列表中
        Com_inf.Addcontrolobj_to_Datagridview(m_checklist, dgv_hand_controllist, "control_obj", "level_num")

    End Sub

    ''' <summary>
    ''' 清除控制对象
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_clear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_clear.Click
        If MsgBox("是否清除已选择的控制对象?", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.Yes Then

            'm_controllevel = "#"
            'm_controlobjlist.Clear()
            'm_controlobjnum = 0
            'Me.rb_open_all.Checked = True
            'Me.rb_single_open.Enabled = True
            'Me.rb_double_open.Enabled = True
            'Me.rb_open1_2.Enabled = True
            'Me.rb_denggan1_1.Enabled = True

            Dim i As Integer = 0
            Dim boxlist As New ArrayList


            While i < dgv_hand_controllist.RowCount
                If dgv_hand_controllist.Rows(i).Cells("checkid").Value = 0 Then
                    Dim inf(1) As String
                    inf(0) = Trim(dgv_hand_controllist.Rows(i).Cells("control_obj").Value)
                    inf(1) = Trim(dgv_hand_controllist.Rows(i).Cells("level_num").Value)
                    boxlist.Add(inf)

                End If
                i += 1
            End While
            dgv_hand_controllist.Rows.Clear()
            i = 0
            While i < boxlist.Count
                dgv_hand_controllist.Rows.Add()
                dgv_hand_controllist.Rows(i).Cells("control_obj").Value = boxlist(i)(0)
                dgv_hand_controllist.Rows(i).Cells("level_num").Value = boxlist(i)(1)
                i += 1
            End While

            If dgv_hand_controllist.Rows.Count = 0 Then
                '全部清除后则将左边的选择节点清除，标志位复位
                For Each treenode As TreeNode In tv_control_list.Nodes
                    SetControlNode(treenode, False)
                    m_firstchooselevel = 10
                Next

            End If
        End If
    End Sub
    Private Sub Get_controlobj()
        Dim i As Integer = 0
        m_controlobjlist.Clear()
        While i < dgv_hand_controllist.RowCount
            Dim data As New m_controlobj
            data.level = dgv_hand_controllist.Rows(i).Cells("level_num").Value
            data.obj_name = dgv_hand_controllist.Rows(i).Cells("control_obj").Value
            data.row_id = i
            m_controlobjlist.Add(data)

            i += 1
        End While
    End Sub

    ''' <summary>
    ''' 对灯的控制操作
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_operation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_operation.Click
        '控制灯的操作
        If Me.rb_open_all.Checked = False And Me.rb_close_all.Checked = False And Me.rb_single_open.Checked = False _
        And Me.rb_double_open.Checked = False And Me.rb_open1_2.Checked = False And Me.rb_denggan1_1.Checked = False And rb_order_group3.Checked = False Then
            MsgBox("请选择控制方式", , PROJECT_TITLE_STRING)
            Exit Sub
        End If
        If cb_auto_close.Checked = True And Trim(cb_auto_waittime.Text) = "" Then
            MsgBox("请选择自动延时关闭时间", , PROJECT_TITLE_STRING)
            Exit Sub
        End If
        If cb_auto_close.Checked = True Then
            auto_waittime = Val(Trim(cb_auto_waittime.Text))
        End If
        m_control_type = ""
        m_rowid = 0
        m_successnum = 0
        Get_controlobj()
        If rb_open_all.Checked = True Then
            m_control_type = "开灯"
            'open_all_lamp(m_controlobjlist(i).obj_name, m_controlobjlist(i).level, j)   '路灯全开
        End If
        If rb_close_all.Checked = True Then
            m_control_type = "关灯"
            'close_all_lamp(m_controlobjlist(i).obj_name, m_controlobjlist(i).level, j)   '路灯全闭
        End If
        If rb_single_open.Checked = True Then
            m_control_type = "单号开"

            ' single_open_control(1, 1, m_controlobjlist(i).obj_name, m_controlobjlist(i).level, j)  '路灯奇开
        End If
        If rb_double_open.Checked = True Then
            m_control_type = "双号开"

            ' single_open_control(1, 2, m_controlobjlist(i).obj_name, m_controlobjlist(i).level, j)  '路灯奇开
        End If

        If rb_normal.Checked = True Then
            '非调光
            m_dianganvalue = "全功率"
            m_powervalue = "100"

        Else
            If rb_diangan.Checked = True Then
                m_dianganvalue = Trim(cb_diangan.Text)
                m_powervalue = "100"
            End If
            If rb_power.Checked = True Then

                If IsNumeric(Trim(lb_power.Text)) = False Then
                    MsgBox("调光功率请输入数字", , PROJECT_TITLE_STRING)
                    lb_power.Focus()
                    Exit Sub
                End If

                m_dianganvalue = "全功率"
                m_powervalue = Trim(lb_power.Text)
            End If
        End If


        If rb_open1_2.Checked = True Then
            If Trim(lb_start_id.Text) = "" Then
                MsgBox("请选择起始编号", , PROJECT_TITLE_STRING)
                lb_start_id.Focus()
                Exit Sub
            End If
            If IsNumeric(Trim(lb_start_id.Text)) = False Then
                MsgBox("起始编号必须为数字", , PROJECT_TITLE_STRING)
                lb_start_id.Focus()
                Exit Sub
            End If
            m_control_type = "隔两盏开"
            g_startid = Val(Trim(lb_start_id.Text))

            ' single_open_control(1, 3, m_controlobjlist(i).obj_name, m_controlobjlist(i).level, j)  '路灯奇开

        End If
        If rb_denggan1_1.Checked = True Then

            If Trim(cb_startid_6.Text) = "" Then
                MsgBox("请选择起始编号", , PROJECT_TITLE_STRING)
                cb_startid_6.Focus()
                Exit Sub
            End If
            If cb_waittime.Text = "" Then
                MsgBox("请选择延缓时间", , PROJECT_TITLE_STRING)
                cb_waittime.Focus()
                Exit Sub
            End If
            m_control_type = "灯杆隔盏开"

        End If

        If rb_order_group3.Checked = True Then
            If cb_grouporder_list.Text = "" Then
                MsgBox("请选择组合控制命令", , PROJECT_TITLE_STRING)
                rb_order_group3.Focus()
                Exit Sub
            End If
            m_grouporder = Trim(cb_grouporder_list.Text)
            m_control_type = "三回路组合控制"


        End If
        If Me.BackgroundWorkergroupcontrol.IsBusy = True Then
            MsgBox("控制命令正在发送，请稍后操作......", , PROJECT_TITLE_STRING)
            Exit Sub
        End If
        Me.BackgroundWorkergroupcontrol.RunWorkerAsync()
        '使部分按钮不可用，防止操作失败
        bt_add_controlinf.Enabled = False
        bt_selectall.Enabled = False
        bt_clear.Enabled = False

        '  MsgBox("操作完毕！", , PROJECT_TITLE_STRING)
    End Sub

    ''' <summary>
    ''' 按单号开还是双号开关
    ''' </summary>
    ''' <param name="open">open :0,关，1 开</param>
    ''' <param name="single_double">single_double 1，奇  0,偶</param>
    ''' <remarks></remarks>
    Private Sub single_open_control(ByVal open As Integer, ByVal single_double As Integer, ByVal control_name As String, ByVal level As String, ByVal control_time As Integer, ByVal row_id As Integer)  'open :0,关，1 开；single_double 1，奇  0,偶
        ' Dim power_string As String
        ' Dim control_lamp_obj As New control_lamp
        Dim conn As New ADODB.Connection
        Dim rs As New ADODB.Recordset
        Dim sql As String
        Dim msg As String
        Dim type(1) As String
        Dim hand_type As String

        DBOperation.OpenConn(conn)
        Dim control_method As String

        msg = ""

     

        hand_type = ""
        If open = 1 And single_double = 1 Then
            control_method = "奇开"

        Else
            If open = 1 And single_double = 2 Then
                control_method = "偶开"
            Else
                g_startidvalue = Val(Trim(lb_start_id.Text))
                control_method = "1/3开"
            End If

        End If
        If level = 0 Then
            '按市的级别进行控制
            hand_type = "城市"
            sql = "select distinct (control_box_name), control_box_id from lamp_street where city='" & control_name & "'"
        Else
            If level = 1 Then
                '按区的级别进行控制
                hand_type = "区域"
                sql = "select distinct (control_box_name) , control_box_id from lamp_street where area='" & control_name & "'"
            Else
                If level = 2 Then
                    '按街道的级别进行控制
                    hand_type = "街道"
                    sql = "select distinct (control_box_name) , control_box_id from lamp_street where street='" & control_name & "'"
                Else
                    GoTo next1
                End If
            End If

        End If


        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "single_open_control", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        While rs.EOF = False
            '奇开回路
            If open = 1 And single_double = 1 Then  '奇开
                'control_type 是回路的控制类型
                m_result = m_control_lamp_obj.Input_control_inf(m_huilu, "类型", Trim(rs.Fields("control_box_name").Value), "类型奇开", 0, m_dianganvalue, m_powervalue, hand_type, row_id)
                'control_lamp_obj.Input_control_inf(m_lamp, "类型", Trim(rs.Fields("control_box_name").Value), "类型全开", 0, Trim(diangan.Text), power_string, hand_type)
                m_control_lamp_obj.open_close_huilulamp(1, 1, Trim(rs.Fields("control_box_id").Value)) '1回路灯打开
                m_control_lamp_obj.open_close_huilulamp(0, 2, Trim(rs.Fields("control_box_id").Value)) '2回路灯关闭
                m_control_lamp_obj.open_close_huilulamp(1, 3, Trim(rs.Fields("control_box_id").Value)) '2回路灯关闭

            End If
            '偶开回路
            If open = 1 And single_double = 2 Then  '偶开
                m_result = m_control_lamp_obj.Input_control_inf(m_huilu, "类型", Trim(rs.Fields("control_box_name").Value), "类型偶开", 0, m_dianganvalue, m_powervalue, hand_type, row_id)
                ' control_lamp_obj.Input_control_inf(m_lamp, "类型", Trim(rs.Fields("control_box_name").Value), "类型全开", 0, Trim(diangan.Text), power_string, hand_type)
                m_control_lamp_obj.open_close_huilulamp(0, 1, Trim(rs.Fields("control_box_id").Value)) '1回路灯打开
                m_control_lamp_obj.open_close_huilulamp(1, 2, Trim(rs.Fields("control_box_id").Value)) '2回路灯关闭
                m_control_lamp_obj.open_close_huilulamp(0, 3, Trim(rs.Fields("control_box_id").Value)) '2回路灯关闭

            End If
            '1/3开回路
            If open = 1 And single_double = 3 Then  '1/3开
                'control_lamp_obj.Input_control_inf(huilu_type, "类型", Trim(rs.Fields("control_box_name").Value), "类型全开", 1, Trim(diangan.Text), power_string, hand_type)  '回路全开

                m_result = m_control_lamp_obj.Input_control_inf("", "主控箱", Trim(rs.Fields("control_box_name").Value), "1/3开", 1, m_dianganvalue, m_powervalue, "主控箱", row_id)

            End If

            rs.MoveNext()
        End While
        GoTo next2

next1:

        '控制电控箱级别
        If level = 3 Then
            '全开，先开回路
            hand_type = "主控箱"
            sql = "select control_box_id from control_box where control_box_name='" & control_name & "'"
            rs = DBOperation.SelectSQL(conn, sql, msg)
            If rs Is Nothing Then
                MsgBox(MSG_ERROR_STRING & "single_open_control", , PROJECT_TITLE_STRING)
                conn.Close()
                conn = Nothing
                Exit Sub
            End If
            If rs.RecordCount > 0 Then
                '奇开回路
                If open = 1 And single_double = 1 Then  '奇开
                    m_result = m_control_lamp_obj.Input_control_inf(m_huilu, "类型", control_name, "类型奇开", 0, m_dianganvalue, m_powervalue, hand_type, row_id)
                    'control_lamp_obj.Input_control_inf(m_lamp, "类型", control_name, "类型全开", 0, Trim(diangan.Text), power_string, hand_type)
                    m_control_lamp_obj.open_close_huilulamp(1, 1, Trim(rs.Fields("control_box_id").Value)) '1回路灯打开
                    m_control_lamp_obj.open_close_huilulamp(0, 2, Trim(rs.Fields("control_box_id").Value)) '2回路灯关闭
                    m_control_lamp_obj.open_close_huilulamp(1, 3, Trim(rs.Fields("control_box_id").Value)) '2回路灯关闭

                End If
                '偶开回路
                If open = 1 And single_double = 2 Then  '偶开
                    m_result = m_control_lamp_obj.Input_control_inf(m_huilu, "类型", control_name, "类型偶开", 0, m_dianganvalue, m_powervalue, hand_type, row_id)
                    'control_lamp_obj.Input_control_inf(m_lamp, "类型", control_name, "类型全开", 0, Trim(diangan.Text), power_string, hand_type)
                    m_control_lamp_obj.open_close_huilulamp(0, 1, Trim(rs.Fields("control_box_id").Value)) '1回路灯打开
                    m_control_lamp_obj.open_close_huilulamp(1, 2, Trim(rs.Fields("control_box_id").Value)) '2回路灯关闭
                    m_control_lamp_obj.open_close_huilulamp(0, 3, Trim(rs.Fields("control_box_id").Value)) '2回路灯关闭

                End If
                '1/3开回路
                If open = 1 And single_double = 3 Then  '1/3开
                    m_result = m_control_lamp_obj.Input_control_inf("", "主控箱", control_name, "1/3开", 1, m_dianganvalue, m_powervalue, "主控箱", row_id)

                End If
            End If

        End If
        '控制类型级别
        If level = 4 Then
            type = control_name.Split(" ")
            hand_type = "类型"
            sql = "select control_box_id from control_box where control_box_name='" & type(0) & "'"
            rs = DBOperation.SelectSQL(conn, sql, msg)
            If rs Is Nothing Then
                MsgBox(MSG_ERROR_STRING & "single_open_control", , PROJECT_TITLE_STRING)
                conn.Close()
                conn = Nothing
                Exit Sub
            End If
            If rs.RecordCount > 0 Then
                '奇开回路
                If open = 1 And single_double = 1 Then  '奇开
                    m_result = m_control_lamp_obj.Input_control_inf(type(1), "类型", type(0), "类型奇开", 1, m_dianganvalue, m_powervalue, hand_type, row_id)
                    If type(1) = m_huilu Then
                        'control_lamp_obj.Input_control_inf(m_lamp, "类型", type(0), "类型全开", 0, Trim(diangan.Text), power_string, hand_type)
                        m_control_lamp_obj.open_close_huilulamp(1, 1, Trim(rs.Fields("control_box_id").Value)) '1回路灯打开
                        m_control_lamp_obj.open_close_huilulamp(0, 2, Trim(rs.Fields("control_box_id").Value)) '2回路灯关闭
                        m_control_lamp_obj.open_close_huilulamp(1, 3, Trim(rs.Fields("control_box_id").Value)) '2回路灯关闭

                    End If
                End If
                '偶开回路
                If open = 1 And single_double = 2 Then  '偶开
                    m_result = m_control_lamp_obj.Input_control_inf(type(1), "类型", type(0), "类型偶开", 1, m_dianganvalue, m_powervalue, hand_type, row_id)

                    If type(1) = m_huilu Then
                        'control_lamp_obj.Input_control_inf(m_lamp, "类型", type(0), "类型全开", 0, Trim(diangan.Text), power_string, hand_type)
                        m_control_lamp_obj.open_close_huilulamp(0, 1, Trim(rs.Fields("control_box_id").Value)) '1回路灯打开
                        m_control_lamp_obj.open_close_huilulamp(1, 2, Trim(rs.Fields("control_box_id").Value)) '2回路灯关闭
                        m_control_lamp_obj.open_close_huilulamp(0, 3, Trim(rs.Fields("control_box_id").Value)) '2回路灯关闭

                    End If
                End If
                ''1/3开回路
                'If open = 1 And single_double = 3 Then  '1/3开
                '    control_lamp_obj.Input_control_inf("", "主控箱", Trim(rs.Fields("control_box_name").Value), "1/3开", 1, Trim(diangan.Text), power_string, "主控箱")

                'End If
            End If

        End If

        'If level = 5 Then
        '    Dim lamp(3) As String
        '    lamp = control_name.Split("-")
        '    While lamp(0).Length < 4
        '        lamp(0) = "0" & lamp(0)
        '    End While
        '    While lamp(1).Length < 2
        '        lamp(1) = "0" & lamp(1)
        '    End While
        '    While lamp(2).Length < 3
        '        lamp(2) = "0" & lamp(2)
        '    End While

        '    open_close_single_lamp(lamp(0) & lamp(1) & lamp(2), 0)

        'End If


next2:


        If control_time = 0 Then
            '记录手控信息到数据库
            sql = "insert into hand_control_record(control_content,content_name,control_method,diangan,power,control_time,user_name) values('" & hand_type & "','" & control_name & "','" & control_method & "','" & m_dianganvalue & "','" & m_powervalue & "','" & Now() & "','" & g_username & "')"

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
    ''' 关闭所有的路灯，按区域、类型和单灯三个级别
    ''' </summary>
    ''' <remarks></remarks>

    Private Sub close_all_lamp(ByVal control_name As String, ByVal level As String, ByVal control_time As Integer, ByVal row_id As Integer)   '关闭所有路灯

        '  Dim power_string As String
        'Dim control_lamp_obj As New control_lamp
        Dim conn As New ADODB.Connection
        Dim rs As New ADODB.Recordset
        Dim sql As String
        Dim msg As String
        Dim type(1) As String
        Dim hand_type As String
        Dim control_method As String

        control_method = "全闭"

        DBOperation.OpenConn(conn)

        msg = ""
     
        hand_type = ""

        If level = 0 Then
            '按市的级别进行控制
            hand_type = "城市"
            sql = "select distinct (control_box_name) from lamp_street where city='" & control_name & "'"
        Else
            If level = 1 Then
                '按区的级别进行控制
                hand_type = "区域"
                sql = "select distinct (control_box_name) from lamp_street where area='" & control_name & "'"
            Else
                If level = 2 Then
                    '按街道的级别进行控制
                    hand_type = "街道"
                    sql = "select distinct (control_box_name) from lamp_street where street='" & control_name & "'"
                Else
                    GoTo next1
                End If
            End If

        End If

        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "close_all_lamp", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        While rs.EOF = False
            '关闭，先开回路
            m_result = m_control_lamp_obj.Input_control_inf(m_huilu, "类型", Trim(rs.Fields("control_box_name").Value), "类型全闭", 0, "关闭灯", m_powervalue, hand_type, row_id)

            m_result = m_control_lamp_obj.Input_control_inf("", "主控箱", Trim(rs.Fields("control_box_name").Value), "全闭", 0, "关闭灯", m_powervalue, hand_type, row_id)

            rs.MoveNext()
        End While
        GoTo next2

next1:

        '控制电控箱级别
        If level = 3 Then
            '全开，先开回路
            hand_type = "主控箱"
            m_result = m_control_lamp_obj.Input_control_inf(m_huilu, "类型", control_name, "类型全闭", 0, "关闭灯", m_powervalue, hand_type, row_id)

            m_result = m_control_lamp_obj.Input_control_inf("", "主控箱", control_name, "全闭", 0, "关闭灯", m_powervalue, hand_type, row_id)

        End If
        '控制类型级别
        If level = 4 Then
            type = control_name.Split(" ")
            hand_type = "类型"
            'control_lamp_obj.Input_control_inf(huilu_type, "类型", type(0), "类型全开", 1, Trim(diangan.Text), power_string)

            m_result = m_control_lamp_obj.Input_control_inf(type(1), "类型", type(0), "类型全闭", 0, "关闭灯", m_powervalue, hand_type, row_id)
            If type(1) = m_huilu Then
                '控制的回路，则将该类型下的灯全部关闭
                m_result = m_control_lamp_obj.Input_control_inf("", "主控箱", type(0), "全闭", 0, "关闭灯", m_powervalue, hand_type, row_id)

            End If
        End If

        If level = 5 Then
            Dim lamp(3) As String
            lamp = control_name.Split("-")
            While lamp(0).Length < 4
                lamp(0) = "0" & lamp(0)
            End While
            While lamp(1).Length < 2
                lamp(1) = "0" & lamp(1)
            End While
            While lamp(2).Length < LAMP_ID_LEN
                lamp(2) = "0" & lamp(2)
            End While

            m_result = open_close_single_lamp(lamp(0) & lamp(1) & lamp(2), 0, control_time, row_id)

            '将该回路下所有的路灯全部关闭
            If lamp(1) = "31" Then
                m_control_lamp_obj.open_close_huilulamp(0, Val(lamp(2)), lamp(0))

            End If
            GoTo next3

        End If


next2:

        If control_time = 0 Then
            '记录手控信息到数据库

            sql = "insert into hand_control_record(control_content,content_name,control_method,diangan,power,control_time,user_name) values('" & hand_type & "','" & control_name & "','" & control_method & "','关闭灯',0,'" & Now() & "','" & g_username & "')"

            'sql = "insert into hand_control_record(control_content,content_name,control_method,diangan,power,control_time) values('" & condition & "','" & condition_name & lamp_type & "','" & control_method & "','" & diangan & "','" & power & "','" & Now() & "')"
            DBOperation.ExecuteSQL(conn, sql, msg)
        End If

next3:

    End Sub

    ''' <summary>
    ''' 三回路组合控制方式
    ''' </summary>
    ''' <param name="control_name"></param>
    ''' <param name="level"></param>
    ''' <param name="control_time"></param>
    ''' <remarks></remarks>
    Private Sub group_control(ByVal control_name As String, ByVal level As String, ByVal control_time As Integer, ByVal row_id As Integer)   '打开所有路灯
        Dim power_string As String
        '   Dim control_lamp_obj As New control_lamp
        Dim conn As New ADODB.Connection
        Dim rs, rs_order As New ADODB.Recordset
        Dim sql As String
        Dim msg As String
        Dim hand_type As String
        Dim control_method As String
        Dim group_order, group_order_method As String '组合命令
        Dim order_string As String '命令字符串


        control_method = "三回路组合控制:" & m_grouporder

        DBOperation.OpenConn(conn)


        power_string = "100"  '转换功率的值
        hand_type = ""
        msg = ""
        '查询出当前的控制命令
        sql = "select * from group_order where grouporder_name='" & m_grouporder & "'"
        rs_order = DBOperation.SelectSQL(conn, sql, msg)
        If rs_order Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "group_control", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If

        If rs_order.RecordCount > 0 Then
            group_order = Trim(rs_order.Fields("grouporder").Value)
            group_order_method = Trim(rs_order.Fields("grouporder_string").Value)

        Else
            GoTo finish
        End If


        If level = 0 Then
            '按市的级别进行控制
            hand_type = "城市"
            sql = "select distinct (control_box_id) from lamp_street where city='" & control_name & "'"
        Else
            If level = 1 Then
                '按区的级别进行控制
                hand_type = "区域"
                sql = "select distinct (control_box_id) from lamp_street where area='" & control_name & "'"
            Else
                If level = 2 Then
                    '按街道的级别进行控制
                    hand_type = "街道"
                    sql = "select distinct (control_box_id) from lamp_street where street='" & control_name & "'"
                Else
                    If level = 3 Then
                        '全开，先开回路
                        hand_type = "主控箱"
                        sql = "select control_box_id from control_box where control_box_name='" & control_name & "'"
                    Else
                        MsgBox("三回路组合控制只针对主控箱以上级别的路灯类型进行控制，对于其他控制范围无效", , PROJECT_TITLE_STRING)
                        GoTo finish

                    End If
                End If
            End If

        End If
        msg = ""
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "group_control", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        While rs.EOF = False
            '组合成控制命令
            order_string = Com_inf.Dec_to_Hex(Trim(rs.Fields("control_box_id").Value), 4)  '主控箱编号
            order_string = Mid(order_string, 3, 2) & " 00 01 " & group_order & " 64 " & Mid(order_string, 1, 2)
            '将命令放置到数据库中
            m_result = m_control_lamp_obj.Input_db_control(order_string, Trim(rs.Fields("control_box_id").Value), m_grouporder, 2, row_id)

            '将lamp_inf表中的状态转换成控制方式
            m_control_lamp_obj.Setgrouporder_lamp_state(Trim(rs.Fields("control_box_id").Value), group_order_method)
            rs.MoveNext()
        End While

        If control_time = 0 Then  '手工控制记录只记录一次
            '记录手控信息到数据库
            sql = "insert into hand_control_record(control_content,content_name,control_method,diangan,power,control_time,user_name) values('" & hand_type & "','" & control_name & "','" & control_method & "','" & m_dianganvalue & "','" & m_powervalue & "','" & Now() & "','" & g_username & "')"

            'sql = "insert into hand_control_record(control_content,content_name,control_method,diangan,power,control_time) values('" & condition & "','" & condition_name & lamp_type & "','" & control_method & "','" & diangan & "','" & power & "','" & Now() & "')"
            DBOperation.ExecuteSQL(conn, sql, msg)
        End If


finish:
        If rs_order.State = 1 Then
            rs_order.Close()
            rs_order = Nothing
        End If
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing

    End Sub

    ''' <summary>
    ''' 打开所有的路灯，按区域、类型和单灯三个级别
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub open_all_lamp(ByVal control_name As String, ByVal level As String, ByVal control_time As Integer, ByVal row_id As Integer)   '打开所有路灯

        Dim conn As New ADODB.Connection
        Dim rs As New ADODB.Recordset
        Dim sql As String
        Dim msg As String
        Dim type(1) As String
        Dim hand_type As String
        Dim control_method As String

        control_method = "全开"

        DBOperation.OpenConn(conn)


        hand_type = ""
        msg = ""

        If level = 0 Then
            '按市的级别进行控制
            hand_type = "城市"
            sql = "select distinct (control_box_name) from lamp_street where city='" & control_name & "'"
        Else
            If level = 1 Then
                '按区的级别进行控制
                hand_type = "区域"
                sql = "select distinct (control_box_name) from lamp_street where area='" & control_name & "'"
            Else
                If level = 2 Then
                    '按街道的级别进行控制
                    hand_type = "街道"
                    sql = "select distinct (control_box_name) from lamp_street where street='" & control_name & "'"
                Else
                    GoTo next1
                End If
            End If

        End If
        msg = ""
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "open_all_lamp", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        While rs.EOF = False
            '全开，先开回路
            m_result = m_control_lamp_obj.Input_control_inf(m_huilu, "类型", Trim(rs.Fields("control_box_name").Value), "类型全开", 1, m_dianganvalue, m_powervalue, hand_type, row_id)

            m_result = m_control_lamp_obj.Input_control_inf("", "主控箱", Trim(rs.Fields("control_box_name").Value), "全开", 1, m_dianganvalue, m_powervalue, hand_type, row_id)

            rs.MoveNext()
        End While
        GoTo next2

next1:

        '控制电控箱级别
        If level = 3 Then
            '全开，先开回路
            hand_type = "主控箱"
            m_result = m_control_lamp_obj.Input_control_inf(m_huilu, "类型", control_name, "类型全开", 1, m_dianganvalue, m_powervalue, hand_type, row_id)

            m_result = m_control_lamp_obj.Input_control_inf("", "主控箱", control_name, "全开", 1, m_dianganvalue, m_powervalue, hand_type, row_id)

        End If
        '控制类型级别
        If level = 4 Then
            type = control_name.Split(" ")
            hand_type = "类型"
            'control_lamp_obj.Input_control_inf(huilu_type, "类型", type(0), "类型全开", 1, Trim(diangan.Text), power_string)

            m_result = m_control_lamp_obj.Input_control_inf(type(1), "类型", type(0), "类型全开", 1, m_dianganvalue, m_powervalue, hand_type, row_id)
            If type(1) = m_huilu Then
                '控制的是主控箱节点,则将该回路中的所有灯都打开
                m_result = m_control_lamp_obj.Input_control_inf("", "主控箱", type(0), "全开", 1, m_dianganvalue, m_powervalue, hand_type, row_id)
            End If
        End If

        If level = 5 Then
            Dim lamp(3) As String
            lamp = control_name.Split("-")
            While lamp(0).Length < 4
                lamp(0) = "0" & lamp(0)
            End While
            While lamp(1).Length < 2
                lamp(1) = "0" & lamp(1)
            End While
            While lamp(2).Length < LAMP_ID_LEN
                lamp(2) = "0" & lamp(2)
            End While

            m_result = open_close_single_lamp(lamp(0) & lamp(1) & lamp(2), 1, control_time, row_id)

            If lamp(1) = "31" Then
                '将该回路下所有的路灯全部打开
                m_control_lamp_obj.open_close_huilulamp(1, Val(lamp(2)), lamp(0))

                'sql = "select control_box_name from control_box where control_box_id='" & lamp(0) & "'"
                'rs = DBOperation.SelectSQL(conn, sql, msg)

                'If rs.RecordCount > 0 Then
                '    hand_type = "类型"
                '    control_lamp_obj.Input_control_inf(m_lamp, "类型", Trim(rs.Fields("control_box_name").Value), "类型全开", 1, Trim(diangan.Text), power_string, hand_type)

                'End If

            End If
            GoTo next3

        End If


next2:

        If control_time = 0 Then  '手工控制记录只记录一次
            '记录手控信息到数据库
            sql = "insert into hand_control_record(control_content,content_name,control_method,diangan,power,control_time,user_name) values('" & hand_type & "','" & control_name & "','" & control_method & "','" & m_dianganvalue & "','" & m_powervalue & "','" & Now() & "','" & g_username & "')"

            'sql = "insert into hand_control_record(control_content,content_name,control_method,diangan,power,control_time) values('" & condition & "','" & condition_name & lamp_type & "','" & control_method & "','" & diangan & "','" & power & "','" & Now() & "')"
            DBOperation.ExecuteSQL(conn, sql, msg)
        End If


next3:

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
        DBOperation.OpenConn(conn)


        ' power_string = "100"

        type_id = Val(Mid(lamp_id, 5, 2))
        control_box_id = Mid(lamp_id, 1, 4)
        lamp_id_bin = Com_inf.Get_lampid_bin(type_id, Val(Mid(lamp_id, 7, LAMP_ID_LEN))) '十六位长度的路灯编号二进制
        If lamp_id_bin = "" Then
            GoTo finish
        End If
        'lamp_id_bin = Com_inf.Dec_to_Bin(type_id, 5) & Com_inf.Dec_to_Bin(Mid(lamp_id, 7, LAMP_ID_LEN), 11) '十六位长度的路灯编号二进制
        If open_close = 1 Then  '单灯开

            '打开路灯操作
            open_close_single_lamp = m_control_lamp_obj.open_light_single(control_box_id, lamp_id_bin, lamp_id, m_dianganvalue, m_powervalue, control_time, row_id)
        End If
        If open_close = 0 Then
            '关闭路灯
            open_close_single_lamp = m_control_lamp_obj.close_light_single(control_box_id, lamp_id_bin, lamp_id, "关闭灯", "0", control_time, row_id)
        End If

finish:
        conn.Close()
        conn = Nothing
    End Function

    ''' <summary>
    '''  隔两盏开则可设置起始编号
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rb_open1_2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_open1_2.Click
        If rb_open1_2.Checked = True Then
            lb_start_id.Enabled = True
        End If
    End Sub

    ''' <summary>
    ''' 选择打开所有的灯，设置一些控件不可用
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rb_open_all_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_open_all.Click
        If rb_open_all.Checked = True Then
            lb_start_id.Text = ""
            lb_start_id.Enabled = False
            cb_startid_6.Visible = False
            cb_waittime.Visible = False
            waittime_string.Visible = False
        End If
    End Sub

    ''' <summary>
    ''' 选择关闭所有的灯，设置一些控件不可用
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rb_close_all_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_close_all.Click
        If rb_close_all.Checked = True Then
            lb_start_id.Text = ""
            lb_start_id.Enabled = False
            cb_startid_6.Visible = False
            cb_waittime.Visible = False
            waittime_string.Visible = False
        End If
    End Sub

    ''' <summary>
    ''' 选择隔单号灯开，设置相关控件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rb_single_open_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_single_open.Click
        If rb_single_open.Checked = True Then
            lb_start_id.Text = ""
            lb_start_id.Enabled = False
            cb_startid_6.Visible = False
            cb_waittime.Visible = False
            waittime_string.Visible = False
        End If
    End Sub

    ''' <summary>
    ''' 选择双号灯开，设置相关控件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rb_double_open_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_double_open.Click
        If rb_double_open.Checked = True Then
            lb_start_id.Text = ""
            lb_start_id.Enabled = False
            cb_startid_6.Visible = False
            cb_waittime.Visible = False
            waittime_string.Visible = False
        End If
    End Sub

    ''' <summary>
    ''' 选择按隔盏灯杆开
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rb_denggan1_1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_denggan1_1.Click
        If rb_denggan1_1.Checked = True Then
            cb_startid_6.Visible = True
            cb_waittime.Visible = True
            waittime_string.Visible = True
        End If
    End Sub

    ''' <summary>
    ''' 隔灯杆开，选择的起始位置
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_startid_6_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_startid_6.SelectedIndexChanged
        m_startid = Val(cb_startid_6.Text)

    End Sub

    ''' <summary>
    ''' 隔灯杆开中间间隔的时间
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_waittime_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_waittime.SelectedIndexChanged
        m_waittime = Val(Trim(cb_waittime.Text))
    End Sub

    '返回自动关闭的命令
    Private Function get_auto_order(ByVal order_string As String) As String
        Dim order() As String
        Dim i As Integer = 0
        Dim temp_string As String = ""

        get_auto_order = ""
        order = Trim(order_string).Split(" ")
        Dim change_tag As Boolean = False

        If order.Length = 7 Then
            '传统的7字节命令
            If order(3) = "1B" Then
                '单开命令
                order(3) = "1C"
                change_tag = True
            End If
            If order(3) = "41" Or order(3) = "43" Or order(3) = "44" Or order(3) = "45" Or order(3) = "46" Then
                '类型全开命令
                order(3) = "42"
                change_tag = True
            End If
            If order(3) = "11" Or order(3) = "13" Or order(3) = "14" Or order(3) = "15" Or order(3) = "16" Then
                '全开命令
                order(3) = "12"
                change_tag = True
            End If
        End If
        If order.Length = 8 Then
            '三回路组合命令
            order(3) = "42"
            order(4) = "13"
            order(5) = "00"
            order(6) = "00"

        End If
        If change_tag = True Then
            While i < 7
                temp_string &= order(i) & " "
                i += 1
            End While

            get_auto_order = Trim(temp_string)

        End If

    End Function

    ''' <summary>
    ''' 发送隔灯杆开灯的操作命令，线程操作
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BackgroundWorkergroupcontrol_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorkergroupcontrol.DoWork

        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim nowtime As DateTime = Now

        m_successnum = 0 '命令执行成功的个数置0
        While j < 1
            While i < m_controlobjlist.Count
                SetDataGridviewDelegate(i, "发送命令...", dgv_hand_controllist)
                ' g_welcomewinobj.SetTextDelegate(m_controlobjlist(i).obj_name & " " & m_control_type & " 时间：" & Now.ToString & vbCrLf, True, g_welcomewinobj.rtb_info_list)

                '处理增加了的对象
                If m_control_type = "开灯" Then
                    open_all_lamp(m_controlobjlist(i).obj_name, m_controlobjlist(i).level, j, m_controlobjlist(i).row_id)   '路灯全开
                End If
                If m_control_type = "关灯" Then
                    close_all_lamp(m_controlobjlist(i).obj_name, m_controlobjlist(i).level, j, m_controlobjlist(i).row_id)   '路灯全闭
                End If
                If m_control_type = "单号开" Then
                    single_open_control(1, 1, m_controlobjlist(i).obj_name, m_controlobjlist(i).level, j, m_controlobjlist(i).row_id)  '路灯奇开
                End If
                If m_control_type = "双号开" Then
                    single_open_control(1, 2, m_controlobjlist(i).obj_name, m_controlobjlist(i).level, j, m_controlobjlist(i).row_id)  '路灯奇开
                End If
                If m_control_type = "隔两盏开" Then

                    single_open_control(1, 3, m_controlobjlist(i).obj_name, m_controlobjlist(i).level, j, m_controlobjlist(i).row_id)  '路灯奇开

                End If
                If m_control_type = "三回路组合控制" Then
                    group_control(m_controlobjlist(i).obj_name, m_controlobjlist(i).level, j, m_controlobjlist(i).row_id) '三回路组合控制方式
                End If
                'If m_result = True Then
                '    SetDataGridviewDelegate(i, "命令执行成功", dgv_hand_controllist)
                'Else
                '    SetDataGridviewDelegate(i, "命令执行失败", dgv_hand_controllist)
                'End If
                i += 1

            End While
            j += 1

        End While

        'If m_control_type = "灯杆隔盏开" Then
        '    dendgan1_1()
        'End If

        '如果当前的操作需要定时关灯的，则将相关的关灯命令保存到TimeControl这张表中
        Dim auto_orderstring As String
        Dim order As New handorder
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String
        Dim order_time As DateTime '自动关闭的时间
        sql = ""
        msg = ""
        order_time = Now
        DBOperation.OpenConn(conn)
        If cb_auto_close.Checked = True Then
            i = 0
            While i < m_control_lamp_obj.m_handcontrollist.Count
                order = m_control_lamp_obj.m_handcontrollist(i)
                auto_orderstring = get_auto_order(order.order_string)
                order_time = Now.AddMinutes(auto_waittime)
                If auto_orderstring <> "" Then
                    '表示需要保存关闭灯的命令
                    sql = "insert into TimeControl(CMDType, CMDContent, CreateTime) values('" & HG_TYPE.HG_AUTO_CLOSE_ORDER & "','" & auto_orderstring & "','" & order_time & "')"
                    DBOperation.ExecuteSQL(conn, sql, msg)


                End If

                i += 1
            End While
        End If

        '所有的命令发送完毕后，查询返回的状态
        Dim waittime As Integer = 20
        Dim feed_back_tag As Integer  ',0表示失败，1表示成功，2表示无返回
        Dim temp As New ArrayList

        While waittime > 0
            i = 0
            If m_control_lamp_obj.m_handcontrollist.Count > 0 Then

                While i < m_control_lamp_obj.m_handcontrollist.Count
                    temp = m_control_lamp_obj.m_handcontrollist
                    order = temp(i)
                    feed_back_tag = m_control_lamp_obj.Get_Control_FeedBack(order.order_string, nowtime, order.order_type)
                    If feed_back_tag = 0 Then
                        '返回失败状态
                        SetDataGridviewDelegate(order.row_id, "命令执行失败", dgv_hand_controllist)
                        m_control_lamp_obj.m_handcontrollist.RemoveAt(i)
                    Else
                        If feed_back_tag = 1 Then
                            '表示返回成功
                            SetDataGridviewDelegate(order.row_id, "命令执行成功", dgv_hand_controllist)
                            m_successnum += 1
                            m_control_lamp_obj.m_handcontrollist.RemoveAt(i)
                        Else
                            i += 1
                        End If
                    End If

                End While

            Else
                Exit While
            End If

            System.Threading.Thread.Sleep(1000)
            waittime -= 1

        End While

        '超时
        If waittime <= 0 Then
            i = 0
            While i < m_control_lamp_obj.m_handcontrollist.Count
                SetDataGridviewDelegate(temp(i).row_id, "命令执行超时", dgv_hand_controllist)
                m_control_lamp_obj.m_handcontrollist.RemoveAt(i)
            End While
        End If

        g_welcomewinobj.area_content_list_all(0) '刷新右侧列表

        conn.Close()
        conn = Nothing

    End Sub

    ''' <summary>
    ''' 按三头灯杆隔盏开
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub dendgan1_1()
        Dim control_box_name As String
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        msg = ""

        Dim i As Integer = 0
        Dim j As Integer = 0
        DBOperation.OpenConn(conn)
        While j < 1
            While i < m_controlobjnum
                '选择开6盏关6盏

                If m_controlobjlist(i).level = 4 Then '按类型控制
                    control_box_name = m_controlobjlist(i).obj_name.ToString.Split(" ")(0)
                    Me.open6_close6(m_startid, control_box_name)

                End If
                If m_controlobjlist(i).level = 3 Then  '按主控箱级别
                    control_box_name = m_controlobjlist(i).obj_name.ToString
                    Me.open6_close6(m_startid, control_box_name)
                End If
                If m_controlobjlist(i).level = 2 Then  '按街道级别
                    sql = "select control_box_name from control_inf where street='" & m_controlobjlist(i).obj_name & "'"
                    rs = DBOperation.SelectSQL(conn, sql, msg)
                    If rs Is Nothing Then
                        MsgBox(MSG_ERROR_STRING & "DendGan_1_1", , PROJECT_TITLE_STRING)
                        conn.Close()
                        conn = Nothing
                        Exit Sub
                    End If
                    While rs.EOF = False
                        control_box_name = Trim(rs.Fields("control_box_name").Value)
                        Me.open6_close6(m_startid, control_box_name)
                        rs.MoveNext()
                    End While

                End If
                If m_controlobjlist(i).level = 1 Then  '按区域级别
                    sql = "select control_box_name from control_inf where area='" & m_controlobjlist(i).obj_name & "'"
                    rs = DBOperation.SelectSQL(conn, sql, msg)
                    If rs Is Nothing Then
                        MsgBox(MSG_ERROR_STRING & "DendGan_1_1", , PROJECT_TITLE_STRING)
                        conn.Close()
                        conn = Nothing
                        Exit Sub
                    End If
                    While rs.EOF = False
                        control_box_name = Trim(rs.Fields("control_box_name").Value)
                        Me.open6_close6(m_startid, control_box_name)
                        rs.MoveNext()
                    End While

                End If

                If m_controlobjlist(i).level = 0 Then  '按城市级别
                    sql = "select control_box_name from control_inf where city='" & m_controlobjlist(i).obj_name & "'"
                    rs = DBOperation.SelectSQL(conn, sql, msg)
                    If rs Is Nothing Then
                        MsgBox(MSG_ERROR_STRING & "DendGan_1_1", , PROJECT_TITLE_STRING)
                        conn.Close()
                        conn = Nothing
                        Exit Sub
                    End If
                    While rs.EOF = False
                        control_box_name = Trim(rs.Fields("control_box_name").Value)
                        Me.open6_close6(m_startid, control_box_name)
                        rs.MoveNext()
                    End While

                End If


                i += 1
            End While
            j += 1
            i = 0
        End While

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    ''' 三个灯头一个灯杆，隔6盏开6盏
    ''' </summary>
    ''' <param name="start_denggan" >起始灯杆（一边）</param>
    ''' <remarks></remarks>
    Public Sub open6_close6(ByVal start_denggan As Integer, ByVal control_box_name As String)
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim lamp_id As String '需要关灯的灯的编号
        Dim num As Integer '灯号
        Dim start_num As Integer '该电控箱的起始编号
        Dim i As Integer = 1
        Dim control_lamp_obj As New control_lamp
        Dim power_string As String
        Dim hand_type As String
        Dim control_box_id As String '电控箱编号
        Dim lamp_num As Integer '共有多少盏路灯
        Dim order_string As String  '发送的关灯命令
        Dim order_descreption As String = "" '命令描述

        power_string = "100"  '转换功率的值
        hand_type = ""
        msg = ""
        DBOperation.OpenConn(conn)
        '记录手控信息到数据库
        sql = "insert into hand_control_record(control_content,content_name,control_method,diangan,power,control_time,user_name) values('主控箱','" & control_box_name & "','灯杆隔盏开','" & Trim(cb_diangan.Text) & "','" & power_string & "','" & Now() & "','" & g_username & "')"

        DBOperation.ExecuteSQL(conn, sql, msg)

        '隔6盏开6盏的效果，先将所有的灯全部打开，然后关闭间隔的6盏灯
        control_lamp_obj.Input_control_inf("", "主控箱", control_box_name, "全开", 1, Trim(cb_diangan.Text), power_string, hand_type, -1)


        sql = "select lamp_id from lamp_street where control_box_name='" & control_box_name & "' and type_id=0 order by lamp_id"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "open6_close6", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs.RecordCount > 0 Then
            start_num = Mid(Trim(rs.Fields("lamp_id").Value), 7, LAMP_ID_LEN)  '起始编号
            control_box_id = Mid(Trim(rs.Fields("lamp_id").Value), 1, 4) '电控箱编号
            control_box_id = Com_inf.Dec_to_Hex(control_box_id, 4)

            lamp_num = rs.RecordCount  '共有多少盏灯
            While rs.EOF = False
                If start_denggan = 1 Then
                    i = 1
                    While i <= 6
                        If rs.EOF = True Then
                            Exit While
                        End If
                        rs.MoveNext()
                        i += 1
                    End While
                    i = 1
                End If
                i = 1
                While i <= 6
                    num = start_num + 6 * start_denggan + i - 1
                    If num <> Val(Mid(Trim(rs.Fields("lamp_id").Value), 7, LAMP_ID_LEN)) Then
                        '    Dim yy As Integer = Val(Mid(Trim(rs.Fields("lamp_id").Value), 7, LAMP_ID_LEN))
                        i += 1
                        Continue While

                    End If
                    lamp_id = Com_inf.BIN_to_HEX("00000" & Com_inf.Dec_to_Bin(num, 11))
                    order_string = Mid(control_box_id, 3, 2) & " " & Mid(lamp_id, 1, 2) & " " & Mid(lamp_id, 3, 2) & " 1C 13 00 " & Mid(control_box_id, 1, 2)
                    order_descreption = Val(Mid(Trim(rs.Fields("lamp_id").Value), 1, 4)).ToString & "-" & Val(Mid(Trim(rs.Fields("lamp_id").Value), 5, 2)).ToString & "-" & Val(Mid(Trim(rs.Fields("lamp_id").Value), 7, LAMP_ID_LEN)).ToString & "单灯闭"
                    control_lamp_obj.Input_db_control(order_string, control_box_id, order_descreption, 1, -1)  '发送命令

                    'System.Threading.Thread.Sleep(1000)
                    'control_lamp_obj.Input_db_control(order_string)  '发送命令
                    sql = "update lamp_inf set state=0, result=4 where lamp_id='" & Trim(rs.Fields("lamp_id").Value) & "'"
                    DBOperation.ExecuteSQL(conn, sql, msg)
                    i += 1

                    rs.MoveNext()
                    If rs.EOF = True Then
                        Exit While
                    End If

                    System.Threading.Thread.Sleep(500 * m_waittime)
                End While
                i = 1
                start_denggan += 1



                While i <= 6
                    num = start_num + 6 * start_denggan + i - 1
                    If rs.EOF = True Then
                        Exit While
                    End If
                    If num <> Val(Mid(Trim(rs.Fields("lamp_id").Value), 7, LAMP_ID_LEN)) Then
                        i += 1
                        '   Dim xx As Integer = Val(Mid(Trim(rs.Fields("lamp_id").Value), 7, LAMP_ID_LEN))
                        Continue While

                    End If
                    rs.MoveNext()

                    i += 1
                End While

                i = 1

                start_denggan += 1
            End While
        End If
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing

    End Sub

    ''' <summary>
    ''' 选择三回路组合命令
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rb_order_group3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_order_group3.CheckedChanged
        If rb_order_group3.Checked = True Then
            cb_grouporder_list.Visible = True
        Else
            cb_grouporder_list.Visible = False
        End If
    End Sub

    ''' <summary>
    ''' 选择三回路组合的命令名称
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_grouporder_list_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_grouporder_list.DropDown
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String

        msg = ""
        sql = "select grouporder_name from group_order where grouporder_type=" & g_lampnum & "order by id"
        DBOperation.OpenConn(conn)
        cb_grouporder_list.Items.Clear()
        rs = DBOperation.SelectSQL(conn, sql, msg)
        While rs.EOF = False
            cb_grouporder_list.Items.Add(Trim(rs.Fields("grouporder_name").Value))
            rs.MoveNext()
        End While
        If cb_grouporder_list.Items.Count > 0 Then
            cb_grouporder_list.SelectedIndex = 0
        End If

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub


    Private Sub tv_control_list_AfterCheck(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tv_control_list.AfterCheck
        If tv_control_list.SelectedNode.Nodes.Count = 1 And e.Node.Checked = False Then
            m_firstchooselevel = 10
        End If
        If e.Node.Checked = True Then
            If m_firstchooselevel = 10 Then
                m_firstchooselevel = e.Node.Level
            End If
            If m_firstchooselevel = 5 Then  '表示第一个选择的是单灯，则接下来只能选择单灯

                If e.Node.Level <> 5 Then
                    MsgBox("节点单独控制命令请不要和整体控制命令混合使用", , PROJECT_TITLE_STRING)
                    e.Node.Checked = False
                    Exit Sub

                End If
            Else  '表示第一个选择的是整体，则接下来只能选择整体
                If e.Node.Level = 5 Then
                    MsgBox("节点单独控制命令请不要和整体控制命令混合使用", , PROJECT_TITLE_STRING)
                    e.Node.Checked = False
                    Exit Sub
                End If
            End If
        End If

    End Sub

    ''' <summary>
    ''' 全部选中控制对象
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_selectall_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_selectall.Click
        Dim i As Integer = 0
        While i < dgv_hand_controllist.RowCount
            dgv_hand_controllist.Rows(i).Cells("checkid").Value = 1
            i += 1
        End While
    End Sub

    Private Sub BackgroundWorkergroupcontrol_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorkergroupcontrol.ProgressChanged

        'If e.ProgressPercentage = 1 Then
        '    dgv_hand_controllist.Rows(m_rowid).Cells("result").Value = "发送命令..."

        'End If
        'If e.ProgressPercentage >= 2 Then
        '    If e.ProgressPercentage - 2 > Me.dgv_hand_controllist.Rows.Count Then
        '        Exit Sub
        '    End If
        '    If m_result = True Then
        '        dgv_hand_controllist.Rows(e.ProgressPercentage - 2).Cells("result").Value = "命令执行成功"
        '    Else
        '        dgv_hand_controllist.Rows(e.ProgressPercentage - 2).Cells("result").Value = "命令执行失败"
        '    End If
        'End If
    End Sub

    Private Sub BackgroundWorkergroupcontrol_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorkergroupcontrol.RunWorkerCompleted
        '使部分按钮恢复可用
        bt_add_controlinf.Enabled = True
        bt_selectall.Enabled = True
        bt_clear.Enabled = True

        '取所有接触器的状态，与相应的回路对应
        Com_inf.get_jiechuqi_state()
        '首先删除列表中命令执行成功的项目
        Dim i As Integer = 0
        Dim row As Integer = 0

        While i < dgv_hand_controllist.Rows.Count
            If dgv_hand_controllist.Rows(i).Cells("result").Value = "命令执行成功" Then
                row += 1
            End If
            i += 1
        End While


        '查询发送是否全部成功如果有没成功的则是失败重发按钮可见
        If row = dgv_hand_controllist.Rows.Count Then
            '所有命令都执行成功，则重发按钮不可用
            bt_reoperation.Enabled = False
        Else
            '有命令执行不成功，则重发按钮可用
            bt_reoperation.Enabled = True
        End If
    End Sub

    Private Sub 设备控制面板_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If Me.BackgroundWorkergroupcontrol.IsBusy = True Then
            MsgBox("线程正在运行，请稍后关闭", , PROJECT_TITLE_STRING)
            e.Cancel = True
        End If
        g_windowclose = 1
    End Sub

    Private Sub cb_auto_close_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_auto_close.CheckedChanged
        If cb_auto_close.Checked = True Then
            MsgBox("选择此选项表示手工操作后一定时间内会自动发送关灯命令，请慎重操作!", , PROJECT_TITLE_STRING)
            cb_auto_waittime.Enabled = True
        Else
            cb_auto_waittime.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' 如果有发送没有成功的命令，则提示对不成功的命令进行重发
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_reoperation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_reoperation.Click
        '首先删除列表中命令执行成功的项目
        Dim i As Integer = 0

        While i < dgv_hand_controllist.Rows.Count
            If dgv_hand_controllist.Rows(i).Cells("result").Value = "命令执行成功" Then
                dgv_hand_controllist.Rows.RemoveAt(i)
            Else
                i += 1
            End If
        End While
        m_rowid = 0
        Get_controlobj()
        If Me.BackgroundWorkergroupcontrol.IsBusy = True Then
            MsgBox("控制命令正在发送，请稍后操作......", , PROJECT_TITLE_STRING)
            Exit Sub
        End If
        Me.BackgroundWorkergroupcontrol.RunWorkerAsync()
        '使部分按钮不可用，防止操作失败
        bt_add_controlinf.Enabled = False
        bt_selectall.Enabled = False
        bt_clear.Enabled = False
    End Sub

    Private Sub rb_normal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_normal.Click
        If rb_normal.Checked = True Then
            'lb_string1.Visible = False
            'cb_diangan.Visible = False
            'lb_power.Visible = False
            'm_powercontrol = 0
            'cb_diangan.Text = "全功率"
            gb_tiaoguang.Enabled = False
        End If
    End Sub

    Private Sub rb_power_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_power.Click
        If rb_power.Checked = True Then
            lb_string1.Visible = False
            cb_diangan.Visible = False
            lb_power.Visible = True
            m_powercontrol = 1
            cb_diangan.Text = "全功率"
            lb_string2.Visible = True
        End If

    End Sub

    Private Sub rb_diangan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_diangan.Click
        If rb_diangan.Checked = True Then
            lb_string1.Visible = True
            cb_diangan.Visible = True
            lb_power.Visible = False
            m_powercontrol = 2
            lb_string2.Visible = False
        End If
    End Sub

    Private Sub rb_tiaoguang_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_tiaoguang.Click
        If rb_tiaoguang.Checked = True Then
            gb_tiaoguang.Enabled = True
        End If
    End Sub
End Class