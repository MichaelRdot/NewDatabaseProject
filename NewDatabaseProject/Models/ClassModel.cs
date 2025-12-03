using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace NewDatabaseProject.Models;

[PrimaryKey(nameof(Name), nameof(Date), nameof(Time))]
public class ClassModel
{
    [JsonPropertyName("Name")] public required string Name { get; set; } = string.Empty;
    [JsonPropertyName("Date")] public required string Date { get; set; } = string.Empty;
    [JsonPropertyName("Time")] public required string Time { get; set; } = string.Empty;
    [JsonPropertyName("Minimum_Membership_Access")] public required string MinimumMembershipAccess { get; set; } = string.Empty;
    [JsonPropertyName("Trainer")] public ICollection<TrainerModel> Trainer { get; } = new List<TrainerModel>();
    [JsonPropertyName("Members")] public ICollection<MemberModel> Members { get; } = new List<MemberModel>();
}