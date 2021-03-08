using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Timers;
using Timer = System.Timers.Timer;

namespace ObservableCreatePractice
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
        private static IObservable<string> Blocking()
        {
            var subj = new ReplaySubject<string>();
            subj.OnNext("Foo", "Bar");
            subj.OnCompleted();
            Thread.Sleep(3000);
            return subj;
        }

        private static IObservable<string> NonBlocking()
        {
            return Observable.Create<string>(observer =>
            {
                observer.OnNext("Foo", "Bar");
                observer.OnCompleted();
                Console.WriteLine("Sleep Started");
                Thread.Sleep(3000);
                Console.WriteLine("Sleep ended");
                return Disposable.Empty;
            });
        }
        
        public static void Main(string[] args)
        {
            // Blocking().Inspect("Blocking");
            // NonBlocking().Inspect("NonBlocking");

            var obs = Observable.Create<string>(o =>
            {
                var timer = new Timer(1000);
                timer.Elapsed += (sender, e) => o.OnNext($"tick {e.SignalTime}");
                timer.Elapsed += TimerOnElapsed;
                timer.Start();
                return () =>
                {
                    timer.Elapsed -= TimerOnElapsed;
                    timer.Dispose();
                };
            });

            var sub = obs.Inspect("timer");
            Console.ReadLine();
            sub.Dispose();
            Console.ReadLine();
        }

        private static void TimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine($"tok {e.SignalTime}");
        }
    }
}