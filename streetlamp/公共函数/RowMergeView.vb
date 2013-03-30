Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Drawing.Design
Imports System.Text
Imports System.Windows.Forms
Imports System.Collections
Imports System.Reflection
Imports System.Runtime.InteropServices

Public Class RowMergeView
    Inherits DataGridView

    Protected Overrides Sub OnCellPainting(ByVal e As DataGridViewCellPaintingEventArgs)

        If e.RowIndex > -1 And e.ColumnIndex > -1 Then
            DrawCell(e)
        End If

    End Sub

    '/ <summary>
    '/ DrawCell
    '/ </summary>
    '/ <param name="e"></param>
    Private Sub DrawCell(ByVal e As DataGridViewCellPaintingEventArgs)
        If e.CellStyle.Alignment = DataGridViewContentAlignment.NotSet Then
            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        End If
        Dim gridBrush As Brush = New SolidBrush(Me.GridColor)
        'Dim backBrush As SolidBrush = New SolidBrush(e.CellStyle.BackColor)
        Dim backBrush As SolidBrush = New SolidBrush(Color.White)
        Dim fontBrush As SolidBrush = New SolidBrush(e.CellStyle.ForeColor)
        Dim cellwidth As Integer
        Dim UpRows As Integer = 0
        Dim DownRows As Integer = 0
        Dim count As Integer = 0
        If Me.MergeColumnNames.Contains(Me.Columns(e.ColumnIndex).Name) And e.RowIndex <> -1 Then
            cellwidth = e.CellBounds.Width
            Dim gridLinePen As Pen = New Pen(gridBrush)
            Dim curValue As String = CType(e.Value, String)
            IIf(curValue Is Nothing, "", e.Value.ToString().Trim())
            Dim curSelected As String = CType(Me.CurrentRow.Cells(e.ColumnIndex).Value, String)
            IIf(curSelected Is Nothing, "", Me.CurrentRow.Cells(e.ColumnIndex).Value.ToString().Trim())
            'If Not String.IsNullOrEmpty(curValue) Then
            Dim i As Integer
            For i = e.RowIndex To Me.Rows.Count - 1 Step i + 1
                If Me.Rows(i).Cells(e.ColumnIndex).Value.ToString().Equals(curValue) Then

                    DownRows = DownRows + 1
                    If e.RowIndex <> i Then
                        cellwidth = cellwidth
                        IIf(cellwidth < Me.Rows(i).Cells(e.ColumnIndex).Size.Width, cellwidth, Me.Rows(i).Cells(e.ColumnIndex).Size.Width)
                    End If
                Else
                    Exit For
                End If
            Next

            Dim j As Integer
            For j = e.RowIndex To 0 Step j - 1
                If Me.Rows(j).Cells(e.ColumnIndex).Value.ToString().Equals(curValue) Then

                    UpRows = UpRows + 1
                    If e.RowIndex <> j Then
                        cellwidth = cellwidth
                        IIf(cellwidth < Me.Rows(j).Cells(e.ColumnIndex).Size.Width, cellwidth, Me.Rows(j).Cells(e.ColumnIndex).Size.Width)
                    End If
                Else
                    Exit For
                End If
            Next

            count = DownRows + UpRows - 1
            If count < 2 Then
                Return
            End If
            'End If
            If Me.Rows(e.RowIndex).Selected Then
                backBrush.Color = e.CellStyle.SelectionBackColor
                fontBrush.Color = e.CellStyle.SelectionForeColor
            End If

            e.Graphics.FillRectangle(backBrush, e.CellBounds)

            PaintingFont(e, cellwidth, UpRows, DownRows, count)
            If DownRows = 1 Then
                e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right - 1, e.CellBounds.Bottom - 1)
                count = 0
            End If

            e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1, e.CellBounds.Top, e.CellBounds.Right - 1, e.CellBounds.Bottom - 1)

            e.Handled = True
        End If
    End Sub

    '/ <summary>
    '/ PaintingFont
    '/ </summary>
    Private Sub PaintingFont(ByVal e As System.Windows.Forms.DataGridViewCellPaintingEventArgs, ByVal cellwidth As Integer, ByVal UpRows As Integer, ByVal DownRows As Integer, ByVal count As Integer)
        Dim fontBrush As SolidBrush = New SolidBrush(e.CellStyle.ForeColor)
        Dim fontheight As Integer = CType(e.Graphics.MeasureString(e.Value.ToString(), e.CellStyle.Font).Height, Integer)
        Dim fontwidth As Integer = CType(e.Graphics.MeasureString(e.Value.ToString(), e.CellStyle.Font).Width, Integer)
        Dim cellheight As Integer = e.CellBounds.Height

        If e.CellStyle.Alignment = DataGridViewContentAlignment.BottomCenter Then
            e.Graphics.DrawString(CType(e.Value, String), e.CellStyle.Font, fontBrush, CType(e.CellBounds.X + (cellwidth - fontwidth) / 2, Single), e.CellBounds.Y + cellheight * DownRows - fontheight)
        ElseIf e.CellStyle.Alignment = DataGridViewContentAlignment.BottomLeft Then
            e.Graphics.DrawString(CType(e.Value, String), e.CellStyle.Font, fontBrush, e.CellBounds.X, e.CellBounds.Y + cellheight * DownRows - fontheight)
        ElseIf e.CellStyle.Alignment = DataGridViewContentAlignment.BottomRight Then
            e.Graphics.DrawString(CType(e.Value, String), e.CellStyle.Font, fontBrush, e.CellBounds.X + cellwidth - fontwidth, e.CellBounds.Y + cellheight * DownRows - fontheight)
        ElseIf e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter Then
            e.Graphics.DrawString(CType(e.Value, String), e.CellStyle.Font, fontBrush, CType(e.CellBounds.X + (cellwidth - fontwidth) / 2, Single), CType(e.CellBounds.Y - cellheight * (UpRows - 1) + (cellheight * count - fontheight) / 2, Single))
        ElseIf e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft Then
            e.Graphics.DrawString(CType(e.Value, String), e.CellStyle.Font, fontBrush, e.CellBounds.X, CType(e.CellBounds.Y - cellheight * (UpRows - 1) + (cellheight * count - fontheight) / 2, Single))
        ElseIf e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight Then
            e.Graphics.DrawString(CType(e.Value, String), e.CellStyle.Font, fontBrush, e.CellBounds.X + cellwidth - fontwidth, CType(e.CellBounds.Y - cellheight * (UpRows - 1) + (cellheight * count - fontheight) / 2, Single))
        ElseIf e.CellStyle.Alignment = DataGridViewContentAlignment.TopCenter Then
            e.Graphics.DrawString(CType(e.Value, String), e.CellStyle.Font, fontBrush, e.CellBounds.X + CType((cellwidth - fontwidth) / 2, Single), e.CellBounds.Y - cellheight * (UpRows - 1))
        ElseIf e.CellStyle.Alignment = DataGridViewContentAlignment.TopLeft Then
            e.Graphics.DrawString(CType(e.Value, String), e.CellStyle.Font, fontBrush, e.CellBounds.X, e.CellBounds.Y - cellheight * (UpRows - 1))
        ElseIf e.CellStyle.Alignment = DataGridViewContentAlignment.TopRight Then
            e.Graphics.DrawString(CType(e.Value, String), e.CellStyle.Font, fontBrush, e.CellBounds.X + cellwidth - fontwidth, e.CellBounds.Y - cellheight * (UpRows - 1))
        Else
            e.Graphics.DrawString(CType(e.Value, String), e.CellStyle.Font, fontBrush, e.CellBounds.X + CType((cellwidth - fontwidth) / 2, Single), CType(e.CellBounds.Y - cellheight * (UpRows - 1) + (cellheight * count - fontheight) / 2, Single))
        End If
    End Sub

    '/ <summary>
    '/ MergeColumnNames
    '/ </summary>
    Public Property MergeColumnNames() As List(Of String)
        Get
            Return _mergecolumnname
        End Get
        Set(ByVal Value As List(Of String))
            _mergecolumnname = Value
        End Set
    End Property
    Private _mergecolumnname As List(Of String) = New List(Of String)()


End Class
