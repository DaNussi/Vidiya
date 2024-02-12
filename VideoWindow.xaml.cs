using Microsoft.Win32;
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
using System.Windows.Threading;
using System.Xml.Serialization;

namespace Vidiya
{
    /// <summary>
    /// Interaktionslogik für VideoWindow.xaml
    /// </summary>
    public partial class VideoWindow : Window
    {

        public static List<VideoWindow> windows = new List<VideoWindow>();

        public VideoWindow()
        {
            InitializeComponent();
        }

        private void Fullscreen_Click(object sender, RoutedEventArgs e)
        {
            Fullscreen();
        }

        private void ExitFullscreen_Click(object sender, RoutedEventArgs e)
        {
            ExitFullscreen();
        }

        public void Fullscreen()
        {
            if (this.WindowState == WindowState.Maximized) return;
            this.WindowState = WindowState.Maximized;
            this.WindowStyle = WindowStyle.None;
            this.FullscreenButton.Visibility = Visibility.Hidden;
        }

        public void ExitFullscreen()
        {
            if (this.WindowState != WindowState.Maximized) return;
            this.WindowState = WindowState.Normal;
            this.WindowStyle = WindowStyle.SingleBorderWindow;
            this.FullscreenButton.Visibility = Visibility.Visible;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                if (this.WindowState == WindowState.Maximized) ExitFullscreen();
            }
        }

        public void Window_LocationChanged(object sender, EventArgs e)
        {
            UpdateDisplayState();
        }

        public DisplayState UpdateDisplayState()
        {
            bool fullscreen = this.WindowState == WindowState.Maximized;
            double hight = 270;
            double width = 480;
            if (!fullscreen) hight = this.Height;
            if (!fullscreen) width = this.Width;

            return new DisplayState(
                    width,hight,this.Top,this.Left,fullscreen,
                    WpfScreenHelper.Screen.FromWindow(this).DeviceName
                );
        }

        private void mediaElement_Loaded(object sender, RoutedEventArgs e)
        {
            MediaPlayerManager.register(mediaElement);


            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMicroseconds(200);
            timer.Tick += mediaElement_Timer;
            timer.Start();

            if(!windows.Contains(this)) windows.Add(this);
        }

        private void mediaElement_Unloaded(object sender, RoutedEventArgs e)
        {
            MediaPlayerManager.unregister(mediaElement);
            windows.Remove(this);
        }

        private void mediaElement_Timer(object sender, EventArgs e)
        {
            if (mediaElement.Source == null || !mediaElement.NaturalDuration.HasTimeSpan) {
                logo.Visibility = Visibility.Visible;
                window.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#024D71"));
            } else {
                logo.Visibility = Visibility.Hidden;
                window.Background = Brushes.Black;
            }
        }

    }
}
