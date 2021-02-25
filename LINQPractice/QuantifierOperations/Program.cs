using System;
using System.Linq;

namespace QuantifierOperations
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            int[] numbers = {1, 2, 3, 4, 5};
            Console.WriteLine("Are all numbers > 0 ?\n" + numbers.All(x => x > 0));
            Console.WriteLine("Are all numbers odd ?\n" + numbers.All(x => x %2==1));
            Console.WriteLine("Any number < 2 ?\n" + numbers.Any(x => x < 2));
            Console.WriteLine("Contains 5 ?\n" + numbers.Contains(5));
            
        }
    }
}