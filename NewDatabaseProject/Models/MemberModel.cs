using System.ComponentModel.DataAnnotations;
namespace NewDatabaseProject.Models;

public class MemberModel
{
    [Key] public required string email { get; set; } = string.Empty; //maybe replace discord Id with the password. 
    public required string membership_type { get; set; } = string.Empty;
    public required string name { get; set; } = string.Empty;
    public string birthday { get; set; } = string.Empty;
    public required string picture { get; set; } = string.Empty;
    public ICollection<ClassModels> Classes { get; } = new List<ClassModels>();
    public required string date_signed_up { get; set; } = string.Empty;

}
