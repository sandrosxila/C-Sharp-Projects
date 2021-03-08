using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace SequenceTransformationPractice
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
            var numbers = Observable.Range(1, 10);
            numbers.Select(x => x * x).Inspect("Select");

            var subj = new Subject<object>();

            subj.OfType<float>().Inspect("OfType");
            subj.Cast<float>().Inspect("Cast");

            subj.OnNext(1.0f, 2, 3.30);

            var seq = Observable.Interval(TimeSpan.FromSeconds(1));
            // seq.Timestamp().Inspect("TimeSpan");
            seq.TimeInterval().Inspect("TimeInterval");
            Console.ReadLine();

            var sequence = Observable.Range(0, 4);
            sequence.Materialize().Dematerialize().Inspect("Materialize");

            Observable.Range(1, 4)
                .SelectMany(x => Observable.Range(1, x))
                .Inspect("SelectMany");

            Observable.Range(1, 4, Scheduler.Immediate)
                .SelectMany(x => Observable.Range(1, x, Scheduler.Immediate))
                .Inspect("SelectManyImmediate");
        }
    }
}