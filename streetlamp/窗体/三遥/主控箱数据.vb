Public Class 主控箱数据
    Private m_check As Boolean = False '设置标志，防止死循环
    Private m_checklist As New ArrayList  '存放选中的主控箱名称
    Private m_findtag As Integer '标志查询模式
    Private m_statestring As String  '查询的条件：正常等
    Private m_checktype As Integer '记录查询的类型
    ' Private m_timestart, m_timeend As System.DateTime  '查询时间段的开始与结束时间
    Private m_controlboxnamelist As New ArrayList   '查询的电控箱名称
    Private m_statetag As Integer  '0表示暗，1表示亮
    Private m_exceltable As Integer  '0表示查询，1表示导出excel表
    Private m_xlApp As Microsoft.Office.Interop.Excel.Application

    Private m_xlBook As Microsoft.Office.Interop.Excel.Workbook
    Private m_xlSheet As Microsoft.Office.Interop.Excel.Worksheet
    Private m_row As Integer  'excel表的行数
    Private m_stringtag As Integer   '提示信息只出现一次
    Private m_id As Integer   '编号
    Private m_small_sanyao As Boolean = False   '记录是否为小三遥
    Delegate Sub SetDataGridview(ByVal control_box_name As String, ByVal datagridview As Windows.Forms.DataGridView, ByVal tag As Integer, ByVal state As String, ByVal createtime As Date, ByVal control_box_type As Integer, ByVal board_num As Integer)   '设置DataGridview中的内容
    Delegate Sub SetDataStateGridview(ByVal control_box_name As String, ByVal datagridview As Windows.Forms.DataGridView, ByVal state As String, ByVal createtime As DateTime, ByVal endtime As Date, ByVal state_kind As String)    '设置DataGridview中的内容
    Private m_starttime, m_endtime As Date

    Public Sub SetDataGridviewDelegateState(ByVal control_box_name As String, ByVal datagridview As Windows.Forms.DataGridView, ByVal state As String, ByVal createtime As Date, ByVal endtime As Date, ByVal state_kind As String)
        If datagridview.InvokeRequired Then
            Dim datagridviewobj As SetDataStateGridview = New SetDataStateGridview(AddressOf SetDataGridviewDelegateState)
            Me.Invoke(datagridviewobj, New Object() {control_box_name, datagridview, state, createtime, endtime, state_kind})
        Else
            Try
                Dim rowid As Integer = datagridview.Rows.Count

                If datagridview.Name = "dgv_statelist" Then
                    datagridview.Rows.Add()
                    datagridview.Rows(rowid).Cells("id_state").Value = rowid + 1
                    datagridview.Rows(rowid).Cells("controlboxname_state").Value = control_box_name
                    datagridview.Rows(rowid).Cells("state").Value = state
                    datagridview.Rows(rowid).Cells("time_state_start").Value = createtime
                    datagridview.Rows(rowid).Cells("time_state_end").Value = endtime
                    datagridview.Rows(rowid).Cells("state_kind").Value = state_kind '状态类型
                Else
                    datagridview.Rows.Add()
                    datagridview.Rows(rowid).Cells("id_kaiguan").Value = rowid + 1
                    datagridview.Rows(rowid).Cells("controlboxname_kaiguan").Value = control_box_name
                    datagridview.Rows(rowid).Cells("state_kaiguan").Value = state
                    datagridview.Rows(rowid).Cells("starttime_kaiguan").Value = createtime
                    datagridview.Rows(rowid).Cells("endtime_kaiguan").Value = endtime

                End If
              


            Catch ex As Exception
                MsgBox("数据采集出错，请重试！", , PROJECT_TITLE_STRING)

            End Try
            '合并单元格

        End If
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
            If Me.MergeColumnNames.Contains(dgv_statelist.Columns(e.ColumnIndex).Name) And e.RowIndex <> -1 Then
                cellwidth = e.CellBounds.Width
                Dim gridLinePen As Pen = New Pen(gridBrush)
                Dim curValue As String = CType(e.Value, String)
                IIf(curValue Is Nothing, "", e.Value.ToString().Trim())
                Dim curSelected As String = CType(dgv_statelist.CurrentRow.Cells(e.ColumnIndex).Value, String)
                IIf(curSelected Is Nothing, "", dgv_statelist.CurrentRow.Cells(e.ColumnIndex).Value.ToString().Trim())
                'If Not String.IsNullOrEmpty(curValue) Then
                Dim i As Integer
                For i = e.RowIndex To dgv_statelist.Rows.Count - 1 Step i + 1
                    If dgv_statelist.Rows(i).Cells(e.ColumnIndex).Value IsNot Nothing Then
                        If dgv_statelist.Rows(i).Cells(e.ColumnIndex).Value.ToString().Equals(curValue) Then

                            DownRows = DownRows + 1
                            If e.RowIndex <> i Then
                                cellwidth = cellwidth
                                IIf(cellwidth < dgv_statelist.Rows(i).Cells(e.ColumnIndex).Size.Width, cellwidth, dgv_statelist.Rows(i).Cells(e.ColumnIndex).Size.Width)
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
                    If dgv_statelist.Rows(j).Cells(e.ColumnIndex).Value.ToString().Equals(curValue) Then

                        UpRows = UpRows + 1
                        If e.RowIndex <> j Then
                            cellwidth = cellwidth
                            IIf(cellwidth < dgv_statelist.Rows(j).Cells(e.ColumnIndex).Size.Width, cellwidth, dgv_statelist.Rows(j).Cells(e.ColumnIndex).Size.Width)
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
                If dgv_statelist.Rows(e.RowIndex).Selected Then
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


    'Private Sub dgv_boxinf_CellPainting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellPaintingEventArgs) Handles dgv_boxinf.CellPainting

    '    If e.RowIndex > -1 And e.ColumnIndex > -1 Then
    '        DrawCell(e)
    '    End If
    '    ' dgv_boxinf.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True

    'End Sub

    Public Sub SetDataGridviewDelegate(ByVal control_box_name As String, ByVal datagridview As Windows.Forms.DataGridView, ByVal tag As Integer, ByVal state As String, ByVal createtime As DateTime, ByVal control_box_type As Integer, ByVal board_num As Integer)
        If datagridview.InvokeRequired Then
            Dim datagridviewobj As SetDataGridview = New SetDataGridview(AddressOf SetDataGridviewDelegate)
            Me.Invoke(datagridviewobj, New Object() {control_box_name, datagridview, tag, state, createtime, control_box_type, board_num})
        Else
            Try
                If control_box_type < 4 Then
                    setresult(control_box_name, state, createtime, board_num)
                Else
                    setresultABC(control_box_name, state, createtime, board_num)

                End If
            Catch ex As Exception
                MsgBox("数据采集出错，请重试！", , PROJECT_TITLE_STRING)

            End Try
            '合并单元格

        End If
    End Sub

    ''' <summary>
    ''' 获取正常数据(AAAABBBBCCCC)
    ''' </summary>
    ''' <param name="control_box_name"></param>
    ''' <param name="state" ></param>   
    ''' <remarks></remarks>
    Private Sub setresult(ByVal control_box_name As String, ByVal state As String, ByVal createtime As DateTime, ByVal board_num As Integer)
        Dim i As Integer = 0
        Dim data_state() As String
        Dim rowcount As Integer
        Dim rowid As Integer = 0
        Dim t As Integer = 0

        data_state = state.Split(" ")

        If data_state.Length = 21 Then
            '小三遥数据
            rowcount = (data_state.Length - 3) / 3
            rowid = dgv_boxinf.RowCount

            While i < rowcount
                dgv_boxinf.Rows.Add()
                dgv_boxinf.Rows(rowid).Cells("id").Value = rowid + 1
                dgv_boxinf.Rows(rowid).Cells("box_name").Value = control_box_name

                If i < 2 Then
                    dgv_boxinf.Rows(rowid).Cells("xiangwei").Value = "A"
                    dgv_boxinf.Rows(rowid).Cells("presure").Value = data_state(0)
                Else

                    If i < 4 Then
                        dgv_boxinf.Rows(rowid).Cells("xiangwei").Value = "B"
                        dgv_boxinf.Rows(rowid).Cells("presure").Value = data_state(1)

                    Else
                        dgv_boxinf.Rows(rowid).Cells("xiangwei").Value = "C"
                        dgv_boxinf.Rows(rowid).Cells("presure").Value = data_state(2)

                    End If
                End If
                dgv_boxinf.Rows(rowid).Cells("huilu").Value = i + 1
                dgv_boxinf.Rows(rowid).Cells("current").Value = data_state(3 + 3 * i)
                dgv_boxinf.Rows(rowid).Cells("power").Value = data_state(4 + 3 * i)
                dgv_boxinf.Rows(rowid).Cells("power_yinshu").Value = data_state(5 + 3 * i)
                dgv_boxinf.Rows(rowid).Cells("Createtime").Value = createtime

                i += 1
                rowid += 1

            End While
        End If

        If data_state.Length = 39 * board_num Then
            '大三遥数据
            rowcount = 12 * board_num
            rowid = dgv_boxinf.RowCount
          
            While t < board_num
                i = 0
                While i < 12
                    dgv_boxinf.Rows.Add()
                    dgv_boxinf.Rows(rowid).Cells("id").Value = rowid + 1
                    dgv_boxinf.Rows(rowid).Cells("box_name").Value = control_box_name

                    If i < 4 Then
                        dgv_boxinf.Rows(rowid).Cells("xiangwei").Value = "A"
                        dgv_boxinf.Rows(rowid).Cells("presure").Value = data_state(0 + 39 * t)
                    Else

                        If i < 8 Then
                            dgv_boxinf.Rows(rowid).Cells("xiangwei").Value = "B"
                            dgv_boxinf.Rows(rowid).Cells("presure").Value = data_state(1 + 39 * t)

                        Else
                            dgv_boxinf.Rows(rowid).Cells("xiangwei").Value = "C"
                            dgv_boxinf.Rows(rowid).Cells("presure").Value = data_state(2 + 39 * t)

                        End If
                    End If

                    dgv_boxinf.Rows(rowid).Cells("huilu").Value = i + 1 + 12 * t
                    dgv_boxinf.Rows(rowid).Cells("current").Value = data_state(3 + 3 * i + 39 * t)
                    dgv_boxinf.Rows(rowid).Cells("power").Value = data_state(4 + 3 * i + 39 * t)
                    dgv_boxinf.Rows(rowid).Cells("power_yinshu").Value = data_state(5 + 3 * i + 39 * t)
                    dgv_boxinf.Rows(rowid).Cells("Createtime").Value = createtime

                    i += 1
                    rowid += 1
                End While
           
                t += 1

            End While

        End If



        ''合并单元格
        'i = 0
        'Dim rowmergeview1 As New RowMergeView
        'While i < dgv_boxinf.ColumnCount
        '    rowmergeview1.MergeColumnNames.Add("state")
        '    i += 1
        'End While

