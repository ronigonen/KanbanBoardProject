using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Reflection;
using log4net;
using log4net.Config;


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

    public void LimitColumn(string columnName, int max)
    {
        if (columnName.Equals("backLog"))
        {
            backLogTasks.EnsureCapacity(max);
            backLogMax = max;
        }
        else if (columnName.Equals("inProgress"))
        {
            inProgressTasks.EnsureCapacity(max);
            inProgressMax = max;
        }
        else if (columnName.Equals("done"))
        {
            doneTasks.EnsureCapacity(max);
            doneMax = max;
        }
        else
        {
            throw new Exception("Invalid column name");
        }
    }

    public int GetColumnLimit(string columnName)
    {
        if (columnName.Equals("backLog"))
        {
            return backLogMax;
        }
        else if (columnName.Equals("inProgress"))
        {
            return inProgressMax;
        }
        else if (columnName.Equals("done"))
        {
            return doneMax;
        }
        else
        {
            throw new Exception("Invalid column name");
        }
    }


    public List<Task> GetColumn(string columnName)
    {
        if (columnName.Equals("backLog"))
        {
            return new List<Task>(backLogTasks.Values.ToList());
        }
        else if (columnName.Equals("inProgress"))
        {
            return new List<Task>(inProgressTasks.Values.ToList());
        }
        else if (columnName.Equals("done"))
        {
            return new List<Task>(doneTasks.Values.ToList());
        }
        else
        {
            throw new Exception("Invalid column name");
        }
    }


    public void UpdateTaskDueDate(int taskId, DateTime dueDate)
    {
        if (backLogTasks.ContainsKey(taskId)) {
            backLogTasks[taskId].UpdateTimeDueDate(dueDate);
        }

        else if (inProgressTasks.ContainsKey(taskId)) {
            inProgressTasks[taskId].UpdateTimeDueDate(dueDate);
        }

        else if (doneTasks.ContainsKey(taskId)) {
            doneTasks[taskId].UpdateTimeDueDate(dueDate);
        }
        else
        {
            throw new Exception("Invalid taskId");
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
            throw new Exception("Invalid taskId");
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
            throw new Exception("Invalid taskId");
        }
    }

    public void AdvanceTask(int taskId)
    {
        if (backLogTasks.ContainsKey(taskId)) {
            inProgressTasks.Add(taskId, backLogTasks[taskId]);
            backLogTasks.Remove(taskId);
        }

        else if (inProgressTasks.ContainsKey(taskId)) {
            doneTasks.Add(taskId, inProgressTasks[taskId]);
            inProgressTasks.Remove(taskId);
        }

        else if (doneTasks.ContainsKey(taskId)) {
            doneTasks.Remove(taskId);
        }
        else
        {
            throw new Exception("Invalid taskId");
        }
    }
}



