using IntroSE.ForumSystem.Backend.ServiceLayer;
using IntroSE.Kanban.Backend.BuisnessLayer;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Reflection;
using log4net.Config;
using System.IO;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class UserService
    {
        private UserFacade uF;

        public UserService()
        {
            uF = new UserFacade();

        }
        
        public string Register(string email, string password)
        {
            try
            {
                uF.Register(email, password);
                LoggerService.log.Debug("Register complete");
                return JsonSerializer.Serialize(new Response());
            }
            catch (KanbanException ex)
            {
                return JsonSerializer.Serialize(new Response(ex.Message));
            }
            catch (Exception ex){
                LoggerService.log.Error("the register wasn't completed because of unexpected error");
                return JsonSerializer.Serialize(new Response($"An unexpected error occured: \n {ex.Message} \nplease contact"));
            }
            
        }
        public string Login(string email, string password)
        {
            try
            {
                uF.LogIn(email, password);
                LoggerService.log.Debug("login complete");
                return JsonSerializer.Serialize(new Response());
            }
            catch (KanbanException ex)
            {
                return JsonSerializer.Serialize(new Response(ex.Message));
            }
            catch (Exception ex)
            {
                LoggerService.log.Warn("the login wasn't completed because of unexpected error");
                return JsonSerializer.Serialize(new Response($"An unexpected error occured: \n {ex.Message} \nplease contact"));
            }
        }
        public string Logout(string email)
        {
            try
            {
                uF.LogOut(email);
                LoggerService.log.Debug("logout complete");
                return JsonSerializer.Serialize(new Response());
            }
            catch (KanbanException ex)
            {
                return JsonSerializer.Serialize(new Response(ex.Message));
            }
            catch (Exception ex)
            {
                LoggerService.log.Error("the logout wasn't completed because of unexpected error");
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
            catch (KanbanException ex)
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
