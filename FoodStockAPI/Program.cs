using FoodStockAPI.Data;
using FoodStockAPI.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlite(builder.Configuration.GetConnectionString("SqliteConn")));

var app = builder.Build();

//app.UseHttpsRedirection();

app.MapGet("api/foodstock", async (AppDbContext ctx) =>
{
    var foods = await ctx.FoodStocks.ToListAsync();
    return Results.Ok(foods);
});

app.MapGet("api/foodstock/{id}", async (AppDbContext ctx, int id) =>
{
    var food = await ctx.FoodStocks.FirstOrDefaultAsync(f => f.Id == id);

    if (food == null)
        return Results.NotFound();

    return Results.Ok(food);
});

app.MapPost("api/foodstock", async (AppDbContext ctx, FoodStock food) =>
{
    await ctx.FoodStocks.AddAsync(food);
    await ctx.SaveChangesAsync();
    return Results.Created($"api/foodstock/{food.Id}", food);
});

app.MapPut("api/foodstock/{id}", async (AppDbContext ctx, int id, FoodStock food) =>
{
    var foodItem = await ctx.FoodStocks.FirstOrDefaultAsync(f => f.Id == id);

    if (foodItem == null)
        return Results.NotFound();

    foodItem.Name = food.Name;
    foodItem.Left = food.Left;
    await ctx.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("api/foodstock/{id}", async (AppDbContext ctx, int id) =>
{
    var foodItem = await ctx.FoodStocks.FirstOrDefaultAsync(f => f.Id == id);

    if (foodItem == null)
        return Results.NotFound();

    ctx.FoodStocks.Remove(foodItem);
    await ctx.SaveChangesAsync();

    return Results.NoContent();
});

app.Run();

