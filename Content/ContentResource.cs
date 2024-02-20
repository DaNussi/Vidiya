using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Vidiya.Content
{

    public abstract class ContentResource
    {
        public string id { get; }
        public Uri uri { get; set; }
        public string title { get; set; }
        public string description { get; set; }

        public ContentResource() { }

        public ContentResource(Uri uri, string title, string description = "") 
        { 
            this.id = Guid.NewGuid().ToString();
            this.uri = uri;
            this.title = title;
            this.description = description;
        }

        public abstract UserControl GetUserControl();
    }

}
