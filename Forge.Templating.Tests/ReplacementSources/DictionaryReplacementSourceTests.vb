Imports Forge.Templating.ReplacementSources

Namespace ReplacementSources

    <TestFixture()>
    Public Class DictionaryReplacementSourceTests

        <Test()>
        Public Sub Returns_Collection_Name()

            Dim src As New DictionaryReplacementSource("test")

            Assert.AreEqual("test", src.Name)

        End Sub

        <Test()>
        Public Sub Can_Add_And_Remove_Items()

            Dim parser As New DictionaryReplacementSource

            parser.Add("test", "string")

            Assert.IsTrue(parser.Items.ContainsKey("test"))

            parser.Remove("test")

            Assert.IsFalse(parser.Items.ContainsKey("test"))

        End Sub

        <Test()>
        Public Sub Can_Clear_Replacements()

            Dim parser As New DictionaryReplacementSource
            parser.Add("test", "string")
            parser.Add("item", "integer")

            Assert.AreEqual(2, parser.Items.Count)

            parser.Clear()

            Assert.AreEqual(0, parser.Items.Count)

        End Sub

        <Test()>
        Public Sub Item_Collection_Can_Be_Modified_By_Reference()

            Dim parser As New DictionaryReplacementSource
            Dim dict As IDictionary(Of String, String) = parser.Items

            dict.Add("Key", "val")

            Assert.AreEqual(1, parser.Items.Count)

            dict.Clear()

            Assert.AreEqual(0, parser.Items.Count)


        End Sub

        <Test()>
        Public Sub Insensitive_Finds_Key_For_Proper_Cased_Key()

            Dim parser As New DictionaryReplacementSource
            parser.Add("test", "string")

            Assert.IsTrue(parser.HasValue("test"))

        End Sub

        <Test()>
        Public Sub Insensitive_Finds_Key_For_Miss_Cased_Key()

            Dim parser As New DictionaryReplacementSource
            parser.Add("test", "string")

            Assert.IsTrue(parser.HasValue("TesT"))

        End Sub

        <Test()>
        Public Sub Sensitive_Finds_Key_For_Proper_Cased_Key()

            Dim parser As New DictionaryReplacementSource(StringComparer.CurrentCulture)
            parser.Add("test", "string")

            Assert.IsTrue(parser.HasValue("test"))

        End Sub

        <Test()>
        Public Sub Sensitive_Doesnt_Find_Key_For_Miss_Cased_Key()

            Dim parser As New DictionaryReplacementSource(StringComparer.CurrentCulture)
            parser.Add("test", "string")

            Assert.IsFalse(parser.HasValue("TesT"))

        End Sub

        <Test()>
    <ExpectedException(GetType(NotSupportedException))>
        Public Sub Does_Not_Support_Collections()

            Dim parser As New DictionaryReplacementSource

            Assert.IsFalse(parser.HasCollection("test"))

            parser.GetCollection("test")

        End Sub

        <Test()>
        Public Sub Insensitive_Gets_Value_Proper_Cased()

            Dim parser As New DictionaryReplacementSource
            parser.Add("string", "value")

            Assert.AreEqual("value", parser.GetValue("string"))

        End Sub

        <Test()>
        Public Sub Insensitive_Gets_Value_Miss_Cased()

            Dim parser As New DictionaryReplacementSource
            parser.Add("string", "value")

            Assert.AreEqual("value", parser.GetValue("STRing"))

        End Sub

        <Test()>
        Public Sub Insensitive_Gets_Default_Value()

            Dim parser As New DictionaryReplacementSource

            Assert.AreEqual("", parser.GetValue("orgno"))

        End Sub

        <Test()>
        Public Sub Sensitive_Gets_Value_Proper_Cased()

            Dim parser As New DictionaryReplacementSource(StringComparer.CurrentCulture)
            parser.Add("test", "value")

            Assert.AreEqual("value", parser.GetValue("test"))

        End Sub

        <Test()>
        Public Sub Sensitive_Gets_Default_Value_Miss_Cased()


            Dim parser As New DictionaryReplacementSource(StringComparer.CurrentCulture)
            parser.Add("test", "value")

            Assert.AreEqual("", parser.GetValue("TEST"))

        End Sub

    End Class

End Namespace
