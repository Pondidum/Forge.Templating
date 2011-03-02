Imports Forge.Templating.Extensions
Imports System.Text.RegularExpressions

Namespace SearchStrategies.Token

    Friend Class TemplateParser

        Private ReadOnly _template As String

        Public Sub New(ByVal template() As Char)

            If template Is Nothing Then Throw New ArgumentNullException("template")

            _template = template

        End Sub
        
        Public Function Process() As MatchData

            Dim matches = GetAllTagMatches()

            Return CreateTree(matches)

        End Function

        Private Function CreateTree(ByVal collection As IEnumerable(Of MatchData)) As MatchData

            Dim root = New MatchData(0, _template.Length, Nothing, TagRepository.TagTypes.Composite)
            Dim parent = root

            For Each match In collection

                While match.Index > (parent.Index + parent.Length)
                    parent = parent.Parent
                End While

                parent.AddChild(match)

                If match.Type.Has(TagRepository.TagTypes.Composite) Then

                    parent.AddChild(match)
                    parent = match

                End If

            Next

            Return root

        End Function

        Private Function GetAllTagMatches() As IEnumerable(Of MatchData)

            Dim allMatches As New List(Of MatchData)

            For Each tag In TagRepository.All

                For Each match As Match In Regex.Matches(_template, tag.Value)

                    allMatches.Add(TagRepository.Create(match.Index, match.Length, match.Value.ToArray(), tag.Key))

                Next

            Next

            Dim ordered = allMatches.OrderBy(Function(m) m.Index).ToList()
            Dim previous = New MatchData(0, 0, Nothing, TagRepository.TagTypes.Composite)

            For i As Integer = 1 To ordered.Count - 1

                Dim current = ordered(i)
                Dim length = current.Index - (previous.Index + previous.Length)

                If length > 0 Then

                    ordered.Insert(i, New MatchData(previous.Index + previous.Length,
                                                    length,
                                                    _template.Range(previous.Index + previous.Length, length),
                                                    TagRepository.TagTypes.Content))
                End If

                previous = current

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
