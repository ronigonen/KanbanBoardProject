using System;
using System.Collections.Generic;

public class UserFacade
{
	private Dictionary<string, User> users;
	public UserFacade()
	{
		this.users = new Dictionary<string, User>();
	}

	public void Register(string email, string password)
	{
		if (email == null)
		{
            throw new InvalidOperationException("email can't be null.");

        }
        if (this.users.ContainsKey(email))
		{
            throw new InvalidOperationException("email already exists.");
        }
        if (password.Length>20 | password.Length<6) 
		{
			throw new InvalidOperationException("invalid password. A valid password is in the length of 6 to 20 characters and must include at least one uppercase letter, one small character, and a number.");
		}
		int counterBigger = 0;
		int counterSmaller = 0;
		int countNumbers = 0;
		for (int i=0; i<password.Length; i++)
		{
			char temp = password[i];
			if (char.IsUpper(temp))
				counterBigger++;
			else if (char.IsLower(temp))
				counterSmaller++;
			else 
				countNumbers++;
		}
		if (counterBigger < 1 | counterSmaller < 1  | countNumbers < 1) {
            throw new InvalidOperationException("invalid password. A valid password is in the length of 6 to 20 characters and must include at least one uppercase letter, one small character, and a number.");
        }
		User newOne = new User(password, email);
		this.users.Add(email, newOne);
    }

	public void LogIn(string email, string password)
	{
		if (!this.users.ContainsKey(email))
		{
            throw new InvalidOperationException("you need to register.");
        }
		this.users[email].LogIn(password);
    }

	public void LogOut(string email)
	{
		this.users[email].LogOut();
	}

	public User GetUser(string email)
	{
		if (this.users.ContainsKey(email))
			throw new InvalidOperationException("user doesn't exist");
		return this.users[email];
	}

}
