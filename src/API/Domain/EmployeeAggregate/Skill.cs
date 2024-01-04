using Challenge.API.Domain.SeedWork;

namespace Challenge.API.Domain.EmployeeAggregate;

public class Skill(string name, string description)
    : Entity, IAggregateRoot
{
    public string Name { get; private set; } = name;
    public string Description { get; private set; } = description;
}