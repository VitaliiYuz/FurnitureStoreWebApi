using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows;

namespace FurnitureStoreWPF
{
    public partial class EditProductWindow : Window
    {
        private readonly int productId;
        private readonly int supplierId;

        public EditProductWindow(int productId, int supplierId)
        {
            InitializeComponent();
            this.productId = productId;
            this.supplierId = supplierId;
        }

        private async void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Зчитати дані з полів форми
                string name = productNameTextBox.Text;
                string description = productDescriptionTextBox.Text;
                int categoryId = int.Parse(categoryIdTextBox.Text);
                double price = double.Parse(productPriceTextBox.Text);
                int stockQuantity = int.Parse(productStockQuantityTextBox.Text);

                // Створити об'єкт для відправки на сервер
                var updatedProduct = new
                {
                    ProductId = productId,
                    SupplierId = supplierId,
                    Name = name,
                    Description = description,
                    CategoryId = categoryId,
                    Price = price,
                    StockQuantity = stockQuantity
                };

                // Викликати PUT запит до сервера
                using (HttpClient client = new HttpClient())
                {
                    // Оновити URL для вашого ендпоінту
                    string apiUrl = $"https://localhost:7089/api/Products/{productId}";

                    HttpResponseMessage response = await client.PutAsJsonAsync(apiUrl, updatedProduct);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Зміни успішно збережено");
                        Close();
                    }
                    else
                    {
                        MessageBox.Show($"Помилка: {response.StatusCode} - {response.ReasonPhrase}");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}");
            }
        }
    }
}
