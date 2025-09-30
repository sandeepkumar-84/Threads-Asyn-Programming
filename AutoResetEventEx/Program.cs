// initially the signal is false. 
AutoResetEvent autoResetEvent = new AutoResetEvent(false);

//Thread thread = new Thread(() => WorkerProcess());
//thread.Start();


// even if there are multiple worker threads,
// only one thread will be released when the signal is set to true.
// in order to release multiple threads, multiple signals have to be sent.
// this is acheieved by using manual reset event.
for (int i = 0; i < 4; i++)
{ 
    Thread thread = new Thread(WorkerProcess);
    thread.Name = $"WorkerThread-{i + 1}";
    thread.Start();

}

while (true)
{ 
    string? input = Console.ReadLine();

    if (input?.ToLower() == "g")
    {
        Console.WriteLine("Main Thread Signaling Worker Thread");
        autoResetEvent.Set(); // Signal the worker thread
    }
}

void WorkerProcess()
{   
    while(true)
    {
        Console.WriteLine($"Worker Process {Thread.CurrentThread.Name} is Waiting... ");
        autoResetEvent.WaitOne(); // Wait until signaled
        {
            Console.WriteLine($"Worker Process {Thread.CurrentThread.Name}  Consuming... ");
            Thread.Sleep(3000);
        }
    }
}