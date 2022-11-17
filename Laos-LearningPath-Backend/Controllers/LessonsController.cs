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
    public class LessonsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LessonsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Lessons
        public IActionResult Index()
        {
            var applicationDbContext = _context.lessons.Include(l => l.Course);
            return View(applicationDbContext.ToList());
        }

        // GET: Lessons/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null || _context.lessons == null)
            {
                return NotFound();
            }

            var lesson = _context.lessons
                .Include(l => l.Course)
                .FirstOrDefault(m => m.id == id);
            if (lesson == null)
            {
                return NotFound();
            }

            return View(lesson);
        }

        // GET: Lessons/Create
        public IActionResult Create()
        {
            ViewData["course_id"] = new SelectList(_context.courses, "id", "name");
            return View();
        }

        // POST: Lessons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("id,name,url,course_id")] Lesson lesson)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lesson);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewData["course_id"] = new SelectList(_context.courses, "id", "name", lesson.course_id);
            return View(lesson);
        }

        // GET: Lessons/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null || _context.lessons == null)
            {
                return NotFound();
            }

            var lesson = _context.lessons.Find(id);
            if (lesson == null)
            {
                return NotFound();
            }
            ViewData["course_id"] = new SelectList(_context.courses, "id", "name", lesson.course_id);
            return View(lesson);
        }

        // POST: Lessons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("id,name,url,course_id")] Lesson lesson)
        {
            if (id != lesson.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lesson);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LessonExists(lesson.id))
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
            ViewData["course_id"] = new SelectList(_context.courses, "id", "name", lesson.course_id);
            return View(lesson);
        }

        // GET: Lessons/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null || _context.lessons == null)
            {
                return NotFound();
            }

            var lesson = _context.lessons
                .Include(l => l.Course)
                .FirstOrDefault(m => m.id == id);
            if (lesson == null)
            {
                return NotFound();
            }

            return View(lesson);
        }

        // POST: Lessons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (_context.lessons == null)
            {
                return Problem("Entity set 'ApplicationDbContext.lessons'  is null.");
            }
            var lesson = _context.lessons.Find(id);
            if (lesson != null)
            {
                _context.lessons.Remove(lesson);
            }
            
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool LessonExists(int id)
        {
          return _context.lessons.Any(e => e.id == id);
        }
    }
}
