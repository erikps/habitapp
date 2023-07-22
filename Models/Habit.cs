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
}