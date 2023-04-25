using System;
using System.Collections.Generic;

public class User
{
	public User()
	{
	private string password;
	private string email;
	private bool loggedIn;

	public User(string password, string email)
	{
		this.password = password;
		this.email = email;
		this.loggedIn = false;
	}

	public bool Login(string password){
		if (User.password == password){
			User.loggedIn = true;
			return true;
		}
		else{
			return false;
		}
	}

	public bool IsLoggedIn(){
		return loggedIn;
	}

	public void LogOut()
	{
		loggedIn = false;
	}
	}
}
