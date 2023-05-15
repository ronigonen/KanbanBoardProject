using IntroSE.Kanban.Backend.BuisnessLayer;
using IntroSE.Kanban.Backend.DataAccessLayer;
using System;

public class Task
{
    private TaskDTO tdto;
    private int id;
    private DateTime creationTime;
    private DateTime dueDate;
    private string title;
    private string description;
    private string emailAssignee;

    public Task(int id, DateTime creationTime, DateTime dueDate, string title, string description)
    {
        if (title == null || title.Length > 50 || title.Length<1)
            throw new KanbanException("Title is null or has more than 50 characters");
        if (description == null || description.Length > 300)
            throw new KanbanException("Description is too long or null");
        else
        {
            this.id = id;
            this.creationTime = creationTime;
            this.dueDate = dueDate;
            this.title = title;
            this.description = description;
        }
    }
    public int ID { get => id; set => id=value; }
    public DateTime DueDate { get => dueDate; set =>dueDate=value; }
    public string Title { get => title; set => title=value; }
    public string Description { get=> description; set=> description=value; }
    

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
