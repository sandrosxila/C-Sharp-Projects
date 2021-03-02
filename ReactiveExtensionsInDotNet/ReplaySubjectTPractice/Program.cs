using System;
using System.Reactive.Subjects;
using System.Threading;
using Console = System.Console;

namespace ReplaySubjectTPractice
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var timeWindow = TimeSpan.FromMilliseconds(500);
            var market = new ReplaySubject<float>(timeWindow);
            market.OnNext(123);
            Thread.Sleep(200);
            market.OnNext(456);
            Thread.Sleep(200);
            market.OnNext(789);
            Thread.Sleep(200);
            market.Subscribe(x => Console.WriteLine($"Got the price {x}"));
        }
    }
}