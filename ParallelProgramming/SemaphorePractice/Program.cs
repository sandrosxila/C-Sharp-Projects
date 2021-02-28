﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace SemaphorePractice
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var semaphore = new SemaphoreSlim(2, 10);
            for (int i = 0; i < 20; i++)
            {
                Task.Factory.StartNew(() =>
                {
                    Console.WriteLine($"Entering task {Task.CurrentId}");
                    semaphore.Wait(); // ReleaseCount--
                    Console.WriteLine($"Processing task {Task.CurrentId}");
                });
            }

            while (semaphore.CurrentCount <= 2)
            {
                Console.WriteLine($"Semaphore Count {semaphore.CurrentCount}");
                Console.ReadKey();
                semaphore.Release(2); //ReleaseCount += 2
            }
        }
    }
}