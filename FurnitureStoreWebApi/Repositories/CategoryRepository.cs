using FurnitureStoreWebApi.Interfaces;
using FurnitureStoreWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FurnitureStoreWebApi.Repositories
{
    public class CategoryRepository:ICategoryRepository

    {
        private readonly FurnitureStoreContext _dataContext;
        public CategoryRepository(FurnitureStoreContext dataContext)
        {
            _dataContext = dataContext;
        }
        public ICollection<Category> GetAllCategories() 
        {
            return _dataContext.Categories.Include(n=>n.Name).ToList();
        }
        public Category GetCategoryById(int id)
        {
            return _dataContext.Categories.FirstOrDefault(c => c.CategoryId == id);
        }

        public bool CategoryExists(int id)
        {
            return _dataContext.Categories.Any(c => c.CategoryId == id);
        }

        public bool CreateCategory(Category category)
        {
            _dataContext.Add(category);
            return Save();
        }

        public bool UpdateCategory(Category category)
        {
            if (!_dataContext.Categories.Any(t => t.CategoryId == category.CategoryId))
            {
                return false;
            }
            _dataContext.Update(category);
            return Save();
        }
        public bool DeleteCategory(Category category)
        {
            _dataContext.Remove(category);
            return Save();
        }
        public bool Save()
        {
            return _dataContext.SaveChanges() > 0;
        }
    }
}
