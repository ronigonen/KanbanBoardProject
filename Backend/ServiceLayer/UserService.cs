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
        public string Register(string email, string password)
        {
            return JsonSerializer.Serialize(new Response("functuion isn't implemented yet"));
        }
        public string Login(string email, string password)
        {
            return JsonSerializer.Serialize(new Response("functuion isn't implemented yet"));
        }
        public string Logout(string email)
        {
            return JsonSerializer.Serialize(new Response("functuion isn't implemented yet"));
        }
    }
}
