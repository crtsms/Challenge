using AutoMapper;

namespace Challenge.API.Model;

public class EmployeeServices(
    EmployeeContext context,
    IMapper mapper,
    ILogger<EmployeeServices> logger)
{
    public EmployeeContext Context { get; } = context;
    public IMapper Mapper { get; } = mapper;
    public ILogger<EmployeeServices> Logger { get; } = logger;
};