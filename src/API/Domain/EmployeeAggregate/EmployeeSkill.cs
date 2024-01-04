using Challenge.API.Domain.SeedWork;

namespace Challenge.API.Domain.EmployeeAggregate;
public class EmployeeSkill
        : Entity, IAggregateRoot
{
    #pragma warning disable CS8618
    private EmployeeSkill(){}
    #pragma warning restore CS8618

    public EmployeeSkill (Employee employee, Skill skill, int yearsExperience){
        YearsExperience = yearsExperience;
        Employee = employee;
        Skill = skill;
    }

    public int YearsExperience { get; private set; }

    public Employee Employee { get; private set; }

    public Skill Skill { get; private set; }
}