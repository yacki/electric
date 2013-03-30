Public Class 抄表
    Dim m_controlboxobj As New control_box
    Private m_checklist As New ArrayList
    Private m_check As Boolean = False '设置标志，防止死循环
    Private m_imeilist As New ArrayList '记录选中的IMEI
    ' Private m_getmeterid_cmd As String '获取编号的命令
    Private m_gettag As Integer  '0表示获取编号，1表示获取电能
    Private m_datatable As New DataTable
    Delegate Sub SetDataGridview(ByVal row As Integer, ByVal text As String, ByVal datagridview As Windows.Forms.DataGridView, ByVal clear As System.Boolean)        '设置DataGridview中的内容
    Private m_xlApp As Microsoft.Office.Interop.Excel.Application
    Private m_xlBook As Microsoft.Office.Interop.Excel.Workbook
    Private m_xlSheet As Microsoft.Office.Interop.Excel.Worksheet

    Public Structure m_boxinf
        Dim IMEI As String   'IMEI
        Dim control_box_id As String  '主控箱编号
        Dim control_box_name As String '主控箱名称
        Dim rowid As Integer '行数
        Dim metertype As String '表的类型
        Dim bianbi As Integer '变比
        Dim meterid As String '表的编号
    End Structure

    Public Sub SetDataGridviewDelegate(ByVal row As Integer, ByVal text As String, ByVal datagridview As Windows.Forms.DataGridView, ByVal clear As System.Boolean)
        If datagridview.InvokeRequired Then
            Dim datagridviewobj As SetDataGridview = New SetDataGridview(AddressOf SetDataGridviewDelegate)
            Me.Invoke(datagridviewobj, New Object() {row, text, datagridview, clear})
        Else
            If clear = True Then
                datagridview.Rows.Clear()

            Else
                datagridview.Rows(row).Cells("powermeter_id_getvalue").Value = text

            End If


        End If
    End Sub

    Private Sub 抄表_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: 这行代码将数据加载到表“StreetlampDataSetpowermeter.controlbox_power”中。您可以根据需要移动或移除它。
        Me.Controlbox_powerTableAdapter.Fill(Me.StreetlampDataSetpowermeter.controlbox_power)
        Me.Icon = New Icon("图片\favicon.ico", 32, 32)
        m_controlboxobj.set_controlbox_list(tv_all_controlbox) '主控箱信息列表
        m_controlboxobj.set_controlbox_list(tv_box_inf_list) '主控箱信息列表

        m_datatable.Columns.Add("control_box_name")
        m_datatable.Columns.Add("power_readdata")
        m_datatable.Columns.Add("power_data")
        m_datatable.Columns.Add("get_time")

        dtp_date_start.Value = Now.AddMonths(-1)
        dtp_date_start.CustomFormat = "yyyy-MM-dd HH:mm:ss  "  '查询条件中开始日期的格式
        dtp_date_end.CustomFormat = "yyyy-MM-dd HH:mm:ss" '查询条件中结束日期的格式

    End Sub

    Private Sub BackgroundWorkergetdata_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorkergetdata.DoWork
        If m_gettag = 0 Then
            '获取电表的编号
            get_meterid()

        Else
            If m_gettag = 1 Then
                '获取电量
                get_meterdata()

            Else
                meterdata_excel()
            End If

        End If
    End Sub

    ''' <summary>
    ''' 导出excel表
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub meterdata_excel()
        Dim rowIndex, colIndex As Integer
        Dim row_id As Integer
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        m_xlApp = (New Microsoft.Office.Interop.Excel.Application)

        rowIndex = 1
        colIndex = 0

        m_xlBook = m_xlApp.Workbooks().Add
        m_xlSheet = m_xlBook.Worksheets("sheet1")

        m_xlApp.Cells(1, 1) = "电能表"
        m_xlApp.Rows(1).RowHeight = 50
        m_xlApp.Rows(1).Font.Size = 18
        m_xlApp.Rows(1).Font.Bold = True

        Dim dt As New DataTable
        m_xlApp.Cells(2, 1) = "编号"
        m_xlApp.Cells(2, 2) = "总抄码"
        m_xlApp.Cells(2, 3) = "抄码差额"
        m_xlApp.Cells(2, 4) = "总电量"
        m_xlApp.Cells(2, 5) = "电量差额"
        m_xlApp.Cells(2, 6) = "读表时间"
        m_xlApp.Rows(2).Font.Bold = True
        m_xlApp.Rows(2).font.size = 9
        m_xlApp.Rows(2).RowHeight = 30

        row_id = 3
        Dim i As Integer = 0


        Dim row_count As Integer
        row_count = dgv_datarecord.RowCount
        Dim progress_percentage As Integer

        While i < row_count
            progress_percentage = i * 100 / row_count + 1
            If progress_percentage > 100 Then
                progress_percentage = 100
            End If
            'Me.BackgroundWorkerexcel.ReportProgress(progress_percentage)
            m_xlApp.Cells(row_id, 1) = (i + 1).ToString
            m_xlApp.Cells(row_id, 2) = System.Convert.ToString(dgv_datarecord.Rows(i).Cells("read_data").Value)
            m_xlApp.Cells(row_id, 3) = System.Convert.ToString(dgv_datarecord.Rows(i).Cells("readdata_cha").Value)
            m_xlApp.Cells(row_id, 4) = System.Convert.ToString(dgv_datarecord.Rows(i).Cells("powerdata").Value)
            m_xlApp.Cells(row_id, 5) = System.Convert.ToString(dgv_datarecord.Rows(i).Cells("powerdata_cha").Value)
            m_xlApp.Cells(row_id, 6) = System.Convert.ToString(dgv_datarecord.Rows(i).Cells("data_time").Value)

            row_id += 1
            i = i + 1

        End While

        With m_xlSheet
            .Range(.Cells(1, 1), .Cells(1, 6)).Merge()
            .Range(.Cells(2, 1), .Cells(2, 1)).ColumnWidth = 20
            .Range(.Cells(2, 2), .Cells(2, 6)).ColumnWidth = 7


            .Range(.Cells(1, 1), .Cells(row_id - 1, 6)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter


            .Range(.Cells(2, 1), .Cells(1, 6)).Font.Name = "宋体"
            '设标题为宋体字
            .Range(.Cells(2, 1), .Cells(1, 6)).Font.Bold = "True"
            '标题字体加粗
            .Range(.Cells(1, 1), .Cells(row_id - 1, 6)).Borders.LineStyle = 1
            '设表格边框样式
            .Range(.Cells(3, 1), .Cells(row_id - 1, 6)).Font.Size = 9
            '设置表格数据的字号

        End With

    End Sub

    ''' <summary>
    ''' 获取电表电量
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub get_meterdata()
        Dim i As Integer = 0
        Dim imei As String
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String
        Dim nowtime As DateTime
        Dim inf As m_boxinf
        Dim metertype As String
        Dim meterid As String '电表编号
        Dim cmd_string As String '获取的命令

        nowtime = Now
        msg = ""
        sql = ""
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        While i < m_imeilist.Count
            inf = m_imeilist(i)
            '先判断有没有编号，如果没有编号则没有返回值
            If inf.meterid = "" Then
                i += 1

                m_datatable.Rows(i)("control_box_name") = inf.control_box_name
                m_datatable.Rows(i)("power_readdata") = "-"
                m_datatable.Rows(i)("power_data") = "-"
                m_datatable.Rows(i)("get_time") = "-"

                '将该要查的电控箱号删除
                m_imeilist.RemoveAt(i)

                Continue While

            End If
            '将所有选择的主控箱的取电表编号的命令发送下去

            imei = inf.IMEI
            metertype = inf.metertype
            meterid = changetodcd(inf.meterid)
          

            If metertype = "97" Then  '97规约
                cmd_string = "68 " & meterid & "68 01 02 43 C3 "
                cmd_string = cmd_string & get_cstring(Trim(cmd_string)) & " 16 16"
                sql = "insert into TimeControl(RoadIMEI, CMDType, CMDContent, HandlerFlag, CreateTime)" _
                & " values('" & imei & "','" & HG_TYPE.HG_ASK_POWERDATA97 & "','" & cmd_string & "','" & CONTROL_BOX_TYPE2_FLAG & "','" & nowtime & "')"
            Else '07规约
                cmd_string = "68 " & meterid & "68 11 04 33 33 34 33 "
                cmd_string = cmd_string & get_cstring(Trim(cmd_string)) & " 16 16"
                sql = "insert into TimeControl(RoadIMEI, CMDType, CMDContent, HandlerFlag, CreateTime)" _
                & " values('" & imei & "','" & HG_TYPE.HG_ASK_POWERDATA07 & "','" & cmd_string & "','" & CONTROL_BOX_TYPE2_FLAG & "','" & nowtime & "')"

            End If

            DBOperation.ExecuteSQL(conn, sql, msg)

            i += 1
        End While

        get_retureDatavalue(nowtime)

        conn.Close()
        conn = Nothing
    End Sub

    Private Sub get_retureDatavalue(ByVal createtime As DateTime)
        Dim inf As New m_boxinf
        Dim i As Integer = 0
        Dim waittime As Integer = g_ycwaittime
        Dim controlboxid As String
        Dim sql As String = ""
        Dim msg As String = ""
        Dim conn As New ADODB.Connection
        Dim rs As New ADODB.Recordset
        Dim metertype As String
        Dim data() As String
        Dim start_id As Integer = 2  '有两个字节的路段号，所以起始编号从2开始
        Dim metervalue As String '电表电能
        Dim j As Integer = 0
        Dim powerdata As Double
        Dim num As Byte


        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If

        While waittime > 0
            i = 0
            If i >= m_imeilist.Count Then
                Exit While
            End If
            If Me.BackgroundWorkergetdata.CancellationPending = True Then
                Exit While
            End If

            While i < m_imeilist.Count
                inf = m_imeilist(i)
                metertype = inf.metertype
                controlboxid = Com_inf.Dec_to_Hex(inf.control_box_id, 4)
                controlboxid = Mid(controlboxid, 1, 2) & " " & Mid(controlboxid, 3, 2)
                If metertype = "97" Then
                    sql = "select * from RoadLightStatus where StatusContent like '" & controlboxid & "%'" _
                    & "and CreateTime>'" & createtime & "' and PackType='" & HG_TYPE.HG_ACK_POWERDATA97 & "' and HandlerFlag=3"
                Else
                    sql = "select * from RoadLightStatus where StatusContent like '" & controlboxid & "%'" _
                    & "and CreateTime>'" & createtime & "' and PackType='" & HG_TYPE.HG_ACK_POWERDATA07 & "' and HandlerFlag=3"

                End If
                rs = DBOperation.SelectSQL(conn, sql, msg)
                If rs Is Nothing Then
                    conn.Close()
                    conn = Nothing
                    Exit Sub
                End If
                If rs.RecordCount > 0 Then
                    data = Trim(rs.Fields("StatusContent").Value).Split(" ")
                    While data(start_id) <> "68"
                        start_id += 1
                    End While
                    If inf.metertype = "97" Then
                        '验证97表号数据是否合法
                        If check_data(start_id, data, "43", "C3", "", "") = True Then
                            metervalue = ""
                            While j < 4
                                num = System.Convert.ToByte(data(start_id + 15 - j), 16)
                                metervalue = metervalue & getbcdcode(num - &H33)
                                j += 1
                            End While
                            '将获取的电能存入到列表中
                            powerdata = System.Convert.ToDouble(metervalue / 100)

                            m_datatable.Rows.Add()
                            m_datatable.Rows(i)("control_box_name") = inf.control_box_name
                            m_datatable.Rows(i)("power_readdata") = powerdata
                            m_datatable.Rows(i)("power_data") = powerdata * inf.bianbi
                            m_datatable.Rows(i)("get_time") = Now

                            '将数据记录到数据库中
                            sql = "insert into powerdata_record(control_box_name,power_readdata,powerdata,get_time) values('" & inf.control_box_name & "','" & powerdata & "','" & powerdata * inf.bianbi & "','" & Now & "')"
                            DBOperation.ExecuteSQL(conn, sql, msg)

                            '将该查找信息删除
                            m_imeilist.RemoveAt(i)

                            rs.Fields("HandlerFlag").Value = 1
                            rs.Update()
                            Continue While
                        End If

                    Else
                        '验证07表号数据是否合法
                        If check_data(start_id, data, "33", "33", "34", "33") = True Then
                            metervalue = ""
                            While j < 4
                                num = System.Convert.ToByte(data(start_id + 17 - j), 16)
                                metervalue = metervalue & getbcdcode(num - &H33)
                                j += 1
                            End While
                            '将获取的电能存入到列表中
                            powerdata = System.Convert.ToDouble(metervalue / 100)

                            m_datatable.Rows.Add()
                            m_datatable.Rows(i)("control_box_name") = inf.control_box_name
                            m_datatable.Rows(i)("power_readdata") = powerdata
                            m_datatable.Rows(i)("power_data") = powerdata * inf.bianbi
                            m_datatable.Rows(i)("get_time") = Now

                            '将数据记录到数据库中
                            sql = "insert into powerdata_record(control_box_name,power_readdata,powerdata,get_time) values('" & inf.control_box_name & "','" & powerdata & "','" & powerdata * inf.bianbi & "','" & Now & "')"
                            DBOperation.ExecuteSQL(conn, sql, msg)

                            '将该查找信息删除
                            m_imeilist.RemoveAt(i)

                            rs.Fields("HandlerFlag").Value = 1
                            rs.Update()
                            Continue While
                        End If

                    End If
                 

                End If
                i += 1

            End While
            waittime -= 1
            System.Threading.Thread.Sleep(1000)
        End While


        If waittime = 0 Then
            i = 0
            While i < m_imeilist.Count
                m_datatable.Rows.Add()
                m_datatable.Rows(i)("control_box_name") = inf.control_box_name
                m_datatable.Rows(i)("power_readdata") = "-"
                m_datatable.Rows(i)("power_data") = "-"
                m_datatable.Rows(i)("get_time") = "-"

                '将该查找信息删除
                m_imeilist.RemoveAt(i)
                i += 1
            End While
        End If

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing

    End Sub

    ''' <summary>
    ''' 将12位长度的地址转换成6字节的BCD码
    ''' </summary>
    ''' <param name="address"></param>
    ''' <remarks></remarks>
    Public Function changetodcd(ByVal address As String) As String
        Dim tmp_add As String
        Dim i As Integer = 1
        changetodcd = ""
        While i < 12
            tmp_add = Mid(address, i, 2)
            '将两位的数字转换成一个字节BCD码
            changetodcd = tobcdcode(tmp_add) & " " & changetodcd
            i += 2
        End While

    End Function

    ''' <summary>
    ''' 获取电表编号
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub get_meterid()
        Dim i As Integer = 0
        Dim imei As String
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String
        Dim nowtime As DateTime
        Dim inf As m_boxinf
        Dim metertype As String
        Dim cmd As String

        nowtime = Now
        msg = ""
        sql = ""
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        While i < m_imeilist.Count
            '将所有选择的主控箱的取电表编号的命令发送下去
            inf = m_imeilist(i)
            imei = inf.IMEI
            metertype = inf.metertype
            '设置获取编号的命令
            cmd = set_getid_cmd(metertype)

            If metertype = "97" Then  '97规约
                sql = "insert into TimeControl(RoadIMEI, CMDType, CMDContent, HandlerFlag, CreateTime)" _
                & " values('" & imei & "','" & HG_TYPE.HG_ASK_POWERDATA97 & "','" & cmd & "','" & CONTROL_BOX_TYPE2_FLAG & "','" & nowtime & "')"
            Else '07规约
                sql = "insert into TimeControl(RoadIMEI, CMDType, CMDContent, HandlerFlag, CreateTime)" _
                & " values('" & imei & "','" & HG_TYPE.HG_ASK_POWERDATA07 & "','" & cmd & "','" & CONTROL_BOX_TYPE2_FLAG & "','" & nowtime & "')"

            End If

            DBOperation.ExecuteSQL(conn, sql, msg)

            i += 1
        End While

        get_retureIdvalue(nowtime)

        conn.Close()
        conn = Nothing
    End Sub


    Private Sub get_retureIdvalue(ByVal createtime As DateTime)
        Dim inf As New m_boxinf
        Dim i As Integer = 0
        Dim waittime As Integer = g_ycwaittime
        Dim controlboxid As String
        Dim sql As String = ""
        Dim msg As String = ""
        Dim conn As New ADODB.Connection
        Dim rs As New ADODB.Recordset
        Dim metertype As String
        Dim data() As String
        Dim start_id As Integer = 2  '有两个字节的路段号，所以起始编号从2开始
        Dim meterid As String '电表编号
        Dim j As Integer = 0
        Dim num As Byte

        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If

        While waittime > 0
            i = 0
            If i >= m_imeilist.Count Then
                Exit While
            End If

            If Me.BackgroundWorkergetdata.CancellationPending = True Then
                Exit While
            End If

            While i < m_imeilist.Count
                inf = m_imeilist(i)
                metertype = inf.metertype
                controlboxid = Com_inf.Dec_to_Hex(inf.control_box_id, 4)
                controlboxid = Mid(controlboxid, 1, 2) & " " & Mid(controlboxid, 3, 2)
                If metertype = "97" Then
                    sql = "select * from RoadLightStatus where StatusContent like '" & controlboxid & "%'" _
                    & "and CreateTime>'" & createtime & "' and PackType='" & HG_TYPE.HG_ACK_POWERDATA97 & "' and HandlerFlag=3"
                Else
                    sql = "select * from RoadLightStatus where StatusContent like '" & controlboxid & "%'" _
                    & "and CreateTime>'" & createtime & "' and PackType='" & HG_TYPE.HG_ACK_POWERDATA07 & "' and HandlerFlag=3"

                End If
                rs = DBOperation.SelectSQL(conn, sql, msg)
                If rs Is Nothing Then
                    conn.Close()
                    conn = Nothing
                    Exit Sub
                End If
                If rs.RecordCount > 0 Then
                    data = Trim(rs.Fields("StatusContent").Value).Split(" ")
                    While data(start_id) <> "68"
                        start_id += 1
                    End While
                    '验证97表号数据是否合法
                    If inf.metertype = "97" Then
                        If check_data(start_id, data, "65", "F3", "", "") = True Then
                            meterid = ""
                            While j < 6
                                num = System.Convert.ToByte(data(start_id + 17 - j), 16)
                                meterid &= getbcdcode(num - &H33)
                                j += 1
                            End While
                            '将获取的电表编号存入到列表中
                            ' dgv_configlist.Rows(inf.rowid).Cells("powermeter_id_getvalue").Value = meterid
                            SetDataGridviewDelegate(inf.rowid, meterid, dgv_configlist, False)
                            '将改查找信息删除
                            m_imeilist.RemoveAt(i)

                            rs.Fields("HandlerFlag").Value = 1
                            rs.Update()
                            Continue While
                        End If

                    Else
                        '验证07表号数据是否合法
                        If check_data(start_id, data, "34", "37", "33", "37") = True Then
                            meterid = ""
                            While j < 6
                                num = System.Convert.ToByte(data(start_id + 19 - j), 16)
                                meterid &= getbcdcode(num - &H33)
                                j += 1
                            End While
                            '将获取的电表编号存入到列表中
                            ' dgv_configlist.Rows(inf.rowid).Cells("powermeter_id_getvalue").Value = meterid
                            SetDataGridviewDelegate(inf.rowid, meterid, dgv_configlist, False)

                            '将改查找信息删除
                            m_imeilist.RemoveAt(i)

                            rs.Fields("HandlerFlag").Value = 1
                            rs.Update()
                            Continue While
                        End If

                    End If


                End If
                i += 1

            End While
            waittime -= 1
            System.Threading.Thread.Sleep(1000)
        End While


        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing

    End Sub

    ''' <summary>
    ''' 将十进制数转换成BCD码
    ''' </summary>
    ''' <param name="str"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function tobcdcode(ByVal str As String) As String
        Dim stm_num As Byte
        stm_num = System.Convert.ToByte(Mid(str, 1, 1)) << 4 Or System.Convert.ToByte(Mid(str, 2, 1))
        tobcdcode = System.Convert.ToString(stm_num, 16)
        If tobcdcode.Length < 2 Then
            tobcdcode = "0" & tobcdcode
        End If
        

    End Function

    ''' <summary>
    ''' 将BCD码转换成十进制数
    ''' </summary>
    ''' <param name="num"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getbcdcode(ByVal num As Byte) As String
        Dim stm_num1, stm_num2 As Byte
        stm_num1 = num >> 4
        stm_num2 = num And &HF
        stm_num1 = stm_num1 * 10 + stm_num2
        If stm_num1 < 10 Then
            getbcdcode = "0" & stm_num1.ToString
        Else
            getbcdcode = stm_num1.ToString
        End If

    End Function

    ''' <summary>
    ''' 验证数据是否合法
    ''' </summary>
    ''' <param name="DI0">数据标识1</param>
    ''' <param name="DI1">数据标识2</param>
    ''' <remarks></remarks>
    Public Function check_data(ByVal start_id As Integer, ByVal data() As String, ByVal DI0 As String, ByVal DI1 As String, ByVal DI2 As String, ByVal DI3 As String) As Boolean
        '第一步先判断长度是否合法
        Dim datalen As Integer
        If data.Length < start_id + 10 Then
            check_data = False
            Exit Function
        End If
        datalen = System.Convert.ToByte(data(start_id + 9), 16)
        If data.Length < start_id + 10 + datalen + 2 Then
            check_data = False
            Exit Function
        End If

        '第二步根据验证码判断数据是否合法
        Dim check_code As String
        Dim str As String = ""
        Dim i As Integer = start_id
        While i < start_id + 10 + datalen
            str = str & data(i) & " "
            i += 1
        End While
        check_code = data(start_id + 10 + datalen)
        If check_code <> get_cstring(Trim(str)) Then
            check_data = False
            Exit Function
        End If

        '第三步根据数据标识判断数据是否合法
        If datalen > 0 Then
            If DI2 = "" And DI3 = "" Then
                '97规约
                If data(start_id + 10) <> DI0 Or data(start_id + 11) <> DI1 Then
                    check_data = False
                    Exit Function
                End If
            Else
                '07规约
                If data(start_id + 10) <> DI0 Or data(start_id + 11) <> DI1 Or data(start_id + 12) <> DI2 Or data(start_id + 13) <> DI3 Then
                    check_data = False
                    Exit Function
                End If
            End If
        End If



        check_data = True

    End Function

    Private Sub bt_startgetdata_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_startgetdata.Click

        If Me.BackgroundWorkergetdata.IsBusy = True Then
            MsgBox("正在读取电表，请稍后重试", , PROJECT_TITLE_STRING)
            Exit Sub
        End If
        dgv_datalist.Rows.Clear()
        powermeter_string.Visible = True
        m_datatable.Rows.Clear()

        Dim tnRet As New TreeNode
        Dim i As Integer = 0
        Dim conn As New ADODB.Connection
        Dim rs As New ADODB.Recordset
        Dim sql As String
        Dim msg As String

        msg = ""
        m_gettag = 1
        For Each treenode As TreeNode In tv_all_controlbox.Nodes
            Com_inf.FindNode(treenode, m_checklist)
        Next
        If m_checklist.Count = 0 Then
            MsgBox("请选择需抄表的主控箱名称", , PROJECT_TITLE_STRING)
            Exit Sub
        End If
        If m_imeilist.Count > 0 Then
            m_imeilist.Clear()
        End If
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        While i < m_checklist.Count
            sql = "select * from controlbox_power where control_box_name='" & m_checklist(i) & "'"
            rs = DBOperation.SelectSQL(conn, sql, msg)
            If rs Is Nothing Then
                conn.Close()
                conn = Nothing
            End If
            If rs.RecordCount > 0 Then
                Dim inf As New m_boxinf

                inf.bianbi = rs.Fields("powermeter_bianbi").Value
                inf.control_box_id = Trim(rs.Fields("control_box_id").Value)
                inf.IMEI = Trim(rs.Fields("imei").Value)
                inf.metertype = Trim(rs.Fields("powermeter_type").Value)
                inf.meterid = Trim(rs.Fields("powermeter_id").Value)
                inf.control_box_name = m_checklist(i)
                m_imeilist.Add(inf)
            End If
            i += 1
        End While

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing

        If Me.BackgroundWorkergetdata.IsBusy = False Then
            Me.BackgroundWorkergetdata.RunWorkerAsync()
        End If


    End Sub

    Private Sub tv_all_controlbox_AfterCheck(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tv_all_controlbox.AfterCheck
        Dim controlboxobj As New control_box
        If m_check = False Then
            m_check = controlboxobj.setChild(e.Node)
        End If

        controlboxobj.setparent(e.Node)
        m_check = False

    End Sub

    Private Sub bt_readmeterid_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_readmeterid.Click
        Dim i As Integer = 0
        powermeter_id.Visible = True

        m_imeilist.Clear()
        m_gettag = 0
        While i < dgv_configlist.Rows.Count
            If dgv_configlist.Rows(i).Cells("check_id").Value = 1 Then
                If dgv_configlist.Rows(i).Cells("imei").Value Is System.DBNull.Value Then
                    Dim inf As New m_boxinf
                    inf.control_box_id = ""
                    inf.IMEI = ""
                    inf.rowid = 0
                    inf.metertype = ""
                    m_imeilist.Add(inf)
                Else
                    Dim inf As New m_boxinf
                    inf.control_box_id = Trim(dgv_configlist.Rows(i).Cells("config_controlboxid").Value)
                    inf.IMEI = Trim(dgv_configlist.Rows(i).Cells("imei").Value)
                    inf.rowid = i
                    inf.metertype = Trim(dgv_configlist.Rows(i).Cells("powermeter_type").Value)
                    m_imeilist.Add(inf)
                End If


            End If
            i += 1
        End While

        If m_imeilist.Count = 0 Then
            MsgBox("请选择需要读取编号的电表", , PROJECT_TITLE_STRING)
            Exit Sub
        End If

       

        If Me.BackgroundWorkergetdata.IsBusy = False Then
            Me.BackgroundWorkergetdata.RunWorkerAsync()
        End If

    End Sub

    ''' <summary>
    ''' 设置统一的获取电表编号的命令
    ''' </summary>
    ''' <param name="metertype">97、07</param>
    ''' <remarks></remarks>
    Private Function set_getid_cmd(ByVal metertype As String) As String
        Dim cmd_string As String
        If metertype = "97" Then
            cmd_string = "68 AA AA AA AA AA AA 68 01 02 65 F3 "
            cmd_string = cmd_string & get_cstring(Trim(cmd_string)) & " 16 16"


        Else
            cmd_string = "68 AA AA AA AA AA AA 68 11 04 34 37 33 37 "
            cmd_string = cmd_string & get_cstring(Trim(cmd_string)) & " 16 16"

        End If
        set_getid_cmd = cmd_string
    End Function

    ''' <summary>
    ''' 获取校验码
    ''' </summary>
    ''' <remarks></remarks>
    Public Function get_cstring(ByVal str As String) As String
        Dim strlist() As String
        Dim i As Integer = 0
        Dim data As Integer = 0
        Dim tmp_data As Byte = 0
        strlist = str.Split(" ")
        While i < strlist.Length
            data += System.Convert.ToByte(strlist(i), 16)
            i += 1
        End While
        tmp_data = data And &HFF

        If tmp_data < 16 Then
            get_cstring = "0" & System.Convert.ToString(tmp_data, 16).ToUpper
        Else
            get_cstring = System.Convert.ToString(tmp_data, 16).ToUpper

        End If

    End Function

    Private Sub cb_selectall_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_selectall.Click
        Dim i As Integer = 0
        If cb_selectall.Checked = True Then
            While i < dgv_configlist.Rows.Count
                dgv_configlist.Rows(i).Cells("check_id").Value = 1
                i += 1
            End While

        Else
            While i < dgv_configlist.Rows.Count
                dgv_configlist.Rows(i).Cells("check_id").Value = 0
                i += 1
            End While
        End If
    End Sub

    Private Sub bt_updateid_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_updateid.Click
        If MsgBox("是否将当前选中的编号更新到数据库中?", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.Yes Then
            Dim i As Integer
            Dim conn As New ADODB.Connection
            Dim sql As String
            Dim msg As String
            msg = ""
            i = 0
            If DBOperation.OpenConn(conn) = False Then
                Exit Sub
            End If
            While i < dgv_configlist.Rows.Count
                If dgv_configlist.Rows(i).Cells("check_id").Value = 1 Then
                    If dgv_configlist.Rows(i).Cells("check_id").Value = 1 Then
                        sql = "update controlbox_power set powermeter_id='" & Trim(dgv_configlist.Rows(i).Cells("powermeter_id_getvalue").Value) & "'where id=" & dgv_configlist.Rows(i).Cells("config_id").Value
                        DBOperation.ExecuteSQL(conn, sql, msg)
                    End If
                End If

                i += 1
            End While

            Me.Controlbox_powerTableAdapter.Fill(Me.StreetlampDataSetpowermeter.controlbox_power)

            conn.Close()
            conn = Nothing
        End If
    End Sub

    Private Sub BackgroundWorkergetdata_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorkergetdata.RunWorkerCompleted
        If m_gettag = 0 Then
            MsgBox("电表编号获取完毕", , PROJECT_TITLE_STRING)
            powermeter_id.Visible = False
        End If
        If m_gettag = 1 Then
            dgv_datalist.Rows.Clear()
            Dim i As Integer = 0
            While i < m_datatable.Rows.Count
                dgv_datalist.Rows.Add()
                dgv_datalist.Rows(i).Cells("control_box_name").Value = m_datatable.Rows(i)("control_box_name")
                dgv_datalist.Rows(i).Cells("power_readdata").Value = m_datatable.Rows(i)("power_readdata")
                dgv_datalist.Rows(i).Cells("power_data").Value = m_datatable.Rows(i)("power_data")
                dgv_datalist.Rows(i).Cells("get_time").Value = m_datatable.Rows(i)("get_time")
                i += 1
            End While

            MsgBox("抄表结束", , PROJECT_TITLE_STRING)
            powermeter_string.Visible = False
        End If

        If m_gettag = 2 Then
            '导出excel表
            m_xlApp.Visible = True

            progress.Visible = False
        End If

    End Sub

    Private Sub bt_stopgetdata_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_stopgetdata.Click
        If Me.BackgroundWorkergetdata.IsBusy = True Then
            Me.BackgroundWorkergetdata.CancelAsync()
        Else
            MsgBox("系统未在抄表，无需停止")
        End If
    End Sub

    Private Sub bt_getrecord_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_getrecord.Click
        Dim inf As New DataTable
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim read_data, data As Double '读数和电量
        Dim control_box_name As String
        Dim starttime, endtime As DateTime
        inf.Columns.Add("power_readdata")
        inf.Columns.Add("power_readdata_cha")
        inf.Columns.Add("power_data")
        inf.Columns.Add("power_data_cha")
        inf.Columns.Add("get_time")

        dgv_datarecord.Rows.Clear()
        msg = ""
        If tv_box_inf_list.SelectedNode Is Nothing Then
            MsgBox("请选择主控箱", , PROJECT_TITLE_STRING)
            tv_box_inf_list.Focus()
            Exit Sub
        End If
        If tv_box_inf_list.SelectedNode.Level <> 3 Then
            MsgBox("请选择主控箱", , PROJECT_TITLE_STRING)
            tv_box_inf_list.Focus()
            Exit Sub
        End If
        control_box_name = Trim(tv_box_inf_list.SelectedNode.Text).Split(" ")(0)

        If dtp_date_start.Value = Nothing Then
            MsgBox("请选择开始时间", , PROJECT_TITLE_STRING)
        End If
        starttime = dtp_date_start.Value
        endtime = dtp_date_end.Value
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        sql = "select * from powerdata_record where get_time>='" & starttime & "' and get_time<='" & endtime & "' and control_box_name='" & control_box_name & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        Dim i As Integer = 0

        If rs.RecordCount > 0 Then
            read_data = rs.Fields("power_readdata").Value
            data = rs.Fields("powerdata").Value
            While rs.EOF = False
                inf.Rows.Add()
                inf.Rows(i)("power_readdata") = rs.Fields("power_readdata").Value
                inf.Rows(i)("power_data") = rs.Fields("powerdata").Value
                inf.Rows(i)("get_time") = rs.Fields("get_time").Value
                If i = 0 Then
                    inf.Rows(i)("power_readdata_cha") = read_data
                    inf.Rows(i)("power_data_cha") = data

                Else
                    inf.Rows(i)("power_readdata_cha") = rs.Fields("power_readdata").Value - read_data
                    inf.Rows(i)("power_data_cha") = rs.Fields("powerdata").Value - data

                End If
                read_data = rs.Fields("power_readdata").Value
                data = rs.Fields("powerdata").Value
             
                i += 1
                rs.MoveNext()
            End While
            '将数据增加到列表中
            i = 0
            While i < inf.Rows.Count
                dgv_datarecord.Rows.Add()
                dgv_datarecord.Rows(i).Cells("id").Value = i + 1
                dgv_datarecord.Rows(i).Cells("read_data").Value = System.Convert.ToDouble(inf.Rows(i)("power_readdata"))
                dgv_datarecord.Rows(i).Cells("readdata_cha").Value = System.Convert.ToDouble(inf.Rows(i)("power_readdata_cha"))
                dgv_datarecord.Rows(i).Cells("powerdata").Value = System.Convert.ToDouble(inf.Rows(i)("power_data"))
                dgv_datarecord.Rows(i).Cells("powerdata_cha").Value = inf.Rows(i)("power_data_cha")
                dgv_datarecord.Rows(i).Cells("data_time").Value = inf.Rows(i)("get_time")
                i += 1
            End While

            MsgBox("查询成功", , PROJECT_TITLE_STRING)

        End If

     

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    Private Sub 抄表_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If Me.BackgroundWorkergetdata.IsBusy = True Then
            MsgBox("线程正在运行，请稍后关闭", , PROJECT_TITLE_STRING)
            e.Cancel = True
        End If
        g_windowclose = 1
    End Sub


    Private Sub bt_excel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_excel.Click
        m_gettag = 2
        If dgv_datarecord.Rows.Count <= 0 Then
            MsgBox("请先查询后再导出", , PROJECT_TITLE_STRING)
            Exit Sub
        End If

        If Me.BackgroundWorkergetdata.IsBusy = False Then
            Me.BackgroundWorkergetdata.RunWorkerAsync()
        End If
    End Sub
End Class