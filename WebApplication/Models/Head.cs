using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class Head
    {
		[Key, ForeignKey("User")]
		public string UserId { get; set; }

		[Required]
		public byte[] Image { get; set; }

		public virtual ApplicationUser User { get; set; }
    }
}
