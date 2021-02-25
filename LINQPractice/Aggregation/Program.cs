using System;
using System.Linq;

namespace Aggregation
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var numbers = Enumerable.Range(1, 10);
            Console.WriteLine("Sum = " + 
                              numbers.Aggregate((partialResult, element) => partialResult + element));
            Console.WriteLine("Sum = " + numbers.Sum());
            Console.WriteLine("Product = " + 
                              numbers.Aggregate(1, (partialResult, element) => partialResult * element));
            Console.WriteLine("Product = " + numbers.Average());
            
            Console.WriteLine(new string[] {"one,","two,","three"}.Aggregate(
                "Hello", (partialResult, element) => partialResult + element));
        }
    }
}