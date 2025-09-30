Console.WriteLine("Web Server Running........\n Enter any character to process request q to exit");

// It is not thread safe since if the the system is multi-core and the threads are running on different cores,
// there could be chance that the requestQueue is being accessed by multiple threads at the same time.
// therefore the data structure should be thread safe.

// in this example semaphores are implemented so that the queue doesnt generate 
// large number of threads and overload the system. 

// semaphores are used to limit the number of threads that can access a resource or a section of code concurrently.
// semaphore wait and semaphore release are on different threads.
// They dont have to exist on same thread. therefore they are different than locks,
// monitor because they implement thread affinity and they have to be on the same thread to the aquired thread when when release   

Queue<string> requestQueue = new Queue<string>();

SemaphoreSlim semaphore = new SemaphoreSlim(initialCount:3, maxCount:3); // Limit to 3 concurrent threads

Thread monitorThread = new Thread(MonitorQueue);
monitorThread.Name = "MonitorThread";
monitorThread.Start();

while (true)
{
    string? input = Console.ReadLine();
    if (input?.ToLower() == "q")
    {
        break;
    }
    requestQueue.Enqueue(input);
}

void MonitorQueue()
{
    while (true)
    {
        if (requestQueue.Count > 0)
        {
            string inputReq = requestQueue.Dequeue();
            semaphore.Wait(); // Wait for an available slot
            Thread threadProcessReq = new Thread(() => ProcessRequest(inputReq));
            threadProcessReq.Name = $"Thread-{inputReq}";
            threadProcessReq.Start();
        }
        Thread.Sleep(100);
    }

}

void ProcessRequest(string input)
{
    try
    {
        Thread.Sleep(3000); // Simulate processing time
        Console.WriteLine($"Processing Request Completed {input} by Thread {Thread.CurrentThread.Name}");

    }
    finally
    { 
        semaphore.Release();
    }
}

