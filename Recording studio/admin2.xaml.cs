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
using System.Text.RegularExpressions;
using Microsoft.Win32;
using System.IO;

namespace Recording_studio
{
    /// <summary>
    /// Логика взаимодействия для admin2.xaml
    /// </summary>
    public partial class admin2 : Page
    {
        private StudioEntities _context = new StudioEntities();

        private byte[] image;
        public admin2()
        {
            InitializeComponent();
            string connectionString = @"Data Source=DESKTOP-OH72CL7\MSSQLSERVER01;Initial Catalog=Studio;Integrated Security=True";
            string sql = "SELECT * FROM Услуги";
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
                foreach (Услуги id in db.Услуги)
                {
                    cb1.Items.Add(id.IDУслуги);
                    cb2.Items.Add(id.IDУслуги);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new admin());
        }
        

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            using (StudioEntities db = new StudioEntities())
            {
                if (text.Text != String.Empty && text1.Text != String.Empty && text3.Text != String.Empty)
                {
                    if (text.Text.Length >= 5 && text1.Text.Length >= 5)
                    {
                        Услуги услуги = new Услуги { Название = text1.Text, Описание = text1.Text, Цена = Convert.ToInt32(text3.Text), Изображение = image};
                        db.Услуги.Add(услуги);
                        db.SaveChanges();
                        MessageBox.Show("Данные внесены");
                        
                    }
                    else MessageBox.Show("Введите больше данных");
                }

                else
                {
                    MessageBox.Show("Заполните все поля!");
                }
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (text4.Text == String.Empty || text5.Text == String.Empty || text6.Text == String.Empty)
            {
                MessageBox.Show("Заполните все поля!");
            }
            else
            {
                try
                {
                    using (StudioEntities db = new StudioEntities())
                    {
                        int str = Convert.ToInt32(cb1.Text);
                        Услуги услуги = (from t in db.Услуги where t.IDУслуги == str select t).FirstOrDefault();
                        услуги.Название = text4.Text;
                        услуги.Описание = text5.Text;
                        услуги.Цена = Convert.ToDecimal(text6.Text);
                        услуги.Изображение = image;
                        db.SaveChanges();
                        MessageBox.Show("Изменения внесены успешно!");
                    }
                }
                catch
                {
                    MessageBox.Show("Возникла ошибка при заполнении!");
                }
            }

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            using (StudioEntities db = new StudioEntities())
            {
                int str = Convert.ToInt32(cb1.Text);
                Услуги услуги = (from t in db.Услуги where t.IDУслуги == str select t).FirstOrDefault();
                text4.Text = услуги.Название;
                text5.Text = услуги.Описание;
                text6.Text = Convert.ToString(услуги.Цена);               
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            try
            {
                using (StudioEntities db = new StudioEntities())
                {
                    int str = Convert.ToInt32(cb2.Text);
                    Услуги услуги = (from t in db.Услуги where t.IDУслуги == str select t).FirstOrDefault();
                    db.Услуги.Remove(услуги);
                    db.SaveChanges();
                    cb2.Items.Clear();
                    cb1.Items.Clear();
                    foreach (Услуги id in db.Услуги)
                    {
                        cb1.Items.Add(id.IDУслуги);
                        cb2.Items.Add(id.IDУслуги);

                    }
                }

            }
            catch { MessageBox.Show("Укажите корректный id!"); }
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string path;
            if ((bool)openFileDialog.ShowDialog())
            {
                path = openFileDialog.FileName;
                this.image = System.IO.File.ReadAllBytes(path);
                MemoryStream ms = new MemoryStream(image);
                ImgBook.Source = BitmapFrame.Create(ms, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
            }
        }

        public void AddServices(string Name, string description, byte[] image, int Price)
        {
            Услуги услуги = new Услуги() { Название = Name, Описание = description, Цена = Price, Изображение = image};
            _context.Услуги.Add(услуги);
            _context.SaveChanges();
        }

        public static void EditServices(int id, string Name, string Discription, byte[] image, int Price)
        {
            StudioEntities _context = new StudioEntities();
            Услуги услуги = _context.Услуги.FirstOrDefault(x => x.IDУслуги == id);
            услуги.Название = Name;
            услуги.Описание = Discription;
            услуги.Цена = Convert.ToDecimal(Price);
            услуги.Изображение = image;

            _context.SaveChanges();
        }

        public static void DeleteServices(int id)
        {
            StudioEntities _context = new StudioEntities();
            Услуги услуги = _context.Услуги.FirstOrDefault(x => x.IDУслуги == id);
            _context.Услуги.Remove(услуги);
            _context.SaveChanges();
        }
    }
}


