using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vidiya.Content;
using Vidiya.Content.YouTube;

namespace Vidiya.Managers
{
    public class ContentManager : Manager
    {
        public ObservableCollection<ContentSource> contentSources = new();
        private DataManager dataManager;

        public ContentManager(LogManager logManager, DataManager dataManager) : base(logManager) {
            this.dataManager = dataManager;
        }

        public override void Init()
        {
            var folders = Directory.GetDirectories(DataManager.contentDataFolder);

            foreach (var folder in folders)
            {
                string id = Path.GetFileName(folder);
                ContentSourceState? contentSourceState = dataManager.LoadContentSource(id);
                if(contentSourceState == null) {
                    logger.log(LogType.Info, "Found content " + id + " but could find/parse state.json!");
                    continue;
                }
                logger.log(LogType.Info, "Found content " + id + " | Type: " + contentSourceState.type);

                ContentSource? source = null;
                switch (contentSourceState.type)
                {
                    case ContentType.YouTubePlaylist:
                        source = new YouTubePlaylistContentSource();
                        this.contentSources.Add(source);
                        break;
                    //case ContentType.YouTubeSingle:
                    //    source = new YouTubeSingleContentSource();
                    //    this.contentSources.Add(source);
                    //    break;
                }
                source?.TriggerRebuild(contentSourceState);

            }

            //ContentSource s = new YouTubePlaylistContentSource("https://www.youtube.com/playlist?list=PL-FiWGr6Pwnxc641jIw3ZFCErw-3aPCCR");
            //contentSources.Add(s);
            //s.OnSetup();

        }

        public override void Exit()
        {

        }
    }
}
