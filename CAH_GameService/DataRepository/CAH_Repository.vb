Option Strict On
Imports CAH_DataLibrary.CAH_EntitiesLibrary
Imports CAH_EntitiesLibrary

Module CAH_Repository

    Private Repository As New CAH_Entities
    Private BLACKCARDS_XML As String = "C:\BlackCardsList.xml"

#Region "   ***INITIALIZATION***   "
    ''' <summary>
    ''' Verifies that the BLACKCARD table is populated, and creates the black cards from an XML file if they are not.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub CheckBlackCards()
        'Console.WriteLine("Checking Repository for BlackCardTable.")
        If Repository.BlackCards.ToList.Count <= 0 Then

            Dim xml = XDocument.Load(BLACKCARDS_XML)
            For Each e As XElement In xml.Descendants("BlackCard")
                Dim card As New BlackCard With {.BCID = CInt(e.<BCID>.Value), _
                                                .Sentence = CType(e.<Sentence>.Value, String), _
                                                .PickCount = CInt(e.<PickCount>.Value)}
                Repository.BlackCards.Add(card)
            Next

            Try
                Repository.SaveChanges()
            Catch ex As Entity.Validation.DbEntityValidationException
                MsgBox(ex.ToString)
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
        End If
        'Console.WriteLine("Found details for BlackCardTable.")
    End Sub
#End Region

#Region "   ***PLAYER MANAGER FUNCTIONS***   "

    Function PlayerAlreadyExists(aNewPlayer As NewPlayer) As Boolean
        Try
            Dim thePlayer As Player = Repository.Players.Where(Function(p) p.Username = aNewPlayer.Username).First
            If thePlayer IsNot Nothing Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function CreatePlayer(ByRef aNewPlayer As NewPlayer) As NewPlayer
        Dim PC As New PlayerConverter
        Dim P As Player = PC.Convert(aNewPlayer)
        Repository.Players.Add(P)
        Repository.SaveChanges()
        aNewPlayer = PC.ConvertBack(P)
        Return aNewPlayer
    End Function

    Function Login(uName As String, encPW As String) As NewPlayer
        Dim thePlayer As Player = Repository.Players.Where(Function(p) p.Username = uName And p.Password = encPW).First
        If thePlayer Is Nothing Then
            Throw New InvalidLoginAttemptException(New CustomErrorDetail With { _
                                                    .ErrorInfo = "LOGIN FAILED", _
                                                    .ErrorDetail = "That username and password did work, silly. <blank> a different set."})
        End If
        Dim PC As New PlayerConverter
        Return PC.ConvertBack(thePlayer)
    End Function

    Public Function GetProxyPlayerByID(ByVal playerID As Int32) As NewPlayer
        Dim thePlayer As Player = Repository.Players.Find(playerID)
        Dim PC As New PlayerConverter
        Return PC.ConvertBack(thePlayer)
    End Function
#End Region

