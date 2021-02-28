using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LockRecursion
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
        private static SpinLock sl = new SpinLock(true);
        public static void LockRecursion(int x)
        {
            bool lockTaken = false;
            try
            {
                sl.Enter(ref lockTaken);
            }
            catch(LockRecursionException e)
            {
                Console.WriteLine($"Exception: {e}");
            }
            finally
            {
                if(lockTaken)
                {
                    Console.WriteLine($"Took a lock, x ={x}");
                    LockRecursion(x - 1);
                    sl.Exit();
                }
                else
                {
                    Console.WriteLine($"Failed to take a lock, x = {x}");
                }
            }
        }
        public static void Main(string[] args)
        {
            LockRecursion(5);
        }
    }
}