namespace EFCore_DataManagement.Models;

public class Product
{
    public int ProductId { get; set; }

    public string Title { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public int CategoryId { get; set; }

    public Category Category { get; set; } = null!;
}