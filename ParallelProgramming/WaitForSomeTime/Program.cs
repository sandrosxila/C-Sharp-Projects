using System;
using System.Threading;
using System.Threading.Tasks;

namespace WaitForSomeTime
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;
            var t = new Task(() =>
            {
                Console.WriteLine("Press Any Key to Disarm; You Have 5 seconds");
                bool cancelled = token.WaitHandle.WaitOne(5000);
                Console.WriteLine(cancelled ? "Bomb disarmed" : "BOOM!!!");
            },token);
            t.Start();
            Console.ReadKey();
            cts.Cancel();
            
            Console.WriteLine("Main Program is Done.");
            Console.ReadKey();
        }
    }
}