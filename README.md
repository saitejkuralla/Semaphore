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

:sparkles: <b>Real world scenario:</b> The bus company's online booking system uses a lock statement to create a mutual exclusion lock on seat objects. This ensures that only one thread can access the seat at a time and prevents race conditions when multiple customers try to book the same seat simultaneously. This ensures that the booking process is properly processed without conflicts.


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
:sparkles: <b>Real world scenario:</b> A bank account class is implemented in C# using Monitors to synchronize the deposit and withdrawal operations. The Monitor.Enter method locks the object and Monitor.Wait method blocks the thread until a deposit is made. Once a deposit is made, Monitor.Pulse signals the waiting thread to execute. This ensures that the balance remains accurate and prevents race conditions between multiple threads accessing the same account object.<br>


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
:sparkles: <b>Real world scenario:</b> A bank with multiple ATMs can use a semaphore to limit the number of customers accessing the ATMs at the same time. This helps to ensure that the ATMs are not overloaded and that all customers have a fair chance to access them. By using semaphores, the bank can easily adjust the maximum number of customers allowed to use the ATMs simultaneously, depending on the time of day, day of the week, or other factors that may affect the customer traffic.


### SemaphoreSlim class :vertical_traffic_light:

* SemaphoreSlim is a lightweight alternative to Semaphore class with less overhead
* It allows a limited number of threads to enter a critical section simultaneously
* WaitAsync method blocks a thread until the semaphore's count becomes greater than zero
* Release method increments the semaphore's count and wakes up one blocked thread
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
} finally {
    // Release the semaphore when the critical section is complete
    semaphore.Release();
}
```

:sparkles: <b>Real world scenario:</b> A SemaphoreSlim is used in the program to limit the maximum number of credit card processing tasks that can be executed simultaneously. This helps to prevent overloading the system and ensures that all tasks are processed efficiently. The program generates a list of credit cards and processes them using async tasks. The semaphore is set to allow a maximum of any number of tasks to execute concurrently. The program uses await/async keywords to allow the tasks to execute asynchronously, and the SemaphoreSlim class to synchronize the access to the shared resources.
