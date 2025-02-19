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

namespace IurAir.UI.SetupActivity
{
    /// <summary>
    /// Logica di interazione per SetupActivity.xaml
    /// </summary>
    public partial class SetupActivity : Page
    {

        public SetupActivityViewModel VM = new SetupActivityViewModel();
        public SetupActivity()
        {
            InitializeComponent();
            DataContext = VM; 
        }

        public void Init(object Sender, RoutedEventArgs e)
        {
            VM.Init();
        }

        public void SaveAll(object Sender, RoutedEventArgs e)
        {
            VM.Save();
        }

       public void Translate(object Sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Translations());
        }

        private void OrionFormat_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
