'------------------------------------------------------------------------------
' <auto-generated>
'    This code was generated from a template.
'
'    Manual changes to this file may cause unexpected behavior in your application.
'    Manual changes to this file will be overwritten if the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Imports System
Imports System.Collections.Generic

Partial Public Class Player
    Public Property PlayerID As Integer
    Public Property Username As String
    Public Property Password As String
    Public Property Email As String
    Public Property FName As String
    Public Property LName As String
    Public Property BirthDate As Nullable(Of Date)
    Public Property Mobile As String
    Public Property Avatar As Byte()

    Public Overridable Property Hands As ICollection(Of Hand) = New HashSet(Of Hand)
    Public Overridable Property Games As ICollection(Of Game) = New HashSet(Of Game)

End Class
