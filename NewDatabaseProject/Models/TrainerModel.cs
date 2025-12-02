using System.ComponentModel.DataAnnotations;

namespace NewDatabaseProject.Models;

public class TrainerModel
{
    [Key] public required string employee_id { get; set; } = string.Empty;
    public required string membership_type { get; set; } = string.Empty;
    public required string name { get; set; } = string.Empty;
    public ICollection<ClassModel> Classes_Teaching { get; } = new List<ClassModel>();
    public string birthday { get; set; } = string.Empty;
    public required string picture { get; set; } = string.Empty;
    public required string date_started_working { get; set; } = string.Empty;
}