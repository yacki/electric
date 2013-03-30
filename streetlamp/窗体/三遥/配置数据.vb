Public Class 配置数据

    Private m_configtag As Integer
    Private m_checklist As New ArrayList
    Private m_controlbox As New ArrayList
    Dim m_boxname As String '主控箱名称
    Dim m_string As String
    Private m_check As Boolean = False '设置标志，防止死循环

    Private Structure boxinfstruct
        Dim control_box_name As String  '主控箱名
        Dim control_box_id As String  '主控箱编号
        Dim imei As String  'imei

    End Structure

    Private Sub 配置数据_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = New Icon("图片\favicon.ico", 32, 32)
        Dim controlboxobj As New control_box
        controlboxobj.set_controlbox_list(tv_configvalue) '主控箱信息列表
        m_configtag = 1
    End Sub


    Private Sub bt_readconfig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_readconfig.Click
        If Me.BackgroundWorkerconfigvalue.IsBusy = False Then
            If rb_roadid.Checked = True Then
                m_configtag = 1
            End If
            If rb_divtime.Checked = True Then
                m_configtag = 2
            End If
            If rb_time.Checked = True Then
                m_configtag = 3
            End If

            If m_checklist.Count > 0 Then
                m_checklist.Clear()
            End If
            Dim tnRet As New TreeNode
            For Each treenode As TreeNode In tv_configvalue.Nodes
                Com_inf.FindNode(treenode, m_checklist)
            Next

            If m_controlbox.Count > 0 Then
                m_controlbox.Clear()
            End If

            Me.BackgroundWorkerconfigvalue.RunWorkerAsync()
        Else
            MsgBox("系统配置正在进行，请稍后重试...", , PROJECT_TITLE_STRING)

        End If
    End Sub

    Private Sub BackgroundWorkerconfigvalue_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorkerconfigvalue.DoWork
        If m_configtag = 1 Then
            '向主控箱询问路段号
            read_roadid()
        End If

        If m_configtag = 2 Then
            '向主控箱询问时段控制
            read_divtime()
        End If

        If m_configtag = 3 Then
            '向主控箱询问时间
            read_time()
        End If
    End Sub

    ''' <summary>
    ''' 读取主控箱的时段控制
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub read_time()
        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim sql, msg As String
        Dim conn As New ADODB.Connection
        Dim controlboxobj As New control_box
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        Dim nowtime As DateTime = Now
        msg = ""

        If m_checklist.Count = 0 Then
            MsgBox("请选择主控箱", , PROJECT_TITLE_STRING)
            Exit Sub
        End If
        While i < m_checklist.Count

            If controlboxobj.get_communication(m_checklist(i)) = False Then
                m_boxname = m_checklist(i)
                m_checklist.RemoveAt(i)
                '通信不正常则不测

                Me.BackgroundWorkerconfigvalue.ReportProgress(5)

                System.Threading.Thread.Sleep(1000)
                Continue While
            End If

            '获取该主控箱的IMEI
            Dim imei_string As String
            imei_string = Me.get_imei(m_checklist(i))


            '设置好接触器整年的控制命令后，将命令写入到数据库中
            sql = "insert into TimeControl(RoadIMEI, CMDType, CreateTime,HandlerFlag) values('" & imei_string & "','" & HG_TYPE.HG_ASK_TIME & "','" & nowtime & "','" & CONTROL_BOX_TYPE2_FLAG & "')"
            DBOperation.ExecuteSQL(conn, sql, msg)

