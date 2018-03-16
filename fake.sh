#!/bin/bash

dotnet restore dotnet-fake.csproj
dotnet fake $@