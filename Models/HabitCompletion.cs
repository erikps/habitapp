using System.ComponentModel.DataAnnotations;


namespace habitapp.Models;

public class HabitCompletion
{

    public int id { get; set; }
    public Habit CompletedHabit { get; set; }
    public DateTime CompletedOn { get; set; }
}