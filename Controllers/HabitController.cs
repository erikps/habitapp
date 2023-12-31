using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using habitapp.Data;
using habitapp.Models;

namespace habitapp.Controllers
{
    public class HabitController : Controller
    {
        private readonly HabitContext _context;

        public HabitController(HabitContext context)
        {
            _context = context;
        }

        // GET: Habit
        public async Task<IActionResult> Index()
        {
            return _context.Habit != null ?
                        View(await _context.Habit.ToListAsync()) :
                        Problem("Entity set 'HabitContext.Habit'  is null.");
        }

        // GET: Habit/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Habit == null)
            {
                return NotFound();
            }

            var habit = await _context.Habit
                .FirstOrDefaultAsync(m => m.id == id);
            if (habit == null)
            {
                return NotFound();
            }

            return View(habit);
        }

        // GET: Habit/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Habit/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Name,Description,Interval,startDate")] Habit habit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(habit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(habit);
        }

        // GET: Habit/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Habit == null)
            {
                return NotFound();
            }

            var habit = await _context.Habit.FindAsync(id);
            if (habit == null)
            {
                return NotFound();
            }
            return View(habit);
        }

        // POST: Habit/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Name,Description,Interval")] Habit habit)
        {
            if (id != habit.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(habit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HabitExists(habit.id))
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
            return View(habit);
        }

        // GET: Habit/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Habit == null)
            {
                return NotFound();
            }

            var habit = await _context.Habit
                .FirstOrDefaultAsync(m => m.id == id);
            if (habit == null)
            {
                return NotFound();
            }

            return View(habit);
        }

        // POST: Habit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Habit == null)
            {
                return Problem("Entity set 'HabitContext.Habit'  is null.");
            }
            var habit = await _context.Habit.FindAsync(id);
            if (habit != null)
            {
                _context.Habit.Remove(habit);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Dashboard()
        {

            return _context.Habit != null ?
                View(await _context.Habit.Include((Habit habit) => habit.CompletionHistory).ToListAsync()) :
                Problem("Entity set 'HabitContext.Habit' is null.");
        }

        [HttpPost]
        public async Task<IActionResult> DashboardSubmit(int id, bool completed)
        {
            // This is the habit being edited
            var habit = await _context.Habit.Include(h => h.CompletionHistory).Where((Habit habit) => habit.id == id).FirstAsync();

            if (habit == null)
            {
                return Problem("Habit with the provided id does not exist.");
            }


            // If the habit has been completed, add today as a completion, otherwise remove any such completions
            if (completed)
            {
                var completion = new HabitCompletion { CompletedOn = DateTime.Today };
                habit.CompletionHistory.Add(completion);
            }
            else
            {
                // Remove any completions for today.
                await _context.HabitCompletions
                    .Where((HabitCompletion completion) =>
                        habit.CompletionHistory.Contains(completion) && completion.CompletedOn == DateTime.Today)
                    .ExecuteDeleteAsync();
            }

            await _context.SaveChangesAsync();
            return Redirect("Habit/Dashboard");
        }

        private bool HabitExists(int id)
        {
            return (_context.Habit?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
