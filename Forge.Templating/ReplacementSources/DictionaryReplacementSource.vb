Imports Forge.Templating.Interfaces

Namespace ReplacementSources

    Public Class DictionaryReplacementSource
        Implements IReplacementSource

        Private _name As String
        Private _replacements As IDictionary(Of String, String)

        Public Sub New()
            Me.New(String.Empty)
        End Sub

        Public Sub New(ByVal name As String)
            Me.New(name, StringComparer.OrdinalIgnoreCase)
        End Sub

        Public Sub New(ByVal comparer As IEqualityComparer(Of String))
            Me.New(String.Empty, comparer)
        End Sub

        Public Sub New(ByVal name As String, ByVal comparer As IEqualityComparer(Of String))
            _name = name
            _replacements = New Dictionary(Of String, String)(comparer)
        End Sub

        Public ReadOnly Property Name As String Implements IReplacementSource.Name
            Get
                Return _name
            End Get
        End Property

        Public ReadOnly Property Items As IDictionary(Of String, String)
            Get
                Return _replacements
            End Get
        End Property

        Public Sub Add(ByVal key As String, ByVal value As String)
            _replacements.Add(key, value)
        End Sub

        Public Function Remove(ByVal key As String) As Boolean
            Return _replacements.Remove(key)
        End Function

        Public Sub Clear()
            _replacements.Clear()
        End Sub

        Public Function GetCollection(ByVal name As String) As System.Collections.IEnumerable Implements IReplacementSource.GetCollection
            Throw New NotSupportedException("Collections are not supported by the Dictionary Replacement Source")
        End Function

        Public Function HasCollection(ByVal name As String) As Boolean Implements IReplacementSource.HasCollection
            Return False
        End Function

        Public Function GetValue(ByVal name As String) As String Implements IReplacementSource.GetValue

            If _replacements.ContainsKey(name) Then
                Return _replacements(name)
            Else
                Return String.Empty
            End If

        End Function

        Public Function HasValue(ByVal name As String) As Boolean Implements IReplacementSource.HasValue

            Return _replacements.ContainsKey(name)

        End Function

    End Class

End Namespace
