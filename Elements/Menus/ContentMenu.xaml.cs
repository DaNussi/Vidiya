using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
using Vidiya.Managers;

namespace Vidiya.Elements.Menus
{
    /// <summary>
    /// Interaktionslogik für ContentMenu.xaml
    /// </summary>
    public partial class ContentMenu : UserControl
    {

        public LogManager logger = VidiyaManager.instance.LogManager;
        public List<ContentSource> contentSources = new();
        public List<UserControl> contentSourceDisplays = new();

        public ContentMenu()
        {
            InitializeComponent();

        }

        private bool isConnected = false;
        private void ContentView_Loaded(object sender, RoutedEventArgs e)
        {
            if (isConnected) return; isConnected = true;

            VidiyaManager.instance.ContentManager.contentSources.CollectionChanged += ContentManager_ContentSourcesChanged; 
            ContentManager_UpdateContent();
        }

        private void ContentManager_ContentSourcesChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            ContentManager_UpdateContent();
        }

        private void ContentManager_UpdateContent()
        {
            var contentSources = VidiyaManager.instance.ContentManager.contentSources.ToList();

            this.contentSources.Clear();
            this.contentSources.AddRange(contentSources);
            logger.log(LogType.Info, "contentSources: " + contentSources.Count);

            ContentView.Children.Clear();
            contentSourceDisplays.Clear();
            foreach (ContentSource source in contentSources)
            {
                UserControl userControl = source.GetUserControl();
                userControl.Width = ContentView.ActualWidth;
                userControl.Margin = new Thickness(0, 5, 0, 5);
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
