using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SpinLocking
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
            Balance += amount;
        }

        public void Withdraw(int amount)
        {
            Balance -= amount;
        }
    }

    internal class Program
    {
        public static void Main(string[] args)
        {
            var tasks = new List<Task>();
            var ba = new BankAccount();

            var sl = new SpinLock();

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
                        var lockTaken = false;
                        try
                        {
                            sl.Enter(ref lockTaken);
                            ba.Withdraw(100);
                        }
                        finally
                        {
                            if (lockTaken)
                                sl.Exit();
                        }
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());
            Console.WriteLine($"Final Balance is {ba.Balance}.");
        }
    }
}