next1:
            i += 1
        End While

        conn.Close()
        conn = Nothing

        '发送完所有命令后，等待回复
        get_timereturn(nowtime)
    End Sub

    Private Sub get_timereturn(ByVal gettime As DateTime)
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim i As Integer = 0
        Dim state() As String  '返回的状态
        Dim j As Integer = 0
        Dim boxid_hex As String '主控箱的16进制标号
        'Dim imei As String 'IMEI号
        Dim inf As New boxinfstruct
        Dim time_string As String

        msg = ""
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If

        While i < CONTROL_WAIT_TIME
            If Me.BackgroundWorkerconfigvalue.CancellationPending = False Then
                j = 0
                If m_controlbox.Count = 0 Then
                    Exit While
                End If
                m_string = "正在获取主控器时间信息......" & i + 1 & vbCrLf
                Me.BackgroundWorkerconfigvalue.ReportProgress(4)
                While j < m_controlbox.Count

                    inf = m_controlbox(j)
                    boxid_hex = Com_inf.Dec_to_Hex(inf.control_box_id, 4)
                    boxid_hex = Mid(boxid_hex, 1, 2) & " " & Mid(boxid_hex, 3, 2)


                    sql = "select * from RoadLightStatus where PackType='" & HG_TYPE.HG_ACK_TIME & "'" _
                                     & " and Createtime>'" & gettime & "' and HandlerFlag=3 and StatusContent like '" & boxid_hex & "%'"

                    rs = DBOperation.SelectSQL(conn, sql, msg)
                    If rs Is Nothing Then
                        MsgBox(MSG_ERROR_STRING & "get_back_configtimeinf", , PROJECT_TITLE_STRING)
                        conn.Close()
                        conn = Nothing
                        Exit Sub
                    End If
                    If rs.RecordCount > 0 Then

                        state = Trim(rs.Fields("StatusContent").Value).Split(" ")
                        time_string = System.Convert.ToInt32(state(2), 16) & ":" & System.Convert.ToInt32(state(3), 16) & ":" & System.Convert.ToInt32(state(4), 16)
                        m_string = inf.control_box_name & "获取当前时间为" & time_string & vbCrLf & vbCrLf
                        g_welcomewinobj.Insert_configinf(inf.control_box_name & "获取当前时间为" & time_string, 0, inf.control_box_id, inf.control_box_name)


                        Me.BackgroundWorkerconfigvalue.ReportProgress(4)

                        '将查找到的数据置为1
                        rs.Fields("HandlerFlag").Value = 1
                        rs.Update()

                        m_controlbox.RemoveAt(j)

                    Else
                        j += 1
                    End If
                End While
                i += 1
                System.Threading.Thread.Sleep(1000)

            Else
                Exit While
            End If

         
        End While
        If i >= CONTROL_WAIT_TIME Then
            j = 0
            While j < m_controlbox.Count
                inf = m_controlbox(j)
                m_string = inf.control_box_name & "获取当前时间超时 时间：" & Now & vbCrLf & vbCrLf
                Me.BackgroundWorkerconfigvalue.ReportProgress(4)
                'g_welcomewinobj.SetTextDelegate(m_boxname & "配置" & " " & m_configtitle & "超时 时间：" & Now & vbCrLf, False, config_string)
                '将配置信息保存
                'MsgBox(m_boxname & "配置" & " " & m_configtitle & "超时", , PROJECT_TITLE_STRING)
                g_welcomewinobj.Insert_configinf(inf.control_box_name & "获取当前时间超时", 0, inf.control_box_id, inf.control_box_name)

                j += 1
            End While

        End If

