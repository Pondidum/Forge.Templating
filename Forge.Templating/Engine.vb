Imports Forge.Templating.Interfaces

Public Class Engine

    Private ReadOnly _strategy As ISearchStrategy

    Private _template As Char()

    Public Sub New(ByVal strategy As ISearchStrategy)
        _strategy = strategy
    End Sub

    Public Sub SetTemplate(ByVal template() As Char)
        _template = template
        _strategy.Setup()
    End Sub

    Public Function Parse(ByVal replacementSource As IReplacementSource) As String

        Dim replacements As New List(Of IReplacementSource)
        replacements.Add(replacementSource)

        Return Parse(replacements)

    End Function

    Public Function Parse(ByVal replacementSources As IList(Of IReplacementSource)) As String

        _strategy.Template = _template
        _strategy.Replacements = replacementSources

        Return _strategy.Parse

    End Function

End Class