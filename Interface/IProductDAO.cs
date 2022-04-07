using System.Net.Http.Headers;
using Entities;

namespace Interface;

public interface IProductDAO
{
    public Task AddProduct(Product p);
    public Task<ICollection<Product>> GetAllProducts();
}