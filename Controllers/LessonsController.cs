using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolBackoffice.Data;
using SchoolBackoffice.Models;

namespace SchoolBackoffice.Controllers
{
    public class LessonsController : Controller
    {
        private readonly SchoolBackofficeContext _context;

        private readonly string[] Days = new[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

        private readonly string[] Subjects = new[] { "English", "Mathematics", "Polish", "Physical Education", "Physics", "Chemistry", "Geography", "Biology", "History", "Information Technology", "Music", "Religious Education" };

        public LessonsController(SchoolBackofficeContext context)
        {
            _context = context;
        }

        // GET: Lessons
        public async Task<IActionResult> Index()
        {
            ViewData["Teachers"] = _context.Teachers.Count() != 0;
            var SchoolBackofficeContext = _context.Lessons.Include(l => l.Teacher);
            return View(await SchoolBackofficeContext.ToListAsync());
        }

        // GET: Lessons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Lessons == null)
            {
                return NotFound();
            }

            var lesson = await _context.Lessons
                .Include(l => l.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lesson == null)
            {
                return NotFound();
            }

            return View(lesson);
        }

        // GET: Lessons/Create
        [Authorize]
        public IActionResult Create()
        {
            if (_context != null && _context.Teachers != null && _context.Teachers.Count() != 0)
            {
                ViewData["Teacher"] = new SelectList(_context.Teachers, "Id", null, _context.Teachers.First());
                ViewData["WeekDays"] = Days.Select(x => new SelectListItem() { Text = x, Value = x, Selected = x == Days[0] });
                ViewData["Subjects"] = Subjects.Select(x => new SelectListItem() { Text = x, Value = x, Selected = x == Subjects[0] });
                return View();
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Lessons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Type,WeekDay,DateStart,Duration,TeacherId")] Lesson lesson)
        {
            if (ModelState.IsValid)
            {
                lesson.Teacher = await _context.Teachers.FindAsync(lesson.TeacherId);
                _context.Add(lesson);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Teacher"] = new SelectList(_context.Teachers, "Id", null, lesson.Teacher);
            ViewData["WeekDays"] = Days.Select(x => new SelectListItem() { Text = x, Value = x, Selected = x == lesson.WeekDay });
            ViewData["Subjects"] = Subjects.Select(x => new SelectListItem() { Text = x, Value = x, Selected = x == lesson.Type });
            return View(lesson);
        }

        // GET: Lessons/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Lessons == null)
            {
                return NotFound();
            }

            var lesson = await _context.Lessons.FindAsync(id);
            if (lesson == null)
            {
                return NotFound();
            }
            ViewData["Teacher"] = new SelectList(_context.Teachers, "Id", null, lesson.TeacherId);
            ViewData["WeekDays"] = Days.Select(x => new SelectListItem() { Text = x, Value = x, Selected = x == lesson.WeekDay });
            ViewData["Subjects"] = Subjects.Select(x => new SelectListItem() { Text = x, Value = x, Selected = x == lesson.Type });
            return View(lesson);
        }

        // POST: Lessons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,WeekDay,DateStart,Duration,TeacherId")] Lesson lesson)
        {
            if (id != lesson.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lesson);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LessonExists(lesson.Id))
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
            ViewData["Teacher"] = new SelectList(_context.Teachers, "Id", "Name", lesson.Teacher);
            ViewData["WeekDays"] = Days.Select(x => new SelectListItem() { Text = x, Value = x, Selected = x == lesson.WeekDay });
            ViewData["Subjects"] = Subjects.Select(x => new SelectListItem() { Text = x, Value = x, Selected = x == lesson.Type });
            return View(lesson);
        }

        // GET: Lessons/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Lessons == null)
            {
                return NotFound();
            }

            var lesson = await _context.Lessons
                .Include(l => l.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lesson == null)
            {
                return NotFound();
            }

            return View(lesson);
        }

        // POST: Lessons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Lessons == null)
            {
                return Problem("Entity set 'SchoolBackofficeContext.Lesson'  is null.");
            }
            var lesson = await _context.Lessons.FindAsync(id);
            if (lesson != null)
            {
                _context.Lessons.Remove(lesson);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LessonExists(int id)
        {
          return (_context.Lessons?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
