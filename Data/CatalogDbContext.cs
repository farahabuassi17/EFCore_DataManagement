using EFCore_DataManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCore_DataManagement.Data;

public class CatalogDbContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=Catalog.db");
    }
}