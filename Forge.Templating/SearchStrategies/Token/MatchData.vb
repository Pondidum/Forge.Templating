﻿Imports System.Text.RegularExpressions

Namespace SearchStrategies.Token

    Friend Class MatchData

        Private ReadOnly _matchIndex As Integer
        Private ReadOnly _matchLength As Integer
        Private ReadOnly _matchValue As Char()
        Private ReadOnly _tag As TagRepository.TagTypes

        Public Sub New(ByVal matchIndex As Integer, ByVal matchLength As Integer, ByVal matchValue As Char(), ByVal tag As TagRepository.TagTypes)
            _matchIndex = matchIndex
            _matchLength = matchLength
            _matchValue = matchValue
            _tag = tag
        End Sub
        
        Public Property Parent As MatchData
        Public Property Children As List(Of MatchData)

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

        Public ReadOnly Property Type As TagRepository.TagTypes
            Get
                Return _tag
            End Get
        End Property

        Public ReadOnly Property Value As Char()
            Get
                Return _matchValue
            End Get
        End Property

        Public Sub AddChild(ByVal child As MatchData)

            If child Is Nothing Then Throw New ArgumentNullException("child")

            Children.Add(child)
            child.Parent = Me

        End Sub
        
    End Class

End Namespace