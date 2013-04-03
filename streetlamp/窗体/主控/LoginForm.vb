''' <summary>
''' 用户登录
''' </summary>
''' <remarks></remarks>
Public Class LoginForm

    Private m_rst As ADODB.Recordset
    Private m_conn As ADODB.Connection
  
    ''' <summary>
    ''' 登陆函数，判断用户名和密码的合法性
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_ok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_ok.Click
        Dim SQL As String
        Dim Msg As String


        '判断输入的长度是否超过一定范围

        If tb_username.TextLength > 20 Then   '用户名的长度非法
            MsgBox("用户名的长度超过20,请重新输入", , PROJECT_TITLE_STRING)
            tb_username.Focus()
            Exit Sub
        End If

        If tb_password.TextLength > 20 Then  '密码的长度非法
            MsgBox("密码长度超过20，请重新输入", , PROJECT_TITLE_STRING)
            tb_password.Focus()
            Exit Sub
        End If

        Msg = ""
        m_conn = New ADODB.Connection
        DBOperation.OpenConn(m_conn)
        '发生错误时跳转
        On Error GoTo Error_Renamed
        '判断输入用户名是否为空
        If Trim(tb_username.Text & "") <> "" Then
            '查找数据库中是否有相同名称的用户

            SQL = "select * from manage where name='" & Trim(tb_username.Text & "") & "'"
            m_rst = DBOperation.SelectSQL(m_conn, SQL, Msg)

            '记录是否为空
            If m_rst.EOF = True Then
                MsgBox("没有这个用户，请重新输入！", MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation, PROJECT_TITLE_STRING)
                tb_username.Focus()
                m_rst.Close()
                m_rst = Nothing
                m_conn.Close()
                m_conn = Nothing
                Exit Sub
            Else '登录成功
                '判断用户密码是否
                If Trim(tb_password.Text & "") = Trim(m_rst.Fields("password").Value) Then
                    g_rightmanage = Trim(m_rst.Fields("rights").Value)  '岗位
                    g_username = Trim(m_rst.Fields("name").Value)   '用户名
                    g_password = Trim(m_rst.Fields("password").Value)   '密码
                    '计时进度条
                    pb_login.Visible = True
                    pb_login.Value = 0
                    pb_login.Maximum = 1000
                    pb_login.Minimum = 0
                    Timer1.Interval = 100
                    Timer1.Enabled = True
                    '

                Else
                    MsgBox("密码错误，请重新输入！", MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation, PROJECT_TITLE_STRING)
                    tb_password.Focus()
                    tb_password.SelectionStart = 0
                    tb_password.SelectionLength = Len(tb_password.Text)

                End If
            End If
        Else
            MsgBox("用户不可为空，请重新输入！", MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation, PROJECT_TITLE_STRING)
            tb_username.Focus()
        End If

        Exit Sub

Error_Renamed:
        End

    End Sub

    ''' <summary>
    ''' 取消登陆
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_cancel.Click

        Me.Close()

    End Sub

    ''' <summary>
    ''' 登陆界面的load函数
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub LoginForm1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'right_manage = ""
        ' se = New Sunisoft.IrisSkin.SkinEngine()  '窗体皮肤
        'se.SkinAllForm = True
        '设置登录窗体的图标
        Me.Icon = New Icon("图片\favicon.ico", 50, 50)
        Com_inf.InitSystemConfig()
        Me.Text = PROJECT_TITLE_STRING
        Com_inf.Read_file()
        'Me.MdiParent = Me
    End Sub

    ''' <summary>
    ''' 控制登陆进度条的时钟
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        pb_login.Value = pb_login.Value + pb_login.Maximum / 10
        If pb_login.Value = pb_login.Maximum Then
            Timer1.Enabled = False
            '加载入终端状态颜色

            g_welcomewinobj.Show()

            m_rst.Close()
            m_rst = Nothing
            m_conn.Close()
            m_conn = Nothing
            pb_login.Visible = False

            'Me.Hide()
            Me.Close()
        End If

    End Sub

    ''' <summary>
    ''' 设置连接服务器的字符串
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub set_linkstring_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'string   conn   =   "Database= "   +   txt数据库.Text   +   ";Server= "   +   txt服务器.Text   +   ";uid= "   +   txt用户名.Text   +   ";pwd= "   +   txt密码.Text   +   ";Connection   Reset=FALSE "; 
        Dim ser_obj As New 服务器配置
        ser_obj.Show()
    End Sub
End Class
