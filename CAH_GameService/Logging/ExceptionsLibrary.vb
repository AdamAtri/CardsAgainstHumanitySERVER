Imports System.ServiceModel.Web

Public Class InvalidRoundException
    Inherits WebFaultException(Of CustomErrorDetail)

    Public Sub New(ByVal customError As CustomErrorDetail)
        MyBase.New(customError, Net.HttpStatusCode.Forbidden)
    End Sub
End Class


Public Class HandsNotReadyException
    Inherits WebFaultException(Of CustomErrorDetail)

    Public Sub New(customError As CustomErrorDetail)
        MyBase.New(customError, Net.HttpStatusCode.NoContent)
    End Sub
End Class

Public Class WaitingForAllSelectionsException
    Inherits WebFaultException(Of CustomErrorDetail)

    Public Sub New(customError As CustomErrorDetail)
        MyBase.New(customError, Net.HttpStatusCode.NoContent)
    End Sub
End Class

Public Class WaitingForAllVotesException
    Inherits WebFaultException(Of CustomErrorDetail)

    Public Sub New(errorDetail As CustomErrorDetail)
        MyBase.New(errorDetail, Net.HttpStatusCode.NoContent)
    End Sub
End Class

Public Class InvalidLoginAttemptException
    Inherits WebFaultException(Of CustomErrorDetail)

    Public Sub New(ByVal customError As CustomErrorDetail)
        MyBase.New(customError, Net.HttpStatusCode.NotFound)
    End Sub
End Class
Public Class ActivePlayerException
    Inherits WebFaultException(Of CustomErrorDetail)

    Public Sub New(ByVal customError As CustomErrorDetail)
        MyBase.New(customError, Net.HttpStatusCode.NoContent)
    End Sub
End Class

Public Class MissedGameException
    Inherits WebFaultException(Of CustomErrorDetail)

    Public Sub New(ByVal customError As CustomErrorDetail)
        MyBase.New(customError, Net.HttpStatusCode.Conflict)
    End Sub
End Class

Public Class GenericHandledException
    Inherits WebFaultException(Of CustomErrorDetail)

    Public Sub New(ByVal customError As CustomErrorDetail)
        MyBase.New(customError, Net.HttpStatusCode.InternalServerError)
    End Sub
End Class

Public Class SubmitTooQuicklyException
    Inherits WebFaultException(Of CustomErrorDetail)

    Public Sub New(ByVal customError As CustomErrorDetail)
        MyBase.New(customError, Net.HttpStatusCode.Conflict)
    End Sub
End Class