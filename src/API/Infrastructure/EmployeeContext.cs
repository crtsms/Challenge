using Challenge.API.Domain.EmployeeAggregate;
using Challenge.API.Infrastructure.EntityConfigurations;

namespace Challenge.API.Infrastructure;

public class EmployeeContext(DbContextOptions<EmployeeContext> options) : DbContext(options)
{
    public DbSet<Domain.EmployeeAggregate.Employee> Employees { get; set; }
    public DbSet<EmployeeSkill> EmployeeSkills { get; set; }
    public DbSet<Domain.EmployeeAggregate.Skill> Skills { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new EmployeesEntityTypeConfiguration());
        builder.ApplyConfiguration(new EmployeeSkillTypeConfiguration());
        builder.ApplyConfiguration(new SkillsEntityTypeConfiguration());
    }
}