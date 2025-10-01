// initially the signal is false. 
ManualResetEventSlim manualResetEvent = new ManualResetEventSlim(false);


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
        manualResetEvent.Set(); // Signal the worker thread
    }
}

void WorkerProcess()
{
    while (true)
    {
        Console.WriteLine($"Worker Process {Thread.CurrentThread.Name} is Waiting... ");
        autoResetEvent.WaitOne(); // Wait until signaled
        {
            Console.WriteLine($"Worker Process {Thread.CurrentThread.Name}  Consuming... ");
            Thread.Sleep(3000);
        }
    }
}