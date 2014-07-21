Public Class SelectionHandler

    Function SubmitSelection(thePlayerHand As PlayerHand) As PlayerHand
        If CAH_Repository.AlreadySubmittedHand(thePlayerHand) Then
            Throw New AlreadySubmittedHandException(New CustomErrorDetail With { _
                                                    .ErrorInfo = "ALREADY SUBMITTED HAND", _
                                                    .ErrorDetail = "Hand " & thePlayerHand.HandID & " has already been submitted."})
        End If

        Dim game As Int32 = CAH_Repository.GetGameByHandID(thePlayerHand.HandID)
        If ActiveGameTracker.AllSelectionsMade(game) Then
            Throw New SubmitTooQuicklyException(New CustomErrorDetail With { _
                                                .ErrorInfo = "ROUND NOT OVER", _
                                                .ErrorDetail = "You cannot submit cards for a new round until that round has begun."})
        End If

        Dim pickCount As Int32 = CAH_Repository.GetPickCount(thePlayerHand.HandID)
        For i As Int32 = 0 To pickCount - 1
            If thePlayerHand.Selection(i) = String.Empty Then
                Throw New NotEnoughSelectionsException(New CustomErrorDetail With { _
                                                     .ErrorInfo = "NOT ENOUGH SELECTIONS", _
                                                     .ErrorDetail = "There are not enough selections in your hand."})
            End If
        Next
        If pickCount < 3 Then
            If Not thePlayerHand.Selection(pickCount) = String.Empty Then
                Throw New TooManySelectionsException(New CustomErrorDetail With { _
                                                     .ErrorInfo = "TOO MANY SELECTIONS", _
                                                     .ErrorDetail = "There are too many selections in your hand."})
            End If
        End If
        Dim result As PlayerHand = CAH_Repository.RecordSelection(thePlayerHand)
        If ActiveGameTracker.PlayerAccountedFor(game) Then
            ActiveGameTracker.SelectionsHaveBeenMade(game)
        End If
        Return result

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

