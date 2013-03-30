Public Class 清空数据库

    ''' <summary>
    ''' 删除所选择的表
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_clear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_clear.Click
        Dim database_name As String '数据库名称
        database_name = ""


        If dtp_date_start.Text = "" Then
            MsgBox("请选择开始时间", , PROJECT_TITLE_STRING)
            Exit Sub
        End If
        If dtp_date_end.Text = "" Then
            MsgBox("请选择结束时间", , PROJECT_TITLE_STRING)
            Exit Sub
        End If

        Dim conn As New ADODB.Connection
        Dim rs As New ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim del_string As String

        msg = ""
        sql = ""
        del_string = ""
        DBOperation.OpenConn(conn)
        If Me.cb_database.Text = "系统日志表" Then
            database_name = "hand_control_record"
            sql = "delete from hand_control_record where control_time>='" & dtp_date_start.Value & "' and control_time<='" & dtp_date_end.Value & "'"

            del_string = "删除系统日志表"
        End If
        If Me.cb_database.Text = "设备日志表" Then
            database_name = "lamp_state_list"
            sql = "delete from hand_control_record where control_time>='" & dtp_date_start.Value & "' and control_time<='" & dtp_date_end.Value & "'"
            del_string = "删除设备日志表"
        End If
        'If Me.cb_database.Text = "流量日志表" Then
        '    database_name = "Box_GPRS"
        '    sql = "delete from Box_GPRS where Time>='" & dtp_date_start.Value & "' and Time<='" & dtp_date_end.Value & "'"

        '    del_string = "删除流量日志表"

        'End If
        If Me.cb_database.Text = "报警历史表" Then
            database_name = "lamp_inf_record"
            sql = "delete from lamp_inf_record where date>='" & dtp_date_start.Value & "' and date<='" & dtp_date_end.Value & "'"

            del_string = "删除报警历史表"

        End If
        If Me.cb_database.Text = "数据状态表" Then
            database_name = "RoadLightStatus"
            sql = "delete from RoadLightStatus where Createtime>='" & dtp_date_start.Value & "' and Createtime<='" & dtp_date_end.Value & "'"

            del_string = "删除数据状态表"

        End If
        If Me.cb_database.Text = "系统控制表" Then
            database_name = "RoadLightControl"
            sql = "delete from RoadLightControl where Createtime>='" & dtp_date_start.Value & "' and Createtime<='" & dtp_date_end.Value & "'"

            del_string = "删除系统控制表"


        End If
        If sql <> "" Then
            If MsgBox("删除后数据将不可恢复，请确认是否删除表" & Trim(Me.cb_database.Text) & "表中的内容！！", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                DBOperation.ExecuteSQL(conn, sql, msg)
                MsgBox("数据删除成功", , PROJECT_TITLE_STRING)
                '增加操作记录
                Com_inf.Insert_Operation(del_string & ": " & dtp_date_start.Value & "-" & dtp_date_end.Value)


            End If

        End If
        If Me.cb_database.Text = "报警历史表" Then
            ''首页的报警信息
            g_welcomewinobj.get_boxprobleminf()

        End If


        conn.Close()
        conn = Nothing

    End Sub

    ''' <summary>
    ''' 载入窗体
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 维护数据表_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '设置各个窗体的图标
        Me.Icon = New Icon("图片\favicon.ico", 32, 32)
        dtp_date_start.CustomFormat = "yyyy-MM-dd HH:mm:ss"  '开始时间格式
        dtp_date_end.CustomFormat = "yyyy-MM-dd HH:mm:ss"  '结束时间格式

        '开始日期默认为当前日期的前一天
        dtp_date_start.Value = DateAdd(DateInterval.Day, -1, Now)
    End Sub

    Private Sub 清空数据库_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        g_windowclose = 1
    End Sub
End Class