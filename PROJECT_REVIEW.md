# LoggerWrapper Project Review

Review date: 2026-09-05. Second pass: 2026-09-06. Fixes applied: 2026-09-07.

## Executive summary

LoggerWrapper is a small logging abstraction composed of a .NET Standard 2.0 core library, a Common.Logging/log4net adapter, and a Windows Event Log adapter. The core project and the full solution both build cleanly from the source recorded on this branch, and `dotnet test` discovers and runs the full suite: 173 tests, all passing.

Every finding raised by the first pass has been addressed. The sections below describe the state the review started from and what changed; the Findings section records each item and its resolution.

A second pass on 2026-09-06 re-read the library source and found further issues that the first pass did not cover. The one with a user-visible correctness impact — the level toggles on `LoggerWrapper` — was fixed on 2026-09-07 and is recorded as resolved below. The remaining second-pass items are still open.

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

The only remaining build warning was `NU1902`: log4net 2.0.10 carries a known moderate-severity advisory (GHSA-4f7c-pmjv-c25w). This has since been resolved by moving to log4net 3.4.0 (see the follow-ups below); the solution now builds with no warnings.

### Test execution

Command:

```powershell
dotnet test .\LoggerWrapper.sln --configuration Release
```

Result: 173 tests discovered and executed, all passing — 111 in `TestsBase`, 50 in `TestsCommonLog`, 12 in `TestsWindows`. `TestsBase` grew from 91 to 111 with the level-toggle regression tests added on 2026-09-07.

Before this branch the same command reported a successful build but discovered no tests, because the test projects referenced NUnit directly without `Microsoft.NET.Test.Sdk` or `NUnit3TestAdapter`. A green `dotnet test` was therefore meaningless.

## Findings (first pass, resolved)

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

## Open findings (second pass, 2026-09-06)

These were found by re-reading the library source after the first pass closed. The first has since been fixed; the rest are still open.

### Resolved — Medium-high: the level toggles did nothing on `LoggerWrapper`

`LoggerBase` declares `IsFatalEnabled` through `IsInfoEnabled` as virtual properties with both a getter and a setter over private backing fields. `LoggerWrapper` overrode only the getters, which read `fatalLog.IsEnabled` and its siblings instead. C# allows an override to supply a single accessor, so the setter is inherited unchanged: assigning to it writes a `LoggerBase` field that the `LoggerWrapper` getter never reads.

The result is that `logger.IsInfoEnabled = false` compiles, is silently ignored, and logging continues. Confirmed by running the built library:

```text
logger.IsInfoEnabled = false;
logger.Info("should be suppressed");
-> IsInfoEnabled still reads True, and the message is written
```

There was no other way to disable a level on this type. `ILogMethods.IsEnabled` is get-only, and the setter on `LogMethods.IsEnabled` is private, so the value can only be supplied at construction. The `LogMethods` instances created by the `(ILogMethods log, string name)` constructor and by `CreateFromILogLikeObject` are always enabled.

This affected `LoggerWrapper` and everything derived from it, which is `TestLogger` and `LoggerWrapperCommonLog`. No test covered it: the `IsXxxEnabled` assignments already in the suite were all against `NoOpLogger` and `EventLogLogger`, both of which inherit `LoggerBase`'s fields for the getter as well and therefore behaved correctly.

Fixed: all six properties in `LoggerWrapper` now override both accessors over a nullable backing field. While a level has never been assigned the getter still reads the underlying `ILogMethods.IsEnabled`, so the previous behaviour and the first-pass trace tests are unchanged. Once assigned, the explicit value takes precedence in both directions: `false` suppresses a level whose sink is enabled, and `true` re-enables one whose sink is disabled. The inherited `LoggerBase` fields are no longer consulted by this type.

`Tests/LoggerWrapperLevelToggleTests.cs` adds 20 regression cases — each of the six levels set to `false` writes nothing to the sink and raises no exposition event, each set to `true` over a disabled sink writes and exposes all three overloads, each left unassigned still follows its `ILogMethods`, and a disable-then-re-enable case and a one-level-disabled case confirm the levels stay independent. Against the previous source 14 of the 20 fail; the 6 that pass are the unassigned cases, which never assign.

