# LoggerWrapper Project Review

Review date: 2026-09-05

## Executive summary

LoggerWrapper is a small logging abstraction composed of a .NET Standard 2.0 core library, a Common.Logging/log4net adapter, and a Windows Event Log adapter. The core project builds cleanly. The full solution also builds in the current working tree, but only after uncommitted dependency-reference changes that align the Common.Logging projects with the restored log4net package.

The test assemblies compile, but the repository does not include the test SDK or NUnit adapter required for `dotnet test` to discover and execute the tests. A successful `dotnet test` exit therefore does not currently demonstrate that the tests pass.

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

The first run against the checked-in project references failed in `LoggerWrapperCommonLog`. The project expected `packages\log4net.2.0.8\lib\net45-full\log4net.dll`, while `packages.config` restored log4net 2.0.10.

During the review, the working tree was updated to reference the restored 2.0.10 package and its 2.0.9 assembly version. With those uncommitted changes, all six projects built successfully with no warnings or errors. The relevant modified files were:

- `LoggerWrapperCommonLog/LoggerWrapperCommonLog.csproj`
- `LoggerWrapperCommonLog/app.config`
- `Tests/app.config`
- `TestsCommonLog/TestsCommonLog.csproj`
- `TestsCommonLog/app.config`

The dependency updates must be committed before the repository's default branch can be considered buildable from its recorded source state.

### Test execution

Command:

```powershell
dotnet test .\LoggerWrapper.sln --configuration Release --no-build --logger "console;verbosity=normal"
```

Result: the command reported a successful build but did not discover or execute any tests. The legacy test projects reference NUnit 3.6.1 directly and do not include `Microsoft.NET.Test.Sdk` or `NUnit3TestAdapter`.

The repository also relies on legacy `packages.config` dependencies and hard-coded paths under `/packages`, which is ignored by Git. A clean-clone build needs an explicit package restore process, but that process is not documented or automated in the repository.

## Findings

### High: the committed dependency references do not build

The committed Common.Logging project files point to log4net 2.0.8, while their `packages.config` files specify 2.0.10. This produced an unresolved assembly warning followed by a compile error in `Log4NetLogger.cs`. The current uncommitted project and binding-redirect changes correct the mismatch.

### Medium: trace logging checks the debug flag

All three `Trace` overloads in `LoggerWrapper.cs` check `IsDebugEnabled` instead of `IsTraceEnabled`. A logger with different trace and debug settings can therefore emit disabled trace messages or suppress enabled trace messages. The overloads should use `IsTraceEnabled`, with a regression test where the trace and debug states differ.

### Medium: automated tests are not wired into the build

The test source is substantial, but `dotnet test` currently runs zero tests. This can create a false green result in local development or CI. The test projects should include a compatible test SDK and NUnit adapter, or the repository should document and automate a supported NUnit console runner.

### Medium: clean-clone builds are not reproducible yet

Most projects use non-SDK-style project files, `packages.config`, and relative assembly hint paths. There is no `global.json`, CI workflow, or documented restore sequence. The build succeeded only with the ignored `packages` directory already populated.

### Low: generated log prefixes are ambiguous

`LogMethodsWithPrefix` uses `DateTime.Today`, which records midnight rather than the event time. It also does not insert a separator between the level and the message, so output can resemble `InfoMessage`. A timestamp format and explicit delimiter would make generated entries clearer and culture-independent.

## README review

The README states the project's purpose and links to the published package, but it is too brief to support installation, evaluation, or contribution.

### Accurate information

- The core package targets .NET Standard 2.0.
- The NuGet package `AnotherLoggerWrapper` exists at version 1.0.0 and targets .NET Standard 2.0.
- The repository contains Common.Logging and Windows Event Log integrations.

### Corrections needed

- `LoggerWrapper` is described as the base class, but the actual base class is `LoggerBase`; `LoggerWrapper` is a concrete implementation.
- `LogggerWrapperWindows` is misspelled.
- Several sentences need grammatical correction, including “It specifically for library developers” and “with his interface.”
- The claim that the project is MIT-licensed should be backed by a root `LICENSE` file.
- The add-on projects' .NET Framework 4.7.2 and Windows-specific requirements are not stated.
- `CreateFromILogLikeObject` is described too loosely. Compatible objects must expose the expected six level names and supported message/exception overloads.

### Missing content

- A `dotnet add package AnotherLoggerWrapper --version 1.0.0` installation example
- A minimal wrapping and logging example
- Examples for `LoggerWithLevels`, `NoOpLogger`, and `LogManager`
- Restore, build, and test instructions
- A compatibility table for the core and adapter projects
- Known reflection requirements and failure behavior
- Contribution and release guidance

The Markdown linter also reports eight formatting issues: headings are not surrounded by blank lines, multiple top-level headings are used, and the file ends with excessive blank lines.

## Recommended next steps

1. Commit and verify the log4net 2.0.10 project-reference and binding-redirect updates.
2. Change the three `Trace` overloads to use `IsTraceEnabled` and add a focused regression test.
3. Add a supported NUnit adapter/test SDK and verify that the expected test count executes.
4. Rewrite `README.md` with installation, usage, compatibility, and build instructions.
5. Add the MIT license text as a root `LICENSE` file and declare `PackageLicenseExpression` in the package metadata.
6. Add a CI workflow that restores from a clean checkout, builds Release, runs tests, and packs the NuGet package.
