using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using FurnitureStoreWebApi.Models;
using Xunit;
using Xunit.Sdk;

namespace FurnitureStoreWebApi.Dto
{
    public class CustomerDto
    {
        public int CustomerId { get; set; }
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Username is required")]
        [System.ComponentModel.DataAnnotations.MinLength(2, ErrorMessage = "Username must be at least 2 characters!")]
        public string UserName { get; set; } = null!;

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string Password { get; set; } = null!;

    }
}
