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

Partial Public Class Round
    Public Property RoundID As Integer
    Public Property RoundNum As Integer
    Public Property GameID As Integer
    Public Property BlackCardID As Integer

    Public Overridable Property Game As Game
    Public Overridable Property Hands As ICollection(Of Hand) = New HashSet(Of Hand)
    Public Overridable Property BlackCard As BlackCard

End Class