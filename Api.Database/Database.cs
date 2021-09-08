using System;
using System.Collections.Generic;
using Api.Database.Models;

namespace Api.Database
{
	public class Database
	{
		public static IEnumerable<User> GetUsers() => new List<User>
		{
			new User()
			{
				UserName = "Beto",
				Email = "beto@email.com",
				Password = "beto"
			},
			new User()
			{
				UserName = "Becky",
				Email = "becky@email.com",
				Password = "becky"
			}
		};

		public static IEnumerable<Employee> GetAllEmployees() => new[]
		{
			new Employee
			{
				Name = "Alberto",
				LastName = "Ramirez",
				JoiningDate = new DateTime(2018, 05, 10)
			},
			new Employee
			{
				Name = "Becky",
				LastName = "Estrada",
				JoiningDate = new DateTime(2018, 05, 10)
			},
			new Employee
			{
				Name = "Jose",
				LastName = "Perez",
				JoiningDate = new DateTime(2018, 05, 10)
			},
			new Employee
			{
				Name = "Leonela",
				LastName = "Juarez",
				JoiningDate = new DateTime(2018, 05, 10)
			}
		};
	}
}
