<TestFixture()>
Public Class StandardSearchStrategyTests

	<Test()>
	Public Sub Handles_Null_Template()

		Dim strat As New StandardSearchStrategy

		Assert.AreEqual(String.Empty, strat.Parse())

	End Sub

	<Test()>
	Public Sub Handles_Null_Replacement_Source()

		Dim strat As New StandardSearchStrategy
		strat.Replacements = Nothing
		strat.Template = "Test".ToArray

		Assert.AreEqual("Test", strat.Parse)

	End Sub

	<Test()>
	Public Sub Handles_Malformed_Tag_Name()

		Dim strat As New StandardSearchStrategy
		strat.Template = "Malformed {.tag} here".ToCharArray

		Assert.AreEqual("Malformed  here", strat.Parse)

	End Sub

	<Test()>
	Public Sub Handles_Malformed_Tag_Object()

		Dim strat As New StandardSearchStrategy
		strat.Template = "Malformed {name.} here".ToCharArray

		Assert.AreEqual("Malformed  here", strat.Parse)

	End Sub

	<Test()>
	Public Sub Replaces_Property_With_Default()

		Dim strat As New StandardSearchStrategy
		strat.Template = "Test with {some.replacement} nothing".ToCharArray

		Assert.AreEqual("Test with  nothing", strat.Parse)

	End Sub

	<Test()>
	Public Sub Replaces_Tag_From_Correct_Source()

		Dim srcMain As IReplacementSource = MockRepository.GenerateMock(Of IReplacementSource)()
		srcMain.Expect(Function(s) s.Name).Return("Main")

		Dim srcSecondary As IReplacementSource = MockRepository.GenerateMock(Of IReplacementSource)()
		srcSecondary.Expect(Function(s) s.Name).Return("Secondary")
		srcSecondary.Expect(Function(s) s.HasValue("value")).Return(True)
		srcSecondary.Expect(Function(s) s.GetValue("value")).Return("omg!")

		Dim strat As New StandardSearchStrategy
		strat.Replacements.Add(srcMain)
		strat.Replacements.Add(srcSecondary)
		strat.Template = "{Secondary.value} worked".ToCharArray

		Assert.AreEqual("omg! worked", strat.Parse)

	End Sub




End Class
