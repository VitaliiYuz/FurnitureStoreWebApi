using FurnitureStoreWebApi.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private FurnitureStoreContext _dbContext;
        public MainWindow()
        {
            InitializeComponent();
            _dbContext = new FurnitureStoreContext();
            LoadData();
        }
        private void LoadData()
        {
            // Отримання всіх даних з таблиці "orders"
            var orders = _dbContext.Orders.ToList();

            // Призначення даних контексту даних для відображення в DataGrid
            dataGrid.ItemsSource = orders;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
