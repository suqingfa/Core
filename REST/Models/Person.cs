using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace REST.Models
{
    public class Person
    {
		public int Id { get; set; }
		public string Name { get; set; }
		public char Sex { get; set; }
		public int Age { get; set; }

		public Person() { }

		public Person(int id, string name, char sex, int age)
		{
			Id = id;
			Name = name;
			Sex = sex;
			Age = age;
		}
	}
}
