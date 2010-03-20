Module mdlMain

	Sub Main()

		Dim engine As New Templating.Engine(New StandardSearchStrategy)

		Using sr As New IO.StreamReader(My.Application.Info.DirectoryPath & "\templates\multi-object-template.txt")
			engine.Template = sr.ReadToEnd
		End Using

		Dim people As New Templating.ReflectionReplacementSource(New Person)

		Dim animals As New Templating.DictionaryReplacementSource("animals")
		animals.Add("cat", "jess")
		animals.Add("name", "jezz")

		Console.WriteLine(engine.Parse(New List(Of IReplacementSource)({people, animals})))
		Console.ReadKey()

	End Sub

End Module


Public Class Person

	Public ReadOnly Property Name As String
		Get
			Return "Andy"
		End Get
	End Property

End Class