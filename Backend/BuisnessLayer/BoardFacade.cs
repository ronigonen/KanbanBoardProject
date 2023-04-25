using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

public class BoardFacade
{
	private Dictionary<string, board> boards;
	private UserFacade uf;
	private list<UserInProgressTaskList> userInProgressTaskList;

	public BoardFacade()
	{
		this.boards = new Dictionary<string, board>)(null, null);
		this.uf = null;
	}
	public Dictionary<string, Board> CreateBoard(string email, string boardName)
	{
        User user = uf.getUser(email);
        Board board =new Board (boardName, email);
	}
	public Task AddTask(string email,string boardName, string title, string description, DateTime dueDate, DateTime creationTime)
	{
		Board board = boards.get(boardName);
		User user = uf.getUser(email);
		Task addedTask=board.AddTask(user, dueDate, title, description, creationTime);
		return addedTask;
	}
	public Task UpdateTaskDueDate(string email, string boardName, int taskId, DateTime dueDate)
	{
		Board board= boards.get(boardName);
		User user = uf.getUser(email);
		Task updatedTask = board.UpdateTaskDueDate(user, taskId, dueDate);
		return updatedTask;
	}
    public Task UpdateTaskTitle(string email, string boardName, int taskId, string title)
    {
        Board board = boards.get(boardName);
        User user = uf.getUser(email);
        Task updatedTask = board.UpdateTaskTitle(user, taskId, title);
		return updatedTask;
    }
	public Task UpdateTaskDescription(string email, string boardName, int taskId, string description)
	{
		Board board = boards.get(boardName);
		User user = uf.getUser(email);
		Task updatedTask = board.UpdateTaskDescription(user, taskId, description);
		return updatedTask;
	}
	public Task AdvanceTask(string email, string boardName, int taskId)
	{
		Board board = boards.get(boardName);
		Task advancedTask = board.AdvanceTask(taskId);
		return advancedTask;
	}
	public void LimitColumn(string email, string boardName, string columnName, int limit)
	{
        Board board = boards.get(boardName);
		board.LimitColumn(columnName, limit);
    }
    public int GetColumnLimit(string email, string boardName, string columnName)
    {
        Board board = boards.get(boardName);
        int lim = board.GetColumnLimit(columnName);
		return lim;
    }
	public List<Task> GetColumn(string email, string boardName, string columnName)
	{
        Board board = boards.get(boardName);
		List<Task> tasks = board.GetColumn(columnName);
		return tasks;
    }
	public List<Task> GetInProgress(string email)
	{
        User user = uf.getUser(email);
		List<Task> userList = userInProgressTaskList.get(email).GetList();
		return userList;
	}
}
