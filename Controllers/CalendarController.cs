using System.Globalization;
using habitapp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace habitapp.Controllers
{

    public class CalendarController : Controller
    {

        private HabitContext _context;

        public CalendarController(HabitContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Month()
        {
            if (_context.Habit == null)
            {
                return Problem("HabitContext.Habit is null.");
            }

            var calendar = new GregorianCalendar();
            var today = DateTime.Now;

            var days = calendar.GetDaysInMonth(today.Year, today.Month);
            ViewBag.days = days;
            ViewBag.today = today;

            var habits = await _context.Habit.ToListAsync();
            return View(habits);
        }

        public IActionResult Week()
        {
            return View();
        }

    }

}