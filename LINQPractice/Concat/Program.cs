using System;
using System.Collections.Generic;
using System.Linq;

namespace Concat
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
            var integralTypes = new[] {typeof(int), typeof(short)};
            var floatingPointTypes = new[] {typeof(float), typeof(double)};
            PrintEnumerable(integralTypes.Concat(floatingPointTypes));
            PrintEnumerable(integralTypes.Prepend(typeof(bool)));
        }
    }

    static class ExtenstionMethods
    {
        public static IEnumerable<T> Prepend<T>(this IEnumerable<T> values, T value)
        {
            return new[] {value}.Concat(values);
        }
    }
}