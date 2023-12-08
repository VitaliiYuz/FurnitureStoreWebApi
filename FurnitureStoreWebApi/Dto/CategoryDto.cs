using System.ComponentModel.DataAnnotations;
using Xunit.Sdk;

namespace FurnitureStoreWebApi.Dto
{
    public class CategoryDto
    {
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(45, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 45 characters")]

        public string Name { get; set; } = null!;
    }
}
