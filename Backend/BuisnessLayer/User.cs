using IntroSE.Kanban.Backend.BuisnessLayer;
using IntroSE.Kanban.Backend.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;

public class User
{
    private UserDTO udto;
    private string password;
	private string email;
	private bool loggedIn;
	private Dictionary<string, Board> boards;


	public User(string password, string email)
	{
		this.password = password;
		this.email = email;
		this.loggedIn = true;
		this.boards = new Dictionary<string, Board>();
	}

    public User(UserDTO udto)
    {
        this.password = password;
        this.email = email;
        this.loggedIn = true;
        this.boards = new Dictionary<string, Board>();
    }


    public string EMAIL { get => email; set => email = value; }


    public void LogIn(string password) {
		if (!this.password.Equals(password)) {
			throw new KanbanException("password is wrong.");
		}
		if (this.loggedIn)
		{
			throw new KanbanException("user already logged in.");
		}
		this.loggedIn = true;
	}

	public bool IsLoggedIn() {
		return this.loggedIn;
	}

	public void LogOut()
	{
		if (!this.loggedIn)
		{
            throw new KanbanException("user already logged out.");
        }
        this.loggedIn = false;
    }

	public Dictionary<string, Board> GetBoards()
	{
		return this.boards;
	}

	public void AddBoard(string boardName, Board board)
	{
		this.boards.Add(boardName, board);
	}
    public void DeleteBoard(string boardName)
    {
        this.boards.Remove(boardName);
    }
}
