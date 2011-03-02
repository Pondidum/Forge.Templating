Namespace Extensions

    Friend Module Check

        Public Sub Self(ByVal self As Object)

            Check.Self(self, "self")

        End Sub

        Public Sub Self(ByVal self As Object, ByVal name As String)

            If self Is Nothing Then Throw New ArgumentNullException(name)

        End Sub

        Public Sub Collection(ByVal collection As ICollection)

            Check.Collection(collection, "collection")

        End Sub

        Public Sub Collection(ByVal collection As ICollection, ByVal name As String)

            If collection Is Nothing Then Throw New ArgumentNullException(name)

        End Sub

    End Module

End Namespace
