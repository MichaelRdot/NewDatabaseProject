using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NewDatabaseProject.Models;

public class MembershipTypeModel
{
    [Key]
    [JsonPropertyName("Access_Level")]
    public required string AccessLevel { get; set; } = string.Empty;

    [JsonPropertyName("Price")] public required float Price { get; set; }
}