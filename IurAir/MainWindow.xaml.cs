using IurAir.Domain.Translations;
using IurAir.UI.HomePage;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Forms;

namespace IurAir
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private NotifyIcon notifyIcon;
        private MainWindow mainWindow;
        private AutoWatcher watcher;

        public AutoWatcher GetWatcher() { return watcher; }

        public MainWindow()
        {
            InitializeComponent();
            mainWindow = this;
            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = Properties.Resources.plane;
            notifyIcon.MouseDoubleClick += new MouseEventHandler(nIcoDC);
            this.StateChanged += OnStateChanged;
            this.ShowActivated = true;
            this.watcher = new AutoWatcher();
            this.watcher.OnFileFound();
            this.watcher.watch();
            System.Windows.Application.Current.Exit += OnApplicationExit;
        }

        private void nIcoDC(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                if (mainWindow != null)
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(() =>
                    {
                        if (WindowState == WindowState.Minimized)
                        {
                            WindowState = WindowState.Normal;
                        }

                        Activate();
                        OnStateChanged(this, EventArgs.Empty);
                    });
                }
                else
                {
                    mainWindow = new MainWindow();
                    content.Navigate(new HomePage());
                    mainWindow.Show();
                }
            }
        }

        private void OnApplicationExit(object sender, ExitEventArgs e)
        {
            if (Properties.Settings.Default.DebugMode && !Properties.Settings.Default.PersistentDebug)
            {
                Properties.Settings.Default.DebugMode = false;
                Properties.Settings.Default.Save();
            }
        }

        private void OnStateChanged(object sender, EventArgs args)
        {
            if (WindowState == WindowState.Minimized)
            {
                content.Navigate(new HomePage());
                if (IsVisible && IsActive)
                {
                    Hide();
                    notifyIcon.Visible = true;
                    watcher.EnableWatcher();
                    if (watcher.watching)
                    {
                        notifyIcon.ShowBalloonTip(1000, "I2A", "Application is watching", ToolTipIcon.Info);
                    }
                }
            }
            else if (WindowState == WindowState.Normal)
            {
                if (!IsVisible && IsActive)
                {
                    Show();
                    notifyIcon.Visible = false;
                }
            }
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
            notifyIcon.Visible = false;
            watcher.stopWatch();
            System.Windows.MessageBox.Show("I2A application is closing");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            key.SetValue("IurAir", System.Reflection.Assembly.GetExecutingAssembly().Location);
        }
    private void LoadHomePage(object sender, RoutedEventArgs e)
        {
            content.Navigate(new HomePage());
        }
    }
}


