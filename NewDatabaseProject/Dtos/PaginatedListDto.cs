using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace NewDatabaseProject.Dtos;

public class PaginatedListDto<T>
{
    [JsonPropertyName("Object")] public required string ObjectType { get; set; }
    [JsonPropertyName("Data")] public required List<T> Data { get; set; }
}