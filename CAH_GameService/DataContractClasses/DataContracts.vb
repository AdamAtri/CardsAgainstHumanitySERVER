Imports System.Runtime.Serialization

<Serializable()> _
<DataContract(Name:="Player")> _
Public Class NewPlayer
    <DataMember(isRequired:=False)> _
    Public PlayerID As Int32
    <DataMember(isrequired:=True)> _
    Public Username As String
    <DataMember(isRequired:=True)> _
    Public Password As String
    <DataMember(isRequired:=True)> _
    Public Email As String
    <DataMember(isRequired:=False)> _
    Public FName As String
    <DataMember(isRequired:=False)> _
    Public LName As String
    <DataMember(isRequired:=False)>
    Public Mobile As String
End Class

<Serializable()> _
<DataContract(Name:="Hand")> _
Public Class PlayerHand
    <DataMember(isRequired:=True)> _
    Public HandID As Int32
    <DataMember(isRequired:=True)> _
    Public RoundNum As Int32
    <DataMember(isRequired:=True)> _
    Public Cards(10) As Int32
    <DataMember(isRequired:=False)> _
    Public Selection(3) As String
    <DataMember(isRequired:=False)> _
    Public Vote As String
End Class

<Serializable()> _
<DataContract(Name:="RoundStuff")> _
Public Class RoundStuff
    <DataMember(isRequired:=True)> _
    Public RoundID As Int32
    <DataMember(isRequired:=True)> _
    Public RoundNum As Int32
    <DataMember(isRequired:=True)> _
    Public BlackCard As Int32
    <DataMember(isRequired:=True)> _
    Public PickCount As Int32
End Class

<Serializable()> _
<DataContract(Name:="PlayerSelection")> _
Public Class PlayerSelection
    <DataMember(isRequired:=True)> _
    Public Username As String
    <DataMember(isRequired:=True)> _
    Public Selection(3) As String
End Class

<Serializable()> _
<DataContract(Name:="Ballot")> _
Public Class Ballot
    <DataMember(isRequired:=True)> _
    Public Username As String
    <DataMember(isRequired:=True)> _
    Public NumVotes As Int32
End Class

<Serializable()> _
<DataContract()> _
Public Class CustomErrorDetail
    <DataMember(isRequired:=True)> _
    Public ErrorInfo As String
    <DataMember(isRequired:=True)> _
    Public ErrorDetail As String
End Class


<DataContract()> _
Public Enum GameStatus
    <EnumMember()> _
    CREATED = 0
    <EnumMember()> _
    ENROLLING = 10
    <EnumMember()> _
    STARTED = 20
    <EnumMember()> _
    ROUND1 = 21
    <EnumMember()> _
    ROUND2 = 22
    <EnumMember()> _
    ROUND3 = 23
    <EnumMember()> _
    ROUND4 = 24
    <EnumMember()> _
    ROUND5 = 25
    <EnumMember()> _
    FINISHED = 30
End Enum