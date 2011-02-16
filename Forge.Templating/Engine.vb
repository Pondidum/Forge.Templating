Imports Forge.Templating.Interfaces

Public Class Engine

    Private ReadOnly _strategy As ISearchStrategy

    Private _template As Char()

    Public Sub New(ByVal strategy As ISearchStrategy)
        _strategy = strategy
    End Sub

    Public WriteOnly Property Template As String
        Set(ByVal value As String)
            _template = value.ToArray
        End Set
    End Property

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