Imports Forge.Templating.Interfaces

Namespace SearchStrategies.Token

    Friend Class ForLoopTag
        Inherits Tag

        '(?ixms)
        '\{\!
        '    (?:\s)?
        '    (?:foreach)
        '    (?:\s)+
        '    (?<current>.*)
        '    (?:\s)+
        '    (?:in)
        '    (?:\s)+
        '    (?<collection>.*?)
        '\}
        '(?<content>.*?)S
        '\{\!end\}
        Private Const RegexForLoop As String = "(?ixms)\{\!(?:\s)?(?:foreach)(?:\s)+(?<current>.*)(?:\s)+(?:in)(?:\s)+(?<collection>.*?)\}(?<content>.*?)\{\!end\}"

        Public Sub New(ByVal replacements As IList(Of IReplacementSource))
            MyBase.New(replacements, TagTypes.Composite, RegexForLoop)
        End Sub

    End Class

End Namespace