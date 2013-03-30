Public Class 遥测窗口
    Private m_check As Boolean = False '设置标志，防止死循环
    Public m_checklist As New ArrayList  '存放选中的主控箱名称
    Private m_rowcount As Integer
    Private m_rowid As Integer
    Private m_statestring As String
    ' Private m_controlboxid As String '主控箱编号
    Private m_controlboxname As String '主控箱名称
    Private m_boardid As String  '测量板编号
    Private m_waittime As Integer  '招测等待时间
    Delegate Sub SetDataGridview(ByVal control_box_name As String, ByVal control_box_id As String, ByVal board_id As String, ByVal rowid As String, ByVal datagridview As Windows.Forms.DataGridView, ByVal tag As Integer)     '设置DataGridview中的内容


    Public Sub SetDataGridviewDelegate(ByVal control_box_name As String, ByVal control_box_id As String, ByVal board_id As String, ByVal rowid As String, ByVal datagridview As Windows.Forms.DataGridView, ByVal tag As Integer)
        If datagridview.InvokeRequired Then
            Dim datagridviewobj As SetDataGridview = New SetDataGridview(AddressOf SetDataGridviewDelegate)
            Me.Invoke(datagridviewobj, New Object() {control_box_name, control_box_id, board_id, rowid, datagridview, tag})
        Else
            Try
                If tag = 0 Then
                    '数据正常获取
                    setresult(control_box_name, control_box_id, board_id, rowid)
                Else
                    '数据超时
                    setnoresult(control_box_name, control_box_id, board_id, rowid)
                End If
            Catch ex As Exception
                MsgBox("数据采集出错，请重试！", , PROJECT_TITLE_STRING)

            End Try



            '合并单元格

        End If
    End Sub



    'Private Sub Drawcell(ByVal e As DataGridViewCellPaintingEventArgs)
    '    If dgv_boxinf.Columns("state").Index = e.ColumnIndex And e.RowIndex >= 0 Then
    '        Dim gridBrush As SolidBrush = New SolidBrush(dgv_boxinf.GridColor)
    '        Dim backColorBrush As SolidBrush = New SolidBrush(e.CellStyle.BackColor)
    '        Using gridLinePen = New Pen(gridBrush)

    '            '擦除原来的单元格背景
    '            e.Graphics.FillRectangle(backColorBrush, e.CellBounds)
    '            '绘制线条,这些线条是单元格相互间隔的区分线条,
    '            '因为我们只对列name做处理,所以datagridview自己会处理左侧和上边缘的线条
    '            If e.RowIndex <> dgv_boxinf.RowCount - 1 Then
    '                If e.Value.ToString <> dgv_boxinf.Rows(e.RowIndex + 1).Cells(e.ColumnIndex).Value.ToString Then
    '                    e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left, e.CellBounds.Bottom - 1, _
    '                    e.CellBounds.Right - 1, e.CellBounds.Bottom - 1)
    '                    '绘制值
    '                    If e.Value IsNot System.DBNull.Value Then
    '                        e.Graphics.DrawString(e.Value.ToString, e.CellStyle.Font, _
    '                        Brushes.Crimson, e.CellBounds.X + 2, _
    '                        e.CellBounds.Y + 2, StringFormat.GenericDefault)
    '                    End If

    '                End If

    '            Else
    '                e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left, e.CellBounds.Bottom - 1, _
    '                  e.CellBounds.Right - 1, e.CellBounds.Bottom - 1)  '下边缘的线
    '                '绘制值
    '                If e.Value IsNot System.DBNull.Value Then
    '                    e.Graphics.DrawString(e.Value.ToString, e.CellStyle.Font, _
    '                    Brushes.Crimson, e.CellBounds.X + 2, _
    '                    e.CellBounds.Y + 2, StringFormat.GenericDefault)
    '                End If

    '            End If
    '            '右侧的线
    '            e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1, e.CellBounds.Top, e.CellBounds.Right - 1, _
    '            e.CellBounds.Bottom - 1)
    '            e.Handled = True
    '        End Using
    '        gridBrush.Dispose()
    '        backColorBrush.Dispose()
    '        gridBrush = Nothing
    '        backColorBrush = Nothing


    '    End If

    'End Sub

    Private Sub setnoresult(ByVal control_box_name As String, ByVal control_box_id As String, ByVal board_id As Integer, ByVal rowid As Integer)
        dgv_boxinf.Rows.Add()
        dgv_boxinf.Rows(m_rowid).Cells("id").Value = m_rowid + 1
        dgv_boxinf.Rows(m_rowid).Cells("box_id").Value = control_box_id
        dgv_boxinf.Rows(m_rowid).Cells("box_name").Value = control_box_name
        dgv_boxinf.Rows(m_rowid).Cells("board_id").Value = "-"
        dgv_boxinf.Rows(m_rowid).Cells("xiangwei").Value = "-"
        dgv_boxinf.Rows(m_rowid).Cells("presure").Value = "-"

        dgv_boxinf.Rows(m_rowid).Cells("current").Value = "-"
        dgv_boxinf.Rows(m_rowid).Cells("power").Value = "-"
        dgv_boxinf.Rows(m_rowid).Cells("power_yinshu").Value = "-"
        dgv_boxinf.Rows(m_rowid).Cells("state").Value = "-"

        dgv_boxinf.Rows(m_rowid).Cells("yaoce_state").Value = "超时"
        rtb_inf.AppendText("主控箱：" & control_box_name & "数据召测超时！" & vbCrLf)

        m_rowid += 1
    End Sub


    ''' <summary>
    ''' 获取正常数据
    ''' </summary>
    ''' <param name="control_box_name"></param>
    ''' <param name="control_box_id"></param>
    ''' <param name="board_id"></param>
    ''' <param name="rowid"></param>
    ''' <remarks></remarks>
    Private Sub setresult(ByVal control_box_name As String, ByVal control_box_id As String, ByVal board_id As Integer, ByVal rowid As Integer)
        Dim i As Integer = 0
        Dim state() As String
        Dim data_state() As String
        state = m_statestring.Split("+")
        If state.Length <> 2 Then
            Exit Sub
        End If
      
        data_state = state(1).Split(" ")
        If state(0) = "测量板数据采集超时" Then
            '表示超时没有数据，只有报警信息
            dgv_boxinf.Rows.Add()
            dgv_boxinf.Rows(m_rowid).Cells("id").Value = m_rowid + 1
            dgv_boxinf.Rows(m_rowid).Cells("box_id").Value = control_box_id
            dgv_boxinf.Rows(m_rowid).Cells("box_name").Value = control_box_name
            dgv_boxinf.Rows(m_rowid).Cells("board_id").Value = board_id
            dgv_boxinf.Rows(m_rowid).Cells("xiangwei").Value = "-"
            dgv_boxinf.Rows(m_rowid).Cells("presure").Value = "-"


            dgv_boxinf.Rows(m_rowid).Cells("current").Value = "-"
            dgv_boxinf.Rows(m_rowid).Cells("power").Value = "-"
            dgv_boxinf.Rows(m_rowid).Cells("power_yinshu").Value = "-"
            dgv_boxinf.Rows(m_rowid).Cells("state").Value = state(0)
            dgv_boxinf.Rows(m_rowid).Cells("yaoce_state").Value = "正常"
            m_rowid += 1
            GoTo finish

        End If

        If data_state.Length = 1 Then
            '表示超时没有数据，只有报警信息
            dgv_boxinf.Rows.Add()
            dgv_boxinf.Rows(m_rowid).Cells("id").Value = m_rowid + 1
            dgv_boxinf.Rows(m_rowid).Cells("box_id").Value = control_box_id
            dgv_boxinf.Rows(m_rowid).Cells("box_name").Value = control_box_name
            dgv_boxinf.Rows(m_rowid).Cells("board_id").Value = board_id
            dgv_boxinf.Rows(m_rowid).Cells("xiangwei").Value = "-"
            dgv_boxinf.Rows(m_rowid).Cells("presure").Value = "-"


            dgv_boxinf.Rows(m_rowid).Cells("current").Value = "-"
            dgv_boxinf.Rows(m_rowid).Cells("power").Value = "-"
            dgv_boxinf.Rows(m_rowid).Cells("power_yinshu").Value = "-"
            dgv_boxinf.Rows(m_rowid).Cells("state").Value = state(0)

            dgv_boxinf.Rows(m_rowid).Cells("yaoce_state").Value = "正常"
            m_rowid += 1
            GoTo finish
        End If
        If data_state.Length <> 21 And data_state.Length <> 39 Then
            Exit Sub
        End If

        While i < m_rowcount
            dgv_boxinf.Rows.Add()
            dgv_boxinf.Rows(m_rowid).Cells("id").Value = m_rowid + 1
            dgv_boxinf.Rows(m_rowid).Cells("box_id").Value = control_box_id
            dgv_boxinf.Rows(m_rowid).Cells("box_name").Value = control_box_name
            dgv_boxinf.Rows(m_rowid).Cells("board_id").Value = board_id
            If data_state.Length = 21 Then
                '小三遥数据

                If i < 2 Then
                    dgv_boxinf.Rows(m_rowid).Cells("xiangwei").Value = "A"
                    dgv_boxinf.Rows(m_rowid).Cells("presure").Value = data_state(0)
                Else

                    If i < 4 Then
                        dgv_boxinf.Rows(m_rowid).Cells("xiangwei").Value = "B"
                        dgv_boxinf.Rows(m_rowid).Cells("presure").Value = data_state(1)

                    Else
                        dgv_boxinf.Rows(m_rowid).Cells("xiangwei").Value = "C"
                        dgv_boxinf.Rows(m_rowid).Cells("presure").Value = data_state(2)

                    End If
                End If

            Else

                If i < 4 Then
                    dgv_boxinf.Rows(m_rowid).Cells("xiangwei").Value = "A"
                    dgv_boxinf.Rows(m_rowid).Cells("presure").Value = data_state(0)
                Else

                    If i < 8 Then
                        dgv_boxinf.Rows(m_rowid).Cells("xiangwei").Value = "B"
                        dgv_boxinf.Rows(m_rowid).Cells("presure").Value = data_state(1)

                    Else
                        dgv_boxinf.Rows(m_rowid).Cells("xiangwei").Value = "C"
                        dgv_boxinf.Rows(m_rowid).Cells("presure").Value = data_state(2)

                    End If
                End If

            End If


            dgv_boxinf.Rows(m_rowid).Cells("current").Value = data_state(3 + 3 * i)
            dgv_boxinf.Rows(m_rowid).Cells("power").Value = data_state(4 + 3 * i)
            dgv_boxinf.Rows(m_rowid).Cells("power_yinshu").Value = data_state(5 + 3 * i)
            dgv_boxinf.Rows(m_rowid).Cells("state").Value = state(0)
            dgv_boxinf.Rows(m_rowid).Cells("yaoce_state").Value = "正常"

            i += 1
            m_rowid += 1

        End While

        ''合并单元格
        'i = 0
        'Dim rowmergeview1 As New RowMergeView
        'While i < dgv_boxinf.ColumnCount
        '    rowmergeview1.MergeColumnNames.Add("state")
        '    i += 1
        'End While

