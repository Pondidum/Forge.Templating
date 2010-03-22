Imports System.Text.RegularExpressions
Imports Forge.Templating.Extensions.CharArray

Public Class LoopingSearchStrategy
    Implements ISearchStrategy

    '(?ixms)
    '\{\!
    '    (?:\s)?
    '    (?:foreach)
    '    (?:\s)+
    '    (?<current>.*)
    '    (?:\s)+
    '    (?:in)
    '    (?:\s)+
    '    (?<collection>.*?)
    '\}
    '(?<content>.*?)
    '\{\!end}
    Private Const RegexForLoop As String = "(?ixms)\{\!(?:\s)?(?:foreach)(?:\s)+(?<current>.*)(?:\s)+(?:in)(?:\s)+(?<collection>.*?)\}(?<content>.*?)\{\!end}"

    Private _template() As Char
    Private _replacements As IList(Of IReplacementSource)

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

        Dim sb As New Text.StringBuilder
        Dim index As Integer = 0

        For Each m As Match In Regex.Matches(New String(_template), RegexForLoop, RegexOptions.Singleline)
            
            sb.Append(_template.Range(index, m.Index - index))

            Dim loopParts As New ForLoopParts(m)

            Dim strat As New StandardSearchStrategy
            strat.Template = loopParts.Content

            Dim source As IReplacementSource = SourceFromName(loopParts.Source, loopParts.Collection)

            If source IsNot Nothing Then

                Dim collection As IEnumerable = source.GetCollection(loopParts.Collection)

                For Each item In collection

                    strat.Replacements.Clear()
                    strat.Replacements.Add(New ReflectionReplacementSource(item))
                    sb.Append(strat.Parse)

                Next

            End If

            index = m.Index + m.Length

        Next

        If index < _template.Length AndAlso _template.Length - index > 0 Then
            sb.Append(_template.Range(index, _template.Length - index))
        End If

        Return sb.ToString

    End Function

    Private Function SourceFromName(ByVal sourceName As String, ByVal collectionName As String) As IReplacementSource

        If String.IsNullOrWhiteSpace(collectionName) Then
            Return Nothing
        End If

        If Not String.IsNullOrWhiteSpace(sourceName) Then

            Return (From irs As IReplacementSource
                    In _replacements
                    Where sourceName.Equals(irs.Name, StringComparison.OrdinalIgnoreCase) And irs.HasCollection(collectionName)
                    Select irs).FirstOrDefault

        Else

            Return (From irs As IReplacementSource
                    In _replacements
                    Where irs.HasCollection(collectionName)
                    Select irs).FirstOrDefault

        End If


    End Function

    Private Class ForLoopParts

        Property Variable As String
        Property Collection As String
        Property Source As String
        Property Content As Char()

        Public Sub New(ByVal match As Match)

            Variable = match.Groups("current").Value.Trim
            Content = match.Groups("content").Value.ToCharArray
            Content = Content.Range(2, Content.Length - 4) 'the first 2 chars and last 2 chars are newlines from the regex

            Dim col As String = match.Groups("collection").Value

            If col.Contains(".") Then
                Source = col.Substring(0, col.IndexOf("."))
                Collection = col.Substring(col.IndexOf(".") + 1)
            Else
                Source = String.Empty
                Collection = col
            End If

        End Sub

    End Class

End Class
