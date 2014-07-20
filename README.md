CardsAgainstHumanitySERVER
==========================
This is the SERVER application for a software implementation of the game CARDS AGAINST HUMANITY.

<strong>Technology used:</strong><br/>
**All code proudly written in VB.NET (VS2013 IDE)
  <ul>
  	<li>WCF Restful Services</li>
  	<li>Entity Framework</li>
  	<li>LINQ</li>
  </ul>
  
<p><strong>PreRequisits to running:</strong> <br/>
  MSSQL Server 2008 or newer (see Database Configuration below)
</p>
  

<p><strong>Database Configuration:</strong>
<ol>
  <li>Create a new database named "CAH_Database"</li>
  <li>Create a new login with username: CAH_Model pw: password (Security >> Logins)</li>
  <li>Add CAH_Model to CAH_Database >> Security >> Users</li>
  <li>Create a new schema within CAH_Database named "cah" with owner "CAH_Model". <br/>
  	  (Databases >> CAH_Database >> Security >> Schemas)</li>
  <li>Restart MSSQL Server</li>
</ol>
</p>
  
  

  
