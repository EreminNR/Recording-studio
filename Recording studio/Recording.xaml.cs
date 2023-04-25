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
    /// Логика взаимодействия для Recording.xaml
    /// </summary>
    public partial class Recording : Page
    {
        public Recording()
        {
            InitializeComponent();
            SearchTermTextBox.MaxLength = 11;
            using (StudioEntities db = new StudioEntities())
            {
                foreach (Услуги id in db.Услуги)
                {
                    cb1.Items.Add(id.Название);
                    
                }
            }


        }

        private void TextBlock_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Main());
        }

        private void TextBlock_PreviewMouseDown_1(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Services());
        }



        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Personal());
        }

        private void SearchTermTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }

            
        }
     


        private void SearchTermTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void DatePicker_CalendarOpened(object sender, RoutedEventArgs e)
        {
            ViborData.DisplayDateStart = DateTime.Now;
            ViborData.DisplayDateEnd = DateTime.Now + TimeSpan.FromDays(14);

            var minDate = ViborData.DisplayDateStart ?? DateTime.MinValue;
            var maxDate = ViborData.DisplayDateEnd ?? DateTime.MaxValue;

            for (var d = minDate; d <= maxDate && DateTime.MaxValue > d; d = d.AddDays(1))
            {
                if (d.DayOfWeek == DayOfWeek.Saturday || d.DayOfWeek == DayOfWeek.Sunday)
                {
                    ViborData.BlackoutDates.Add(new CalendarDateRange(d));
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (StudioEntities db = new StudioEntities())
                {
                    
                    string str = (cb1.Text);
                    Услуги услуги = (from t in db.Услуги where t.Название == str select t).FirstOrDefault();
                    Запись запись = new Запись { IDПользователя = Authorization.idperson, IDУслуги = услуги.IDУслуги, Телефон = SearchTermTextBox.Text, Дата = (ViborData.SelectedDate), Время = "14:00" };
                    db.Запись.Add(запись);
                    db.SaveChanges();
                    MessageBox.Show("Запись создана");
                    
                    bt1.Visibility = Visibility.Hidden;

                }
            }

            catch
            {

            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                using (StudioEntities db = new StudioEntities())
                {

                    string str = (cb1.Text);
                    Услуги услуги = (from t in db.Услуги where t.Название == str select t).FirstOrDefault();
                    Запись запись = new Запись { IDПользователя = Authorization.idperson, IDУслуги = услуги.IDУслуги, Телефон = SearchTermTextBox.Text, Дата = (ViborData.SelectedDate), Время = "19:00" };
                    db.Запись.Add(запись);
                    db.SaveChanges();
                    MessageBox.Show("Запись создана");                   
                    bt2.Visibility = Visibility.Hidden;


                }
            }

            catch
            {

            }
        }
    }
}
