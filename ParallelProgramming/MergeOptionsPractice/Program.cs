using System;
using System.Linq;

namespace MergeOptionsPractice
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var numbers = Enumerable.Range(1, 20).ToArray();
            Console.WriteLine("Not Buffered");
            var results = numbers.AsParallel().WithMergeOptions(ParallelMergeOptions.NotBuffered).
                Select(x =>
            {
                var result = Math.Log10(x);
                Console.WriteLine($"Product {result}");
                return result;
            });

            foreach (var result in results)
            {
                Console.WriteLine($"Consume {result}");
            }
            Console.WriteLine("Fully Buffered");

            var results2 = numbers.AsParallel().WithMergeOptions(ParallelMergeOptions.FullyBuffered).
                Select(x =>
            {
                var result = Math.Log10(x);
                Console.WriteLine($"Product {result}");
                return result;
            });

            foreach (var result in results2)
            {
                Console.WriteLine($"Consume {result}");
            }
        }
    }
}