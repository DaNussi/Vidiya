using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Vidiya.Content
{
    public abstract class ContentSource
    {
        public ContentState state = ContentState.Pending;
        public List<ContentResource> content = new();

        public abstract UserControl GetUserControl();

        public abstract ContentState OnSetup();
    }
}
