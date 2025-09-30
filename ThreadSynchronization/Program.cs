int counter = 0;

Object lockObject = new Object();

Thread thread1 = new Thread(IncrementCounter);
Thread thread2 = new Thread(IncrementCounter);

thread1.Start();
thread2.Start();

thread1.Join();
thread2.Join();

Console.WriteLine($"Counter value is {counter}");

//Thread synchronization. Why it is required. 

// 1.Resource Sharing and non atomic statements issues in multi threading can cause race/inconsistent behavior as all the threads if using same resource can access it at the same time. 
// 2. To avoid this we use thread synchronization techniques like lock, mutex, semaphore etc.
void IncrementCounter()
{
    for (int i = 0; i < 100; i++)
    { 
        Thread.Sleep(10);
        // critical section is being locked  
        lock (lockObject)
        {
            counter = counter + 1; // non atomic statement and shared resource
        }
        
    }
}





