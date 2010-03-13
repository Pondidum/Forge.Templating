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


	'Public Sub Add(ByVal item As KeyValuePair(Of String, String)) Implements ICollection(Of KeyValuePair(Of String, String)).Add
	'_replacements.Add(item)
	'End Sub

	'Public Sub Add(ByVal key As String, ByVal value As String) Implements IDictionary(Of String, String).Add
	'_replacements.Add(key, value)
	'End Sub

	'Public Function Remove(ByVal item As KeyValuePair(Of String, String)) As Boolean Implements ICollection(Of KeyValuePair(Of String, String)).Remove
	'Return Remove(item)
	'End Function

	'Public Function Remove(ByVal key As String) As Boolean Implements IDictionary(Of String, String).Remove
	'Return _replacements.Remove(key)
	'End Function

	'Public Sub Clear() Implements ICollection(Of KeyValuePair(Of String, String)).Clear
	'_replacements.Clear()
	'End Sub

	'Public Function Contains(ByVal item As KeyValuePair(Of String, String)) As Boolean Implements ICollection(Of KeyValuePair(Of String, String)).Contains
	'Return _replacements.Contains(item)
	'End Function

	'Public Function ContainsKey(ByVal key As String) As Boolean Implements IDictionary(Of String, String).ContainsKey
	'Return _replacements.ContainsKey(key)
	'End Function


	'Public Sub CopyTo(ByVal array() As KeyValuePair(Of String, String), ByVal arrayIndex As Integer) Implements ICollection(Of KeyValuePair(Of String, String)).CopyTo
	'_replacements.CopyTo(array, arrayIndex)
	'End Sub

	'Public ReadOnly Property Count As Integer Implements ICollection(Of KeyValuePair(Of String, String)).Count
	'Get
	'Return _replacements.Count
	'End Get
	'End Property

	'Public ReadOnly Property IsReadOnly As Boolean Implements ICollection(Of KeyValuePair(Of String, String)).IsReadOnly
	'Get
	'Return False
	'End Get
	'End Property


	'Default Public Property Item(ByVal key As String) As String Implements IDictionary(Of String, String).Item
	'Get
	'Return _replacements(key)
	'End Get
	'Set(ByVal value As String)
	'_replacements(key) = value
	'End Set
	'End Property

	'Public ReadOnly Property Keys As ICollection(Of String) Implements IDictionary(Of String, String).Keys
	'Get
	'Return _replacements.Keys
	'End Get
	'End Property

	'Public Function TryGetValue(ByVal key As String, ByRef value As String) As Boolean Implements IDictionary(Of String, String).TryGetValue
	'Return _replacements.TryGetValue(key, value)
	'End Function

	'Public ReadOnly Property Values As ICollection(Of String) Implements IDictionary(Of String, String).Values
	'Get
	'Return _replacements.Values
	'End Get
	'End Property

	'Public Function GetEnumerator() As IEnumerator(Of KeyValuePair(Of String, String)) Implements IEnumerable(Of KeyValuePair(Of String, String)).GetEnumerator
	'Return _replacements.GetEnumerator
	'End Function

	'Private Function GetEnumeratorOther() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
	'Return _replacements.GetEnumerator
	'End Function

End Class
