Imports Forge.Templating.Interfaces
Imports Forge.Templating.SearchStrategies.Token
Namespace SearchStrategies

    Public Class TokenSearchStrategy
        Implements ISearchStrategy

        Private _template() As Char
        Private _replacements As IList(Of IReplacementSource)

        Public Sub New()
            _replacements = New List(Of IReplacementSource)
        End Sub

        Public WriteOnly Property Template() As Char() Implements ISearchStrategy.Template
            Set(ByVal value As Char())
                _template = value
            End Set
        End Property

        Public Property Replacements() As IList(Of IReplacementSource) Implements ISearchStrategy.Replacements
            Get
                Return _replacements
            End Get
            Set(ByVal value As IList(Of IReplacementSource))
                _replacements = value
            End Set
        End Property

        Public Function Parse() As String Implements ISearchStrategy.Parse

            Dim parser = New TemplateParser(_template)
            parser.AddTags(New ValueTag(_replacements))
            parser.AddTags(New ForLoopTag(_replacements))

            Dim tree = parser.Process()


        End Function

        'Private Function CreateOutput(ByVal current As MatchData) As Char()

        '    Dim result = New List(Of Char)

        '    If current.Tag.Type = Tag.TagTypes.Content Then
        '        result.AddRange(current.Value)
        '    End If

        'End Function

    End Class


End Namespace
