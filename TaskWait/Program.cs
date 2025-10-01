
//Case 1: Here the main thraed starts and completes first and then the child thread starts and completes
// for main thread in order to wait and completes after the child threads completes
// In threads thread.join was used. 
// In task task.wait can be used.


Console.WriteLine($"Main Thread {Thread.CurrentThread.ManagedThreadId} Started");
Task taskPrint = Task.Run(PrintNumbers);
// if this is not used the main thread will complete first and then the child thread will start and complete
taskPrint.Wait();
Console.WriteLine($"Main Thread {Thread.CurrentThread.ManagedThreadId} Completed");
void PrintNumbers()
{
    Console.WriteLine($"Child Thread {Thread.CurrentThread.ManagedThreadId} Started");
    for (int i = 1; i <= 5; i++)
    {
        Console.WriteLine(i);

    }
    Console.WriteLine($"Child Thread {Thread.CurrentThread.ManagedThreadId} Completed");
}