`LoggerWrapperCommonLog` and `TestLogger` inherit the corrected behaviour. The finding below is unaffected: `LoggerWrapperCommonLog` still snapshots the log4net level state at construction, so its unassigned getters keep reporting whatever log4net reported at that moment.

`ILoggerBasic` declares these members as `{ get; }` only, so the setters remain reachable through the concrete type rather than the interface. Widening the interface was left as a separate API decision.

### Open — Medium: log4net level state is captured once at construction

`LoggerWrapperCommonLog` passes `logger.IsFatalEnabled` and its siblings to the `LogMethods` constructor as values. The wrapper therefore holds whatever log4net reported at the moment it was built, and a later log4net reconfiguration is never picked up. Since the finding above was fixed the stale state can at least be corrected by assigning the `IsXxxEnabled` property, but nothing re-reads log4net on its own.

Suggested fix: hold the `ILog` and query its level properties per call rather than snapshotting them.

### Open — Low: assorted API and consistency issues

- `LoggerWithLevels.Logger` is a public settable property with no null check, although the constructor rejects a null logger. Assigning null converts a constructor-time `ArgumentNullException` into a `NullReferenceException` on the next log call.
- The `LogMethods` constructor takes an `exposeMessageMethod` parameter that is never stored or used. It is public API surface that does nothing.
- `EventLogLogger` writes a level prefix into the event text for `Fatal`, `Trace`, and `Debug`, but not for `Error`, `Warn`, and `Info`, so entries are inconsistently formatted.
- `TestLogger.LogMessages` is a static `List<LogEntry>` with a public setter. It is unbounded, is not thread-safe, and ships in the published package.
- `CreateFromILogLikeObject` throws bare `System.Exception` for an unresolvable overload, which callers cannot catch selectively, and throws `NullReferenceException` rather than `ArgumentNullException` when passed null. The `LoggerWrapper(ILogMethods, string)` constructor has the same null behaviour.

### Open — Low: the documented thread slot is empty in practice

`LogMethodsWithPrefix` builds the prefix from `Thread.CurrentThread.Name`, which is null unless the host application sets it. The prefix therefore normally renders as `2026-09-06 00:07:29.371 [] MyComponent Info: message`, not the `[thread]` shown in the first-pass write-up above and in the README. Either populate the slot from the managed thread id as a fallback, or correct both documents.

### Open — Low: the package version does not reflect the behaviour changes

`LoggerWrapper.csproj` still declares version `1.0.0.0`, but this branch changed observable behaviour: `Trace` now gates on `IsTraceEnabled`, and the generated prefix uses a different timestamp and separator. `releases/AnotherLoggerWrapper.1.0.0.nupkg` is the older build. The version needs a bump before the next publish.

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

1. ~~Move log4net off 2.0.10 to clear the `NU1902` advisory warning, once a patched version compatible with `Common.Logging.Log4Net.Universal` is selected.~~ Done: log4net is now 3.4.0. `Common.Logging.Log4Net.Universal` 1.2.0 still binds against log4net 2.0.9, so the `log4net` binding redirects in the `app.config` files were raised to 3.4.0.0; the adapter works unchanged through that redirect and all 153 tests pass.
2. ~~Make the level toggles effective on `LoggerWrapper`, with regression coverage. This is the one open finding with a user-visible correctness impact.~~ Done: both accessors are overridden per level over a nullable backing field, and `Tests/LoggerWrapperLevelToggleTests.cs` covers all six levels in both directions. The full suite is 173 tests, all passing.
3. Query the `ILog` level properties per call in `LoggerWrapperCommonLog` instead of snapshotting them at construction.
4. Clear the low-severity API and consistency items: the unchecked `LoggerWithLevels.Logger` setter, the unused `LogMethods` constructor parameter, the inconsistent `EventLogLogger` prefixes, the static `TestLogger.LogMessages` list, and the bare `Exception` and missing null guards in `CreateFromILogLikeObject`.
5. Decide whether the prefix should fall back to the managed thread id, then align the README and this document with whichever behaviour is chosen.
6. Bump the package version before the next publish.
