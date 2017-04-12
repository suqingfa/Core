using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FilterDemo.Filters;

namespace FilterDemo.Controllers
{
	[ActionFilter]
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

		[HttpGet("Exception")]
		public string Exception()
		{
			throw new Exception();
		}
    }
}
