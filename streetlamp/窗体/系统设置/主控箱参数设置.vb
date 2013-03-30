Public Class 主控箱参数设置
    Private m_currenttopvalue As Integer   '电流上限
    Private m_currentbottomvalue As Integer  '电流下限
    Private m_presuretopvalue As Integer  '电压上限
    Private m_presurebottomvalue As Integer   '电压下限
    Private m_bianbivalue As Integer  '变比参数

    ''' <summary>
    ''' 载入窗体
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 主控箱参数设置_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = New Icon("图片\favicon.ico", 32, 32)
        Try
            Dim Read1 As New System.IO.StreamReader("box_config.txt", System.Text.Encoding.UTF8)
            m_currenttopvalue = Read1.ReadLine
            current_top.Text = m_currenttopvalue
            m_currentbottomvalue = Read1.ReadLine
            current_bottom.Text = m_currentbottomvalue
            m_presuretopvalue = Read1.ReadLine
            presure_top.Text = m_presuretopvalue
            m_presurebottomvalue = Read1.ReadLine
            presure_bottom.Text = m_presurebottomvalue
            m_bianbivalue = Read1.ReadLine
            bianbip.Text = m_bianbivalue
            Read1.Close()
        Catch ex As Exception
            MsgBox(ex.Message, , PROJECT_TITLE_STRING)

        End Try
    End Sub

    ''' <summary>
    ''' 设置电流上下限
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub set_current_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles set_current.Click
        If current_top.Text = "" Then
            MsgBox("电流上限不可以为空", , PROJECT_TITLE_STRING)
            current_top.Focus()
            Exit Sub
        End If
        If current_bottom.Text = "" Then
            MsgBox("电流下限不可以为空", , PROJECT_TITLE_STRING)
            current_bottom.Focus()
            Exit Sub
        End If

        m_currenttopvalue = Val(current_top.Text)
        m_currentbottomvalue = Val(current_bottom.Text)

        Try
            Dim write As New System.IO.StreamWriter("box_config.txt")  '打开control_condition文本框
            write.WriteLine(m_currenttopvalue)
            write.WriteLine(m_currentbottomvalue)
            write.WriteLine(m_presuretopvalue)
            write.WriteLine(m_presurebottomvalue)
            write.WriteLine(m_bianbivalue)
            write.Close()

            MsgBox("控制参数编辑成功！", , PROJECT_TITLE_STRING)

        Catch ex As Exception
            MsgBox(ex.Message, , PROJECT_TITLE_STRING)

        End Try

        '重新设置全局变量
        g_welcomewinobj.m_controlboxobj.Get_TopBottom_value()
    End Sub

    ''' <summary>
    ''' 设置电压上下限
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub set_presure_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles set_presure.Click
        If presure_top.Text = "" Then
            MsgBox("电压上限不可以为空", , PROJECT_TITLE_STRING)
            presure_top.Focus()
            Exit Sub
        End If
        If presure_bottom.Text = "" Then
            MsgBox("电压下限不可以为空", , PROJECT_TITLE_STRING)
            presure_bottom.Focus()
            Exit Sub
        End If

        m_presuretopvalue = Val(presure_top.Text)
        m_presurebottomvalue = Val(presure_bottom.Text)

        Try
            Dim write As New System.IO.StreamWriter("box_config.txt")  '打开control_condition文本框
            write.WriteLine(m_currenttopvalue)
            write.WriteLine(m_currentbottomvalue)
            write.WriteLine(m_presuretopvalue)
            write.WriteLine(m_presurebottomvalue)
            write.WriteLine(m_bianbivalue)
            write.Close()

            MsgBox("控制参数编辑成功！", , PROJECT_TITLE_STRING)

        Catch ex As Exception
            MsgBox(ex.Message, , PROJECT_TITLE_STRING)

        End Try
        '重新设置全局变量
        g_welcomewinobj.m_controlboxobj.Get_TopBottom_value()
    End Sub

    ''' <summary>
    ''' 设置变比参数
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub set_bianbi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles set_bianbi.Click
        If bianbip.Text = "" Then
            MsgBox("变比参数不可为空", , PROJECT_TITLE_STRING)
            bianbip.Focus()
            Exit Sub
        End If

        m_bianbivalue = Val(bianbip.Text)  '变比参数设置


        Try
            Dim write As New System.IO.StreamWriter("box_config.txt")  '打开control_condition文本框
            write.WriteLine(m_currenttopvalue)
            write.WriteLine(m_currentbottomvalue)
            write.WriteLine(m_presuretopvalue)
            write.WriteLine(m_presurebottomvalue)
            write.WriteLine(m_bianbivalue)

            write.Close()

            MsgBox("控制参数编辑成功！", , PROJECT_TITLE_STRING)

        Catch ex As Exception
            MsgBox(ex.Message, , PROJECT_TITLE_STRING)

        End Try
        '重新设置全局变量
        g_welcomewinobj.m_controlboxobj.Get_TopBottom_value()
    End Sub
End Class