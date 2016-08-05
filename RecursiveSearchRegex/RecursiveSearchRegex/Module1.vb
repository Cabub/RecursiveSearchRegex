Imports System.IO
Imports System.Text.RegularExpressions
Module Module1

    Private SearchRoot As String
    Private FileRegex As String
    Private SearchRegex As String

    Sub Main(ByVal Args() As String)
        If Args.Count < 3 Then
            help()
            Return
        End If

        SearchRoot = Args(0)
        FileRegex = Args(1)
        SearchRegex = Args(2)

        If Not Directory.Exists(SearchRoot) Then
            Console.WriteLine("Root folder doesn't exist")
            Return
        End If

        Dim rootDir As New DirectoryInfo(SearchRoot)

        RecursiveRegexSearch(rootDir)

    End Sub

    Sub help()
        Console.WriteLine("RecursiveSerchRegex <root> <fileregex> <searchregex>")
    End Sub

    Sub RecursiveRegexSearch(ByVal root As DirectoryInfo)
        Dim reg As New Regex(FileRegex)
        For Each rootFile As FileInfo In root.GetFiles
            If reg.IsMatch(rootFile.Name) Then
                SearchFile(rootFile)
            End If
        Next
        For Each rootDir As DirectoryInfo In root.GetDirectories
            RecursiveRegexSearch(rootDir)
        Next
    End Sub

    Sub SearchFile(ByVal file As FileInfo)
        Dim reg As New Regex(SearchRegex)
        Dim str As String
        Using read As New StreamReader(file.FullName)
            While Not read.EndOfStream
                str = read.ReadLine()
                If reg.IsMatch(str) Then
                    Console.WriteLine(file.FullName)
                    Exit Sub
                End If
            End While
        End Using
    End Sub


End Module
