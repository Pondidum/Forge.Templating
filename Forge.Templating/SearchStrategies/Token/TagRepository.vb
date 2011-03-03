Imports Forge.Templating.Extensions

Namespace SearchStrategies.Token

    Friend Class TagRepository

        Private Shared ReadOnly _tags As IDictionary(Of TagTypes, String)

        Shared Sub New()
            _tags = New Dictionary(Of TagTypes, String)

            Add(TagTypes.Single Or TagTypes.Content, "")
            Add(TagTypes.Single Or TagTypes.Value, "(?ixm)\{(?<object>.*?)(?:\.)(?<property>.*?)\}")
            Add(TagTypes.Composite Or TagTypes.ForLoop, "(?ixms)\{\!(?:\s)?(?:foreach)(?:\s)+(?<current>.*)(?:\s)+(?:in)(?:\s)+(?<collection>.*?)\}(?<content>.*?)\{\!end\}")

        End Sub

        Public Shared ReadOnly Property All As IDictionary(Of TagTypes, String)
            Get
                Return _tags
            End Get
        End Property

        Public Shared Sub Add(ByVal type As TagTypes, ByVal pattern As String)

            If type.IsAny(TagTypes.Single, TagTypes.Composite) Then
                Throw New NotSupportedException("A type must contain Single or Composite and a value")
            End If

            If Not type.HasAny(TagTypes.Single, TagTypes.Composite) Then
                Throw New NotSupportedException("A TagType must contain either Single or Composite")
            End If

            _tags.Add(type, pattern)

        End Sub

        Public Shared Function Create(ByVal index As Integer, ByVal length As Integer, ByVal value As Char(), ByVal tag As TagTypes) As Tag

            If tag.Has(TagTypes.Content) Then

            ElseIf tag.Has(TagTypes.Value) Then

            ElseIf tag.Has(TagTypes.ForLoop) Then

                Return New Tags.ForLoop(index, length, value)

            Else

                Throw New NotSupportedException()
            End If

        End Function

        <Flags()> _
        Public Enum TagTypes
            [Single] = 1
            Composite = 2

            Content = 4
            Value = 8

            Root = 16
            ForLoop = 32
        End Enum

    End Class

End Namespace
