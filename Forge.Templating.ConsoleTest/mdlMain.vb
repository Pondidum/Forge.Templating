Imports Forge.Templating.SearchStrategies
Imports Forge.Templating.ReplacementSources

Module mdlMain

    Sub Main()

        Dim strat As New LoopingSearchStrategy
        strat.Template = ("{!foreach person in company.people}" & Environment.NewLine & "Hi {person.name}" & Environment.NewLine & Environment.NewLine & "{!end}").ToCharArray

        strat.Replacements.Add(New ReflectionReplacementSource(New Company))

        Console.Write(strat.Parse)

        Console.ReadKey()

    End Sub

    Private Class Company

        Property People As New List(Of Person)

        Public Sub New()
            People.Add(New Person("Dave"))
            People.Add(New Person("Steve"))
        End Sub

    End Class

    Private Class Person
        Property Name As String

        Public Sub New(ByVal theName As String)
            Name = theName
        End Sub
    End Class

End Module


Public Class Person

	Public ReadOnly Property Name As String
		Get
			Return "Andy"
		End Get
	End Property

End Class