using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ConvertingDataTypes
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

        public static void PrintDictionary<T, U>(IDictionary<T, U> dictionary)
        {
            foreach (KeyValuePair<T, U> item in dictionary)
            {
                Console.WriteLine($"{item.Key} {item.Value}");
            }

            Console.WriteLine();
        }

        public static void Main(string[] args)
        {
            var list = new ArrayList();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            Console.WriteLine(list.Cast<int>().Average());


            var numbers = Enumerable.Range(1, 10);

            var arr = numbers.ToArray();
            PrintEnumerable(arr);
            
            var dict = numbers.ToDictionary(i => (double) i / 10, i => i % 2 == 0);
            PrintDictionary(dict);

            var arr2 = new[] {1, 2, 3};
            var arrE = arr2.AsEnumerable();
            PrintEnumerable(arrE);
        }
    }
}