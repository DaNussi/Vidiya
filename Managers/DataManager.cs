using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Vidiya
{
    class DataManager
    {
        public static string appDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Vidiya");
        public static string contentDataFolder = Path.Combine(appDataFolder, "content");

        public static string stateDataFile = Path.Combine(appDataFolder, "state.json");
        public static string ffmpegDataFile = Path.Combine(appDataFolder, "ffmpeg.exe");
        public static string ytdlpDataFile = Path.Combine(appDataFolder, "yt-dlp.exe");

        public static void Init(Action<LogMessageType, string> logger)
        {
            logger.Invoke(LogMessageType.Info, "Creating appdata folder");
            Directory.CreateDirectory(appDataFolder);
            logger.Invoke(LogMessageType.Info, "Creating content folder");
            Directory.CreateDirectory(contentDataFolder);


            if (!File.Exists(stateDataFile))
            {
                logger.Invoke(LogMessageType.Info, "Creating sate.json file becouse it wasn't found");
                SaveState(new GlobalState());
            }

        }

        internal static GlobalState LoadState()
        {
            if (!File.Exists(stateDataFile))
            {
                return new GlobalState();
            }

            string jsonString = File.ReadAllText(stateDataFile);
            GlobalState state = JsonSerializer.Deserialize<GlobalState>(jsonString);
            return state;
        }

        internal static void SaveState(GlobalState state)
        {
            string jsonString = JsonSerializer.Serialize(state);
            File.WriteAllText(Path.Combine(appDataFolder, "state.json"), jsonString);
        }


        internal static ContentState LoadContentState(string folder)
        {
            string stateDataFile = Path.Combine(folder, "state.json");

            if (!File.Exists(stateDataFile))
            {
                return new ContentState();
            }

            string jsonString = File.ReadAllText(stateDataFile);
            ContentState state = JsonSerializer.Deserialize<ContentState>(jsonString);
            return state;
        }

        internal static void SaveContentState(ContentState state)
        {
            string stateDataFile = Path.Combine(state.path, "state.json");
            string jsonString = JsonSerializer.Serialize(state);
            
            if(Directory.Exists(state.path)) File.WriteAllText(stateDataFile, jsonString);
        }

        internal static void SaveContentState(ContentListBoxItem item)
        {
            SaveContentState(item.state);
            item.OnStateChange();
        }

    }
}
