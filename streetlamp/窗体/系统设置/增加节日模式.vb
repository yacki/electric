''' <summary>
''' 增加用户自定义的节日模式
''' </summary>
''' <remarks></remarks>

Public Class 增加节日模式
    ''' <summary>
    ''' 按区域控制
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rb_box_control_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_box_control.Click
        If rb_box_control.Checked = True Then
            cb_city_all.Enabled = True
            cb_area_all.Enabled = True
            cb_street_all.Enabled = True
            cb_box_all.Enabled = True
            cb_lamp_type_all.Enabled = False
            cb_lamp_id.Enabled = False
            rb_type_single_open.Enabled = True
            rb_type_double_open.Enabled = True
            rb_all_open.Text = "全开"
            rb_all_close.Text = "全闭"
        End If
    End Sub


    ''' <summary>
    ''' '在右边的面板中添加控制控制类型，对象，方法
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_add_method_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_add_method.Click
        Dim control_string As String   '控制字符串
        Dim control_num As Integer  '控制方法的个数
        Dim row_num As Integer

        If rb_box_control.Checked = True Or rb_city_control.Checked = True Or rb_area_control.Checked = True Or rb_street_control.Checked = True Or rb_type_control.Checked = True Then  '按电控箱名称控制
            control_num = 0
            control_string = ""
            If rb_city_control.Checked = True Then
                If cb_city_all.Text = "" Then
                    MsgBox("请选择城市名称", , PROJECT_TITLE_STRING)
                    cb_city_all.Focus()
                    Exit Sub
                End If
            End If

            If rb_area_control.Checked = True Then
                If cb_city_all.Text = "" Then
                    MsgBox("请选择城市名称", , PROJECT_TITLE_STRING)
                    cb_city_all.Focus()
                    Exit Sub
                End If
                If cb_area_all.Text = "" Then
                    MsgBox("请选择区域名称", , PROJECT_TITLE_STRING)
                    cb_area_all.Focus()
                    Exit Sub
                End If
            End If
            If rb_street_control.Checked = True Then
                If cb_city_all.Text = "" Then
                    MsgBox("请选择城市名称", , PROJECT_TITLE_STRING)
                    cb_city_all.Focus()
                    Exit Sub
                End If
                If cb_area_all.Text = "" Then
                    MsgBox("请选择区域名称", , PROJECT_TITLE_STRING)
                    cb_area_all.Focus()
                    Exit Sub
                End If
                If cb_street_all.Text = "" Then
                    MsgBox("请选择街道名称", , PROJECT_TITLE_STRING)
                    cb_street_all.Focus()
                    Exit Sub
                End If
            End If

            If rb_box_control.Checked = True Then
                If cb_city_all.Text = "" Then
                    MsgBox("请选择城市名称", , PROJECT_TITLE_STRING)
                    cb_city_all.Focus()
                    Exit Sub
                End If
                If cb_area_all.Text = "" Then
                    MsgBox("请选择区域名称", , PROJECT_TITLE_STRING)
                    cb_area_all.Focus()
                    Exit Sub
                End If
                If cb_street_all.Text = "" Then
                    MsgBox("请选择街道名称", , PROJECT_TITLE_STRING)
                    cb_street_all.Focus()
                    Exit Sub
                End If
                If cb_box_all.Text = "" Then
                    MsgBox("请选择主控箱名称", , PROJECT_TITLE_STRING)
                    cb_box_all.Focus()
                    Exit Sub
                End If
            End If

            If rb_type_control.Checked = True Then
                If cb_city_all.Text = "" Then
                    MsgBox("请选择城市名称", , PROJECT_TITLE_STRING)
                    cb_city_all.Focus()
                    Exit Sub
                End If
                If cb_area_all.Text = "" Then
                    MsgBox("请选择区域名称", , PROJECT_TITLE_STRING)
                    cb_area_all.Focus()
                    Exit Sub
                End If
                If cb_street_all.Text = "" Then
                    MsgBox("请选择街道名称", , PROJECT_TITLE_STRING)
                    cb_street_all.Focus()
                    Exit Sub
                End If
                If cb_box_all.Text = "" Then
                    MsgBox("请选择主控箱名称", , PROJECT_TITLE_STRING)
                    cb_box_all.Focus()
                    Exit Sub
                End If
                If cb_lamp_type_all.Text = "" Then
                    MsgBox("请选择类型名称", , PROJECT_TITLE_STRING)
                    cb_lamp_type_all.Focus()
                    Exit Sub
                End If
            End If

            If rb_all_open.Checked = False And rb_all_close.Checked = False And rb_type_single_open.Checked = False And rb_type_double_open.Checked = False Then
                MsgBox("请选择控制方法", , PROJECT_TITLE_STRING)
                rb_all_open.Focus()
                Exit Sub
            End If

            If cb_next_time.Text = "" Then
                MsgBox("请选择间隔时间", , PROJECT_TITLE_STRING)
                cb_next_time.Focus()
                Exit Sub
            End If

            If rb_all_open.Checked = True Then
                '选择全开
                control_num += 1
                control_string &= " 全开"

            End If

            If rb_all_close.Checked = True Then
                '选择全闭
                control_num += 1
                control_string &= " 全闭"

            End If
            If rb_type_single_open.Checked = True Then
                '选择单灯开
                control_num += 1
                control_string &= " 奇开"
            End If


            If rb_type_double_open.Checked = True Then
                '选择双灯开
                control_num += 1
                control_string &= " 偶开"
            End If



            row_num = dgv_control_list.RowCount
            Me.dgv_control_list.Rows.Add()
            If rb_city_control.Checked = True Then
                Me.dgv_control_list.Rows(row_num).Cells("control_type").Value = Trim(rb_city_control.Text)
                Me.dgv_control_list.Rows(row_num).Cells("control_obj").Value = Trim(cb_city_all.Text)
            End If
            If rb_area_control.Checked = True Then
                Me.dgv_control_list.Rows(row_num).Cells("control_type").Value = Trim(rb_area_control.Text)
                Me.dgv_control_list.Rows(row_num).Cells("control_obj").Value = Trim(cb_area_all.Text)
            End If

            If rb_street_control.Checked = True Then
                Me.dgv_control_list.Rows(row_num).Cells("control_type").Value = Trim(rb_street_control.Text)
                Me.dgv_control_list.Rows(row_num).Cells("control_obj").Value = Trim(cb_street_all.Text)
            End If

            If rb_box_control.Checked = True Then
                Me.dgv_control_list.Rows(row_num).Cells("control_type").Value = Trim(rb_box_control.Text)
                Me.dgv_control_list.Rows(row_num).Cells("control_obj").Value = Trim(cb_box_all.Text)
            End If

            If rb_type_control.Checked = True Then
                Me.dgv_control_list.Rows(row_num).Cells("control_type").Value = Trim(rb_box_control.Text) & " " & Trim(rb_type_control.Text)
                Me.dgv_control_list.Rows(row_num).Cells("control_obj").Value = Trim(cb_lamp_type_all.Text)
            End If
            Me.dgv_control_list.Rows(row_num).Cells("control_method").Value = control_string
            Me.dgv_control_list.Rows(row_num).Cells("time").Value = Trim(cb_next_time.Text)
            Me.dgv_control_list.Update()

        End If

        If rb_lamp_id_control.Checked = True Then  '按景观灯编号控制
            control_num = 0
            control_string = ""

            If cb_box_all.Text = "" Then
                MsgBox("请选择主控箱", , PROJECT_TITLE_STRING)
                cb_box_all.Focus()
                Exit Sub
            End If
            If cb_lamp_type_all.Text = "" Then
                MsgBox("请选择类型名称", , PROJECT_TITLE_STRING)
                cb_lamp_type_all.Focus()
                Exit Sub
            End If

            If cb_lamp_id.Text = "" Then
                MsgBox("请选择灯的编号", , PROJECT_TITLE_STRING)
                cb_lamp_id.Focus()
                Exit Sub
            End If

            If rb_all_open.Checked = False And rb_all_close.Checked = False And rb_type_single_open.Checked = False And rb_type_double_open.Checked = False Then
                MsgBox("请选择控制方法", , PROJECT_TITLE_STRING)
                rb_all_open.Focus()
                Exit Sub
            End If

            If cb_next_time.Text = "" Then
                MsgBox("请选择间隔时间", , PROJECT_TITLE_STRING)
                cb_next_time.Focus()
                Exit Sub
            End If

            If rb_all_open.Checked = True Then
                '选择全开
                control_num += 1
                control_string &= " 打开"

            End If

            If rb_all_close.Checked = True Then
                '选择全闭
                control_num += 1
                control_string &= " 关闭"

            End If

            row_num = dgv_control_list.RowCount
            Me.dgv_control_list.Rows.Add()
            Me.dgv_control_list.Rows(row_num).Cells("control_type").Value = Trim(rb_lamp_id_control.Text)
            Me.dgv_control_list.Rows(row_num).Cells("control_obj").Value = Trim(cb_box_all.Text) & " " & Trim(cb_lamp_type_all.Text) & " " & Trim(cb_lamp_id.Text)
            Me.dgv_control_list.Rows(row_num).Cells("control_method").Value = control_string
            Me.dgv_control_list.Rows(row_num).Cells("time").Value = Trim(cb_next_time.Text)
            Me.dgv_control_list.Update()

        End If

    End Sub

    ''' <summary>
    ''' 双击删除当前选择的内容
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub DataGridView_control_list_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv_control_list.CellDoubleClick
        Dim row_select As Integer
        row_select = Me.dgv_control_list.CurrentRow.Index
        If MsgBox("是否删除当前选择的控制方法", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.Yes Then
            Me.dgv_control_list.Rows.Remove(Me.dgv_control_list.CurrentRow) '删除当前选择的行
            Me.dgv_control_list.Update() '刷新
        End If

    End Sub

    ''' <summary>
    ''' 增加按钮处理函数
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_add_mod_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_add_mod.Click
        If tb_mod_title.Text = "" Then
            MsgBox("模式名称不可为空", , PROJECT_TITLE_STRING)
            tb_mod_title.Focus()
            Exit Sub
        End If
        If tb_mod_title.TextLength > 10 Then
            MsgBox("模式名称长度大于10", , PROJECT_TITLE_STRING)
            tb_mod_title.Focus()
            Exit Sub
        End If
        If dgv_control_list.RowCount <= 0 Then
            MsgBox("请先添加控制方法", , PROJECT_TITLE_STRING)
            bt_add_method.Focus()
            Exit Sub
        End If

        Dim rs As ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim conn As New ADODB.Connection
        DBOperation.OpenConn(conn)


        msg = ""
        sql = "select * from holiday_mod where mod_title='" & Trim(tb_mod_title.Text) & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "add_mod_Click", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If

        If rs.RecordCount > 0 Then
            If MsgBox("该用户自定义模式名已存在，是否覆盖原来的内容？", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.Yes Then
                sql = "delete from holiday_mod where mod_title='" & Trim(tb_mod_title.Text) & "'"
                DBOperation.ExecuteSQL(conn, sql, msg)
                sql = "select * from holiday_mod"
                rs = DBOperation.SelectSQL(conn, sql, msg)
                If rs Is Nothing Then
                    MsgBox(MSG_ERROR_STRING & "add_mod_Click", , PROJECT_TITLE_STRING)
                    conn.Close()
                    conn = Nothing
                    Exit Sub
                End If
                Dim i As Integer = 0
                While i < Me.dgv_control_list.RowCount
                    rs.AddNew()
                    rs.Fields("mod_title").Value = Trim(tb_mod_title.Text)
                    rs.Fields("control_type").Value = Me.dgv_control_list.Rows(i).Cells("control_type").Value
                    rs.Fields("control_obj").Value = Me.dgv_control_list.Rows(i).Cells("control_obj").Value
                    rs.Fields("control_method").Value = Me.dgv_control_list.Rows(i).Cells("control_method").Value
                    rs.Fields("time").Value = Val(Trim(Me.dgv_control_list.Rows(i).Cells("time").Value))
                    i += 1
                    rs.Update()
                End While

            End If

        Else
            '新增
            sql = "select * from holiday_mod"
            rs = DBOperation.SelectSQL(conn, sql, msg)
            If rs Is Nothing Then
                MsgBox(MSG_ERROR_STRING & "add_mod_Click", , PROJECT_TITLE_STRING)
                conn.Close()
                conn = Nothing
                Exit Sub
            End If
            Dim i As Integer = 0
            While i < Me.dgv_control_list.RowCount
                rs.AddNew()
                rs.Fields("mod_title").Value = Trim(tb_mod_title.Text)
                rs.Fields("control_type").Value = Me.dgv_control_list.Rows(i).Cells("control_type").Value
                rs.Fields("control_obj").Value = Me.dgv_control_list.Rows(i).Cells("control_obj").Value
                rs.Fields("control_method").Value = Me.dgv_control_list.Rows(i).Cells("control_method").Value
                rs.Fields("time").Value = Val(Trim(Me.dgv_control_list.Rows(i).Cells("time").Value))
                i += 1
                rs.Update()
            End While


        End If
        MsgBox("用户自定义模式" & Trim(tb_mod_title.Text) & "添加成功", , PROJECT_TITLE_STRING)

        '增加操作记录
        Com_inf.Insert_Operation("添加用户自定义模式：" & Trim(tb_mod_title.Text))

        rs.Close()
        rs = Nothing
        conn.Close()
        conn = Nothing

    End Sub

    ''' <summary>
    ''' 窗体载入函数
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 增加节日模式_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '设置各个窗体的图标
        Me.Icon = New Icon("图片\favicon.ico", 32, 32)
        Com_inf.Select_city_name(cb_city_all)

        cb_next_time.SelectedIndex = 0

        lb_lamp_id_start.Visible = False   '终端编号的前半部分
        lb_lamp_type_id.Visible = False
    End Sub

    ''' <summary>
    ''' 选择区域名称
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_box_all_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_box_all.DropDown
        Com_inf.Select_box_name_level(cb_city_all, cb_area_all, cb_street_all, cb_box_all)
    End Sub

    ''' <summary>
    ''' 选择类型
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_lamp_type_all_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_lamp_type_all.DropDown
        Com_inf.Select_type_name(cb_box_all, cb_lamp_type_all, lb_lamp_type_id)
    End Sub

    ''' <summary>
    ''' 电控箱名称改变，类型和编号也跟着改变
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_box_all_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_box_all.SelectedIndexChanged
        If cb_box_all.Items.Count > 0 Then
            Com_inf.Select_type_name(cb_box_all, cb_lamp_type_all, lb_lamp_type_id)
            If cb_lamp_type_all.Items.Count > 0 Then
                Com_inf.Select_lamp_id_type(cb_box_all, cb_lamp_type_all, cb_lamp_id, lb_lamp_id_start)
            End If
        End If
    End Sub

    ''' <summary>
    ''' 按单灯进行控制
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub lamp_id_control_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_lamp_id_control.Click
        If rb_lamp_id_control.Checked = True Then
            cb_city_all.Enabled = True
            cb_area_all.Enabled = True
            cb_street_all.Enabled = True
            cb_box_all.Enabled = True
            cb_lamp_type_all.Enabled = True
            cb_lamp_id.Enabled = True
            rb_all_open.Enabled = True
            rb_all_close.Enabled = True
            rb_type_single_open.Enabled = False
            rb_type_double_open.Enabled = False
            rb_all_open.Text = "打开"
            rb_all_close.Text = "关闭"
        End If
    End Sub

    ''' <summary>
    ''' 类型改变，编号也跟着改变
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_lamp_type_all_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_lamp_type_all.SelectedIndexChanged
        If cb_lamp_type_all.Items.Count > 0 Then
            Com_inf.Select_lamp_id_type(cb_box_all, cb_lamp_type_all, cb_lamp_id, lb_lamp_id_start)
        End If
    End Sub

    ''' <summary>
    ''' 选择编号
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_lamp_id_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_lamp_id.DropDown
        Com_inf.Select_lamp_id_type(cb_box_all, cb_lamp_type_all, cb_lamp_id, lb_lamp_id_start)
    End Sub

    ''' <summary>
    ''' 以城市进行控制
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rb_city_control_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_city_control.Click
        If rb_city_control.Checked = True Then
            cb_city_all.Enabled = True
            cb_area_all.Enabled = False
            cb_street_all.Enabled = False
            cb_box_all.Enabled = False
            cb_lamp_type_all.Enabled = False
            cb_lamp_id.Enabled = False
            rb_type_single_open.Enabled = True
            rb_type_double_open.Enabled = True
            rb_all_open.Text = "全开"
            rb_all_close.Text = "全闭"
        End If
    End Sub

    ''' <summary>
    ''' 以区域进行控制
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rb_area_control_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_area_control.Click
        If rb_area_control.Checked = True Then
            cb_city_all.Enabled = True
            cb_area_all.Enabled = True
            cb_street_all.Enabled = False
            cb_box_all.Enabled = False
            cb_lamp_type_all.Enabled = False
            cb_lamp_id.Enabled = False
            rb_type_single_open.Enabled = True
            rb_type_double_open.Enabled = True
            rb_all_open.Text = "全开"
            rb_all_close.Text = "全闭"
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
            cb_city_all.Enabled = True
            cb_area_all.Enabled = True
            cb_street_all.Enabled = True
            cb_box_all.Enabled = False
            cb_lamp_type_all.Enabled = False
            cb_lamp_id.Enabled = False
            rb_type_single_open.Enabled = True
            rb_type_double_open.Enabled = True
            rb_all_open.Text = "全开"
            rb_all_close.Text = "全闭"
        End If
    End Sub

    ''' <summary>
    ''' 增加城市名称
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_city_all_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_city_all.DropDown
        Com_inf.Select_city_name(cb_city_all)
    End Sub

    ''' <summary>
    ''' 城市名称改变，其他信息跟随改变
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_city_all_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_city_all.SelectedIndexChanged
        Com_inf.Select_area_name(cb_city_all, cb_area_all)
        Com_inf.Select_street_name(cb_city_all, cb_area_all, cb_street_all)
        Com_inf.Select_box_name_level(cb_city_all, cb_area_all, cb_street_all, cb_box_all)
        Com_inf.Select_type_name(cb_box_all, cb_lamp_type_all, lb_lamp_type_id)
    End Sub

    ''' <summary>
    ''' 增加区域名称
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_area_all_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_area_all.DropDown
        Com_inf.Select_area_name(cb_city_all, cb_area_all)
    End Sub

    ''' <summary>
    ''' 区域名称改变，其他信息跟随改变
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_area_all_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_area_all.SelectedIndexChanged
        Com_inf.Select_street_name(cb_city_all, cb_area_all, cb_street_all)
        Com_inf.Select_box_name_level(cb_city_all, cb_area_all, cb_street_all, cb_box_all)
        Com_inf.Select_type_name(cb_box_all, cb_lamp_type_all, lb_lamp_type_id)
    End Sub

    ''' <summary>
    ''' 增加街道信息
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_street_all_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_street_all.DropDown
        Com_inf.Select_street_name(cb_city_all, cb_area_all, cb_street_all)
    End Sub

    ''' <summary>
    ''' 街道信息改变，其他信息跟随改变
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_street_all_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_street_all.SelectedIndexChanged
        Com_inf.Select_box_name_level(cb_city_all, cb_area_all, cb_street_all, cb_box_all)
        Com_inf.Select_type_name(cb_box_all, cb_lamp_type_all, lb_lamp_type_id)
    End Sub

    ''' <summary>
    ''' 以类型进行控制
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rb_type_control_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_type_control.Click
        If rb_type_control.Checked = True Then
            cb_city_all.Enabled = True
            cb_area_all.Enabled = True
            cb_street_all.Enabled = True
            cb_box_all.Enabled = True
            cb_lamp_type_all.Enabled = True
            cb_lamp_id.Enabled = False
            rb_type_single_open.Enabled = True
            rb_type_double_open.Enabled = True
            rb_all_open.Text = "全开"
            rb_all_close.Text = "全闭"
        End If
    End Sub


    Private Sub 增加节日模式_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        g_windowclose = 1
    End Sub
End Class