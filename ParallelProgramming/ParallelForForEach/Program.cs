using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParallelForForEach
{
    internal class Program
    {

        public static IEnumerable<int> Range(int start, int end, int step)
        {
            for (int i = start; i < end; i+=step)
            {
                yield return i;
            }
        }
        
        public static void Main(string[] args)
        {
            var a = new Action(() => Console.WriteLine($"First {Task.CurrentId}"));
            var b = new Action(() => Console.WriteLine($"Second {Task.CurrentId}"));
            var c = new Action(() => Console.WriteLine($"Third {Task.CurrentId}"));
            Parallel.Invoke(a,b,c);

            Parallel.For(1, 11, i =>
            {
                Console.WriteLine($"{i * i}\t");
            });
            Console.WriteLine();
            
            string[] words = {"oh", "what", "a", "night"};
            Parallel.ForEach(words, word =>
            {
                Console.WriteLine($"{word} has lenght {word.Length} (task {Task.CurrentId})");
            });
            Console.WriteLine();

            Parallel.ForEach(Range(1, 20, 3), i => Console.WriteLine($"value {i}"));

        }
    }
}