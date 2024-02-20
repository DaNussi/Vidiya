using Avalonia.Controls;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Vidiya.Managers;

namespace Vidiya.Elements.Panels
{
    /// <summary>
    /// Interaktionslogik für BigPanel.xaml
    /// </summary>
    public partial class BigPanel : System.Windows.Controls.UserControl
    {
        public BigPanel()
        {
            InitializeComponent();
        }

        private void Media_Loaded(object sender, RoutedEventArgs e)
        {
            VidiyaManager.instance.MediaManager.addMediaElement(Media, true);
        }
    }
}
