Public Class SunTime
    Private Const PI = 3.1415926
    Private m_mothList() = {31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31}
    Private m_leapList() = {31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31}

    ''' <summary>
    ''' 判断是否为闰年
    ''' </summary>
    ''' <param name="iYear"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsLeapYear(ByVal iYear As Integer) As Boolean
        IsLeapYear = (iYear Mod 4 = 0 And iYear Mod 100 <> 0) Or iYear Mod 400 = 0
    End Function

    Private Function CalcGamma(ByVal iJulianDay As Integer) As Double
        CalcGamma = (2 * PI / 365) * (iJulianDay - 1)

    End Function

    Private Function CalcEqofTime(ByVal dGamma As Double) As Double
        CalcEqofTime = (229.18 * (0.000075 + 0.001868 * System.Math.Cos(dGamma) - 0.032077 * System.Math.Sin(dGamma) - 0.014615 * System.Math.Cos(2 * dGamma) - 0.040849 * System.Math.Sin(2 * dGamma)))
    End Function

    Private Function CalcSolarDec(ByVal dGamma As Double) As Double
        CalcSolarDec = (0.006918 - 0.399912 * System.Math.Cos(dGamma) + 0.070257 * System.Math.Sin(dGamma) - 0.006758 * System.Math.Cos(2 * dGamma) + 0.000907 * System.Math.Sin(2 * dGamma))
    End Function
    Private Function dRadToDeg(ByVal dAngleRad As Double) As Double
        dRadToDeg = 180 * dAngleRad / PI
    End Function
    Private Function CalcDayLength(ByVal dHourAngle As Double) As Double
        CalcDayLength = (2 * System.Math.Abs(Int(dRadToDeg(dHourAngle)))) / 15
    End Function
    Private Function dDegToRad(ByVal dAngleDeg As Double) As Double
        dDegToRad = PI * dAngleDeg / 180
    End Function
    Private Function CalcGamma2(ByVal iJulianDay As Integer, ByVal iHour As Integer) As Double
        CalcGamma2 = (2 * PI / 365) * (iJulianDay - 1 + (iHour \ 24))
    End Function

    ''' <summary>
    ''' 计算当前的天数
    ''' </summary>
    ''' <param name="iMonth">当前的月份</param>
    ''' <param name="iDay">月份中的天数</param>
    ''' <param name="bLeapYr">是否为闰年</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CalcJulianDay(ByVal iMonth As Integer, ByVal iDay As Integer, ByVal bLeapYr As Integer) As Integer
        Dim iJulDay As Integer = 0
        Dim i As Integer = 0
        If bLeapYr = True Then
            While i < iMonth - 1
                iJulDay += m_leapList(i)
                i += 1
            End While
        Else
            While i < iMonth - 1
                iJulDay += m_mothList(i)
                i += 1
            End While
        End If

        iJulDay += iDay
        CalcJulianDay = iJulDay

    End Function
    Private Function CalcHourAngle(ByVal dLat As Double, ByVal dSolarDec As Double, ByVal bTime As Boolean)
        Dim dLatRad As Double
        dLatRad = dDegToRad(dLat)
        If bTime = True Then
            CalcHourAngle = (System.Math.Acos(System.Math.Cos(dDegToRad(90.833)) / (System.Math.Cos(dLatRad) * System.Math.Cos(dSolarDec)) - System.Math.Tan(dLatRad) * System.Math.Tan(dSolarDec)))

        Else
            CalcHourAngle = -(System.Math.Acos(System.Math.Cos(dDegToRad(90.833)) / (System.Math.Cos(dLatRad) * System.Math.Cos(dSolarDec)) - System.Math.Tan(dLatRad) * System.Math.Tan(dSolarDec)))
        End If

    End Function
   

    Private Function calcSunriseGMT(ByVal iJulDay As Integer, ByVal dLatitude As Double, ByVal dLongitude As Double)
        Dim gamma As Double
        Dim eqTime As Double
        Dim solarDec As Double
        Dim hourAngle As Double
        Dim delta As Double
        Dim timeDiff As Double
        Dim timeGMT As Double
        Dim gamma_sunrise As Double

        gamma = CalcGamma(iJulDay)
        eqTime = CalcEqofTime(gamma)
        solarDec = CalcSolarDec(gamma)
        hourAngle = CalcHourAngle(dLatitude, solarDec, True)
        delta = dLongitude - dRadToDeg(hourAngle)
        timeDiff = 4 * delta
        timeGMT = 720 + timeDiff - eqTime
        gamma_sunrise = CalcGamma2(iJulDay, Int(timeGMT / 60))

        eqTime = CalcEqofTime(gamma_sunrise)
        solarDec = CalcSolarDec(gamma_sunrise)
        hourAngle = CalcHourAngle(dLatitude, solarDec, 1)
        delta = dLongitude - dRadToDeg(hourAngle)
        timeDiff = 4 * delta
        timeGMT = 720 + timeDiff - eqTime
        calcSunriseGMT = timeGMT


    End Function
    Private Function IsInteger(ByVal dValue As Double) As Boolean
        Dim iTemp As Integer
        Dim dTemp As Double
        iTemp = Int(dValue)
        dTemp = dValue - iTemp
        If dTemp = 0 Then
            IsInteger = True
        Else
            IsInteger = False
        End If
    End Function

    Private Function findRecentSunrise(ByVal iJulDay As Integer, ByVal dLatitude As Double, ByVal dLongitude As Double) As Double
        Dim jday As Integer
        Dim dTime As Double

        jday = iJulDay
        dTime = calcSunriseGMT(jday, dLatitude, dLongitude)
        While IsInteger(dTime) = False
            jday -= 1
            If jday < 1 Then
                jday = 365
                dTime = calcSunriseGMT(jday, dLatitude, dLongitude)
            End If
        End While
        findRecentSunrise = jday
    End Function

    Private Function findNextSunrise(ByVal iJulDay As Integer, ByVal dLatitude As Double, ByVal dLongitude As Double) As Double
        Dim jday As Integer
        Dim dTime As Double
        jday = iJulDay
        dTime = calcSunriseGMT(jday, dLatitude, dLongitude)
        While IsInteger(dTime) = False
            jday += 1
            If jday > 366 Then
                jday = 1

            End If
            dTime = calcSunriseGMT(jday, dLatitude, dLongitude)
        End While
        findNextSunrise = jday
    End Function
   
    Public Function GetSunrise(ByVal dLat As Double, ByVal dLon As Double, ByVal year As Integer, ByVal month As Integer, ByVal day As Integer) As DateTime

        Dim bLeap As Boolean '是否为闰年
        Dim iJulianDay As Integer
        Dim timeGMT As Double
        bLeap = IsLeapYear(year)
        iJulianDay = CalcJulianDay(month, day, bLeap)
        timeGMT = calcSunriseGMT(iJulianDay, dLat, dLon)

        ' if Northern hemisphere and spring or summer, use last sunrise and next sunset
        If ((dLat > 66.4) And (iJulianDay > 79) And (iJulianDay < 267)) Then
            timeGMT = findRecentSunrise(iJulianDay, dLat, dLon)
        Else
            ' if Northern hemisphere and fall or winter, use next sunrise and last sunset
            If ((dLat > 66.4) And ((iJulianDay < 83) Or (iJulianDay > 263))) Then
                timeGMT = findNextSunrise(iJulianDay, dLat, dLon)
            Else
                ' if Southern hemisphere and fall or winter, use last sunrise and next sunset
                If ((dLat < -66.4) And ((iJulianDay < 83) Or (iJulianDay > 263))) Then
                    timeGMT = findRecentSunrise(iJulianDay, dLat, dLon)
                Else
                    'if Southern hemisphere and spring or summer, use next sunrise and last sunset
                    If ((dLat < -66.4) And (iJulianDay > 79) And (iJulianDay < 267)) Then
                        timeGMT = findNextSunrise(iJulianDay, dLat, dLon)
                    End If
                End If
            End If

        End If

        timeGMT += (120 - dLon) / 360.0 * 48 * 60 '此时间为东8区120E上的日出、日落时间，其他经度需要校正
        Dim dHour As Double = timeGMT / 60
        Dim iHour As Integer = Int(dHour)
        Dim dMinute As Double = 60 * (dHour - iHour)
        Dim iMinute As Integer = Int(dMinute)
        Dim dSecond As Double = 60 * (dMinute - iMinute)
        Dim iSecond As Integer = Int(dSecond)
        Dim ret As New System.DateTime
        Dim timestring As String
        timestring = year.ToString & "-" & month.ToString & "-" & day.ToString & " " & (iHour - 8).ToString & ":" & iMinute.ToString & ":" & iSecond.ToString
        ret = System.Convert.ToDateTime(timestring)
        GetSunrise = ret
    End Function

    Private Function calcSunsetGMT(ByVal iJulDay As Integer, ByVal dLatitude As Double, ByVal dLongitude As Double) As Double
        'First calculates sunrise and approx length of day
        Dim dGamma As Double
        Dim eqTime As Double
        Dim solarDec As Double
        Dim hourAngle As Double
        Dim delta As Double
        Dim timeDiff As Double
        Dim setTimeGMT As Double

        dGamma = CalcGamma(iJulDay + 1)
        eqTime = CalcEqofTime(dGamma)
        solarDec = CalcSolarDec(dGamma)
        hourAngle = CalcHourAngle(dLatitude, solarDec, 0)
        delta = dLongitude - dRadToDeg(hourAngle)
        timeDiff = 4 * delta
        setTimeGMT = 720 + timeDiff - eqTime
        'first pass used to include fractional day in gamma calc
        Dim gamma_sunset As Double
        gamma_sunset = CalcGamma2(iJulDay, Int(setTimeGMT / 60))
        eqTime = CalcEqofTime(gamma_sunset)
        solarDec = CalcSolarDec(gamma_sunset)
        hourAngle = CalcHourAngle(dLatitude, solarDec, False)
        delta = dLongitude - dRadToDeg(hourAngle)
        timeDiff = 4 * delta
        setTimeGMT = 720 + timeDiff - eqTime
        calcSunsetGMT = setTimeGMT

    End Function
    Private Function findRecentSunset(ByVal iJulDay As Integer, ByVal dLatitude As Double, ByVal dLongitude As Double) As Double
        Dim jday As Integer
        Dim dTime As Double
        jday = iJulDay
        dTime = calcSunsetGMT(jday, dLatitude, dLongitude)

        While IsInteger(dTime) = False
            jday -= 1
            If jday < 1 Then
                jday = 365

            End If
            dTime = calcSunsetGMT(jday, dLatitude, dLongitude)
        End While
        findRecentSunset = jday
    End Function
    Private Function findNextSunset(ByVal iJulDay As Integer, ByVal dLatitude As Double, ByVal dLongitude As Double) As Double
        Dim jday As Integer
        Dim dTime As Double
        jday = iJulDay
        dTime = calcSunriseGMT(jday, dLatitude, dLongitude)
        While IsInteger(dTime) = False
            jday += 1
            If jday > 366 Then
                jday = 1
            End If
            dTime = calcSunriseGMT(jday, dLatitude, dLongitude)
        End While
        findNextSunset = jday
    End Function

    Public Function GetSunset(ByVal dLat As Double, ByVal dLon As Double, ByVal year As Integer, ByVal month As Integer, ByVal day As Integer) As DateTime
        Dim bLeap As Boolean
        Dim iJulianDay As Integer
        Dim timeGMT As Double

        bLeap = IsLeapYear(year)
        iJulianDay = CalcJulianDay(Int(month), Int(Day), bLeap)
        timeGMT = calcSunsetGMT(iJulianDay, dLat, dLon)
        ' if Northern hemisphere and spring or summer, use last sunrise and next sunset
        If ((dLat > 66.4) And (iJulianDay > 79) And (iJulianDay < 267)) Then
            timeGMT = findRecentSunset(iJulianDay, dLat, dLon)
            ' if Northern hemisphere and fall or winter, use next sunrise and last sunset
        Else
            If ((dLat > 66.4) And ((iJulianDay < 83) Or (iJulianDay > 263))) Then
                timeGMT = findNextSunset(iJulianDay, dLat, dLon)

            Else
                ' if Southern hemisphere and fall or winter, use last sunrise and next sunset
                If ((dLat < -66.4) And ((iJulianDay < 83) Or (iJulianDay > 263))) Then
                    timeGMT = findRecentSunset(iJulianDay, dLat, dLon)
                    'if Southern hemisphere and spring or summer, use next sunrise and last sunset
                Else
                    If ((dLat < -66.4) And (iJulianDay > 79) And (iJulianDay < 267)) Then
                        timeGMT = findNextSunset(iJulianDay, dLat, dLon)
                    End If

                End If

            End If

        End If
        timeGMT += (120 - dLon) / 360.0 * 48 * 60 '此时间为东8区120E上的日出、日落时间，其他经度需要校正
        Dim dHour As Double = timeGMT / 60
        Dim iHour As Integer = Int(dHour)
        Dim dMinute As Double = 60 * (dHour - iHour)
        Dim iMinute As Integer = Int(dMinute)
        Dim dSecond As Double = 60 * (dMinute - iMinute)
        Dim iSecond As Integer = Int(dSecond)

        Dim ret As New DateTime
        Dim timestring As String
        timestring = year.ToString & "-" & month.ToString & "-" & day.ToString & " " & (iHour - 8).ToString & ":" & iMinute.ToString & ":" & iSecond.ToString
        ret = System.Convert.ToDateTime(timestring)
        GetSunset = ret

    End Function

End Class
