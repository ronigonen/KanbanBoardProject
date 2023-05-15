using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    internal class BoardDTO
    {
        private BoardController boardController;
        private Boolean isPersisted;
        private string name;
        private Dictionary<int, Task> backLogTasks;
        private Dictionary<int, Task> inProgressTasks;
        private Dictionary<int, Task> doneTasks;
        private int TaskId;
        private int backLogMax;
        private int inProgressMax;
        private int doneMax;
        private UserInProgressTasks inProgressUser;
        private int boardID;
        private string ownerEmail;
    }
}
