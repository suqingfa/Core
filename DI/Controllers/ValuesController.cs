using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace DI.Controllers
{
	[Route("api/[controller]")]
	public class ValuesController : Controller
	{
		public ITestTransient _testTransient { get; }
		public ITestScoped _testScoped { get; }
		public ITestSingleton _testSingleton { get; }

		public ValuesController(ITestTransient testTransient, ITestScoped testScoped, ITestSingleton testSingleton)
		{
			_testTransient = testTransient;
			_testScoped = testScoped;
			_testSingleton = testSingleton;
		}

		[HttpGet]
		public IEnumerable<Guid> Index()
		{
			return new Guid[] { _testTransient.TargetId, _testScoped.TargetId, _testSingleton.TargetId };
		}
	}
}
