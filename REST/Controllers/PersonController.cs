using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using REST.Models;

namespace REST.Controllers
{
    [Route("api/[controller]")]
    public class PersonController : Controller
    {
		private static List<Person> m_persons = new List<Person>() {
			new Person(1,"苏", '男',25),
			new Person(2,"雅", '女', 17)};

		// GET api/values
		[HttpGet]
        public IEnumerable<Person> Get()
        {
            return m_persons;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Person Get(int id)
        {
            return m_persons.Where(p => p.Id == id).FirstOrDefault();
        }

        // POST api/values
        [HttpPost]
        public Person Post([FromBody]Person value)
        {
			m_persons.Add(value);
			return value;
		}

        // PUT api/values/5
        [HttpPut("{id}")]
        public Person Put(int id, [FromBody]Person value)
        {
			if (id != value.Id)
				return null;

			var person = m_persons.Where(p => p.Id == id).First();
			person.Name = value.Name;
			person.Sex = value.Sex;
			person.Age = value.Age;

			return person;
		}

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public int Delete(int id)
        {
			return m_persons.RemoveAll(p => p.Id == id);
		}
    }
}
