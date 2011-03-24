Imports Forge.Templating.Interfaces

Namespace SearchStrategies.Token.Tags

    Friend Class EndTag
        Inherits Tag

        Public Sub New(ByVal matchIndex As Integer, ByVal matchLength As Integer, ByVal matchValue() As Char)
            MyBase.new(matchIndex,
                       matchLength,
                       matchValue,
                       TagRepository.TagTypes.End)

        End Sub

        Public Overrides Function Render(ByVal replacements As IList(Of IReplacementSource)) As Char()
            Return Nothing
        End Function

    End Class

End Namespace
