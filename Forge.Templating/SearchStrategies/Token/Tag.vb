Imports Forge.Templating.Interfaces

Namespace SearchStrategies.Token

    Friend Class Tag

        Private ReadOnly _pattern As String
        Private ReadOnly _type As TagTypes
        Private ReadOnly _replacements As IList(Of IReplacementSource)

        Public Sub New(ByVal replacements As IList(Of IReplacementSource), ByVal type As TagTypes, ByVal pattern As String)
            _type = type
            _pattern = pattern
            _replacements = replacements
        End Sub

        Public ReadOnly Property Replacements As IList(Of IReplacementSource)
            Get
                Return _replacements
            End Get
        End Property

        Public ReadOnly Property Pattern() As String
            Get
                Return _pattern
            End Get
        End Property

        Public ReadOnly Property Type() As TagTypes
            Get
                Return _type
            End Get
        End Property

        Public Overridable Function Render(ByVal value As Char()) As Char()
            Return value
        End Function

        Public Enum TagTypes
            Content
            Value
            Composite
        End Enum

    End Class

End Namespace
