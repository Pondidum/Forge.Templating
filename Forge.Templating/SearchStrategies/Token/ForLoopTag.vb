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
        '(?<content>.*?)
        '\{\!end\}
        Private Const RegexForLoop As String = "(?ixms)\{\!(?:\s)?(?:foreach)(?:\s)+(?<current>.*)(?:\s)+(?:in)(?:\s)+(?<collection>.*?)\}(?<content>.*?)\{\!end\}"

        Public Sub New()
            MyBase.New(TagTypes.ParentBegining, RegexForLoop)
        End Sub

    End Class

End Namespace