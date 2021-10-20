# Road Status
TFL Road Status Coding Challenge

# Required version to build and execute the Road Status Repository
  .NET 3.1 version(.Net Core 3.1)
  Visual Studio 2019 IDE

# Build and Run the Repository
Build any .NET Core project using the .NET Core CLI, which is installed with the [.NET Core SDK](https://dotnet.microsoft.com/download). Then run these commands from the CLI in the directory of this project (src\TFLAssessment.Console) :<br />

``dotnet build``<br />
``dotnet run``<br />

These will install any needed dependencies, build the project, and run the project respectively.  

**Other Options** - 
1) **Buid :** Open the Visual Studio(2019) IDE **Build**  Menu --> **Build solution**
2) **Run :** Open the command prompt/Powershell from the buid package directlory or publish directory and execute this command(Attached the screenshot below). ``TFLAssessment.Console.exe A1``

**Test Project Execution:** Open the Visual Studio(2019) IDE **Test**  Menu --> Run All Tests<br />
    Once it is executed, Test explorer will show the test results(Executed screenshot added below) 

**Added Published files in this Ropository to help for executing without build and run.

# Changing App ID and API key
a.	APPID and APIKey are stored under appsettings.json in the application
b.	<img src=".\resources\img\Appsettings.JPG"/>

c.	Change below configuration in the settings file appsettings.json 
i.	  "AppId": "PLEASE ENTER YOUR APP_ID"<br />
ii.	  "AppKey": "PLEASE ENTER YOUR APP_KEY"

# Additional Details

1) Task implemented using clean architecture.
2) Implemented CQRS and the Mediator pattern.
3) Logging implemented using serilog.
4) Unit test written using xUnit.


# ScreenShots

# Result
<img src=".\resources\img\OutputConsole.JPG"/>

# Test Case Status
<img src=".\resources\img\TestResults.JPG"/>

