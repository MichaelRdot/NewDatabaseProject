using System.ComponentModel.DataAnnotations;

namespace NewDatabaseProject.Database;

public class ClassModels
{  [Key] public required string name { get; set; } = string.Empty;
   [Key] public required string date { get; set; } = string.Empty;
   [Key] public required string time { get; set; } = string.Empty;
   public required string minimum_membership_access { get; set; } = string.Empty;
   public ICollection<TrainerModel> trainer { get; } = new List<TrainerModel>();
   public ICollection<MemberModel> members { get; } = new List<MemberModel>();

}