﻿Namespace SearchStrategies.Token

    Friend Class TagRepository

        Private Shared ReadOnly _tags As IDictionary(Of TagTypes, TagDefinition)
        Private Shared ReadOnly _specialTags() As TagTypes

        Shared Sub New()
            _tags = New Dictionary(Of TagTypes, TagDefinition)
            _specialTags = New TagTypes() {TagTypes.Content, TagTypes.Root}

            Add(TagTypes.Content,
                "",
                Function(i, l, v) New Tags.ContentTag(i, l, v))

            Add(TagTypes.Value,
                "(?ixm)\{(?<property>[^!].*?)\}",
                Function(i, l, v) New Tags.ValueTag(i, l, v))

            Add(TagTypes.Root, "", Function(i, l, v) New Tags.RootTag(v)) 'special case

            Add(TagTypes.ForLoop,
                "(?ixms)\{\!(?:\s)?(?:foreach)(?:\s)+(?<current>.*)(?:\s)+(?:in)(?:\s)+(?<collection>.*?)\}(?<content>.*?)",
                Function(i, l, v) New Tags.ForLoop(i, l, v))

            Add(TagTypes.End,
                "(?ixms)\{\!end\}",
                Function(i, l, v) New Tags.EndTag(i, l, v))

        End Sub

        Public Shared ReadOnly Property AllTags As IDictionary(Of TagTypes, String)
            Get
                Return _tags.Where(Function(t) Not _specialTags.Contains(t.Key)).
                             ToDictionary(Function(t) t.Key,
                                          Function(t) t.Value.Pattern)
            End Get
        End Property

        Public Shared Sub Add(ByVal type As TagTypes, ByVal pattern As String, ByVal creator As Func(Of Integer, Integer, Char(), Tag))

            _tags.Add(type, New TagDefinition(type, pattern, creator))

        End Sub

        Public Shared Function Create(ByVal index As Integer, ByVal length As Integer, ByVal value As Char(), ByVal tag As TagTypes) As Tag

            If Not _tags.ContainsKey(tag) Then
                Throw New NotSupportedException(String.Format("The tag '{0}' has not been added to the TagRepository for creation.", tag))
            End If

            Return _tags(tag).Creator.Invoke(index, length, value)

        End Function

        Public Enum TagTypes

            Content = 0
            Value
            [End]

            Root = 100
            ForLoop

        End Enum

        Private Class TagDefinition

            Private ReadOnly _type As TagTypes
            Private ReadOnly _pattern As String
            Private ReadOnly _creator As Func(Of Integer, Integer, Char(), Tag)

            Public Sub New(ByVal type As TagTypes, ByVal pattern As String, ByVal creator As Func(Of Integer, Integer, Char(), Tag))
                _type = type
                _pattern = pattern
                _creator = creator
            End Sub

            Public ReadOnly Property Type As TagTypes
                Get
                    Return _type
                End Get
            End Property

            Public ReadOnly Property Pattern As String
                Get
                    Return _pattern
                End Get
            End Property

            Public ReadOnly Property Creator As Func(Of Integer, Integer, Char(), Tag)
                Get
                    Return _creator
                End Get
            End Property

        End Class

    End Class

End Namespace
