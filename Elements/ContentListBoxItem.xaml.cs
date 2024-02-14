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
        public bool useStateInizilaiced = false;

        public ContentListBoxItem(ContentState state)
        {
            this.state = state;

            InitializeComponent();

            string group = Guid.NewGuid().ToString();
            this.ContentUseMusicRadialButton.GroupName = group;
            this.ContentUseAdRadialButton.GroupName = group;
            useStateInizilaiced = true;
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

            switch(this.state.use)
            {
                case ContentUse.music:
                    this.ContentUseMusicRadialButton.IsChecked = true;
                    break;
                case ContentUse.ad:
                    this.ContentUseAdRadialButton.IsChecked = true;
                    break;
            }
        }

        private void RetryButton_Click(object sender, RoutedEventArgs e)
        {
            ContentManager.remove_content(this);
            this.state = new ContentState(ContentStatus.Unknown, ContentType.Unknown, ContentUse.music, null, null, this.state.url);
            ContentManager.add_content(this);
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            //var content = ContentManager.get_content(this);
            //if (content.Count <= 0) return;
            //MediaPlayerManager.loadContent(content[0]);
        }

        private void ContentUseMusicRadialButton_Checked(object sender, RoutedEventArgs e)
        {
            if (!useStateInizilaiced || this.state.path == null) return;
            this.state.use = ContentUse.music;
            DataManager.SaveContentState(this);
        }

        private void ContentUseAdRadialButton_Checked(object sender, RoutedEventArgs e)
        {
            if (!useStateInizilaiced || this.state.path == null) return;
            this.state.use = ContentUse.ad;
            DataManager.SaveContentState(this);
        }
    }

    public enum ContentUse
    {
        music,
        ad
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
        Unknown,
        Failed,
        Folder
    }
}
