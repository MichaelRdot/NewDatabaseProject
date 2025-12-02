using System.ComponentModel.DataAnnotations;

namespace NewDatabaseProject.Database;

public class MembershipTypeDatabase
{
   [Key] public required string Access_Level { get; set; } = string.Empty;
    public required string price { get; set; } = string.Empty;

}