using System;
using System.Reactive.Linq;

namespace SequenceGeneratorsPractice
{
    static class ExtensionMethods
    {
        public static IDisposable Inspect<T>(this IObservable<T> self, string name)
        {
            return self.Subscribe(
                x => Console.WriteLine($"{name} has generated value {x}"),
                ex => Console.WriteLine($"{name} has generated exception: {ex.Message}"),
                () => Console.WriteLine($"{name} has completed")
            );
        }

        public static IObserver<T> OnNext<T>(this IObserver<T> self, params T[] args)
        {
            foreach (var arg in args)
            {
                self.OnNext(arg);
            }

            return self;
        }
    }
    
    internal class Program
    {
        public static void Main(string[] args)
        {
            var timer = Observable.Timer(TimeSpan.FromSeconds(2));
            timer.Inspect("Timer");
            Console.ReadKey();
            
            var tenToTwenty = Observable.Range(10, 11);
            tenToTwenty.Inspect("Range");

            var generated = Observable.Generate(
                1,
                value => value < 100,
                value => value * value + 1,
                value => $"[{value}]"
            );
            generated.Inspect("Generated");

            var interval = Observable.Interval(TimeSpan.FromMilliseconds(500));
            using (interval.Inspect("Interval"))
            {
                Console.ReadKey();
            }
        }
    }
}