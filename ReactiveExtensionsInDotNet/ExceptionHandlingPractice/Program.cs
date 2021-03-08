using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace ExceptionHandlingPractice
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
            var subj = new Subject<int>();
            var fallback = Observable.Range(1, 3);

            subj.Catch(fallback).Inspect("Subj");
            subj.OnNext(32);
            subj.OnError(new Exception("Oops!!!"));


            var subj2 = new Subject<int>();
            subj2.Catch<int, ArgumentException>(
                ex => Observable.Return(111)
            ).Catch(Observable.Empty<int>()).Inspect("Subj2");
            subj2.OnNext(32);
            subj2.OnError(new ArgumentException("Arg"));
            subj2.OnError(new Exception("Oops!!!"));

            var seq1 = new Subject<char>();
            var seq2 = new Subject<char>();

            seq1.OnErrorResumeNext(seq2).Inspect("OnErrorResumeNext");

            seq1.OnNext('a', 'b', 'c').OnError(new Exception());

            seq2.OnNext('d', 'e', 'f');


            SucceedAfter(3).Retry(4).Inspect("SucceedAfter");
        }

        private static IObservable<int> SucceedAfter(int attempts)
        {
            int count = 0;
            return Observable.Create<int>(o =>
            {
                Console.WriteLine((count > 0 ? "Ret" : "T") + "rying to do work");
                if (count++ < attempts)
                {
                    Console.WriteLine("Failed");
                    o.OnError(new Exception());
                }
                else
                {
                    Console.WriteLine("Succeeded");
                    o.OnNext(count);
                    o.OnCompleted();
                }
                return Disposable.Empty;
            });
        }
    }
}