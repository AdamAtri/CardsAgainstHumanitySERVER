Imports CAH_EntitiesLibrary
Imports System.IO
Public Class HandFactory

    Private Const MAX_CARDS As Int32 = 10
    Private Const WHITE_DECK As Int32 = 457

    ''' <summary>
    ''' This creates the HANDS for all PLAYERS for all ROUNDS of the GAME
    ''' </summary>
    ''' <param name="ents"></param>
    ''' <param name="theGame"></param>
    ''' <remarks></remarks>
    Public Sub GenerateHands(ByRef ents As CAH_DataLibrary.CAH_EntitiesLibrary.CAH_Entities, ByRef theGame As Game)
        For theRound As Int32 = 1 To MAX_ROUNDS
            Dim newRound As New Round
            With newRound
                .GameID = theGame.GameID
                .RoundNum = theRound
                .BlackCardID = SelectBlackCard(ents, theGame)
            End With

            ents.Rounds.Add(newRound)
            ents.SaveChanges()

            Dim theWhiteCards(WHITE_DECK) As Int32
            For i As Int32 = 1 To WHITE_DECK
                theWhiteCards(i - 1) = i
            Next
            Shuffle(theWhiteCards)

            Dim cardcount As Int32 = 0
            For Each p As Player In theGame.Players
                Dim newHand As New Hand
                With newHand
                    .PlayerID = p.PlayerID
                    .RoundID = newRound.RoundID
                    .Card01 = theWhiteCards(cardcount)
                    cardcount += 1
                    .Card02 = theWhiteCards(cardcount)
                    cardcount += 1
                    .Card03 = theWhiteCards(cardcount)
                    cardcount += 1
                    .Card04 = theWhiteCards(cardcount)
                    cardcount += 1
                    .Card05 = theWhiteCards(cardcount)
                    cardcount += 1
                    .Card06 = theWhiteCards(cardcount)
                    cardcount += 1
                    .Card07 = theWhiteCards(cardcount)
                    cardcount += 1
                    .Card08 = theWhiteCards(cardcount)
                    cardcount += 1
                    .Card09 = theWhiteCards(cardcount)
                    cardcount += 1
                    .Card10 = theWhiteCards(cardcount)
                    cardcount += 1
                End With
                newRound.Hands.Add(newHand)
            Next
            ents.SaveChanges()
        Next
        ActiveGameTracker.HandsReady(theGame.GameID)
    End Sub

    ''' <summary>
    ''' Selects a black card for the round
    ''' </summary>
    ''' <param name="ents"></param>
    ''' <param name="theGame"></param>
    ''' <returns>Black Card ID (int)</returns>
    ''' <remarks></remarks>
    Private Function SelectBlackCard(ByRef ents As CAH_DataLibrary.CAH_EntitiesLibrary.CAH_Entities, ByRef theGame As Game) As Integer

        Dim AlreadyUsedBC = _
            From r In theGame.Rounds _
            Select r.BlackCardID

        Dim AlreadyUsed = AlreadyUsedBC.ToList
        Dim BlackCardCount As Int32 = ents.BlackCards.ToList.Count

        Dim bc As Int32 = rand.Next(1, BlackCardCount + 1)

        While AlreadyUsed.Contains(bc)
            bc = rand.Next(1, BlackCardCount + 1)
        End While

        Return bc
    End Function
    ''' <summary>
    ''' Shuffles an array of white card ids
    ''' </summary>
    ''' <param name="shuffleArray"></param>
    ''' <remarks></remarks>
    Private Shared Sub Shuffle(ByRef shuffleArray() As Int32)
        Dim counter As Integer
        Dim newPosition As Integer
        Dim temp As Int32

        For counter = 0 To shuffleArray.Length - 1
            newPosition = rand.Next(0, shuffleArray.Length - 1)

            temp = shuffleArray(counter)
            shuffleArray(counter) = shuffleArray(newPosition)
            shuffleArray(newPosition) = temp
        Next counter
    End Sub

  


End Class
