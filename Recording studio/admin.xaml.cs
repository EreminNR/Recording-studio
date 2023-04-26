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
using System.Data.SqlClient;
using System.Data;

namespace Recording_studio
{
    /// <summary>
    /// Логика взаимодействия для admin.xaml
    /// </summary>
    public partial class admin : Page
    {
        public admin()
        {
            InitializeComponent();
            string connectionString = @"Data Source=DESKTOP-OH72CL7\MSSQLSERVER01;Initial Catalog=Studio;Integrated Security=True";
            string sql = "SELECT * FROM Пользователь";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);

                DataSet ds = new DataSet();

                adapter.Fill(ds);

                MyData.ItemsSource = ds.Tables[0].DefaultView;

                
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = @"Data Source=DESKTOP-OH72CL7\MSSQLSERVER01;Initial Catalog=Studio;Integrated Security=True";
            string sql = "SELECT * FROM Пользователь";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);

                DataSet ds = new DataSet();

                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);

                adapter.Update(ds);

                ds.Clear();

                adapter.Fill(ds);


                MyData.ItemsSource = ds.Tables[0].DefaultView;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new admin2());
        }

        
    }
}
