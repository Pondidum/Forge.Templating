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

        strat.Replacements.Add(New ReflectionReplacementSource(New Company(New Person("Dave"))))

        Assert.AreEqual("Hi Dave", strat.Parse)

    End Sub

    <Test()>
    Public Sub Two_iteration_loop_deos_not_add_newlines()

        Dim strat As New LoopingSearchStrategy
        strat.Template = ("{!foreach person in company.people}" & Environment.NewLine &
                          "Hi {person.name} " & Environment.NewLine &
                          "{!end}").ToCharArray

        strat.Replacements.Add(New ReflectionReplacementSource(New Company(New Person("Dave"),
                                                                           New Person("Steve")
                                                                           )))

        Assert.AreEqual("Hi Dave Hi Steve ", strat.Parse)

    End Sub

    Private Class Company

        Property People As New List(Of Person)

        Public Sub New(ByVal ParamArray employees() As Person)
            People = New List(Of Person)(employees)
        End Sub

    End Class

    Private Class Person
        Property Name As String

        Public Sub New(ByVal theName As String)
            Name = theName
        End Sub
    End Class

End Class
