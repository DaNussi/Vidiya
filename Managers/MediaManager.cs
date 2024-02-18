using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Vidiya.Managers
{
    public class MediaManager : Manager
    {
        MediaElement? rootMediaElemen;
        List<MediaElement> mediaElements = new List<MediaElement>();
        MediaState mediaState = MediaState.Unknown;

        public MediaManager(LogManager logger) : base(logger)
        {

        }

        public void addMediaElement(MediaElement media, bool isRoot = false)
        {
            mediaElements.Add(media);
            if (isRoot)
            {
                rootMediaElemen = media;
                Content(new Uri("C:\\Users\\danie\\Downloads\\Ads\\Beverage.mp4"));
            }
            else
            {
                media.Volume = 0;

                if(rootMediaElemen != null)
                {
                    media.Source = rootMediaElemen.Source;
                    media.Position = rootMediaElemen.Position;
                }

                switch (mediaState)
                {
                    case MediaState.Playing:
                        media.Play();
                        break;
                    case MediaState.Paused: 
                        media.Pause();
                        break;
                    case MediaState.Stopped:
                        media.Stop();
                        break;
                }
            }
        }

        public override void Init()
        {

        }

        public override void Exit()
        {

        }

        public void Play()
        {
            mediaState = MediaState.Playing;
            mediaElements.ForEach((m) => { m.Play(); });
        }

        public void Stop()
        {
            mediaState = MediaState.Stopped;
            mediaElements.ForEach((m) => { m.Stop(); });
        }

        public void Pause()
        {
            mediaState = MediaState.Paused;
            mediaElements.ForEach((m) => { m.Pause(); });
        }

        public void Sync()
        {
            if (rootMediaElemen == null) return;
            mediaElements.ForEach((m) => { m.Position = rootMediaElemen.Position; });
        }

        public void Volume(double volume)
        {
            if(rootMediaElemen == null) return;
            rootMediaElemen.Volume = volume;
        }

        public void Content(Uri uri)
        {
            this.mediaElements.ForEach(e => e.Source = uri);
        }

        public enum MediaState
        {
            Playing,
            Paused,
            Stopped,
            Unknown,
        }
    }
}
