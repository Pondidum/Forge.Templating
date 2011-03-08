Imports Forge.Templating.Interfaces
Imports Forge.Templating.SearchStrategies.Token
Namespace SearchStrategies

    Public Class TokenSearchStrategy
        Implements ISearchStrategy

        Private _template() As Char
        Private _replacements As IList(Of IReplacementSource)
        Private _tree As Tag

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

        Public Sub Setup() Implements ISearchStrategy.Setup

            Dim parser = New TemplateParser(_template)
            Dim tree = parser.Process()

            _tree = tree

        End Sub

        Public Function Parse() As String Implements ISearchStrategy.Parse
            
            Dim result = _tree.Render(_replacements)

            Return New String(result)

        End Function


    End Class


End Namespace
