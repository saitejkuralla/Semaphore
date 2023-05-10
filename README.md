## Thread Synchronization Methods in C#
Hey there! :wave: 

When multiple threads access shared resources simultaneously, it can lead to race conditions and unexpected behavior. To prevent this, thread synchronization is necessary. Here are some common thread synchronization methods in C#:

### Lock Statement :closed_lock_with_key:

* The `lock` statement is a simple and easy-to-use synchronization mechanism that ensures only one thread at a time can access a shared resource.
* It creates a mutual exclusion lock on the specified lockObject, ensuring that only one thread at a time can execute the code block within the lock. 
* If another thread attempts to enter the lock statement while it is already held by another thread, it will block until the lock is released.

<b>Syntax:</b>

```csharp
lock (lockObject)
{
    // Code block that accesses shared resource
}
```

:sparkles: <b>Scenario: Bus Reservation System</b> <br>
* In the Bus Reservation System, the `lock` keyword is used to protect the `AvailableTickets` variable from being accessed simultaneously by multiple threads. The lock keyword allows only one thread at a time to execute the critical section of code that accesses the AvailableTickets variable, ensuring that no two threads can access it at the same time.
* Here how it works:
  1. The program creates a BusReservation class with a private lockObject field.
  2. The BusReservation class has a BookTicket method that takes a name and the number of wanted tickets as arguments, and checks if the desired number of tickets is available. If so, it subtracts that number from the AvailableTickets field; if not, it outputs a message indicating that no tickets are available.
  3. The BusReservation class also has a TicketBooking method that gets the current thread name and calls the BookTicket method with the appropriate number of tickets based on the thread name.
  4. In the Main method, three threads are created and started, each with a different name: "User1", "User2", and "User3". Each thread calls the TicketBooking method of the BusReservation object.
  5. The lock statement is used inside the BookTicket method to ensure that only one thread at a time can access the AvailableTickets field and update it. This prevents race conditions and ensures that the ticket booking is thread-safe.


### Monitor Class :eyes:
* The Monitor class provides more fine-grained control over thread synchronization. It is similar to the lock statement, but offers additional methods for waiting and signaling.
* The Monitor class in C# has three properties:<br>
     * `Enter` method acquires an exclusive lock on an object.
     * `Exit` method releases an exclusive lock on an object.
     * `Wait` method releases an exclusive lock on an object and blocks the current thread until it is awakened by a call to the `Pulse` or `PulseAll` method.<br>
* It provides three properties to acquire and release exclusive locks on an object, and to block threads until they are signaled by another thread. This prevents race conditions and allows for more efficient and coordinated processing.

<b>Syntax:</b>
```c#
// Acquire a lock on the shared resource
Monitor.Enter(lockObject);

try {
    // Code block that accesses shared resource 

    // Wait for a signal from another thread
    Monitor.Wait(lockObject);

    // Code block that continues after receiving a signal
}
finally {
    // Release the lock on the shared resource
    Monitor.Exit(lockObject);
}

// Signal another waiting thread to continue
Monitor.Pulse(lockObject);
```
:sparkles: <b>Scenario: To synchronize access to a shared bank account</b> <br>
* Defined a BankAccount class with two methods, Deposit() and Withdraw(), to update the balance of the account. The methods use a Monitor to ensure that only one thread can access the account balance at a time.
* Here how it works:
    1. The `Deposit()` method takes an integer amount and adds it to the balance. It then sends a notification to any waiting threads by calling the `Monitor.Pulse()` method.
    2. The `Withdraw()` method takes an integer amount and subtracts it from the balance. If the balance is too low to withdraw the requested amount, the thread waits for a deposit notification by calling the `Monitor.Wait()` method.
    3. In the Main method, we create two threads: one to withdraw $50 and another to deposit $100. Both threads access the same BankAccount object, which is protected by a Monitor.
    4. When the threads are started, the `Withdraw()` thread is blocked because the balance is less than the requested amount. The `Deposit()` thread deposits $100 and sends a notification to the waiting thread, allowing the `Withdraw()` thread to complete the withdrawal and update the balance.
    5. The output shows the state of the account balance during the transaction. The program terminates after the threads complete their operations.


