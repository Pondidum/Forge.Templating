Imports System.Runtime.CompilerServices

Namespace Extensions

    Public Module CharArray

        <Extension()>
        Public Function Range(ByVal source() As Char, ByVal startIndex As Integer, ByVal length As Integer) As Char()

            If source Is Nothing Then Throw New ArgumentNullException("source", "cannot be null")

            If startIndex < 0 Then Throw New ArgumentOutOfRangeException("startIndex", "must be 0 or more")
            If startIndex >= source.Length Then Throw New ArgumentOutOfRangeException("startIndex", "must be less than the input length")

            If length < 0 Then Throw New ArgumentOutOfRangeException("length", "must be 0 or more")
            If length > source.Length Then Throw New ArgumentOutOfRangeException("length", "cannot be longer than input length")

            If startIndex + length > source.Length Then Throw New ArgumentOutOfRangeException("startIndex+length", "must be less than input length")

            Dim result(length - 1) As Char
            Array.Copy(source, startIndex, result, 0, length)

            Return result

        End Function

        <Extension()>
        Public Function IndexOfWord(ByVal haystack() As Char, ByVal needle() As Char) As Integer

            If haystack Is Nothing Then Throw New ArgumentNullException("haystack", "cannot be null")
            If needle Is Nothing Then Throw New ArgumentNullException("needle", "cannot be null")

            Dim n As Integer = haystack.Length
            Dim m As Integer = needle.Length

            If m > n Then
                Return -1
            End If

            '// pre processing
            Dim d As Integer = 1 << (m - 1)
            Dim hh As Integer = 0
            Dim hn As Integer = 0

            For i As Integer = 0 To m - 1
                hh = ((hh << 1) + AscW(haystack(i)))
                hn = ((hn << 1) + AscW(needle(i)))
            Next

            '//searching
            Dim j As Integer = 0

            While j <= n - m

                Dim temp(m - 1) As Char
                Array.Copy(haystack, j, temp, 0, m)

                If hh = hn AndAlso temp = needle Then
                    Return j
                End If

                If j = n - m Then
                    Return -1
                End If

                '//rehashing
                hh = ((hh - AscW(haystack(j)) * d) << 1) + AscW(haystack(j + m))
                j += 1

            End While

            Return -1

        End Function

        <Extension()>
        Public Function AsString(ByVal chars() As Char) As String

            Return New String(chars)

        End Function

    End Module

End Namespace