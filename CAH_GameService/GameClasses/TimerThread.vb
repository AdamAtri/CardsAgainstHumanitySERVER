Public Class TimerThread

    Public Sub Start(ByVal gameID As Int32)
        Dim MAX_TIME As Int32 = 120
        Dim MIN_PLAYERS As Int32 = 2
        Dim count As Int32 = 0
        Dim status As GameStatus = ActiveGameTracker.GetGameStatus(gameID)

        While status = GameStatus.ENROLLING
            If CAH_Repository.GamePlayerCount(gameID) >= MIN_PLAYERS Then
                If count = MAX_TIME Then
                    CAH_Repository.StartGame(gameID)
                Else
                    Threading.Thread.Sleep(990)
                End If

                status = ActiveGameTracker.GetGameStatus(gameID)
            End If
            count += 1
        End While

    End Sub

End Class