### Semaphore class :traffic_light:

* The Semaphore class allows a specified number of threads to access a shared resource simultaneously. It maintains a count of the number of threads currently accessing the resource, and blocks additional threads once the maximum count is reached.
* The Semaphore constructor takes an `initialCount` parameter, which specifies the initial number of threads that can access the resource, and a `maximumCount` parameter, which specifies the maximum number of threads that can access the resource. 
* The `WaitOne` method blocks the thread until the semaphore count is greater than zero, and then decrements the count. The `Release` method increments the semaphore count.

<b>Syntax:</b>
```csharp
// Creates a Semaphore object with the specified initial and maximum counts.
Semaphore semaphore = new Semaphore(initialCount, maximumCount);

// Blocks the current thread until it can enter the semaphore.
semaphore.WaitOne();

try {
    // Code block that accesses shared resource
} finally {
    // Releases the semaphore, allowing another thread to enter.
    semaphore.Release();
}
```
:sparkles: <b>Scenario: To limit number of customers  who can access the ATM machines at a time</b> <br>
* The program uses a Semaphore object to control access to the ATMs, allowing a maximum of three customers to use them at a time.
* Here how it works:
  1. The program creates ten threads, each representing a customer, and assigns them to use the ATMs.
  2. `static Semaphore semaphore = new Semaphore(3, 3);`
      <br> The Semaphore object is created with an initial count of 3 and a maximum count of 3. This means that only 3 threads can acquire the semaphore at the same time.
  3.   ```csharp
        static void Customer(int id)
         {
            semaphore.WaitOne();
            Console.WriteLine("Customer {0} is using the ATM", id);
            Thread.Sleep(1000);
            Console.WriteLine("Customer {0} is done using the ATM", id);
            semaphore.Release();
        }
       ```
     This method represents a customer thread that uses the ATM. The WaitOne() method is called to acquire a slot from the semaphore. If all three slots are already taken,      the thread will wait until one becomes available. Once the slot is acquired, the thread will simulate the customer using the ATM by sleeping for 1 second, and then          release the slot by calling the Release() method.<br>
  4. The program ensures that all customer threads finish using the ATMs before it terminates, by calling the `Join()` method on each thread. Finally, the program outputs       a message indicating that all customers have finished using the ATMs, and waits for user input before exiting.

### SemaphoreSlim class :vertical_traffic_light:

* SemaphoreSlim is a lightweight alternative to Semaphore class with less overhead
* It allows a limited number of threads to enter a critical section simultaneously
* `WaitAsync` method blocks a thread until the semaphore's count becomes greater than zero
* `Release` method increments the semaphore's count and wakes up one blocked thread
* It is useful for limiting the number of concurrent operations, such as limiting the number of database connections or HTTP requests
* It helps avoid deadlocks that can occur when multiple threads are waiting for each other to release a resource.

<b>Syntax:</b>
```csharp
// Create a new SemaphoreSlim object with an initial count and maximum count
SemaphoreSlim semaphore = new SemaphoreSlim(initialCount, maximumCount);

// Asynchronously wait for the semaphore to become available
await semaphore.WaitAsync();

try {
    // Code block that accesses shared resource
} 
finally {
    // Release the semaphore when the critical section is complete
    semaphore.Release();
}
```

