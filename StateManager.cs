using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Vidiya
{
    public class StateManager
    {
        private static string appDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Vidiya");
        private static string stateDataFile = Path.Combine(appDataFolder, "state.json");

        public static GlobalState Load()
        {
            if(!File.Exists(stateDataFile))
            {
                return new GlobalState();
            }

            string jsonString = File.ReadAllText(stateDataFile);
            GlobalState state = JsonSerializer.Deserialize<GlobalState>(jsonString);
            return state;
        }

        public static void Save(GlobalState state) {
            string jsonString = JsonSerializer.Serialize(state);
            Directory.CreateDirectory(appDataFolder);
            File.WriteAllText(Path.Combine(appDataFolder, "state.json"), jsonString);
        }

    }

    public class GlobalState
    {
        public string? contentSource { get; set; }
        public List<DisplayState> displayStates { get; set; }
    }

    public class DisplayState
    {
        public double width { get; set; }
        public double hight { get; set; }
        public double top { get; set; }
        public double left { get; set; }
        public bool fullscreen { get; set; }
        public string screen { get; set; }

        public DisplayState(double width, double hight, double top, double left, bool fullscreen, string screen)
        {
            this.width = width;
            this.hight = hight;
            this.top = top;
            this.left = left;
            this.fullscreen = fullscreen;
            this.screen = screen;
        }

        public DisplayState() { }

    }

}
