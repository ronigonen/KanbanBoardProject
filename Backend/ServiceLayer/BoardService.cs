using IntroSE.ForumSystem.Backend.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class BoardService
    {
        public string CreateBoard(string email, string name)
        {
            return JsonSerializer.Serialize(new Response("functuion isn't implemented yet"));
        }

        public string LimitColumn(string email, string boardName, int columnOrdinal, int limit)
        {
            return JsonSerializer.Serialize(new Response("functuion isn't implemented yet"));
        }

        public string GetColumnLimit(string email, string boardName, int columnOrdinal)
        {
            return JsonSerializer.Serialize(new Response("functuion isn't implemented yet"));
        }

        public string GetColumnName(string email, string boardName, int columnOrdinal)
        {
            return JsonSerializer.Serialize(new Response("functuion isn't implemented yet"));
        }

        public string GetColumn(string email, string boardName, int columnOrdinal)
        {
            return JsonSerializer.Serialize(new Response("functuion isn't implemented yet"));
        }

        public string DeleteBoard(string email, string name)
        {
            return JsonSerializer.Serialize(new Response("functuion isn't implemented yet"));
        }

        public string InProgressTasks(string email)
        {
            return JsonSerializer.Serialize(new Response("functuion isn't implemented yet"));
        }

        public string AddTask(string email, string boardName, string title, string description, DateTime dueDate)
        {
            return JsonSerializer.Serialize(new Response("functuion isn't implemented yet"));
        }

    }
}
