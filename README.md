# Mendz.Library
Provides helper and utility classes or types.
## Namespace
Mendz.Library
## Contents
Name | Description
---- | -----------
IDGenerator | Represents an ID generator.
SingletonBase | A base class to define a singleton.
### IDGenerator
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
### SingletonBase
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
## NuGet It...
https://www.nuget.org/packages/Mendz.Library/
