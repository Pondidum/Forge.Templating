Imports System.Text.RegularExpressions

Namespace SearchStrategies.Token

    Friend Class MatchData

        Private ReadOnly _match As Match
        Private ReadOnly _tag As Tag

        Public Sub New(ByVal match As Match, ByVal tag As Tag)
            _match = match
            _tag = tag
        End Sub

        Public ReadOnly Property Index As Integer
            Get
                Return _match.Index
            End Get
        End Property

        Public ReadOnly Property Length As Integer
            Get
                Return _match.Length
            End Get
        End Property

        Public ReadOnly Property Tag As Tag
            Get
                Return _tag
            End Get
        End Property

        Public ReadOnly Property Value As String
            Get
                Return _match.Value
            End Get
        End Property

    End Class

End Namespace