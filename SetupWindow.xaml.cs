using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using YoutubeDLSharp;

namespace Vidiya
{
    /// <summary>
    /// Interaktionslogik für SetupWindow.xaml
    /// </summary>
    public partial class SetupWindow : Window
    {
        public SetupWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            log(LogMessageType.Info, "Starting software...");

            DataManager.Init(log);


            if (File.Exists(DataManager.ytdlpDataFile))
            {
                log(LogMessageType.Info, "File yt-dlp.exe found!");
            } else
            {
                log(LogMessageType.Info, "File yt-dlp.exe NOT found! Downloading...");
                await YoutubeDLSharp.Utils.DownloadYtDlp(DataManager.appDataFolder);
                log(LogMessageType.Info, "Finished downloading yt-dlp.exe!");
            }

            if (File.Exists(DataManager.ffmpegDataFile))
            {
                log(LogMessageType.Info, "File ffmpeg.exe found!");
            }
            else
            {
                log(LogMessageType.Info, "File ffmpeg.exe NOT found! Downloading...");
                await YoutubeDLSharp.Utils.DownloadFFmpeg(DataManager.appDataFolder);
                log(LogMessageType.Info, "Finished downloading ffmpeg.exe!");
            }

            MainWindow mainWindow = new MainWindow();

            log(LogMessageType.Info, "Finished setup");
            log(LogMessageType.Info, "Opening main window");

            Thread.Sleep(1000);

            this.Hide();
            mainWindow.Show();
        }

        public void log(LogMessageType type,String text)
        {
            logTextBlock.Text += type + " | " + text + "\n";
        }
    }

    public enum LogMessageType
    {
        Info,
        Warning,
        Error,
    }
}
