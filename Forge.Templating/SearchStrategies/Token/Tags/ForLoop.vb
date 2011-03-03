﻿Namespace SearchStrategies.Token.Tags

    Friend Class ForLoop
        Inherits Tag
        
        Public Sub New(ByVal matchIndex As Integer, ByVal matchLength As Integer, ByVal matchValue() As Char)
            MyBase.new(matchIndex,
                       matchLength,
                       matchValue,
                       TagRepository.TagTypes.Composite Or TagRepository.TagTypes.ForLoop)
        End Sub

        Public Overrides Function Parse() As Char()

        End Function

    End Class

End Namespace