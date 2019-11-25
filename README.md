# Mendz.Library
Provides helper and utility classes, types and extensions. [Wiki](https://github.com/etmendz/Mendz.Library/wiki)
## Namespaces
### Mendz.Library
#### Contents
Name | Description
---- | -----------
CommandProcess | Provides methods to start a command process (usually, a window-less process).
ShellProcess | Provides methods to start a shell process (usually, a windowed process).
IDGenerator | Represents an ID generator.
SingletonBase | A base class to define a singleton.
IGenericMapper | Defines a mapper.
IStreamingMapper | Defines a streaming mapper.
MapperExtensions | IGenericMapper and IStreamingMapper extensions.
StreamingGenericMapperBase | The base class of a streaming mapper that also implements its generic mapper.
GenericStreamingMapper | Represents a streaming mapper that uses a provided generic mapper.
IAsyncGenericMapper | Defines an asynchronous mapper.
IAsyncStreamingMapper | Defines an asynchronous streaming mapper.
AsyncMapperExtensions | IAsyncGenericMapper and IAsyncStreamingMapper extensions.
AsyncStreamingGenericMapperBase | The base class of an asynchronous streaming mapper that also implements its asynchronous generic mapper.
AsyncGenericStreamingMapper | Represents an asynchronous streaming mapper that uses a provided asynchronous generic mapper.
#### CommandProcess
Use the CommandProcess to start window-less commands and programs.
The parameter should be an executable program.
Note that, if possible, the CommandProcess waits for the launched process to exit.
CommandProcess.Start() returns the exit code returned by the launched program.
```C#
using Mendz.Library;
...
    private StringBuilder _output = new StringBuilder();
    private StringBuilder _error = new StringBuilder();
    ...
        int exitCode = CommandProcess.Start(new ProcessStartInfo()
        {
            FileName = "cmd.exe",
            Arguments = "/c dir",
            RedirectStandardOutput = true,
            RedirectStandardError = true
        }, Process_Output, Process_Error, Process_Exited);
    ...
    private void Process_Output(object sender, DataReceivedEventArgs e)
    {
        _output.Append(e.Data);
    }

    private void Process_Error(object sender, DataReceivedEventArgs e)
    {
        _error.Append(e.Data);
    }

    private void Process_Exited(object sender, EventArgs e)
    {
        Console.WriteLine(_output.ToString());
        Console.WriteLine(_error.ToString());
    }
...
```
The event handlers allow for output and error data to be handled.
The exited handler allows for final evaluation of the result.
For example, if the program launched completes successfully but creates an error log file,
it can be evaluated to affect the exit code, say by throwing an exception.
#### ShellProcess
Use the ShellProcess to start windowed commands and programs.
For executable programs passed as parameter, the ShellProcess launches them.
For non-executable parameters, the ShellProcess depends on the platform's shell to launch the appropriate program.
Note that, if possible, the ShellProcess waits for the launched process to exit.
```C#
using Mendz.Library;
...
    ...
        ShellProcess.Start(new ProcessStartInfo() { FileName = "notepad.exe" });
        ShellProcess.Start(new ProcessStartInfo() { FileName = "https://www.bing.com" });
        ShellProcess.Start(new ProcessStartInfo() { FileName = @"C:\New Microsoft Word Document.docx" }, Process_Exited);
    ...
    private void Process_Exited(object sender, EventArgs e)
    {
        ...
    }
...
```
The exited handler allows for final evaluation of the result.
#### IDGenerator
Use the IDGenerator to create incremental Int32 IDs from a seed value (default is 1).
```C#
using Mendz.Library;
...
    IDGenerator idGenerator = new IDGenerator();
    int id = idGenerator.ID; // 1
    id = idGenerator.Generate(); // 2
    idGenerator.Seed(10);
    id = idGenerator.ID; // 10
    id = idGenerator.Generate(); // 11
    idGenerator.Seed(5); // does nothing, because the seed 5 is less than the current ID 11
    id = idGenerator.ID; // 11
    id = idGenerator.Generate(); // 12
    idGenerator = new IDGenerator(100);
    id = idGenerator.ID; // 100
    id = idGenerator.Generate(); // 101
...
```
IDGenerator is thread-safe.
#### SingletonBase
Use SingletonBase to define a class as a singleton.
The derived class must have a private constructor that accepts no parameter.
```C#
using Mendz.Library;

namespace Test
{
    public class TestSingleton : SingletonBase<TestSingleton>
    {
        private DateTime _created;

        public string Info
        {
            get => Instance.ToString() + " as of " + _created.ToString();
        }

        private TestSingleton() => _created = DateTime.Now;
    }
}
```
Sample use:
```C#
...
    Console.WriteLine(TestSingleton.Instance.Info);
...
```
In an application domain/runtime, the first call to the Instance property instantiates the singleton.
The instantiation of the singleton based on SingletonBase is thread-safe.
Depending on the implementation, singletons derived from SingletonBase may not be thread-safe.
#### Mappers
Use the mappers to define data and type conversion/mapping operations. Derive from the StreamingGenericMapperBase class to implement a generic mapper that can also be used for streaming operations. If you've created IGenericMapper implementations instead, they can be injected to the GenericStreamingMapper for streaming operations.
#### Asynchronous Mappers
Use the asynchronous mappers to define asynchronous data and type conversion/mapping operations. Derive from the AsyncStreamingGenericMapperBase class to implement an asynchronous  generic mapper that can also be used for asynchronous streaming operations. If you've created IAsyncGenericMapper implementations instead, they can be injected to the AsyncGenericStreamingMapper for asynchronous streaming operations.
### Mendz.Library.Extensions
#### Contents
Name | Description
---- | -----------
StringExtensions| String class extensions.
- IsMatch() tests if a string matches a regular expression.
- ReplaceMatch() replaces each part of a string that matches a regular expression.
### Mendz.Library.Extensions.IO
#### Contents
Name | Description
---- | -----------
TextReaderExtensions| TextReader class extensions.
- ReadLineMatch() reads a line that matches a regular expression.
- ReadAllMatch() reads all lines that match a regular expression.
- YieldLine() yields lines read. 
- YieldLineMatch() yields lines read that match a regular expression. 
- ReadLineMatchAsync() asynchronously reads a line that matches a regular expression.
- ReadAllMatchAsync() asynchronously reads all lines that match a regular expression.
- YieldLineAsync() asynchronously yields lines read. 
- YieldLineMatchAsync() asynchronously yields lines read that match a regular expression. 
## NuGet It...
https://www.nuget.org/packages/Mendz.Library/
