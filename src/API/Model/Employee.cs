namespace Challenge.API.Model;
public record Employee(string FirstName, string LastName, string PhoneNumber, List<EmployeeSkill> Skills);