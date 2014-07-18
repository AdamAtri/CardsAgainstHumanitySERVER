Imports CAH_EntitiesLibrary
Imports CAH_DataLibrary.CAH_EntitiesLibrary
Public Class PlayerManager

    Public Function Login(uName As String, encPW As String) As NewPlayer
        Try
            Return CAH_Repository.Login(uName, encPW)
        Catch ila As InvalidLoginAttemptException
            Throw
        Catch ex As Exception
            Throw New GenericHandledException(New CustomErrorDetail With { _
                                              .ErrorInfo = ex.Source, _
                                              .ErrorDetail = ex.Message})
        End Try

    End Function

    Function CreateNewPlayer(ByVal aNewPlayer As NewPlayer) As NewPlayer
        Try
            Return CAH_Repository.CreatePlayer(aNewPlayer)
        Catch ex As Exception
            Throw New GenericHandledException(New CustomErrorDetail With { _
                                              .ErrorInfo = ex.Source, _
                                              .ErrorDetail = ex.Message})
        End Try
    End Function

    Function GetPlayer(playerID As Int32) As NewPlayer
        Try
            Return CAH_Repository.GetProxyPlayerByID(playerID)
        Catch ex As Exception
            Throw New GenericHandledException(New CustomErrorDetail With { _
                                              .ErrorInfo = ex.Source, _
                                              .ErrorDetail = ex.Message})
        End Try
    End Function

End Class

