Imports System
Imports System.IO
Imports System.Text
Imports System.Text.Json
Imports System.Runtime.Serialization.Json

Public Class SerializationManager(Of t As Class)


    Public Shared Function ReadFromJsonFile(ByVal filePath As String) As t
        Dim jsonString As String = File.ReadAllText(filePath)
        Dim obj As t = JsonSerializer.Deserialize(Of t)(jsonString)
        Return obj
    End Function

    Public Shared Sub WriteToJsonFile(ByVal filePath As String, ByVal objectToWrite As t, Optional Indented As Boolean = True)
        Dim options As New JsonSerializerOptions With {.WriteIndented = Indented}
        Dim jsonString As String = JsonSerializer.Serialize(objectToWrite, options)
        File.WriteAllText(filePath, jsonString)
    End Sub

End Class
