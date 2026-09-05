# LoggerWrapper Project Review

Review date: 2026-09-05

## Executive summary

LoggerWrapper is a small logging abstraction composed of a .NET Standard 2.0 core library, a Common.Logging/log4net adapter, and a Windows Event Log adapter. The core project and the full solution both build cleanly from the source recorded on this branch, and `dotnet test` discovers and runs the full suite: 153 tests, all passing.

Every finding raised by this review has been addressed on this branch. The sections below describe the state the review started from and what changed; the Findings section records each item and its resolution.

## Project structure

| Project | Target | Purpose |
| --- | --- | --- |
| `LoggerWrapper` | .NET Standard 2.0 | Core interfaces, wrappers, level filtering, no-op logger, and reflection-based logger adaptation |
| `LoggerWrapperCommonLog` | .NET Framework 4.7.2 | Common.Logging and log4net integration |
| `LoggerWrapperWindows` | .NET Framework 4.7.2 | Windows Event Log integration |
| `TestsBase` | .NET Framework 4.7.2 | Core library tests |
| `TestsCommonLog` | .NET Framework 4.7.2 | Common.Logging and log4net tests |
| `TestsWindows` | .NET Framework 4.7.2 | Windows Event Log tests |

The core API is centered on `ILoggerBasic`, `ILogger`, `LoggerBase`, and `LoggerWrapper`. `LoggerWrapper.CreateFromILogLikeObject` discovers conventional `Fatal`, `Error`, `Warn`, `Trace`, `Debug`, and `Info` overloads through reflection and converts them to delegates. `LoggerWithLevels` adds threshold-based dispatch, while `LogManager` acts as a global logger factory.

## Build assessment

The review used .NET SDK 10.0.302 on Windows.

### Core project

Command:

```powershell
dotnet build .\LoggerWrapper\LoggerWrapper.csproj --configuration Release
```

Result: succeeded with no errors.

### Full solution

Command:

```powershell
dotnet build .\LoggerWrapper.sln --configuration Release
```

A first run against the project references as they stood before this branch failed in `LoggerWrapperCommonLog`. The project expected `packages\log4net.2.0.8\lib\net45-full\log4net.dll`, while `packages.config` restored log4net 2.0.10.

This branch corrects that: the projects now reference the restored 2.0.10 package and its 2.0.9 assembly version. All six projects build successfully with no errors.

The only remaining build warning is `NU1902`: log4net 2.0.10 carries a known moderate-severity advisory (GHSA-4f7c-pmjv-c25w). Moving to a patched log4net is left to the dependency-update process rather than this review.

### Test execution

Command:

```powershell
dotnet test .\LoggerWrapper.sln --configuration Release
```

Result: 153 tests discovered and executed, all passing — 91 in `TestsBase`, 50 in `TestsCommonLog`, 12 in `TestsWindows`.

Before this branch the same command reported a successful build but discovered no tests, because the test projects referenced NUnit directly without `Microsoft.NET.Test.Sdk` or `NUnit3TestAdapter`. A green `dotnet test` was therefore meaningless.

## Findings

### Resolved baseline: the dependency references did not build

Before this branch, the Common.Logging project files pointed to log4net 2.0.8 while their `packages.config` files specified 2.0.10. This produced an unresolved assembly warning followed by a compile error in `Log4NetLogger.cs`. The project-reference and binding-redirect updates on this branch resolve the mismatch, so this is recorded as the review's starting state rather than an open issue.

### Resolved — Medium: trace logging checked the debug flag

All three `Trace` overloads in `LoggerWrapper.cs` checked `IsDebugEnabled` instead of `IsTraceEnabled`. A logger with different trace and debug settings could therefore emit disabled trace messages or suppress enabled ones.

Fixed: the three overloads now check `IsTraceEnabled`. `Tests/LoggerWrapperTraceAndPrefixTests.cs` adds regression coverage with the trace and debug states deliberately differing in both directions; those tests fail against the previous behaviour.

### Resolved — Medium: automated tests were not wired into the build

`dotnet test` ran zero tests, which could produce a false green result locally or in CI.

Fixed: all three test projects were converted to SDK-style projects using `PackageReference`, and now include `Microsoft.NET.Test.Sdk` and `NUnit3TestAdapter` alongside NUnit. The suite is discovered and executed by `dotnet test`.

Six `TestsWindows` cases previously wrote to the real Windows Event Log and failed without elevation. They assert only on the exposed `LogMessageHandler` events, so — like their sibling tests in the same fixture — they now substitute a no-op `WriteEntryToEventLog` sink and pass without elevation.

### Resolved — Medium: clean-clone builds were not reproducible

Most projects used non-SDK-style project files, `packages.config`, and relative assembly hint paths into the git-ignored `packages` directory. There was no CI workflow or documented restore sequence, and the build succeeded only where that directory was already populated.

Fixed: every project in the solution is now SDK-style and uses `PackageReference`; no `packages.config` file remains and no project points into `packages`. This was verified by moving the local `packages` directory aside and running a full restore, build, test, and pack from scratch. A GitHub Actions workflow (`.github/workflows/build.yml`) now restores, builds Release, runs the tests, and packs the NuGet package on Windows, and the README documents the same sequence.

### Resolved — Low: generated log prefixes were ambiguous

`LogMethodsWithPrefix` used `DateTime.Today`, recording midnight rather than the event time, and inserted no separator between the level and the message, so output could read `InfoMessage`.

Fixed: the prefix now uses `DateTime.Now` formatted as `yyyy-MM-dd HH:mm:ss.fff` with `CultureInfo.InvariantCulture`, and ends with a colon and a space. A prefix now reads `2026-09-05 21:28:14.402 [thread] Name Info: Message`. Both properties are covered by regression tests.

## README review

The README stated the project's purpose and linked to the published package, but was too brief to support installation, evaluation, or contribution. It has been rewritten on this branch.

### Corrections applied

- `LoggerWrapper` was described as the base class; the base class is `LoggerBase` and `LoggerWrapper` is a concrete implementation.
- `LogggerWrapperWindows` was misspelled.
- Grammatical fixes, including the sentences beginning "It specifically for library developers" and "with his interface".
- The MIT claim is now backed by a root `LICENSE` file.
- The add-on projects' .NET Framework 4.7.2 and Windows-specific requirements are stated.
- `CreateFromILogLikeObject` is described precisely: the required six level names, the accepted overload shapes, the requirement that each resolve unambiguously, and the exception thrown when one does not.

### Content added

- A `dotnet add package AnotherLoggerWrapper --version 1.0.0` installation example
- A minimal wrapping and logging example, and a delegate-based example
- Examples for `LoggerWithLevels`, `NoOpLogger`, and `LogManager`
- Restore, build, and test instructions
- A compatibility table for the core and adapter projects
- Reflection requirements and failure behaviour
- Contribution guidance

The Markdown linter issues (headings not surrounded by blank lines, multiple top-level headings, trailing blank lines) are resolved by the rewrite.

## Packaging

The core project now declares `PackageLicenseExpression` (MIT) and ships the `LICENSE` and `README.md` files in the package, which also clears the "package is missing a readme" pack warning. Dead TFS source-control bindings (`SccProjectName` and friends) were dropped from the project files during the SDK-style conversion.

## Remaining follow-ups

1. Move log4net off 2.0.10 to clear the `NU1902` advisory warning, once a patched version compatible with `Common.Logging.Log4Net.Universal` is selected.
