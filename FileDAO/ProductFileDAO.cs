using Entities;
using Interface;

namespace FileDAO;

public class ProductFileDao:IProductDAO
{
    private ProductContext _productContext;

    public ProductFileDao(ProductContext productContext)
    {
        _productContext = productContext;
    }

    public async Task AddProduct(Product p)
    {
      _productContext.Products.Add(p);
      await _productContext.SaveChanges();
    }

    public async  Task<ICollection<Product>> GetAllProducts()
    {
        return _productContext.Products;
    }
}