using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Vidiya.Content;
using Vidiya.Content.YouTube;
using Vidiya.Managers;

namespace Vidiya.Elements.Content.YouTube
{
    /// <summary>
    /// Interaktionslogik für YouTubeSingleContentSourceDisplay.xaml
    /// </summary>
    public partial class YouTubeSingleContentSourceDisplay : UserControl
    {

        public YouTubeSingleContentSourceDisplay(YouTubeSingleContentSource source)
        {
            InitializeComponent();

            ContentView.Children.Clear();

            foreach (ContentResource contentResource in source.content)
            {
                UserControl userControl = contentResource.GetUserControl();
                userControl.Width = ContentView.ActualWidth;
                ContentView.Children.Add(userControl);
                VidiyaManager.instance.LogManager.log(LogType.Info, "Added ContentResource: " + contentResource.uri);
            }
        }

    }
}
