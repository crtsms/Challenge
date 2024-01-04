using Challenge.API.Model;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Challenge.API;
public static class Employee
{
    public static IEndpointRouteBuilder MapEmployeeApi(this IEndpointRouteBuilder app)
    {
        // Routes for querying catalog items.
        app.MapGet("/employees", GetAllEmployees);
        return app;
    }

    public static async Task<Results<Ok<PaginatedItems<Model.Employee>>, BadRequest<string>>> GetAllEmployees(
        [AsParameters] PaginationRequest paginationRequest,
        [AsParameters] EmployeeServices services)
    {
        var pageSize = paginationRequest.PageSize;
        var pageIndex = paginationRequest.PageIndex;

        var totalItems = await services.Context.Employees
            .LongCountAsync();

        var itemsOnPage = await services.Context.Employees
            .Include(i => i.Skills)
            .ThenInclude(i => i.Skill)
            .OrderBy(c => c.Id)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync();

        return TypedResults.Ok(
            new PaginatedItems<Model.Employee>(
                pageIndex, 
                pageSize, 
                totalItems, 
                services.Mapper.Map<List<Model.Employee>>(itemsOnPage)));
    }    
}