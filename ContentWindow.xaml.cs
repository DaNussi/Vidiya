using YoutubeDLSharp;
using System;
using System.IO;
using System.Windows;

namespace Vidiya
{
    /// <summary>
    /// Interaktionslogik für ContentWindow.xaml
    /// </summary>
    public partial class ContentWindow : Window
    {
        public YoutubeDL youtubeDL = new YoutubeDL();

        private static string appDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Vidiya");
        private static string ffmpegDataFile = Path.Combine(appDataFolder, "ffmpeg.exe");
        private static string ytdlpDataFile = Path.Combine(appDataFolder, "yt-dlp.exe");
        private static string youtubeDataFile = Path.Combine(appDataFolder, "youtube");

        public ContentWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            youtubeDL.YoutubeDLPath = ytdlpDataFile;
            youtubeDL.FFmpegPath = ffmpegDataFile;
            youtubeDL.OutputFolder = youtubeDataFile;

            if(!File.Exists(ytdlpDataFile)) await YoutubeDLSharp.Utils.DownloadYtDlp(appDataFolder);
            if (!File.Exists(ffmpegDataFile)) await YoutubeDLSharp.Utils.DownloadFFmpeg(appDataFolder);


            Directory.CreateDirectory(youtubeDataFile);
            var res = await youtubeDL.RunVideoDownload("https://www.youtube.com/watch?v=bq9ghmgqoyc", mergeFormat: YoutubeDLSharp.Options.DownloadMergeFormat.Mp4);

            string path = res.Data;
        }



    }
}
