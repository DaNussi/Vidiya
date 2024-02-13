using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using YoutubeDLSharp;
using static System.Net.Mime.MediaTypeNames;

namespace Vidiya
{
    /// <summary>
    /// Interaktionslogik für ContentListBoxItem.xaml
    /// </summary>
    public partial class ContentListBoxItem : UserControl
    {
        public ContentState state;
        public CancellationTokenSource? downloadCancelationToken;

        public ContentListBoxItem(ContentState state)
        {
            this.state = state;
            InitializeComponent();
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            ContentManager.remove_content(this);
            this.Visibility = Visibility.Hidden;
        }

        public void SetStatus(ContentStatus status, string? text = null)
        {
            this.state.status = status;
            StatusTextBox.Text = status.ToString();
            if(text != null)
            {
                StatusTextBox.Text += " | " + text.ToString();
            }
        }

        public void SetType(ContentType type)
        {
            this.state.type = type;
        }

        internal void OnStateChange()
        {
            this.TitleTextBox.Text = state.title;
            this.UrlTextBox.Text = state.url;
        }

        private void RetryButton_Click(object sender, RoutedEventArgs e)
        {
            ContentManager.remove_content(this);
            this.state = new ContentState(ContentStatus.Unknown, ContentType.Unknown, null, null, this.state.url);
            ContentManager.add_content(this);
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            var contentPaths = Directory.GetFiles(this.state.path, "*.mp4");
            if(contentPaths.Length <= 0) return;
            MediaPlayerManager.loadContent(new Uri(contentPaths[0]));
        }
    }

    public enum ContentStatus
    {
        Unknown,
        Ready,
        Pending,
        DownloadingMeta,
        DownloadingContent,
        Failed
    }

    public enum ContentType
    {
        YouTubeVideo,
        YouTubePlaylist,
        YoutubeUnkown,
        SpotifyVideo,
        SpotifyPlaylist,
        SpotifyUnkown,
        Unknown
    }
}
