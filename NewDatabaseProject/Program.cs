using System.Text.Json;
using NewDatabaseProject.Dtos;
using NewDatabaseProject.Models;

string userInput;
await using var context = new GymDbContext();

var helpText = "Accepted commands:\n" +
               "/help - It does this thing, obviously.\n" +
               "/exit and /quit - Both of these do the same thing, which\n" +
               "is exiting the program.\n" +
               "/query - Allows you to query the database." +
               "/addUser - Starts the process of adding yourself as a user.\n" +
               "/deleteUser - Deletes yourself from our database.\n" +
               "/changeMembership - Allows you to upgrade or downgrade your\n" +
               "membership." +
               "/apply - Starts the process of onboarding you to become\n" +
               "a trainer." +
               "/addClass - Allows you to add a class to your schedule.\n" +
               "/deleteClass - Allows your to delete a class on your schedule.\n";

Console.WriteLine("Hello, and welcome to the GymFit Database!\n" +
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
    var classJsonStream = new StreamReader("/NewDatabaseProject/Data/Classes.json");
    var memberJsonStream = new StreamReader("/NewDatabaseProject/Data/Members.json");
    var trainerJsonStream = new StreamReader("/NewDatabaseProject/Data/Trainers.json");
    var MembershipTypeJsonStream = new StreamReader("/NewDatabaseProject/Data/MembershipTypes.json");

    var Classes = JsonSerializer.Deserialize<PaginatedListDto<ClassModel>>(classJsonStream.ToString());
    var Members = JsonSerializer.Deserialize<PaginatedListDto<MemberModel>>(memberJsonStream.ToString());
    var Trainers = JsonSerializer.Deserialize<PaginatedListDto<TrainerModel>>(trainerJsonStream.ToString());
    var MembershipTypes = JsonSerializer.Deserialize<PaginatedListDto<MembershipTypeModel>>(MembershipTypeJsonStream.ToString());

    context.Classes.AddRange(Classes.Data);
    context.Members.AddRange(Members.Data);
    context.Trainers.AddRange(Trainers.Data);
    context.MembershipTypes.AddRange(MembershipTypes.Data);


    await context.SaveChangesAsync();
}