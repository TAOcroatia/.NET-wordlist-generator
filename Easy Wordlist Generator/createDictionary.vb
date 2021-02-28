Imports System.IO
#Disable Warning BC42105 ' Function doesn't return a value on all code paths
Public Class createDictionary
    Public Sub main()
        custom()
        numbers()
        letters()
        characters()
        list()
    End Sub
    Public Function IndexOf(str As String, character As Char, occ As Integer)
        Dim count As Integer = 0
        For i As Integer = 0 To str.Length - 1
            If str.Substring(i, 1) = character Then
                count = count + 1
            End If
            If str.Substring(i, 1) = character AndAlso occ = count Then
                Return i
            End If
        Next
    End Function
    Public Function CharCount(value As String, ch As Char)
        Dim cnt As Integer = 0
        For Each c As Char In value
            If c = ch Then
                cnt += 1
            End If
        Next
        Return cnt
    End Function
    Public Function qsplit(input As String)
        Dim count As Integer = 0
        Dim substring As New List(Of String)
        'Count Number of "'s
        For i As Integer = 0 To input.Count - 1
            If input.Substring(i, 1) = Chr(34) Then
                count = count + 1
            End If
        Next
        For i As Integer = 0 To count - 1 Step 2
            Dim halfi As Integer = i / 2
            Dim quotesfinder As Integer = IndexOf(input, Chr(34), i + 1)
            Dim quotesfinderend As Integer = IndexOf(input, Chr(34), i + 1 + 1)
            Dim betweenquotes As String = input.Substring(quotesfinder + 1, quotesfinderend - quotesfinder - 1)
            substring.Add(betweenquotes)
        Next
        Return substring
    End Function
    Public Function writeDictionary(text As String)
        Using fs As FileStream = New FileStream(Form1.SaveFileDialog1.FileName & ".tmp", FileMode.Append, FileAccess.Write)
            Using writereader As StreamWriter = New StreamWriter(fs)
                writereader.WriteLine(text)
            End Using
        End Using
    End Function
    'nece splita zarez i razmak zajedno
    Public Sub numbers()
        If Form1.CheckBox2.Checked Then
            Dim substring As New List(Of String)
            substring = qsplit(Form1.TextBox2.Text)
            For i As Integer = 0 To substring.Count - 1
                If substring(i).Contains(",") Then
                    Dim numbers() As String
                    numbers = substring(i).Split(", ")
                    For iii As Integer = 0 To numbers.Count - 1
                        writeDictionary(numbers(iii))
                    Next
                ElseIf substring(i).Contains("-") Then
                    Dim numb(1) As String
                    numb = substring(i).Split("-")
                    For ii As Integer = numb(0) To numb(1)
                        writeDictionary(ii)
                    Next
                Else
                    writeDictionary(substring(i))
                End If
            Next
        End If
    End Sub
    'npr. "A-F" izbaci "A-C"
    Public Sub letters()
        Dim upperalphabet As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        Dim loweralphabet As String = "abcdefghijklmnopqrstuvwxyz"
        If Form1.CheckBox3.Checked Then
            Dim substring As New List(Of String)
            substring = qsplit(Form1.TextBox3.Text)
            For i As Integer = 0 To substring.Count - 1
                If substring(i).Contains(",") Then
                    Dim letters() As String
                    letters = substring(i).Split(", ")
                    For iii As Integer = 0 To letters.Count - 1
                        writeDictionary(letters(iii).Substring(letters(iii).Length - 1))
                    Next
                ElseIf substring(i).Contains("-") Then
                    Dim lett(1) As String
                    lett = substring(i).Split("-")

                    If lett(0) = lett(0).ToUpper Then
                        Dim a As Integer = substring(i).IndexOf(lett(0))
                        Dim b As Integer = substring(i).IndexOf(lett(1))
                        For ii As Integer = a To b
                            writeDictionary(upperalphabet.Substring(ii, 1))
                        Next
                    ElseIf lett(0) = lett(0).ToLower Then
                        Dim a As Integer = IndexOf(loweralphabet, lett(0), 1)
                        Dim b As Integer = IndexOf(loweralphabet, lett(1), 1)
                        For ii As Integer = a To b
                            writeDictionary(loweralphabet.Substring(ii, 1))
                        Next
                    End If
                Else
                    writeDictionary(substring(i))
                End If
            Next
        End If

    End Sub
    Public Sub characters()
        If Form1.CheckBox4.Checked = True Then
            For i As Integer = 0 To Form1.CheckBox4.Text.Count - 1 Step 2
                writeDictionary(Form1.CheckBox4.Text.Substring(i, 1))
            Next
        End If
        If Form1.CheckBox5.Checked = True Then
            For i As Integer = 0 To Form1.TextBox4.Text.Count Step 2
                writeDictionary(Form1.TextBox4.Text.Substring(i, 1))
            Next
        End If
    End Sub
    Public Sub list()
        If Form1.CheckBox1.Checked Then
            For i As Integer = 0 To Form1.ListBox1.Items.Count - 1
                writeDictionary(Form1.ListBox1.Items(i))
            Next
        End If
    End Sub
    Public Sub custom()
        If Form1.Button1.BackColor = Color.Green Then
            File.WriteAllLines(Form1.SaveFileDialog1.FileName & ".tmp", File.ReadAllLines(Form1.OpenFileDialog1.FileName))
        End If
    End Sub
End Class
