Public Class copy_reuse
    Dim SQLDMOApplication_definst As New SQLDMO.Application
    Dim gSQLServer As SQLDMO.SQLServer
    Dim WithEvents oBackupEvent As SQLDMO.Backup
    Dim WithEvents oRestoreEvent As SQLDMO.Restore
    Dim gbConnected As Boolean
    Dim gDatabaseName As String
    Dim gBkupRstrFileName As String
    Dim gBkupRstrFilePath As String
    Const gTitle As String = "���ӷ�����"


    Private Sub connect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles connect.Click

        Dim Servername As String
        Dim UserName As String
        Dim password As String

        Try
            If gSQLServer Is Nothing Then
                gSQLServer = New SQLDMO.SQLServer
            End If
            '��ȡ���Ӳ���
            Servername = txtServerName.Text
            UserName = txtUserName.Text
            password = txtPassword.Text

            '���õ�½ʱ��
            gSQLServer.LoginTimeout = 15

            '���õ�½��ʽ
            'If optWinNTAuth.Checked = True Then
            '    gSQLServer.LoginSecure = True
            'End If
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            gSQLServer.Connect(Servername, UserName, password)
            gbConnected = True

            '�г���������
            FillDatabaseList()
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

            '֪ͨ���ӳɹ�
            MsgBox("���ӵ�������", MsgBoxStyle.OkOnly, gTitle)
            'buttonsConnectOpen()


            Exit Sub
        Catch ex As Exception

            MsgBox("����" & Err.Description)
            If System.Windows.Forms.Cursor.Current.Equals(System.Windows.Forms.Cursors.WaitCursor) Then
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            End If
        End Try
    End Sub
    Private Sub FillDatabaseList()
        cmbDatabaseName.Items.Clear()
        'ö���������ݿ⣬�����絽�б�ؼ�
        Dim oDB As SQLDMO.Database
        For Each oDB In gSQLServer.Databases
            If oDB.SystemObject = False Then
                cmbDatabaseName.Items.Add(oDB.Name)
                cmbDatabaseName2.Items.Add(oDB.Name)
            End If
        Next oDB
        '���ñ����ļ���·��
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

        'ѡ���һ�����ݿ�
        If cmbDatabaseName.Items.Count > 0 Then
            cmbDatabaseName.SelectedIndex = 0
            '����Ĭ�ϵı����ļ�·��
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
            '��һ���Ѿ����ӵķ������Ͽ�
            If gbConnected = True Then
                Msg = "ȷ���ӷ������Ͽ���"
                Response = CStr(MsgBox(Msg, MsgBoxStyle.OkCancel, gTitle))
                If Response = CStr(MsgBoxResult.Ok) Then
                    Call gSQLServer.DisConnect()
                    gSQLServer = Nothing
                    cmbDatabaseName.Items.Clear()
                    txtDataFileName2.Text = ""
                    gbConnected = False
                    'buttonsConnectClosed()
                    If MsgBox("�������ѶϿ��������˳���", MsgBoxStyle.YesNo, "��ʾ") = MsgBoxResult.Yes Then
                        'LoginForm1.Show()
                        Me.Close()

                    End If

                End If
            End If

            Exit Sub
        Catch ex As Exception
            MsgBox("����" & Err.Description)
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
            'ɾ��ԭ�����ļ�
            If Len(Dir(gBkupRstrFileName)) > 0 Then
                Kill((gBkupRstrFileName))

            End If
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            '�������ݿ�
            oBackup.SQLBackup(gSQLServer)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            oBackupEvent = Nothing
            oBackup = Nothing
            MsgBox("���ݿⱸ�ݳɹ���", , "��ʾ����")

            Exit Sub
        Catch ex As Exception
            MsgBox("����" & Err.Description)

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
            '�ָ����ݿ�
            oRestore.SQLRestore(gSQLServer)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            oRestoreEvent = Nothing
            oRestore = Nothing
            MsgBox("���ݿ�ָ��ɹ���", , "��ʾ����")
            Exit Sub
        Catch ex As Exception
            MsgBox("����" & Err.Description)

        End Try
    End Sub

    Private Sub find_file_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles find_file.Click
        With find_bak
            .InitialDirectory = Environment.CurrentDirectory
            .Filter = "BAK�ļ���*BAK)|*.bak"
            .Title = "���ݿⱸ���ļ�"
            .RestoreDirectory = True

        End With
        find_bak.ShowDialog()
        txtDataFileName2.Text = find_bak.FileName


    End Sub

    Private Sub set_constring_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim config_db As New Config
        'Dim conn_string As String  '�����ַ���
        'conn_string = config_db.GetValue("connectionString")

        '2��Xml�����ĵ�����Ĭ�ϵ�������Ϣ----------------------------------
        'Dim filename As String = "app.config"
        'Dim read As New System.IO.StreamReader(filename, System.Text.Encoding.Default)
        ''����Ĵ������xml��
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
        '    _parameter.Add(xnl(i).Attributes(1).InnerText)   '���ؼ����ռ���al    
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
    ''' �޸�XML
    ''' </summary>
    ''' <param name="xmlFileName">Ҫ�޸ĵ�XML�ļ���</param>
    ''' <param name="rootName">XML�ļ��еĸ�Ԫ������</param>
    ''' <param name="elementNameArry">Ҫ�޸ĵ�Ԫ������</param>
    ''' <param name="innerTextArry">��Ӧ��Ҫ�޸ĵ�Ԫ��������޸��ı�����</param>
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
    '���������ַ���
    '</summary>
    '<param name="newName">�����ַ�������</param>
    '<param name="newConString">�����ַ�������</param>
    '<param name="newProviderName">�����ṩ��������</param>
    Private Sub UpdateConnectionStringsConfig(ByVal newName As String, ByVal newConString As String, ByVal newProviderName As String)

        Dim isModified As Boolean = False '��¼�����Ӵ��Ƿ��Ѿ�����
        '���Ҫ���ĵ����Ӵ��Ѿ�����
        If System.Configuration.ConfigurationManager.ConnectionStrings(newName) IsNot System.DBNull.Value Then
            isModified = True
        End If
        '�½�һ�������ַ���ʵ��
        Dim mySettings As New System.Configuration.ConnectionStringSettings(newName, newConString, newProviderName)
        '�򿪿�ִ�е������ļ�*.exe.config
        Dim config As Configuration.Configuration
        Config = System.Configuration.ConfigurationManager.OpenExeConfiguration(Configuration.ConfigurationUserLevel.None)

        ' ������Ӵ��Ѵ��ڣ�����ɾ����
        If isModified Then
            config.ConnectionStrings.ConnectionStrings.Remove(newName)
        End If
   
        '���µ����Ӵ���ӵ������ļ���.
        config.ConnectionStrings.ConnectionStrings.Add(mySettings)
        '����������ļ������ĸ���
        config.Save(System.Configuration.ConfigurationSaveMode.Modified)
        ' ǿ���������������ļ���ConnectionStrings���ý�
        System.Configuration.ConfigurationManager.RefreshSection("ConnectionStrings")
    End Sub

End Class