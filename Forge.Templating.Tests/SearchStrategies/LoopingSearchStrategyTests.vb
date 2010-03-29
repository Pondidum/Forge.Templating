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

    <Test()>
    Public Sub Single_iteration_loop_replaces_correctly()

        Dim strat As New LoopingSearchStrategy
        strat.Template = ("{!foreach person in company.people}" & Environment.NewLine &
                          "Hi {person.name}" & Environment.NewLine &
                          "{!end}").ToCharArray


        Dim collection As New List(Of Person)
        collection.Add(New Person("Dave"))

        Dim irs As IReplacementSource = MockRepository.GenerateMock(Of IReplacementSource)()
        irs.Expect(Function(i) i.Name).Return("company")
        irs.Expect(Function(i) i.HasCollection("people")).Return(True)
        irs.Expect(Function(i) i.GetCollection("people")).Return(Collection)

        irs.Replay()

        strat.Replacements.Add(irs)

        Assert.AreEqual("Hi Dave", strat.Parse)
        irs.VerifyAllExpectations()

    End Sub

    <Test()>
    Public Sub Two_iteration_loop_deos_not_add_newlines()

        Dim strat As New LoopingSearchStrategy
        strat.Template = ("{!foreach person in company.people}" & Environment.NewLine &
                          "Hi {person.name} " & Environment.NewLine &
                          "{!end}").ToCharArray

        Dim collection As New List(Of Person)
        collection.Add(New Person("Dave"))
        collection.Add(New Person("Steve"))

        Dim irs As IReplacementSource = MockRepository.GenerateMock(Of IReplacementSource)()
        irs.Expect(Function(i) i.Name).Return("Company")
        irs.Expect(Function(i) i.HasCollection("people")).Return(True)
        irs.Expect(Function(i) i.GetCollection("people")).Return(collection)

        irs.Replay()

        strat.Replacements.Add(irs)

        Assert.AreEqual("Hi Dave Hi Steve ", strat.Parse)
        irs.VerifyAllExpectations()

    End Sub

    Private Class Person
        Property Name As String

        Public Sub New(ByVal theName As String)
            Name = theName
        End Sub
    End Class

End Class
