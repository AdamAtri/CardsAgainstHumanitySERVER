Public Class VoteHandler

    Public Function CastVote(thePlayerHand As PlayerHand) As PlayerHand
        Dim result As PlayerHand = CAH_Repository.CastVote(thePlayerHand)
        Dim round As Int32 = CAH_Repository.GetRoundByHandID(thePlayerHand.HandID)
        Dim game As Int32 = CAH_Repository.GetGameByHandID(thePlayerHand.HandID)
        If ActiveGameTracker.PlayerAccountedFor(game) Then
            TallyVotes(round)
        End If
        Return result
    End Function

    Private Sub TallyVotes(ByVal round As Int32)
        Dim t As New Task(Sub()
                              Dim Tally As List(Of Ballot) = CAH_Repository.GetVotesByRound(round)

                              Dim winner = _
                                  From b As Ballot In Tally.AsEnumerable
                                  Group b By b.Username Into Group _
                                  Let maxVotes = Group.Max(Function(v) v.NumVotes) _
                                  Select Username, MostVotes = Group.Where(Function(v) v.NumVotes = maxVotes)

                              For Each w In winner.ToList
                                  CAH_Repository.SubmitWinner(round, w.Username)
                              Next

                              ActiveGameTracker.VotesHaveBeenCast(CAH_Repository.GetGameByRoundID(round))
                          End Sub)
        t.Start()
    End Sub

    Function GetRoundWinner(roundID As Int32) As List(Of Ballot)
        Dim game As Int32 = CAH_Repository.GetGameByRoundID(roundID)
        If ActiveGameTracker.AllVotesCast(game) Then
            If ActiveGameTracker.PlayerAccountedFor(game) Then
                If Not ActiveGameTracker.NextRound(game) Then
                    CalcGameWinner(game)
                End If
            End If
            Return CAH_Repository.GetVotesByRound(roundID)
        Else
            Throw New WaitingForAllVotesException(New CustomErrorDetail With { _
                                                  .ErrorInfo = "NOT ALL VOTES CAST", _
                                                  .ErrorDetail = "Not all players have voted. Try again."})
        End If
    End Function

    Private Sub CalcGameWinner(game As Integer)
        Dim t As New Task(Sub() CAH_Repository.CalcWinner(game))
        t.Start()
    End Sub

    Function GetGameWinner(gameID As Int32) As List(Of Ballot)
        Dim result As List(Of Ballot) = CAH_Repository.GetWinnersForGame(gameID)
        If ActiveGameTracker.PlayerAccountedFor(gameID) Then
            CAH_Repository.FinishGame(gameID)
            ActiveGameTracker.FinishGame(gameID)
        End If
        Return result
    End Function


End Class
