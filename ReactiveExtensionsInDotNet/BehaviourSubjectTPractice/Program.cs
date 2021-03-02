using System.Reactive.Subjects;

namespace BehaviourSubjectTPractice
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var sensorReader = new BehaviorSubject<double>(-1.0);
            sensorReader.Inspect("Sensor");
            sensorReader.OnNext(0.99);
            sensorReader.OnCompleted();
        }
    }
}