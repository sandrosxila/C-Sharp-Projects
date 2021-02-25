using System;
using System.Collections.Generic;
using System.Linq;

namespace Sorting
{

    class Person
    {
        public string Name;
        public int Age;
    }
    
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
            var random = new Random();
            var randomValues = Enumerable.Range(1, 10)
                .Select(_ => random.Next(10) - 5).ToArray();
            var csvString = new Func<IEnumerable<int>, string>(values =>
            {
                return string.Join(",", values.Select(v => v.ToString()).ToArray());
            });
            Console.WriteLine(csvString(randomValues));
            Console.WriteLine(csvString(randomValues.OrderBy(x => x)));
            Console.WriteLine(csvString(randomValues.OrderByDescending(x => x)));

            var people = new List<Person>()
            {
                new Person {Name = "Adam", Age = 36},
                new Person {Name = "Boris", Age = 18},
                new Person {Name = "Charlie", Age = 36},
                new Person {Name = "Adam", Age = 20},
                new Person {Name = "Jack", Age = 20},
            };

            foreach (var person in people.OrderBy(p => p.Age).ThenByDescending(p => p.Name))
            {
                Console.WriteLine($"{person.Name} {person.Age}");
            }

            string s = "This is a test";

            Console.WriteLine(new string(s.Reverse().ToArray()));
        }
    }
}