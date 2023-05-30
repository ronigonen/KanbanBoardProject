using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Reflection;
using log4net;
using log4net.Config;
using IntroSE.Kanban.Backend.BuisnessLayer;
using IntroSE.Kanban.Backend.DataAccessLayer;
using System.Linq.Expressions;

public class Board
{
    private BoardDTO bdto;
    private string name;
    private Dictionary<int, Task> backLogTasks;
    private Dictionary<int, Task> inProgressTasks;
    private Dictionary<int, Task> doneTasks;
    private int TaskId;
    private int backLogMax;
    private int inProgressMax;
    private int doneMax;
    private int boardID;
    private string ownerEmail;
    private UserInProgressTasks inProgressUseru;


    public Board(UserInProgressTasks u, string name, User user)
    {
        this.bdto = new BoardDTO(u.UserInProgressTasksDTO, name, user); 
        this.name = name;
        backLogTasks = new Dictionary<int, Task>();
        inProgressTasks = new Dictionary<int, Task>();
        doneTasks = new Dictionary<int, Task>();
        this.TaskId = 0;
        backLogMax = -1;
        inProgressMax = -1;
        doneMax = -1;
        InProgressUser = u;
        boardID = 0;
        ownerEmail = user.Email;
    }

    public Board(BoardDTO bdto)
    {
        this.bdto = bdto;
        this.name = bdto.Name;
        this.backLogTasks = bdto.BackLogTasks;
        this.inProgressTasks = bdto.InProgressTasks;
        this.doneTasks = bdto.DoneTasks;
        this.TaskId = bdto.TaskId;
        this.backLogMax = bdto.BackLogMax;
        this.inProgressMax = bdto.InProgressMax;
        this.doneMax = bdto.DoneMax;
        this.inProgressUser = bdto.InProgressUser;
        this.boardID = bdto.BoardId;
        this.ownerEmail = bdto.OwnerEmail;
    }

    public string Name { get =>  name; set => name = value; }
    public Dictionary<int, Task> BackLogTasks {  get => backLogTasks; set => backLogTasks = value; }
    public Dictionary<int, Task> InProgressTasks { get => inProgressTasks; set => inProgressTasks = value; }
    public Dictionary<int,Task> DoneTasks { get => doneTasks; set => doneTasks = value; }   
    public int BackLogMax { get => backLogMax; set => backLogMax = value; }
    public int DoneMax { get => doneMax; set => doneMax = value; }
    public UserInProgressTasks InProgressUser { get => inProgressUser; set => inProgressUser = value; }

    public Task getTask(int taskId)
    {
        return backLogTasks[taskId];
    }

    public void AddTask(User user, DateTime dueDate, string title, string description, DateTime creationTime)
    {
        if (backLogTasks.Count() == backLogMax)
        {
            throw new KanbanException("The Back Log column is full");
        }
        if (!user.GetBoards().ContainsKey(name))
            throw new KanbanException("the user is not a member.");
        else
        {
            Task toAdd = new Task(TaskId, creationTime, dueDate, title, description);
            backLogTasks.Add(TaskId, toAdd);
            TaskId = TaskId+1; //uniqe id
            try
            {
                bdto.addTask(toAdd.Tdto);
            }
            catch (Exception ex)
            {
                backLogTasks.Remove(TaskId);
                throw ex;
            }
        }
    }

    public void LimitColumn(int columnOrdinal, int max)
    {
        if (columnOrdinal==0)
        {
            backLogMax = max;
            bdto.BackLogMax=max;
        }
        else if (columnOrdinal == 1)
        {
            inProgressMax = max;
            bdto.InProgressMax=max;
        }
        else 
        {
            doneMax = max;
            bdto.DoneMax=max;
        }
    }

    public int GetColumnLimit(int columnOrdinal)
    {
        if (columnOrdinal == 0)
        {
            return backLogMax;
        }
        else if (columnOrdinal == 1)
        {
            return inProgressMax;
        }
        else
        {
            return doneMax;
        }
    }
     
    public string GetColumnName(int columnOrdinal)
    {
        if (columnOrdinal == 0)
        {
            return "backlog";
        }
        else if (columnOrdinal == 1)
        {
            return "in progress";
        }
        else
        {
            return "done";
        }

    }

