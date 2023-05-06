using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class WrapperClass
    {
        public UserService UserService;
        public BoardService boardService;
        public TaskService TaskService;


        public WrapperClass() {
            UserService = new UserService();
            boardService = new BoardService(UserService.uF);
            TaskService = new TaskService(boardService.bF);
        }
    }
}
