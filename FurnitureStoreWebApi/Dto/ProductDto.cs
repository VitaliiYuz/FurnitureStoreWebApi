using FurnitureStoreWebApi.Models;
using System.ComponentModel.DataAnnotations;
using Xunit.Sdk;

namespace FurnitureStoreWebApi.Dto
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(45, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 45 characters")]

        public string Name { get; set; } = null!;
        [StringLength(255, ErrorMessage = "Description must not exceed 255 characters")]
        public string? Description { get; set; }
        [Required(ErrorMessage ="Category Id is required")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]

        public double Price { get; set; }
        [Required(ErrorMessage = "Stock quantity is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock quantity must be a non-negative integer")]

        public int StockQuantity { get; set; }
        [Required(ErrorMessage ="Supplier Id is required")]
        public int SupplierId { get; set; }


    }
}
