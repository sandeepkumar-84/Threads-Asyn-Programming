// initially the signal is false. 
ManualResetEventSlim manualResetEvent = new ManualResetEventSlim(false);

Console.WriteLine("Main Thread Waiting for Go...............");

for (int i = 0; i < 4; i++)
{
    Thread thread = new Thread(WorkerProcess);
    thread.Name = $"WorkerThread-{i + 1}";
    thread.Start();

}

   
string? input = Console.ReadLine();


if (input?.ToLower() == "g")
{
    Console.WriteLine("Main Thread Signaling Worker Thread");
    manualResetEvent.Set(); // Signal the worker thread
}


void WorkerProcess()
{
    while (true)
    {
        Console.WriteLine($"Worker Process {Thread.CurrentThread.Name} is Waiting... ");
        // All the threads will be released when the signal is set to true
        manualResetEvent.Wait(); // Wait until signaled
        {
            Console.WriteLine($"Worker Process {Thread.CurrentThread.Name}  Released... ");
            Thread.Sleep(3000);
        }
    }
}