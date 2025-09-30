

Console.WriteLine("Hello, World!");
void WriteThreadID()
{
    Console.WriteLine($"Current thread: {Thread.CurrentThread.ManagedThreadId}");
}

/*
 * Different ways to create and start a thread
 

// main thread
WriteThreadID();

// diferent new thread - directlty in constructor
Thread thread1 = new Thread(WriteThreadID);
thread1.Start();


// different new thread - using ThreadStart delegate    
ThreadStart threadStart2 = new ThreadStart(WriteThreadID);
Thread thread2 = new Thread(threadStart2);
thread2.Start();

// different new thread - using lambda expression
ThreadStart threadStart3 = new ThreadStart(
    () => { WriteThreadID(); }
 );

Thread thread3 = new Thread(threadStart3);
thread3.Start();
// different new thread - using method group conversion

ThreadStart threadStart4 = WriteThreadID;
Thread thread4 = new Thread(threadStart4);
thread4.Start();

Console.ReadLine();

*/

/*
 * Passing parameters to a thread
 * ParameterizedThreadStart delegate


static void ShowNumbers(object maxnum)
{
    for (int i = 0; i < Convert.ToInt16(maxnum); i ++)    
    { 
        Console.Write(i + " ");
    }
}

object max = 10;
ParameterizedThreadStart parameterizedThreadStart = new ParameterizedThreadStart(ShowNumbers);
Thread threadParamEx = new Thread(parameterizedThreadStart);
threadParamEx.Start(max);

Thread threadParamLambda = new Thread(() => ShowNumbers(20));
threadParamLambda.Start();
Console.ReadLine();
 */

// Above methods are not type safe.
// We can use helper classes. Now if you pass types other than int, it will be compile time error.
// 


//ThreadWithParameter threadWithParameter = new ThreadWithParameter(30);

//Thread threadWithParam = new Thread(threadWithParameter.ShowNumbers);

//threadWithParam.Start();


// above cases deligates returns void. To capture the retunred data through delegates
// retrieval of the return value from a thread

static void DisplaySum(int sum)
{
    Console.WriteLine($"\n Sum of numbers is : {sum}");
}

//ThreadWithParameterAndreturn threadWithParameterAndreturn = new ThreadWithParameterAndreturn(30, DisplaySum);

//Thread threadWithParamAndReturn = new Thread(threadWithParameterAndreturn.ShowNumbers);
//threadWithParamAndReturn.Start();

// Thread Use cases
// Divide and conquering the big fat task.
// 

int[] arr = [1, 2, 3, 4, 5, 6, 7, 8, 9];
int sum = 0;
object lockObj = new object();

// emulates a big task
void SumLargeNumbers()
{
    foreach (var item in arr)
    {
        // Sum is a shared resource
        // it is not an atomic statement
        // therefor the whole function is locked using lock(lockObj)
        sum = sum + item; 
        Thread.Sleep(100);
    }
}

void LoopSum(int loop)
{
    for (int i = 1; i <= loop;  i++)
    {
        lock(lockObj)
        {
            SumLargeNumbers();
        }        
    }
}

float startTime = DateTime.Now.Millisecond;
LoopSum(4);
float endTime = DateTime.Now.Millisecond;

Console.WriteLine($"\n Time taken to execute the task in main thread: {endTime - startTime} ms calculated sum = {sum}");

sum = 0;
// using multiple threads

Thread thread1 = new Thread(()=> LoopSum(1));
Thread thread2 = new Thread(() => LoopSum(1));
Thread thread3 = new Thread(() => LoopSum(1));
Thread thread4 = new Thread(() => LoopSum(1));

startTime = DateTime.Now.Millisecond;

thread1.Start();
thread2.Start();
thread3.Start();
thread4.Start();
thread1.Join();
thread2.Join();
thread3.Join();
thread4.Join();

endTime = DateTime.Now.Millisecond;

Console.WriteLine($"\n Time taken to execute the task in multiple threads: {endTime - startTime} ms calculated sum = {sum}");




public delegate void SumOfNumbers(int max);

public class ThreadWithParameter
{
    private int _max;
    public ThreadWithParameter(int max)
    {
        _max = max;
    }

    public void ShowNumbers()
    {
        for (int i = 0; i < _max; i++)
        {
            Console.Write(i + " ");
        }
    }
}


public class ThreadWithParameterAndreturn
{
    private int _max;
    SumOfNumbers _delegateSumOfNumbers = null;
    public ThreadWithParameterAndreturn(int max, SumOfNumbers _delegate)
    {
        _max = max;
        _delegateSumOfNumbers = _delegate;
    }

    public void ShowNumbers()
    {
        int sum = 0;
        for (int i = 0; i < _max; i++)
        {
           sum = sum + i;
        }
        if (_delegateSumOfNumbers != null)
        {
            _delegateSumOfNumbers(sum);
        }
    }
}





