using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Validators;

namespace PartitioningPractice
{
    public class Program
    {
        [Benchmark]
        public void SquareEachValue()
        {
            const int count = 100000;
            var values = Enumerable.Range(0, count);
            var results = new int[count];
            Parallel.ForEach(values, x => { results[x] = (int) Math.Pow(x, 2); });
        }

        [Benchmark]
        public void SquareEachValueChunk()
        {
            const int count = 100000;
            var values = Enumerable.Range(0, count);
            var results = new int[count];

            var part = Partitioner.Create(0, count, 10000);
            Parallel.ForEach(part, range =>
            {
                for (int i = range.Item1; i < range.Item2; i++)
                {
                    results[i] = (int) Math.Pow(i, 2);
                }
            });
        }
        
        public static void Main(string[] args)
        {
            var config = new ManualConfig()
                    .WithOptions(ConfigOptions.DisableOptimizationsValidator)
                    .AddValidator(JitOptimizationsValidator.DontFailOnError)
                    .AddLogger(ConsoleLogger.Default)
                    .AddColumnProvider(DefaultColumnProviders.Instance);
            var summary = BenchmarkRunner.Run<Program>(config);
            Console.WriteLine(summary);
        }
    }
}