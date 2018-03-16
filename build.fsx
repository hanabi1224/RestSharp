#r "paket:
nuget Fake.IO.FileSystem prerelease
nuget Fake.DotNet.MsBuild prerelease
nuget Fake.Testing.Common prerelease
nuget Fake.Testing.SonarQube prerelease
nuget Fake.DotNet.Nuget prerelease
nuget Fake.Core.Target prerelease //"
#load "./.fake/build.fsx/intellisense.fsx"

open Fake.IO
open Fake.IO.Globbing.Operators //enables !! and globbing
open Fake.DotNet
open Fake.Core
open Fake.Testing
open Fake.DotNet.NuGet
open Fake.DotNet.NuGet

// Properties
let buildDir = "./build/"

// Targets
Target.Create "Clean" (fun _ ->
  Shell.CleanDir buildDir
)

Target.Create "Install" (fun _ ->
  Install.NugetInstall(fun p -> {p with OutputDirectory = "./tools"}) "JetBrains.dotCover.CommandLineTools"
)

Target.Create "SonarQubeStart" (fun _ ->
  SonarQube.Begin(fun p -> 
    {p with
      Key = "restsharp"
      Settings = ["sonar.organization=restsharp"; 
        "sonar.host.url=\"https://sonarcloud.io\"";
        "sonar.login=\"46f9f7e17726eeb1414ece5f31dfe280b4f86a71\""]
    })
)

Target.Create "SonarQubeFinish" (fun _ ->
  SonarQube.End(Some(fun p -> { p with Settings = ["sonar.login=\"46f9f7e17726eeb1414ece5f31dfe280b4f86a71\""] }))
)

Target.Create "BuildApp" (fun _ ->
    MsBuild.RunWithDefaults "Build" ["./RestSharp.sln"]
    |> Trace.Log "AppBuild-Output: "
)

Target.Create "Default" (fun _ ->
  Trace.trace "All seems to be fine"
)

open Fake.Core.TargetOperators

"Clean"
  ==> "Install"
  ==> "SonarQubeStart"
  ==> "BuildApp"
  ==> "SonarQubeFinish"
  ==> "Default"

// start build
Target.RunOrDefault "Default"