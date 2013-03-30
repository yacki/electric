Public Class 增加特殊时段控制
    Private m_id As Integer  '每个时段模式中的控制时段编号

    ''' <summary>
    ''' 载入特殊时段控制窗体
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 增加特殊时段控制_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '设置各个窗体的图标
        Me.Icon = New Icon("图片\favicon.ico", 32, 32)
        m_id = 1 '初始化为1
        start_time.CustomFormat = "yyyy-MM-dd HH:mm:ss  "  '查询条件中开始日期的格式

    End Sub

    ''' <summary>
    ''' 修改的方式为每个控制模式任意增加时间段及模式
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Add_divlevel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Add_divlevel.Click
        If Me.start_time.Text = "" Then
            MsgBox("开始时间不可以为空", , PROJECT_TITLE_STRING)
            Me.start_time.Focus()
            Exit Sub
        End If
        If mod_value.Text = "" Then
            MsgBox("模式不可以为空", , PROJECT_TITLE_STRING)
            Me.mod_value.Focus()
        End If

        Me.Add_modlist.Rows.Add()
        Me.Add_modlist.Rows(m_id - 1).Cells("id").Value = m_id
        Me.Add_modlist.Rows(m_id - 1).Cells("time").Value = Trim(start_time.Text)
        Me.Add_modlist.Rows(m_id - 1).Cells("moduel").Value = Trim(mod_value.Text)

        m_id += 1  '时段编号增加1
    End Sub

    ''' <summary>
    ''' 根据列表中的时段划分，增加一个时段或修改一个时段
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub input_hms_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles input_hms.Click
        If Me.Add_modlist.RowCount <= 0 Then
            MsgBox("控制模式列表为空", , PROJECT_TITLE_STRING)
            Exit Sub

        End If

        If mod_level_choose.Text = "" Then
            MsgBox("请选择控制模式名称", , PROJECT_TITLE_STRING)
            Me.mod_level_choose.Focus()
            Exit Sub
        End If
        If mod_level_choose.TextLength > 10 Then
            MsgBox("控制模式名称长度大于10", , PROJECT_TITLE_STRING)
            Me.mod_level_choose.Focus()
            Exit Sub
        End If

        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String
        Dim i As Integer

        msg = ""
        i = 0
        sql = "select * from Special_div_time where name='" & Trim(Me.mod_level_choose.Text) & "'"
        DBOperation.OpenConn(conn)
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
                sql = "delete from Special_div_time where name='" & Trim(mod_level_choose.Text) & "'"
                DBOperation.ExecuteSQL(conn, sql, msg)
            Else
                GoTo finish

            End If

        End If
        While i < Me.Add_modlist.RowCount
            rs.AddNew()
            rs.Fields("Time").Value = Trim(Me.Add_modlist.Rows(i).Cells("time").Value)
            rs.Fields("mod").Value = Trim(Me.Add_modlist.Rows(i).Cells("moduel").Value)
            rs.Fields("gonglv").Value = 100
            rs.Fields("diangan").Value = Trim(gonglv1.Text)  '电感
            rs.Fields("name").Value = Trim(mod_level_choose.Text)
            rs.Update()
            i += 1
        End While
        MsgBox("模式添加成功", , PROJECT_TITLE_STRING)
        g_welcomewinobj.init_div_list()
        g_welcomewinobj.init_special_div_list()
        Dim div_time_obj As New div_time_class
        div_time_obj.Div_time_show()
     
finish:

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing

    End Sub
End Class