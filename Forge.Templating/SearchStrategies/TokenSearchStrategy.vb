
Imports Forge.Templating.Extensions
Imports Forge.Templating.Interfaces
Imports System.Text.RegularExpressions
Imports Forge.Templating.SearchStrategies.Token

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

            Dim parser = New Matcher(_template)
            parser.AddTags(New ValueTag())
            parser.AddTags(New ForLoopTag())

        End Function





        'Private Class Node

        '    Public Sub New()

        '    End Sub

        '    Public Sub New(ByVal type As Tag.TagTypes, ByVal value As String)

        '    End Sub

        '    Public Property Parent As Node
        '    Public Property Children As List(Of Node)
        '    Public Property Value As String

        '    Public Sub AddChild(ByVal node As Node)

        '        If node Is Nothing Then Throw New ArgumentNullException("node")

        '        Children.Add(node)
        '        node.Parent = Me

        '    End Sub

        'End Class

        'Private Class ContentNode
        '    Inherits Node

        '    Public Sub New(ByVal value As String)

        '    End Sub
        'End Class

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

                Dim matches = GetAllTagMatches()


            End Sub

            Private Function GetAllTagMatches() As IEnumerable(Of MatchData)

                Dim allMatches As New List(Of MatchData)

                For Each tag In _tags

                    For Each match As Match In Regex.Matches(_template, tag.Pattern)

                        allMatches.Add(New MatchData(match, tag))

                    Next

                Next

                Dim ordered = allMatches.OrderBy(Function(m) m.Index).ToList()

                Dim index = ordered.First().Index
                If index <> 0 Then
                    ordered.Insert(0, New MatchData(0, ordered.First().Index, _template.Range(0, index), New  )
                End If

                For i As Integer = 1 To ordered.Count - 1

                Next

                If Not ValidateMatches(ordered) Then
                    Return New List(Of MatchData)
                End If

                Return ordered.ToList()

            End Function

            Private Function ValidateMatches(ByVal source As IEnumerable(Of MatchData)) As Boolean

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


End Namespace
