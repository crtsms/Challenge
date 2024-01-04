using AutoMapper;

public class MapperProfile : Profile
{
	public MapperProfile()
	{
		CreateMap<Challenge.API.Domain.EmployeeAggregate.Employee, Challenge.API.Model.Employee>();
        CreateMap<Challenge.API.Domain.EmployeeAggregate.Skill, Challenge.API.Model.Skill>();
        CreateMap<Challenge.API.Domain.EmployeeAggregate.EmployeeSkill, Challenge.API.Model.EmployeeSkill>();
	}
}