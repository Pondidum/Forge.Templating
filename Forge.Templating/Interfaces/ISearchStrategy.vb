Namespace Interfaces

    Public Interface ISearchStrategy

        WriteOnly Property Template As Char()
        Property Replacements As IList(Of IReplacementSource)

        Sub Setup()
        Function Parse() As String

    End Interface

End Namespace
