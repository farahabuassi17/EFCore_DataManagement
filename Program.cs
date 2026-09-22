using EFCore_DataManagement.Data;
using EFCore_DataManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

using CatalogDbContext context = new();
// 1. CREATE - Seed Data

if (!context.Categories.Any())
{
    Category electronics = new()
    {
        Name = "Electronics",
        Products = new List<Product>
        {
            new Product { Title = "Laptop", Price = 900m },
            new Product { Title = "Mouse", Price = 20m },
            new Product { Title = "Keyboard", Price = 35m }
        }
    };

    Category books = new()
    {
        Name = "Books",
        Products = new List<Product>
        {
            new Product { Title = "C# Fundamentals", Price = 40m },
            new Product { Title = "Clean Code", Price = 45m }
        }
    };

    context.Categories.Add(electronics);
    context.Categories.Add(books);

    context.SaveChanges();

    Console.WriteLine("Data added successfully.");
}
else
{
    Console.WriteLine("Data already exists.");
}
// 2. READ - Include Products

Console.WriteLine("\n--- Categories and Products ---");

var categories = context.Categories
    .Include(c => c.Products)
    .ToList();

foreach (var category in categories)
{
    Console.WriteLine($"\nCategory: {category.Name}");

    foreach (var product in category.Products)
    {
        Console.WriteLine(
            $"  Product: {product.Title}, Price: {product.Price}");
    }
}
// 3. UPDATE

Console.WriteLine("\n--- Update Product ---");

var productToUpdate = context.Products
    .FirstOrDefault(p => p.Title == "Laptop");

if (productToUpdate != null)
{
    productToUpdate.Price = 1000m;

    context.SaveChanges();

    Console.WriteLine("Product updated successfully.");
    Console.WriteLine(
        $"New price of {productToUpdate.Title}: {productToUpdate.Price}");
}
else
{
    Console.WriteLine("Product not found.");
}
// 4. DELETE

Console.WriteLine("\n--- Delete Product ---");

var productToDelete = context.Products
    .FirstOrDefault(p => p.Title == "Mouse");

if (productToDelete != null)
{
    int deletedProductId = productToDelete.ProductId;

    context.Products.Remove(productToDelete);
    context.SaveChanges();

    bool productExists = context.Products
        .Any(p => p.ProductId == deletedProductId);

    if (!productExists)
    {
        Console.WriteLine("Product deleted successfully.");
    }
}
else
{
    Console.WriteLine("Product not found.");
}
// 5. JSON Serialization

Console.WriteLine("\n--- JSON Output ---");

var jsonData = context.Categories
    .Include(c => c.Products)
    .Select(c => new
    {
        c.CategoryId,
        c.Name,

        Products = c.Products.Select(p => new
        {
            p.ProductId,
            p.Title,
            p.Price
        })
    })
    .ToList();

string json = JsonSerializer.Serialize(
    jsonData,
    new JsonSerializerOptions
    {
        WriteIndented = true
    });

Console.WriteLine(json);