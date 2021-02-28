using System;
using System.Collections.Concurrent;
using System.Linq;

namespace ConcurrentStackPractice
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var stack = new ConcurrentStack<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Push(4);

            int result;
            if (stack.TryPeek(out result))
            {
                Console.WriteLine($"{result} is on Top");
            }

            if (stack.TryPop(out result))
            {
                Console.WriteLine($"{result} is Popped");
            }

            var items = new int[5];
            if (stack.TryPopRange(items,0,5) > 0)
            {
                var text = string.Join(", ", items.Select(i => i.ToString()));
                Console.WriteLine($"Popped These Elements {text}");
            }
        }
    }
}