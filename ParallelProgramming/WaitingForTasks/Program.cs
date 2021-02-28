using System;
using System.Threading;
using System.Threading.Tasks;

namespace WaitingForTasks
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            
            var cts = new CancellationTokenSource();
            var token = cts.Token;
            var t = new Task(() =>
            {
                Console.WriteLine("I Take 5 seconds");
                for (int i = 0; i < 5; i++)
                {
                    token.ThrowIfCancellationRequested();
                    Thread.Sleep(1000);
                }

                Console.WriteLine("I'm Done");
            },token);
            t.Start();

            var t2 = Task.Factory.StartNew(() =>
            {
                Thread.Sleep(3000);
                Console.WriteLine("Waiting t to end.");
                t.Wait(token);
                Console.WriteLine($"t2 is done!");
            }, token);
            
            Task.WaitAny(t,t2);
            Console.WriteLine("Main Program is Done.");
            Console.ReadKey();
        }
    }
}