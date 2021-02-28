using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace ConcurrentDictionaryPractice
{
    internal class Program
    {
        private static ConcurrentDictionary<string, string> capitals =
            new ConcurrentDictionary<string, string>();

        public static void AddParis()
        {
            bool success = capitals.TryAdd("France", "Paris");
            string who = Task.CurrentId.HasValue ? ($"Task {Task.CurrentId}") : "Main Thread";
            Console.WriteLine($"{who} {(success ? "added" : "did not add")} the element.");
        }
        public static void Main(string[] args)
        {
            Task.Factory.StartNew(AddParis).Wait();
            AddParis();

            capitals["India"] = "Delhi";
            capitals.AddOrUpdate("India","New Delhi",
                (k, old) => $"{old} --> New Delhi");
            Console.WriteLine($"The Capital of India is {capitals["India"]}");

            // capitals["Sweden"] = "Uppsala";
            var capOfSweden = capitals.GetOrAdd("Sweden", "Stockholm");
            Console.WriteLine($"Capital of Sweden is {capOfSweden}");

            const string toRemove = "India";
            string removed;
            var didRemove = capitals.TryRemove(toRemove, out removed);
            if (didRemove)
            {
                Console.WriteLine($"We Just Removed {removed}");
            }
            else
            {
                Console.WriteLine($"We Failed To remove capital of {toRemove}");
            }

            foreach (var capital in capitals)
            {
                Console.WriteLine($"- {capital.Value} is the capital of {capital.Key}");
            }
        }
    }
}