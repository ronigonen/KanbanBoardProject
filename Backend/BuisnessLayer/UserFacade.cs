using System;
using System.Numerics;

public class UserFacade
{
	public UserFacade()
	{
		this.users = new Dictionary<string, User>;
	}

	public User Register(string email, string password)
	{

		if (this.users.ContainsKey(email))
		{
            throw new InvalidOperationException("email already exists.");
        }
        if (password.Length>20) 
		{
			throw new InvalidOperationException("invalid password. A valid password is in the length of 6 to 20 characters and must include at least one uppercase letter, one small character, and a number.");
		}
		int counterBigger = 0;
		int counterSmaller = 0;
		int countNumbers = 0;
		for (int i=0; i<password.Length; i++)
		{
			if (password[i].IsUpper)
				counterBigger++;
			else if (password[i].IsLower)
				counterSmaller++;
			else 
				countNumbers++;
		}
		if (counterBigger < 1 | counterSmaller < 1  | countNumbers < 1) {
            throw new InvalidOperationException("invalid password. A valid password is in the length of 6 to 20 characters and must include at least one uppercase letter, one small character, and a number.");
        }
		User newOne = new User(password, email);
		this.users.add(email, newOne);
		this.users[email].LogIn(password);
		return newOne;
    }

	public User LogIn(string email, string password)
	{
		if (!this.users.ContainsKey(email))
		{
            throw new InvalidOperationException("you need to register.");
        }
		this.users[email].LogIn(password);
		return this.users[email];
    }

	public void LogOut(string email)
	{
		this.users[email].Logout();
		return this.users[email];
	}

}
