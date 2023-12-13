using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

[Generator]
public partial class Generator : ISourceGenerator
{
    public void Execute(GeneratorExecutionContext context)
    {
        var sw = Stopwatch.StartNew();

        var file = context.AdditionalFiles.FirstOrDefault();
        var path = file.Path;

        while (sw.ElapsedMilliseconds < 5000)
        {
            var text = File.ReadAllText(path);
            Parallel.For(0, 1000000, _ => { if (sw.ElapsedMilliseconds > 10) { var s = sw.ToString(); } });
        }

        File.WriteAllText(path, "text");
    }

    public void Initialize(GeneratorInitializationContext context)
    {
    }
}