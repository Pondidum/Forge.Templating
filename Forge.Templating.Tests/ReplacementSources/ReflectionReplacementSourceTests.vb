<TestFixture()>
Public Class ReflectionReplacementSourceTests

	<Test()>
	Public Sub Source_Sets_Via_Constructor()

		Dim prop As New PropertyOnly
		Dim src As New ReflectionReplacementSource(prop)

		Assert.AreEqual(prop, src.SourceObject)

	End Sub

	<Test()>
	<ExpectedException(GetType(ArgumentNullException))>
	Public Sub Name_Checks_For_Null_Source_Object()

		Dim src As New ReflectionReplacementSource

		Dim name As String = src.Name

	End Sub

	<Test()>
	Public Sub Name_Returns_Friendly_Class_Name()

		Dim src As New ReflectionReplacementSource(New PropertyOnly)

		Assert.AreEqual("PropertyOnly", src.Name)

	End Sub

	<Test()>
	<ExpectedException(GetType(ArgumentNullException))>
	Public Sub Has_Member_Checks_For_Null_Source_Object()

		Dim parser As New ReflectionReplacementSource
		parser.HasValue("test")

	End Sub

	<Test()>
	<ExpectedException(GetType(ArgumentNullException))>
	Public Sub Has_Member_Checks_For_Null_Member_Name()

		Dim parser As New ReflectionReplacementSource
		parser.SourceObject = New PropertyOnly

		parser.HasValue("")

	End Sub

	<Test()>
	Public Sub Has_Member_Can_Find_Property_Proper_Cased()

		Dim parser As New ReflectionReplacementSource
		parser.SourceObject = New PropertyOnly

		Assert.IsTrue(parser.HasValue("StringValue"))

	End Sub

	<Test()>
	Public Sub Has_Member_Can_Find_Property_Miss_Cased()

		Dim parser As New ReflectionReplacementSource
		parser.SourceObject = New PropertyOnly

		Assert.IsTrue(parser.HasValue("STRINGVALUE"))

	End Sub

	<Test()>
	Public Sub Has_Member_Cannot_Find_Write_Only_Property()

		Dim parser As New ReflectionReplacementSource
		parser.SourceObject = New WriteOnlyPropertyOnly

		Assert.IsFalse(parser.HasValue("StringValue"))

	End Sub

	<Test()>
	Public Sub Has_Member_Can_Find_Method_With_Return_Value_Proper_Cased()

		Dim parser As New ReflectionReplacementSource
		parser.SourceObject = New FunctionsOnly

		Assert.IsTrue(parser.HasValue("IntegerValue"))

	End Sub

	<Test()>
	Public Sub Has_Member_Can_Find_Method_With_Return_Value_Miss_Cased()

		Dim parser As New ReflectionReplacementSource
		parser.SourceObject = New FunctionsOnly

		Assert.IsTrue(parser.HasValue("INTEGERVALUE"))

	End Sub

	<Test()>
	Public Sub Has_Member_Cannot_Find_Method_Without_Return_Value_Proper_Cased()

		Dim parser As New ReflectionReplacementSource
		parser.SourceObject = New SubroutineOnly

		Assert.IsFalse(parser.HasValue("StringValue"))

	End Sub

	<Test()>
	Public Sub Has_Member_Cannot_Find_Method_Without_Return_Value_Miss_Cased()

		Dim parser As New ReflectionReplacementSource
		parser.SourceObject = New SubroutineOnly

		Assert.IsFalse(parser.HasValue("STRINGVALUE"))

	End Sub

	<Test()>
	Public Sub Has_Member_Can_Find_Field_Proper_Cased()

		Dim parser As New ReflectionReplacementSource
		parser.SourceObject = New FieldOnly

		Assert.IsTrue(parser.HasValue("StringValue"))

	End Sub

	<Test()>
	Public Sub Has_Member_Can_Find_Field_Miss_Cased()

		Dim parser As New ReflectionReplacementSource
		parser.SourceObject = New FieldOnly

		Assert.IsTrue(parser.HasValue("STRINGVALUE"))

	End Sub

	'
	'
	'

	<Test()>
	<ExpectedException(GetType(ArgumentNullException))>
	Public Sub Is_Collection_Checks_For_Null_Source_Object()

		Dim parser As New ReflectionReplacementSource
		parser.HasCollection("test")

	End Sub

	<Test()>
	<ExpectedException(GetType(ArgumentNullException))>
	Public Sub Is_Collection_Checks_For_Null_Member_Name()

		Dim parser As New ReflectionReplacementSource
		parser.SourceObject = New PropertyOnly

		parser.HasCollection("")

	End Sub

	<Test()>
	Public Sub Is_Collection_Checks_For_Arrays()

		Dim parser As New ReflectionReplacementSource
		parser.SourceObject = New ArrayTests

		Assert.IsTrue(parser.HasCollection("ArrayValue"))

	End Sub

	<Test()>
	Public Sub Is_Collection_Checks_For_IEnumerable()

		Dim parser As New ReflectionReplacementSource
		parser.SourceObject = New ArrayTests

		Assert.IsTrue(parser.HasCollection("IEnumerableValue"))

	End Sub

	<Test()>
	Public Sub Is_Collection_Checks_For_IEnumerable_Inheritance()

		Dim parser As New ReflectionReplacementSource
		parser.SourceObject = New ArrayTests

		Assert.IsTrue(parser.HasCollection("ListValue"))

	End Sub

	<Test()>
	Public Sub Is_Collection_Checks_For_Dictionaries()

		Dim parser As New ReflectionReplacementSource
		parser.SourceObject = New ArrayTests

		Assert.IsTrue(parser.HasCollection("DictionaryValue"))

	End Sub

	<Test()>
	Public Sub Is_Collection_Can_Find_Properties_Proper_Cased()

		Dim parser As New ReflectionReplacementSource
		parser.SourceObject = New ArrayTests

		Assert.IsTrue(parser.HasCollection("ArrayValue"))

	End Sub

	<Test()>
	Public Sub Is_Collection_Can_Find_Properties_Miss_Cased()

		Dim parser As New ReflectionReplacementSource
		parser.SourceObject = New ArrayTests

		Assert.IsTrue(parser.HasCollection("ARRAYVALUE"))

	End Sub

	<Test()>
	Public Sub Is_Collection_Can_Find_Methods_Proper_Cased()

		Dim parser As New ReflectionReplacementSource
		parser.SourceObject = New ArrayTests

		Assert.IsTrue(parser.HasCollection("ArrayFunction"))

	End Sub

	<Test()>
	Public Sub Is_Collection_Can_Find_Methods_Miss_Cased()

		Dim parser As New ReflectionReplacementSource
		parser.SourceObject = New ArrayTests

		Assert.IsTrue(parser.HasCollection("ARRAYFUNCTION"))

	End Sub

	<Test()>
	Public Sub Is_Collection_Can_Find_Fields_Proper_Cased()

		Dim parser As New ReflectionReplacementSource
		parser.SourceObject = New ArrayTests

		Assert.IsTrue(parser.HasCollection("ArrayField"))

	End Sub

	<Test()>
	Public Sub Is_Collection_Can_Find_Fields_Miss_Cased()

		Dim parser As New ReflectionReplacementSource
		parser.SourceObject = New ArrayTests

		Assert.IsTrue(parser.HasCollection("ARRAYFIELD"))

	End Sub

	<Test()>
	Public Sub Is_Collection_Does_Not_Have_False_Positives()

		Dim parser As New ReflectionReplacementSource
		parser.SourceObject = New ArrayTests

		Assert.IsFalse(parser.HasCollection("IntegerValue"))

	End Sub

	<Test()>
	Public Sub Is_Collection_Has_Fallback_Value()

		Dim parser As New ReflectionReplacementSource
		parser.SourceObject = New ArrayTests

		Assert.IsFalse(parser.HasCollection("wfwefe"))

	End Sub

	<Test()>
	Public Sub Is_Collection_Checks_For_Methods_With_No_Return_Value()

		Dim parser As New ReflectionReplacementSource
		parser.SourceObject = New SubroutineOnly

		Assert.IsFalse(parser.HasCollection("StringValue"))

	End Sub

	<Test()>
	Public Sub Is_Collection_Checks_For_Unsupported_Types()

		Dim parser As New ReflectionReplacementSource
		parser.SourceObject = New SubClassOnly

		Assert.IsFalse(parser.HasCollection("SubTest"))

	End Sub

	<Test()>
	Public Sub Is_Collection_Checks_For_Non_Array_Methods()

		Dim parser As New ReflectionReplacementSource
		parser.SourceObject = New FunctionsOnly

		Assert.IsFalse(parser.HasCollection("IntegerValue"))

	End Sub

	'
	'
	'

	<Test()>
	<ExpectedException(GetType(ArgumentNullException))>
	Public Sub Get_Value_Checks_For_Null_Source_Object()

		Dim parser As New ReflectionReplacementSource
		parser.GetValue("test")

	End Sub

	<Test()>
	<ExpectedException(GetType(ArgumentNullException))>
	Public Sub Get_Value_Checks_For_Null_Member_Name()

		Dim parser As New ReflectionReplacementSource
		parser.SourceObject = New FunctionsOnly

		parser.GetValue("")

	End Sub

	<Test()>
	Public Sub Get_Value_Returns_Value_From_Property()

		Dim parser As New ReflectionReplacementSource
		parser.SourceObject = New PropertyOnly With {.StringValue = "Test Value"}

		Assert.AreEqual("Test Value", parser.GetValue("StringValue"))

	End Sub

	<Test()>
	Public Sub Get_Value_Returns_Value_From_Readonly_Property()

		Dim parser As New ReflectionReplacementSource
		parser.SourceObject = New ReadonlyPropertyOnly

		Assert.AreEqual("Test Value", parser.GetValue("StringValue"))

	End Sub

	<Test()>
	Public Sub Get_Value_Returns_Nothing_From_Writeonly_Property()

		Dim parser As New ReflectionReplacementSource
		parser.SourceObject = New WriteOnlyPropertyOnly

		Assert.AreEqual(String.Empty, parser.GetValue("StringValue"))

	End Sub

	<Test()>
	Public Sub Get_Value_Returns_From_Function()

		Dim parser As New ReflectionReplacementSource
		parser.SourceObject = New FunctionsOnly

		Assert.AreEqual("1234", parser.GetValue("IntegerValue"))

	End Sub

	<Test()>
	Public Sub Get_Value_Returns_Nothing_From_Subroutine()

		Dim parser As New ReflectionReplacementSource
		parser.SourceObject = New SubroutineOnly

		Assert.AreEqual(String.Empty, parser.GetValue("IntegerValue"))

	End Sub

	<Test()>
	Public Sub Get_Value_Returns_From_Field()

		Dim parser As New ReflectionReplacementSource
		parser.SourceObject = New FieldOnly

		Assert.AreEqual("Test", parser.GetValue("StringValue"))

	End Sub

	<Test()>
	Public Sub Get_Value_Checks_For_SubClasses()

		Dim parser As New ReflectionReplacementSource
		parser.SourceObject = New SubClassOnly

		Assert.AreEqual(String.Empty, parser.GetValue("SubTest"))

	End Sub

	<Test()>
	Public Sub Get_Value_Returns_To_String_For_An_Object()

		Dim parser As New ReflectionReplacementSource
		parser.SourceObject = New SubClassProperties

		Assert.AreEqual("OverriddenToString", parser.GetValue("Overridden"))
		Assert.AreEqual(GetType(SubClassProperties.NoOverrides).FullName, parser.GetValue("Subbed"))

	End Sub

	'
	'
	'

	<Test()>
	<ExpectedException(GetType(ArgumentNullException))>
	Public Sub Get_Collection_Checks_For_Null_Source_Object()

		Dim parser As New ReflectionReplacementSource

		parser.GetCollection("tetton")

	End Sub

	<Test()>
	<ExpectedException(GetType(ArgumentNullException))>
	Public Sub Get_Collection_Checks_For_Null_Member_Name()

		Dim parser As New ReflectionReplacementSource
		parser.SourceObject = New FunctionsOnly

		parser.GetCollection("")

	End Sub

	<Test()>
	Public Sub Get_Collection_Handles_Empty_Object()

		Dim src As New ReflectionReplacementSource
		src.SourceObject = New EmptyObject

		Assert.AreEqual(Nothing, src.GetCollection("test"))

	End Sub

	<Test()>
	Public Sub Get_Collection_Handles_Non_Collection_Property()

		Dim src As New ReflectionReplacementSource
		src.SourceObject = New FunctionsOnly

		Assert.AreEqual(Nothing, src.GetCollection("IntegerValue"))

	End Sub
	<Test()>
	Public Sub Get_Collection_Returns_From_Property()

		Dim parser As New ReflectionReplacementSource
		parser.SourceObject = New PropertyOnly

		Dim result As IEnumerable = parser.GetCollection("StringValue")

		Assert.IsNotNull(result)
		Assert.IsTrue(result.GetEnumerator.MoveNext)

	End Sub

	<Test()>
	Public Sub Get_Collection_Returns_From_Readonly_Property()

		Dim parser As New ReflectionReplacementSource
		parser.SourceObject = New ReadonlyPropertyOnly

		Dim result As IEnumerable = parser.GetCollection("StringValue")

		Assert.IsNotNull(result)
		Assert.IsTrue(result.GetEnumerator.MoveNext)

	End Sub

	<Test()>
	Public Sub Get_Collection_Returns_From_Function()

		Dim parser As New ReflectionReplacementSource
		parser.SourceObject = New FunctionsOnly

		Dim result As IEnumerable = parser.GetCollection("StringValue")

		Assert.IsNotNull(result)
		Assert.IsTrue(result.GetEnumerator.MoveNext)

	End Sub

	<Test()>
	Public Sub Get_Collection_Returns_From_Fields()

		Dim parser As New ReflectionReplacementSource
		parser.SourceObject = New FieldOnly

		Dim result As IEnumerable = parser.GetCollection("StringValue")

		Assert.IsNotNull(result)
		Assert.IsTrue(result.GetEnumerator.MoveNext)

	End Sub

	'
	' Test Classes  - dont want to Mock them due to the set of tests are testing reflection
	'

	Private Class PropertyOnly
		Public Property StringValue As String = "Test Value"
	End Class

	Private Class ReadonlyPropertyOnly
		Public ReadOnly Property StringValue As String
			Get
				Return "Test Value"
			End Get
		End Property
	End Class

	Private Class WriteOnlyPropertyOnly
		Public WriteOnly Property StringValue As String
			Set(ByVal value As String)
				'...
			End Set
		End Property
	End Class

	Private Class FunctionsOnly

		Public Function IntegerValue() As Integer
			Return 1234
		End Function

		Public Function StringValue() As String
			Return "Test Value"
		End Function

	End Class

	Private Class FunctionOverloadeded

		Public Function StringValue() As String
			Return "noparams"
		End Function

		Public Function StringValue(ByVal param As Integer) As String
			Return "params"
		End Function
	End Class

	Private Class SubroutineOnly
		Public Sub StringValue()
			'...
		End Sub
	End Class

	Private Class FieldOnly
		Public StringValue As String = "Test"
	End Class

	Private Class PropertyAndFunction
		Public Property StringValue As String = "property"
		Public Function StringFunction() As String
			Return "function"
		End Function
	End Class

	Private Class ArrayTests

		Public Property ArrayValue As Integer()
		Public Property IEnumerableValue As IEnumerable = New List(Of Integer)
		Public Property ListValue As New List(Of Integer)
		Public Property DictionaryValue As New Dictionary(Of String, String)

		Public Function ArrayFunction() As Integer()
			Return New Integer() {19, 23, 4}
		End Function

		Public ArrayField As Integer()

		'flase positives etc
		Public Property IntegerValue As Integer
	End Class

	Private Class SubClassOnly

		Public Class SubTest
		End Class

	End Class

	Private Class SubClassProperties

		Public Property Subbed As New NoOverrides
		Public Property Overridden As New ToStringClass

		Public Class ToStringClass

			Public Overrides Function ToString() As String
				Return "OverriddenToString"
			End Function

		End Class

		Public Class NoOverrides

		End Class

	End Class

	Private Class EmptyObject

	End Class

End Class
