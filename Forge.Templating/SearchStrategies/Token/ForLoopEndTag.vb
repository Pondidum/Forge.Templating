Namespace SearchStrategies.Token

    Friend Class ForLoopEndTag
        Inherits Tag

        '(?ixms)
        '\{\!end\}
        Private Const RegexForLoop As String = "(?ixms)\{\!end\}"

        Public Sub New()
            MyBase.New(TagTypes.ParentBegining, RegexForLoop)
        End Sub

    End Class

End Namespace
