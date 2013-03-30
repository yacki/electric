Public Class SelectAlarmType
    Private m_control_box_name As String '设置的主控箱名称

    Private Sub 选择报警类型_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: 这行代码将数据加载到表“TypeListDataSet.alarm_typelist”中。您可以根据需要移动或移除它。
        ' Me.Alarm_typelistTableAdapter.Fill(Me.TypeListDataSet.alarm_typelist)
        Me.Icon = New Icon("图片\favicon.ico", 32, 32)  'logo
        cb_alarmlist_tag.SelectedIndex = 0
    End Sub

    ''' <summary>
    ''' 选择城市名称
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_city_showtype_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_city_showtype.DropDown
        Com_inf.Select_city_name(cb_city_showtype)
    End Sub

    ''' <summary>
    ''' 城市名称改变后，改变后面的相关数据
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_city_showtype_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_city_showtype.SelectedIndexChanged
        Com_inf.Select_area_name(cb_city_showtype, cb_area_showtype)
    End Sub

    ''' <summary>
    ''' 选择区域名称
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_area_showtype_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_area_showtype.DropDown
        Com_inf.Select_area_name(cb_city_showtype, cb_area_showtype)
    End Sub

    ''' <summary>
    ''' 区域名称改变后，改变后面的相关数据
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_area_showtype_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_area_showtype.SelectedIndexChanged
        Com_inf.Select_street_name(cb_city_showtype, cb_area_showtype, cb_street_showtype)
    End Sub

    ''' <summary>
    ''' 选择街道名称
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_street_showtype_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_street_showtype.DropDown
        Com_inf.Select_street_name(cb_city_showtype, cb_area_showtype, cb_street_showtype)

    End Sub

    ''' <summary>
    ''' 街道名称改变后，改变后面的数据
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_street_showtype_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_street_showtype.SelectedIndexChanged
        Com_inf.Select_box_name_level(cb_city_showtype, cb_area_showtype, cb_street_showtype, cb_box_showtype)
    End Sub

    ''' <summary>
    ''' 选择主控箱名称
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_box_showtype_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_box_showtype.DropDown
        Com_inf.Select_box_name_level(cb_city_showtype, cb_area_showtype, cb_street_showtype, cb_box_showtype)

    End Sub

    ''' <summary>
    ''' 按主控箱查询设置的报警类型
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_showinf_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_showinf.Click
        If Trim(cb_box_showtype.Text) = "" Then
            MsgBox("请选择主控箱名称", , PROJECT_TITLE_STRING)
            cb_box_showtype.Focus()
            Exit Sub
        End If
        m_control_box_name = Trim(cb_box_showtype.Text)
        GBSelect.Enabled = True
        GBAdd.Enabled = True
        cb_alarmtype_list.Text = ""
        tb_alarmstring.Text = ""
        cb_alarmlist_tag.Text = ""

        '选择开关量是否报警
        selectAlarm()

        Me.Alarm_typelistTableAdapter.FillBy_box(Me.TypeListDataSet.alarm_typelist, m_control_box_name)

    End Sub

    ''' <summary>
    ''' 选择开关量是否报警
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub selectAlarm()
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim i As Integer = 0

        msg = ""
        sql = "select * from alarm_typelist where control_box_name='" & m_control_box_name & "' order by id"
        DBOperation.OpenConn(conn)
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "selectAlarm", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If

        clb_alarmtype_list.Items.Clear()
        While rs.EOF = False
            clb_alarmtype_list.Items.Add(Trim(rs.Fields("alarm_type").Value))
            If rs.Fields("check_tag").Value = 1 Then
                clb_alarmtype_list.SetItemChecked(i, True)
            Else
                clb_alarmtype_list.SetItemChecked(i, False)
            End If
            rs.MoveNext()
            i += 1
        End While

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing

    End Sub

    ''' <summary>
    ''' 选择开关量名称，此处开关量名称是根据之前设置的，不需要用户手工输入
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_alarmtype_list_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_alarmtype_list.DropDown
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String

        msg = ""
        sql = "select * from kaiguan_list where control_box_name='" & m_control_box_name & "'"
        DBOperation.OpenConn(conn)
        rs = DBOperation.SelectSQL(conn, sql, msg)

        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "AlarmTypeList_DropDown", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If

        cb_alarmtype_list.Items.Clear()
        While rs.EOF = False
            cb_alarmtype_list.Items.Add(Trim(rs.Fields("kaiguan_name").Value))  '增加开关量名称
            rs.MoveNext()

        End While
        If cb_alarmtype_list.Items.Count > 0 Then
            cb_alarmtype_list.SelectedIndex = 0
        End If

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing

    End Sub

    ''' <summary>
    ''' 增加报警类型
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_addtype_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_addtype.Click

        Dim conn As New ADODB.Connection
        Dim rs As New ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim alarmcontent As String
        Dim alarmtag As Integer
        Dim kaiguantag As Integer '开关量标志

        msg = ""

        If Trim(cb_alarmtype_list.Text) = "" Then
            MsgBox("请选择开关量名称", , PROJECT_TITLE_STRING)
            cb_alarmtype_list.Focus()
            Exit Sub
        End If
        If Trim(cb_alarmlist_tag.Text) = "" Then
            MsgBox("请选择报警标志", , PROJECT_TITLE_STRING)
            cb_alarmlist_tag.Focus()
            Exit Sub
        End If
        DBOperation.OpenConn(conn)
        alarmcontent = Trim(cb_alarmtype_list.Text) & "_" & Trim(tb_alarmstring.Text)
        alarmtag = Val(cb_alarmlist_tag.Text)

        '查询开关量标志
        sql = "select * from kaiguan_list where control_box_name='" & m_control_box_name & "' and kaiguan_name='" & Trim(cb_alarmtype_list.Text) & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "AddType_Click", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs.RecordCount > 0 Then
            kaiguantag = rs.Fields("kaiguan_tag").Value
        Else
            rs.Close()
            rs = Nothing
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        sql = "select * from alarm_typelist where control_box_name='" & m_control_box_name & "' and alarm_type like'" & Trim(cb_alarmtype_list.Text) & "%'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "AddType_Click", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        'If rs.RecordCount > 0 Then
        '    MsgBox("报警类型已存在，无需重复输入", , PROJECT_TITLE_STRING)
        '    If rs.State = 1 Then
        '        rs.Close()
        '        rs = Nothing
        '    End If
        '    conn.Close()
        '    conn = Nothing
        '    Exit Sub
        'End If

        rs.AddNew()
        rs.Fields("alarm_type").Value = alarmcontent
        rs.Fields("alarm_tag").Value = alarmtag
        rs.Fields("check_tag").Value = 0
        rs.Fields("control_box_name").Value = m_control_box_name
        rs.Fields("kaiguan_tag").Value = kaiguantag
        rs.Update()

        '增加操作记录
        Com_inf.Insert_Operation("新增报警类型：" & m_control_box_name & " ," & alarmcontent)

        Me.Alarm_typelistTableAdapter.FillBy_box(Me.TypeListDataSet.alarm_typelist, m_control_box_name)
        selectAlarm()
    End Sub

    ''' <summary>
    ''' 删除打钩的报警信息
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_delalarm_type_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_delalarm_type.Click
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String
        Dim del_string As String = ""
        ' Dim rs As New ADODB.Recordset

        Dim i As Integer = 0
        msg = ""
        DBOperation.OpenConn(conn)
        If MsgBox("是否删除勾选项?", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.No Then
            Exit Sub
        End If
        While i < dgv_alarmlist.RowCount
            If dgv_alarmlist.Rows(i).Cells("checkid").Value = 1 Then
                sql = "delete from alarm_typelist where id='" & dgv_alarmlist.Rows(i).Cells("id").Value & "'"
                DBOperation.ExecuteSQL(conn, sql, msg)
                del_string &= dgv_alarmlist.Rows(i).Cells("Alarmtype").Value
            End If
            i += 1
        End While

        MsgBox("删除自定义报警类型成功", , PROJECT_TITLE_STRING)

        '增加操作记录
        If del_string.Length > 80 Then
            del_string = Mid(del_string, 1, 80) & "..."
        End If
        Com_inf.Insert_Operation("删除自定义报警类型：" & del_string)

        Me.Alarm_typelistTableAdapter.FillBy_box(Me.TypeListDataSet.alarm_typelist, m_control_box_name)
        selectAlarm()
        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    ''' 保存设置的开关报警设置
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_savealarm_type_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_savealarm_type.Click
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim i As Integer = 0
        Dim alarmstring As String
        Dim choose_alarm_string As String = ""  '选择报警类型

        msg = ""
        If MsgBox("是否保存开关报警的设置", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.No Then
            Exit Sub
        End If
        DBOperation.OpenConn(conn)
        '先将所有的开关报警标志置为0
        sql = "update alarm_typelist set check_tag=0 where control_box_name='" & m_control_box_name & "'"
        DBOperation.ExecuteSQL(conn, sql, msg)

        While i < clb_alarmtype_list.CheckedItems.Count
            alarmstring = clb_alarmtype_list.CheckedItems(i)
            sql = "update alarm_typelist set check_tag=1 where control_box_name='" & m_control_box_name & "' and alarm_type='" & alarmstring & "'"
            DBOperation.ExecuteSQL(conn, sql, msg)

            choose_alarm_string &= alarmstring & " "
            i += 1
        End While

        MsgBox("报警类型选择成功", , PROJECT_TITLE_STRING)

        '增加操作记录
        If choose_alarm_string.Length > 80 Then
            choose_alarm_string = Mid(choose_alarm_string, 1, 80) & "..."

        End If

        Com_inf.Insert_Operation("选择报警类型：" & m_control_box_name & choose_alarm_string)

        Me.Alarm_typelistTableAdapter.FillBy_box(Me.TypeListDataSet.alarm_typelist, m_control_box_name)

        conn.Close()
        conn = Nothing
    End Sub

    Private Sub cb_checkall_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_checkall.Click
        Dim i As Integer = 0

        If cb_checkall.Checked = True Then
            While i < clb_alarmtype_list.Items.Count
                clb_alarmtype_list.SetItemChecked(i, True)
                i += 1
            End While

        Else
            While i < clb_alarmtype_list.Items.Count
                clb_alarmtype_list.SetItemChecked(i, False)
                i += 1
            End While
        End If
    End Sub

 
    Private Sub SelectAlarmType_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        g_windowclose = 1
    End Sub
End Class