
Public Class RoundInfoHandler

    Function GetRoundInfo(GameID As Int32) As RoundStuff
        Dim round As Int32 = ActiveGameTracker.GetRound(GameID)
        If round > 0 And round <= MAX_ROUNDS Then
            Return CAH_Repository.RoundInfoByGameID(GameID, round)
        Else
            Throw New InvalidRoundException(New CustomErrorDetail With { _
                                            .ErrorInfo = "INVALID ROUND NUMBER", _
                                            .ErrorDetail = "The request for round info is DENIED because the round value is outside the accepted range of values."})
        End If
    End Function

End Class

