using backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using backend.Services;

namespace backend.Controllers
{
	public class AuthenticateController : Controller
	{
		Database db;
		public AuthenticateController(Database _db) {
			db = _db;
		}
		public string Registration(RegistrationModel registration)
		{
			if (db.Users.FirstOrDefault(u => u.Login == registration.Login) != null) 
			{ Response.StatusCode = 200; return "Такой логин уже занят"; };

			if (db.Users.FirstOrDefault(u => u.Email == registration.Email) != null) 
			{ Response.StatusCode = 200; return "Такая почта уже используется"; };

			User user = new User();
			user.Login = registration.Login;
			user.Email = registration.Email;
			user.Password = Crypt.Hash(registration.Password);
			db.Users.Add(user);
			db.SaveChanges();

			Response.StatusCode = 200;
			return "OK";
		}

		public string Login(LoginModel LoginModel)
		{
            if (db.Users.FirstOrDefault(u => u.Login == LoginModel.Login && u.Password == Crypt.Hash(LoginModel.Password)) == null) 
			{ Response.StatusCode = 200; return "Неверный логин или пароль"; }

			
			User user = db.Users.FirstOrDefault(u => u.Login == LoginModel.Login && u.Password == Crypt.Hash(LoginModel.Password))!;

			Response.StatusCode = 200;
			return Crypt.Encrypt(user.Login);
		}

	}
}