#Region "   ***GAME QUEUE FUNCTIONS***   "
    Public Function GetPlayerByID(ByVal playerID As Int32) As Player
        Return Repository.Players.Find(playerID)
    End Function

    Public Function GetAvailableGames() As ICollection(Of Int32)
        Dim AvailableGames = _
            From aGame In Repository.Games _
            Where aGame.GameStatus = GameStatus.ENROLLING _
            Or aGame.GameStatus = GameStatus.CREATED _
            Select aGame.GameID

        Return AvailableGames.ToList
    End Function

    Public Function ModifyGameStatus(ByVal GameID As Int32, ByVal status As GameStatus) As Boolean
        Try
            Dim theGame As Game = Repository.Games.Find(GameID)
            theGame.GameStatus = status
            Repository.SaveChanges()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Function CreateGame() As Int32
        Dim theGame As New Game
        With theGame
            .GameStatus = GameStatus.CREATED
            .StartTime = DateTime.Now
            .NumPlayers = 0
        End With
        Repository.Games.Add(theGame)
        Repository.SaveChanges()
        Return theGame.GameID
    End Function

    Function AddPlayerToGame(theGameID As Integer, aPlayerID As Integer) As String
        Dim thePlayer As Player = Repository.Players.Find(aPlayerID)
        Dim theGame As Game = Repository.Games.Find(theGameID)
        theGame.NumPlayers += 1
        theGame.Players.Add(thePlayer)
        Repository.SaveChanges()
        Return theGame.GameID & "_" & thePlayer.PlayerID
    End Function

    Function GamePlayerCount(theGameID As Integer) As Integer
        Return Repository.Games.Find(theGameID).Players.ToList.Count
    End Function

    Function PlayersOpenGames(aPlayerID As Integer) As Boolean
        Dim proceed As Boolean = True
        Dim thePlayer As Player = Repository.Players.Find(aPlayerID)
        If thePlayer Is Nothing Then
            Throw New InvalidLoginAttemptException(New CustomErrorDetail With { _
                                                   .ErrorInfo = "INVALID PLAYER ID", _
                                                   .ErrorDetail = "The player id supplied does not exist in the Database."})
        End If
        Dim StartedGames = thePlayer.Games.Where(Function(g) g.GameStatus = GameStatus.STARTED)
        Dim EnrollingGames = thePlayer.Games.Where(Function(g) g.GameStatus = GameStatus.ENROLLING)

        If StartedGames.Count > 0 Or EnrollingGames.Count > 0 Then
            proceed = False
        End If

        Return proceed
    End Function

    Sub StartGame(theGameID As Integer)
        Dim HF As New HandFactory
        Dim theGame As Game = Repository.Games.Find(theGameID)
        HF.GenerateHands(Repository, theGame)
        theGame.GameStatus = GameStatus.STARTED
        ActiveGameTracker.StartGame(theGameID, CInt(theGame.NumPlayers))
        Repository.SaveChanges()
    End Sub

    Function GetGamePlayerToken(aPlayerID As Integer) As String
        Dim thegames = _
            From g In Repository.Players.Find(aPlayerID).Games.AsEnumerable()
            Order By g.GameID Descending _
            Select g

        Dim gameID As String = thegames.ToList()(0).GameID.ToString
        Return String.Format("{0}_{1}", gameID, aPlayerID)
    End Function
#End Region

#Region "   ***DEALER FUNCTIONS***   "
    Public Function GetPlayerHands(ByVal gameID As Int32, ByVal playerID As Int32) As PlayerHand()
        Dim Hands = _
            From h In Repository.Hands.AsEnumerable _
            Where h.PlayerID = playerID _
            And h.Round.GameID = gameID _
            Order By h.RoundID Ascending
            Select h

        Dim Converter As New HandConverter
        Dim PlayerHands As New List(Of PlayerHand)
        For Each h In Hands.ToList
            PlayerHands.Add(Converter.BackConvert(h))
        Next

        Return PlayerHands.ToArray
    End Function
#End Region

#Region "   ***SELECTION HANDLER FUNCTIONS***   "

    Function RecordSelection(thePlayerHand As PlayerHand) As PlayerHand
        Dim hc As New HandConverter
        Dim theHand As Hand = Repository.Hands.Find(thePlayerHand.HandID)
        theHand.Selection = hc.ConvertSelection(thePlayerHand.Selection)
        Repository.SaveChanges()
        Return hc.BackConvert(theHand)
    End Function

    Function GetGameByHandID(HandID As Integer) As Int32
        Return Repository.Hands.Find(HandID).Round.GameID
    End Function

    Function GetSelectionsByRound(theRoundID As Integer) As PlayerSelection()
        Dim Selections = _
            From h In Repository.Rounds.Find(theRoundID).Hands.AsEnumerable _
            Select New PlayerSelection With { _
                .Username = h.Player.Username, _
                .Selection = h.Selection.Split("_"c)}

        Return Selections.ToArray
    End Function

    Function GetGameByRoundID(theRoundID As Integer) As Integer
        Return Repository.Rounds.Find(theRoundID).GameID
    End Function

    Function GetRoundByHandID(HandID As Integer) As Integer
        Return Repository.Hands.Find(HandID).RoundID
    End Function

    Function GetPickCount(HandID As Integer) As Integer
        Return Repository.Hands.Find(HandID).Round.BlackCard.PickCount
    End Function

    Function AlreadySubmittedHand(thePlayerHand As PlayerHand) As Boolean
        Dim theHand As Hand = Repository.Hands.Find(thePlayerHand.HandID)
        If theHand.Selection IsNot Nothing Then
            Return True
        Else
            Return False
        End If
    End Function
