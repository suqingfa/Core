using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using MiddlewareDemo.Middleware;

namespace MiddlewareDemo
{
	public class Startup
	{
		public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
				.AddEnvironmentVariables();
			Configuration = builder.Build();
		}

		public IConfigurationRoot Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			// Add framework services.
			services.AddMvc();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			loggerFactory.AddConsole(Configuration.GetSection("Logging"));
			loggerFactory.AddDebug();

			app.UseMvc();

			// 添加自定义中间件
			app.Map("/test", MapTest);

			// 添加自定义中间件
			// /*?a
			app.MapWhen(context =>
			{
				return context.Request.Query.ContainsKey("a");
			}, a =>
			{
				a.Run(async context =>
				{
					await context.Response.WriteAsync("Hello World MapWhen!");
				});
			});

			// 添加自定义中间件
			app.Use(async (context, next) =>
			{
				await context.Response.WriteAsync("Hello World Use !");
				await next();
			});

			// 添加自定义中间件
			app.UseHelloworldMiddleware();

			// 添加自定义中间件
			app.Run(async context =>
			{
				await context.Response.WriteAsync("Hello World Run!");
			});
		}

		private void MapTest(IApplicationBuilder app)
		{
			// /test/level
			app.Map("/level", a =>
			{
				a.Run(async context =>
				{
					await context.Response.WriteAsync("Hello World Run!");
				});
			}).Run(async context =>
			{
				await context.Response.WriteAsync("Url is " + context.Request.PathBase + context.Request.Path);
			});
		}
	}
}
