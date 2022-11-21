using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Laos_LearningPath_Backend.Data;
using Laos_LearningPath_Backend.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Laos_LearningPath_Backend.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Users
        public IActionResult Index()
        {
            if(HttpContext.Session.GetString("id") != null)
            {
                return View(_context.users.ToList());
            }
            else
            {
                return RedirectToAction(nameof(Login));
            }
        }

        // GET: Users/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null || _context.users == null)
            {
                return NotFound();
            }

            var user =_context.users
                .FirstOrDefault(m => m.id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        public IActionResult Login()
        {
            if(HttpContext.Session.GetString("id") != null)
            {
                if (HttpContext.Session.GetString("is_admin") != "True")
                {
                    ViewBag.Notification = "Anda bukan Admin. Dilarang masuk";
                    return View();
                }
                
                return RedirectToAction("Index"); 
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(User user)
        {
            var usr = _context.users.Where(u => u.email.Equals(user.email) && u.password.Equals(user.password)).FirstOrDefault();
            if (usr != null)
            {
                HttpContext.Session.SetString("id", usr.id.ToString());
                HttpContext.Session.SetString("name", usr.name.ToString());
                HttpContext.Session.SetString("is_admin", usr.is_admin.ToString());
            }
            else
            {
                ViewBag.Notification = "User atau password salah";
                return View();
            }
            return RedirectToAction("Login");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
                
        private bool UserExists(int id)
        {
          return _context.users.Any(e => e.id == id);
        }
    }
}