:sparkles: <b>Scenario: Limiting Concurrent Credit Card Processing Tasks</b><br>
* In this example, we'll use SemaphoreSlim to limit the number of concurrent credit card processing tasks that can be executed at once. This helps prevent overloading the system and ensures that all tasks are processed efficiently.
* Here's how it works:
  1. The main thread generates a list of 15 credit cards to be processed.
  2. The `ProcessCreditCards()` method is called to process the credit cards concurrently.
  3.The program uses a SemaphoreSlim object to limit the number of concurrent tasks that can be executed. The SemaphoreSlim is initialized with a count of 3, which means       that up to three tasks can be executed concurrently. This is done to limit the number of credit cards that can be processed at the same time.
       <br>`static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(3, 3);`
  4. The ProcessCreditCards method uses `async/await` and a SemaphoreSlim object to ensure that no more than three tasks are executed concurrently. It creates a list of        tasks using the creditCards.Select() LINQ method, each of which calls the ProcessCard method and waits for it to complete. Each task acquires the semaphore before          calling ProcessCard using the `semaphoreSlim.WaitAsync()` method, and releases it afterward using the `semaphoreSlim.Release()` method.
    ```csharp
    var tasks = creditCards.Select(async card =>
    {
        await semaphoreSlim.WaitAsync();
        try
        {
            return await ProcessCard(card);
        }
        finally
        {
            semaphoreSlim.Release();
         }
     }).ToList();

     await Task.WhenAll(tasks);
    ```
  5. The `Task.WhenAll()` method is used to wait for all tasks to complete before displaying the elapsed time.
  6. The `ProcessCard()` method is a simple delay task that simulates processing a credit card by waiting for 1 second.
  7. The output displays each credit card number as it is processed and the total time taken to process all credit cards.
  <br>
:sparkles: <b>Scenario:Limiting Multiple Services</b>
* In this example, we'll use a SemaphoreSlim is used to limit the number of concurrent service calls. 
* Here's how it works:
  1. The code creates a list of six tasks, each of which represents a service that needs to be executed. Each task waits for the semaphore to be available using the             `WaitAsync` method before running its respective service. Once the service completes, the semaphore is released using the Release method, allowing other waiting tasks        to proceed.
  2. The program uses a SemaphoreSlim object to limit the number of concurrent tasks that can be executed. The SemaphoreSlim is initialized with a count of 2, which means       that up to two tasks can be executed concurrently. This is done to limit the number of credit cards that can be processed at the same time.
       `static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(2,2);`
  3.  ```csharp
      tasks.Add(Task.Run(async () =>
      {
         await semaphoreSlim.WaitAsync(); // Wait for semaphore to be available
         try
         {
             await Service();
         }
         finally
         {
             semaphoreSlim.Release(1); // Release semaphore
          }
       }));
          ```
     The `WaitAsync()` method is called on the semaphore to acquire a lock on the resource, which blocks the execution of the task until the semaphore becomes available.        Once the lock is acquired, the task runs the Service() method, which simulates the processing of a service by delaying for 1000 milliseconds and then printing a            message to the console.<br>
  iv. Finally, the `Release()` method is called on the semaphore to release the lock on the resource, allowing other tasks to acquire the semaphore and access the                 service. The finally block ensures that the semaphore is released even if an exception occurs while executing the service.
     
<hr>
:question: <b>Now we may get a doubt that when to use these methods in which scenarios?</b> :exploding_head:
<br>So here are some common scenarios where each synchronization method is commonly used :sunglasses:
<br><br> <b>Lock:</b>
    <br>* Simple and efficient synchronization for low contention scenarios.
    <br>* Used when the code block is short and the contention is low.
    <br>* Suitable for synchronization within a single process.
 <br>
<br> <b>Monitor:</b>
    <br>* Provides more control over synchronization, as well as more advanced features such as waiting and signaling.
    <br>* Suitable for scenarios where a thread needs to wait for a specific condition to be met.
    <br>* Can be used for synchronization between threads in a single process.
<br>
<br> <b>Semaphore:</b>
    <br>* Used to control access to a resource with limited capacity.
    <br>* Suitable for scenarios where there are multiple threads that need to access the resource, but the resource can only handle a limited number of concurrent access.
    <br>* Useful for scenarios where the threads need to acquire and release the resource multiple times.
<br>
<br> <b>SemaphoreSlim:</b>
    <br>* Similar to Semaphore, but more lightweight and faster.
    <br>* Suitable for scenarios with low contention where the synchronization needs to be performed frequently.
    <br>* Useful for scenarios where the synchronization needs to be performed across multiple processes, as it is a cross-process synchronization primitive.
<br><br>
:collision: It is important to choose the right synchronization technique for your specific use case based on factors such as thread safety, performance, resource consumption, and ease of use. These synchronization techniques helps to improve the performance and reliability of multi-threaded applications.
