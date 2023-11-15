using System.IO;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Newtonsoft.Json.Schema;

[Generator]
public class Generator : ISourceGenerator
{
    public void Execute(GeneratorExecutionContext context)
    {
        var assembly = typeof(SchemaExtensions).Assembly;
        SchemaExtensions.Validate(null, null, null);
        File.AppendAllLines(@"analyzer.log", new string[] { assembly.Location });

        context.AddSource("example.cs", SourceText.From(text: """
            class J { }
            """, encoding: Encoding.UTF8));
    }

    public void Initialize(GeneratorInitializationContext context)
    {
    }
}
