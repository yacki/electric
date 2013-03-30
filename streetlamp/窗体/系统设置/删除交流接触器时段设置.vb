Public Class 删除交流接触器时段设置

    ''' <summary>
    ''' 载入窗体
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 删除交流接触器时段设置_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: 这行代码将数据加载到表“Lamp_level_list.control_level_view”中。您可以根据需要移动或移除它。
        Me.Control_level_viewTableAdapter.Fill(Me.Lamp_level_list.control_level_view)
        '设置各个窗体的图标
        Me.Icon = New Icon("图片\favicon.ico", 32, 32)
        Dim i As Integer = 0
        Dim str As String
        While i < jiaoliumod_list.RowCount
            str = Me.jiaoliumod_list.Rows(i).Cells("full_id").Value
            Me.jiaoliumod_list.Rows(i).Cells("lamp_id_short").Value = Val(Mid(str, 1, 4)).ToString & "-" & Val(Mid(str, 5, 2)).ToString & "-" & Val(Mid(str, 7, LAMP_ID_LEN)).ToString
            i += 1
        End While
        count_string.Text = "共有" & Me.jiaoliumod_list.RowCount & "项记录"

    End Sub

    ''' <summary>
    ''' 删除选中的设置项目
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub del_mod_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles del_mod.Click
        Dim i As Integer
        Dim msg As String
        Dim sql As String
        Dim conn As New ADODB.Connection

        DBOperation.OpenConn(conn)

        msg = ""
        If MsgBox("是否删除选中的项目", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.No Then

            GoTo finish
        End If
        While i < Me.jiaoliumod_list.RowCount
            If Me.jiaoliumod_list.Rows(i).Cells("id").Value = 1 Then
                sql = "delete from road_level where lamp_id='" & Me.jiaoliumod_list.Rows(i).Cells("full_id").Value & "' and div_time_level='" & Me.jiaoliumod_list.Rows(i).Cells("control_mod").Value & "'"
                DBOperation.ExecuteSQL(conn, sql, msg)

            End If
            i += 1
        End While

        Me.Control_level_viewTableAdapter.Fill(Me.Lamp_level_list.control_level_view)
        '对灯的编号进行格式化

        i = 0
        Dim str As String
        While i < jiaoliumod_list.RowCount
            str = Me.jiaoliumod_list.Rows(i).Cells("full_id").Value
            Me.jiaoliumod_list.Rows(i).Cells("lamp_id_short").Value = Val(Mid(str, 1, 4)).ToString & "-" & Val(Mid(str, 5, 2)).ToString & "-" & Val(Mid(str, 7, LAMP_ID_LEN)).ToString
            i += 1
        End While
        count_string.Text = "共有" & Me.jiaoliumod_list.RowCount & "项记录"
        '刷新
        Dim div_time_obj As New div_time_class
        div_time_obj.Div_time_show()

finish:
        conn.Close()
        conn = Nothing
    End Sub
End Class