Public Class Dealer

    Public Function DealHands(gameID As Int32, playerID As Int32) As PlayerHand()

        Dim result As PlayerHand()
        If ActiveGameTracker.AllHandsReady(gameID) Then
            Try
                result = CAH_Repository.GetPlayerHands(gameID, playerID)
            Catch ex As Exception
                Throw
            End Try
        Else
            Throw New HandsNotReadyException(New CustomErrorDetail With { _
                                             .ErrorInfo = "Hands are not ready.", _
                                             .ErrorDetail = "The hands for this game have not been generated yet. Try Again."})
        End If

        If ActiveGameTracker.GetGameStatus(gameID) = GameStatus.ENROLLING Then
            Try
                If ActiveGameTracker.PlayerAccountedFor(gameID) Then
                    ActiveGameTracker.NextRound(gameID)
                End If
            Catch ex As Exception
                Throw New GenericHandledException(New CustomErrorDetail With { _
                                                   .ErrorDetail = ex.Message, _
                                                   .ErrorInfo = "DEALHAND_" & ex.GetType.ToString
                                                  })
            End Try
        End If
        Return result

    End Function

End Class


