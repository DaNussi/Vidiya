using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeDLSharp;

namespace Vidiya.Managers
{
    public class YouTubeDLManager : Manager
    {
        private YoutubeDL youtubeDL = new YoutubeDL();

        public YouTubeDLManager(LogManager logger) : base(logger) { }

        public YoutubeDL GetYouTubeDL()
        {
            return youtubeDL;
        }

        public override async void Init()
        {
            if (File.Exists(DataManager.ytdlpDataFile))
            {
                logger.log(LogType.Info, "File yt-dlp.exe found!");
            }
            else
            {
                logger.log(LogType.Info, "File yt-dlp.exe NOT found! Downloading...");
                await YoutubeDLSharp.Utils.DownloadYtDlp(DataManager.appDataFolder);
                logger.log(LogType.Info, "Finished downloading yt-dlp.exe!");
            }

            if (File.Exists(DataManager.ffmpegDataFile))
            {
                logger.log(LogType.Info, "File ffmpeg.exe found!");
            }
            else
            {
                logger.log(LogType.Info, "File ffmpeg.exe NOT found! Downloading...");
                await YoutubeDLSharp.Utils.DownloadFFmpeg(DataManager.appDataFolder);
                logger.log(LogType.Info, "Finished downloading ffmpeg.exe!");
            }




            youtubeDL.YoutubeDLPath = DataManager.ytdlpDataFile;
            youtubeDL.FFmpegPath = DataManager.ffmpegDataFile;
        }

        public override void Exit()
        {

        }
    }
}
