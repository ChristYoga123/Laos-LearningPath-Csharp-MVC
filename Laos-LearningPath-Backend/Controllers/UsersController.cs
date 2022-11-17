using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Laos_LearningPath_Backend.Data;
using Laos_LearningPath_Backend.Models;

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
              return View(_context.users.ToList());
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
                
        private bool UserExists(int id)
        {
          return _context.users.Any(e => e.id == id);
        }
    }
}
