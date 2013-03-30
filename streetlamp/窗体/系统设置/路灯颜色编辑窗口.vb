''' <summary>
''' 编辑表示路灯各种状态的颜色值，记录得到lamp_color.txt文件中
''' </summary>
''' <remarks></remarks>
Public Class 路灯颜色编辑窗口

    ''' <summary>
    ''' 双击打开状态颜色框
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub full_color_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pb_full_color.MouseDoubleClick
        ColorDialog1.ShowDialog()  '打开颜色编辑窗口
        pb_full_color.BackColor = ColorDialog1.Color  '设置全功率颜色
        pb_full_color.Refresh()
    End Sub

    ''' <summary>
    '''  双击故障状态颜色框
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub problem_color_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pb_problem_color.MouseDoubleClick
        ColorDialog1.ShowDialog()  '打开颜色编辑窗口
        pb_problem_color.BackColor = ColorDialog1.Color '设置故障灯颜色
        pb_problem_color.Refresh()
    End Sub

    ''' <summary>
    ''' 编辑四类状态的颜色，写入lamp_color.txt
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_edit_color_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_edit_color.Click
        Try
            Dim write As New System.IO.StreamWriter("lamp_color.txt")  '将设置的系统默认颜色写入文件中
            write.WriteLine(Trim(pb_full_color.BackColor.ToArgb))  '1
            write.WriteLine(Trim(pb_par_color.BackColor.ToArgb))  '4
            write.WriteLine(Trim(pb_part_color.BackColor.ToArgb))  '4
            write.WriteLine(Trim(pb_close_color.BackColor.ToArgb))  '1
            write.WriteLine(Trim(pb_problem_color.BackColor.ToArgb))  '4
            write.Close()
            '刷新窗口中的路灯颜色信息
            g_fullcolor = pb_full_color.BackColor  '主控界面中的全功率颜色变量
            g_problemcolor = pb_problem_color.BackColor  '主控界面中的故障颜色变量
            g_closecolor = pb_close_color.BackColor  '主控界面中的关闭灯颜色变量
            g_noreturncolor = pb_part_color.BackColor  '主控界面中的无返回值变量
            g_partcolor = pb_par_color.BackColor  '主控界面中的半功率变量

            g_welcomewinobj.pb_full_pic.BackColor = g_fullcolor
            g_welcomewinobj.pb_close_pic.BackColor = g_closecolor
            g_welcomewinobj.pb_no_return_pic.BackColor = g_noreturncolor
            g_welcomewinobj.pb_problem_pic.BackColor = g_problemcolor

            MsgBox("颜色编辑成功！", , PROJECT_TITLE_STRING)

            '增加操作记录
            Com_inf.Insert_Operation("编辑路灯状态颜色")

        Catch ex As Exception
            MsgBox(ex.Message, , PROJECT_TITLE_STRING)
        End Try

        Me.Close()

    End Sub

    ''' <summary>
    ''' 窗体载入函数
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 路灯颜色编辑窗口_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '设置各个窗体的图标
        Me.Icon = New Icon("图片\favicon.ico", 32, 32)

        Com_inf.Read_file()  '读取系统默认的路灯标识颜色

        pb_full_color.BackColor = g_fullcolor  '全功率路灯标识颜色
        pb_problem_color.BackColor = g_problemcolor '故障灯标志颜色
        pb_close_color.BackColor = g_closecolor  '关闭路灯标识颜色
        pb_part_color.BackColor = g_noreturncolor '无返回值标志颜色
        pb_par_color.BackColor = g_partcolor '半功率标志颜色

    End Sub

    ''' <summary>
    '''  双击关闭状态颜色框
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub close_color_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pb_close_color.DoubleClick
        ColorDialog1.ShowDialog()  '打开颜色编辑窗口
        pb_close_color.BackColor = ColorDialog1.Color  '设置关闭灯颜色
        pb_close_color.Refresh()
    End Sub

    ''' <summary>
    '''  双击无返回值状态颜色框
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub part_color_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pb_part_color.DoubleClick
        ColorDialog1.ShowDialog()  '打开颜色编辑窗口
        pb_part_color.BackColor = ColorDialog1.Color  '设置无返回值颜色颜色
        pb_part_color.Refresh()
    End Sub

    ''' <summary>
    '''  双击半功率状态颜色框
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub par_color_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pb_par_color.MouseDoubleClick
        ColorDialog1.ShowDialog()  '打开颜色编辑窗口
        pb_par_color.BackColor = ColorDialog1.Color  '设置半功率颜色颜色
        pb_par_color.Refresh()
    End Sub

    Private Sub 路灯颜色编辑窗口_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        g_windowclose = 1
    End Sub
End Class