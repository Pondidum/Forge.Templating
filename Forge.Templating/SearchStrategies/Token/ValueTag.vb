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

        Public Sub New()
            MyBase.New(TagTypes.Value, RegexTag)
        End Sub
        
    End Class

End Namespace