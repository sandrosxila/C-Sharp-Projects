using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reactive.Threading.Tasks;

namespace SequenceRelatedTimeProcessingPractice
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
            Observable.Range(1, 100).Buffer(5, 3)
                .Subscribe(x => Console.WriteLine(string.Join(",", x)));
            
            var source = Observable.Interval(TimeSpan.FromSeconds(1)).Take(3);
            var delay = source.Delay(TimeSpan.FromSeconds(2));
            source.Timestamp().Inspect("source");
            delay.Timestamp().Inspect("delay");
            
            Console.ReadLine();
            
            var samples = Observable.Interval(TimeSpan.FromSeconds(0.5))
                .Take(20).Sample(TimeSpan.FromSeconds(1.75));
            samples.Inspect("Sample");
            samples.ToTask().Wait();


            var subj = new Subject<string>();
            subj
                .Throttle(TimeSpan.FromSeconds(1))
                // .Timeout(TimeSpan.FromSeconds(3),Observable.Empty<string>())
                .Inspect("Subj");
            
            string input = string.Empty;
            ConsoleKeyInfo c;
            while ((c=Console.ReadKey()).Key != ConsoleKey.Escape)
            {
                if (char.IsLetterOrDigit(c.KeyChar))
                {
                    input += c.KeyChar;
                    subj.OnNext(input);
                }
            }
            
            
        }
    }
}