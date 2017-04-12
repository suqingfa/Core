using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilterDemo.Filters
{
	public class SimpleResourceFilterAttribute : Attribute, IResourceFilter
	{
		private readonly ILogger<SimpleResourceFilterAttribute> _logger;

		public SimpleResourceFilterAttribute(ILogger<SimpleResourceFilterAttribute> logger)
		{
			_logger = logger;
		}

		public void OnResourceExecuted(ResourceExecutedContext context)
		{
			_logger.LogInformation("ResourceFilter Executed!");
		}

		public void OnResourceExecuting(ResourceExecutingContext context)
		{
			_logger.LogInformation("ResourceFilter Executing!");
		}
	}
}
