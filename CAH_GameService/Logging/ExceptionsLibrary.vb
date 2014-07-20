Imports System.ServiceModel.Web
Imports System.ServiceModel

<Serializable> _
Public Class InvalidRoundException
    Inherits WebFaultException(Of CustomErrorDetail)

    Public Sub New(ByVal customError As CustomErrorDetail)
        MyBase.New(customError, Net.HttpStatusCode.Forbidden)
    End Sub
End Class

<Serializable> _
Public Class HandsNotReadyException
    Inherits WebFaultException(Of CustomErrorDetail)

    Public Sub New(customError As CustomErrorDetail)
        MyBase.New(customError, Net.HttpStatusCode.NoContent)
    End Sub
End Class

<Serializable> _
Public Class WaitingForAllSelectionsException
    Inherits WebFaultException(Of CustomErrorDetail)

    Public Sub New(customError As CustomErrorDetail)
        MyBase.New(customError, Net.HttpStatusCode.NoContent)
    End Sub
End Class

<Serializable> _
Public Class WaitingForAllVotesException
    Inherits WebFaultException(Of CustomErrorDetail)

    Public Sub New(errorDetail As CustomErrorDetail)
        MyBase.New(errorDetail, Net.HttpStatusCode.NoContent)
    End Sub
End Class

<Serializable> _
Public Class InvalidLoginAttemptException
    Inherits WebFaultException(Of CustomErrorDetail)

    Public Sub New(ByVal customError As CustomErrorDetail)
        MyBase.New(customError, Net.HttpStatusCode.NotFound)
    End Sub
End Class

<Serializable> _
Public Class ActivePlayerException
    Inherits WebFaultException(Of CustomErrorDetail)

    Public Sub New(ByVal customError As CustomErrorDetail)
        MyBase.New(customError, Net.HttpStatusCode.NoContent)
    End Sub
End Class

<Serializable> _
Public Class MissedGameException
    Inherits WebFaultException(Of CustomErrorDetail)

    Public Sub New(ByVal customError As CustomErrorDetail)
        MyBase.New(customError, Net.HttpStatusCode.Conflict)
    End Sub
End Class

<Serializable> _
Public Class GenericHandledException
    Inherits WebFaultException(Of CustomErrorDetail)

    Public Sub New(ByVal customError As CustomErrorDetail)
        MyBase.New(customError, Net.HttpStatusCode.InternalServerError)
    End Sub
End Class

<Serializable> _
Public Class SubmitTooQuicklyException
    Inherits WebFaultException(Of CustomErrorDetail)

    Public Sub New(ByVal customError As CustomErrorDetail)
        MyBase.New(customError, Net.HttpStatusCode.Conflict)
    End Sub
End Class

<Serializable> _
Public Class TooManySelectionsException
    Inherits WebFaultException(Of CustomErrorDetail)

    Public Sub New(ByVal customError As CustomErrorDetail)
        MyBase.New(customError, Net.HttpStatusCode.Conflict)
    End Sub
End Class