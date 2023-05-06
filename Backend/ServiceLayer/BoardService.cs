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
using System.Xml.Linq;
using System.Reflection;
using log4net.Config;
using System.IO;
using log4net.Repository.Hierarchy;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class BoardService
    {
        private BoardFacade bF;


        public BoardService()
        {
            bF = new BoardFacade();


        }
        public string CreateBoard(string email, string name)
        {
            try
            {
                bF.CreateBoard(email, name);
                LoggerService.log.Debug("Board created");
                return JsonSerializer.Serialize(new Response());
            }
            catch (KanbanException ex)
            {
                return JsonSerializer.Serialize(new Response(ex.Message));
            }
            catch (Exception ex)
            {
                LoggerService.log.Fatal("the board wasn't created because of unexpected error");
                return JsonSerializer.Serialize(new Response($"An unexpected error occured: \n {ex.Message} \nplease contact"));
            }
        }

        public string LimitColumn(string email, string boardName, int columnOrdinal, int limit)
        {
            try
            {
                bF.LimitColumn(email, boardName, columnOrdinal, limit);
                LoggerService.log.Debug("column limited");
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

        public string GetColumnLimit(string email, string boardName, int columnOrdinal)
        {
            try
            {
                bF.GetColumnLimit(email, boardName, columnOrdinal);
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
        public string GetColumnName(string email, string boardName, int columnOrdinal)
        {
            try
            {
                bF.GetColumnName(email, boardName, columnOrdinal);
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

  
        public string GetColumn(string email, string boardName, int columnOrdinal)
        {
            try
            {
                bF.GetColumn(email, boardName, columnOrdinal);
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

        public string DeleteBoard(string email, string boardName)
        {
            try
            {
                bF.DeleteBoard(email, boardName);
                LoggerService.log.Debug("Board deleted");
                return JsonSerializer.Serialize(new Response());
            }
            catch (KanbanException ex)
            {
                return JsonSerializer.Serialize(new Response(ex.Message));
            }
            catch (Exception ex)
            {
                LoggerService.log.Fatal("the board wasn't deleted because of unexpected error");
                return JsonSerializer.Serialize(new Response($"An unexpected error occured: \n {ex.Message} \nplease contact"));
            }
        }

        public string InProgressTasks(string email)
        {
            try
            {
                bF.GetInProgress(email);
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

        public string AddTask(string email, string boardName, string title, string description, DateTime dueDate, DateTime creationTime)
        {
            try
            {
                bF.AddTask(email, boardName, title, description, dueDate, creationTime);
                LoggerService.log.Debug("Task added");
                return JsonSerializer.Serialize(new Response());
            }
            catch (KanbanException ex)
            {
                return JsonSerializer.Serialize(new Response(ex.Message));
            }
            catch (Exception ex)
            {
                LoggerService.log.Warn("Task wasn't added because of an unexpected error");
                return JsonSerializer.Serialize(new Response($"An unexpected error occured: \n {ex.Message} \nplease contact"));
            }
        }

    }
}
