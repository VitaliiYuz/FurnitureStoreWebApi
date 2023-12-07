using FurnitureStoreWebApi.Interfaces;
using FurnitureStoreWebApi.Models;

namespace FurnitureStoreWebApi.Repositories
{
    public class ProductRepository: IProductRepository
    {
        private readonly FurnitureStoreContext _dataContext;

        public ProductRepository(FurnitureStoreContext dataContext)
        {
            _dataContext = dataContext;
        }
        public ICollection<Product> GetAllProducts() 
        {
            return _dataContext.Products.ToList();
        }
        public Product GetProductById(int id)
        {
            return _dataContext.Products.FirstOrDefault(p => p.ProductId == id);
        }

        public bool ProductExists(int id)
        {
            return _dataContext.Products.Any(p => p.ProductId == id);
        }

        public bool CreateProduct(Product product)
        {
            _dataContext.Add(product);
            return Save();
        }

        public bool UpdateProduct(Product product)
        {
            if (!_dataContext.Products.Any(t => t.ProductId == product.ProductId))
            {
                return false;
            }
            _dataContext.Update(product);
            return Save();
        }
        public bool DeleteProduct(Product product)
        {
            _dataContext.Remove(product);
            return Save();
        }
        public bool Save()
        {
            return _dataContext.SaveChanges() > 0;
        }
    }
}
