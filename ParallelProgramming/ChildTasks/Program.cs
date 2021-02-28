using System;
using System.Threading;
using System.Threading.Tasks;

namespace ChildTasks
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var parent = new Task(() =>
            {
                // detached
                var child = new Task(() =>
                {
                    Console.WriteLine($"Child Task is starting ");
                    Thread.Sleep(3000);
                    Console.WriteLine($"Child Task is finishing ");
                    throw new Exception();
                }, TaskCreationOptions.AttachedToParent);

                var completionHandler = child.ContinueWith(
                    t => { Console.WriteLine($"Hooray, task {t.Id}'s state is {t.Status}!!!"); },
                    TaskContinuationOptions.AttachedToParent |
                    TaskContinuationOptions.OnlyOnRanToCompletion);

                var failHandler = child.ContinueWith(
                    t => { Console.WriteLine($"Oops, task {t.Id}'s state is {t.Status}!!!"); },
                    TaskContinuationOptions.AttachedToParent |
                    TaskContinuationOptions.OnlyOnFaulted);
                child.Start();
            });
            parent.Start();
            try
            {
                parent.Wait();
            }
            catch (AggregateException ae)
            {
                ae.Handle(e => true);
            }
        }
    }
}