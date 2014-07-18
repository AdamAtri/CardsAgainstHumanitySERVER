CardsAgainstHumanitySERVER
==========================
This is the SERVER application for a software implementation of the game CARDS AGAINST HUMANITY.

Technology used:
-All code proudly written in VB.NET (VS2013 IDE)
  WCF Restful Services
  Entity Framework
  LINQ
  
PreRequisits to running:
  MSSQL Server 2008 or newer (see Database Configuration below)
  

Database Configuration:
  Create a new database named "CAH_Database"
  Create a new login with username: CAH_Model pw: password (Security >> Logins)
  Add CAH_Model to CAH_Database >> Security >> Users
  Create a new schema within CAH_Database named "cah" with owner "CAH_Model". (Databases >> CAH_Database >> Security >> Schemas)
  Restart MSSQL Server
  

  
