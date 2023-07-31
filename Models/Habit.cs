using System.ComponentModel.DataAnnotations;

namespace habitapp.Models;


public class Habit
{
    public int id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    [Range(1, 7)]
    public int Interval { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Start Date")]
    public DateTime startDate { get; set; }

    public ICollection<HabitCompletion> CompletionHistory { get; set; } = new List<HabitCompletion>();

    // Should this habit be done on the given day?
    public bool IsOnDate(DateTime day)
    {
        return (startDate - day).Days % Interval == 0;
    }

    // Has the habit been completed on the given day?
    public bool HasBeenCompletedOn(DateTime day)
    {
        return CompletionHistory.Any((HabitCompletion completion) => completion.CompletedOn == day);
    }
}