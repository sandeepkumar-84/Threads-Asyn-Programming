string path = "../../../counter.txt";
object lockObj = new object();

Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId}");

// below code write 100 in the file. 
// if two instances are used then 200 should be written. however since the critical section is not protected,
// it is possible that the final value is less than 200.
//for (int i = 0; i < 100; i++)
//{
//    // critical section
//    int tempCount = ReadFromFile(path);
//    tempCount++;
//    WriteToFile(tempCount);   
//}

// lets use lock and check if it solves the problem
//for (int i = 0; i < 10000; i++)
//{
//    // critical section
//    lock (lockObj)
//    {
//        int tempCount = ReadFromFile(path);
//        tempCount++;
//        WriteToFile(tempCount);
//        Console.WriteLine($"tempCount: {tempCount}");
//    }
//}

// lets use Mutex and check if it solves the problem

using (Mutex mutex = new Mutex(false, "myMutex"))
{
    mutex.WaitOne();
    try
    {
        for (int i = 0; i < 10000; i++)
        {
            // critical section
            lock (lockObj)
            {
                int tempCount = ReadFromFile(path);
                tempCount++;
                WriteToFile(tempCount);
                Console.WriteLine($"tempCount: {tempCount}");
            }
        }
    }
    finally
    {
        mutex.ReleaseMutex();
    }

}


Console.WriteLine($"process finished...");

Console.ReadLine();

void WriteToFile(int tempCount)
{
    File.WriteAllText(path, tempCount.ToString());
}
int ReadFromFile(string filePath)
{
    int tempCount = 0;
    if (File.Exists(filePath))
    {
        string text = File.ReadAllText(filePath);
        tempCount = int.Parse(text);
    }
    return tempCount;
}


