Imports System.Runtime.CompilerServices

Namespace Extensions

    Module StringExtensions

        <Extension()>
        Public Function Range(ByVal source As String, ByVal startIndex As Integer, ByVal length As Integer) As String

            If source Is Nothing Then Throw New ArgumentNullException("source", "cannot be null")

            If startIndex < 0 Then Throw New ArgumentOutOfRangeException("startIndex", "must be 0 or more")
            If startIndex >= source.Length Then Throw New ArgumentOutOfRangeException("startIndex", "must be less than the input length")

            If length < 0 Then Throw New ArgumentOutOfRangeException("length", "must be 0 or more")
            If length > source.Length Then Throw New ArgumentOutOfRangeException("length", "cannot be longer than input length")

            If startIndex + length > source.Length Then Throw New ArgumentOutOfRangeException("startIndex+length", "must be less than input length")

            Return source.Substring(startIndex, length)

        End Function

    End Module

End Namespace
