using System.Text;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using NewDatabaseProject.Dtos;
using NewDatabaseProject.Models;
using NewDatabaseProject.Database;

string userInput;

InitializeDb();

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
            case "/apply":
                Apply();
                continue;
            case "/addClass":
                AddClass();
                continue;
            case "/deleteClass":
                DeleteClass();
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
    using var context = new GymDbContext();
    while (true)
    {
        Console.WriteLine("What would you like to query?");
        Console.WriteLine("1 - Members");
        Console.WriteLine("2 - Trainers");
        Console.WriteLine("3 - Classes");
        Console.WriteLine("4 - Membership types");
        Console.WriteLine("5 - Check Class Roster");
        Console.WriteLine("Press Enter without typing anything to go back.");
        var choice = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(choice))
        {
            break;
        }

        switch (choice.Trim().ToLower())
        {
            case "1":
            case "members":
                Console.Write("Enter member email or part of the name (leave empty to list all): ");
                var memberSearch = Console.ReadLine();
                var membersQuery = context.Members.AsQueryable();
                if (!string.IsNullOrWhiteSpace(memberSearch))
                {
                    var term = memberSearch.Trim();
                    membersQuery = membersQuery.Where(m => m.MemberEmail == term || m.Name.Contains(term));
                }

                foreach (var m in membersQuery)
                {
                    Console.WriteLine($"{m.Name} ({m.MemberEmail}) - {m.MembershipType}");
                }

                break;

            case "2":
            case "trainers":
                Console.Write("Enter trainer id or part of the name (leave empty to list all): ");
                var trainerSearch = Console.ReadLine();
                var trainersQuery = context.Trainers.AsQueryable();
                if (!string.IsNullOrWhiteSpace(trainerSearch))
                {
                    var term = trainerSearch.Trim();
                    trainersQuery = trainersQuery.Where(t => t.EmployeeId == term || t.Name.Contains(term));
                }

                foreach (var t in trainersQuery)
                {
                    Console.WriteLine($"{t.EmployeeId}: {t.Name} ({t.MembershipType})");
                }

                break;
            case "3":
            case "classes":
                Console.Write("Enter class name (leave empty to list all): ");
                var classSearch = Console.ReadLine();
                var classesQuery = context.Classes.AsQueryable();
                if (!string.IsNullOrWhiteSpace(classSearch))
                {
                    var term = classSearch.Trim();
                    classesQuery = classesQuery.Where(c => c.Name.Contains(term));
                }

                foreach (var c in classesQuery)
                {
                    Console.WriteLine($"{c.Name} on {c.Date} at {c.Time} (min membership: {c.MinimumMembershipAccess})");
                }

                break;
            case "4":
            case "membership":
            case "membership types":
                foreach (var mt in context.MembershipTypes)
                {
                    Console.WriteLine($"{mt.AccessLevel} - {mt.Price:C}");
                }

                break;
            default:
                Console.WriteLine("Unknown option.");
                break;
        }

        Console.WriteLine();
    }
}

