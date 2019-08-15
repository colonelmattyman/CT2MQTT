Public Class TempLoad
    Private Temp(10, 256, 10) As Integer
    Private Load(10, 256, 10) As Integer
    Private Counter As Integer
    Private Divisor As Integer

    Public Sub New()
        Divisor = 0
        Counter = 0
    End Sub

    Public Sub AddTempLoad(ByVal CPUNumber As Integer, CoreNumber As Integer, TempIn As Integer, LoadIn As Integer)

        For i = 0 To 9
            Temp(CPUNumber, CoreNumber, 10 - i) = Temp(CPUNumber, CoreNumber, 9 - i)
            Load(CPUNumber, CoreNumber, 10 - i) = Load(CPUNumber, CoreNumber, 9 - i)
        Next

        Temp(CPUNumber, CoreNumber, 0) = TempIn
        Load(CPUNumber, CoreNumber, 0) = LoadIn
        If Divisor < 10 Then
            Divisor = Divisor + 1
        End If
    End Sub

    Public Function GetTempAverage(ByVal CPUNumber As Integer, CoreNumber As Integer) As Double
        Dim TotalTemp As Integer
        Dim i As Integer
        Dim List As String

        List = "CPU" & CPUNumber & ", CORE" & CoreNumber
        TotalTemp = 0

        For i = 1 To Divisor
            TotalTemp = TotalTemp + Temp(CPUNumber, CoreNumber, i - 1)
            List = List & ", " & Temp(CPUNumber, CoreNumber, i - 1)
        Next

        List = List & vbCrLf
        Console.WriteLine(List)

        Return TotalTemp / Divisor
    End Function


    Public Function GetLoadAverage(ByVal CPUNumber As Integer, CoreNumber As Integer) As Double
        Dim TotalLoad As Integer
        Dim i As Integer
        Dim List As String

        List = "CPU" & CPUNumber & ", CORE" & CoreNumber
        TotalLoad = 0

        For i = 1 To Divisor
            TotalLoad = TotalLoad + Load(CPUNumber, CoreNumber, i - 1)
            List = List & ", " & Load(CPUNumber, CoreNumber, i - 1)
        Next

        List = List & vbCrLf
        Console.WriteLine(List)
        Return TotalLoad / Divisor
    End Function

    Public Function GetTempCurrent(ByVal CPUNumber As Integer, CoreNumber As Integer) As Double

        Return Temp(CPUNumber, CoreNumber, 0)
    End Function

    Public Function GetLoadCurrent(ByVal CPUNumber As Integer, CoreNumber As Integer) As Double

        Return Load(CPUNumber, CoreNumber, 0)
    End Function

End Class