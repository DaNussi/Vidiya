using System;
using System.Windows.Controls;
using Vidiya.Elements.Content;

namespace Vidiya.Content.YouTube
{

    public class YouTubePlaylistContentSource : ContentSource
    {
        public string url { get; }

        public YouTubePlaylistContentSource(string url) {
            this.url = url;
        }

        public override UserControl GetUserControl()
        {
            var userControl = new GenericPlaylistContentSourceDisplay();
            userControl.SetContent(this);
            return userControl;
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
            var userControl = new GenericPlaylistContentDisplay();
            userControl.SetContent(this);
            return userControl;
        }
    }
}
