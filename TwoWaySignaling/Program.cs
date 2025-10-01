
Queue<int> producerQueue = new Queue<int>();
Thread[] threads = new Thread[4];
ManualResetEventSlim manualResetEvent = new ManualResetEventSlim(false);
ManualResetEventSlim manualResetEventProducer = new ManualResetEventSlim(true);
object lockObject = new object();
int threadCount = 4;
int counter = threadCount;

for (int i = 0; i < threadCount; i++)
{ 
    Thread thread = new Thread(Consumer);
    thread.Name = $"ConsumerThread-{i + 1}";
    threads[i] = thread;
}

foreach (var item in threads)
{
    item.Start();
}

ProducerController();

void ProducerController()
{
    Thread producerThread = new Thread(Producer);
    producerThread.Name = "ProducerThread-1";
    manualResetEventProducer.Wait();
    producerThread.Start();
}
void Producer()
{
    Console.WriteLine($"Producing the produce......");
    for (int i = 0; i < 10; i++)
    {
        producerQueue.Enqueue(i);
        Thread.Sleep(200);
    }
    
    Console.WriteLine($"Production Completed and ready to be consumed.");
    manualResetEvent.Set(); // Signal the consumer thread
}
void Consumer()
{
    // wait for the producer to produce
    // gets the signal
    // consume the item 

    while (true)
    {
        manualResetEvent.Wait();
        try
        {
                lock (lockObject)
                {
                    if (producerQueue?.Count > 0)
                    {
                        Console.WriteLine($"Consuming the item {producerQueue.Dequeue()} by thread {Thread.CurrentThread.Name}");
                        Thread.Sleep(100);
                        counter = counter - 1;

                        if (producerQueue.Count == 0)
                        {
                            Console.WriteLine($"Consumed all the items............Waiting for more production...");
                            
                            manualResetEvent.Reset();
                            manualResetEventProducer.Reset();
                        }
                    }
                }
        }
        finally
        {
            // any cleanup code
        }
    }
}