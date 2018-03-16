#r "paket:
nuget Fake.IO.FileSystem prerelease
nuget Fake.DotNet.MsBuild prerelease
nuget Fake.Core.Target prerelease //"
#load "./.fake/build.fsx/intellisense.fsx"

open Fake.IO
open Fake.IO.Globbing.Operators //enables !! and globbing
open Fake.DotNet
open Fake.Core
