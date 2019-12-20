Setting up the project
---------------------
New way:
1. Download project.
2. Install missing packages. (Either by build, manually etc.)
3. Open up Package Manager console within Visual Studio.
	3.1. If Package Manager Console is not on by default (depends on the version used), go to >Tools, >NuGet Package Manager and select Package Manager Console.
4. At this point, you should get a message saying about missing packages, install them.
5. Type "Update-Database" into Package Manager Console without quotations. This will create a local SQL database on the local machine using the code-first method's migrations.
6. Project might cause an error during the first time running, it is recommend it is restarted after downloading missing packages.

**NOTE:**
If UiTest crashes during their tests, it might leave ChromeDriver.exe running in the background. You need to use task manager to close this down if rerunning the test causes an error.


Every test project can be run separately. Integration tests copy and use a separate database for their testing.

The Ui Tests in the project only test using Chrome due to time constraint. It requires an actual website to be ran while in testing so it uses your Iis Express to host the project so it is able to run tests succesfully. This means that the Ui tests only work on windows based computers.



---
Old installation way:
1. Start up the project using Visual Studio (upwards from 2016).
2. If Visual studio at any point warns about missing dependencies, download them.
3. Open up Package Manager Console within Visual Studio. 
	3.1. If Package Manager Console is not on by default (depends on the version used), go to >Tools, >NuGet Package Manager and select Package Manager Console.
4. Type "Update-Database" into Package Manager Console without quotations. This will create a local SQL database on the local machine using the code-first method's migrations.
	4.1. There is a known bug that might cause "File not found" or similar error. In this case, remove App_Data folder inside the project as well as the project root folder. After that re-create the app_data folder and run the command again.
5. You can now run the program.
