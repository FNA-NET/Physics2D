using BenchmarkDotNet.Attributes;
using Physics2D.Benchmarks.Code;
using Physics2D.Benchmarks.Code.TestClasses;

namespace Physics2D.Benchmarks.Tests.CLR
{
    public class FieldPropertyBenchmarks : UnmeasuredBenchmark
    {
        private readonly Dummy _dummy = new Dummy { ValueField = new Struct32() };

        [Benchmark]
        public Struct32 PropertyGetTest()
        {
            return _dummy.ValueProperty;
        }

        [Benchmark]
        public Struct32 FieldGetTest()
        {
            return _dummy.ValueField;
        }

        [Benchmark]
        public Struct32 MethodGetTest()
        {
            return _dummy.ValueMethod();
        }
    }
}
