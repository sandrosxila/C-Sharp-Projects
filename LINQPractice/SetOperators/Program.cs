using System;
using System.Linq;

namespace SetOperators
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string word1 = "helloooo";
            string word2 = "help";

            word1.Distinct().ToList().ForEach(c => Console.Write(c));
            Console.WriteLine();
            word1.Intersect(word2).ToList().ForEach(c => Console.Write(c));
            Console.WriteLine();
            word1.Union(word2).ToList().ForEach(c => Console.Write(c));
            Console.WriteLine();
            word1.Except(word2).ToList().ForEach(c => Console.Write(c));
            Console.WriteLine();
        }
    }
}