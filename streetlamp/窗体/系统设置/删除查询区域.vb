''' <summary>
''' 删除定位的区域
''' </summary>
''' <remarks></remarks>

Public Class 删除查询区域
    ''' <summary>
    ''' 窗体载入函数
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 删除查询街道_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: 这行代码将数据加载到表“Street_position.street_position_view”中。您可以根据需要移动或移除它。
        '设置各个窗体的图标
        Me.Icon = New Icon("图片\favicon.ico", 32, 32)
        Me.Street_position_viewTableAdapter.Fill(Me.Street_position.street_position_view)  '查询街道列表

    End Sub

    ''' <summary>
    ''' 双击区域条目，提示是否删除，是则删除该条目
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgv_street_position_list_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgv_street_position_list.MouseDoubleClick
        Dim sql As String
        Dim msg As String
        Dim conn As New ADODB.Connection
        Dim rs As New ADODB.Recordset
        DBOperation.OpenConn(conn)

        msg = ""
        '确认是否删除街道的定位信息
        If MsgBox("是否删除电控箱" & Trim(dgv_street_position_list.CurrentRow.Cells("control_box_name").Value) & "的定位信息？", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            sql = "delete from street_position where control_box_name='" & Trim(dgv_street_position_list.CurrentRow.Cells("control_box_name").Value) & "'"
            DBOperation.ExecuteSQL(conn, sql, msg) '删除街道的定位信息
            MsgBox("定位信息删除成功", , PROJECT_TITLE_STRING)

            '增加操作记录
            Com_inf.Insert_Operation("删除查询区域：" & Trim(dgv_street_position_list.CurrentRow.Cells("control_box_name").Value))


        End If
        Me.Street_position_viewTableAdapter.Fill(Me.Street_position.street_position_view)  '重新载入定位街道列表
        ''主控界面上刷新下拉框
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


        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    Private Sub 删除查询区域_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        g_windowclose = 1
    End Sub
End Class