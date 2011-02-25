Imports Forge.Templating.Extensions
Imports System.Text.RegularExpressions

Namespace SearchStrategies.Token

    Friend Class TemplateParser

        Private ReadOnly _template As String
        Private ReadOnly _tags As Dictionary(Of Tag.TagTypes, Tag)

        Public Sub New(ByVal template() As Char)

            If template Is Nothing Then Throw New ArgumentNullException("template")

            _template = template

        End Sub

        Public Sub AddTags(ByVal ParamArray tags() As Tag)
            Array.ForEach(tags, Sub(t) _tags.Add(t.Type, t))
        End Sub

        Public Function Process() As MatchData

            Dim matches = GetAllTagMatches()

            Return CreateTree(matches)

        End Function

        Private Function CreateTree(ByVal collection As IEnumerable(Of MatchData)) As MatchData

            Dim root = New MatchData(0, _template.Length, String.Empty, _tags(Tag.TagTypes.Composite))
            Dim parent = root

            For Each match In collection

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

End Namespace
