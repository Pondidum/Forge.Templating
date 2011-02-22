Namespace SearchStrategies.Token

    Friend Class Tag

        Private ReadOnly _pattern As String
        Private ReadOnly _type As TagTypes

        Public Sub New(ByVal type As TagTypes, ByVal pattern As String)
            _type = type
            _pattern = pattern 
        End Sub
        
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

        Public Enum TagTypes
            Content
            Value
            Parent
        End Enum

    End Class
End Namespace