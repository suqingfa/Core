using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Hangfire;

namespace HangfireDemo
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
			services.AddHangfire(x => x.UseSqlServerStorage(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Hangfire;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"));
		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

			app.UseHangfireServer();
			app.UseHangfireDashboard();

			//任务每分钟执行一次
			RecurringJob.AddOrUpdate(() => Console.WriteLine($"ASP.NET Core LineZero"), Cron.Minutely());
			//任务执行一次
			BackgroundJob.Enqueue(() => Console.WriteLine($"ASP.NET Core One Start LineZero {DateTime.Now}"));
			//任务延时两分钟执行
			BackgroundJob.Schedule(() => Console.WriteLine($"ASP.NET Core await LineZero {DateTime.Now}"), TimeSpan.FromMinutes(2));
		}
    }
}
