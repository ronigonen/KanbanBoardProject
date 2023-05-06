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

public class Board
{
    private string name;
    private Dictionary<int, Task> backLogTasks;
    private Dictionary<int, Task> inProgressTasks;
    private Dictionary<int, Task> doneTasks;
    private int TaskId;
    private int backLogMax;
    private int inProgressMax;
    private int doneMax;
    private UserInProgressTasks inProgressUser;


    public Board(UserInProgressTasks u, string name, User user)
    {
        this.name = name;
        backLogTasks = new Dictionary<int, Task>();
        inProgressTasks = new Dictionary<int, Task>();
        doneTasks = new Dictionary<int, Task>();
        TaskId = 0;
        backLogMax = -1;
        inProgressMax = -1;
        doneMax = -1;
        inProgressUser=u;
    }

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
        else
        {
            backLogTasks.Add(TaskId, new Task(TaskId, creationTime, dueDate, title, description));
            TaskId = TaskId+1; //uniqe id
        }

    }

    public void LimitColumn(int columnOrdinal, int max)
    {
        if (columnOrdinal==1)
        {
            backLogMax = max;
        }
        else if (columnOrdinal == 2)
        {
            inProgressMax = max;
        }
        else 
        {
            doneMax = max;
        }
    }

    public int GetColumnLimit(int columnOrdinal)
    {
        if (columnOrdinal == 1)
        {
            return backLogMax;
        }
        else if (columnOrdinal == 2)
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
        if (columnOrdinal == 1)
        {
            return "Back Log";
        }
        else if (columnOrdinal == 2)
        {
            return "In Progress";
        }
        else
        {
            return "Done";
        }

    }

    public List<Task> GetColumn(int columnOrdinal)
    {
        if (columnOrdinal == 1)
        {
            return new List<Task>(backLogTasks.Values.ToList());
        }
        else if (columnOrdinal == 2)
        {
            return new List<Task>(inProgressTasks.Values.ToList());
        }
        else if (columnOrdinal == 3)
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
        if (columnOrdinal==1) {
            if(!backLogTasks.ContainsKey(taskId))
                throw new KanbanException("Invalid taskId");
            backLogTasks[taskId].UpdateTaskDueDate(dueDate);
        }
        else if (columnOrdinal==2) {
            if(!inProgressTasks.ContainsKey(taskId))
                throw new KanbanException("Invalid taskId");
            inProgressTasks[taskId].UpdateTaskDueDate(dueDate);
        }
        else {
            throw new KanbanException("Invalid taskId");
        }
    }

    public void UpdateTaskTitle(int taskId, int columnOrdinal, string title)
    {
        if (columnOrdinal == 1)
        {
            if (!backLogTasks.ContainsKey(taskId))
                throw new KanbanException("Invalid taskId");
            backLogTasks[taskId].UpdateTaskTitle(title);
        }
        else if (columnOrdinal == 2)
        {
            if (!inProgressTasks.ContainsKey(taskId))
                throw new KanbanException("Invalid taskId");
            inProgressTasks[taskId].UpdateTaskTitle(title);
        }
        else
        {
            throw new KanbanException("Invalid taskId");
        }
    }

    public void UpdateTaskDescription(int taskId, int columnOrdinal, string description)
    {
        if (columnOrdinal == 1)
        {
            if (!backLogTasks.ContainsKey(taskId))
                throw new KanbanException("Invalid taskId");
            backLogTasks[taskId].UpdateTaskDescription(description);
        }
        else if (columnOrdinal == 2)
        {
            if (!inProgressTasks.ContainsKey(taskId))
                throw new KanbanException("Invalid taskId");
            inProgressTasks[taskId].UpdateTaskDescription(description);
        }
        else
        {
                throw new KanbanException("Invalid taskId");
        }
    }

    public void AdvanceTask(string email, int columnOrdinal, int taskId)
    {
        if (columnOrdinal == 1)
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
            }
        }
        else if (columnOrdinal == 2)
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
            }
        }
        else
            throw new KanbanException("Not possible");
        }
    }




