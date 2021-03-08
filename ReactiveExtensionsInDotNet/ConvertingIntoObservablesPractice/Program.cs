using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;

namespace ConvertingIntoObservablesPractice
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

    public class Market
    {
        private float price;

        public float Price { get; set; }

        public void ChangePrice(float price)
        {
            Price = price;
            PriceChanged?.Invoke(this, price);
        }

        public event EventHandler<float> PriceChanged;
    }

    internal class Program
    {
        public static void Main(string[] args)
        {
            // var start = Observable.Start(() =>
            // {
            //     Console.WriteLine("Starting Work....");
            //     for (int i = 0; i < 10; i++)
            //     {
            //         Thread.Sleep(200);
            //         Console.Write(".");
            //     }
            // });
            //
            // for (int i = 0; i < 10; i++)
            // {
            //     Thread.Sleep(200);
            //     Console.Write("-");
            // }

            var market = new Market();

            var priceChanges = Observable.FromEventPattern<float>(
                handler => market.PriceChanged += handler,
                handler => market.PriceChanged -= handler
            );

            priceChanges.Subscribe(
                x=>Console.WriteLine($"{x.EventArgs}")
                );

            market.ChangePrice(1);
            market.ChangePrice(2);
            market.ChangePrice(3);


            var t = Task.Factory.StartNew(() => "Test");
            var src = t.ToObservable();
            src.Inspect("Task");
            Console.ReadKey();

            var item = new List<int> {1, 2, 3};
            var source = item.ToObservable();
            source.Inspect("Observable");
        }
    }
}