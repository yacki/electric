''' <summary>
''' 备忘录，供用户记录关于系统操作中遇到的问题，为提高软件性能提供文档上的帮助
''' </summary>
''' <remarks></remarks>
''' 
Public Class 备忘录

    Private Sub 备忘录_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: 这行代码将数据加载到表“Record_list._record_list”中。您可以根据需要移动或移除它。
        Me.Record_listTableAdapter.Fill(Me.Record_list._record_list)
        '设置各个窗体的图标
        Me.Icon = New Icon("图片\favicon.ico", 32, 32)

    End Sub
    ''' <summary>
    ''' 记录备忘信息
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub input_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles input.Click

        If record_title.Text = "" Then
            MsgBox("主题不可为空", , PROJECT_TITLE_STRING)
            record_title.Focus()
            Exit Sub
        End If

        If recorder.Text = "" Then
            MsgBox("记录人员不可为空", , PROJECT_TITLE_STRING)
            recorder.Focus()
            Exit Sub
        End If

        If record_content.Text = "" Then
            MsgBox("主要内容部可为空", , PROJECT_TITLE_STRING)
            record_content.Focus()
            Exit Sub
        End If

        If record_title.TextLength > 50 Then
            MsgBox("主题长度大于50，请重新输入", , PROJECT_TITLE_STRING)
            record_title.Focus()
            Exit Sub
        End If

        If recorder.TextLength > 10 Then
            MsgBox("记录人员长度大于10，请重新输入", , PROJECT_TITLE_STRING)
            recorder.Focus()
            Exit Sub
        End If

        If record_content.TextLength > 200 Then
            MsgBox("主要内容长度大于200，请重新输入", , PROJECT_TITLE_STRING)
            record_content.Focus()
            Exit Sub

        End If

        Dim conn As New ADODB.Connection
        Dim rs As New ADODB.Recordset
        Dim msg As String
        Dim sql As String

        msg = ""
        sql = "select * from record_list"
        DBOperation.OpenConn(conn)
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "input_Click", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If

        If rs.RecordCount >= 0 Then
            rs.AddNew()
            rs.Fields("record_title").Value = Trim(record_title.Text)
            rs.Fields("recorder_name").Value = Trim(recorder.Text)
            rs.Fields("record_content").Value = Trim(record_content.Text)
            rs.Update()
        End If

        rs.Close()
        rs = Nothing
        conn.Close()
        conn = Nothing
        Me.Record_listTableAdapter.Fill(Me.Record_list._record_list)

    End Sub
End Class