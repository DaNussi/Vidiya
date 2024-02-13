using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;
using YoutubeDLSharp;

namespace Vidiya
{
    class ContentManager
    {
        private static YoutubeDL youtubeDL = new YoutubeDL();
        public static ContentCollection contentCollection = new ContentCollection();

        public static bool isInitialized = false;
        public static async void Init(Action<LogMessageType, string> log)
        {
            if(isInitialized) return;
            isInitialized = true;

            if (File.Exists(DataManager.ytdlpDataFile))
            {
                log(LogMessageType.Info, "File yt-dlp.exe found!");
            }
            else
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




            youtubeDL.YoutubeDLPath = DataManager.ytdlpDataFile;
            youtubeDL.FFmpegPath = DataManager.ffmpegDataFile;

            var contentFolders = Directory.GetDirectories(DataManager.contentDataFolder);
            foreach (var folder in contentFolders)
            {
                ContentState state = DataManager.LoadContentState(folder);
                switch(state.status)
                {
                    case ContentStatus.Ready:
                    case ContentStatus.Failed:
                    case ContentStatus.Pending: break;
                    default:
                        state.status = ContentStatus.Failed;
                        break;
                }
                add_content(new ContentListBoxItem(state));
            }

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Tick += scan_Timer;
            timer.Start();
        }

        private static void scan_Timer(object sender, EventArgs e)
        {
            foreach (ContentListBoxItem item in contentCollection.ToList())
            {
                ContentState state = item.state;
                if (state.status == ContentStatus.Failed) continue;

                switch(state.status)
                {
                    case ContentStatus.Failed:
                        continue;
                    case ContentStatus.Pending:
                        scan_Pending(item); 
                        break;
                }



            }

        }

        private static async void scan_Pending(ContentListBoxItem item)
        {
            var folder = Guid.NewGuid().ToString();
            var path = Path.Combine(DataManager.contentDataFolder, folder);
            item.state.path = path;
            Directory.CreateDirectory(path);

            switch (item.state.type) {
                case ContentType.YouTubeVideo:
                    youtube_video_download(item);
                    break;
                case ContentType.YouTubePlaylist:
                    youtube_playlist_download(item);
                    break;
            }

        }

        public static async void youtube_video_download(ContentListBoxItem item)
        {
            item.SetStatus(ContentStatus.DownloadingMeta);
            DataManager.SaveContentState(item);
            var meta = await youtubeDL.RunVideoDataFetch(item.state.url);
            if (!meta.Success)
            {
                item.SetStatus(ContentStatus.Failed, "Filed to fetch metadata! " + meta.ErrorOutput);
                DataManager.SaveContentState(item);
                return;
            }
            item.state.title = meta.Data.Title;

            item.SetStatus(ContentStatus.DownloadingContent);
            DataManager.SaveContentState(item);
            var progress = new Progress<DownloadProgress>(p => { if (item.state.status == ContentStatus.DownloadingContent) item.SetStatus(ContentStatus.DownloadingContent, "Progress: " + p.Progress); });
            var cts = new CancellationTokenSource();
            item.downloadCancelationToken = cts;
            youtubeDL.OutputFolder = item.state.path;
            try
            {
                var content = await youtubeDL.RunVideoDownload(item.state.url, recodeFormat: YoutubeDLSharp.Options.VideoRecodeFormat.Mp4, progress: progress, ct: cts.Token);
                if (!content.Success) throw new Exception(content.ErrorOutput.ToString());
            } catch (Exception e)
            {
                item.SetStatus(ContentStatus.Failed, "Filed to fetch video! " + e.Message);
                DataManager.SaveContentState(item);
                return;
            }


            item.downloadCancelationToken = null;
            item.SetStatus(ContentStatus.Ready);
            DataManager.SaveContentState(item);
        }

        public static async void youtube_playlist_download(ContentListBoxItem item)
        {
            item.SetStatus(ContentStatus.DownloadingContent);
            DataManager.SaveContentState(item);
            var progress = new Progress<DownloadProgress>(p => {if(item.state.status == ContentStatus.DownloadingContent) item.SetStatus(ContentStatus.DownloadingContent, "Progress: " + p.Progress + " | Video: " + p.VideoIndex); });
            var cts = new CancellationTokenSource();
            item.downloadCancelationToken = cts;
            youtubeDL.OutputFolder = item.state.path;
            try
            {
                var content = await youtubeDL.RunVideoPlaylistDownload(item.state.url, recodeFormat: YoutubeDLSharp.Options.VideoRecodeFormat.Mp4, progress: progress, ct: cts.Token);
                if (!content.Success) throw new Exception(content.ErrorOutput.ToString());
                item.state.title = content.Data.Length + " Videos";
            }
            catch (Exception e)
            {
                item.SetStatus(ContentStatus.Failed, "Filed to fetch playlist ! " + e.Message);
                DataManager.SaveContentState(item);
                return;
            }

            item.downloadCancelationToken = null;
            item.SetStatus(ContentStatus.Ready);
            DataManager.SaveContentState(item);
        }

        public static void add_content(ContentListBoxItem item)
        {
            ContentState state = item.state;
            if(state.status == ContentStatus.Unknown)
            {
                if (state.url.Contains("youtube.com") || state.url.Contains("musi.youtube.com"))
                {
                    if (state.url.Contains("/watch"))
                    {
                        item.SetType(ContentType.YouTubeVideo);
                        item.SetStatus(ContentStatus.Pending);
                    }
                    else if (state.url.Contains("/playlist"))
                    {
                        item.SetType(ContentType.YouTubePlaylist);
                        item.SetStatus(ContentStatus.Pending);
                    }
                    else
                    {
                        item.SetType(ContentType.YoutubeUnkown);
                        item.SetStatus(ContentStatus.Failed);
                    }
                }
                else if (state.url.Contains("spotify.com"))
                {
                    if (state.url.Contains("/track"))
                    {
                        item.SetType(ContentType.SpotifyVideo);
                        item.SetStatus(ContentStatus.Pending);
                    }
                    else if (state.url.Contains("/playlist"))
                    {
                        item.SetType(ContentType.SpotifyPlaylist);
                        item.SetStatus(ContentStatus.Pending);
                    }
                    else
                    {
                        item.SetType(ContentType.SpotifyUnkown);
                        item.SetStatus(ContentStatus.Failed);
                    }
                }
            }

            item.state = state;
            item.SetStatus(state.status);
            item.SetType(state.type);
            item.OnStateChange();

            contentCollection.Add(item);
        }

        public static void remove_content(ContentListBoxItem item)
        {
            if(item.downloadCancelationToken != null) { item.downloadCancelationToken.Cancel(); }
            if(contentCollection.Contains(item)) contentCollection.Remove(item);
            
            if(Directory.Exists(item.state.path)) Directory.Delete(item.state.path, true);
        }

    }


    public class ContentState
    {
        public ContentStatus status { get; set; }
        public ContentType type { get; set; }
        public string path { get; set; }
        public string title { get; set; }
        public string url { get; set; }

        public ContentState(ContentStatus status, ContentType type, string path, string title, string url)
        {
            this.status = status;
            this.type = type;
            this.path = path;
            this.title = title;
            this.url = url;
        }

        public ContentState() { }
    }
}
