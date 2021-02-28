Imports System.IO
#Disable Warning BC42105 ' Function doesn't return a value on all code paths
Public Class createWordlist
    Dim lowlenght As Integer
    Dim highlenght As Integer
    Dim pos As Integer = 0
    Public Sub main()
        Dim lenght() As String = Form1.TextBox6.Text.Split("-")
        lowlenght = lenght(0) ' minimalna duljina čuda
        highlenght = lenght(1) + 1 'maksimalna duljina čuda
        Console.WriteLine(lowlenght)
        Console.WriteLine(highlenght)
        makeWordlist()
    End Sub
    Public Function readDictionary(line As Integer)
        Using openReader As StreamReader = New StreamReader(Form1.SaveFileDialog1.FileName & ".tmp")
            For i As Integer = 0 To line - 1
                openReader.ReadLine()
            Next
            Dim finalline As String = openReader.ReadLine
            Return (finalline)
        End Using
    End Function
    Public Function writeWordlist(text As String)
        Using fs As FileStream = New FileStream(Form1.SaveFileDialog1.FileName, FileMode.Append, FileAccess.Write)
            Using writereader As StreamWriter = New StreamWriter(fs)
                writereader.WriteLine(text)
            End Using
        End Using
    End Function
    Public Sub makeWordlist()
        Dim count(highlenght) As Integer
        Dim lineCount = File.ReadAllLines(Form1.SaveFileDialog1.FileName & ".tmp").Length 'broj linija u riječniku
        For i As Integer = 0 To lowlenght
            count(i) = -1
        Next
        For i As Integer = highlenght - lowlenght To highlenght
            count(i) = 0
        Next
        count(highlenght) = 0
        Dim done As Boolean = False
        While count(0) = -1
            For i As Integer = highlenght - 1 To 0 Step -1
                If count(i) = lineCount AndAlso i <> 0 Then
                    count(i - 1) = count(i - 1) + 1
                    For ii As Integer = i To highlenght
                        count(i) = 0
                    Next
                End If
            Next
            For i As Integer = highlenght - 1 To 0 Step -1
                If count(i) = -1 Then
                    pos = i + 1
                    i = 0
                End If
            Next
            'start koda

            Dim output As String = ""
            Dim dont As Boolean = False
            For i As Integer = pos To highlenght - 1 Step 0
                Dim line As String = readDictionary(count(i))
                If Not line.Length + i - 1 > highlenght - 1 Then
                    output = output + line
                    If line.Length > 1 Then
                        For ii As Integer = i + 1 To i + line.Length - 1
                            count(ii) = lineCount - 1
                        Next
                    End If
                Else
                    dont = True
                End If
                i = i + line.Length
            Next
            If dont = False Then writeWordlist(output)
            Dim progress As String = " "
            Dim lenght As Integer
            If count.Count < 3 Then
                lenght = count.Count
            Else
                lenght = 3
            End If
            If Not count(1) = 0 Then
                progress = count(1) + 1 & "/" & lineCount
                Form1.Label1.Text = progress
                Form1.Label1.Refresh()
            End If
            If Not count(2) = 0 Then
                progress = count(2) + 1 & "/" & lineCount
                Form1.Label3.Text = progress
                Form1.Label3.Refresh()
            End If
            count(highlenght - 1) = count(highlenght - 1) + 1
        End While
        Form1.Label1.Text = "Made by"
        Form1.Label3.Text = "TAO_Croatia"
    End Sub
    Public Sub updateprogress()

    End Sub
End Class
