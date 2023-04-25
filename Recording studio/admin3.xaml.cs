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
using System.Data;
using System.Data.SqlClient;

namespace Recording_studio
{
    /// <summary>
    /// Логика взаимодействия для admin3.xaml
    /// </summary>
    public partial class admin3 : Page
    {
        public admin3()
        {
            InitializeComponent();
            string connectionString = @"Data Source=DESKTOP-OH72CL7\MSSQLSERVER01;Initial Catalog=Studio;Integrated Security=True";
            string sql = "SELECT * FROM Запись";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);

                DataSet ds = new DataSet();

                adapter.Fill(ds);

                MyData.ItemsSource = ds.Tables[0].DefaultView;
            }
            using (StudioEntities db = new StudioEntities())
            {
                foreach (Запись id in db.Запись)
                {
                    cb1.Items.Add(id.IDЗаписи);
                    cb2.Items.Add(id.IDЗаписи);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new admin());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new admin2());
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            using (StudioEntities db = new StudioEntities())
            {
                int str = Convert.ToInt32(cb1.Text);
                Запись запись = (from t in db.Запись where t.IDЗаписи == str select t).FirstOrDefault();
                text4.Text = Convert.ToString(запись.Дата);
                text5.Text = запись.Телефон;
                text6.Text = запись.Время;
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            try
            {
                using (StudioEntities db = new StudioEntities())
                {
                    int str = Convert.ToInt32(cb2.Text);
                    Запись запись = (from t in db.Запись where t.IDЗаписи == str select t).FirstOrDefault();
                    db.Запись.Remove(запись);
                    db.SaveChanges();
                    cb2.Items.Clear();
                    cb1.Items.Clear();
                    foreach (Запись id in db.Запись)
                    {
                        cb1.Items.Add(id.IDЗаписи);
                        cb2.Items.Add(id.IDЗаписи);

                    }
                }

            }
            catch { MessageBox.Show("Укажите корректный id!"); }
        }
    }
}
