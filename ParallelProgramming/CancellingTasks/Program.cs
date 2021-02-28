using System;
using System.Threading;
using System.Threading.Tasks;

namespace CancellingTasks
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;
            token.Register(() => { Console.WriteLine("Cancellation has been requested."); });

            var t = new Task(() =>
            {
                int i = 0;
                while (true)
                {
                    // less canonical way
                    // if (token.IsCancellationRequested)
                    //     break;

                    // more canonical way
                    // if (token.IsCancellationRequested)
                    //     throw new OperationCanceledException();

                    // shortcut way
                    token.ThrowIfCancellationRequested();
                    Console.WriteLine($"{i++}\t");
                }
            }, token);
            t.Start();

            Task.Factory.StartNew(() =>
            {
                token.WaitHandle.WaitOne();
                Console.WriteLine("Wait Handle has Released, Cancellation was Requested.");
            });

            Console.ReadKey();
            cts.Cancel();

            Console.WriteLine("Main Program is Done.");
            Console.ReadKey();
        }
    }
}