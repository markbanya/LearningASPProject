using LearningProjectASP.Data;
using LearningProjectASP.Models;
using Microsoft.EntityFrameworkCore;

namespace LearningProjectASP.Endpoints.ToDo
{
    public static class ToDoEndpointExtensions
    {
        public static RouteGroupBuilder MapToDoEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/todo");

            group.MapGet("/", async (AppDbContext db) =>
                await db.ToDoItems.ToListAsync());

            group.MapPost("/", async (ToDoItem newItem, AppDbContext db) =>
            {
                db.ToDoItems.Add(newItem);
                await db.SaveChangesAsync();
                return Results.Created($"/api/todo/{newItem.Id}", newItem);
            });

            group.MapPut("/{id}/complete", async (int id, AppDbContext db) =>
            {
                var item = await db.ToDoItems.FindAsync(id);
                if (item is null)
                {
                    return Results.NotFound();
                }

                item.IsCompleted = true;
                await db.SaveChangesAsync();
                return Results.NoContent();
            });

            group.MapDelete("/{id}", async (int id, AppDbContext db) =>
            {
                var item = await db.ToDoItems.FindAsync(id);
                if (item is null)
                {
                    return Results.NotFound();
                }

                db.ToDoItems.Remove(item);
                await db.SaveChangesAsync();
                return Results.NoContent();
            });

            return group;
        }
    }
}
