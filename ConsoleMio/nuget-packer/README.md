# NugetPackager

#### Scripts for generating a NuGet packages for this solution

### Procedure

1 Create a `.nuspec` file from the Project
  * If you have nuget.exe in your `PATH` go to the root of the project and run `nuget spec`
else run the command relative to this directory

  * Edit the .nuspec file as XML with the required information 

2 run `pack.bat`

3 run `publish.bat`

  * If you don't want to enter the API key each time you run `publish` open a terminal and go to **this** directory and run `nuget setApiKey Your-API-Key`
