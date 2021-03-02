using System.Reactive.Subjects;

namespace ProxyAndBroadcast
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var market = new Subject<float>();
            var marketConsumer = new Subject<float>();

            market.Subscribe(marketConsumer);

            marketConsumer.Inspect("Market Consumer");

            market.OnNext(1,2,3,4);
            market.OnCompleted();
        }
    }
}