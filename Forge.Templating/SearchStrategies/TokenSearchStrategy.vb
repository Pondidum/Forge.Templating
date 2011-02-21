Imports System.Runtime.CompilerServices
Imports Forge.Templating.Interfaces
Imports System.Text.RegularExpressions

Namespace SearchStrategies

    Public Class TokenSearchStrategy
        Implements ISearchStrategy

        Private _template() As Char
        Private _replacements As IList(Of IReplacementSource)

        Public Sub New()
            _replacements = New List(Of IReplacementSource)
        End Sub

        Public WriteOnly Property Template() As Char() Implements ISearchStrategy.Template
            Set(ByVal value As Char())
                _template = value
            End Set
        End Property

        Public Property Replacements() As IList(Of IReplacementSource) Implements ISearchStrategy.Replacements
            Get
                Return _replacements
            End Get
            Set(ByVal value As IList(Of IReplacementSource))
                _replacements = value
            End Set
        End Property

        Public Function Parse() As String Implements ISearchStrategy.Parse



        End Function

        Private MustInherit Class Tag

            Public MustOverride ReadOnly Property Pattern() As String

        End Class

        Private Class MatchData

        End Class

        Private Class Matcher

            Private ReadOnly _template As String
            Private ReadOnly _tags As List(Of Tag)
            Private _results As List(Of MatchData)


            Public Sub New(ByVal template() As Char)

                If template Is Nothing Then Throw New ArgumentNullException("template")

                _template = template

            End Sub

            Public ReadOnly Property Matches() As List(Of MatchData)
                Get
                    Return _results
                End Get
            End Property

            Public Sub AddTags(ByVal ParamArray tags() As Tag)
                _tags.AddRange(tags)
            End Sub

            Public Sub Process()

                Dim tags = GetAllTags()




            End Sub

            Private Function GetAllTags() As IEnumerable(Of Match)

                Dim allMatches As New List(Of Match)

                '_tags.ForEach(Sub(t) allMatches.AddRange(Regex.Matches(_template, t.Pattern).Cast(Of Match)))

                Dim ordered = allMatches.OrderBy(Function(m) m.Index)

                If Not ValidateMatches(ordered) Then
                    Return New List(Of Match)
                End If

                Return ordered.ToList()

            End Function

            Private Function ValidateMatches(ByVal source As IEnumerable(Of Match)) As Boolean

                'i have a nice extension method for this, but no yield in vb and, i dont want external 
                'dependencies, and i'm not in the mood to convert this to c# atm.
                'source.Check((prev, current) => prev.index + pref.length >  current.index)

                Using iterator = source.GetEnumerator()

                    If Not iterator.MoveNext() Then
                        Return True
                    End If


                    Dim prev = iterator.Current

                    While iterator.MoveNext()

                        If prev.Index + prev.Length > iterator.Current.Index Then
                            Return False
                        End If

                        prev = iterator.Current

                    End While

                End Using

                Return True

            End Function

        End Class


    End Class

    Friend Module Extensions

        <Extension()>
        Public Function Compare(Of TSource)(ByVal source As IEnumerable(Of TSource), ByVal selector As Func(Of TSource, TSource, Boolean)) As Boolean

            Using iterator = source.GetEnumerator()

                If iterator.MoveNext() Then

                End If

            End Using

        End Function

    End Module


End Namespace
