using System;
using System.Threading;
using System.Threading.Tasks;

namespace CountdownEventPractice
{
    internal class Program
    {
        private static int taskCount = 5;
        private static CountdownEvent cte = new CountdownEvent(taskCount);
        private static Random random = new Random();
        public static void Main(string[] args)
        {
            for (int i = 0; i < taskCount; i++)
            {
                Task.Factory.StartNew(() =>
                {
                    Console.WriteLine($"Entering task {Task.CurrentId}");
                    Thread.Sleep(random.Next(3000));
                    Console.WriteLine($"Exiting task {Task.CurrentId}");
                    cte.Signal();
                });
            }

            var finalTask = Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"Waiting other tasks to complete in {Task.CurrentId}");
                cte.Wait();
                Console.WriteLine($"All tasks are completed");
            });
            finalTask.Wait();
        }
    }
}