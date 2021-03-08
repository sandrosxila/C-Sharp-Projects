using System;
using System.Collections.Immutable;
using System.Reactive.Disposables;

namespace IObservableTPractice
{
    public class Market : IObservable<float>
    {
        private ImmutableHashSet<IObserver<float>> observers = ImmutableHashSet<IObserver<float>>.Empty;
        
        public IDisposable Subscribe(IObserver<float> observer)
        {
            observers = observers.Add(observer);
            return Disposable.Create(() =>
            {
                observers = observers.Remove(observer);
            });
        }

        public void Publish(float price)
        {
            foreach (var observer in observers)
            {
                observer.OnNext(price);
            }
        }
    }
    internal class Program
    {
        public static void Main(string[] args)
        {
            var market = new Market();
            var sub = market.Inspect("market");
            var sub2 = market.Inspect("market2");

            market.Publish(123);
            
            sub.Dispose();
            
            market.Publish(21.0f);
        }
    }
}