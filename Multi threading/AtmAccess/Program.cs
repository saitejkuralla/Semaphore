using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Semaphore_project
{
    class Program
    {
        static Semaphore semaphore = new Semaphore(3, 3); // maximum of 3 customers can access the ATMs at a time
        static Stopwatch stopwatch = new Stopwatch(); // create a stopwatch to measure elapsed time

        static void Customer(int id)
        {
            semaphore.WaitOne(); // acquire the semaphore

            Console.WriteLine("Customer {0} is using the ATM", id);
            Thread.Sleep(1000); // simulate the customer using the ATM
            Console.WriteLine("Customer {0} is done using the ATM", id);

            semaphore.Release(); // release the semaphore
        }

        static void Main(string[] args)
        {
            stopwatch.Start(); // start the stopwatch

            Task[] tasks = new Task[10];
            for (int i = 0; i < 10; i++)
            {
                int id = i;
                tasks[i] = Task.Run(() => Customer(id));
            }

            Task.WaitAll(tasks); // wait for all tasks to complete

            stopwatch.Stop(); // stop the stopwatch
            Console.WriteLine("All customers have finished using the ATMs");
            Console.WriteLine("Elapsed time: {0} sec", stopwatch.ElapsedMilliseconds / 1000.0);
            Console.ReadKey(); // wait for user input before exiting
        }
    }
}
