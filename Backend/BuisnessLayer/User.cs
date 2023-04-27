using System;
using System.Collections.Generic;
using System.ComponentModel.Design;

public class User
{
	private string password;
	private string email;
	private bool loggedIn;
	private Dictionary<string, Board> boards;


	public User(string password, string email)
	{
		this.password = password;
		this.email = email;
		this.loggedIn = true;
		this.boards=new Dictionary<string, Board>();
	}

	public string GetEmail() {
		return email;
	}

	public void LogIn(string password) {
		if (this.password != password) {
			throw new InvalidOperationException("password is wrong.");
		}
		else 
		{ 
            if (this.loggedIn)
			{
				throw new InvalidOperationException("user already logged in.");
			}
			else
			{
				this.loggedIn = true;
			}
		}
	}

	public bool IsLoggedIn() {
		return this.loggedIn;
	}

	public void LogOut()
	{
		if (!this.loggedIn)
		{
            throw new InvalidOperationException("user already logged out.");
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
}
