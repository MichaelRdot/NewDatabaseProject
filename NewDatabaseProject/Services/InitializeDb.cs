using System.Text.Json;
using NewDatabaseProject.Dtos;
using NewDatabaseProject.Models;

namespace NewDatabaseProject.Services;

public class InitializeDb
{
    public async Task FromData(string jsonString)
    {
        var Members = JsonSerializer.Deserialize<PaginatedListDto<MemberModel>>(jsonString);

        using (var context = new GymDbContext())
        {
            context.Members.AddRange(Members.Data);

            await context.SaveChangesAsync();
        }
    }
}