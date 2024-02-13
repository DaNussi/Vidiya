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


        public ContentWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            youtubeDL.YoutubeDLPath = DataManager.ytdlpDataFile;
            youtubeDL.FFmpegPath = DataManager.ffmpegDataFile;
            youtubeDL.OutputFolder = DataManager.youtubeDataFolder;


            var res = await youtubeDL.RunVideoDownload("https://www.youtube.com/watch?v=bq9ghmgqoyc", mergeFormat: YoutubeDLSharp.Options.DownloadMergeFormat.Mp4);

            string path = res.Data;
        }

        private void AddContentTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == System.Windows.Input.Key.Enter)
            {

            }
        }

        private void AddContentTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
    }
}
