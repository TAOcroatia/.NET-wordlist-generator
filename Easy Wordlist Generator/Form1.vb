Imports System.IO
Public Class Form1
    Dim createDictionary As New createDictionary
    Dim createWordlist As New createWordlist
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            Button1.BackColor = Color.Green
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        OpenFileDialog1.Reset()
        Button1.BackColor = DefaultBackColor
    End Sub

    Private Sub TextBox5_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox5.KeyDown
        If e.KeyCode = Keys.Enter Then
            ListBox1.Items.Add(TextBox5.Text)
            TextBox5.Clear()
        End If
    End Sub

    Private Sub ListBox1_DoubleClick(sender As Object, e As EventArgs) Handles ListBox1.DoubleClick
        If ListBox1.SelectedItems.Count = 1 Then
            ListBox1.Items.Remove(ListBox1.SelectedItem)
        End If
    End Sub


    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If Not SaveFileDialog1.FileName.Length < 1 Then
            Try
                File.Delete(SaveFileDialog1.FileName & ".tmp")
                File.Delete(SaveFileDialog1.FileName)
            Catch
            End Try
            createDictionary.main()
            createWordlist.main()

        Else
            MsgBox("Please select file location")
        End If
    End Sub


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If SaveFileDialog1.ShowDialog() = DialogResult.OK Then
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) 
        createDictionary.main()
    End Sub

    Private Sub Worker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs)
    End Sub
End Class
