using LearningProjectASP.Data;
using LearningProjectASP.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/todo", async (AppDbContext db) =>
    await db.ToDoItems.ToListAsync());

app.MapPost("/api/todo", async (ToDoItem newItem, AppDbContext db) =>
{
    db.ToDoItems.Add(newItem);
    await db.SaveChangesAsync();
    return Results.Created($"/api/todo/{newItem.Id}", newItem);
});

app.MapPut("/api/todo/{id}/complete", async (int id, AppDbContext db) =>
{
    var item = await db.ToDoItems.FindAsync(id);
    if (item == null)
    {
        return Results.NotFound();
    }

    item.IsCompleted = true;
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/api/todo/{id}", async (int id, AppDbContext db) =>
{
    var item = await db.ToDoItems.FindAsync(id);
    if (item == null)
    {
        return Results.NotFound();
    }

    db.ToDoItems.Remove(item);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();
