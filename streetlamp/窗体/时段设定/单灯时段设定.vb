Public Class 单灯时段设定

    ''' <summary>
    ''' 初始化窗体（对单个进行特殊的时段设置，暂未使用）
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 单灯时段设定_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        '改变灯的编号形式
        Dim i As Integer = 0
        Dim lamp_short_time As String
        Me.Icon = New Icon("图片\favicon.ico", 32, 32)
        Me.Lamp_levelTableAdapter.Fill(Me.Lamp_level._lamp_level)
        While i < Me.lamp_time_list.RowCount
            lamp_short_time = Val(Mid(Trim(Me.lamp_time_list.Rows(i).Cells("lamp_id").Value), 1, 4)).ToString & "-" & _
             Val(Mid(Trim(Me.lamp_time_list.Rows(i).Cells("lamp_id").Value), 5, 2)).ToString & "-" & _
              Val(Mid(Trim(Me.lamp_time_list.Rows(i).Cells("lamp_id").Value), 7, LAMP_ID_LEN)).ToString
            Me.lamp_time_list.Rows(i).Cells("short_lamp_id").Value = lamp_short_time
            i += 1
        End While

        select_lamp_id()
        state.SelectedIndex = 0
        hour_start.SelectedIndex = 0
        min_start.SelectedIndex = 0
        second_start.SelectedIndex = 0
        hour_end.SelectedIndex = 0
        min_end.SelectedIndex = 0
        second_end.SelectedIndex = 0

    End Sub

    ''' <summary>
    ''' 选取所有的灯的编号
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub select_lamp_id()
        Dim rs As New ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim conn As New ADODB.Connection

        msg = ""
        sql = "select lamp_id from lamp_inf order by lamp_id"
        DBOperation.OpenConn(conn)
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "select_lamp_id", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        lamp_id_list.Items.Clear()
        While rs.EOF = False
            '将数据库里的灯的编号类型转换成1-1-1的表示形式
            lamp_id_list.Items.Add(Val(Mid(Trim(rs.Fields("lamp_id").Value), 1, 4)) & "-" & Val(Mid(Trim(rs.Fields("lamp_id").Value), 5, 2)) & "-" & Val(Mid(Trim(rs.Fields("lamp_id").Value), 7, LAMP_ID_LEN)))
            rs.MoveNext()
        End While

        If lamp_id_list.Items.Count > 0 Then
            lamp_id_list.SelectedIndex = 0
        End If

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing

        End If
        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    ''' 增加灯的编号
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub lamp_id_list_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lamp_id_list.DropDown
        select_lamp_id()
    End Sub

    ''' <summary>
    ''' 设置时段控制模式
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub time_set_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles time_set.Click
        If lamp_id_list.Text = "" Then
            MsgBox("灯的编号不可以为空")
            lamp_id_list.Focus()
            Exit Sub
        End If
        If state.Text = "" Then
            MsgBox("时控状态不可以为空")
            state.Focus()
            Exit Sub
        End If
        If hour_start.Text = "" Then
            MsgBox("开始时间不可以为空")
            hour_start.Focus()
            Exit Sub
        End If
        If min_start.Text = "" Then
            MsgBox("开始时间不可以为空")
            min_start.Focus()
            Exit Sub
        End If
        If second_start.Text = "" Then
            MsgBox("开始时间不可以为空")
            second_start.Focus()
            Exit Sub
        End If
        If hour_end.Text = "" Then
            MsgBox("结束时间不可以为空")
            hour_end.Focus()
            Exit Sub
        End If
        If min_end.Text = "" Then
            MsgBox("结束时间不可以为空")
            min_end.Focus()
            Exit Sub
        End If
        If second_end.Text = "" Then
            MsgBox("结束时间不可以为空")
            second_end.Focus()
            Exit Sub
        End If

        Dim time_start, time_end As String
        time_start = hour_start.Text & min_start.Text & second_start.Text
        time_end = hour_end.Text & min_end.Text & second_end.Text

        If time_start > time_end Then
            MsgBox("开始时间比结束时间大，请重新选择")
            hour_start.Focus()
            Exit Sub
        End If

        Dim sql As String
        Dim msg As String
        Dim conn As New ADODB.Connection
        Dim rs As New ADODB.Recordset
        Dim i As Integer = 0
        Dim lamp_short_time As String
    
        time_start = hour_start.Text & ":" & min_start.Text & ":" & second_start.Text
        time_end = hour_end.Text & ":" & min_end.Text & ":" & second_end.Text


        DBOperation.OpenConn(conn)
        msg = ""
        sql = "select lamp_id from lamp_level where lamp_id='" & Trim(full_lamp_id.Text) & "'"

        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "time_set_Click", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs.RecordCount > 0 Then
            '已经存在该灯的时段控制，则提示是否更新
            If MsgBox("该灯时段控制方式已经存在，是否增加新的时段控制?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                '增加新的时段控制信息
                sql = "insert into lamp_level (lamp_id,open_close,time_start,time_end) values('" & Trim(full_lamp_id.Text) & "','" & Trim(state.Text) & "','" & time_start & "','" & time_end & "') "
                DBOperation.ExecuteSQL(conn, sql, msg)
            End If
        Else
            '增加新的时段控制信息
            sql = "insert into lamp_level (lamp_id,open_close,time_start,time_end) values('" & Trim(full_lamp_id.Text) & "','" & Trim(state.Text) & "','" & time_start & "','" & time_end & "') "
            DBOperation.ExecuteSQL(conn, sql, msg)
        End If

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
        Me.Lamp_levelTableAdapter.Fill(Me.Lamp_level._lamp_level)
        '改变灯的编号形式
        While i < Me.lamp_time_list.RowCount
            lamp_short_time = Val(Mid(Trim(Me.lamp_time_list.Rows(i).Cells("lamp_id").Value), 1, 4)).ToString & "-" & _
             Val(Mid(Trim(Me.lamp_time_list.Rows(i).Cells("lamp_id").Value), 5, 2)).ToString & "-" & _
              Val(Mid(Trim(Me.lamp_time_list.Rows(i).Cells("lamp_id").Value), 7, LAMP_ID_LEN)).ToString
            Me.lamp_time_list.Rows(i).Cells("short_lamp_id").Value = lamp_short_time
            i += 1
        End While
    End Sub

    ''' <summary>
    ''' 删除被勾选的灯的时段控制信息
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub time_del_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles time_del.Click
        Dim i As Integer = 0
        Dim sql As String
        Dim msg As String
        Dim conn As New ADODB.Connection
        DBOperation.OpenConn(conn)
        msg = ""

        sql = ""
        While i < lamp_time_list.RowCount
            If lamp_time_list.Rows(i).Cells("del_id").Value = 1 Then
                If MsgBox("是否删除" & lamp_time_list.Rows(i).Cells("lamp_id").Value & "的时段控制信息?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    sql = "delete from lamp_level where id='" & lamp_time_list.Rows(i).Cells("id").Value & "'"
                    DBOperation.ExecuteSQL(conn, sql, msg)

                End If
            End If
            i += 1
        End While
        conn.Close()
        conn = Nothing
        Me.Lamp_levelTableAdapter.Fill(Me.Lamp_level._lamp_level)

    End Sub

    ''' <summary>
    ''' 灯的短编号改变后，将full_lamp_id的完整灯的编号也改变
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub lamp_id_list_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lamp_id_list.SelectedIndexChanged
        Dim lamp_id(2) As String
        lamp_id = Trim(Me.lamp_id_list.Text).Split("-")
        '将区域编号组合成四位编号
        While lamp_id(0).Length < 4
            lamp_id(0) = "0" & lamp_id(0)
        End While
        '将类型编号组合成两位编号
        While lamp_id(1).Length < 2
            lamp_id(1) = "0" & lamp_id(1)
        End While
        '将灯的编号转换成三位编号
        While lamp_id(2).Length < 3
            lamp_id(2) = "0" & lamp_id(2)
        End While
        Me.full_lamp_id.Text = lamp_id(0) & lamp_id(1) & lamp_id(2)


    End Sub
  
    Private Sub 单灯时段设定_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        g_windowclose = 1
    End Sub
End Class