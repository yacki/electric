''' <summary>
''' 设定四个级别，三个时段的时段划分，可编辑，增加，删除
''' </summary>
''' <remarks></remarks>
''' 
Public Class 时段划分

    Private div_mod_level As Integer '1编辑模式,0表示添加模式，2表示删除
    Private lv As String = "100"
    Private diangan As String

    ''' <summary>
    ''' '1编辑模式,0表示添加模式，2表示删除，div_mod_level的属性方法
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Property_div_mod_level() As Integer
        Get
            Return div_mod_level
        End Get
        Set(ByVal value As Integer)
            div_mod_level = value
        End Set
    End Property

    ''' <summary>
    ''' 将三个时段中的各个文本框赋值
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub clear_div_table()
        Dim rs As ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim conn As New ADODB.Connection
        DBOperation.OpenConn(conn)


        msg = ""
        sql = ""

        sql = "select * from div_time where div_level='" & Trim(mod_level_choose.Text) & "'"
        rs = DBOperation.SelectSQL(conn, sql, msg)  '根据时段划分的级别，填充相关的变量框

        While rs.EOF = False
            If rs.Fields("id").Value = 1 Then  '时段中的第一时间区间
                hour1_beg.Text = Trim(rs.Fields("hour_beg").Value)  '时间段开始时间的小时
                min1_beg.Text = Trim(rs.Fields("min_beg").Value) '时间段开始时间的分钟
                second1_beg.Text = Trim(rs.Fields("second_beg").Value) '时间段开始的秒
               
                mod1.Text = Mid(Trim(rs.Fields("mod").Value), 3)  '控制模式
                'lv1.Text = Trim(rs.Fields("gonglv").Value)  '时段控制的功率
                gonglv1.Text = Trim(rs.Fields("diangan").Value) '时段控制的电感路灯控制方式

            Else
                If rs.Fields("id").Value = 2 Then  '时段中的第二时间区间
                    hour2_beg.Text = Trim(rs.Fields("hour_beg").Value)  '时间段开始时间的小时
                    min2_beg.Text = Trim(rs.Fields("min_beg").Value)  '时段开始时间的分钟
                    second2_beg.Text = Trim(rs.Fields("second_beg").Value) '时段开始的秒
                    mod2.Text = Mid(Trim(rs.Fields("mod").Value), 3) '控制模式
                    'lv2.Text = Trim(rs.Fields("gonglv").Value) '时段控制的功率
                    gonglv2.Text = Trim(rs.Fields("diangan").Value) '时段控制的电感路灯控制方式

                Else
                    If rs.Fields("id").Value = 3 Then  '时段中的第三时间区间
                        hour3_beg.Text = Trim(rs.Fields("hour_beg").Value) '时间段开始时间的小时
                        min3_beg.Text = Trim(rs.Fields("min_beg").Value) '时间段开始时间的分钟
                        second3_beg.Text = Trim(rs.Fields("second_beg").Value) '时间段开始时间的秒
                      
                        'lv3.Text = Trim(rs.Fields("gonglv").Value) '时段控制的功率
                        gonglv3.Text = Trim(rs.Fields("diangan").Value) '时段控制的电感路灯控制方式


                    End If
                End If

            End If
            rs.MoveNext()
        End While

        mod_level_choose.Text = ""
        conn.Close()
        conn = Nothing
    End Sub
    ''' <summary>
    ''' 对时段划分的操作函数，增加，编辑，删除
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub input_hms_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles input_hms.Click
        Dim rs As ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim div As New div_time_class
        Dim div_time_obj As New div_time_class
        Dim conn As New ADODB.Connection
        Dim mod_level As String 'A,B,C,D四个级别
        DBOperation.OpenConn(conn)

        msg = ""
        sql = ""
        mod_level = ""

        '将模式名称转换由中文名称转换成A,B,C,D四个级别
        If Me.mod_level_choose.Text = "平时模式" Then
            mod_level = "A"
        End If
        If Me.mod_level_choose.Text = "节日模式" Then
            mod_level = "B"
        End If
        If Me.mod_level_choose.Text = "半夜模式" Then
            mod_level = "C"
        End If
        If Me.mod_level_choose.Text = "全关模式" Then
            mod_level = "D"
        End If

        If div_mod_level = 1 Then  '编辑时段划分
            sql = "select * from div_time where div_level='" & mod_level & "'"
        Else
            If div_mod_level = 0 Then  '添加时段划分
                If mod_level < "A" Or mod_level > "D" Then '模式级别限定在Ａ．Ｂ．Ｃ．Ｄ四个级别
                    MsgBox("请选择正确的控制模式！", , PROJECT_TITLE_STRING)
                    mod_level_choose.Focus()  '光标定位在模式级别文本框
                    conn.Close()
                    conn = Nothing
                    Exit Sub
                End If
                sql = "select * from div_time where div_level='" & mod_level & "'"
            Else  '删除时段划分
                If mod_level < "A" Or mod_level > "D" Then '模式级别限定在Ａ，Ｂ，Ｃ，Ｄ四个级别
                    MsgBox("请选择正确的控制模式！", , PROJECT_TITLE_STRING)
                    mod_level_choose.Focus() '光标定位在模式级别文本框
                    conn.Close()
                    conn = Nothing
                    Exit Sub
                End If
                '询问是否删除该级别的时段划分
                If MsgBox("是否删除" & Me.mod_level_choose.Text & "的时段控制", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.Yes Then
                    sql = "delete from div_time where div_level='" & mod_level & "'"
                    DBOperation.ExecuteSQL(conn, sql, msg)  '删除
                    div_time_obj.Div_time_show()  '删除后刷新主控界面中的时段treelist和时段信息
                    MsgBox("删除成功", , PROJECT_TITLE_STRING)
                    conn.Close()
                    conn = Nothing
                    Me.Close()
                    Exit Sub
                Else
                    conn.Close()
                    conn = Nothing
                    Exit Sub

                End If

            End If

        End If

        Dim time_string1, time_string2, time_string3 As String
        time_string1 = hour1_beg.Text & min1_beg.Text & second1_beg.Text
        time_string2 = hour2_beg.Text & min2_beg.Text & second2_beg.Text
        time_string3 = hour3_beg.Text & min3_beg.Text & second3_beg.Text
        ' time_string2 = hour1_end.Text & min1_end.Text & second1_end.Text
        If time_string1 >= time_string2 Or time_string2 > time_string3 Or time_string1 >= time_string3 Then  '时间段1中，开始时间在结束时间之后
            MsgBox("时段输入错误，请重新输入！", , PROJECT_TITLE_STRING)
            hour1_beg.Focus() '光标定位在时段1开始时间的小时文本框
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
      

        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs.RecordCount = 0 Then
            '增加的新时段划分
            rs.AddNew()
            rs.Fields("hour_beg").Value = Trim(hour1_beg.Text)  '时段1开始时间小时
            rs.Fields("min_beg").Value = Trim(min1_beg.Text) '时段1开始时间分钟
            rs.Fields("second_beg").Value = Trim(second1_beg.Text) '时段1开始时间秒
           
            rs.Fields("mod").Value = "类型" & Trim(mod1.Text) '时段1控制模式
            rs.Fields("gonglv").Value = lv '时段1控制功率
            rs.Fields("diangan").Value = Trim(gonglv1.Text) '时段1电感路灯控制方法
            rs.Fields("id").Value = 1 '时段1标号
            rs.Fields("div_level").Value = mod_level '时段1的级别

            rs.Update()

            rs.AddNew()
            rs.Fields("hour_beg").Value = Trim(hour2_beg.Text)  '时段2开始时间小时
            rs.Fields("min_beg").Value = Trim(min2_beg.Text) '时段2开始时间分钟
            rs.Fields("second_beg").Value = Trim(second2_beg.Text) '时段2开始时间秒
            rs.Fields("mod").Value = "类型" & Trim(mod2.Text) '时段2控制模式
            rs.Fields("gonglv").Value = lv '时段2控制功率
            rs.Fields("diangan").Value = Trim(gonglv2.Text) '时段2电感路灯控制方法
            rs.Fields("id").Value = 2 '时段2标号
            rs.Fields("div_level").Value = mod_level '时段2的级别
            rs.Update()

            rs.AddNew()
            rs.Fields("hour_beg").Value = Trim(hour3_beg.Text) '时段3开始时间小时
            rs.Fields("min_beg").Value = Trim(min3_beg.Text) '时段3开始时间分钟
            rs.Fields("second_beg").Value = Trim(second3_beg.Text) '时段3开始时间秒
          
            rs.Fields("mod").Value = "类型" & Trim(mod3.Text) '时段3控制模式
            rs.Fields("gonglv").Value = lv '时段3控制功率
            rs.Fields("diangan").Value = Trim(gonglv3.Text) '时段3电感路灯控制方法
            rs.Fields("id").Value = 3 '时段3标号

            rs.Fields("div_level").Value = mod_level '时段3的级别

            rs.Update()
        Else
            '编辑模式
            If div_mod_level = 0 Then
                If MsgBox("该控制模式已经存在，是否替换？", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.No Then
                    conn.Close()
                    conn = Nothing
                    Exit Sub
                End If
            End If
            While rs.EOF = False
                If rs.Fields("id").Value = 1 Then '时段1
                    rs.Fields("hour_beg").Value = Trim(hour1_beg.Text) '时段1开始时间小时
                    rs.Fields("min_beg").Value = Trim(min1_beg.Text) '时段1开始时间分钟
                    rs.Fields("second_beg").Value = Trim(second1_beg.Text) '时段1开始时间秒
                    rs.Fields("mod").Value = "类型" & Trim(mod1.Text) '时段1控制模式
                    rs.Fields("gonglv").Value = lv '时段1控制功率
                    rs.Fields("diangan").Value = Trim(gonglv1.Text)

                Else
                    If rs.Fields("id").Value = 2 Then
                        rs.Fields("hour_beg").Value = Trim(hour2_beg.Text) '时段2开始时间小时
                        rs.Fields("min_beg").Value = Trim(min2_beg.Text) '时段2开始时间分钟
                        rs.Fields("second_beg").Value = Trim(second2_beg.Text) '时段2开始时间秒
                        rs.Fields("mod").Value = "类型" & Trim(mod2.Text) '时段2控制模式
                        rs.Fields("gonglv").Value = lv '时段2控制功率
                        rs.Fields("diangan").Value = Trim(gonglv2.Text)
                    Else
                        rs.Fields("hour_beg").Value = Trim(hour3_beg.Text) '时段3开始时间小时
                        rs.Fields("min_beg").Value = Trim(min3_beg.Text) '时段3开始时间分钟
                        rs.Fields("second_beg").Value = Trim(second3_beg.Text) '时段3开始时间秒
                        rs.Fields("mod").Value = "类型" & Trim(mod3.Text)  '时段3控制模式
                        rs.Fields("gonglv").Value = lv '时段3控制功率
                        rs.Fields("diangan").Value = Trim(gonglv3.Text)
                    End If

                    rs.Update()
                End If

                rs.MoveNext()

            End While
            rs.Close()
            rs = Nothing

        End If

        div_time_obj.Div_time_show()  '刷新主控界面中的时段treelist和时段信息

        If div_mod_level = 0 Then
            MsgBox(Trim(Me.mod_level_choose.Text) & "添加成功!", , PROJECT_TITLE_STRING)
        Else
            MsgBox(Trim(Me.mod_level_choose.Text) & "修改成功!", , PROJECT_TITLE_STRING)
            Me.Close()
        End If
        conn.Close()
        conn = Nothing
        'Me.Close()
    End Sub
    ''' <summary>
    ''' 根据不同的选择载入不同的窗口：编辑模式，添加模式，删除模式
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 时段控制窗口_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '设置各个窗体的图标
        Me.Icon = New Icon("图片\favicon.ico", 32, 32)
        If div_mod_level = 1 Then  '进行编辑操作
            input_hms.Text = "编辑"
        Else
            If div_mod_level = 0 Then  '进行添加操作
                input_hms.Text = "添加"
            Else  '进行删除操作
                input_hms.Text = "删除"
            End If
        End If
        edit_div_inf()
    End Sub
    ''' <summary>
    ''' 如果是编辑或删除操作，需要对窗体中的各个文本框进行初始化赋值
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub edit_div_inf()
        Dim rs As ADODB.Recordset
        Dim msg As String
        Dim sql As String
        Dim conn As New ADODB.Connection
        DBOperation.OpenConn(conn)
        Dim mod_level As String '模式级别
        mod_level = ""
        If Trim(mod_level_choose.Text = "平时模式") Then
            mod_level = "A"
        End If
        If Trim(mod_level_choose.Text = "节日模式") Then
            mod_level = "B"
        End If
        If Trim(mod_level_choose.Text = "半夜模式") Then
            mod_level = "C"
        End If
        If Trim(mod_level_choose.Text = "全关模式") Then
            mod_level = "D"
        End If


        msg = ""
        sql = ""
        If div_mod_level <> 0 Then  '编辑或删除操作

            sql = "select * from div_time where div_level='" & mod_level & "'"
        Else
            conn.Close()
            conn = Nothing
            Exit Sub
        End If

        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "edit_div_inf", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        Else
            If rs.RecordCount <= 0 Then  '如果数据库中没有时段划分信息
                hour1_beg.Text = "" '时段1开始小时
                min1_beg.Text = "" '时段1开始分钟
                second1_beg.Text = "" '时段1开始秒
                mod1.Text = "" '时段1控制模式
                'lv1.Text = "" '时段1功率
                gonglv1.Text = "" '时段1电感控制方式

                hour2_beg.Text = "" '时段2开始小时
                min2_beg.Text = ""  '时段2开始分钟
                second2_beg.Text = "" '时段2开始秒
                mod2.Text = "" '时段2控制模式
                'lv2.Text = ""  '时段2功率
                gonglv2.Text = "" '时段2电感控制方式

                hour3_beg.Text = "" '时段3开始小时
                min3_beg.Text = "" '时段3开始分钟
                second3_beg.Text = ""  '时段3开始秒
                mod3.Text = "" '时段3控制模式
                'lv3.Text = ""  '时段3功率
                gonglv3.Text = "" '时段3电感控制方式

            Else  '数据库中有时段划分的信息
                While rs.EOF = False
                    If rs.Fields("id").Value = 1 Then '时段1
                        hour1_beg.Text = Trim(rs.Fields("hour_beg").Value) '开始小时
                        min1_beg.Text = Trim(rs.Fields("min_beg").Value) '开始分钟
                        second1_beg.Text = Trim(rs.Fields("second_beg").Value) '开始秒
                        mod1.Text = Mid(Trim(rs.Fields("mod").Value), 3) '控制模式
                        'lv1.Text = Trim(rs.Fields("gonglv").Value) '控制功率
                        gonglv1.Text = Trim(rs.Fields("diangan").Value) '电感控制方式
                    Else
                        If rs.Fields("id").Value = 2 Then '时段2
                            hour2_beg.Text = Trim(rs.Fields("hour_beg").Value) '开始小时
                            min2_beg.Text = Trim(rs.Fields("min_beg").Value) '开始分钟
                            second2_beg.Text = Trim(rs.Fields("second_beg").Value) '开始秒
                            mod2.Text = Mid(Trim(rs.Fields("mod").Value), 3) '控制模式
                            'lv2.Text = Trim(rs.Fields("gonglv").Value) '控制功率
                            gonglv2.Text = Trim(rs.Fields("diangan").Value) '电感控制方式
                        Else
                            If rs.Fields("id").Value = 3 Then '时段3
                                hour3_beg.Text = Trim(rs.Fields("hour_beg").Value) '开始时间
                                min3_beg.Text = Trim(rs.Fields("min_beg").Value) '开始分钟
                                second3_beg.Text = Trim(rs.Fields("second_beg").Value) '开始秒
                                mod3.Text = Mid(Trim(rs.Fields("mod").Value), 3) '控制模式
                                'lv3.Text = Trim(rs.Fields("gonglv").Value) '控制功率
                                gonglv3.Text = Trim(rs.Fields("diangan").Value) '电感控制方式
                            End If
                        End If

                    End If
                    rs.MoveNext()
                End While
                rs.Close()
                rs = Nothing
            End If
        End If
        conn.Close()
        conn = Nothing
    End Sub
    ''' <summary>
    ''' 编辑模式下，模式级别的改变，则改变文本框中的值
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mod_level_choose_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mod_level_choose.SelectedIndexChanged
        edit_div_inf()
    End Sub

   
End Class