finish:
        rtb_inf.AppendText("主控箱：" & control_box_name & " " & board_id & "号测量板数据召测完毕" & vbCrLf)

    End Sub

    Private Sub 遥测窗口_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim controlboxobj As New control_box
        'controlboxobj.set_controlbox_list(tv_yaoce_controlbox) '主控箱信息列表
        controlboxobj.Dispose()
        '设置各个窗体的图标
        Me.Icon = New Icon("图片\favicon.ico", 32, 32)

        Me.MergeColumnNames.Add("board_id")
        Me.MergeColumnNames.Add("box_name")
        Me.MergeColumnNames.Add("xiangwei")
        Me.MergeColumnNames.Add("presure")
        Me.MergeColumnNames.Add("state")
        Me.MergeColumnNames.Add("yaoce_state")

    End Sub

    ''' <summary>
    ''' 将选定的主控箱名称增加到右边的列表中
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_add_boxname_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_add_boxname.Click
        m_checklist.Clear() '清除所有选中的项目
        Dim tnRet As New TreeNode
        For Each treenode As TreeNode In tv_yaoce_controlbox.Nodes
            Com_inf.FindNode(treenode, m_checklist)
        Next
        If m_checklist.Count = 0 Then
            MsgBox("请选择需设置的主控箱名称", , PROJECT_TITLE_STRING)
            Exit Sub
        End If

        '将arraylist中的主控箱名称，显示到右边的列表中
        Com_inf.Addcontrolbox_to_Datagridview(m_checklist, dgv_yaoce_list, "yaoce_boxname")

    End Sub

    Private Sub bt_yaoce_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_yaoce.Click
        dgv_boxinf.Rows.Clear()

        If Me.BackgroundWorker_yaoce.IsBusy = False Then
            Me.BackgroundWorker_yaoce.RunWorkerAsync()

            '招测的时候有些按钮变灰
            bt_add_boxname.Enabled = False
            bt_del_boxname.Enabled = False
            bt_yaoce.Enabled = False
        Else
            MsgBox("召测正在进行,请稍后重试！", , PROJECT_TITLE_STRING)

        End If
    End Sub

    ''' <summary>
    ''' 召测开关量数据
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub sendkaiguan_data()
        Dim imei As String
        Dim i As Integer = 0
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String = ""
        Dim msg As String = ""
        Dim tag As Boolean = False
        Dim id_hex As String
        Dim id_list As New ArrayList '所有主控箱编号的列表
        Dim boxname_list As New ArrayList '所有主控箱名称
        Dim controlboxobj As New control_box
        Dim waittime = 5  '开关量等待时间
        Dim j As Integer = 0
        Dim now_time As DateTime = Now
        Dim controlboxid As String
        Dim controlboxname As String

        m_rowid = 0 '将列表中的编号初始化为0


        If m_checklist.Count = 0 Then
            MsgBox("请选择遥测主控箱", , PROJECT_TITLE_STRING)
            Exit Sub
        End If
        Dim boxname(m_checklist.Count - 1) As String
        m_checklist.CopyTo(boxname)
        DBOperation.OpenConn(conn)
        While i < m_checklist.Count
            If Me.BackgroundWorker_yaoce.CancellationPending = False Then
                sql = "select imei,control_box_id,control_box_name from Box_IMEI where control_box_name='" & boxname(i) & "'"
                rs = DBOperation.SelectSQL(conn, sql, msg)
                If rs Is Nothing Then
                    MsgBox(MSG_ERROR_STRING, , PROJECT_TITLE_STRING)
                    conn.Close()
                    conn = Nothing
                    Exit Sub
                End If
                If rs.RecordCount > 0 Then
                    controlboxid = Trim(rs.Fields("control_box_id").Value)
                    controlboxname = Trim(rs.Fields("control_box_name").Value)
                    If controlboxobj.get_communication(controlboxname) = False Then
                        '通信不正常则不测
                        m_controlboxname = controlboxname
                        Me.BackgroundWorker_yaoce.ReportProgress(30)
                        System.Threading.Thread.Sleep(1000)
                        GoTo next1
                    End If
                    id_hex = Com_inf.Dec_to_Hex(controlboxid, 4)
                    id_hex = Mid(id_hex, 1, 2) & " " & Mid(id_hex, 3, 2)

                    '发送开关数据招测
                    imei = Trim(rs.Fields("imei").Value)
                    sql = "INSERT INTO TimeControl(RoadIMEI, CMDType,CMDContent, CreateTime,HandlerFlag) values('" & imei & "', '" & HG_TYPE.HG_SET_KAIGUAN & "','" & id_hex & "','" & Now & "',10)"
                    DBOperation.ExecuteSQL(conn, sql, msg)

                    id_list.Add(id_hex)  '将主控箱编号放入列表中
                    boxname_list.Add(Trim(rs.Fields("control_box_name").Value)) '主控箱名称


                End If
            Else
                GoTo finish
            End If
