using System.Windows;
using Vidiya.Managers;

namespace Vidiya.Windows
{
    /// <summary>
    /// Interaktionslogik für MediaWindow.xaml
    /// </summary>
    public partial class MediaWindow : Window
    {
        public MediaWindow()
        {
            InitializeComponent();
        }

        private void Media_Loaded(object sender, RoutedEventArgs e)
        {
            VidiyaManager.instance.MediaManager.addMediaElement(Media);
        }

        private void FullscreenButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
            this.WindowStyle = WindowStyle.SingleBorderWindow;
        }

        private void OverscanButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
            this.WindowStyle = WindowStyle.None;
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
                this.WindowStyle = WindowStyle.SingleBorderWindow;
            }
        }
    }
}