#End Region

#Region "   ***VOTE HANDLER FUNCTIONS***   "
    Function CastVote(thePlayerHand As PlayerHand) As PlayerHand
        Dim theHand As Hand = Repository.Hands.Find(thePlayerHand.HandID)
        theHand.Vote = GetPlayerIDByUsername(thePlayerHand.Vote)
        Repository.SaveChanges()
        Return thePlayerHand
    End Function

    Function GetVotesByRound(round As Integer) As List(Of Ballot)
        Dim theVotes = _
            From h In Repository.Rounds.Find(round).Hands.AsEnumerable _
            Select New With { _
                .Username = Repository.Players.Find(h.Vote).Username
            }

        Dim totals = _
            From v In theVotes.AsEnumerable _
            Group v By v.Username Into Count() _
            Select New Ballot With { _
                .Username = Username, _
                .NumVotes = Count}

        Return totals.ToList
    End Function

    Sub SubmitWinner(round As Integer, username As String)
        Repository.RoundWinners.Add(New RoundWinner With {.RoundID = round, .PlayerID = CInt(GetPlayerIDByUsername(username))})
        Repository.SaveChanges()
    End Sub

    Sub CalcWinner(game As Integer)
        Dim theWinners As List(Of Ballot) = GetWinnersForGame(game)

        'If theWinners(0).NumVotes > theWinners(1).NumVotes Then
        Repository.Games.Find(game).Winner = GetPlayerIDByUsername(theWinners(0).Username)
        'End If
        Repository.SaveChanges()
    End Sub

    Function GetWinnersForGame(game As Integer) As List(Of Ballot)
        Dim roundIDs = _
           From round In Repository.Games.Find(game).Rounds.AsEnumerable _
           Select round.RoundID

        Dim winners = _
            From rw In Repository.RoundWinners.AsEnumerable _
            Where roundIDs.Contains(rw.RoundID) _
            Order By rw.PlayerID _
            Select rw

        Dim WinnersCounts = _
            From w In winners _
            Group w.PlayerID By w.PlayerID Into Count() _
            Order By Count Descending _
            Select New Ballot With { _
                .Username = Repository.Players.Find(PlayerID).Username, _
                .NumVotes = Count}

        Return WinnersCounts.ToList
    End Function

    Sub FinishGame(gameID As Integer)
        Dim theGame As Game = Repository.Games.Find(gameID)
        With theGame
            .GameStatus = GameStatus.FINISHED
            .FinishTime = DateTime.Now
        End With
        Repository.SaveChanges()
    End Sub

    Function AlreadyCastVote(thePlayerHand As PlayerHand) As Boolean
        Dim theHand As Hand = Repository.Hands.Find(thePlayerHand.HandID)
        If theHand.Vote IsNot Nothing Then
            Return True
        Else
            Return False
        End If
    End Function
#End Region

#Region "   ***ROUND INFO HANDLER FUNCTIONS***   "
    Function RoundInfoByGameID(GameID As Int32, ByVal rNum As Int32) As RoundStuff
        Dim rs = _
            From r In Repository.Games.Find(GameID).Rounds.Where(Function(d) d.RoundNum = rNum).AsEnumerable _
            Select New RoundStuff With { _
                .RoundID = r.RoundID, _
                .RoundNum = r.RoundNum, _
                .BlackCard = r.BlackCard.BCID, _
                .PickCount = r.BlackCard.PickCount}

        Return rs.ToList().ElementAt(0)
    End Function
#End Region

    Function GetPlayerIDByUsername(Username As String) As Integer?
        Return Repository.Players.Where(Function(p) p.Username = Username).Single.PlayerID
    End Function





  








    



  

 





    


 














End Module
