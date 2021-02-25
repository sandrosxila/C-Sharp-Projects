using System;
using System.Collections.Generic;
using System.Linq;

namespace SelectSelectMany
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
            var numbers = Enumerable.Range(1, 4);
            var squares = numbers.Select(x => x * x);
            PrintEnumerable(squares);

            string sentence = "This is a nice sentence.";
            var wordLengths = sentence.Split().Select(w => w.Length);
            PrintEnumerable(wordLengths);

            var wordsWithLength = sentence.Split().Select(w => new {Word = w, Size = w.Length});

            foreach (var wordWithLength in wordsWithLength)
            {
                Console.WriteLine($"{wordWithLength.Word} {wordWithLength.Size}");
            }

            Random random = new Random();
            var randomNumbers = Enumerable.Range(1, 10).Select(_ => random.Next(10));
            PrintEnumerable(randomNumbers);

            var sequences = new[] {"red,green,blue", "orange", "white,pink"};
            var allWords = sequences.SelectMany(s => s.Split(','));
            PrintEnumerable(allWords);

            string[] objects = {"house", "car", "bicycle"};
            string[] colors = {"red", "green", "blue"};
            var pairs = colors.SelectMany(_ => objects,
                (c,o) => $"{c} {o}\n");
            PrintEnumerable(pairs);
        }
    }
}