﻿Imports System.IO
Imports System.Net
Imports System.Management
Imports System.Runtime.InteropServices
Imports System.Net.NetworkInformation

Public Class Desktop
    Private Function SpamRefresh(times As Integer)
        For tmp = 0 To times
            Me.Refresh()
        Next
        Return 0
    End Function

    Public Function ProcessKeys(e As KeyEventArgs)
        If e.KeyCode = Keys.F3 And My.Computer.Keyboard.ShiftKeyDown Then
            If My.Settings.Admin Then
                My.Settings.Admin = False
                AdminIcon.Hide()
            Else
                My.Settings.Admin = True
                AdminIcon.Show()
            End If
        ElseIf e.KeyCode = Keys.OemMinus And My.Computer.Keyboard.AltKeyDown And My.Computer.Keyboard.CtrlKeyDown Then
            FatalError.Show()
        ElseIf e.KeyCode = Keys.S And My.Computer.Keyboard.AltKeyDown Then
            DeskMenu.Show()
        End If
        Return 0
    End Function
    Private Sub Desktop_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AdminIcon.Hide()
        Timer1.Interval = 1
        Timer1.Enabled = True
        Timer1.Interval = 1000
        VersionLabel.Text = "NEX OS Public Build " + My.Application.Info.Version.ToString
        Try
            Dim Client As WebClient = New WebClient()
            Dim Reader As StreamReader = New StreamReader(Client.OpenRead("https://httpbin.org/get"))
        Catch ex As Exception
            NoInternet.Show()
            NoInternet.MoreInfo.Text = ex.ToString
        End Try
    End Sub

    Private Sub PictureBox5_Click(sender As Object, e As EventArgs) Handles PictureBox5.Click
        Settings.Show()
    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs)
        Internet.Show()
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Debug.Print("Modal started")
        Dim modalResult = ModalBox.ShowModal("Shutdown", "Are you sure that you want to close NEX OS?")
        ModalBox.Close()
        Debug.Print("Modal finished // " & modalResult.ToString)
        If modalResult = 1 Then
            Try
                DeskMenu.Close()
            Catch ex As Exception
                Debug.Print(ex.ToString)
            End Try
            Me.Close()
        End If
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        LoginForm.Show()
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        DeskMenu.Show()
    End Sub

    Private Sub PictureBox6_Click(sender As Object, e As EventArgs)
        Notes.Show()
    End Sub

    Private Sub PictureBox7_Click(sender As Object, e As EventArgs)
        Files.Show()
    End Sub

    Private Sub AdminIcon_Click(sender As Object, e As EventArgs) Handles AdminIcon.Click
        ModalBox.ShowModal("Administrator", "You're using the administrator account. To revert back to the normal user, press Shift+F3.", YesNoModal:=False)
        ModalBox.Close()
    End Sub

    Private Sub Me_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        ProcessKeys(e)
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim Time As String
        If Date.Now.Hour < 12 Then
            Time = Date.Now.ToString("HH:mm") & " AM"
        ElseIf Date.Today.Hour = 12 Then
            Time = Date.Now.ToString("12:mm") & " PM"
        Else
            Dim format = Date.Now.Hour - 12
            Time = format.ToString & Date.Now.ToString(":mm") & " PM"
        End If
        TimeLabel.Text = MonthName(Month(DateTime.Now)) & Date.Now.ToString(" dd, yyyy ") & Time
    End Sub
End Class