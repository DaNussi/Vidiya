using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vidiya.Managers
{
    public class VidiyaManager : Manager
    {
        public static VidiyaManager instance = new VidiyaManager();

        public static LogManager getLogManager() { return instance.LogManager; }
        public static DataManager getDataManager() { return instance.DataManager; }
        public static YouTubeDLManager getYouTubeDLManager() { return instance.YouTubeDLManager; }
        public static MediaManager getMediaManager() { return instance.MediaManager; }
        public static MenuManager getMenuManager() { return instance.MenuManager; }

        private Manager[] mangers = { };

        public LogManager LogManager { get; }
        public DataManager DataManager { get; }
        public YouTubeDLManager YouTubeDLManager { get; }
        public MediaManager MediaManager { get; }
        public ContentManager ContentManager { get; }
        public MenuManager MenuManager { get; }

        public VidiyaManager() : base(new LogManager())
        {
            this.LogManager = this.logger;
            this.DataManager = new DataManager(LogManager);
            this.YouTubeDLManager = new YouTubeDLManager(LogManager);
            this.MediaManager = new MediaManager(LogManager);
            this.ContentManager = new ContentManager(LogManager);
            this.MenuManager = new MenuManager(LogManager);

            this.mangers = new Manager[] { LogManager, DataManager, YouTubeDLManager, MediaManager , ContentManager, MenuManager };
        }

        public override void Init()
        {
            foreach (var manager in mangers)
            {
                manager.Init();
                logger.log(LogType.Info, "Initializing " + manager.GetType().Name);
            }
        }

        public override void Exit()
        {
            foreach (var manager in mangers.Reverse())
            {
                manager.Exit();
                logger.log(LogType.Info, "Exiting " + manager.GetType().Name);
            }
            Environment.Exit(0);
        }
    }
}
