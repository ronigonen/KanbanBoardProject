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

        public BoardDTO(bool isPersisted, string name, Dictionary<int, Task> backLogTasks, Dictionary<int, Task> inProgressTasks, Dictionary<int, Task> doneTasks, int taskId, int backLogMax, int inProgressMax, int doneMax, UserInProgressTasks inProgressUser, int boardID, string ownerEmail)
        {
            this.boardController = new BoardController();
            this.isPersisted = isPersisted;
            this.name = name;
            this.backLogTasks = backLogTasks;
            this.inProgressTasks = inProgressTasks;
            this.doneTasks = doneTasks;
            TaskId = taskId;
            this.backLogMax = backLogMax;
            this.inProgressMax = inProgressMax;
            this.doneMax = doneMax;
            this.inProgressUser = inProgressUser;
            this.boardID = boardID;
            this.ownerEmail = ownerEmail;
        }

        public void AddBoard()
        {
            boardController.insert(this);
            isPersisted = true;
        }

        public void delete()
        {
            boardController.delete(this);
            isPersisted = false;
        }
    }
}
