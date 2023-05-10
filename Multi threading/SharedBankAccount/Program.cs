using System;
using System.Threading;

namespace SharedBankAccount
{
    public class BankAccount
    {
        private int balance;

        public BankAccount(int initialBalance)
        {
            balance = initialBalance;
        }

        public void Deposit(int amount)
        {
            Monitor.Enter(this);
            try
            {
                Console.WriteLine($"Depositing {amount}...");
                balance += amount;
                Console.WriteLine($"New balance is {balance}");
                Monitor.Pulse(this);
            }
            finally
            {
                Monitor.Exit(this);
            }
        }

        public void Withdraw(int amount)
        {
            Monitor.Enter(this);
            try
            {
                while (balance < amount)
                {
                    Console.WriteLine($"Withdrawal of {amount} blocked, waiting for deposit...");
                    Monitor.Wait(this);
                }
                Console.WriteLine($"Withdrawing {amount}...");
                balance -= amount;
                Console.WriteLine($"New balance is {balance}");
            }
            finally
            {
                Monitor.Exit(this);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int balance = 100;
            var account = new BankAccount(balance);
            Console.WriteLine($"Current available balance is:{balance}");
            Thread t1 = new Thread(() =>
            {
                account.Withdraw(50);
                Console.WriteLine("Withdrawal complete");
            });
            Thread t2 = new Thread(() =>
            {
                account.Deposit(100);
                Console.WriteLine("Deposit complete");
            });
            t1.Start();
            t2.Start();
            Console.ReadLine();
        }
    }
}
