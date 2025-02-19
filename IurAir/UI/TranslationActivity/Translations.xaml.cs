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

namespace IurAir.UI.TranslationActivity
{
    /// <summary>
    /// Logica di interazione per Translations.xaml
    /// </summary>
    public partial class Translations : Page
    {
        public TranslationsViewModel VM = new TranslationsViewModel();
        public Translations()
        {
            InitializeComponent();
            DataContext = VM;
        }

        public void Setup(object Sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SetupActivity.SetupActivity());
        }
    }
}
