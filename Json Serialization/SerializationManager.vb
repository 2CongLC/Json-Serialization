Imports System
Imports System.IO
Imports System.Text
Imports System.Text.Json
Imports System.Runtime.Serialization.Json

Public Class SerializationManager(Of t As Class)


    Public Shared Function ReadFromJsonFile(ByVal filePath As String) As t

        Try

            Dim jsonString As String = File.ReadAllText(filePath)
            Dim obj As t = JsonSerializer.Deserialize(Of t)(jsonString)
            Return obj

        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try

    End Function

    Public Shared Sub WriteToJsonFile(ByVal filePath As String, ByVal objectToWrite As t, Optional Indented As Boolean = True)
        Try

            Dim options As New JsonSerializerOptions With {.WriteIndented = Indented}
            Dim jsonString As String = JsonSerializer.Serialize(objectToWrite, options)
            File.WriteAllText(filePath, jsonString)

        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
    End Sub

Public Shared Function JsonSerialization(ByVal value As t, Optional Indented As Boolean = True) as String
        Try

            Dim options As New JsonSerializerOptions With {.WriteIndented = Indented}
            Dim jsonString As String = JsonSerializer.Serialize(value, options)
            Return jsonString

        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
    End Function

Public Shared Function JsonDeSerialization(ByVal value As String) As t

        Try

            Dim jsonString As String = value
            Dim obj As t = JsonSerializer.Deserialize(Of t)(jsonString)
            Return obj

        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try

    End Function
            
End Class
