using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.Models;
using Microsoft.AspNetCore.Identity;

namespace WebApplication.Controllers
{
	public class BlogsController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly UserManager<ApplicationUser> _userManager;
		private ApplicationUser _user { get => _userManager.GetUserAsync(HttpContext.User).Result; }

		public BlogsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		// GET: Blogs
		public async Task<IActionResult> Index()
		{
			var applicationDbContext = _context.Blogs.Where(b=>b.UserId == _user.Id).Include(b => b.User);
			return View(await applicationDbContext.ToListAsync());
		}

		// GET: Blogs/Details/5
		public async Task<IActionResult> Details(string id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var blog = await _context.Blogs
				.Include(b => b.User)
				.SingleOrDefaultAsync(m => m.Id == id);
			if (blog == null)
			{
				return NotFound();
			}

			return View(blog);
		}

		// GET: Blogs/Create
		public IActionResult Create()
		{
			//ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
			return View();
		}

		// POST: Blogs/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Content")] Blog blog)
		{
			//if (ModelState.IsValid)
			{
				blog.UserId = _user.Id;
				_context.Add(blog);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			//ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", blog.UserId);
			//return View(blog);
		}

		// GET: Blogs/Edit/5
		public async Task<IActionResult> Edit(string id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var blog = await _context.Blogs.SingleOrDefaultAsync(m => m.Id == id);
			if (blog == null)
			{
				return NotFound();
			}
			ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", blog.UserId);
			return View(blog);
		}

		// POST: Blogs/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(string id, [Bind("Id,UserId,Content")] Blog blog)
		{
			if (id != blog.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(blog);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!BlogExists(blog.Id))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction("Index");
			}
			ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", blog.UserId);
			return View(blog);
		}

		// GET: Blogs/Delete/5
		public async Task<IActionResult> Delete(string id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var blog = await _context.Blogs
				.Include(b => b.User)
				.SingleOrDefaultAsync(m => m.Id == id);
			if (blog == null)
			{
				return NotFound();
			}

			return View(blog);
		}

		// POST: Blogs/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(string id)
		{
			var blog = await _context.Blogs.SingleOrDefaultAsync(m => m.Id == id);
			_context.Blogs.Remove(blog);
			await _context.SaveChangesAsync();
			return RedirectToAction("Index");
		}

		private bool BlogExists(string id)
		{
			return _context.Blogs.Any(e => e.Id == id);
		}
	}
}
