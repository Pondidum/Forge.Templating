Public Interface IReplacementStrategy

    Sub Setup(ByVal template() As Char)
    Sub Replace(ByVal key As String, ByVal value As String)
    ReadOnly Property Result() As String

End Interface
