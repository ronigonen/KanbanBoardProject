﻿using IntroSE.Kanban.Backend.BuisnessLayer;
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

	public BoardFacade(UserFacade uF)
	{
		this.boards = new Dictionary<string, Board>();
		this.uf = uF;
        this.inProgressUser = new UserInProgressTasks();
	}
	public void CreateBoard(string email, string boardName)
	{
		User user = uf.GetUser(email);
        if (user == null)
        {
            throw new KanbanException("this user does not exists");
        }
        if (!user.IsLoggedIn()) 
        {
            throw new KanbanException("User is not logged in");
        }
        Board board = new Board(inProgressUser, boardName, user);
        boards.Add(boardName, board);
        user.AddBoard(boardName, board);
        inProgressUser.AddUser(email);
    }
    public void DeleteBoard(string email, string boardName)
    {
        User user = uf.GetUser(email);
        if (user == null)
        {
            throw new KanbanException("this user does not exists");
        }
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
        if (user == null)
        {
            throw new KanbanException("this user does not exists");
        }
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
	public void UpdateTaskDueDate(string email, string boardName, int taskId, int columnOrdinal, DateTime dueDate)
	{
        if (columnOrdinal > 2)
        {
            throw new KanbanException("no such column");
        }
        User user = uf.GetUser(email);
        if (user == null)
        {
            throw new KanbanException("this user does not exists");
        }
        if (!user.IsLoggedIn())
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
        if (columnOrdinal == 2)
        {
            throw new KanbanException("Tasks that are 'Done' can not be changed");
        }
        else
            board.UpdateTaskDueDate(taskId, columnOrdinal, dueDate);
	}
    public void UpdateTaskTitle(string email, string boardName, int taskId, int columnOrdinal, string title)
    {
        if (columnOrdinal != 1 & columnOrdinal != 2)
        {
            throw new KanbanException("no such column");
        }
        User user = uf.GetUser(email);
        if (user == null)
        {
            throw new KanbanException("this user does not exists");
        }
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
        if (columnOrdinal == 2)
        {
            throw new KanbanException("Tasks that are 'Done' can not be changed");
        }
        else
            board.UpdateTaskTitle(taskId, columnOrdinal, title);
    }
	public void UpdateTaskDescription(string email, string boardName, int taskId, int columnOrdinal, string description)
	{
        if (columnOrdinal > 2)
        {
            throw new KanbanException("no such column");
        }
        User user = uf.GetUser(email);
        if (user == null)
        {
            throw new KanbanException("this user does not exists");
        }
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
        if (columnOrdinal == 2)
        {
            throw new KanbanException("Tasks that are 'Done' can not be changed");
        }
        else
            board.UpdateTaskDescription(taskId, columnOrdinal, description);
	}
	public void AdvanceTask(string email, string boardName, int columnOrdinal, int taskId)
	{
        if (columnOrdinal > 2)
        {
            throw new KanbanException("Tasks can not be changed to the giving column");
        }
        User user = uf.GetUser(email);
        if (user == null)
        {
            throw new KanbanException("this user does not exists");
        }
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
        else
            board.AdvanceTask(email, columnOrdinal, taskId);
	}
	public void LimitColumn(string email, string boardName, int columnOrdinal, int limit)
	{
        if (columnOrdinal > 2)
        {
            throw new KanbanException("no such column");
        }
        User user = uf.GetUser(email);
        if (user == null)
        {
            throw new KanbanException("this user does not exists");
        }
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
        board.LimitColumn(columnOrdinal, limit);
    }
    public int GetColumnLimit(string email, string boardName, int columnOrdinal)
    {
        if (columnOrdinal > 2)
        {
            throw new KanbanException("no such column");
        }
        User user = uf.GetUser(email);
        if (user == null)
        {
            throw new KanbanException("this user does not exists");
        }
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
        int lim = board.GetColumnLimit(columnOrdinal);
		return lim;
    }
    public string GetColumnName(string email, string boardName, int columnOrdinal)
    {
        if (columnOrdinal > 2)
        {
            throw new KanbanException("no such column");
        }
        User user = uf.GetUser(email);
        if (user == null)
        {
            throw new KanbanException("this user does not exists");
        }
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
        string name = board.GetColumnName(columnOrdinal);
        return name;
    }
	public List<Task> GetColumn(string email, string boardName, int columnOrdinal)
	{
        if (columnOrdinal > 2)
        {
            throw new KanbanException("no such column");
        }
        User user = uf.GetUser(email);
        if (user == null)
        {
            throw new KanbanException("this user does not exists");
        }
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
        List<Task> tasks = board.GetColumn(columnOrdinal);
		return tasks;
    }
	public List<Task> GetInProgress(string email)
	{
        User user = uf.GetUser(email);
        if (user == null)
        {
            throw new KanbanException("this user does not exists");
        }
        if (!user.IsLoggedIn())
        {
            throw new KanbanException("User is not logged in");
        }
        return inProgressUser.GetList(email);
	}
}
