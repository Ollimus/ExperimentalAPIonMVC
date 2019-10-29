Setting up the project
---------------------

1. Start up the project using Visual Studio (upwards from 2016).
2. If Visual studio at any point warns about missing dependencies, download them.
3. Open up Package Manager Console within Visual Studio. 
	3.1. If Package Manager Console is not on by default (depends on the version used), go to >Tools, >NuGet Package Manager and select Package Manager Console.
4. Type "Update-Database" into Package Manager Console without quotations. This will create a local SQL database on the local machine using the code-first method's migrations.
	4.1. There is a known bug that might cause "File not found" or similar error. In this case, remove App_Data folder inside the project as well as the project root folder. After that re-create the app_data folder and run the command again.
5. You can now run the program.