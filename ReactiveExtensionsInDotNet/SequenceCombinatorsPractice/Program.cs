using System;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;

namespace SequenceCombinatorsPractice
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
            var mechanical = new BehaviorSubject<bool>(true);
            var electrical = new BehaviorSubject<bool>(true);
            var electronic = new BehaviorSubject<bool>(true);

            mechanical.Inspect("Mechanical");
            electrical.Inspect("Electrical");
            electronic.Inspect("Electronic");

            Observable.CombineLatest(mechanical, electrical, electronic).Select(values => values.All(x => x))
                .Inspect("Is System OK?");

            mechanical.OnNext(false);


            var digits = Observable.Range(0, 10);
            var letters = Observable.Range(0, 10).Select(x => (char) ('A' + x));
            var punctuation = "!@#$%^&*()".ToCharArray().ToObservable();

            letters.Zip(digits, (letter, digit) => $"{letter}{digit}").Inspect("Zip");

            Observable.When(
                digits.And(letters).And(punctuation).
                    Then(((digit, letter, symbol) => $"{digit}{letter}{symbol}"))
            ).Inspect("And-Then-When");


            var s1 = Observable.Range(1, 3);
            var s2 = Observable.Range(4, 3);
            s1.Concat(s2).Inspect("Concat");
            s1.StartWith(2, 1, 0).Inspect("StartWith");


            var seq1 = new Subject<int>();
            var seq2 = new Subject<int>();
            var seq3 = new Subject<int>();

            seq1.Amb(seq2).Amb(seq3).Inspect("Amb");

            seq2.OnNext(23);
            seq1.OnNext(1, 2, 3);
            seq2.OnNext(10, 20, 30);
            seq3.OnNext(100, 200, 300);

            var foo = new Subject<long>();
            var bar = new Subject<long>();
            var baz = Observable.Interval(TimeSpan.FromSeconds(0.5)).Take(5);

            foo.Merge(bar).Merge(baz).Inspect("Merge");

            foo.OnNext(100);
            Thread.Sleep(1000);
            bar.OnNext(10);
            Thread.Sleep(1000);
            foo.OnNext(1000);
            Thread.Sleep(1000);

        }
    }
}