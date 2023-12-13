using FurnitureStoreWebApi.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Diagnostics;
using System.Windows;

namespace FurnitureStoreWPF
{
    public class DataAccess
    {
        private string apiBaseUrl = "https://localhost:7089/api/Products"; // Замінити на свою URL-адресу

        public async Task<DataTable> GetProductsAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiBaseUrl);

                if (response.IsSuccessStatusCode)
                {
                    List<ProductDto> productList = await response.Content.ReadFromJsonAsync<List<ProductDto>>();

                    return await ConvertToDataTableAsync(productList);
                }
                else
                {
                    // Обробка помилок, наприклад, виведення повідомлення про помилку
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    return null;
                }
            }
        }
        private async Task<DataTable> ConvertToDataTableAsync(List<ProductDto> productList)
        {
            DataTable table = new DataTable();

            // Додати перейменовані колонки
            table.Columns.Add("ProductId", typeof(int)).ColumnName = "ProductId"; 
            table.Columns.Add("Назва", typeof(string)).ColumnName = "Name";
            table.Columns.Add("Опис", typeof(string)).ColumnName = "Description";
            table.Columns.Add("Категорія", typeof(string)).ColumnName = "CategoryName"; // залишаємо колонку для отримання Id категорії
            table.Columns.Add("Ціна", typeof(double)).ColumnName = "Price";
            table.Columns.Add("Кількість", typeof(int)).ColumnName = "StockQuantity";
            table.Columns.Add("SupplierId", typeof(int)).ColumnName = "SupplierId";

            // Додати рядки
            foreach (var product in productList)
            {
                // Отримати назву категорії за Id
                string categoryName = await GetCategoryNameByIdAsync(product.CategoryId);

                table.Rows.Add(
                     product.ProductId,
                    product.Name,
                    product.Description,
                    categoryName, // використовуємо назву категорії замість Id
                    product.Price,
                    product.StockQuantity,
                    product.SupplierId
                ); ;
                Debug.WriteLine(categoryName+ " " + product.Name );
            }
            
            return await Task.FromResult(table); 
        }



        private async Task<string> GetCategoryNameByIdAsync(int categoryId)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"https://localhost:7089/api/Categories/{categoryId}");

                if (response.IsSuccessStatusCode)
                {
                    CategoryDto category = await response.Content.ReadFromJsonAsync<CategoryDto>();
                    return category?.Name ?? "Unknown Category";
                }
                else
                {
                    // Обробка помилок, наприклад, виведення повідомлення про помилку
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    return "Unknown Category";
                }
            }
        }


    }
}
