Partial Public Class welcome_win
    '主控窗体的地图操作部分
#Region "移动地图的操作"
    ''' <summary>
    ''' 鼠标移动图片时，鼠标点下后记录移动的起始坐标
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub map_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pb_map.MouseDown
        Me.Cursor = Cursors.Hand   '鼠标的样式设置为手型
        g_movepictag = 1 '移动图片的标志
        g_beginpoint = Control.MousePosition   '记录下移动的开始坐标位置

        If e.Button = Windows.Forms.MouseButtons.Right Then
            '点击右键
            m_rightbuttonpos.X = (Me.pb_map.Location.X - Me.DesktopLocation.X - (Me.Width - Me.ClientSize.Width) - SplitContainer3.Panel1.Width) - Me.GroupBox1.Location.X   '(Me.Width - Me.ClientSize.Width) 为左边框的宽度
            m_rightbuttonpos.Y = (Me.pb_map.Location.Y - Me.DesktopLocation.Y - (Me.Height - Me.ClientSize.Height)) - Me.GroupBox1.Location.Y '(Me.Height - Me.ClientSize.Height)为上标题栏的高度

            m_rightbuttonpos.X = (Control.MousePosition.X - Me.pb_map.Location.X - Me.DesktopLocation.X - (Me.Width - Me.ClientSize.Width) - SplitContainer3.Panel1.Width) - Me.GroupBox1.Location.X   '(Me.Width - Me.ClientSize.Width) 为左边框的宽度
            m_rightbuttonpos.Y = (Control.MousePosition.Y - Me.pb_map.Location.Y - Me.DesktopLocation.Y - (Me.Height - Me.ClientSize.Height)) - Me.GroupBox1.Location.Y '(Me.Height - Me.ClientSize.Height)为上标题栏的高度
            '经过缩放后的点的坐标
            m_rightbuttonpos.X = (m_rightbuttonpos.X) / (MAP_SIZE_BASE + MAP_SIZE_CHANGE * tb_map_size.Value)
            m_rightbuttonpos.Y = (m_rightbuttonpos.Y) / (MAP_SIZE_BASE + MAP_SIZE_CHANGE * tb_map_size.Value)
            Me.SetTextLabelDelegate("X:" & m_rightbuttonpos.X & " ,Y:" & m_rightbuttonpos.Y, Tool, "location_string")  '状态栏显示当前坐标

        End If
    End Sub

    ''' <summary>
    ''' 鼠标移动图片时，鼠标松开后记录移动的终点坐标
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub map_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pb_map.MouseUp
        g_endpoint = Control.MousePosition          '记录下移动的终点位置
        g_midpoint.X = pb_map.Left + pb_map.Width / 2  '记录中点的X坐标
        g_midpoint.Y = pb_map.Top + pb_map.Height / 2  '记录中点的Y坐标

        If g_movepictag = 1 And g_endpoint.X <> g_beginpoint.X And g_endpoint.Y <> g_beginpoint.Y Then   '开始移动
            If g_endpoint.Y - g_beginpoint.Y > 0 Then   '向上移动

                pb_map.Top = pb_map.Top + (g_endpoint.Y - g_beginpoint.Y)   '移动地图
            Else
                If g_endpoint.Y - g_beginpoint.Y < 0 Then   '向下移动

                    pb_map.Top = pb_map.Top + (g_endpoint.Y - g_beginpoint.Y)   '移动地图
                End If

            End If

            If g_endpoint.X - g_beginpoint.X > 0 Then  '向左移动

                pb_map.Left = pb_map.Left + (g_endpoint.X - g_beginpoint.X)   '移动地图
            Else
                If pb_map.Location.X > -pb_map.Width + GroupBox1.Width Then   '地图没有超出边界

                    pb_map.Left = pb_map.Left + (g_endpoint.X - g_beginpoint.X)    '移动地图
                End If
            End If

            g_movepictag = 0  '将移动地图的标识清零，为下一次移动服务
        End If
        Me.Cursor = Cursors.Default  '鼠标恢复到默认状态

    End Sub

#End Region

#Region "缩放地图"

    ''' <summary>
    ''' 按比例缩放地图
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub map_size_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tb_map_size.Scroll
        Dim size As Integer
        size = MAP_SIZE_BASE * 100 + MAP_SIZE_CHANGE * 100 * tb_map_size.Value
        g_changemapvalue = tb_map_size.Value  '记录地图的大小
        If size = 0 Then
            size = 1
        End If
        map_size_id.Text = "地图尺寸：" & size.ToString & "%"
    End Sub


    ''' <summary>
    ''' 地图的大小值改变，调整地图的位置和大小
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub map_size_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tb_map_size.ValueChanged
        tb_map_size.Value = g_changemapvalue
        Me.SetMapSizeDelegate(pb_map)  '调整地图的位置及大小
    End Sub
#End Region
End Class
