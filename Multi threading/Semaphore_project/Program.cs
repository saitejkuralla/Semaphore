using System;
using System.Threading;

namespace Semaphore_project
{
    class Program
    {
        // create a Semaphore object with an initial count of 2 and a maximum count of 3
        // to allow a maximum of 3 customers to access the ATMs at a time
        static Semaphore semaphore = new Semaphore(2, 3);

        static void Customer(int id)
        {
            try
            {
                // call WaitOne() method to acquire the semaphore
                // if the semaphore count is 0, the current thread will block until it can acquire the semaphore
                semaphore.WaitOne();

                Console.WriteLine("Customer {0} is using the ATM", id);
                Thread.Sleep(2000); // Simulate the customer using the ATM
                Console.WriteLine("Customer {0} is done using the ATM", id);
            }
            finally
            {
                // call Release() method to release the semaphore
                // this will increase the semaphore count by 1
                semaphore.Release();
            }
        }

        static void Main(string[] args)
        {
            Thread[] threads = new Thread[10];
            for (int i = 1; i <= 10; i++)
            {
                threads[i - 1] = new Thread(() => Customer(i - 1)); // create a new thread for each customer
                threads[i - 1].Start(); // start the thread
            }
            foreach (Thread t in threads)
            {
                t.Join(); // wait for all threads to complete
            }

            Console.WriteLine("All customers have finished using the ATMs");
            Console.ReadKey(); // wait for user input before exiting
        }
    }
}
