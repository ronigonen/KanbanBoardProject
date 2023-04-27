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


    public Board(string name, User user)
    {
        this.name = name;
        backLogTasks = new Dictionary<int, Task>();
        inProgressTasks = new Dictionary<int, Task>();
        doneTasks = new Dictionary<int, Task>();
        TaskId = 0;
        backLogMax = -1;
        inProgressMax = -1;
        doneMax = -1;
        inProgressUser=new UserInProgressTasks(user.GetEmail());
    }

    public Task getTask(int taskId)
    {
        return backLogTasks[taskId];
    }

    public void AddTask(User user, DateTime dueDate, string title, string description, DateTime creationTime)
    {
        backLogTasks.Add(TaskId, new Task(user, TaskId, creationTime, dueDate, title, description));
        TaskId = TaskId++; //uniqe id
    }

    public void LimitColumn(int columnOrdinal, int max)
    {
        if (columnOrdinal==1)
        {
            backLogTasks.EnsureCapacity(max);
            backLogMax = max;
        }
        else if (columnOrdinal == 2)
        {
            inProgressTasks.EnsureCapacity(max);
            inProgressMax = max;
        }
        else if (columnOrdinal == 3)
        {
            doneTasks.EnsureCapacity(max);
            doneMax = max;
        }
        else
        {
            throw new KanbanException("Invalid column name");
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
        else if (columnOrdinal == 3)
        {
            return doneMax;
        }
        else
        {
            throw new KanbanException("Invalid column name");
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
            throw new KanbanException("Invalid column name");
        }
    }


    public void UpdateTaskDueDate(int taskId, DateTime dueDate)
    {
        if (backLogTasks.ContainsKey(taskId)) {
            backLogTasks[taskId].UpdateTaskDueDate(dueDate);
        }

        else if (inProgressTasks.ContainsKey(taskId)) {
            inProgressTasks[taskId].UpdateTaskDueDate(dueDate);
        }

        else if (doneTasks.ContainsKey(taskId)) {
            doneTasks[taskId].UpdateTaskDueDate(dueDate);
        }
        else
        {
            throw new KanbanException("Invalid taskId");
        }
    }

    public void UpdateTaskTitle(int taskId, string title)
    {
        if (backLogTasks.ContainsKey(taskId)) {
            backLogTasks[taskId].UpdateTaskTitle(title);
        }

        else if (inProgressTasks.ContainsKey(taskId)) {
            inProgressTasks[taskId].UpdateTaskTitle(title);
        }

        else if (doneTasks.ContainsKey(taskId)) {
            doneTasks[taskId].UpdateTaskTitle(title);
        }
        else
        {
            throw new KanbanException("Invalid taskId");
        }
    }

    public void UpdateTaskDescription(int taskId, string description)
    {
        if (backLogTasks.ContainsKey(taskId)) {
            backLogTasks[taskId].UpdateTaskDescription(description);
        }

        else if (inProgressTasks.ContainsKey(taskId)) {
            inProgressTasks[taskId].UpdateTaskDescription(description);
        }

        else if (doneTasks.ContainsKey(taskId)) {
            doneTasks[taskId].UpdateTaskDescription(description);
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
            inProgressTasks.Add(taskId, backLogTasks[taskId]);
            backLogTasks.Remove(taskId);
            Task task = inProgressTasks[taskId];
            inProgressUser.AddTasks(email, task);
        }
        else if (columnOrdinal == 2) {
            if (!inProgressTasks.ContainsKey(taskId))
            {
                throw new KanbanException("Invalid taskId");
            }
            doneTasks.Add(taskId, inProgressTasks[taskId]);
            inProgressTasks.Remove(taskId);
            Task task = doneTasks[taskId];
            inProgressUser.RemoveTasks(email, task);
        }
        else {
            if (!doneTasks.ContainsKey(taskId))
            {
                throw new KanbanException("Invalid taskId");
            }
            doneTasks.Remove(taskId);
        }
    }
}



