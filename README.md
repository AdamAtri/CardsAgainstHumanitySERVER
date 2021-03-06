CardsAgainstHumanitySERVER
==========================
This is the SERVER application for a software implementation of the game CARDS AGAINST HUMANITY.<br/>
The server manages game play, allowing multiple concurrent games, while tracking the action from every round of play.</br>


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
  <li>Create a new login with <strong>username:</strong> "CAH_Model" and <strong>pw:</strong> "password" (Security >> Logins)</li>
  <li>Add CAH_Model to CAH_Database >> Security >> Users</li>
  <li>Create a new schema within CAH_Database named "cah" with owner "CAH_Model". <br/>
  	  (Databases >> CAH_Database >> Security >> Schemas)</li>
  <li>Restart MSSQL Server</li>
  <li>In the SERVER application, open the CAH_DataLibrary directory and execute the sql script: CAH_DataModel.edmx.sql <br/>
      (if you run into an error, copy and paste the script into SQLServer and remove the stuff about <br/>
      dropping FK Constraints and existing tables at the top.)</li>
  <li>Once the database is setup, open CAH_ConsoleServer >> App.config <br/>
      Find the "connectionStrings" element, then in the "connectionString" attribute replace <br/>
      the "Data Source" value with your named instance of SQL Server.</li>
</ol>
</p>
  
  

  
