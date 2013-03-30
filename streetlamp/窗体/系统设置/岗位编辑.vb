Public Class 岗位编辑

    ''' <summary>
    ''' 载入窗口
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 岗位编辑_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '设置各个窗体的图标
        Me.Icon = New Icon("图片\favicon.ico", 32, 32)
        Me.GangweiTableAdapter.Fill(Me.Gangwei._gangwei)

    End Sub

    ''' <summary>
    ''' 增加岗位名称
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_add.Click
        If tb_gangwei_title.Text = "" Then
            MsgBox("岗位名称不可以为空", , PROJECT_TITLE_STRING)
            tb_gangwei_title.Focus()
            Exit Sub
        End If

        If tb_gangwei_title.TextLength > 10 Then
            MsgBox("岗位名称长度大于10", , PROJECT_TITLE_STRING)
            tb_gangwei_title.Focus()
            Exit Sub
        End If

        If tb_gangwei_des.Text = "" Then
            MsgBox("岗位描述不可以为空", , PROJECT_TITLE_STRING)
            tb_gangwei_des.Focus()
            Exit Sub
        End If

        If tb_gangwei_des.TextLength > 20 Then
            MsgBox("岗位描述长度大于20", , PROJECT_TITLE_STRING)
            tb_gangwei_des.Focus()
            Exit Sub
        End If

        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String
        Dim rs As New ADODB.Recordset

        DBOperation.OpenConn(conn)
        msg = ""
        sql = "select * from gangwei where gangwei='" & Trim(tb_gangwei_title.Text) & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "Add_Click", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs.RecordCount > 0 Then
            If MsgBox("该岗位名称已存在，是否更新描述", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.Yes Then
                '编辑描述
                rs.Fields("description").Value = Trim(tb_gangwei_des.Text)
                rs.Update()

            End If
        Else
            sql = "insert into gangwei(gangwei,description) values('" & Trim(tb_gangwei_title.Text) & "','" & Trim(tb_gangwei_des.Text) & "')"
            DBOperation.ExecuteSQL(conn, sql, msg)

            MsgBox("岗位新增成功", , PROJECT_TITLE_STRING)
            '增加操作记录
            Com_inf.Insert_Operation("新增岗位" & Trim(dgv_gangweilist.Text))

        End If
        rs.Close()
        rs = Nothing
        conn.Close()
        conn = Nothing

        Me.GangweiTableAdapter.Fill(Me.Gangwei._gangwei)

    End Sub


    ''' <summary>
    ''' 双击删除
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgv_gangweilist_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgv_gangweilist.DoubleClick
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        msg = ""
        DBOperation.OpenConn(conn)
        If MsgBox("是否删除" & Me.dgv_gangweilist.CurrentRow.Cells("GangweiDataGridViewTextBoxColumn").Value, MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            sql = "delete from gangwei where ID='" & Me.dgv_gangweilist.CurrentRow.Cells("IDDataGridViewTextBoxColumn").Value & "'"
            DBOperation.ExecuteSQL(conn, sql, msg)
            '增加操作记录
            Com_inf.Insert_Operation("删除岗位" & Me.dgv_gangweilist.CurrentRow.Cells("GangweiDataGridViewTextBoxColumn").Value)
        End If

        conn.Close()
        conn = Nothing
        Me.GangweiTableAdapter.Fill(Me.Gangwei._gangwei)
    End Sub

    Private Sub 岗位编辑_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        g_windowclose = 1
    End Sub
End Class