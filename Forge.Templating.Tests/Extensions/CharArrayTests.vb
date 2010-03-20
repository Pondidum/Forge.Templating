Imports Forge.Templating.Extensions.CharArray

Namespace CharArrayTests

	<TestFixture()>
	Public Class RangeTests

		<Test()>
		<ExpectedException(GetType(ArgumentNullException))>
		Public Sub Handles_null_input()

			Dim input() As Char = Nothing
			Dim result() As Char = input.Range(0, 0)

		End Sub

		<Test()>
		<ExpectedException(GetType(ArgumentOutOfRangeException))>
		Public Sub Handles_negative_start_index()

			Dim input() As Char = "abcde".ToCharArray
			Dim result() As Char = input.Range(-1, 2)

		End Sub

		<Test()>
		<ExpectedException(GetType(ArgumentOutOfRangeException))>
		Public Sub Handles_start_index_larger_than_input_length()

			Dim input() As Char = "abcde".ToCharArray
			Dim result() As Char = input.Range(50, 2)

		End Sub

		<Test()>
		Public Sub Handles_zero_length()

			Dim input() As Char = "abcde".ToCharArray
			Dim result() As Char = input.Range(0, 0)

			Assert.AreEqual(0, result.Length)

		End Sub

		<Test()>
		<ExpectedException(GetType(ArgumentOutOfRangeException))>
		Public Sub Handles_negative_length()

			Dim input() As Char = "abcde".ToCharArray
			Dim result() As Char = input.Range(0, -2)

		End Sub

		<Test()>
		<ExpectedException(GetType(ArgumentOutOfRangeException))>
		Public Sub Handles_length_larger_then_input_length()

			Dim input() As Char = "abcde".ToCharArray
			Dim result() As Char = input.Range(0, 6)

		End Sub

		<Test()>
		<ExpectedException(GetType(ArgumentOutOfRangeException))>
		Public Sub Handles_start_plus_length_larger_than_input()

			Dim input() As Char = "abcde".ToCharArray
			Dim result() As Char = input.Range(2, 4)

		End Sub

		<Test()>
		Public Sub Returns_first_char()

			Dim input() As Char = "abcde".ToCharArray
			Dim result() As Char = input.Range(0, 1)

			Assert.AreEqual(1, result.Length)
			Assert.AreEqual("a"c, result.First)

		End Sub

		<Test()>
		Public Sub Returns_last_char()

			Dim input() As Char = "abcde".ToCharArray
			Dim result() As Char = input.Range(4, 1)

			Assert.AreEqual(1, result.Length)
			Assert.AreEqual("e"c, result.Last)

		End Sub

	End Class

	<TestFixture()>
	Public Class IndexOfWordTests

		<Test()>
		<ExpectedException(GetType(ArgumentNullException))>
		Public Sub Handles_null_haystack()

			Dim haystack() As Char = Nothing
			Dim result As Integer = haystack.IndexOfWord("needle".ToCharArray)

		End Sub

		<Test()>
		<ExpectedException(GetType(ArgumentNullException))>
		Public Sub Handles_null_needle()

			Dim haystack() As Char = "input sentance with many words".ToCharArray
			Dim needle() As Char = Nothing

			Dim result As Integer = haystack.IndexOfWord(needle)

		End Sub

		<Test()>
		Public Sub Handles_needle_longer_than_haystack()

			Dim haystack() As Char = "haystack".ToCharArray
			Dim needle() As Char = "like finding a needle in a ".ToCharArray

			Assert.AreEqual(-1, haystack.IndexOfWord(needle))

		End Sub

		<Test()>
		Public Sub Finds_word_case_correct()

			Dim haystack() As Char = "input sentance with many words".ToCharArray
			Dim needle() As Char = "sentance".ToCharArray

			Assert.AreEqual(6, haystack.IndexOfWord(needle))

		End Sub

		<Test()>
		Public Sub Does_not_find_word_case_incorrect()

			Dim haystack() As Char = "input sentance with many words".ToCharArray
			Dim needle() As Char = "SENTANCE".ToCharArray

			Assert.AreEqual(-1, haystack.IndexOfWord(needle))

		End Sub

		<Test()>
		Public Sub Does_not_find_word()


			Dim haystack() As Char = "input sentance with many words".ToCharArray
			Dim needle() As Char = "Templating".ToCharArray

			Assert.AreEqual(-1, haystack.IndexOfWord(needle))

		End Sub

	End Class

	<TestFixture()>
	Public Class AsStringTests

		<Test()>
		Public Sub Handles_null_input()

			Dim input() As Char = Nothing

			Assert.AreEqual(String.Empty, input.AsString)

		End Sub

		<Test()>
		Public Sub Converts_Properly()

			Dim input() As Char = "test string".ToCharArray

			Assert.AreEqual("test string", input.AsString)

		End Sub
	End Class
End Namespace