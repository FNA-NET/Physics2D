using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using Physics2D.Benchmarks.Code;
using Physics2D.Benchmarks.Code.TestClasses;
using Physics2D.Shared;

namespace Physics2D.Benchmarks.Tests.Shared
{
    public class PoolBenchmarks : UnmeasuredBenchmark
    {
        private readonly Pool<PoolObject> _pool;

        public PoolBenchmarks()
        {
            _pool = new Pool<PoolObject>(() => new PoolObject(), o => o.Reset(), 1000);
        }

        [Benchmark]
        public void NewObject()
        {
            for (int i = 0; i < 1000; i++)
            {
                PoolObject obj = new PoolObject();
                obj.TestInteger = 5;
                obj.TestString = "test";
            }
        }

        [Benchmark]
        public void NewPooledObject()
        {
            for (int i = 0; i < 1000; i++)
            {
                PoolObject obj = _pool.GetFromPool();
                obj.TestInteger = 5;
                obj.TestString = "test";

                _pool.ReturnToPool(obj);
            }
        }

        [Benchmark]
        public void NewPooledObjectMany()
        {
            IEnumerable<PoolObject> many = _pool.GetManyFromPool(1000);
            foreach (PoolObject obj in many)
            {
                obj.TestInteger = 5;
                obj.TestString = "test";
                _pool.ReturnToPool(obj);
            }
        }
    }
}