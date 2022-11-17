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
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Courses
        public IActionResult Index()
        {
            var applicationDbContext = _context.courses.Include(c => c.Category);
            return View(applicationDbContext.ToList());
        }

        // GET: Courses/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null || _context.courses == null)
            {
                return NotFound();
            }

            var course = _context.courses
                .Include(c => c.Category)
                .FirstOrDefault(m => m.id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            ViewData["category_id"] = new SelectList(_context.categories, "id", "name");
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("id,name,image,description,category_id,price")] Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Add(course);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewData["category_id"] = new SelectList(_context.categories, "id", "name", course.category_id);
            return View(course);
        }

        // GET: Courses/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null || _context.courses == null)
            {
                return NotFound();
            }

            var course = _context.courses.Find(id);
            if (course == null)
            {
                return NotFound();
            }
            ViewData["category_id"] = new SelectList(_context.categories, "id", "name", course.category_id);
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("id,name,image,description,category_id,price")] Course course)
        {
            if (id != course.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.id))
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
            ViewData["category_id"] = new SelectList(_context.categories, "id", "name", course.category_id);
            return View(course);
        }

        // GET: Courses/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null || _context.courses == null)
            {
                return NotFound();
            }

            var course = _context.courses
                .Include(c => c.Category)
                .FirstOrDefault(m => m.id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (_context.courses == null)
            {
                return Problem("Entity set 'ApplicationDbContext.courses'  is null.");
            }
            var course = _context.courses.Find(id);
            if (course != null)
            {
                _context.courses.Remove(course);
            }
            
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
          return _context.courses.Any(e => e.id == id);
        }
    }
}