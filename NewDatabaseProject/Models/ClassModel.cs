using Microsoft.EntityFrameworkCore;

namespace NewDatabaseProject.Models;

[PrimaryKey(nameof(name), nameof(date), nameof(time))]
public class ClassModel
{
    public required string name { get; set; } = string.Empty;
    public required string date { get; set; } = string.Empty;
    public required string time { get; set; } = string.Empty;
    public required string minimum_membership_access { get; set; } = string.Empty;
    public ICollection<TrainerModel> trainer { get; } = new List<TrainerModel>();
    public ICollection<MemberModel> members { get; } = new List<MemberModel>();
}