Imports System.ServiceModel
<ServiceBehavior(InstanceContextMode:=ServiceModel.InstanceContextMode.Single)> _
Public Class CAH_Service
    Implements ICAH_Service


    Public Sub New()
        ActiveGameTracker.Intialize()
        CAH_Repository.CheckBlackCards()
        ExceptionLogger.Initalize()
    End Sub



    Public Function Login(user As String, pass As String) As NewPlayer Implements ICAH_Service.Login
        Try
            Dim PM As New PlayerManager
            Return PM.Login(user, pass)
        Catch ex As Exception
            ExceptionLogger.WriteException(ex, "PlayerManager:Login" & vbCrLf & "USERNAME: " & user)
            Throw
        End Try
    End Function

    Public Function NewPlayer(aNewPlayer As NewPlayer) As NewPlayer Implements ICAH_Service.NewPlayer
        Try
            Dim PM As New PlayerManager
            Return PM.CreateNewPlayer(aNewPlayer)
        Catch ex As Exception
            ExceptionLogger.WriteException(ex, "PlayerManager:CreateNew")
            Throw
        End Try
    End Function

    Public Function GetHands(GamePlayerToken As String) As PlayerHand() Implements ICAH_Service.GetHands
        Dim GamePlayer() As String = GamePlayerToken.Split("_"c)
        Try
            Dim D As New Dealer
            Return D.DealHand(CInt(GamePlayer(0)), CInt(GamePlayer(1)))
        Catch ex As Exception
            ExceptionLogger.WriteException(ex, "Dealer:DealHands", CInt(GamePlayer(0)), CInt(GamePlayer(1)))
            Throw
        End Try
    End Function

    Public Function JoinGame(playerID As String) As String Implements ICAH_Service.JoinGame
        Try
            Dim GQ As New GameQueue
            Dim GamePlayerID As String = GQ.QueuePlayer(CInt(playerID))
            Return GamePlayerID
        Catch ex As Exception
            ExceptionLogger.WriteException(ex, "Game_Queue.JoinGame", playerID:=CInt(playerID))
            Throw
        End Try

    End Function

    Public Function GetRoundStuff(GamePlayerToken As String) As RoundStuff Implements ICAH_Service.GetRoundStuff
        Dim GamePlayer() As String = GamePlayerToken.Split("_"c)
        Try
            Dim RH As New RoundInfoHandler
            Return RH.GetRoundInfo(CInt(GamePlayer(0)))
        Catch ex As Exception
            ExceptionLogger.WriteException(ex, "Round_Info_Handler.GetRoundInfo", CInt(GamePlayer(0)), CInt(GamePlayer(1)))
            Throw
        End Try
    End Function

    Public Function MakeSelection(aPlayerHand As PlayerHand) As PlayerHand Implements ICAH_Service.MakeSelection
        Try
            Dim SH As New SelectionHandler
            Return SH.SubmitSelection(aPlayerHand)
        Catch ex As Exception
            ExceptionLogger.WriteException(ex, "Selection_Handler.Submit(HAND_ID: " & aPlayerHand.HandID & ")")
            Throw
        End Try
    End Function

    Public Function GetAllSelections(RoundID As String) As PlayerSelection() Implements ICAH_Service.GetAllSelections
        Try
            Dim SH As New SelectionHandler
            Return SH.GetAllSelections(CInt(RoundID))
        Catch ex As Exception
            ExceptionLogger.WriteException(ex, "Selection_Handler:GetAllSelections(ROUND_ID: " & RoundID & ")")
            Throw
        End Try
    End Function

    Public Function CastVote(thePlayerHand As PlayerHand) As PlayerHand Implements ICAH_Service.CastVote
        Try
            Dim VH As New VoteHandler
            Return VH.CastVote(thePlayerHand)
        Catch ex As Exception
            ExceptionLogger.WriteException(ex, "Vote_Handler:CastVote(HAND_ID: " & thePlayerHand.HandID & ")")
            Throw
        End Try
    End Function

    Public Function GetRoundWinner(roundID As String) As Ballot() Implements ICAH_Service.GetRoundWinner
        Try
            Dim VH As New VoteHandler
            Return VH.GetRoundWinner(CInt(roundID)).ToArray
        Catch ex As Exception
            ExceptionLogger.WriteException(ex, "Vote_Handler:GetRoundWinner(ROUND_ID: " & roundID & ")")
            Throw
        End Try
    End Function

    Public Function GetGameWinner(gameID As String) As Ballot() Implements ICAH_Service.GetGameWinner
        Try
            Dim VH As New VoteHandler
            Return VH.GetGameWinner(CInt(gameID)).ToArray
        Catch ex As Exception
            ExceptionLogger.WriteException(ex, "Vote_Handler:GetGameWinner", CInt(gameID))
            Throw
        End Try
    End Function

    Public Function GetPlayer(playerID As String) As NewPlayer Implements ICAH_Service.GetPlayer
        VerifyInitialized()
        Dim PM As New PlayerManager
        Return PM.GetPlayer(CInt(playerID))
    End Function

    Public Sub VerifyInitialized()
        If Not Initialized Then
            ActiveGameTracker.Intialize()
            Initialized = True
        End If
    End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If
            ActiveGameTracker.SaveActiveTable()
            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
