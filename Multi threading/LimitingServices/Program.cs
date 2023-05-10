using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace LimitingServices
{
    class Program
    {
        // Create a SemaphoreSlim object with a maximum count of 2.
        // This means that only 2 tasks can enter the semaphore at any given time.
        private static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(2, 2);

        static async Task Main(string[] args)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            List<Task> tasks = new List<Task>();

            // Add tasks to the list. Each task waits for the semaphore to be available,
            // runs a service, and then releases the semaphore.
            tasks.Add(Task.Run(async () =>
            {
                await semaphoreSlim.WaitAsync(); // Wait for semaphore to be available
                try
                {
                    await Service1();
                }
                finally
                {
                    semaphoreSlim.Release(1); // Release semaphore
                }
            }));

            tasks.Add(Task.Run(async () =>
            {
                await semaphoreSlim.WaitAsync();
                try
                {
                    await Service2();
                }
                finally
                {
                    semaphoreSlim.Release(1);
                }
            }));

            tasks.Add(Task.Run(async () =>
            {
                await semaphoreSlim.WaitAsync();
                try
                {
                    await Service3();
                }
                finally
                {
                    semaphoreSlim.Release(1);
                }
            }));

            tasks.Add(Task.Run(async () =>
            {
                await semaphoreSlim.WaitAsync();
                try
                {
                    await Service4();
                }
                finally
                {
                    semaphoreSlim.Release(1);
                }
            }));

            tasks.Add(Task.Run(async () =>
            {
                await semaphoreSlim.WaitAsync();
                try
                {
                    await Service5();
                }
                finally
                {
                    semaphoreSlim.Release(1);
                }
            }));

            tasks.Add(Task.Run(async () =>
            {
                await semaphoreSlim.WaitAsync();
                try
                {
                    await Service6();
                }
                finally
                {
                    semaphoreSlim.Release(1);
                }
            }));

            await Task.WhenAll(tasks);

            stopwatch.Stop();
            Console.WriteLine($"Time taken: {stopwatch.ElapsedMilliseconds / 1000.0} seconds");

            Console.ReadLine();
        }
        static async Task Service1()
        {
            await Task.Delay(1000);
            Console.WriteLine("Processed Service 1");
        }
        static async Task Service2()
        {
            await Task.Delay(1000);
            Console.WriteLine("Processed Service 2");
        }
        static async Task Service3()
        {
            await Task.Delay(1000);
            Console.WriteLine("Processed Service 3");
        }
        static async Task Service4()
        {
            await Task.Delay(1000);
            Console.WriteLine("Processed Service 4");
        }
        static async Task Service5()
        {
            await Task.Delay(1000);
            Console.WriteLine("Processed Service 5");
        }
        static async Task Service6()
        {
            await Task.Delay(1000);
            Console.WriteLine("Processed Service 6");
        }
    }
}
