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
        internal static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        public UserService()
        {
            uF = new UserFacade();
            var logRespository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRespository, new FileInfo("log4net.config"));
            log.Info("starting log!");
        }

        public UserFacade UF { get => uF; set => uF = value; }

        /// <summary>
        /// This method registers a new user to the system.
        /// </summary>
        /// <param name="email">The user email address, used as the username for logging the system.</param>
        /// <param name="password">The user password.</param>
        /// <returns>An empty response, unless an error occurs (see <see cref="UserService"/>)</returns>
        public string Register(string email, string password)
        {
            try
            {
                uF.Register(email, password);
                UserService.log.Debug("Register complete");
                return JsonSerializer.Serialize(new Response());
            }
            catch (KanbanException ex)
            {
                return JsonSerializer.Serialize(new Response(ex.Message));
            }
            catch (Exception ex){
                UserService.log.Error("the register wasn't completed because of unexpected error");
                return JsonSerializer.Serialize(new Response($"An unexpected error occured: \n {ex.Message} \nplease contact"));
            }
            
        }


        /// <summary>
        ///  This method logs in an existing user.
        /// </summary>
        /// <param name="email">The email address of the user to login</param>
        /// <param name="password">The password of the user to login</param>
        /// <returns>A response with the user's email, unless an error occurs (see <see cref="UserService"/>)</returns>
        public string Login(string email, string password)
        {
            try
            {
                UserService.log.Debug("login complete");
                return JsonSerializer.Serialize(new Response((Object) uF.LogIn(email, password)));
            }
            catch (KanbanException ex)
            {
                return JsonSerializer.Serialize(new Response(ex.Message));
            }
            catch (Exception ex)
            {
                UserService.log.Warn("the login wasn't completed because of unexpected error");
                return JsonSerializer.Serialize(new Response($"An unexpected error occured: \n {ex.Message} \nplease contact"));
            }
        }

        /// <summary>
        /// This method logs out a logged in user. 
        /// </summary>
        /// <param name="email">The email of the user to log out</param>
        /// <returns>An empty response, unless an error occurs (see <see cref="UserService"/>)</returns>
        public string Logout(string email)
        {
            try
            {
                uF.LogOut(email);
                UserService.log.Debug("logout complete");
                return JsonSerializer.Serialize(new Response());
            }
            catch (KanbanException ex)
            {
                return JsonSerializer.Serialize(new Response(ex.Message));
            }
            catch (Exception ex)
            {
                UserService.log.Error("the logout wasn't completed because of unexpected error");
                return JsonSerializer.Serialize(new Response($"An unexpected error occured: \n {ex.Message} \nplease contact"));
            }
        }

        /// <summary>
        /// This method finding the user with the matches email. 
        /// </summary>
        /// <param name="email">The email of the user to find</param>
        /// <returns>A response with a User, unless an error occurs (see <see cref="UserService"/>)</returns>
        public string GetUser(string email)
        {
            try
            {
                return JsonSerializer.Serialize(new Response(uF.GetUser(email)));
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

        public string GetUserBoards(string email)
        {
            return JsonSerializer.Serialize(new Response("not implemented yet."));
        }

        public string LoadData()
        {
            return JsonSerializer.Serialize(new Response("not implemented yet."));
        }

        public string DeleteData()
        {
            return JsonSerializer.Serialize(new Response("not implemented yet."));
        }
    }
}
