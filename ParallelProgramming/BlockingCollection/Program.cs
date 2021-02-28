using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace BlockingCollection
{
    internal class Program
    {
        private static BlockingCollection<int> messages =
            new BlockingCollection<int>(new ConcurrentBag<int>(), 10);

        private static CancellationTokenSource cts = new CancellationTokenSource();

        private static Random random = new Random();

        public static void Main(string[] args)
        {
            Task.Factory.StartNew(ProduceAndConsume, cts.Token);

            Console.ReadKey();
            cts.Cancel();
        }

        static void ProduceAndConsume()
        {
            var producer = Task.Factory.StartNew(RunProducer);
            var consumer = Task.Factory.StartNew(RunConsumer);

            try
            {
                Task.WaitAll(new[] {producer, consumer}, cts.Token);
            }
            catch (AggregateException ae)
            {
                ae.Handle(e => true);
            }
        }

        private static void RunConsumer()
        {
            foreach (var item in messages.GetConsumingEnumerable())
            {
                cts.Token.ThrowIfCancellationRequested();
                Console.WriteLine($"-{item}\t");
                Thread.Sleep(random.Next(1000));
            }
        }

        private static void RunProducer()
        {
            while (true)
            {
                cts.Token.ThrowIfCancellationRequested();
                int i = random.Next(100);
                messages.Add(i);
                Console.WriteLine($"+ int {i}");
                Thread.Sleep(random.Next(100));
            }
        }
    }
}