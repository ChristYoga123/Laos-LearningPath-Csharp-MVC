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
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Orders
        public IActionResult Index()
        {
            var applicationDbContext = _context.orders.Include(o => o.Course).Include(o => o.User);
            return View(applicationDbContext.ToList());
        }

        // GET: Orders/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null || _context.orders == null)
            {
                return NotFound();
            }

            var order = _context.orders
                .Include(o => o.Course)
                .Include(o => o.User)
                .FirstOrDefault(m => m.id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null || _context.orders == null)
            {
                return NotFound();
            }

            var order = _context.orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["course_id"] = new SelectList(_context.courses, "id", "name", order.course_id);
            ViewData["user_id"] = new SelectList(_context.users, "id", "name", order.user_id);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("id,user_id,course_id,status")] Order order)
        {
            if (id != order.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["course_id"] = new SelectList(_context.courses, "id", "name", order.course_id);
            ViewData["user_id"] = new SelectList(_context.users, "id", "name", order.user_id);
            return View(order);
        }

        private bool OrderExists(int id)
        {
          return _context.orders.Any(e => e.id == id);
        }
    }
}
