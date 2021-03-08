using System;
using System.Reactive.Linq;

namespace SimpleFactoryMethodsPractice
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var obs = Observable.Return(42); // ReplaySubject
            var sub = obs.Inspect("obs");
            
            var obs2 = Observable.Empty<int>();
            var sub2 = obs2.Inspect("obs2");
            
            var obs3 = Observable.Never<int>();
            var sub3 = obs3.Inspect("obs3");
            
            var obs4 = Observable.Throw<int>(new Exception("Oops!!!"));
            var sub4 = obs4.Inspect("obs4");
            
        }
    }
}