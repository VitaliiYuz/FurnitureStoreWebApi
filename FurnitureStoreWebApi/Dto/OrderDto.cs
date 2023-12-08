using System.ComponentModel.DataAnnotations;
using Xunit.Sdk;

namespace FurnitureStoreWebApi.Dto
{
    public class OrderDto
    {
        public int OrderId { get; set; }

        public int CustomerId { get; set; }
        [Required]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format")]
        public DateTime OrderDate { get; set; }
        [Required(ErrorMessage ="Status is required")]
        [StringLength(10, ErrorMessage = "Status must not exceed 10 characters")]
        public string Status { get; set; } = null!;
    }
}
