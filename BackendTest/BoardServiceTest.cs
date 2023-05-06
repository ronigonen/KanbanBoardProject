using IntroSE.ForumSystem.Backend.ServiceLayer;
using IntroSE.Kanban.Backend.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BackendTest
{
    class BoardServiceTest
    {
        private readonly BoardService boardService;
        private readonly UserService userService;

        public BoardServiceTest(WrapperClass w)
        {
            this.boardService=w.boardService;
            this.userService = w.UserService;
        }

        public void createUsers()
        {
            userService.Register("hadas12@gmail.com", "Aa012345");
            userService.Register("roni12@gmail.com", "R012356r");
            userService.Register("noga12@gmail.com", "A2345a");
        }

        public void runTests()
        {
         //   createUsers();
            runTestSuccessfullCreateBoard();
         //   runTestFailedCreateBoardBySameNameBoardToSameUser();
         //   runTestSuccessfullLimitColumnAndGetLimitColumn();
          //  runTestFailedLimitColumnByNotExistColumnAndGetLimitColumn();
          //  runTestSuccessfullAddTask();
          //  runTestFailedAddTaskByLimitColumn();
          //  runTestSuccessfullDeleteBoard();
        }

        public void runTestSuccessfullCreateBoard()
        {
            Response resp1a = JsonSerializer.Deserialize<Response>(boardService.CreateBoard("hadas12@gmail.com", "AssignmentHadas"));
            if (resp1a.ErrorOccured())
            {
                Console.WriteLine(resp1a.ErrorMessage);
            }
            else
            {
                Console.WriteLine("runTestSuccessfullCreateBoard- succeeded.");
            }
            Response resp1c = JsonSerializer.Deserialize<Response>(boardService.CreateBoard("roni12@gmail.com", "Assignment1"));
            if (resp1c.ErrorOccured())
            {
                Console.WriteLine(resp1c.ErrorMessage);
            }
            else
            {
                Console.WriteLine("runTestSuccessfullCreateBoard- succeeded.");
            }
            Response resp1d = JsonSerializer.Deserialize<Response>(boardService.CreateBoard("roni12@gmail.com", "Assignment2"));
            if (resp1d.ErrorOccured())
            {
                Console.WriteLine(resp1d.ErrorMessage);
            }
            else
            {
                Console.WriteLine("runTestSuccessfullCreateBoard- succeeded.");
            }
            Response resp1e = JsonSerializer.Deserialize<Response>(boardService.CreateBoard("noga12@gmail.com", "Assignment3"));
            if (resp1e.ErrorOccured())
            {
                Console.WriteLine(resp1e.ErrorMessage);
            }
            else
            {
                Console.WriteLine("runTestSuccessfullCreateBoard- succeeded.");
            }
        }

        public void runTestFailedCreateBoardBySameNameBoardToSameUser()
        {
            Response resp1b = JsonSerializer.Deserialize<Response>(boardService.CreateBoard("hadas12@gmail.com", "AssignmentHadas"));
            if (resp1b.ErrorOccured())
            {
                Console.WriteLine("runTestFailedCreateBoardBySameNameBoardToSameUser- succeeded.");
            }
            else
            {
                Console.WriteLine("runTestFailedCreateBoardBySameNameBoardToSameUse- failed.");
            }
        }


        public void runTestSuccessfullLimitColumnAndGetLimitColumn()
        {
            Response resp2a = JsonSerializer.Deserialize<Response>(boardService.LimitColumn("hadas12@gmail.com", "AssignmentHadas", 1, 2));
            if (resp2a.ErrorOccured())
            {
                Console.WriteLine(resp2a.ErrorMessage);
            }
            else
            {
                Console.WriteLine("runTestSuccessfullLimitColumn- succeeded.");
            }
            Response resp2c = JsonSerializer.Deserialize<Response>(boardService.LimitColumn("roni12@gmail.com", "Assignment1", 2, 3));
            if (resp2c.ErrorOccured())
            {
                Console.WriteLine(resp2c.ErrorMessage);
            }
            else
            {
                Console.WriteLine("runTestSuccessfullLimitColumn- succeeded.");
            }
        }

        public void runTestFailedLimitColumnByNotExistColumnAndGetLimitColumn()
        {
            Response resp2b = JsonSerializer.Deserialize<Response>(boardService.LimitColumn("hadas12@gmail.com", "AssignmentHadas", 8, 2));
            if (resp2b.ErrorOccured())
            {
                Console.WriteLine("runTestFailedLimitColumnByNotExistColumn- succeeded.");
            }
            else
            {
                Console.WriteLine("runTestFailedLimitColumnByNotExistColumn- failed.");
            }
        }


        public void runTestSuccessfullAddTask()
        {
            Response resp5a = JsonSerializer.Deserialize<Response>(boardService.AddTask("hadas12@gmail.com", "AssignmentHadas", "Mission1", "your first mission", new DateTime(2023, 05, 20), new DateTime(2023, 04, 23)));
            if (resp5a.ErrorOccured())
            {
                Console.WriteLine(resp5a.ErrorMessage);
            }
            else
            {
                Console.WriteLine("runTestSuccessfullAddTask- succeeded.");
            }
            Response resp5b = JsonSerializer.Deserialize<Response>(boardService.AddTask("hadas12@gmail.com", "AssignmentHadas", "Mission2", "your second mission", new DateTime(2023, 05, 20), new DateTime(2023, 04, 23)));
            if (resp5b.ErrorOccured())
            {
                Console.WriteLine(resp5a.ErrorMessage);
            }
            else
            {
                Console.WriteLine("runTestSuccessfullAddTask- succeeded.");
            }
        }

        public void runTestFailedAddTaskByLimitColumn()
        {
            Response resp5c = JsonSerializer.Deserialize<Response>(boardService.AddTask("hadas12@gmail.com", "AssignmentHadas", "Mission3", "your third mission", new DateTime(2023, 05, 20), new DateTime(2023, 04, 23)));
            if (resp5c.ErrorOccured())
            {
                Console.WriteLine("runTestFailedAddTaskByLimitColumn- failed.");
            }
            else
            {
                Console.WriteLine("runTestFailedAddTaskByLimitColumn- failed.");
            }
        }

        public void runTestSuccessfullDeleteBoard()
        {
            Response resp5d = JsonSerializer.Deserialize<Response>(boardService.DeleteBoard("hadas12@gmail.com", "AssignmentHadas"));
            if (resp5d.ErrorOccured())
            {
                Console.WriteLine(resp5d.ErrorMessage);
            }
            else
            {
                Console.WriteLine("runTestSuccessfullDeleteBoard- succeeded.");
            }
        }
    }
}
