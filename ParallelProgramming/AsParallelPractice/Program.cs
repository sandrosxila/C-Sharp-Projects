using System;
using System.Linq;
using System.Threading.Tasks;

namespace AsParallelPractice
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            const int count = 50;
            var items = Enumerable.Range(1, count);
            var results = new int[count];
            
            items.AsParallel().ForAll(x =>
            {
                int newValue = x * x * x;
                Console.WriteLine($"{newValue} ({Task.CurrentId})");
                results[x - 1] = newValue;
            });
            
            Console.WriteLine();
            
            foreach (var i in results)
            {
                Console.WriteLine($"{i}");
            }

            Console.WriteLine();


            var cubes = items.AsParallel().AsOrdered().Select(x => x * x * x);
            foreach (var i in cubes)
            {
                Console.WriteLine(i);
            }
        }
    }
}