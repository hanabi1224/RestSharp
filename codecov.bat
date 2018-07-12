choco install opencover.portable
choco install codecov

set _config=%1
if not defined _config (
  set _config=Debug
)

set IsInCodeCoverageTask=1
OpenCover.Console.exe -register:user -target:"C:\Program Files\dotnet\dotnet.exe" -targetargs:"test -c %_config% --verbosity q"
codecov -f "results.xml" 
