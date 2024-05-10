Public Class WeatherForecast
    Public Property [Date] As DateTimeOffset
    Public Property TemperatureCelsius As Integer
    Public Property Summary As String
    Public SummaryField As String
    Public Property DatesAvailable As IList(Of DateTimeOffset)
    Public Property TemperatureRanges As Dictionary(Of String, HighLowTemps)
    Public Property SummaryWords As String()
End Class

Public Class HighLowTemps
    Public Property High As Integer
    Public Property Low As Integer
End Class