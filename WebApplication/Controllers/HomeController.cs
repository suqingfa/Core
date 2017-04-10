using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Threading;
using Microsoft.Extensions.Primitives;
using Microsoft.Extensions.Logging;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
		private readonly IMemoryCache _memoryCache;
		private readonly ILogger _logger;

		public HomeController(IMemoryCache memoryCache, ILoggerFactory loggerFactory)
		{
			_memoryCache = memoryCache;
			_logger = loggerFactory.CreateLogger<HomeController>();
		}

		public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
			string cacheKey = "key";
			string result;
			if (!_memoryCache.TryGetValue(cacheKey, out result))
			{
				result = $"LineZero{DateTime.Now}";
				//缓存回调 3秒相对时间过期会回调
				_memoryCache.Set(cacheKey, result, new MemoryCacheEntryOptions()
					.SetSlidingExpiration(TimeSpan.FromSeconds(3))
					.RegisterPostEvictionCallback((key, value, reason, substate) =>
					{
						_logger.LogDebug($"键{key}值{value}改变，因为{reason}");
					}));
				//缓存回调 根据Token过期
				//var cts = new CancellationTokenSource();
				//_memoryCache.Set(cacheKey, result, new MemoryCacheEntryOptions()
				//	.AddExpirationToken(new CancellationChangeToken(cts.Token))
				//	.RegisterPostEvictionCallback((key, value, reason, substate) =>
				//	{
				//		Console.WriteLine($"键{key}值{value}改变，因为{reason}");
				//	}));
				//cts.Cancel();
			}
			ViewBag.Message = result;

			return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
