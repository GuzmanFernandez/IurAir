using IurAir.UI.TranslationActivity;
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

namespace IurAir.UI.HomePage
{
    /// <summary>
    /// Logica di interazione per HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
        }

        public void Setup(object Sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SetupActivity.SetupActivity());
        }

        public void Translate(object Sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Translations());
        }
    }
}
