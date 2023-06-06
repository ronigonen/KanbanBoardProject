using IntroSE.Kanban.Backend.BuisnessLayer;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public class UserDTO
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
            boards = userController.SelectBoardsByEmail(email);
            persist();
        }

        internal List<BoardDTO> Boards { get => boards; }
        internal string Email { get => email; }
        internal string Password { get => password; }

        public void persist()
        {
            try
            {
                userController.Insert(this);
                isPersisted = true;
            }
            catch (KanbanDataException e)
            { 
                throw new KanbanDataException("didn't added to the data base");
            }
            catch (Exception ex)
            {
                throw new Exception(($"An unexpected error occured: \n {ex.Message} \nplease contact"));
            }
        }

        public void delete()
        {
            try
            {
                userController.Delete(this);
                isPersisted = true;
            }
            catch (KanbanDataException e)
            {
                throw new KanbanDataException("didn't deleted to the data base");
            }
            catch (Exception ex)
            {
                throw new Exception(($"An unexpected error occured: \n {ex.Message} \nplease contact"));
            }
        }

    }

    }
}
