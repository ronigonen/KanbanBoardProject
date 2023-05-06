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
            userService.Register("hadas@gmail.com", "Aa0123457");
            userService.Register("roni@gmail.com", "R0123546r");
            userService.Register("noga@gmail.com", "A234589a");
        }

        public void runTests()
        {
            createUsers();
            runTestSuccessfullCreateBoard();
            runTestFailedCreateBoardBySameNameBoardToSameUser();
            runTestSuccessfullLimitColumn();
            runTestSucceessGetLimitColumn();
            runTestSucceessGetNameColumn();
            runTestSuccessfullAddTask();
            runTestFailedAddTaskByLimitColumn();
            //runTestSuccessfullGetColumn();
            runTestSuccessfullDeleteBoard();
            runTestSuccessfullAddTaskAfterDeleteBoard();
            runTestSuccessfullAdvanceTask();
            runTestFailedAdvanceTask();
            //runTestSuccessfullGetInProgress();
        }

        public void runTestSuccessfullCreateBoard()
        {
            Response resp1a = JsonSerializer.Deserialize<Response>(boardService.CreateBoard("hadas@gmail.com", "AssignmentHadas"));
            if (resp1a.ErrorOccured())
            {
                Console.WriteLine(resp1a.ErrorMessage);
            }
            else
            {
                Console.WriteLine("runTestSuccessfullCreateBoard- succeeded.");
            }
            Response resp1c = JsonSerializer.Deserialize<Response>(boardService.CreateBoard("roni@gmail.com", "Assignment1"));
            if (resp1c.ErrorOccured())
            {
                Console.WriteLine(resp1c.ErrorMessage);
            }
            else
            {
                Console.WriteLine("runTestSuccessfullCreateBoard- succeeded.");
            }
            Response resp1d = JsonSerializer.Deserialize<Response>(boardService.CreateBoard("roni@gmail.com", "Assignment2"));
            if (resp1d.ErrorOccured())
            {
                Console.WriteLine(resp1d.ErrorMessage);
            }
            else
            {
                Console.WriteLine("runTestSuccessfullCreateBoard- succeeded.");
            }
            Response resp1e = JsonSerializer.Deserialize<Response>(boardService.CreateBoard("noga@gmail.com", "Assignment3"));
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
            Response resp1b = JsonSerializer.Deserialize<Response>(boardService.CreateBoard("hadas@gmail.com", "AssignmentHadas"));
            if (resp1b.ErrorOccured())
            {
                Console.WriteLine("runTestFailedCreateBoardBySameNameBoardToSameUser- succeeded.");
            }
            else
            {
                Console.WriteLine("runTestFailedCreateBoardBySameNameBoardToSameUse- failed.");
            }
        }


        public void runTestSuccessfullLimitColumn()
        {
            Response resp2a = JsonSerializer.Deserialize<Response>(boardService.LimitColumn("hadas@gmail.com", "AssignmentHadas", 1, 2));
            if (resp2a.ErrorOccured())
            {
                Console.WriteLine(resp2a.ErrorMessage);
            }
            else
            {
                Console.WriteLine("runTestSuccessfullLimitColumn- succeeded.");
            }
            Response resp2c = JsonSerializer.Deserialize<Response>(boardService.LimitColumn("roni@gmail.com", "Assignment1", 2, 3));
            if (resp2c.ErrorOccured())
            {
                Console.WriteLine(resp2c.ErrorMessage);
            }
            else
            {
                Console.WriteLine("runTestSuccessfullLimitColumn- succeeded.");
            }
        }

        public void runTestSucceessGetLimitColumn()
        {
            Response resp3a = JsonSerializer.Deserialize<Response>(boardService.GetColumnLimit("hadas@gmail.com", "AssignmentHadas", 1));
            Object o = resp3a.ReturnValue;
            if (o.ToString().Equals("2"))
                Console.WriteLine("runTestSucceessGetLimitColumn- succeeded.");
            else
                Console.WriteLine("runTestSucceessGetLimitColumn- failed.");
        }

        public void runTestSucceessGetNameColumn()
        {
            Response resp3b = JsonSerializer.Deserialize<Response>(boardService.GetColumnName("roni@gmail.com", "Assignment1", 2));
            Object o1 = resp3b.ReturnValue;
            if (o1.ToString().Equals("In Progress"))
                Console.WriteLine("runTestSucceessGetNameColumn- succeeded.");
            else
                Console.WriteLine("runTestSucceessGetNameColumn- failed.");
        }


        public void runTestSuccessfullAddTask()
        {
            Response resp5a = JsonSerializer.Deserialize<Response>(boardService.AddTask("hadas@gmail.com", "AssignmentHadas", "Mission1", "your first mission", new DateTime(2023, 05, 20), new DateTime(2023, 04, 23)));
            if (resp5a.ErrorOccured())
            {
                Console.WriteLine(resp5a.ErrorMessage);
            }
            else
            {
                Console.WriteLine("runTestSuccessfullAddTask- succeeded.");
            }
            Response resp5b = JsonSerializer.Deserialize<Response>(boardService.AddTask("hadas@gmail.com", "AssignmentHadas", "Mission2", "your second mission", new DateTime(2023, 05, 20), new DateTime(2023, 04, 23)));
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
            Response resp5c = JsonSerializer.Deserialize<Response>(boardService.AddTask("hadas@gmail.com", "AssignmentHadas", "Mission3", "your third mission", new DateTime(2023, 05, 20), new DateTime(2023, 04, 23)));
            if (resp5c.ErrorOccured())
            {
                Console.WriteLine("runTestFailedAddTaskByLimitColumn- succeeded.");
            }
            else
            {
                Console.WriteLine("runTestFailedAddTaskByLimitColumn- failed.");
            }
        }

        public void runTestSuccessfullGetColumn()
        {
            Response resp3a = JsonSerializer.Deserialize<Response>(boardService.GetColumn("hadas@gmail.com", "AssignmentHadas", 1));
            Object o1 = resp3a.ReturnValue;
            Console.WriteLine(o1);
            if (o1.ToString().Equals("In Progress"))
                Console.WriteLine("runTestSucceessGetColumn- succeeded.");
            else
                Console.WriteLine("runTestSucceessGetColumn- failed.");
        }

        public void runTestSuccessfullDeleteBoard()
        {
            Response resp5d = JsonSerializer.Deserialize<Response>(boardService.DeleteBoard("roni@gmail.com", "Assignment1"));
            if (resp5d.ErrorOccured())
            {
                Console.WriteLine(resp5d.ErrorMessage);
            }
            else
            {
                Console.WriteLine("runTestSuccessfullDeleteBoard- succeeded.");
            }
        }

        public void runTestSuccessfullAddTaskAfterDeleteBoard()
        {
            Response resp5a = JsonSerializer.Deserialize<Response>(boardService.AddTask("roni@gmail.com", "Assignment1", "Mission2", "your first mission", new DateTime(2023, 05, 20), new DateTime(2023, 04, 23)));
            if (resp5a.ErrorOccured())
            {
                Console.WriteLine("runTestSuccessfullAddTaskAfterDeleteBoard- succeeded.");
            }
            else
            {
                Console.WriteLine("runTestSuccessfullAddTaskAfterDeleteBoard- failed.");
            }
        }

        public void runTestSuccessfullAdvanceTask()
        {
            Response resp5a = JsonSerializer.Deserialize<Response>(boardService.AdvanceTask("hadas@gmail.com", "AssignmentHadas", 1, 0));
            if (resp5a.ErrorOccured())
            {
                Console.WriteLine(resp5a.ErrorMessage);
            }
            else
            {
                Console.WriteLine("runTestSuccessfullAdvanceTask- succeeded.");
            }
            Response resp5b = JsonSerializer.Deserialize<Response>(boardService.AdvanceTask("hadas@gmail.com", "AssignmentHadas", 2, 0));
            if (resp5b.ErrorOccured())
            {
                Console.WriteLine(resp5b.ErrorMessage);
            }
            else
            {
                Console.WriteLine("runTestSuccessfullAdvanceTask- succeeded.");
            }
            Response resp5c = JsonSerializer.Deserialize<Response>(boardService.AdvanceTask("hadas@gmail.com", "AssignmentHadas", 1, 1));
            if (resp5c.ErrorOccured())
            {
                Console.WriteLine(resp5c.ErrorMessage);
            }
            else
            {
                Console.WriteLine("runTestSuccessfullAdvanceTask- succeeded.");
            }
        }

        public void runTestFailedAdvanceTask()
        {
            Response resp5b = JsonSerializer.Deserialize<Response>(boardService.AdvanceTask("hadas@gmail.com", "AssignmentHadas", 3, 0));
            if (resp5b.ErrorOccured())
            {
                Console.WriteLine("runTestFailedAdvanceTask- succeeded.");
            }
            else
            {
                Console.WriteLine("runTestFailedAdvanceTask- failed.");
            }
        }

        public void runTestSuccessfullGetInProgress()
        {
            Response resp3a = JsonSerializer.Deserialize<Response>(boardService.InProgressTasks("hadas@gmail.com"));
            Object o1 = resp3a.ReturnValue;
            Console.WriteLine(o1);
            if (o1.ToString().Equals("In Progress"))
                Console.WriteLine("runTestSuccessfullGetInProgress- succeeded.");
            else
                Console.WriteLine("runTestSuccessfullGetInProgress- failed.");   
        }


    }
}
