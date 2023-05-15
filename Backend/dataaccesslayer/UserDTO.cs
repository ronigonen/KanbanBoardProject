using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    internal class UserDTO
    {
        private UserController userController;
        private Boolean isPersisted;
        private string password;
        private string email;
        private bool loggedIn;
        private Dictionary<string, Board> boards;
    }
}
