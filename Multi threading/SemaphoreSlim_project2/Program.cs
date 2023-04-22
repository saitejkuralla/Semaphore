using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SemaphoreSlim_Project2
{
    internal class Program
    {
        static SemaphoreSlim semaphore = new SemaphoreSlim(5); // allow up to 5 concurrent connections
        static string connectionString = "Data Source=localhost;Initial Catalog=Company;Integrated Security=True";


        private static async Task<int> GetRowCountAsync()
        {
            await semaphore.WaitAsync(); // wait until there's a free connection
            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand("SELECT COUNT(*) FROM TableName", connection))
                {
                    await connection.OpenAsync();
                    return (int)await command.ExecuteScalarAsync();
                }
            }
            finally
            {
                semaphore.Release(); // release the connection
            }
        }

        static async Task Main(string[] args)
        {
            Console.WriteLine($"Main Thread Started");

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            Task<int>[] tasks = new Task<int>[10];
            for (int i = 0; i < 10; i++)
            {
                tasks[i] = GetRowCountAsync();
            }

            await Task.WhenAll(tasks);


            for (int i = 0; i < tasks.Length; i++)
            {
                Console.WriteLine($"Task {i + 1} Result: {tasks[i].Result}");
            }

            Console.WriteLine($"Main Thread Completed");
            stopwatch.Stop();
            Console.WriteLine($"Processing of queries done in {stopwatch.ElapsedMilliseconds / 1000.0} Seconds");

            Console.ReadKey();
        }
    }
}
