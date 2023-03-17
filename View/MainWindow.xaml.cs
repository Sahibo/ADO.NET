using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace MarketStock.View
{
    public partial class MainWindow : Window
    {
        public static List<string> SortByList = new() { "All info about products", "All product types", "All suppliers", "Quantity Max to Min", "Quantity Min to Max", "Price Max to Min", "Price Min to Max", "Oldest product", "Avg quantity by type of product"};
        public MainWindow()
        {
            InitializeComponent();

            SortCb.ItemsSource = SortByList;
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SortCb_Selected(object sender, SelectionChangedEventArgs e)
        {
            ConfigurationBuilder builder = new();
            builder.AddJsonFile("appsettings.json");
            var res = builder.Build();
            SqlConnection conn = new(res.GetConnectionString("MarketStock"));

            using (conn)
            {
                conn.Open();

                int choice = SortCb.SelectedIndex;

                switch (choice)
                {
                    case 0:
                    {
                        ShowAll(conn);
                        break;
                    }
                    case 1:
                    {
                        ShowAllTypes(conn);
                        break;
                    }
                    case 2:
                    {
                        ShowAllSuppliers(conn);
                        break;
                    }
                    case 3:
                    {
                        ShowMaxMinQuantity(conn);
                        break;
                    }
                    case 4:
                    {
                        ShowMinMaxQuantity(conn);
                        break;
                    }
                    case 5:
                    {
                        ShowMaxMinPrice(conn);
                        break;
                    }
                    case 6:
                    {
                        ShowMinMaxPrice(conn);
                        break;
                    }
                    case 7:
                    {
                        ShowOldest(conn);
                        break;
                    }
                    case 8:
                    {
                        ShowAvgByType(conn);
                        break;
                    }
                }

                if (conn.State == System.Data.ConnectionState.Open)
                    MessageBox.Show("Success connection!");
                else
                    MessageBox.Show("Error connection!");

            }
        }
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if (SortTextBox.Text != string.Empty)
            {
                ConfigurationBuilder builder = new();
                builder.AddJsonFile("appsettings.json");
                var res = builder.Build();
                SqlConnection conn = new(res.GetConnectionString("MarketStock"));

                using (conn)
                {
                    conn.Open();
                    list.Items.Clear();
                    using SqlCommand command = new($"SELECT * FROM Products " +
                                                   $"JOIN ProductTypes ON Products.ProductTypeID = ProductTypes.ProductTypeID " +
                                                   $"JOIN Suppliers ON Products.SupplierID = Suppliers.SupplierID " +
                                                   $"WHERE ProductTypes.ProductTypeName LIKE N'{SortTextBox.Text}' OR Suppliers.SupplierName LIKE N'{SortTextBox.Text}'" , conn);

                    SqlDataReader reader = command.ExecuteReader();


                    list.Items.Add($"{reader.GetName(8)}  |  {reader.GetName(6)}  |  {reader.GetName(1)}  |  {reader.GetName(4)}");
                    while (reader.Read())
                    {
                        list.Items.Add($"{reader[8]}  |  {reader[6]}  |  {reader[1]}  |  {reader[4]}");
                    }
                    if (conn.State == System.Data.ConnectionState.Open)
                        MessageBox.Show("Success connection!");
                    else
                        MessageBox.Show("Error connection!");

                }
            }
        }

        public void ShowAll(SqlConnection conn)
        {
            list.Items.Clear();
            using SqlCommand command = new("SELECT * FROM Products", conn);

            SqlDataReader reader = command.ExecuteReader();

            list.Items.Add($"{reader.GetName(0)}  |  {reader.GetName(1)}  |  {reader.GetName(2)}  |  {reader.GetName(3)}");
            while (reader.Read())
            {
                list.Items.Add($"{reader[0]}  |  {reader[1]}  |  {reader[2]}  |  {reader[3]}");
            }

        }

        public void ShowAllTypes(SqlConnection conn)
        {
            list.Items.Clear();
            using SqlCommand command = new("SELECT [ProductTypeName] FROM ProductTypes", conn);

            SqlDataReader reader = command.ExecuteReader();

            list.Items.Add($"{reader.GetName(0)}");
            while (reader.Read())
            {
                list.Items.Add($"{reader[0]}");
            }

        }

        public void ShowAllSuppliers(SqlConnection conn)
        {
            list.Items.Clear();
            using SqlCommand command = new("SELECT [SupplierName] FROM Suppliers", conn);

            SqlDataReader reader = command.ExecuteReader();

            list.Items.Add($"{reader.GetName(0)}");
            while (reader.Read())
            {
                list.Items.Add($"{reader[0]}");
            }
        }

        public void ShowMaxMinQuantity(SqlConnection conn)
        {
            list.Items.Clear();
            using SqlCommand command = new("SELECT * FROM Deliveries JOIN Products ON Products.ProductTypeID = Deliveries.DeliveryID ORDER BY Quantity DESC", conn);

            SqlDataReader reader = command.ExecuteReader();

            list.Items.Add($"{reader.GetName(5)}  |  {reader.GetName(2)}  |  {reader.GetName(3)}  |  {reader.GetName(8)}");
            while (reader.Read())
            {
                list.Items.Add($"{reader[5]}  |  {reader[2]}  |  {reader[3]}  |  {reader[8]}");
            }
        }

        public void ShowMinMaxQuantity(SqlConnection conn)
        {
            list.Items.Clear();
            using SqlCommand command = new("SELECT * FROM Deliveries JOIN Products ON Products.ProductTypeID = Deliveries.DeliveryID ORDER BY Quantity ASC", conn);

            SqlDataReader reader = command.ExecuteReader();

            list.Items.Add($"{reader.GetName(5)}  |  {reader.GetName(2)}  |  {reader.GetName(3)}  |  {reader.GetName(8)}");
            while (reader.Read())
            {
                list.Items.Add($"{reader[5]}  |  {reader[2]}  |  {reader[3]}  |  {reader[8]}");
            }
        }

        public void ShowMaxMinPrice(SqlConnection conn)
        {
            list.Items.Clear();
            using SqlCommand command = new("SELECT * FROM Deliveries JOIN Products ON Products.ProductTypeID = Deliveries.DeliveryID ORDER BY Cost DESC", conn);

            SqlDataReader reader = command.ExecuteReader();

            list.Items.Add($"{reader.GetName(5)}  |  {reader.GetName(2)}  |  {reader.GetName(3)}  |  {reader.GetName(8)}");
            while (reader.Read())
            {
                list.Items.Add($"{reader[5]}  |  {reader[2]}  |  {reader[3]}  |  {reader[8]}");
            }
        }

        public void ShowMinMaxPrice(SqlConnection conn)
        {
            list.Items.Clear();
            using SqlCommand command = new("SELECT * FROM Deliveries JOIN Products ON Products.ProductTypeID = Deliveries.DeliveryID ORDER BY Cost ASC", conn);

            SqlDataReader reader = command.ExecuteReader();

            list.Items.Add($"{reader.GetName(5)}  |  {reader.GetName(2)}  |  {reader.GetName(3)}  |  {reader.GetName(8)}");
            while (reader.Read())
            {
                list.Items.Add($"{reader[5]}  |  {reader[2]}  |  {reader[3]}  |  {reader[8]}");
            }
        }

        public void ShowOldest(SqlConnection conn)
        {
            list.Items.Clear();
            using SqlCommand command = new("SELECT * FROM Deliveries JOIN Products ON Products.ProductTypeID = Deliveries.DeliveryID ORDER BY DeliveryDate ASC", conn);

            SqlDataReader reader = command.ExecuteReader();

            list.Items.Add($"{reader.GetName(5)}  |  {reader.GetName(2)}  |  {reader.GetName(3)}  |  {reader.GetName(8)}");
            while (reader.Read())
            {
                list.Items.Add($"{reader[5]}  |  {reader[2]}  |  {reader[3]}  |  {reader[8]}");
            }
        }
        public void ShowAvgByType(SqlConnection conn)
        {
            list.Items.Clear();
            using SqlCommand command = new("SELECT ProductTypeName, AVG(Quantity) AS AvgQuantity FROM ProductTypes " +
                                           "JOIN Products ON Products.ProductTypeID = ProductTypes.ProductTypeID " +
                                           "JOIN Deliveries ON Deliveries.DeliveryID = ProductTypes.ProductTypeID " +
                                           "GROUP BY ProductTypeName", conn);

            SqlDataReader reader = command.ExecuteReader();

            list.Items.Add($"{reader.GetName(0)}  |  {reader.GetName(1)}");
            while (reader.Read())
            {
                list.Items.Add($"{reader[0]}  |  {reader[1]}");
            }
        }
    }

}
