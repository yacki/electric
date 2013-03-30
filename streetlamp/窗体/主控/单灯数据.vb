Public Class 单灯数据
    Public g_control_box_name As String
    Private m_checkvalue As String  '按区域查询的区域名称
    Private Sub 单灯监测_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.Icon = New Icon("图片\favicon.ico", 50, 50)
        Timer1.Start()
    End Sub

    Public Sub SetControlBoxListDelegate(ByVal control_box_name As String)
        '右边的路灯统计信息
        g_control_box_name = control_box_name
        Dim probleminf As String = ""
        Dim lampkind As String = ""
        Dim lampstate As String = ""  '灯的开关情况，div_time_id列>=8为开，<8的为关
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim lamp_pointinfor As String = ""
        Dim msg As String
        Dim sql As String
        Dim controllamp As New control_lamp
        msg = ""
        sql = "select A.control_box_name,B.* from control_box A LEFT JOIN lamp_street B ON A.control_box_id=B.control_box_id  where A.control_box_name='" & control_box_name & "'  AND B.type_id<>'31'"
        DBOperation.OpenConn(conn)
        rs = DBOperation.SelectSQL(conn, sql, msg)
        Dim row As Integer
        row = 0
        dgv_lamp_state_list.Rows.Clear()
        Me.dgv_lamp_state_list.Refresh()
        System.Threading.Thread.Sleep(1000)
        While rs.EOF = False
            dgv_lamp_state_list.Rows.Add()
            If IsDBNull(rs.Fields("jiechuqi_id").Value) = False Then
                lampkind = Trim(rs.Fields("jiechuqi_id").Value)
            Else
                lampkind = 1
            End If

            If lampkind < 3 Then
                '表示两字节的，则把状态显示中的功率，功率因数，电压值置为“-”
                Me.dgv_lamp_state_list.Rows(row).Cells("presure_lamp").Value = "-"
                Me.dgv_lamp_state_list.Rows(row).Cells("yinshu_lamp").Value = "-"
                Me.dgv_lamp_state_list.Rows(row).Cells("power_lamp").Value = "-"
            Else
                '六字节的存在
                Me.dgv_lamp_state_list.Rows(row).Cells("presure_lamp").Value = rs.Fields("presure_l").Value
                Me.dgv_lamp_state_list.Rows(row).Cells("yinshu_lamp").Value = rs.Fields("presure_end").Value
                Me.dgv_lamp_state_list.Rows(row).Cells("power_lamp").Value = rs.Fields("power").Value
            End If
            Me.dgv_lamp_state_list.Rows(row).Cells("current_lamp").Value = rs.Fields("current_l").Value
            Me.dgv_lamp_state_list.Rows(row).Cells("control_box_name").Value = rs.Fields("control_box_name").Value
            Me.dgv_lamp_state_list.Rows(row).Cells("type_string").Value = Com_inf.Get_Type_String(Val(rs.Fields("type_id").Value))
            ' Com_inf.Get_DengGan(Trim(rs.Fields("lamp_id").Value))
            If IsDBNull(rs.Fields("lamp_pointinfor").Value) = False Then
                lamp_pointinfor = rs.Fields("lamp_pointinfor").Value
                If Trim(lamp_pointinfor) <> "" Then
                    lamp_pointinfor = "  位置:" & lamp_pointinfor
                Else
                    lamp_pointinfor = ""
                End If
            Else
                lamp_pointinfor = ""
            End If
            Me.dgv_lamp_state_list.Rows(row).Cells("lamp_id_part").Value = Val(Mid(Trim(rs.Fields("lamp_id").Value), 1, 4)).ToString & "-" & Val(Mid(Trim(rs.Fields("lamp_id").Value), 5, 2)).ToString & "-" & Val(Mid(Trim(rs.Fields("lamp_id").Value), 7, 5)).ToString
            If lampkind = "3" Then
                probleminf = controllamp.get_probleminf(rs.Fields("state").Value)
                If rs.Fields("div_time_id").Value = "0" Then
                    '表示关闭
                    Me.dgv_lamp_state_list.Rows(row).Cells("open_close").Value = "关闭"
                Else
                    If rs.Fields("div_time_id").Value = "1" Then
                        '表示打开
                        Me.dgv_lamp_state_list.Rows(row).Cells("open_close").Value = "打开"
                    Else
                        '表示初始状态
                        Me.dgv_lamp_state_list.Rows(row).Cells("open_close").Value = "-"
                    End If
                End If
                Me.dgv_lamp_state_list.Rows(row).Cells("state_content").Value = probleminf
            Else
                If rs.Fields("div_time_id").Value = 0 Then
                    '表示关闭
                    Me.dgv_lamp_state_list.Rows(row).Cells("open_close").Value = "关闭"
                Else
                    If rs.Fields("div_time_id").Value = 1 Then
                        '表示打开
                        Me.dgv_lamp_state_list.Rows(row).Cells("open_close").Value = "打开"
                    Else
                        '表示初始状态
                        Me.dgv_lamp_state_list.Rows(row).Cells("open_close").Value = "-"
                    End If
                End If
                If rs.Fields("result").Value = 0 Then
                    If rs.Fields("state").Value = 1 Or rs.Fields("state").Value = 4 Then
                        Me.dgv_lamp_state_list.Rows(row).Cells("state_content").Value = LAMP_STATE_ON
                    Else
                        Me.dgv_lamp_state_list.Rows(row).Cells("state_content").Value = LAMP_STATE_OFF
                    End If
                Else
                    If rs.Fields("result").Value = 1 Then
                        Me.dgv_lamp_state_list.Rows(row).Cells("state_content").Value = LAMP_STATE_PROBLEM_ON
                    Else
                        If rs.Fields("result").Value = 2 Then
                            Me.dgv_lamp_state_list.Rows(row).Cells("state_content").Value = LAMP_STATE_PROBLEM_OFF
                        Else
                            If rs.Fields("result").Value = 3 Then
                                Me.dgv_lamp_state_list.Rows(row).Cells("state_content").Value = LAMP_STATE_NORETURN
                            Else
                                Me.dgv_lamp_state_list.Rows(row).Cells("state_content").Value = LAMP_STATE_CONTROL
                            End If
                        End If
                    End If
                End If
            End If
            rs.MoveNext()
            row += 1
        End While
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    ''' 时钟定时器，显示时间，控制时段(特殊时段+平时控制模式)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Dim nowtime As DateTime '当前的时间
        Dim row_lamp, colume_lamp As Integer
        nowtime = Now
        If Now.Second = 0 Then
            Try

                If dgv_lamp_state_list.CurrentCell Is Nothing Then
                    row_lamp = 0
                    colume_lamp = 0
                Else
                    row_lamp = dgv_lamp_state_list.CurrentCell.RowIndex
                    colume_lamp = dgv_lamp_state_list.CurrentCell.ColumnIndex
                End If
                Me.SetControlBoxListDelegate(g_control_box_name)
                If row_lamp > dgv_lamp_state_list.Rows.Count - 1 Then
                    row_lamp = dgv_lamp_state_list.Rows.Count - 1
                End If
                If dgv_lamp_state_list.Rows.Count > 0 Then
                    dgv_lamp_state_list.CurrentCell = dgv_lamp_state_list(colume_lamp, row_lamp)
                End If

            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        m_checkvalue = Trim(Me.ComboBox1.Text)
    End Sub

    Private Sub ComboBox1_DropDown(sender As System.Object, e As System.EventArgs) Handles ComboBox1.DropDown
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        msg = ""
        'sql = "select control_box_name from control_box"
        sql = "select distinct A.control_box_name from control_box A LEFT JOIN lamp_inf B ON A.control_box_id=B.control_box_id  where B.lamp_type_id<>'31'"
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        ComboBox1.Items.Clear()
        While rs.EOF = False
            ComboBox1.Items.Add(Trim(rs.Fields("control_box_name").Value))
            rs.MoveNext()
        End While
        rs.Close()
        rs = Nothing
        conn.Close()
        conn = Nothing
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        If Me.ComboBox1.Text = "" Then
            MsgBox("请选择主控箱名称", , PROJECT_TITLE_STRING)
            Exit Sub
        End If
        g_control_box_name = Me.ComboBox1.Text
        Me.SetControlBoxListDelegate(g_control_box_name)
    End Sub
End Class