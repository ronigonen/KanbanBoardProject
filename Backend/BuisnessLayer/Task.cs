using System;

public class Task
{
    User user;
    private int id;
    private DateTime creationTime;
    private DateTime dueDate;
    private string title;
    private string description;

    public Task(User user, int id, DateTime creationTime, DateTime dueDate, string title, string description)
	{
        this.user = user;
        this.id = id;
        this.creationTime = creationTime;
        this.dueDate = dueDate;
        this.title = title;
        this.description = description;
	}

    public int GetId() { 
        return id; 
    }

    public void UpdateTimeDueDate(DateTime dueDate)
    {
        this.dueDate= dueDate;
    }

    public void UpdateTaskTitle(string title)
    {
        this.title = title;
    }

    public void UpdateTaskDescription(string description)
    {
        this.description= description;
    }
}
