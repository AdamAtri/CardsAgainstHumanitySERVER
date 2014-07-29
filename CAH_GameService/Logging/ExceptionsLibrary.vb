Imports System.ServiceModel.Web
Imports System.ServiceModel

<Serializable> _
Public MustInherit Class CustomGameException
    Inherits WebFaultException(Of CustomErrorDetail)
    Public Sub New(ByVal customError As CustomErrorDetail, ByVal status As Net.HttpStatusCode)
        MyBase.New(customError, status)
    End Sub
End Class

<Serializable> _
Public Class InvalidRoundException
    Inherits CustomGameException

    Public Sub New(ByVal customError As CustomErrorDetail)
        MyBase.New(customError, Net.HttpStatusCode.Forbidden)
    End Sub
End Class

<Serializable> _
Public Class HandsNotReadyException
    Inherits CustomGameException

    Public Sub New(customError As CustomErrorDetail)
        MyBase.New(customError, Net.HttpStatusCode.PartialContent)
    End Sub
End Class

<Serializable> _
Public Class WaitingForAllSelectionsException
    Inherits CustomGameException

    Public Sub New(customError As CustomErrorDetail)
        MyBase.New(customError, Net.HttpStatusCode.PartialContent)
    End Sub
End Class

<Serializable> _
Public Class WaitingForAllVotesException
    Inherits CustomGameException

    Public Sub New(errorDetail As CustomErrorDetail)
        MyBase.New(errorDetail, Net.HttpStatusCode.PartialContent)
    End Sub
End Class

<Serializable> _
Public Class InvalidLoginAttemptException
    Inherits CustomGameException

    Public Sub New(ByVal customError As CustomErrorDetail)
        MyBase.New(customError, Net.HttpStatusCode.NotFound)
    End Sub
End Class

<Serializable> _
Public Class ActivePlayerException
    Inherits CustomGameException

    Public Sub New(ByVal customError As CustomErrorDetail)
        MyBase.New(customError, Net.HttpStatusCode.NoContent)
    End Sub
End Class

<Serializable> _
Public Class MissedGameException
    Inherits CustomGameException

    Public Sub New(ByVal customError As CustomErrorDetail)
        MyBase.New(customError, Net.HttpStatusCode.Conflict)
    End Sub
End Class

<Serializable> _
Public Class GenericHandledException
    Inherits CustomGameException

    Public Sub New(ByVal customError As CustomErrorDetail)
        MyBase.New(customError, Net.HttpStatusCode.InternalServerError)
    End Sub
End Class

<Serializable> _
Public Class SubmitTooQuicklyException
    Inherits CustomGameException

    Public Sub New(ByVal customError As CustomErrorDetail)
        MyBase.New(customError, Net.HttpStatusCode.Conflict)
    End Sub
End Class

<Serializable> _
Public Class TooManySelectionsException
    Inherits CustomGameException

    Public Sub New(ByVal customError As CustomErrorDetail)
        MyBase.New(customError, Net.HttpStatusCode.PreconditionFailed)
        Me.CreateMessageFault()
    End Sub
End Class

<Serializable> _
Public Class NotEnoughSelectionsException
    Inherits CustomGameException
    Public Sub New(ByVal customError As CustomErrorDetail)
        MyBase.New(customError, Net.HttpStatusCode.PreconditionFailed)
    End Sub
End Class

<Serializable> _
Public Class AlreadySubmittedHandException
    Inherits CustomGameException

    Public Sub New(ByVal customError As CustomErrorDetail)
        MyBase.New(customError, Net.HttpStatusCode.Conflict)
    End Sub
End Class

<Serializable> _
Public Class AlreadyCastVoteException
    Inherits CustomGameException

    Public Sub New(ByVal customError As CustomErrorDetail)
        MyBase.New(customError, Net.HttpStatusCode.Conflict)
    End Sub
End Class

<Serializable> _
Public Class PlayerAlreadyExistsException
    Inherits CustomGameException

    Public Sub New(ByVal customError As CustomErrorDetail)
        MyBase.New(customError, Net.HttpStatusCode.Conflict)
    End Sub
End Class

<Serializable> _
Public Class GameNotOverException
    Inherits CustomGameException

    Public Sub New(ByVal customError As CustomErrorDetail)
        MyBase.New(customError, Net.HttpStatusCode.Conflict)
    End Sub
End Class