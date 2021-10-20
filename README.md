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

# Configuring the App ID and API key

a.	APPID and APIKey are stored under appsettings.json in the application (Refer screenshot below)

​	<img src=".\resources\img\Appsettings.JPG"/>

b.	Change below keys in the appsettings.json file <br />
* "AppId": "PLEASE ENTER YOUR APP_ID"<br />
* "AppKey": "PLEASE ENTER YOUR APP_KEY"

# Additional Details

1. Task implemented using clean architecture.

2. Implemented CQRS and the Mediator pattern.

3. Logging implemented using serilog. Applications Logs can be found under the logs folder

 <img src=".\resources\img\logs.JPG"/>

4. Unit test written using xUnit.
5. Installed Fine code coverage extension to check the code coverage.

<img src=".\resources\img\OutputConsole.JPG"/>



# Changing App ID and API key

# Test Project Execution:

Open the Visual Studio(2019) IDE **Test**  Menu --> Run All Tests<br />
Once it is executed, Test explorer will show the test results(Executed screenshot added below) 

## Test Case Status

<img src=".\resources\img\TestResults.JPG"/>

## Code Coverage (Fine Code Coverage)
<img src=".\resources\img\CodeCoverage.JPG"/>



## Areas of  Improvement

* More negative test case
* Add integration test using spec flow (BDD)
* Add transforms for application config files for each environment

