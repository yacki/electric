Module Resizeform
    ''Option Explicit
    'Public Xtwips As Integer, Ytwips As Integer
    'Public Xpixels As Integer, Ypixels As Integer

    'Structure FRMSIZE
    '    Dim Height As Long
    '    Dim Width As Long
    'End Structure


    'Public RePosForm As Boolean
    'Public DoResize As Boolean
    'Public myForm As FRMSIZE
    'Public DesignX As Integer
    'Public DesignY As Integer
    'Public ScaleFactorX As Single, ScaleFactorY As Single

    'Sub Resize_For_Resolution(ByVal SFX As Single, ByVal SFY As Single, ByVal myForm As Form)
    '    Dim I As Integer
    '    Dim SFFont As Single
    '    SFFont = (SFX + SFY) / 2
    '    On Error Resume Next
    '    With myForm
    '        For I = 0 To .Count - 1
    '            If TypeOf .Controls(I) Is ComboBox Then
    '                .Controls(I).Left = .Controls(I).Left * SFX
    '                .Controls(I).Top = .Controls(I).Top * SFY
    '                .Controls(I).Width = .Controls(I).Width * SFX
    '            Else
    ' .Controls(I).Move .Controls(I).Left * SFX, _
    '.Controls(I).Top * SFY, _
    '  .Controls(I).Width * SFX, _
    '  .Controls(I).Height * SFY
    '            End If
    '            .Controls(I).Font.Size = .Controls(I).Font.Size * SFFont
    '        Next I
    '        If RePosForm Then
    ' .Move .Left * SFX, .Top * SFY, .Width * SFX, .Height * SFY 
    '        End If
    '    End With
    'End Sub

    'Public Sub FormResize(ByVal TheForm As Form)
    '    Dim ScaleFactorX As Single, ScaleFactorY As Single
    '    If Not DoResize Then
    '        DoResize = True
    '        Exit Sub
    '    End If
    '    RePosForm = False
    '    ScaleFactorX = TheForm.Width / myForm.Width
    '    ScaleFactorY = TheForm.Height / myForm.Height
    '    Resize_For_Resolution(ScaleFactorX, ScaleFactorY, TheForm)
    '    myForm.Height = TheForm.Height
    '    myForm.Width = TheForm.Width
    'End Sub

    'Public Sub AdjustForm(ByVal TheForm As Form)
    '    Dim Res As String ' Returns resolution of systemZjVVB知识库
    '    ' Put the design time resolution in hereZjVVB知识库
    '    DesignX = 640
    '    DesignY = 480
    '    RePosForm = True
    '    DoResize = False
    '    Xtwips = Screen.TwipsPerPixelX
    '    Ytwips = Screen.TwipsPerPixelY
    '    Ypixels = Screen.Height / Ytwips
    '    Xpixels = Screen.Width / Xtwips
    '    ScaleFactorX = (Xpixels / DesignX)
    '    ScaleFactorY = (Ypixels / DesignY)
    '    TheForm.ScaleMode = 1
    '    Resize_For_Resolution(ScaleFactorX, ScaleFactorY, TheForm)
    '    Res = Str$(Xpixels) + "  by " + Str$(Ypixels)
    '    'Debug.Print ResZjVVB知识库
    '    myForm.Height = TheForm.Height
    '    myForm.Width = TheForm.Width

    'End Sub

End Module
