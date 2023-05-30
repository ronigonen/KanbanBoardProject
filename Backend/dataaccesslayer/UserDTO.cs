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
        private List<BoardDTO> boards;

        public UserDTO(string password, string email)
        {
            this.userController = new UserController();
            this.password = password;
            this.email = email;
            this.isPersisted = false;
            boards = userController.getAllBoards();
            try
            {
                persist();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message); //throw special exception
            }
        }

        internal List<BoardDTO> Boards { get => boards; }
        internal string Email { get => email; }
        internal string Password { get => password; }

        public void persist()
        {
            userController.insert(this);
            isPersisted = true;
        }

        public void delete()
        {
            userController.delete(this);
            isPersisted = true;
        }

    }
}
