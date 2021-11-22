using BenchmarkDotNet.Running;

namespace Physics2D.Benchmarks
{
    internal class Program
    {
        public static void Main()
        {
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run();
        }
    }
}