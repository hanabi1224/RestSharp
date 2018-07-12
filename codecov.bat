choco install opencover.portable
choco install codecov
set IsInCodeCoverageTask=1
OpenCover.Console.exe -register:user -target:"C:\Program Files\dotnet\dotnet.exe" -targetargs:"test -c Release --verbosity q"
codecov -f "results.xml" 
