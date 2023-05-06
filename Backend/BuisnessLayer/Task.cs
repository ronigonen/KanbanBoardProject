using IntroSE.Kanban.Backend.BuisnessLayer;
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
        if (title == null || title.Length > 50)
            throw new KanbanException("Title is empty or has more than 50 characters");
        if (description.Length > 300)
            throw new KanbanException("Description is too long");
        else
        {
            this.user = user;
            this.id = id;
            this.creationTime = creationTime;
            this.dueDate = dueDate;
            this.title = title;
            this.description = description;
        }
	}

    public int GetId() { 
        return id; 
    }

    public void UpdateTaskDueDate(DateTime dueDate)
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
