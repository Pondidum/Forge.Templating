Imports System.Text.RegularExpressions
Imports Templating.Extensions.CharArray

Public Class LoopingSearchStrategy
	Implements ISearchStrategy

	'(?ixm)
	'\{\!
	'    (?:\s)?
	'    (?:foreach)
	'    (?:\s)+
	'    (?<current>.*)
	'    (?:\s)+
	'    (?:in)
	'    (?:\s)+
	'    (?<collection>.*)
	'\}
	'(?<content>.|[\r\n])*?
	'\{\!end}
	Private Const RegexForLoop As String = "(?ixm)\{\!(?:\s)?(?:foreach)(?:\s)+(?<current>.*)(?:\s)+(?:in)(?:\s)+(?<collection>.*)\}(?<content>.|[\r\n])*?\{\!end}"

	Private _template() As Char
	Private _replacements As IList(Of IReplacementSource)

	Public Sub New()
		_replacements = New List(Of IReplacementSource)
	End Sub

	Public WriteOnly Property Template As Char() Implements ISearchStrategy.Template
		Set(ByVal value As Char())
			_template = value
		End Set
	End Property

	Public Property Replacements As IList(Of IReplacementSource) Implements ISearchStrategy.Replacements
		Get
			Return _replacements
		End Get
		Set(ByVal value As IList(Of IReplacementSource))
			_replacements = value
		End Set
	End Property

	Public Function Parse() As String Implements ISearchStrategy.Parse

		If _template Is Nothing OrElse _template.Length = 0 Then
			Return String.Empty
		End If

		Dim sb As New Text.StringBuilder
		Dim index As Integer = 0
		
		''first we do all the loops (and one of the parsed replacements might be in the loop too, so pass them in also)

		For Each m As Match In Regex.Matches(_template, RegexForLoop)

			Dim loopVariable As String = m.Groups("current").Value.Trim
			Dim loopCollection As String = m.Groups("collection").Value.Trim
			Dim content As String = m.Groups("content").Value.Trim

			Dim replacement As New StandardSearchStrategy
			replacement.Template = content.ToArray
			replacement.Replacements = _replacements

			sb.Append(_template.Range(index, m.Index - index))

			sb.Append(replacement.Parse)

			index = m.Index + m.Length

		Next

		sb.Append(_template.Range(index, _template.Length - index))

		Return sb.ToString

	End Function



End Class
