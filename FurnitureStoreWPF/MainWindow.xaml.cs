using FurnitureStoreWebApi.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FurnitureStoreWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DataAccess dataAccess;

        public MainWindow()
        {
            InitializeComponent();
            dataAccess = new DataAccess();
            //UpdateProductGridAsync();
        }

        private async void UpdateProductGridAsync()
        {
            var dataTable = await dataAccess.GetProductsAsync();
            if (dataTable != null)
            {
                productGrid.ItemsSource = dataTable.DefaultView;
            }
        }

        private void UpdateButtonClick(object sender, RoutedEventArgs e)
        {
            UpdateProductGridAsync();
        }
        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // Отримати вибраний продукт
            var selectedProduct = (sender as Button)?.DataContext as DataRowView;
            if (selectedProduct != null)
            {
                // Отримати Id продукту
                int productId = Convert.ToInt32(selectedProduct["ProductId"]);

                // Виклик методу для видалення продукту з бази даних
                await DeleteProductAsync(productId);

                // Оновити DataGrid
                UpdateProductGridAsync();
            }
        }

        private async Task DeleteProductAsync(int productId)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.DeleteAsync($"https://localhost:7089/api/Products/{productId}");

                if (!response.IsSuccessStatusCode)
                {
                    // Обробка помилок, наприклад, виведення повідомлення про помилку
                    System.Diagnostics.Debug.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                }
            }
        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            // Створення та відображення вікна додавання нового продукту
            AddProductWindow addProductWindow = new AddProductWindow();
            addProductWindow.ShowDialog();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            // Отримати вибраний рядок з DataGrid
            DataRowView selectedRow = (DataRowView)productGrid.SelectedItem;

            if (selectedRow != null)
            {
                // Отримати значення ProductId та SupplierId
                int productId = Convert.ToInt32(selectedRow["ProductId"]);
                int supplierId = Convert.ToInt32(selectedRow["SupplierId"]);

                // Відкрити вікно редагування товару
                EditProductWindow editProductWindow = new EditProductWindow(productId, supplierId);
                editProductWindow.ShowDialog();

                // Оновити DataGrid після редагування
                UpdateProductGridAsync();
            }
            else
            {
                MessageBox.Show("Будь ласка, виберіть продукт для редагування");
            }
        }


    }
}

