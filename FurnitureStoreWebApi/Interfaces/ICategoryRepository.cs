using FurnitureStoreWebApi.Models;

namespace FurnitureStoreWebApi.Interfaces
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetAllCategories();
        Category GetCategoryById(int id);
        bool CategoryExists(int id);
        bool CreateCategory(Category category);
        bool UpdateCategory(Category category);
        bool DeleteCategory(Category category);
        bool Save();
    }
}
