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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using WpfScreenHelper;

namespace Vidiya
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public GlobalState state;
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void NewDisplayButton_Click(object sender, RoutedEventArgs e)
        {

            Window win2 = new VideoWindow();
            win2.Show();
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            MediaPlayerManager.play();
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            MediaPlayerManager.pause();
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            MediaPlayerManager.stop();
        }

        private void SyncButton_Click(object sender, RoutedEventArgs e)
        {
            MediaPlayerManager.sync(mediaElement.Position);
        }

        private void ContentButton_Click(object sender, RoutedEventArgs e)
        {
            /*
            if(mediaElement.Source == null)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Media files (*.mp3;*.mpg;*.mpeg;*.mp4)|*.mp4;*.mp3;*.mpg;*.mpeg|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == true) MediaPlayerManager.loadContent(new Uri(openFileDialog.FileName));

                state.contentSource = openFileDialog.FileName;
                ContentButton.Content = "Unload Content";
            } else {
                MediaPlayerManager.loadContent(null);
                state.contentSource = null;
                ContentButton.Content = "Load Content";
            }
            */

            ContentWindow contentWindow = new ContentWindow();
            contentWindow.Show();
        }

        private void mediaElement_Loaded(object sender, RoutedEventArgs e)
        {
            MediaPlayerManager.register(mediaElement);


            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMicroseconds(200);
            timer.Tick += mediaElement_Timer;
            timer.Start();

        }

        private void mediaElement_Unloaded(object sender, RoutedEventArgs e)
        {
            MediaPlayerManager.unregister(mediaElement);
        }

        private void mediaElement_Timer(object sender, EventArgs e)
        {

            if (mediaElement.Source == null || !mediaElement.NaturalDuration.HasTimeSpan)
            {
                logo.Visibility = Visibility.Visible;
                window.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#024D71"));
                statusLabel.Content = "Waiting...";
            }
            else
            {
                logo.Visibility = Visibility.Hidden;
                window.Background = Brushes.Black;
                statusLabel.Content = String.Format("{0} / {1}", mediaElement.Position.ToString(@"mm\:ss"), mediaElement.NaturalDuration.TimeSpan.ToString(@"mm\:ss"));
            }
        }


        private void MainWindow_Closed(Object Sender, EventArgs E)
        {
            state.displayStates = new List<DisplayState>();

            foreach(VideoWindow videoWindow in VideoWindow.windows)
            {
                state.displayStates.Add(videoWindow.UpdateDisplayState());
            }

            StateManager.Save(state);
            Environment.Exit(0);
        }

        private void MainWindow_Loaded(object sender, EventArgs e)
        {
            state = StateManager.Load();

            Dictionary<string, Screen> screens = new Dictionary<string, Screen>();
            foreach (Screen screen in Screen.AllScreens)
            {
                screens.Add(screen.DeviceName, screen);
            }

            if (state.displayStates != null)
            {
                foreach (DisplayState displayState in state.displayStates)
                {
                    VideoWindow window = new VideoWindow();
                    window.Show();
                    WindowHelper.SetWindowPosition(window, WpfScreenHelper.Enum.WindowPositions.Center, screens[displayState.screen]);
                    window.Top = displayState.top;
                    window.Left = displayState.left;
                    window.Width = displayState.width;
                    window.Height = displayState.hight;

                    if (displayState.fullscreen) window.Fullscreen();
                }
            }

            if(state.contentSource != null) MediaPlayerManager.loadContent(new Uri(state.contentSource));
        }
    }
}
