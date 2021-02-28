using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ReaderWriterLock
{
    internal class Program
    {
        static ReaderWriterLockSlim padLock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
        private static Random random = new Random();

        public static void Main(string[] args)
        {
            int x = 0;
            var tasks = new List<Task>();
            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew((object idx) =>
                {
                    // padLock.EnterReadLock();
                    padLock.EnterUpgradeableReadLock();

                    if ((int)idx % 2 == 0)
                    {
                        padLock.EnterWriteLock();
                        x = 100 * ((int)idx+1);
                        padLock.ExitWriteLock();
                    }

                    Console.WriteLine($"Entered Read Lock, x = {x}");
                    Thread.Sleep(5000);

                    padLock.ExitUpgradeableReadLock();
                    // padLock.ExitReadLock();
                    Console.WriteLine($"Exited Read Lock, x = {x}");
                },i));
            }

            try
            {
                Task.WaitAll(tasks.ToArray());
            }
            catch (AggregateException ae)
            {
                ae.Handle(e =>
                {
                    Console.WriteLine(e);
                    return true;
                });
            }

            while (true)
            {
                Console.ReadKey();
                padLock.EnterWriteLock();
                Console.WriteLine($"Write Lock Acquired.");
                int newValue = random.Next(10);
                x = newValue;
                Console.WriteLine($"Set x={x}");
                padLock.ExitWriteLock();
                Console.WriteLine($"Write Lock Released, x={x}");
            }
        }
    }
}