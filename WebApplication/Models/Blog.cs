using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class Blog
    {
		public string Id { get; set; }
		public string UserId { get; set; }
		public string Content { get; set; }

		public virtual ApplicationUser User { get; set; }
    }
}
