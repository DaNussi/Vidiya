using System;
using System.IO;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Media;
using Vidiya.Elements.Content;
using Vidiya.Managers;
using YoutubeDLSharp;

namespace Vidiya.Content.YouTube
{

    public class YouTubePlaylistContentSource : ContentSource
    {

        string url;

        public YouTubePlaylistContentSource(string url) : base()
        {
            this.url = url;
        }

        public YouTubePlaylistContentSource() { }

        public override UserControl GetUserControl()
        {
            var userControl = new GenericPlaylistDisplay();
            userControl.SetContent(this);
            return userControl;
        }

        public override async void OnSetup()
        {
            YoutubeDL youtubeDL = VidiyaManager.instance.YouTubeDLManager.GetYouTubeDL();
            DataManager dataManager = VidiyaManager.instance.DataManager;

            this.SetStateDownloading();
            string folder = dataManager.CreateContentSource(this.id);

            var progress = new Progress<DownloadProgress>(p => {
                if (this.state == ContentState.Downloading) this.SetStateDownloading(p.Progress, p.State.ToString());
            });
            var cts = new CancellationTokenSource();

            youtubeDL.OutputFolder = folder;
            youtubeDL.OutputFileTemplate = "%(title)s";
            var contentFetch = await youtubeDL.RunVideoPlaylistDownload(url, progress: progress, ct: cts.Token, recodeFormat: YoutubeDLSharp.Options.VideoRecodeFormat.Mp4);
            if (!contentFetch.Success)
            {
                this.SetStateError("Cotent fetch was not successfull! Error: " + contentFetch.ErrorOutput);
                return;
            }

            foreach(var file in contentFetch.Data)
            {
                var contentResource = new YouTubePlaylistContent(new Uri(url), Path.GetFileName(file).Replace(".mp4","") ?? "Unknow Title", "");
                string resourcePath = dataManager.CreateContent(contentResource.id, this.id);
                string destenationPath = Path.Combine(resourcePath, "Content.mp4");
                File.Move(file, destenationPath, true);
                contentResource.uri = new Uri(destenationPath);
                dataManager.SaveContent(contentResource, this);
                this.content.Add(contentResource);
            }

            this.SetStateReady("Ready!");
        }

        public override ContentType GetContentType()
        {
            return ContentType.YouTubePlaylist;
        }

        public override void OnRebuild()
        {
        }
    }

    public class YouTubePlaylistContent : ContentResource
    {
        public YouTubePlaylistContent(Uri uri, string title, string description = "") : base(uri, title, description) { }

        public override UserControl GetUserControl()
        {
            var userControl = new GenericContentDisplay();
            userControl.SetContent(this);
            return userControl;
        }
    }
}
