using System;
using System.Windows.Controls;
using Vidiya.Elements.Content.YouTube;

namespace Vidiya.Content.YouTube
{
    public class YouTubeSingleContentSource : ContentSource
    {
        string url;

        public YouTubeSingleContentSource(string url)
        {
            this.url = url;
        }

        public override UserControl GetUserControl()
        {
            return new YouTubeSingleContentSourceDisplay(this);
        }

        public override ContentState OnSetup()
        {
            this.content.Add(new YouTubeSingleContent(new Uri(url),url, url));

            return ContentState.Error;
        }
    }

    public class YouTubeSingleContent : ContentResource
    {
        public YouTubeSingleContent(Uri uri, string title, string description = "") : base(uri, title, description) { }

        public override UserControl GetUserControl()
        {
            return new YouTubeSingleContentDisplay(this);
        }
    }
}
