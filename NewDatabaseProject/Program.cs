using System.Text.Json;
using NewDatabaseProject.Dtos;
using NewDatabaseProject.Models;
using NewDatabaseProject.Database;

string userInput;
await using var context = new GymDbContext();

var helpText = "Accepted commands:\n" +
               "/help - It does this thing, obviously.\n" +
               "/exit and /quit - Both of these do the same thing, which " +
               "is exiting the program.\n" +
               "/query - Allows you to query the database.\n" +
               "/addUser - Starts the process of adding yourself as a user.\n" +
               "/deleteUser - Deletes yourself from our database.\n" +
               "/changeMembership - Allows you to upgrade or downgrade your " +
               "membership.\n" +
               "/apply - Starts the process of onboarding you to become " +
               "a trainer.\n" +
               "/addClass - Allows you to add a class to your schedule.\n" +
               "/deleteClass - Allows your to delete a class on your schedule.\n";

Console.WriteLine("\n\nHello, and welcome to the GymFit Database!\n" +
                  "Here you can query the database, add yourself as a user,\n" +
                  "delete yourself if your are currently a user, modify your\n" +
                  "Gym schedule, modify your membership, apply to become a\n" +
                  "trainer.\n" +
                  "     For help with commands, please type \"/help\" and then\n" +
                  "press the \"Enter\" key on your keyboard.\n");
AwaitInitialResponse();
Console.WriteLine("Goodbye! Please come again. And spend more money next time!!\n");

void AwaitInitialResponse()
{
    while (true)
    {
        userInput = Console.ReadLine();
        switch (userInput)
        {
            case "/help":
                Console.WriteLine(helpText);
                continue;
            case "/query":
                Query();
                continue;
            case "/addUser":
                AddUser();
                continue;
            case "/deleteUser":
                DeleteUser();
                continue;
            case "/changeMembership":
                ChangeMembership();
                continue;
            case "/apply ":
                Apply();
                continue;
            case "/addClass":
                AddClass();
                continue;
            case "/deleteClass":
                DeleteClass();
                continue;
            case "/initialize":
                InitializeDb();
                continue;
            case "/exit" or "/quit":
                break;
            default:
                continue;
        }

        break;
    }
}

void Query()
{
}

void AddUser()
{
}

void DeleteUser()
{
}

void ChangeMembership()
{
}

void Apply()
{
}

void AddClass()
{
}

void DeleteClass()
{
}


async Task InitializeDb()
{
     var basePath = Path.Combine(Directory.GetCurrentDirectory(), "Data");

     var classesJson = File.ReadAllText(Path.Combine(basePath, "Classes.json"));
     var membersJson = File.ReadAllText(Path.Combine(basePath, "Members.json"));
     var trainersJson = File.ReadAllText(Path.Combine(basePath, "Trainers.json"));
     var membershipTypesJson = File.ReadAllText(Path.Combine(basePath, "MembershipTypes.json"));

     var classes = JsonSerializer.Deserialize<PaginatedListDto<ClassModel>>(classesJson);
     var members = JsonSerializer.Deserialize<PaginatedListDto<MemberModel>>(membersJson);
     var trainers = JsonSerializer.Deserialize<PaginatedListDto<TrainerModel>>(trainersJson);
     var membershipTypes = JsonSerializer.Deserialize<PaginatedListDto<MembershipTypeModel>>(membershipTypesJson);
 
     context.Classes.AddRange(classes.Data);
     context.Members.AddRange(members.Data);
     context.Trainers.AddRange(trainers.Data);
     context.MembershipTypes.AddRange(membershipTypes.Data);
     
    await context.SaveChangesAsync();
}