LoggerWrapper
Just another logger wrapper for Log4Net like loggers.

It specifically for library developers, when you would like not to lock an user of your library to use some specific version of log4net.
Actualy is available only as source code.
Nuget will come ...

Base class is LogggerWrapper , with his interface ILogger, and his method LoggerWrapper.CreateFromILogLikeObject(object). 
Given object will be "examined" and then apropriate delegates will be created.
There is also LoggerWithLevels class which will add simplier version of log4net log methods.
Another usefull class is NoOpLogger which is just emty logger class with no real logging.
Base project is for .Net Standard 2.0 so you can use it for "classic" .Net and also for .Net Core.

LogggerWrapper project contains just one class EventLogLogger which is just sending log messages into Windows event log.
There is also LoggerWrapperCommonLog for Common.Logging library.

Lot of tests based on [NUnit] you can use as examples how to use LoggerWrapper and also NUnit possibilities.

Probably better solution is [LibLog], and you can also visit [NLog.Interface].

LoggerWrapper is licensed under MIT Licence.

[LibLog]: <https://github.com/damianh/LibLog>
[NUnit]: <https://nunit.org/>
[NLog.Interface]: <https://github.com/uhaciogullari/NLog.Interface>

