REM passing Newtonsoft.Json.dll 13.0.0.0 works fine and results in the expected:
REM arning CS8785: Generator 'Generator' failed to generate source. It will not contribute to the output and compilation errors may occur as a result. Exception was of type 'ArgumentNullException' with message 'Value cannot be null.
REM Parameter name: source'
REM 
REM Loaded modules:
REM C:\temp\CscAnal\analyzer\bin\Debug\netstandard2.0\analyzer.dll
REM C:\temp\CscAnal\analyzer\bin\Debug\netstandard2.0\Newtonsoft.Json.dll  (13.0.0.0)
REM C:\temp\CscAnal\analyzer\bin\Debug\netstandard2.0\Newtonsoft.Json.Schema.dll

csc 1.cs /t:Library -analyzer:analyzer\bin\Debug\netstandard2.0\analyzer.dll -analyzer:NS.J-13\Newtonsoft.Json.dll

REM passing Newtonsoft.Json.dll 12.0.0.0 fails with:
REM System.MissingMethodException: Method not found: 'Void Newtonsoft.Json.Schema.SchemaExtensions.Validate(Newtonsoft.Json.Linq.JToken, Newtonsoft.Json.Schema.JSchema, Newtonsoft.Json.Schema.SchemaValidationEventHandler)'.
REM 
REM Loaded modules:
REM C:\temp\CscAnal\analyzer\bin\Debug\netstandard2.0\analyzer.dll
REM C:\temp\CscAnal\analyzer\bin\Debug\netstandard2.0\Newtonsoft.Json.dll  (13.0.0.0)
REM C:\temp\CscAnal\analyzer\bin\Debug\netstandard2.0\Newtonsoft.Json.Schema.dll
REM C:\temp\CscAnal\NS.J-12\Newtonsoft.Json.dll

csc 1.cs /t:Library -analyzer:analyzer\bin\Debug\netstandard2.0\analyzer.dll -analyzer:NS.J-12\Newtonsoft.Json.dll

REM not passing Newtonsoft.Json.dll fails with:
REM System.IO.FileNotFoundException: Could not load file or assembly 'Newtonsoft.Json, Version=12.0.0.0, Culture=neutral, PublicKeyToken=30ad4fe6b2a6aeed' or one of its dependencies. The system cannot find the file specified.
REM 
REM Loaded modules:
REM C:\temp\CscAnal\analyzer\bin\Debug\netstandard2.0\analyzer.dll
REM C:\temp\CscAnal\analyzer\bin\Debug\netstandard2.0\Newtonsoft.Json.dll  (13.0.0.0)
REM C:\temp\CscAnal\analyzer\bin\Debug\netstandard2.0\Newtonsoft.Json.Schema.dll

csc 1.cs /t:Library -analyzer:analyzer\bin\Debug\netstandard2.0\analyzer.dll