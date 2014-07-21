
Module ActiveGameTracker

    Public ReadOnly ActiveGameTable As New DataTable
    Public Initialized As Boolean = False
    Public rand As New Random()
    Public Const MAX_PLAYERS As Int32 = 2
    Public Const MAX_ROUNDS As Int32 = 5

    Public Sub Intialize()
        With ActiveGameTable
            .TableName = "ActiveGamesTable"
            With .Columns
                .Add("GameID", Type.GetType("System.Int32"))
                .Add("NumPlayers", Type.GetType("System.Int32"))
                .Add("CurrentRound", Type.GetType("System.Int32"))
                .Add("HandsReady", Type.GetType("System.Boolean"))
                .Add("RoundStartTime", Type.GetType("System.DateTime"))
                .Add("AllSelectionsMade", Type.GetType("System.Boolean"))
                .Add("AllVotesCast", Type.GetType("System.Boolean"))
                .Add("PlayersAccountedFor", Type.GetType("System.Int32"))
            End With
            .PrimaryKey = {ActiveGameTable.Columns("GameID")}
        End With
        Initialized = True
        ReadActiveTable()
    End Sub
    Sub AddNewGame(theGameID As Integer)
        Dim row As DataRow = ActiveGameTable.NewRow
        row("GameID") = theGameID
        row("NumPlayers") = 0
        row("CurrentRound") = GameStatus.ENROLLING
        row("HandsReady") = False
        row("AllSelectionsMade") = False
        row("AllVotesCast") = False
        row("PlayersAccountedFor") = 0
        ActiveGameTable.Rows.Add(row)
        ActiveGameTable.AcceptChanges()
    End Sub
    Public Function GetGameStatus(ByVal gameID As Int32) As GameStatus
        Dim row As DataRow = ActiveGameTable.Rows.Find(gameID)
        If row IsNot Nothing Then
            Return CType(row("CurrentRound"), GameStatus)
        Else
            Return GameStatus.FINISHED
        End If
    End Function
    Public Sub StartGame(ByVal game As Int32, ByVal numPlayers As Int32)
        Dim row As DataRow = ActiveGameTable.Rows.Find(game)
        row("NumPlayers") = numPlayers
        row("CurrentRound") = GameStatus.ROUND1
        ActiveGameTable.AcceptChanges()
    End Sub

    Sub HandsReady(ByVal game As Int32)
        Dim row As DataRow = ActiveGameTable.Rows.Find(game)
        row("HandsReady") = True
        ActiveGameTable.AcceptChanges()
    End Sub

    Function AllHandsReady(ByVal game As Int32) As Boolean
        Return CBool(ActiveGameTable.Rows.Find(game)("HandsReady"))
    End Function

    Public Sub FinishGame(ByVal game As Int32)
        ActiveGameTable.Rows.Remove(ActiveGameTable.Rows.Find(game))
        ActiveGameTable.AcceptChanges()
    End Sub

    Public Function NextRound(ByVal game As Int32) As Boolean
        Dim stillPlaying As Boolean = True
        Dim row As DataRow = ActiveGameTable.Rows.Find(game)
        row("CurrentRound") = CInt(row("CurrentRound")) + 1
        row("RoundStartTime") = DateTime.Now
        row("AllSelectionsMade") = False
        row("AllVotesCast") = False
        row("PlayersAccountedFor") = 0
        ActiveGameTable.AcceptChanges()

        If CInt(row("CurrentRound")) > GameStatus.ROUND5 Then
            stillPlaying = False
        End If
        Return stillPlaying
    End Function

    Public Sub SelectionsHaveBeenMade(ByVal game As Int32)
        Dim row As DataRow = ActiveGameTable.Rows.Find(game)
        row("AllSelectionsMade") = True
        row("PlayersAccountedFor") = 0
        ActiveGameTable.AcceptChanges()
    End Sub

    Public Function AllSelectionsMade(ByVal game As Int32) As Boolean
        Dim row As DataRow = ActiveGameTable.Rows.Find(game)
        Return CBool(row("AllSelectionsMade"))
    End Function

    Public Sub VotesHaveBeenCast(ByVal game As Int32)
        Dim row As DataRow = ActiveGameTable.Rows.Find(game)
        row("AllVotesCast") = True
        row("PlayersAccountedFor") = 0
        ActiveGameTable.AcceptChanges()
    End Sub

    Public Function AllVotesCast(ByVal game As Int32) As Boolean
        Dim row As DataRow = ActiveGameTable.Rows.Find(game)
        Return CBool(row("AllVotesCast"))
    End Function

    Public Function PlayerAccountedFor(ByVal game As Int32) As Boolean
        Dim row As DataRow = ActiveGameTable.Rows.Find(game)
        row("PlayersAccountedFor") = CInt(row("PlayersAccountedFor")) + 1
        ActiveGameTable.AcceptChanges()
        'FIX
        'This is not the most elegant solution.
        'Add player tracker to ActiveGameTracker.
        If CInt(row("NumPlayers")) <= CInt(row("PlayersAccountedFor")) Then
            Return True
        Else
            Return False
        End If
    End Function

    Function GetRound(GameID As Int32) As Integer
        Return CInt(ActiveGameTable.Rows.Find(GameID)("CurrentRound"))
    End Function

    Sub SaveActiveTable()
        If ActiveGameTable.Rows.Count > 0 Then
            Try
                Dim ActiveTableXMLFile As String = My.Application.Info.DirectoryPath & "/ActiveTable.xml"
                ActiveGameTable.WriteXml(ActiveTableXMLFile)
            Catch ex As Exception
                ExceptionLogger.WriteException(ex, "ACTIVE_GAME_TRKR.SAVETABLE")
            End Try
        End If
    End Sub

    Sub ReadActiveTable()
        Try
            Dim ActiveTableXMLFile As String = My.Application.Info.DirectoryPath & "/ActiveTable.xml"
            If System.IO.File.Exists(ActiveTableXMLFile) Then
                ActiveGameTable.ReadXml(ActiveTableXMLFile)
            End If
            System.IO.File.Delete(ActiveTableXMLFile)
        Catch ex As Exception
            ExceptionLogger.WriteException(ex, "ACTIVE_GAME_TRKR.READTABLE")
        End Try
    End Sub
    

End Module
