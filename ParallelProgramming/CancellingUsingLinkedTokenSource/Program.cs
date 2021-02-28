using System;
using System.Threading;
using System.Threading.Tasks;

namespace CancellingUsingLinkedTokenSource
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var planned = new CancellationTokenSource();
            var preventative = new CancellationTokenSource();
            var emergency = new CancellationTokenSource();

            var paranoid = CancellationTokenSource.CreateLinkedTokenSource(
                planned.Token, preventative.Token, emergency.Token
            );

            Task.Factory.StartNew(() =>
            {
                var i = 0;
                while (true)
                {
                    paranoid.Token.ThrowIfCancellationRequested();
                    Console.WriteLine($"{++i}\t");
                    Thread.Sleep(1000);
                }
            }, paranoid.Token);

            Console.ReadKey();
            emergency.Cancel();
            
            Console.WriteLine("Main Program is Done.");
            Console.ReadKey();
        }
    }
}