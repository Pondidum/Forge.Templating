Namespace SearchStrategies.Token.Tags

    Friend Class ContentTag
        Inherits Tag

        Public Sub New(ByVal matchIndex As Integer, ByVal matchLength As Integer, ByVal matchValue() As Char)
            MyBase.new(matchIndex, matchLength, matchValue, TagRepository.TagTypes.Single Or TagRepository.TagTypes.Content)
        End Sub

        Public Overrides Function Parse() As Char()
            Return Me.Value
        End Function

    End Class

End Namespace
