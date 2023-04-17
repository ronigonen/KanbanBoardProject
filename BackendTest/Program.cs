// See https://aka.ms/new-console-template for more information
using BackendTest;
using IntroSE.Kanban.Backend.ServiceLayer;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome!");
        UserService userService = new UserService();
        new UserServiceTest(userService).runTests();
        BoardService boardService = new BoardService();
        new BoardServiceTest(boardService).runTests();
        TaskService taskService = new TaskService();
        new TaskServiceTest(taskService).runTests();
    }
}

