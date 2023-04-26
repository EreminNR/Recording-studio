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
    /// Логика взаимодействия для Services.xaml
    /// </summary>
    public partial class Services : Page
    {
        public StudioEntities _context = new StudioEntities();
        public ListView list;
        public Services()
        {
            InitializeComponent();
            var services = StudioEntities.GetContext().Услуги.ToList();
            ServicesList.ItemsSource = services;
            ServicesList.ItemsSource = _context.Услуги.ToList();
            list = ServicesList;
        }

        private void TextBlock_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Main());
        }

        
    }
}
