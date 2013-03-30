Public Class copy_reuse
    Dim SQLDMOApplication_definst As New SQLDMO.Application
    Dim gSQLServer As SQLDMO.SQLServer
    Dim WithEvents oBackupEvent As SQLDMO.Backup
    Dim WithEvents oRestoreEvent As SQLDMO.Restore
    Dim gbConnected As Boolean
    Dim gDatabaseName As String
    Dim gBkupRstrFileName As String
    Dim gBkupRstrFilePath As String
    Const gTitle As String = "连接服务器"


    Private Sub connect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles connect.Click

        Dim Servername As String
        Dim UserName As String
        Dim password As String

        Try
            If gSQLServer Is Nothing Then
                gSQLServer = New SQLDMO.SQLServer
            End If
            '获取连接参数
            Servername = txtServerName.Text
            UserName = txtUserName.Text
            password = txtPassword.Text

            '设置登陆时间
            gSQLServer.LoginTimeout = 15

            '设置登陆方式
            'If optWinNTAuth.Checked = True Then
            '    gSQLServer.LoginSecure = True
            'End If
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            gSQLServer.Connect(Servername, UserName, password)
            gbConnected = True

            '列出所有数据
            FillDatabaseList()
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

            '通知连接成功
            MsgBox("连接到服务器", MsgBoxStyle.OkOnly, gTitle)
            'buttonsConnectOpen()


            Exit Sub
        Catch ex As Exception

            MsgBox("错误：" & Err.Description)
            If System.Windows.Forms.Cursor.Current.Equals(System.Windows.Forms.Cursors.WaitCursor) Then
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            End If
        End Try
    End Sub
    Private Sub FillDatabaseList()
        cmbDatabaseName.Items.Clear()
        '枚举所有数据库，并加如到列表控件
        Dim oDB As SQLDMO.Database
        For Each oDB In gSQLServer.Databases
            If oDB.SystemObject = False Then
                cmbDatabaseName.Items.Add(oDB.Name)
                cmbDatabaseName2.Items.Add(oDB.Name)
            End If
        Next oDB
        '设置备份文件的路径
        Dim MyPos As Short
        gBkupRstrFilePath = CurDir()
        MyPos = InStr(1, CurDir(), "DevTools", 1)
        If MyPos > 0 Then
            gBkupRstrFilePath = Microsoft.VisualBasic.Left(gBkupRstrFilePath, MyPos - 1)
            If Len(Dir(gBkupRstrFilePath & "backup", FileAttribute.Directory)) Then

                gBkupRstrFilePath = gBkupRstrFilePath & "backup\"
            Else
                gBkupRstrFilePath = "D:\"
            End If
        Else
            gBkupRstrFilePath = "D:\"
        End If

        '选择第一个数据库
        If cmbDatabaseName.Items.Count > 0 Then
            cmbDatabaseName.SelectedIndex = 0
            '设置默认的备份文件路径
            If Len(cmbDatabaseName.Text) <> 0 Then
                txtDataFileName.Text = gBkupRstrFilePath & cmbDatabaseName.Text & ".bak"
            End If
        End If
        If cmbDatabaseName2.Items.Count > 0 Then
            cmbDatabaseName2.SelectedIndex = 0
          
        End If

    End Sub


    Private Sub cmdDisconnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDisconnect.Click

        Try
            Dim Msg As String
            Dim Response As String
            '从一个已经连接的服务器断开
            If gbConnected = True Then
                Msg = "确定从服务器断开吗？"
                Response = CStr(MsgBox(Msg, MsgBoxStyle.OkCancel, gTitle))
                If Response = CStr(MsgBoxResult.Ok) Then
                    Call gSQLServer.DisConnect()
                    gSQLServer = Nothing
                    cmbDatabaseName.Items.Clear()
                    txtDataFileName2.Text = ""
                    gbConnected = False
                    'buttonsConnectClosed()
                    If MsgBox("服务器已断开，现在退出？", MsgBoxStyle.YesNo, "提示") = MsgBoxResult.Yes Then
                        'LoginForm1.Show()
                        Me.Close()

                    End If

                End If
            End If

            Exit Sub
        Catch ex As Exception
            MsgBox("错误：" & Err.Description)
        End Try
    End Sub

    Private Sub cmdBackup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBackup.Click
        Try
            Dim oBackup As SQLDMO.Backup
            gDatabaseName = cmbDatabaseName.Text
            oBackup = New SQLDMO.Backup
            oBackupEvent = oBackup
            oBackup.Database = gDatabaseName
            gBkupRstrFileName = txtDataFileName.Text
            oBackup.Files = gBkupRstrFileName
            '删除原来的文件
            If Len(Dir(gBkupRstrFileName)) > 0 Then
                Kill((gBkupRstrFileName))

            End If
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            '备份数据库
            oBackup.SQLBackup(gSQLServer)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            oBackupEvent = Nothing
            oBackup = Nothing
            MsgBox("数据库备份成功！", , "提示窗口")

            Exit Sub
        Catch ex As Exception
            MsgBox("错误：" & Err.Description)

        End Try
    End Sub

    Private Sub cmdRestore_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRestore.Click
        

        Try


            Dim oRestore As SQLDMO.Restore
            ' Dim msg As String
            '  Dim Response As String
            gDatabaseName = cmbDatabaseName2.Text
            oRestore = New SQLDMO.Restore
            oRestoreEvent = oRestore
            oRestore.Database = gDatabaseName
            gBkupRstrFileName = txtDataFileName2.Text
            oRestore.Files = gBkupRstrFileName
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            '恢复数据库
            oRestore.SQLRestore(gSQLServer)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            oRestoreEvent = Nothing
            oRestore = Nothing
            MsgBox("数据库恢复成功！", , "提示窗口")
            Exit Sub
        Catch ex As Exception
            MsgBox("错误：" & Err.Description)

        End Try
    End Sub

    Private Sub find_file_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles find_file.Click
        With find_bak
            .InitialDirectory = Environment.CurrentDirectory
            .Filter = "BAK文件（*BAK)|*.bak"
            .Title = "数据库备份文件"
            .RestoreDirectory = True

        End With
        find_bak.ShowDialog()
        txtDataFileName2.Text = find_bak.FileName


    End Sub

    Private Sub set_constring_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim config_db As New Config
        'Dim conn_string As String  '连接字符串
        'conn_string = config_db.GetValue("connectionString")

        '2读Xml配置文档设置默认的配置信息----------------------------------
        'Dim filename As String = "app.config"
        'Dim read As New System.IO.StreamReader(filename, System.Text.Encoding.Default)
        ''下面的代码解析xml流
        'Dim xd As New System.Xml.XmlDocument()
        'Dim connstr As String
        'xd.Load(read)
        'Dim _parameter As New ArrayList()
        'Dim root As System.Xml.XmlNode
        'root = xd.SelectSingleNode("configuration")
        'Dim xnl As Xml.XmlNodeList = root.SelectNodes("connectionStrings/add")
        'Dim i As Integer = 0
        'connstr = ""
        'While i < xnl.Count
        '    _parameter.Add(xnl(i).Attributes(1).InnerText)   '将关键子收集到al    
        '    i += 1
        'End While
        'i = 0
        'While i < xnl.Count
        '    connstr &= _parameter(i).ToString & vbCrLf
        '    Me.server_linktext.AppendText(connstr)
        '    i += 1
        'End While
        'read.Close()
    End Sub

    ''' <summary>
    ''' 修改XML
    ''' </summary>
    ''' <param name="xmlFileName">要修改的XML文件名</param>
    ''' <param name="rootName">XML文件中的根元素名称</param>
    ''' <param name="elementNameArry">要修改的元素数组</param>
    ''' <param name="innerTextArry">对应于要修改的元素数组的修改文本数组</param>
    ''' <remarks></remarks>
    Public Sub modifXML(ByVal xmlFileName As String, ByVal rootName As String, ByVal elementNameArry() As String, ByVal innerTextArry() As String)
        If My.Computer.FileSystem.FileExists(Application.StartupPath.Trim & "" & xmlFileName) Then
            Dim doc As New Xml.XmlDocument
            doc.Load(Application.StartupPath.Trim & "" & xmlFileName)
            Dim list As Xml.XmlNodeList = doc.SelectSingleNode(rootName).ChildNodes
            For Each xn As Xml.XmlNode In list
                Dim xe As Xml.XmlElement
                xe = xn
                Dim nls As Xml.XmlNodeList = xe.ChildNodes
                For Each xn1 As Xml.XmlNode In nls
                    Dim xe2 As Xml.XmlElement
                    xe2 = xn1
                    For i As Integer = 0 To elementNameArry.Length - 1
                        If xe2.Name = elementNameArry(i) Then
                            xe2.InnerText = innerTextArry(i)
                        End If
                    Next
                Next
            Next
            doc.Save(Application.StartupPath.Trim & "" & xmlFileName)
        End If
    End Sub



    Private Sub set_connstr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.UpdateConnectionStringsConfig("streetlamp.My.MySettings.streetlampConnectionString", "jhhhhh", "System.Data.SqlClient")
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
        Config = System.Configuration.ConfigurationManager.OpenExeConfiguration(Configuration.ConfigurationUserLevel.None)

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

End Class