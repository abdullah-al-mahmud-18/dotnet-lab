# .NET CLI Cheat Sheet

A generic reference for creating, managing, updating, deleting, running, and testing .NET projects and solutions without an IDE.

**Conventions**

- `<...>` = replace with your value. `[...]` = optional.
- Examples use placeholder names: `MyApp`, `MyLib`, `MyApp.Tests`, `MySolution`.
- Commands work in bash, PowerShell, and cmd unless a note says otherwise.
- `<target>` means a project file (`.csproj`), a solution file (`.sln`/`.slnx`), or a folder containing exactly one of them.

---

## Table of contents

1. [SDK and environment](#1-sdk-and-environment)
2. [Creating projects (templates)](#2-creating-projects-templates)
3. [Solution files](#3-solution-files)
4. [Project references](#4-project-references)
5. [NuGet packages](#5-nuget-packages)
6. [Restore, build, clean](#6-restore-build-clean)
7. [Run](#7-run)
8. [Watch (auto-rebuild, hot reload)](#8-watch-auto-rebuild-hot-reload)
9. [Test](#9-test)
10. [Publish and pack](#10-publish-and-pack)
11. [Updating things](#11-updating-things)
12. [Deleting, moving, renaming](#12-deleting-moving-renaming)
13. [Tools (global and local)](#13-tools-global-and-local)
14. [EF Core CLI](#14-ef-core-cli)
15. [Formatting](#15-formatting)
16. [User secrets](#16-user-secrets)
17. [Workloads](#17-workloads)
18. [Project file (.csproj) essentials](#18-project-file-csproj-essentials)
19. [Typical end-to-end workflow](#19-typical-end-to-end-workflow)
20. [Quick reference table](#20-quick-reference-table)
21. [Troubleshooting](#21-troubleshooting)

---

## 1. SDK and environment

```bash
dotnet --info                 # SDK, runtime, OS details
dotnet --version              # SDK version in use in the current folder
dotnet --list-sdks            # installed SDKs
dotnet --list-runtimes        # installed runtimes
dotnet --help                 # list all commands
dotnet <command> --help       # help for one command (e.g. dotnet build --help)
```

### Pin the SDK version (`global.json`)

```bash
dotnet new globaljson --sdk-version <x.y.z> --roll-forward latestFeature
```

- Applies to the folder it is in and everything below it.
- `--roll-forward latestFeature` allows newer feature bands and patches of the same major version.
- Get valid version numbers from `dotnet --list-sdks`.

### Useful config files you can generate

```bash
dotnet new gitignore          # .gitignore for .NET
dotnet new editorconfig       # .editorconfig (code style rules)
dotnet new nugetconfig        # nuget.config (package sources)
dotnet new tool-manifest      # .config/dotnet-tools.json (local tools)
```

---

## 2. Creating projects (templates)

### Syntax

```bash
dotnet new <template> -n <Name> -o <folder> [options]
```

`-n MyApp -o src/MyApp` creates `src/MyApp/MyApp.csproj`. If you omit `-o`, the project is created in the current folder, named after the folder. If you omit `-n`, the name comes from the output folder.

### Discover, install, update, uninstall templates

```bash
dotnet new list                       # all installed templates
dotnet new list <keyword>             # filter, e.g. dotnet new list web
dotnet new list --type project        # project templates only
dotnet new list --language "F#"       # by language
dotnet new search <keyword>           # search NuGet for template packs
dotnet new <template> --help          # options for one template

dotnet new install <PackageId>        # install a template pack (e.g. Avalonia.Templates)
dotnet new uninstall <PackageId>
dotnet new uninstall                  # with no args: list installed template packs
dotnet new update                     # update installed template packs
```

### Common built-in project templates

| Purpose | Command | Notes |
|---|---|---|
| Console app | `dotnet new console` | |
| Class library | `dotnet new classlib` | Reusable code referenced by other projects |
| Empty web app / minimal API | `dotnet new web` | Bare ASP.NET Core |
| Web API | `dotnet new webapi` | Minimal APIs by default in .NET 8+ |
| Web API with controllers | `dotnet new webapi --use-controllers` | Short form: `-controllers` |
| MVC | `dotnet new mvc` | Controllers + Views |
| Razor Pages | `dotnet new razor` | |
| Blazor Web App | `dotnet new blazor` | .NET 8+ unified model |
| Blazor WebAssembly (standalone) | `dotnet new blazorwasm` | |
| Worker service | `dotnet new worker` | Background services |
| gRPC service | `dotnet new grpc` | |
| Razor class library | `dotnet new razorclasslib` | |
| xUnit tests | `dotnet new xunit` | |
| NUnit tests | `dotnet new nunit` | |
| MSTest tests | `dotnet new mstest` | |
| WPF app | `dotnet new wpf` | Windows only |
| WinForms app | `dotnet new winforms` | Windows only |
| MAUI app | `dotnet new maui` | Needs the `maui` workload |
| Avalonia app | `dotnet new avalonia.app` | After `dotnet new install Avalonia.Templates` |
| Avalonia MVVM app | `dotnet new avalonia.mvvm` | Same template pack |

Other (non-project) templates: `sln`, `globaljson`, `gitignore`, `editorconfig`, `nugetconfig`, `tool-manifest`, `webconfig`, `buildprops` (Directory.Build.props), `packagesprops` (Directory.Packages.props). The last two only exist in newer SDKs; confirm with `dotnet new list`.

### Useful options for `dotnet new`

| Option | Meaning |
|---|---|
| `-n`, `--name <name>` | Project name (also default namespace and `.csproj` file name) |
| `-o`, `--output <dir>` | Output folder (created if missing) |
| `-f`, `--framework <tfm>` | Target framework, e.g. `net8.0`, `net10.0` |
| `-lang`, `--language <lang>` | `C#` (default), `F#`, `VB` |
| `--force` | Overwrite existing files |
| `--dry-run` | Preview what would be created |
| `--no-restore` | Skip the automatic restore |
| `--use-program-main` | (console) Explicit `Main` method instead of top-level statements |
| `--aot` | (console/webapi) Enable Native AOT |

### Examples

```bash
dotnet new console   -n MyApp   -o src/MyApp
dotnet new classlib  -n MyLib   -o src/MyLib -f net8.0
dotnet new webapi    -n MyApi   -o src/MyApi
dotnet new xunit     -n MyApp.Tests -o tests/MyApp.Tests
dotnet new console   -n MyApp --dry-run          # preview only
```

### Keep projects out of each other's folders

SDK-style projects automatically compile every `**/*.cs` file under their own folder. If one project sits **inside** another project's folder, the outer project will compile the inner project's files too, causing duplicate-type or missing-package errors.

- Preferred: keep projects as siblings (`src/MyApp`, `tests/MyApp.Tests`).
- If nesting is unavoidable, exclude the inner folder in the outer `.csproj`:

```xml
<ItemGroup>
  <Compile Remove="MyApp.Tests/**" />
  <Content Remove="MyApp.Tests/**" />
  <EmbeddedResource Remove="MyApp.Tests/**" />
  <None Remove="MyApp.Tests/**" />
</ItemGroup>
```

---

## 3. Solution files

A solution groups projects so you can build and test them together. It contains no code, only a list of projects.

> **.sln vs .slnx:** The .NET 10 SDK creates the newer XML-based `.slnx` by default. Older SDKs create `.sln`. All `dotnet sln` commands work with both.

### Create

```bash
dotnet new sln -n MySolution                  # .sln (or .slnx on .NET 10 SDK)
dotnet new sln -n MySolution --format sln     # force classic format (.NET 10 SDK)
dotnet new sln -n MySolution --format slnx    # force .slnx (.NET 10 SDK)
dotnet new sln -n MySolution -o path/to/dir   # create in a specific folder
```

### Add projects

```bash
dotnet sln add src/MyApp/MyApp.csproj
dotnet sln add src/A/A.csproj src/B/B.csproj             # several at once
dotnet sln add src/**/*.csproj                           # glob (bash expands it)
dotnet sln add src/MyLib/MyLib.csproj --solution-folder libs   # virtual solution folder
dotnet sln MySolution.sln add src/MyApp/MyApp.csproj     # name the solution explicitly
```

If the current folder holds more than one solution file, you must name the solution: `dotnet sln <solution> add <project>`.

PowerShell has no `**` expansion, so use:

```powershell
dotnet sln add (Get-ChildItem -Recurse -Filter *.csproj)
```

### List, remove, migrate

```bash
dotnet sln list                                  # projects in the solution
dotnet sln remove src/MyApp/MyApp.csproj         # remove from solution (files stay on disk)
dotnet sln remove src/A/A.csproj src/B/B.csproj  # several at once
dotnet sln migrate                               # convert .sln to .slnx (.NET 9.0.200+ / .NET 10)
```

### Building and testing via the solution

```bash
dotnet build MySolution.sln
dotnet test  MySolution.sln
dotnet clean MySolution.sln
dotnet restore MySolution.sln
```

### Delete a solution

A solution is just a file. Delete `MySolution.sln` (or `.slnx`). Your projects are unaffected.

---

## 4. Project references

Project-to-project dependencies (for example, a test project referencing the app, or an app referencing a library).

```bash
# Run inside the project that needs the dependency
dotnet add reference ../MyLib/MyLib.csproj
dotnet add reference ../A/A.csproj ../B/B.csproj      # several

dotnet list reference                                 # show references
dotnet remove reference ../MyLib/MyLib.csproj         # remove

# From any folder, naming the project explicitly
dotnet add src/MyApp/MyApp.csproj reference src/MyLib/MyLib.csproj
dotnet remove src/MyApp/MyApp.csproj reference src/MyLib/MyLib.csproj
dotnet list src/MyApp/MyApp.csproj reference
```

Circular references (A references B, B references A) are not allowed. Extract shared code into a third library.

---

## 5. NuGet packages

### Add, remove, inspect

```bash
dotnet add package <PackageId>
dotnet add package <PackageId> --version <x.y.z>
dotnet add package <PackageId> --prerelease
dotnet add <project.csproj> package <PackageId>       # target a specific project

dotnet remove package <PackageId>
dotnet remove <project.csproj> package <PackageId>

dotnet list package                                   # direct dependencies
dotnet list package --include-transitive              # plus indirect ones
dotnet list package --outdated                        # newer versions available
dotnet list package --deprecated                      # deprecated packages
dotnet list package --vulnerable                      # known vulnerabilities
dotnet list package --vulnerable --include-transitive
dotnet list package --format json                     # machine-readable output (newer SDKs)

dotnet package search <keyword> --take 10             # search NuGet (newer SDKs)
dotnet nuget why <project.csproj> <PackageId>         # why is this package in my graph? (newer SDKs)
```

Newer SDKs (.NET 10) also accept noun-first forms: `dotnet package add`, `dotnet package list`, `dotnet package remove`. The older forms keep working.

### Sources and caches

```bash
dotnet nuget list source
dotnet nuget add source <url-or-path> --name <SourceName>
dotnet nuget add source <url> --name <SourceName> --username <u> --password <p> --store-password-in-clear-text
dotnet nuget disable source <SourceName>
dotnet nuget enable source <SourceName>
dotnet nuget remove source <SourceName>

dotnet nuget locals all --list         # show cache locations
dotnet nuget locals all --clear        # clear caches (fixes odd restore problems)
```

### Central package management (optional; helpful with many projects)

Create `Directory.Packages.props` at the repository or solution root:

```xml
<Project>
  <PropertyGroup>
    <ManagePackageVersionsCentrally>true</ManagePackageVersionsCentrally>
  </PropertyGroup>
  <ItemGroup>
    <PackageVersion Include="Serilog" Version="4.2.0" />
  </ItemGroup>
</Project>
```

Projects then use `<PackageReference Include="Serilog" />` with no version.

---

## 6. Restore, build, clean

```bash
dotnet restore [<target>]              # download packages (build/run/test restore automatically)
dotnet build [<target>]                # build (Debug by default)
dotnet build -c Release                # Release configuration
dotnet build --no-restore              # skip restore
dotnet build --no-incremental          # force full rebuild
dotnet build -o ./out                  # custom output folder
dotnet build -f net8.0                 # one framework (multi-targeted projects)
dotnet build -v minimal                # verbosity: quiet | minimal | normal | detailed | diagnostic
dotnet build -p:Property=Value         # pass an MSBuild property
dotnet clean [<target>]                # delete build outputs
dotnet build-server shutdown           # stop background build servers (fixes stale locks)
```

Facts to remember:

- If a folder contains **more than one** project or solution file, `dotnet build` fails with an ambiguity error. Pass the file explicitly.
- Outputs go to `bin/<Configuration>/<TargetFramework>/`; intermediates go to `obj/`.
- `dotnet clean` removes outputs but leaves `obj/` restore data. For a full reset, delete `bin/` and `obj/` and restore again:

```bash
# bash
find . -type d \( -name bin -o -name obj \) -prune -exec rm -rf {} +
```

```powershell
# PowerShell
Get-ChildItem -Recurse -Directory -Include bin,obj | Remove-Item -Recurse -Force
```

---

## 7. Run

```bash
dotnet run                                 # project in the current folder
dotnet run --project <folder-or-csproj>    # run a project from anywhere
dotnet run -c Release
dotnet run --no-build                      # skip build
dotnet run -f net8.0                       # pick framework if multi-targeted
dotnet run --launch-profile <name>         # profile from Properties/launchSettings.json
dotnet run -- arg1 arg2 --flag value       # everything after -- goes to your program
```

Note: `dotnet run` runs a **project**, not a solution. In a solution with several runnable projects, use `--project`.

### Web projects

```bash
dotnet run --urls "http://localhost:5050"        # override URLs
dotnet run --launch-profile https                # use the https profile
dotnet dev-certs https --trust                   # trust the local dev certificate (Windows/macOS)
dotnet dev-certs https --clean                   # remove it
```

On Linux, `--trust` support is limited, so the `http` profile is often easier for local work.

### Environment variables

```bash
# bash
ASPNETCORE_ENVIRONMENT=Development dotnet run
DOTNET_ENVIRONMENT=Development dotnet run          # non-web hosts
```

```powershell
# PowerShell
$env:ASPNETCORE_ENVIRONMENT = "Development"; dotnet run
```

### Single-file C# programs (.NET 10 SDK)

```bash
dotnet run hello.cs                        # run one .cs file with no .csproj
```

Handy for quick experiments when a full project is overkill.

---

## 8. Watch (auto-rebuild, hot reload)

```bash
dotnet watch                               # same as dotnet watch run
dotnet watch run
dotnet watch --project <folder-or-csproj>
dotnet watch test                          # re-run tests when files change
dotnet watch run --no-hot-reload           # restart on change instead of hot reload
dotnet watch run -- arg1 arg2              # pass args to your app
```

---

## 9. Test

```bash
dotnet test [<target>]                     # all tests in the project or solution
dotnet test --no-build                     # skip build
dotnet test --no-restore
dotnet test -c Release
dotnet test --list-tests                   # list tests without running
dotnet test -v normal                      # more output
dotnet test --logger "console;verbosity=detailed"   # per-test output
dotnet test --logger trx                   # write .trx results
dotnet test --results-directory ./TestResults
dotnet test --blame                        # find tests that crash the test host
dotnet test -- <runsettings args>          # args after -- go to the test runner
dotnet test --settings <file>.runsettings
```

### Filtering

```bash
dotnet test --filter "FullyQualifiedName~MyClassTests"             # contains
dotnet test --filter "FullyQualifiedName=MyNamespace.MyClass.MyTest"   # exact
dotnet test --filter "Name~Add"                                    # by method name
dotnet test --filter "Category=Unit"                               # trait/category
dotnet test --filter "FullyQualifiedName~Add&Category!=Slow"       # combine
```

Filter operators: `=` equals, `!=` not equals, `~` contains, `!~` does not contain. Combine with `&` (and) and `|` (or). Property names available (e.g. `Category`, `Priority`) depend on the test framework.

### Code coverage

```bash
dotnet test --collect:"XPlat Code Coverage"
```

Writes `coverage.cobertura.xml` under `TestResults/<guid>/`. Requires the `coverlet.collector` package (included in the xUnit template).

---

## 10. Publish and pack

### Publish (deployable output)

```bash
dotnet publish -c Release -o ./publish
dotnet publish -c Release -r <rid> --self-contained true       # bundles the .NET runtime
dotnet publish -c Release -r <rid> --self-contained false      # needs .NET on the target machine
dotnet publish -c Release -r <rid> -p:PublishSingleFile=true --self-contained true
dotnet publish -c Release -r <rid> --self-contained true -p:PublishTrimmed=true
dotnet publish -c Release -p:PublishAot=true                   # Native AOT (needs platform toolchain)
```

Common runtime identifiers (RIDs): `win-x64`, `win-arm64`, `linux-x64`, `linux-arm64`, `osx-x64`, `osx-arm64`.

Trimming and AOT can break code that relies heavily on reflection; test the published output.

### Pack (NuGet package from a class library)

```bash
dotnet pack -c Release -o ./nupkgs
dotnet nuget push ./nupkgs/<Package>.<version>.nupkg --source <feed-url> --api-key <key>
dotnet nuget delete <PackageId> <version> --source <feed-url> --api-key <key>   # unlist/delete on a feed
```

---

## 11. Updating things

| What | How |
|---|---|
| A NuGet package | `dotnet add package <PackageId> --version <new>` (re-adding sets the new version) |
| Find outdated packages | `dotnet list package --outdated` |
| Update all packages | Update each from the outdated list, or use the `dotnet-outdated-tool` (see section 13) |
| Target framework | Edit `<TargetFramework>` in the `.csproj` (e.g. `net8.0` to `net10.0`), then `dotnet build` |
| Pinned SDK version | Edit `global.json` or regenerate with `dotnet new globaljson --force ...` |
| Template packs | `dotnet new update` |
| Global tools | `dotnet tool update -g <tool>` |
| Local tools | `dotnet tool update <tool>` |
| Workloads | `dotnet workload update` |
| Solution format | `dotnet sln migrate` (.sln to .slnx) |
| The .NET SDK itself | Install a newer SDK with your OS package manager or installer; check with `dotnet --list-sdks` |

After changing target frameworks or major package versions, run a clean restore and build, then run the tests:

```bash
dotnet restore --force
dotnet build --no-incremental
dotnet test
```

---

## 12. Deleting, moving, renaming

### Delete a project

There is no `dotnet delete`. Do it in this order:

```bash
dotnet sln remove src/MyLib/MyLib.csproj                          # 1. detach from solution(s)
dotnet remove src/MyApp/MyApp.csproj reference src/MyLib/MyLib.csproj   # 2. detach from every project that references it
rm -rf src/MyLib                                                  # 3. delete the folder (PowerShell: Remove-Item -Recurse -Force src/MyLib)
dotnet build MySolution.sln                                       # 4. confirm nothing still depends on it
```

### Delete a package, a reference, or a solution

```bash
dotnet remove package <PackageId>
dotnet remove reference <path.csproj>
dotnet sln remove <path.csproj>
# Solution file: delete the .sln/.slnx file itself
```

### Move or rename a project

1. `dotnet sln remove <old.csproj>`
2. Move/rename the folder or `.csproj` file.
3. `dotnet sln add <new.csproj>`
4. Fix `ProjectReference` paths in dependants: `dotnet remove reference <old>` then `dotnet add reference <new>`.
5. Update namespaces, and `<RootNamespace>` / `<AssemblyName>` if set in the `.csproj`.
6. Delete stale `bin/` and `obj/`, then `dotnet build`.

---

## 13. Tools (global and local)

```bash
# Global tools (per user, available everywhere)
dotnet tool install -g <tool>
dotnet tool install -g <tool> --version <x.y.z>
dotnet tool list -g
dotnet tool update -g <tool>
dotnet tool uninstall -g <tool>
dotnet tool search <keyword>

# Local tools (pinned per repo in .config/dotnet-tools.json; best for reproducibility)
dotnet new tool-manifest
dotnet tool install <tool>
dotnet tool restore                     # install everything listed in the manifest
dotnet tool list
dotnet tool update <tool>
dotnet tool uninstall <tool>
dotnet tool run <tool-command>          # or run the tool command directly, e.g. dotnet ef
```

Handy tools:

| Tool | Package ID | Use |
|---|---|---|
| EF Core CLI | `dotnet-ef` | Migrations and database updates |
| Outdated packages | `dotnet-outdated-tool` | `dotnet outdated` shows and applies updates |
| Coverage reports | `dotnet-reportgenerator-globaltool` | HTML reports from coverage files |
| Diagnostics | `dotnet-counters`, `dotnet-trace`, `dotnet-dump` | Runtime performance and crash analysis |

---

## 14. EF Core CLI

Requires `dotnet-ef` and the `Microsoft.EntityFrameworkCore.Design` package in the startup project.

```bash
dotnet ef migrations add <MigrationName>
dotnet ef migrations list
dotnet ef migrations remove                 # remove the last (unapplied) migration
dotnet ef migrations script                 # generate SQL script
dotnet ef database update                   # apply all pending migrations
dotnet ef database update <MigrationName>   # move to a specific migration (can roll back)
dotnet ef database drop
dotnet ef dbcontext list
dotnet ef dbcontext scaffold "<connection-string>" <Provider>   # reverse-engineer a database
```

When the DbContext lives in a different project from the startup project, add:

```bash
--project <data-project> --startup-project <startup-project>
```

---

## 15. Formatting

```bash
dotnet format                               # apply formatting and style fixes (uses .editorconfig)
dotnet format --verify-no-changes           # check only (good for CI)
dotnet format whitespace                    # whitespace only
dotnet format style                         # code style only
dotnet format analyzers                     # analyzer fixes only
```

---

## 16. User secrets

For local-only configuration such as API keys. Stored outside the project folder, so they are never committed.

```bash
dotnet user-secrets init                    # adds a UserSecretsId to the csproj
dotnet user-secrets set "ApiKey" "value"
dotnet user-secrets set "ConnectionStrings:Default" "Server=..."
dotnet user-secrets list
dotnet user-secrets remove "ApiKey"
dotnet user-secrets clear
```

ASP.NET Core loads them automatically in the Development environment.

---

## 17. Workloads

Optional SDK components (MAUI, WebAssembly tooling, etc.).

```bash
dotnet workload list
dotnet workload search
dotnet workload install <workload-id>       # e.g. maui, wasm-tools
dotnet workload update
dotnet workload uninstall <workload-id>
dotnet workload clean
```

---

## 18. Project file (.csproj) essentials

```xml
<Project Sdk="Microsoft.NET.Sdk">           <!-- Sdk differs by type: Microsoft.NET.Sdk.Web, .Razor, .BlazorWebAssembly, ... -->

  <PropertyGroup>
    <OutputType>Exe</OutputType>            <!-- Exe for runnable apps; omit for libraries -->
    <TargetFramework>net8.0</TargetFramework>
    <!-- Multi-target: <TargetFrameworks>net8.0;net10.0</TargetFrameworks> -->
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
    <LangVersion>latest</LangVersion>
    <TreatWarningsAsErrors>false</TreatWarningsAsErrors>
    <RootNamespace>MyApp</RootNamespace>
    <AssemblyName>MyApp</AssemblyName>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Serilog" Version="4.2.0" />
    <ProjectReference Include="..\MyLib\MyLib.csproj" />
  </ItemGroup>

  <ItemGroup>
    <None Update="appsettings.json" CopyToOutputDirectory="PreserveNewest" />
  </ItemGroup>

</Project>
```

### Share settings across many projects

- `Directory.Build.props`: MSBuild properties applied to every project in that folder and below (e.g. `Nullable`, `ImplicitUsings`, `TreatWarningsAsErrors`).
- `Directory.Build.targets`: same idea, applied after project evaluation.
- `Directory.Packages.props`: central NuGet versions (see section 5).

Generate a starter file with `dotnet new buildprops` if your SDK has that template, or create the file by hand:

```xml
<Project>
  <PropertyGroup>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>
</Project>
```

---

## 19. Typical end-to-end workflow

A generic sequence you can adapt to any project or solution.

```bash
# 1. Setup
mkdir MySolution && cd MySolution
git init
dotnet new gitignore
dotnet new globaljson --sdk-version <x.y.z> --roll-forward latestFeature

# 2. Create solution and projects
dotnet new sln -n MySolution
dotnet new classlib -n MyLib       -o src/MyLib
dotnet new console  -n MyApp       -o src/MyApp
dotnet new xunit    -n MyApp.Tests -o tests/MyApp.Tests

# 3. Wire them together
dotnet sln add src/MyLib/MyLib.csproj src/MyApp/MyApp.csproj tests/MyApp.Tests/MyApp.Tests.csproj
dotnet add src/MyApp/MyApp.csproj reference src/MyLib/MyLib.csproj
dotnet add tests/MyApp.Tests/MyApp.Tests.csproj reference src/MyApp/MyApp.csproj

# 4. Add packages
dotnet add src/MyApp/MyApp.csproj package Serilog

# 5. Build, run, test
dotnet build
dotnet run --project src/MyApp
dotnet test

# 6. Maintain
dotnet list package --outdated
dotnet format
dotnet clean

# 7. Ship
dotnet publish src/MyApp -c Release -o ./publish
```

---

## 20. Quick reference table

| Task | Command |
|---|---|
| New project | `dotnet new <template> -n <Name> -o <folder>` |
| List templates | `dotnet new list` |
| New solution | `dotnet new sln -n <Name>` |
| Add project to solution | `dotnet sln add <path.csproj>` |
| List solution projects | `dotnet sln list` |
| Remove project from solution | `dotnet sln remove <path.csproj>` |
| Add project reference | `dotnet add reference <path.csproj>` |
| Remove project reference | `dotnet remove reference <path.csproj>` |
| List project references | `dotnet list reference` |
| Add NuGet package | `dotnet add package <Id> [--version x.y.z]` |
| Remove NuGet package | `dotnet remove package <Id>` |
| List packages | `dotnet list package` |
| Outdated / vulnerable packages | `dotnet list package --outdated` / `--vulnerable` |
| Restore | `dotnet restore` |
| Build | `dotnet build [-c Release]` |
| Clean | `dotnet clean` |
| Run | `dotnet run [--project <path>] [-- args]` |
| Hot reload | `dotnet watch` |
| Test | `dotnet test [--filter "..."]` |
| Test with coverage | `dotnet test --collect:"XPlat Code Coverage"` |
| Publish | `dotnet publish -c Release -o ./publish` |
| Pack NuGet | `dotnet pack -c Release` |
| Format | `dotnet format` |
| Install global tool | `dotnet tool install -g <tool>` |
| Install local tool | `dotnet new tool-manifest` then `dotnet tool install <tool>` |
| Restore local tools | `dotnet tool restore` |
| EF migration | `dotnet ef migrations add <Name>` then `dotnet ef database update` |
| Set secret | `dotnet user-secrets set "<key>" "<value>"` |
| Show SDK info | `dotnet --info` |
| Clear NuGet caches | `dotnet nuget locals all --clear` |

---

## 21. Troubleshooting

| Problem | Likely cause and fix |
|---|---|
| "MSB1011: more than one project or solution file" | The folder has several. Pass the file: `dotnet build <file>` |
| "Couldn't find a project to run" | Wrong folder. Use `--project <path>` |
| `dotnet run` on a solution does nothing useful | `run` needs a project. Use `--project` |
| Duplicate type definitions in a project | Another project's `.cs` files are inside its folder (see section 2) |
| Restore fails or odd package errors | `dotnet nuget locals all --clear`, then `dotnet restore` |
| Old output keeps running, or files locked | `dotnet build-server shutdown`, then close the running app |
| "SDK not found" or wrong SDK version | Compare `global.json` with `dotnet --list-sdks` |
| HTTPS certificate errors on Linux | Use the `http` launch profile |
| Tests not discovered | Missing `Microsoft.NET.Test.Sdk` package, or the test project doesn't reference the code under test |
| Package restored but types not found | Wrong target framework for that package, or a missing `using`. Check `dotnet list package` |
| Strange state after switching branches | Delete `bin/` and `obj/`, then `dotnet restore` |
