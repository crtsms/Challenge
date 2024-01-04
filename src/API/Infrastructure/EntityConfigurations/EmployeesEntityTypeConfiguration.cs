using Challenge.API.Domain.EmployeeAggregate;

namespace Challenge.API.Infrastructure.EntityConfigurations;

class EmployeesEntityTypeConfiguration
    : IEntityTypeConfiguration<Domain.EmployeeAggregate.Employee>
{
    public void Configure(EntityTypeBuilder<Domain.EmployeeAggregate.Employee> builder)
    {
        builder.ToTable("Employee");

        builder.Property(cb => cb.FirstName)
            .HasMaxLength(100);

        builder.Property(cb => cb.LastName)
            .HasMaxLength(100);
    }
}