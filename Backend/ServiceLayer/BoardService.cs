using IntroSE.ForumSystem.Backend.ServiceLayer;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class BoardService
    {
        private BoardFacade bF;
        public string CreateBoard(string email, string name)
        {
            try
            {
                bF.CreateBoard(email, name);
                return JsonSerializer.Serialize(new Response());
            }
            catch (KanbanExeption ex)
            {
                return JsonSerializer.Serialize(new Response(ex.Message));
            }
            catch (Exception ex)
            {
                return JsonSerializer.Serialize(new Response($"An unexpected error occured: \n {ex.Message} \nplease contact"));
            }
        }

        public string LimitColumn(string email, string boardName, string columnName, int limit)
        {
            try
            {
                bF.LimitColumn(email, boardName, columnName, limit);
                return JsonSerializer.Serialize(new Response());
            }
            catch (KanbanExeption ex)
            {
                return JsonSerializer.Serialize(new Response(ex.Message));
            }
            catch (Exception ex)
            {
                return JsonSerializer.Serialize(new Response($"An unexpected error occured: \n {ex.Message} \nplease contact"));
            }
        }

        public string GetColumnLimit(string email, string boardName, string columnName)
        {
            try
            {
                bF.GetColumnLimit(email, boardName, columnName);
                return JsonSerializer.Serialize(new Response());
            }
            catch (KanbanExeption ex)
            {
                return JsonSerializer.Serialize(new Response(ex.Message));
            }
            catch (Exception ex)
            {
                return JsonSerializer.Serialize(new Response($"An unexpected error occured: \n {ex.Message} \nplease contact"));
            }
        }

  
        public string GetColumn(string email, string boardName, string columnName)
        {
            try
            {
                bF.GetColumn(email, boardName, columnName);
                return JsonSerializer.Serialize(new Response());
            }
            catch (KanbanExeption ex)
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
                return JsonSerializer.Serialize(new Response());
            }
            catch (KanbanExeption ex)
            {
                return JsonSerializer.Serialize(new Response(ex.Message));
            }
            catch (Exception ex)
            {
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
            catch (KanbanExeption ex)
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
                return JsonSerializer.Serialize(new Response());
            }
            catch (KanbanExeption ex)
            {
                return JsonSerializer.Serialize(new Response(ex.Message));
            }
            catch (Exception ex)
            {
                return JsonSerializer.Serialize(new Response($"An unexpected error occured: \n {ex.Message} \nplease contact"));
            }
        }

    }
}
