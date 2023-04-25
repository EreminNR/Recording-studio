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

namespace Recording_studio
{
    /// <summary>
    /// Логика взаимодействия для Personal.xaml
    /// </summary>
    public partial class Personal : Page
    {
        public Personal()
        {
            InitializeComponent();
            SearchTermTextBox2.IsReadOnly = true;
            SearchTermTextBox2.Text = Authorization.log;
            SearchTermTextBox.Text = Authorization.name;
            SearchTermTextBox1.Text = Authorization.lastname;
            but1.Visibility = Visibility.Hidden;


        }

        private void TextBlock_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Main());
        }

        private void TextBlock_PreviewMouseDown_1(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Services());
        }

        private void SearchTermTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void SearchTermTextBox1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void SearchTermTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            but1.Visibility = Visibility.Visible;
        }

        private void SearchTermTextBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            but1.Visibility = Visibility.Visible;
        }

        private void but1_Click(object sender, RoutedEventArgs e)
        {
            using (StudioEntities db = new StudioEntities())
            {
                if (SearchTermTextBox.Text != String.Empty && SearchTermTextBox1.Text != String.Empty)
                {
                    if (SearchTermTextBox.Text.Length >= 3 && SearchTermTextBox1.Text.Length >= 3)
                    {
                        string l = Authorization.log;
                        string p = Authorization.pas;
                        Пользователь user1 = (from t in db.Пользователь where t.Логин == l && t.Пароль == p select t).FirstOrDefault();
                        user1.Имя = SearchTermTextBox.Text;
                        user1.Фамилия = SearchTermTextBox1.Text;                        
                        db.SaveChanges();
                        MessageBox.Show("Изменения сохранены, данные изменятся после повторного входа в приложение");
                    }
                    else MessageBox.Show("Введите данные длиннее");
                }

                else
                {
                    MessageBox.Show("Заполните все поля!");
                }
            }
        }
    }
}
