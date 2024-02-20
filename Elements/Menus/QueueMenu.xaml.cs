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

        public QueueMenu()
        {
            InitializeComponent();
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            VidiyaManager.instance.MenuManager.returnToPrevious();
        }

        private void ContentView_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }

        private void ContentView_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
