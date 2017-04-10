using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class Blog
    {
		[Key]
		public string Id { get; set; }
		[Required]
		public string UserId { get; set; }
		[Required]
		public string Content { get; set; }

		public virtual ApplicationUser User { get; set; }
    }
}
