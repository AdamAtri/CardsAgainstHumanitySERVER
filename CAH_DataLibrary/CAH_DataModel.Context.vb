﻿'------------------------------------------------------------------------------
' <auto-generated>
'    This code was generated from a template.
'
'    Manual changes to this file may cause unexpected behavior in your application.
'    Manual changes to this file will be overwritten if the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Imports System
Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure
Imports CAH_EntitiesLibrary

Namespace CAH_EntitiesLibrary

    Partial Public Class CAH_Entities
        Inherits DbContext
    
        Public Sub New()
            MyBase.New("name=CAH_Entities")
        End Sub
    
        Protected Overrides Sub OnModelCreating(modelBuilder As DbModelBuilder)
            Throw New UnintentionalCodeFirstException()
        End Sub
    
        Public Property BlackCards() As DbSet(Of BlackCard)
        Public Property Games() As DbSet(Of Game)
        Public Property Hands() As DbSet(Of Hand)
        Public Property Players() As DbSet(Of Player)
        Public Property Rounds() As DbSet(Of Round)
        Public Property RoundWinners() As DbSet(Of RoundWinner)
    
    End Class

End Namespace
