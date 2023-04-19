using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using IntroSE.ForumSystem.Backend.ServiceLayer;
using IntroSE.Kanban.Backend.ServiceLayer;

namespace BackendTest
{

    class UserServiceTest
    {
        private readonly UserService userService;

        public UserServiceTest(UserService userService)
        {
            this.userService = userService;
        }

        public void runTests()
        {
            Response res1a = JsonSerializer.Deserialize<Response>(userService.Register("hadas12@gmail.com", "Aa012345"));
            if (res1a.ErrorOccured)
            {
                Console.WriteLine(res1a.ErrorMessage);
            }
            else
            {
                Console.WriteLine("hadas12@gmail.com registered successfully.");
            }
            Response res1b = JsonSerializer.Deserialize<Response>(userService.Register("roni12@gmail.com", "0123"));
            if (res1b.ErrorOccured)
            {
                Console.WriteLine(res1b.ErrorMessage);
            }
            else
            {
                Console.WriteLine("Register test failed.");
            }



            Response res2a = JsonSerializer.Deserialize<Response>(userService.Login("hadas12@gmail.com", "Aa012345"));
            if (res2a.ErrorOccured)
            {
                Console.WriteLine(res2a.ErrorMessage);
            }
            else
            {
                Console.WriteLine("hadas12@gmail.com loggedIn successfully.");
            }
            Response res2b = JsonSerializer.Deserialize<Response>(userService.Login("hadas12@gmail.com", "Aa012"));
            if (res2b.ErrorOccured)
            {
                Console.WriteLine(res2b.ErrorMessage);
            }
            else
            {
                Console.WriteLine("LogIn test failed.");
            }



            Response res3a = JsonSerializer.Deserialize<Response>(userService.Logout("hadas12@gmail.com"));
            if (res3a.ErrorOccured)
            {
                Console.WriteLine(res3a.ErrorMessage);
            }
            else
            {
                Console.WriteLine("hadas12@gmail.com loggedOut successfully.");
            }
            Response res3b = JsonSerializer.Deserialize<Response>(userService.Logout("noga@gmail.com"));
            if (res3b.ErrorOccured)
            {
                Console.WriteLine(res3b.ErrorMessage);
            }
            else
            {
                Console.WriteLine("LogOut test failed.");
            }


        }
    }
}
