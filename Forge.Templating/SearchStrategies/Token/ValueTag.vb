Imports System.Text.RegularExpressions
Imports Forge.Templating.Interfaces

Namespace SearchStrategies.Token

    Friend Class ValueTag
        Inherits Tag

        '(?ixm)
        '\{
        '    (?<object>.*?)
        '    (?:\.)
        '    (?<property>.*?)
        '\}
        'Looks for strings with the following form: {name.property}
        Private Const RegexTag As String = "(?ixm)\{(?<object>.*?)(?:\.)(?<property>.*?)\}"

        Public Sub New(ByVal replacements As IList(Of IReplacementSource))
            MyBase.New(replacements, TagTypes.Value, RegexTag)
        End Sub

        Public Overrides Function Render(ByVal value() As Char) As Char()

            Dim match = Regex.Match(value, RegexTag)

            Dim name As String = match.Groups("object").Value
            Dim prop As String = match.Groups("property").Value

            Dim source As IReplacementSource = SourceByName(name)
            
            If source IsNot Nothing AndAlso source.HasValue(prop) Then
                Return source.GetValue(prop).ToArray()
            End If

            Return New Char() {}

        End Function


        Private Function SourceByName(ByVal name As String) As IReplacementSource

            If String.IsNullOrWhiteSpace(name) Then
                Return Nothing
            End If

            Dim sources = Replacements.Where(Function(s) String.Equals(s.Name, name, StringComparison.OrdinalIgnoreCase))
        
            Return sources.FirstOrDefault()

        End Function
    End Class

End Namespace