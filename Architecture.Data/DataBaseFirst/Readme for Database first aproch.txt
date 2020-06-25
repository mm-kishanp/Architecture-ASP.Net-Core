To Generate model from the database
run below code
 Scaffold-DbContext "Data Source=Server01,1983;Database=TBSAdmin;User ID=devteam;Password=devteam;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir DatabaseFirst/Models


 PM>  Scaffold-DbContext "Data Source=Server01,1983;Database=TBSAdmin;User ID=devteam;Password=devteam;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir DatabaseFirst/Models -Tables LogEntry

If you get errors to add packages then add as per below
Microsoft.AspNetCore.Mvc.Core
Microsoft.EntityFrameworkCore.Design
Microsoft.EntityFrameworkCore.SqlServer