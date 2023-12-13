using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Newtonsoft.Json.Schema;

[Generator]
public class Generator : ISourceGenerator
{
    public void Execute(GeneratorExecutionContext context)
    {
        //try
        {
            M();
        }
        //catch { }
        G();
        context.AddSource("example.cs", SourceText.From(text: """
            class J { }
            """, encoding: Encoding.UTF8));
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    void M()
    {
        Newtonsoft.Json.Schema.SchemaExtensions.Validate(null, null, null);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    void G()
    {
        var assembly = typeof(SchemaExtensions).Assembly;
        File.AppendAllLines(@"analyzer.log", new string[] { assembly.Location });
    }

    public void Initialize(GeneratorInitializationContext context)
    {
        Debugger.Launch();
    }
}
