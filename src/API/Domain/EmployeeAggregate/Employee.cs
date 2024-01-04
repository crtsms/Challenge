using Challenge.API.Domain.SeedWork;

namespace Challenge.API.Domain.EmployeeAggregate;
public class Employee(string firstName, string lastName, string phoneNumber)
        : Entity, IAggregateRoot
{
    public string FirstName { get; private set; } = firstName;

    public string LastName { get; private set; } = lastName;

    public string PhoneNumber { get; private set; } = phoneNumber;

        private readonly List<EmployeeSkill> _skills = [];
    public IReadOnlyCollection<EmployeeSkill> Skills => _skills;

    public void AddSkill(Skill skill, int yearsExperience)
    {
        if (Skills.Any(x => x.Id == skill.Id))
            return;
            
        _skills.Add(new EmployeeSkill(this, skill, yearsExperience));
    }
}