void AddUser()
{
    using var context = new GymDbContext();

    Console.Write("Enter your email: ");
    var email = Console.ReadLine()?.Trim();
    if (string.IsNullOrWhiteSpace(email))
    {
        Console.WriteLine("Email is required.");
        return;
    }

    if (context.Members.Any(m => m.MemberEmail == email))
    {
        Console.WriteLine("A member with that email already exists.");
        return;
    }

    Console.Write("Enter your full name: ");
    var name = Console.ReadLine()?.Trim();
    if (string.IsNullOrWhiteSpace(name))
    {
        Console.WriteLine("Name is required.");
        return;
    }

    var membershipTypes = context.MembershipTypes.ToList();
    if (membershipTypes.Count == 0)
    {
        Console.WriteLine("No membership types are configured.");
        return;
    }

    Console.WriteLine("Available membership types:");
    foreach (var mt in membershipTypes)
    {
        if (mt.AccessLevel is not "Staff") Console.WriteLine($"- {mt.AccessLevel} ({mt.Price:C})");
    }

    Console.Write("Choose membership type: ");
    string membership = Console.ReadLine()?.Trim() ?? string.Empty;
    if (membership == "Staff")
    {
        Console.WriteLine("Invalid membership type.");
        return;
    }
    var selectedType = membershipTypes
        .FirstOrDefault(t => string.Equals(t.AccessLevel, membership, StringComparison.OrdinalIgnoreCase));

    if (selectedType == null)
    {
        Console.WriteLine("Invalid membership type.");
        return;
    }

    Console.Write("Enter your birthday (YYYY-MM-DD, optional): ");
    var birthday = Console.ReadLine()?.Trim() ?? string.Empty;

    Console.Write("Enter a picture file name for your profile: ");
    var picture = Console.ReadLine()?.Trim();
    if (string.IsNullOrWhiteSpace(picture))
    {
        picture = "default.jpg";
    }

    var member = new MemberModel
    {
        MemberEmail = email,
        Name = name,
        MembershipType = selectedType.AccessLevel,
        Birthday = birthday ?? string.Empty,
        Picture = picture,
        DateSignedUp = DateTime.Today.ToString("yyyy-MM-dd")
    };

    context.Members.Add(member);
    context.SaveChanges();
    Console.WriteLine("You have been added as a member.");
}

void DeleteUser()
{
    using var context = new GymDbContext();

    Console.Write("Enter the email of the member to delete: ");
    var email = Console.ReadLine()?.Trim();
    if (string.IsNullOrWhiteSpace(email))
    {
        Console.WriteLine("Email is required.");
        return;
    }

    var member = context.Members.FirstOrDefault(m => m.MemberEmail == email);
    if (member == null)
    {
        Console.WriteLine("No member with that email was found.");
        return;
    }

    Console.Write($"Are you sure you want to delete {member.Name}? Type 'yes' to confirm: ");
    var confirm = Console.ReadLine();
    if (!string.Equals(confirm?.Trim(), "yes", StringComparison.OrdinalIgnoreCase))
    {
        Console.WriteLine("Aborted.");
        return;
    }

    context.Members.Remove(member);
    context.SaveChanges();
    Console.WriteLine("Member deleted.");
}

void ChangeMembership()
{
    using var context = new GymDbContext();

    Console.Write("Enter your email: ");
    var email = Console.ReadLine()?.Trim();
    if (string.IsNullOrWhiteSpace(email))
    {
        Console.WriteLine("Email is required.");
        return;
    }

    var member = context.Members.FirstOrDefault(m => m.MemberEmail == email);
    if (member == null)
    {
        Console.WriteLine("No member with that email was found.");
        return;
    }

    Console.WriteLine($"Current membership: {member.MembershipType}");

    var membershipTypes = context.MembershipTypes.ToList();
    if (membershipTypes.Count == 0)
    {
        Console.WriteLine("No membership types are configured.");
        return;
    }

    Console.WriteLine("Available membership types:");
    foreach (var mt in membershipTypes)
    {
        Console.WriteLine($"- {mt.AccessLevel} ({mt.Price:C})");
    }

    Console.Write("Enter new membership type: ");
    var membership = Console.ReadLine()?.Trim();
    var selectedType = membershipTypes
        .FirstOrDefault(t => string.Equals(t.AccessLevel, membership, StringComparison.OrdinalIgnoreCase));

    if (selectedType == null)
    {
        Console.WriteLine("Invalid membership type.");
        return;
    }

    member.MembershipType = selectedType.AccessLevel;
    context.SaveChanges();
    Console.WriteLine("Membership updated.");
}

