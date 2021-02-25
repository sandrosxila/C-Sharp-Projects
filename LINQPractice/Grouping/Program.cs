using System;
using System.Collections.Generic;
using System.Linq;

namespace Grouping
{
    class Person
    {
        public string Name;
        public int Age;
    }

    internal class Program
    {
        public static void Main(string[] args)
        {
            var people = new List<Person>()
            {
                new Person {Name = "Adam", Age = 36},
                new Person {Name = "Boris", Age = 18},
                new Person {Name = "Charlie", Age = 36},
                new Person {Name = "Adam", Age = 20},
                new Person {Name = "Jack", Age = 20},
            };

            var byName = people.GroupBy(p => p.Name);
            foreach (var person in byName)
            {
                Console.WriteLine($"{person.Select(p => p.Name).FirstOrDefault()} " +
                                  $"{person.Average(p => p.Age)}");
            }

            Console.WriteLine();
            var byAge = people.GroupBy(p => p.Age < 30);
            foreach (var group in byAge)
            {
                Console.WriteLine(group.Key);
                group.ToList().ForEach(member => Console.WriteLine($"{member.Name} {member.Age}"));
            }

            var byAgeNames = people.GroupBy(p => p.Age, p => p.Name);
            Console.WriteLine();
            foreach (var group in byAgeNames)
            {
                Console.WriteLine(group.Key);
                group.ToList().ForEach(name => Console.WriteLine($"{name} "));
            }

        }
    }
}