using LearningProjectASP.Models;
using LearningProjectASP.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace LearningProjectASP.Endpoints
{
    public static class UserEndpoints
    {
        public static void MapUserEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/users");

            group.MapGet("/", async (IUserService userService) =>
                await userService.GetAllAsync());

            group.MapGet("/{id}", async (int id, IUserService userService) =>
            {
                var user = await userService.GetByIdAsync(id);
                return user is not null ? Results.Ok(user) : Results.NotFound();
            });

            group.MapPost("/", async (User user, IUserService userService) =>
            {
                var created = await userService.CreateAsync(user);
                return Results.Created($"/api/users/{created.Id}", created);
            });

            group.MapPut("/{id}", async (int id, User updatedUser, IUserService userService) =>
            {
                var updated = await userService.UpdateAsync(id, updatedUser);
                return updated ? Results.NoContent() : Results.NotFound();
            });

            group.MapDelete("/{id}", async (int id, IUserService userService) =>
            {
                var deleted = await userService.DeleteAsync(id);
                return deleted ? Results.NoContent() : Results.NotFound();
            });

            group.MapGet("/me", [Authorize] (ClaimsPrincipal user) =>
            {
                var name = user.Identity?.Name;
                var role = user.FindFirst(ClaimTypes.Role)?.Value;
                return Results.Ok(new { name, role });
            });
        }
    }
}