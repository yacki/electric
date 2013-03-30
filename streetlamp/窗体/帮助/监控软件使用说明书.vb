Public Class 监控软件使用说明书

    ''' <summary>
    ''' 软件使用说明书
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 监控软件使用说明书_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '设置各个窗体的图标
        Me.Icon = New Icon("图片\favicon.ico", 32, 32)
        Dim url As String
        url = System.Windows.Forms.Application.StartupPath
        WebBrowser1.Navigate(url & "\苏州电网故障报警系统使用说明书.doc")

    End Sub
End Class