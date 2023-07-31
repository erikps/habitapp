using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using habitapp.Models;

namespace habitapp.Data
{
    public class HabitContext : DbContext
    {
        public HabitContext (DbContextOptions<HabitContext> options)
            : base(options)
        {
        }

        public DbSet<habitapp.Models.Habit> Habit { get; set; } = default!;
        public DbSet<habitapp.Models.HabitCompletion> HabitCompletions { get; set; } = default!;
    }
}
