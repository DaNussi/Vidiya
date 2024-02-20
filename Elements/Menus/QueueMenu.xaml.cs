using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Vidiya.Content;
using Vidiya.Managers;

namespace Vidiya.Elements.Menus
{
    /// <summary>
    /// Interaktionslogik für QueueMenu.xaml
    /// </summary>
    public partial class QueueMenu : UserControl
    {
        public LogManager logger = VidiyaManager.instance.LogManager;
        public List<ContentSource> contentSources = new();
        public List<UserControl> contentSourceDisplays = new();

        public QueueMenu()
        {
            InitializeComponent();
            VidiyaManager.instance.ContentManager.contentSourcesChanged += ContentManager_contentSourcesChanged;
            ContentManager_contentSourcesChanged(this, VidiyaManager.instance.ContentManager.contentSources);
        }

        private void ContentManager_contentSourcesChanged(object? sender, List<ContentSource> contentSources)
        {
            this.contentSources.Clear();
            this.contentSources.AddRange(contentSources);
            logger.log(LogType.Info, "contentSources: " + contentSources.Count);

            ContentView.Children.Clear();
            contentSourceDisplays.Clear();
            foreach (ContentSource source in contentSources)
            {
                UserControl userControl = source.GetUserControl();
                userControl.Width = ContentView.ActualWidth;
                contentSourceDisplays.Add(userControl);
                ContentView.Children.Add(userControl);

                logger.log(LogType.Info, "Added ContentSource: " + source.GetType().Name + " " + source.content.Count);
            }

        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            VidiyaManager.instance.MenuManager.returnToPrevious();
        }

        private void ContentView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            contentSourceDisplays.ForEach(contentSourceDisplay => contentSourceDisplay.Width = ContentView.ActualWidth);
        }
    }
}
