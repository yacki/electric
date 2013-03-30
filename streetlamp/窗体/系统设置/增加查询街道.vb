''' <summary>
''' 增加可定位的区域
''' </summary>
''' <remarks></remarks>

Public Class 增加查询街道

    ''' <summary>
    ''' 选择区域名称后，在地图上将此区域移到窗体中心，双击后取坐标，然后输入数据库
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_input_pos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_input_pos.Click
        If cb_city_name.Text = "" Then
            MsgBox("请选择城市名称", , PROJECT_TITLE_STRING)
            cb_city_name.Focus()  '光标定位在城市名下拉框
            Exit Sub
        End If
        If cb_area_name.Text = "" Then
            MsgBox("请选择区域名称", , PROJECT_TITLE_STRING)
            cb_area_name.Focus()  '光标定位在区域名下拉框
            Exit Sub
        End If
        If tb_position.Text = "" Then
            MsgBox("请输入定位区域名称", , PROJECT_TITLE_STRING)
            tb_position.Focus()  '光标定位在街道名下拉框
            Exit Sub
        End If
        If tb_position.TextLength > 10 Then
            MsgBox("定位区域名称长度大于10，请重新输入", , PROJECT_TITLE_STRING)
            tb_position.Focus()
            Exit Sub
        End If


        If lb_pos_x.Text = "" Or lb_pos_y.Text = "" Then  '如街道定位的坐标为空，则提示如何可以获取定位坐标
            MsgBox("请在地图上将该区域移动到中心位置后，双击地图后获得定位坐标", , PROJECT_TITLE_STRING)
            Exit Sub
        End If

        Dim rs As ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim conn As New ADODB.Connection
        DBOperation.OpenConn(conn)


        msg = ""

        '*********************判断是否在当前的地图范围内，是就取坐标，不是不可以取坐标*****************
        sql = "SELECT map_list.id FROM dbo.city_area_view INNER JOIN dbo.map_list ON dbo.city_area_view.area = dbo.map_list.area where map_list.area='" & Trim(cb_area_name.Text) & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "input_pos_Click", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs.RecordCount > 0 Then
            If rs.Fields("id").Value <> g_choosemapid Then
                MsgBox("请将地图与所选区域匹配", , PROJECT_TITLE_STRING)
                rs.Close()
                rs = Nothing
                conn.Close()
                conn = Nothing
                Exit Sub
            End If
        End If

        sql = "select * from street_position where control_box_name='" & Trim(tb_position.Text) & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)  '在street_position表中查询是否已存在该街道的定位信息
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "input_pos_Click", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If

        If rs.RecordCount > 0 Then
            If MsgBox("主控箱定位信息已存在，是否覆盖数据？", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.Yes Then
                '修改数据库中原来的定位信息
                rs.Fields("control_box_name").Value = Trim(tb_position.Text)
                rs.Fields("pos_x").Value = Trim(lb_pos_x.Text)  '定位坐标X
                rs.Fields("pos_y").Value = Trim(lb_pos_y.Text)  '定位坐标Y
                rs.Fields("map_id").Value = g_choosemapid  '街道所对应的地图编号

                rs.Update()

            End If
        Else
            '如数据库中没有该街道的定位信息，增新增
            rs.AddNew()
            rs.Fields("control_box_name").Value = Trim(tb_position.Text)
            rs.Fields("pos_x").Value = Trim(lb_pos_x.Text)  '定位坐标X
            rs.Fields("pos_y").Value = Trim(lb_pos_y.Text)  '定位坐标Y
            '  rs.Fields("map_id").Value = LoginForm1.Property_welcome_win_obj.Property_map_id   '街道所对应的地图编号
            rs.Fields("map_id").Value = g_choosemapid  '街道所对应的地图编号


            rs.Update()

        End If

finish:
        MsgBox("查询区域增加完毕", , PROJECT_TITLE_STRING)

        '增加操作记录
        Com_inf.Insert_Operation("增加查询区域：" & Trim(tb_position.Text))

        rs.Close()
        rs = Nothing
        conn.Close()
        conn = Nothing
        Me.Close()
    End Sub

    ''' <summary>
    ''' 窗体载入函数
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 增加查询街道_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        g_addstreettag = 1
        '设置各个窗体的图标
        Me.Icon = New Icon("图片\favicon.ico", 32, 32)
        Com_inf.Select_city_name(cb_city_name)
    End Sub


    ''' <summary>
    ''' 选择城市名称
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_city_name_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_city_name.DropDown
        Com_inf.Select_city_name(cb_city_name)
    End Sub

    ''' <summary>
    ''' 主控箱名称改变，其他跟随改变
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_city_name_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_city_name.SelectedIndexChanged
        Com_inf.Select_area_name(cb_city_name, cb_area_name)

    End Sub

    ''' <summary>
    ''' 增加区域名称
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_area_name_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_area_name.DropDown
        Com_inf.Select_area_name(cb_city_name, cb_area_name)
    End Sub

    ''' <summary>
    ''' 关闭窗口
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 增加查询街道_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        g_addstreettag = 0
        g_windowclose = 1
    End Sub
End Class