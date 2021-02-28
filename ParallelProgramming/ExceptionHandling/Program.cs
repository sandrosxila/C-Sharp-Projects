using System;
using System.Threading.Tasks;

namespace ExceptionHandling
{
    internal class Program
    {
        public static void Test()
        {
            var t1 = Task.Factory.StartNew(() =>
            {
                throw new InvalidOperationException("Can't Do This!!!") {Source = "t1"};
            });
            var t2 = Task.Factory.StartNew(() =>
            {
                throw new AccessViolationException("Can't Access This!!!") {Source = "t2"};
            });
            try
            {
                Task.WaitAll(t1, t2);
            }
            catch (AggregateException ae)
            {
                ae.Handle((exception =>
                {
                    if (exception is InvalidOperationException)
                    {
                        Console.WriteLine("Invalid Op!!!");
                        return true;
                    }

                    return false;
                }));
            }
        }

        public static void Main(string[] args)
        {
            try
            {
                Test();
            }
            catch (AggregateException ae)
            {
                foreach (var e in ae.InnerExceptions)
                {
                    Console.WriteLine($"Exception {e.GetType()} from {e.Source}");
                }
            }

            Console.WriteLine("Main Program is Done.");
            Console.ReadKey();
        }
    }
}