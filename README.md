# LoggerWrapper
Just another logger wrapper for Log4Net like loggers.

It specifically for library developers, when you would like not to lock a user of your library to use some specific version of log4net.

# Base
Base class is LoggerWrapper, with his interface ILogger, and his method LoggerWrapper.CreateFromILogLikeObject(object).
Given object will be "examined" and then appropriate delegates will be created.
There is also LoggerWithLevels class which will add simpler version of log4net log methods.
Another useful class is NoOpLogger which is just empty logger class with no real logging.
Base project is for .Net Standard 2.0 so you can use it for "classic" .Net and also for .Net Core.
You can install it by nuget package [AnotherLoggerWrapper].

# Other projects
LogggerWrapperWindows project contains just one class EventLogLogger which is just sending log messages into Windows event log.
There is also LoggerWrapperCommonLog project for Common.Logging library.

# Tests
Lot of tests based on [NUnit] you can use as examples how to use LoggerWrapper and also NUnit possibilities.

Probably better solution is [LibLog], and you can also visit [NLog.Interface].

LoggerWrapper is licensed under MIT License.

[LibLog]: <https://github.com/damianh/LibLog>
[NUnit]: <https://nunit.org/>
[NLog.Interface]: <https://github.com/uhaciogullari/NLog.Interface>
[AnotherLoggerWrapper]: <https://www.nuget.org/packages/AnotherLoggerWrapper>

