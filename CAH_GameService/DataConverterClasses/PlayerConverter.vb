Imports CAH_EntitiesLibrary
Public Class PlayerConverter
    Public Function Convert(ByRef aNewPlayer As NewPlayer) As Player
        Dim p As New Player
        With p
            .Username = aNewPlayer.Username
            .Password = aNewPlayer.Password
            .Email = aNewPlayer.Email
            .FName = If(aNewPlayer.FName <> String.Empty, aNewPlayer.FName, "NEEDS NAME")
            .LName = If(aNewPlayer.LName <> String.Empty, aNewPlayer.LName, "NEEDS NAME")
            .Mobile = If(aNewPlayer.Mobile <> String.Empty, aNewPlayer.Mobile, "NEEDS NUMBER")
        End With
        Return p
    End Function

    Public Function ConvertBack(ByRef aPlayer As Player) As NewPlayer
        Dim np As New NewPlayer
        With np
            .PlayerID = aPlayer.PlayerID
            .Username = aPlayer.Username
            .Password = "**********"
            .Email = aPlayer.Email
            .FName = If(aPlayer.FName <> "NEEDS NAME", aPlayer.FName, String.Empty)
            .LName = If(aPlayer.LName <> "NEEDS NAME", aPlayer.LName, String.Empty)
            .Mobile = If(aPlayer.Mobile <> "NEEDS NUMBER", aPlayer.Mobile, String.Empty)
        End With
        Return np
    End Function

End Class
