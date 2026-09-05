# LoggerWrapper

Just another logger wrapper for log4net-like loggers.

It is aimed at library developers who do not want to lock the users of their
library into a specific logging implementation or a specific version of it.
Your library depends only on `AnotherLoggerWrapper`; the application decides
what actually does the logging.

## Installation

```powershell
dotnet add package AnotherLoggerWrapper --version 1.0.0
```

## Quick start

Wrap any log4net-like logger object and use it through `ILogger`:

```csharp
using idev.LoggerWrapper;

// someLogger is any object exposing log4net-like methods, for example log4net's ILog
ILogger logger = LoggerWrapper.CreateFromILogLikeObject(someLogger);

logger.Info("Service started");
logger.Warn("Queue is filling up");
logger.Error("Import failed", exception);
```

## Compatibility

| Project | Package | Target framework | Notes |
| --- | --- | --- | --- |
| `LoggerWrapper` | `AnotherLoggerWrapper` | .NET Standard 2.0 | Core interfaces and wrappers; works on .NET Framework and .NET Core / .NET 5+ |
| `LoggerWrapperCommonLog` | not published | .NET Framework 4.7.2 | Common.Logging and log4net integration |
| `LoggerWrapperWindows` | not published | .NET Framework 4.7.2 | Windows Event Log integration; Windows only |

Only the core project is published to NuGet. The two add-on projects are part
of this repository and target .NET Framework 4.7.2.

## Core types

The base class is `LoggerBase`. `LoggerWrapper` is a concrete implementation
of it, and `ILogger` is the interface both are used through.

| Type | Purpose |
| --- | --- |
| `ILoggerBasic` | Level checks and the plain `Trace`/`Debug`/`Info`/`Warn`/`Error`/`Fatal` methods |
| `ILogger` | `ILoggerBasic` plus the culture-aware `*Format` overloads |
| `ILoggerWithMessagesExpositionWithLevels` | The `LogMessageHandler` event, raised for every message that passes the level check |
| `LoggerBase` | Abstract base implementing `ILogger` and the exposition event, with the shared level-check logic |
| `LoggerWrapper` | Concrete logger that adapts an existing logger object or a set of delegates |
| `LoggerWithLevels` | Adds a single `Log(LogLevel, message)` entry point with a threshold |
| `NoOpLogger` | Logger that discards everything; useful as a default |
| `LogManager` | Global factory that hands out loggers of a configured type |

## Adapting an existing logger

`LoggerWrapper.CreateFromILogLikeObject(object)` inspects the given object with
reflection and builds delegates from its methods. The object must expose all
six level names — `Fatal`, `Error`, `Warn`, `Trace`, `Debug` and `Info` — and
for each of them these three public instance overloads:

- `(string)` or `(object)`
- `(Exception)`, or `(object)` if no `Exception` overload exists
- `(string, Exception)` or `(object, Exception)`

Each overload must resolve unambiguously: if a level name has no matching
overload, or more than one candidate matches, `CreateFromILogLikeObject`
throws an `Exception` naming the level and the parameter list it could not
resolve. log4net's `ILog` satisfies these requirements.

If you do not have such an object, build the wrapper from delegates instead:

```csharp
var logMethods = new LogMethods(
    message => Console.WriteLine(message),
    exception => Console.WriteLine(exception.Message),
    (message, exception) => Console.WriteLine(message + " " + exception.Message));

// Every message gets a "<timestamp> [<thread>] <name> <Level>: " prefix
ILogger logger = new LoggerWrapper(logMethods, "MyComponent");
```

## LoggerWithLevels

`LoggerWithLevels` wraps any `ILoggerBasic` and dispatches through a single
method, dropping anything below the configured threshold:

```csharp
var logger = new LoggerWithLevels(someLogger, LogLevel.Info);

logger.Log(LogLevel.Debug, "Not written, Debug is below Info");
logger.Log(LogLevel.Error, "Written");

logger.LogLevel = LogLevel.Trace; // the threshold can be changed at runtime
```

Levels are ordered `Trace < Debug < Info < Warn < Error < Fatal`.

## NoOpLogger

`NoOpLogger` implements the full interface and does nothing. Use it as the
default in a library so callers are never forced to configure logging:

```csharp
ILogger logger = NoOpLogger.Instance;
```

## LogManager

`LogManager` is a small global factory. It hands out `NoOpLogger` instances
until a logger type is registered:

```csharp
LogManager.SetLoggerType(typeof(MyLogger)); // must implement ILogger

ILogger logger = LogManager.GetLogger(typeof(MyService));
ILoggerWithLevels levelLogger = LogManager.GetLoggerWithLevels("MyService", LogLevel.Debug);
```

`SetLoggerType` throws `ArgumentException` if the given type does not implement
`ILogger`. The type must also expose a public constructor taking a `Type` or a
`string`, because `GetLogger` instantiates it through `Activator.CreateInstance`.

## Building and testing

The repository needs the .NET SDK (10.0 was used for the current build) and,
for the .NET Framework 4.7.2 projects, the .NET Framework 4.7.2 developer pack
or targeting pack. All projects use SDK-style project files and
`PackageReference`, so a clean clone needs nothing but a restore:

```powershell
dotnet restore .\LoggerWrapper.sln
dotnet build .\LoggerWrapper.sln --configuration Release
dotnet test .\LoggerWrapper.sln --configuration Release
```

Building the solution in Release also produces the `AnotherLoggerWrapper`
NuGet package under `LoggerWrapper\bin\Release`.

The .NET Framework test projects only build and run on Windows.

## Tests

The tests are written with [NUnit] and double as usage examples:

| Project | Covers |
| --- | --- |
| `Tests` (`TestsBase`) | Core library |
| `TestsCommonLog` | Common.Logging and log4net integration |
| `TestsWindows` | Windows Event Log integration |

## Contributing

Issues and pull requests are welcome at the [project page]. Please make sure
`dotnet build` and `dotnet test` both succeed in Release before opening a pull
request, and add a test for any behaviour change.

## Alternatives

Probably a better solution is [LibLog]; you can also look at
[NLog.Interface].

## License

LoggerWrapper is licensed under the MIT License. See [LICENSE](LICENSE) for the
full text.

[LibLog]: <https://github.com/damianh/LibLog>
[NUnit]: <https://nunit.org/>
[NLog.Interface]: <https://github.com/uhaciogullari/NLog.Interface>
[project page]: <https://github.com/gasparovicm/LoggerWrapper>
