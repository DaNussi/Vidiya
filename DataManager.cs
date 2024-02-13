using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Vidiya
{
    class DataManager
    {
        public static string appDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Vidiya");
        public static string youtubeDataFolder = Path.Combine(appDataFolder, "youtube");

        public static string stateDataFile = Path.Combine(appDataFolder, "state.json");
        public static string ffmpegDataFile = Path.Combine(appDataFolder, "ffmpeg.exe");
        public static string ytdlpDataFile = Path.Combine(appDataFolder, "yt-dlp.exe");

        public static void Init(Action<LogMessageType, string> logger)
        {
            logger.Invoke(LogMessageType.Info, "Creating appdata folder");
            Directory.CreateDirectory(appDataFolder);
            logger.Invoke(LogMessageType.Info, "Creating youtube folder");
            Directory.CreateDirectory(youtubeDataFolder);


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
    }
}