    public List<Task> GetColumn(int columnOrdinal)
    {
        if (columnOrdinal == 0)
        {
            return new List<Task>(backLogTasks.Values.ToList());
        }
        else if (columnOrdinal == 1)
        {
            return new List<Task>(inProgressTasks.Values.ToList());
        }
        else if (columnOrdinal == 2)
        {
            return new List<Task>(doneTasks.Values.ToList());
        }
        else
        {
            throw new KanbanException("Invalid column ordinal");
        }
    }


    public void UpdateTaskDueDate(int taskId, int columnOrdinal, DateTime dueDate)
    {
        if (columnOrdinal==0) {
            if(!backLogTasks.ContainsKey(taskId))
                throw new KanbanException("Invalid taskId");
            backLogTasks[taskId].DueDate = dueDate;
        }
        else if (columnOrdinal==1) {
            if(!inProgressTasks.ContainsKey(taskId))
                throw new KanbanException("Invalid taskId");
            inProgressTasks[taskId].DueDate= dueDate;
        }
        else {
            throw new KanbanException("Invalid taskId");
        }
    }

    public void UpdateTaskTitle(int taskId, int columnOrdinal, string title)
    {
        if (title.Length < 0 || title.Length > 50)
        {
            throw new KanbanException("Invalid title- max 50 characters.");
        }
        if (columnOrdinal == 0)
        {
            if (!backLogTasks.ContainsKey(taskId))
                throw new KanbanException("Invalid taskId");
            backLogTasks[taskId].Title= title;
        }
        else if (columnOrdinal == 1)
        {
            if (!inProgressTasks.ContainsKey(taskId))
                throw new KanbanException("Invalid taskId");
            inProgressTasks[taskId].Title=title;
        }
        else
        {
            throw new KanbanException("Invalid taskId");
        }
    }

    public void UpdateTaskDescription(int taskId, int columnOrdinal, string description)
    {
        if (description.Length > 300)
        {
            throw new KanbanException("Invalid description- max 300 characters.");
        }
        else if (columnOrdinal == 0)
        {
            if (!backLogTasks.ContainsKey(taskId))
                throw new KanbanException("Invalid taskId");
            backLogTasks[taskId].Description= description;
        }
        else if (columnOrdinal == 1)
        {
            if (!inProgressTasks.ContainsKey(taskId))
                throw new KanbanException("Invalid taskId");
            inProgressTasks[taskId].Description= description;
        }
        else
        {
                throw new KanbanException("Invalid taskId");
        }
    }

    public void AdvanceTask(string email, int columnOrdinal, int taskId)
    {
        if (columnOrdinal == 0)
        {
            if (!backLogTasks.ContainsKey(taskId))
            {
                throw new KanbanException("Invalid taskId");
            }
            if (inProgressTasks.Count() == inProgressMax)
            {
                throw new KanbanException("'In progress' column is full");
            }
            else
            {
                inProgressTasks.Add(taskId, backLogTasks[taskId]);
                inProgressUser.AddTasks(email, backLogTasks[taskId]);
                backLogTasks.Remove(taskId);
                Task task = inProgressTasks[taskId];
                task.Tdto.ColumnOrdinal = 1;
            }
        }
        else if (columnOrdinal == 1)
        {
            if (!inProgressTasks.ContainsKey(taskId))
            {
                throw new KanbanException("Invalid taskId");
            }
            if (doneTasks.Count() == doneMax)
            {
                throw new KanbanException("'Done' column is full");
            }
            else
            {
                doneTasks.Add(taskId, inProgressTasks[taskId]);
                inProgressTasks.Remove(taskId);
                Task task = doneTasks[taskId];
                inProgressUser.RemoveTasks(email, task);
                task.Tdto.ColumnOrdinal = 2;
            }
        }
        else
            throw new KanbanException("Not possible");
        }

    public void JoinBoard(string email, int boardID)
    {

    }

    public void LeaveBoard(string email, int boardID)
    {

    }

    public string GetBoardName(int boardID)
    {

    }
    public void TransferOwnership(string currentOwnerEmail, string newOwnewemail, string boardName)
    {

    }

    public void AssignTask(string email, string boardName, int columnOrdinal, int taskID, string emailAssignee)
    {

    }


}




