using IntroSE.ForumSystem.Backend.ServiceLayer;
using IntroSE.Kanban.Backend.BuisnessLayer;
using log4net;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Reflection;
using log4net.Config;
using System.IO;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class TaskService
    {
        private BoardFacade bF;


        public TaskService()
        {
            bF = new BoardFacade();

        }
     
        public string UpdateTaskDueDate(string email, string boardName, int columnOrdinal, int taskId, DateTime dueDate)
        {
            try
            {
                bF.UpdateTaskDueDate(email, boardName, taskId, dueDate);
                LoggerService.log.Debug("task due date update completed");
                return JsonSerializer.Serialize(new Response());
            }
            catch (KanbanException ex)
            {
                return JsonSerializer.Serialize(new Response(ex.Message));
            }
            catch (Exception ex)
            {
                return JsonSerializer.Serialize(new Response($"An unexpected error occured: \n {ex.Message} \nplease contact"));
            }
        }

        public string UpdateTaskTitle(string email, string boardName, int columnOrdinal, int taskId, string title)
        {
            try
            {
                bF.UpdateTaskTitle(email, boardName, taskId, title);
                LoggerService.log.Debug("task title update completed");
                return JsonSerializer.Serialize(new Response());
            }
            catch (KanbanException ex)
            {
                return JsonSerializer.Serialize(new Response(ex.Message));
            }
            catch (Exception ex)
            {
                return JsonSerializer.Serialize(new Response($"An unexpected error occured: \n {ex.Message} \nplease contact"));
            }
        }

        public string UpdateTaskDescription(string email, string boardName, int columnOrdinal, int taskId, string description)
        {
            try
            {
                bF.UpdateTaskTitle(email, boardName, taskId, description);
                LoggerService.log.Debug("task description update completed");
                return JsonSerializer.Serialize(new Response());
            }
            catch (KanbanException ex)
            {
                return JsonSerializer.Serialize(new Response(ex.Message));
            }
            catch (Exception ex)
            {
                return JsonSerializer.Serialize(new Response($"An unexpected error occured: \n {ex.Message} \nplease contact"));
            }
        }

        public string AdvanceTask(string email, string boardName, int columnOrdinal, int taskId)
        {
            try
            {
                bF.AdvanceTask(email, boardName, columnOrdinal, taskId);
                LoggerService.log.Debug("Advanced Task completed");
                return JsonSerializer.Serialize(new Response());
            }
            catch (KanbanException ex)
            {
                return JsonSerializer.Serialize(new Response(ex.Message));
            }
            catch (Exception ex)
            {
                LoggerService.log.Warn("the task wasn't advanced because of unexpected error");
                return JsonSerializer.Serialize(new Response($"An unexpected error occured: \n {ex.Message} \nplease contact"));
            }
        }


    }
}