next1:
            i += 1
        End While

        '发送完所有召测数据后，等待召测结果
        System.Threading.Thread.Sleep(1000)
        tag = Get_returnkaiguanvalue(id_list, boxname_list, now_time, 0)

finish:

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    ''' 召测三遥数据
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub sendsanyao_data()
        Dim imei As String
        Dim i As Integer = 0
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String = ""
        Dim msg As String = ""
        Dim tag As Boolean = False
        '   Dim id_hex As String
        Dim id_list As New ArrayList '所有主控箱编号的列表
        Dim boxname_list As New ArrayList '所有主控箱名称
        Dim controlboxobj As New control_box
        Dim waittime = 5  '开关量等待时间
        Dim controlboxid As String '主控箱编号
        Dim controlboxname As String  '主控箱名称
        Dim j As Integer = 0
        Dim now_time As DateTime = Now
        Dim boxid_hex As String

        m_rowid = 0 '将列表中的编号初始化为0


        If m_checklist.Count = 0 Then
            MsgBox("请选择遥测主控箱", , PROJECT_TITLE_STRING)
            Exit Sub
        End If
        Dim boxname(m_checklist.Count - 1) As String
        m_checklist.CopyTo(boxname)
        DBOperation.OpenConn(conn)
        While i < m_checklist.Count
            If Me.BackgroundWorker_yaoce.CancellationPending = False Then
                sql = "select imei,control_box_id,control_box_name from Box_IMEI where control_box_name='" & boxname(i) & "'"
                rs = DBOperation.SelectSQL(conn, sql, msg)
                If rs Is Nothing Then
                    MsgBox(MSG_ERROR_STRING, , PROJECT_TITLE_STRING)
                    conn.Close()
                    conn = Nothing
                    Exit Sub
                End If
                If rs.RecordCount > 0 Then
                    controlboxid = Trim(rs.Fields("control_box_id").Value)
                    boxid_hex = Com_inf.Dec_to_Hex(controlboxid, 4)
                    boxid_hex = Mid(boxid_hex, 1, 2) & " " & Mid(boxid_hex, 3, 2)
                    controlboxname = Trim(rs.Fields("control_box_name").Value)
                    If controlboxobj.get_communication(controlboxname) = False Then
                        m_controlboxname = controlboxname
                        '通信不正常则不测
                        Me.BackgroundWorker_yaoce.ReportProgress(30)

                        System.Threading.Thread.Sleep(1000)
                        GoTo next1
                    End If
                    'id_hex = Com_inf.Dec_to_Hex(m_controlboxid, 4)
                    'id_hex = Mid(id_hex, 1, 2) & " " & Mid(id_hex, 3, 2)

                    '发送三遥数据招测
                    imei = Trim(rs.Fields("imei").Value)
                    sql = "insert into TimeControl(RoadIMEI, CMDType,CMDContent, CreateTime,HandlerFlag) values('" & imei & "', '" & HG_TYPE.HG_SET_YAOCE & "','" & boxid_hex & "','" & Now & "',10)"
                    DBOperation.ExecuteSQL(conn, sql, msg)

                    id_list.Add(controlboxid)  '将主控箱编号放入列表中
                    boxname_list.Add(controlboxname) '主控箱名称


                End If
            Else
                GoTo finish
            End If
next1:
            i += 1
        End While

        '发送完所有召测数据后，等待召测结果
        System.Threading.Thread.Sleep(1000)
        tag = Get_returnvalue(id_list, boxname_list, now_time, 0)

finish:

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    ''' 遥测线程
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BackgroundWorker_yaoce_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker_yaoce.DoWork
        BackgroundWorker_yaoce.ReportProgress(21)
        Com_inf.setstatueflag()

        '召测三遥数据
        sendsanyao_data()
        BackgroundWorker_yaoce.ReportProgress(22)
        '召测开关量
        sendkaiguan_data()

        BackgroundWorker_yaoce.ReportProgress(23)
    End Sub

    ''' <summary>
    ''' 获取招测的三遥数据
    ''' </summary>
    ''' <param name="boxid"></param>
    ''' <param name="time"></param>
    ''' <param name="sendtimes">招测的次数，第一次显示，第二次不显示，两次目的是使用户点击招测后有报警立刻报警</param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Private Function Get_returnvalue(ByVal boxid As ArrayList, ByVal boxname As ArrayList, ByVal time As DateTime, ByVal sendtimes As Integer) As Boolean

        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String = ""
        Dim msg As String = ""
        Dim recString As String '接收到的数据
        Dim data() As String
        Dim controlboxobj As New control_box
        Dim problem_tag As String = ""
        Dim row_count As Integer = 0
        Dim group_num As Integer = 0  '组号
        Dim sanyao_data(56) As String
        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim t As Integer = 0
        Dim datainf As Boolean = False
        Dim id_hex As String '主控箱编号的16进制表示
        Dim boxtype As String '主控箱类型区分大小三遥

        Get_returnvalue = False
        DBOperation.OpenConn(conn)
        m_waittime = g_ycwaittime
        While m_waittime > 0
            If Me.BackgroundWorker_yaoce.CancellationPending = False Then
                Me.BackgroundWorker_yaoce.ReportProgress(m_waittime)
                t = 0
                While t < boxid.Count
                    '根据主控箱编号查询主控箱的类型，区分大小三遥
                    boxtype = controlboxobj.get_controltype(boxid(t))

                    id_hex = Com_inf.Dec_to_Hex(boxid(t), 4)
                    id_hex = Mid(id_hex, 1, 2) & " " & Mid(id_hex, 3, 2)
                    If boxtype = 2 Then
                        sql = "select * from RoadLightStatus where PackType='" & HG_TYPE.HG_GET_YAOCE & "' and HandlerFlag=3 and Createtime>'" & time & "' and StatusContent like'" & id_hex & "%' order by id desc"
                    Else
                        sql = "select * from RoadLightStatus where PackType='" & HG_TYPE.HG_GET_SMALLYAOCE & "' and HandlerFlag=3 and Createtime>'" & time & "' and StatusContent like'" & id_hex & "%' order by id desc"

                    End If
                    rs = DBOperation.SelectSQL(conn, sql, msg)
                    If rs Is Nothing Then
                        MsgBox(MSG_ERROR_STRING, , PROJECT_TITLE_STRING)
                        conn.Close()
                        conn = Nothing
                        Exit Function
                    End If
                    If rs.RecordCount > 0 Then
                        recString = Trim(rs.Fields("StatusContent").Value)
                        data = recString.Split(" ")

                        If boxtype = 3 Then
                            '判断数据是否合法，如果数据合法则继续判读否则丢弃不判
                            'datainf = controlboxobj.CheckData(data, rs.Fields("ID").Value)
                            'If datainf = False Then
                            '    '证明上传的数据为超时数据，丢弃不管
                            '    'Me.BackgroundWorker_yaoce.ReportProgress(22)
                            '    'GoTo finish
                            '    GoTo next1
                            'End If
                            '所获取的数据为小三遥数据，将数据中第三个字节的组数减1变为组号，调用通用的三遥数据分析
                            If Val(data(2)) = 0 Then
                                '表示数据失败,将该状态置为1
                                rs.Fields("handlerflag").Value = 1
                                rs.Update()
                                GoTo next2
                            End If
                            data(2) = (Val(data(2).ToString) - 1).ToString
                           
                            controlboxobj.Get_Huilu_inf_small(0, data, problem_tag, boxid(t), rs.Fields("ID").Value)

                            '分析control_box表中的数据
                            m_statestring = get_controlboxstate(0, boxid(t))
                            m_rowcount = 6
                            m_boardid = 1
                            If sendtimes = 0 Then
                                SetDataGridviewDelegate(boxname(t), boxid(t), m_boardid, m_rowcount, dgv_boxinf, 0)

                            End If
                            Get_returnvalue = True
                            ' Exit While

                        End If

                        If boxtype = 2 Then
                            '传统三遥数据
                            group_num = System.Convert.ToInt32(data(2), 16)
                            If group_num = 0 Then
                                '表示数据失败,将该状态置为1
                                rs.Fields("handlerflag").Value = 1
                                rs.Update()
                                GoTo next2
                            End If
                            j = 0
                            ReDim sanyao_data(56)
                            While j < group_num
                                sanyao_data(0) = data(0)
                                sanyao_data(1) = data(1)
                                sanyao_data(2) = j
                                i = 3
                                While i < 57
                                    sanyao_data(i) = data(i + (j) * 54)
                                    i += 1
                                End While
                                datainf = controlboxobj.CheckData(sanyao_data, rs.Fields("ID").Value)
                                If datainf = False Then
                                    '证明上传的数据为超时数据，丢弃不管
                                    j += 1
                                    Continue While
                                End If
                                controlboxobj.Get_Huilu_inf(j, group_num, sanyao_data, problem_tag, boxid(t))
                                j += 1
                            End While
                            '读取故障信息
                            controlboxobj.get_alarminf(data, 3 + group_num * 54, rs.Fields("ID").Value, boxid(t), group_num)

                            j = 0
                            While j < group_num
                                '分析control_box表中的数据
                                m_statestring = get_controlboxstate(j, boxid(t))
                                m_rowcount = 12
                                m_boardid = j + 1

                                Get_returnvalue = True
                                If sendtimes = 0 Then
                                    SetDataGridviewDelegate(boxname(t), boxid(t), m_boardid, m_rowcount, dgv_boxinf, 0)

                                End If
                                j += 1
                            End While



                        End If
                        '有上传数据，数据处理完毕后将该主控箱的编号移去
