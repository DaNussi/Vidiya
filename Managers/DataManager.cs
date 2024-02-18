using System;
using System.IO;
using System.Text.Json;
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

        internal static GlobalState LoadState()
        {
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
