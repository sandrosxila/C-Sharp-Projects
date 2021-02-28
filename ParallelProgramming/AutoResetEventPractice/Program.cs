using System;
using System.Threading;
using System.Threading.Tasks;

namespace AutoResetEventPractice
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var evt = new AutoResetEvent(false);

            Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"Boiling water");
                evt.Set();
            });

            var makeTea = Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"Waiting for water...");
                evt.WaitOne();
                Console.WriteLine("Here is your tea.");
                var ok = evt.WaitOne(1000);
                if (ok)
                {
                    Console.WriteLine("Enjoy your tea.");
                }
                else
                {
                    Console.WriteLine("No tea for you");
                }
            });

            makeTea.Wait();
        }
    }
}