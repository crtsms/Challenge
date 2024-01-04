using Challenge.API.Domain.EmployeeAggregate;

namespace Challenge.API.Infrastructure.EntityConfigurations;

class EmployeeSkillTypeConfiguration
    : IEntityTypeConfiguration<EmployeeSkill>
{
    public void Configure(EntityTypeBuilder<EmployeeSkill> builder)
    {
        builder.ToTable("EmployeeSkill");

        builder
            .HasOne(e => e.Employee)
            .WithMany(e => e.Skills);
        
        builder
            .HasOne(e => e.Skill)
            .WithMany();
    }
}