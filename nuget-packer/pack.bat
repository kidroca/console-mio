@echo Off
set config=%1
if "%config%" == "" (
   set config=Release
)
 
set nuget=
if "%nuget%" == "" (
	set nuget=nuget
)

mkdir Build
mkdir Build\lib
mkdir Build\lib\net40

%nuget% pack ..\ConsoleMio.csproj -Symbols -build -IncludeReferencedProjects -NoPackageAnalysis -verbosity detailed -o Build -p Configuration="%config%"

PAUSE