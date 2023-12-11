using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;

[Generator]
public partial class Generator : ISourceGenerator
{
    public void Execute(GeneratorExecutionContext context)
    {
        var sw = Stopwatch.StartNew();

        while (sw.ElapsedMilliseconds < 5000)
        {
            var file = context.AdditionalFiles.FirstOrDefault();
            var path = file.Path;
            var text = File.ReadAllText(path);
            Parallel.For(0, 1000000, _ => { });
        }
    }

    public void Initialize(GeneratorInitializationContext context)
    {
    }
}