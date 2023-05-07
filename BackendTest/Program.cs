// See https://aka.ms/new-console-template for more information
using BackendTest;
using IntroSE.Kanban.Backend.ServiceLayer;
using System.Data.Common;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome!");
        WrapperClass w= new WrapperClass();
        UserServiceTest us= new UserServiceTest(w);
        BoardServiceTest bs = new BoardServiceTest(w);
        TaskServiceTest ts = new TaskServiceTest(w);
        us.runTests();
        bs.runTests();
        ts.runTests();  
    }
}

