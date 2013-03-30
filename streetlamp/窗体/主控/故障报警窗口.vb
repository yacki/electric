Public Class 故障报警窗口
    Private m_controllampobj As New control_lamp
    Private Sub 故障报警窗口_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.Icon = New Icon("图片\favicon.ico", 32, 32)  'logo
        g_lampalarm_tag = True
        welcome_win.sanyao_alarm_list() '查找是否有单灯的报警
    End Sub

    Private Sub 故障报警窗口_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        g_lampalarm_tag = False
    End Sub
End Class