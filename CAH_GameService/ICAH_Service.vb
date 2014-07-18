Imports System.ServiceModel
Imports System.ServiceModel.Web

<ServiceContract()>
Public Interface ICAH_Service
    Inherits IDisposable

    <OperationContract()> _
    <WebGet(uriTemplate:="/Players/{playerID}")> _
    Function GetPlayer(ByVal playerID As String) As NewPlayer

    <OperationContract()> _
    <WebInvoke(UriTemplate:="/Players/New", RequestFormat:=WebMessageFormat.Json)> _
    Function NewPlayer(ByVal aNewPlayer As NewPlayer) As NewPlayer

    <OperationContract()> _
    <WebGet(UriTemplate:="/JoinGame/{playerID}")> _
    Function JoinGame(ByVal playerID As String) As String

    <OperationContract()> _
    <WebInvoke(UriTemplate:="/Players/Login", requestFormat:=WebMessageFormat.Json, bodystyle:=WebMessageBodyStyle.WrappedRequest)> _
    Function Login(ByVal user As String, ByVal pass As String) As NewPlayer

    <OperationContract()> _
    <WebGet(UriTemplate:="/Hands/{GamePlayerToken}")> _
    Function GetHands(ByVal GamePlayerToken As String) As PlayerHand()

    <OperationContract()> _
    <WebGet(UriTemplate:="/Round/Stuff/{GamePlayerToken}")> _
    Function GetRoundStuff(ByVal GamePlayerToken As String) As RoundStuff

    <OperationContract()> _
    <WebInvoke(UriTemplate:="/Hands", requestFormat:=WebMessageFormat.Json)> _
    Function MakeSelection(ByVal aPlayerHand As PlayerHand) As PlayerHand

    <OperationContract()> _
    <WebGet(UriTemplate:="/Round/Selections/{RoundID}")> _
    Function GetAllSelections(ByVal RoundID As String) As PlayerSelection()

    <OperationContract()> _
    <WebInvoke(UriTemplate:="/Hands/Vote", requestFormat:=WebMessageFormat.Json)> _
    Function CastVote(ByVal aPlayerHand As PlayerHand) As PlayerHand

    <OperationContract()> _
    <WebGet(UriTemplate:="/Round/Winner/{RoundID}")> _
    Function GetRoundWinner(ByVal RoundID As String) As Ballot()

    <OperationContract()> _
    <WebGet(UriTemplate:="/Game/Winner/{GameID}")> _
    Function GetGameWinner(ByVal GameID As String) As Ballot()

End Interface
