using IntroSE.ForumSystem.Backend.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class BoardService
    {
        public Response CreateBoard(string email, string name)
        {
            return new Response("functuion isn't implemented yet");
        }

        public Response LimitColumn(string email, string boardName, int columnOrdinal, int limit)
        {
            return new Response("functuion isn't implemented yet");
        }

        public Response GetColumnLimit(string email, string boardName, int columnOrdinal)
        {
            return new Response("functuion isn't implemented yet");
        }

        public Response GetColumnName(string email, string boardName, int columnOrdinal)
        {
            return new Response("functuion isn't implemented yet");
        }

        public Response GetColumn(string email, string boardName, int columnOrdinal)
        {
            return new Response("functuion isn't implemented yet");
        }

        public Response DeleteBoard(string email, string name)
        {
            return new Response("functuion isn't implemented yet");
        }

        public Response InProgressTasks(string email)
        {
            return new Response("functuion isn't implemented yet");
        }

        public Response AddTask(string email, string boardName, string title, string description, DateTime dueDate)
        {
            return new Response("functuion isn't implemented yet");
        }

    }
}
