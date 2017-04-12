using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilterDemo.Filters
{
	public class SimpleExceptionFilterAttribute : Attribute, IExceptionFilter
	{
		private readonly ILogger<SimpleExceptionFilterAttribute> _logger;

		public SimpleExceptionFilterAttribute(ILogger<SimpleExceptionFilterAttribute> logger)
		{
			_logger = logger;
		}

		public void OnException(ExceptionContext context)
		{
			_logger.LogError("Exception Execute! Message:" + context.Exception.Message);
			context.ExceptionHandled = true;
		}
	}
}
