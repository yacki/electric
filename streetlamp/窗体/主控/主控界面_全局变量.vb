Partial Public Class welcome_win
    '主控窗体的全局变量及初始化部分
#Region "变量"
    '****************************绘制终端的变量***************************************

    Private m_lamp As New Bitmap(3000, 565)   '绘制终端的bmp图
    ' Private m_fullcolor, m_partcolor, m_closecolor, m_problemcolor, m_noreturncolor As System.Drawing.Color   '状态颜色分别是全功率，半功率，无返回状态，关，故障
    Private m_lvlighton, m_lvlightoff, m_lvlightproblem, m_lvnoreturn As Double  '开灯，关灯,故障，无返回值概率
    Private m_controllampobj As New control_lamp  'control_lamp_obj的实例化对象
    Private m_divtimestart As Integer    '时段控制的开始标志
    ' Private m_editboxobj As New 编辑电控箱  '编辑电控箱对象
    Private m_addboxobj As 主控箱 '增加电控箱
    Private m_addstreetobj As New 增加查询街道  '增加区域定位对象
    Private m_waittime As Integer    '发送单灯查询状态后，等待状态返回的时间
    Private m_findtag As Integer  '判断是否有状态返回的查询标志
    Private m_controlboxnamestring, m_typestring, m_lampidstring As String   '区域名称，类型，景观灯编号
    Private m_holidaytitle As String  '节日模式标题
    Private m_addlampobj As 增加终端 '增加终端
    Private m_checkcondition As Integer  '查询的条件，0表示查询所有，1表示查询某一个区域
    Private m_checkvalue As String  '按区域查询的区域名称
    Private m_rightbuttonpos As System.Drawing.Point  '单击右键时鼠标的坐标
    Private m_alarmopenorclose As Boolean 'true表示打开报警声音，false表示静音
    Private m_jiankongopenorclose As Boolean 'true表示打开监控，false表示关闭监控面板
    Private m_tongjiopenorclose As Boolean 'true表示打开区域统计，false 表示关闭区域统计
    Private m_gongnengopenorclose As Boolean 'true表示打开功能提示面板 ，false表示关闭功能提示面板
    Private m_zhishitime As Integer  '显示指示图片的时间
    Private m_dgvviewtag As Integer '显示列表的时间
    Private m_lamptip As String  '手表移动时显示的字符串
    Private m_index As Integer  '记录当前选中行的位置
    Private m_indexbox As Integer  '电控箱的当前位置
    Private m_loadwaittime As Integer '统计每隔多长时间存储一次回路数据
    Public m_controlboxobj As New control_box  'control_box的实例化对象
    Public m_controlboxname As String '自动召测的时候显示的主控箱名称



    Private Structure m_control_box_IMEI  '区域的属性的结构体
        Dim IMEI As String
        Dim control_box_id As String
        Dim control_box_name As String
        Dim state As String
        Dim elec_state As String
        Dim tag As Integer
    End Structure

    Public Structure m_box_IMEI
        Dim IMEI As String   'IMEI
        Dim control_box_id As String  '主控箱编号
        Dim control_box_name As String  '主控箱名称
    End Structure

    Private Structure m_control_box_on_off  '判断电控箱是否上电属性的结构体
        Dim control_box_string1 As String
        Dim control_box_string2 As String
        Dim tag As Integer  '0表示没有上电，1表示上电
    End Structure

    Public Structure m_alarminf  '报警信息结构体
        Dim control_box_name As String
        Dim alarm_msg As String
        Dim alarm_time As DateTime
    End Structure

    Public Structure m_boxinf
        Dim IMEI As String   'IMEI
        Dim control_box_id As String  '主控箱编号
        Dim control_box_name As String '主控箱名称
        Dim rowid As Integer '行数
        Dim metertype As String '表的类型
        Dim bianbi As Integer '变比
        Dim meterid As String '表的编号
    End Structure

    '报警声音
    Public Declare Function PlaySound Lib "winmm.dll" (ByVal lpszSoundName As String, ByVal hModule As Integer, ByVal dwFlags As Integer) As Integer
    Const SND_FILENAME As Integer = &H20000
    Public mstrfileName As String = "alarm.wav"


    '定义的委托
    Delegate Sub SetTextControlCallBack(ByVal text As String, ByVal appendOrSet As Boolean, ByVal control As Control)  '用于设置文本或者是获取文本内容
    Delegate Sub SetColor(ByVal picbox As Windows.Forms.PictureBox, ByVal color As System.Drawing.Color)  '更换颜色
    Delegate Sub SetVisual(ByVal picbox As Windows.Forms.PictureBox, ByVal visual As Boolean)  '图片框是否可视
    Delegate Sub SetMap(ByVal picbox As Windows.Forms.PictureBox, ByVal map As System.Drawing.Bitmap, ByVal add As Boolean)  '设置地图图片
    Delegate Sub SetStatusStripText(ByVal text As String, ByVal Label_obj As StatusStrip, ByVal name As String)  '设置label的文字
    Delegate Sub Movtext(ByVal Label As Windows.Forms.Label, ByVal Groupbox As Windows.Forms.GroupBox)  '移动文字
    Delegate Sub ClearTreeView(ByVal treeview As Windows.Forms.TreeView)  '清除树形列表
    Delegate Sub AddTreeView(ByVal text As String, ByVal treeview As Windows.Forms.TreeView, ByVal node_level As Integer, _
                 ByVal node_level_num As Integer, ByVal imagekey As Integer, ByVal level1_num As Integer, ByVal _
                 level2_num As Integer)                '增加树形列表中的内容
    Delegate Sub SetDataGridview(ByVal row As Integer, ByVal text1 As String, ByVal text2 As String, ByVal text3 As String, ByVal datagridview _
                 As Windows.Forms.DataGridView, ByVal end_tag As System.Boolean)        '设置DataGridview中的内容
    Delegate Sub ClearDataGridview(ByVal datagridview As Windows.Forms.DataGridView)  '清除DataGridview中的内容
    Delegate Sub SetMapSize(ByVal picbox As Windows.Forms.PictureBox)                 '设置地图的大小
    Delegate Sub SetControlBoxList(ByVal Lamp_State As Windows.Forms.DataGridView, ByVal find_condition As Integer)