finish:
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    ''' 读取主控箱的时段控制
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub read_divtime()
        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim sql, msg As String
        Dim conn As New ADODB.Connection
        Dim controlboxobj As New control_box
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        Dim nowtime As DateTime = Now
        msg = ""

        If m_checklist.Count = 0 Then
            MsgBox("请选择主控箱", , PROJECT_TITLE_STRING)
            Exit Sub
        End If
        While i < m_checklist.Count

            If controlboxobj.get_communication(m_checklist(i)) = False Then
                m_boxname = m_checklist(i)
                m_checklist.RemoveAt(i)
                '通信不正常则不测

                Me.BackgroundWorkerconfigvalue.ReportProgress(5)

                System.Threading.Thread.Sleep(1000)
                Continue While
            End If

            '获取该主控箱的IMEI
            Dim imei_string As String
            imei_string = Me.get_imei(m_checklist(i))


            '设置好接触器整年的控制命令后，将命令写入到数据库中
            sql = "insert into TimeControl(RoadIMEI, CMDType, CreateTime,HandlerFlag) values('" & imei_string & "','" & HG_TYPE.HG_ASK_TIMECONTROL & "','" & nowtime & "','" & CONTROL_BOX_TYPE2_FLAG & "')"
            DBOperation.ExecuteSQL(conn, sql, msg)

next1:
            i += 1
        End While

        conn.Close()
        conn = Nothing

        '发送完所有命令后，等待回复
        get_divtimereturn(nowtime)
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="gettime"></param>
    ''' <remarks></remarks>
    Private Sub get_divtimereturn(ByVal gettime As DateTime)
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim i As Integer = 0
        Dim state As String '返回的状态
        Dim j As Integer = 0
        Dim boxid_hex As String '主控箱的16进制标号
        'Dim imei As String 'IMEI号
        Dim inf As New boxinfstruct
        Dim time_string As String
        Dim boxstate As String

        msg = ""
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If

        While i < CONTROL_WAIT_TIME
            If Me.BackgroundWorkerconfigvalue.CancellationPending = False Then
                j = 0
                If m_controlbox.Count = 0 Then
                    Exit While
                End If
                m_string = "正在获取时段控制信息......" & i + 1 & vbCrLf
                Me.BackgroundWorkerconfigvalue.ReportProgress(4)
                While j < m_controlbox.Count

                    inf = m_controlbox(j)
                    boxid_hex = Com_inf.Dec_to_Hex(inf.control_box_id, 4)
                    boxid_hex = Mid(boxid_hex, 1, 2) & " " & Mid(boxid_hex, 3, 2)


                    sql = "select * from RoadLightStatus where PackType='" & HG_TYPE.HG_ACK_TIMECONTROL & "'" _
                                     & " and Createtime>'" & gettime & "' and HandlerFlag=3 and StatusContent like'" & boxid_hex & "%'"

                    rs = DBOperation.SelectSQL(conn, sql, msg)
                    If rs Is Nothing Then
                        MsgBox(MSG_ERROR_STRING & "get_back_configtimeinf", , PROJECT_TITLE_STRING)
                        conn.Close()
                        conn = Nothing
                        Exit Sub
                    End If
                    If rs.RecordCount > 0 Then

                        state = Mid(Trim(rs.Fields("StatusContent").Value), 7)
                        time_string = Com_inf.controlorder_string(Mid(state, 4), System.Convert.ToInt32(Mid(state, 1, 2), 16))  '分析时间
                        boxstate = Com_inf.get_boxstate(inf.control_box_name) '主控箱的状态
                        If state = boxstate Then
                            '记录的主控箱和读取的主控箱的信息一致
                            m_string = inf.control_box_name & "获取时段控制信息与配置信息一致，读取数据为:" & vbCrLf & time_string & "读取时间：" & Now & vbCrLf & vbCrLf

                            g_welcomewinobj.Insert_configinf(inf.control_box_name & "获取时段控制信息与配置信息一致", 0, inf.control_box_id, inf.control_box_name)


                        Else
                            m_string = inf.control_box_name & "获取时段控制信息与配置信息不一致，读取数据为" & vbCrLf & time_string & "读取时间：" & Now & vbCrLf & vbCrLf

                            g_welcomewinobj.Insert_configinf(inf.control_box_name & "获取时段控制信息与配置信息不一致", 0, inf.control_box_id, inf.control_box_name)

                        End If

                        Me.BackgroundWorkerconfigvalue.ReportProgress(4)

                        '将查找到的数据置为1
                        rs.Fields("HandlerFlag").Value = 1
                        rs.Update()

                        m_controlbox.RemoveAt(j)

                    Else
                        j += 1
                    End If
                End While
                i += 1
                System.Threading.Thread.Sleep(1000)

            Else
                Exit While
            End If

          
        End While
        If i >= CONTROL_WAIT_TIME Then
            j = 0
            While j < m_controlbox.Count
                inf = m_controlbox(j)
                m_string = inf.control_box_name & "获取时段控制信息超时 时间：" & Now & vbCrLf & vbCrLf
                Me.BackgroundWorkerconfigvalue.ReportProgress(4)
                'g_welcomewinobj.SetTextDelegate(m_boxname & "配置" & " " & m_configtitle & "超时 时间：" & Now & vbCrLf, False, config_string)
                '将配置信息保存
                'MsgBox(m_boxname & "配置" & " " & m_configtitle & "超时", , PROJECT_TITLE_STRING)
                g_welcomewinobj.Insert_configinf(inf.control_box_name & "获取时段控制信息超时", 0, inf.control_box_id, inf.control_box_name)

                j += 1
            End While

        End If

