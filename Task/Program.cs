


using System.Reflection.Emit;

Console.WriteLine($"Main Thread {Thread.CurrentThread.ManagedThreadId} Started");

// Create and start a task to print numbers
//Task taskPrint = new Task(PrintNumbers);
//taskPrint.Start();

// create using factory. starts immediately
//Task taskPrint = Task.Factory.StartNew(PrintNumbers);

// create using run method. starts immediately recommended
// advantages of using run method are it is simpler and it uses thread pool threads

//Task.Run() for simplicity, safety, and correct async behavior.
//Task.Factory.StartNew() is powerful but risky unless you really know what you're doing.
//new Task() is low - level and almost never needed.

Task taskPrint = Task.Run(PrintNumbers);


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