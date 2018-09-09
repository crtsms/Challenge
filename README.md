# Challenge app

This app is really simple, have 3 tables, Employee, Skill and EmployeeSkill.
Althought it's simple, have some interesting stuff, like:

* [Remote validation of properties of model](https://stackoverflow.com/questions/24863634/mvc-5-remote-validation)
* [Async action methods](https://docs.microsoft.com/en-us/aspnet/mvc/overview/performance/using-asynchronous-methods-in-aspnet-mvc-4)
* [Modal to edit ternal relationship](https://getbootstrap.com/docs/4.0/components/modal/)
* Load multiple partial view, in same page, with asynchronous operations
* Use Model, ModelView, View and Controller
* [Use POCO Entities](https://docs.microsoft.com/en-us/previous-versions/dotnet/netframework-4.0/dd456853(v=vs.100))

## Live demo

* [See the live demo](https://crtsms.azurewebsites.net/) - Hosted on Microsoft Azure

## Getting Started

Restore NuGet packages with the following command

```
nuget restore Challenge.sln
```

Restore database running SQLServer on Linux Container

```
sudo docker exec -it mssql /opt/mssql-tools/bin/sqlcmd \
   -S localhost -U SA -P 'Pa$$w0rd' \
   -Q 'RESTORE DATABASE EmployeeSkills FROM DISK = "/var/opt/mssql/backup/EmployeeSkills.bak" WITH FILE = 1, 
   MOVE "EmployeeSkills" TO "/var/opt/mssql/data/EmployeeSkills.MDF", 
   MOVE "EmployeeSkills_LOG" TO "/var/opt/mssql/data/EmployeeSkills_LOG.LDF"'
```

Restore database running SQLServer Express 2014 on Windows 7

```
sqlcmd -S crisleiOldMachine\SQLExpress
    RESTORE DATABASE EmployeeSkills
    FROM DISK = N'C:\EmployeeSkills.bak'
    WITH FILE = 1,  
    MOVE N'EmployeeSkills' TO N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\EmployeeSkills.MDF',  
    MOVE N'EmployeeSkills_LOG' TO N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\EmployeeSkills_LOG.LDF',  
    NOUNLOAD,  
    REPLACE,  
    STATS = 10
    GO
```
