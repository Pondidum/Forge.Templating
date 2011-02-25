Imports System.Text.RegularExpressions

Namespace SearchStrategies.Token

    Friend Class MatchData

        Private ReadOnly _matchIndex As Integer
        Private ReadOnly _matchLength As Integer
        Private ReadOnly _matchValue As String
        Private ReadOnly _tag As Tag

        Public Sub New(ByVal matchIndex As Integer, ByVal matchLength As Integer, ByVal matchValue As String, ByVal tag As Tag)
            _matchIndex = matchIndex
            _matchLength = matchLength
            _matchValue = matchValue
            _tag = tag
        End Sub

        Public Sub New(ByVal match As Match, ByVal tag As Tag)
            Me.New(match.Index, match.Length, match.Value, tag)
        End Sub

        Public ReadOnly Property Index As Integer
            Get
                Return _matchIndex
            End Get
        End Property

        Public ReadOnly Property Length As Integer
            Get
                Return _matchLength
            End Get
        End Property

        Public ReadOnly Property Tag As Tag
            Get
                Return _tag
            End Get
        End Property

        Public ReadOnly Property Value As String
            Get
                Return _matchValue
            End Get
        End Property

        Public Property Parent As MatchData
        Public Property Children As List(Of MatchData)

        Public Sub AddChild(ByVal child As MatchData)

            If child Is Nothing Then Throw New ArgumentNullException("child")

            Children.Add(child)
            child.Parent = Me

        End Sub

    End Class

End Namespace