finish:

    End Sub

    ''' <summary>
    ''' 获取正常数据(ABCABCABCABC)
    ''' </summary>
    ''' <param name="control_box_name"></param>
    ''' <param name="state" ></param>   
    ''' <remarks></remarks>
    Private Sub setresultABC(ByVal control_box_name As String, ByVal state As String, ByVal createtime As DateTime, ByVal board_num As Integer)
        Dim i As Integer = 0
        Dim data_state() As String
        Dim rowcount As Integer
        Dim rowid As Integer = 0
        Dim t As Integer = 0
        Dim presure_type As Integer
        data_state = state.Split(" ")
        Dim controlboxobj As New control_box
        If data_state.Length = 21 Then
            '小三遥数据
            rowcount = (data_state.Length - 3) / 3
            rowid = dgv_boxinf.RowCount

            While i < rowcount
                dgv_boxinf.Rows.Add()
                dgv_boxinf.Rows(rowid).Cells("id").Value = rowid + 1
                dgv_boxinf.Rows(rowid).Cells("box_name").Value = control_box_name
                presure_type = controlboxobj.get_presuretype(i + 1, control_box_name)

                If presure_type = 0 Then
                    dgv_boxinf.Rows(rowid).Cells("xiangwei").Value = "A"
                    dgv_boxinf.Rows(rowid).Cells("presure").Value = data_state(0)
                Else

                    If presure_type = 1 Then
                        dgv_boxinf.Rows(rowid).Cells("xiangwei").Value = "B"
                        dgv_boxinf.Rows(rowid).Cells("presure").Value = data_state(1)

                    Else
                        dgv_boxinf.Rows(rowid).Cells("xiangwei").Value = "C"
                        dgv_boxinf.Rows(rowid).Cells("presure").Value = data_state(2)

                    End If
                End If
                dgv_boxinf.Rows(rowid).Cells("huilu").Value = i + 1
                dgv_boxinf.Rows(rowid).Cells("current").Value = data_state(3 + 3 * i)
                dgv_boxinf.Rows(rowid).Cells("power").Value = data_state(4 + 3 * i)
                dgv_boxinf.Rows(rowid).Cells("power_yinshu").Value = data_state(5 + 3 * i)
                dgv_boxinf.Rows(rowid).Cells("Createtime").Value = createtime

                i += 1
                rowid += 1

            End While
        End If

        If data_state.Length = 39 * board_num Then
            '大三遥数据
            rowcount = 12 * board_num
            rowid = dgv_boxinf.RowCount

            While t < board_num
                i = 0
                While i < 12
                    dgv_boxinf.Rows.Add()
                    dgv_boxinf.Rows(rowid).Cells("id").Value = rowid + 1
                    dgv_boxinf.Rows(rowid).Cells("box_name").Value = control_box_name
                    presure_type = controlboxobj.get_presuretype(i + 1, control_box_name)

                    If presure_type = 0 Then
                        dgv_boxinf.Rows(rowid).Cells("xiangwei").Value = "A"
                        dgv_boxinf.Rows(rowid).Cells("presure").Value = data_state(0 + 39 * t)
                    Else

                        If presure_type = 1 Then
                            dgv_boxinf.Rows(rowid).Cells("xiangwei").Value = "B"
                            dgv_boxinf.Rows(rowid).Cells("presure").Value = data_state(1 + 39 * t)

                        Else
                            dgv_boxinf.Rows(rowid).Cells("xiangwei").Value = "C"
                            dgv_boxinf.Rows(rowid).Cells("presure").Value = data_state(2 + 39 * t)

                        End If
                    End If

                    dgv_boxinf.Rows(rowid).Cells("huilu").Value = i + 1 + 12 * t
                    dgv_boxinf.Rows(rowid).Cells("current").Value = data_state(3 + 3 * i + 39 * t)
                    dgv_boxinf.Rows(rowid).Cells("power").Value = data_state(4 + 3 * i + 39 * t)
                    dgv_boxinf.Rows(rowid).Cells("power_yinshu").Value = data_state(5 + 3 * i + 39 * t)
                    dgv_boxinf.Rows(rowid).Cells("Createtime").Value = createtime

                    i += 1
                    rowid += 1
                End While

                t += 1

            End While

        End If



        ''合并单元格
        'i = 0
        'Dim rowmergeview1 As New RowMergeView
        'While i < dgv_boxinf.ColumnCount
        '    rowmergeview1.MergeColumnNames.Add("state")
        '    i += 1
        'End While

