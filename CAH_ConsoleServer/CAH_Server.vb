Imports System.Runtime.Serialization
Imports System.ServiceModel
Imports System.ServiceModel.Web
Imports CAH_GameService
Imports System.ServiceModel.Description

Module CAH_Server

    Sub Main()
        Dim host As New ServiceHost(GetType(CAH_Service))
        Try
            host.Open()
            PrintServiceInfo(host)
            Console.ReadLine()
            host.Close()
        Catch ex As Exception
            Console.WriteLine(ex)
            Console.ReadLine()
            host.Abort()
        End Try

    End Sub
    Private Sub PrintServiceInfo(host As ServiceHost)
        Console.WriteLine("{0} is up and running with these endpoints:", host.Description.ServiceType)
        For Each se As ServiceEndpoint In host.Description.Endpoints
            Console.WriteLine(se.Address)
        Next
    End Sub
End Module
