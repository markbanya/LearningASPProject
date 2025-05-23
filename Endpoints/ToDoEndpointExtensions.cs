using LearningProjectASP.Models;
using LearningProjectASP.Services;
using Microsoft.AspNetCore.Builder;

namespace LearningProjectASP.Endpoints
{
    public static class ToDoEndpointExtensions
    {
        public static void MapToDoEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/todo");

            group.MapGet("/", async (IToDoService toDoService) =>
                await toDoService.GetAllAsync());

            group.MapPost("/", async (ToDoItem newItem, IToDoService toDoService) =>
            {
                var created = await toDoService.CreateAsync(newItem);
                return Results.Created($"/api/todo/{created.Id}", created);
            });

            group.MapPut("/{id}/complete", async (int id, IToDoService toDoService) =>
            {
                var updated = await toDoService.MarkCompleteAsync(id);
                return updated ? Results.NoContent() : Results.NotFound();
            });

            group.MapDelete("/{id}", async (int id, IToDoService toDoService) =>
            {
                var deleted = await toDoService.DeleteAsync(id);
                return deleted ? Results.NoContent() : Results.NotFound();
            });
        }
    }
}
