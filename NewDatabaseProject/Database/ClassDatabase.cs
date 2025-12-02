using System.ComponentModel.DataAnnotations;

namespace NewDatabaseProject.Database;

public class ClassDatabase
{  [Key] public required string name { get; set; } = string.Empty;
   [Key] public required string date { get; set; } = string.Empty;
   [Key] public required string time { get; set; } = string.Empty;
   public required string minimum_membership_access { get; set; } = string.Empty;
   public ICollection<TrainerDatabase> trainer { get; } = new List<TrainerDatabase>();
   public ICollection<MemberDatabase> members { get; } = new List<MemberDatabase>();

}