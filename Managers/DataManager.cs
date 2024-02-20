using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection.Metadata;
using System.Text.Json;
using Vidiya.Content;
using static Vidiya.Managers.LogManager;

namespace Vidiya.Managers
{
    public class DataManager : Manager
    {
        public static string appDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Vidiya");
        public static string contentDataFolder = Path.Combine(appDataFolder, "content");

        public static string stateDataFile = Path.Combine(appDataFolder, "state.json");
        public static string ffmpegDataFile = Path.Combine(appDataFolder, "ffmpeg.exe");
        public static string ytdlpDataFile = Path.Combine(appDataFolder, "yt-dlp.exe");

        public DataManager(LogManager logger) : base(logger)
        {
        }

        public override void Init()
        {
            logger.log(LogType.Info, "Creating appdata folder");
            Directory.CreateDirectory(appDataFolder);
            logger.log(LogType.Info, "Creating content folder");
            Directory.CreateDirectory(contentDataFolder);
        }

        public override void Exit()
        {

        }


        public string SaveContentSource(ContentSource content)
        {
            string folder = GetContentSourcePath(content);
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

            string jsonString = JsonConvert.SerializeObject(content.GetSaveState());
            File.WriteAllText(Path.Combine(folder, "state.json"), jsonString);

            return folder;
        }

        public ContentSourceState? LoadContentSource(string contentID)
        {
            try
            {
                string folder = GetContentSourcePath(contentID);
                if(folder == null) throw new Exception("Folder was null! " + folder); 

                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };

                string jsonString = File.ReadAllText(Path.Combine(folder, "state.json"));
                var state = JsonConvert.DeserializeObject<ContentSourceState>(jsonString, settings);
                return state;
            } catch (Exception e)
            {
                logger.log(LogType.Error, e.Message);
                return null;
            }
        }

        public string CreateContentSource(string id)
        {
            string folder = GetContentSourcePath(id);
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
            return folder;
        }

        public string CreateContentSource(ContentSource contentSource)
        {
            return CreateContentSource(contentSource.id);
        }

        public string GetContentSourcePath(ContentSource contentSource)
        {
            return GetContentSourcePath(contentSource.id);
        }

        public string GetContentSourcePath(string id)
        {
            return Path.Combine(contentDataFolder, id);
        }



        public string SaveContent(ContentResource content, ContentSource source)
        {
            string folder = GetContentPath(content, source);
            if(!Directory.Exists(folder)) Directory.CreateDirectory(folder);

            string jsonString = JsonConvert.SerializeObject(content);
            File.WriteAllText(Path.Combine(folder, "state.json"), jsonString);

            return folder;
        }

        public ContentResource? LoadContent(string contentID, string sourceID)
        {
            string folder = GetContentPath(contentID, sourceID);

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            string jsonString = File.ReadAllText(Path.Combine(folder, "state.json"));
            ContentResource? state = JsonConvert.DeserializeObject<ContentResource>(jsonString, settings);
            return state;
        }

        public string CreateContent(string contentID, string sourceID)
        {
            string folder = GetContentPath(contentID, sourceID);
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
            return folder;
        }

        public string GetContentPath(ContentResource content, ContentSource source)
        {
            return GetContentPath(content.id, source.id);
        }

        public string GetContentPath(string contentID, string sourceID)
        {
            return Path.Combine(contentDataFolder, sourceID, contentID);
        }
    }
}
