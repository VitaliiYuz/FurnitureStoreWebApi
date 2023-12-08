using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using FurnitureStoreWebApi.Models;
using Xunit;
using Xunit.Sdk;
using Xunit.Abstractions;

namespace FurnitureStoreWebApi.Dto
{
    public class CustomerDto
    {
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage="Id is required")]
        public int CustomerId { get; set; }
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Username is required")]
        [System.ComponentModel.DataAnnotations.MinLength(2, ErrorMessage = "Username must be at least 2 characters!")]
        public string UserName { get; set; } = null!;
        [RegularExpression(@"^[a-zA-Z0-9._-]{1,20}@[a-zA-Z0-9.-]{1,20}\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email format")]
        public string? Email { get; set; }

        [RegularExpression(@"^\d{10,12}$", ErrorMessage = "Invalid phone number format (10-12 digits)")]
        public string? Phone { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        public string Password { get; set; } = null!;

    }
}
