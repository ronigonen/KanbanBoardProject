using IntroSE.Kanban.Backend.BuisnessLayer;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

public class BoardFacade
{
	private Dictionary<string, Board> boards;
	private UserFacade uf;
	private UserInProgressTasks inProgressUser;

	public BoardFacade()
	{
		this.boards = new Dictionary<string, Board>(null, null);
		this.uf = null;
        this.inProgressUser = new UserInProgressTasks();
	}
	public void CreateBoard(string email, string boardName)
	{
		User user = uf.GetUser(email);
		if (!user.IsLoggedIn()) 
        {
            throw new KanbanException("User is not logged in");
        }
        Board board = new Board(boardName, user);
        user.AddBoard(boardName, board);
    }
    public void DeleteBoard(string email, string boardName)
    {
        User user = uf.GetUser(email);
        if (!user.IsLoggedIn())
        {
            throw new KanbanException("User is not logged in");
        }
        if (!boards.ContainsKey(boardName))
        {
            throw new KanbanException("This board name does not exists");
        }
        boards.Remove(boardName);
        user.DeleteBoard(boardName);
    }
    public void AddTask(string email,string boardName, string title, string description, DateTime dueDate, DateTime creationTime)
	{
        User user = uf.GetUser(email);
        if (!user.IsLoggedIn())
        {
            throw new KanbanException("User is not logged in");
        }
        Board board = boards[boardName];
        if (board == null)
        {
            throw new KanbanException("This board name does not exists");
        }
        if (!user.GetBoards().ContainsKey(boardName))
        {
            throw new KanbanException("The user is not a member of this board");
        }
        board.AddTask(user, dueDate, title, description, creationTime);
	}
	public void UpdateTaskDueDate(string email, string boardName, int taskId, DateTime dueDate)
	{
        User user = uf.GetUser(email);
		if(!user.IsLoggedIn())
		{
            throw new KanbanException("User is not logged in");
        }
        Board board= boards[boardName];
		if (board == null)
		{
			throw new KanbanException("This board name does not exists");
		}
        if (!user.GetBoards().ContainsKey(boardName))
        {
            throw new KanbanException("The user is not a member of this board");
        }
        board.UpdateTaskDueDate(taskId, dueDate);
	}
    public void UpdateTaskTitle(string email, string boardName, int taskId, string title)
    {
        User user = uf.GetUser(email);
        if (!user.IsLoggedIn())
        {
            throw new KanbanException("User is not logged in");
        }
        Board board = boards[boardName];
        if (board == null)
        {
            throw new KanbanException("This board name does not exists");
        }
        if (!user.GetBoards().ContainsKey(boardName))
        {
            throw new KanbanException("The user is not a member of this board");
        }
        board.UpdateTaskTitle(taskId, title);
    }
	public void UpdateTaskDescription(string email, string boardName, int taskId, string description)
	{
        User user = uf.GetUser(email);
        if (!user.IsLoggedIn())
        {
            throw new KanbanException("User is not logged in");
        }
        Board board = boards[boardName];
        if (board == null)
        {
            throw new KanbanException("This board name does not exists");
        }
        if (!user.GetBoards().ContainsKey(boardName))
        {
            throw new KanbanException("The user is not a member of this board");
        }
        board.UpdateTaskDescription(taskId, description);
	}
	public void AdvanceTask(string email, string boardName, int taskId)
	{
        User user = uf.GetUser(email);
        if (!user.IsLoggedIn())
        {
            throw new KanbanException("User is not logged in");
        }
        Board board = boards[boardName];
        if (board == null)
        {
            throw new KanbanException("This board name does not exists");
        }
        if (!user.GetBoards().ContainsKey(boardName))
        {
            throw new KanbanException("The user is not a member of this board");
        }
        board.AdvanceTask(email, taskId);
	}
	public void LimitColumn(string email, string boardName, string columnName, int limit)
	{
        User user = uf.GetUser(email);
        if (!user.IsLoggedIn())
        {
            throw new KanbanException("User is not logged in");
        }
        Board board = boards[boardName];
        if (board == null)
        {
            throw new KanbanException("This board name does not exists");
        }
        if (!user.GetBoards().ContainsKey(boardName))
        {
            throw new KanbanException("The user is not a member of this board");
        }
        board.LimitColumn(columnName, limit);
    }
    public int GetColumnLimit(string email, string boardName, string columnName)
    {
        User user = uf.GetUser(email);
        if (!user.IsLoggedIn())
        {
            throw new KanbanException("User is not logged in");
        }
        Board board = boards[boardName];
        if (board == null)
        {
            throw new KanbanException("This board name does not exists");
        }
        if (!user.GetBoards().ContainsKey(boardName))
        {
            throw new KanbanException("The user is not a member of this board");
        }
        int lim = board.GetColumnLimit(columnName);
		return lim;
    }
	public List<Task> GetColumn(string email, string boardName, string columnName)
	{
        User user = uf.GetUser(email);
        if (!user.IsLoggedIn())
        {
            throw new KanbanException("User is not logged in");
        }
        Board board = boards[boardName];
        if (board == null)
        {
            throw new KanbanException("This board name does not exists");
        }
        if (!user.GetBoards().ContainsKey(boardName))
        {
            throw new KanbanException("The user is not a member of this board");
        }
        List<Task> tasks = board.GetColumn(columnName);
		return tasks;
    }
	public List<Task> GetInProgress(string email)
	{
        User user = uf.GetUser(email);
        if (!user.IsLoggedIn())
        {
            throw new KanbanException("User is not logged in");
        }
        return inProgressUser.GetList(email);
	}
}
