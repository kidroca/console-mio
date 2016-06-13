@echo Off
set config=%1
if "%config%" == "" (
   set config=Release
)
 
set nuget=
if "%nuget%" == "" (
	set nuget=nuget
)

%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild ..\nConsoleMio.csproj /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=diag /nr:false

mkdir Build
mkdir Build\lib
mkdir Build\lib\net40

%nuget% pack ..\ConsoleMio.csproj -Symbols -IncludeReferencedProjects -NoPackageAnalysis -verbosity detailed -o Build -p Configuration="%config%"

PAUSE