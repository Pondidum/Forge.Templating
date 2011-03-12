Imports Forge.Templating.Interfaces

Namespace SearchStrategies.Token.Tags

    Friend Class RootTag
        Inherits Tag

        Public Sub New(ByVal template As Char())
            MyBase.New(0,
                       template.Count(),
                       template,
                       TagRepository.TagTypes.Root)
        End Sub

        Public Overrides Function Render(ByVal replacements As IList(Of IReplacementSource)) As Char()

            Dim output As New List(Of Char)(Me.Length)

            For Each child In Me.Children
                output.AddRange(child.Render(replacements))
            Next

            Return output.ToArray()

        End Function

    End Class

End Namespace
