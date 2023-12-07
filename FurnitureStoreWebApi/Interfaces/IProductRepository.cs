using FurnitureStoreWebApi.Models;

namespace FurnitureStoreWebApi.Interfaces
{
    public interface IProductRepository
    {
        ICollection<Product> GetAllProducts();
        Product GetProductById(int id);
        bool ProductExists(int id);
        bool CreateProduct(Product product);
        bool UpdateProduct(Product product);
        bool DeleteProduct(Product product);
        bool Save();
    }
}
