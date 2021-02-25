using System;
using System.Collections.Generic;
using System.Linq;

namespace DataPartitioning
{
    internal class Program
    {
        public static void PrintEnumerable<T>(IEnumerable<T> container)
        {
            foreach (var item in container)
            {
                Console.Write(item);
                Console.Write(" ");
            }

            Console.WriteLine();
        }
        public static void Main(string[] args)
        {
            var numbers = new[] {3, 3, 2, 2, 1, 1, 2, 2, 3, 3};
            PrintEnumerable(numbers.Skip(2).Take(6));
            PrintEnumerable(numbers.SkipWhile(i => i > 1));
            PrintEnumerable(numbers.TakeWhile(i => i > 1));
        }
    }
}