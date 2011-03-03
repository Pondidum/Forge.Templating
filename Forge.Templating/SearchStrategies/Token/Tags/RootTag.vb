Namespace SearchStrategies.Token.Tags

    Friend Class RootTag
        Inherits Tag

        Public Sub New(ByVal template As Char())
            MyBase.New(0,
                       template.Count(),
                       template,
                       TagRepository.TagTypes.Composite Or TagRepository.TagTypes.Content)
        End Sub

        Public Overrides Function Parse() As Char()

            Dim output As New List(Of Char)(Me.Length)

            For Each child In Me.Children
                output.AddRange(child.Parse())
            Next

            Return output.ToArray()

        End Function

    End Class

End Namespace
