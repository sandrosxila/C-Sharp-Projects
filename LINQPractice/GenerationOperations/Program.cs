using System;
using System.Collections.Generic;
using System.Linq;

namespace GenerationOperations
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
            PrintEnumerable(Enumerable.Empty<int>());
            PrintEnumerable(Enumerable.Repeat("Hello",3));
            PrintEnumerable(Enumerable.Range(1,10));
            PrintEnumerable(Enumerable.Range('a','z'-'a' + 1).Select(c => (char) c));
            PrintEnumerable(Enumerable.Range(1,10).Select(i => new string('x',i)));
        }
    }
}