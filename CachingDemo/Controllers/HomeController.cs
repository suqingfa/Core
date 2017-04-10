using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Threading;
using Microsoft.Extensions.Primitives;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;

namespace RedisDemo.Controllers
{
    public class HomeController : Controller
    {
		private readonly IMemoryCache _memoryCache;
		private readonly IDistributedCache _cache;

		public HomeController(IMemoryCache memoryCache, IDistributedCache cache)
		{
			_memoryCache = memoryCache;
			_cache = cache;
		}

		public async Task<IActionResult> Index()
        {
			string cacheKey = "key";
			if (!_memoryCache.TryGetValue(cacheKey, out string result))
			{
				result = $"IMemoryCache{DateTime.Now}";

				// 设置相对过期时间2秒钟
				_memoryCache.Set(cacheKey, result, new MemoryCacheEntryOptions()
					.SetSlidingExpiration(TimeSpan.FromSeconds(2)));

				// 设置绝对过期时间2秒钟
				// _memoryCache.Set(cacheKey, result, TimeSpan.FromSeconds(2));
			}

			var value = await _cache.GetStringAsync("lastServerStartTime");
			if (value == null)
				await _cache.SetStringAsync("lastServerStartTime", value = $"IDistributedCache{DateTime.Now}", 
					new DistributedCacheEntryOptions { SlidingExpiration = TimeSpan.FromSeconds(2) });
			
			return Content(result + " " + value);
        }

		public IActionResult RedisSession()
		{

			string valueToDisplay = "Not Found";
			HttpContext.Session.TryGetValue("TestProperty", out byte[] valueFromRedis);
			if(valueFromRedis == null)
			{
				var valueToStoreInRedis = Encoding.UTF8.GetBytes(valueToDisplay = $"Redis Session{DateTime.Now}");
				HttpContext.Session.Set("TestProperty", valueToStoreInRedis);
			}
			else
				valueToDisplay = Encoding.UTF8.GetString(valueFromRedis);

			return Content(valueToDisplay);
		}
    }
}
