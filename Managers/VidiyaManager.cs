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

        private Manager[] mangers = { };

        public LogManager LogManager { get; }
        public DataManager DataManager { get; }
        public MediaManager MediaManager { get; }

        public VidiyaManager() : base(new LogManager())
        {
            this.LogManager = this.logger;
            this.DataManager = new DataManager(LogManager);
            this.MediaManager = new MediaManager(LogManager);

            this.mangers = new Manager[] { LogManager, DataManager };
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
