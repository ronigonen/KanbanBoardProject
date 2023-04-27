using IntroSE.Kanban.Backend.BuisnessLayer;
using System;
using System.Collections.Generic;

public class UserInProgressTasks
{
	Dictionary<string, List<Task>> userTasks;

	public UserInProgressTasks() { 
	userTasks = new Dictionary<string, List<Task>>();
	}
	
	public UserInProgressTasks(string email)
	{
		this.userTasks = new Dictionary<string, List<Task>>();
		this.userTasks.Add(email, new List<Task>());
	}
	public void AddTasks(string email, Task task)
	{
		if (task == null)
		{
			throw new KanbanException("Task in null");
		}
		userTasks[email].Add(task);
	}
	public void RemoveTasks(string email, Task task)
	{
		if (task == null)
		{
            throw new KanbanException("Task in null");
        }
        userTasks[email].Remove(task);
    }
    public List<Task> GetList(string email) 
	{
		if (userTasks[email] == null | userTasks[email].Count==0)
		{
			throw new KanbanException("No Tasks in progress");
		}
		return userTasks[email];
	}
}
