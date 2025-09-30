//Thread synchronization. Why it is required. 

// 1.Resource Sharing and non atomic statements issues in multi threading can cause race/inconsistent behavior as all the threads if using same resource can access it at the same time. 
// 2. To avoid this we use thread synchronization techniques like lock, mutex, semaphore etc.

// Example using locks 
int counter = 0;

Object lockObject = new Object();

//Thread thread1 = new Thread(IncrementCounter);
//Thread thread2 = new Thread(IncrementCounter);

//thread1.Start();
//thread2.Start()
//thread1.Join();
//thread2.Join();

// Console.WriteLine($"Counter value is {counter} using locks");


// Example using monitors

//Thread thread3 = new Thread(IncrementCounterUsingMonitors);
//Thread thread4 = new Thread(IncrementCounterUsingMonitors);

//thread3.Start();
//thread4.Start();

//thread3.Join();
//thread4.Join();

//Console.WriteLine($"Counter value is {counter} using monitors");

//Thread thread5 = new Thread(IncrementCounterUsingMonitorsTryEnter);
//Thread thread6 = new Thread(IncrementCounterUsingMonitorsTryEnter);

//thread5.Start();
//thread6.Start();
//thread5.Join();
//thread6.Join();

//Console.WriteLine($"Counter value is {counter} using monitors tryenter");


// Mutex

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
void IncrementCounterUsingMonitors()
{
    for (int i = 0; i < 100; i++)
    {
        Thread.Sleep(10);
        // critical section is being locked  
        Monitor.Enter(lockObject);
        try
        {            
                counter = counter + 1; // non atomic statement and shared resource            
        }
        finally
        { 
            Monitor.Exit(lockObject);
        }

    }
}


void IncrementCounterUsingMonitorsTryEnter()
{    
    for (int i = 0; i < 100; i++)
    {  
        // critical section is being locked  
        if(Monitor.TryEnter(lockObject,100))
        {
            try
            {
                Thread.Sleep(100);
                counter = counter + 1; // non atomic statement and shared resource            
            }
            finally
            {
                Monitor.Exit(lockObject);
            }
        }
        else
        { 
            Console.WriteLine("system is busy...");
        }

    }
}