#End Region

#Region "委托"

    Public Sub SetControlBoxListDelegate(ByVal Lamp_State As Windows.Forms.DataGridView, ByVal find_condition As Integer)

        If Lamp_State.InvokeRequired Then
            Dim stateobj As SetControlBoxList = New SetControlBoxList(AddressOf SetControlBoxListDelegate)
            Me.Invoke(stateobj, New Object() {Lamp_State, find_condition})

        Else
            '右边的终端统计信息
            Dim probleminf As String = ""
            Dim lampkind As String = ""
            Dim lampstate As String = ""  '灯的开关情况，div_time_id列>=8为开，<8的为关
            Dim lamp_pointinfor As String = ""

            If find_condition = 0 Then  '载入所有的电控箱信息

                Me.Lamp_streetTableAdapter.Fill(Me.State_list.lamp_street, g_choosemapid)
            Else
                Me.Lamp_streetTableAdapter.FillBy_area(Me.State_list.lamp_street, m_checkvalue, g_choosemapid)
            End If

            Dim row As Integer
            row = 0
            While row < Me.dgv_lamp_state_list.RowCount
                If Me.dgv_lamp_state_list.Rows(row).Cells("lampkind").Value IsNot System.DBNull.Value Then
                    lampkind = Trim(Me.dgv_lamp_state_list.Rows(row).Cells("lampkind").Value)
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
                    Me.dgv_lamp_state_list.Rows(row).Cells("presure_lamp").Value = Me.dgv_lamp_state_list.Rows(row).Cells("presure_l").Value
                    Me.dgv_lamp_state_list.Rows(row).Cells("yinshu_lamp").Value = Me.dgv_lamp_state_list.Rows(row).Cells("presure_end").Value
                    Me.dgv_lamp_state_list.Rows(row).Cells("power_lamp").Value = Me.dgv_lamp_state_list.Rows(row).Cells("power1").Value
                End If

                'Com_inf.Get_DengGan(Trim(Me.dgv_lamp_state_list.Rows(row).Cells("lamp_id").Value))
                If IsDBNull(Me.dgv_lamp_state_list.Rows(row).Cells("lamp_pointinfor").Value) = False Then
                    lamp_pointinfor = Me.dgv_lamp_state_list.Rows(row).Cells("lamp_pointinfor").Value
                    If Trim(lamp_pointinfor) <> "" Then
                        lamp_pointinfor = "  位置:" & lamp_pointinfor
                    Else
                        lamp_pointinfor = ""
                    End If
                Else
                    lamp_pointinfor = ""
                End If
                Me.dgv_lamp_state_list.Rows(row).Cells("lamp_id_part").Value = Val(Mid(Me.dgv_lamp_state_list.Rows(row).Cells("lamp_id").Value, 1, 4)).ToString & "-" & Val(Mid(Me.dgv_lamp_state_list.Rows(row).Cells("lamp_id").Value, 5, 2)).ToString & "-" & Val(Mid(Me.dgv_lamp_state_list.Rows(row).Cells("lamp_id").Value, 7, 5)).ToString
                If lampkind = "3" Then
                    probleminf = m_controllampobj.get_probleminf(Me.dgv_lamp_state_list.Rows(row).Cells("state").Value)
                    If Me.dgv_lamp_state_list.Rows(row).Cells("div_time_id").Value = "0" Then
                        '表示关闭
                        Me.dgv_lamp_state_list.Rows(row).Cells("open_close").Value = "关闭"
                    Else
                        If Me.dgv_lamp_state_list.Rows(row).Cells("div_time_id").Value = "1" Then
                            '表示打开
                            Me.dgv_lamp_state_list.Rows(row).Cells("open_close").Value = "打开"
                        Else
                            '表示初始状态
                            Me.dgv_lamp_state_list.Rows(row).Cells("open_close").Value = "-"

                        End If

                    End If

                    Me.dgv_lamp_state_list.Rows(row).Cells("state_content").Value = probleminf


                Else
                    If Me.dgv_lamp_state_list.Rows(row).Cells("div_time_id").Value = 0 Then
                        '表示关闭
                        Me.dgv_lamp_state_list.Rows(row).Cells("open_close").Value = "关闭"
                    Else
                        If Me.dgv_lamp_state_list.Rows(row).Cells("div_time_id").Value = 1 Then
                            '表示打开
                            Me.dgv_lamp_state_list.Rows(row).Cells("open_close").Value = "打开"
                        Else
                            '表示初始状态
                            Me.dgv_lamp_state_list.Rows(row).Cells("open_close").Value = "-"

                        End If

                    End If
                    If Me.dgv_lamp_state_list.Rows(row).Cells("result").Value = 0 Then
                        If Me.dgv_lamp_state_list.Rows(row).Cells("state").Value = 1 Or Me.dgv_lamp_state_list.Rows(row).Cells("state").Value = 4 Then
                            Me.dgv_lamp_state_list.Rows(row).Cells("state_content").Value = LAMP_STATE_ON
                        Else
                            Me.dgv_lamp_state_list.Rows(row).Cells("state_content").Value = LAMP_STATE_OFF
                        End If
                    Else
                        If Me.dgv_lamp_state_list.Rows(row).Cells("result").Value = 1 Then
                            Me.dgv_lamp_state_list.Rows(row).Cells("state_content").Value = LAMP_STATE_PROBLEM_ON

                        Else
                            If Me.dgv_lamp_state_list.Rows(row).Cells("result").Value = 2 Then
                                Me.dgv_lamp_state_list.Rows(row).Cells("state_content").Value = LAMP_STATE_PROBLEM_OFF

                            Else
                                If Me.dgv_lamp_state_list.Rows(row).Cells("result").Value = 3 Then
                                    Me.dgv_lamp_state_list.Rows(row).Cells("state_content").Value = LAMP_STATE_NORETURN
                                Else
                                    Me.dgv_lamp_state_list.Rows(row).Cells("state_content").Value = LAMP_STATE_CONTROL
                                End If
                            End If

                        End If
                    End If
                End If

                row += 1

            End While
            If Me.dgv_lamp_state_list.RowCount > 0 Then
                If m_index > Me.dgv_lamp_state_list.RowCount Then
                    m_index = 0
                End If
                Me.dgv_lamp_state_list.Rows(m_index).Selected = True
                Me.dgv_lamp_state_list.FirstDisplayedScrollingRowIndex = m_index
            End If
            '  Dim temp As DataGridView = Me.dgv_lamp_state_list

        End If
    End Sub


    ''' <summary>
    ''' 设置地图的大小，进行缩放操作
    ''' </summary>
    ''' <param name="picbox">地图的图片框</param>
    ''' <remarks></remarks>

    Public Sub SetMapSizeDelegate(ByVal picbox As Windows.Forms.PictureBox)
        If picbox.InvokeRequired Then
            Dim picboxobj As SetMapSize = New SetMapSize(AddressOf SetMapSizeDelegate)
            Me.Invoke(picboxobj, New Object() {picbox})
        Else

            Dim pos1, pos2 As System.Drawing.Point  '缩小之前和之后的点的坐标
            Dim percent_x, percent_y As Double  '纵横百分比
            ' map_size_value = map.Size '地图的尺寸
            'g_mapsizevalue = map.Size '地图的尺寸

            If picbox.Width = 0 Or picbox.Height = 0 Then
                Exit Sub
            End If
            pos1.X = Me.GroupBox1.Width / 2 - picbox.Left
            pos1.Y = Me.GroupBox1.Height / 2 - picbox.Top
            percent_x = System.Math.Abs(pos1.X) / picbox.Width
            percent_y = System.Math.Abs(pos1.Y) / picbox.Height
            'If picbox.Width = 0 Then
            '    percent_x = 1
            'Else
            '    percent_x = System.Math.Abs(pos1.X) / picbox.Width

            'End If

            'If picbox.Height = 0 Then
            '    percent_y = 1
            'Else
            '    percent_y = System.Math.Abs(pos1.Y) / picbox.Height

            'End If


            picbox.Width = g_mapsizevalue.Width * (MAP_SIZE_BASE + MAP_SIZE_CHANGE * tb_map_size.Value)   '设置图片框的宽度
            picbox.Height = g_mapsizevalue.Height * (MAP_SIZE_BASE + MAP_SIZE_CHANGE * tb_map_size.Value)  '设置图片框的高度

            pos2.X = picbox.Width * percent_x
            pos2.Y = picbox.Height * percent_y

            picbox.Left += pos1.X - pos2.X    '图片缩放后，图片的左上角X坐标
            picbox.Top += pos1.Y - pos2.Y   '图片缩放后，图片的左上角Y坐标


        End If
    End Sub

    ''' <summary>
    ''' 清除列表中的内容
    ''' </summary>
    ''' <param name="datagridview">列表名</param>
    ''' <remarks></remarks>
    Public Sub ClearDataGridviewDelegate(ByVal datagridview As Windows.Forms.DataGridView)
        If datagridview.InvokeRequired Then
            Dim datagridviewobj As ClearDataGridview = New ClearDataGridview(AddressOf ClearDataGridviewDelegate)
            Me.Invoke(datagridviewobj, New Object() {datagridview})
        Else
            datagridview.Rows.Clear()   '清除列表中的内容


        End If
    End Sub

    ''' <summary>
    ''' 设置列表中的内容
    ''' </summary>
    ''' <param name="row">列表的行数</param>
    ''' <param name="text1">第一列的内容</param>
    ''' <param name="text2">第二列的内容</param>
    ''' <param name="datagridview">列表名</param>
    ''' <remarks></remarks>
    ''' 
    Public Sub SetDataGridviewDelegate(ByVal row As Integer, ByVal text1 As String, ByVal text2 As String, ByVal text3 As String, ByVal datagridview As Windows.Forms.DataGridView, ByVal end_tag As System.Boolean)
        If datagridview.InvokeRequired Then
            Dim datagridviewobj As SetDataGridview = New SetDataGridview(AddressOf SetDataGridviewDelegate)
            Me.Invoke(datagridviewobj, New Object() {row, text1, text2, text3, datagridview, end_tag})
        Else
            datagridview.Rows.Add()     '增加行
            datagridview.Rows(row).Cells(0).Value = text1   '第一列的内容
            If text2 = "通信不正常" Then
                datagridview.Rows(row).Cells(1).Style.ForeColor = Color.Red
            Else
                datagridview.Rows(row).Cells(1).Style.ForeColor = Color.Black

            End If
            datagridview.Rows(row).Cells(1).Value = text2   '第二列的内容
            datagridview.Rows(row).Cells(2).Value = text3   '第二列的内容
            datagridview.Update()    '更新

            If end_tag = True Then
                If Me.dgv_control_box_list.RowCount > 0 Then
                    If m_indexbox > Me.dgv_control_box_list.RowCount Then
                        m_indexbox = 0
                    End If
                    Me.dgv_control_box_list.Rows(m_indexbox).Selected = True
                    Me.dgv_control_box_list.FirstDisplayedScrollingRowIndex = m_indexbox
                End If
            End If


        End If
    End Sub

    ''' <summary>
    ''' 清除树形列表的内容
    ''' </summary>
    ''' <param name="treeview">树形列表名称</param>
    ''' <remarks></remarks>
    Public Sub ClearTreeViewDelegate(ByVal treeview As Windows.Forms.TreeView)
        If treeview.InvokeRequired Then
            Dim treeviewobj As ClearTreeView = New ClearTreeView(AddressOf ClearTreeViewDelegate)
            Me.Invoke(treeviewobj, New Object() {treeview})

        Else
            treeview.Nodes.Clear()   '清除树形列表的内容

        End If
    End Sub
    ''' <summary>
    ''' 增加树形列表内容及各层的图标
    ''' </summary>
    ''' <param name="text">节点内容</param>
    ''' <param name="treeview">树形列表的名称</param>
    ''' <param name="node_level">节点的层次号</param>
    ''' <param name="node_level_num">第二层的节点号（三层）</param>
    ''' <param name="imagekey">图标的KEY</param>
    ''' <param name="level1_num">第二层的节点号（两层）</param>
    ''' <param name="level2_num">第三层的节点号</param>
    ''' <remarks></remarks>

    Public Sub AddTreeViewDelegate(ByVal text As String, ByVal treeview As Windows.Forms.TreeView, ByVal node_level As Integer, ByVal node_level_num As Integer, ByVal imagekey As Integer, ByVal level1_num As Integer, ByVal level2_num As Integer)
        If treeview.InvokeRequired Then
            Dim treeviewobj As AddTreeView = New AddTreeView(AddressOf AddTreeViewDelegate)
            Me.Invoke(treeviewobj, New Object() {text, treeview, node_level, node_level_num, imagekey, level1_num, level2_num})

        Else

            If node_level = 0 Then   '第一层节点
                treeview.Nodes.Add(text)  '树形第一层节点
                treeview.Nodes(0).ImageIndex = 0   '第一层的图标
                treeview.Nodes(0).SelectedImageIndex = 0   '
            Else
                If node_level = 1 Then   '第二层节点
                    treeview.Nodes(0).Nodes.Add(text)  '树形第二层节点
                    treeview.Nodes(0).Nodes(level1_num).ImageIndex = 1   '第二层的图标
                    treeview.Nodes(0).Nodes(level1_num).SelectedImageIndex = 1
                Else  '第三层节点
                    treeview.Nodes(0).Nodes(node_level_num).Nodes.Add(text)  '树形第三层节点
                    treeview.Nodes(0).Nodes(node_level_num).Nodes(level2_num).ImageIndex = imagekey  '第二层的图标
                    treeview.Nodes(0).Nodes(node_level_num).Nodes(level2_num).SelectedImageIndex = imagekey
                End If
            End If

        End If
    End Sub

    ''' <summary>
    ''' 设置窗口底部状态栏的提示
    ''' </summary>
    ''' <param name="text">提示字符串</param>
    ''' <param name="label_obj">状态栏</param>
    ''' <param name="name">状态栏的对象名</param>
    ''' <remarks></remarks>

    Public Sub SetTextLabelDelegate(ByVal text As String, ByVal label_obj As StatusStrip, ByVal name As String)
        If label_obj.InvokeRequired Then
            Dim setLabelText As SetStatusStripText = New SetStatusStripText(AddressOf SetTextLabelDelegate)
            Me.Invoke(setLabelText, New Object() {text, label_obj, name})
        Else
            label_obj.Items(name).Text = text   '设置状态栏中的文字

        End If
    End Sub

    ''' <summary>
    ''' 设置地图的图片框
    ''' </summary>
    ''' <param name="picbox">图片框名</param>
    ''' <param name="map">地图图片</param>
    ''' <param name="add">add=true表示增加，add=false表示删除</param>
    ''' <remarks></remarks>
    Public Sub SetMapDelegate(ByVal picbox As Windows.Forms.PictureBox, ByVal map As System.Drawing.Bitmap, ByVal add As Boolean)
        If picbox.InvokeRequired Then
            Dim setbg As SetMap = New SetMap(AddressOf SetMapDelegate)
            Me.Invoke(setbg, New Object() {picbox, map, add})
        Else
            If add = True Then
                picbox.Image = map '增加图片
            Else
                picbox.Image.Dispose()   '删除图片
            End If
        End If
    End Sub

    ''' <summary>
    ''' 设置图片框的可见性
    ''' </summary>
    ''' <param name="picbox">图片框名称</param>
    ''' <param name="visual">visual=true表示可见，visual=false表示不可见</param>
    ''' <remarks></remarks>
    Public Sub SetVisualDelegate(ByVal picbox As Windows.Forms.PictureBox, ByVal visual As Boolean)
        If picbox.InvokeRequired Then
            Dim Setvis As SetVisual = New SetVisual(AddressOf SetVisualDelegate)
            Me.Invoke(Setvis, New Object() {picbox, visual})

        Else
            If visual = True Then
                picbox.Visible = True   '图片框可见
            Else
                picbox.Visible = False    '图片框不可见
            End If
        End If
    End Sub

    ''' <summary>
    ''' 设置颜色提示图片框的颜色
    ''' </summary>
    ''' <param name="picbox">图片框的名称</param>
    ''' <param name="color">背景颜色</param>
    ''' <remarks></remarks>
    Public Sub SetColorDelegate(ByVal picbox As Windows.Forms.PictureBox, ByVal color As System.Drawing.Color)

        If picbox.InvokeRequired Then
            Dim SetCol As SetColor = New SetColor(AddressOf SetColorDelegate)
            Me.Invoke(SetCol, New Object() {picbox, color})

        Else
            picbox.BackColor = color   '设置图片框的背景颜色
        End If
    End Sub

    ''' <summary>
    ''' 设置文本框的内容
    ''' </summary>
    ''' <param name="text">内容字符串</param>
    ''' <param name="appendOrSet">appendOrSet=true 表示增加，appendOrSet=false表示替换</param>
    ''' <param name="control">控件对象</param>
    ''' <remarks></remarks>
    Public Sub SetTextDelegate(ByVal text As String, ByVal appendOrSet As Boolean, ByVal control As Control)
        '如果调用方位于创建该控件线程以外的线程时，需要获取是否要调用Invoke方法
        If (control.InvokeRequired) Then
            Dim SetText As SetTextControlCallBack = New SetTextControlCallBack(AddressOf SetTextDelegate)
            Me.Invoke(SetText, New Object() {text, appendOrSet, control})
        Else '如果不是线程外的调用时：设置文本框的值
            If control.Text.Length > 5000 Then
                control.Text = ""
            End If
            If (appendOrSet = True) Then
                ' control.Text &= text   '增加文本框的内容
                If control.Name = Me.rtb_info_list.Name Then  '状态统计文本框
                    rtb_info_list.AppendText(text)  '增加文本框的内容
                    rtb_info_list.Select(rtb_info_list.Text.Length, 0)
                    rtb_info_list.ScrollToCaret()
                Else
                    control.Text &= text   '增加文本框的内容
                End If
            Else
                If control.Name = Me.rtb_info_list.Name Then  '状态统计文本框
                    rtb_info_list.SelectionStart = rtb_info_list.TextLength
                    rtb_info_list.SelectionColor = Color.Red     ' 红色的显示是故障
                    rtb_info_list.AppendText(text) '增加文本框的内容
                Else
                    control.Text = text   '替换文本框的内容
                End If
            End If
            control.Refresh()   '刷新
        End If
    End Sub
