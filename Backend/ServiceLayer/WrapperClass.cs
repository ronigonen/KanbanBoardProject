using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    internal class WrapperClass
    {
        internal UserService UserService;
        internal BoardService boardService;
        internal TaskService TaskService;


        public WrapperClass() {
            UserService = new UserService();
            boardService = new BoardService(UserService.uF);
            TaskService = new TaskService(boardService.bF);
        }
    }
}
