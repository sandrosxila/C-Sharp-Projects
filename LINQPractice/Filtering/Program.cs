using System;
using System.Collections.Generic;
using System.Linq;

namespace Filtering
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
            var numbers = Enumerable.Range(1, 10);
            var evenNumbers = numbers.Where(n => n % 2 == 0);
            PrintEnumerable(evenNumbers);

            var oddsSquares = numbers.Select(x => x * x).Where(y => y % 2 == 1);
            PrintEnumerable(oddsSquares);

            object[] values = {1, 2.5, 3, 4.56};
            PrintEnumerable(values.OfType<double>());
            
            
        }
    }
}