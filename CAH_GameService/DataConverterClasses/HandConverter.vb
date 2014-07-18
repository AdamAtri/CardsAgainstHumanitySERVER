Imports CAH_EntitiesLibrary
Public Class HandConverter

    Public Function Convert(ByRef aPlayerHand As PlayerHand) As Hand
        Dim aHand As New Hand
        With aHand
            .Card01 = aPlayerHand.Cards(0)
            .Card02 = aPlayerHand.Cards(1)
            .Card03 = aPlayerHand.Cards(2)
            .Card04 = aPlayerHand.Cards(3)
            .Card05 = aPlayerHand.Cards(4)
            .Card06 = aPlayerHand.Cards(5)
            .Card07 = aPlayerHand.Cards(6)
            .Card08 = aPlayerHand.Cards(7)
            .Card09 = aPlayerHand.Cards(8)
            .Card10 = aPlayerHand.Cards(9)
            .HandID = aPlayerHand.HandID
            .Selection = ConvertSelection(aPlayerHand.Selection)
            .Vote = If(aPlayerHand.Vote IsNot Nothing, CAH_Repository.GetPlayerIDByUsername(aPlayerHand.Vote), Nothing)
        End With
        Return aHand
    End Function

    Public Function BackConvert(ByRef aHand As Hand) As PlayerHand
        Dim aPH As New PlayerHand
        With aPH
            .HandID = aHand.HandID
            .RoundNum = aHand.Round.RoundNum
            .Cards = {aHand.Card01, aHand.Card02, aHand.Card03, aHand.Card04, aHand.Card05, aHand.Card06, aHand.Card07, aHand.Card08, aHand.Card09, aHand.Card10}
            .Selection = If(aHand.Selection IsNot Nothing Or aHand.Selection <> String.Empty, BackConvertSelection(aHand.Selection), {"", "", ""})
            .Vote = ""
        End With
        Return aPH
    End Function

    Public Function ConvertSelection(Selection As String()) As String
        Dim result As String = String.Empty
        For Each s As String In Selection
            result &= s & "_"
        Next
        result = result.Remove(result.Length - 1, 1)
        Return result
    End Function

    Private Function BackConvertSelection(Selection As String) As String()
        Return Selection.Split("_"c)
    End Function

End Class
