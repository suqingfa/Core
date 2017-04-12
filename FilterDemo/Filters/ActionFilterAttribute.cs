using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilterDemo.Filters
{

	/*  以ActionFilter执行顺序为例，默认执行顺序如下图
		1. Controller OnActionExecuting
		2. Global OnActionExecuting
		3. Class OnActionExecuting
		4. Method OnActionExecuting
		5. Method OnActionExecuted
		6. Class OnActionExecuted
		7. Global OnActionExecuted
		8. Controller OnActionExecuted
	 */
	public class ActionFilterAttribute : Attribute, IActionFilter
	{
		public void OnActionExecuted(ActionExecutedContext context)
		{
		}

		public void OnActionExecuting(ActionExecutingContext context)
		{
			context.HttpContext.Response.Headers.Add("My-Header", "WebApiFrame-Header");
		}
	}
}
