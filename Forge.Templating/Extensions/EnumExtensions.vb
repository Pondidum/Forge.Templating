Imports System.Runtime.CompilerServices

Namespace Extensions

    Friend Module EnumExtensions

        <Extension()>
        Public Function IsAny(Of T As Structure)(ByVal self As T, ByVal ParamArray items() As T) As Boolean

            Check.Self(self)
            Check.Collection(items, "items")

            Return items.Contains(self)

        End Function


        <Extension()>
        Public Function HasAll(Of T As Structure)(ByVal self As T, ByVal ParamArray items() As T) As Boolean

            Check.Self(self)
            Check.Collection(items, "items")

            Return items.All(Function(x) self.Has(x))

        End Function

        <Extension()> _
        Public Function HasAny(Of T As Structure)(ByVal self As T, ByVal ParamArray items() As T) As Boolean

            Check.Self(self)
            Check.Collection(items, "items")

            Return items.Any(Function(x) self.Has(x))

        End Function

        <Extension()>
        Public Function Has(Of T As Structure)(ByVal self As T, ByVal item As T) As Boolean

            Check.Self(self)
            Check.Self(item, "item")

            Dim selfInteger = Convert.ToInt32(self)
            Dim itemInteger = Convert.ToInt32(item)

            Return (selfInteger And itemInteger) = itemInteger

        End Function
    End Module

End Namespace
