Imports Forge.Templating.SearchStrategies
Imports Forge.Templating.Interfaces

Namespace SearchStrategies

    <TestFixture()>
    Public Class SimpleSearchStrategyTests

        <Test()>
        Public Sub Handles_Blank_Template()

            Dim replacements As IReplacementSource = MockRepository.GenerateMock(Of IReplacementSource)()
            Dim simple As New SimpleSearchStrategy
            simple.Replacements.Add(replacements)

            Assert.AreEqual(String.Empty, simple.Parse)

        End Sub

        <Test()>
        Public Sub Handles_Null_Replacment_Source_List()

            Dim simple As New SimpleSearchStrategy
            simple.Template = "template".ToCharArray
            simple.Replacements = Nothing

            Assert.AreEqual("template", simple.Parse)

        End Sub

        <Test()>
        Public Sub Handles_No_Replacement_Source()

            Dim simple As New SimpleSearchStrategy
            simple.Template = "Test with {one} replacement".ToCharArray

            Assert.AreEqual("Test with  replacement", simple.Parse)

        End Sub

        <Test()>
        Public Sub Handles_Empty_Replacement_Source()

            Dim replacements As IReplacementSource = MockRepository.GenerateMock(Of IReplacementSource)()
            replacements.Expect(Function(i) i.HasValue("one")).Return(False)

            Dim simple As New SimpleSearchStrategy
            simple.Template = "Test with {one} replacement".ToCharArray
            simple.Replacements.Add(replacements)

            Assert.AreEqual("Test with  replacement", simple.Parse)

        End Sub

        <Test()>
        Public Sub Replaces_Single_Tag_Instance()

            Dim replacements As IReplacementSource = MockRepository.GenerateMock(Of IReplacementSource)()
            replacements.Expect(Function(c) c.HasValue("one")).Return(True)
            replacements.Expect(Function(c) c.GetValue("one")).Return("a cool")

            Dim simple As New SimpleSearchStrategy
            simple.Template = "Test with {one} replacement".ToCharArray
            simple.Replacements.Add(replacements)

            Assert.AreEqual("Test with a cool replacement", simple.Parse)

        End Sub

    End Class

End Namespace
