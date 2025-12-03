using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NewDatabaseProject.Models;

public class TrainerModel
{
    [Key] [JsonPropertyName("Employee_Id")] public required string EmployeeId { get; set; } = string.Empty;
    [JsonPropertyName("Membership_Type")] public required string MembershipType { get; set; } = string.Empty;
    [JsonPropertyName("Name")] public required string Name { get; set; } = string.Empty;
    [JsonPropertyName("Classes_Teaching")] public ICollection<ClassModel> ClassesTeaching { get; } = new List<ClassModel>();
    [JsonPropertyName("Birthday")] public string Birthday { get; set; } = string.Empty;
    [JsonPropertyName("Picture")] public required string Picture { get; set; } = string.Empty;
    [JsonPropertyName("Date_Started_Working")] public required string DateStartedWorking { get; set; } = string.Empty;
}