#End Region


#Region "全局变量初始化"

    ''' <summary>
    ''' 全局变量的初始化
    ''' </summary>
    ''' <remarks></remarks>

    Private Sub Init_value()
        Com_inf.Read_file() '读取状态颜色文件
        pb_full_pic.BackColor = g_fullcolor  '开灯的颜色
        pb_close_pic.BackColor = g_closecolor  '关灯的颜色
        pb_no_return_pic.BackColor = g_noreturncolor  '无返回值的颜色
        pb_problem_pic.BackColor = g_problemcolor '故障的颜色
        g_mapsizevalue = pb_map.Size '地图的尺寸
        g_addstreettag = 0 '增加查询街道
        m_controllampobj.Get_condition()  '获取控制条件及条件值,及开灯关灯的阈值
        m_divtimestart = 1  '时段控制开始
        '记录地图的初始中心点坐标
        g_midpoint.X = pb_map.Location.X + pb_map.Width / 2   '中点的X坐标
        g_midpoint.Y = pb_map.Location.Y + pb_map.Height / 2   '中点的Y坐标
        g_lampmap = Graphics.FromImage(m_lamp)  '载入灯的图片
        'm_waittime = 15  '检测通信是否正常时等待时间
        m_checkcondition = 0  '初始化时查询所有
        m_checkvalue = ""  '右侧终端状态列表查询的值
        m_alarmopenorclose = True   '初始化打开报警声音
        m_jiankongopenorclose = False  '初始化关闭监控面板
        m_gongnengopenorclose = False   '初始化关闭功能栏
        m_tongjiopenorclose = False   '初始化关闭统计
        m_index = 0  '初始化当前选中的行号为0（终端）防止频闪
        m_indexbox = 0 '初始化当前选中的行号为0（主控箱）防止频闪
        m_zhishitime = 0 '指示初始化为0
        g_changemapvalue = MAP_MID_SIZE  '地图的大小
        init_div_list() '初始化模式名称g_divname，统计所有的时段名称
        init_special_div_list() '初始化特殊模式g_specialdivname，统计所有的特殊时段名称
        m_loadwaittime = 0  '每十分钟统计一次回路数据
        g_addboxtag = 0  '绘制电控箱时取坐标的标志
        m_controlboxnamestring = ""  '电控箱名称
        m_controlboxobj.Get_TopBottom_value()  '设置的电流电压的上下限值
        setControlMod()  '对时段控制模式的全局 变量参数进行初始化设置

        get_waittime()  '设置招测等待时间

        get_lightvalue_fun() '设置光照阈值

        get_chaobiaoconfig()  '电表的设置

        get_lamptime()  '自动招测单灯数据的时间
    End Sub

    Public Sub get_lamptime()

        Try
            Dim rs As New ADODB.Recordset
            Dim conn As New ADODB.Connection
            Dim msg As String
            Dim sql As String

            msg = ""
            '设置光照阈值
            sql = "select * from sysconfig where type='区间时间'"
            DBOperation.OpenConn(conn)
            rs = DBOperation.SelectSQL(conn, sql, msg)
            If rs Is Nothing Then
                MsgBox(MSG_ERROR_STRING & "set_lamp_Click", , PROJECT_TITLE_STRING)
                conn.Close()
                conn = Nothing
                Exit Sub
            End If
            If rs.RecordCount > 0 Then
                g_getlamp_time = Val(Trim(rs.Fields("name").Value))
            Else
                g_getlamp_time = 20
            End If
            If rs.State = 1 Then
                rs.Close()
                rs = Nothing
            End If
            conn.Close()
            conn = Nothing
        Catch ex As Exception
            MsgBox(ex.Message, , PROJECT_TITLE_STRING)
        End Try
    End Sub

    ''' <summary>
    ''' 设置光照阈值
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub get_lightvalue_fun()
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String = ""
        Dim sql As String

        '光照阈值的设置
        Dim type_string As String = "光照阈值"
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        sql = "select * from sysconfig where type='" & type_string & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            conn.Close()
            conn = Nothing
        End If
        If rs.RecordCount > 0 Then
            g_lightvalueset = Val(Trim(rs.Fields("name").Value))
        End If
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing

    End Sub

    ''' <summary>
    ''' 获取招测延时及等待时间
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub get_waittime()
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String = ""
        Dim sql As String
        '召测等待时间
        Dim type_string As String = "召测等待时间"
        g_ycwaittime = 5  '默认等待5秒
        If DBOperation.OpenConn(conn) = False Then
            Exit Sub
        End If
        sql = "select * from sysconfig where type='" & type_string & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            conn.Close()
            conn = Nothing
        End If
        If rs.RecordCount > 0 Then
            g_ycwaittime = Val(Trim(rs.Fields("name").Value))
        End If

        '召测间隔时间
        type_string = "召测间隔时间"
        g_ycjgtime = 2  '默认等待2分钟

        sql = "select * from sysconfig where type='" & type_string & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            conn.Close()
            conn = Nothing
        End If
        If rs.RecordCount > 0 Then
            g_ycjgtime = Val(Trim(rs.Fields("name").Value))
        End If
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing

    End Sub
    ''' <summary>
    ''' 对时段控制模式的全局变量参数进行初始化设置
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setControlMod()
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String
        Dim type_string As String
        type_string = "级别"
        msg = ""
        sql = "select name from sysconfig where type='" & type_string & "'"
        DBOperation.OpenConn(conn)
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "setControlMod", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            g_sysjibiecontrol = ""
            Exit Sub
        End If
        If rs.RecordCount > 0 Then
            g_sysjibiecontrol = Trim(rs.Fields("name").Value)
        Else
            g_sysjibiecontrol = ""
        End If
        type_string = "组合"
        sql = "select name from sysconfig where type='" & type_string & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "setControlMod", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            g_modgroup(0) = ""
            Exit Sub
        End If
        If rs.RecordCount > 0 Then
            g_modgroup = Trim(rs.Fields("name").Value).Split(" ")
        Else
            g_modgroup(0) = ""
        End If
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub
    ''' <summary>
    ''' 载入地图和滚动条尺寸
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub load_map()
        Dim map_path, smallmap_path As String   '全景地图和鹰眼地图的路径
        'map_size.Maximum = MAP_MAX_SIZE
        'map_size.Value = MAP_MID_SIZE
        'g_changemapvalue = MAP_MID_SIZE
        get_map_id()  ' 获取初始化地图
        map_path = "map\" & g_mapname & ".jpg"
        smallmap_path = "map\s" & g_mapname & ".jpg"
        If g_mapname <> "" Then
            pb_map.BackgroundImage = System.Drawing.Image.FromFile(map_path) '载入地图
            pb_small_map.BackgroundImage = System.Drawing.Image.FromFile(smallmap_path) '载入鹰眼地图
            pb_map.Width = System.Drawing.Image.FromFile(map_path).Width   '地图的宽度
            pb_map.Height = System.Drawing.Image.FromFile(map_path).Height  '地图的高度
            m_lamp = New Bitmap(pb_map.Width, pb_map.Height)
        End If
        Me.map_area.Text = "地图区域：" & g_mapname

    End Sub

    '初始化软件启动时的地图
    Private Sub get_map_id()
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim sql As String
        Dim msg As String

        msg = ""
        sql = ""
        DBOperation.OpenConn(conn)
        sql = "select top (1) * from map_list order by id"
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then   '地图列表中没有增加地图
            MsgBox(MSG_ERROR_STRING & "Get_map_id", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        If rs.RecordCount > 0 Then
            g_choosemapid = rs.Fields("id").Value   '地图的编号
            g_mapname = Trim(rs.Fields("map_name").Value)  '地图的名称
        Else
            g_choosemapid = 0
            g_mapname = ""
        End If

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub



    Public Sub init_special_div_list()
        '初始化模式名称
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection

        Dim msg As String
        Dim sql As String
        Dim i As Integer = 0
        DBOperation.OpenConn(conn)
        msg = ""
        sql = "select distinct(name) from special_div_time"  '时段控制的层
        '清空树形表
        ' LoginForm.Property_welcome_win_obj.Div_infList.Nodes.Clear()
        'DBOperation.OpenConn(conn)
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "init_special_div_list", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        ReDim g_specialdivname(rs.RecordCount - 1)
        While rs.EOF = False

            g_specialdivname(i) = Trim(rs.Fields("name").Value)
            i += 1
            rs.MoveNext()
        End While
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub
    Public Sub init_div_list()
        '初始化模式名称
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String
        Dim i As Integer = 0
        DBOperation.OpenConn(conn)
        msg = ""
        sql = "select distinct(div_level) from div_time"  '时段控制的层
        '清空树形表
        g_welcomewinobj.Div_infList.Nodes.Clear()
        'DBOperation.OpenConn(conn)
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "init_div_list", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        ReDim g_divname(rs.RecordCount - 1)
        While rs.EOF = False
            g_divname(i) = Trim(rs.Fields("div_level").Value)
            i += 1
            rs.MoveNext()
        End While
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub
#End Region

#Region "隐藏不需要可见的控件"

    ''' <summary>
    ''' 隐藏不需要可见的控件
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub set_visual_false()
        type_id.Visible = False  '灯的类型编号
        '手工整体控制
        power.Visible = False   '功率
        city_all.Visible = False  '城市名称
        area_all.Visible = False  '区域名称
        street_all.Visible = False  '街道名称
        lamp_id_all_start.Visible = False  '灯的编号的前半部分（电控箱编号及类型编号）
        lamp_type_id.Visible = False
        single_all_close.Visible = False   '单号灯关
        double_all_close.Visible = False  '双号灯关
        close_1_3.Visible = False   '1/3号灯关
        ToolStripProgressBar_check_communication.Visible = False   '检测通信是否正常的进度条
        SplitContainer1.Panel2Collapsed = True '系统登录的初始化阶段，先隐藏右边的三个面板
    End Sub

#End Region

End Class
