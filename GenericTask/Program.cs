using System.Reflection.Emit;

Console.WriteLine($"Main Thread {Thread.CurrentThread.ManagedThreadId} Started");


Task<int> taskPrint = Task.Run(() => SumNumbers(5) );

Task<Student> taskStudent = Task.Run(() => CreateStudent(100,"George"));

taskPrint.Wait();

Console.WriteLine($"Main Thread {Thread.CurrentThread.ManagedThreadId} Completed");

Console.WriteLine($"Sum is: {taskPrint.Result}");

Console.WriteLine($"Student Id: {taskStudent.Result.Id}, Name: {taskStudent.Result.Name}");
int SumNumbers(int max)
{
    int sum = 0;
    Console.WriteLine($"Child Thread {Thread.CurrentThread.ManagedThreadId} Started");
    for (int i = 1; i <= max; i++)
    {
        sum = sum + i;
    }
    Console.WriteLine($"Child Thread {Thread.CurrentThread.ManagedThreadId} Completed");

    return sum;
}

// returning complex type

Student CreateStudent(int id, string name)
{ 
    return new Student { Id = id, Name = name };
}

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
}