using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilterDemo.Filters
{
	public class SimpleActionFilterAttribute : Attribute, IActionFilter
	{
		private readonly ILogger<SimpleActionFilterAttribute> _logger;

		public SimpleActionFilterAttribute(ILogger<SimpleActionFilterAttribute> logger)
		{
			_logger = logger;
		}

		public void OnActionExecuted(ActionExecutedContext context)
		{
			_logger.LogInformation("ActionFilter Executed!");
		}

		public void OnActionExecuting(ActionExecutingContext context)
		{
			_logger.LogInformation("ActionFilter Executing!");
		}
	}
}
