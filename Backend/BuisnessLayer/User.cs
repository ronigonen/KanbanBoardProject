using System;
using System.Collections.Generic;

public class User
{
	private string password;
	private string email;
	private bool loggedIn;


	public User(string password, string email)
	{
		this.password = password;
		this.email = email;
		this.loggedIn = true;
	}

	public string GetEmail() { 
		return email; 
	}

	public bool LogIn(string password){
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
