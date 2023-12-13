using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FurnitureStoreWebApi.Dto;
using FurnitureStoreWebApi.Models;
using Newtonsoft.Json;

namespace FurnitureStoreWPF
{
    public partial class AddProductWindow : Window
    {
        private const string ApiBaseUrl = "https://localhost:7089/api/Products";

        public AddProductWindow()
        {
            InitializeComponent();
        }

        private async void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Зчитування даних з полів форми
                string name = productNameTextBox.Text;
                string description = productDescriptionTextBox.Text;
                int categoryId = int.Parse(categoryIdTextBox.Text);
                double price = double.Parse(productPriceTextBox.Text);
                int stockQuantity = int.Parse(productStockQuantityTextBox.Text);
                int supplierId = int.Parse(supplierIdTextBox.Text);

                // Створення нового продукту
                ProductDto newProduct = new ProductDto
                {
                    Name = name,
                    Description = description,
                    CategoryId = categoryId,
                    Price = price,
                    StockQuantity = stockQuantity,
                    SupplierId = supplierId
                };

                // Відправлення запиту на додавання продукту
                await AddProductAsync(newProduct);

                // Закриття вікна додавання продукту
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task AddProductAsync(ProductDto newProduct)
        {
            using (HttpClient client = new HttpClient())
            {
                // Конвертація об'єкта в JSON
                string jsonProduct = JsonConvert.SerializeObject(newProduct);
                StringContent content = new StringContent(jsonProduct, Encoding.UTF8, "application/json");

                // Відправлення POST-запиту на сервер
                HttpResponseMessage response = await client.PostAsync(ApiBaseUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    // Обробка успішної відповіді (можна щось зробити, якщо необхідно)
                    MessageBox.Show("Продукт успішно додано!", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    // Обробка помилок
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Помилка сервера: {errorMessage}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
