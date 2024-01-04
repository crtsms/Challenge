using Challenge.API.Model;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Challenge.API;
public static class Skill
{
    public static IEndpointRouteBuilder MapSkillApi(this IEndpointRouteBuilder app)
    {
        // Routes for querying catalog items.
        app.MapGet("/skills", GetAllSkills);
        return app;
    }

    public static async Task<Results<Ok<PaginatedItems<Model.Skill>>, BadRequest<string>>> GetAllSkills(
        [AsParameters] PaginationRequest paginationRequest,
        [AsParameters] EmployeeServices services)
    {
        var pageSize = paginationRequest.PageSize;
        var pageIndex = paginationRequest.PageIndex;

        var totalItems = await services.Context.Skills
            .LongCountAsync();

        var itemsOnPage = await services.Context.Skills
            .OrderBy(c => c.Name)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .Select(s => new Model.Skill(s.Name, s.Description))
            .ToListAsync();

        return TypedResults.Ok(new PaginatedItems<Model.Skill>(pageIndex, pageSize, totalItems, itemsOnPage));
    }    
}