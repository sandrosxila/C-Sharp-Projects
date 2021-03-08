using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace SequenceFilteringPractice
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
            var values = Observable.Range(-10, 21);
            values.Select(x => x * x).Distinct().Inspect("Select Distinct");


            new List<int>() {1, 1, 2, 2, 3, 3, 2, 2}.ToObservable().DistinctUntilChanged().Inspect("Dit");

            Observable.Range(1, 10).Skip(3).Take(4).Inspect("Skip/Take");

            values.SkipWhile(x => x < 0).TakeWhile(x => x < 6).Inspect("While");

            values.SkipLast(5).Inspect("SkipLast");

            var stockPrices = new Subject<float>();
            var optionPrices = new Subject<float>();

            stockPrices.SkipUntil(optionPrices).Inspect("SkipUntil");
            
            stockPrices.OnNext(1, 2, 3);
            optionPrices.OnNext(55);
            stockPrices.OnNext(4, 5, 6);
        }
    }
}