using System.Reactive.Subjects;

namespace AsyncSubjectTPractice
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var sensor = new AsyncSubject<double>();
            sensor.Inspect("Async");
            sensor.OnNext(1.0);
            sensor.OnNext(2.0);
            sensor.OnNext(3.0);
            sensor.OnCompleted();

            sensor.OnNext(123);
        }
    }
}