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
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public UserService()
        {
            uF = new UserFacade();
            var logRespository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRespository,new FileInfo("log4net.config"));
            log.Info("starting log!");
        }
        
        public string Register(string email, string password)
        {
            try
            {
                uF.Register(email, password);
                return JsonSerializer.Serialize(new Response());
            }
            catch (KanbanException ex)
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
            catch (KanbanException ex)
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
            catch (KanbanException ex)
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
