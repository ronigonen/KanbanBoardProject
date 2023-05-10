﻿using IntroSE.Kanban.Backend.BuisnessLayer;
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
		if (!this.password.Equals(password)) {
			Console.WriteLine("the password is wrong");
			throw new KanbanException("password is wrong.");
		}
		else 
		{ 
            if (this.loggedIn)
			{
				Console.WriteLine("this user is already logged in");
				throw new KanbanException("user already logged in.");
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
