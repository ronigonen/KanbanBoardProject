using IntroSE.Kanban.Backend.BuisnessLayer;
using System;
using System.Buffers;
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
        private List<TaskDTO> Tasks;
        private int taskId;
        private int backLogMax;
        private int inProgressMax;
        private int doneMax;
        private UserInProgressTasksDTO inProgressUser;
        private int boardID;
        private string ownerEmail;

<<<<<<< HEAD
        public BoardDTO(string name1, List<TaskDTO> tasks1, int taskId1, int backLogMax1, int inProgressMax1, int doneMax1, Dictionary<string,List<TaskDTO>> inProgressUser1, int boardID1, string ownerEmail1)
        {
            this.boardController = new BoardController();
            this.isPersisted = true;
            this.name = name1;
            this.Tasks = tasks1;
            this.taskId = taskId1;
            this.backLogMax = backLogMax1;
            this.inProgressMax = inProgressMax1;
            this.doneMax = doneMax1;
            this.inProgressUser = new UserInProgressTasksDTO();
            this.boardID = boardID1;
            this.ownerEmail = ownerEmail1;
            foreach (TaskDTO t in tasks1)
            {
                if (t.ColumnOrdinal == 1)
                {
                    inProgressUser.UserTasks[t.EmailAssignee].Add(t);
                }
            }
        }

        public BoardDTO(UserInProgressTasks u1, string name1, User user1, int boardID1)
        {
            this.boardController = new BoardController();
            this.isPersisted = false;
            this.name = name1;
            this.Tasks = new List<TaskDTO>();
            this.taskId = 0;
            this.backLogMax = -1;
            this.inProgressMax = -1;
            this.doneMax = -1;
            this.inProgressUser = u1.UserInProgressTasksDTO;
            this.boardID = boardID1;
            this.ownerEmail = user1.Email;
            persist();
        }


        public void persist()
        {
            try
            {
                boardController.insert(this);
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
                boardController.delete(this);
                isPersisted = false;
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


        public BoardDTO(UserInProgressTasksDTO u, string name, User user, int BoardID)
        {
            this.boardController = new BoardController();
            this.isPersisted = false;
            this.name = name;
            this.Tasks = new List<TaskDTO>();
            taskId = 0;
            this.backLogMax = -1;
            this.inProgressMax = -1;
            this.doneMax = -1;
            this.inProgressUser = u;
            this.boardID = BoardID;
            this.ownerEmail = user.Email;
        }




        public string Name { get => name; }
        public int TaskId { get => taskId; }
        public List<TaskDTO> tasks { get => Tasks; }
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
        
        
        public UserInProgressTasksDTO InProgressUser { get => inProgressUser; }
        public int BoardId { get => boardID; }
        public string OwnerEmail { get => ownerEmail; }

        public void addTask(TaskDTO task)
        {
            task.BoardID=this.boardID;
            task.persist();
            Tasks.Add(task); // if persist failed, it won't reach to this line
        }
    }
}
