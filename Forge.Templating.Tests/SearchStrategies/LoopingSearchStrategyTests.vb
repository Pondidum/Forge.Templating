<TestFixture()>
Public Class LoopingSearchStrategyTests

	<Test()>
	Public Sub Handles_Blank_Template()

		Dim replacements As IReplacementSource = MockRepository.GenerateMock(Of IReplacementSource)()
		Dim simple As New LoopingSearchStrategy
		simple.Replacements.Add(replacements)

		Assert.AreEqual(String.Empty, simple.Parse)

	End Sub

	<Test()>
	Public Sub Handles_Null_Replacment_Source_List()

		Dim simple As New LoopingSearchStrategy
		simple.Template = "template".ToCharArray
		simple.Replacements = Nothing

		Assert.AreEqual("template", simple.Parse)

	End Sub

	<Test()>
	Public Sub Handles_No_Replacement_Source()

		Dim simple As New LoopingSearchStrategy
		simple.Template = "Test with {!foreach item in collection} test {!end} replacement".ToCharArray

		Assert.AreEqual("Test with  replacement", simple.Parse)

	End Sub

	<Test()>
	Public Sub Handles_Empty_Replacement_Source()

		Dim replacements As IReplacementSource = MockRepository.GenerateMock(Of IReplacementSource)()
		replacements.Expect(Function(i) i.HasValue("one")).Return(False)

		Dim simple As New LoopingSearchStrategy
		simple.Template = "Test with {!foreach item in collection} test {!end} replacement".ToCharArray
		simple.Replacements.Add(replacements)

		Assert.AreEqual("Test with  replacement", simple.Parse)

	End Sub

End Class
