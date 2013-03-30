Public Class 流量日志

    Private m_controlboxname As String  '电控箱名称

    ''' <summary>
    ''' 载入窗体
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 流量日志_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '设置各个窗体的图标
        Me.Icon = New Icon("图片\favicon.ico", 32, 32)
        Me.Box_GPRSTableAdapter.Fill(Me.GPRS.Box_GPRS)
        GPRS_total.Visible = False
        dtp_date_start.CustomFormat = "yyyy-MM-dd HH:mm:ss"  '开始时间格式
        dtp_date_end.CustomFormat = "yyyy-MM-dd HH:mm:ss"  '结束时间格式
        '开始日期默认为当前日期的前一天
        dtp_date_start.Value = DateAdd(DateInterval.Day, -1, Now)
        Com_inf.Select_box_name(cb_control_box_name)  '选择电控箱名称

    End Sub

    ''' <summary>
    ''' 按区域和单灯进行查询统计流量
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_find_record_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_find_record.Click
        m_controlboxname = Trim(cb_control_box_name.Text)  '电控箱名称
        Dim control_box_id As String
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String
        Dim time_start, time_end As System.DateTime
        Dim gprs As Long = 0  '统计该电控箱的GPRS流量
        Dim gprs_count As String
        Dim i As Integer = 0
        msg = ""
        sql = ""
        control_box_id = ""
        DBOperation.OpenConn(conn)
        sql = "select * from control_box where control_box_name='" & m_controlboxname & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "find_record_Click", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs.RecordCount > 0 Then
            control_box_id = Trim(rs.Fields("control_box_id").Value)  '电控箱编号

        End If
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing

        If rb_date_condition_find.Checked = True Then  '按日期查询
            If dtp_date_start.Text = "" Then  '如果开始日期为空
                MsgBox("请选择开始时间", , PROJECT_TITLE_STRING)
                dtp_date_start.Focus() '光标定位在开始日期框
                Exit Sub
            End If
            If dtp_date_start.Text = "" Then  '如果结束日期为空
                MsgBox("请选择结束时间", , PROJECT_TITLE_STRING)
                dtp_date_end.Focus()  '光标定位在结束日期框
                Exit Sub
            End If

            time_start = dtp_date_start.Value '开始时间
            time_end = dtp_date_end.Value  '结束时间
            dtp_date_start.Text = Format(time_start, "yyyy-MM-dd HH:mm:ss")  '开始日期框中为当前日期
            If rb_date_condition_find.Checked = True Then
                '选择电控箱+日期
                Me.Box_GPRSTableAdapter.FillBy_Date_box(Me.GPRS.Box_GPRS, time_start, time_end, control_box_id)
            End If

        End If
        If rb_no_date_condition_find.Checked = True Then '不按日期查询

            If rb_no_date_condition_find.Checked = True Then
                '选择电控箱
                Me.Box_GPRSTableAdapter.FillBy_box(Me.GPRS.Box_GPRS, control_box_id)
            End If
        End If


        While i < Me.GPRS_list.RowCount
            If Me.GPRS_list.Rows(i).Cells("ControlboxidDataGridViewTextBoxColumn").Value Then
                gprs += Val(Me.GPRS_list.Rows(i).Cells("GPRSDataGridViewTextBoxColumn").Value)

            End If
            i += 1
        End While
        If gprs < 1000000 Then
            gprs_count = Format(System.Convert.ToDouble(gprs) / 1000, "0.00") & "KB"
        Else
            gprs_count = Format(System.Convert.ToDouble(gprs) / 10000000, "0.00") & "MB"

        End If
        GPRS_total.Text = "区域：" & Trim(cb_control_box_name.Text) & "流量  共计" & gprs_count   '提示列表中的记录条数
        GPRS_total.Visible = True

    End Sub

    ''' <summary>
    ''' 按日期查询
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rb_date_condition_find_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_date_condition_find.Click
        If rb_date_condition_find.Checked = True Then
            dtp_date_start.Enabled = True
            dtp_date_end.Enabled = True
        End If
    End Sub

    ''' <summary>
    ''' 不按日期查询
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rb_no_date_condition_find_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_no_date_condition_find.Click
        If rb_no_date_condition_find.Checked = True Then
            dtp_date_start.Enabled = False
            dtp_date_end.Enabled = False
        End If
    End Sub

    Private Sub 流量日志_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        g_windowclose = 1
    End Sub
End Class