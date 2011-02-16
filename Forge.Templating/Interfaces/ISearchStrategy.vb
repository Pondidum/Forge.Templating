Namespace Interfaces

    Public Interface ISearchStrategy

        WriteOnly Property Template As Char()
        Property Replacements As IList(Of IReplacementSource)
        Function Parse() As String

    End Interface

End Namespace
