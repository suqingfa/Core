using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiddlewareDemo.Middleware
{
	public class HelloworldMiddleware
	{
		private readonly RequestDelegate _next;

		public HelloworldMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			await context.Response.WriteAsync("Hello World Middleware!");
			await _next(context);
		}
	}

	public static class HelloworldMiddlewareExtensions
	{
		public static IApplicationBuilder UseHelloworldMiddleware(this IApplicationBuilder app)
		{
			app.UseMiddleware<HelloworldMiddleware>();
			return app;
		}
	}
}
