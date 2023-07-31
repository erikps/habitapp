using habitapp.Data;
using habitapp.Models;

namespace habitapp.Services
{
    public class HabitDateService
    {

        private readonly HabitContext _context;

        public HabitDateService(HabitContext context)
        {
            _context = context;
        }

    }
}
