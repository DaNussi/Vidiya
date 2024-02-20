using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vidiya.Content;
using Vidiya.Content.YouTube;

namespace Vidiya.Managers
{
    public class ContentManager : Manager
    {
        public List<ContentSource> contentSources = new();
        public event EventHandler<List<ContentSource>>? contentSourcesChanged;

        public ContentManager(LogManager logManager) : base(logManager) { }

        public override void Init()
        {
            contentSources.Add(new YouTubeSingleContentSource("https://www.youtube.com/watch?v=_NvVaUTBAVE"));
            contentSources.Add(new YouTubePlaylistContentSource("https://www.youtube.com/watch?v=_NvVaUTBAVE"));
            contentSources.ForEach(e => e.OnSetup());

            contentSourcesChanged?.Invoke(this, contentSources);

            logger.log(LogType.Info, "contentSources: " + contentSources.Count);
        }

        public override void Exit()
        {

        }
    }
}
