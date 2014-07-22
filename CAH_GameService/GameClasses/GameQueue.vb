Public Class GameQueue

    ''' <summary>
    ''' Join Game will return a Game_Player token that identifies the player's
    ''' new game. When enough players have joined, the function will also initiate
    ''' the GAME start process.
    ''' </summary>
    ''' <param name="aPlayerID"></param>
    ''' <returns>Game_Player token</returns>
    ''' <remarks>Conditional: GAME Start</remarks>
    Public Function QueuePlayer(ByVal aPlayerID As Int32) As String
        If CheckPlayerForOpenGames(CInt(aPlayerID)) Then
            Dim GamePlayerID As String = String.Empty
            Dim theGameID As Int32 = Nothing
            Dim AvailableGames = CAH_Repository.GetAvailableGames
            If AvailableGames.Count = 0 Then
                theGameID = CreateNewGame()
                CAH_Repository.ModifyGameStatus(theGameID, GameStatus.ENROLLING)
                ActiveGameTracker.AddNewGame(theGameID)
                GamePlayerID = CAH_Repository.AddPlayerToGame(theGameID, CInt(aPlayerID))
                Dim T As New Task(Sub()
                                      Dim TT As New TimerThread
                                      TT.Start(theGameID)
                                  End Sub)
                T.Start()
            Else
                theGameID = AvailableGames(0)
                If CAH_Repository.GamePlayerCount(theGameID) < MAX_PLAYERS Then
                    GamePlayerID = CAH_Repository.AddPlayerToGame(theGameID, aPlayerID)
                Else
                    Throw New MissedGameException(New CustomErrorDetail With { _
                                                  .ErrorInfo = "PLAYER NOT ADDED", _
                                                  .ErrorDetail = "An error occured while adding the player to a game. Try again. JOIN GAME"})
                End If
            End If

            If CAH_Repository.GamePlayerCount(theGameID) = MAX_PLAYERS Then
                StartGame(theGameID)
            End If

            Return GamePlayerID
        Else
            Return CAH_Repository.GetGamePlayerToken(aPlayerID)
        End If
    End Function


    Private Function CreateNewGame() As Int32
        Return CAH_Repository.CreateGame()
    End Function

    ''' <summary>
    ''' This is a validation check to see if the player is already participating in an 
    ''' active or enrolling game. If so then do not proceed.
    ''' </summary>
    ''' <param name="aPlayerID"></param>
    ''' <returns>Proceed (Bool)</returns>
    ''' <remarks></remarks>
    Private Function CheckPlayerForOpenGames(aPlayerID As Integer) As Boolean
        Return CAH_Repository.PlayerHasOpenGames(aPlayerID)
    End Function

    Private Sub StartGame(ByVal theGameID As Int32)
        Dim t As New Task(Sub() CAH_Repository.StartGame(theGameID))
        t.Start()
    End Sub



End Class

