Imports System.Reflection

Public Class ReflectionReplacementSource
	Implements IReplacementSource

	Private ReadOnly SearchFlags As BindingFlags = BindingFlags.Instance Or BindingFlags.Public Or BindingFlags.Static Or BindingFlags.IgnoreCase
	Public Property SourceObject As Object

	Public Sub New()
	End Sub

	Public Sub New(ByVal source As Object)
		SourceObject = source
	End Sub

	Public ReadOnly Property Name As String Implements IReplacementSource.Name
		Get
			If SourceObject Is Nothing Then Throw New ArgumentNullException("SourceObject")

			Return SourceObject.GetType.Name
		End Get
	End Property

	Public Function HasCollection(ByVal name As String) As Boolean Implements IReplacementSource.HasCollection

		If SourceObject Is Nothing Then Throw New ArgumentNullException("SourceObject")
		If String.IsNullOrWhiteSpace(name) Then Throw New ArgumentNullException("Name", "The name should be non whitespace")

		Dim type As Type = SourceObject.GetType

		Dim info As MemberInfo = GetMemberInfo(type.GetMember(name, SearchFlags))

		If info Is Nothing Then
			Return Nothing
		End If

		Return IsTypeCollection(info)

	End Function

	Public Function HasValue(ByVal name As String) As Boolean Implements IReplacementSource.HasValue

		If SourceObject Is Nothing Then Throw New ArgumentNullException("SourceObject") ' Return False
		If String.IsNullOrWhiteSpace(name) Then Throw New ArgumentNullException("Name", "The name should be non whitespace")

		Dim info() As MemberInfo = SourceObject.GetType.GetMember(name, SearchFlags)

		Return GetMemberInfo(info) IsNot Nothing

	End Function

	Public Function GetValue(ByVal name As String) As String Implements IReplacementSource.GetValue

		If SourceObject Is Nothing Then Throw New ArgumentNullException("SourceObject")
		If String.IsNullOrWhiteSpace(name) Then Throw New ArgumentNullException("Name", "The name should be non whitespace")

		Dim mi As MemberInfo = GetMemberInfo(SourceObject.GetType.GetMember(name, SearchFlags))

		If mi Is Nothing Then
			Return String.Empty
		End If

		Select Case mi.MemberType
			Case MemberTypes.Property
				Return DirectCast(mi, PropertyInfo).GetValue(SourceObject, Nothing).ToString

			Case MemberTypes.Method
				Return DirectCast(mi, MethodInfo).Invoke(SourceObject, Nothing).ToString

			Case MemberTypes.Field
				Return DirectCast(mi, FieldInfo).GetValue(SourceObject).ToString

		End Select

		Return String.Empty

	End Function

	Public Function GetCollection(ByVal name As String) As IEnumerable Implements IReplacementSource.GetCollection

		If SourceObject Is Nothing Then Throw New ArgumentNullException("SourceObject") ' Return False
		If String.IsNullOrWhiteSpace(name) Then Throw New ArgumentNullException("Name", "The name should be non whitespace")

		Dim mi As MemberInfo = GetMemberInfo(SourceObject.GetType.GetMember(name, SearchFlags))

		If mi Is Nothing Then
			Return Nothing
		End If

		If Not IsTypeCollection(mi) Then
			Return Nothing
		End If

		Select Case mi.MemberType

			Case MemberTypes.Property
				Return CType(DirectCast(mi, PropertyInfo).GetValue(SourceObject, Nothing), IEnumerable)

			Case MemberTypes.Method
				Return CType(DirectCast(mi, MethodInfo).Invoke(SourceObject, Nothing), IEnumerable)

			Case MemberTypes.Field
				Return CType(DirectCast(mi, FieldInfo).GetValue(SourceObject), IEnumerable)

		End Select

		Return Nothing

	End Function


	Private Function IsTypeCollection(ByVal info As MemberInfo) As Boolean

		Select Case info.MemberType

			Case MemberTypes.Property
				If IsArrayOrIEnumerable(DirectCast(info, PropertyInfo).PropertyType) Then
					Return True
				End If

			Case MemberTypes.Method
				If IsArrayOrIEnumerable(DirectCast(info, MethodInfo).ReturnType) Then
					Return True
				End If

			Case MemberTypes.Field
				If IsArrayOrIEnumerable(DirectCast(info, FieldInfo).FieldType) Then
					Return True
				End If

		End Select

		Return False

	End Function

	Private Function GetMemberInfo(ByVal info() As MemberInfo) As MemberInfo

		If info.Count <= 0 Then
			Return Nothing
		End If

		For Each mi As MemberInfo In info

			Select Case mi.MemberType

				Case MemberTypes.Property

					If DirectCast(mi, PropertyInfo).CanRead Then
						Return mi
					End If

				Case MemberTypes.Method

					Dim method As MethodInfo = DirectCast(mi, MethodInfo)

					If method.ReturnType <> GetType(Void) AndAlso method.GetParameters.Count = 0 Then
						Return mi
					End If

				Case MemberTypes.Field

					Return mi

			End Select

		Next

		Return Nothing

	End Function

	Private Function IsArrayOrIEnumerable(ByVal type As Type) As Boolean

		If type.IsArray Then
			Return True
		End If

		If type.GetInterface("IEnumerable") IsNot Nothing Then
			Return True
		End If

		If type Is GetType(IEnumerable) Then
			Return True
		End If

		Return False
	End Function

End Class