finish:
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    ''' 读取主控箱中配置的路段号
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub read_roadid()
        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim sql, msg As String
        Dim conn As New ADODB.Connection
        Dim controlboxobj As New control_box
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        Dim nowtime As DateTime = Now
        msg = ""

        If m_checklist.Count = 0 Then
            MsgBox("请选择主控箱", , PROJECT_TITLE_STRING)
            Exit Sub
        End If
        While i < m_checklist.Count

            If controlboxobj.get_communication(m_checklist(i)) = False Then
                m_boxname = m_checklist(i)
                m_checklist.RemoveAt(i)
                '通信不正常则不测

                Me.BackgroundWorkerconfigvalue.ReportProgress(5)

                System.Threading.Thread.Sleep(1000)
                Continue While
            End If

            '获取该主控箱的IMEI
            Dim imei_string As String
            imei_string = Me.get_imei(m_checklist(i))


            '设置好接触器整年的控制命令后，将命令写入到数据库中
            sql = "insert into TimeControl(RoadIMEI, CMDType, CreateTime,HandlerFlag) values('" & imei_string & "','" & HG_TYPE.HG_ASK_ROADID & "','" & nowtime & "','" & CONTROL_BOX_TYPE2_FLAG & "')"
            DBOperation.ExecuteSQL(conn, sql, msg)

next1:
            i += 1
        End While

        conn.Close()
        conn = Nothing

        '发送完所有命令后，等待回复
        get_roadidreturn(nowtime)

    End Sub

    ''' <summary>
    ''' 通过主控箱名称获取IMEI
    ''' </summary>
    ''' <param name="control_box_name"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function get_imei(ByVal control_box_name As String) As String
        Dim sql As String
        Dim conn As New ADODB.Connection
        Dim rs As New ADODB.Recordset
        Dim msg As String
        get_imei = ""
        msg = ""
        sql = "select * from Box_IMEI where control_box_name='" & control_box_name & "'"
        If DBOperation.OpenConn(conn) = False Then
            get_imei = ""
            Exit Function
        End If
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            conn.Close()
            conn = Nothing
        End If
        If rs.RecordCount > 0 Then
            get_imei = Trim(rs.Fields("IMEI").Value)
            Dim controlboxid As String = Trim(rs.Fields("control_box_id").Value)

            Dim inf As New boxinfstruct
            inf.control_box_id = controlboxid
            inf.imei = get_imei
            inf.control_box_name = control_box_name
            m_controlbox.Add(inf)


        End If

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing

    End Function


    Private Sub get_roadidreturn(ByVal gettime As DateTime)
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim i As Integer = 0
        Dim state As String '返回的状态
        Dim j As Integer = 0
        Dim boxid_hex As String '主控箱的16进制标号
        'Dim imei As String 'IMEI号
        Dim inf As New boxinfstruct

        msg = ""
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If

        While i < CONTROL_WAIT_TIME
            If Me.BackgroundWorkerconfigvalue.CancellationPending = False Then
                j = 0
                If m_controlbox.Count = 0 Then
                    Exit While
                End If

                While j < m_controlbox.Count

                    inf = m_controlbox(j)
                    boxid_hex = Com_inf.Dec_to_Hex(inf.control_box_id, 4)
                    boxid_hex = Mid(boxid_hex, 1, 2) & " " & Mid(boxid_hex, 3, 2)


                    sql = "select * from RoadLightStatus where PackType='" & HG_TYPE.HG_ACK_ROADID & "'" _
                                     & " and Createtime>'" & gettime & "' and HandlerFlag=3 and information='" & inf.imei & "'"

                    rs = DBOperation.SelectSQL(conn, sql, msg)
                    If rs Is Nothing Then
                        MsgBox(MSG_ERROR_STRING & "get_back_configtimeinf", , PROJECT_TITLE_STRING)
                        conn.Close()
                        conn = Nothing
                        Exit Sub
                    End If
                    If rs.RecordCount > 0 Then

                        state = Trim(rs.Fields("StatusContent").Value).Split(" ")(12) & Trim(rs.Fields("StatusContent").Value).Split(" ")(13)
                        state = System.Convert.ToInt32(state, 16)
                        While state.Length < 4
                            state = "0" & state
                        End While

                        m_string = inf.control_box_name & "获取路段号为" & state & " 时间：" & Now & vbCrLf & vbCrLf
                        Me.BackgroundWorkerconfigvalue.ReportProgress(4)

                        '将查找到的数据置为1
                        rs.Fields("HandlerFlag").Value = 1
                        rs.Update()

                        m_controlbox.RemoveAt(j)
                        g_welcomewinobj.Insert_configinf(inf.control_box_name & "获取路段号为" & state, 0, inf.control_box_id, inf.control_box_name)

                    Else
                        j += 1
                    End If
                End While
                m_string = "正在获取路段号信息......" & i + 1 & vbCrLf
                Me.BackgroundWorkerconfigvalue.ReportProgress(4)
                i += 1
                System.Threading.Thread.Sleep(1000)

            Else
                Exit While
            End If


          
        End While
        If i >= CONTROL_WAIT_TIME Then
            j = 0
            While j < m_controlbox.Count
                inf = m_controlbox(j)
                m_string = inf.control_box_name & "获取路段号超时 时间：" & Now & vbCrLf & vbCrLf
                Me.BackgroundWorkerconfigvalue.ReportProgress(4)
                'g_welcomewinobj.SetTextDelegate(m_boxname & "配置" & " " & m_configtitle & "超时 时间：" & Now & vbCrLf, False, config_string)
                '将配置信息保存
                'MsgBox(m_boxname & "配置" & " " & m_configtitle & "超时", , PROJECT_TITLE_STRING)
                g_welcomewinobj.Insert_configinf(inf.control_box_name & "获取路段号超时", 0, inf.control_box_id , inf.control_box_name)

                j += 1
            End While

        End If

finish:
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub



    Private Sub BackgroundWorkerconfigvalue_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorkerconfigvalue.ProgressChanged
     
        If e.ProgressPercentage = 4 Then
            rtb_returnvalue.AppendText(m_string)
            rtb_returnvalue.Select(rtb_returnvalue.Text.Length, 0)
            rtb_returnvalue.ScrollToCaret()
        End If

        If e.ProgressPercentage = 5 Then
            rtb_returnvalue.AppendText("主控箱：" & m_boxname & "未连接无法配置" & vbCrLf)
        End If
    End Sub

    Private Sub 配置数据_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If Me.BackgroundWorkerconfigvalue.IsBusy = True Then
            MsgBox("线程正在运行，请稍后关闭", , PROJECT_TITLE_STRING)
            e.Cancel = True
        End If
        g_windowclose = 1
    End Sub

    Private Sub bt_stopconfig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_stopconfig.Click
        If Me.BackgroundWorkerconfigvalue.IsBusy = True Then
            Me.BackgroundWorkerconfigvalue.CancelAsync()
        End If
    End Sub

    Private Sub bt_clearconfig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_clearconfig.Click
        rtb_returnvalue.Text = ""
    End Sub

    Private Sub tv_configvalue_AfterCheck(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tv_configvalue.AfterCheck
        Dim controlboxobj As New control_box
        If m_check = False Then
            m_check = controlboxobj.setChild(e.Node)
        End If

        controlboxobj.setparent(e.Node)
        m_check = False
    End Sub
End Class