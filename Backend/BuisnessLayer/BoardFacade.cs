using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

public class BoardFacade
{
	private Dictionary<string, Board> boards;
	private UserFacade uf;
	private list<UserInProgressTaskList> userInProgressTaskList;

	public BoardFacade()
	{
		this.boards = new Dictionary<string, Board>)(null, null);
		this.uf = null;
	}
	public void CreateBoard(string email, string boardName)
	{
		User user = uf.GetUser(email);
		if (!user.IsLoggedIn()) 
        {
            throw new Exception("User is not logged in");
        }
        Board board = new Board(boardName, user);
    }
    public void AddTask(string email,string boardName, string title, string description, DateTime dueDate, DateTime creationTime)
	{
        User user = uf.GetUser(email);
        if (!user.IsLoggedIn())
        {
            throw new Exception("User is not logged in");
        }
        Board board = boards[boardName];
        if (board == null)
        {
            throw new Exception("This board name does not exists");
        }
        Task addedTask =board.AddTask(user, dueDate, title, description, creationTime);
	}
	public void UpdateTaskDueDate(string email, string boardName, int taskId, DateTime dueDate)
	{
        User user = uf.GetUser(email);
		if(!user.IsLoggedIn())
		{
            throw new Exception("User is not logged in");
        }
        Board board= boards[boardName];
		if (board == null)
		{
			throw new Exception("This board name does not exists");
		}		
		Task updatedTask = board.UpdateTaskDueDate(user, taskId, dueDate);
	}
    public void UpdateTaskTitle(string email, string boardName, int taskId, string title)
    {
        User user = uf.GetUser(email);
        if (!user.IsLoggedIn())
        {
            throw new Exception("User is not logged in");
        }
        Board board = boards[boardName];
        if (board == null)
        {
            throw new Exception("This board name does not exists");
        }
        Task updatedTask = board.UpdateTaskTitle(user, taskId, title);
    }
	public void UpdateTaskDescription(string email, string boardName, int taskId, string description)
	{
        User user = uf.GetUser(email);
        if (!user.IsLoggedIn())
        {
            throw new Exception("User is not logged in");
        }
        Board board = boards[boardName];
        if (board == null)
        {
            throw new Exception("This board name does not exists");
        }
        Task updatedTask = board.UpdateTaskDescription(user, taskId, description);
	}
	public void AdvanceTask(string email, string boardName, int taskId)
	{
        User user = uf.GetUser(email);
        if (!user.IsLoggedIn())
        {
            throw new Exception("User is not logged in");
        }
        Board board = boards[boardName];
        if (board == null)
        {
            throw new Exception("This board name does not exists");
        }
        Task advancedTask = board.AdvanceTask(taskId);
		return advancedTask;
	}
	public void LimitColumn(string email, string boardName, string columnName, int limit)
	{
        User user = uf.GetUser(email);
        if (!user.IsLoggedIn())
        {
            throw new Exception("User is not logged in");
        }
        Board board = boards[boardName];
        if (board == null)
        {
            throw new Exception("This board name does not exists");
        }
        board.LimitColumn(columnName, limit);
    }
    public int GetColumnLimit(string email, string boardName, string columnName)
    {
        User user = uf.GetUser(email);
        if (!user.IsLoggedIn())
        {
            throw new Exception("User is not logged in");
        }
        Board board = boards[boardName];
        if (board == null)
        {
            throw new Exception("This board name does not exists");
        }
        int lim = board.GetColumnLimit(columnName);
		return lim;
    }
	public List<Task> GetColumn(string email, string boardName, string columnName)
	{
        User user = uf.GetUser(email);
        if (!user.IsLoggedIn())
        {
            throw new Exception("User is not logged in");
        }
        Board board = boards[boardName];
        if (board == null)
        {
            throw new Exception("This board name does not exists");
        }
        List<Task> tasks = board.GetColumn(columnName);
		return tasks;
    }
	public List<Task> GetInProgress(string email)
	{
        User user = uf.GetUser(email);
        if (!user.IsLoggedIn())
        {
            throw new Exception("User is not logged in");
        }
        List<Task> userList = userInProgressTaskList.get(email).GetList();
		return userList;
	}
}
