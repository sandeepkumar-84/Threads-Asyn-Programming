Console.WriteLine("Web Server Running........\n Enter any character to process request q to exit");

// It is not thread safe since if the the system is multi-core and the threads are running on different cores,
// there could be chance that the requestQueue is being accessed by multiple threads at the same time.
// therefore the data structure should be thread safe.
Queue<string> requestQueue = new Queue<string>();

Thread monitorThread = new Thread(MonitorQueue);
monitorThread.Name = "MonitorThread";
monitorThread.Start();

while (true)
{    
    string? input =  Console.ReadLine();
    if (input?.ToLower() == "q")
    {        
        break;
    }
    requestQueue.Enqueue(input);
}

void MonitorQueue()
{ 
    while(true)
    {
        if (requestQueue.Count > 0)
        { 
            string inputReq = requestQueue.Dequeue();
            Thread threadProcessReq = new Thread(() => ProcessRequest(inputReq));
            threadProcessReq.Name = $"Thread-{inputReq}";
            threadProcessReq.Start();
        }
        Thread.Sleep(100);
    }
    
}

void ProcessRequest(string input)
{
    Thread.Sleep(3000); // Simulate processing time
    Console.WriteLine($"Processing Request Completed {input} by Thread {Thread.CurrentThread.Name}");
}

