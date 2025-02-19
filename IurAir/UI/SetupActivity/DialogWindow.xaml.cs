using IurAir.Domain.Common;
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
using System.Windows.Shapes;

namespace IurAir.UI.SetupActivity
{
    /// <summary>
    /// Logica di interazione per DialogWindow.xaml
    /// </summary>
    public partial class DialogWindow : Window
    {
        public string Password { get; private set; }
        public string AgencyIata { get; private set; }
        public Boolean DebugMode { get; private set; }
        public Boolean PersistentDebug { get; private set; }

        public DialogWindow()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            Password = passwordBox.Password;
            AgencyIata = textBox1.Text;
            DebugMode = debugModeCheckBox.IsChecked == true;
            PersistentDebug = persistentDebugCheckBox.IsChecked == true;    
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
