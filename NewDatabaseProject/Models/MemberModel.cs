using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NewDatabaseProject.Models;

public class MemberModel
{
    [Key]
    [JsonPropertyName("Member_Email")]
    public required string MemberEmail { get; set; } = string.Empty;

    [JsonPropertyName("Membership_Type")] public required string MembershipType { get; set; } = string.Empty;
    [JsonPropertyName("Name")] public required string Name { get; set; } = string.Empty;
    [JsonPropertyName("Birthday")] public string Birthday { get; set; } = string.Empty;
    [JsonPropertyName("Picture")] public required string Picture { get; set; } = string.Empty;
    [JsonPropertyName("Classes")] public ICollection<ClassModel> Classes { get; } = new List<ClassModel>();
    [JsonPropertyName("Date_Signed_Up")] public required string DateSignedUp { get; set; } = string.Empty;
}