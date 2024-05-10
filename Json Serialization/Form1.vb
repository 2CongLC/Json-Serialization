Imports System.IO

Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim db As New WeatherForecast With {
               .[Date] = DateTime.Parse("2019-08-01"),
               .TemperatureCelsius = 25,
               .Summary = "Hot",
               .SummaryField = "Hot",
               .DatesAvailable = New List(Of DateTimeOffset) From {
                   DateTime.Parse("2019-08-01"), DateTime.Parse("2019-08-02")
               },
               .TemperatureRanges = New Dictionary(Of String, HighLowTemps) From {
                   {"Cold", New HighLowTemps With {.High = 20, .Low = -10}},
                   {"Hot", New HighLowTemps With {.High = 60, .Low = 20}}
               },
               .SummaryWords = New String() {"Cool", "Windy", "Humid"}
           }

            If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                SerializationManager(Of WeatherForecast).WriteToJsonFile(SaveFileDialog1.FileName, db)
            End If

            MessageBox.Show("ok")

        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            If OpenFileDialog1.ShowDialog = DialogResult.OK Then

                Dim jsdata As WeatherForecast = SerializationManager(Of WeatherForecast).ReadFromJsonFile(OpenFileDialog1.FileName)

                TextBox1.Text = jsdata.Date.ToString()
                TextBox2.Text = jsdata.TemperatureCelsius
                TextBox3.Text = jsdata.Summary

                For Each s As HighLowTemps In jsdata.TemperatureRanges.Values

                    TextBox4.Text = s.High & "," & s.Low
                Next

                TextBox5.Text = jsdata.SummaryWords(0)

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            If OpenFileDialog1.ShowDialog = DialogResult.OK AndAlso SaveFileDialog1.ShowDialog = DialogResult.OK Then

                Dim jsonstring As String = File.ReadAllText(OpenFileDialog1.FileName)
                SerializationManager(Of String).WriteToJsonFile(SaveFileDialog1.FileName, jsonstring)
                MessageBox.Show("ok")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Try
            If OpenFileDialog1.ShowDialog = DialogResult.OK AndAlso SaveFileDialog1.ShowDialog = DialogResult.OK Then

                Dim jsonstring As String = SerializationManager(Of String).ReadFromJsonFile(OpenFileDialog1.FileName)
                File.WriteAllText(SaveFileDialog1.FileName, jsonstring)
                MessageBox.Show("ok")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
    End Sub
End Class
