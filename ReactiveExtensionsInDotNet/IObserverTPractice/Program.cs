using System;

namespace IObserverTPractice
{

    public class Market : IObservable<float>
    {
        public IDisposable Subscribe(IObserver<float> observer)
        {
            throw new NotImplementedException();
        }
    }
    
    internal class Program : IObserver<float>
    {
        public Program()
        {
            var market = new Market();
            market.Subscribe(this);
        }
        public static void Main(string[] args)
        {
            
        }

        public void OnNext(float value)
        {
            Console.WriteLine($"Market gave us {value}");
        }

        public void OnError(Exception error)
        {
            Console.WriteLine($"Something went wrong");
        }

        public void OnCompleted()
        {
            Console.WriteLine($"Everything Completed");
        }
    }
}