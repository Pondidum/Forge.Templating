Imports System.Text.RegularExpressions
Imports Forge.Templating.Extensions.CharArray

Public Class SimpleSearchStrategy
    Implements ISearchStrategy

    Private _template() As Char
    Private _replacements As IList(Of IReplacementSource)

    '(?ixm)
    '\{
    '    (.*?)
    '\}
    Private Const RegexTag As String = "(?ixm)\{(.*?)\}"

    Public Sub New()
        _replacements = New List(Of IReplacementSource)
    End Sub

    Public WriteOnly Property Template As Char() Implements ISearchStrategy.Template
        Set(ByVal value As Char())
            _template = value
        End Set
    End Property

    Public Property Replacements As IList(Of IReplacementSource) Implements ISearchStrategy.Replacements
        Get
            Return _replacements
        End Get
        Set(ByVal value As IList(Of IReplacementSource))
            _replacements = value
        End Set
    End Property

    Public Function Parse() As String Implements ISearchStrategy.Parse

        If _template Is Nothing OrElse _template.Length = 0 Then
            Return String.Empty
        End If

        Dim sb As New StringBuilder()
        Dim rx As New Regex(RegexTag)

        Dim index As Integer = 0

        For Each m As Match In rx.Matches(_template)

            Dim replacement As String = FindReplacement(m.Groups(0).Value)

            sb.Append(_template.Range(index, m.Index - index))

            If Not String.IsNullOrWhiteSpace(replacement) Then

                sb.Append(replacement)

            End If

            index = m.Index + m.Length

        Next

        sb.Append(_template.Range(index, _template.Length - index))

        Return sb.ToString

    End Function


    Private Function FindReplacement(ByVal key As String) As String

        key = key.Replace("{", "")
        key = key.Replace("}", "")

        For Each rs As IReplacementSource In _replacements

            If rs.HasValue(key) Then
                Return rs.GetValue(key)
            End If

        Next

        Return String.Empty

    End Function



End Class