next2:
                        boxid.RemoveAt(t)
                        boxname.RemoveAt(t)

                    Else
next1:
                        t += 1
                    End If

                End While

                If t = 0 Then  '表示说有的数据全部召测完毕
                    Exit While
                End If
                m_waittime -= 1
                System.Threading.Thread.Sleep(1000)
            Else
                Exit While
            End If

        End While

        '查询超时，不正常
        If m_waittime <= 0 Then
            If sendtimes = 0 Then
                t = 0
                While t < boxname.Count
                    'm_controlboxid = boxid(t)
                    'm_controlboxname = boxname(t)
                    '超时
                    SetDataGridviewDelegate(boxname(t), boxid(t), m_boardid, m_rowcount, dgv_boxinf, 1)
                    System.Threading.Thread.Sleep(500)
                    t += 1
                End While
            End If

        End If
finish:
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing


    End Function

    ''' <summary>
    ''' 获取招测的三遥数据
    ''' </summary>
    ''' <param name="boxid"></param>
    ''' <param name="time"></param>
    ''' <param name="sendtimes">招测的次数，第一次显示，第二次不显示，两次目的是使用户点击招测后有报警立刻报警</param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Private Function Get_returnkaiguanvalue(ByVal boxid As ArrayList, ByVal boxname As ArrayList, ByVal time As DateTime, ByVal sendtimes As Integer) As Boolean

        Dim rs, rs_box, rs_record As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String = ""
        Dim msg As String = ""

        Dim controlboxobj As New control_box
        Dim problem_tag As String = "正常"
        Dim row_count As Integer = 0
        Dim group_num As Integer = 0  '组号
        Dim sanyao_data(56) As String
        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim t As Integer = 0
        Dim datainf As Boolean = False
        Dim alarm_list(2) As String '存放开关量报警的3位长度的字符串
        Dim alarmstring As String '报警字符串
        Dim alarminf() As String '故障列表
        Dim alarm_tag As Boolean = False
        Dim id_hex As String '主控箱编号的十六进制
        Dim control_box_name As String
        Dim control_box_id As String
        Dim control_box_type As Integer '主控箱的类型，目的是区分大小三遥
        Dim huilu_tag As Integer '默认的控制路数，大三遥默认前六路，小三遥默认前三路

        Get_returnkaiguanvalue = False
        DBOperation.OpenConn(conn)
        m_waittime = g_ycwaittime
        While m_waittime > 0
            If Me.BackgroundWorker_yaoce.CancellationPending = False Then
                '   Me.BackgroundWorker_yaoce.ReportProgress(m_waittime)
                t = 0
                While t < boxid.Count
                    id_hex = Com_inf.Dec_to_Hex(boxid(t), 4)
                    id_hex = Mid(id_hex, 1, 2) & " " & Mid(id_hex, 3, 2)

                    sql = "select * from RoadLightStatus where PackType='" & HG_TYPE.HG_GET_KAIGUAN & "' and HandlerFlag=3 and Createtime>'" & time.AddMinutes(-10) & "' and StatusContent like'" & boxid(t) & "%' order by id desc"
                    rs = DBOperation.SelectSQL(conn, sql, msg)
                    If rs Is Nothing Then
                        MsgBox(MSG_ERROR_STRING, , PROJECT_TITLE_STRING)
                        conn.Close()
                        conn = Nothing
                        Exit Function
                    End If
                    If rs.RecordCount > 0 Then

                        '判断开关量的合法性
                        If Trim(rs.Fields("StatusContent").Value).Split(" ").Length < 4 Then
                            GoTo next1
                        End If
                        'alarm_list = Trim(rs.Fields("StatusContent").Value).Split(" ")
                        controlboxobj.get_kaiguanString(Trim(rs.Fields("StatusContent").Value), alarm_list)

                        '2011年4月20日增加电控箱的用电类型：电池或交流电
                        sql = "select control_box_name,control_box_id,power_type,kaiguan_string,Createtime,control_box_type from control_box where control_box_id='" & alarm_list(0) & "'"
                        rs_box = DBOperation.SelectSQL(conn, sql, msg)
                        If rs_box Is Nothing Then
                            If rs.State = 1 Then
                                rs.Close()
                                rs = Nothing
                            End If
                            conn.Close()
                            conn = Nothing
                            Exit Function
                        End If
                        If rs_box.RecordCount > 0 Then
                            control_box_name = Trim(rs_box.Fields("control_box_name").Value)
                            control_box_id = Trim(rs_box.Fields("control_box_id").Value)
                            control_box_type = rs_box.Fields("control_box_type").Value
                            If control_box_type = 3 Then  '小三遥，预留3位
                                huilu_tag = 3
                            Else  '大三遥，预留6位
                                huilu_tag = 6
                            End If
                            i = 0
                            '2012年3月5日对开关量的前6个值个开关量默认配置给6个接触器,大三遥6个接触器，小三遥3个接触器
                            While i < huilu_tag
                                '根据开关量，获取实际的接触器状态
                                controlboxobj.set_huilu_actualstate(i + 1, Mid(alarm_list(1), 16 - i, 1), control_box_id, control_box_name)
                                i += 1
                            End While

                            'i = huilu_tag

                            'i = 0
                            ''2012年3月5日对开关量的前6个值个开关量默认配置给6个接触器
                            'While i < 6
                            '    controlboxobj.set_huilu_actualstate(i + 1, Mid(alarm_list(1), 16 - i, 1), control_box_id, control_box_name)
                            '    i += 1
                            'End While

                            i = 0
                            While i < alarm_list(1).Length
                                alarmstring = controlboxobj.alarm_yes_no(i + 1, Mid(alarm_list(1), 16 - i, 1), boxname(t))
                                If alarmstring <> "" Then  '报警

                                    '有报警
                                    alarminf = Trim(alarmstring).Split(" ")
                                    j = 0
                                    While j < alarminf.Length
                                        sql = "select * from kaiguan_alarm_list where control_box_name='" & boxname(t) & "' and alarm_string='" & alarminf(j) & "' and (alarm_tag=0 or alarm_tag=2)"
                                        rs_record = DBOperation.SelectSQL(conn, sql, msg)
                                        If rs_record Is Nothing Then
                                            ' g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Check_Kaiguan_Alarm" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

                                            GoTo finish
                                        End If
                                        If rs_record.RecordCount <= 0 Then
                                            alarm_tag = True
                                            '原来没有报警数据
                                            sql = "insert into kaiguan_alarm_list(alarm_string,createtime,alarm_tag," _
                                          & "control_box_name,kaiguan_tag) values('" & alarminf(j) & "','" & Now & "'," _
                                          & "0,'" & boxname(t) & "' ,'" & i + 1 & "')"
                                            DBOperation.ExecuteSQL(conn, sql, msg)
                                        Else
                                            '有原来有报警信息，则将原来的报警信息置为2，表示经过确认的
                                            alarm_tag = True
                                            sql = "update kaiguan_alarm_list set alarm_tag=2 where id='" & rs_record.Fields("id").Value & "'"
                                            DBOperation.ExecuteSQL(conn, sql, msg)

                                        End If
                                        j += 1

                                    End While


                                Else  '状态是正常的，则查询当前的报警表，有报警信息则将报警信息置1
                                    sql = "select * from kaiguan_alarm_list where control_box_name='" & boxname(t) & "' and kaiguan_tag='" & i + 1 & "' and (alarm_tag=0 or alarm_tag=2)"
                                    rs_record = DBOperation.SelectSQL(conn, sql, msg)
                                    If rs_record Is Nothing Then
                                        'g_welcomewinobj.SetTextDelegate(MSG_ERROR_STRING & "Check_Kaiguan_Alarm" & " 时间：" & Now.ToString & vbCrLf & "********************" & vbCrLf & vbCrLf, False, g_welcomewinobj.rtb_info_list)

                                        GoTo finish
                                    End If
                                    While rs_record.EOF = False
                                        alarm_tag = True
                                        '原来有报警数据，则将报警标志置为2，表示报警结束
                                        'rs_record.Fields("alarm_tag").Value = 1
                                        'rs_record.Fields("endtime").Value = Now
                                        'rs_record.Update()
                                        sql = "update kaiguan_alarm_list set alarm_tag=1,endtime='" & Now & "' where id='" & rs_record.Fields("id").Value & "'"
                                        DBOperation.ExecuteSQL(conn, sql, msg)

                                        rs_record.MoveNext()
                                    End While


                                End If
                                i += 1  '同一个主控箱中的16位开关量报警数据
                            End While

                            '更新主控箱的供电类型
                            If alarm_list(2) = 1 Then
                                ' rs_box.Fields("power_type").Value = POWERTYPE_BUTTERY

                                sql = "update control_box set power_type='" & POWERTYPE_BUTTERY & "',kaiguan_string='" & alarm_list(1) & "', Createtime='" & Now & "'  where control_box_name='" & boxname(t) & "'"
                            Else
                                '  rs_box.Fields("power_type").Value = POWERTYPE_CURRENT
                                sql = "update control_box set power_type='" & POWERTYPE_CURRENT & "',kaiguan_string='" & alarm_list(1) & "', Createtime='" & Now & "' where control_box_name='" & boxname(t) & "'"
                            End If


                            DBOperation.ExecuteSQL(conn, sql, msg)
                            'rs_box.Fields("kaiguan_string").Value = alarm_list(1)
                            'rs_box.Fields("Createtime").Value = Now
                            'rs_box.Update()



                            '记录供电情况
                            If alarm_list(2) = 1 Then
                                Setcontrolbox_Record(boxname(t), POWERTYPE_BUTTERY, "供电")
                            Else
                                Setcontrolbox_Record(boxname(t), POWERTYPE_CURRENT, "供电")
                            End If
                            '失压报警刷新
                            g_welcomewinobj.BackgroundWorker_find_state.ReportProgress(2)
                        End If


