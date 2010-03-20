Public Interface IReplacementSource

	ReadOnly Property Name As String
	
	Function HasValue(ByVal name As String) As Boolean
	Function GetValue(ByVal name As String) As String

	Function HasCollection(ByVal name As String) As Boolean
	Function GetCollection(ByVal name As String) As IEnumerable

End Interface
