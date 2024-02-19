using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Vidiya.Content
{

    public abstract class ContentResource
    {
        public Uri uri { get; }
        public string title { get; }
        public string description { get; }

        public ContentResource(Uri uri, string title, string description = "") 
        { 
            this.uri = uri;
            this.title = title;
            this.description = description;
        }

        public abstract UserControl GetUserControl();
    }

}
