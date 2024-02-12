using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Vidiya
{
    class MediaPlayerManager
    {
        public static MediaPlayerManager instance = new MediaPlayerManager();

        private static List<MediaElement> mediaPlayers = new List<MediaElement>();
        private static MediaState CurrentState = MediaState.Manual;

        public static void register(MediaElement mediaPlayer)
        {

            if (mediaPlayers.Count >= 1)
            {
                mediaPlayer.Position = mediaPlayers[0].Position;
                mediaPlayer.Source = mediaPlayers[0].Source;
            }

            switch (CurrentState)
            {
                case MediaState.Play: mediaPlayer.Play(); break;
                case MediaState.Pause: mediaPlayer.Pause(); break;
                case MediaState.Stop: mediaPlayer.Stop(); break;
            }

            mediaPlayers.Add(mediaPlayer);
        }

        public static void unregister(MediaElement mediaPlayer)
        {
            mediaPlayers.Remove(mediaPlayer);
        }

        public static bool isPlaying() {
            return CurrentState == MediaState.Play;
        }

        public static void loadContent(Uri file)
        {
            mediaPlayers.ForEach(m => { m.Source = file; });
            pause();
        }

        public static void play()
        {
            CurrentState = MediaState.Play;
            mediaPlayers.ForEach((m) => { m.Play(); });
        }

        public static void stop()
        {
            CurrentState = MediaState.Stop;
            mediaPlayers.ForEach((m) => { m.Stop(); });
        }

        public static void pause()
        {
            CurrentState = MediaState.Pause;
            mediaPlayers.ForEach((m) => { m.Pause(); });
        }

        public static void sync(TimeSpan position)
        {
            mediaPlayers.ForEach((m) => { m.Position = position; });
        }
    }
}
