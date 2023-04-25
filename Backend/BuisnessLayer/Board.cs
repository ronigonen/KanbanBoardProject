using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

public class Board
{
    private string name;
    private List<User> members;
    private Dictionary<int, Task> backLogTasks;
    private Dictionary<int, Task> inProgressTasks;
    private Dictionary<int, Task> doneTasks;
    private int TaskId;



    public Board(string name, User user)
	{
        this.Name = name;
        members = new List<User>();
        members.Add(user);
        backLogTasks = new Dictionary<int, Task>();
        inProgressTasks = new Dictionary<int, Task>();
        doneTasks = new Dictionary<int, Task>();
        TaskId = 0;

    }

    public Task AddTask(User user, DateTime dueDate, string title, string description, string creationTime)
    {
        backLogTasks.Add(TaskId, new Task(user, TaskId, creationTime, dueDate, title, description));
        TaskId = TaskId++;
    }
    
    public void LimitColumn(string columnName, int max)
    {
        if (columnName == backLog)
        {
            backLogTasks.EnsureCapacity(max);
        }
        else if (columnName == inProgress)
        {
            inProgressTasks.EnsureCapacity(max);
        }
        else (columnName == done)
        {
            doneTasks.EnsureCapacity(max);
        }
    }

    public int GetColumnLimit(string columnName)
    {
        if (columnName == backLog)
        {
            return backLogTasks.EnsureCapacity;
        }
        else if (columnName == inProgress)
        {
            return inProgressTasks.EnsureCapacity;
        }
        else (columnName == done)
        {
            return doneTasks.EnsureCapacity;
        }
    }

    public List GetColumn(string columnName)
    {
        if (columnName == backLog)
        {
            return backLogTasks.Values;
        }
        else if (columnName == inProgress)
        {
            return inProgressTasks.Values;
        }
        else (columnName == done)
        {
            return doneTasks.Values;
        }
    }


    public Task UpdateTaskDueDate(User user, int taskId, DateTime dueDate)
    {
        if (backLogTasks.ContainsKey(taskId){
            return backLogTasks[taskId].UpdateTaskDueDate(dueDate);
        }
        
        else if (inProgressTasks.ContainsKey(taskId){
            return inProgressTasks[taskId].UpdateTaskDueDate(dueDate);
        }

        else (doneTasks.ContainsKey(taskId){
            return doneTasks[taskId].UpdateTaskDueDate(dueDate);
        }
    }

    public Task UpdateTaskTitle(User user, int taskId, string title)
    {
        if (backLogTasks.ContainsKey(taskId){
            return backLogTasks[taskId].UpdateTaskTitle(title);
        }

        else if (inProgressTasks.ContainsKey(taskId){
            return inProgressTasks[taskId].UpdateTaskTitle(title);
        }

        else (doneTasks.ContainsKey(taskId){
            return doneTasks[taskId].UpdateTaskTitle(title);
        }
    }

    public Task UpdateTaskDescription(User user, int taskId, string description)
    {
        if (backLogTasks.ContainsKey(taskId){
            return backLogTasks[taskId].UpdateTaskDescription(description);
        }

        else if (inProgressTasks.ContainsKey(taskId){
            return inProgressTasks[taskId].UpdateTaskDescription(description);
        }

        else (doneTasks.ContainsKey(taskId){
            return doneTasks[taskId].UpdateTaskDescription(description);
        }
    }

    public Task AdvanceTask (int taskId)
    {
        if (backLogTasks.ContainsKey(taskId){
            inProgressTasks.Add(taskId, backLogTasks[taskId]);
            backLogTasks.Remove(taskId);
            return inProgressTasks[taskId]
        }

        else if (inProgressTasks.ContainsKey(taskId){
            doneTasks.Add(taskId, inProgressTasks[taskId]);
            inProgressTasks.Remove(taskId);
            return doneTasks[taskId]
        }

        else (doneTasks.ContainsKey(taskId){
            doneTasks.Remove(taskId);
            return null;
        }
    }