finish:


    End Sub

    Private Sub 主控箱数据_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = New Icon("图片\favicon.ico", 32, 32)
        Dim controlboxobj As New control_box
        controlboxobj.set_controlbox_list(tv_yaoce_controlbox) '主控箱信息列表
        controlboxobj.set_controlbox_list(tv_boxlist_state) '状态查询中的主控箱列表
        controlboxobj.set_controlbox_list(tv_boxlist_kaiguan) '开关查询中的主控箱列表
        controlboxobj.Dispose()

        '  Me.MergeColumnNames.Add("id_state")
        Me.MergeColumnNames.Add("controlboxname_state")
        Me.MergeColumnNames.Add("state_kind")
        ' Me.MergeColumnNames.Add("state")
        'Me.MergeColumnNames.Add("state")
        'Me.MergeColumnNames.Add("yaoce_state")
        dtp_date_start.Value = DateAdd(DateInterval.Day, -1, Now)
        dtp_date_start.CustomFormat = "yyyy-MM-dd HH:mm:ss"  '开始时间格式
        dtp_date_end.CustomFormat = "yyyy-MM-dd HH:mm:ss"  '结束时间格式

        dtp_starttime_state.Value = DateAdd(DateInterval.Day, -1, Now)
        dtp_starttime_state.CustomFormat = "yyyy-MM-dd HH:mm:ss"  '开始时间格式
        dtp_endtime_state.CustomFormat = "yyyy-MM-dd HH:mm:ss"  '开始时间格式

        dtp_starttime_kaiguan.Value = DateAdd(DateInterval.Day, -1, Now)
        dtp_starttime_kaiguan.CustomFormat = "yyyy-MM-dd HH:mm:ss"  '开始时间格式
        dtp_endtime_kaiguan.CustomFormat = "yyyy-MM-dd HH:mm:ss"  '开始时间格式
    End Sub

    Private Sub tv_yaoce_controlbox_AfterCheck(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tv_yaoce_controlbox.AfterCheck
        Dim controlboxobj As New control_box
        If m_check = False Then
            m_check = controlboxobj.setChild(e.Node)
        End If

        controlboxobj.setparent(e.Node)
        m_check = False

    End Sub

    Private Sub bt_yaoce_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_yaoce.Click

        If Me.BackgroundWorker_find.IsBusy = False Then
            m_checklist.Clear() '清除所有选中的项目
            Me.dgv_boxinf.Rows.Clear() '清除列表

            ' Dim timestring As String
            'timestring = System.Convert.ToDateTime(dtp_starttime_state.Value).Date.ToString
            m_starttime = dtp_date_start.Value
            ' timestring = System.Convert.ToDateTime(dtp_endtime_state.Value).Date.AddHours(24).ToString
            m_endtime = dtp_date_end.Value
            Dim tnRet As New TreeNode
            For Each treenode As TreeNode In tv_yaoce_controlbox.Nodes
                Com_inf.FindNode(treenode, m_checklist)
            Next
            If m_checklist.Count = 0 Then
                MsgBox("请选择需设置的主控箱名称", , PROJECT_TITLE_STRING)
                Exit Sub
            End If
            m_findtag = 1 '标志着查询的是主控箱的数据


            Me.BackgroundWorker_find.RunWorkerAsync()
        Else
            MsgBox("查询正在进行，请稍候....", , PROJECT_TITLE_STRING)
        End If
       
    End Sub

    Private Sub BackgroundWorker_find_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker_find.DoWork
        Dim i As Integer = 0
        Dim controlboxname As String '主控箱名称
        If m_findtag = 1 Then
            '查询三遥数据 
            m_statestring = "全部"
            If m_exceltable = 0 Then
                While i < m_checklist.Count
                    controlboxname = m_checklist(i)
                    sanyao_data(m_statestring, controlboxname)  '查询
                    i += 1
                End While

            Else
                If m_exceltable = 1 Then
                    sanyao_data_excel()   'excel表
                End If

            End If
        Else
            If m_findtag = 2 Then
                If m_exceltable = 0 Then
                    While i < m_checklist.Count
                        controlboxname = m_checklist(i)
                        sanyao_state(m_statestring, controlboxname)  '查询
                        i += 1
                    End While

                Else
                    If m_exceltable = 1 Then
                        sanyao_state_excel()   'excel表
                    End If

                End If
            Else
                If m_findtag = 3 Then
                    '查询开关量的记录
                    If m_exceltable = 0 Then
                        While i < m_checklist.Count
                            controlboxname = m_checklist(i)
                            sanyao_kaiguan(m_statestring, controlboxname)  '查询
                            i += 1
                        End While

                    Else
                        If m_exceltable = 1 Then
                            sanyao_kaiguan_excel()   'excel表
                        End If

                    End If
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' 运行状态的excel结构
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Private Sub sanyao_kaiguan_excel()
        m_xlApp = (New Microsoft.Office.Interop.Excel.Application)

        m_xlBook = m_xlApp.Workbooks().Add
        m_xlSheet = m_xlBook.Worksheets("sheet1")
        m_xlApp.Cells(1, 1) = "主控箱开关量报警统计"
        m_xlApp.Rows(1).RowHeight = 50
        m_xlApp.Rows(1).Font.Size = 18
        m_xlApp.Rows(1).Font.Bold = True
        m_xlApp.Cells(2, 1) = "单位：" & COMPANY_NAME
        m_xlApp.Cells(2, 2) = ""
        m_xlApp.Cells(2, 3) = ""
        m_xlApp.Cells(2, 4) = "时间：" & dtp_starttime_state.Value.Date & "至" & dtp_endtime_state.Value.Date


        m_xlApp.Rows(2).RowHeight = 30
        m_xlApp.Rows(2).Font.Size = 12
        m_xlApp.Cells(3, 1) = "编号"

        m_xlApp.Cells(3, 2) = "主控箱"
        m_xlApp.Cells(3, 3) = "状态"
        m_xlApp.Cells(3, 4) = "开始时间"
        m_xlApp.Cells(3, 5) = "结束时间"
   


        m_xlApp.Rows(3).Font.Bold = True
        m_xlApp.Rows(3).font.size = 12
        m_xlApp.Rows(3).RowHeight = 30
        m_row = 4

        Dim i As Integer = 0
        Dim controlboxname As String
        While i < m_checklist.Count
            controlboxname = m_checklist(i)
            sanyao_kaiguan(m_statestring, controlboxname)
            i += 1
        End While

        With m_xlSheet
            .Range(.Cells(1, 1), .Cells(1, 5)).Merge()
            .Range(.Cells(2, 1), .Cells(2, 3)).Merge()
            .Range(.Cells(2, 4), .Cells(2, 5)).Merge()

            .Range(.Cells(3, 1), .Cells(3, 1)).ColumnWidth = 5
            .Range(.Cells(3, 2), .Cells(3, 2)).ColumnWidth = 20
            .Range(.Cells(3, 3), .Cells(3, 5)).ColumnWidth = 50


            .Range(.Cells(4, 1), .Cells(m_row - 1, 5)).RowHeight = 20

            .Range(.Cells(1, 1), .Cells(1, 5)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter
            .Range(.Cells(2, 1), .Cells(2, 3)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft
            .Range(.Cells(2, 6), .Cells(2, 5)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight
            .Range(.Cells(2, 11), .Cells(2, 5)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight

            .Range(.Cells(3, 1), .Cells(m_row - 1, 5)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter


            .Range(.Cells(3, 1), .Cells(3, 5)).Font.Name = "宋体"
            '设标题为宋体字
            .Range(.Cells(3, 1), .Cells(3, 5)).Font.Bold = "True"
            '标题字体加粗
            .Range(.Cells(1, 1), .Cells(2, 5)).Borders.LineStyle = 0
            .Range(.Cells(3, 1), .Cells(m_row - 1, 5)).Borders.LineStyle = 1
            '设表格边框样式
            .Range(.Cells(2, 1), .Cells(m_row - 1, 5)).Font.Size = 12

            '表中数据的字号

            .PageSetup.CenterFooter = "第 &P 页/共 &N 页"

        End With
    End Sub


    ''' <summary>
    ''' 运行状态的excel结构
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Private Sub sanyao_state_excel()
        m_xlApp = (New Microsoft.Office.Interop.Excel.Application)

        m_xlBook = m_xlApp.Workbooks().Add
        m_xlSheet = m_xlBook.Worksheets("sheet1")
        m_xlApp.Cells(1, 1) = "主控箱状态统计"
        m_xlApp.Rows(1).RowHeight = 50
        m_xlApp.Rows(1).Font.Size = 18
        m_xlApp.Rows(1).Font.Bold = True
        m_xlApp.Cells(2, 1) = "单位：" & COMPANY_NAME
        m_xlApp.Cells(2, 2) = ""
        m_xlApp.Cells(2, 3) = ""
        m_xlApp.Cells(2, 4) = "时间：" & dtp_starttime_state.Value.Date & "至" & dtp_endtime_state.Value.Date


        m_xlApp.Rows(2).RowHeight = 30
        m_xlApp.Rows(2).Font.Size = 12
        m_xlApp.Cells(3, 1) = "编号"

        m_xlApp.Cells(3, 2) = "主控箱"
        m_xlApp.Cells(3, 3) = "状态"
        m_xlApp.Cells(3, 4) = "开始时间"
        m_xlApp.Cells(3, 5) = "结束时间"
      
      

        m_xlApp.Rows(3).Font.Bold = True
        m_xlApp.Rows(3).font.size = 12
        m_xlApp.Rows(3).RowHeight = 30
        m_row = 4

        Dim i As Integer = 0
        Dim controlboxname As String
        While i < m_checklist.Count
            controlboxname = m_checklist(i)
            sanyao_state(m_statestring, controlboxname)
            i += 1
        End While

        With m_xlSheet
            .Range(.Cells(1, 1), .Cells(1, 5)).Merge()
            .Range(.Cells(2, 1), .Cells(2, 3)).Merge()
            .Range(.Cells(2, 4), .Cells(2, 5)).Merge()

            .Range(.Cells(3, 1), .Cells(3, 1)).ColumnWidth = 5
            .Range(.Cells(3, 2), .Cells(3, 2)).ColumnWidth = 20
            .Range(.Cells(3, 3), .Cells(3, 5)).ColumnWidth = 50


            .Range(.Cells(4, 1), .Cells(m_row - 1, 5)).RowHeight = 20

            .Range(.Cells(1, 1), .Cells(1, 5)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter
            .Range(.Cells(2, 1), .Cells(2, 3)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft
            .Range(.Cells(2, 6), .Cells(2, 5)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight
            .Range(.Cells(2, 11), .Cells(2, 5)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight

            .Range(.Cells(3, 1), .Cells(m_row - 1, 5)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter


            .Range(.Cells(3, 1), .Cells(3, 5)).Font.Name = "宋体"
            '设标题为宋体字
            .Range(.Cells(3, 1), .Cells(3, 5)).Font.Bold = "True"
            '标题字体加粗
            .Range(.Cells(1, 1), .Cells(2, 5)).Borders.LineStyle = 0
            .Range(.Cells(3, 1), .Cells(m_row - 1, 5)).Borders.LineStyle = 1
            '设表格边框样式
            .Range(.Cells(2, 1), .Cells(m_row - 1, 5)).Font.Size = 12

            '表中数据的字号

            .PageSetup.CenterFooter = "第 &P 页/共 &N 页"

        End With
    End Sub

    ''' <summary>
    ''' 运行状态的excel结构
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Private Sub sanyao_data_excel()
        m_xlApp = (New Microsoft.Office.Interop.Excel.Application)

        m_xlBook = m_xlApp.Workbooks().Add
        m_xlSheet = m_xlBook.Worksheets("sheet1")
        m_xlApp.Cells(1, 1) = "主控箱数据统计"
        m_xlApp.Rows(1).RowHeight = 50
        m_xlApp.Rows(1).Font.Size = 18
        m_xlApp.Rows(1).Font.Bold = True
        m_xlApp.Cells(2, 1) = "单位：" & COMPANY_NAME
        m_xlApp.Cells(2, 2) = ""
        m_xlApp.Cells(2, 3) = ""
        m_xlApp.Cells(2, 4) = ""
        m_xlApp.Cells(2, 5) = ""
        m_xlApp.Cells(2, 6) = "时间：" & dtp_date_start.Value.Date & "至" & dtp_date_end.Value.Date
        m_xlApp.Cells(2, 7) = ""
        m_xlApp.Cells(2, 8) = ""


        m_xlApp.Rows(2).RowHeight = 30
        m_xlApp.Rows(2).Font.Size = 12
        m_xlApp.Cells(3, 1) = "编号"

        m_xlApp.Cells(3, 2) = "采集时间"
        m_xlApp.Cells(3, 3) = "主控箱"
        m_xlApp.Cells(3, 4) = "回路"
        m_xlApp.Cells(3, 5) = "电压(V)"
        m_xlApp.Cells(3, 6) = "电流(A)"
        m_xlApp.Cells(3, 7) = "功率(KW)"
        m_xlApp.Cells(3, 8) = "功率因数"

        m_xlApp.Rows(3).Font.Bold = True
        m_xlApp.Rows(3).font.size = 12
        m_xlApp.Rows(3).RowHeight = 30
        m_row = 4

        Dim i As Integer = 0
        Dim controlboxname As String
        While i < m_checklist.Count
            controlboxname = m_checklist(i)
            sanyao_data(m_statestring, controlboxname)
            i += 1
        End While

        With m_xlSheet
            .Range(.Cells(1, 1), .Cells(1, 8)).Merge()
            .Range(.Cells(2, 1), .Cells(2, 3)).Merge()
            .Range(.Cells(2, 4), .Cells(2, 5)).Merge()
            .Range(.Cells(2, 6), .Cells(2, 8)).Merge()
            i = 4
            If m_small_sanyao = True Then  '小三遥合并6行
                While i < m_row - 1
                    .Range(.Cells(i, 1), .Cells(i + 5, 1)).Merge()
                    .Range(.Cells(i, 2), .Cells(i + 5, 2)).Merge()
                    .Range(.Cells(i, 3), .Cells(i + 5, 3)).Merge()
                    i += 6
                End While

            Else
                While i < m_row - 1
                    .Range(.Cells(i, 1), .Cells(i + 11, 1)).Merge()
                    .Range(.Cells(i, 2), .Cells(i + 11, 2)).Merge()
                    .Range(.Cells(i, 3), .Cells(i + 11, 3)).Merge()
                    i += 12
                End While
            End If

            .Range(.Cells(3, 1), .Cells(3, 1)).ColumnWidth = 5
            .Range(.Cells(3, 2), .Cells(3, 2)).ColumnWidth = 20
            .Range(.Cells(3, 3), .Cells(3, 5)).ColumnWidth = 10
            .Range(.Cells(3, 6), .Cells(3, 8)).ColumnWidth = 15


            .Range(.Cells(4, 1), .Cells(m_row - 1, 8)).RowHeight = 20

            .Range(.Cells(1, 1), .Cells(1, 8)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter
            .Range(.Cells(2, 1), .Cells(2, 3)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft
            .Range(.Cells(2, 6), .Cells(2, 8)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight
            .Range(.Cells(2, 11), .Cells(2, 8)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight

            .Range(.Cells(3, 1), .Cells(m_row - 1, 8)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter


            .Range(.Cells(3, 1), .Cells(3, 8)).Font.Name = "宋体"
            '设标题为宋体字
            .Range(.Cells(3, 1), .Cells(3, 8)).Font.Bold = "True"
            '标题字体加粗
            .Range(.Cells(1, 1), .Cells(2, 8)).Borders.LineStyle = 0
            .Range(.Cells(3, 1), .Cells(m_row - 1, 8)).Borders.LineStyle = 1
            '设表格边框样式
            .Range(.Cells(2, 1), .Cells(m_row - 1, 8)).Font.Size = 12

            '表中数据的字号

            .PageSetup.CenterFooter = "第 &P 页/共 &N 页"

        End With
    End Sub

    ''' <summary>
    '''   '按电控箱进行终端状态查询
    ''' </summary>
    ''' <param name="check_state"></param>
    ''' <param name="controlboxname"></param>
    ''' <remarks></remarks>
    Private Sub sanyao_kaiguan(ByVal check_state As String, ByVal controlboxname As String)

        Dim rs_box As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim progress_value As Integer
        Dim num As Integer
        Dim statestring As String
        Dim controlboxobj As New control_box
        Dim statetype1 As String = "数据"
        Dim statetype2 As String = "时段"
        Dim timestart, timeend As Date

        Dim recData(20) As String '接收的数据
        Dim box_type As Integer '电控箱类型
        Dim state_kind As String '为不同的状态类型分类，通信，供电，状态

        Dim i As Integer = 0
        Dim j As Integer = 0

        progress_value = 1
        msg = ""

        sql = "select * from kaiguan_alarm_list where (createtime>='" & m_starttime & "' and " _
         & "createtime<='" & m_endtime & "' or (createtime<'" & m_starttime & "' and " _
         & "(endtime is NULL or endtime >'" & m_starttime & "')))" _
         & "and control_box_name='" & controlboxname & "' order by createtime"

        DBOperation.OpenConn(conn)
        rs_box = DBOperation.SelectSQL(conn, sql, msg)
        If rs_box Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "sanyao_kaiguan", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        While rs_box.EOF = False
            If Me.BackgroundWorker_find.CancellationPending = True Then
                GoTo finish
            End If
            controlboxname = Trim(rs_box.Fields("control_box_name").Value)
            box_type = controlboxobj.Get_boxtype_name(controlboxname)  '主控箱类型
            If rs_box.Fields("endtime").Value Is System.DBNull.Value And rs_box.Fields("alarm_tag").Value = 1 Then
                rs_box.MoveNext()
                Continue While
            End If

            '显示进度
            num = 100 * progress_value / rs_box.RecordCount
            If num > 100 Then
                num = 100
            End If
            Me.BackgroundWorker_find.ReportProgress(num)
            progress_value += 1

            timestart = rs_box.Fields("createtime").Value
            If timestart < Me.m_starttime Then  '开始时间小于查询的开始时间，将状态的开始时间设为查询时间
                timestart = m_starttime
            End If
            If rs_box.Fields("endtime").Value Is System.DBNull.Value Then
                timeend = Me.m_endtime
            Else
                timeend = System.Convert.ToDateTime(rs_box.Fields("endtime").Value)
            End If

            If rs_box.Fields("alarm_string").Value Is System.DBNull.Value Then
                statestring = ""
            Else
                statestring = Trim(rs_box.Fields("alarm_string").Value)
            End If

            i = 0
            If m_exceltable = 0 Then
                SetDataGridviewDelegateState(controlboxname, dgv_kaiguanlist, statestring, timestart, timeend, "")

            Else  '导出报表

                m_xlApp.Cells(m_row, 1) = "'" & m_id
                m_xlApp.Cells(m_row, 2) = "'" & controlboxname
                m_xlApp.Cells(m_row, 3) = statestring
                m_xlApp.Cells(m_row, 4) = "'" & timestart
                m_xlApp.Cells(m_row, 5) = "'" & timeend

                m_row += 1

                m_id += 1

            End If

            rs_box.MoveNext()
        End While

finish:
        If rs_box.State = 1 Then
            rs_box.Close()
            rs_box = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    '''   '按电控箱进行终端状态查询
    ''' </summary>
    ''' <param name="check_state"></param>
    ''' <param name="controlboxname"></param>
    ''' <remarks></remarks>
    Private Sub sanyao_state(ByVal check_state As String, ByVal controlboxname As String)

        Dim rs_box As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim progress_value As Integer
        Dim num As Integer
        Dim statestring As String
        Dim controlboxobj As New control_box
        Dim statetype1 As String = "数据"
        Dim statetype2 As String = "时段"
        Dim timestart, timeend As Date
        ' Dim starttime, endtime As DateTime
        'Dim timestring As String
        'timestring = System.Convert.ToDateTime(dtp_starttime_state.Value).Date.ToString
        'starttime = System.Convert.ToDateTime(timestring)
        'timestring = System.Convert.ToDateTime(dtp_endtime_state.Value).Date.AddHours(24).ToString
        'endtime = System.Convert.ToDateTime(timestring)



        Dim recData(20) As String '接收的数据
        Dim box_type As Integer '电控箱类型
        Dim state_kind As String '为不同的状态类型分类，通信，供电，状态

        Dim i As Integer = 0
        Dim j As Integer = 0

        progress_value = 1
        msg = ""

        If check_state = "全部" Then
            sql = "select * from control_box_state where createtime>='" & m_starttime & "' and createtime<'" & m_endtime & "' and control_box_name='" & controlboxname & "' and kaiguan_string<>'" & statetype1 & "' and kaiguan_string<>'" & statetype2 & "' order by kaiguan_string,createtime"

        Else
            If check_state = "正常" Then

                sql = "select * from control_box_state where createtime>='" & m_starttime & "' and createtime<'" & m_endtime & "' and control_box_name='" & controlboxname & "' and (StatusContent ='" & check_state & "' or StatusContent='通信正常'or StatusContent='交流电') and kaiguan_string<>'" & statetype1 & "' and kaiguan_string<>'" & statetype2 & "' order by kaiguan_string, createtime"

            Else
                sql = "select * from control_box_state where createtime>='" & m_starttime & "' and createtime<'" & m_endtime & "' and control_box_name='" & controlboxname & "' and StatusContent<>'正常' and StatusContent<>'通信正常' and StatusContent<>'交流电' and kaiguan_string<>'" & statetype1 & "' and kaiguan_string<>'" & statetype2 & "' order by kaiguan_string, createtime"
            End If

        End If
        DBOperation.OpenConn(conn)
        rs_box = DBOperation.SelectSQL(conn, sql, msg)
        If rs_box Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "sanyao_state", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        While rs_box.EOF = False
            If Me.BackgroundWorker_find.CancellationPending = True Then
                GoTo finish
            End If
            controlboxname = Trim(rs_box.Fields("control_box_name").Value)
            box_type = controlboxobj.Get_boxtype_name(controlboxname)  '主控箱类型
            state_kind = Trim(rs_box.Fields("kaiguan_string").Value)
            '显示进度
            num = 100 * progress_value / rs_box.RecordCount
            If num > 100 Then
                num = 100
            End If
            Me.BackgroundWorker_find.ReportProgress(num)
            progress_value += 1

            timestart = rs_box.Fields("createtime").Value
            If timestart < m_starttime Then  '开始时间小于查询的开始时间，将状态的开始时间设为查询时间
                timestart = m_starttime
            End If
            If rs_box.Fields("StatusContent2").Value Is System.DBNull.Value Then
                timeend = Me.m_endtime
            Else
                timeend = System.Convert.ToDateTime(Trim(rs_box.Fields("StatusContent2").Value))
            End If

            If rs_box.Fields("StatusContent").Value Is System.DBNull.Value Then
                statestring = ""
            Else
                statestring = Trim(rs_box.Fields("StatusContent").Value)
            End If

            i = 0
            If m_exceltable = 0 Then
                SetDataGridviewDelegateState(controlboxname, dgv_statelist, statestring, timestart, timeend, state_kind)

            Else  '导出报表

                m_xlApp.Cells(m_row, 1) = "'" & m_id
                m_xlApp.Cells(m_row, 2) = "'" & controlboxname
                m_xlApp.Cells(m_row, 3) = statestring
                m_xlApp.Cells(m_row, 4) = "'" & timestart
                m_xlApp.Cells(m_row, 5) = "'" & timeend

                m_row += 1

                m_id += 1

            End If

            rs_box.MoveNext()
        End While

finish:
        If rs_box.State = 1 Then
            rs_box.Close()
            rs_box = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub


    ''' <summary>
    '''   '按电控箱进行终端状态查询
    ''' </summary>
    ''' <param name="check_state"></param>
    ''' <param name="controlboxname"></param>
    ''' <remarks></remarks>
    Private Sub sanyao_data(ByVal check_state As String, ByVal controlboxname As String)

        Dim rs_box As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim progress_value As Integer
        Dim num As Integer
        Dim datastring As String
        Dim controlboxobj As New control_box
        Dim statetype As String = "数据"

        Dim recData(20) As String '接收的数据
        Dim box_type As Integer '电控箱类型
        Dim data1, data2, data3 As String

        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim presure_type As Integer
        progress_value = 1
        msg = ""

        If check_state = "全部" Then
            sql = "select * from control_box_state where createtime>='" & m_starttime & "' and createtime<='" & m_endtime & "' and control_box_name='" & controlboxname & "' and kaiguan_string='" & statetype & "' order by createtime"

        Else
            If check_state = "正常" Then

                sql = "select * from control_box_state where createtime>='" & m_starttime & "' and createtime<='" & m_endtime & "' and control_box_name='" & controlboxname & "' and state='" & check_state & "' and kaiguan_string='" & statetype & "' order by createtime"

            Else
                sql = "select * from control_box_state where createtime>='" & m_starttime & "' and createtime<='" & m_endtime & "' and control_box_name='" & controlboxname & "' and state<>'正常' and kaiguan_string='" & statetype & "' order by createtime"
            End If

        End If
        DBOperation.OpenConn(conn)
        rs_box = DBOperation.SelectSQL(conn, sql, msg)
        If rs_box Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "sanyao_state", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        While rs_box.EOF = False
            If Me.BackgroundWorker_find.CancellationPending = True Then
                GoTo finish
            End If
            controlboxname = Trim(rs_box.Fields("control_box_name").Value)
            box_type = controlboxobj.Get_boxtype_name(controlboxname)  '主控箱类型
            '显示进度
            num = 100 * progress_value / rs_box.RecordCount
            If num > 100 Then
                num = 100
            End If
            Me.BackgroundWorker_find.ReportProgress(num)
            progress_value += 1
            'If rs_box.Fields("StatusContent").Value Is System.DBNull.Value Then
            '    datastring = ""
            'Else
            '    datastring = Trim(rs_box.Fields("StatusContent").Value)
            'End If
            Dim board_num As Integer = 0
            '1号测量板
            If rs_box.Fields("statuscontent").Value Is System.DBNull.Value Then
                data1 = ""  '电流电压等值
            Else
                data1 = Trim(rs_box.Fields("statusContent").Value)  '电流电压等值
                board_num += 1
            End If
            '2号测量板
            If rs_box.Fields("statuscontent2").Value Is System.DBNull.Value Then
                data2 = "" '电流电压等值
            Else
                data2 = Trim(rs_box.Fields("statusContent2").Value)  '电流电压等值
                board_num += 1
            End If
            '3号测量板
            If rs_box.Fields("statuscontent3").Value Is System.DBNull.Value Then
                data3 = ""  '电流电压等值
            Else
                data3 = Trim(rs_box.Fields("statusContent3").Value)  '电流电压等值
                board_num += 1
            End If
            If data2 <> "" Then
                datastring = Trim(data1 & " " & data2 & " " & data3)
            Else
                datastring = Trim(data1 & " " & data3)

            End If

            recData = datastring.Split(" ")
            If recData.Length = 21 Then
                '小三遥
                m_small_sanyao = True
            Else
                m_small_sanyao = False
            End If

            i = 0


            If m_exceltable = 0 Then
                SetDataGridviewDelegate(controlboxname, dgv_boxinf, 0, datastring, rs_box.Fields("Createtime").Value, box_type, board_num)

            Else  '导出报表

                Dim data_state() As String
                Dim rowcount As Integer
                Dim t As Integer = 0

                m_xlApp.Cells(m_row, 1) = "'" & m_id
                m_xlApp.Cells(m_row, 2) = "'" & rs_box.Fields("createtime").Value
                m_xlApp.Cells(m_row, 3) = "'" & controlboxname
                data_state = datastring.Split(" ")
                m_id += 1
                i = 3
                If box_type < 4 Then
                    'AAAABBBBCCCC
                    If data_state.Length = 21 Then
                        '小三遥数据
                        rowcount = (data_state.Length - 3) / 3
                        j = 0
                        While j < rowcount
                        
                            '小三遥数据
                            If j < 2 Then
                                'A相
                                m_xlApp.Cells(m_row, 4) = "'" & (j + 1).ToString & "(" & controlboxobj.m_huilu_small(j) & ")"
                                m_xlApp.Cells(m_row, 5) = "'" & recData(0)
                                m_xlApp.Cells(m_row, 6) = "'" & recData(i)
                                m_xlApp.Cells(m_row, 7) = "'" & Format(recData(i + 1) / 1000, "0.000")
                                m_xlApp.Cells(m_row, 8) = "'" & recData(i + 2)
                            Else
                                If j < 4 Then
                                    'B相
                                    m_xlApp.Cells(m_row, 4) = "'" & (j + 1).ToString & "(" & controlboxobj.m_huilu_small(j) & ")"
                                    m_xlApp.Cells(m_row, 5) = "'" & recData(1)
                                    m_xlApp.Cells(m_row, 6) = "'" & recData(i)
                                    m_xlApp.Cells(m_row, 7) = "'" & Format(recData(i + 1) / 1000, "0.000")
                                    m_xlApp.Cells(m_row, 8) = "'" & recData(i + 2)
                                Else 'C相
                                    m_xlApp.Cells(m_row, 4) = "'" & (j + 1).ToString & "(" & controlboxobj.m_huilu_small(j) & ")"
                                    m_xlApp.Cells(m_row, 5) = "'" & recData(2)
                                    m_xlApp.Cells(m_row, 6) = "'" & recData(i)
                                    m_xlApp.Cells(m_row, 7) = "'" & Format(recData(i + 1) / 1000, "0.000")
                                    m_xlApp.Cells(m_row, 8) = "'" & recData(i + 2)
                                End If
                            End If
                            i += 3
                            j += 1
                            m_row += 1

                        End While
                    End If


                    If data_state.Length = 39 * board_num Then
                        '大三遥数据

                        While t < board_num
                            j = 0
                            While j < 12
                                If j < 4 Then
                                    'A相
                                    m_xlApp.Cells(m_row, 4) = "'" & (j + 1 + 12 * t).ToString & "(" & controlboxobj.m_huilu(j) & ")"
                                    m_xlApp.Cells(m_row, 5) = "'" & recData(0)
                                    m_xlApp.Cells(m_row, 6) = "'" & recData(i)
                                    m_xlApp.Cells(m_row, 7) = "'" & Format(recData(i + 1) / 1000, "0.000")
                                    m_xlApp.Cells(m_row, 8) = "'" & recData(i + 2)
                                Else
                                    If j < 8 Then
                                        'B相
                                        m_xlApp.Cells(m_row, 4) = "'" & (j + 1 + 12 * t).ToString & "(" & controlboxobj.m_huilu(j) & ")"
                                        m_xlApp.Cells(m_row, 5) = "'" & recData(1)
                                        m_xlApp.Cells(m_row, 6) = "'" & recData(i)
                                        m_xlApp.Cells(m_row, 7) = "'" & Format(recData(i + 1) / 1000, "0.000")
                                        m_xlApp.Cells(m_row, 8) = "'" & recData(i + 2)
                                    Else 'C相
                                        m_xlApp.Cells(m_row, 4) = "'" & (j + 1 + 12 * t).ToString & "(" & controlboxobj.m_huilu(j) & ")"
                                        m_xlApp.Cells(m_row, 5) = "'" & recData(2)
                                        m_xlApp.Cells(m_row, 6) = "'" & recData(i)
                                        m_xlApp.Cells(m_row, 7) = "'" & Format(recData(i + 1) / 1000, "0.000")
                                        m_xlApp.Cells(m_row, 8) = "'" & recData(i + 2)
                                    End If
                                End If
                                i += 3
                                j += 1
                                m_row += 1
                            End While

                            t += 1

                        End While

                    End If  '大三遥

                Else
                    'ABCABCABCABC

                    If data_state.Length = 21 Then
                        '小三遥数据
                        rowcount = (data_state.Length - 3) / 3
                        j = 0
                        While j < rowcount
                            presure_type = controlboxobj.get_presuretype(j + 1, controlboxname)
                            'm_xlApp.Cells(m_row, 1) = "'" & m_id
                            'm_xlApp.Cells(m_row, 2) = "'" & rs_box.Fields("createtime").Value
                            'm_xlApp.Cells(m_row, 3) = "'" & controlboxname
                            '小三遥数据
                            If presure_type = 0 Then
                                'A相
                                m_xlApp.Cells(m_row, 4) = (j + 1).ToString & "(A)"
                                m_xlApp.Cells(m_row, 5) = "'" & recData(0)
                                m_xlApp.Cells(m_row, 6) = "'" & recData(i)
                                m_xlApp.Cells(m_row, 7) = "'" & Format(recData(i + 1) / 1000, "0.000")
                                m_xlApp.Cells(m_row, 8) = "'" & recData(i + 2)
                            Else
                                If presure_type = 1 Then
                                    'B相
                                    m_xlApp.Cells(m_row, 4) = (j + 1).ToString & "(B)"
                                    m_xlApp.Cells(m_row, 5) = "'" & recData(1)
                                    m_xlApp.Cells(m_row, 6) = "'" & recData(i)
                                    m_xlApp.Cells(m_row, 7) = "'" & Format(recData(i + 1) / 1000, "0.000")
                                    m_xlApp.Cells(m_row, 8) = "'" & recData(i + 2)
                                Else 'C相
                                    m_xlApp.Cells(m_row, 4) = (j + 1).ToString & "(C)"
                                    m_xlApp.Cells(m_row, 5) = "'" & recData(2)
                                    m_xlApp.Cells(m_row, 6) = "'" & recData(i)
                                    m_xlApp.Cells(m_row, 7) = "'" & Format(recData(i + 1) / 1000, "0.000")
                                    m_xlApp.Cells(m_row, 8) = "'" & recData(i + 2)
                                End If
                            End If
                            i += 3
                            j += 1
                            m_row += 1

                        End While
                        m_id += 1
                    End If


                    If data_state.Length = 39 * board_num Then
                        '大三遥数据

                        While t < board_num
                            j = 0
                            While j < 12
                                presure_type = controlboxobj.get_presuretype(j + 1 + 12 * t, controlboxname)
                                'm_xlApp.Cells(m_row, 1) = "'" & m_id
                                'm_xlApp.Cells(m_row, 2) = "'" & rs_box.Fields("createtime").Value
                                'm_xlApp.Cells(m_row, 3) = "'" & controlboxname
                                If presure_type = 0 Then
                                    'A相
                                    m_xlApp.Cells(m_row, 4) = (j + 1 + 12 * t).ToString & "(A)"
                                    m_xlApp.Cells(m_row, 5) = "'" & recData(0 + 39 * t)
                                    m_xlApp.Cells(m_row, 6) = "'" & recData(i)
                                    m_xlApp.Cells(m_row, 7) = "'" & Format(recData(i + 1) / 1000, "0.000")
                                    m_xlApp.Cells(m_row, 8) = "'" & recData(i + 2)
                                Else
                                    If presure_type = 1 Then
                                        'B相
                                        m_xlApp.Cells(m_row, 4) = (j + 1 + 12 * t).ToString & "(B)"
                                        m_xlApp.Cells(m_row, 5) = "'" & recData(1 + 39 * t)
                                        m_xlApp.Cells(m_row, 6) = "'" & recData(i)
                                        m_xlApp.Cells(m_row, 7) = "'" & Format(recData(i + 1) / 1000, "0.000")
                                        m_xlApp.Cells(m_row, 8) = "'" & recData(i + 2)
                                    Else 'C相
                                        m_xlApp.Cells(m_row, 4) = (j + 1 + 12 * t).ToString & "(C)"
                                        m_xlApp.Cells(m_row, 5) = "'" & recData(2 + 39 * t)
                                        m_xlApp.Cells(m_row, 6) = "'" & recData(i)
                                        m_xlApp.Cells(m_row, 7) = "'" & Format(recData(i + 1) / 1000, "0.000")
                                        m_xlApp.Cells(m_row, 8) = "'" & recData(i + 2)
                                    End If
                                End If
                                i += 3
                                j += 1
                                m_row += 1
                            End While

                            t += 1

                        End While

                    End If  '大三遥

                End If


            End If

            rs_box.MoveNext()
        End While

finish:
        If rs_box.State = 1 Then
            rs_box.Close()
            rs_box = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    Private Sub bt_stop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_stop.Click
        If Me.BackgroundWorker_find.IsBusy = True Then
            Me.BackgroundWorker_find.CancelAsync()
        End If
    End Sub

    Private Sub clear_text_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles clear_text.Click
        Me.dgv_boxinf.Rows.Clear()
    End Sub

    Private Sub bt_static_excel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_static_excel.Click

        If Me.BackgroundWorker_find.IsBusy = False Then
            If dtp_date_start.Text = "" Then  '开始日期为空
                MsgBox("请选择开始时间", , PROJECT_TITLE_STRING)
                dtp_date_start.Focus()  '光标定位在开始日期
                Exit Sub
            End If
            If dtp_date_end.Text = "" Then  '结束日期为空
                MsgBox("请选择结束时间", , PROJECT_TITLE_STRING)
                dtp_date_end.Focus()  '光标定位在结束日期
                Exit Sub
            End If
            ' Dim timestring As String
            'timestring = System.Convert.ToDateTime(dtp_starttime_state.Value).Date.ToString
            m_starttime = dtp_date_start.Value
            ' timestring = System.Convert.ToDateTime(dtp_endtime_state.Value).Date.AddHours(24).ToString
            m_endtime = dtp_date_end.Value
            m_exceltable = 1  '标志为excel表
            m_checklist.Clear() '清除所有选中的项目
            Dim tnRet As New TreeNode
            For Each treenode As TreeNode In tv_yaoce_controlbox.Nodes
                Com_inf.FindNode(treenode, m_checklist)
            Next
            If m_checklist.Count = 0 Then
                MsgBox("请选择需设置的主控箱名称", , PROJECT_TITLE_STRING)
                Exit Sub
            End If
            m_findtag = 1 '标志着查询的是主控箱的数据
            m_id = 1

            ProgressBar.Visible = True
            'm_timestart = dtp_date_start.Value  '开始日期
            'm_timeend = dtp_date_end.Value  '结束日期
            Me.BackgroundWorker_find.RunWorkerAsync()
        Else
            MsgBox("查询正在进行，请稍候....", , PROJECT_TITLE_STRING)
        End If
       
       
    End Sub

    Private Sub BackgroundWorker_find_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker_find.ProgressChanged
        If Me.BackgroundWorker_find.CancellationPending = True Then
            Exit Sub

        End If
        ProgressBar.Value = e.ProgressPercentage
        If m_exceltable = 0 And m_stringtag = 0 Then
            g_welcomewinobj.circle_string.Text = "查询主控箱数据统计信息"
            '   record_num.Text = "查询主控箱数据统计信息"
            m_stringtag = 1

        Else
            If m_stringtag = 0 And m_exceltable = 1 Then
                g_welcomewinobj.circle_string.Text = "导出主控箱数据统计的EXCEL表"
                '   record_num.Text = "导出主控箱数据统计的EXCEL表"
                m_stringtag = 1
            End If

        End If
    End Sub

    Private Sub BackgroundWorker_find_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker_find.RunWorkerCompleted
        ProgressBar.Value = 0
        m_id = 1
        ProgressBar.Visible = False
        m_statetag = 1  '文字颜色
        If m_exceltable = 1 Then
            m_xlApp.Visible = True

        End If

    End Sub

    Private Sub bt_find_state_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_find_state.Click

        If Me.BackgroundWorker_find.IsBusy = False Then
            m_checklist.Clear() '清除所有选中的项目
            Me.dgv_statelist.Rows.Clear() '清除列表
            If Trim(cb_state_list.Text) = "" Then
                MsgBox("请选择数据状态", , PROJECT_TITLE_STRING)
                cb_state_list.Focus()
                Exit Sub
            End If
            ' Dim timestring As String
            'timestring = System.Convert.ToDateTime(dtp_starttime_state.Value).Date.ToString
            m_starttime = dtp_starttime_state.Value
            ' timestring = System.Convert.ToDateTime(dtp_endtime_state.Value).Date.AddHours(24).ToString
            m_endtime = dtp_endtime_state.Value
            Dim tnRet As New TreeNode
            For Each treenode As TreeNode In tv_boxlist_state.Nodes
                Com_inf.FindNode(treenode, m_checklist)
            Next
            If m_checklist.Count = 0 Then
                MsgBox("请选择需设置的主控箱名称", , PROJECT_TITLE_STRING)
                Exit Sub
            End If
            m_statestring = Trim(cb_state_list.Text)
            m_findtag = 2 '标志着查询的是主控箱的状态
            m_exceltable = 0  '标志为excel表
            Me.BackgroundWorker_find.RunWorkerAsync()
        Else
            MsgBox("查询正在进行，请稍候....", , PROJECT_TITLE_STRING)
        End If
      
    End Sub

  
    Private Sub tv_boxlist_state_AfterCheck(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tv_boxlist_state.AfterCheck
        Dim controlboxobj As New control_box
        If m_check = False Then
            m_check = controlboxobj.setChild(e.Node)
        End If

        controlboxobj.setparent(e.Node)
        m_check = False

    End Sub

    Private Sub bt_stop_state_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_stop_state.Click
        If Me.BackgroundWorker_find.IsBusy = True Then
            Me.BackgroundWorker_find.CancelAsync()
        End If
    End Sub

    Private Sub bt_clear_state_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_clear_state.Click
        dgv_statelist.Rows.Clear()
    End Sub

    Private Sub bt_excel_state_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_excel_state.Click

        If Me.BackgroundWorker_find.IsBusy = False Then
            If dtp_starttime_state.Text = "" Then  '开始日期为空
                MsgBox("请选择开始时间", , PROJECT_TITLE_STRING)
                dtp_starttime_state.Focus()  '光标定位在开始日期
                Exit Sub
            End If
            If dtp_endtime_state.Text = "" Then  '结束日期为空
                MsgBox("请选择结束时间", , PROJECT_TITLE_STRING)
                dtp_endtime_state.Focus()  '光标定位在结束日期
                Exit Sub
            End If
            If Trim(cb_state_list.Text) = "" Then
                MsgBox("请选择数据状态", , PROJECT_TITLE_STRING)
                cb_state_list.Focus()
                Exit Sub
            End If
            ' Dim timestring As String
            'timestring = System.Convert.ToDateTime(dtp_starttime_state.Value).Date.ToString
            m_starttime = dtp_starttime_state.Value
            ' timestring = System.Convert.ToDateTime(dtp_endtime_state.Value).Date.AddHours(24).ToString
            m_endtime = dtp_endtime_state.Value

            m_checklist.Clear() '清除所有选中的项目
            Dim tnRet As New TreeNode
            For Each treenode As TreeNode In tv_boxlist_state.Nodes
                Com_inf.FindNode(treenode, m_checklist)
            Next
            If m_checklist.Count = 0 Then
                MsgBox("请选择需设置的主控箱名称", , PROJECT_TITLE_STRING)
                Exit Sub
            End If

            m_exceltable = 1  '标志为excel表
            m_findtag = 2 '标志着查询的是主控箱的状态
            ProgressBar.Visible = True
            'm_timestart = dtp_starttime_state.Value  '开始日期
            'm_timeend = dtp_endtime_state.Value  '结束日期
            Me.BackgroundWorker_find.RunWorkerAsync()
        Else
            MsgBox("查询正在进行，请稍候....", , PROJECT_TITLE_STRING)
        End If
       
     
    End Sub

    Private Sub dgv_statelist_CellPainting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellPaintingEventArgs) Handles dgv_statelist.CellPainting

        If e.RowIndex > -1 And e.ColumnIndex > -1 Then
            DrawCell(e)
        End If
    End Sub


    Private Sub bt_find_kaiguan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_find_kaiguan.Click
        If Me.BackgroundWorker_find.IsBusy = False Then
            m_checklist.Clear() '清除所有选中的项目
            Me.dgv_kaiguanlist.Rows.Clear() '清除列表
        
            m_starttime = dtp_starttime_kaiguan.Value
            m_endtime = dtp_endtime_kaiguan.Value
            Dim tnRet As New TreeNode
            For Each treenode As TreeNode In tv_boxlist_kaiguan.Nodes
                Com_inf.FindNode(treenode, m_checklist)
            Next
            If m_checklist.Count = 0 Then
                MsgBox("请选择需设置的主控箱名称", , PROJECT_TITLE_STRING)
                Exit Sub
            End If

            m_findtag = 3 '标志着查询的是主控箱的开关量
            m_exceltable = 0  '标志为非excel表
            Me.BackgroundWorker_find.RunWorkerAsync()
        Else
            MsgBox("查询正在进行，请稍候....", , PROJECT_TITLE_STRING)
        End If
    End Sub

    Private Sub bt_excel_kaiguan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_excel_kaiguan.Click
        If Me.BackgroundWorker_find.IsBusy = False Then
            If dtp_starttime_kaiguan.Text = "" Then  '开始日期为空
                MsgBox("请选择开始时间", , PROJECT_TITLE_STRING)
                dtp_starttime_kaiguan.Focus()  '光标定位在开始日期
                Exit Sub
            End If
            If dtp_endtime_kaiguan.Text = "" Then  '结束日期为空
                MsgBox("请选择结束时间", , PROJECT_TITLE_STRING)
                dtp_endtime_kaiguan.Focus()  '光标定位在结束日期
                Exit Sub
            End If
           
            m_starttime = dtp_starttime_kaiguan.Value
            ' timestring = System.Convert.ToDateTime(dtp_endtime_state.Value).Date.AddHours(24).ToString
            m_endtime = dtp_endtime_kaiguan.Value

            m_checklist.Clear() '清除所有选中的项目
            Dim tnRet As New TreeNode
            For Each treenode As TreeNode In tv_boxlist_kaiguan.Nodes
                Com_inf.FindNode(treenode, m_checklist)
            Next
            If m_checklist.Count = 0 Then
                MsgBox("请选择需设置的主控箱名称", , PROJECT_TITLE_STRING)
                Exit Sub
            End If

            m_exceltable = 1  '标志为excel表
            m_findtag = 3 '标志着查询的是主控箱的状态
            ProgressBar.Visible = True
            'm_timestart = dtp_starttime_state.Value  '开始日期
            'm_timeend = dtp_endtime_state.Value  '结束日期
            Me.BackgroundWorker_find.RunWorkerAsync()
        Else
            MsgBox("查询正在进行，请稍候....", , PROJECT_TITLE_STRING)
        End If

    End Sub


    Private Sub tv_boxlist_kaiguan_AfterCheck(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tv_boxlist_kaiguan.AfterCheck
        Dim controlboxobj As New control_box
        If m_check = False Then
            m_check = controlboxobj.setChild(e.Node)
        End If

        controlboxobj.setparent(e.Node)
        m_check = False
    End Sub

    Private Sub bt_stop_kaiguan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_stop_kaiguan.Click
        If Me.BackgroundWorker_find.IsBusy = True Then
            Me.BackgroundWorker_find.CancelAsync()
        End If
    End Sub

    Private Sub bt_clear_kaiguan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_clear_kaiguan.Click
        dgv_kaiguanlist.Rows.Clear()
    End Sub
End Class