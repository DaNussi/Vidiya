using System.Windows;
using Vidiya.Managers;

namespace Vidiya.Windows
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            VidiyaManager.instance.Init();
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            VidiyaManager.instance.Exit();
        }
    }
}
