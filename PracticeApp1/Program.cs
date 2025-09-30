

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


ThreadWithParameter threadWithParameter = new ThreadWithParameter(30);

Thread threadWithParam = new Thread(threadWithParameter.ShowNumbers);

threadWithParam.Start();


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

