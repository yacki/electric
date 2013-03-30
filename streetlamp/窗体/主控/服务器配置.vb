Public Class 服务器配置
    Private m_usertag As Boolean = False   '是否拥有修改权限

    Private Sub 服务器配置_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '设置登录窗体的图标
        Me.Icon = New Icon("图片\favicon.ico", 50, 50)
     
    End Sub

    ''' <summary>
    ''' 设置连接字符串
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_set_server_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_set_server.Click
        If m_usertag = True Then
            If MsgBox("服务器的配置有可能影响到您的软件使用，请确认是否进行配置?", MsgBoxStyle.Exclamation, PROJECT_TITLE_STRING) = MsgBoxResult.Ok Then
                UpdateConnectionStringsConfig("streetlamp.My.MySettings.streetlampConnectionString", Trim(rtb_text1.Text), "System.Data.SqlClient")
                UpdateConnectionStringsConfig("streetlamp.Login", Trim(rtb_text2.Text), "System.Data.SqlClient")

            End If
        Else
            MsgBox("请输入用户名和密码，否则无权进行服务器配额", , PROJECT_TITLE_STRING)

        End If


    End Sub
    '<summary>
    '更新连接字符串
    '</summary>
    '<param name="newName">连接字符串名称</param>
    '<param name="newConString">连接字符串内容</param>
    '<param name="newProviderName">数据提供程序名称</param>
    Private Sub UpdateConnectionStringsConfig(ByVal newName As String, ByVal newConString As String, ByVal newProviderName As String)

        Dim isModified As Boolean = False '记录该连接串是否已经存在
        '如果要更改的连接串已经存在
        If System.Configuration.ConfigurationManager.ConnectionStrings(newName) IsNot System.DBNull.Value Then
            isModified = True
        End If
        '新建一个连接字符串实例
        Dim mySettings As New System.Configuration.ConnectionStringSettings(newName, newConString, newProviderName)
        '打开可执行的配置文件*.exe.config
        Dim config As Configuration.Configuration
        config = System.Configuration.ConfigurationManager.OpenExeConfiguration(Configuration.ConfigurationUserLevel.None)

        ' 如果连接串已存在，首先删除它
        If isModified Then
            config.ConnectionStrings.ConnectionStrings.Remove(newName)
        End If

        '将新的连接串添加到配置文件中.
        config.ConnectionStrings.ConnectionStrings.Add(mySettings)
        '保存对配置文件所作的更改
        config.Save(System.Configuration.ConfigurationSaveMode.Modified)
        ' 强制重新载入配置文件的ConnectionStrings配置节
        System.Configuration.ConfigurationManager.RefreshSection("ConnectionStrings")
    End Sub

    ''' <summary>
    ''' 输入用户名密码登陆
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_input_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_input.Click
        Try
            Dim read As New System.IO.StreamReader("set_password.txt", System.Text.Encoding.UTF8)
            If Trim(Me.tb_username.Text) <> read.ReadLine Or Trim(Me.tb_password.Text) <> read.ReadLine Then
                MsgBox("用户名或密码不正确，无法进行配置", , PROJECT_TITLE_STRING)
                read.Close()
                Exit Sub
            End If
            read.Close()
            m_usertag = True '可以修改
            Dim GetConnStr As String
            Dim config As Configuration.Configuration
            config = System.Configuration.ConfigurationManager.OpenExeConfiguration(Configuration.ConfigurationUserLevel.None)

            GetConnStr = config.ConnectionStrings.ConnectionStrings("streetlamp.My.MySettings.streetlampConnectionString").ConnectionString
            rtb_text1.Text = GetConnStr
            GetConnStr = config.ConnectionStrings.ConnectionStrings("streetlamp.Login").ConnectionString

            'GetConnStr = System.Configuration.ConfigurationManager.ConnectionStrings("streetlamp.Logion").ConnectionString
            rtb_text2.Text = GetConnStr
        Catch ex As Exception

        End Try
    End Sub
End Class