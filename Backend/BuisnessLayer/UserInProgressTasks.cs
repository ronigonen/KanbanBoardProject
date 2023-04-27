using System;
using System.Collections.Generic;

public class UserInProgressTasks
{
	private string email;
	private List<Task> inProgressTasks;

	public UserInProgressTasks(string email)
	{
		this.email = email;	
		this.inProgressTasks= new List<Task>();
	}
	public void AddTasks(Task task)
	{
		if (task == null)
		{
			throw new Exception("Task in null");
		}
		inProgressTasks.Add(task);
	}
	public void RemoveTasks(Task task)
	{
		if (task == null)
		{
            throw new Exception("Task in null");
        }
		inProgressTasks.Remove(task);
    }
	public List<Task> GetList() 
	{
		if (inProgressTasks == null)
		{
			throw new Exception("No Tasks in progress");
		}
		return inProgressTasks;
	}
}
