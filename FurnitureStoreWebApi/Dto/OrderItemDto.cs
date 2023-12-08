using System.ComponentModel.DataAnnotations;
using Xunit.Sdk;

namespace FurnitureStoreWebApi.Dto
{
    public class OrderItemDto
    {
        public int OrderItemId { get; set; }
        [Required(ErrorMessage="Order Id is required")]
        public int OrderId { get; set; }
        [Required(ErrorMessage = "Product Id is required")]
        public int ProductId { get; set; }
        [Required(ErrorMessage ="Quantity is required")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Quantity must be a non-negative integer")]
        [Range(0, 1000000, ErrorMessage = "Quantity must be between 0 and 1000000")]

        public int? Quantity { get; set; }
    }
}
