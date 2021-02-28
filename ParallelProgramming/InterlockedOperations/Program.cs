using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace InterlockedOperations
{
    internal class Program
    {
        public class BankAccount
        {
            public object padlock = new object();
            private int balance;

            public int Balance
            {
                get => balance;
                private set => balance = value;
            }

            public void Deposit(int amount)
            {
                Interlocked.Add(ref balance, amount);
            }

            public void Withdraw(int amount)
            {
                Interlocked.Add(ref balance, -amount);
            }
        }
        public static void Main(string[] args)
        {
            var tasks = new List<Task>();
            var ba = new BankAccount();
            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 100; j++)
                    {
                        ba.Deposit(100);
                    }
                }));
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 100; j++)
                    {
                        ba.Withdraw(100);
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());
            Console.WriteLine($"Final Balance is {ba.Balance}.");
        }
    }
}