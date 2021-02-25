using System;
using System.Linq;

namespace ElementOperations
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var numbers = new[] {1, 2, 3};
            Console.WriteLine(numbers.First());
            Console.WriteLine(numbers.First(x => x > 2));
            Console.WriteLine(numbers.FirstOrDefault(x => x > 10));
            Console.WriteLine(new int[] {1}.Single());
            Console.WriteLine(new int[] {}.SingleOrDefault());
            Console.WriteLine($"Item at position 1 equals to: {numbers.ElementAt(1)}");
            Console.WriteLine($"Item at position 4 equals to: {numbers.ElementAtOrDefault(4)}");
        }
    }
}