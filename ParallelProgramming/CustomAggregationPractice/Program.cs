using System;
using System.Linq;

namespace CustomAggregationPractice
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var sum = ParallelEnumerable.Range(1, 1000).
                Aggregate(
                    0,
                    (partialSum, i) => partialSum + i,
                    (total, subTotal) => total+=subTotal,
                    i => i
                    );

            Console.WriteLine($"sum(1...1000) = {sum}");
        }
    }
}