using System.ComponentModel.DataAnnotations;

namespace NewDatabaseProject.Models;

public class MembershipTypeModel
{
    [Key] public required string Access_Level { get; set; } = string.Empty;
    public required string price { get; set; } = string.Empty;
}