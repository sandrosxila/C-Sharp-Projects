using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace SequenceInspectionPractice
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
            var subject = new Subject<int>();

            subject.Any(x => x > 1).Inspect("Any");
            subject.OnNext(2);
            subject.OnCompleted();

            var values = new List<int>(){1,2,3,4,5};
            values.ToObservable().All(x => x > 0).Inspect("All");

            var subj = new Subject<string>();
            subj.Contains("foo").Inspect("Contains");
            subj.OnNext("foo");

            var subj1 = new Subject<float>();
            subj1.DefaultIfEmpty(0.99f).Inspect("Default if Empty");
            subj1.OnCompleted();

            var numbers = Observable.Range(1, 10);
            numbers.ElementAt(15).Inspect("ElementAt");


            var seq1 = new Subject<int>();
            var seq2 = new Subject<int>();

            seq1.Inspect("seq1");
            seq2.Inspect("seq2");

            seq1.SequenceEqual(seq2).Inspect("SequenceEqual");

            seq1.OnNext(1);
            seq2.OnNext(1);
            
            seq1.OnCompleted();
            seq2.OnCompleted();
        }
    }
}