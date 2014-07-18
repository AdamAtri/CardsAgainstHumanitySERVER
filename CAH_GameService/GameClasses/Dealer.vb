Public Class Dealer

    Public Function DealHand(gameID As Int32, playerID As Int32) As PlayerHand()
        If ActiveGameTracker.AllHandsReady(gameID) Then
            Try
                Dim result = CAH_Repository.GetPlayerHands(gameID, playerID)
                If ActiveGameTracker.PlayerAccountedFor(gameID) Then
                    ActiveGameTracker.NextRound(gameID)
                End If
                Return result

            Catch ex As Exception
                Throw
            End Try
        Else
            Throw New HandsNotReadyException(New CustomErrorDetail With { _
                                             .ErrorInfo = "Hands are not ready.", _
                                             .ErrorDetail = "The hands for this game have not been generated yet. Try Again."})
        End If

    End Function

End Class


