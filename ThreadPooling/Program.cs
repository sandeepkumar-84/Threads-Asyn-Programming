void WithThread()
{
    for (int i = 0; i < 5; i++)
    { 
        Thread thread = new Thread(Test);
        thread.Start();
    }
}


void WithThreadPool()
{
    for (int i = 0; i < 5; i++)
    {
        ThreadPool.QueueUserWorkItem(new WaitCallback(Test));
    }
}

// start and end time

int startTime = DateTime.Now.Millisecond;

WithThread();
int endTime = DateTime.Now.Millisecond;

Console.WriteLine("Time taken with Thread: " + (endTime - startTime) + " ms");

startTime = DateTime.Now.Millisecond;

WithThreadPool();

endTime = DateTime.Now.Millisecond;

Console.WriteLine("Time taken with ThreadPool: " + (endTime - startTime) + " ms");

void Test(object obj)
{
     Console.WriteLine("Test");
}
Console.ReadLine();
