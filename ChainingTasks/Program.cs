// See https://aka.ms/new-console-template for more information
Console.WriteLine($"Main Thread {Thread.CurrentThread.ManagedThreadId} Started");


//Task<string> taskSum = Task<int>.Run(
//                        () => SumNumbers(5)
//                        ).ContinueWith((s) => "Sum of the numbers is = " + s.Result.ToString());


//taskSum.Wait();

//Console.WriteLine(taskSum.Result);


Task<int> taskSum1 = Task<int>.Run(
                        () => SumNumbers(5)
                        );

taskSum1.ContinueWith((s) =>
    Console.WriteLine("Sum of the numbers is = " + s.Result.ToString())
    , TaskContinuationOptions.OnlyOnRanToCompletion);

taskSum1.ContinueWith(t =>
{
    Console.WriteLine($"[Error] Exception: {t.Exception?.InnerException?.Message}");
}, TaskContinuationOptions.OnlyOnFaulted);

taskSum1.ContinueWith(t =>
{
    Console.WriteLine("[Cancelled] Task was cancelled.");
}, TaskContinuationOptions.OnlyOnCanceled);

try { taskSum1.Wait();  } catch { } // to avoid warning


Console.WriteLine($"Main Thread {Thread.CurrentThread.ManagedThreadId} Completed");

int SumNumbers(int max)
{
    int sum = 0;
    
    for (int i = 1; i <= max; i++)
    {
        if (i == 3)
        {
           // throw new Exception("Something went wrong at i = 3"); // test Faulted
        }
        sum = sum + i;
    }
    
    return sum;
}