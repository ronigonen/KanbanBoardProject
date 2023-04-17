using IntroSE.ForumSystem.Backend.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class TaskService
    {
     
        public Response UpdateTaskDueDate(string email, string boardName, int columnOrdinal, int taskId, DateTime dueDate)
        {
            return new Response("functuion isn't implemented yet");
        }

        public Response UpdateTaskTitle(string email, string boardName, int columnOrdinal, int taskId, string title)
        {
            return new Response("functuion isn't implemented yet");
        }

        public Response UpdateTaskDescription(string email, string boardName, int columnOrdinal, int taskId, string description)
        {
            return new Response("functuion isn't implemented yet");
        }

        public Response AdvanceTask(string email, string boardName, int columnOrdinal, int taskId)
        {
            return new Response("functuion isn't implemented yet");
        }


    }
}
