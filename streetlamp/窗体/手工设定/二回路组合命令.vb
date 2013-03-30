Public Class 二回路组合命令
    Private m_lamp(7) As System.Windows.Forms.PictureBox
    Private m_denggan_order() As Integer = {0, 0, 0, 0} '灯杆的控制方式
    Private m_lamp_order() As Integer = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0} '12位的四组灯头控制方式

    Private Sub 二回路组合命令_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '设置各个窗体的图标
        Me.Icon = New Icon("图片\favicon.ico", 32, 32)
        Init_lamp()
        Init_lamp_color()

    End Sub

    ''' <summary>
    ''' 初始化m_lamp数组，包括24盏模拟灯的名称
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Init_lamp()
        m_lamp(0) = pb_lamp1
        m_lamp(1) = pb_lamp2
        m_lamp(2) = pb_lamp3
        m_lamp(3) = pb_lamp4
        m_lamp(4) = pb_lamp5
        m_lamp(5) = pb_lamp6
        m_lamp(6) = pb_lamp7
        m_lamp(7) = pb_lamp8
    End Sub

    ''' <summary>
    ''' 将模拟灯的背景色初始化为白色
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Init_lamp_color()
        Dim i As Integer = 0
        While i <= 7
            m_lamp(i).BackColor = Color.White  '初始化设为白色
            i += 1
        End While
    End Sub

    ''' <summary>
    ''' 选择一号灯杆
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chb_denggan_1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chb_denggan_1.Click
        If chb_denggan_1.Checked = True Then
            gpb_denggan1_group.Enabled = True
            m_denggan_order(0) = 1
        Else
            chb_lamp_1_1.Checked = False
            chb_lamp_1_2.Checked = False
            gpb_denggan1_group.Enabled = False
            m_denggan_order(0) = 0
        End If

    End Sub

    ''' <summary>
    ''' 选择2号灯杆
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chb_denggan_2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chb_denggan_2.Click
        If chb_denggan_2.Checked = True Then
            gpb_denggan2_group.Enabled = True
            m_denggan_order(1) = 1
        Else
            chb_lamp_2_1.Checked = False
            chb_lamp_2_2.Checked = False
            gpb_denggan2_group.Enabled = False
            m_denggan_order(1) = 0
        End If
    End Sub

    ''' <summary>
    ''' 选择3号灯杆
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chb_denggan_3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chb_denggan_3.Click
        If chb_denggan_3.Checked = True Then
            gpb_denggan3_group.Enabled = True
            m_denggan_order(2) = 1
        Else
            chb_lamp_3_1.Checked = False
            chb_lamp_3_2.Checked = False
            gpb_denggan3_group.Enabled = False
            m_denggan_order(2) = 0
        End If
    End Sub

    ''' <summary>
    ''' 选择4号灯杆
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chb_denggan_4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chb_denggan_4.Click
        If chb_denggan_4.Checked = True Then
            gpb_denggan4_group.Enabled = True
            m_denggan_order(3) = 1
        Else
            chb_lamp_4_1.Checked = False
            chb_lamp_4_2.Checked = False
            gpb_denggan4_group.Enabled = False
            m_denggan_order(3) = 0
        End If
    End Sub
   

    ''' <summary>
    ''' 点击1号灯杆1号灯头
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chb_lamp_1_1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chb_lamp_1_1.CheckedChanged
        If chb_lamp_1_1.Checked = True Then
            m_lamp_order(0) = 1
            m_lamp(0).BackColor = Color.Yellow
        Else
            m_lamp_order(0) = 0
            m_lamp(0).BackColor = Color.White
        End If
    End Sub

    ''' <summary>
    ''' 点击1号灯杆2号灯头
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chb_lamp_1_2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chb_lamp_1_2.CheckedChanged
        If chb_lamp_1_2.Checked = True Then
            m_lamp_order(1) = 1
            m_lamp(1).BackColor = Color.Yellow
        Else
            m_lamp_order(1) = 0
            m_lamp(1).BackColor = Color.White
        End If
    End Sub
    ''' <summary>
    ''' 点击2号灯杆1号灯头
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chb_lamp_2_1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chb_lamp_2_1.CheckedChanged
        If chb_lamp_2_1.Checked = True Then
            m_lamp_order(2) = 1
            m_lamp(2).BackColor = Color.Yellow
        Else
            m_lamp_order(2) = 0
            m_lamp(2).BackColor = Color.White
        End If
    End Sub

    ''' <summary>
    ''' 点击2号灯杆2号灯头
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chb_lamp_2_2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chb_lamp_2_2.CheckedChanged
        If chb_lamp_2_2.Checked = True Then
            m_lamp_order(3) = 1
            m_lamp(3).BackColor = Color.Yellow
        Else
            m_lamp_order(3) = 0
            m_lamp(3).BackColor = Color.White
        End If
    End Sub
    ''' <summary>
    ''' 点击3号灯杆1号灯头
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chb_lamp_3_1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chb_lamp_3_1.CheckedChanged
        If chb_lamp_3_1.Checked = True Then
            m_lamp_order(4) = 1
            m_lamp(4).BackColor = Color.Yellow
        Else
            m_lamp_order(4) = 0
            m_lamp(4).BackColor = Color.White
        End If
    End Sub

    ''' <summary>
    ''' 点击3号灯杆2号灯头
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chb_lamp_3_2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chb_lamp_3_2.CheckedChanged
        If chb_lamp_3_2.Checked = True Then
            m_lamp_order(5) = 1
            m_lamp(5).BackColor = Color.Yellow
        Else
            m_lamp_order(5) = 0
            m_lamp(5).BackColor = Color.White
        End If
    End Sub
    ''' <summary>
    ''' 点击4号灯杆1号灯头
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chb_lamp_4_1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chb_lamp_4_1.CheckedChanged
        If chb_lamp_4_1.Checked = True Then
            m_lamp_order(6) = 1
            m_lamp(6).BackColor = Color.Yellow
        Else
            m_lamp_order(6) = 0
            m_lamp(6).BackColor = Color.White
        End If
    End Sub

    ''' <summary>
    ''' 点击4号灯杆2号灯头
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chb_lamp_4_2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chb_lamp_4_2.CheckedChanged
        If chb_lamp_4_2.Checked = True Then
            m_lamp_order(7) = 1
            m_lamp(7).BackColor = Color.Yellow
        Else
            m_lamp_order(7) = 0
            m_lamp(7).BackColor = Color.White
        End If
    End Sub

    ''' <summary>
    ''' 增加二回路控制命令
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub add_grouporder_string()
        If chb_denggan_1.Checked = False And chb_denggan_2.Checked = False And chb_denggan_3.Checked = False And chb_denggan_4.Checked = False Then
            MsgBox("请选择要控制的灯杆号", , PROJECT_TITLE_STRING)
            chb_denggan_1.Focus()
            Exit Sub
        End If
        If lb_grouporder_name.Text = "" Then
            MsgBox("请输入命令名称", , PROJECT_TITLE_STRING)
            lb_grouporder_name.Focus()
            Exit Sub
        End If
        Dim denggan_value As String = ""
        Dim lamp_value As String = ""
        Dim i As Integer = 0
        Dim msg As String
        Dim sql As String
        Dim conn As New ADODB.Connection
        Dim rs As New ADODB.Recordset
        Dim group_order_string As String '命令字符串
        Dim group_method_string As String '记录命令格式，01表示

        msg = ""
        sql = ""
        DBOperation.OpenConn(conn)
        While i <= 3
            denggan_value &= m_denggan_order(i)
            i += 1
        End While
        denggan_value = "0" & Com_inf.BIN_to_HEX(denggan_value)
        group_order_string = ""
        group_method_string = ""
      
        i = 0
        While i <= 7
            'If i > 0 Then
            If i Mod 2 = 0 Then
                lamp_value &= "0"
                'group_method_string &= "0"
            End If
          
            'End If
            lamp_value &= m_lamp_order(i)
            group_method_string &= m_lamp_order(i)
            If (i + 1) Mod 2 = 0 Then
                lamp_value &= "0"
                'group_method_string &= "0"
            End If

            i += 1
        End While

       
        lamp_value = Com_inf.BIN_to_HEX(lamp_value)

        group_order_string = denggan_value & " " & Mid(lamp_value, 1, 2) & " " & Mid(lamp_value, 3, 2)

        If MsgBox("是否保存当前的二回路组合控制命令？", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.Yes Then
            sql = "select * from group_order where grouporder_name='" & Trim(lb_grouporder_name.Text) & "' and grouporder_type=2"
            rs = DBOperation.SelectSQL(conn, sql, msg)
            If rs Is Nothing Then
                MsgBox(MSG_ERROR_STRING & "Add_grouporderstring", , PROJECT_TITLE_STRING)
                conn.Close()
                conn = Nothing
                Exit Sub
            End If
            If rs.RecordCount > 0 Then
                If MsgBox("命令名称已存在，是否覆盖原有命令？", MsgBoxStyle.YesNo, PROJECT_TITLE_STRING) = MsgBoxResult.Yes Then
                    rs.Fields("grouporder").Value = group_order_string
                    rs.Fields("grouporder_string").Value = group_method_string
                    rs.Update()
                End If
            Else
                rs.AddNew()
                rs.Fields("grouporder_name").Value = Trim(lb_grouporder_name.Text)
                rs.Fields("grouporder").Value = group_order_string
                rs.Fields("grouporder_string").Value = group_method_string
                rs.Fields("grouporder_type").Value = 2
                rs.Update()
            End If
            MsgBox("命令保存完毕！", , PROJECT_TITLE_STRING)
        End If

        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    ''' 删除已保存的控制模式
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub del_grouporder_string()
        If cb_order_list.Text = "" Then
            MsgBox("请选择需要删除的命令名称", , PROJECT_TITLE_STRING)
            cb_order_list.Focus()
            Exit Sub
        End If
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String

        msg = ""
        sql = "delete from group_order where grouporder_name='" & Trim(cb_order_list.Text) & "'"
        DBOperation.OpenConn(conn)
        DBOperation.ExecuteSQL(conn, sql, msg)

        MsgBox("命令删除成功！", , PROJECT_TITLE_STRING)

        conn.Close()
        conn = Nothing
    End Sub


    ''' <summary>
    ''' 将用户的选择组合成控制命令发送到数据库中
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bt_send_order_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_send_order.Click
        If rb_add_order.Checked = True Then
            add_grouporder_string()
        End If
        If rb_del_order.Checked = True Then
            del_grouporder_string()
        End If
    End Sub

    ''' <summary>
    ''' 选择增加控制命令
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rb_add_order_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_add_order.Click
        If rb_add_order.Checked = True Then
            '表示增加控制命令
            GBMethod.Enabled = True
            gpb_denggan1_group.Enabled = False
            gpb_denggan2_group.Enabled = False
            gpb_denggan3_group.Enabled = False
            gpb_denggan4_group.Enabled = False
            cb_order_list.Visible = False
            lb_grouporder_name.Visible = True
            bt_send_order.Text = "保存命令"
        End If
    End Sub

    ''' <summary>
    ''' 删除控制命令
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rb_del_order_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_del_order.Click
        If rb_del_order.Checked = True Then
            '表示删除控制命令
            GBMethod.Enabled = False
            gpb_denggan1_group.Enabled = False
            gpb_denggan2_group.Enabled = False
            gpb_denggan3_group.Enabled = False
            gpb_denggan4_group.Enabled = False
            cb_order_list.Visible = True
            lb_grouporder_name.Visible = False
            bt_send_order.Text = "删除命令"
        End If
    End Sub

    ''' <summary>
    ''' 选择控制名称列表
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub order_list_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_order_list.DropDown
        Dim rs As New ADODB.Recordset
        Dim conn As New ADODB.Connection
        Dim msg As String
        Dim sql As String

        msg = ""
        sql = "select grouporder_name from group_order where grouporder_type=2 order by id"
        DBOperation.OpenConn(conn)
        rs = DBOperation.SelectSQL(conn, sql, msg)
        If rs Is Nothing Then
            MsgBox(MSG_ERROR_STRING & "order_list_DropDown", , PROJECT_TITLE_STRING)
            conn.Close()
            conn = Nothing
            Exit Sub
        End If
        cb_order_list.Items.Clear()
        While rs.EOF = False
            cb_order_list.Items.Add(Trim(rs.Fields("grouporder_name").Value))
            rs.MoveNext()
        End While
        If cb_order_list.Items.Count > 0 Then
            cb_order_list.SelectedIndex = 0
        End If
        If rs.State = 1 Then
            rs.Close()
            rs = Nothing
        End If
        conn.Close()
        conn = Nothing
    End Sub

    ''' <summary>
    ''' 显示当前选中的控制状态
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cb_order_list_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_order_list.SelectedIndexChanged
        If rb_del_order.Checked = True Then
            '显示当前选中的控制状态
            Dim rs As New ADODB.Recordset
            Dim conn As New ADODB.Connection
            Dim msg As String
            Dim sql As String
            Dim control_method As String
            Dim i As Integer = 0

            msg = ""
            sql = "select grouporder_string from group_order where grouporder_name='" & Trim(cb_order_list.Text) & "' and grouporder_type=2"
            DBOperation.OpenConn(conn)
            rs = DBOperation.SelectSQL(conn, sql, msg)
            If rs Is Nothing Then
                MsgBox(MSG_ERROR_STRING & "order_list_SelectedIndexChanged", , PROJECT_TITLE_STRING)
                conn.Close()
                conn = Nothing
                Exit Sub
            End If
            If rs.RecordCount > 0 Then
                control_method = Trim(rs.Fields("grouporder_string").Value)
                control_method &= control_method
                While i < m_lamp.Length
                    If Mid(control_method, i + 1, 1) = 1 Then   '被选中的则显示黄色
                        m_lamp(i).BackColor = Color.Yellow
                    Else   '否则显示为白色
                        m_lamp(i).BackColor = Color.White
                    End If
                    i += 1
                End While
            End If
            If rs.State = 1 Then
                rs.Close()
                rs = Nothing
            End If
            conn.Close()
            conn = Nothing
        End If
    End Sub


    Private Sub 二回路组合命令_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        g_windowclose = 1
    End Sub
End Class