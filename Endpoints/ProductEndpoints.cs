using Microsoft.EntityFrameworkCore;
using StockManagementSystem.Data;
using StockManagementSystem.Models;

namespace StockManagementSystem.Endpoints;

public static class ProductEndpoints
{
    public static void MapProductEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/products").WithTags("Products");

        // GET all products
        group.MapGet("/", async (StockDbContext db) =>
            await db.Products.ToListAsync())
            .WithName("GetAllProducts")
            .WithOpenApi();

        // GET product by id
        group.MapGet("/{id}", async (int id, StockDbContext db) =>
            await db.Products.FindAsync(id) is Product product
                ? Results.Ok(product)
                : Results.NotFound())
            .WithName("GetProductById")
            .WithOpenApi();

        // POST new product
        group.MapPost("/", async (Product product, StockDbContext db) =>
        {
            product.CreatedAt = DateTime.UtcNow;
            db.Products.Add(product);
            await db.SaveChangesAsync();
            return Results.Created($"/api/products/{product.Id}", product);
        })
        .WithName("CreateProduct")
        .WithOpenApi();

        // PUT update product
        group.MapPut("/{id}", async (int id, Product updatedProduct, StockDbContext db) =>
        {
            var product = await db.Products.FindAsync(id);
            if (product is null) return Results.NotFound();

            product.ProductName = updatedProduct.ProductName;
            product.Price = updatedProduct.Price;
            product.Quantity = updatedProduct.Quantity;
            product.UpdatedAt = DateTime.UtcNow;

            await db.SaveChangesAsync();
            return Results.Ok(product);
        })
        .WithName("UpdateProduct")
        .WithOpenApi();

        // DELETE product
        group.MapDelete("/{id}", async (int id, StockDbContext db) =>
        {
            var product = await db.Products.FindAsync(id);
            if (product is null) return Results.NotFound();

            db.Products.Remove(product);
            await db.SaveChangesAsync();
            return Results.NoContent();
        })
        .WithName("DeleteProduct")
        .WithOpenApi();
    }
}
