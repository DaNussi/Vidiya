using System;
using System.Threading;
using System.Windows.Controls;
using Vidiya.Elements.Content;
using Vidiya.Managers;
using YoutubeDLSharp;

namespace Vidiya.Content.YouTube
{
    public class YouTubeSingleContentSource : ContentSource
    {
        string url;

        public YouTubeSingleContentSource(string url)
        {
            this.url = url;
        }

        public YouTubeSingleContentSource() { }

        public override UserControl GetUserControl()
        {
            var userControl = new GenericSingleDisplay();
            userControl.SetContent(this);
            return userControl;
        }

        public override async void OnSetup()
        {
            YoutubeDL youtubeDL = VidiyaManager.instance.YouTubeDLManager.GetYouTubeDL();
            DataManager dataManager = VidiyaManager.instance.DataManager;


            this.SetStateError("Not Implemented!");
        }

        public override ContentType GetContentType()
        {
            return ContentType.YouTubeSingle;
        }

        public override void OnRebuild()
        {

        }
    }

    public class YouTubeSingleContent : ContentResource
    {
        public YouTubeSingleContent(Uri uri, string title, string description = "") : base(uri, title, description) { }

        public override UserControl GetUserControl()
        {
            var userControl = new GenericContentDisplay();
            userControl.SetContent(this);
            return userControl;
        }
    }
}
