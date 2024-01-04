using Challenge.API.Domain.EmployeeAggregate;

namespace Challenge.API.Infrastructure.EntityConfigurations;

class SkillsEntityTypeConfiguration
    : IEntityTypeConfiguration<Domain.EmployeeAggregate.Skill>
{
    public void Configure(EntityTypeBuilder<Domain.EmployeeAggregate.Skill> builder)
    {
        builder.ToTable("Skill");

        builder.Property(cb => cb.Description)
            .HasMaxLength(4000);

        builder.Property(cb => cb.Description)
            .HasMaxLength(250);
    }
}