using System;
using System.Windows.Controls;
using Vidiya.Elements.Content;
using Vidiya.Elements.Content.YouTube;

namespace Vidiya.Content.YouTube
{

    public class YouTubePlaylistContentSource<YouTubePlaylistContent> : ContentSource
    {
        public override UserControl GetUserControl()
        {
            return new YouTubePlaylistContentSourceDisplay();
        }

        public override ContentState OnSetup()
        {
            return ContentState.Error;
        }
    }

    public class YouTubePlaylistContent : ContentResource
    {
        public YouTubePlaylistContent(Uri uri, string title, string description = "") : base(uri, title, description) { }

        public override UserControl GetUserControl()
        {
            return new YouTubePlaylistContentDisplay();
        }
    }
}
