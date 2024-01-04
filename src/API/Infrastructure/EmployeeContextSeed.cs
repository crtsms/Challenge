using System.Text.Json;

namespace Challenge.API.Infrastructure;

public partial class EmployeeContextSeed(
    IWebHostEnvironment env,
    ILogger<EmployeeContextSeed> logger) : IDbSeeder<EmployeeContext>
{
    public async Task SeedAsync(EmployeeContext context)
    {
        var contentRootPath = env.ContentRootPath;

        if (!context.Skills.Any())
        {
            //Seed the skills
            var sourcePath = Path.Combine(contentRootPath, "Data", "skills.json");
            var sourceJson = File.ReadAllText(sourcePath);
            var sourceItems = JsonSerializer.Deserialize<Domain.EmployeeAggregate.Skill[]>(sourceJson);

            context.Skills.RemoveRange(context.Skills);

            if (sourceItems is not null)
                await context.Skills.AddRangeAsync(sourceItems);

            await context.SaveChangesAsync();
            logger.LogInformation("Seeded skills with {NumSkills} skills", context.Skills.Count());

        }

        if (!context.Employees.Any())
        {
            //Seed the employees
            var sourcePath = Path.Combine(contentRootPath, "Data", "employees.json");
            var sourceJson = File.ReadAllText(sourcePath);
            var sourceItems = JsonSerializer.Deserialize<List<Domain.EmployeeAggregate.Employee>>(sourceJson);

            var skills = context.Skills.ToList();
            Random random = new();

            sourceItems?.ForEach(x =>
            {
                var i = 0;
                var items = random.Next(0, 10);
                while (i < items)
                {
                    i++;
                    x.AddSkill(skills[random.Next(0, skills.Count)], random.Next(1, 30));
                }
            });

            context.Employees.RemoveRange(context.Employees);

            if (sourceItems is not null)
                await context.Employees.AddRangeAsync(sourceItems);

            await context.SaveChangesAsync();
            logger.LogInformation("Seeded employees with {NumEmployees} employees", context.Employees.Count());

        }
    }
}