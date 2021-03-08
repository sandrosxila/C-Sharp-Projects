using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace SequenceAggregationPractice
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
            var values = Observable.Range(1, 5);
            values.Inspect("Values");
            values.Count().Inspect("Count");
            values.Average().Inspect("Average");
            values.Max().Inspect("Max");
            values.Min().Inspect("Min");
            values.Sum().Inspect("Sum");

            var replay = new ReplaySubject<int>();

            replay.OnNext(-1,2);
            replay.OnCompleted();

            replay.FirstAsync(i => i > 0).Inspect("First Async");
            
            var replay2 = new ReplaySubject<int>();

            replay2.OnNext(-1);
            replay2.OnCompleted();

            replay2.FirstOrDefaultAsync(i => i > 0).Inspect("First Or Default Async");
            
            var replay3 = new ReplaySubject<int>();

            replay3.OnNext(-1);
            replay3.OnCompleted();

            replay3.SingleAsync().Inspect("Single Async");
            
            var replay4 = new ReplaySubject<int>();

            replay4.OnNext(-1,2);
            replay4.OnCompleted();

            replay4.SingleOrDefaultAsync().Inspect("Single Or Default Async");

            var subj = new Subject<double>();
            int power = 1;

            subj.Aggregate(0.0, (p, c) => p + Math.Pow(c, power++)).Inspect("Poly");
            subj.OnNext(1, 2, 4);
            subj.OnCompleted();
            
            var subj2 = new Subject<double>();

            subj2.Scan(0.0,(p,c) => p+c).Inspect("Scan");
            subj2.OnNext(1, 2, 3);
        }
    }
}