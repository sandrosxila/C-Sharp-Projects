using System;
using System.Reactive.Subjects;

namespace SubjectTPractice
{
    internal class Program // : IObserver<float>
    {
        public Program()
        {
            var market = new Subject<float>();
            // market.Subscribe(this);
            market.Subscribe(
                (f) => Console.WriteLine($"Price is {f}"),
                () => Console.WriteLine($"Sequence is Completed")
            );
            market.OnNext(1.24f);
            market.OnError(new Exception("Oops!!!"));
            // market.OnCompleted();
        }

        public static void Main(string[] args)
        {
            new Program();
        }

        // public void OnNext(float value)
        // {
        //     Console.WriteLine($"Market gave us {value}");
        // }
        //
        // public void OnError(Exception error)
        // {
        //     Console.WriteLine($"Something went wrong: {error.Message}");
        // }
        //
        // public void OnCompleted()
        // {
        //     Console.WriteLine($"Everything Completed");
        // }
    }
}