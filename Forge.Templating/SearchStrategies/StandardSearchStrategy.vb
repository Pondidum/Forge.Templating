Imports System.Text.RegularExpressions
Imports Forge.Templating.Extensions.CharArray

Public Class StandardSearchStrategy
	Implements ISearchStrategy

	Private _replacements As IList(Of IReplacementSource)
	Private _template() As Char

	'(?ixm)
	'\{
	'    (?<object>.*?)
	'    (?:\.)
	'    (?<property>.*?)
	'\}
	'Looks for strings with the following form: {name.property}
	Private Const RegexTag As String = "(?ixm)\{(?<object>.*?)(?:\.)(?<property>.*?)\}"

	Public Sub New()
		_replacements = New List(Of IReplacementSource)
	End Sub

	Public Property Replacements As IList(Of IReplacementSource) Implements ISearchStrategy.Replacements
		Get
			Return _replacements
		End Get
		Set(ByVal value As IList(Of IReplacementSource))
			_replacements = value
		End Set
	End Property

	Public WriteOnly Property Template As Char() Implements ISearchStrategy.Template
		Set(ByVal value As Char())
			_template = value
		End Set
	End Property

	Public Function Parse() As String Implements ISearchStrategy.Parse

        If _template Is Nothing OrElse _template.Length = 0 Then
            Return String.Empty
        End If

        Dim sb As New StringBuilder
		Dim index As Integer = 0

		For Each m As Match In Regex.Matches(_template, RegexTag)

			Dim name As String = m.Groups("object").Value
			Dim prop As String = m.Groups("property").Value

			Dim source As IReplacementSource = SourceByName(name)
			'TODO: add logging to ISearchStrategy

			sb.Append(_template.Range(index, m.Index - index))

            If source IsNot Nothing AndAlso source.HasValue(prop) Then
                sb.Append(source.GetValue(prop))
            End If

			index = m.Index + m.Length

		Next

		If index < _template.Length AndAlso _template.Length - index > 0 Then
			sb.Append(_template.Range(index, _template.Length - index))
		End If


		Return sb.ToString

	End Function

	Private Function SourceByName(ByVal name As String) As IReplacementSource

		If String.IsNullOrWhiteSpace(name) Then
			Return Nothing
		End If

		Dim sources =
		 From s
		 In _replacements
		 Where name.Equals(s.Name, StringComparison.OrdinalIgnoreCase)
		 Select s

		Return sources.FirstOrDefault

	End Function

End Class