next1:

                        '将本条记录置为1
                        sql = "update RoadLightStatus set handlerflag=1 where id='" & rs.Fields("id").Value & "'"
                        DBOperation.ExecuteSQL(conn, sql, msg)
                        rs.MoveNext()

                        '有上传数据，数据处理完毕后将该主控箱的编号移去
                        boxid.RemoveAt(t)
                        boxname.RemoveAt(t)

                    Else
                        t += 1
                    End If

                End While

                If t = 0 Then  '表示说有的数据全部召测完毕
                    Exit While
                End If
                m_waittime -= 1
                System.Threading.Thread.Sleep(1000)
            Else
                Exit While
            End If

        End While

        '查询超时，不正常
        'If m_waittime <= 0 Then
        '    If sendtimes = 0 Then
        '        t = 0
        '        While t < boxname.Count
        '            m_controlboxid = boxid(t)
        '            m_controlboxname = boxname(t)
        '            '超时
        '            SetDataGridviewDelegate(boxname(t), boxid(t), m_boardid, m_rowcount, dgv_boxinf, 1)
        '            System.Threading.Thread.Sleep(500)
        '            t += 1
        '        End While
        '    End If

        'End If
finish:
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        If rs_record.State = 1 Then
            rs_record.Close()
            rs_record = Nothing
        End If
        If rs_box.State = 1 Then
            rs_box.Close()
            rs_box = Nothing
        End If
        conn.Close()
        conn = Nothing


    End Function



    '刷新列表
    Private Sub BackgroundWorker_yaoce_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker_yaoce.ProgressChanged

        '召测三遥数据
        If e.ProgressPercentage = 21 Then
            rtb_inf.AppendText("开始召测三遥数据......" & vbCrLf)

        End If

        '招测开关量数据
        If e.ProgressPercentage = 22 Then
            rtb_inf.AppendText("开始召测开关量数据......" & vbCrLf)
        End If

        '数据召测完毕
        If e.ProgressPercentage = 23 Then
            rtb_inf.AppendText("数据召测完毕" & vbCrLf)
        End If

        If e.ProgressPercentage <= 20 Then
            ' rtb_inf.AppendText("正在召测主控箱：" & m_controlboxname & "的数据...(" & (g_ycwaittime + 1 - e.ProgressPercentage) & "秒)" & vbCrLf)
            rtb_inf.AppendText("正在召测数据...(" & (g_ycwaittime + 1 - e.ProgressPercentage) & "秒)" & vbCrLf)

        End If

        If e.ProgressPercentage = 30 Then
            rtb_inf.AppendText("主控箱：" & m_controlboxname & "未连接无法召测" & vbCrLf)
        End If
        rtb_inf.Select(rtb_inf.Text.Length, 0)
        rtb_inf.ScrollToCaret()
    End Sub

    ''' <summary>
    ''' 删除被选中的主控箱
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_del_boxname_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_del_boxname.Click
        Dim i As Integer = 0
        Dim boxlist As New ArrayList

        While i < dgv_yaoce_list.RowCount
            If dgv_yaoce_list.Rows(i).Cells("checkid").Value = 0 Then
                boxlist.Add(Trim(dgv_yaoce_list.Rows(i).Cells("yaoce_boxname").Value))

            End If
            i += 1
        End While
        dgv_yaoce_list.Rows.Clear()
        i = 0
        While i < boxlist.Count
            dgv_yaoce_list.Rows.Add()
            dgv_yaoce_list.Rows(i).Cells("yaoce_boxname").Value = boxlist(i)
            i += 1
        End While

        '重新载入m_checklist
        i = 0
        m_checklist.Clear()
        While i < Me.dgv_yaoce_list.Rows.Count
            m_checklist.Add(Trim(Me.dgv_yaoce_list.Rows(i).Cells("yaoce_boxname").Value))
            i += 1
        End While


    End Sub

    Private Sub bt_stop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_stop.Click
        If Me.BackgroundWorker_yaoce.IsBusy = True Then
            Me.BackgroundWorker_yaoce.CancelAsync()
        End If
    End Sub


    Private Sub tv_yaoce_controlbox_AfterCheck(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tv_yaoce_controlbox.AfterCheck
        Dim controlboxobj As New control_box
        If m_check = False Then
            m_check = controlboxobj.setChild(e.Node)
        End If

        controlboxobj.setparent(e.Node)
        m_check = False
    End Sub

    Private Sub BackgroundWorker_yaoce_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker_yaoce.RunWorkerCompleted
        bt_add_boxname.Enabled = True
        bt_del_boxname.Enabled = True

        bt_yaoce.Enabled = True
    End Sub

    Private Sub 遥测窗口_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If Me.BackgroundWorker_yaoce.IsBusy = True Then
            MsgBox("线程正在运行，请稍后关闭", , PROJECT_TITLE_STRING)
            e.Cancel = True
        End If
        g_windowclose = 1
    End Sub

    Private Sub clear_text_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles clear_text.Click
        rtb_inf.Text = ""
    End Sub

    Private Sub dgv_boxinf_CellPainting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellPaintingEventArgs)

        If e.RowIndex > -1 And e.ColumnIndex > -1 Then
            DrawCell(e)
        End If
        ' dgv_boxinf.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True

    End Sub

    '/ <summary>
    '/ DrawCell
    '/ </summary>
    '/ <param name="e"></param>
    Private Sub DrawCell(ByVal e As DataGridViewCellPaintingEventArgs)
        If e.CellStyle.Alignment = DataGridViewContentAlignment.NotSet Then
            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        End If
        Dim gridBrush As Brush = New SolidBrush(dgv_boxinf.GridColor)
        'Dim backBrush As SolidBrush = New SolidBrush(e.CellStyle.BackColor)
        Dim backBrush As SolidBrush = New SolidBrush(Color.White)
        Dim fontBrush As SolidBrush = New SolidBrush(e.CellStyle.ForeColor)
        Dim cellwidth As Integer
        Dim UpRows As Integer = 0
        Dim DownRows As Integer = 0
        Dim count As Integer = 0
        If e.Value IsNot Nothing Then
            If Me.MergeColumnNames.Contains(dgv_boxinf.Columns(e.ColumnIndex).Name) And e.RowIndex <> -1 Then
                cellwidth = e.CellBounds.Width
                Dim gridLinePen As Pen = New Pen(gridBrush)
                Dim curValue As String = CType(e.Value, String)
                IIf(curValue Is Nothing, "", e.Value.ToString().Trim())
                Dim curSelected As String = CType(dgv_boxinf.CurrentRow.Cells(e.ColumnIndex).Value, String)
                IIf(curSelected Is Nothing, "", dgv_boxinf.CurrentRow.Cells(e.ColumnIndex).Value.ToString().Trim())
                'If Not String.IsNullOrEmpty(curValue) Then
                Dim i As Integer
                For i = e.RowIndex To dgv_boxinf.Rows.Count - 1 Step i + 1
                    If dgv_boxinf.Rows(i).Cells(e.ColumnIndex).Value IsNot Nothing Then
                        If dgv_boxinf.Rows(i).Cells(e.ColumnIndex).Value.ToString().Equals(curValue) Then

                            DownRows = DownRows + 1
                            If e.RowIndex <> i Then
                                cellwidth = cellwidth
                                IIf(cellwidth < dgv_boxinf.Rows(i).Cells(e.ColumnIndex).Size.Width, cellwidth, dgv_boxinf.Rows(i).Cells(e.ColumnIndex).Size.Width)
                            End If
                        Else
                            Exit For
                        End If
                    Else
                        Exit For
                    End If

                Next

                Dim j As Integer
                For j = e.RowIndex To 0 Step j - 1
                    If dgv_boxinf.Rows(j).Cells(e.ColumnIndex).Value.ToString().Equals(curValue) Then

                        UpRows = UpRows + 1
                        If e.RowIndex <> j Then
                            cellwidth = cellwidth
                            IIf(cellwidth < dgv_boxinf.Rows(j).Cells(e.ColumnIndex).Size.Width, cellwidth, dgv_boxinf.Rows(j).Cells(e.ColumnIndex).Size.Width)
                        End If
                    Else
                        Exit For
                    End If

                Next

                count = DownRows + UpRows - 1
                If count < 2 Then
                    Return
                End If
                'End If
                If dgv_boxinf.Rows(e.RowIndex).Selected Then
                    backBrush.Color = e.CellStyle.SelectionBackColor
                    fontBrush.Color = e.CellStyle.SelectionForeColor
                End If

                e.Graphics.FillRectangle(backBrush, e.CellBounds)

                PaintingFont(e, cellwidth, UpRows, DownRows, count)
                If DownRows = 1 Then
                    e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right - 1, e.CellBounds.Bottom - 1)
                    count = 0
                End If

                e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1, e.CellBounds.Top, e.CellBounds.Right - 1, e.CellBounds.Bottom - 1)

                e.Handled = True
            End If
        End If

    End Sub

    '/ <summary>
    '/ PaintingFont
    '/ </summary>
    Private Sub PaintingFont(ByVal e As System.Windows.Forms.DataGridViewCellPaintingEventArgs, ByVal cellwidth As Integer, ByVal UpRows As Integer, ByVal DownRows As Integer, ByVal count As Integer)
        Dim fontBrush As SolidBrush = New SolidBrush(e.CellStyle.ForeColor)
        Dim fontheight As Integer = CType(e.Graphics.MeasureString(e.Value.ToString(), e.CellStyle.Font).Height, Integer)
        Dim fontwidth As Integer = CType(e.Graphics.MeasureString(e.Value.ToString(), e.CellStyle.Font).Width, Integer)
        Dim cellheight As Integer = e.CellBounds.Height

        If e.CellStyle.Alignment = DataGridViewContentAlignment.BottomCenter Then
            e.Graphics.DrawString(CType(e.Value, String), e.CellStyle.Font, fontBrush, CType(e.CellBounds.X + (cellwidth - fontwidth) / 2, Single), e.CellBounds.Y + cellheight * DownRows - fontheight)
        ElseIf e.CellStyle.Alignment = DataGridViewContentAlignment.BottomLeft Then
            e.Graphics.DrawString(CType(e.Value, String), e.CellStyle.Font, fontBrush, e.CellBounds.X, e.CellBounds.Y + cellheight * DownRows - fontheight)
        ElseIf e.CellStyle.Alignment = DataGridViewContentAlignment.BottomRight Then
            e.Graphics.DrawString(CType(e.Value, String), e.CellStyle.Font, fontBrush, e.CellBounds.X + cellwidth - fontwidth, e.CellBounds.Y + cellheight * DownRows - fontheight)
        ElseIf e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter Then
            e.Graphics.DrawString(CType(e.Value, String), e.CellStyle.Font, fontBrush, CType(e.CellBounds.X + (cellwidth - fontwidth) / 2, Single), CType(e.CellBounds.Y - cellheight * (UpRows - 1) + (cellheight * count - fontheight) / 2, Single))
        ElseIf e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft Then
            'e.Graphics.DrawString(CType(e.Value, String), e.CellStyle.Font, fontBrush, e.CellBounds.X, CType(e.CellBounds.Y - cellheight * (UpRows - 1) + (cellheight * count - fontheight) / 2, Single))
            'e.Graphics.DrawString(CType(e.Value, String), e.CellStyle.Font, fontBrush, New RectangleF(e.CellBounds.X, CType(e.CellBounds.Y - cellheight * (UpRows - 1) + (cellheight * count - fontheight) / 2, Single), e.CellBounds.X + CType((cellwidth - fontwidth) / 2, Single), CType(e.CellBounds.Y - cellheight * (UpRows - 1) + (cellheight * count - fontheight) / 2, Single)))
            e.Graphics.DrawString(CType(e.Value, String), e.CellStyle.Font, fontBrush, New RectangleF(e.CellBounds.X, CType(e.CellBounds.Y - cellheight * (UpRows - 1) + (cellheight * count - fontheight) / 2, Single), e.CellBounds.Width, CType(e.CellBounds.Y - cellheight * (UpRows - 1) + (cellheight * count - fontheight) / 2, Single)))
            'e.CellStyle.WrapMode = DataGridViewTriState.True

        ElseIf e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight Then
            e.Graphics.DrawString(CType(e.Value, String), e.CellStyle.Font, fontBrush, e.CellBounds.X + cellwidth - fontwidth, CType(e.CellBounds.Y - cellheight * (UpRows - 1) + (cellheight * count - fontheight) / 2, Single))
        ElseIf e.CellStyle.Alignment = DataGridViewContentAlignment.TopCenter Then
            e.Graphics.DrawString(CType(e.Value, String), e.CellStyle.Font, fontBrush, e.CellBounds.X + CType((cellwidth - fontwidth) / 2, Single), e.CellBounds.Y - cellheight * (UpRows - 1))
        ElseIf e.CellStyle.Alignment = DataGridViewContentAlignment.TopLeft Then
            e.Graphics.DrawString(CType(e.Value, String), e.CellStyle.Font, fontBrush, e.CellBounds.X, e.CellBounds.Y - cellheight * (UpRows - 1))
        ElseIf e.CellStyle.Alignment = DataGridViewContentAlignment.TopRight Then
            e.Graphics.DrawString(CType(e.Value, String), e.CellStyle.Font, fontBrush, e.CellBounds.X + cellwidth - fontwidth, e.CellBounds.Y - cellheight * (UpRows - 1))
        Else
            e.Graphics.DrawString(CType(e.Value, String), e.CellStyle.Font, fontBrush, e.CellBounds.X + CType((cellwidth - fontwidth) / 2, Single), CType(e.CellBounds.Y - cellheight * (UpRows - 1) + (cellheight * count - fontheight) / 2, Single))
        End If
        

    End Sub

    '/ <summary>
    '/ MergeColumnNames
    '/ </summary>
    Public Property MergeColumnNames() As List(Of String)
        Get
            Return _mergecolumnname
        End Get
        Set(ByVal Value As List(Of String))
            _mergecolumnname = Value
        End Set
    End Property
    Private _mergecolumnname As List(Of String) = New List(Of String)()

End Class