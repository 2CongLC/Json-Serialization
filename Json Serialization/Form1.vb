﻿Imports System.IO
Imports System.Text
Imports System.Text.Json

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

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Try
            If OpenFileDialog1.ShowDialog = DialogResult.OK AndAlso SaveFileDialog1.ShowDialog = DialogResult.OK Then

                Dim jsonstring As String = File.ReadAllText(OpenFileDialog1.FileName)
                Dim result As String = SerializeJson(Of String)(IO.File.ReadAllText(OpenFileDialog1.FileName))
                IO.File.WriteAllText(SaveFileDialog1.FileName, result)
                MessageBox.Show("ok")

            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Try
            If OpenFileDialog1.ShowDialog = DialogResult.OK AndAlso SaveFileDialog1.ShowDialog = DialogResult.OK Then

                Dim jsonstring As String = File.ReadAllText(OpenFileDialog1.FileName)
                Dim result As String = DeSerializeJson(Of String)(IO.File.ReadAllText(OpenFileDialog1.FileName))
                IO.File.WriteAllText(SaveFileDialog1.FileName, result)
                MessageBox.Show("ok")

            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
    End Sub

    Public Function SerializeJson(Of T)(ByVal value As String, Optional Indented As Boolean = True) As String
        Dim obj As String = value
        Dim options As New JsonSerializerOptions With {.WriteIndented = Indented}
        Dim result As String = JsonSerializer.Serialize(obj, options)
        Return result
    End Function


    Public Function DeSerializeJson(Of T)(ByVal value As String) As T
        Dim result As T = JsonSerializer.Deserialize(Of T)(value)
        Return result
    End Function
End Class
