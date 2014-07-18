Public Class SelectionHandler

    Function SubmitSelection(thePlayerHand As PlayerHand) As PlayerHand
        Dim game As Int32 = CAH_Repository.GetGameByHandID(thePlayerHand.HandID)
        If ActiveGameTracker.AllSelectionsMade(game) Then
            Throw New SubmitTooQuicklyException(New CustomErrorDetail With { _
                                                .ErrorInfo = "ROUND NOT OVER", _
                                                .ErrorDetail = "You cannot submit cards for a new round until that round has begun."})
        Else
            Dim result As PlayerHand = CAH_Repository.RecordSelection(thePlayerHand)
            If ActiveGameTracker.PlayerAccountedFor(game) Then
                ActiveGameTracker.SelectionsHaveBeenMade(game)
            End If
            Return result
        End If
    End Function

    Function GetAllSelections(theRoundID As Int32) As PlayerSelection()
        Dim game As Int32 = CAH_Repository.GetGameByRoundID(theRoundID)
        If ActiveGameTracker.AllSelectionsMade(game) Then
            Dim result As PlayerSelection() = CAH_Repository.GetSelectionsByRound(theRoundID)
            Return result
        Else
            Throw New WaitingForAllSelectionsException(New CustomErrorDetail With { _
                                                       .ErrorInfo = "Waiting for all selections.", _
                                                       .ErrorDetail = "Not all players have made their selections. Try again."})
        End If
    End Function

End Class

