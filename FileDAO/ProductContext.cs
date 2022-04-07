using System.Text.Json;
using Entities;

namespace FileDAO;

public class ProductContext
{
    private string userFilePath = "products.json";

    private ICollection<Product> products;

    public   ICollection<Product> Products
    {
         get
        {
            if (products == null)
            {
                LoadData();
            }

            return products;
        }
        set
        {
            products = value;
        }
    }
    public ProductContext()
    {
        if (!File.Exists(userFilePath))
        {
            Seed();
        }
    }
    private void Seed()
    {
        Product[] ts = {
            new Product {
                ProductId = "1",
                ProductName = "FirstProd"
            },
            new Product {
                ProductId = "2",
                ProductName = "SecondProd"
            },
            
        };
        products = ts.ToList();
        Task.FromResult(SaveChanges());
    }
    public async  Task SaveChanges()
    {
        string serialize = JsonSerializer.Serialize(Products);
        await File.WriteAllTextAsync(userFilePath,serialize);
        products = null;
    }
    private void LoadData()
    {
        string content = File.ReadAllText(userFilePath);
        Products = JsonSerializer.Deserialize<List<Product>>(content);
    }
}