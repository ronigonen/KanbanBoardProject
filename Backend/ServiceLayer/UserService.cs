using IntroSE.ForumSystem.Backend.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class UserService
    {
        private UserFacade uF;
        
        public string Register(string email, string password)
        {
            try
            {
                uF.Register(email, password);
                return JsonSerializer.Serialize(new Response());
            }
            catch (KanbanExeption ex)
            {
                return JsonSerializer.Serialize(new Response(ex.Message));
            }
            catch (Exception ex){
                return JsonSerializer.Serialize(new Response($"An unexpected error occured: \n {ex.Message} \nplease contact"));
            }
            
        }
        public string Login(string email, string password)
        {
            try
            {
                uF.LogIn(email, password);
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
        public string Logout(string email)
        {
            try
            {
                uF.LogOut(email);
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
        public string GetUser(string email)
        {
            try
            {
                uF.GetUser(email);
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
