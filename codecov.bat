choco install opencover.portable
choco install codecov
OpenCover.Console.exe -register:user -target:"C:\Program Files\dotnet\dotnet.exe" -targetargs:"test --framework net452 --verbosity q"
codecov -f "results.xml" 
