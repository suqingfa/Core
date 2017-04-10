using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
		[RegularExpression("(男|女)")]
		public string Sex { get; set; }

		public virtual IEnumerable<Blog> Blogs { get; set; }
		public virtual Head Head { get; set; }
	}
}
