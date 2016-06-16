set /p apiKey= Enter yout nuget apiKey: 

nuget push .\Build\Gambit.SDK.*.nupkg -apiKey %apiKey%

nuget push .\Build\Gambit.SDK.*.symbols.nupkg -apiKey %apiKey%

PAUSE