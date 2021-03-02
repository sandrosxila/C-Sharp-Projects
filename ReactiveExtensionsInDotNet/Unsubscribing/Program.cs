using System;
using System.Reactive.Subjects;

namespace Unsubscribing
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var sensor = new Subject<float>();
            using (sensor.Subscribe(Console.WriteLine))
            {
                sensor.OnNext(1);
                sensor.OnNext(2);
                sensor.OnNext(3);
            }
            sensor.OnNext(4);
        }
    }
}