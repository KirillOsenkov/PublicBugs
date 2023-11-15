using System.Diagnostics;
using System;
using Microsoft.CodeAnalysis;

[Generator]
public class Generator : ISourceGenerator
{
    public void Execute(GeneratorExecutionContext context)
    {
        Newtonsoft.Json.Schema.SchemaExtensions.Validate(null, null, null);
    }

    public void Initialize(GeneratorInitializationContext context)
    {
    }
}
