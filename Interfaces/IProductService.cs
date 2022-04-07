using Entities;

namespace Interfaces;

public interface IProductService 
{
    public Task<Product> GetSentProduct();
}