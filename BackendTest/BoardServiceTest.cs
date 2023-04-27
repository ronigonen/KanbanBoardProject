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

        public BoardServiceTest(BoardService boardService)
        {
            this.boardService = boardService;
        }

        public void runTests()
        {
            User user1 = new User("hadas12@gmail.com", "Aa012345");
            User user2 = new User("roni12@gmail.com", "R012356r");
            Response resp1a = JsonSerializer.Deserialize<Response>(boardService.CreateBoard("hadas12@gmail.com", "AssignmentHadas"));
            if (resp1a.ErrorOccured)
            {
                Console.WriteLine(resp1a.ErrorMessage);
            }
            else
            {
                Console.WriteLine("hadas12@gmail.com created board successfully.");
            }
            Response resp1b = JsonSerializer.Deserialize<Response>(boardService.CreateBoard("hadas12@gmail.com", "AssignmentHadas"));
            if (resp1b.ErrorOccured)
            {
                Console.WriteLine(resp1b.ErrorMessage);
            }
            else
            {
                Console.WriteLine("Create board test failed- can't be two boards with the same name.");
            }
            Response resp1c = JsonSerializer.Deserialize<Response>(boardService.CreateBoard("roni12@gmail.com", "Assignment1"));
            if (resp1c.ErrorOccured)
            {
                Console.WriteLine(resp1c.ErrorMessage);
            }
            else
            {
                Console.WriteLine("roni12@gmail.com created board successfully.");
            }
            Response resp1d = JsonSerializer.Deserialize<Response>(boardService.CreateBoard("roni12@gmail.com", "Assignment2"));
            if (resp1d.ErrorOccured)
            {
                Console.WriteLine(resp1d.ErrorMessage);
            }
            else
            {
                Console.WriteLine("roni12@gmail.com created board successfully.");
            }



            Response resp2a = JsonSerializer.Deserialize<Response>(boardService.LimitColumn("hadas12@gmail.com", "AssignmentHadas", "done", 2));
            if (resp2a.ErrorOccured)
            {
                Console.WriteLine(resp2a.ErrorMessage);
            }
            else
            {
                Console.WriteLine("hadas12@gmail.com limited column done successfully.");
            }
            Response resp2b = JsonSerializer.Deserialize<Response>(boardService.LimitColumn("hadas12@gmail.com", "Ass", 3, 2));
            if (resp2b.ErrorOccured)
            {
                Console.WriteLine(resp2b.ErrorMessage);
            }
            else
            {
                Console.WriteLine("Limit column test failed- there isn't Ass board.");
            }
            Response resp2c = JsonSerializer.Deserialize<Response>(boardService.LimitColumn("roni12@gmail.com", "Assignment1", "InProgress", 3));
            if (resp2b.ErrorOccured)
            {
                Console.WriteLine(resp2c.ErrorMessage);
            }
            else
            {
                Console.WriteLine("roni12@gmail.com limited column in progress successfully.");
            }




            Response resp3a = JsonSerializer.Deserialize<Response>(boardService.GetColumnLimit("hadas12@gmail.com", "Assignment", 3));
            if (resp3a.ErrorOccured)
            {
                Console.WriteLine(resp3a.ErrorMessage);
            }
            else
            {
                Console.WriteLine("On board assignment in column number 3 the limit is 3 and we got" + resp3a);
            }
            Response resp3b = JsonSerializer.Deserialize<Response>(boardService.GetColumnLimit("hadas12@gmail.com", "Assignment", 2));
            if (resp3b.ErrorOccured)
            {
                Console.WriteLine(resp3b.ErrorMessage);
            }
            else
            {
                Console.WriteLine("Get column limit test failed.");
            }



            Response resp4a = JsonSerializer.Deserialize<Response>(boardService.GetColumnName("hadas12@gmail.com", "Assignment", 1));
            if (resp4a.ErrorOccured)
            {
                Console.WriteLine(resp4a.ErrorMessage);
            }
            else
            {
                Console.WriteLine("hadas12@gmail.com got the first column's name successfully.");
            }
            Response resp4b = JsonSerializer.Deserialize<Response>(boardService.GetColumnName("hadas12@gmail.com", "Assignment", 5));
            if (resp4b.ErrorOccured)
            {
                Console.WriteLine(resp4b.ErrorMessage);
            }
            else
            {
                Console.WriteLine("Get column name test failed.");
            }



            Response resp5a = JsonSerializer.Deserialize<Response>(boardService.AddTask("hadas12@gmail.com", "Assignment", "Mission1", "your first mission", new DateTime(2023, 04, 20)));
            if (resp5a.ErrorOccured)
            {
                Console.WriteLine(resp5a.ErrorMessage);
            }
            else
            {
                Console.WriteLine("The task 'Mission1' was added successfully.");
            }
            Response resp5b = JsonSerializer.Deserialize<Response>(boardService.AddTask("hadas12@gmail.com", "Ass", "Mission1", "your first mission", new DateTime(2023, 04, 20)));
            if (resp5b.ErrorOccured)
            {
                Console.WriteLine(resp5b.ErrorMessage);
            }
            else
            {
                Console.WriteLine("Add task test failed.");
            }



            Response resp6a = JsonSerializer.Deserialize<Response>(boardService.GetColumn("hadas12@gmail.com", "Assignment", 1));
            if (resp6a.ErrorOccured)
            {
                Console.WriteLine(resp6a.ErrorMessage);
            }
            else
            {
                Console.WriteLine("The Task that is supposed to return is 'Mission1' and we got"+resp6a);
            }
            Response resp6b = JsonSerializer.Deserialize<Response>(boardService.GetColumn("hadas12@gmail.com", "Assignment", 5));
            if (resp6b.ErrorOccured)
            {
                Console.WriteLine(resp6b.ErrorMessage);
            }
            else
            {
                Console.WriteLine("Get column test failed.");
            }




            Response resp7a = JsonSerializer.Deserialize<Response>(boardService.DeleteBoard("hadas12@gmail.com", "Assignment"));
            if (resp7a.ErrorOccured)
            {
                Console.WriteLine(resp7a.ErrorMessage);
            }
            else
            {
                Console.WriteLine("Board 'Assignment' deleted successfully");
            }
            Response resp7b = JsonSerializer.Deserialize<Response>(boardService.DeleteBoard("hadas12@gmail.com", "Assignment"));
            if (resp7b.ErrorOccured)
            {
                Console.WriteLine(resp7b.ErrorMessage);
            }
            else
            {
                Console.WriteLine("Same board was deleted twice - test failed.");
            }
            Response resp = JsonSerializer.Deserialize<Response>(boardService.CreateBoard("hadas12@gmail.com", "Assignment"));





            Response resp8a = JsonSerializer.Deserialize<Response>(boardService.InProgressTasks("hadas12@gmail.com"));
            if (resp8a.ErrorOccured)
            {
                Console.WriteLine(resp8a.ErrorMessage);
            }
            else
            {
                Console.WriteLine("The In Progress tasks column is empty and we got: ");
            }
            Response resp8b = JsonSerializer.Deserialize<Response>(boardService.InProgressTasks("hadas@gmail.com"));
            if (resp8b.ErrorOccured)
            {
                Console.WriteLine(resp8b.ErrorMessage);
            }
            else
            {
                Console.WriteLine("In progress tasks test failed");
            }

        }


    }
}
