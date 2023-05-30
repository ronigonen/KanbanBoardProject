﻿using System;
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

        public BoardDTO(UserInProgressTasksDTO u, string name, User user)
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
            this.boardID = 0;
            this.ownerEmail = user.Email;
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
            task.BoardID=this.boardID;
            task.persist();
            Tasks.Add(task); // if persist failed, it won't reach to this line
        }
    }
}