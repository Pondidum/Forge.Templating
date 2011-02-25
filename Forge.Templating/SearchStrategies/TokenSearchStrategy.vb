Imports Forge.Templating.Extensions
Imports Forge.Templating.Interfaces
Imports Forge.Templating.SearchStrategies.Token
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

            Dim parser = New Matcher(_template)
            parser.AddTags(New ValueTag())
            parser.AddTags(New ForLoopTag())

        End Function

        Private Class Matcher

            Private ReadOnly _template As String
            Private ReadOnly _tags As Dictionary(Of Tag.TagTypes, Tag)
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
                Array.ForEach(tags, Sub(t) _tags.Add(t.Type, t))
            End Sub

            Public Sub Process()

                Dim matches = GetAllTagMatches()
                Dim tree = CreateTree(matches)

            End Sub

            Private Function CreateTree(ByVal collection As IEnumerable(Of MatchData)) As MatchData

                Dim root = New MatchData(0, _template.Length, String.Empty, _tags(Tag.TagTypes.Composite))
                Dim parent = root

                For Each match In Matches

                    While match.Index > (parent.Index + parent.Length)
                        parent = parent.Parent
                    End While

                    parent.AddChild(match)

                    If match.Tag.Type = Tag.TagTypes.Composite Then

                        parent.AddChild(match)
                        parent = match

                    End If

                Next

                Return root

            End Function

            Private Function GetAllTagMatches() As IEnumerable(Of MatchData)

                Dim allMatches As New List(Of MatchData)

                For Each tag In _tags.Values

                    For Each match As Match In Regex.Matches(_template, tag.Pattern)

                        allMatches.Add(New MatchData(match, tag))

                    Next

                Next

                Dim ordered = allMatches.OrderBy(Function(m) m.Index).ToList()

                'Dim index = ordered.First().Index

                'If index <> 0 Then
                '    ordered.Insert(0, New MatchData(0,
                '                                    ordered.First().Index,
                '                                    _template.Range(0, index),
                '                                    _tags(Tag.TagTypes.Content)))
                'End If

                'Dim x =
                Dim prev = New MatchData(0, 0, String.Empty, Nothing)

                For i As Integer = 1 To ordered.Count - 1

                    Dim current = ordered(i)
                    Dim length = current.Index - (prev.Index + prev.Length)

                    If length > 0 Then

                        ordered.Insert(i, New MatchData(prev.Index + prev.Length,
                                                        length,
                                                        _template.Range(prev.Index + prev.Length, length),
                                                        _tags(Tag.TagTypes.Content)))
                    End If

                    prev = current

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