void Apply()
{
    using var context = new GymDbContext();

    Console.Write("Enter your full name: ");
    var name = Console.ReadLine()?.Trim();
    if (string.IsNullOrWhiteSpace(name))
    {
        Console.WriteLine("Name is required.");
        return;
    }

    Console.Write("Enter your birthday (YYYY-MM-DD, optional): ");
    var birthday = Console.ReadLine()?.Trim() ?? string.Empty;

    Console.Write("Enter a picture file name for your profile: ");
    var picture = Console.ReadLine()?.Trim();
    if (string.IsNullOrWhiteSpace(picture))
    {
        picture = "default_trainer.jpg";
    }

    var staffMembership = context.MembershipTypes
        .FirstOrDefault(t => t.AccessLevel == "Staff");

    var membershipType = staffMembership?.AccessLevel ?? "Staff";

    var existingIds = context.Trainers.Select(t => t.EmployeeId).ToList();
    var nextNumber = 1;
    foreach (var id in existingIds)
    {
        if (!string.IsNullOrWhiteSpace(id) &&
            id.Length > 1 &&
            char.ToUpperInvariant(id[0]) == 'T' &&
            int.TryParse(id[1..], out var n) &&
            n >= nextNumber)
        {
            nextNumber = n + 1;
        }
    }

    var employeeId = $"T{nextNumber:D3}";

    var trainer = new TrainerModel
    {
        EmployeeId = employeeId,
        Name = name,
        MembershipType = membershipType,
        Birthday = birthday ?? string.Empty,
        Picture = picture,
        DateStartedWorking = DateTime.Today.ToString("yyyy-MM-dd")
    };

    context.Trainers.Add(trainer);
    context.SaveChanges();
    Console.WriteLine($"Application recorded. Your trainer id is {trainer.EmployeeId}.");
}

void AddClass()
{
    using var context = new GymDbContext();

    Console.Write("Enter your email: ");
    var email = Console.ReadLine()?.Trim();
    if (string.IsNullOrWhiteSpace(email))
    {
        Console.WriteLine("Email is required.");
        return;
    }

    var member = context.Members.FirstOrDefault(m => m.MemberEmail == email);
    if (member == null)
    {
        Console.WriteLine("No member with that email was found.");
        return;
    }

    Console.Write("Enter class name: ");
    var name = Console.ReadLine()?.Trim();
    Console.Write("Enter class date (YYYY-MM-DD): ");
    var date = Console.ReadLine()?.Trim();
    Console.Write("Enter class time (HH:mm): ");
    var time = Console.ReadLine()?.Trim();

    if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(date) || string.IsNullOrWhiteSpace(time))
    {
        Console.WriteLine("Class name, date and time are required.");
        return;
    }

    var classEntity = context.Classes
        .FirstOrDefault(c => c.Name == name && c.Date == date && c.Time == time);

    if (classEntity == null)
    {
        Console.WriteLine("No such class exists.");
        return;
    }

    member.Classes.Add(classEntity);
    classEntity.Members.Add(member);
    context.SaveChanges();
    Console.WriteLine("Class added to your schedule.");
}

void DeleteClass()
{
    using var context = new GymDbContext();

    Console.Write("Enter your email: ");
    var email = Console.ReadLine()?.Trim();
    if (string.IsNullOrWhiteSpace(email))
    {
        Console.WriteLine("Email is required.");
        return;
    }

    var member = context.Members.FirstOrDefault(m => m.MemberEmail == email);
    if (member == null)
    {
        Console.WriteLine("No member with that email was found.");
        return;
    }

    context.Entry(member).Collection(m => m.Classes).Load();

    Console.Write("Enter class name: ");
    var name = Console.ReadLine()?.Trim();
    Console.Write("Enter class date (YYYY-MM-DD): ");
    var date = Console.ReadLine()?.Trim();
    Console.Write("Enter class time (HH:mm): ");
    var time = Console.ReadLine()?.Trim();

    if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(date) || string.IsNullOrWhiteSpace(time))
    {
        Console.WriteLine("Class name, date and time are required.");
        return;
    }

    var classEntity = context.Classes
        .FirstOrDefault(c => c.Name == name && c.Date == date && c.Time == time);
    if (classEntity == null)
    {
        Console.WriteLine("No such class exists.");
        return;
    }

    context.Entry(classEntity).Collection(c => c.Members).Load();

    if (!member.Classes.Contains(classEntity))
    {
        Console.WriteLine("That class is not in your schedule.");
        return;
    }

    member.Classes.Remove(classEntity);
    classEntity.Members.Remove(member);
    context.SaveChanges();
    Console.WriteLine("Class removed from your schedule.");
}

async Task InitializeDb()
{
    await using var context = new GymDbContext();
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