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
                Console.WriteLine("Register test failed- the password invalid.");
            }
            Response res1c = JsonSerializer.Deserialize<Response>(userService.Register("hadas12@gmail.com", "Ab01234567"));
            if (res1c.ErrorOccured)
            {
                Console.WriteLine(res1c.ErrorMessage);
            }
            else
            {
                Console.WriteLine("Register test failed- email has been used.");
            }
            Response res1d = JsonSerializer.Deserialize<Response>(userService.Register(null, "Ab01234567"));
            if (res1d.ErrorOccured)
            {
                Console.WriteLine(res1d.ErrorMessage);
            }
            else
            {
                Console.WriteLine("Register test failed- email can't be null.");
            }
            Response res1e = JsonSerializer.Deserialize<Response>(userService.Register("roni12@gmail.com", "R012356r"));
            if (res1e.ErrorOccured)
            {
                Console.WriteLine(res1e.ErrorMessage);
            }
            else
            {
                Console.WriteLine("roni12@gmail.com registered successfully.");
            }




            Response res2a = JsonSerializer.Deserialize<Response>(userService.Logout("hadas12@gmail.com"));
            if (res2a.ErrorOccured)
            {
                Console.WriteLine(res2a.ErrorMessage);
            }
            else
            {
                Console.WriteLine("hadas12@gmail.com loggedOut successfully.");
            }
            Response res2b = JsonSerializer.Deserialize<Response>(userService.Logout("noga@gmail.com"));
            if (res2b.ErrorOccured)
            {
                Console.WriteLine(res2b.ErrorMessage);
            }
            else
            {
                Console.WriteLine("LogOut test failed- noga@gmail.com doesn't have a user.");
            }
            Response res2c = JsonSerializer.Deserialize<Response>(userService.Logout("roni12@gmail.com"));
            if (res2c.ErrorOccured)
            {
                Console.WriteLine(res2c.ErrorMessage);
            }
            else
            {
                Console.WriteLine("roni12@gmail.com loggedOut successfully.");
            }




            Response res3a = JsonSerializer.Deserialize<Response>(userService.Login("hadas12@gmail.com", "Aa012345"));
            if (res3a.ErrorOccured)
            {
                Console.WriteLine(res3a.ErrorMessage);
            }
            else
            {
                Console.WriteLine("hadas12@gmail.com loggedIn successfully.");
            }
            Response res3b = JsonSerializer.Deserialize<Response>(userService.Login("roni12@gmail.com", "R012356rvc"));
            if (res3b.ErrorOccured)
            {
                Console.WriteLine(res3b.ErrorMessage);
            }
            else
            {
                Console.WriteLine("LogIn test failed- this isn't the right password.");
            }
            Response res3c = JsonSerializer.Deserialize<Response>(userService.Login("roni12@gmail.com", "R012356r"));
            if (res3c.ErrorOccured)
            {
                Console.WriteLine(res3c.ErrorMessage);
            }
            else
            {
                Console.WriteLine("roni12@gmail.com loggedIn successfully.");
            }
        }
    }
}
