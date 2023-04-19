using IntroSE.ForumSystem.Backend.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class TaskService
    {
     
        public string UpdateTaskDueDate(string email, string boardName, int columnOrdinal, int taskId, DateTime dueDate)
        {
            return JsonSerializer.Serialize(new Response("function isn't implemented yet"));
        }

        public string UpdateTaskTitle(string email, string boardName, int columnOrdinal, int taskId, string title)
        {
            return JsonSerializer.Serialize(new Response("function isn't implemented yet"));
        }

        public string UpdateTaskDescription(string email, string boardName, int columnOrdinal, int taskId, string description)
        {
            return JsonSerializer.Serialize(new Response("function isn't implemented yet"));
        }

        public string AdvanceTask(string email, string boardName, int columnOrdinal, int taskId)
        {
            return JsonSerializer.Serialize(new Response("function isn't implemented yet"));
        }


    }
}
