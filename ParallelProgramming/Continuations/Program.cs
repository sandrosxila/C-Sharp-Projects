using System;
using System.Threading.Tasks;

namespace Continuations
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            // var task = Task.Factory.StartNew(() =>
            // {
            //     Console.WriteLine($"Boiling Water");
            // });
            // var task2 = task.ContinueWith(t =>
            // {
            //     Console.WriteLine($"Completed task {t.Id}, Pour Water into the cup.");
            //     
            // });
            //
            // task2.Wait();

            var task = Task.Factory.StartNew(() => "Task 1");
            var task2 = Task.Factory.StartNew(() => "Task 2");
            var task3 = Task.Factory.ContinueWhenAll(new[] {task, task2}, (tasks) =>
            {
                Console.WriteLine($"Tasks are being Completed:");
                foreach (var t in tasks)
                {
                    Console.WriteLine($" - {t.Result}");
                }
            
                Console.WriteLine($"All Tasks Done!!");
            });

            // var task3 = Task.Factory.ContinueWhenAny(new[] {task, task2}, (t) =>
            // {
            //     Console.WriteLine($"Tasks are being Completed:");
            //     Console.WriteLine($" - {t.Result}");
            //
            //     Console.WriteLine($"All Tasks Done!!");
            // });
            
            task3.Wait();
            
        }
    }
}