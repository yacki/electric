Public Class 主控箱状态查询

    ''' <summary>
    ''' 载入窗体
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 主控箱状态查询_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = New Icon("图片\favicon.ico", 32, 32)
        get_state_inf()  '获取当前最新的主控箱数据

    End Sub

    ''' <summary>
    ''' 获取最新的电控箱数据
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub get_state_inf()
        Dim rs_box As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim box_statestring As String '电控箱状态
        Dim i As Integer = 0
        Dim j As Integer = 1
        Dim control_box_obj As New control_box
        Dim control_box_type As Integer

        rtb_control_box_state.Text = ""
        msg = ""
        sql = "select control_box_id,board_num,control_box_type from control_box order by control_box_id"  '将主控箱列表中的数据读出来
        DBOperation.OpenConn(conn)
        rs_box = DBOperation.SelectSQL(conn, sql, msg)
        If rs_box Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "Get_state_inf", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        box_statestring = ""
        While rs_box.EOF = False
            control_box_type = rs_box.Fields("control_box_type").Value
            If control_box_type < 4 Then
                box_statestring &= control_box_obj.Get_controlbox_tip(Trim(rs_box.Fields("control_box_id").Value), rs_box.Fields("board_num").Value) & vbCrLf & vbCrLf
            Else
                box_statestring &= control_box_obj.Get_controlbox_tipABC(Trim(rs_box.Fields("control_box_id").Value), rs_box.Fields("board_num").Value) & vbCrLf & vbCrLf

            End If

            rs_box.MoveNext()
        End While
        rtb_control_box_state.AppendText(box_statestring & vbCrLf)


        If rs_box.State = 1 Then
            rs_box.Close()
            rs_box = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    ''' 点击后刷新
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_getnew_state_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_getnew_state.Click
        get_state_inf()
    End Sub

    Private Sub 主控箱状态查询_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        g_windowclose = 1
    End Sub
End Class