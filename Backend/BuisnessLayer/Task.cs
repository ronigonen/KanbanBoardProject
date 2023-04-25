using System;
using System.Random;

public class Task
{
    User user;
    private int id;
    private DateTime creationTime;
    private DateTime dueDate;
    private string title;
    private string description;
    private string state;

    public Task(User user, int id, DateTime creationTime, DateTime dueDate, string title, string description)
	{
        this.user = user;
        this.id = id;
        this.creationTime = creationTime;
        this.dueDate = dueDate;
        this.title = title;
        this.description = description;
        this.state = "backLog";

	}

    public int GetId() { 
        return id; 
    }

    public Task UpdateTimeDueDate(DateTime dueDate)
    {
        this.dueDate= dueDate;
        return this;
    }

    public Task UpdateTaskTitle(string title)
    {
        this.title = title;
        return this;
    }

    public Task UpdateTaskDescription(string description)
    {
        this.description= description;
        return this;
    }
}
