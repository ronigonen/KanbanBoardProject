using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public class BoardDTO
    {
        private BoardController boardController;
        private Boolean isPersisted;
        private string name;
        private Dictionary<int, Task> backLogTasks;
        private Dictionary<int, Task> inProgressTasks;
        private Dictionary<int, Task> doneTasks;
        private int taskId;
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

        public BoardDTO(UserInProgressTasks u, string name, User user)
        {
            this.boardController = new BoardController();
            this.isPersisted = false;
            this.name = name;
            this.backLogTasks = new Dictionary<int, Task>();
            this.inProgressTasks = new Dictionary<int, Task>();
            this.doneTasks = new Dictionary<int, Task>();
            taskId = 0;
            this.backLogMax = -1;
            this.inProgressMax = -1;
            this.doneMax = -1;
            this.inProgressUser = u;
            this.boardID = 0;
            this.ownerEmail = user.EMAIL;
        }

        public string Name { get => name; }
        public Dictionary<int, Task> BackLogTasks { get => backLogTasks; }
        public Dictionary<int, Task> InProgressTasks { get => inProgressTasks; }
        public Dictionary<int, Task> DoneTasks { get => doneTasks; }
        public int TaskId { get => taskId; }
        internal int BackLogMax { get => backLogMax;
            set
            {
                if (isPersisted)
                    BoardController.updateBackLogMax(value);
                backLogMax = value;
            }
        }
        internal int InProgressMax { get => inProgressMax;
            set
            {
                if (isPersisted)
                    BoardController.updateInProgressMax(value);
                InProgressMax = value;
            }
        }
        internal int DoneMax { get => doneMax;
            set
            {
                if (isPersisted)
                    BoardController.updateDoneMax(value);
                doneMax = value;
            }
        }
        public UserInProgressTasks InProgressUser { get => inProgressUser; }
        public int BoardId { get => boardID; }
        public string OwnerEmail { get => ownerEmail; }

        public void addTask(TaskDTO task)
        {
            task.BoardId = this.boardID;
            task.persist();
        }
    }
}
